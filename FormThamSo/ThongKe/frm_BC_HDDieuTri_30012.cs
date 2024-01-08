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
    public partial class frm_BC_HDDieuTri_30012 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_HDDieuTri_30012()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BC_HDDieuTri_30009_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            List<KPhong> kp = (from n in data.KPhongs.Where(p => p.PLoai == "Lâm sàng")
                               select n).ToList();
            kp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKho.Properties.DataSource = kp;
            lupKho.Properties.DisplayMember = "TenKP";
            lupKho.Properties.ValueMember = "MaKP";
            ck_KhoaYHCT.Enabled = false;
        }

        public bool KiemTraBC()
        {
            if (lupTuNgay.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn ngày bắt đầu.");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn ngày kết thúc.");
                lupDenNgay.Focus();
                return false;
            }
            if (Convert.ToDateTime(lupDenNgay.Text.Trim()) < Convert.ToDateTime(lupTuNgay.Text.Trim()))
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bát đầu in báo cáo.");
                lupDenNgay.Focus();
                return false;
            }
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Chưa chọn Khoa.");
                lupKho.Focus();
                return false;
            }
            else return true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            List<HDDieuTri> _lHD = new List<HDDieuTri>();
            HDDieuTri _hdMoi = new HDDieuTri();
            if (KiemTraBC())
            {
                int maKhoa = 0;
                if (lupKho.EditValue != null)
                    maKhoa = Convert.ToInt32(lupKho.EditValue);
                string khoa = lupKho.Properties.GetDisplayText(maKhoa);
                #region query
                //tổng người bệnh điều trị(nội trú + đt ngoại trú)
                var bnDieuTri = (from bn in data.BenhNhans
                                 join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                 join rv in data.RaViens on vv.MaBNhan equals rv.MaBNhan into kq
                                 from kq1 in kq.DefaultIfEmpty()
                                 select new
                                 {
                                     bn.MaBNhan,
                                     vv.NgayVao,
                                     kq1.NgayRa,
                                     kq1.SoNgaydt,
                                     vv.MaKP,
                                     kq1.Status,
                                     KetQua = kq1.KetQua == null ? "" : kq1.KetQua,
                                     bn.Tuoi,
                                     bn.CapCuu,
                                     bn.SThe,
                                     bn.GTinh,
                                     bn.NoiTru,
                                     bn.DTNT
                                 }).Where(p => maKhoa == 0 || p.MaKP == maKhoa)
                                   .Where(p => (p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)) ||
                                                (p.NgayVao >= tungay && p.NgayVao <= denngay) ||
                                                (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null)))
                                   .Distinct().ToList();
                //BN đẻ = đẻ thường + đẻ mổ
                var bnDe = (from dt in data.DThuocs
                            join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                            join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                            //join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan into qcls
                            //from qcls1 in qcls.DefaultIfEmpty()
                            join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                            join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                            from kq1 in kq.DefaultIfEmpty()
                            select new
                            {
                                bn.Tuoi,
                                bn.SThe,
                                bn.GTinh,
                                bn.CapCuu,
                                bn.NoiTru,
                                bn.DTNT,
                                NgayTH = dtct.NgayNhap,
                                dv.TenDV,
                                KetQua = kq1 == null ? "" : kq1.KetQua,
                                SoNgaydt = kq1 == null ? 0 : kq1.SoNgaydt,
                                vv.MaKP
                            }).Where(p => p.TenDV.ToLower().Contains("đỡ đẻ") || p.TenDV.ToLower().Contains("phẫu thuật lấy thai"))
                              .Where(p => maKhoa == 0 || p.MaKP == maKhoa).Where(p => (p.NgayTH >= tungay && p.NgayTH <= denngay)).ToList();
                //bn miễn giảm viện phí
                var bnMienGiam = (from bn in data.BenhNhans
                                  join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                                  join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                                  from kq1 in kq.DefaultIfEmpty()
                                  join dtbn in data.DTBNs on bn.IDDTBN equals dtbn.IDDTBN into q
                                  from q1 in q.DefaultIfEmpty()
                                  select new
                                  {
                                      bn.MaBNhan,
                                      vv.NgayVao,
                                      kq1.SoNgaydt,
                                      vv.MaKP,
                                      bn.Tuoi,
                                      bn.CapCuu,
                                      bn.SThe,
                                      bn.GTinh,
                                      bn.NoiTru,
                                      bn.DTNT,
                                      bn.MaDTuong,
                                      q1.DTBN1,
                                      q1.HTTT
                                  }).Where(p => maKhoa == 0 || p.MaKP == maKhoa).Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay))
                                   .Where(p => p.MaDTuong.Trim().Equals("TE") || p.MaDTuong.Trim().Equals("HN") || p.HTTT == 0).Distinct().ToList();
                //điều trị bằng YHCT hoặc kết hợp YHCT
                //var bnYHCT = (from bn in data.BenhNhans
                //              join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                //              join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                //              from kq1 in kq.DefaultIfEmpty()
                //              join kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng") on vv.MaKP equals kp.MaKP
                //              join ck in data.ChuyenKhoas.Where(p => p.TenCK.Equals("Đông y") || p.TenCK.Trim().Contains("YHCT")) on kp.MaCK equals ck.MaCK
                //              select new
                //              {
                //                  bn.MaBNhan,
                //                  kq1.NgayRa,
                //                  kq1.SoNgaydt,
                //                  bn.Tuoi,
                //                  bn.CapCuu,
                //                  bn.SThe,
                //                  bn.GTinh,
                //                  bn.NoiTru,
                //                  bn.DTNT,
                //                  vv.NgayVao,
                //                  vv.MaKP
                //              }).Where(p => (p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)) ||
                //                                (p.NgayVao >= tungay && p.NgayVao <= denngay) ||
                //                                (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null)))
                //                .Where(p => maKhoa == 0 || p.MaKP == maKhoa).Distinct().ToList();
                var kpdongy = (from kp in data.KPhongs join ck in data.ChuyenKhoas.Where(p => p.TenCK.Equals("Đông y") || p.TenCK.Trim().Contains("YHCT")) on kp.MaCK equals ck.MaCK select kp).ToList();
                var bnYHCT1 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                              join rv in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay) on bn.MaBNhan equals rv.MaBNhan
                              join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                              select new { bn.MaBNhan, bn.NoiTru, bn.DTNT, bn.GTinh, bn.Tuoi, bn.CapCuu, bn.SThe, rv.SoNgaydt, bnkb.MaKP }).Distinct().ToList();
                var bnYHCT = (from kp in kpdongy
                              join bn in bnYHCT1 on kp.MaKP equals bn.MaKP
                              select bn).ToList();
                //phẫu thuật
                var bnPhauThuat = (from cls in data.CLS
                                   join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                   join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                   join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                   join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                   group new { cls, bn, cd, dv, tn } by new
                                   {
                                       tn.TenRG,
                                       cls.NgayTH,
                                       cls.MaBNhan,
                                       cls.MaKP,
                                       dv.TenDV,
                                       dv.Loai,
                                       bn.Tuoi,
                                       bn.CapCuu,
                                       bn.SThe,
                                       bn.GTinh,
                                       bn.NoiTru,
                                       bn.DTNT
                                   } into q
                                   select new
                                   {
                                       q.Key.TenRG,
                                       q.Key.NgayTH,
                                       q.Key.MaBNhan,
                                       q.Key.MaKP,
                                       q.Key.TenDV,
                                       q.Key.Loai,
                                       q.Key.Tuoi,
                                       q.Key.CapCuu,
                                       q.Key.SThe,
                                       q.Key.GTinh,
                                       q.Key.NoiTru,
                                       q.Key.DTNT
                                   }).Where(p => (p.NgayTH >= tungay && p.NgayTH <= denngay))
                                     .Where(p => maKhoa == 0 || p.MaKP == maKhoa).Where(p => p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat) || p.TenRG.Equals(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat)).ToList();
                #region tạm thời bỏ
                //var bnPhauThuat = (from cd in data.ChiDinhs
                //                   join dv in data.DichVus.Where(p => p.Loai == 1 || p.Loai == 2 || p.Loai == 3) on cd.MaDV equals dv.MaDV
                //                   join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                //                   join dtct in data.DThuoccts on cd.IDCD equals dtct.IDCD
                //                   join cls in data.CLS on cd.IdCLS equals cls.IdCLS
                //                   join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                //                   group new { cd, dv, tn, dtct, cls, bn } by new
                //                   {
                //                       cd.Status,
                //                       tn.TenRG,
                //                       dv.Loai,
                //                       dtct.IDCD,
                //                       cls.NgayTH,
                //                       cls.MaBNhan,
                //                       cls.MaKP,
                //                       dv.TenDV,
                //                       bn.Tuoi,
                //                       bn.CapCuu,
                //                       bn.SThe,
                //                       bn.GTinh,
                //                       bn.NoiTru,
                //                       bn.DTNT
                //                   } into q
                //                   select new
                //                   {
                //                       q.Key.Status,
                //                       q.Key.TenRG,
                //                       q.Key.Loai,
                //                       q.Key.IDCD,
                //                       q.Key.NgayTH,
                //                       q.Key.MaBNhan,
                //                       q.Key.MaKP,
                //                       q.Key.TenDV,
                //                       q.Key.Tuoi,
                //                       q.Key.CapCuu,
                //                       q.Key.SThe,
                //                       q.Key.GTinh,
                //                       q.Key.NoiTru,
                //                       q.Key.DTNT
                //                   }).Where(p => (p.NgayTH >= tungay && p.NgayTH <= denngay))
                //                     .Where(p => p.Status == 1 && p.IDCD != null).Where(p => maKhoa == 0 || p.MaKP == maKhoa).ToList();
                #endregion
                var bnPTRaVien = (from n in bnPhauThuat
                                  join rv in data.RaViens on n.MaBNhan equals rv.MaBNhan into k
                                  from k1 in k.DefaultIfEmpty()
                                  group new { n, k1 } by new
                                  {
                                      n.MaBNhan,
                                      n.NoiTru,
                                      n.DTNT,
                                      n.TenRG,
                                      SoNgaydt = k1 == null ? 0 : k1.SoNgaydt
                                  } into kq
                                  select new
                                  {
                                      kq.Key.MaBNhan,
                                      kq.Key.NoiTru,
                                      kq.Key.DTNT,
                                      kq.Key.TenRG,
                                      SoNgaydt = kq.Key.SoNgaydt
                                  }).ToList();
                var bnPTRaVienTheoLoai = (from n in bnPhauThuat
                                          join rv in data.RaViens on n.MaBNhan equals rv.MaBNhan into k
                                          from k1 in k.DefaultIfEmpty()
                                          select new
                                          {
                                              n.NoiTru,
                                              n.DTNT,
                                              n.TenRG,
                                              n.Loai,
                                              SoNgaydt = k1 == null ? 0 : k1.SoNgaydt
                                          }).ToList();
                #endregion
                #region 1. Người bệnh nằm điều trị
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 1;
                _hdMoi.TenNhom = "Người bệnh nằm điều trị";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.Duoi1T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").GroupBy(p => p.MaBNhan).Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").GroupBy(p => p.MaBNhan).Count();
                _hdMoi.Nu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 2. Người bệnh cũ tồn
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 2;
                _hdMoi.TenNhom = "Người bệnh cũ tồn";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 1 && p.DTNT == false).Count();
                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => (p.NgayVao <= tungay && p.NgayRa >= tungay && p.NgayRa <= denngay) || (p.NgayVao <= tungay && p.NgayRa == null)).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Count();
                _hdMoi.Nu = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => ((p.NgayVao <= tungay && ((p.NgayRa >= tungay && p.NgayRa <= denngay) || p.NgayRa == null)))).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 3. Người bệnh mới vào
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 3;
                _hdMoi.TenNhom = "Người bệnh mới vào.";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 1 && p.DTNT == false).Count();
                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Count();
                _hdMoi.Nu = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay)).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 4. Người bệnh mới tồn
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 4;
                _hdMoi.TenNhom = "Người bệnh mới tồn.";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 1 && p.DTNT == false).Count();
                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Count();
                _hdMoi.Nu = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => (p.NgayVao >= tungay && p.NgayVao <= denngay && (p.NgayRa >= denngay || p.NgayRa == null))).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 5. Người bệnh ra viện
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 5;
                _hdMoi.TenNhom = "Người bệnh ra viện.";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Count();
                _hdMoi.Duoi1T = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Count();
                _hdMoi.Nu = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 6. Người bệnh chuyển viện
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 6;
                _hdMoi.TenNhom = "Người bệnh chuyển viện.";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 1 && p.DTNT == false).Count();
                _hdMoi.Duoi1T = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Count();
                _hdMoi.Nu = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.Status == 1).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 8. Tổng số ngày người bệnh ra viện nằm điều trị
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 8;
                _hdMoi.TenNhom = "Tổng số ngày người bệnh ra viện nằm điều trị.";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                _hdMoi.Duoi1T = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Sum(p => p.SoNgaydt);
                _hdMoi.Duoi6T = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Sum(p => p.SoNgaydt);
                _hdMoi.Duoi15T = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Sum(p => p.SoNgaydt);
                _hdMoi.CapCuu = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Sum(p => p.SoNgaydt);
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Sum(p => p.SoNgaydt);
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Sum(p => p.SoNgaydt);
                _hdMoi.Nu = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Sum(p => p.SoNgaydt);
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 9. Tổng số lượt người bệnh khỏi
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 9;
                _hdMoi.TenNhom = "Tổng số lượt người bệnh khỏi.";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Khỏi")).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.Duoi1T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.KetQua.Equals("Khỏi")).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.KetQua.Equals("Khỏi")).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.KetQua.Equals("Khỏi")).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.KetQua.Equals("Khỏi")).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Khỏi")).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.KetQua.Equals("Khỏi")).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.KetQua.Equals("Khỏi")).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.Nu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.KetQua.Equals("Khỏi")).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Khỏi")).GroupBy(p => p.MaBNhan).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Khỏi")).Sum(p => p.SoNgaydt);

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 10. Tổng số lượt người bệnh đỡ/giảm
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 10;
                _hdMoi.TenNhom = "Tổng số lượt người bệnh đỡ/giảm.";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                _hdMoi.Duoi1T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Đỡ|Giảm")).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                _hdMoi.Nu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Đỡ|Giảm")).Count();

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 11. Tổng số lượt người bệnh không thay đổi
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 11;
                _hdMoi.TenNhom = "Tổng số lượt người bệnh không thay đổi.";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                _hdMoi.Duoi1T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Không T.đổi")).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                _hdMoi.Nu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Không T.đổi")).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Không T.đổi")).Count();

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 12. Tổng số lượt người bệnh nặng lên
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 12;
                _hdMoi.TenNhom = "Tổng số lượt người bệnh nặng lên.";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                _hdMoi.Duoi1T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                _hdMoi.Duoi6T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                _hdMoi.Duoi15T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                _hdMoi.CapCuu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Nặng hơn")).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                _hdMoi.Nu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Nặng hơn")).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Nặng hơn")).Count();

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion
                #region 19. Tổng số lượt điều trị cho người cao tuổi (>60 tuổi)
                _hdMoi = new HDDieuTri();
                _hdMoi.Stt = 19;
                _hdMoi.TenNhom = "Tổng số lượt điều trị cho người cao tuổi (>60 tuổi).";
                _hdMoi.TenChiTiet = "";
                _hdMoi.TongSo = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi >= 60).Count();

                _hdMoi.CapCuu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.Tuoi >= 60).Count();
                _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi >= 60).Sum(p => p.SoNgaydt);
                _hdMoi.CoBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.Tuoi >= 60).Count();
                _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.Tuoi >= 60).Count();
                _hdMoi.Nu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.Tuoi >= 60).Count();
                _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi >= 60).Count();
                _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.Tuoi >= 60).Sum(p => p.SoNgaydt);

                _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                _lHD.Add(_hdMoi);
                #endregion

                if (ck_KhoaYHCT.Checked == false)
                {
                    #region 7. Tổng số ngày người bệnh nằm điều trị
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 7;
                    _hdMoi.TenNhom = "Tổng số ngày người bệnh nằm điều trị.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                    _hdMoi.Duoi1T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Sum(p => p.SoNgaydt);
                    _hdMoi.Duoi6T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Sum(p => p.SoNgaydt);
                    _hdMoi.Duoi15T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Sum(p => p.SoNgaydt);
                    _hdMoi.CapCuu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Sum(p => p.SoNgaydt);
                    _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Sum(p => p.SoNgaydt);
                    _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Sum(p => p.SoNgaydt);
                    _hdMoi.Nu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Sum(p => p.SoNgaydt);
                    _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);
                    _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 13. Đẻ
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 13;
                    _hdMoi.TenNhom = "Đẻ.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnDe.Where(p => p.NoiTru == 1 && p.DTNT == false).Count();
                    _hdMoi.Duoi1T = bnDe.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Count();
                    _hdMoi.Duoi6T = bnDe.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count();
                    _hdMoi.Duoi15T = bnDe.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count();
                    _hdMoi.CapCuu = bnDe.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Count();
                    _hdMoi.SoNgayDT = bnDe.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnDe.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                    _hdMoi.KhongBHYT = bnDe.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Count();
                    _hdMoi.Nu = bnDe.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Count();
                    _hdMoi.SoNguoiDTNT = bnDe.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                    _hdMoi.SoNgayDTNT = bnDe.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 14. Sơ sinh sống
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 14;
                    _hdMoi.TenNhom = "Sơ sinh sống.";
                    _hdMoi.TenChiTiet = "";
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 15. Sơ sinh < 2500g
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 15;
                    _hdMoi.TenNhom = "Sơ sinh < 2500g.";
                    _hdMoi.TenChiTiet = "";
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 16. Người bệnh miễn giảm viện phí
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 16;
                    _hdMoi.TenNhom = "Người bệnh miễn giảm viện phí.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Count();
                    _hdMoi.Duoi1T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Count();
                    _hdMoi.Duoi6T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count();
                    _hdMoi.Duoi15T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count();
                    _hdMoi.CapCuu = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Count();
                    _hdMoi.SoNgayDT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                    _hdMoi.KhongBHYT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Count();
                    _hdMoi.Nu = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Count();
                    _hdMoi.SoNguoiDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                    _hdMoi.SoNgayDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 16;
                    _hdMoi.TenNhom = "Người bệnh miễn giảm viện phí.";
                    _hdMoi.TenChiTiet = "Trong đó:";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 16;
                    _hdMoi.TenNhom = "Người bệnh miễn giảm viện phí.";
                    _hdMoi.TenChiTiet = "- Trẻ em < 6T";
                    _hdMoi.TongSo = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.MaDTuong.Trim().Equals("TE")).Count();
                    _hdMoi.Duoi1T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.MaDTuong.Trim().Equals("TE")).Count();
                    _hdMoi.Duoi6T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.MaDTuong.Trim().Equals("TE")).Count();
                    _hdMoi.Duoi15T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.MaDTuong.Trim().Equals("TE")).Count();
                    _hdMoi.CapCuu = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.MaDTuong.Trim().Equals("TE")).Count();
                    _hdMoi.SoNgayDT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.MaDTuong.Trim().Equals("TE")).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.MaDTuong.Trim().Equals("TE")).Count();
                    _hdMoi.KhongBHYT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.MaDTuong.Trim().Equals("TE")).Count();
                    _hdMoi.Nu = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.MaDTuong.Trim().Equals("TE")).Count();
                    _hdMoi.SoNguoiDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong.Trim().Equals("TE")).Count();
                    _hdMoi.SoNgayDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong.Trim().Equals("TE")).Count();

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 16;
                    _hdMoi.TenNhom = "Người bệnh miễn giảm viện phí.";
                    _hdMoi.TenChiTiet = "- Người bệnh có giấy chứng nhận người nghèo";
                    _hdMoi.TongSo = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.MaDTuong.Trim().Equals("HN")).Count();
                    _hdMoi.Duoi1T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.MaDTuong.Trim().Equals("HN")).Count();
                    _hdMoi.Duoi6T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.MaDTuong.Trim().Equals("HN")).Count();
                    _hdMoi.Duoi15T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.MaDTuong.Trim().Equals("HN")).Count();
                    _hdMoi.CapCuu = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.MaDTuong.Trim().Equals("HN")).Count();
                    _hdMoi.SoNgayDT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.MaDTuong.Trim().Equals("HN")).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.MaDTuong.Trim().Equals("HN")).Count();
                    _hdMoi.KhongBHYT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.MaDTuong.Trim().Equals("HN")).Count();
                    _hdMoi.Nu = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.MaDTuong.Trim().Equals("HN")).Count();
                    _hdMoi.SoNguoiDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong.Trim().Equals("HN")).Count();
                    _hdMoi.SoNgayDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.MaDTuong.Trim().Equals("HN")).Count();

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 16;
                    _hdMoi.TenNhom = "Người bệnh miễn giảm viện phí.";
                    _hdMoi.TenChiTiet = "- Các đối tượng khác do bệnh viện xem xét quyết định";
                    _hdMoi.TongSo = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.HTTT == 0).Count();
                    _hdMoi.Duoi1T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.HTTT == 0).Count();
                    _hdMoi.Duoi6T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.HTTT == 0).Count();
                    _hdMoi.Duoi15T = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.HTTT == 0).Count();
                    _hdMoi.CapCuu = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.HTTT == 0).Count();
                    _hdMoi.SoNgayDT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.HTTT == 0).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.HTTT == 0).Count();
                    _hdMoi.KhongBHYT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.HTTT == 0).Count();
                    _hdMoi.Nu = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.HTTT == 0).Count();
                    _hdMoi.SoNguoiDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.HTTT == 0).Count();
                    _hdMoi.SoNgayDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.HTTT == 0).Count();

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 17. Tai biến trong điều trị
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "Trong đó:";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "- Số tai biến do sử dụng thuốc (nhầm lẫn)";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "- Tai biến do phản ứng có hại của thuốc (ADR)";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "- Tai biến do phẫu thuật";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "- Tai biến do thủ thuật";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "- Tai biến do truyền máu";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "- Số tai biến vì lý do khác";
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 18. Điều trị bằng YHCT hoặc kết hợp YHCT
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 18;
                    _hdMoi.TenNhom = "Điều trị bằng YHCT hoặc kết hợp YHCT.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Count();
                    _hdMoi.Duoi1T = bnYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Count();
                    _hdMoi.Duoi6T = bnYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count();
                    _hdMoi.Duoi15T = bnYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count();
                    _hdMoi.CapCuu = bnYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Count();
                    _hdMoi.SoNgayDT = bnYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                    _hdMoi.KhongBHYT = bnYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Count();
                    _hdMoi.Nu = bnYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Count();
                    _hdMoi.SoNguoiDTNT = bnYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                    _hdMoi.SoNgayDTNT = bnYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 20. Người bệnh tử vong
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 20;
                    _hdMoi.TenNhom = "Người bệnh tử vong.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Tử vong")).Count();
                    _hdMoi.Duoi1T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.KetQua.Equals("Tử vong")).Count();
                    _hdMoi.Duoi6T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.KetQua.Equals("Tử vong")).Count();
                    _hdMoi.Duoi15T = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.KetQua.Equals("Tử vong")).Count();
                    _hdMoi.CapCuu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.KetQua.Equals("Tử vong")).Count();
                    _hdMoi.SoNgayDT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.KetQua.Equals("Tử vong")).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.KetQua.Equals("Tử vong")).Count();
                    _hdMoi.KhongBHYT = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.KetQua.Equals("Tử vong")).Count();
                    _hdMoi.Nu = bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.KetQua.Equals("Tử vong")).Count();
                    _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Tử vong")).Count();
                    _hdMoi.SoNgayDTNT = bnDieuTri.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.KetQua.Equals("Tử vong")).Count();

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 21. Phẫu thuật
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 21;
                    _hdMoi.TenNhom = "Phẫu thuật.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
                    _hdMoi.Duoi1T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
                    _hdMoi.Duoi6T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
                    _hdMoi.Duoi15T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
                    _hdMoi.CapCuu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
                    _hdMoi.SoNgayDT = bnPTRaVien.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
                    _hdMoi.KhongBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
                    _hdMoi.Nu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
                    _hdMoi.SoNguoiDTNT = bnPhauThuat.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Count();
                    _hdMoi.SoNgayDTNT = bnPTRaVien.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 21;
                    _hdMoi.TenNhom = "Phẫu thuật.";
                    _hdMoi.TenChiTiet = "Trong đó:";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 21;
                    _hdMoi.TenNhom = "Phẫu thuật.";
                    _hdMoi.TenChiTiet = "- Phẫu thuật loại I";
                    _hdMoi.TongSo = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.Duoi1T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.Duoi6T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.Duoi15T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.CapCuu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.SoNgayDT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.KhongBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.Nu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.SoNguoiDTNT = bnPhauThuat.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.SoNgayDTNT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 1).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 21;
                    _hdMoi.TenNhom = "Phẫu thuật.";
                    _hdMoi.TenChiTiet = "- Phẫu thuật loại II";
                    _hdMoi.TongSo = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.Duoi1T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.Duoi6T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.Duoi15T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.CapCuu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.SoNgayDT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.KhongBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.Nu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.SoNguoiDTNT = bnPhauThuat.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.SoNgayDTNT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 2).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 21;
                    _hdMoi.TenNhom = "Phẫu thuật.";
                    _hdMoi.TenChiTiet = "- Phẫu thuật loại III";
                    _hdMoi.TongSo = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.Duoi1T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.Duoi6T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.Duoi15T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.CapCuu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.SoNgayDT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.KhongBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.Nu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.SoNguoiDTNT = bnPhauThuat.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.SoNgayDTNT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("phẫu thuật")).Where(p => p.Loai == 3).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 22. Thủ thuật
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 22;
                    _hdMoi.TenNhom = "Thủ thuật.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Count();
                    _hdMoi.Duoi1T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Count();
                    _hdMoi.Duoi6T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Count();
                    _hdMoi.Duoi15T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Count();
                    _hdMoi.CapCuu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Count();
                    _hdMoi.SoNgayDT = bnPTRaVien.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Count();
                    _hdMoi.KhongBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Count();
                    _hdMoi.Nu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Count();
                    _hdMoi.SoNguoiDTNT = bnPhauThuat.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Count();
                    _hdMoi.SoNgayDTNT = bnPTRaVien.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 22;
                    _hdMoi.TenNhom = "Thủ thuật.";
                    _hdMoi.TenChiTiet = "Trong đó:";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 22;
                    _hdMoi.TenNhom = "Thủ thuật.";
                    _hdMoi.TenChiTiet = "- Thủ thuật loại I";
                    _hdMoi.TongSo = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.Duoi1T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.Duoi6T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.Duoi15T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.CapCuu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.SoNgayDT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.KhongBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.Nu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.SoNguoiDTNT = bnPhauThuat.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Count();
                    _hdMoi.SoNgayDTNT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 1).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 22;
                    _hdMoi.TenNhom = "Thủ thuật.";
                    _hdMoi.TenChiTiet = "- Thủ thuật loại II";
                    _hdMoi.TongSo = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.Duoi1T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.Duoi6T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.Duoi15T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.CapCuu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.SoNgayDT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.KhongBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.Nu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.SoNguoiDTNT = bnPhauThuat.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Count();
                    _hdMoi.SoNgayDTNT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 2).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 22;
                    _hdMoi.TenNhom = "Thủ thuật.";
                    _hdMoi.TenChiTiet = "- Thủ thuật loại III";
                    _hdMoi.TongSo = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.Duoi1T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.Duoi6T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.Duoi15T = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.CapCuu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.SoNgayDT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Sum(p => p.SoNgaydt);
                    _hdMoi.CoBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.KhongBHYT = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.Nu = bnPhauThuat.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.SoNguoiDTNT = bnPhauThuat.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Count();
                    _hdMoi.SoNgayDTNT = bnPTRaVienTheoLoai.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenRG.ToLower().Contains("thủ thuật")).Where(p => p.Loai == 3).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                    _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                    _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                    _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                    _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region  Xuat excel
                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                    string[] _tieude = { "Stt", "Chỉ tiêu", "Tổng số BN vào điều trị", "BN dưới 1T", "BN dưới 6T", "BN từ 6-15T", "BN cấp cứu", "Số ngày điều trị", "Có BHYT", "Không BHYT", "Nữ", "BN ngoại trú", "Số ngày điều trị BN ngoại trú" };
                    string _filePath = "C:\\" + "HoatDongDieuTri.xls";
                    int[] _arrWidth = new int[] { };
                    var qexcel = _lHD;
                    DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 13];
                    for (int i = 0; i < 13; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int num = 1;
                    foreach (var r in qexcel)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        if (r.TenChiTiet == "")
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenNhom;
                        else
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenChiTiet;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TongSo;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.Duoi1T;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.Duoi6T;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.Duoi15T;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.CapCuu;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.SoNgayDT;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.CoBHYT;
                        DungChung.Bien.MangHaiChieu[num, 9] = r.KhongBHYT;
                        DungChung.Bien.MangHaiChieu[num, 10] = r.Nu;
                        DungChung.Bien.MangHaiChieu[num, 11] = r.SoNguoiDTNT;
                        DungChung.Bien.MangHaiChieu[num, 12] = r.SoNgayDTNT;
                        num++;
                    }
                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);
                    #endregion
                    if (ck_MS02.Checked == false)
                    {
                        BaoCao.Rep_BC_HDDieuTri_30012 rep = new BaoCao.Rep_BC_HDDieuTri_30012();
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                        rep.lbl_ThoiGian.Text = "Khoa: " + khoa + ", từ ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year
                                                + " đến ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                        rep.DataSource = _lHD.OrderBy(p => p.Stt);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    //mẫu số 02
                    else
                    {
                        #region 23. Ngày điều trị trung bình
                        _hdMoi = new HDDieuTri();
                        _hdMoi.Stt = 23;
                        _hdMoi.TenNhom = "Ngày điều trị trung bình.";
                        _hdMoi.TenChiTiet = "";
                        _hdMoi.TongSo = (bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 1 && p.DTNT == false).Count() == 0) ? 0 : (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt) / bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Count();
                        _hdMoi.TongSo = Math.Round(_hdMoi.TongSo.Value, 2);
                        _hdMoi.Duoi1T = (bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.Tuoi <= 1).Count() == 0) ? 0 : (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Sum(p => p.SoNgaydt) / bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.Tuoi <= 1).Count();
                        _hdMoi.Duoi1T = Math.Round(_hdMoi.Duoi1T.Value, 2);
                        _hdMoi.Duoi6T = (bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count() == 0) ? 0 : (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Sum(p => p.SoNgaydt) / bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Count();
                        _hdMoi.Duoi6T = Math.Round(_hdMoi.Duoi6T.Value, 2);
                        _hdMoi.Duoi15T = (bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count() == 0) ? 0 : (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Sum(p => p.SoNgaydt) / bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Count();
                        _hdMoi.Duoi15T = Math.Round(_hdMoi.Duoi15T.Value, 2);
                        _hdMoi.CapCuu = (bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.CapCuu == 1).Count() == 0) ? 0 : (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Sum(p => p.SoNgaydt) / bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.CapCuu == 1).Count();
                        _hdMoi.CapCuu = Math.Round(_hdMoi.CapCuu.Value, 2);
                        _hdMoi.SoNgayDT = (bnDieuTri.Sum(p => p.SoNgaydt) == 0) ? 0 : (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt) / bnDieuTri.Sum(p => p.SoNgaydt);
                        _hdMoi.CoBHYT = (bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.SThe != null && p.SThe.Trim() != "").Count() == 0) ? 0 : (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Sum(p => p.SoNgaydt) / bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                        _hdMoi.CoBHYT = Math.Round(_hdMoi.CoBHYT.Value, 2);
                        _hdMoi.KhongBHYT = (bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.SThe == null || p.SThe.Trim() == "").Count() == 0) ? 0 : (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Sum(p => p.SoNgaydt) / bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.SThe == null || p.SThe.Trim() == "").Count();
                        _hdMoi.KhongBHYT = Math.Round(_hdMoi.KhongBHYT.Value, 2);
                        _hdMoi.Nu = (bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.GTinh == 0).Count() == 0) ? 0 : (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Sum(p => p.SoNgaydt) / bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Status == 2).Where(p => p.GTinh == 0).Count();
                        _hdMoi.Nu = Math.Round(_hdMoi.Nu.Value, 2);
                        _hdMoi.SoNguoiDTNT = bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                        _hdMoi.SoNgayDTNT = (bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 0 && p.DTNT == true).Count() == 0) ? 0 : (double)bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt) / bnDieuTri.Where(p => p.Status == 2).Where(p => p.NoiTru == 0 && p.DTNT == true).Count();

                        _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                        _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                        _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                        _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                        _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                        _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                        _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                        _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                        _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                        _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                        _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                        _lHD.Add(_hdMoi);
                        #endregion
                        #region 24. Công suất sử dụng giường bệnh
                        double day = (denngay - tungay).TotalDays;
                        _hdMoi = new HDDieuTri();
                        _hdMoi.Stt = 24;
                        _hdMoi.TenNhom = "Công suất sử dụng giường bệnh.";
                        _hdMoi.TenChiTiet = "";
                        _hdMoi.TongSo = (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt) / (145 * day);
                        _hdMoi.TongSo = Math.Round(_hdMoi.TongSo.Value * 100, 2);
                        _hdMoi.Duoi1T = (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi <= 1).Sum(p => p.SoNgaydt) / (145 * day);
                        _hdMoi.Duoi1T = Math.Round(_hdMoi.Duoi1T.Value * 100, 2);
                        _hdMoi.Duoi6T = (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 1 && p.Tuoi <= 6).Sum(p => p.SoNgaydt) / (145 * day);
                        _hdMoi.Duoi6T = Math.Round(_hdMoi.Duoi6T.Value * 100, 2);
                        _hdMoi.Duoi15T = (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.Tuoi > 6 && p.Tuoi <= 15).Sum(p => p.SoNgaydt) / (145 * day);
                        _hdMoi.Duoi15T = Math.Round(_hdMoi.Duoi15T.Value * 100, 2);
                        _hdMoi.CapCuu = (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.CapCuu == 1).Sum(p => p.SoNgaydt) / (145 * day);
                        _hdMoi.CapCuu = Math.Round(_hdMoi.CapCuu.Value * 100, 2);
                        _hdMoi.SoNgayDT = (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Sum(p => p.SoNgaydt) / (145 * day);
                        _hdMoi.SoNgayDT = Math.Round(_hdMoi.SoNgayDT.Value * 100, 2);
                        _hdMoi.CoBHYT = (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Sum(p => p.SoNgaydt) / (145 * day);
                        _hdMoi.CoBHYT = Math.Round(_hdMoi.CoBHYT.Value * 100, 2);
                        _hdMoi.KhongBHYT = (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe == null || p.SThe.Trim() == "").Sum(p => p.SoNgaydt) / (145 * day);
                        _hdMoi.KhongBHYT = Math.Round(_hdMoi.KhongBHYT.Value * 100, 2);
                        _hdMoi.Nu = (double)bnDieuTri.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Sum(p => p.SoNgaydt) / (145 * day);
                        _hdMoi.Nu = Math.Round(_hdMoi.Nu.Value * 100, 2);

                        _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                        _hdMoi.Duoi1T = _hdMoi.Duoi1T == 0 ? null : _hdMoi.Duoi1T;
                        _hdMoi.Duoi15T = _hdMoi.Duoi15T == 0 ? null : _hdMoi.Duoi15T;
                        _hdMoi.Duoi6T = _hdMoi.Duoi6T == 0 ? null : _hdMoi.Duoi6T;
                        _hdMoi.CapCuu = _hdMoi.CapCuu == 0 ? null : _hdMoi.CapCuu;
                        _hdMoi.SoNgayDT = _hdMoi.SoNgayDT == 0 ? null : _hdMoi.SoNgayDT;
                        _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                        _hdMoi.KhongBHYT = _hdMoi.KhongBHYT == 0 ? null : _hdMoi.KhongBHYT;
                        _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                        _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                        _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                        _lHD.Add(_hdMoi);
                        #endregion
                        BaoCao.Rep_BC_HDDieuTri_MS02_30012 rep = new BaoCao.Rep_BC_HDDieuTri_MS02_30012();
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                        rep.lbl_ThoiGian.Text = "Khoa: " + khoa + ", từ ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year
                                                + " đến ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                        rep.DataSource = _lHD.OrderBy(p => p.Stt);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    #region query
                    //bệnh nhân khám bệnh khoa YHCT
                    var bnKhambenh = (from bn in data.BenhNhans
                                      join bnkb in data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                      join vv in data.VaoViens on bnkb.MaBNhan equals vv.MaBNhan
                                      join rv in data.RaViens on vv.MaBNhan equals rv.MaBNhan into kq
                                      from kq1 in kq.DefaultIfEmpty()
                                      select new { bn.SThe, bn.GTinh, bn.NoiTru, bn.DTNT, kq1.SoNgaydt, bnkb.NgayKham, bnkb.MaKP }).Where(p => maKhoa == 0 || p.MaKP == maKhoa).Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay).ToList();
                    //bn miễn giảm viện phí
                    //var bnMienGiamVP = (from bn in data.BenhNhans
                    //                    join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                    //                    join vpct in data.VienPhicts.Where(p => p.Mien > 0) on vp.idVPhi equals vpct.idVPhi
                    //                    join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                    //                    from kq1 in kq.DefaultIfEmpty()
                    //                    group new { bn, vp, vpct, kq1 } by new { bn.MaBNhan, bn.SThe, bn.GTinh, kq1.SoNgaydt, kq1.MaKP, vpct.Mien, bn.NoiTru, bn.DTNT } into q
                    //                    select new { q.Key.MaBNhan, q.Key.SThe, q.Key.GTinh, q.Key.SoNgaydt, q.Key.MaKP, q.Key.Mien, q.Key.NoiTru, q.Key.DTNT }).Where(p => maKhoa == 0 || p.MaKP == maKhoa).ToList();
                    //bn thực hiện kỹ thuật YHCT
                    var bnTHYHCT = (from bn in data.BenhNhans
                                    join cls in data.CLS on bn.MaBNhan equals cls.MaBNhan
                                    join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                    join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                    join rv in data.RaViens on bn.MaBNhan equals rv.MaBNhan into kq
                                    from kq1 in kq.DefaultIfEmpty()
                                    group new { bn, cls, cd, kq1 } by new
                                    {
                                        bn.Tuoi,
                                        bn.SThe,
                                        bn.GTinh,
                                        bn.CapCuu,
                                        bn.NoiTru,
                                        bn.DTNT,
                                        bn.MaBNhan,
                                        cls.NgayTH,
                                        dv.TenDV,
                                        KetQua = kq1 == null ? "" : kq1.KetQua,
                                        kq1.SoNgaydt,
                                        cls.MaKP
                                    } into q
                                    select new
                                    {
                                        q.Key.Tuoi,
                                        q.Key.SThe,
                                        q.Key.GTinh,
                                        q.Key.CapCuu,
                                        q.Key.NoiTru,
                                        q.Key.DTNT,
                                        q.Key.MaBNhan,
                                        q.Key.NgayTH,
                                        q.Key.TenDV,
                                        q.Key.KetQua,
                                        q.Key.SoNgaydt,
                                        q.Key.MaKP
                                    }).Where(p => p.TenDV.ToLower().Contains("điện châm") || p.TenDV.ToLower().Contains("hồng ngoại") || p.TenDV.ToLower().Contains("sóng ngắn")
                                              || p.TenDV.ToLower().Contains("thủy châm") || p.TenDV.ToLower().Contains("xoa bóp bấm huyệt") || p.TenDV.ToLower().Contains("siêu âm trị liệu")
                                              || p.TenDV.ToLower().Contains("tập vận động") || p.TenDV.ToLower().Contains("kéo giãn cột sống") || p.TenDV.ToLower().Contains("điện xung"))
                              .Where(p => maKhoa == 0 || p.MaKP == maKhoa).Where(p => (p.NgayTH >= tungay && p.NgayTH <= denngay)).ToList();
                    //Bệnh nhân YHCT kết hợp YHHD
                    var bnYHCTKetHopYHHD = (from n in bnYHCT
                                            join cls in data.CLS on n.MaBNhan equals cls.MaBNhan
                                            join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                            join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                            group new { n, cls, cd, dv, tn } by new { n.MaBNhan, n.SThe, n.GTinh, n.NoiTru, n.DTNT, cls.NgayTH, n.SoNgaydt } into kq
                                            select new { kq.Key.MaBNhan, kq.Key.SThe, kq.Key.GTinh, kq.Key.NoiTru, kq.Key.DTNT, kq.Key.NgayTH, kq.Key.SoNgaydt }).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).ToList();
                    #endregion
                    #region 7. Tổng số lượt khám bệnh
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 7;
                    _hdMoi.TenNhom = "Tổng số lượt khám bệnh.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnKhambenh.Where(p => p.NoiTru == 1 && p.DTNT == false).Count();
                    _hdMoi.CoBHYT = bnKhambenh.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Count();
                    _hdMoi.Nu = bnKhambenh.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Count();
                    _hdMoi.SoNguoiDTNT = bnKhambenh.Where(p => p.NoiTru == 0 && p.DTNT == true).Count();
                    _hdMoi.SoNgayDTNT = bnKhambenh.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 13. Người bệnh miễn giảm viện phí
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 13;
                    _hdMoi.TenNhom = "Người bệnh miễn giảm viện phí.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.HTTT == 0).Count();
                    _hdMoi.CoBHYT = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.HTTT == 0).Count();
                    _hdMoi.Nu = bnMienGiam.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.HTTT == 0).Count();
                    _hdMoi.SoNguoiDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.HTTT == 0).Count();
                    _hdMoi.SoNgayDTNT = bnMienGiam.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.HTTT == 0).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 14. Tai biến trong điều trị
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 14;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 14;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "Trong đó:";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 14;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "- Số tai biến do sử dụng thuốc (nhầm lẫn)";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 14;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "- Tai biến do phản ứng có hại của thuốc (ADR)";
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 14;
                    _hdMoi.TenNhom = "Tai biến trong điều trị.";
                    _hdMoi.TenChiTiet = "- Tai biến vì lý do khác";
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 15. Điều trị bằng YHCT kết hợp YHHĐ
                    //bệnh nhân YHCT có thực hiện CLS
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 15;
                    _hdMoi.TenNhom = "Điều trị bằng YHCT kết hợp YHHĐ.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnYHCTKetHopYHHD.Where(p => p.NoiTru == 1 && p.DTNT == false).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnYHCTKetHopYHHD.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnYHCTKetHopYHHD.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnYHCTKetHopYHHD.Where(p => p.NoiTru == 0 && p.DTNT == true).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnYHCTKetHopYHHD.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 16. Điều trị bằng YHCT kết hợp PHCN
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 16;
                    _hdMoi.TenNhom = "Điều trị bằng YHCT kết hợp PHCN.";
                    _hdMoi.TenChiTiet = "";
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region 17. Tổng số kỹ thuật YHCT thực hiện tại bệnh viện
                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "- Điện châm";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenDV.ToLower().Contains("điện châm")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenDV.ToLower().Contains("điện châm")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenDV.ToLower().Contains("điện châm")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("điện châm")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("điện châm")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "- Hồng ngoại";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenDV.ToLower().Contains("hồng ngoại")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenDV.ToLower().Contains("hồng ngoại")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenDV.ToLower().Contains("hồng ngoại")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("hồng ngoại")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("hồng ngoại")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "- Sóng ngắn";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenDV.ToLower().Contains("sóng ngắn")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenDV.ToLower().Contains("sóng ngắn")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenDV.ToLower().Contains("sóng ngắn")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("sóng ngắn")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("sóng ngắn")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "- Thủy châm";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenDV.ToLower().Contains("thủy châm")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenDV.ToLower().Contains("thủy châm")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenDV.ToLower().Contains("thủy châm")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("thủy châm")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("thủy châm")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "- Xoa bóp bấm huyệt";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenDV.ToLower().Contains("xoa bóp bấm huyệt")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenDV.ToLower().Contains("xoa bóp bấm huyệt")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenDV.ToLower().Contains("xoa bóp bấm huyệt")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("xoa bóp bấm huyệt")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("xoa bóp bấm huyệt")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "- Siêu âm trị liệu";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenDV.ToLower().Contains("siêu âm trị liệu")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenDV.ToLower().Contains("siêu âm trị liệu")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenDV.ToLower().Contains("siêu âm trị liệu")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("siêu âm trị liệu")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("siêu âm trị liệu")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "- Tập vận động";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenDV.ToLower().Contains("tập vận động")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenDV.ToLower().Contains("tập vận động")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenDV.ToLower().Contains("tập vận động")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("tập vận động")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("tập vận động")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "- Kéo giãn cột sống";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenDV.ToLower().Contains("kéo giãn cột sống")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenDV.ToLower().Contains("kéo giãn cột sống")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenDV.ToLower().Contains("kéo giãn cột sống")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("kéo giãn cột sống")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("kéo giãn cột sống")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);

                    _hdMoi = new HDDieuTri();
                    _hdMoi.Stt = 17;
                    _hdMoi.TenNhom = "Tổng số kỹ thuật YHCT thực hiện tại bệnh viện.";
                    _hdMoi.TenChiTiet = "- Điện xung";
                    _hdMoi.TongSo = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.TenDV.ToLower().Contains("điện xung")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.CoBHYT = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.SThe != null && p.SThe.Trim() != "").Where(p => p.TenDV.ToLower().Contains("điện xung")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.Nu = bnTHYHCT.Where(p => p.NoiTru == 1 && p.DTNT == false).Where(p => p.GTinh == 0).Where(p => p.TenDV.ToLower().Contains("điện xung")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNguoiDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("điện xung")).GroupBy(p => p.MaBNhan).Count();
                    _hdMoi.SoNgayDTNT = bnTHYHCT.Where(p => p.NoiTru == 0 && p.DTNT == true).Where(p => p.TenDV.ToLower().Contains("điện xung")).Sum(p => p.SoNgaydt);

                    _hdMoi.TongSo = _hdMoi.TongSo == 0 ? null : _hdMoi.TongSo;
                    _hdMoi.CoBHYT = _hdMoi.CoBHYT == 0 ? null : _hdMoi.CoBHYT;
                    _hdMoi.Nu = _hdMoi.Nu == 0 ? null : _hdMoi.Nu;
                    _hdMoi.SoNguoiDTNT = _hdMoi.SoNguoiDTNT == 0 ? null : _hdMoi.SoNguoiDTNT;
                    _hdMoi.SoNgayDTNT = _hdMoi.SoNgayDTNT == 0 ? null : _hdMoi.SoNgayDTNT;
                    _lHD.Add(_hdMoi);
                    #endregion
                    #region  Xuat excel
                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@" };
                    string[] _tieude = { "Stt", "Chỉ tiêu", "Tổng số BN vào điều trị", "Có BHYT", "Nữ", "BN ngoại trú", "Số ngày điều trị BN ngoại trú" };
                    string _filePath = "C:\\" + "HoatDongDieuTri_KhoaYHCT.xls";
                    int[] _arrWidth = new int[] { };
                    var qexcel = _lHD;
                    DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 7];
                    for (int i = 0; i < 7; i++)
                    {
                        DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                    }
                    int num = 1;
                    foreach (var r in qexcel)
                    {
                        DungChung.Bien.MangHaiChieu[num, 0] = num;
                        if (r.TenChiTiet == "")
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenNhom;
                        else
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenChiTiet;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.TongSo;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.CoBHYT;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.Nu;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoNguoiDTNT;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.SoNgayDTNT;
                        num++;
                    }
                    //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);
                    #endregion
                    BaoCao.Rep_BC_HDDieuTri_YHCT_30012 rep = new BaoCao.Rep_BC_HDDieuTri_YHCT_30012();
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                    rep.lbl_Khoa.Text = khoa;
                    rep.lbl_ThoiGian.Text = "Từ ngày " + tungay.Day + " tháng " + tungay.Month + " năm " + tungay.Year
                                            + " đến ngày " + denngay.Day + " tháng " + denngay.Month + " năm " + denngay.Year;
                    rep.DataSource = _lHD;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        #region class HDDieuTri
        public class HDDieuTri
        {
            public int? Stt { get; set; }
            public string TenNhom { get; set; }
            public string TenChiTiet { get; set; }
            public double? TongSo { get; set; }
            public double? Duoi1T { get; set; }
            public double? Duoi6T { get; set; }
            public double? Duoi15T { get; set; }
            public double? CapCuu { get; set; }
            public double? SoNgayDT { get; set; }
            public double? CoBHYT { get; set; }
            public double? KhongBHYT { get; set; }
            public double? Nu { get; set; }
            public double? SoNguoiDTNT { get; set; }
            public double? SoNgayDTNT { get; set; }
        }
        #endregion

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string khoa = lupKho.Properties.GetDisplayText(lupKho.EditValue);
            int _maK = Convert.ToInt32(lupKho.EditValue);
            var khoaYHCT = (from kp in data.KPhongs.Where(p => p.PLoai == "Lâm sàng" && p.MaKP == _maK)
                            join ck in data.ChuyenKhoas.Where(p => p.TenCK.Equals("Đông y") || p.TenCK.Contains("YHCT")) on kp.MaCK equals ck.MaCK
                            select new { kp.MaKP, kp.TenKP }).ToList();
            if (khoaYHCT.Count > 0)
            {
                ck_KhoaYHCT.Enabled = true;
                ck_MS02.Checked = false;
                ck_MS02.Enabled = false;
            }
            else
            {
                ck_KhoaYHCT.Enabled = false;
                ck_KhoaYHCT.Checked = false;
                ck_MS02.Enabled = true;
            }
        }

        private void ck_KhoaYHCT_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_KhoaYHCT.Checked == true)
                ck_MS02.Enabled = false;
            else
                ck_MS02.Enabled = true;
        }

        private void ck_MS02_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_MS02.Checked == true)
                ck_KhoaYHCT.Enabled = false;
            else
                ck_KhoaYHCT.Enabled = true;
        }
    }
}