using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BbHoiChan : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BbHoiChan()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV=="20001")
            {
                xrLabel21.Visible = false;
                xrLabel1.Visible = false;
                xrLine1.Visible = true;
            }

        }

    }
}
