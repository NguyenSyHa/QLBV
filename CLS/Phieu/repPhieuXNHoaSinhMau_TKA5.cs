using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuXNHoaSinhMau_TKA5 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXNHoaSinhMau_TKA5()
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
           // colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            string macb = "";
            if (this.MaCB.Value != null)
                macb = this.MaCB.Value.ToString();
            var bsdt = (from bs in DataContect.CanBoes
                        where (bs.MaCB.Equals(macb))
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
