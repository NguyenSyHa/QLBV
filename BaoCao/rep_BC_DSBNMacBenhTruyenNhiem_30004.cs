using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_DSBNMacBenhTruyenNhiem_30004 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_DSBNMacBenhTruyenNhiem_30004()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblCQCQ.Text = "Cơ quan chủ quản: " + DungChung.Bien.TenCQCQ;
            lblTenCQ.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            lblNgayThang.Text = "........................., ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
        }
        public void BindingData()
        {
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");
            celTuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            celGTinh.DataBindings.Add("Text", DataSource, "GioiTinh");
            celDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            celSDT.DataBindings.Add("Text", DataSource, "SDT");
            celNgayKhoiPhat.DataBindings.Add("Text", DataSource, "NgayKhoiPhat").FormatString = "{0:dd/MM/yyyy}";
            celChanDoan.DataBindings.Add("Text", DataSource, "ChanDoan");
            //celKQXetNghiem.DataBindings.Add("Text", DataSource, "KQXN");
            celKQRaVien.DataBindings.Add("Text", DataSource, "KetQua");
        }
    }
}
