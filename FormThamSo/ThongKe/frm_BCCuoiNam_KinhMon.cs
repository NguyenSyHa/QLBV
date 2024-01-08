using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_BCCuoiNam_KinhMon : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCCuoiNam_KinhMon()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KPhong> _kp = new List<KPhong>();
        private void frm_BCCuoiNam_KinhMon_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime NgayTu = DungChung.Ham.NgayTu( lupTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            var dv = (from a in _data.DichVus.Where(p => p.PLoai == 1) select a).ToList();
            var nd = (from a in _data.NhapDs
                      join b in _data.NhapDcts on a.IDNhap equals b.IDNhap
                      select new {b.IDNhapct, b.MaDV, b.SoLuongN, b.SoLuongX, a.MaKP, a.MaKPnx, a.NgayNhap, a.PLoai, a.KieuDon}).ToList();
            var ncc = _data.NhaCCs.ToList();
            var ds = (from a in dv
                      join b in nd on a.MaDV equals b.MaDV
                      join c in ncc on a.MaCC equals c.MaCC
                      join d in _data.KPhongs on b.MaKP equals d.MaKP
                      group new { a, b, d, c } by new { a.MaDV, a.TenDV, a.HamLuong, a.TenHC, a.SoDK, a.MaATC, c.TenCC, a.MaTam, a.DonGia, a.DonVi, a.DuongD } into kq
                      select new {
                          kq.Key.MaDV,
                          kq.Key.TenHC,
                          kq.Key.MaATC,
                          MaNB = kq.Key.MaTam,
                          TenThuoc = kq.Key.TenDV,
                          kq.Key.SoDK,
                          NoiSX = kq.Key.TenCC,
                          ND_HL = kq.Key.HamLuong,
                          DVT = kq.Key.DonVi,
                          DuongDung = kq.Key.DuongD,
                          kq.Key.DonGia,
                          TonDK = kq.Where(p => p.b.NgayNhap < NgayTu ).Sum(p => p.b.SoLuongN) - kq.Where(p => p.b.NgayNhap < NgayTu ).Sum(p => p.b.SoLuongX),
                          TonCK = kq.Where(p => p.b.NgayNhap <= DenNgay ).Sum(p => p.b.SoLuongN) - kq.Where(p => p.b.NgayNhap <= DenNgay ).Sum(p => p.b.SoLuongX),
                          Nhap = kq.Where(p => p.b.NgayNhap >= NgayTu && p.b.NgayNhap <= DenNgay && p.d.TenKP.ToLower() == "kho tổng" && p.b.PLoai == 1 && p.b.KieuDon != 2 ).Sum(p => p.b.SoLuongN),
                          NhapKhac = kq.Where(p => p.b.NgayNhap >= NgayTu && p.b.NgayNhap <= DenNgay && p.b.PLoai == 1 && p.b.KieuDon == 2 && p.d.TenKP.ToLower() == "kho nội trú").Sum(p => p.b.SoLuongN),
                          Xuat = kq.Where(p => p.b.NgayNhap >= NgayTu && p.b.NgayNhap <= DenNgay && (p.d.TenKP.ToLower() == "kho nội trú" || p.d.TenKP.ToLower() == "Kho Ngoại trú" || p.d.TenKP.ToLower() == "Kho Xã") && p.b.PLoai == 2 && (p.b.KieuDon == 0 || p.b.KieuDon == 1 || p.b.KieuDon == 0)).Sum(p => p.b.SoLuongX),
                          XuatKhac = kq.Where(p => p.b.NgayNhap >= NgayTu && p.b.NgayNhap <= DenNgay && p.b.PLoai == 2 && p.b.KieuDon == 8 ).Sum(p => p.b.SoLuongX),
                      }).ToList();
            BaoCao.rep_BCCuoiNam_KinhMon rep = new BaoCao.rep_BCCuoiNam_KinhMon();
            rep.ngaythang.Text = "Từ ngày " + NgayTu.Day + "/ " + NgayTu.Month + "/ " + NgayTu.Year + " đến ngày " + DenNgay.Day + "/ " + DenNgay.Month + "/ " + DenNgay.Year;
            rep.DataSource = ds.ToList();
            rep.BindingData();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}