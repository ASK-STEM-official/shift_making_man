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
            // ストアのリストを取得して ComboBox に追加する
            List<Store> stores = _controller.GetStores(); // 修正: _controller.GetStores() を呼び出す
            cmbStore.DataSource = stores;
            cmbStore.DisplayMember = "StoreName"; // StoreName を表示する
            cmbStore.ValueMember = "StoreID"; // StoreID を選択項目として扱う
        }

        private void OnCreateShiftsButtonClick(object sender, EventArgs e)
        {
            // シフト作成処理を呼び出す
            CreateShifts(dtpStartDate.Value, dtpEndDate.Value, (cmbStore.SelectedItem as Store)?.StoreID ?? 0);
        }

        private void CreateShifts(DateTime startDate, DateTime endDate, int storeId)
        {
            // シフト作成処理を呼び出す
            var shifts = _controller.CreateShifts(startDate, endDate);

            // DataGridViewにシフトを表示
            dgvShifts.Rows.Clear();

            foreach (var shift in shifts)
            {
                if (shift.StoreID == storeId) // 選択したストアのシフトのみ表示
                {
                    dgvShifts.Rows.Add(shift.StoreID, shift.StaffID, shift.ShiftDate.ToShortDateString(), shift.StartTime.ToString(@"hh\:mm"), shift.EndTime.ToString(@"hh\:mm"), shift.Status);
                }
            }
        }
    }
}
