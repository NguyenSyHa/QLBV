using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNNuocTieuPhanDCD_TKA5 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNNuocTieuPhanDCD_TKA5()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colYCKT.DataBindings.Add("Text", DataSource, "TenDV");

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            //colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
        }
    }
}
