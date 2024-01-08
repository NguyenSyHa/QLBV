using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_SaoBenhAn : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_SaoBenhAn()
        {
            InitializeComponent();        
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
        //public void BindingData()
        //{
        //    GroupHeader1.GroupFields.Add(new GroupField("MaDV"));
        //    xrTableCell7.DataBindings.Add("Text", DataSource, "TenDV");
        //    xrTableCell30.DataBindings.Add("Text", DataSource, "TenXN");
        //}
        //int x = 1;
        //private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    xrTableCell26.Text = x.ToString();
        //    x++;
        //}
    }
}
