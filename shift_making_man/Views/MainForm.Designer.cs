namespace shift_making_man.Views
{
    partial class MainForm
    {
        private System.Windows.Forms.Button btnGenerateSchedule;
        private System.Windows.Forms.DataGridView dgvShifts;

        private void InitializeComponent()
        {
            this.btnGenerateSchedule = new System.Windows.Forms.Button();
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerateSchedule
            // 
            this.btnGenerateSchedule.Location = new System.Drawing.Point(12, 12);
            this.btnGenerateSchedule.Name = "btnGenerateSchedule";
            this.btnGenerateSchedule.Size = new System.Drawing.Size(160, 23);
            this.btnGenerateSchedule.TabIndex = 0;
            this.btnGenerateSchedule.Text = "Generate Schedule";
            this.btnGenerateSchedule.UseVisualStyleBackColor = true;
            // 
            // dgvShifts
            // 
            this.dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShifts.Location = new System.Drawing.Point(12, 50);
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.Size = new System.Drawing.Size(760, 400);
            this.dgvShifts.TabIndex = 1;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnGenerateSchedule);
            this.Controls.Add(this.dgvShifts);
            this.Name = "MainForm";
            this.Text = "Shift Scheduler";
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
