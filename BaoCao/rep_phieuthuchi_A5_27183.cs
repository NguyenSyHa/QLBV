using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_phieuthuchi_A5_27183 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_phieuthuchi_A5_27183()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = "Đơn vị: " + DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "20001")
            {
                xrTable8.Visible = false;
            }
            else
            {
                xrTable8.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            GD.Value = DungChung.Bien.GiamDoc;
            KTT.Value = DungChung.Bien.KeToanTruong;
            NguoiLap.Value = DungChung.Bien.NguoiLapBieu;     
        }

    }
}
