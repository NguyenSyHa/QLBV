using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_bia_SoBanGiaoHSBN1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_bia_SoBanGiaoHSBN1()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            //colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celcq.Text = DungChung.Bien.TenCQ;
            celcqcq.Text = DungChung.Bien.TenCQCQ;
        }
    }

}
