using System;
using System.Collections.Generic;
using System.Windows.Forms;
using shift_making_man.Controllers;
using shift_making_man.Models;

namespace shift_making_man.Views
{
    public partial class ShiftSchedulerForm : Form
    {
        private readonly ShiftSchedulerController _controller;

        public ShiftSchedulerForm(ShiftSchedulerController controller)
        {
            InitializeComponent();
            _controller = controller;
            LoadStores();
            btnCreateShifts.Click += OnCreateShiftsButtonClick;
        }

        private void LoadStores()
        {
            try
            {
                List<Store> stores = _controller.GetStores();
                cmbStore.DataSource = stores;
                cmbStore.DisplayMember = "StoreName";
                cmbStore.ValueMember = "StoreID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ストアの読み込み中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnCreateShiftsButtonClick(object sender, EventArgs e)
        {
            if (cmbStore.SelectedItem is Store selectedStore)
            {
                List<string> errors;
                CreateShifts(dtpStartDate.Value, dtpEndDate.Value, selectedStore.StoreID, out errors);

                // エラーがあれば表示
                if (errors.Count > 0)
                {
                    MessageBox.Show(string.Join(Environment.NewLine, errors), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ストアが選択されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CreateShifts(DateTime startDate, DateTime endDate, int storeId, out List<string> errors)
        {
            errors = new List<string>();
            try
            {
                var shifts = _controller.CreateShifts(startDate, endDate, out errors);
                dgvShifts.Rows.Clear();
                foreach (var shift in shifts)
                {
                    if (shift.StoreID == storeId)
                    {
                        dgvShifts.Rows.Add(shift.StoreID, shift.StaffID, shift.ShiftDate.ToShortDateString(), shift.StartTime.ToString(@"hh\:mm"), shift.EndTime.ToString(@"hh\:mm"), shift.Status);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"シフト作成中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
