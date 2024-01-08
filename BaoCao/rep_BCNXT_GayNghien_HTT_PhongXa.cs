using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCNXT_GayNghien_HTT_PhongXa : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCNXT_GayNghien_HTT_PhongXa()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colTonDK.DataBindings.Add("Text", DataSource, "SLTonDK").FormatString = DungChung.Bien.FormatString[0];
            colNhap.DataBindings.Add("Text", DataSource, "SLNhap").FormatString = DungChung.Bien.FormatString[0];
            colTong.DataBindings.Add("Text", DataSource, "SLTong").FormatString = DungChung.Bien.FormatString[0];
            colXuat.DataBindings.Add("Text", DataSource, "SLXuat").FormatString = DungChung.Bien.FormatString[0];
            colHH.DataBindings.Add("Text", DataSource, "SLHuHao").FormatString = DungChung.Bien.FormatString[0];
            colTonCK.DataBindings.Add("Text", DataSource, "SLTonCK").FormatString = DungChung.Bien.FormatString[0];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rep_BCNXT_GayNghien_HTT_PhongXa_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtcq.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
