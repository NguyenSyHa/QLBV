using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuXNBNDTNgT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuXNBNDTNgT()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = txtcqcq2.Text = DungChung.Bien.TenCQCQ;
            txtcq.Text = txtcq2.Text = DungChung.Bien.TenCQ;
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "30010")
            {
                xrTableCell40.Text = "GIÁM ĐỐC TRUNG TÂM";
                xrTableCell8.Visible = false;
                xrTableCell10.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }
    }
}
