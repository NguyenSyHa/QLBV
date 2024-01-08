using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;
namespace QLBV.BaoCao
{
    public partial class Rep_BcHoatDongKKB_HL01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BcHoatDongKKB_HL01()
        {
            InitializeComponent();
        }
     
        
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //int x = 0;
            //if (TS2.Value != null) 
            //{ x =Convert.ToInt32(TS2.Value); }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
              colTenCQ.Text = DungChung.Bien.TenCQ;
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
