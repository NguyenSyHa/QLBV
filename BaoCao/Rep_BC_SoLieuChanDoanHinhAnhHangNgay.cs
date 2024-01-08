using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_SoLieuChanDoanHinhAnhHangNgay : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_SoLieuChanDoanHinhAnhHangNgay()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            cel_Ngay.DataBindings.Add("Text", DataSource, "Ngay");
            cel_SA_Tong.DataBindings.Add("Text", DataSource, "SA_Tong");
            cel_SA_Thuong.DataBindings.Add("Text", DataSource, "SA_Thuong");
            cel_XQ_Tong.DataBindings.Add("Text", DataSource, "XQ_Tong");
            cel_XQ_Thuong.DataBindings.Add("Text", DataSource, "XQ_Thuong");
            cel_SA_TimMach.DataBindings.Add("Text", DataSource, "SA_TimMach");
            cel_XQ_KTS.DataBindings.Add("Text", DataSource, "XQ_KTS");
            cel_XQ_TaiGiuong.DataBindings.Add("Text", DataSource, "XQ_TaiGiuong");
            cel_NoiSoi_Tong.DataBindings.Add("Text", DataSource, "NoiSoi_Tong");
            cel_NoiSoi_DaDay.DataBindings.Add("Text", DataSource, "NoiSoi_DaDay");
            cel_NoiSoi_DaiTrang.DataBindings.Add("Text", DataSource, "NoiSoi_DaiTrang");
            cel_DienTim.DataBindings.Add("Text", DataSource, "DienTim");

            gr_SA_Tong.DataBindings.Add("Text", DataSource, "SA_Tong");
            gr_SA_Thuong.DataBindings.Add("Text", DataSource, "SA_Thuong");
            gr_SA_TimMach.DataBindings.Add("Text", DataSource, "SA_TimMach");
            gr_XQ_Tong.DataBindings.Add("Text", DataSource, "XQ_Tong");
            gr_XQ_thuong.DataBindings.Add("Text", DataSource, "XQ_Thuong");
            gr_XQ_KTS.DataBindings.Add("Text", DataSource, "XQ_KTS");
            gr_XQ_TaiGiuong.DataBindings.Add("Text", DataSource, "XQ_TaiGiuong");
            gr_NoiSoi_Tong.DataBindings.Add("Text", DataSource, "NoiSoi_Tong");
            gr_NoiSoi_DaDay.DataBindings.Add("Text", DataSource, "NoiSoi_DaDay");
            gr_NoiSoi_DaiTrang.DataBindings.Add("Text", DataSource, "NoiSoi_DaiTrang");
            gr_DienTim.DataBindings.Add("Text", DataSource, "DienTim");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            lblTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

    }
}
