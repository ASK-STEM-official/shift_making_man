using System;

namespace shift_making_man.Models
{
    public class Store
    {
        public int StoreID { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public TimeSpan BusyTimeStart { get; set; }
        public TimeSpan BusyTimeEnd { get; set; }
        public int NormalStaffCount { get; set; }
        public int BusyStaffCount { get; set; }
    }
}
