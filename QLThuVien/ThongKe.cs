using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace QLThuVien
{
    public partial class ThongKe : Form
    {
        // Chuỗi kết nối tới CSDL SQL Server
        private string connectionString = @"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603";

        public ThongKe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo kết nối và lấy dữ liệu từ CSDL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Thay đổi câu lệnh SQL để sắp xếp theo SOLUONGMUON từ cao đến thấp
                    SqlCommand cmd = new SqlCommand("SELECT * FROM SACH ORDER BY SOLUONGMUON DESC", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // Tạo DataSet DSThuVien
                    DataSet ds = new DataSet();

                    // Đổ dữ liệu từ SqlDataAdapter vào DataSet
                    da.Fill(ds, "SACH");

                    connection.Close();

                    // Tạo instance của Crystal Report
                    ReportDocument cryRpt = new ReportDocument();
                    cryRpt.Load(@"D:\DA2\QLThuVien\QLThuVien\CRSachNhieu.rpt");

                    // Gán DataSet cho Crystal Report
                    cryRpt.SetDataSource(ds);

                    // Tạo và hiển thị form BaoCao  
                    BaoCao baoCaoForm = new BaoCao();
                    baoCaoForm.ShowReport(cryRpt);
                    baoCaoForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            // Không cần xử lý gì khi form load
        }

        private void btnSachIt_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a connection and fetch data from the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Modify the SQL query to sort by SOLUONGMUON in ascending order
                    SqlCommand cmd = new SqlCommand("SELECT * FROM SACH ORDER BY SOLUONGMUON ASC", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // Create a DataSet DSThuVien
                    DataSet ds = new DataSet();

                    // Fill the DataSet with data from SqlDataAdapter
                    da.Fill(ds, "SACH");

                    connection.Close();

                    // Create an instance of Crystal Report
                    ReportDocument cryRpt = new ReportDocument();
                    cryRpt.Load(@"D:\DA2\QLThuVien\QLThuVien\CRSachIt.rpt");

                    // Assign the DataSet to Crystal Report
                    cryRpt.SetDataSource(ds);

                    // Create and display the report form
                    BaoCao baoCaoForm = new BaoCao();
                    baoCaoForm.ShowReport(cryRpt);
                    baoCaoForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDGNhieu_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo kết nối và lấy dữ liệu từ cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sửa đổi câu truy vấn SQL để lấy tất cả các trường cần thiết từ bảng DOCGIA và số lượng sách mượn
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT DG.MADOCGIA, DG.HOTEN, DG.GIOITINH, DG.LOP, DG.EMAIL, DG.DIENTHOAI, COUNT(CTMT.MASACH) AS SOLUONGMUON
                         FROM DOCGIA DG
                        JOIN CHITIETMUONTRA CTMT ON DG.MADOCGIA = CTMT.MADOCGIA
                         GROUP BY DG.MADOCGIA, DG.HOTEN, DG.GIOITINH, DG.LOP, DG.EMAIL, DG.DIENTHOAI
                         ORDER BY SOLUONGMUON DESC", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // Tạo DataSet DSThuVien
                    DataSet ds = new DataSet();

                    // Điền dữ liệu vào DataSet từ SqlDataAdapter
                    da.Fill(ds, "DOCGIA");

                    connection.Close();

                    // Debug: Hiển thị các bảng và cột trong DataSet
                    foreach (DataTable table in ds.Tables)
                    {
                        Console.WriteLine("Table: " + table.TableName);
                        foreach (DataColumn column in table.Columns)
                        {
                            Console.WriteLine("Column: " + column.ColumnName);
                        }
                    }

                    // Tạo một đối tượng Crystal Report
                    ReportDocument cryRpt = new ReportDocument();
                    cryRpt.Load(@"D:\DA2\QLThuVien\QLThuVien\CRDocGiaNhieu.rpt");

                    // Gán DataSet vào Crystal Report
                    cryRpt.SetDataSource(ds);

                    // Tạo và hiển thị form báo cáo
                    BaoCao baoCaoForm = new BaoCao();
                    baoCaoForm.ShowReport(cryRpt);
                    baoCaoForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDGIt_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo kết nối và lấy dữ liệu từ cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sửa đổi câu truy vấn SQL để lấy tất cả các trường cần thiết từ bảng DOCGIA và số lượng sách mượn
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT DG.MADOCGIA, DG.HOTEN, DG.GIOITINH, DG.LOP, DG.EMAIL, DG.DIENTHOAI, COUNT(CTMT.MASACH) AS SOLUONGMUON
                         FROM DOCGIA DG
                        JOIN CHITIETMUONTRA CTMT ON DG.MADOCGIA = CTMT.MADOCGIA
                         GROUP BY DG.MADOCGIA, DG.HOTEN, DG.GIOITINH, DG.LOP, DG.EMAIL, DG.DIENTHOAI
                         ORDER BY SOLUONGMUON ASC", connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // Tạo DataSet DSThuVien
                    DataSet ds = new DataSet();

                    // Điền dữ liệu vào DataSet từ SqlDataAdapter
                    da.Fill(ds, "DOCGIA");

                    connection.Close();

                    // Debug: Hiển thị các bảng và cột trong DataSet
                    foreach (DataTable table in ds.Tables)
                    {
                        Console.WriteLine("Table: " + table.TableName);
                        foreach (DataColumn column in table.Columns)
                        {
                            Console.WriteLine("Column: " + column.ColumnName);
                        }
                    }

                    // Tạo một đối tượng Crystal Report
                    ReportDocument cryRpt = new ReportDocument();
                    cryRpt.Load(@"D:\DA2\QLThuVien\QLThuVien\CRDocGiaIt.rpt");

                    // Gán DataSet vào Crystal Report
                    cryRpt.SetDataSource(ds);

                    // Tạo và hiển thị form báo cáo
                    BaoCao baoCaoForm = new BaoCao();
                    baoCaoForm.ShowReport(cryRpt);
                    baoCaoForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}