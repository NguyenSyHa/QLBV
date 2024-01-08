using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BBKTTuVong : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BBKTTuVong()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = colTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = colTenCQ2.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }

    }
}
