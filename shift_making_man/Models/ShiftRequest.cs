using System;

namespace shift_making_man.Models
{
    public class ShiftRequest
    {
        public int RequestID { get; set; }
        public int? StaffID { get; set; }
        public int? OriginalShiftID { get; set; }
        public int? RequestedShiftID { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? RequestedShiftDate { get; set; } // 追加
        public TimeSpan? RequestedStartTime { get; set; } // 追加
        public TimeSpan? RequestedEndTime { get; set; } // 追加
        public int Status { get; set; } // 修正
    }
}
