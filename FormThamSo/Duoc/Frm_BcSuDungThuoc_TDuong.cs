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
    public partial class Frm_BcSuDungThuoc_TDuong : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcSuDungThuoc_TDuong()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        #region function KTtaoBcNXT
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
            int cn = 0;
            for (int i = 1; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    cn++;
            }
            if (cn == 0)
            {
                MessageBox.Show("Bạn chưa chọn kho xuất");
                cklKP.Focus();
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
        #endregion
        #region Clss KPhong
        private class KP
        {
            private string TenKP;
            private string maKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public string makp
            { set { maKP = value; } get { return maKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
            public int MaKP { set; get; }
        }
        #endregion

        #region Class NXT
        private class NXT
        {
            private string MaDV;

            public string madv
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
        #endregion
        List<NXT> _lnxt = new List<NXT>();
        #region Class BC
        private class BC
        {
            private string _TenNhomCT, _TenTN, _NuocSX, _TenDV, _DonVi, _MaTam;
            private double _DonGia;
            private int _noitru, _kieudon, _MaBNhan, _MaDV, _phanloai;
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
            public int PhanLoai
            {
                set { _phanloai = value; }
                get { return _phanloai; }
            }
            public int NoiTru
            {
                set { _noitru = value; }
                get { return _noitru; }
            }
            int xuatNB;

            public int XuatNB
            {
                get { return xuatNB; }
                set { xuatNB = value; }
            }
            int _dk;
            public int DK
            {
                set { _dk = value; }
                get { return _dk; }
            }
            int dK_TE;
            public int DK_TE
            {
                set { dK_TE = value; }
                get { return dK_TE; }
            }
            int dK_139;

            public int DK_139
            {
                get { return dK_139; }
                set { dK_139 = value; }
            }
            public string TenNhomCT
            {
                set { _TenNhomCT = value; }
                get { return _TenNhomCT; }
            }
            public string MaTam
            {
                set { _MaTam = value; }
                get { return _MaTam; }
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

            public int? MaKP { get; set; }

            /// <summary>
            /// 1: thanh toán
            /// 2: Không thanh toán
            /// </summary>
            public int ThanhToan { get; set; }

            public int? SoPL { get; set; }
            public string SoCT { get; set; }
        }
        #endregion
        List<BC> _BC = new List<BC>();
        private void Frm_BcSuDungThuoc_TDuong_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            rdgMau_SelectedIndexChanged(sender, e);
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;

            List<KPhong> qkp = new List<KPhong>();
            qkp = data.KPhongs.Where(p => p.PLoai == ("Khoa dược")).ToList();           
                qkp.Insert(0, new KPhong { TenKP = "Tất cả", MaKP = 0 });
            //lupKho.Properties.DataSource = qkp.ToList();
            cklKP.DataSource = qkp.ToList();
            cklKP.CheckAll();
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
          
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (KTtaoBcNXT())
            {
                string _tenkho = "";//lupKho.Properties.GetDisplayText(lupKho.EditValue);
                _lnxt.Clear();
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                _BC.Clear();
                if (rdgMau.SelectedIndex == 1)
                {
                    #region bv tuyến xã

                   // Mẫu chung: xuất điều chuyển bao gồm tất cả các xuất khác ngoài xuất nội ngoại trú; cột xuất khác chỉ lấy chi phí không thanh toán (trongBH = 2 : Lấy trong bảng đơn thuốc thông qua số chứng từ)
                    // Tách chi phí xuất khác vào cột xuất khác : là tách chi phí xuất khác từ cột xuất điều chuyển cộng thêm sang cột xuất khác

                    List<BC> lNoiTru = new List<BC>();
                    List<BC> lNgoaiTru = new List<BC>();
                    List<BC> lXuatKhac = new List<BC>();
                    frmIn frm = new frmIn();                    
                    List<int> lMaKP = new List<int>();
                    string kho = "";

                    for (int i = 0; i < cklKP.ItemCount; i++)
                    {
                        if (cklKP.GetItemChecked(i))
                        {
                            lMaKP.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
                            kho = kho + "," + cklKP.GetItemText(i);
                        }
                    }
                    _tenkho = kho;
                    string _pl = "";
                    if (lupPL.Text != null)
                    {
                        _pl = lupPL.EditValue.ToString();
                    }
                    List<BC> _BC0 = (from nhapd in data.NhapDs.Where(p => p.NgayNhap <= denngay).Where(p => (p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)).Where(p =>lMaKP.Contains(p.MaKP??-1))//.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                                     join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                     join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                     join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                     join nhomdv in data.NhomDVs on tieunhomdv.IDNhom equals nhomdv.IDNhom
                                     select new BC { TenNhomCT = nhomdv.TenNhomCT,
                                         TenTN = tieunhomdv.TenTN,
                                         MaTam = dv.MaTam,
                                         MaDV = dv.MaDV, 
                                         NuocSX = dv.NuocSX,
                                         TenDV = dv.TenDV, 
                                         DonVi = dv.DonVi,
                                         DonGia = nhapdct.DonGia,
                                         MaBNhan = nhapdct.MaBNhan ?? -1,
                                         SLNhap = nhapdct.SoLuongN, 
                                         SLXuat = nhapdct.SoLuongX,
                                         TTNhap = nhapdct.ThanhTienN,
                                         TTXuat = nhapdct.ThanhTienX,
                                         NgayNhap = nhapd.NgayNhap ?? DateTime.Now,
                                         KieuDon = nhapd.KieuDon ?? -1,
                                         PhanLoai = nhapd.PLoai ?? -1,
                                         MaKP = nhapd.MaKP, 
                                         SoCT = nhapd.SoCT }).ToList();
                    #region tính xuất ngoại trú

                    List<int> _lMaBNNgoaiTru = (from a in _BC0.Where(p => p.NgayNhap >= tungay)
                                                    .Where(p => p.PhanLoai == 2 && p.KieuDon == 0) group a by a.MaBNhan into kq select kq.Key).ToList();
                    var bnNgoaiTru = (from ma in _lMaBNNgoaiTru join b in data.BenhNhans on ma equals b.MaBNhan select new { b.NoiTru, b.MaBNhan, b.DTuong, b.MaDTuong }).ToList();
                    lNgoaiTru = (from bc in _BC0.Where(p => p.PhanLoai == 2 && p.KieuDon == 0)
                                 join bnhan in bnNgoaiTru on bc.MaBNhan equals bnhan.MaBNhan
                                 //    into kq
                                 //from kq1 in kq.DefaultIfEmpty()
                                 select new BC
                                 {
                                     TenNhomCT = bc.TenNhomCT,
                                     TenTN = bc.TenTN,
                                     MaTam = bc.MaTam,
                                     MaDV = bc.MaDV,
                                     NuocSX = bc.NuocSX,
                                     TenDV = bc.TenDV,
                                     DonVi = bc.DonVi,
                                     DonGia = bc.DonGia,
                                     MaBNhan = bc.MaBNhan,
                                     SLXuat = bc.SLXuat,
                                     TTXuat = bc.TTXuat,
                                     NgayNhap = bc.NgayNhap,
                                     KieuDon = 0,
                                     PhanLoai = 2,
                                     MaKP = bc.MaKP,
                                     DK = (bnhan.DTuong == "BHYT") ? 1 : 0,
                                     DK_139 = (bnhan.DTuong == "BHYT" && bnhan.MaDTuong != null && (bnhan.MaDTuong.Trim().ToLower() == "dt" || bnhan.MaDTuong.Trim().ToLower() == "hn" || bnhan.MaDTuong.Trim().ToLower() == "dk")) ? 1 : 0,
                                     DK_TE = (bnhan.DTuong == "BHYT" && bnhan.MaDTuong != null && bnhan.MaDTuong.Trim().ToLower() == "te") ? 1 : 0,
                                     XuatNB = 0,
                                     NoiTru = 0,
                                     SoPL = bc.SoPL,
                                     ThanhToan = 1,

                                 }).ToList();

                    #endregion
                    #region tính xuất nội trú  vaf ddieeuf trij ngoaij trus theo đơn thuốc
                    var lSoPL0 = (from bc in _BC0.Where(p => p.NgayNhap >= tungay).Where(p => p.PhanLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 4) && p.SoCT != null && p.SoCT != "") group bc by new { bc.SoCT } into kq select new { SoCT = kq.Key.SoCT.Split(';') }).ToList();
                    List<string> lSoPL = new List<string>();
                    foreach (var a in lSoPL0)
                    {
                        foreach (var b in a.SoCT)
                            lSoPL.Add(b);
                    }
                    lSoPL = lSoPL.Distinct().ToList();
                    var qdthuocNoiTru0 = (from dt in data.DThuocs.Where(p=>p.PLDV == 1)
                                          join dtct in data.DThuoccts.Where(p =>lMaKP.Contains(p.MaKXuat??-1)).Where(p => p.Status == 1) on dt.IDDon equals dtct.IDDon
                                          select new { dt.MaBNhan, dtct.MaDV, SoPL = dtct.SoPL == null ? -100 : dtct.SoPL, SoLuong = dtct.SoLuong, dtct.DonGia, ThanhTien = dtct.ThanhTien, dtct.TrongBH })
                                          .ToList();
                    var qdthuocNoiTru1 = (from dt in qdthuocNoiTru0
                                          select new { dt.MaBNhan, dt.MaDV, SoPL = dt.SoPL.ToString(), SoLuong = dt.SoLuong, dt.DonGia, ThanhTien = dt.ThanhTien, dt.TrongBH })
                                       .ToList();
                    var qdthuocNoiTru = (from sopl in lSoPL
                                         join dt in qdthuocNoiTru1 on sopl equals dt.SoPL
                                         group new { dt } by new { dt.MaBNhan, dt.MaDV, dt.DonGia, TrongBH = dt.TrongBH == 2 ? 2 : 1 } into kq//TrongBH : 2: không thanh toán, 1: có thanh toán
                                         select new { kq.Key.MaBNhan, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dt.SoLuong), kq.Key.DonGia, ThanhTien = kq.Sum(p => p.dt.ThanhTien), kq.Key.TrongBH }).ToList();

                    var _lMaBNNoiTru = qdthuocNoiTru.Select(p => p.MaBNhan).Distinct().ToList();
                    var bnNoiTru = (from ma in _lMaBNNoiTru join b in data.BenhNhans.Where(p => p.NoiTru == 1 || (p.NoiTru == 0 && p.DTNT == true )) on ma equals b.MaBNhan select new { b.NoiTru, b.MaBNhan, b.DTuong, b.MaDTuong }).ToList();
                    var qdv = (from dv in data.DichVus
                               join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                               join nhomdv in data.NhomDVs.Where(p => p.Status == 1) on tieunhomdv.IDNhom equals nhomdv.IDNhom
                               select new { dv.MaDV, dv.TenDV, dv.TenRG, dv.TenHC, tieunhomdv.TenTN, nhomdv.TenNhomCT, dv.MaTam, NuocSX = dv.NuocSX, DonVi = dv.DonVi }).ToList();

                    lNoiTru = (from bn in bnNoiTru
                               join dt in qdthuocNoiTru on bn.MaBNhan equals dt.MaBNhan
                               join dv in qdv on dt.MaDV equals dv.MaDV
                               select new BC
                               {
                                   TenNhomCT = dv.TenNhomCT,
                                   TenTN = dv.TenTN,
                                   MaTam = dv.MaTam,
                                   MaDV = dv.MaDV,
                                   NuocSX = dv.NuocSX,
                                   TenDV = dv.TenDV,
                                   DonVi = dv.DonVi,
                                   DonGia = dt.DonGia,
                                   MaBNhan = bn.MaBNhan,
                                   NgayNhap = denngay,
                                   KieuDon = bn.NoiTru == 0 ? 4 :  1,
                                   PhanLoai = 2,
                                   DK = bn.DTuong == "BHYT" ? 1 : 0,
                                   DK_139 = (bn.DTuong == "BHYT" && bn.MaDTuong != null && (bn.MaDTuong.Trim().ToLower() == "dt" || bn.MaDTuong.Trim().ToLower() == "hn" || bn.MaDTuong.Trim().ToLower() == "dk")) ? 1 : 0,
                                   DK_TE = (bn != null && bn.DTuong == "BHYT" && bn.MaDTuong != null && bn.MaDTuong.Trim().ToLower() == "te") ? 1 : 0,
                                   ThanhToan = dt.TrongBH,
                                   XuatNB = 0,
                                   NoiTru = bn.NoiTru ?? 2,
                                   SLXuat = dt.SoLuong,
                                   TTXuat = dt.ThanhTien,

                               }).ToList();
                    #endregion
                    #region xuất nhập khác
                    lXuatKhac = (from bc in _BC0
                                 .Where(p => (p.PhanLoai == 1 || p.PhanLoai == 3) ? true : ((p.KieuDon == 0 || p.KieuDon == 1 || p.KieuDon == 4) ? (p.NgayNhap < tungay) : true))
                               
                                 select new BC
                                 {
                                     TenNhomCT = bc.TenNhomCT,
                                     TenTN = bc.TenTN,
                                     MaTam = bc.MaTam,
                                     MaDV = bc.MaDV,
                                     NuocSX = bc.NuocSX,
                                     TenDV = bc.TenDV,
                                     DonVi = bc.DonVi,
                                     DonGia = bc.DonGia,
                                     MaBNhan = bc.MaBNhan,
                                     SLNhap = bc.SLNhap,

                                     TTNhap = bc.TTNhap,
                                     SLXuat = bc.SLXuat,
                                     TTXuat = bc.TTXuat,
                                     NgayNhap = bc.NgayNhap,
                                     KieuDon = bc.KieuDon,
                                     PhanLoai = bc.PhanLoai,
                                     MaKP = bc.MaKP,
                                     SoPL = bc.SoPL,
                                     XuatNB = 1,
                                     ThanhToan = 1
                                 }).ToList();

                    #endregion

                    _BC.AddRange(lNgoaiTru);
                    _BC.AddRange(lNoiTru);
                    _BC.AddRange(lXuatKhac);
                    if (rdgMau.SelectedIndex == 1)// tính xuất khác cho Bv tuyến xã Tam Đường
                    {
                        #region tính cho bệnh nhân ngoại trú
                        var qdt0 = (from dt in data.DThuocs.Where(p=>p.PLDV == 1)//.Where(p => qbnNgoaiTru.Contains(p.MaBNhan ?? 0))
                                    join dtct in data.DThuoccts.Where(p => p.TrongBH == 2) on dt.IDDon equals dtct.IDDon
                                    select new { dt.MaBNhan, dtct.MaDV, dt.MaKP, dt.MaKXuat, dtct.SoPL }).ToList();
                        //if (radBN.SelectedIndex == 0 || radBN.SelectedIndex == 2)
                        //{

                            var qdt = (from bnhan in _lMaBNNgoaiTru
                                       join dt in qdt0 on bnhan equals dt.MaBNhan
                                       select new { dt.MaBNhan, dt.MaDV, dt.MaKP, dt.MaKXuat }).ToList();
                            _BC = (from bc in _BC
                                   join dt in qdt on new { MaBNhan = bc.MaBNhan, MaKP = bc.MaKP, MaDV = bc.MaDV } equals new { MaBNhan = dt.MaBNhan ?? 0, MaKP = dt.MaKXuat, MaDV = dt.MaDV ?? 0 }
                                       into kq
                                   from kq1 in kq.DefaultIfEmpty()
                                   select new BC { TenNhomCT = bc.TenNhomCT, TenTN = bc.TenTN, MaTam = bc.MaTam, MaDV = bc.MaDV, NuocSX = bc.NuocSX, TenDV = bc.TenDV, DonVi = bc.DonVi, DonGia = bc.DonGia, MaBNhan = bc.MaBNhan, SLNhap = bc.SLNhap, SLXuat = bc.SLXuat, TTNhap = bc.TTNhap, TTXuat = bc.TTXuat, NgayNhap = bc.NgayNhap, KieuDon = bc.KieuDon, PhanLoai = bc.PhanLoai, MaKP = bc.MaKP, XuatNB = bc.XuatNB, NoiTru = bc.NoiTru, DK = bc.DK, DK_139 = bc.DK_139, DK_TE = bc.DK_TE, ThanhToan = kq1 == null ? bc.ThanhToan : 2, SoPL = bc.SoPL }).ToList();
                        //}
                        #endregion


                    }

                    var q1 = (from c in _BC
                              group new { c } by new { c.DonGia, c.DonVi, c.MaDV, c.NuocSX, c.TenDV, c.TenNhomCT, c.TenTN, c.MaTam } into kq
                              select new
                              {
                                  TenNhomDV = kq.Key.TenNhomCT,
                                  TieuNhomDV = kq.Key.TenTN,
                                  MaDV = kq.Key.MaDV,
                                  TenDV = kq.Key.TenDV,
                                  NuocSX = kq.Key.NuocSX,
                                  DVT = kq.Key.DonVi,
                                  DonGia = kq.Key.DonGia,
                                  kq.Key.MaTam,

                                  TonDKSL = kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLXuat),
                                  TonDKTT = kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTXuat),

                                  NhapTKSL = kq.Where(p => p.c.NgayNhap >= tungay).Sum(p => p.c.SLNhap),
                                  NhapTKTT = kq.Where(p => p.c.NgayNhap >= tungay).Sum(p => p.c.TTNhap),

                                  TNhapTKSL = kq.Where(p => p.c.NgayNhap >= tungay).Sum(p => p.c.SLNhap) +
                                              kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLXuat),// tổng nhập cộng tồn đầu kỳ
                                  TNhapTKTT = kq.Where(p => p.c.NgayNhap >= tungay).Sum(p => p.c.TTNhap) +
                                              kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTXuat),

                                  XuatBHSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.DK == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p => p.c.ThanhToan != 2).Sum(p => p.c.SLXuat),
                                  XuatBHTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.DK == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p => p.c.ThanhToan != 2).Sum(p => p.c.TTXuat),

                                  Xuat139SL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.DK_139 == 1)
                                                .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p => p.c.ThanhToan != 2).Sum(p => p.c.SLXuat),
                                  Xuat139TT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.DK_139 == 1)
                                                .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p => p.c.ThanhToan != 2).Sum(p => p.c.TTXuat),

                                  XuatTESL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.DK_TE == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p => p.c.ThanhToan != 2).Sum(p => p.c.SLXuat),
                                  XuatTETT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.DK_TE == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p => p.c.ThanhToan != 2).Sum(p => p.c.TTXuat),

                                               //nếu hiển thị hư hao thì xuất điều chuyển là hư hao;
                                               //nếu không tính hư hao: TH1 (nếu xuất khác cộng vào cột xuất khác thì ko được tính trong xuất điều chuyển và ngược lại)
                                  XuatLCSL = ck_HienThiHuHao.Checked ? kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.PhanLoai == 3).Sum(p => p.c.SLXuat) :
                                             kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.XuatNB == 1).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p=> ckXuatKhac.Checked ? p.c.KieuDon != 9 : true).Where(p=> ckXuatKiemNghiem.Checked ? p.c.KieuDon != 8 :true).Sum(p => p.c.SLXuat),
                                  XuatLCTT = ck_HienThiHuHao.Checked ? kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.PhanLoai == 3).Sum(p => p.c.TTXuat) :
                                             kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.XuatNB == 1).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p => ckXuatKhac.Checked ? p.c.KieuDon != 9 : true).Where(p => ckXuatKiemNghiem.Checked ? p.c.KieuDon != 8 : true).Sum(p => p.c.TTXuat),

                                  XuatKhacSL = rdgMau.SelectedIndex == 0 ? kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.PhanLoai == 2 && p.c.KieuDon == 9).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat) : kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.PhanLoai == 2 && (p.c.ThanhToan == 2 || (p.c.KieuDon == 9 && ckXuatKhac.Checked) || (p.c.KieuDon == 8 && ckXuatKiemNghiem.Checked))).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),//.Where(p => p.c.PhanLoai == 2 && p.c.KieuDon == 9).Sum(p => p.c.SLNhap),
                                  XuatKhacTT = rdgMau.SelectedIndex == 0 ? kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.PhanLoai == 2 && p.c.KieuDon == 9).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat) : kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.PhanLoai == 2 && (p.c.ThanhToan == 2 || (p.c.KieuDon == 9 && ckXuatKhac.Checked) || (p.c.KieuDon == 8 && ckXuatKiemNghiem.Checked))).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),//.Where(p => p.c.PhanLoai == 2 && p.c.KieuDon == 9).Sum(p => p.c.TTNhap),

                                  XuatTSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatTTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  TonCKSL = (kq.Sum(p => p.c.SLNhap) - kq.Sum(p => p.c.SLXuat)),
                                  TonCKTT = (kq.Sum(p => p.c.TTNhap) - kq.Sum(p => p.c.TTXuat))
                              }).Select(x => new BCNXT
                              {
                                  TenNhomDV = x.TenNhomDV,
                                  TieuNhomDV = x.TieuNhomDV,
                                  MaDV = x.MaDV,
                                  NuocSX = x.NuocSX,
                                  DVT = x.DVT,
                                  MaTam = x.MaTam,
                                  TenDV = x.TenDV,
                                  DonGia = x.DonGia,
                                  TonDKSL = x.TonDKSL,
                                  TonDKTT = x.TonDKTT,
                                  NhapTKSL = x.NhapTKSL,
                                  NhapTKTT = x.NhapTKTT,
                                  TNhapTKSL = x.TNhapTKSL,
                                  TNhapTKTT = x.TNhapTKTT,
                                  XuatBHSL = x.XuatBHSL - x.Xuat139SL - x.XuatTESL,
                                  XuatBHTT = x.XuatBHTT - x.Xuat139TT - x.XuatTETT,
                                  Xuat139SL = x.Xuat139SL,
                                  Xuat139TT = x.Xuat139TT,
                                  XuatTESL = x.XuatTESL,
                                  XuatTETT = x.XuatTETT,
                                  XuatLCSL = x.XuatLCSL,
                                  XuatLCTT = x.XuatLCTT,
                                  XuatKhacSL = x.XuatKhacSL,
                                  XuatKhacTT = x.XuatKhacTT,
                                  XuatTSL = x.XuatTSL,
                                  XuatTTT = x.XuatTTT,
                                  TongXuatSL = x.XuatBHSL + x.XuatKhacSL + x.XuatLCSL,//dùng cho mẫu xã
                                  TongXuatTT = x.XuatBHTT + x.XuatKhacTT + x.XuatLCTT,
                                  TonCKSL = x.TonCKSL,
                                  TonCKTT = x.TonCKTT
                              }).ToList();

                    BaoCao.rep_BC_NXTThuocXaTamDuong rep1 = new BaoCao.rep_BC_NXTThuocXaTamDuong();
                    rep1.lblTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                    rep1.DataSource = q1.Where(p => p.TenNhomDV.Contains(_pl)).Where(p => p.NhapTKSL != 0 || p.TonDKSL != 0 || p.TonCKSL != 0).OrderBy(p => p.TenDV).ToList();
                    rep1.BindingData();
                    rep1.CreateDocument();
                    frm.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm.ShowDialog();

                    #endregion
                }
                else if (rdgMau.SelectedIndex == 0 && mau1.SelectedIndex == 0)
                {
                    #region mẫu trung tâm xuất kho chính
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BcSuDungThuoc_TDuong rep = new BaoCao.Rep_BcSuDungThuoc_TDuong();
                    rep.TuNgay.Value = dateTuNgay.Text;
                    rep.DenNgay.Value = dateDenNgay.Text;
                    rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                    if (chkThang.Checked == true) { rep.TuNgayDenNgay.Value = "Tháng " + denngay.Month + " năm " + denngay.Year; }
                   
                    List<int> lMaKP = new List<int>();
                    
                    string kho = "";

                    for (int i = 0; i < cklKP.ItemCount; i++)
                    {
                        if (cklKP.GetItemChecked(i))
                        {
                            lMaKP.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
                            kho = kho + "," + cklKP.GetItemText(i);
                        }
                    }

                    rep.Nguon.Value = "Nguồn thuốc: " + kho;
                    string _pl = "";
                    if (lupPL.Text != null)
                    {
                        _pl = lupPL.EditValue.ToString();
                    }
                   // rep.MaKP.Value = _kho;

                    _BC = (from nhapd in data.NhapDs.Where(p => (p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)).Where(p => lMaKP.Contains(p.MaKP ?? -1))//.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                           join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                           join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                           join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                           join nhomdv in data.NhomDVs.Where(p => p.Status == 1) on tieunhomdv.IDNhom equals nhomdv.IDNhom
                           select new BC { TenNhomCT = nhomdv.TenNhomCT, TenTN = tieunhomdv.TenTN, MaTam = dv.MaTam, MaDV = dv.MaDV, NuocSX = dv.NuocSX, TenDV = dv.TenDV, DonVi = dv.DonVi, DonGia = nhapdct.DonGia, MaBNhan = nhapdct.MaBNhan ?? -1, SLNhap = nhapdct.SoLuongN, SLXuat = nhapdct.SoLuongX, TTNhap = nhapdct.ThanhTienN, TTXuat = nhapdct.ThanhTienX, NgayNhap = nhapd.NgayNhap ?? DateTime.Now, KieuDon = nhapd.KieuDon ?? 0, PhanLoai = nhapd.PLoai ?? 0 })
                                         .ToList();

                    List<int> _lMaBN = (from a in _BC group a by a.MaBNhan into kq select kq.Key).ToList();
                    var bn = (from ma in _lMaBN join b in data.BenhNhans on ma equals b.MaBNhan select new { b.NoiTru, b.MaBNhan, b.DTuong, b.MaDTuong }).ToList();
                    foreach (var b in _BC)
                    {
                        b.DK = 0;
                        if (b.MaBNhan > 0)
                        {
                            //int mbn = b.MaBNhan;
                            var q = bn.Where(p => p.MaBNhan == b.MaBNhan).FirstOrDefault();
                            if (q != null)
                            {
                                if (q.DTuong == "BHYT")
                                {
                                    b.DK = 1;// điều kiện là 1 là xuất BHYT
                                }
                                if (q.DTuong == "BHYT" && q.MaDTuong == "TE")
                                {
                                    b.DK_TE = 1;// điều kiện 2 là xuất trẻ em
                                }
                                if (q.DTuong == "BHYT" && (q.MaDTuong == "DT" || q.MaDTuong == "HN" || q.MaDTuong == "CK"))
                                {
                                    b.DK_139 = 1;// điều kiện bằng 3 là xuất 139
                                }

                                if (q.NoiTru != null)
                                {
                                    b.NoiTru = q.NoiTru.Value;
                                }
                            }
                        }
                        if (b.KieuDon == 2)
                        {
                            b.XuatNB = 1;// điều kiện bằng 4 là xuất nội bộ
                        }
                    }

                    var q1 = (from c in _BC
                              group new { c } by new { c.DonGia, c.DonVi, c.MaDV, c.NuocSX, c.TenDV, c.TenNhomCT, c.TenTN, c.MaTam } into kq
                              select new
                              {
                                  TenNhomDV = kq.Key.TenNhomCT,
                                  TieuNhomDV = kq.Key.TenTN,
                                  MaDV = kq.Key.MaDV,
                                  TenDV = kq.Key.TenDV,
                                  NuocSX = kq.Key.NuocSX,
                                  DVT = kq.Key.DonVi,
                                  DonGia = kq.Key.DonGia,
                                  kq.Key.MaTam,

                                  TonDKSL = kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLXuat),
                                  TonDKTT = kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTXuat),

                                  NhapTKSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap),
                                  NhapTKTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap),

                                  TNhapTKSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap) +
                                              kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLXuat),
                                  TNhapTKTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap) +
                                              kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTXuat),

                                  XuatBHSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatBHTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  Xuat139SL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK_139 == 1)
                                                .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  Xuat139TT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK_139 == 1)
                                                .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  XuatTESL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK_TE == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatTETT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK_TE == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  XuatLCSL = ck_HienThiHuHao.Checked ? kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.PhanLoai == 3).Sum(p => p.c.SLXuat) :
                                             kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.XuatNB == 1).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatLCTT = ck_HienThiHuHao.Checked ? kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.PhanLoai == 3).Sum(p => p.c.TTXuat) :
                                             kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.XuatNB == 1).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  XuatKhacSL = kq.Where(p => p.c.NgayNhap >= tungay && p.c.NgayNhap <= denngay).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p => p.c.PhanLoai == 2 && p.c.KieuDon == 9).Sum(p => p.c.SLXuat),
                                  XuatKhacTT = kq.Where(p => p.c.NgayNhap >= tungay && p.c.NgayNhap <= denngay).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Where(p => p.c.PhanLoai == 2 && p.c.KieuDon == 9).Sum(p => p.c.TTXuat),

                                  XuatTSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatTTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  TonCKSL = (kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLXuat)),
                                  TonCKTT = (kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTXuat))
                              }).Select(x => new
                              {
                                  x.TenNhomDV,
                                  x.TieuNhomDV,
                                  x.MaDV,
                                  x.NuocSX,
                                  x.DVT,
                                  x.MaTam,
                                  x.TenDV,
                                  x.DonGia,
                                  x.TonDKSL,
                                  x.TonDKTT,
                                  x.NhapTKSL,
                                  x.NhapTKTT,
                                  x.TNhapTKSL,
                                  x.TNhapTKTT,
                                  XuatBHSL = x.XuatBHSL - x.Xuat139SL - x.XuatTESL,
                                  XuatBHTT = x.XuatBHTT - x.Xuat139TT - x.XuatTETT,
                                  x.Xuat139SL,
                                  x.Xuat139TT,
                                  x.XuatTESL,
                                  x.XuatTETT,
                                  x.XuatLCSL,
                                  x.XuatLCTT,
                                  x.XuatKhacSL,
                                  x.XuatKhacTT,
                                  x.XuatTSL,
                                  x.XuatTTT,
                                  //TongXuatSL = x.XuatBHSL + x.XuatKhacSL + x.XuatLCSL,//dùng cho mẫu xã
                                  //TongXuatTT = x.XuatBHTT + x.XuatKhacTT + x.XuatLCTT,
                                  x.TonCKSL,
                                  x.TonCKTT
                              }).ToList();

                    rep.lbl_TieuDe.Text = ck_HienThiHuHao.Checked ? "Xuất hư hao" : "Chuyển về kho chính";
                    rep.DataSource = q1.Where(p => p.TenNhomDV.Contains(_pl)).Where(p => p.NhapTKSL != 0 || p.TonDKSL != 0 || p.TonCKSL != 0).OrderBy(p => p.TenDV).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion
                }
                else if (rdgMau.SelectedIndex == 0 && mau1.SelectedIndex == 1)
                {
                    #region mẫu trung tâm nhập trả lại
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BcSuDungThuoc_TDuong_New rep = new BaoCao.Rep_BcSuDungThuoc_TDuong_New();
                    rep.TuNgay.Value = dateTuNgay.Text;
                    rep.DenNgay.Value = dateDenNgay.Text;
                    rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                    if (chkThang.Checked == true) { rep.TuNgayDenNgay.Value = "Tháng " + denngay.Month + " năm " + denngay.Year; }

                    List<int> lMaKP = new List<int>();

                    string kho = "";

                    for (int i = 0; i < cklKP.ItemCount; i++)
                    {
                        if (cklKP.GetItemChecked(i))
                        {
                            lMaKP.Add(Convert.ToInt32(cklKP.GetItemValue(i)));
                            kho = kho + "," + cklKP.GetItemText(i);
                        }
                    }

                    rep.Nguon.Value = "Nguồn thuốc: " + kho;
                    string _pl = "";
                    if (lupPL.Text != null)
                    {
                        _pl = lupPL.EditValue.ToString();
                    }
                    // rep.MaKP.Value = _kho;

                    _BC = (from nhapd in data.NhapDs.Where(p => (p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)).Where(p => lMaKP.Contains(p.MaKP ?? -1))//.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                           join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                           join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                           join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                           join nhomdv in data.NhomDVs.Where(p => p.Status == 1) on tieunhomdv.IDNhom equals nhomdv.IDNhom
                           select new BC { TenNhomCT = nhomdv.TenNhomCT, TenTN = tieunhomdv.TenTN, MaTam = dv.MaTam, MaDV = dv.MaDV, NuocSX = dv.NuocSX, TenDV = dv.TenDV, DonVi = dv.DonVi, DonGia = nhapdct.DonGia, MaBNhan = nhapdct.MaBNhan ?? -1, SLNhap = nhapdct.SoLuongN, SLXuat = nhapdct.SoLuongX, TTNhap = nhapdct.ThanhTienN, TTXuat = nhapdct.ThanhTienX, NgayNhap = nhapd.NgayNhap ?? DateTime.Now, KieuDon = nhapd.KieuDon ?? 0, PhanLoai = nhapd.PLoai ?? 0 })
                                         .ToList();

                    List<int> _lMaBN = (from a in _BC group a by a.MaBNhan into kq select kq.Key).ToList();
                    var bn = (from ma in _lMaBN join b in data.BenhNhans on ma equals b.MaBNhan select new { b.NoiTru, b.MaBNhan, b.DTuong, b.MaDTuong }).ToList();
                    foreach (var b in _BC)
                    {
                        b.DK = 0;
                        if (b.MaBNhan > 0)
                        {
                            //int mbn = b.MaBNhan;
                            var q = bn.Where(p => p.MaBNhan == b.MaBNhan).FirstOrDefault();
                            if (q != null)
                            {
                                if (q.DTuong == "BHYT")
                                {
                                    b.DK = 1;// điều kiện là 1 là xuất BHYT
                                }
                                if (q.DTuong == "BHYT" && q.MaDTuong == "TE")
                                {
                                    b.DK_TE = 1;// điều kiện 2 là xuất trẻ em
                                }
                                if (q.DTuong == "BHYT" && (q.MaDTuong == "DT" || q.MaDTuong == "HN" || q.MaDTuong == "CK"))
                                {
                                    b.DK_139 = 1;// điều kiện bằng 3 là xuất 139
                                }

                                if (q.NoiTru != null)
                                {
                                    b.NoiTru = q.NoiTru.Value;
                                }
                            }
                        }
                        if (b.KieuDon == 2)
                        {
                            b.XuatNB = 1;// điều kiện bằng 4 là xuất nội bộ
                        }
                    }

                    var q1 = (from c in _BC
                              group new { c } by new { c.DonGia, c.DonVi, c.MaDV, c.NuocSX, c.TenDV, c.TenNhomCT, c.TenTN, c.MaTam } into kq
                              select new
                              {
                                  TenNhomDV = kq.Key.TenNhomCT,
                                  TieuNhomDV = kq.Key.TenTN,
                                  MaDV = kq.Key.MaDV,
                                  TenDV = kq.Key.TenDV,
                                  NuocSX = kq.Key.NuocSX,
                                  DVT = kq.Key.DonVi,
                                  DonGia = kq.Key.DonGia,
                                  kq.Key.MaTam,

                                  TonDKSL = kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLXuat),
                                  TonDKTT = kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTXuat),

                                  NhapTKSL = kq.Where(p => p.c.NgayNhap >= tungay && p.c.KieuDon != 2).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap),
                                  NhapTKTT = kq.Where(p => p.c.NgayNhap >= tungay && p.c.KieuDon != 2).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap),

                                  NhapTLSL = kq.Where(p => p.c.NgayNhap >= tungay && p.c.KieuDon == 2).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap),
                                  NhapTLTT = kq.Where(p => p.c.NgayNhap >= tungay && p.c.KieuDon == 2).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap),

                                  TNhapTKSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap) +
                                              kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.SLXuat),
                                  TNhapTKTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap) +
                                              kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap < tungay).Sum(p => p.c.TTXuat),

                                  XuatBHSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatBHTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  Xuat139SL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK_139 == 1)
                                                .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  Xuat139TT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK_139 == 1)
                                                .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  XuatTESL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK_TE == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatTETT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.DK_TE == 1)
                                               .Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  XuatLCSL = ck_HienThiHuHao.Checked ? kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.PhanLoai == 3).Sum(p => p.c.SLXuat) :
                                             kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.XuatNB == 1).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatLCTT = ck_HienThiHuHao.Checked ? kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.PhanLoai == 3).Sum(p => p.c.TTXuat) :
                                             kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => p.c.XuatNB == 1).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  XuatKhacSL = kq.Where(p => p.c.NgayNhap >= tungay && p.c.NgayNhap <= denngay).Where(p => p.c.PhanLoai == 2 && p.c.KieuDon == 9).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatKhacTT = kq.Where(p => p.c.NgayNhap >= tungay && p.c.NgayNhap <= denngay).Where(p => p.c.PhanLoai == 2 && p.c.KieuDon == 9).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  XuatTSL = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.SLXuat),
                                  XuatTTT = kq.Where(p => p.c.NgayNhap >= tungay).Where(p => p.c.NgayNhap <= denngay).Where(p => radBN.SelectedIndex == 2 ? true : p.c.NoiTru == radBN.SelectedIndex).Sum(p => p.c.TTXuat),

                                  TonCKSL = (kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLNhap) - kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.SLXuat)),
                                  TonCKTT = (kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTNhap) - kq.Where(p => p.c.NgayNhap <= denngay).Sum(p => p.c.TTXuat))
                              }).Select(x => new
                              {
                                  x.TenNhomDV,
                                  x.TieuNhomDV,
                                  x.MaDV,
                                  x.NuocSX,
                                  x.DVT,
                                  x.MaTam,
                                  x.TenDV,
                                  x.DonGia,
                                  x.TonDKSL,
                                  x.TonDKTT,
                                  x.NhapTKSL,
                                  x.NhapTKTT,
                                  x.NhapTLSL,
                                  x.NhapTLTT,
                                  x.TNhapTKSL,
                                  x.TNhapTKTT,
                                  XuatBHSL = x.XuatBHSL - x.Xuat139SL - x.XuatTESL,
                                  XuatBHTT = x.XuatBHTT - x.Xuat139TT - x.XuatTETT,
                                  x.Xuat139SL,
                                  x.Xuat139TT,
                                  x.XuatTESL,
                                  x.XuatTETT,
                                  x.XuatLCSL,
                                  x.XuatLCTT,
                                  x.XuatKhacSL,
                                  x.XuatKhacTT,
                                  x.XuatTSL,
                                  x.XuatTTT,
                                  //TongXuatSL = x.XuatBHSL + x.XuatKhacSL + x.XuatLCSL,//dùng cho mẫu xã
                                  //TongXuatTT = x.XuatBHTT + x.XuatKhacTT + x.XuatLCTT,
                                  x.TonCKSL,
                                  x.TonCKTT
                              }).ToList();

                    //rep.lbl_TieuDe.Text = ck_HienThiHuHao.Checked ? "Xuất hư hao" : "Chuyển về kho chính";
                    rep.DataSource = q1.Where(p => p.TenNhomDV.Contains(_pl)).Where(p =>p.NhapTLSL != 0 && p.NhapTKSL != 0 || p.TonDKSL != 0 || p.TonCKSL != 0).OrderBy(p => p.TenDV).ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion
                }
            }
        }

        public class BCNXT
        {
            public string TieuNhomDV { get; set; }

            public string TenNhomDV { get; set; }

            public int MaDV { get; set; }

            public string NuocSX { get; set; }

            public string DVT { get; set; }

            public string MaTam { get; set; }

            public string TenDV { get; set; }

            public double DonGia { get; set; }

            public double TonDKSL { get; set; }

            public double TonDKTT { get; set; }

            public double NhapTKSL { get; set; }

            public double NhapTKTT { get; set; }

            public double TNhapTKSL { get; set; }

            public double TNhapTKTT { get; set; }

            public double XuatBHSL { get; set; }

            public double XuatBHTT { get; set; }

            public double Xuat139SL { get; set; }

            public double Xuat139TT { get; set; }

            public double XuatTESL { get; set; }

            public double XuatTETT { get; set; }

            public double XuatLCSL { get; set; }

            public double XuatLCTT { get; set; }

            public double XuatKhacSL { get; set; }

            public double XuatKhacTT { get; set; }

            public double XuatTSL { get; set; }

            public double XuatTTT { get; set; }

            public double TonCKSL { get; set; }

            public double TonCKTT { get; set; }

            public double TongXuatSL { get; set; }

            public double TongXuatTT { get; set; }
        }
        private void rdgMau_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void rdgMau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdgMau.SelectedIndex == 0)
            {
                ck_HienThiHuHao.Enabled = true;
                chkThang.Enabled = true;
                ckXuatKhac.Visible = false   ;
                ckXuatKiemNghiem.Visible = false;
                ckXuatKhac.Checked = false;
                ckXuatKiemNghiem.Checked = false;
                mau1.Visible = true;
            }
            if (rdgMau.SelectedIndex == 1)
            {
                ck_HienThiHuHao.Enabled = false;
                chkThang.Enabled = false;
                ckXuatKhac.Visible = true;
                ckXuatKiemNghiem.Visible = true;
                mau1.Visible = false;
            }
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (cklKP.GetItemChecked(0))
                    cklKP.CheckAll();
                else
                    cklKP.UnCheckAll();
            }
        }

        private void mau1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdgMau.SelectedIndex == 0)
            {
                ck_HienThiHuHao.Enabled = false;
                chkThang.Enabled = true;
                ckXuatKhac.Visible = false;
                mau1.Visible = true;
            }
            if (rdgMau.SelectedIndex == 1)
            {
                ck_HienThiHuHao.Enabled = false;
                chkThang.Enabled = false;
                ckXuatKhac.Visible = true;
                mau1.Visible = false;
            }
        }
    }
}