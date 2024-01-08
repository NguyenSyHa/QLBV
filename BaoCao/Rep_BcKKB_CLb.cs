using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_BcKKB_CLb : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcKKB_CLb()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            txtNTN.DataBindings.Add("Text", DataSource, "NTN").FormatString = "{0:dd/MM}";

            c1.DataBindings.Add("Text", DataSource, "SL31").FormatString = DungChung.Bien.FormatString[1];
            c2.DataBindings.Add("Text", DataSource, "SL32").FormatString = DungChung.Bien.FormatString[1];
            c3.DataBindings.Add("Text", DataSource, "Sl33").FormatString = DungChung.Bien.FormatString[1];
            c4.DataBindings.Add("Text", DataSource, "Sl34").FormatString = DungChung.Bien.FormatString[1];
            c5.DataBindings.Add("Text", DataSource, "Sl35").FormatString = DungChung.Bien.FormatString[1];
            c6.DataBindings.Add("Text", DataSource, "Sl36").FormatString = DungChung.Bien.FormatString[1];
            c7.DataBindings.Add("Text", DataSource, "SL37").FormatString = DungChung.Bien.FormatString[1];
            c8.DataBindings.Add("Text", DataSource, "SL38").FormatString = DungChung.Bien.FormatString[1];
            c9.DataBindings.Add("Text", DataSource, "SL39").FormatString = DungChung.Bien.FormatString[1];
            c10.DataBindings.Add("Text", DataSource, "SL40").FormatString = DungChung.Bien.FormatString[1];
            c11.DataBindings.Add("Text", DataSource, "Sl41").FormatString = DungChung.Bien.FormatString[1];
            c12.DataBindings.Add("Text", DataSource, "SL42").FormatString = DungChung.Bien.FormatString[1];
            c13.DataBindings.Add("Text", DataSource, "SL43").FormatString = DungChung.Bien.FormatString[1];
            c14.DataBindings.Add("Text", DataSource, "SL44").FormatString = DungChung.Bien.FormatString[1];
            c15.DataBindings.Add("Text", DataSource, "SL45").FormatString = DungChung.Bien.FormatString[1];
            c16.DataBindings.Add("Text", DataSource, "SL46").FormatString = DungChung.Bien.FormatString[1];
            c17.DataBindings.Add("Text", DataSource, "SL47").FormatString = DungChung.Bien.FormatString[1];
            c18.DataBindings.Add("Text", DataSource, "SL48").FormatString = DungChung.Bien.FormatString[1];
            c19.DataBindings.Add("Text", DataSource, "SL49").FormatString = DungChung.Bien.FormatString[1];
            c20.DataBindings.Add("Text", DataSource, "SL50").FormatString = DungChung.Bien.FormatString[1];
            c21.DataBindings.Add("Text", DataSource, "SL51").FormatString = DungChung.Bien.FormatString[1];
            c22.DataBindings.Add("Text", DataSource, "SL52").FormatString = DungChung.Bien.FormatString[1];
            c23.DataBindings.Add("Text", DataSource, "SL53").FormatString = DungChung.Bien.FormatString[1];
            c24.DataBindings.Add("Text", DataSource, "SL54").FormatString = DungChung.Bien.FormatString[1];
            c25.DataBindings.Add("Text", DataSource, "SL55").FormatString = DungChung.Bien.FormatString[1];
            c26.DataBindings.Add("Text", DataSource, "SL56").FormatString = DungChung.Bien.FormatString[1];
            c27.DataBindings.Add("Text", DataSource, "SL57").FormatString = DungChung.Bien.FormatString[1];
            c28.DataBindings.Add("Text", DataSource, "SL58").FormatString = DungChung.Bien.FormatString[1];
            c29.DataBindings.Add("Text", DataSource, "SL59").FormatString = DungChung.Bien.FormatString[1];
            c30.DataBindings.Add("Text", DataSource, "SL60").FormatString = DungChung.Bien.FormatString[1];

            c1T.DataBindings.Add("Text", DataSource, "SL31").FormatString = DungChung.Bien.FormatString[1];
            c2T.DataBindings.Add("Text", DataSource, "SL32").FormatString = DungChung.Bien.FormatString[1];
            c3T.DataBindings.Add("Text", DataSource, "SL33").FormatString = DungChung.Bien.FormatString[1];
            c4T.DataBindings.Add("Text", DataSource, "SL34").FormatString = DungChung.Bien.FormatString[1];
            c5T.DataBindings.Add("Text", DataSource, "SL35").FormatString = DungChung.Bien.FormatString[1];
            c6T.DataBindings.Add("Text", DataSource, "SL36").FormatString = DungChung.Bien.FormatString[1];
            c7T.DataBindings.Add("Text", DataSource, "SL37").FormatString = DungChung.Bien.FormatString[1];
            c8T.DataBindings.Add("Text", DataSource, "SL38").FormatString = DungChung.Bien.FormatString[1];
            c9T.DataBindings.Add("Text", DataSource, "SL39").FormatString = DungChung.Bien.FormatString[1];
            c10T.DataBindings.Add("Text", DataSource, "SL40").FormatString = DungChung.Bien.FormatString[1];
            c11T.DataBindings.Add("Text", DataSource, "Sl41").FormatString = DungChung.Bien.FormatString[1];
            c12T.DataBindings.Add("Text", DataSource, "SL42").FormatString = DungChung.Bien.FormatString[1];
            c13T.DataBindings.Add("Text", DataSource, "SL43").FormatString = DungChung.Bien.FormatString[1];
            c14T.DataBindings.Add("Text", DataSource, "SL44").FormatString = DungChung.Bien.FormatString[1];
            c15T.DataBindings.Add("Text", DataSource, "SL45").FormatString = DungChung.Bien.FormatString[1];
            c16T.DataBindings.Add("Text", DataSource, "SL46").FormatString = DungChung.Bien.FormatString[1];
            c17T.DataBindings.Add("Text", DataSource, "SL47").FormatString = DungChung.Bien.FormatString[1];
            c18T.DataBindings.Add("Text", DataSource, "SL48").FormatString = DungChung.Bien.FormatString[1];
            c19T.DataBindings.Add("Text", DataSource, "SL49").FormatString = DungChung.Bien.FormatString[1];
            c20T.DataBindings.Add("Text", DataSource, "SL50").FormatString = DungChung.Bien.FormatString[1];
            c21T.DataBindings.Add("Text", DataSource, "SL51").FormatString = DungChung.Bien.FormatString[1];
            c22T.DataBindings.Add("Text", DataSource, "SL52").FormatString = DungChung.Bien.FormatString[1];
            c23T.DataBindings.Add("Text", DataSource, "SL53").FormatString = DungChung.Bien.FormatString[1];
            c24T.DataBindings.Add("Text", DataSource, "SL54").FormatString = DungChung.Bien.FormatString[1];
            c25T.DataBindings.Add("Text", DataSource, "SL55").FormatString = DungChung.Bien.FormatString[1];
            c26T.DataBindings.Add("Text", DataSource, "SL56").FormatString = DungChung.Bien.FormatString[1];
            c27T.DataBindings.Add("Text", DataSource, "SL57").FormatString = DungChung.Bien.FormatString[1];
            c28T.DataBindings.Add("Text", DataSource, "SL58").FormatString = DungChung.Bien.FormatString[1];
            c29T.DataBindings.Add("Text", DataSource, "SL59").FormatString = DungChung.Bien.FormatString[1];
            c30T.DataBindings.Add("Text", DataSource, "SL60").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //       colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            // colTTDV.Text = DungChung.Bien.NguoiLapBieu;
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void c28_BeforePrint(object sender, CancelEventArgs e)
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
