using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_dutruthuoc_A5_2lien : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_dutruthuoc_A5_2lien()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString=DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienRP.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            coldongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");

            colTenHH2.DataBindings.Add("Text", DataSource, "TenDV");
            colsoluongyc2.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
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

           if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                {
                    txtNgayThang3.Text = "Ngày ... tháng ... năm 20...";
                    txtNgayThang4.Text = "Ngày ... tháng ... năm 20...";
                }
                PageFooter.Visible = true;
                SubBand1.Visible = false;
            }

           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
            {
                txtNgayThang.Text = "Ngày ... tháng ... năm 20...";
                txtNgayThang2.Text = "Ngày ... tháng ... năm 20...";
            }
            if (DungChung.Bien.MaBV == "30003")
                xrTableCell14.Text = "trưởng|Phó khoa".ToUpper();
                //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
        }
    }
     
}
