
namespace QLThuVien
{
    partial class ViPhamDC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dgvVPDC = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVPDC)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(192, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "VI PHẠM CỦA BẠN";
            // 
            // dgvVPDC
            // 
            this.dgvVPDC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVPDC.Location = new System.Drawing.Point(2, 148);
            this.dgvVPDC.Name = "dgvVPDC";
            this.dgvVPDC.RowHeadersWidth = 51;
            this.dgvVPDC.RowTemplate.Height = 24;
            this.dgvVPDC.Size = new System.Drawing.Size(602, 251);
            this.dgvVPDC.TabIndex = 1;
            // 
            // ViPhamDC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 399);
            this.Controls.Add(this.dgvVPDC);
            this.Controls.Add(this.label1);
            this.Name = "ViPhamDC";
            this.Text = "ViPhamDC";
            this.Load += new System.EventHandler(this.ViPhamDC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVPDC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvVPDC;
    }
}