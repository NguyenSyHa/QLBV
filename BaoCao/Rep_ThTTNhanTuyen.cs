using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_ThTTNhanTuyen : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_ThTTNhanTuyen()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenBNhan.DataBindings.Add("Text",DataSource,"TenBNhan");
            txtMaBNhan.DataBindings.Add("Text",DataSource,"MaBNhan");
            colNam.DataBindings.Add("Text",DataSource,"Nam");
            colNu.DataBindings.Add("Text",DataSource,"Nu");
            colChanDoan.DataBindings.Add("Text",DataSource,"CDNoiGT");
            colBHYT.DataBindings.Add("Text", DataSource, "DTuong");
            colTenBV.DataBindings.Add("Text",DataSource,"TenBV");
            colCDRV.DataBindings.Add("Text", DataSource, "ChanDoan");
            col1a.DataBindings.Add("Text", DataSource, "S1a");
           // col1aT.DataBindings.Add("Text", DataSource, "S1a");
            col1b.DataBindings.Add("Text", DataSource, "S1b");
           // col1bT.DataBindings.Add("Text", DataSource, "S1b");
            col2.DataBindings.Add("Text", DataSource, "S2");
            //col2T.DataBindings.Add("Text", DataSource, "S2");
            col3.DataBindings.Add("Text", DataSource, "S3");
         //   col3T.DataBindings.Add("Text", DataSource, "S3");
            col4.DataBindings.Add("Text", DataSource, "S4");
           // col4T.DataBindings.Add("Text", DataSource, "S4");
            col5.DataBindings.Add("Text", DataSource, "S5");
          //  col5T.DataBindings.Add("Text", DataSource, "S5");
            col6.DataBindings.Add("Text", DataSource, "S6");
          //  col6T.DataBindings.Add("Text", DataSource, "S6");
            col7.DataBindings.Add("Text", DataSource, "S7");
          //  col7T.DataBindings.Add("Text", DataSource, "S7");
            col8.DataBindings.Add("Text", DataSource, "S8");
          //  col8T.DataBindings.Add("Text", DataSource, "S8");
            col9.DataBindings.Add("Text", DataSource, "S9");
          //  col9T.DataBindings.Add("Text", DataSource, "S9"); 
            

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

    
      
      

    }
}
