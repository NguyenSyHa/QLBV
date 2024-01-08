using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_DanhMucThuoc_Mau17_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_DanhMucThuoc_Mau17_20001()
        {
            InitializeComponent();
        }

        public void BindingData() 
        {
            cel_BaoQuan.DataBindings.Add("Text", DataSource, "TyLeBQ");
            //cel_BoPhan.DataBindings.Add("Text", DataSource, "BoPhan");
            //cel_ChiPhiKhac.DataBindings.Add("Text", DataSource, "ChiPhiKhac");
            cel_DVT.DataBindings.Add("Text", DataSource, "DonVi");
            cel_GiaDeNghi.DataBindings.Add("Text", DataSource, "DonGia2");
            cel_GiaNhap.DataBindings.Add("Text", DataSource, "DonGia");
            cel_MaSoTheoDMT.DataBindings.Add("Text", DataSource, "MaQD");
            cel_NhaSX.DataBindings.Add("Text", DataSource, "NhaSX");
            cel_NuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            cel_TenKhoaHocCuaThanhPhan.DataBindings.Add("Text", DataSource, "TenHC");
            cel_TenKhoaHocCuaViThuoc.DataBindings.Add("Text", DataSource, "TenHC");
            cel_TenViThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_TinhTrang.DataBindings.Add("Text", DataSource, "TinhTNhap");
            //cel_TrongCheBien.DataBindings.Add("Text", DataSource, "TrongCheBien");
            cel_YeuCau.DataBindings.Add("Text", DataSource, "YCSD");
            cel_NguonGoc.DataBindings.Add("Text", DataSource, "NguonGoc");
        }
    }
}
