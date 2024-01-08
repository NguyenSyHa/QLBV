using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BNThucHienCLSTheoNgay : DevExpress.XtraReports.UI.XtraReport
    {
        bool isHienThiGio = false;
        public rep_BNThucHienCLSTheoNgay()
        {
            InitializeComponent();
        }
        public rep_BNThucHienCLSTheoNgay(bool isHienThiGio)
        {
            InitializeComponent();
            this.isHienThiGio = isHienThiGio;
        }
        public void databind()
        {
            ColMaBNhan.DataBindings.Add("Text", DataSource, "MabNhan");
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNgaySinh.DataBindings.Add("Text", DataSource, "NgaySinh");
            colDoiTuong.DataBindings.Add("Text", DataSource, "DoiTuong");
            colTenDichVu.DataBindings.Add("Text", DataSource, "TenDichVu");
            ColDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[0]; 
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            ColThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[0]; 
            colNgayChiDinh.DataBindings.Add("Text", DataSource, "NgayChiDinh").FormatString = isHienThiGio ? "{0: HH:mm dd/MM/yyyy}" : "{0: dd/MM/yyyy}";
            colNgayThucHien.DataBindings.Add("Text", DataSource, "NgayThucHien").FormatString = isHienThiGio ? "{0: HH:mm dd/MM/yyyy}" : "{0: dd/MM/yyyy}";
            colNgayThanhToan.DataBindings.Add("Text", DataSource, "NgayThanhToan").FormatString = isHienThiGio ? "{0: HH:mm dd/MM/yyyy}" : "{0: dd/MM/yyyy}";
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24272")
            {
                xrTableCell20.Text = "Thời gian chỉ định";
                xrTableCell21.Text = "Thời gian thực hiện";
            }
        }
    }
}
