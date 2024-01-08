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
    public partial class Frm_ThVienPhi_BG05 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_ThVienPhi_BG05()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private bool KTtaoBc()
        {
            if (LupNgayTu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                LupNgayTu.Focus();
                return false;
            }
            if (LupNgayDen.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                LupNgayDen.Focus();
                return false;
            }
           
            return true;
        }
        private void Frm_ThVienPhi_BG05_Load(object sender, EventArgs e)
        {
            LupNgayTu.Focus();
            LupNgayTu.DateTime = System.DateTime.Now;
            LupNgayDen.DateTime = System.DateTime.Now;
        }
        private class DichVu
        {
            private int MaKP;
            private string TenKP;
            private string TenRG;
            private string TenRGTN;
            private int SoBN;
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
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string tenkp
            {
                set { TenKP = value; }
                get { return TenKP; }
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
            public int sobn
            { set { SoBN = value; } get { return SoBN; } }
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
        class TenDV
        {
            private string TenRG;
             public string tendv
            { set { TenRG = value; } get { return TenRG; } }
        }
        List<TenDV> _TenDV = new List<TenDV>();
        List<DichVu> _DichVu = new List<DichVu>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            //DateTime ngaytu = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBc())
            {

                _DichVu.Clear();
                _TenDV.Clear();

                DateTime ngaytu = DungChung.Ham.NgayTu(LupNgayTu.DateTime);
                DateTime ngayden = DungChung.Ham.NgayDen(LupNgayDen.DateTime);
                //int _nt = -1; int _ngt = -1;
                //if (radBN.SelectedIndex == 0) { _nt = 1; _ngt = 0; }
                //if (radBN.SelectedIndex == 1) { _nt = 1; _ngt = -1; }
                //if (radBN.SelectedIndex == 2) { _nt = -1; _ngt = 0; }

                List<DichVu> _BN = new List<DichVu>();
                      
                var q = (from b in dataContext.BenhNhans.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == 1)
                        join vp in dataContext.VienPhis.Where(p => p.NgayTT >= ngaytu && p.NgayTT <= ngayden) on b.MaBNhan equals vp.MaBNhan
                        join vpct in dataContext.VienPhicts on vp.idVPhi equals vpct.idVPhi
                        join dv in dataContext.DichVus on vpct.MaDV equals dv.MaDV
                         join tu in dataContext.TamUngs.Where(p => p.PhanLoai == 1||p.PhanLoai==2) on b.MaBNhan equals tu.MaBNhan
                        join tnhom in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                        join nhomdv in dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                         select new { nhomdv.TenNhomCT, dv.TenRG, b.MaBNhan, b.MaKP, vpct.ThanhTien, dv.MaDV,TenRGTN=tnhom.TenRG, })
                         .OrderBy(p => p.TenNhomCT == "Thủ thuật, phẫu thuật").ThenBy(p => p.TenNhomCT == "Giường điều trị nội trú").ThenBy(p => p.TenNhomCT == "Xét nghiệm").ThenBy(p => p.TenNhomCT == "Chẩn đoán hình ảnh").ToList();
                var qbn = (from a in q
                           group new { a } by new { a.MaKP, a.TenRG,a.TenRGTN,a.TenNhomCT } into kq
                           select new {kq.Key.TenNhomCT, kq.Key.MaKP, kq.Key.TenRG,kq.Key.TenRGTN, ThanhTien = kq.Sum(p => p.a.ThanhTien) }).ToList();
                if (qbn.Count > 0)
                {
                    foreach (var a in qbn)
                    {
                        DichVu them = new DichVu();
                        them.makp = a.MaKP == null?0: a.MaKP.Value;
                        them.tenrg = a.TenRG;
                        them.tenrgtn=a.TenRGTN;
                        them.tongtien = Convert.ToDouble(a.ThanhTien);
                        _BN.Add(them);

                    }
                }
             //   _BN = _BN.Where(p => p.makp == "KN2" && p.tenrg == "SA").ToList();
                //var qtendv = (from a in q
                //              group new { a } by new { a.TenRG } into kq
                //              select new
                //              {
                //                  TenDV = kq.Key.TenRG,
                //              }).ToList();
                //if (qtendv.Count > 0)
                //{
                //    foreach (var a in qtendv)
                //    {
                //        TenDV themmoi = new TenDV();
                //        themmoi.tendv = a.TenDV;
                //        _TenDV.Add(themmoi);
                //    }
                //}
                //_TenDV.ToList();
          
              
                var MaKP = (from a in q
                            join kp in dataContext.KPhongs on a.MaKP equals kp.MaKP
                            group new { a } by new { a.MaKP,kp.TenKP } into kq
                            select new
                            {
                                MaKP = kq.Key.MaKP,
                                TenKP = kq.Key.TenKP,
                                 TongTien = kq.Sum(p => p.a.ThanhTien)
                            }).ToList();


                if (MaKP.Count > 0)
                {
                    foreach (var c in MaKP)
                    {
                        DichVu themmoi = new DichVu();
                        themmoi.makp = c.MaKP == null ? 0 : c.MaKP.Value;
                        themmoi.tenkp = c.TenKP;
                        //themmoi.sobn = c.SoBN;
                        themmoi.tongtien = Convert.ToDouble(c.TongTien);

                        _DichVu.Add(themmoi);
                    }
                        
                }
                var MaKP1 = (from a in q
                            join kp in dataContext.KPhongs on a.MaKP equals kp.MaKP
                            group new { a } by new { a.MaKP, a.MaBNhan, kp.TenKP } into kq
                            select new
                            {
                                MaKP = kq.Key.MaKP,
                                TenKP = kq.Key.TenKP,
                                MaBN = kq.Key.MaBNhan,
                                TongTien = kq.Sum(p => p.a.ThanhTien)
                            }).ToList();

                if (MaKP1.Count > 0)
                {
                    
                    foreach (var b in _DichVu)
                    {
                        foreach (var c in MaKP1)
                        {
                            if (c.MaKP == b.makp)
                            {
                                b.sobn =b.sobn+ 1;
                            }
                        }
                    }

                }

                var cls = (from b in dataContext.BenhNhans.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NoiTru == 1)
                           join a2 in dataContext.CLS.Where(p => p.NgayThang >= ngaytu && p.NgayThang <= ngayden) on b.MaBNhan equals a2.MaBNhan
                           join cd1 in dataContext.ChiDinhs.Where(p => p.SoPhieu != null) on a2.IdCLS equals cd1.IdCLS
                           join dv in dataContext.DichVus on cd1.MaDV equals dv.MaDV
                           select new { a2.MaBNhan, cd1.MaDV, dv.DonGia, b.MaKP, dv.TenRG }).ToList();

                if (cls.Count > 0)
                {
                    foreach (var b in _BN)
                    {
                        foreach (var c in cls)
                        {
                            int temp = 0;
                            if (c.MaKP == b.makp && c.TenRG == b.tenrg && temp == 0)
                            {
                                b.tongtien = b.tongtien - Convert.ToDouble(c.DonGia);
                                temp = 1;
                            }
                        }
                    }
                }

               // _BN = _BN.Where(p => p.makp == "KN2" && p.tenrg == "SA").ToList();

                if (cls.Count > 0)
                {
                    foreach (var a in _DichVu)
                    {
                        foreach (var c in cls)
                        {
                            if (c.MaKP == a.makp)

                                a.tongtien = Convert.ToDouble(a.tongtien) - Convert.ToDouble(c.DonGia);
                        }
                    }
                }

                    var bnrv = (from a in q
                                join rv in dataContext.RaViens on a.MaBNhan equals rv.MaBNhan
                                group new { a,rv } by new { a.MaKP,a.MaBNhan,rv.SoNgaydt} into kq
                                select new
                                {
                                   // TenRG = kq.Key.TenRG,
                                    MaBN = kq.Key.MaBNhan,
                                    MaKPhong = kq.Key.MaKP,
                                    SoNDT = Convert.ToInt32(kq.Key.SoNgaydt)
                                }).ToList();

                    if (bnrv.Count > 0)
                    {
                        foreach (var a in _DichVu)
                        {
                            {foreach(var b in bnrv)
                                if (b.MaKPhong == a.makp)
                                {
                                    a.sondt =a.sondt+ Convert.ToInt32(b.SoNDT);
                                } 
                            }
                          
                        }
                    }

                    if (_BN.Count > 0)
                    {
                        foreach (var n in _DichVu)
                        {
                            foreach (var m in _BN)
                            {
                                if (n.makp == m.makp)
                                {
                                    if (m.tongtien != null && m.tongtien != 0)
                                    {
                                        if (m.tenrg == "SA")
                                        { n.DV1 = n.DV1 + Convert.ToDouble(m.tongtien); }
                                        if (m.tenrg == "DT"||m.tenrg=="ĐT")
                                        { n.DV2 = n.DV2 + Convert.ToDouble(m.tongtien); }
                                        if (m.tenrg == "XQ")
                                        { n.DV3 = n.DV3 + Convert.ToDouble(m.tongtien); }
                                        if (m.tenrg == "NSDD")
                                        { n.DV4 = n.DV4 + Convert.ToDouble(m.tongtien); }
                                        if ( m.tenrg == "NSTMH")
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
                                        if ((m.tenrgtn == "Thủ thuật"&&m.tenrg!="Tắm bé")&&(m.tenrgtn == "Thủ thuật"&&m.tenrg!="Tắm Bé"))
                                        { n.DV14 = n.DV14 + Convert.ToDouble(m.tongtien); }
                                        if (m.tenrgtn.Contains("tiêu hao"))
                                        { n.DV15 = n.DV14 + Convert.ToDouble(m.tongtien); }
                                        if (m.tenrg.Contains("iường")||m.tenrgtn.Contains("iường"))
                                        { n.DV16 = n.DV16 + Convert.ToDouble(m.tongtien); }
                                        if (m.tenrg.Contains("C.sinh") || (m.tenrg.Contains("Chứng sinh")) || m.tenrg.Contains("ắm bé"))
                                        { n.DV17 = n.DV17 + Convert.ToDouble(m.tongtien); }
                                        //if (m.tenrg != "Máu" && m.tenrg == "PTM" && m.tenrg != "NT" && m.tenrg != "VGB" && m.tenrg != "HIV" && m.tenrg != "SHM" && (m.tenrg != "DT"||m.tenrg != "ĐT") && m.tenrg != "Thuốc" || m.tenrg != "thuốc"
                                        //    && m.tenrg != "Dịch" && m.tenrgtn != "Thủ thuật" && (m.tenrgtn != null && m.tenrgtn.Contains("tiêu hao")) && (m.tenrg != null && (m.tenrg.Contains("iường") || m.tenrg.Contains("iường")))
                                        //    && (m.tenrg != null && (m.tenrg.Contains("C.sinh") || m.tenrg.Contains("C.sinh") || m.tenrg.Contains("Chứng sinh") || m.tenrg.Contains("Chứng sinh") || m.tenrg.Contains("ắm bé"))))
                                        //{ n.DV14 = n.DV14 + Convert.ToDouble(m.tongtien); }
                                    }

                                }
                            }
                        }

                    }
                    
                        BaoCao.Rep_ThVienPhi_BG05 rep = new BaoCao.Rep_ThVienPhi_BG05();
                        frmIn frm = new frmIn();
                       
                        rep.TenBC.Value = ("BẢNG TỔNG HỢP VIỆN PHÍ tháng " + LupNgayDen.Text.Substring(3, 2)).ToUpper();
                        rep.NgayThang.Value = " Từ ngày " + LupNgayTu.Text.Substring(0, 10) + " đến ngày " + LupNgayDen.Text.Substring(0, 10);
                        
                        rep.DataSource = _DichVu.ToList().OrderBy(p=>p.tenkp);
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
               
        
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}