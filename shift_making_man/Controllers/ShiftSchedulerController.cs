using System;
using System.Collections.Generic;
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

        public List<Shift> CreateShifts(DateTime startDate, DateTime endDate)
        {
            List<Shift> shifts = new List<Shift>();
            List<Store> stores = _storeDataAccess.GetStores(); // 修正: GetAllStores → GetStores
            List<Staff> staffList = _staffDataAccess.GetStaff();  // 修正

            foreach (Store store in stores)
            {
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    shifts.AddRange(CreateDailyShifts(store, date, staffList));
                }
            }

            // データベースにシフトを保存
            foreach (Shift shift in shifts)
            {
                _shiftDataAccess.AddShift(shift);
            }

            return shifts;
        }

        private List<Shift> CreateDailyShifts(Store store, DateTime date, List<Staff> staffList)
        {
            List<Shift> dailyShifts = new List<Shift>();

            // 忙しい時間帯のシフト作成
            dailyShifts.AddRange(CreateShiftsForTimeRange(store, date, store.BusyTimeStart, store.BusyTimeEnd, store.BusyStaffCount, staffList, true));

            // 通常の時間帯のシフト作成
            dailyShifts.AddRange(CreateShiftsForTimeRange(store, date, store.OpenTime, store.BusyTimeStart, store.NormalStaffCount, staffList, false));
            dailyShifts.AddRange(CreateShiftsForTimeRange(store, date, store.BusyTimeEnd, store.CloseTime, store.NormalStaffCount, staffList, false));

            // 休憩時間の挿入（6時間以上の連続勤務を防ぐ）
            InsertBreaks(dailyShifts);

            return dailyShifts;
        }

        private List<Shift> CreateShiftsForTimeRange(Store store, DateTime date, TimeSpan startTime, TimeSpan endTime, int requiredStaffCount, List<Staff> staffList, bool isBusyTime)
        {
            List<Shift> shifts = new List<Shift>();

            for (int i = 0; i < requiredStaffCount; i++)
            {
                Staff staff = AssignStaffForShift(date, startTime, endTime, staffList, isBusyTime);
                if (staff != null)
                {
                    Shift shift = new Shift
                    {
                        StoreID = store.StoreID,
                        StaffID = staff.StaffID,
                        ShiftDate = date,
                        StartTime = startTime,
                        EndTime = endTime,
                        Status = isBusyTime ? 1 : 0 // 状態を設定（忙しい時間帯は1、それ以外は0）
                    };
                    shifts.Add(shift);
                }
            }

            return shifts;
        }

        private Staff AssignStaffForShift(DateTime date, TimeSpan startTime, TimeSpan endTime, List<Staff> staffList, bool isBusyTime)
        {
            // スタッフの割り当てロジックを実装
            foreach (Staff staff in staffList)
            {
                if (IsStaffAvailable(staff, date, startTime, endTime, isBusyTime))
                {
                    return staff;
                }
            }

            return null;
        }

        private bool IsStaffAvailable(Staff staff, DateTime date, TimeSpan startTime, TimeSpan endTime, bool isBusyTime)
        {
            return !HasOverlappingShifts(staff, date, startTime, endTime) && !HasExceededDailyHours(staff, date, startTime, endTime);
        }

        private bool HasOverlappingShifts(Staff staff, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var existingShifts = _shiftDataAccess.GetShiftsForStaff(staff.StaffID);  // 修正
            foreach (var shift in existingShifts)
            {
                if (shift.ShiftDate.Date == date.Date && shift.StartTime < endTime && shift.EndTime > startTime)
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasExceededDailyHours(Staff staff, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var existingShifts = _shiftDataAccess.GetShiftsForStaff(staff.StaffID);  // 修正
            TimeSpan totalWorkedHours = TimeSpan.Zero;
            foreach (var shift in existingShifts)
            {
                if (shift.ShiftDate.Date == date.Date)
                {
                    totalWorkedHours += shift.EndTime - shift.StartTime;
                }
            }
            totalWorkedHours += endTime - startTime;
            return totalWorkedHours.TotalHours > 8;
        }

        private void InsertBreaks(List<Shift> shifts)
        {
            // 休憩時間の挿入（6時間以上の連続勤務を防ぐ）
            List<Shift> shiftsWithBreaks = new List<Shift>(shifts); // 修正: 新しいリストを作成
            foreach (var shift in shifts)
            {
                if (shift.EndTime - shift.StartTime > TimeSpan.FromHours(6))
                {
                    // 6時間を超える勤務の場合、休憩を挿入
                    shiftsWithBreaks.Add(new Shift
                    {
                        StoreID = shift.StoreID,
                        StaffID = shift.StaffID,
                        ShiftDate = shift.ShiftDate,
                        StartTime = shift.EndTime, // 修正
                        EndTime = shift.EndTime.Add(TimeSpan.FromHours(1)), // 1時間の休憩
                        Status = 2 // 休憩シフトのステータス（例：2）
                    });
                }
            }

            // 元のリストを置き換える
            shifts.Clear();
            shifts.AddRange(shiftsWithBreaks);
        }

        // GetStores メソッドを追加
        public List<Store> GetStores()
        {
            return _storeDataAccess.GetStores();
        }
    }
}
