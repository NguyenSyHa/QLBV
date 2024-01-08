using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BcNXT_CM02 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BcNXT_CM02()
        {
            InitializeComponent();
        }
        bool HThi = true;
        public rep_BcNXT_CM02(bool ht)
        {
            InitializeComponent();
            HThi = ht;
        }
        public void BindingData()
        {
           // colTenHamLuongGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            colTenTieuNhomDV.DataBindings.Add("Text", DataSource, "TenTieuNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");

            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            //colSoLo.DataBindings.Add("Text", DataSource, "SoLo");

            colTonDKSL.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTT.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonDKTTTong.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            

            colNhapTKSL.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTT.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            colNhapTKTTTong.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatTKSL.DataBindings.Add("Text", DataSource, "XuatTKSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTT.DataBindings.Add("Text", DataSource, "XuatTKTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKTTTong.DataBindings.Add("Text", DataSource, "XuatTKTT").FormatString = DungChung.Bien.FormatString[1];

            colXuatTKKhacSL.DataBindings.Add("Text", DataSource, "XuatTKKhacSL").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKKhacTT.DataBindings.Add("Text", DataSource, "XuatTKKhacTT").FormatString = DungChung.Bien.FormatString[1];
            colXuatTKKhacTTTong.DataBindings.Add("Text", DataSource, "XuatTKKhacTT").FormatString = DungChung.Bien.FormatString[1];

            colTongXuatSL.DataBindings.Add("Text", DataSource, "TongXuatSL").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTT.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];
            colTongXuatTTTong.DataBindings.Add("Text", DataSource, "TongXuatTT").FormatString = DungChung.Bien.FormatString[1];

            colTonCKSL.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTT.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            colTonCKTTTong.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhomDV"));
            //GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            GroupHeader1.Visible = HThi;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKeToanTruong.Text = DungChung.Bien.KeToanTruong;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void txtTienBangChu_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (!string.IsNullOrEmpty(colTonCKTTTong.Text))
     
           // if (GetCurrentColumnValue("TongTien") != null)
            //{

            Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtTienBangChu.Text = DungChung.Ham.DocTienBangChu(st, " đồng./");
            //}
        }

    }
}
