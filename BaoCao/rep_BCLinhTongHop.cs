using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCLinhTongHop : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCLinhTongHop()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ;
            tencq.Text = DungChung.Bien.TenCQ;
        }
        public void BindingData()
        {
            celTenKP.DataBindings.Add("Text", DataSource, "TenKP");
            Thuoc.DataBindings.Add("Text", DataSource, "Thuoc");
            VTYT.DataBindings.Add("Text", DataSource, "VTYT");
            HoaChat.DataBindings.Add("Text", DataSource, "HoaChat");
            Tong.DataBindings.Add("Text", DataSource, "Tong");
        }
    }
}
