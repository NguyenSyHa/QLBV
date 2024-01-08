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
    public partial class frm_ThuKSK : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        public frm_ThuKSK()
        {
            InitializeComponent();
        }
        public frm_ThuKSK(int mabn)
        {
            InitializeComponent();
            _mabn = mabn;
        }
        public delegate void _getstring(string noidung, int soto, int ketluan, double sotien);
        public _getstring GetData;
        private void frm_ThuKSK_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = (from kp in _dataContext.KPhongs.Where(p => p.PLoai== ("Phòng khám")) select new { kp.TenKP, kp.MaKP, kp.PLoai }).OrderBy(p => p.TenKP).ToList();
            lupBPTC.Properties.DataSource = q.ToList();
            var _tencb = _dataContext.CanBoes.OrderBy(p => p.TenCB).ToList();
            lupCBTC.Properties.DataSource = _tencb.ToList();
            var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan== _mabn).ToList();
            if(bn.Count>0){
                txtTenBN.Text = bn.First().TenBNhan;
                txtLoaihinhKSK.Text = bn.First().TChung;
                if (bn.First().CapCuu != null && bn.First().CapCuu.Value==1)
                    cboCapCuu.Text = "Có";
                else
                    cboCapCuu.Text = "Không";
            }
            if (_dataContext.TTboXungs.First(p => p.MaBNhan == _mabn).SoTo != null && DungChung.Bien.MaBV == "30002")
            {
                txtSoTo.Text = _dataContext.TTboXungs.First(p => p.MaBNhan == _mabn).SoTo.ToString();
                txtSoTo.Enabled = false;
            }
          
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            int soto = 0, ketluan = 0;
            double sotien = 0;
            string noidung = "";
            noidung = txtNoiDungTC.Text;
            if (!string.IsNullOrEmpty(txtSoTienTC.Text))
            {
                if (!string.IsNullOrEmpty(txtSoTo.Text))
                    soto = Convert.ToInt32(txtSoTo.Text);
                if (!string.IsNullOrEmpty(cboKLKSK.Text))
                    ketluan = cboKLKSK.SelectedIndex;
                sotien = Convert.ToDouble(txtSoTienTC.Text);
            }
            GetData(noidung, soto, ketluan, sotien);
            this.Dispose();
        }

        private void txtSoTienTC_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoTienTC.Text))
            {
                double stien = double.Parse(txtSoTienTC.Text);
                txtBangChuTC.Text = DungChung.Ham.DocTienBangChu(stien, " đồng.");
            }
        }
    }
}