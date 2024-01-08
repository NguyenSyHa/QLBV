using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuThuBG : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuThuBG()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel2.Text = DungChung.Bien.TenCQ.ToUpper();
            xrLabel21.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel22.Text = DungChung.Bien.TenCQ.ToUpper();
        }

    }
}
