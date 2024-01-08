using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCHoatDongCanLamSang_12122 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCHoatDongCanLamSang_12122()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "12122")
                celPhong.Text = "KHOA XÉT NGHIỆM";
        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            ngayth.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
        }


        internal void BindingData()
        {
            celND.DataBindings.Add("Text", DataSource, "NoiDung");
            cel1.DataBindings.Add("Text", DataSource, "kp1");
            cel2.DataBindings.Add("Text", DataSource, "kp2");
            cel3.DataBindings.Add("Text", DataSource, "kp3");
            cel4.DataBindings.Add("Text", DataSource, "kp4");
            cel5.DataBindings.Add("Text", DataSource, "kp5");
            cel6.DataBindings.Add("Text", DataSource, "kp6");
            cel7.DataBindings.Add("Text", DataSource, "kp7");
            cel8.DataBindings.Add("Text", DataSource, "kp8");
            cel9.DataBindings.Add("Text", DataSource, "kp9");
            celDV.DataBindings.Add("Text", DataSource, "DonVi");
            celTS.DataBindings.Add("Text", DataSource, "Tong");
        }
    }
}
