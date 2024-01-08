using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SoSieuAmDopplerTim : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoSieuAmDopplerTim()
        {
            InitializeComponent();
        }
        public void BinDingData()
        {
            celMaBN.DataBindings.Add("Text", DataSource, "Mabn");
            celNgayTH.DataBindings.Add("Text", DataSource, "NgayTHCG");
            celHoTen.DataBindings.Add("Text", DataSource, "TenBN");
            celNam.DataBindings.Add("Text", DataSource, "Nam");
            celNu.DataBindings.Add("Text", DataSource, "Nu");
            celDChi.DataBindings.Add("Text", DataSource, "DChi");
            celBHYT.DataBindings.Add("Text", DataSource, "CoBH");
            celChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            celNoiGui.DataBindings.Add("Text", DataSource, "NoiGui");
            celYeuCau.DataBindings.Add("Text", DataSource, "Yeucau");
            celKetQua.DataBindings.Add("Text", DataSource, "TenBNhan");
            celNguoiDoc.DataBindings.Add("Text", DataSource, "NguoiTH");
            celKetQua.DataBindings.Add("Text", DataSource, "KetQua");
            celBSCDn.DataBindings.Add("Text", DataSource, "BSCD");

        }
    }
}
