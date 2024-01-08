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
    public partial class Frm_BkChiTietThuVP_TY02 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BkChiTietThuVP_TY02()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
         private bool KTtaoBc()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupNgaytu.Focus();
                return false;
            }
            if (lupNgayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                lupNgayden.Focus();
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
        public class BNPT
        {
            private int MaBNhan;
            private string TenBNhan;
            private string DChi;
            private int NNT;
            private string TenKP;
            private int TienGiuong;
            private int Thuoc;
            private int TTPT;
            private int MauCP;
            private int KSK;
            private int CDHA;
            private int VTYT;
            private int XN;
            private int Khac;
            private int NgoaiBH;
            private int TienBHYT;
            private int tong;
            public int mabnhan
            {
                set { MaBNhan = value; }
                get { return MaBNhan; }
            }
            public string tenbnhan
            {
                set { TenBNhan = value; }
                get { return TenBNhan; }
            }
            public string dchi
            { set { DChi = value; } get { return DChi; } }
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int nnt
            { set { NNT = value; } get { return NNT; } }
            public int tiengiuong
            { set { TienGiuong = value; } get { return TienGiuong; } }
            public int thuoc
            { set { Thuoc = value; } get { return Thuoc; } }
            public int ttpt
            { set { TTPT = value; } get { return TTPT; } }
            public int maucp
            { set { MauCP = value; } get { return MauCP; } }
            public int ksk
            { set { KSK = value; } get { return KSK; } }
            public int cdha
            { set { CDHA = value; } get { return CDHA; } }
            public int vtyt
            { set { VTYT = value; } get { return VTYT; } }
            public int xn
            { set { XN = value; } get { return XN; } }
            public int khac
            { set { Khac = value; } get { return Khac; } }
            public int ngoaibh
            { set { NgoaiBH = value; } get { return NgoaiBH; } }
            public int tienbhyt
            { set { TienBHYT = value; } get { return TienBHYT; } }
            public int Tong
            { set { tong = value; } get { return tong; } }
        }
        List<BNPT> _BNPT = new List<BNPT>();
        List<KPhong> _Kphong = new List<KPhong>();
        private void Frm_BkChiTietThuVP_TY02_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            _BNPT.Clear();
            lupNgaytu.DateTime = System.DateTime.Now;
            lupNgayden.DateTime = System.DateTime.Now;
            var kphong = (from kp in _Data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = false;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        private void bntOK_Click(object sender, EventArgs e)
        {
             DateTime _Ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            DateTime _Ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            int _MaKP1 = 0;
            int _MaKP2 = 0;
            int _MaKP3 = 0;
            int _MaKP4 = 0;
            int _MaKP5 = 0;
            int _MaKP6 = 0;
            int _MaKP7 = 0;
            int _MaKP8 = 0;
            int _MaKP9 = 0;
            int _MaKP10 = 0;
            int _MaKP11 = 0;
            int _MaKP12 = 0;
            int _MaKP13 = 0;
            int _MaKP14 = 0;
            int _MaKP15 = 0;
            for (int i = 0; i < _Kphong.Count; i++)
            {
                if (_Kphong.Skip(i).First().chon == true)
                {
                    switch (i)
                    {
                        case 0:
                            _MaKP1 = _Kphong.Skip(i).First().makp;
                            break;
                        case 1:
                            _MaKP2 = _Kphong.Skip(i).First().makp;
                            break;
                        case 2:
                            _MaKP3 = _Kphong.Skip(i).First().makp;
                            break;
                        case 3:
                            _MaKP4 = _Kphong.Skip(i).First().makp;
                            break;
                        case 4:
                            _MaKP5 = _Kphong.Skip(i).First().makp;
                            break;
                        case 5:
                            _MaKP6 = _Kphong.Skip(i).First().makp;
                            break;
                        case 6:
                            _MaKP7 = _Kphong.Skip(i).First().makp;
                            break;
                        case 7:
                            _MaKP8 = _Kphong.Skip(i).First().makp;
                            break;
                        case 8:
                            _MaKP9 = _Kphong.Skip(i).First().makp;
                            break;
                        case 9:
                            _MaKP10 = _Kphong.Skip(i).First().makp;
                            break;
                        case 10:
                            _MaKP11 = _Kphong.Skip(i).First().makp;
                            break;
                        case 11:
                            _MaKP12 = _Kphong.Skip(i).First().makp;
                            break;
                        case 12:
                            _MaKP13 = _Kphong.Skip(i).First().makp;
                            break;
                        case 13:
                            _MaKP14 = _Kphong.Skip(i).First().makp;
                            break;
                        case 14:
                            _MaKP15 = _Kphong.Skip(i).First().makp;
                            break;
                    }
                }
            }
            
              var qbn= (from bn in _Data.BenhNhans
                        //join tu in _Data.TamUngs.Where(p => p.PhanLoai == 1).Where(p => p.NgayThu >= _Ngaytu).Where(p => p.NgayThu <= _Ngayden) on bn.MaBNhan equals tu.MaBNhan
                        join vp in _Data.VienPhis.Where(P => P.NgayTT >= _Ngaytu).Where(p => p.NgayTT <= _Ngayden) on bn.MaBNhan equals vp.MaBNhan
                      //   join vpct in _Data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                      //   join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                      //   join nhom in _Data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                      ////   join rv in _Data.RaViens on bn.MaBNhan equals rv.MaBNhan
                         join kp in _Data.KPhongs on bn.MaKP equals kp.MaKP
                         where (bn.MaKP == _MaKP1 || bn.MaKP == _MaKP10 || bn.MaKP == _MaKP11 || bn.MaKP == _MaKP12 || bn.MaKP == _MaKP13 || bn.MaKP == _MaKP14 || bn.MaKP == _MaKP15 || bn.MaKP == _MaKP2 || bn.MaKP == _MaKP3 || bn.MaKP == _MaKP4 || bn.MaKP == _MaKP5 || bn.MaKP == _MaKP6 || bn.MaKP == _MaKP7 || bn.MaKP == _MaKP8 || bn.MaKP == _MaKP9)
                         group new { bn,  kp } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi,bn.NoiTru, bn.MaKP, kp.TenKP } into kq
                         select new
                         {
                             Makp = kq.Key.MaKP,
                             Mabn = kq.Key.MaBNhan,
                             TenKP = kq.Key.TenKP,
                             TenBN = kq.Key.TenBNhan,
                             DChi = kq.Key.DChi,
                             NNT=kq.Key.NoiTru,
                          
                           }).ToList();
              if (qbn.Count > 0)
              {
                  foreach (var a in qbn)
                  {
                      BNPT themmoi = new BNPT();
                      themmoi.mabnhan = a.Mabn;
                      themmoi.tenbnhan = a.TenBN;
                      themmoi.dchi = a.DChi;
                      themmoi.tenkp = a.TenKP;
                      themmoi.nnt =Convert.ToInt32(a.NNT);
                      _BNPT.Add(themmoi);
                  }
              }
              var qbnsk = (from bn in _Data.BenhNhans
                           join tu in _Data.TamUngs.Where(p => p.PhanLoai == 1).Where(p => p.NgayThu >= _Ngaytu).Where(p => p.NgayThu <= _Ngayden) on bn.MaBNhan equals tu.MaBNhan
                           join kp in _Data.KPhongs on bn.MaKP equals kp.MaKP
                         where (bn.MaKP == _MaKP1 || bn.MaKP == _MaKP10 || bn.MaKP == _MaKP11 || bn.MaKP == _MaKP12 || bn.MaKP == _MaKP13 || bn.MaKP == _MaKP14 || bn.MaKP == _MaKP15 || bn.MaKP == _MaKP2 || bn.MaKP == _MaKP3 || bn.MaKP == _MaKP4 || bn.MaKP == _MaKP5 || bn.MaKP == _MaKP6 || bn.MaKP == _MaKP7 || bn.MaKP == _MaKP8 || bn.MaKP == _MaKP9)
                         group new { bn, kp } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.NoiTru, bn.MaKP, kp.TenKP } into kq
                         select new
                         {
                             Makp = kq.Key.MaKP,
                             Mabn = kq.Key.MaBNhan,
                             TenKP = kq.Key.TenKP,
                             TenBN = kq.Key.TenBNhan,
                             DChi = kq.Key.DChi,
                             NNT = kq.Key.NoiTru,

                         }).ToList();
              if (qbnsk.Count > 0)
              {
                  foreach (var b in qbnsk)
                  {
                      BNPT themmoi = new BNPT();
                      themmoi.mabnhan = b.Mabn;
                      themmoi.tenbnhan = b.TenBN;
                      themmoi.dchi = b.DChi;
                      themmoi.tenkp = b.TenKP;
                      themmoi.nnt = Convert.ToInt32(b.NNT);
                      _BNPT.Add(themmoi);
                  }
              }
              var qtt = (from bn in _Data.BenhNhans
                       join vp in _Data.VienPhis.Where(P => P.NgayTT >= _Ngaytu).Where(p => p.NgayTT <= _Ngayden) on bn.MaBNhan equals vp.MaBNhan
                       join vpct in _Data.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                       join dv in _Data.DichVus on vpct.MaDV equals dv.MaDV
                       join nhom in _Data.NhomDVs on dv.IDNhom equals nhom.IDNhom
                       join kp in _Data.KPhongs on bn.MaKP equals kp.MaKP
                       where (bn.MaKP == _MaKP1 || bn.MaKP == _MaKP10 || bn.MaKP == _MaKP11 || bn.MaKP == _MaKP12 || bn.MaKP == _MaKP13 || bn.MaKP == _MaKP14 || bn.MaKP == _MaKP15 || bn.MaKP == _MaKP2 || bn.MaKP == _MaKP3 || bn.MaKP == _MaKP4 || bn.MaKP == _MaKP5 || bn.MaKP == _MaKP6 || bn.MaKP == _MaKP7 || bn.MaKP == _MaKP8 || bn.MaKP == _MaKP9)
                       group new { bn, dv, vpct, nhom, vp,kp } by new { vp.MaBNhan, bn.TenBNhan, bn.DChi, bn.NoiTru, bn.MaKP, kp.TenKP } into kq
                       select new
                       {
                           MaBNhan = kq.Key.MaBNhan,
               
                           Giuong = kq.Where(p => p.dv.TrongDM == 1).Where(p => p.nhom.TenNhomCT == "Ngày giường").Sum(p => p.vpct.ThanhTien),
                           Thuoc = kq.Where(p => p.dv.TrongDM == 1).Where(p => p.nhom.TenNhomCT.Contains("Thuốc")).Sum(p => p.vpct.ThanhTien),
                           TTPT = kq.Where(p => p.dv.TrongDM == 1).Where(p => p.nhom.TenNhomCT == "Thủ thuật, phẫu thuật").Sum(p => p.vpct.ThanhTien),
                           Mau = kq.Where(p => p.dv.TrongDM == 1).Where(p => p.nhom.TenNhomCT == "Máu, chế phẩm máu").Sum(p => p.vpct.ThanhTien),
                           CDHA = kq.Where(p => p.dv.TrongDM == 1).Where(p => p.nhom.TenNhomCT == "CĐHA TDCN").Sum(p => p.vpct.ThanhTien),
                           VTYT = kq.Where(p => p.dv.TrongDM == 1).Where(p => p.nhom.TenNhomCT.Contains("Vật tư")).Sum(p => p.vpct.ThanhTien),
                           XN = kq.Where(p => p.dv.TrongDM == 1).Where(p => p.nhom.TenNhomCT == "Xét nghiệm").Sum(p => p.vpct.ThanhTien),
                           Khac = kq.Where(p => p.dv.TrongDM == 1).Where(p => p.nhom.TenNhomCT == "Tiền khám bệnh").Sum(p => p.vpct.ThanhTien),
                           CPNgoaiBH = kq.Where(p => p.dv.TrongDM != 1).Sum(p => p.vpct.ThanhTien),
                           VPBHYT = kq.Sum(p => p.vpct.TienBH),
                           Tong = kq.Sum(p => p.vpct.ThanhTien),
                       }).ToList();
             if (qtt.Count > 0)
                {
                    foreach (var n in _BNPT)
                    {
                        foreach (var m in qtt)
                        {
                            if (n.mabnhan == m.MaBNhan)
                            {
                                if (m.Thuoc != null && m.Thuoc != 0)
                                {
                                    n.thuoc = Convert.ToInt32(m.Thuoc);
                                }
                                 if (m.Giuong != null && m.Giuong != 0)
                                {
                                    n.tiengiuong = Convert.ToInt32(m.Giuong);
                                }
                                //else { n.tiengiuong = ; }
                                if (m.TTPT != null && m.TTPT != 0)
                                {
                                    n.ttpt = Convert.ToInt32(m.TTPT);
                                }
                                if (m.Mau != null && m.Mau != 0)
                                {
                                    n.maucp = Convert.ToInt32(m.Mau);
                                }
                                if (m.CDHA != null && m.CDHA != 0)
                                {
                                    n.cdha = Convert.ToInt32(m.CDHA);
                                }
                                if (m.VTYT != null && m.VTYT != 0)
                                {
                                    n.vtyt = Convert.ToInt32(m.VTYT);
                                }
                                if (m.XN != null && m.XN != 0)
                                {
                                    n.xn = Convert.ToInt32(m.XN);
                                }
                                if (m.Khac != null && m.Khac != 0)
                                {
                                    n.khac = Convert.ToInt32(m.Khac);
                                }
                                if (m.CPNgoaiBH != null && m.CPNgoaiBH != 0)
                                {
                                    n.ngoaibh = Convert.ToInt32(m.CPNgoaiBH);
                                }
                                if (m.VPBHYT != null && m.VPBHYT != 0)
                                {
                                    n.tienbhyt = Convert.ToInt32(m.VPBHYT);
                                }
                                if (m.Tong != null && m.Tong != 0)
                                {
                                    n.Tong = Convert.ToInt32(m.Tong);
                                }

                            }
                        }
                     } 
                   }
                 var qksk = (from bn in _Data.BenhNhans
                             join tu in _Data.TamUngs.Where(p=>p.PhanLoai==1).Where(p=>p.NgayThu>=_Ngaytu).Where(p=>p.NgayThu<=_Ngayden) on bn.MaBNhan equals tu.MaBNhan
                       where (bn.MaKP == _MaKP1 || bn.MaKP == _MaKP10 || bn.MaKP == _MaKP11 || bn.MaKP == _MaKP12 || bn.MaKP == _MaKP13 || bn.MaKP == _MaKP14 || bn.MaKP == _MaKP15 || bn.MaKP == _MaKP2 || bn.MaKP == _MaKP3 || bn.MaKP == _MaKP4 || bn.MaKP == _MaKP5 || bn.MaKP == _MaKP6 || bn.MaKP == _MaKP7 || bn.MaKP == _MaKP8 || bn.MaKP == _MaKP9)
                       group new { bn, tu } by new { tu.MaBNhan, bn.TenBNhan, bn.DChi, bn.NoiTru, bn.MaKP } into kq
                       select new
                       {
                           MaBNhan = kq.Key.MaBNhan,
               
                           KSK = kq.Sum(p => p.tu.SoTien),
                           
                          
                       }).ToList();
                 if (qksk.Count > 0)
                 {
                     foreach (var n in _BNPT)
                     {
                         foreach (var m in qksk)
                         {
                             if (n.mabnhan == m.MaBNhan)
                             {
                                 if (m.KSK != null && m.KSK != 0)
                                 {
                                     n.ksk = Convert.ToInt32(m.KSK);
                                     n.Tong = Convert.ToInt32(m.KSK);
                                 }
                             }
                         }
                     }
                 }
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BkChiTietThuVP_TY02 rep = new BaoCao.Rep_BkChiTietThuVP_TY02();
                    rep.TuNgayDenNgay.Value = "Từ ngày " + _Ngaytu.ToString().Substring(0, 10) + " Đến ngày " + _Ngayden.ToString().Substring(0, 10);
                    if (cmbPLBN.Text == "Tất cả")
                    {
                        rep.DataSource = _BNPT.ToList();
                        _BNPT.Clear();
                    }
                    if (cmbPLBN.Text == "Nội trú")
                    {
                        rep.DataSource = _BNPT.Where(p => p.nnt == 1).ToList();
                        _BNPT.Clear();
                    }
                    if (cmbPLBN.Text == "Ngoại trú")
                    {
                        
                        rep.DataSource = _BNPT.Where(p => p.nnt == 0).ToList();
                        _BNPT.Clear();
                    }
                    rep.BindingData();
                    rep.CreateDocument();
                    //rep.DataMember = "Table";
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                
            
    }
        

        private void bntHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}