using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuYCXNNongDoConTrongMau : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuYCXNNongDoConTrongMau()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            celtendvct.DataBindings.Add("Text", DataSource, "TenDVct");
            celkq.DataBindings.Add("Text", DataSource, "KetQua");
            celdonvi.DataBindings.Add("Text", DataSource, "DonVi");
            celtstb.DataBindings.Add("Text", DataSource, "TSBT");
            celmamay.DataBindings.Add("Text", DataSource, "MaMay");
        }

        private void rep_PhieuYCXNNongDoConTrongMau_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
