using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class Rep_PhieuNhapXuatDuoc_04007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNhapXuatDuoc_04007()
        {
            InitializeComponent();
            colNgay.Text = "Ngày " + "" + System.DateTime.Now.Day.ToString() + "" + " tháng " + "" + System.DateTime.Now.Month.ToString() + " " + " năm" + " " + System.DateTime.Now.Year.ToString() + ".";


        }

        public void BindingData() // Đối với phiếu nhập dược 04007
        {
            colcccq.Text = DungChung.Bien.TenCQCQ;
            colTenBV.Text = DungChung.Bien.TenCQ;
            colTenSP.DataBindings.Add("Text", DataSource, "TenDuoc");
            colMsvt.DataBindings.Add("Text", DataSource, "madv");
            colHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            colDVTinh.DataBindings.Add("Text", DataSource, "DonViTinh");
            colTheoChungTu.DataBindings.Add("Text", DataSource, "SoLuongN");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0: #,##}";
            colThucNhap.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = "{0: #,##}";
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = "{0: #,##}";
        }
        public void BindingData1() // Đối với phiếu xuất dượ 04007
        {
            colcccq.Text = DungChung.Bien.TenCQCQ;
            colTenBV.Text = DungChung.Bien.TenCQ;
            colTenSP.DataBindings.Add("Text", DataSource, "TenDV");
            colMsvt.DataBindings.Add("Text", DataSource, "SoLo");
            colHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            colDVTinh.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0: dd/MM/yyyy}";
            colTheoChungTu.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = "{0: #,##}";
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0: #,##}";
            colThucNhap.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = "{0: #,##}";
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = "{0: #,##}";
        }
        private void colTheoChungTu_BeforePrint(object sender, CancelEventArgs e)
        {
        }
    }
}
