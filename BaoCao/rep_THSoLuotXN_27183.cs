using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_THSoLuotXN_27183 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_THSoLuotXN_27183()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            cellTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            cellTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            cellTenDV.DataBindings.Add("Text", DataSource, "TenDV");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            labTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
