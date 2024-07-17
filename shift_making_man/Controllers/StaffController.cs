using shift_making_man.Models;
using shift_making_man.Data;
using System;
using System.Collections.Generic;

namespace shift_making_man.Controllers
{
    public class StaffController
    {
        private readonly IDataAccess _dataAccess;

        public StaffController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Staff> GetStaff()
        {
            return _dataAccess.GetStaff();
        }

        // Add other methods to handle staff logic
    }
}
