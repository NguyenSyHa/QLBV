using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_phieukhamCK_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_phieukhamCK_20001()
        {
            InitializeComponent();
        }

        private void rep_phieukhamCK_20001_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.gtinh.Value.ToString() == "1")
            {
                xrLabel4.Visible = false;
            }
            else
                xrLabel3.Visible = false;
        }

    }
}
