using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_DsBNMo_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_DsBNMo_27022()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            lblNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }

        public void BindingData()
        {
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celNgayMo.DataBindings.Add("Text", DataSource, "NgayMo");
            celPTChinh.DataBindings.Add("Text", DataSource, "CBPT");
            celPhu1.DataBindings.Add("Text", DataSource, "Phu1");
            celPhu2.DataBindings.Add("Text", DataSource, "Phu2");
            celGhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
        }
    }
}
