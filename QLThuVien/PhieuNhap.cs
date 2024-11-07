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
    public partial class PhieuNhap : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public PhieuNhap()
        {
            InitializeComponent();
        }

        private void PhieuNhap_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                string sql = "select MAPHIEUNHAP, MANHACUNGCAP, MASACH, NGAYNHAP, SOLUONGNHAP from PHIEUNHAP  ";
                SqlDataAdapter ad = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvPhieuNhap.DataSource = dt;
                dgvPhieuNhap.AllowUserToAddRows = false;
                dgvPhieuNhap.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PhieuNhap_Load: {ex.Message}");
            }
        }

        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvPhieuNhap.CurrentRow.Index;
            txtMaNCC.Text = dgvPhieuNhap.Rows[i].Cells[1].Value.ToString();
            txtMaSach.Text = dgvPhieuNhap.Rows[i].Cells[2].Value.ToString();
            dateNgayNhap.Text = dgvPhieuNhap.Rows[i].Cells[3].Value.ToString();
            txtSLN.Text = dgvPhieuNhap.Rows[i].Cells[4].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text) || string.IsNullOrEmpty(txtMaSach.Text) || string.IsNullOrEmpty(dateNgayNhap.Text) || string.IsNullOrEmpty(txtSLN.Text))
            {
                MessageBox.Show("Không được để trống bất kỳ trường nào", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (mycon.State == ConnectionState.Closed)
                        mycon.Open();

                    // Bắt đầu giao dịch để đảm bảo tính nhất quán dữ liệu
                    SqlTransaction transaction = mycon.BeginTransaction();

                    try
                    {
                        // Thêm mới phiếu nhập
                        SqlCommand cmdInsertPhieuNhap = new SqlCommand("INSERT INTO PHIEUNHAP (MANHACUNGCAP, MASACH, NGAYNHAP, SOLUONGNHAP) VALUES (@MANHACUNGCAP, @MASACH, @NGAYNHAP, @SOLUONGNHAP)", mycon, transaction);
                        cmdInsertPhieuNhap.Parameters.AddWithValue("@MANHACUNGCAP", txtMaNCC.Text);
                        cmdInsertPhieuNhap.Parameters.AddWithValue("@MASACH", txtMaSach.Text);
                        cmdInsertPhieuNhap.Parameters.AddWithValue("@NGAYNHAP", dateNgayNhap.Text);
                        cmdInsertPhieuNhap.Parameters.AddWithValue("@SOLUONGNHAP", txtSLN.Text);

                        cmdInsertPhieuNhap.ExecuteNonQuery();

                        // Cập nhật số lượng tồn của sách
                        SqlCommand cmdUpdateSach = new SqlCommand("UPDATE SACH SET SOLUONGTON = SOLUONGTON + @SOLUONGNHAP WHERE MASACH = @MASACH", mycon, transaction);
                        cmdUpdateSach.Parameters.AddWithValue("@SOLUONGNHAP", txtSLN.Text);
                        cmdUpdateSach.Parameters.AddWithValue("@MASACH", txtMaSach.Text);

                        cmdUpdateSach.ExecuteNonQuery();

                        // Xác nhận giao dịch
                        transaction.Commit();

                        // Tải lại dữ liệu sau khi chèn và cập nhật
                        PhieuNhap_Load(sender, e);
                    }
                    catch (Exception e2)
                    {
                        // Nếu có lỗi, hủy giao dịch
                        transaction.Rollback();
                        MessageBox.Show(e2.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (mycon.State == ConnectionState.Open)
                        mycon.Close();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                SqlCommand cmd = new SqlCommand("UPDATE PHIEUNHAP SET MANHACUNGCAP=@MANHACUNGCAP, MASACH=@MASACH, NGAYNHAP=@NGAYNHAP, SOLUONGNHAP=@SOLUONGNHAP WHERE MANHACUNGCAP=@MANHACUNGCAP", mycon);
                cmd.Parameters.AddWithValue("@MANHACUNGCAP", txtMaNCC.Text);
                cmd.Parameters.AddWithValue("@MASACH", txtMaSach.Text);
                cmd.Parameters.AddWithValue("@NGAYNHAP", dateNgayNhap.Text);
                cmd.Parameters.AddWithValue("@SOLUONGNHAP", txtSLN.Text);

                cmd.ExecuteNonQuery();
                mycon.Close();

                // Tải lại dữ liệu sau khi cập nhật
                PhieuNhap_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("DELETE FROM PHIEUNHAP WHERE MANHACUNGCAP = @MaNCC", mycon);
                cmd.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text);
                cmd.ExecuteNonQuery();

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();

                PhieuNhap_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM PHIEUNHAP WHERE MASACH LIKE @MaSach", mycon);
                cmd.Parameters.AddWithValue("@MaSach", "%" + txtTimKiem.Text + "%");
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvPhieuNhap.DataSource = dt;

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
