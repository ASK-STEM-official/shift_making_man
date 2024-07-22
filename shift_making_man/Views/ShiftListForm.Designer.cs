namespace shift_making_man.Views
{
    partial class ShiftListForm
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
            this.dataGridViewShifts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShifts)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewShifts
            // 
            this.dataGridViewShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShifts.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewShifts.Name = "dataGridViewShifts";
            this.dataGridViewShifts.Size = new System.Drawing.Size(776, 676);
            this.dataGridViewShifts.TabIndex = 0;
            // 
            // ShiftListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.dataGridViewShifts);
            this.Name = "ShiftListForm";
            this.Text = "Shift List";
            this.Load += new System.EventHandler(this.ShiftListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShifts)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridView dataGridViewShifts;
    }
}
