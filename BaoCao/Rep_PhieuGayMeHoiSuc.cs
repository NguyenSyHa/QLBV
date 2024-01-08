using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuGayMeHoiSuc : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuGayMeHoiSuc()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void xrTableRow1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                SubBand1.Visible = true;
            }
            else SubBand1.Visible = false;
        }
        
    }
}
