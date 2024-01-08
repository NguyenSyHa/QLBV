using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_DanhMucThuoc_Mau16_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_DanhMucThuoc_Mau16_20001()
        {
            InitializeComponent();
        }

        public void BindingData() 
        {
            Group_TieuNhom.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            Group_HuongBH.DataBindings.Add("Text", DataSource, "TenLoaiHuongBH");

            cel_MaSoTheoDMT.DataBindings.Add("Text", DataSource, "MaQD");
            cel_TenViThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_DuongDung.DataBindings.Add("Text", DataSource, "DuongD");
            cel_HamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            cel_NhaSX.DataBindings.Add("Text", DataSource, "NhaSX");
            cel_NuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            cel_SoDangKy.DataBindings.Add("Text", DataSource, "SoDK");
            cel_DVT.DataBindings.Add("Text", DataSource, "DonVi");
            cel_GiaTrungThau.DataBindings.Add("Text", DataSource, "DonGia");
            cel_GiaThanhToanBHYT.DataBindings.Add("Text", DataSource, "DonGia2");
            cel_GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
            cel_ThanhPhan.DataBindings.Add("Text", DataSource, "TenHC");

            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhom"));
            GroupHeader2.GroupFields.Add(new GroupField("TenLoaiHuongBH"));
        }
    }
}
