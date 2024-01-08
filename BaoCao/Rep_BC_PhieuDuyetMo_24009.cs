using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_PhieuDuyetMo_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_PhieuDuyetMo_24009()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

    }
}
