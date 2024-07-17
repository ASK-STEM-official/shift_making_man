using shift_making_man.Models;
using System.Collections.Generic;

namespace shift_making_man.Data
{
    public interface IDataAccess
    {
        List<Shift> GetShifts();
        List<Staff> GetStaff();
        List<Store> GetStores();
        void UpdateShift(Shift shift);
    }
}
