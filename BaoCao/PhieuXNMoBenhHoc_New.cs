using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class PhieuXNMoBenhHoc_New : DevExpress.XtraReports.UI.XtraReport
    {
        public PhieuXNMoBenhHoc_New()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtCQCQ.Text = DungChung.Bien.TenCQCQ;
            txtCQ.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                xrLabel2.Visible = false;
            if (DungChung.Bien.MaBV == "27777")
            {
                picLogo.Image = DungChung.Ham.GetLogo();
            }
        }

    }
}
