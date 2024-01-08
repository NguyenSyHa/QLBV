using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuPhatThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuPhatThuoc()
        {
            InitializeComponent();
        }
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        public void BindData()
        {
            cell_TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            cell_SoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:##,###}";
            cellDVT.DataBindings.Add("Text", DataSource, "DonVi");
            cell_DonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = "{0:##,###.###}";
            cell_ThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = "{0:##,###.###}";
            cellMa.DataBindings.Add("Text", DataSource, "MaTam");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = txtTenCQ2.Text = DungChung.Bien.TenCQ;
            labTenCQCQ.Text = txtTenCQCQ2.Text = DungChung.Bien.TenCQCQ;
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }

        private void xrTableCell12_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {


        }

    }
}
