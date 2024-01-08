using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuThuTN : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuThuTN()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
    xrLabel1.Text = DungChung.Bien.TenCQCQ;
            xrLabel2.Text = DungChung.Bien.TenCQ;
            xrLabel25.Text = DungChung.Bien.TenCQCQ;
            xrLabel37.Text = DungChung.Bien.TenCQ;
        }

    }
}
