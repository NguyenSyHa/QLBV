using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuTheoDoiTruyenDich : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuTheoDoiTruyenDich()
        {
            InitializeComponent();
        }
        public void databinding()
        {
            CELNGAY.DataBindings.Add("Text", DataSource, "NgayKe");
            CELTENDV.DataBindings.Add("Text", DataSource, "TenDV");
            CELSOLUONG.DataBindings.Add("Text", DataSource, "SoLuong");
            CELSOLO.DataBindings.Add("Text", DataSource, "SoLo");
            CELTOCDO.DataBindings.Add("Text", DataSource, "TocDo");
            CELBD.DataBindings.Add("Text", DataSource, "BatDau");
            CELKT.DataBindings.Add("Text", DataSource, "KetThuc");
            CELBSCD.DataBindings.Add("Text", DataSource, "TenCBKe");
            CELBSTH.DataBindings.Add("Text", DataSource, "TenCBTh");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }
        }
    }
}
