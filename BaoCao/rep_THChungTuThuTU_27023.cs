using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.FormThamSo
{
    public partial class rep_THChungTuThuTU_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_THChungTuThuTU_27023()
        {
            InitializeComponent();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
            celNgayThang.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + DateTime.Now.Year;
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            celGiamdoc.Text = DungChung.Bien.GiamDoc;
        }


        internal void BindingData()
        {           
            celSoCT.DataBindings.Add("Text", DataSource, "SoHD");          
            celNgay.DataBindings.Add("Text", DataSource, "NgayThu").FormatString = "{0:dd/MM}";
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celSoTien.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            celSotien_T.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            celTyLeBN.DataBindings.Add("Text", DataSource, "Muc");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            celKhoaPhong.Text = DungChung.Bien.MaBV == "27022" ? "Phòng TCKT" : "";
        }
    }
}
