using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_BcThSoLuotBN_BLac : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcThSoLuotBN_BLac()
        {
            InitializeComponent();
        }
     

       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenBV1.Text = DungChung.Bien.TenCQ;
            colTenBV2.Text = DungChung.Bien.TenCQ;
            colTenBV3.Text = DungChung.Bien.TenCQ;
        }

      
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           

        }
    }
}
