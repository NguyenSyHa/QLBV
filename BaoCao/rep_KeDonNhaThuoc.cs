using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_KeDonNhaThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_KeDonNhaThuoc()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celDVT.DataBindings.Add("Text", DataSource, "DonVi");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            celGhichu.DataBindings.Add("Text", DataSource, "GhiChu");

            //24272
            celTenDV24272.DataBindings.Add("Text", DataSource, "TenDV");
            celDVT24272.DataBindings.Add("Text", DataSource, "DonVi");
            celSL24272.DataBindings.Add("Text", DataSource, "SoLuong");
            celDonGia24272.DataBindings.Add("Text", DataSource, "DonGia");
            celThanhTien24272.DataBindings.Add("Text", DataSource, "ThanhTien");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24272")
            {
                //SubBand1.Visible = false;
                //SubBand2.Visible = false;
                //xrPictureBox1.Image = DungChung.Ham.GetLogo();
                GeneralReportHeader.Visible = GeneralDetail.Visible = GeneralReportFooter.Visible = false;
                ReportHeader24272.Visible = Detail24272.Visible = ReportFooter24272.Visible = true;
            }
        }
    }
}
