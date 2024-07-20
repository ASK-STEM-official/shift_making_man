using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public class StoreDataAccess : IStoreDataAccess
    {
        private readonly string connectionString = "server=localhost;database=19demo;user=root;password=;";

        public List<Store> GetStores()
        {
            List<Store> stores = new List<Store>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM store"; // 修正: テーブル名をstoresからstoreに変更
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

        public Store GetStoreById(int storeId)
        {
            Store store = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM store WHERE StoreID = @StoreID"; // 修正: テーブル名をstoresからstoreに変更
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreID", storeId);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        store = new Store
                        {
                            StoreID = rdr.GetInt32("StoreID"),
                            OpenTime = rdr.GetTimeSpan("OpenTime"),
                            CloseTime = rdr.GetTimeSpan("CloseTime"),
                            BusyTimeStart = rdr.GetTimeSpan("BusyTimeStart"),
                            BusyTimeEnd = rdr.GetTimeSpan("BusyTimeEnd"),
                            NormalStaffCount = rdr.GetInt32("NormalStaffCount"),
                            BusyStaffCount = rdr.GetInt32("BusyStaffCount")
                        };
                    }
                }
            }
            return store;
        }

        public void AddStore(Store store)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO store (OpenTime, CloseTime, BusyTimeStart, BusyTimeEnd, NormalStaffCount, BusyStaffCount) VALUES (@OpenTime, @CloseTime, @BusyTimeStart, @BusyTimeEnd, @NormalStaffCount, @BusyStaffCount)"; // 修正: テーブル名をstoresからstoreに変更
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@OpenTime", store.OpenTime);
                cmd.Parameters.AddWithValue("@CloseTime", store.CloseTime);
                cmd.Parameters.AddWithValue("@BusyTimeStart", store.BusyTimeStart);
                cmd.Parameters.AddWithValue("@BusyTimeEnd", store.BusyTimeEnd);
                cmd.Parameters.AddWithValue("@NormalStaffCount", store.NormalStaffCount);
                cmd.Parameters.AddWithValue("@BusyStaffCount", store.BusyStaffCount);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStore(Store store)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE store SET OpenTime = @OpenTime, CloseTime = @CloseTime, BusyTimeStart = @BusyTimeStart, BusyTimeEnd = @BusyTimeEnd, NormalStaffCount = @NormalStaffCount, BusyStaffCount = @BusyStaffCount WHERE StoreID = @StoreID"; // 修正: テーブル名をstoresからstoreに変更
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreID", store.StoreID);
                cmd.Parameters.AddWithValue("@OpenTime", store.OpenTime);
                cmd.Parameters.AddWithValue("@CloseTime", store.CloseTime);
                cmd.Parameters.AddWithValue("@BusyTimeStart", store.BusyTimeStart);
                cmd.Parameters.AddWithValue("@BusyTimeEnd", store.BusyTimeEnd);
                cmd.Parameters.AddWithValue("@NormalStaffCount", store.NormalStaffCount);
                cmd.Parameters.AddWithValue("@BusyStaffCount", store.BusyStaffCount);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStore(int storeId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM store WHERE StoreID = @StoreID"; // 修正: テーブル名をstoresからstoreに変更
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreID", storeId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
