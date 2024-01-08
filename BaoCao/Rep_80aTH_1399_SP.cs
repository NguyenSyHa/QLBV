using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_80aTH_1399_SP : DevExpress.XtraReports.UI.XtraReport
    {
        List<DTuong> _ldtuong = new List<DTuong>();
        public Rep_80aTH_1399_SP()
        {
            InitializeComponent();
            QLBV_Database.QLBVEntities _data=new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            _ldtuong = _data.DTuongs.ToList();
        }
        public void BindingData()
        {
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF1.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHAGF2.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colCDHARF.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colTienGiuong.DataBindings.Add("Text", DataSource, "TienNgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colTienGiuongGF1.DataBindings.Add("Text", DataSource, "TienNgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colTienGiuongGF2.DataBindings.Add("Text", DataSource, "TienNgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colTienGiuongRF.DataBindings.Add("Text", DataSource, "TienNgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colSongay.DataBindings.Add("Text", DataSource, "SoNgaydt").FormatString = DungChung.Bien.FormatString[1];
            colSongayGF1.DataBindings.Add("Text", DataSource, "SoNgaydt").FormatString = DungChung.Bien.FormatString[1];
            colSongayGF2.DataBindings.Add("Text", DataSource, "SoNgaydt").FormatString = DungChung.Bien.FormatString[1];
            colSongayRF.DataBindings.Add("Text", DataSource, "SoNgaydt").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYT.DataBindings.Add("Text", DataSource, "CPVanChuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTGF1.DataBindings.Add("Text", DataSource, "CPVanChuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTGF2.DataBindings.Add("Text", DataSource, "CPVanChuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHYTRF.DataBindings.Add("Text", DataSource, "CPVanChuyen").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl_GF2.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl_GF1.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            colDVKT_tl_RF.DataBindings.Add("Text", DataSource, "DVKT_tl").FormatString = DungChung.Bien.FormatString[1];
            txtGioitinh.DataBindings.Add("Text", DataSource, "GTinh");
            colHotenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colMau.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF1.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauGF2.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            colMauRF.DataBindings.Add("Text", DataSource, "Mau").FormatString = DungChung.Bien.FormatString[1];
            txtNamsinh.DataBindings.Add("Text", DataSource, "NamSinh");
            colNguoibenhchitra.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraRF.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF1.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            colNguoibenhchitraGF2.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            //colTennhomGF2.DataBindings.Add("Text", DataSource, "Tennhom");
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF1.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocGF2.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuocRF.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl_GF1.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl_GF2.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl_RF.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colVTYT_tl.DataBindings.Add("Text", DataSource, "VTYT_tl").FormatString = DungChung.Bien.FormatString[1];
            colCPVC.DataBindings.Add("Text", DataSource, "CPVanChuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVCGF1.DataBindings.Add("Text", DataSource, "CPVanChuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVCGF2.DataBindings.Add("Text", DataSource, "CPVanChuyen").FormatString = DungChung.Bien.FormatString[1];
            colCPVCRF.DataBindings.Add("Text", DataSource, "CPVanChuyen").FormatString = DungChung.Bien.FormatString[1];
            colTongcong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYT.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF1.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTGF2.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongBHYTRF.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongGF2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTongcongRF2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colTTPT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF1.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTGF2.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTTPTRF.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colTuyen.DataBindings.Add("Text", DataSource, "Tuyen").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_GF1.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_GF2.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            colThuoc_tl_RF.DataBindings.Add("Text", DataSource, "Thuoc_tl").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham1.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham2.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham3.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            //colcongkham4.DataBindings.Add("Text", DataSource, "Vanchuyen").FormatString = DungChung.Bien.FormatString[1];
            colVTYT.DataBindings.Add("Text", DataSource, "VTYTTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF1.DataBindings.Add("Text", DataSource, "VTYTTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTGF2.DataBindings.Add("Text", DataSource, "VTYTTH").FormatString = DungChung.Bien.FormatString[1];
            colVTYTRF.DataBindings.Add("Text", DataSource, "VTYTTH").FormatString = DungChung.Bien.FormatString[1];
            colxetnghiem.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            ColXetnghiemGF1.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemGF2.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetnghiemRF.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            txtNoiTinh.DataBindings.Add("Text", DataSource, "NoiTinh").FormatString = DungChung.Bien.FormatString[1];
            txtMabn.DataBindings.Add("Text", DataSource, "MaBNhan");
            colSoLuot.DataBindings.Add("Text", DataSource, "MaBNhan");
            colSoLuotGF1.DataBindings.Add("Text", DataSource, "MaBNhan");
            colSoLuotGF2.DataBindings.Add("Text", DataSource, "MaBNhan");
            colSoLuotRF.DataBindings.Add("Text", DataSource, "MaBNhan");
            GroupHeader2.GroupFields.Add(new GroupField("NoiTinh"));
            GroupHeader1.GroupFields.Add(new GroupField("Tuyen"));
        }
        string tongcong = " Tổng cộng ";
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("NoiTinh") != null)
            {
                string noitinh = this.GetCurrentColumnValue("NoiTinh").ToString();
                switch (noitinh)
                {
                    case "1":
                        colNhomBNG1.Text = " Bệnh nhân nội tỉnh kcb ban đầu".ToUpper();
                        colSTTG1.Text = "A";
                        tongcong += "A";
                        colTennhomGF2.Text = " Cộng: A";
                        break;
                    case "2":
                        colNhomBNG1.Text = " Bệnh nhân nội tỉnh đến".ToUpper();
                        colSTTG1.Text = "B";
                        tongcong += "+B";
                        colTennhomGF2.Text = " Cộng: B";
                        break;
                    case "3":
                        colNhomBNG1.Text = " Bệnh nhân ngoại tỉnh đến".ToUpper();
                        colSTTG1.Text = "C";
                        tongcong += "+C";
                        colTennhomGF2.Text = " Cộng: C";
                        break;
                }
            }
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int sttg2 = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                if (sttg2 == 2)
                {
                    colTTGF1.Text = "II";
                }
                else
                {
                    if (sttg2 == 1)
                    {
                        colTTGF1.Text = "I";
                    }
                }
            }
            if (this.GetCurrentColumnValue("Tuyen") != null)
            {
                int tuyen = int.Parse(this.GetCurrentColumnValue("Tuyen").ToString().Trim());
                if (tuyen == 2)
                {
                    colTuyenGrp2.Text = " Trái tuyến";
                    colTennhomGF1.Text = " Trái tuyến";
                }
                if (tuyen == 1)
                {
                    colTuyenGrp2.Text = " Đúng tuyến";
                    colTennhomGF1.Text = " Đúng tuyến";
                }
            }
        }


        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtMaCS.Text = DungChung.Bien.MaBV;
            if (_dt != "BHYT")
            {
                GroupFooter1.Visible = false;
                GroupFooter2.Visible = false;
                GroupHeader1.Visible = false;
                GroupHeader2.Visible = false;
                txtTieuDe.Text = "DANH SÁCH NGƯỜI BỆNH NHÂN DÂN KHÁM CHỮA BỆNH NGOẠI TRÚ";
            }
        }
        string _dt = "BHYT";
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTongcong.Text = tongcong;
        }


    }
}
