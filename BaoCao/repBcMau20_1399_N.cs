using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcMau20_1399_N : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcMau20_1399_N()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "30350")
            {
                xrTableCell31.Text = xrTableCell66.Text = "Phụ trách kho dược";
                xrTableCell33.Text = xrTableCell68.Text = "Bệnh xá trưởng";
            }
        }
        bool inTieuNhom = false;
        public repBcMau20_1399_N(bool inTN)
        {
            InitializeComponent();
            inTieuNhom = inTN;
            if (DungChung.Bien.MaBV == "30350")
            {
                xrTableCell31.Text = xrTableCell66.Text = "Phụ trách kho dược";
                xrTableCell33.Text = xrTableCell68.Text = "Bệnh xá trưởng";
            }
        }
        public void BindingData()
        {
           
            
            colTenHC.DataBindings.Add("Text", DataSource, "TenHC");
            if (DungChung.Bien.MaBV == "26007")
                colDuongDung.DataBindings.Add("Text", DataSource, "Duong_dung");
            else
                colDuongDung.DataBindings.Add("Text", DataSource, "Ma_DuongD");
            if (DungChung.Bien.MaBV != "20001")
                colSTT.DataBindings.Add("Text", DataSource, "SoTTqd");
            colSTTQD.DataBindings.Add("Text", DataSource, "MaQD");
            colTenThuoc.DataBindings.Add("Text", DataSource, "Ten_thuoc");
            colDonGia.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
            colHamLuong.DataBindings.Add("Text", DataSource, "Ham_luong");
            colSoDK.DataBindings.Add("Text", DataSource, "So_dang_ky");
            colDonVi.DataBindings.Add("Text", DataSource, "Don_vi_tinh");
            ColSoLuongNoiT.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];
            ColSoLuong.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];
            //ColSoLuongGp1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp1.DataBindings.Add("Text", DataSource, "Thanh_tien");
            colThanhTienGp1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp2.DataBindings.Add("Text", DataSource, "Thanh_tien");
            colThanhTienGp2.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "Thanh_tien");
            colThanhTienTong.Summary.FormatString = DungChung.Bien.FormatString[1];
            if (inTieuNhom)
            {
                colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTN");
                GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            }
           // colSTT.DataBindings.Add("Text", DataSource, "SoTTqd");
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "20001")
            {
                GroupHeader2.DataBindings.Add("Text", DataSource, "TenSapXep");
                GroupHeader2.GroupFields.Add(new GroupField("SapXep"));
            }
            else
            {
                colTenThuocGh2.DataBindings.Add("Text", DataSource, "Tennhom");
                colTenThuocGh2_24012.DataBindings.Add("Text", DataSource, "Tennhom");
                GroupHeader2.GroupFields.Add(new GroupField("Tennhom"));
            }

            if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
            {

                SubBand1.Visible = false;
                SubBand2.Visible = true;
                SubBand7.Visible = true;
                SubBand8.Visible = true;
                SubBand3.Visible = true;
                SubBand4.Visible = false;
                SubBand5.Visible = false;
                SubBand6.Visible = true;
                SubBand8.Visible = false;
                SubBand9.Visible = false;
                SubBand10.Visible = true;
                //
                xrTable13.Visible = true;
                xrTable5.Visible = true;
                xrTable9.Visible = false;
                //
                colSTT_24012.DataBindings.Add("Text", DataSource, "SoTTqd");
                colSTTQD_24012.DataBindings.Add("Text", DataSource, "MaQD");
                colTenThuoc_24012.DataBindings.Add("Text", DataSource, "Ten_thuoc");
                colTenHC_24012.DataBindings.Add("Text", DataSource, "TenHC");
                ColDuongDung_24012.DataBindings.Add("Text", DataSource, "Duong_dung");
                colDangBC_24012.DataBindings.Add("Text", DataSource, "DangBC");
                colHamLuong_24012.DataBindings.Add("Text", DataSource, "Ham_luong");
                colSoDK_24012.DataBindings.Add("Text", DataSource, "So_dang_ky");
                colDonVi_24012.DataBindings.Add("Text", DataSource, "Don_vi_tinh");

                colSoLuongNgoaiTru_24012.DataBindings.Add("Text", DataSource, "SoluongNgT");
                //colSoLuongNgoaiTruTong_24012.DataBindings.Add("Text", DataSource, "SoluongNgT");
                //colSoLuongNgoaiTru_24012.Summary.FormatString = ;
                colSoLuongNoiT_24012.DataBindings.Add("Text", DataSource, "SoluongNT");
                //colSoLuongNoiTruTong_24012.DataBindings.Add("Text", DataSource, "SoluongNT");

                colDonGia_24012.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
                //colDonGiaTong_24012.DataBindings.Add("Text", DataSource, "Don_gia");
                //colDonGiaTong_24012.Summary.FormatString = DungChung.Bien.FormatString[1];

                colDonGiaBH_24012.DataBindings.Add("Text", DataSource, "Don_giaBHYT").FormatString = DungChung.Bien.FormatString[1];
                //colDonGiaBHTong_24012.DataBindings.Add("Text", DataSource, "Don_giaBHYT");
                //colDonGiaBHTong_24012.Summary.FormatString = DungChung.Bien.FormatString[1];

                colTyLeTT_24012.DataBindings.Add("Text", DataSource, "TyLeTT");
                //colTyLeTTTong_24012.DataBindings.Add("Text", DataSource, "TyLeTT");

                colThanhTien_24012.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
                colThanhTienTong_24012.DataBindings.Add("Text", DataSource, "Thanh_tien");
                colThanhTienTong_24012.Summary.FormatString = DungChung.Bien.FormatString[1];

                colTienDeNghiBH_24012.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT").FormatString = DungChung.Bien.FormatString[1];
                colTongTienDeNghi_24012.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT");
                colTongTienDeNghi_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
                xrTableCell88.Summary.FormatString = DungChung.Bien.FormatString[1];
                xrTableCell89.Summary.FormatString = DungChung.Bien.FormatString[1];

                xrTable5.Visible = false;
                xrTable_grf_24012_3672.Visible = true;
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
            if (DungChung.Bien.Check_CV3762 == true)
            {
                xrTableTong24012.Visible = true;
                xrTable7.Visible = false;
            }
        }
        int stt = 1;
  
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            switch (stt)
            {
                case 1:
                    colSTTGh1.Text = "A";
                    colCongGr.Text = xrTableCell60.Text = "Cộng A:";
                    break;
                case 2:
                    colSTTGh1.Text = "B";
                    colCongGr.Text = xrTableCell60.Text = "Cộng B:";
                    break;
                case 3:
                    colSTTGh1.Text = "C";
                    colCongGr.Text = xrTableCell60.Text = "Cộng C:";
                    break;
                case 4:
                    colSTTGh1.Text = "D";
                    colCongGr.Text = xrTableCell60.Text = "Cộng D:";
                    break;
                case 5:
                    colSTTGh1.Text = "E";
                    colCongGr.Text = xrTableCell60.Text = "Cộng E:";
                    break;
                case 6:
                    colSTTGh1.Text = "F";
                    colCongGr.Text = xrTableCell60.Text = "Cộng F:";
                    break;
                case 7:
                    colSTTGh1.Text = "G";
                    colCongGr.Text = xrTableCell60.Text = "Cộng G:";
                    break;
                case 8:
                    colSTTGh1.Text = "H";
                    colCongGr.Text = xrTableCell60.Text = "Cộng H:";
                    break;
                case 9:
                    colSTTGh1.Text = "I";
                    colCongGr.Text = xrTableCell60.Text = "Cộng I:";
                    break;

            }
            stt++;
        }

     
        private void rdFont_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("SapXep") != null)
            //{
            //    int stt = Convert.ToInt16(this.GetCurrentColumnValue("SapXep"));
            //    if (stt == 1)
            //    {
            //        colTenThuocGh2.Text = "1. Thuốc tân dược";
            //    }
            //    if (stt == 2)
            //    {
            //        colTenThuocGh2.Text = "2. Chế phẩm y học cổ truyền";
            //    }
            //    if (stt == 3)
            //    {
            //        colTenThuocGh2.Text = "3. Thuốc y học cổ truyền";
            //    }
            //}
            sttdt = 1;
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
        int sttdt = 1;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
                colSTT.Text = sttdt.ToString();
            sttdt++;
        }
    }
}
