using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_VienPhiHangNgay_MauCT_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        private int dem;

        public rep_VienPhiHangNgay_MauCT_30005()
        {
            InitializeComponent();
        }

        public rep_VienPhiHangNgay_MauCT_30005(int dem)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.dem = dem;
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
            if (dem == 0)
            {
                celTN1.DataBindings.Add("Text", DataSource, "TienTN1").FormatString = DungChung.Bien.FormatString[1];
                celTN2.DataBindings.Add("Text", DataSource, "TienTN2").FormatString = DungChung.Bien.FormatString[1];
                celTN3.DataBindings.Add("Text", DataSource, "TienTN3").FormatString = DungChung.Bien.FormatString[1];
                celTN4.DataBindings.Add("Text", DataSource, "TienTN4").FormatString = DungChung.Bien.FormatString[1];
                celTN5.DataBindings.Add("Text", DataSource, "TienTN5").FormatString = DungChung.Bien.FormatString[1];
                celTN6.DataBindings.Add("Text", DataSource, "TienTN6").FormatString = DungChung.Bien.FormatString[1];
                celTN7.DataBindings.Add("Text", DataSource, "TienTN7").FormatString = DungChung.Bien.FormatString[1];
                celTN8.DataBindings.Add("Text", DataSource, "TienTN8").FormatString = DungChung.Bien.FormatString[1];
                celTN9.DataBindings.Add("Text", DataSource, "TienTN9").FormatString = DungChung.Bien.FormatString[1];
                celTN10.DataBindings.Add("Text", DataSource, "TienTN10").FormatString = DungChung.Bien.FormatString[1];
                celTN11.DataBindings.Add("Text", DataSource, "TienTN11").FormatString = DungChung.Bien.FormatString[1];
                celTN12.DataBindings.Add("Text", DataSource, "TienTN12").FormatString = DungChung.Bien.FormatString[1];
                celTN13.DataBindings.Add("Text", DataSource, "TienTN13").FormatString = DungChung.Bien.FormatString[1];
                celTN14.DataBindings.Add("Text", DataSource, "TienTN14").FormatString = DungChung.Bien.FormatString[1];
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
                celTN10_G.DataBindings.Add("Text", DataSource, "TienTN10").FormatString = DungChung.Bien.FormatString[1];
                celTN11_G.DataBindings.Add("Text", DataSource, "TienTN11").FormatString = DungChung.Bien.FormatString[1];
                celTN12_G.DataBindings.Add("Text", DataSource, "TienTN12").FormatString = DungChung.Bien.FormatString[1];
                celTN13_G.DataBindings.Add("Text", DataSource, "TienTN13").FormatString = DungChung.Bien.FormatString[1];
                celTN14_G.DataBindings.Add("Text", DataSource, "TienTN14").FormatString = DungChung.Bien.FormatString[1];
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
                celTN10_R.DataBindings.Add("Text", DataSource, "TienTN10").FormatString = DungChung.Bien.FormatString[1];
                celTN11_R.DataBindings.Add("Text", DataSource, "TienTN11").FormatString = DungChung.Bien.FormatString[1];
                celTN12_R.DataBindings.Add("Text", DataSource, "TienTN12").FormatString = DungChung.Bien.FormatString[1];
                celTN13_R.DataBindings.Add("Text", DataSource, "TienTN13").FormatString = DungChung.Bien.FormatString[1];
                celTN14_R.DataBindings.Add("Text", DataSource, "TienTN14").FormatString = DungChung.Bien.FormatString[1];
                celTong_R.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            }
            else if(dem == 1)
            {
                celTN1.DataBindings.Add("Text", DataSource, "TienTN15").FormatString = DungChung.Bien.FormatString[1];
                celTN2.DataBindings.Add("Text", DataSource, "TienTN16").FormatString = DungChung.Bien.FormatString[1];
                celTN3.DataBindings.Add("Text", DataSource, "TienTN17").FormatString = DungChung.Bien.FormatString[1];
                celTN4.DataBindings.Add("Text", DataSource, "TienTN18").FormatString = DungChung.Bien.FormatString[1];
                celTN5.DataBindings.Add("Text", DataSource, "TienTN19").FormatString = DungChung.Bien.FormatString[1];
                celTN6.DataBindings.Add("Text", DataSource, "TienTN20").FormatString = DungChung.Bien.FormatString[1];
                celTN7.DataBindings.Add("Text", DataSource, "TienTN21").FormatString = DungChung.Bien.FormatString[1];
                celTN8.DataBindings.Add("Text", DataSource, "TienTN22").FormatString = DungChung.Bien.FormatString[1];
                celTN9.DataBindings.Add("Text", DataSource, "TienTN23").FormatString = DungChung.Bien.FormatString[1];
                celTN10.DataBindings.Add("Text", DataSource, "TienTN24").FormatString = DungChung.Bien.FormatString[1];
                celTN11.DataBindings.Add("Text", DataSource, "TienTN25").FormatString = DungChung.Bien.FormatString[1];
                celTN12.DataBindings.Add("Text", DataSource, "TienTN26").FormatString = DungChung.Bien.FormatString[1];
                celTN13.DataBindings.Add("Text", DataSource, "TienTN27").FormatString = DungChung.Bien.FormatString[1];
                celTN14.DataBindings.Add("Text", DataSource, "TienTN28").FormatString = DungChung.Bien.FormatString[1];
                celTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

                celTN1_G.DataBindings.Add("Text", DataSource, "TienTN15").FormatString = DungChung.Bien.FormatString[1];
                celTN2_G.DataBindings.Add("Text", DataSource, "TienTN16").FormatString = DungChung.Bien.FormatString[1];
                celTN3_G.DataBindings.Add("Text", DataSource, "TienTN17").FormatString = DungChung.Bien.FormatString[1];
                celTN4_G.DataBindings.Add("Text", DataSource, "TienTN18").FormatString = DungChung.Bien.FormatString[1];
                celTN5_G.DataBindings.Add("Text", DataSource, "TienTN19").FormatString = DungChung.Bien.FormatString[1];
                celTN6_G.DataBindings.Add("Text", DataSource, "TienTN20").FormatString = DungChung.Bien.FormatString[1];
                celTN7_G.DataBindings.Add("Text", DataSource, "TienTN21").FormatString = DungChung.Bien.FormatString[1];
                celTN8_G.DataBindings.Add("Text", DataSource, "TienTN22").FormatString = DungChung.Bien.FormatString[1];
                celTN9_G.DataBindings.Add("Text", DataSource, "TienTN23").FormatString = DungChung.Bien.FormatString[1];
                celTN10_G.DataBindings.Add("Text", DataSource, "TienTN24").FormatString = DungChung.Bien.FormatString[1];
                celTN11_G.DataBindings.Add("Text", DataSource, "TienTN25").FormatString = DungChung.Bien.FormatString[1];
                celTN12_G.DataBindings.Add("Text", DataSource, "TienTN26").FormatString = DungChung.Bien.FormatString[1];
                celTN13_G.DataBindings.Add("Text", DataSource, "TienTN27").FormatString = DungChung.Bien.FormatString[1];
                celTN14_G.DataBindings.Add("Text", DataSource, "TienTN28").FormatString = DungChung.Bien.FormatString[1];
                celTong_G.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

                celTN1_R.DataBindings.Add("Text", DataSource, "TienTN15").FormatString = DungChung.Bien.FormatString[1];
                celTN2_R.DataBindings.Add("Text", DataSource, "TienTN16").FormatString = DungChung.Bien.FormatString[1];
                celTN3_R.DataBindings.Add("Text", DataSource, "TienTN17").FormatString = DungChung.Bien.FormatString[1];
                celTN4_R.DataBindings.Add("Text", DataSource, "TienTN18").FormatString = DungChung.Bien.FormatString[1];
                celTN5_R.DataBindings.Add("Text", DataSource, "TienTN19").FormatString = DungChung.Bien.FormatString[1];
                celTN6_R.DataBindings.Add("Text", DataSource, "TienTN20").FormatString = DungChung.Bien.FormatString[1];
                celTN7_R.DataBindings.Add("Text", DataSource, "TienTN21").FormatString = DungChung.Bien.FormatString[1];
                celTN8_R.DataBindings.Add("Text", DataSource, "TienTN22").FormatString = DungChung.Bien.FormatString[1];
                celTN9_R.DataBindings.Add("Text", DataSource, "TienTN23").FormatString = DungChung.Bien.FormatString[1];
                celTN10_R.DataBindings.Add("Text", DataSource, "TienTN24").FormatString = DungChung.Bien.FormatString[1];
                celTN11_R.DataBindings.Add("Text", DataSource, "TienTN25").FormatString = DungChung.Bien.FormatString[1];
                celTN12_R.DataBindings.Add("Text", DataSource, "TienTN26").FormatString = DungChung.Bien.FormatString[1];
                celTN13_R.DataBindings.Add("Text", DataSource, "TienTN27").FormatString = DungChung.Bien.FormatString[1];
                celTN14_R.DataBindings.Add("Text", DataSource, "TienTN28").FormatString = DungChung.Bien.FormatString[1];
                celTong_R.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            }
            else if(dem == 2)
            {
                celTN1.DataBindings.Add("Text", DataSource, "TienTN29").FormatString = DungChung.Bien.FormatString[1];
                celTN2.DataBindings.Add("Text", DataSource, "TienTN30").FormatString = DungChung.Bien.FormatString[1];
                celTN3.DataBindings.Add("Text", DataSource, "TienTN31").FormatString = DungChung.Bien.FormatString[1];
                celTN4.DataBindings.Add("Text", DataSource, "TienTN32").FormatString = DungChung.Bien.FormatString[1];
                celTN5.DataBindings.Add("Text", DataSource, "TienTN33").FormatString = DungChung.Bien.FormatString[1];
                celTN6.DataBindings.Add("Text", DataSource, "TienTN34").FormatString = DungChung.Bien.FormatString[1];
                celTN7.DataBindings.Add("Text", DataSource, "TienTN35").FormatString = DungChung.Bien.FormatString[1];
                celTN8.DataBindings.Add("Text", DataSource, "TienTN36").FormatString = DungChung.Bien.FormatString[1];
                celTN9.DataBindings.Add("Text", DataSource, "TienTN37").FormatString = DungChung.Bien.FormatString[1];
                celTN10.DataBindings.Add("Text", DataSource, "TienTN38").FormatString = DungChung.Bien.FormatString[1];
                celTN11.DataBindings.Add("Text", DataSource, "TienTN39").FormatString = DungChung.Bien.FormatString[1];
                celTN12.DataBindings.Add("Text", DataSource, "TienTN40").FormatString = DungChung.Bien.FormatString[1];
                celTN13.DataBindings.Add("Text", DataSource, "TienTN51").FormatString = DungChung.Bien.FormatString[1];
                celTN14.DataBindings.Add("Text", DataSource, "TienTN42").FormatString = DungChung.Bien.FormatString[1];
                celTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

                celTN1_G.DataBindings.Add("Text", DataSource, "TienTN29").FormatString = DungChung.Bien.FormatString[1];
                celTN2_G.DataBindings.Add("Text", DataSource, "TienTN30").FormatString = DungChung.Bien.FormatString[1];
                celTN3_G.DataBindings.Add("Text", DataSource, "TienTN31").FormatString = DungChung.Bien.FormatString[1];
                celTN4_G.DataBindings.Add("Text", DataSource, "TienTN32").FormatString = DungChung.Bien.FormatString[1];
                celTN5_G.DataBindings.Add("Text", DataSource, "TienTN33").FormatString = DungChung.Bien.FormatString[1];
                celTN6_G.DataBindings.Add("Text", DataSource, "TienTN34").FormatString = DungChung.Bien.FormatString[1];
                celTN7_G.DataBindings.Add("Text", DataSource, "TienTN35").FormatString = DungChung.Bien.FormatString[1];
                celTN8_G.DataBindings.Add("Text", DataSource, "TienTN36").FormatString = DungChung.Bien.FormatString[1];
                celTN9_G.DataBindings.Add("Text", DataSource, "TienTN37").FormatString = DungChung.Bien.FormatString[1];
                celTN10_G.DataBindings.Add("Text", DataSource, "TienTN38").FormatString = DungChung.Bien.FormatString[1];
                celTN11_G.DataBindings.Add("Text", DataSource, "TienTN39").FormatString = DungChung.Bien.FormatString[1];
                celTN12_G.DataBindings.Add("Text", DataSource, "TienTN40").FormatString = DungChung.Bien.FormatString[1];
                celTN13_G.DataBindings.Add("Text", DataSource, "TienTN41").FormatString = DungChung.Bien.FormatString[1];
                celTN14_G.DataBindings.Add("Text", DataSource, "TienTN42").FormatString = DungChung.Bien.FormatString[1];
                celTong_G.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

                celTN1_R.DataBindings.Add("Text", DataSource, "TienTN29").FormatString = DungChung.Bien.FormatString[1];
                celTN2_R.DataBindings.Add("Text", DataSource, "TienTN30").FormatString = DungChung.Bien.FormatString[1];
                celTN3_R.DataBindings.Add("Text", DataSource, "TienTN31").FormatString = DungChung.Bien.FormatString[1];
                celTN4_R.DataBindings.Add("Text", DataSource, "TienTN32").FormatString = DungChung.Bien.FormatString[1];
                celTN5_R.DataBindings.Add("Text", DataSource, "TienTN33").FormatString = DungChung.Bien.FormatString[1];
                celTN6_R.DataBindings.Add("Text", DataSource, "TienTN34").FormatString = DungChung.Bien.FormatString[1];
                celTN7_R.DataBindings.Add("Text", DataSource, "TienTN35").FormatString = DungChung.Bien.FormatString[1];
                celTN8_R.DataBindings.Add("Text", DataSource, "TienTN36").FormatString = DungChung.Bien.FormatString[1];
                celTN9_R.DataBindings.Add("Text", DataSource, "TienTN37").FormatString = DungChung.Bien.FormatString[1];
                celTN10_R.DataBindings.Add("Text", DataSource, "TienTN38").FormatString = DungChung.Bien.FormatString[1];
                celTN11_R.DataBindings.Add("Text", DataSource, "TienTN39").FormatString = DungChung.Bien.FormatString[1];
                celTN12_R.DataBindings.Add("Text", DataSource, "TienTN40").FormatString = DungChung.Bien.FormatString[1];
                celTN13_R.DataBindings.Add("Text", DataSource, "TienTN41").FormatString = DungChung.Bien.FormatString[1];
                celTN14_R.DataBindings.Add("Text", DataSource, "TienTN42").FormatString = DungChung.Bien.FormatString[1];
                celTong_R.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            }

            celTN1_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN2_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN3_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN4_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN5_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN6_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN7_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN8_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN9_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN10_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN11_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN12_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN13_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN14_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTong_G.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTN1_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN2_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN3_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN4_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN5_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN6_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN7_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN8_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN9_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN10_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN11_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN12_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN13_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTN14_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTong_R.Summary.FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("NgayTT"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celnguoilap.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
