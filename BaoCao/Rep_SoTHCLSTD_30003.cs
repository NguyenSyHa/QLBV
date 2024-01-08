using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SoTHCLSTD_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoTHCLSTD_30003()
        {
            InitializeComponent();
        }
        public void BinDingData()
        {
            
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
            cel13.DataBindings.Add("Text", DataSource, "Phim13");
            cel18.DataBindings.Add("Text", DataSource, "Phim18");
            celKetQua.DataBindings.Add("Text", DataSource, "KetQua");
            cel30.DataBindings.Add("Text", DataSource, "Phim30");
            cel24.DataBindings.Add("Text", DataSource, "Phim24");

        }
    }
}
