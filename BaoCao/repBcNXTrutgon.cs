using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repBcNXTrutgon : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXTrutgon()
        {
            InitializeComponent();
        }
        bool hiennhom = true;
        bool HTTrongNgoaiNuoc = false;
        public repBcNXTrutgon(bool a, bool nk)
        {
            InitializeComponent();
            hiennhom = a;
            HTTrongNgoaiNuoc = nk;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "30003")
            {
                SubBand10.Visible = false;
                SubBand8.Visible = false;
                SubBand9.Visible = true;
                SubBand7.Visible = true;
                SubBand12.Visible = false;
                SubBand11.Visible = false;

                SubBand14.Visible = false;
                SubBand13.Visible = false;
                GroupFooter4.Visible = false;
                GroupFooter5.Visible = false;
                GroupFooter6.Visible = false;
                GroupFooter7.Visible = false;

                colMaTam.DataBindings.Add("Text", DataSource, "MaTam");
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
                colNhapTKTTde.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[0];
                //colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];


                colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];


                colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[0];
                colTonCKTTGf1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf3.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else if (DungChung.Bien.MaBV == "24012")
            {
                SubBand10.Visible = false;
                SubBand8.Visible = false;
                SubBand9.Visible = false;
                SubBand7.Visible = false;
                SubBand11.Visible = true;
                SubBand12.Visible = true;

                SubBand14.Visible = false;
                SubBand13.Visible = false;
                GroupFooter4.Visible = false;
                GroupFooter5.Visible = false;
                GroupFooter6.Visible = false;
                GroupFooter7.Visible = false;

                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                HamLuong1.DataBindings.Add("Text", DataSource, "TenHamLuong");
                handung.DataBindings.Add("Text", DataSource, "HanDung");
                DVT1.DataBindings.Add("Text", DataSource, "DonVi");
                solo.DataBindings.Add("Text", DataSource, "SoLo");
                DonGia1.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                TDKSL1.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                TDKTT1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];


                NTKSL1.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                NTKTT1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[0];
                //colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];


                XTKSL1.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                XTKTT1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];


                TCKSL1.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
                TCKTT1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[0];
                colTonCKTTGf1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf3.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

            }
            else if (DungChung.Bien.MaBV == "27023")
            {
                SubBand14.Visible = true;
                SubBand13.Visible = true;
                SubBand9.Visible = false;
                SubBand7.Visible = false;
                SubBand8.Visible = false;
                SubBand10.Visible = false;
                SubBand12.Visible = false;
                SubBand11.Visible = false;
                GroupFooter4.Visible = true;
                GroupFooter5.Visible = true;
                GroupFooter6.Visible = true;
                GroupFooter7.Visible = true;

                GroupFooter1.Visible = false;
                GroupFooter2.Visible = false;
                GroupFooter3.Visible = false;
                xrTable6.Visible = false;
                //colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");

                colTHL.DataBindings.Add("Text", DataSource, "TenHamLuong");
                colNDHL.DataBindings.Add("Text", DataSource, "HamLuong");
                colNKT.DataBindings.Add("Text", DataSource, "NhomThau");
                colHC.DataBindings.Add("Text", DataSource, "TenHC");
                colDV.DataBindings.Add("Text", DataSource, "DonVi");
                colDG.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

                colSLTDK.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTTTDK.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong09.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf09.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf099.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf0999.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf099.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf0999.Summary.FormatString = DungChung.Bien.FormatString[1];


                colSLNTK.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                colTTNTK.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong09.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf09.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf099.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf0999.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf099.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf0999.Summary.FormatString = DungChung.Bien.FormatString[1];

                colSLXTK.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                colTTXTK.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf09.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong09.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGhf099.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGhf0999.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGhf099.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGhf0999.Summary.FormatString = DungChung.Bien.FormatString[1];

                colSLTCK.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
                colTTTCK.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf11.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf21.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf31.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf21.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf31.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];

                colTonDKTTGf11.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf21.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf31.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf21.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf31.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];

                colNhapTKTTGf11.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf21.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf31.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf21.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf31.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKTongTTGf11.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh21.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh31.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh21.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh31.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else if (DungChung.Bien.MaBV == "27022")
            {
                SubBand10.Visible = false;
                SubBand8.Visible = false;
                SubBand9.Visible = false;
                SubBand7.Visible = false;
                SubBand12.Visible = false;
                SubBand11.Visible = false;

                SubBand14.Visible = false;
                SubBand13.Visible = false;

                SubBand15.Visible = true;
                SubBand16.Visible = true;

                GroupFooter4.Visible = false;
                GroupFooter5.Visible = false;
                GroupFooter6.Visible = false;
                GroupFooter7.Visible = false;

                GroupFooter8.Visible = true;
                GroupFooter9.Visible = true;
                GroupFooter10.Visible = true;
                GroupFooter11.Visible = true;

                GroupFooter1.Visible = false;
                GroupFooter2.Visible = false;
                GroupFooter3.Visible = false;

                xrTable6.Visible = false;
                colMaTam09.DataBindings.Add("Text", DataSource, "MaTam");
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                colTenHamLuong09.DataBindings.Add("Text", DataSource, "TenHamLuong");
                colHC09.DataBindings.Add("Text", DataSource, "TenHC");
                colDVT09.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia09.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                colTonDKSL09.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonDKTT09.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong09.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf09.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf099.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf0999.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf099.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf0999.Summary.FormatString = DungChung.Bien.FormatString[1];


                colNhapTKSL09.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                colNhapTKTTde09.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[0];
                //colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong09.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf09.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf099.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf0999.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf099.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf0999.Summary.FormatString = DungChung.Bien.FormatString[1];


                colXuatTKTongSL09.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT09.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTTGf09.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong09.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGhf099.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGhf0999.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGhf099.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGhf0999.Summary.FormatString = DungChung.Bien.FormatString[1];


                colTonCKSL09.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTT09.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[0];
                colTonCKTTGf09.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf099.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf0999.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong09.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf09.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf099.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf0999.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong09.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else
            {
                if (DungChung.Bien.MaBV == "30004")
                {
                    sub_30004.Visible = true;
                    Sub_30004_1.Visible = true;
                    SubBand10.Visible = false;
                    SubBand8.Visible = false;
                    xrTable28.Visible = true;
                    grup_30004_2.Visible = true;
                    grup_30004_3.Visible = true;
                    xrTable31.Visible = true;
                    xrTable6.Visible = false;
                    SubBand12.Visible = false;
                    SubBand11.Visible = false;
                    SubBand7.Visible = false;
                    SubBand10.Visible = false;
                    SubBand14.Visible = false;
                    SubBand13.Visible = false;
                    SubBand9.Visible = false;
                    GroupFooter4.Visible = false;
                    GroupFooter5.Visible = false;
                    GroupFooter6.Visible = false;
                    GroupFooter7.Visible = false;
                    xrTable2.Visible = false;
                    GroupFooter2.Visible = false;
                    GroupFooter3.Visible = false;

                    TenHC_30004.DataBindings.Add("Text", DataSource, "TenHC");
                    NongDo_30004.DataBindings.Add("Text", DataSource, "HamLuong");
                    colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                    HamLuong_30004.DataBindings.Add("Text", DataSource, "TenHamLuong");
                    DVT_30004.DataBindings.Add("Text", DataSource, "DonVi");
                    DonGia_30004.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                    TDKSL_30004.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                    TDKTT_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTTong_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf2_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf3_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    NTKSL_30004.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                    NTKTT_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTTong_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf2_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf3_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    XTKSL_30004.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                    XTKTT_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGf_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTTong_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh2_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh3_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGf_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    TCKSL_30004.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
                    TCKTT_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf2_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf3_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTTong_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                }
                else
                {
                    SubBand9.Visible = false;
                    SubBand7.Visible = false;
                    SubBand8.Visible = true;
                    SubBand10.Visible = true;
                    SubBand12.Visible = false;
                    SubBand11.Visible = false;
                    SubBand14.Visible = false;
                    SubBand13.Visible = false;
                    GroupFooter4.Visible = false;
                    GroupFooter5.Visible = false;
                    GroupFooter6.Visible = false;
                    GroupFooter7.Visible = false;

                    colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                    HamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");
                    DVT.DataBindings.Add("Text", DataSource, "DonVi");
                    DonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
                    TDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                    TDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf3.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonDKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                    NTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                    NTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf3.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colNhapTKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                    XTKSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                    XTKTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];
                    TCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
                    TCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf3.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colTonCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                }
            }

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

            if (DungChung.Bien.MaBV == "27023")
            {
                SubBand6.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            if(xrTableCell1.Text != "TP. TCHC-CTXH")
            {
                colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            }
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;

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
                var cb = data.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).FirstOrDefault();
                if (cb != null)
                {
                    xrTableCell31.Text = cb.TenCB;
                }
                xrTableCell15.Text = "TRƯỞNG KHOA DƯỢC";
                //xrTableCell31.Text=DungChung.Bie
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
                if (DungChung.Bien.MaBV == "30350")
                    xrTableCell5.Text = "Bệnh Xá Trưởng";
            }
            //colThuKho.Text = DungChung.Bien.ThuKho;
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

        private void SubBand1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30002")
                xrTableCell9.Text = "Thống kê dược";
        }

        private void colTonCKTTTong_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //double soTien = 0;
            //if (e.Value != null)
            //{
            //    soTien = Convert.ToDouble(e.Value);
            //}
            //soTienStr = DungChung.Ham.DocTienBangChu(soTien, " đồng!");
        }

        private void lblSoTien_BeforePrint(object sender, CancelEventArgs e)
        {
            lblSoTien.Text = DungChung.Ham.DocTienBangChu(soTien, " đồng!");
        }

        double soTien = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27023")
            {
                double tonCK = Convert.ToDouble(GetCurrentColumnValue("TonCKTT"));
                soTien += tonCK;
            }
        }

        private void SubBand8_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "24297")
                if (DonGia.Text == "11223344")
                    DonGia.Text = "";
        }
    }
}
