using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class Rep_PhieuNhapDuoc_04007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNhapDuoc_04007()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrTableCell147.Text = "";
            }
        }
        public int Formatdate;
        public void BindingData() // Đối với phiếu nhập dược 04007
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                DateTime Ngay = DateTime.Now;
                colNgay.Text = "Bắc Giang, " + DungChung.Ham.NgaySangChu(System.DateTime.Now, 1);
            }
            else
            {
                colNgay.Text = DungChung.Ham.NgaySangChu(System.DateTime.Now, Formatdate);
            }
            colTenSP.DataBindings.Add("Text", DataSource, "TenDuoc");
            colMsvt.DataBindings.Add("Text", DataSource, "MaTam");
            colHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            colDVTinh.DataBindings.Add("Text", DataSource, "DonViTinh");
            colTheoChungTu.DataBindings.Add("Text", DataSource, "SoLuongN");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0: #,##}";
            colThucNhap.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = "{0: #,##}";
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = "{0: #,##}";

            colTenSP1.DataBindings.Add("Text", DataSource, "TenDuoc");
            colMsvt1.DataBindings.Add("Text", DataSource, "MaTam");
            colDVTinh1.DataBindings.Add("Text", DataSource, "DonViTinh");
            colTheoChungTu1.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = "{0: #,##}"; 
            colDonGia1.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[0];
            colThucNhap1.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = "{0: #,##}";
            colThanhTien1.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = "{0: #,##}";
        }
      
        private void colTheoChungTu_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable2.Visible = false;
                xrTable7.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable3.Visible = false;
                xrTable8.Visible = true;
            }
        }
    }
}
