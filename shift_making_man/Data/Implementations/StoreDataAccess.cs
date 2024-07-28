﻿using System;
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
            try
            {
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
                                OpenTime = rdr.GetTimeSpan(rdr.GetOrdinal("OpenTime")),
                                CloseTime = rdr.GetTimeSpan(rdr.GetOrdinal("CloseTime")),
                                BusyTimeStart = rdr.GetTimeSpan(rdr.GetOrdinal("BusyTimeStart")),
                                BusyTimeEnd = rdr.GetTimeSpan(rdr.GetOrdinal("BusyTimeEnd")),
                                NormalStaffCount = rdr.GetInt32("NormalStaffCount"),
                                BusyStaffCount = rdr.GetInt32("BusyStaffCount")
                            });
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQLエラー: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"TimeSpan変換エラー: {ex.Message}");
            }
            return stores;
        }

        public Store GetStoreById(int storeId)
        {
            Store store = null;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM store WHERE StoreID = @StoreID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@StoreID", storeId);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            store = new Store
                            {
                                StoreID = rdr.GetInt32("StoreID"),
                                OpenTime = rdr.GetTimeSpan(rdr.GetOrdinal("OpenTime")),
                                CloseTime = rdr.GetTimeSpan(rdr.GetOrdinal("CloseTime")),
                                BusyTimeStart = rdr.GetTimeSpan(rdr.GetOrdinal("BusyTimeStart")),
                                BusyTimeEnd = rdr.GetTimeSpan(rdr.GetOrdinal("BusyTimeEnd")),
                                NormalStaffCount = rdr.GetInt32("NormalStaffCount"),
                                BusyStaffCount = rdr.GetInt32("BusyStaffCount")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQLエラー: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"TimeSpan変換エラー: {ex.Message}");
            }
            return store;
        }

        public void AddStore(Store store)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO store (OpenTime, CloseTime, BusyTimeStart, BusyTimeEnd, NormalStaffCount, BusyStaffCount) VALUES (@OpenTime, @CloseTime, @BusyTimeStart, @BusyTimeEnd, @NormalStaffCount, @BusyStaffCount)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@OpenTime", store.OpenTime.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@CloseTime", store.CloseTime.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@BusyTimeStart", store.BusyTimeStart.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@BusyTimeEnd", store.BusyTimeEnd.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@NormalStaffCount", store.NormalStaffCount);
                    cmd.Parameters.AddWithValue("@BusyStaffCount", store.BusyStaffCount);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQLエラー: {ex.Message}");
            }
        }

        public void UpdateStore(Store store)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE store SET OpenTime = @OpenTime, CloseTime = @CloseTime, BusyTimeStart = @BusyTimeStart, BusyTimeEnd = @BusyTimeEnd, NormalStaffCount = @NormalStaffCount, BusyStaffCount = @BusyStaffCount WHERE StoreID = @StoreID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@StoreID", store.StoreID);
                    cmd.Parameters.AddWithValue("@OpenTime", store.OpenTime.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@CloseTime", store.CloseTime.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@BusyTimeStart", store.BusyTimeStart.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@BusyTimeEnd", store.BusyTimeEnd.ToString(@"hh\:mm\:ss"));
                    cmd.Parameters.AddWithValue("@NormalStaffCount", store.NormalStaffCount);
                    cmd.Parameters.AddWithValue("@BusyStaffCount", store.BusyStaffCount);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQLエラー: {ex.Message}");
            }
        }

        public void DeleteStore(int storeId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM store WHERE StoreID = @StoreID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@StoreID", storeId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQLエラー: {ex.Message}");
            }
        }

        public TimeSpan GetStoreOpenTime(int storeId)
        {
            TimeSpan openTime = default;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT OpenTime FROM store WHERE StoreID = @StoreID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@StoreID", storeId);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            openTime = rdr.GetTimeSpan(rdr.GetOrdinal("OpenTime"));
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQLエラー: {ex.Message}");
            }
            return openTime;
        }

        public TimeSpan GetStoreCloseTime(int storeId)
        {
            TimeSpan closeTime = default;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT CloseTime FROM store WHERE StoreID = @StoreID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@StoreID", storeId);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            closeTime = rdr.GetTimeSpan(rdr.GetOrdinal("CloseTime"));
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQLエラー: {ex.Message}");
            }
            return closeTime;
        }
    }
}