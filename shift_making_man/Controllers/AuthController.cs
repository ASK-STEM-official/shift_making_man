using shift_making_man.Data;
using shift_making_man.Models;

namespace shift_making_man.Controllers
{
    public class AuthController
    {
        private readonly IAdminDataAccess _dataAccess;

        public AuthController(IAdminDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Admin Login(string username, string password)
        {
            Admin admin = _dataAccess.GetAdminByUsername(username);
            if (admin != null && BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                return admin;
            }
            return null;
        }
    }
}
