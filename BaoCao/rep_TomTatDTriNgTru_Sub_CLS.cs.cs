using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TomTatDTriNgTru_Sub_CLS : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TomTatDTriNgTru_Sub_CLS()
        {
            InitializeComponent();
        }
        public void BindData()
        {
            celKQ.DataBindings.Add("Text", DataSource, "KetQua");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");

        }
    }
}
