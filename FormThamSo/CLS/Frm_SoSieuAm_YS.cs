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
    public partial class Frm_SoSieuAm_YS : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoSieuAm_YS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
        private class DV
        {
            private int MaDV;
            private string TenDV;
            //   private string TenRG;
            public int madv
            {
                set { MaDV = value; }
                get { return MaDV; }
            }
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            //public string tenrg
            //{ set { TenRG = value; } get { return TenRG; } }
        }
        List<KPhong> _Kphong = new List<KPhong>();
        List<DV> _ldv = new List<DV>();

        private void Frm_SoSieuAm_Load(object sender, EventArgs e)
        {

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lcanbo = data.CanBoes.ToList();
            _Kphong.Clear();
            _ldv.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
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
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }

            var qdv = (from dv in data.DichVus.Where(p => p.PLoai != 1)
                       join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Siêu âm") on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new { dv.MaDV, dv.TenDV, dv.TenRG }).ToList();

            if (qdv.Count() > 0)
            {
                DV them1 = new DV();
                them1.madv = 0;
                them1.tendv = "Tất cả";
                _ldv.Add(them1);
                foreach (var a in qdv)
                {
                    DV themmoi = new DV();
                    themmoi.madv = a.MaDV;
                    themmoi.tendv = a.TenDV;
                    _ldv.Add(themmoi);
                }

                lupDichVu.Properties.DataSource = _ldv.ToList();
            }

        }
        private class BN
        {
            private string TenBNhan;
            private int MaBNhan;
            private int NoiTru;
            private int GTinh;

            public int gtinh
            {
                get { return GTinh; }
                set { GTinh = value; }
            }
            private string DTuong;

            public string dtuong
            {
                get { return DTuong; }
                set { DTuong = value; }
            }
            private string Nam;
            private string Nu;
            private string DiaChi;
            private string BHYT;
            private string Khac;

            public string khac
            {
                get { return Khac; }
                set { Khac = value; }
            }
            private string ChanDoan;
            private int MaKP;
            private string TenKP;
            private string NoiGui;
            private int MaDV;

            public int madv
            {
                get { return MaDV; }
                set { MaDV = value; }
            }
            private string YeuCau;
            private string KetQua;
            private string BSTH;
            private string NgayTH;
            private DateTime Ngay;
            private DateTime NgayTT;
            private int Loai;
            public string tenbnhan
            { set { TenBNhan = value; } get { return TenBNhan; } }
            public int mabnhan
            { set { MaBNhan = value; } get { return MaBNhan; } }
            public string nam
            { set { Nam = value; } get { return Nam; } }
            public int noitru
            { set { NoiTru = value; } get { return NoiTru; } }
            public string nu
            { set { Nu = value; } get { return Nu; } }
            public string diachi
            { set { DiaChi = value; } get { return DiaChi; } }
            public string bhyt
            { set { BHYT = value; } get { return BHYT; } }
            public string chandoan
            { set { ChanDoan = value; } get { return ChanDoan; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public string noigui
            { set { NoiGui = value; } get { return NoiGui; } }
            public string yeucau
            { set { YeuCau = value; } get { return YeuCau; } }
            public string ketqua
            { set { KetQua = value; } get { return KetQua; } }
            public string bsth
            { set { BSTH = value; } get { return BSTH; } }
            public string ngayth
            { set { NgayTH = value; } get { return NgayTH; } }
            public DateTime ngay
            { set { Ngay = value; } get { return Ngay; } }
            public DateTime ngaytt
            { set { NgayTT = value; } get { return NgayTT; } }
            public int loai
            { set { Loai = value; } get { return Loai; } }
            private int PL;

            public int pl
            {
                get { return PL; }
                set { PL = value; }
            }

        }
        List<BN> _BN = new List<BN>();
        List<BN> _BNhan = new List<BN>();
        //  List<BN> _BN1 = new List<BN>();
        List<CanBo> _lcanbo = new List<CanBo>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            List<KPhong> _lKhoaP = new List<KPhong>();
            string _dtuong = "";
            _dtuong = cboDTuong.Text;

            int _madv = 0;
            if (lupDichVu.EditValue != null)
                _madv = Convert.ToInt32(lupDichVu.EditValue);
            frmIn frm = new frmIn();
            if (KTtaoBc())
            {
                _BNhan.Clear(); _lKhoaP.Clear();

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                string _cdha = "";
                if (radioGroup1.SelectedIndex == 0)
                {
                    _cdha = "Siêu âm";

                }
                if (radioGroup1.SelectedIndex == 1)
                {
                    _cdha = "X-Quang";
                }
                if (radioGroup1.SelectedIndex == 2)
                {
                    _cdha = "Điện tim";
                }
                _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });
                #region Tất cả BN thực hiện
                string aa = cboTKBN.EditValue.ToString();
                var dichvu = (from dcvt in data.DichVucts
                              join dv in data.DichVus on dcvt.MaDV equals dv.MaDV
                              join tnhom in data.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                              where (tnhom.TenRG.Contains(_cdha))
                              select new { dv.TenDV, dv.Loai, dv.MaDV, dcvt.MaDVct, tnhom.TenRG }).ToList();
                if (aa == "Tất cả BN thực hiện (Thống kê theo ngày thực hiện)")
                {
                    if (chkBNcoCLS.Checked == true)
                    {
                        
                        var qso1 = (from cd in data.ChiDinhs.Where(p => p.Status == 1)
                                    join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                    select new
                                    {
                                        clsct.MaDVct,
                                        cd.IdCLS,
                                        clsct.KetQua,                                       
                                        cd.MaDV,
                                        cd.ChiDinh1,
                                        cd.Status,
                                        cd.KetLuan
                                    }
                                ).ToList();


                        var qso2 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                    join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong) on cls.MaBNhan equals bn.MaBNhan
                                    select new { cls.IdCLS, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, cls.MaKP, cls.MaCBth, cls.NgayTH }
                                ).ToList();

                        var qso = (from bn in qso2

                                   join cls in qso1 on bn.IdCLS equals cls.IdCLS
                                   select new { cls.MaDVct, cls.IdCLS, cls.KetQua, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, cls.MaDV, cls.ChiDinh1, cls.Status, cls.KetLuan, bn.MaKP, bn.MaCBth, bn.NgayTH }
                               ).ToList();



                        var qtc = (from kp in _lKhoaP
                                   join q1 in qso on kp.makp equals q1.MaKP
                                   join dv in dichvu on q1.MaDVct equals dv.MaDVct
                                   group new { q1, dv } by new { q1.IdCLS, q1.KetQua, q1.MaBNhan, q1.TenBNhan, q1.DChi, q1.GTinh, q1.Tuoi, q1.DTuong, q1.NoiTru, q1.MaDV, q1.ChiDinh1, dv.TenDV, dv.Loai, q1.KetLuan, kp.makp, kp.tenkp, q1.MaCBth, q1.NgayTH } into kq
                                   select new
                                   {
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenBNhan = kq.Key.TenBNhan,
                                       GTinh = kq.Key.GTinh,
                                       Tuoi = kq.Key.Tuoi,
                                       NoiTru = kq.Key.NoiTru,
                                       DTuong = kq.Key.DTuong,
                                       DiaChi = kq.Key.DChi,
                                       MaDV = kq.Key.MaDV,
                                       TenDV = kq.Key.TenDV + kq.Key.ChiDinh1,
                                       KetQua = _cdha == "X-Quang" ? kq.Key.KetLuan + kq.Key.KetQua : kq.Key.KetLuan,
                                       MakP = kq.Key.makp,
                                       TenKP = kq.Key.tenkp,
                                       BSTH = kq.Key.MaCBth,
                                       NgayTH = kq.Key.NgayTH,
                                       Loai = kq.Key.Loai,
                                   }).ToList();
                        if (qtc.Count() > 0)
                        {
                            foreach (var a in qtc)
                            {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh);
                                if (a.GTinh == 0)
                                {
                                    themmoi.nu = a.Tuoi.ToString();
                                }
                                else { themmoi.nu = ""; }
                                if (a.GTinh == 1)
                                {
                                    themmoi.nam = a.Tuoi.ToString();
                                }
                                else { themmoi.nam = ""; }
                                themmoi.noitru = Convert.ToInt32(a.NoiTru.ToString());                               
                                if (a.DTuong == "BHYT")
                                {
                                    themmoi.bhyt = "X";
                                }
                                else { themmoi.khac = "X"; }
                                themmoi.diachi = a.DiaChi;
                                themmoi.madv = a.MaDV == null ? 0 : a.MaDV.Value;
                                themmoi.yeucau = a.TenDV;
                                themmoi.ketqua = a.KetQua;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                if (a.BSTH != null)
                                {
                                    var a1 = (from k in _lcanbo.Where(p => p.MaCB == a.BSTH) select new { k.TenCB }).ToList();
                                    if (a1.Count > 0)
                                    { themmoi.bsth = a1.First().TenCB; }
                                }
                                if (a.NgayTH != null && a.NgayTH.ToString().Length >= 10)
                                {
                                    themmoi.ngayth = a.NgayTH.ToString().Substring(0, 5);
                                }
                                else
                                {
                                    themmoi.ngayth = "00/00";
                                }

                                themmoi.loai = Convert.ToInt32(a.Loai);
                                _BNhan.Add(themmoi);
                            }
                        }
                    }
                    if (chkBNkhongCLS.Checked == true)
                    {                       
                        var qdt2 = (from dtct in data.DThuoccts  //.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                    join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                    select new { MaBNhan = dt.MaBNhan ?? 0, dtct.IDDonct, dtct.MaDV, dtct.MaKP, dtct.MaCB, dtct.NgayNhap, dtct.IDCD }).ToList();

                        var qdt3 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                    join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                    select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, vp.NgayTT }).ToList();

                        var qdt1 = (from dtct in qdt2  //.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                    join bn in qdt3 on dtct.MaBNhan equals bn.MaBNhan
                                    select new { dtct.IDDonct, dtct.MaDV, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, dtct.MaKP, dtct.MaCB, dtct.NgayNhap, dtct.IDCD, bn.NgayTT }).ToList();

                        var qdt = (from kp in _lKhoaP
                                   join qd in qdt1.Where(p => p.IDCD == null || p.IDCD <= 0) on kp.makp equals qd.MaKP
                                   join dv in dichvu on qd.MaDV equals dv.MaDV
                                   group new { qd, dv } by new { qd.IDDonct, qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DChi, qd.GTinh, qd.Tuoi, qd.DTuong, qd.MaDV, dv.TenDV, dv.Loai, kp.makp, kp.tenkp, qd.MaCB, qd.NgayNhap } into kq
                                   select new { kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.TenBNhan, kq.Key.DChi, kq.Key.GTinh, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.MaDV, kq.Key.TenDV, kq.Key.Loai, kq.Key.makp, kq.Key.tenkp, kq.Key.MaCB, kq.Key.NgayNhap }).ToList();
                        if (qdt.Count > 0)
                        {
                            foreach (var a in qdt)
                            {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh);
                                if (a.GTinh == 0)
                                {
                                    themmoi.nu = a.Tuoi.ToString();
                                }
                                else { themmoi.nu = ""; }
                                if (a.GTinh == 1)
                                {
                                    themmoi.nam = a.Tuoi.ToString();
                                }
                                else { themmoi.nam = ""; }
                                themmoi.noitru = Convert.ToInt32(a.NoiTru.ToString());                               
                                if (a.DTuong == "BHYT")
                                {
                                    themmoi.bhyt = "X";
                                }
                                else { themmoi.khac = "X"; }
                                themmoi.diachi = a.DChi;
                                themmoi.madv = a.MaDV ?? 0;
                                themmoi.yeucau = a.TenDV;
                                themmoi.makp = a.makp;
                                themmoi.noigui = a.tenkp;
                                if (a.MaCB != null)
                                {
                                    var a1 = (from k in _lcanbo.Where(p => p.MaCB == a.MaCB) select new { k.TenCB }).ToList();
                                    if (a1.Count > 0)
                                    { themmoi.bsth = a1.First().TenCB; }
                                }
                                if (a.NgayNhap != null && a.NgayNhap.ToString().Length >= 10)
                                {

                                    themmoi.ngayth = a.NgayNhap.ToString().Substring(0, 5);
                                }
                                else
                                {
                                    themmoi.ngayth = "00/00";                                   
                                }
                                themmoi.loai = Convert.ToInt32(a.Loai);
                                _BNhan.Add(themmoi);
                            }
                        }
                    }

                }
                #endregion
                #region Chỉ BN đã TT
                if (aa == "Chỉ BN đã thanh toán (Thống kê theo ngày thanh toán)")
                {
                    if (chkBNcoCLS.Checked == true)
                    {                       
                        var qso1 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                    join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong) on vp.MaBNhan equals bn.MaBNhan
                                   join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan                                   
                                   select new { cls.IdCLS,bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru,cls.MaKP, cls.MaCBth, cls.NgayTH, vp.NgayTT }
                                ).ToList();
                        var qso2 = (from   cd in data.ChiDinhs.Where(p => p.Status == 1)
                                   join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                   select new { cd.IdCLS, clsct.MaDVct, clsct.KetQua, cd.MaDV, cd.ChiDinh1, cd.Status, cd.KetLuan}
                                ).ToList();
                        var qso = (from bn in qso1                                   
                                   join clsct in qso2 on bn.IdCLS equals clsct.IdCLS
                                   select new { clsct.IdCLS, clsct.MaDVct, clsct.KetQua, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, clsct.MaDV, clsct.ChiDinh1, clsct.Status, clsct.KetLuan, bn.MaKP, bn.MaCBth, bn.NgayTH, bn.NgayTT }
                                ).ToList();


                        var qtc = (from kp in _lKhoaP
                                   join q1 in qso on kp.makp equals q1.MaKP
                                   join dv in dichvu on q1.MaDVct equals dv.MaDVct
                                   group new { q1, dv } by new { q1.IdCLS, q1.KetQua, q1.MaBNhan, q1.TenBNhan, q1.DChi, q1.GTinh, q1.Tuoi, q1.DTuong, q1.NoiTru, q1.MaDV, q1.ChiDinh1, dv.TenDV, dv.Loai, q1.KetLuan, kp.makp, kp.tenkp, q1.MaCBth, q1.NgayTH } into kq
                                   select new
                                   {
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenBNhan = kq.Key.TenBNhan,
                                       GTinh = kq.Key.GTinh,
                                       Tuoi = kq.Key.Tuoi,
                                       NoiTru = kq.Key.NoiTru,
                                       DTuong = kq.Key.DTuong,
                                       DiaChi = kq.Key.DChi,
                                       MaDV = kq.Key.MaDV,
                                       TenDV = kq.Key.TenDV + kq.Key.ChiDinh1,
                                       KetQua = _cdha == "X-Quang" ? kq.Key.KetLuan + kq.Key.KetQua : kq.Key.KetLuan,
                                       MakP = kq.Key.makp,
                                       TenKP = kq.Key.tenkp,
                                       BSTH = kq.Key.MaCBth,
                                       NgayTH = kq.Key.NgayTH,
                                       Loai = kq.Key.Loai,
                                   }).ToList();

                        List<BN> lbntc = (from a in qtc
                                          join cb in _lcanbo on a.BSTH equals cb.MaCB into kq
                                          from kq1 in kq.DefaultIfEmpty()
                                          select new BN
                                          {
                                              mabnhan = a.MaBNhan,
                                              tenbnhan = a.TenBNhan,
                                              gtinh = Convert.ToInt32(a.GTinh),
                                              nu = a.GTinh == 0 ? a.Tuoi.ToString() : "",
                                              nam = a.GTinh == 1 ? a.Tuoi.ToString() : "",
                                              noitru = Convert.ToInt32(a.NoiTru.ToString()),
                                              bhyt = a.DTuong == "BHYT" ? "X" : "",
                                              khac = a.DTuong == "BHYT" ? "" : "X",
                                              diachi = a.DiaChi,
                                              madv = a.MaDV == null ? 0 : a.MaDV.Value,
                                              yeucau = a.TenDV,
                                              ketqua = a.KetQua,
                                              makp = a.MakP,
                                              noigui = a.TenKP,
                                              bsth = kq1 != null ? kq1.TenCB : "",
                                              ngayth = a.NgayTH == null ? "00/00" : a.NgayTH.ToString().Substring(0, 5),
                                              loai = Convert.ToInt32(a.Loai)
                                          }).ToList();
                        _BNhan.AddRange(lbntc);
                        
                    }
                    if (chkBNkhongCLS.Checked == true)
                    {
                        var qdt1 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                    join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                    join bn in data.BenhNhans.Where(p => cboDTuong.SelectedIndex == 2 || p.DTuong == _dtuong) on vp.MaBNhan equals bn.MaBNhan
                                    join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan
                                    select new { vpct.MaDV, vpct.idVPhict, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, rv.MaKP, vp.MaCB, vp.NgayTT }).ToList();
                        var qdt3 = (from kp in _lKhoaP
                                    join qd in qdt1 on kp.makp equals qd.MaKP
                                    join dv in dichvu on qd.MaDV equals dv.MaDV
                                    group new { qd, dv } by new { qd.idVPhict, qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DChi, qd.GTinh, qd.Tuoi, qd.DTuong, qd.MaDV, dv.TenDV, dv.Loai, kp.makp, kp.tenkp, qd.MaCB, qd.NgayTT } into kq
                                    select new { kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.TenBNhan, kq.Key.DChi, kq.Key.GTinh, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.MaDV, kq.Key.TenDV, kq.Key.Loai, kq.Key.makp, kq.Key.tenkp, kq.Key.MaCB, kq.Key.NgayTT }).OrderBy(p => p.MaBNhan).ToList();
                       
                        var qdt = (from dtc in qdt3
                                   where !(from dtcta in _BNhan select dtcta.mabnhan).Contains(dtc.MaBNhan)
                                   select dtc).ToList();
                        if (qdt.Count > 0)
                        {
                            foreach (var a in qdt)
                            {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh);
                                if (a.GTinh == 0)
                                {
                                    themmoi.nu = a.Tuoi.ToString();
                                }
                                else { themmoi.nu = ""; }
                                if (a.GTinh == 1)
                                {
                                    themmoi.nam = a.Tuoi.ToString();
                                }
                                else { themmoi.nam = ""; }
                                themmoi.noitru = Convert.ToInt32(a.NoiTru.ToString());
                                // themmoi.dtuong = a.DTuong;
                                if (a.DTuong == "BHYT")
                                {
                                    themmoi.bhyt = "X";
                                }
                                else { themmoi.khac = "X"; }
                                themmoi.diachi = a.DChi;
                                themmoi.madv = a.MaDV ?? 0;
                                themmoi.yeucau = a.TenDV;
                                themmoi.makp = a.makp;
                                themmoi.noigui = a.tenkp;
                                if (a.MaCB != null)
                                {
                                    var a1 = (from k in _lcanbo.Where(p => p.MaCB == a.MaCB) select new { k.TenCB }).ToList();
                                    if (a1.Count > 0)
                                    { themmoi.bsth = a1.First().TenCB; }
                                }
                                if (a.NgayTT != null && a.NgayTT.ToString().Length >= 10)
                                {
                                    themmoi.ngayth = a.NgayTT.ToString().Substring(0, 5);
                                   
                                }
                                else
                                {
                                    themmoi.ngayth = "00/00";                                   
                                }
                                themmoi.loai = a.Loai ?? 0;
                                _BNhan.Add(themmoi);
                            }
                        }
                    }
                }
                #endregion

                var qcd = (from bn in _BNhan
                           join bnkb in data.BNKBs on bn.mabnhan equals bnkb.MaBNhan
                           select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, bnkb.BenhKhac }).ToList();
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
                                    b.chandoan = a.ChanDoan.ToString() + " " + a.BenhKhac.ToString();
                                }

                            }
                        }
                    }
                }
                int _bn1 = -1; int _bn2 = -1;
                {
                    if (radBN.SelectedIndex == 0) { _bn1 = 1; _bn2 = 0; }
                    if (radBN.SelectedIndex == 1) { _bn1 = 1; _bn2 = -1; }
                    if (radBN.SelectedIndex == 2) { _bn2 = 0; _bn1 = -1; }
                }
                var _BN = (from bn in _BNhan.Where(p => p.noitru == _bn1 || p.noitru == _bn2)
                           group new { bn } by new { bn.makp, bn.noigui, bn.mabnhan, bn.tenbnhan, bn.diachi, bn.gtinh, bn.nam, bn.nu, bn.bhyt, bn.khac, bn.chandoan, bn.pl, bn.noitru, bn.madv, bn.yeucau, bn.ketqua, bn.bsth, bn.ngayth, } into kq
                           select new { kq.Key.makp, kq.Key.noigui, kq.Key.mabnhan, kq.Key.tenbnhan, kq.Key.diachi, kq.Key.gtinh, kq.Key.nam, kq.Key.nu, kq.Key.bhyt, kq.Key.khac, kq.Key.chandoan, loai = kq.Sum(p => p.bn.loai), kq.Key.pl, kq.Key.noitru, kq.Key.madv, kq.Key.ketqua, kq.Key.yeucau, kq.Key.bsth, kq.Key.ngayth }).ToList();


                string _tenso = "";
                if (radioGroup1.SelectedIndex == 0)
                {
                    _tenso = ("Sổ siêu âm").ToUpper();
                }
                if (radioGroup1.SelectedIndex == 1)
                {
                    _tenso = ("Sổ X-Quang").ToUpper();
                }
                if (radioGroup1.SelectedIndex == 2)
                {
                    _tenso = ("Sổ Điện tim").ToUpper();
                }
               
                if (radMau.SelectedIndex == 0)
                {

                    BaoCao.Rep_SoSieuAm_A4 rep = new BaoCao.Rep_SoSieuAm_A4();
                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);

                    if (lupDichVu.EditValue == null || Convert.ToInt32(lupDichVu.EditValue) == 0)
                    {

                        rep.DataSource = _BN.OrderBy(p => p.ngayth.ToString().Substring(3, 2)).ThenBy(p => p.ngayth.ToString().Substring(0, 2)).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                        rep.Nam.Value = _BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count();
                        rep.Nu.Value = _BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count();
                        rep.BHYT.Value = _BN.Where(p => p.bhyt == "X").Select(p => p.mabnhan).Count();
                        rep.Khac.Value = _BN.Where(p => p.khac == "X").Select(p => p.mabnhan).Count();
                        int LSPhim = _BN.Sum(p => p.loai);
                        rep.YC.Value = LSPhim;
                        rep.TenSo.Value = _tenso;
                    }
                    else
                    {
                        int madv = Convert.ToInt32(lupDichVu.EditValue);
                        rep.DataSource = _BN.Where(p => p.madv == madv).OrderBy(p => p.ngayth.ToString().Substring(3, 2)).ThenBy(p => p.ngayth.ToString().Substring(0, 2)).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                        rep.Nam.Value = _BN.Where(p => p.madv == madv).Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count();
                        rep.Nu.Value = _BN.Where(p => p.madv == madv).Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count();
                        rep.BHYT.Value = _BN.Where(p => p.madv == madv).Where(p => p.bhyt == "X").Select(p => p.mabnhan).Count();
                        rep.Khac.Value = _BN.Where(p => p.madv == madv).Where(p => p.khac == "X").Select(p => p.mabnhan).Count();
                        rep.YC.Value = _BN.Where(p => p.madv == madv).Sum(p => p.loai);
                        rep.TenSo.Value = _tenso;
                    }
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                if (radMau.SelectedIndex == 1)
                {
                    BaoCao.Rep_SoSieuAm rep = new BaoCao.Rep_SoSieuAm();
                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                    rep.TenSo.Value = _tenso;

                    if (lupDichVu.EditValue == null || Convert.ToInt32(lupDichVu.EditValue) == 0)
                    {
                        rep.DataSource = _BN.OrderBy(p => p.ngayth.ToString().Substring(3, 2)).ThenBy(p => p.ngayth.ToString().Substring(0, 2)).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();

                    }
                    else
                    {
                        int madv = Convert.ToInt32(lupDichVu.EditValue);
                        rep.DataSource = _BN.Where(p => p.madv == madv).OrderBy(p => p.ngayth.ToString().Substring(3, 2)).ThenBy(p => p.ngayth.ToString().Substring(0, 2)).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();

                    }
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

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int idtieunhom = 0;
            //if (radioGroup1.SelectedIndex == 0)
            //{
            //    var a = (from dv in data.DichVus
            //             join tn in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Siêu âm")) on dv.IdTieuNhom equals tn.IdTieuNhom
            //             select dv).ToList();
            //    lupDichVu.Properties.DataSource = a;
            //}
            //else {
            //    var a = (from dv in data.DichVus
            //             join tn in data.TieuNhomDVs.Where(p => p.TenRG.Contains("X-Quang")) on dv.IdTieuNhom equals tn.IdTieuNhom
            //             select dv).ToList();
            //    lupDichVu.Properties.DataSource = a;
            //}
            string _cdha = "";
            if (radioGroup1.SelectedIndex == 0)
            {
                _cdha = "Siêu âm";

            }
            if (radioGroup1.SelectedIndex == 1)
            {
                _cdha = "X-Quang";
            }
            if (radioGroup1.SelectedIndex == 2)
            {
                _cdha = "Điện tim";
            } _ldv.Clear();
            var qdv = from dv in data.DichVus.Where(p => p.PLoai != 1)
                      join tn in data.TieuNhomDVs.Where(p => p.TenRG == _cdha) on dv.IdTieuNhom equals tn.IdTieuNhom
                      select new { dv.MaDV, dv.TenDV, dv.TenRG };
            if (qdv.Count() > 0)
            {
                DV them1 = new DV();
                them1.madv = 0;
                them1.tendv = "Tất cả";
                _ldv.Add(them1);
                foreach (var a in qdv)
                {
                    DV themmoi = new DV();
                    themmoi.madv = a.MaDV;
                    themmoi.tendv = a.TenDV;
                    _ldv.Add(themmoi);
                }
            }
            if (_ldv.Count() > 0)
            {
                lupDichVu.Properties.DataSource = _ldv;
            }

        }

        private void radioGroup1_MouseClick(object sender, MouseEventArgs e)
        {
            //    string _cdha = "";
            //    if (radioGroup1.SelectedIndex == 0)
            //    {
            //        _cdha = "Siêu âm";

            //    }
            //    else
            //    {
            //        _cdha = "X-Quang";
            //    }
            //    _ldv.Clear();
            //    var qdv = from dv in data.DichVus.Where(p => p.PLoai != 1)
            //              join tn in data.TieuNhomDVs.Where(p=>p.TenRG==_cdha) on dv.IdTieuNhom equals tn.IdTieuNhom
            //              select new { dv.MaDV, dv.TenDV, dv.TenRG };
            //    if (qdv.Count() > 0)
            //    {
            //        DV them1 = new DV();
            //        them1.madv = " ";
            //        them1.tendv = "Tất cả";
            //        them1.tenrg = " ";
            //        _ldv.Add(them1);
            //        foreach (var a in qdv)
            //        {
            //            DV themmoi = new DV();
            //            themmoi.madv = a.MaDV;
            //            themmoi.tendv = a.TenDV;
            //            themmoi.tenrg = a.TenRG;
            //            _ldv.Add(themmoi);
            //        }
            //    }
            //    if (_ldv.Count() > 0)
            //    {
            //        lupDichVu.Properties.DataSource = _ldv;
            //    }

        }
    }
}