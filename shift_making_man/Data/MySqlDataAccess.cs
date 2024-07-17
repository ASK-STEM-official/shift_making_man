using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public class MySqlDataAccess : IDataAccess
    {
        private readonly string connectionString = "server=localhost;database=19demo;user=root;password=;";

        public List<Shift> GetShifts()
        {
            List<Shift> shifts = new List<Shift>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM shifts";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        shifts.Add(new Shift
                        {
                            ShiftID = rdr.GetInt32("ShiftID"),
                            StaffID = rdr.IsDBNull("StaffID") ? (int?)null : rdr.GetInt32("StaffID"),
                            ShiftDate = rdr.GetDateTime("ShiftDate"),
                            StartTime = rdr.GetTimeSpan("StartTime"),
                            EndTime = rdr.GetTimeSpan("EndTime"),
                            Status = rdr.GetInt32("Status")
                        });
                    }
                }
            }
            return shifts;
        }

        public List<Staff> GetStaff()
        {
            List<Staff> staff = new List<Staff>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM staff";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        staff.Add(new Staff
                        {
                            StaffID = rdr.GetInt32("StaffID"),
                            Username = rdr.GetString("Username"),
                            PasswordHash = rdr.GetString("PasswordHash"),
                            FullName = rdr.GetString("FullName"),
                            Email = rdr.GetString("Email"),
                            PhoneNumber = rdr.GetString("PhoneNumber"),
                            EmploymentType = rdr.GetString("EmploymentType"),
                            StoreID = rdr.IsDBNull("StoreID") ? (int?)null : rdr.GetInt32("StoreID")
                        });
                    }
                }
            }
            return staff;
        }

        public List<Store> GetStores()
        {
            List<Store> stores = new List<Store>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM store";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        stores.Add(new Store
                        {
                            StoreID = rdr.GetInt32("StoreID"),
                            OpenTime = rdr.GetTimeSpan("OpenTime"),
                            CloseTime = rdr.GetTimeSpan("CloseTime"),
                            BusyTimeStart = rdr.GetTimeSpan("BusyTimeStart"),
                            BusyTimeEnd = rdr.GetTimeSpan("BusyTimeEnd"),
                            NormalStaffCount = rdr.GetInt32("NormalStaffCount"),
                            BusyStaffCount = rdr.GetInt32("BusyStaffCount")
                        });
                    }
                }
            }
            return stores;
        }

        public void UpdateShift(Shift shift)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE shifts SET StaffID = @StaffID, ShiftDate = @ShiftDate, StartTime = @StartTime, EndTime = @EndTime, Status = @Status WHERE ShiftID = @ShiftID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ShiftID", shift.ShiftID);
                cmd.Parameters.AddWithValue("@StaffID", shift.StaffID);
                cmd.Parameters.AddWithValue("@ShiftDate", shift.ShiftDate);
                cmd.Parameters.AddWithValue("@StartTime", shift.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", shift.EndTime);
                cmd.Parameters.AddWithValue("@Status", shift.Status);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
