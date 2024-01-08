using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_NXTThuocXaTamDuong : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_NXTThuocXaTamDuong()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            lblTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            celTenTN.DataBindings.Add("Text", DataSource, "TieuNhomDV");        
            celTonDK_G.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTK_G.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            celTongNhap_G.DataBindings.Add("Text", DataSource, "TNhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatBH_G.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            celXuat139_G.DataBindings.Add("Text", DataSource, "Xuat139TT").FormatString = DungChung.Bien.FormatString[1];
            celXuatDieuChuyen_G.DataBindings.Add("Text", DataSource, "XuatLCTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatTE_G.DataBindings.Add("Text", DataSource, "XuatTETT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhac_G.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuat_G.DataBindings.Add("Text", DataSource, "XuatTTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCK_G.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            celTonDK_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapTK_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongNhap_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatBH_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuat139_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatDieuChuyen_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatTE_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatKhac_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongXuat_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTonCK_G.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celMa.DataBindings.Add("Text", DataSource, "MaTam");
            celDV.DataBindings.Add("Text", DataSource, "DVT");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            celTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            celTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            celTongNhapSL.DataBindings.Add("Text", DataSource, "TNhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            celTongNhapTT.DataBindings.Add("Text", DataSource, "TNhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatBHSL.DataBindings.Add("Text", DataSource, "XuatBHSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatBHTT.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            celXuat139SL.DataBindings.Add("Text", DataSource, "Xuat139SL").FormatString = DungChung.Bien.FormatString[0];
            celXuat139TT.DataBindings.Add("Text", DataSource, "Xuat139TT").FormatString = DungChung.Bien.FormatString[1];
            celXuatDieuChuyenSL.DataBindings.Add("Text", DataSource, "XuatLCSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatDieuChuyenTT.DataBindings.Add("Text", DataSource, "XuatLCTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatTESL.DataBindings.Add("Text", DataSource, "XuatTESL").FormatString = DungChung.Bien.FormatString[0];
            celXuatTETT.DataBindings.Add("Text", DataSource, "XuatTETT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacSL.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatKhacTT.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuatSL.DataBindings.Add("Text", DataSource, "XuatTSL").FormatString = DungChung.Bien.FormatString[0];
            celTongXuatTT.DataBindings.Add("Text", DataSource, "XuatTTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            celTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            celTonDK_T.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTK_T.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            celTongNhap_T.DataBindings.Add("Text", DataSource, "TNhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatBH_T.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            celXuat139_T.DataBindings.Add("Text", DataSource, "Xuat139TT").FormatString = DungChung.Bien.FormatString[1];
            celXuatDieuChuyen_T.DataBindings.Add("Text", DataSource, "XuatLCTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatTE_T.DataBindings.Add("Text", DataSource, "XuatTETT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhac_T.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuat_T.DataBindings.Add("Text", DataSource, "XuatTTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCK_T.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            celTonDK_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapTK_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongNhap_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatBH_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuat139_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatDieuChuyen_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatTE_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatKhac_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongXuat_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTonCK_T.Summary.FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TieuNhomDV"));
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            celTongTenTN.Text = "Tổng " + GetCurrentColumnValue("TieuNhomDV");
        }
    }
}
