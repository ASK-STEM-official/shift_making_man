namespace shift_making_man.Views
{
    partial class DashboardForm
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
            this.listBoxStoreWorkCounts = new System.Windows.Forms.ListBox();
            this.listBoxStaffPositions = new System.Windows.Forms.ListBox();
            this.listBoxRequestedStaffs = new System.Windows.Forms.ListBox();
            this.listBoxWorkingStaffs = new System.Windows.Forms.ListBox();
            this.labelStoreWorkCounts = new System.Windows.Forms.Label();
            this.labelStaffPositions = new System.Windows.Forms.Label();
            this.labelRequestedStaffs = new System.Windows.Forms.Label();
            this.labelWorkingStaffs = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxStoreWorkCounts
            // 
            this.listBoxStoreWorkCounts.FormattingEnabled = true;
            this.listBoxStoreWorkCounts.Location = new System.Drawing.Point(12, 35);
            this.listBoxStoreWorkCounts.Name = "listBoxStoreWorkCounts";
            this.listBoxStoreWorkCounts.Size = new System.Drawing.Size(300, 95);
            this.listBoxStoreWorkCounts.TabIndex = 0;
            // 
            // listBoxStaffPositions
            // 
            this.listBoxStaffPositions.FormattingEnabled = true;
            this.listBoxStaffPositions.Location = new System.Drawing.Point(12, 149);
            this.listBoxStaffPositions.Name = "listBoxStaffPositions";
            this.listBoxStaffPositions.Size = new System.Drawing.Size(300, 95);
            this.listBoxStaffPositions.TabIndex = 1;
            // 
            // listBoxRequestedStaffs
            // 
            this.listBoxRequestedStaffs.FormattingEnabled = true;
            this.listBoxRequestedStaffs.Location = new System.Drawing.Point(12, 263);
            this.listBoxRequestedStaffs.Name = "listBoxRequestedStaffs";
            this.listBoxRequestedStaffs.Size = new System.Drawing.Size(300, 95);
            this.listBoxRequestedStaffs.TabIndex = 2;
            // 
            // listBoxWorkingStaffs
            // 
            this.listBoxWorkingStaffs.FormattingEnabled = true;
            this.listBoxWorkingStaffs.Location = new System.Drawing.Point(12, 377);
            this.listBoxWorkingStaffs.Name = "listBoxWorkingStaffs";
            this.listBoxWorkingStaffs.Size = new System.Drawing.Size(300, 95);
            this.listBoxWorkingStaffs.TabIndex = 3;
            // 
            // labelStoreWorkCounts
            // 
            this.labelStoreWorkCounts.AutoSize = true;
            this.labelStoreWorkCounts.Location = new System.Drawing.Point(12, 17);
            this.labelStoreWorkCounts.Name = "labelStoreWorkCounts";
            this.labelStoreWorkCounts.Size = new System.Drawing.Size(110, 15);
            this.labelStoreWorkCounts.TabIndex = 4;
            this.labelStoreWorkCounts.Text = "店舗ごとの勤務人数";
            // 
            // labelStaffPositions
            // 
            this.labelStaffPositions.AutoSize = true;
            this.labelStaffPositions.Location = new System.Drawing.Point(12, 131);
            this.labelStaffPositions.Name = "labelStaffPositions";
            this.labelStaffPositions.Size = new System.Drawing.Size(110, 15);
            this.labelStaffPositions.TabIndex = 5;
            this.labelStaffPositions.Text = "役職ごとの人数";
            // 
            // labelRequestedStaffs
            // 
            this.labelRequestedStaffs.AutoSize = true;
            this.labelRequestedStaffs.Location = new System.Drawing.Point(12, 245);
            this.labelRequestedStaffs.Name = "labelRequestedStaffs";
            this.labelRequestedStaffs.Size = new System.Drawing.Size(132, 15);
            this.labelRequestedStaffs.TabIndex = 6;
            this.labelRequestedStaffs.Text = "働いているはずのメンバー";
            // 
            // labelWorkingStaffs
            // 
            this.labelWorkingStaffs.AutoSize = true;
            this.labelWorkingStaffs.Location = new System.Drawing.Point(12, 359);
            this.labelWorkingStaffs.Name = "labelWorkingStaffs";
            this.labelWorkingStaffs.Size = new System.Drawing.Size(132, 15);
            this.labelWorkingStaffs.TabIndex = 7;
            this.labelWorkingStaffs.Text = "現在働いているメンバー";
            // 
            // DashboardForm
            // 
            this.ClientSize = new System.Drawing.Size(324, 481);
            this.Controls.Add(this.labelWorkingStaffs);
            this.Controls.Add(this.labelRequestedStaffs);
            this.Controls.Add(this.labelStaffPositions);
            this.Controls.Add(this.labelStoreWorkCounts);
            this.Controls.Add(this.listBoxWorkingStaffs);
            this.Controls.Add(this.listBoxRequestedStaffs);
            this.Controls.Add(this.listBoxStaffPositions);
            this.Controls.Add(this.listBoxStoreWorkCounts);
            this.Name = "DashboardForm";
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListBox listBoxStoreWorkCounts;
        private System.Windows.Forms.ListBox listBoxStaffPositions;
        private System.Windows.Forms.ListBox listBoxRequestedStaffs;
        private System.Windows.Forms.ListBox listBoxWorkingStaffs;
        private System.Windows.Forms.Label labelStoreWorkCounts;
        private System.Windows.Forms.Label labelStaffPositions;
        private System.Windows.Forms.Label labelRequestedStaffs;
        private System.Windows.Forms.Label labelWorkingStaffs;
    }
}
