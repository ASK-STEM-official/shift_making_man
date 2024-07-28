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

            var groupedShifts = shifts.GroupBy(shift => shift.StoreID);
            foreach (var group in groupedShifts)
            {
                var store = _storeDataAccess.GetStoreById(group.Key.Value);
                var shiftsForStore = group.ToList();

                foreach (var shift in shiftsForStore)
                {
                    if (IsShiftOverlapping(shift, shiftsForStore))
                    {
                        issues.Add($"シフトID: {shift.ShiftID} - シフトの時間が他のシフトと重複しています。");
                    }

                    if (shift.EndTime.Subtract(shift.StartTime).TotalHours >= 6 && shift.EndTime == TimeSpan.Zero)
                    {
                        issues.Add($"シフトID: {shift.ShiftID} - 6時間以上のシフトには1時間の休憩が必要です。");
                    }

                    if (shift.StartTime < store.OpenTime || shift.EndTime > store.CloseTime)
                    {
                        issues.Add($"シフトID: {shift.ShiftID} - 開店時間と閉店時間を超えたシフトがあります。");
                    }

                    if (IsInBusyTimePeriod(shift, store) &&
                        !IsStaffCountSufficient(shiftsForStore, store.BusyTimeStart, store.BusyTimeEnd, store.BusyStaffCount))
                    {
                        issues.Add($"シフトID: {shift.ShiftID} - 忙しい時間帯に必要なスタッフ数が確保されていません。");
                    }
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
