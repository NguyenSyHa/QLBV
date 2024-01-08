using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BaoCaoNXT_30002_Moi : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BaoCaoNXT_30002_Moi()
        {
            InitializeComponent();
        }


        internal void databinding()
        {

            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDVT.DataBindings.Add("Text", DataSource, "DonVi");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            celTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            celTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapSL.DataBindings.Add("Text", DataSource, "NhapSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapTT.DataBindings.Add("Text", DataSource, "NhapTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTuTrucSL.DataBindings.Add("Text", DataSource, "NhapTuTuTrucSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapTuTrucTT.DataBindings.Add("Text", DataSource, "NhapTuTuTrucTT").FormatString = DungChung.Bien.FormatString[1];

            celXuatBHSL.DataBindings.Add("Text", DataSource, "XuatBHSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatBHTT.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatDVSL.DataBindings.Add("Text", DataSource, "XuatDVSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatDVTT.DataBindings.Add("Text", DataSource, "XuatDVTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacSL.DataBindings.Add("Text", DataSource, "XuatKhacSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatKhacTT.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuatSL.DataBindings.Add("Text", DataSource, "TongXuatSL").FormatString = DungChung.Bien.FormatString[0];
            celTongXuatTT.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            celTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            celTonDKTTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTTT.DataBindings.Add("Text", DataSource, "NhapTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTuTrucTTT.DataBindings.Add("Text", DataSource, "NhapTuTuTrucTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatBHTTT.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatDVTTT.DataBindings.Add("Text", DataSource, "XuatDVTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTT.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTT.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCKTTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            celTonDKTTT1.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTTT1.DataBindings.Add("Text", DataSource, "NhapTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTuTrucTTT1.DataBindings.Add("Text", DataSource, "NhapTuTuTrucTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatBHTTT1.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatDVTTT1.DataBindings.Add("Text", DataSource, "XuatDVTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTT1.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTT1.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCKTTT1.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            celTonDKTTT2.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTTT2.DataBindings.Add("Text", DataSource, "NhapTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTuTrucTTT2.DataBindings.Add("Text", DataSource, "NhapTuTuTrucTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatBHTTT2.DataBindings.Add("Text", DataSource, "XuatBHTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatDVTTT2.DataBindings.Add("Text", DataSource, "XuatDVTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTT2.DataBindings.Add("Text", DataSource, "XuatKhacTT").FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTT2.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCKTTT2.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            colNhom.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            colTieuNhom.DataBindings.Add("Text", DataSource, "TenTN");

            GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));

            celTonDKTTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapTTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapTuTrucTTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatBHTTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatDVTTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTT.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTonCKTTT.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTonDKTTT1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapTTT1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapTuTrucTTT1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatBHTTT1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatDVTTT1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTT1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTT1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTonCKTTT1.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTonDKTTT2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapTTT2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapTuTrucTTT2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatBHTTT2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatDVTTT2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuatKhacTTT2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongXuatTTT2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTonCKTTT2.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtNguoiLApBieu.Text = DungChung.Bien.NguoiLapBieu;
            txtGiamDoc.Text = DungChung.Bien.GiamDoc;
            txtNgayLap.Text = "Ngày....tháng....năm......";//"Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            xtxTruongKS.Text = DungChung.Bien.TruongKhoaDuoc;
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
