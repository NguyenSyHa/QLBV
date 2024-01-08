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
    public partial class Frm_BcNXT_TH : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcNXT_TH()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
   
        private bool KTtaoBcNXT()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho");
                lupKho.Focus();
                return false;
            }
            if (lupNhom.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn nhóm");
                lupNhom.Focus();
                return false;
            }
            return true;
        }
        private class Nhom
        {
            private string tennhomct;

            public string TenNhomCT
            {
                get { return tennhomct; }
                set { tennhomct = value; }
            }
        }
        private class NXT
        {
            private string tennhomct;

            public string TenNhomCT
            {
                get { return tennhomct; }
                set { tennhomct = value; }
            }
            private string tentn;

            public string TenTN
            {
                get { return tentn; }
                set { tentn = value; }
            }
            private int madv;

            public int MaDV
            {
                get { return madv; }
                set { madv = value; }
            }
            private string tendv;

            public string TenDV
            {
                get { return tendv; }
                set { tendv = value; }
            }
            private string dvt;

            public string DVT
            {
                get { return dvt; }
                set { dvt = value; }
            }
            private double dongia;

            public double DonGia
            {
                get { return dongia; }
                set { dongia = value; }
            }
            private double slx;

            public double Slx
            {
                get { return slx; }
                set { slx = value; }
            }
            private double ttx;

            public double Ttx
            {
                get { return ttx; }
                set { ttx = value; }
            }
            private double sl1;

            public double Sl1
            {
                get { return sl1; }
                set { sl1 = value; }
            }
            private double tt1;

            public double Tt1
            {
                get { return tt1; }
                set { tt1 = value; }
            }
            private double sl2;

            public double Sl2
            {
                get { return sl2; }
                set { sl2 = value; }
            }
            private double tt2;

            public double Tt2
            {
                get { return tt2; }
                set { tt2 = value; }
            }
            private double sl3;

            public double Sl3
            {
                get { return sl3; }
                set { sl3 = value; }
            }
            private double tt3;

            public double Tt3
            {
                get { return tt3; }
                set { tt3 = value; }
            }
            private double sl4;

            public double Sl4
            {
                get { return sl4; }
                set { sl4 = value; }
            }
            private double tt4;

            public double Tt4
            {
                get { return tt4; }
                set { tt4 = value; }
            }
            private double sl5;

            public double Sl5
            {
                get { return sl5; }
                set { sl5 = value; }
            }
            private double tt5;

            public double Tt5
            {
                get { return tt5; }
                set { tt5 = value; }
            }
            private double sl6;

            public double Sl6
            {
                get { return sl6; }
                set { sl6 = value; }
            }
            private double tt6;

            public double Tt6
            {
                get { return tt6; }
                set { tt6 = value; }
            }
            private double sl7;

            public double Sl7
            {
                get { return sl7; }
                set { sl7 = value; }
            }
            private double tt7;

            public double Tt7
            {
                get { return tt7; }
                set { tt7 = value; }
            }
            //private int id;

            //public int Id
            //{
            //    get { return id; }
            //    set { id = value; }
            //}
            
        }
        List<Nhom> _lnh = new List<Nhom>();
        List<NXT> _lnxt1 = new List<NXT>();
        List<NXT> _lnxt = new List<NXT>();
        private void Frm_BcNXTTuTruc_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            lupKho.Properties.DataSource = (from TK in data.KPhongs.Where(p => p.PLoai.Contains("Khoa dược")) select new { TK.MaKP, TK.TenKP }).ToList();
            var nh= data.NhomDVs.Where(p => p.Status == 1).Select(p => new { p.TenNhomCT }).ToList();
            if (nh.Count > 0)
            {
                Nhom them = new Nhom();
                them.TenNhomCT = "Tất cả";
                _lnh.Add(them);
               
                foreach (var a in nh)
                {
                    Nhom them1 = new Nhom();
                    them1.TenNhomCT = a.TenNhomCT;
                    _lnh.Add(them1);
                }
            }
            
            lupNhom.Properties.DataSource = _lnh.ToList();
           
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBcNXT())
            {
                _lnxt.Clear();
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                 frmIn frm = new frmIn();
                BaoCao.Rep_BcNXT_TH rep = new BaoCao.Rep_BcNXT_TH();

                //rep.TuNgay.Value = dateTuNgay.Text;
                //rep.DenNgay.Value = dateDenNgay.Text;
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + "  đến ngày " + dateDenNgay.Text;
                int kho = 0;
                if (lupKho.EditValue != null)
                    kho = Convert.ToInt32( lupKho.EditValue);
             
                int _makp = 0;
                if (lupKho.EditValue != null)
                    _makp =Convert.ToInt32(  lupKho.EditValue);

                var qtenkp = data.KPhongs.Where(p => p.MaKP == _makp).Select(p => new { p.TenKP }).ToList();
                if (qtenkp.Count > 0)
                {
                    rep.TenKP.Value = (qtenkp.First().TenKP);
                }
                rep.TieuDe.Value = ("báo cáo nhập - xuất - tồn ").ToUpper();
                if (KoInN.Checked == true) { rep.Nhom.Value = 1; } else { rep.Nhom.Value = 0; }
                if (KoInTN.Checked == true) { rep.TNhom.Value = 1; } else { rep.TNhom.Value = 0; }

          
                string _tennh="";
              
                     var q = (from nhapd in data.NhapDs.Where(p => p.MaKP == kho).Where(p => p.PLoai == 1 || p.PLoai == 2)//.Where(p=>p.NgayNhap>=tungay &&p.NgayNhap<=denngay)
                              join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                              join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                              join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                              join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                              select new { nhomdv.TenNhomCT, tieunhomdv.TenTN,dv.MaDV, dv.TenDV, nhapdct.DonVi,nhapd.NgayNhap,nhapd.PLoai,nhapd.KieuDon,nhapd.MaCC, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.ThanhTienN, nhapdct.SoLuongX, nhapdct.ThanhTienX }
                         ).ToList();
                     var qnxt = (from p in q
                                 group new { p } by new { p.TenNhomCT, p.TenTN, p.MaDV, p.TenDV, p.DonVi, p.DonGia } into kq
                                 select new
                                 {
                                     kq.Key.TenNhomCT,
                                     kq.Key.TenTN,
                                     MaDV = kq.Key.MaDV,
                                     TenDV = kq.Key.TenDV,
                                     DVT = kq.Key.DonVi,
                                     DonGia = kq.Key.DonGia,

                                     TonDKSL = kq.Where(p => p.p.NgayNhap < tungay).Sum(p => p.p.SoLuongN) - kq.Where(p => p.p.NgayNhap < tungay).Sum(p => p.p.SoLuongX),// == null ? 0 : kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX),
                                     TonDKTT = kq.Where(p => p.p.NgayNhap < tungay).Sum(p => p.p.ThanhTienN) - kq.Where(p => p.p.NgayNhap < tungay).Sum(p => p.p.ThanhTienX),// == null ? 0 : kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX),

                                     NhapTKSL = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.SoLuongN),//== null ? 0 :kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                                     NhapTKTT = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.ThanhTienN),//== null ? 0 :kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                                     XuatTKSL = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Where(p => p.p.KieuDon != 2).Sum(p => p.p.SoLuongX),// == null ? 0 : kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapd.KieuDon != 2).Sum(p => p.nhapdct.SoLuongX),
                                     XuatTKTT = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Where(p => p.p.KieuDon != 2).Sum(p => p.p.ThanhTienX),// == null ? 0 : kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapd.KieuDon != 2).Sum(p => p.nhapdct.ThanhTienX),

                                     XuatTKSLCK = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Where(p => p.p.KieuDon == 2).Sum(p => p.p.SoLuongX),// == null ? 0 : kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapd.KieuDon == 2).Sum(p => p.nhapdct.SoLuongX),
                                     XuatTKTTCK = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Where(p => p.p.KieuDon == 2).Sum(p => p.p.ThanhTienX),// == null ? 0 : kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapd.KieuDon == 2).Sum(p => p.nhapdct.ThanhTienX),

                                     XuatTKSLTong = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.SoLuongX),// == null ? 0 : kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                     XuatTKTTTong = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.ThanhTienX),//== null ? 0 :  kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                                     TonCKSL = kq.Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.SoLuongN) - kq.Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.SoLuongX),// == null ? 0 :kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN)- kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                                     TonCKTT = kq.Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.ThanhTienN) - kq.Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.ThanhTienX),// == null ? 0 :kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN)- kq.Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                                 }).ToList();
                      if (qnxt.Count > 0)
                      {
                          foreach (var a in qnxt)
                          {
                              NXT them = new NXT();
                              them.TenNhomCT = a.TenNhomCT;
                              them.TenTN = a.TenTN;
                              them.TenDV = a.TenDV;
                              them.MaDV = a.MaDV;
                              them.DVT = a.DVT;
                              them.DonGia =Convert.ToDouble(a.DonGia);
                              them.Sl1 = Convert.ToDouble(a.TonDKSL);
                              them.Tt1 = Convert.ToDouble(a.TonDKTT);
                              them.Slx = Convert.ToDouble(a.NhapTKSL);
                              them.Ttx = Convert.ToDouble(a.NhapTKTT);
                              them.Sl3 = Convert.ToDouble(a.XuatTKSL);
                              them.Tt3 = Convert.ToDouble(a.XuatTKTT);
                              them.Sl4 = Convert.ToDouble(a.XuatTKSLCK);
                              them.Tt4 = Convert.ToDouble(a.XuatTKTTCK);
                              them.Sl5 = Convert.ToDouble(a.XuatTKSLTong);
                              them.Tt5 = Convert.ToDouble(a.XuatTKTTTong);
                              them.Sl6 = Convert.ToDouble(a.TonCKSL);
                              them.Tt6 = Convert.ToDouble(a.TonCKTT);
                              _lnxt.Add(them);
                          }
                      }
                      var qnxt1 = (from p in q.Where(p=>p.PLoai==1).Where(p=>p.NgayNhap>=tungay&&p.NgayNhap<=denngay)
                                  join kp in data.KPhongs.Where(p => p.PLoai == "Khoa dược") on p.MaCC equals kp.MaKP.ToString()
                                  group new { p,kp } by new { p.TenNhomCT, p.TenTN, p.MaDV, p.TenDV, p.DonVi, p.DonGia } into kq
                                  select new
                                  {
                                      kq.Key.TenNhomCT,
                                      kq.Key.TenTN,
                                      MaDV = kq.Key.MaDV,
                                      TenDV = kq.Key.TenDV,
                                      DVT = kq.Key.DonVi,
                                      DonGia = kq.Key.DonGia,

                                      NhapTKSL = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.SoLuongN),//== null ? 0 :kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                                      NhapTKTT = kq.Where(p => p.p.NgayNhap >= tungay).Where(p => p.p.NgayNhap <= denngay).Sum(p => p.p.ThanhTienN),//== null ? 0 :kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                                   
                                  }).ToList();
                      if (qnxt1.Count > 0)
                      {
                          foreach (var a in _lnxt)
                          {
                              foreach (var b in qnxt1)
                              {
                                  if (b.MaDV == a.MaDV && b.DVT == a.DVT && b.DonGia == a.DonGia)
                                  {
                                      a.Sl2 = Convert.ToDouble(b.NhapTKSL);
                                      a.Tt2 =Convert.ToDouble(b.NhapTKTT);
                                  }
                              }
                          }
                      }
                     var nxt=_lnxt.Select(p => new
                      {
                          p.TenNhomCT,p.TenTN,
                          p.MaDV,
                          p.TenDV,
                          p.DVT,
                          p.DonGia,
                          p.Sl1,
                          p.Tt1,
                          TonDKSL = p.Sl1,//==0?null:p.Sl1.ToString(),
                          TonDKTT = p.Tt1,//==0?null:p.Tt1.ToString(),
                          NhapTKSL = (p.Slx - p.Sl2),//==0?null:(p.Slx - p.Sl2).ToString(),
                          NhapTKTT = (p.Ttx - p.Tt2),// == 0 ? null : (p.Ttx - p.Tt2).ToString(),
                          NhapTKckSL = p.Sl2,//==0?null:p.Sl2.ToString(),
                          NhapTKckTT = p.Tt2,//==0?null:p.Tt2.ToString(),
                          XuatTKSL=p.Sl3,//==0?null:p.Sl3.ToString(),
                          XuatTKTT=p.Tt3,//==0?null:p.Tt3.ToString(),
                          XuatTKSLCK = p.Sl4,// == 0 ? null : p.Sl4.ToString(),
                          XuatTKTTCK = p.Tt4,// == 0 ? null : p.Tt4.ToString(),
                          XuatTKSLTong = p.Sl5,// == 0 ? null : p.Sl5.ToString(),
                          XuatTKTTTong = p.Tt5,// == 0 ? null : p.Tt5.ToString(),
                          TonCKSL = p.Sl6,// == 0 ? null : p.Sl6.ToString(),
                          TonCKTT = p.Tt6 ,//== 0 ? null : p.Tt6.ToString(),
                      }).ToList();
                     if (lupNhom.EditValue != "Tất cả")
                     {
                         _tennh = lupNhom.EditValue.ToString();
                         rep.DataSource = nxt.Where(p=>p.TenNhomCT==_tennh).OrderBy(p => p.TenDV).Where(p=>p.TonDKSL>0||p.NhapTKSL>0||p.NhapTKckSL>0||p.XuatTKSL>0||p.XuatTKSLCK>0||p.XuatTKSLTong>0||p.TonCKSL>0);
                     }
                    else 
                     {
                         rep.DataSource = nxt.OrderBy(p => p.TenDV).Where(p => p.TonDKSL > 0 || p.NhapTKSL > 0 || p.NhapTKckSL > 0 || p.XuatTKSL > 0 || p.XuatTKSLCK > 0 || p.XuatTKSLTong > 0 || p.TonCKSL > 0);
                     }
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