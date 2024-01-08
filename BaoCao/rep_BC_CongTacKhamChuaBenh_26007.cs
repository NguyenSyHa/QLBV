using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_CongTacKhamChuaBenh_26007 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_CongTacKhamChuaBenh_26007()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            celStt.DataBindings.Add("Text", DataSource, "Stt");
            celNoiDung.DataBindings.Add("Text", DataSource, "tieude");
            celDV.DataBindings.Add("Text", DataSource, "donvi");
            celKH.DataBindings.Add("Text", DataSource, "kehoach");
            celTH.DataBindings.Add("Text", DataSource, "thuchien").FormatString = DungChung.Bien.FormatString[1];
            celCong.DataBindings.Add("Text", DataSource, "congdon");
            celPtramKH.DataBindings.Add("Text", DataSource, "phantram");
        }
    }
}
