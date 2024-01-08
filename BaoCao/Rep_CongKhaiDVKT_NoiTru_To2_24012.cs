using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLBV.BaoCao
{
    public partial class Rep_CongKhaiDVKT_NoiTru_To2_24012 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_CongKhaiDVKT_NoiTru_To2_24012()
        {
            InitializeComponent();
        }
        public void BindingData()
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand3.Visible = false;
                SubBand4.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }
    }
}
