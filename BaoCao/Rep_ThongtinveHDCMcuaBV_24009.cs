using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_ThongtinveHDCMcuaBV_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ThongtinveHDCMcuaBV_24009()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            Cel_chiso.DataBindings.Add("Text", DataSource, "Chiso");
            Cel_2011.DataBindings.Add("Text", DataSource, "SLNamTruoc");
            Cel_2012.DataBindings.Add("Text", DataSource, "SLNamSau");
            Cel_sosanh.DataBindings.Add("Text", DataSource, "Tyle");

            celTenNhom.DataBindings.Add("Text", DataSource, "Tennhom");
            celNamTruocG.DataBindings.Add("Text", DataSource, "SLNamTruoc");
            celNamSauG.DataBindings.Add("Text", DataSource, "SLNamSau");
            GroupHeader1.GroupFields.Add(new GroupField("SttNhom"));
        }

        private void xrTableRow5_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Chiso") != null)
            {
                string nt = this.GetCurrentColumnValue("Chiso").ToString();
                if (nt == "")
                    xrTableRow5.Visible = false;
                else
                    xrTableRow5.Visible = true;

            }
        }
    }
}
