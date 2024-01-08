using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_NghiOm_TT56_01071_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_NghiOm_TT56_01071_A4()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV.Substring(0,2) == "30")
            {
                xrLabel1.Text = "Mẫu số: CT07";
                xrLabel41.Text = "Mẫu số: CT07";
                lbTenCQ.Text = "Liên số 1: Lưu";
                xrLabel10.Text = "Liên 2: Giao cho người lao động";
            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
        }

    }
}
