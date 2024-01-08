using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BienLaiBG : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BienLaiBG()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel1.Text = DungChung.Bien.TenCQCQ;
            xrLabel2.Text = DungChung.Bien.TenCQ;
            xrLabel21.Text = DungChung.Bien.TenCQCQ;
            xrLabel22.Text = DungChung.Bien.TenCQ;
        }

    }
}
