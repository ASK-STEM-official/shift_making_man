//
using System.Collections.Generic;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public interface IAdminDataAccess
    {
        List<Admin> GetAdmins();
        Admin GetAdminByUsername(string username);
        //void AddAdmin(Admin admin);
        //void UpdateAdmin(Admin admin);
        //void DeleteAdmin(int adminId);
    }
}
