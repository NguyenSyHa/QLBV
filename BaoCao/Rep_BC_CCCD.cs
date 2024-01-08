using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_CCCD : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_CCCD()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colMaBN.DataBindings.Add("Text", DataSource, "MaBN");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBN");
            colSoTheBHYT.DataBindings.Add("Text", DataSource, "SoTheBHYT");
            colBHYT.DataBindings.Add("Text", DataSource, "BHYT");
            colCCCD.DataBindings.Add("Text", DataSource, "CCCD");
        }
    }
}
