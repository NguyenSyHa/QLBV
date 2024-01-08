using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_CTNhapThuoc_VTYT_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_CTNhapThuoc_VTYT_30009()
        {
            InitializeComponent();
        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celDV.DataBindings.Add("Text", DataSource, "DonVi");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celSLTheoHD.DataBindings.Add("Text", DataSource, "SLNhapHD").FormatString = DungChung.Bien.FormatString[1];
            celTienTheoHD.DataBindings.Add("Text", DataSource, "TTNhapHD").FormatString = DungChung.Bien.FormatString[1];
            celSLBPT.DataBindings.Add("Text", DataSource, "SLNhapTra").FormatString = DungChung.Bien.FormatString[1];
            celTienTheoBPT.DataBindings.Add("Text", DataSource, "TTNhapTra").FormatString = DungChung.Bien.FormatString[1];
            celSLCK.DataBindings.Add("Text", DataSource, "SLNhapCK").FormatString = DungChung.Bien.FormatString[1];
            celTienCK.DataBindings.Add("Text", DataSource, "TTNhapCK").FormatString = DungChung.Bien.FormatString[1];
            celSLKhac.DataBindings.Add("Text", DataSource, "SLNhapKhac").FormatString = DungChung.Bien.FormatString[1];
            celTienKhac.DataBindings.Add("Text", DataSource, "TTNhapKhac").FormatString = DungChung.Bien.FormatString[1];
            celSLTong.DataBindings.Add("Text", DataSource, "SLTongNhap").FormatString = DungChung.Bien.FormatString[1];
            celTongTien.DataBindings.Add("Text", DataSource, "TTTongNhap").FormatString = DungChung.Bien.FormatString[1];
            lblMaThuoc.DataBindings.Add("Text", DataSource, "MaTam");
            celSLTheoHD_T.DataBindings.Add("Text", DataSource, "SLNhapHD");
            celTienTheoHD_T.DataBindings.Add("Text", DataSource, "TTNhapHD");
            celSLBPT_T.DataBindings.Add("Text", DataSource, "SLNhapTra");
            celTienBPT_T.DataBindings.Add("Text", DataSource, "TTNhapTra");
            celSLCK_T.DataBindings.Add("Text", DataSource, "SLNhapCK");
            celTienCK_T.DataBindings.Add("Text", DataSource, "TTNhapCK");
            celSLKhac_T.DataBindings.Add("Text", DataSource, "SLNhapKhac");
            celTienKhac_T.DataBindings.Add("Text", DataSource, "TTNhapKhac");
            celSLTong_T.DataBindings.Add("Text", DataSource, "SLTongNhap");
            celTongTien_T.DataBindings.Add("Text", DataSource, "TTTongNhap");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            cel_NguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            cel_KeToan.Text = DungChung.Bien.KeToanTruong;
            cel_ThuKho.Text = DungChung.Bien.ThuKho;
            cel_GiamDoc.Text = DungChung.Bien.GiamDoc;
            cel_TruongKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            lblNgayThang.Text = "..........., ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }
    }
}
