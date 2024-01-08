using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNNhuomSoi : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNNhuomSoi()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            CELTENXN.DataBindings.Add("Text", DataSource, "TenDV");
            CELTENCT.DataBindings.Add("Text", DataSource, "TenDVct");
            celTSBT.DataBindings.Add("Text", DataSource, "TSBT");
            celKQ.DataBindings.Add("Text", DataSource, "KetQua");
            GroupHeader1.GroupFields.Add(new GroupField("TenDV"));
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (BSCD.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, BSCD.Value.ToString());
            }
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
        }

    }
}
