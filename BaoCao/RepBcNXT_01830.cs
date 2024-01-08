using System;using QLBV_Database;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace QLBV.BaoCao
{
    public partial class RepBcNXT_01830 : DevExpress.XtraReports.UI.XtraReport
    {
        public RepBcNXT_01830()
        {
            InitializeComponent();
            
        }
        bool _ht = true;
        bool HTTrongNgoaiNuoc = false;
        public RepBcNXT_01830(bool ht,bool nk)
        {
            InitializeComponent();
            _ht = ht;
            HTTrongNgoaiNuoc = nk;
        }
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand10.Visible = false;
                SubBand9.Visible = true;
                SubBand8.Visible = true;
                SubBand6.Visible = false;
                SubBand7.Visible = false;
                xrTableRow2.Visible = false;
                xrTableRow23.Visible = true;
                xrTableRow3.Visible = false;
                xrTableRow24.Visible = true;
                xrTableRow11.Visible = false;
                xrTableRow25.Visible = true;
                xrTableRow6.Visible = false;
                xrTableRow26.Visible = true;
                colMaNoiBo1.DataBindings.Add("Text", DataSource, "MaTam");
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                //colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
                colTenHamLuong1.DataBindings.Add("Text", DataSource, "TenHamLuong");
                handung.DataBindings.Add("Text", DataSource, "HanDung");
                colDVT1.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia1.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                solo.DataBindings.Add("Text", DataSource, "SoLo");
                colTonDKTT1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKSL1.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonDKTTTong1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf11.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf22.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf33.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf33.Summary.FormatString = DungChung.Bien.FormatString[1];


                colNhapTKSL1.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                colNhapTKTTde1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf11.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf22.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf33.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf33.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKNoiTruSL1.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKNoiTruTT1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf11.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf22.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf33.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf33.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatKhacSL1.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatKhacTT1.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF11.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF22.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF33.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF1.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF33.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF1.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKNgoaiTruSL1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKNgoaiTruTT1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf11.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf22.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf33.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf33.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKTongSL1.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf11.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh22.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh33.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh33.Summary.FormatString = DungChung.Bien.FormatString[1];

                colTonCKTT1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[0];
                colTonCKSL1.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf11.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf22.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf33.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf33.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else if(DungChung.Bien.MaBV == "30004")
            {
                xrTable28.Visible = true;
                xrTable6.Visible = false;
                xrTable23.Visible = false;
                xrTable12.Visible = false;
                xrTable27.Visible = true;
                xrTable22.Visible = false;
                xrTable2.Visible = false;
                xrTable25.Visible = true;
                xrTable20.Visible = false;
                xrTable21.Visible = false;
                xrTable3.Visible = false;
                xrTable26.Visible = true;
                SubBand10.Visible = true;
                SubBand9.Visible = false;
                SubBand8.Visible = false;
                SubBand6.Visible = false;
                sub_PH_30004.Visible = true;
                SubBand7.Visible = false;
                xrTableRow2.Visible = true;
                xrTableRow23.Visible = false;
                xrTableRow3.Visible = true;
                xrTableRow24.Visible = false;
                xrTableRow11.Visible = true;
                xrTableRow25.Visible = false;
                xrTableRow6.Visible = true;
                xrTableRow26.Visible = false;
                colMaNoiBo_30004.DataBindings.Add("Text", DataSource, "MaTam");
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                colTenHamLuong_30004.DataBindings.Add("Text", DataSource, "TenHamLuong");
                col_TenHC.DataBindings.Add("Text", DataSource, "TenHC");
                col_NongDo.DataBindings.Add("Text", DataSource, "HamLuong");

                colDVT_30004.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia_30004.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

                colTonDKTT_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKSL_30004.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonDKTTTong_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];


                colNhapTKSL_30004.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                colNhapTKTTde_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKNoiTruSL_30004.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKNoiTruTT_30004.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1_30004.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2_30004.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3_30004.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong_30004.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatKhacSL_30004.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatKhacTT_30004.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1_30004.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2_30004.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3_30004.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF_30004.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF_30004.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKNgoaiTruSL_30004.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKNgoaiTruTT_30004.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1_30004.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2_30004.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3_30004.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong_30004.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKTongSL_30004.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];

                colTonCKSL_30004.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonCKTT_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf1_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf2_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf3_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
            }    
            else
            {
                SubBand10.Visible = true;
                SubBand9.Visible = false;
                SubBand8.Visible = false;
                SubBand6.Visible = true;
                SubBand7.Visible = false;
                xrTableRow2.Visible = true;
                xrTableRow23.Visible = false;
                xrTableRow3.Visible = true;
                xrTableRow24.Visible = false;
                xrTableRow11.Visible = true;
                xrTableRow25.Visible = false;
                xrTableRow6.Visible = true;
                xrTableRow26.Visible = false;
                colMaNoiBo.DataBindings.Add("Text", DataSource, "MaTam");
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                //colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
                colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

                colDVT.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

                colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];


                colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                colNhapTKTTde.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKNoiTruSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKNoiTruTT.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatKhacSL.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatKhacTT.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKNgoaiTruSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKNgoaiTruTT.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];

                colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf3.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

            }
                if (_ht)
                {
                    GroupHeader1.GroupFields.Add(new GroupField("STT"));
                    GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                    //GroupHeader2.GroupFields.Add(new GroupField("TenTN"));
                    colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTN");

                }
                if (HTTrongNgoaiNuoc)
                {
                    GroupHeader3.GroupFields.Add(new GroupField("NuocSX"));
                }
                GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
            
        }
                       

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper(); 
            
        }
        int sttGh2 = 1;
        int sttGh1 = 1;
        
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (sttGh2)
            {
                case 1:
                    colSTTGh2.Text = "I";
                    break;
                case 2:
                    colSTTGh2.Text = "II";
                    break;
                case 3:
                    colSTTGh2.Text = "III";
                    break;
                case 4:
                    colSTTGh2.Text = "IV";
                    break;
                case 5:
                    colSTTGh2.Text = "IV";
                    break;

            }
            sttGh2++;
            sttGh1 = 1;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            if (xrTableCell1.Text != "TP. TCHC-CTXH")
            {
                colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            }
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            //colThuKho.Text = DungChung.Bien.ThuKho;
            colKhoaDuoc2.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan2.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc2.Text = DungChung.Bien.GiamDoc;
            colKeToan3.Text = DungChung.Bien.KeToanTruong;
            colThuKho3.Text = DungChung.Bien.ThuKho;
            colpgd3.Text = DungChung.Bien.TruongKhoaDuoc;

            if (DungChung.Bien.MaBV == "27021")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                SubBand3.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "20001")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand3.Visible = true;
                xrTableCell51.Text = "TRƯỞNG KHOA DƯỢC";
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
            }
            GroupHeader3.Visible = HTTrongNgoaiNuoc;
            GroupFooter3.Visible = HTTrongNgoaiNuoc;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("XuatTKTongTT") != null && GetCurrentColumnValue("XuatTKTongTT").ToString()== "0")
            //if (DungChung.Bien.MaBV == "12345")
            //{
                //xrTable1.Visible = false;
                //xrTable17.Visible = true;
                
            //}
            //else
            //{
                xrTable17.Visible = false;
            if (DungChung.Bien.MaBV == "30004")
            {
                xrTable24.Visible = true;
                xrTable1.Visible = false;
            }
            else
            {
                xrTable1.Visible = true;
            }
            //}
            if (DungChung.Bien.MaBV == "24297")
                if (colDonGia.Text == "11223344")
                    colDonGia.Text = "";
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupHeader1.Visible = _ht;
            GroupFooter1.Visible = _ht;
            if (DungChung.Bien.MaBV == "20001")
                SubBand5.Visible = true;
            else
                SubBand4.Visible = true;
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            colGr1.Text = sttGh1.ToString();
            sttGh1++;
        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NuocSX") != null)
            {
                if (this.GetCurrentColumnValue("NuocSX").ToString() == "1")
                    celTrongNgoaiNuoc.Text = "Thuốc nội";
                else
                    celTrongNgoaiNuoc.Text = "Thuốc ngoại";
            }
        }

        private void GroupFooter3_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NuocSX") != null)
            {
                if (this.GetCurrentColumnValue("NuocSX").ToString() == "1")
                    celTongTrongNgoaiNuoc.Text = "Tổng thuốc nội";
                else
                    celTongTrongNgoaiNuoc.Text = "Tổng thuốc ngoại";
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "12345")
            //{
               //SubBand6.Visible = false;
            //}
            //else
            //{
                SubBand7.Visible = false;
            //}
        }

    }
}
