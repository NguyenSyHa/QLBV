using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_GiayGiuThe_Sub2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_GiayGiuThe_Sub2()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            celKP2.DataBindings.Add("Text", DataSource, "TenKP");
        }
    }
}
