using System;

namespace shift_making_man.Models
{
    public class Attendance
    {
        public int AttendanceID { get; set; }
        public int? StaffID { get; set; } 
        public int? ShiftID { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
    }
}
