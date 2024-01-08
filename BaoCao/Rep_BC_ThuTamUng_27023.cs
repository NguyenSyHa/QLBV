using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_ThuTamUng_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_ThuTamUng_27023()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celSoCT.DataBindings.Add("Text", DataSource, "SoHD");
            celNgayThang.DataBindings.Add("Text", DataSource, "NgayThu").FormatString = "{0:dd/MM}";
            celHoten.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celLanTU.DataBindings.Add("Text", DataSource, "SoLan");
            celThuTU.DataBindings.Add("Text", DataSource, "SotienTU").FormatString = DungChung.Bien.FormatString[1];
            celThuThieu.DataBindings.Add("Text", DataSource, "ThuThieu").FormatString = DungChung.Bien.FormatString[1];
            celThuTU_T.DataBindings.Add("Text", DataSource, "SotienTU").FormatString = DungChung.Bien.FormatString[1];
            celThuThieu_T.DataBindings.Add("Text", DataSource, "ThuThieu").FormatString = DungChung.Bien.FormatString[1];
            celThuTU_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celThuThieu_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celMucBH.DataBindings.Add("Text", DataSource, "Muc");
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
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
