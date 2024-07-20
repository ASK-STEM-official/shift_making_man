using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public class AttendanceDataAccess : IAttendanceDataAccess
    {
        private readonly string connectionString = "server=localhost;database=19demo;user=root;password=;";

        public List<Attendance> GetAttendances()
        {
            List<Attendance> attendances = new List<Attendance>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM attendance";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        attendances.Add(new Attendance
                        {
                            AttendanceID = rdr.GetInt32("AttendanceID"),
                            StaffID = rdr.IsDBNull(rdr.GetOrdinal("StaffID")) ? (int?)null : rdr.GetInt32("StaffID"),
                            ShiftID = rdr.IsDBNull(rdr.GetOrdinal("ShiftID")) ? (int?)null : rdr.GetInt32("ShiftID"),
                            CheckInTime = rdr.GetDateTime("CheckInTime"),
                            CheckOutTime = rdr.IsDBNull(rdr.GetOrdinal("CheckOutTime")) ? (DateTime?)null : rdr.GetDateTime("CheckOutTime")
                        });
                    }
                }
            }
            return attendances;
        }

        public Attendance GetAttendanceById(int attendanceId)
        {
            Attendance attendance = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM attendance WHERE AttendanceID = @AttendanceID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceId);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        attendance = new Attendance
                        {
                            AttendanceID = rdr.GetInt32("AttendanceID"),
                            StaffID = rdr.IsDBNull(rdr.GetOrdinal("StaffID")) ? (int?)null : rdr.GetInt32("StaffID"),
                            ShiftID = rdr.IsDBNull(rdr.GetOrdinal("ShiftID")) ? (int?)null : rdr.GetInt32("ShiftID"),
                            CheckInTime = rdr.GetDateTime("CheckInTime"),
                            CheckOutTime = rdr.IsDBNull(rdr.GetOrdinal("CheckOutTime")) ? (DateTime?)null : rdr.GetDateTime("CheckOutTime")
                        };
                    }
                }
            }
            return attendance;
        }

        public void AddAttendance(Attendance attendance)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO attendance (StaffID, ShiftID, CheckInTime, CheckOutTime) VALUES (@StaffID, @ShiftID, @CheckInTime, @CheckOutTime)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StaffID", attendance.StaffID);
                cmd.Parameters.AddWithValue("@ShiftID", attendance.ShiftID);
                cmd.Parameters.AddWithValue("@CheckInTime", attendance.CheckInTime);
                cmd.Parameters.AddWithValue("@CheckOutTime", attendance.CheckOutTime);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAttendance(Attendance attendance)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE attendance SET StaffID = @StaffID, ShiftID = @ShiftID, CheckInTime = @CheckInTime, CheckOutTime = @CheckOutTime WHERE AttendanceID = @AttendanceID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AttendanceID", attendance.AttendanceID);
                cmd.Parameters.AddWithValue("@StaffID", attendance.StaffID);
                cmd.Parameters.AddWithValue("@ShiftID", attendance.ShiftID);
                cmd.Parameters.AddWithValue("@CheckInTime", attendance.CheckInTime);
                cmd.Parameters.AddWithValue("@CheckOutTime", attendance.CheckOutTime);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAttendance(int attendanceId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM attendance WHERE AttendanceID = @AttendanceID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
