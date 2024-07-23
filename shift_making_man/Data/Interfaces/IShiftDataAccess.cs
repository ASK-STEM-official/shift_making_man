using System.Collections.Generic;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public interface IShiftDataAccess
    {
        List<Shift> GetShifts();
        Shift GetShiftById(int shiftId);
        void AddShift(Shift shift);
        void UpdateShift(Shift shift);
        void DeleteShift(int shiftId);
        List<Shift> GetShiftsForStaff(int staffId); // 追加
    }
}
