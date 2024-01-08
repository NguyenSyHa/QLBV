using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuKQXNNongDoConTrongMau : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuKQXNNongDoConTrongMau()
        {
            InitializeComponent();
        }
        public void databinding()
        {

        }

        private void rep_PhieuYCXNNongDoConTrongMau_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtcq.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
