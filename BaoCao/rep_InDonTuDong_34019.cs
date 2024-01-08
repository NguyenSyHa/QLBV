using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_InDonTuDong_34019 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_InDonTuDong_34019()
        {
            InitializeComponent();
        }



        internal void databinding()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celSL.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ;

        }
    }
}
