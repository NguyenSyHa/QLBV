using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuLinhMau : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuLinhMau()
        {
            InitializeComponent();
        }

        private void PhieuLinhMau_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = txtcqcq2.Text = DungChung.Bien.TenCQCQ;
            txtcq.Text = txtcq2.Text = DungChung.Bien.TenCQ;
            xrLabel52.Text = xrLabel142.Text = DungChung.Bien.TenCQCQ;
            xrLabel50.Text = xrLabel140.Text = DungChung.Bien.TenCQ;
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }

    }
}
