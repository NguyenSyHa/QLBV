using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXNHuyetHoc_TKA5 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXNHuyetHoc_TKA5()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colYCKT.DataBindings.Add("Text", DataSource, "TenDV");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            //colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            string _macb = "";
            if (BSCD.Value != null)
                _macb = BSCD.Value.ToString();
            var bsdt = (from bs in DataContect.CanBoes
                        where (bs.MaCB.Equals(_macb))
                        select new { bs.TenCB }).ToList();
            if (bsdt.Count > 0)
            {
                colTenBSDT.Text = bsdt.First().TenCB;
            }
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
        }

    }
}
