using System;
using QLBV_Database;
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
    public partial class frm_BCGiaoBanHangNgay_01071 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCGiaoBanHangNgay_01071()
        {
            InitializeComponent();
        }

        private void frm_BCGiaoBanHangNgay_01071_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Today;
            DateTime tungay = dateTuNgay.DateTime.AddHours(23).AddMinutes(59).AddSeconds(59);
            dateDenNgay.DateTime = tungay;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = dateTuNgay.DateTime;
            DateTime denngay = dateDenNgay.DateTime;
            #region nội trú
            if (radmauin.SelectedIndex == 0)
            {
                #region bn cũ/ mới
                var bn1 = (from a in data.BenhNhans.Where(p => p.NoiTru == 1)
                           join b in data.BNKBs on a.MaBNhan equals b.MaBNhan
                           join c in data.VaoViens on a.MaBNhan equals c.MaBNhan
                           join d in data.RaViens on a.MaBNhan equals d.MaBNhan into kq
                           from kq1 in kq.DefaultIfEmpty()
                           select new
                           {
                               a.MaBNhan,
                               c.NgayVao,
                               a.Status,
                               NgayRa = kq1 == null ? null : kq1.NgayRa,
                               b.IDKB,
                               b.NgayKham,
                               a.DTuong
                           }).ToList();
                var bn2 = (from a in bn1.Where(p => p.NgayKham <= denngay).Where(p => p.NgayRa > denngay || p.NgayRa == null)
                           group a by new
                           {
                               a.MaBNhan,
                               a.NgayVao,
                               a.Status,
                               a.NgayRa,
                               a.DTuong
                           } into kq
                           select new
                           {
                               kq.Key.MaBNhan,
                               kq.Key.NgayVao,
                               kq.Key.Status,
                               kq.Key.NgayRa,
                               IDKB = kq.Max(p => p.IDKB),
                               kq.Key.DTuong
                           }).ToList();
                var bn21 = (from b in bn2
                            join a in data.BNKBs on b.IDKB equals a.IDKB
                            select new
                            {
                                b.MaBNhan,
                                b.NgayVao,
                                b.Status,
                                b.NgayRa,
                                b.DTuong,
                                MaKP = (a.MaKPdt != null && a.MaKPdt != 0 & a.NgayNghi < denngay) ? a.MaKPdt : a.MaKP
                            }).ToList();
                var query2 = (from b in bn21
                              join c in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang) on b.MaKP equals c.MaKP
                              join d in data.TTboXungs on b.MaBNhan equals d.MaBNhan
                              group new { b, c, d } by new { c.MaKP, c.TenKP } into kq
                              select new
                              {
                                  kq.Key.MaKP,
                                  kq.Key.TenKP,
                                  BHYT1 = 0,
                                  BHYT2 = 0,
                                  BHYT3 = 0,
                                  DV1 = 0,
                                  DV2 = 0,
                                  DV3 = 0,
                                  Tong1 = 0,
                                  Tong2 = 0,
                                  Tong3 = 0,
                                  Den = 0,
                                  Di = 0,
                                  BNBHYTHT = kq.Where(p => p.b.DTuong == "BHYT" && p.d.HTThanhToan == 0).Count(),
                                  BNDVHT = kq.Where(p => p.b.DTuong == "Dịch vụ" && p.d.HTThanhToan == 0).Count(),
                                  BNCKHT = kq.Where(p => p.d.HTThanhToan == 1).Count(),
                                  RV = 0,
                              }).ToList();
                var bn3 = (from a in bn1.Where(p => p.NgayKham < tungay).Where(p => p.NgayRa > tungay || p.NgayRa == null)
                           group a by new
                           {
                               a.MaBNhan,
                               a.NgayVao,
                               a.Status,
                               a.NgayRa,
                               a.DTuong
                           } into kq
                           select new
                           {
                               kq.Key.MaBNhan,
                               kq.Key.NgayVao,
                               kq.Key.Status,
                               kq.Key.NgayRa,
                               IDKB = kq.Max(p => p.IDKB),
                               kq.Key.DTuong
                           }).ToList();
                var bn31 = (from b in bn3
                            join a in data.BNKBs on b.IDKB equals a.IDKB
                            select new
                            {
                                b.MaBNhan,
                                b.NgayVao,
                                b.Status,
                                b.NgayRa,
                                b.DTuong,
                                MaKP = (a.MaKPdt != null && a.MaKPdt != 0 && a.NgayNghi < tungay) ? a.MaKPdt : a.MaKP
                            }).ToList();
                var query3 = (from b in bn31
                              join c in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang) on b.MaKP equals c.MaKP
                              group new { b, c } by new { c.MaKP, c.TenKP } into kq
                              select new
                              {
                                  kq.Key.MaKP,
                                  kq.Key.TenKP,
                                  BHYT1 = kq.Where(p => p.b.DTuong == "BHYT").Count(),
                                  BHYT2 = 0,
                                  BHYT3 = 0,
                                  DV1 = kq.Where(p => p.b.DTuong == "Dịch vụ").Count(),
                                  DV2 = 0,
                                  DV3 = 0,
                                  Tong1 = kq.Where(p => p.b.DTuong == "BHYT" || p.b.DTuong == "Dịch vụ").Count(),
                                  Tong2 = 0,
                                  Tong3 = 0,
                                  Den = 0,
                                  Di = 0,
                                  BNBHYTHT = 0,
                                  BNDVHT = 0,
                                  BNCKHT = 0,
                                  RV = 0,
                              }).ToList();
                var query4 = query3.Concat(query2);
                #endregion
                #region bn vv/cv
                var qbn = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                           join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                           join kp in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang) on vv.MaKP equals kp.MaKP//.Where(p => p.PLoai.Equals("Lâm sàng"))
                           select new
                           {
                               kp.TenKP,
                               kp.MaKP,
                               bn.MaBNhan,
                               vv.NgayVao,
                               bn.Status,
                               bn.DTuong
                           }).ToList();

                var query = (from b in qbn
                             group b by new { b.MaKP, b.TenKP } into kq
                             select new
                             {
                                 kq.Key.MaKP,
                                 kq.Key.TenKP,
                                 BHYT1 = 0,
                                 BHYT2 = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                                 BHYT3 = 0,
                                 DV1 = 0,
                                 DV2 = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                                 DV3 = 0,
                                 Tong1 = 0,
                                 Tong2 = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                                 Tong3 = 0,
                                 Den = 0,
                                 Di = 0,
                                 BNBHYTHT = 0,
                                 BNDVHT = 0,
                                 BNCKHT = 0,
                                 RV = 0,
                             }).ToList();
                var qbn1 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                            join vv in data.RaViens.Where(p => p.Status == 1) on bn.MaBNhan equals vv.MaBNhan
                            join kp in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang) on vv.MaKP equals kp.MaKP//.Where(p => p.PLoai.Equals("Lâm sàng"))
                            select new
                            {
                                kp.TenKP,
                                kp.MaKP,
                                bn.MaBNhan,
                                vv.NgayRa,
                                bn.Status,
                                bn.DTuong
                            }).ToList();

                var query1 = (from b in qbn1
                              group b by new { b.MaKP, b.TenKP } into kq
                              select new
                              {
                                  kq.Key.MaKP,
                                  kq.Key.TenKP,
                                  BHYT1 = 0,
                                  BHYT2 = 0,
                                  BHYT3 = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.DTuong == "BHYT").Select(p => p.MaBNhan).Distinct().Count(),
                                  DV1 = 0,
                                  DV2 = 0,
                                  DV3 = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                                  Tong1 = 0,
                                  Tong2 = 0,
                                  Tong3 = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.DTuong == "BHYT" || p.DTuong == "Dịch vụ").Select(p => p.MaBNhan).Distinct().Count(),
                                  Den = 0,
                                  Di = 0,
                                  BNBHYTHT = 0,
                                  BNDVHT = 0,
                                  BNCKHT = 0,
                                  RV = 0,
                              }).ToList();
                var query5 = query1.Concat(query);
                var query6 = query5.Concat(query4);
                #endregion
                #region bn chuyển khoa
                var chuyen = (from a in data.BNKBs.Where(p => p.PhuongAn == 3).Where(p => p.NgayNghi >= tungay && p.NgayNghi <= denngay)
                              join b in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang) on a.MaKP equals b.MaKP
                              select new { a.MaBNhan, a.MaKP, b.TenKP, a.MaKPdt, a.NgayNghi }).ToList();
                var test = (from a in chuyen
                            join b in data.BNKBs.Where(p => p.MaKP == 35) on a.MaBNhan equals b.MaBNhan
                            where (b.NgayKham >= a.NgayNghi && a.MaKPdt == b.MaKP && b.NgayKham >= tungay && b.NgayKham <= denngay)
                            select new { a.MaBNhan, a.MaKP, a.TenKP, }).ToList();
                var chuyen1 = (from a in chuyen
                               group a by new { a.MaKP, a.TenKP } into kq
                               select new
                               {
                                   MaKP = kq.Key.MaKP ?? 0,
                                   kq.Key.TenKP,
                                   BHYT1 = 0,
                                   BHYT2 = 0,
                                   BHYT3 = 0,
                                   DV1 = 0,
                                   DV2 = 0,
                                   DV3 = 0,
                                   Tong1 = 0,
                                   Tong2 = 0,
                                   Tong3 = 0,
                                   Den = 0,
                                   Di = kq.Count(),
                                   BNBHYTHT = 0,
                                   BNDVHT = 0,
                                   BNCKHT = 0,
                                   RV = 0,
                               }).ToList();
                var chuyen2 = (from a in data.BNKBs.Where(p => p.PhuongAn == 3).Where(p => p.NgayNghi >= tungay && p.NgayNghi <= denngay)
                               join b in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang) on a.MaKPdt equals b.MaKP
                               select new { a.MaBNhan, a.MaKPdt, b.TenKP, a.NgayNghi, a.MaKP }).ToList();
                var test2 = (from a in chuyen2
                             join b in data.BNKBs on a.MaBNhan equals b.MaBNhan
                             where (b.NgayKham >= a.NgayNghi && a.MaKP == b.MaKP && b.NgayKham >= tungay && b.NgayKham <= denngay)
                             select new { a.MaBNhan, a.MaKP, a.TenKP, }).ToList();
                var chuyen3 = (from a in chuyen2
                                   //join b in data.BNKBs on a.MaBNhan equals b.MaBNhan
                                   //where (a.MaKPdt == b.MaKP && a.NgayNghi <= b.NgayKham && b.NgayKham >= tungay && b.NgayKham <= denngay)
                               group a by new { a.MaKPdt, a.TenKP } into kq
                               select new
                               {
                                   MaKP = kq.Key.MaKPdt ?? 0,
                                   kq.Key.TenKP,
                                   BHYT1 = 0,
                                   BHYT2 = 0,
                                   BHYT3 = 0,
                                   DV1 = 0,
                                   DV2 = 0,
                                   DV3 = 0,
                                   Tong1 = 0,
                                   Tong2 = 0,
                                   Tong3 = 0,
                                   Den = kq.Count(),
                                   Di = 0,
                                   BNBHYTHT = 0,
                                   BNDVHT = 0,
                                   BNCKHT = 0,
                                   RV = 0,
                               }).ToList();
                var chuyen4 = (from a in data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.Status != 1)
                               join b in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang) on a.MaKP equals b.MaKP
                               join c in data.BenhNhans.Where(p => p.NoiTru == 1) on a.MaBNhan equals c.MaBNhan
                               select new { a.MaBNhan, b.TenKP, a.MaKP }).ToList();
                var chuyen5 = (from a in chuyen4
                               group a by new { a.MaKP, a.TenKP } into kq
                               select new
                               {
                                   MaKP = kq.Key.MaKP,
                                   kq.Key.TenKP,
                                   BHYT1 = 0,
                                   BHYT2 = 0,
                                   BHYT3 = 0,
                                   DV1 = 0,
                                   DV2 = 0,
                                   DV3 = 0,
                                   Tong1 = 0,
                                   Tong2 = 0,
                                   Tong3 = 0,
                                   Den = 0,
                                   Di = 0,
                                   BNBHYTHT = 0,
                                   BNDVHT = 0,
                                   BNCKHT = 0,
                                   RV = kq.Count(),
                               }).ToList();
                var query7 = chuyen1.Concat(chuyen3).Concat(chuyen5);
                var query8 = query7.Concat(query6);
                var ds = (from a in query8
                          group a by new { a.MaKP, a.TenKP } into kq
                          select new
                          {
                              kq.Key.MaKP,
                              kq.Key.TenKP,
                              BHYT1 = kq.Sum(p => p.BHYT1),
                              BHYT2 = kq.Sum(p => p.BHYT2),
                              BHYT3 = kq.Sum(p => p.BHYT3),
                              DV1 = kq.Sum(p => p.DV1),
                              DV2 = kq.Sum(p => p.DV2),
                              DV3 = kq.Sum(p => p.DV3),
                              Tong1 = kq.Sum(p => p.Tong1),
                              Tong2 = kq.Sum(p => p.Tong2),
                              Tong3 = kq.Sum(p => p.Tong3),
                              Den = kq.Sum(p => p.Den),
                              Di = kq.Sum(p => p.Di),
                              BNBHYTHT = kq.Sum(p => p.BNBHYTHT),
                              BNDVHT = kq.Sum(p => p.BNDVHT),
                              BNCKHT = kq.Sum(p => p.BNCKHT),
                              RV = kq.Sum(p => p.RV),
                          }).ToList();
                #endregion
                BaoCao.rep_BCGiaoBan_NT rep = new BaoCao.rep_BCGiaoBan_NT();
                frmIn frm = new frmIn();
                rep.DataSource = ds.OrderBy(p => p.TenKP).ToList();
                rep.ngaythang.Value = "Từ ngày " + dateTuNgay.Text + " đến ngày " + dateDenNgay.Text;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion
            #region ngoại trú
            if (radmauin.SelectedIndex == 1)
            {
                var bn = (from a in data.BenhNhans
                          join b in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on a.MaBNhan equals b.MaBNhan
                          join c in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) on b.MaKP equals c.MaKP
                          join d in data.BenhViens on a.MaCS equals d.MaBV into kq
                          from k in kq.DefaultIfEmpty()
                          group new { a, b, k } by new
                          {
                              a.MaBNhan,
                              a.DTuong,
                              b.MaKP,
                              a.CapCuu,
                              b.PhuongAn,
                              TuyenBV = (k != null) ? k.TuyenBV : "-1"
                          } into kq
                          select new
                          {
                              kq.Key.MaBNhan,
                              kq.Key.DTuong,
                              kq.Key.MaKP,
                              kq.Key.CapCuu,
                              kq.Key.PhuongAn,
                              kq.Key.TuyenBV
                          }).ToList();
                string tuyen = data.BenhViens.Where(p => p.MaBV == "01071" || p.MaBV == "01049").Select(p => p.TuyenBV).First().ToString();
                var ds1 = (from a in bn
                           group new { a } by new { a.MaKP } into kq
                           select new
                           {
                               kq.Key.MaKP,
                               TSBNTN = 0,
                               TSBNDK = kq.Count(),
                               CC = kq.Where(p => p.a.CapCuu == 1).Count(),
                               BHYTDT = kq.Where(p => p.a.TuyenBV == tuyen && p.a.DTuong == "BHYT").Count(),
                               BHYTTT = kq.Where(p => p.a.TuyenBV != tuyen && p.a.DTuong == "BHYT").Count(),
                               DV = kq.Where(p => p.a.DTuong == "Dịch vụ").Count(),
                               KSK = kq.Where(p => p.a.DTuong == "KSK").Count(),
                               BNVV = kq.Where(p => p.a.PhuongAn == 1).Count(),
                               BNCV = kq.Where(p => p.a.PhuongAn == 2).Count(),
                               TSDNCK = 0
                           }).ToList();
                var _lbnkb = (from bnn in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                              join bnkb in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on bnn.MaBNhan equals bnkb.MaBNhan into k
                              from kq in k.DefaultIfEmpty()
                              select new { bnn.MaBNhan, MaKP = (kq != null) ? kq.MaKP : bnn.MaKP, MaKPKB = (kq != null) ? kq.MaKPdt : 0 }).ToList();
                var dskp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Select(p => new { p.MaKP, p.TenKP }).ToList();
                List<kphong> kp2 = new List<kphong>();
                kp2.Clear();
                foreach (var item in dskp)
                {
                    var ds2 = (from bnn in _lbnkb
                               where (bnn.MaKP == item.MaKP || bnn.MaKPKB == item.MaKP)
                               group bnn by new
                               {
                                   MaBNhan = bnn.MaBNhan,
                               } into kq
                               select new
                               {
                                   MaBNhan = kq.Key.MaBNhan,
                               }).ToList();
                    kphong kp1 = new kphong();
                    kp1.MaKP = item.MaKP;
                    kp1.Tong = ds2.Count();
                    kp2.Add(kp1);
                }
                var ds = (from b in kp2.Where(p => p.Tong != 0)
                          join k1 in ds1 on b.MaKP equals k1.MaKP into k
                          from a in k.DefaultIfEmpty()
                          join c in data.KPhongs on b.MaKP equals c.MaKP
                          select new
                          {
                              c.TenKP,
                              b.MaKP,
                              TSBNTN = b.Tong,
                              TSBNDK = a != null ? a.TSBNDK : 0,
                              CC = a != null ? a.CC : 0,
                              BHYTDT = a != null ? a.BHYTDT : 0,
                              BHYTTT = a != null ? a.BHYTTT : 0,
                              DV = a != null ? a.DV : 0,
                              KSK = a != null ? a.KSK : 0,
                              BNVV = a != null ? a.BNVV : 0,
                              BNCV = a != null ? a.BNCV : 0,
                              TSDNCK = b.Tong - (a != null ? a.TSBNDK : 0)
                          }).ToList();
                BaoCao.rep_BCGiaoBan_NgT rep = new BaoCao.rep_BCGiaoBan_NgT();
                frmIn frm = new frmIn();
                rep.DataSource = ds.OrderBy(p => p.TenKP).ToList();
                rep.ngaythang.Value = "Từ ngày " + dateTuNgay.Text + " đến ngày " + dateDenNgay.Text;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion
            #region ngoại trú_rút gọn
            else if (radmauin.SelectedIndex == 2)
            {
                var bn = (from a in data.BenhNhans.Where(p => p.NNhap >= tungay && p.NNhap <= denngay)
                          join b in data.BNKBs on a.MaBNhan equals b.MaBNhan into kq
                          from k in kq.DefaultIfEmpty()
                          select new { a, idkb = k != null ? k.IDKB : 0 }).ToList();
                var bn1 = (from a in bn group a by new { a.a.MaBNhan, a.a.MaKP, a.a.DTuong } into kq select new { kq.Key, idkb = kq.Min(p => p.idkb) }).ToList();
                var ds1 = (from a in bn1
                           join b in data.BNKBs on a.idkb equals b.IDKB into k
                           from k1 in k.DefaultIfEmpty()
                           select new
                           {
                               a.Key.MaBNhan,
                               a.Key.DTuong,
                               MaKP = k1 != null ? k1.MaKP : a.Key.MaKP
                           }).ToList();
                //var dsss = (from a in ds1
                //            join b in data.KPhongs on a.MaKP equals b.MaKP
                //            group new { a, b } by new { a.MaKP, b.TenKP } into kq
                //            select new
                //            {
                //                kq.Key.MaKP,
                //                kq.Key.TenKP,
                //                TSBNTN = kq.Count(),
                //                BHYT = kq.Where(p => p.a.DTuong == "BHYT"),
                //                DV = kq.Where(p => p.a.DTuong == "DV")
                //            }).ToList;
                var _lkp = data.KPhongs.ToList();
                var kqua = (from a in ds1
                            join kp in _lkp on a.MaKP equals kp.MaKP
                            group new { a, kp } by new { a.MaKP, kp.TenKP } into kq
                            select new
                            {
                                kq.Key.MaKP,
                                kq.Key.TenKP,
                                TSBNTN = kq.Where(p => p.a.DTuong == "BHYT" || p.a.DTuong == "Dịch vụ").Count(),
                                BHYT = kq.Where(p => p.a.DTuong == "BHYT").Count(),
                                DV = kq.Where(p => p.a.DTuong == "Dịch vụ").Count()
                            }).OrderBy(p => p.TenKP).ToList();
                BaoCao.rep_BCGiaoBan_NgT_Rutgon rep = new BaoCao.rep_BCGiaoBan_NgT_Rutgon();
                frmIn frm = new frmIn();
                rep.DataSource = kqua.ToList();
                rep.ngaythang.Value = "Từ ngày " + dateTuNgay.Text + " đến ngày " + dateDenNgay.Text;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            #endregion
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class kphong
        {
            int maKP, tong;

            public int Tong
            {
                get { return tong; }
                set { tong = value; }
            }

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
        }

        private void radmauin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}