using System;
using System.Collections.Generic;
using System.Linq;
using shift_making_man.Models;
using shift_making_man.Data;

namespace shift_making_man.Controllers
{
    public class ShiftSchedulerController
    {
        private readonly IShiftDataAccess _shiftDataAccess;
        private readonly IStoreDataAccess _storeDataAccess;
        private readonly IStaffDataAccess _staffDataAccess;
        private readonly IShiftRequestDataAccess _shiftRequestDataAccess;

        public ShiftSchedulerController(IShiftDataAccess shiftDataAccess, IStoreDataAccess storeDataAccess, IStaffDataAccess staffDataAccess, IShiftRequestDataAccess shiftRequestDataAccess)
        {
            _shiftDataAccess = shiftDataAccess;
            _storeDataAccess = storeDataAccess;
            _staffDataAccess = staffDataAccess;
            _shiftRequestDataAccess = shiftRequestDataAccess;
        }

        public List<Store> GetStores()
        {
            return _storeDataAccess.GetStores();
        }

        public List<Shift> CreateShifts(DateTime startDate, DateTime endDate, out List<string> errors)
        {
            var shifts = new List<Shift>();
            var stores = _storeDataAccess.GetStores();
            var staffList = _staffDataAccess.GetStaff();
            errors = new List<string>();

            foreach (var store in stores)
            {
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    var dailyShifts = CreateDailyShifts(store, date, staffList, out List<string> dailyErrors);
                    shifts.AddRange(dailyShifts);
                    errors.AddRange(dailyErrors);
                }
            }

            shifts = SimulatedAnnealingOptimize(shifts);

            var shiftIssues = GetShiftIssues(shifts);
            errors.AddRange(shiftIssues);

            SaveShiftsToDatabase(shifts);

            return shifts;
        }

        private List<Shift> CreateDailyShifts(Store store, DateTime date, List<Staff> staffList, out List<string> errors)
        {
            var dailyShifts = new List<Shift>();
            errors = new List<string>();

            var busyShifts = CreateShiftsForTimeRange(store, date, store.BusyTimeStart, store.BusyTimeEnd, store.BusyStaffCount, staffList, true, out List<string> busyErrors);
            var normalMorningShifts = CreateShiftsForTimeRange(store, date, store.OpenTime, store.BusyTimeStart, store.NormalStaffCount, staffList, false, out List<string> normalMorningErrors);
            var normalEveningShifts = CreateShiftsForTimeRange(store, date, store.BusyTimeEnd, store.CloseTime, store.NormalStaffCount, staffList, false, out List<string> normalEveningErrors);

            dailyShifts.AddRange(busyShifts);
            dailyShifts.AddRange(normalMorningShifts);
            dailyShifts.AddRange(normalEveningShifts);

            errors.AddRange(busyErrors);
            errors.AddRange(normalMorningErrors);
            errors.AddRange(normalEveningErrors);

            InsertBreaks(dailyShifts);

            return dailyShifts;
        }

        private List<Shift> CreateShiftsForTimeRange(Store store, DateTime date, TimeSpan startTime, TimeSpan endTime, int requiredStaffCount, List<Staff> staffList, bool isBusyTime, out List<string> errors)
        {
            var shifts = new List<Shift>();
            errors = new List<string>();

            var availableStaff = staffList.Where(staff => IsStaffAvailable(staff, date, startTime, endTime, isBusyTime)).ToList();

            for (int i = 0; i < requiredStaffCount; i++)
            {
                if (availableStaff.Count == 0)
                {
                    errors.Add($"店舗ID: {store.StoreID} - 日付: {date.ToShortDateString()} - 時間: {startTime}から{endTime}のシフトに必要なスタッフが不足しています。");
                    break;
                }

                var staff = availableStaff.First();
                availableStaff.Remove(staff);

                var shift = new Shift
                {
                    StoreID = store.StoreID,
                    StaffID = staff.StaffID,
                    ShiftDate = date,
                    StartTime = startTime,
                    EndTime = endTime,
                    Status = isBusyTime ? 1 : 0
                };
                shifts.Add(shift);
            }

            return shifts;
        }

        private bool IsStaffAvailable(Staff staff, DateTime date, TimeSpan startTime, TimeSpan endTime, bool isBusyTime)
        {
            return !HasOverlappingShifts(staff, date, startTime, endTime) && !HasExceededDailyHours(staff, date, startTime, endTime);
        }

        private bool HasOverlappingShifts(Staff staff, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var existingShifts = _shiftDataAccess.GetShiftsForStaff(staff.StaffID);
            return existingShifts.Any(shift =>
                shift.ShiftDate.Date == date.Date &&
                shift.StartTime < endTime &&
                shift.EndTime > startTime);
        }

        private bool HasExceededDailyHours(Staff staff, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var existingShifts = _shiftDataAccess.GetShiftsForStaff(staff.StaffID);
            double totalWorkedHours = existingShifts
                .Where(shift => shift.ShiftDate.Date == date.Date)
                .Sum(shift => (shift.EndTime - shift.StartTime).TotalHours);
            totalWorkedHours += (endTime - startTime).TotalHours;
            return totalWorkedHours > 8;
        }

        private void InsertBreaks(List<Shift> shifts)
        {
            var shiftsWithBreaks = new List<Shift>();

            foreach (var shift in shifts)
            {
                shiftsWithBreaks.Add(shift);

                if (shift.EndTime - shift.StartTime > TimeSpan.FromHours(6))
                {
                    var breakShift = new Shift
                    {
                        StoreID = shift.StoreID,
                        StaffID = shift.StaffID,
                        ShiftDate = shift.ShiftDate,
                        StartTime = shift.EndTime,
                        EndTime = shift.EndTime.Add(TimeSpan.FromHours(1)),
                        Status = 2 // 休憩のステータス
                    };
                    shiftsWithBreaks.Add(breakShift);
                }
            }

            shifts.Clear();
            shifts.AddRange(shiftsWithBreaks);
        }

        private void SaveShiftsToDatabase(List<Shift> shifts)
        {
            foreach (var shift in shifts)
            {
                _shiftDataAccess.AddShift(shift);
            }
        }

        private List<Shift> SimulatedAnnealingOptimize(List<Shift> initialShifts)
        {
            var currentShifts = new List<Shift>(initialShifts);
            var bestShifts = new List<Shift>(initialShifts);
            double temperature = 10000;
            double coolingRate = 0.003;
            Random rand = new Random();

            while (temperature > 1)
            {
                var newShifts = new List<Shift>(currentShifts);
                int index1 = rand.Next(newShifts.Count);
                int index2 = rand.Next(newShifts.Count);
                var tempShift = newShifts[index1];
                newShifts[index1] = newShifts[index2];
                newShifts[index2] = tempShift;

                double currentEnergy = CalculateShiftEnergy(currentShifts);
                double newEnergy = CalculateShiftEnergy(newShifts);

                if (AcceptanceProbability(currentEnergy, newEnergy, temperature) > rand.NextDouble())
                {
                    currentShifts = new List<Shift>(newShifts);
                }

                if (CalculateShiftEnergy(currentShifts) < CalculateShiftEnergy(bestShifts))
                {
                    bestShifts = new List<Shift>(currentShifts);
                }

                temperature *= 1 - coolingRate;
            }

            return bestShifts;
        }

        private double CalculateShiftEnergy(List<Shift> shifts)
        {
            // ここにエネルギー計算のロジックを実装
            // 例: スタッフの過剰なシフト数や営業時間外シフトなど
            double energy = 0.0;
            foreach (var shift in shifts)
            {
                if (shift.StartTime < TimeSpan.FromHours(9) || shift.EndTime > TimeSpan.FromHours(21))
                {
                    energy += 10.0; // 営業時間外のシフトに高いペナルティ
                }

                // シフトが過剰に配置されている場合のペナルティなど
                // ここにさらに条件を追加することも可能です
            }
            return energy;
        }

        private double AcceptanceProbability(double currentEnergy, double newEnergy, double temperature)
        {
            if (newEnergy < currentEnergy)
            {
                return 1.0;
            }
            return Math.Exp((currentEnergy - newEnergy) / temperature);
        }

        private List<string> GetShiftIssues(List<Shift> shifts)
        {
            var issues = new List<string>();

            foreach (var shift in shifts)
            {
                if (shift.StartTime < TimeSpan.FromHours(9) || shift.EndTime > TimeSpan.FromHours(21))
                {
                    issues.Add($"店舗ID: {shift.StoreID} - 日付: {shift.ShiftDate.ToShortDateString()} - シフトの時間が営業時間外です: {shift.StartTime}から{shift.EndTime}");
                }
            }

            return issues;
        }
    }
}
