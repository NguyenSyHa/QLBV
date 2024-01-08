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
    public partial class frm_KetQuaHDThang_30010 : DevExpress.XtraEditors.XtraForm
    {
        public frm_KetQuaHDThang_30010()
        {
            InitializeComponent();
        }
        #region class Content
        public class Content
        {
            public string TenNhom { get; set; }
            public string TenNhom1 { get; set; }
            public string TenChiTiet { get; set; }
            public int Stt { get; set; }
            public string DVT { get; set; }
            public double? chitieu { get; set; }
            public double? thangtruoc { get; set; }
            public double? thangnay { get; set; }
            public double? tongdenthangnay { get; set; }
            public double? dat { get; set; }
        }
        #endregion
        List<Content> _listContent = new List<Content>();
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _listContent.Clear();
            Content moi = new Content();
            DateTime tungayHT = new DateTime();
            DateTime denngayHT = new DateTime();
            DateTime ngaydaunam = new DateTime();
            int ngay = 0, ngay1 = 0;
            string tenhuyen = "";
            if (!string.IsNullOrEmpty(DungChung.Bien.MaHuyen))
            {
                var qhuyen = (from h in data.DmHuyens.Where(p => p.MaHuyen.Equals(DungChung.Bien.MaHuyen)) select h).ToList();
                if (qhuyen.Count > 0)
                    tenhuyen = qhuyen.FirstOrDefault().TenHuyen;
            }
            tungayHT = DungChung.Ham.NgayTu(GetFirstDayOfMonth(Convert.ToInt32(comboBoxEdit1.Text), Convert.ToInt32(textEdit1.Text)));
            denngayHT = DungChung.Ham.NgayDen(GetLastDayOfMonth(Convert.ToInt32(comboBoxEdit1.Text), Convert.ToInt32(textEdit1.Text)));
            ngaydaunam = DungChung.Ham.NgayTu(GetFirstDayOfMonth(1, Convert.ToInt32(textEdit1.Text)));
            ngay = (denngayHT - tungayHT).Days + 1; ngay1 = (tungayHT - ngaydaunam).Days + 1;

            #region query
            var benhnhan = (from bn in data.BenhNhans
                            join bnkb in data.BNKBs.Where(p => p.NgayKham >= tungayHT && p.NgayKham <= denngayHT) on bn.MaBNhan equals bnkb.MaBNhan
                            join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                            where (DungChung.Bien.MaBV.Equals("30012") ? (kp.PLoai == "Phòng khám") : kp.PLoai == "Phòng khám")
                            select bn).ToList();
            var benhnhanthangtruoc = (from bn in data.BenhNhans
                                      join bnkb in data.BNKBs.Where(p => p.NgayKham < tungayHT && p.NgayKham >= ngaydaunam) on bn.MaBNhan equals bnkb.MaBNhan
                            join kp in data.KPhongs on bnkb.MaKP equals kp.MaKP
                            where (DungChung.Bien.MaBV.Equals("30012") ? (kp.PLoai == "Phòng khám") : kp.PLoai == "Phòng khám")
                            select bn).ToList();
            var benhnhanRV = (from bn in data.BenhNhans
                              join rv in data.RaViens.Where(p => p.NgayRa >= tungayHT && p.NgayRa <= denngayHT) on bn.MaBNhan equals rv.MaBNhan
                              select new { bn, rv }).ToList();
            var benhnhanRVthangtruoc = (from bn in data.BenhNhans
                              join rv in data.RaViens.Where(p => p.NgayRa < tungayHT && p.NgayRa >= ngaydaunam) on bn.MaBNhan equals rv.MaBNhan
                              select new { bn, rv }).ToList();
            var donthuoc = (from a in data.DThuocs
                            join b in data.DThuoccts on a.IDDon equals b.IDDon
                            join c in data.DichVus.Where(p => p.PLoai == 1) on b.MaDV equals c.MaDV
                            join d in data.BenhNhans on a.MaBNhan equals d.MaBNhan
                            group new { a, d} by new { a.IDDon, a.NgayKe, d.MaBNhan, d.DTuong, d.NoiTru, d.Tuoi  } into kq
                            select new { kq.Key.IDDon, kq.Key.NgayKe, kq.Key.MaBNhan, kq.Key.DTuong, kq.Key.NoiTru, kq.Key.Tuoi }).OrderBy(p => p.NgayKe).ToList();
            var bnvv = (from a in data.VaoViens
                        join b in data.BenhNhans on a.MaBNhan equals b.MaBNhan
                        select new { a.MaBNhan, a.NgayVao, b }).ToList();
            var q19 = (from bn in data.BenhNhans
                       join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                       join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new { tn.TenRG, dv.Loai, NgayTH = dtct.NgayNhap, bn.MaBNhan, dv.TenDV, bn.NoiTru, bn.DTNT, bn.DTuong }).ToList();

            var q17_1 = benhnhanRV.Where(p => p.bn.NoiTru == 1).ToList();
            var q17_2 = benhnhanRVthangtruoc.Where(p => p.bn.NoiTru == 1).ToList();

            List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
            List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong> _da1 = new List<QLBV.FormThamSo.frm_NhapBuongGiuongKeKoach.dsBuongGiuong>();
            List<KPhong> _lKhoaP = new List<KPhong>();
            _lKhoaP = data.KPhongs.ToList();
            foreach (var item in data.KPhongs.ToList())
            {
                _da = frm_NhapBuongGiuongKeKoach.getBuongGiuong(data, item.MaKP, Convert.ToString(textEdit1.Text));
                foreach (var item1 in _da)
                {
                    _da1.Add(item1);
                }
            }
            var n1 = (from a in _da1 group a by new { a.giuongKH, a.makp } into kq select new { kq.Key.makp, kq.Key.giuongKH }).ToList();

            #endregion

            #region 1.Tổng số lượt khám bệnh
            moi = new Content();
            moi.Stt = 1;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số khám bệnh";
            moi.TenChiTiet = "";
            moi.DVT = "Lần"; 
            //moi.chitieu = 0;
            moi.thangtruoc = benhnhanthangtruoc.Count();
            moi.thangnay = benhnhan.Count();
            moi.tongdenthangnay = benhnhanthangtruoc.Count() + benhnhan.Count();
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 1;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số khám bệnh";
            moi.TenChiTiet = " Trong đó: Khám sức khỏe";
            moi.DVT = "Lần";
            //moi.chitieu = 0;
            moi.thangtruoc = benhnhanthangtruoc.Where(p => p.DTuong == "KSK").Count();
            moi.thangnay = benhnhan.Where(p => p.DTuong == "KSK").Count();
            moi.tongdenthangnay = benhnhanthangtruoc.Where(p => p.DTuong == "KSK").Count() + benhnhan.Where(p => p.DTuong == "KSK").Count();
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 1;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số khám bệnh";
            moi.TenChiTiet = " Khám BHYT";
            moi.DVT = "Lần";
            //moi.chitieu = 0;
            moi.thangtruoc = benhnhanthangtruoc.Where(p => p.DTuong == "BHYT").Count();
            moi.thangnay = benhnhan.Where(p => p.DTuong == "BHYT").Count();
            moi.tongdenthangnay = benhnhanthangtruoc.Where(p => p.DTuong == "BHYT").Count() + benhnhan.Where(p => p.DTuong == "KSK").Count();
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 1;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số khám bệnh";
            moi.TenChiTiet = " TE dưới 6 tuổi";
            moi.DVT = "Lần";
            //moi.chitieu = 0;
            moi.thangtruoc = benhnhanthangtruoc.Where(p => p.Tuoi < 6).Count();
            moi.thangnay = benhnhan.Where(p => p.Tuoi < 6).Count();
            moi.tongdenthangnay = benhnhanthangtruoc.Where(p => p.Tuoi < 6).Count() + benhnhan.Where(p => p.DTuong == "KSK").Count();
            _listContent.Add(moi);
            #endregion

            #region 2. TỔng số bệnh nhân điều trị ngoiaj trú BHYT
            moi = new Content();
            moi.Stt = 2;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số BN điều trị ngoại trú BHYT";
            moi.TenChiTiet = "";
            moi.DVT = "BN";
            //moi.chitieu = 0;
            moi.thangtruoc = benhnhanthangtruoc.Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.DTNT == true).GroupBy(p => p.MaBNhan).Count();
            moi.thangnay = benhnhan.Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.DTNT == true).GroupBy(p => p.MaBNhan).Count();
            moi.tongdenthangnay = benhnhanthangtruoc.Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.DTNT == true).GroupBy(p => p.MaBNhan).Count() + benhnhan.Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.DTNT == true).Count();
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 2;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số BN điều trị ngoại trú BHYT";
            moi.TenChiTiet = " Trong đó: YHCT";
            moi.DVT = "BN";
            //moi.chitieu = 0;
            moi.thangtruoc =( from a in benhnhanthangtruoc.Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.DTNT == true)
                                  join b in data.KPhongs.Where(p => p.TenKP.Contains("YHCT")) on a.MaKP equals b.MaKP select a).GroupBy(p => p.MaBNhan).Count();
            moi.thangnay =( from a in benhnhan.Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.DTNT == true)
                                  join b in data.KPhongs.Where(p => p.TenKP.Contains("YHCT")) on a.MaKP equals b.MaKP select a).GroupBy(p => p.MaBNhan).Count();
            moi.tongdenthangnay = (from a in benhnhanthangtruoc.Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.DTNT == true)
                                   join b in data.KPhongs.Where(p => p.TenKP.Contains("YHCT")) on a.MaKP equals b.MaKP
                                   select a).GroupBy(p => p.MaBNhan).Count() + (from a in benhnhan.Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.DTNT == true)
                                                                                join b in data.KPhongs.Where(p => p.TenKP.Contains("YHCT")) on a.MaKP equals b.MaKP
                                                                                select a).GroupBy(p => p.MaBNhan).Count(); ;
            _listContent.Add(moi);
            #endregion

            #region 3. Đơn thuốc BHYT ngoại trú
            moi = new Content();
            moi.Stt = 3;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Đơn thuốc BHYT ngoại trú";
            moi.TenChiTiet = "";
            moi.DVT = "Đơn";
            //moi.chitieu = 0;
            moi.thangtruoc = donthuoc.Where(p => p.NgayKe >= ngaydaunam && p.NgayKe < tungayHT).Where(p => p.DTuong == "BHYT" && p.NoiTru == 0).Count();
            moi.thangnay = donthuoc.Where(p => p.NgayKe >= tungayHT && p.NgayKe <= denngayHT).Where(p => p.DTuong == "BHYT" && p.NoiTru == 0).Count();
            moi.tongdenthangnay = donthuoc.Where(p => p.NgayKe >= ngaydaunam && p.NgayKe < tungayHT).Where(p => p.DTuong == "BHYT" && p.NoiTru == 0).Count() + donthuoc.Where(p => p.NgayKe >= tungayHT && p.NgayKe <= denngayHT).Where(p => p.DTuong == "BHYT" && p.NoiTru == 0).Count();
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 3;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Đơn thuốc BHYT ngoại trú";
            moi.TenChiTiet = " Trẻ em < 6T";
            moi.DVT = "Đơn";
            //moi.chitieu = 0;
            moi.thangtruoc = donthuoc.Where(p => p.NgayKe >= ngaydaunam && p.NgayKe < tungayHT).Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.Tuoi < 6 ).Count();
            moi.thangnay = donthuoc.Where(p => p.NgayKe >= tungayHT && p.NgayKe <= denngayHT).Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.Tuoi < 6).Count();
            moi.tongdenthangnay = donthuoc.Where(p => p.NgayKe >= ngaydaunam && p.NgayKe < tungayHT).Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.Tuoi < 6).Count() + donthuoc.Where(p => p.NgayKe >= tungayHT && p.NgayKe <= denngayHT).Where(p => p.DTuong == "BHYT" && p.NoiTru == 0 && p.Tuoi < 6).Count();
            _listContent.Add(moi);

            #endregion

            #region 4. Tổng số lượt bn điều trị nội trú
            moi = new Content();
            moi.Stt = 4;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số lượt bn điều trị nội trú";
            moi.TenChiTiet = "";
            moi.DVT = "Lượt";
            //moi.chitieu = 0;
            moi.thangtruoc = bnvv.Where(p => p.NgayVao >= ngaydaunam && p.NgayVao < tungayHT).Where(p =>p.b.NoiTru == 1).Count();
            moi.thangnay = bnvv.Where(p => p.NgayVao >= tungayHT && p.NgayVao <= denngayHT).Where(p =>p.b.NoiTru == 1).Count();
            moi.tongdenthangnay = bnvv.Where(p => p.NgayVao >= ngaydaunam && p.NgayVao < tungayHT).Where(p =>p.b.NoiTru == 1).Count() + bnvv.Where(p => p.NgayVao >= tungayHT && p.NgayVao <= denngayHT).Where(p =>p.b.NoiTru == 1).Count();
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 4;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số lượt bn điều trị nội trú";
            moi.TenChiTiet = " Trong đó: Bệnh nhân BHYT";
            moi.DVT = "Lượt";
            //moi.chitieu = 0;
            moi.thangtruoc = bnvv.Where(p => p.NgayVao >= ngaydaunam && p.NgayVao < tungayHT).Where(p => p.b.DTuong == "BHYT").Where(p => p.b.NoiTru == 1).Count();
            moi.thangnay = bnvv.Where(p => p.NgayVao >= tungayHT && p.NgayVao <= denngayHT).Where(p => p.b.DTuong == "BHYT").Where(p => p.b.NoiTru == 1).Count();
            moi.tongdenthangnay = bnvv.Where(p => p.NgayVao >= ngaydaunam && p.NgayVao < tungayHT).Where(p => p.b.DTuong == "BHYT").Where(p => p.b.NoiTru == 1).Count() + bnvv.Where(p => p.NgayVao >= tungayHT && p.NgayVao <= denngayHT).Where(p => p.b.DTuong == "BHYT").Where(p => p.b.NoiTru == 1).Count();
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 4;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số lượt bn điều trị nội trú";
            moi.TenChiTiet = "           TE < 6T";
            moi.DVT = "Lượt";
            //moi.chitieu = 0;
            moi.thangtruoc = bnvv.Where(p => p.NgayVao >= ngaydaunam && p.NgayVao < tungayHT).Where(p => p.b.Tuoi < 6).Where(p => p.b.NoiTru == 1).Count();
            moi.thangnay = bnvv.Where(p => p.NgayVao >= tungayHT && p.NgayVao <= denngayHT).Where(p => p.b.Tuoi < 6).Where(p => p.b.NoiTru == 1).Count();
            moi.tongdenthangnay = bnvv.Where(p => p.NgayVao >= ngaydaunam && p.NgayVao < tungayHT).Where(p => p.b.Tuoi < 6).Where(p => p.b.NoiTru == 1).Count() + bnvv.Where(p => p.NgayVao >= tungayHT && p.NgayVao <= denngayHT).Where(p => p.b.Tuoi < 6).Where(p => p.b.NoiTru == 1).Count();
            _listContent.Add(moi);

            #endregion

            #region 5.Tổng số ngày điều trị nội trú
            moi = new Content();
            moi.Stt = 5;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số ngày điều trị nội trú";
            moi.TenChiTiet = "";
            moi.DVT = "Ngày";
            //moi.chitieu = 0;
            moi.thangtruoc = benhnhanRVthangtruoc.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt);
            moi.thangnay = benhnhanRV.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt);
            moi.tongdenthangnay = benhnhanRV.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt) + benhnhanRVthangtruoc.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt);
            _listContent.Add(moi);
            #endregion

            #region 6. Số chết ở cơ sở y tế
            moi = new Content();
            moi.Stt = 6;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Số chết ở cơ sở y tế";
            moi.TenChiTiet = "";
            moi.DVT = "Người";
            //moi.chitieu = 0;
            moi.thangtruoc = benhnhanRVthangtruoc.Where(p => p.rv.NgayRa >= ngaydaunam && p.rv.NgayRa < tungayHT).Where(p => p.rv.KetQua == "Tử vong").Count();
            moi.thangnay = benhnhanRV.Where(p => p.rv.NgayRa >= tungayHT && p.rv.NgayRa <= denngayHT).Where(p => p.rv.KetQua == "Tử vong").Count();
            moi.tongdenthangnay = benhnhanRVthangtruoc.Where(p => p.rv.NgayRa >= ngaydaunam && p.rv.NgayRa < tungayHT).Where(p => p.rv.KetQua == "Tử vong").Count() + benhnhanRV.Where(p => p.rv.NgayRa >= tungayHT && p.rv.NgayRa <= denngayHT).Where(p => p.rv.KetQua == "Tử vong").Count();
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 6;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Số chết ở cơ sở y tế";
            moi.TenChiTiet = " Trong đó: Chết < 1 Tuổi";
            moi.DVT = "Người";
            //moi.chitieu = 0;
            moi.thangtruoc = benhnhanRVthangtruoc.Where(p => p.rv.NgayRa >= ngaydaunam && p.rv.NgayRa < tungayHT).Where(p => p.rv.KetQua == "Tử vong").Where(p => p.bn.Tuoi < 1).Count();
            moi.thangnay = benhnhanRV.Where(p => p.rv.NgayRa >= tungayHT && p.rv.NgayRa <= denngayHT).Where(p => p.rv.KetQua == "Tử vong").Where(p => p.bn.Tuoi < 1).Count();
            moi.tongdenthangnay = benhnhanRVthangtruoc.Where(p => p.rv.NgayRa >= ngaydaunam && p.rv.NgayRa < tungayHT).Where(p => p.rv.KetQua == "Tử vong").Where(p => p.bn.Tuoi < 1).Count() + benhnhanRV.Where(p => p.rv.NgayRa >= tungayHT && p.rv.NgayRa <= denngayHT).Where(p => p.rv.KetQua == "Tử vong").Where(p => p.bn.Tuoi < 1).Count();
            _listContent.Add(moi);

            moi = new Content();
            moi.Stt = 6;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số lượt bn điều trị nội trú";
            moi.TenChiTiet = "           Chết < 6 Tuổi";
            moi.DVT = "Người";
            //moi.chitieu = 0;
            moi.thangtruoc = benhnhanRVthangtruoc.Where(p => p.rv.NgayRa >= ngaydaunam && p.rv.NgayRa < tungayHT).Where(p => p.rv.KetQua == "Tử vong").Where(p => p.bn.Tuoi < 6).Count();
            moi.thangnay = benhnhanRV.Where(p => p.rv.NgayRa >= tungayHT && p.rv.NgayRa <= denngayHT).Where(p => p.rv.KetQua == "Tử vong").Where(p => p.bn.Tuoi < 6).Count();
            moi.tongdenthangnay = benhnhanRVthangtruoc.Where(p => p.rv.NgayRa >= ngaydaunam && p.rv.NgayRa < tungayHT).Where(p => p.rv.KetQua == "Tử vong").Where(p => p.bn.Tuoi < 6).Count() + benhnhanRV.Where(p => p.rv.NgayRa >= tungayHT && p.rv.NgayRa <= denngayHT).Where(p => p.rv.KetQua == "Tử vong").Where(p => p.bn.Tuoi < 6).Count();
            _listContent.Add(moi);

            #endregion

            #region 7. Ngày điều trị trung bình của BN nội trú
            var benhnhanRV1 = (from bn in data.BenhNhans
                              join rv in data.RaViens.Where(p => p.NgayRa >= ngaydaunam && p.NgayRa <= denngayHT) on bn.MaBNhan equals rv.MaBNhan
                              select new { bn, rv }).ToList();
            var q17_11 = benhnhanRV1.Where(p => p.bn.NoiTru == 1).ToList();
            moi = new Content();
            moi.Stt = 7;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Ngày điều trị trung bình của BN nội trú";
            moi.TenChiTiet = "";
            moi.DVT = "Ngày";
            //moi.chitieu = 0;
            moi.thangtruoc = (q17_2.Count != 0) ? Math.Round((double)q17_2.Sum(p => p.rv.SoNgaydt) / q17_2.Count, 2) : 0;
            moi.thangnay = (q17_1.Count != 0) ? Math.Round((double)q17_1.Sum(p => p.rv.SoNgaydt) / q17_1.Count, 2) : 0;
            moi.tongdenthangnay = (q17_11.Count != 0) ? Math.Round((double)q17_11.Sum(p => p.rv.SoNgaydt) / q17_11.Count, 2) : 0;
            _listContent.Add(moi);

            #endregion

            #region 8. Công suất sử dụng giường bệnh kế hoạch
            moi = new Content();
            moi.Stt = 8;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Công suất sử dụng giường bệnh kế hoạch";
            moi.TenChiTiet = "";
            moi.DVT = "%";
            moi.chitieu = 100;
            moi.thangtruoc = (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay != 0) ? (double)(benhnhanRVthangtruoc.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay)) : 0.0;
            moi.thangnay = (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay != 0) ? (double)(benhnhanRV.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay)) : 0.0;
            moi.tongdenthangnay = ( (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay != 0) ? (double)(benhnhanRVthangtruoc.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay)) : 0.0 ) + ( (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay != 0) ? (double)(benhnhanRV.Where(p => p.bn.NoiTru == 1).Sum(p => p.rv.SoNgaydt) * 100 / (n1.Sum(p => Convert.ToInt32(p.giuongKH)) * ngay)) : 0.0 );
            _listContent.Add(moi);

            #endregion

            #region 9. Tổng số ca phẫu thuật
            moi = new Content();
            moi.Stt = 9;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số ca phẫu thuật";
            moi.TenChiTiet = "";
            moi.DVT = "Ca";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == "Phẫu Thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Phẫu Thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == "Phẫu Thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Phẫu Thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            _listContent.Add(moi); 
            #endregion

            #region 10. Tổng số ca thủ thuật
            moi = new Content();
            moi.Stt = 10;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số ca thủ thuật";
            moi.TenChiTiet = "";
            moi.DVT = "Ca";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == "Thủ thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Thủ thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == "Thủ thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == "Thủ thuật" && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Count();
            _listContent.Add(moi);
            #endregion

            #region 11. Tổng số lần xét nghiệm
            moi = new Content();
            moi.Stt = 11;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số lần xét nghiệm";
            moi.TenChiTiet = "";
            moi.DVT = "Lần";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG.Contains("XN")).Select(p => p.MaBNhan).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG.Contains("XN")).Select(p => p.MaBNhan).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG.Contains("XN")).Select(p => p.MaBNhan).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG.Contains("XN")).Select(p => p.MaBNhan).Count();
            _listContent.Add(moi);
            #endregion

            #region 12. Tổng số lần chụp X-Quang
            moi = new Content();
            moi.Stt = 12;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số lần chụp X-Quang";
            moi.TenChiTiet = "";
            moi.DVT = "Lần";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.MaBNhan).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.MaBNhan).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.MaBNhan).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Select(p => p.MaBNhan).Count();
            _listContent.Add(moi);
            #endregion

            #region 13. Tổng số lần điện tim
            moi = new Content();
            moi.Stt = 13;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số lần điện tim";
            moi.TenChiTiet = "";
            moi.DVT = "Lần";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Select(p => p.MaBNhan).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Select(p => p.MaBNhan).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Select(p => p.MaBNhan).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Select(p => p.MaBNhan).Count();
            _listContent.Add(moi);
            #endregion

            #region 14. Tổng số lần siêu âm
            moi = new Content();
            moi.Stt = 14;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số lần siêu âm";
            moi.TenChiTiet = "";
            moi.DVT = "Lần";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p => p.MaBNhan).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p => p.MaBNhan).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p => p.MaBNhan).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm).Select(p => p.MaBNhan).Count();
            _listContent.Add(moi);
            #endregion

            #region 15. Tổng số ca nội soi
            moi = new Content();
            moi.Stt = 15;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số ca nội soi";
            moi.TenChiTiet = "";
            moi.DVT = "Ca";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
            _listContent.Add(moi);
            #endregion

            #region 16. Tổng số lần chụp CT - Canner
            moi = new Content();
            moi.Stt = 16;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số lần chụp CT - Canner";
            moi.TenChiTiet = "";
            moi.DVT = "Lần";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.MaBNhan).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.MaBNhan).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.MaBNhan).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Select(p => p.MaBNhan).Count();
            _listContent.Add(moi);
            #endregion

            #region 17. Tổng số ca đo loãng xương
            moi = new Content();
            moi.Stt = 17;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Tổng số ca đo loãng xương";
            moi.TenChiTiet = "";
            moi.DVT = "Ca";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong).Count();
            _listContent.Add(moi);
            #endregion

            #region 18. Tổng số ca đo chức năng ho hấp
            moi = new Content();
            moi.Stt = 18;
            moi.TenNhom = "HOẠT ĐỘNG CHUYÊN MÔN";
            moi.TenNhom1 = "Đo chức năng hô hấp";
            moi.TenChiTiet = "";
            moi.DVT = "Ca";
            //moi.chitieu = 0;
            moi.thangtruoc = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
            moi.thangnay = q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
            moi.tongdenthangnay = q19.Where(p => p.NgayTH >= ngaydaunam && p.NgayTH < tungayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count() + q19.Where(p => p.NgayTH >= tungayHT && p.NgayTH <= denngayHT && p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap).Count();
            _listContent.Add(moi);
            #endregion

            BaoCao.rep_KetQuaHDThang rep = new BaoCao.rep_KetQuaHDThang();
            frmIn frm = new frmIn();
            rep.lblTitle.Text = "KẾT QUẢ HOẠT ĐỘNG THÁNG " + comboBoxEdit1.Text + " Năm " + textEdit1.Text;
            rep.xrTableCell4.Text = "Chỉ tiêu kế hoạch năm " + textEdit1.Text;
            rep.xrLabel7.Text = "Bệnh viện đa khoa huyện Kim Thành báo cáo công tác khám chưa bệnh tháng " + comboBoxEdit1.Text + "năm" + textEdit1.Text + "kết quả cụ thể như sau:";
            rep.lblNgayThang.Text = tenhuyen + ", ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.DataSource = _listContent;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
        public static DateTime GetFirstDayOfMonth(int iMonth, int iYear)
        {
            DateTime dtResult = new DateTime(iYear, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        public static DateTime GetLastDayOfMonth(int iMonth, int iYear)
        {
            DateTime dtResult = new DateTime(iYear, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
    }
}