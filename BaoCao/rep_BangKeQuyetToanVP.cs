using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BangKeQuyetToanVP : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BangKeQuyetToanVP()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
          //  celNgay.DataBindings.Add("Text", DataSource, "NgayTT").FormatString = "{0: dd/MM/yyyy}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celNSinhNam.DataBindings.Add("Text", DataSource, "NamSinhNam");
            celNSinhNu.DataBindings.Add("Text", DataSource, "NamSinhNu");
            celDiachi.DataBindings.Add("Text", DataSource, "DChi");
            celNgayVao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString = "{0:dd/MM/yyyy}";
            celNgayRa.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM/yyyy}";


            celSoNgayDT.DataBindings.Add("Text", DataSource, "SoNgaydt");

            celXN.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            celCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            celThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            celMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            celPT.DataBindings.Add("Text", DataSource, "PhauThuat").FormatString = DungChung.Bien.FormatString[1];
            celVTYT.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            celCongKham.DataBindings.Add("Text", DataSource, "Kham").FormatString = DungChung.Bien.FormatString[1];
            celTienGiuong.DataBindings.Add("Text", DataSource, "Giuong").FormatString = DungChung.Bien.FormatString[1];
            celKhac.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            celTongCong.DataBindings.Add("Text", DataSource, "Cong").FormatString = DungChung.Bien.FormatString[1];

            celSoNgayDT_R.DataBindings.Add("Text", DataSource, "SoNgaydt");
            celXN_R.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            celCDHA_R.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            celThuoc_R.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            celMau_R.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            celPT_R.DataBindings.Add("Text", DataSource, "PhauThuat").FormatString = DungChung.Bien.FormatString[1];
            celVTYT_R.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            celCongKham_R.DataBindings.Add("Text", DataSource, "Kham").FormatString = DungChung.Bien.FormatString[1];
            celTienGiuong_R.DataBindings.Add("Text", DataSource, "Giuong").FormatString = DungChung.Bien.FormatString[1];
            celKhac_R.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            celTongCong_R.DataBindings.Add("Text", DataSource, "Cong").FormatString = DungChung.Bien.FormatString[1];

            celXN_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celCDHA_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuoc_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celMau_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celPT_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celVTYT_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celCongKham_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienGiuong_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celKhac_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongCong_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            

            //celTong_R.DataBindings.Add("Text", DataSource, "NgoaiTruBH").FormatString = DungChung.Bien.FormatString[1];
           
            //celTienBN_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            //celTienBH_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            //celTong_R.Summary.FormatString = DungChung.Bien.FormatString[1];

           // GroupHeader1.GroupFields.Add(new GroupField("NgayTT"));

        }
    }
}
