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
    public partial class Frm_BcSuDungDuoc_BG : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcSuDungDuoc_BG()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBc()
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
                MessageBox.Show("Bạn chưa chọn kho xuất dược");
                lupKho.Focus();
                return false;
            }

            if (cmbPL.Text == null)
            {
                MessageBox.Show("Bạn chưa chọn phân loại dược");
                cmbPL.Focus();
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
        List<KPhong> _Kphong = new List<KPhong>();
        private void Frm_BcSuDungDuoc_BG_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            dateTuNgay.Focus();
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            var kd = (from khoa in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc) select new { khoa.TenKP, khoa.MaKP }).ToList();
            if (kd.Count() > 0)
            {
                lupKho.Properties.DataSource = kd;
            }
            cmbPL.EditValue = "Thuốc";
            var kphong = (from nd in data.NhapDs.Where(p => p.PLoai == 5)
                          join kp in data.KPhongs on nd.MaKPnx equals kp.MaKP
                          group kp by new { kp.TenKP, kp.MaKP } into kq
                          select new { kq.Key.TenKP, kq.Key.MaKP }).ToList();
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
        #region class SDDuoc
        private class SDDuoc
        {
            private int MaDV;
            private string TenDV;
            private string NuocSX;
            private string DVT;
            private string SL1;
            private string SL2;
            private string SL3;
            private string SL4;
            private string SL5;
            private string SL6;
            private string SL7;
            private string SL8;
            private string SL9;
            private string SL10;
            private string SL11;
            private string SL12;
            private string SL13;
            private string SL14;
            private string SL15, SL16, SL17, SL18, SL19;
            private string TC;
            private string NhomDV;

            public string nhomdv
            { set { NhomDV = value; } get { return NhomDV; } }
            public int madv
            { set { MaDV = value; } get { return MaDV; } }
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
            public string nuocsx
            { set { NuocSX = value; } get { return NuocSX; } }
            public string dvt
            { set { DVT = value; } get { return DVT; } }
            public string sl1
            { set { SL1 = value; } get { return SL1; } }
            public string sl2
            { set { SL2 = value; } get { return SL2; } }
            public string sl3
            { set { SL3 = value; } get { return SL3; } }
            public string sl4
            { set { SL4 = value; } get { return SL4; } }
            public string sl5
            { set { SL5 = value; } get { return SL5; } }
            public string sl6
            { set { SL6 = value; } get { return SL6; } }
            public string sl7
            { set { SL7 = value; } get { return SL7; } }
            public string sl8
            { set { SL8 = value; } get { return SL8; } }
            public string sl9
            { set { SL9 = value; } get { return SL9; } }
            public string sl10
            { set { SL10 = value; } get { return SL10; } }
            public string sl11
            { set { SL11 = value; } get { return SL11; } }
            public string sl12
            { set { SL12 = value; } get { return SL12; } }
            public string sl13
            { set { SL13 = value; } get { return SL13; } }
            public string sl14
            { set { SL14 = value; } get { return SL14; } }
            public string sl15
            { set { SL15 = value; } get { return SL15; } }
            public string sl16
            { set { SL16 = value; } get { return SL16; } }
            public string sl17
            { set { SL17 = value; } get { return SL17; } }
            public string sl18
            { set { SL18 = value; } get { return SL18; } }
            public string sl19
            { set { SL19 = value; } get { return SL19; } }
            public string tc
            { set { TC = value; } get { return TC; } }


            public string TenTN { get; set; }

            public double ThanhTien { get; set; }

            public double DongGia { get; set; }
        }
        #endregion
        #region class DSKP
        class DSKP
        {
            private string TenKP;
            private int MaKP;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            {
                set { MaKP = value; }
                get { return MaKP; }
            }
        }
        #endregion
        List<DSKP> _DSKP = new List<DSKP>();
        List<SDDuoc> _SDDuoc = new List<SDDuoc>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            List<KPhong> _lKhoaP = new List<KPhong>();


            if (KTtaoBc())
            {
                int _makpx = 0;
                string _PL = "";
                _SDDuoc.Clear();
                _DSKP.Clear();
                if (cmbPL.Text != null)
                {
                    _PL = cmbPL.EditValue.ToString();
                }
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                if (lupKho.EditValue != null)
                {
                    _makpx = Convert.ToInt32(lupKho.EditValue);
                }

                _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                _lKhoaP.Add(new KPhong { makp = 0, tenkp = "" });
                var _ldv = (from dv in data.DichVus
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            join nhdv in data.NhomDVs on tn.IDNhom equals nhdv.IDNhom
                            select new { dv, nhdv, tn }).ToList();
                var q1 = (from nd in data.NhapDs.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 5)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          join kp1 in data.KPhongs.Where(p => p.MaKP == _makpx) on nd.MaKP equals kp1.MaKP
                          join kp2 in data.KPhongs on nd.MaKPnx equals kp2.MaKP //.Where(p => p.PLoai == _plx)
                          select new { kp2.MaKP, nd.NgayNhap, nd.PLoai, nd.MaKPnx, nd.IDNhap, ndct.MaDV, ndct.SoLuongN, ndct.DonGia, ndct.SoLuongSD, ndct.ThanhTienSD }).ToList();
                var qkp = (from k in _lKhoaP
                           join nd in q1 on k.makp equals nd.MaKP
                           group new { k, nd } by new { k.makp, k.tenkp } into kq
                           select new { kq.Key.makp, kq.Key.tenkp }).OrderBy(p => p.tenkp).ToList();
                if (qkp.Count > 0)
                {
                    foreach (var a in qkp)
                    {
                        DSKP themmoi = new DSKP();
                        themmoi.tenkp = a.tenkp;
                        themmoi.makp = a.makp;
                        _DSKP.Add(themmoi);
                    }
                }
                string _pl = "";
                if (cmbPL.Text != null)
                {
                    if (cmbPL.Text == "Tất cả")
                    { _pl = ""; }
                    else { _pl = cmbPL.Text; }

                }
                var MaDV = (from k in _lKhoaP
                            join nd in q1.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 5) on k.makp equals nd.MaKP
                            join dv in _ldv.Where(p => p.nhdv.TenNhom.Contains(_pl)) on nd.MaDV equals dv.dv.MaDV
                            //join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                            //join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                            //join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            //join nhdv in data.NhomDVs.Where(p => p.TenNhom.Contains(_pl)) on tn.IDNhom equals nhdv.IDNhom
                            group new { nd, dv } by new { nd.MaDV, dv.dv.TenDV, dv.dv.NuocSX, dv.dv.DonVi, nd.DonGia, dv.nhdv.TenNhom, dv.tn.IdTieuNhom, dv.tn.TenTN, dv.tn.TenRG } into kq
                            select new
                            {
                                // makp = kq.Key.MaKP,
                                TenDV = kq.Key.TenDV,
                                MaDV = kq.Key.MaDV,
                                kq.Key.DonGia,
                                NuocSX = kq.Key.NuocSX,
                                DVT = kq.Key.DonVi,
                                NhomDV = kq.Key.TenNhom,
                                kq.Key.TenTN
                            }).OrderBy(p => p.TenDV).ToList();

                if (MaDV.Count > 0)
                {
                    foreach (var a in MaDV)
                    {
                        SDDuoc themmoi = new SDDuoc();
                        themmoi.madv = a.MaDV == null ? 0 : a.MaDV.Value;
                        themmoi.tendv = a.TenDV;
                        themmoi.DongGia = a.DonGia;
                        themmoi.nuocsx = a.NuocSX;
                        themmoi.dvt = a.DVT;
                        themmoi.nhomdv = a.NhomDV;
                        themmoi.TenTN = a.TenTN;
                        _SDDuoc.Add(themmoi);
                    }
                }

                var qsl = (from k in _lKhoaP
                           join nd in q1.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 5) on k.makp equals nd.MaKP
                           join dv in _ldv.Where(p => p.nhdv.TenNhom.Contains(_pl)) on nd.MaDV equals dv.dv.MaDV
                           //join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                           //join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                           //join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           //join nhdv in data.NhomDVs.Where(p => p.TenNhom.Contains(_pl)) on tn.IDNhom equals nhdv.IDNhom
                           group new { nd, dv } by new { nd.MaKPnx, nd.MaDV, nd.DonGia, dv.dv.TenDV, dv.dv.NuocSX, dv.dv.DonVi, dv.tn.TenTN, dv.nhdv.TenNhom } into kq
                           select new
                           {
                               MaDV = kq.Key.MaDV,
                               NuocSX = kq.Key.NuocSX,
                               DVT = kq.Key.DonVi,
                               NhomDV = kq.Key.TenNhom,
                               makp = kq.Key.MaKPnx,
                               madv = kq.Key.MaDV,
                               kq.Key.DonGia,
                               TenDV = kq.Key.TenDV,
                               kq.Key.TenTN,
                               SoLuongSD = kq.Sum(p => p.nd.SoLuongSD),
                               ThanhTien = kq.Sum(p => p.nd.ThanhTienSD),
                           }).OrderBy(p => p.TenDV).ToList();

                var qsltheoxa = (from k in _DSKP
                                 join a in qsl on k.makp equals a.makp
                                 group new { a, k } by new { a.makp, a.MaDV, a.DonGia, a.TenDV, a.NuocSX, a.DVT, a.TenTN, a.NhomDV, k.tenkp } into kq
                                 select new
                                 {
                                     kq.Key.tenkp,
                                     NuocSX = kq.Key.NuocSX,
                                     DVT = kq.Key.DVT,
                                     NhomDV = kq.Key.NhomDV,
                                     makp = kq.Key.makp,
                                     madv = kq.Key.MaDV,
                                     kq.Key.DonGia,
                                     TenDV = kq.Key.TenDV,
                                     kq.Key.TenTN,
                                     SoLuongSD = kq.Sum(p => p.a.SoLuongSD),
                                     ThanhTien = kq.Sum(p => p.a.ThanhTien)
                                 }).OrderBy(p => p.TenDV).ToList();
                if (qsl.Count > 0)
                {
                    foreach (var a in _SDDuoc)
                    {
                        foreach (var b in qsl)
                        {
                            if (a.madv == b.madv && a.DongGia == b.DonGia)
                            {
                                if (b.SoLuongSD != null && b.SoLuongSD != 0)
                                {
                                    for (int i = 0; i < _DSKP.Count; i++)
                                    {
                                        if (b.makp == _DSKP.Skip(i).First().makp)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    a.sl1 = b.SoLuongSD.ToString();
                                                    break;
                                                case 1:
                                                    a.sl2 = b.SoLuongSD.ToString();
                                                    break;
                                                case 2:
                                                    a.sl3 = b.SoLuongSD.ToString();
                                                    break;
                                                case 3:
                                                    a.sl4 = b.SoLuongSD.ToString();
                                                    break;
                                                case 4:
                                                    a.sl5 = b.SoLuongSD.ToString();
                                                    break;
                                                case 5:
                                                    a.sl6 = b.SoLuongSD.ToString();
                                                    break;
                                                case 6:
                                                    a.sl7 = b.SoLuongSD.ToString();
                                                    break;
                                                case 7:
                                                    a.sl8 = b.SoLuongSD.ToString();
                                                    break;
                                                case 8:
                                                    a.sl9 = b.SoLuongSD.ToString();
                                                    break;
                                                case 9:
                                                    a.sl10 = b.SoLuongSD.ToString();
                                                    break;
                                                case 10:
                                                    a.sl11 = b.SoLuongSD.ToString();
                                                    break;
                                                case 11:
                                                    a.sl12 = b.SoLuongSD.ToString();
                                                    break;
                                                case 12:
                                                    a.sl13 = b.SoLuongSD.ToString();
                                                    break;
                                                case 13:
                                                    a.sl14 = b.SoLuongSD.ToString();
                                                    break;
                                                case 14:
                                                    a.sl15 = b.SoLuongSD.ToString();
                                                    break;
                                                case 15:
                                                    a.sl16 = b.SoLuongSD.ToString();
                                                    break;
                                                case 16:
                                                    a.sl17 = b.SoLuongSD.ToString();
                                                    break;
                                                case 17:
                                                    a.sl18 = b.SoLuongSD.ToString();
                                                    break;
                                                case 18:
                                                    a.sl19 = b.SoLuongSD.ToString();
                                                    break;

                                            }
                                            a.tendv = b.TenDV;
                                            a.TenTN = b.TenTN;// 

                                        }
                                    }

                                }
                                //a.tc = b.SoLuongX.ToString();
                            }
                        }
                    }

                }
                var qsltong = (from k in _lKhoaP
                               join nd in q1.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay && p.PLoai == 5) on k.makp equals nd.MaKP
                               join dv in _ldv.Where(p => p.nhdv.TenNhom.Contains(_pl)) on nd.MaDV equals dv.dv.MaDV
                               //join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                               //join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                               //join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                               //join nhdv in data.NhomDVs.Where(p => p.TenNhom.Contains(_pl)) on tn.IDNhom equals nhdv.IDNhom
                               group new { nd, dv } by new { nd.MaDV, dv.dv.TenDV, dv.tn.IdTieuNhom, dv.tn.TenRG, dv.tn.TenTN, nd.DonGia } into kq
                               select new
                               {
                                   madv = kq.Key.MaDV,
                                   kq.Key.DonGia,
                                   TenDV = kq.Key.TenDV,
                                   kq.Key.TenTN,
                                   SoLuongSD = kq.Sum(p => p.nd.SoLuongSD),
                                   ThanhTien = kq.Sum(p => p.nd.ThanhTienSD),
                               }).OrderBy(p => p.TenDV).ToList();

                if (qsltong.Count > 0)
                {
                    foreach (var a in _SDDuoc)
                    {
                        foreach (var b in qsltong)
                        {
                            if (a.madv == b.madv && a.DongGia == b.DonGia)
                            {
                                if (b.SoLuongSD != null && b.SoLuongSD != 0)
                                {
                                    a.tc = b.SoLuongSD.ToString();
                                    a.ThanhTien = b.ThanhTien;
                                }
                            }
                        }
                    }
                }

                if (ckHTThanhTien.Checked)
                {
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BcSuDungDuoc_BG_TT rep = new BaoCao.Rep_BcSuDungDuoc_BG_TT(ckHTTieuNhom.Checked);
                    rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                    var kp = from k in data.KPhongs.Where(p => p.MaKP == _makpx) select new { k.TenKP };
                    //rep.TenBC.Value = ("bảng kê xuất " + cmbPL.Text + " tại " + kp.First().TenKP).ToUpper();
                    if (cmbPL.Text == "Tất cả")
                    {
                        rep.NhomDV.Value = 0;
                        rep.TenBC.Value = ("bảng kê xuất dược tại " + kp.First().TenKP).ToUpper();
                        rep.TenDuoc.Value = "Tên dược";
                    }
                    else
                    {
                        rep.TenBC.Value = ("bảng kê xuất " + cmbPL.Text + " tại " + kp.First().TenKP).ToUpper();
                        rep.TenDuoc.Value = "Tên " + cmbPL.Text;
                    }

                    for (int i = 0; i < _DSKP.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP1.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 1:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP2.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 2:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP3.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 3:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP4.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 4:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP5.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 5:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP6.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 6:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP7.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 7:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP8.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 8:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP9.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 9:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP10.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 10:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP11.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 11:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP12.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 12:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP13.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 13:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP14.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 14:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP15.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 15:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP16.Value = _DSKP.Skip(i).First().tenkp; }
                                break;

                        }
                    }
                    rep.DataSource = _SDDuoc.OrderBy(p => p.tendv);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();


                }
                else
                {
                    frmIn frm = new frmIn();
                    BaoCao.Rep_BcSuDungDuoc_BG rep = new BaoCao.Rep_BcSuDungDuoc_BG(ckHTTieuNhom.Checked);
                    rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                    var kp = from k in data.KPhongs.Where(p => p.MaKP == _makpx) select new { k.TenKP };
                    //rep.TenBC.Value = ("bảng kê xuất " + cmbPL.Text + " tại " + kp.First().TenKP).ToUpper();
                    if (cmbPL.Text == "Tất cả")
                    {
                        rep.NhomDV.Value = 0;
                        rep.TenBC.Value = ("bảng kê xuất dược tại " + kp.First().TenKP).ToUpper();
                        rep.TenDuoc.Value = "Tên dược";
                    }
                    else
                    {
                        rep.TenBC.Value = ("bảng kê xuất " + cmbPL.Text + " tại " + kp.First().TenKP).ToUpper();
                        rep.TenDuoc.Value = "Tên " + cmbPL.Text;
                    }

                    for (int i = 0; i < _DSKP.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP1.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 1:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP2.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 2:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP3.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 3:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP4.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 4:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP5.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 5:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP6.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 6:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP7.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 7:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP8.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 8:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP9.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 9:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP10.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 10:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP11.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 11:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP12.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 12:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP13.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 13:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP14.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 14:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP15.Value = _DSKP.Skip(i).First().tenkp; }
                                break;
                            case 15:
                                if (_DSKP.Skip(i).First().tenkp != null)
                                { rep.KP16.Value = _DSKP.Skip(i).First().tenkp; }
                                break;

                        }
                    }
                    rep.DataSource = _SDDuoc.OrderBy(p => p.tendv);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
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