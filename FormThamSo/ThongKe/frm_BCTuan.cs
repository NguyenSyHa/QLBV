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
    public partial class frm_BCTuan : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCTuan()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private class bcthang
        {
            public int SoTTct { set; get; }
            private int sTT;

            public int STT
            {
                get { return sTT; }
                set { sTT = value; }
            }

            private string NoiDung;

            public string noiDung
            {
                get { return NoiDung; }
                set { NoiDung = value; }
            }
            public int? SoLuongTong;
            private int? soluottg
            {
                get { return SoLuongTong; }
                set { SoLuongTong = value; }
            }



            private string NoiDung2;

            public string noiDung2
            {
                get { return NoiDung2; }
                set { NoiDung2 = value; }
            }

            private int? SoLuot;

            public int? soLuot
            {
                get { return SoLuot; }
                set { SoLuot = value; }
            }
        }

        private class bctuan
        {
            private string noidung;

            public string Noidung
            {
                get { return noidung; }
                set { noidung = value; }
            }
            private string noidungct;

            public string NoidungCT
            {
                get { return noidungct; }
                set { noidungct = value; }
            }
            private int stt;
            public int STT
            {
                get { return stt; }
                set { stt = value; }
            }

            private int? tong;

            public int? Tong
            {
                get { return tong; }
                set { tong = value; }
            }
            private int? tongct;

            public int? TongCT
            {
                get { return tongct; }
                set { tongct = value; }
            }
            private int? bh;

            public int? Bh
            {
                get { return bh; }
                set { bh = value; }
            }

            private int? tp;

            public int? Tp
            {
                get { return tp; }
                set { tp = value; }
            }

            private int? pk;

            public int? Pk
            {
                get { return pk; }
                set { pk = value; }
            }

            private int? noi;

            public int? Noi
            {
                get { return noi; }
                set { noi = value; }
            }

            private int? ngoai;

            public int? Ngoai
            {
                get { return ngoai; }
                set { ngoai = value; }
            }
        }

        private class canlamsang
        {

            private int idcls;

            public int IDCLS
            {
                get { return idcls; }
                set { idcls = value; }
            }

            private int idcd;

            public int IDCD
            {
                get { return idcd; }
                set { idcd = value; }
            }

            private int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }

            private string tenTN;

            public string TenTN
            {
                get { return tenTN; }
                set { tenTN = value; }
            }

            private int status;

            public int Status
            {
                get { return status; }
                set { status = value; }
            }

            private int ngay;

            public int Ngay
            {
                get { return ngay; }
                set { ngay = value; }
            }

            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
        }

        //private class canlamsang

        private class c_KPhong
        {
            private string TenKP;
            private int MaKP;
            private string PLoai;
            private string ChuyenKhoa;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string ploai
            { set { PLoai = value; } get { return PLoai; } }
            public string chuyenkhoa
            { set { ChuyenKhoa = value; } get { return ChuyenKhoa; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }
        List<c_KPhong> _Kphong = new List<c_KPhong>();
        List<KPhong> _lKPhong = new List<KPhong>();
        List<canlamsang> _cls = new List<canlamsang>();
        List<bctuan> _bctuan = new List<bctuan>();
        List<bcthang> _bcthang = new List<bcthang>();
        private void Frm_BcCongTacKB_NH_Load(object sender, EventArgs e)
        {

            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.Focus();
            _Kphong.Clear();
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP, kp.PLoai, kp.ChuyenKhoa }).ToList();
            rdgdichvu.Enabled = false;
            if (kphong.Count > 0)
            {
                c_KPhong themmoi1 = new c_KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.ploai = "";
                themmoi1.chuyenkhoa = "";
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    c_KPhong themmoi = new c_KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.ploai = a.PLoai;
                    themmoi.chuyenkhoa = a.ChuyenKhoa;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                cklKP.DataSource = _Kphong.ToList();
            }
            cklKP.CheckAll();
            bctheo_SelectedIndexChanged(sender, e);

            var Update = data.BNKBs.Where(p => p.PhuongAn == 3 && p.MaKP == p.MaKPdt && p.NgayNghi != null).ToList();
            foreach (var item in Update)
            {
                item.NgayNghi = null;
                item.PhuongAn = -1;
                item.MaKPdt = 0;
                data.SaveChanges();
            }
        }

        private bool KTtaoBc()
        {
            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            else if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            else return true;
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            _cls.Clear();
            _bctuan.Clear();
            _bcthang.Clear();

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    int makp = Convert.ToInt32(cklKP.GetItemValue(i));
                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp && item.makp != 0)
                        {
                            item.chon = true;
                            break;
                        }
                    }
                }
            }
            var kp11 = _Kphong.Where(p => p.chon == true).ToList();
            if (KTtaoBc())
            {
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                //    if (lupKPhong.EditValue != null)
                //{
                //    khoaphong = Convert.ToInt32(lupKPhong.EditValue);
                //}
                #region select
                var bnz = (from a in data.BenhNhans
                           join b in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on a.MaBNhan equals b.MaBNhan
                           join d in data.RaViens on a.MaBNhan equals d.MaBNhan into k1
                           from k in k1.DefaultIfEmpty()
                           select new
                           {
                               a.MaBNhan,
                               b.IDKB,
                               b.MaICD,
                               b.NgayKham,
                               b.PhuongAn,
                               b.MaKPdt,
                               a.DTuong,
                               //b.MaKP,
                               a.NoiTru,
                               tainan = a.ChuyenKhoa,
                               a.Tuoi,
                               Status = k != null ? k.Status : null,
                               a.DTNT,
                               b.MaICD2,
                               //c.ChuyenKhoa,
                               NgayRa = k != null ? k.NgayRa : null,
                               MaKP = k != null ? k.MaKP : 0,
                               KetQua = k != null ? k.KetQua : null,
                               SongayDT = k != null ? k.SoNgaydt : null
                           }).Distinct().ToList();
                var bn = (from tsbn in bnz
                          join kp in kp11 on tsbn.MaKP equals kp.makp
                          select new
                          {
                              tsbn.MaBNhan,
                              tsbn.IDKB,
                              tsbn.MaICD,
                              tsbn.DTuong,
                              tsbn.MaKPdt,
                              tsbn.MaKP,
                              tsbn.NoiTru,
                              tsbn.Tuoi,
                              tsbn.NgayKham,
                              tsbn.tainan,
                              tsbn.Status,
                              tsbn.DTNT,
                              tsbn.NgayRa,
                              tsbn.KetQua,
                              tsbn.SongayDT,
                              kp.tenkp,
                              kp.ploai,
                              tsbn.MaICD2
                          }).Distinct().ToList();
                var bnvv1 = (from a in data.BenhNhans
                             join b in data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay) on a.MaBNhan equals b.MaBNhan
                             //join c in kp11 on b.MaKP equals c.makp
                             select new
                             {
                                 a.MaBNhan,
                                 a.DTuong,
                                 b.NgayVao,
                                 a.NoiTru,
                                 tainan = a.ChuyenKhoa,
                                 b.MaKP,
                                 a.DTNT
                             }).ToList();
                var bnvv = (from vv in bnvv1
                            join kp in kp11 on vv.MaKP equals kp.makp
                            select new
                            {
                                vv.MaBNhan,
                                vv.DTuong,
                                vv.NgayVao,
                                vv.NoiTru,
                                vv.tainan,
                                vv.MaKP,
                                vv.DTNT,
                                kp.ploai,
                                kp.tenkp,
                                kp.chuyenkhoa
                            }).ToList();
                var bnrv1 = (from a in data.BenhNhans
                             join b in data.VaoViens on a.MaBNhan equals b.MaBNhan
                             join c in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on b.MaBNhan equals c.MaBNhan

                             select new
                             {
                                 a.MaBNhan,
                                 a.DTuong,
                                 b.NgayVao,
                                 c.NgayRa,
                                 c.MaKP,
                                 a.NoiTru,
                                 tainan = a.ChuyenKhoa,
                                 c.KetQua,
                                 c.Status,

                                 a.DTNT,

                             }).ToList();
                var bnrv = (from rv in bnrv1
                            join kp in kp11 on rv.MaKP equals kp.makp
                            select new
                            {
                                rv.MaBNhan,
                                rv.DTuong,
                                rv.NgayVao,
                                rv.NgayRa,
                                rv.MaKP,
                                rv.NoiTru,
                                rv.tainan,
                                rv.KetQua,
                                rv.Status,
                                kp.tenkp,
                                rv.DTNT,
                                kp.ploai,
                                kp.chuyenkhoa
                            }).ToList();
                var bnct1 = (from a in data.BenhNhans
                             join b in data.RaViens on a.MaBNhan equals b.MaBNhan
                             join c in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on a.MaBNhan equals c.MaBNhan
                             select new { a.MaBNhan, c.NgayTT, b.Status, b.MaKP }).ToList();
                var bnct = (from a in bnct1
                            join b in kp11 on a.MaKP equals b.makp
                            select new { a.MaBNhan, a.NgayTT, a.Status }).ToList();
                var bnvr1 = (from a in data.BenhNhans.Where(p => (p.NoiTru == 1) || (p.NoiTru == 0 && p.DTNT == true))
                             join b in data.BNKBs.Where(p => (p.NgayNghi == null || p.NgayNghi >= denngay)) on a.MaBNhan equals b.MaBNhan
                             join c in data.RaViens.Where(p => p.NgayRa >= denngay) on b.MaBNhan equals c.MaBNhan into k
                             from k1 in k.DefaultIfEmpty()
                             select new
                             {
                                 a.MaBNhan,
                                 a.DTuong,
                                 a.Status,
                                 b.NgayKham,
                                 a.DTNT,
                                 NgayRa = k1 != null ? k1.NgayRa : null,
                                 a.NoiTru,
                                 b.MaKP
                             }).ToList();
                //var tets = bnvr1.Where(p => p.MaBNhan == 1460).ToList();
                var bnvr2 = (from vr in bnvr1.Where(p => p.NgayRa == null).Where(p => p.Status != 3 && p.Status != 2)
                             join kp in kp11.Where(p => p.ploai == "Lâm sàng") on vr.MaKP equals kp.makp
                             select new
                             {
                                 vr.Status,
                                 vr.MaBNhan,
                                 vr.DTuong,
                                 vr.DTNT,
                                 vr.NgayRa,
                                 vr.NoiTru,
                                 kp.tenkp,
                                 vr.MaKP,
                                 kp.ploai
                             }).Distinct().ToList();
                var tong1 = (from a in bnvr2
                             select new { a.MaBNhan, a.DTuong }).Distinct().ToList();
                var bnvr = (from vr in bnvr1.Where(p => p.NgayRa != null)
                            join kp in kp11.Where(p => p.ploai == "Lâm sàng") on vr.MaKP equals kp.makp
                            select new
                            {
                                vr.MaBNhan,
                                vr.DTuong,
                                vr.DTNT,
                                vr.NgayRa,
                                vr.NoiTru,
                                kp.tenkp,
                                vr.MaKP,
                                kp.ploai
                            }).Distinct().ToList();
                var tong2 = (from a in bnvr
                             select new { a.MaBNhan, a.DTuong }).Distinct().ToList();

                var TongBN = (from bn1 in data.BenhNhans
                              join vv in data.VaoViens on bn1.MaBNhan equals vv.MaBNhan
                              join rv in data.RaViens on bn1.MaBNhan equals rv.MaBNhan into k
                              from kq in k.DefaultIfEmpty()
                              select new
                              {
                                  MaBNhan = kq != null ? kq.MaBNhan : 0,
                                  bn1.DTuong,
                                  vv.MaKP,
                              }).ToList();
                var tongbn = (from bn1 in TongBN.Where(o => o.MaBNhan == 0)
                              join kp in kp11.Where(p => p.ploai == "Lâm sàng") on bn1.MaKP equals kp.makp
                              select new { bn1.MaBNhan, bn1.DTuong }).ToList();
                //var test = (from a in tong1
                //            join b in tong2 on a.MaBNhan equals b.MaBNhan
                //            select new { a.MaBNhan }).ToList();
                //var tes = bnvr.Where(p => p.tenkp.Contains("Ngoại") && p.NoiTru == 1).ToList();
                #region bỏ
                //var dv = (from a in data.DichVus
                //          join b in data.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                //          select new
                //          {
                //              a.MaDV,
                //              b.TenTN,
                //              a.TenDV
                //          }).ToList();

                //var chidinh = (from a in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.MaKP == khoaphong)
                //               join b in data.ChiDinhs on a.IdCLS equals b.IdCLS
                //               select new
                //               {
                //                   a.IdCLS,
                //                   b.IDCD,
                //                   b.MaDV,
                //                   a.NgayTH,
                //                   b.Status
                //               }).ToList();

                //var cls = (from a in chidinh 
                //               join b in dv on a.MaDV equals b.MaDV
                //               select new {
                //                   a.IdCLS,
                //                   a.IDCD,
                //                   a.MaDV,
                //                   b.TenTN,
                //                   a.NgayTH,
                //                   a.Status,
                //                   b.TenDV
                //               }).ToList();
                //int thu ;
                //for(int i =0; i<(denngay - tungay).Days + 1; i++)
                //{
                //    DateTime ngay = tungay.AddDays(i);
                //    var thu1 = ngay.DayOfWeek;
                //    thu = Convert.ToInt32(thu1);

                //    foreach (var item in cls.Where(p => p.NgayTH >= DungChung.Ham.NgayTu(ngay) && p.NgayTH <= DungChung.Ham.NgayDen(ngay)))
                //    {
                //        canlamsang cls1 = new canlamsang();
                //        cls1.IDCLS = item.IdCLS;
                //        cls1.IDCD = item.IDCD;
                //        cls1.MaDV = item.MaDV??0;
                //        cls1.TenTN = item.TenTN;
                //        cls1.Status = item.Status??0;
                //        cls1.TenDV = item.TenDV;
                //        cls1.Ngay = (thu != 0 && thu !=6 && item.Status == 1)? 1 : ((item.Status == 0 && thu == 0 || thu == 6) ? 2 :3);
                //        _cls.Add(cls1);
                //    }
                //}

                //var clsang = (from a in _cls 
                //                  group a by new {a.TenTN, a.Ngay} into kq
                //                  select new {
                //                  kq.Key.TenTN,
                //                  tongngaythuong = kq.Where(p => p.Status == 1).Where(p => p.Ngay == 1).Count(),
                //                  tongcuoituan = kq.Where(p => p.Status == 0 && (p.TenTN.ToLower().Contains("phẫu thuật") || p.TenTN.ToLower().Contains("thủ thuật"))).Where(p => p.Ngay == 2).Count(),
                //                  kq.Key.Ngay
                //                  }).ToList();

                //var clsang1 = (from a in _cls
                //               group a by new { a.TenDV, a.Ngay } into kq
                //               select new
                //               {
                //                   kq.Key.TenDV,
                //                   kq.Key.Ngay,

                //               }).ToList();
                #endregion
                #endregion
                #region báo cáo tuần
                if (bctheo.SelectedIndex == 0)
                {
                    BaoCao.rep_BCTuan rep = new BaoCao.rep_BCTuan();
                    frmIn frm = new frmIn();
                    var kpchon = kp11.Where(p => p.ploai == "Phòng khám").ToList();
                    if (kpchon.Count() > 0 && kp11.Count < 3)
                    {
                        rep.Khoa.Value = "Khoa khám bệnh".ToUpper();
                    }
                    else
                    {
                        if (kp11.Count > 1)
                        {
                            rep.Khoa.Value = "";
                        }
                        else
                        {
                            rep.Khoa.Value = kp11.First().tenkp.ToUpper();
                        }
                    }
                    rep.Ngay.Value = "Từ " + tungay.Day + " tháng " + tungay.Month + " đến " + denngay.Day + " tháng " + denngay.Month + " năm " + tungay.Year;

                    bctuan moi = new bctuan();
                    moi.STT = 1;
                    moi.Noidung = "Tổng số lần khám";
                    moi.Tong = bn.Where(p => p.tenkp.ToLower() != "khoa điều trị ngoại trú").Count();
                    moi.Bh = bn.Where(p => p.tenkp.ToLower() != "khoa điều trị ngoại trú").Where(p => p.DTuong == "BHYT").Count();
                    moi.Tp = bn.Where(p => p.tenkp.ToLower() != "khoa điều trị ngoại trú").Where(p => p.DTuong == "Dịch vụ").Count();
                    _bctuan.Add(moi);

                    var tsvv = (from b in bnz.Where(p => p.PhuongAn == 1)
                                join kp in data.KPhongs on b.MaKPdt equals kp.MaKP
                                select new { b.MaBNhan, b.PhuongAn, b.MaKP, kp.PLoai, kp.TenKP, b.DTuong, b.NoiTru }).Distinct().ToList();
                    if (kpchon.Count() > 0)
                    {
                        if (DungChung.Bien.MaBV == "20001")
                        {
                            bctuan moi1 = new bctuan();
                            moi1.STT = 2;
                            moi1.Noidung = "Tổng số bệnh nhân vào viện";
                            moi1.Tong = tsvv.Select(p => p.MaBNhan).Count();
                            moi1.Bh = tsvv.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Count();
                            moi1.Tp = tsvv.Where(p => p.DTuong == "Dịch vụ").Count();
                            moi1.Pk = tsvv.Where(p => p.TenKP == "Khoa: Điều Trị Ngoại Trú").Count();
                            moi1.Noi = tsvv.Where(p => p.TenKP == "Khoa: Nội - Nhi").Count();
                            moi1.Ngoai = tsvv.Where(p => p.TenKP == "Khoa: Phụ - Ngoại").Count();
                            _bctuan.Add(moi1);
                        }
                        else
                        {
                            bctuan moi1 = new bctuan();
                            moi1.STT = 2;
                            moi1.Noidung = "Tổng số bệnh nhân vào viện";
                            moi1.Tong = tsvv.Select(p => p.MaBNhan).Count();
                            moi1.Bh = tsvv.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Count();
                            moi1.Tp = tsvv.Where(p => p.DTuong == "Dịch vụ").Count();
                            moi1.Pk = tsvv.Where(p => p.TenKP.ToLower() == "khoa điều trị ngoại trú").Count();
                            moi1.Noi = tsvv.Where(p => p.TenKP.Contains("Khoa Nội")).Count();
                            moi1.Ngoai = tsvv.Where(p => p.TenKP.Contains("Khoa Phụ Ngoại")).Count();
                            _bctuan.Add(moi1);
                        }
                        
                    }
                    else
                    {
                        bctuan moi1 = new bctuan();
                        moi1.STT = 2;
                        moi1.Noidung = "Tổng số bệnh nhân vào viện";
                        moi1.Tong = bnvv.Select(p => p.MaBNhan).Distinct().Count();
                        moi1.Bh = bnvv.Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count();
                        moi1.Tp = bnvv.Where(p => p.DTuong == "Dịch vụ").Distinct().Count();
                        moi1.Pk = bnvv.Where(p => p.NoiTru == 0).Where(p => p.tenkp.ToLower() == "khoa điều trị ngoại trú").Distinct().Count();
                        moi1.Noi = bnvv.Where(p => p.tenkp.Contains("Khoa Nội")).Distinct().Count();
                        moi1.Ngoai = bnvv.Where(p => p.tenkp.Contains("Khoa Phụ Ngoại")).Distinct().Count();
                        _bctuan.Add(moi1);
                    }
                    int tongnoi = 0, tongng = 0;
                    tongnoi = bnvr.Where(p => p.tenkp.Contains("Khoa Nội")).Count() + bnvr2.Where(p => p.tenkp.Contains("Khoa Nội")).Count();
                    tongng = bnvr.Where(p => p.tenkp.Contains("Khoa Phụ Ngoại")).Count() + bnvr2.Where(p => p.tenkp.Contains("Khoa Phụ Ngoại")).Count();
                    bctuan moi2 = new bctuan();
                    moi2.STT = 3;
                    moi2.Noidung = "Tổng số bệnh nhân đang điều trị";
                    moi2.Tong = tongbn.Count() + tong2.Count();
                    moi2.Bh = tongbn.Where(p => p.DTuong == "BHYT").Count() + tong2.Where(p => p.DTuong == "BHYT").Count();
                    moi2.Tp = tongbn.Where(p => p.DTuong == "Dịch vụ").Count() + tong2.Where(p => p.DTuong == "Dịch vụ").Count();
                    moi2.Pk = (tongbn.Count() + tong2.Count()) - (tongnoi + tongng);
                    //bnvr.Where(p => p.tenkp.ToLower() == "khoa điều trị ngoại trú").Count() + bnvr2.Where(p => p.tenkp.ToLower() == "khoa điều trị ngoại trú").Count();
                    moi2.Noi = bnvr.Where(p => p.tenkp.Contains("Khoa Nội")).Count() + bnvr2.Where(p => p.tenkp.Contains("Khoa Nội")).Count();
                    moi2.Ngoai = bnvr.Where(p => p.tenkp.Contains("Khoa Phụ Ngoại")).Count() + bnvr2.Where(p => p.tenkp.Contains("Khoa Phụ Ngoại")).Count();
                    _bctuan.Add(moi2);

                    tongnoi = bnrv.Where(p => p.tenkp.Contains("Khoa Nội")).Where(p => p.ploai == "Lâm sàng").Count();
                    tongng = bnrv.Where(p => p.tenkp.Contains("Khoa Phụ Ngoại")).Where(p => p.ploai == "Lâm sàng").Count();
                    bctuan moi21 = new bctuan();
                    moi21.STT = 4;
                    moi21.Noidung = "Tổng số bệnh nhân ra viện";
                    moi21.Tong = bnrv.Count();
                    moi21.Bh = bnrv.Where(p => p.DTuong == "BHYT").Count();
                    moi21.Tp = bnrv.Where(p => p.DTuong == "Dịch vụ").Count();
                    moi21.Pk = bnrv.Where(p => p.tenkp.ToLower() == "khoa điều trị ngoại trú").Count();
                    moi21.Noi = bnrv.Where(p => p.tenkp.Contains("Khoa Nội")).Where(p => p.ploai == "Lâm sàng").Count();
                    moi21.Ngoai = bnrv.Where(p => p.tenkp.Contains("Khoa Phụ Ngoại")).Where(p => p.ploai == "Lâm sàng").Count();
                    _bctuan.Add(moi21);

                    var _tstainan = (from bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay)
                                     join bn1 in data.BenhNhans.Where(p => p.ChuyenKhoa != null && p.ChuyenKhoa != "" && p.ChuyenKhoa != "0") on bnkb.MaBNhan equals bn1.MaBNhan
                                     select new { bnkb.MaBNhan, bn1.NoiTru, bn1.DTuong, bn1.DTNT, bn1.ChuyenKhoa }).Distinct().ToList();

                    bctuan moi3 = new bctuan();
                    moi3.STT = 5;
                    moi3.Noidung = "Tổng số ca tai nạn";
                    //moi3.Tong = bnrv.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaKP == khoaphong).Count();
                    //moi3.Bh = bnrv.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaKP == khoaphong).Where(p => p.DTuong == "BHYT").Count();
                    //moi3.Tp = bnrv.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaKP == khoaphong).Where(p => p.DTuong == "Dịch vụ").Count();
                    //moi3.Pk = bnrv.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaKP == khoaphong).Where(p => p.NoiTru == 0 && p.DTNT == true && p.PLoai == "Phòng khám").Count();
                    //moi3.Noi = bnrv.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaKP == khoaphong).Where(p => p.NoiTru == 0 && p.DTNT == true && p.PLoai != "Phòng khám" && p.ChuyenKhoa == "Nội").Count();
                    //moi3.Ngoai = bnrv.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.MaKP == khoaphong).Where(p => p.NoiTru == 0 && p.DTNT == true && p.PLoai != "Phòng khám" && p.ChuyenKhoa == "Ngoại").Count();
                    moi3.Tong = _tstainan.Count();
                    moi3.Bh = _tstainan.Where(p => p.DTuong == "BHYT").Count();
                    moi3.Tp = _tstainan.Where(p => p.DTuong == "Dịch vụ").Count();
                    _bctuan.Add(moi3);

                    bctuan moi4 = new bctuan();
                    moi4.Noidung = "Tổng số bệnh nhân tử vong";
                    moi4.STT = 6;
                    moi4.Tong = bnrv.Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    moi4.Bh = bnrv.Where(p => p.DTuong == "BHYT").Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    moi4.Tp = bnrv.Where(p => p.DTuong == "Dịch vụ").Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    moi4.Pk = bnrv.Where(p => p.tenkp == "khoa điều trị ngoại trú").Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    moi4.Noi = bnrv.Where(p => p.tenkp.Contains("Khoa Nội")).Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    moi4.Ngoai = bnrv.Where(p => p.tenkp.Contains("Khoa Phụ Ngoại")).Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    _bctuan.Add(moi4);

                    bctuan moi5 = new bctuan();
                    moi5.STT = 7;
                    moi5.Noidung = "Tổng số bệnh nhân chuyển viện";
                    moi5.Tong = bnrv.Where(p => p.Status == 1).Count();
                    moi5.Bh = bnrv.Where(p => p.DTuong == "BHYT").Where(p => p.Status == 1).Count();
                    moi5.Tp = bnrv.Where(p => p.DTuong == "Dịch vụ").Where(p => p.Status == 1).Count();
                    moi5.Pk = bnrv.Where(p => p.tenkp == "khoa điều trị ngoại trú").Where(p => p.Status == 1).Count();
                    moi5.Noi = bnrv.Where(p => p.tenkp.Contains("Khoa Nội")).Where(p => p.Status == 1).Count();
                    moi5.Ngoai = bnrv.Where(p => p.tenkp.Contains("Khoa Phụ Ngoại")).Where(p => p.Status == 1).Count();
                    _bctuan.Add(moi5);

                    var _ldvkt = (from d in data.DichVus.Where(p => p.PLoai == 2)
                                  join tn in data.TieuNhomDVs.Where(p => p.IDNhom == 1 || p.IDNhom == 2) on d.IdTieuNhom equals tn.IdTieuNhom
                                  select new { d.MaDV, tn.IdTieuNhom, tn.TenRG }).ToList();

                    var _lcls = (from c in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).Where(p => p.Status == 1)
                                 join b in data.BenhNhans on c.MaBNhan equals b.MaBNhan
                                 join cd in data.ChiDinhs on c.IdCLS equals cd.IdCLS
                                 select new { c.MaBNhan, c.NgayTH, b.NoiTru, b.DTNT, b.DTuong, cd.MaDV, c.MaKP, c.IdCLS }).ToList();

                    var _lkqua = (from kp in kp11
                                  join c in _lcls on kp.makp equals c.MaKP
                                  join d in _ldvkt on c.MaDV equals d.MaDV
                                  group new { c, d, kp } by new { d.TenRG } into kq
                                  select new
                                  {
                                      Noidung = kq.Key.TenRG,
                                      Tong = kq.Select(p => p.c.IdCLS).Distinct().Count(),
                                      Bh = kq.Where(p => p.c.DTuong == "BHYT").Select(p => p.c.IdCLS).Distinct().Count(),
                                      Tp = kq.Where(p => p.c.DTuong == "Dịch vụ").Select(p => p.c.IdCLS).Distinct().Count(),
                                      Pk = kq.Where(p => p.kp.tenkp.ToLower() == "khoa điều trị ngoại trú" || p.kp.ploai == "Phòng khám").Select(p => p.c.IdCLS).Distinct().Count(),
                                      Noi = kq.Where(p => p.kp.tenkp.Contains("Khoa Nội")).Select(p => p.c.IdCLS).Distinct().Count(),
                                      Ngoai = kq.Where(p => p.kp.tenkp.Contains("Khoa Phụ Ngoại")).Select(p => p.c.IdCLS).Distinct().Count()
                                  }).ToList();
                    int i = 8;
                    //_bctuan.AddRange(_lkqua);
                    foreach (var item in _lkqua)
                    {
                        bctuan moi8 = new bctuan();
                        moi8.STT = i++;
                        moi8.Noidung = item.Noidung;
                        moi8.Tong = item.Tong;
                        moi8.Bh = item.Bh;
                        moi8.Tp = item.Tp;
                        moi8.Pk = item.Pk;
                        moi8.Noi = item.Noi;
                        moi8.Ngoai = item.Ngoai;
                        _bctuan.Add(moi8);
                    }

                    DateTime newtungay7 = tungay.AddDays(1);
                    DateTime newdenngaycn = tungay.AddDays(3);


                    var _ldvt7 = (from c in data.CLS.Where(p => p.NgayThang >= newtungay7 && p.NgayThang <= newdenngaycn)
                                  join cd in data.ChiDinhs on c.IdCLS equals cd.IdCLS
                                  join r in data.RaViens on c.MaBNhan equals r.MaBNhan into k1
                                  from k in k1.DefaultIfEmpty()
                                  join d in data.DichVus.Where(p => p.MaDV != 123)
                                  on cd.MaDV equals d.MaDV
                                  select new
                                  {
                                      Mabn = k != null ? null : c.MaBNhan,
                                      d.TenDV,
                                      c.MaKP,
                                      cd.IDCD,
                                      cd.MaDV
                                  }).ToList();
                    var slgtong = (from d in _ldvt7.Where(p => p.Mabn != null)
                                   join k in kp11 on d.MaKP equals k.makp
                                   group d by d into kq
                                   select new { tong = kq.Select(p => p.IDCD).Distinct().Count() }).ToList();
                    var _lclst7 = (from d in _ldvt7.Where(p => p.Mabn != null)
                                   join k in kp11 on d.MaKP equals k.makp
                                   group d by new { d.TenDV, d.MaDV } into kq
                                   select new bctuan
                                   {
                                       STT = 20,
                                       Tong = slgtong.Count(),
                                       Noidung = "Tổng số dịch vụ kỹ thuật làm TB - CN",
                                       NoidungCT = kq.Key.TenDV,
                                       TongCT = kq.Select(p => p.IDCD).Distinct().Count()
                                   }).OrderBy(p => p.Noidung).ToList();
                    //bctuan moi7 = new bctuan();
                    //moi7.Noidung = "Tổng số dịch vụ kỹ thuật làm TB - CN";
                    //moi7.Tong = _lclst7.Sum(p => p.Tong);
                    //_bctuan.Add(moi7);
                    _bctuan.AddRange(_lclst7);

                    var _kqua = (from a in _bctuan
                                 group a by new { a.STT, a.Noidung, a.Tong, a.Bh, a.Tp, a.Pk, a.Noi, a.Ngoai, a.NoidungCT, a.TongCT } into kq
                                 select new
                                 {
                                     kq.Key.STT,
                                     kq.Key.Noidung,
                                     Tong = kq.Key.Tong == 0 ? null : kq.Key.Tong,
                                     Bh = kq.Key.Bh == 0 ? null : kq.Key.Bh,
                                     Tp = kq.Key.Tp == 0 ? null : kq.Key.Tp,
                                     Noi = kq.Key.Noi == 0 ? null : kq.Key.Noi,
                                     Ngoai = kq.Key.Ngoai == 0 ? null : kq.Key.Ngoai,
                                     Pk = kq.Key.Pk == 0 ? null : kq.Key.Pk,
                                     kq.Key.NoidungCT,
                                     TongCT = kq.Key.TongCT == 0 ? null : kq.Key.TongCT
                                 }).OrderBy(p => p.STT).ToList();
                    rep.DataSource = _kqua;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion

                #region báo cáo tháng
                if (bctheo.SelectedIndex == 1)
                {


                    #region tổng số khám
                    //bcthang moi = new bcthang();
                    //moi.STT = 1;
                    //moi.noiDung = "Tổng số lần khám";
                    //moi.noiDung2 = "";
                    //moi.SoLuongTong = bn.Count();
                    //_bcthang.Add(moi);

                    bcthang moi1 = new bcthang();
                    moi1.STT = 1;
                    moi1.noiDung = "Tổng số lần khám";
                    moi1.noiDung2 = "  Khám đối tượng BHYT";
                    moi1.soLuot = bn.Where(p => p.DTuong == "BHYT").Count();
                    moi1.SoLuongTong = bn.Count();
                    _bcthang.Add(moi1);

                    bcthang moi2 = new bcthang();
                    moi2.STT = 1;
                    moi2.noiDung = "Tổng số lần khám";
                    moi2.noiDung2 = "  Số bệnh nhân thu phí";
                    moi2.soLuot = bn.Where(p => p.DTuong == "Dịch vụ").Count();
                    moi2.SoLuongTong = bn.Count();
                    _bcthang.Add(moi2);

                    bcthang moi3 = new bcthang();
                    moi3.STT = 1;
                    moi3.noiDung = "Tổng số lần khám";
                    moi3.noiDung2 = "  Số bệnh nhân nghèo miến phí";
                    moi3.SoLuongTong = bn.Count();
                    //moi3.soLuot = bn.Count();
                    _bcthang.Add(moi3);

                    bcthang moi4 = new bcthang();
                    moi4.STT = 1;
                    moi4.noiDung = "Tổng số lần khám";
                    moi4.noiDung2 = "  Khám cho trẻ em dưới 6 tuổi BH";
                    moi4.soLuot = bn.Where(p => p.DTuong == "BHYT" && p.Tuoi <= 6).Count();
                    moi4.SoLuongTong = bn.Count();
                    _bcthang.Add(moi4);

                    bcthang moi5 = new bcthang();
                    moi5.STT = 1;
                    moi5.noiDung = "Tổng số lần khám";
                    moi5.noiDung2 = "  Khám cho trẻ em dưới 6 tuổi TP";
                    moi5.SoLuongTong = bn.Count();
                    moi5.soLuot = bn.Where(p => p.DTuong == "Dịch vụ" && p.Tuoi <= 6).Count();
                    _bcthang.Add(moi5);

                    bcthang moi6 = new bcthang();
                    moi6.STT = 1;
                    moi6.noiDung = "Tổng số lần khám";
                    moi6.noiDung2 = "  Khám cho người cao tuổi BH";
                    moi6.SoLuongTong = bn.Count();
                    moi6.soLuot = bn.Where(p => p.DTuong == "BHYT" && p.Tuoi >= 60).Count();
                    _bcthang.Add(moi6);

                    bcthang moi7 = new bcthang();
                    moi7.STT = 1;
                    moi7.noiDung = "Tổng số lần khám";
                    moi7.noiDung2 = "  Khám cho người cao tuổi TP";
                    moi7.SoLuongTong = bn.Count();
                    moi7.soLuot = bn.Where(p => p.DTuong == "Dịch vụ" && p.Tuoi >= 60).Count();
                    _bcthang.Add(moi7);

                    //bcthang moi8 = new bcthang();
                    //moi8.STT = 1;
                    //moi8.noiDung = "Tổng số lần khám";
                    //moi8.noiDung2 = "  Khám cho người cao tuổi TP";
                    //moi8.soLuot = bn.Where(p => p.DTuong == "Dịch vụ" && p.Tuoi >= 60).Count();
                    //_bcthang.Add(moi8);

                    bcthang moi9 = new bcthang();
                    moi9.STT = 1;
                    moi9.noiDung = "Tổng số lần khám";
                    moi9.noiDung2 = "  Số bệnh nhân chuyển BV tuyến";
                    moi9.SoLuongTong = bn.Count();
                    moi9.soLuot = bnct.Where(p => p.Status == 1).Count();
                    _bcthang.Add(moi9);

                    bcthang moi10 = new bcthang();
                    moi10.STT = 1;
                    moi10.noiDung = "Tổng số lần khám";
                    moi10.noiDung2 = "  Số bệnh nhân chuyển BV chuyên";
                    moi10.SoLuongTong = bn.Count();
                    //moi10.soLuot = bn.Where(p => p.Status == 1).Count();
                    _bcthang.Add(moi10);

                    //bcthang moi11 = new bcthang();
                    //moi11.STT = 1;
                    //moi11.noiDung = "Tổng số lần khám";
                    //moi11.noiDung2 = "  Số bệnh nhân chuyển BV tuyến";
                    //moi11.soLuot = bn.Where(p => p.Status == 1 && p.DTuong == "BHYT").Count();
                    //_bcthang.Add(moi11);
                    if (DungChung.Bien.MaBV == "20001")
                    {
                        bcthang moi101 = new bcthang();
                        moi101.STT = 1;
                        moi101.noiDung = "Tổng số lần khám";
                        moi101.noiDung2 = "  Bệnh nhân tăng huyết áp";
                        //moi101.SoLuongTong = bn.Count();
                        moi101.soLuot = bn.Where(p => p.MaICD.Contains("I10") || p.MaICD.Contains("I15.0") || p.MaICD.Contains("I15.1") || p.MaICD.Contains("I15.2") || p.MaICD.Contains("I15.8") || p.MaICD.Contains("I15.9") || p.MaICD2.Contains("I10") || p.MaICD2.Contains("I15.0") || p.MaICD2.Contains("I15.1") || p.MaICD2.Contains("I15.2") || p.MaICD2.Contains("I15.8") || p.MaICD2.Contains("I15.9")).Select(p => p.MaBNhan).Distinct().Count();
                        _bcthang.Add(moi101);

                        bcthang moi102 = new bcthang();
                        moi102.STT = 1;
                        moi102.noiDung = "Tổng số lần khám";
                        moi102.noiDung2 = "  Bệnh nhân tiểu đường";
                        //moi102.SoLuongTong = bn1.Count();
                        moi102.soLuot = bn.Where(p => p.MaICD.Contains("E10") || p.MaICD.Contains("E11") || p.MaICD2.Contains("E10") || p.MaICD2.Contains("E11")).Select(p => p.MaBNhan).Distinct().Count();
                        _bcthang.Add(moi102);
                    }
                    #endregion

                    #region BNRV trong tháng
                    //var bn1 = (from a in bnz.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => (p.NoiTru == 0 && p.DTNT == true) || (p.NoiTru == 1)).Where(p => p.maKP != null)
                    //           join kp in kp11 on a.maKP equals kp.makp
                    //           select a).ToList();
                    var _lbnrv = (from r in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay)
                                  join b in data.BenhNhans.Where(p => (p.NoiTru == 0 && p.DTNT == true) || (p.NoiTru == 1)) on r.MaBNhan equals b.MaBNhan
                                  select new { r.MaBNhan, r.MaKP, r.KetQua, b.DTNT, b.NoiTru, b.DTuong, b.Tuoi, SongayDT = r.SoNgaydt, r.Status, MaICD = r.MaICD }).Distinct().ToList();
                    var bn1 = (from b in _lbnrv
                               join k in kp11 on b.MaKP equals k.makp
                               select new { b.MaBNhan, b.KetQua, b.DTNT, b.NoiTru, b.DTuong, b.Tuoi, b.SongayDT, b.Status, b.MaICD }).Distinct().ToList();

                    //bcthang moi12 = new bcthang();
                    //moi12.STT = 2;

                    //moi12.noiDung2 = "";
                    //moi12.SoLuongTong = bn1.Count();
                    //_bcthang.Add(moi12);

                    #region BNRV ngoại trú
                    bcthang moi13 = new bcthang();
                    moi13.STT = 2;
                    moi13.SoTTct = 1;
                    moi13.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi13.SoLuongTong = bn1.Count();
                    moi13.noiDung2 = "Ngoại trú";
                    moi13.soLuot = bn1.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();

                    _bcthang.Add(moi13);

                    var bn2 = bn1.Where(p => p.NoiTru == 0 && p.DTNT == true).ToList();

                    bcthang moi14 = new bcthang();
                    moi14.STT = 2;
                    moi14.SoTTct = 1;
                    moi14.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi14.noiDung2 = "  Số bệnh nhân có thẻ BHYT";
                    //moi14.soLuot = bn2.Count();
                    moi14.SoLuongTong = bn1.Count();
                    moi14.soLuot = bn1.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.DTuong == "BHYT").Count();
                    _bcthang.Add(moi14);

                    bcthang moi15 = new bcthang();
                    moi15.STT = 2;
                    moi15.SoTTct = 1;
                    moi15.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi15.noiDung2 = "  Số bệnh nhân thu phí";
                    moi15.SoLuongTong = bn1.Count();
                    moi15.soLuot = bn2.Where(p => p.DTuong == "Dịch vụ").Count();
                    _bcthang.Add(moi15);

                    bcthang moi16 = new bcthang();
                    moi16.STT = 2;
                    moi16.SoTTct = 1;
                    moi16.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi16.noiDung2 = "  Trẻ em dưới 6 tuổi BHYT";
                    moi16.SoLuongTong = bn1.Count();
                    moi16.soLuot = bn2.Where(p => p.DTuong == "BHYT" && p.Tuoi <= 6).Count();
                    _bcthang.Add(moi16);

                    bcthang moi17 = new bcthang();
                    moi17.STT = 2;
                    moi17.SoTTct = 1;
                    moi17.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi17.noiDung2 = "  Trẻ em dưới 6 tuổi TP";
                    moi17.SoLuongTong = bn1.Count();
                    moi17.soLuot = bn2.Where(p => p.DTuong == "Dịch vụ" && p.Tuoi <= 6).Count();
                    _bcthang.Add(moi17);

                    bcthang moi18 = new bcthang();
                    moi18.STT = 2;
                    moi18.SoTTct = 1;
                    moi18.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi18.noiDung2 = "  SỐ bệnh nhân nghèo được điều trị miễn phí";
                    moi18.SoLuongTong = bn1.Count();
                    //moi18.soLuot = bn2.Where(p => p.DTuong == "Dịch vụ" && p.Tuoi < 6).Count();
                    _bcthang.Add(moi18);

                    bcthang moi19 = new bcthang();
                    moi19.STT = 2;
                    moi19.SoTTct = 1;
                    moi19.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi19.noiDung2 = "  Tổng số Bn cao tuổi BH";
                    moi19.SoLuongTong = bn1.Count();
                    moi19.soLuot = bn2.Where(p => p.DTuong == "BHYT" && p.Tuoi >= 60).Count();
                    _bcthang.Add(moi19);

                    bcthang moi20 = new bcthang();
                    moi20.STT = 2;
                    moi20.SoTTct = 1;
                    moi20.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi20.noiDung2 = "  Tổng số Bn cao tuổi TP";
                    moi20.SoLuongTong = bn1.Count();
                    moi20.soLuot = bn2.Where(p => p.DTuong == "Dịch vụ" && p.Tuoi >= 60).Count();
                    _bcthang.Add(moi20);

                    bcthang moi21 = new bcthang();
                    moi21.STT = 2;
                    moi21.SoTTct = 1;
                    moi21.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi21.noiDung2 = "  Số Bn tử vong";
                    moi21.SoLuongTong = bn1.Count();
                    moi21.soLuot = bn2.Where(p => p.KetQua != null && p.KetQua.ToLower() == "tử vong").Count();
                    _bcthang.Add(moi21);

                    bcthang moi22 = new bcthang();
                    moi22.STT = 2;
                    moi22.SoTTct = 1;
                    moi22.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi22.noiDung2 = "  Số bệnh nhân chuyển BV tuyến trên";
                    moi22.soLuot = bn2.Where(p => p.Status == 1).Count();
                    _bcthang.Add(moi22);

                    bcthang moi23 = new bcthang();
                    moi23.STT = 2;
                    moi23.SoTTct = 1;
                    moi23.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi23.noiDung2 = "  Số bệnh nhân chuyển BV chuyên";
                    moi23.SoLuongTong = bn1.Count();
                    //moi23.soLuot = bn2.Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    _bcthang.Add(moi23);

                    bcthang moi24 = new bcthang();
                    moi24.STT = 2;
                    moi24.SoTTct = 1;
                    moi24.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi24.noiDung2 = "  Số bệnh nhân chuyển BV lý do khác";
                    moi24.SoLuongTong = bn1.Count();
                    //moi24.soLuot = bn2.Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    _bcthang.Add(moi24);

                    bcthang moi25 = new bcthang();
                    moi25.STT = 2;
                    moi25.SoTTct = 1;
                    moi25.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi25.SoLuongTong = bn1.Count();
                    moi25.noiDung2 = "  Số ngày điều trị người cao tuổi";
                    moi25.soLuot = bn2.Where(p => p.Tuoi >= 60).Sum(p => p.SongayDT ?? 0);
                    _bcthang.Add(moi25);
                    if (DungChung.Bien.MaBV == "20001")
                    {
                        bcthang moi251 = new bcthang();
                        moi251.STT = 2;
                        moi251.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                        moi251.noiDung2 = "  Bệnh nhân tăng huyết áp";
                        moi251.SoLuongTong = bn1.Count();
                        moi251.soLuot = bn2.Where(p => p.MaICD.Contains("I10") || p.MaICD.Contains("I15.0") || p.MaICD.Contains("I15.1") || p.MaICD.Contains("I15.2") || p.MaICD.Contains("I15.8") || p.MaICD.Contains("I15.9")).Count();
                        _bcthang.Add(moi251);

                        bcthang moi252 = new bcthang();
                        moi252.STT = 2;
                        moi252.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                        moi252.noiDung2 = "  Bệnh nhân tiểu đường";
                        moi252.SoLuongTong = bn1.Count();
                        moi252.soLuot = bn2.Where(p => p.MaICD.Contains("E10") || p.MaICD.Contains("E11")).Count();
                        _bcthang.Add(moi252);
                    }
                    #endregion

                    #region BNRV nội trú
                    bcthang moi26 = new bcthang();
                    moi26.STT = 2;
                    moi26.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi26.noiDung2 = "Nội trú";
                    moi26.SoLuongTong = bn1.Count();
                    moi26.soLuot = bn1.Where(p => p.NoiTru == 1).Count();
                    _bcthang.Add(moi26);

                    var bn21 = bn1.Where(p => p.NoiTru == 1).ToList();

                    bcthang moi27 = new bcthang();
                    moi27.STT = 2;

                    moi27.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi27.noiDung2 = "  Số bệnh nhân có thẻ BHYT";
                    moi27.SoLuongTong = bn1.Count();
                    moi27.soLuot = bn21.Where(p => p.DTuong == "BHYT").Count();
                    _bcthang.Add(moi27);

                    bcthang moi28 = new bcthang();
                    moi28.STT = 2;
                    moi28.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi28.noiDung2 = "  Số bệnh nhân thu phí";
                    moi28.SoLuongTong = bn1.Count();
                    moi28.soLuot = bn21.Where(p => p.DTuong == "Dịch vụ").Count();
                    _bcthang.Add(moi28);

                    bcthang moi29 = new bcthang();
                    moi29.STT = 2;
                    moi29.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi29.noiDung2 = "  Trẻ em dưới 6 tuổi BHYT";
                    moi29.SoLuongTong = bn1.Count();
                    moi29.soLuot = bn21.Where(p => p.Tuoi <= 6).Count();
                    _bcthang.Add(moi29);

                    bcthang moi30 = new bcthang();
                    moi30.STT = 2;
                    moi30.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi30.SoLuongTong = bn1.Count();
                    moi30.noiDung2 = "  SỐ bệnh nhân nghèo được điều trị miễn phí";
                    //moi18.soLuot = bn21.Where(p => p.DTuong == "Dịch vụ" && p.Tuoi < 6).Count();
                    _bcthang.Add(moi30);

                    bcthang moi31 = new bcthang();
                    moi31.STT = 2;
                    moi31.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi31.noiDung2 = "  Tổng số Bn cao tuổi BH";
                    moi31.SoLuongTong = bn1.Count();
                    moi31.soLuot = bn21.Where(p => p.DTuong == "BHYT" && p.Tuoi >= 60).Count();
                    _bcthang.Add(moi31);

                    bcthang moi32 = new bcthang();
                    moi32.STT = 2;
                    moi32.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi32.noiDung2 = "  Tổng số Bn cao tuổi TP";
                    moi32.SoLuongTong = bn1.Count();
                    moi22.soLuot = bn21.Where(p => p.DTuong == "Dịch vụ" && p.Tuoi >= 60).Count();
                    _bcthang.Add(moi32);

                    bcthang moi33 = new bcthang();
                    moi33.STT = 2;
                    moi33.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi33.noiDung2 = "  Số lượt điều trị khỏi";
                    moi33.SoLuongTong = bn1.Count();
                    moi33.soLuot = bn21.Where(p => p.KetQua.ToLower().Contains("khỏi")).Count();
                    _bcthang.Add(moi33);

                    bcthang moi34 = new bcthang();
                    moi34.STT = 2;
                    moi34.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi34.noiDung2 = "  Số lượt điều trị đỡ";
                    moi34.SoLuongTong = bn1.Count();
                    moi34.soLuot = bn21.Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                    _bcthang.Add(moi34);

                    bcthang moi35 = new bcthang();
                    moi35.STT = 2;
                    moi35.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi35.noiDung2 = "  Số lượt điều trị  không thay đổi";
                    moi35.SoLuongTong = bn1.Count();
                    moi35.soLuot = bn21.Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                    _bcthang.Add(moi35);

                    bcthang moi36 = new bcthang();
                    moi36.STT = 2;
                    moi36.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi36.noiDung2 = "  Số lượt điều trị nặng hơn";
                    moi36.SoLuongTong = bn1.Count();
                    moi36.soLuot = bn21.Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                    _bcthang.Add(moi36);

                    bcthang moi37 = new bcthang();
                    moi37.STT = 2;
                    moi37.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi37.noiDung2 = "  Số Bn tử vong";
                    moi37.SoLuongTong = bn1.Count();
                    moi37.soLuot = bn21.Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    _bcthang.Add(moi37);

                    bcthang moi38 = new bcthang();
                    moi38.STT = 2;
                    moi38.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi38.noiDung2 = "  Số bệnh nhân chuyển BV tuyến trên";
                    moi38.SoLuongTong = bn1.Count();
                    moi38.soLuot = bn21.Where(p => p.Status == 1).Count();
                    _bcthang.Add(moi38);

                    bcthang moi39 = new bcthang();
                    moi39.STT = 2;
                    moi39.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi39.noiDung2 = "  Số bệnh nhân chuyển BV chuyên";
                    moi39.SoLuongTong = bn1.Count();
                    //moi23.soLuot = bn21.Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    _bcthang.Add(moi39);

                    bcthang moi40 = new bcthang();
                    moi40.STT = 2;
                    moi40.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi40.noiDung2 = "  Số bệnh nhân chuyển BV lý do khác";
                    moi40.SoLuongTong = bn1.Count();
                    //moi40.soLuot = bn21.Where(p => p.KetQua.ToLower().Contains("tử vong")).Count();
                    _bcthang.Add(moi40);

                    bcthang moi41 = new bcthang();
                    moi41.STT = 2;
                    moi41.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi41.noiDung2 = "  Số ngày điều trị người cao tuổi";
                    moi41.SoLuongTong = bn1.Count();
                    moi41.soLuot = bn21.Where(p => p.Tuoi >= 60).Sum(p => p.SongayDT ?? 0);
                    _bcthang.Add(moi41);

                    bcthang moi42 = new bcthang();
                    moi42.STT = 2;
                    moi42.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi42.noiDung2 = "  Bệnh nhân tăng huyết áp ";
                    moi42.SoLuongTong = bn1.Count();
                    moi42.soLuot = bn21.Where(p => p.MaICD.Contains("I10") || p.MaICD.Contains("I15.0") || p.MaICD.Contains("I15.1") || p.MaICD.Contains("I15.2") || p.MaICD.Contains("I15.8") || p.MaICD.Contains("I15.9")).Count();
                    _bcthang.Add(moi42);

                    bcthang moi43 = new bcthang();
                    moi43.STT = 2;
                    moi43.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi43.noiDung2 = "  Bệnh nhân tiểu đường ";
                    moi43.SoLuongTong = bn1.Count();
                    moi43.soLuot = bn21.Where(p => p.MaICD.Contains("E10") || p.MaICD.Contains("E11")).Count();
                    _bcthang.Add(moi43);

                    bcthang moi44 = new bcthang();
                    moi44.STT = 2;
                    moi44.noiDung = "Tổng số bệnh nhân ra viện trong tháng";
                    moi44.noiDung2 = "  Tổng số ngày điều trị";
                    moi44.SoLuongTong = bn1.Count();
                    moi44.soLuot = bn21.Sum(p => p.SongayDT ?? 0);
                    _bcthang.Add(moi44);
                    #endregion
                    #endregion

                    #region Phẫu thuật
                    //bcthang moi45 = new bcthang();
                    //moi45.STT = 3;
                    //moi45.noiDung = "Phẫu thuật";
                    ////moi45.noiDung2 = "";
                    //moi45.soLuot = null; //cls.Where(p => p.Status == 1 && p.TenTN == "Phẫu Thuật").Count();
                    //_bcthang.Add(moi45);
                    #endregion
                    var _ldvtt = (from dv1 in data.DichVus.Where(p => p.PLoai == 2)
                                  join tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat) on dv1.IdTieuNhom equals tn.IdTieuNhom
                                  select new { dv1.TenDV, dv1.MaDV, tn.IdTieuNhom, tn.TenRG }).ToList();

                    if (rdgdichvu.SelectedIndex == 0)
                    {
                        var _ldv1 = (from dt in data.DThuocs.Where(p => p.PLDV == 2)
                                     join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                     select new { dtct.IDDonct, dtct.MaDV, dtct.SoLuong, dt.MaKP }).ToList();
                        var kqs = (from dvs in _ldv1
                                   join dvs1 in _ldvtt on dvs.MaDV equals dvs1.MaDV
                                   join kp in kp11 on dvs.MaKP equals kp.makp
                                   group new { dvs, dvs1, kp } by new { dvs.MaDV, dvs1.TenDV, dvs1.TenRG, kp.makp } into kq
                                   select new
                                   {
                                       kq.Key.MaDV,
                                       kq.Key.TenDV,
                                       kq.Key.TenRG,
                                       kq.Key.makp,
                                       soluong = kq.Sum(p => p.dvs.SoLuong)
                                   }).OrderBy(p => p.TenRG).ThenBy(p => p.TenDV).ToList();
                        //bcthang moi46 = new bcthang();
                        //moi46.STT = 4;
                        //moi46.noiDung = "Tổng số các kỹ thuật y tế";
                        //moi46.noiDung2 = "";

                        //_bcthang.Add(moi46);

                        int slg = 0;
                        foreach (var item in kqs)
                        {
                            if (item.TenRG.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat.ToLower())
                            {
                                slg = Convert.ToInt32(item.soluong);
                                bcthang moi45 = new bcthang();
                                moi45.STT = 3;
                                moi45.noiDung = "Phẫu thuật";
                                moi45.noiDung2 = "  " + item.TenDV;
                                moi45.SoLuongTong = Convert.ToInt32(kqs.Where(p => p.TenRG.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat.ToLower()).Sum(p => p.soluong));
                                moi45.soLuot = slg;
                                _bcthang.Add(moi45);
                            }
                            else
                            {
                                slg = Convert.ToInt32(item.soluong);
                                bcthang moi47 = new bcthang();
                                moi47.STT = 4;
                                moi47.noiDung = "Tổng số các kỹ thuật y tế";
                                moi47.noiDung2 = "  " + item.TenDV;
                                moi47.SoLuongTong = Convert.ToInt32(kqs.Where(p => p.TenRG.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat.ToLower()).Sum(p => p.soluong));
                                moi47.soLuot = slg;
                                _bcthang.Add(moi47);
                            }

                        }
                        //var _lkqz = _bcthang.OrderBy(p => p.STT).ToList();
                        var _lkqz = (from bc in _bcthang
                                     group bc by new { bc.STT, bc.noiDung, bc.noiDung2, bc.SoLuongTong, bc.SoTTct } into kq
                                     select new
                                     {
                                         kq.Key.STT,
                                         kq.Key.noiDung,
                                         kq.Key.noiDung2,
                                         SoLuongTong = kq.Key.SoLuongTong == 0 ? null : kq.Key.SoLuongTong,
                                         soLuot = kq.Sum(p => p.soLuot) == 0 ? null : kq.Sum(p => p.soLuot)
                                     }).OrderBy(p => p.STT).ToList();
                        frmIn frm1 = new frmIn();
                        BaoCao.rep_BCThang rep1 = new BaoCao.rep_BCThang();
                        if (tieudeBCThang.Text == "")
                            rep1.tieude.Value = "THÁNG " + tungay.Month + "/" + tungay.Year;
                        else
                            rep1.tieude.Value = tieudeBCThang.Text;
                        rep1.ngay.Value = "Từ " + tungay.Day + "/" + tungay.Month + " đến " + denngay.Day + "/" + denngay.Month + " năm " + tungay.Year;
                        int _makp1 = DungChung.Bien.MaKP;
                        var kpchon = kp11.Where(p => p.ploai == "Phòng khám").ToList();
                        if (kpchon.Count() > 0 && kp11.Count() < 3)
                        {
                            rep1.txtkhoa.Text = "Khoa khám bệnh".ToUpper();
                        }
                        else
                        {
                            if (kp11.Count > 1)
                            {
                                rep1.txtkhoa.Text = "";
                            }
                            else
                            {
                                rep1.txtkhoa.Text = kp11.First().tenkp.ToUpper();
                            }
                        }
                        rep1.txtTenBV.Text = DungChung.Bien.TenCQ;
                        rep1.DataSource = _lkqz;
                        rep1.BindingData();
                        rep1.CreateDocument();
                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                        frm1.ShowDialog();
                    }
                    if (rdgdichvu.SelectedIndex == 1)
                    {
                        var _ldv1 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                                     join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                     select new
                                     {
                                         vpct.idVPhict,
                                         vpct.SoLuong,
                                         vpct.MaDV,
                                         vpct.MaKP
                                     }).ToList();
                        var _lkqs = (from vp in _ldv1
                                     join dv1 in _ldvtt on vp.MaDV equals dv1.MaDV
                                     join kp in kp11 on vp.MaKP equals kp.makp
                                     group new { vp, dv1, kp } by new { vp.MaDV, dv1.TenDV, dv1.TenRG } into kq
                                     select new
                                     {
                                         kq.Key.MaDV,
                                         kq.Key.TenDV,
                                         kq.Key.TenRG,
                                         //kq.Key.makp,
                                         soluong = kq.Sum(p => p.vp.SoLuong)
                                     }).OrderBy(p => p.TenDV).ToList();
                        //bcthang moi46 = new bcthang();
                        //moi46.STT = 4;
                        //moi46.noiDung = "Tổng số các kỹ thuật y tế";
                        //moi46.noiDung2 = "";

                        //_bcthang.Add(moi46);
                        foreach (var item in _lkqs)
                        {
                            if (item.TenRG.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat.ToLower())
                            {
                                int slg = Convert.ToInt32(item.soluong);
                                bcthang moi45 = new bcthang();
                                moi45.STT = 3;
                                moi45.noiDung = "Phẫu thuật";
                                moi45.noiDung2 = "  " + item.TenDV;
                                moi45.SoLuongTong = Convert.ToInt32(_lkqs.Where(p => p.TenRG.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat.ToLower()).Sum(p => p.soluong));
                                moi45.soLuot = slg;
                                _bcthang.Add(moi45);
                            }
                            else
                            {
                                int slg = Convert.ToInt32(item.soluong);
                                bcthang moi47 = new bcthang();
                                moi47.STT = 4;
                                moi47.noiDung = "Tổng số các kỹ thuật y tế";
                                moi47.noiDung2 = "  " + item.TenDV;
                                moi47.SoLuongTong = Convert.ToInt32(_lkqs.Where(p => p.TenRG.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat.ToLower()).Sum(p => p.soluong));
                                moi47.soLuot = slg;
                                _bcthang.Add(moi47);
                            }

                        }
                        var _lkqz = (from bc in _bcthang
                                     group bc by new { bc.STT, bc.noiDung, bc.noiDung2, bc.SoLuongTong, bc.SoTTct } into kq
                                     select new
                                     {
                                         kq.Key.STT,
                                         kq.Key.noiDung,
                                         kq.Key.noiDung2,
                                         SoLuongTong = kq.Key.SoLuongTong == 0 ? null : kq.Key.SoLuongTong,
                                         soLuot = kq.Sum(p => p.soLuot) == 0 ? null : kq.Sum(p => p.soLuot)
                                     }).ToList().OrderBy(p => p.STT);
                        frmIn frm1 = new frmIn();
                        BaoCao.rep_BCThang rep1 = new BaoCao.rep_BCThang();
                        var kpchon = kp11.Where(p => p.ploai == "Phòng khám").ToList();
                        if (kpchon.Count() > 0 && kp11.Count() < 3)
                        {
                            rep1.txtkhoa.Text = "Khoa khám bệnh".ToUpper();
                        }
                        else
                        {
                            if (kp11.Count > 1)
                            {
                                rep1.txtkhoa.Text = "";
                            }
                            else
                            {
                                rep1.txtkhoa.Text = kp11.First().tenkp.ToUpper();
                            }
                        }
                        if (tieudeBCThang.Text == "")
                            rep1.tieude.Value = "THÁNG " + tungay.Month + "/" + tungay.Year;
                        else
                            rep1.tieude.Value = tieudeBCThang.Text;
                        rep1.ngay.Value = "Từ " + tungay.Day + "/" + tungay.Month + " đến " + denngay.Day + "/" + denngay.Month + " năm " + tungay.Year;

                        rep1.DataSource = _lkqz;
                        rep1.BindingData();
                        rep1.CreateDocument();
                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                        frm1.ShowDialog();
                    }
                    if (rdgdichvu.SelectedIndex == 2)
                    {
                        var _ldv1 = (from dt in data.CLS.Where(p => p.NgayThang >= tungay && p.NgayThang <= denngay)
                                     join dtct in data.ChiDinhs.Where(p => p.Status == 0) on dt.IdCLS equals dtct.IdCLS
                                     select new { dtct.IDCD, dtct.MaDV, SoLuong = 1, dt.MaKP }).ToList();
                        var kqs = (from dvs in _ldv1
                                   join dvs1 in _ldvtt on dvs.MaDV equals dvs1.MaDV
                                   join kp in kp11 on dvs.MaKP equals kp.makp
                                   group new { dvs, dvs1, kp } by new { dvs.MaDV, dvs1.TenDV, dvs1.TenRG, kp.makp } into kq
                                   select new
                                   {
                                       kq.Key.MaDV,
                                       kq.Key.TenDV,
                                       kq.Key.TenRG,
                                       kq.Key.makp,
                                       soluong = kq.Sum(p => p.dvs.SoLuong)
                                   }).OrderBy(p => p.TenRG).ThenBy(p => p.TenDV).ToList();
                        //bcthang moi46 = new bcthang();
                        //moi46.STT = 4;
                        //moi46.noiDung = "Tổng số các kỹ thuật y tế";
                        //moi46.noiDung2 = "";

                        //_bcthang.Add(moi46);

                        int slg = 0;
                        foreach (var item in kqs)
                        {
                            if (item.TenRG.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat.ToLower())
                            {
                                slg = Convert.ToInt32(item.soluong);
                                bcthang moi45 = new bcthang();
                                moi45.STT = 3;
                                moi45.noiDung = "Phẫu thuật";
                                moi45.noiDung2 = "  " + item.TenDV;
                                moi45.SoLuongTong = Convert.ToInt32(kqs.Where(p => p.TenRG.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat.ToLower()).Sum(p => p.soluong));
                                moi45.soLuot = slg;
                                _bcthang.Add(moi45);
                            }
                            else
                            {
                                slg = Convert.ToInt32(item.soluong);
                                bcthang moi47 = new bcthang();
                                moi47.STT = 4;
                                moi47.noiDung = "Tổng số các kỹ thuật y tế";
                                moi47.noiDung2 = "  " + item.TenDV;
                                moi47.SoLuongTong = Convert.ToInt32(kqs.Where(p => p.TenRG.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat.ToLower()).Sum(p => p.soluong));
                                moi47.soLuot = slg;
                                _bcthang.Add(moi47);
                            }

                        }
                        //var _lkqz = _bcthang.OrderBy(p => p.STT).ToList();
                        var _lkqz = (from bc in _bcthang
                                     group bc by new { bc.STT, bc.noiDung, bc.noiDung2, bc.SoLuongTong, bc.SoTTct } into kq
                                     select new
                                     {
                                         kq.Key.STT,
                                         kq.Key.noiDung,
                                         kq.Key.noiDung2,
                                         SoLuongTong = kq.Key.SoLuongTong == 0 ? null : kq.Key.SoLuongTong,
                                         soLuot = kq.Sum(p => p.soLuot) == 0 ? null : kq.Sum(p => p.soLuot)
                                     }).OrderBy(p => p.STT).ToList();
                        frmIn frm1 = new frmIn();
                        BaoCao.rep_BCThang rep1 = new BaoCao.rep_BCThang();
                        if (tieudeBCThang.Text == "")
                            rep1.tieude.Value = "THÁNG " + tungay.Month + "/" + tungay.Year;
                        else
                            rep1.tieude.Value = tieudeBCThang.Text;
                        rep1.ngay.Value = "Từ " + tungay.Day + "/" + tungay.Month + " đến " + denngay.Day + "/" + denngay.Month + " năm " + tungay.Year;
                        int _makp1 = DungChung.Bien.MaKP;
                        var kpchon = kp11.Where(p => p.ploai == "Phòng khám").ToList();
                        if (kpchon.Count() > 0 && kp11.Count() < 3)
                        {
                            rep1.txtkhoa.Text = "Khoa khám bệnh".ToUpper();
                        }
                        else
                        {
                            if (kp11.Count > 1)
                            {
                                rep1.txtkhoa.Text = "";
                            }
                            else
                            {
                                rep1.txtkhoa.Text = kp11.First().tenkp.ToUpper();
                            }
                        }
                        rep1.txtTenBV.Text = DungChung.Bien.TenCQ;
                        rep1.DataSource = _lkqz;
                        rep1.BindingData();
                        rep1.CreateDocument();
                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                        frm1.ShowDialog();
                    }
                    //#region Tổng số các kỹ thuật y tế
                    //bcthang moi46 = new bcthang();
                    //moi46.STT = 4;
                    //moi46.noiDung = "Tổng số các kỹ thuật y tế";
                    //moi46.noiDung2 = "";
                    //moi46.soLuot = cls.Where(p => p.Status == 1).Count();
                    //_bcthang.Add(moi46);

                    //var cls1 = (from a in cls.Where(p => p.Status == 1)
                    //            group a by new { 
                    //                a.MaDV,
                    //                a.TenDV
                    //            } into kq
                    //            select new
                    //            {
                    //                kq.Key.TenDV,
                    //                soluot = kq.Count()
                    //            }).ToList();

                    //foreach(var item in cls1)
                    //{
                    //    bcthang moi47 = new bcthang();
                    //    moi47.STT = 4;
                    //    moi47.noiDung = "Tổng số các kỹ thuật y tế";
                    //    moi47.noiDung2 = "  " + item.TenDV;
                    //    moi47.soLuot = item.soluot;
                    //    _bcthang.Add(moi47);
                    //}
                    //#endregion


                }
                #endregion
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (Convert.ToInt32(cklKP.SelectedValue) == 0)
            {
                if (cklKP.GetItemChecked(cklKP.SelectedIndex))
                {
                    cklKP.CheckAll();

                }
                else
                {
                    cklKP.UnCheckAll();
                }
            }

            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                int makp = Convert.ToInt32(cklKP.GetItemValue(i));
                if (cklKP.GetItemChecked(i))
                {

                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp && item.makp != 0)
                        {
                            item.chon = true;
                            //break;
                        }
                    }
                }
                else
                {
                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp || item.makp == 0)
                        {
                            item.chon = false;
                            // break;
                        }
                    }
                }
            }
        }

        private void bctheo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bctheo.SelectedIndex == 1)
            {
                rdgdichvu.Enabled = true;
                lupTuNgay.DateTime = lupDenNgay.DateTime.AddMonths(-1);

            }
            else
            {
                lupTuNgay.DateTime = lupDenNgay.DateTime.AddDays(-7);
                rdgdichvu.Enabled = false;
            }
        }

    }
}