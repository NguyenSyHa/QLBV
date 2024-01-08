using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNKhac_VX04 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNKhac_VX04()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenXN.DataBindings.Add("Text", DataSource, "TenDVct");
            colTSBT.DataBindings.Add("Text", DataSource, "TSBT");
            colKetQua.DataBindings.Add("Text", DataSource, "KetQua");

        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            string _macbdt = MaBSCD.Value.ToString();
            int _makdt = KhoaBSCD.Value == null ? 0 : Convert.ToInt32(KhoaBSCD.Value);
             var qcb = (from bs in _data.CanBoes.Where(p => p.MaCB == _macbdt) select new { bs.TenCB }).ToList();
            if (qcb.Count > 0)
            {
                colBSCD.Text = qcb.First().TenCB;
            }
            var qk = (from kp in _data.KPhongs.Where(p => p.MaKP == _makdt) select new { kp.TenKP }).ToList();
            if (qk.Count > 0)
            {
                colKhoaDT.Text = qk.First().TenKP;
            }
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            string _mabsck = MaBSXN.Value.ToString();

            var qcb = (from bs in _data.CanBoes.Where(p => p.MaCB == _mabsck) select new { bs.TenCB }).ToList();
            if (qcb.Count > 0)
            {
                colTenTKXN.Text = qcb.First().TenCB.ToUpper();
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                //colBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
        }

    }
}
