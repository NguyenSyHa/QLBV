using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCGiaoBan_NT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCGiaoBan_NT()
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

            celBHYT1.DataBindings.Add("Text", DataSource, "BHYT1");
            celDV1.DataBindings.Add("Text", DataSource, "DV1");
            celBNBHYTHT.DataBindings.Add("Text", DataSource, "BNBHYTHT");
            celBNDVHT.DataBindings.Add("Text", DataSource, "BNDVHT");
            celBNCKHT.DataBindings.Add("Text", DataSource, "BNCKHT");
            celTong1.DataBindings.Add("Text", DataSource, "Tong1");

            celBHYT1T.DataBindings.Add("Text", DataSource, "BHYT1");
            celDV1T.DataBindings.Add("Text", DataSource, "DV1");
            celBNHYTHTT.DataBindings.Add("Text", DataSource, "BNBHYTHT");
            celBNDVHTT.DataBindings.Add("Text", DataSource, "BNDVHT");
            celBNCKHTT.DataBindings.Add("Text", DataSource, "BNCKHT");
            celTong1T.DataBindings.Add("Text", DataSource, "Tong1");
        }
    }
}
