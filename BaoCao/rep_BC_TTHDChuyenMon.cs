using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_TTHDChuyenMon : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_TTHDChuyenMon()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celCQCQ.Text = DungChung.Bien.TenCQCQ;
            celCQ.Text = DungChung.Bien.TenCQ;
        }

        public void BindingData()
        {
            celND.DataBindings.Add("Text", DataSource, "NoiDung");
            celKetQua.DataBindings.Add("Text", DataSource, "KetQua");
        }
    }
}
