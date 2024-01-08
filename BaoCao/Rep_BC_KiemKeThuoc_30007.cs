using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_KiemKeThuoc_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_KiemKeThuoc_30007()
        {
            InitializeComponent();
        }

        public void BindingData() 
        {
            grp_Tieunhom.DataBindings.Add("Text", DataSource, "TenTN");
            cel_TenHoatChat.DataBindings.Add("Text", DataSource, "TenHC");
            cel_MaATC.DataBindings.Add("Text", DataSource, "MaATC");
            cel_MaNoiBo.DataBindings.Add("Text", DataSource, "MaTam");
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
            celVEN.DataBindings.Add("Text", DataSource, "VEN");

            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }
    }
}
