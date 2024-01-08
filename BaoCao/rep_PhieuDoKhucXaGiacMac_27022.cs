
using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuDoKhucXaGiacMac_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuDoKhucXaGiacMac_27022()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = colTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = colTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        //private void xrPictureBox1_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    this.xrPictureBox1.ImageUrl = DuongDan.Value.ToString();
        //}

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
    }
}
