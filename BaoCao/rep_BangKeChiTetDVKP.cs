using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BangKeChiTetDVKP : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BangKeChiTetDVKP()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celKhoa1.DataBindings.Add("Text", DataSource, "SLKP1").FormatString = DungChung.Bien.FormatString[0];
            celKhoa2.DataBindings.Add("Text", DataSource, "SLKP2").FormatString = DungChung.Bien.FormatString[0];
            celKhoa3.DataBindings.Add("Text", DataSource, "SLKP3").FormatString = DungChung.Bien.FormatString[0];
            celKhoa4.DataBindings.Add("Text", DataSource, "SLKP4").FormatString = DungChung.Bien.FormatString[0];
            celKhoa5.DataBindings.Add("Text", DataSource, "SLKP5").FormatString = DungChung.Bien.FormatString[0];
            celKhoa6.DataBindings.Add("Text", DataSource, "SLKP6").FormatString = DungChung.Bien.FormatString[0];
            celKhoa7.DataBindings.Add("Text", DataSource, "SLKP7").FormatString = DungChung.Bien.FormatString[0];
            celKhoa8.DataBindings.Add("Text", DataSource, "SLKP8").FormatString = DungChung.Bien.FormatString[0];
            celKhoa9.DataBindings.Add("Text", DataSource, "SLKP9").FormatString = DungChung.Bien.FormatString[0];
            celKhoa10.DataBindings.Add("Text", DataSource, "SLKP10").FormatString = DungChung.Bien.FormatString[0];
            celKhoa11.DataBindings.Add("Text", DataSource, "SLKP11").FormatString = DungChung.Bien.FormatString[0];
            celKhoa12.DataBindings.Add("Text", DataSource, "SLKP12").FormatString = DungChung.Bien.FormatString[0];
            celKhoa13.DataBindings.Add("Text", DataSource, "SLKP13").FormatString = DungChung.Bien.FormatString[0];
            celKhoa14.DataBindings.Add("Text", DataSource, "SLKP14").FormatString = DungChung.Bien.FormatString[0];
            celKhoa15.DataBindings.Add("Text", DataSource, "SLKP15").FormatString = DungChung.Bien.FormatString[0];
            TongCong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[0];
            ThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            clTongG.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcq.Text = DungChung.Bien.TenCQ;
            txtcqcq.Text = DungChung.Bien.TenCQCQ;
            if(DungChung.Bien.MaBV == "26007")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
            else
            {
                SubBand2.Visible = false;
                SubBand1.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30009")
                xrTable5.Visible = true;
        }
    }
}
