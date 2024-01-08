using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_KetQuaHDThang : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_KetQuaHDThang()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            lblTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        public void BindingData()
        {
            celNoiDung.DataBindings.Add("Text", DataSource, "TenChiTiet");
            celDVT.DataBindings.Add("Text", DataSource, "DVT");
            celKH.DataBindings.Add("Text", DataSource, "chitieu");
            celThangTruoc.DataBindings.Add("Text", DataSource, "thangtruoc");
            celThangNay.DataBindings.Add("Text", DataSource, "thangnay");
            celLuyKe.DataBindings.Add("Text", DataSource, "tongdenthangnay");
            celDat.DataBindings.Add("Text", DataSource, "");

            gr_celNoiDung.DataBindings.Add("Text", DataSource, "TenNhom1");
            gr_celDVT.DataBindings.Add("Text", DataSource, "DVT");
            gr_celKH.DataBindings.Add("Text", DataSource, "chitieu");
            gr_celThangTruoc.DataBindings.Add("Text", DataSource, "thangtruoc");
            gr_celThangNay.DataBindings.Add("Text", DataSource, "thangnay");
            gr_celLuyKe.DataBindings.Add("Text", DataSource, "tongdenthangnay");
            GroupHeader1.GroupFields.Add(new GroupField("Stt"));
            stt.DataBindings.Add("Text", DataSource, "Stt");
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
