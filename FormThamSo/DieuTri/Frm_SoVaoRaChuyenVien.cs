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
    public partial class Frm_SoVaoRaChuyenVien : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoVaoRaChuyenVien()
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
            //if (lupKhoa.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn khoa phòng");
            //    lupKhoa.Focus();
            //    return false;
            //}
            return true;
        }
        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            string phanLoai;

            public string PhanLoai
            {
                get { return phanLoai; }
                set { phanLoai = value; }
            }
        }
        List<KPhong> _Kphong = new List<KPhong>();

        private void Frm_SoVaoRaChuyenVien_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.Focus();
            radBN.SelectedIndex = 2;
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            //if (DungChung.Bien.MaBV == "12121")
            //    ck12121.Checked = true;
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Toàn viện";
                themmoi1.makp = 0;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    _Kphong.Add(themmoi);
                }
                lupKhoa.Properties.DataSource = _Kphong.ToList();
            }
            if (lupKhoa.Text == "Toàn viện")
            {
                tkvaokhoa.Enabled = false;
                tkvaokhoa.Checked = false;
                tkchuyendi.Enabled = false;
                tkchuyendi.Checked = false;
                tkchuyenden.Enabled = false;
                tkchuyenden.Checked = false;
            }
            else
            {
                tkvaokhoa.Enabled = true;
            }
            if (DungChung.Bien.MaBV == "20001")
            {
                checkBNDK.Visible = true;
            }
            else
            {
                checkBNDK.Visible = false;
            }
        }
        #region class RVCV
        private class RVCV
        {
            public string chanDoan { get; set; }
            private string soba, solt;
            public string SoBA
            { set { soba = value; } get { return soba; } }
            public string SoVV { get; set; }
            public string SoLT
            { set { solt = value; } get { return solt; } }
            private int MaBNhan;

            public int MaBNhan1
            {
                get { return MaBNhan; }
                set { MaBNhan = value; }
            }
            private string TenBNhan;

            public string TenBNhan1
            {
                get { return TenBNhan; }
                set { TenBNhan = value; }
            }
            private int MaKP;

            public int MaKP1
            {
                get { return MaKP; }
                set { MaKP = value; }
            }
            private string Nam;

            public string Nam1
            {
                get { return Nam; }
                set { Nam = value; }
            }
            private string Nu;

            public string Nu1
            {
                get { return Nu; }
                set { Nu = value; }
            }
            private string CVC;

            public string CVC1
            {
                get { return CVC; }
                set { CVC = value; }
            }
            private string BHYT;

            public string BHYT1
            {
                get { return BHYT; }
                set { BHYT = value; }
            }
            private string TE12;

            public string TE121
            {
                get { return TE12; }
                set { TE12 = value; }
            }
            private string TE15;

            public string TE151
            {
                get { return TE15; }
                set { TE15 = value; }
            }
            private string NgheNghiep;

            public string NgheNghiep1
            {
                get { return NgheNghiep; }
                set { NgheNghiep = value; }
            }
            private string DiaChi;

            public string DiaChi1
            {
                get { return DiaChi; }
                set { DiaChi = value; }
            }
            private string NoiGT;

            public string NoiGT1
            {
                get { return NoiGT; }
                set { NoiGT = value; }
            }
            private string MaBV;

            public string MaBV1
            {
                get { return MaBV; }
                set { MaBV = value; }
            }
            private DateTime? VV;

            public DateTime? VV1
            {
                get { return VV; }
                set { VV = value; }
            }
            private DateTime? CV;

            public DateTime? CV1
            {
                get { return CV; }
                set { CV = value; }
            }
            private DateTime? RV;

            public DateTime? RV1
            {
                get { return RV; }
                set { RV = value; }
            }
            private DateTime? RV2;

            public DateTime? RV3
            {
                get { return RV2; }
                set { RV2 = value; }
            }
            private DateTime? TV;

            public DateTime? TV1
            {
                get { return TV; }
                set { TV = value; }
            }
            private DateTime? ngayKham;

            public DateTime? NgayKham
            {
                get { return ngayKham; }
                set { ngayKham = value; }
            }
            private string CDTD;

            public string CDTD1
            {
                get { return CDTD; }
                set { CDTD = value; }
            }
            private string CDKKB;

            public string CDKKB1
            {
                get { return CDKKB; }
                set { CDKKB = value; }
            }
            private string CDKDT;

            public string CDKDT1
            {
                get { return CDKDT; }
                set { CDKDT = value; }
            }
            private string Khoi;

            public string Khoi1
            {
                get { return Khoi; }
                set { Khoi = value; }
            }
            private string DoGiam;

            public string DoGiam1
            {
                get { return DoGiam; }
                set { DoGiam = value; }
            }
            private string NangHon;

            public string NangHon1
            {
                get { return NangHon; }
                set { NangHon = value; }
            }
            private string KoTD;

            public string KoTD1
            {
                get { return KoTD; }
                set { KoTD = value; }
            }
            private double SoNgaydt;

            public double SoNgaydt1
            {
                get { return SoNgaydt; }
                set { SoNgaydt = value; }
            }
            private int NoiTru;

            public int NoiTru1
            {
                get { return NoiTru; }
                set { NoiTru = value; }
            }
            private int PAn;

            public int PAn1
            {
                get { return PAn; }
                set { PAn = value; }
            }

            public string TE3Tuoi { get; set; }

            public string CapCuu { get; set; }

            public string NThan { get; set; }

            public int? KPVV { get; set; }

            public DateTime? NgayChuyenKhoa { get; set; }

            public string CDVV { get; set; }

            public string CDRV { get; set; }

            public string KQTuVong { get; set; }

            public string MaHuyen { get; set; }

            public string ThanhThi { get; set; }

            public string NongThon { get; set; }

            public string CongVienChuc { get; set; }

            public string KhoaVV { get; set; }
            public string KPChuyen { get; set; }
            public int? MaKhoaRV { get; set; }
            public string TenKhoaRV { get; set; }
        }
        #endregion
        List<RVCV> _lrvcv = new List<RVCV>();
        List<RaVien> _lRaVien = new List<RaVien>();
        List<VaoVien> _lVaoVien = new List<VaoVien>();
        List<BenhNhan> _lBenhNhan = new List<BenhNhan>();
        List<BNKB> _lBNKB = new List<BNKB>();
        List<KPhong> _lKPhong = new List<KPhong>();

        private bool Duoithang(DateTime nnhap, string ngaysinh, string thangsinh, string namsinh)
        {
            try
            {
                if (ngaysinh != null && ngaysinh.Trim() != "" && Convert.ToInt32(ngaysinh) > 0 && Convert.ToInt32(ngaysinh) <= 31 && thangsinh != null && thangsinh.Trim() != "" && Convert.ToInt32(thangsinh) > 0 && Convert.ToInt32(thangsinh) <= 12 && namsinh != null && namsinh.Trim() != "")
                {
                    DateTime _ngaysinh = Convert.ToDateTime(ngaysinh + "/" + thangsinh + "/" + namsinh);
                    if ((nnhap - _ngaysinh).TotalDays < 365)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                return false;
            }
            catch
            {

            }
            return false;

        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            string format = "dd/MM/yy";

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string tenKP = "";


            if (KTtaoBc())
            {
                _lrvcv.Clear();
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                int _makp = 0;
                if (lupKhoa.EditValue != null)
                {
                    _makp = Convert.ToInt32(lupKhoa.EditValue);
                }
                _lKPhong = (from kp in data.KPhongs
                            select new KPhong
                            {
                                tenkp = kp.TenKP,
                                makp = kp.MaKP,
                                PhanLoai = kp.PLoai,
                            }).ToList();
                DateTime _ngaytuBN = tungay.AddMonths(-3);
                _lRaVien = data.RaViens.Where(p => DungChung.Bien.MaBV == "12122" ? (p.NgayRa >= _ngaytuBN && p.NgayRa <= denngay) : (p.NgayRa >= tungay && p.NgayRa <= denngay)).ToList();
                _lVaoVien = (from vv in data.VaoViens.Where(p => p.NgayVao >= _ngaytuBN && p.NgayVao <= denngay) select vv).OrderBy(p => p.MaBNhan).ToList();
                _lBenhNhan = data.BenhNhans.Where(p => p.NNhap >= _ngaytuBN && p.NNhap <= denngay).Where(p => (radBN.SelectedIndex == 2 ? (checkBNDK.Checked == false ? (p.NoiTru == 1 || (true)) : (p.NoiTru == 0 || p.NoiTru == 1)) : (radBN.SelectedIndex == 1 ? p.NoiTru == 1 : (p.NoiTru == 0 && p.DTNT)))).ToList();
                _lBNKB = (from kb in data.BNKBs.Where(p => p.NgayKham >= _ngaytuBN && p.NgayKham <= denngay)
                          select kb).ToList();
                var bnKB_ngayVao = (from kb in data.BNKBs.Where(p => p.NgayKham >= _ngaytuBN && p.NgayKham <= denngay)

                                    select new { kb.NgayKham, kb.MaKP, kb.MaKPdt, kb.MaBNhan, kb.NgayNghi, kb.PhuongAn }).ToList();
                var qhuyen = data.DmHuyens.ToList();

                #region Lấy BN toàn viện
                if (_makp == 0)
                {
                    foreach (var a in _lVaoVien)
                    {
                        RVCV them = new RVCV();
                        them.MaBNhan1 = a.MaBNhan;
                        //them.TenBNhan1 = a.TenBNhan;
                        them.SoBA = a.SoBA;
                        them.KPVV = a.MaKP;
                        them.SoVV = a.SoVV;
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                            them.SoLT = a.SoBA;
                        them.KPChuyen = data.KPhongs.Where(p => p.MaKP == a.MaKP).Select(p => p.TenKP).ToList().FirstOrDefault();
                        if (a.NgayVao != null)
                        { them.VV1 = a.NgayVao.Value; }
                        them.CDVV = a.ChanDoan;
                        var ngaychuyenkhoa = bnKB_ngayVao.Where(p => p.MaBNhan == a.MaBNhan && p.MaKP == a.MaKP && p.PhuongAn == 3).FirstOrDefault();
                        if (ngaychuyenkhoa != null && (RGChonmau.SelectedIndex != 2 && RGChonmau.SelectedIndex != 3))
                            them.NgayChuyenKhoa = ngaychuyenkhoa.NgayNghi;
                        _lrvcv.Add(them);
                    }

                    foreach (var a in _lRaVien)
                    {
                        bool _kt = false;

                        var check = _lrvcv.FirstOrDefault(o => o.MaBNhan1 == a.MaBNhan);
                        if (check != null)
                        {
                            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && string.IsNullOrWhiteSpace(a.SoLT))
                            {
                                check.SoLT = check.SoBA;
                            }
                            else
                                check.SoLT = a.SoLT;
                            if (a.Status == 1 && (DungChung.Bien.MaBV == "12122" ? (a.NgayVao != null || a.NgayVao == null) : (a.NgayVao != null)) && a.NgayVao.ToString().Length > 5) { check.CV1 = a.NgayRa.Value; }
                            if ((a.Status == 2 || a.Status == 3) && a.KetQua != "Tử vong" && a.NgayRa != null) { check.RV1 = a.NgayRa.Value; }
                            if (a.Status == 2 && a.KetQua == "Tử vong" && a.NgayRa != null) { check.TV1 = a.NgayRa.Value; }
                            if (a.KetQua == "Đỡ|Giảm") { check.DoGiam1 = "X"; }
                            if (a.KetQua == "Không T.đổi") { check.KoTD1 = "X"; }
                            if (a.KetQua == "Khỏi") { check.Khoi1 = "X"; }
                            if (a.KetQua == "Nặng hơn") { check.NangHon1 = "X"; }
                            if (a.KetQua == "Tử vong") { check.KQTuVong = "X"; }
                            check.SoNgaydt1 = a.SoNgaydt ?? 0;
                            if (a.Status == 1)
                            {
                                check.CV1 = a.NgayRa;
                            }
                            else
                                check.RV1 = a.NgayRa;

                            var krv = data.KPhongs.FirstOrDefault(o => o.MaKP == a.MaKP);
                            if (krv != null)
                            {
                                check.MaKhoaRV = krv.MaKP;
                                check.TenKhoaRV = krv.TenKP;
                            }

                            _kt = true;
                        }
                        //foreach (var b in _lrvcv)
                        //{
                        //    if (a.MaBNhan == b.MaBNhan1)
                        //    {
                        //        b.SoLT = a.SoLT;
                        //        if (a.Status == 1 && (DungChung.Bien.MaBV == "12122" ? (a.NgayVao != null || a.NgayVao == null) : (a.NgayVao != null)) && a.NgayVao.ToString().Length > 5) { b.CV1 = a.NgayRa.Value; }
                        //        if ((a.Status == 2 || a.Status == 3) && a.KetQua != "Tử vong" && a.NgayRa != null) { b.RV1 = a.NgayRa.Value; }
                        //        if (a.Status == 2 && a.KetQua == "Tử vong" && a.NgayRa != null) { b.TV1 = a.NgayRa.Value; }
                        //        if (a.KetQua == "Đỡ|Giảm") { b.DoGiam1 = "X"; }
                        //        if (a.KetQua == "Không T.đổi") { b.KoTD1 = "X"; }
                        //        if (a.KetQua == "Khỏi") { b.Khoi1 = "X"; }
                        //        if (a.KetQua == "Nặng hơn") { b.NangHon1 = "X"; }
                        //        if (a.KetQua == "Tử vong") { b.KQTuVong = "X"; }
                        //        b.SoNgaydt1 = a.SoNgaydt ?? 0;
                        //        if (a.Status == 1)
                        //        {
                        //            b.CV1 = a.NgayRa;
                        //        }
                        //        else
                        //            b.RV1 = a.NgayRa;

                        //        var krv = data.KPhongs.FirstOrDefault(o => o.MaKP == a.MaKP);
                        //        if (krv != null)
                        //        {
                        //            b.MaKhoaRV = krv.MaKP;
                        //            b.TenKhoaRV = krv.TenKP;
                        //        }

                        //        _kt = true;
                        //        break;
                        //    }
                        //}
                        if (_kt == false)
                        {
                            RVCV them = new RVCV();
                            them.MaBNhan1 = a.MaBNhan;
                            //them.TenBNhan1 = a.TenBNhan;
                            them.SoLT = a.SoLT;
                            them.CDRV = a.ChanDoan;
                            if (a.Status == 1 && (RGChonmau.SelectedIndex == 2 ? (a.NgayVao != null || a.NgayVao == null) : (a.NgayVao != null)) && a.NgayVao.ToString().Length > 5) { them.CV1 = a.NgayRa.Value; }
                            if ((a.Status == 2 || a.Status == 3) && a.KetQua != "Tử vong" && a.NgayRa != null) { them.RV1 = a.NgayRa.Value; }
                            if (a.Status == 2 && a.KetQua == "Tử vong" && a.NgayRa != null) { them.TV1 = a.NgayRa.Value; }
                            if (a.KetQua == "Đỡ|Giảm") { them.DoGiam1 = "X"; }
                            if (a.KetQua == "Không T.đổi") { them.KoTD1 = "X"; }
                            if (a.KetQua == "Khỏi") { them.Khoi1 = "X"; }
                            if (a.KetQua == "Nặng hơn") { them.NangHon1 = "X"; }
                            if (a.KetQua == "Tử vong") { them.KQTuVong = "X"; }
                            them.SoNgaydt1 = Convert.ToInt32(a.SoNgaydt);
                            if (a.Status == 1)
                                them.CV1 = a.NgayRa;
                            else
                                them.RV1 = a.NgayRa;
                            var krv = data.KPhongs.FirstOrDefault(o => o.MaKP == a.MaKP);
                            if (krv != null)
                            {
                                them.MaKhoaRV = krv.MaKP;
                                them.TenKhoaRV = krv.TenKP;
                            }
                            _lrvcv.Add(them);

                        }
                    }
                    if (checkBNDK.Checked == true)
                    {
                        foreach (var a in _lBenhNhan)
                        {
                            bool _kt = false;
                            foreach (var b in _lrvcv)
                            {
                                if (a.MaBNhan == b.MaBNhan1)
                                {
                                    _kt = true;
                                    break;
                                }
                            }
                            if (_kt == false)
                            {
                                RVCV them = new RVCV();
                                them.MaBNhan1 = a.MaBNhan;
                                _lrvcv.Add(them);

                            }
                        }
                    }
                    // tính bệnh nhân tuổi < 12 tháng
                    foreach (var item in _lBenhNhan)
                    {
                        string X = Duoithang(item.NNhap ?? DateTime.Now, item.NgaySinh ?? "", item.ThangSinh ?? "", item.NamSinh ?? "") ? "X" : "";
                        item.Ma_lk = X;

                    }

                    //
                    _lrvcv = (from rv in _lrvcv
                              join bn in _lBenhNhan on rv.MaBNhan1 equals bn.MaBNhan
                              join ttbs in data.TTboXungs on bn.MaBNhan equals ttbs.MaBNhan
                              join nn in data.DmNNs on ttbs.MaNN equals nn.MaNN into k
                              from k1 in k.DefaultIfEmpty()
                              select new RVCV
                              {
                                  MaBNhan1 = rv.MaBNhan1,
                                  TenBNhan1 = bn.TenBNhan,
                                  NgayKham = bn.NNhap,
                                  Nam1 = bn.GTinh == 1 ? bn.Tuoi.ToString() : "",
                                  Nu1 = bn.GTinh == 0 ? bn.Tuoi.ToString() : "",
                                  BHYT1 = bn.DTuong == "BHYT" ? "X" : "",
                                  TE121 = bn.Ma_lk,
                                  TE3Tuoi = (bn.Tuoi <= 3 && string.IsNullOrEmpty(bn.Ma_lk)) ? "X" : "",
                                  TE151 = (bn.Tuoi > 3 && bn.Tuoi <= 15 && string.IsNullOrEmpty(bn.Ma_lk)) ? "X" : "",
                                  DiaChi1 = bn.DChi,
                                  CapCuu = bn.CapCuu == 1 ? "X" : "",
                                  NThan = ttbs.NThan,
                                  MaBV1 = bn.MaBV,
                                  CDTD1 = bn.CDNoiGT,
                                  SoLT = ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && bn.NoiTru == 0 && bn.DTNT) ? bn.SoHSBA : rv.SoLT,
                                  SoBA = ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && bn.NoiTru == 0 && bn.DTNT) ? bn.SoHSBA : rv.SoBA,
                                  SoVV = rv.SoVV,
                                  VV1 = rv.VV1,
                                  CV1 = rv.CV1,
                                  RV1 = rv.RV1,
                                  TV1 = rv.TV1,
                                  CDVV = rv.CDVV,
                                  KPVV = rv.KPVV,
                                  // CDKKB1 = rv.CDKKB1,
                                  DoGiam1 = rv.DoGiam1,
                                  KoTD1 = rv.KoTD1,
                                  Khoi1 = rv.Khoi1,
                                  NangHon1 = rv.NangHon1,
                                  SoNgaydt1 = rv.SoNgaydt1,
                                  NgayChuyenKhoa = rv.NgayChuyenKhoa,
                                  KQTuVong = rv.KQTuVong,
                                  NgheNghiep1 = k1 == null ? "" : k1.TenNN,
                                  MaHuyen = ttbs.MaHuyen == null ? "" : ttbs.MaHuyen,
                                  CongVienChuc = k1 == null ? "" : (k1.CongVienChuc == 1 ? "X" : ""),
                                  KPChuyen = rv.KPChuyen,
                                  MaKhoaRV = rv.MaKhoaRV,
                                  TenKhoaRV = rv.TenKhoaRV
                              }).ToList();
                    var qkb = (from idkb in _lBNKB
                               join bnkb in _lKPhong on idkb.MaKP equals bnkb.makp
                               select new { idkb.IDKB, idkb.MaBNhan, idkb.MaICD, idkb.NgayKham, bnkb.PhanLoai, idkb.ChanDoan }).OrderBy(p => p.IDKB).ToList();

                    foreach (var b in _lrvcv)
                    {
                        var check = qkb.FirstOrDefault(o => o.MaBNhan == b.MaBNhan1);
                        if (check != null)
                        {
                            if (check.PhanLoai == "Phòng khám") { b.CDKKB1 = check.MaICD; b.chanDoan = check.ChanDoan; }
                            if (check.PhanLoai == "Lâm sàng" && b.SoNgaydt1 >= 1) { b.CDKDT1 = DungChung.Bien.MaBV == "20001" ? DungChung.Ham.FreshString(check.MaICD) : check.ChanDoan; }
                        }
                        //foreach (var a in qkb)
                        //{
                        //    if (a.MaBNhan == b.MaBNhan1)
                        //    {
                        //        //  b.VV1 = a.NgayVao.ToString().Substring(0, 5);              
                        //        if (a.PhanLoai == "Phòng khám") { b.CDKKB1 = a.MaICD; b.chanDoan = a.ChanDoan; }
                        //        if (a.PhanLoai == "Lâm sàng" && b.SoNgaydt1 >= 1) { b.CDKDT1 = DungChung.Bien.MaBV == "20001" ? DungChung.Ham.FreshString(a.MaICD) : a.ChanDoan; }
                        //        //b.NgayKham = a.NgayKham;
                        //    }
                        //}
                        b.ThanhThi = "";
                        b.NongThon = "X";
                        b.KhoaVV = _lKPhong.Where(p => p.makp == b.KPVV).Select(p => p.tenkp).FirstOrDefault();
                        if (b.MaHuyen != "")
                        {
                            var thanhthi = qhuyen.Where(p => p.MaHuyen == b.MaHuyen).Select(p => p.ThanhThi == true).FirstOrDefault();
                            if (thanhthi != null)
                            {
                                b.ThanhThi = "X";
                                b.NongThon = "";
                            }
                        }
                    }
                    if (checkBNDK.Checked == false)
                        _lrvcv = _lrvcv.Where(p => (Convert.ToDateTime(p.VV1) >= _ngaytuBN && Convert.ToDateTime(p.VV1) <= denngay)
                           || (Convert.ToDateTime(p.RV1) >= _ngaytuBN && Convert.ToDateTime(p.RV1) <= denngay)
                           || (Convert.ToDateTime(p.CV1) >= _ngaytuBN && Convert.ToDateTime(p.CV1) <= denngay)
                           || (Convert.ToDateTime(p.TV1) >= _ngaytuBN && Convert.ToDateTime(p.TV1) <= denngay)
                           ).ToList();

                }

                #endregion
                #region Lấy BN theo khoa phòng
                else
                {
                    var kp = _lKPhong.Where(p => p.makp == _makp).Select(p => new { p.tenkp }).ToList();
                    if (kp.Count > 0)
                    {
                        tenKP = kp.First().tenkp.ToUpper();
                        //rep.Khoa.Value = kp.First().tenkp.ToUpper(); 
                    }
                    List<RVCV> _lbnbk1 = new List<RVCV>();
                    var _lbnbk11 = (from a in data.BNKBs.Where(p => p.NgayKham <= denngay && p.NgayKham >= _ngaytuBN) select new { a.MaBNhan, a.MaKP, a.NgayNghi, a.NgayKham }).ToList();
                    foreach (var item in _lbnbk11)
                    {
                        RVCV them = new RVCV();
                        them.MaBNhan1 = item.MaBNhan ?? 0;
                        them.MaKP1 = item.MaKP ?? 0;
                        them.NgayKham = item.NgayKham;
                        if (item.NgayNghi == null || item.NgayNghi > denngay)
                            them.SoNgaydt1 = (denngay.Date - item.NgayKham.Value.Date).Days + 1;
                        else
                            them.SoNgaydt1 = (item.NgayNghi.Value.Date - item.NgayKham.Value.Date).Days;
                        _lbnbk1.Add(them);
                    }
                    _lbnbk1 = (from a in _lbnbk1 group a by new { a.MaBNhan1, a.MaKP1 } into kq select new RVCV { MaBNhan1 = kq.Key.MaBNhan1, MaKP1 = kq.Key.MaKP1, SoNgaydt1 = kq.Sum(p => p.SoNgaydt1) }).ToList();
                    if (checkBNDK.Checked == false)
                    {
                        _lbnbk1 = (from a in _lbnbk1
                                   join b in _lVaoVien on a.MaBNhan1 equals b.MaBNhan
                                   join c in _lRaVien on a.MaBNhan1 equals c.MaBNhan into k
                                   from k1 in k.DefaultIfEmpty()
                                   select new RVCV { MaBNhan1 = a.MaBNhan1, MaKP1 = a.MaKP1, SoNgaydt1 = (a.MaKP1 == b.MaKP) ? (a.SoNgaydt1 + 1) : ((k1 != null && k1.MaKP == a.MaKP1) ? (a.SoNgaydt1 + 1) : a.SoNgaydt1) }).ToList();
                    }
                    else
                    {
                        //_lbnbk1 = (from a in _lbnbk1
                        //           join b in _lVaoVien on a.MaBNhan1 equals b.MaBNhan into kk
                        //           join c in _lRaVien on a.MaBNhan1 equals c.MaBNhan into k
                        //           from k1 in k.DefaultIfEmpty()
                        //           from kk1 in kk.DefaultIfEmpty()
                        //           select new RVCV { MaBNhan1 = a.MaBNhan1, MaKP1 = a.MaKP1, SoNgaydt1 = kk1 != null ? ((a.MaKP1 == kk1.MaKP) ? (a.SoNgaydt1 + 1) : ((k1 != null && k1.MaKP == a.MaKP1) ? (a.SoNgaydt1 + 1) : a.SoNgaydt1)) : 0 }).ToList();
                    }
                    var BNKBMax1 = (from a in _lBNKB group a by new { a.MaBNhan } into pq select new { pq.Key.MaBNhan, IDKB = pq.Max(p => p.IDKB) }).ToList();
                    var BNKB11 = (from a in _lBNKB
                                  join m in BNKBMax1 on a.IDKB equals m.IDKB
                                  select a).ToList();
                    var BNKBMax = (from a in _lBNKB where (a.MaKP == _makp) group a by new { a.MaBNhan } into pq select new { pq.Key.MaBNhan, IDKB = pq.Max(p => p.IDKB) }).ToList();
                    var BNKB1 = (from a in _lBNKB.Where(p => p.MaKP == _makp)
                                 join m in BNKBMax on a.IDKB equals m.IDKB
                                 select a).ToList();
                    var x1 = (from a in _lVaoVien
                              join b in data.KPhongs on a.MaKP equals b.MaKP
                              select new { a, TenKP = b.TenKP }).ToList();
                    var abc = (from a in BNKB1
                               join vv in x1 on a.MaBNhan equals vv.a.MaBNhan into k
                               from k1 in k.DefaultIfEmpty()
                               select new { a.MaBNhan, a.NgayKham, a.NgayNghi, a.PhuongAn, vv = k1 != null ? k1.a : null, TenKP = k1 != null ? k1.TenKP : "", a.MaKP }).ToList();
                    foreach (var a in abc)
                    {
                        RVCV them = new RVCV();
                        them.MaBNhan1 = a.MaBNhan ?? 0;
                        //them.TenBNhan1 = a.TenBNhan;
                        them.SoBA = a.vv != null ? a.vv.SoBA : "";
                        them.SoVV = a.vv != null ? a.vv.SoVV : "";
                        them.CDVV = a.vv != null ? a.vv.ChanDoan : "";
                        them.KPVV = a.vv != null ? a.vv.MaKP : null;
                        them.KPChuyen = a.TenKP;
                        if (RGChonmau.SelectedIndex == 2 || RGChonmau.SelectedIndex == 1 || RGChonmau.SelectedIndex == 3)
                        {
                            if (a.PhuongAn == 3)
                            {
                                them.NgayChuyenKhoa = a.NgayNghi;
                            }
                        }
                        else
                            them.NgayChuyenKhoa = a.NgayNghi;

                        if (a.vv != null && a.vv.MaKP == _makp)
                        {
                            if (a.vv.NgayVao != null)
                            { them.VV1 = a.vv.NgayVao.Value; }
                        }
                        else
                        {
                            if (a.NgayKham != null)
                            {
                                if (RGChonmau.SelectedIndex == 2 || RGChonmau.SelectedIndex == 3)
                                    them.RV3 = a.NgayKham;
                                them.VV1 = a.NgayKham.Value;

                            }
                        }

                        if (a.NgayNghi != null)
                        {
                            if (a.NgayNghi <= denngay && a.NgayNghi >= tungay)
                            {
                                if ((RGChonmau.SelectedIndex != 2 && RGChonmau.SelectedIndex != 3))
                                {
                                    if (DungChung.Bien.MaBV != "20001")
                                        them.RV1 = a.NgayNghi.Value;
                                    them.SoNgaydt1 = (a.NgayNghi.Value.Date - a.NgayKham.Value.Date).Days + 1;
                                }
                                else
                                {
                                    if (BNKB11.Where(p => p.MaBNhan == a.MaBNhan).First().MaKP == _makp)
                                    {
                                        them.SoNgaydt1 = (a.NgayNghi.Value.Date - a.NgayKham.Value.Date).Days + 1;
                                    }
                                    else
                                    {
                                        them.SoNgaydt1 = (a.NgayNghi.Value.Date - a.NgayKham.Value.Date).Days;
                                    }
                                }
                            }
                        }
                        else
                        {
                            them.SoNgaydt1 = (denngay - a.NgayKham.Value.Date).Days + 1;
                        }
                        if (DungChung.Bien.MaBV == "12122")
                        {
                            them.SoNgaydt1 = _lbnbk1.Where(p => p.MaBNhan1 == a.MaBNhan && p.MaKP1 == a.MaKP).Select(p => p.SoNgaydt1).FirstOrDefault();
                        }
                        _lrvcv.Add(them);
                    }
                    _lRaVien = _lRaVien.Where(p => p.MaKP == _makp).ToList();
                    foreach (var a in _lRaVien)
                    {
                        bool _kt = false;
                        var check = _lrvcv.FirstOrDefault(o => o.MaBNhan1 == a.MaBNhan);
                        if (check != null)
                        {
                            check.SoLT = a.SoLT;

                            if (a.Status == 1 && ((RGChonmau.SelectedIndex == 2 || RGChonmau.SelectedIndex == 3 || DungChung.Bien.MaBV == "20001") ? (a.NgayVao != null || a.NgayVao == null) : (a.NgayVao != null)) && a.NgayVao.ToString().Length > 5)
                            { check.CV1 = a.NgayRa.Value; }
                            if ((a.Status == 2 || a.Status == 3) && a.KetQua != "Tử vong" && a.NgayRa != null)
                            { check.RV1 = a.NgayRa.Value; }
                            if (a.Status == 2 && a.KetQua == "Tử vong" && a.NgayRa != null)
                            { check.TV1 = a.NgayRa.Value; }
                            if (a.KetQua == "Đỡ|Giảm")
                            { check.DoGiam1 = "X"; }
                            if (a.KetQua == "Không T.đổi")
                            { check.KoTD1 = "X"; }
                            if (a.KetQua == "Khỏi")
                            { check.Khoi1 = "X"; }
                            if (a.KetQua == "Nặng hơn")
                            { check.NangHon1 = "X"; }
                            if (a.KetQua == "Tử vong")
                            { check.KQTuVong = "X"; }
                            if (a.NgayVao != null && (RGChonmau.SelectedIndex != 3 && RGChonmau.SelectedIndex != 2))
                                check.SoNgaydt1 = DungChung.Bien.MaBV == "12122" ? Convert.ToInt32(check.SoNgaydt1) : ((a.NgayRa.Value.Date - a.NgayVao.Value.Date).Days + 1);
                            var krv = data.KPhongs.FirstOrDefault(o => o.MaKP == a.MaKP);
                            if (krv != null)
                            {
                                check.MaKhoaRV = krv.MaKP;
                                check.TenKhoaRV = krv.TenKP;
                            }
                            _kt = true;
                        }
                        //foreach (var b in _lrvcv)
                        //{
                        //    if (a.MaBNhan == b.MaBNhan1)
                        //    {
                        //        b.SoLT = a.SoLT;

                        //        if (a.Status == 1 && ((RGChonmau.SelectedIndex == 2 || RGChonmau.SelectedIndex == 3 || DungChung.Bien.MaBV == "20001") ? (a.NgayVao != null || a.NgayVao == null) : (a.NgayVao != null)) && a.NgayVao.ToString().Length > 5)
                        //        { b.CV1 = a.NgayRa.Value; }
                        //        if ((a.Status == 2 || a.Status == 3) && a.KetQua != "Tử vong" && a.NgayRa != null)
                        //        { b.RV1 = a.NgayRa.Value; }
                        //        if (a.Status == 2 && a.KetQua == "Tử vong" && a.NgayRa != null)
                        //        { b.TV1 = a.NgayRa.Value; }
                        //        if (a.KetQua == "Đỡ|Giảm")
                        //        { b.DoGiam1 = "X"; }
                        //        if (a.KetQua == "Không T.đổi")
                        //        { b.KoTD1 = "X"; }
                        //        if (a.KetQua == "Khỏi")
                        //        { b.Khoi1 = "X"; }
                        //        if (a.KetQua == "Nặng hơn")
                        //        { b.NangHon1 = "X"; }
                        //        if (a.KetQua == "Tử vong")
                        //        { b.KQTuVong = "X"; }
                        //        if (a.NgayVao != null && (RGChonmau.SelectedIndex != 3 && RGChonmau.SelectedIndex != 2))
                        //            b.SoNgaydt1 = DungChung.Bien.MaBV == "12122" ? Convert.ToInt32(b.SoNgaydt1) : ((a.NgayRa.Value.Date - a.NgayVao.Value.Date).Days + 1);
                        //        _kt = true;
                        //        break;
                        //    }
                        //}
                        if (_kt == false)
                        {
                            RVCV them = new RVCV();
                            them.MaBNhan1 = a.MaBNhan;
                            //them.TenBNhan1 = a.TenBNhan;
                            them.SoLT = a.SoLT;
                            if (a.Status == 1 && ((RGChonmau.SelectedIndex == 2 || RGChonmau.SelectedIndex == 3 || DungChung.Bien.MaBV == "20001") ? (a.NgayVao != null || a.NgayVao == null) : (a.NgayVao != null)) && a.NgayVao.ToString().Length > 5) { them.CV1 = a.NgayRa.Value; }
                            if ((a.Status == 2 || a.Status == 3) && a.KetQua != "Tử vong" && a.NgayRa != null) { them.RV1 = a.NgayRa.Value; }
                            if (a.Status == 2 && a.KetQua == "Tử vong" && a.NgayRa != null) { them.TV1 = a.NgayRa.Value; }
                            if (a.KetQua == "Đỡ|Giảm") { them.DoGiam1 = "X"; }
                            if (a.KetQua == "Không T.đổi") { them.KoTD1 = "X"; }
                            if (a.KetQua == "Khỏi") { them.Khoi1 = "X"; }
                            if (a.KetQua == "Nặng hơn") { them.NangHon1 = "X"; }
                            if (a.KetQua == "Tử vong") { them.KQTuVong = "X"; }
                            them.SoNgaydt1 = Convert.ToInt32(a.SoNgaydt);
                            var krv = data.KPhongs.FirstOrDefault(o => o.MaKP == a.MaKP);
                            if (krv != null)
                            {
                                them.MaKhoaRV = krv.MaKP;
                                them.TenKhoaRV = krv.TenKP;
                            }
                            _lrvcv.Add(them);

                        }
                    }
                    _lrvcv = (from rv in _lrvcv
                              join bn in _lBenhNhan on rv.MaBNhan1 equals bn.MaBNhan
                              select new RVCV
                              {
                                  MaBNhan1 = rv.MaBNhan1,
                                  TenBNhan1 = bn.TenBNhan,
                                  NgayKham = bn.NNhap,
                                  Nam1 = bn.GTinh == 1 ? bn.Tuoi.ToString() : "",
                                  Nu1 = bn.GTinh == 0 ? bn.Tuoi.ToString() : "",
                                  BHYT1 = bn.DTuong == "BHYT" ? "X" : "",
                                  TE121 = bn.Ma_lk,
                                  TE3Tuoi = (bn.Tuoi <= 3 && string.IsNullOrEmpty(bn.Ma_lk)) ? "X" : "",
                                  TE151 = (bn.Tuoi <= 15 && string.IsNullOrEmpty(bn.Ma_lk)) ? "X" : "",
                                  DiaChi1 = bn.DChi,
                                  CapCuu = bn.CapCuu == 1 ? "X" : "",
                                  MaBV1 = bn.MaBV,
                                  CDTD1 = bn.CDNoiGT,
                                  CDVV = rv.CDVV,
                                  NgayChuyenKhoa = rv.NgayChuyenKhoa,
                                  SoLT = rv.SoLT,
                                  SoBA = rv.SoBA,
                                  SoVV = rv.SoVV,
                                  VV1 = rv.VV1,
                                  CV1 = rv.CV1,
                                  RV1 = rv.RV1,
                                  TV1 = rv.TV1,
                                  RV3 = rv.RV3,
                                  DoGiam1 = rv.DoGiam1,
                                  KoTD1 = rv.KoTD1,
                                  Khoi1 = rv.Khoi1,
                                  NangHon1 = rv.NangHon1,
                                  KQTuVong = rv.KQTuVong,
                                  SoNgaydt1 = rv.SoNgaydt1,
                                  KPVV = rv.KPVV,
                                  KPChuyen = rv.KPChuyen,
                                  MaKhoaRV = rv.MaKhoaRV,
                                  TenKhoaRV = rv.TenKhoaRV
                                  //NgheNghiep1 = q1 == null ? "" : q1.TenNN
                              }).ToList();
                    var qkb = (from idkb in _lBNKB.Where(p => p.MaKP == _makp || p.MaKPdt == _makp)
                               join bnkb in _lKPhong on idkb.MaKP equals bnkb.makp
                               select new { idkb.IDKB, idkb.MaBNhan, idkb.MaICD, idkb.NgayKham, bnkb.PhanLoai, idkb.ChanDoan }).OrderBy(p => p.IDKB).ToList();

                    var noigt = data.BenhViens.Select(p => new { p.MaBV, p.TenBV }).ToList();
                    DateTime _ngaytuBN1 = tungay.AddMonths(-4);
                    var ttbx = (from a in data.BenhNhans.Where(p => p.NNhap >= _ngaytuBN1 && p.NNhap <= denngay)
                                join b in data.TTboXungs on a.MaBNhan equals b.MaBNhan
                                select b).ToList(); ;
                    var tt = (from ttbs in ttbx
                              join nn in data.DmNNs on ttbs.MaNN equals nn.MaNN into k
                              from k1 in k.DefaultIfEmpty()
                              select new { ttbs.MaBNhan, TenNN = k1 != null ? k1.TenNN : "", CongVienChuc = k1 != null ? k1.CongVienChuc : 0, ttbs.NThan, ttbs.MaHuyen }).ToList();
                    foreach (var b in _lrvcv)
                    {
                        if (b.VV1 == null)
                        {
                            var ngayvao = _lVaoVien.Where(p => p.MaKP == _makp && p.MaBNhan == b.MaBNhan1).Select(p => p.NgayVao).ToList();
                            if (ngayvao.Count > 0 && ngayvao.First() != null)
                                b.VV1 = ngayvao.First().Value;
                            else
                            {
                                var nvao2 = bnKB_ngayVao.Where(p => p.MaKP == _makp && p.MaBNhan == b.MaBNhan1).Select(p => p.NgayKham).ToList();
                                if (nvao2.Count > 0 && nvao2.First() != null)
                                    b.VV1 = nvao2.First().Value;
                            }
                        }
                        if (string.IsNullOrEmpty(b.SoBA))
                        {
                            var ngayvao = _lVaoVien.Where(p => p.MaBNhan == b.MaBNhan1).Select(p => p.SoBA).ToList();
                            if (ngayvao.Count > 0 && ngayvao.First() != null)
                                b.SoBA = ngayvao.First();
                        }


                        var d = tt.Where(p => p.MaBNhan == b.MaBNhan1).ToList();
                        if (d.Count > 0)
                        {
                            b.NgheNghiep1 = d.First().TenNN;
                            b.CongVienChuc = d.First().CongVienChuc == 1 ? "X" : "";
                            b.NThan = d.First().NThan;
                            b.ThanhThi = "";
                            b.NongThon = "X";
                            if (d.First().MaHuyen != "")
                            {
                                var thanhthi = qhuyen.Where(p => p.MaHuyen == d.First().MaHuyen).Select(p => p.ThanhThi == true).FirstOrDefault();
                                if (thanhthi != null)
                                {
                                    b.ThanhThi = "X";
                                    b.NongThon = "";
                                }
                            }
                        }
                        var f = noigt.Where(p => p.MaBV == b.MaBV1).Select(p => p.TenBV).ToList();
                        if (f.Count > 0)

                            b.NoiGT1 = f.First();
                        foreach (var a in qkb.Where(p => p.MaBNhan == b.MaBNhan1))
                        {
                            if (a.PhanLoai == "Phòng khám") { b.CDKKB1 = a.MaICD; b.chanDoan = a.ChanDoan; }
                            if (a.PhanLoai == "Lâm sàng") { b.CDKDT1 = DungChung.Bien.MaBV == "20001" ? DungChung.Ham.FreshString(a.MaICD) : a.ChanDoan; }
                        }

                        b.KhoaVV = _lKPhong.Where(p => p.makp == b.KPVV).Select(p => p.tenkp).FirstOrDefault();
                    }
                }

                #endregion

                if (tkvaokhoa.Checked || tkchuyendi.Checked || tkchuyenden.Checked || tkchuyenvien.Checked || chcravien.Checked || ckccvpkham.Checked)
                {
                    _lrvcv = (from a in _lrvcv
                              where ((tkvaokhoa.Checked ? (a.VV1 >= tungay && a.VV1 <= denngay) : false) || (tkchuyenden.Checked ? (a.RV3 >= tungay && a.RV3 <= denngay) : false)
                              || (tkchuyendi.Checked ? (a.NgayChuyenKhoa >= tungay && a.NgayChuyenKhoa <= denngay) : false)
                              || ((tkchuyenvien.Checked || ckccvpkham.Checked) ? (a.CV1 >= tungay && a.CV1 <= denngay && a.PAn1 == 2) : false)
                              || (chcravien.Checked ? ((a.RV1 >= tungay && a.RV1 <= denngay) || (a.TV1 >= tungay && a.TV1 <= denngay)) : false))
                              select a).ToList();

                    //_lrvcv = (from a in  _lrvcv  
                    //          where(( tkvaokhoa.Checked ? (a.VV1 != null && a.KPVV == _makp) : true)
                    //          || ( tkchuyenden.Checked ? (a.RV3 != null) : true)
                    //          || ( tkchuyendi.Checked ? (a.NgayChuyenKhoa != null) : true)
                    //          || ( tkchuyenvien.Checked ? (a.CV1 != null) : true)
                    //          || ( chcravien.Checked ? ((a.RV1 != null) || (a.TV1 != null)) : true))
                    //          select a).ToList();
                }
                if (chcravien.Checked)
                {
                    foreach (var b in _lrvcv)
                    {
                        var check = _lRaVien.FirstOrDefault(o => o.MaBNhan == b.MaBNhan1);
                        if (check != null)
                        {
                            b.CDKDT1 = DungChung.Bien.MaBV == "20001" ? DungChung.Ham.FreshString(check.MaICD) : check.ChanDoan;
                        }
                        //foreach (var a in _lRaVien)
                        //{
                        //    if (a.MaBNhan == b.MaBNhan1)
                        //    {
                        //        b.CDKDT1 = DungChung.Bien.MaBV == "20001" ? DungChung.Ham.FreshString(a.MaICD) : a.ChanDoan;
                        //    }
                        //}
                        b.ThanhThi = "";
                        b.NongThon = "X";
                        b.KhoaVV = _lKPhong.Where(p => p.makp == b.KPVV).Select(p => p.tenkp).FirstOrDefault();
                        if (b.MaHuyen != "")
                        {
                            var thanhthi = qhuyen.Where(p => p.MaHuyen == b.MaHuyen).Select(p => p.ThanhThi == true).FirstOrDefault();
                            if (thanhthi != null)
                            {
                                b.ThanhThi = "X";
                                b.NongThon = "";
                            }
                        }
                    }
                    _lrvcv = _lrvcv.Where(p => (Convert.ToDateTime(p.RV1) >= tungay && Convert.ToDateTime(p.RV1) <= denngay)
                       || (Convert.ToDateTime(p.CV1) >= tungay && Convert.ToDateTime(p.CV1) <= denngay)
                       || (Convert.ToDateTime(p.TV1) >= tungay && Convert.ToDateTime(p.TV1) <= denngay)
                       ).ToList();
                    _lrvcv = _lrvcv.Where(p => p.RV1 != null || p.TV1 != null || p.CV1 != null).ToList();
                }
                #region so bn chuyển viện tại PK
                if (ckccvpkham.Checked || tkchuyenvien.Checked)
                {
                    var _lBenhNhanc = data.BenhNhans.Where(p => p.DTNT == false && p.NoiTru == 0).ToList();
                    if (tkchuyenvien.Checked)
                        _lBenhNhanc = data.BenhNhans.Where(p => radBN.SelectedIndex == 0 ? (p.DTNT == true && p.NoiTru == 0) : (radBN.SelectedIndex == 1 ? p.NoiTru == 1 : true)).ToList();
                    var _lcvpk = data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay && (p.MaKP == _makp || tenKP == "")).Where(p => p.Status == 1).ToList();
                    var _lkbpk = data.BNKBs.Where(p => p.PhuongAn == 2).ToList();
                    var _lbncvpk = (from bn in _lBenhNhanc
                                    join rv in _lcvpk on bn.MaBNhan equals rv.MaBNhan
                                    join kb in _lkbpk on bn.MaBNhan equals kb.MaBNhan
                                    select new
                                    {
                                        bn.MaBNhan,
                                        bn.TenBNhan,
                                        rv.NgayRa,
                                        Nam1 = bn.GTinh == 1 ? bn.Tuoi.ToString() : "",
                                        Nu1 = bn.GTinh == 0 ? bn.Tuoi.ToString() : "",
                                        BHYT1 = bn.DTuong == "BHYT" ? "X" : "",
                                        TE121 = bn.Ma_lk,
                                        TE3Tuoi = (bn.Tuoi <= 3 && string.IsNullOrEmpty(bn.Ma_lk)) ? "X" : "",
                                        TE151 = (bn.Tuoi <= 15 && string.IsNullOrEmpty(bn.Ma_lk)) ? "X" : "",
                                        bn.GTinh,
                                        bn.Tuoi,
                                        bn.DTuong,
                                        bn.NNhap,
                                        CapCuu = bn.CapCuu == 1 ? "X" : "",
                                        bn.DChi,
                                        bn.MaBV,
                                        kb.MaICD,
                                        bn.CDNoiGT,
                                        rv.SoLT,
                                        rv.KetQua,
                                    }).ToList();
                    foreach (var item in _lbncvpk)
                    {
                        RVCV moi = new RVCV();
                        moi.MaBNhan1 = item.MaBNhan;
                        moi.TenBNhan1 = item.TenBNhan;
                        moi.TE121 = item.TE121;
                        moi.TE151 = item.TE151;
                        moi.TE3Tuoi = item.TE3Tuoi;
                        moi.Nam1 = item.Nam1;
                        moi.Nu1 = item.Nu1;
                        moi.BHYT1 = item.BHYT1;
                        moi.CapCuu = item.CapCuu;
                        moi.DiaChi1 = item.DChi;
                        moi.CV1 = item.NgayRa;
                        moi.VV1 = item.NNhap;
                        moi.CDKKB1 = item.MaICD;
                        _lrvcv.Add(moi);
                    }
                }
                #endregion
                #region so bn ra viện ngoại trú
                if (chcravien.Checked && DungChung.Bien.MaBV == "20001" && _makp == 0)
                {
                    var _lBenhNhanc = data.BenhNhans.Where(p => p.DTNT == false && p.NoiTru == 0).ToList();
                    var _lcvpk = data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay && (p.MaKP == _makp || tenKP == "")).Where(p => p.Status == 2).ToList();
                    var _lkbpk = data.BNKBs.Where(p => p.PhuongAn == 0).ToList();
                    var _lbncvpk = (from bn in _lBenhNhanc
                                    join rv in _lcvpk on bn.MaBNhan equals rv.MaBNhan
                                    join kb in _lkbpk on bn.MaBNhan equals kb.MaBNhan
                                    select new
                                    {
                                        bn.MaBNhan,
                                        bn.TenBNhan,
                                        rv.NgayRa,
                                        Nam1 = bn.GTinh == 1 ? bn.Tuoi.ToString() : "",
                                        Nu1 = bn.GTinh == 0 ? bn.Tuoi.ToString() : "",
                                        BHYT1 = bn.DTuong == "BHYT" ? "X" : "",
                                        TE121 = bn.Ma_lk,
                                        TE3Tuoi = (bn.Tuoi <= 3 && string.IsNullOrEmpty(bn.Ma_lk)) ? "X" : "",
                                        TE151 = (bn.Tuoi <= 15 && string.IsNullOrEmpty(bn.Ma_lk)) ? "X" : "",
                                        bn.GTinh,
                                        bn.Tuoi,
                                        bn.DTuong,
                                        bn.NNhap,
                                        CapCuu = bn.CapCuu == 1 ? "X" : "",
                                        bn.DChi,
                                        bn.MaBV,
                                        kb.MaICD,
                                        bn.CDNoiGT,
                                        rv.SoLT,
                                        rv.KetQua,
                                    }).ToList();
                    foreach (var item in _lbncvpk)
                    {
                        bool _kt = false;
                        var check = _lrvcv.FirstOrDefault(o => o.MaBNhan1 == item.MaBNhan);
                        if (check != null)
                        {
                            _kt = true;
                        }
                        //foreach (var item1 in _lrvcv)
                        //{

                        //    if (item.MaBNhan == item1.MaBNhan1)
                        //    {
                        //        _kt = true;
                        //        break;
                        //    }
                        //}
                        if (_kt == false)
                        {
                            RVCV moi = new RVCV();
                            moi.MaBNhan1 = item.MaBNhan;
                            moi.TenBNhan1 = item.TenBNhan;
                            moi.TE121 = item.TE121;
                            moi.TE151 = item.TE151;
                            moi.TE3Tuoi = item.TE3Tuoi;
                            moi.Nam1 = item.Nam1;
                            moi.Nu1 = item.Nu1;
                            moi.BHYT1 = item.BHYT1;
                            moi.CapCuu = item.CapCuu;
                            moi.DiaChi1 = item.DChi;
                            moi.RV1 = item.NgayRa;
                            moi.VV1 = item.NNhap;
                            moi.CDKKB1 = item.MaICD;
                            _lrvcv.Add(moi);
                        }
                    }
                }
                #endregion

                if (chkBNTT.Checked)
                {
                    if (DungChung.Bien.MaBV == "12122" || DungChung.Bien.MaBV == "20001")
                        _lrvcv = (from l in _lrvcv
                                  join rv in data.VienPhis.Where(p => (p.NgayTT >= tungay && p.NgayTT <= denngay)) on l.MaBNhan1 equals rv.MaBNhan
                                  select l).ToList();
                    else
                        _lrvcv = (from l in _lrvcv
                                  join rv in data.VienPhis on l.MaBNhan1 equals rv.MaBNhan
                                  select l).ToList();
                }
                if (checkBNDK.Checked == true)
                    _lrvcv = _lrvcv;
                else if (!tkvaokhoa.Checked && !tkchuyendi.Checked && !tkchuyenden.Checked && !tkchuyenvien.Checked && !chcravien.Checked && !ckccvpkham.Checked)
                    _lrvcv = _lrvcv.Where(p => DungChung.Bien.MaBV == "12122" ? ((p.VV1 >= _ngaytuBN && p.VV1 <= denngay) || (p.RV1 >= _ngaytuBN && p.RV1 <= denngay) || (p.CV1 >= _ngaytuBN && p.CV1 <= denngay)) : ((p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay))).ToList();
                else
                    _lrvcv = (from a in _lrvcv
                              where (((tkvaokhoa.Checked) ? (a.VV1 != null && a.KPVV == _makp) : false)
                              || (tkchuyenden.Checked ? (a.RV3 != null) : false)
                              || (tkchuyendi.Checked ? (a.NgayChuyenKhoa != null) : false)
                              || ((tkchuyenvien.Checked || ckccvpkham.Checked) ? (a.CV1 != null) : false)
                              || (chcravien.Checked ? ((a.RV1 != null) || (a.TV1 != null)) : false)
                              )
                              select a).ToList();
                _lrvcv = _lrvcv.OrderBy(p => p.VV1).ThenBy(p => p.TenBNhan1).ThenBy(p => p.CV1).ThenBy(p => p.RV1).ToList();
                if (cbo_sapXep.SelectedIndex == 1)
                {
                    _lrvcv = _lrvcv.OrderBy(p => p.CV1).ThenBy(p => p.RV1).ThenBy(p => p.VV1).ThenBy(p => p.TenBNhan1).ToList();
                }
                else if (cbo_sapXep.SelectedIndex == 2)
                {
                    _lrvcv = _lrvcv.OrderBy(p => p.SoLT).ThenBy(p => p.RV1).ThenBy(p => p.VV1).ThenBy(p => p.TenBNhan1).ToList();
                }

                if (checkVV.Checked == true)
                {
                    _lrvcv = _lrvcv.Where(p => p.VV1 >= tungay && p.VV1 <= denngay).ToList();
                }
                if (checkBNDK.Checked == true)
                {
                    _lrvcv = _lrvcv.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).ToList();
                }
                #region 12121
                if (RGChonmau.SelectedIndex == 1)
                {

                    #region
                    int[] _arrWidth = new int[] { };
                    // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                    int num = 1;
                    string[] _arr = new string[] { };
                    string[] _tieude = new string[] { };

                    if (checkVV.Checked && checkkhoachuyen.Checked)
                    {
                        BaoCao.Rep_SoVaoRaChuyenVien_12121_RV rep = new BaoCao.Rep_SoVaoRaChuyenVien_12121_RV();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        rep.Khoa.Value = tenKP;
                        rep.DataSource = _lrvcv.ToList(); //.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList();
                        rep.BindingData();
                        _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };

                        DungChung.Bien.MangHaiChieu = new Object[_lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList().Count + 1, 34];
                        _tieude = new string[] { "STT", "Số VV", "Họ và tên", "Tuổi Nam", "Tuổi Nữ", "Cán bộ công nhân viên", "Thành thị", "Nông thôn", "Dưới 12 tháng", "Từ 12 tháng đến 36 tháng", "từ 3 đến 15 tuổi", "Chức vụ - Nghề nghiệp", "Chỗ ở hiện tại", "Họ tên, chỗ ở người thân khi cần liên lạc", "BN cấp cứu", "Vào khoa nào", "Khoa ra viện", "Vào viện", "Chuyển khoa", "Chuyển viện", "Ra viện hay chết", "Tổng số ngày nằm viện", "Nơi giới thiệu", "Khoa khám bệnh", "Lúc vào", "Lúc ra", "Giải phẫu bệnh lý", "Khỏi", "Đỡ", "Không chuyển", "Nặng thêm", "Chết", "Đi điều dưỡng", "GHI CHÚ" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in _lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.SoVV;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan1;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.Nam1;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.Nu1;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.CongVienChuc;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.ThanhThi;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.NongThon;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.TE121;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.TE3Tuoi;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.TE151;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.NgheNghiep1;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.DiaChi1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.NThan;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.CapCuu;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.KhoaVV;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.TenKhoaRV;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.NgayChuyenKhoa;
                            DungChung.Bien.MangHaiChieu[num, 19] = r.CV1;
                            DungChung.Bien.MangHaiChieu[num, 20] = r.RV1;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.SoNgaydt1;
                            DungChung.Bien.MangHaiChieu[num, 22] = r.CDTD1;
                            DungChung.Bien.MangHaiChieu[num, 23] = r.chanDoan;
                            DungChung.Bien.MangHaiChieu[num, 24] = r.CDVV;
                            DungChung.Bien.MangHaiChieu[num, 25] = r.CDKDT1;
                            DungChung.Bien.MangHaiChieu[num, 26] = "";
                            DungChung.Bien.MangHaiChieu[num, 27] = r.Khoi1;
                            DungChung.Bien.MangHaiChieu[num, 28] = r.DoGiam1;
                            DungChung.Bien.MangHaiChieu[num, 29] = r.KoTD1;
                            DungChung.Bien.MangHaiChieu[num, 30] = r.NangHon1;
                            DungChung.Bien.MangHaiChieu[num, 31] = r.KQTuVong;
                            DungChung.Bien.MangHaiChieu[num, 32] = "";
                            DungChung.Bien.MangHaiChieu[num, 33] = "";
                            num++;
                        }
                        rep.CreateDocument();
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ vào ra chuyển viện", "C:\\SoVaoRaChuenVien.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        BaoCao.Rep_SoVaoRaChuyenVien_12121 rep = new BaoCao.Rep_SoVaoRaChuyenVien_12121();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        rep.Khoa.Value = tenKP;
                        rep.DataSource = _lrvcv.ToList(); //.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList();
                        rep.BindingData();

                        _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };

                        DungChung.Bien.MangHaiChieu = new Object[_lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList().Count + 1, 33];
                        _tieude = new string[] { "STT", "Số VV", "Họ và tên", "Tuổi Nam", "Tuổi Nữ", "Cán bộ công nhân viên", "Thành thị", "Nông thôn", "Dưới 12 tháng", "Từ 12 tháng đến 36 tháng", "từ 3 đến 15 tuổi", "Chức vụ - Nghề nghiệp", "Chỗ ở hiện tại", "Họ tên, chỗ ở người thân khi cần liên lạc", "BN cấp cứu", "Vào khoa nào", "Vào viện", "Chuyển khoa", "Chuyển viện", "Ra viện hay chết", "Tổng số ngày nằm viện", "Nơi giới thiệu", "Khoa khám bệnh", "Lúc vào", "Lúc ra", "Giải phẫu bệnh lý", "Khỏi", "Đỡ", "Không chuyển", "Nặng thêm", "Chết", "Đi điều dưỡng", "GHI CHÚ" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in _lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.SoVV;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan1;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.Nam1;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.Nu1;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.CongVienChuc;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.ThanhThi;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.NongThon;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.TE121;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.TE3Tuoi;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.TE151;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.NgheNghiep1;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.DiaChi1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.NThan;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.CapCuu;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.KhoaVV;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.NgayChuyenKhoa;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.CV1;
                            DungChung.Bien.MangHaiChieu[num, 19] = r.RV1;
                            DungChung.Bien.MangHaiChieu[num, 20] = r.SoNgaydt1;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.CDTD1;
                            DungChung.Bien.MangHaiChieu[num, 22] = r.chanDoan;
                            DungChung.Bien.MangHaiChieu[num, 23] = r.CDVV;
                            DungChung.Bien.MangHaiChieu[num, 24] = r.CDKDT1;
                            DungChung.Bien.MangHaiChieu[num, 25] = "";
                            DungChung.Bien.MangHaiChieu[num, 26] = r.Khoi1;
                            DungChung.Bien.MangHaiChieu[num, 27] = r.DoGiam1;
                            DungChung.Bien.MangHaiChieu[num, 28] = r.KoTD1;
                            DungChung.Bien.MangHaiChieu[num, 29] = r.NangHon1;
                            DungChung.Bien.MangHaiChieu[num, 30] = r.KQTuVong;
                            DungChung.Bien.MangHaiChieu[num, 31] = "";
                            DungChung.Bien.MangHaiChieu[num, 32] = "";
                            num++;
                        }

                        rep.CreateDocument();
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ vào ra chuyển viện", "C:\\SoVaoRaChuenVien.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    #endregion
                }
                #endregion
                #region 12122_A3
                if (RGChonmau.SelectedIndex == 2)
                {
                    if (checkkhoachuyen.Checked == true)
                    {
                        BaoCao.Rep_SoVaoRaChuyenVien_12122_A3_ThemCot rep = new BaoCao.Rep_SoVaoRaChuyenVien_12122_A3_ThemCot();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        rep.Khoa.Value = tenKP;
                        rep.DataSource = _lrvcv.ToList(); //.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        #region
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[_lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList().Count + 1, 32];
                        string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Công viên chức", "Có BHYT", "Thành thị", "Nông thôn", "Trẻ em < 12 tháng tuổi", "Trẻ em 1 - 15 tuổi", "Số bệnh án", "Sổ lưu trữ", "Nghề nghiệp", "Địa chỉ", "Nơi giới thiệu", "Vào viện", "Chuyển khoa đến", "Chuyển khoa đi", "Chuyển viện", "Ra viện", "Tử vong", "Khoa chuyển đến", "Tử vong trong 24h", "Tuyến dưới", "Khoa khám bệnh", "Khoa điều trị", "Khoa GPB (nếu có)", "Khỏi", "Đỡ giảm", "Nặng hơn", "Không thay đổi", "Tổng số ngày điều trị" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in _lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan1;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.Nam1;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.Nu1;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.CongVienChuc;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.BHYT1;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.ThanhThi;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.NongThon;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.TE121;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.TE151;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.SoBA;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.SoLT;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.NgheNghiep1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.DiaChi1;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.NoiGT1;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.RV3;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.NgayChuyenKhoa;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.CV1;
                            DungChung.Bien.MangHaiChieu[num, 19] = r.RV1;
                            DungChung.Bien.MangHaiChieu[num, 20] = r.TV1;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.KPChuyen;
                            DungChung.Bien.MangHaiChieu[num, 22] = "";
                            DungChung.Bien.MangHaiChieu[num, 23] = r.CDTD1;
                            DungChung.Bien.MangHaiChieu[num, 24] = r.CDKKB1;
                            DungChung.Bien.MangHaiChieu[num, 25] = r.CDKDT1;
                            DungChung.Bien.MangHaiChieu[num, 26] = "";
                            DungChung.Bien.MangHaiChieu[num, 27] = r.Khoi1;
                            DungChung.Bien.MangHaiChieu[num, 28] = r.DoGiam1;
                            DungChung.Bien.MangHaiChieu[num, 29] = r.NangHon1;
                            DungChung.Bien.MangHaiChieu[num, 30] = r.KoTD1;
                            DungChung.Bien.MangHaiChieu[num, 31] = r.SoNgaydt1;
                            num++;
                        }
                        #endregion
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo sử dụng thuốc bệnh viện", "C:\\BCSDThuocBV.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {

                        BaoCao.Rep_SoVaoRaChuyenVien_12122 rep = new BaoCao.Rep_SoVaoRaChuyenVien_12122();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        rep.Khoa.Value = tenKP;
                        rep.DataSource = _lrvcv.ToList(); //.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        #region
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[_lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList().Count + 1, 31];
                        string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Công viên chức", "Có BHYT", "Thành thị", "Nông thôn", "Trẻ em < 12 tháng tuổi", "Trẻ em 1 - 15 tuổi", "Số bệnh án", "Sổ lưu trữ", "Nghề nghiệp", "Địa chỉ", "Nơi giới thiệu", "Vào viện", "Chuyển khoa đến", "Chuyển khoa đi", "Chuyển viện", "Ra viện", "Tử vong", "Tử vong trong 24h", "Tuyến dưới", "Khoa khám bệnh", "Khoa điều trị", "Khoa GPB (nếu có)", "Khỏi", "Đỡ giảm", "Nặng hơn", "Không thay đổi", "Tổng số ngày điều trị" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in _lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan1;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.Nam1;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.Nu1;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.CongVienChuc;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.BHYT1;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.ThanhThi;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.NongThon;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.TE121;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.TE151;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.SoBA;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.SoLT;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.NgheNghiep1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.DiaChi1;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.NoiGT1;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.RV3;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.NgayChuyenKhoa;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.CV1;
                            DungChung.Bien.MangHaiChieu[num, 19] = r.RV1;
                            DungChung.Bien.MangHaiChieu[num, 20] = r.TV1;
                            DungChung.Bien.MangHaiChieu[num, 21] = "";
                            DungChung.Bien.MangHaiChieu[num, 22] = r.CDTD1;
                            DungChung.Bien.MangHaiChieu[num, 23] = r.CDKKB1;
                            DungChung.Bien.MangHaiChieu[num, 24] = r.CDKDT1;
                            DungChung.Bien.MangHaiChieu[num, 25] = "";
                            DungChung.Bien.MangHaiChieu[num, 26] = r.Khoi1;
                            DungChung.Bien.MangHaiChieu[num, 27] = r.DoGiam1;
                            DungChung.Bien.MangHaiChieu[num, 28] = r.NangHon1;
                            DungChung.Bien.MangHaiChieu[num, 29] = r.KoTD1;
                            DungChung.Bien.MangHaiChieu[num, 30] = r.SoNgaydt1;
                            num++;
                        }
                        #endregion
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo sử dụng thuốc bệnh viện", "C:\\BCSDThuocBV.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                }
                #endregion
                #region 12122_A4
                if (RGChonmau.SelectedIndex == 3)
                {
                    if (checkkhoachuyen.Checked == true)
                    {
                        BaoCao.Rep_SoVaoRaChuyenVien_12122_A4_ThemCo rep = new BaoCao.Rep_SoVaoRaChuyenVien_12122_A4_ThemCo();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        rep.Khoa.Value = tenKP;
                        rep.DataSource = _lrvcv.ToList(); //.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        #region
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { 3, 18, 5, 5, 8, };
                        // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[_lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList().Count + 1, 32];
                        string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Có BHYT", "Trẻ em < 12 tháng tuổi", "Trẻ em 1 - 15 tuổi", "Số bệnh án", "Sổ lưu trữ", "Nghề nghiệp", "Địa chỉ", "Nơi giới thiệu", "Vào viện", "Chuyển khoa đến", "Chuyển khoa đi", "Chuyển viện", "Ra viện", "Tử vong", "Khoa chuyển đến", "Tuyến dưới", "Khoa khám bệnh", "Khoa điều trị", "Khỏi", "Đỡ giảm", "Nặng hơn", "Không thay đổi", "Tổng số ngày điều trị" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in _lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan1;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.Nam1;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.Nu1;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.BHYT1;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.TE121;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.TE151;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.SoBA;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.SoLT;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.NgheNghiep1;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.DiaChi1;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.NoiGT1;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.RV3;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.NgayChuyenKhoa;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.CV1;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.RV1;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.TV1;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.KPChuyen;
                            DungChung.Bien.MangHaiChieu[num, 19] = r.CDTD1;
                            DungChung.Bien.MangHaiChieu[num, 20] = r.CDKKB1;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.CDKDT1;
                            DungChung.Bien.MangHaiChieu[num, 22] = r.Khoi1;
                            DungChung.Bien.MangHaiChieu[num, 23] = r.DoGiam1;
                            DungChung.Bien.MangHaiChieu[num, 24] = r.NangHon1;
                            DungChung.Bien.MangHaiChieu[num, 25] = r.KoTD1;
                            DungChung.Bien.MangHaiChieu[num, 26] = r.SoNgaydt1;
                            num++;
                        }
                        #endregion
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo sử dụng thuốc bệnh viện", "C:\\BCSDThuocBV.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        BaoCao.Rep_SoVaoRaChuyenVien_12122_A4 rep = new BaoCao.Rep_SoVaoRaChuyenVien_12122_A4();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        rep.Khoa.Value = tenKP;
                        rep.DataSource = _lrvcv.ToList(); //.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        #region
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { 3, 18, 5, 5, 8, };
                        // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[_lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList().Count + 1, 31];
                        string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Có BHYT", "Trẻ em < 12 tháng tuổi", "Trẻ em 1 - 15 tuổi", "Số bệnh án", "Sổ lưu trữ", "Nghề nghiệp", "Địa chỉ", "Nơi giới thiệu", "Vào viện", "Chuyển khoa đến", "Chuyển khoa đi", "Chuyển viện", "Ra viện", "Tử vong", "Tuyến dưới", "Khoa khám bệnh", "Khoa điều trị", "Khỏi", "Đỡ giảm", "Nặng hơn", "Không thay đổi", "Tổng số ngày điều trị" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in _lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan1;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.Nam1;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.Nu1;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.BHYT1;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.TE121;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.TE151;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.SoBA;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.SoLT;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.NgheNghiep1;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.DiaChi1;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.NoiGT1;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.RV3;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.NgayChuyenKhoa;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.CV1;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.RV1;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.TV1;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.CDTD1;
                            DungChung.Bien.MangHaiChieu[num, 19] = r.CDKKB1;
                            DungChung.Bien.MangHaiChieu[num, 20] = r.CDKDT1;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.Khoi1;
                            DungChung.Bien.MangHaiChieu[num, 22] = r.DoGiam1;
                            DungChung.Bien.MangHaiChieu[num, 26] = r.NangHon1;
                            DungChung.Bien.MangHaiChieu[num, 24] = r.KoTD1;
                            DungChung.Bien.MangHaiChieu[num, 25] = r.SoNgaydt1;
                            num++;
                        }
                        #endregion
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo sử dụng thuốc bệnh viện", "C:\\BCSDThuocBV.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                }
                #endregion
                #region chung
                if (RGChonmau.SelectedIndex == 0)
                {
                    if (checkkhoachuyen.Checked == false)
                    {
                        BaoCao.Rep_SoVaoRaChuyenVien rep = new BaoCao.Rep_SoVaoRaChuyenVien();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        rep.Khoa.Value = tenKP;
                        rep.DataSource = _lrvcv.ToList(); //.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        #region
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[_lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList().Count + 1, 29];
                        string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Công viên chức", "Có BHYT", "Thành thị", "Nông thôn", "Trẻ em < 12 tháng tuổi", "Trẻ em 1 - 15 tuổi", "Số bệnh án", "Sổ lưu trữ", "Nghề nghiệp", "Địa chỉ", "Nơi giới thiệu", "Vào viện", "Chuyển viện", "Ra viện", "Tử vong", "Tử vong trong 24h", "Tuyến dưới", "Khoa khám bệnh", "Khoa điều trị", "Khoa GPB (nếu có)", "Khỏi", "Đỡ giảm", "Nặng hơn", "Không thay đổi", "TỔng số ngày điều trị" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in _lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan1;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.Nam1;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.Nu1;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.CongVienChuc;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.BHYT1;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.ThanhThi;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.NongThon;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.TE121;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.TE151;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.SoBA;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.SoLT;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.NgheNghiep1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.DiaChi1;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.NoiGT1;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.CV1;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.RV1;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.TV1;
                            DungChung.Bien.MangHaiChieu[num, 19] = "";
                            DungChung.Bien.MangHaiChieu[num, 20] = r.CDTD1;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.CDKKB1;
                            DungChung.Bien.MangHaiChieu[num, 22] = r.CDKDT1;
                            DungChung.Bien.MangHaiChieu[num, 23] = "";
                            DungChung.Bien.MangHaiChieu[num, 24] = r.Khoi1;
                            DungChung.Bien.MangHaiChieu[num, 25] = r.DoGiam1;
                            DungChung.Bien.MangHaiChieu[num, 26] = r.NangHon1;
                            DungChung.Bien.MangHaiChieu[num, 27] = r.KoTD1;
                            DungChung.Bien.MangHaiChieu[num, 28] = r.SoNgaydt1;
                            num++;
                        }
                        #endregion
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ vào ra chuyển viện", "C:\\SoVaoRaChuenVien.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        BaoCao.Rep_SoVaoRaChuyenVien_ThemCot rep = new BaoCao.Rep_SoVaoRaChuyenVien_ThemCot();
                        rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                        rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                        rep.Khoa.Value = tenKP;
                        rep.DataSource = _lrvcv.ToList(); //.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        #region
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        // int[] _arrWidth = new int[] { }; có thể gọi khi truyền kiểu fomatAutowith = true
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[_lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList().Count + 1, 29];
                        string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Công viên chức", "Có BHYT", "Thành thị", "Nông thôn", "Trẻ em < 12 tháng tuổi", "Trẻ em 1 - 15 tuổi", "Số bệnh án", "Sổ lưu trữ", "Nghề nghiệp", "Địa chỉ", "Nơi giới thiệu", "Vào viện", "Chuyển viện", "Ra viện", "Tử vong", "Tử vong trong 24h", "Tuyến dưới", "Khoa khám bệnh", "Khoa điều trị", "Khoa GPB (nếu có)", "Khỏi", "Đỡ giảm", "Nặng hơn", "Không thay đổi", "TỔng số ngày điều trị" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        //for (int i = 0; i <= 17; i++) {
                        //    DungChung.Bien.MangHaiChieu[0, i] = _arr[0,i];
                        //}
                        foreach (var r in _lrvcv.Where(p => (p.VV1 >= tungay && p.VV1 <= denngay) || (p.RV1 >= tungay && p.RV1 <= denngay) || (p.CV1 >= tungay && p.CV1 <= denngay)).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan1;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.Nam1;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.Nu1;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.CongVienChuc;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.BHYT1;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.ThanhThi;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.NongThon;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.TE121;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.TE151;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.SoBA;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.SoLT;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.NgheNghiep1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.DiaChi1;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.NoiGT1;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.CV1;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.RV1;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.TV1;
                            DungChung.Bien.MangHaiChieu[num, 19] = "";
                            DungChung.Bien.MangHaiChieu[num, 20] = r.CDTD1;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.CDKKB1;
                            DungChung.Bien.MangHaiChieu[num, 22] = r.CDKDT1;
                            DungChung.Bien.MangHaiChieu[num, 23] = "";
                            DungChung.Bien.MangHaiChieu[num, 24] = r.Khoi1;
                            DungChung.Bien.MangHaiChieu[num, 25] = r.DoGiam1;
                            DungChung.Bien.MangHaiChieu[num, 26] = r.NangHon1;
                            DungChung.Bien.MangHaiChieu[num, 27] = r.KoTD1;
                            DungChung.Bien.MangHaiChieu[num, 28] = r.SoNgaydt1;
                            num++;
                        }
                        #endregion
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ vào ra chuyển viện", "C:\\SoVaoRaChuenVien.xls", true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                #endregion
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lupKhoa_EditValueChanged(object sender, EventArgs e)
        {
            if (lupKhoa.Text == "Toàn viện" || lupKhoa.Text == "")
            {
                tkvaokhoa.Enabled = false;
                tkvaokhoa.Checked = false;
                tkchuyendi.Enabled = false;
                tkchuyendi.Checked = false;
                tkchuyenden.Checked = false;
                tkchuyenden.Enabled = false;
            }
            else
            {
                if (DungChung.Bien.MaBV == "12122")
                {
                    tkvaokhoa.Enabled = true;
                    tkchuyendi.Enabled = true;
                    tkchuyenden.Enabled = true;
                }
            }
        }

        private void checkBNDK_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBNDK.Checked == true)
            {
                var kphong = (from kp in data.KPhongs
                              where (kp.PLoai == "Phòng khám")
                              select new { kp.TenKP, kp.MaKP }).ToList();
                _Kphong.Clear();
                if (kphong.Count > 0)
                {
                    KPhong themmoi1 = new KPhong();
                    themmoi1.tenkp = "Toàn viện";
                    themmoi1.makp = 0;
                    _Kphong.Add(themmoi1);
                    foreach (var a in kphong)
                    {
                        KPhong themmoi = new KPhong();
                        themmoi.tenkp = a.TenKP;
                        themmoi.makp = a.MaKP;
                        _Kphong.Add(themmoi);
                    }
                    lupKhoa.Properties.DataSource = _Kphong.ToList();
                }
                cbo_sapXep.Enabled = false;
                chkBNTT.Enabled = false;
                chcravien.Enabled = false;
                tkvaokhoa.Enabled = false;
                ckccvpkham.Enabled = false;
                checkkhoachuyen.Enabled = false;
                tkchuyenvien.Enabled = false;
                tkchuyendi.Enabled = false;
                tkchuyenden.Enabled = false;
                checkVV.Enabled = false;
                RGChonmau.SelectedIndex = 0;
                RGChonmau.Enabled = false;
            }
            else
            {
                var kphong = (from kp in data.KPhongs
                              where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                              select new { kp.TenKP, kp.MaKP }).ToList();
                _Kphong.Clear();
                if (kphong.Count > 0)
                {
                    KPhong themmoi1 = new KPhong();
                    themmoi1.tenkp = "Toàn viện";
                    themmoi1.makp = 0;
                    _Kphong.Add(themmoi1);
                    foreach (var a in kphong)
                    {
                        KPhong themmoi = new KPhong();
                        themmoi.tenkp = a.TenKP;
                        themmoi.makp = a.MaKP;
                        _Kphong.Add(themmoi);
                    }
                    lupKhoa.Properties.DataSource = _Kphong.ToList();
                }
                cbo_sapXep.Enabled = true;
                chkBNTT.Enabled = true;
                chcravien.Enabled = true;
                tkvaokhoa.Enabled = false;
                ckccvpkham.Enabled = true;
                checkkhoachuyen.Enabled = true;
                tkchuyenvien.Enabled = true;
                tkchuyendi.Enabled = false;
                tkchuyenden.Enabled = false;
                RGChonmau.Enabled = true;
            }
        }

    }
}