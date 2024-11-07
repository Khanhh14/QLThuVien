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
    public partial class ViPham : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public ViPham()
        {
            InitializeComponent();
        }

        private void dgvViPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViPham_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                string sql = "select MAVIPHAM, MADOCGIA, LYDOVP, HINHTHUCXULY from VIPHAM  ";
                SqlDataAdapter ad = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvViPham.DataSource = dt;
                dgvViPham.AllowUserToAddRows = false;
                dgvViPham.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ViPham_Load: {ex.Message}");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDG.Text) || string.IsNullOrEmpty(txtLDVP.Text) || string.IsNullOrEmpty(txtHTXL.Text))
            {
                MessageBox.Show("Không được để trống bất kỳ trường nào", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (mycon.State == ConnectionState.Closed)
                        mycon.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO VIPHAM (MADOCGIA, LYDOVP, HINHTHUCXULY) VALUES (@MADOCGIA, @LYDOVP, @HINHTHUCXULY)", mycon);
                    cmd.Parameters.AddWithValue("@MADOCGIA", txtMaDG.Text);
                    cmd.Parameters.AddWithValue("@LYDOVP", txtLDVP.Text);
                    cmd.Parameters.AddWithValue("@HINHTHUCXULY", txtHTXL.Text);

                    cmd.ExecuteNonQuery();
                    mycon.Close();

                    // Tải lại dữ liệu sau khi chèn
                    ViPham_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("UPDATE VIPHAM SET MADOCGIA=@MADOCGIA, LYDOVP=@LYDOVP, HINHTHUCXULY=@HINHTHUCXULY WHERE MADOCGIA=@MADOCGIA", mycon);
                cmd.Parameters.AddWithValue("@MADOCGIA", txtMaDG.Text);
                cmd.Parameters.AddWithValue("@LYDOVP", txtLDVP.Text);
                cmd.Parameters.AddWithValue("@HINHTHUCXULY", txtHTXL.Text);

                cmd.ExecuteNonQuery();
                mycon.Close();

                // Tải lại dữ liệu sau khi cập nhật
                ViPham_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("DELETE FROM VIPHAM WHERE MADOCGIA = @MaDG", mycon);
                cmd.Parameters.AddWithValue("@MaDG", txtMaDG.Text);
                cmd.ExecuteNonQuery();

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();

                ViPham_Load(sender, e);
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM VIPHAM WHERE MADOCGIA LIKE @MaDG", mycon);
                cmd.Parameters.AddWithValue("@MaDG", "%" + txtTimKiem.Text + "%");
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvViPham.DataSource = dt;

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();
            }
            catch
            {
                MessageBox.Show("Tìm kiếm không thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvViPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvViPham.CurrentRow.Index;
            txtMaDG.Text = dgvViPham.Rows[i].Cells[1].Value.ToString();
            txtLDVP.Text = dgvViPham.Rows[i].Cells[2].Value.ToString();
            txtHTXL.Text = dgvViPham.Rows[i].Cells[3].Value.ToString();
        }
    }
}