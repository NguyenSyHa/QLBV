using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_ThHoaSinhMauSL : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ThHoaSinhMauSL()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

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
        List<KPhong> _Kphong = new List<KPhong>();
        private void Frm_ThHoaSinhMauSL_Load(object sender, EventArgs e)
        {
            LupNgaytu.DateTime = System.DateTime.Now;
            LupNgayden.DateTime = System.DateTime.Now;
            _Kphong.Clear();
            var kphong = (from kp in _Data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP == null ? 0 : Convert.ToInt32(a.MaKP);
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }
        private class SHM
        {
            private string Tendvct;
            private string Madvct;
            private string MaBNhan;
             public string tendvct
            { set { Tendvct = value; } get { return Tendvct; } }
            public string madvct
            { set { Madvct = value; } get { return Madvct; } }
            public string mabnhan
            { set { MaBNhan = value; } get { return MaBNhan; } }
        }
        private class BenhNhan
        {
            private string TenBN;
            private int MaBN;
            private string Gioitinh;
            private int Tuoi;
            private string Diachi;
            private string Chandoan;
            private string Noigui;
            private string Yeucau;
            private int MaKP;
            private string BHYT;
            private string Ngaythang;
            private string MaDVct;
            private string SL1; private string SL2; private string SL3; private string SL4; private string SL5; private string SL6; private string SL7; private string SL8; private string SL9;
            private string SL10; private string SL11; private string SL12; private string SL13; private string SL14; private string SL15; private string SL16; private string SL17; private string SL18; private string SL19; private string SL20;
            private string SL21; private string SL22; private string SL23; private string SL24; private string SL25; private string SL26; private string SL27; private string SL28; private string SL29; private string SL30; 
            public string ngaythang
            {set { Ngaythang = value; }get { return Ngaythang; }}
            public string bhyt
            {set { BHYT = value; }get { return BHYT; }}
            public int maBN
            {set { MaBN = value; }get { return MaBN; }}
            public string tenBN
            {set { TenBN = value; }get { return TenBN; }}
            public string gioitinh
            {set { Gioitinh = value; }get { return Gioitinh; }}
            public int tuoi
            {set { Tuoi = value; }get { return Tuoi; }}
            public string diachi
            {set { Diachi = value; }get { return Diachi; }}
            public string chandoan
            {set { Chandoan = value; }get { return Chandoan; }}
            public string noigui
            {set { Noigui = value; }get { return Noigui; }}
            public int makp
            {set { MaKP = value; }get { return MaKP; }}
            public string madvct
            { set { MaDVct = value; } get { return MaDVct; } }
            public string sl1
            { set { SL1 = value; } get { return SL1; } }
            public string sl2
            { set { SL2 = value; } get { return SL2; } }
            public string sl3
            { set { SL3 = value; } get { return SL3; } }
            public string sl4
            { set { SL4 = value; } get { return SL4; } }
            public string sl5
            { set { SL5 = value; } get { return SL5; } }
            public string sl6
            { set { SL6 = value; } get { return SL6; } }
            public string sl7
            { set { SL7 = value; } get { return SL7; } }
            public string sl8
            { set { SL8 = value; } get { return SL8; } }
            public string sl9
            { set { SL9 = value; } get { return SL9; } }
             public string sl10
            { set { SL10 = value; } get { return SL10; } }
             public string sl11
            { set { SL11 = value; } get { return SL11; } }
             public string sl12
            { set { SL12 = value; } get { return SL12; } }
             public string sl13
             { set { SL13 = value; } get { return SL13; } }
             public string sl14
             { set { SL14 = value; } get { return SL14; } }
             public string sl15
             { set { SL15 = value; } get { return SL15; } }
             public string sl16
             { set { SL16 = value; } get { return SL16; } }
             public string sl17
             { set { SL17 = value; } get { return SL17; } }
             public string sl18
             { set { SL18 = value; } get { return SL18; } }
             public string sl19
             { set { SL19 = value; } get { return SL19; } }
             public string sl20
             { set { SL20 = value; } get { return SL20; } }
             public string sl21
             { set { SL21 = value; } get { return SL21; } }
             public string sl22
             { set { SL22 = value; } get { return SL22; } }
             public string sl23
             { set { SL23 = value; } get { return SL23; } }
             public string sl24
             { set { SL24 = value; } get { return SL24; } }
             public string sl25
             { set { SL25 = value; } get { return SL25; } }
             public string sl26
             { set { SL26 = value; } get { return SL26; } }
             public string sl27
             { set { SL27 = value; } get { return SL27; } }
             public string sl28
             { set { SL28 = value; } get { return SL28; } }
             public string sl29
             { set { SL29 = value; } get { return SL29; } }
             public string sl30
             { set { SL30 = value; } get { return SL30; } }
             private int idcls;
             public int IDCLS
             { set { idcls = value; } get { return idcls; } }
        }
        List<BenhNhan> _BenhNhan = new List<BenhNhan>();
        List<SHM> _SHM = new List<SHM>();

        private void butTaoBC_Click(object sender, EventArgs e)
        {
            _BenhNhan.Clear();
            _SHM.Clear();
            DateTime NT = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime ND = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            string _tenbc = "";
            if (cmbInBC.EditValue != null)
            {
                _tenbc = cmbInBC.EditValue.ToString();
            }
            if (cmbBN.EditValue == "Tất cả")
            {
                if (cboTT.SelectedIndex == 1)// đã thanh toán, lấy theo ngày thanh toán
                {

                    var dvct1 = (from chidinh in _Data.ChiDinhs
                                 join cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND) on chidinh.IdCLS equals cls.IdCLS
                                 join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                 
                                 join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                 join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                                 join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                 group new { cls, dvct, clsct } by new { dvct.MaDVct, dvct.TenDVct, dvct.STT } into kq
                                 select new { TenDVct = kq.Key.TenDVct, MaDVct = kq.Key.MaDVct, STT = kq.Key.STT }).OrderBy(p => p.STT).ToList();

                    if (dvct1.Count > 0)
                    {
                        foreach (var a in dvct1)
                        {
                            SHM themmoi = new SHM();
                            themmoi.tendvct = a.TenDVct;
                            themmoi.madvct = a.MaDVct;
                            _SHM.Add(themmoi);
                        }
                    }

                    var qbn5 = (from cls in _Data.CLS
                                join bn in _Data.BenhNhans.Where(p=>p.NoiTru==1||p.NoiTru==0) on cls.MaBNhan equals bn.MaBNhan
                               join cd in _Data.ChiDinhs.Where(p=>p.Status==1) on cls.IdCLS equals cd.IdCLS
                                join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD
                               join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
                               join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                               join VP in _Data.VienPhis.Where(p => p.NgayTT >= NT).Where(p => p.NgayTT <= ND) on cls.MaBNhan equals VP.MaBNhan
                               join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                               where (from vp in _Data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
                                //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                               group new { bn, cls } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, cls.MaKP, kp.TenKP, cls.IdCLS } into kq
                               select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP=kq.Key.MaKP, IDcls=kq.Key.IdCLS }).ToList();

                    var qbn= (from bn in qbn5
                             join kp in _Kphong.Where(p=>p.chon==true) on bn.MaKP equals kp.makp
                             group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, bn.MaKP, bn.TenKP, bn.IDcls } into kq
                              select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP = kq.Key.MaKP, IDcls = kq.Key.IDcls }).ToList();

                    if (qbn.Count > 0)
                    {
                        foreach (var a in qbn)
                        {
                            BenhNhan themmoi = new BenhNhan();
                            themmoi.maBN = a.MaBNhan;
                            themmoi.tenBN = a.TenBNhan;
                            if (a.GTinh == 1)
                            {
                                themmoi.gioitinh = "Nam";
                            }
                            else { themmoi.gioitinh = "Nữ"; }

                            themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                            if (a.DTuong == "BHYT")
                            {
                                themmoi.bhyt = "X";
                            }
                            else { themmoi.bhyt = ""; }
                            themmoi.diachi = a.DChi;
                            themmoi.noigui = a.TenKP;
                            themmoi.IDCLS = a.IDcls;
                            //themmoi.makp = a.MaKP;
                            _BenhNhan.Add(themmoi);
                        }

                        var qcd = (from cls in _Data.CLS
                                   join VP in _Data.VienPhis.Where(p => p.NgayTT >= NT).Where(p => p.NgayTT <= ND) on cls.MaBNhan equals VP.MaBNhan
                                   join bnkb in _Data.BNKBs on cls.MaBNhan equals bnkb.MaBNhan
                                   select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                        if (qcd.Count > 0)
                        {
                            foreach (var a in qcd)
                            {
                                foreach (var b in _BenhNhan)
                                {
                                    if (a.MaBNhan == b.maBN)
                                    {
                                            b.chandoan = a.ChanDoan.ToString();
                                    }
                                }
                            }
                        }
                    }
                    var hoasinh5 = (from chidinh in _Data.ChiDinhs
                                   join cls in _Data.CLS on chidinh.IdCLS equals cls.IdCLS
                                   join VP in _Data.VienPhis.Where(p => p.NgayTT >= NT).Where(p => p.NgayTT <= ND) on cls.MaBNhan equals VP.MaBNhan
                                   join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                   join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                   join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                                   join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                   //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                                   group new { cls, dvct, clsct } by new { cls.MaBNhan, dvct.MaDVct, cls.MaKP, cls.IdCLS} into kq
                                   select new { MaBN = kq.Key.MaBNhan, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.clsct.Id).Count(), MaKP=kq.Key.MaKP, id=kq.Key.IdCLS }).ToList();

                    var hoasinh = (from hs in hoasinh5
                                  join kp in _Kphong.Where(p=>p.chon==true) on hs.MaKP equals kp.makp
                                  group new { hs } by new { hs.MaBN, hs.MaDVct, hs.id} into kq
                                   select new { MaBN = kq.Key.MaBN, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.hs.KetQua).Count(), id=kq.Key.id}).ToList();

                    if (hoasinh.Count > 0)
                    {
                        foreach (var a in _BenhNhan)
                        {
                            foreach (var b in hoasinh)
                            {
                                if (a.maBN == b.MaBN && a.IDCLS==b.id)
                                {
                                    for (int i = 0; i < _SHM.Count; i++)
                                    {
                                        if (b.MaDVct == _SHM.Skip(i).First().madvct)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    a.sl1 = b.KetQua.ToString();
                                                    break;
                                                case 1:
                                                    a.sl2 = b.KetQua.ToString();
                                                    break;
                                                case 2:
                                                    a.sl3 = b.KetQua.ToString();
                                                    break;
                                                case 3:
                                                    a.sl4 = b.KetQua.ToString();
                                                    break;
                                                case 4:
                                                    a.sl5 = b.KetQua.ToString();
                                                    break;
                                                case 5:
                                                    a.sl6 = b.KetQua.ToString();
                                                    break;
                                                case 6:
                                                    a.sl7 = b.KetQua.ToString();
                                                    break;
                                                case 7:
                                                    a.sl8 = b.KetQua.ToString();
                                                    break;
                                                case 8:
                                                    a.sl9 = b.KetQua.ToString();
                                                    break;
                                                case 9:
                                                    a.sl10 = b.KetQua.ToString();
                                                    break;
                                                case 10:
                                                    a.sl11 = b.KetQua.ToString();
                                                    break;
                                                case 11:
                                                    a.sl12 = b.KetQua.ToString();
                                                    break;
                                                case 12:
                                                    a.sl13 = b.KetQua.ToString();
                                                    break;
                                                case 13:
                                                    a.sl14 = b.KetQua.ToString();
                                                    break;
                                                case 14:
                                                    a.sl15 = b.KetQua.ToString();
                                                    break;
                                                case 16:
                                                    a.sl17 = b.KetQua.ToString();
                                                    break;
                                                case 17:
                                                    a.sl18 = b.KetQua.ToString();
                                                    break;
                                                case 18:
                                                    a.sl19 = b.KetQua.ToString();
                                                    break;
                                                case 19:
                                                    a.sl20 = b.KetQua.ToString();
                                                    break;
                                               case 20:
                                                    a.sl21 = b.KetQua.ToString();
                                                    break;
                                               case 21:
                                                    a.sl22 = b.KetQua.ToString();
                                                    break;
                                               case 22:
                                                    a.sl23 = b.KetQua.ToString();
                                                    break;
                                               case 23:
                                                    a.sl24 = b.KetQua.ToString();
                                                    break;
                                               case 24:
                                                    a.sl25 = b.KetQua.ToString();
                                                    break;
                                               case 25:
                                                    a.sl26 = b.KetQua.ToString();
                                                    break;
                                               case 26:
                                                    a.sl27 = b.KetQua.ToString();
                                                    break;
                                               case 27:
                                                    a.sl28 = b.KetQua.ToString();
                                                    break;
                                               case 28:
                                                    a.sl29 = b.KetQua.ToString();
                                                    break;
                                               case 29:
                                                    a.sl30 = b.KetQua.ToString();
                                                    break;
                                         
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }

                    BaoCao.Rep_ThHoaSinhMauSL rep = new BaoCao.Rep_ThHoaSinhMauSL();
                    frmIn frm = new frmIn();
                    rep.TenBC.Value = ("Thống kê số lượt thực hiện " + _tenbc).ToUpper();
                    rep.TuNgayDenNgay.Value = "Từ ngày " + NT.ToString().Substring(0,10) + " đến ngày " + ND.ToString().Substring(0,10);
                    rep.TuNgay.Value = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
                    rep.DenNgay.Value = DungChung.Ham.NgayTu(LupNgayden.DateTime);

                    for (int i = 0; i < _SHM.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM1.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 1:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM2.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 2:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM3.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 3:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM4.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 4:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM5.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 5:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM6.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 6:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM7.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 7:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM8.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 8:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM9.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 9:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM10.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 10:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM11.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 11:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM12.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 12:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM13.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 13:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM14.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 14:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM15.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 15:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM16.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 16:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM17.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 17:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM18.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 18:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM19.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 19:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM20.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 20:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM21.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 21:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM22.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 22:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM23.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 23:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM24.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 24:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM25.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 25:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM26.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 26:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM27.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 27:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM28.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 28:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM29.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                             case 29:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM30.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            
                        }

                    }


                    rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang);
                    rep.BindingData();
                    rep.CreateDocument();
                    _BenhNhan.Clear();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }

            
            if (cboTT.SelectedIndex == 0)// chưa thanh toán, lấy theo ngày thực hiện
            {

                var dvct1 = (from chidinh in _Data.ChiDinhs
                             join cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND) on chidinh.IdCLS equals cls.IdCLS
                             join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                             
                             join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                             join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                             join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                             group new { cls, dvct, clsct } by new { dvct.MaDVct, dvct.TenDVct, dvct.STT } into kq
                             select new { TenDVct = kq.Key.TenDVct, MaDVct = kq.Key.MaDVct, STT = kq.Key.STT }).OrderBy(p => p.STT).ToList();

                if (dvct1.Count > 0)
                {
                    foreach (var a in dvct1)
                    {
                        SHM themmoi = new SHM();
                        themmoi.tendvct = a.TenDVct;
                        themmoi.madvct = a.MaDVct;
                        _SHM.Add(themmoi);
                    }
                }

                var qbn5 = (from bn in _Data.BenhNhans
                            join cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND) on bn.MaBNhan equals cls.MaBNhan
                            join cd in _Data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                           join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
                           join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                           join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                          
                           //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                           group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, cls.MaKP, kp.TenKP, cls.IdCLS } into kq
                           select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP=kq.Key.MaKP, idcls=kq.Key.IdCLS }).ToList();
                var qbn= (from bn in qbn5
                         join kp in _Kphong.Where(p=>p.chon==true) on bn.MaKP equals kp.makp
                         group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, bn.MaKP, bn.TenKP, bn.idcls } into kq
                           select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP=kq.Key.MaKP, IDcls=kq.Key.idcls }).ToList();

                if (qbn.Count > 0)
                {
                    foreach (var a in qbn)
                    {
                        BenhNhan themmoi = new BenhNhan();
                        themmoi.maBN = a.MaBNhan;
                        themmoi.tenBN = a.TenBNhan;
                        if (a.GTinh == 1)
                        {
                            themmoi.gioitinh = "Nam";
                        }
                        else { themmoi.gioitinh = "Nữ"; }

                        themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                        if (a.DTuong == "BHYT")
                        {
                            themmoi.bhyt = "X";
                        }
                        else { themmoi.bhyt = ""; }
                        themmoi.diachi = a.DChi;
                        themmoi.noigui = a.TenKP;
                        themmoi.IDCLS = a.IDcls;
                         _BenhNhan.Add(themmoi);
                    }

                    var qcd = (from cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND)
                               join bnkb in _Data.BNKBs on cls.MaBNhan equals bnkb.MaBNhan
                               select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                    if (qcd.Count > 0)
                    {
                        foreach (var a in qcd)
                        {
                            foreach (var b in _BenhNhan)
                            {
                                if (a.MaBNhan == b.maBN)
                                {
                                    
                                        b.chandoan = a.ChanDoan.ToString();
                                    
                                }
                            }
                        }
                    }
                }
                var hoasinh5 = (from chidinh in _Data.ChiDinhs
                                join cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND) on chidinh.IdCLS equals cls.IdCLS
                               join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                               
                               join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                               join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                               join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                               //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                               group new { cls, dvct, clsct } by new { cls.MaBNhan, dvct.MaDVct, cls.MaKP, cls.IdCLS } into kq
                               select new { MaBN = kq.Key.MaBNhan, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.clsct.Id).Count(), MaKP=kq.Key.MaKP, ID=kq.Key.IdCLS }).ToList();

                var hoasinh = (from hs in hoasinh5
                               join kp in _Kphong.Where(p => p.chon == true) on hs.MaKP equals kp.makp
                               group new { hs } by new { hs.MaBN, hs.MaDVct, hs.ID } into kq
                               select new { MaBN = kq.Key.MaBN, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.hs.KetQua).Count(), id=kq.Key.ID }).ToList();
                if (hoasinh.Count > 0)
                {
                    foreach (var a in _BenhNhan)
                    {
                        foreach (var b in hoasinh)
                        {
                            if (a.maBN == b.MaBN&&a.IDCLS==b.id)
                            {
                                for (int i = 0; i < _SHM.Count; i++)
                                {
                                    if (b.MaDVct == _SHM.Skip(i).First().madvct)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                a.sl1 = b.KetQua.ToString();
                                                break;
                                            case 1:
                                                a.sl2 = b.KetQua.ToString();
                                                break;
                                            case 2:
                                                a.sl3 = b.KetQua.ToString();
                                                break;
                                            case 3:
                                                a.sl4 = b.KetQua.ToString();
                                                break;
                                            case 4:
                                                a.sl5 = b.KetQua.ToString();
                                                break;
                                            case 5:
                                                a.sl6 = b.KetQua.ToString();
                                                break;
                                            case 6:
                                                a.sl7 = b.KetQua.ToString();
                                                break;
                                            case 7:
                                                a.sl8 = b.KetQua.ToString();
                                                break;
                                            case 8:
                                                a.sl9 = b.KetQua.ToString();
                                                break;
                                            case 9:
                                                a.sl10 = b.KetQua.ToString();
                                                break;
                                            case 10:
                                                a.sl11 = b.KetQua.ToString();
                                                break;
                                            case 11:
                                                a.sl12 = b.KetQua.ToString();
                                                break;
                                            case 12:
                                                a.sl13 = b.KetQua.ToString();
                                                break;
                                            case 13:
                                                a.sl14 = b.KetQua.ToString();
                                                break;
                                            case 14:
                                                a.sl15 = b.KetQua.ToString();
                                                break;
                                            case 16:
                                                a.sl17 = b.KetQua.ToString();
                                                break;
                                            case 17:
                                                a.sl18 = b.KetQua.ToString();
                                                break;
                                            case 18:
                                                a.sl19 = b.KetQua.ToString();
                                                break;
                                            case 19:
                                                a.sl20 = b.KetQua.ToString();
                                                break;
                                            case 20:
                                                a.sl21 = b.KetQua.ToString();
                                                break;
                                            case 21:
                                                a.sl22 = b.KetQua.ToString();
                                                break;
                                            case 22:
                                                a.sl23 = b.KetQua.ToString();
                                                break;
                                            case 23:
                                                a.sl24 = b.KetQua.ToString();
                                                break;
                                            case 24:
                                                a.sl25 = b.KetQua.ToString();
                                                break;
                                            case 25:
                                                a.sl26 = b.KetQua.ToString();
                                                break;
                                            case 26:
                                                a.sl27 = b.KetQua.ToString();
                                                break;
                                            case 27:
                                                a.sl28 = b.KetQua.ToString();
                                                break;
                                            case 28:
                                                a.sl29 = b.KetQua.ToString();
                                                break;
                                            case 29:
                                                a.sl30 = b.KetQua.ToString();
                                                break;

                                        }
                                        //}
                                    }
                                }
                            }
                        }
                    }

                }

                BaoCao.Rep_ThHoaSinhMauSL rep = new BaoCao.Rep_ThHoaSinhMauSL();
                frmIn frm = new frmIn();
                rep.TenBC.Value = ("Thống kê số lượt thực hiện " + _tenbc).ToUpper();
                rep.TuNgayDenNgay.Value = "Từ ngày " + NT.ToString().Substring(0, 10) + " đến ngày " + ND.ToString().Substring(0, 10);
                rep.TuNgay.Value = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
                rep.DenNgay.Value = DungChung.Ham.NgayTu(LupNgayden.DateTime);

                for (int i = 0; i < _SHM.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM1.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 1:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM2.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 2:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM3.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 3:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM4.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 4:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM5.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 5:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM6.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 6:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM7.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 7:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM8.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 8:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM9.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 9:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM10.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 10:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM11.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 11:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM12.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 12:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM13.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 13:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM14.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 14:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM15.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 15:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM16.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 16:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM17.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 17:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM18.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 18:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM19.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 19:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM20.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 20:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM21.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 21:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM22.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 22:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM23.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 23:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM24.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 24:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM25.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 25:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM26.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 26:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM27.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 27:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM28.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 28:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM29.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                        case 29:
                            if (_SHM.Skip(i).First().tendvct != null)
                            { rep.SHM30.Value = _SHM.Skip(i).First().tendvct; }
                            break;
                    }

                }


                rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang);
                rep.BindingData();
                rep.CreateDocument();
                _BenhNhan.Clear();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
            //if (cboTT.SelectedIndex == 2)// cả hai
            //{

            //    var dvct1 = (from chidinh in _Data.ChiDinhs
            //                 join cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND) on chidinh.IdCLS equals cls.IdCLS
            //                 join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
            //                 join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
            //                 join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
            //                 join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
            //                 group new { cls, dvct, clsct } by new { dvct.MaDVct, dvct.TenDVct, dvct.STT } into kq
            //                 select new { TenDVct = kq.Key.TenDVct, MaDVct = kq.Key.MaDVct, STT = kq.Key.STT }).OrderBy(p => p.STT).ToList();

            //    if (dvct1.Count > 0)
            //    {
            //        foreach (var a in dvct1)
            //        {
            //            SHM themmoi = new SHM();
            //            themmoi.tendvct = a.TenDVct;
            //            themmoi.madvct = a.MaDVct;
            //           _SHM.Add(themmoi);
            //        }
            //    }

            //    var qbn = (from bn in _Data.BenhNhans
            //               join cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND) on bn.MaBNhan equals cls.MaBNhan
            //               join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
            //               join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
            //               join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
            //               join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
            //               //where !(from vp in _Data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
            //               where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
            //               group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, cls.MaKP, kp.TenKP } into kq
            //               select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan }).ToList();


            //    if (qbn.Count > 0)
            //    {
            //        foreach (var a in qbn)
            //        {
            //            BenhNhan themmoi = new BenhNhan();
            //            themmoi.maBN = a.MaBNhan;
            //            themmoi.tenBN = a.TenBNhan;
            //            if (a.GTinh == 1)
            //            {
            //                themmoi.gioitinh = "Nam";
            //            }
            //            else { themmoi.gioitinh = "Nữ"; }

            //            themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
            //            if (a.DTuong == "BHYT")
            //            {
            //                themmoi.bhyt = "X";
            //            }
            //            else { themmoi.bhyt = ""; }
            //            themmoi.diachi = a.DChi;
            //            themmoi.noigui = a.TenKP;
            //            //themmoi.makp = a.MaKP;
            //            _BenhNhan.Add(themmoi);
            //        }

            //        var qcd = (from cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND)
            //                   join bnkb in _Data.BNKBs on cls.MaBNhan equals bnkb.MaBNhan
            //                   select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
            //        if (qcd.Count > 0)
            //        {
            //            foreach (var a in qcd)
            //            {
            //                foreach (var b in _BenhNhan)
            //                {
            //                    if (a.MaBNhan == b.maBN)
            //                    {
            //                        if (a.MaKP == b.makp)
            //                        {
            //                            b.chandoan = a.ChanDoan.ToString();
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    var hoasinh = (from chidinh in _Data.ChiDinhs
            //                   join cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND) on chidinh.IdCLS equals cls.IdCLS
            //                   join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
            //                   join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
            //                   join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
            //                   join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
            //                   where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
            //                   group new { cls, dvct, clsct } by new { cls.MaBNhan, dvct.MaDVct } into kq
            //                   select new { MaBN = kq.Key.MaBNhan, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.clsct.Id).Count() }).ToList();
            //    if (hoasinh.Count > 0)
            //    {
            //        foreach (var a in _BenhNhan)
            //        {
            //            foreach (var b in hoasinh)
            //            {
            //                if (a.maBN == b.MaBN)
            //                {
            //                    for (int i = 0; i < _SHM.Count; i++)
            //                    {
            //                        if (b.MaDVct == _SHM.Skip(i).First().madvct)
            //                        {
            //                            switch (i)
            //                            {
            //                                case 0:
            //                                    a.sl1 = b.KetQua.ToString();
            //                                    break;
            //                                case 1:
            //                                    a.sl2 = b.KetQua.ToString();
            //                                    break;
            //                                case 2:
            //                                    a.sl3 = b.KetQua.ToString();
            //                                    break;
            //                                case 3:
            //                                    a.sl4 = b.KetQua.ToString();
            //                                    break;
            //                                case 4:
            //                                    a.sl5 = b.KetQua.ToString();
            //                                    break;
            //                                case 5:
            //                                    a.sl6 = b.KetQua.ToString();
            //                                    break;
            //                                case 6:
            //                                    a.sl7 = b.KetQua.ToString();
            //                                    break;
            //                                case 7:
            //                                    a.sl8 = b.KetQua.ToString();
            //                                    break;
            //                                case 8:
            //                                    a.sl9 = b.KetQua.ToString();
            //                                    break;
            //                                case 9:
            //                                    a.sl10 = b.KetQua.ToString();
            //                                    break;
            //                                case 10:
            //                                    a.sl11 = b.KetQua.ToString();
            //                                    break;
            //                                case 11:
            //                                    a.sl12 = b.KetQua.ToString();
            //                                    break;
            //                                case 12:
            //                                    a.sl13 = b.KetQua.ToString();
            //                                    break;
            //                                case 13:
            //                                    a.sl14 = b.KetQua.ToString();
            //                                    break;
            //                                case 14:
            //                                    a.sl15 = b.KetQua.ToString();
            //                                    break;
            //                                case 16:
            //                                    a.sl17 = b.KetQua.ToString();
            //                                    break;
            //                                case 17:
            //                                    a.sl18 = b.KetQua.ToString();
            //                                    break;
            //                                case 18:
            //                                    a.sl19 = b.KetQua.ToString();
            //                                    break;
            //                                case 19:
            //                                    a.sl20 = b.KetQua.ToString();
            //                                    break;
            //                                case 20:
            //                                    a.sl21 = b.KetQua.ToString();
            //                                    break;
            //                                case 21:
            //                                    a.sl22 = b.KetQua.ToString();
            //                                    break;
            //                                case 22:
            //                                    a.sl23 = b.KetQua.ToString();
            //                                    break;
            //                                case 23:
            //                                    a.sl24 = b.KetQua.ToString();
            //                                    break;
            //                                case 24:
            //                                    a.sl25 = b.KetQua.ToString();
            //                                    break;
            //                                case 25:
            //                                    a.sl26 = b.KetQua.ToString();
            //                                    break;
            //                                case 26:
            //                                    a.sl27 = b.KetQua.ToString();
            //                                    break;
            //                                case 27:
            //                                    a.sl28 = b.KetQua.ToString();
            //                                    break;
            //                                case 28:
            //                                    a.sl29 = b.KetQua.ToString();
            //                                    break;
            //                                case 29:
            //                                    a.sl30 = b.KetQua.ToString();
            //                                    break;

            //                            }
            //                            //}
            //                        }
            //                    }
            //                }
            //            }
            //        }

            //    }

            //    BaoCao.Rep_ThHoaSinhMauSL rep = new BaoCao.Rep_ThHoaSinhMauSL();
            //    frmIn frm = new frmIn();
            //    rep.TenBC.Value = ("Thống kê số lượt thực hiện " + _tenbc).ToUpper();
            //    rep.TuNgayDenNgay.Value = "Từ ngày " + NT.ToString().Substring(0, 10) + " đến ngày " + ND.ToString().Substring(0, 10);
            //    rep.TuNgay.Value = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            //    rep.DenNgay.Value = DungChung.Ham.NgayTu(LupNgayden.DateTime);

            //    for (int i = 0; i < _SHM.Count; i++)
            //    {
            //        switch (i)
            //        {
            //            case 0:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM1.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 1:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM2.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 2:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM3.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 3:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM4.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 4:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM5.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 5:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM6.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 6:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM7.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 7:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM8.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 8:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM9.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 9:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM10.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 10:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM11.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 11:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM12.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 12:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM13.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 13:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM14.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 14:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM15.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 15:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM16.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 16:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM17.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 17:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM18.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 18:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM19.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 19:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM20.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 20:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM21.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 21:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM22.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 22:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM23.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 23:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM24.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 24:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM25.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 25:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM26.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 26:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM27.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 27:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM28.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 28:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM29.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //            case 29:
            //                if (_SHM.Skip(i).First().tendvct != null)
            //                { rep.SHM30.Value = _SHM.Skip(i).First().tendvct; }
            //                break;
            //        }

            //    }


            //    rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang);
            //    rep.BindingData();
            //    rep.CreateDocument();
            //    _BenhNhan.Clear();
            //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //    frm.ShowDialog();

            //}
        }
            int sbn = 0;
            if (cmbBN.EditValue != "Tất cả")
            {
                if (cmbBN.EditValue == "Ngoại trú")
                {
                    sbn = 0;
                }
                if (cmbBN.EditValue == "Nội trú")
                {
                    sbn = 1;
                }

                if (cboTT.SelectedIndex == 1)// đã thanh toán
                {

                    var dvct1 = (from chidinh in _Data.ChiDinhs
                                 join cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND) on chidinh.IdCLS equals cls.IdCLS
                                 join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                 join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                 join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                                 //join vp in _Data.VienPhis.Where(p => p.NgayTT >= NT).Where(p => p.NgayTT <= ND) on cls.MaKP equals vp.MaBNhan
                                 join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                 group new { cls, dvct, clsct } by new { dvct.MaDVct, dvct.TenDVct, dvct.STT } into kq
                                 select new { TenDVct = kq.Key.TenDVct, MaDVct = kq.Key.MaDVct, STT = kq.Key.STT }).OrderBy(p => p.STT).ToList();

                    if (dvct1.Count > 0)
                    {
                        foreach (var a in dvct1)
                        {
                            SHM themmoi = new SHM();
                            themmoi.tendvct = a.TenDVct;
                            themmoi.madvct = a.MaDVct;
                            //  themmoi.mabnhan = a.MaBNhan;
                            _SHM.Add(themmoi);
                        }
                    }

                    var qbn5 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == sbn)
                               join cls in _Data.CLS on bn.MaBNhan equals cls.MaBNhan
                               join cd in _Data.ChiDinhs.Where(p=>p.Status==1) on cls.IdCLS equals cd.IdCLS
                               join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
                               join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                               join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                               join vp1 in _Data.VienPhis.Where(p=>p.NgayTT<=ND).Where(p=>p.NgayTT>=NT) on cls.MaBNhan equals vp1.MaBNhan
                               where (from vp in _Data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
                               //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                               group new { bn, cls } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, cls.MaKP, kp.TenKP, cls.IdCLS } into kq
                                select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP=kq.Key.MaKP, idcls=kq.Key.IdCLS }).ToList();


                    //var qbn5 = (from cls in _Data.CLS
                    //            join bn in _Data.BenhNhans.Where(p => p.NoiTru == 1 || p.NoiTru == 0) on cls.MaBNhan equals bn.MaBNhan
                    //            join cd in _Data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                    //            join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD
                    //            join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
                    //            join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                    //            join VP in _Data.VienPhis.Where(p => p.NgayTT >= NT).Where(p => p.NgayTT <= ND) on cls.MaBNhan equals VP.MaBNhan
                    //            join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                    //            where (from vp in _Data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
                    //            //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                    //            group new { bn, cls } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, cls.MaKP, kp.TenKP, cls.IdCLS } into kq
                    //            select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP = kq.Key.MaKP, IDcls = kq.Key.IdCLS }).ToList();
                    var qbn = (from bn in qbn5
                               join kp in _Kphong.Where(p => p.chon == true) on bn.MaKP equals kp.makp
                               group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, bn.MaKP, bn.TenKP, bn.idcls } into kq
                               select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP = kq.Key.MaKP, idcls=kq.Key.idcls }).ToList();
                    if (qbn.Count > 0)
                    {
                        foreach (var a in qbn)
                        {
                            BenhNhan themmoi = new BenhNhan();
                            themmoi.maBN = a.MaBNhan;
                            themmoi.tenBN = a.TenBNhan;
                            if (a.GTinh == 1)
                            {
                                themmoi.gioitinh = "Nam";
                            }
                            else { themmoi.gioitinh = "Nữ"; }

                            themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                            if (a.DTuong == "BHYT")
                            {
                                themmoi.bhyt = "X";
                            }
                            else { themmoi.bhyt = ""; }
                            themmoi.diachi = a.DChi;
                            themmoi.noigui = a.TenKP;
                            themmoi.IDCLS = a.idcls;
                            //themmoi.makp = a.MaKP;
                            _BenhNhan.Add(themmoi);
                        }

                        var qcd = (from cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND)
                                   join bnkb in _Data.BNKBs on cls.MaBNhan equals bnkb.MaBNhan
                                   select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                        if (qcd.Count > 0)
                        {
                            foreach (var a in qcd)
                            {
                                foreach (var b in _BenhNhan)
                                {
                                    if (a.MaBNhan == b.maBN)
                                    {
                                            b.chandoan = a.ChanDoan.ToString();
                                    }
                                }
                            }
                        }
                    }
                    var hoasinh5 = (from chidinh in _Data.ChiDinhs
                                    join cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND) on chidinh.IdCLS equals cls.IdCLS
                                   join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                   
                                   join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                   join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                                   join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                   //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                                   group new { cls, dvct, clsct } by new { cls.MaBNhan, dvct.MaDVct, cls.MaKP, cls.IdCLS} into kq
                                    select new { MaBN = kq.Key.MaBNhan, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.clsct.Id).Count(), MaKP=kq.Key.MaKP, id=kq.Key.IdCLS }).ToList();
                    var hoasinh = (from hs in hoasinh5
                                   join kp in _Kphong.Where(p => p.chon == true) on hs.MaKP equals kp.makp
                                   group new { hs } by new { hs.MaBN, hs.MaDVct, hs.id } into kq
                                   select new { MaBN = kq.Key.MaBN, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.hs.KetQua).Count(), id=kq.Key.id }).ToList();
                    if (hoasinh.Count > 0)
                    {
                        foreach (var a in _BenhNhan)
                        {
                            foreach (var b in hoasinh)
                            {
                                if (a.maBN == b.MaBN && a.IDCLS==b.id)
                                {
                                    for (int i = 0; i < _SHM.Count; i++)
                                    {
                                        if (b.MaDVct == _SHM.Skip(i).First().madvct)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    a.sl1 = b.KetQua.ToString();
                                                    break;
                                                case 1:
                                                    a.sl2 = b.KetQua.ToString();
                                                    break;
                                                case 2:
                                                    a.sl3 = b.KetQua.ToString();
                                                    break;
                                                case 3:
                                                    a.sl4 = b.KetQua.ToString();
                                                    break;
                                                case 4:
                                                    a.sl5 = b.KetQua.ToString();
                                                    break;
                                                case 5:
                                                    a.sl6 = b.KetQua.ToString();
                                                    break;
                                                case 6:
                                                    a.sl7 = b.KetQua.ToString();
                                                    break;
                                                case 7:
                                                    a.sl8 = b.KetQua.ToString();
                                                    break;
                                                case 8:
                                                    a.sl9 = b.KetQua.ToString();
                                                    break;
                                                case 9:
                                                    a.sl10 = b.KetQua.ToString();
                                                    break;
                                                case 10:
                                                    a.sl11 = b.KetQua.ToString();
                                                    break;
                                                case 11:
                                                    a.sl12 = b.KetQua.ToString();
                                                    break;
                                                case 12:
                                                    a.sl13 = b.KetQua.ToString();
                                                    break;
                                                case 13:
                                                    a.sl14 = b.KetQua.ToString();
                                                    break;
                                                case 14:
                                                    a.sl15 = b.KetQua.ToString();
                                                    break;
                                                case 16:
                                                    a.sl17 = b.KetQua.ToString();
                                                    break;
                                                case 17:
                                                    a.sl18 = b.KetQua.ToString();
                                                    break;
                                                case 18:
                                                    a.sl19 = b.KetQua.ToString();
                                                    break;
                                                case 19:
                                                    a.sl20 = b.KetQua.ToString();
                                                    break;
                                                case 20:
                                                    a.sl21 = b.KetQua.ToString();
                                                    break;
                                                case 21:
                                                    a.sl22 = b.KetQua.ToString();
                                                    break;
                                                case 22:
                                                    a.sl23 = b.KetQua.ToString();
                                                    break;
                                                case 23:
                                                    a.sl24 = b.KetQua.ToString();
                                                    break;
                                                case 24:
                                                    a.sl25 = b.KetQua.ToString();
                                                    break;
                                                case 25:
                                                    a.sl26 = b.KetQua.ToString();
                                                    break;
                                                case 26:
                                                    a.sl27 = b.KetQua.ToString();
                                                    break;
                                                case 27:
                                                    a.sl28 = b.KetQua.ToString();
                                                    break;
                                                case 28:
                                                    a.sl29 = b.KetQua.ToString();
                                                    break;
                                                case 29:
                                                    a.sl30 = b.KetQua.ToString();
                                                    break;
                                            }
                                            //}
                                        }
                                    }
                                }
                            }
                        }

                    }

                    BaoCao.Rep_ThHoaSinhMauSL rep = new BaoCao.Rep_ThHoaSinhMauSL();
                    frmIn frm = new frmIn();
                    rep.TenBC.Value = ("Thống kê số lượt thực hiện " + _tenbc).ToUpper();
                    rep.TuNgayDenNgay.Value = "Từ ngày " + NT.ToString().Substring(0, 10) + " đến ngày " + ND.ToString().Substring(0, 10);
                    rep.TuNgay.Value = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
                    rep.DenNgay.Value = DungChung.Ham.NgayTu(LupNgayden.DateTime);

                    for (int i = 0; i < _SHM.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM1.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 1:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM2.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 2:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM3.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 3:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM4.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 4:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM5.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 5:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM6.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 6:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM7.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 7:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM8.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 8:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM9.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 9:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM10.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 10:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM11.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 11:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM12.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 12:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM13.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 13:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM14.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 14:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM15.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 15:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM16.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 16:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM17.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 17:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM18.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 18:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM19.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 19:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM20.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 20:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM21.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 21:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM22.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 22:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM23.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 23:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM24.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 24:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM25.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 25:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM26.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 26:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM27.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 27:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM28.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 28:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM29.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 29:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM30.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                        }

                    }


                    rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang);
                    rep.BindingData();
                    rep.CreateDocument();
                    _BenhNhan.Clear();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }


                if (cboTT.SelectedIndex == 0)// chưa thanh toán
                {

                    var dvct1 = (from chidinh in _Data.ChiDinhs
                                 join cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND) on chidinh.IdCLS equals cls.IdCLS
                                 join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                 
                                 join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                 join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                                 join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                 group new { cls, dvct, clsct } by new { dvct.MaDVct, dvct.TenDVct, dvct.STT } into kq
                                 select new { TenDVct = kq.Key.TenDVct, MaDVct = kq.Key.MaDVct, STT = kq.Key.STT }).OrderBy(p => p.STT).ToList();

                    if (dvct1.Count > 0)
                    {
                        foreach (var a in dvct1)
                        {
                            SHM themmoi = new SHM();
                            themmoi.tendvct = a.TenDVct;
                            themmoi.madvct = a.MaDVct;
                            //  themmoi.mabnhan = a.MaBNhan;
                            _SHM.Add(themmoi);
                        }
                    }

                    var qbn5 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == sbn)
                               join cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND) on bn.MaBNhan equals cls.MaBNhan
                               join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                               join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
                               join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                               join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                               //where !(from vp in _Data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
                               //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                               group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, cls.MaKP, kp.TenKP, cls.IdCLS } into kq
                                select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP=kq.Key.MaKP, idcls=kq.Key.IdCLS }).ToList();

                    var qbn = (from bn in qbn5
                               join kp in _Kphong.Where(p => p.chon == true) on bn.MaKP equals kp.makp
                               group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, bn.MaKP, bn.TenKP, bn.idcls } into kq
                               select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP = kq.Key.MaKP, idcls=kq.Key.idcls }).ToList();
                    if (qbn.Count > 0)
                    {
                        foreach (var a in qbn)
                        {
                            BenhNhan themmoi = new BenhNhan();
                            themmoi.maBN = a.MaBNhan;
                            themmoi.tenBN = a.TenBNhan;
                            if (a.GTinh == 1)
                            {
                                themmoi.gioitinh = "Nam";
                            }
                            else { themmoi.gioitinh = "Nữ"; }

                            themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                            if (a.DTuong == "BHYT")
                            {
                                themmoi.bhyt = "X";
                            }
                            else { themmoi.bhyt = ""; }
                            themmoi.diachi = a.DChi;
                            themmoi.noigui = a.TenKP;
                            themmoi.IDCLS = a.idcls;
                            //themmoi.makp = a.MaKP;
                            _BenhNhan.Add(themmoi);
                        }

                        var qcd = (from cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND)
                                   join bnkb in _Data.BNKBs on cls.MaBNhan equals bnkb.MaBNhan
                                   select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                        if (qcd.Count > 0)
                        {
                            foreach (var a in qcd)
                            {
                                foreach (var b in _BenhNhan)
                                {
                                    if (a.MaBNhan == b.maBN)
                                    {
                                        
                                            b.chandoan = a.ChanDoan.ToString();
                                        
                                    }
                                }
                            }
                        }
                    }
                    var hoasinh5 = (from chidinh in _Data.ChiDinhs
                                    join cls in _Data.CLS.Where(p => p.NgayTH >= NT).Where(p => p.NgayTH <= ND) on chidinh.IdCLS equals cls.IdCLS
                                   join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                   
                                   join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                   join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                                   join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                   //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                                   group new { cls, dvct, clsct } by new { cls.MaBNhan, dvct.MaDVct, cls.MaKP, cls.IdCLS} into kq
                                    select new { MaBN = kq.Key.MaBNhan, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.clsct.Id).Count(), MaKP=kq.Key.MaKP, id=kq.Key.IdCLS }).ToList();

                    var hoasinh = (from hs in hoasinh5
                                   join kp in _Kphong.Where(p => p.chon == true) on hs.MaKP equals kp.makp
                                   group new { hs } by new { hs.MaBN, hs.MaDVct, hs.id } into kq
                                   select new { MaBN = kq.Key.MaBN, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.hs.KetQua).Count(), id=kq.Key.id }).ToList();
                    if (hoasinh.Count > 0)
                    {
                        foreach (var a in _BenhNhan)
                        {
                            foreach (var b in hoasinh)
                            {
                                if (a.maBN == b.MaBN&&a.IDCLS==b.id)
                                {
                                    for (int i = 0; i < _SHM.Count; i++)
                                    {
                                        if (b.MaDVct == _SHM.Skip(i).First().madvct)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    a.sl1 = b.KetQua.ToString();
                                                    break;
                                                case 1:
                                                    a.sl2 = b.KetQua.ToString();
                                                    break;
                                                case 2:
                                                    a.sl3 = b.KetQua.ToString();
                                                    break;
                                                case 3:
                                                    a.sl4 = b.KetQua.ToString();
                                                    break;
                                                case 4:
                                                    a.sl5 = b.KetQua.ToString();
                                                    break;
                                                case 5:
                                                    a.sl6 = b.KetQua.ToString();
                                                    break;
                                                case 6:
                                                    a.sl7 = b.KetQua.ToString();
                                                    break;
                                                case 7:
                                                    a.sl8 = b.KetQua.ToString();
                                                    break;
                                                case 8:
                                                    a.sl9 = b.KetQua.ToString();
                                                    break;
                                                case 9:
                                                    a.sl10 = b.KetQua.ToString();
                                                    break;
                                                case 10:
                                                    a.sl11 = b.KetQua.ToString();
                                                    break;
                                                case 11:
                                                    a.sl12 = b.KetQua.ToString();
                                                    break;
                                                case 12:
                                                    a.sl13 = b.KetQua.ToString();
                                                    break;
                                                case 13:
                                                    a.sl14 = b.KetQua.ToString();
                                                    break;
                                                case 14:
                                                    a.sl15 = b.KetQua.ToString();
                                                    break;
                                                case 16:
                                                    a.sl17 = b.KetQua.ToString();
                                                    break;
                                                case 17:
                                                    a.sl18 = b.KetQua.ToString();
                                                    break;
                                                case 18:
                                                    a.sl19 = b.KetQua.ToString();
                                                    break;
                                                case 19:
                                                    a.sl20 = b.KetQua.ToString();
                                                    break;
                                                case 20:
                                                    a.sl21 = b.KetQua.ToString();
                                                    break;
                                                case 21:
                                                    a.sl22 = b.KetQua.ToString();
                                                    break;
                                                case 22:
                                                    a.sl23 = b.KetQua.ToString();
                                                    break;
                                                case 23:
                                                    a.sl24 = b.KetQua.ToString();
                                                    break;
                                                case 24:
                                                    a.sl25 = b.KetQua.ToString();
                                                    break;
                                                case 25:
                                                    a.sl26 = b.KetQua.ToString();
                                                    break;
                                                case 26:
                                                    a.sl27 = b.KetQua.ToString();
                                                    break;
                                                case 27:
                                                    a.sl28 = b.KetQua.ToString();
                                                    break;
                                                case 28:
                                                    a.sl29 = b.KetQua.ToString();
                                                    break;
                                                case 29:
                                                    a.sl30 = b.KetQua.ToString();
                                                    break;
                                            }
                                            //}
                                        }
                                    }
                                }
                            }
                        }

                    }

                    BaoCao.Rep_ThHoaSinhMauSL rep = new BaoCao.Rep_ThHoaSinhMauSL();
                    frmIn frm = new frmIn();
                    rep.TenBC.Value = ("Thống kê số lượt thực hiện " + _tenbc).ToUpper();
                    rep.TuNgayDenNgay.Value = "Từ ngày " + NT.ToString().Substring(0, 10) + " đến ngày " + ND.ToString().Substring(0, 10);
                    rep.TuNgay.Value = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
                    rep.DenNgay.Value = DungChung.Ham.NgayTu(LupNgayden.DateTime);

                    for (int i = 0; i < _SHM.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM1.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 1:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM2.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 2:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM3.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 3:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM4.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 4:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM5.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 5:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM6.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 6:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM7.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 7:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM8.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 8:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM9.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 9:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM10.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 10:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM11.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 11:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM12.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 12:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM13.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 13:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM14.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 14:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM15.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 15:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM16.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 16:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM17.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 17:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM18.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 18:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM19.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 19:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM20.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 20:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM21.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 21:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM22.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 22:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM23.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 23:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM24.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 24:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM25.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 25:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM26.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 26:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM27.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 27:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM28.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 28:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM29.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                            case 29:
                                if (_SHM.Skip(i).First().tendvct != null)
                                { rep.SHM30.Value = _SHM.Skip(i).First().tendvct; }
                                break;
                        }

                    }


                    rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang);
                    rep.BindingData();
                    rep.CreateDocument();
                    _BenhNhan.Clear();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }
                //if (cboTT.SelectedIndex == 2)// cả hai
                //{

                //    var dvct1 = (from chidinh in _Data.ChiDinhs
                //                 join cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND) on chidinh.IdCLS equals cls.IdCLS
                //                 join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                 
                //                 join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                //                 join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                //                 join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                //                 group new { cls, dvct, clsct } by new { dvct.MaDVct, dvct.TenDVct, dvct.STT } into kq
                //                 select new { TenDVct = kq.Key.TenDVct, MaDVct = kq.Key.MaDVct, STT = kq.Key.STT }).OrderBy(p => p.STT).ToList();

                //    if (dvct1.Count > 0)
                //    {
                //        foreach (var a in dvct1)
                //        {
                //            SHM themmoi = new SHM();
                //            themmoi.tendvct = a.TenDVct;
                //            themmoi.madvct = a.MaDVct;
                //            //  themmoi.mabnhan = a.MaBNhan;
                //            _SHM.Add(themmoi);
                //        }
                //    }

                //    var qbn5 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == sbn)
                //               join cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND) on bn.MaBNhan equals cls.MaBNhan
                //               join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                //               join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
                //               join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                //               join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                //               //where !(from vp in _Data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
                //               //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                //               group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, cls.MaKP, kp.TenKP, cls.IdCLS } into kq
                //                select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP=kq.Key.MaKP, idcls=kq.Key.IdCLS }).ToList();

                //    var qbn = (from bn in qbn5
                //               join kp in _Kphong.Where(p => p.chon = true) on bn.MaKP equals kp.makp
                //               group new { bn } by new { bn.MaBNhan, bn.TenBNhan, bn.GTinh, bn.Tuoi, bn.DChi, bn.DTuong, bn.MaKP, bn.TenKP, bn.idcls } into kq
                //               select new { TenBNhan = kq.Key.TenBNhan, GTinh = kq.Key.GTinh, Tuoi = kq.Key.Tuoi, DChi = kq.Key.DChi, DTuong = kq.Key.DTuong, TenKP = kq.Key.TenKP, MaBNhan = kq.Key.MaBNhan, MaKP = kq.Key.MaKP, idcls=kq.Key.idcls }).ToList();
                //    if (qbn.Count > 0)
                //    {
                //        foreach (var a in qbn)
                //        {
                //            BenhNhan themmoi = new BenhNhan();
                //            themmoi.maBN = a.MaBNhan;
                //            themmoi.tenBN = a.TenBNhan;
                //            if (a.GTinh == 1)
                //            {
                //                themmoi.gioitinh = "Nam";
                //            }
                //            else { themmoi.gioitinh = "Nữ"; }

                //            themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                //            if (a.DTuong == "BHYT")
                //            {
                //                themmoi.bhyt = "X";
                //            }
                //            else { themmoi.bhyt = ""; }
                //            themmoi.diachi = a.DChi;
                //            themmoi.noigui = a.TenKP;
                //            themmoi.IDCLS = a.idcls;
                //            //themmoi.makp = a.MaKP;
                //            _BenhNhan.Add(themmoi);
                //        }

                //        var qcd = (from cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND)
                //                   join bnkb in _Data.BNKBs on cls.MaBNhan equals bnkb.MaBNhan
                //                   select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                //        if (qcd.Count > 0)
                //        {
                //            foreach (var a in qcd)
                //            {
                //                foreach (var b in _BenhNhan)
                //                {
                //                    if (a.MaBNhan == b.maBN)
                //                    {
                //                        if (a.MaKP == b.makp)
                //                        {
                //                            b.chandoan = a.ChanDoan.ToString();
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    var hoasinh5 = (from chidinh in _Data.ChiDinhs
                //                   join cls in _Data.CLS.Where(p => p.NgayThang >= NT).Where(p => p.NgayThang <= ND) on chidinh.IdCLS equals cls.IdCLS
                //                   join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                                  
                //                   join dvct in _Data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                //                   join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                //                   join tnhom in _Data.TieuNhomDVs.Where(p => p.TenRG.ToLower().Contains(_tenbc.ToLower())) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                //                   //where (cls.MaKP == _MaKP1 || cls.MaKP == _MaKP10 || cls.MaKP == _MaKP11 || cls.MaKP == _MaKP12 || cls.MaKP == _MaKP13 || cls.MaKP == _MaKP14 || cls.MaKP == _MaKP15 || cls.MaKP == _MaKP2 || cls.MaKP == _MaKP3 || cls.MaKP == _MaKP4 || cls.MaKP == _MaKP5 || cls.MaKP == _MaKP6 || cls.MaKP == _MaKP7 || cls.MaKP == _MaKP8 || cls.MaKP == _MaKP9)
                //                   group new { cls, dvct, clsct } by new { cls.MaBNhan, dvct.MaDVct, cls.MaKP, cls.IdCLS} into kq
                //                    select new { MaBN = kq.Key.MaBNhan, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.clsct.Id).Count(), MaKP=kq.Key.MaKP, id=kq.Key.IdCLS }).ToList();
                //    var hoasinh = (from hs in hoasinh5
                //                   join kp in _Kphong.Where(p => p.chon = true) on hs.MaKP equals kp.makp
                //                   group new { hs } by new { hs.MaBN, hs.MaDVct, hs.id } into kq
                //                   select new { MaBN = kq.Key.MaBN, MaDVct = kq.Key.MaDVct, KetQua = kq.Select(p => p.hs.KetQua).Count(), id=kq.Key.id }).ToList();
                //    if (hoasinh.Count > 0)
                //    {
                //        foreach (var a in _BenhNhan)
                //        {
                //            foreach (var b in hoasinh)
                //            {
                //                if (a.maBN == b.MaBN&&a.IDCLS==b.id)
                //                {
                //                    for (int i = 0; i < _SHM.Count; i++)
                //                    {
                //                        if (b.MaDVct == _SHM.Skip(i).First().madvct)
                //                        {
                //                            switch (i)
                //                            {
                //                                case 0:
                //                                    a.sl1 = b.KetQua.ToString();
                //                                    break;
                //                                case 1:
                //                                    a.sl2 = b.KetQua.ToString();
                //                                    break;
                //                                case 2:
                //                                    a.sl3 = b.KetQua.ToString();
                //                                    break;
                //                                case 3:
                //                                    a.sl4 = b.KetQua.ToString();
                //                                    break;
                //                                case 4:
                //                                    a.sl5 = b.KetQua.ToString();
                //                                    break;
                //                                case 5:
                //                                    a.sl6 = b.KetQua.ToString();
                //                                    break;
                //                                case 6:
                //                                    a.sl7 = b.KetQua.ToString();
                //                                    break;
                //                                case 7:
                //                                    a.sl8 = b.KetQua.ToString();
                //                                    break;
                //                                case 8:
                //                                    a.sl9 = b.KetQua.ToString();
                //                                    break;
                //                                case 9:
                //                                    a.sl10 = b.KetQua.ToString();
                //                                    break;
                //                                case 10:
                //                                    a.sl11 = b.KetQua.ToString();
                //                                    break;
                //                                case 11:
                //                                    a.sl12 = b.KetQua.ToString();
                //                                    break;
                //                                case 12:
                //                                    a.sl13 = b.KetQua.ToString();
                //                                    break;
                //                                case 13:
                //                                    a.sl14 = b.KetQua.ToString();
                //                                    break;
                //                                case 14:
                //                                    a.sl15 = b.KetQua.ToString();
                //                                    break;
                //                                case 16:
                //                                    a.sl17 = b.KetQua.ToString();
                //                                    break;
                //                                case 17:
                //                                    a.sl18 = b.KetQua.ToString();
                //                                    break;
                //                                case 18:
                //                                    a.sl19 = b.KetQua.ToString();
                //                                    break;
                //                                case 19:
                //                                    a.sl20 = b.KetQua.ToString();
                //                                    break;
                //                                case 20:
                //                                    a.sl21 = b.KetQua.ToString();
                //                                    break;
                //                                case 21:
                //                                    a.sl22 = b.KetQua.ToString();
                //                                    break;
                //                                case 22:
                //                                    a.sl23 = b.KetQua.ToString();
                //                                    break;
                //                                case 23:
                //                                    a.sl24 = b.KetQua.ToString();
                //                                    break;
                //                                case 24:
                //                                    a.sl25 = b.KetQua.ToString();
                //                                    break;
                //                                case 25:
                //                                    a.sl26 = b.KetQua.ToString();
                //                                    break;
                //                                case 26:
                //                                    a.sl27 = b.KetQua.ToString();
                //                                    break;
                //                                case 27:
                //                                    a.sl28 = b.KetQua.ToString();
                //                                    break;
                //                                case 28:
                //                                    a.sl29 = b.KetQua.ToString();
                //                                    break;
                //                                case 29:
                //                                    a.sl30 = b.KetQua.ToString();
                //                                    break;

                //                            }
                //                            //}
                //                        }
                //                    }
                //                }
                //            }
                //        }

                //    }

                //    BaoCao.Rep_ThHoaSinhMauSL rep = new BaoCao.Rep_ThHoaSinhMauSL();
                //    frmIn frm = new frmIn();
                //    rep.TenBC.Value = ("Thống kê số lượt thực hiện " + _tenbc).ToUpper();
                //    rep.TuNgayDenNgay.Value = "Từ ngày " + NT.ToString().Substring(0, 10) + " đến ngày " + ND.ToString().Substring(0, 10);
                //    rep.TuNgay.Value = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
                //    rep.DenNgay.Value = DungChung.Ham.NgayTu(LupNgayden.DateTime);
                    
                //    for (int i = 0; i < _SHM.Count; i++)
                //    {
                //        switch (i)
                //        {
                //            case 0:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM1.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 1:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM2.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 2:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM3.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 3:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM4.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 4:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM5.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 5:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM6.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 6:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM7.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 7:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM8.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 8:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM9.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 9:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM10.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 10:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM11.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 11:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM12.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 12:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM13.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 13:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM14.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 14:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM15.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 15:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM16.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 16:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM17.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 17:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM18.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 18:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM19.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 19:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM20.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 20:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM21.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 21:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM22.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 22:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM23.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 23:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM24.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 24:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM25.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 25:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM26.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 26:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM27.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 27:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM28.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 28:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM29.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //            case 29:
                //                if (_SHM.Skip(i).First().tendvct != null)
                //                { rep.SHM30.Value = _SHM.Skip(i).First().tendvct; }
                //                break;
                //        }

                //    }


                //    rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang);
                //    rep.BindingData();
                //    rep.CreateDocument();
                //    _BenhNhan.Clear();
                //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //    frm.ShowDialog();

                //}
            }
    }


        private void ButHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();
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
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
    }   
}