using System;using QLBV_Database;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace QLBV.BaoCao
{
    public partial class RepBcNXT_NT : DevExpress.XtraReports.UI.XtraReport
    {
        public RepBcNXT_NT()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenNhomG1.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            colTenNCCG2.DataBindings.Add("Text", DataSource, "TenCC");
            //colTenHamLuongGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
            colTenHamLuong.DataBindings.Add("Text", DataSource, "TenHamLuong");

            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

            //txtTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL");
            //txtTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT");
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
           // colTonDKSLGf1.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            //colTonDKSLGf2.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGf2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
           

            //txtNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL");
            //txtNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT");
            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            colNhapTKTTde.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
           // colNhapTKSLGf1.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf1.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
           // colNhapTKSLGf2.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGf2.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatHHSL.DataBindings.Add("Text", DataSource, "HuHaoSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatHHTT.DataBindings.Add("Text", DataSource, "HuHaoTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatHHTTGF1.DataBindings.Add("Text", DataSource, "HuHaoTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatHHTTGF2.DataBindings.Add("Text", DataSource, "HuHaoTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatHHTTRF.DataBindings.Add("Text", DataSource, "HuHaoTT").FormatString = DungChung.Bien.FormatString[1];

            colNhapTraSL.DataBindings.Add("Text", DataSource, "NhapTraSL").FormatString = DungChung.Bien.FormatString[0];
            colNhapTraTT.DataBindings.Add("Text", DataSource, "NhapTraTT").FormatString = DungChung.Bien.FormatString[1];

            colNhapTraTTGf1.DataBindings.Add("Text", DataSource, "NhapTraTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTraTTGh2.DataBindings.Add("Text", DataSource, "NhapTraTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTraTTTong.DataBindings.Add("Text", DataSource, "NhapTraTT").FormatString = DungChung.Bien.FormatString[1];

            //txtXuatNTSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL");
            //txtXuatNTTT.DataBindings.Add("Text", DataSource, "XuatNoiTruTT");
            colXuatTKNoiTruSL.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatTKNoiTruTT.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKNoiTruSLGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKNoiTruSLGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNoiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNoiTruTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatKhacSL.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatKhacTT.DataBindings.Add("Text", DataSource, "xuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatKhacSLGF1.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTGF1.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatKhacSLGF2.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTGF2.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatKhacSLRF.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatKhacTTRF.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            //txtXuatNgTSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL");
            //txtXuatNgTTT.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT");
            colXuatTKNgoaiTruSL.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatTKNgoaiTruTT.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            //colXuatTKNgoaiTruSLGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruTTGf1.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
           // colXuatTKNgoaiTruSLGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruTTGf2.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKNgoaiTruTTTong.DataBindings.Add("Text", DataSource, "XuatNgoaiTruTT").FormatString = DungChung.Bien.FormatString[1];

            //txtXuatTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL");
            //txtXuatTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT");
            colXuatTKTongSL.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[0];
            colXuatTKTongTT.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString=DungChung.Bien.FormatString[1];
          //  colXuatTKTongSLGf1.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGf1.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTTong.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];
           // colXuatTKTongSLGh2.DataBindings.Add("Text", DataSource, "XuatTKTongSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTongTTGh2.DataBindings.Add("Text", DataSource, "XuatTKTongTT").FormatString = DungChung.Bien.FormatString[1];

            //txtTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL");
            //txtTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT");
            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
          //  colTonCKSLGf1.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
          //  colTonCKSLGf2.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGf2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomDuoc"));
            GroupHeader2.GroupFields.Add(new GroupField("TenCC"));
        }
                       

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ;
            TenCQCQ.Value = DungChung.Bien.TenCQCQ;
            
        }
        int sttGh2 = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            colSTTGh2.Text = sttGh2.ToString();
            sttGh2++;
            sttGh1 = 1;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {          
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;          
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {         
                
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //GroupHeader2.Visible = _ht;
            //GroupFooter2.Visible = _ht;
        }

        int sttGh1 = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            colSTTg1.Text = sttGh1.ToString();
            sttGh1++;
        }

    }
}
