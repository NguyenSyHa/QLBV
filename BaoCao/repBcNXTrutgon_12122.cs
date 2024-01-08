using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcNXTrutgon_12122 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNXTrutgon_12122()
        {
            InitializeComponent();
        }
        bool hiennhom = true;
        bool HTTrongNgoaiNuoc = false;
        public repBcNXTrutgon_12122(bool a, bool HTTrongNgoaiNuoc)
        {
            InitializeComponent();
            hiennhom = a;
            this.HTTrongNgoaiNuoc = HTTrongNgoaiNuoc;
        }
        public void BindingData()
        {
            colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
           
            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

            celHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");

            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
           // txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
         
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTG3.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTG3.Summary.FormatString = DungChung.Bien.FormatString[1];


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
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf3.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
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
