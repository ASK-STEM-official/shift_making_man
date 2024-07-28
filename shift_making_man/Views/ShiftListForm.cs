using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using shift_making_man.Models;
using shift_making_man.Data;

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
            // 日付検索の初期値を現在の日付に設定
            dateTimePicker.Value = DateTime.Now;
            LoadShiftsByDate(DateTime.Now);
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            LoadShiftsByDate(dateTimePicker.Value);
        }

        private void LoadShiftsByDate(DateTime date)
        {
            List<Shift> shifts = dataAccessFacade.ShiftDataAccess.GetShifts()
                                        .Where(s => s.ShiftDate.Date == date.Date)
                                        .ToList();

            var staffIds = shifts.Select(s => s.StaffID).Distinct().ToList();
            var staffs = dataAccessFacade.StaffDataAccess.GetStaff()
                                    .Where(s => staffIds.Contains(s.StaffID))
                                    .ToList();

            var dataTable = new DataTable();
            dataTable.Columns.Add("Staff Name", typeof(string));

            for (int hour = 0; hour < 24; hour++)
            {
                dataTable.Columns.Add(hour.ToString("00") + ":00", typeof(string));
            }

            foreach (var staff in staffs)
            {
                var row = dataTable.NewRow();
                row["Staff Name"] = staff.FullName;

                foreach (var shift in shifts.Where(s => s.StaffID == staff.StaffID))
                {
                    for (var time = shift.StartTime.Hours; time < shift.EndTime.Hours; time++)
                    {
                        row[time.ToString("00") + ":00"] = "Shift";
                    }
                }

                dataTable.Rows.Add(row);
            }

            dataGridViewShifts.DataSource = dataTable;
        }
    }
}
