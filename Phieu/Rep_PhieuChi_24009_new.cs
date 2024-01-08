using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuChi_24009_new : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuChi_24009_new()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel1.Text = xrLabel18.Text = DungChung.Bien.TenCQ;
        }

    }
}
