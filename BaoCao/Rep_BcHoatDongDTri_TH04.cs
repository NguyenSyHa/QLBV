using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongDTri_TH04 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongDTri_TH04()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenCK.DataBindings.Add("Text",DataSource,"Khoa");
            col1.DataBindings.Add("Text",DataSource,"C1");
            col2.DataBindings.Add("Text", DataSource, "C2");
            col3.DataBindings.Add("Text", DataSource, "C3");
            col4.DataBindings.Add("Text", DataSource, "C4");
            col5.DataBindings.Add("Text", DataSource, "C5");
            col6.DataBindings.Add("Text", DataSource, "C6");
            col7.DataBindings.Add("Text", DataSource, "C7");
            col8.DataBindings.Add("Text", DataSource, "C8");
            col9.DataBindings.Add("Text", DataSource, "C9");
            col10.DataBindings.Add("Text", DataSource, "C10");
            col11.DataBindings.Add("Text", DataSource, "C11");
            col12.DataBindings.Add("Text", DataSource, "C12");
            col13.DataBindings.Add("Text", DataSource, "C13");
            col14.DataBindings.Add("Text", DataSource, "C14");
            col15.DataBindings.Add("Text", DataSource, "C15");
            col1T.DataBindings.Add("Text", DataSource, "C1");
             col2T.DataBindings.Add("Text", DataSource, "C2");
             col3T.DataBindings.Add("Text", DataSource, "C3");
             col4T.DataBindings.Add("Text", DataSource, "C4");
             col5T.DataBindings.Add("Text", DataSource, "C5");
             col6T.DataBindings.Add("Text", DataSource, "C6");
             col7T.DataBindings.Add("Text", DataSource, "C7");
             col8T.DataBindings.Add("Text", DataSource, "C8");
             col9T.DataBindings.Add("Text", DataSource, "C9");
             col10T.DataBindings.Add("Text", DataSource, "C10");
             col11T.DataBindings.Add("Text", DataSource, "C11");
              col12T.DataBindings.Add("Text", DataSource, "C12");
             col13T.DataBindings.Add("Text", DataSource, "C13");
             col14T.DataBindings.Add("Text", DataSource, "C14");
             col15T.DataBindings.Add("Text", DataSource, "C15");
        }
        
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //int x = 0;
            //if (TS2.Value != null) 
            //{ x =Convert.ToInt32(TS2.Value); }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
              colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
              colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

    

        private void colBHYTNN_BeforePrint(object sender, CancelEventArgs e)
        {
        
        }
        

    }
}
