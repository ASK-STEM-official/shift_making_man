namespace shift_making_man.Views
{
    partial class ShiftSchedulerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbStore = new System.Windows.Forms.ComboBox();
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            this.ColumnShiftDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStaffName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCreateShifts = new System.Windows.Forms.Button();
            this.btnModifyShifts = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStore = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblShifts = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbStore
            // 
            this.cmbStore.FormattingEnabled = true;
            this.cmbStore.Location = new System.Drawing.Point(103, 19);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Size = new System.Drawing.Size(200, 23);
            this.cmbStore.TabIndex = 0;
            // 
            // dgvShifts
            // 
            this.dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShifts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnShiftDate,
            this.ColumnStartTime,
            this.ColumnEndTime,
            this.ColumnStaffName});
            this.dgvShifts.Location = new System.Drawing.Point(12, 223);
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.RowHeadersWidth = 51;
            this.dgvShifts.RowTemplate.Height = 25;
            this.dgvShifts.Size = new System.Drawing.Size(760, 150);
            this.dgvShifts.TabIndex = 1;
            // 
            // ColumnShiftDate
            // 
            this.ColumnShiftDate.HeaderText = "シフト日付";
            this.ColumnShiftDate.MinimumWidth = 6;
            this.ColumnShiftDate.Name = "ColumnShiftDate";
            this.ColumnShiftDate.Width = 120;
            // 
            // ColumnStartTime
            // 
            this.ColumnStartTime.HeaderText = "開始時刻";
            this.ColumnStartTime.MinimumWidth = 6;
            this.ColumnStartTime.Name = "ColumnStartTime";
            this.ColumnStartTime.Width = 120;
            // 
            // ColumnEndTime
            // 
            this.ColumnEndTime.HeaderText = "終了時刻";
            this.ColumnEndTime.MinimumWidth = 6;
            this.ColumnEndTime.Name = "ColumnEndTime";
            this.ColumnEndTime.Width = 120;
            // 
            // ColumnStaffName
            // 
            this.ColumnStaffName.HeaderText = "担当スタッフ";
            this.ColumnStaffName.MinimumWidth = 6;
            this.ColumnStaffName.Name = "ColumnStaffName";
            this.ColumnStaffName.Width = 150;
            // 
            // btnCreateShifts
            // 
            this.btnCreateShifts.Location = new System.Drawing.Point(103, 123);
            this.btnCreateShifts.Name = "btnCreateShifts";
            this.btnCreateShifts.Size = new System.Drawing.Size(200, 30);
            this.btnCreateShifts.TabIndex = 3;
            this.btnCreateShifts.Text = "シフト作成";
            this.btnCreateShifts.UseVisualStyleBackColor = true;
            this.btnCreateShifts.Click += new System.EventHandler(this.OnCreateShiftsButtonClick);
            // 
            // btnModifyShifts
            // 
            this.btnModifyShifts.Location = new System.Drawing.Point(103, 159);
            this.btnModifyShifts.Name = "btnModifyShifts";
            this.btnModifyShifts.Size = new System.Drawing.Size(200, 30);
            this.btnModifyShifts.TabIndex = 4;
            this.btnModifyShifts.Text = "シフト修正";
            this.btnModifyShifts.UseVisualStyleBackColor = true;
            this.btnModifyShifts.Click += new System.EventHandler(this.OnModifyShiftsButtonClick);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(103, 53);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 22);
            this.dtpStartDate.TabIndex = 5;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(103, 85);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 22);
            this.dtpEndDate.TabIndex = 6;
            // 
            // lblStore
            // 
            this.lblStore.AutoSize = true;
            this.lblStore.Location = new System.Drawing.Point(22, 22);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(75, 15);
            this.lblStore.TabIndex = 7;
            this.lblStore.Text = "店舗選択：";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(22, 59);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(75, 15);
            this.lblStartDate.TabIndex = 8;
            this.lblStartDate.Text = "開始日付：";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(22, 91);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(75, 15);
            this.lblEndDate.TabIndex = 9;
            this.lblEndDate.Text = "終了日付：";
            // 
            // lblShifts
            // 
            this.lblShifts.AutoSize = true;
            this.lblShifts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblShifts.Location = new System.Drawing.Point(-3, 192);
            this.lblShifts.Name = "lblShifts";
            this.lblShifts.Size = new System.Drawing.Size(152, 28);
            this.lblShifts.TabIndex = 10;
            this.lblShifts.Text = "作成されたシフト";
            // 
            // ShiftSchedulerForm
            // 
            this.ClientSize = new System.Drawing.Size(780, 385);
            this.Controls.Add(this.lblShifts);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.lblStore);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.btnModifyShifts);
            this.Controls.Add(this.btnCreateShifts);
            this.Controls.Add(this.dgvShifts);
            this.Controls.Add(this.cmbStore);
            this.Name = "ShiftSchedulerForm";
            this.Text = "Shift Scheduler";
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox cmbStore;
        private System.Windows.Forms.DataGridView dgvShifts;
        private System.Windows.Forms.Button btnCreateShifts;
        private System.Windows.Forms.Button btnModifyShifts; // 修正用ボタンの追加
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStore;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblShifts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnShiftDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStaffName;
    }
}
