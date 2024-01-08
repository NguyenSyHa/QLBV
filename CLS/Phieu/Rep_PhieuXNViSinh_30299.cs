 using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNViSinh_30299 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNViSinh_30299()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void lab113_BeforePrint(object sender, CancelEventArgs e)
        {            
        }

        internal void databinding()
        {
            celMaQD.DataBindings.Add("Text", DataSource, "MaQD");
            celTick.DataBindings.Add("Text", DataSource, "MaCC");
            celTenXN.DataBindings.Add("Text", DataSource, "TenDV");
            celKetQua.DataBindings.Add("Text", DataSource, "TenRG");
        }
    }
}
