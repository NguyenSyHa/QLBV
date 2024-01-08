using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class REP_BCRAVIENBNBHYT : DevExpress.XtraReports.UI.XtraReport
    {
        public REP_BCRAVIENBNBHYT()
        {
            InitializeComponent();
        }
        public void SonOC()
        {
            coltenkhoa.DataBindings.Add("Text", DataSource, "TenKP");
            colsobn.DataBindings.Add("Text", DataSource, "SoBN");
            coltongbn.DataBindings.Add("Text", DataSource, "SoBN");

            colthuocthuong.DataBindings.Add("Text", DataSource, "ThuocThuongTong").FormatString = DungChung.Bien.FormatString[1];
            coltongtt.DataBindings.Add("Text", DataSource, "ThuocThuongTong");

            colthuocdy.DataBindings.Add("Text", DataSource, "DongYTong").FormatString = DungChung.Bien.FormatString[1];
            coltongdy.DataBindings.Add("Text", DataSource, "DongYTong");

            coltong1.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            coltongtong1.DataBindings.Add("Text", DataSource, "Tong");

            coltrongbh.DataBindings.Add("Text", DataSource, "ThuocThuongBH").FormatString = DungChung.Bien.FormatString[1];
            coltongbh.DataBindings.Add("Text", DataSource, "ThuocThuongBH");

            colbn.DataBindings.Add("Text", DataSource, "ThuocThuongBN").FormatString = DungChung.Bien.FormatString[1];
            coltongtienbn.DataBindings.Add("Text", DataSource, "ThuocThuongBN");

            coltong2.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            coltongtong2.DataBindings.Add("Text", DataSource, "Tong");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        


    }
}
