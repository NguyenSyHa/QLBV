using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuKhaiThacTSDiUng : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuKhaiThacTSDiUng()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel4.Text = xrLabel1.Text = DungChung.Bien.TenCQCQ;
            xrLabel19.Text = xrLabel2.Text = DungChung.Bien.TenCQ;
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }
        //public void BindingData() {
        //    col_CheDoDDCS.DataBindings.Add("Text", DataSource, "HuongDtri");
        //        colNgayGio.DataBindings.Add("Text", DataSource, "NgayNhapHT").FormatString = "{0:dd/MM/yyyy HH:mm}";
        //        colTenCBDB.DataBindings.Add("Text", DataSource, "TenCB");
        //    colDienbien.DataBindings.Add("Text", DataSource, "DienBien1");
        //    colYLenh.DataBindings.Add("Text", DataSource, "YLenh");
        //    colTenCB.DataBindings.Add("Text", DataSource, "TenCB");
        //}

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void rep_ToDieuTri_YHCT_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel4.Text = DungChung.Bien.TenCQCQ;
            xrLabel19.Text = DungChung.Bien.TenCQ;
        }

        private void ReportHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            xrLabel4.Text = xrLabel1.Text = DungChung.Bien.TenCQCQ;
            xrLabel19.Text = xrLabel2.Text = DungChung.Bien.TenCQ;
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }
    }
}
