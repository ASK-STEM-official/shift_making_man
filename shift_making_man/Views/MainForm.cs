using System;
using System.Collections.Generic;
using System.Windows.Forms;
using shift_making_man.Controllers;
using shift_making_man.Models;

namespace shift_making_man.Views
{
    public partial class MainForm : Form
    {
        private readonly ShiftController _shiftController;

        public MainForm(ShiftController shiftController)
        {
            InitializeComponent();
            _shiftController = shiftController;
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            btnGenerateSchedule.Click += btnGenerateSchedule_Click;
        }

        private void btnGenerateSchedule_Click(object sender, EventArgs e)
        {
            var schedule = _shiftController.GenerateSchedule();
            DisplaySchedule(schedule);
        }

        private void DisplaySchedule(Dictionary<int, List<Shift>> schedule)
        {
            dgvShifts.Rows.Clear();

            foreach (var day in schedule.Keys)
            {
                foreach (var shift in schedule[day])
                {
                    dgvShifts.Rows.Add(shift.Date.ToString("yyyy-MM-dd"), shift.StartTime.ToString("HH:mm"), shift.EndTime.ToString("HH:mm"), shift.EmployeeId);
                }
            }
        }

        // 他のUI関連メソッドを追加
    }
}
