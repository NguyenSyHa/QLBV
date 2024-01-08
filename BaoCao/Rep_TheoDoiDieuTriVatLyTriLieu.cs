using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_TheoDoiDieuTriVatLyTriLieu : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TheoDoiDieuTriVatLyTriLieu()
        {
            InitializeComponent();
        }
        public void Binding()
        {
            colTenTT.DataBindings.Add("Text", DataSource, "TenDV");
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
