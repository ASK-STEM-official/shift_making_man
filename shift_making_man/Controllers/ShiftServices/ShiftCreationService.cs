//
using System;
using System.Collections.Generic;
using System.Linq;
using shift_making_man.Models;
using shift_making_man.Data;

namespace shift_making_man.Controllers.ShiftServices
{
    public class ShiftCreationService
    {
        private readonly IStoreDataAccess _storeDataAccess;
        private readonly IStaffDataAccess _staffDataAccess;
        private readonly IShiftDataAccess _shiftDataAccess;
        private readonly IShiftRequestDataAccess _shiftRequestDataAccess;
        private readonly ShiftValidationService _validationService;
        private readonly ShiftOptimizationService _optimizationService;

        public ShiftCreationService(
            IStoreDataAccess storeDataAccess,
            IStaffDataAccess staffDataAccess,
            IShiftDataAccess shiftDataAccess,
            IShiftRequestDataAccess shiftRequestDataAccess,
            ShiftValidationService validationService,
            ShiftOptimizationService optimizationService)
        {
            _storeDataAccess = storeDataAccess;
            _staffDataAccess = staffDataAccess;
            _shiftDataAccess = shiftDataAccess;
            _shiftRequestDataAccess = shiftRequestDataAccess;
            _validationService = validationService;
            _optimizationService = optimizationService;
        }

        public List<Shift> CreateShifts(int storeId, DateTime startDate, DateTime endDate, out List<string> errors)
        {
            errors = new List<string>();
            try
            {
                Console.WriteLine($"店舗ID: {storeId} のシフト作成開始 (期間: {startDate:yyyy-MM-dd} から {endDate:yyyy-MM-dd})");

                var store = _storeDataAccess.GetStoreById(storeId);
                var staffList = _staffDataAccess.GetStaffByStoreId(storeId);
                var shiftRequests = _shiftRequestDataAccess.GetShiftRequestsByStoreId(storeId)
                    .Where(r => r.Status == 0 && !r.OriginalShiftID.HasValue)
                    .ToList();

                var shifts = new List<Shift>();
                var currentDate = startDate;

                // スタッフのシフト割り当て回数を記録
                var staffShiftCounts = staffList.ToDictionary(staff => staff.StaffID, staff => 0);

                while (currentDate <= endDate)
                {
                    var dailyShifts = CreateDailyShifts(store, currentDate, staffList, shiftRequests, staffShiftCounts, out var dailyErrors);
                    shifts.AddRange(dailyShifts);
                    errors.AddRange(dailyErrors);

                    currentDate = currentDate.AddDays(1);
                }

                var validationErrors = _validationService.GetShiftIssues(shifts);
                errors.AddRange(validationErrors);

                Console.WriteLine("シフト最適化開始");
                shifts = _optimizationService.SimulatedAnnealingOptimize(shifts);

                // 使用されたシフトリクエストのステータスを更新
                foreach (var request in shiftRequests)
                {
                    if (shifts.Any(s => s.StaffID == request.StaffID && s.ShiftDate == request.RequestDate && s.StartTime == request.RequestedStartTime && s.EndTime == request.RequestedEndTime))
                    {
                        request.Status = 1;
                    }
                    else
                    {
                        request.Status = 2;
                    }
                    _shiftRequestDataAccess.UpdateShiftRequest(request);
                }

                foreach (var shift in shifts)
                {
                    try
                    {
                        shift.Status = 0;
                        _shiftDataAccess.SaveShift(shift);
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"シフトの保存中にエラーが発生しました: {ex.Message}");
                    }
                }

                if (errors.Count > 0)
                {
                    Console.WriteLine("エラー一覧:");
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error);
                    }
                }

                return shifts;
            }
            catch (Exception ex)
            {
                errors.Add($"シフト作成中にエラーが発生しました: {ex.Message}");
                return new List<Shift>();
            }
        }

        private List<Shift> CreateDailyShifts(Store store, DateTime date, List<Staff> staffList, List<ShiftRequest> shiftRequests, Dictionary<int, int> staffShiftCounts, out List<string> errors)
        {
            errors = new List<string>();
            var shifts = new List<Shift>();

            try
            {
                var busyShiftCount = store.BusyStaffCount;
                var normalShiftCount = store.NormalStaffCount;
                var openTime = store.OpenTime;
                var closeTime = store.CloseTime;
                var busyTimeStart = store.BusyTimeStart;
                var busyTimeEnd = store.BusyTimeEnd;

                // ステータスが0のシフトリクエストのみを対象とする
                var validShiftRequests = shiftRequests.Where(r => r.Status == 0).ToList();

                for (var hour = openTime.Hours; hour < closeTime.Hours; hour++)
                {
                    var currentStartTime = new TimeSpan(hour, 0, 0);
                    var currentEndTime = new TimeSpan(hour + 1, 0, 0);

                    var shiftCount = (currentStartTime < busyTimeEnd && currentEndTime > busyTimeStart) ? busyShiftCount : normalShiftCount;
                    var hourlyShifts = CreateShiftsForHour(store, date, currentStartTime, currentEndTime, staffList, validShiftRequests, shiftCount, staffShiftCounts, out var hourErrors);

                    shifts.AddRange(hourlyShifts);
                    errors.AddRange(hourErrors);
                }
            }
            catch (Exception ex)
            {
                errors.Add($"日次シフト作成中にエラーが発生しました: {ex.Message}");
            }

            return shifts;
        }

        private List<Shift> CreateShiftsForHour(Store store, DateTime date, TimeSpan startTime, TimeSpan endTime, List<Staff> staffList, List<ShiftRequest> shiftRequests, int shiftCount, Dictionary<int, int> staffShiftCounts, out List<string> errors)
        {
            errors = new List<string>();
            var shifts = new List<Shift>();

            try
            {
                var availableStaff = staffList
                    .OrderBy(staff => staffShiftCounts[staff.StaffID])
                    .ToList();

                var staffIndex = 0;

                while (shifts.Count < shiftCount && staffIndex < availableStaff.Count)
                {
                    var staff = availableStaff[staffIndex];
                    var shift = new Shift
                    {
                        StoreID = store.StoreID,
                        StaffID = staff.StaffID,
                        ShiftDate = date,
                        StartTime = startTime,
                        EndTime = endTime,
                        Status = 0
                    };

                    shifts.Add(shift);
                    staffShiftCounts[staff.StaffID]++;
                    staffIndex++;
                }

                if (shifts.Count < shiftCount)
                {
                    errors.Add($"必要なシフト数を作成できませんでした (日付: {date:yyyy-MM-dd}, 作成済みシフト数: {shifts.Count}, 必要シフト数: {shiftCount})");
                }
            }
            catch (Exception ex)
            {
                errors.Add($"期間シフト作成中にエラーが発生しました: {ex.Message}");
            }

            return shifts;
        }
    }
}
