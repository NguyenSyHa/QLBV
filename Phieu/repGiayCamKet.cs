using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class repGiayCamKet : DevExpress.XtraReports.UI.XtraReport
    {
        public repGiayCamKet()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
        }
    }
}
