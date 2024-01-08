using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class rep_PhieuKB_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuKB_30007()
        {
            InitializeComponent();
            TenBV.Value = DungChung.Bien.TenCQ.ToUpper();
        }

    }
}
