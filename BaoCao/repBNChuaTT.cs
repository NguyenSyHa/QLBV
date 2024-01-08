using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBNChuaTT : DevExpress.XtraReports.UI.XtraReport
    {
        public repBNChuaTT()
        {
            InitializeComponent();
            xrLabel2.Text = "THỐNG KÊ BỆNH NHÂN CHƯA THANH TOÁN";
            TenCQ.Value = DungChung.Bien.TenCQ;
            //xrLbThoigian.Text = "Tu ngay "+ 
        }

        public void BindingData()
        {
            colMaBN1.DataBindings.Add("Text", DataSource, "MaBN");
            colTenBN1.DataBindings.Add("Text", DataSource, "TenBN");
            //colDTuong1.DataBindings.Add("Text", DataSource, "Dtuong");
            colDiaChi1.DataBindings.Add("Text",DataSource,"DiaChi");
            colKP1.DataBindings.Add("Text", DataSource, "KhoaPhong");
            colIDDon1.DataBindings.Add("Text", DataSource, "IdDT");
            GroupHeader1.GroupFields.Add(new GroupField("IdDT"));
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colNgayKe1.DataBindings.Add("Text", DataSource, "NgayKe").FormatString="{0:dd/MM/yyyy}";
            colGrFooterThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString=DungChung.Bien.FormatString[1];
            colGrReportThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
        }
        
        private void colSTT_BeforePrint(object sender, CancelEventArgs e)
        {
            //stt++;
            //colSTT.Text = stt.ToString();
        }
        int sttCong = 0;
        private void colCongGr_BeforePrint(object sender, CancelEventArgs e)
        {
            sttCong++;
            colCongGr.Text = "Tổng Tiền Bệnh Nhân Thứ " + sttCong;
            colSTT1.Text = "Bệnh Nhân " + sttCong;
        }

    }
}
