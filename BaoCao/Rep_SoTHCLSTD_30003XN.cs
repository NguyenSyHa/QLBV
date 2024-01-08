using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Text.RegularExpressions;

namespace QLBV.BaoCao
{
    public partial class Rep_SoTHCLSTD_30003XN : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SoTHCLSTD_30003XN()
        {
            InitializeComponent();
        }
        public void BinDingData()
        {

           
            
            cellNgayTH.DataBindings.Add("Text", DataSource, "NgayTHCG");
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
            celNguoiGui.DataBindings.Add("Text", DataSource, "CoPhim");
            //cel18.DataBindings.Add("Text", DataSource, "SoLuongF");
            celKetQua.DataBindings.Add("Text", DataSource, "KetQua");
            celNguoiGui.DataBindings.Add("Text", DataSource, "BSCD");
            //cel30.DataBindings.Add("Text", DataSource, "PhimHong");

          



        }                     
    }
    
}
