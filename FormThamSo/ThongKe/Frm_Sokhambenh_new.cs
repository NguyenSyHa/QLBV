using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLBV.FormThamSo
{
    public partial class Frm_Sokhambenh_new : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Sokhambenh_new()
        {
            InitializeComponent();
        }

        #region class benh nhan

        private class benhnhan
        {
            //public int STT { get; set; }
            private int MaBN;

            private int Phuongan;
            private int IDKB;
            private string TuoiN;
            private string TuoiNu;
            private string Sothe;
            private string DTuongTP;
            private string DTuongBH;
            private int MaKP;
            private string NTT;
            private string Diachi;
            private string CDNoiGT;
            private string MaBVGT;
            private string NoiGT;
            private int NoiTru;
            private string CD;
            private string TenBS;
            private string TenBN;
            private string CapCuu;
            private int CT;
            private string TIDC;
            private string CDDT;
            private bool DTNT;
            private DateTime NKB;
            private DateTime Ngay;
            private int trongBH;

            public int TrongBH
            {
                get { return trongBH; }
                set { trongBH = value; }
            }

            private string ChiDinh1;

            public string chidinh1
            {
                get { return ChiDinh1; }
                set { ChiDinh1 = value; }
            }

            public DateTime ngay
            {
                get { return Ngay; }
                set { Ngay = value; }
            }

            public DateTime nkb
            {
                get { return NKB; }
                set { NKB = value; }
            }

            public bool dtnt
            {
                get { return DTNT; }
                set { DTNT = value; }
            }

            private string VV;

            public string VV1
            {
                get { return VV; }
                set { VV = value; }
            }

            private string TT;

            public string TT1
            {
                get { return TT; }
                set { TT = value; }
            }

            private string TD;

            public string TD1
            {
                get { return TD; }
                set { TD = value; }
            }

            private string NT;

            public string NT1
            {
                get { return NT; }
                set { NT = value; }
            }

            private string VN;

            public string VN1
            {
                get { return VN; }
                set { VN = value; }
            }

            private string ThuThuat;

            public string ThuThuat1
            {
                get { return ThuThuat; }
                set { ThuThuat = value; }
            }

            private string KhamCK;

            public string KhamCK1
            {
                get { return KhamCK; }
                set { KhamCK = value; }
            }

            public string cddt
            { set { CDDT = value; } get { return CDDT; } }

            public string ticd
            { set { TIDC = value; } get { return TIDC; } }

            public int ct
            { set { CT = value; } get { return CT; } }

            private int GT;

            public int gt
            { set { GT = value; } get { return GT; } }

            public string tenbn
            { set { TenBN = value; } get { return TenBN; } }

            public string tenbs
            { set { TenBS = value; } get { return TenBS; } }

            public string cd
            { set { CD = value; } get { return CD; } }

            public string mabvgt
            { set { MaBVGT = value; } get { return MaBVGT; } }

            public string noigt
            { set { NoiGT = value; } get { return NoiGT; } }

            public string cdnoigt
            { set { CDNoiGT = value; } get { return CDNoiGT; } }

            public string diachi
            { set { Diachi = value; } get { return Diachi; } }

            public int maBN
            { get { return MaBN; } set { MaBN = value; } }

            public int phuongan
            { get { return Phuongan; } set { Phuongan = value; } }

            public int idkb
            { get { return IDKB; } set { IDKB = value; } }

            public string tuoin
            { get { return TuoiN; } set { TuoiN = value; } }

            public string tuoinu
            { get { return TuoiNu; } set { TuoiNu = value; } }

            public string sothe
            { get { return Sothe; } set { Sothe = value; } }

            public string dtuongtp
            { get { return DTuongTP; } set { DTuongTP = value; } }

            public string dtuongbh
            { get { return DTuongBH; } set { DTuongBH = value; } }

            public string capcuu
            { get { return CapCuu; } set { CapCuu = value; } }

            public int makp
            { get { return MaKP; } set { MaKP = value; } }

            public string ntt
            { get { return NTT; } set { NTT = value; } }

            public int noitru
            { get { return NoiTru; } set { NoiTru = value; } }

            private string maBVC;

            public string MaBVC
            {
                get { return maBVC; }
                set { maBVC = value; }
            }

            private string NgheNgiep;

            public string NgheNgiep1
            {
                get { return NgheNgiep; }
                set { NgheNgiep = value; }
            }

            private string DanToc;

            public string DanToc1
            {
                get { return DanToc; }
                set { DanToc = value; }
            }

            private string TrieuChung;

            public string TrieuChung1
            {
                get { return TrieuChung; }
                set { TrieuChung = value; }
            }

            public string DoTuoi { get; set; }

            public int ThangTuoi { get; set; }
            public string NgeNghiep { get; set; }

            public string GhiChu { get; set; }
        }

        #endregion class benh nhan

        #region class KB

        private class KB
        {
            public int? MaBNhan { get; set; }
            public int IDKB { get; set; }
            public DateTime? Ngay { get; set; }
        }

        #endregion class KB

        private List<benhnhan> _benhnhan = new List<benhnhan>();
        private List<KPhong> _lKPhong = new List<KPhong>();
        private List<RaVien> _lRaVien = new List<RaVien>();
        private List<VaoVien> _lVaoVien = new List<VaoVien>();
        private List<BenhNhan> _lBenhNhan = new List<BenhNhan>();
        private List<BNKB> _lBNKB = new List<BNKB>();
        private List<CanBo> _lCB = new List<CanBo>();
        private List<KB> _lKB = new List<KB>();

        private void Frm_Sokhambenh_Load(object sender, EventArgs e)
        {
            lupNgaytu.Focus();
            lupNgaytu.DateTime = System.DateTime.Today;
            DateTime tungay = lupNgaytu.DateTime.AddDays(1).AddHours(23).AddMinutes(59);
            lupNgayden.DateTime = tungay;
            rgngay.SelectedIndex = 2;
            _lKPhong = (from TK in _Data.KPhongs where (TK.PLoai == "Phòng khám" || TK.PLoai == "Lâm sàng") select TK).ToList();
            _lKPhong.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });

            cklKP.DataSource = _lKPhong;
            //radMau.SelectedIndex = 1;
            radMau.SelectedIndex = 1;
            List<DTBN> _ldtuong = _Data.DTBNs.Where(p => p.Status == 1).ToList();
            _ldtuong.Insert(0, new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            cklDTBN.DataSource = _ldtuong;
            cklDTBN.CheckAll();
            radMau_SelectedIndexChanged(sender, e);
            if (DungChung.Bien.MaBV == "12122")
            {
                ckHThiDoTuoi.Checked = true;
                ckHTTheoNgay.Checked = true;
            }
            radXP_SelectedIndexChanged(sender, e);
            if (DungChung.Bien.MaBV == "12001")
            {
                this.Width = 690;
                sbtHuy.Width = 345;
                groupControl1.Visible = true;
            }
            else
            {
                this.Width = 440;
                sbtHuy.Width = 220;
            }
        }

        private QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        #region kiểm tra

        private bool KTtaoBc()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupNgayden.Focus();
                return false;
            }

            return true;
        }

        #endregion kiểm tra

        private void sbtTao_Click(object sender, EventArgs e)
        {
            List<string> _dsCSKCB = new List<string>();
            for (int i = 0; i < cklKP1.ItemCount; i++)
            {
                if (cklKP1.GetItemChecked(i))
                {
                    _dsCSKCB.Add(cklKP1.GetItemValue(i).ToString());
                }
            }
            _dsCSKCB = _dsCSKCB.Distinct().ToList();
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _benhnhan.Clear();
            _lKB.Clear();
            DateTime Ngaytu = lupNgaytu.DateTime;
            DateTime Ngayden = lupNgayden.DateTime;
            DateTime Ngaytu2 = Ngaytu.AddMonths(-2);
            DateTime Ngayden2 = Ngayden.AddMonths(2);
            int _TheoNgay = rgngay.SelectedIndex;
            int trongDM = 0;
            if (radTrongDM.SelectedIndex == 1)
            {
                trongDM = 1;
            }
            if (radTrongDM.SelectedIndex == 2)
            {
                trongDM = 2;
            }
            DungChung.Bien.c_chuyenkhoa.f_ChuyenKhoa();
            if (KTtaoBc())
            {
                //_lBNKB = (from kb in _Data.BNKBs.Where(p => p.NgayKham >= Ngaytu && p.NgayKham <= Ngayden)
                //          select kb).ToList();
                List<int> _lIDDTBN = new List<int>();
                for (int i = 0; i < cklDTBN.ItemCount; i++)
                {
                    if (cklDTBN.GetItemChecked(i))
                        _lIDDTBN.Add(Convert.ToInt16(cklDTBN.GetItemValue(i)));
                }
                _lCB = _Data.CanBoes.ToList();
                List<int> lMaKP = new List<int>();
                for (int i = 0; i < cklKP.ItemCount; i++)
                {
                    if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                        lMaKP.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
                }
                // int _makhoa = 0;
                _benhnhan.Clear();
                string _tuyenbv = "";
                var bv = (from b in _Data.BenhViens select new { b.MaBV, b.TenBV, b.TuyenBV, b.HangBV }).ToList();
                if (bv.Where(p => p.MaBV == (DungChung.Bien.MaBV)).First().TuyenBV != null)
                    _tuyenbv = bv.Where(p => p.MaBV == (DungChung.Bien.MaBV)).First().TuyenBV.ToString().Trim();
                //if (lupKhoa.EditValue != null)
                //    _makhoa = Convert.ToInt32(lupKhoa.EditValue);
                int[] _lMaBnhan = new int[100000];

                List<DmNN> nn = new List<DmNN>();
                List<DanToc> dt = new List<DanToc>();
                List<TTboXung> TTBX = new List<TTboXung>();
                if (DungChung.Bien.MaBV == "04002" || radMau.SelectedIndex == 2)
                {
                    nn = _Data.DmNNs.ToList();
                    dt = _Data.DanTocs.ToList();
                    TTBX = (from id in _lMaBnhan
                            join r in _Data.TTboXungs on id equals r.MaBNhan
                            select r).ToList();
                }

                if (rdgDieuKien.SelectedIndex == 0)//theo số bệnh nhân lấy lượt khám đầu
                {
                    if (_TheoNgay == 2)//theo ngày khám
                    {
                        var qbnkb = (from bnkb in _Data.BNKBs.Where(p => p.NgayKham >= Ngaytu && p.NgayKham <= Ngayden && (lMaKP.Contains(p.MaKP ?? -1)))
                                     group bnkb by bnkb.MaBNhan into kq
                                     select new KB { MaBNhan = kq.Key, IDKB = kq.Min(p => p.IDKB), Ngay = kq.Min(p => p.NgayKham) }).ToList();

                        _lKB.AddRange(qbnkb);
                    }
                    else if (_TheoNgay == 0)//theo ngày ra viện
                    {
                        var qbnkb = (from a in _Data.RaViens.Where(p => p.NgayRa >= Ngaytu && p.NgayRa <= Ngayden)
                                     join bnkb in _Data.BNKBs.Where(p => lMaKP.Contains(p.MaKP ?? -1)) on a.MaBNhan equals bnkb.MaBNhan
                                     group new { bnkb, a } by new { bnkb.MaBNhan, a.NgayRa } into kq
                                     select new KB { MaBNhan = kq.Key.MaBNhan, IDKB = kq.Min(p => p.bnkb.IDKB), Ngay = kq.Key.NgayRa }).ToList();
                        _lKB.AddRange(qbnkb);
                    }
                    else if (_TheoNgay == 1)//theo ngày thanh toán
                    {
                        var qbnkb = (from a in _Data.VienPhis.Where(p => p.NgayTT >= Ngaytu && p.NgayTT <= Ngayden)
                                     join bnkb in _Data.BNKBs.Where(p => lMaKP.Contains(p.MaKP ?? -1)) on a.MaBNhan equals bnkb.MaBNhan
                                     group new { bnkb, a } by new { bnkb.MaBNhan, a.NgayTT } into kq
                                     select new KB { MaBNhan = kq.Key.MaBNhan, IDKB = kq.Min(p => p.bnkb.IDKB), Ngay = kq.Key.NgayTT }).ToList();
                        _lKB.AddRange(qbnkb);
                    }
                }
                if (rdgDieuKien.SelectedIndex == 1)//theo lần khám
                {
                    if (_TheoNgay == 2)//theo ngày khám
                    {
                        var qbnkb = (from bnkb in _Data.BNKBs.Where(p => p.NgayKham >= Ngaytu && p.NgayKham <= Ngayden && (lMaKP.Contains(p.MaKP ?? -1)))// (_makhoa == 0 || p.MaKP == _makhoa))
                                     select new KB { MaBNhan = bnkb.MaBNhan, IDKB = bnkb.IDKB, Ngay = bnkb.NgayKham }).ToList();
                        _lKB.AddRange(qbnkb);
                    }
                    else if (_TheoNgay == 0)//theo ngày ra viện
                    {
                        var qbnkb = (from a in _Data.RaViens.Where(p => p.NgayRa >= Ngaytu && p.NgayRa <= Ngayden)
                                     join bnkb in _Data.BNKBs.Where(p => lMaKP.Contains(p.MaKP ?? -1)) on a.MaBNhan equals bnkb.MaBNhan
                                     select new KB { MaBNhan = bnkb.MaBNhan, IDKB = bnkb.IDKB, Ngay = a.NgayRa }).ToList();
                        _lKB.AddRange(qbnkb);
                    }
                    else if (_TheoNgay == 1)//theo ngày thanh toán
                    {
                        var qbnkb = (from a in _Data.VienPhis.Where(p => p.NgayTT >= Ngaytu && p.NgayTT <= Ngayden)
                                     join bnkb in _Data.BNKBs.Where(p => lMaKP.Contains(p.MaKP ?? -1)) on a.MaBNhan equals bnkb.MaBNhan
                                     select new KB { MaBNhan = bnkb.MaBNhan, IDKB = bnkb.IDKB, Ngay = a.NgayTT }).ToList();
                        _lKB.AddRange(qbnkb);
                    }
                }
                var q2 = (from
                          kb in _Data.BNKBs
                          join bn in _Data.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                          select new { bn.IDDTBN, bn.TChung, kb.ChanDoanBD, kb.MaCB, kb.IDKB, bn.MaBNhan, bn.SThe, bn.DTuong, bn.CapCuu, bn.Tuoi, bn.DChi, bn.CDNoiGT, bn.MaBV, bn.TenBNhan, bn.NoiTru, bn.GTinh, bn.DTNT, kb.BenhKhac, kb.ChanDoan, kb.MaCK, kb.MaICD, kb.MaICD2, kb.PhuongAn, MaKCB = bn.MaKCB == null ? "" : bn.MaKCB.Trim().ToUpper() }).ToList();
                var q = (from qkb in _lKB
                         join kb in q2 on qkb.IDKB equals kb.IDKB

                         select new { kb.IDDTBN, kb.TChung, kb.ChanDoanBD, kb.MaCB, kb.IDKB, kb.MaBNhan, kb.SThe, kb.DTuong, kb.CapCuu, kb.Tuoi, kb.DChi, kb.CDNoiGT, kb.MaBV, kb.TenBNhan, kb.NoiTru, kb.GTinh, kb.DTNT, kb.BenhKhac, kb.ChanDoan, kb.MaCK, NgayKham = qkb.Ngay, kb.MaICD, kb.MaICD2, kb.PhuongAn, MaKCB = kb.MaKCB == null ? "" : kb.MaKCB.Trim().ToUpper() }).ToList();

                var BN1 = (from a in q
                           join b in DungChung.Bien._lChuyenKhoa on a.MaCK equals b.MaCK
                           select new { ChuyenKhoa = b.ChuyenKhoa, a.IDDTBN, a.TChung, a.ChanDoanBD, a.MaCB, a.IDKB, a.MaBNhan, a.SThe, a.DTuong, a.CapCuu, a.Tuoi, a.DChi, a.CDNoiGT, a.MaBV, a.TenBNhan, a.NoiTru, a.GTinh, a.DTNT, a.BenhKhac, a.ChanDoan, a.MaCK, a.NgayKham, a.MaICD, a.MaICD2, a.PhuongAn }).Distinct().ToList();
                if (DungChung.Bien.MaBV == "12001")
                {
                    BN1 = (from a in q
                           join b in DungChung.Bien._lChuyenKhoa on a.MaCK equals b.MaCK
                           join cskcb in _dsCSKCB on a.MaKCB equals cskcb
                           select new { ChuyenKhoa = b.ChuyenKhoa, a.IDDTBN, a.TChung, a.ChanDoanBD, a.MaCB, a.IDKB, a.MaBNhan, a.SThe, a.DTuong, a.CapCuu, a.Tuoi, a.DChi, a.CDNoiGT, a.MaBV, a.TenBNhan, a.NoiTru, a.GTinh, a.DTNT, a.BenhKhac, a.ChanDoan, a.MaCK, a.NgayKham, a.MaICD, a.MaICD2, a.PhuongAn }).Distinct().ToList();
                }

                var bnNgheNghiep = (from ttbx in _Data.TTboXungs
                                    join nng in _Data.DmNNs on ttbx.MaNN equals nng.MaNN into kq
                                    from kq1 in kq.DefaultIfEmpty()
                                    join dtoc in _Data.DanTocs on ttbx.MaDT equals dtoc.MaDT into kqDT
                                    from kq2 in kqDT.DefaultIfEmpty()
                                    select new { ttbx.MaBNhan, kq1.TenNN, kq2.TenDT }).ToList();

                var BN = (from bn in BN1
                          join dt2 in _lIDDTBN on bn.IDDTBN equals dt2
                          //join tdv in _Data.TieuNhomDVs on dt2 equals tdv.
                          //join dv in _Data.DichVus on bn.Ma equals dv.MaDV
                          join nng in bnNgheNghiep on bn.MaBNhan equals nng.MaBNhan into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new
                          {
                              bn.IDDTBN,
                              bn.ChanDoanBD,
                              bn.TChung,
                              bn.MaCB,
                              bn.IDKB,
                              bn.MaBNhan,
                              bn.SThe,
                              bn.DTuong,
                              //bn.CapCuu,
                              bn.Tuoi,
                              bn.DChi,
                              NgeNghiep = kq1 == null ? "" : kq1.TenNN,
                              DanToc = kq1 == null ? "" : kq1.TenDT,
                              bn.CDNoiGT,
                              bn.MaBV,
                              bn.TenBNhan,
                              bn.NoiTru,
                              bn.GTinh,
                              bn.DTNT,
                              bn.BenhKhac,
                              bn.ChanDoan,
                              bn.ChuyenKhoa,
                              bn.NgayKham,
                              bn.MaICD,
                              bn.MaICD2,
                              bn.PhuongAn,
                              VN1 = (bn.PhuongAn == 0 || bn.PhuongAn == 3) ? "X" : "",
                              VV1 = (bn.PhuongAn == 1 && bn.DTNT == false) ? "X" : "",
                              NT1 = (bn.PhuongAn == 1 && bn.DTNT == true) ? "X" : "",
                              CapCuu = (bn.CapCuu == 1) ? "X" : ""
                          }).ToList();
                _lMaBnhan = BN.Select(p => p.MaBNhan).Distinct().ToArray();
                var _lBNhanDTNgoaiDM = BN.Select(p => new { p.MaBNhan, p.DTuong }).Distinct().ToArray();

                var qvp2 = (from vp in _Data.VienPhis.Where(p => p.NgayTT >= Ngaytu2 && p.NgayTT <= Ngayden2)
                            join vpct in _Data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                            //where vpct.TienBN > 0
                            select new { vp.MaBNhan, TienBN = vpct.TienBN, vpct.ThanhTien }).ToList();
                var qvp = (from id in _lMaBnhan
                           join vp in qvp2 on id equals vp.MaBNhan
                           group new { id, vp } by new { vp.MaBNhan } into kq
                           select new { kq.Key.MaBNhan, TienBN = kq.Sum(p => p.vp.TienBN), ThanhTien = kq.Sum(p => p.vp.ThanhTien) }).ToList();

                //var qthuoc2 = (from DT in _Data.DThuocs.Where(p => _lMaBnhan.Contains(p.MaBNhan??0)).Where(p => radMau.SelectedIndex == 2 ? p.PLDV == 1 : (chkHT_Thuoc.Checked ? true : p.PLDV == 2))//.Where(p => p.NgayKe >= Ngaytu2 && p.NgayKe <= Ngayden2)
                //               join DTct in _Data.DThuoccts.Where(p => p.TrongBH < 2) on DT.IDDon equals DTct.IDDon
                //               where (lMaKP.Contains(DTct.MaKP ?? -1))
                //               // where (_makhoa == 0 || DTct.MaKP == _makhoa)
                //               select new { DTct.TrongBH, DTct.MaDV, DT.MaBNhan, MaKP = DTct.MaKP, DT.PLDV, DTct.SoLuong, DTct.Loai }).ToList();
                var qthuoc3 = (from DT in _Data.DThuocs.Where(p => radMau.SelectedIndex == 2 ? p.PLDV == 1 : (chkHT_Thuoc.Checked ? true : p.PLDV == 2))//.Where(p => p.NgayKe >= Ngaytu2 && p.NgayKe <= Ngayden2)
                               join DTct in _Data.DThuoccts.Where(p => p.TrongBH < 2) on DT.IDDon equals DTct.IDDon
                               where (lMaKP.Contains(DTct.MaKP ?? -1))
                               // where (_makhoa == 0 || DTct.MaKP == _makhoa)
                               select new { DTct.TrongBH, DTct.MaDV, DT.MaBNhan, MaKP = DTct.MaKP, DT.PLDV, DTct.SoLuong, DTct.Loai }).ToList();
                var qthuoc2 = (from DT in qthuoc3.Where(p => (_lMaBnhan.Contains(p.MaBNhan ?? 0)))
                               select new { DT.TrongBH, DT.MaDV, DT.MaBNhan, MaKP = DT.MaKP, DT.PLDV, DT.SoLuong, DT.Loai }).ToList();
                var qthuoc = (from id in _lBNhanDTNgoaiDM
                              join th in qthuoc2 on id.MaBNhan equals th.MaBNhan
                              where (th.TrongBH == trongDM)
                              group new { id, th } by new { th.MaDV, th.MaBNhan, th.PLDV, id.DTuong, th.TrongBH } into k
                              select new { k.Key.TrongBH, k.Key.DTuong, k.Key.MaDV, k.Key.MaBNhan, k.Key.PLDV, SoLuong = k.Sum(p => p.th.SoLuong), Loai = k.Max(p => p.th.Loai) }).ToList();
                if (trongDM != 1 && trongDM != 0)
                {
                    qthuoc = (from id in _lBNhanDTNgoaiDM
                              join th in qthuoc2 on id.MaBNhan equals th.MaBNhan
                              //where (th.TrongBH == trongDM)
                              group new { id, th } by new { th.MaDV, th.MaBNhan, th.PLDV, id.DTuong, th.TrongBH } into k
                              select new { k.Key.TrongBH, k.Key.DTuong, k.Key.MaDV, k.Key.MaBNhan, k.Key.PLDV, SoLuong = k.Sum(p => p.th.SoLuong), Loai = k.Max(p => p.th.Loai) }).ToList();
                }
                //var rv = (from id in _lMaBnhan join r in _Data.RaViens on id equals r.MaBNhan select new { r.MaBNhan, r.MaBVC, r.Status }).ToList();
                var rv = (from r in _Data.RaViens.Where(p => _lMaBnhan.Contains(p.MaBNhan)) select new { r.MaBNhan, r.MaBVC, r.Status }).ToList();

                var dvu = (from d in _Data.DichVus.Where(x => x.Status == 1).Where(s => s.TrongDM == trongDM)
                           join tn in _Data.TieuNhomDVs on d.IdTieuNhom equals tn.IdTieuNhom
                           join nhom in _Data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                           //join bn in BN on cddv.MaBNhan equals bn.MaBNhan
                           where nhom.TenNhomCT != "Khám bệnh" && nhom.TenNhomCT != "Giường điều trị ngoại trú" && nhom.TenNhomCT != "Giường điều trị nội trú"
                           select new { d.PLoai, d.TenDV, d.MaDV, tn.TenRG, TrongBH = d.TrongDM }).ToList();
                var dvu1 = (from dtt in qthuoc
                            join bn in BN on dtt.MaBNhan equals bn.MaBNhan
                            join dv in _Data.DichVus on dtt.MaDV equals dv.MaDV
                            join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join nhom in _Data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                            //where _lIDDTBN.Contains(bn.IDDTBN)
                            where dv.Status == 1
                            //where nhom.TenNhomCT != "Khám bệnh" && nhom.TenNhomCT != "Giường điều trị ngoại trú" && nhom.TenNhomCT != "Giường điều trị nội trú"
                            where dtt.TrongBH == trongDM
                            select new { dv, tn, bn, dtt })
                            .AsEnumerable()
                            .Select(x => new
                            {
                                x.dv.PLoai,
                                x.dv.TenDV,
                                x.dv.MaDV,
                                x.tn.TenRG,
                                x.dtt.TrongBH,
                                x.bn.MaBNhan,
                                x.dtt.PLDV,
                                x.dtt.SoLuong,
                                x.bn.DTuong,
                                x.dtt.Loai
                            })
                            .ToList();

                if (trongDM != 1 && trongDM != 0)
                {
                    dvu = (from d in _Data.DichVus.Where(x => x.Status == 1).Where(s => s.TrongDM == trongDM)
                           join tn in _Data.TieuNhomDVs on d.IdTieuNhom equals tn.IdTieuNhom
                           join nhom in _Data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                           where nhom.TenNhomCT != "Khám bệnh" && nhom.TenNhomCT != "Giường điều trị ngoại trú" && nhom.TenNhomCT != "Giường điều trị nội trú"
                           select new { d.PLoai, d.TenDV, d.MaDV, tn.TenRG, TrongBH = d.TrongDM }).ToList();

                    dvu1 = (from dtt in qthuoc
                            join bn in BN on dtt.MaBNhan equals bn.MaBNhan
                            join dv in _Data.DichVus on dtt.MaDV equals dv.MaDV
                            join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join nhom in _Data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                            //where _lIDDTBN.Contains(bn.IDDTBN)
                            where dv.Status == 1
                            //where nhom.TenNhomCT != "Khám bệnh" && nhom.TenNhomCT != "Giường điều trị ngoại trú" && nhom.TenNhomCT != "Giường điều trị nội trú"
                            where dtt.TrongBH == trongDM
                            select new { dv, tn, bn, dtt })
                            .AsEnumerable()
                            .Select(x => new
                            {
                                x.dv.PLoai,
                                x.dv.TenDV,
                                x.dv.MaDV,
                                x.tn.TenRG,
                                x.dtt.TrongBH,
                                x.bn.MaBNhan,
                                x.dtt.PLDV,
                                x.dtt.SoLuong,
                                x.bn.DTuong,
                                x.dtt.Loai
                            })
                            .ToList();
                }

                foreach (var a in BN)
                {
                    benhnhan themmoi = new benhnhan();
                    themmoi.idkb = a.IDKB;
                    themmoi.tenbn = DungChung.Bien.MaBV == "26007" ? (a.MaBNhan + "-" + a.TenBNhan) : a.TenBNhan;
                    themmoi.maBN = a.MaBNhan;
                    themmoi.ThangTuoi = ThangTuoi(_Data, a.MaBNhan);
                    themmoi.DoTuoi = (a.Tuoi == 0 || a.Tuoi == null || themmoi.ThangTuoi == 1) ? "Dưới 12 tháng tuổi" : ((a.Tuoi >= 1 && a.Tuoi <= 5) ? "Từ 1 đến 5 Tuổi" : (a.Tuoi <= 15 ? "Từ 6 đến 15 tuổi" : "Từ trên 15 tuổi"));
                    if (a.GTinh != null)
                    {
                        if (a.GTinh == 1) { themmoi.tuoin = a.Tuoi.ToString(); }
                        if (a.GTinh == 0) { themmoi.tuoinu = a.Tuoi.ToString(); }
                    }
                    themmoi.mabvgt = a.MaBV;
                    themmoi.diachi = a.DChi;
                    themmoi.sothe = a.SThe;
                    themmoi.cdnoigt = a.CDNoiGT;
                    themmoi.noitru = a.NoiTru.Value;
                    themmoi.dtnt = a.DTNT;
                    themmoi.TrieuChung1 = DungChung.Bien.MaBV == "24297" ? a.ChanDoanBD : a.TChung;
                    if (DungChung.Bien.MaBV == "04002")
                    {
                        var ttbx = TTBX.Where(p => p.MaBNhan == a.MaBNhan).ToList();
                        if (ttbx.Count > 0)
                        {
                            string nn1 = ttbx.First() == null ? "" : ttbx.First().MaNN;
                            var abc = nn.Where(p => p.MaNN == nn1).ToList();
                            themmoi.NgheNgiep1 = abc.Count > 0 ? abc.First().TenNN : "";
                            string dt1 = ttbx.First() == null ? "" : ttbx.First().MaDT;
                            var def = dt.Where(p => p.MaDT == dt1).ToList();
                            themmoi.DanToc1 = def.Count > 0 ? def.First().TenDT : "";
                        }
                    }
                    if (radMau.SelectedIndex == 2)
                    {
                        themmoi.NgheNgiep1 = a.NgeNghiep;
                        themmoi.DanToc1 = a.DanToc;
                    }
                    string maICD = (a.MaICD2 == null || a.MaICD2 == "") ? a.MaICD : a.MaICD + ";" + a.MaICD2;
                    string chandoan = radMau.SelectedIndex == 2 ? a.ChanDoan : ((a.BenhKhac == null || a.BenhKhac == "") ? a.ChanDoan : a.ChanDoan + ";" + a.BenhKhac + " - " + maICD);
                    themmoi.ticd = DungChung.Ham.FreshString(chandoan);
                    if (DungChung.Bien.MaBV == "20001")
                        themmoi.cd = a.MaICD;
                    else themmoi.cd = maICD;
                    themmoi.VN1 = a.VN1;
                    themmoi.VV1 = a.VV1;
                    themmoi.NT1 = a.NT1;
                    themmoi.KhamCK1 = a.ChuyenKhoa;
                    if (a.NgayKham != null)
                    {
                        themmoi.nkb = a.NgayKham.Value.Date;
                        themmoi.ngay = Convert.ToDateTime(a.NgayKham);
                    }
                    if (a.DTuong != null)
                    {
                        if (a.DTuong == "Dịch vụ")
                        { themmoi.dtuongtp = "X"; }
                        if (a.DTuong == "BHYT")
                        {
                            if (qvp.Where(p => p.MaBNhan == a.MaBNhan).Sum(p => p.TienBN) > 0)
                            {
                                themmoi.dtuongtp = "X";
                            }
                            else
                            {
                                if (qvp.Where(p => p.MaBNhan == a.MaBNhan).Sum(p => p.ThanhTien) > 0)
                                    themmoi.dtuongbh = "X";
                            }
                        }
                    }
                    themmoi.capcuu = a.CapCuu;
                    var tenbs = _lCB.Where(p => p.MaCB == a.MaCB).ToList();
                    themmoi.tenbs = tenbs.Count > 0 ? ((DungChung.Bien.MaBV == "12001" && !String.IsNullOrEmpty(tenbs.First().ChucVu)) ? (tenbs.First().ChucVu + "." + tenbs.First().TenCB) : tenbs.First().TenCB) : "";
                    if (radMau.SelectedIndex == 1 || radMau.SelectedIndex == 0 || (radMau.SelectedIndex == 0 && DungChung.Bien.MaBV == "30002"))
                    {
                        if (DungChung.Bien.MaBV == "30372")
                        {
                            var thuocdung = (from qa in dvu1.Where(p => p.MaBNhan == a.MaBNhan && (chkHT_Thuoc.Checked ? true : p.PLDV == 2))//.Where(s => s.DTuong == "Dịch vụ")
                                             select new
                                             {
                                                 qa.PLoai,
                                                 qa.MaDV,
                                                 qa.TenDV,
                                                 qa.TenRG,
                                                 qa.TrongBH,
                                                 qa.DTuong
                                                 //qa.D
                                             }).Distinct().OrderBy(p => p.TenRG).ToList();

                            var chidinhk = (from qa in thuocdung
                                            where (qa.TenRG != "Thủ thuật" && qa.PLoai == 2)
                                            group qa by qa into kq
                                            select new
                                            {
                                                kq.Key.TenRG,
                                                kq.Key.TrongBH,
                                                kq.Key.DTuong
                                            }).Distinct().ToList();
                            foreach (var item in chidinhk)
                            {
                                //themmoi.cddt += item.TenRG + ", ";
                                if (trongDM == 0)
                                {
                                    if (item.TrongBH == 0 || (item.DTuong.ToLower() == ("Dịch vụ").ToLower() && item.TrongBH == 1))
                                    {
                                        themmoi.cddt += item.TenRG + ", ";
                                    }
                                }
                                else if (trongDM == 1)
                                {
                                    if (item.TrongBH == 1)// || (item.DTuong == "Dịch vụ" && item.TrongBH == 0))
                                    {
                                        themmoi.cddt += item.TenRG + ", ";
                                    }
                                }
                                else
                                {
                                    themmoi.cddt += item.TenRG + ", ";
                                }
                            }
                            foreach (var c in thuocdung.Where(p => p.TenRG == "Thủ thuật" || p.PLoai == 1))
                            {
                                if (trongDM == 0)
                                {
                                    if (c.TrongBH == 0 || (c.DTuong.ToLower() == ("Dịch vụ").ToLower() && c.TrongBH == 1))
                                    {
                                        themmoi.cddt += c.TenDV + ", ";
                                        if (c.TenRG == "Thủ thuật")
                                        {
                                            themmoi.ThuThuat1 = "X";
                                        }
                                    }
                                }
                                else if (trongDM == 1)
                                {
                                    if (c.TrongBH == 1)// || (c.DTuong == "Dịch vụ" && c.TrongBH == 0))
                                    {
                                        themmoi.cddt += c.TenDV + ", ";
                                        if (c.TenRG == "Thủ thuật")
                                        {
                                            themmoi.ThuThuat1 = "X";
                                        }
                                    }
                                }
                                else
                                {
                                    themmoi.cddt += c.TenDV + ", ";
                                    if (c.TenRG == "Thủ thuật")
                                    {
                                        themmoi.ThuThuat1 = "X";
                                    }
                                }
                            }
                        }
                        else
                        {
                            var thuocdung = (from qa in qthuoc.Where(p => p.MaBNhan == a.MaBNhan && (chkHT_Thuoc.Checked ? true : p.PLDV == 2)).Where(s => s.DTuong == "Dịch vụ")
                                             join b in dvu on qa.MaDV equals b.MaDV
                                             select new
                                             {
                                                 b.PLoai,
                                                 b.MaDV,
                                                 b.TenDV,
                                                 b.TenRG,
                                                 b.TrongBH,
                                                 qa.DTuong
                                                 //qa.D
                                             }).Distinct().OrderBy(p => p.TenRG).ToList();

                            var chidinhk = (from qa in thuocdung
                                            where (qa.TenRG != "Thủ thuật" && qa.PLoai == 2)
                                            group qa by qa into kq
                                            select new
                                            {
                                                kq.Key.TenRG,
                                                kq.Key.TrongBH,
                                                kq.Key.DTuong
                                            }).Distinct().ToList();
                            foreach (var item in chidinhk)
                            {
                                //themmoi.cddt += item.TenRG + ", ";
                                if (trongDM == 0)
                                {
                                    if (item.TrongBH == 0 || (item.DTuong.ToLower() == ("Dịch vụ").ToLower() && item.TrongBH == 1))
                                    {
                                        themmoi.cddt += item.TenRG + ", ";
                                    }
                                }
                                else if (trongDM == 1)
                                {
                                    if (item.TrongBH == 1)// || (item.DTuong == "Dịch vụ" && item.TrongBH == 0))
                                    {
                                        themmoi.cddt += item.TenRG + ", ";
                                    }
                                }
                                else
                                {
                                    themmoi.cddt += item.TenRG + ", ";
                                }
                            }
                            foreach (var c in thuocdung.Where(p => p.TenRG == "Thủ thuật" || p.PLoai == 1))
                            {
                                if (trongDM == 0)
                                {
                                    if (c.TrongBH == 0 || (c.DTuong == "Dịch vụ" && c.TrongBH == 1))
                                    {
                                        themmoi.cddt += c.TenDV + ", ";
                                        if (c.TenRG == "Thủ thuật")
                                        {
                                            themmoi.ThuThuat1 = "X";
                                        }
                                    }
                                }
                                else if (trongDM == 1)
                                {
                                    if (c.TrongBH == 1)// || (c.DTuong == "Dịch vụ" && c.TrongBH == 0))
                                    {
                                        themmoi.cddt += c.TenDV + ", ";
                                        if (c.TenRG == "Thủ thuật")
                                        {
                                            themmoi.ThuThuat1 = "X";
                                        }
                                    }
                                }
                                else
                                {
                                    themmoi.cddt += c.TenDV + ", ";
                                    if (c.TenRG == "Thủ thuật")
                                    {
                                        themmoi.ThuThuat1 = "X";
                                    }
                                }
                            }
                        }
                    }
                    if (radMau.SelectedIndex == 2)// || radMau.SelectedIndex == 0)
                    {
                        if (DungChung.Bien.MaBV == "30372")
                        {
                            var thuocdung = (from qa in dvu1.Where(p => p.MaBNhan == a.MaBNhan && p.PLDV == 1)
                                             select new
                                             {
                                                 qa.TenDV,
                                                 qa.SoLuong,
                                                 qa.Loai,
                                                 qa.TrongBH,
                                                 qa.DTuong
                                             }).Distinct().ToList();

                            foreach (var item in thuocdung)
                            {
                                if (trongDM == 0)
                                {
                                    if (item.TrongBH == 0 || (item.DTuong == "Dịch vụ" && item.TrongBH == 1))
                                    {
                                        if (item.Loai == 0)
                                        {
                                            themmoi.cddt += item.TenDV + " (" + item.SoLuong + "), ";
                                        }
                                        else
                                        {
                                            themmoi.cddt += item.TenDV + " (" + item.SoLuong + "_" + item.Loai + "), ";
                                        }
                                    }
                                }
                                else if (trongDM == 1)
                                {
                                    if (item.TrongBH == 1)//)// || (item.DTuong == "Dịch vụ" && item.TrongBH == 0))
                                    {
                                        if (item.Loai == 0)
                                        {
                                            themmoi.cddt += item.TenDV + " (" + item.SoLuong + "), ";
                                        }
                                        else
                                        {
                                            themmoi.cddt += item.TenDV + " (" + item.SoLuong + "_" + item.Loai + "), ";
                                        }
                                    }
                                }
                                else
                                {
                                    if (item.Loai == 0)
                                    {
                                        themmoi.cddt += item.TenDV + " (" + item.SoLuong + "), ";
                                    }
                                    else
                                    {
                                        themmoi.cddt += item.TenDV + " (" + item.SoLuong + "_" + item.Loai + "), ";
                                    }
                                }
                            }
                        }
                        else
                        {
                            var thuocdung = (from qa in qthuoc.Where(p => p.MaBNhan == a.MaBNhan && p.PLDV == 1)
                                             join b in dvu on qa.MaDV equals b.MaDV
                                             select new
                                             {
                                                 b.TenDV,
                                                 qa.SoLuong,
                                                 qa.Loai,
                                                 b.TrongBH,
                                                 qa.DTuong
                                             }).Distinct().ToList();

                            foreach (var item in thuocdung)
                            {
                                if (trongDM == 0)
                                {
                                    if (item.TrongBH == 0 || (item.DTuong == "Dịch vụ" && item.TrongBH == 1))
                                    {
                                        if (item.Loai == 0)
                                        {
                                            themmoi.cddt += item.TenDV + " (" + item.SoLuong + "), ";
                                        }
                                        else
                                        {
                                            themmoi.cddt += item.TenDV + " (" + item.SoLuong + "_" + item.Loai + "), ";
                                        }
                                    }
                                }
                                else if (trongDM == 1)
                                {
                                    if (item.TrongBH == 1)//)// || (item.DTuong == "Dịch vụ" && item.TrongBH == 0))
                                    {
                                        if (item.Loai == 0)
                                        {
                                            themmoi.cddt += item.TenDV + " (" + item.SoLuong + "), ";
                                        }
                                        else
                                        {
                                            themmoi.cddt += item.TenDV + " (" + item.SoLuong + "_" + item.Loai + "), ";
                                        }
                                    }
                                }
                                else
                                {
                                    if (item.Loai == 0)
                                    {
                                        themmoi.cddt += item.TenDV + " (" + item.SoLuong + "), ";
                                    }
                                    else
                                    {
                                        themmoi.cddt += item.TenDV + " (" + item.SoLuong + "_" + item.Loai + "), ";
                                    }
                                }
                            }
                        }
                    }

                    var tuyen = (from r in rv.Where(p => p.Status == 1)
                                 join b in bv on r.MaBVC equals b.MaBV
                                 where (r.MaBNhan == a.MaBNhan)
                                 select new { b.TuyenBV, b.MaBV }).ToList();
                    string tuyen2 = "", mabv = "";
                    if (tuyen.Count > 0)
                    {
                        tuyen2 = tuyen.First().TuyenBV.Trim();
                        mabv = tuyen.First().MaBV.Trim();
                    }
                    switch (_tuyenbv)
                    {
                        case "A":
                            if (tuyen2 == "B" || tuyen2 == "C" || tuyen2 == "D")
                            { themmoi.TD1 = "X"; }
                            break;

                        case "B":
                            if (tuyen2 == "A")
                            {
                                if (DungChung.Bien.MaBV == "12122")
                                {
                                    themmoi.TT1 = mabv.ToString();
                                }
                                else
                                {
                                    themmoi.TT1 = "X";
                                }
                                if (DungChung.Bien.MaBV == "12001")
                                    themmoi.GhiChu = "Tuyến trên";
                            }
                            if (tuyen2 == "C" || tuyen2 == "D")
                            {
                                if (DungChung.Bien.MaBV == "12122")
                                {
                                    themmoi.TD1 = mabv.ToString();
                                }
                                else
                                {
                                    themmoi.TD1 = "X";
                                }
                            }
                            break;

                        case "C":
                            if (tuyen2 == "A" || tuyen2 == "B")
                            { themmoi.TT1 = "X"; }
                            if (tuyen2 == "D")
                            { themmoi.TD1 = "X"; }
                            break;

                        case "D":
                            if (tuyen2 == "A" || tuyen2 == "B" || tuyen2 == "C")
                            { themmoi.TT1 = "X"; }
                            break;
                    }

                    if (DungChung.Bien.MaBV == "27194" || DungChung.Bien.MaBV == "24272")
                    {
                        var cbbd1 = (from cls in _Data.CLS.Where(p => p.Status == 1 && p.MaBNhan == a.MaBNhan) //lay chi dinh
                                     join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                     join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                                     select new { dv.TenDV, cd.Status, dv.TrongDM }).Distinct().ToList();

                        var cbbd2 = (from d in _Data.DThuocs.Where(p => p.MaBNhan == a.MaBNhan) // lay thuoc
                                     join dtct in _Data.DThuoccts.Where(p => p.Status == 1) on d.IDDon equals dtct.IDDon
                                     join dv in _Data.DichVus on dtct.MaDV equals dv.MaDV
                                     select new { dv.TenDV, dv.TrongDM, dtct.Status, }).Distinct().ToList();

                        string a1 = "- Chỉ định: \n";

                        int dem = 0;
                        foreach (var item in cbbd1)
                        {
                            if (trongDM == 0)
                            {
                                if (item.TrongDM == 0 && item.Status == 1)
                                {
                                    a1 = a1 + item.TenDV + ";\n  ";
                                    dem++;
                                }
                            }
                            else if (trongDM == 1)
                            {
                                if (item.TrongDM == 1 && item.Status == 1)
                                {
                                    a1 = a1 + item.TenDV + ";\n  ";
                                    dem++;
                                }
                            }
                            else
                            {
                                if (item.Status == 1)
                                {
                                    a1 = a1 + item.TenDV + ";\n  ";
                                    dem++;
                                }
                            }
                        }

                        if (dem == 0)
                        {
                            a1 = "";
                        }
                        dem = 0;
                        string a2 = "\n- Thuốc:\n";
                        foreach (var item in cbbd2)
                        {
                            if (trongDM == 0)
                            {
                                if (item.TrongDM == 0 && item.Status == 1)
                                {
                                    a2 = a2 + item.TenDV + ";\n   ";
                                    dem++;
                                }
                            }
                            else if (trongDM == 1)
                            {
                                if (item.TrongDM == 1 && item.Status == 1)
                                {
                                    a2 = a2 + item.TenDV + ";\n   ";
                                    dem++;
                                }
                            }
                            else
                            {
                                if (item.Status == 1)
                                {
                                    a2 = a2 + item.TenDV + ";\n   ";
                                    dem++;
                                }
                            }
                        }
                        if (dem == 0)
                        {
                            a2 = "";
                        }
                        string a3 = a1 + a2;
                        if (string.IsNullOrEmpty(a3) && trongDM == 0)
                        {
                            continue;
                        }
                        if (string.IsNullOrEmpty(a3) && trongDM == 1)
                        {
                            continue;
                        }
                        themmoi.chidinh1 = a3;
                    }
                    if (string.IsNullOrEmpty(themmoi.cddt) && trongDM == 0)
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(themmoi.cddt) && trongDM == 1)
                    {
                        continue;
                    }

                    #region comment

                    //var cbbd1 = (from d in BN.Where(p => p.MaBNhan == a.MaBNhan) //lay chi dinh
                    //            join cls in _Data.CLS.Where(p => p.Status == 1) on d.MaBNhan equals cls.MaBNhan
                    //            join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                    //            join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                    //            select new {dv.TenDV, cd.Status, dv.TrongDM, }).Distinct().ToList();

                    //var cbbd2 = (from d in BN.Where(p => p.MaBNhan == a.MaBNhan)// lay thuoc
                    //             join dtt in _Data.DThuocs on d.MaBNhan equals dtt.MaBNhan
                    //             join dtct in _Data.DThuoccts.Where(p => p.Status == 1) on dtt.IDDon equals dtct.IDDon
                    //             join dv in _Data.DichVus on dtct.MaDV equals dv.MaDV
                    //             select new { dv.TenDV, dv.TrongDM, dtct.Status, }).Distinct().ToList();
                    //string a1 = "- Chỉ định: \n";

                    //int dem = 0;
                    //foreach (var item in cbbd1)
                    //{
                    //    if (item.TrongDM == 1 && item.Status == 1)
                    //    {
                    //        a1 = a1 + item.TenDV + ";\n  ";
                    //        dem++;
                    //    }
                    //}
                    //if (dem == 0)
                    //{
                    //    a1 = "";
                    //}
                    //dem = 0;
                    //string a2 = "\n- Thuốc:\n";
                    //foreach (var item in cbbd2)
                    //{
                    //    if (item.TrongDM == 1 && item.Status == 1)
                    //    {
                    //        a2 = a2 + item.TenDV + ";\n   ";
                    //        dem++;
                    //    }
                    //}
                    //if (dem == 0)
                    //{
                    //    a2 = "";
                    //}
                    //string a3 = a1 + a2;
                    //themmoi.chidinh1 =  a3;

                    #endregion comment

                    var noigt = bv.Where(p => p.MaBV == a.MaBV).ToList();
                    themmoi.noigt = noigt.Count > 0 ? noigt.First().TenBV : "";
                    _benhnhan.Add(themmoi);
                }

                int _a = 0;

                if (radMau.SelectedIndex == 0)// mẫu A4
                {
                    #region 04002

                    if (DungChung.Bien.MaBV == "04002")
                    {
                        BaoCao.Rep_SokhambenhA4_04002 rep = new BaoCao.Rep_SokhambenhA4_04002(_a, _TheoNgay);
                        rep.Ngaythang.Value = "Từ ngày: " + lupNgaytu.Text.Substring(0, 10) + " đến ngày: " + lupNgayden.Text.Substring(0, 10);
                        rep.NgayBD.Value = "- Bắt đầu sử dụng ngày: " + lupNgaytu.Text.Substring(0, 10);
                        rep.NgayLT.Value = "- Hết sổ nộp lưu trữ ngày: " + lupNgayden.Text.Substring(0, 10);
                        var qkp = (from kp in _Data.KPhongs.Where(p => lMaKP.Contains(p.MaKP)) //_makhoa == 0 || p.MaKP == _makhoa)
                                   select new { kp.TenKP }).ToList();
                        if (qkp.Count() > 0)
                        {
                            rep.Khoa.Value = qkp.First().TenKP.ToUpper();
                        }

                        rep.DataSource = _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList();// _benhnhan.OrderBy(p => p.nkb.ToString().Substring(3, 2)).ThenBy(p => p.nkb.ToString().Substring(0, 2)).ThenBy(p => p.tenbn).ToList();

                        rep.BindingData();
                        rep.CreateDocument();

                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[_benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList().Count + 1, 19];
                        string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Địa chỉ", "Số bảo hiểm y tế", "Nghề nghiệp", "Dân tộc", "Triệu chứng", "Nơi giới thiệu", "Chẩn đoán", "Vào viện", "Tuyến trên", "Tuyến dưới", "Ngoại trú", "về nhà", "Khám chuyên khoa", "Họ và tên bác sỹ khám bệnh", "Ngày khám" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        foreach (var r in _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.tuoin;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.tuoinu;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.sothe;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.NgheNgiep1;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.DanToc1;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.TrieuChung1;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.noigt;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.ticd;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.TT1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.TD1;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.NT1;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.VN1;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.KhamCK1;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.tenbs;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.nkb;
                            num++;
                        }

                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ khám bệnh", "C:\\SoKhamBenh.xls", true, this.Name);

                        //rep.DataMember = "Table";
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                    #endregion 04002

                    #region bv 30002

                    else if (DungChung.Bien.MaBV == "30002")
                    {
                        BaoCao.Rep_SokhambenhA4_30002 rep = new BaoCao.Rep_SokhambenhA4_30002(_a, ckHThiDoTuoi.Checked, ckHTTheoNgay.Checked, _TheoNgay);
                        rep.Ngaythang.Value = "Từ ngày: " + lupNgaytu.Text.Substring(0, 10) + " đến ngày: " + lupNgayden.Text.Substring(0, 10);
                        rep.NgayBD.Value = "- Bắt đầu sử dụng ngày: " + lupNgaytu.Text.Substring(0, 10);
                        rep.NgayLT.Value = "- Hết sổ nộp lưu trữ ngày: " + lupNgayden.Text.Substring(0, 10);
                        var qkp = (from kp in _Data.KPhongs.Where(p => lMaKP.Contains(p.MaKP)) //.Where(p => _makhoa == 0 || p.MaKP == _makhoa)
                                   select new { kp.TenKP }).ToList();
                        if (qkp.Count() > 0)
                        {
                            rep.Khoa.Value = qkp.First().TenKP.ToUpper();
                        }

                        rep.DataSource = _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList();//.OrderBy(p => p.nkb.ToString().Substring(3, 2)).ThenBy(p => p.nkb.ToString().Substring(0, 2)).ThenBy(p => p.tenbn).ToList();

                        rep.BindingData();
                        rep.CreateDocument();

                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[_benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList().Count + 1, 16];
                        string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Địa chỉ", "Số bảo hiểm y tế", "Nơi giới thiệu", "Chẩn đoán", "Vào viện", "Tuyến trên", "Tuyến dưới", "Ngoại trú", "về nhà", "Khám chuyên khoa", "Họ và tên bác sỹ khám bệnh", "Ngày khám" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        foreach (var r in _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.tuoin;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.tuoinu;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.sothe;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.noigt;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.ticd;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.TT1;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.TD1;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.NT1;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.VN1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.KhamCK1;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.tenbs;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.nkb;
                            num++;
                        }

                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ khám bệnh", "C:\\SoKhamBenh.xls", true, this.Name);

                        //rep.DataMember = "Table";
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                    #endregion bv 30002

                    #region bv khác

                    else
                    {
                        BaoCao.Rep_SokhambenhA4 rep = new BaoCao.Rep_SokhambenhA4(_a, ckHThiDoTuoi.Checked, ckHTTheoNgay.Checked, _TheoNgay);
                        rep.Ngaythang.Value = "Từ ngày: " + lupNgaytu.Text.Substring(0, 10) + " đến ngày: " + lupNgayden.Text.Substring(0, 10);
                        rep.NgayBD.Value = "- Bắt đầu sử dụng ngày: " + lupNgaytu.Text.Substring(0, 10);
                        rep.NgayLT.Value = "- Hết sổ nộp lưu trữ ngày: " + lupNgayden.Text.Substring(0, 10);
                        var qkp = (from kp in _Data.KPhongs.Where(p => lMaKP.Contains(p.MaKP))  //.Where(p => _makhoa == 0 || p.MaKP == _makhoa)
                                   select new { kp.TenKP }).ToList();
                        if (qkp.Count() > 0)
                        {
                            rep.Khoa.Value = qkp.First().TenKP.ToUpper();
                        }

                        rep.DataSource = _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList();//.OrderBy(p => p.nkb.ToString().Substring(3, 2)).ThenBy(p => p.nkb.ToString().Substring(0, 2)).ThenBy(p => p.tenbn).ToList();

                        rep.BindingData();
                        rep.CreateDocument();

                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        int[] _arrWidth = new int[] { };
                        int num = 1;
                        DungChung.Bien.MangHaiChieu = new Object[_benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList().Count + 1, 16];
                        string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Địa chỉ", "Số bảo hiểm y tế", "Nơi giới thiệu", "Chẩn đoán", "Vào viện", "Tuyến trên", "Tuyến dưới", "Ngoại trú", "về nhà", "Khám chuyên khoa", "Họ và tên bác sỹ khám bệnh", "Ngày khám" };
                        for (int i = 0; i < _tieude.Length; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }

                        foreach (var r in _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList())
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.tuoin;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.tuoinu;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.sothe;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.noigt;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.ticd;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.VV1;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.TT1;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.TD1;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.NT1;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.VN1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.KhamCK1;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.tenbs;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.nkb;
                            num++;
                        }

                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ khám bệnh", "C:\\SoKhamBenh.xls", true, this.Name);

                        //rep.DataMember = "Table";
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                    #endregion bv khác
                }
                else if (radMau.SelectedIndex == 1 && DungChung.Bien.MaBV == "20001")// mẫu A3
                {
                    #region mẫu A3

                    BaoCao.Rep_Sokhambenh_new_20001 rep = new BaoCao.Rep_Sokhambenh_new_20001(ckHThiDoTuoi.Checked, ckHTTheoNgay.Checked);
                    var qkp = (from kp in _Data.KPhongs.Where(p => lMaKP.Contains(p.MaKP)) //.Where(p => p.MaKP == _makhoa)
                               select new { kp.TenKP }).ToList();
                    if (qkp.Count() > 0)
                    {
                        rep.Khoa.Value = qkp.First().TenKP.ToUpper();
                    }
                    rep.Ngaythang.Value = "Từ ngày: " + lupNgaytu.Text.Substring(0, 10) + " đến ngày: " + lupNgayden.Text.Substring(0, 10);
                    rep.NgayBD.Value = "- Bắt đầu sử dụng ngày: " + lupNgaytu.Text.Substring(0, 10);
                    rep.NgayLT.Value = "- Hết sổ nộp lưu trữ ngày: " + lupNgayden.Text.Substring(0, 10);

                    rep.DataSource = _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    //rep.DataMember = "Table";

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[_benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList().Count + 1, 22];
                    string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Địa chỉ", "Số bảo hiểm y tế", "Nơi giới thiệu", "Chẩn đoán tuyến dưới", "Chẩn đoán khoa khám bệnh", "Mã ICD", "Các chỉ định điều trị (Nếu có thủ thuật thì ghi rõ)", "Vào viện", "Tuyến trên", "Tuyến dưới", "Ngoại trú", "Về nhà", "Tiến hành TT", "Khám chuyên khoa", "Họ và tên bác sỹ khám bệnh", "ĐT Thu phí", "ĐT Miễn phí", "ĐT Cấp cứu" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }

                    foreach (var r in _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList())
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.tuoin;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.tuoinu;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.sothe;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.noigt;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.cdnoigt;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.ticd;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.cd;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.cddt;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.VV1;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.TT1;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.TD1;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.NT1;
                        DungChung.Bien.MangHaiChieu[num, 15] = r.VN1;
                        DungChung.Bien.MangHaiChieu[num, 16] = r.ThuThuat1;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.KhamCK1;
                        DungChung.Bien.MangHaiChieu[num, 18] = r.tenbs;
                        DungChung.Bien.MangHaiChieu[num, 19] = r.dtuongtp;
                        DungChung.Bien.MangHaiChieu[num, 20] = r.dtuongbh;
                        DungChung.Bien.MangHaiChieu[num, 21] = r.capcuu;
                        num++;
                    }

                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ khám bệnh", "C:\\SoKhamBenh.xls", true, this.Name);

                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                    #endregion mẫu A3
                }
                else if (radMau.SelectedIndex == 1)// mẫu A3
                {
                    #region mẫu A3

                    BaoCao.Rep_Sokhambenh_new rep = new BaoCao.Rep_Sokhambenh_new(ckHThiDoTuoi.Checked, ckHTTheoNgay.Checked);
                    var qkp = (from kp in _Data.KPhongs.Where(p => lMaKP.Contains(p.MaKP)) //.Where(p => p.MaKP == _makhoa)
                               select new { kp.TenKP }).ToList();
                    if (qkp.Count() > 0)
                    {
                        rep.Khoa.Value = qkp.First().TenKP.ToUpper();
                    }
                    rep.Ngaythang.Value = "Từ ngày: " + lupNgaytu.Text.Substring(0, 10) + " đến ngày: " + lupNgayden.Text.Substring(0, 10);
                    rep.NgayBD.Value = "- Bắt đầu sử dụng ngày: " + lupNgaytu.Text.Substring(0, 10);
                    rep.NgayLT.Value = "- Hết sổ nộp lưu trữ ngày: " + lupNgayden.Text.Substring(0, 10);

                    rep.DataSource = _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    //rep.DataMember = "Table";

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[_benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList().Count + 1, 21];
                    string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Địa chỉ", "Số bảo hiểm y tế", "Nơi giới thiệu", "Chẩn đoán tuyến dưới", "Chẩn đoán khoa khám bệnh", "Các chỉ định điều trị (Nếu có thủ thuật thì ghi rõ)", "Vào viện", "Tuyến trên", "Tuyến dưới", "Ngoại trú", "Về nhà", "Tiến hành TT", "Khám chuyên khoa", "Họ và tên bác sỹ khám bệnh", "ĐT Thu phí", "ĐT Miễn phí", "ĐT Cấp cứu" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }

                    foreach (var r in _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList())
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.tuoin;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.tuoinu;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.sothe;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.noigt;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.cdnoigt;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.ticd;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.cddt;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.VV1;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.TT1;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.TD1;
                        DungChung.Bien.MangHaiChieu[num, 13] = r.NT1;
                        DungChung.Bien.MangHaiChieu[num, 14] = r.VN1;
                        DungChung.Bien.MangHaiChieu[num, 15] = r.ThuThuat1;
                        DungChung.Bien.MangHaiChieu[num, 16] = r.KhamCK1;
                        DungChung.Bien.MangHaiChieu[num, 17] = r.tenbs;
                        DungChung.Bien.MangHaiChieu[num, 18] = r.dtuongtp;
                        DungChung.Bien.MangHaiChieu[num, 19] = r.dtuongbh;
                        DungChung.Bien.MangHaiChieu[num, 20] = r.capcuu;
                        num++;
                    }

                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ khám bệnh", "C:\\SoKhamBenh.xls", true, this.Name);

                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                    #endregion mẫu A3
                }
                else if (radMau.SelectedIndex == 2) // mẫu A1_tt27
                {
                    #region mẫu A1_tt27

                    BaoCao.Rep_SokhambenhA4_TT27 rep = new BaoCao.Rep_SokhambenhA4_TT27(_a, ckHThiDoTuoi.Checked, ckHTTheoNgay.Checked);
                    rep.Ngaythang.Value = "Từ ngày: " + lupNgaytu.Text.Substring(0, 10) + " đến ngày: " + lupNgayden.Text.Substring(0, 10);
                    rep.NgayBD.Value = "- Bắt đầu sử dụng ngày: " + lupNgaytu.Text.Substring(0, 10);
                    rep.NgayLT.Value = "- Hết sổ nộp lưu trữ ngày: " + lupNgayden.Text.Substring(0, 10);
                    var qkp = (from kp in _Data.KPhongs.Where(p => lMaKP.Contains(p.MaKP)) //.Where(p => _makhoa == 0 || p.MaKP == _makhoa)
                               select new { kp.TenKP }).ToList();
                    if (qkp.Count() > 0)
                    {
                        rep.Khoa.Value = qkp.First().TenKP.ToUpper();
                    }

                    rep.DataSource = _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList();//.OrderBy(p => p.nkb.ToString().Substring(3, 2)).ThenBy(p => p.nkb.ToString().Substring(0, 2)).ThenBy(p => p.tenbn).ToList();

                    rep.BindingData();
                    rep.CreateDocument();

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    int[] _arrWidth = new int[] { };
                    int num = 1;
                    DungChung.Bien.MangHaiChieu = new Object[_benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList().Count + 1, 13];
                    string[] _tieude = { "STT", "Họ tên bệnh nhân", "Tuổi Nam", "Tuổi Nữ", "Địa chỉ", "Số bảo hiểm y tế", "Nghề nghiệp", "Dân tộc", "Triệu chứng", "Chẩn đoán", "Phương pháp điều trị", "Y, BS khám bệnh", "Ghi chú" };
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }

                    foreach (var r in _benhnhan.Distinct().OrderBy(p => p.nkb).ThenBy(p => p.tenbn).ToList())
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.tenbn;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.tuoin;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.tuoinu;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.sothe;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.NgheNgiep1;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DanToc1;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.TrieuChung1;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.ticd;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.cddt;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.tenbs;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.GhiChu;

                        num++;
                    }

                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Sổ khám bệnh", "C:\\SoKhamBenh.xls", true, this.Name);

                    //rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                    #endregion mẫu A1_tt27
                }
            }
        }

        /// <summary>
        /// 0:Từ 1 tuổi trở lên
        /// 1: dưới 12 tháng tuổi
        /// </summary>
        /// <param name="_Data"></param>
        /// <param name="maBNhan"></param>
        /// <returns></returns>
        private int ThangTuoi(QLBV_Database.QLBVEntities _Data, int maBNhan)
        {
            BenhNhan bn = _Data.BenhNhans.Where(p => p.MaBNhan == maBNhan).FirstOrDefault();
            if (bn != null)
            {
                int thang = 0; int ot; int ngay = 1;
                int songaytrongthang = 31;
                if (int.TryParse(bn.ThangSinh, out ot) && Convert.ToInt32(bn.ThangSinh) > 0 && Convert.ToInt32(bn.ThangSinh) < 13)
                    thang = Convert.ToInt32(bn.ThangSinh);
                if (thang > 0)
                {
                    songaytrongthang = (new DateTime(Convert.ToInt32(bn.NamSinh), thang, 1)).AddMonths(1).AddDays(-1).Day;
                    if (int.TryParse(bn.NgaySinh, out ot) && Convert.ToInt32(bn.NgaySinh) > 0 && Convert.ToInt32(bn.NgaySinh) <= songaytrongthang)
                        ngay = Convert.ToInt32(bn.NgaySinh);
                    DateTime ngaysinh = new DateTime(Convert.ToInt32(bn.NamSinh), thang, ngay);
                    if (bn.NNhap.Value < ngaysinh.AddMonths(12))
                        return 1;
                }
            }
            return 0;
        }

        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void radMau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radMau.SelectedIndex == 1)
            {
                chkHT_Thuoc.Enabled = true;
            }
            else
            {
                chkHT_Thuoc.Enabled = false;
            }
        }

        private void cklDTBN_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklDTBN.GetItemCheckState(0) == CheckState.Checked)
                {
                    for (int i = 0; i < cklDTBN.ItemCount; i++)
                    {
                        cklDTBN.SetItemCheckState(i, CheckState.Checked);
                    }
                }
                if (cklDTBN.GetItemCheckState(0) == CheckState.Unchecked)
                {
                    for (int i = 0; i < cklDTBN.ItemCount; i++)
                    {
                        cklDTBN.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
        }

        public class KhoaPhong
        {
            public bool _check;
            public string _maKP;
            public string _kp;

            public string MaKP
            { get { return _maKP; } set { _maKP = value; } }

            public bool Check
            { get { return _check; } set { _check = value; } }

            public string TenKP
            { get { return _kp; } set { _kp = value; } }
        }

        private List<KhoaPhong> _lCSKCB = new List<KhoaPhong>();

        private void radXP_SelectedIndexChanged(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lCSKCB.Clear();
            var kphong = _dataContext.KPhongs.ToList();
            if (radXP.SelectedIndex == 0)
            {
                _lCSKCB.Add(new KhoaPhong { Check = true, MaKP = DungChung.Bien.MaBV, TenKP = DungChung.Bien.TenCQ });
            }
            if (radXP.SelectedIndex == 1)
            {
                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.XaPhuong || (kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham && kp.TrongBV == 0)
                           select new KhoaPhong()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
                _lCSKCB.Insert(0, new KhoaPhong { MaKP = "0", TenKP = "Tất cả", });
            }
            if (radXP.SelectedIndex == 2)
            {
                _lCSKCB = (from kp in kphong
                           where kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PKKhuVuc
                           select new KhoaPhong()
                           {
                               Check = false,
                               MaKP = kp.MaBVsd,
                               TenKP = kp.TenKP
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
            }
            if (radXP.SelectedIndex == 3)
            {
                _lCSKCB = (from kp in _dataContext.BenhViens.Where(p => p.Connect)
                           select new KhoaPhong()
                           {
                               Check = false,
                               MaKP = kp.MaBV,
                               TenKP = kp.TenBV
                           }).Distinct().OrderBy(p => p.TenKP).ToList();
                _lCSKCB.Insert(0, new KhoaPhong { MaKP = "0", TenKP = "Tất cả", });
            }
            cklKP1.DataSource = null;
            cklKP1.DataSource = _lCSKCB;
            cklKP1.CheckAll();
        }

        private void cklKP1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                    cklKP1.CheckAll();
                else
                    cklKP1.UnCheckAll();
            }
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                    cklKP.CheckAll();
                else
                    cklKP.UnCheckAll();
            }
        }
    }
}