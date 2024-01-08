using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_DThuoc_TuVan : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DThuoc_TuVan()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            celSL.DataBindings.Add("Text", DataSource, "SoLuong");
            celDV.DataBindings.Add("Text", DataSource, "DonVi");
            celCachDung.DataBindings.Add("Text", DataSource, "CachDung");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //celNgay.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }
    }
}
