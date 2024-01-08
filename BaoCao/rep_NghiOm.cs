using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_NghiOm : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_NghiOm()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
     
           
           
        }
       public void datatext()
        {
            if (DungChung.Bien.MaBV == "30303")
            {
                lbTenCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                lblMaBV.Text = DungChung.Bien.TenCQ.ToUpper();
            }
            else
            {
                lbTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            }
        }

    }
}
