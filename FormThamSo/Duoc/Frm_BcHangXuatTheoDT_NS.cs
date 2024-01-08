using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_BcHangXuatTheoDT_NS : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcHangXuatTheoDT_NS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        private class KPhong
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
    
        List<KPhong> _Kphong = new List<KPhong>();
        private void Frm_BcHangXuatTheoDT_NS_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
          _Kphong.Clear();
           var kphong = (from kp in Data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { kp.TenKP, kp.MaKP }).ToList();
           if (kphong.Count > 0)
           {
               KPhong themmoi1 = new KPhong();
               themmoi1.tenkp = "Chọn tất cả";
               themmoi1.makp = 0;
               themmoi1.chon = true;
               _Kphong.Add(themmoi1);
               foreach (var a in kphong)
               {
                   KPhong themmoi = new KPhong();
                   themmoi.tenkp = a.TenKP;
                   themmoi.makp = a.MaKP;
                   themmoi.chon = true;
                   _Kphong.Add(themmoi);
               }
               grcKhoaphong.DataSource = _Kphong.ToList();
           }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            List<KPhong> _lKhoaP = new List<KPhong>();
            if (rad_mau2.SelectedIndex == 0)
            {
                if (KTtaoBc())
                {
                    tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BcHangXuatTheoDT_NS rep = new BaoCao.Rep_BcHangXuatTheoDT_NS();
                    rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                    _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                    _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });

                    var qkho = (from lkp in _lKhoaP
                                join kp in Data.KPhongs on lkp.makp equals kp.MaKP
                                select new { kp.TenKP }).Distinct().OrderBy(p => p.TenKP).ToList();
                    if (qkho.Count > 0)
                    {
                        int i = qkho.Count();
                        if (i == 0) { rep.KhoXuat.Value = "Kho xuất: "; }
                        if (i == 1) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP; }
                        if (i == 2) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP; }
                        if (i == 3) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP; }
                        if (i == 4) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP; }
                        if (i == 5) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP + ", " + qkho.Skip(4).First().TenKP; }
                        if (i == 6) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP + ", " + qkho.Skip(4).First().TenKP + ", " + qkho.Skip(5).First().TenKP; }
                        if (i == 7) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP + ", " + qkho.Skip(4).First().TenKP + ", " + qkho.Skip(5).First().TenKP + ", " + qkho.Skip(6).First().TenKP; }
                        if (i == 8) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP + ", " + qkho.Skip(4).First().TenKP + ", " + qkho.Skip(5).First().TenKP + ", " + qkho.Skip(6).First().TenKP + ", " + qkho.Skip(7).First().TenKP; }
                        if (i > 8) { rep.KhoXuat.Value = "Kho xuất: "; }
                    }
                    if (radBN.SelectedIndex == 0)
                    {
                        var qxngtru1 = (from nhapd in Data.NhapDs.Where(p => p.PLoai == 2 && p.KieuDon == 0)
                                        join bn in Data.BenhNhans on nhapd.MaBNhan equals bn.MaBNhan
                                        join nhapdct in Data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                        join dv in Data.DichVus on nhapdct.MaDV equals dv.MaDV
                                        join nhomdv in Data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                        join tieunhomdv in Data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                        select new
                                        {
                                            NgayNhap = nhapd.NgayNhap,
                                            Tuoi = bn.Tuoi,
                                            PLoai = nhapd.PLoai,
                                            MaKP = nhapd.MaKP,
                                            KieuDon = nhapd.KieuDon,
                                            StatusNhomDV = nhomdv.Status,
                                            TenTN = tieunhomdv.TenTN,
                                            MaDV = dv.MaDV,
                                            TenDV = dv.TenDV,
                                            DonVi = dv.DonVi,
                                            DonGia = nhapdct.DonGia,
                                            SoLuongX =  nhapdct.SoLuongX,
                                            ThanhTienX =  nhapdct.ThanhTienX ,
                                        }).ToList();

                        var qxngtru = ((from lkp in _lKhoaP
                                        join q in qxngtru1.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)//.Where(p => p.PLoai == 2 && p.KieuDon == 0)
                                        .Where(p => p.StatusNhomDV == 1) on lkp.makp equals q.MaKP
                                        group q by new { q.TenTN, q.MaDV, q.TenDV, q.DonVi, q.DonGia } into kq
                                        select new
                                        {
                                            TieuNhomDV = kq.Key.TenTN,
                                            MaDV = kq.Key.MaDV,
                                            TenDV = kq.Key.TenDV,
                                            DVT = kq.Key.DonVi,
                                            DonGia = kq.Key.DonGia,

                                            TESL = kq.Where(p => p.Tuoi < 6).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.Tuoi < 6).Sum(p => p.SoLuongX),
                                            TETT = kq.Where(p => p.Tuoi < 6).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.Tuoi < 6).Sum(p => p.ThanhTienX),

                                            NGSL = kq.Where(p => p.Tuoi >= 60).Sum(p => p.SoLuongX) == null ? 0 : kq.Where(p => p.Tuoi >= 60).Sum(p => p.SoLuongX),
                                            NGTT = kq.Where(p => p.Tuoi >= 60).Sum(p => p.ThanhTienX) == null ? 0 : kq.Where(p => p.Tuoi >= 60).Sum(p => p.ThanhTienX),

                                            //CLSL = kq.Sum(p => p.nhapdct.SoLuongX) - kq.Where(p => p.bn.Tuoi < 6||p.bn.Tuoi>=60).Sum(p => p.nhapdct.SoLuongX),
                                            //CLTT = kq.Sum(p => p.nhapdct.ThanhTienX) - kq.Where(p => p.bn.Tuoi < 6 || p.bn.Tuoi >= 60).Sum(p => p.nhapdct.ThanhTienX),

                                            TongSL = kq.Sum(p => p.SoLuongX) == null ? 0 : kq.Sum(p => p.SoLuongX),
                                            TongTT = kq.Sum(p => p.ThanhTienX) == null ? 0 : kq.Sum(p => p.ThanhTienX),
                                        }).OrderBy(p => p.TenDV).ToList()).Select(x => new
                                        {
                                            x.TieuNhomDV,
                                            x.MaDV,
                                            x.TenDV,
                                            x.DVT,
                                            x.DonGia,
                                            TESL =  x.TESL,
                                            TETT =  x.TETT,
                                            NGSL = x.NGSL,
                                            NGTT = x.NGTT ,
                                            TongSL = x.TongSL,
                                            TongTT = x.TongTT ,
                                            CLSL = x.TongSL - x.TESL - x.NGSL,
                                            CLTT = x.TongTT - x.TETT - x.NGTT
                                        }).ToList();


                        rep.DataSource = qxngtru.OrderBy(p => p.TenDV).ToList();
                    }
                    if (radBN.SelectedIndex == 1)
                    {
                        var qdv = (from dv in Data.DichVus 
                                       join tieunhomdv in Data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                       join nhomdv in Data.NhomDVs.Where(p=>p.Status == 1) on tieunhomdv.IDNhom equals nhomdv.IDNhom
                                   select new { dv.MaDV, dv.TenDV, dv.DonVi, tieunhomdv.TenTN, StatusNhomDV = nhomdv.Status}).ToList();
                        var qnhapd = (from nhapd in Data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 2)
                                          .Where(p => p.SoPL != null)
                                      join nhapdct in Data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap                                     
                                      select new
                                      {
                                          SoPL = nhapd.SoPL,
                                          MaBNhan = nhapd.MaBNhan,
                                          NgayNhap = nhapd.NgayNhap,
                                          PLoai = nhapd.PLoai,
                                          MaKP = nhapd.MaKP,
                                          nhapdct.MaDV,
                                          DonGia = nhapdct.DonGia,
                                      }).ToList();
                        var qdhuoc = (from DT in Data.DThuocs
                                      join dtct in Data.DThuoccts.Where(p => p.Status == 1).Where(p => p.SoPL != null) on DT.IDDon equals dtct.IDDon
                                      select new
                                      {
                                          dtct.MaDV,
                                          dtct.SoPL,
                                          DT.MaBNhan,
                                          SoLuong = dtct.SoLuong,
                                          ThanhTien = dtct.ThanhTien
                                      }).ToList();
                        var bnhan = Data.BenhNhans.ToList();
                        var qxntru = ((from lkp in _lKhoaP
                                       join nd in qnhapd on lkp.makp equals nd.MaKP
                                       join dt in qdhuoc on new {SoPL = nd.SoPL??0, nd.MaDV } equals new { dt.SoPL, dt.MaDV}
                                       join dv in qdv on dt.MaDV equals dv.MaDV
                                       join bn in bnhan on dt.MaBNhan equals bn.MaBNhan
                                       group new {nd, dt, bn, dv} by new { dv.TenTN, dv.MaDV, dv.TenDV, dv.DonVi, nd.DonGia } into kq
                                       select new
                                       {
                                           TieuNhomDV = kq.Key.TenTN,
                                           MaDV = kq.Key.MaDV,
                                           TenDV = kq.Key.TenDV,
                                           DVT = kq.Key.DonVi,
                                           DonGia = kq.Key.DonGia,

                                           TESL = kq.Where(p => p.bn.Tuoi < 6).Sum(p => p.dt.SoLuong) == null ? 0 : kq.Where(p => p.bn.Tuoi < 6).Sum(p => p.dt.SoLuong),
                                           TETT = kq.Where(p => p.bn.Tuoi < 6).Sum(p => p.dt.ThanhTien) == null ? 0 : kq.Where(p => p.bn.Tuoi < 6).Sum(p => p.dt.ThanhTien),

                                           NGSL = kq.Where(p => p.bn.Tuoi >= 60).Sum(p => p.dt.SoLuong) == null ? 0 : kq.Where(p => p.bn.Tuoi >= 60).Sum(p => p.dt.SoLuong),
                                           NGTT = kq.Where(p => p.bn.Tuoi >= 60).Sum(p => p.dt.ThanhTien) == null ? 0 : kq.Where(p => p.bn.Tuoi >= 60).Sum(p => p.dt.ThanhTien),

                                           //CLSL = kq.Sum(p => p.nhapdct.SoLuongX) - kq.Where(p => p.bn.Tuoi < 6||p.bn.Tuoi>=60).Sum(p => p.nhapdct.SoLuongX),
                                           //CLTT = kq.Sum(p => p.nhapdct.ThanhTienX) - kq.Where(p => p.bn.Tuoi < 6 || p.bn.Tuoi >= 60).Sum(p => p.nhapdct.ThanhTienX),

                                           TongSL = kq.Sum(p => p.dt.SoLuong) == null ? 0 : kq.Sum(p => p.dt.SoLuong),
                                           TongTT = kq.Sum(p => p.dt.ThanhTien) == null ? 0 : kq.Sum(p => p.dt.ThanhTien),
                                       }).OrderBy(p => p.TenDV).ToList()).Select(x => new
                                       {
                                           x.TieuNhomDV,
                                           x.MaDV,
                                           x.TenDV,
                                           x.DVT,
                                           x.DonGia,
                                           TESL = x.TESL,
                                           TETT = x.TETT,
                                           NGSL = x.NGSL,
                                           NGTT = x.NGTT,
                                           TongSL = x.TongSL,
                                           TongTT = x.TongTT,
                                           CLSL = x.TongSL - x.TESL - x.NGSL,
                                           CLTT = x.TongTT - x.TETT - x.NGTT,
                                       }).ToList();
                        rep.DataSource = qxntru.OrderBy(p => p.TenDV).ToList();
                    }
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }
            }
            else {
                if (KTtaoBc())
                {
                    tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                    denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BcHangXuatTheoDT_NS_MS2 rep = new BaoCao.Rep_BcHangXuatTheoDT_NS_MS2();
                    rep.TuNgayDenNgay.Value = "Từ ngày " + lupTuNgay.Text + " Đến ngày " + lupDenNgay.Text;
                    _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                    _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });

                    var qkho = (from lkp in _lKhoaP
                                join kp in Data.KPhongs on lkp.makp equals kp.MaKP
                                select new { kp.TenKP }).Distinct().OrderBy(p => p.TenKP).ToList();
                    if (qkho.Count > 0)
                    {
                        int i = qkho.Count();
                        if (i == 0) { rep.KhoXuat.Value = "Kho xuất: "; }
                        if (i == 1) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP; }
                        if (i == 2) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP; }
                        if (i == 3) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP; }
                        if (i == 4) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP; }
                        if (i == 5) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP + ", " + qkho.Skip(4).First().TenKP; }
                        if (i == 6) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP + ", " + qkho.Skip(4).First().TenKP + ", " + qkho.Skip(5).First().TenKP; }
                        if (i == 7) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP + ", " + qkho.Skip(4).First().TenKP + ", " + qkho.Skip(5).First().TenKP + ", " + qkho.Skip(6).First().TenKP; }
                        if (i == 8) { rep.KhoXuat.Value = "Kho xuất: " + qkho.First().TenKP + ", " + qkho.Skip(1).First().TenKP + ", " + qkho.Skip(2).First().TenKP + ", " + qkho.Skip(3).First().TenKP + ", " + qkho.Skip(4).First().TenKP + ", " + qkho.Skip(5).First().TenKP + ", " + qkho.Skip(6).First().TenKP + ", " + qkho.Skip(7).First().TenKP; }
                        if (i > 8) { rep.KhoXuat.Value = "Kho xuất: "; }
                    }
                    if (radBN.SelectedIndex == 0)
                    {
                        var qxngtru1 = (from nhapd in Data.NhapDs.Where(p=>p.PLoai == 2)
                                        join bn in Data.BenhNhans on nhapd.MaBNhan equals bn.MaBNhan
                                        join nhapdct in Data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                        join dv in Data.DichVus on nhapdct.MaDV equals dv.MaDV
                                        join nhomdv in Data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                        join tieunhomdv in Data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                        select new
                                        {
                                            NgayNhap = nhapd.NgayNhap,
                                            Tuoi = bn.Tuoi,
                                            DTuong = bn.DTuong,
                                            PLoai = nhapd.PLoai,
                                            MaKP = nhapd.MaKP,
                                            KieuDon = nhapd.KieuDon,
                                            StatusNhomDV = nhomdv.Status,
                                            TenTN = tieunhomdv.TenTN,
                                            MaDV = dv.MaDV,
                                            TenDV = dv.TenDV,
                                            DonVi = dv.DonVi,
                                            DonGia = nhapdct.DonGia,
                                            SoLuongX = nhapdct.SoLuongX,
                                            ThanhTienX = nhapdct.ThanhTienX,
                                        }).ToList();

                        var qxngtru = ((from lkp in _lKhoaP
                                        join q in qxngtru1.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)//.Where(p => p.PLoai == 2 )
                                        .Where(p => p.StatusNhomDV == 1) on lkp.makp equals q.MaKP
                                        group q by new { q.TenTN, q.MaDV, q.TenDV, q.DonVi, q.DonGia } into kq
                                        select new
                                        {
                                            TieuNhomDV = kq.Key.TenTN,
                                            MaDV = kq.Key.MaDV,
                                            TenDV = kq.Key.TenDV,
                                            DVT = kq.Key.DonVi,
                                            DonGia = kq.Key.DonGia,

                                            TESL = kq.Where(p => p.Tuoi < 6).Sum(p => p.SoLuongX),
                                            TETT =  kq.Where(p => p.Tuoi < 6).Sum(p => p.ThanhTienX),

                                            NGSL =  kq.Where(p => p.DTuong == "BHYT").Sum(p => p.SoLuongX), // xuất BH
                                            NGTT =  kq.Where(p => p.DTuong == "BHYT").Sum(p => p.ThanhTienX), // xuất BH

                                            CLSL = kq.Where(p => p.DTuong == "Dịch vụ").Sum(p => p.SoLuongX), // xuất BH
                                            CLTT = kq.Where(p => p.DTuong == "Dịch vụ").Sum(p => p.ThanhTienX), // xuất dịch vụ

                                            TongSL = kq.Where(p => p.KieuDon == 3).Sum(p => p.SoLuongX), //
                                            TongTT =  kq.Where(p => p.KieuDon == 3).Sum(p => p.ThanhTienX),
                                        }).OrderBy(p => p.TenDV).ToList());


                        rep.DataSource = qxngtru.OrderBy(p => p.TenDV).ToList();
                    }
                    if (radBN.SelectedIndex == 1)
                    {

                        var qdv = (from dv in Data.DichVus
                                   join tieunhomdv in Data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                   join nhomdv in Data.NhomDVs.Where(p => p.Status == 1) on tieunhomdv.IDNhom equals nhomdv.IDNhom
                                   select new { dv.MaDV, dv.TenDV, dv.DonVi, tieunhomdv.TenTN, StatusNhomDV = nhomdv.Status }).ToList();
                        var qnhapd = (from nhapd in Data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 2).Where(p => p.SoPL != null)
                                      join nhapdct in Data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                      select new
                                      {
                                          SoPL = nhapd.SoPL,
                                          nhapd.KieuDon,
                                          MaBNhan = nhapd.MaBNhan,
                                          NgayNhap = nhapd.NgayNhap,
                                          PLoai = nhapd.PLoai,
                                          MaKP = nhapd.MaKP,
                                          nhapdct.MaDV,
                                          DonGia = nhapdct.DonGia,
                                          nhapdct.SoLuongX,
                                          nhapdct.ThanhTienX
                                      }).ToList();
                        var qdhuoc = (from DT in Data.DThuocs
                                      join dtct in Data.DThuoccts.Where(p => p.SoPL != null).Where(p => p.Status == 1) on DT.IDDon equals dtct.IDDon
                                      select new
                                      {
                                          dtct.MaDV,
                                          dtct.SoPL,
                                          DT.MaBNhan,
                                          SoLuong = dtct.SoLuong,
                                          ThanhTien = dtct.ThanhTien
                                      }).ToList();
                        var bnhan = Data.BenhNhans.ToList();
                        var qxntru = ((from lkp in _lKhoaP
                                       join nd in qnhapd on lkp.makp equals nd.MaKP
                                       join dt in qdhuoc on new { SoPL = nd.SoPL ?? 0, nd.MaDV } equals new { dt.SoPL, dt.MaDV }
                                       join dv in qdv on dt.MaDV equals dv.MaDV
                                       join bn in bnhan on dt.MaBNhan equals bn.MaBNhan
                                       group new { nd, dt, bn, dv } by new { bn.DTuong, dv.TenTN, dv.MaDV, dv.TenDV, dv.DonVi, nd.DonGia, nd.KieuDon } into kq
                                       select new
                                       {
                                           TieuNhomDV = kq.Key.TenTN,
                                           MaDV = kq.Key.MaDV,
                                           TenDV = kq.Key.TenDV,
                                           DVT = kq.Key.DonVi,
                                           DonGia = kq.Key.DonGia,
                                           kq.Key.KieuDon,
                                           kq.Key.DTuong,
                                           TESL = kq.Where(p => p.bn.Tuoi < 6).Sum(p => p.dt.SoLuong),
                                           TETT = kq.Where(p => p.bn.Tuoi < 6).Sum(p => p.dt.ThanhTien),

                                           NGSL =  kq.Where(p => p.bn.DTuong == "BHYT").Sum(p => p.dt.SoLuong), // xuất BH
                                           NGTT = kq.Where(p => p.bn.DTuong == "BHYT").Sum(p => p.dt.ThanhTien), // xuất BH

                                           CLSL = kq.Where(p => p.bn.DTuong == "Dịch vụ").Sum(p => p.dt.SoLuong), // xuất BH
                                           CLTT = kq.Where(p => p.bn.DTuong == "Dịch vụ").Sum(p => p.dt.ThanhTien), // xuất dịch vụ

                                           TongSL = 0,
                                           TongTT = 0,
                                           SoLuongX = kq.Sum(p=>p.nd.SoLuongX),
                                           ThanhTienX = kq.Sum(p=>p.nd.ThanhTienX)
                                       }).OrderBy(p => p.TenDV).ToList());


                        var qxntru1 = ((from q in qxntru
                                       group q by new { q.DTuong, q.TieuNhomDV, q.MaDV, q.TenDV, q.DVT, q.DonGia } into kq
                                         select new _source
                                         {
                                             
                                             TieuNhomDV = kq.Key.TieuNhomDV,
                                             MaDV = kq.Key.MaDV ,
                                             TenDV = kq.Key.TenDV,
                                             DVT = kq.Key.DVT,
                                             DonGia = kq.Key.DonGia ,

                                             TESL = kq.Sum(p=>p.TESL),
                                             TETT = kq.Sum(p => p.TETT),

                                             NGSL = kq.Sum(p=>p.NGSL) - kq.Where(p=>p.DTuong=="BHYT").Sum(p=>p.TESL),
                                             NGTT = kq.Sum(p => p.NGTT) - kq.Where(p => p.DTuong == "BHYT").Sum(p => p.TETT),

                                             CLSL = kq.Sum(p => p.CLSL) - kq.Where(p => p.DTuong == "Dịch vụ").Sum(p => p.TESL),
                                             CLTT = kq.Sum(p => p.CLTT) - kq.Where(p => p.DTuong == "Dịch vụ").Sum(p => p.TETT),

                                             TongSL = 0,
                                             TongTT = 0,
                                         }).OrderBy(p => p.TenDV).ToList());
                        var qxntruNg= ((from  q in qxntru
                                        group q by new { q.TieuNhomDV, q.MaDV, q.TenDV, q.DVT, q.DonGia } into kq
                                         select new _source
                                        {
                                           TieuNhomDV = kq.Key.TieuNhomDV == null ? "" : kq.Key.TieuNhomDV,
                                           MaDV = kq.Key.MaDV ,
                                           TenDV = kq.Key.TenDV == null ? "" : kq.Key.TenDV,
                                           DVT =  kq.Key.DVT == null ? "" : kq.Key.DVT,
                                           DonGia = kq.Key.DonGia,

                                            TESL =0,
                                            TETT = 0,

                                            NGSL =0,// xuất BH
                                            NGTT =  0,

                                            CLSL =  0,
                                            CLTT = 0,

                                            TongSL = kq.Where(p => p.KieuDon == 3).Sum(p => p.SoLuongX), //
                                            TongTT =  kq.Where(p => p.KieuDon == 3).Sum(p => p.ThanhTienX),
                                        }).OrderBy(p => p.TenDV).ToList());

                        var tong = qxntru1.Union(qxntruNg);
                        var a=(from q in tong group q by new { q.TieuNhomDV, q.MaDV, q.TenDV, q.DVT, q.DonGia } into kq
                                   select new 
                                       {
                                           TieuNhomDV = kq.Key.TieuNhomDV,
                                           MaDV = kq.Key.MaDV,
                                           TenDV = kq.Key.TenDV == null ? "" : kq.Key.TenDV,
                                           DVT =  kq.Key.DVT,
                                           DonGia = kq.Key.DonGia ,

                                           TESL = kq.Sum(p => p.TESL) ,
                                           TETT = kq.Sum(p => p.TETT),

                                           NGSL = kq.Sum(p => p.NGSL),
                                           NGTT = kq.Sum(p => p.NGTT) ,

                                           CLSL = kq.Sum(p => p.CLSL) ,
                                           CLTT = kq.Sum(p => p.CLTT),

                                           TongSL =kq.Sum(p => p.TongSL),
                                           TongTT = kq.Sum(p => p.TongTT),
                                       }).OrderBy(p => p.TenDV).ToList();
                        rep.DataSource = a.OrderBy(p => p.TenDV).ToList();
                    }
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }
            }
        }
public class _source{
    string tieuNhomDV,  tenDV, dVT;
    int maDV;

    public string DVT
    {
        get { return dVT; }
        set { dVT = value; }
    }

    public string TenDV
    {
        get { return tenDV; }
        set { tenDV = value; }
    }

    public int MaDV
    {
        get { return maDV; }
        set { maDV = value; }
    }

    public string TieuNhomDV
    {
        get { return tieuNhomDV; }
        set { tieuNhomDV = value; }
    }
    double donGia, tESL, tETT, nGSL, nGTT, cLSL, cLTT, tongSL, tongTT;

    public double TongTT
    {
        get { return tongTT; }
        set { tongTT = value; }
    }

    public double TongSL
    {
        get { return tongSL; }
        set { tongSL = value; }
    }

    public double CLTT
    {
        get { return cLTT; }
        set { cLTT = value; }
    }

    public double CLSL
    {
        get { return cLSL; }
        set { cLSL = value; }
    }

    public double NGTT
    {
        get { return nGTT; }
        set { nGTT = value; }
    }

    public double NGSL
    {
        get { return nGSL; }
        set { nGSL = value; }
    }

    public double TETT
    {
        get { return tETT; }
        set { tETT = value; }
    }

    public double TESL
    {
        get { return tESL; }
        set { tESL = value; }
    }

    public double DonGia
    {
        get { return donGia; }
        set { donGia = value; }
    }
}
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
    }
}