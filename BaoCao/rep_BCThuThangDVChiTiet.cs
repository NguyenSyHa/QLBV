using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCThuThangDVChiTiet : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCThuThangDVChiTiet()
        {                                                                      
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDVKTGh1.DataBindings.Add("Text", DataSource, "TenDV");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];

            colThanhTien_R.DataBindings.Add("Text", DataSource, "ThanhTien");
            colThanhTien_R.Summary.FormatString = DungChung.Bien.FormatString[1];

            colThanhTien_G.DataBindings.Add("Text", DataSource, "ThanhTien");
            colThanhTien_G.Summary.FormatString = DungChung.Bien.FormatString[1];

            colSoLuong_G.DataBindings.Add("Text", DataSource, "SoLuong");
            colSoLuong_G.Summary.FormatString = DungChung.Bien.FormatString[1];

            colSoLuong_R.DataBindings.Add("Text", DataSource, "SoLuong");
            colSoLuong_R.Summary.FormatString = DungChung.Bien.FormatString[1];

            GroupHeader1.GroupFields.Add(new GroupField("TenDV"));
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);              
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = "Tên CSYT: " + DungChung.Bien.TenCQ.ToUpper();
            colMaCQ.Text = "Mã CSYT: " + DungChung.Bien.MaBV;
            TenCQ.Value = DungChung.Bien.TenCQ;
        //    NguoiLap.Value = DungChung.Bien.NguoiLapBieu;
        //    //GiamDoc.Value = DungChung.Bien.GiamDoc;
            //if (this.DTuong.Value.ToString() == "BHYT")
            //    this.NoiNgoaiTru.Value = "Đối với người bệnh BHYT đăng ký ban đầu/đa tuyến đến";
            //else
            //    this.NoiNgoaiTru.Value = "Đối với người bệnh thu phí";
            //txtTieuDe.Text = "THỐNG KÊ TỔNG HỢP DỊCH VỤ KỸ THUẬT THANH TOÁN "+this.DTuong.Value.ToString().ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtKTT.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;

        }
        //int stt = 1;
        //string TongCong = "Tổng cộng (";
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
          
            //switch (stt) {
            //    case 1:
            //        colSTT01.Text = "I";
            //        colCongGr.Text = "Cộng I:";
            //        TongCong += "I";
            //        break;
            //    case 2:
            //        colSTT01.Text = "II";
            //        colCongGr.Text = "Cộng II:";
            //        TongCong += "+II";
            //        break;
            //    case 3:
            //        colSTT01.Text = "III";
            //        colCongGr.Text = "Cộng III:";
            //        TongCong += "+II";
            //        break;
            //    case 4:
            //        colSTT01.Text = "IV";
            //        colCongGr.Text = "Cộng IV:";
            //        TongCong += "+IV";
            //        break;
            //    case 5:
            //        colSTT01.Text = "V";
            //        colCongGr.Text = "Cộng V:";
            //        TongCong += "+V";
            //        break;
            //    case 6:
            //        colSTT01.Text = "VI";
            //        colCongGr.Text = "Cộng VI:";
            //        TongCong += "+VI";
            //        break;
            //    case 7:
            //        colSTT01.Text = "VII";
            //        colCongGr.Text = "Cộng VII:";
            //        TongCong += "+VII";
            //        break;
            //    case 8:
            //        colSTT01.Text = "VIII";
            //        colCongGr.Text = "Cộng VIII:";
            //        TongCong += "+VIII";
            //        break;

            //}
            //stt++;
        }       
        private void colTongCong_BeforePrint(object sender, CancelEventArgs e)
        {
             //TongCong += ")";
             //colTongCong.Text = TongCong;
        }
    }
}
