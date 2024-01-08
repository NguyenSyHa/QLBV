using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rpt_PhiVCDuocMien : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_PhiVCDuocMien()
        {
            InitializeComponent();
        }


        internal void DataBinding()
        {
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celNamSinh.DataBindings.Add("Text", DataSource, "NamSinh");
            celMaThe.DataBindings.Add("Text", DataSource, "SThe");
            celDChi.DataBindings.Add("Text", DataSource, "DChi");
            celNgayVao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString = "{0:dd/MM/yyyy}";
            celNgayRa.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM/yyyy}";
            celKhoaDtri.DataBindings.Add("Text", DataSource, "TenKP");
            celSoNgayDT.DataBindings.Add("Text", DataSource, "SoNgaydt");
            celSoKm.DataBindings.Add("Text", DataSource, "SoLuong");
            celDongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celSoTien.DataBindings.Add("Text", DataSource, "TienMien").FormatString = DungChung.Bien.FormatString[1];
            celTong.DataBindings.Add("Text", DataSource, "TienMien").FormatString = DungChung.Bien.FormatString[1];
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            celKTT.Text = DungChung.Bien.KeToanTruong;
            celGD.Text = DungChung.Bien.GiamDoc;
            celDiadanh.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
        }
    }
}
