//
using System.Collections.Generic;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public interface IAttendanceDataAccess
    {
        List<Attendance> GetAttendances();
        //Attendance GetAttendanceById(int attendanceId);
        //void AddAttendance(Attendance attendance);
        //void UpdateAttendance(Attendance attendance);
        //void DeleteAttendance(int attendanceId);
    }
}
