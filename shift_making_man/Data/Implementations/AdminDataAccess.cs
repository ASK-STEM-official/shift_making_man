using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public class AdminDataAccess : IAdminDataAccess
    {
        private readonly string connectionString = "server=localhost;database=19demo;user=root;password=;";

        public List<Admin> GetAdmins()
        {
            List<Admin> admins = new List<Admin>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM admins";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        admins.Add(new Admin
                        {
                            AdminID = rdr.GetInt32("AdminID"),
                            Username = rdr.GetString("Username"),
                            PasswordHash = rdr.GetString("PasswordHash"),
                            FullName = rdr.GetString("FullName"),
                            Email = rdr.GetString("Email")
                        });
                    }
                }
            }
            return admins;
        }

        public Admin GetAdminByUsername(string username)
        {
            Admin admin = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM admins WHERE Username = @Username";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            admin = new Admin
                            {
                                AdminID = reader.GetInt32("AdminID"),
                                Username = reader.GetString("Username"),
                                PasswordHash = reader.GetString("PasswordHash"),
                                FullName = reader.GetString("FullName"),
                                Email = reader.GetString("Email")
                            };
                        }
                    }
                }
            }
            return admin;
        }

        public void AddAdmin(Admin admin)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO admins (Username, PasswordHash, FullName, Email) VALUES (@Username, @PasswordHash, @FullName, @Email)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Username", admin.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", admin.PasswordHash);
                cmd.Parameters.AddWithValue("@FullName", admin.FullName);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAdmin(Admin admin)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE admins SET Username = @Username, PasswordHash = @PasswordHash, FullName = @FullName, Email = @Email WHERE AdminID = @AdminID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AdminID", admin.AdminID);
                cmd.Parameters.AddWithValue("@Username", admin.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", admin.PasswordHash);
                cmd.Parameters.AddWithValue("@FullName", admin.FullName);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAdmin(int adminId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM admins WHERE AdminID = @AdminID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AdminID", adminId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
