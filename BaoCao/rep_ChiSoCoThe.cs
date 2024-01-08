using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;


namespace QLBV.Phieu
{
    public partial class rep_ChiSoCoThe : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_ChiSoCoThe()
        {
            InitializeComponent();
        }


        internal void Bindings()
        {
           
        }

        private void xrChart1_CustomDrawSeriesPoint(object sender, DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs e)
        {
       
            double val = e.SeriesPoint[0];
            //if (e.Series.Points.IndexOf(e.SeriesPoint) == 1)
            if (val < 38)
            {
                 e.SeriesDrawOptions.Color = Color.FromArgb(100, Color.Black);               
                // ((BarDrawOptions)e.SeriesDrawOptions).FillStyle.FillMode = FillMode.Solid;                 
            }

                   
               
           
        }
    }
}
