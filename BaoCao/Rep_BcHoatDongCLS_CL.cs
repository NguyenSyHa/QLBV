using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongCLS_CL : DevExpress.XtraReports.UI.XtraReport
    {
        int a = 0;
        public Rep_BcHoatDongCLS_CL(int _a)
        {
            InitializeComponent();
            a = _a;
        }
        public void BindingData()
        {
            //colLH.DataBindings.Add("Text", DataSource, "TenNhomCT");
          
            //colTenDV.DataBindings.Add("Text",DataSource,"TenTN");
            //colDV.DataBindings.Add("Text", DataSource, "DVT");
            //t1.DataBindings.Add("Text",DataSource,"TS");
            //n1.DataBindings.Add("Text", DataSource, "NoiTru");
         
            //t11.DataBindings.Add("Text", DataSource, "TS");
            //col2T.DataBindings.Add("Text", DataSource, "NoiTru");
      

            //GroupHeader1.GroupFields.Add(new GroupField("TenNhomCT"));
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
            if (DungChung.Bien.MaBV.Equals("30003"))
            {
                xrTableRow21.Visible = false;
                xrTableRow22.Visible = false;
            }
            else if (DungChung.Bien.MaBV.Equals("30010"))
            {
                xrTableRow21.Visible = true;
                xrTableRow22.Visible = true;
            }
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

    

        private void colBHYTNN_BeforePrint(object sender, CancelEventArgs e)
        {
        
        }

        private void colTenCK_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
    //    int stt =1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            //switch (stt)
            //{
            //    case 1:
            //        colSTT.Text = "A";
            //        break;
            //    case 2:
            //        colSTT.Text = "B";
            //        break;
            //    case 3:
            //        colSTT.Text = "C";
            //        break;
            //    case 4:
            //        colSTT.Text = "D";
            //        break;
            //    case 5:
            //        colSTT.Text = "E";
            //        break;
            //}
            //stt++;
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (a == 2)
            { GroupFooter1.Visible = false; }
        }
        

    }
}
