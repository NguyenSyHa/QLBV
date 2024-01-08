using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuThuChiTN : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuThuChiTN()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel1.Text = DungChung.Bien.TenCQCQ;
            xrLabel2.Text = DungChung.Bien.TenCQ;
        }

    }
}
