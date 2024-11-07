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
    public partial class SachDangMuon : Form
    {
        private string madocgia;
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public SachDangMuon(string madocgia)
        {
            InitializeComponent();
            this.madocgia = madocgia;
        }

        private void SachDangMuon_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                if (mycon.State != ConnectionState.Open)
                    mycon.Open();

                // Load data into DataGridView
                string sql = "SELECT MACHITIETMUONTRA, MADOCGIA, MASACH, SOLUONGMUON, GHICHU, NGAYMUON, HANTRA " +
                             "FROM CHITIETMUONTRA " +
                             "WHERE MADOCGIA = @madocgia";
                using (SqlCommand cmd = new SqlCommand(sql, mycon))
                {
                    cmd.Parameters.AddWithValue("@madocgia", madocgia);

                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);

                    dgvSachDM.DataSource = dt;
                    dgvSachDM.AllowUserToAddRows = false;
                    dgvSachDM.EditMode = DataGridViewEditMode.EditProgrammatically;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
            finally
            {
                mycon.Close();
            }
        }
    }
}
