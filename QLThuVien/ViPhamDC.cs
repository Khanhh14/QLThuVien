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
    public partial class ViPhamDC : Form
    {
        private string madocgia;
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public ViPhamDC(string madocgia)
        {
            InitializeComponent();
            this.madocgia = madocgia;
        }

        private void ViPhamDC_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                if (mycon.State != ConnectionState.Open)
                    mycon.Open();

                // Sử dụng MADOCGIA để lọc dữ liệu chỉ của độc giả đó
                string sql = "SELECT MAVIPHAM, MADOCGIA, LYDOVP, HINHTHUCXULY " +
                             "FROM VIPHAM " +
                             "WHERE MADOCGIA = @madocgia";

                SqlCommand cmd = new SqlCommand(sql, mycon);
                cmd.Parameters.AddWithValue("@madocgia", madocgia);

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                dgvVPDC.DataSource = dt;
                dgvVPDC.AllowUserToAddRows = false;
                dgvVPDC.EditMode = DataGridViewEditMode.EditProgrammatically;
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
