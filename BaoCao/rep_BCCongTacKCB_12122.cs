using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCCongTacKCB_12122 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCCongTacKCB_12122()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            cel1.DataBindings.Add("Text", DataSource, "stt");
            cel3.DataBindings.Add("Text", DataSource, "NoiDung");
            cell4.DataBindings.Add("Text", DataSource, "DonVi");
            cell5.DataBindings.Add("Text", DataSource, "Tong");
            cell6.DataBindings.Add("Text", DataSource, "TE6");
            cell7.DataBindings.Add("Text", DataSource, "TE15");
            cell8.DataBindings.Add("Text", DataSource, "BH60");
            cell9.DataBindings.Add("Text", DataSource, "HN");
            cell10.DataBindings.Add("Text", DataSource, "BHKhac");
            cell11.DataBindings.Add("Text", DataSource, "DV");
          
        }
    }
}
