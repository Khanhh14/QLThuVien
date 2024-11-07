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
    public partial class NhaCungCap : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public NhaCungCap()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenNCC.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtDienThoai.Text))
            {
                MessageBox.Show("Không được để trống bất kỳ trường nào", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (mycon.State == ConnectionState.Closed)
                        mycon.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO NHACUNGCAP (TENNHACUNGCAP, DIACHI, DIENTHOAI) VALUES (@TENNHACUNGCAP, @DIACHI, @DIENTHOAI)", mycon);
                    cmd.Parameters.AddWithValue("@TENNHACUNGCAP", txtTenNCC.Text);
                    cmd.Parameters.AddWithValue("@DIACHI", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@DIENTHOAI", txtDienThoai.Text);

                    cmd.ExecuteNonQuery();
                    mycon.Close();

                    // Tải lại dữ liệu sau khi chèn
                    NhaCungCap_Load(sender, e);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void NhaCungCap_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                string sql = "select MANHACUNGCAP, TENNHACUNGCAP, DIACHI, DIENTHOAI from NHACUNGCAP  ";
                SqlDataAdapter ad = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvNhaCungCap.DataSource = dt;
                dgvNhaCungCap.AllowUserToAddRows = false;
                dgvNhaCungCap.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in NhaCungCap_Load: {ex.Message}");
            }
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvNhaCungCap.CurrentRow.Index;
            txtTenNCC.Text = dgvNhaCungCap.Rows[i].Cells[1].Value.ToString();
            txtDiaChi.Text = dgvNhaCungCap.Rows[i].Cells[2].Value.ToString();
            txtDienThoai.Text = dgvNhaCungCap.Rows[i].Cells[3].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                SqlCommand cmd = new SqlCommand("UPDATE NHACUNGCAP SET TENNHACUNGCAP=@TENNHACUNGCAP, DIACHI=@DIACHI, DIENTHOAI=@DIENTHOAI WHERE TENNHACUNGCAP=@TENNHACUNGCAP", mycon);
                cmd.Parameters.AddWithValue("@TENNHACUNGCAP", txtTenNCC.Text);
                cmd.Parameters.AddWithValue("@DIACHI", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@DIENTHOAI", txtDienThoai.Text);

                cmd.ExecuteNonQuery();
                mycon.Close();

                // Tải lại dữ liệu sau khi cập nhật
                NhaCungCap_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("DELETE FROM NHACUNGCAP WHERE TENNHACUNGCAP = @TenNCC", mycon);
                cmd.Parameters.AddWithValue("@TenNCC", txtTenNCC.Text);
                cmd.ExecuteNonQuery();

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();

                NhaCungCap_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM NHACUNGCAP WHERE TENNHACUNGCAP LIKE @TenNCC", mycon);
                cmd.Parameters.AddWithValue("@TenNCC", "%" + txtTimKiem.Text + "%");
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvNhaCungCap.DataSource = dt;

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
