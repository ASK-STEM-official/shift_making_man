using System;

namespace shift_making_man.Models
{
    public class ShiftRequest
    {
        public int RequestID { get; set; }
        public int StoreID { get; set; }
        public int? StaffID { get; set; }
        public int? OriginalShiftID { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public DateTime? RequestedShiftDate { get; set; }
        public TimeSpan? RequestedStartTime { get; set; }
        public TimeSpan? RequestedEndTime { get; set; }
    }
}
