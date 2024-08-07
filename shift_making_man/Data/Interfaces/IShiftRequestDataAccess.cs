﻿//
using System.Collections.Generic;
using shift_making_man.Models;

namespace shift_making_man.Data
{
    public interface IShiftRequestDataAccess
    {
        List<ShiftRequest> GetShiftRequests();
        //ShiftRequest GetShiftRequestById(int requestId);
        //void AddShiftRequest(ShiftRequest shiftRequest);
        void UpdateShiftRequest(ShiftRequest shiftRequest);
        //void DeleteShiftRequest(int requestId);
        //int GetPendingShiftRequestCount();
        //int GetCountByOriginalShiftID(int? originalShiftID);
        //int GetCountByOriginalShiftIDNotNull();
        //(int NewRequestCount, int ChangeRequestCount) GetPendingShiftRequestCounts();
        List<ShiftRequest> GetShiftRequestsByStoreId(int storeId);
        List<ShiftRequest> GetPendingRequests();
    }
}
