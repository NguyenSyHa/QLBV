using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLBV.Phieu
{
    public partial class repDonThuoc_TT04_N : DevExpress.XtraReports.UI.XtraReport
    {
        public repDonThuoc_TT04_N()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            DonVi.DataBindings.Add("Text", DataSource, "DonVi");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");

        }

        private void SubBand2_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = colTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            colDiaChiCQ.Text = colDiaChiCQ2.Text = "Địa chỉ: " + DungChung.Ham.GetDiaChiBV();
            txtSDT.Text = "Điện thoại: " + DungChung.Ham.GetSDTBV();
            txtSDT.Text += DungChung.Ham.GetFaxBV() == null ? null : " - " + DungChung.Ham.GetFaxBV();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand5.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }
    }
}
