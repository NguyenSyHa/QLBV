using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongKKB_TH01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongKKB_TH01()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenCK.DataBindings.Add("Text",DataSource,"ChuyenKhoa");
            col1.DataBindings.Add("Text",DataSource,"TS");
            col2.DataBindings.Add("Text", DataSource, "Nu");
            col3.DataBindings.Add("Text", DataSource, "BHYT");
            col4.DataBindings.Add("Text", DataSource, "VP");
            col5.DataBindings.Add("Text", DataSource, "KTD");
            col6.DataBindings.Add("Text", DataSource, "TE15");
            col7.DataBindings.Add("Text", DataSource, "TE6");
            col8.DataBindings.Add("Text", DataSource, "TE4");
            col9.DataBindings.Add("Text", DataSource, "TC60");
            col10.DataBindings.Add("Text", DataSource, "BHYTCV");
            col11.DataBindings.Add("Text", DataSource, "VPCV");
            col12.DataBindings.Add("Text", DataSource, "CC");
            col13.DataBindings.Add("Text", DataSource, "VV");
            col14.DataBindings.Add("Text", DataSource, "NBDTNT");
            col15.DataBindings.Add("Text", DataSource, "SNDTNT");
            col1T.DataBindings.Add("Text", DataSource, "TS");
             col2T.DataBindings.Add("Text", DataSource, "Nu");
             col3T.DataBindings.Add("Text", DataSource, "BHYT");
             col4T.DataBindings.Add("Text", DataSource, "VP");
             col5T.DataBindings.Add("Text", DataSource, "KTD");
             col6T.DataBindings.Add("Text", DataSource, "TE15");
             col7T.DataBindings.Add("Text", DataSource, "TE6");
             col8T.DataBindings.Add("Text", DataSource, "TE4");
             col9T.DataBindings.Add("Text", DataSource, "TC60");
             col10T.DataBindings.Add("Text", DataSource, "BHYTCV");
             col11T.DataBindings.Add("Text", DataSource, "VPCV");
             col11T.DataBindings.Add("Text", DataSource, "VPCV");
             col12T.DataBindings.Add("Text", DataSource, "CC");
             col13T.DataBindings.Add("Text", DataSource, "VV");
             col14T.DataBindings.Add("Text", DataSource, "NBDTNT");
             col15T.DataBindings.Add("Text", DataSource, "SNDTNT");
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
