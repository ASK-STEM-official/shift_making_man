using MySql.Data.MySqlClient;
using shift_making_man.Models;
using System;
using System.Collections.Generic;
using shift_making_man.Data;

namespace shift_making_man.Data
{
    public class MySqlDataAccess : IDataAccess
    {
        private readonly string _connectionString;

        public MySqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM staff", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = reader.GetInt32("StaffID"),
                            Username = reader.GetString("Username"),
                            PasswordHash = reader.GetString("PasswordHash"),
                            Name = reader.GetString("FullName"),
                            Email = reader.GetString("Email"),
                            PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString("PhoneNumber"),
                            EmploymentType = reader.GetString("EmploymentType"),
                            StoreId = reader.IsDBNull(reader.GetOrdinal("StoreID")) ? (int?)null : reader.GetInt32("StoreID")
                        });
                    }
                }
            }
            return employees;
        }

        public List<Shift> GetAllShifts()
        {
            var shifts = new List<Shift>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM shifts", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shifts.Add(new Shift
                        {
                            Id = reader.GetInt32("ShiftID"),
                            EmployeeId = reader.GetInt32("StaffID"),
                            Date = reader.GetDateTime("ShiftDate"),
                            StartTime = reader.GetDateTime("ShiftDate").Add(reader.GetTimeSpan("StartTime")),
                            EndTime = reader.GetDateTime("ShiftDate").Add(reader.GetTimeSpan("EndTime")),
                            Status = reader.GetInt32("Status")
                        });
                    }
                }
            }
            return shifts;
        }

        public List<ShiftRequest> GetAllShiftRequests()
        {
            var requests = new List<ShiftRequest>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM shiftrequests", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requests.Add(new ShiftRequest
                        {
                            RequestID = reader.GetInt32("RequestID"),
                            StaffID = reader.GetInt32("StaffID"),
                            OriginalShiftID = reader.IsDBNull(reader.GetOrdinal("OriginalShiftID")) ? (int?)null : reader.GetInt32("OriginalShiftID"),
                            RequestedShiftID = reader.IsDBNull(reader.GetOrdinal("RequestedShiftID")) ? (int?)null : reader.GetInt32("RequestedShiftID"),
                            RequestDate = reader.GetDateTime("RequestDate"),
                            Status = reader.GetInt32("Status")
                        });
                    }
                }
            }
            return requests;
        }

        public List<Attendance> GetAllAttendances()
        {
            var attendances = new List<Attendance>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM attendance", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        attendances.Add(new Attendance
                        {
                            AttendanceID = reader.GetInt32("AttendanceID"),
                            StaffID = reader.GetInt32("StaffID"),
                            ShiftID = reader.IsDBNull(reader.GetOrdinal("ShiftID")) ? (int?)null : reader.GetInt32("ShiftID"),
                            CheckInTime = reader.GetDateTime("CheckInTime"),
                            CheckOutTime = reader.IsDBNull(reader.GetOrdinal("CheckOutTime")) ? (DateTime?)null : reader.GetDateTime("CheckOutTime")
                        });
                    }
                }
            }
            return attendances;
        }

        public List<Admin> GetAllAdmins()
        {
            var admins = new List<Admin>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM admins", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admins.Add(new Admin
                        {
                            AdminID = reader.GetInt32("AdminID"),
                            Username = reader.GetString("Username"),
                            PasswordHash = reader.GetString("PasswordHash"),
                            FullName = reader.GetString("FullName"),
                            Email = reader.GetString("Email")
                        });
                    }
                }
            }
            return admins;
        }

        public void SaveShiftRequest(ShiftRequest request)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "INSERT INTO shiftrequests (StaffID, OriginalShiftID, RequestedShiftID, RequestDate, Status) " +
                    "VALUES (@StaffID, @OriginalShiftID, @RequestedShiftID, @RequestDate, @Status)", connection);

                command.Parameters.AddWithValue("@StaffID", request.StaffID);
                command.Parameters.AddWithValue("@OriginalShiftID", request.OriginalShiftID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RequestedShiftID", request.RequestedShiftID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RequestDate", request.RequestDate);
                command.Parameters.AddWithValue("@Status", request.Status);

                command.ExecuteNonQuery();
            }
        }

        public void SaveAttendance(Attendance attendance)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "INSERT INTO attendance (StaffID, ShiftID, CheckInTime, CheckOutTime) " +
                    "VALUES (@StaffID, @ShiftID, @CheckInTime, @CheckOutTime)", connection);

                command.Parameters.AddWithValue("@StaffID", attendance.StaffID);
                command.Parameters.AddWithValue("@ShiftID", attendance.ShiftID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CheckInTime", attendance.CheckInTime);
                command.Parameters.AddWithValue("@CheckOutTime", attendance.CheckOutTime ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateShiftStatus(int shiftId, int status)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "UPDATE shifts SET Status = @Status WHERE ShiftID = @ShiftID", connection);

                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@ShiftID", shiftId);

                command.ExecuteNonQuery();
            }
        }
    }
}