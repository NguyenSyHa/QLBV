using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class PhieuTrathuocVTYT_A5_2lien : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuTrathuocVTYT_A5_2lien()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");           
            colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");           
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienRP.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            coldongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");

            colTenHH2.DataBindings.Add("Text", DataSource, "TenDV");
            colsoluongyc2.DataBindings.Add("Text", DataSource, "SoLuong");
            colThanhTien2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienRP2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            coldongia2.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDVT2.DataBindings.Add("Text", DataSource, "DonVi");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
           txtTenCQCQ.Text= DungChung.Bien.TenCQCQ.ToUpper();

           txtTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
           txtTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();

           if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
           {
               PageFooter.Visible = true;
               SubBand1.Visible = false;
               SubBand2.Visible = false;
           }
            else if(DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
           {

               PageFooter.Visible = false;
               SubBand1.Visible = false;
               SubBand2.Visible = true ;
           }

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            if (DungChung.Bien.MaBV == "30003")
                xrTableCell13.Text = "Người trả".ToUpper();
            
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
           
           
                //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
    }
     
}
