using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuPTTT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuPTTT()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV != "30007")
            {
                xrTable3.Visible = false;
                ReportFooter.Visible = true;
            }
            else
            {
                xrTable3.Visible = true;
                ReportFooter.Visible = false;
            }
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrLabel32.Visible = false;
                xrLabel31.Visible = false;
            }
        }

    }
}
