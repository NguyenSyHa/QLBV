using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNKhac_TKA5 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNKhac_TKA5()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper(); ;
           // colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            if (BSDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, BSDT.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
        }

    }
}
