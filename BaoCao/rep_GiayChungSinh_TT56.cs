using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_GiayChungSinh_TT56 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_GiayChungSinh_TT56()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ;
            xrLabel11.Text = DungChung.Bien.TenCQ;
            xrTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            xrTenCQCQ2.Text = DungChung.Bien.TenCQCQ;

            TenCQ.Text = DungChung.Bien.TenCQ;
            TenCQ2.Text = DungChung.Bien.TenCQ;
        }

        public void check(int a)
        {
            if (DungChung.Bien.MaBV == "30012" && a == 1)
            {
                Detail.Visible = false;
                ReportHeader.Visible = true;
                GroupHeader1.Visible = true;
            }
        }
        
        private void rep_GiayChungSinh_TT56_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV=="01071" || DungChung.Bien.MaBV == "01049")
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
                if(DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    xrTable10.Visible = false;
                    xrTable12.Visible = false;
                }    
            }
           
                xrTenCQCQ.Visible = false;
                xrTenCQCQ2.Visible = false;
                xrMaVach.Visible = false;
                xrMaVach2.Visible = false;
                xrBarCode1.Visible = false;
                xrBarCode2.Visible = false;
                celTenCQ.LocationF = new PointF(2F, 4F); xrLabel2.LocationF = new PointF(2F, 27F); xrLabel11.LocationF = new PointF(1.38F, 4F);
                xrLabel10.LocationF = new PointF(1.38F, 27F);
            //else
            //{
            //    xrLabel5.LocationF = new PointF(131.19F, 131.96F);
            //    xrTable2.LocationF = new PointF(2F,185.48F);
            //    xrLabel12.LocationF = new PointF(131.19F, 154.96F);
            //    xrTable7.LocationF = new PointF(2.5F, 185.48F);
            //    xrLabel2.LocationF = new PointF(2F, 50F);
            //    xrLabel11.LocationF = new PointF(1.38F, 27F);
            //    xrLabel10.LocationF = new PointF(1.38F, 50F);
            //    xrLabel7.LocationF = new PointF(152.23F, 131.96F);
            //    xrLabel13.LocationF = new PointF(152.23F, 154.96F);
            //    celTenCQ.LocationF = new PointF(2F,27F);
            //    //xrLabel12.LocationF = new PointF(140.5F, 87F);
            //}
        }

    }
}
