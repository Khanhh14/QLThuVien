using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace QLThuVien
{
    public partial class Sach : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public Sach()
        {
            InitializeComponent();
        }

        private void Sach_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                string sql = "select MASACH, TENSACH, TACGIA, SOLUONGTON, MOTA, SOLUONGMUON, MANHAXUATBAN, MATHELOAI from SACH  ";
                SqlDataAdapter ad = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvSach.DataSource = dt;
                dgvSach.AllowUserToAddRows = false;
                dgvSach.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Sach_Load: {ex.Message}");
            }
        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvSach.CurrentRow.Index;
            txtTenSach.Text = dgvSach.Rows[i].Cells[1].Value.ToString();
            txtTacGia.Text = dgvSach.Rows[i].Cells[2].Value.ToString();
            txtSLT.Text = dgvSach.Rows[i].Cells[3].Value.ToString();
            txtMoTa.Text = dgvSach.Rows[i].Cells[4].Value.ToString();
            txtSLM.Text = dgvSach.Rows[i].Cells[5].Value.ToString();
            txtMaNXB.Text = dgvSach.Rows[i].Cells[6].Value.ToString();
            txtMaTL.Text = dgvSach.Rows[i].Cells[7].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenSach.Text) || string.IsNullOrEmpty(txtTacGia.Text) || string.IsNullOrEmpty(txtSLT.Text) || string.IsNullOrEmpty(txtMoTa.Text) || string.IsNullOrEmpty(txtSLM.Text) || string.IsNullOrEmpty(txtMaNXB.Text) || string.IsNullOrEmpty(txtMaTL.Text))
            {
                MessageBox.Show("Không được để trống bất kỳ trường nào", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (mycon.State == ConnectionState.Closed)
                        mycon.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO SACH (TENSACH, TACGIA, SOLUONGTON, MOTA, SOLUONGMUON, MANHAXUATBAN, MATHELOAI) VALUES (@TENSACH, @TACGIA, @SOLUONGTON, @MOTA, @SOLUONGMUON, @MANHAXUATBAN, @MATHELOAI)", mycon);
                    cmd.Parameters.AddWithValue("@TENSACH", txtTenSach.Text);
                    cmd.Parameters.AddWithValue("@TACGIA", txtTacGia.Text);
                    cmd.Parameters.AddWithValue("@SOLUONGTON", txtSLT.Text);
                    cmd.Parameters.AddWithValue("@MOTA", txtMoTa.Text);
                    cmd.Parameters.AddWithValue("@SOLUONGMUON", txtSLM.Text);
                    cmd.Parameters.AddWithValue("@MANHAXUATBAN", txtMaNXB.Text);
                    cmd.Parameters.AddWithValue("@MATHELOAI", txtMaTL.Text);

                    cmd.ExecuteNonQuery();
                    mycon.Close();

                    // Tải lại dữ liệu sau khi chèn
                    Sach_Load(sender, e);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                SqlCommand cmd = new SqlCommand("UPDATE SACH SET TENSACH=@TENSACH, TACGIA=@TACGIA, SOLUONGTON=@SOLUONGTON, MOTA=@MOTA, SOLUONGMUON=@SOLUONGMUON, MANHAXUATBAN=@MANHAXUATBAN, MATHELOAI=@MATHELOAI WHERE TENSACH=@TENSACH", mycon);
                cmd.Parameters.AddWithValue("@TENSACH", txtTenSach.Text);
                cmd.Parameters.AddWithValue("@TACGIA", txtTacGia.Text);
                cmd.Parameters.AddWithValue("@SOLUONGTON", txtSLT.Text);
                cmd.Parameters.AddWithValue("@MOTA", txtMoTa.Text);
                cmd.Parameters.AddWithValue("@SOLUONGMUON", txtSLM.Text);
                cmd.Parameters.AddWithValue("@MANHAXUATBAN", txtMaNXB.Text);
                cmd.Parameters.AddWithValue("@MATHELOAI", txtMaTL.Text);

                cmd.ExecuteNonQuery();
                mycon.Close();

                // Tải lại dữ liệu sau khi cập nhật
                Sach_Load(sender, e);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                // Kiểm tra số lượng mượn của sách theo tên sách
                SqlCommand checkCmd = new SqlCommand("SELECT SOLUONGMUON FROM SACH WHERE TENSACH = @TENSACH", mycon);
                checkCmd.Parameters.AddWithValue("@TENSACH", txtTenSach.Text);
                int soLuongMuon = (int)checkCmd.ExecuteScalar();

                if (soLuongMuon > 1)
                {
                    MessageBox.Show("Không thể xóa sách vì số sách đang được mượn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Thực hiện xóa sách nếu số lượng mượn không lớn hơn 1
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM SACH WHERE TENSACH = @TENSACH", mycon);
                    deleteCmd.Parameters.AddWithValue("@TENSACH", txtTenSach.Text);
                    deleteCmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();

                Sach_Load(sender, e);
            }
            catch
            {
                MessageBox.Show("Xóa không thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM SACH WHERE TENSACH LIKE @TenSach", mycon);
                cmd.Parameters.AddWithValue("@TenSach", "%" + txtTimKiem.Text + "%");
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvSach.DataSource = dt;

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();
            }
            catch
            {
                MessageBox.Show("Tìm kiếm không thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
