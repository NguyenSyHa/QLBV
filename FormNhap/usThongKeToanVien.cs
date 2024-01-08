using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class usThongKeToanVien : DevExpress.XtraEditors.XtraUserControl
    {
        public usThongKeToanVien()
        {
            InitializeComponent();
        }

        private void usThongKeToanVien_Load(object sender, EventArgs e)
        {
            dtdenngay.DateTime = DateTime.Now;
            dtdenngay_EditValueChanged(sender, e);
            
            //gridView1_CellValueChanged(null, null);
            //gridView2_CellValueChanged(null, null);
            //gridView3_CellValueChanged(null, null);
            //gridView4_CellValueChanged(null, null);
            //DateTime ngay=DateTime.Now;
            //List<GetTong> ketqua1 = new List<GetTong>();
            //ketqua1 = GetTongThu(ngay);
            //gridControl1.DataSource = ketqua1;

            //List<GetTong> ketqua2 = new List<GetTong>();
            //ketqua2 = GetTongKham(ngay);
            //gridControl2.DataSource = ketqua2;

            //List<GetTong> ketqua3 = new List<GetTong>();
            //ketqua3 = GetTongVaoVien(ngay);
            //gridControl3.DataSource = ketqua3;

            //List<GetTong> ketqua4 = new List<GetTong>();
            //ketqua4 = GetTongRaVien(ngay);
            //gridControl4.DataSource = ketqua4;

            //GetTonKho(ngay);
        }
        public class CaptionGrid
        {
            public string NameCol { get; set; }
            public int STT { get; set; }
        }

        public class GetData
        {
            public string Dtuong { get; set; }
            public string Ngay { get; set; }
            public double SoTien { get; set; }
        }

        private List<CaptionGrid> GetNameCaption(DateTime Date)
        {
            List<CaptionGrid> ListName = new List<CaptionGrid>();
            for (int i = 0; i < 11; i++)
            {
                CaptionGrid moi = new CaptionGrid();
                moi.STT = i;
                moi.NameCol = i > 0 ? Date.AddDays(-(i-1)).ToShortDateString() : "Đối tượng";
                ListName.Add(moi);
            }
            return ListName.OrderBy(p=>p.STT).ToList();
        }
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DateTime Ngay = dtdenngay.DateTime;
            List<CaptionGrid> ListName = new List<CaptionGrid>();
            ListName = GetNameCaption(Ngay);

            for (int j = 0; j < gridView1.Columns.Count-1; j++)
            {
                string ten = ListName.Skip(j).First().NameCol;
                gridView1.Columns[j].Caption = ten;
            }

        }

     

       
      
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private List<GetTong> GetTongThu(DateTime ngay)
        {
            List<GetTong> kqs = new List<GetTong>();
            List<GetData> ketqua = new List<GetData>();
            DateTime tungay = DungChung.Ham.NgayTu(ngay.AddDays(-10));
            var _lvienphi = (from vp in _data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= ngay)
                             join vpct in _data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                             select new { vp.MaBNhan, vpct.idVPhict, vpct.TienBN,vp.NgayTT }).ToList();
            DateTime ngaynhap = DungChung.Ham.NgayTu(ngay.AddMonths(-2));
            var _lbenhnhan = (from bn in _data.BenhNhans.Where(p => p.NNhap >= ngaynhap && p.NNhap <= ngay)
                              select new { bn.MaBNhan, bn.IDDTBN,bn.DTuong }).ToList();
            var _ldtuong = _data.DTBNs.Where(p => p.Status == 1).ToList();
            DateTime _ngaytu = DateTime.Now;
            DateTime _ngayden = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                _ngaytu = DungChung.Ham.NgayTu(ngay.AddDays(-i));
                _ngayden = DungChung.Ham.NgayDen(ngay.AddDays(-i));
                var _ltongtien = (from vp in _lvienphi.Where(p => p.NgayTT >= _ngaytu && p.NgayTT <= _ngayden)
                                  join bn in _lbenhnhan on vp.MaBNhan equals bn.MaBNhan
                                  group new { vp, bn } by new { bn.DTuong } into kq
                                  select new
                                  {
                                      Dtuong = kq.Key.DTuong,
                                      Ngay = _ngaytu.ToShortDateString(),
                                      SoTien = kq.Sum(p => p.vp.TienBN)
                                  }).ToList();
                var _lks = (from dt in _ldtuong
                            join tt in _ltongtien on dt.DTBN1 equals tt.Dtuong into k1
                            from k in k1.DefaultIfEmpty()
                            select new GetData
                            {
                                Dtuong=dt.DTBN1,
                                Ngay = _ngaytu.ToShortDateString(),
                                SoTien = k1 == null ? 0 : k1.Sum(p => p.SoTien)
                            }).ToList();
                ketqua.AddRange(_lks);
            }
            var list1 = ketqua.Where(p => p.Dtuong == "BHYT").ToList();
            GetTong moi = new GetTong();
            moi.Nhom = "1. Tổng thu(Tiền BN chi trả)";
            moi.DTuong = list1.First().Dtuong.ToString();
            moi.day1 = list1.First().SoTien.ToString("0,###");
            moi.day2 = list1.Skip(1).First().SoTien.ToString("0,###");
            moi.day3 = list1.Skip(2).First().SoTien.ToString("0,###");
            moi.day4 = list1.Skip(3).First().SoTien.ToString("0,###");
            moi.day5 = list1.Skip(4).First().SoTien.ToString("0,###");
            moi.day6 = list1.Skip(5).First().SoTien.ToString("0,###");
            moi.day7 = list1.Skip(6).First().SoTien.ToString("0,###");
            moi.day8 = list1.Skip(7).First().SoTien.ToString("0,###");
            moi.day9 = list1.Skip(8).First().SoTien.ToString("0,###");
            moi.day10 = list1.Skip(9).First().SoTien.ToString("0,###");
            kqs.Add(moi);
            var list2 = ketqua.Where(p => p.Dtuong == "Dịch vụ").ToList();
            GetTong moi2 = new GetTong();
            moi2.Nhom = "1. Tổng thu(Tiền BN chi trả)";
            moi2.DTuong = list2.First().Dtuong.ToString();
            moi2.day1 = list2.First().SoTien.ToString("0,###");
            moi2.day2 = list2.Skip(1).First().SoTien.ToString("0,###");
            moi2.day3 = list2.Skip(2).First().SoTien.ToString("0,###");
            moi2.day4 = list2.Skip(3).First().SoTien.ToString("0,###");
            moi2.day5 = list2.Skip(4).First().SoTien.ToString("0,###");
            moi2.day6 = list2.Skip(5).First().SoTien.ToString("0,###");
            moi2.day7 = list2.Skip(6).First().SoTien.ToString("0,###");
            moi2.day8 = list2.Skip(7).First().SoTien.ToString("0,###");
            moi2.day9 = list2.Skip(8).First().SoTien.ToString("0,###");
            moi2.day10 = list2.Skip(9).First().SoTien.ToString("0,###");
            kqs.Add(moi2);
            var lis3 = (from a in ketqua
                        group a by new { a.Ngay } into kq
                        select new
                        {
                            kq.Key.Ngay,
                            SoTien = kq.Sum(p => p.SoTien)
                        }).ToList();
            GetTong moi3 = new GetTong();
            moi3.Nhom = "1. Tổng thu(Tiền BN chi trả)";
            moi3.DTuong = "Tổng";
            moi3.day1 = lis3.First().SoTien.ToString("0,###");
            moi3.day2 = lis3.Skip(1).First().SoTien.ToString("0,###");
            moi3.day3 = lis3.Skip(2).First().SoTien.ToString("0,###");
            moi3.day4 = lis3.Skip(3).First().SoTien.ToString("0,###");
            moi3.day5 = lis3.Skip(4).First().SoTien.ToString("0,###");
            moi3.day6 = lis3.Skip(5).First().SoTien.ToString("0,###");
            moi3.day7 = lis3.Skip(6).First().SoTien.ToString("0,###");
            moi3.day8 = lis3.Skip(7).First().SoTien.ToString("0,###");
            moi3.day9 = lis3.Skip(8).First().SoTien.ToString("0,###");
            moi3.day10 = lis3.Skip(9).First().SoTien.ToString("0,###");
            kqs.Add(moi3);
            return kqs;
        }
        private List<GetTong> GetTongKham(DateTime ngay)
        {
            List<GetTong> kqs = new List<GetTong>();
            List<GetData> ketqua = new List<GetData>();
            DateTime tungay = DungChung.Ham.NgayTu(ngay.AddDays(-10));
            DateTime ngaynhap = DungChung.Ham.NgayTu(ngay.AddMonths(-2));
            DateTime _ngaytu = DateTime.Now;
            DateTime _ngayden = DateTime.Now;
            var _lkham = (from vp in _data.BNKBs.Where(p => p.NgayKham >= tungay && p.NgayKham <= ngay)
                          join kp in _data.KPhongs.Where(p => p.PLoai.Contains("Phòng khám")) on vp.MaKP equals kp.MaKP
                          select new { vp.MaBNhan, vp.NgayKham }).Distinct().ToList();

            var _lbenhnhan = (from bn in _data.BenhNhans.Where(p => p.NNhap >= ngaynhap && p.NNhap <= ngay).Where(p => p.NoiTru == 0)
                              select new { bn.MaBNhan, bn.IDDTBN, bn.DTuong }).ToList();
            var _ldtuong = _data.DTBNs.Where(p => p.Status == 1).ToList();
            for (int i = 0; i < 10; i++)
            {
                 _ngaytu = DungChung.Ham.NgayTu(ngay.AddDays(-i));
                _ngayden = DungChung.Ham.NgayDen(ngay.AddDays(-i));
                var list = (from a in _lkham.Where(p => p.NgayKham >= _ngaytu && p.NgayKham <= _ngayden)
                             join b in _lbenhnhan on a.MaBNhan equals b.MaBNhan
                             group new { a, b } by new { b.DTuong } into kq
                             select new GetData
                             {
                                 Dtuong = kq.Key.DTuong,
                                 Ngay = _ngaytu.ToShortDateString(),
                                 SoTien = kq.Select(p => p.a.MaBNhan).Distinct().Count()
                             }).ToList();
                var _lks = (from dt in _ldtuong
                            join tt in list on dt.DTBN1 equals tt.Dtuong into k1
                            from k in k1.DefaultIfEmpty()
                            select new GetData
                            {
                                Dtuong = dt.DTBN1,
                                Ngay = _ngaytu.ToShortDateString(),
                                SoTien = k1 == null ? 0 : k1.Sum(p => p.SoTien)
                            }).ToList();
                ketqua.AddRange(_lks);
            }
            var list1 = ketqua.Where(p => p.Dtuong == "BHYT").ToList();
            GetTong moi = new GetTong();
            moi.Nhom = "2. Tổng số bệnh nhân khám bệnh ngoại trú";
            moi.DTuong = list1.First().Dtuong.ToString();
            moi.day1 = list1.First().SoTien.ToString("0.###");
            moi.day2 = list1.Skip(1).First().SoTien.ToString("0.###");
            moi.day3 = list1.Skip(2).First().SoTien.ToString("0.###");
            moi.day4 = list1.Skip(3).First().SoTien.ToString("0.###");
            moi.day5 = list1.Skip(4).First().SoTien.ToString("0.###");
            moi.day6 = list1.Skip(5).First().SoTien.ToString("0.###");
            moi.day7 = list1.Skip(6).First().SoTien.ToString("0.###");
            moi.day8 = list1.Skip(7).First().SoTien.ToString("0.###");
            moi.day9 = list1.Skip(8).First().SoTien.ToString("0.###");
            moi.day10 = list1.Skip(9).First().SoTien.ToString("0.###");
            kqs.Add(moi);
            var list2 = ketqua.Where(p => p.Dtuong == "Dịch vụ").ToList();
            GetTong moi2 = new GetTong();
            moi2.Nhom = "2. Tổng số bệnh nhân khám bệnh ngoại trú";
            moi2.DTuong = list2.First().Dtuong.ToString();
            moi2.day1 = list2.First().SoTien.ToString("0.###");
            moi2.day2 = list2.Skip(1).First().SoTien.ToString("0.###");
            moi2.day3 = list2.Skip(2).First().SoTien.ToString("0.###");
            moi2.day4 = list2.Skip(3).First().SoTien.ToString("0.###");
            moi2.day5 = list2.Skip(4).First().SoTien.ToString("0.###");
            moi2.day6 = list2.Skip(5).First().SoTien.ToString("0.###");
            moi2.day7 = list2.Skip(6).First().SoTien.ToString("0.###");
            moi2.day8 = list2.Skip(7).First().SoTien.ToString("0.###");
            moi2.day9 = list2.Skip(8).First().SoTien.ToString("0.###");
            moi2.day10 = list2.Skip(9).First().SoTien.ToString("0.###");
            kqs.Add(moi2);
            var lis3 = (from a in ketqua
                        group a by new { a.Ngay } into kq
                        select new
                        {
                            kq.Key.Ngay,
                            SoTien = kq.Sum(p => p.SoTien)
                        }).ToList();
            GetTong moi3 = new GetTong();
            moi3.Nhom = "2. Tổng số bệnh nhân khám bệnh ngoại trú";
            moi3.DTuong = "Tổng";
            moi3.day1 = lis3.First().SoTien.ToString("0.###");
            moi3.day2 = lis3.Skip(1).First().SoTien.ToString("0.###");
            moi3.day3 = lis3.Skip(2).First().SoTien.ToString("0.###");
            moi3.day4 = lis3.Skip(3).First().SoTien.ToString("0.###");
            moi3.day5 = lis3.Skip(4).First().SoTien.ToString("0.###");
            moi3.day6 = lis3.Skip(5).First().SoTien.ToString("0.###");
            moi3.day7 = lis3.Skip(6).First().SoTien.ToString("0.###");
            moi3.day8 = lis3.Skip(7).First().SoTien.ToString("0.###");
            moi3.day9 = lis3.Skip(8).First().SoTien.ToString("0.###");
            moi3.day10 = lis3.Skip(9).First().SoTien.ToString("0.###");
            kqs.Add(moi3);
            return kqs;
        }

        private List<GetTong> GetTongVaoVien(DateTime ngay)
        {
            List<GetTong> kqs = new List<GetTong>();
            List<GetData> ketqua = new List<GetData>();
            DateTime tungay = DungChung.Ham.NgayTu(ngay.AddDays(-10));
            DateTime ngaynhap = DungChung.Ham.NgayTu(ngay.AddMonths(-2));
            DateTime _ngaytu = DateTime.Now;
            DateTime _ngayden = DateTime.Now;
            var _lvaovien = (from vp in _data.VaoViens.Where(p => p.NgayVao >= tungay && p.NgayVao <= ngay)
                          select new { vp.MaBNhan, vp.NgayVao }).ToList();
            var _lbenhnhan = (from bn in _data.BenhNhans.Where(p => p.NNhap >= ngaynhap && p.NNhap <= ngay).Where(p => p.NoiTru == 1 || (p.NoiTru == 0 && p.DTNT == true))
                              select new { bn.MaBNhan, bn.IDDTBN, bn.DTuong }).ToList();
            var _ldtuong = _data.DTBNs.Where(p => p.Status == 1).ToList();
            for (int i = 0; i < 10; i++)
            {
                _ngaytu = DungChung.Ham.NgayTu(ngay.AddDays(-i));
                _ngayden = DungChung.Ham.NgayDen(ngay.AddDays(-i));
                var kq1 = (from vv in _lvaovien.Where(p => p.NgayVao >= _ngaytu && p.NgayVao <= _ngayden)
                           join bn in _lbenhnhan on vv.MaBNhan equals bn.MaBNhan
                           group new { vv, bn } by new { bn.DTuong } into kq
                           select new GetData
                           {
                               Dtuong = kq.Key.DTuong,
                               Ngay = _ngaytu.ToShortDateString(),
                               SoTien = kq.Select(p => p.vv.MaBNhan).Distinct().Count()
                           }).ToList();
                var _lks = (from dt in _ldtuong
                            join tt in kq1 on dt.DTBN1 equals tt.Dtuong into k1
                            from k in k1.DefaultIfEmpty()
                            select new GetData
                            {
                                Dtuong = dt.DTBN1,
                                Ngay = _ngaytu.ToShortDateString(),
                                SoTien = k1 == null ? 0 : k1.Sum(p => p.SoTien)
                            }).ToList();
                ketqua.AddRange(_lks);
            }
            var list1 = ketqua.Where(p => p.Dtuong == "BHYT").ToList();
            GetTong moi = new GetTong();
            moi.Nhom = "2. Tổng số bệnh nhân vào viện";
            moi.DTuong = list1.First().Dtuong.ToString();
            moi.day1 = list1.First().SoTien.ToString("0.###");
            moi.day2 = list1.Skip(1).First().SoTien.ToString("0.###");
            moi.day3 = list1.Skip(2).First().SoTien.ToString("0.###");
            moi.day4 = list1.Skip(3).First().SoTien.ToString("0.###");
            moi.day5 = list1.Skip(4).First().SoTien.ToString("0.###");
            moi.day6 = list1.Skip(5).First().SoTien.ToString("0.###");
            moi.day7 = list1.Skip(6).First().SoTien.ToString("0.###");
            moi.day8 = list1.Skip(7).First().SoTien.ToString("0.###");
            moi.day9 = list1.Skip(8).First().SoTien.ToString("0.###");
            moi.day10 = list1.Skip(9).First().SoTien.ToString("0.###");
            kqs.Add(moi);
            var list2 = ketqua.Where(p => p.Dtuong == "Dịch vụ").ToList();
            GetTong moi2 = new GetTong();
            moi2.Nhom = "2. Tổng số bệnh nhân vào viện";
            moi2.DTuong = list2.First().Dtuong.ToString();
            moi2.day1 = list2.First().SoTien.ToString("0.###");
            moi2.day2 = list2.Skip(1).First().SoTien.ToString("0.###");
            moi2.day3 = list2.Skip(2).First().SoTien.ToString("0.###");
            moi2.day4 = list2.Skip(3).First().SoTien.ToString("0.###");
            moi2.day5 = list2.Skip(4).First().SoTien.ToString("0.###");
            moi2.day6 = list2.Skip(5).First().SoTien.ToString("0.###");
            moi2.day7 = list2.Skip(6).First().SoTien.ToString("0.###");
            moi2.day8 = list2.Skip(7).First().SoTien.ToString("0.###");
            moi2.day9 = list2.Skip(8).First().SoTien.ToString("0.###");
            moi2.day10 = list2.Skip(9).First().SoTien.ToString("0.###");
            kqs.Add(moi2);
            var lis3 = (from a in ketqua
                        group a by new { a.Ngay } into kq
                        select new
                        {
                            kq.Key.Ngay,
                            SoTien = kq.Sum(p => p.SoTien)
                        }).ToList();
            GetTong moi3 = new GetTong();
            moi3.Nhom = "2. Tổng số bệnh nhân vào viện";
            moi3.DTuong = "Tổng";
            moi3.day1 = lis3.First().SoTien.ToString("0.###");
            moi3.day2 = lis3.Skip(1).First().SoTien.ToString("0.###");
            moi3.day3 = lis3.Skip(2).First().SoTien.ToString("0.###");
            moi3.day4 = lis3.Skip(3).First().SoTien.ToString("0.###");
            moi3.day5 = lis3.Skip(4).First().SoTien.ToString("0.###");
            moi3.day6 = lis3.Skip(5).First().SoTien.ToString("0.###");
            moi3.day7 = lis3.Skip(6).First().SoTien.ToString("0.###");
            moi3.day8 = lis3.Skip(7).First().SoTien.ToString("0.###");
            moi3.day9 = lis3.Skip(8).First().SoTien.ToString("0.###");
            moi3.day10 = lis3.Skip(9).First().SoTien.ToString("0.###");
            kqs.Add(moi3);
            return kqs;
        }

        private List<GetTong> GetTongRaVien(DateTime ngay)
        {
            List<GetTong> kqs = new List<GetTong>();
            List<GetData> ketqua = new List<GetData>();
            DateTime tungay = DungChung.Ham.NgayTu(ngay.AddDays(-10));
            DateTime ngaynhap = DungChung.Ham.NgayTu(ngay.AddMonths(-2));
            DateTime _ngaytu = DateTime.Now;
            DateTime _ngayden = DateTime.Now;
            var _lravien = (from vp in _data.RaViens.Where(p => p.NgayRa >= tungay && p.NgayRa <= ngay)
                             select new { vp.MaBNhan, vp.NgayRa }).ToList();
            var _lbenhnhan = (from bn in _data.BenhNhans.Where(p => p.NNhap >= ngaynhap && p.NNhap <= ngay).Where(p => p.NoiTru == 1 || (p.DTNT == true && p.NoiTru == 0))
                              select new { bn.MaBNhan, bn.IDDTBN, bn.DTuong }).ToList();



            var _ldtuong = _data.DTBNs.Where(p => p.Status == 1).ToList();
            for (int i = 0; i < 10; i++)
            {
                _ngaytu = DungChung.Ham.NgayTu(ngay.AddDays(-i));
                _ngayden = DungChung.Ham.NgayDen(ngay.AddDays(-i));
                var kq1 = (from vv in _lravien.Where(p => p.NgayRa >= _ngaytu && p.NgayRa <= _ngayden)
                           join bn in _lbenhnhan on vv.MaBNhan equals bn.MaBNhan
                           group new { vv, bn } by new { bn.DTuong } into kq
                           select new GetData
                           {
                               Dtuong = kq.Key.DTuong,
                               Ngay = _ngaytu.ToShortDateString(),
                               SoTien = kq.Select(p => p.vv.MaBNhan).Distinct().Count()
                           }).ToList();
                var _lks = (from dt in _ldtuong
                            join tt in kq1 on dt.DTBN1 equals tt.Dtuong into k1
                            from k in k1.DefaultIfEmpty()
                            select new GetData
                            {
                                Dtuong = dt.DTBN1,
                                Ngay = _ngaytu.ToShortDateString(),
                                SoTien = k1 == null ? 0 : k1.Sum(p => p.SoTien)
                            }).ToList();
                ketqua.AddRange(_lks);
            }
            var _lbenhnhan1 = (from bn in _data.BenhNhans.Where(p => p.NoiTru == 1|| (p.DTNT == true && p.NoiTru == 0))
                               join vv in _data.VaoViens.Where(p =>p.NgayVao <= ngay) on bn.MaBNhan equals vv.MaBNhan
                               select new { bn.MaBNhan, bn.DTuong,bn.NoiTru,bn.DTNT }).Distinct().ToList();
            var _lrvtoanvien = (from rv in _data.RaViens
                                select new { rv.MaBNhan, rv.NgayRa }).ToList();
            var _lchuara = (from a in _lbenhnhan1
                            join b in _lrvtoanvien on a.MaBNhan equals b.MaBNhan into k
                            from k1 in k.DefaultIfEmpty()
                            select new
                            {
                                a.NoiTru,
                                a.MaBNhan,
                                a.DTuong,
                                soluong = k1 != null ? 0 : 1
                            }).Distinct().ToList();
            int SLTong = 0, SLBHYT = 0, SLDichVu = 0, slnoitru = 0, slngoaitru = 0;
            if (_lchuara.Count > 0)
            {
                SLTong = _lchuara.Sum(p => p.soluong);
                SLBHYT = _lchuara.Where(p => p.DTuong == "BHYT").Sum(p => p.soluong);
                SLDichVu = _lchuara.Where(p => p.DTuong == "Dịch vụ").Sum(p => p.soluong);
                slnoitru = _lchuara.Where(p => p.NoiTru == 1).Sum(p => p.soluong);
                slngoaitru = _lchuara.Where(p => p.NoiTru == 0).Sum(p => p.soluong);

                labtongrv.Text = SLTong.ToString();
                labtongrvbhyt.Text = SLBHYT.ToString();
                tongrvdv.Text = SLDichVu.ToString();
                labnoitru.Text = slnoitru.ToString();
                labngoaitru.Text = slngoaitru.ToString();
            }
            else
            {
                labtongrv.Text = "0";
                labtongrvbhyt.Text = "0";
                tongrvdv.Text = "0";
                labnoitru.Text = "0";
                labngoaitru.Text = "0";
            }
            var list1 = ketqua.Where(p => p.Dtuong == "BHYT").ToList();
            GetTong moi = new GetTong();
            moi.Nhom = "3. Tổng số bệnh nhân ra viện";
            moi.DTuong = list1.First().Dtuong.ToString();
            moi.day1 = list1.First().SoTien.ToString("0.###");
            moi.day2 = list1.Skip(1).First().SoTien.ToString("0.###");
            moi.day3 = list1.Skip(2).First().SoTien.ToString("0.###");
            moi.day4 = list1.Skip(3).First().SoTien.ToString("0.###");
            moi.day5 = list1.Skip(4).First().SoTien.ToString("0.###");
            moi.day6 = list1.Skip(5).First().SoTien.ToString("0.###");
            moi.day7 = list1.Skip(6).First().SoTien.ToString("0.###");
            moi.day8 = list1.Skip(7).First().SoTien.ToString("0.###");
            moi.day9 = list1.Skip(8).First().SoTien.ToString("0.###");
            moi.day10 = list1.Skip(9).First().SoTien.ToString("0.###");
            kqs.Add(moi);
            var list2 = ketqua.Where(p => p.Dtuong == "Dịch vụ").ToList();
            GetTong moi2 = new GetTong();
            moi2.Nhom = "3. Tổng số bệnh nhân ra viện";
            moi2.DTuong = list2.First().Dtuong.ToString();
            moi2.day1 = list2.First().SoTien.ToString("0.###");
            moi2.day2 = list2.Skip(1).First().SoTien.ToString("0.###");
            moi2.day3 = list2.Skip(2).First().SoTien.ToString("0.###");
            moi2.day4 = list2.Skip(3).First().SoTien.ToString("0.###");
            moi2.day5 = list2.Skip(4).First().SoTien.ToString("0.###");
            moi2.day6 = list2.Skip(5).First().SoTien.ToString("0.###");
            moi2.day7 = list2.Skip(6).First().SoTien.ToString("0.###");
            moi2.day8 = list2.Skip(7).First().SoTien.ToString("0.###");
            moi2.day9 = list2.Skip(8).First().SoTien.ToString("0.###");
            moi2.day10 = list2.Skip(9).First().SoTien.ToString("0.###");
            kqs.Add(moi2);
            var lis3 = (from a in ketqua
                        group a by new { a.Ngay } into kq
                        select new
                        {
                            kq.Key.Ngay,
                            SoTien = kq.Sum(p => p.SoTien)
                        }).ToList();
            GetTong moi3 = new GetTong();
            moi3.Nhom = "3. Tổng số bệnh nhân ra viện";
            moi3.DTuong = "Tổng";
            moi3.day1 = lis3.First().SoTien.ToString("0.###");
            moi3.day2 = lis3.Skip(1).First().SoTien.ToString("0.###");
            moi3.day3 = lis3.Skip(2).First().SoTien.ToString("0.###");
            moi3.day4 = lis3.Skip(3).First().SoTien.ToString("0.###");
            moi3.day5 = lis3.Skip(4).First().SoTien.ToString("0.###");
            moi3.day6 = lis3.Skip(5).First().SoTien.ToString("0.###");
            moi3.day7 = lis3.Skip(6).First().SoTien.ToString("0.###");
            moi3.day8 = lis3.Skip(7).First().SoTien.ToString("0.###");
            moi3.day9 = lis3.Skip(8).First().SoTien.ToString("0.###");
            moi3.day10 = lis3.Skip(9).First().SoTien.ToString("0.###");
            kqs.Add(moi3);
            return kqs;
        }

        private void GetTonKho(DateTime ngay)
        {
            var _lkp = (from kp in _data.KPhongs.Where(p => p.PLoai.Contains("Khoa dược") && p.Status == 1) select new { kp.MaKP, kp.TenKP }).ToList();
            var _lton1 = (from nd in _data.NhapDs.Where(p => p.NgayNhap <= ngay)
                          join ndct in _data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          select new { nd.MaKP, nd.PLoai, ndct.ThanhTienN, ndct.ThanhTienX }).ToList();
                         
            var _lton = (from nd in _lton1
                         group new { nd } by new { nd.MaKP } into kq
                         select new
                         {
                             kq.Key.MaKP,
                             TongTien = kq.Where(p => p.nd.PLoai == 1).Sum(p => p.nd.ThanhTienN) - kq.Where(p => p.nd.PLoai == 2 || p.nd.PLoai == 3).Sum(p => p.nd.ThanhTienX)
                         }).ToList();
            var _lkq = (from kp in _lkp
                        join to in _lton on kp.MaKP equals to.MaKP
                        select new
                        {
                            kp.MaKP,
                            kp.TenKP,
                            to.TongTien
                        }).ToList();
            double Tong = 0;
            if(_lkq.Count>0)
                Tong = _lkq.Sum(p => p.TongTien);

            grcthuocton.DataSource = _lkq;
        }
        public class GetTong
        {
            public string Nhom { set; get; }
            public string DTuong { get; set; }
            public string day1 { get; set; }
            public string day2 { get; set; }
            public string day3 { get; set; }
            public string day4 { get; set; }
            public string day5 { get; set; }
            public string day6 { get; set; }
            public string day7 { get; set; }
            public string day8 { get; set; }
            public string day9 { get; set; }
            public string day10 { get; set; }
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl4_Click(object sender, EventArgs e)
        {

        }

        private void grcthuocton_Click(object sender, EventArgs e)
        {




        }

        private void dtdenngay_EditValueChanged(object sender, EventArgs e)
        {
            gridView1_CellValueChanged(null, null);
            List<GetTong> qTong = new List<GetTong>();
            DateTime ngay = dtdenngay.DateTime;
            qTong = GetTongThu(ngay);
            qTong.AddRange(GetTongKham(ngay));
            qTong.AddRange(GetTongVaoVien(ngay));
            qTong.AddRange(GetTongRaVien(ngay));
            gridControl1.DataSource = qTong;
            GetTonKho(ngay);
        }
    }
}
