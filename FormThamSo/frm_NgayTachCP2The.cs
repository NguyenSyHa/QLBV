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
    public partial class frm_NgayTachCP2The : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0, _makp = 0;
        DateTime _ngaytt = DateTime.Now;
        public frm_NgayTachCP2The(int mabn,DateTime ngaytt)
        {
            InitializeComponent();
            _mabn = mabn;
            _ngaytt = ngaytt;
        }
        public delegate void getValue(DateTime ngaycp,bool TT2The);
        public getValue getdata;
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnOk_Click(object sender, EventArgs e)
        {
            getdata(DungChung.Ham.NgayDen(deNgayTachCP.DateTime), true);
            this.Close();
            //this.ClientSize = new System.Drawing.Size(384, 157);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            getdata(deNgayTachCP.DateTime, false);
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //bool ThanhToan = true;
            //if (DungChung.Ham.NgayDen(deNgayTachCP.DateTime) > _ngaytt)
            //{
            //    MessageBox.Show("Ngày tách chi phí không được lớn hơn thanh toán!");
            //    ThanhToan = false;
            //}
            //if (ThanhToan)
            //{
            //    getdata(DungChung.Ham.NgayDen(deNgayTachCP.DateTime), true);
            //    this.Close();
            //}
            getdata(DungChung.Ham.NgayDen(deNgayTachCP.DateTime), true);
            this.Close();
        }

        private void frm_NgayTachCP2The_Load(object sender, EventArgs e)
        {
            deNgayTachCP.DateTime = _ngaytt.Date; // bat ngay tach the < ngay bat dau the 2, k bat theo form nay nua
            this.ClientSize = new System.Drawing.Size(384, 90);
        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}