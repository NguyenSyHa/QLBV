using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV.ChucNang;

namespace QLBV.BaoCao
{
    public partial class rep_TT102 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TT102()
        {
            InitializeComponent();
            LoadKCB();
        }
        private void LoadKCB()
        {
            lblMSKCB.Text = DungChung.Bien.MaBV;
            lblTenCSKCB.Text = DungChung.Bien.TenCQ;
        }

        public void Bingding()
        {
            databind(colTenNguoiBenh, "TenBNhan");
            databind(colNamSinh, "NSinh");
            databind(colGioiTinh, "GTinh");
            databind(colSThe, "SThe");
            databind(colMaBenh, "MaICD");
            databind(colNgayVao, "Ngaykham");
            databind(colNgayRa, "Ngayra");
            databind(ColSTTB, "STTa");
            databind(tableCell174, "SSTC");
            databind(colDoiTuong, "DoiTuongTheoND");
            databind(colHoatDong, "KCBNNT");
            databind(colLuot, "BenhNhanNNT");
            colSoNgayDieuTri.DataBindings.Add("Text", DataSource, "SoNgaydt");

            databindGroup(colTongCong, "Tongcong");
            databindGroup(colKhamBenh, "Congkham");
            databindGroup(colNgayGiuong, "TienGiuong");
            databindGroup(colXetNghiem, "Xetnghiem");
            databindGroup(colCDHA, "CDHA");
            databindGroup(colTTPT, "TTPT");
            databindGroup(colMau, "Mau");
            databindGroup(colThuoc, "Thuoc");
            databindGroup(colVTYT, "VTYT");
            databindGroup(colVanChuyenNguoiBenh, "CPVanchuyen");
            databindGroup(colTaiTinhThanhPho, "TTTP");
            databindGroup(colND70, "ND70");
            databindGroup(colCungChiTra, "CungChiTra");
            databindGroup(colTuTra, "TuTra");
            databindGroup(colChiPhiNgoai, "CPNBHYT");
            databindGroup(colTaiTrungUong, "TaiTrungUong");
            databindGroup(colNSDiaPhuong, "NSDiaPhuong");
            databindGroup(colHoTroTaiTro, "HoTroTaiTro");

            databindGroup(colTTongCong, "Tongcong");
            databindGroup(colTKhamBenh, "Congkham");
            databindGroup(colTNgayGiuong, "TienGiuong");
            databindGroup(colTXetNghiem, "Xetnghiem");
            databindGroup(colTCDHA, "CDHA");
            databindGroup(colTMau, "Mau");
            databindGroup(colTThuoc, "Thuoc");
            databindGroup(colTVTYT, "VTYT");
            databindGroup(colTCPVCNB, "CPVanchuyen");
            databindGroup(colTTTPT, "TTPT");
            databindGroup(colTND70, "ND70");
            databindGroup(colTCCT, "CungChiTra");
            databindGroup(colTCPNBH, "CPNBHYT");
            databindGroup(colTTTTP, "TTTP");
            databindGroup(colTTuTra, "TuTra");
            databindGroup(c, "TaiTrungUong");
            databindGroup(tableCell170, "NSDiaPhuong");
            databindGroup(tableCell171, "HoTroTaiTro");

            databindGroup(colTTongCong2, "Tongcong");
            databindGroup(colTKhamBenh2, "Congkham");
            databindGroup(colTNgayGiuong2, "TienGiuong");
            databindGroup(colTXetNghiem2, "Xetnghiem");
            databindGroup(colTCDHA2, "CDHA");
            databindGroup(colTTTPT2, "TTPT");
            databindGroup(colTMau2, "Mau");
            databindGroup(colTThuoc2, "Thuoc");
            databindGroup(colTVTYT2, "VTYT");
            databindGroup(colTCPVCNB2, "CPVanchuyen");
            databindGroup(colTTTTP2, "TTTP");
            databindGroup(colTND702, "ND70");
            databindGroup(colTCCT2, "CungChiTra");
            databindGroup(colTCPNBH2, "CPNBHYT");
            databindGroup(colTTuTra2, "TuTra");
            databindGroup(tableCell51, "TaiTrungUong");
            databindGroup(tableCell119, "NSDiaPhuong");
            databindGroup(tableCell120, "HoTroTaiTro");

            databindGroup(colTTongCong3, "Tongcong");
            databindGroup(colTKhamBenh3, "Congkham");
            databindGroup(colTNgayGiuong3, "TienGiuong");
            databindGroup(colTXetNghiem3, "Xetnghiem");
            databindGroup(colTCDHA3, "CDHA");
            databindGroup(colTTTPT3, "TTPT");
            databindGroup(colTMau3, "Mau");
            databindGroup(colTThuoc3, "Thuoc");
            databindGroup(colTVTY3, "VTYT");
            databindGroup(colTCPVCNB3, "CPVanchuyen");
            databindGroup(colTTTP3, "TTTP");
            databindGroup(colTND703, "ND70");
            databindGroup(colTCCT3, "CungChiTra");
            databindGroup(colTCPNBH3, "CPNBHYT");
            databindGroup(colTTuTra3, "TuTra");
            databindGroup(tableCell35, "TaiTrungUong");
            databindGroup(tableCell39, "NSDiaPhuong");
            databindGroup(tableCell40, "HoTroTaiTro");

            Bingdingcell();

            GroupHeader3.GroupFields.Add(new GroupField("NoiTru"));
            GroupHeader2.GroupFields.Add(new GroupField("STTa"));
            GroupHeader1.GroupFields.Add(new GroupField("SSTC"));
        }

        public void Bingdingcell()
        {
            databindGroup(coltongSongayDTthucTe, "SoNgaydt");
            databindGroup(colTongcongAB, "TongCong");
            databindGroup(colTongKhamBenhAB, "Congkham");
            databindGroup(colTongNgayGiuongAB, "TienGiuong");
            databindGroup(colXetNghiemAB, "XetNghiem");
            databindGroup(ColTongCDHAAB, "CDHA");
            databindGroup(colTongTTPT, "TTPT");
            databindGroup(colTongMauAB, "Mau");
            databindGroup(colTongThuocAB, "Thuoc");
            databindGroup(ColTongVTYTAB, "VTYT");
            databindGroup(colTongVCNBAB, "CPVanchuyen");
            databindGroup(colTongTTTP, "TTTP");
            databindGroup(colTongTND70, "ND70");
            databindGroup(colTongTCCT, "CungChiTra");
            databindGroup(colTongTuTra, "TuTra");
            databindGroup(colTongCPNBH, "CPNBHYT");
            databindGroup(tableCell214, "TaiTrungUong");
            databindGroup(tableCell218, "NSDiaPhuong");
            databindGroup(tableCell219, "HoTroTaiTro");

        }
        private void databind(XRTableCell cell, string databin, int type = 1)
        {
            cell.DataBindings.Add("Text", DataSource, databin).FormatString = DungChung.Bien.FormatString[type];
        }

        private void databindGroup(XRTableCell cell, string databin, int type = 1)
        {
            cell.DataBindings.Add("Text", DataSource, databin).FormatString = DungChung.Bien.FormatString[type];
            cell.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void databind(XRTableCell cell, object data, string databin)
        {
            cell.DataBindings.Add("Text", data, databin).FormatString = DungChung.Bien.FormatString[1];
        }
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            var row = (frm_DK_TT102.repcount)this.GetCurrentRow();
            if (row != null)
            {
                if (string.IsNullOrWhiteSpace(row.TenBNhan))
                    e.Cancel = true;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            var row = (frm_DK_TT102.repcount)this.GetCurrentRow();
            if (row != null)
            {
                if (string.IsNullOrWhiteSpace(row.TenBNhan))
                    e.Cancel = true;
            }
        }

        private void colTaiTinhThanhPho_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void colND70_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                TB24012_FOOTER.Visible = true;
                table2.Visible = false;
            }
        }
    }
}
