using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcNXTTheoXa_CM08 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcNXTTheoXa_CM08()
        {
            InitializeComponent();
        }
        bool HThi = true;
        public Rep_BcNXTTheoXa_CM08(bool ht)
        {
            InitializeComponent();
            HThi = ht;
        }
        public void BindingData()
        {
            // colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            colTieuNhomDV.DataBindings.Add("Text", DataSource, "TieuNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");

            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];


            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            colTongNhapSL.DataBindings.Add("Text", DataSource, "TongNhapSL").FormatString = DungChung.Bien.FormatString[0];
            colTongNhapTT.DataBindings.Add("Text", DataSource, "TongNhapTT").FormatString = DungChung.Bien.FormatString[1];
            colTongNhapTT_RF.DataBindings.Add("Text", DataSource, "TongNhapTT").FormatString = DungChung.Bien.FormatString[1];

            colTongTraSL.DataBindings.Add("Text", DataSource, "TongTraSL").FormatString = DungChung.Bien.FormatString[0];
            colTongTraTT.DataBindings.Add("Text", DataSource, "TongTraTT").FormatString = DungChung.Bien.FormatString[1];
            colTongTraTT_RF.DataBindings.Add("Text", DataSource, "TongTraTT").FormatString = DungChung.Bien.FormatString[1];

            colSDTKSL.DataBindings.Add("Text", DataSource, "SDTKSL").FormatString = DungChung.Bien.FormatString[0];
            colSDTKTT.DataBindings.Add("Text", DataSource, "SDTKTT").FormatString = DungChung.Bien.FormatString[1];
            colSDTKTTTong.DataBindings.Add("Text", DataSource, "SDTKTT").FormatString = DungChung.Bien.FormatString[1];

            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TieuNhomDV"));
            //GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            GroupHeader1.Visible = HThi;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell8.Text = "PHÓ TRƯỞNG KHOA PHÒNG KHÁM_DƯỢC_CLS";
            }
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            xrThuKho.Text = DungChung.Bien.ThuKho;
        }

   

    }
}
