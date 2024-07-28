using System;
using System.Collections.Generic;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public interface IStaffDataAccess
    {
        List<Staff> GetStaff();
        Staff GetStaffById(int staffId);
        void AddStaff(Staff staff);
        void UpdateStaff(Staff staff);
        void DeleteStaff(int staffId);

        bool IsFullTimeAvailable(DateTime shiftDate, TimeSpan startTime, TimeSpan endTime);
        List<Staff> GetStaffByStoreId(int storeId); 
    }
}
