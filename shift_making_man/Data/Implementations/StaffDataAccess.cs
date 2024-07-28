using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using shift_making_man.Models;
using shift_making_man.Data;

public class StaffDataAccess : IStaffDataAccess
{
    private readonly string connectionString = "server=localhost;database=19demo;user=root;password=;";

    public List<Staff> GetStaff()
    {
        var staffs = new List<Staff>();
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "SELECT * FROM staff";
            using (var cmd = new MySqlCommand(sql, conn))
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    staffs.Add(new Staff
                    {
                        StaffID = rdr.GetInt32("StaffID"),
                        Username = rdr.GetString("Username"),
                        PasswordHash = rdr.GetString("PasswordHash"),
                        FullName = rdr.GetString("FullName"),
                        Email = rdr.GetString("Email"),
                        PhoneNumber = rdr.IsDBNull(rdr.GetOrdinal("PhoneNumber")) ? null : rdr.GetString("PhoneNumber"),
                        EmploymentType = rdr.IsDBNull(rdr.GetOrdinal("EmploymentType")) ? null : rdr.GetString("EmploymentType")
                    });
                }
            }
        }
        return staffs;
    }

    public Staff GetStaffById(int staffId)
    {
        Staff staff = null;
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "SELECT * FROM staff WHERE StaffID = @StaffID";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@StaffID", staffId);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        staff = new Staff
                        {
                            StaffID = rdr.GetInt32("StaffID"),
                            Username = rdr.GetString("Username"),
                            PasswordHash = rdr.GetString("PasswordHash"),
                            FullName = rdr.GetString("FullName"),
                            Email = rdr.GetString("Email"),
                            PhoneNumber = rdr.IsDBNull(rdr.GetOrdinal("PhoneNumber")) ? null : rdr.GetString("PhoneNumber"),
                            EmploymentType = rdr.IsDBNull(rdr.GetOrdinal("EmploymentType")) ? null : rdr.GetString("EmploymentType")
                        };
                    }
                }
            }
        }
        return staff;
    }

    public void AddStaff(Staff staff)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "INSERT INTO staff (Username, PasswordHash, FullName, Email, PhoneNumber, EmploymentType) VALUES (@Username, @PasswordHash, @FullName, @Email, @PhoneNumber, @EmploymentType)";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Username", staff.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", staff.PasswordHash);
                cmd.Parameters.AddWithValue("@FullName", staff.FullName);
                cmd.Parameters.AddWithValue("@Email", staff.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", (object)staff.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EmploymentType", (object)staff.EmploymentType ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void UpdateStaff(Staff staff)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "UPDATE staff SET Username = @Username, PasswordHash = @PasswordHash, FullName = @FullName, Email = @Email, PhoneNumber = @PhoneNumber, EmploymentType = @EmploymentType WHERE StaffID = @StaffID";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@StaffID", staff.StaffID);
                cmd.Parameters.AddWithValue("@Username", staff.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", staff.PasswordHash);
                cmd.Parameters.AddWithValue("@FullName", staff.FullName);
                cmd.Parameters.AddWithValue("@Email", staff.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", (object)staff.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EmploymentType", (object)staff.EmploymentType ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void DeleteStaff(int staffId)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "DELETE FROM staff WHERE StaffID = @StaffID";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@StaffID", staffId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public bool IsFullTimeAvailable(DateTime shiftDate, TimeSpan startTime, TimeSpan endTime)
    {
        bool isAvailable = false;
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = @"
                SELECT COUNT(*) 
                FROM staff 
                WHERE EmploymentType = 'FullTime' 
                AND StaffID NOT IN (
                    SELECT StaffID 
                    FROM shifts 
                    WHERE ShiftDate = @ShiftDate 
                    AND (
                        (StartTime < @EndTime AND EndTime > @StartTime)
                    )
                )";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ShiftDate", shiftDate.Date);
                cmd.Parameters.AddWithValue("@StartTime", startTime);
                cmd.Parameters.AddWithValue("@EndTime", endTime);

                int availableCount = Convert.ToInt32(cmd.ExecuteScalar());
                isAvailable = availableCount > 0;
            }
        }
        return isAvailable;
    }

    public List<Staff> GetStaffByStoreId(int storeId)
    {
        var staffs = new List<Staff>();
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = @"
                SELECT s.*
                FROM staff s
                JOIN shiftrequests sr ON s.StaffID = sr.StaffID
                WHERE sr.StoreID = @StoreID
                GROUP BY s.StaffID";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@StoreID", storeId);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        staffs.Add(new Staff
                        {
                            StaffID = rdr.GetInt32("StaffID"),
                            Username = rdr.GetString("Username"),
                            PasswordHash = rdr.GetString("PasswordHash"),
                            FullName = rdr.GetString("FullName"),
                            Email = rdr.GetString("Email"),
                            PhoneNumber = rdr.IsDBNull(rdr.GetOrdinal("PhoneNumber")) ? null : rdr.GetString("PhoneNumber"),
                            EmploymentType = rdr.IsDBNull(rdr.GetOrdinal("EmploymentType")) ? null : rdr.GetString("EmploymentType")
                        });
                    }
                }
            }
        }
        return staffs;
    }

    public List<Staff> GetStaffByEmploymentType(string employmentType)
    {
        var staffs = new List<Staff>();
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "SELECT * FROM staff WHERE EmploymentType = @EmploymentType";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@EmploymentType", employmentType);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        staffs.Add(new Staff
                        {
                            StaffID = rdr.GetInt32("StaffID"),
                            Username = rdr.GetString("Username"),
                            PasswordHash = rdr.GetString("PasswordHash"),
                            FullName = rdr.GetString("FullName"),
                            Email = rdr.GetString("Email"),
                            PhoneNumber = rdr.IsDBNull(rdr.GetOrdinal("PhoneNumber")) ? null : rdr.GetString("PhoneNumber"),
                            EmploymentType = rdr.IsDBNull(rdr.GetOrdinal("EmploymentType")) ? null : rdr.GetString("EmploymentType")
                        });
                    }
                }
            }
        }
        return staffs;
    }
}
