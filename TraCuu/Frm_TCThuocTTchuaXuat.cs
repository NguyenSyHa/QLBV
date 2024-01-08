using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormTraCuu
{
    public partial class Frm_TCThuocTTchuaXuat : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TCThuocTTchuaXuat()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);
        private class KD
        {
            private int MaDV;

            public int madv
            {
                get { return MaDV; }
                set { MaDV = value; }
            }
            private string TenDV;

            public string tendv
            {
                get { return TenDV; }
                set { TenDV = value; }
            }
            private string DonVi;

            public string donvi
            {
                get { return DonVi; }
                set { DonVi = value; }
            }
            private double DonGia;

            public double dongia
            {
                get { return DonGia; }
                set { DonGia = value; }
            }
            private int SoLuongKD;

            public int soluongkd
            {
                get { return SoLuongKD; }
                set { SoLuongKD = value; }
            }
            private double ThanhTienKD;

            public double thanhtienkd
            {
                get { return ThanhTienKD; }
                set { ThanhTienKD = value; }
            }
            private int SoLuongTT;

            public int soluongtt
            {
                get { return SoLuongTT; }
                set { SoLuongTT = value; }
            }
            private double ThanhTienTT;

            public double thanhtientt
            {
                get { return ThanhTienTT; }
                set { ThanhTienTT = value; }
            }
            private int SLChuaTT;

            public int slchuatt
            {
                get { return SLChuaTT; }
                set { SLChuaTT = value; }
            }
            private double TTChuaTT;

            public double ttchuatt
            {
                get { return TTChuaTT; }
                set { TTChuaTT = value; }
            }
            private int SLDaXuat;

            public int sldaxuat
            {
                get { return SLDaXuat; }
                set { SLDaXuat = value; }
            }
            private double TTDaXuat;

            public double ttdaxuat
            {
                get { return TTDaXuat; }
                set { TTDaXuat = value; }
            }
          
        }
        private class DV
        {
            private int MaDV;
            private string TenDV;
            private string PLoai;
            private string MaNoiBo { get; set; }
            //   private string TenRG;
            public int madv
            {
                set { MaDV = value; }
                get { return MaDV; }
            }
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public string ploai
            { set { PLoai = value; } get { return PLoai; } }
            public string manoibo
            { set { MaNoiBo = value; } get { return MaNoiBo; } }
        }
        List<KPhong> _Kphong = new List<KPhong>();
        List<DV> _ldv = new List<DV>();
        List<KD> _KD = new List<KD>();
        private void FrmTC_NhapXuatTon_Load(object sender, EventArgs e)
        {
            if(DungChung.Bien.MaBV == "24012")
            {
                lupMaDV.Properties.Columns[2].Visible = true;
                lupMaDV.Properties.Columns[3].Visible = true;
                lupMaDV.Properties.Columns[4].Visible = true;
            }
            dtTimTuNgay.DateTime = DungChung.Ham.ConvertNgay("01/01/2015");
            dtTimDenNgay.DateTime = System.DateTime.Now;
            var kho = (from kp in _data.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng") || p.PLoai.Contains("Phòng khám")) select new { kp.TenKP, kp.MaKP }).ToList();
            lupKhoa.Properties.DataSource = kho;
            var qdv = (from dv in _data.DichVus.Where(p => p.PLoai == 1)
                       join dct in _data.NhapDcts on dv.MaDV equals dct.MaDV
                       select new { dv.MaDV, dv.TenDV, dv.MaTam, dct.SoLo, dct.HanDung }).OrderBy(p => p.TenDV).Distinct().ToList();
            if (qdv.Count() > 0)
            {
                DV them1 = new DV();
                them1.madv = 0;
                them1.tendv = "Tất cả";
                 _ldv.Add(them1);
                foreach (var a in qdv)
                {
                    DV themmoi = new DV();
                    themmoi.madv = a.MaDV;
                    themmoi.tendv = a.TenDV;
                     _ldv.Add(themmoi);
                }
                 lupMaDV.Properties.DataSource = qdv.ToList();
            }
            
        }
        int _makhoa = 0;
        int _madv = 0;
       
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (!string.IsNullOrEmpty(lupKhoa.Text) && !string.IsNullOrEmpty(lupMaDV.Text))
            {
                _KD.Clear();
          
                _makhoa = Convert.ToInt32( lupKhoa.EditValue);
                   _madv = Convert.ToInt32( lupMaDV.EditValue);
                        
                    var qkd = (from dt in _data.DThuocs.Where(p => p.MaKP == _makhoa).Where(p => p.NgayKe>= _dttu).Where(p => p.NgayKe <= _dtden)
                                   join dtct in _data.DThuoccts.Where(p=>p.MaDV==_madv) on dt.IDDon equals dtct.IDDon
                                   join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                                   group new { dt, dtct } by new {dv.MaDV, dv.TenDV,dtct.DonVi,dtct.DonGia} into kq
                                   select new
                                   {
                                       MaDV=kq.Key.MaDV,
                                       TenDV=kq.Key.TenDV,
                                       DonVi=kq.Key.DonVi,
                                       DonGia=kq.Key.DonGia,
                                       SoLuongKD=kq.Sum(p=>p.dtct.SoLuong),
                                       ThanhTienKD=kq.Sum(p=>p.dtct.ThanhTien),
                                   }).ToList();
                    if (qkd.Count > 0)
                        {
                            foreach (var x in qkd)
                            {
                                KD them = new KD();
                                them.madv=x.MaDV;
                                them.tendv=x.TenDV;
                                them.dongia = Convert.ToDouble(x.DonGia);
                                them.donvi = x.DonVi;
                                them.soluongkd=Convert.ToInt32(x.SoLuongKD);
                                them.thanhtienkd=Convert.ToInt32(x.ThanhTienKD);
                                _KD.Add(them);
                            }

                        }
                    var qtt = (from vp in _data.VienPhis.Where(p => p.NgayTT >= _dttu).Where(p => p.NgayTT <= _dtden).Where(p => p.MaKP == _makhoa)
                               join vpct in _data.VienPhicts.Where(p => p.MaDV == _madv) on vp.idVPhi equals vpct.idVPhi
                                join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
                               group new { vp, vpct } by new { dv.MaDV, dv.TenDV,vpct.DonVi,vpct.DonGia } into kq
                               select new
                               {
                                   MaDV = kq.Key.MaDV,
                                   TenDV = kq.Key.TenDV,
                                   DonVi = kq.Key.DonVi,
                                   DonGia = kq.Key.DonGia,
                                   SoLuongTT = kq.Sum(p=>p.vpct.SoLuong),
                                   ThanhTienTT = kq.Sum(p=>p.vpct.ThanhTien)
                               }).ToList();
                    if (qtt.Count > 0)
                    {
                        foreach (var a in _KD)
                        {
                            foreach (var b in qtt)
                            {
                                if (a.madv == b.MaDV)
                                {
                                    if (a.donvi == b.DonVi)
                                    {
                                        if (a.dongia == b.DonGia)
                                        {
                                            a.soluongtt = Convert.ToInt32(b.SoLuongTT);
                                            a.thanhtientt = Convert.ToDouble(b.ThanhTienTT);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    var qdxd = (from dt in _data.DThuocs.Where(p => p.MaKP == _makhoa).Where(p => p.NgayKe >= _dttu).Where(p => p.NgayKe <= _dtden)
                                join dtct in _data.DThuoccts.Where(p => p.MaDV == _madv).Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
                                join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                                group new { dt, dtct } by new { dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into kq
                                select new
                                {
                                    MaDV = kq.Key.MaDV,
                                    TenDV = kq.Key.TenDV,
                                    DonVi = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    SoLuongX = kq.Sum(p => p.dtct.SoLuong),
                                    ThanhTienX = kq.Sum(p => p.dtct.ThanhTien),
                                }).ToList();
                    if (qdxd.Count > 0)
                    {
                        foreach (var a in _KD)
                        {
                            foreach (var b in qdxd)
                            {
                                if (a.madv == b.MaDV)
                                {
                                    if (a.donvi == b.DonVi)
                                    {
                                        if (a.dongia == b.DonGia)
                                        {
                                            a.sldaxuat = Convert.ToInt32(b.SoLuongX);
                                            a.ttdaxuat = Convert.ToDouble(b.ThanhTienX);
                                        }
                                    }
                                }
                            }
                        }
                    }
            }
            if (string.IsNullOrEmpty(lupKhoa.Text) && !string.IsNullOrEmpty(lupMaDV.Text))
            { // _makhoa = lupKhoa.EditValue.ToString();
                _KD.Clear();
          
                _madv = lupMaDV.EditValue== null ? 0 : Convert.ToInt32(lupMaDV.EditValue);

                var qkd = (from dt in _data.DThuocs.Where(p => p.NgayKe >= _dttu).Where(p => p.NgayKe <= _dtden)
                           join dtct in _data.DThuoccts.Where(p => p.MaDV == _madv) on dt.IDDon equals dtct.IDDon
                           join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                           group new { dt, dtct } by new {dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into kq
                           select new
                           {
                               MaDV = kq.Key.MaDV,
                               TenDV = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               DonGia = kq.Key.DonGia,
                               SoLuongKD = kq.Sum(p => p.dtct.SoLuong),
                               ThanhTienKD = kq.Sum(p => p.dtct.ThanhTien),
                           }).ToList();
                if (qkd.Count > 0)
                {
                    foreach (var x in qkd)
                    {
                        KD them = new KD();
                        them.madv = x.MaDV;
                        them.tendv = x.TenDV;
                        them.dongia = Convert.ToDouble(x.DonGia);
                        them.donvi = x.DonVi;
                        them.soluongkd = Convert.ToInt32(x.SoLuongKD);
                        them.thanhtienkd = Convert.ToDouble(x.ThanhTienKD);
                        _KD.Add(them);
                    }

                }
                var qtt = (from vp in _data.VienPhis.Where(p => p.NgayTT >= _dttu).Where(p => p.NgayTT <= _dtden)
                           join vpct in _data.VienPhicts.Where(p => p.MaDV == _madv) on vp.idVPhi equals vpct.idVPhi
                           join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
                           group new { vp, vpct } by new {dv.MaDV, dv.TenDV, vpct.DonVi, vpct.DonGia } into kq
                           select new
                           {
                               MaDV = kq.Key.MaDV,
                               TenDV = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               DonGia = kq.Key.DonGia,
                               SoLuongTT = kq.Sum(p=>p.vpct.SoLuong),
                               ThanhTienTT = kq.Sum(p=>p.vpct.ThanhTien)
                           }).ToList();
                if (qtt.Count > 0)
                {
                    foreach (var a in _KD)
                    {
                        foreach (var b in qtt)
                        {
                            if (a.madv == b.MaDV)
                            {
                                if (a.donvi == b.DonVi)
                                {
                                    if (a.dongia == b.DonGia)
                                    {
                                        a.soluongtt = Convert.ToInt32(b.SoLuongTT);
                                        a.thanhtientt = Convert.ToDouble(b.ThanhTienTT);
                                    }
                                }
                            }
                        }
                    }
                }

                var qdxd = (from dt in _data.DThuocs.Where(p => p.NgayKe >= _dttu).Where(p => p.NgayKe <= _dtden)
                            join dtct in _data.DThuoccts.Where(p => p.MaDV == _madv).Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
                            join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                            group new { dt, dtct } by new {dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into kq
                            select new
                            {
                                MaDV = kq.Key.MaDV,
                                TenDV = kq.Key.TenDV,
                                DonVi = kq.Key.DonVi,
                                DonGia = kq.Key.DonGia,
                                SoLuongX = kq.Sum(p => p.dtct.SoLuong),
                                ThanhTienX = kq.Sum(p => p.dtct.ThanhTien),
                            }).ToList();
                if (qdxd.Count > 0)
                {
                    foreach (var a in _KD)
                    {
                        foreach (var b in qdxd)
                        {
                            if (a.madv == b.MaDV)
                            {
                                if (a.donvi == b.DonVi)
                                {
                                    if (a.dongia == b.DonGia)
                                    {
                                        a.sldaxuat = Convert.ToInt32(b.SoLuongX);
                                        a.ttdaxuat = Convert.ToDouble(b.ThanhTienX);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(lupKhoa.Text) && string.IsNullOrEmpty(lupMaDV.Text))
            {
                _KD.Clear();
          
                _makhoa = Convert.ToInt32( lupKhoa.EditValue);
               // _madv = lupMaDV.EditValue.ToString();

                var qkd = (from dt in _data.DThuocs.Where(p => p.MaKP == _makhoa).Where(p => p.NgayKe >= _dttu).Where(p => p.NgayKe <= _dtden)
                           join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                           join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                           group new { dt, dtct } by new { dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into kq
                           select new
                           {
                               MaDV = kq.Key.MaDV,
                               TenDV = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               DonGia = kq.Key.DonGia,
                               SoLuongKD = kq.Sum(p => p.dtct.SoLuong),
                               ThanhTienKD = kq.Sum(p => p.dtct.ThanhTien),
                           }).ToList();
                if (qkd.Count > 0)
                {
                    foreach (var x in qkd)
                    {
                        KD them = new KD();
                        them.madv = x.MaDV;
                        them.tendv = x.TenDV;
                        them.dongia = Convert.ToDouble(x.DonGia);
                        them.donvi = x.DonVi;
                        them.soluongkd = Convert.ToInt32(x.SoLuongKD);
                        them.thanhtienkd = Convert.ToDouble(x.ThanhTienKD);
                        _KD.Add(them);
                    }

                }
                var qtt = (from vp in _data.VienPhis.Where(p => p.NgayTT >= _dttu).Where(p => p.NgayTT <= _dtden).Where(p => p.MaKP == _makhoa)
                           join vpct in _data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
                            group new { vp, vpct } by new { dv.MaDV, dv.TenDV,vpct.DonVi,vpct.DonGia } into kq
                           select new
                           {
                               //MaBN=kq.Key.MaBNhan,
                               MaDV = kq.Key.MaDV,
                               TenDV = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               DonGia = kq.Key.DonGia,
                               SoLuongTT = kq.Sum(p => p.vpct.SoLuong),
                               ThanhTienTT = kq.Sum(p => p.vpct.ThanhTien)
                           }).ToList();
          
                if (qtt.Count > 0)
                {
                    foreach (var a in _KD)
                    {
                        foreach (var b in qtt)
                        {
                            if (a.madv == b.MaDV)
                            {
                                if (a.donvi == b.DonVi)
                                {
                                    if (a.dongia == b.DonGia)
                                    {
                                        a.soluongtt = Convert.ToInt32(b.SoLuongTT);
                                        a.thanhtientt = Convert.ToDouble(b.ThanhTienTT);
                                    }
                                }
                            }
                        }
                    }
                }

                var qdxd = (from dt in _data.DThuocs.Where(p => p.MaKP == _makhoa).Where(p => p.NgayKe >= _dttu).Where(p => p.NgayKe <= _dtden)
                            join dtct in _data.DThuoccts.Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
                            join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                            group new { dt, dtct } by new { dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into kq
                            select new
                            {
                                MaDV = kq.Key.MaDV,
                                TenDV = kq.Key.TenDV,
                                DonVi = kq.Key.DonVi,
                                DonGia = kq.Key.DonGia,
                                SoLuongX = kq.Sum(p => p.dtct.SoLuong),
                                ThanhTienX = kq.Sum(p => p.dtct.ThanhTien),
                            }).ToList();
                if (qdxd.Count > 0)
                {
                    foreach (var a in _KD)
                    {
                        foreach (var b in qdxd)
                        {
                            if (a.madv == b.MaDV)
                            {
                                if (a.donvi == b.DonVi)
                                {
                                    if (a.dongia == b.DonGia)
                                    {
                                        a.sldaxuat = Convert.ToInt32(b.SoLuongX);
                                        a.ttdaxuat = Convert.ToDouble(b.ThanhTienX);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(lupKhoa.Text) && string.IsNullOrEmpty(lupMaDV.Text))
            {
                //_makhoa = lupKhoa.EditValue.ToString();
                //_madv = lupMaDV.EditValue.ToString();
                _KD.Clear();
          
                var qkd = (from dt in _data.DThuocs.Where(p => p.NgayKe >= _dttu).Where(p => p.NgayKe <= _dtden)
                           join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                           join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                           group new { dt, dtct } by new {dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into kq
                           select new
                           {
                               MaDV = kq.Key.MaDV,
                               TenDV = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               DonGia = kq.Key.DonGia,
                               SoLuongKD = kq.Sum(p => p.dtct.SoLuong),
                               ThanhTienKD = kq.Sum(p => p.dtct.ThanhTien),
                           }).ToList();
                if (qkd.Count > 0)
                {
                    foreach (var x in qkd)
                    {
                        KD them = new KD();
                        them.madv = x.MaDV;
                        them.tendv = x.TenDV;
                        them.dongia = Convert.ToDouble(x.DonGia);
                        them.donvi = x.DonVi;
                        them.soluongkd = Convert.ToInt32(x.SoLuongKD);
                        them.thanhtienkd = Convert.ToDouble(x.ThanhTienKD);
                        _KD.Add(them);
                    }

                }
                var qtt = (from vp in _data.VienPhis.Where(p => p.NgayTT >= _dttu).Where(p => p.NgayTT <= _dtden)
                           join vpct in _data.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           join dv in _data.DichVus on vpct.MaDV equals dv.MaDV
                           group new { vp, vpct } by new { dv.MaDV, dv.TenDV, vpct.DonVi, vpct.DonGia } into kq
                           select new
                           {
                               MaDV = kq.Key.MaDV,
                               TenDV = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               DonGia = kq.Key.DonGia,
                               SoLuongTT = kq.Sum(p => p.vpct.SoLuong),
                               ThanhTienTT = kq.Sum(p => p.vpct.ThanhTien)
                      
                           }).ToList();
                if (qtt.Count > 0)
                {
                    foreach (var a in _KD)
                    {
                        foreach (var b in qtt)
                        {
                            if (a.madv == b.MaDV)
                            {
                                if (a.donvi == b.DonVi)
                                {
                                    if (a.dongia == b.DonGia)
                                    {
                                        a.soluongtt = Convert.ToInt32(b.SoLuongTT);
                                        a.thanhtientt = Convert.ToDouble(b.ThanhTienTT);
                                    }
                                }
                            }
                        }
                    }
                }
                var qdxd = (from dt in _data.DThuocs.Where(p => p.NgayKe >= _dttu).Where(p => p.NgayKe <= _dtden)
                            join dtct in _data.DThuoccts.Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
                            join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
                            group new { dt, dtct } by new { dv.MaDV, dv.TenDV, dtct.DonVi, dtct.DonGia } into kq
                            select new
                            {
                                MaDV = kq.Key.MaDV,
                                TenDV = kq.Key.TenDV,
                                DonVi = kq.Key.DonVi,
                                DonGia = kq.Key.DonGia,
                                SoLuongX = kq.Sum(p => p.dtct.SoLuong),
                                ThanhTienX = kq.Sum(p => p.dtct.ThanhTien),
                            }).ToList();
                if (qdxd.Count > 0)
                {
                    foreach (var a in _KD)
                    {
                        foreach (var b in qdxd)
                        {
                            if (a.madv == b.MaDV)
                            {
                                if (a.donvi == b.DonVi)
                                {
                                    if (a.dongia == b.DonGia)
                                    {
                                        a.sldaxuat = Convert.ToInt32(b.SoLuongX);
                                        a.ttdaxuat = Convert.ToDouble(b.ThanhTienX);
                                    }
                                }
                            }
                        }
                    }
                }
            }
                 grcTraCuu.DataSource = _KD.OrderBy(p => p.tendv).Where(p=>p.sldaxuat>0).ToList();
             
           
        }

      
        private void grvTraCuu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvTraCuu_DoubleClick(object sender, EventArgs e)
        {
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime); 
            int madv = 0; 

            if (grvTraCuu.GetFocusedRowCellValue(colMaDV) != null )
            {
                madv = Convert.ToInt32( grvTraCuu.GetFocusedRowCellValue(colMaDV));//.ToString();

                FormTraCuu.Frm_TCThuocTTchuaXuatct frm = new FormTraCuu.Frm_TCThuocTTchuaXuatct( madv,_dttu,_dtden);
                frm.ShowDialog();
            
            }
        }
        //int _id = -1;
       private void grvTraCuu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        //      if (grvTraCuu.GetFocusedRowCellValue(colMaDV) != null && grvTraCuu.GetFocusedRowCellValue(colMaDV).ToString() != "")
        //    {
        //        _id =Convert.ToInt32(grvTraCuu.GetFocusedRowCellValue(colMaDV));//.ToString();

        //    }
        }

       private void radioGroup2_MouseClick(object sender, MouseEventArgs e)
       {

       }

        private void grcTraCuu_Click(object sender, EventArgs e)
        {

        }

        //private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    _ldv.Clear();
        //    var kho = (from kp in _data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") select new { kp.TenKP, kp.MaKP,kp.PLoai }).ToList();
        //    if (kho.Count() > 0)
        //    {
        //        DV them1 = new DV();
        //        them1.madv = " ";
        //        them1.tendv = "Tất cả";
        //        _ldv.Add(them1);
        //        foreach (var a in kho)
        //        {
        //            DV themmoi = new DV();
        //            themmoi.madv = a.MaKP;
        //            themmoi.tendv = a.TenKP;
        //            themmoi.ploai=a.PLoai;
        //            _ldv.Add(themmoi);
        //        }
        //    }

        //    if (radioGroup1.SelectedIndex == 0)
        //    {
        //        lupMaDV.Properties.DataSource = _ldv;
        //    }
        //    if (radioGroup1.SelectedIndex == 1)
        //    {
        //        lupMaDV.Properties.DataSource = _ldv.Where(p => p.ploai == "Lâm sàng").ToList();
        //    }
        //    if (radioGroup1.SelectedIndex == 2)
        //    {
        //        lupMaDV.Properties.DataSource = _ldv.Where(p => p.ploai == "Phòng khám").ToList(); ;
        //    }


        //}
    }
}