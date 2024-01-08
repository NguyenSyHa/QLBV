using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_Congtacchuyentuyenden_sub : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_Congtacchuyentuyenden_sub()
        {
            InitializeComponent();
        }


        internal void dataBinding()
        {
            cel_TenCS.DataBindings.Add("Text", DataSource, "tenCSKCB");
            cel_TS.DataBindings.Add("Text", DataSource, "TSBN");
            cel_Sthe.DataBindings.Add("Text", DataSource, "TSCoThe");
            cel_1a.DataBindings.Add("Text", DataSource, "HT1aSL");
            cel_1aTL.DataBindings.Add("Text", DataSource, "HT1aTL");
            cel_1b.DataBindings.Add("Text", DataSource, "HT1bSL");
            cel_1bTL.DataBindings.Add("Text", DataSource, "HT1bTL");
            cel_2.DataBindings.Add("Text", DataSource, "HT2SL");
            cel_2TL.DataBindings.Add("Text", DataSource, "HT2TL");
            cel_3.DataBindings.Add("Text", DataSource, "HT3SL");
            cel_3TL.DataBindings.Add("Text", DataSource, "HT3TL");
            cel_4.DataBindings.Add("Text", DataSource, "HT4SL");
            cel_4TL.DataBindings.Add("Text", DataSource, "HT4TL");
            cel_5.DataBindings.Add("Text", DataSource, "HT5SL");
            cel_5TL.DataBindings.Add("Text", DataSource, "HT5TL");
            cel_CDPhuhop.DataBindings.Add("Text", DataSource, "CdPH");
            cel_CDPhuhopTL.DataBindings.Add("Text", DataSource, "CdPHTL");
            cel_CDKhacBiet.DataBindings.Add("Text", DataSource, "CdKb");
            cel_CDKhacBietTL.DataBindings.Add("Text", DataSource, "CdKbTL");
            cel_GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");

           
            cel_TST.DataBindings.Add("Text", DataSource, "TSBN");
            cel_StheT.DataBindings.Add("Text", DataSource, "TSCoThe");
            cel_1aT.DataBindings.Add("Text", DataSource, "HT1aSL");
           // cel_1aTLT.DataBindings.Add("Text", DataSource, "HT1aTL");
            cel_1bT.DataBindings.Add("Text", DataSource, "HT1bSL");
           // cel_1bTLT.DataBindings.Add("Text", DataSource, "HT1bTL");
            cel_2T.DataBindings.Add("Text", DataSource, "HT2SL");
           // cel_2TLT.DataBindings.Add("Text", DataSource, "HT2TL");
            cel_3T.DataBindings.Add("Text", DataSource, "HT3SL");
           // cel_3TLT.DataBindings.Add("Text", DataSource, "HT3TL");
            cel_4T.DataBindings.Add("Text", DataSource, "HT4SL");
           // cel_4TLT.DataBindings.Add("Text", DataSource, "HT4TL");
            cel_5T.DataBindings.Add("Text", DataSource, "HT5SL");
           // cel_5TLT.DataBindings.Add("Text", DataSource, "HT5TL");
            cel_CDPhuhopT.DataBindings.Add("Text", DataSource, "CdPH");
          //  cel_CDPhuhopTLT.DataBindings.Add("Text", DataSource, "CdPHTL");
            cel_CDKhacBietT.DataBindings.Add("Text", DataSource, "CdKb");
           // cel_CDKhacBietTLT.DataBindings.Add("Text", DataSource, "CdKbTL");
            
           
        }

        private void xrTableCell22_BeforePrint(object sender, CancelEventArgs e)
        {
            int TSBN = 0, TS1a = 0, ts1b = 0, ts2 = 0, ts3 = 0, ts4 = 0, ts5 = 0, tsPH = 0, tsK = 0;
            if (!String.IsNullOrEmpty(cel_TST.Text))
            {
                TSBN = Convert.ToInt32(cel_TST.Text);
            }
            if (!String.IsNullOrEmpty(cel_1aT.Text))
            {
                TS1a = Convert.ToInt32(cel_1aT.Text);
                cel_1aTLT.Text = (Math.Round(((float)TS1a * 100 / TSBN), 2)).ToString();
            }
            if (!String.IsNullOrEmpty(cel_1bT.Text))
            {
                ts1b = Convert.ToInt32(cel_1bT.Text);
                cel_1bTLT.Text = (Math.Round(((float)ts1b * 100 / TSBN), 2)).ToString();
            }
            if (!String.IsNullOrEmpty(cel_2T.Text))
            {
                ts2 = Convert.ToInt32(cel_2T.Text);
                cel_2TLT.Text = (Math.Round(((float)ts2 * 100 / TSBN), 2)).ToString();
            }
            if (!String.IsNullOrEmpty(cel_3T.Text))
            {
                ts3 = Convert.ToInt32(cel_3T.Text);
                cel_3TLT.Text = (Math.Round(((float)ts3 * 100 / TSBN), 2)).ToString();
            }
            if (!String.IsNullOrEmpty(cel_4T.Text))
            {
                ts4 = Convert.ToInt32(cel_4T.Text);
                cel_4TLT.Text = (Math.Round(((float)ts4 * 100 / TSBN), 2)).ToString();
            }
            if (!String.IsNullOrEmpty(cel_5T.Text))
            {
                ts5 = Convert.ToInt32(cel_5T.Text);
                cel_5TLT.Text = (Math.Round(((float)ts5 * 100 / TSBN), 2)).ToString();
            }
            if (!String.IsNullOrEmpty(cel_CDPhuhopT.Text))
            {
                tsPH = Convert.ToInt32(cel_CDPhuhopT.Text);
                cel_CDPhuhopTLT.Text = (Math.Round(((float)tsPH * 100 / TSBN), 2)).ToString();
            }
            if (!String.IsNullOrEmpty(cel_CDKhacBietT.Text))
            {
                tsK = Convert.ToInt32(cel_CDKhacBietT.Text);
                cel_CDKhacBietTLT.Text = (Math.Round(((float)tsK * 100 / TSBN), 2)).ToString();
            }
            
        }
    }
}
