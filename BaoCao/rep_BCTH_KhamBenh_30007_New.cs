using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCTH_KhamBenh_30007_New : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCTH_KhamBenh_30007_New()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celstt.DataBindings.Add("Text", DataSource, "Stt");
            celNd.DataBindings.Add("Text", DataSource, "Nd");
            cel1.DataBindings.Add("Text", DataSource, "Khoa1");
            cel2.DataBindings.Add("Text", DataSource, "Khoa2");
            cel3.DataBindings.Add("Text", DataSource, "Khoa3");
            cel4.DataBindings.Add("Text", DataSource, "Khoa4");
            cel5.DataBindings.Add("Text", DataSource, "Khoa5");
            cel6.DataBindings.Add("Text", DataSource, "Khoa6");
            cel7.DataBindings.Add("Text", DataSource, "Khoa7");
            cel8.DataBindings.Add("Text", DataSource, "Khoa8");
            cel9.DataBindings.Add("Text", DataSource, "Khoa9");
            cel10.DataBindings.Add("Text", DataSource, "Khoa10");
            celt.DataBindings.Add("Text", DataSource, "Tong");
           
           
        }

       
    }
}
