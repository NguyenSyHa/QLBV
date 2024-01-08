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
    public partial class frm_BCXetNghiem_30007 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCXetNghiem_30007()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime _Tungay = DungChung.Ham.NgayTu(deTuNgay.DateTime);
            DateTime _DenNgay = DungChung.Ham.NgayDen(deDenNgay.DateTime);

            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _ldv = (from dv in _data.DichVus
                        join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                        join n in _data.NhomDVs on tn.IDNhom equals n.IDNhom
                        select new { dv, tn, n }).ToList();
            var _lkp = _data.KPhongs.ToList();
            if (rgNgay.SelectedIndex == 0)
            {
                var _lcls = (from cls in _data.CLS.Where(p => p.NgayTH >= _Tungay && p.NgayTH <= _DenNgay).Where(p => p.Status == 1)
                             join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join bn in _data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             select new { cls.IdCLS, cls.MaBNhan, cd.MaDV, bn.DTuong, cls.MaKP, bn.NoiTru }).ToList();
                //.Select(p => p.cls.IdCLS).Count()
                var q1 = (from cls in _lcls
                          join dv in _ldv.Where(p => p.dv.PLoai == 2) on cls.MaDV equals dv.dv.MaDV
                          join kp in _lkp on cls.MaKP equals kp.MaKP
                          group new { cls, kp, dv } by new { kp.MaKP, kp.TenKP } into kq
                          select new
                          {
                              kq.Key.MaKP,
                              kq.Key.TenKP,

                              HHocNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Select(p => p.cls.IdCLS).Count(),
                              HHocNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Select(p => p.cls.IdCLS).Count(),
                              HHocKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Select(p => p.cls.IdCLS).Count(),

                              SHoaNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo).Select(p => p.cls.IdCLS).Count(),
                              SHoaNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo).Select(p => p.cls.IdCLS).Count(),
                              SHoaKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo).Select(p => p.cls.IdCLS).Count(),

                              VSinhNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("vi nấm soi tươi")).Select(p => p.cls.IdCLS).Count(),
                              VSinhNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("vi nấm soi tươi")).Select(p => p.cls.IdCLS).Count(),
                              VSinhKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("vi nấm soi tươi")).Select(p => p.cls.IdCLS).Count(),

                              HIVNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac && p.dv.dv.TenDV.ToLower().Contains("hiv")).Select(p => p.cls.IdCLS).Count(),
                              HIVNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac && p.dv.dv.TenDV.ToLower().Contains("hiv")).Select(p => p.cls.IdCLS).Count(),
                              HIVKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac && p.dv.dv.TenDV.ToLower().Contains("hiv")).Select(p => p.cls.IdCLS).Count(),

                              STietNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("xét nghiệm tế bào học áp nhuộm thường quy")).Select(p => p.cls.IdCLS).Count(),
                              STietNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("xét nghiệm tế bào học áp nhuộm thường quy")).Select(p => p.cls.IdCLS).Count(),
                              STietKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("xét nghiệm tế bào học áp nhuộm thường quy")).Select(p => p.cls.IdCLS).Count()

                          }).OrderBy(p => p.TenKP).ToList();
                var kqs = (from a in q1
                           group a by new { a.MaKP, a.TenKP } into kq
                           select new BC
                           {
                               MaKP = kq.Key.MaKP,
                               TenKP = kq.Key.TenKP,
                               HHocNgTru = kq.Sum(p => p.HHocNgTru),
                               HHocNTru = kq.Sum(p => p.HHocNTru),
                               HHocKSK = kq.Sum(p => p.HHocKSK),

                               SHoaNgTru = kq.Sum(p => p.SHoaNgTru),
                               SHoaNTru = kq.Sum(p => p.SHoaNTru),
                               SHoaKSK = kq.Sum(p => p.SHoaKSK),

                               VSinhNgTru = kq.Sum(p => p.VSinhNgTru),
                               VSinhNTru = kq.Sum(p => p.VSinhNTru),
                               VSinhKSK = kq.Sum(p => p.VSinhKSK),

                               HIVNgTru = kq.Sum(p => p.HIVNgTru),
                               HIVNTru = kq.Sum(p => p.HIVNTru),
                               HIVKSK = kq.Sum(p => p.HIVKSK),

                               STietNgTru = kq.Sum(p => p.STietNgTru),
                               STietNTru = kq.Sum(p => p.STietNTru),
                               STietKSK = kq.Sum(p => p.STietKSK),

                               Tong = kq.Sum(p => p.HHocNgTru + p.HHocNTru + p.HHocKSK + p.SHoaNgTru + p.SHoaNTru + p.SHoaKSK + p.VSinhNgTru + p.VSinhNTru + p.VSinhKSK + p.HIVNgTru + p.HIVNTru + p.HIVKSK + p.STietNgTru + p.STietNTru + p.STietKSK)
                           }).ToList();
                var _ldtct = (from dt in _data.DThuocs
                              join dtct in _data.DThuoccts.Where(p => p.NgayNhap >= _Tungay && p.NgayNhap <= _DenNgay) on dt.IDDon equals dtct.IDDon
                              select new { dt, dtct }).ToList();
                var q2 = (from a in _ldtct
                          join dv in _ldv.Where(p => p.n.IDNhom == 7) on a.dtct.MaDV equals dv.dv.MaDV
                          group new { a, dv } by new { a.dtct.MaDV, dv.dv.TenDV, a.dtct.DonVi } into kq
                          select new BCTruyenMau
                          {
                              MaDV = kq.Key.MaDV ?? 0,
                              TenDV = kq.Key.TenDV,
                              DonVi = kq.Key.DonVi,
                              SoLuong = kq.Sum(p => p.a.dtct.SoLuong)
                          }).ToList();
                frmIn frm = new frmIn();
                BaoCao.Rep_BCXetNghiem_30007 rep = new BaoCao.Rep_BCXetNghiem_30007(q2);
                rep.NgayThang.Value = "Từ ngày " + _Tungay.ToShortDateString() + " đến " + _DenNgay.ToShortDateString() + " Nộp vào ngày 01 của tháng liền kề";
                rep.DataSource = q1;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                var _lcls = (from cls in _data.VienPhis.Where(p => p.NgayTT >= _Tungay && p.NgayTT <= _DenNgay)
                             join cd in _data.VienPhicts on cls.idVPhi equals cd.idVPhi
                             join bn in _data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                             select new { cd.idVPhict, cls.MaBNhan, cd.MaDV, bn.DTuong, cd.MaKP, bn.NoiTru, cd.DonVi, cd.SoLuong }).ToList();
                var q1 = (from cls in _lcls
                          join dv in _ldv.Where(p => p.dv.PLoai == 2) on cls.MaDV equals dv.dv.MaDV
                          join kp in _lkp on cls.MaKP equals kp.MaKP
                          group new { cls, kp, dv } by new { kp.MaKP, kp.TenKP } into kq
                          select new
                          {
                              kq.Key.MaKP,
                              kq.Key.TenKP,

                              HHocNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.cls.SoLuong),
                              HHocNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.cls.SoLuong),
                              HHocKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.cls.SoLuong),

                              SHoaNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo).Sum(p => p.cls.SoLuong),
                              SHoaNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo).Sum(p => p.cls.SoLuong),
                              SHoaKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo).Sum(p => p.cls.SoLuong),

                              VSinhNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("vi nấm soi tươi")).Sum(p => p.cls.SoLuong),
                              VSinhNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("vi nấm soi tươi")).Sum(p => p.cls.SoLuong),
                              VSinhKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("vi nấm soi tươi")).Sum(p => p.cls.SoLuong),

                              HIVNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac && p.dv.dv.TenDV.ToLower().Contains("hiv")).Sum(p => p.cls.SoLuong),
                              HIVNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac && p.dv.dv.TenDV.ToLower().Contains("hiv")).Sum(p => p.cls.SoLuong),
                              HIVKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac && p.dv.dv.TenDV.ToLower().Contains("hiv")).Sum(p => p.cls.SoLuong),

                              STietNgTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("xét nghiệm tế bào học áp nhuộm thường quy")).Sum(p => p.cls.SoLuong),
                              STietNTru = kq.Where(p => p.kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang).Where(p => p.cls.DTuong != "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("xét nghiệm tế bào học áp nhuộm thường quy")).Sum(p => p.cls.SoLuong),
                              STietKSK = kq.Where(p => p.cls.DTuong == "KSK").Where(p => p.dv.dv.TenDV.ToLower().Contains("xét nghiệm tế bào học áp nhuộm thường quy")).Sum(p => p.cls.SoLuong)

                          }).OrderBy(p => p.TenKP).ToList();
                var kqs = (from a in q1
                           group a by new { a.MaKP, a.TenKP } into kq
                           select new BC
                           {
                               MaKP = kq.Key.MaKP,
                               TenKP = kq.Key.TenKP,
                               HHocNgTru = kq.Sum(p => p.HHocNgTru),
                               HHocNTru = kq.Sum(p => p.HHocNTru),
                               HHocKSK = kq.Sum(p => p.HHocKSK),

                               SHoaNgTru = kq.Sum(p => p.SHoaNgTru),
                               SHoaNTru = kq.Sum(p => p.SHoaNTru),
                               SHoaKSK = kq.Sum(p => p.SHoaKSK),

                               VSinhNgTru = kq.Sum(p => p.VSinhNgTru),
                               VSinhNTru = kq.Sum(p => p.VSinhNTru),
                               VSinhKSK = kq.Sum(p => p.VSinhKSK),

                               HIVNgTru = kq.Sum(p => p.HIVNgTru),
                               HIVNTru = kq.Sum(p => p.HIVNTru),
                               HIVKSK = kq.Sum(p => p.HIVKSK),

                               STietNgTru = kq.Sum(p => p.STietNgTru),
                               STietNTru = kq.Sum(p => p.STietNTru),
                               STietKSK = kq.Sum(p => p.STietKSK),

                               Tong = kq.Sum(p => p.HHocNgTru + p.HHocNTru + p.HHocKSK + p.SHoaNgTru + p.SHoaNTru + p.SHoaKSK + p.VSinhNgTru + p.VSinhNTru + p.VSinhKSK + p.HIVNgTru + p.HIVNTru + p.HIVKSK + p.STietNgTru + p.STietNTru + p.STietKSK)
                           }).ToList();
                var q2 = (from a in _lcls
                          join dv in _ldv.Where(p => p.n.IDNhom == 7) on a.MaDV equals dv.dv.MaDV
                          group new { a, dv } by new { a.MaDV, dv.dv.TenDV, a.DonVi } into kq
                          select new BCTruyenMau
                          {
                              MaDV = kq.Key.MaDV ?? 0,
                              TenDV = kq.Key.TenDV,
                              DonVi = kq.Key.DonVi,
                              SoLuong = kq.Sum(p => p.a.SoLuong)
                          }).ToList();
                frmIn frm = new frmIn();
                BaoCao.Rep_BCXetNghiem_30007 rep = new BaoCao.Rep_BCXetNghiem_30007(q2);
                rep.NgayThang.Value = "Từ ngày " + _Tungay.ToShortDateString() + " đến " + _DenNgay.ToShortDateString() + " Nộp vào ngày 01 của tháng liền kề";
                rep.DataSource = q1;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        public class BC
        {
            public string TenKP { get; set; }
            public int MaKP { get; set; }

            public double HHocNgTru { get; set; }
            public double HHocNTru { get; set; }
            public double HHocKSK { get; set; }

            public double SHoaNgTru { get; set; }
            public double SHoaNTru { get; set; }
            public double SHoaKSK { get; set; }

            public double VSinhNgTru { get; set; }
            public double VSinhNTru { get; set; }
            public double VSinhKSK { get; set; }

            public double HIVNgTru { get; set; }
            public double HIVNTru { get; set; }
            public double HIVKSK { get; set; }

            public double STietNgTru { get; set; }
            public double STietNTru { get; set; }
            public double STietKSK { get; set; }

            public double Tong { get; set; }
        }
        public class BCTruyenMau
        {
            public string TenDV { get; set; }
            public int MaDV { get; set; }
            public double SoLuong { get; set; }
            public string DonVi { get; set; }
        }
        private void frm_BCXetNghiem_30007_Load(object sender, EventArgs e)
        {
            deTuNgay.DateTime = DateTime.Now;
            deDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}