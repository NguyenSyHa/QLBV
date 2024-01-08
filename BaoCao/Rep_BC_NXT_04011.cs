using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_NXT_04011 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_NXT_04011()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            grp_Tieunhom.DataBindings.Add("Text", DataSource, "TenTN");
            celTenHC.DataBindings.Add("Text", DataSource, "TenHC");
            celMaATC.DataBindings.Add("Text", DataSource, "MaATC");
            celMaTam.DataBindings.Add("Text", DataSource, "MaTam");
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celSoDK.DataBindings.Add("Text", DataSource, "SoDangKy");
            celNoiSX.DataBindings.Add("Text", DataSource, "NuocSX");
            celNongDo.DataBindings.Add("Text", DataSource, "HamLuong");
            celDV.DataBindings.Add("Text", DataSource, "DonVi");
            celDuongDung.DataBindings.Add("Text", DataSource, "DuongD");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            celTonDK.DataBindings.Add("Text", DataSource, "TonDK");
            celNhap.DataBindings.Add("Text", DataSource, "NhapHD");
            celNhapKhac.DataBindings.Add("Text", DataSource, "NhapKhac");
            celXuat.DataBindings.Add("Text", DataSource, "Xuat");
            celXuatKhac.DataBindings.Add("Text", DataSource, "XuatKhac");
            celTonCK.DataBindings.Add("Text", DataSource, "TonCK");

            celTonDK_T.DataBindings.Add("Text", DataSource, "TonDK");
            celNhap_T.DataBindings.Add("Text", DataSource, "NhapHD");
            celNhapKhac_T.DataBindings.Add("Text", DataSource, "NhapKhac");
            celXuat_T.DataBindings.Add("Text", DataSource, "Xuat");
            celXuatKhac_T.DataBindings.Add("Text", DataSource, "XuatKhac");
            celTonCK_T.DataBindings.Add("Text", DataSource, "TonCK");

            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colThuKho.Text = DungChung.Bien.ThuKho;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            lblNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }
    }
}
