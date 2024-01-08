using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV
{
    public partial class frm_GetLyDoLog : DevExpress.XtraEditors.XtraForm
    {
        string ThongBao = "";
        public frm_GetLyDoLog(string tb)
        {
            InitializeComponent();
            ThongBao = tb;
        }
        public delegate void _getdata(string a);
        public _getdata ok;
        private void frm_GetLyDoLog_Load(object sender, EventArgs e)
        {
            groupControl1.Text = ThongBao.ToString();
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(mmLyDo.Text))
            {
                ok(mmLyDo.Text);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Lý do bắt buộc phải nhập để xóa thanh toán bệnh nhân");
                mmLyDo.Focus();
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            ok("");
            this.Dispose();
        }
    }
}