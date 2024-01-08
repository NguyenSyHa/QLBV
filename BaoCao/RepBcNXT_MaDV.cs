using System;using QLBV_Database;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace QLBV.BaoCao
{
    public partial class RepBcNXT_MaDV : DevExpress.XtraReports.UI.XtraReport
    {
        public RepBcNXT_MaDV()
        {
            InitializeComponent();
        }
        bool _ht = true;
        bool HTTrongNgoaiNuoc = false;
        public RepBcNXT_MaDV(bool ht,bool nk)
        {
            InitializeComponent();
            _ht = ht;
            HTTrongNgoaiNuoc = nk;
        }
        public void BindingData()
        {
            colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            //colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");
            if(DungChung.Bien.MaBV == "24009")
                colMaDV.DataBindings.Add("Text", DataSource, "MaTam");
            else
                colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
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
            colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString=DungChung.Bien.FormatString[1];
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
            TenCQ.Value = DungChung.Bien.TenCQ;
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            
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

            if (DungChung.Bien.MaBV == "27021")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
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

    }
}
