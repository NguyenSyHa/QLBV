using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_GiayChungNhanPhauThuat_TK : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_GiayChungNhanPhauThuat_TK()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(DungChung.Bien.GiamDoc))
            colGiamDoc.Text = DungChung.Bien.GiamDoc.ToUpper();
            colTruongKhoa.Text = DungChung.Bien.TruongKhoaLS.ToUpper();
        }

    }
}
