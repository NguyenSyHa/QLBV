using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_VienPhiHangNgay_MauCT_30005_InDoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_VienPhiHangNgay_MauCT_30005_InDoc()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "NgayTT").FormatString = "{0: dd/MM/yyyy}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celDiachi.DataBindings.Add("Text", DataSource, "DChi");
            celSoPhieu.DataBindings.Add("Text", DataSource, "MaBNhan");

            celTN1.DataBindings.Add("Text", DataSource, "TienTN1").FormatString = DungChung.Bien.FormatString[1];
            celTN2.DataBindings.Add("Text", DataSource, "TienTN2").FormatString = DungChung.Bien.FormatString[1];
            celTN3.DataBindings.Add("Text", DataSource, "TienTN3").FormatString = DungChung.Bien.FormatString[1];
            celTN4.DataBindings.Add("Text", DataSource, "TienTN4").FormatString = DungChung.Bien.FormatString[1];
            celTN5.DataBindings.Add("Text", DataSource, "TienTN5").FormatString = DungChung.Bien.FormatString[1];
            celTN6.DataBindings.Add("Text", DataSource, "TienTN6").FormatString = DungChung.Bien.FormatString[1];
            celTN7.DataBindings.Add("Text", DataSource, "TienTN7").FormatString = DungChung.Bien.FormatString[1];
            celTN8.DataBindings.Add("Text", DataSource, "TienTN8").FormatString = DungChung.Bien.FormatString[1];
            celTN9.DataBindings.Add("Text", DataSource, "TienTN9").FormatString = DungChung.Bien.FormatString[1];

            celTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            celTN1_G.DataBindings.Add("Text", DataSource, "TienTN1").FormatString = DungChung.Bien.FormatString[1];
            celTN2_G.DataBindings.Add("Text", DataSource, "TienTN2").FormatString = DungChung.Bien.FormatString[1];
            celTN3_G.DataBindings.Add("Text", DataSource, "TienTN3").FormatString = DungChung.Bien.FormatString[1];
            celTN4_G.DataBindings.Add("Text", DataSource, "TienTN4").FormatString = DungChung.Bien.FormatString[1];
            celTN5_G.DataBindings.Add("Text", DataSource, "TienTN5").FormatString = DungChung.Bien.FormatString[1];
            celTN6_G.DataBindings.Add("Text", DataSource, "TienTN6").FormatString = DungChung.Bien.FormatString[1];
            celTN7_G.DataBindings.Add("Text", DataSource, "TienTN7").FormatString = DungChung.Bien.FormatString[1];
            celTN8_G.DataBindings.Add("Text", DataSource, "TienTN8").FormatString = DungChung.Bien.FormatString[1];
            celTN9_G.DataBindings.Add("Text", DataSource, "TienTN9").FormatString = DungChung.Bien.FormatString[1];

            celTong_G.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            celTN1_R.DataBindings.Add("Text", DataSource, "TienTN1").FormatString = DungChung.Bien.FormatString[1];
            celTN2_R.DataBindings.Add("Text", DataSource, "TienTN2").FormatString = DungChung.Bien.FormatString[1];
            celTN3_R.DataBindings.Add("Text", DataSource, "TienTN3").FormatString = DungChung.Bien.FormatString[1];
            celTN4_R.DataBindings.Add("Text", DataSource, "TienTN4").FormatString = DungChung.Bien.FormatString[1];
            celTN5_R.DataBindings.Add("Text", DataSource, "TienTN5").FormatString = DungChung.Bien.FormatString[1];
            celTN6_R.DataBindings.Add("Text", DataSource, "TienTN6").FormatString = DungChung.Bien.FormatString[1];
            celTN7_R.DataBindings.Add("Text", DataSource, "TienTN7").FormatString = DungChung.Bien.FormatString[1];
            celTN8_R.DataBindings.Add("Text", DataSource, "TienTN8").FormatString = DungChung.Bien.FormatString[1];
            celTN9_R.DataBindings.Add("Text", DataSource, "TienTN9").FormatString = DungChung.Bien.FormatString[1];

            celTong_R.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];


            GroupHeader1.GroupFields.Add(new GroupField("NgayTT"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celnguoilap.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
