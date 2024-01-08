using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PLThuoctheothang_TT44 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PLThuoctheothang_TT44()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            celTendv.DataBindings.Add("Text", DataSource, "TenDV");
            celDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            celSL.DataBindings.Add("Text", DataSource, "Soluong");
            celghichu.DataBindings.Add("Text", DataSource, "GhiChu");
            
            xrTable_2.Visible = true;
            //RFThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017")
            {
                celTenCQ.Text = "SỞ Y TẾ SƠN LA";
            }
            else
            {
                celTenCQ.Text = celTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
                if (DungChung.Bien.MaBV == "24272")
                {
                    SubBand1.Visible = false;
                    SubBand2.Visible = true;
                    xrPictureBox1.Image = DungChung.Ham.GetLogo();
                }
            }
            //if (DungChung.Bien.MaBV == "27001")
            //    xrTableCell12.Text = "BÁC SỸ KÊ ĐƠN";
        }

        private void Donvi_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
