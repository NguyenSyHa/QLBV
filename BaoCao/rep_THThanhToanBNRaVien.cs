using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class 
        rep_THThanhToanBNRaVien : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_THThanhToanBNRaVien()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celNoiDung.DataBindings.Add("Text", DataSource, "TenKP");
            celTongSoBN.DataBindings.Add("Text", DataSource, "tsbn");
            celTamUngSL.DataBindings.Add("Text", DataSource, "TamUngSL");
            celTamUngTT.DataBindings.Add("Text", DataSource, "TamUngTT").FormatString = DungChung.Bien.FormatString[1];
            celThanhToanSL.DataBindings.Add("Text", DataSource, "ThanhToanSL");
            celThanhToanTT.DataBindings.Add("Text", DataSource, "ThanhToanTT").FormatString = DungChung.Bien.FormatString[1];           
            celThuThangTT.DataBindings.Add("Text", DataSource, "ThuThangTT").FormatString = DungChung.Bien.FormatString[1];
            celTienTraLaiBN.DataBindings.Add("Text", DataSource, "TienTraLaiBN").FormatString = DungChung.Bien.FormatString[1];
            celNopThem.DataBindings.Add("Text", DataSource, "TienNopThem").FormatString = DungChung.Bien.FormatString[1];

            celTongSoBN_T.DataBindings.Add("Text", DataSource, "tsbn");
            celTamUngSL_T.DataBindings.Add("Text", DataSource, "TamUngSL");
            celTamUngTT_T.DataBindings.Add("Text", DataSource, "TamUngTT").FormatString = DungChung.Bien.FormatString[1];
         
            celThuThangTT_T.DataBindings.Add("Text", DataSource, "ThuThangTT").FormatString = DungChung.Bien.FormatString[1];
            celThanhToanSL_T.DataBindings.Add("Text", DataSource, "ThanhToanSL");
            celThanhToanTT_T.DataBindings.Add("Text", DataSource, "ThanhToanTT").FormatString = DungChung.Bien.FormatString[1];
            celTienTraLaiBN_T.DataBindings.Add("Text", DataSource, "TienTraLaiBN").FormatString = DungChung.Bien.FormatString[1];
            celNopThem_T.DataBindings.Add("Text", DataSource, "TienNopThem").FormatString = DungChung.Bien.FormatString[1];

            celTamUngTT_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuThangTT_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThanhToanTT_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienTraLaiBN_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNopThem_T.Summary.FormatString = DungChung.Bien.FormatString[1];

          
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celKT.Text = DungChung.Bien.KeToanVP;
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celCQ.Text = DungChung.Bien.TenCQ;
        }
    }
}
