using System;
using System.Collections.Generic;
using System.Windows.Forms;
using shift_making_man.Data;
using shift_making_man.Models;

namespace shift_making_man.Views
{
    public partial class MainForm : Form
    {
        private readonly IAdminDataAccess _adminDataAccess;
        private readonly IShiftDataAccess _shiftDataAccess;
        private readonly IStaffDataAccess _staffDataAccess;
        private readonly IStoreDataAccess _storeDataAccess;

        public MainForm(IAdminDataAccess adminDataAccess, IShiftDataAccess shiftDataAccess, IStaffDataAccess staffDataAccess, IStoreDataAccess storeDataAccess)
        {
            InitializeComponent();
            this._adminDataAccess = adminDataAccess;
            this._shiftDataAccess = shiftDataAccess;
            this._staffDataAccess = staffDataAccess;
            this._storeDataAccess = storeDataAccess;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadShifts();
            LoadStaff();
            LoadStores();
            LoadAdmins();
        }

        private void btnOpenDashboard_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.Show();
        }

        private void LoadShifts()
        {
            List<Shift> shifts = _shiftDataAccess.GetShifts();
            dataGridViewShifts.DataSource = shifts;
        }

        private void LoadStaff()
        {
            List<Staff> staff = _staffDataAccess.GetStaff();
            dataGridViewStaff.DataSource = staff;
        }

        private void LoadStores()
        {
            List<Store> stores = _storeDataAccess.GetStores();
            dataGridViewStores.DataSource = stores;
        }

        private void LoadAdmins()
        {
            List<Admin> admins = _adminDataAccess.GetAdmins();
            dataGridViewAdmins.DataSource = admins;
        }
    }
}
