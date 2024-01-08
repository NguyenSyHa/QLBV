using System;using QLBV_Database;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace QLBV.BaoCao
{
    public partial class RepBcNXT_12001 : DevExpress.XtraReports.UI.XtraReport
    {
        public RepBcNXT_12001()
        {
            InitializeComponent();
        }
        bool _ht = true;
        bool HTTrongNgoaiNuoc = false;
        public RepBcNXT_12001(bool ht, bool HTTrongNgoaiNuoc)
        {
            InitializeComponent();
            _ht = ht;
            this.HTTrongNgoaiNuoc = HTTrongNgoaiNuoc;
        }
        public void BindingData()
        {
            colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            //colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            col_NhapCK.DataBindings.Add("Text", DataSource, "NhapNB_SL").FormatString = DungChung.Bien.FormatString[0];
            col_NhapCK_TT.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgTruSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatTKNgTruTT.DataBindings.Add("Text", DataSource, "xuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatCLS.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatCLS_TT.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatTKNoiTruTT.DataBindings.Add("Text", DataSource, "xuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhoXa.DataBindings.Add("Text", DataSource, "XuatNB_SL").FormatString = DungChung.Bien.FormatString[0];
            colXuatKhoXa_TT.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

          
            colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];            
            colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];          
            col_NhapCKTT_GF1.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNTruTTGf1.DataBindings.Add("Text", DataSource, "xuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];           
            colXuatKhacTTGF1.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgTruTTGf1.DataBindings.Add("Text", DataSource, "xuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            colTonDKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            col_NhapCKTT_GF1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTGF1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgTruTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf1.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            col_NhapCKTT_GF2.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNTruTTGf2.DataBindings.Add("Text", DataSource, "xuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTGF2.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgTruTTGf2.DataBindings.Add("Text", DataSource, "xuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            col_NhapCKTT_GF2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTGF2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgTruTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf2.Summary.FormatString = DungChung.Bien.FormatString[1];


            colTonDKTTGf3.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf3.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            col_NhapCKTT_GF3.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNTruTTGf3.DataBindings.Add("Text", DataSource, "xuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTGF3.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgTruTTGf3.DataBindings.Add("Text", DataSource, "xuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh3.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf3.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
            col_NhapCKTT_GF3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTGF3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgTruTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh3.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf3.Summary.FormatString = DungChung.Bien.FormatString[1];

            colTonDKTTrf.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTrf.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            col_NhapCKTT_rf.DataBindings.Add("Text", DataSource, "NhapNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNTruTTrf.DataBindings.Add("Text", DataSource, "xuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTrf.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgTruTTrf.DataBindings.Add("Text", DataSource, "xuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTrf.DataBindings.Add("Text", DataSource, "XuatNB_TT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTrf.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            colTonDKTTrf.Summary.FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTrf.Summary.FormatString = DungChung.Bien.FormatString[1];
            col_NhapCKTT_rf.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNTruTTrf.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTrf.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgTruTTrf.Summary.FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTrf.Summary.FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTrf.Summary.FormatString = DungChung.Bien.FormatString[1];

            
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
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell2.Text = "Phó trưởng khoa phòng khám_Dược_CLS";
            }
            //colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToan.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            //colThuKho.Text = DungChung.Bien.ThuKho;
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
