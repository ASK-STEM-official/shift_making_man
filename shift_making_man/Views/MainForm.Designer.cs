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
            this.dataGridViewShifts = new System.Windows.Forms.DataGridView();
            this.dataGridViewStaff = new System.Windows.Forms.DataGridView();
            this.dataGridViewStores = new System.Windows.Forms.DataGridView();
            this.dataGridViewAdmins = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdmins)).BeginInit();
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 700);
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
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.DataGridView dataGridViewShifts;
        private System.Windows.Forms.DataGridView dataGridViewStaff;
        private System.Windows.Forms.DataGridView dataGridViewStores;
        private System.Windows.Forms.DataGridView dataGridViewAdmins;
    }
}
