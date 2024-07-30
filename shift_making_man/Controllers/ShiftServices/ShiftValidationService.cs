//
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
