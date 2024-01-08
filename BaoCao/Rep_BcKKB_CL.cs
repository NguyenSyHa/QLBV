using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcKKB_CL : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcKKB_CL()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            txtNTN.DataBindings.Add("Text", DataSource, "NTN");

            c1.DataBindings.Add("Text", DataSource, "SL1").FormatString = DungChung.Bien.FormatString[1];
            c2.DataBindings.Add("Text", DataSource, "SL2").FormatString = DungChung.Bien.FormatString[1];
            c3.DataBindings.Add("Text", DataSource, "Sl3").FormatString = DungChung.Bien.FormatString[1];
            c4.DataBindings.Add("Text", DataSource, "Sl4").FormatString = DungChung.Bien.FormatString[1];
            c5.DataBindings.Add("Text", DataSource, "Sl5").FormatString = DungChung.Bien.FormatString[1];
            c6.DataBindings.Add("Text", DataSource, "Sl6").FormatString = DungChung.Bien.FormatString[1];
            c7.DataBindings.Add("Text", DataSource, "SL7").FormatString = DungChung.Bien.FormatString[1];
            c8.DataBindings.Add("Text", DataSource, "SL8").FormatString = DungChung.Bien.FormatString[1];
            c9.DataBindings.Add("Text", DataSource, "SL9").FormatString = DungChung.Bien.FormatString[1];
            c10.DataBindings.Add("Text", DataSource, "SL10").FormatString = DungChung.Bien.FormatString[1];
            c11.DataBindings.Add("Text", DataSource, "Sl11").FormatString = DungChung.Bien.FormatString[1];
            c12.DataBindings.Add("Text", DataSource, "SL12").FormatString = DungChung.Bien.FormatString[1];
            c13.DataBindings.Add("Text", DataSource, "SL13").FormatString = DungChung.Bien.FormatString[1];
            c14.DataBindings.Add("Text", DataSource, "SL14").FormatString = DungChung.Bien.FormatString[1];
            c15.DataBindings.Add("Text", DataSource, "SL15").FormatString = DungChung.Bien.FormatString[1];
            c16.DataBindings.Add("Text", DataSource, "SL16").FormatString = DungChung.Bien.FormatString[1];
            c17.DataBindings.Add("Text", DataSource, "SL17").FormatString = DungChung.Bien.FormatString[1];
            c18.DataBindings.Add("Text", DataSource, "SL18").FormatString = DungChung.Bien.FormatString[1];
            c19.DataBindings.Add("Text", DataSource, "SL19").FormatString = DungChung.Bien.FormatString[1];
            c20.DataBindings.Add("Text", DataSource, "SL20").FormatString = DungChung.Bien.FormatString[1];
            c21.DataBindings.Add("Text", DataSource, "SL21").FormatString = DungChung.Bien.FormatString[1];
            c22.DataBindings.Add("Text", DataSource, "SL22").FormatString = DungChung.Bien.FormatString[1];
            c23.DataBindings.Add("Text", DataSource, "SL23").FormatString = DungChung.Bien.FormatString[1];
            c24.DataBindings.Add("Text", DataSource, "SL24").FormatString = DungChung.Bien.FormatString[1];
            c25.DataBindings.Add("Text", DataSource, "SL25").FormatString = DungChung.Bien.FormatString[1];
            c26.DataBindings.Add("Text", DataSource, "SL26").FormatString = DungChung.Bien.FormatString[1];
            c27.DataBindings.Add("Text", DataSource, "SL27").FormatString = DungChung.Bien.FormatString[1];
            c28.DataBindings.Add("Text", DataSource, "SL28").FormatString = DungChung.Bien.FormatString[1];
            c29.DataBindings.Add("Text", DataSource, "SL29").FormatString = DungChung.Bien.FormatString[1];
            c30.DataBindings.Add("Text", DataSource, "SL30").FormatString = DungChung.Bien.FormatString[1];

            c1T.DataBindings.Add("Text", DataSource, "sl1").FormatString = DungChung.Bien.FormatString[1];
            c2T.DataBindings.Add("Text", DataSource, "sl2").FormatString = DungChung.Bien.FormatString[1];
            c3T.DataBindings.Add("Text", DataSource, "SL3").FormatString = DungChung.Bien.FormatString[1];
            c4T.DataBindings.Add("Text", DataSource, "SL4").FormatString = DungChung.Bien.FormatString[1];
            c5T.DataBindings.Add("Text", DataSource, "SL5").FormatString = DungChung.Bien.FormatString[1];
            c6T.DataBindings.Add("Text", DataSource, "SL6").FormatString = DungChung.Bien.FormatString[1];
            c7T.DataBindings.Add("Text", DataSource, "SL7").FormatString = DungChung.Bien.FormatString[1];
            c8T.DataBindings.Add("Text", DataSource, "SL8").FormatString = DungChung.Bien.FormatString[1];
            c9T.DataBindings.Add("Text", DataSource, "SL9").FormatString = DungChung.Bien.FormatString[1];
            c10T.DataBindings.Add("Text", DataSource, "SL10").FormatString = DungChung.Bien.FormatString[1];
            c11T.DataBindings.Add("Text", DataSource, "Sl11").FormatString = DungChung.Bien.FormatString[1];
            c12T.DataBindings.Add("Text", DataSource, "SL12").FormatString = DungChung.Bien.FormatString[1];
            c13T.DataBindings.Add("Text", DataSource, "SL13").FormatString = DungChung.Bien.FormatString[1];
            c14T.DataBindings.Add("Text", DataSource, "SL14").FormatString = DungChung.Bien.FormatString[1];
            c15T.DataBindings.Add("Text", DataSource, "SL15").FormatString = DungChung.Bien.FormatString[1];
            c16T.DataBindings.Add("Text", DataSource, "SL16").FormatString = DungChung.Bien.FormatString[1];
            c17T.DataBindings.Add("Text", DataSource, "SL17").FormatString = DungChung.Bien.FormatString[1];
            c18T.DataBindings.Add("Text", DataSource, "SL18").FormatString = DungChung.Bien.FormatString[1];
            c19T.DataBindings.Add("Text", DataSource, "SL19").FormatString = DungChung.Bien.FormatString[1];
            c20T.DataBindings.Add("Text", DataSource, "SL20").FormatString = DungChung.Bien.FormatString[1];
            c21T.DataBindings.Add("Text", DataSource, "SL21").FormatString = DungChung.Bien.FormatString[1];
            c22T.DataBindings.Add("Text", DataSource, "SL22").FormatString = DungChung.Bien.FormatString[1];
            c23T.DataBindings.Add("Text", DataSource, "SL23").FormatString = DungChung.Bien.FormatString[1];
            c24T.DataBindings.Add("Text", DataSource, "SL24").FormatString = DungChung.Bien.FormatString[1];
            c25T.DataBindings.Add("Text", DataSource, "SL25").FormatString = DungChung.Bien.FormatString[1];
            c26T.DataBindings.Add("Text", DataSource, "SL26").FormatString = DungChung.Bien.FormatString[1];
            c27T.DataBindings.Add("Text", DataSource, "SL27").FormatString = DungChung.Bien.FormatString[1];
            c28T.DataBindings.Add("Text", DataSource, "SL28").FormatString = DungChung.Bien.FormatString[1];
            c29T.DataBindings.Add("Text", DataSource, "SL29").FormatString = DungChung.Bien.FormatString[1];
            c30T.DataBindings.Add("Text", DataSource, "SL30").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        //    colTTDV.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void xrTable12_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void colNTN_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NTN") != null)
            {
                string _nt = this.GetCurrentColumnValue("NTN").ToString();
                colNTN.Text = _nt.ToString().Substring(0, 5);
            }
            else colNTN.Text = "";
        }
    }
}
