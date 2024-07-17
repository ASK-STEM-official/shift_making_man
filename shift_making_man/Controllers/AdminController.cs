using shift_making_man.Models;
using shift_making_man.Data;
using System.Collections.Generic;

namespace shift_making_man.Controllers
{
    public class AdminController
    {
        private readonly IDataAccess _dataAccess;

        public AdminController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Add methods to handle admin logic
    }
}
