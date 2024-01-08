using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcMau21BHYT_1399_SP : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcMau21BHYT_1399_SP()
        {                                                                      
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDVKTGh1.DataBindings.Add("Text", DataSource, "TenNhom");
            colTenDVKT02.DataBindings.Add("Text", DataSource, "TenDV");
            colSoLuong02.DataBindings.Add("Text", DataSource, "SoLuongNgTru").FormatString = DungChung.Bien.FormatString[0];
            colSoLuongNoiT.DataBindings.Add("Text", DataSource, "SoLuongNTru").FormatString = DungChung.Bien.FormatString[0];
            colDonGia02.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSTT02.DataBindings.Add("Text", DataSource, "SoTTqd");
            colMaQD.DataBindings.Add("Text", DataSource, "MaQD");
            colThanhTien02.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienRep.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhom"));
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
              
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
            txtTieuDe.Text = "THỐNG KÊ TỔNG HỢP DỊCH VỤ KỸ THUẬT THANH TOÁN "+this.DTuong.Value;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtKTT.Text = DungChung.Bien.KeToanTruong;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;

        }
        int stt = 1;
        string TongCong = "Tổng cộng (";
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
          
            switch (stt) {
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
        }

        private void colTongCong_BeforePrint(object sender, CancelEventArgs e)
        {
             TongCong += ")";
             colTongCong.Text = TongCong;
        }
    }
}
