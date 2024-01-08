using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Linq;
using System.Data;

namespace QLBV.BaoCao
{
    public partial class rep_BkeThuTienVP_BG03_TH : DevExpress.XtraReports.UI.XtraReport
    {
        public static DateTime tungay;
        public static DateTime denngay;
        public rep_BkeThuTienVP_BG03_TH()
        {
            InitializeComponent();
         
        }       
        private void rep_BkeVienPhi_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }
        internal void BindingData()
        {
           // colSTT.DataBindings.Add("Text", DataSource, "STT");
            colTenKhoa.DataBindings.Add("Text", DataSource, "TenBNhan");
            colSBN.DataBindings.Add("Text", DataSource, "soBN");
            colSNDT.DataBindings.Add("Text", DataSource, "SoNgaydt");
            colTongSBN.DataBindings.Add("Text", DataSource, "soBN");
            colTongSNDT.DataBindings.Add("Text", DataSource, "SoNgaydt");
            colTenKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            coltn1.DataBindings.Add("Text", DataSource, "tn1").FormatString = "{0:##,###}";
            coltn2.DataBindings.Add("Text", DataSource, "tn2").FormatString = "{0:##,###}";
            coltn3.DataBindings.Add("Text", DataSource, "tn3").FormatString = "{0:##,###}";
            coltn4.DataBindings.Add("Text", DataSource, "tn4").FormatString = "{0:##,###}";
            coltn5.DataBindings.Add("Text", DataSource, "tn5").FormatString = "{0:##,###}";
            coltn6.DataBindings.Add("Text", DataSource, "tn6").FormatString = "{0:##,###}";
            coltn7.DataBindings.Add("Text", DataSource, "tn7").FormatString = "{0:##,###}";
            coltn8.DataBindings.Add("Text", DataSource, "tn8").FormatString = "{0:##,###}";
            coltn9.DataBindings.Add("Text", DataSource, "tn9").FormatString = "{0:##,###}";
            coltn10.DataBindings.Add("Text", DataSource, "tn10").FormatString = "{0:##,###}";
            coltn11.DataBindings.Add("Text", DataSource, "tn11").FormatString = "{0:##,###}";
            coltn12.DataBindings.Add("Text", DataSource, "tn12").FormatString = "{0:##,###}";
            coltn13.DataBindings.Add("Text", DataSource, "tn13").FormatString = "{0:##,###}";
            coltn14.DataBindings.Add("Text", DataSource, "tn14").FormatString = "{0:##,###}";
            coltn15.DataBindings.Add("Text", DataSource, "tn15").FormatString = "{0:##,###}";
            coltn16.DataBindings.Add("Text", DataSource, "tn16").FormatString = "{0:##,###}";
            coltn17.DataBindings.Add("Text", DataSource, "tn17").FormatString = "{0:##,###}";

            coltnR1.DataBindings.Add("Text", DataSource, "tn1").FormatString = "{0:##,###}";
            coltnR2.DataBindings.Add("Text", DataSource, "tn2").FormatString = "{0:##,###}";
            coltnR3.DataBindings.Add("Text", DataSource, "tn3").FormatString = "{0:##,###}";
            coltnR4.DataBindings.Add("Text", DataSource, "tn4").FormatString = "{0:##,###}";
            coltnR5.DataBindings.Add("Text", DataSource, "tn5").FormatString = "{0:##,###}";
            coltnR6.DataBindings.Add("Text", DataSource, "tn6").FormatString = "{0:##,###}";
            coltnR7.DataBindings.Add("Text", DataSource, "tn7").FormatString = "{0:##,###}";
            coltnR8.DataBindings.Add("Text", DataSource, "tn8").FormatString = "{0:##,###}";
            coltnR9.DataBindings.Add("Text", DataSource, "tn9").FormatString = "{0:##,###}";
            coltnR10.DataBindings.Add("Text", DataSource, "tn10").FormatString = "{0:##,###}";
            coltnR11.DataBindings.Add("Text", DataSource, "tn11").FormatString = "{0:##,###}";
            coltnR12.DataBindings.Add("Text", DataSource, "tn12").FormatString = "{0:##,###}";
            coltnR13.DataBindings.Add("Text", DataSource, "tn13").FormatString = "{0:##,###}";
            coltnR14.DataBindings.Add("Text", DataSource, "tn14").FormatString = "{0:##,###}";
            coltnR15.DataBindings.Add("Text", DataSource, "tn15").FormatString = "{0:##,###}";
            coltnR16.DataBindings.Add("Text", DataSource, "tn16").FormatString = "{0:##,###}";
            coltnR17.DataBindings.Add("Text", DataSource, "tn17").FormatString = "{0:##,###}"; 
            colTongSo.DataBindings.Add("Text", DataSource, "TongCong").FormatString = "{0:##,###}";
            colTong.DataBindings.Add("Text", DataSource, "TongCong").FormatString = "{0:##,###}";
           
         
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            Rep_301_sub repSub = (Rep_301_sub)xrSubreport1.ReportSource;
        }

      
    }
}
