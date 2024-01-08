﻿using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNXTrutgon_Nhaptra : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXTrutgon_Nhaptra()
        {
            InitializeComponent();
        }
        bool hiennhom = true;
        bool HTTrongNgoaiNuoc = false;
        public repBcNXTrutgon_Nhaptra(bool a, bool nk)
        {
            InitializeComponent();
            hiennhom = a;
            HTTrongNgoaiNuoc = nk;
        }
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                SubBand6.Visible = true;
                SubBand7.Visible = false;
                SubBand8.Visible = true;
                SubBand9.Visible = false;
                SubBand10.Visible = false;
                SubBand11.Visible = false;
                colMaTam.DataBindings.Add("Text", DataSource, "MaTam");
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");

                HamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

                DVT.DataBindings.Add("Text", DataSource, "DonVi");
                DonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                TonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                TonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];


                NhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                NhapTKTTde.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];

                NhapTraLaiSL.DataBindings.Add("Text", DataSource, "NhapTra_SL").FormatString = DungChung.Bien.FormatString[0];
                NhapTraLaiTT.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf1.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf2.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf3.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraLaiTTTong.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                celNhapTraLaiTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

                XuatTKTongSL.DataBindings.Add("Text", DataSource, "TongXuatTKSL").FormatString = DungChung.Bien.FormatString[0];
                XuatTKTongTT.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else if (DungChung.Bien.MaBV == "24012")
            {
                    SubBand6.Visible = false;
                    SubBand7.Visible = false;
                    SubBand8.Visible = false;
                    SubBand9.Visible = false;
                    SubBand10.Visible = true;
                    SubBand11.Visible = true;
                    handung.DataBindings.Add("Text", DataSource, "HanDung");
                    colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                    solo.DataBindings.Add("Text", DataSource, "SoLo");
                    HamLuong1.DataBindings.Add("Text", DataSource, "TenHamLuong");
                    DVT1.DataBindings.Add("Text", DataSource, "DonVi");
                    DonGia1.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                    TonDKSL1.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                    TonDKTT1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf3.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];


                    NhapTKSL1.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                    NhapTKTTde1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf3.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];

                    NhapTraLaiSL1.DataBindings.Add("Text", DataSource, "NhapTra_SL").FormatString = DungChung.Bien.FormatString[0];
                    NhapTraLaiTT1.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                    celNhapTraTTGf1.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                    celNhapTraTTGf2.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                    celNhapTraTTGf3.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                    celNhapTraLaiTTTong.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                    celNhapTraTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                    celNhapTraTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                    celNhapTraTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                    celNhapTraLaiTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

                    XuatTKTongSL1.DataBindings.Add("Text", DataSource, "TongXuatTKSL").FormatString = DungChung.Bien.FormatString[0];
                    XuatTKTongTT1.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else
            {
                SubBand6.Visible = false;
                SubBand7.Visible = true;
                SubBand8.Visible = false;
                SubBand9.Visible = true;
                SubBand10.Visible = false;
                SubBand11.Visible = false;
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");

                colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

                colDVT.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
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

                celNhapTraLaiSL.DataBindings.Add("Text", DataSource, "NhapTra_SL").FormatString = DungChung.Bien.FormatString[0];
                celNhapTraLaiTT.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf1.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf2.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf3.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraLaiTTTong.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                celNhapTraTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                celNhapTraLaiTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKTongSL.DataBindings.Add("Text", DataSource, "TongXuatTKSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];
            }

           
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                colXuatTKTongSL.DataBindings.Add("Text", DataSource, "TongXuatTKSL1").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT.DataBindings.Add("Text", DataSource, "TongXuatTKTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "TongXuatTKTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "TongXuatTKTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "TongXuatTKTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "TongXuatTKTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];
            }

            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            TonCKTT1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            TonCKSL1.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonCKTTGf1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf3.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            if (hiennhom)
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

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
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

            GroupFooter1.Visible = hiennhom;
            GroupHeader1.Visible = hiennhom;
            GroupHeader3.Visible = HTTrongNgoaiNuoc;
            GroupFooter3.Visible = HTTrongNgoaiNuoc;
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            if (DungChung.Bien.MaBV == "20001")
                SubBand5.Visible = true;
            else
                SubBand4.Visible = true;
        }
        
        int sttGh2 = 1;
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
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
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
    }
}
