using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_BcKhoaKB_BLac : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcKhoaKB_BLac()
        {
            InitializeComponent();
        }
     

       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

      
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           

        }
    }
}
