using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuPTThethuyTinh : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuPTThethuyTinh()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

    }
}
