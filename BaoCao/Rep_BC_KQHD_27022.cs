using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_KQHD_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_KQHD_27022()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celGroup1.DataBindings.Add("Text", DataSource, "Group");

            celSTT.DataBindings.Add("Text", DataSource, "Stt");
            celChiTieuGr.DataBindings.Add("Text", DataSource, "Nhom");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celSoLieuGr.DataBindings.Add("Text", DataSource, "SoLieu");

            celChiTiet.DataBindings.Add("Text", DataSource, "ChiTiet");
            celSoLieu.DataBindings.Add("Text", DataSource, "SoLieu");

            GroupHeader1.GroupFields.Add(new GroupField("Group"));
            GroupHeader2.GroupFields.Add(new GroupField("Stt"));
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("ChiTiet") != null)
            {
                string nt = this.GetCurrentColumnValue("ChiTiet").ToString();
                if (nt == "")
                    xrTableRow4.Visible = false;
                else
                    xrTableRow4.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
