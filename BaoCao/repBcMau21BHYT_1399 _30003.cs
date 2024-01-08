using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcMau21BHYT_1399_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcMau21BHYT_1399_30003()
        {
            InitializeComponent();
        }
        public void BindingData()
        {

            //if (DungChung.Bien.MaTinh != "04" && DungChung.Bien.MaBV != "30002") anh quý 28/08/2017
            if (DungChung.Bien.MaBV == "26007")
                colSTT02.DataBindings.Add("Text", DataSource, "SoTTqd");

            colTenDVKTGh1.DataBindings.Add("Text", DataSource, "Tennhom");
            colTenDVKT02.DataBindings.Add("Text", DataSource, "Ten_thuoc");
            colSoLuong02.DataBindings.Add("Text", DataSource, "SoluongNgT").FormatString = DungChung.Bien.FormatString[0];
            colSoLuongNoiT.DataBindings.Add("Text", DataSource, "SoluongNT").FormatString = DungChung.Bien.FormatString[0];
            colDonGia02.DataBindings.Add("Text", DataSource, "Don_gia").FormatString = DungChung.Bien.FormatString[1];
            colDonGia39_2.DataBindings.Add("Text", DataSource, "Don_gia_tt39").FormatString = DungChung.Bien.FormatString[1];
            if (DungChung.Bien.MaBV == "08602" || DungChung.Bien.MaBV == "04011")
                colMaQD.DataBindings.Add("Text", DataSource, "MaQD");
            else
                colMaQD.DataBindings.Add("Text", DataSource, "SoQD");
            colThanhTien02.DataBindings.Add("Text", DataSource, "Thanh_tien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp1.DataBindings.Add("Text", DataSource, "Thanh_tien");
            colThanhTienGp1.Summary.FormatString = DungChung.Bien.FormatString[1];
            colThanhTienRep.DataBindings.Add("Text", DataSource, "Thanh_tien");
            colThanhTienRep.Summary.FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("Stt"));
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = "Tên CSYT: " + DungChung.Bien.TenCQ.ToUpper();
            colMaCQ.Text = "Mã CSYT: " + DungChung.Bien.MaBV;
            TenCQ.Value = DungChung.Bien.TenCQ;
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
            txtKTT.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            if (DungChung.Bien.MaBV == "04006")
                xrTableCell22.Text = "Trưởng phòng kế hoạch";
        }
        int stt = 1;
        string TongCong = "Tổng cộng (";
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            SoTT = 1;
            switch (stt)
            {
                case 1:
                    colSTT01.Text = "I";
                    colCongGr.Text = "Cộng I:";
                    TongCong += "I";
                    break;
                case 2:
                    colSTT01.Text = "II";
                    colCongGr.Text = "Cộng II:";
                    TongCong += "+II";
                    break;
                case 3:
                    colSTT01.Text = "III";
                    colCongGr.Text = "Cộng III:";
                    TongCong += "+II";
                    break;
                case 4:
                    colSTT01.Text = "IV";
                    colCongGr.Text = "Cộng IV:";
                    TongCong += "+IV";
                    break;
                case 5:
                    colSTT01.Text = "V";
                    colCongGr.Text = "Cộng V:";
                    TongCong += "+V";
                    break;
                case 6:
                    colSTT01.Text = "VI";
                    colCongGr.Text = "Cộng VI:";
                    TongCong += "+VI";
                    break;
                case 7:
                    colSTT01.Text = "VII";
                    colCongGr.Text = "Cộng VII:";
                    TongCong += "+VII";
                    break;
                case 8:
                    colSTT01.Text = "VIII";
                    colCongGr.Text = "Cộng VIII:";
                    TongCong += "+VIII";
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
                colSTT02.Text = SoTT.ToString();
                SoTT++;
            //}
        }

    }
}
