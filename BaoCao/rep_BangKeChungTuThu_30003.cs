using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BangKeChungTuThu_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BangKeChungTuThu_30003()
        {
            InitializeComponent();
        }
        public void databinding()
        {
            ColSoBL.DataBindings.Add("Text", DataSource, "SoHD");
            coltenbn.DataBindings.Add("Text", DataSource, "TenBNhan");
            coldchi.DataBindings.Add("Text", DataSource, "DChi");
            colKP.DataBindings.Add("Text", DataSource, "TenKP");
            coltendv.DataBindings.Add("Text", DataSource, "LyDo");
            colsl.DataBindings.Add("Text", DataSource, "DonVi");
            coldvt.DataBindings.Add("Text", DataSource, "SoLuong");
            coldongia.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            colthanhtien.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            coltongtt.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
        }

        private void SubBand1_BeforePrint(object sender, CancelEventArgs e)
        {
            xrTableCell32.Text = DungChung.Bien.KeToanTruong;
        }
    }
}
