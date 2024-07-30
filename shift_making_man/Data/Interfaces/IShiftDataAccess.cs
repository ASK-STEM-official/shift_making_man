//
using shift_making_man.Models;
using System.Collections.Generic;

public interface IShiftDataAccess
{
    List<Shift> GetShifts();
    Shift GetShiftById(int shiftId);
    void DeleteShift(int shiftId);
    //List<Shift> GetShiftsForStaff(int staffId);
    //List<Shift> GetShiftsForStore(int storeId);
    void SaveShift(Shift shift);
    //string ConnectionString { get; }
    void SaveShiftList(List<Shift> shifts);
    //List<ShiftRequest> GetShiftRequestsByStoreId(int storeId);
    //void UpdateShiftRequest(ShiftRequest request);
}
