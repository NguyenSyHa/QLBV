using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_301_sub : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_301_sub()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            xrTableCell19.Text = DungChung.Bien.NguoiLapBieu;
            if(DungChung.Bien.MaBV=="27023")
            {
                Detail.Visible = false;
            }
            else
            {
                ReportHeader.Visible = false;
            }
        }

    }
}
