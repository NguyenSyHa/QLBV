using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuXNTBH : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuXNTBH()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
        public void BindingData()
        {
            celTich.DataBindings.Add("Text", DataSource, "Tich");
            celMaQD.DataBindings.Add("Text", DataSource, "MaQD");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
        }
    }
}
