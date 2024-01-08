using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class rep_TomTatDieuTriNgTru : DevExpress.XtraReports.UI.XtraReport
    {
        int _mabn = 0;
        int _makp = 0;
        public rep_TomTatDieuTriNgTru(int mabn, int makp)
        {
            InitializeComponent();
            _mabn = mabn;
            _makp = makp;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DichVu> ldv = new List<DichVu>();
        List<DichVuct> ldvct = new List<DichVuct>();
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Text = TenCQCQ2.Text = DungChung.Bien.TenCQCQ;
            TenCQ.Text = TenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand3.Visible = false;
                SubBand4.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }

            var ktdthuoc = data.DThuocs.Where(p => p.PLDV == 1 && p.MaKP == _makp && p.MaBNhan == _mabn).ToList();
            if (ktdthuoc.Count() > 0)
                SubBand1.Visible = true;
            var ktcls = data.CLS.Where(p => p.MaBNhan == _mabn && p.MaKP == _makp && p.Status == 1).ToList();
            if (ktcls.Count() > 0)
                SubBand2.Visible = true;
            ldv = data.DichVus.ToList();
            var qMaChuQuan = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (qMaChuQuan != null)
                _maCQCQ = qMaChuQuan.MaChuQuan;
        }
        string _maCQCQ = "";
        private void _InSubReport_Dthuoc(XRSubreport repsub)
        {
            //private void _InSubReport_PhieuThuThuoc(XRSubreport repsub)
            var qd1 = (from dt in data.DThuocs.Where(p => p.MaBNhan == _mabn && p.MaKP == _makp).Where(p => p.PLDV == 1)
                       join dtct in data.DThuoccts.Where(p => (DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30005") ? true : (p.TrongBH != 2)) on dt.IDDon equals dtct.IDDon
                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                       join tn in data.TieuNhomDVs.Where(p => DungChung.Bien.MaBV == "12122" ? p.IDNhom == 4 : true) on dv.IdTieuNhom equals tn.IdTieuNhom
                       where tn.TenRG != "Thuốc gây nghiện" && tn.TenRG != "Thuốc hướng tâm thần"
                       select new { TenTNRG = tn.TenRG, dv.TenRG, TenDV = _maCQCQ == "00000" ? dv.TenHC + " " + dv.HamLuong : ((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") ? ((dv.TenDV ?? "") + " (" + (dv.TenHC ?? "") + ") " + (dv.HamLuong ?? "")) : ((dv.TenHC.Contains(",") || dv.TenHC.Contains("+") || dv.TenHC.Contains(";")) ? dv.TenDV : (dv.TenHC + " (" + dv.TenDV + ") " + dv.HamLuong))), dv.TenHC, dv.HamLuong, dv.MaDV, dv.DonVi, dtct.SoLuong, dtct.IDDonct, dtct.GhiChu, HuongDan = "", DuongD = dtct.DuongD ?? "", SoLan = dtct.SoLan ?? "", MoiLan = dtct.MoiLan ?? "", Luong = dtct.Luong ?? "", DviUong = dtct.DviUong ?? "", LoiDan = dt.GhiChu }).OrderBy(p => p.IDDonct).ToList();
            //(DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "08602" || (DungChung.Bien.MaBV == "12001" && dv.TenTNRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_CPYHCT)) ? (dv.TenHC + "(" + dv.TenDV + ")" + " " + dv.HamLuong) : ((DungChung.Bien.MaBV == "04011" || DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? dv.TenRG : (DungChung.Bien.MaTinh == "12" ? dv.TenDV + "( " + dv.HamLuong + " )" : dv.TenDV))
            var qd2 = (from dv in qd1.Where(p => DungChung.Bien.MaBV == "27022" ? p.TenTNRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThucPhamChucNang : true)
                       group dv by new { dv.TenDV, dv.MaDV, dv.DonVi, dv.HamLuong, dv.TenRG } into kq
                       select new { TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV, kq.Key.HamLuong, kq.Key.MaDV, kq.Key.DonVi, SoLuong = kq.Sum(p => p.SoLuong), HuongDan = kq.First().DuongD + " " + kq.First().SoLan + " " + kq.First().MoiLan + " " + kq.First().Luong + " " + kq.First().DviUong + " " + kq.First().GhiChu }).ToList();
            QLBV.BaoCao.rep_TomTatDTriNgTru_Sub_Dthuoc rep = new QLBV.BaoCao.rep_TomTatDTriNgTru_Sub_Dthuoc();
            repsub.ReportSource = rep;
            rep.DataSource = qd2;
            rep.BindData();
        }
        private void _InSubReport_CLS(XRSubreport repsub)
        {
            var q1 = (from cls in data.CLS.Where(p => p.MaKP == _makp && p.MaBNhan == _mabn && p.Status == 1)
                      join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                      join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                      join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                      join dv in data.DichVus on dvct.MaDV equals dv.MaDV
                      select new { dv.MaDV, dv.TenDV, dvct.MaDVct, dvct.TenDVct, clsct.KetQua, cd.KetLuan, dv.IDNhom }).ToList();
            var q2 = (from a in q1 select new { a.MaDV, a.TenDV, TenDVct = a.IDNhom == 1 ? a.TenDVct + ":" + a.KetQua : a.KetQua }).ToList();
            var q3 = (from a in q2
                      group a by new { a.MaDV, a.TenDV, } into kq
                      select new
                      {
                          kq.Key.MaDV,
                          kq.Key.TenDV,
                          KetQua = string.Join(";", kq.Select(p => p.TenDVct).ToArray())
                      }).ToList();
            QLBV.BaoCao.rep_TomTatDTriNgTru_Sub_CLS rep = new QLBV.BaoCao.rep_TomTatDTriNgTru_Sub_CLS();
            repsub.ReportSource = rep;
            rep.DataSource = q3;
            rep.BindData();
        }
        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            _InSubReport_Dthuoc(xrSubreport1);
        }

        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {
            _InSubReport_CLS(xrSubreport2);
        }

    }
}
