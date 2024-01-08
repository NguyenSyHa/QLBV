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
    public partial class frm_BC_DoiChieuThuocTTBHYTNgoaiTru_30009 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_DoiChieuThuocTTBHYTNgoaiTru_30009()
        {
            InitializeComponent();
        }
        List<KhoaPhong> _lKP2 = new List<KhoaPhong>();
        List<KhoaKe> _lKhoake = new List<KhoaKe>();
        private void frm_BC_DoiChieuThuocTTBHYTNgoaiTru_30009_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            radNoiTru.SelectedIndex = 2;
            radTrongBH.SelectedIndex = 1;
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            // load danh sách kho xuất dược
            var q = (from k in data.KPhongs.Where(p => p.PLoai.Equals("Khoa dược"))
                     select new KhoaPhong()
                     {
                         Check = true,
                         MaKP = k.MaKP,
                         TenKP = k.TenKP
                     }).Distinct().ToList();
            _lKP2 = new List<KhoaPhong>(q.Count + 1);
            _lKP2.Add(new KhoaPhong { Check = true, MaKP = 0, TenKP = "Tất cả" });
            _lKP2.InsertRange(1, q);
            grcKhoaphong.DataSource = _lKP2;
            //danh sách khoa kê đơn
            var q1 = (from k in data.KPhongs
                      join rv in data.RaViens on k.MaKP equals rv.MaKP
                      select new KhoaKe()
                      {
                          Check = true,
                          MaKP = k.MaKP,
                          TenKP = k.TenKP
                      }).Distinct().ToList();
            _lKhoake = new List<KhoaKe>(q.Count + 1);
            _lKhoake.Add(new KhoaKe { Check = true, MaKP = 0, TenKP = "Tất cả" });
            _lKhoake.InsertRange(1, q1);
            grcKhoaKe.DataSource = _lKhoake;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region class Khoa kê, kho xuất dược
        //kho xuất dược
        public class KhoaPhong
        {
            public bool check;
            public int maKP;
            public string tenKP;

            public int MaKP { get { return maKP; } set { maKP = value; } }
            public bool Check { get { return check; } set { check = value; } }
            public string TenKP { get { return tenKP; } set { tenKP = value; } }
        }

        public class KhoaKe
        {
            public bool check;
            public int maKP;
            public string tenKP;

            public int MaKP { get { return maKP; } set { maKP = value; } }
            public bool Check { get { return check; } set { check = value; } }
            public string TenKP { get { return tenKP; } set { tenKP = value; } }
        }
        #endregion
        List<DChieuThuoc> _lThuoc = new List<DChieuThuoc>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            List<KhoaPhong> khoxuat = new List<KhoaPhong>();
            khoxuat = _lKP2.Where(p => p.Check == true).ToList();
            List<KhoaKe> khoake = new List<KhoaKe>();
            khoake = _lKhoake.Where(p => p.Check == true).ToList();
            _lThuoc.Clear();
            DChieuThuoc thuoc = new DChieuThuoc();
            int noitru = -1;
            if (radNoiTru.SelectedIndex == 0)//ngoại trú
                noitru = 0;
            if (radNoiTru.SelectedIndex == 1)//điều trị ngoại trú
                noitru = 1;
            if (radNoiTru.SelectedIndex == 2)//cả hai
                noitru = 2;
            var nhomdv = (from n in data.NhomDVs.Where(p => p.Status == 1).Where(p => p.TenNhomCT.Equals("Thuốc trong danh mục BHYT") || p.TenNhomCT.Equals("Thuốc thanh toán theo tỷ lệ"))
                          select n).ToList();
            var qdv = (from a in nhomdv
                       join tn in data.TieuNhomDVs on a.IDNhom equals tn.IDNhom
                       join dv in data.DichVus.Where(p => p.PLoai == 1) on tn.IdTieuNhom equals dv.IdTieuNhom
                       select new
                       {
                           a.IDNhom,
                           a.TenNhomCT,
                           a.TenNhom,
                           tn.TenTN,
                           dv.MaDV,
                           dv.TenDV,
                           dv.DonVi,
                           dv.DonGia
                       }).ToList();
            #region biểu mẫu 20
            var qrv = (from rv in data.RaViens
                       join bn in data.BenhNhans.Where(p => p.DTuong.ToUpper().Contains("BHYT") && (p.MaKCB == DungChung.Bien.MaBV)) on rv.MaBNhan equals bn.MaBNhan
                       join vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay) on bn.MaBNhan equals vp.MaBNhan
                       join vpct in data.VienPhicts.Where(p => radTrongBH.SelectedIndex == 2 || p.TrongBH == radTrongBH.SelectedIndex) on vp.idVPhi equals vpct.idVPhi
                       where (noitru == 0 ? (bn.NoiTru == 0 && bn.DTNT == false) : (noitru == 1 ? (bn.NoiTru == 0 && bn.DTNT == true) : (bn.NoiTru == 0)))
                       select new
                       {
                           bn.MaBNhan,
                           vp.NgayTT,
                           vpct.DonGia,
                           vpct.MaDV,
                           vpct.SoLuong,
                           vpct.ThanhTien,
                           vpct.TienBH,
                           vpct.TienBN,
                           MaKP = vpct.MaKP == null ? 0 : vpct.MaKP,
                       }).ToList();
            var q6 = (from l in qrv
                      join nhom in qdv on l.MaDV equals nhom.MaDV
                      select new
                      {
                          l.MaBNhan,
                          l.DonGia,
                          l.MaDV,
                          nhom.TenNhom,
                          nhom.TenTN,
                          nhom.TenDV,
                          l.SoLuong,
                          ThanhTien = l.ThanhTien,
                          l.MaKP
                      }).ToList();

            var q20 = (from lq in q6
                       join kp in khoake on lq.MaKP equals kp.MaKP
                       group lq by new
                       {
                           lq.DonGia,
                           //lq.TenNhom,
                           //lq.TenTN,
                           lq.TenDV,
                           lq.MaDV,
                           //lq.SoLuong,
                           //lq.ThanhTien
                       } into kq
                       select new
                       {
                           kq.Key.MaDV,
                           DonGia = kq.Key.DonGia,
                           //TenNhomThuoc = kq.Key.TenNhom,
                           //TenTNhom = kq.Key.TenTN,
                           TenThuoc = kq.Key.TenDV,
                           SoLuongMau20 = kq.Sum(p => p.SoLuong),
                           ThanhTienMau20 = kq.Sum(p => p.ThanhTien),
                       }).OrderBy(p => p.TenThuoc).ToList();
            //foreach (var item in q20)
            //{
            //    thuoc = new DChieuThuoc();
            //    thuoc.MaDV = item.MaDV ?? 0;
            //    thuoc.TenDV = item.TenThuoc;
            //    thuoc.DonGia = item.DonGia;
            //    thuoc.SoLuongMau20 = item.SoLuongMau20;
            //    thuoc.ThanhTienMau20 = item.ThanhTienMau20;
            //    _lThuoc.Add(thuoc);
            //}
            #endregion
            #region Xuất ngoại trú cho BN BHYT
            var qnduoc = (from nd in data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          join dtbn in data.DTBNs.Where(p => p.DTBN1.ToUpper().Contains("BHYT") && p.HTTT == 1) on ndct.IDDTBN equals dtbn.IDDTBN
                          group new { nd, ndct } by new { ndct.MaDV, nd.KieuDon, ndct.SoLuongX, ndct.ThanhTienX, nd.MaKP, nd.MaKPnx, ndct.DonGia } into kqduoc
                          select new
                          {
                              kqduoc.Key.MaKP,
                              kqduoc.Key.MaKPnx,
                              kqduoc.Key.MaDV,
                              kqduoc.Key.KieuDon,
                              kqduoc.Key.SoLuongX,
                              kqduoc.Key.ThanhTienX,
                              kqduoc.Key.DonGia
                          }).ToList();
            var qnduoc1 = (from a in qdv
                           join b in qnduoc on a.MaDV equals b.MaDV
                           join c in khoake on b.MaKPnx equals c.MaKP
                           join d in khoxuat on b.MaKP equals d.MaKP
                           group new { a, b } by new { a.MaDV, a.DonVi, a.TenDV, b.DonGia } into qb
                           select new
                           {
                               qb.Key.TenDV,
                               qb.Key.MaDV,
                               qb.Key.DonVi,
                               qb.Key.DonGia,
                               SLDTNT = qb.Where(p => p.b.KieuDon == 4).Sum(p => p.b.SoLuongX),
                               TTDTNT = qb.Where(p => p.b.KieuDon == 4).Sum(p => p.b.ThanhTienX),
                               //SLXuatKhac = (radNoiTru.SelectedIndex == 2) ? qb.Where(p => p.b.KieuDon != 4 && p.b.KieuDon != 0 && p.b.KieuDon != 1 && p.b.KieuDon != 2).Sum(p => p.b.SoLuongX) : qb.Where(p => p.b.KieuDon != 4 && p.b.KieuDon != radNoiTru.SelectedIndex && p.b.KieuDon != 2).Sum(p => p.b.SoLuongX),
                               //TTXuatKhac = (radNoiTru.SelectedIndex == 2) ? qb.Where(p => p.b.KieuDon != 4 && p.b.KieuDon != 0 && p.b.KieuDon != 1 && p.b.KieuDon != 2).Sum(p => p.b.ThanhTienX) : qb.Where(p => p.b.KieuDon != 4 && p.b.KieuDon != radNoiTru.SelectedIndex && p.b.KieuDon != 2).Sum(p => p.b.ThanhTienX),
                               SLXuatKhac = qb.Where(p => p.b.KieuDon != 4 && p.b.KieuDon != 0 && p.b.KieuDon != 1 && p.b.KieuDon != 2).Sum(p => p.b.SoLuongX),
                               TTXuatKhac = qb.Where(p => p.b.KieuDon != 4 && p.b.KieuDon != 0 && p.b.KieuDon != 1 && p.b.KieuDon != 2).Sum(p => p.b.ThanhTienX)
                           }).ToList();

            var duoc = (from dt in data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= denngay)
                        join dtct in data.DThuoccts.Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
                        join bn in data.BenhNhans.Where(p => p.DTuong.ToUpper().Contains("BHYT") && (p.MaKCB == DungChung.Bien.MaBV)) on dt.MaBNhan equals bn.MaBNhan
                        where (noitru == 0 ? (bn.NoiTru == 0 && bn.DTNT == false) : (noitru == 1 ? (bn.NoiTru == 0 && bn.DTNT == true) : (bn.NoiTru == 0)))
                        group new { dt, dtct, bn } by new { dt.MaKP, dtct.MaDV, dt.MaBNhan, dtct.DonGia, dtct.SoLuong, dtct.ThanhTien, dtct.MaKXuat, KhoXuat = dt.MaKXuat, bn.NoiTru, bn.DTNT } into qduoc
                        select new
                        {
                            qduoc.Key.MaKP,
                            qduoc.Key.MaDV,
                            qduoc.Key.DonGia,
                            SoLuong = (qduoc.Key.NoiTru == 0 && qduoc.Key.DTNT == false) ? qduoc.Key.SoLuong : 0,
                            qduoc.Key.MaBNhan,
                            ThanhTien = (qduoc.Key.NoiTru == 0 && qduoc.Key.DTNT == false) ? qduoc.Key.ThanhTien : 0,
                            MaKXuat = qduoc.Key.MaKXuat == null ? qduoc.Key.KhoXuat : qduoc.Key.MaKXuat
                        }).ToList();
            //var duoc = (from nd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 2 && p.KieuDon == 0)
            //            join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
            //            join bn in data.BenhNhans.Where(p => p.DTuong.ToUpper().Contains("BHYT") && (p.MaKCB == DungChung.Bien.MaBV)) on ndct.MaBNhan equals bn.MaBNhan
            //            where (noitru == 0 ? (bn.NoiTru == 0 && bn.DTNT == false) : (noitru == 1 ? (bn.NoiTru == 0 && bn.DTNT == true) : (bn.NoiTru == 0)))
            //            group new { nd, ndct, bn } by new { MaKP = nd.MaKPnx, ndct.MaDV, ndct.MaBNhan, ndct.DonGia, SoLuong = ndct.SoLuongX, ThanhTien = ndct.ThanhTienX, MaKXuat = nd.MaKP, bn.NoiTru, bn.DTNT } into qduoc
            //            select new
            //            {
            //                qduoc.Key.MaKP,
            //                qduoc.Key.MaDV,
            //                qduoc.Key.DonGia,
            //                SoLuong = qduoc.Key.SoLuong,
            //                qduoc.Key.MaBNhan,
            //                ThanhTien = qduoc.Key.ThanhTien,
            //                MaKXuat = qduoc.Key.MaKXuat
            //            }).ToList();
            var vienphi = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                           join vpct in data.VienPhicts.Where(p => radTrongBH.SelectedIndex == 2 || p.TrongBH == radTrongBH.SelectedIndex) on vp.idVPhi equals vpct.idVPhi
                           join bn in data.BenhNhans.Where(p => p.DTuong.ToUpper().Contains("BHYT") && (p.MaKCB == DungChung.Bien.MaBV)) on vp.MaBNhan equals bn.MaBNhan
                           where (noitru == 0 ? (bn.NoiTru == 0 && bn.DTNT == false) : (noitru == 1 ? (bn.NoiTru == 0 && bn.DTNT == true) : (bn.NoiTru == 0)))
                           select new { vpct.DonGia, vpct.MaKP, vpct.MaDV, vp.MaBNhan, vp.NgayTT }).ToList();
            var nhapd = (from k in khoxuat
                         join nd in duoc on k.MaKP equals nd.MaKXuat
                         join kho in khoake on nd.MaKP equals kho.MaKP
                         join vp in vienphi on new { madv = nd.MaDV, mabn = nd.MaBNhan } equals new { madv = vp.MaDV, mabn = vp.MaBNhan } into q
                         from kq in q.DefaultIfEmpty()
                         group new { nd, kq } by new { nd.MaDV, DonGia = kq != null ? kq.DonGia : nd.DonGia } into qnd
                         select new
                         {
                             SoLuongNgTru = qnd.Sum(p => p.nd.SoLuong),
                             ThanhTienNgTru = qnd.Sum(p => p.nd.ThanhTien),
                             qnd.Key.MaDV,
                             qnd.Key.DonGia
                         }).ToList();
            #endregion
            var qnhapd = (from n1 in nhapd join n2 in qdv on n1.MaDV equals n2.MaDV select n1).ToList();

            var qbn = (from q1 in q20
                       join q2 in qnhapd on new { madv = q1.MaDV, dongia = q1.DonGia } equals new { madv = q2.MaDV, dongia = q2.DonGia } into q
                       from kq1 in q.DefaultIfEmpty()
                       group new { q1, kq1 } by new
                       {
                           q1.MaDV,
                           q1.DonGia,
                           q1.SoLuongMau20,
                           q1.ThanhTienMau20,
                           SoLuongNgTru = kq1 == null ? 0 : kq1.SoLuongNgTru,
                           ThanhTienNgTru = kq1 == null ? 0 : kq1.ThanhTienNgTru
                       } into k
                       select new
                       {
                           k.Key.MaDV,
                           k.Key.DonGia,
                           SoLuongMau20 = k.Key.SoLuongMau20,
                           ThanhTienMau20 = k.Key.ThanhTienMau20,
                           SoLuongNgTru = k.Sum(p => (double?)k.Key.SoLuongNgTru) ?? 0,
                           ThanhTienNgTru = k.Sum(p => (double?)k.Key.ThanhTienNgTru) ?? 0
                       }).ToList();

            var query = (from dv in qdv
                         join n in qbn on dv.MaDV equals n.MaDV
                         group new { dv, n } by new
                         {
                             dv.MaDV,
                             dv.TenDV,
                             dv.DonVi,
                             n.DonGia,
                             n.SoLuongMau20,
                             n.ThanhTienMau20,
                             n.SoLuongNgTru,
                             n.ThanhTienNgTru
                         } into kq
                         select new
                         {
                             kq.Key.MaDV,
                             TenDV = kq.Key.TenDV,
                             DonVi = kq.Key.DonVi,
                             DonGia = kq.Key.DonGia,
                             SoLuongNgTru = kq.Key.SoLuongNgTru,
                             ThanhTienNgTru = kq.Key.ThanhTienNgTru,
                             SoLuongMau20 = kq.Key.SoLuongMau20,
                             ThanhTienMau20 = kq.Key.ThanhTienMau20
                         }).ToList();
            List<DChieuThuoc> dsThuoc = new List<DChieuThuoc>();
            //List<DChieuThuoc> dsThuocTT = new List<DChieuThuoc>();
            //var thuocTT = (from a in qnduoc1
            //               join b in vienphi on a.MaDV equals b.MaDV
            //               join c in duoc on a.MaDV equals c.MaDV
            //               select a).ToList();
            foreach (var item in query)
            {
                DChieuThuoc moi = new DChieuThuoc();
                moi.TenDV = item.TenDV;
                moi.DonVi = item.DonVi;
                moi.DonGia = item.DonGia;
                moi.SoLuongNgTru = item.SoLuongNgTru;
                moi.ThanhTienNgTru = item.ThanhTienNgTru;
                moi.SoLuongMau20 = item.SoLuongMau20;
                moi.ThanhTienMau20 = item.ThanhTienMau20;
                foreach (var d in qnduoc1)
                {
                    if (item.MaDV == d.MaDV)
                    {
                        moi.SLDTNT = d.SLDTNT;
                        moi.TTDTNT = d.TTDTNT;
                        moi.SLXuatKhac = d.SLXuatKhac;
                        moi.TTXuatKhac = d.TTXuatKhac;
                    }
                }
                dsThuoc.Add(moi);
            }
            #region thuốc đã xuất nhưng chưa thanh toán
            var thuocChuaTT = (from a in qnduoc1
                               join c in duoc on a.MaDV equals c.MaDV
                               join b in vienphi on a.MaDV equals b.MaDV into q
                               from kq in q.DefaultIfEmpty()
                               where (kq == null)
                               select a).ToList();
            foreach (var item in thuocChuaTT)
            {
                thuoc = new DChieuThuoc();
                thuoc.TenDV = item.TenDV;
                thuoc.MaDV = item.MaDV;
                thuoc.DonVi = item.DonVi;
                thuoc.DonGia = item.DonGia;
                thuoc.SoLuongMau20 = 0;
                thuoc.ThanhTienMau20 = 0;
                thuoc.SLDTNT = item.SLDTNT;
                thuoc.TTDTNT = item.TTDTNT;
                thuoc.SLXuatKhac = item.SLXuatKhac;
                thuoc.TTXuatKhac = item.TTXuatKhac;
                _lThuoc.Add(thuoc);
            }
            var Thuoc = (from a in dsThuoc select a).Union(from b in _lThuoc select b);//thuốc đã xuất nhưng chưa thanh toán và thuốc đã thanh toán
            var qThuoc = (from c in Thuoc
                          group c by new { c.TenDV, c.DonGia, c.DonVi } into kq
                          select new
                          {
                              kq.Key.TenDV,
                              kq.Key.DonGia,
                              kq.Key.DonVi,
                              SoLuongNgTru = kq.Sum(p => p.SoLuongNgTru),
                              ThanhTienNgTru = kq.Sum(p => p.ThanhTienNgTru),
                              SoLuongMau20 = kq.Sum(p => p.SoLuongMau20),
                              ThanhTienMau20 = kq.Sum(p => p.ThanhTienMau20),
                              SLDTNT = kq.Sum(p => p.SLDTNT),
                              TTDTNT = kq.Sum(p => p.TTDTNT),
                              SLXuatKhac = kq.Sum(p => p.SLXuatKhac),
                              TTXuatKhac = kq.Sum(p => p.TTXuatKhac)
                          }).ToList();
            #endregion
            if (query.Count > 0)
            {
                BaoCao.Rep_BC_DoiChieuThuocTTBHYTNgoaiTru_30009 rep = new BaoCao.Rep_BC_DoiChieuThuocTTBHYTNgoaiTru_30009();
                frmIn frm = new frmIn();
                rep.lblNgayThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                rep.DataSource = qThuoc.OrderBy(p => p.TenDV).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Không có dữ liệu", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region class DChieuThuoc
        private class DChieuThuoc
        {
            private int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            private string donVi;

            public string DonVi
            {
                get { return donVi; }
                set { donVi = value; }
            }
            private double? donGia;

            public double? DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            private double? soLuongNgTru;

            public double? SoLuongNgTru
            {
                get { return soLuongNgTru; }
                set { soLuongNgTru = value; }
            }
            private double? thanhTienNgTru;

            public double? ThanhTienNgTru
            {
                get { return thanhTienNgTru; }
                set { thanhTienNgTru = value; }
            }
            private double? soLuongMau20;

            public double? SoLuongMau20
            {
                get { return soLuongMau20; }
                set { soLuongMau20 = value; }
            }
            private double? thanhTienMau20;

            public double? ThanhTienMau20
            {
                get { return thanhTienMau20; }
                set { thanhTienMau20 = value; }
            }
            private double? sLDTNT;

            public double? SLDTNT
            {
                get { return sLDTNT; }
                set { sLDTNT = value; }
            }
            private double? tTDTNT;

            public double? TTDTNT
            {
                get { return tTDTNT; }
                set { tTDTNT = value; }
            }
            private double? sLXuatKhac;

            public double? SLXuatKhac
            {
                get { return sLXuatKhac; }
                set { sLXuatKhac = value; }
            }
            private double? tTXuatKhac;

            public double? TTXuatKhac
            {
                get { return tTXuatKhac; }
                set { tTXuatKhac = value; }
            }
        }
        #endregion

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Tất cả")
                    {
                        if (_lKP2.First().Check == true)
                        {
                            foreach (var a in _lKP2)
                            {
                                a.Check = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKP2)
                            {
                                a.Check = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKP2.ToList();
                    }
                }
            }
        }

        private void grvKhoaKe_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colCheck")
            {
                if (grvKhoaKe.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaKe.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Tất cả")
                    {
                        if (_lKhoake.First().Check == true)
                        {
                            foreach (var a in _lKhoake)
                            {
                                a.Check = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoake)
                            {
                                a.Check = true;
                            }
                        }
                        grcKhoaKe.DataSource = "";
                        grcKhoaKe.DataSource = _lKhoake.ToList();
                    }
                }
            }
        }
    }
}