using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_ChiTraTienUngThua_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_ChiTraTienUngThua_27023()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ;
        }

        public void BindingData()
        {
            celSoCT.DataBindings.Add("Text", DataSource, "SoHD");
            celNgayThang.DataBindings.Add("Text", DataSource, "NgayThu").FormatString = "{0:dd/MM}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celSoTien.DataBindings.Add("Text", DataSource, "TienChenh").FormatString = DungChung.Bien.FormatString[1];
            celSoTien_T.DataBindings.Add("Text", DataSource, "TienChenh").FormatString = DungChung.Bien.FormatString[1];
            celSoTien_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celGhiChu.DataBindings.Add("Text", DataSource, "Muc");
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayIn.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }

        private void celSoCT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (celSoCT.Text == "0")
                celSoCT.Text = "";
        }
    }
}
