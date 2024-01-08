using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TKDSBN : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TKDSBN()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;

        }
        public void BindingDaTa() {
            colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNamSinh.DataBindings.Add("Text", DataSource, "NamSinh");
            colSoThe.DataBindings.Add("Text", DataSource, "SThe");
            colMaCS.DataBindings.Add("Text", DataSource, "MaCS");
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

    }
}
