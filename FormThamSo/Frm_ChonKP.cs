using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class Frm_ChonKP : DevExpress.XtraEditors.XtraForm
    {
        private int _mbn = 0, _makp = 0, _mau = 0;
        private string _TenBN = "";

        public Frm_ChonKP()
        {
            InitializeComponent();
        }

        public Frm_ChonKP(int _m, int makp, int Mau)
        {
            InitializeComponent();
            _mbn = _m;
            _mau = Mau;
            _makp = makp;
        }

        public class l_CTThuoc
        {
            public string tendv, mabn, donvi, sl1, sl2, sl3, sl4, sl5, sl6, sl7, sl8, sl9, sl10, sl11, sl12, sl13, sl14, sl15, sl16, sl17, sl18, sl19, sl20, sl21, sl22, sl23, sl24, sl25, sl26, sl27, sl28, sl29, sl30, sl31, sl32, tennhomdv;
            private string sL;

            public string SL
            {
                get { return sL; }
                set { sL = value; }
            }

            private string tT;

            public string TT
            {
                get { return tT; }
                set { tT = value; }
            }

            private int pLoai;

            public int PLoai
            {
                get { return pLoai; }
                set { pLoai = value; }
            }

            private int trongbh, idnhom, madv;
            private double soluong, dongia, thanhtien;

            public string TenNhomDV
            { set { tennhomdv = value; } get { return tennhomdv; } }

            public int IDNHOM
            { set { idnhom = value; } get { return idnhom; } }

            public string TenDV
            {
                set { tendv = value; }
                get { return tendv; }
            }

            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }

            public string DonVi
            {
                set { donvi = value; }
                get { return donvi; }
            }

            public double SoLuong
            {
                set { soluong = value; }
                get { return soluong; }
            }

            public double DonGia
            {
                set { dongia = value; }
                get { return dongia; }
            }

            public double ThanhTien
            { set { thanhtien = value; } get { return thanhtien; } }

            public string SL1
            { set { sl1 = value; } get { return sl1; } }

            public string SL2
            { set { sl2 = value; } get { return sl2; } }

            public string SL3
            { set { sl3 = value; } get { return sl3; } }

            public string SL4
            { set { sl4 = value; } get { return sl4; } }

            public string SL5
            { set { sl5 = value; } get { return sl5; } }

            public string SL6
            { set { sl6 = value; } get { return sl6; } }

            public string SL7
            { set { sl7 = value; } get { return sl7; } }

            public string SL8
            { set { sl8 = value; } get { return sl8; } }

            public string SL9
            { set { sl9 = value; } get { return sl9; } }

            public string SL10
            { set { sl10 = value; } get { return sl10; } }

            public string SL11
            { set { sl11 = value; } get { return sl11; } }

            public string SL12
            { set { sl12 = value; } get { return sl12; } }

            public string SL13
            { set { sl13 = value; } get { return sl13; } }

            public string SL14
            { set { sl14 = value; } get { return sl14; } }

            public string SL15
            { set { sl15 = value; } get { return sl15; } }

            public string SL16
            { set { sl16 = value; } get { return sl16; } }

            public string SL17
            { set { sl17 = value; } get { return sl17; } }

            public string SL18
            { set { sl18 = value; } get { return sl18; } }

            public string SL19
            { set { sl19 = value; } get { return sl19; } }

            public string SL20
            { set { sl20 = value; } get { return sl20; } }

            public string SL21
            { set { sl21 = value; } get { return sl21; } }

            public string SL22
            { set { sl22 = value; } get { return sl22; } }

            public string SL23
            { set { sl23 = value; } get { return sl23; } }

            public string SL24
            { set { sl24 = value; } get { return sl24; } }

            public string SL25
            { set { sl25 = value; } get { return sl25; } }

            public string SL26
            { set { sl26 = value; } get { return sl26; } }

            public string SL27
            { set { sl27 = value; } get { return sl27; } }

            public string SL28
            { set { sl28 = value; } get { return sl28; } }

            public string SL29
            { set { sl29 = value; } get { return sl29; } }

            public string SL30
            { set { sl30 = value; } get { return sl30; } }

            public string SL31
            { set { sl31 = value; } get { return sl31; } }

            public string SL32
            { set { sl32 = value; } get { return sl32; } }
        }

        private class DV
        {
            public string TenDV { get; set; }
            public int? MaDV { get; set; }
            public int? IDNhom { get; set; }
            public bool Chon { get; set; }
            public string SapXep { get; set; }
            public string TenNhom { get; set; }
            public string TenRG { get; set; }
            public string DonVi { get; set; }
            public string ChiDinh { get; set; }
        }

        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;

            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }

            public int makp
            { set { MaKP = value; } get { return MaKP; } }

            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        private List<DV> _DichVu = new List<DV>();
        private List<KPhong> _Kphong = new List<KPhong>();
        private QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private int trangthai = 0;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            //var khoa = (from a in data.BNKBs.Where(p => p.MaBNhan == _mbn)
            //            join b in data.KPhongs on a.MaKP equals b.MaKP
            //            select new { a, b }).ToList();
            //  int makp = khoa.First().a.MaKP ?? 0;
            var dv = (from a in data.DichVus
                          //join b in data.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                          //join c in data.NhomDVs on a.IDNhom equals c.IDNhom
                      select new { a.MaDV, a.TenDV, a.DonVi, a.IDNhom, a.HamLuong, a.TenRG }).ToList();
            if (DungChung.Bien.MaBV == "27194")
            {
                if (radBN.SelectedIndex != 3)
                {
                    dv = (from a in data.DichVus.Where(p => p.TrongDM == radBN.SelectedIndex)
                              //join b in data.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                              //join c in data.NhomDVs on a.IDNhom equals c.IDNhom
                          select new { a.MaDV, a.TenDV, a.DonVi, a.IDNhom, a.HamLuong, a.TenRG }).ToList();
                }
            }
            var bn_dv = (from a in data.DThuocs.Where(P => P.MaBNhan == _mbn)
                         join c in data.DThuoccts on a.IDDon equals c.IDDon
                         select new { a.MaBNhan, a.NgayKe, c.NgayNhap, c.MaDV, c.SoLuong, c.MaKP }).ToList();
            var ds = (from a in bn_dv
                      join d in dv on a.MaDV equals d.MaDV
                      group a by new { a.MaDV, NgayNhap = a.NgayNhap.Value.Date, d.TenDV, d.IDNhom, d.HamLuong, d.DonVi, d.TenRG, a.MaKP } into kq
                      select new
                      {
                          kq.Key.MaDV,
                          kq.Key.TenDV,
                          kq.Key.TenRG,
                          kq.Key.IDNhom,
                          kq.Key.HamLuong,
                          kq.Key.DonVi,
                          kq.Key.MaKP,
                          NgayNhap = kq.Key.NgayNhap,
                          tong = kq.Count()
                      }).OrderBy(p => p.IDNhom).ToList();
            var ds1 = (from a in ds
                       join kp in _Kphong.Where(p => p.chon == true).Where(p => p.makp != 0) on a.MaKP equals kp.makp
                       group a by new { a.MaDV, NgayNhap = a.NgayNhap.Date, a.TenDV, a.IDNhom, a.HamLuong, a.DonVi, a.TenRG } into kq
                       select new
                       {
                           kq.Key.MaDV,
                           kq.Key.TenDV,
                           kq.Key.TenRG,
                           kq.Key.IDNhom,
                           kq.Key.HamLuong,
                           kq.Key.DonVi,
                           NgayNhap = kq.Key.NgayNhap,
                           tong = kq.Count()
                       }).OrderBy(p => p.IDNhom).ToList();
            List<DateTime> ngayy = new List<DateTime>();
            foreach (var item in ds1.Select(p => p.NgayNhap).OrderBy(p => p).Distinct().ToList())
            {
                ngayy.Add(item);
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                if (_mau == 1)
                {
                    if (ngayy.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu!");
                    }
                    else if (ngayy.Count <= 25 && ngayy.Count > 0)
                    {
                        hamin(ngayy[0], data, _mbn);
                    }
                    else if (ngayy.Count > 25 && ngayy.Count <= 50)
                    {
                        hamin(ngayy[0], data, _mbn);
                        hamin(ngayy[25], data, _mbn);
                    }
                    else if (ngayy.Count > 50 && ngayy.Count <= 75)
                    {
                        hamin(ngayy[0], data, _mbn);
                        hamin(ngayy[25], data, _mbn);
                        hamin(ngayy[50], data, _mbn);
                    }
                    else if (ngayy.Count > 75 && ngayy.Count <= 90)
                    {
                        hamin(ngayy[0], data, _mbn);
                        hamin(ngayy[25], data, _mbn);
                        hamin(ngayy[50], data, _mbn);
                        hamin(ngayy[75], data, _mbn);
                    }
                    else
                    {
                        MessageBox.Show("Thời gian quá dài!");
                    }
                }
                else if (_mau == 2)
                {
                    var cd1 = (from a in data.ChiDinhs
                               join b in data.CLS on a.IdCLS equals b.IdCLS
                               where b.MaBNhan == _mbn && b.NgayThang != null
                               select new { b.NgayThang, b.MaBNhan }).ToList();
                    //--
                    var chiDinh = (from a in cd1
                                   join c in data.BenhNhans on a.MaBNhan equals c.MaBNhan
                                   select a.NgayThang).ToList();
                    //--
                    var thuoc = (from dt in data.DThuocs
                                 join dct in data.DThuoccts on dt.IDDon equals dct.IDDon
                                 join d in data.DichVus on dct.MaDV equals d.MaDV
                                 where dt.MaBNhan == _mbn && dt.NgayKe != null
                                        && d.MaNhom5937 != 13
                                 select dt.NgayKe).ToList();

                    DateTime firstDate = new DateTime();
                    DateTime lastDate = new DateTime();

                    int soNgay = 0;
                    ////ngày vào viện
                    //var ngayvao = data.VaoViens.Where(p => p.MaBNhan == _mbn).Select(p => p.NgayVao).FirstOrDefault();
                    //if(ngayvao != null && ngayvao.Value.Year > 2000)
                    //{
                    //    firstDate = Convert.ToDateTime(ngayvao);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Ngày vào viện không hợp lệ");
                    //    return;
                    //}

                    //mốc thời gian đầu
                    if (chiDinh.Count() == 0 && thuoc.Count() == 0)
                    {
                        MessageBox.Show("Bệnh nhân chưa có chỉ định, đơn thuốc");
                        return;
                    }
                    else if (chiDinh.Count() > 0 && thuoc.Count() == 0)
                    {
                        var chiDinhDau = chiDinh.OrderBy(p => p.Value).First();
                        if (chiDinhDau.Value.Year > 2000)
                        {
                            firstDate = Convert.ToDateTime(chiDinhDau);
                        }

                        var chiDinhCuoi = chiDinh.OrderByDescending(p => p.Value).First();
                        if (chiDinhCuoi != null && chiDinhCuoi.Value.Year > 2000)
                        {
                            lastDate = Convert.ToDateTime(chiDinhCuoi);
                        }
                    }
                    else if (chiDinh.Count() == 0 && thuoc.Count() > 0)
                    {
                        var thuocDau = thuoc.OrderBy(p => p.Value).First();
                        if (thuocDau.Value.Year > 2000)
                        {
                            firstDate = Convert.ToDateTime(thuocDau);
                        }

                        var thuocCuoi = thuoc.OrderByDescending(p => p.Value).First();

                        if (thuocCuoi != null && thuocCuoi.Value.Year > 2000)
                        {
                            lastDate = Convert.ToDateTime(thuocCuoi);
                        }
                    }
                    else
                    {
                        var chiDinhDau = chiDinh.OrderBy(p => p.Value).First();
                        var thuocDau = thuoc.OrderBy(p => p.Value).First();
                        if (chiDinhDau.Value.Year > 2000)
                        {
                            firstDate = Convert.ToDateTime(chiDinhDau);
                        }
                        if (thuocDau.Value.Year > 2000 && thuocDau < chiDinhDau)
                        {
                            firstDate = Convert.ToDateTime(thuocDau);
                        }

                        // mốc thời gian cuối
                        var chiDinhCuoi = chiDinh.OrderByDescending(p => p.Value).First();
                        var thuocCuoi = thuoc.OrderByDescending(p => p.Value).First();
                        if (chiDinhCuoi != null && chiDinhCuoi.Value.Year > 2000)
                        {
                            lastDate = Convert.ToDateTime(chiDinhCuoi);
                        }
                        if (thuocCuoi != null && thuocCuoi.Value.Year > 2000 && thuocCuoi > chiDinhCuoi)
                        {
                            lastDate = Convert.ToDateTime(thuocCuoi);
                        }
                    }

                    TimeSpan time = lastDate - firstDate;
                    soNgay = time.Days;
                    if (firstDate.TimeOfDay > lastDate.TimeOfDay)
                    {
                        soNgay++;
                    }
                    if (soNgay < 25 && soNgay >= 0)
                    {
                        hamin(firstDate, data, _mbn);
                    }
                    else if (soNgay >= 25 && soNgay <= 50)
                    {
                        hamin(firstDate, data, _mbn);
                        //hamin(firstDate.AddDays(25), data, _mbn);
                    }
                    else
                    {
                        MessageBox.Show("Thời gian quá dài!");
                    }
                }
            }
            else
            {
                if (ngayy.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu!");
                }
                else if (ngayy.Count <= 20 && ngayy.Count > 0)
                {
                    hamin(ngayy[0], data, _mbn);
                }
                else if (ngayy.Count <= 40 && ngayy.Count > 20)
                {
                    hamin(ngayy[0], data, _mbn);
                    hamin(ngayy[20], data, _mbn);
                }
                else
                {
                    MessageBox.Show("Thời gian quá dài!");
                }
            }
        }

        private class ds
        {
            public int stt { get; set; }
            private string sTT;
            public string STTLM { get; set; }

            public string STT
            {
                get { return sTT; }
                set { sTT = value; }
            }

            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }

            private string donVi;

            public string DonVi
            {
                get { return donVi; }
                set { donVi = value; }
            }

            private double sl1;

            public double Sl1
            {
                get { return sl1; }
                set { sl1 = value; }
            }

            private double sl2;

            public double Sl2
            {
                get { return sl2; }
                set { sl2 = value; }
            }

            private double sl3;

            public double Sl3
            {
                get { return sl3; }
                set { sl3 = value; }
            }

            private double sl4;

            public double Sl4
            {
                get { return sl4; }
                set { sl4 = value; }
            }

            private double sl5;

            public double Sl5
            {
                get { return sl5; }
                set { sl5 = value; }
            }

            private double sl6;

            public double Sl6
            {
                get { return sl6; }
                set { sl6 = value; }
            }

            private double sl7;

            public double Sl7
            {
                get { return sl7; }
                set { sl7 = value; }
            }

            private double sl8;

            public double Sl8
            {
                get { return sl8; }
                set { sl8 = value; }
            }

            private double sl9;

            public double Sl9
            {
                get { return sl9; }
                set { sl9 = value; }
            }

            private double sl10;

            public double Sl10
            {
                get { return sl10; }
                set { sl10 = value; }
            }

            private double sl11;

            public double Sl11
            {
                get { return sl11; }
                set { sl11 = value; }
            }

            private double sl12;

            public double Sl12
            {
                get { return sl12; }
                set { sl12 = value; }
            }

            private double sl13;

            public double Sl13
            {
                get { return sl13; }
                set { sl13 = value; }
            }

            private double sl14;

            public double Sl14
            {
                get { return sl14; }
                set { sl14 = value; }
            }

            private double sl15;

            public double Sl15
            {
                get { return sl15; }
                set { sl15 = value; }
            }

            private double sl16;

            public double Sl16
            {
                get { return sl16; }
                set { sl16 = value; }
            }

            private double sl17;

            public double Sl17
            {
                get { return sl17; }
                set { sl17 = value; }
            }

            private double sl18;

            public double Sl18
            {
                get { return sl18; }
                set { sl18 = value; }
            }

            private double sl19;

            public double Sl19
            {
                get { return sl19; }
                set { sl19 = value; }
            }

            private double sl20;

            public double Sl20
            {
                get { return sl20; }
                set { sl20 = value; }
            }

            private double sl21;

            public double Sl21
            {
                get { return sl21; }
                set { sl21 = value; }
            }

            private double sl22;

            public double Sl22
            {
                get { return sl22; }
                set { sl22 = value; }
            }

            private double sl23;

            public double Sl23
            {
                get { return sl23; }
                set { sl23 = value; }
            }

            private double sl24;

            public double Sl24
            {
                get { return sl24; }
                set { sl24 = value; }
            }

            private double sl25;

            public double Sl25
            {
                get { return sl25; }
                set { sl25 = value; }
            }

            private double sl26;

            public double Sl26
            {
                get { return sl26; }
                set { sl26 = value; }
            }

            private double sl27;

            public double Sl27
            {
                get { return sl27; }
                set { sl27 = value; }
            }

            private double sl28;

            public double Sl28
            {
                get { return sl28; }
                set { sl28 = value; }
            }

            private double sl29;

            public double Sl29
            {
                get { return sl29; }
                set { sl29 = value; }
            }

            private double sl30;

            public double Sl30
            {
                get { return sl30; }
                set { sl30 = value; }
            }

            private string ghiChu;

            public string GhiChu
            {
                get { return ghiChu; }
                set { ghiChu = value; }
            }

            private int st;

            public int St
            {
                get { return st; }
                set { st = value; }
            }
        }

        private void hamin(DateTime n1, QLBV_Database.QLBVEntities _db, int mabn)
        {
            List<ds> _ds = new List<ds>();
            //var khoa = (from a in _db.BNKBs.Where(p => p.IDKB == bnkb)
            //            join b in _db.KPhongs on a.MaKP equals b.MaKP
            //            select new { a, b }).ToList();
            //int makp = khoa.First().a.MaKP ?? 0;
            var dv = (from a in _db.DichVus
                          //join b in _db.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                          //join c in _db.NhomDVs on a.IDNhom equals c.IDNhom
                      select new { a.MaDV, a.TenDV, a.DonVi, a.IDNhom, a.HamLuong, a.TenRG }).ToList();
            var bn_dv = (from a in _db.DThuocs.Where(P => P.MaBNhan == mabn)
                         join c in _db.DThuoccts on a.IDDon equals c.IDDon
                         select new { a.MaBNhan, a.NgayKe, c.NgayNhap, c.MaDV, c.SoLuong, c.MaKP, c.IDCD }).ToList();
            var ds = (from a in bn_dv.Where(p => p.NgayNhap >= n1)
                      join d in dv on a.MaDV equals d.MaDV
                      group a by new { a.MaDV, NgayNhap = a.NgayNhap.Value.Date, d.TenDV, d.IDNhom, d.HamLuong, d.DonVi, d.TenRG, a.IDCD, a.MaKP, a.SoLuong } into kq
                      select new
                      {
                          kq.Key.MaDV,
                          kq.Key.TenDV,
                          kq.Key.TenRG,
                          kq.Key.IDNhom,
                          kq.Key.HamLuong,
                          kq.Key.DonVi,
                          kq.Key.MaKP,
                          NgayNhap = kq.Key.NgayNhap,
                          kq.Key.SoLuong,
                          kq.Key.IDCD
                      }).OrderBy(p => p.IDNhom).ToList();
            var ds1 = (from a in ds
                       join kp in _Kphong.Where(p => p.chon == true).Where(p => p.makp != 0) on a.MaKP equals kp.makp
                       group a by new { a.MaDV, NgayNhap = a.NgayNhap.Date, a.TenDV, a.IDNhom, a.HamLuong, a.DonVi, a.TenRG, a.IDCD, a.SoLuong } into kq
                       select new
                       {
                           kq.Key.MaDV,
                           kq.Key.TenDV,
                           kq.Key.TenRG,
                           kq.Key.IDNhom,
                           kq.Key.HamLuong,
                           kq.Key.DonVi,
                           NgayNhap = kq.Key.NgayNhap,
                           tong = kq.Sum(p => p.SoLuong),
                           kq.Key.IDCD
                       }).OrderBy(p => p.IDNhom).ToList();
            var dv240 = (from cd in _db.ChiDinhs
                      join cls in _db.CLS on cd.IdCLS equals cls.IdCLS
                      where cls.MaBNhan == mabn && cd.Status == 0
                      select new { cls.Status, cls.MaKP, cls.NgayThang, cd.ChiDinh1, cd.MaDV }).ToList();
            var dv_24012_0 = (from cd in dv240
                              join d in _db.DichVus on cd.MaDV equals d.MaDV
                              select new { d.MaDV, d.TenDV, d.DonVi, d.IDNhom, d.HamLuong, d.TenRG, cd.Status, cd.MaKP, cd.NgayThang, cd.ChiDinh1 }).ToList();
            var dv_24012 = (from a in dv_24012_0.Where(p => p.NgayThang >= n1)
                            join kp in _Kphong.Where(p => p.chon == true).Where(p => p.makp != 0) on a.MaKP equals kp.makp
                            group a by new { a.MaDV, NgayThang = a.NgayThang.Value, a.TenDV, a.IDNhom, a.HamLuong, a.DonVi, a.TenRG, a.ChiDinh1 } into kq
                            select new
                            {
                                kq.Key.MaDV,
                                kq.Key.TenDV,
                                kq.Key.TenRG,
                                kq.Key.IDNhom,
                                kq.Key.HamLuong,
                                kq.Key.DonVi,
                                NgayNhap = kq.Key.NgayThang,
                                tong = kq.Count(),
                                kq.Key.ChiDinh1
                            }).OrderBy(p => p.IDNhom).ToList();
            if (_mau == 1)
            {
                List<DateTime> ngayy = new List<DateTime>();
                foreach (var item in ds1.OrderBy(p => p.NgayNhap).Select(p => p.NgayNhap).Distinct().ToList())
                {
                    ngayy.Add(item);
                }
                var ds3 = (from a in ds1
                           join nhom in _db.NhomDVs on a.IDNhom equals nhom.IDNhom
                           group a by new { a.MaDV, a.TenDV, a.IDNhom, a.HamLuong, a.DonVi, nhom.TenNhom } into kq
                           select new 
                           {
                               STT = (kq.Key.IDNhom == 4 || kq.Key.IDNhom == 5 || kq.Key.IDNhom == 6) ? "Thuốc, dịch truyền (tên, nồng độ/hàm lượng)" : ((kq.Key.IDNhom == 8 || kq.Key.IDNhom == 9) ? "Dịch vụ kỹ thuật" : ((kq.Key.IDNhom == 10 || kq.Key.IDNhom == 11) ? "Vật tư y tế (không có trong dịch vụ kỹ thuật)" : kq.Key.TenNhom)),
                               TenDV = (kq.Key.IDNhom == 4 || kq.Key.IDNhom == 5 || kq.Key.IDNhom == 6) ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               St = (kq.Key.IDNhom == 4 || kq.Key.IDNhom == 5 || kq.Key.IDNhom == 6) ? 6 : ((kq.Key.IDNhom == 8 || kq.Key.IDNhom == 9) ? 4 : ((kq.Key.IDNhom == 10 || kq.Key.IDNhom == 11) ? 7 : ((kq.Key.IDNhom == 7 ? 5 : kq.Key.IDNhom ?? 0)))),
                               Sl1 = ngayy.Count > 0 ? kq.Where(p => p.NgayNhap == ngayy[0]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl2 = ngayy.Count > 1 ? kq.Where(p => p.NgayNhap == ngayy[1]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl3 = ngayy.Count > 2 ? kq.Where(p => p.NgayNhap == ngayy[2]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl4 = ngayy.Count > 3 ? kq.Where(p => p.NgayNhap == ngayy[3]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl5 = ngayy.Count > 4 ? kq.Where(p => p.NgayNhap == ngayy[4]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl6 = ngayy.Count > 5 ? kq.Where(p => p.NgayNhap == ngayy[5]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl7 = ngayy.Count > 6 ? kq.Where(p => p.NgayNhap == ngayy[6]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl8 = ngayy.Count > 7 ? kq.Where(p => p.NgayNhap == ngayy[7]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl9 = ngayy.Count > 8 ? kq.Where(p => p.NgayNhap == ngayy[8]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl10 = ngayy.Count > 9 ? kq.Where(p => p.NgayNhap == ngayy[9]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl11 = ngayy.Count > 10 ? kq.Where(p => p.NgayNhap == ngayy[10]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl12 = ngayy.Count > 11 ? kq.Where(p => p.NgayNhap == ngayy[11]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl13 = ngayy.Count > 12 ? kq.Where(p => p.NgayNhap == ngayy[12]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl14 = ngayy.Count > 13 ? kq.Where(p => p.NgayNhap == ngayy[13]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl15 = ngayy.Count > 14 ? kq.Where(p => p.NgayNhap == ngayy[14]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl16 = ngayy.Count > 15 ? kq.Where(p => p.NgayNhap == ngayy[15]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl17 = ngayy.Count > 16 ? kq.Where(p => p.NgayNhap == ngayy[16]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl18 = ngayy.Count > 17 ? kq.Where(p => p.NgayNhap == ngayy[17]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl19 = ngayy.Count > 18 ? kq.Where(p => p.NgayNhap == ngayy[18]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl20 = ngayy.Count > 19 ? kq.Where(p => p.NgayNhap == ngayy[19]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl21 = ngayy.Count > 20 ? kq.Where(p => p.NgayNhap == ngayy[20]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl22 = ngayy.Count > 21 ? kq.Where(p => p.NgayNhap == ngayy[21]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl23 = ngayy.Count > 22 ? kq.Where(p => p.NgayNhap == ngayy[22]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl24 = ngayy.Count > 23 ? kq.Where(p => p.NgayNhap == ngayy[23]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl25 = ngayy.Count > 24 ? kq.Where(p => p.NgayNhap == ngayy[24]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl26 = ngayy.Count > 25 ? kq.Where(p => p.NgayNhap == ngayy[25]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl27 = ngayy.Count > 26 ? kq.Where(p => p.NgayNhap == ngayy[26]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl28 = ngayy.Count > 27 ? kq.Where(p => p.NgayNhap == ngayy[27]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl29 = ngayy.Count > 28 ? kq.Where(p => p.NgayNhap == ngayy[28]).Select(p => p.tong).FirstOrDefault() : 0,
                               Sl30 = ngayy.Count > 29 ? kq.Where(p => p.NgayNhap == ngayy[29]).Select(p => p.tong).FirstOrDefault() : 0,
                           }
                    ).ToList();
                var ds2 = (from a in ds3
                           group a by new { a.St, a.STT, a.DonVi, a.TenDV } into kq
                           select new 
                           {
                               STT = kq.Key.STT,
                               TenDV = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               St = kq.Key.St,

                               Sl1 = kq.Sum(p => p.Sl1),
                               Sl2 = kq.Sum(p => p.Sl2),
                               Sl3 = kq.Sum(p => p.Sl3),
                               Sl4 = kq.Sum(p => p.Sl4),
                               Sl5 = kq.Sum(p => p.Sl5),
                               Sl6 = kq.Sum(p => p.Sl6),
                               Sl7 = kq.Sum(p => p.Sl7),
                               Sl8 = kq.Sum(p => p.Sl8),
                               Sl9 = kq.Sum(p => p.Sl9),
                               Sl10 = kq.Sum(p => p.Sl10),
                               Sl11 = kq.Sum(p => p.Sl11),
                               Sl12 = kq.Sum(p => p.Sl12),
                               Sl13 = kq.Sum(p => p.Sl13),
                               Sl14 = kq.Sum(p => p.Sl14),
                               Sl15 = kq.Sum(p => p.Sl15),
                               Sl16 = kq.Sum(p => p.Sl16),
                               Sl17 = kq.Sum(p => p.Sl17),
                               Sl18 = kq.Sum(p => p.Sl18),
                               Sl19 = kq.Sum(p => p.Sl19),
                               Sl20 = kq.Sum(p => p.Sl20),
                               Sl21 = kq.Sum(p => p.Sl21),
                               Sl22 = kq.Sum(p => p.Sl22),
                               Sl23 = kq.Sum(p => p.Sl23),
                               Sl24 = kq.Sum(p => p.Sl24),
                               Sl25 = kq.Sum(p => p.Sl25),
                               Sl26 = kq.Sum(p => p.Sl26),
                               Sl27 = kq.Sum(p => p.Sl27),
                               Sl28 = kq.Sum(p => p.Sl28),
                               Sl29 = kq.Sum(p => p.Sl29),
                               Sl30 = kq.Sum(p => p.Sl30),
                           }
                    ).OrderBy(p => p.St).ToList();

                int i = 1;
                List<ds> _abc = new List<ds>();
                foreach (var item in ds2)
                {
                    ds r = new ds();
                    r.stt = i;
                    r.STTLM = DungChung.Ham.ToRoman(i);
                    r.STT = item.STT;
                    r.TenDV = item.TenDV;
                    r.DonVi = item.DonVi;
                    r.St = item.St;
                    r.Sl1 = item.Sl1;
                    r.Sl2 = item.Sl2;
                    r.Sl3 = item.Sl3;
                    r.Sl4 = item.Sl4;
                    r.Sl5 = item.Sl5;
                    r.Sl6 = item.Sl6;
                    r.Sl7 = item.Sl7;
                    r.Sl8 = item.Sl8;
                    r.Sl9 = item.Sl9;
                    r.Sl10 = item.Sl10;
                    r.Sl11 = item.Sl11;
                    r.Sl12 = item.Sl12;
                    r.Sl13 = item.Sl13;
                    r.Sl14 = item.Sl14;
                    r.Sl15 = item.Sl15;
                    r.Sl16 = item.Sl16;
                    r.Sl17 = item.Sl17;
                    r.Sl18 = item.Sl18;
                    r.Sl19 = item.Sl19;
                    r.Sl20 = item.Sl20;
                    r.Sl21 = item.Sl21;
                    r.Sl22 = item.Sl22;
                    r.Sl23 = item.Sl23;
                    r.Sl24 = item.Sl24;
                    r.Sl25 = item.Sl25;
                    r.Sl26 = item.Sl26;
                    r.Sl27 = item.Sl27;
                    r.Sl28 = item.Sl28;
                    r.Sl29 = item.Sl29;
                    r.Sl30 = item.Sl30;

                    if (_abc.Where(p => p.STT == item.STT).Count() == 0)
                    {
                        i++;
                    }
                    _abc.Add(r);
                }
                BaoCao.rep_PhieuCongKhaiDVKT_01071 rep = new BaoCao.rep_PhieuCongKhaiDVKT_01071();
                frmIn frm = new frmIn();
                rep.ngay1.Value = ngayy.Count > 0 ? (ngayy[0].Day + "/" + ngayy[0].Month) : null;
                rep.ngay2.Value = ngayy.Count > 1 ? (ngayy[1].Day + "/" + ngayy[1].Month) : null;
                rep.ngay3.Value = ngayy.Count > 2 ? (ngayy[2].Day + "/" + ngayy[2].Month) : null;
                rep.ngay4.Value = ngayy.Count > 3 ? (ngayy[3].Day + "/" + ngayy[3].Month) : null;
                rep.ngay5.Value = ngayy.Count > 4 ? (ngayy[4].Day + "/" + ngayy[4].Month) : null;
                rep.ngay6.Value = ngayy.Count > 5 ? (ngayy[5].Day + "/" + ngayy[5].Month) : null;
                rep.ngay7.Value = ngayy.Count > 6 ? (ngayy[6].Day + "/" + ngayy[6].Month) : null;
                rep.ngay8.Value = ngayy.Count > 7 ? (ngayy[7].Day + "/" + ngayy[7].Month) : null;
                rep.ngay9.Value = ngayy.Count > 8 ? (ngayy[8].Day + "/" + ngayy[8].Month) : null;
                rep.ngay10.Value = ngayy.Count > 9 ? (ngayy[9].Day + "/" + ngayy[9].Month) : null;
                rep.ngay11.Value = ngayy.Count > 10 ? (ngayy[10].Day + "/" + ngayy[10].Month) : null;
                rep.ngay12.Value = ngayy.Count > 11 ? (ngayy[11].Day + "/" + ngayy[11].Month) : null;
                rep.ngay13.Value = ngayy.Count > 12 ? (ngayy[12].Day + "/" + ngayy[12].Month) : null;
                rep.ngay14.Value = ngayy.Count > 13 ? (ngayy[13].Day + "/" + ngayy[13].Month) : null;
                rep.ngay15.Value = ngayy.Count > 14 ? (ngayy[14].Day + "/" + ngayy[14].Month) : null;
                rep.ngay16.Value = ngayy.Count > 15 ? (ngayy[15].Day + "/" + ngayy[15].Month) : null;
                rep.ngay17.Value = ngayy.Count > 16 ? (ngayy[16].Day + "/" + ngayy[16].Month) : null;
                rep.ngay18.Value = ngayy.Count > 17 ? (ngayy[17].Day + "/" + ngayy[17].Month) : null;
                rep.ngay19.Value = ngayy.Count > 18 ? (ngayy[18].Day + "/" + ngayy[18].Month) : null;
                rep.ngay20.Value = ngayy.Count > 19 ? (ngayy[19].Day + "/" + ngayy[19].Month) : null;
                rep.ngay21.Value = ngayy.Count > 20 ? (ngayy[20].Day + "/" + ngayy[20].Month) : null;
                rep.ngay22.Value = ngayy.Count > 21 ? (ngayy[21].Day + "/" + ngayy[21].Month) : null;
                rep.ngay23.Value = ngayy.Count > 22 ? (ngayy[22].Day + "/" + ngayy[22].Month) : null;
                rep.ngay24.Value = ngayy.Count > 23 ? (ngayy[23].Day + "/" + ngayy[23].Month) : null;
                rep.ngay25.Value = ngayy.Count > 24 ? (ngayy[24].Day + "/" + ngayy[24].Month) : null;
                rep.ngay26.Value = ngayy.Count > 25 ? (ngayy[25].Day + "/" + ngayy[25].Month) : null;
                rep.ngay27.Value = ngayy.Count > 26 ? (ngayy[26].Day + "/" + ngayy[26].Month) : null;
                rep.ngay28.Value = ngayy.Count > 27 ? (ngayy[27].Day + "/" + ngayy[27].Month) : null;
                rep.ngay29.Value = ngayy.Count > 28 ? (ngayy[28].Day + "/" + ngayy[28].Month) : null;
                rep.ngay30.Value = ngayy.Count > 29 ? (ngayy[29].Day + "/" + ngayy[29].Month) : null;

                var truongkhoa = data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN);
                if (truongkhoa.Count() > 0)
                {
                    rep.TruongKhoa.Value = truongkhoa.First().TruongKhoa;
                }

                var kp = (from a in _db.BNKBs.Where(p => p.MaBNhan == _mbn)
                          join b in _db.KPhongs on a.MaKP equals b.MaKP
                          select new { a, b }).ToList();
                var khoa_24012 = (from x in kp
                                  join y in _Kphong.Where(p => p.chon == true).Where(p => p.makp != 0)
                                  on x.b.MaKP equals y.makp
                                  select new { x.a, x.b }).ToList();
                var khoa = (from a in _db.BNKBs.Where(p => p.MaBNhan == _mbn)
                            join b in _db.KPhongs on a.MaKP equals b.MaKP
                            select new { a, b }).ToList();
                rep.Khoa.Value = string.Join(", ", _Kphong.Where(p => p.makp != 0).Where(p => p.chon == true).Select(p => p.tenkp));//khoa.First().b.TenKP;

                if (khoa.Count > 0)
                {
                    rep.SoBuong.Value = string.Join("; ", khoa.Where(p => !string.IsNullOrEmpty(p.a.Buong)).Select(p => p.a.Buong).Distinct());//khoa.First().a.Buong;
                    rep.SoGiuong.Value = string.Join("; ", khoa.Where(p => !string.IsNullOrEmpty(p.a.Giuong)).Select(p => p.a.Giuong).Distinct()); //khoa.First().a.Giuong;
                    rep.ChanDoan.Value = DungChung.Ham.FreshString(string.Join("; ", DungChung.Bien.MaBV == "24012" ? khoa_24012.Select(p => DungChung.Ham.FreshString(p.a.ChanDoan)) : khoa.Select(p => DungChung.Ham.FreshString(p.a.ChanDoan))) + "; " + string.Join("; ", khoa.Select(p => DungChung.Ham.FreshString(p.a.BenhKhac)))); // DungChung.Ham.FreshString(khoa.First().a.ChanDoan + "; " + khoa.First().a.BenhKhac);
                }
                var vv = (from a in _db.BenhNhans.Where(p => p.MaBNhan == mabn)
                          join b in _db.VaoViens on a.MaBNhan equals b.MaBNhan
                          select new { a.MaBNhan, a.TenBNhan, a.NgaySinh, a.GTinh, b.NgayVao, a.ThangSinh, a.NamSinh }).ToList();
                if (vv.Count > 0)
                {
                    rep.TenBN.Value = vv.First().TenBNhan.ToUpper();
                    rep.NgaySinh.Value = vv.First().NgaySinh + "/" + vv.First().ThangSinh + "/" + vv.First().NamSinh;
                    rep.NgayVV.Value = vv.First().NgayVao;
                    rep.Nam.Value = vv.First().GTinh == 1 ? "X" : "";
                    rep.Nu.Value = vv.First().GTinh == 0 ? "X" : "";
                }
                if (DungChung.Bien.MaBV == "27183")
                {
                    var qBN = _db.BenhNhans.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                    if (qBN != null)
                    {
                        if (qBN.NoiTru == 0)
                            rep.xrTableCell4.Text = "KHÁM, CHỮA BỆNH NGOẠI TRÚ";
                    }
                }
                rep.DataSource = _abc.OrderBy(p => p.St).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else if (_mau == 2)
            {
                _DichVu.Clear();
                foreach (var a in ds1)
                {
                    DV themmoi = new DV();
                    themmoi.TenDV = a.TenDV;
                    themmoi.TenRG = a.TenRG;
                    themmoi.MaDV = a.MaDV;
                    themmoi.IDNhom = a.IDNhom;
                    themmoi.DonVi = a.DonVi;
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        string cd = data.ChiDinhs.Where(p => p.IDCD == a.IDCD).Select(p => p.ChiDinh1).FirstOrDefault();
                        themmoi.ChiDinh = cd;
                    }
                    _DichVu.Add(themmoi);
                }
                if (DungChung.Bien.MaBV == "24012")
                {
                    foreach (var a in dv_24012)
                    {
                        DV themmoi = new DV();
                        themmoi.TenDV = a.TenDV;
                        themmoi.TenRG = a.TenRG;
                        themmoi.MaDV = a.MaDV;
                        themmoi.IDNhom = a.IDNhom;
                        themmoi.DonVi = a.DonVi;
                        themmoi.ChiDinh = a.ChiDinh1;
                        _DichVu.Add(themmoi);
                    }
                }

                #region Lấy Báo cáo

                bool DTNT;
                frmIn frm = new frmIn();
                BaoCao.Rep_CongKhaiDVKT_24012 rep = new BaoCao.Rep_CongKhaiDVKT_24012();
                BaoCao.Rep_CongKhaiDVKT_NoiTru_To2_24012 rep1 = new BaoCao.Rep_CongKhaiDVKT_NoiTru_To2_24012();

                #region TTBN

                rep.Parameters["TenCQCQ"].Value = DungChung.Bien.TenCQCQ.ToUpper();
                rep.Parameters["TenCQ"].Value = DungChung.Bien.TenCQ.ToUpper();
                rep1.Parameters["TenCQCQ"].Value = DungChung.Bien.TenCQCQ.ToUpper();
                rep1.Parameters["TenCQ"].Value = DungChung.Bien.TenCQ.ToUpper();
                if (DungChung.Bien.MaBV == "24012")
                {
                    DTNT = _db.BenhNhans.Where(p => p.MaBNhan == mabn).Select(p => p.DTNT).First(); //true: dieu tri ngoai tru, false: dieu tri noi tru
                    if (DTNT == false)
                    {
                        rep.Parameters["TieuDeNoiTru"].Value = "PHIẾU CÔNG KHAI DỊCH VỤ KHÁM, CHỮA BỆNH NỘI TRÚ";
                        rep1.Parameters["TieuDeNoiTru"].Value = "PHIẾU CÔNG KHAI DỊCH VỤ KHÁM, CHỮA BỆNH NỘI TRÚ";
                    }
                    else
                    {
                        rep.Parameters["TieuDeNoiTru"].Value = "PHIẾU CÔNG KHAI DỊCH VỤ KHÁM, CHỮA BỆNH NGOẠI TRÚ";
                        rep1.Parameters["TieuDeNoiTru"].Value = "PHIẾU CÔNG KHAI DỊCH VỤ KHÁM, CHỮA BỆNH NGOẠI TRÚ";
                    }
                }
                List<DV> _list = new List<DV>();
                List<DV> _Report = new List<DV>();
                _list = _DichVu.ToList();
                var kp = (from a in _db.BNKBs.Where(p => p.MaBNhan == mabn)
                          join b in _db.KPhongs on a.MaKP equals b.MaKP
                          select new { a, b }).ToList();
                var khoa_24012 = (from x in kp
                                  join y in _Kphong.Where(p => p.chon == true).Where(p => p.makp != 0)
                                  on x.b.MaKP equals y.makp
                                  select new { x.a, x.b }).ToList();

                var khoa = (from a in _db.BNKBs.Where(p => p.MaBNhan == _mbn)
                            join b in _db.KPhongs on a.MaKP equals b.MaKP
                            select new { a, b }).ToList();
                if (khoa.Count > 0)
                {
                    rep.Parameters["Buong"].Value = string.Join("; ", khoa.Where(p => !string.IsNullOrEmpty(p.a.Buong)).Select(p => p.a.Buong).Distinct());
                    rep.Parameters["Giuong"].Value = string.Join("; ", khoa.Where(p => !string.IsNullOrEmpty(p.a.Giuong)).Select(p => p.a.Giuong).Distinct());
                    rep.Parameters["CD"].Value = string.Join("; ", DungChung.Ham.FreshString(string.Join("; ", DungChung.Bien.MaBV == "24012" ? khoa_24012.Select(p => DungChung.Ham.FreshString(p.a.ChanDoan)) : khoa_24012.Select(p => DungChung.Ham.FreshString(p.a.ChanDoan))) + "; " + string.Join("; ", khoa_24012.Select(p => DungChung.Ham.FreshString(p.a.BenhKhac)))));
                    rep1.Parameters["Buong"].Value = string.Join("; ", khoa.Where(p => !string.IsNullOrEmpty(p.a.Buong)).Select(p => p.a.Buong).Distinct());
                    rep1.Parameters["Giuong"].Value = string.Join("; ", khoa.Where(p => !string.IsNullOrEmpty(p.a.Giuong)).Select(p => p.a.Giuong).Distinct());
                    rep1.Parameters["CD"].Value = string.Join("; ", DungChung.Ham.FreshString(string.Join("; ", DungChung.Bien.MaBV == "24012" ? khoa_24012.Select(p => DungChung.Ham.FreshString(p.a.ChanDoan)) : khoa_24012.Select(p => DungChung.Ham.FreshString(p.a.ChanDoan))) + "; " + string.Join("; ", khoa_24012.Select(p => DungChung.Ham.FreshString(p.a.BenhKhac)))));
                }
                var vv = (from a in _db.BenhNhans.Where(p => p.MaBNhan == mabn)
                          join b in _db.VaoViens on a.MaBNhan equals b.MaBNhan
                          select new { a.MaBNhan, a.TenBNhan, a.NgaySinh, a.GTinh, b.NgayVao, a.ThangSinh, a.NamSinh, a.MaKP, a.SThe }).ToList();
                if (vv.Count > 0)
                {
                    rep.Parameters["TenBNhan"].Value = vv.First().TenBNhan.ToUpper();
                    rep.Parameters["NgSinh"].Value = vv.First().NgaySinh + "/" + vv.First().ThangSinh + "/" + vv.First().NamSinh;
                    rep.Parameters["NgVao"].Value = DungChung.Ham.NgaySangChu(Convert.ToDateTime(vv.First().NgayVao), 7);
                    rep.Parameters["GioiTinh"].Value = vv.First().GTinh == 1 ? "Nam" : "Nữ";
                    rep.Parameters["Khoa"].Value = string.Join(", ", _Kphong.Where(p => p.makp != 0).Where(p => p.chon == true).Select(p => p.tenkp));
                    rep1.Parameters["TenBNhan"].Value = vv.First().TenBNhan.ToUpper();
                    rep1.Parameters["NgSinh"].Value = vv.First().NgaySinh + "/" + vv.First().ThangSinh + "/" + vv.First().NamSinh;
                    rep1.Parameters["NgVao"].Value = DungChung.Ham.NgaySangChu(Convert.ToDateTime(vv.First().NgayVao), 7);
                    rep1.Parameters["GioiTinh"].Value = vv.First().GTinh == 1 ? "Nam" : "Nữ";
                    rep1.Parameters["Khoa"].Value = string.Join(", ", _Kphong.Where(p => p.makp != 0).Where(p => p.chon == true).Select(p => p.tenkp));
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        rep.Parameters["MaBHYT"].Value = vv.First().SThe.ToUpper();
                        rep1.Parameters["MaBHYT"].Value = vv.First().SThe.ToUpper();
                    }

                    #region Ngày

                    for (int j = 0; j < 25; j++)// ds 25 cot ngay
                    {
                        int v = j + 1;
                        DateTime day1 = n1.AddDays(j);
                        rep.Parameters["NgayVV_" + v].Value = day1.Day + " / " + day1.Month;
                        rep1.Parameters["NgayVV_" + v].Value = day1.Day + " / " + day1.Month;
                    }

                    #endregion Ngày

                    #region Xét Nghiệm

                    var _ListXetNghiem = (from l in _list
                                          select new
                                          {
                                              l.Chon,
                                              l.IDNhom,
                                              l.MaDV,
                                              l.SapXep,
                                              l.TenDV,
                                              l.TenNhom,
                                              l.TenRG,
                                              l.DonVi,
                                          }).Where(p => p.IDNhom == 1).Distinct().ToList();
                    string tenNhomXN = "Xét nghiệm";
                    for (int i = 0; i < 16; i++) // ds 16 hang xet nghiem
                    {
                        DV tt = new DV();
                        int v = i + 1;
                        if (_ListXetNghiem.Count > i)
                        {
                            if (_ListXetNghiem[i].TenRG != "")
                            {
                                rep.Parameters["XetNghiem_" + v].Value = _ListXetNghiem[i].TenRG;
                                tt.TenRG = _ListXetNghiem[i].TenRG;
                            }
                            else
                            {
                                rep.Parameters["XetNghiem_" + v].Value = _ListXetNghiem[i].TenDV;
                                tt.TenDV = _ListXetNghiem[i].TenDV;
                            }

                            rep.Parameters["DonViXetNghiem_" + v].Value = _ListXetNghiem[i].DonVi;
                            tt.DonVi = _ListXetNghiem[i].DonVi;
                        }
                        else
                        {
                            rep.Parameters["XetNghiem_" + v].Value = "";
                            tt.TenRG = "";
                            rep.Parameters["DonViXetNghiem_" + v].Value = "";
                            tt.DonVi = "";
                            tt.TenDV = "";
                        }

                        tt.SapXep = "I";
                        tt.TenNhom = tenNhomXN;
                        _Report.Add(tt);
                    }

                    #endregion Xét Nghiệm

                    #region CDHA

                    var _ListCDHA = (from l in _list
                                     select new
                                     {
                                         l.Chon,
                                         l.IDNhom,
                                         l.MaDV,
                                         l.SapXep,
                                         l.TenRG,
                                         l.TenNhom,
                                         l.DonVi,
                                         l.TenDV,
                                     }).Where(p => p.IDNhom == 2).Distinct().ToList();
                    string tenNhomCDHA = "Chẩn đoán hình ảnh";
                    for (int i = 0; i < 6; i++) // ds 6 hàng Chẩn đoán hình ảnh
                    {
                        DV tt = new DV();
                        int v = i + 1;
                        if (_ListCDHA.Count > i)
                        {
                            if (_ListCDHA[i].TenRG != "")
                            {
                                rep.Parameters["CDHA_" + v].Value = _ListCDHA[i].TenRG;
                                tt.TenRG = _ListCDHA[i].TenRG;
                            }
                            else
                            {
                                rep.Parameters["CDHA_" + v].Value = _ListCDHA[i].TenDV;
                                tt.TenDV = _ListCDHA[i].TenDV;
                            }
                            rep.Parameters["DonViCDHA_" + v].Value = _ListCDHA[i].DonVi;
                            tt.DonVi = _ListCDHA[i].DonVi;
                        }
                        else
                        {
                            rep.Parameters["CDHA_" + v].Value = "";
                            tt.TenRG = "";
                            rep.Parameters["DonViCDHA_" + v].Value = "";
                            tt.DonVi = "";
                            tt.TenDV = "";
                        }

                        tt.SapXep = "II";
                        tt.TenNhom = tenNhomCDHA;
                        _Report.Add(tt);
                    }

                    #endregion CDHA

                    #region TDCN

                    var _ListTDCN = (from l in _list
                                     select new
                                     {
                                         l.Chon,
                                         l.IDNhom,
                                         l.MaDV,
                                         l.SapXep,
                                         l.TenDV,
                                         l.TenRG,
                                         l.TenNhom,
                                         l.DonVi
                                     }).Where(p => p.IDNhom == 3).Distinct().ToList();
                    string tenNhomTDCN = "Thăm dò chức năng";
                    for (int i = 0; i < 6; i++) // ds 6 hàng thăm dò chức năng
                    {
                        DV tt = new DV();
                        int v = i + 1;
                        if (_ListTDCN.Count > i)
                        {
                            if (_ListTDCN[i].TenRG != "")
                            {
                                rep.Parameters["TDCN_" + v].Value = _ListTDCN[i].TenRG;
                                tt.TenRG = _ListTDCN[i].TenRG;
                            }
                            else
                            {
                                rep.Parameters["TDCN_" + v].Value = _ListTDCN[i].TenDV;
                                tt.TenDV = _ListTDCN[i].TenDV;
                            }
                            rep.Parameters["DonViTDCN_" + v].Value = _ListTDCN[i].DonVi;
                            tt.DonVi = _ListTDCN[i].DonVi;
                        }
                        else
                        {
                            rep.Parameters["TDCN_" + v].Value = "";
                            tt.TenRG = "";
                            rep.Parameters["DonViTDCN_" + v].Value = "";
                            tt.DonVi = "";
                            tt.TenDV = "";
                        }

                        tt.SapXep = "III";
                        tt.TenNhom = tenNhomTDCN;
                        _Report.Add(tt);
                    }

                    #endregion TDCN

                    #region DVKT

                    var _ListDVKT = (from l in _list
                                     select new
                                     {
                                         l.Chon,
                                         l.IDNhom,
                                         l.MaDV,
                                         l.SapXep,
                                         l.TenDV,
                                         l.TenRG,
                                         l.TenNhom,
                                         l.DonVi,
                                         l.ChiDinh
                                     }).Where(p => p.IDNhom == 8 || p.IDNhom == 9).Distinct().ToList();
                    string tenNhomDVKT = "Dịch vị kỹ thuật";
                    for (int i = 0; i < 12; i++) // ds 12 hàng DV kỹ thuật
                    {
                        DV tt = new DV();
                        int v = i + 1;
                        if (_ListDVKT.Count > i)
                        {
                            if (_ListDVKT[i].TenRG != "")
                            {
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    rep1.Parameters["DVKT_" + v].Value = _ListDVKT[i].TenRG + " (" + _ListDVKT[i].ChiDinh + ")";
                                }
                                else
                                    rep1.Parameters["DVKT_" + v].Value = _ListDVKT[i].TenRG;

                                tt.TenRG = _ListDVKT[i].TenRG;
                            }
                            else
                            {
                                rep1.Parameters["DVKT_" + v].Value = _ListDVKT[i].TenDV;
                                tt.TenDV = _ListDVKT[i].TenDV;
                            }
                            rep1.Parameters["DonViDVKT_" + v].Value = _ListDVKT[i].DonVi;
                            tt.DonVi = _ListDVKT[i].DonVi;
                        }
                        else
                        {
                            rep1.Parameters["DVKT_" + v].Value = "";
                            tt.TenRG = "";
                            rep1.Parameters["DonViDVKT_" + v].Value = "";
                            tt.DonVi = "";
                            tt.TenDV = "";
                        }
                        tt.SapXep = "IV";
                        tt.TenNhom = tenNhomDVKT;
                        _Report.Add(tt);
                    }

                    #endregion DVKT

                    #region Thuốc

                    var _ListThuoc = (from l in _list
                                      select new
                                      {
                                          l.Chon,
                                          l.IDNhom,
                                          l.MaDV,
                                          l.SapXep,
                                          l.TenDV,
                                          l.TenRG,
                                          l.TenNhom,
                                          l.DonVi
                                      }).Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6).Distinct().ToList();

                    string tenNhomThuoc = "Thuốc, dịch truyền(tên, nồng độ, hàm lượng)";
                    for (int i = 0; i < 12; i++) // ds 12 hàng thuốc, dịch truyền
                    {
                        DV tt = new DV();
                        int v = i + 1;
                        if (_ListThuoc.Count > i)
                        {
                            if (_ListThuoc[i].TenRG != "")
                            {
                                rep1.Parameters["Thuoc_" + v].Value = _ListThuoc[i].TenRG;
                                tt.TenRG = _ListThuoc[i].TenRG;
                            }
                            else
                            {
                                rep1.Parameters["Thuoc_" + v].Value = _ListThuoc[i].TenDV;
                                tt.TenDV = _ListThuoc[i].TenDV;
                            }
                            rep1.Parameters["DonViThuoc_" + v].Value = _ListThuoc[i].DonVi;
                            tt.DonVi = _ListThuoc[i].DonVi;
                        }
                        else
                        {
                            rep1.Parameters["Thuoc_" + v].Value = "";
                            tt.TenRG = "";
                            rep1.Parameters["DonViThuoc_" + v].Value = "";
                            tt.DonVi = "";
                            tt.TenDV = "";
                        }

                        tt.SapXep = "V";
                        tt.TenNhom = tenNhomThuoc;
                        _Report.Add(tt);
                    }

                    #endregion Thuốc

                    #region VTYT

                    var _ListVTYT = (from l in _list
                                     select new
                                     {
                                         l.Chon,
                                         l.IDNhom,
                                         l.MaDV,
                                         l.SapXep,
                                         l.TenDV,
                                         l.TenRG,
                                         l.TenNhom,
                                         l.DonVi
                                     }).Where(p => p.IDNhom == 10 || p.IDNhom == 11).Distinct().ToList();
                    string tenNhomVTYT = "VTYT (không có trong DVKT)";
                    for (int i = 0; i < 6; i++) // ds 6 hàng vtyt
                    {
                        DV tt = new DV();
                        int v = i + 1;
                        if (_ListVTYT.Count > i)
                        {
                            if (_ListVTYT[i].TenRG != "")
                            {
                                rep1.Parameters["VTYT_" + v].Value = _ListThuoc[i].TenRG;
                                tt.TenRG = _ListVTYT[i].TenRG;
                            }
                            else
                            {
                                rep1.Parameters["VTYT_" + v].Value = _ListVTYT[i].TenDV;
                                tt.TenDV = _ListVTYT[i].TenDV;
                            }
                            rep1.Parameters["DonViVTYT_" + v].Value = _ListVTYT[i].DonVi;
                            tt.DonVi = _ListVTYT[i].DonVi;
                        }
                        else
                        {
                            rep1.Parameters["VTYT_" + v].Value = "";
                            tt.TenRG = "";
                            rep1.Parameters["DonViVTYT_" + v].Value = "";
                            tt.DonVi = "";
                            tt.TenDV = "";
                        }

                        tt.SapXep = "VI";
                        tt.TenNhom = tenNhomVTYT;
                        _Report.Add(tt);
                    }

                    #endregion VTYT
                }

                #endregion TTBN

                if (DungChung.Bien.MaBV == "24012")
                {
                    rep.CreateDocument();
                    rep1.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }

                #endregion Lấy Báo cáo
            }
        }

        private void simpleButton21_Click(object sender, EventArgs e, double thuoc)
        {
        }

        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string _maCQCQ = "";

        private void Frm_ChonKP_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194")
            {
                Frm_ChonKP size = new Frm_ChonKP();
                size.Size = new Size(462, 290);
                radBN.Visible = true;
            }
            //_mbn = "73860";
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qCQCQ = _data.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (qCQCQ != null)
                _maCQCQ = qCQCQ.MaChuQuan;
            var kp1 = (from dt in data.DThuocs.Where(p => p.MaBNhan == _mbn)
                       join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                       select new { dtct.MaKP }).ToList();
            var kp2 = (from dt in kp1
                          join kp in data.KPhongs on dt.MaKP equals kp.MaKP
                          select kp ).ToList();
            var kphong = (from kp in kp2
                          //where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          group new { kp } by new { kp.MaKP, kp.TenKP } into kq
                          select new { TenKP = kq.Key.TenKP, MaKP = kq.Key.MaKP }).ToList();
            //
            var kp1_24012 = (from cd in data.ChiDinhs
                                join cls in data.CLS on cd.IdCLS equals cls.IdCLS
                                where cls.MaBNhan == _mbn && cd.Status == 0
                                select new { cls.MaKP }).ToList();
            //
            var kp2_24012 = (from cd in kp1_24012
                             join kp in data.KPhongs on cd.MaKP equals kp.MaKP
                                select kp).ToList();

            var kphong_24012 = (from kp in kp2_24012
                                group new { kp } by new { kp.MaKP, kp.TenKP } into kq
                                select new { TenKP = kq.Key.TenKP, MaKP = kq.Key.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = false;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = false;
                    _Kphong.Add(themmoi);
                }
                if (DungChung.Bien.MaBV == "24012" && _mau == 2)
                {
                    foreach (var a in kphong_24012)
                    {
                        if (!kphong.Contains(a))
                        {
                            KPhong themmoi = new KPhong();
                            themmoi.tenkp = a.TenKP;
                            themmoi.makp = a.MaKP;
                            themmoi.chon = false;
                            _Kphong.Add(themmoi);
                        }
                    }
                }
                grcKP.DataSource = _Kphong.ToList();
            }
        }

        private void radBN_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void grvKP_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKP.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKP.GetFocusedRowCellValue("tenkp").ToString();
                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKP.DataSource = "";
                        grcKP.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
    }
}