using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class PhieuVPP_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuVPP_27022()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            clMaTam.DataBindings.Add("Text", DataSource, "MaTam");
            clTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            clYeucau.DataBindings.Add("Text", DataSource, "SoLuong");
            colThucte.DataBindings.Add("Text", DataSource, "SoLuong");
            clDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            clPrice.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[0];

            clSumPrice.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[0];
            
            clTotalPrice.DataBindings.Add("Text", DataSource, "ThanhTien");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtNgaythang.Text = "Ngày " + DateTime.Today.Day.ToString() + " tháng " + DateTime.Today.Month.ToString() + " năm " + DateTime.Today.Year.ToString();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtBoyte.Text.ToUpper();
            txtHospital.Text.ToUpper();
            txtFaculty.Text.ToUpper();
        }

    }
}
