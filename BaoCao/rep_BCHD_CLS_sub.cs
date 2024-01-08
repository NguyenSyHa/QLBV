using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCHD_CLS_sub : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCHD_CLS_sub()
        {
            InitializeComponent();
        }


        internal void binding()
        {
           
            celLoaiXN.DataBindings.Add("Text", DataSource, "TenTN");
            celTongSo.DataBindings.Add("Text", DataSource, "TongXN");
           
        }
    }
}
