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
    public partial class Frm_NXTThuoc_TDuong_TH : DevExpress.XtraEditors.XtraForm
    {
        public Frm_NXTThuoc_TDuong_TH()
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
                MessageBox.Show("Bạn chưa chọn kho để tổng hợp BC");
                lupKho.Focus();
                return false;
            }
            if (lupPL.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn phân loại dược");
                lupPL.Focus();
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

        private class NXT
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
            private string TenNhomDV;

            public string tennhomdv
            {
                get { return TenNhomDV; }
                set { TenNhomDV = value; }
            }
            private string TieuNhomDV;

            public string tieunhomdv
            {
                get { return TieuNhomDV; }
                set { TieuNhomDV = value; }
            }
            private string NuocSX;

            public string nuocsx
            {
                get { return NuocSX; }
                set { NuocSX = value; }
            }
            private string DVT;

            public string dvt
            {
                get { return DVT; }
                set { DVT = value; }
            }
            private double DonGia;

            public double dongia
            {
                get { return DonGia; }
                set { DonGia = value; }
            }
            private int TonDKSL;

            public int tondksl
            {
                get { return TonDKSL; }
                set { TonDKSL = value; }
            }
            private double TonDKTT;

            public double tondktt
            {
                get { return TonDKTT; }
                set { TonDKTT = value; }
            }
            private int NhapTKSL;

            public int nhaptksl
            {
                get { return NhapTKSL; }
                set { NhapTKSL = value; }
            }
            private double NhapTKTT;

            public double nhaptktt
            {
                get { return NhapTKTT; }
                set { NhapTKTT = value; }
            }
            private int NhapTKSLTong;

            public int nhaptksltong
            {
                get { return NhapTKSLTong; }
                set { NhapTKSLTong = value; }
            }
            private double NhapTKTTTong;

            public double nhaptktttong
            {
                get { return NhapTKTTTong; }
                set { NhapTKTTTong = value; }
            }
            private int XuatBHYTSL;

            public int xuatbhytsl
            {
                get { return XuatBHYTSL; }
                set { XuatBHYTSL = value; }
            }
            private double XuatBHYTTT;

            public double xuatbhyttt
            {
                get { return XuatBHYTTT; }
                set { XuatBHYTTT = value; }
            }
            private int Xuat139SL;

            public int xuat139sl
            {
                get { return Xuat139SL; }
                set { Xuat139SL = value; }
            }
            private double Xuat139TT;

            public double xuat139tt
            {
                get { return Xuat139TT; }
                set { Xuat139TT = value; }
            }
            private int XuatTESL;

            public int xuattesl
            {
                get { return XuatTESL; }
                set { XuatTESL = value; }
            }
            private double XuatTETT;

            public double xuattett
            {
                get { return XuatTETT; }
                set { XuatTETT = value; }
            }
            private int XuatNBSL;

            public int xuatnbsl
            {
                get { return XuatNBSL; }
                set { XuatNBSL = value; }
            }
            private double XuatNBTT;

            public double xuatnbtt
            {
                get { return XuatNBTT; }
                set { XuatNBTT = value; }
            }
            private int TXTKSL;

            public int txtksl
            {
                get { return TXTKSL; }
                set { TXTKSL = value; }
            }
            private double TXTKTT;

            public double txtktt
            {
                get { return TXTKTT; }
                set { TXTKTT = value; }
            }
            private int TonCKSL;

            public int toncksl
            {
                get { return TonCKSL; }
                set { TonCKSL = value; }
            }
            private double TonCKTT;

            public double toncktt
            {
                get { return TonCKTT; }
                set { TonCKTT = value; }
            }
        }
        List<NXT> _lnxt = new List<NXT>();
        private class BC
        {
            private string _TenNhomCT, _TenTN, _NuocSX, _TenDV, _DonVi;
            private double _DonGia;
            private int _dk, _noitru, _kieudon, _MaBNhan, _MaDV;
            private double _SoLuongN, _SoLuongX, _TTNhap, _TTXuat;
            private DateTime _NgayNhap;
            public double TTNhap
            {
                set { _TTNhap = value; }
                get { return _TTNhap; }
            }
            public double TTXuat
            {
                set { _TTXuat = value; }
                get { return _TTXuat; }
            }
            public int KieuDon
            {
                set { _kieudon = value; }
                get { return _kieudon; }
            }
            public int NoiTru
            { set { _noitru = value; }
                get { return _noitru; }
            }
            public int DK
            {
                set { _dk = value; }
                get { return _dk; }
            }
            public string TenNhomCT
            {
                set { _TenNhomCT = value; }
                get { return _TenNhomCT; }
            }
            public string TenTN
            {
                set { _TenTN = value; }
                get { return _TenTN; }
            }
            public int MaDV
            {
                set { _MaDV = value; }
                get { return _MaDV; }
            }
            public string NuocSX
            {
                set { _NuocSX = value; }
                get { return _NuocSX; }
            }
            public string TenDV
            {
                set { _TenDV = value; }
                get { return _TenDV; }
            }
            public string DonVi
            {
                set { _DonVi = value; }
                get { return _DonVi; }
            }
            public double DonGia
            {
                set { _DonGia = value; }
                get { return _DonGia; }
            }
            public int MaBNhan
            {
                set { _MaBNhan = value; }
                get { return _MaBNhan; }
            }
            public double SLXuat
            {
                set { _SoLuongX = value; }
                get { return _SoLuongX; }
            }
            public double SLNhap
            {
                set { _SoLuongN = value; }
                get { return _SoLuongN; }
            }
            public DateTime NgayNhap
            {
                set { _NgayNhap = value; }
                get { return _NgayNhap; }
            }
        }
        List<BC> _BC = new List<BC>();
        private void Frm_BcSuDungThuoc_TDuong_Load(object sender, EventArgs e)
        {

            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;

            var q = (from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP }).ToList();
            lupKho.Properties.DataSource = q.ToList();

            List<NhomDV> _Ploai = new List<NhomDV>();
            _Ploai = (from pl in data.NhomDVs.Where(p => p.Status == 1) select pl).ToList();
            _Ploai.Add(new NhomDV { TenNhomCT = " ", Status = 1 }); //
            lupPL.Properties.DataSource = _Ploai.OrderBy(p => p.TenNhomCT).ToList();

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
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
                _BC.Clear();
                frmIn frm = new frmIn();
                BaoCao.RepBcNXT_DTuongTH rep = new BaoCao.RepBcNXT_DTuongTH();

                //rep.TuNgay.Value = dateTuNgay.Text;
                //rep.TuNgayDenNgay.Value = dateDenNgay.Text;
                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                if (chkThang.Checked == true) { rep.TuNgayDenNgay.Value = "Tháng " + denngay.Month + " năm " + denngay.Year; }
                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = Convert.ToInt32( lupKho.EditValue);
                var kho = data.KPhongs.Where(p => p.MaKP == _kho).Select(p => new { p.TenKP }).ToList();
                rep.Kho.Value =kho.First().TenKP;
                string _pl = "";
                if (lupPL.Text != null)
                {
                    _pl = lupPL.EditValue.ToString();
                }
                //rep.MaKP.Value = _kho;
                var qnhap = (from nhapd in data.NhapDs.Where(p => (p.PLoai == 1 || p.PLoai == 2) && (p.MaKP == _kho))//.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                             join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                             join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                             join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                             join nhomdv in data.NhomDVs.Where(p => p.Status == 1) on tieunhomdv.IDNhom equals nhomdv.IDNhom
                             select new { nhomdv.TenNhomCT, tieunhomdv.TenTN, dv.MaDV, dv.NuocSX, dv.TenDV, dv.DonVi, nhapdct.DonGia, nhapdct.MaBNhan, nhapdct.SoLuongN, nhapdct.SoLuongX,nhapdct.ThanhTienN, nhapdct.ThanhTienX, nhapd.NgayNhap, nhapd.KieuDon }).ToList();
                
                if (qnhap.Count > 0)
                {
                    foreach (var a in qnhap)
                    {
                        BC themmoi = new BC();
                        themmoi.DonGia = a.DonGia;
                        themmoi.DonVi = a.DonVi;
                        themmoi.MaBNhan = a.MaBNhan== null? 0: a.MaBNhan.Value;
                    
                        themmoi.MaDV = a.MaDV;
                        themmoi.NuocSX = a.NuocSX;
                        themmoi.TTNhap=a.ThanhTienN;
                        themmoi.TTXuat=a.ThanhTienX;
                        themmoi.NgayNhap = a.NgayNhap.Value;
                        if (a.SoLuongN != null)
                        {
                            themmoi.SLNhap = a.SoLuongN;
                        }
                        else
                        { themmoi.SLNhap = 0; }
                        if (a.SoLuongX != null)
                        {
                            themmoi.SLXuat = a.SoLuongX;
                        }
                        else
                        { themmoi.SLXuat = 0; }
                        themmoi.TenDV = a.TenDV;
                        themmoi.TenNhomCT = a.TenNhomCT;
                        themmoi.TenTN = a.TenTN;
                        if (a.KieuDon != null)
                        {
                            themmoi.KieuDon = a.KieuDon.Value;
                        }
                        else
                        { themmoi.KieuDon = -1; }
                        _BC.Add(themmoi);
                    }
                }
                foreach (var b in _BC)
                {
                    if (b.MaBNhan != null)
                    {
                        int mbn= b.MaBNhan;
                        var q = data.BenhNhans.Where(p => p.MaBNhan == mbn).ToList();
                        if (q.First().DTuong == "BHYT")
                        {
                            b.DK = 1;// điều kiện là 1 là xuất BHYT
                        }
                        else
                        {
                            b.DK = 2;// điều kiện 2 là xuất nhân dân
                        }
                    }
                }
                int _a1 = -1, _a2 = -1;
                if (radBN.SelectedIndex == 0)
                {
                    _a1 = 1;
                    _a2 = -1;
                }
                if (radBN.SelectedIndex == 1)
                {
                    _a1 = 0;
                    _a2 = -1;
                }
                if (radBN.SelectedIndex == 2)
                {
                    _a1 = 0;
                    _a2 = 1;
                }

                var q1 = (from c in _BC
                          group new { c } by new { c.DonGia, c.DonVi, c.MaDV, c.NuocSX, c.NgayNhap, c.TenDV, c.TenNhomCT, c.TenTN } into kq
                          select new
                          {
                              TenNhomDV = kq.Key.TenNhomCT,
                              TieuNhomDV = kq.Key.TenTN,
                              MaDV = kq.Key.MaDV,
                              TenDV = kq.Key.TenDV,
                              NuocSX = kq.Key.NuocSX,
                              DVT = kq.Key.DonVi,
                              DonGia = kq.Key.DonGia,

                              TonDKSL = kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLXuat),
                              TonDKTT = kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTXuat),

                              NhapTKSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap),
                              NhapTKTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap),

                              TNhapTKSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap) + kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLXuat),
                              TNhapTKTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap) + kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTXuat),

                              XuatBHSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK == 1).Where(p=>p.c.NoiTru==_a1||p.c.NoiTru==_a2).Sum(p => p.c.SLXuat),
                              XuatBHTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK == 1).Where(p => p.c.NoiTru == _a1 || p.c.NoiTru == _a2).Sum(p => p.c.TTXuat),

                              Xuat139SL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK == 3).Where(p => p.c.NoiTru == _a1 || p.c.NoiTru == _a2).Sum(p => p.c.SLXuat),
                              Xuat139TT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK == 3).Where(p => p.c.NoiTru == _a1 || p.c.NoiTru == _a2).Sum(p => p.c.TTXuat),

                              XuatTSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLXuat),
                              XuatTTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTXuat),

                              TonCKSL = (kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLXuat)),
                              TonCKTT = (kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTXuat)) ,
                          }).ToList();
                rep.DataSource = q1.Where(p => p.TenNhomDV.Contains(_pl)).OrderBy(p => p.TenDV).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }


    }
}