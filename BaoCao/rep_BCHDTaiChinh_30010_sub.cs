using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCHDTaiChinh_30010_sub : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCHDTaiChinh_30010_sub()
        {
            InitializeComponent();
        }


        internal void dataBinding()
        {
            celSTT.DataBindings.Add("Text", DataSource, "STT");
            celChiTieu.DataBindings.Add("Text", DataSource, "ChiTieu");
            celVP.DataBindings.Add("Text", DataSource, "vp").FormatString = "{0:##,##}";
            celBH.DataBindings.Add("Text", DataSource, "bh").FormatString = "{0:##,##}";

            celVPT.DataBindings.Add("Text", DataSource, "vp").FormatString = "{0:##,##}";
            celBHT.DataBindings.Add("Text", DataSource, "bh").FormatString = "{0:##,##}";
        }
    }
}
