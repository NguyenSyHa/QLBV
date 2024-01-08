using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.BaoCao;

namespace QLBV.FormThamSo
{
    public partial class frm_BC_ChucNangHoHap : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_ChucNangHoHap()
        {
            InitializeComponent();
        }
        private static QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
            private int idtn;
            private int MaDV;
            private string TenDV;
            public int IDTieuNhom

            { set { idtn = value; } get { return idtn; } }
            public int madv
            {
                set { MaDV = value; }
                get { return MaDV; }
            }
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
        }

        List<KPhong> _Kphong = new List<KPhong>();
        List<DV> _ldv = new List<DV>();


        private void frm_BC_ChucNangHoHap_Load(object sender, EventArgs e)
        {

            dtpTuNgay.DateTime = System.DateTime.Now;
            dtpDenNgay.DateTime = System.DateTime.Now;
            _Kphong.Clear();


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
            GetDataDV();
        }

        void GetDataDV()
        {
            var dichvu = (from dv in data.DichVus
                          join dvct in data.DichVucts on dv.MaDV equals dvct.MaDV
                          join tndv in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap) on dv.IdTieuNhom equals tndv.IdTieuNhom
                          select new
                          {
                              tndv.IdTieuNhom,
                              dv.MaDV,
                              dv.TenDV,
                          }).ToList();
            if (dichvu.Count() > 0)
            {
                DV them1 = new DV();

                them1.madv = 0;
                them1.tendv = "Tất cả";
                _ldv.Add(them1);
                foreach (var a in dichvu)
                {
                    DV themmoi = new DV();
                    themmoi.IDTieuNhom = a.IdTieuNhom;
                    themmoi.madv = a.MaDV;
                    themmoi.tendv = a.TenDV;
                    _ldv.Add(themmoi);
                }

            }

            lupDichVu.Properties.DataSource = _ldv.ToList();
            lupDichVu.Properties.DisplayMember = "tendv";
            lupDichVu.Properties.ValueMember = "IDTieuNhom";
        }
        #region class benh nhan
        private class BN
        {
            private string Tenso;
            public string TenSo
            { get { return Tenso; } set { Tenso = value; } }

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
            private DateTime? NgayTH;
            private DateTime Ngay;
            private DateTime NgayTT;
            private DateTime tungay;
            private DateTime denngay;
            public DateTime TuNgay
            { set { tungay = value; } get { return tungay; } }
            public DateTime DenNgay
            { set { denngay = value; } get { return denngay; } }
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
            public DateTime? ngayth
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
        #endregion





        private void btnInBC_Click(object sender, EventArgs e)
        {
            int DichVu;
            if (lupDichVu.EditValue != null)
            {
                DichVu = Convert.ToInt32(lupDichVu.EditValue);
            }

            int IDTN = Convert.ToInt32(lupDichVu.EditValue);

            List<BN> _BN = new List<BN>();
            List<BN> _BNhan = new List<BN>();
            List<KPhong> KhoaPhong = new List<KPhong>();
            frmIn frm = new frmIn();
            DateTime TuNgay = DungChung.Ham.NgayTu(dtpTuNgay.DateTime);
            DateTime DenNgay = DungChung.Ham.NgayDen(dtpDenNgay.DateTime);
            var IDTieuNhom = _ldv.Select(p => p.IDTieuNhom);

            if (TuNgay > DenNgay)
            {
                XtraMessageBox.Show("Thông báo thời gian không hợp lệ. \n (Từ ngày không thể nhỏ hơn (Đến ngày)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpTuNgay.Focus();
                return;
            }

            var qdv = (from dv in data.DichVus
                       join tn in IDTieuNhom.Where(p => (IDTN == 0 || IDTN == null) ? true : (p == IDTN)) on dv.IdTieuNhom equals tn
                       select new { dv.MaDV, dv.TenDV, dv.Loai }).ToList();
            int tg = 3, ng = 3;
            if (ChkTrongGio.Checked == true)
            { tg = 0; }
            if (ChkNgoaiGio.Checked == true)
            { ng = 1; }

            KhoaPhong = _Kphong.Where(a => a.chon == true).ToList();

            string ThongKe = cboTKBN.Text;


            int _bn1 = -1; int _bn2 = -1;
            {
                if (radBN.SelectedIndex == 0) { _bn1 = 1; _bn2 = 0; }
                if (radBN.SelectedIndex == 1) { _bn1 = 1; _bn2 = -1; }
                if (radBN.SelectedIndex == 2) { _bn2 = 0; _bn1 = -1; }
            }

            #region tat ca benh nhan thuc hien

            if (ThongKe == "Tất cả BN thực hiện (Thống kê theo ngày thực hiện)")
            {
                if (chkBNcoCLS.Checked == true)
                {

                    var DSbenhnhan = (from bn in data.BenhNhans.Where(p => p.NoiTru == _bn1 || p.NoiTru == _bn2)
                                      join cls in data.CLS.Where(o => o.NgayTH >= TuNgay && o.NgayTH <= DenNgay) on bn.MaBNhan equals cls.MaBNhan
                                      join cd in data.ChiDinhs.Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                      join Kp in data.KPhongs on cls.MaKP equals Kp.MaKP
                                      join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                      join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                      join cb in data.CanBoes on cls.MaCB equals cb.MaCB
                                      select new
                                      {
                                          Kp = Kp.MaKP,
                                          TenBenhNhan = bn.TenBNhan,
                                          Tuoi = bn.Tuoi,
                                          GTinh = bn.GTinh,
                                          DiaChi = bn.DChi,
                                          DTuong = bn.DTuong,
                                          ChanDoan = cls.ChanDoan,
                                          MaDv = dv.MaDV,
                                          KetQua = clsct.KetQua,
                                          BSTH = cb.TenCB,
                                          NgayTH = cls.NgayTH,
                                          tenKP = Kp.TenKP,
                                          Nt = bn.NoiTru,

                                      }).ToList();

                    var DSKpThuchien = (from kp in KhoaPhong
                                        select new
                                        {
                                            MaKhoaPhong = kp.makp,
                                        }).ToList();

                    var benhnhan = (from bn in DSbenhnhan
                                    join kpth in DSKpThuchien on bn.Kp equals kpth.MaKhoaPhong
                                    join dv in qdv on bn.MaDv equals dv.MaDV
                                    select new
                                    {
                                        Kp = kpth.MaKhoaPhong,
                                        TenBenhNhan = bn.TenBenhNhan,
                                        Tuoi = bn.Tuoi,
                                        GTinh = bn.GTinh,
                                        DiaChi = bn.DiaChi,
                                        DTuong = bn.DTuong,
                                        ChanDoan = bn.ChanDoan,
                                        TenDV = dv.TenDV,
                                        KetQua = bn.KetQua,
                                        BSTH = bn.BSTH,
                                        NgayTH = bn.NgayTH,
                                        Noigui = bn.tenKP,
                                        Noitru = bn.Nt,
                                    }).ToList();


                    if (benhnhan.Count > 0)
                    {

                        foreach (var a in benhnhan)
                        {
                            BN themmoi = new BN();
                            themmoi.tenbnhan = a.TenBenhNhan;
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

                            if (a.DTuong == "BHYT")
                            {
                                themmoi.bhyt = "X";
                            }
                            else { themmoi.khac = "X"; }
                            themmoi.diachi = a.DiaChi;
                            themmoi.yeucau = a.TenDV;
                            themmoi.noigui = a.Noigui;
                            themmoi.chandoan = a.ChanDoan;
                            themmoi.bsth = a.BSTH;
                            themmoi.ngayth = a.NgayTH;
                            themmoi.ketqua = a.KetQua;

                            _BN.Add(themmoi);
                        }


                    }
                 }

                if (chkBNkhongCLS.Checked == true)
                {
                    var danhSachBenhnhan = (from bn in data.BenhNhans.Where(p => p.NoiTru == _bn1 || p.NoiTru == _bn2)
                                            join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                            join dtct in data.DThuoccts.Where(p => p.IDCD == null || p.IDCD <= 0).Where(o => o.NgayNhap >= TuNgay && o.NgayNhap <= DenNgay) on dt.IDDon equals dtct.IDDon
                                            join Kp in data.KPhongs on bn.MaKP equals Kp.MaKP
                                            join cb in data.CanBoes on bn.MaCB equals cb.MaCB
                                            join bnkb in data.BNKBs on dtct.IDKB equals bnkb.IDKB
                                            select new
                                            {
                                                bn.MaBNhan,
                                                bn.TenBNhan,
                                                bn.DChi,
                                                bn.GTinh,
                                                bn.Tuoi,
                                                bn.DTuong,
                                                bn.NoiTru,
                                                dtct.MaKP,
                                                dtct.MaDV,
                                                dtct.MaCB,
                                                dtct.NgayNhap,
                                                dtct.IDCD,
                                                cb.TenCB,
                                                bnkb.ChanDoan,

                                            }).ToList();

                    var dv = (from bn in danhSachBenhnhan
                              join dichvu in qdv on bn.MaDV equals dichvu.MaDV
                              join kp in KhoaPhong on bn.MaKP equals kp.makp
                              select new
                              {
                                  bn.MaBNhan,
                                  kp.makp,
                                  bn.TenBNhan,
                                  bn.Tuoi,
                                  bn.GTinh,
                                  bn.DChi,
                                  bn.DTuong,
                                  bn.NgayNhap,
                                  bn.NoiTru,
                                  kp.tenkp,
                                  dichvu.TenDV,
                                  bn.TenCB,
                                  bn.ChanDoan,
                              }).ToList();
                    var BNhan = (from bn in dv
                                 join kpth in KhoaPhong on bn.makp equals kpth.makp

                                 select new
                                 {
                                     Kp = kpth.makp,
                                     TenBenhNhan = bn.TenBNhan,
                                     Tuoi = bn.Tuoi,
                                     GTinh = bn.GTinh,
                                     DiaChi = bn.DChi,
                                     DTuong = bn.DTuong,
                                     Noigui = kpth.tenkp,
                                     Noitru = bn.NoiTru,
                                     TenDv = bn.TenDV,
                                     TenCB = bn.TenCB,
                                     NgayTH = bn.NgayNhap,
                                     YeuCau = bn.ChanDoan,
                                 }).ToList();
                    if (BNhan.Count > 0)
                    {
                        foreach (var a in BNhan)
                        {
                            BN themmoi = new BN();
                            themmoi.tenbnhan = a.TenBenhNhan;
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

                            if (a.DTuong == "BHYT")
                            {
                                themmoi.bhyt = "X";
                            }
                            else { themmoi.khac = "X"; }
                            themmoi.diachi = a.DiaChi;
                            themmoi.chandoan = a.YeuCau;
                            themmoi.bsth = a.TenCB;
                            themmoi.noigui = a.Noigui;
                            themmoi.ngayth = a.NgayTH;
                            themmoi.yeucau = a.TenDv;
                            _BN.Add(themmoi);
                        }

                    }
                  
                }

            #endregion
            }

            if (ThongKe == "Chỉ BN đã thanh toán (Thống kê theo ngày thanh toán)")
            {

                if (chkBNcoCLS.Checked == true)
                {
                    var dsbn1 = (from bn in data.BenhNhans.Where(p => p.NoiTru == _bn1 || p.NoiTru == _bn2)
                                 join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                                 join cd in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                 join Kp in data.KPhongs on cls.MaKP equals Kp.MaKP
                                 join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                 join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                 join cb in data.CanBoes on cls.MaCB equals cb.MaCB
                                 join vp in data.VienPhis.Where(p => p.NgayTT >= TuNgay && p.NgayTT <= DenNgay) on cls.MaBNhan equals vp.MaBNhan
                                 select new
                                 {
                                     Kp = Kp.MaKP,
                                     Madv  = cd.MaDV,
                                     TenBenhNhan = bn.TenBNhan,
                                     Tuoi = bn.Tuoi,
                                     GTinh = bn.GTinh,
                                     DiaChi = bn.DChi,
                                     DTuong = bn.DTuong,
                                     ChanDoan = cls.ChanDoan,
                                     TenDV = dv.TenDV,
                                     KetQua = clsct.KetQua,
                                     BSTH = cb.TenCB,
                                     NgayTH = cls.NgayTH,
                                     tenKP = Kp.TenKP,
                                     Nt = bn.NoiTru,
                                 }).ToList();
                    var dskpth1 = (from kp in KhoaPhong
                                   select new
                                   {
                                       MaKhoaPhong = kp.makp,
                                   }).ToList();

                    var bn1 = (from bn in dsbn1
                               join kpth in dskpth1 on bn.Kp equals kpth.MaKhoaPhong
                               join dv in qdv on bn.Madv equals dv.MaDV
                               select new
                               {
                                   Kp = kpth.MaKhoaPhong,
                                   TenBenhNhan = bn.TenBenhNhan,
                                   Tuoi = bn.Tuoi,
                                   GTinh = bn.GTinh,
                                   DiaChi = bn.DiaChi,
                                   DTuong = bn.DTuong,
                                   ChanDoan = bn.ChanDoan,
                                   TenDV = bn.TenDV,
                                   KetQua = bn.KetQua,
                                   BSTH = bn.BSTH,
                                   NgayTH = bn.NgayTH,
                                   Noigui = bn.tenKP,
                                   Noitru = bn.Nt,
                               }).ToList();


                    if (dsbn1.Count > 0)
                    {

                        foreach (var a in bn1)
                        {
                            BN themmoi = new BN();
                            themmoi.tenbnhan = a.TenBenhNhan;
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

                            if (a.DTuong == "BHYT")
                            {
                                themmoi.bhyt = "X";
                            }
                            else { themmoi.khac = "X"; }
                            themmoi.diachi = a.DiaChi;
                            themmoi.yeucau = a.TenDV;
                            themmoi.noigui = a.Noigui;
                            themmoi.chandoan = a.ChanDoan;
                            themmoi.bsth = a.BSTH;
                            themmoi.ngayth = a.NgayTH;
                            themmoi.ketqua = a.KetQua;

                            _BN.Add(themmoi);
                        }
                    }
                   
                }

                if (chkBNkhongCLS.Checked == true)
                {
                    var DSBN = (from bn in data.BenhNhans.Where(p => p.NoiTru == _bn1 || p.NoiTru == _bn2)
                                join vp in data.VienPhis.Where(p => p.NgayTT >= TuNgay && p.NgayTT <= DenNgay) on bn.MaBNhan equals vp.MaBNhan
                                join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                join dtct in data.DThuoccts.Where(p => p.IDCD == null || p.IDCD <= 0) on dt.IDDon equals dtct.IDDon
                                join cbth in data.CanBoes on bn.MaCB equals cbth.MaCB
                                join bnkb in data.BNKBs on dtct.IDKB equals bnkb.IDKB
                                select new
                                {
                                    bn.MaBNhan, 
                                    bn.TenBNhan,
                                    bn.DChi,
                                    bn.GTinh,
                                    bn.Tuoi,
                                    bn.DTuong,
                                    bn.NoiTru,
                                    dtct.MaKP,
                                    dtct.MaDV,
                                    dtct.MaCB,
                                    dtct.NgayNhap,
                                    dtct.IDCD,
                                    vp.NgayTT,
                                    cbth.TenCB,
                                    bnkb.ChanDoan,

                                }).ToList();

                    var dv = (from bn in DSBN
                              join dichvu in qdv on bn.MaDV equals dichvu.MaDV
                              join kp in KhoaPhong on bn.MaKP equals kp.makp
                              select new
                              {
                                  mbn = bn.MaBNhan,
                                  MKP = kp.makp,
                                  TenBenhNhan = bn.TenBNhan,
                                  Tuoi = bn.Tuoi,
                                  GTinh = bn.GTinh,
                                  DiaChi = bn.DChi,
                                  DTuong = bn.DTuong,
                                  NgayTH = bn.NgayNhap,
                                  Noitru = bn.NoiTru,
                                  khoaPhong = kp.tenkp,
                                  TenDv = dichvu.TenDV,
                                  TenCanBo = bn.TenCB,
                                  DichVu = dichvu.TenDV,
                                  ChanDoan = bn.ChanDoan,

                              }).ToList();

                    var qcd = (from bn in dv
                               select new
                               {
                                   
                                   bn.TenBenhNhan,
                                   bn.DTuong,
                                   bn.DiaChi,
                                   bn.Tuoi,
                                   bn.GTinh,
                                   bn.Noitru,
                                   bn.NgayTH,
                                   bn.TenDv,
                                   bn.ChanDoan,
                                   bn.khoaPhong,
                                   bn.TenCanBo,
                                   bn.DichVu,
                               }).ToList();


                    if (qcd.Count > 0)
                    {

                        foreach (var a in qcd)
                        {
                            BN themmoi = new BN();
                            themmoi.tenbnhan = a.TenBenhNhan;
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

                            if (a.DTuong == "BHYT")
                            {
                                themmoi.bhyt = "X";
                            }
                            else { themmoi.khac = "X"; }
                            themmoi.diachi = a.DiaChi;
                            themmoi.yeucau = a.TenDv;
                            themmoi.noigui = a.khoaPhong;
                            themmoi.chandoan = a.ChanDoan;
                            themmoi.bsth = a.TenCanBo;
                            themmoi.ngayth = a.NgayTH;
                            themmoi.yeucau = a.DichVu;
                            //themmoi.ketqua = a.;
                            _BN.Add(themmoi);
                        }

                    }
                   
                }

            }
            #region Hàm in
            if (radMau.SelectedIndex == 0)
            {
                BaoCao.Rep_SoSieuAm_A4 rep = new BaoCao.Rep_SoSieuAm_A4();
             
                if (_BN.Count > 0)
                {
                    rep.DataSource = _BN.ToList();
                    rep.TenSo.Value = "SỔ CHỨC NĂNG HÔ HẤP";
                    rep.TuNgay.Value = TuNgay;
                    rep.DenNgay.Value = DenNgay;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
           
            }
            if (radMau.SelectedIndex == 1)
            {
                BaoCao.Rep_SoSieuAm rep = new BaoCao.Rep_SoSieuAm();
                if (_BN.Count > 0)
                {
                    rep.DataSource = _BN.ToList();
                    rep.TenSo.Value = "SỔ CHỨC NĂNG HÔ HẤP";
                    rep.TuNgay.Value = TuNgay;
                    rep.DenNgay.Value = DenNgay;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              
            }
            #endregion
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

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

        private void chkBNkhongCLS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBNkhongCLS.Checked == true)
            {
                ChkTrongGio.Enabled = false;
                ChkNgoaiGio.Enabled = false;
            }
            else
            {
                ChkTrongGio.Enabled = true;
                ChkNgoaiGio.Enabled = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
        }
    }
}



