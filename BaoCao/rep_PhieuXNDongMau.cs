using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuXNDongMau : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuXNDongMau()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        public void BindingData()
        {

            tenxetnghiem.DataBindings.Add("Text", DataSource, "tenxetnghiem");
            donvi.DataBindings.Add("Text", DataSource, "donvi");
            csbt.DataBindings.Add("Text", DataSource, "csbt");
            ketqua.DataBindings.Add("Text", DataSource, "ketqua");
        }
    }
}
