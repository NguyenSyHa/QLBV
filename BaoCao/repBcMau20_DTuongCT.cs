using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcMau20_DTuongCT : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcMau20_DTuongCT()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "30350")
            {
                xrTableCell31.Text = "Phụ trách kho dược";
                xrTableCell33.Text = "Bệnh xá trưởng";
            }
        }
        bool inTieuNhom = false;
        public repBcMau20_DTuongCT(bool inTN)
        {
            InitializeComponent();
            inTieuNhom = inTN;
            if (DungChung.Bien.MaBV == "30350")
            {
                xrTableCell31.Text = "Phụ trách kho dược";
                xrTableCell33.Text = "Bệnh xá trưởng";
            }
        }
        public void BindingData()
        {
           
            colTenHC.DataBindings.Add("Text", DataSource, "TenHC");
            if (DungChung.Bien.MaBV == "26007")
                colDuongDung.DataBindings.Add("Text", DataSource, "Duong_dung");
            else
                colDuongDung.DataBindings.Add("Text", DataSource, "Ma_DuongD");
            colSTT.DataBindings.Add("Text", DataSource, "SoTTqd");
            colSTTQD.DataBindings.Add("Text", DataSource, "MaQD");
            colTenThuoc.DataBindings.Add("Text", DataSource, "Ten_thuoc");
            colDonGia.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
            colHamLuong.DataBindings.Add("Text", DataSource, "Ham_luong");
            colSoDK.DataBindings.Add("Text", DataSource, "So_dang_ky");
            colDonVi.DataBindings.Add("Text", DataSource, "Don_vi_tinh");
            ColSoLuongBH_DV.DataBindings.Add("Text", DataSource, "SoLuongBHYT_DV").FormatString = DungChung.Bien.FormatString[1];
            ColSoLuong139.DataBindings.Add("Text", DataSource, "SoLuong139").FormatString = DungChung.Bien.FormatString[1];
            ColSoLuongTE.DataBindings.Add("Text", DataSource, "SoLuongTE").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp1.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp2.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
           // colSTT.DataBindings.Add("Text", DataSource, "SoTTqd");
            if (inTieuNhom)
            {
                colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTN");
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            }
            if (DungChung.Bien.MaBV == "27001")
            {
                lblSapXep.DataBindings.Add("Text", DataSource, "SapXep");
                GroupHeader2.GroupFields.Add(new GroupField("SapXep"));
            }
            else
            {
                colTenThuocGh2.DataBindings.Add("Text", DataSource, "Tennhom");
                GroupHeader2.GroupFields.Add(new GroupField("Tennhom"));
            }
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
            if (DungChung.Bien.MaBV == "30340")
                xrTableCell26.Text = "Kế toán";
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

     
        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void colTenThuocGh2_BeforePrint(object sender, CancelEventArgs e)
        {

            if (this.GetCurrentColumnValue("SapXep") != null)
            {
                int stt = Convert.ToInt16(this.GetCurrentColumnValue("SapXep"));
                if (stt == 1)
                {
                    colTenThuocGh2.Text = "1. Thuốc tân dược";
                }
                if (stt == 2)
                {
                    colTenThuocGh2.Text = "2. Chế phẩm y học cổ truyền";
                }
                if (stt == 3)
                {
                    colTenThuocGh2.Text = "3. Thuốc y học cổ truyền";
                }
            }
        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {

            if (this.GetCurrentColumnValue("SapXep") != null)
            {
                int stt = Convert.ToInt16(this.GetCurrentColumnValue("SapXep"));
                if (stt == 1)
                {
                    colGrpt2.Text = "Tổng thuốc tân dược";
                }
                if (stt == 2)
                {
                    colGrpt2.Text = "Tổng chế phẩm y học cổ truyền";
                }
                if (stt == 3)
                {
                    colGrpt2.Text = "Tổng thuốc y học cổ truyền";
                }
            }
        }
    }
}
