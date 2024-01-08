using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PLThuoctheothang_TT01_A5 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PLThuoctheothang_TT01_A5()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            celTendv.DataBindings.Add("Text", DataSource, "TenDV");
            celSL.DataBindings.Add("Text", DataSource, "SoLuong");
            celDonvi.DataBindings.Add("Text", DataSource, "DonVi");

            //RFThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
            if (DungChung.Bien.MaBV == "14017")
            {
                this.SubBand3.Visible = false;
                this.SubBand4.Visible = true;
                xrLabel18.Visible = true;
                xrLabel19.Visible = true;
                this.SubBand1.Visible = false;
                this.SubBand2.Visible = false;
                this.SubBand5.Visible = true;
                PageHeader.Visible = false;
                celTenDV2.DataBindings.Add("Text", DataSource, "TenDV");
                celSL2.DataBindings.Add("Text", DataSource, "SoLuong");
                celDonvi2.DataBindings.Add("Text", DataSource, "DonVi");
                celMaDV2.DataBindings.Add("Text", DataSource, "MaTam");
                this.SubBand6.Visible = false;
                this.SubBand7.Visible = true;


            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            xrPictureBox1.Visible = DungChung.Bien.MaBV == "24297" ? true : false;
        }

        private void Donvi_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "24297")
                txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year ;
        }

        private void celghichu_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrLabel27_BeforePrint(object sender, CancelEventArgs e)
        {
            txtLuuY.Text = "Những điều cần lưu ý: Chia " + txtSoThang.Text + " thang sắc thuốc ngày 1 thang, ngày đầu chia chiều -tối, những ngày sau chia sáng chiều, sau ăn.";

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void celghichu2_BeforePrint(object sender, CancelEventArgs e)
        {
            celghichu2.Text = Convert.ToString(Convert.ToInt32(celSL2.Text) / Convert.ToInt32(txtSoThang.Text)) + "g/ngày";
        }
    }
}
