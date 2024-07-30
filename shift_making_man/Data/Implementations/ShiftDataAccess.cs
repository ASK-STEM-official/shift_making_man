//
using System;
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
                            StartTime = rdr.GetTimeSpan(rdr.GetOrdinal("StartTime")),
                            EndTime = rdr.GetTimeSpan(rdr.GetOrdinal("EndTime")),
                            Status = rdr.GetInt32("Status"),
                            StoreID = rdr.IsDBNull(rdr.GetOrdinal("StoreID")) ? (int?)null : rdr.GetInt32("StoreID")
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
                            StartTime = rdr.GetTimeSpan(rdr.GetOrdinal("StartTime")),
                            EndTime = rdr.GetTimeSpan(rdr.GetOrdinal("EndTime")),
                            Status = rdr.GetInt32("Status"),
                            StoreID = rdr.IsDBNull(rdr.GetOrdinal("StoreID")) ? (int?)null : rdr.GetInt32("StoreID")
                        };
                    }
                }
            }
            return shift;
        }

        public void SaveShift(Shift shift)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql;
                if (shift.ShiftID > 0) // シフトIDが存在する場合は更新
                {
                    sql = "UPDATE shifts SET StaffID = @StaffID, ShiftDate = @ShiftDate, StartTime = @StartTime, EndTime = @EndTime, Status = @Status, StoreID = @StoreID WHERE ShiftID = @ShiftID";
                }
                else // シフトIDが存在しない場合は挿入
                {
                    sql = "INSERT INTO shifts (StaffID, ShiftDate, StartTime, EndTime, Status, StoreID) VALUES (@StaffID, @ShiftDate, @StartTime, @EndTime, @Status, @StoreID)";
                }
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ShiftID", shift.ShiftID);
                cmd.Parameters.AddWithValue("@StaffID", shift.StaffID.HasValue ? (object)shift.StaffID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ShiftDate", shift.ShiftDate);
                cmd.Parameters.AddWithValue("@StartTime", shift.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", shift.EndTime);
                cmd.Parameters.AddWithValue("@Status", shift.Status);
                cmd.Parameters.AddWithValue("@StoreID", shift.StoreID.HasValue ? (object)shift.StoreID.Value : DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveShiftList(List<Shift> shifts)
        {
            foreach (var shift in shifts)
            {
                SaveShift(shift);
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

        public List<Shift> GetShiftsForStaff(int staffId)
        {
            List<Shift> shifts = new List<Shift>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM shifts WHERE StaffID = @StaffID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StaffID", staffId);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        shifts.Add(new Shift
                        {
                            ShiftID = rdr.GetInt32("ShiftID"),
                            StaffID = rdr.IsDBNull(rdr.GetOrdinal("StaffID")) ? (int?)null : rdr.GetInt32("StaffID"),
                            ShiftDate = rdr.GetDateTime("ShiftDate"),
                            StartTime = rdr.GetTimeSpan(rdr.GetOrdinal("StartTime")),
                            EndTime = rdr.GetTimeSpan(rdr.GetOrdinal("EndTime")),
                            Status = rdr.GetInt32("Status"),
                            StoreID = rdr.IsDBNull(rdr.GetOrdinal("StoreID")) ? (int?)null : rdr.GetInt32("StoreID")
                        });
                    }
                }
            }
            return shifts;
        }

        public List<Shift> GetShiftsForStore(int storeId)
        {
            List<Shift> shifts = new List<Shift>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM shifts WHERE StoreID = @StoreID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreID", storeId);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        shifts.Add(new Shift
                        {
                            ShiftID = rdr.GetInt32("ShiftID"),
                            StaffID = rdr.IsDBNull(rdr.GetOrdinal("StaffID")) ? (int?)null : rdr.GetInt32("StaffID"),
                            ShiftDate = rdr.GetDateTime("ShiftDate"),
                            StartTime = rdr.GetTimeSpan(rdr.GetOrdinal("StartTime")),
                            EndTime = rdr.GetTimeSpan(rdr.GetOrdinal("EndTime")),
                            Status = rdr.GetInt32("Status"),
                            StoreID = rdr.IsDBNull(rdr.GetOrdinal("StoreID")) ? (int?)null : rdr.GetInt32("StoreID")
                        });
                    }
                }
            }
            return shifts;
        }

        public string ConnectionString { get; private set; }

        public ShiftDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<ShiftRequest> GetShiftRequestsByStoreId(int storeId)
        {
            List<ShiftRequest> shiftRequests = new List<ShiftRequest>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM shiftrequests WHERE StoreID = @StoreID AND Status = 1";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreID", storeId);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        shiftRequests.Add(new ShiftRequest
                        {
                            RequestID = rdr.GetInt32("RequestID"),
                            StaffID = rdr.GetInt32("StaffID"),
                            StoreID = rdr.GetInt32("StoreID"),
                            RequestedShiftDate = rdr.GetDateTime("RequestedShiftDate"),
                            RequestedStartTime = rdr.GetTimeSpan("RequestedStartTime"),
                            RequestedEndTime = rdr.GetTimeSpan("RequestedEndTime"),
                            Status = rdr.GetInt32("Status"),
                            OriginalShiftID = rdr.IsDBNull(rdr.GetOrdinal("OriginalShiftID")) ? (int?)null : rdr.GetInt32("OriginalShiftID")
                        });
                    }
                }
            }
            return shiftRequests;
        }

        public void UpdateShiftRequest(ShiftRequest request)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE shiftrequests SET Status = @Status WHERE RequestID = @RequestID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", request.Status);
                cmd.Parameters.AddWithValue("@RequestID", request.RequestID);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
