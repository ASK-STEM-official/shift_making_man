using shift_making_man.Controllers.ShiftServices;

namespace shift_making_man.Data
{
    public class DataAccessFacade
    {
        public IAdminDataAccess AdminDataAccess { get; }
        public IShiftDataAccess ShiftDataAccess { get; }
        public IStaffDataAccess StaffDataAccess { get; }
        public IStoreDataAccess StoreDataAccess { get; }
        public IShiftRequestDataAccess ShiftRequestDataAccess { get; }
        public ShiftCreationService ShiftCreationService { get; }
        public ShiftValidationService ShiftValidationService { get; }
        public ShiftOptimizationService ShiftOptimizationService { get; }
        public ShiftModificationService ShiftModificationService { get; }

        public DataAccessFacade(
            IAdminDataAccess adminDataAccess,
            IShiftDataAccess shiftDataAccess,
            IStaffDataAccess staffDataAccess,
            IStoreDataAccess storeDataAccess,
            IShiftRequestDataAccess shiftRequestDataAccess,
            ShiftCreationService shiftCreationService,
            ShiftValidationService shiftValidationService,
            ShiftOptimizationService shiftOptimizationService,
            ShiftModificationService shiftModificationService)
        {
            AdminDataAccess = adminDataAccess;
            ShiftDataAccess = shiftDataAccess;
            StaffDataAccess = staffDataAccess;
            StoreDataAccess = storeDataAccess;
            ShiftRequestDataAccess = shiftRequestDataAccess;
            ShiftCreationService = shiftCreationService;
            ShiftValidationService = shiftValidationService;
            ShiftOptimizationService = shiftOptimizationService;
            ShiftModificationService = shiftModificationService;
        }
    }
}
