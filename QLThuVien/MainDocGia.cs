using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThuVien
{
    public partial class MainDocGia : Form
    {
        private string madocgia;
        public MainDocGia(string madocgia)
        {
            InitializeComponent();
            this.madocgia = madocgia;
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoiMatKhau doi = new DoiMatKhau();
            doi.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DangNhap dang = new DangNhap();
            dang.Show();

            this.Hide();
        }

        private void xemSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XemSach xem = new XemSach();
            xem.Show();
        }

        private void xemSáchĐangMượnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SachDangMuon sachdm = new SachDangMuon(madocgia);
            sachdm.Show();
        }

        private void xemViPhạmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViPhamDC vi = new ViPhamDC(madocgia);
            vi.Show();
        }
    }
}
