using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_DSBNPTTT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DSBNPTTT()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            celMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celMaThe.DataBindings.Add("Text", DataSource, "SThe");
            celNgayTH.DataBindings.Add("Text", DataSource, "NgayTH");
            celTenTT.DataBindings.Add("Text", DataSource, "TenDV");
            celLoaiTT.DataBindings.Add("Text", DataSource, "Loai");
            celBSTH.DataBindings.Add("Text", DataSource, "TenCB");
            celKhoaCD.DataBindings.Add("Text", DataSource, "TenKP");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            GroupHeader1.GroupFields.Add(new GroupField("MaDV"));
            //celDonGiaG.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[1];
            celSoLuong_G.DataBindings.Add("Text", DataSource, "SoLuong");
            celSoLuong_G.Summary.FormatString = DungChung.Bien.FormatString[0];
            celThanhTienG.DataBindings.Add("Text", DataSource, "ThanhTien");
            celThanhTienG.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ;
            txtcq.Text = DungChung.Bien.TenCQ;
        }

    }
}
