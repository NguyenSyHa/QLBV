using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongKKB_CL : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongKKB_CL()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenCK.DataBindings.Add("Text",DataSource,"ChuyenKhoa");
            col1.DataBindings.Add("Text",DataSource,"A1");
            col2.DataBindings.Add("Text", DataSource, "A2");
            col3.DataBindings.Add("Text", DataSource, "A3");
            col4.DataBindings.Add("Text", DataSource, "A4");
            col5.DataBindings.Add("Text", DataSource, "A5");
            col6.DataBindings.Add("Text", DataSource, "A6");
            col7.DataBindings.Add("Text", DataSource, "A7");
            col8.DataBindings.Add("Text", DataSource, "A8");
            col9.DataBindings.Add("Text", DataSource, "A9");
             col1T.DataBindings.Add("Text", DataSource, "A1");
             col2T.DataBindings.Add("Text", DataSource, "A2");
             col3T.DataBindings.Add("Text", DataSource, "A3");
             col4T.DataBindings.Add("Text", DataSource, "A4");
             col5T.DataBindings.Add("Text", DataSource, "A5");
             col6T.DataBindings.Add("Text", DataSource, "A6");
             col7T.DataBindings.Add("Text", DataSource, "A7");
             col8T.DataBindings.Add("Text", DataSource, "A8");
             col9T.DataBindings.Add("Text", DataSource, "A9");
          
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
