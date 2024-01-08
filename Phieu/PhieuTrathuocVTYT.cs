using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class PhieuTrathuocVTYT : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuTrathuocVTYT()
        {
            InitializeComponent();


        }
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            xrTableCell57.DataBindings.Add("Text", DataSource, "TenDV");
            xrTableCell64.DataBindings.Add("Text", DataSource, "MaTam");
            if (DungChung.Bien.MaBV == "24009")
            {
                xrTableCell10.Text = "TRƯỞNG KHOA DƯỢC";
                txtNguoiNhan.Text = "TRƯỞNG PHÒNG TÀI CHÍNH - KẾ TOÁN";
                txtNguoiTra.Text = "NGƯỜI LẬP PHIẾU";
                xrTableCell32.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:00}";
            }
            else
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");
                xrTableCell60.DataBindings.Add("Text", DataSource, "SoLuong");
            }
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell62.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienRP.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell75.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            coldongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            xrTableCell61.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            xrTableCell58.DataBindings.Add("Text", DataSource, "DonVi");
            colSoKS.DataBindings.Add("Text", DataSource, "SoLo");
            xrTableCell59.DataBindings.Add("Text", DataSource, "SoLo");


        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();

            if (DungChung.Bien.MaBV != "14017")
                SubBand1.Visible = false;

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30012")
            {
                SubBand2.Visible = true;
                tblKy2.Visible = true;
                txtNguoiNhan.Text = "TRƯỞNG PHÒNG TÀI CHÍNH - KẾ TOÁN";
                txtNguoiTra.Text = "NGƯỜI LẬP PHIẾU";
                xrTableCell32.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }
            if (DungChung.Bien.MaBV == "30003")
                xrTableCell13.Text = "Người trả".ToUpper();
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "" && DungChung.Bien.MaBV != "14017")
                txtNgayThang.Text = "Ngày ... tháng ... năm 20...";
            if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "02005" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "08204" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "30004")
            {
                if (DungChung.Bien.MaBV == "08204" || DungChung.Bien.MaBV == "30003")
                    xrTableCell15.Text = "Người nhận thuốc".ToUpper();
                else
                    xrTableCell15.Text = "Thủ kho".ToUpper();
                colThuKho.Text = DungChung.Bien.ThuKho;
            }
            else
            {
                xrTableCell15.Text = "TRƯỞNG PHÒNG TÀI CHÍNH - KẾ TOÁN";
                colThuKho.Text = "";
            }
            if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV != "30012")
            {
                tblKy1.Visible = false;
                tblKy2.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30003")
            {
                txtNguoiTra.Text = "NGƯỜI NHẬN";
            }
            if (DungChung.Bien.MaBV == "14017")
            {
                SubBand3.Visible = true;
                SubBand2.Visible = false;
                tblKy1.Visible = true;
            }
            else
            {
                SubBand3.Visible = false;
            }
            //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
            if (DungChung.Bien.MaBV == "24012")
            {
                txtNguoiNhan.Text = "NGƯỜI NHẬN";
                txtNguoiTra.Text = "NGƯỜI TRẢ";
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

            if (DungChung.Bien.MaBV == "14017")
                xrTable2.Visible = xrLine1.Visible = false;
            else
                xrTable6.Visible = false;
            //xrTable2.Visible = true;
            xrTable6.Size = new Size(517, 35);
            if (DungChung.Bien.MaBV == "14017")
            {
                SubBand4.Visible = true;
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017")
                xrTable1.Visible = false;
        }

    }

}
