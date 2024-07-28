using System;
using System.Collections.Generic;
using System.Linq;
using shift_making_man.Models;
using shift_making_man.Data;

namespace shift_making_man.Controllers.ShiftServices
{
    public class ShiftValidationService
    {
        private readonly IStoreDataAccess _storeDataAccess;
        private readonly IShiftDataAccess _shiftDataAccess;
        private readonly IStaffDataAccess _staffDataAccess;

        public ShiftValidationService(IStoreDataAccess storeDataAccess, IShiftDataAccess shiftDataAccess, IStaffDataAccess staffDataAccess)
        {
            _storeDataAccess = storeDataAccess;
            _shiftDataAccess = shiftDataAccess;
            _staffDataAccess = staffDataAccess;
        }

        public List<string> GetShiftIssues(List<Shift> shifts)
        {
            Console.WriteLine("Validating shifts");

            var issues = new List<string>();

            // Full-Timeスタッフがいるかどうかを確認
            var fullTimeStaffCount = _staffDataAccess.GetStaffByEmploymentType("Full-Time").Count();
            if (fullTimeStaffCount < 1)
            {
                issues.Add("少なくとも1人のフルタイムスタッフが必要です。");
            }

            var groupedShifts = shifts.GroupBy(shift => shift.StoreID);
            foreach (var group in groupedShifts)
            {
                var store = _storeDataAccess.GetStoreById(group.Key.Value);
                var shiftsForStore = group.ToList();

                foreach (var shift in shiftsForStore)
                {
                    // シフト時間の重複チェック
                    if (IsShiftOverlapping(shift, shiftsForStore))
                    {
                        issues.Add($"シフトID: {shift.ShiftID} - シフトの時間が他のシフトと重複しています。");
                    }

                    // 開店時間と閉店時間を超えるシフトの確認
                    if (shift.StartTime < store.OpenTime || shift.EndTime > store.CloseTime)
                    {
                        issues.Add($"シフトID: {shift.ShiftID} - 開店時間と閉店時間を超えたシフトがあります。");
                    }

                    // 1日の労働時間の合計が8時間を超えないことを確認
                    var staffShiftsForDay = shiftsForStore
                        .Where(s => s.StaffID == shift.StaffID && s.ShiftDate == shift.ShiftDate)
                        .ToList();

                    var totalHoursWorked = staffShiftsForDay.Sum(s => (s.EndTime - s.StartTime).TotalHours);
                    if (totalHoursWorked > 8)
                    {
                        issues.Add($"スタッフID: {shift.StaffID} - 1日の労働時間が8時間を超えています。");
                    }

                    // 1日の労働時間が6時間を超えた場合に休憩があることを確認
                    if (totalHoursWorked > 6 && !HasBreakTime(staffShiftsForDay))
                    {
                        issues.Add($"スタッフID: {shift.StaffID} - 1日の労働時間が6時間を超えているため休憩が必要です。");
                    }

                    // 忙しい時間帯に必要なスタッフ数の確認
                    if (IsInBusyTimePeriod(shift, store) &&
                        !IsStaffCountSufficient(shiftsForStore, store.BusyTimeStart, store.BusyTimeEnd, store.BusyStaffCount))
                    {
                        issues.Add($"シフトID: {shift.ShiftID} - 忙しい時間帯に必要なスタッフ数が確保されていません。");
                    }
                    // 通常の時間帯に必要なスタッフ数の確認
                    else if (!IsInBusyTimePeriod(shift, store) &&
                             !IsStaffCountSufficient(shiftsForStore, store.OpenTime, store.CloseTime, store.NormalStaffCount))
                    {
                        issues.Add($"シフトID: {shift.ShiftID} - 通常の時間帯に必要なスタッフ数が確保されていません。");
                    }
                }
            }

            Console.WriteLine($"Found {issues.Count} issues");
            return issues;
        }

        private bool IsShiftOverlapping(Shift shift, List<Shift> shiftsForStore)
        {
            return shiftsForStore.Any(otherShift =>
                otherShift.ShiftID != shift.ShiftID &&
                shift.ShiftDate == otherShift.ShiftDate &&
                shift.StartTime < otherShift.EndTime &&
                shift.EndTime > otherShift.StartTime);
        }

        private bool HasBreakTime(List<Shift> shiftsForDay)
        {
            var totalHoursWorked = shiftsForDay.Sum(s => (s.EndTime - s.StartTime).TotalHours);
            return totalHoursWorked >= 7; // 6時間の労働＋1時間の休憩
        }

        private bool IsInBusyTimePeriod(Shift shift, Store store)
        {
            return shift.StartTime < store.BusyTimeEnd && shift.EndTime > store.BusyTimeStart;
        }

        private bool IsStaffCountSufficient(List<Shift> shiftsForStore, TimeSpan startTime, TimeSpan endTime, int requiredStaffCount)
        {
            return shiftsForStore.Count(s =>
                s.StartTime < endTime &&
                s.EndTime > startTime) >= requiredStaffCount;
        }
    }
}
