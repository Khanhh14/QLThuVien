
namespace QLThuVien
{
    partial class MainDocGia
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.thôngTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemSáchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemSáchĐangMượnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemViPhạmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thôngTinToolStripMenuItem,
            this.xemSáchToolStripMenuItem,
            this.xemSáchĐangMượnToolStripMenuItem,
            this.xemViPhạmToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(800, 28);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // thôngTinToolStripMenuItem
            // 
            this.thôngTinToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.đăngXuấtToolStripMenuItem});
            this.thôngTinToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thôngTinToolStripMenuItem.Name = "thôngTinToolStripMenuItem";
            this.thôngTinToolStripMenuItem.Size = new System.Drawing.Size(94, 24);
            this.thôngTinToolStripMenuItem.Text = "Thông Tin";
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng Xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // xemSáchToolStripMenuItem
            // 
            this.xemSáchToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xemSáchToolStripMenuItem.Name = "xemSáchToolStripMenuItem";
            this.xemSáchToolStripMenuItem.Size = new System.Drawing.Size(91, 24);
            this.xemSáchToolStripMenuItem.Text = "Xem Sách";
            this.xemSáchToolStripMenuItem.Click += new System.EventHandler(this.xemSáchToolStripMenuItem_Click);
            // 
            // xemSáchĐangMượnToolStripMenuItem
            // 
            this.xemSáchĐangMượnToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xemSáchĐangMượnToolStripMenuItem.Name = "xemSáchĐangMượnToolStripMenuItem";
            this.xemSáchĐangMượnToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.xemSáchĐangMượnToolStripMenuItem.Text = "Xem Sách Đang Mượn";
            this.xemSáchĐangMượnToolStripMenuItem.Click += new System.EventHandler(this.xemSáchĐangMượnToolStripMenuItem_Click);
            // 
            // xemViPhạmToolStripMenuItem
            // 
            this.xemViPhạmToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xemViPhạmToolStripMenuItem.Name = "xemViPhạmToolStripMenuItem";
            this.xemViPhạmToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.xemViPhạmToolStripMenuItem.Text = "Xem Vi Phạm ";
            this.xemViPhạmToolStripMenuItem.Click += new System.EventHandler(this.xemViPhạmToolStripMenuItem_Click);
            // 
            // MainDocGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::QLThuVien.Properties.Resources.dsd;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainDocGia";
            this.Text = "MainDocGia";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem thôngTinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemSáchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemSáchĐangMượnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemViPhạmToolStripMenuItem;
    }
}