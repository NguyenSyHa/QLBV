using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SoXN_Expert : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SoXN_Expert()
        {
            InitializeComponent();
        }


        internal void DataBinding()
        {
            celSoXN.DataBindings.Add("Text", DataSource, "soXN");
            celNgayNhanMau.DataBindings.Add("Text", DataSource, "NgayNhanMau").FormatString = "{0:dd/MM/yy}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoiNam.DataBindings.Add("Text", DataSource, "TuoiNam");
            celTuoiNu.DataBindings.Add("Text", DataSource, "TuoiNu");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celDviYeuCau.DataBindings.Add("Text", DataSource, "TenKP");
            celdtuong1.DataBindings.Add("Text", DataSource, "ChanDoanDaKhang");
            celDtuong2.DataBindings.Add("Text", DataSource, "ChanDoanLao");
            ChiDinh1.DataBindings.Add("Text", DataSource, "Lan1");
            ChiDinh2.DataBindings.Add("Text", DataSource, "LyDo");
            TinhTrangH.DataBindings.Add("Text", DataSource, "TinhTrangH");
            celBPDom.DataBindings.Add("Text", DataSource, "BPDom");
            celBPKhac.DataBindings.Add("Text", DataSource, "BPKhac");
            celTrangThaiBP.DataBindings.Add("Text", DataSource, "TrangThaiBP");
            celNgayLamXN.DataBindings.Add("Text", DataSource, "NgayTH").FormatString = "{0:dd/MM/yy}";
            celKQ1.DataBindings.Add("Text", DataSource, "KQ1");
            celKQ2.DataBindings.Add("Text", DataSource, "KQ2");
            celKQ3.DataBindings.Add("Text", DataSource, "KQ3");
            celKQ4.DataBindings.Add("Text", DataSource, "KQ4");
            celKQ5.DataBindings.Add("Text", DataSource, "KQ5");
            celNgayTraKQ.DataBindings.Add("Text", DataSource, "NgayTraKQ").FormatString = "{0:dd/MM/yy}";
            GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
           
        }
    }
}
