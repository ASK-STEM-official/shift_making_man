namespace shift_making_man.Views
{
    partial class MainForm
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
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnOpenDashboard = new System.Windows.Forms.Button();
            this.btnLoadShifts = new System.Windows.Forms.Button();
            this.btnOpenShiftScheduler = new System.Windows.Forms.Button();
            this.dataGridViewShifts = new System.Windows.Forms.DataGridView();
            this.dataGridViewStaff = new System.Windows.Forms.DataGridView();
            this.dataGridViewStores = new System.Windows.Forms.DataGridView();
            this.dataGridViewAdmins = new System.Windows.Forms.DataGridView();
            this.dataGridViewShiftRequests = new System.Windows.Forms.DataGridView(); // 新しいDataGridViewを追加
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdmins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShiftRequests)).BeginInit(); // 新しいDataGridViewを初期化
            this.SuspendLayout();
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(13, 13);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(75, 23);
            this.btnLoadData.TabIndex = 0;
            this.btnLoadData.Text = "Load Data";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnOpenDashboard
            // 
            this.btnOpenDashboard.Location = new System.Drawing.Point(95, 13);
            this.btnOpenDashboard.Name = "btnOpenDashboard";
            this.btnOpenDashboard.Size = new System.Drawing.Size(120, 23);
            this.btnOpenDashboard.TabIndex = 5;
            this.btnOpenDashboard.Text = "Open Dashboard";
            this.btnOpenDashboard.UseVisualStyleBackColor = true;
            this.btnOpenDashboard.Click += new System.EventHandler(this.btnOpenDashboard_Click);
            // 
            // btnLoadShifts
            // 
            this.btnLoadShifts.Location = new System.Drawing.Point(221, 13);
            this.btnLoadShifts.Name = "btnLoadShifts";
            this.btnLoadShifts.Size = new System.Drawing.Size(120, 23);
            this.btnLoadShifts.TabIndex = 6;
            this.btnLoadShifts.Text = "Load Shifts";
            this.btnLoadShifts.UseVisualStyleBackColor = true;
            this.btnLoadShifts.Click += new System.EventHandler(this.btnLoadShifts_Click);
            // 
            // btnOpenShiftScheduler
            // 
            this.btnOpenShiftScheduler.Location = new System.Drawing.Point(347, 13);
            this.btnOpenShiftScheduler.Name = "btnOpenShiftScheduler";
            this.btnOpenShiftScheduler.Size = new System.Drawing.Size(150, 23);
            this.btnOpenShiftScheduler.TabIndex = 7;
            this.btnOpenShiftScheduler.Text = "Open Shift Scheduler";
            this.btnOpenShiftScheduler.UseVisualStyleBackColor = true;
            this.btnOpenShiftScheduler.Click += new System.EventHandler(this.btnOpenShiftScheduler_Click);
            // 
            // dataGridViewShifts
            // 
            this.dataGridViewShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShifts.Location = new System.Drawing.Point(13, 43);
            this.dataGridViewShifts.Name = "dataGridViewShifts";
            this.dataGridViewShifts.Size = new System.Drawing.Size(775, 150);
            this.dataGridViewShifts.TabIndex = 1;
            // 
            // dataGridViewStaff
            // 
            this.dataGridViewStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStaff.Location = new System.Drawing.Point(13, 200);
            this.dataGridViewStaff.Name = "dataGridViewStaff";
            this.dataGridViewStaff.Size = new System.Drawing.Size(775, 150);
            this.dataGridViewStaff.TabIndex = 2;
            // 
            // dataGridViewStores
            // 
            this.dataGridViewStores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStores.Location = new System.Drawing.Point(13, 357);
            this.dataGridViewStores.Name = "dataGridViewStores";
            this.dataGridViewStores.Size = new System.Drawing.Size(775, 150);
            this.dataGridViewStores.TabIndex = 3;
            // 
            // dataGridViewAdmins
            // 
            this.dataGridViewAdmins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAdmins.Location = new System.Drawing.Point(13, 514);
            this.dataGridViewAdmins.Name = "dataGridViewAdmins";
            this.dataGridViewAdmins.Size = new System.Drawing.Size(775, 150);
            this.dataGridViewAdmins.TabIndex = 4;
            // 
            // dataGridViewShiftRequests
            // 
            this.dataGridViewShiftRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShiftRequests.Location = new System.Drawing.Point(13, 670); // 新しいDataGridViewの位置
            this.dataGridViewShiftRequests.Name = "dataGridViewShiftRequests";
            this.dataGridViewShiftRequests.Size = new System.Drawing.Size(775, 150); // サイズを調整
            this.dataGridViewShiftRequests.TabIndex = 8; // 新しいDataGridViewのTabIndex
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 830); // フォームのサイズを調整
            this.Controls.Add(this.dataGridViewShiftRequests); // 新しいDataGridViewを追加
            this.Controls.Add(this.btnOpenShiftScheduler);
            this.Controls.Add(this.btnLoadShifts);
            this.Controls.Add(this.btnOpenDashboard);
            this.Controls.Add(this.dataGridViewAdmins);
            this.Controls.Add(this.dataGridViewStores);
            this.Controls.Add(this.dataGridViewStaff);
            this.Controls.Add(this.dataGridViewShifts);
            this.Controls.Add(this.btnLoadData);
            this.Name = "MainForm";
            this.Text = "Database Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdmins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShiftRequests)).EndInit(); // 新しいDataGridViewの初期化
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnOpenDashboard;
        private System.Windows.Forms.Button btnLoadShifts;
        private System.Windows.Forms.Button btnOpenShiftScheduler;
        private System.Windows.Forms.DataGridView dataGridViewShifts;
        private System.Windows.Forms.DataGridView dataGridViewStaff;
        private System.Windows.Forms.DataGridView dataGridViewStores;
        private System.Windows.Forms.DataGridView dataGridViewAdmins;
        private System.Windows.Forms.DataGridView dataGridViewShiftRequests; // 新しいDataGridViewのフィールド
    }
}
