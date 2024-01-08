using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class PhieuTrathuocVTYT_A5 : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuTrathuocVTYT_A5()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:00}"; 
            }
            else
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");
            }
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienRP.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            coldongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
           txtTenCQCQ.Text= DungChung.Bien.TenCQCQ.ToUpper();

           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            if (DungChung.Bien.MaBV == "30003")
                xrTableCell13.Text = "Người trả".ToUpper();
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ... tháng ... năm 20...";
            if (DungChung.Bien.MaBV == "06007" || DungChung.Bien.MaBV == "02005" || DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "08204" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "30004")
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
            if (DungChung.Bien.MaBV == "30009") {
                tblKy1.Visible = false;
                tblKy2.Visible = true;
            }
            if(DungChung.Bien.MaBV=="30003"){
            xrTableCell31.Text="NGƯỜI NHẬN";
            }
                //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
        }

        private void SubBand2_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
     
}
