using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class rep_XNTeBao : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_XNTeBao()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrLabel1.Visible = false;
                xrLabel2.Visible = false;
            }
        }
        public void Databind()
        {
            lblMaDungChung.DataBindings.Add("Text", DataSource, "MaQD");
            lblTenDichVu.DataBindings.Add("Text", DataSource, "TenDV");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049")
            {
                lblBarcode.Visible = txtBarcode.Visible = false;
            }

            if (DungChung.Bien.MaBV == "27777")
            {
                picLogo.Image = DungChung.Ham.GetLogo();
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            }

            if (DungChung.Bien.MaBV == "24272")
            {
                picLogo.Visible = false;
                xrPictureBox1.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }

            if (DungChung.Bien.MaBV == "30372")
            {
                picLogo.Visible = false;
                xrPictureBox2.Visible = true;
            }
        }
    }
}
