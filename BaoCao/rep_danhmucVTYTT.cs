using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_danhmucVTYTT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_danhmucVTYTT()
        {
            InitializeComponent();
        }
        public void inbaocao() {

            CQ.Text = CQ2.Text = DungChung.Bien.TenCQ;
            CQCQ.Text = CQCQ2.Text = DungChung.Bien.TenCQCQ;
            mavtytbv.DataBindings.Add("Text", DataSource,"MaTam");
            mavtyt.DataBindings.Add("Text", DataSource,"SoTTqd");
            tenvtyt.DataBindings.Add("Text", DataSource,"TenDV");
            quycach.DataBindings.Add("Text", DataSource, "QCPC");
            nuocsx.DataBindings.Add("Text", DataSource, "NuocSX");
            hangsx.DataBindings.Add("Text", DataSource, "NhaSX");
            donvitinh.DataBindings.Add("Text", DataSource, "DonVi");
            giathau.DataBindings.Add("Text", DataSource, "");
            giabhyt.DataBindings.Add("Text", DataSource, "");
            dinhmuc.DataBindings.Add("Text", DataSource, "");
            soluong.DataBindings.Add("Text", DataSource, "");
            macsdkkcb.Text = DungChung.Bien.MaBV;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }
    }
}
