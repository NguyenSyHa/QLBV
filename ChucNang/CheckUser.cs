using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class CheckUser : DevExpress.XtraEditors.XtraForm
    {

        public CheckUser(string td)
        {
            InitializeComponent();
            lab_TD.Text = td;
        }
        public delegate void _getdata(string user, string pass);
        public _getdata ok;

        private void CheckUser_Load(object sender, EventArgs e)
        {
            if (lab_TD.Text.Contains("BYT"))
            {
                txtTenDN.Text = DungChung.Bien.xmlFilePath_LIS[14];
                txtMatKhau.Text = DungChung.Bien.xmlFilePath_LIS[15];
            }
            else
            {
                txtTenDN.Text = DungChung.Bien.xmlFilePath_LIS[10];
                txtMatKhau.Text = DungChung.Bien.xmlFilePath_LIS[11];
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            ok(txtTenDN.Text, txtMatKhau.Text);
            this.Close();
        }
    }
}