using System;
using System.Collections.Generic;
using System.Linq;
using shift_making_man.Models;
using shift_making_man.Data;

namespace shift_making_man.Controllers.ShiftServices
{
    public class ShiftCreationService
    {
        private readonly IStoreDataAccess _storeDataAccess;
        private readonly IStaffDataAccess _staffDataAccess;
        private readonly IShiftDataAccess _shiftDataAccess;
        private readonly IShiftRequestDataAccess _shiftRequestDataAccess;
        private readonly ShiftValidationService _validationService;
        private readonly ShiftOptimizationService _optimizationService;

        public ShiftCreationService(
            IStoreDataAccess storeDataAccess,
            IStaffDataAccess staffDataAccess,
            IShiftDataAccess shiftDataAccess,
            IShiftRequestDataAccess shiftRequestDataAccess,
            ShiftValidationService validationService,
            ShiftOptimizationService optimizationService)
        {
            _storeDataAccess = storeDataAccess;
            _staffDataAccess = staffDataAccess;
            _shiftDataAccess = shiftDataAccess;
            _shiftRequestDataAccess = shiftRequestDataAccess;
            _validationService = validationService;
            _optimizationService = optimizationService;
        }

        public List<Shift> CreateShifts(int storeId, DateTime startDate, DateTime endDate, out List<string> errors)
        {
            Console.WriteLine($"店舗ID: {storeId} のシフト作成開始 (期間: {startDate:yyyy-MM-dd} から {endDate:yyyy-MM-dd})");

            var store = _storeDataAccess.GetStoreById(storeId);
            var openTime = _storeDataAccess.GetStoreOpenTime(storeId);
            var closeTime = _storeDataAccess.GetStoreCloseTime(storeId);
            var staffList = _staffDataAccess.GetStaffByStoreId(storeId);
            var shiftRequests = _shiftRequestDataAccess.GetShiftRequestsByStoreId(storeId)
                .Where(r => r.Status == 0 && !r.OriginalShiftID.HasValue)
                .ToList();

            Console.WriteLine("シフトリクエスト:");
            foreach (var request in shiftRequests)
            {
                Console.WriteLine($"リクエストID: {request.RequestID}, スタッフID: {request.StaffID}, 日付: {request.RequestedShiftDate:yyyy-MM-dd}, 開始: {request.RequestedStartTime}, 終了: {request.RequestedEndTime}");
            }

            errors = new List<string>();
            var shifts = new List<Shift>();
            var currentDate = startDate;

            while (currentDate <= endDate)
            {
                Console.WriteLine($"日付: {currentDate:yyyy-MM-dd} のシフト作成開始");
                var dailyShifts = CreateDailyShifts(store, openTime, closeTime, currentDate, staffList, shiftRequests, out var dailyErrors);
                shifts.AddRange(dailyShifts);
                errors.AddRange(dailyErrors);

                currentDate = currentDate.AddDays(1);
            }

            var validationErrors = _validationService.GetShiftIssues(shifts);
            errors.AddRange(validationErrors);

            Console.WriteLine("シフト最適化開始");
            shifts = _optimizationService.SimulatedAnnealingOptimize(shifts);

            foreach (var request in shiftRequests)
            {
                request.Status = 1;
                _shiftRequestDataAccess.UpdateShiftRequest(request);
            }

            Console.WriteLine("シフト作成完了:");
            foreach (var shift in shifts)
            {
                shift.Status = 0; // シフトステータスを0に設定
                _shiftDataAccess.SaveShift(shift); // シフトをデータベースに保存
                Console.WriteLine($"シフトID: {shift.ShiftID}, 店舗ID: {shift.StoreID}, スタッフID: {shift.StaffID}, 日付: {shift.ShiftDate:yyyy-MM-dd}, 開始: {shift.StartTime}, 終了: {shift.EndTime}");
            }

            return shifts;
        }

        private List<Shift> CreateDailyShifts(Store store, TimeSpan openTime, TimeSpan closeTime, DateTime date, List<Staff> staffList, List<ShiftRequest> shiftRequests, out List<string> errors)
        {
            errors = new List<string>();
            var shifts = new List<Shift>();

            Console.WriteLine($"日付: {date:yyyy-MM-dd} のシフト作成");

            var busyShiftCount = store.BusyStaffCount;
            var busyShifts = CreateShiftsForPeriod(store, openTime, closeTime, date, store.BusyTimeStart, store.BusyTimeEnd, staffList, shiftRequests, busyShiftCount, out var busyErrors);
            shifts.AddRange(busyShifts);
            errors.AddRange(busyErrors);

            var normalShiftCount = store.NormalStaffCount;
            var normalShifts = CreateShiftsForPeriod(store, openTime, closeTime, date, openTime, closeTime, staffList, shiftRequests, normalShiftCount, out var normalErrors);
            shifts.AddRange(normalShifts);
            errors.AddRange(normalErrors);

            return shifts;
        }

        private List<Shift> CreateShiftsForPeriod(Store store, TimeSpan openTime, TimeSpan closeTime, DateTime date, TimeSpan startTime, TimeSpan endTime, List<Staff> staffList, List<ShiftRequest> shiftRequests, int shiftCount, out List<string> errors)
        {
            errors = new List<string>();
            var shifts = new List<Shift>();

            Console.WriteLine($"期間: {date:yyyy-MM-dd}, 開始: {startTime}, 終了: {endTime} のシフト作成");

            // 店舗の開店時間と閉店時間を超えないようにする
            if (startTime < openTime)
                startTime = openTime;
            if (endTime > closeTime)
                endTime = closeTime;

            // 1時間ごとにシフトを作成
            for (var currentTime = startTime; currentTime < endTime; currentTime = currentTime.Add(TimeSpan.FromHours(1)))
            {
                var shiftEndTime = currentTime.Add(TimeSpan.FromHours(1));
                if (shiftEndTime > endTime)
                    shiftEndTime = endTime;

                var requestsInPeriod = shiftRequests
                    .Where(r => r.RequestedShiftDate == date && r.RequestedStartTime >= currentTime && r.RequestedEndTime <= shiftEndTime)
                    .ToList();

                if (requestsInPeriod.Count == 0)
                {
                    errors.Add($"リクエストがありません (日付: {date:yyyy-MM-dd}, 開始: {currentTime}, 終了: {shiftEndTime})");
                }

                for (int i = 0; i < shiftCount; i++)
                {
                    var staff = GetAvailableStaff(staffList, requestsInPeriod, date, currentTime, shiftEndTime);
                    if (staff == null)
                    {
                        errors.Add($"スタッフ不足 (日付: {date:yyyy-MM-dd}, 開始: {currentTime}, 終了: {shiftEndTime})");
                    }

                    var shift = new Shift
                    {
                        StoreID = store.StoreID,
                        ShiftDate = date,
                        StartTime = currentTime,
                        EndTime = shiftEndTime,
                        StaffID = staff?.StaffID
                    };

                    Console.WriteLine($"シフト作成 - 店舗ID: {store.StoreID}, 日付: {date:yyyy-MM-dd}, スタッフID: {staff?.StaffID ?? 0}, 開始: {currentTime}, 終了: {shiftEndTime}");

                    shifts.Add(shift);
                }
            }

            Console.WriteLine($"期間: {date:yyyy-MM-dd}, 開始: {startTime}, 終了: {endTime} のシフト数: {shifts.Count}");

            return shifts;
        }

        private Staff GetAvailableStaff(List<Staff> staffList, List<ShiftRequest> requestsInPeriod, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            foreach (var request in requestsInPeriod)
            {
                var staff = staffList.FirstOrDefault(s => s.StaffID == request.StaffID);
                if (staff != null)
                {
                    staffList.Remove(staff);
                    return staff;
                }
            }
            return null;
        }
    }
}
