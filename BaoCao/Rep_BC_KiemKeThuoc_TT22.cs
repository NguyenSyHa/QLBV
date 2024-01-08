using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_KiemKeThuoc_TT22 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_KiemKeThuoc_TT22()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "20001")
            {
                SubBand2.Visible = true;
                SubBand6.Visible = true;
                SubBand4.Visible = true;
                SubBand1.Visible = false;
                SubBand3.Visible = false;
                SubBand5.Visible = false;

            }
            else
            {
                SubBand2.Visible = false;
                SubBand6.Visible = false;
                SubBand4.Visible = false;
                SubBand1.Visible = true;
                SubBand3.Visible = true;
                SubBand5.Visible = true;
            }
        }

        public void BindingData()
        {
            grp_Tieunhom.DataBindings.Add("Text", DataSource, "TenTN");
            cel_TenHoatChat.DataBindings.Add("Text", DataSource, "TenHC");
            cel_MaATC.DataBindings.Add("Text", DataSource, "MaATC");
            cel_HangSX.DataBindings.Add("Text", DataSource, "NhaSX");
            cel_TenBietDuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_SoDK.DataBindings.Add("Text", DataSource, "SoDangKy");
            cel_NuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            cel_NongDo.DataBindings.Add("Text", DataSource, "HamLuong");
            cel_DV.DataBindings.Add("Text", DataSource, "DonVi");
            cel_DuongDung.DataBindings.Add("Text", DataSource, "DuongD");
            cel_DonGia.DataBindings.Add("Text", DataSource, "DonGia");
            cel_TonDK.DataBindings.Add("Text", DataSource, "TonDK");
            cel_NhapMoi.DataBindings.Add("Text", DataSource, "NhapKho");
            //cel_NhapKhac.DataBindings.Add("Text", DataSource, "NhapTraLai");
            cel_XuatBN.DataBindings.Add("Text", DataSource, "XuatBN");
            cel_XuatKhac.DataBindings.Add("Text", DataSource, "XuatKhac");
            cel_TonCuoiKy.DataBindings.Add("Text", DataSource, "TonCuoi");
            cel_HangPP.DataBindings.Add("Text", DataSource, "Nhacc");

            lblMaNoiBo.DataBindings.Add("Text", DataSource, "MaTam");
            TenHoatChat.DataBindings.Add("Text", DataSource, "TenHC");
            MaATC.DataBindings.Add("Text", DataSource, "MaATC");
            HangSanXuat.DataBindings.Add("Text", DataSource, "NhaSX");
            TenBietDuoc.DataBindings.Add("Text", DataSource, "TenDV");
            SoDK.DataBindings.Add("Text", DataSource, "SoDangKy");
            NuocSanXuat.DataBindings.Add("Text", DataSource, "NuocSX");
            NongDoHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            DVDongGoi.DataBindings.Add("Text", DataSource, "DonVi");
            DuonDung.DataBindings.Add("Text", DataSource, "DuongD");
            DonGiaTheoDV.DataBindings.Add("Text", DataSource, "DonGia");
            TonDauKi.DataBindings.Add("Text", DataSource, "TonDK");
            NhapMuaMoi.DataBindings.Add("Text", DataSource, "NhapKho");
            //cel_NhapKhac.DataBindings.Add("Text", DataSource, "NhapTraLai");
            XuatBN.DataBindings.Add("Text", DataSource, "XuatBN");
            XuatKhac.DataBindings.Add("Text", DataSource, "XuatKhac");
            lblTonCuoiKi.DataBindings.Add("Text", DataSource, "TonCuoi");
            HangPhanPhoi.DataBindings.Add("Text", DataSource, "Nhacc");

           
        }
    }
}
