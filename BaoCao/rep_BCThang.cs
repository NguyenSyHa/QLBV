using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCThang : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCThang()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        public void BindingData()
        {
            clndbcgr2.DataBindings.Add("Text", DataSource, "noiDung");
            clslbcgr2.DataBindings.Add("Text", DataSource, "SoLuongTong");
            STT.DataBindings.Add("Text", DataSource, "STT");

            clndbc.DataBindings.Add("Text", DataSource, "noiDung2");
            clslbc.DataBindings.Add("Text", DataSource, "soLuot");

            GroupHeader2.GroupFields.Add(new GroupField("STT"));

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("noiDung2") != null)
            {
                string nt = this.GetCurrentColumnValue("noiDung2").ToString();
                if (nt == "Ngoại trú" || nt == "Nội trú")
                {
                    this.clndbc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    this.clndbc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                }
            }
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("noiDung") != null)
            {
                string nt = this.GetCurrentColumnValue("noiDung").ToString();
                if (nt == "Phẫu thuật")
                    Detail.Visible = false;
                else
                    Detail.Visible = true;
            }
        }
    }
}
