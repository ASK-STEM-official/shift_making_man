using System.Collections.Generic;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public interface IDataAccess
    {
        List<Employee> GetAllEmployees();
        List<Shift> GetAllShifts();
        List<ShiftRequest> GetAllShiftRequests();
        List<Attendance> GetAllAttendances();
        List<Admin> GetAllAdmins();
        void SaveShiftRequest(ShiftRequest request);
        void SaveAttendance(Attendance attendance);
        void UpdateShiftStatus(int shiftId, int status);
    }
}