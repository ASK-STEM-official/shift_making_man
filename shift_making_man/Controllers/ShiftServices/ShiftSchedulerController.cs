using shift_making_man.Controllers.ShiftServices;
using shift_making_man.Data;
using shift_making_man.Models;
using System.Collections.Generic;
using System;

namespace shift_making_man.Controllers
{
    public class ShiftSchedulerController
    {
        private readonly ShiftCreationService _creationService;
        private readonly ShiftModificationService _modificationService;
        private readonly ShiftValidationService _validationService;
        private readonly ShiftOptimizationService _optimizationService;
        private readonly IStoreDataAccess _storeDataAccess;
        private readonly IShiftDataAccess _shiftDataAccess;

        public ShiftSchedulerController(
            ShiftCreationService creationService,
            ShiftModificationService modificationService,
            ShiftValidationService validationService,
            ShiftOptimizationService optimizationService,
            IStoreDataAccess storeDataAccess,
            IShiftDataAccess shiftDataAccess)
        {
            _creationService = creationService;
            _modificationService = modificationService;
            _validationService = validationService;
            _optimizationService = optimizationService;
            _storeDataAccess = storeDataAccess;
            _shiftDataAccess = shiftDataAccess;
        }

        public List<Store> GetStores()
        {
            return _storeDataAccess.GetStores();
        }

        public List<Shift> CreateShifts(int storeId, DateTime startDate, DateTime endDate, out List<string> errors)
        {
            // シフト作成の際のエラーハンドリング
            errors = new List<string>();
            var shifts = _creationService.CreateShifts(storeId, startDate, endDate, out errors);
            return shifts;
        }

        public void ScheduleShifts(int storeId, DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"Scheduling shifts for store ID: {storeId}, from {startDate} to {endDate}");

            var errors = new List<string>();

            // シフト作成とエラーチェック
            var shifts = CreateShifts(storeId, startDate, endDate, out errors);
            if (errors.Count > 0)
            {
                Console.WriteLine("Errors during shift creation:");
                errors.ForEach(error => Console.WriteLine($"Error: {error}"));
                return; // エラーがある場合、シフトスケジューリングを中止
            }

            _modificationService.ProcessShiftRequests(storeId);

            var issues = _validationService.GetShiftIssues(shifts);
            if (issues.Count > 0)
            {
                Console.WriteLine("Issues found during validation:");
                issues.ForEach(issue => Console.WriteLine($"Issue: {issue}"));
                return; // 問題がある場合、シフトスケジューリングを中止
            }

            var optimizedShifts = _optimizationService.SimulatedAnnealingOptimize(shifts);

            _shiftDataAccess.SaveShiftList(optimizedShifts);
            Console.WriteLine("Shifts have been saved");
        }
    }
}
