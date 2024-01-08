using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongCLS_TH03_26007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongCLS_TH03_26007()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colLH.DataBindings.Add("Text", DataSource, "TenNhomCT");
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            colTenDV.DataBindings.Add("Text",DataSource,"TenDV");
            colDV.DataBindings.Add("Text", DataSource, "DVT");
            col1.DataBindings.Add("Text",DataSource,"TS");
            col2.DataBindings.Add("Text", DataSource, "BHYT");
            col3.DataBindings.Add("Text", DataSource, "TP");
            col4.DataBindings.Add("Text", DataSource, "NoiTru");
            col5.DataBindings.Add("Text", DataSource, "NgoaiTru");

            colDVG1.DataBindings.Add("Text", DataSource, "DVT");
            col1G1.DataBindings.Add("Text", DataSource, "TS");
            col2G1.DataBindings.Add("Text", DataSource, "BHYT");
            col3G1.DataBindings.Add("Text", DataSource, "TP");
            col4G1.DataBindings.Add("Text", DataSource, "NoiTru");
            col5G1.DataBindings.Add("Text", DataSource, "NgoaiTru");
        
            col1T.DataBindings.Add("Text", DataSource, "TS");
            col2T.DataBindings.Add("Text", DataSource, "BHYT");
            col3T.DataBindings.Add("Text", DataSource, "TP");
            col4T.DataBindings.Add("Text", DataSource, "NoiTru");
            col5T.DataBindings.Add("Text", DataSource, "NgoaiTru");


            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            GroupHeader2.GroupFields.Add(new GroupField("TenNhomCT"));
        }
        int count = 0;
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
            count++;
            celSTT.Text = count.ToString();
           
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
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

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void xrTableRow2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TenDV") != null)
            {
                if (this.GetCurrentColumnValue("TenDV").ToString().Trim() != "")
                {
                    xrTableRow2.Visible = true;
                   

                }
                else

                    xrTableRow2.Visible = false;
            }
            else
                xrTableRow2.Visible = false;
            //if (count > 0)
            //    Detail.Visible = true;
            //else
            //    Detail.Visible = false;
        }
        

    }
}
