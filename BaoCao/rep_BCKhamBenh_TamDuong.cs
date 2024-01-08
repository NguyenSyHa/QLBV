using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCKhamBenh_TamDuong : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCKhamBenh_TamDuong()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcq.Text = DungChung.Bien.TenCQ;
        }
    }
}
