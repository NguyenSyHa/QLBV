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
    public partial class Frm_SoSieuAm : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoSieuAm()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "27194" || DungChung.Bien.MaBV == "30372")
            {
                panel2.Visible = true;
            }
            

        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon, 1800);
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
            radTrongDM.SelectedIndex = 2;
            {
                radioGroup1.Properties.Items[6].Enabled = false;
                radioGroup1.Properties.Items[7].Enabled = false;
            }
            if (DungChung.Bien.MaBV == "27001")
            {
                radioGroup1.Properties.Items[6].Enabled = true;
                radioGroup1.Properties.Items[7].Enabled = true;
            }
            if (DungChung.Bien.MaBV != "30002")
            {
                chkPhim.Visible = false;
            }
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

            if (DungChung.Bien.MaBV != "27183")
            {
                lblDTuong.Visible = cboDTuong.Visible = false;
            }
        }

        private class BN
        {
            private string TenBNhan;
            private int MaBNhan;
            private int NoiTru;
            private int GTinh;
            public int SoPhim { get; set; }
            public double CoPhim { get; set; }
            public string SoTheBHYT { get; set; }

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


        //  List<BN> _BN1 = new List<BN>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            try
            {
                DungChung.Ham.CallProcessWaitingForm(Data, "Xin đợi ...", "Đang xử lý dữ liệu.");
            }
            finally
            {
                GC.Collect();
            }
        }


        private void Data()
        {
            List<BN> _BN = new List<BN>();
            List<BN> _BNhan = new List<BN>();
            int tg = 3, ng = 3;
            if (TrongGio.Checked == true)
            { tg = 0; }
            if (NgoaiGio.Checked == true)
            { ng = 1; }
            try
            {
                DateTime tungay = System.DateTime.Now.Date;
                DateTime denngay = System.DateTime.Now.Date;
                List<KPhong> _lKhoaP = new List<KPhong>();
                List<CanBo> _lCB = new List<CanBo>();
                int _madv = 0;
                if (lupDichVu.EditValue != null)
                    _madv = Convert.ToInt32(lupDichVu.EditValue);
                if (KTtaoBc())
                {
                    tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                    int cdha = 0;
                    string _cdha = "";
                    string noisoiKhac = "";
                    if (radioGroup1.SelectedIndex == 0)
                    {
                        _cdha = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm;//"Siêu âm";
                    }
                    if (radioGroup1.SelectedIndex == 1)
                    {
                        _cdha = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang;// "X-Quang";
                    }
                    if (radioGroup1.SelectedIndex == 2)
                    {
                        _cdha = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim;// "Điện tim";
                    }
                    if (radioGroup1.SelectedIndex == 3)
                    {
                        if (ckNoiSoiTMH.Checked)
                            _cdha = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong;// "Nội soi";
                        if (ckNoiSoiKhac.Checked)
                            noisoiKhac = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi;
                    }
                    if (radioGroup1.SelectedIndex == 4)
                    {
                        _cdha = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT;// "X-QuangCT";
                    }
                    if (radioGroup1.SelectedIndex == 5)
                    {
                        _cdha = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler;
                    }
                    if (radioGroup1.SelectedIndex == 6)
                    {
                        _cdha = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong;
                    }
                    if (radioGroup1.SelectedIndex == 7)
                    {
                        cdha = 502;
                    }
                    _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                    _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });
                    _lCB = data.CanBoes.ToList();

                    var qdv = (from dv in data.DichVus
                               join tn in data.TieuNhomDVs.Where(p => DungChung.Bien.MaBV == "27001" && radioGroup1.SelectedIndex == 7 ? p.IdTieuNhom == cdha : (radioGroup1.SelectedIndex == 3 ? (p.TenRG == _cdha || p.TenRG == noisoiKhac) : (p.TenRG == _cdha)))
                               on dv.IdTieuNhom equals tn.IdTieuNhom

                               select new { dv.MaDV, dv.TenDV, dv.Loai, tn.TenRG }).ToList();
                    #region Tất cả BN thực hiện
                    string aa = cboTKBN.EditValue.ToString();
                    if (aa == "Tất cả BN thực hiện (Thống kê theo ngày thực hiện)")
                    {
                        if (chkBNcoCLS.Checked == true)
                        {
                            if (ckXQuangCTDichvu.Checked)
                            {
                                var qso00 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                             join cd in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                             join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                             join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                                             select new { dtct.TrongBH, clsct.KetQua, cd.MaDV, cls.MaBNhan, cd.ChiDinh1, cls.ChanDoan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) }
                                     )
                                     .ToList();
                                var mabnhan = qso00.Select(p => p.MaBNhan).Distinct().ToList();
                                var Bnhan = data.BenhNhans.Where(o => mabnhan.Contains(o.MaBNhan) && DungChung.Bien.MaBV == "27183" ? (cboDTuong.SelectedIndex == 0 ? true : o.DTuong == cboDTuong.Text) : true).ToList();

                                var qso1 = (from cls in qso00
                                            join bn in Bnhan on cls.MaBNhan equals bn.MaBNhan
                                            select new
                                            {
                                                cls.KetQua,
                                                cls.MaDV,
                                                cls.ChanDoan,
                                                cls.ChiDinh1,
                                                cls.Status,
                                                cls.KetLuan,
                                                cls.MaKP,
                                                cls.MaCBth,
                                                cls.NgayTH,
                                                cls.TrongBH,
                                                bn.MaBNhan,
                                                bn.TenBNhan,
                                                bn.DChi,
                                                bn.GTinh,
                                                bn.Tuoi,
                                                bn.DTuong,
                                                bn.NoiTru,
                                                cls.SoPhieu,
                                                cls.SoPhim,
                                                bn.SThe
                                            }).ToList();
                                var qso = (from cls in qso1
                                           join dv in qdv on cls.MaDV equals dv.MaDV
                                           select new { cls.TrongBH, cls.KetQua, cls.MaBNhan, cls.TenBNhan, cls.DChi, cls.GTinh, cls.Tuoi, cls.DTuong, cls.ChanDoan, cls.NoiTru, cls.MaDV, cls.ChiDinh1, cls.Status, dv.TenDV, dv.Loai, cls.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, dv.TenRG, cls.SoPhim, cls.SoPhieu, cls.SThe }).ToList();
                                var qtc = (from kp in _lKhoaP
                                           join q1 in qso on kp.makp equals q1.MaKP
                                           group new { q1 } by new { q1.TrongBH, q1.KetQua, q1.MaBNhan, q1.ChanDoan, q1.TenBNhan, q1.DChi, q1.GTinh, q1.Tuoi, q1.DTuong, q1.NoiTru, q1.MaDV, q1.ChiDinh1, q1.TenDV, q1.Loai, q1.KetLuan, kp.makp, kp.tenkp, q1.MaCBth, q1.NgayTH, q1.SThe, q1.SoPhieu, q1.SoPhim } into kq
                                           select new
                                           {
                                               TrongBH = kq.Key.TrongBH,
                                               MaBNhan = kq.Key.MaBNhan,
                                               TenBNhan = kq.Key.TenBNhan,
                                               GTinh = kq.Key.GTinh,
                                               Tuoi = kq.Key.Tuoi,
                                               NoiTru = kq.Key.NoiTru,
                                               DTuong = kq.Key.DTuong,
                                               DiaChi = kq.Key.DChi,
                                               MaDV = kq.Key.MaDV,
                                               TenDV = kq.Key.TenDV + kq.Key.ChiDinh1,
                                               KetQua = (_cdha == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || _cdha == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT) ? kq.Key.KetLuan + " " + kq.Key.KetQua : kq.Key.KetLuan,
                                               MakP = kq.Key.makp,
                                               TenKP = kq.Key.tenkp,
                                               BSTH = kq.Key.MaCBth,
                                               NgayTH = kq.Key.NgayTH,
                                               Loai = kq.Key.Loai,
                                               ChanDoan = kq.Key.ChanDoan,
                                               kq.Key.SoPhieu,
                                               kq.Key.SoPhim,
                                               kq.Key.SThe
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
                                        if (a.TrongBH == 1)
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
                                            var a1 = (from k in _lCB.Where(p => p.MaCB == a.BSTH) select new { k.TenCB }).ToList();
                                            if (a1.Count > 0)
                                            { themmoi.bsth = a1.First().TenCB; }
                                        }
                                        if (a.NgayTH != null && a.NgayTH.ToString().Length >= 10)
                                        {
                                            themmoi.ngayth = a.NgayTH;
                                        }

                                        themmoi.loai = Convert.ToInt32(a.Loai);
                                        themmoi.chandoan = a.ChanDoan;
                                        themmoi.SoPhim = a.SoPhim ?? 0;
                                        themmoi.CoPhim = a.SoPhieu ?? 0;
                                        themmoi.SoTheBHYT = a.SThe;
                                        _BNhan.Add(themmoi);
                                    }
                                }
                            }
                            else
                            {
                                var qso0 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                            join cd in data.ChiDinhs.Where(p => (DungChung.Bien.MaBV != "27194" ? true : (radTrongDM.SelectedIndex == 2 ? true : (p.TrongBH == radTrongDM.SelectedIndex)))).Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                            select new { clsct.KetQua, cd.MaDV, cls.MaBNhan, cd.ChiDinh1, cls.ChanDoan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) }
                                        )
                                        .ToList();
                                #region 30372
                                if (DungChung.Bien.MaBV == "30372")
                                {
                                    if (radTrongDM.SelectedIndex == 0)
                                    {
                                         qso0 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                                    join cd in data.ChiDinhs.Where(p => p.TrongBH == 0).Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                                    join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                                    select new { clsct.KetQua, cd.MaDV, cls.MaBNhan, cd.ChiDinh1, cls.ChanDoan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) }
                                            )
                                            .ToList();
                                    }
                                    else if (radTrongDM.SelectedIndex == 1)
                                    {
                                         qso0 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                                    join cd in data.ChiDinhs.Where(p => p.TrongBH == 1).Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                                    join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                                    select new { clsct.KetQua, cd.MaDV, cls.MaBNhan, cd.ChiDinh1, cls.ChanDoan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) }
                                            )
                                            .ToList();
                                    }
                                    else if (radTrongDM.SelectedIndex == 2)
                                    {
                                         qso0 = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                                    join cd in data.ChiDinhs.Where(p => p.TrongBH == 1 || p.TrongBH == 0).Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                                    join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                                    select new { clsct.KetQua, cd.MaDV, cls.MaBNhan, cd.ChiDinh1, cls.ChanDoan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) }
                                            )
                                            .ToList();
                                    }
                                }
                                #endregion
                                var mabnhan = qso0.Select(p => p.MaBNhan).Distinct().ToList();
                                var Bnhan = data.BenhNhans.Where(o => mabnhan.Contains(o.MaBNhan) && DungChung.Bien.MaBV == "27183" ? (cboDTuong.SelectedIndex == 0 ? true : o.DTuong == cboDTuong.Text) : true).ToList();

                                var qso1 = (from cls in qso0
                                            join bn in Bnhan on cls.MaBNhan equals bn.MaBNhan
                                            select new
                                            {
                                                cls.KetQua,
                                                cls.MaDV,
                                                cls.ChanDoan,
                                                cls.ChiDinh1,
                                                cls.Status,
                                                cls.KetLuan,
                                                cls.MaKP,
                                                cls.MaCBth,
                                                cls.NgayTH,
                                                bn.MaBNhan,
                                                bn.TenBNhan,
                                                bn.DChi,
                                                bn.GTinh,
                                                bn.Tuoi,
                                                bn.DTuong,
                                                bn.NoiTru,
                                                cls.SoPhieu,
                                                cls.SoPhim,
                                                bn.SThe
                                            }).ToList();
                                var qso = (from cls in qso1
                                           join dv in qdv on cls.MaDV equals dv.MaDV
                                           select new { cls.KetQua, cls.MaBNhan, cls.TenBNhan, cls.DChi, cls.GTinh, cls.Tuoi, cls.DTuong, cls.ChanDoan, cls.NoiTru, cls.MaDV, cls.ChiDinh1, cls.Status, dv.TenDV, dv.Loai, cls.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, dv.TenRG, cls.SoPhim, cls.SoPhieu, cls.SThe }).ToList();
                                var qtc = (from kp in _lKhoaP
                                           join q1 in qso on kp.makp equals q1.MaKP
                                           group new { q1 } by new { q1.KetQua, q1.MaBNhan, q1.ChanDoan, q1.TenBNhan, q1.DChi, q1.GTinh, q1.Tuoi, q1.DTuong, q1.NoiTru, q1.MaDV, q1.ChiDinh1, q1.TenDV, q1.Loai, q1.KetLuan, kp.makp, kp.tenkp, q1.MaCBth, q1.NgayTH, q1.SThe, q1.SoPhieu, q1.SoPhim } into kq
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
                                               TenDV = DungChung.Bien.MaBV == "27001" ? kq.Key.TenDV : kq.Key.TenDV + kq.Key.ChiDinh1,
                                               KetQua = (DungChung.Bien.MaBV == "14018") ? kq.Key.KetLuan : (_cdha == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || _cdha == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT) ? kq.Key.KetLuan + " " + kq.Key.KetQua : kq.Key.KetLuan,
                                               MakP = kq.Key.makp,
                                               TenKP = kq.Key.tenkp,
                                               BSTH = kq.Key.MaCBth,
                                               NgayTH = kq.Key.NgayTH,
                                               Loai = kq.Key.Loai,
                                               ChanDoan = kq.Key.ChanDoan,
                                               kq.Key.SoPhieu,
                                               kq.Key.SoPhim,
                                               kq.Key.SThe
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
                                            var a1 = (from k in _lCB.Where(p => p.MaCB == a.BSTH) select new { k.TenCB }).ToList();
                                            if (a1.Count > 0)
                                            { themmoi.bsth = a1.First().TenCB; }
                                        }
                                        if (a.NgayTH != null && a.NgayTH.ToString().Length >= 10)
                                        {
                                            themmoi.ngayth = a.NgayTH;
                                        }

                                        themmoi.loai = Convert.ToInt32(a.Loai);
                                        themmoi.chandoan = a.ChanDoan;
                                        themmoi.SoPhim = a.SoPhim ?? 0;
                                        themmoi.CoPhim = a.SoPhieu ?? 0;
                                        themmoi.SoTheBHYT = a.SThe;
                                        _BNhan.Add(themmoi);
                                    }
                                }
                            }
                            
                        }
                        if (chkBNkhongCLS.Checked == true)
                        {
                            var qdt0 = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.IDCD == null || p.IDCD <= 0)
                                        join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                        join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                        where DungChung.Bien.MaBV == "27183" ? (cboDTuong.SelectedIndex == 0 ? true : bn.DTuong == cboDTuong.Text) : true
                                        select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, dtct.MaDV, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, dtct.MaKP, dtct.MaCB, dtct.NgayNhap, dtct.IDCD, bn.SThe }).ToList();
                            var qdt1 = (from dt in qdt0
                                        join dv in qdv on dt.MaDV equals dv.MaDV
                                        select new { dt.MaBNhan, dt.TenBNhan, dt.DChi, dt.GTinh, dt.Tuoi, dt.DTuong, dt.NoiTru, dt.MaKP, dt.MaCB, dt.NgayNhap, dt.IDCD, dv.MaDV, dv.TenDV, dv.Loai, dv.TenRG, dt.SThe }).ToList();
                            var qdt = (from kp in _lKhoaP
                                       join qd in qdt1 on kp.makp equals qd.MaKP
                                       group new { qd } by new { qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DChi, qd.GTinh, qd.Tuoi, qd.DTuong, qd.MaDV, qd.TenDV, qd.Loai, kp.makp, kp.tenkp, qd.MaCB, qd.NgayNhap, qd.SThe } into kq
                                       select new { kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.TenBNhan, kq.Key.DChi, kq.Key.GTinh, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.MaDV, kq.Key.TenDV, kq.Key.Loai, kq.Key.makp, kq.Key.tenkp, kq.Key.MaCB, kq.Key.NgayNhap, kq.Key.SThe }).ToList();
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
                                    themmoi.madv = a.MaDV;
                                    themmoi.yeucau = a.TenDV;
                                    themmoi.makp = a.makp;
                                    themmoi.noigui = a.tenkp;
                                    if (a.MaCB != null)
                                    {
                                        var a1 = (from k in _lCB.Where(p => p.MaCB == a.MaCB) select new { k.TenCB }).ToList();
                                        if (a1.Count > 0)
                                        { themmoi.bsth = a1.First().TenCB; }
                                    }
                                    if (a.NgayNhap != null && a.NgayNhap.ToString().Length >= 10)
                                    {

                                        themmoi.ngayth = a.NgayNhap;

                                    }

                                    themmoi.loai = Convert.ToInt32(a.Loai);
                                    themmoi.SoPhim = 0;
                                    themmoi.CoPhim = 0;
                                    themmoi.SoTheBHYT = a.SThe;
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
                            if (ckXQuangCTDichvu.Checked)
                            {
                                var qso0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                            join cls in data.CLS on vp.MaBNhan equals cls.MaBNhan
                                            join cd in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                            join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                                            select new { dtct.TrongBH, clsct.KetQua, cls.ChanDoan, cd.MaDV, cd.ChiDinh1, cls.MaBNhan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, vp.NgayTT, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) } // bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru,
                                     ).ToList();
                                var mabnhan = qso0.Select(p => p.MaBNhan).Distinct().ToList();
                                var Bnhan = data.BenhNhans.Where(o => mabnhan.Contains(o.MaBNhan) && DungChung.Bien.MaBV == "27183" ? (cboDTuong.SelectedIndex == 0 ? true : o.DTuong == cboDTuong.Text) : true).ToList();

                                var qso1 = (from cls in qso0 join bn in Bnhan on cls.MaBNhan equals bn.MaBNhan select new {cls.TrongBH, cls.KetQua, cls.MaDV, cls.ChiDinh1, cls.Status, cls.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.NgayTT, cls.ChanDoan, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, cls.SoPhieu, cls.SoPhim, bn.SThe }).ToList();
                                var qso = (from cls in qso1 join dv in qdv on cls.MaDV equals dv.MaDV select new {cls.TrongBH, cls.KetQua, cls.MaBNhan, cls.TenBNhan, cls.DChi, cls.GTinh, cls.Tuoi, cls.DTuong, cls.NoiTru, cls.ChiDinh1, cls.Status, cls.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.ChanDoan, cls.NgayTT, dv.Loai, dv.TenRG, dv.TenDV, dv.MaDV, cls.SoPhieu, cls.SoPhim, cls.SThe }).ToList();
                                var qtc = (from kp in _lKhoaP
                                           join q1 in qso on kp.makp equals q1.MaKP
                                           group new { q1 } by new {q1.TrongBH, q1.KetQua, q1.MaBNhan, q1.ChanDoan, q1.TenBNhan, q1.DChi, q1.GTinh, q1.Tuoi, q1.DTuong, q1.NoiTru, q1.MaDV, q1.ChiDinh1, q1.TenDV, q1.Loai, q1.KetLuan, kp.makp, kp.tenkp, q1.MaCBth, q1.NgayTH, q1.SoPhieu, q1.SoPhim, q1.SThe } into kq
                                           select new
                                           {
                                               TrongBH = kq.Key.TrongBH,
                                               MaBNhan = kq.Key.MaBNhan,
                                               TenBNhan = kq.Key.TenBNhan,
                                               GTinh = kq.Key.GTinh,
                                               Tuoi = kq.Key.Tuoi,
                                               NoiTru = kq.Key.NoiTru,
                                               DTuong = kq.Key.DTuong,
                                               DiaChi = kq.Key.DChi,
                                               MaDV = kq.Key.MaDV,
                                               TenDV = kq.Key.TenDV + kq.Key.ChiDinh1,
                                               KetQua = (DungChung.Bien.MaBV == "14018") ? kq.Key.KetLuan : (_cdha == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || _cdha == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT) ? kq.Key.KetLuan + " " + kq.Key.KetQua : kq.Key.KetLuan,
                                               MakP = kq.Key.makp,
                                               TenKP = kq.Key.tenkp,
                                               BSTH = kq.Key.MaCBth,
                                               NgayTH = kq.Key.NgayTH,
                                               Loai = kq.Key.Loai,
                                               ChanDoan = kq.Key.ChanDoan,
                                               kq.Key.SoPhim,
                                               kq.Key.SoPhieu,
                                               kq.Key.SThe
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
                                        if (a.TrongBH == 1)
                                        {
                                            themmoi.bhyt = "X";
                                        }
                                        else { themmoi.khac = "X"; }
                                        themmoi.diachi = a.DiaChi;
                                        themmoi.madv = a.MaDV;
                                        themmoi.yeucau = a.TenDV;
                                        themmoi.ketqua = a.KetQua;
                                        themmoi.makp = a.MakP;
                                        themmoi.noigui = a.TenKP;
                                        if (a.BSTH != null)
                                        {
                                            var a1 = (from k in _lCB.Where(p => p.MaCB == a.BSTH) select new { k.TenCB }).ToList();
                                            if (a1.Count > 0)
                                            { themmoi.bsth = a1.First().TenCB; }
                                        }
                                        if (a.NgayTH != null && a.NgayTH.ToString().Length >= 10)
                                        {
                                            themmoi.ngayth = a.NgayTH;
                                        }

                                        themmoi.loai = Convert.ToInt32(a.Loai);
                                        themmoi.chandoan = a.ChanDoan;
                                        themmoi.SoPhim = a.SoPhim ?? 0;
                                        themmoi.CoPhim = a.SoPhieu ?? 0;
                                        themmoi.SoTheBHYT = a.SThe;
                                        _BNhan.Add(themmoi);
                                    }
                                }
                            }
                            else
                            {
                                var qso0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                            join cls in data.CLS on vp.MaBNhan equals cls.MaBNhan
                                            join cd in data.ChiDinhs.Where(p => (DungChung.Bien.MaBV != "27194" ? true : (radTrongDM.SelectedIndex == 2 ? true : (p.TrongBH == radTrongDM.SelectedIndex)))).Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                            join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                            select new { clsct.KetQua, cls.ChanDoan, cd.MaDV, cd.ChiDinh1, cls.MaBNhan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, vp.NgayTT, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) } // bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru,
                                     ).ToList();
                                #region 30372
                                if (DungChung.Bien.MaBV == "30372")
                                {
                                    if (radTrongDM.SelectedIndex == 0)
                                    {
                                         qso0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                                    join cls in data.CLS on vp.MaBNhan equals cls.MaBNhan
                                                    join cd in data.ChiDinhs.Where(p => p.TrongBH == 0).Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                                    join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                                    select new { clsct.KetQua, cls.ChanDoan, cd.MaDV, cd.ChiDinh1, cls.MaBNhan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, vp.NgayTT, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) } // bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru,
                                                ).ToList();
                                    }
                                    else if (radTrongDM.SelectedIndex == 1)
                                    {
                                         qso0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                                    join cls in data.CLS on vp.MaBNhan equals cls.MaBNhan
                                                    join cd in data.ChiDinhs.Where(p => p.TrongBH == 1).Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                                    join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                                    select new { clsct.KetQua, cls.ChanDoan, cd.MaDV, cd.ChiDinh1, cls.MaBNhan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, vp.NgayTT, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) } // bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru,
                                                ).ToList();
                                    }
                                    else if (radTrongDM.SelectedIndex == 2)
                                    {
                                         qso0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                                    join cls in data.CLS on vp.MaBNhan equals cls.MaBNhan
                                                    join cd in data.ChiDinhs.Where(p => p.TrongBH == 0 || p.TrongBH == 1).Where(p => p.Status == 1).Where(p => p.NgoaiGioHC == tg || p.NgoaiGioHC == ng) on cls.IdCLS equals cd.IdCLS
                                                    join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                                    select new { clsct.KetQua, cls.ChanDoan, cd.MaDV, cd.ChiDinh1, cls.MaBNhan, cd.Status, cd.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, vp.NgayTT, SoPhim = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "30002" ? cd.SoPhim : null), SoPhieu = (DungChung.Bien.MaBV == "14018" ? clsct.SoPhieu : null) } // bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru,
                                                ).ToList();
                                    }
                                }
                                #endregion
                                var mabnhan = qso0.Select(p => p.MaBNhan).Distinct().ToList();
                                var Bnhan = data.BenhNhans.Where(o => mabnhan.Contains(o.MaBNhan) && DungChung.Bien.MaBV == "27183" ? (cboDTuong.SelectedIndex == 0 ? true : o.DTuong == cboDTuong.Text) : true).ToList();

                                var qso1 = (from cls in qso0 join bn in Bnhan on cls.MaBNhan equals bn.MaBNhan select new { cls.KetQua, cls.MaDV, cls.ChiDinh1, cls.Status, cls.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.NgayTT, cls.ChanDoan, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, cls.SoPhieu, cls.SoPhim, bn.SThe }).ToList();
                                var qso = (from cls in qso1 join dv in qdv on cls.MaDV equals dv.MaDV select new { cls.KetQua, cls.MaBNhan, cls.TenBNhan, cls.DChi, cls.GTinh, cls.Tuoi, cls.DTuong, cls.NoiTru, cls.ChiDinh1, cls.Status, cls.KetLuan, cls.MaKP, cls.MaCBth, cls.NgayTH, cls.ChanDoan, cls.NgayTT, dv.Loai, dv.TenRG, dv.TenDV, dv.MaDV, cls.SoPhieu, cls.SoPhim, cls.SThe }).ToList();
                                var qtc = (from kp in _lKhoaP
                                           join q1 in qso on kp.makp equals q1.MaKP
                                           group new { q1 } by new { q1.KetQua, q1.MaBNhan, q1.ChanDoan, q1.TenBNhan, q1.DChi, q1.GTinh, q1.Tuoi, q1.DTuong, q1.NoiTru, q1.MaDV, q1.ChiDinh1, q1.TenDV, q1.Loai, q1.KetLuan, kp.makp, kp.tenkp, q1.MaCBth, q1.NgayTH, q1.SoPhieu, q1.SoPhim, q1.SThe } into kq
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
                                               KetQua = (DungChung.Bien.MaBV == "14018") ? kq.Key.KetLuan : (_cdha == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || _cdha == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT) ? kq.Key.KetLuan + " " + kq.Key.KetQua : kq.Key.KetLuan,
                                               MakP = kq.Key.makp,
                                               TenKP = kq.Key.tenkp,
                                               BSTH = kq.Key.MaCBth,
                                               NgayTH = kq.Key.NgayTH,
                                               Loai = kq.Key.Loai,
                                               ChanDoan = kq.Key.ChanDoan,
                                               kq.Key.SoPhim,
                                               kq.Key.SoPhieu,
                                               kq.Key.SThe
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
                                        themmoi.madv = a.MaDV;
                                        themmoi.yeucau = a.TenDV;
                                        themmoi.ketqua = a.KetQua;
                                        themmoi.makp = a.MakP;
                                        themmoi.noigui = a.TenKP;
                                        if (a.BSTH != null)
                                        {
                                            var a1 = (from k in _lCB.Where(p => p.MaCB == a.BSTH) select new { k.TenCB }).ToList();
                                            if (a1.Count > 0)
                                            { themmoi.bsth = a1.First().TenCB; }
                                        }
                                        if (a.NgayTH != null && a.NgayTH.ToString().Length >= 10)
                                        {
                                            themmoi.ngayth = a.NgayTH;
                                        }

                                        themmoi.loai = Convert.ToInt32(a.Loai);
                                        themmoi.chandoan = a.ChanDoan;
                                        themmoi.SoPhim = a.SoPhim ?? 0;
                                        themmoi.CoPhim = a.SoPhieu ?? 0;
                                        themmoi.SoTheBHYT = a.SThe;
                                        _BNhan.Add(themmoi);
                                    }
                                }
                            }
                        }
                        if (chkBNkhongCLS.Checked == true)
                        {
                            var qdt0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                        join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                        join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                                        join dtct in data.DThuoccts.Where(p => p.IDCD == null || p.IDCD <= 0) on dt.IDDon equals dtct.IDDon
                                        where DungChung.Bien.MaBV == "27183" ? (cboDTuong.SelectedIndex == 0 ? true : bn.DTuong == cboDTuong.Text) : true
                                        select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, bn.NoiTru, dtct.MaKP, dtct.MaDV, dtct.MaCB, dtct.NgayNhap, dtct.IDCD, vp.NgayTT, bn.SThe })
                                                    .ToList();
                            var qdt1 = (from dt in qdt0
                                        join dv in qdv on dt.MaDV equals dv.MaDV
                                        select new { dt.MaBNhan, dt.TenBNhan, dt.DChi, dt.GTinh, dt.Tuoi, dt.DTuong, dt.NoiTru, dt.MaKP, dt.MaCB, dt.NgayNhap, dt.IDCD, dt.NgayTT, dv.MaDV, dv.TenDV, dv.Loai, dv.TenRG, dt.SThe }).ToList();
                            var qdt = (from kp in _lKhoaP
                                       join qd in qdt1 on kp.makp equals qd.MaKP
                                       group new { qd } by new { qd.MaBNhan, qd.NoiTru, qd.TenBNhan, qd.DChi, qd.GTinh, qd.Tuoi, qd.DTuong, qd.MaDV, qd.TenDV, qd.Loai, kp.makp, kp.tenkp, qd.MaCB, qd.NgayNhap, qd.SThe } into kq
                                       select new { kq.Key.MaBNhan, kq.Key.NoiTru, kq.Key.TenBNhan, kq.Key.DChi, kq.Key.GTinh, kq.Key.Tuoi, kq.Key.DTuong, kq.Key.MaDV, kq.Key.TenDV, kq.Key.Loai, kq.Key.makp, kq.Key.tenkp, kq.Key.MaCB, kq.Key.NgayNhap, kq.Key.SThe }).ToList();
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
                                    themmoi.madv = a.MaDV;
                                    themmoi.yeucau = a.TenDV;
                                    themmoi.makp = a.makp;
                                    themmoi.noigui = a.tenkp;
                                    if (a.MaCB != null)
                                    {
                                        var a1 = (from k in _lCB.Where(p => p.MaCB == a.MaCB) select new { k.TenCB }).ToList();
                                        if (a1.Count > 0)
                                        { themmoi.bsth = a1.First().TenCB; }
                                    }
                                    if (a.NgayNhap != null && a.NgayNhap.ToString().Length >= 10)
                                    {
                                        themmoi.ngayth = a.NgayNhap;
                                    }

                                    themmoi.loai = Convert.ToInt32(a.Loai);
                                    themmoi.SoPhim = 0;
                                    themmoi.CoPhim = 0;
                                    themmoi.SoTheBHYT = a.SThe;
                                    _BNhan.Add(themmoi);
                                }
                            }
                        }
                    }
                    #endregion

                    var maBnhans = _BNhan.Select(o => o.mabnhan);
                    var makps = _BNhan.Select(o => o.makp);
                    var qcd0 = data.BNKBs.Where(o => maBnhans.Contains(o.MaBNhan ?? 0) && makps.Contains(o.MaKP ?? 0)).ToList();
                    var qcd = (from bn in _BNhan
                               join bnkb in qcd0 on new { mabnhan = bn.mabnhan, makp = bn.makp } equals new { mabnhan = bnkb.MaBNhan ?? 0, makp = bnkb.MaKP ?? 0 }
                               select new { bnkb.MaBNhan, bnkb.MaKP, chandoan = DungChung.Bien.MaBV == "30009" ? bnkb.ChanDoanBD : bnkb.ChanDoan + " " + bnkb.BenhKhac, bnkb.BenhKhac, bnkb.ChanDoan, bnkb.MaICD, bnkb.MaICD2 }).ToList();
                    var listICD = data.ICD10.Where(o => true).ToList();
                    if (qcd.Count > 0)
                    {
                        foreach (var a in qcd)
                        {
                            foreach (var b in _BNhan)
                            {
                                if (a.MaBNhan == b.mabnhan)
                                {
                                    if (DungChung.Bien.MaBV == "14018")
                                    {
                                        b.chandoan = DungChung.Ham.GhepChuoiChanDoan_14018(listICD, a.ChanDoan, a.MaICD, a.MaICD2);
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(b.chandoan))
                                            b.chandoan = a.chandoan;
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

                    var clsCDs = (from cls in data.CLS.Where(o => maBnhans.Contains(o.MaBNhan ?? 0))
                                 join cd in data.ChiDinhs.Where(p => (DungChung.Bien.MaBV != "27194" ? true : (radTrongDM.SelectedIndex == 2 ? true : (p.TrongBH == radTrongDM.SelectedIndex)))) on cls.IdCLS equals cd.IdCLS
                                 select new { cls, cd }).ToList();
                    var _BNhanQ = (from bn in _BNhan.Where(p => p.noitru == _bn1 || p.noitru == _bn2)
                                   join cls in clsCDs.Where(p => (DungChung.Bien.MaBV != "27194" ? true : (radTrongDM.SelectedIndex == 2 ? true : (p.cd.TrongBH == radTrongDM.SelectedIndex)))) on bn.mabnhan equals cls.cls.MaBNhan
                                   group new { bn } by new { bn.makp, bn.noigui, bn.mabnhan, bn.tenbnhan, bn.diachi, bn.gtinh, bn.nam, bn.nu, bn.bhyt, bn.khac, bn.chandoan, bn.pl, bn.loai, bn.noitru, bn.madv, bn.yeucau, bn.ketqua, bn.bsth, bn.ngayth, bn.SoPhim, bn.CoPhim, bn.SoTheBHYT } into kq
                                   select new BNCLS {
                                       makp = kq.Key.makp, noigui = kq.Key.noigui, mabnhan = kq.Key.mabnhan, tenbnhan = kq.Key.tenbnhan, diachi = kq.Key.diachi, gtinh = kq.Key.gtinh, nam = kq.Key.nam, nu = kq.Key.nu, bhyt = kq.Key.bhyt, khac = kq.Key.khac, chandoan = kq.Key.chandoan, loai = kq.Key.loai, pl = kq.Key.pl, noitru = kq.Key.noitru, madv = kq.Key.madv, ketqua = kq.Key.ketqua, yeucau = kq.Key.yeucau, bsth = kq.Key.bsth, ngayth = kq.Key.ngayth, SoPhim = chkPhim.Checked == true ? (kq.Key.SoPhim == null ? 0 : kq.Key.SoPhim) : -1, CoPhim2025 = kq.Key.CoPhim == 0 ? kq.Key.SoPhim : 0, CoPhim2530 = kq.Key.CoPhim == 1 ? kq.Key.SoPhim : 0, NgayThangTH = kq.Key.ngayth != null ? ("Ngày " + kq.Key.ngayth.Value.ToString("dd/MM/yyyy")) : "", SoTheBHYT = kq.Key.SoTheBHYT }).ToList();
                    #region Trong ngoài danh mục 300372
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        if (radTrongDM.SelectedIndex == 0)
                        {
                             clsCDs = (from cls in data.CLS.Where(o => maBnhans.Contains(o.MaBNhan ?? 0))
                                          join cd in data.ChiDinhs.Where(p => p.TrongBH == 0) on cls.IdCLS equals cd.IdCLS
                                          select new { cls, cd }).ToList();
                             _BNhanQ = (from bn in _BNhan.Where(p => p.noitru == _bn1 || p.noitru == _bn2)
                                           join cls in clsCDs.Where(p => p.cd.TrongBH == 0) on bn.mabnhan equals cls.cls.MaBNhan
                                           group new { bn } by new { bn.makp, bn.noigui, bn.mabnhan, bn.tenbnhan, bn.diachi, bn.gtinh, bn.nam, bn.nu, bn.bhyt, bn.khac, bn.chandoan, bn.pl, bn.loai, bn.noitru, bn.madv, bn.yeucau, bn.ketqua, bn.bsth, bn.ngayth, bn.SoPhim, bn.CoPhim, bn.SoTheBHYT } into kq
                                           select new BNCLS
                                           {
                                               makp = kq.Key.makp,
                                               noigui = kq.Key.noigui,
                                               mabnhan = kq.Key.mabnhan,
                                               tenbnhan = kq.Key.tenbnhan,
                                               diachi = kq.Key.diachi,
                                               gtinh = kq.Key.gtinh,
                                               nam = kq.Key.nam,
                                               nu = kq.Key.nu,
                                               bhyt = kq.Key.bhyt,
                                               khac = kq.Key.khac,
                                               chandoan = kq.Key.chandoan,
                                               loai = kq.Key.loai,
                                               pl = kq.Key.pl,
                                               noitru = kq.Key.noitru,
                                               madv = kq.Key.madv,
                                               ketqua = kq.Key.ketqua,
                                               yeucau = kq.Key.yeucau,
                                               bsth = kq.Key.bsth,
                                               ngayth = kq.Key.ngayth,
                                               SoPhim = chkPhim.Checked == true ? (kq.Key.SoPhim == null ? 0 : kq.Key.SoPhim) : -1,
                                               CoPhim2025 = kq.Key.CoPhim == 0 ? kq.Key.SoPhim : 0,
                                               CoPhim2530 = kq.Key.CoPhim == 1 ? kq.Key.SoPhim : 0,
                                               NgayThangTH = kq.Key.ngayth != null ? ("Ngày " + kq.Key.ngayth.Value.ToString("dd/MM/yyyy")) : "",
                                               SoTheBHYT = kq.Key.SoTheBHYT
                                           }).ToList();
                        }
                        else if (radTrongDM.SelectedIndex == 1)
                        {
                            clsCDs = (from cls in data.CLS.Where(o => maBnhans.Contains(o.MaBNhan ?? 0))
                                      join cd in data.ChiDinhs.Where(p => p.TrongBH ==1) on cls.IdCLS equals cd.IdCLS
                                      select new { cls, cd }).ToList();
                            _BNhanQ = (from bn in _BNhan.Where(p => p.noitru == _bn1 || p.noitru == _bn2)
                                       join cls in clsCDs.Where(p => p.cd.TrongBH == 1) on bn.mabnhan equals cls.cls.MaBNhan
                                       group new { bn } by new { bn.makp, bn.noigui, bn.mabnhan, bn.tenbnhan, bn.diachi, bn.gtinh, bn.nam, bn.nu, bn.bhyt, bn.khac, bn.chandoan, bn.pl, bn.loai, bn.noitru, bn.madv, bn.yeucau, bn.ketqua, bn.bsth, bn.ngayth, bn.SoPhim, bn.CoPhim, bn.SoTheBHYT } into kq
                                       select new BNCLS
                                       {
                                           makp = kq.Key.makp,
                                           noigui = kq.Key.noigui,
                                           mabnhan = kq.Key.mabnhan,
                                           tenbnhan = kq.Key.tenbnhan,
                                           diachi = kq.Key.diachi,
                                           gtinh = kq.Key.gtinh,
                                           nam = kq.Key.nam,
                                           nu = kq.Key.nu,
                                           bhyt = kq.Key.bhyt,
                                           khac = kq.Key.khac,
                                           chandoan = kq.Key.chandoan,
                                           loai = kq.Key.loai,
                                           pl = kq.Key.pl,
                                           noitru = kq.Key.noitru,
                                           madv = kq.Key.madv,
                                           ketqua = kq.Key.ketqua,
                                           yeucau = kq.Key.yeucau,
                                           bsth = kq.Key.bsth,
                                           ngayth = kq.Key.ngayth,
                                           SoPhim = chkPhim.Checked == true ? (kq.Key.SoPhim == null ? 0 : kq.Key.SoPhim) : -1,
                                           CoPhim2025 = kq.Key.CoPhim == 0 ? kq.Key.SoPhim : 0,
                                           CoPhim2530 = kq.Key.CoPhim == 1 ? kq.Key.SoPhim : 0,
                                           NgayThangTH = kq.Key.ngayth != null ? ("Ngày " + kq.Key.ngayth.Value.ToString("dd/MM/yyyy")) : "",
                                           SoTheBHYT = kq.Key.SoTheBHYT
                                       }).ToList();
                        }
                        else if (radTrongDM.SelectedIndex == 2)
                        {
                            clsCDs = (from cls in data.CLS.Where(o => maBnhans.Contains(o.MaBNhan ?? 0))
                                      join cd in data.ChiDinhs.Where(p => p.TrongBH == 0 || p.TrongBH == 1) on cls.IdCLS equals cd.IdCLS
                                      select new { cls, cd }).ToList();
                            _BNhanQ = (from bn in _BNhan.Where(p => p.noitru == _bn1 || p.noitru == _bn2)
                                       join cls in clsCDs.Where(p => p.cd.TrongBH == 0 || p.cd.TrongBH == 1) on bn.mabnhan equals cls.cls.MaBNhan
                                       group new { bn } by new { bn.makp, bn.noigui, bn.mabnhan, bn.tenbnhan, bn.diachi, bn.gtinh, bn.nam, bn.nu, bn.bhyt, bn.khac, bn.chandoan, bn.pl, bn.loai, bn.noitru, bn.madv, bn.yeucau, bn.ketqua, bn.bsth, bn.ngayth, bn.SoPhim, bn.CoPhim, bn.SoTheBHYT } into kq
                                       select new BNCLS
                                       {
                                           makp = kq.Key.makp,
                                           noigui = kq.Key.noigui,
                                           mabnhan = kq.Key.mabnhan,
                                           tenbnhan = kq.Key.tenbnhan,
                                           diachi = kq.Key.diachi,
                                           gtinh = kq.Key.gtinh,
                                           nam = kq.Key.nam,
                                           nu = kq.Key.nu,
                                           bhyt = kq.Key.bhyt,
                                           khac = kq.Key.khac,
                                           chandoan = kq.Key.chandoan,
                                           loai = kq.Key.loai,
                                           pl = kq.Key.pl,
                                           noitru = kq.Key.noitru,
                                           madv = kq.Key.madv,
                                           ketqua = kq.Key.ketqua,
                                           yeucau = kq.Key.yeucau,
                                           bsth = kq.Key.bsth,
                                           ngayth = kq.Key.ngayth,
                                           SoPhim = chkPhim.Checked == true ? (kq.Key.SoPhim == null ? 0 : kq.Key.SoPhim) : -1,
                                           CoPhim2025 = kq.Key.CoPhim == 0 ? kq.Key.SoPhim : 0,
                                           CoPhim2530 = kq.Key.CoPhim == 1 ? kq.Key.SoPhim : 0,
                                           NgayThangTH = kq.Key.ngayth != null ? ("Ngày " + kq.Key.ngayth.Value.ToString("dd/MM/yyyy")) : "",
                                           SoTheBHYT = kq.Key.SoTheBHYT
                                       }).ToList();
                        }
                    }
                    #endregion
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
                    if (radioGroup1.SelectedIndex == 3)
                    {
                        _tenso = ("Sổ Nội soi").ToUpper();
                    }
                    if (radioGroup1.SelectedIndex == 4)
                    {
                        if (DungChung.Bien.MaBV == "30009")
                        {
                            _tenso = "SỔ CT - SCANNER";
                        }
                        else
                            _tenso = ("Sổ X-QuangCT").ToUpper();
                    }
                    if (radioGroup1.SelectedIndex == 5)
                    {
                        _tenso = "SỔ SIÊU ÂM DOPPLER";
                    }

                    if (radioGroup1.SelectedIndex == 6)
                    {
                        _tenso = "SỔ ĐO MẬT ĐỘ XƯƠNG";
                    }

                    if (radioGroup1.SelectedIndex == 7)
                    {
                        _tenso = "SỔ ĐO CHỨC NĂNG HÔ HẤP";
                    }


                    if (DungChung.Bien.MaBV == "14018" && radioGroup1.SelectedIndex == 1)
                    {
                        var source = _BNhanQ.OrderBy(p => p.ngayth).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("TenSo", "SỔ CHẨN ĐOÁN HÌNH ẢNH: " + _tenso);
                        dic.Add("TuNgay", tungay.ToString("dd/MM/yyyy"));
                        dic.Add("DenNgay", denngay.ToString("dd/MM/yyyy"));
                        dic.Add("TuNgayDenNgay", string.Format("Từ ngày {0} đến ngày {1}", tungay.ToString("dd/MM/yyyy"), denngay.ToString("dd/MM/yyyy")));
                        DungChung.Ham.Print(DungChung.PrintConfig.rep_BC_SoXquang14018_ID406, source, dic, false);
                    }
                    else
                    {
                        if (radMau.SelectedIndex == 0)
                        {
                            BaoCao.Rep_SoSieuAm_A4 rep = new BaoCao.Rep_SoSieuAm_A4();
                            if (ckXQuangCTDichvu.Checked)
                            {
                                rep.xrLabel19.Text = "Dịch vụ";
                            }
                            rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                            rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                            if (chkPhim.Checked)
                            {
                                rep.SL.Value = _BNhanQ.Sum(p => p.SoPhim);
                            }
                            else
                            {
                                rep.SL.Value = -1;
                            }
                            if (lupDichVu.EditValue == null || Convert.ToInt32(lupDichVu.EditValue) == 0)
                            {


                                rep.DataSource = _BNhanQ.OrderBy(p => p.ngayth).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                                rep.Nam.Value = _BNhanQ.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count();
                                rep.Nu.Value = _BNhanQ.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count();
                                rep.BHYT.Value = _BNhanQ.Where(p => p.bhyt == "X").Select(p => p.mabnhan).Count();
                                rep.Khac.Value = _BNhanQ.Where(p => p.khac == "X").Select(p => p.mabnhan).Count();

                                rep.YC.Value = DungChung.Bien.MaBV == "27001" && (radioGroup1.SelectedIndex == 6 || radioGroup1.SelectedIndex == 7) ? 0 : _BNhanQ.Sum(p => p.loai);
                                rep.TenSo.Value = _tenso;
                                _BNhanQ = _BNhanQ.OrderBy(p => p.ngayth).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                            }
                            else
                            {
                                int madv = lupDichVu.EditValue == null ? 0 : Convert.ToInt32(lupDichVu.EditValue);
                                rep.DataSource = _BNhanQ.Where(p => p.madv == madv).OrderBy(p => p.ngayth).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                                rep.Nam.Value = _BNhanQ.Where(p => p.madv == madv).Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count();
                                rep.Nu.Value = _BNhanQ.Where(p => p.madv == madv).Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count();
                                rep.BHYT.Value = _BNhanQ.Where(p => p.madv == madv).Where(p => p.bhyt == "X").Select(p => p.mabnhan).Count();
                                rep.Khac.Value = _BNhanQ.Where(p => p.madv == madv).Where(p => p.khac == "X").Select(p => p.mabnhan).Count();

                                rep.YC.Value = DungChung.Bien.MaBV == "27001" && (radioGroup1.SelectedIndex == 6 || radioGroup1.SelectedIndex == 7) ? 0 : _BNhanQ.Where(p => p.madv == madv).Sum(p => p.loai);
                                rep.TenSo.Value = _tenso;
                                _BNhanQ = _BNhanQ.Where(p => p.madv == madv).OrderBy(p => p.ngayth).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                            }

                            #region xuat Excel
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            string[] _tieude = { "STT", "Họ tên người bệnh", "Tuổi nam", "Tuổi nữ", "Địa chỉ", "Đối tượng BHYT", "Đối tượng khác", "Chẩn đoán", "Nơi gửi", "Yêu cầu", "Kết quả", "Bác sĩ thực hiện", "Ngày thực hiện", "Ghi chú" };
                            DungChung.Bien.MangHaiChieu = new Object[_BNhanQ.Count + 2, 15];
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            int num = 1;
                            foreach (var r in _BNhanQ)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.tenbnhan;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.nam;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.nu;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.bhyt;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.khac;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.chandoan;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.noigui;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.yeucau;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.ketqua;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.bsth;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.ngayth;
                                DungChung.Bien.MangHaiChieu[num, 13] = "";
                                num++;

                            }

                            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, _tenso, "C:\\Sosieuam.xls", true, this.Name);
                            #endregion

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
                            if (chkPhim.Checked == false)
                            {
                                rep.SL.Value = "-1";
                            }

                            if (lupDichVu.EditValue == null || Convert.ToInt32(lupDichVu.EditValue) == 0)
                            {
                                rep.DataSource = _BNhanQ.OrderBy(p => p.ngayth).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                                _BNhanQ = _BNhanQ.OrderBy(p => p.ngayth).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();


                            }
                            else
                            {
                                int madv = Convert.ToInt32(lupDichVu.EditValue);
                                rep.DataSource = _BNhanQ.Where(p => p.madv == madv).OrderBy(p => p.ngayth).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                                _BNhanQ = _BNhanQ.Where(p => p.madv == madv).OrderBy(p => p.ngayth).ThenBy(p => p.tenbnhan).ThenBy(p => p.noigui).ToList();
                            }
                            #region xuat Excel
                            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                            int[] _arrWidth = new int[] { };
                            string[] _tieude = { "STT", "Họ tên người bệnh", "Tuổi nam", "Tuổi nữ", "Địa chỉ", "Đối tượng BHYT", "Đối tượng khác", "Chẩn đoán", "Nơi gửi", "Yêu cầu", "Kết quả", "Bác sĩ thực hiện", "Ngày thực hiện", "Ghi chú" };
                            DungChung.Bien.MangHaiChieu = new Object[_BNhanQ.Count + 2, 15];
                            for (int i = 0; i < _tieude.Length; i++)
                            {
                                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                            }

                            int num = 1;
                            foreach (var r in _BNhanQ)
                            {
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.tenbnhan;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.nam;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.nu;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.bhyt;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.khac;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.chandoan;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.noigui;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.yeucau;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.ketqua;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.bsth;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.ngayth;
                                DungChung.Bien.MangHaiChieu[num, 13] = "";
                                num++;

                            }

                            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, _tenso, "C:\\Sosieuam.xls", true, this.Name);
                            #endregion
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        public class BNCLS
        {
            public int makp { get; set; }
            public string noigui { get; set; }
            public int mabnhan { get; set; }
            public string tenbnhan { get; set; }
            public string diachi { get; set; }
            public int gtinh { get; set; }
            public string nam { get; set; }
            public string nu { get; set; }
            public string bhyt { get; set; }
            public string khac { get; set; }
            public string chandoan { get; set; }
            public int loai { get; set; }
            public int pl { get; set; }
            public int noitru { get; set; }
            public int madv { get; set; }
            public string ketqua { get; set; }
            public string yeucau { get; set; }
            public string bsth { get; set; }
            public DateTime? ngayth { get; set; }
            public int SoPhim { get; set; }
            public int CoPhim2025 { get; set; }
            public int CoPhim2530 { get; set; }
            public string NgayThangTH { get; set; }
            public string SoTheBHYT { get; set; }
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
            if (radioGroup1.SelectedIndex != 4)
            {
                ckXQuangCTDichvu.Visible = false;
                ckXQuangCTDichvu.Checked = false;
            }
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
                ckNoiSoiTMH.Checked = false;
                ckNoiSoiKhac.Checked = false;

            }
            if (radioGroup1.SelectedIndex == 1)
            {
                _cdha = "X-Quang";
                ckNoiSoiTMH.Checked = false;
                ckNoiSoiKhac.Checked = false;
            }
            if (radioGroup1.SelectedIndex == 2)
            {
                _cdha = "Điện tim";
                ckNoiSoiTMH.Checked = false;
                ckNoiSoiKhac.Checked = false;
            }
            if (radioGroup1.SelectedIndex == 3)
            {
                ckNoiSoiTMH.Checked = true;
                ckNoiSoiKhac.Checked = true;
            }
            if (radioGroup1.SelectedIndex == 4)
            {
                _cdha = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT;
                ckNoiSoiTMH.Checked = false;
                ckNoiSoiKhac.Checked = false;
                if (DungChung.Bien.MaBV == "30009")
                {
                    ckXQuangCTDichvu.Visible = true;
                }

            }
            if (radioGroup1.SelectedIndex == 6)
            {
                _cdha = DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong;
            }
            if (radioGroup1.SelectedIndex == 7)
            {
                _cdha = "Đo chức năng hô hấp";
                ckNoiSoiTMH.Checked = false;
                ckNoiSoiKhac.Checked = false;
            }
            _ldv.Clear();
            var qdv = from dv in data.DichVus.Where(p => p.PLoai != 1)
                      join tn in data.TieuNhomDVs.Where(p => DungChung.Bien.MaBV == "27001" ? p.TenTN == _cdha : p.TenRG == _cdha) on dv.IdTieuNhom equals tn.IdTieuNhom
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chkBNkhongCLS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBNkhongCLS.Checked == true)
            {
                TrongGio.Enabled = false;
                NgoaiGio.Enabled = false;
            }
            else
            {
                TrongGio.Enabled = true;
                NgoaiGio.Enabled = true;
            }
        }

        private void chkBNcoCLS_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckXQuangCTDichvu_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}