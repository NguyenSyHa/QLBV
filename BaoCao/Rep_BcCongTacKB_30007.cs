using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcCongTacKB_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcCongTacKB_30007()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenKP.DataBindings.Add("Text", DataSource, "TenKP");
            colSLT.DataBindings.Add("Text", DataSource, "SLT");
            colSL1.DataBindings.Add("Text", DataSource, "SL1");
            colSL2.DataBindings.Add("Text", DataSource, "SL2");
            colSL3.DataBindings.Add("Text", DataSource, "SL3");
            colSL4.DataBindings.Add("Text", DataSource, "SL4");
            GroupHeader1.GroupFields.Add(new GroupField("TenKP"));
        }
        int i = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (i)
            {
                case 1:
                    colSTT.Text = "2.1";
                    break;
                case 2:
                    colSTT.Text = "2.2";
                    break;
                case 3:
                    colSTT.Text = "2.3";
                    break;
                case 4:
                    colSTT.Text = "2.4";
                    break;
                case 5:
                    colSTT.Text = "2.5";
                    break;
                case 6:
                    colSTT.Text = "2.6";
                    break;
                case 7:
                    colSTT.Text = "2.7";
                    break;
                case 8:
                    colSTT.Text = "2.8";
                    break;
                case 9:
                    colSTT.Text = "2.9";
                    break;
                case 10:
                    colSTT.Text = "2.10";
                    break;
                case 11:
                    colSTT.Text = "2.11";
                    break;
                case 12:
                    colSTT.Text = "2.12";
                    break;
            }
            i++;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
