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
    public partial class DoiMatKhau : Form
    {
        SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-FDIQ9791;Initial Catalog=QLThuVien;User ID=sa;Password=140603");
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {

        }
    }
}
