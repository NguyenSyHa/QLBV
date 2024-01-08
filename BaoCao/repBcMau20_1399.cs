using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV.FormThamSo;

namespace QLBV.BaoCao
{
    public partial class repBcMau20_1399 : DevExpress.XtraReports.UI.XtraReport
    {
        frmTsBcMau19_20_21_1399 frm192021 = new frmTsBcMau19_20_21_1399();
        public repBcMau20_1399()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                xrTable1.Visible = false;
                SubBand1.Visible = true;
            }
            else
            {
                xrTable1.Visible = true;
                SubBand1.Visible = false;
                if (DungChung.Bien.MaBV == "30350")
                {
                    xrTableCell31.Text = xrTableCell38.Text = "Phụ trách kho dược";
                    xrTableCell39.Text = xrTableCell33.Text = "Bệnh xá trưởng";
                }
            }
        }
        bool inTieuNhom = false;
        public repBcMau20_1399(bool inTN)
        {
            InitializeComponent();
            inTieuNhom = inTN;
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                GroupFooter3.Visible = false;
                GroupFooter2.Visible = false;
                GroupFooter1.Visible = false;
                xrTable3.Visible = false;
                xrTable15.Visible = true;
                //xrLine1.Visible = false;
                //xrLine2.Visible = true;
                xrTable1.Visible = false;
                SubBand1.Visible = true;
            }
            else
            {
                xrTable1.Visible = true;
                SubBand1.Visible = false;
                if (DungChung.Bien.MaBV == "30350")
                {
                    xrTableCell31.Text = xrTableCell38.Text = "Phụ trách kho dược";
                    xrTableCell39.Text = xrTableCell33.Text = "Bệnh xá trưởng";
                }
            }
        }
        public void BindingData()
        {
           
          
            colTenHC.DataBindings.Add("Text", DataSource, "TenHC");
            if (DungChung.Bien.MaBV == "26007")
                colDuongDung.DataBindings.Add("Text", DataSource, "Duong_dung");
            else
                colDuongDung.DataBindings.Add("Text", DataSource, "Ma_DuongD");
            if (DungChung.Bien.MaBV != "20001" && DungChung.Bien.MaBV != "24012")
            {
                colSTT.DataBindings.Add("Text", DataSource, "SoTTqd");
                colSTTQD.DataBindings.Add("Text", DataSource, "MaQD");
            }
            else if (DungChung.Bien.MaBV == "24012")
            {
                colSTT.DataBindings.Add("Text", DataSource, "num");
                colSTTQD.DataBindings.Add("Text", DataSource, "num");
            }
            else
            {
                colSTT.DataBindings.Add("Text", DataSource, "num");
                colSTTQD.DataBindings.Add("Text", DataSource, "MaQD");
            }
            
            colSTTQD.DataBindings.Add("Text", DataSource, "MaQD");
            colTenThuoc.DataBindings.Add("Text", DataSource, "Ten_thuoc");
            colDonGia.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
            colHamLuong.DataBindings.Add("Text", DataSource, "Ham_luong");
            colSoDK.DataBindings.Add("Text", DataSource, "So_dang_ky");
            colDonVi.DataBindings.Add("Text", DataSource, "Don_vi_tinh");
            ColSoLuongNgoaiT.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];
            ColSoLuongNoiTru.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];
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
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "20001")
            {
                colTenThuocGh2.DataBindings.Add("Text", DataSource, "TenSapXep");
                GroupHeader2.GroupFields.Add(new GroupField("SapXep"));
            }
            else
            {
                colTenThuocGh2.DataBindings.Add("Text", DataSource, "Tennhom");
                colTenThuocGh3.DataBindings.Add("Text", DataSource, "Tennhom");
                GroupHeader2.GroupFields.Add(new GroupField("Tennhom"));
            }
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                //colSTT1.DataBindings.Add("Text", DataSource, "SoTTqd");
                colTenThuocGh3.DataBindings.Add("Text", DataSource, "Tennhom");
                colTenHC1.DataBindings.Add("Text", DataSource, "TenHC");
                colDuongDung1.DataBindings.Add("Text", DataSource, "Ma_DuongD");
                colSTTQD1.DataBindings.Add("Text", DataSource, "MaQD");
                colTenThuoc1.DataBindings.Add("Text", DataSource, "Ten_thuoc");
                colDonGia1.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[0];
                colGiaThanhToan1.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[0];
                colHamLuong1.DataBindings.Add("Text", DataSource, "Ham_luong");
                colSoDK1.DataBindings.Add("Text", DataSource, "So_dang_ky");
                colDonVi1.DataBindings.Add("Text", DataSource, "Don_vi_tinh");
                ColSoLuongNgoaiT1.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];
                ColSoLuongNoiTru1.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];
                colThanhTienGp3.DataBindings.Add("Text", DataSource, "Thanh_tien");
                colThanhTienGp3.Summary.FormatString = DungChung.Bien.FormatString[0];
                colThanhTienTong1.DataBindings.Add("Text", DataSource, "Thanh_tien");
                colThanhTienTong1.Summary.FormatString = DungChung.Bien.FormatString[0];
                colTyLeTT1.DataBindings.Add("Text", DataSource, "TyLeTT");
                colThanhTien12.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[0];
                colTienDeNghi1.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[0];
                colTienDeNghi1.Summary.FormatString = DungChung.Bien.FormatString[0];
                colTongDeNghiQuy.DataBindings.Add("Text", DataSource, "TienBH");
                colTongDeNghiQuy.Summary.FormatString = DungChung.Bien.FormatString[0];
                if (inTieuNhom)
                {
                    colTenThuocGh1.DataBindings.Add("Text", DataSource, "TenTN");
                    GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
                }
                GroupHeader1.GroupFields.Add(new GroupField("Stt"));
            }

            if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
            {
                SubBand1.Visible = true;
                SubBand6.Visible = false;
                SubBand2.Visible = false;
                SubBand3.Visible = true;
                SubBand4.Visible = false;
                SubBand5.Visible = true;
                xrTable7.Visible = false;
                xrTable14.Visible = true;
                //
                colSTT24012.DataBindings.Add("Text", DataSource, "num");
                colSTTQD24012.DataBindings.Add("Text", DataSource, "MaQD");
                colTenHC24012.DataBindings.Add("Text", DataSource, "TenHC");
                colTenThuoc24012.DataBindings.Add("Text", DataSource, "Ten_thuoc");
                colDuongDung24012.DataBindings.Add("Text", DataSource, "Ma_DuongD");
                colDangBaoChe24012.DataBindings.Add("Text", DataSource, "DangBC");
                colHamLuong24012.DataBindings.Add("Text", DataSource, "Ham_luong");
                colSoDK24012.DataBindings.Add("Text", DataSource, "So_dang_ky");
                colDonVi24012.DataBindings.Add("Text", DataSource, "Don_vi_tinh");
                colSoLuongNgoaiT24012.DataBindings.Add("Text", DataSource, "SoluongNgT");//.FormatString = DungChung.Bien.FormatString[0];
                //colSoLuongNgoaiTTong.DataBindings.Add("Text", DataSource, "SoLuongNgT");
                //colSoLuongNgoaiTTong.Summary.FormatString = DungChung.Bien.FormatString[0];
                colSoLuongNoiTru24012.DataBindings.Add("Text", DataSource, "SoluongNT");//.FormatString = DungChung.Bien.FormatString[0];
                                                                                        //colSoLuongNoiTruTong.DataBindings.Add("Text", DataSource, "SoluongNT");
                                                                                        // colSoLuongNoiTruTong.Summary.FormatString = DungChung.Bien.FormatString[0];
                                                                                        //
                colDonGia24012.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
                //colDonGiaTong.DataBindings.Add("Text", DataSource, "Don_Gia");
                //colDonGiaTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colGiaThanhToan24012.DataBindings.Add("Text", DataSource, "Don_giaBHYT").FormatString = DungChung.Bien.FormatString[1];
                //colGiaThanhToanTong.DataBindings.Add("Text", DataSource, "Don_giaBHYT");
                //colGiaThanhToanTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTyLeTT24012.DataBindings.Add("Text", DataSource, "TyLeTT");//.FormatString = DungChung.Bien.FormatString[0];
                //colTyLeTTTong.DataBindings.Add("Text", DataSource, "TyLeTT");
                //colTyLeTTTong.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTien24012.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
                colThanhTienTong1.DataBindings.Add("Text", DataSource, "Thanh_tien");
                colThanhTienTong1.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTienDeNghi24012.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT").FormatString = DungChung.Bien.FormatString[1];
                colTongDeNghiQuy.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT");
                colTongDeNghiQuy.Summary.FormatString = DungChung.Bien.FormatString[1];

                this.colTenThuocGh1.WidthF = 727.69F;
                this.colSTTGh1.WidthF = 30.35F;
                this.xrTable8.SizeF = new SizeF(758.04F, 19.79F);
                this.xrTable8.LocationF = new PointF(15.67F, 0F);

                xrTable5.Visible = false;
                xrTable_grf_24012_3672.Visible = true;
                xrTable_grf_24012_3672.SizeF = new SizeF(758.71F, 36.33F);
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTKD.Text = DungChung.Bien.TruongKhoaDuoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable10.Visible = true;
                xrTable4.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                xrTable7.Visible = false;
                xrTable14.Visible = true;
                xrTableCell28.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = "Tên CSYT: "+DungChung.Bien.TenCQ.ToUpper();
            colMaCQ.Text = "Mã CSYT: " + DungChung.Bien.MaBV;
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                colTenCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                colMaCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            }
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
                    colCongGr.Text = colCongGr24012.Text = "Cộng A:";
                   
                    break;
                case 2:
                    colSTTGh1.Text = "B";
                    colCongGr.Text = colCongGr24012.Text = "Cộng B:";                   
                    break;
                case 3:
                    colSTTGh1.Text = "C";
                    colCongGr.Text = colCongGr24012.Text = "Cộng C:";
                   
                    break;
                case 4:
                    colSTTGh1.Text = "D";
                    colCongGr.Text = colCongGr24012.Text = "Cộng D:";
                    break;
                case 5:
                    colSTTGh1.Text = "E";
                    colCongGr.Text = colCongGr24012.Text = "Cộng E:";
                    break;
                case 6:
                    colSTTGh1.Text = "F";
                    colCongGr.Text = colCongGr24012.Text = "Cộng F:";
                    break;
                case 7:
                    colSTTGh1.Text = "G";
                    colCongGr.Text = colCongGr24012.Text = "Cộng G:";
                    break;
                case 8:
                    colSTTGh1.Text = "H";
                    colCongGr.Text = colCongGr24012.Text = "Cộng H:";
                    break;
                case 9:
                    colSTTGh1.Text = "I";
                    colCongGr.Text = colCongGr24012.Text = "Cộng I:";
                    break;

            }
            stt++;
        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "27001" && DungChung.Bien.MaBV != "20001")
            GroupFooter2.Visible = false;


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

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                xrTable1.Visible = false;
                SubBand1.Visible = true;
            }
        }
        int a = 1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                xrTable3.Visible = false;
                xrTable15.Visible = true;
            }
            //if (this.GetCurrentColumnValue("SapXep") != null)
            //{
            //    string stt = this.GetCurrentColumnValue("SapXep").ToString();
            //    //if (stt == 1)
            //    //{                   
            //    //        colTenThuocGh2.Text = "1. Thuốc tân dược";
            //    //}
            //    //if (stt == 2)
            //    //{
            //    //    colTenThuocGh2.Text = "2. Chế phẩm y học cổ truyền";                   
            //    //}
            //    //if (stt == 3)
            //    //{
            //    //    colTenThuocGh2.Text = "3. Thuốc y học cổ truyền";
            //    //}
            //}
            sttdt = 1;
        }
        int sttdt = 1;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
                colSTT.Text = sttdt.ToString();
            colSTT1.Text = sttdt.ToString();
            sttdt++;
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                xrTable6.Visible = false;
                xrTable12.Visible = true;
            }
        }

       
    }
}
