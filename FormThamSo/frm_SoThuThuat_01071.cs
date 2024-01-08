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
    public partial class frm_SoThuThuat_01071 : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoThuThuat_01071()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        List<KPhong> _Kphong = new List<KPhong>();
        List<KPhong> _lkp = new List<KPhong>();
        private void frm_SoThuThuat_01071_Load(object sender, EventArgs e)
        {
            
            radTrongDM.SelectedIndex = 2;
            _lkp = _Data.KPhongs.ToList();
            LupNgaytu.DateTime = System.DateTime.Now;
            LupNgayden.DateTime = System.DateTime.Now;
            _Kphong.Clear();
            List<DTBN> _lDTBN = _Data.DTBNs.ToList();
            _lDTBN.Add(new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            cbo_DoiTuong.Properties.DataSource = _lDTBN.OrderBy(p => p.IDDTBN);
            var kphong = (from kp in _Data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.TenKP = "Chọn tất cả";
                themmoi1.MaKP = 0;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.TenKP = a.TenKP;
                    themmoi.MaKP = a.MaKP;
                    _Kphong.Add(themmoi);
                }
                bindingSource1.DataSource = _Kphong.ToList();
                lup_KPhong.Properties.DataSource = bindingSource1;
            }
        }

        private void butTaoBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
             DateTime denngay = DungChung.Ham.NgayDen(LupNgayden.DateTime);
             List<ds> _ds = new List<ds>();
             _ds.Clear();
             int _idDTBN = 99;
             if (cbo_DoiTuong.EditValue != null && cbo_DoiTuong.EditValue.ToString() != "")
                 _idDTBN = Convert.ToInt32(cbo_DoiTuong.EditValue);
             int x = 0;
             if (lup_KPhong.Text == "" || lup_KPhong.Text == "Chọn tất cả")
             {
                 x = 0;
             }
             else
             {
                 x = Convert.ToInt16(lup_KPhong.EditValue);
             }
            var dt = (from a in _Data.DThuocs
                      join b in _Data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.MaKP == x || x == 0).Where(p => radTrongDM.SelectedIndex == 2 ? true : p.TrongBH == radTrongDM.SelectedIndex) on a.IDDon equals b.IDDon
                      join d in _Data.KPhongs on b.MaKP equals d.MaKP
                      select new { 
                          a.MaBNhan,
                          b.NgayNhap,
                          b.MaDV,
                          b.MaKP,
                          b.IDKB,
                          d.PLoai
                      }).ToList();
            var dv = (from a in _Data.DichVus
                      join b in _Data.TieuNhomDVs.Where(p => p.TenRG == "Thủ thuật") on a.IdTieuNhom equals b.IdTieuNhom
                      select new { 
                          a.MaDV,
                          a.TenDV
                      }).ToList();
            var idbkmin = (from a in _Data.BNKBs.Where(p => p.NgayKham <= denngay)
                           join b in _Data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") on a.MaKP equals b.MaKP
                           group new { a, b } by new {
                               a.MaBNhan,
                               b.PLoai
                           } into kq
                           select new { 
                               kq.Key.MaBNhan,
                               kq.Key.PLoai,
                               IDKBMin = kq.Min(p => p.a.IDKB),
                               ChanDoan = ""
                           }).ToList();
            idbkmin = (from a in idbkmin
                       join b in _Data.BNKBs on a.IDKBMin equals b.IDKB
                       select new
                       {
                           a.MaBNhan,
                           a.PLoai,
                           a.IDKBMin,
                           b.ChanDoan
                       }).ToList();
            var bn = (from a in _Data.BenhNhans.Where(p => p.IDDTBN == _idDTBN || _idDTBN == 99).Where(p => cbo_NoiTru.SelectedIndex == 2 ? true : p.NoiTru == cbo_NoiTru.SelectedIndex)
                      join b in _Data.VaoViens on a.MaBNhan equals b.MaBNhan into k1
                      from k in k1.DefaultIfEmpty()
                      join c in _Data.BNKBs.Where(p => p.MaKP == x || x == 0) on a.MaBNhan equals c.MaBNhan
                      
                      select new { 
                          a.MaBNhan,
                          TenBNhan = a.TenBNhan,
                          SoBA = k != null? k.SoBA : "",
                          c.ChanDoan,
                          Tuoi =  a.Tuoi,
                          GTinh = a.GTinh,
                          BHYT = a.DTuong == "BHYT" ? "X" : "",
                          c.MaKP,
                          c.IDKB,
                      }).ToList();
            var ds1 = (from a in bn
                      join b in dt on a.MaBNhan equals b.MaBNhan
                      join c in dv on b.MaDV equals c.MaDV
                      where (a.MaKP == b.MaKP && a.IDKB == b.IDKB)
                      select new {
                          a.MaBNhan,
                          a.SoBA,
                          a.TenBNhan,
                          a.GTinh,
                          a.Tuoi,
                          a.BHYT,
                          a.ChanDoan,
                          c.TenDV,
                          b.NgayNhap,
                          b.MaKP,
                          c.MaDV
                      }).OrderBy(p => p.MaBNhan).ThenBy(p => p.NgayNhap).ToList();

            var chung = (from a in ds1
                         group a by new
                         {
                             a.MaBNhan,
                             a.SoBA,
                             a.TenBNhan,
                             a.GTinh,
                             a.Tuoi,
                             a.BHYT,
                             a.ChanDoan,
                             a.TenDV,
                             a.MaKP,
                             a.MaDV
                         } into kq
                         select new { 
                             MaBNhan = kq.Key.MaBNhan,
                             SoBA = kq.Key.SoBA,
                             HoTen = kq.Key.TenBNhan,
                             Tuoi = kq.Key.Tuoi,
                             GTinh = kq.Key.GTinh,
                             BHYT = kq.Key.BHYT,
                             ChanDoan = kq.Key.ChanDoan,
                             TenDV = kq.Key.TenDV,
                             kq.Key.MaKP,
                             kq.Key.MaDV
                         }).ToList();

            
            foreach (var item in chung)
            {
                ds them = new ds();
                them.SoBA = item.SoBA;
                them.HoTen = item.MaBNhan.ToString() + "_" + item.HoTen;
                them.Nam = item.GTinh == 1 ? item.Tuoi.ToString() : "";
                them.Nu = item.GTinh == 0 ? item.Tuoi.ToString() : "";
                them.BHYT = item.BHYT;
                them.ChanDoan = item.ChanDoan;
                them.TenDV = item.TenDV;
                var te = ds1.Distinct().Where(p => p.MaBNhan == item.MaBNhan && p.SoBA == item.SoBA && p.TenDV == item.TenDV && p.MaKP == item.MaKP && p.MaDV == item.MaDV).ToList();
                them.Sl1 = te.Count > 0 ? te.Skip(0).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl2 = te.Count > 1 ? te.Skip(1).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl3 = te.Count > 2 ? te.Skip(2).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl4 = te.Count > 3 ? te.Skip(3).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl5 = te.Count > 4 ? te.Skip(4).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl6 = te.Count > 5 ? te.Skip(5).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl7 = te.Count > 6 ? te.Skip(6).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl8 = te.Count > 7 ? te.Skip(7).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl9 = te.Count > 8 ? te.Skip(8).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl10 = te.Count > 9 ? te.Skip(9).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl11 = te.Count > 10 ? te.Skip(10).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                _ds.Add(them);
            }

            ds1 = (from a in bn
                   join b in dt on a.MaBNhan equals b.MaBNhan
                   join c in dv on b.MaDV equals c.MaDV
                   join d in idbkmin on a.MaBNhan equals d.MaBNhan
                   where (a.MaKP == b.MaKP && b.IDKB == 0 && b.PLoai == d.PLoai)
                   select new
                   {
                       a.MaBNhan,
                       a.SoBA,
                       a.TenBNhan,
                       a.GTinh,
                       a.Tuoi,
                       a.BHYT,
                       d.ChanDoan,
                       c.TenDV,
                       b.NgayNhap,
                       b.MaKP,
                       c.MaDV
                   }).OrderBy(p => p.MaBNhan).ThenBy(p => p.NgayNhap).ToList();
            chung = (from a in ds1
                     group a by new
                     {
                         a.MaBNhan,
                         a.SoBA,
                         a.TenBNhan,
                         a.GTinh,
                         a.Tuoi,
                         a.BHYT,
                         a.ChanDoan,
                         a.TenDV,
                         a.MaKP,
                         a.MaDV
                     } into kq
                     select new
                     {
                         MaBNhan = kq.Key.MaBNhan,
                         SoBA = kq.Key.SoBA,
                         HoTen = kq.Key.TenBNhan,
                         Tuoi = kq.Key.Tuoi,
                         GTinh = kq.Key.GTinh,
                         BHYT = kq.Key.BHYT,
                         ChanDoan = kq.Key.ChanDoan,
                         TenDV = kq.Key.TenDV,
                         kq.Key.MaKP,
                         kq.Key.MaDV
                     }).ToList();


            foreach (var item in chung)
            {
                ds them = new ds();
                them.SoBA = item.SoBA;
                them.HoTen = item.MaBNhan.ToString() + "_" + item.HoTen;
                them.Nam = item.GTinh == 1 ? item.Tuoi.ToString() : "";
                them.Nu = item.GTinh == 0 ? item.Tuoi.ToString() : "";
                them.BHYT = item.BHYT;
                them.ChanDoan = item.ChanDoan;
                them.TenDV = item.TenDV;
                var te = ds1.Distinct().Where(p => p.MaBNhan == item.MaBNhan && p.SoBA == item.SoBA && p.TenDV == item.TenDV && p.MaKP == item.MaKP && p.MaDV == item.MaDV).ToList();
                them.Sl1 = te.Count > 0 ? te.Skip(0).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl2 = te.Count > 1 ? te.Skip(1).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl3 = te.Count > 2 ? te.Skip(2).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl4 = te.Count > 3 ? te.Skip(3).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl5 = te.Count > 4 ? te.Skip(4).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl6 = te.Count > 5 ? te.Skip(5).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl7 = te.Count > 6 ? te.Skip(6).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl8 = te.Count > 7 ? te.Skip(7).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl9 = te.Count > 8 ? te.Skip(8).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl10 = te.Count > 9 ? te.Skip(9).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                them.Sl11 = te.Count > 10 ? te.Skip(10).First().NgayNhap.Value.Date.ToString("dd/MM") : "";
                _ds.Add(them);
            }
            BaoCao.rep_SoThuThuat_01071 rep = new BaoCao.rep_SoThuThuat_01071();
            rep.BV.Value = DungChung.Bien.TenCQ.ToUpper();
            if (lup_KPhong.Text == "" || lup_KPhong.Text == "Chọn tất cả")
            {
                rep.Khoa.Value = "KHOA: TẤT CẢ CÁC KHOA LÂM SÀNG VÀ PHÒNG KHÁM";
            }
            else
            {
                rep.Khoa.Value = lup_KPhong.Text.ToUpper();
            }

            rep.ngaytu.Value = "-  Bắt đầu sử dụng ngày:  " + tungay.Date.ToString("dd") + "/" + tungay.Date.ToString("MM") + "/" + tungay.Date.ToString("yyyy");
            rep.ngayden.Value = "-  Hết sổ, nộp lưu trữ ngày:  " + denngay.Date.ToString("dd") + "/" + denngay.Date.ToString("MM") + "/" + denngay.Date.ToString("yyyy");
            rep.DataSource = _ds.ToList();
            rep.BindingData();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        private class ds
        {
            string hoTen;

            public string HoTen
            {
                get { return hoTen; }
                set { hoTen = value; }
            }
            private string soBA;

            public string SoBA
            {
                get { return soBA; }
                set { soBA = value; }
            }
            string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            string nam;

            public string Nam
            {
                get { return nam; }
                set { nam = value; }
            }
            string nu;

            public string Nu
            {
                get { return nu; }
                set { nu = value; }
            }
            string bhyt;

            public string BHYT
            {
                get { return bhyt; }
                set { bhyt = value; }
            }
            string chanDoan;

            public string ChanDoan
            {
                get { return chanDoan; }
                set { chanDoan = value; }
            }
            string sl1;

            public string Sl1
            {
                get { return sl1; }
                set { sl1 = value; }
            }
            string sl2;

            public string Sl2
            {
                get { return sl2; }
                set { sl2 = value; }
            }
            string sl3;

            public string Sl3
            {
                get { return sl3; }
                set { sl3 = value; }
            }
            string sl4;

            public string Sl4
            {
                get { return sl4; }
                set { sl4 = value; }
            }string sl5;

            public string Sl5
            {
                get { return sl5; }
                set { sl5 = value; }
            }
            string sl6;

            public string Sl6
            {
                get { return sl6; }
                set { sl6 = value; }
            }
            string sl7;

            public string Sl7
            {
                get { return sl7; }
                set { sl7 = value; }
            }
            string sl8;

            public string Sl8
            {
                get { return sl8; }
                set { sl8 = value; }
            }
            string sl9;

            public string Sl9
            {
                get { return sl9; }
                set { sl9 = value; }
            }string sl10;

            public string Sl10
            {
                get { return sl10; }
                set { sl10 = value; }
            }
            string sl11;

            public string Sl11
            {
                get { return sl11; }
                set { sl11 = value; }
            }
            int st;

            public int St
            {
                get { return st; }
                set { st = value; }
            }
        }
        private void ButHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}