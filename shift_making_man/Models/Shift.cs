using System;

namespace shift_making_man.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public int? StaffID { get; set; }
        public DateTime ShiftDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Status { get; set; }
        public int? StoreID { get; set; }

        public Staff Staff { get; set; }
    }
}
