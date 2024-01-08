using System;
using QLBV_Database;
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
    public partial class Frm_SoPhauThuat_ThuThuat : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoPhauThuat_ThuThuat()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        string _ptt = "";
        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            return true;
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
        private class DichVu
        {
            private string TenDV;
            private int MaDV;
            private bool Chon;
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public int madv
            { set { MaDV = value; } get { return MaDV; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        private class MaDT
        {
            private string DTBN;
            private bool Chon;
            public string dtbn
            { set { DTBN = value; } get { return DTBN; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        List<MaDT> _lmadt = new List<MaDT>();
        List<KPhong> _Kphong = new List<KPhong>();
        //List<DV> _ldv = new List<DV>();
        List<DichVu> _lmadv = new List<DichVu>();

        private void Frm_SoPhauThuat_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            _Kphong.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            if (radioGroup1.SelectedIndex == 0) { _ptt = "Phẫu thuật"; }
            if (radioGroup1.SelectedIndex == 1) { _ptt = "Thủ thuật"; }

            var kphong = (from kp in data.KPhongs
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
            _lmadv.Clear();

            var qdvdt1 = (from dt in data.DThuocs
                          join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                          join tn in data.TieuNhomDVs.Where(p => p.TenRG == _ptt) on dv.IdTieuNhom equals tn.IdTieuNhom
                          select new { dv.MaDV, dv.TenDV, dtct.IDCD, tn.TenRG }).ToList();
            var qdvdt = (from dv in qdvdt1 group dv by new { dv.MaDV, dv.TenDV } into kq select new { kq.Key.MaDV, kq.Key.TenDV }).ToList();

            if (qdvdt.Count > 0)
            {
                DichVu themmoi1 = new DichVu();
                themmoi1.tendv = "A.Chọn tất cả";
                themmoi1.madv = 0;
                themmoi1.chon = true;
                _lmadv.Add(themmoi1);

                foreach (var a in qdvdt)
                {
                    DichVu them = new DichVu();
                    them.madv = a.MaDV;
                    them.tendv = a.TenDV;
                    them.chon = true;
                    _lmadv.Add(them);
                }
            }
            grcDichVu.DataSource = _lmadv.OrderBy(p => p.tendv).ToList();
            _lmadt.Clear();
            var dtuong = (from dt in data.DTBNs group dt by new { dt.DTBN1 } into kq select new { kq.Key.DTBN1 }).ToList();
            if (dtuong.Count > 0)
            {
                MaDT themmoi1 = new MaDT();
                themmoi1.dtbn = "Chọn tất cả";
                themmoi1.chon = true;
                _lmadt.Add(themmoi1);
                foreach (var a in dtuong)
                {
                    MaDT themmoi = new MaDT();
                    themmoi.dtbn = a.DTBN1;
                    themmoi.chon = true;
                    _lmadt.Add(themmoi);
                }
                grcDTuong.DataSource = _lmadt.ToList();
            }


        }
        private class BN
        {
            private string TenBNhan;
            private int MaBNhan;
            private string Nam;
            private string Nu;
            private string BHYT;
            private string DiaChi;
            private int GTinh;
            private int NoiTru;
            private string ChanDoan;
            private int MaKP;
            private string NoiGui;
            private int MaDV;
            private string YeuCau;
            private string Loai;
            private string PPVC;
            private string KetLuan;
            private string MaBSTH;
            private string BSTH;
            private string BSGM;
            private string NgayTH;
            private string NgayBD;
            private int PL;
            private double SoLuong;
            public string tenbnhan

            { set { TenBNhan = value; } get { return TenBNhan; } }
            public int mabnhan
            { set { MaBNhan = value; } get { return MaBNhan; } }
            public string nam
            { set { Nam = value; } get { return Nam; } }
            public string nu
            { set { Nu = value; } get { return Nu; } }
            public string bhyt
            { set { BHYT = value; } get { return BHYT; } }
            public string diachi
            { set { DiaChi = value; } get { return DiaChi; } }
            public int noitru
            { set { NoiTru = value; } get { return NoiTru; } }
            public int gtinh
            { set { GTinh = value; } get { return GTinh; } }
            public string chandoan
            { set { ChanDoan = value; } get { return ChanDoan; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string noigui
            { set { NoiGui = value; } get { return NoiGui; } }
            public int madv
            { set { MaDV = value; } get { return MaDV; } }
            public string yeucau
            { set { YeuCau = value; } get { return YeuCau; } }
            public string loai
            { set { Loai = value; } get { return Loai; } }
            public string ppvc
            { set { PPVC = value; } get { return PPVC; } }
            public string ketluan
            { set { KetLuan = value; } get { return KetLuan; } }
            public string mabsth
            { set { MaBSTH = value; } get { return MaBSTH; } }
            public string bsth
            { set { BSTH = value; } get { return BSTH; } }
            public string bsgm
            { set { BSGM = value; } get { return BSGM; } }
            public string ngayth
            { set { NgayTH = value; } get { return NgayTH; } }
            public string ngaybd
            { set { NgayBD = value; } get { return NgayBD; } }
            public int pl
            { set { PL = value; } get { return PL; } }
            public double soluong
            { set { SoLuong = value; } get { return SoLuong; } }

        }
        List<BN> _BNhan = new List<BN>();
        List<KP> _lKhoaP = new List<KP>();
        public class KP
        {
            private int makp;
            public int MaKP
            {
                set { makp = value; }
                get { return makp; }
            }
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            List<KPhong> _lkp = new List<KPhong>();
            List<MaDT> _ldt = new List<MaDT>();

            if (KTtaoBc())
            {
                _BNhan.Clear(); _lKhoaP.Clear();

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                var cb = data.CanBoes.ToList();
                _lkp = _Kphong.Where(p => p.chon == true).ToList();
                _lkp.Add(new KPhong { makp = 0, tenkp = "" });
                _ldt = _lmadt.Where(p => p.chon == true).ToList();
                _ldt.Add(new MaDT { dtbn = "" });
                int _the = 0;
                if (chkInThe.Checked == true) { _the = 1; } else { _the = 0; }

                //var qdv = (from tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat)
                //           join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                //           select new { dv.TenDV, dv.MaDV, tn.TenRG , dv.Loai}).ToList();
                #region Tất cả BN thực hiện
                string aa = cboTKBN.EditValue.ToString();
                var qdv = (from tn in data.TieuNhomDVs.Where(p => p.TenRG == _ptt)
                           join dv in data.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                           select new { dv.TenDV, dv.MaDV, tn.TenRG, dv.Loai }).ToList();

                if (aa == "Tất cả BN thực hiện (Thống kê theo ngày thực hiện)")
                {
                    #region có CLS
                    if (chkBNcoCLS.Checked == true)
                    {
                        //var qcls0 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                        //             join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                        //             join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                        //             join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                        //             select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, bn.NoiTru, cd.MaDV, cd.ChiDinh1, cd.Status, KetLuan = clsct.KetQua, cd.LoiDan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.DSCBTH }
                        //          ).ToList();


                        var qcls01 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                      join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                      join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                      select new { cls.IdCLS, cd.MaDV, cd.ChiDinh1, cd.Status, KetLuan = clsct.KetQua, cd.LoiDan, cls.MaKP, cls.MaCBth, cls.NgayTH, cd.NgayBDTH, cls.DSCBTH }
                                  ).ToList();

                        var qcls02 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                      join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                      select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, bn.NoiTru, cls.IdCLS }
                                  ).ToList();

                        var qcls0 = (from cls in qcls01
                                     join bn in qcls02 on cls.IdCLS equals bn.IdCLS
                                     select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, bn.NoiTru, cls.MaDV, cls.ChiDinh1, cls.Status, KetLuan = cls.KetLuan, cls.LoiDan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.DSCBTH, cls.NgayBDTH }
                                  ).ToList();


                        var qcls = (from cls in qcls0
                                    join dv in qdv on cls.MaDV equals dv.MaDV
                                    select new { cls.MaBNhan, cls.TenBNhan, cls.DChi, cls.GTinh, cls.Tuoi, cls.DTuong, cls.SThe, cls.NoiTru, cls.MaDV, cls.ChiDinh1, cls.Status, cls.KetLuan, cls.LoiDan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.NgayBDTH, cls.DSCBTH, dv.TenDV, dv.Loai, dv.TenRG }
                                   ).ToList();

                        var qso1 = (from kp in _lkp
                                    join q in qcls on kp.makp equals q.MaKP
                                    select new { q.MaBNhan, q.TenBNhan, q.DChi, q.GTinh, q.Tuoi, q.DTuong, q.SThe, q.NoiTru, q.MaDV, q.TenDV, q.Loai, q.LoiDan, kp.makp, kp.tenkp, q.MaCBth, q.NgayTH, q.NgayBDTH, q.DSCBTH, q.KetLuan, q.Status, q.MaKP, q.TenRG }).ToList();
                        var qso = (from q in qso1
                                   join dt in _ldt.Where(p => p.dtbn != "Chọn tất cả") on q.DTuong equals dt.dtbn
                                   //join cd in data.ChiDinhs on q.Status equals cd.Status
                                   group q by new { q.MaBNhan, q.TenBNhan, q.DChi, q.GTinh, q.Tuoi, q.DTuong, q.SThe, q.NoiTru, q.MaDV, q.TenDV, q.Loai, q.LoiDan, q.makp, q.tenkp, q.MaCBth, q.NgayTH, q.NgayBDTH,q.DSCBTH, q.KetLuan, } into kq
                                   select new
                                   {
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenBNhan = kq.Key.TenBNhan,
                                       GTinh = kq.Key.GTinh,
                                       Tuoi = kq.Key.Tuoi,
                                       DTuong = kq.Key.DTuong,
                                       SThe = kq.Key.SThe,
                                       NoiTru = kq.Key.NoiTru,
                                       DiaChi = kq.Key.DChi,
                                       MaDV = kq.Key.MaDV,
                                       TenDV = kq.Key.TenDV,
                                       LoiDan = kq.Key.LoiDan,
                                       KetLuan = kq.Key.KetLuan,
                                       Loai = kq.Key.Loai,
                                       MakP = kq.Key.makp,
                                       TenKP = kq.Key.tenkp,
                                       BSTH = kq.Key.MaCBth,
                                       NgayTH = kq.Key.NgayTH,
                                       NgayBD = kq.Key.NgayBDTH,
                                       DSCBTH = kq.Key.DSCBTH,
                                   }).ToList();

                        if (qso.Count() > 0)
                        {
                            foreach (var a in qso)
                            {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh);
                                if (a.GTinh == 1) { themmoi.nam = a.Tuoi.ToString(); } else { themmoi.nam = ""; }
                                if (a.GTinh == 0) { themmoi.nu = a.Tuoi.ToString(); } else { themmoi.nu = ""; }
                                if (_the == 0)
                                {
                                    if (a.DTuong == "BHYT")
                                    {
                                        themmoi.bhyt = "X";
                                    }
                                    else { themmoi.bhyt = ""; }
                                }
                                else
                                {
                                    themmoi.bhyt = a.SThe;
                                }
                                themmoi.diachi = a.DiaChi;
                                themmoi.noitru = Convert.ToInt32(a.NoiTru);
                                themmoi.madv = a.MaDV == null ? 0 : a.MaDV.Value;
                                themmoi.yeucau = a.TenDV;
                                themmoi.ppvc = a.LoiDan;
                                themmoi.ketluan = a.KetLuan;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                if (a.Loai != 0 && a.Loai != null)
                                { themmoi.loai = "Loại " + a.Loai.ToString(); }
                                else { themmoi.loai = ""; }
                                if (a.BSTH != null)
                                {
                                    var bs = data.CanBoes.Where(p => p.MaCB == a.BSTH).Select(p => new { p.TenCB }).ToList();
                                    if (bs.Count > 0) { themmoi.bsth = bs.First().TenCB; }
                                }

                                if (a.NgayTH != null && a.NgayTH.ToString().Length >= 10)
                                {

                                    if (DungChung.Bien.MaBV == "30010")
                                    {
                                        themmoi.ngayth = a.NgayTH.Value.ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                    else
                                    {
                                        themmoi.ngayth = (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30281") ? a.NgayTH.ToString() : (DungChung.Bien.MaBV == "30002" ? a.NgayTH.Value.ToString("dd/MM/yyyy HH:mm:ss") : a.NgayTH.ToString().Substring(0, 10));
                                    }

                                }
                                else
                                {
                                    themmoi.ngayth = "00/00";
                                }

                                if (a.NgayBD != null && a.NgayBD.ToString().Length >= 10)
                                {

                                    if (DungChung.Bien.MaBV == "30010")
                                    {
                                        themmoi.ngaybd = a.NgayBD.Value.ToString("dd/MM/yyyy HH:mm:ss");
                                    }                               
                                }
                                else
                                {
                                    themmoi.ngaybd = "00/00";
                                }

                                if (a.DSCBTH != null && DungChung.Bien.MaBV != "27022")
                                {
                                    string _dscb = a.DSCBTH;
                                    string[] gm = new string[5];
                                    gm = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                                    themmoi.bsgm = gm[1];
                                }
                                else
                                {
                                    themmoi.bsgm = a.DSCBTH;
                                }
                                themmoi.soluong = 1;
                                _BNhan.Add(themmoi);
                            }
                        }
                    }
                    #endregion
                    #region không có CLS
                    if (chkBNkhongCLS.Checked == true)
                    {
                        //var qdt0 = (from dt in data.DThuocs
                        //            join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD == null || p.IDCD <= 0) on dt.IDDon equals dtct.IDDon
                        //            join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                        //            select new { bn.MaBNhan, bn.NoiTru, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, dtct.IDCD, dtct.MaKP, dtct.MaCB, dtct.MaDV, dtct.NgayNhap, dtct.DSCBTH, dtct.SoLuong }).ToList();

                        var qdt0 = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && (p.IDCD == null || p.IDCD <= 0))
                                    join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                    join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                    select new { bn.MaBNhan, bn.NoiTru, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, dtct.IDCD, dtct.MaKP, dtct.MaCB, dtct.MaDV, dtct.NgayNhap, dtct.DSCBTH, dtct.SoLuong }).ToList();



                        var qdt1 = (from dt in qdt0 join dv in qdv on dt.MaDV equals dv.MaDV select new { dt.MaBNhan, dt.NoiTru, dt.TenBNhan, dt.DChi, dt.GTinh, dt.Tuoi, dt.DTuong, dt.SThe, dv.MaDV, dv.TenDV, dv.Loai, dt.IDCD, dt.MaKP, dt.MaCB, dt.NgayNhap, dt.DSCBTH, dv.TenRG, dt.SoLuong }).ToList();
                        var qdt2 = (from kp in _lkp
                                    join qd in qdt1 on kp.makp equals qd.MaKP
                                    select new { qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DChi, qd.GTinh, qd.Tuoi, qd.DTuong, qd.SThe, qd.MaDV, qd.TenDV, qd.Loai, qd.MaKP, kp.tenkp, qd.MaCB, qd.NgayNhap, qd.DSCBTH, qd.IDCD, qd.TenRG, qd.SoLuong }).ToList();
                        var qdt = (from ma in _ldt.Where(p => p.dtbn != "Chọn tất cả")
                                   join qd in qdt2
                                       on ma.dtbn equals qd.DTuong
                                   group new { qd } by new { qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DChi, qd.GTinh, qd.Tuoi, qd.DTuong, qd.SThe, qd.MaDV, qd.TenDV, qd.Loai, qd.MaKP, qd.tenkp, qd.MaCB, qd.NgayNhap, qd.DSCBTH } into kq
                                   select new { kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.TenBNhan, kq.Key.DChi, kq.Key.GTinh, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.SThe, kq.Key.MaDV, kq.Key.TenDV, kq.Key.Loai, kq.Key.MaKP, kq.Key.tenkp, kq.Key.MaCB, kq.Key.NgayNhap, kq.Key.DSCBTH, SoLuong = kq.Sum(p => p.qd.SoLuong) }).ToList();

                        if (qdt.Count > 0)
                        {
                            foreach (var a in qdt)
                            {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh);
                                if (a.GTinh == 1) { themmoi.nam = a.Tuoi.ToString(); } else { themmoi.nam = ""; }
                                if (a.GTinh == 0) { themmoi.nu = a.Tuoi.ToString(); } else { themmoi.nu = ""; }
                                if (_the == 0)
                                {
                                    if (a.DTuong == "BHYT")
                                    {
                                        themmoi.bhyt = "X";
                                    }
                                    else { themmoi.bhyt = ""; }
                                }
                                else
                                {
                                    themmoi.bhyt = a.SThe;
                                }
                                themmoi.diachi = a.DChi;
                                themmoi.noitru = Convert.ToInt32(a.NoiTru);
                                themmoi.madv = a.MaDV;
                                themmoi.yeucau = a.TenDV;
                                if (a.Loai != 0 && a.Loai != null)
                                { themmoi.loai = "Loại " + a.Loai.ToString(); }
                                else { themmoi.loai = ""; }
                                themmoi.makp = a.MaKP == null ? 0 : a.MaKP.Value;
                                themmoi.noigui = a.tenkp;
                                if (a.MaCB != null)
                                {
                                    var bs = data.CanBoes.Where(p => p.MaCB == a.MaCB).Select(p => new { p.TenCB }).ToList();
                                    if (bs.Count > 0) { themmoi.bsth = bs.First().TenCB; }
                                }
                                if (a.DSCBTH != null)
                                {
                                    string _dscb = a.DSCBTH;
                                    string[] gm = new string[5];
                                    gm = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                                    themmoi.bsgm = gm[1];
                                }
                                if (a.NgayNhap != null && a.NgayNhap.ToString().Length >= 10)
                                {

                                    themmoi.ngayth = (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30281") ? a.NgayNhap.ToString() : (DungChung.Bien.MaBV == "30002" ? a.NgayNhap.Value.ToString("dd/MM/yyyy HH:mm:ss") : a.NgayNhap.Value.ToString("dd/MM/yyyy"));
                                }
                                else
                                {
                                    themmoi.ngayth = "00/00";
                                }
                                themmoi.soluong = a.SoLuong;
                                _BNhan.Add(themmoi);
                            }
                        }
                    }
                    #endregion
                }
                #endregion
                #region Chỉ BN đã thanh toán
                if (aa == "Chỉ BN đã thanh toán (Thống kê theo ngày thanh toán)")
                {
                    #region có CLS - tính theo ngày thanh toán
                    if (chkBNcoCLS.Checked == true)
                    {
                        //var qcls0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                        //             join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                        //             join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                        //             join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                        //             join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                        //             select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, bn.NoiTru, cd.MaDV, cd.ChiDinh1, cd.Status, KetLuan = clsct.KetQua, cd.LoiDan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.DSCBTH, vp.NgayTT }
                        //          ).ToList();

                        var qcls01 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                      join cls in data.CLS on vp.MaBNhan equals cls.MaBNhan
                                      join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                      join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                      select new { vp.MaBNhan, cd.MaDV, cd.ChiDinh1, cd.Status, KetLuan = clsct.KetQua, cd.LoiDan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.DSCBTH, vp.NgayTT }
                                  ).ToList();
                        var qcls02 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                      join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                      select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, bn.NoiTru, vp.NgayTT }
                                  ).ToList();
                        var qcls0 = (from cls in qcls01
                                     join bn in qcls02 on cls.MaBNhan equals bn.MaBNhan
                                     select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, bn.NoiTru, cls.MaDV, cls.ChiDinh1, cls.Status, KetLuan = cls.KetLuan, cls.LoiDan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.DSCBTH, bn.NgayTT }
                                  ).ToList();


                        var qcls = (from cls in qcls0
                                    join dv in qdv on cls.MaDV equals dv.MaDV
                                    select new { cls.MaBNhan, cls.TenBNhan, cls.DChi, cls.GTinh, cls.Tuoi, cls.DTuong, cls.SThe, cls.NoiTru, dv.MaDV, cls.ChiDinh1, cls.Status, dv.TenDV, dv.Loai, cls.KetLuan, cls.LoiDan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.DSCBTH, cls.NgayTT, dv.TenRG }
                                  ).ToList();

                        var qso1 = (from kp in _lkp
                                    join q in qcls on kp.makp equals q.MaKP
                                    select new { q.NgayTT, q.Status, q.TenRG, q.MaBNhan, q.TenBNhan, q.DChi, q.GTinh, q.Tuoi, q.DTuong, q.SThe, q.NoiTru, q.MaDV, q.TenDV, q.Loai, q.LoiDan, kp.makp, kp.tenkp, q.MaCBth, q.NgayTH, q.DSCBTH, q.KetLuan }).ToList();
                        var qso = (from ma in _ldt.Where(p => p.dtbn != "Chọn tất cả")
                                   join q in qso1 on ma.dtbn equals q.DTuong
                                   group q by new { q.MaBNhan, q.TenBNhan, q.DChi, q.GTinh, q.Tuoi, q.DTuong, q.SThe, q.NoiTru, q.MaDV, q.TenDV, q.Loai, q.LoiDan, q.makp, q.tenkp, q.MaCBth, q.NgayTH, q.DSCBTH, q.KetLuan } into kq
                                   select new
                                   {
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenBNhan = kq.Key.TenBNhan,
                                       GTinh = kq.Key.GTinh,
                                       Tuoi = kq.Key.Tuoi,
                                       DTuong = kq.Key.DTuong,
                                       SThe = kq.Key.SThe,
                                       NoiTru = kq.Key.NoiTru,
                                       DiaChi = kq.Key.DChi,
                                       MaDV = kq.Key.MaDV,
                                       TenDV = kq.Key.TenDV,
                                       LoiDan = kq.Key.LoiDan,
                                       KetLuan = kq.Key.KetLuan,
                                       Loai = kq.Key.Loai,
                                       MakP = kq.Key.makp,
                                       TenKP = kq.Key.tenkp,
                                       BSTH = kq.Key.MaCBth,
                                       NgayTH = kq.Key.NgayTH,
                                       DSCBTH = kq.Key.DSCBTH,
                                   }).ToList();
                        if (qso.Count() > 0)
                        {
                            foreach (var a in qso)
                            {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh);
                                if (a.GTinh == 1) { themmoi.nam = a.Tuoi.ToString(); } else { themmoi.nam = ""; }
                                if (a.GTinh == 0) { themmoi.nu = a.Tuoi.ToString(); } else { themmoi.nu = ""; }
                                if (_the == 0)
                                {
                                    if (a.DTuong == "BHYT")
                                    {
                                        themmoi.bhyt = "X";
                                    }
                                    else { themmoi.bhyt = ""; }
                                }
                                else
                                {
                                    themmoi.bhyt = a.SThe;
                                }
                                themmoi.diachi = a.DiaChi;
                                themmoi.noitru = Convert.ToInt32(a.NoiTru);
                                themmoi.madv = a.MaDV == null ? 0 : a.MaDV;
                                themmoi.yeucau = a.TenDV;
                                themmoi.ppvc = a.LoiDan;
                                themmoi.ketluan = a.KetLuan;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                if (a.Loai != 0 && a.Loai != null)
                                { themmoi.loai = "Loại " + a.Loai.ToString(); }
                                else { themmoi.loai = ""; }
                                //if (a.BSTH != null)
                                //{
                                //    var bs = data.CanBoes.Where(p => p.MaCB == a.BSTH).Select(p => new { p.TenCB }).ToList();
                                //    if (bs.Count > 0) { themmoi.bsth = bs.First().TenCB; }
                                //}
                                themmoi.mabsth = a.BSTH;
                                if (a.NgayTH != null && a.NgayTH.ToString().Length >= 10)
                                {
                                    themmoi.ngayth = (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30281") ? a.NgayTH.ToString() : (DungChung.Bien.MaBV == "30002" ? a.NgayTH.Value.ToString("dd/MM/yyyy HH:mm:ss") : a.NgayTH.ToString().Substring(0, 10));
                                }
                                else
                                {
                                    themmoi.ngayth = "00/00";
                                }
                                if (a.DSCBTH != null && DungChung.Bien.MaBV != "27022")
                                {
                                    string _dscb = a.DSCBTH;
                                    string[] gm = new string[5];
                                    gm = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                                    themmoi.bsgm = gm[1];
                                }
                                else
                                {
                                    themmoi.bsgm = a.DSCBTH;
                                }
                                themmoi.soluong = 1;
                                _BNhan.Add(themmoi);
                            }
                        }
                    }
                    #endregion
                    #region không có CLS
                    if (chkBNkhongCLS.Checked == true)
                    {
                        //var qdt0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                        //            join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                        //            join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                        //            join dtct in data.DThuoccts.Where(p => p.IDCD == null || p.IDCD <= 0) on dt.IDDon equals dtct.IDDon
                        //            select new { bn.MaBNhan, dtct.MaDV, bn.NoiTru, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, dtct.MaKP, dtct.IDCD, dtct.MaCB, dtct.NgayNhap, dtct.DSCBTH, vp.NgayTT, dtct.SoLuong }).ToList();

                        var qbn = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                   join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                   select new { bn.MaBNhan, bn.NoiTru, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, vp.NgayTT }).ToList();
                        var qdt02 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                     join dt in data.DThuocs on vp.MaBNhan equals dt.MaBNhan
                                     join dtct in data.DThuoccts.Where(p => p.IDCD == null || p.IDCD <= 0) on dt.IDDon equals dtct.IDDon
                                     select new { vp.MaBNhan, dtct.MaDV, dtct.MaKP, dtct.IDCD, dtct.MaCB, dtct.NgayNhap, dtct.DSCBTH, vp.NgayTT, dtct.SoLuong }).ToList();
                        var qdt0 = (from bn in qbn
                                    join dtct in qdt02 on bn.MaBNhan equals dtct.MaBNhan
                                    select new { bn.MaBNhan, dtct.MaDV, bn.NoiTru, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.SThe, dtct.MaKP, dtct.IDCD, dtct.MaCB, dtct.NgayNhap, dtct.DSCBTH, bn.NgayTT, dtct.SoLuong }).ToList();

                        var qdt01 = (from dt in qdt0
                                     join dv in qdv on dt.MaDV equals dv.MaDV
                                     select new { dt.MaBNhan, dt.NoiTru, dt.TenBNhan, dt.DChi, dt.GTinh, dt.Tuoi, dt.DTuong, dt.SThe, dt.MaKP, dt.IDCD, dt.MaCB, dt.NgayNhap, dt.DSCBTH, dt.NgayTT, dv.TenDV, dv.TenRG, dv.Loai, dv.MaDV, dt.SoLuong }).ToList();
                        var qdt2 = (from kp in _lkp
                                    join qd in qdt01 on kp.makp equals qd.MaKP
                                    select new { qd.NgayTT, qd.IDCD, qd.TenRG, qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DChi, qd.GTinh, qd.Tuoi, qd.DTuong, qd.SThe, qd.MaDV, qd.TenDV, qd.Loai, qd.MaKP, kp.tenkp, qd.MaCB, qd.NgayNhap, qd.DSCBTH, qd.SoLuong }).ToList();
                        var qdt = (from ma in _ldt.Where(p => p.dtbn != "Chọn tất cả")
                                   join qd in qdt2 on ma.dtbn equals qd.DTuong
                                   group new { qd } by new { qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DChi, qd.GTinh, qd.Tuoi, qd.DTuong, qd.SThe, qd.MaDV, qd.TenDV, qd.Loai, qd.MaKP, qd.tenkp, qd.MaCB, qd.NgayNhap, qd.DSCBTH } into kq
                                   select new { kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.TenBNhan, kq.Key.DChi, kq.Key.GTinh, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.SThe, kq.Key.MaDV, kq.Key.TenDV, kq.Key.Loai, kq.Key.MaKP, kq.Key.tenkp, kq.Key.MaCB, kq.Key.NgayNhap, kq.Key.DSCBTH, SoLuong = kq.Sum(p => p.qd.SoLuong) }).ToList();
                        //var qdt = (from qd in qdt2  
                        //            join   ma in _ldt.Where(p => p.dtbn != "Chọn tất cả") on qd.DTuong equals ma.dtbn
                        //           //group new { qd } by new { qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DChi, qd.GTinh, qd.Tuoi, qd.DTuong, qd.SThe, qd.MaDV, qd.TenDV, qd.Loai, qd.MaKP, qd.tenkp, qd.MaCB, qd.NgayNhap, qd.DSCBTH } into kq
                        //           //select new { kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.TenBNhan, kq.Key.DChi, kq.Key.GTinh, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.SThe, kq.Key.MaDV, kq.Key.TenDV, kq.Key.Loai, kq.Key.MaKP, kq.Key.tenkp, kq.Key.MaCB, kq.Key.NgayNhap, kq.Key.DSCBTH }).ToList();

                        if (qdt.Count > 0)
                        {
                            foreach (var a in qdt)
                            {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh);
                                if (a.GTinh == 1) { themmoi.nam = a.Tuoi.ToString(); } else { themmoi.nam = ""; }
                                if (a.GTinh == 0) { themmoi.nu = a.Tuoi.ToString(); } else { themmoi.nu = ""; }
                                if (_the == 0)
                                {
                                    if (a.DTuong == "BHYT")
                                    {
                                        themmoi.bhyt = "X";
                                    }
                                    else { themmoi.bhyt = ""; }
                                }
                                else
                                {
                                    themmoi.bhyt = a.SThe;
                                }
                                themmoi.diachi = a.DChi;
                                themmoi.noitru = Convert.ToInt32(a.NoiTru);
                                themmoi.madv = a.MaDV;
                                themmoi.yeucau = a.TenDV;
                                if (a.Loai != 0 && a.Loai != null)
                                { themmoi.loai = "Loại " + a.Loai.ToString(); }
                                else { themmoi.loai = ""; }
                                themmoi.makp = a.MaKP == null ? 0 : a.MaKP.Value;
                                themmoi.noigui = a.tenkp;
                                themmoi.mabsth = a.MaCB;
                                if (a.DSCBTH != null)
                                {
                                    string _dscb = a.DSCBTH;
                                    string[] gm = new string[5];
                                    gm = QLBV_Library.QLBV_Ham.LayChuoi(';', _dscb);
                                    themmoi.bsgm = gm[1];
                                }
                                if (a.NgayNhap != null && a.NgayNhap.ToString().Length >= 10)
                                {

                                    themmoi.ngayth = (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30281") ? a.NgayNhap.ToString() : (DungChung.Bien.MaBV == "30002" ? a.NgayNhap.Value.ToString("dd/MM/yyyy HH:mm:ss") : a.NgayNhap.Value.ToString("dd/MM/yyyy"));
                                }
                                else
                                {
                                    themmoi.ngayth = "00/00";
                                }
                                themmoi.soluong = a.SoLuong;
                                _BNhan.Add(themmoi);
                            }
                        }
                    }
                    #endregion
                }
                #endregion
                foreach (var a in _BNhan)
                {
                    foreach (var b in cb)
                    {
                        if (a.mabsth == b.MaCB)
                        {
                            a.bsth = b.TenCB;
                        }
                    }
                }
                #region lấy chẩn đoán
                //var qcd1 = (from dt in data.DThuocs
                //            join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                //            join bnkb in data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                //            select new { dtct.NgayNhap, bnkb.MaBNhan, bnkb.MaKP, ChanDoan = bnkb.ChanDoan == null ? "" : bnkb.ChanDoan, BenhKhac = bnkb.BenhKhac == null ? "" : bnkb.BenhKhac }).ToList();

                var qcd1 = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                            join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                            join bnkb in data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                            select new { dtct.NgayNhap, bnkb.MaBNhan, bnkb.MaKP, ChanDoan = bnkb.ChanDoan == null ? "" : bnkb.ChanDoan, BenhKhac = bnkb.BenhKhac == null ? "" : bnkb.BenhKhac }).ToList();


                var qcd = (from dt in qcd1
                           group dt by new { dt.MaBNhan, dt.MaKP, dt.ChanDoan, dt.BenhKhac } into kq
                           select new { kq.Key.MaBNhan, kq.Key.MaKP, ChanDoan = kq.Key.ChanDoan + (DungChung.Bien.MaBV == "30009" ? (kq.Key.ChanDoan == "" ? kq.Key.BenhKhac : ("; " + kq.Key.BenhKhac)) : "") }).ToList();
                if (qcd.Count > 0)
                {
                    foreach (var a in qcd)
                    {
                        foreach (var b in _BNhan)
                        {
                            if (a.MaBNhan == b.mabnhan)
                            {
                                if (a.MaKP == b.makp)
                                {
                                    b.chandoan = a.ChanDoan.ToString();
                                }
                            }
                        }
                    }
                }
                #endregion
                int _bn1 = -1; int _bn2 = -1;
                {
                    if (radBN.SelectedIndex == 0) { _bn1 = 1; _bn2 = 0; }
                    if (radBN.SelectedIndex == 1) { _bn1 = 1; _bn2 = -1; }
                    if (radBN.SelectedIndex == 2) { _bn2 = 0; _bn1 = -1; }
                }
                var _BN = (from dv in _lmadv.Where(p => p.chon == true)
                           join bn in _BNhan.Where(p => p.noitru == _bn1 || p.noitru == _bn2) on dv.madv equals bn.madv
                           group new { bn } by new { bn.makp, bn.noigui, bn.mabnhan, bn.tenbnhan, bn.diachi, bn.gtinh, bn.nam, bn.nu, bn.bhyt, bn.chandoan, bn.ketluan, bn.pl, bn.noitru, bn.madv, bn.yeucau, bn.loai, bn.ppvc, bn.bsth, bn.bsgm, bn.ngayth,bn.ngaybd, bn.soluong } into kq
                           select new
                           {
                               kq.Key.makp,
                               kq.Key.noigui,
                               kq.Key.mabnhan,
                               tenbnhan = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") ? (kq.Key.mabnhan + " - " + kq.Key.tenbnhan) : kq.Key.tenbnhan,
                               kq.Key.diachi,
                               kq.Key.gtinh,
                               kq.Key.nam,
                               kq.Key.nu,
                               kq.Key.bhyt,
                               kq.Key.chandoan,
                               kq.Key.ketluan,
                               kq.Key.pl,
                               kq.Key.noitru,
                               kq.Key.madv,
                               kq.Key.loai,
                               kq.Key.yeucau,
                               kq.Key.ppvc,
                               kq.Key.bsth,
                               kq.Key.bsgm,
                               kq.Key.ngayth,
                               ngaybd = (DungChung.Bien.MaBV == "30010" ? kq.Key.ngaybd : (kq.Key.ngaybd)),
                               kq.Key.soluong
                           }).ToList();


                string _tenso = "", _mso = "", _cdt = "", _cds = "", _pppt = "", _ng = "", _loai = "", _bsy = "";
                if (radioGroup1.SelectedIndex == 0)
                {
                    _tenso = ("Sổ phẫu thuật").ToUpper(); _mso = "MS: 11/BV-01"; _cdt = "Trước phẫu thuật"; _cds = "Sau phẫu thuật"; _pppt = "Phương pháp phẫu thuật"; _ng = DungChung.Bien.MaBV == "20001" ? "Ngày phẫu thuật" : "Ngày/giờ phẫu thuật"; _loai = "Loại phẫu thuật"; _bsy = DungChung.Bien.MaBV == "20001" ? "Người thực hiện phẫu thuật" : "Bác sỹ phẫu thuật";
                }
                if (radioGroup1.SelectedIndex == 1)
                {
                    _tenso = ("Sổ thủ thuật").ToUpper(); _mso = "MS: 12/BV-01"; _cdt = "Trước thủ thuật"; _cds = "Sau thủ thuật"; _pppt = "Phương pháp thủ thuật"; _ng = DungChung.Bien.MaBV == "20001" ? "Ngày thủ thuật" : "Ngày/giờ thủ thuật"; _loai = "Loại thủ thuật"; _bsy = DungChung.Bien.MaBV == "20001" ? "Người thực hiện thủ thuật" : "Bác sỹ thủ thuật";
                }

                if (radMau.SelectedIndex == 0)
                {
                    if (ckcHienThiSL.Checked)
                    {
                        BaoCao.Rep_SoPhauThuat_ThuThuat_A4_SL rep = new BaoCao.Rep_SoPhauThuat_ThuThuat_A4_SL();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        rep.TenSo.Value = _tenso; rep.MSo.Value = _mso; rep.CDTruoc.Value = _cdt; rep.CDSau.Value = _cds; rep.PPPT.Value = _pppt; rep.NgayGio.Value = _ng; rep.Loai.Value = _loai; rep.BSy.Value = _bsy;

                        rep.DataSource = _BN.OrderBy(p => p.ngayth.ToString().Substring(3, 2)).ThenBy(p => p.ngayth.ToString().Substring(0, 2)).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();

                        rep.Nam.Value = _BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count();
                        rep.Nu.Value = _BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count();
                        rep.BHYT.Value = _BN.Where(p => p.bhyt == "X").Select(p => p.mabnhan).Count();
                        rep.TenSo.Value = _tenso;
                        if (_the == 0)
                        { rep.TenTheBH.Value = "Có BHYT"; }
                        else { rep.TenTheBH.Value = "Số thẻ BHYT"; }

                        #region xuat Excel

                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        string[] _tieude = { "STT", "Họ tên người bệnh", "Tuổi nam", "Tuổi nữ", "Địa chỉ", "BHYT", "CĐ trước PTTT", "CĐ sau PTTT", "Phương pháp PTTT", "Số lượng", "Phương pháp vô cảm", "Ngày giờ PTTT", "Loại PTTT", "Bác sỹ PTTT", "Bác sỹ gây mê, tê", "Ghi chú" };
                        DungChung.Bien.MangHaiChieu = new Object[_BN.Count + 2, 16];
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }
                        int[] _arrWidth = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                        int num = 0;
                        foreach (var r in _BN)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
                            DungChung.Bien.MangHaiChieu[num, 2] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.nam != "") ? DungChung.Ham.TuoitheoThang(data, r.mabnhan, "12-00") : r.nam;
                            DungChung.Bien.MangHaiChieu[num, 3] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.nu != "") ? DungChung.Ham.TuoitheoThang(data, r.mabnhan, "12-00") : r.nu;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.nu;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.bhyt;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.chandoan;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.ketluan;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.yeucau;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.soluong;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.ppvc;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.ngayth;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.loai;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.bsth;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.bsgm;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.noigui;

                            num++;

                        }
                        //frmIn frm = new frmIn();

                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo Phẫu thuật / Thủ thuật", "C:\\BcPTTT.xls", true, this.Name);
                        #endregion
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        BaoCao.Rep_SoPhauThuat_ThuThuat_A4 rep = new BaoCao.Rep_SoPhauThuat_ThuThuat_A4();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        if (DungChung.Bien.MaBV == "27022")
                        {
                            _bsy = "Người thực hiện";
                        }
                        rep.TenSo.Value = _tenso; rep.MSo.Value = _mso; rep.CDTruoc.Value = _cdt; rep.CDSau.Value = _cds; rep.PPPT.Value = _pppt; rep.NgayGio.Value = _ng; rep.Loai.Value = _loai; rep.BSy.Value = _bsy;

                        rep.DataSource = _BN.OrderBy(p => p.ngayth.ToString().Substring(3, 2)).ThenBy(p => p.ngayth.ToString().Substring(0, 2)).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();

                        rep.Nam.Value = _BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count();
                        rep.Nu.Value = _BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count();
                        rep.BHYT.Value = _BN.Where(p => p.bhyt == "X").Select(p => p.mabnhan).Count();
                        rep.TenSo.Value = _tenso;
                        if (_the == 0)
                        { rep.TenTheBH.Value = "Có BHYT"; }
                        else { rep.TenTheBH.Value = "Số thẻ BHYT"; }

                        #region xuat Excel

                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        string[] _tieude = { "STT", "Họ tên người bệnh", "Tuổi nam", "Tuổi nữ", "Địa chỉ", "BHYT", "CĐ trước PTTT", "CĐ sau PTTT", "Phương pháp PTTT", "Phương pháp vô cảm", "Ngày giờ PTTT", "Loại PTTT", "Bác sỹ PTTT", "Bác sỹ gây mê, tê", "Ghi chú" };
                        DungChung.Bien.MangHaiChieu = new Object[_BN.Count + 2, 15];
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }
                        int[] _arrWidth = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                        int num = 0;
                        foreach (var r in _BN)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbnhan;
                            DungChung.Bien.MangHaiChieu[num, 2] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.nam != "") ? DungChung.Ham.TuoitheoThang(data, r.mabnhan, "12-00") : r.nam;
                            DungChung.Bien.MangHaiChieu[num, 3] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.nu != "") ? DungChung.Ham.TuoitheoThang(data, r.mabnhan, "12-00") : r.nu;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.bhyt;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.chandoan;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.ketluan;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.yeucau;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.ppvc;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.ngayth;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.loai;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.bsth;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.bsgm;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.noigui;

                            num++;

                        }
                        // frmIn frm = new frmIn();

                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo Phẫu thuật / Thủ thuật", "C:\\BcPTTT.xls", true, this.Name);
                        #endregion
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                }
                if (radMau.SelectedIndex == 1)
                {
                    BaoCao.Rep_SoPhauThuat_ThuThuat rep = new BaoCao.Rep_SoPhauThuat_ThuThuat();
                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                    if (DungChung.Bien.MaBV == "27022")
                    {
                        _bsy = "Người thực hiện";
                    }
                    rep.TenSo.Value = _tenso; rep.MSo.Value = _mso; rep.CDTruoc.Value = _cdt; rep.CDSau.Value = _cds; rep.PPPT.Value = _pppt; rep.NgayGio.Value = _ng; rep.Loai.Value = _loai; rep.BSy.Value = _bsy;
                    rep.DataSource = _BN.OrderBy(p => p.ngayth.ToString().Substring(3, 2)).ThenBy(p => p.ngayth.ToString().Substring(0, 2)).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                    rep.Nam.Value = _BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count();
                    rep.Nu.Value = _BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count();
                    rep.BHYT.Value = _BN.Where(p => p.bhyt == "X").Select(p => p.mabnhan).Count();
                    rep.TenSo.Value = _tenso;
                    if (_the == 0)
                    { rep.TenTheBH.Value = "Có BHYT"; }
                    else { rep.TenTheBH.Value = "Số thẻ BHYT"; }

                    //frmIn frm = new frmIn();
                    #region xuat Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    string[] _tieude = { "STT", "Họ tên người bệnh", "Tuổi nam", "Tuổi nữ", "Địa chỉ", "BHYT", "CĐ trước PTTT", "CĐ sau PTTT", "Phương phát PTTT", "Phương pháp vô cảm", "Ngày giờ PTTT", "Loại PTTT", "Bác sỹ PTTT", "Bác sỹ gây mê, tê", "Ghi chú" };
                    DungChung.Bien.MangHaiChieu = new Object[_BN.Count + 2, 15];
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int[] _arrWidth = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
                    DungChung.Bien.MangHaiChieu = new Object[_BN.Count + 1, 15];
                    int num = 1;
                    foreach (var r in _BN)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num + 1;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.tenbnhan;
                        DungChung.Bien.MangHaiChieu[num, 2] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.nam != "") ? DungChung.Ham.TuoitheoThang(data, r.mabnhan, "12-00") : r.nam;
                        DungChung.Bien.MangHaiChieu[num, 3] = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" && r.nu != "") ? DungChung.Ham.TuoitheoThang(data, r.mabnhan, "12-00") : r.nu;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.bhyt;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.chandoan;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.ketluan;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.yeucau;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.ppvc;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.ngayth;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.loai;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.bsth;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.bsgm;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.noigui;

                        num++;

                    }
                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo Phẫu thuật / Thủ thuật", "C:\\BcPTTT.xls", true, this.Name);

                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }


            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "col_Chon")
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
                    else
                    {
                        if (grvKhoaphong.GetFocusedRowCellValue(col_Chon) != null && grvKhoaphong.GetFocusedRowCellValue(col_Chon).ToString().ToLower() == "false")
                        {
                            foreach (var a in _Kphong)
                            {
                                if (a.tenkp == grvKhoaphong.GetFocusedRowCellValue(TenKP).ToString())
                                {
                                    a.chon = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                if (a.tenkp == grvKhoaphong.GetFocusedRowCellValue(TenKP).ToString())
                                {
                                    a.chon = false;
                                }
                            }
                        }
                    }

                }

                //List<KPhong> _lp = new List<KPhong>();
                //_lp = _Kphong.Where(p => p.chon == true).ToList();

                _lmadv.Clear();
                var qdvdt1 = (from dt in data.DThuocs
                              join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                              join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                              join tn in data.TieuNhomDVs.Where(p => p.TenRG == _ptt) on dv.IdTieuNhom equals tn.IdTieuNhom
                              select new { dtct.MaKP, dv.MaDV, dv.TenDV, dtct.IDCD, tn.TenRG }).ToList();
                var qdvdt = (from kp in _Kphong.Where(p => p.chon == true)
                             join dv in qdvdt1 on kp.makp equals dv.MaKP
                             group dv by new { dv.MaDV, dv.TenDV } into kq
                             select new { kq.Key.MaDV, kq.Key.TenDV }).ToList();

                if (qdvdt.Count > 0)
                {
                    DichVu themmoi1 = new DichVu();
                    themmoi1.tendv = "A.Chọn tất cả";
                    themmoi1.madv = 0;
                    themmoi1.chon = true;
                    _lmadv.Add(themmoi1);
                    foreach (var a in qdvdt)
                    {
                        DichVu them = new DichVu();
                        them.madv = a.MaDV;
                        them.tendv = a.TenDV;
                        them.chon = true;
                        _lmadv.Add(them);
                    }
                }
                grcDichVu.DataSource = "";
                grcDichVu.DataSource = _lmadv.OrderBy(p => p.tendv).ToList();

            }


        }

        private void grvDTuong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.Name == "Chon")
            {
                if (grvDTuong.GetFocusedRowCellValue("dtbn") != null)
                {
                    string Madt = grvDTuong.GetFocusedRowCellValue("dtbn").ToString();

                    if (Madt == "Chọn tất cả")
                    {
                        if (_lmadt.First().chon == true)
                        {
                            foreach (var a in _lmadt)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lmadt)
                            {
                                a.chon = true;
                            }
                        }
                        grcDTuong.DataSource = "";
                        grcDTuong.DataSource = _lmadt.ToList();
                    }
                }
            }

        }


        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0) { _ptt = "Phẫu thuật"; }
            else { _ptt = "Thủ thuật"; }
            Frm_SoPhauThuat_Load(sender, e);
        }

        private void radioGroup1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {


        }

        private void radioGroup1_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (radioGroup1.SelectedIndex == 0) { _ptt = "Phẫu thuật"; }
            //else { _ptt = "Thủ thuật"; }
            ////   if (radioGroup1.SelectedIndex == 1) { _ptt = "Thủ thuật"; }
            //var qdvdt1 = (from kp in _Kphong.Where(p => p.chon == true)
            //              join dt in data.DThuocs on kp.makp equals dt.MaKP
            //              join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
            //              join dv in data.DichVus on dtct.MaDV equals dv.MaDV
            //              join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
            //              select new { dv.MaDV, dv.TenDV, dtct.IDCD, tn.TenRG }).ToList();
            //var qdvdt = (from dv in qdvdt1.Where(p => p.TenRG == _ptt) group dv by new { dv.MaDV, dv.TenDV } into kq select new { kq.Key.MaDV, kq.Key.TenDV }).ToList();

            //if (qdvdt.Count > 0)
            //{
            //    foreach (var a in qdvdt)
            //    {
            //        DichVu them = new DichVu();
            //        them.madv = a.MaDV;
            //        them.tendv = a.TenDV;
            //        them.chon = true;
            //        _lmadv.Add(them);
            //    }
            //}
            //grcDichVu.DataSource = "";
            //grcDichVu.DataSource = _lmadv.OrderBy(p => p.tendv).ToList();
        }

        private void grvDichVu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvDichVu.GetFocusedRowCellValue("tendv") != null)
                {
                    string Ten = grvDichVu.GetFocusedRowCellValue("tendv").ToString();

                    if (Ten == "A.Chọn tất cả")
                    {
                        if (_lmadv.First().chon == true)
                        {
                            foreach (var a in _lmadv)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lmadv)
                            {
                                a.chon = true;
                            }
                        }
                        grcDichVu.DataSource = "";
                        grcDichVu.DataSource = _lmadv.ToList();
                    }
                    else
                    {
                        if (grvDichVu.GetFocusedRowCellValue(colChon) != null && grvDichVu.GetFocusedRowCellValue(colChon).ToString().ToLower() == "false")
                        {
                            foreach (var a in _lmadv)
                            {
                                if (a.tendv == grvDichVu.GetFocusedRowCellValue(colTenDV).ToString())
                                {
                                    a.chon = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (var a in _lmadv)
                            {
                                if (a.tendv == grvDichVu.GetFocusedRowCellValue(colTenDV).ToString())
                                {
                                    a.chon = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void radMau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radMau.SelectedIndex == 0)
                ckcHienThiSL.Visible = true;
            else
                ckcHienThiSL.Visible = false;
        }
    }
}