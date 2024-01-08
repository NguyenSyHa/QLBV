using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCCTKCB : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCCTKCB()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        public void BindingData()
        {
            celNDBC.DataBindings.Add("Text", DataSource, "ND");
            celBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            celDV.DataBindings.Add("Text", DataSource, "DV");
            celTong.DataBindings.Add("Text", DataSource, "Tong");
        }
    }
}
