using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCTHBienLaiVP_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCTHBienLaiVP_30003()
        {
            InitializeComponent();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
           
            celNgaythangnam.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            celGD.Text = DungChung.Bien.GiamDoc; 
            
        }


        internal void BindingData()
        {
            celKhoa.DataBindings.Add("Text", DataSource, "QuyenHD");
            celXQ58.DataBindings.Add("Text", DataSource, "XQ1").FormatString = DungChung.Bien.FormatString[1];
            celXQ83.DataBindings.Add("Text", DataSource, "XQ2").FormatString = DungChung.Bien.FormatString[1];
            celXQ121.DataBindings.Add("Text", DataSource, "XQ3").FormatString = DungChung.Bien.FormatString[1];
            celXQciti.DataBindings.Add("Text", DataSource, "XQ0").FormatString = DungChung.Bien.FormatString[1];
            celSieuAm.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
            celCong.DataBindings.Add("Text", DataSource, "Cong").FormatString = DungChung.Bien.FormatString[1];

            celXQ58T.DataBindings.Add("Text", DataSource, "XQ1").FormatString = DungChung.Bien.FormatString[1];
            celXQ83T.DataBindings.Add("Text", DataSource, "XQ2").FormatString = DungChung.Bien.FormatString[1];
            celXQ121T.DataBindings.Add("Text", DataSource, "XQ3").FormatString = DungChung.Bien.FormatString[1];
            celXQCitiT.DataBindings.Add("Text", DataSource, "XQ0").FormatString = DungChung.Bien.FormatString[1];
            celSAT.DataBindings.Add("Text", DataSource, "SA").FormatString = DungChung.Bien.FormatString[1];
            celCongT.DataBindings.Add("Text", DataSource, "Cong").FormatString = DungChung.Bien.FormatString[1];

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
