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
    public partial class Frm_BkVienPhiTheoKhoa_BG_04 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BkVienPhiTheoKhoa_BG_04()
        {
            InitializeComponent();
        }
        private bool kt()
        {
            if (LupNgayTu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                LupNgayTu.Focus();
                return false;
            }
            if (LupNgayDen.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                LupNgayDen.Focus();
                return false;
            }
            if (LupKhoaPhong.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng");
                LupKhoaPhong.Focus();
                return false;
            }
            else return true;
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Frm_BkVienPhiTheoKhoa_BG_04_Load(object sender, EventArgs e)
        {
            LupNgayTu.DateTime = System.DateTime.Now;
            LupNgayDen.DateTime = System.DateTime.Now;
            var KP = (from kp1 in dataContext.KPhongs.Where(p => p.PLoai.Contains("Lâm sàng") || p.PLoai.Contains("Phòng khám")) select new { kp1.TenKP, kp1.MaKP }).ToList();
            if (KP.Count > 0)
            {
                LupKhoaPhong.Properties.DataSource = KP.OrderBy(p => p.TenKP).ToList();
            }
        }
        private class DichVu
        {
            private int MaBN;
            private string TenBN;
            private string TenRG;
            private string DiaChi;
            private string NgayVao;
            private string NgayRa;
            private int SoNDT;
            private double TongTien;
            private double dv1;
            private double dv2;
            private double dv3;
            private double dv4;
            private double dv5;
            private double dv6;
            private double dv7;
            private double dv8;
            private double dv9;
            private double dv10;
            private double dv11;
            private double dv12;
            private double dv13;
            private string TenRGTN;
            private double dv14;
            private double dv15;
            private double dv16;
            private double dv17;
            private double dv18;
            private double dv19;
            public double DV15
            { set { dv15 = value; } get { return dv15; } }
            public double DV16
            { set { dv16 = value; } get { return dv16; } }
            public double DV17
            { set { dv17 = value; } get { return dv17; } }
            public double DV18
            { set { dv18 = value; } get { return dv18; } }
            public double DV19
            { set { dv19 = value; } get { return dv19; } }
              public int mabn
            { set { MaBN = value; } get { return MaBN; } }
            public string tenbn
            {
                set { TenBN = value; }
                get { return TenBN; }
            }
            public string tenrg
            {
                set { TenRG = value; }
                get { return TenRG; }
            }
            public string tenrgtn
            {
                set { TenRGTN = value; }
                get { return TenRGTN; }
            }
            public string diachi
            { set { DiaChi = value; } get { return DiaChi; } }
            public string ngayvao
            { set { NgayVao = value; } get { return NgayVao; } }
            public string ngayra
            { set { NgayRa = value; } get { return NgayRa; } }
            public int sondt
            { set { SoNDT = value; } get { return SoNDT; } }
            public double tongtien
            { set { TongTien = value; } get { return TongTien; } }
            public double DV1
            { set { dv1 = value; } get { return dv1; } }
            public double DV2
            { set { dv2 = value; } get { return dv2; } }
            public double DV3
            { set { dv3 = value; } get { return dv3; } }
            public double DV4
            { set { dv4 = value; } get { return dv4; } }
            public double DV5
            { set { dv5 = value; } get { return dv5; } }
            public double DV6
            { set { dv6 = value; } get { return dv6; } }
            public double DV7
            { set { dv7 = value; } get { return dv7; } }
            public double DV8
            { set { dv8 = value; } get { return dv8; } }
            public double DV9
            { set { dv9 = value; } get { return dv9; } }
            public double DV10
            { set { dv10 = value; } get { return dv10; } }
            public double DV11
            { set { dv11 = value; } get { return dv11; } }
            public double DV12
            { set { dv12 = value; } get { return dv12; } }
            public double DV13
            { set { dv13 = value; } get { return dv13; } }
            public double DV14
            { set { dv14 = value; } get { return dv14; } }
        }
            class DV
            {
                private string TenDV;
                private int MaDV;
                public string tendv
                { set { TenDV = value; } get { return TenDV; } }
                public int madv
                {
                    set { MaDV = value; }
                    get { return MaDV; }
                }
            }
            List<DV> _DV = new List<DV>();
            List<DichVu> _DichVu = new List<DichVu>();

            private void btnOK_Click(object sender, EventArgs e)
            {
                if (kt())
                {
                    int _MaKP = 0;
                    _DichVu.Clear();
                    _DV.Clear();
                    if (LupKhoaPhong.Text != null)
                    {
                        _MaKP =Convert.ToInt32( LupKhoaPhong.EditValue);
                    }
                    DateTime ngaytu = DungChung.Ham.NgayTu(LupNgayTu.DateTime);
                    DateTime ngayden = DungChung.Ham.NgayDen(LupNgayDen.DateTime);
                  var q = (from b in dataContext.BenhNhans.Where(p => p.DTuong == "Dịch vụ").Where(p => p.MaKP == _MaKP)
                         join vp in dataContext.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on b.MaBNhan equals vp.MaBNhan
                        join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                           join tu in dataContext.TamUngs.Where(p => p.PhanLoai == 1||p.PhanLoai==2) on b.MaBNhan equals tu.MaBNhan
                           join dv in dataContext.DichVus on vpct.MaDV equals dv.MaDV
                           join tnhom in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                           join nhomdv in dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                           select new { dv.TenRG, b.MaBNhan, b.TenBNhan, b.MaKP, b.DChi, b.NNhap, vpct.ThanhTien, dv.MaDV, TenRGTN = tnhom.TenRG,nhomdv.TenNhomCT }).ToList();
                    //var qtendv = (from a in q
                    //              group new { a } by new { a.TenRG } into kq
                    //              select new
                    //              {
                    //                  TenDV = kq.Key.TenRG,
                    //              }).OrderBy(p => p.TenDV).ToList();
                    //if (qtendv.Count > 0)
                    //{
                    //    foreach (var a in qtendv)
                    //    {
                    //        DV themmoi = new DV();
                    //        themmoi.tendv = a.TenDV;
                    //        _DV.Add(themmoi);
                    //    }
                    //}
                    List<DichVu> _BN = new List<DichVu>();
                    var qbn = (from a in q
                               group new { a } by new { a.MaBNhan, a.TenRG, a.TenRGTN } into kq
                               select new { kq.Key.MaBNhan,kq.Key.TenRGTN, kq.Key.TenRG, ThanhTien = kq.Sum(p => p.a.ThanhTien) }).ToList();
                    if (qbn.Count > 0)
                    {
                        foreach (var a in qbn)
                        {
                            DichVu them = new DichVu();
                            them.mabn = a.MaBNhan;
                            them.tenrg = a.TenRG ;
                            them.tenrgtn = a.TenRGTN;
                            them.tongtien = Convert.ToDouble(a.ThanhTien);
                            _BN.Add(them);

                        }
                    }
                    //if (q.Count > 0)
                    //{
                    //    foreach (var a in q)
                    //    {
                    //        DichVu them = new DichVu();
                    //        them.tenrg = a.TenRG;
                    //        them.mabn = a.MaBNhan;
                    //        them.tenrg = a.TenRG == null ? a.TenRG : a.TenRGTN;
                    //        them.tongtien = Convert.ToDouble(a.ThanhTien);
                    //        _BN.Add(them);

                    //    }
                    //}
                    _BN.ToList();
                    var MaBN = (from a in q
                                join kp in dataContext.KPhongs on a.MaKP equals kp.MaKP
                                group new { a, kp, } by new { kp.MaKP, kp.TenKP, a.TenBNhan, a.MaBNhan, a.DChi, a.NNhap } into kq
                                select new
                                {
                                    MaBN = kq.Key.MaBNhan,
                                    TenBN = kq.Key.TenBNhan,
                                    DiaChi = kq.Key.DChi,
                                    NgayVao = kq.Key.NNhap,
                                    TongTien = kq.Sum(p => p.a.ThanhTien)
                                }).ToList();

                    if (MaBN.Count > 0)
                    {
                        foreach (var c in MaBN)
                        {
                            DichVu themmoi = new DichVu();
                            themmoi.mabn = c.MaBN;
                            themmoi.tenbn = c.TenBN;
                            themmoi.diachi = c.DiaChi;
                            themmoi.ngayvao = c.NgayVao.ToString().Substring(0, 5);
                            themmoi.tongtien = Convert.ToDouble(c.TongTien);

                            _DichVu.Add(themmoi);
                        }
                    }

                    var cls = (from b in dataContext.BenhNhans.Where(p => p.DTuong == "Dịch vụ").Where(p => p.MaKP == _MaKP)
                               join a2 in dataContext.CLS on b.MaBNhan equals a2.MaBNhan
                               join cd1 in dataContext.ChiDinhs.Where(p => p.SoPhieu != null) on a2.IdCLS equals cd1.IdCLS
                               join dv in dataContext.DichVus on cd1.MaDV equals dv.MaDV
                               select new { a2.MaBNhan, cd1.MaDV, dv.DonGia, b.MaKP, dv.TenRG }).ToList();
                    if (cls.Count > 0)
                    {
                        foreach (var b in _BN)
                        {
                            foreach (var c in cls)
                            {
                                if (c.TenRG == b.tenrg && c.MaBNhan == b.mabn)
                                {
                                    b.tongtien = b.tongtien - Convert.ToDouble(c.DonGia);
                                }
                                //int temp = 0;
                                //if (c.MaBNhan == b.mabn && c.TenRG == b.tenrg && temp == 0)
                                //{
                                //    b.tongtien = b.tongtien - Convert.ToDouble(c.DonGia);
                                //    temp = 1;
                                //}
                            }
                        }
                    }
                    _BN.ToList();

                    if (_DichVu.Count > 0)
                    {
                        foreach (var a in _DichVu)
                        {
                            foreach (var c in cls)
                            {
                                if (c.MaBNhan == a.mabn)

                                    a.tongtien = Convert.ToDouble(a.tongtien) - Convert.ToDouble(c.DonGia);
                            }
                        }
                    }
                    var bnrv = (from a in q
                                join rv in dataContext.RaViens on a.MaBNhan equals rv.MaBNhan
                                group new { a, rv } by new { a.MaKP, a.MaBNhan, rv.NgayRa,rv.SoNgaydt } into kq
                                select new
                                {
                                    MaBN = kq.Key.MaBNhan,
                                    NgayRa = kq.Key.NgayRa,
                                    SoNDT = kq.Key.SoNgaydt,
                                 }).ToList();

                    if (bnrv.Count > 0)
                    {
                        foreach (var a in _DichVu)
                        {
                            {
                                foreach (var b in bnrv)
                                {
                                    if (b.MaBN == a.mabn)
                                    {
                                        a.ngayra = b.NgayRa.ToString().Substring(0, 5);
                                        a.sondt = Convert.ToInt32(b.SoNDT);
                                    }
                                }
                            }

                        }
                    }
                    //var bn = (from bn1 in q
                    //          group new { bn1} by new { bn1.TenRG, bn1.TenBNhan, bn1.MaBNhan } into kq
                    //          select new
                    //          {
                    //              MaBN = kq.Key.MaBNhan,
                    //              MaDV = kq.Key.TenRG,
                    //              ThanhTien = kq.Sum(p => p.bn1.SoTien),
                    //              TenBN = kq.Key.TenBNhan,
                    //          }).ToList();
                    _DichVu.ToList();
                    if (_BN.Count > 0)
                    {
                        foreach (var n in _DichVu)
                        {
                            foreach (var m in _BN)
                            {
                                if (n.mabn == m.mabn)
                                {
                                    if (m.tongtien != null && m.tongtien != 0)
                                            {
                                                if (m.tenrg == "SA")
                                                { n.DV1 = n.DV1 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "DT" || m.tenrg == "ĐT")
                                                { n.DV2 = n.DV2 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "XQ")
                                                { n.DV3 = n.DV3 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "NSDD")
                                                { n.DV4 = n.DV4 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "NSTMH")
                                                { n.DV5 = n.DV5 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "Máu")
                                                { n.DV6 = n.DV6 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "PTM")
                                                { n.DV7 = n.DV7 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "NT")
                                                { n.DV8 = n.DV8 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "SHM")
                                                { n.DV9 = n.DV9 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "VGB")
                                                { n.DV10 = n.DV10 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "HIV")
                                                { n.DV11 = n.DV11 + Convert.ToDouble(m.tongtien); }

                                                if (m.tenrg == "Thuốc" || m.tenrg == "thuốc")
                                                { n.DV12 = n.DV12 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg == "Dịch")
                                                { n.DV13 = n.DV13 + Convert.ToDouble(m.tongtien); }
                                                if ((m.tenrgtn == "Thủ thuật" && m.tenrg != "Tắm bé") && (m.tenrgtn == "Thủ thuật" && m.tenrg != "Tắm Bé"))
                                                { n.DV14 = n.DV14 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrgtn!=null && m.tenrgtn.Contains("tiêu hao"))
                                                { n.DV15 = n.DV15 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg.ToLower().Contains("giường"))
                                                {
                                                    
                                                    n.DV16 = n.DV16 + Convert.ToDouble(m.tongtien); }
                                                if (m.tenrg.Contains("C.sinh")||(m.tenrg.Contains("Chứng sinh")) || m.tenrg.Contains("ắm bé"))
                                                { n.DV17 = n.DV17 + Convert.ToDouble(m.tongtien); }
                                            }
                                        }
                                    
                                }
                            
                        }

                    }
                    BaoCao.Rep_BkVienPhiTheoKhoa_BG_04 rep = new BaoCao.Rep_BkVienPhiTheoKhoa_BG_04();
                    frmIn frm = new frmIn();
                  
                    rep.TenBC.Value = ("Bảng kê viện phí tháng " + LupNgayDen.Text.Substring(3, 2)).ToUpper();
                    rep.NgayThang.Value = " Từ ngày " + LupNgayTu.Text.Substring(0, 10) + " đến ngày " + LupNgayDen.Text.Substring(0, 10);
                    rep.KhoaPhong.Value = LupKhoaPhong.Text;
                    rep.DataSource = _DichVu;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }    
    
}