using shift_making_man.Controllers;
using shift_making_man.Models;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace shift_making_man.Views
{
    public partial class ShiftSchedulerForm : Form
    {
        private readonly ShiftSchedulerController _controller;

        public ShiftSchedulerForm(ShiftSchedulerController schedulerController)
        {
            InitializeComponent();
            _controller = schedulerController;
            LoadStores();
        }

        private void LoadStores()
        {
            var stores = _controller.GetStores();
            cmbStore.DataSource = stores;
            cmbStore.DisplayMember = "StoreName";
            cmbStore.ValueMember = "StoreID";
        }

        private void OnCreateShiftsButtonClick(object sender, EventArgs e)
        {
            // 店舗IDを取得
            int storeId = (int)cmbStore.SelectedValue;

            // DateTimePicker から選択された日時を取得し、時間部分をクリアする
            DateTime startDate = dtpStartDate.Value.Date; 
            DateTime endDate = dtpEndDate.Value.Date;     

            // シフト作成メソッドを呼び出し
            List<string> errors;
            List<Shift> shifts = _controller.CreateShifts(storeId, startDate, endDate, out errors);

            // DataGridView にシフトを表示
            dgvShifts.Rows.Clear();
            foreach (var shift in shifts)
            {
                dgvShifts.Rows.Add(shift.ShiftDate, shift.StartTime, shift.EndTime, shift.Staff?.FullName ?? "スタッフ未設定");
            }

            // シフト作成完了のメッセージ
            if (shifts.Count > 0)
            {
                MessageBox.Show("シフトが作成されました。");
            }
        }


        private void OnModifyShiftsButtonClick(object sender, EventArgs e)
        {
            int storeId = (int)cmbStore.SelectedValue;
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            MessageBox.Show("シフト修正リクエストが処理されました。");
        }
    }
}
