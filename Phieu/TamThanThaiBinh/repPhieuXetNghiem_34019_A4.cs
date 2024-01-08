using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.Phieu.TamThanThaiBinh
{
    public partial class repPhieuXetNghiem_34019_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuXetNghiem_34019_A4()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            colYeuCau.DataBindings.Add("Text", DataSource, "TenDV");
            colThoiGianDuKien.DataBindings.Add("Text", DataSource, "ThoiGianDuKien");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

    }
}
