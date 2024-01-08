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
    public partial class Frm_BcTonDuoc_BG : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcTonDuoc_BG()
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
            //if (dateDenNgay.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
            //    dateDenNgay.Focus();
            //    return false;
            //}
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

            if (cklNhomDV.Enabled && (ckcTieuNhomDV.CheckedItemsCount <= 0 || cklNhomDV.CheckedItemsCount <= 0))
            {
                MessageBox.Show("Bạn chưa chọn nhóm, tiểu nhóm dịch vụ");
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
        List<TieuNhomDV> _listTieuNhom = new List<TieuNhomDV>();
        List<NhomDV> _listNhomDV = new List<NhomDV>();
        private void Frm_BcSuDungDuoc_BG_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            dateTuNgay.Focus();
            dateTuNgay.DateTime = System.DateTime.Now;
            //dateDenNgay.DateTime = System.DateTime.Now;
            var kd = (from khoa in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc) select new { khoa.TenKP, khoa.MaKP }).ToList();
            if (kd.Count() > 0)
            {
                lupKho.Properties.DataSource = kd;
            }


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

            _listNhomDV = data.NhomDVs.Where(p => p.Status > 0 && p.TenNhom.Contains("Thuốc")).ToList();
            cklNhomDV.DisplayMember = "TenNhomCT";
            cklNhomDV.ValueMember = "IDNhom";
            //cklNhomDV.DataSource = _listNhomDV;
            ckcTieuNhomDV.DisplayMember = "TenTN";
            ckcTieuNhomDV.ValueMember = "IdTieuNhom";
            _listTieuNhom = data.TieuNhomDVs.Where(p => p.Status == 1).ToList();

            cmbPL.EditValue = "Thuốc";

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
            private string TCTT;

            public string tctt
            {
                get { return TCTT; }
                set { TCTT = value; }
            }
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


            public double DonGia { get; set; }
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
            //DateTime denngay = System.DateTime.Now.Date;
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
                //denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                if (lupKho.EditValue.ToString() != null)
                {
                    _makpx = Convert.ToInt32(lupKho.EditValue);
                }
                #region lấy ds xã
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
                int _MaKP16 = 0;
                int _MaKP17 = 0;
                int _MaKP18 = 0;
                int _MaKP19 = 0;
                int _MaKP20 = 0;
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
                            case 15:
                                _MaKP16 = _Kphong.Skip(i).First().makp;
                                break;
                            case 16:
                                _MaKP17 = _Kphong.Skip(i).First().makp;
                                break;
                            case 17:
                                _MaKP18 = _Kphong.Skip(i).First().makp;
                                break;
                            case 18:
                                _MaKP19 = _Kphong.Skip(i).First().makp;
                                break;
                            case 19:
                                _MaKP20 = _Kphong.Skip(i).First().makp;
                                break;
                        }
                    }
                }
                #endregion
                var qkp = (from nd in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 2 || (p.PLoai == 1 && p.KieuDon == 2)).Where(p => p.NgayNhap <= tungay)
                           join kp1 in data.KPhongs.Where(p => p.MaKP == _makpx) on nd.MaKP equals kp1.MaKP
                           join kp2 in data.KPhongs on nd.MaKPnx equals kp2.MaKP //.Where(p => p.PLoai == _plx)
                           //where (nd.NgayNhap <= tungay)
                           where (kp2.MaKP == _MaKP1 || kp2.MaKP == _MaKP2 || kp2.MaKP == _MaKP3 || kp2.MaKP == _MaKP4 || kp2.MaKP == _MaKP5 || kp2.MaKP == _MaKP6 || kp2.MaKP == _MaKP7 || kp2.MaKP == _MaKP8 || kp2.MaKP == _MaKP9 || kp2.MaKP == _MaKP10 || kp2.MaKP == _MaKP11 || kp2.MaKP == _MaKP12 ||
                           kp2.MaKP == _MaKP13 || kp2.MaKP == _MaKP14 || kp2.MaKP == _MaKP15 || kp2.MaKP == _MaKP16 || kp2.MaKP == _MaKP17 || kp2.MaKP == _MaKP18 || kp2.MaKP == _MaKP19 || kp2.MaKP == _MaKP20)
                           group new { kp2 } by new { kp2.MaKP, kp2.TenKP } into kq
                           select new { kq.Key.MaKP, kq.Key.TenKP }).OrderBy(p => p.TenKP).ToList();
                if (qkp.Count > 0)
                {
                    foreach (var a in qkp)
                    {
                        DSKP themmoi = new DSKP();
                        themmoi.tenkp = a.TenKP;
                        themmoi.makp = a.MaKP;
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
                var MaDV = (from nd in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 2 || (p.PLoai == 1 && p.KieuDon == 2)).Where(p => p.NgayNhap <= tungay)
                            join kp1 in data.KPhongs.Where(p => p.MaKP == _makpx) on nd.MaKP equals kp1.MaKP
                            join kp2 in data.KPhongs on nd.MaKPnx equals kp2.MaKP
                            join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                            join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                            join nhdv in data.NhomDVs.Where(p => p.TenNhom.Contains(_pl)) on dv.IDNhom equals nhdv.IDNhom
                            where (kp2.MaKP == _MaKP1 || kp2.MaKP == _MaKP2 || kp2.MaKP == _MaKP3 || kp2.MaKP == _MaKP4 || kp2.MaKP == _MaKP5 || kp2.MaKP == _MaKP6 || kp2.MaKP == _MaKP7 || kp2.MaKP == _MaKP8 || kp2.MaKP == _MaKP9 || kp2.MaKP == _MaKP10 || kp2.MaKP == _MaKP11 || kp2.MaKP == _MaKP12 ||
                            kp2.MaKP == _MaKP13 || kp2.MaKP == _MaKP14 || kp2.MaKP == _MaKP15 || kp2.MaKP == _MaKP16 || kp2.MaKP == _MaKP17 || kp2.MaKP == _MaKP18 || kp2.MaKP == _MaKP19 || kp2.MaKP == _MaKP20)
                            group new { nd, ndct, dv } by new { nd.MaKP, ndct.MaDV, dv.TenDV, dv.NuocSX, dv.DonVi, nhdv.TenNhom, ndct.DonGia, nhdv.IDNhom, dv.IdTieuNhom } into kq
                            select new
                            {
                                makp = kq.Key.MaKP,
                                kq.Key.DonGia,
                                TenDV = kq.Key.TenDV,
                                MaDV = kq.Key.MaDV,
                                NuocSX = kq.Key.NuocSX,
                                DVT = kq.Key.DonVi,
                                NhomDV = kq.Key.TenNhom,
                                IDNhom = kq.Key.IDNhom,
                                IDTieuNhom = kq.Key.IdTieuNhom,
                            }).OrderBy(p => p.TenDV).ToList();

                if (ckcTieuNhomDV.Enabled)
                {
                    List<int> _idTieuNhomDV = new List<int>();
                    for (int i = 0; i < ckcTieuNhomDV.ItemCount; i++)
                    {
                        if (ckcTieuNhomDV.GetItemCheckState(i) == CheckState.Checked)
                            _idTieuNhomDV.Add(Convert.ToInt32(ckcTieuNhomDV.GetItemValue(i)));
                    }

                    var tn = _listTieuNhom.Where(o => _idTieuNhomDV.Contains(o.IdTieuNhom)).ToList();

                    MaDV = (from dv in MaDV
                            join tndv in tn on dv.IDTieuNhom equals tndv.IdTieuNhom
                            select dv).OrderBy(p => p.TenDV).ToList();
                }

                if (MaDV.Count > 0)
                {
                    foreach (var a in MaDV)
                    {
                        SDDuoc themmoi = new SDDuoc();
                        //themmoi
                        themmoi.madv = a.MaDV == null ? 0 : a.MaDV.Value;
                        themmoi.DonGia = a.DonGia;
                        themmoi.tendv = a.TenDV;
                        themmoi.nuocsx = a.NuocSX;
                        themmoi.dvt = a.DVT;
                        themmoi.nhomdv = a.NhomDV;
                        _SDDuoc.Add(themmoi);
                    }
                }

                var qsl = (from nd in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 2 || (p.PLoai == 1 && p.KieuDon == 2)).Where(p => p.NgayNhap <= tungay)
                           join kp1 in data.KPhongs.Where(p => p.MaKP == _makpx) on nd.MaKP equals kp1.MaKP
                           join kp2 in data.KPhongs on nd.MaKPnx equals kp2.MaKP
                           join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                           join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                           join nhdv in data.NhomDVs.Where(p => p.TenNhom.Contains(_pl)) on dv.IDNhom equals nhdv.IDNhom
                           where (kp2.MaKP == _MaKP1 || kp2.MaKP == _MaKP2 || kp2.MaKP == _MaKP3 || kp2.MaKP == _MaKP4 || kp2.MaKP == _MaKP5 || kp2.MaKP == _MaKP6 || kp2.MaKP == _MaKP7 || kp2.MaKP == _MaKP8 || kp2.MaKP == _MaKP9 || kp2.MaKP == _MaKP10 || kp2.MaKP == _MaKP11 || kp2.MaKP == _MaKP12 || kp2.MaKP == _MaKP13 || kp2.MaKP == _MaKP14 || kp2.MaKP == _MaKP15 || kp2.MaKP == _MaKP16
                           || kp2.MaKP == _MaKP17 || kp2.MaKP == _MaKP18 || kp2.MaKP == _MaKP19 || kp2.MaKP == _MaKP20)
                           group new { nd, ndct, dv } by new { nd.MaKPnx, ndct.MaDV, dv.TenDV, ndct.DonGia } into kq
                           select new
                           {
                               makp = kq.Key.MaKPnx,
                               kq.Key.DonGia,
                               madv = kq.Key.MaDV,
                               TenDV = kq.Key.TenDV,
                               //SoLuongSD = kq.Sum(p => p.ndct.SoLuongX) - kq.Sum(p => p.ndct.SoLuongSD),
                               SoLuongSD = kq.Sum(p => p.ndct.SoLuongX) - kq.Sum(p => p.ndct.SoLuongSD) - kq.Sum(p => p.ndct.SoLuongN),
                               //so = kq.Sum(p => p.ndct.SoLuongN),
                           }).OrderBy(p => p.TenDV).ToList();
                if (qsl.Count > 0)
                {
                    foreach (var a in _SDDuoc)
                    {
                        foreach (var b in qsl)
                        {
                            if (a.madv == b.madv && a.DonGia == b.DonGia)
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

                                        }
                                    }

                                }
                                //a.tc = b.SoLuongX.ToString();
                            }
                        }
                    }
                }
                var qsltong = (from nd in data.NhapDs.Where(p => p.PLoai == 5 || p.PLoai == 2 || (p.PLoai == 1 && p.KieuDon == 2)).Where(p => p.NgayNhap <= tungay)
                               join kp1 in data.KPhongs.Where(p => p.MaKP == _makpx) on nd.MaKP equals kp1.MaKP
                               join kp2 in data.KPhongs on nd.MaKPnx equals kp2.MaKP
                               join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                               join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                               join nhdv in data.NhomDVs.Where(p => p.TenNhom.Contains(_pl)) on dv.IDNhom equals nhdv.IDNhom
                               where (kp2.MaKP == _MaKP1 || kp2.MaKP == _MaKP2 || kp2.MaKP == _MaKP3 || kp2.MaKP == _MaKP4 || kp2.MaKP == _MaKP5 || kp2.MaKP == _MaKP6 || kp2.MaKP == _MaKP7 || kp2.MaKP == _MaKP8 || kp2.MaKP == _MaKP9 || kp2.MaKP == _MaKP10 || kp2.MaKP == _MaKP11 || kp2.MaKP == _MaKP12 || kp2.MaKP == _MaKP13 || kp2.MaKP == _MaKP14 || kp2.MaKP == _MaKP15 || kp2.MaKP == _MaKP16
                                  || kp2.MaKP == _MaKP17 || kp2.MaKP == _MaKP18 || kp2.MaKP == _MaKP19 || kp2.MaKP == _MaKP20)
                               group new { nd, ndct, dv } by new { ndct.MaDV, dv.TenDV, ndct.DonGia } into kq
                               select new
                               {
                                   madv = kq.Key.MaDV,
                                   kq.Key.DonGia,
                                   TenDV = kq.Key.TenDV,
                                   //SoLuongSD = kq.Sum(p => p.ndct.SoLuongX) - kq.Sum(p => p.ndct.SoLuongSD),
                                   SoLuongSD = kq.Sum(p => p.ndct.SoLuongX) - kq.Sum(p => p.ndct.SoLuongSD) - kq.Sum(p => p.ndct.SoLuongN),
                               }).OrderBy(p => p.TenDV).ToList();
                if (qsltong.Count > 0)
                {
                    foreach (var a in _SDDuoc)
                    {
                        foreach (var b in qsltong)
                        {
                            if (a.madv == b.madv && a.DonGia == b.DonGia)
                            {
                                if (b.SoLuongSD != null && b.SoLuongSD != 0)
                                {
                                    a.tc = b.SoLuongSD.ToString();
                                    a.tctt = (b.DonGia * b.SoLuongSD).ToString();
                                }
                            }
                        }
                    }
                }
                frmIn frm = new frmIn();
                BaoCao.Rep_BcTonDuoc_BG rep = new BaoCao.Rep_BcTonDuoc_BG();
                if (DungChung.Bien.MaBV == "27022")
                {
                    rep.xrTableCell38.Text = "Phó trưởng khoa phòng khám_dược_CLS";
                }
                rep.TuNgayDenNgay.Value = "Đến ngày" + dateTuNgay.Text;
                var kp = from k in data.KPhongs.Where(p => p.MaKP == _makpx) select new { k.TenKP };
                //rep.TenBC.Value = ("bảng kê xuất " + cmbPL.Text + " tại " + kp.First().TenKP).ToUpper();
                if (cmbPL.Text == "Tất cả")
                {
                    rep.NhomDV.Value = 0;
                    rep.TenBC.Value = ("bảng kê tồn dược tại " + kp.First().TenKP).ToUpper();
                    rep.TenDuoc.Value = "Tên dược";
                }
                else
                {
                    rep.TenBC.Value = ("bảng kê tồn " + cmbPL.Text + " tại " + kp.First().TenKP).ToUpper();
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
                        case 16:
                            if (_DSKP.Skip(i).First().tenkp != null)
                            { rep.KP17.Value = _DSKP.Skip(i).First().tenkp; }
                            break;
                        case 17:
                            if (_DSKP.Skip(i).First().tenkp != null)
                            { rep.KP18.Value = _DSKP.Skip(i).First().tenkp; }
                            break;
                        case 18:
                            if (_DSKP.Skip(i).First().tenkp != null)
                            { rep.KP19.Value = _DSKP.Skip(i).First().tenkp; }
                            break;
                        case 19:
                            if (_DSKP.Skip(i).First().tenkp != null)
                            { rep.KP20.Value = _DSKP.Skip(i).First().tenkp; }
                            break;
                    }
                }
                if (_SDDuoc.Count > 0)
                {
                    rep.DataSource = _SDDuoc.Where(p => p.tc != null).OrderBy(p => p.tendv);
                }
                else MessageBox.Show("Không có dữ liệu");
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

        private void cklNhomDV_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            List<int> _idNhomDV = new List<int>();
            for (int i = 0; i < cklNhomDV.ItemCount; i++)
            {
                if (cklNhomDV.GetItemCheckState(i) == CheckState.Checked)
                    _idNhomDV.Add(Convert.ToInt32(cklNhomDV.GetItemValue(i)));
            }
            var _ltn = (from nh in _idNhomDV
                        join tn in _listTieuNhom on nh equals tn.IDNhom
                        select tn).ToList();
            ckcTieuNhomDV.DataSource = _ltn;
        }

        private void cmbPL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPL_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbPL.Text == "Thuốc" || cmbPL.Text == "Tất cả")
            {
                cklNhomDV.DataSource = _listNhomDV;
                cklNhomDV.Enabled = true;
                ckcTieuNhomDV.Enabled = true;
            }
            else
            {
                cklNhomDV.DataSource = null;
                cklNhomDV.Enabled = false;
                ckcTieuNhomDV.Enabled = false;
            }
        }

        private void chkAllNhom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllNhom.Checked)
                cklNhomDV.CheckAll();
            else
                cklNhomDV.UnCheckAll();
        }

        private void chkAllTieuNhom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllTieuNhom.Checked)
                ckcTieuNhomDV.CheckAll();
            else
                ckcTieuNhomDV.UnCheckAll();
        }
    }
}