using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_BenhTruyenNhiemThang_30004 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_BenhTruyenNhiemThang_30004()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblCQCQ.Text = "Cơ quan chủ quản: " + DungChung.Bien.TenCQCQ;
            lblTenCQ.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            lblNgayThang.Text = "........................., ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }
        public void BindingData()
        {
            celDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            celM1.DataBindings.Add("Text", DataSource, "M1");
            celC1.DataBindings.Add("Text", DataSource, "C1");
            celM2.DataBindings.Add("Text", DataSource, "M2");
            celC2.DataBindings.Add("Text", DataSource, "C2");
            celM3.DataBindings.Add("Text", DataSource, "M3");
            celC3.DataBindings.Add("Text", DataSource, "C3");
            celM4.DataBindings.Add("Text", DataSource, "M4");
            celC4.DataBindings.Add("Text", DataSource, "C4");
            celM5.DataBindings.Add("Text", DataSource, "M5");
            celC5.DataBindings.Add("Text", DataSource, "C5");
            celM6.DataBindings.Add("Text", DataSource, "M6");
            celC6.DataBindings.Add("Text", DataSource, "C6");
            celM7.DataBindings.Add("Text", DataSource, "M7");
            celC7.DataBindings.Add("Text", DataSource, "C7");
            celM8.DataBindings.Add("Text", DataSource, "M8");
            celC8.DataBindings.Add("Text", DataSource, "C8");

            celM1T.DataBindings.Add("Text", DataSource, "M1");
            celC1T.DataBindings.Add("Text", DataSource, "C1");
            celM2T.DataBindings.Add("Text", DataSource, "M2");
            celC2T.DataBindings.Add("Text", DataSource, "C2");
            celM3T.DataBindings.Add("Text", DataSource, "M3");
            celC3T.DataBindings.Add("Text", DataSource, "C3");
            celM4T.DataBindings.Add("Text", DataSource, "M4");
            celC4T.DataBindings.Add("Text", DataSource, "C4");
            celM5T.DataBindings.Add("Text", DataSource, "M5");
            celC5T.DataBindings.Add("Text", DataSource, "C5");
            celM6T.DataBindings.Add("Text", DataSource, "M6");
            celC6T.DataBindings.Add("Text", DataSource, "C6");
            celM7T.DataBindings.Add("Text", DataSource, "M7");
            celC7T.DataBindings.Add("Text", DataSource, "C7");
            celM8T.DataBindings.Add("Text", DataSource, "M8");
            celC8T.DataBindings.Add("Text", DataSource, "C8");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
        }
    }
}
