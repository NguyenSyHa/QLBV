using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuPT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuPT()
        {
            InitializeComponent();
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }

        public void databinding()
        {

            cel_tenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_dvt.DataBindings.Add("Text", DataSource, "DonVi");
            cel_soluong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            cel_dongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            cel_nuocsx.DataBindings.Add("Text", DataSource, "GhiChu").FormatString = DungChung.Bien.FormatString[1];
            cel_thanhtien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            cellTongTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            cell_congkhoan.DataBindings.Add("Text", DataSource, "TenDV").FormatString = DungChung.Bien.FormatString[0];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
                xrLabel1.Visible = false;
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }
    }
}
