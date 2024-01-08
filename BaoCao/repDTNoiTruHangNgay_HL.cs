using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repDTNoiTruHangNgay_HL : DevExpress.XtraReports.UI.XtraReport
    {
        public repDTNoiTruHangNgay_HL()
        {
            InitializeComponent();
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            repDTNoiTruHangNgay_HL01b rep = (repDTNoiTruHangNgay_HL01b)xrSubreport1.ReportSource;
         // rep.BindingData();
        }

    }
}
