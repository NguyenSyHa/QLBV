using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.DungChung;
using System.Data.SqlClient;
namespace QLBV.ChucNang
{
    public partial class frm_KKTuDong : DevExpress.XtraEditors.XtraForm
    {
        public frm_KKTuDong()
        {
            InitializeComponent();
        }
        int _kieu = 0;
        //1 kiểm kê tự đông
        //2 tạo sử dụng tự động
        public frm_KKTuDong(int k)
        {
            InitializeComponent();
            _kieu = k;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime _dtden = System.DateTime.Now;
        DateTime _dttu = System.DateTime.Now;
        int _makho = 0, _makhobc;
        int _ploai = 0;
        private class c_duoc
        {
            private int madv;
            private string donvi, macc, tendv, solo;
            private double dongia, soluong, thanhtien;
            private DateTime? handung;
            public DateTime? HanDung
            {
                set { handung = value; }
                get { return handung; }
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
            public string TenDV
            {
                set { tendv = value; }
                get { return tendv; }
            }
            public string SoLo
            {
                set { solo = value; }
                get { return solo; }
            }
            public string DonVi
            {
                set { donvi = value; }
                get { return donvi; }
            }
            public string MaCC
            {
                set { macc = value; }
                get { return macc; }
            }
            public double DonGia
            {
                set { dongia = value; }
                get { return dongia; }
            }
            public double SoLuongX
            {
                set { soluong = value; }
                get { return soluong; }
            }
            public double ThanhTienX
            {
                set { thanhtien = value; }
                get { return thanhtien; }
            }

            public double SoLuongN { get; set; }

            public double ThanhTienN { get; set; }
        }
        private class s_lo
        {
            private int madv;
            private string solo;
            private double dongia;
            private DateTime? handung;
            public DateTime? HanDung
            {
                set { handung = value; }
                get { return handung; }
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
            public string SoLo
            {
                set { solo = value; }
                get { return solo; }
            }
            public double DonGia
            {
                set { dongia = value; }
                get { return dongia; }
            }
        }
        List<c_duoc> _lduoc = new List<c_duoc>();
        List<s_lo> _sl = new List<s_lo>();
        #region hàm tìm kiếm
        private void TimKiem()
        {
            _lduoc.Clear();
            _makhobc = 0; _makho = 0; _ploai = 0;
            if (lupPloai.EditValue != null)
                _ploai = Convert.ToInt32(lupPloai.EditValue);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            _dttu = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            if (lupKhobc.EditValue != null)
                _makhobc = Convert.ToInt32(lupKhobc.EditValue);
            if (lupTimMaKP.EditValue != null)
                _makho = Convert.ToInt32(lupTimMaKP.EditValue);

            var qdv = _ldv.Where(p => _ploai == -1 || p.IDNhom == _ploai).ToList();
            switch (_kieu)
            {
                case 1:
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        var qkq1 = (from nhapd in _data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap <= _dtden).Where(p => p.PLoai == 1 || p.PLoai == 2 || (ckHuHao.Checked && p.PLoai == 3))
                                    join nhapdct in _data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                    select new { nhapdct.MaDV, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.ThanhTienN, nhapdct.SoLuongX, nhapdct.ThanhTienX, nhapdct.SoLo, nhapdct.HanDung }).ToList();

                        _lduoc = (from d in qkq1
                                  group d by new { d.MaDV, d.DonGia, d.SoLo, d.HanDung } into kq
                                  select new c_duoc
                                  {
                                      MaDV = kq.Key.MaDV ?? 0,
                                      DonGia = kq.Key.DonGia,
                                      SoLo = kq.Key.SoLo,
                                      HanDung = kq.Key.HanDung,
                                      SoLuongX = (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                      ThanhTienX = (kq.Sum(p => p.ThanhTienN) - kq.Sum(p => p.ThanhTienX)),
                                  }).ToList();

                        _lduoc = (from d in _lduoc
                                  join dv in qdv on d.MaDV equals dv.MaDV
                                  select new c_duoc()
                                  {
                                      MaDV = d.MaDV,
                                      DonVi = dv.DonVi,
                                      DonGia = d.DonGia,
                                      TenDV = dv.TenDV,
                                      SoLo = d.SoLo,
                                      MaCC = dv.MaCC,
                                      SoLuongX = d.SoLuongX,
                                      ThanhTienX = d.ThanhTienX,
                                      HanDung = d.HanDung
                                  }).ToList();
                    }
                    else
                    {
                        _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                        if (chkSoLoHanDung.Checked) // a Quý yc để chung ngày 22/11/2017
                        {
                            //var qndct0 = (from nhapd in _data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.PLoai == 1 || p.PLoai == 2 || (ckHuHao.Checked && p.PLoai == 3)).Where(p => p.NgayNhap <= _dtden)
                            //              join nhapdct in _data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                            //              select new { nhapdct.IDNhapct, nhapdct.SoLo, MaDV = nhapdct.MaDV ?? 0, nhapdct.HanDung, nhapdct.DonGia, nhapd.PLoai, nhapd.KieuDon, nhapdct.SoLuongN, nhapdct.ThanhTienN, nhapdct.SoLuongX, nhapdct.ThanhTienX }).ToList();


                            //var qndct = (from d in qndct0
                            //             join dv in qdv on d.MaDV equals dv.MaDV
                            //             select new { d.IDNhapct, d.SoLo, MaDV = d.MaDV, dv.MaCC, dv.TenDV, dv.DonVi, d.HanDung, d.DonGia, d.PLoai, d.KieuDon, d.SoLuongN, d.ThanhTienN, d.SoLuongX, d.ThanhTienX }).ToList();

                            //var qsolo0 = (from ndct in qndct.Where(p => p.SoLo != null && p.SoLo != "") group ndct by new { ndct.MaDV } into kq select new { kq.Key.MaDV, IDNhapct = kq.Where(p => p.PLoai == 1 && p.KieuDon != 2).Count() > 0 ? kq.Where(p => p.PLoai == 1 && p.KieuDon != 2).Max(p => p.IDNhapct) : 0 }).ToList();
                            //var qsolo1 = (from ndct in qsolo0 join solo in qndct on ndct.IDNhapct equals solo.IDNhapct select new { ndct.MaDV, solo.SoLo, solo.HanDung }).ToList();
                            //_lduoc = (from nd in qndct
                            //          join sl in qsolo1 on nd.MaDV equals sl.MaDV into kqsolo
                            //          from kq1 in kqsolo.DefaultIfEmpty()
                            //          select new c_duoc()
                            //          {
                            //              SoLo = kq1 == null ? "" : kq1.SoLo,
                            //              MaDV = nd.MaDV,
                            //              DonVi = nd.DonVi,
                            //              DonGia = nd.DonGia,
                            //              TenDV = nd.TenDV,
                            //              MaCC = nd.MaCC,
                            //              SoLuongN = nd.SoLuongN,
                            //              ThanhTienN = nd.ThanhTienN,
                            //              SoLuongX = nd.SoLuongX,
                            //              ThanhTienX = nd.ThanhTienX
                            //          }).ToList();
                            //_lduoc = (from nd in _lduoc
                            //          group new { nd } by new { MaDV = nd.MaDV, nd.MaCC, nd.TenDV, nd.DonVi, nd.DonGia, nd.SoLo, nd.HanDung } into kq
                            //          select new c_duoc()
                            //          {
                            //              SoLo = kq.Key.SoLo,
                            //              HanDung = kq.Key.HanDung,
                            //              MaDV = kq.Key.MaDV,
                            //              DonVi = kq.Key.DonVi,
                            //              DonGia = kq.Key.DonGia,
                            //              TenDV = kq.Key.TenDV,
                            //              MaCC = kq.Key.MaCC,
                            //              SoLuongX = kq.Sum(p => p.nd.SoLuongN) - kq.Sum(p => p.nd.SoLuongX),
                            //              ThanhTienX = (kq.Sum(p => p.nd.ThanhTienN) - kq.Sum(p => p.nd.ThanhTienX)),
                            //          }).ToList();

                            //var handung = (from nd in qndct.Where(p => p.PLoai == 1)
                            //               select nd).ToList();
                            //foreach (var a in _lduoc)
                            //{
                            //    var qhd = handung.Where(p => p.MaDV == a.MaDV && p.SoLo == a.SoLo && p.HanDung != null).FirstOrDefault();
                            //    if (qhd != null)
                            //        a.HanDung = qhd.HanDung;
                            //}
                            if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023")
                            {
                                var qkq1 = (from nhapd in _data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap <= _dtden).Where(p => p.PLoai == 1 || p.PLoai == 2 || (ckHuHao.Checked && p.PLoai == 3))
                                            join nhapdct in _data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                            select new { nhapdct.SoLo, nhapdct.MaDV, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.ThanhTienN, nhapdct.SoLuongX, nhapdct.ThanhTienX, nhapdct.HanDung }).ToList();

                                var _lduoc1 = (from d in qkq1
                                               group d by new { d.MaDV, d.DonGia, /*d.SoLo*/ } into kq
                                               select new c_duoc
                                               {
                                                   MaDV = kq.Key.MaDV ?? 0,
                                                   DonGia = kq.Key.DonGia,
                                                   SoLo = kq.Select(p => p.SoLo).FirstOrDefault(),
                                                   SoLuongX = (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                                   ThanhTienX = (kq.Sum(p => p.ThanhTienN) - kq.Sum(p => p.ThanhTienX)),
                                                   HanDung = kq.OrderByDescending(p => p.HanDung).Select(p => p.HanDung).FirstOrDefault()
                                               }).ToList();
                                _lduoc1 = (from d in _lduoc1
                                           join dv in qdv on d.MaDV equals dv.MaDV
                                           select new c_duoc()
                                           {
                                               MaDV = d.MaDV,
                                               DonVi = dv.DonVi,
                                               DonGia = d.DonGia,
                                               TenDV = dv.TenDV,
                                               SoLo = d.SoLo,
                                               MaCC = dv.MaCC,
                                               SoLuongX = d.SoLuongX,
                                               ThanhTienX = d.ThanhTienX,
                                               HanDung = chkHienthiHD.Checked == true ? d.HanDung : null
                                           }).ToList();
                                foreach (var item in _lduoc1)
                                {
                                    if (item.SoLuongX > 0)
                                    {
                                        _lduoc.Add(item);
                                    }
                                }
                            }
                            else if (DungChung.Bien.MaBV == "27001")
                            {
                                var qkq1 = (from nhapd in _data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap <= _dtden).Where(p => p.PLoai == 1 || p.PLoai == 2 || (ckHuHao.Checked && p.PLoai == 3))
                                            join nhapdct in _data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                            select new { nhapdct.MaDV, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.ThanhTienN, nhapdct.SoLuongX, nhapdct.ThanhTienX, nhapdct.HanDung }).ToList();

                                var _lduoc1 = (from d in qkq1
                                               group d by new { d.MaDV, d.DonGia } into kq
                                               select new c_duoc
                                               {
                                                   MaDV = kq.Key.MaDV ?? 0,
                                                   DonGia = kq.Key.DonGia,
                                                   SoLuongX = (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                                   ThanhTienX = (kq.Sum(p => p.ThanhTienN) - kq.Sum(p => p.ThanhTienX)),
                                                   HanDung = kq.OrderByDescending(p => p.HanDung).Select(p => p.HanDung).FirstOrDefault()
                                               }).ToList();
                                _lduoc1 = (from d in _lduoc1
                                           join dv in qdv on d.MaDV equals dv.MaDV
                                           select new c_duoc()
                                           {
                                               MaDV = d.MaDV,
                                               DonVi = dv.DonVi,
                                               DonGia = d.DonGia,
                                               TenDV = dv.TenDV,
                                               MaCC = dv.MaCC,
                                               SoLuongX = d.SoLuongX,
                                               ThanhTienX = d.ThanhTienX,
                                               SoLo = "",
                                               HanDung = chkHienthiHD.Checked == true ? d.HanDung : null
                                           }).ToList();
                                var solo = (from nhapd in _data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap <= _dtden).Where(p => p.PLoai == 1 || p.PLoai == 2 || (ckHuHao.Checked && p.PLoai == 3))
                                            join nhapdct in _data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                            where (nhapdct.SoLo != null && nhapdct.SoLo != "")
                                            select new { nhapdct.DonGia, nhapdct.MaDV, nhapdct.SoLo,/* nhapdct.HanDung*/}).Distinct().ToList();
                                foreach (var item in _lduoc1)
                                {
                                    if (item.SoLuongX > 0)
                                    {
                                        _lduoc.Add(item);
                                    }
                                }
                                var a = (from dv in _lduoc
                                         select new { dv.DonGia, dv.MaDV, dv.DonVi, dv.MaCC, dv.HanDung, dv.SoLo, dv.SoLuongN, dv.SoLuongX, dv.TenDV, dv.ThanhTienN, dv.ThanhTienX }).ToList();
                                _lduoc.Clear();
                                foreach (var c in a)
                                {

                                    c_duoc _solo = new c_duoc();
                                    foreach (var b in solo)
                                    {
                                        if ((b.MaDV == c.MaDV) && (b.DonGia == c.DonGia))
                                        {
                                            _solo.SoLo = b.SoLo;
                                        }
                                    }
                                    _solo.HanDung = c.HanDung;
                                    _solo.DonGia = c.DonGia;
                                    _solo.DonVi = c.DonVi;
                                    _solo.MaCC = c.MaCC;
                                    _solo.MaDV = c.MaDV;
                                    _solo.SoLuongN = c.SoLuongN;
                                    _solo.SoLuongX = c.SoLuongX;
                                    _solo.TenDV = c.TenDV;
                                    _solo.ThanhTienN = c.ThanhTienN;
                                    _solo.ThanhTienX = c.ThanhTienX;
                                    _lduoc.Add(_solo);     
                                }
                            }
                            else
                            {
                                var qkq1 = (from nhapd in _data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap <= _dtden).Where(p => p.PLoai == 1 || p.PLoai == 2 || (ckHuHao.Checked && p.PLoai == 3))
                                            join nhapdct in _data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                            select new { nhapdct.MaDV, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.ThanhTienN, nhapdct.SoLuongX, nhapdct.ThanhTienX, nhapdct.HanDung }).ToList();

                                var _lduoc1 = (from d in qkq1
                                               group d by new { d.MaDV, d.DonGia } into kq
                                               select new c_duoc
                                               {
                                                   MaDV = kq.Key.MaDV ?? 0,
                                                   DonGia = kq.Key.DonGia,
                                                   SoLuongX = (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                                   ThanhTienX = (kq.Sum(p => p.ThanhTienN) - kq.Sum(p => p.ThanhTienX)),
                                                   HanDung = kq.OrderByDescending(p => p.HanDung).Select(p => p.HanDung).FirstOrDefault()
                                               }).ToList();

                                _lduoc1 = (from d in _lduoc1
                                           join dv in qdv on d.MaDV equals dv.MaDV
                                           select new c_duoc()
                                           {
                                               MaDV = d.MaDV,
                                               DonVi = dv.DonVi,
                                               DonGia = d.DonGia,
                                               TenDV = dv.TenDV,
                                               //SoLo=kq.Key.SoLo,
                                               MaCC = dv.MaCC,
                                               SoLuongX = d.SoLuongX,
                                               ThanhTienX = d.ThanhTienX,
                                               HanDung = chkHienthiHD.Checked == true ? d.HanDung : null
                                           }).ToList();
                                foreach (var item in _lduoc1)
                                {
                                    if (item.SoLuongX > 0)
                                    {
                                        _lduoc.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {

                            var qkq1 = (from nhapd in _data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap <= _dtden).Where(p => p.PLoai == 1 || p.PLoai == 2 || (ckHuHao.Checked && p.PLoai == 3))
                                        join nhapdct in _data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                        select new { nhapdct.MaDV, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.ThanhTienN, nhapdct.SoLuongX, nhapdct.ThanhTienX, nhapdct.HanDung }).ToList();

                            _lduoc = (from d in qkq1
                                      group d by new { d.MaDV, d.DonGia, d.HanDung } into kq
                                      select new c_duoc
                                      {
                                          MaDV = kq.Key.MaDV ?? 0,
                                          DonGia = kq.Key.DonGia,
                                          HanDung = kq.Key.HanDung,
                                          SoLuongX = (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),
                                          ThanhTienX = (kq.Sum(p => p.ThanhTienN) - kq.Sum(p => p.ThanhTienX)),
                                      }).ToList();

                            _lduoc = (from d in _lduoc
                                      join dv in qdv on d.MaDV equals dv.MaDV
                                      select new c_duoc()
                                      {
                                          MaDV = d.MaDV,
                                          DonVi = dv.DonVi,
                                          DonGia = d.DonGia,
                                          TenDV = dv.TenDV,
                                          //SoLo = d.SoLo,
                                          MaCC = dv.MaCC,
                                          SoLuongX = d.SoLuongX,
                                          ThanhTienX = d.ThanhTienX,
                                          HanDung = d.HanDung
                                      }).ToList();
                        }
                    }
                    break;
                case 2:
                    _lduoc = (from nhapd in _data.NhapDs.Where(p => p.MaKP == _makhobc).Where(p => p.NgayNhap <= _dtden && p.NgayNhap >= _dttu)
                              join nhapdct in _data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                              join dv in _data.DichVus.Where(p => _ploai == -1 || p.IDNhom == _ploai) on nhapdct.MaDV equals dv.MaDV
                              where (nhapd.PLoai == 3 || nhapd.PLoai == 2)
                              group new { dv, nhapd, nhapdct } by new { nhapdct.MaDV, dv.MaCC, dv.TenDV, nhapdct.DonGia, dv.DonVi, nhapdct.HanDung } into kq
                              select new c_duoc()
                              {
                                  MaDV = kq.Key.MaDV == null ? 0 : kq.Key.MaDV.Value,
                                  DonVi = kq.Key.DonVi,
                                  TenDV = kq.Key.TenDV,
                                  DonGia = kq.Key.DonGia,
                                  MaCC = kq.Key.MaCC,
                                  SoLuongX = kq.Sum(p => p.nhapdct.SoLuongX),
                                  ThanhTienX = kq.Sum(p => p.nhapdct.ThanhTienX),
                                  HanDung = kq.Key.HanDung
                              }).ToList();

                    break;
            }
            _lduoc = _lduoc.OrderBy(p => p.TenDV).ToList();
            grcNhapCT.DataSource = _lduoc;
        }
        #endregion

        private bool CheckMonth(DateTime dateTime, int month, bool hetHan)
        {
            bool rs = false;
            var addMonth = DateTime.Now.Date.AddMonths(month);
            if (hetHan ? (DateTime.Now.Date > dateTime.Date) : (addMonth > dateTime.Date && DateTime.Now.Date <= dateTime.Date))
                rs = true;
            return rs;
        }

        List<DichVu> _ldv = new List<DichVu>();

        private void frm_KKTuDong_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                colSoLo.Visible = true;
                chkSoLoHanDung.Visible = false;
            }

            dtTimDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
            _ldv = _data.DichVus.ToList();
            var q = from TK in _data.KPhongs
                    where (TK.PLoai == ("Khoa dược") || TK.PLoai == ("Tủ trực"))
                    select new { TK.TenKP, TK.MaKP, TK.PLoai };
            lupTimMaKP.Properties.DataSource = q.Where(p => p.PLoai == ("Khoa dược")).ToList();
            lupKhobc.Properties.DataSource = q.ToList();
            List<NhomDV> nhom = _data.NhomDVs.Where(p => p.Status == 1).OrderBy(p => p.TenNhom).ToList();
            nhom.Insert(0, new NhomDV { IDNhom = -1, TenNhom = "Tất cả", TenNhomCT = "Tất cả" });
            lupPloai.Properties.DataSource = nhom.ToList();
            var a = _data.DichVus.ToList();
            lupMaDuoc.DataSource = a;
            switch (_kieu)
            {
                case 1:
                    this.Text = "Tạo kiểm kê tự động";
                    btnTao.Text = "Tạo kiểm kê";
                    labKhoSD.Text = "Kho Kiểm kê:";
                    lupKhobc.Visible = false;
                    labkhobc.Visible = false;
                    if (DungChung.Bien.MaBV == "01830")
                        dateTuNgay.Visible = true;
                    else
                        dateTuNgay.Visible = false;
                    labelControl2.Visible = false;
                    labHinhThuc.Visible = false;
                    radHinhthucx.Visible = false;
                    break;
                case 2:
                    this.Text = "Tạo sử dụng tự động";
                    labKhoSD.Text = "Kho sử dụng:";
                    btnTao.Text = "Tạo sử dụng";
                    labkhobc.Visible = true;
                    lupKhobc.Visible = true;
                    dateTuNgay.Visible = true;
                    labelControl2.Visible = true;
                    break;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            //TimKiem();
        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void cboPloai_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TimKiem();
        }

        private void grvNhapCT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
        private bool KTLuu()
        {

            if (string.IsNullOrEmpty(lupTimMaKP.Text))
            {
                MessageBox.Show("Bạn chưa chọn kho!");
                lupTimMaKP.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupPloai.Text))
            {
                MessageBox.Show("Bạn chưa chọn phân loại!");
                lupPloai.Focus();
                return false;
            }
            if (_kieu == 2)
            {
                if (string.IsNullOrEmpty(lupKhobc.Text))
                {
                    MessageBox.Show("Bạn chưa chọn đơn vị báo cáo!");
                    lupKhobc.Focus();
                    return false;
                }
                if (radHinhthucx.SelectedIndex < 0)
                {
                    MessageBox.Show("Bạn chưa chọn hình thức sử dụng");
                    radHinhthucx.Focus();
                    return false;
                }
            }
            return true;
        }

        private void btnTao_Click(object sender, EventArgs e)
        {


            if (KTLuu())
            {
                switch (_kieu)
                {
                    case 1:
                        if (grvNhapCT.RowCount > 1)
                        {
                            DialogResult _result = MessageBox.Show("Bạn muốn tạo kiểm kê theo số liệu sổ sách?", "Hỏi tạo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.Yes)
                            {

                                if (lupTimMaKP.EditValue != null)
                                    _makho = Convert.ToInt32(lupTimMaKP.EditValue);

                                NhapD nhap = new NhapD();
                                nhap.PLoai = 4;
                                nhap.NgayNhap = dtTimDenNgay.DateTime;
                                nhap.MaKP = Convert.ToInt32(lupTimMaKP.EditValue);
                                nhap.SoCT = dtTimDenNgay.DateTime.Day.ToString() + dtTimDenNgay.DateTime.Month.ToString() + dtTimDenNgay.DateTime.Year.ToString() + dtTimDenNgay.DateTime.Minute.ToString();
                                nhap.GhiChu = "Kiểm kê " + (lupPloai.Text == "Tất cả" ? "" : lupPloai.Text) + " tại kho: " + lupTimMaKP.Text + " ngày " + dtTimDenNgay.DateTime;
                                _data.NhapDs.Add(nhap);
                                if (_data.SaveChanges() >= 0)
                                {
                                    int idnhap = 0;
                                    var que = (from IDMax in _data.NhapDs orderby IDMax.IDNhap descending select IDMax.IDNhap).ToList();
                                    if (que.Count > 0)
                                    {
                                        idnhap = que.First();
                                    }

                                    List<NhapDct> lnhapct = (from nd in _lduoc
                                                             select new NhapDct
                                                             {
                                                                 IDNhap = idnhap,
                                                                 MaDV = nd.MaDV,
                                                                 SoLo = nd.SoLo,
                                                                 HanDung = (nd.HanDung != null && nd.HanDung.Value.Year > 2000) ? nd.HanDung : null,
                                                                 SoLuongKK = Math.Round(nd.SoLuongX, 4),
                                                                 ThanhTienKK = Math.Round(nd.ThanhTienX, 4),
                                                                 DonGia = nd.DonGia,
                                                                 DonVi = nd.DonVi
                                                             }).ToList();
                                    DataTable dt = new DataTable();

                                    SqlDataAdapter adapter = new SqlDataAdapter();
                                    SqlConnection connection;
                                    string _connectionString = "server=" + DungChung.Bien.TenServer + ";database=" + DungChung.Bien.TenCSDL + ";uid=" + DungChung.Bien.accountsql + ";password=" + DungChung.Bien.passql + ";";
                                    connection = new SqlConnection(_connectionString);
                                    using (SqlBulkCopy blkcopy = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.KeepIdentity & SqlBulkCopyOptions.KeepNulls))
                                    {
                                        connection.Open();

                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("IDNhap", "IDNhap"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("MaDV", "MaDV"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SoLo", "SoLo"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("HanDung", "HanDung"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SoLuongKK", "SoLuongKK"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ThanhTienKK", "ThanhTienKK"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DonGia", "DonGia"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DonVi", "DonVi"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("TyLeCK", "TyLeCK"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DonGiaCT", "DonGiaCT"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("VAT", "VAT"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SoLuongN", "SoLuongN"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ThanhTienN", "ThanhTienN"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SoLuongX", "SoLuongX"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ThanhTienX", "ThanhTienX"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SoLuongSD", "SoLuongSD"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ThanhTienSD", "ThanhTienSD"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DonGiaDY", "DonGiaDY"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SoLuongDY", "SoLuongDY"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ThanhTienDY", "ThanhTienDY"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DonGiaX", "DonGiaX"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("TrongBH", "TrongBH"));
                                        blkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("IDDTBN", "IDDTBN"));

                                        blkcopy.BatchSize = 1000;
                                        blkcopy.BulkCopyTimeout = 3000;
                                        blkcopy.DestinationTableName = "NhapDct";
                                        DataTable table = ConvertToTable.ToDataTable<NhapDct>(lnhapct);
                                        blkcopy.WriteToServer(table);
                                        connection.Close();

                                    }
                                }
                            }
                        }
                        break;
                    case 2:
                        DialogResult _result2 = MessageBox.Show("Bạn muốn tạo báo cáo sử dụng theo số liệu sổ sách?", "Hỏi tạo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result2 == DialogResult.Yes)
                        {

                            if (lupKhobc.EditValue != null)
                                _makho = Convert.ToInt32(lupKhobc.EditValue);
                            var qnxt2 = (from nhapd in _data.NhapDs.Where(p => p.MaKP == _makho).Where(p => p.NgayNhap <= _dtden && p.NgayNhap >= _dttu).Where(p => p.KieuDon.Value != 6)
                                         join nhapdct in _data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                         join dv in _data.DichVus.Where(p => p.IDNhom == _ploai) on nhapdct.MaDV equals dv.MaDV
                                         where (nhapd.PLoai == 3 || nhapd.PLoai == 2)
                                         group new { dv, nhapd, nhapdct } by new { nhapdct.MaDV, dv.MaCC, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia } into kq
                                         select new
                                         {
                                             MaDV = kq.Key.MaDV,
                                             TenHamLuong = kq.Key.TenDV,
                                             DonVi = kq.Key.DonVi,
                                             DonGia = kq.Key.DonGia,
                                             //SoLo=kq.Key.SoLo,
                                             kq.Key.MaCC,
                                             SoLuongX = kq.Sum(p => p.nhapdct.SoLuongX),
                                             ThanhTienX = kq.Sum(p => p.nhapdct.ThanhTienX)
                                         }).ToList();
                            NhapD nhap2 = new NhapD();
                            nhap2.PLoai = 5;
                            nhap2.KieuDon = radHinhthucx.SelectedIndex;
                            nhap2.NgayNhap = dtTimDenNgay.DateTime;
                            nhap2.MaKP = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                            nhap2.MaKPnx = lupKhobc.EditValue == null ? 0 : Convert.ToInt32(lupKhobc.EditValue);
                            nhap2.SoCT = dtTimDenNgay.DateTime.Day.ToString() + dtTimDenNgay.DateTime.Month.ToString() + dtTimDenNgay.DateTime.Year.ToString() + dtTimDenNgay.DateTime.Minute.ToString();
                            nhap2.GhiChu = "Sử dụng " + (lupPloai.Text == "Tất cả" ? "Thuốc, vật tư, hóa chât" : lupPloai.Text) + " tại kho: " + lupTimMaKP.Text + "từ ngày:" + _dttu.ToString() + " đến ngày " + _dtden.ToString();
                            _data.NhapDs.Add(nhap2);
                            if (_data.SaveChanges() >= 0)
                            {
                                int idnhap = 0;
                                var que = (from IDMax in _data.NhapDs orderby IDMax.IDNhap descending select IDMax.IDNhap).ToList();
                                if (que.Count > 0)
                                {
                                    idnhap = que.First();
                                }
                                foreach (var a in _lduoc)
                                {
                                    NhapDct nhapct = new NhapDct();
                                    nhapct.IDNhap = idnhap;
                                    nhapct.MaDV = a.MaDV;
                                    nhapct.SoLuongKK = 0;
                                    nhapct.ThanhTienKK = 0;
                                    nhapct.DonGia = a.DonGia;
                                    nhapct.DonVi = a.DonVi;
                                    if (a.MaCC != null && a.MaCC != "")
                                        nhapct.MaCC = a.MaCC;
                                    else
                                        nhapct.MaCC = "";
                                    nhapct.SoLuongN = 0;
                                    nhapct.ThanhTienN = 0;
                                    nhapct.SoLuongSD = a.SoLuongX;
                                    nhapct.ThanhTienSD = a.ThanhTienX;
                                    nhapct.SoLuongX = 0;
                                    nhapct.ThanhTienX = 0;
                                    _data.NhapDcts.Add(nhapct);
                                    _data.SaveChanges();
                                }
                            }
                        }
                        break;
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn kho sử dụng!");
            }
        }





        private void lupKhobc_EditValueChanged(object sender, EventArgs e)
        {
            // TimKiem();
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            //TimKiem();
        }

        private void lupPloai_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void chkHanDung_CheckedChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void radHinhthucx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grvNhapCT_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void grcNhapCT_Click(object sender, EventArgs e)
        {

        }

        private void grvNhapCT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var row = (c_duoc)grvNhapCT.GetRow(e.RowHandle);
            if (row != null)
            {
                if (DungChung.Bien.MaBV == "14017")
                {
                    if (row.HanDung == null)
                        return;
                    if (CheckMonth(row.HanDung.Value, 0, true))
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (CheckMonth(row.HanDung.Value, 1, false))
                    {
                        e.Appearance.BackColor = Color.Orange;
                    }
                    else if (CheckMonth(row.HanDung.Value, 3, false))
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                    else if (CheckMonth(row.HanDung.Value, 6, false))
                    {
                        e.Appearance.BackColor = Color.LimeGreen;
                    }
                    else if (CheckMonth(row.HanDung.Value, 12, false))
                    {
                        e.Appearance.BackColor = Color.DodgerBlue;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }

        private void chkHienthiHD_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkHienthiHD_CheckedChanged_1(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void ckHuHao_CheckedChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
    }
}