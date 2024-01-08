using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BangTHCongNo_12121 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BangTHCongNo_12121()
        {
            InitializeComponent();
        }

        private void xrTableCell3_BeforePrint(object sender, CancelEventArgs e)
        {


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            txtGiamDoc.Text = DungChung.Bien.GiamDoc;
            txtNgayThang.Text = "Ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " +  DateTime.Now.Year;
        }


        internal void BindingData()
        {
            celTenNhaCC.DataBindings.Add("Text", DataSource, "TenCC");
            celDuNoKyTruoc.DataBindings.Add("Text", DataSource, "TonDK").FormatString = DungChung.Bien.FormatString[1];
            celNhapTK.DataBindings.Add("Text", DataSource, "NhapTK").FormatString = DungChung.Bien.FormatString[1];
            celThanhtoanTK.DataBindings.Add("Text", DataSource, "TTTK").FormatString = DungChung.Bien.FormatString[1];
            celTonCK.DataBindings.Add("Text", DataSource, "TonCK").FormatString = DungChung.Bien.FormatString[1];
            celDuNoKyTruoc_T.DataBindings.Add("Text", DataSource, "TonDK").FormatString = DungChung.Bien.FormatString[1];
            celNhapTK_T.DataBindings.Add("Text", DataSource, "NhapTK").FormatString = DungChung.Bien.FormatString[1];
            celThanhtoanTK_T.DataBindings.Add("Text", DataSource, "TTTK").FormatString = DungChung.Bien.FormatString[1];
            celTonCK_T.DataBindings.Add("Text", DataSource, "TonCK").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if(DungChung.Bien.MaBV == "12121")
            celPhong.Text = "Phòng tài chính kế toán";
        }
    }
}
