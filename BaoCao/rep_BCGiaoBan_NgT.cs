using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCGiaoBan_NgT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCGiaoBan_NgT()
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
            celTSBNDK.DataBindings.Add("Text", DataSource, "TSBNDK");
            celCC.DataBindings.Add("Text", DataSource, "CC");
            celBHYTDT.DataBindings.Add("Text", DataSource, "BHYTDT");
            celBHYTTT.DataBindings.Add("Text", DataSource, "BHYTTT");
            celDV.DataBindings.Add("Text", DataSource, "DV");
            celKSK.DataBindings.Add("Text", DataSource, "KSK");
            celBNVV.DataBindings.Add("Text", DataSource, "BNVV");
            celBNCV.DataBindings.Add("Text", DataSource, "BNCV");
            celTSDNCK.DataBindings.Add("Text", DataSource, "TSDNCK");

            celTSBNTNT.DataBindings.Add("Text", DataSource, "TSBNTN");
            celTSBNDKT.DataBindings.Add("Text", DataSource, "TSBNDK");
            celCCT.DataBindings.Add("Text", DataSource, "CC");
            celBHYTDTT.DataBindings.Add("Text", DataSource, "BHYTDT");
            celBHYTTTT.DataBindings.Add("Text", DataSource, "BHYTTT");
            celDVT.DataBindings.Add("Text", DataSource, "DV");
            celKSKT.DataBindings.Add("Text", DataSource, "KSK");
            celBNVVT.DataBindings.Add("Text", DataSource, "BNVV");
            celBNCVT.DataBindings.Add("Text", DataSource, "BNCV");
            celTSDNCKT.DataBindings.Add("Text", DataSource, "TSDNCK");
        }
    }
}
