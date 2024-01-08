using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcHangXuatTheoDT_NS : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHangXuatTheoDT_NS()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTieuNhomDV.DataBindings.Add("Text", DataSource, "TieuNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");

            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

            colTonDKSL.DataBindings.Add("Text", DataSource, "TESL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TETT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTGF.DataBindings.Add("Text", DataSource, "TETT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TETT").FormatString = DungChung.Bien.FormatString[1];


            colNhapTKSL.DataBindings.Add("Text", DataSource, "NGSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NGTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTGF.DataBindings.Add("Text", DataSource, "NGTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NGTT").FormatString = DungChung.Bien.FormatString[1];

            colSDTKSL.DataBindings.Add("Text", DataSource, "ClSL").FormatString = DungChung.Bien.FormatString[1];
            colSDTKTT.DataBindings.Add("Text", DataSource, "CLTT").FormatString = DungChung.Bien.FormatString[1];
            colSDTKTTGF.DataBindings.Add("Text", DataSource, "CLTT").FormatString = DungChung.Bien.FormatString[1];
            colSDTKTTTong.DataBindings.Add("Text", DataSource, "CLTT").FormatString = DungChung.Bien.FormatString[1];

            colTonCKSL.DataBindings.Add("Text", DataSource, "TongSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TongTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTGF.DataBindings.Add("Text", DataSource, "TongTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TongTT").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TieuNhomDV"));
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
         }

    }
}
