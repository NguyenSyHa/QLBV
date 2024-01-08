using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCGiaoBan_NgT_Rutgon : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCGiaoBan_NgT_Rutgon()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tencqcq.Text = DungChung.Bien.TenCQCQ;
            tencq.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNLB.Text = DungChung.Bien.NguoiLapBieu;
            //celTP.Text = DungChung.Bien.tr
            celGD.Text = DungChung.Bien.GiamDoc;
        }

        public void BindingData()
        {
            celKP.DataBindings.Add("Text", DataSource, "TenKP");

            celTSBNTN.DataBindings.Add("Text", DataSource, "TSBNTN");
            celBHYTTT.DataBindings.Add("Text", DataSource, "BHYT");
            celDV.DataBindings.Add("Text", DataSource, "DV");

            celTSBNTNT.DataBindings.Add("Text", DataSource, "TSBNTN");
            celBHYTTTT.DataBindings.Add("Text", DataSource, "BHYT");
            celDVT.DataBindings.Add("Text", DataSource, "DV");
        }
    }
}
