using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_THThuVP_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_THThuVP_27023()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "27023")
                xrTableRow34.Visible = true;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celKeToan.Text = DungChung.Bien.KeToanTruong;
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            celNgayThang.Text = DungChung.Bien.DiaDanh + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }

        public void BindingData()
        {
            celKhamBenh.DataBindings.Add("Text", DataSource, "KhamBenh").FormatString = DungChung.Bien.FormatString[1];
            celSieuAm.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            celXQ.DataBindings.Add("Text", DataSource, "XQ").FormatString = DungChung.Bien.FormatString[1];
            celXQ_CT.DataBindings.Add("Text", DataSource, "XQ_CT").FormatString = DungChung.Bien.FormatString[1];
            celXetNghiem.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            celDienTim.DataBindings.Add("Text", DataSource, "DienTim").FormatString = DungChung.Bien.FormatString[1];
            celDoCNHH.DataBindings.Add("Text", DataSource, "DoCNHH").FormatString = DungChung.Bien.FormatString[1];
            celThuThuat.DataBindings.Add("Text", DataSource, "ThuThuat").FormatString = DungChung.Bien.FormatString[1];
            celCongI.DataBindings.Add("Text", DataSource, "CongI").FormatString = DungChung.Bien.FormatString[1];
            celTienUng.DataBindings.Add("Text", DataSource, "TienUng").FormatString = DungChung.Bien.FormatString[1];
            celThuThem.DataBindings.Add("Text", DataSource, "ThuThem").FormatString = DungChung.Bien.FormatString[1];
            celTraLai.DataBindings.Add("Text", DataSource, "TraLai").FormatString = DungChung.Bien.FormatString[1];
            celCongII.DataBindings.Add("Text", DataSource, "CongII").FormatString = DungChung.Bien.FormatString[1];
            celTongCong.DataBindings.Add("Text", DataSource, "TongCong").FormatString = DungChung.Bien.FormatString[1];
            celTTRaVien.DataBindings.Add("Text", DataSource, "TTRaVien").FormatString = DungChung.Bien.FormatString[1];
            NTruTruThuThem.DataBindings.Add("Text", DataSource, "NTruTruThuThem").FormatString = DungChung.Bien.FormatString[1];
            celThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
