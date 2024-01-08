using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BaoCaoChiTraBN_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BaoCaoChiTraBN_27023()
        {
            InitializeComponent();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayThang.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + DateTime.Now.Year;
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            celGiamdoc.Text = DungChung.Bien.GiamDoc;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            celKhoaPhong.Text = DungChung.Bien.MaBV == "27022" ? "Phòng TCKT" : "";
        }


        internal void BindingData()
        {
            celQuyen.DataBindings.Add("Text", DataSource, "QuyenHD");
            celSoCT.DataBindings.Add("Text", DataSource, "SoHD");            
            celNGayVao.DataBindings.Add("Text", DataSource, "NNhap").FormatString = "{0:dd/MM}";
            celNgayRa.DataBindings.Add("Text", DataSource, "NgayThu").FormatString = "{0:dd/MM}";
            celTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celSoTienTU.DataBindings.Add("Text", DataSource, "SotienTU").FormatString = DungChung.Bien.FormatString[1];
            celSoTT.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            celChitra.DataBindings.Add("Text", DataSource, "TienChenh").FormatString = DungChung.Bien.FormatString[1];
            celSoTienTU_T.DataBindings.Add("Text", DataSource, "SotienTU").FormatString = DungChung.Bien.FormatString[1];
            celSoTT_T.DataBindings.Add("Text", DataSource, "SoTien").FormatString = DungChung.Bien.FormatString[1];
            celChitra_T.DataBindings.Add("Text", DataSource, "TienChenh").FormatString = DungChung.Bien.FormatString[1];
            celPhanTramBN.DataBindings.Add("Text", DataSource, "Muc");
            
        }

        private void celSoCT_BeforePrint(object sender, CancelEventArgs e)
        {
            if (celSoCT.Text == "0")
                celSoCT.Text = "";
        }
    }
}
