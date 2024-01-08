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
    public partial class Frm_SoPhauThuat : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoPhauThuat()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }

            return true;
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
        private class DV
        {
            private int MaDV;
            private string TenDV;
            //   private string TenRG;
            public int madv
            {
                set { MaDV = value; }
                get { return MaDV; }
            }
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            //public string tenrg
            //{ set { TenRG = value; } get { return TenRG; } }
        }
        List<KPhong> _Kphong = new List<KPhong>();
        List<DV> _ldv = new List<DV>();
        private void Frm_SoPhauThuat_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            var kphong = (from kp in data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
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
            var qdv = (from dv in data.DichVus.Where(p => p.PLoai != 1)
                       join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Phẫu thuật") on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new { dv.MaDV, dv.TenDV, dv.TenRG }).ToList();
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

                lupDichVu.Properties.DataSource = _ldv.ToList();
            }
        }
        private class BN
        {
            private string TenBNhan;
            private int MaBNhan;
            private int GTinh;
            private int Tuoi;
            private string DiaChi;
            private string DTuong;
            private string ChanDoan;
            private string ChanDoanSau;
            private int MaKP;
            private string NoiGui;
            private string YeuCau;
            private string KetQua;
            private string BSTH;
            private string NgayTH;
            private int MaDV;
            public string tenbnhan
            { set { TenBNhan = value; } get { return TenBNhan; } }
            public int mabnhan
            { set { MaBNhan = value; } get { return MaBNhan; } }
            public int madv
            { set { MaDV = value; } get { return MaDV; } }
            public int gtinh
            { set { GTinh = value; } get { return GTinh; } }
            public int tuoi
            { set { Tuoi = value; } get { return Tuoi; } }
            public string diachi
            { set { DiaChi = value; } get { return DiaChi; } }
            public string dtuong
            { set { DTuong = value; } get { return DTuong; } }
            public string chandoansau
            { set { ChanDoanSau = value; } get { return ChanDoanSau; } }
            public string chandoan
            { set { ChanDoan = value; } get { return ChanDoan; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string noigui
            { set { NoiGui = value; } get { return NoiGui; } }
            public string yeucau
            { set { YeuCau = value; } get { return YeuCau; } }
            public string ketqua
            { set { KetQua = value; } get { return KetQua; } }
            public string bsth
            { set { BSTH = value; } get { return BSTH; } }
            public string ngayth
            { set { NgayTH = value; } get { return NgayTH; } }

        }
        List<BN> _BN = new List<BN>();
        List<KP> _lkp = new List<KP>();
        public class KP
        {
            private int makp;
            public int MaKP
            {
                set { makp = value; }
                get { return makp; }
            }
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;

            frmIn frm = new frmIn();
            if (KTtaoBc())
            {
                _BN.Clear();

                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                var _ldv = (from dv in data.DichVus.Where(p => p.PLoai == 2)
                            join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Phẫu thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                            select new { dv.TenDV, dv.MaDV }).ToList();
                _lkp.Clear();
                for (int k = 0; k < grvKhoaphong.RowCount; k++)
                {
                    if (grvKhoaphong.GetRowCellValue(k, Chọn).ToString().ToLower() == "true")
                    {
                        _lkp.Add(new KP { MaKP = grvKhoaphong.GetRowCellValue(k, MaKP) == null ? 0 : Convert.ToInt32(grvKhoaphong.GetRowCellValue(k, MaKP) )});
                    }
                }
                if ( lupDichVu.EditValue == null || Convert.ToInt32( lupDichVu.EditValue) ==0)
                {
                  //  var a1 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay) group kb by kb.MaBNhan into kq select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();

                    if (cboTKBN.Text == "Tất cả BN thực hiện PT")
                    {
                        //foreach (var b in a1)
                        //{
                       
                        var _lbn = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                    join dt in data.DThuocs.Where(p => p.PLDV == 2) on dtct.IDDon equals dt.IDDon
                                    join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                    join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                    select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap, dtct.MaDV }).ToList();
                        var qso = (from ma in _lkp
                                   join bn in _lbn on ma.MaKP equals bn.MaKP
                                   join dv in _ldv on bn.MaDV equals dv.MaDV
                                   //join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on ma.MaKP equals dtct.MaKP
                                   //join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                   //join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                   //join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                   //join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                   //    join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Phẫu thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                   group new { bn, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.TenDV, dv.MaDV, bn.MaKP, bn.TenKP, bn.MaCB, bn.NgayNhap } into kq
                                       select new
                                       {
                                           MaBNhan = kq.Key.MaBNhan,
                                           TenBNhan = kq.Key.TenBNhan,
                                           GTinh = kq.Key.GTinh,
                                           Tuoi = kq.Key.Tuoi,
                                           DTuong = kq.Key.DTuong,
                                            DiaChi = kq.Key.DChi,
                                           TenDV = kq.Key.TenDV,
                                           MakP = kq.Key.MaKP,
                                           TenKP = kq.Key.TenKP,
                                           BSTH = kq.Key.MaCB,
                                           NgayTH = kq.Key.NgayNhap,
                                           MaDV = kq.Key.MaDV
                                       }).ToList();
                            if (qso.Count() > 0)
                            {
                                foreach (var a in qso)
                                {
                                    BN themmoi = new BN();
                                    themmoi.mabnhan = a.MaBNhan;
                                    themmoi.tenbnhan = a.TenBNhan;
                                    themmoi.gtinh = Convert.ToInt32(a.GTinh.ToString());
                                    themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                                    themmoi.dtuong = a.DTuong;
                                    themmoi.diachi = a.DiaChi;
                                    themmoi.yeucau = a.TenDV;
                                    themmoi.makp = a.MakP;
                                    themmoi.noigui = a.TenKP;
                                    themmoi.bsth = a.BSTH;
                                    themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                    themmoi.madv = a.MaDV;
                                    _BN.Add(themmoi);
                                }
                            }

                            var qcd = (from dt in data.DThuocs
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                       join bnkb in data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                                       select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                            
                            if (qcd.Count > 0)
                            {
                                foreach (var a in qcd)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan)
                                        {
                                            if (a.MaKP == b.makp)
                                            {
                                                b.chandoan = a.ChanDoan.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            var kqq = (from a in data.CLS
                                       join b in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on a.IdCLS equals b.IdCLS
                                       join c in data.CLScts on b.IDCD equals c.IDCD
                                       select new
                                       {
                                           a.MaBNhan,
                                           b.MaDV,
                                           c.KetQua,
                                           b.NgayTH
                                       }).ToList();
                            if (kqq.Count > 0)
                            {
                                foreach (var a in kqq)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan && a.MaDV == b.madv && a.NgayTH.ToString().Substring(0, 10) == b.ngayth)
                                        {
                                            b.chandoansau = a.KetQua;
                                        }
                                    }
                                }
                            }

                    }
                    if (cboTKBN.Text == "Chỉ BN đã thanh toán")
                    {
                        //foreach (var b in a1)
                        //{
                        var _lbn = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                    join dt in data.DThuocs.Where(p => p.PLDV == 2) on dtct.IDDon equals dt.IDDon
                                    join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                    join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                                    join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                    select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap, dtct.MaDV }).ToList();

                        var qbntt = (from ma in _lkp
                                     join bn in _lbn on ma.MaKP equals bn.MaKP
                                     join dv in _ldv on bn.MaDV equals dv.MaDV
                                     //join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on ma.MaKP equals dtct.MaKP
                                     //join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                     //join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                                     //join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                     //join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                     //join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                         //join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Phẫu thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                     group new { bn, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.TenDV, bn.MaKP, bn.TenKP, bn.MaCB, bn.NgayNhap } into kq
                                         select new
                                         {
                                             MaBNhan = kq.Key.MaBNhan,
                                             TenBNhan = kq.Key.TenBNhan,
                                             GTinh = kq.Key.GTinh,
                                             Tuoi = kq.Key.Tuoi,
                                             DTuong = kq.Key.DTuong,
                                             DiaChi = kq.Key.DChi,
                                             TenDV = kq.Key.TenDV,
                                             MakP = kq.Key.MaKP,
                                             TenKP = kq.Key.TenKP,
                                             BSTH = kq.Key.MaCB,
                                             NgayTH = kq.Key.NgayNhap,
                                         }).ToList();
                            if (qbntt.Count() > 0)
                            {
                                foreach (var a in qbntt)
                                {
                                    BN themmoi = new BN();
                                    themmoi.mabnhan = a.MaBNhan;
                                    themmoi.tenbnhan = a.TenBNhan;
                                    themmoi.gtinh = Convert.ToInt32(a.GTinh.ToString());
                                    themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                                    themmoi.dtuong = a.DTuong;
                                    themmoi.diachi = a.DiaChi;
                                    themmoi.yeucau = a.TenDV;
                                    themmoi.noigui = a.TenKP;
                                    themmoi.makp = a.MakP;
                                    themmoi.bsth = a.BSTH;
                                    themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                    _BN.Add(themmoi);
                                }
                            }
                        
                            var qcd = (from dt in data.DThuocs
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                       join bnkb in data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                                       select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                            if (qcd.Count > 0)
                            {
                                foreach (var a in qcd)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan)
                                        {
                                            if (a.MaKP == b.makp)
                                            {
                                                b.chandoan = a.ChanDoan.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            var kqq = (from a in data.CLS
                                       join b in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on a.IdCLS equals b.IdCLS
                                       join c in data.CLScts on b.IDCD equals c.IDCD
                                       select new
                                       {
                                           a.MaBNhan,
                                           b.MaDV,
                                           c.KetQua,
                                           b.NgayTH
                                       }).ToList();
                            if (kqq.Count > 0)
                            {
                                foreach (var a in kqq)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan && a.MaDV == b.madv && a.NgayTH.ToString().Substring(0, 10) == b.ngayth)
                                        {
                                            b.chandoansau = a.KetQua;
                                        }
                                    }
                                }
                            }
                    }

                    if (cboTKBN.Text == "Chỉ BN chưa thanh toán")
                    {
                        //foreach (var b in a1)
                        //{
                        var _lbn = (from dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                    join dt in data.DThuocs.Where(p => p.PLDV == 2) on dtct.IDDon equals dt.IDDon
                                    join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                    join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                    where !(from vp in data.VienPhis select vp.MaBNhan).Contains(dt.MaBNhan)
                                    select new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap, dtct.MaDV }).ToList();
                        var qso = (from ma in _lkp
                                   join bn in _lbn on ma.MaKP equals bn.MaKP
                                   join dv in _ldv on bn.MaDV equals dv.MaDV
                                   //join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on ma.MaKP equals dtct.MaKP
                                   //join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                   //join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                   //join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                   //join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                   //join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Phẫu thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                   //where !(from vp in data.VienPhis select vp.MaBNhan).Contains(dt.MaBNhan)
                                   group new { bn, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.MaDV, dv.TenDV, bn.MaKP, bn.TenKP, bn.MaCB, bn.NgayNhap } into kq
                                   select new
                                   {
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenBNhan = kq.Key.TenBNhan,
                                       GTinh = kq.Key.GTinh,
                                       Tuoi = kq.Key.Tuoi,
                                       DTuong = kq.Key.DTuong,
                                       DiaChi = kq.Key.DChi,
                                       TenDV = kq.Key.TenDV,
                                       MakP = kq.Key.MaKP,
                                       TenKP = kq.Key.TenKP,
                                       BSTH = kq.Key.MaCB,
                                       NgayTH = kq.Key.NgayNhap,
                                       MaDV = kq.Key.MaDV
                                   }).ToList();


                            if (qso.Count() > 0)
                            {
                                foreach (var a in qso)
                                {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh.ToString());
                                themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                                themmoi.dtuong = a.DTuong;
                                themmoi.diachi = a.DiaChi;
                                themmoi.yeucau = a.TenDV;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                themmoi.bsth = a.BSTH;
                                themmoi.ngayth =a.NgayTH.ToString().Substring(0, 10);
                                themmoi.madv = a.MaDV;
                                _BN.Add(themmoi);
                                }
                            }

                            var qcd = (from dt in data.DThuocs
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                       join bnkb in data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                                       select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                            if (qcd.Count > 0)
                            {
                                foreach (var a in qcd)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan)
                                        {
                                            if (a.MaKP == b.makp)
                                            {
                                                b.chandoan = a.ChanDoan.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            var kqq = (from a in data.CLS
                                       join b in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on a.IdCLS equals b.IdCLS
                                       join c in data.CLScts on b.IDCD equals c.IDCD
                                       select new
                                       {
                                           a.MaBNhan,
                                           b.MaDV,
                                           c.KetQua,
                                           b.NgayTH
                                       }).ToList();
                            if (kqq.Count > 0)
                            {
                                foreach (var a in kqq)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan && a.MaDV == b.madv && a.NgayTH.ToString().Substring(0, 10) == b.ngayth)
                                        {
                                            b.chandoansau = a.KetQua;
                                        }
                                    }
                                }
                            }
                    }

                    if (_BN.Count > 0)
                    {
                        if (radMau.SelectedIndex == 0)
                        {
                            BaoCao.Rep_SoPhauThuat_A4 rep = new BaoCao.Rep_SoPhauThuat_A4();
                            rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                            rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                            rep.Nam.Value = _BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count()).ToString();
                            rep.Nu.Value = _BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count()).ToString();
                            rep.BHYT.Value = _BN.Where(p => p.dtuong=="BHYT").Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.dtuong=="BHYT").Select(p => p.mabnhan).Count()).ToString();
                            rep.BindingData();
                            rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        if (radMau.SelectedIndex == 1)
                        {
                            BaoCao.Rep_SoPhauThuat rep = new BaoCao.Rep_SoPhauThuat();
                            rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                            rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                            rep.Nam.Value = _BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count()).ToString();
                            rep.Nu.Value = _BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count()).ToString();
                            rep.BHYT.Value = _BN.Where(p => p.dtuong == "BHYT").Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.dtuong == "BHYT").Select(p => p.mabnhan).Count()).ToString();
                            rep.BindingData();
                            rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else MessageBox.Show("Không có dữ liệu để in báo cáo ");
                }
                else
                {
                    int madv = lupDichVu.EditValue == null ? 0 : Convert.ToInt32(lupDichVu.EditValue);
                  //  var a1 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay) group kb by kb.MaBNhan into kq select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();

                    if (cboTKBN.Text == "Tất cả BN thực hiện PT")
                    {
                        //foreach (var b in a1)
                        //{
                            var qso = (from ma in _lkp
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p=>p.MaDV== madv) on ma.MaKP equals dtct.MaKP
                                       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Phẫu thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       group new { bn, dt, dtct, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.MaDV, dv.TenDV, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap } into kq
                                       select new
                                       {
                                           MaBNhan = kq.Key.MaBNhan,
                                           TenBNhan = kq.Key.TenBNhan,
                                           GTinh = kq.Key.GTinh,
                                           Tuoi = kq.Key.Tuoi,
                                           DTuong = kq.Key.DTuong,
                                           DiaChi = kq.Key.DChi,
                                           TenDV = kq.Key.TenDV,
                                           MakP = kq.Key.MaKP,
                                           TenKP = kq.Key.TenKP,
                                           BSTH = kq.Key.MaCB,
                                           NgayTH = kq.Key.NgayNhap,
                                           MaDV = kq.Key.MaDV
                                       }).ToList();
                            if (qso.Count() > 0)
                            {
                                foreach (var a in qso)
                                {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh.ToString());
                                themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                                themmoi.dtuong = a.DTuong;
                                themmoi.diachi = a.DiaChi;
                                themmoi.yeucau = a.TenDV;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                themmoi.bsth = a.BSTH;
                                themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                themmoi.madv = a.MaDV;
                                _BN.Add(themmoi);
                                }
                            }
                            var qcd = (from dt in data.DThuocs
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                       join bnkb in data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                                       select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                            if (qcd.Count > 0)
                            {
                                foreach (var a in qcd)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan)
                                        {
                                            if (a.MaKP == b.makp)
                                            {
                                                b.chandoan = a.ChanDoan.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            var kqq = (from a in data.CLS
                                       join b in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on a.IdCLS equals b.IdCLS
                                       join c in data.CLScts on b.IDCD equals c.IDCD
                                       select new
                                       {
                                           a.MaBNhan,
                                           b.MaDV,
                                           c.KetQua,
                                           b.NgayTH
                                       }).ToList();
                            if (kqq.Count > 0)
                            {
                                foreach (var a in kqq)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan && a.MaDV == b.madv && a.NgayTH.ToString().Substring(0, 10) == b.ngayth)
                                        {
                                            b.chandoansau = a.KetQua;
                                        }
                                    }
                                }
                            }
                    }
                    if (cboTKBN.Text == "Chỉ BN đã thanh toán")
                    {
                        //foreach (var b in a1)
                        //{
                        var qbntt = (from ma in _lkp
                                     join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.MaDV == madv) on ma.MaKP equals dtct.MaKP
                                     join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                     join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                                     join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                     join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                     join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                         join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Phẫu thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                         group new { bn, dv, vp, dtct } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.MaDV, dv.TenDV, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap } into kq
                                         select new
                                         {
                                             MaBNhan = kq.Key.MaBNhan,
                                             TenBNhan = kq.Key.TenBNhan,
                                             GTinh = kq.Key.GTinh,
                                             Tuoi = kq.Key.Tuoi,
                                             DTuong = kq.Key.DTuong,
                                              DiaChi = kq.Key.DChi,
                                             TenDV = kq.Key.TenDV,
                                             MakP = kq.Key.MaKP,
                                             TenKP = kq.Key.TenKP,
                                             BSTH = kq.Key.MaCB,
                                             NgayTH = kq.Key.NgayNhap,
                                             MaDV = kq.Key.MaDV
                                         }).ToList();
                            if (qbntt.Count() > 0)
                            {
                                foreach (var a in qbntt)
                                {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh.ToString());
                                themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                                themmoi.dtuong = a.DTuong;
                                themmoi.diachi = a.DiaChi;
                                themmoi.yeucau = a.TenDV;
                                 themmoi.noigui = a.TenKP;
                                themmoi.makp = a.MakP;
                                themmoi.bsth = a.BSTH;
                                themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                themmoi.madv = a.MaDV;
                                _BN.Add(themmoi);
                                }
                            }

                            var qcd = (from dt in data.DThuocs
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                       join bnkb in data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                                       select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                            if (qcd.Count > 0)
                            {
                                foreach (var a in qcd)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan)
                                        {
                                            if (a.MaKP == b.makp)
                                            {
                                                b.chandoan = a.ChanDoan.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            var kqq = (from a in data.CLS
                                       join b in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on a.IdCLS equals b.IdCLS
                                       join c in data.CLScts on b.IDCD equals c.IDCD
                                       select new
                                       {
                                           a.MaBNhan,
                                           b.MaDV,
                                           c.KetQua,
                                           b.NgayTH
                                       }).ToList();
                            if (kqq.Count > 0)
                            {
                                foreach (var a in kqq)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan && a.MaDV == b.madv && a.NgayTH.ToString().Substring(0, 10) == b.ngayth)
                                        {
                                            b.chandoansau = a.KetQua;
                                        }
                                    }
                                }
                            }
                    }

                    if (cboTKBN.Text == "Chỉ BN chưa thanh toán")
                    {
                        //foreach (var b in a1)
                        //{
                            var qso = (from ma in _lkp
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.MaDV == madv) on ma.MaKP equals dtct.MaKP
                                       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Phẫu thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       where !(from vp in data.VienPhis select vp.MaBNhan).Contains(dt.MaBNhan)
                                        group new { bn, dt, dtct, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.MaDV, dv.TenDV, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap } into kq
                                       select new
                                       {
                                           MaBNhan = kq.Key.MaBNhan,
                                           TenBNhan = kq.Key.TenBNhan,
                                           GTinh = kq.Key.GTinh,
                                           Tuoi = kq.Key.Tuoi,
                                           DTuong = kq.Key.DTuong,
                                           DiaChi = kq.Key.DChi,
                                           TenDV = kq.Key.TenDV,
                                           MakP = kq.Key.MaKP,
                                           TenKP = kq.Key.TenKP,
                                           BSTH = kq.Key.MaCB,
                                           NgayTH = kq.Key.NgayNhap,
                                           MaDV = kq.Key.MaDV
                                       }).ToList();

                            if (qso.Count() > 0)
                            {
                                foreach (var a in qso)
                                {
                                BN themmoi = new BN();
                                themmoi.mabnhan = a.MaBNhan;
                                themmoi.tenbnhan = a.TenBNhan;
                                themmoi.gtinh = Convert.ToInt32(a.GTinh.ToString());
                                themmoi.tuoi = Convert.ToInt32(a.Tuoi.ToString());
                                themmoi.dtuong = a.DTuong;
                                themmoi.diachi = a.DiaChi;
                                themmoi.yeucau = a.TenDV;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                themmoi.bsth = a.BSTH;
                                themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                themmoi.madv = a.MaDV;
                                _BN.Add(themmoi);
                                }
                            }

                            var qcd = (from dt in data.DThuocs
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on dt.IDDon equals dtct.IDDon
                                       join bnkb in data.BNKBs on dt.MaBNhan equals bnkb.MaBNhan
                                       select new { bnkb.MaBNhan, bnkb.MaKP, bnkb.ChanDoan, }).ToList();
                            if (qcd.Count > 0)
                            {
                                foreach (var a in qcd)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan)
                                        {
                                            if (a.MaKP == b.makp)
                                            {
                                                b.chandoan = a.ChanDoan.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            var kqq = (from a in data.CLS
                                       join b in data.ChiDinhs.Where(p => p.Status == 1).Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on a.IdCLS equals b.IdCLS
                                       join c in data.CLScts on b.IDCD equals c.IDCD
                                       select new
                                       {
                                           a.MaBNhan,
                                           b.MaDV,
                                           c.KetQua,
                                           b.NgayTH
                                       }).ToList();
                            if (kqq.Count > 0)
                            {
                                foreach (var a in kqq)
                                {
                                    foreach (var b in _BN)
                                    {
                                        if (a.MaBNhan == b.mabnhan && a.MaDV == b.madv && a.NgayTH.ToString().Substring(0, 10) == b.ngayth)
                                        {
                                            b.chandoansau = a.KetQua;
                                        }
                                    }
                                }
                            }
                    }

                    if (_BN.Count > 0)
                    {
                        if (radMau.SelectedIndex == 0)
                        {
                            BaoCao.Rep_SoPhauThuat_A4 rep = new BaoCao.Rep_SoPhauThuat_A4();
                            rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                            rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                            rep.Nam.Value = _BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count()).ToString();
                            rep.Nu.Value = _BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count()).ToString();
                            rep.BHYT.Value = _BN.Where(p => p.dtuong == "BHYT").Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.dtuong == "BHYT").Select(p => p.mabnhan).Count()).ToString();
                            rep.BindingData();
                            rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        if (radMau.SelectedIndex == 1)
                        {
                            BaoCao.Rep_SoPhauThuat rep = new BaoCao.Rep_SoPhauThuat();
                            rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                            rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                            rep.Nam.Value = _BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count()).ToString();
                            rep.Nu.Value = _BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count()).ToString();
                            rep.BHYT.Value = _BN.Where(p => p.dtuong == "BHYT").Select(p => p.mabnhan).Count() == 0 ? null : (_BN.Where(p => p.dtuong == "BHYT").Select(p => p.mabnhan).Count()).ToString();
                            rep.BindingData();
                            rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else MessageBox.Show("Không có dữ liệu để in báo cáo ");
                }
                
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
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