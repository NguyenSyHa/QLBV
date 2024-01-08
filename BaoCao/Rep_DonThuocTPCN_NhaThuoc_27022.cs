using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_DonThuocTPCN_NhaThuoc_27022 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DonThuocTPCN_NhaThuoc_27022
()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lbTenCQCQ.Text = lbTenCQCQ2.Text = DungChung.Bien.TenCQCQ;
            lbTenCQ.Text = lbTenCQ2.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }

        public void BindingData()
        {
            celThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celDVT.DataBindings.Add("Text", DataSource, "DonVi");
            celMaThuoc.DataBindings.Add("Text", DataSource, "MaThuoc");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            celCachDung.DataBindings.Add("Text", DataSource, "CachDung");
            celCachDung.DataBindings.Add("Text", DataSource, "CachDung");
        }

    }
}
