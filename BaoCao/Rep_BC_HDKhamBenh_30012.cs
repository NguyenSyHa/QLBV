using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_HDKhamBenh_30012 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_HDKhamBenh_30012()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lbl_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lbl_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celTruongKhoa.Text = DungChung.Bien.TruongKhoaLS;
            celLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            cel_ChuyenKhoa.DataBindings.Add("Text", DataSource, "ChuyenKhoa");
            cel_TongSo.DataBindings.Add("Text", DataSource, "TongSo");
            cel_BHYT.DataBindings.Add("Text", DataSource, "BHYT");
            cel_VienPhi.DataBindings.Add("Text", DataSource, "VienPhi");
            cel_KhongThuDuoc.DataBindings.Add("Text", DataSource, "KhongThuDuoc");
            cel_CapCuu.DataBindings.Add("Text", DataSource, "CapCuu");
            cel_Duoi1T.DataBindings.Add("Text", DataSource, "Duoi1T");
            cel_Duoi6T.DataBindings.Add("Text", DataSource, "Duoi6T");
            cel_Duoi15.DataBindings.Add("Text", DataSource, "Duoi15T");
            cel_Tren60.DataBindings.Add("Text", DataSource, "Tren60");
            cel_BNVaoVien.DataBindings.Add("Text", DataSource, "BNVaoVien");
            cel_BNChuyenVien.DataBindings.Add("Text", DataSource, "BNChuyenVien");
            cel_DTKhamMP.DataBindings.Add("Text", DataSource, "DTKhacMP");
            cel_DTNgheo.DataBindings.Add("Text", DataSource, "DTNgheo");

            rf_TongSo.DataBindings.Add("Text", DataSource, "TongSo");
            rf_BHYT.DataBindings.Add("Text", DataSource, "BHYT");
            rf_VienPhi.DataBindings.Add("Text", DataSource, "VienPhi");
            rf_KhongThuDuoc.DataBindings.Add("Text", DataSource, "KhongThuDuoc");
            rf_CapCuu.DataBindings.Add("Text", DataSource, "CapCuu");
            rf_Duoi1T.DataBindings.Add("Text", DataSource, "Duoi1T");
            rf_Duoi6T.DataBindings.Add("Text", DataSource, "Duoi6T");
            rf_Duoi15.DataBindings.Add("Text", DataSource, "Duoi15T");
            rf_Tren60.DataBindings.Add("Text", DataSource, "Tren60");
            rf_BNVaoVien.DataBindings.Add("Text", DataSource, "BNVaoVien");
            rf_BNChuyenVien.DataBindings.Add("Text", DataSource, "BNChuyenVien");
            rf_DTKhamMP.DataBindings.Add("Text", DataSource, "DTKhacMP");
            rf_DTNgheo.DataBindings.Add("Text", DataSource, "DTNgheo");
        }

    }
}
