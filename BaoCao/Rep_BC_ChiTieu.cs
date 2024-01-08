using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_ChiTieu : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_ChiTieu()
        {
            InitializeComponent();
        }

        public void BindingData()
        {
            cel_ChiTiet.DataBindings.Add("Text", DataSource, "TenChiTiet");
            cel_KH2016.DataBindings.Add("Text", DataSource, "KH2016");
            cel_KQ6Thang2015.DataBindings.Add("Text", DataSource, "KQ6Thang2015");
            cel_KQ6Thang2016.DataBindings.Add("Text", DataSource, "KQ6Thang2016");
            cel_TiLe6Thang2016VSKH2016.DataBindings.Add("Text", DataSource, "Tile6Thang2016SoVoiKH2016");
            cel_TiLe6Thang2016VS2015.DataBindings.Add("Text", DataSource, "Tile6Thang2016SoVoi2015");

            gr_TenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            gr_KHNam2016.DataBindings.Add("Text", DataSource, "KH2016");
            gr_KQ6ThangNam2015.DataBindings.Add("Text", DataSource, "KQ6Thang2015");
            gr_KQ6ThangNam2016.DataBindings.Add("Text", DataSource, "KQ6Thang2016");
            gr_TiLe6ThangNam2016VSKH2016.DataBindings.Add("Text", DataSource, "Tile6Thang2016SoVoiKH2016");
            gr_TiLe6ThangNam2016VS2015.DataBindings.Add("Text", DataSource, "Tile6Thang2016SoVoi2015");
            GroupHeader1.GroupFields.Add(new GroupField("Stt"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
            celLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            lblTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("TenChiTiet") != null)
            {
                string nt = this.GetCurrentColumnValue("TenChiTiet").ToString();
                if (nt == "")
                    xrTableRow3.Visible = false;
                else
                    xrTableRow3.Visible = true;

            }
        }
    }
}
