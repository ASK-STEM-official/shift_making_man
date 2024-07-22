using System;
using System.Collections.Generic;
using System.Windows.Forms;
using shift_making_man.Models;
using shift_making_man.Services;

namespace shift_making_man.Views
{
    public partial class ShiftListForm : Form
    {
        private readonly DataAccessFacade dataAccessFacade;

        public ShiftListForm(DataAccessFacade dataAccessFacade)
        {
            InitializeComponent();
            this.dataAccessFacade = dataAccessFacade;
        }

        private void ShiftListForm_Load(object sender, EventArgs e)
        {
            LoadShifts();
        }

        private void LoadShifts()
        {
            List<Shift> shifts = dataAccessFacade.ShiftDataAccess.GetShifts();
            dataGridViewShifts.DataSource = shifts;
        }
    }
}
