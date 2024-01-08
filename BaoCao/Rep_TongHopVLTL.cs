using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_TongHopVLTL : DevExpress.XtraReports.UI.XtraReport
    {
        int startIndex = 2;
        public Rep_TongHopVLTL()
        {
            InitializeComponent();
        }
        public void Binding()
        {
            if (DungChung.Bien.MaBV != "24012")
            {
                colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
                colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
                colThisMonth.DataBindings.Add("Text", DataSource, "SlThangNay");
                colPreviousMonth.DataBindings.Add("Text", DataSource, "SlThangTruoc");
            }
            else
            {
                colTenDV1.DataBindings.Add("Text", DataSource, "TenDV");
                colDonVi1.DataBindings.Add("Text", DataSource, "DonVi");
                colThisMonth1.DataBindings.Add("Text", DataSource, "SlThangNay");
                colPreviousMonth1.DataBindings.Add("Text", DataSource, "SlThangTruoc");
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        private void GroupHeader1_BeforePrint_1(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                GroupHeader1.Visible = true;
            }
            else
                GroupHeader1.Visible = false;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                SubBand2.Visible = true;
            }
            else
                SubBand1.Visible = true;
        }

        private void ColSTT_BeforePrint(object sender, CancelEventArgs e)
        {
            ColSTT.Text = startIndex.ToString();
            startIndex++;
        }
    }
}
