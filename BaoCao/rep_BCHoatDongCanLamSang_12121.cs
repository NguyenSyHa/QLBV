using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCHoatDongCanLamSang_12121 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCHoatDongCanLamSang_12121()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "12121")
                celPhong.Text = "KHOA CẬN LÂM SÀNG";
        }

        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            ngayth.Text = "Ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year;
        }

    }
}
