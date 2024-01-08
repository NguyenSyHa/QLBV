using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_THBNThanhToanRaVien_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_THBNThanhToanRaVien_27023()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celDT.DataBindings.Add("Text", DataSource, "DT");
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celTongTU.DataBindings.Add("Text", DataSource, "TienUng").FormatString = DungChung.Bien.FormatString[1];
            celThanhToan.DataBindings.Add("Text", DataSource, "ThanhToan").FormatString = DungChung.Bien.FormatString[1];
            celChiTra.DataBindings.Add("Text", DataSource, "ChiTra").FormatString = DungChung.Bien.FormatString[1];
            celThuThieu.DataBindings.Add("Text", DataSource, "ThuThieu").FormatString = DungChung.Bien.FormatString[1];

            celTongTU_T.DataBindings.Add("Text", DataSource, "TienUng").FormatString = DungChung.Bien.FormatString[1];
            celThanhToan_T.DataBindings.Add("Text", DataSource, "ThanhToan").FormatString = DungChung.Bien.FormatString[1];
            celChiTra_T.DataBindings.Add("Text", DataSource, "ChiTra").FormatString = DungChung.Bien.FormatString[1];
            celThuThieu_T.DataBindings.Add("Text", DataSource, "ThuThieu").FormatString = DungChung.Bien.FormatString[1];

            celTongTU_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThanhToan_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celChiTra_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuThieu_T.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            lblNgayIn.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
