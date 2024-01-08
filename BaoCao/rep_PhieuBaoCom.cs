using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuBaoCom : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuBaoCom()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcq.Text = txtcq2.Text = DungChung.Bien.TenCQ.ToUpper();
            xrPictureBox2.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }
        public void BindingData()
        {
            celTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celSoTien.DataBindings.Add("Text", DataSource, "");
        }
    }
}
