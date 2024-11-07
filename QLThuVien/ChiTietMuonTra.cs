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
using CrystalDecisions.CrystalReports.Engine;



namespace QLThuVien
{
    public partial class ChiTietMuonTra : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");

        public ChiTietMuonTra()
        {
            InitializeComponent();
        }

        private void ChiTietMuonTra_Load(object sender, EventArgs e)
        {
            LoadDLGridView();
        }
        private void LoadDLGridView()
        {
            try
            {
                string sql = "select MACHITIETMUONTRA, MADOCGIA, MASACH, SOLUONGMUON, GHICHU, NGAYMUON, HANTRA from CHITIETMUONTRA  ";
                SqlDataAdapter ad = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvMuonTra.DataSource = dt;
                dgvMuonTra.AllowUserToAddRows = false;
                dgvMuonTra.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Sach_Load: {ex.Message}");
            }
        }

        private void dgvMuonTra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvMuonTra.CurrentRow.Index;
            txtMaDG.Text = dgvMuonTra.Rows[i].Cells[1].Value.ToString();
            txtMaSach.Text = dgvMuonTra.Rows[i].Cells[2].Value.ToString();
            txtSLM.Text = dgvMuonTra.Rows[i].Cells[3].Value.ToString();
            txtGC.Text = dgvMuonTra.Rows[i].Cells[4].Value.ToString();
            dateNgayMuon.Text = dgvMuonTra.Rows[i].Cells[5].Value.ToString();
            dateHanTra.Text = dgvMuonTra.Rows[i].Cells[6].Value.ToString();
        }

        private void btnMuonSach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDG.Text) || string.IsNullOrEmpty(txtMaSach.Text) || string.IsNullOrEmpty(txtSLM.Text) || string.IsNullOrEmpty(txtGC.Text) || string.IsNullOrEmpty(dateNgayMuon.Text) || string.IsNullOrEmpty(dateHanTra.Text))
            {
                MessageBox.Show("Không được để trống bất kỳ trường nào", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (mycon.State == ConnectionState.Closed)
                        mycon.Open();

                    // Bắt đầu một transaction để đảm bảo tính nhất quán
                    using (SqlTransaction transaction = mycon.BeginTransaction())
                    {
                        try
                        {
                            // Kiểm tra số lần vi phạm của độc giả
                            SqlCommand cmdCheckVipham = new SqlCommand("SELECT COUNT(*) FROM VIPHAM WHERE MADOCGIA = @MADOCGIA", mycon, transaction);
                            cmdCheckVipham.Parameters.AddWithValue("@MADOCGIA", txtMaDG.Text);
                            int soLanViPham = (int)cmdCheckVipham.ExecuteScalar();

                            if (soLanViPham >= 2)
                            {
                                // Độc giả đã vi phạm nhiều lần
                                MessageBox.Show("Độc giả đã vi phạm nhiều lần, không được phép mượn sách", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                            }
                            else
                            {
                                // Kiểm tra số lượng tồn hiện tại của sách
                                SqlCommand cmdCheck = new SqlCommand("SELECT SOLUONGTON FROM SACH WHERE MASACH = @MASACH", mycon, transaction);
                                cmdCheck.Parameters.AddWithValue("@MASACH", txtMaSach.Text);
                                int soLuongTon = (int)cmdCheck.ExecuteScalar();

                                int soLuongMuon = int.Parse(txtSLM.Text);

                                if (soLuongTon <= 0)
                                {
                                    // Số lượng tồn không đủ để mượn
                                    MessageBox.Show("Sách đã hết hàng, không đủ để mượn", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    transaction.Rollback();
                                }
                                else if (soLuongTon < soLuongMuon)
                                {
                                    // Số lượng tồn không đủ để mượn
                                    MessageBox.Show("Số lượng tồn không đủ để mượn sách", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    transaction.Rollback();
                                }
                                else
                                {
                                    // Chèn dữ liệu vào bảng CHITIETMUONTRA
                                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO CHITIETMUONTRA (MADOCGIA, MASACH, SOLUONGMUON, GHICHU, NGAYMUON, HANTRA) VALUES (@MADOCGIA, @MASACH, @SOLUONGMUON, @GHICHU, @NGAYMUON, @HANTRA)", mycon, transaction);
                                    cmdInsert.Parameters.AddWithValue("@MADOCGIA", txtMaDG.Text);
                                    cmdInsert.Parameters.AddWithValue("@MASACH", txtMaSach.Text);
                                    cmdInsert.Parameters.AddWithValue("@SOLUONGMUON", soLuongMuon);
                                    cmdInsert.Parameters.AddWithValue("@GHICHU", txtGC.Text);
                                    cmdInsert.Parameters.AddWithValue("@NGAYMUON", dateNgayMuon.Text);
                                    cmdInsert.Parameters.AddWithValue("@HANTRA", dateHanTra.Text);
                                    cmdInsert.ExecuteNonQuery();

                                    // Cập nhật số lượng tồn và số lượng mượn trong bảng SACH
                                    SqlCommand cmdUpdate = new SqlCommand("UPDATE SACH SET SOLUONGTON = SOLUONGTON - @SOLUONGMUON, SOLUONGMUON = SOLUONGMUON + @SOLUONGMUON WHERE MASACH = @MASACH", mycon, transaction);
                                    cmdUpdate.Parameters.AddWithValue("@SOLUONGMUON", soLuongMuon);
                                    cmdUpdate.Parameters.AddWithValue("@MASACH", txtMaSach.Text);
                                    cmdUpdate.ExecuteNonQuery();

                                    // Commit transaction
                                    transaction.Commit();

                                    // Đóng kết nối
                                    mycon.Close();

                                    // Tải lại dữ liệu sau khi chèn
                                    ChiTietMuonTra_Load(sender, e);
                                }
                            }
                        }
                        catch (Exception e1)
                        {
                            // Rollback transaction nếu có lỗi xảy ra
                            transaction.Rollback();
                            MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                // Lấy giá trị số lượng mượn và ngày hạn trả
                SqlCommand getQuantityCmd = new SqlCommand(
                    "SELECT MASACH, SOLUONGMUON, HANTRA FROM CHITIETMUONTRA WHERE MADOCGIA = @MaDG", mycon);
                getQuantityCmd.Parameters.AddWithValue("@MaDG", txtMaDG.Text);
                SqlDataReader reader = getQuantityCmd.ExecuteReader();

                // Tạo một dictionary để lưu trữ các cặp MASACH và SOLUONGMUON
                Dictionary<string, Tuple<int, DateTime>> bookQuantities = new Dictionary<string, Tuple<int, DateTime>>();
                bool isOverdue = false;

                while (reader.Read())
                {
                    string maSach = reader["MASACH"].ToString();
                    int soLuongMuon = Convert.ToInt32(reader["SOLUONGMUON"]);
                    DateTime hanTra = Convert.ToDateTime(reader["HANTRA"]);

                    // Kiểm tra nếu ngày hạn trả đã vượt quá ngày hiện tại
                    if (hanTra < DateTime.Now)
                    {
                        isOverdue = true;
                    }

                    // Thử thêm vào Dictionary, nếu đã tồn tại thì cập nhật giá trị
                    if (bookQuantities.ContainsKey(maSach))
                    {
                        var existingValue = bookQuantities[maSach];
                        int updatedSoLuongMuon = existingValue.Item1 + soLuongMuon;
                        bookQuantities[maSach] = new Tuple<int, DateTime>(updatedSoLuongMuon, hanTra);
                    }
                    else
                    {
                        bookQuantities.Add(maSach, new Tuple<int, DateTime>(soLuongMuon, hanTra));
                    }
                }
                reader.Close();

                // Cập nhật SOLUONGMUON và SOLUONGTON trong bảng SACH
                foreach (var book in bookQuantities)
                {
                    SqlCommand updateSachCmd = new SqlCommand(
                        "UPDATE SACH SET SOLUONGMUON = SOLUONGMUON - @SoLuongMuon, SOLUONGTON = SOLUONGTON + @SoLuongMuon WHERE MASACH = @MaSach", mycon);
                    updateSachCmd.Parameters.AddWithValue("@SoLuongMuon", book.Value.Item1);
                    updateSachCmd.Parameters.AddWithValue("@MaSach", book.Key);
                    updateSachCmd.ExecuteNonQuery();
                }

                // Xóa từ bảng CHITIETMUONTRA
                SqlCommand deleteCmd = new SqlCommand(
                    "DELETE FROM CHITIETMUONTRA WHERE MADOCGIA = @MaDG", mycon);
                deleteCmd.Parameters.AddWithValue("@MaDG", txtMaDG.Text);
                deleteCmd.ExecuteNonQuery();

                // Chèn thông tin vào bảng VIPHAM nếu có trả sách quá hạn
                if (isOverdue)
                {
                    SqlCommand insertViolationCmd = new SqlCommand(
                        "INSERT INTO VIPHAM (MADOCGIA, LYDOVP, HINHTHUCXULY) VALUES (@MaDG, @LyDoVP, @HinhThucXuLy)", mycon);
                    insertViolationCmd.Parameters.AddWithValue("@MaDG", txtMaDG.Text);
                    insertViolationCmd.Parameters.AddWithValue("@LyDoVP", "Trả sách quá hạn");
                    insertViolationCmd.Parameters.AddWithValue("@HinhThucXuLy", "Xử lý theo quy định");
                    insertViolationCmd.ExecuteNonQuery();
                }

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();

                ChiTietMuonTra_Load(sender, e);

                // Hiển thị thông báo
                if (isOverdue)
                {
                    MessageBox.Show("Trả sách thành công nhưng bị vượt quá thời hạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Trả sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa không thành công: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                SqlCommand cmd = new SqlCommand("UPDATE CHITIETMUONTRA SET MADOCGIA=@MADOCGIA, MASACH=@MASACH, SOLUONGMUON=@SOLUONGMUON, GHICHU=@GHICHU, NGAYMUON=@NGAYMUON, HANTRA=@HANTRA WHERE MADOCGIA=@MADOCGIA", mycon);
                cmd.Parameters.AddWithValue("@MADOCGIA", txtMaDG.Text);
                cmd.Parameters.AddWithValue("@MASACH", txtMaSach.Text);
                cmd.Parameters.AddWithValue("@SOLUONGMUON", txtSLM.Text);
                cmd.Parameters.AddWithValue("@GHICHU", txtGC.Text);
                cmd.Parameters.AddWithValue("@NGAYMUON", dateNgayMuon.Text);
                cmd.Parameters.AddWithValue("@HANTRA", dateHanTra.Text);

                cmd.ExecuteNonQuery();
                mycon.Close();

                // Tải lại dữ liệu sau khi cập nhật
                ChiTietMuonTra_Load(sender, e);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mycon.State == ConnectionState.Closed)
                    mycon.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM CHITIETMUONTRA WHERE MADOCGIA LIKE @MaDG", mycon);
                cmd.Parameters.AddWithValue("@MaDG", "%" + txtTimKiem.Text + "%");
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvMuonTra.DataSource = dt;

                if (mycon.State == ConnectionState.Open)
                    mycon.Close();
            }
            catch
            {
                MessageBox.Show("Tìm kiếm không thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ TextBox txtChonMa
            string maChiTietMuonTra = txtChonMa.Text.Trim();

            if (string.IsNullOrEmpty(maChiTietMuonTra))
            {
                MessageBox.Show("Vui lòng nhập mã CHITIETMUONTRA.");
                return;
            }

            try
            {
                // Create a connection and fetch data from the database
                using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603"))
                {
                    connection.Open();

                    // Modify the SQL query to fetch data from CHITIETMUONTRA where MACHITIETMUONTRA = maChiTietMuonTra
                    SqlCommand cmd = new SqlCommand("SELECT MACHITIETMUONTRA, MADOCGIA, MASACH, SOLUONGMUON, GHICHU, NGAYMUON, HANTRA FROM CHITIETMUONTRA WHERE MACHITIETMUONTRA = @MACHITIETMUONTRA", connection);
                    cmd.Parameters.AddWithValue("@MACHITIETMUONTRA", maChiTietMuonTra);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // Create a DataSet DSThuVien
                    DataSet ds = new DataSet();

                    // Fill the DataSet with data from SqlDataAdapter
                    da.Fill(ds, "CHITIETMUONTRA");

                    connection.Close();

                    // Create an instance of Crystal Report
                    ReportDocument cryRpt = new ReportDocument();
                    cryRpt.Load(@"D:\DA2\QLThuVien\QLThuVien\CRMuon.rpt"); // Đường dẫn tới file Crystal Report của bạn

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
    }
}
