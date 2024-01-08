using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_Tonghopchuyenden : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_Tonghopchuyenden()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenBV.DataBindings.Add("Text", DataSource, "TBV");
            colTBN.DataBindings.Add("Text", DataSource, "TBN");
            colTBNC.DataBindings.Add("Text", DataSource, "TBN");
            colTBNBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            colTBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            colSL1a.DataBindings.Add("Text", DataSource, "a");
            colTSL1a.DataBindings.Add("Text", DataSource, "a");
            col1a.DataBindings.Add("Text", DataSource, "XX");
            colT1a.DataBindings.Add("Text", DataSource, "XX");
            colSL1b.DataBindings.Add("Text", DataSource, "b");
            colTSL1b.DataBindings.Add("Text", DataSource, "b");
            col1b.DataBindings.Add("Text", DataSource, "XX");
            colT1b.DataBindings.Add("Text", DataSource, "XX");
            colSL2.DataBindings.Add("Text", DataSource, "c");
            colTSL2.DataBindings.Add("Text", DataSource, "c");
            col2.DataBindings.Add("Text", DataSource, "XX");
            colT2.DataBindings.Add("Text", DataSource, "XX");
            colSL3.DataBindings.Add("Text", DataSource, "d");
            colTSL3.DataBindings.Add("Text", DataSource, "d");
            col3.DataBindings.Add("Text", DataSource, "XX");
            colT3.DataBindings.Add("Text", DataSource, "XX");
            colSL4.DataBindings.Add("Text", DataSource, "e");
            colTSL4.DataBindings.Add("Text", DataSource, "e");
            col4.DataBindings.Add("Text", DataSource, "XX");
            colT4.DataBindings.Add("Text", DataSource, "XX");
            colSL5.DataBindings.Add("Text", DataSource, "f");
            colTSL5.DataBindings.Add("Text", DataSource, "f");
            col5.DataBindings.Add("Text", DataSource, "XX");
            colT5.DataBindings.Add("Text", DataSource, "XX");
            colSLCDPH.DataBindings.Add("Text", DataSource, "CDPH");
            colTSLPH.DataBindings.Add("Text", DataSource, "CDPH");
            col6.DataBindings.Add("Text", DataSource, "XX");
            colT6.DataBindings.Add("Text", DataSource, "XX");
            colSLCDKB.DataBindings.Add("Text", DataSource, "CDKB");
            colTSLKB.DataBindings.Add("Text", DataSource, "CDKB");
            col7.DataBindings.Add("Text", DataSource, "XX");
            colT7.DataBindings.Add("Text", DataSource, "XX");
            colGC.DataBindings.Add("Text", DataSource, "XX");
            colTGH.DataBindings.Add("Text", DataSource, "XX");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            txtTenCq.Text = DungChung.Bien.TenCQ;
        }
    }
}
