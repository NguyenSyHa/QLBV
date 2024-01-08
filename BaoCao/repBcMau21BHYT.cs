using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repBcMau21BHYT : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcMau21BHYT()
        {                                                                      
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDVKTGh1.DataBindings.Add("Text", DataSource, "TenNhomThuoc");
            //colTenDVKT01.DataBindings.Add("Text", DataSource, "TenDVKT01");
            colTenDVKT02.DataBindings.Add("Text", DataSource, "TenThuoc");
            //colSoLuong01.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong02.DataBindings.Add("Text", DataSource, "SoLuongNT").FormatString = DungChung.Bien.FormatString[0];
            //colSoLuong03.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
           // colDonGia01.DataBindings.Add("Text", DataSource, "DonGia");
            colDonGia02.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
          //  colDonGia03.DataBindings.Add("Text", DataSource, "DonGia");
            //colThanhTien01.DataBindings.Add("Text", DataSource, "ThanhTien");
            colThanhTien02.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienGp1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienRep.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomThuoc"));
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

              
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            TenCQ.Value = DungChung.Bien.TenCQ;
        //    NguoiLap.Value = DungChung.Bien.NguoiLapBieu;
        //    //GiamDoc.Value = DungChung.Bien.GiamDoc;
            txtTieuDe.Text = "THỐNG KÊ TỔNG HỢP DỊCH VỤ KỸ THUẬT SỬ DỤNG " + Quy.Value;
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
                    colSTT01.Text = "A";
                    colCongGr.Text = "Cộng A:";
                    //TongCong += "A";
                    break;
                case 2:
                    colSTT01.Text = "B";
                    colCongGr.Text = "Cộng B:";
                   // TongCong += "+B";
                    break;
                case 3:
                    colSTT01.Text = "C";
                    colCongGr.Text = "Cộng C:";
                    //TongCong += "+B";
                    break;
                case 4:
                    colSTT01.Text = "D";
                    colCongGr.Text = "Cộng D:";
                   // TongCong += "+B";
                    break;
                case 5:
                    colSTT01.Text = "E";
                    colCongGr.Text = "Cộng E:";
                   // TongCong += "+B";
                    break;
                case 6:
                    colSTT01.Text = "F";
                    colCongGr.Text = "Cộng F:";
                    // TongCong += "+B";
                    break;
                case 7:
                    colSTT01.Text = "G";
                    colCongGr.Text = "Cộng G:";
                    // TongCong += "+B";
                    break;
            }
            //TongCong += "";
           // colTongCong.Text = TongCong;
            stt++;
        }

        private void xrTable4_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
