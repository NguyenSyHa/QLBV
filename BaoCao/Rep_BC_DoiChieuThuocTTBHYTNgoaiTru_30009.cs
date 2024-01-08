using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_DoiChieuThuocTTBHYTNgoaiTru_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_DoiChieuThuocTTBHYTNgoaiTru_30009()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayThangNam.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            celLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            celKeToan.Text = DungChung.Bien.KeToanTruong;
            celThuKho.Text = DungChung.Bien.ThuKho;
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        public void BindingData()
        {
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celDV.DataBindings.Add("Text", DataSource, "DonVi");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            celSLXuatNgTru.DataBindings.Add("Text", DataSource, "SoLuongNgTru").FormatString = DungChung.Bien.FormatString[1];
            celTienXuatNgTru.DataBindings.Add("Text", DataSource, "ThanhTienNgTru").FormatString = DungChung.Bien.FormatString[1];
            celSLXuatMau20.DataBindings.Add("Text", DataSource, "SoLuongMau20").FormatString = DungChung.Bien.FormatString[1];
            celTienXuatMau20.DataBindings.Add("Text", DataSource, "ThanhTienMau20").FormatString = DungChung.Bien.FormatString[1];
            celSLXuatDTNT.DataBindings.Add("Text", DataSource, "SLDTNT").FormatString = DungChung.Bien.FormatString[1];
            celTienXuatDTNT.DataBindings.Add("Text", DataSource, "TTDTNT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacSL.DataBindings.Add("Text", DataSource, "SLXuatKhac").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTT.DataBindings.Add("Text", DataSource, "TTXuatKhac").FormatString = DungChung.Bien.FormatString[1];

            celSLNgTru_T.DataBindings.Add("Text", DataSource, "SoLuongNgTru");
            celTienNgTru_T.DataBindings.Add("Text", DataSource, "ThanhTienNgTru");
            celSLMau20_T.DataBindings.Add("Text", DataSource, "SoLuongMau20");
            celTienMau20_T.DataBindings.Add("Text", DataSource, "ThanhTienMau20");
            celSLXuatDTNT_T.DataBindings.Add("Text", DataSource, "SLDTNT");
            celTienDTNT_T.DataBindings.Add("Text", DataSource, "TTDTNT");
            celSLXuatKhac_T.DataBindings.Add("Text", DataSource, "SLXuatKhac");
            celTTXuatKhac_T.DataBindings.Add("Text", DataSource, "TTXuatKhac");
        }
    }
}
