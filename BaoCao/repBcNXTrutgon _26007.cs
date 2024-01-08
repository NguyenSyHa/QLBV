using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNXTrutgon_26007 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXTrutgon_26007()
        {
            InitializeComponent();
        }
        bool hiennhom = true;
        bool HTTrongNgoaiNuoc = false;
        public repBcNXTrutgon_26007(bool a, bool nk)
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


            colNhapHD.DataBindings.Add("Text", DataSource, "NhapHD_SL").FormatString = DungChung.Bien.FormatString[0];
            colNhapHDTTde.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTTong.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTde1.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTde2.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTde3.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];

            colNhapHDTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTde1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTde2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTde3.Summary.FormatString = DungChung.Bien.FormatString[1];

            colNhapCK.DataBindings.Add("Text", DataSource, "NhapCK_SL").FormatString = DungChung.Bien.FormatString[0];
            colNhapCKTT.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapCKTTTong.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapCKTT1.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapCKTT2.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapCKTT3.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];

            colNhapCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapCKTT1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapCKTT2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapCKTT3.Summary.FormatString = DungChung.Bien.FormatString[1];

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

            colKhoaDuoc2.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan2.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc2.Text = DungChung.Bien.GiamDoc;

            if (DungChung.Bien.MaBV == "27021")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }
            //colThuKho.Text = DungChung.Bien.ThuKho;

            GroupFooter1.Visible = hiennhom;
            GroupHeader1.Visible = hiennhom;
            GroupHeader3.Visible = HTTrongNgoaiNuoc;
            GroupFooter3.Visible = HTTrongNgoaiNuoc;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ;
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
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
