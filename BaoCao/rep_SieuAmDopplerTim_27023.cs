using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SieuAmDopplerTim_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SieuAmDopplerTim_27023()
        {
            InitializeComponent();
        }

        private void rep_SieuAmDopplerTim_27023_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
