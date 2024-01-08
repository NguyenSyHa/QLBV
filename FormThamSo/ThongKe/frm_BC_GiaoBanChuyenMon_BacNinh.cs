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
    public partial class frm_BC_GiaoBanChuyenMon_BacNinh : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_GiaoBanChuyenMon_BacNinh()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_BC_GiaoBanChuyenMon_BacNinh_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now.Date;
            lupngayden.DateTime = DateTime.Now.Date.AddDays(1);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = lupNgaytu.DateTime;
            DateTime denngay = lupngayden.DateTime;

            var bn1 = (from a in data.BenhNhans.Where(p => p.NoiTru == 1).Where(p => p.MaKCB == DungChung.Bien.MaBV)
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
            var bn2 = (from a in bn1.Where(p => p.NgayKham <= denngay).Where(p => p.NgayRa >denngay || p.NgayRa == null)
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
                           kq.Key.DTuong,
                           IDKB = kq.Max(p => p.IDKB)
                       }).ToList();
            var query21 = (from b in bn2
                          join a in data.BNKBs on b.IDKB equals a.IDKB
                          join c in data.KPhongs on a.MaKP equals c.MaKP
                           join d in data.TTboXungs on a.MaBNhan equals d.MaBNhan
                          // group new { a, b, c,d } by new { c.MaKP, c.TenKP } into kq
                          select new
                          {
                              c.MaKP,
                              c.TenKP,
                              a.MaBNhan,
                              b.DTuong,
                              d.HTThanhToan
                          }).ToList();
            var query2 = (from b in query21
                          group new {b} by new { b.MaKP, b.TenKP } into kq
                          select new
                          {
                              kq.Key.MaKP,
                              kq.Key.TenKP,
                              BNCu = 0,
                              BNVV = 0,
                              BNRaVien = 0,
                              BNRaVien1 = 0,
                              BNChuyenVien = 0,
                              BNBHYTHienCo = kq.Where(p => p.b.DTuong == "BHYT" && p.b.HTThanhToan == 0).Count(),
                              BNDVHienCo = kq.Where(p => p.b.DTuong == "Dịch vụ" && p.b.HTThanhToan == 0).Count(),
                              BNCKHienCo = kq.Where(p => p.b.HTThanhToan == 1).Count(),
                              BNHienCo = kq.Count()
                          }).ToList();
            var bn3 = (from a in bn1.Where(p => p.NgayKham < tungay).Where(p => p.NgayRa > tungay || p.NgayRa == null)
                       group a by new
                       {
                           a.MaBNhan,
                           a.NgayVao,
                           a.Status,
                           a.NgayRa,
                       } into kq
                       select new
                       {
                           kq.Key.MaBNhan,
                           kq.Key.NgayVao,
                           kq.Key.Status,
                           kq.Key.NgayRa,
                           IDKB = kq.Max(p => p.IDKB)
                       }).ToList();
            var query3 = (from b in bn3
                          join a in data.BNKBs on b.IDKB equals a.IDKB
                          join c in data.KPhongs on a.MaKP equals c.MaKP
                          group new { a, b, c } by new { c.MaKP, c.TenKP } into kq
                          select new
                          {
                              kq.Key.MaKP,
                              kq.Key.TenKP,
                              BNCu = kq.Count(),
                              BNVV = 0,
                              BNRaVien = 0,
                              BNRaVien1 = 0,
                              BNChuyenVien = 0,
                              BNBHYTHienCo = 0,
                              BNDVHienCo = 0,
                              BNCKHienCo = 0,
                              BNHienCo = 0
                          }).ToList();
            var query4 = query3.Concat(query2);
            var qbn = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                                                .Where(p => p.MaKCB == DungChung.Bien.MaBV)
                       join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                       join kp in data.KPhongs on vv.MaKP equals kp.MaKP//.Where(p => p.PLoai.Equals("Lâm sàng"))
                       select new
                       {
                           kp.TenKP,
                           kp.MaKP,
                           bn.MaBNhan,
                           vv.NgayVao,
                           bn.Status,
                           //NgayRa = kq1 == null ? null : kq1.NgayRa,
                           
                       }).ToList();

            var query = (from b in qbn
                         group b by new {b.MaKP, b.TenKP } into kq
                         select new
                         {
                             kq.Key.MaKP,
                             kq.Key.TenKP,
                             BNCu = 0,
                             BNVV = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Select(p => p.MaBNhan).Distinct().Count(),
                             BNRaVien = 0,
                             BNRaVien1 = 0,
                             BNChuyenVien = 0,
                             BNBHYTHienCo = 0,
                             BNDVHienCo = 0,
                             BNCKHienCo = 0,
                             BNHienCo = 0
                         }).ToList();
            var qbn1 = (from bn in data.BenhNhans.Where(p => p.NoiTru == 1)
                                                .Where(p => p.MaKCB == DungChung.Bien.MaBV)
                       join vv in data.RaViens on bn.MaBNhan equals vv.MaBNhan
                       join kp in data.KPhongs on vv.MaKP equals kp.MaKP//.Where(p => p.PLoai.Equals("Lâm sàng"))
                       select new
                       {
                           kp.TenKP,
                           kp.MaKP,
                           bn.MaBNhan,
                           vv.NgayRa,
                           bn.Status,
                           PhuongAn = vv.Status
                       }).ToList();

            var query1 = (from b in qbn1
                         group b by new { b.MaKP, b.TenKP } into kq
                         select new
                         {
                             kq.Key.MaKP,
                             kq.Key.TenKP,
                             BNCu = 0,
                             BNVV = 0,
                             BNRaVien = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Select(p => p.MaBNhan).Distinct().Count(),
                             BNRaVien1 = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.PhuongAn != 1).Select(p => p.MaBNhan).Distinct().Count(),
                             BNChuyenVien = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Where(p => p.PhuongAn == 1).Select(p => p.MaBNhan).Distinct().Count(),
                             BNBHYTHienCo = 0,
                             BNDVHienCo = 0,
                             BNCKHienCo = 0,
                             BNHienCo = 0
                         }).ToList();
            var query5 = query1.Concat(query);
            var query6 = query5.Concat(query4);
            var ds = (from a in query6
                      group a by new { a.MaKP, a.TenKP } into kq
                      select new
                      {
                          kq.Key.MaKP,
                          kq.Key.TenKP,
                          BNCu = kq.Sum(p => p.BNCu),
                          BNVV = kq.Sum(p => p.BNVV),
                          BNRaVien = kq.Sum(p => p.BNRaVien),
                          BNRaVien1 = kq.Sum(p => p.BNRaVien1),
                          BNChuyenVien = kq.Sum(p => p.BNChuyenVien),
                          BNBHYTHienCo = kq.Sum(p => p.BNBHYTHienCo),
                          BNDVHienCo = kq.Sum(p => p.BNDVHienCo),
                          BNCKHienCo = kq.Sum(p => p.BNCKHienCo),
                          BNHienCo = kq.Sum(p => p.BNHienCo)
                      }).Where(p => p.BNCu != 0 || p.BNVV != 0 || p.BNRaVien != 0 || p.BNBHYTHienCo != 0 || p.BNDVHienCo != 0 || p.BNCKHienCo != 0 || p.BNHienCo != 0).ToList();

            if(radioGroup1.SelectedIndex == 0)
            {
                BaoCao.rep_BC_GiaoBanChuyenMon_BacNinh rep = new BaoCao.rep_BC_GiaoBanChuyenMon_BacNinh();
                frmIn frm = new frmIn();
                rep.DataSource = ds.OrderBy(p => p.TenKP).ToList();
                rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy HH:mm") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy HH:mm");
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                BaoCao.rep_BC_GiaoBanChuyenMon_01071 rep = new BaoCao.rep_BC_GiaoBanChuyenMon_01071();
                frmIn frm = new frmIn();
                rep.DataSource = ds.OrderBy(p => p.TenKP).ToList();
                rep.celNgayThang.Text = "Từ ngày " + lupNgaytu.DateTime.ToString("dd/MM/yyyy HH:mm") + " đến ngày " + lupngayden.DateTime.ToString("dd/MM/yyyy HH:mm");
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
    }
}