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
    public partial class Frm_SoNoiSoi : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SoNoiSoi()
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
            public int madv
            {
                set { MaDV = value; }
                get { return MaDV; }
            }
            public string tendv
            { set { TenDV = value; } get { return TenDV; } }
        }
        List<KPhong> _Kphong = new List<KPhong>();
        List<DV> _ldv = new List<DV>();
        List<DichVu> ldv = new List<DichVu>();
        private void Frm_SoNoiSoi_Load(object sender, EventArgs e)
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
            var _ltn = (from tn in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Nội soi"))
                        select tn).ToList();
            //_ltn.Add(new TieuNhomDV { IdTieuNhom = -1, TenTN = "Tất cả" });
            ckcTieuNhomDV.DisplayMember = "TenTN";
            ckcTieuNhomDV.ValueMember = "IdTieuNhom";
            ckcTieuNhomDV.DataSource = _ltn;
            ckcTieuNhomDV_ItemCheck(null, null);
            ldv = data.DichVus.Where(p => p.PLoai == 2).ToList();

            if(DungChung.Bien.MaBV != "27183")
            {
                lblDTuong.Visible = cboDTuong.Visible = false;
                ckcTieuNhomDV.Size = new System.Drawing.Size(306, 91);
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
            private int NoiTru;
            private string ChanDoan;
            private int MaKP;
            private string NoiGui;
            private string YeuCau;
            private string KetQua;
            private string BSTH;
            private string NgayTH;
            public string tenbnhan
            { set { TenBNhan = value; } get { return TenBNhan; } }
            public int mabnhan
            { set { MaBNhan = value; } get { return MaBNhan; } }
            public int gtinh
            { set { GTinh = value; } get { return GTinh; } }
            public int noitru
            { set { NoiTru = value; } get { return NoiTru; } }
            public int tuoi
            { set { Tuoi = value; } get { return Tuoi; } }
            public string diachi
            { set { DiaChi = value; } get { return DiaChi; } }
            public string dtuong
            { set { DTuong = value; } get { return DTuong; } }
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
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            List<KPhong> _lKhoaP = new List<KPhong>();

            if (KTtaoBc())
            {
                _BN.Clear(); _lKhoaP.Clear();
                List<int> _idTieuNhomDV = new List<int>();
                for (int i = 0; i < ckcTieuNhomDV.ItemCount; i++)
                {
                    if (ckcTieuNhomDV.GetItemCheckState(i) == CheckState.Checked)
                        _idTieuNhomDV.Add(Convert.ToInt32(ckcTieuNhomDV.GetItemValue(i)));
                }
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);

                _lKhoaP = _Kphong.Where(p => p.makp != 0).Where(p => p.chon == true).ToList();

                var _ldv = (from dv in data.DichVus.Where(p => p.PLoai == 2)
                            join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new { dv.MaDV, dv.TenDV, tn.IdTieuNhom, tn.TenRG, tn.TenTN }).ToList();

                if (lupDichVu.EditValue == null || Convert.ToInt32(lupDichVu.EditValue) == 0)
                {

                    if (cboTKBN.EditValue == "Tất cả BN thực hiện NS")
                    {
                        var _lcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                     join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                     join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                     join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                     where DungChung.Bien.MaBV == "27183" ? (cboDTuong.SelectedIndex == 0 ? true : bn.DTuong == cboDTuong.Text) : true
                                     select new { bn.MaBNhan, bn.TenBNhan, clsct.KetQua, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, cd.KetLuan, cls.ChanDoan, cls.MaCBth, cls.NgayTH, cd.MaDV, cls.MaKP }).ToList();

                        var qso = (from kp in _lKhoaP
                                   join cls in _lcls on kp.makp equals cls.MaKP
                                   join dv in _ldv.Where(p => _idTieuNhomDV.Contains(p.IdTieuNhom)) on cls.MaDV equals dv.MaDV
                                   //join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on kp.makp equals cls.MaKP
                                   //join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                   //join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                   //join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                   //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                   //join tnhom in data.TieuNhomDVs.Where(p => _idTieuNhomDV.Contains(p.IdTieuNhom)) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                   group new { kp, cls, dv } by new { cls.KetQua, cls.MaBNhan, cls.TenBNhan, cls.DChi, cls.GTinh, cls.Tuoi, cls.DTuong, dv.TenDV, cls.KetLuan, cls.ChanDoan, kp.makp, kp.tenkp, cls.MaCBth, cls.NgayTH } into kq
                                   select new
                                   {
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenBNhan = kq.Key.TenBNhan,
                                       GTinh = kq.Key.GTinh,
                                       Tuoi = kq.Key.Tuoi,
                                       DTuong = kq.Key.DTuong,
                                       DiaChi = kq.Key.DChi,
                                       TenDV = kq.Key.TenDV,
                                       KetQua =  kq.Key.KetLuan,//kq.Key.KetQua,
                                       MakP = kq.Key.makp,
                                       TenKP = kq.Key.tenkp,
                                       BSTH = kq.Key.MaCBth,
                                       NgayTH = kq.Key.NgayTH,
                                       ChanDoan = kq.Key.ChanDoan
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
                                themmoi.ketqua = a.KetQua;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                themmoi.bsth = a.BSTH;
                                themmoi.chandoan = a.ChanDoan;
                                themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                _BN.Add(themmoi);
                            }

                        }

                    }
                    if (cboTKBN.EditValue == "Chỉ BN đã thanh toán")
                    {
                        var _lcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                     join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                     join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                     join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                     join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                     where DungChung.Bien.MaBV == "27183" ? (cboDTuong.SelectedIndex == 0 ? true : bn.DTuong == cboDTuong.Text) : true
                                     select new { bn.MaBNhan, bn.TenBNhan, clsct.KetQua, bn.DChi, bn.GTinh, bn.Tuoi, bn.NoiTru, bn.DTuong, cd.KetLuan, cls.ChanDoan, cls.MaCBth, cls.NgayTH, cd.MaDV, cls.MaKP }).ToList();

                        var qbntt = (from kp in _lKhoaP
                                     join cls in _lcls on kp.makp equals cls.MaKP
                                     join dv in _ldv.Where(p => _idTieuNhomDV.Contains(p.IdTieuNhom)) on cls.MaDV equals dv.MaDV
                                     //join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on kp.makp equals cls.MaKP
                                     //join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                     //join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                     //join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                     //join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                     //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                     //join tnhom in data.TieuNhomDVs.Where(p => _idTieuNhomDV.Contains(p.IdTieuNhom)) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                     group new { kp, cls, dv } by new { cls.KetQua, cls.MaBNhan, cls.TenBNhan, cls.DChi, cls.GTinh, cls.NoiTru, cls.Tuoi, cls.DTuong, dv.TenDV, kp.makp, kp.tenkp, cls.MaCBth, cls.ChanDoan, cls.NgayTH, cls.KetLuan } into kq
                                     select new
                                     {
                                         MaBNhan = kq.Key.MaBNhan,
                                         TenBNhan = kq.Key.TenBNhan,
                                         GTinh = kq.Key.GTinh,
                                         Tuoi = kq.Key.Tuoi,
                                         DTuong = kq.Key.DTuong,
                                         NoiTru = kq.Key.NoiTru,
                                         DiaChi = kq.Key.DChi,
                                         TenDV = kq.Key.TenDV,
                                         KetQua = DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "27023" ? kq.Key.KetLuan : kq.Key.KetQua,//kq.Key.KetLuan,
                                         MakP = kq.Key.makp,
                                         TenKP = kq.Key.tenkp,
                                         BSTH = kq.Key.MaCBth,
                                         NgayTH = kq.Key.NgayTH,
                                         kq.Key.ChanDoan
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
                                themmoi.noitru = Convert.ToInt32(a.NoiTru.ToString());
                                themmoi.dtuong = a.DTuong;
                                themmoi.diachi = a.DiaChi;
                                themmoi.yeucau = a.TenDV;
                                themmoi.ketqua = a.KetQua;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                themmoi.bsth = a.BSTH;
                                themmoi.chandoan = a.ChanDoan;
                                themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                _BN.Add(themmoi);
                            }

                        }

                    }

                    if (cboTKBN.EditValue == "Chỉ BN chưa thanh toán")
                    {
                        var _lcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                     join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                     join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                     join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                     where !(from vp in data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan) && DungChung.Bien.MaBV == "27183" ? (cboDTuong.SelectedIndex == 0 ? true : bn.DTuong == cboDTuong.Text) : true
                                     select new { bn.MaBNhan, bn.TenBNhan, clsct.KetQua, bn.DChi, bn.GTinh, bn.Tuoi, bn.NoiTru, bn.DTuong, cd.KetLuan, cls.ChanDoan, cls.MaCBth, cls.NgayTH, cd.MaDV, cls.MaKP }).ToList();

                        var qso = (from kp in _lKhoaP
                                   join cls in _lcls on kp.makp equals cls.MaKP
                                   join dv in _ldv.Where(p => _idTieuNhomDV.Contains(p.IdTieuNhom)) on cls.MaDV equals dv.MaDV
                                   //join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on kp.makp equals cls.MaKP
                                   //join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                   //join cd in data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                   //join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                   //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                   //where !(from vp in data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
                                   //join tnhom in data.TieuNhomDVs.Where(p => _idTieuNhomDV.Contains(p.IdTieuNhom)) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                   group new { kp, cls, dv } by new { cls.MaBNhan, cls.KetQua, cls.TenBNhan, cls.DChi, cls.GTinh, cls.NoiTru, cls.Tuoi, cls.DTuong, dv.TenDV, cls.KetLuan, kp.makp, kp.tenkp, cls.MaCBth, cls.NgayTH, cls.ChanDoan } into kq
                                   select new
                                   {
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenBNhan = kq.Key.TenBNhan,
                                       GTinh = kq.Key.GTinh,
                                       Tuoi = kq.Key.Tuoi,
                                       DTuong = kq.Key.DTuong,
                                       NoiTru = kq.Key.NoiTru,
                                       DiaChi = kq.Key.DChi,
                                       TenDV = kq.Key.TenDV,
                                       KetQua = DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "27023" ? kq.Key.KetLuan : kq.Key.KetQua,//kq.Key.KetLuan,
                                       MakP = kq.Key.makp, 
                                       TenKP = kq.Key.tenkp,
                                       BSTH = kq.Key.MaCBth,
                                       NgayTH = kq.Key.NgayTH,
                                       kq.Key.ChanDoan
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
                                themmoi.noitru = Convert.ToInt32(a.NoiTru.ToString());
                                themmoi.dtuong = a.DTuong;
                                themmoi.diachi = a.DiaChi;
                                themmoi.yeucau = a.TenDV;
                                themmoi.ketqua = a.KetQua;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                themmoi.bsth = a.BSTH;
                                themmoi.chandoan = a.ChanDoan;
                                themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                _BN.Add(themmoi);
                            }

                        }

                    }
                    if (_BN.Count > 0)
                    {
                        if (radBN.SelectedIndex == 0)
                        {
                            if (radMau.SelectedIndex == 0)
                            {
                                if (DungChung.Bien.MaBV == "01049")
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_A4_01049 rep = new BaoCao.Rep_SoNoiSoi_A4_01049();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_A4 rep = new BaoCao.Rep_SoNoiSoi_A4();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                            if (radMau.SelectedIndex == 1)
                            {
                                if (DungChung.Bien.MaBV == "01049")
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_01049 rep = new BaoCao.Rep_SoNoiSoi_01049();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi rep = new BaoCao.Rep_SoNoiSoi();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                        }
                        if (radBN.SelectedIndex != 0)
                        {
                            int _bn = -1;
                            {
                                if (radBN.SelectedIndex == 1) { _bn = 1; }
                                if (radBN.SelectedIndex == 2) { _bn = 0; }
                            }
                            if (radMau.SelectedIndex == 0)
                            {
                                if (DungChung.Bien.MaBV == "01049")
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_A4_01049 rep = new BaoCao.Rep_SoNoiSoi_A4_01049();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.Where(p => p.noitru == _bn).OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_A4 rep = new BaoCao.Rep_SoNoiSoi_A4();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.Where(p => p.noitru == _bn).OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                            if (radMau.SelectedIndex == 1)
                            {
                                if (DungChung.Bien.MaBV == "01049")
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_01049 rep = new BaoCao.Rep_SoNoiSoi_01049();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.Where(p => p.noitru == _bn).OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi rep = new BaoCao.Rep_SoNoiSoi();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.Where(p => p.noitru == _bn).OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                        }

                    }
                    else MessageBox.Show("Không có dữ liệu để in báo cáo ");


                }
                else
                {
                    int madv = lupDichVu.EditValue == null ? 0 : Convert.ToInt32(lupDichVu.EditValue);
                    if (cboTKBN.EditValue == "Tất cả BN thực hiện NS")
                    {
                        var _lcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                     join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                     join cd in data.ChiDinhs.Where(p => p.MaDV == madv).Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                     join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                     select new { bn.MaBNhan, bn.TenBNhan, clsct.KetQua, bn.DChi, bn.GTinh, bn.Tuoi, bn.NoiTru, bn.DTuong, cd.KetLuan, cls.ChanDoan, cls.MaCBth, cls.NgayTH, cd.MaDV, cls.MaKP }).ToList();

                        var qso = (from kp in _lKhoaP
                                   join cls in _lcls on kp.makp equals cls.MaKP
                                   join dv in _ldv.Where(p => p.TenRG.Contains("Nội soi")) on cls.MaDV equals dv.MaDV
                                   //join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on kp.makp equals cls.MaKP
                                   //join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                   //join cd in data.ChiDinhs.Where(p => p.MaDV == madv).Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                   //join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                   //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                   //join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Nội soi")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                   group new { cls, dv, kp } by new { cls.MaBNhan, cls.KetQua, cls.TenBNhan, cls.DChi, cls.NoiTru, cls.GTinh, cls.Tuoi, cls.DTuong, dv.TenDV, cls.KetLuan, kp.makp, kp.tenkp, cls.MaCBth, cls.NgayTH, cls.ChanDoan } into kq
                                   select new
                                   {
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenBNhan = kq.Key.TenBNhan,
                                       GTinh = kq.Key.GTinh,
                                       Tuoi = kq.Key.Tuoi,
                                       DTuong = kq.Key.DTuong,
                                       NoiTru = kq.Key.NoiTru,
                                       DiaChi = kq.Key.DChi,
                                       TenDV = kq.Key.TenDV,
                                       KetQua = DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "27023" ? kq.Key.KetLuan : kq.Key.KetQua,//kq.Key.KetLuan,
                                       MakP = kq.Key.makp,
                                       TenKP = kq.Key.tenkp,
                                       BSTH = kq.Key.MaCBth,
                                       NgayTH = kq.Key.NgayTH,
                                       kq.Key.ChanDoan
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
                                themmoi.noitru = Convert.ToInt32(a.NoiTru.ToString());
                                themmoi.dtuong = a.DTuong;
                                themmoi.diachi = a.DiaChi;
                                themmoi.yeucau = a.TenDV;
                                themmoi.ketqua = a.KetQua;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                themmoi.bsth = a.BSTH;
                                themmoi.chandoan = a.ChanDoan;
                                themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                _BN.Add(themmoi);
                            }

                        }
                    }
                    if (cboTKBN.EditValue == "Chỉ BN đã thanh toán")
                    {
                        var _lcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                     join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                     join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                     join cd in data.ChiDinhs.Where(p => p.MaDV == madv).Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                     join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                     select new { bn.MaBNhan, bn.TenBNhan, clsct.KetQua, bn.DChi, bn.GTinh, bn.Tuoi, bn.NoiTru, bn.DTuong, cd.KetLuan, cls.ChanDoan, cls.MaCBth, cls.NgayTH, cd.MaDV, cls.MaKP }).ToList();

                        var qbntt = (from kp in _lKhoaP
                                     join cls in _lcls on kp.makp equals cls.MaKP
                                     join dv in _ldv.Where(p => p.TenRG.Contains("Nội soi")) on cls.MaDV equals dv.MaDV
                                     //join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on kp.makp equals cls.MaKP
                                     //join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                     //join vp in data.VienPhis on bn.MaBNhan equals vp.MaBNhan
                                     //join cd in data.ChiDinhs.Where(p => p.MaDV == madv).Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                     //join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                     //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                     //join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Nội soi")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                     group new { kp, cls, dv } by new { cls.MaBNhan, cls.KetQua, cls.TenBNhan, cls.DChi, cls.NoiTru, cls.GTinh, cls.Tuoi, cls.DTuong, dv.TenDV, kp.makp, kp.tenkp, cls.MaCBth, cls.NgayTH, cls.KetLuan, cls.ChanDoan } into kq
                                     select new
                                     {
                                         MaBNhan = kq.Key.MaBNhan,
                                         TenBNhan = kq.Key.TenBNhan,
                                         GTinh = kq.Key.GTinh,
                                         Tuoi = kq.Key.Tuoi,
                                         DTuong = kq.Key.DTuong,
                                         NoiTru = kq.Key.NoiTru,
                                         DiaChi = kq.Key.DChi,
                                         TenDV = kq.Key.TenDV,
                                         KetQua = DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "27023" ? kq.Key.KetLuan : kq.Key.KetQua,//kq.Key.KetLuan,
                                         MakP = kq.Key.makp,
                                         TenKP = kq.Key.tenkp,
                                         BSTH = kq.Key.MaCBth,
                                         NgayTH = kq.Key.NgayTH,
                                         kq.Key.ChanDoan
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
                                themmoi.noitru = Convert.ToInt32(a.NoiTru.ToString());
                                themmoi.dtuong = a.DTuong;
                                themmoi.diachi = a.DiaChi;
                                themmoi.yeucau = a.TenDV;
                                themmoi.ketqua = a.KetQua;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                themmoi.bsth = a.BSTH;
                                themmoi.chandoan = a.ChanDoan;
                                themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                _BN.Add(themmoi);
                            }
                        }

                    }

                    if (cboTKBN.EditValue == "Chỉ BN chưa thanh toán")
                    {
                        var _lcls = (from cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay)
                                     join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                     join cd in data.ChiDinhs.Where(p => p.MaDV == madv).Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                     join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                     where !(from vp in data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
                                     select new { bn.MaBNhan, bn.TenBNhan, clsct.KetQua, bn.DChi, bn.GTinh, bn.Tuoi, bn.NoiTru, bn.DTuong, cd.KetLuan, cls.ChanDoan, cls.MaCBth, cls.NgayTH, cd.MaDV, cls.MaKP }).ToList();

                        var qso = (from kp in _lKhoaP
                                   join cls in _lcls on kp.makp equals cls.MaKP
                                   join dv in _ldv.Where(p => p.TenRG.Contains("Nội soi")) on cls.MaDV equals dv.MaDV
                                   //join cls in data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay) on kp.makp equals cls.MaKP
                                   //join bn in data.BenhNhans on cls.MaBNhan equals bn.MaBNhan
                                   //join cd in data.ChiDinhs.Where(p => p.MaDV == madv).Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                                   //join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                   //join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                   //where !(from vp in data.VienPhis select vp.MaBNhan).Contains(cls.MaBNhan)
                                   //join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Nội soi")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                   group new { kp, cls, dv } by new { cls.MaBNhan, cls.KetQua, cls.TenBNhan, cls.NoiTru, cls.DChi, cls.GTinh, cls.Tuoi, cls.DTuong, dv.TenDV, cls.KetLuan, kp.makp, kp.tenkp, cls.MaCBth, cls.NgayTH, cls.ChanDoan } into kq
                                   select new
                                   {
                                       MaBNhan = kq.Key.MaBNhan,
                                       TenBNhan = kq.Key.TenBNhan,
                                       GTinh = kq.Key.GTinh,
                                       Tuoi = kq.Key.Tuoi,
                                       DTuong = kq.Key.DTuong,
                                       NoiTru = kq.Key.NoiTru,
                                       DiaChi = kq.Key.DChi,
                                       TenDV = kq.Key.TenDV,
                                       KetQua = DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "27023" ? kq.Key.KetLuan : kq.Key.KetQua,// kq.Key.KetLuan,
                                       MakP = kq.Key.makp,
                                       TenKP = kq.Key.tenkp,
                                       BSTH = kq.Key.MaCBth,
                                       NgayTH = kq.Key.NgayTH,
                                       kq.Key.ChanDoan,
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
                                themmoi.noitru = Convert.ToInt32(a.NoiTru.ToString());
                                themmoi.dtuong = a.DTuong;
                                themmoi.diachi = a.DiaChi;
                                themmoi.yeucau = a.TenDV;
                                themmoi.ketqua = a.KetQua;
                                themmoi.makp = a.MakP;
                                themmoi.noigui = a.TenKP;
                                themmoi.bsth = a.BSTH;
                                themmoi.chandoan = a.ChanDoan;
                                themmoi.ngayth = a.NgayTH.ToString().Substring(0, 10);
                                _BN.Add(themmoi);
                            }

                        }
                    }
                    if (_BN.Count > 0)
                    {
                        if (radBN.SelectedIndex == 0)
                        {
                            if (radMau.SelectedIndex == 0)
                            {
                                if (DungChung.Bien.MaBV == "01049")
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_A4_01049 rep = new BaoCao.Rep_SoNoiSoi_A4_01049();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_A4 rep = new BaoCao.Rep_SoNoiSoi_A4();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                            if (radMau.SelectedIndex == 1)
                            {
                                if (DungChung.Bien.MaBV == "01049")
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_01049 rep = new BaoCao.Rep_SoNoiSoi_01049();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi rep = new BaoCao.Rep_SoNoiSoi();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                        }
                        if (radBN.SelectedIndex != 0)
                        {
                            int _bn = -1;
                            {
                                if (radBN.SelectedIndex == 1) { _bn = 1; }
                                if (radBN.SelectedIndex == 2) { _bn = 0; }
                            }
                            if (radMau.SelectedIndex == 0)
                            {
                                if (DungChung.Bien.MaBV == "01049")
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_A4_01049 rep = new BaoCao.Rep_SoNoiSoi_A4_01049();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.Where(p => p.noitru == _bn).OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_A4 rep = new BaoCao.Rep_SoNoiSoi_A4();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.Where(p => p.noitru == _bn).OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
                            if (radMau.SelectedIndex == 1)
                            {
                                if (DungChung.Bien.MaBV == "01049")
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi_01049 rep = new BaoCao.Rep_SoNoiSoi_01049();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.Where(p => p.noitru == _bn).OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    frmIn frm = new frmIn();
                                    BaoCao.Rep_SoNoiSoi rep = new BaoCao.Rep_SoNoiSoi();
                                    rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                    rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                    rep.BindingData();
                                    rep.DataSource = _BN.Where(p => p.noitru == _bn).OrderBy(p => p.mabnhan).ToList();
                                    rep.CreateDocument();
                                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                    frm.ShowDialog();
                                }
                            }
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

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void ckcTieuNhomDV_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            List<int> _idTieuNhomDV = new List<int>();
            for (int i = 0; i < ckcTieuNhomDV.ItemCount; i++)
            {
                if (ckcTieuNhomDV.GetItemCheckState(i) == CheckState.Checked)
                    _idTieuNhomDV.Add(Convert.ToInt32(ckcTieuNhomDV.GetItemValue(i)));
            }
            _ldv.Clear();
            var qdv = (from tn in _idTieuNhomDV
                       join dv in ldv on tn equals dv.IdTieuNhom
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
                lupDichVu.Properties.DataSource = null;
                lupDichVu.Properties.DataSource = _ldv.ToList();
            }
            else
                lupDichVu.Properties.DataSource = null;
        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckcTieuNhomDV.CheckAll();
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckcTieuNhomDV.UnCheckAll();
        }

        private void ckcTieuNhomDV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}