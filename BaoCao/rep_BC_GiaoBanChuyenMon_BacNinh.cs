using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_GiaoBanChuyenMon_BacNinh : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_GiaoBanChuyenMon_BacNinh()
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
            celBNRaVien.DataBindings.Add("Text", DataSource, "BNRaVien").FormatString = ("{0:##,###}");
            celBNHienCo.DataBindings.Add("Text", DataSource, "BNHienCo").FormatString = ("{0:##,###}");

            celBNCu_T.DataBindings.Add("Text", DataSource, "BNCu").FormatString = ("{0:##,###}");
            celBNVaoVien_T.DataBindings.Add("Text", DataSource, "BNVV").FormatString = ("{0:##,###}");
            celBNRaVien_T.DataBindings.Add("Text", DataSource, "BNRaVien").FormatString = ("{0:##,###}");
            celBNHienCo_T.DataBindings.Add("Text", DataSource, "BNHienCo").FormatString = ("{0:##,###}"); 
        }
    }
}
