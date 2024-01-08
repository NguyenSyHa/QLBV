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
    public partial class frm_PhieuCongKhai_01071 : DevExpress.XtraEditors.XtraForm
    {
        int mabn = 0;
        int bnkb = 0;
        public frm_PhieuCongKhai_01071()
        {
            InitializeComponent();
        }
        public frm_PhieuCongKhai_01071(int _mabn, int _bnkb)
        {
            mabn = _mabn;
            bnkb = _bnkb;
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ButHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private class ds
        {
            private string sTT;

            public string STT
            {
                get { return sTT; }
                set { sTT = value; }
            }
            string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            string donVi;

            public string DonVi
            {
                get { return donVi; }
                set { donVi = value; }
            }
            int sl1;

            public int Sl1
            {
                get { return sl1; }
                set { sl1 = value; }
            }
            int sl2;

            public int Sl2
            {
                get { return sl2; }
                set { sl2 = value; }
            }
            int sl3;

            public int Sl3
            {
                get { return sl3; }
                set { sl3 = value; }
            }
            int sl4;

            public int Sl4
            {
                get { return sl4; }
                set { sl4 = value; }
            }int sl5;

            public int Sl5
            {
                get { return sl5; }
                set { sl5 = value; }
            }
            int sl6;

            public int Sl6
            {
                get { return sl6; }
                set { sl6 = value; }
            }
            int sl7;

            public int Sl7
            {
                get { return sl7; }
                set { sl7 = value; }
            }
            int sl8;

            public int Sl8
            {
                get { return sl8; }
                set { sl8 = value; }
            }
            int sl9;

            public int Sl9
            {
                get { return sl9; }
                set { sl9 = value; }
            }int sl10;

            public int Sl10
            {
                get { return sl10; }
                set { sl10 = value; }
            }
            int sl11;

            public int Sl11
            {
                get { return sl11; }
                set { sl11 = value; }
            }
            int sl12;

            public int Sl12
            {
                get { return sl12; }
                set { sl12 = value; }
            }
            int sl13;

            public int Sl13
            {
                get { return sl13; }
                set { sl13 = value; }
            }
            int sl14;

            public int Sl14
            {
                get { return sl14; }
                set { sl14 = value; }
            }int sl15;

            public int Sl15
            {
                get { return sl15; }
                set { sl15 = value; }
            }
            int sl16;

            public int Sl16
            {
                get { return sl16; }
                set { sl16 = value; }
            }
            int sl17;

            public int Sl17
            {
                get { return sl17; }
                set { sl17 = value; }
            }
            int sl18;

            public int Sl18
            {
                get { return sl18; }
                set { sl18 = value; }
            }
            int sl19;

            public int Sl19
            {
                get { return sl19; }
                set { sl19 = value; }
            }int sl20;

            public int Sl20
            {
                get { return sl20; }
                set { sl20 = value; }
            }
            string ghiChu ;

            public string GhiChu
            {
              get { return ghiChu; }
              set { ghiChu = value; }
            }
        }
        private void butTaoBC_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            double ngay = (denngay - ngaytu).TotalDays;
            if (ngay <= 20)
            {
                hamin(ngaytu, denngay);
            }
            else if (ngay <= 40 && ngay > 20)
            {
                hamin(ngaytu, ngaytu.AddDays(20));
                hamin(ngaytu.AddDays(20), denngay);
            }
            else
            {
                MessageBox.Show("Thời gian quá dài!");
            }
        }

        private void frm_PhieuCongKhai_01071_Load(object sender, EventArgs e)
        {
            LupNgaytu.DateTime = DateTime.Now.AddDays(-19);
            LupNgayden.DateTime = DateTime.Now;
        }
        private void hamin(DateTime ngaytu, DateTime denngay)
        {
            List<ds> _ds = new List<ds>();
            var dv = (from a in _db.DichVus
                      join b in _db.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                      join c in _db.NhomDVs on a.IDNhom equals c.IDNhom
                      select new { a.MaDV, a.TenDV, a.DonVi, a.IDNhom, a.HamLuong }).ToList();
            var bn_dv = (from a in _db.DThuocs.Where(P => P.MaBNhan == mabn)
                         join c in _db.DThuoccts.Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= denngay) on a.IDDon equals c.IDDon
                         
                         select new { a.MaBNhan, a.NgayKe, c.NgayNhap, c.MaDV }).ToList();
            var ds1 = (from a in bn_dv
                       join d in dv on a.MaDV equals d.MaDV
                       group a by new { a.MaDV, NgayNhap = a.NgayNhap.Value.Date, d.TenDV, d.IDNhom, d.HamLuong, d.DonVi } into kq
                       select new
                       {
                           kq.Key.MaDV,
                           kq.Key.TenDV,
                           kq.Key.IDNhom,
                           kq.Key.HamLuong,
                           kq.Key.DonVi,
                           NgayNhap = kq.Key.NgayNhap,
                           tong = kq.Count()
                       }).OrderBy(p => p.IDNhom).ToList();

            var ds2 = (from a in ds1
                       join nhom in _db.NhomDVs on a.IDNhom equals nhom.IDNhom
                       group a by new { a.MaDV, a.TenDV, a.IDNhom, a.HamLuong, a.DonVi, nhom.TenNhom } into kq
                       select new ds
                       {
                           STT = (kq.Key.IDNhom == 4 || kq.Key.IDNhom == 5 || kq.Key.IDNhom == 6) ? "Thuốc, dịch truyền (tên, nồng độ/hàm lượng)" : ((kq.Key.IDNhom == 8 || kq.Key.IDNhom == 9) ? "Dịch vụ kỹ thuật" : ((kq.Key.IDNhom == 10 || kq.Key.IDNhom == 11) ? "Vật tư y tế (không có trong dịch vụ kỹ thuật)" : kq.Key.TenNhom)),
                           TenDV = (kq.Key.IDNhom == 4 || kq.Key.IDNhom == 5 || kq.Key.IDNhom == 6) ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : kq.Key.TenDV,
                           DonVi = kq.Key.DonVi,
                           Sl1 = kq.Where(p => p.NgayNhap == ngaytu.Date).Select(p => p.tong).FirstOrDefault(),
                           Sl2 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(1).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl3 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(2).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl4 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(3).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl5 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(4).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl6 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(5).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl7 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(6).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl8 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(7).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl9 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(8).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl10 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(9).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl11 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(10).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl12 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(11).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl13 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(12).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl14 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(13).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl15 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(14).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl16 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(15).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl17 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(16).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl18 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(17).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl19 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(18).Date).Select(p => p.tong).FirstOrDefault(),
                           Sl20 = kq.Where(p => p.NgayNhap == ngaytu.AddDays(19).Date).Select(p => p.tong).FirstOrDefault(),
                       }
                ).ToList();
            BaoCao.rep_PhieuCongKhaiDVKT_01071 rep = new BaoCao.rep_PhieuCongKhaiDVKT_01071();
            frmIn frm = new frmIn();
            rep.ngay1.Value = ngaytu.Day + "/" + ngaytu.Month;
            rep.ngay2.Value = ngaytu.AddDays(+1) <= denngay ? ngaytu.AddDays(+1).Day + "/" + ngaytu.AddDays(+1).Month : null;
            rep.ngay3.Value = ngaytu.AddDays(+2) <= denngay ? ngaytu.AddDays(+2).Day + "/" + ngaytu.AddDays(+2).Month : null;
            rep.ngay4.Value = ngaytu.AddDays(+3) <= denngay ? ngaytu.AddDays(+3).Day + "/" + ngaytu.AddDays(+3).Month : null;
            rep.ngay5.Value = ngaytu.AddDays(+4) <= denngay ? ngaytu.AddDays(+4).Day + "/" + ngaytu.AddDays(+4).Month : null;
            rep.ngay6.Value = ngaytu.AddDays(+5) <= denngay ? ngaytu.AddDays(+5).Day + "/" + ngaytu.AddDays(+5).Month : null;
            rep.ngay7.Value = ngaytu.AddDays(+6) <= denngay ? ngaytu.AddDays(+6).Day + "/" + ngaytu.AddDays(+6).Month : null;
            rep.ngay8.Value = ngaytu.AddDays(+7) <= denngay ? ngaytu.AddDays(+7).Day + "/" + ngaytu.AddDays(+7).Month : null;
            rep.ngay9.Value = ngaytu.AddDays(+9) <= denngay ? ngaytu.AddDays(+8).Day + "/" + ngaytu.AddDays(+8).Month : null;
            rep.ngay10.Value = ngaytu.AddDays(+9) <= denngay ? ngaytu.AddDays(+9).Day + "/" + ngaytu.AddDays(+9).Month : null;
            rep.ngay11.Value = ngaytu.AddDays(+10) <= denngay ? ngaytu.AddDays(+10).Day + "/" + ngaytu.AddDays(+10).Month : null;
            rep.ngay12.Value = ngaytu.AddDays(+11) <= denngay ? ngaytu.AddDays(+11).Day + "/" + ngaytu.AddDays(+11).Month : null;
            rep.ngay13.Value = ngaytu.AddDays(+12) <= denngay ? ngaytu.AddDays(+12).Day + "/" + ngaytu.AddDays(+12).Month : null;
            rep.ngay14.Value = ngaytu.AddDays(+13) <= denngay ? ngaytu.AddDays(+13).Day + "/" + ngaytu.AddDays(+13).Month : null;
            rep.ngay15.Value = ngaytu.AddDays(+14) <= denngay ? ngaytu.AddDays(+14).Day + "/" + ngaytu.AddDays(+14).Month : null;
            rep.ngay16.Value = ngaytu.AddDays(+15) <= denngay ? ngaytu.AddDays(+15).Day + "/" + ngaytu.AddDays(+15).Month : null;
            rep.ngay17.Value = ngaytu.AddDays(+16) <= denngay ? ngaytu.AddDays(+16).Day + "/" + ngaytu.AddDays(+16).Month : null;
            rep.ngay18.Value = ngaytu.AddDays(+17) <= denngay ? ngaytu.AddDays(+17).Day + "/" + ngaytu.AddDays(+17).Month : null;
            rep.ngay19.Value = ngaytu.AddDays(+18) <= denngay ? ngaytu.AddDays(+18).Day + "/" + ngaytu.AddDays(+18).Month : null;
            rep.ngay20.Value = ngaytu.AddDays(+19) <= denngay ? ngaytu.AddDays(+19).Day + "/" + ngaytu.AddDays(+19).Month : null;
            var khoa = (from a in _db.BNKBs.Where(p => p.IDKB == bnkb)
                        join b in _db.KPhongs on a.MaKP equals b.MaKP
                        select new { a, b }).ToList();
            if (khoa.Count > 0)
            {
                rep.Khoa.Value = khoa.First().b.TenKP;
                rep.SoBuong.Value = khoa.First().a.Buong;
                rep.SoGiuong.Value = khoa.First().a.Giuong;
                rep.ChanDoan.Value = khoa.First().a.ChanDoan;
            }
            var vv = (from a in _db.BenhNhans.Where(p => p.MaBNhan == mabn)
                      join b in _db.VaoViens on a.MaBNhan equals b.MaBNhan
                      select new { a.MaBNhan, a.TenBNhan, a.NgaySinh, a.GTinh, b.NgayVao }).ToList();
            if (vv.Count > 0)
            {
                rep.TenBN.Value = vv.First().TenBNhan;
                rep.NgaySinh.Value = vv.First().NgaySinh;
                rep.NgayVV.Value = vv.First().NgayVao;
                rep.Nam.Value = vv.First().GTinh == 0 ? "x" : "";
                rep.Nu.Value = vv.First().GTinh == 1 ? "x" : "";
            }
            rep.DataSource = ds2;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}