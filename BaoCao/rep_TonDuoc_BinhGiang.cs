using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_TonDuoc_BinhGiang : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TonDuoc_BinhGiang()
        {
            InitializeComponent();
        }
        public void databinding()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            celTonDauSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            celTonDauTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
           // celTonDauTTTong.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            celTonDauTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celTonDauTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTonDauTTTong1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celTonDauTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTonDauTTTong2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celTonDauTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];

            celNhapHDSL.DataBindings.Add("Text", DataSource, "NhapTheoHDSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapHDTT.DataBindings.Add("Text", DataSource, "NhapTheoHDTT").FormatString = DungChung.Bien.FormatString[1];
           // celNhapHDSLTong.DataBindings.Add("Text", DataSource, "NhapTheoHDSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapHDTTTong.DataBindings.Add("Text", DataSource, "NhapTheoHDTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapHDTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapHDTTTong1.DataBindings.Add("Text", DataSource, "NhapTheoHDTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapHDTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapHDTTTong2.DataBindings.Add("Text", DataSource, "NhapTheoHDTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapHDTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];

            celNhapCKSL.DataBindings.Add("Text", DataSource, "NhapChuyenKhoSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapCKTT.DataBindings.Add("Text", DataSource, "NhapChuyenKhoTT").FormatString = DungChung.Bien.FormatString[1];
           // celNhapCKSLTong.DataBindings.Add("Text", DataSource, "NhapChuyenKhoSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapCKTTTong.DataBindings.Add("Text", DataSource, "NhapChuyenKhoTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapCKTTTong1.DataBindings.Add("Text", DataSource, "NhapChuyenKhoTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapCKTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapCKTTTong2.DataBindings.Add("Text", DataSource, "NhapChuyenKhoTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapCKTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];


            celNhapKhacSL.DataBindings.Add("Text", DataSource, "NhapKhacSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapKhacTT.DataBindings.Add("Text", DataSource, "NhapKhacTT").FormatString = DungChung.Bien.FormatString[1];
            // celNhapCKSLTong.DataBindings.Add("Text", DataSource, "NhapChuyenKhoSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapKhacTTTong.DataBindings.Add("Text", DataSource, "NhapKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapKhacTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapKhacTTTong1.DataBindings.Add("Text", DataSource, "NhapKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapKhacTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapKhacTTTong2.DataBindings.Add("Text", DataSource, "NhapKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapKhacTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];

            celXuatSDSL.DataBindings.Add("Text", DataSource, "XuatBNSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatSDTT.DataBindings.Add("Text", DataSource, "XuatBNTT").FormatString = DungChung.Bien.FormatString[1];
          //  celXuatSDSLTong.DataBindings.Add("Text", DataSource, "XuatBNSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatSDTTTong.DataBindings.Add("Text", DataSource, "XuatBNTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatSDTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatSDTTTong1.DataBindings.Add("Text", DataSource, "XuatBNTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatSDTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatSDTTTong2.DataBindings.Add("Text", DataSource, "XuatBNTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatSDTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];

            celXuatCKSL.DataBindings.Add("Text", DataSource, "XuatSuDungSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatCKTT.DataBindings.Add("Text", DataSource, "XuatSuDungTT").FormatString = DungChung.Bien.FormatString[1];
           // celXuatCKSLTong.DataBindings.Add("Text", DataSource, "XuatSuDungSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatCKTTTong.DataBindings.Add("Text", DataSource, "XuatSuDungTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatCKTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatCKTTTong1.DataBindings.Add("Text", DataSource, "XuatSuDungTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatCKTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatCKTTTong2.DataBindings.Add("Text", DataSource, "XuatSuDungTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatCKTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];

            celXuatNgTSL.DataBindings.Add("Text", DataSource, "XuatNgTSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatNgTTT.DataBindings.Add("Text", DataSource, "XuatNgTTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatNgTTTTong.DataBindings.Add("Text", DataSource, "XuatNgTTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatNgTTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatNgTTTTong1.DataBindings.Add("Text", DataSource, "XuatNgTTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatNgTTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatNgTTTTong2.DataBindings.Add("Text", DataSource, "XuatNgTTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatNgTTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];

            celXuatKhacSL.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatKhacTT.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTTong.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTTong1.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTTong2.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTongXuatSL.DataBindings.Add("Text", DataSource, "TongXuatSL").FormatString = DungChung.Bien.FormatString[0];
            celTongXuatTT.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTTong.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTTong1.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTTong2.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];


            celTonCuoiSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            celTonCuoiTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            //celTonCuoiSLTong.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            celTonCuoiTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCuoiTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTonCuoiTTTong1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCuoiTTTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTonCuoiTTTong2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCuoiTTTong2.Summary.FormatString = DungChung.Bien.FormatString[1];

            colNhom.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            colTieuNhom.DataBindings.Add("Text", DataSource, "TenTN");

            GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _da = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var cb = _da.CanBoes.Where(p => p.MaCB == DungChung.Bien.MaCB).ToList();
            if (cb.Count > 0)
            {
                txtnguoilapbieu.Text = cb.First().TenCB;
            }
            txtNgayLap.Text = "Ngày....tháng....năm....";//"Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            xtxTruongKS.Text = DungChung.Bien.TruongKhoaDuoc;
            txtGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        int sttGh2 = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (sttGh2)
            {
                case 1:
                    colSTTNhom.Text = "I";
                    break;
                case 2:
                    colSTTNhom.Text = "II";
                    break;
                case 3:
                    colSTTNhom.Text = "III";
                    break;
                case 4:
                    colSTTNhom.Text = "IV";
                    break;
                case 5:
                    colSTTNhom.Text = "IV";
                    break;
            }
            sttGh2++;
        }
    }
}
