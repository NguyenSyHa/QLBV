using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCCTKCBNgoaiChu : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCCTKCBNgoaiChu()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                txtcq.Text = DungChung.Bien.TenCQ + " CSI";
            else if (DungChung.Bien.MaBV == "01049")
                txtcq.Text = DungChung.Bien.TenCQ + " CSII";
            else
                txtcq.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //nguoilap.Text = DungChung.Bien.NguoiLapBieu;
            //truongkhoa.Text = DungChung.Bien.KeToanTruong;
        }

        public void BindingData()
        {
            STT.DataBindings.Add("Text", DataSource, "STT");
            noidung.DataBindings.Add("Text", DataSource, "NoiDung");
            bhyt.DataBindings.Add("Text", DataSource, "BHYT");
            dichvu.DataBindings.Add("Text", DataSource, "DichVu");
            tong.DataBindings.Add("Text", DataSource, "Tong");

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.noidung.Text.Contains("1.") || this.noidung.Text.Contains("2.") || this.noidung.Text.Contains("3."))
            //    this.noidung.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            //else
            //    this.noidung.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
        }
    }
}
