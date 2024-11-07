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
    public partial class DocGia : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public DocGia()
        {
            InitializeComponent();
        }

        private void DocGia_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                string sql = "select MADOCGIA, HOTEN, GIOITINH, LOP, EMAIL, DIENTHOAI, USERNAME, PASSWORD from DOCGIA";
                SqlDataAdapter ad = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvDocGia.DataSource = dt;

                // Ẩn các cột USERNAME và PASSWORD
                dgvDocGia.Columns["USERNAME"].Visible = false;
                dgvDocGia.Columns["PASSWORD"].Visible = false;

                dgvDocGia.AllowUserToAddRows = false;
                dgvDocGia.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DocGia_Load: {ex.Message}");
            }
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvDocGia.CurrentRow.Index;
            txtHoTen.Text = dgvDocGia.Rows[i].Cells[1].Value.ToString();
            cmbGioiTinh.Text = dgvDocGia.Rows[i].Cells[2].Value.ToString();
            txtLop.Text = dgvDocGia.Rows[i].Cells[3].Value.ToString();
            txtEmail.Text = dgvDocGia.Rows[i].Cells[4].Value.ToString();
            txtDienThoai.Text = dgvDocGia.Rows[i].Cells[5].Value.ToString();
            txtUser.Text = dgvDocGia.Rows[i].Cells[6].Value.ToString();
            txtPass.Text = dgvDocGia.Rows[i].Cells[7].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(cmbGioiTinh.Text) || string.IsNullOrEmpty(txtLop.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtDienThoai.Text) || string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Không được để trống bất kỳ trường nào", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (mycon.State == ConnectionState.Closed)
                        mycon.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO DOCGIA (HOTEN, GIOITINH, LOP, EMAIL, DIENTHOAI, USERNAME, PASSWORD) VALUES (@HOTEN, @GIOITINH, @LOP, @EMAIL, @DIENTHOAI, @USERNAME, @PASSWORD)", mycon);
                    cmd.Parameters.AddWithValue("@HOTEN", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@GIOITINH", cmbGioiTinh.Text);
                    cmd.Parameters.AddWithValue("@LOP", txtLop.Text);
                    cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DIENTHOAI", txtDienThoai.Text);
                    cmd.Parameters.AddWithValue("@USERNAME", txtUser.Text);
                    cmd.Parameters.AddWithValue("@PASSWORD", txtPass.Text);

                    cmd.ExecuteNonQuery();
                    mycon.Close();

                    // Tải lại dữ liệu sau khi chèn
                    DocGia_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("UPDATE DOCGIA SET HOTEN=@HOTEN, GIOITINH=@GIOITINH, LOP=@LOP, EMAIL=@EMAIL, DIENTHOAI=@DIENTHOAI, USERNAME=@USERNAME, PASSWORD=@PASSWORD WHERE HOTEN=@HOTEN", mycon);
                cmd.Parameters.AddWithValue("@HOTEN", txtHoTen.Text);
                cmd.Parameters.AddWithValue("GIOITINH", cmbGioiTinh.Text);
                cmd.Parameters.AddWithValue("@LOP", txtLop.Text);
                cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                cmd.Parameters.AddWithValue("@DIENTHOAI", txtDienThoai.Text);
                cmd.Parameters.AddWithValue("@USERNAME", txtUser.Text);
                cmd.Parameters.AddWithValue("@PASSWORD", txtPass.Text);

                cmd.ExecuteNonQuery();
                mycon.Close();

                // Tải lại dữ liệu sau khi cập nhật
                DocGia_Load(sender, e);
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

                // Kiểm tra sự tồn tại của MADOCGIA trong bảng CHITIETMUONTRA
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM CHITIETMUONTRA WHERE MADOCGIA = (SELECT MADOCGIA FROM DOCGIA WHERE HOTEN = @HoTen)", mycon);
                checkCmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    // Nếu MADOCGIA tồn tại trong CHITIETMUONTRA, không thực hiện xóa và hiển thị thông báo
                    MessageBox.Show("Độc giả này đang mượn sách, không thể xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Nếu MADOCGIA không tồn tại trong CHITIETMUONTRA, thực hiện lệnh xóa
                    SqlCommand cmd = new SqlCommand("DELETE FROM DOCGIA WHERE HOTEN = @HoTen", mycon);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();

                DocGia_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM DOCGIA WHERE HOTEN LIKE @HoTen", mycon);
                cmd.Parameters.AddWithValue("@HoTen", "%" + txtTimKiem.Text + "%");
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvDocGia.DataSource = dt;

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
