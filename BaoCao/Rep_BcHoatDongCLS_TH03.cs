using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongCLS_TH03 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongCLS_TH03()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colLH.DataBindings.Add("Text", DataSource, "TenNhomCT");
          
            colTenDV.DataBindings.Add("Text",DataSource,"TenTN");
            colDV.DataBindings.Add("Text", DataSource, "DVT");
            col1.DataBindings.Add("Text",DataSource,"TS");
            col2.DataBindings.Add("Text", DataSource, "BHYT");
            col3.DataBindings.Add("Text", DataSource, "TP");
            col4.DataBindings.Add("Text", DataSource, "NoiTru");
            col5.DataBindings.Add("Text", DataSource, "NgoaiTru");
        
            col1T.DataBindings.Add("Text", DataSource, "TS");
            col2T.DataBindings.Add("Text", DataSource, "BHYT");
            col3T.DataBindings.Add("Text", DataSource, "TP");
            col4T.DataBindings.Add("Text", DataSource, "NoiTru");
            col5T.DataBindings.Add("Text", DataSource, "NgoaiTru");


            GroupHeader1.GroupFields.Add(new GroupField("TenNhomCT"));
        }
        
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           
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

        private void colTenCK_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
        int stt =1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (stt)
            {
                case 1:
                    colSTT.Text = "A";
                    break;
                case 2:
                    colSTT.Text = "B";
                    break;
                case 3:
                    colSTT.Text = "C";
                    break;
                case 4:
                    colSTT.Text = "D";
                    break;
                case 5:
                    colSTT.Text = "E";
                    break;
            }
            stt++;
        }
        

    }
}
