using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repDTNoiTruHangNgay_HL01b3 : DevExpress.XtraReports.UI.XtraReport
    {
        public repDTNoiTruHangNgay_HL01b3()
        {
            InitializeComponent();
        }
       // repDTNoiTruHangNgay_HL01b rep = (repDTNoiTruHangNgay_HL01b)xrSubreport1.ReportSource;
        public void BindingData()
        {
            colTenNhomDV.DataBindings.Add("Text", DataSource, "TenNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colSTT.DataBindings.Add("Text", DataSource, "STT");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1]; ;
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1]; ;
            colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colNgay.DataBindings.Add("Text", DataSource, "NgayNhap");
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomDV"));

        }
       
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            //colTruongKhoaDT.Text = DungChung.Bien.TruongKhoaLS;
            //colKeToan.Text = DungChung.Bien.KeToanTruong;
        }

        private void colNgay_BeforePrint(object sender, CancelEventArgs e)
        {
             if (this.Ngay.Value != null && this.Ngay.Value.ToString() == "1")
                 colNgay.Visible = false;
        }
    }
}
