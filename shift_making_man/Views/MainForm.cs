using System;
using System.Collections.Generic;
using System.Windows.Forms;
using shift_making_man.Models;
using shift_making_man.Services;

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
            LoadAdmins();
        }

        private void btnOpenDashboard_Click(object sender, EventArgs e)
        {
            // DashboardForm に DataAccessFacade を渡す
            DashboardForm dashboardForm = new DashboardForm(dataAccessFacade);
            dashboardForm.Show();
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

        private void LoadAdmins()
        {
            List<Admin> admins = dataAccessFacade.AdminDataAccess.GetAdmins();
            dataGridViewAdmins.DataSource = admins;
        }
    }
}
