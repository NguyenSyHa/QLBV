using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuKhamCK : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuKhamCK()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = lblTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lblTenCQ.Text = lblTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }

    }
}
