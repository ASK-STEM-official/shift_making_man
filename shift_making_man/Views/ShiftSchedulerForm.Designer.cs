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

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Shift Scheduler";

            // 日付選択
            this.lblStartDate = new Label { Text = "開始日:", Location = new System.Drawing.Point(20, 20) };
            this.lblEndDate = new Label { Text = "終了日:", Location = new System.Drawing.Point(20, 60) };
            this.dtpStartDate = new DateTimePicker { Location = new System.Drawing.Point(100, 20), Format = DateTimePickerFormat.Short };
            this.dtpEndDate = new DateTimePicker { Location = new System.Drawing.Point(100, 60), Format = DateTimePickerFormat.Short };

            // ストア選択
            this.lblStore = new Label { Text = "ストア:", Location = new System.Drawing.Point(20, 100) };
            this.cmbStore = new ComboBox { Location = new System.Drawing.Point(100, 100), Width = 200 };

            // シフト作成ボタン
            this.btnCreateShifts = new Button { Text = "シフト作成", Location = new System.Drawing.Point(20, 140) };

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

            // フォームにコントロールを追加
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblStore);
            this.Controls.Add(this.cmbStore);
            this.Controls.Add(this.btnCreateShifts);
            this.Controls.Add(this.dgvShifts);
        }
    }
}
