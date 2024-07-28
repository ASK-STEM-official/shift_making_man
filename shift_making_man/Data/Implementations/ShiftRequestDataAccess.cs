using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public class ShiftRequestDataAccess : IShiftRequestDataAccess
    {
        private readonly string connectionString = "server=localhost;database=19demo;user=root;password=;";

        public List<ShiftRequest> GetShiftRequests()
        {
            return ExecuteQuery("SELECT * FROM shiftrequests");
        }

        public ShiftRequest GetShiftRequestById(int requestId)
        {
            return ExecuteQuery($"SELECT * FROM shiftrequests WHERE RequestID = @RequestID", new MySqlParameter("@RequestID", requestId)).FirstOrDefault();
        }

        public void AddShiftRequest(ShiftRequest shiftRequest)
        {
            ExecuteNonQuery(
                "INSERT INTO shiftrequests (StoreID, StaffID, OriginalShiftID, RequestDate, RequestedShiftDate, RequestedStartTime, RequestedEndTime, Status) VALUES (@StoreID, @StaffID, @OriginalShiftID, @RequestDate, @RequestedShiftDate, @RequestedStartTime, @RequestedEndTime, @Status)",
                new MySqlParameter("@StoreID", shiftRequest.StoreID),
                new MySqlParameter("@StaffID", shiftRequest.StaffID.HasValue ? (object)shiftRequest.StaffID.Value : DBNull.Value),
                new MySqlParameter("@OriginalShiftID", shiftRequest.OriginalShiftID.HasValue ? (object)shiftRequest.OriginalShiftID.Value : DBNull.Value),
                new MySqlParameter("@RequestDate", shiftRequest.RequestDate),
                new MySqlParameter("@RequestedShiftDate", shiftRequest.RequestedShiftDate.HasValue ? (object)shiftRequest.RequestedShiftDate.Value : DBNull.Value),
                new MySqlParameter("@RequestedStartTime", shiftRequest.RequestedStartTime.HasValue ? (object)shiftRequest.RequestedStartTime.Value : DBNull.Value),
                new MySqlParameter("@RequestedEndTime", shiftRequest.RequestedEndTime.HasValue ? (object)shiftRequest.RequestedEndTime.Value : DBNull.Value),
                new MySqlParameter("@Status", shiftRequest.Status));
        }

        public void UpdateShiftRequest(ShiftRequest shiftRequest)
        {
            ExecuteNonQuery(
                "UPDATE shiftrequests SET StoreID = @StoreID, StaffID = @StaffID, OriginalShiftID = @OriginalShiftID, RequestDate = @RequestDate, RequestedShiftDate = @RequestedShiftDate, RequestedStartTime = @RequestedStartTime, RequestedEndTime = @RequestedEndTime, Status = @Status WHERE RequestID = @RequestID",
                new MySqlParameter("@RequestID", shiftRequest.RequestID),
                new MySqlParameter("@StoreID", shiftRequest.StoreID),
                new MySqlParameter("@StaffID", shiftRequest.StaffID.HasValue ? (object)shiftRequest.StaffID.Value : DBNull.Value),
                new MySqlParameter("@OriginalShiftID", shiftRequest.OriginalShiftID.HasValue ? (object)shiftRequest.OriginalShiftID.Value : DBNull.Value),
                new MySqlParameter("@RequestDate", shiftRequest.RequestDate),
                new MySqlParameter("@RequestedShiftDate", shiftRequest.RequestedShiftDate.HasValue ? (object)shiftRequest.RequestedShiftDate.Value : DBNull.Value),
                new MySqlParameter("@RequestedStartTime", shiftRequest.RequestedStartTime.HasValue ? (object)shiftRequest.RequestedStartTime.Value : DBNull.Value),
                new MySqlParameter("@RequestedEndTime", shiftRequest.RequestedEndTime.HasValue ? (object)shiftRequest.RequestedEndTime.Value : DBNull.Value),
                new MySqlParameter("@Status", shiftRequest.Status));
        }

        public void DeleteShiftRequest(int requestId)
        {
            ExecuteNonQuery("DELETE FROM shiftrequests WHERE RequestID = @RequestID", new MySqlParameter("@RequestID", requestId));
        }

        public int GetPendingShiftRequestCount()
        {
            return GetCount("SELECT COUNT(*) FROM shiftrequests WHERE Status = @Status", new MySqlParameter("@Status", 0));
        }

        public int GetCountByOriginalShiftID(int? originalShiftID)
        {
            return GetCount("SELECT COUNT(*) FROM shiftrequests WHERE OriginalShiftID = @OriginalShiftID", new MySqlParameter("@OriginalShiftID", (object)originalShiftID ?? DBNull.Value));
        }

        public int GetCountByOriginalShiftIDNotNull()
        {
            return GetCount("SELECT COUNT(*) FROM shiftrequests WHERE OriginalShiftID IS NOT NULL");
        }

        public (int NewRequestCount, int ChangeRequestCount) GetPendingShiftRequestCounts()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT " +
                             "(SELECT COUNT(*) FROM shiftrequests WHERE Status = @Status AND OriginalShiftID IS NULL) AS NewRequestCount, " +
                             "(SELECT COUNT(*) FROM shiftrequests WHERE Status = @Status AND OriginalShiftID IS NOT NULL) AS ChangeRequestCount";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", 0); // 0: 未処理のステータス
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return (rdr.GetInt32("NewRequestCount"), rdr.GetInt32("ChangeRequestCount"));
                    }
                }
            }
            return (0, 0);
        }

        public List<ShiftRequest> GetShiftRequestsByStoreId(int storeId)
        {
            return ExecuteQuery("SELECT * FROM shiftrequests WHERE StoreID = @StoreID", new MySqlParameter("@StoreID", storeId));
        }

        private List<ShiftRequest> ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            var shiftRequests = new List<ShiftRequest>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        shiftRequests.Add(new ShiftRequest
                        {
                            RequestID = rdr.GetInt32("RequestID"),
                            StoreID = rdr.GetInt32("StoreID"),
                            StaffID = rdr.IsDBNull(rdr.GetOrdinal("StaffID")) ? (int?)null : rdr.GetInt32("StaffID"),
                            OriginalShiftID = rdr.IsDBNull(rdr.GetOrdinal("OriginalShiftID")) ? (int?)null : rdr.GetInt32("OriginalShiftID"),
                            RequestDate = rdr.GetDateTime("RequestDate"),
                            Status = rdr.GetInt32("Status"),
                            RequestedShiftDate = rdr.IsDBNull(rdr.GetOrdinal("RequestedShiftDate")) ? (DateTime?)null : rdr.GetDateTime("RequestedShiftDate"),
                            RequestedStartTime = rdr.IsDBNull(rdr.GetOrdinal("RequestedStartTime")) ? (TimeSpan?)null : rdr.GetTimeSpan("RequestedStartTime"),
                            RequestedEndTime = rdr.IsDBNull(rdr.GetOrdinal("RequestedEndTime")) ? (TimeSpan?)null : rdr.GetTimeSpan("RequestedEndTime")
                        });
                    }
                }
            }
            return shiftRequests;
        }

        private void ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
            }
        }

        private int GetCount(string sql, params MySqlParameter[] parameters)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public List<ShiftRequest> GetPendingRequests()
        {
            return ExecuteQuery("SELECT * FROM shiftrequests WHERE Status = @Status", new MySqlParameter("@Status", 0));
        }

    }
}
