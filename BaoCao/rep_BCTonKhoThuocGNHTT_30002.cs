using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCTonKhoThuocGNHTT_30002 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCTonKhoThuocGNHTT_30002()
        {
            InitializeComponent();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiBaoCao.Text = DungChung.Bien.NguoiLapBieu;
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
            celDiadanh.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;


        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celkinhgui.Text = "Kính gửi: " + DungChung.Bien.TenCQCQ;
        }


        internal void BindingData()
        {
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celDVT.DataBindings.Add("Text", DataSource, "DonVi");
            celTonDK.DataBindings.Add("Text", DataSource, "TonDK").FormatString = DungChung.Bien.FormatString[1];
            celNhapTK.DataBindings.Add("Text", DataSource, "NhapTK").FormatString = DungChung.Bien.FormatString[1];
            celTongDauVao.DataBindings.Add("Text", DataSource, "TongDauVao").FormatString = DungChung.Bien.FormatString[1];
            celXuatTK.DataBindings.Add("Text", DataSource, "XuatTK").FormatString = DungChung.Bien.FormatString[1];
            celHuHao.DataBindings.Add("Text", DataSource, "HuHao").FormatString = DungChung.Bien.FormatString[1];
            celTonCuoiKy.DataBindings.Add("Text", DataSource, "TonCK").FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
