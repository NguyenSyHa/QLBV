using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repPhieuXN : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXN()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
        }

        private void repPhieuXN_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30004")
            {
                colBSDT.Visible = false;
                colTKXN.Visible = false;
            }
        }

        private void xrLabel27_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30007")
            {
                xrLabel27.Text = "Bác Sỹ Cận Lâm Sàng".ToUpper();
            }
        }

    }
}
