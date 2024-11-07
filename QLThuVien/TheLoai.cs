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
    public partial class TheLoai : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public TheLoai()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                // Tạo SqlCommand để kiểm tra xem MATHELOAI có trong bảng SACH không
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM SACH WHERE MATHELOAI = (SELECT MATHELOAI FROM THELOAI WHERE TENTHELOAI = @TenTL)", mycon);
                checkCmd.Parameters.AddWithValue("@TenTL", txtTenTL.Text);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // Nếu MATHELOAI có trong bảng SACH, hiển thị thông báo lỗi
                    MessageBox.Show("Không thể xóa thể loại này vì đang có sách thuộc thể loại này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Nếu MATHELOAI không có trong bảng SACH, thực hiện xóa
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM THELOAI WHERE TENTHELOAI = @TenTL", mycon);
                    deleteCmd.Parameters.AddWithValue("@TenTL", txtTenTL.Text);
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();

                TheLoai_Load(sender, e);
            }
            catch
            {
                MessageBox.Show("Xóa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TheLoai_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                string sql = "select MATHELOAI, TENTHELOAI from THELOAI  ";
                SqlDataAdapter ad = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvTheLoai.DataSource = dt;
                dgvTheLoai.AllowUserToAddRows = false;
                dgvTheLoai.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TheLoai_Load: {ex.Message}");
            }
        }

        private void dgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvTheLoai.CurrentRow.Index;
            txtTenTL.Text = dgvTheLoai.Rows[i].Cells[1].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenTL.Text))
            {
                MessageBox.Show("Không được để trống bất kỳ trường nào", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (mycon.State == ConnectionState.Closed)
                        mycon.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO THELOAI (TENTHELOAI) VALUES (@TENTHELOAI)", mycon);
                    cmd.Parameters.AddWithValue("@TENTHELOAI", txtTenTL.Text);

                    cmd.ExecuteNonQuery();
                    mycon.Close();

                    // Tải lại dữ liệu sau khi chèn
                    TheLoai_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("UPDATE THELOAI SET TENTHELOAI=@TENTHELOAI WHERE TENTHELOAI=@TENTHELOAI", mycon);
                cmd.Parameters.AddWithValue("@TENTHELOAI", txtTenTL.Text);

                cmd.ExecuteNonQuery();
                mycon.Close();

                // Tải lại dữ liệu sau khi cập nhật
                TheLoai_Load(sender, e);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
