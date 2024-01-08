using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using QLBV.FormThamSo;

namespace QLBV.BaoCao
{
    public partial class repBcMau21BHYT_1399 : DevExpress.XtraReports.UI.XtraReport
    {

        QLBVEntities data = new QLBVEntities(DungChung.Bien.StrCon);
        frmTsBcMau19_20_21_1399 frm192021 = new frmTsBcMau19_20_21_1399();
        public repBcMau21BHYT_1399()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
            {
                xrTable8.Visible = false;
                tableTong24012.Visible = true;
            }
            else
            {
                xrTable8.Visible = true;
            }
            
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                SubBand1.Visible = true;
            }

            if (DungChung.Bien.MaBV == "30350")
            {
                xrTableCell32.Text = xrTableCell107.Text = xrTableCell53.Text = "Phụ trách kho dược";
                xrTableCell33.Text = xrTableCell108.Text = xrTableCell59.Text = "Bệnh xá trưởng";
                SubBand3.Visible = false;
                SubBand13.Visible = false;
                SubBand11.Visible = true;
            }
        }
        public void BindingData()
        {

            //if (DungChung.Bien.MaTinh != "04" && DungChung.Bien.MaBV != "30002") anh quý 28/08/2017
            if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                SubBand4.Visible = false;
                SubBand5.Visible = true;
                SubBand6.Visible = false;
                SubBand7.Visible = true;
                SubBand8.Visible = false;
                SubBand9.Visible = true;
                SubBand10.Visible = true;
                SubBand12.Visible = false;
                //SubBand3.Visible = false;
                //SubBand11.Visible = true;
                xrTable8.Visible = false;
                xrTable13.Visible = false;
                tableTong24012.Visible = true;

                colTenDVKTGh11_24012.DataBindings.Add("Text", DataSource, "Tennhom");
                colTenDVKT02.DataBindings.Add("Text", DataSource, "Tenrg");
                colSTT24012.DataBindings.Add("Text", DataSource, "num");
                colMaQD24012.DataBindings.Add("Text", DataSource, "SoTTqd");
                colTenDVKT24012.DataBindings.Add("Text", DataSource, "Ten_thuoc");

                colSoLuong24012.DataBindings.Add("Text", DataSource, "SoluongNgT");
                //colSoLuongNgoaiTGp_24012.DataBindings.Add("Text", DataSource, "SoluongNgT");
                //colSoLuongNgoaiTGp_24012.Summary.FormatString = DungChung.Bien.FormatString[0];
                //colSoLuongNgoaiTTong_24012.DataBindings.Add("Text", DataSource, "SoluongNgT");
                //colSoLuongNgoaiTTong_24012.Summary.FormatString = DungChung.Bien.FormatString[0];

                colSoLuongNoiT24012.DataBindings.Add("Text", DataSource, "SoluongNT");
                //colSoLuongNoiTruGp_24012.DataBindings.Add("Text", DataSource, "SoluongNT");
                //colSoLuongNoiTruGp_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colSoLuongNoiTruTong_24012.DataBindings.Add("Text", DataSource, "SoluongNT");
                //colSoLuongNoiTruTong_24012.Summary.FormatString = DungChung.Bien.FormatString[1];

                colDonGia24012.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
               // colDonGiaGp_24012.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
                //colDonGiaGp_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colDonGiaTong_24012.DataBindings.Add("Text", DataSource, "Don_gia");
                //colDonGiaTong_24012.Summary.FormatString = DungChung.Bien.FormatString[1];

                colDonGiaQuy24012.DataBindings.Add("Text", DataSource, "Don_giaBHYT").FormatString = DungChung.Bien.FormatString[1];
               // colDonGiaBHYTGp_24012.DataBindings.Add("Text", DataSource, "Don_giaBHYT").FormatString = DungChung.Bien.FormatString[1];
                //colDonGiaBHYTGp_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colDonGiaBHTong_24012.DataBindings.Add("Text", DataSource, "Don_giaBHYT");
                //colDonGiaBHTong_24012.Summary.FormatString = DungChung.Bien.FormatString[1];

                colTyLeTT24012.DataBindings.Add("Text", DataSource, "TyLeTT");
                //colTyLeTTGp_24012.DataBindings.Add("Text", DataSource, "TyLeTT");
                //colTyLeTTGp_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colTyLeTTTong_24012.DataBindings.Add("Text", DataSource, "TyLeTT");
                //colTyLeTTTong_24012.Summary.FormatString = DungChung.Bien.FormatString[1];

                colThanhTien24012.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
                colThanhTienGp_24012.DataBindings.Add("Text", DataSource, "Thanh_tien");
                colThanhTienGp_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
                colThanhTienTong_24012.DataBindings.Add("Text", DataSource, "Thanh_tien");
                colThanhTienTong_24012.Summary.FormatString = DungChung.Bien.FormatString[1];

                colTienDeNghi24012.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT").FormatString = DungChung.Bien.FormatString[1];
                colTienKyQuyGp_24012.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT");
                colTienKyQuyGp_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
                colTongTienKeQuy_24012.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT");
                colTongTienKeQuy_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colThanhTienDeNghi.DataBindings.Add("Text", DataSource, "Thanh_tien");
                //colThanhTienRep_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
                //colTienDeNghiRep_24012.DataBindings.Add("Text", DataSource, "Thanh_tien_BHYT");
                //colTienDeNghiRep_24012.Summary.FormatString = DungChung.Bien.FormatString[1];
            }
            else
            {
                if (DungChung.Bien.MaBV == "26007")
                    colSTT02.DataBindings.Add("Text", DataSource, "SoTTqd");

                colTenDVKTGh1.DataBindings.Add("Text", DataSource, "Tennhom");

                colTenDVKT02.DataBindings.Add("Text", DataSource, "Ten_thuoc");
                if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
                {
                    colTenDVKTGh11.DataBindings.Add("Text", DataSource, "Tennhom");
                    colTenDVKT022.DataBindings.Add("Text", DataSource, "Ten_thuoc");
                    colSoLuong022.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];
                    colSoLuongNoiT2.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];
                    colDonGia022.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[0];
                    colDonGiaQuy.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[0];
                    colMaQD2.DataBindings.Add("Text", DataSource, "SoQD");
                    colThanhTien022.DataBindings.Add("Text", DataSource, "Thanh_tien");

                    colThanhTienGp11.DataBindings.Add("Text", DataSource, "Thanh_tien");
                    colThanhTienGp11.Summary.FormatString = DungChung.Bien.FormatString[1];

                    colThanhTienRep1.DataBindings.Add("Text", DataSource, "Thanh_tien");
                    colThanhTienRep1.Summary.FormatString = DungChung.Bien.FormatString[0];

                    colTienKyQuy1.DataBindings.Add("Text", DataSource, "Don_giaBHYT");
                    colTienKyQuy1.Summary.FormatString = DungChung.Bien.FormatString[0];

                    colTienDeNghi.DataBindings.Add("Text", DataSource, "Don_giaBHYT");
                    colTienDeNghi.Summary.FormatString = DungChung.Bien.FormatString[0];
                    //colTienDeNghi1.DataBindings.Add("Text", DataSource, "TienBH");
                    //colTienDeNghi1.Summary.FormatString = DungChung.Bien.FormatString[0];
                    //colThanhTien0111.DataBindings.Add("Text", DataSource, "Thanh_tien");
                    //colThanhTien0111.Summary.FormatString = DungChung.Bien.FormatString[0];
                    colThanhTienDeNghi.DataBindings.Add("Text", DataSource, "TienBH");
                    colThanhTienDeNghi.Summary.FormatString = DungChung.Bien.FormatString[0];
                    colTyLeTT.DataBindings.Add("Text", DataSource, "TyLeTT");
                }
                else
                {

                    colSoLuong02.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];
                    colSoLuongNoiT.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];
                    colDonGia02.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
                    if (DungChung.Bien.MaBV == "08602" || DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24012")
                        colMaQD.DataBindings.Add("Text", DataSource, "MaQD");
                    else
                        colMaQD.DataBindings.Add("Text", DataSource, "SoQD");
                    colThanhTien02.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
                    colThanhTienGp1.DataBindings.Add("Text", DataSource, "Thanh_tien");
                    colThanhTienGp1.Summary.FormatString = DungChung.Bien.FormatString[1];
                    colThanhTienRep.DataBindings.Add("Text", DataSource, "Thanh_tien");
                    colThanhTienRep.Summary.FormatString = DungChung.Bien.FormatString[1];
                }
            }
            GroupHeader1.GroupFields.Add(new GroupField("Stt"));
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = "Tên CSYT: " + DungChung.Bien.TenCQ.ToUpper();
            colMaCQ.Text = "Mã CSYT: " + DungChung.Bien.MaBV;
            TenCQ.Value = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                colTenCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                colMaCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                xrLabel9.Visible = false;
            }
            //    NguoiLap.Value = DungChung.Bien.NguoiLapBieu;
            //    //GiamDoc.Value = DungChung.Bien.GiamDoc;
            if (this.DTuong.Value.ToString() == "BHYT")
                this.NoiNgoaiTru.Value = "Đối với người bệnh BHYT đăng ký ban đầu/đa tuyến đến";
            else
                this.NoiNgoaiTru.Value = "Đối với người bệnh thu phí";
            txtTieuDe.Text = "THỐNG KÊ TỔNG HỢP DỊCH VỤ KỸ THUẬT THANH TOÁN " + this.DTuong.Value.ToString().ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                xrTableCell2.Text = "Ký, họ tên, đóng dấu";
                xrTableCell22.Text = "Kế toán";
                xrTable8.Visible = false;
                xrTable13.Visible = true;
                tableTong24012.Visible = false;
                xrTableCell16.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            }
            txtKTT.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            if (DungChung.Bien.MaBV == "04006")
                xrTableCell22.Text = "Trưởng phòng kế hoạch";
            if (DungChung.Bien.MaBV == "24012")
            {
                //xrTable3.Visible = false;
                xrTable5.Visible = true;
            }

            if (DungChung.Bien.MaBV == "30350")
            {
                SubBand3.Visible = false;
                SubBand13.Visible = false;
                SubBand11.Visible = true;
            }
        }
        int stt = 1;
        string TongCong = "Tổng cộng (";
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                xrTable6.Visible = false;
                xrTable9.Visible = true;
            }
            SoTT = 1;
            switch (stt)
            {
                case 1:
                    colSTT01.Text = "I";
                    colCongGr.Text = "Cộng I:";
                    colCongGr1.Text = "Tổng tiền: ";
                    TongCong += "I";
                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                    {
                        colSTT011_24012.Text = "I";
                        colCongGr_24012.Text = "Cộng I:";
                        colCongGr1.Text = "Tổng tiền: ";
                    }
                    break;
                case 2:
                    colSTT01.Text = "II";
                    colCongGr.Text = "Cộng II:";
                    colCongGr1.Text = "Tổng tiền: ";
                    TongCong += "+II";
                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                    {
                        colSTT011_24012.Text = "II";
                        colCongGr_24012.Text = "Cộng II:";
                        colCongGr1.Text = "Tổng tiền: ";
                    }
                    break;
                case 3:
                    colSTT01.Text = "III";
                    colCongGr.Text = "Cộng III:";
                    colCongGr1.Text = "Tổng tiền: ";
                    TongCong += "+II";
                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                    {
                        colSTT011_24012.Text = "III";
                        colCongGr_24012.Text = "Cộng III:";
                        colCongGr1.Text = "Tổng tiền: ";
                    }
                    break;
                case 4:
                    colSTT01.Text = "IV";
                    colCongGr.Text = "Cộng IV:";
                    colCongGr1.Text = "Tổng tiền: ";
                    TongCong += "+IV";
                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                    {
                        colSTT011_24012.Text = "IV";
                        colCongGr_24012.Text = "Cộng IV:";
                        colCongGr1.Text = "Tổng tiền: ";
                    }
                    break;
                case 5:
                    colSTT01.Text = "V";
                    colCongGr.Text = "Cộng V:";
                    colCongGr1.Text = "Tổng tiền: ";
                    TongCong += "+V";
                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                    {
                        colSTT011_24012.Text = "V";
                        colCongGr_24012.Text = "Cộng V:";
                        colCongGr1.Text = "Tổng tiền: ";
                    }
                    break;
                case 6:
                    colSTT01.Text = "VI";
                    colCongGr.Text = "Cộng VI:";
                    colCongGr1.Text = "Tổng tiền: ";
                    TongCong += "+VI";
                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                    {
                        colSTT011_24012.Text = "VI";
                        colCongGr_24012.Text = "Cộng VI:";
                        colCongGr1.Text = "Tổng tiền: ";
                        TongCong += "+VI";
                    }
                    break;
                case 7:
                    colSTT01.Text = "VII";
                    colCongGr.Text = "Cộng VII:";
                    colCongGr1.Text = "Tổng tiền: ";
                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                    {
                        colSTT011_24012.Text = "VII";
                        colCongGr_24012.Text = "Cộng VII:";
                        colCongGr1.Text = "Tổng tiền: ";
                    }
                    break;
                case 8:
                    colSTT01.Text = "VIII";
                    colCongGr.Text = "Cộng VIII:";
                    colCongGr1.Text = "Tổng tiền: ";
                    TongCong += "+VIII";
                    if (DungChung.Bien.MaBV == "24012" && DungChung.Bien.Check_CV3762 == true)
                    {
                        colSTT011_24012.Text = "VIII";
                        colCongGr_24012.Text = "Cộng VIII:";
                        colCongGr1.Text = "Tổng tiền: ";
                    }
                    break;

            }
            stt++;
            sttBL = 0;

        }
        private void colTongCong_BeforePrint(object sender, CancelEventArgs e)
        {
            TongCong += ")";
            colTongCong.Text = TongCong;
            
        }
        int stt1 = 0;
        int SoTT = 1;
        int sttBL = 0;//số thứ tự lần lượt trong từng nhóm (bệnh viện 04012 _Bảo Lâm)
        private void colSTT02_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaTinh == "04")
            {
                stt1++;
                colSTT02.Text = stt1.ToString();
            }
            if (DungChung.Bien.MaBV == "04012")
            {
                sttBL++;
                colSTT02.Text = sttBL.ToString();
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30002") //quynv 28/08/17 các Bv đánh số tt tăng dần từ 1
            //{
            colSTT022.Text = SoTT.ToString();
            colSTT02.Text = SoTT.ToString();
            SoTT++;
            //}
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                xrTable1.Visible = false;
                xrTable10.Visible = true;
            }
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                //GroupFooter1.Visible = false;
                xrTable7.Visible = false;
                xrTable12.Visible = true;
            }
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194" && DungChung.Bien.CheckbtnVsip == true)
            {
                xrTable2.Visible = false;
                SubBand1.Visible = true;
                xrTable11.Visible = true;
            }
        }

        private void tableTong24012_BeforePrint(object sender, CancelEventArgs e)
        {
            TongCong += ")";
            colTong24012.Text = TongCong;
        }
    }
}
