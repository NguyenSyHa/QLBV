using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCNXTNew_30002 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCNXTNew_30002()
        {
            InitializeComponent();
        }
        public void databinding()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

            celTonDauSL.DataBindings.Add("Text", DataSource, "TonDauKySL").FormatString = DungChung.Bien.FormatString[0];
            celTonDauTT.DataBindings.Add("Text", DataSource, "TonDauKyTT").FormatString = DungChung.Bien.FormatString[1];
            celTonDauTTTong.DataBindings.Add("Text", DataSource, "TonDauKySL").FormatString = DungChung.Bien.FormatString[0];
            celTonDauSLTong.DataBindings.Add("Text", DataSource, "TonDauKyTT").FormatString = DungChung.Bien.FormatString[1];

            celNhapHDSL.DataBindings.Add("Text", DataSource, "NhapHDKTongSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapHDTT.DataBindings.Add("Text", DataSource, "NhapHDKTongTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapHDSLTong.DataBindings.Add("Text", DataSource, "NhapHDKTongSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapHDTTTong.DataBindings.Add("Text", DataSource, "NhapHDKTongTT").FormatString = DungChung.Bien.FormatString[1];

            celNhapCKSL.DataBindings.Add("Text", DataSource, "NhapCKVeKTongSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapCKTT.DataBindings.Add("Text", DataSource, "NhapCKVeKTongTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapCKSLTong.DataBindings.Add("Text", DataSource, "NhapCKVeKTongSL").FormatString = DungChung.Bien.FormatString[0];
            celNhapCKTTTong.DataBindings.Add("Text", DataSource, "NhapCKVeKTongTT").FormatString = DungChung.Bien.FormatString[1];

            celXuatSDSL.DataBindings.Add("Text", DataSource, "XuatSDSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatSDTT.DataBindings.Add("Text", DataSource, "XuatSDTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatSDSLTong.DataBindings.Add("Text", DataSource, "XuatSDSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatSDTTTong.DataBindings.Add("Text", DataSource, "XuatSDTT").FormatString = DungChung.Bien.FormatString[1];

            celXuatCKSL.DataBindings.Add("Text", DataSource, "XuatChuyenKhoSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatCKTT.DataBindings.Add("Text", DataSource, "XuatChuyenKhoTT").FormatString = DungChung.Bien.FormatString[1];
            celXuatCKSLTong.DataBindings.Add("Text", DataSource, "XuatChuyenKhoSL").FormatString = DungChung.Bien.FormatString[0];
            celXuatCKTTTong.DataBindings.Add("Text", DataSource, "XuatChuyenKhoTT").FormatString = DungChung.Bien.FormatString[1];

            celTonCuoiSL.DataBindings.Add("Text", DataSource, "TonCuoiKySL").FormatString = DungChung.Bien.FormatString[0];
            celTonCuoiTT.DataBindings.Add("Text", DataSource, "TonCuoiKyTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCuoiSLTong.DataBindings.Add("Text", DataSource, "TonCuoiKySL").FormatString = DungChung.Bien.FormatString[0];
            celTonCuoiTTTong.DataBindings.Add("Text", DataSource, "TonCuoiKyTT").FormatString = DungChung.Bien.FormatString[1];
           
        }
    }
}
