using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCCM_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCCM_27022()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            lblNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            celTieuDe.DataBindings.Add("Text", DataSource, "Title");
            celSoLieu.DataBindings.Add("Text", DataSource, "Quantity");
        }

    }
}
