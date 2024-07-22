using MySql.Data.MySqlClient;
using shift_making_man.Data;
using shift_making_man.Models;
using System.Collections.Generic;
using System;

public class StaffDataAccess : IStaffDataAccess
{
    private readonly string connectionString = "server=localhost;database=19demo;user=root;password=;";

    public List<Staff> GetStaffs()
    {
        List<Staff> staffs = new List<Staff>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string sql = "SELECT * FROM staff"; // クエリを修正
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
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
                        PhoneNumber = rdr.IsDBNull(rdr.GetOrdinal("PhoneNumber")) ? null : rdr.GetString("PhoneNumber"), // Nullチェック追加
                        EmploymentType = rdr.IsDBNull(rdr.GetOrdinal("EmploymentType")) ? null : rdr.GetString("EmploymentType") // Nullチェック追加
                    });
                }
            }
        }
        return staffs;
    }

    public Staff GetStaffById(int staffId)
    {
        Staff staff = null;
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string sql = "SELECT * FROM staff WHERE StaffID = @StaffID"; // クエリを修正
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StaffID", staffId);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
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
                        PhoneNumber = rdr.IsDBNull(rdr.GetOrdinal("PhoneNumber")) ? null : rdr.GetString("PhoneNumber"), // Nullチェック追加
                        EmploymentType = rdr.IsDBNull(rdr.GetOrdinal("EmploymentType")) ? null : rdr.GetString("EmploymentType") // Nullチェック追加
                    };
                }
            }
        }
        return staff;
    }

    public void AddStaff(Staff staff)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string sql = "INSERT INTO staff (Username, PasswordHash, FullName, Email, PhoneNumber, EmploymentType) VALUES (@Username, @PasswordHash, @FullName, @Email, @PhoneNumber, @EmploymentType)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", staff.Username);
            cmd.Parameters.AddWithValue("@PasswordHash", staff.PasswordHash);
            cmd.Parameters.AddWithValue("@FullName", staff.FullName);
            cmd.Parameters.AddWithValue("@Email", staff.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", (object)staff.PhoneNumber ?? DBNull.Value); // Nullチェック追加
            cmd.Parameters.AddWithValue("@EmploymentType", (object)staff.EmploymentType ?? DBNull.Value); // Nullチェック追加
            cmd.ExecuteNonQuery();
        }
    }

    public void UpdateStaff(Staff staff)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string sql = "UPDATE staff SET Username = @Username, PasswordHash = @PasswordHash, FullName = @FullName, Email = @Email, PhoneNumber = @PhoneNumber, EmploymentType = @EmploymentType WHERE StaffID = @StaffID";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StaffID", staff.StaffID);
            cmd.Parameters.AddWithValue("@Username", staff.Username);
            cmd.Parameters.AddWithValue("@PasswordHash", staff.PasswordHash);
            cmd.Parameters.AddWithValue("@FullName", staff.FullName);
            cmd.Parameters.AddWithValue("@Email", staff.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", (object)staff.PhoneNumber ?? DBNull.Value); // Nullチェック追加
            cmd.Parameters.AddWithValue("@EmploymentType", (object)staff.EmploymentType ?? DBNull.Value); // Nullチェック追加
            cmd.ExecuteNonQuery();
        }
    }

    public void DeleteStaff(int staffId)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string sql = "DELETE FROM staff WHERE StaffID = @StaffID";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StaffID", staffId);
            cmd.ExecuteNonQuery();
        }
    }

    public List<Staff> GetStaff()
    {
        return GetStaffs(); // インターフェースのメソッドを実装
    }
}
