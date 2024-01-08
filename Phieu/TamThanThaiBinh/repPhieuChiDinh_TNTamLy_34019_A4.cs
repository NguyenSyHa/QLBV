using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.Phieu.TamThanThaiBinh
{
    public partial class repPhieuChiDinh_TNTamLy_34019_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChiDinh_TNTamLy_34019_A4()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        public void BindingData()
        {
            colYeuCau.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24272")
            {
                colYeuCau.DataBindings.Add("Text", DataSource, "TenRG");
            }
        }

    }
}
