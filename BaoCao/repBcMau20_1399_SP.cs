using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcMau20_1399_SP : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcMau20_1399_SP()
        {
            InitializeComponent();
        }
        bool inTieuNhom = false;
        public repBcMau20_1399_SP(bool inTN)
        {
            InitializeComponent();
            inTieuNhom = inTN;
        }
        public void BindingData()
        {
            colTenThuocGh2.DataBindings.Add("Text", DataSource, "TenNhomThuoc");
            colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTNhom");
            colTenHC.DataBindings.Add("Text", DataSource, "TenHC");
            colDuongDung.DataBindings.Add("Text", DataSource, "DuongDung");
            colSTT.DataBindings.Add("Text", DataSource, "STT");
            colSTTQD.DataBindings.Add("Text", DataSource, "MaQD");
            colTenThuoc.DataBindings.Add("Text", DataSource, "TenThuoc");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colHamLuong.DataBindings.Add("Text", DataSource, "HamLuong");
            colSoDK.DataBindings.Add("Text", DataSource, "SoDK");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi").FormatString = DungChung.Bien.FormatString[1];
            ColSoLuongNgoaiT.DataBindings.Add("Text", DataSource, "SoLuongNgT").FormatString = DungChung.Bien.FormatString[0];
            ColSoLuongNoiTru.DataBindings.Add("Text", DataSource, "SoLuongNT").FormatString = DungChung.Bien.FormatString[0];
            //ColSoLuongGp1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colSTT.DataBindings.Add("Text", DataSource, "SoTTqd");
            GroupHeader2.GroupFields.Add(new GroupField("TenNhomThuoc"));
            GroupHeader1.GroupFields.Add(new GroupField("TenTNhom"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTKD.Text = DungChung.Bien.TruongKhoaDuoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = "Tên CSYT: "+DungChung.Bien.TenCQ.ToUpper();
            colMaCQ.Text = "Mã CSYT: " +DungChung.Bien.MaBV;
            GroupHeader1.Visible = inTieuNhom;
            GroupFooter1.Visible = inTieuNhom;
          
        }
        int stt = 1;
  
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (stt)
            {
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
                case 6:
                    colSTTGh1.Text = "F";
                    colCongGr.Text = "Cộng F:";
                    break;
                case 7:
                    colSTTGh1.Text = "G";
                    colCongGr.Text = "Cộng G:";
                    break;
                case 8:
                    colSTTGh1.Text = "H";
                    colCongGr.Text = "Cộng H:";
                    break;
                case 9:
                    colSTTGh1.Text = "I";
                    colCongGr.Text = "Cộng I:";
                    break;

            }
            stt++;
        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupFooter2.Visible = false;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
               
        }
    }
}
