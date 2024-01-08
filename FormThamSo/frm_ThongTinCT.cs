using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_ThongTinCT : DevExpress.XtraEditors.XtraForm
    {
        public frm_ThongTinCT()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public delegate void dsKyTen(string khoaduoc, string ketoan, string nguoinhan, string thutruong);
        public dsKyTen dsKT;
        private void btnIn_Click(object sender, EventArgs e)
        {
            this.Close();
            if (dsKT != null)
                dsKT(txtKhoaDuoc.Text, txtKeToan.Text, txtNguoiNhan.Text, txtThuTruong.Text);
        }

        private void frm_ThongTinCT_Load(object sender, EventArgs e)
        {
            txtKeToan.Text = "Nguyễn Đăng Dật";
            txtNguoiNhan.Text = "Ngô Thị Nga";
            txtKhoaDuoc.Text = "Đoàn Thị Nết";
        }
    }
}