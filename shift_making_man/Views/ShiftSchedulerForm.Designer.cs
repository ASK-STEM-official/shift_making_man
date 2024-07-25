using shift_making_man.Models;
using System;
using System.Windows.Forms;

namespace shift_making_man.Views
{
    partial class ShiftSchedulerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblStartDate;
        private Label lblEndDate;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Label lblStore;
        private ComboBox cmbStore;
        private Button btnCreateShifts;
        private DataGridView dgvShifts;
        private Label lblStoreConfig;
        private TextBox txtStoreConfig;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500); // サイズを調整
            this.Text = "Shift Scheduler";

            // 日付選択
            this.lblStartDate = new Label
            {
                Text = "開始日:",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };
            this.lblEndDate = new Label
            {
                Text = "終了日:",
                Location = new System.Drawing.Point(20, 60),
                AutoSize = true
            };
            this.dtpStartDate = new DateTimePicker
            {
                Location = new System.Drawing.Point(100, 15),
                Format = DateTimePickerFormat.Short,
                Width = 120
            };
            this.dtpEndDate = new DateTimePicker
            {
                Location = new System.Drawing.Point(100, 55),
                Format = DateTimePickerFormat.Short,
                Width = 120
            };

            // ストア選択
            this.lblStore = new Label
            {
                Text = "ストア:",
                Location = new System.Drawing.Point(20, 100),
                AutoSize = true
            };
            this.cmbStore = new ComboBox
            {
                Location = new System.Drawing.Point(100, 95),
                Width = 200
            };
            this.cmbStore.SelectedIndexChanged += CmbStore_SelectedIndexChanged; // 追加イベントハンドラ

            // シフト作成ボタン
            this.btnCreateShifts = new Button
            {
                Text = "シフト作成",
                Location = new System.Drawing.Point(20, 140),
                Width = 120
            };

            // シフト一覧表示
            this.dgvShifts = new DataGridView
            {
                Location = new System.Drawing.Point(20, 180),
                Size = new System.Drawing.Size(760, 250),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true
            };
            this.dgvShifts.Columns.Add("StoreID", "Store ID");
            this.dgvShifts.Columns.Add("StaffID", "Staff ID");
            this.dgvShifts.Columns.Add("ShiftDate", "Shift Date");
            this.dgvShifts.Columns.Add("StartTime", "Start Time");
            this.dgvShifts.Columns.Add("EndTime", "End Time");
            this.dgvShifts.Columns.Add("Status", "Status");

            // 店舗設定表示
            this.lblStoreConfig = new Label
            {
                Text = "店舗設定:",
                Location = new System.Drawing.Point(20, 450),
                AutoSize = true
            };
            this.txtStoreConfig = new TextBox
            {
                Location = new System.Drawing.Point(100, 445),
                Size = new System.Drawing.Size(680, 30),
                Multiline = true,
                ReadOnly = true
            };

            // フォームにコントロールを追加
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblStore);
            this.Controls.Add(this.cmbStore);
            this.Controls.Add(this.btnCreateShifts);
            this.Controls.Add(this.dgvShifts);
            this.Controls.Add(this.lblStoreConfig);
            this.Controls.Add(this.txtStoreConfig);
        }

        private void CmbStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStore.SelectedItem is Store selectedStore)
            {
                txtStoreConfig.Text = $"店舗ID: {selectedStore.StoreID}\n" +
                                      $"営業時間: {selectedStore.OpenTime} - {selectedStore.CloseTime}\n" +
                                      $"忙しい時間帯: {selectedStore.BusyTimeStart} - {selectedStore.BusyTimeEnd}\n" +
                                      $"忙しいスタッフ数: {selectedStore.BusyStaffCount}\n" +
                                      $"通常スタッフ数: {selectedStore.NormalStaffCount}";
            }
        }
    }
}
