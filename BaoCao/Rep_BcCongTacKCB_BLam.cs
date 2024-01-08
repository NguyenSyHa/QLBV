using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_BcCongTacKCB_BLam : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcCongTacKCB_BLam()
        {
            InitializeComponent();
        }
     

       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

      
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           

        }
    }
}
