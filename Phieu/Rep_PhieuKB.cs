using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuKB : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuKB()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel2.Text = DungChung.Bien.TenCQ.ToUpper();
            if(DungChung.Bien.MaBV =="26007")
            {
                //xrLabel5.Font = new Font(xrLabel5.Font.FontFamily, 50);
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if(this.HT.Value.ToString().Length>=20)
            xrLabel7.Font = new Font("Times New Roman", 8);
        }

    }
}
