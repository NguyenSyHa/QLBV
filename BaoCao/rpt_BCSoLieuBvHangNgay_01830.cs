using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rpt_BCSoLieuBvHangNgay_01830 : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_BCSoLieuBvHangNgay_01830()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            cel1.DataBindings.Add("Text", DataSource, "col1").FormatString = "{0: dd/MM/yyyy}";
            cel2.DataBindings.Add("Text", DataSource, "col2");
            cel3.DataBindings.Add("Text", DataSource, "col3");
            cel4.DataBindings.Add("Text", DataSource, "col4");
            cel5.DataBindings.Add("Text", DataSource, "col5");
            cel6.DataBindings.Add("Text", DataSource, "col6");
            cel7.DataBindings.Add("Text", DataSource, "col7");
            cel8.DataBindings.Add("Text", DataSource, "col8");
            cel9.DataBindings.Add("Text", DataSource, "col9");
            cel10.DataBindings.Add("Text", DataSource, "col10");
            cel11.DataBindings.Add("Text", DataSource, "col11");
            cel12.DataBindings.Add("Text", DataSource, "col12");
            cel13.DataBindings.Add("Text", DataSource, "col13");
            cel14.DataBindings.Add("Text", DataSource, "col14");
            cel15.DataBindings.Add("Text", DataSource, "col15");
            cel16.DataBindings.Add("Text", DataSource, "col16");
            cel17.DataBindings.Add("Text", DataSource, "col17");
            cel18.DataBindings.Add("Text", DataSource, "col18");
            cel19.DataBindings.Add("Text", DataSource, "col19");
            cel20.DataBindings.Add("Text", DataSource, "col20");
            cel21.DataBindings.Add("Text", DataSource, "col21");
            cel22.DataBindings.Add("Text", DataSource, "col22");
            cel23.DataBindings.Add("Text", DataSource, "col23");
            cel24.DataBindings.Add("Text", DataSource, "col24");
            cel25.DataBindings.Add("Text", DataSource, "col25");
            cel26.DataBindings.Add("Text", DataSource, "col26");
            cel27.DataBindings.Add("Text", DataSource, "col27");
            cel28.DataBindings.Add("Text", DataSource, "col28");
            cel29.DataBindings.Add("Text", DataSource, "col29");
            cel30.DataBindings.Add("Text", DataSource, "col30");
            cel31.DataBindings.Add("Text", DataSource, "col31");
            cel32.DataBindings.Add("Text", DataSource, "col32");
            cel33.DataBindings.Add("Text", DataSource, "col33");
            cel34.DataBindings.Add("Text", DataSource, "col34");
            cel35.DataBindings.Add("Text", DataSource, "col35");
            cel36.DataBindings.Add("Text", DataSource, "col36");
            cel37.DataBindings.Add("Text", DataSource, "col37");
            cel38.DataBindings.Add("Text", DataSource, "col38");
            cel39.DataBindings.Add("Text", DataSource, "col39");
            cel40.DataBindings.Add("Text", DataSource, "col40");
            cel41.DataBindings.Add("Text", DataSource, "col41");

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            cel_NguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            cel_diadanh.Text = "Ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
        }
    }
}
