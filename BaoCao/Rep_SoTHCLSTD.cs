using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SoTHCLSTD : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoTHCLSTD()
        {
            InitializeComponent();
        }
        public void BinDingData()
        {
            celNgayTH.DataBindings.Add("Text", DataSource, "NgayTHCG");
            celHoTen.DataBindings.Add("Text", DataSource, "TenBN");
            celNam.DataBindings.Add("Text", DataSource, "Nam");
            celNu.DataBindings.Add("Text", DataSource, "Nu");
            celDChi.DataBindings.Add("Text", DataSource, "DChi");
            celBHYT.DataBindings.Add("Text", DataSource, "CoBH");
            celChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            celNoiGui.DataBindings.Add("Text", DataSource, "NoiGui");
            celYeuCau.DataBindings.Add("Text", DataSource, "Yeucau");
            //celKetQua.DataBindings.Add("Text", DataSource, "KetLuan");
            celNguoiDoc.DataBindings.Add("Text", DataSource, "NguoiTH");
            celCoPhim.DataBindings.Add("Text", DataSource, "CoPhim");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuongF");
            celKetQua.DataBindings.Add("Text", DataSource, "KLTH");
            celPhimHong.DataBindings.Add("Text", DataSource, "PhimHong");
            GroupHeader1.GroupFields.Add(new GroupField("NgayTH"));
            celNgayTHR.DataBindings.Add("Text", DataSource, "NgayTHT");
            celSoLuongR.DataBindings.Add("Text", DataSource, "SoLuongF");
            celPHR.DataBindings.Add("Text", DataSource, "PhimHong");
            celSLR.DataBindings.Add("Text", DataSource, "SoLuongF");
            celPhimHongR.DataBindings.Add("Text", DataSource, "PhimHong");
        }
    }
}
