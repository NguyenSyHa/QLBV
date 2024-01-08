using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNXTrutgon_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXTrutgon_24009()
        {
            InitializeComponent();
        }
        bool hiennhom = true;
        bool HTTrongNgoaiNuoc = false;
        public repBcNXTrutgon_24009(bool a, bool nk)
        {
            InitializeComponent();
            hiennhom = a;
            HTTrongNgoaiNuoc = nk;
        }
        public void BindingData()
        {
            colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");

            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");
                colMaTam.DataBindings.Add("Text", DataSource, "MaTam");
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
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell1.Text = "Phó trưởng khoa phòng khám_Dược_CLS";
                xrTableCell7.Text = "Phó trưởng khoa phòng khám_Dược_CLS";
                xrTableCell51.Text = "PHÓ TRƯỞNG KHOA PHÒNG KHÁM_DƯỢC_CLS";


            }
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
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
                xrTableCell51.Text = "PHỤ TRÁCH DƯỢC";
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                SubBand3.Visible = false;
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
