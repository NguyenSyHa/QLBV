using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongPTTT_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongPTTT_24009()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colLH.DataBindings.Add("Text", DataSource, "LoaiHinh");

            colTenCK.DataBindings.Add("Text", DataSource, "TenLoai");
            col1.DataBindings.Add("Text", DataSource, "TS").FormatString = "{0:##,###}";
            col2.DataBindings.Add("Text", DataSource, "TKH").FormatString = "{0:##,###}";
            col3.DataBindings.Add("Text", DataSource, "CC").FormatString = "{0:##,###}";
            col8.DataBindings.Add("Text", DataSource, "TSTB").FormatString = "{0:##,###}";
            col9.DataBindings.Add("Text", DataSource, "GMHS").FormatString = "{0:##,###}";
            col10.DataBindings.Add("Text", DataSource, "NK").FormatString = "{0:##,###}";
            col11.DataBindings.Add("Text", DataSource, "TBK").FormatString = "{0:##,###}";
            col12.DataBindings.Add("Text", DataSource, "TSTV").FormatString = "{0:##,###}";
            col13.DataBindings.Add("Text", DataSource, "TVTB").FormatString="{0:##,###}";
            col14.DataBindings.Add("Text", DataSource, "TV24Gio").FormatString="{0:##,###}";
            col1T.DataBindings.Add("Text", DataSource, "TS").FormatString="{0:##,###}";
            col2T.DataBindings.Add("Text", DataSource, "TKH").FormatString="{0:##,###}";
            col3T.DataBindings.Add("Text", DataSource, "CC").FormatString="{0:##,###}";
            col8T.DataBindings.Add("Text", DataSource, "TSTB").FormatString = "{0:##,###}";
            col9T.DataBindings.Add("Text", DataSource, "GMHS").FormatString = "{0:##,###}";
            col10T.DataBindings.Add("Text", DataSource, "NK").FormatString = "{0:##,###}";
            col11T.DataBindings.Add("Text", DataSource, "TBK").FormatString = "{0:##,###}";
            col12T.DataBindings.Add("Text", DataSource, "TSTV").FormatString = "{0:##,###}";
            col13T.DataBindings.Add("Text", DataSource, "TVTB").FormatString = "{0:##,###}";
            col14T.DataBindings.Add("Text", DataSource, "TV24Gio").FormatString = "{0:##,###}";

            GroupHeader1.GroupFields.Add(new GroupField("LoaiHinh"));
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
            }
            stt++;
        }
        

    }
}
