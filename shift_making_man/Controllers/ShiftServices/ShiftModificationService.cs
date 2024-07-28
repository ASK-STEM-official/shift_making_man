using System;
using System.Collections.Generic;
using shift_making_man.Models;
using shift_making_man.Data;

namespace shift_making_man.Controllers.ShiftServices
{
    public class ShiftModificationService
    {
        private readonly IShiftDataAccess _shiftDataAccess;
        private readonly IShiftRequestDataAccess _shiftRequestDataAccess;
        private readonly IStaffDataAccess _staffDataAccess;

        public ShiftModificationService(IShiftDataAccess shiftDataAccess, IShiftRequestDataAccess shiftRequestDataAccess, IStaffDataAccess staffDataAccess)
        {
            _shiftDataAccess = shiftDataAccess;
            _shiftRequestDataAccess = shiftRequestDataAccess;
            _staffDataAccess = staffDataAccess;
        }

        public void ProcessShiftRequests(int storeId)
        {
            Console.WriteLine($"ProcessShiftRequests started for storeId: {storeId}");

            var requests = _shiftRequestDataAccess.GetPendingRequests();
            Console.WriteLine($"Found {requests.Count} pending requests");

            foreach (var request in requests)
            {
                if (request.Status == 0) // 未処理のリクエスト
                {
                    Console.WriteLine($"Processing request ID: {request.RequestID}");

                    if (request.OriginalShiftID.HasValue)
                    {
                        var originalShift = _shiftDataAccess.GetShiftById(request.OriginalShiftID.Value);
                        if (originalShift != null && originalShift.StoreID == storeId)
                        {
                            Console.WriteLine($"Found original shift ID: {originalShift.ShiftID}");
                            ProcessModificationRequest(originalShift, request);
                        }
                        else
                        {
                            Console.WriteLine($"Shift ID: {request.OriginalShiftID.Value} does not exist or is not associated with the specified store.");
                        }
                    }
                    else
                    {
                        ProcessNewShiftRequest(request, storeId);
                    }

                    request.Status = 1; // 完了済み
                    _shiftRequestDataAccess.UpdateShiftRequest(request);
                    Console.WriteLine($"Request ID: {request.RequestID} status updated to completed");
                }
            }
        }

        private void ProcessModificationRequest(Shift originalShift, ShiftRequest request)
        {
            Console.WriteLine($"Modifying shift ID: {originalShift.ShiftID}");

            var newShift = new Shift
            {
                StoreID = originalShift.StoreID,
                StaffID = request.StaffID ?? originalShift.StaffID,
                ShiftDate = request.RequestedShiftDate ?? originalShift.ShiftDate,
                StartTime = request.RequestedStartTime ?? originalShift.StartTime,
                EndTime = request.RequestedEndTime ?? originalShift.EndTime,
                Status = originalShift.Status
            };

            _shiftDataAccess.DeleteShift(originalShift.ShiftID);
            Console.WriteLine($"Deleted original shift ID: {originalShift.ShiftID}");

            _shiftDataAccess.SaveShift(newShift);
            Console.WriteLine($"Saved new shift ID: {newShift.ShiftID}");
        }

        private void ProcessNewShiftRequest(ShiftRequest request, int storeId)
        {
            if (request.OriginalShiftID.HasValue)
            {
                Console.WriteLine($"Skipping request ID: {request.RequestID} because it is not a new shift request");
                return;
            }

            Console.WriteLine($"Creating new shift for request ID: {request.RequestID}");

            var newShift = new Shift
            {
                StoreID = storeId,
                StaffID = request.StaffID,
                ShiftDate = request.RequestedShiftDate ?? DateTime.Now,
                StartTime = request.RequestedStartTime ?? TimeSpan.Zero,
                EndTime = request.RequestedEndTime ?? TimeSpan.Zero,
                Status = 0 // 新規シフト
            };

            _shiftDataAccess.SaveShift(newShift);
            Console.WriteLine($"Saved new shift ID: {newShift.ShiftID}");
        }
    }
}
