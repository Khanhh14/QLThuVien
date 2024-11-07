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
    public partial class DangNhap : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTaiKhoan.Text.Trim();
            string password = txtMatKhau.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản và mật khẩu.");
                return;
            }

            try
            {
                if (mycon.State != ConnectionState.Open)
                    mycon.Open();

                // Check login information in THUTHU table
                string sqlThuThu = "SELECT * FROM THUTHU WHERE USERNAME = @username AND PASSWORD = @password";
                using (SqlCommand cmdThuThu = new SqlCommand(sqlThuThu, mycon))
                {
                    cmdThuThu.Parameters.AddWithValue("@username", username);
                    cmdThuThu.Parameters.AddWithValue("@password", password);

                    SqlDataReader readerThuThu = cmdThuThu.ExecuteReader();
                    if (readerThuThu.Read())
                    {
                        readerThuThu.Close(); // Close reader before opening new form
                        MainThuThu frmThuThu = new MainThuThu();
                        frmThuThu.Show();
                        this.Hide();
                        return;
                    }
                    else
                    {
                        readerThuThu.Close(); // Close reader before next query

                        // Check login information in DOCGIA table if not found in THUTHU
                        string sqlDocGia = "SELECT * FROM DOCGIA WHERE USERNAME = @username AND PASSWORD = @password";
                        using (SqlCommand cmdDocGia = new SqlCommand(sqlDocGia, mycon))
                        {
                            cmdDocGia.Parameters.AddWithValue("@username", username);
                            cmdDocGia.Parameters.AddWithValue("@password", password);

                            SqlDataReader readerDocGia = cmdDocGia.ExecuteReader();
                            if (readerDocGia.Read())
                            {
                                string madocgia = readerDocGia["MADOCGIA"].ToString();
                                readerDocGia.Close(); // Close reader before opening new form

                                MainDocGia frmDocGia = new MainDocGia(madocgia);
                                frmDocGia.Show();
                                this.Hide();
                                return;
                            }
                            else
                            {
                                readerDocGia.Close(); // Close reader after reading

                                MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng.");
                                txtTaiKhoan.ResetText();
                                txtMatKhau.ResetText();
                                txtTaiKhoan.Focus();
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng nhập: {ex.Message}");
            }
            finally
            {
                mycon.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
