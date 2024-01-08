using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.CLS
{
    public partial class frm_setBarcode : DevExpress.XtraEditors.XtraForm
    {
        int _mabn = 0;
        public frm_setBarcode(int mabn)
        {
            InitializeComponent();
            this._mabn = mabn;
        }

        private void frm_setBarcode_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qmnt = (from cls in _data.CLS.Where(p => p.MaBNhan == _mabn)
                        join cd in _data.ChiDinhs.Where(p => p.Status == 0) on cls.IdCLS equals cd.IdCLS
                        join kp in _data.KPhongs on cls.MaKP equals kp.MaKP
                        join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                        join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                        where (tn.TenRG.Contains("XN"))
                        select new { cls.IdCLS, tn.TenRG }).ToList();
            ckl_ChiDInh.DataSource = qmnt;
            ckl_ChiDInh.CheckAll();
        }
        int dem = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            dem++;
            if(dem==3)
            {
                btnOK_Click(sender, e);
               
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            frmIn frm = new frmIn();
            var cls = _data.CLS.Where(p => p.MaBNhan == _mabn).ToList();
            string barcode =DateTime.Now.ToString("ddMMHHmm")+ _mabn.ToString();
            string _inMauCD = "A5";
            if (DungChung.Bien.MaBV == "30009")
                _inMauCD = "A4";

           List<int> _lidCLS=new List<int>();
         
                for (int i = 0; i < ckl_ChiDInh.ItemCount; i++)
                {
                    if (ckl_ChiDInh.GetItemCheckState(i) == CheckState.Checked)
                        _lidCLS.Add(Convert.ToInt32(ckl_ChiDInh.GetItemValue(i)));
                }
                int idCLS = _lidCLS.FirstOrDefault() == null ? 0 : _lidCLS.FirstOrDefault();
                var barc = (from c in cls.Where(p=>p.Code==null || p.Code=="") join id in _lidCLS on c.IdCLS equals id select c).ToList();
                foreach (var item in cls)
                {
                    if (barc.Where(p => p.IdCLS == item.IdCLS).ToList().Count > 0)
                        item.Code = barcode;
                    _data.SaveChanges();
                }
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            CLS.InPhieu._setParamInChiDinh_XN(_data, frm, _inMauCD, _mabn, idCLS, true,  barcode);
            this.Dispose();
        }
    }
}