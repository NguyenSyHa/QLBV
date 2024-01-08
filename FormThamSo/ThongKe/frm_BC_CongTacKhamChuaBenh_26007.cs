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
    public partial class frm_BC_CongTacKhamChuaBenh_26007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_CongTacKhamChuaBenh_26007()
        {
            InitializeComponent();
        }

        #region class Khoa
        private class Khoa
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
        #endregion
        #region class noidung
        private class noidung
        {
            public string Stt { get; set; }
            public string tieude { get; set; }
            public string donvi { get; set; }
            public double? kehoach { get; set; }
            public double? thuchien { get; set; }
            public double? congdon { get; set; }
            public double? phantram { get; set; }
        }
        #endregion

        List<Khoa> _lKhoa = new List<Khoa>();
        List<noidung> _lnoidung = new List<noidung>();
        private void frm_BC_CongTacKhamChuaBenh_26007_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            _lKhoa.Clear();
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") select new { kp.TenKP, kp.MaKP }).ToList();

            if (kphong.Count > 0)
            {
                Khoa themmoi1 = new Khoa();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _lKhoa.Add(themmoi1);
                foreach (var a in kphong)
                {
                    Khoa themmoi = new Khoa();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _lKhoa.Add(themmoi);
                }
                grcKhoaphong.DataSource = _lKhoa.ToList();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            List<Khoa> khoa = new List<Khoa>();
            khoa = _lKhoa.Where(p => p.chon == true && p.makp > 0).ToList();
            var qkhoa = (from k in khoa
                         join kp in data.KPhongs on k.makp equals kp.MaKP
                         select new { k.makp, k.tenkp, kp.PLoai }).ToList();
            _lnoidung.Clear();
            noidung nd = new noidung();
            #region query
            var bnkb = data.BNKBs.ToList();
            var bn1 = data.BenhNhans.ToList();
            var rv1 = data.RaViens.ToList();
            var vv1 = data.VaoViens.ToList();
            var qksk = (from k in khoa
                        join kb in bnkb.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on k.makp equals kb.MaKP
                        join bn in bn1 on kb.MaBNhan equals bn.MaBNhan
                        select new { kb.IDKB, bn.MaBNhan, bn.Tuoi, bn.NoiTru, bn.DTNT, bn.DTuong }).ToList();

            var qkb1 = (from kb in bnkb
                        join bn in bn1 on kb.MaBNhan equals bn.MaBNhan
                        //join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                        join rv in rv1 on bn.MaBNhan equals rv.MaBNhan
                        select new { kb.IDKB, bn.MaBNhan, bn.Tuoi, bn.NoiTru, bn.DTNT, kb.NgayKham, rv.NgayVao, bn.DTuong, rv.SoNgaydt, kb.MaKP, kb.ChanDoan }).ToList();

            var qkb = (from k in qkhoa
                       join kb in qkb1 on k.makp equals kb.MaKP
                       select new { kb.IDKB, kb.MaBNhan, kb.Tuoi, kb.NoiTru, kb.DTNT, kb.NgayKham, kb.NgayVao, kb.DTuong, kb.SoNgaydt, k.PLoai, kb.MaKP, kb.ChanDoan }).ToList();

            var qdt = (from k in khoa
                       join bn in bn1 on k.makp equals bn.MaKP
                       //join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       join rv in rv1 on bn.MaBNhan equals rv.MaBNhan
                       select new { bn.MaBNhan, bn.Tuoi, bn.NoiTru, bn.DTNT, rv.NgayVao, bn.DTuong, rv.SoNgaydt, rv.NgayRa, rv.Status, rv.KetQua }).ToList();

            var qbnluu = (from k in khoa
                          join bn in bn1 on k.makp equals bn.MaKP
                          join vv in vv1 on bn.MaBNhan equals vv.MaBNhan
                          join rv in rv1 on bn.MaBNhan equals rv.MaBNhan into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new { bn.MaBNhan, vv.NgayVao, NgayRa = kq1 == null ? null : kq1.NgayRa, bn.NoiTru, bn.DTNT }).ToList();

            var dt1 = (from dt in data.DThuocs
                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                       select new { dt.MaBNhan, dtct.MaDV, dtct.MaKP, dtct.SoLuong }).ToList();
            var dv1 = (from dv in data.DichVus.Where(p => p.PLoai == 2)
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join ndv in data.NhomDVs on tn.IDNhom equals ndv.IDNhom
                       select new { dv.MaDV, dv.TenDV, dv.Loai, tn.TenRG, ndv.TenNhomCT }).ToList();

            var qcls1 = (from bn in bn1
                         join dt in dt1 on bn.MaBNhan equals dt.MaBNhan
                         join dv in dv1 on dt.MaDV equals dv.MaDV
                         select new { dv.TenRG, dv.Loai, bn.MaBNhan, dt.MaKP, dt.SoLuong, dv.TenDV, bn.NoiTru, bn.DTNT, bn.DTuong, dv.TenNhomCT }).ToList();

            var qcls = (from k in khoa
                        join c in qcls1 on k.makp equals c.MaKP
                        select c).ToList();

            var vp1 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                       join vpct in data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                       select new { vpct.TienBH, vpct.TienBN, vpct.ThanhTien, vpct.Mien, vp.MaBNhan, vpct.MaKP }).ToList();

            var qvp = (from k in khoa
                       join vpct in vp1 on k.makp equals vpct.MaKP
                       join bn in bn1 on vpct.MaBNhan equals bn.MaBNhan
                       select new { vpct.TienBH, vpct.TienBN, bn.NoiTru, vpct.ThanhTien, bn.DTuong, vpct.Mien, bn.MaDTuong }).ToList();
            #endregion
            #region tổng số lần khám bệnh
            nd = new noidung();
            nd.Stt = "I";
            nd.tieude = "Công tác khám chữa bệnh";
            nd.donvi = "";
            _lnoidung.Add(nd);

            nd = new noidung();
            nd.Stt = "*";
            nd.tieude = "Tổng số lượt khám bệnh";
            nd.donvi = "Người";
            _lnoidung.Add(nd);

            nd = new noidung();
            nd.Stt = "1";
            nd.tieude = "Tổng số lần khám bệnh";
            nd.donvi = "Người";
            nd.thuchien = qkb.Where(p => p.PLoai.ToLower().Contains("phòng khám")).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);

            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số lần KB miễn giảm VP";
            nd.donvi = "Lần";
            nd.congdon = 0;
            _lnoidung.Add(nd);

            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Số lần khám ngoại viện";
            nd.donvi = "Lần";
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region Số lần khám trẻ < 6
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Số lần khám trẻ < 6 tuổi";
            nd.donvi = "Lần";
            nd.thuchien = qkb.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => p.Tuoi < 6 && p.NoiTru == 0 && p.DTNT == false).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số điều trị < 6
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số ĐT < 6 tuổi";
            nd.donvi = "Người";
            nd.thuchien = qkb.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => p.Tuoi < 6 && p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số khám người cao tuổi (>60 tuổi)
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số khám người cao tuổi(>60 tuổi)";
            nd.donvi = "Người";
            nd.thuchien = qkb.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => p.Tuoi > 60 && p.NoiTru == 0 && p.DTNT == false).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số điều trị người cao tuổi (>60 tuổi)
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số ĐT người cao tuổi(>60 tuổi)";
            nd.donvi = "Người";
            nd.thuchien = qkb.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => p.Tuoi > 60 && p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số khám ND
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số khám ND";
            nd.donvi = "Người";
            nd.thuchien = qkb.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => p.DTuong.ToLower().Contains("dịch vụ") && p.NoiTru == 0 && p.DTNT == false).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số bn đt nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số BN ĐT nội trú";
            nd.donvi = "Người";
            nd.thuchien = qksk.Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region nội trú BH
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Nội trú BH";
            nd.donvi = "Người";
            nd.thuchien = qdt.Where(p => p.NgayVao != null && p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.NoiTru == 1 && p.DTuong.ToLower().Contains("bhyt")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region nội trú ND
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Nội trú ND";
            nd.donvi = "Người";
            nd.thuchien = qdt.Where(p => p.NgayVao != null && p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.NoiTru == 1 && p.DTuong.ToLower().Contains("dịch vụ")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region khám sức khỏe
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Khám sức khỏe";
            nd.donvi = "Người";
            nd.thuchien = qksk.Where(p => p.DTuong.ToLower().Contains("ksk") || p.DTuong.ToLower().Contains("khám sức khỏe")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số ngày đt nôi trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số ngày ĐT nội trú";
            nd.donvi = "Ngày";
            nd.thuchien = qdt.Where(p => p.NgayVao != null && p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.NoiTru == 1).Sum(p => p.SoNgaydt);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số ngày đt ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số ngày ĐT ngoại trú";
            nd.donvi = "Ngày";
            nd.thuchien = qdt.Where(p => p.NgayVao != null && p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số bn ra viện
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số BN ra viện";
            nd.donvi = "Người";
            nd.thuchien = qkb1.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số bn ra viện nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Trong đó: + Ra viện nội trú";
            nd.donvi = "Người";
            nd.thuchien = qkb1.Where(p => p.NoiTru == 1 && p.NgayKham >= tungay && p.NgayKham <= denngay).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số bn ra viện ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "           + Ra viện ngoại trú";
            nd.donvi = "Người";
            nd.thuchien = qkb1.Where(p => p.NoiTru != 1 && p.NgayKham >= tungay && p.NgayKham <= denngay).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số bn lưu tháng sau
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số BN lưu tháng sau";
            nd.donvi = "Người";
            nd.thuchien = qksk.Where(p => !rv1.Select(o => o.MaBNhan).Contains(p.MaBNhan)).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region lưu nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Trong đó: + Lưu nội trú";
            nd.donvi = "Người";
            nd.thuchien = qksk.Where(p => p.NoiTru == 1 && !rv1.Select(o => o.MaBNhan).Contains(p.MaBNhan)).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region lưu ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "           + Lưu ngoại trú";
            nd.donvi = "Người";
            nd.thuchien = qksk.Where(p => p.NoiTru != 1 && !rv1.Select(o => o.MaBNhan).Contains(p.MaBNhan)).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số bn chuyển viện
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số BN chuyển viện";
            nd.donvi = "Người";
            nd.thuchien = qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.Status == 1 && p.NoiTru == 0).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region chuyển BH
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Trong đó: + Chuyển BH";
            nd.donvi = "Người";
            nd.thuchien = qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.Status == 1 && p.NoiTru == 0 && p.DTuong.ToLower().Contains("bhyt")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region chuyển ND
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "           + Chuyển ND";
            nd.donvi = "Người";
            nd.thuchien = qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.Status == 1 && p.NoiTru == 0 && p.DTuong.ToLower().Contains("dịch vụ")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số bn chuyển viện tại khoa
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số BN chuyển viện tại khoa";
            nd.donvi = "Người";
            nd.thuchien = qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.Status == 1 && p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng số bn đẻ
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số BN đẻ";
            nd.donvi = "Người";
            nd.thuchien = qkb.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).Where(p => p.ChanDoan.ToLower().Contains("đẻ") || p.ChanDoan.ToLower().Contains("mổ lấy thai")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region sơ sinh nam
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Trong đó: + Sơ sinh nam";
            nd.donvi = "Người";
            //nd.thuchien = qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.Status == 1 && p.NoiTru == 0 && p.DTNT == false && p.DTuong.ToLower().Contains("bhyt")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region sơ sinh nữ
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "           + Sơ sinh nữ";
            nd.donvi = "Người";
            //nd.thuchien = qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.Status == 1 && p.NoiTru == 0 && p.DTNT == false && p.DTuong.ToLower().Contains("dịch vụ")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tử vong trước 24h
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tổng số BN tử vong: + Trước 24h";
            nd.donvi = "Người";
            //nd.thuchien = qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.Status == 1 && p.NoiTru == 0 && p.DTNT == false && p.DTuong.ToLower().Contains("bhyt")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tử vong sau 24h
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "                    + Sau 24h";
            nd.donvi = "Người";
            nd.thuchien = qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.KetQua != null && p.KetQua.Equals("Tử vong")).Select(p => p.MaBNhan).Distinct().Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region ngày đt trung bình /BN
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Ngày ĐT trung bình 1/BN";
            nd.donvi = "Ngày/Người";
            nd.thuchien = (qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Select(p => p.MaBNhan).Distinct().Count() != 0) ?
                          (double)qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Sum(p => p.SoNgaydt) / qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Select(p => p.MaBNhan).Distinct().Count() : 0;
            nd.thuchien = Math.Round(nd.thuchien.GetValueOrDefault(), 2);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region ngày đt trung bình /BN nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Trong đó: + Nội trú";
            nd.donvi = "Ngày/Người";
            nd.thuchien = (qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count() != 0) ?
                          (double)qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 1).Sum(p => p.SoNgaydt) / qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 1).Select(p => p.MaBNhan).Distinct().Count() : 0;
            nd.thuchien = Math.Round(nd.thuchien.GetValueOrDefault(), 2);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region ngày đt trung bình /BN ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "           + Ngoại trú";
            nd.donvi = "Ngày/Người";
            nd.thuchien = (qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count() != 0) ?
                          (double)qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt) / qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count() : 0;
            nd.thuchien = Math.Round(nd.thuchien.GetValueOrDefault(), 2);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region ngày sử dụng giường bệnh
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Ngày sử dụng giường bệnh";
            nd.donvi = "Ngày/tháng";
            //nd.thuchien = (qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count() != 0) ?
            //              (double)qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt) / qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count() : 0;
            //nd.thuchien = Math.Round(nd.thuchien.GetValueOrDefault(), 2);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region công suất sử dụng giường bệnh
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Công suất sử dụng giường bệnh";
            nd.donvi = "%";
            //nd.thuchien = (qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count() != 0) ?
            //              (double)qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt) / qdt.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.NoiTru == 0 && p.DTNT == true).Select(p => p.MaBNhan).Distinct().Count() : 0;
            //nd.thuchien = Math.Round(nd.thuchien.GetValueOrDefault(), 2);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion

            #region II. tổng số lần xét nghiệm
            nd = new noidung();
            nd.Stt = "II";
            nd.tieude = "Tổng số lần xét nghiệm";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region sinh hóa
            nd = new noidung();
            nd.Stt = "1";
            nd.tieude = "Sinh hóa";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau)).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region sinh hóa nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Nội trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) && p.NoiTru == 1).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region sinh hóa ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Ngoại trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau) && p.NoiTru == 0).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region XN huyết học
            nd = new noidung();
            nd.Stt = "2";
            nd.tieude = "XN huyết học";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region XN huyết học nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Nội trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc) && p.NoiTru == 1).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region XN huyết học ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Ngoại trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc) && p.NoiTru == 0).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region XN vi khuẩn
            nd = new noidung();
            nd.Stt = "3";
            nd.tieude = "XN vi khuẩn";
            nd.donvi = "Lần";
            //nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc)).Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region XN vi khuẩn nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Nội trú";
            nd.donvi = "Lần";
            //nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc) && p.NoiTru == 1).Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region XN vi khuẩn ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Ngoại trú";
            nd.donvi = "Lần";
            //nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc) && p.NoiTru == 0).Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region XN HIV
            nd = new noidung();
            nd.Stt = "4";
            nd.tieude = "Tổng số XN HIV";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenDV.ToLower().Contains("xét nghiệm hiv")).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region XN morphin
            nd = new noidung();
            nd.Stt = "5";
            nd.tieude = "Xét nghiệm Morphin";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenDV.ToLower().Contains("xét nghiệm morphin")).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region XN HBsAg
            nd = new noidung();
            nd.Stt = "6";
            nd.tieude = "Xét nghiệm HBsAg";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenNhomCT.ToLower().Contains("xét nghiệm")).Where(p => p.TenDV.ToLower().Contains("hbsag test nhanh")).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region X-quang
            nd = new noidung();
            nd.Stt = "7";
            nd.tieude = "Tổng số lần X-quang";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang)).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region X-quang nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + X-quang nội trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang) && p.NoiTru == 1).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region X-quang ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + X-quang nội trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang) && p.NoiTru == 0).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region điện tim
            nd = new noidung();
            nd.Stt = "8";
            nd.tieude = "Tổng số lần điện tim";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim)).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region điện tim nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Điện tim nội trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim) && p.NoiTru == 1).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region điện tim ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Điện tim nội trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim) && p.NoiTru == 0).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region siêu âm
            nd = new noidung();
            nd.Stt = "9";
            nd.tieude = "Tổng số lần siêu âm";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region siêu âm nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Siêu âm nội trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm) && p.NoiTru == 1).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region siêu âm ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Siêu âm ngoại trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm) && p.NoiTru == 0).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region nội soi TMH
            nd = new noidung();
            nd.Stt = "10";
            nd.tieude = "Tổng số lần nội soi TMH";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenDV.ToLower().Contains("nội soi tai mũi họng")).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region nội soi TMH nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Nội trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenDV.ToLower().Contains("nội soi tai mũi họng") && p.NoiTru == 1).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region nội soi TMH ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Ngoại trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenDV.ToLower().Contains("nội soi tai mũi họng") && p.NoiTru == 0).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region luu huyết não
            nd = new noidung();
            nd.Stt = "11";
            nd.tieude = "Tổng số lưu huyết não";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao)).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region lưu huyết não nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Nội trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao) && p.NoiTru == 1).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region lưu huyết não ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Ngoại trú";
            nd.donvi = "Lần";
            nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao) && p.NoiTru == 0).Sum(p => p.SoLuong);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region nội soi tiêu hóa
            nd = new noidung();
            nd.Stt = "12";
            nd.tieude = "Tổng số nội soi tiêu hóa";
            nd.donvi = "Lần";
            //nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao)).Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region nội soi tiêu hóa nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Nội trú";
            nd.donvi = "Lần";
            //nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao) && p.NoiTru == 1).Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region nội soi tiêu hóa ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Ngoại trú";
            nd.donvi = "Lần";
            //nd.thuchien = qcls.Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao) && p.NoiTru == 0).Count();
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion

            #region III. công tác tài chính
            nd = new noidung();
            nd.Stt = "III";
            nd.tieude = "Công tác tài chính";
            nd.donvi = "Nghìn đồng";
            nd.thuchien = qvp.Where(p => p.DTuong.ToLower().Contains("dịch vụ") || p.DTuong.ToLower().Contains("bhyt")).Sum(p => p.TienBN);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng thu viện phí ND
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tông thu viện phí ND";
            nd.donvi = "Nghìn đồng";
            nd.thuchien = qvp.Where(p => p.DTuong.ToLower().Contains("dịch vụ")).Sum(p => p.TienBN);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng viện phí BHYT
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Tông viện phí BHYT";
            nd.donvi = "Nghìn đồng";
            nd.thuchien = qvp.Where(p => p.DTuong.ToLower().Contains("bhyt")).Sum(p => p.TienBN);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng viện phí BHYT nội trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Nội trú";
            nd.donvi = "Nghìn đồng";
            nd.thuchien = qvp.Where(p => p.DTuong.ToLower().Contains("bhyt") && p.NoiTru == 1).Sum(p => p.TienBN);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region tổng viện phí BHYT ngoại trú
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = " + Ngoại trú";
            nd.donvi = "Nghìn đồng";
            nd.thuchien = qvp.Where(p => p.DTuong.ToLower().Contains("bhyt") && p.NoiTru == 0).Sum(p => p.TienBN);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion

            #region IV. tổng số miễn viện phí
            nd = new noidung();
            nd.Stt = "IV";
            nd.tieude = "Tổng số miễn viện phí";
            nd.donvi = "Nghìn đồng";
            nd.thuchien = qvp.Where(p => p.Mien == 100).Sum(p => p.TienBH);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region miễn viện phí người nghèo
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Miễn viện phí người nghèo";
            nd.donvi = "Nghìn đồng";
            nd.thuchien = qvp.Where(p => p.Mien == 100 && p.MaDTuong.Equals("HN")).Sum(p => p.TienBH);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region miễn phí khác
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Miễn phí khác";
            nd.donvi = "Nghìn đồng";
            nd.thuchien = qvp.Where(p => p.Mien == 100 && !p.MaDTuong.Equals("HN") && (!p.DTuong.ToLower().Contains("ksk") || !p.DTuong.ToLower().Contains("khám sức khỏe"))).Sum(p => p.TienBH);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion
            #region miễn viện phí khám sức khỏe
            nd = new noidung();
            nd.Stt = "";
            nd.tieude = "Miễn viện phí khám sức khỏe";
            nd.donvi = "Nghìn đồng";
            nd.thuchien = qvp.Where(p => p.Mien == 100 && (p.DTuong.ToLower().Contains("ksk") || p.DTuong.ToLower().Contains("khám sức khỏe"))).Sum(p => p.TienBH);
            nd.congdon = 0;
            _lnoidung.Add(nd);
            #endregion

            #region  Xuat excel
            string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@" };
            string[] _tieude = { "Stt", "nội dung", "đơn vị tính", "kh", "thực hiện", "cộng dồn", "%KH năm" };
            string _filePath = "C:\\" + "BC_CongTacKhamChuaBenh.xls";
            int[] _arrWidth = new int[] { };
            var qexcel = _lnoidung;
            DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 7];
            for (int i = 0; i < 7; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToUpper();
            }
            int num = 1;
            foreach (var r in qexcel)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = r.Stt;
                DungChung.Bien.MangHaiChieu[num, 1] = r.tieude;
                DungChung.Bien.MangHaiChieu[num, 2] = r.donvi;
                DungChung.Bien.MangHaiChieu[num, 3] = r.kehoach;
                DungChung.Bien.MangHaiChieu[num, 4] = r.thuchien;
                DungChung.Bien.MangHaiChieu[num, 5] = r.congdon;
                DungChung.Bien.MangHaiChieu[num, 6] = r.phantram;
                num++;
            }
            //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Công tác khám chữa bệnh", _filePath, true);
            #endregion

            BaoCao.rep_BC_CongTacKhamChuaBenh_26007 rep = new BaoCao.rep_BC_CongTacKhamChuaBenh_26007();
            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Công tác khám chữa bệnh", _filePath, true, this.Name);
            rep.lblThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.DataSource = _lnoidung;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_lKhoa.First().chon == true)
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoa)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoa.ToList();
                    }
                }
            }
        }
    }
}