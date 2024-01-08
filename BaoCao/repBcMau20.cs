using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcMau20 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcMau20()
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
            ColSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            //ColSoLuongGp1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
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
            txtKTT.Text = DungChung.Bien.KeToanTruong;
            txtTKD.Text = DungChung.Bien.TruongKhoaDuoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }
        
        int stt = 1;
        private void GroupHeader1_BeforePrint_1(object sender, CancelEventArgs e)
        {       
            switch (stt) {
                case 1:
                    colSTTGh1.Text = "A";
                    colCongGr.Text = "Cộng A:";
                    break;
                case 2:
                    colSTTGh1.Text = "B";
                    colCongGr.Text = "Cộng B:";
                    break;
                case 3:
                    colSTTGh1.Text = "C";
                    colCongGr.Text = "Cộng C:";
                    break;
                case 4:
                    colSTTGh1.Text = "D";
                    colCongGr.Text = "Cộng D:";
                    break;
                case 5:
                    colSTTGh1.Text = "E";
                    colCongGr.Text = "Cộng E:";
                    break;

            }
            stt++;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            lbTenCQ.Text = DungChung.Bien.TenCQ;
            txtTieuDe.Text = "THỐNG KÊ TỔNG HỢP THUỐC SỬ DỤNG " + Quy.Value.ToString();
        }

        
    }
}
