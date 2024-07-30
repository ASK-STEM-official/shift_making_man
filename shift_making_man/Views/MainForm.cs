//
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using shift_making_man.Controllers;
using shift_making_man.Models;
using shift_making_man.Data;

namespace shift_making_man.Views
{
    public partial class MainForm : Form
    {
        private readonly DataAccessFacade dataAccessFacade;

        public MainForm(DataAccessFacade dataAccessFacade)
        {
            InitializeComponent();
            this.dataAccessFacade = dataAccessFacade;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadShifts();
            LoadStaff();
            LoadStores();
            LoadAttendance();
            LoadShiftRequests();
        }

        private void btnOpenDashboard_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm(dataAccessFacade);
            dashboardForm.Show();
        }

        private void btnLoadShifts_Click(object sender, EventArgs e)
        {
            ShiftListForm shiftListForm = new ShiftListForm(dataAccessFacade);
            shiftListForm.Show();
        }

        private void btnOpenShiftScheduler_Click(object sender, EventArgs e)
        {
            ShiftSchedulerController shiftSchedulerController = new ShiftSchedulerController(
                dataAccessFacade.ShiftCreationService, // ShiftCreationService 型
                dataAccessFacade.ShiftModificationService, // ShiftModificationService 型
                dataAccessFacade.ShiftValidationService, // ShiftValidationService 型
                dataAccessFacade.ShiftOptimizationService, // ShiftOptimizationService 型
                dataAccessFacade.StoreDataAccess, // IStoreDataAccess 型
                dataAccessFacade.ShiftDataAccess); // IShiftDataAccess 型

            ShiftSchedulerForm shiftSchedulerForm = new ShiftSchedulerForm(shiftSchedulerController);
            shiftSchedulerForm.Show();
        }

        private void LoadShifts()
        {
            List<Shift> shifts = dataAccessFacade.ShiftDataAccess.GetShifts();
            dataGridViewShifts.DataSource = shifts;
        }

        private void LoadStaff()
        {
            List<Staff> staff = dataAccessFacade.StaffDataAccess.GetStaff();
            dataGridViewStaff.DataSource = staff;
        }

        private void LoadStores()
        {
            List<Store> stores = dataAccessFacade.StoreDataAccess.GetStores();
            dataGridViewStores.DataSource = stores;
        }

        private void LoadAttendance()
        {
            List<Attendance> admins = dataAccessFacade.AttendanceDataAccess.GetAttendances();
            dataGridViewAdmins.DataSource = admins;
        }

        private void LoadShiftRequests()
        {
            List<ShiftRequest> shiftRequests = dataAccessFacade.ShiftRequestDataAccess.GetShiftRequests();
            dataGridViewShiftRequests.DataSource = shiftRequests;
        }
    }
}
