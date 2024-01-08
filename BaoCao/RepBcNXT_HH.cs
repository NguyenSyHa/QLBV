using System;using QLBV_Database;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace QLBV.BaoCao
{
    public partial class RepBcNXT_HH : DevExpress.XtraReports.UI.XtraReport
    {
        public RepBcNXT_HH()
        {
            InitializeComponent();
        }
        bool _ht = true;
        bool HTTrongNgoaiNuoc = false;
        public RepBcNXT_HH(bool ht, bool HTTrongNgoaiNuoc)
        {
            InitializeComponent();
            _ht = ht;
            this.HTTrongNgoaiNuoc = HTTrongNgoaiNuoc;
        }
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                SubBand6.Visible = true;
                SubBand8.Visible = false;
                SubBand7.Visible = true;
                SubBand9.Visible = false;
                SubBand10.Visible = false;
                SubBand11.Visible = false;
                colMaTam.DataBindings.Add("Text", DataSource, "MaTam");
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                //colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
                HamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

                DVT.DataBindings.Add("Text", DataSource, "DonVi");
                DonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                DKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                DKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];


                TKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                TKTTde.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];

                XuatTKNoiTruSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[0];
                XuatTKNoiTruTT.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

                XuatKhacSL.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
                XuatKhacTT.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF.Summary.FormatString = DungChung.Bien.FormatString[1];

                XuatHuHaoSL.DataBindings.Add("Text", DataSource, "XuatHuHaoSL").FormatString = DungChung.Bien.FormatString[0];
                HuHaoTT.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                colHuhaoTTGF1.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF2.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF3.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                colHuHaoTTRF.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                colHuhaoTTGF1.Summary.FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF2.Summary.FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colHuHaoTTRF.Summary.FormatString = DungChung.Bien.FormatString[1];

                XuatTKNgoaiTruSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[0];
                XuatTKNgoaiTruTT.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else
            {
                SubBand6.Visible = false;
                SubBand8.Visible = true;
                SubBand7.Visible = false;
                SubBand9.Visible = true;
                SubBand10.Visible = false;
                SubBand11.Visible = false;
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

                colXuatHuHaoSL.DataBindings.Add("Text", DataSource, "XuatHuHaoSL").FormatString = DungChung.Bien.FormatString[0];
                colHuHaoTT.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                colHuhaoTTGF1.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF2.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF3.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                colHuHaoTTRF.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                colHuhaoTTGF1.Summary.FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF2.Summary.FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colHuHaoTTRF.Summary.FormatString = DungChung.Bien.FormatString[1];

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
            }
            if (DungChung.Bien.MaBV == "30005")
            {
                colXuatTKNoiTruSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL1").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKNoiTruTT.DataBindings.Add("Text", DataSource, "XuatNoiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3.DataBindings.Add("Text", DataSource, "XuatNoiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNoiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatKhacSL.DataBindings.Add("Text", DataSource, "XuatKhacSL1").FormatString = DungChung.Bien.FormatString[0];
                colXuatKhacTT.DataBindings.Add("Text", DataSource, "xuatKhacTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1.DataBindings.Add("Text", DataSource, "XuatKhacTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2.DataBindings.Add("Text", DataSource, "XuatKhacTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3.DataBindings.Add("Text", DataSource, "XuatKhacTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF.DataBindings.Add("Text", DataSource, "XuatKhacTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKNgoaiTruSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL1").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKNgoaiTruTT.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand6.Visible = false;
                SubBand8.Visible = false;
                SubBand7.Visible = false;
                SubBand9.Visible = false;
                SubBand10.Visible = true;
                SubBand11.Visible = true;
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                //colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
                HamLuong1.DataBindings.Add("Text", DataSource, "TenHamLuong");
                handung.DataBindings.Add("Text", DataSource, "HanDung");
                solo.DataBindings.Add("Text", DataSource, "SoLo");
                DVT1.DataBindings.Add("Text", DataSource, "DonVi");
                DonGia1.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                DKTT1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                DKSL1.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];


                TKSL1.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                TKTTde1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];

                XuatTKNoiTruSL1.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[0];
                XuatTKNoiTruTT1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNoiTruTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

                XuatKhacSL1.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
                XuatKhacTT1.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTGF3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatKhacTTRF.Summary.FormatString = DungChung.Bien.FormatString[1];

                XuatHuHaoSL1.DataBindings.Add("Text", DataSource, "XuatHuHaoSL").FormatString = DungChung.Bien.FormatString[0];
                HuHaoTT1.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                colHuhaoTTGF1.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF2.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF3.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                colHuHaoTTRF.DataBindings.Add("Text", DataSource, "XuatHuHaoTT").FormatString = DungChung.Bien.FormatString[1];
                colHuhaoTTGF1.Summary.FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF2.Summary.FormatString = DungChung.Bien.FormatString[1];
                coHuHaoTTGF3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colHuHaoTTRF.Summary.FormatString = DungChung.Bien.FormatString[1];

                XuatTKNgoaiTruSL1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[0];
                XuatTKNgoaiTruTT1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKNgoaiTruTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            XuatTKTongTT1.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[1];
            XuatTKTongSL1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString=DungChung.Bien.FormatString[0];
            colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];

            TonCKSL1.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            TonCKTT1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf3.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
            if (_ht)
            {
                GroupHeader1.GroupFields.Add(new GroupField("STT"));
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
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
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
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
                if (DungChung.Bien.MaBV == "30002")
                    xrTableCell9.Text = "Thống kê dược";
            }
            GroupHeader3.Visible = HTTrongNgoaiNuoc;
            GroupFooter3.Visible = HTTrongNgoaiNuoc;

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("XuatTKTongTT") != null && GetCurrentColumnValue("XuatTKTongTT").ToString()== "0")
                
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

        private void SubBand1_BeforePrint(object sender, CancelEventArgs e)
        {

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
    }
}
