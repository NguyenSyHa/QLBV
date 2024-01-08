using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuTHCD_DichVu : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuTHCD_DichVu()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            colthanhtien.DataBindings.Add("Text", DataSource, "DonGia");
            coltongsl.DataBindings.Add("Text", DataSource, "SoLuong");
            coldongia.DataBindings.Add("Text", DataSource, "DonGia");
            coltongtt.DataBindings.Add("Text", DataSource, "DonGia");
            colTenchidinh.DataBindings.Add("Text", DataSource, "TenDV");
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenBSDT.Text = TenBS.Value.ToString();
            txtGDBHYT.Text = DungChung.Bien.GiamDinhBH;
            if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ;
        }

    }
}
