using QuanLyBanHang.DataAccessLayer;
using QuanLyBanHang.Reporting;
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
using QuanLyBanHang.Class;


namespace QuanLyBanHang
{
    public partial class frmBaoCaoDoanhThu : Form
    {
        DataTable tblds;
        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void frmBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            ResetValues();
            dgvDaonhThu.DataSource = null;

        }

        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtThang.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtThang.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "Select sum(TongTien) from tblHDBan where 1=1 ";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(NgayBan) Like N'%" + txtThang.Text + "%'";
            string sql2 = "select * from tblHDBan where Month(NgayBan) Like N'%" + txtThang.Text + "%'";
            tblds = Functions.GetDataToTable(sql);
           DataTable ktr = Functions.GetDataToTable(sql2);
            if (ktr.Rows.Count == 0)
            {
                MessageBox.Show("Không có đơn hàng nào trong tháng này!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          //  else
              //  MessageBox.Show("Có " + tblds.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDaonhThu.DataSource = tblds;
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            dgvDaonhThu.Columns[0].HeaderText = "Tổng doanh số tháng này:";
            dgvDaonhThu.Columns[0].Width = 180;
            dgvDaonhThu.AllowUserToAddRows = false;
            dgvDaonhThu.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dgvDaonhThu.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
