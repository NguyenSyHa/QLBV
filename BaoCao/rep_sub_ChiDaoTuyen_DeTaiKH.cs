using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_sub_ChiDaoTuyen_DeTaiKH : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_sub_ChiDaoTuyen_DeTaiKH()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celTenDeTai.DataBindings.Add("Text", DataSource, "TenDeTai");
            celSLCapNN.DataBindings.Add("Text", DataSource, "SLNN");
            celSLCapBo.DataBindings.Add("Text", DataSource, "SLB");
            celSLCapCS.DataBindings.Add("Text", DataSource, "SLCS");
        }
    }
}
