using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcSuDungThuoc_TDuong_New : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcSuDungThuoc_TDuong_New()
        {
            InitializeComponent();
        }
        //bool HThi = true;
        //public Rep_BcSuDungThuoc_TDuong(bool ht)
        //{
        //    InitializeComponent();
        //    HThi = ht;
        //}
        //QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
    
        public void BindingData()
        {
            //colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDV");
            colTieuNhomDV.DataBindings.Add("Text", DataSource, "TieuNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            //txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGF.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGF.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            col1SL.DataBindings.Add("Text", DataSource, "NhapTLSL").FormatString = DungChung.Bien.FormatString[1];
            col1TT.DataBindings.Add("Text", DataSource, "NhapTLTT").FormatString = DungChung.Bien.FormatString[1];
            col1TTCong.DataBindings.Add("Text", DataSource, "NhapTLSL").FormatString = DungChung.Bien.FormatString[1];
            col1TTtc.DataBindings.Add("Text", DataSource, "NhapTLTT").FormatString = DungChung.Bien.FormatString[1];

            col2SL.DataBindings.Add("Text", DataSource, "TNhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            col2TT.DataBindings.Add("Text", DataSource, "TNhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            col2TTcong.DataBindings.Add("Text", DataSource, "TNhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            col2TTtc.DataBindings.Add("Text", DataSource, "TNhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            col3SL.DataBindings.Add("Text", DataSource, "XuatBHSL").FormatString = DungChung.Bien.FormatString[1];
            col3TT.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            col3TTcong.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            col3TTtc.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];

            col4SL.DataBindings.Add("Text", DataSource, "Xuat139SL").FormatString = DungChung.Bien.FormatString[1];
            col4TT.DataBindings.Add("Text", DataSource, "Xuat139TT").FormatString = DungChung.Bien.FormatString[1];
            col4TTcong.DataBindings.Add("Text", DataSource, "Xuat139TT").FormatString = DungChung.Bien.FormatString[1];
            col4TTtc.DataBindings.Add("Text", DataSource, "Xuat139TT").FormatString = DungChung.Bien.FormatString[1];

            col5SL.DataBindings.Add("Text", DataSource, "XuatTESL").FormatString = DungChung.Bien.FormatString[1];
            col5TT.DataBindings.Add("Text", DataSource, "XuatTETT").FormatString = DungChung.Bien.FormatString[1];
            col5TTcong.DataBindings.Add("Text", DataSource, "XuatTETT").FormatString = DungChung.Bien.FormatString[1];
            col5TTtc.DataBindings.Add("Text", DataSource, "XuatTETT").FormatString = DungChung.Bien.FormatString[1];

            celXuatKhacSL.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTT.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTT_G.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTT_R.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];

            colSDTKSL.DataBindings.Add("Text", DataSource, "XuatTSL").FormatString = DungChung.Bien.FormatString[1];
            colSDTKTT.DataBindings.Add("Text", DataSource, "XuatTTT").FormatString = DungChung.Bien.FormatString[1];
            colSDTKTTGF.DataBindings.Add("Text", DataSource, "XuatTTT").FormatString = DungChung.Bien.FormatString[1];
            colSDTKTTTong.DataBindings.Add("Text", DataSource, "XuatTTT").FormatString = DungChung.Bien.FormatString[1];

            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGF.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TieuNhomDV"));
            //GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colThuKho.Text = DungChung.Bien.NguoiLapBieu;
            colTKDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
    
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void colDonGia_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        
    }
}
