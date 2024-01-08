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
    public partial class frm_BC_TTHDChuyenMon : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_TTHDChuyenMon()
        {
            InitializeComponent();
        }
        public class Content
        {
            public string NoiDung { get; set; }
            public double? KetQua { get; set; }
            public double? KetQuaNamNay { get; set; }
            public double? KetQuaNamNgoai { get; set; }
            public double? KetQuaSoSanh { get; set; }
        }
        class cravien
        {
            public int maBN { set; get; }
            public int SoNgayDT { set; get; }
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<Content> _listContent = new List<Content>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _listContent.Clear();
            List<BenhVien> _lbenhvien = data.BenhViens.ToList();
            List<KPhong> _lkp = data.KPhongs.ToList();
            List<int> lMabnvaovien = data.VaoViens.Where(p => p.MaBNhan != null).Select(p => p.MaBNhan).ToList();
            List<BenhNhan> _lbenhnhan = data.BenhNhans.ToList();
            List<BNKB> _lbnkb = data.BNKBs.ToList();
            Content moi = new Content();
            DateTime tungayHT = new DateTime();
            DateTime denngayHT = new DateTime();
            tungayHT =  tungay.DateTime;
            denngayHT = denngay.DateTime;

            string namnay = "0";
            string namngoai = "0";
            if (comboBoxEdit1.Text != "")
            {
                namnay = comboBoxEdit1.Text;
                namngoai = Convert.ToString(Convert.ToInt32(comboBoxEdit1.Text) - 1);
            }
            DateTime tungaynamngoai = new DateTime();
            DateTime denngaynamngoai = new DateTime();
            DateTime tungaynamnay = new DateTime();
            DateTime denngaynamnay = new DateTime();
            tungaynamngoai = Convert.ToDateTime("1/1/" + namngoai + " 00:00:00");
            denngaynamngoai = Convert.ToDateTime("31/12/" + namngoai + " 23:59:59");
            tungaynamnay = Convert.ToDateTime("1/1/" + namnay + " 00:00:00");
            denngaynamnay = Convert.ToDateTime("31/12/" + namnay + " 23:59:59");

            #region query
            #region BV Thường Tín
            var bn = (from a in _lbnkb.Where(p => p.NgayKham >= tungayHT && p.NgayKham <= denngayHT)
                      join b in _lbenhnhan on a.MaBNhan equals b.MaBNhan
                      select new {a,b}).ToList();
            var bnn = (from a in _lbnkb.Where(p => p.NgayKham <= denngayHT)
                       join b in _lbenhnhan on a.MaBNhan equals b.MaBNhan
                      select new { a, b }).ToList();
            
            var bn1 = (from a in bnn group new { a.b, a.a } by new { a.b } into kq select new { kq.Key.b, IDKB = kq.Max(p => p.a.IDKB) }).ToList();
            var bnrvv1 = (from a in bn1
                         join b in _data.VaoViens on a.b.MaBNhan equals b.MaBNhan
                         join c in _data.RaViens on b.MaBNhan equals c.MaBNhan into k
                         from k1 in k.DefaultIfEmpty()
                          join d in _lbnkb on a.IDKB equals d.IDKB
                         select new {
                            a.b.MaBNhan,
                            d.MaKP,
                            b.NgayVao,
                            NgayRa = k1 != null ? k1.NgayRa : null,
                            giotv = k1 != null ? (k1.NgayRa - b.NgayVao) : null,
                            a.b.DTuong,
                            a.b.ChuyenKhoa,
                            a.b.Tuoi,
                            KetQua = k1 != null ? k1.KetQua : "",
                            MaBVC = k1 != null ? k1.MaBVC : "",
                            SoNGDT = k1 != null ? k1.SoNgaydt : 0,
                            a.b.NoiTru
                         }).ToList();
            var bnrvv = (from a in bnrvv1
                         join b in _lbenhvien on a.MaBVC equals b.MaBV into k
                         from k1 in k.DefaultIfEmpty()
                         select new {
                             a.MaBNhan,
                             a.MaKP,
                             a.NgayVao,
                             a.NgayRa,
                             a.giotv,
                             a.DTuong,
                             a.ChuyenKhoa,
                             a.Tuoi,
                             a.KetQua,
                             a.MaBVC ,
                             a.SoNGDT,
                             Tuyen = k1 != null ? k1.TuyenBV : "",
                             a.NoiTru
                         }).ToList();
            var pt = (from a in _data.CLS
                      join b in _data.ChiDinhs.Where(p => p.Status == 1) on a.IdCLS equals b.IdCLS
                      //join c in _data.CLScts on b.IDCD equals c.IDCD
                      join d in _data.DichVus on b.MaDV equals d.MaDV
                      join h in _data.TieuNhomDVs on d.IdTieuNhom equals h.IdTieuNhom
                      join f in _data.NhomDVs on d.IDNhom equals f.IDNhom
                      select new { a.MaBNhan, d.MaDV, d.PLoai, d.Loai, b.Status, b.NgayTH, h.TenRG, h.TenTN, d.TenDV }).ToList();
            #endregion
            #region BV Nam Thăng Long
            var qTong = (from bn2 in _lbenhnhan
                         join bnkb in _lbnkb.Where(p => p.NgayKham >= tungaynamngoai && p.NgayKham <= denngaynamnay) on bn2.MaBNhan equals bnkb.MaBNhan
                         select new { bn2, bnkb.NgayKham, bnkb.MaKP }
                            ).ToList();

            var benhnhan = (from bn2 in qTong
                            where (bn2.NgayKham >= tungaynamnay && bn2.NgayKham <= denngaynamnay)
                            join kp in _lkp on bn2.MaKP equals kp.MaKP
                            where ( kp.PLoai == "Phòng khám")
                            select bn2.bn2).ToList();
            var benhNhanNamNgoai = (from bn2 in qTong
                                    where (bn2.NgayKham >= tungaynamngoai && bn2.NgayKham <= denngaynamngoai)
                                    join kp in _lkp on bn2.MaKP equals kp.MaKP
                                    where ( kp.PLoai == "Phòng khám")
                                    select bn2.bn2).ToList();
            var qtongRV = (from bn2 in _lbenhnhan
                           join rv in data.RaViens.Where(p => p.NgayRa >= tungaynamngoai && p.NgayRa <= denngaynamnay) on bn2.MaBNhan equals rv.MaBNhan
                           select new { bn2, rv }).ToList();
            var benhnhanRV = (from bn2 in qtongRV
                              join ttbx in _data.TTboXungs on bn2.bn2.MaBNhan equals ttbx.MaBNhan into k
                              from k1 in k.DefaultIfEmpty()
                              where (bn2.rv.NgayRa >= tungaynamnay && bn2.rv.NgayRa <= denngaynamnay)
                              select new { bn2.bn2, bn2.rv, Ngoai = k1 != null ? k1.NgoaiKieu : "" }).ToList();
            var benhnhanRVnamNgoai = (from bn2 in qtongRV
                                      join ttbx in _data.TTboXungs on bn2.bn2.MaBNhan equals ttbx.MaBNhan into k
                                      from k1 in k.DefaultIfEmpty()
                                      where (bn2.rv.NgayRa >= tungaynamngoai && bn2.rv.NgayRa <= denngaynamngoai)
                                      select new { bn2.bn2, bn2.rv, Ngoai = k1 != null ? k1.NgoaiKieu : "" }).ToList();
            var bnCoThe = benhnhan.Where(p => p.SThe != null && p.SThe != "").ToList();
            var bnCoTheNoiTru = benhnhanRV.Where(p => p.bn2.SThe != null && p.bn2.SThe != "").Where(p => p.bn2.NoiTru == 1).ToList();
            var bnThanhToanTT = benhnhan.Where(p => p.SThe == null || p.SThe == "").ToList();
            var bnThanhToanTTNoiTru = benhnhanRV.Where(p => p.bn2.SThe == null || p.bn2.SThe == "").ToList();
            List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
            List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da1 = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
            List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da2 = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
            List<cravien> _lclRavien = (from a in data.RaViens select new cravien { maBN = a.MaBNhan, SoNgayDT = a.SoNgaydt ?? 0 }).ToList();
            List<KPhong> _lKhoaP = new List<KPhong>();
            _lKhoaP = data.KPhongs.ToList();
            foreach (var item in data.KPhongs.ToList())
            {
                _da = frm_NhapBuongGiuongKeKoach.getBuongGiuong(data, item.MaKP, namnay);
                foreach (var item1 in _da)
                {
                    _da1.Add(item1);
                }
            }
            foreach (var item in data.KPhongs.ToList())
            {
                _da = frm_NhapBuongGiuongKeKoach.getBuongGiuong(data, item.MaKP, namngoai);
                foreach (var item1 in _da)
                {
                    _da2.Add(item1);
                }
            }
            var n2 = (from a in _da2 group a by new { a.giuongKH, a.makp } into kq select new { kq.Key.makp, kq.Key.giuongKH }).ToList();
            var n1 = (from a in _da1 group a by new { a.giuongKH, a.makp } into kq select new { kq.Key.makp, kq.Key.giuongKH }).ToList();
            var q7_1 = (from a in benhnhanRV
                        join bv in _lbenhvien on a.rv.MaBVC equals bv.MaBV
                        join ttbx in _data.TTboXungs on a.bn2.MaBNhan equals ttbx.MaBNhan into k
                        from k1 in k.DefaultIfEmpty()
                        select new
                        {
                            a.rv.Status,
                            a.rv.MaBVC,
                            bv.TuyenBV,
                            a.bn2.NoiTru,
                            HangBV = (bv.TuyenBV.Contains("A")) ? bv.HangBV = 1 : (bv.TuyenBV.Contains("B")) ? bv.HangBV = 2 : (bv.TuyenBV.Contains("C")) ? bv.HangBV = 3 : (bv.TuyenBV.Contains("D")) ? bv.HangBV = 4 : null,
                            NuocNgoai = k1 != null ? k1.NgoaiKieu : ""
                        }).Where(p => p.Status == 1).ToList();
            var q7_2 = (from b in benhnhanRVnamNgoai
                        join bv in _lbenhvien on b.rv.MaBVC equals bv.MaBV
                        join ttbx in _data.TTboXungs on b.bn2.MaBNhan equals ttbx.MaBNhan into k
                        from k1 in k.DefaultIfEmpty()
                        select new
                        {
                            b.rv.Status,
                            b.rv.MaBVC,
                            bv.TuyenBV,
                            b.bn2.NoiTru,
                            HangBV = (bv.TuyenBV.Contains("A")) ? bv.HangBV = 1 : (bv.TuyenBV.Contains("B")) ? bv.HangBV = 2 : (bv.TuyenBV.Contains("C")) ? bv.HangBV = 3 : (bv.TuyenBV.Contains("D")) ? bv.HangBV = 4 : null,
                            NuocNgoai = k1 != null ? k1.NgoaiKieu : ""
                        }).Where(p => p.Status == 1).ToList();
            var q8_1 = (from a in benhnhan.Where(p => p.NoiTru == 0 && p.DTNT == true)
                        join vv in lMabnvaovien on a.MaBNhan equals vv
                        join rv in _lclRavien on a.MaBNhan equals rv.maBN
                        group new { a, rv } by new { a.MaBNhan, rv.SoNgayDT } into kq
                        select new { kq.Key.MaBNhan, SoNgaydt = kq.Key.SoNgayDT }).ToList();

            var q8_2 = (from a in benhNhanNamNgoai.Where(p => p.NoiTru == 0 && p.DTNT == true)
                        join vv in lMabnvaovien on a.MaBNhan equals vv
                        join rv in _lclRavien on a.MaBNhan equals rv.maBN
                        group new { a, rv } by new { a.MaBNhan, rv.SoNgayDT } into kq
                        select new { kq.Key.MaBNhan, SoNgaydt = kq.Key.SoNgayDT }).ToList();
            var qtongvienphi = (from vp in data.VienPhis
                                join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                                select new { vp.MaBNhan, vpct.Mien }).ToList();
            var q10_a = (from n in benhnhanRV
                         join vp in qtongvienphi on n.bn2.MaBNhan equals vp.MaBNhan
                         select new
                         {
                             n.bn2.MaBNhan,
                             n.bn2.NoiTru,
                             n.rv.KetQua,
                             n.rv.Status,
                             vp.Mien,
                             n.bn2.DTNT,
                             SThe = n.bn2.SThe == null ? "" : n.bn2.SThe,
                             n.bn2.MaDTuong,
                             n.bn2.DTuong
                         }).Where(p => p.NoiTru == 1 && p.DTNT == false).ToList();
            var q10_b = (from n in benhnhanRVnamNgoai
                         join vp in qtongvienphi on n.bn2.MaBNhan equals vp.MaBNhan
                         select new
                         {
                             n.bn2.MaBNhan,
                             n.bn2.NoiTru,
                             n.rv.KetQua,
                             n.rv.Status,
                             vp.Mien,
                             n.bn2.DTNT,
                             SThe = n.bn2.SThe == null ? "" : n.bn2.SThe,
                             n.bn2.MaDTuong,
                             n.bn2.DTuong
                         }).Where(p => p.NoiTru == 1 && p.DTNT == false).ToList();
            var kpdongy = (from kp in data.KPhongs join ck in data.ChuyenKhoas.Where(p => p.TenCK == "Đông y") on kp.MaCK equals ck.MaCK select kp).ToList();
            var q11 = (from bn2 in benhnhanRV.Where(p => p.bn2.NoiTru == 1)
                       join bnkb in qTong on bn2.bn2.MaBNhan equals bnkb.bn2.MaBNhan
                       join kp in kpdongy on bnkb.MaKP equals kp.MaKP
                       select bn2).Distinct().ToList();
            var q11_1 = (from bn2 in benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1) join bnkb in qTong on bn2.bn2.MaBNhan equals bnkb.bn2.MaBNhan join kp in kpdongy on bnkb.MaKP equals kp.MaKP select bn).Distinct().ToList();
            #endregion
            #region Hạng BV
            var hangBV = _lbenhvien.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            int tuyenBV = -1;
            if (hangBV.TuyenBV.Contains("A"))
                tuyenBV = 1;
            if (hangBV.TuyenBV.Contains("B"))
                tuyenBV = 2;
            if (hangBV.TuyenBV.Contains("C"))
                tuyenBV = 3;
            if (hangBV.TuyenBV.Contains("D"))
                tuyenBV = 4;
            #endregion
            #endregion

            #region 1. Tổng số giường bệnh kế hoạch
            moi = new Content();
            moi.NoiDung = "1. Tổng số giường bệnh kế hoạch";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = n1.Sum(p => Convert.ToInt32(p.giuongKH));
            moi.KetQuaNamNgoai = n2.Sum(p => Convert.ToInt32(p.giuongKH));
            moi.KetQuaSoSanh =n2.Sum(p => Convert.ToInt32(p.giuongKH)) > 0 ? n1.Sum(p => Convert.ToInt32(p.giuongKH)) * 100 / n2.Sum(p => Convert.ToInt32(p.giuongKH)) : 0;
            _listContent.Add(moi);
            #endregion

            #region 2. Tổng số giường thực kê
            moi = new Content();
            moi.NoiDung = "2. Tổng số giường thực kê";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = _da1.Count();
            moi.KetQuaNamNgoai = _da2.Count();
            moi.KetQuaSoSanh = _da2.Count() > 0 ? _da1.Count() * 100 / _da2.Count() : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "2a. Trong đó: Tổng số giường Tự nguyện/ Theo yêu cầu/ Xã hội hóa/ Hoặc do các tổ chức tặng";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 3. Công suất sử dụng giường bệnh
            moi = new Content();
            moi.NoiDung = "3. Công suất sử dụng giường bệnh";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "3a. Tính theo số giường thực kê";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = (_da1.Count() * 365 != 0) ? (double)(benhnhanRV.Where(p => p.bn2.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (_da1.Count() * 365)) : 0.0;
            moi.KetQuaNamNgoai = (_da2.Count() * 365 != 0) ? (double)(benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (_da2.Count() * 365)) : 0.0;
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay *100 /moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "3b. Tính theo số giường kế hoạch";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * 365 != 0) ? (double)(benhnhanRV.Where(p => p.bn2.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * 365)) : 0.0;
            moi.KetQuaNamNgoai = (n2.Sum(p => Convert.ToInt32(p.giuongKH)) * 365 != 0) ? (double)(benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (n2.Sum(p => Convert.ToInt32(p.giuongKH)) * 365)) : 0.0;
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "Tổng số thẻ BHYT đăng ký khám chữa bệnh ban đầu tại BV";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "Tổng số bàn khám";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 4. Tổng số lượt khám bệnh (tất cả cá đối tượng = 4a + 4b + 4c + 4d + 4đ )
            moi = new Content();
            moi.NoiDung = "4. Tổng số lượt khám bệnh (tất cả cá đối tượng = 4a + 4b + 4c + 4d )";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = benhnhan.Where(p => p.DTuong != "KSK").Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "4a. Tổng số lượt khám bệnh thu phí trực tiếp";
            moi.KetQua = bn.Where(p => p.b.DTuong == "Dịch vụ").Count();
            moi.KetQuaNamNay = benhnhan.Where(p => p.DTuong != "KSK").Where(p => p.SThe == "").Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Where(p => p.SThe == "").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "4b. Tổng số lượt khám bệnh cho người được BHYT chi trả ( tất cả các đối tượng có thẻ BHYT)";
            moi.KetQua = bn.Where(p => p.b.DTuong == "BHYT").Count();
            moi.KetQuaNamNay = benhnhan.Where(p => p.DTuong != "KSK").Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "CN").Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Where(p => p.SThe != "" && p.MaDTuong != "HN" && p.MaDTuong != "CN").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "4c. Tổng số lượt khám cho người nghèo (không sử dụng thẻ BHYT nhưng vẫn đk quyết toán theo thực thanh thực chi)";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = benhnhan.Where(p => p.DTuong != "KSK").Where(p => p.MaDTuong == "HN").Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Where(p => p.MaDTuong == "HN").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "4d. Tổng số lượt khám miễn viễn phí cho các đối tượng (cận nghèo, khó khắn, ....) do BV quyết định";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = benhnhan.Where(p => p.DTuong != "KSK").Where(p => p.MaDTuong == "CN").Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p => p.DTuong != "KSK").Where(p => p.MaDTuong == "CN").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "4đ. Tổng số lượt khám giảm viễn phí do BV quyết định";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "Tổng số khám sức khỏe định kỳ";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 5.Tổng số khám bệnh trẻ dưới 6 tuổi (các đối tượng):
            moi = new Content();
            moi.NoiDung = "5.Tổng số khám bệnh trẻ dưới 6 tuổi (các đối tượng):";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = benhnhan.Where(p => p.Tuoi < 6 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")).Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p => p.Tuoi < 6 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "Số trẻ dưới 6 tuổi có thẻ BHYT, hoặc thẻ khám, chữa bệnh cho trẻ em dưới 6 tuổi:";
            moi.KetQua = bn.Where(p => p.b.Tuoi < 6 && p.b.DTuong == "BHYT").GroupBy(p => p.b.MaBNhan).Count();
            moi.KetQuaNamNay = benhnhan.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p => p.DTuong == "BHYT").Where(p => p.Tuoi < 6).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "Trong đó số trẻ dưới 6 tuổi thu phí trực tiếp";
            moi.KetQua = bn.Where(p => p.b.Tuoi < 6 && p.b.DTuong == "Dịch vụ").GroupBy(p => p.b.MaBNhan).Count();
            moi.KetQuaNamNay = benhnhan.Where(p => p.DTuong == "Dịch vụ").Where(p =>  p.Tuoi < 6).Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p => p.DTuong == "Dịch vụ").Where(p =>  p.Tuoi < 6).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 6. Tổng số khám cho người benehn cao tuổi >= 60 tuổi (tất cả các đối tượng):
            moi = new Content();
            moi.NoiDung = "6. Tổng số khám cho người bệnh cao tuổi >= 60 tuổi (tất cả các đối tượng):";
            moi.KetQua = bn.Where(p => p.b.Tuoi >= 60 && (p.b.DTuong == "BHYT" || p.b.DTuong == "Dịch vụ")).GroupBy(p => p.b.MaBNhan).Count();
            moi.KetQuaNamNay = benhnhan.Where(p => p.Tuoi >= 60 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")).Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p =>  p.Tuoi >= 60 && (p.DTuong == "BHYT" || p.DTuong == "Dịch vụ")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "6a. Số lượt khám bệnh cho người cao tuổi có thẻ BHYT, hoặc đối tượng chính sách khắc được miễn viện phí";
            moi.KetQua = bn.Where(p => p.b.Tuoi >= 60 && p.b.DTuong == "BHYT").GroupBy(p => p.b.MaBNhan).Count();
            moi.KetQuaNamNay = benhnhan.Where(p => p.SThe != "").Where(p =>  p.Tuoi >= 60 && p.DTuong == "BHYT").Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p => p.SThe != "").Where(p =>  p.Tuoi >= 60 && p.DTuong == "BHYT").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "Trong đó Số lượt khám bệnh cho người cao tuổi thu phí trực tiếp";
            moi.KetQua = bn.Where(p => p.b.Tuoi >= 60 && p.b.DTuong == "Dịch vụ").GroupBy(p => p.b.MaBNhan).Count();
            moi.KetQuaNamNay = benhnhan.Where(p =>  p.Tuoi >= 60 && p.DTuong == "Dịch vụ").Count();
            moi.KetQuaNamNgoai = benhNhanNamNgoai.Where(p =>  p.Tuoi >= 60 && p.DTuong == "Dịch vụ").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 7. Tổng số lượt khám cho người nước ngoài
            moi = new Content();
            moi.NoiDung = "7. Tổng số lượt khám cho người nước ngoài";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q7_1.Where(p => p.NuocNgoai != "" && p.NuocNgoai != "Việt Nam").Count();
            moi.KetQuaNamNgoai = q7_2.Where(p => p.NuocNgoai != "" && p.NuocNgoai != "Việt Nam").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 8. Tổng số lượt chuyển khám
            moi = new Content();
            moi.NoiDung = "8. Tổng số lượt chuyển khám:";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q7_1.Count;
            moi.KetQuaNamNgoai = q7_2.Count;
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "8a. Chuyển khám BV tuyến trên";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q7_1.Where(p => p.HangBV < tuyenBV).Count();
            moi.KetQuaNamNgoai = q7_2.Where(p => p.HangBV < tuyenBV).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "8b. Chuyển khám BV chuyên khoa (do không thuộc chức năng nghiệm vụ vủa BV)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "8c. Chuyển khám vì lý do khác (không thuốc 2 mục trên)";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q7_1.Count - q7_1.Where(p => p.HangBV < tuyenBV).Count();
            moi.KetQuaNamNgoai = q7_2.Count - q7_2.Where(p => p.HangBV < tuyenBV).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 9. Tổng số lượt người bệnh điều trị ngoại trú, Điều trị ban ngày
            moi = new Content();
            moi.NoiDung = "9. Tổng số lượt người bệnh điều trị ngoại trú, Điều trị ban ngày";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q8_1.Count();
            moi.KetQuaNamNgoai = q8_2.Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 10. Tổng số ngày nđiều trị của người bệnh điều trị ngoại trú, điều trị ban ngày. (Trong suốt đợt điều trị, mỗi lần bệnh nhân quay laih BV sử trí được tính 1 ngày)
            moi = new Content();
            moi.NoiDung = "10. Tổng số ngày nđiều trị của người bệnh điều trị ngoại trú, điều trị ban ngày. (Trong suốt đợt điều trị, mỗi lần bệnh nhân quay lại BV sử trí được tính 1 ngày)";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q8_1.Sum(p => p.SoNgaydt);
            moi.KetQuaNamNgoai = q8_2.Sum(p => p.SoNgaydt);
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 11. Tổng số lượt người bệnh nội trú, tất cả các đối tượng ( 11 = 11a + 11b + 11c + 11d):
            moi = new Content();
            moi.NoiDung = "11. Tổng số lượt người bệnh nội trú, tất cả các đối tượng ( 11 = 11a + 11b + 11c + 11d):";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q10_a.Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "11a. Tổng số lượt điều trị nội trú thu viện phí trực tiếp";
            moi.KetQua = bnrvv.Where(p => p.NgayRa == null || p.NgayRa > denngayHT).Where(p => p.DTuong == "Dịch vụ" && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = q10_a.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "11b. Tổng số lượt điều trị nội trú được BHYT chi trả ( các đối tượng thẻ BHYT)";
            moi.KetQua = bnrvv.Where(p => p.NgayRa == null || p.NgayRa > denngayHT).Where(p => p.DTuong == "BHYT" && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = q10_a.Where(p => p.DTuong == "BHYT").Where(p => !p.MaDTuong.Equals("HN")).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.DTuong == "BHYT").Where(p => !p.MaDTuong.Equals("HN")).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "11c. Tổng số lượt điều trị cho người nghèo (không có thẻ BHYT, hoặc có thẻ khám chữa bệnh cho người nghèo được quyết toán theo thực thanh thực chi";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q10_a.Where(p => p.MaDTuong == "HN").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.MaDTuong == "HN").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "11d. Tổng số lượt điều trị nội trú được miễn viện phí cho BV quyết định";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q10_a.Where(p => p.Mien == 100).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.Mien == 100).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "11đ. Tổng số lượt điều trị nội trú được giảm cho BV quyết định";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q10_a.Where(p => p.Mien > 0 && p.Mien < 100).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.Mien > 0 && p.Mien < 100).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 12. Tổng số lượt người bệnh điều trị nội trú bằng YHCT, hoặc có kết hợp YHCT
            moi = new Content();
            moi.NoiDung = "12. Tổng số lượt người bệnh điều trị nội trú bằng YHCT, hoặc có kết hợp YHCT";
            moi.KetQua = bnrvv.Where(p => p.NgayRa == null || p.NgayRa > denngayHT).Where(p => p.ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDongY && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = q11.Count();
            moi.KetQuaNamNgoai = q11_1.Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 13. Tổng số lượt trẻ em dưới 6 tuổi điều trị nội trú:
            moi = new Content();
            moi.NoiDung = "13. Tổng số lượt trẻ em dưới 6 tuổi điều trị nội trú:";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.Tuoi < 6).Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.Tuoi < 6).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "13a. Số lượt điều trị cho trẻ em dưới 6 tuổi có BHYT, hoặc có khám chữa bệnh cho trẻ em dưới 6 tuổi";
            moi.KetQua = bnrvv.Where(p => p.NgayRa == null || p.NgayRa > denngayHT).Where(p => p.DTuong == "BHYT" && p.Tuoi < 6 && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.DTuong.ToUpper().Contains("BHYT")).Where(p => p.bn2.Tuoi < 6).Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.DTuong.ToUpper().Contains("BHYT")).Where(p => p.bn2.Tuoi < 6).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "13b. Số lượt điều trị cho trẻ em dưới 6 tuổi thu phí trực tiếp";
            moi.KetQua = bnrvv.Where(p => p.NgayRa == null || p.NgayRa > denngayHT).Where(p => p.DTuong == "Dịch vụ" && p.Tuoi < 6 && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.DTuong.ToLower().Contains("dịch vụ")).Where(p => p.bn2.Tuoi < 6).Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.DTuong.ToLower().Contains("dịch vụ")).Where(p => p.bn2.Tuoi < 6).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 14. Tổng số lượt điều trị cho người bệnh cao tuổi ( >= 60)
            moi = new Content();
            moi.NoiDung = "14. Tổng số lượt điều trị cho người bệnh cao tuổi ( >= 60)";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.Tuoi >= 60).Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.Tuoi >= 60).Count();
            moi.KetQuaSoSanh = moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "14a. Số lượt điều trị cho người cao tuổi có thẻ BHYT, hoặc thẻ chính sách khác được miễn giảm viện phí";
            moi.KetQua = bnrvv.Where(p => p.NgayRa == null || p.NgayRa > denngayHT).Where(p => p.DTuong == "BHYT" && p.Tuoi >= 60 && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.DTuong.ToUpper().Equals("BHYT")).Where(p => p.bn2.Tuoi >= 60).Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.DTuong.ToUpper().Equals("BHYT")).Where(p => p.bn2.Tuoi >= 60).Count();
            moi.KetQuaSoSanh = moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "14b. Số lượt điều trị người cao tuổi thu phí trực tiếp";
            moi.KetQua = bnrvv.Where(p => p.NgayRa == null || p.NgayRa > denngayHT).Where(p => p.DTuong == "Dịch vụ" && p.Tuoi >= 60 && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.DTuong.ToLower().Contains("dịch vụ")).Where(p => p.bn2.Tuoi >= 60).Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Where(p => p.bn2.DTuong.ToLower().Contains("dịch vụ")).Where(p => p.bn2.Tuoi >= 60).Count();
            moi.KetQuaSoSanh = moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 15. Tổng số lượt điều trị cho người nước ngoài 
            moi = new Content();
            moi.NoiDung = "15. Tổng số lượt điều trị cho người nước ngoài ";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.bn2.NoiTru == 1).Where(p => p.Ngoai != "" && p.Ngoai != "Việt Nam").Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Where(p => p.Ngoai != "" && p.Ngoai != "Việt Nam").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 16. Kết quả điều tị nội trú
            moi = new Content();
            moi.NoiDung = "16. Kết quả điều tị nội trú";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q10_a.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "16a. số lượt người bệnh được điều trị khỏi";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Where(p => p.KetQua == "Khỏi" && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Khỏi").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Khỏi").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "16b. Số lượt người bệnh đỡ giảm";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Where(p => p.KetQua == "Đỡ|Giảm" && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Đỡ|Giảm").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Đỡ|Giảm").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "16c. Số lượt người bệnh kết quả không thay đổi";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Where(p => p.KetQua == "Không T.đổi" && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Không T.đổi").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Không T.đổi").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "16d. Số lượt người bệnh nặng hơn";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Where(p => p.KetQua == "Nặng hơn" && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Nặng hơn" || (p.KetQua != "Nặng hơn" && p.KetQua != "Không T.đổi" && p.KetQua != "Tử vong" && p.KetQua != "Đỡ|Giảm" && p.KetQua != "Khỏi")).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Nặng hơn" || (p.KetQua != "Nặng hơn" && p.KetQua != "Không T.đổi" && p.KetQua != "Tử vong" && p.KetQua != "Đỡ|Giảm" && p.KetQua != "Khỏi")).Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "16e. Số lượt người bệnh tiên lượng tử vong gia đình xin về";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Where(p => p.KetQua == "Tử vong" && p.NoiTru == 1).Count();
            moi.KetQuaNamNay = q10_a.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Tử vong").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaNamNgoai = q10_b.Where(p => p.NoiTru == 1).Where(p => p.KetQua == "Tử vong").Select(p => p.MaBNhan).Distinct().Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 17. Tổng số điều trị nội trú chuyển viện
            moi = new Content();
            moi.NoiDung = "17. Tổng số điều trị nội trú chuyển viện";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = q7_1.Where(p => p.NoiTru == 1).Count();
            moi.KetQuaNamNgoai = q7_2.Where(p => p.NoiTru == 1).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0; 
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "17a. Chuyển BV tuyến trên";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT && p.NoiTru == 1).Where(p => p.MaBVC == "30013").Count();
            moi.KetQuaNamNay = q7_1.Where(p => p.NoiTru == 1).Where(p => p.HangBV < tuyenBV).Count();
            moi.KetQuaNamNgoai = q7_2.Where(p => p.NoiTru == 1).Where(p => p.HangBV < tuyenBV).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "17b. Chuyển bệnh viện chuyên khoa (không thuốc CN NV)";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT && p.NoiTru == 1).Where(p => p.MaBVC == "30300" || p.MaBVC == "30330" || p.MaBVC == "30302" || p.MaBVC == "30335").Count();
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "17c. Chuyển tuyến dưới";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT && p.NoiTru == 1).Where(p => p.Tuyen == "D").Count();
            moi.KetQuaNamNay = q7_1.Where(p => p.NoiTru == 1).Where(p => p.HangBV > tuyenBV).Count();
            moi.KetQuaNamNgoai = q7_2.Where(p => p.NoiTru == 1).Where(p => p.HangBV > tuyenBV).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "17d. Chuyển viện khác (Không thuốc 3 trường hợp trên)";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT && p.NoiTru == 1).Where(p => p.MaBVC != "30013" && p.MaBVC != "30300" && p.MaBVC != "30330" && p.MaBVC != "30302" && p.MaBVC != "30335" && p.Tuyen != "D" && p.MaBVC != null).Count();
            moi.KetQuaNamNay = q7_1.Where(p => p.NoiTru == 1).Count() - q7_1.Where(p => p.NoiTru == 1).Where(p => p.HangBV < tuyenBV).Count() - q7_1.Where(p => p.NoiTru == 1).Where(p => p.HangBV > tuyenBV).Count();
            moi.KetQuaNamNgoai = q7_2.Where(p => p.NoiTru == 1).Count() - q7_2.Where(p => p.NoiTru == 1).Where(p => p.HangBV < tuyenBV).Count() - q7_2.Where(p => p.NoiTru == 1).Where(p => p.HangBV > tuyenBV).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 18. Tổng số ngày điều trị của người bệnh nội trú
            moi = new Content();
            moi.NoiDung = "18. Tổng số ngày điều trị của người bệnh nội trú";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.bn2.NoiTru == 1).Sum(p => p.rv.SoNgaydt);
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).Sum(p => p.rv.SoNgaydt);
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 19. Số ngày điều trị trung bình của người bệnh nội trú
            var q17_1 = benhnhanRV.Where(p => p.bn2.NoiTru == 1).ToList();
            var q17_2 = benhnhanRVnamNgoai.Where(p => p.bn2.NoiTru == 1).ToList();
            moi = new Content();
            moi.NoiDung = "19. Số ngày điều trị trung bình của người bệnh nội trú";
            moi.KetQua = ((bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Where(p => p.SoNGDT > 0).Count()) > 0 ) ? (double)((bnrvv.Where(p => p.SoNGDT > 0 && p.NoiTru == 1).Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Sum(p => p.SoNGDT)) / (bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Where(p => p.SoNGDT > 0).Count())) : 0;
            moi.KetQuaNamNay = (q17_1.Count != 0) ? Math.Round((double)q17_1.Sum(p => p.rv.SoNgaydt) / q17_1.Count, 2) : 0;
            moi.KetQuaNamNgoai = (q17_2.Count != 0) ? Math.Round((double)q17_2.Sum(p => p.rv.SoNgaydt) / q17_2.Count, 2) : 0;
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 20. Tổng số người bệnh tử vong tại bệnh viện (20 = 20a + 20b)
            moi = new Content();
            moi.NoiDung = "20. Tổng số người bệnh tử vong tại bệnh viện (20 = 20a + 20b)";
            //moi.KetQua = 0;
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.rv.KetQua == "Tử vong").Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.rv.KetQua == "Tử vong").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "20a. Số tử vong trong 24h đầu nhập viện";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Where(p => p.KetQua == "Tử vong").Where(p => (p.NgayRa.Value - p.NgayVao.Value).Hours <= 24).Count();
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.rv.KetQua == "Tử vong").Where(p => (p.rv.NgayRa.Value - p.rv.NgayVao.Value).Hours <= 24).Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.rv.KetQua == "Tử vong").Where(p => (p.rv.NgayRa.Value - p.rv.NgayVao.Value).Hours <= 24).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "20b. Số tử vong sau 24h nhập viện";
            moi.KetQua = bnrvv.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT).Where(p => p.KetQua == "Tử vong").Where(p => (p.NgayRa.Value - p.NgayVao.Value).Hours > 24).Count();
            moi.KetQuaNamNay = benhnhanRV.Where(p => p.rv.KetQua == "Tử vong").Where(p => (p.rv.NgayRa.Value - p.rv.NgayVao.Value).Hours > 24).Count();
            moi.KetQuaNamNgoai = benhnhanRVnamNgoai.Where(p => p.rv.KetQua == "Tử vong").Where(p => (p.rv.NgayRa.Value - p.rv.NgayVao.Value).Hours > 24).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 21. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 21a + 21b + 21c + 21d)
            moi = new Content();
            moi.NoiDung = "21. Tổng số phẫu thuật thực hiện tại BV ( loại 3 trở lên = 21a + 21b + 21c + 21d)";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "21a. Số phẫu thuật loại đặc biệt";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 0).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 0).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 0).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "21b. Số phẫu thuật loại 1";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 1).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 1).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 1).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "21c. Số phẫu thuật loại 2";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 2).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 2).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 2).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "21d. Số phẫu thuật loại 3";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 3).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 3).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 3).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 22. Phân tích cơ cấu phẫu thuật:
            moi = new Content();
            moi.NoiDung = "22. Phân tích cơ cấu phẫu thuật:";
            //moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "22a. Số phẫu thuật nội soi";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.TenDV.Contains("nội soi")).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.TenDV.Contains("nội soi")).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.TenDV.Contains("nội soi")).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "22b. Số phẫu thuật vi phẫu";
            //moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.TenDV.Contains("nội soi")).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            //moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.TenDV.Contains("nội soi")).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            //moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.TenDV.Contains("nội soi")).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            //moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "22c. Số phẫu thuật la-ze";
            //moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.TenDV.Contains("nội soi")).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            //moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.TenDV.Contains("nội soi")).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            //moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.TenDV.Contains("nội soi")).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            //moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 23. Tổng số thủ thuật được thực biện lại BV:
            moi = new Content();
            moi.NoiDung = "23. Tổng số thủ thuật được thực biện lại BV:";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "23a. Số thủ thuật loại đặc biệt";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 0).Count();
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 0).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 0).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 0).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "23b. Số thủ thuật loại 1";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 1).Count();
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 1).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 1).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 1).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "23c. Số thủ thuật loại 2";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 2).Count();
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 2).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 2).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 2).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "23d. Số thủ thuật loại 3";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 3).Count();
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 3).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 3).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat).Where(p => p.Loai == 3).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 24. Tổng số ca đẻ/sinh tại BV (bao gồm cả đẻ/sinh thường và can thiệp). Trong đó:
            var bnDe = (from bn2 in pt
                        join rv in data.RaViens on bn2.MaBNhan equals rv.MaBNhan into kq
                        from kq1 in kq.DefaultIfEmpty()
                        select new
                        {
                            bn2.NgayTH,
                            bn2.TenDV,
                            KetQua = kq1 == null ? "" : kq1.KetQua
                        }).Where(p => p.TenDV.ToLower().Contains("đỡ đẻ") || p.TenDV.ToLower().Contains("phẫu thuật lấy thai")).ToList();

            moi = new Content();
            moi.NoiDung = "24. Tổng số ca đẻ/sinh tại BV (bao gồm cả đẻ/sinh thường và can thiệp). Trong đó:";
            moi.KetQua = bnDe.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Count();
            moi.KetQuaNamNay = bnDe.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Count();
            moi.KetQuaNamNgoai = bnDe.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "24a. Số ca phẫu thuật lấy thai";
            moi.KetQua = bnDe.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenDV.ToLower().Contains("phẫu thuật lấy thai")).Count();
            moi.KetQuaNamNay = bnDe.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay && p.TenDV.ToLower().Contains("phẫu thuật lấy thai")).Count();
            moi.KetQuaNamNgoai = bnDe.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai && p.TenDV.ToLower().Contains("phẫu thuật lấy thai")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "24b. Số ca tử vong mẹ";
            moi.KetQua = bnDe.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.KetQua == "Tử vong").Count();
            moi.KetQuaNamNay = bnDe.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay && p.KetQua == "Tử vong").Count();
            moi.KetQuaNamNgoai = bnDe.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai && p.KetQua == "Tử vong").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "24c. Số ca tử vong bé sơ sinh";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 25. Tổng số lượng máu đã sửa dụng tại BV (đơn vị tính = lít)
            var q22 = (from dt in data.DThuocs
                       join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                       join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Máu") on dv.IdTieuNhom equals tn.IdTieuNhom
                       group new { dt, dtct, dv, tn } by new { dt.IDDon, dt.NgayKe, dtct.DonVi, dtct.SoLuong, dv.TenDV } into kq
                       select new
                       {
                           kq.Key.TenDV,
                           kq.Key.DonVi,
                           kq.Key.SoLuong,
                           kq.Key.NgayKe,
                       }).Where(p => p.DonVi.ToLower().Contains("ml")).ToList();

            moi = new Content();
            moi.NoiDung = "25. Tổng số lượng máu đã sửa dụng tại BV (đơn vị tính = lít)";
            moi.KetQua = q22.Where(p => p.NgayKe >= tungayHT && p.NgayKe <= denngayHT).Sum(p => p.SoLuong) / 1000;
            moi.KetQuaNamNay = q22.Where(p => p.NgayKe >= tungaynamnay && p.NgayKe <= denngaynamnay).Sum(p => p.SoLuong) / 1000;
            moi.KetQuaNamNgoai = q22.Where(p => p.NgayKe >= tungaynamngoai && p.NgayKe <= denngaynamngoai).Sum(p => p.SoLuong) / 1000;
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "25a. Số lượng máu tiếp nhận từ người hiến máu tình nguyện (đơn vị tính = lít)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "25b. Số lượng mấu tiếp nhận từ các trung tâm Huyết học truyền máu ( đơn vị tính = lít)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "25c. Số lượng mấu tiếp nhận từ các nguồn khác (người nhà, tự thân, người cho máu .v.v.) ( đơn vị tính = lít)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 26. Tổng số xét nghiệm về Sinh hóa thực hiện tại BV (26 = 26a + 26b + 26c)
            var pt1 = (from a in pt
                       join b in _data.BenhNhans on a.MaBNhan equals b.MaBNhan
                       select new { a, b }).ToList();
            moi = new Content();
            moi.NoiDung = "26. Tổng số xét nghiệm về Sinh hóa thực hiện tại BV (26 = 26a + 26b + 26c)";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "26a. Số XN Sinh hóa cho người nội trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "26b. Số XN Sinh hóa cho NB khám và điều trị ngoại trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "26c. Số XN Sinh hóa phục vụ những đối tượng không khám, chữa bệnh tại BV; Khám sức khỏe: NCKH";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 27. Tổng số xét nghiệm về Huyết học thực hiện tại BV (27 = 27a + 27b + 27c)
            moi = new Content();
            moi.NoiDung = "27. Tổng số xét nghiệm về Huyết học thực hiện tại BV (27 = 27a + 27b + 27c)";
            moi.KetQua = pt.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
            moi.KetQuaNamNay = pt.Where(p => p.NgayTH >= tungaynamnay && p.NgayTH <= denngaynamnay).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
            moi.KetQuaNamNgoai = pt.Where(p => p.NgayTH >= tungaynamngoai && p.NgayTH <= denngaynamngoai).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "27a. Số XN về Huyết học cho người nội trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "27b. Số XN về Huyết học cho NB khám và điều trị ngoại trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "27c. Số XN Huyết học phục vụ những đối tượng không khám, chữa bệnh tại BV; Khám sức khỏe: NCKH";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 28. Tổng số xét nghiệm về VI sinh thực hiện tại BV (28 = 28a + 28b + 28c)
            moi = new Content();
            moi.NoiDung = "28. Tổng số xét nghiệm về Vi sinh thực hiện tại BV (28 = 28a + 28b + 28c)";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "28a. Số XN về Vi sinh cho người nội trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "28b. Số XN về Vi sinh cho NB khám và điều trị ngoại trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "28c. Số XN Vi sinh phục vụ những đối tượng không khám, chữa bệnh tại BV; Khám sức khỏe: NCKH";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 29. Tổng số xét nghiệm về Giải phẫu bệnh lý thực hiện tại BV (29 = 29a + 29b + 29c)
            moi = new Content();
            moi.NoiDung = "29. Tổng số xét nghiệm về Giải phẫu bệnh lý thực hiện tại BV (29 = 29a + 29b + 29c)";
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            //moi.KetQua = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "29a. Số XN về Giải phẫu bệnh lý cho người nội trú";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "29b. Số XN về Giải phẫu bệnh lý cho NB khám và điều trị ngoại trú";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "29c. Số XN Giải phẫu bệnh lý phục vụ những đối tượng không khám, chữa bệnh tại BV; Khám sức khỏe: NCKH";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 30. Tổng số chụp X quang (30 = 30a + 30b + 30c)
            moi = new Content();
            moi.NoiDung = "30. Tổng số chụp X quang (30 = 30a + 30b + 30c)";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "30a. Số chụp X quang cho người nội trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "30b. Số chụp X quang cho NB khám và điều trị ngoại trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "30c. Số chụp X quang phục vụ những đối tượng không khám, chữa bệnh tại BV; Khám sức khỏe: NCKH";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 31. Tổng số chụp CT Scan (31 = 31a + 31b + 31c)
            moi = new Content();
            moi.NoiDung = "31. Tổng số chụp CT Scan (31 = 31a + 31b + 31c)";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "31a. Số chụp CT Scan cho người nội trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "31b. Số chụp CT Scan cho NB khám và điều trị ngoại trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "31c. Số chụp CT Scan phục vụ những đối tượng không khám, chữa bệnh tại BV; Khám sức khỏe: NCKH";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 32. Tổng số chụp MRI (32 = 32a + 32b + 32c)
            moi = new Content();
            moi.NoiDung = "32. Tổng số chụp MRI (32 = 32a + 32b + 32c)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "32a. Số chụp MRI cho người nội trú";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "32b. Số chụp MRI cho NB khám và điều trị ngoại trú";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "32c. Số chụp MRI phục vụ những đối tượng không khám, chữa bệnh tại BV; Khám sức khỏe: NCKH";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 33. Tổng số chụp Pet/CT (33 = 33a + 33b + 33c)
            moi = new Content();
            moi.NoiDung = "33. Tổng số chụp Pet/CT (33 = 33a + 33b + 33c)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "33a. Số chụp Pet/CT cho người nội trú";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "33b. Số chụp Pet/CT cho NB khám và điều trị ngoại trú";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "33c. Số chụp Pet/CT phục vụ những đối tượng không khám, chữa bệnh tại BV; Khám sức khỏe: NCKH";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 34. Tổng số siêu âm chẩn đoán và điều trị (34 = 34a + 34b + 34c)
            moi = new Content();
            moi.NoiDung = "34. Tổng số siêu âm chẩn đoán và điều trị  (34 = 34a + 34b + 34c)";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "34a. Số Siêu ấm cho người nội trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "34b. Số Siêu ấm cho NB khám và điều trị ngoại trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "34c. Số Siêu ấm phục vụ những đối tượng không khám, chữa bệnh tại BV; Khám sức khỏe: NCKH";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm && p.b.DTuong != "BHYT" && p.b.DTuong != "Dịch vụ" && p.b.DTuong != "KSK").Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 35. Tổng số nội soi chẩn đoán và cân thiệp (35 = 35a + 35b)
            moi = new Content();
            moi.NoiDung = "35. Tổng số nội soi chẩn đoán và cân thiệp (35 = 35a + 35b)";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "35a. Số nội soi các loại cho người nội trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && p.b.NoiTru == 1 && p.b.DTNT == false).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "35b. Số nội soi các loại cho NB khám và điều trị ngoại trú";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && p.b.NoiTru == 0 && !p.b.DTuong.ToUpper().Contains("KSK")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);
            #endregion

            #region 36. Tổng số tai biến trong điều trị phát hiện được: (36 = 36a + 36b + 36c + 36d + 36đ)
            moi = new Content();
            moi.NoiDung = "36. Tổng số tai biến trong điều trị phát hiện được: (36 = 36a + 36b + 36c + 36d + 36đ)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "36a. Số tai biến do sử dụng thuốc";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "- Số tai biến do phản ứng có hại của thuốc (ADR)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "36b. Số tai biến do truyền máu";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "36c. Số tai biến do phẫu thuật";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "36d. Số tai biến do thủ thuật";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "36đ. Số tai biến khác (ghi cụ thể)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 37. Tổng số tai biến sản, phụ khoa
            moi = new Content();
            moi.NoiDung = "37. Tổng số tai biến sản, phụ khoa";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 38. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV)
            moi = new Content();
            moi.NoiDung = "38. Số kỹ thuật lâm sàng mới (lần đầu tiên thực hiện tại BV)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "38a. Số kỹ thuật lâm sàng mới được BV tuyến trên về chuyên giao lại";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "38b. Số kỹ thuật lâm sàng mới do BV cử cán bộ đi học về triển khai";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "38c. Tổng số kỹ thuật theo phân tuyến kỹ thuật (thông tư 43)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "38c1. Tổng số kỹ thuật BV thực hiện trong phạm vi phân tuyến";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "38c2. Tổng số kỹ thuật BV thực hiện vượt tuyến";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "38c3. Tỷ lệ % thực hiện phân tuyến KT (= (38c1 + 38c2)/38c + 100)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 39
            moi = new Content();
            moi.NoiDung = "39a. Số ca phẫu thuật cao, ghép nội tạng thực hiện tại bệnh viện";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a1. Ghép Gan";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a2. Ghép Thận";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a3. Ghép Tim";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a4. Ghép tế bào gốc tự thân tạo máu";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a5. Ghép Tế bào gốc tạo máu";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a6. Ghép Giác mạc";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a7. Phẫu thuật tim hở";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a8. Can thiệp tim mạch kín";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a9. Số ca phẫu thuật thay khớp háng";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a10. Số ca phẫu thuật thay khớp gối";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a11. Số lượt chạy thận nhân tạo";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("thận nhân tạo")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("thận nhân tạo")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("thận nhân tạo")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a12. Số trẻ ra đời do thụ tinh trong ống nghiệm";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39a13. Tổng số ca ghép mô tạng khác (ghi rõ tên từng loại)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b. Tổng số kỹ thuật YHCT thực hiển tại BV (Tích hợp bảng điểm các BV YHCT)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b1. Thủy châm";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("thủy châm")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("thủy châm")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("thủy châm")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b2. Điện châm";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("điện châm")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("điện châm")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("điện châm")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b3. Hào châm";
            //moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("hào châm")).Count();
            //moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("hào châm")).Count();
            //moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("hào châm")).Count();
            //moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b4. Nhĩ châm";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b5. Cứu";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b6. Giác";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b7. Xóa bóp, bấm huyệt";
            moi.KetQua = pt1.Where(p => p.a.NgayTH >= tungayHT && p.a.NgayTH <= denngayHT).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("xoa bóp bấm huyệt")).Count();
            moi.KetQuaNamNay = pt1.Where(p => p.a.NgayTH >= tungaynamnay && p.a.NgayTH <= denngaynamnay).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("xoa bóp bấm huyệt")).Count();
            moi.KetQuaNamNgoai = pt1.Where(p => p.a.NgayTH >= tungaynamngoai && p.a.NgayTH <= denngaynamngoai).Where(p => p.a.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.a.TenDV.ToLower().Contains("xoa bóp bấm huyệt")).Count();
            moi.KetQuaSoSanh = moi.KetQuaNamNgoai > 0 ? moi.KetQuaNamNay * 100 / moi.KetQuaNamNgoai : 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b8. Xông hơi thuốc";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b9. Ngâm thuốc";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b10. Đắp thuốc tại chỗ";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b11. Vận động trị liệu";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b12. Vật lý trị liệu";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b13. Số ca phẫu thuật trĩ (dành cho BV YHCT)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b14. Số ca phẫu thuật hậu môn trực tràng (dành cho BV YHCT)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "39b15. Các kỹ thuật YHCT khác (ghi rõ tên từng loại)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 40. Số lượng kỹ thuật Cận lâm sàng MỚI (lần đầu tiên thực hiện tại BV)
            moi = new Content();
            moi.NoiDung = "40. Số lượng kỹ thuật Cận lâm sàng MỚI (lần đầu tiên thực hiện tại BV)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "40a. SỐ kỹ thuật cận lâm sàng mới được BV tuyến trên, chuyển giao tại BV";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "40b. Số kỹ thuật lâm sàng mới do BV cử cán bộ đi học về triển khai";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 41. Tổng số lượt CBVC luân phiên theo đề án 1816
            moi = new Content();
            moi.NoiDung = "41. Tổng số lượt CBVC luân phiên theo đề án 1816";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "41a. Bác sỹ";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "41b. Dược sỹ";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "42c. Điều dưỡng/Hộ sinh/KTV";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "42d. Cán bộ hỗ trợ kỹ thuật sửa chữa trang thiết bị";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "42e. Đối tượng khác";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 42. Tổng số lượt cán bộ viên chức tham gia chỉ đạo tuyến (lượt người)
            moi = new Content();
            moi.NoiDung = "42. Tổng số lượt cán bộ viên chức tham gia chỉ đạo tuyến (lượt người)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 43. Số lượt kỹ thuật Lâm sàng chuyển giao cho tuyến dưới: (thống nhất cách tính: một kỹ thuật cùng chuyển giao cho 5 BV được tính là  5 lần)
            moi = new Content();
            moi.NoiDung = "43. Số lượt kỹ thuật Lâm sàng chuyển giao cho tuyến dưới: (thống nhất cách tính: một kỹ thuật cùng chuyển giao cho 5 BV được tính là  5 lần)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 44. Số lần kỹ thuật Cận lâm sàng chuyển giao tuyến dưới: (cách tính như kỹ thuật lầm sàng)
            moi = new Content();
            moi.NoiDung = "44. Số lần kỹ thuật Cận lâm sàng chuyển giao tuyến dưới: (cách tính như kỹ thuật lầm sàng)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 45. Số (lượt người) cán bộ tuyến dưới được tập huấn chuyên môn do BV tổ chức (cả ngắn, dài ngày):
            moi = new Content();
            moi.NoiDung = "45. Số (lượt người) cán bộ tuyến dưới được tập huấn chuyên môn do BV tổ chức (cả ngắn, dài ngày):";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 46. Số (lượt người) cán bộ tuyến dưới được tập huấn chuyên môn do BV tổ chức (cả ngắn, dài ngày):
            moi = new Content();
            moi.NoiDung = "46. Số (lượt người) cán bộ của BV được tập huấn chuyên môn (cả ngắn, dài ngày):";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 47. Hoạt động thông tin 2 chiều: Số công văn nawhcs tuyến dưới về các lỗi, sai sót chuyên môn
            moi = new Content();
            moi.NoiDung = "47. Hoạt động thông tin 2 chiều: Số công văn nawhcs tuyến dưới về các lỗi, sai sót chuyên môn";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 48. Số lượt sơ kết, tổng kết về công tác chỉ đạo tuyến/1816
            moi = new Content();
            moi.NoiDung = "48. Số lượt sơ kết, tổng kết về công tác chỉ đạo tuyến/1816";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 49
            moi = new Content();
            moi.NoiDung = "49a. Số đề tài nghiên cứu khoa học Cấp cơ sở do BV chủ trì đã nghiệm thu:";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "49b. Số đề tài nghiên cứu khoa học Cấp bộ/ngành/tỉnh BV chủ trì đã nghiệm thu:";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "49c. Số đề tài nghiên cứu khoa học Cấp Nhà nước do BV chủ trì đã nghiệm thu:";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 50
            moi = new Content();
            moi.NoiDung = "50a. Số bài báo đã đăng trên các tạp chí chuyên ngành quốc tế";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "50b. Số bài báo đã đăng trên các tạp chí chuyên ngành trong nước";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            #region 52
            moi = new Content();
            moi.NoiDung = "52. Số lượt cán bộ y tế người nước ngoài làm việc tại BV (làm việc có hợp đồng từ 3 tháng trở lên, không tính học việc, nghiên cứu, giảng dạy...)";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "52a. Bác sỹ";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "52b. Dược sỹ";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "52c. Điều dưỡng/Hộ sinh/KTV";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "52d. Quản lý bệnh viện";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);

            moi = new Content();
            moi.NoiDung = "52e. Đối tượng khác";
            //moi.KetQua = 0;
            //moi.KetQuaNamNay = 0;
            //moi.KetQuaNamNgoai = 0;
            //moi.KetQuaSoSanh = 0;
            _listContent.Add(moi);
            #endregion

            if(radioGroup1.SelectedIndex == 0)
            {
                BaoCao.rep_BC_TTHDChuyenMon rep = new BaoCao.rep_BC_TTHDChuyenMon();
                frmIn frm = new frmIn();

                rep.ngay.Text = "Từ " + tungayHT.Day + "/" + tungayHT.Month + "/" + tungayHT.Year + " đến " + denngayHT.Day + "/" + denngayHT.Month + "/" + denngayHT.Year;
                rep.DataSource = _listContent.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                BaoCao.rep_BCCuoiNam_01071 rep = new BaoCao.rep_BCCuoiNam_01071();
                frmIn frm = new frmIn();
                rep.namnay.Text = namnay;
                rep.namngoai.Text = namngoai;
                rep.DataSource = _listContent.ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BC_TTHDChuyenMon_Load(object sender, EventArgs e)
        {
            tungay.DateTime = DateTime.Now;
            denngay.DateTime = DateTime.Now;
            tungay.Enabled = true;
            denngay.Enabled = true;
            comboBoxEdit1.Enabled = false;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                tungay.Enabled = true;
                denngay.Enabled = true;
                comboBoxEdit1.Enabled = false;
            }
            else
            {
                tungay.Enabled = false;
                denngay.Enabled = false;
                comboBoxEdit1.Enabled = true;
            }
        }
    }
}