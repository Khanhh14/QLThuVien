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
    public partial class NhaXuatBan : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public NhaXuatBan()
        {
            InitializeComponent();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenNXB.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtDienThoai.Text))
            {
                MessageBox.Show("Không được để trống bất kỳ trường nào", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (mycon.State == ConnectionState.Closed)
                        mycon.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO NHAXUATBAN (TENNHAXUATBAN, DIACHI, DIENTHOAI, EMAIL) VALUES (@TENNHACUNGCAP, @DIACHI, @DIENTHOAI, @EMAIL)", mycon);
                    cmd.Parameters.AddWithValue("@TENNHACUNGCAP", txtTenNXB.Text);
                    cmd.Parameters.AddWithValue("@DIACHI", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@DIENTHOAI", txtDienThoai.Text);
                    cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text);

                    cmd.ExecuteNonQuery();
                    mycon.Close();

                    // Tải lại dữ liệu sau khi chèn
                    NhaXuatBan_Load(sender, e);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void NhaXuatBan_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                string sql = "select MANHAXUATBAN, TENNHAXUATBAN, DIACHI, DIENTHOAI, EMAIL from NHAXUATBAN  ";
                SqlDataAdapter ad = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvNhaXuatBan.DataSource = dt;
                dgvNhaXuatBan.AllowUserToAddRows = false;
                dgvNhaXuatBan.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in NhaXuatBan_Load: {ex.Message}");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                SqlCommand cmd = new SqlCommand("UPDATE NHAXUATBAN SET TENNHAXUATBAN=@TENNHAXUATBAN, DIACHI=@DIACHI, DIENTHOAI=@DIENTHOAI WHERE TENNHAXUATBAN=@TENNHAXUATBAN", mycon);
                cmd.Parameters.AddWithValue("@TENNHAXUATBAN", txtTenNXB.Text);
                cmd.Parameters.AddWithValue("@DIACHI", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@DIENTHOAI", txtDienThoai.Text);
                cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text);

                cmd.ExecuteNonQuery();
                mycon.Close();

                // Tải lại dữ liệu sau khi cập nhật
                NhaXuatBan_Load(sender, e);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhaXuatBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvNhaXuatBan.CurrentRow.Index;
            txtTenNXB.Text = dgvNhaXuatBan.Rows[i].Cells[1].Value.ToString();
            txtDiaChi.Text = dgvNhaXuatBan.Rows[i].Cells[2].Value.ToString();
            txtDienThoai.Text = dgvNhaXuatBan.Rows[i].Cells[3].Value.ToString();
            txtEmail.Text = dgvNhaXuatBan.Rows[i].Cells[4].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                // Kiểm tra xem MANHAXUATBAN có trong bảng SACH hay không
                string checkQuery = "SELECT COUNT(*) FROM SACH WHERE MANHAXUATBAN = (SELECT MANHAXUATBAN FROM NHAXUATBAN WHERE TENNHAXUATBAN = @TenNXB)";
                SqlCommand checkCmd = new SqlCommand(checkQuery, mycon);
                checkCmd.Parameters.AddWithValue("@TenNXB", txtTenNXB.Text);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // Nếu MANHAXUATBAN có trong bảng SACH, không cho phép xóa
                    MessageBox.Show("Không thể xóa vì đang được sử dụng trong bảng sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Nếu MANHAXUATBAN không có trong bảng SACH, thực hiện xóa
                    SqlCommand cmd = new SqlCommand("DELETE FROM NHAXUATBAN WHERE TENNHAXUATBAN = @TenNXB", mycon);
                    cmd.Parameters.AddWithValue("@TenNXB", txtTenNXB.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();

                NhaXuatBan_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa không thành công: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM NHAXUATBAN WHERE TENNHAXUATBAN LIKE @TenNXB", mycon);
                cmd.Parameters.AddWithValue("@TenNXB", "%" + txtTimKiem.Text + "%");
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvNhaXuatBan.DataSource = dt;

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