using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_KQKhamDT_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_KQKhamDT_27022()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            celTieuDe_Gr.DataBindings.Add("Text", DataSource, "TieuDe");
            celSoLieu_Gr.DataBindings.Add("Text", DataSource, "KQTH");

            celChiTieu.DataBindings.Add("Text", DataSource, "ChiTiet");
            celSoLieu.DataBindings.Add("Text", DataSource, "KQTH");

            GroupHeader1.GroupFields.Add(new GroupField("Stt"));
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("ChiTiet") != null)
            {
                string nt = this.GetCurrentColumnValue("ChiTiet").ToString();
                if (nt == "")
                    xrTableRow2.Visible = false;
                else
                    xrTableRow2.Visible = true;

            }
        }
    }
}
