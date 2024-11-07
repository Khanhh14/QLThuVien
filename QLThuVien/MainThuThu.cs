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
    public partial class MainThuThu : Form
    {
        public MainThuThu()
        {
            InitializeComponent();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhaCungCap nhaCungCap = new NhaCungCap();
            nhaCungCap.Show();
        }

        private void nhàXuấtBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhaXuatBan nhaXuatBan = new NhaXuatBan();
            nhaXuatBan.Show();
        }

        private void quảnLýThểLoạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TheLoai the = new TheLoai();
            the.Show();
        }

        private void quảnLýSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sach sach = new Sach();
            sach.Show();
        }

        private void quảnLýĐộcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocGia doc = new DocGia();
            doc.Show();
        }

        private void phiếuNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhieuNhap phieu = new PhieuNhap();
            phieu.Show();
        }

        private void chiTiếtMượnTrảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChiTietMuonTra chitiet = new ChiTietMuonTra();
            chitiet.Show();
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKe thong = new ThongKe();
            thong.Show();
        }

        private void viPhạmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViPham vi = new ViPham();
            vi.Show();
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
    }
}
