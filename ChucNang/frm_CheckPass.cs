using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.ChucNang
{
    public partial class frm_CheckPass : DevExpress.XtraEditors.XtraForm
    {
        string _macb = "";
        public frm_CheckPass()
        {
            InitializeComponent();

        }
        public frm_CheckPass(string macb)
        {
            InitializeComponent();
            _macb = macb;
        }
        int dem = 0;
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public delegate void _getdata(bool a);
        public _getdata ok;

        private void btnOK_Click(object sender, EventArgs e)
        {
            _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string matkhau = "";
            var mk = _dataContext.ADMINs.Where(p => p.MaCB == _macb && p.TenDN== DungChung.Bien.TenDN).Select(p => p.MatK).ToList();
            if (mk.Count > 0)
                matkhau = mk.First();
            if (dem <= 2)
            {
                if (QLBV_Library.QLBV_Ham.MaHoa(txtMatKhau.Text) == matkhau)
                {
                    ok(true);
                    this.Dispose();
                }
                else
                {
                    ok(false);
                }
            }
            dem++;
            if (dem > 2)
            {
                MessageBox.Show("Mật khẩu sai nhiều lần");
                this.Dispose();
            }
        }

        private void frm_CheckPass_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(_macb))
                _macb = DungChung.Bien.MaCB;
            var mk = _dataContext.ADMINs.Where(p => p.MaCB == _macb && p.TenDN == DungChung.Bien.TenDN).ToList();
            if (mk.Count > 0)
            {
                labelControl1.Text = "Nhập mật khẩu người dùng: " + mk.First().TenGoi;
            }
            else { labelControl1.Text = "Nhập mật khẩu xác nhận"; }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}