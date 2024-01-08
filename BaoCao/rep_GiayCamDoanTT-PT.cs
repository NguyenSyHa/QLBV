using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_GiayCamDoanTT_PT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_GiayCamDoanTT_PT()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrTableCell56.Visible = false;
                xrTableCell30.Visible = false;
            }
        }

    }
}
