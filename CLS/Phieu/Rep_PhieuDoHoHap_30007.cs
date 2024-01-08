using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuDoHoHap_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuDoHoHap_30007()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
        public class BC
        {
            public string TenDV { set; get; }
            public string KQ1 { set; get; }
            public string KQ2 { set; get; }
            public string KQ3 { set; get; }
            public string KQ4 { set; get; }
            public string KQ5 { set; get; }
        }
        public void DataBindings()
        {
           
                col1.DataBindings.Add("Text", DataSource, "TenDV");
                col2.DataBindings.Add("Text", DataSource, "KQ1");
                col3.DataBindings.Add("Text", DataSource, "KQ2");
                col4.DataBindings.Add("Text", DataSource, "KQ3");
                col5.DataBindings.Add("Text", DataSource, "KQ4");
                col6.DataBindings.Add("Text", DataSource, "KQ5");
           
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30009")
            //{
            //    lab89.Visible = false;
            //    lab90.Visible = false;
            //    colBSDT.Visible = false;
            //}
            if (BSDT.Value != null)
            {
               
                colBSDT.Text = DungChung.Ham._getTenCB(data, BSDT.Value.ToString());
            }
            if (BSCK.Value != null)
            {
                colBSCK.Text = DungChung.Ham._getTenCB(data, BSCK.Value.ToString());
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

    }
}
