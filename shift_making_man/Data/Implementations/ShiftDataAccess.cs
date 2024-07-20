using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public class ShiftDataAccess : IShiftDataAccess
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
                            StaffID = rdr.IsDBNull(rdr.GetOrdinal("StaffID")) ? (int?)null : rdr.GetInt32("StaffID"),
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

        public Shift GetShiftById(int shiftId)
        {
            Shift shift = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM shifts WHERE ShiftID = @ShiftID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ShiftID", shiftId);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        shift = new Shift
                        {
                            ShiftID = rdr.GetInt32("ShiftID"),
                            StaffID = rdr.IsDBNull(rdr.GetOrdinal("StaffID")) ? (int?)null : rdr.GetInt32("StaffID"),
                            ShiftDate = rdr.GetDateTime("ShiftDate"),
                            StartTime = rdr.GetTimeSpan("StartTime"),
                            EndTime = rdr.GetTimeSpan("EndTime"),
                            Status = rdr.GetInt32("Status")
                        };
                    }
                }
            }
            return shift;
        }

        public void AddShift(Shift shift)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO shifts (StaffID, ShiftDate, StartTime, EndTime, Status) VALUES (@StaffID, @ShiftDate, @StartTime, @EndTime, @Status)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StaffID", shift.StaffID);
                cmd.Parameters.AddWithValue("@ShiftDate", shift.ShiftDate);
                cmd.Parameters.AddWithValue("@StartTime", shift.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", shift.EndTime);
                cmd.Parameters.AddWithValue("@Status", shift.Status);
                cmd.ExecuteNonQuery();
            }
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

        public void DeleteShift(int shiftId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM shifts WHERE ShiftID = @ShiftID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ShiftID", shiftId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
