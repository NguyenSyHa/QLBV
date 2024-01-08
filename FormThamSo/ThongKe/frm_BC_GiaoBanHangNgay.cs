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
    public partial class frm_BC_GiaoBanHangNgay : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_GiaoBanHangNgay()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private class BenhNhands
        {
            public string TenKP { get; set; }
            public int NoiTru { get; set; }
            public string khoa { get; set; }

            public double bnctt { get; set; }
            public double bncct { get; set; }
            public double bncvt { get; set; }
            public double bncdv { get; set; }
            public double bnctong { get; set; }

            public double bnvvtt { get; set; }
            public double bnvvct { get; set; }
            public double bnvvvt { get; set; }
            public double bnvvdv { get; set; }
            public double bnvvtong { get; set; }

            public double bnrvtt { get; set; }
            public double bnrvct { get; set; }
            public double bnrvvt { get; set; }
            public double bnrvdv { get; set; }
            public double bnrvtong { get; set; }

            public double bnhctt { get; set; }
            public double bnhcct { get; set; }
            public double bnhcvt { get; set; }
            public double bnhcdv { get; set; }
            public double bnhctong { get; set; }
        }
        List<BenhNhands> _benhnhan = new List<BenhNhands>();
        private void btnOK_Click(object sender, EventArgs e)
        {

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(date_tungay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(date_ngayden.DateTime);
            _benhnhan.Clear();
            if (tungay <= denngay)
            {
                //string tuyenbv = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).Select(P => P.TuyenBV).ToString();

                var taituyen = (from a in data.BenhNhans.Where(p => p.MaCS == DungChung.Bien.MaBV).Where(p => p.DTuong != "Dịch vụ")
                                join b in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on a.MaBNhan equals b.MaBNhan
                                group new { a, b } by new { a.MaBNhan } into kq
                                select new { kq.Key.MaBNhan }).ToList();

                var chuyentuyen = (from a in data.BenhNhans.Where(p => p.MaBV != null && p.MaCS != DungChung.Bien.MaBV).Where(p => p.DTuong != "Dịch vụ")
                                   join b in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on a.MaBNhan equals b.MaBNhan
                                   group new { a, b } by new { a.MaBNhan } into kq
                                   select new { kq.Key.MaBNhan }).ToList();

                var vuottuyen = (from a in data.BenhNhans.Where(p => p.MaBV == null && p.MaCS != DungChung.Bien.MaBV).Where(p => p.DTuong != "Dịch vụ")
                                 join b in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on a.MaBNhan equals b.MaBNhan
                                 group new { a, b } by new { a.MaBNhan } into kq
                                 select new { kq.Key.MaBNhan }).ToList();
                var dichvu = (from a in data.BenhNhans.Where(p => p.DTuong == "Dịch vụ")
                              join b in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on a.MaBNhan equals b.MaBNhan
                              group new { a, b } by new { a.MaBNhan } into kq
                              select new { kq.Key.MaBNhan }).ToList();

                var bnkb = (from a in data.BenhNhans
                            join b in data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= denngay) on a.MaBNhan equals b.MaBNhan
                            group new { a, b } by new { a.MaBNhan } into kq
                            select new { kq.Key.MaBNhan, IDKB = kq.Max(p => p.b.IDKB) }).ToList(); ;

                var bn = (from a in data.BenhNhans.Where(p => p.DTuong != "Khám sức khỏe")
                          join c in data.BenhViens on a.MaBV equals c.MaBV into kq
                          from kq1 in kq.DefaultIfEmpty()
                          group new { a, kq1 } by new
                          {
                              a.MaBNhan,
                              a.DTuong,
                              a.MaCS,
                              a.MaBV,
                              TuyenBV = kq1 == null ? "" : kq1.TuyenBV,
                              a.NoiTru,
                              a.DTNT,
                              a.MaKP
                          } into kq
                          select new
                          {
                              kq.Key.MaBNhan,
                              kq.Key.DTuong,
                              kq.Key.MaCS,
                              kq.Key.MaBV,
                              kq.Key.TuyenBV,
                              NoiTru = kq.Key.NoiTru ?? -1,
                              kq.Key.DTNT,
                              kq.Key.MaKP
                          });

                var benhnhan = (from a in bn
                                join b in data.VaoViens on a.MaBNhan equals b.MaBNhan
                                join c in data.RaViens on a.MaBNhan equals c.MaBNhan into kq
                                join d in data.KPhongs on b.MaKP equals d.MaKP
                                join ee in data.BNKBs on b.MaKP equals ee.MaKP
                                from kq1 in kq.DefaultIfEmpty()
                                where (a.MaBNhan == ee.MaBNhan)
                                group new { a, b } by new
                                {
                                    a.MaBNhan,
                                    b.NgayVao,
                                    NgayRa = kq1 != null ? kq1.NgayRa : null,
                                    a.DTuong,
                                    a.MaCS,
                                    a.MaBV,
                                    a.TuyenBV,
                                    a.NoiTru,
                                    b.MaKP,
                                    //ChanDoan = (ee.ChanDoan.Contains("mạn tính")) ? "mạn tính" : "",
                                    ChanDoan = (ee.MaICD == "E10" || ee.MaICD == "E11" || ee.MaICD == "I10" || ee.MaICD2 == "E10" || ee.MaICD2 == "E11" || ee.MaICD2 == "I10") ? "Bệnh án mãn tính" : "Bệnh án thường",
                                    TenKP = (a.NoiTru == 1) ? d.TenKP : ((ee.MaICD == "E10" || ee.MaICD == "E11" || ee.MaICD == "I10" || ee.MaICD2.Contains("E10") || ee.MaICD2.Contains("E11") || ee.MaICD2.Contains("I10")) ? "Ngoại trú 1 ( Bệnh án mãn tính)" : "Ngoại trú 2 ( Bệnh án thường)"),
                                    Status = kq1 != null ? kq1.Status : null,
                                    a.DTNT
                                } into kq
                                select new
                                {
                                    kq.Key.MaBNhan,
                                    kq.Key.NgayVao,
                                    kq.Key.NgayRa,
                                    kq.Key.DTuong,
                                    kq.Key.MaCS,
                                    kq.Key.MaBV,
                                    kq.Key.TuyenBV,
                                    kq.Key.NoiTru,
                                    kq.Key.MaKP,
                                    kq.Key.ChanDoan,
                                    kq.Key.TenKP,
                                    kq.Key.Status,
                                    kq.Key.DTNT,
                                    khoa = (kq.Key.NoiTru == 1) ? "Điều trị nội trú" : "Điều trị ngoại trú",
                                }).OrderByDescending(p => p.NgayVao).ToList();

                var bc = (from a in benhnhan
                          group a by new
                          {
                              a.NoiTru,
                              a.TenKP,
                              a.khoa
                          } into kq
                          select new
                          {
                              NoiTru = kq.Key.NoiTru,
                              TenKP = kq.Key.TenKP,
                              khoa = kq.Key.khoa,

                              //  Bệnh nhân cũ
                              bnctt = kq.Where(p => p.MaCS == DungChung.Bien.MaBV).Where(p => p.NgayVao < tungay && (p.NgayRa >= tungay || p.NgayRa == null)).Where(p => p.DTuong != "Dịch vụ").Distinct().Count(),
                              bncct = kq.Where(p => p.MaBV != null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayVao < tungay && (p.NgayRa >= tungay || p.NgayRa == null)).Distinct().Count(),
                              bncvt = kq.Where(p => p.MaBV == null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayVao < tungay && (p.NgayRa >= tungay || p.NgayRa == null)).Distinct().Count(),
                              bncdv = kq.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayVao < tungay && (p.NgayRa >= tungay || p.NgayRa == null)).Distinct().Count(),
                              bnctong = kq.Where(p => p.NgayVao < tungay && (p.NgayRa >= tungay || p.NgayRa == null)).Distinct().Count(),

                              // Bệnh nhân vào viện
                              bnvvtt = kq.Where(p => p.DTuong != "Dịch vụ").Where(p => p.MaCS == DungChung.Bien.MaBV).Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Distinct().Count(),
                              bnvvct = kq.Where(p => p.MaBV != null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Distinct().Count(),
                              bnvvvt = kq.Where(p => p.MaBV == null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Distinct().Count(),
                              bnvvdv = kq.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Distinct().Count(),
                              bnvvtong = kq.Where(p => p.NgayVao >= tungay && p.NgayVao <= denngay).Distinct().Count(),

                              // Bệnh Nhân ra viện
                              bnrvtt = kq.Where(p => p.DTuong != "Dịch vụ").Where(p => p.MaCS == DungChung.Bien.MaBV).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Distinct().Count(),
                              bnrvct = kq.Where(p => p.MaBV != null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Distinct().Count(),
                              bnrvvt = kq.Where(p => p.MaBV == null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Distinct().Count(),
                              bnrvdv = kq.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Distinct().Count(),
                              bnrvtong = kq.Where(p => p.NgayRa >= tungay && p.NgayRa <= denngay).Distinct().Count(),

                              // Bệnh nhân viện có 
                              bnhctt = kq.Where(p => p.MaCS == DungChung.Bien.MaBV).Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Where(p => p.DTuong != "Dịch vụ").Distinct().Count(),
                              bnhcct = kq.Where(p => p.MaBV != null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Distinct().Count(),
                              bnhcvt = kq.Where(p => p.MaBV == null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Distinct().Count(),
                              bnhcdv = kq.Where(p => p.MaBV == null && p.DTuong == "Dịch vụ").Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Distinct().Count(),
                              bnhctong = kq.Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Distinct().Count()

                          }).OrderBy(p => p.khoa).OrderBy(p => p.TenKP).ToList();

                if (DungChung.Bien.MaBV == "27022")
                {
                    foreach (var item in bc)
                    {
                        BenhNhands moi = new BenhNhands();
                        moi.NoiTru = item.NoiTru;
                        moi.TenKP = item.TenKP;
                        moi.khoa = item.khoa;

                        moi.bnctt = item.bnctt;
                        moi.bncct = item.bncct;
                        moi.bncvt = item.bncvt;
                        moi.bncdv = item.bncdv;
                        moi.bnctong = item.bnctong;

                        moi.bnvvtt = item.bnvvtt;
                        moi.bnvvct = item.bnvvct;
                        moi.bnvvvt = item.bnvvvt;
                        moi.bnvvdv = item.bnvvdv;
                        moi.bnvvtong = item.bnvvtong;

                        moi.bnrvtt = item.bnrvtt;
                        moi.bnrvct = item.bnrvct;
                        moi.bnrvvt = item.bnrvvt;
                        moi.bnrvdv = item.bnrvdv;
                        moi.bnrvtong = item.bnrvtong;

                        moi.bnhctt = 0;
                        moi.bnhcct = 0;
                        moi.bnhcvt = 0;
                        moi.bnhcdv = 0;
                        moi.bnhctong = 0;
                        _benhnhan.Add(moi);
                    }
                    //DateTime ngaytu1 = tungay.AddMonths(-6);
                    var bnkb1 = (from b in data.BNKBs.Where(p =>p.NgayKham <= denngay)
                                 group new { b } by new { b.MaBNhan } into kq
                                 select new { MaBNhan = kq.Key.MaBNhan ?? 0, IDKB = kq.Max(p => p.b.IDKB) }).ToList();

                    var bnkb2 = (from a in bnkb1
                                 join b in data.BNKBs on a.IDKB equals b.IDKB
                                 select new { a.MaBNhan, b.MaKP, b.MaICD, b.MaICD2 }).ToList();

                    benhnhan = (from d1 in bnkb2
                                join a in bn on d1.MaBNhan equals a.MaBNhan
                                join b in data.VaoViens on a.MaBNhan equals b.MaBNhan
                                join c in data.RaViens on a.MaBNhan equals c.MaBNhan into kq
                                join d in data.KPhongs on d1.MaKP equals d.MaKP
                                where(d1.MaKP == a.MaKP)
                                from kq1 in kq.DefaultIfEmpty()
                                group new { a, b } by new
                                {
                                    a.MaBNhan,
                                    b.NgayVao,
                                    NgayRa = kq1 != null ? kq1.NgayRa : null,
                                    a.DTuong,
                                    a.MaCS,
                                    a.MaBV,
                                    a.TuyenBV,
                                    a.NoiTru,
                                    b.MaKP,
                                    //ChanDoan = (ee.ChanDoan.Contains("mạn tính")) ? "mạn tính" : "",
                                    ChanDoan = (d1.MaICD == "E10" || d1.MaICD == "E11" || d1.MaICD == "I10" || d1.MaICD2 == "E10" || d1.MaICD2 == "E11" || d1.MaICD2 == "I10") ? "Bệnh án mãn tính" : "Bệnh án thường",
                                    TenKP = (a.NoiTru == 1) ? d.TenKP : ((d1.MaICD == "E10" || d1.MaICD == "E11" || d1.MaICD == "I10" || d1.MaICD2.Contains("E10") || d1.MaICD2.Contains("E11") || d1.MaICD2.Contains("I10")) ? "Ngoại trú 1 ( Bệnh án mãn tính)" : "Ngoại trú 2 ( Bệnh án thường)"),
                                    Status = kq1 != null ? kq1.Status : null,
                                    a.DTNT
                                } into kq
                                select new
                                {
                                    kq.Key.MaBNhan,
                                    kq.Key.NgayVao,
                                    kq.Key.NgayRa,
                                    kq.Key.DTuong,
                                    kq.Key.MaCS,
                                    kq.Key.MaBV,
                                    kq.Key.TuyenBV,
                                    kq.Key.NoiTru,
                                    kq.Key.MaKP,
                                    kq.Key.ChanDoan,
                                    kq.Key.TenKP,
                                    kq.Key.Status,
                                    kq.Key.DTNT,
                                    khoa = (kq.Key.NoiTru == 1) ? "Điều trị nội trú" : "Điều trị ngoại trú",
                                }).OrderByDescending(p => p.NgayVao).ToList();

                    bc = (from a in benhnhan
                          group a by new
                          {
                              a.NoiTru,
                              a.TenKP,
                              a.khoa
                          } into kq
                          select new
                          {
                              NoiTru = kq.Key.NoiTru,
                              TenKP = kq.Key.TenKP,
                              khoa = kq.Key.khoa,

                              //  Bệnh nhân cũ
                              bnctt = 0,
                              bncct = 0,
                              bncvt = 0,
                              bncdv = 0,
                              bnctong = 0,

                              // Bệnh nhân vào viện
                              bnvvtt = 0,
                              bnvvct = 0,
                              bnvvvt = 0,
                              bnvvdv = 0,
                              bnvvtong = 0,

                              // Bệnh Nhân ra viện
                              bnrvtt = 0,
                              bnrvct = 0,
                              bnrvvt = 0,
                              bnrvdv = 0,
                              bnrvtong = 0,

                              // Bệnh nhân viện có 
                              bnhctt = kq.Where(p => p.MaCS == DungChung.Bien.MaBV).Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Where(p => p.DTuong != "Dịch vụ").Distinct().Count(),
                              bnhcct = kq.Where(p => p.MaBV != null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Distinct().Count(),
                              bnhcvt = kq.Where(p => p.MaBV == null && p.DTuong != "Dịch vụ" && p.MaCS != DungChung.Bien.MaBV).Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Distinct().Count(),
                              bnhcdv = kq.Where(p => p.MaBV == null && p.DTuong == "Dịch vụ").Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Distinct().Count(),
                              bnhctong = kq.Where(p => p.NgayVao <= denngay && (p.NgayRa > denngay || p.NgayRa == null)).Distinct().Count()

                          }).OrderBy(p => p.khoa).OrderBy(p => p.TenKP).ToList();
                    foreach (var item in bc)
                    {
                        foreach (var item2 in _benhnhan)
                        {
                            if (item.NoiTru == item.NoiTru && item.TenKP == item2.TenKP && item.khoa == item2.khoa)
                            {
                                item2.bnhctt = item.bnhctt;
                                item2.bnhcct = item.bnhcct;
                                item2.bnhcvt = item.bnhcvt;
                                item2.bnhcdv = item.bnhcdv;
                                item2.bnhctong = item.bnhctong;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in bc)
                    {
                        BenhNhands moi = new BenhNhands();
                        moi.NoiTru = item.NoiTru;
                        moi.TenKP = item.TenKP;
                        moi.khoa = item.khoa;

                        moi.bnctt = item.bnctt;
                        moi.bncct = item.bncct;
                        moi.bncvt = item.bncvt;
                        moi.bncdv = item.bncdv;
                        moi.bnctong = item.bnctong;

                        moi.bnvvtt = item.bnvvtt;
                        moi.bnvvct = item.bnvvct;
                        moi.bnvvvt = item.bnvvvt;
                        moi.bnvvdv = item.bnvvdv;
                        moi.bnvvtong = item.bnvvtong;

                        moi.bnrvtt = item.bnrvtt;
                        moi.bnrvct = item.bnrvct;
                        moi.bnrvvt = item.bnrvvt;
                        moi.bnrvdv = item.bnrvdv;
                        moi.bnrvtong = item.bnrvtong;

                        moi.bnhctt = item.bnhctt;
                        moi.bnhcct = item.bnhcct;
                        moi.bnhcvt = item.bnhcvt;
                        moi.bnhcdv = item.bnhcdv;
                        moi.bnhctong = item.bnhctong;
                        _benhnhan.Add(moi);
                    }

                }
                BaoCao.rep_BC_GiaoBanHangNgay rep = new BaoCao.rep_BC_GiaoBanHangNgay();
                frmIn frm = new frmIn();
                rep.tungay.Value = date_tungay.Text;
                rep.denngay.Value = date_ngayden.Text;
                rep.taituyen.Value = taituyen.Count();
                rep.chuyentuyen.Value = chuyentuyen.Count();
                rep.vuottuyen.Value = vuottuyen.Count();
                rep.dichvu.Value = dichvu.Count();
                rep.Tongsokham.Value = bnkb.Count();
                rep.DataSource = _benhnhan;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Từ ngày không được lớn hơn ngày đến", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void frm_BC_GiaoBanHangNgay_Load(object sender, EventArgs e)
        {
            date_ngayden.DateTime = System.DateTime.Now;
            date_tungay.DateTime = System.DateTime.Now;
        }
    }
}