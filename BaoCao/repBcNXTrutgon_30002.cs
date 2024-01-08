using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNXTrutgon_30002 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXTrutgon_30002()
        {
            InitializeComponent();
        }
        bool hiennhom = true;
        bool HTTrongNgoaiNuoc = false;
        public repBcNXTrutgon_30002(bool a, bool nk)
        {
            InitializeComponent();
            hiennhom = a;
            HTTrongNgoaiNuoc = nk;
        }
        public void BindingData()
        {
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

            colNhapTTTSL.DataBindings.Add("Text", DataSource, "NhapTTTSL").FormatString = DungChung.Bien.FormatString[0];
            colNhapTTTTT.DataBindings.Add("Text", DataSource, "NhapTTTTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTTTTTGf1.DataBindings.Add("Text", DataSource, "NhapTTTTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTTTTTGf2.DataBindings.Add("Text", DataSource, "NhapTTTTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTTTTTGf3.DataBindings.Add("Text", DataSource, "NhapTTTTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTTTTTTongCong.DataBindings.Add("Text", DataSource, "NhapTTTTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTTTTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapTTTTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapTTTTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapTTTTTTongCong.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTongXuatSL.DataBindings.Add("Text", DataSource, "TongCotXuatSL").FormatString = DungChung.Bien.FormatString[0];
            colTongXuatTT.DataBindings.Add("Text", DataSource, "TongCotXuatTT").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTGf1.DataBindings.Add("Text", DataSource, "TongCotXuatTT").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTGf2.DataBindings.Add("Text", DataSource, "TongCotXuatTT").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTGf3.DataBindings.Add("Text", DataSource, "TongCotXuatTT").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTTongCong.DataBindings.Add("Text", DataSource, "TongCotXuatTT").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTTongCong.Summary.FormatString = DungChung.Bien.FormatString[1];

            colXuatDVSL.DataBindings.Add("Text", DataSource, "XuatDVSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatDVTT.DataBindings.Add("Text", DataSource, "XuatDVTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatDVTTG1.DataBindings.Add("Text", DataSource, "XuatDVTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatDVTTG2.DataBindings.Add("Text", DataSource, "XuatDVTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatDVTTG3.DataBindings.Add("Text", DataSource, "XuatDVTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatDVTTR.DataBindings.Add("Text", DataSource, "XuatDVTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatDVTTG3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatDVTTG1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatDVTTG2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatDVTTG1.Summary.FormatString = DungChung.Bien.FormatString[1];

            colXuatBHSL.DataBindings.Add("Text", DataSource, "XuatBHSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatBHTT.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatBHTTG1.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatBHTTG2.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatBHTTG3.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatBHTTR.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatBHTTG1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatBHTTG2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatBHTTG3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatBHTTR.Summary.FormatString = DungChung.Bien.FormatString[1];

            colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatKhac30002SL").FormatString = DungChung.Bien.FormatString[0];
            colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatKhac30002TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatKhac30002TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatKhac30002TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatKhac30002TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatKhac30002TT").FormatString = DungChung.Bien.FormatString[1];
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

           

            GroupFooter1.Visible = hiennhom;
            GroupHeader1.Visible = hiennhom;
            GroupHeader3.Visible = HTTrongNgoaiNuoc;
            GroupFooter3.Visible = HTTrongNgoaiNuoc;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ;
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;           
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

       
    }
}
