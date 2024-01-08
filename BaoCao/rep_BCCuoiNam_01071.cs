using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCCuoiNam_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCCuoiNam_01071()
        {
            InitializeComponent();
        }

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
        }

        public void BindingData()
        {
            celnoidung.DataBindings.Add("Text", DataSource, "NoiDung");
            celnamnay.DataBindings.Add("Text", DataSource, "KetQuaNamNay");
            celnamngoai.DataBindings.Add("Text", DataSource, "KetQuaNamNgoai");
            celsosanh.DataBindings.Add("Text", DataSource, "KetQuaSoSanh");
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
