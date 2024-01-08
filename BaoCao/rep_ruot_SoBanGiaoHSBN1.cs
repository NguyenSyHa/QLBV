using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_ruot_SoBanGiaoHSBN1 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ruot_SoBanGiaoHSBN1()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            tenba.DataBindings.Add("Text", DataSource, "TenBNhan");
            sohsba.DataBindings.Add("Text", DataSource, "SoBA");

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }

}
