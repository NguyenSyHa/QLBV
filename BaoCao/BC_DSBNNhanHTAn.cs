using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class BC_DSBNNhanHTAn : DevExpress.XtraReports.UI.XtraReport
    {
        public BC_DSBNNhanHTAn()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcq.Text = txtcq2.Text = DungChung.Bien.TenCQ;
            txtcqcq.Text = txtcqcq2.Text = DungChung.Bien.TenCQCQ;
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }

        public void BindingData()
        {
            cellten.DataBindings.Add("Text", DataSource, "TenBNhan");
            celldoituong.DataBindings.Add("Text", DataSource, "MaDTuong");
            cellhsbn.DataBindings.Add("Text", DataSource, "SoBA");
            cellbhyt.DataBindings.Add("Text", DataSource, "SThe");
            celldiachi.DataBindings.Add("Text", DataSource, "DChi");
            cellngayvv.DataBindings.Add("Text", DataSource, "NgayVao").FormatString = "{0:dd/MM}";
            cellngayrv.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM}";
            cellngayhuongcda.DataBindings.Add("Text", DataSource, "SoNgay");
        }
    }
}
