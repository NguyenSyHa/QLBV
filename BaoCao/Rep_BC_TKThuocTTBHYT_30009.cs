using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_TKThuocTTBHYT_30009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_TKThuocTTBHYT_30009()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblCSYT.Text = "Tên CSYT: " + DungChung.Bien.TenCQ;
            lblMaCSYT.Text = "Mã CSYT: " + DungChung.Bien.MaBV;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colThuKho.Text = DungChung.Bien.ThuKho;
            celTruongKhoa.Text = DungChung.Bien.TruongKhoaDuoc;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        public void BindingData()
        {
            celMaHC.DataBindings.Add("Text", DataSource, "MaHC");
            celHoatChat.DataBindings.Add("Text", DataSource, "TenHC");
            celMaDD.DataBindings.Add("Text", DataSource, "MaDuongDung");
            celDuongDung.DataBindings.Add("Text", DataSource, "DuongD");
            celHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celSDK.DataBindings.Add("Text", DataSource, "SoDK");
            celDGoi.DataBindings.Add("Text", DataSource, "QCPC");
            celDV.DataBindings.Add("Text", DataSource, "DonVi");
            celNgTruSL.DataBindings.Add("Text", DataSource, "NgoaiTruSL").FormatString = DungChung.Bien.FormatString[1];
            celNTruSL.DataBindings.Add("Text", DataSource, "NoiTruSL").FormatString = DungChung.Bien.FormatString[1];
            celTong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            celMaCSKCB.DataBindings.Add("Text", DataSource, "MaKCB");
            celMaThuoc.DataBindings.Add("Text", DataSource, "MaTam");

            celThanhTien_T.DataBindings.Add("Text", DataSource, "ThanhTien");
            celTongSL.DataBindings.Add("Text", DataSource, "SoLuong");
            celTongNgTru.DataBindings.Add("Text", DataSource, "NgoaiTruSL");
            celtongNTru.DataBindings.Add("Text", DataSource, "NoiTruSL");
        }
    }
}
