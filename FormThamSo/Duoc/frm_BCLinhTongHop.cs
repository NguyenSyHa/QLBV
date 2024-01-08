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
    public partial class frm_BCLinhTongHop : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCLinhTongHop()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        #region class Khoa phòng
        private class KhoaPhong
        {
            private string tenKP;

            public string TenKP
            {
                get { return tenKP; }
                set { tenKP = value; }
            }
            private int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }

            private bool chon;

            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
        }
        #endregion
        List<KhoaPhong> _lKhoaPhong = new List<KhoaPhong>();
        private void frm_BCLinhTongHop_Load(object sender, EventArgs e)
        {
            date_TuNgay.DateTime = DateTime.Now;
            date_DenNgay.DateTime = DateTime.Now;

            var kphong = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang).ToList();
            if (kphong.Count > 0)
            {
                KhoaPhong themmoi1 = new KhoaPhong();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                themmoi1.Chon = true;
                _lKhoaPhong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KhoaPhong themmoi = new KhoaPhong();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    themmoi.Chon = true;
                    _lKhoaPhong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKhoaPhong.ToList();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime ngaytu = DungChung.Ham.NgayTu(date_TuNgay.DateTime);
            DateTime ngayden = DungChung.Ham.NgayDen(date_DenNgay.DateTime);
            List<KhoaPhong> kphong = new List<KhoaPhong>();
            kphong = _lKhoaPhong.Where(p => p.Chon == true).Where(p => p.MaKP > 0).ToList();
            var dt = (from a in _data.DThuocs.Where(p => p.KieuDon == 3 || p.KieuDon == 4).Where(p => p.NgayKe >= ngaytu && p.NgayKe <= ngayden)
                      join b in _data.DThuoccts.Where(p => p.SoPL != 0).Where(p => p.Status == 1) on a.IDDon equals b.IDDon
                      select new { b, a.MaKP,a.KieuDon}).ToList();
            var dv = (from a in _data.DichVus.Where(p => p.PLoai == 1)
                      join b in _data.NhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 10 || p.IDNhom == 11 || p.IDNhom == 20) on a.IDNhom equals b.IDNhom
                      select a).ToList();
            var ds1 = (from a in dt
                      join b in dv on a.b.MaDV equals b.MaDV
                      select new { a, b }).ToList();
            var ds = (from a in ds1
                  group a by new { a.a.MaKP } into kq
                  select new
                  {
                      TenKP = "",
                      MaKP = kq.Key.MaKP,
                      Thuoc = kq.Where(p => p.b.IDNhom == 4 || p.b.IDNhom == 5 || p.b.IDNhom == 6).Where(p => p.a.KieuDon == 3).Sum(p => p.a.b.SoLuong) + kq.Where(p => p.b.IDNhom == 4 || p.b.IDNhom == 5 || p.b.IDNhom == 6).Where(p => p.a.KieuDon == 4).Sum(p => p.a.b.SoLuong),
                      VTYT = kq.Where(p => p.b.IDNhom == 10 || p.b.IDNhom == 11).Where(p => p.a.KieuDon == 3).Sum(p => p.a.b.SoLuong) + kq.Where(p => p.b.IDNhom == 10 || p.b.IDNhom == 11).Where(p => p.a.KieuDon == 4).Sum(p => p.a.b.SoLuong),
                      HoaChat = kq.Where(p => p.b.IDNhom == 20).Where(p => p.a.KieuDon == 3).Sum(p => p.a.b.SoLuong) + kq.Where(p => p.b.IDNhom == 20).Where(p => p.a.KieuDon == 4).Sum(p => p.a.b.SoLuong),
                      Tong = kq.Where(p => p.a.KieuDon == 3).Sum(p => p.a.b.SoLuong) + kq.Where(p => p.a.KieuDon == 4).Sum(p => p.a.b.SoLuong)
                  }).ToList();
            ds = (from a in ds
                  join b in kphong on a.MaKP equals b.MaKP
                  select new { TenKP = b.TenKP, MaKP = a.MaKP, Thuoc = a.Thuoc, VTYT = a.VTYT, HoaChat = a.HoaChat, Tong = a.Tong }).ToList();
            BaoCao.rep_BCLinhTongHop rep = new BaoCao.rep_BCLinhTongHop();
            frmIn frm = new frmIn();
            rep.DataSource = ds.ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKhoaPhong.First().Chon == true)
                        {
                            foreach (var a in _lKhoaPhong)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoaPhong)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoaPhong.ToList();
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}