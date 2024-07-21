using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public class ShiftRequestDataAccess : IShiftRequestDataAccess
    {
        private readonly string connectionString = "server=localhost;database=19demo;user=root;password=;";

        public List<ShiftRequest> GetShiftRequests()
        {
            List<ShiftRequest> shiftRequests = new List<ShiftRequest>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM shiftrequests";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        shiftRequests.Add(new ShiftRequest
                        {
                            RequestID = rdr.GetInt32("RequestID"),
                            StaffID = rdr.IsDBNull(rdr.GetOrdinal("StaffID")) ? (int?)null : rdr.GetInt32("StaffID"),
                            OriginalShiftID = rdr.IsDBNull(rdr.GetOrdinal("OriginalShiftID")) ? (int?)null : rdr.GetInt32("OriginalShiftID"),
                            RequestedShiftID = rdr.IsDBNull(rdr.GetOrdinal("RequestedShiftID")) ? (int?)null : rdr.GetInt32("RequestedShiftID"),
                            RequestDate = rdr.GetDateTime("RequestDate"),
                            RequestedShiftDate = rdr.IsDBNull(rdr.GetOrdinal("RequestedShiftDate")) ? (DateTime?)null : rdr.GetDateTime("RequestedShiftDate"),
                            RequestedStartTime = rdr.IsDBNull(rdr.GetOrdinal("RequestedStartTime")) ? (TimeSpan?)null : rdr.GetTimeSpan("RequestedStartTime"),
                            RequestedEndTime = rdr.IsDBNull(rdr.GetOrdinal("RequestedEndTime")) ? (TimeSpan?)null : rdr.GetTimeSpan("RequestedEndTime"),
                            Status = rdr.GetInt32("Status")
                        });
                    }
                }
            }
            return shiftRequests;
        }

        public ShiftRequest GetShiftRequestById(int requestId)
        {
            ShiftRequest shiftRequest = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM shiftrequests WHERE RequestID = @RequestID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RequestID", requestId);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        shiftRequest = new ShiftRequest
                        {
                            RequestID = rdr.GetInt32("RequestID"),
                            StaffID = rdr.IsDBNull(rdr.GetOrdinal("StaffID")) ? (int?)null : rdr.GetInt32("StaffID"),
                            OriginalShiftID = rdr.IsDBNull(rdr.GetOrdinal("OriginalShiftID")) ? (int?)null : rdr.GetInt32("OriginalShiftID"),
                            RequestedShiftID = rdr.IsDBNull(rdr.GetOrdinal("RequestedShiftID")) ? (int?)null : rdr.GetInt32("RequestedShiftID"),
                            RequestDate = rdr.GetDateTime("RequestDate"),
                            RequestedShiftDate = rdr.IsDBNull(rdr.GetOrdinal("RequestedShiftDate")) ? (DateTime?)null : rdr.GetDateTime("RequestedShiftDate"),
                            RequestedStartTime = rdr.IsDBNull(rdr.GetOrdinal("RequestedStartTime")) ? (TimeSpan?)null : rdr.GetTimeSpan("RequestedStartTime"),
                            RequestedEndTime = rdr.IsDBNull(rdr.GetOrdinal("RequestedEndTime")) ? (TimeSpan?)null : rdr.GetTimeSpan("RequestedEndTime"),
                            Status = rdr.GetInt32("Status")
                        };
                    }
                }
            }
            return shiftRequest;
        }

        public void AddShiftRequest(ShiftRequest shiftRequest)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO shiftrequests (StaffID, OriginalShiftID, RequestedShiftID, RequestDate, RequestedShiftDate, RequestedStartTime, RequestedEndTime, Status) VALUES (@StaffID, @OriginalShiftID, @RequestedShiftID, @RequestDate, @RequestedShiftDate, @RequestedStartTime, @RequestedEndTime, @Status)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StaffID", shiftRequest.StaffID.HasValue ? (object)shiftRequest.StaffID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@OriginalShiftID", shiftRequest.OriginalShiftID.HasValue ? (object)shiftRequest.OriginalShiftID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@RequestedShiftID", shiftRequest.RequestedShiftID.HasValue ? (object)shiftRequest.RequestedShiftID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@RequestDate", shiftRequest.RequestDate);
                cmd.Parameters.AddWithValue("@RequestedShiftDate", shiftRequest.RequestedShiftDate.HasValue ? (object)shiftRequest.RequestedShiftDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@RequestedStartTime", shiftRequest.RequestedStartTime.HasValue ? (object)shiftRequest.RequestedStartTime.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@RequestedEndTime", shiftRequest.RequestedEndTime.HasValue ? (object)shiftRequest.RequestedEndTime.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", shiftRequest.Status);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateShiftRequest(ShiftRequest shiftRequest)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE shiftrequests SET StaffID = @StaffID, OriginalShiftID = @OriginalShiftID, RequestedShiftID = @RequestedShiftID, RequestDate = @RequestDate, RequestedShiftDate = @RequestedShiftDate, RequestedStartTime = @RequestedStartTime, RequestedEndTime = @RequestedEndTime, Status = @Status WHERE RequestID = @RequestID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RequestID", shiftRequest.RequestID);
                cmd.Parameters.AddWithValue("@StaffID", shiftRequest.StaffID.HasValue ? (object)shiftRequest.StaffID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@OriginalShiftID", shiftRequest.OriginalShiftID.HasValue ? (object)shiftRequest.OriginalShiftID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@RequestedShiftID", shiftRequest.RequestedShiftID.HasValue ? (object)shiftRequest.RequestedShiftID.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@RequestDate", shiftRequest.RequestDate);
                cmd.Parameters.AddWithValue("@RequestedShiftDate", shiftRequest.RequestedShiftDate.HasValue ? (object)shiftRequest.RequestedShiftDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@RequestedStartTime", shiftRequest.RequestedStartTime.HasValue ? (object)shiftRequest.RequestedStartTime.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@RequestedEndTime", shiftRequest.RequestedEndTime.HasValue ? (object)shiftRequest.RequestedEndTime.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", shiftRequest.Status);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteShiftRequest(int requestId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM shiftrequests WHERE RequestID = @RequestID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RequestID", requestId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
