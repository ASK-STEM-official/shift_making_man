using shift_making_man.Models;
using shift_making_man.Services;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Linq;

namespace shift_making_man.Views
{
    public partial class DashboardForm : Form
    {
        private readonly DataAccessFacade dataAccessFacade;

        public DashboardForm(DataAccessFacade dataAccessFacade)
        {
            InitializeComponent();
            this.dataAccessFacade = dataAccessFacade;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // 店舗情報を取得
            var stores = dataAccessFacade.StoreDataAccess.GetStores();
            var shifts = dataAccessFacade.ShiftDataAccess.GetShifts();
            var shiftRequests = dataAccessFacade.ShiftRequestDataAccess.GetShiftRequests();
            var staffs = dataAccessFacade.StaffDataAccess.GetStaff();

            // 店舗ごとに日ごとの勤務人数を集計
            var storeWorkCountsByDate = shifts
                .Where(shift => shift.StoreID.HasValue) // StoreIDがnullでない場合にフィルタリング
                .GroupBy(shift => new { StoreID = shift.StoreID.Value, shift.ShiftDate }) // StoreID.Valueを使用し、プロパティ名を明示的に指定
                .ToDictionary(
                    group => (group.Key.StoreID, group.Key.ShiftDate),
                    group => group.Count()
                );

            // 店舗内の役職ごとの人数
            var staffPositions = staffs.GroupBy(staff => staff.EmploymentType).ToDictionary(
                group => group.Key,
                group => group.Count()
            );

            // 働いているはずのメンバーリスト
            var requestedShifts = shiftRequests
                .Where(request => request.RequestedShiftDate.HasValue && request.RequestedShiftDate.Value.Date == DateTime.Today)
                .ToList();
            var scheduledStaffs = requestedShifts
                .Select(request => dataAccessFacade.StaffDataAccess.GetStaffById(request.StaffID.GetValueOrDefault()))
                .Where(staff => staff != null)
                .ToList();

            // 現在働いているメンバーリスト（現在働いている時間も表示）
            var currentShifts = shifts
                .Where(shift => shift.ShiftDate.Date == DateTime.Today &&
                    shift.StartTime <= DateTime.Now.TimeOfDay && shift.EndTime >= DateTime.Now.TimeOfDay)
                .ToList();
            var workingStaffs = currentShifts
                .Select(shift => dataAccessFacade.StaffDataAccess.GetStaffById(shift.StaffID.GetValueOrDefault()))
                .Where(staff => staff != null)
                .ToList();

            // UIにデータを表示
            DisplayStoreWorkCountsByDate(storeWorkCountsByDate);
            DisplayStaffPositions(staffPositions);
            DisplayRequestedStaffs(scheduledStaffs);
            DisplayWorkingStaffs(workingStaffs);
        }

        private void DisplayStoreWorkCountsByDate(Dictionary<(int StoreID, DateTime ShiftDate), int> storeWorkCountsByDate)
        {
            // 店舗ごとに日ごとの勤務人数をUIに表示するロジック
            listBoxStoreWorkCounts.Items.Clear();
            foreach (var entry in storeWorkCountsByDate)
            {
                listBoxStoreWorkCounts.Items.Add($"店舗ID: {entry.Key.StoreID}, 日付: {entry.Key.ShiftDate.ToShortDateString()}, 勤務人数: {entry.Value}");
            }
        }

        private void DisplayStaffPositions(Dictionary<string, int> staffPositions)
        {
            // 店舗内の役職ごとの人数をUIに表示するロジック
            listBoxStaffPositions.Items.Clear();
            foreach (var staffPosition in staffPositions)
            {
                listBoxStaffPositions.Items.Add($"役職: {staffPosition.Key}, 人数: {staffPosition.Value}");
            }
        }

        private void DisplayRequestedStaffs(List<Staff> requestedStaffs)
        {
            // 働いているはずのメンバーリストをUIに表示するロジック
            listBoxRequestedStaffs.Items.Clear();
            foreach (var staff in requestedStaffs)
            {
                listBoxRequestedStaffs.Items.Add($"スタッフID: {staff.StaffID}, 名前: {staff.FullName}");
            }
        }

        private void DisplayWorkingStaffs(List<Staff> workingStaffs)
        {
            // 現在働いているメンバーリスト（現在働いている時間も表示）をUIに表示するロジック
            listBoxWorkingStaffs.Items.Clear();
            foreach (var staff in workingStaffs)
            {
                listBoxWorkingStaffs.Items.Add($"スタッフID: {staff.StaffID}, 名前: {staff.FullName}, 現在の勤務時間: {DateTime.Now.TimeOfDay}");
            }
        }
    }
}
