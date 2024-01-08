using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_dutruthuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_dutruthuoc()
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

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
           txtTenCQCQ.Text= DungChung.Bien.TenCQCQ.ToUpper();

           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ... tháng ... năm 20...";
            if (DungChung.Bien.MaBV == "30003")
                xrTableCell14.Text = "trưởng|Phó khoa".ToUpper();
                //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;
        }
    }
     
}
