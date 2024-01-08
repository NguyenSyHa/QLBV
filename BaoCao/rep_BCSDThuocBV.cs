using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCSDThuocBV : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCSDThuocBV()
        {
            InitializeComponent();
        }
         public void BindingData()
        {
            celTen.DataBindings.Add("Text", DataSource, "TenDV");
            celDVT.DataBindings.Add("Text", DataSource, "DonVi").FormatString = DungChung.Bien.FormatString[1];
            celTonDauSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            celTonDauTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            celNhapTheoHD.DataBindings.Add("Text", DataSource, "NhapHD").FormatString = DungChung.Bien.FormatString[1];
            celNhapTraLai.DataBindings.Add("Text", DataSource, "NhapTL").FormatString = DungChung.Bien.FormatString[1];
            celNhapChuyenKho.DataBindings.Add("Text", DataSource, "NhapCK").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            celXuatTrongSL.DataBindings.Add("Text", DataSource, "XuatTKSL").FormatString = DungChung.Bien.FormatString[1];
            celXuatTrongTT.DataBindings.Add("Text", DataSource, "XuatTKTT").FormatString = DungChung.Bien.FormatString[1];
            celSDTrongSL.DataBindings.Add("Text", DataSource, "SDTKSL").FormatString = DungChung.Bien.FormatString[1];
            celSDTrongTT.DataBindings.Add("Text", DataSource, "SDTKTT").FormatString = DungChung.Bien.FormatString[1];
            celTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            celTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
