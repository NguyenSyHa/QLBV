using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_TamUngTN : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TamUngTN()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            this.TenCQCQ.Value= DungChung.Bien.TenCQCQ;
            this.TenCQ.Value = DungChung.Bien.TenCQ;
            this.NguoiLapBieu.Value = DungChung.Bien.NguoiLapBieu;
        }

    }
}
