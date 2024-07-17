using shift_making_man.Models;
using shift_making_man.Data;
using System;
using System.Collections.Generic;

namespace shift_making_man.Controllers
{
    public class ShiftController
    {
        private readonly IDataAccess _dataAccess;

        public ShiftController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Shift> GetShifts()
        {
            return _dataAccess.GetShifts();
        }

        public void UpdateShift(Shift shift)
        {
            _dataAccess.UpdateShift(shift);
        }

        // Add other methods to handle shift logic
    }
}
