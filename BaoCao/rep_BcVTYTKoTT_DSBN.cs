using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BcVTYTKoTT_DSBN : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BcVTYTKoTT_DSBN()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenNhomThuoc");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            colSoDK.DataBindings.Add("Text", DataSource, "SoDK");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi").FormatString = DungChung.Bien.FormatString[1];
            ColSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            ColSoLuongGp1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colSoLuongTong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomThuoc"));
        }

        private void TopMargin_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell27.Text = "Phó trưởng khoa phòng khám_dược_CLS";
            }
            
            txtKTT.Text = DungChung.Bien.KeToanTruong;
            txtTKD.Text = DungChung.Bien.TruongKhoaDuoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }
        
        int stt = 1;
        private void GroupHeader1_BeforePrint_1(object sender, CancelEventArgs e)
        {       
           
                    colCongGr.Text = "Cộng "+ stt+": ";
            stt++;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lbTenDV.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTieuDe.Text = "BÁO CÁO VẬT TƯ Y TẾ TIÊU HAO THEO KHOA|PHÒNG ";
        }

        
    }
}
