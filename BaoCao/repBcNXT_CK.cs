using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNXT_CK : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXT_CK()
        {
            InitializeComponent();
        }
        bool hiennhom = true;
        bool HTTrongNgoaiNuoc = false;
        public repBcNXT_CK(bool a, bool HTTrongNgoaiNuoc)
        {
            InitializeComponent();
            hiennhom = a;
            this.HTTrongNgoaiNuoc = HTTrongNgoaiNuoc;
        }
        public void BindingData()
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand6.Visible = false;
                SubBand8.Visible = false;
                SubBand7.Visible = true;
                SubBand9.Visible = true;

                colmanoibo1.DataBindings.Add("Text", DataSource, "MaTam");
                colTenHamLuong1.DataBindings.Add("Text", DataSource, "TenHamLuong");
                handung.DataBindings.Add("Text", DataSource, "HanDung");
                solo.DataBindings.Add("Text", DataSource, "SoLo");
                colDVT1.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia1.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0:##,###.###}";
                colTonDKSL1.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonDKTT1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKSL1.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                colNhapTKTTde1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_SL1.DataBindings.Add("Text", DataSource, "NhapNB_SL").FormatString = DungChung.Bien.FormatString[0];
                colNhapNB_TT1.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_SL1.DataBindings.Add("Text", DataSource, "XuatNB_SL").FormatString = DungChung.Bien.FormatString[0];
                colXuatNB_TT1.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongSL1.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKSL1.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
                colTonCKTT1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
                
                colTonDKTTTong11.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf11.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf22.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf33.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf33.Summary.FormatString = DungChung.Bien.FormatString[1];

                colNhapTKTTTong11.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf11.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf22.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf33.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf33.Summary.FormatString = DungChung.Bien.FormatString[1];

                colNhapNB_TT_tn11.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_nh22.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_nh33.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_rf11.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_tn11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_nh22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_rf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_nh33.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatNB_TT_tn11.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_nh22.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_nh33.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_rf11.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_TT_tn11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_nh22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_nh33.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_rf11.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKTongTTGf11.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh22.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh33.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong11.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh33.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong11.Summary.FormatString = DungChung.Bien.FormatString[1];


                colTonCKTTGf11.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf22.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf33.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong11.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf11.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf22.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTGf33.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonCKTTTong11.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else if (DungChung.Bien.MaBV == "30004")
            {
                sub_30004.Visible = true;
                sub_30004_1.Visible = true;
                SubBand6.Visible = false;
                SubBand8.Visible = false;
                SubBand7.Visible = false;
                SubBand9.Visible = false;
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");

                HamLuong_30004.DataBindings.Add("Text", DataSource, "TenHamLuong");
                TenHC_30004.DataBindings.Add("Text", DataSource, "TenHC");
                NongDo_30004.DataBindings.Add("Text", DataSource, "HamLuong");

                DVT_30004.DataBindings.Add("Text", DataSource, "DonVi");
                DonGia_30004.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0:##,###.###}";

                TDKSL_30004.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
                TDKTT_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3_30004.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTonDKTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];

                NTKSL_30004.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
                NTKTT_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3_30004.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapTKTTGf3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];

                colNhapNB_30004_SL.DataBindings.Add("Text", DataSource, "NhapNB_SL").FormatString = DungChung.Bien.FormatString[0];
                colNhapNB_30004_TT.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_30004_TT_tn.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_30004_TT_nh.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_30004_TT_nh3.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_30004_TT_rf.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_30004_TT_tn.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_30004_TT_nh.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_30004_TT_rf.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_30004_TT_nh3.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatNB_30004_SL.DataBindings.Add("Text", DataSource, "XuatNB_SL").FormatString = DungChung.Bien.FormatString[0];
                colXuatNB_30004_TT.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_30004_TT_tn.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_30004_nh.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_30004_nh3.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_30004_rf.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_30004_TT_tn.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_30004_nh.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_30004_nh3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_30004_rf.Summary.FormatString = DungChung.Bien.FormatString[1];

                XTKSL_30004.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                XTKTT_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh1_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong_30004.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh1_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3_30004.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong_30004.Summary.FormatString = DungChung.Bien.FormatString[1];


                TCKSL_30004.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
                TCKTT_30004.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
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
                SubBand6.Visible = true;
                SubBand8.Visible = true;
                SubBand7.Visible = false;
                SubBand9.Visible = false;
                colmanoibo.DataBindings.Add("Text", DataSource, "MaTam");
                colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");

                colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

                colDVT.DataBindings.Add("Text", DataSource, "DonVi");
                colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0:##,###.###}";

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

                colNhapNB_SL.DataBindings.Add("Text", DataSource, "NhapNB_SL").FormatString = DungChung.Bien.FormatString[0];
                colNhapNB_TT.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_tn.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_nh.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_nh3.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_rf.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_tn.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_nh.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_rf.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapNB_TT_nh3.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatNB_SL.DataBindings.Add("Text", DataSource, "XuatNB_SL").FormatString = DungChung.Bien.FormatString[0];
                colXuatNB_TT.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_TT_tn.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_nh.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_nh3.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_rf.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_TT_tn.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_nh.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_nh3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatNB_rf.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];


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
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell1.Text = "Phó trưởng khoa phòng khám_Dược_CLS";
                xrTableCell31.Text = "Phó trưởng khoa phòng khám_Dược_CLS";
                xrTableCell51.Text = "PHÓ TRƯỞNG KHOA PHÒNG KHÁM_DƯỢC_CLS";

            }
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
                if (DungChung.Bien.MaBV == "30002")
                    xrTableCell9.Text = "Thống kê dược";
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable6.Visible = false;
                xrTable19.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                xrTable6.Visible = false;
                table_30004_3.Visible = true;
            }

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
            TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            GroupFooter1.Visible = hiennhom;
            GroupHeader1.Visible = hiennhom;
            GroupHeader3.Visible = HTTrongNgoaiNuoc;
            GroupFooter3.Visible = HTTrongNgoaiNuoc;
            if(DungChung.Bien.MaBV == "20001")
            {
                SubBand4.Visible = false;
                SubBand5.Visible = true;
                ngaythang1.Text = DungChung.Ham.NgaySangChu(DateTime.Now, DungChung.Bien.FormatDate);
            }
            else
            {
                SubBand5.Visible = false;
                SubBand4.Visible = true;
            }

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
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable13.Visible = false;
                xrTable18.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                xrTable13.Visible = false;
                table_30004_2.Visible = true;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable2.Visible = false;
                xrTable16.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                xrTable2.Visible = false;
                table_30004.Visible = true;
            }    
        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable3.Visible = false;
                xrTable17.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                xrTable3.Visible = false;
                table_30004_1.Visible = true;
            }
        }
    }
}
