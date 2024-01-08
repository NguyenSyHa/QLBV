using System;using QLBV_Database;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace QLBV.BaoCao
{
    public partial class RepBcNXT : DevExpress.XtraReports.UI.XtraReport
    {
        public RepBcNXT()
        {
            InitializeComponent();
        }
        bool _ht = true;
        bool HTTrongNgoaiNuoc = false;
        int ma = -1;
        public RepBcNXT(bool ht,bool nk, int aa)
        {
            InitializeComponent();
            _ht = ht;
            HTTrongNgoaiNuoc = nk;
            ma = aa;
        }
        public void BindingData()
        {
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
           

            colNhapHDSL.DataBindings.Add("Text", DataSource, "NhapHD_SL").FormatString = DungChung.Bien.FormatString[0];
            colNhapHDTTde.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTTong.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTGf1.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTGf2.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTGf3.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapHDTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];

            if (ma == 1 )
            {
                colNhapCKSL.DataBindings.Add("Text", DataSource, "NhapCK_SL").FormatString = DungChung.Bien.FormatString[0];
                colNhapCKTTde.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTTong.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf1.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf2.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf3.DataBindings.Add("Text", DataSource, "NhapCK_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];

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

            }
            else if(ma == 0)
            {
                colNhapCKSL.DataBindings.Add("Text", DataSource, "NhapTra_SL").FormatString = DungChung.Bien.FormatString[0];
                colNhapCKTTde.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTTong.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf1.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf2.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf3.DataBindings.Add("Text", DataSource, "NhapTra_TT").FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colNhapCKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];

                colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL1").FormatString = DungChung.Bien.FormatString[0];
                colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatTKTongTT1").FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
                colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];

            }


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

            colXuatC3SL.DataBindings.Add("Text", DataSource, "XuatC3SL").FormatString = DungChung.Bien.FormatString[0];
            colXuatC3TT.DataBindings.Add("Text", DataSource, "XuatC3TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatC3TTGr1.DataBindings.Add("Text", DataSource, "XuatC3TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatC3TTGr2.DataBindings.Add("Text", DataSource, "XuatC3TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatC3TTGr3.DataBindings.Add("Text", DataSource, "XuatC3TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatC3TTTong.DataBindings.Add("Text", DataSource, "XuatC3TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatC3TTGr1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatC3TTGr2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatC3TTGr3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatC3TTTong.Summary.FormatString = DungChung.Bien.FormatString[1];

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

            //colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
            //colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKTongTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];

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
            }
            GroupHeader3.Visible = HTTrongNgoaiNuoc;
            GroupFooter3.Visible = HTTrongNgoaiNuoc;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (GetCurrentColumnValue("XuatTKTongTT") != null && GetCurrentColumnValue("XuatTKTongTT").ToString()== "0")
            if (DungChung.Bien.MaBV == "24297")
                if (colDonGia.Text == "11223344")
                    colDonGia.Text = "";
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
