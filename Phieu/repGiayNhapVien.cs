using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class repGiayNhapVien : DevExpress.XtraReports.UI.XtraReport
    {
        public repGiayNhapVien()
        {
            InitializeComponent();

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }
    }
}
