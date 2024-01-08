using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongDTri_30010 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongDTri_30010()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenCK.DataBindings.Add("Text",DataSource,"Khoa");
            //col1.DataBindings.Add("Text",DataSource,"C1");
            //col2.DataBindings.Add("Text", DataSource, "C2");
            //col3.DataBindings.Add("Text", DataSource, "C3");
            //col4.DataBindings.Add("Text", DataSource, "C4");
            //col5.DataBindings.Add("Text", DataSource, "C5");
            //col6.DataBindings.Add("Text", DataSource, "C6");
            //col7.DataBindings.Add("Text", DataSource, "C7");
            //col8.DataBindings.Add("Text", DataSource, "C8");
            //col9.DataBindings.Add("Text", DataSource, "C9");
            //col10.DataBindings.Add("Text", DataSource, "C10");
            //col11.DataBindings.Add("Text", DataSource, "C11");

             col1T.DataBindings.Add("Text", DataSource, "C1");
             col2T.DataBindings.Add("Text", DataSource, "C2");
             col3T.DataBindings.Add("Text", DataSource, "C3");
             col4T.DataBindings.Add("Text", DataSource, "C4");
             col5T.DataBindings.Add("Text", DataSource, "C5");
             col6T.DataBindings.Add("Text", DataSource, "C6");
             col7T.DataBindings.Add("Text", DataSource, "C7");
             col71T.DataBindings.Add("Text", DataSource, "C71");
             col8T.DataBindings.Add("Text", DataSource, "C8");
             col9T.DataBindings.Add("Text", DataSource, "C9");
             col11T.DataBindings.Add("Text", DataSource, "C11");
             col12T.DataBindings.Add("Text", DataSource, "C12");
             col13T.DataBindings.Add("Text", DataSource, "C13");
             col14T.DataBindings.Add("Text", DataSource, "C14");
             
        }
        
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            int v;
            if (this.GetCurrentColumnValue("C1") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C1"));
                if (v == 0)
                {
                    col1.Text = "";
                }
                else { col1.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("C2") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C2"));
                if (v == 0)
                {
                    col2.Text = "";
                }
                else { col2.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("C3") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C3"));
                if (v == 0)
                {
                    col3.Text = "";
                }
                else { col3.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("C4") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C4"));
                if (v == 0)
                {
                    col4.Text = "";
                }
                else { col4.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("C5") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C5"));
                if (v == 0)
                {
                    col5.Text = "";
                }
                else { col5.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("C6") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C6"));
                if (v == 0)
                {
                    col6.Text = "";
                }
                else { col6.Text = v.ToString("#,##"); }

            }
            if (this.GetCurrentColumnValue("C7") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C7"));
                if (v == 0)
                {
                    col7.Text = "";
                }
                else { col7.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("C8") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C8"));
                if (v == 0)
                {
                    col8.Text = "";
                }
                else { col8.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("C9") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C9"));
                if (v == 0)
                {
                    col9.Text = "";
                }
                else { col9.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("C11") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C11"));
                if (v == 0)
                {
                    col11.Text = "";
                }
                else { col11.Text = v.ToString("#,##"); }
            }
            if (this.GetCurrentColumnValue("C12") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C12"));
                if (v == 0)
                {
                    col12.Text = "";
                }
                else { col12.Text = v.ToString("#,##"); }
            }

            if (this.GetCurrentColumnValue("C13") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C13"));
                if (v == 0)
                {
                    col13.Text = "";
                }
                else { col13.Text = v.ToString("#,##"); }
            }

            if (this.GetCurrentColumnValue("C14") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C14"));
                if (v == 0)
                {
                    col14.Text = "";
                }
                else { col14.Text = v.ToString("#,##"); }
            }

            if (this.GetCurrentColumnValue("C71") != null)
            {
                v = Convert.ToInt32(this.GetCurrentColumnValue("C71"));
                if (v == 0)
                {
                    col71.Text = "";
                }
                else { col71.Text = v.ToString("#,##"); }
            }
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
