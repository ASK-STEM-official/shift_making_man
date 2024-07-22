using shift_making_man.Data;

namespace shift_making_man.Services
{
    public class DataAccessFacade
    {
        public IAdminDataAccess AdminDataAccess { get; }
        public IShiftDataAccess ShiftDataAccess { get; }
        public IStaffDataAccess StaffDataAccess { get; }
        public IStoreDataAccess StoreDataAccess { get; }
        public IShiftRequestDataAccess ShiftRequestDataAccess { get; }

        public DataAccessFacade(
            IAdminDataAccess adminDataAccess,
            IShiftDataAccess shiftDataAccess,
            IStaffDataAccess staffDataAccess,
            IStoreDataAccess storeDataAccess,
            IShiftRequestDataAccess shiftRequestDataAccess)
        {
            AdminDataAccess = adminDataAccess;
            ShiftDataAccess = shiftDataAccess;
            StaffDataAccess = staffDataAccess;
            StoreDataAccess = storeDataAccess;
            ShiftRequestDataAccess = shiftRequestDataAccess;
        }
    }
}