using shift_making_man.Models;
using shift_making_man.Services;
using System.Collections.Generic;

namespace shift_making_man.Controllers
{
    public class ShiftController
    {
        private readonly ShiftSchedulerService _schedulerService;

        public ShiftController(ShiftSchedulerService schedulerService)
        {
            _schedulerService = schedulerService;
        }

        public Dictionary<int, List<Shift>> GenerateSchedule()
        {
            // 必要なデータをデータベースから取得
            var employees = _schedulerService.GetAllEmployees();
            var shifts = _schedulerService.GetAllShifts();

            // スケジュールを生成
            return _schedulerService.GenerateSchedule(employees, shifts);
        }

        // 他のコントローラメソッドを追加
    }
}

// Controllers/ShiftSchedulerController.cs
namespace shift_making_man.Controllers
{
    public class ShiftSchedulerController
    {
        // メソッドやプロパティを定義
    }
}