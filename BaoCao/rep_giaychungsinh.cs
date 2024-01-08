using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_GiayChungSinh : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_GiayChungSinh()
        {
            InitializeComponent();
        }
        public void databinding()
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ;
            txtcq.Text = DungChung.Bien.TenCQ;
        }
    }
}
