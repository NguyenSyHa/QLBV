using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCVienPhiPTramBHYT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCVienPhiPTramBHYT()
        {
            InitializeComponent();
        }
        internal void BindingData()
        {
            celtenkhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celsoluot.DataBindings.Add("Text", DataSource, "SoLuot");
            xrTableCell35.DataBindings.Add("Text", DataSource, "SoLuot");

            celthuocdich.DataBindings.Add("Text", DataSource, "ThuocThuong").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell36.DataBindings.Add("Text", DataSource, "ThuocThuong").FormatString = DungChung.Bien.FormatString[1];

            celthuocdy.DataBindings.Add("Text", DataSource, "ThuocDongY").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell37.DataBindings.Add("Text", DataSource, "ThuocDongY").FormatString = DungChung.Bien.FormatString[1];

            celvtyt.DataBindings.Add("Text", DataSource, "VatTu").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell38.DataBindings.Add("Text", DataSource, "VatTu").FormatString = DungChung.Bien.FormatString[1];

            celgiuong.DataBindings.Add("Text", DataSource, "GiuongBenh").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell39.DataBindings.Add("Text", DataSource, "GiuongBenh").FormatString = DungChung.Bien.FormatString[1];

            celkhambenh.DataBindings.Add("Text", DataSource, "KhamBenh").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell40.DataBindings.Add("Text", DataSource, "KhamBenh").FormatString = DungChung.Bien.FormatString[1];

            celxetnghiem.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell41.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];

            celxquang.DataBindings.Add("Text", DataSource, "XQuang").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell42.DataBindings.Add("Text", DataSource, "XQuang").FormatString = DungChung.Bien.FormatString[1];

            celsieuam.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell43.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];

            celdientim.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell44.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];

            celthuthuat.DataBindings.Add("Text", DataSource, "ThuThuat").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell45.DataBindings.Add("Text", DataSource, "ThuThuat").FormatString = DungChung.Bien.FormatString[1];

            CELKHAC.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            CELKHACTONG.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];

            celtong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell46.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            xrTableCell48.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            xrTableCell52.Text = DungChung.Bien.NguoiLapBieu;
            xrTableCell53.Text = DungChung.Bien.KeToanTruong;
            xrTableCell54.Text = DungChung.Bien.GiamDoc;
        }
    }
}
