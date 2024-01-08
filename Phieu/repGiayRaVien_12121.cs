using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repGiayRaVien_12121 : DevExpress.XtraReports.UI.XtraReport
    {
        public repGiayRaVien_12121()
        {
            InitializeComponent();
        }

        private void repGiayRaVien_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();       
            //if (DungChung.Bien.MaBV == "12123")
            //    xrLabel26.Text = "GIÁM ĐỐC";
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
     
            if (SThe.Value != null && SThe.Value.ToString() != "")
            {

                col1.Text = SThe.Value.ToString().Substring(0, 2);
                col2.Text = SThe.Value.ToString().Substring(2, 1);
                col3.Text = SThe.Value.ToString().Substring(3, 2);
                col4.Text = SThe.Value.ToString().Substring(5, 2);
                col5.Text = SThe.Value.ToString().Substring(7, 3);
                col6.Text = SThe.Value.ToString().Substring(10, 5);
            }
            
        }
      
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30007")
            //{
            //    //xrLabel26.Text = "Trưởng khoa điều trị";
            //    //colTD_GDBV.Text = "Giám đốc bệnh viện";
            xrLabel32.Visible = true;
            if (DungChung.Bien.MaBV == "24297")
            {
                colTD_GDBV.Text = "Bác sĩ điều trị";
            }
            //    xrLabel23.Visible = true;
            //    //HoTenTruongKhoa.Value = DungChung.Bien.GiamDoc;
            //}
            //else
            //{
                GiamDoc.Value = DungChung.Bien.GiamDoc;
                xrLabel23.Visible = true;
            //}

        }

        private void xrLabel26_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
