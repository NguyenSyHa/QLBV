using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_GiaoBanChuyenMon_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_GiaoBanChuyenMon_01071()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }

        public void BindingData()
        {
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celBNCu.DataBindings.Add("Text", DataSource, "BNCu").FormatString=("{0:##,###}");
            celBNVaoVien.DataBindings.Add("Text", DataSource, "BNVV").FormatString = ("{0:##,###}");
            celBNRaVien.DataBindings.Add("Text", DataSource, "BNRaVien1").FormatString = ("{0:##,###}");
            celChuyemVien.DataBindings.Add("Text", DataSource, "BNChuyenVien").FormatString = ("{0:##,###}");
            celBNBHYTHienCo.DataBindings.Add("Text", DataSource, "BNBHYTHienCo").FormatString = ("{0:##,###}");
            celBNDVHienCo.DataBindings.Add("Text", DataSource, "BNDVHienCo").FormatString = ("{0:##,###}");
            celBNCKHienCo.DataBindings.Add("Text", DataSource, "BNCKHienCo").FormatString = ("{0:##,###}");

            celBNCu_T.DataBindings.Add("Text", DataSource, "BNCu").FormatString = ("{0:##,###}");
            celBNVaoVien_T.DataBindings.Add("Text", DataSource, "BNVV").FormatString = ("{0:##,###}");
            celBNRaVien_T.DataBindings.Add("Text", DataSource, "BNRaVien1").FormatString = ("{0:##,###}");
            celChuyemVien_T.DataBindings.Add("Text", DataSource, "BNChuyenVien").FormatString = ("{0:##,###}");
            celBNBHYTHienCo_T.DataBindings.Add("Text", DataSource, "BNBHYTHienCo").FormatString = ("{0:##,###}");
            celBNDVHienCo_T.DataBindings.Add("Text", DataSource, "BNDVHienCo").FormatString = ("{0:##,###}");
            celBNCKHienCo_T.DataBindings.Add("Text", DataSource, "BNCKHienCo").FormatString = ("{0:##,###}");
        }
    }
}
