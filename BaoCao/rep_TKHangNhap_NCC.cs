using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TKHangNhap_NCC : DevExpress.XtraReports.UI.XtraReport
    {       
        public rep_TKHangNhap_NCC()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
        public void BindingData()
        {
           // colSTTNhom.DataBindings.Add("Text", DataSource, "STTN");
            colNhomThuoc.DataBindings.Add("Text", DataSource, "TenTN");
            colMaThuoc.DataBindings.Add("Text", DataSource, "MaDV");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            colTenDV.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString=DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString=DungChung.Bien.FormatString[1];
            colThanhTienGr.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString=DungChung.Bien.FormatString[1];
            colTongTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString=DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell12.Text = "PHÓ TRƯỞNG KHOA PHÒNG KHÁM_DƯỢC_CLS";
                xrTableCell12.Font = new Font("Times New Roman", 12);
                colTruongKhoaDuoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 20, 0);
                colKeToan.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 20, 0);
                colThuKho.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 20, 0);
                colThongKeDuoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 20, 0);
            }
            colTruongKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colKeToan.Text = DungChung.Bien.KeToanTruong;
        }
        int _stt = 0;       

        private void GroupHeader1_BeforePrint_1(object sender, CancelEventArgs e)
        {
            _stt++;
            if (_stt < 10)
            {
                colSTTNhom.Text = "0" + _stt.ToString() + ":";
                colNhomTT.Text = "0" + _stt.ToString();
            }
            else
            {
                colSTTNhom.Text = _stt.ToString() + ":";
                colNhomTT.Text = _stt.ToString();
            }
        }

       

        
    }
}
