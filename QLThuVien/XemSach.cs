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
    public partial class XemSach : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");

        public XemSach()
        {
            InitializeComponent();
        }
        private void LoadDLGridView()
        {
            try
            {
                string sql = @"
        SELECT 
            SACH.MASACH, 
            SACH.TENSACH, 
            SACH.TACGIA, 
            SACH.SOLUONGTON, 
            SACH.MOTA, 
            SACH.SOLUONGMUON, 
            NHAXUATBAN.TENNHAXUATBAN, 
            THELOAI.TENTHELOAI
        FROM 
            SACH
        JOIN 
            NHAXUATBAN ON SACH.MANHAXUATBAN = NHAXUATBAN.MANHAXUATBAN
        JOIN 
            THELOAI ON SACH.MATHELOAI = THELOAI.MATHELOAI";

                SqlDataAdapter ad = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvXemSach.DataSource = dt;

                dgvXemSach.Columns["SOLUONGMUON"].Visible = false;

                dgvXemSach.AllowUserToAddRows = false;
                dgvXemSach.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Sach_Load: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM SACH WHERE TENSACH LIKE @TenSach", mycon);
                cmd.Parameters.AddWithValue("@TenSach", "%" + txtTK.Text + "%");
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvXemSach.DataSource = dt;

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();
            }
            catch
            {
                MessageBox.Show("Tìm kiếm không thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XemSach_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
    }
}
