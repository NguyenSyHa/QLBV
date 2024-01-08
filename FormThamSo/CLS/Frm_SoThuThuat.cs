using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.Providers.StoredProcedure;
using QLBV.Models.Business.SoThuThuat;

namespace QLBV.FormThamSo
{
    public partial class Frm_SoThuThuat : DevExpress.XtraEditors.XtraForm
    {
        private ExcuteStoredProcedureProvider _excuteStoredProcedureProvider;
        public Frm_SoThuThuat()
        {
            _excuteStoredProcedureProvider = new ExcuteStoredProcedureProvider();
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
        private void Frm_SoThuThuat_Load(object sender, EventArgs e)
        {
            _Kphong.Clear();
            lupTuNgay.Focus();
            lupDichVu.EditValue = 0;
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
            chk_MauMoi.Checked = true;
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
                       join tn in data.TieuNhomDVs.Where(p => p.TenRG == "Thủ thuật") on dv.IdTieuNhom equals tn.IdTieuNhom
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
        List<KP> _lkp = new List<KP>();
        private class BNmoi
        {
            private string TenBNhan;
            private int MaBNhan;
            private int GTinh;
            private string Tuoinam;
            private string tuoinu;
            private string DiaChi;
            private string DTuong;
            private string Chandoan;
            private string ChanDoanSauPT;
            private string PPTT;
            private int MaKP;
            private string PPVC;
            private string KetQua;
            private string BSCD;
            private string KTV;
            private string NgayBatDau;
            private string NgayKetThuc;
            private string LoaiTT;
            private string GhiChu;

            public string tenbnhan
            { set { TenBNhan = value; } get { return TenBNhan; } }
            public int mabnhan
            { set { MaBNhan = value; } get { return MaBNhan; } }
            public int gtinh
            { set { GTinh = value; } get { return GTinh; } }
            public string TuoiNam
            { set { Tuoinam = value; } get { return Tuoinam; } }
            public string TuoiNu
            { set { tuoinu = value; } get { return tuoinu; } }
            public string diachi
            { set { DiaChi = value; } get { return DiaChi; } }
            public string dtuong
            { set { DTuong = value; } get { return DTuong; } }
            public string chandoan
            { set { Chandoan = value; } get { return Chandoan; } }
            public string sauchandoan
            { set { ChanDoanSauPT = value; } get { return ChanDoanSauPT; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string pptt
            { set { PPTT = value; } get { return PPTT; } }
            public string ppvc
            { set { PPVC = value; } get { return PPVC; } }
            public string ketqua
            { set { KetQua = value; } get { return KetQua; } }
            public string bscd
            { set { BSCD = value; } get { return BSCD; } }
            public string ngaybatdau
            { set { NgayBatDau = value; } get { return NgayBatDau; } }
            public string ngayketthuc
            { set { NgayKetThuc = value; } get { return NgayKetThuc; } }
            public string loaitt
            { set { LoaiTT = value; } get { return LoaiTT; } }
            public string ktv
            { set { KTV = value; } get { return KTV; } }
            public string ghichu
            { set { GhiChu = value; } get { return GhiChu; } }
        }
        List<BNmoi> _BNmoi = new List<BNmoi>();

        public class KP
        {
            private int makp;
            private string tenkp;
            public int MaKP
            {
                set { makp = value; }
                get { return makp; }
            }
            public string TenKP
            {
                set { tenkp = value; }
                get { return tenkp; }
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
                _BNmoi.Clear();
                tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                string chuoi = "";
                string tenkp = "";
                //foreach (var item in _Kphong)
                //{
                //    chuoi += item.makp.ToString() + ";";
                //}

                // lấy mã kp
                _lkp.Clear();
                for (int k = 0; k < grvKhoaphong.RowCount; k++)
                {
                    if (grvKhoaphong.GetRowCellValue(k, Chọn).ToString().ToLower() == "true")
                    {
                        _lkp.Add(new KP 
                        { 
                            MaKP = grvKhoaphong.GetRowCellValue(k, MaKP) == null ? 0 : Convert.ToInt32(grvKhoaphong.GetRowCellValue(k, MaKP)), 
                            TenKP = grvKhoaphong.GetRowCellValue(k, TenKP) != null ? grvKhoaphong.GetRowCellValue(k, TenKP).ToString() : "" 
                        });
                    }
                }
                if (_Kphong.Count == _lkp.Count)
                {
                    chuoi = "0";
                    tenkp = "Tất cả";
                }
                else
                {
                    foreach (var item in _lkp)
                    {
                        chuoi += item.MaKP.ToString() + ";";
                        tenkp += item.TenKP + " - ";
                    }
                }
                if (lupDichVu.EditValue == null || Convert.ToInt32(lupDichVu.EditValue) == 0)
                {
                    // var a1 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay) group kb by kb.MaBNhan into kq select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();
                    #region Mẫu mới
                    if (chk_MauMoi.Checked == true)
                    {
                        int status = 0;
                        string makp = "";
                        switch (cboTKBN.SelectedIndex)
                        {
                            case 0:
                                status = 0;
                                break;
                            case 1:
                                status = 3;
                                break;
                            case 2:
                                status = 1;
                                break;
                        }
                        string spName = "sp_sothuthuat";
                        Dictionary<string, string> para = new Dictionary<string, string>()
                        {
                            {"@tungay",tungay.Date.ToString("yyyy/MM/dd")},
                            {"@denngay",denngay.Date.ToString("yyyy/MM/dd")},
                            {"@status",status.ToString()},
                            {"@madv", lupDichVu.EditValue.ToString()},
                            {"@makp",chuoi},
                        };
                        var lstBC = _excuteStoredProcedureProvider.ExcuteStoredProcedure<SoThuThuatModel>(spName, para);

                        if (lstBC.Count > 0)
                        {
                            if (radMau.SelectedIndex == 0)
                            {
                                BaoCao.Rep_SoThuThuat_A4 rep = new BaoCao.Rep_SoThuThuat_A4();
                                rep.SubMoi.Visible = true;
                                rep.Sub_detail_moi.Visible = true;
                                rep.xrPanel1.Visible = false;
                                rep.Parameters["CQ"].Value = "BỆNH VIỆN: " + DungChung.Bien.TenCQ;
                                rep.Parameters["Khoa1"].Value ="KHOA: " + tenkp;
                                rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                rep.DataSource = lstBC.ToList();
                                var a = lstBC.Where(p => p.PhanLoaiTT != null).ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            if (radMau.SelectedIndex == 1)
                            {
                                BaoCao.Rep_SoThuThuat rep = new BaoCao.Rep_SoThuThuat();
                                rep.sub_A3_Moi.Visible = true;
                                rep.sub_detail_moi.Visible = true;
                                rep.xrPanel1.Visible = false;
                                rep.Parameters["CQ"].Value = "BỆNH VIỆN: " + DungChung.Bien.TenCQ;
                                rep.Parameters["Khoa1"].Value = "KHOA: " + tenkp;
                                rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                rep.Nam.Value = _BNmoi.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count() == 0 ? null : (_BNmoi.Where(p => p.gtinh == 1).Select(p => p.mabnhan).Count()).ToString();
                                rep.Nu.Value = _BNmoi.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count() == 0 ? null : (_BNmoi.Where(p => p.gtinh == 0).Select(p => p.mabnhan).Count()).ToString();
                                rep.BHYT.Value = _BNmoi.Where(p => p.dtuong == "BHYT").Select(p => p.mabnhan).Count() == 0 ? null : (_BNmoi.Where(p => p.dtuong == "BHYT").Select(p => p.mabnhan).Count()).ToString();
                                rep.DataSource = lstBC.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        else MessageBox.Show("Không có dữ liệu để in báo cáo ");
                    }
                    #endregion
                    #region Mẫu cũ
                    if (chk_MauMoi.Checked == false)
                    {
                        if (cboTKBN.Text == "Tất cả BN thực hiện TT")
                        {
                            //foreach (var b in a1)
                            //{
                            var qso = (from ma in _lkp
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on ma.MaKP equals dtct.MaKP
                                       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Thủ thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       group new { bn, dt, dtct, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.TenDV, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap } into kq
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
                        }
                        if (cboTKBN.Text == "Chỉ BN đã thanh toán")
                        {

                            //foreach (var b in a1)
                            //{
                            var qbntt = (from ma in _lkp
                                         join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on ma.MaKP equals dtct.MaKP
                                         join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                         join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                                         join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                         join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                         join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                         join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Thủ thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                         group new { bn, dv, vp, dtct } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.TenDV, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap } into kq
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

                        }

                        if (cboTKBN.Text == "Chỉ BN chưa thanh toán")
                        {

                            //foreach (var b in a1)
                            //{
                            var qso = (from ma in _lkp
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on ma.MaKP equals dtct.MaKP
                                       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                       join makp in _lkp on kp.MaKP equals makp.MaKP
                                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Thủ thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       where !(from vp in data.VienPhis select vp.MaBNhan).Contains(dt.MaBNhan)
                                       group new { bn, dt, dtct, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.TenDV, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap } into kq
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

                        }


                        if (_BN.Count > 0)
                        {
                            if (radMau.SelectedIndex == 0)
                            {
                                BaoCao.Rep_SoThuThuat_A4 rep = new BaoCao.Rep_SoThuThuat_A4();
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
                                BaoCao.Rep_SoThuThuat rep = new BaoCao.Rep_SoThuThuat();
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
                    #endregion
                }
                else
                {
                    int madv = lupDichVu.EditValue == null ? 0 : Convert.ToInt32(lupDichVu.EditValue);

                    //var a1 = (from kb in data.BNKBs.Where(p => p.NgayKham >= tungay).Where(p => p.NgayKham <= denngay) group kb by kb.MaBNhan into kq select new { kq.Key, IDKB = kq.Max(p => p.IDKB) }).ToList();

                    #region Mẫu mới
                    if (chk_MauMoi.Checked == true)
                    {
                        int status = 0;
                        switch (cboTKBN.SelectedIndex)
                        {
                            case 0:
                                status = 0;
                            break;
                            case 1:
                                status = 3;
                            break;
                            case 2:
                                status = 1;
                            break;
                        }
                        string spName = "sp_sothuthuat";
                        Dictionary<string, string> para = new Dictionary<string, string>()
                        {
                            {"@tungay",tungay.Date.ToString("yyyy/MM/dd")},
                            {"@denngay",denngay.Date.ToString("yyyy/MM/dd")},
                            {"@status",status.ToString()},
                            {"@madv", lupDichVu.EditValue.ToString()},
                            {"@makp",chuoi},
                        };
                        var lstBC = _excuteStoredProcedureProvider.ExcuteStoredProcedure<SoThuThuatModel>(spName, para);
                        if (lstBC.Count > 0)
                        {
                            if (radMau.SelectedIndex == 0)
                            {
                                BaoCao.Rep_SoThuThuat_A4 rep = new BaoCao.Rep_SoThuThuat_A4();
                                rep.SubMoi.Visible = true;
                                rep.Sub_detail_moi.Visible = true;
                                rep.xrPanel1.Visible = false;
                                rep.Parameters["CQ"].Value = "BỆNH VIỆN: " + DungChung.Bien.TenCQ;
                                rep.Parameters["Khoa1"].Value = "KHOA: " + tenkp;
                                rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                rep.DataSource = lstBC.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            if (radMau.SelectedIndex == 1)
                            {
                                BaoCao.Rep_SoThuThuat rep = new BaoCao.Rep_SoThuThuat();
                                rep.sub_A3_Moi.Visible = true;
                                rep.sub_detail_moi.Visible = true;
                                rep.xrPanel1.Visible = false;
                                rep.Parameters["CQ"].Value = "BỆNH VIỆN: " + DungChung.Bien.TenCQ;
                                rep.Parameters["Khoa1"].Value = "KHOA: " + tenkp;
                                rep.TuNgay.Value = tungay.ToString().Substring(0, 10);
                                rep.DenNgay.Value = denngay.ToString().Substring(0, 10);
                                rep.DataSource = lstBC.ToList();
                                rep.BindingData();
                                rep.CreateDocument();
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        else MessageBox.Show("Không có dữ liệu để in báo cáo ");
                    }
                    #endregion
                    #region Mẫu cũ
                    if (chk_MauMoi.Checked == false)
                    {
                        if (cboTKBN.Text == "Tất cả BN thực hiện TT")
                        {
                            //foreach (var b in a1)
                            //{
                            var qso = (from ma in _lkp
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on ma.MaKP equals dtct.MaKP
                                       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Thủ thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       group new { bn, dt, dtct, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.TenDV, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap } into kq
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
                        }
                        if (cboTKBN.Text == "Chỉ BN đã thanh toán")
                        {

                            //foreach (var b in a1)
                            //{
                            var qbntt = (from ma in _lkp
                                         join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on ma.MaKP equals dtct.MaKP
                                         join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                         join vp in data.VienPhis on dt.MaBNhan equals vp.MaBNhan
                                         join bn in data.BenhNhans on vp.MaBNhan equals bn.MaBNhan
                                         join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                         join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                         join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Thủ thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                         group new { bn, dv, vp, dtct } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.TenDV, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap } into kq
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

                        }

                        if (cboTKBN.Text == "Chỉ BN chưa thanh toán")
                        {

                            //foreach (var b in a1)
                            //{
                            var qso = (from ma in _lkp
                                       join dtct in data.DThuoccts.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay) on ma.MaKP equals dtct.MaKP
                                       join dt in data.DThuocs on dtct.IDDon equals dt.IDDon
                                       join bn in data.BenhNhans on dt.MaBNhan equals bn.MaBNhan
                                       join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                                       join makp in _lkp on kp.MaKP equals makp.MaKP
                                       join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                                       join tnhom in data.TieuNhomDVs.Where(p => p.TenRG.Contains("Thủ thuật")) on dv.IdTieuNhom equals tnhom.IdTieuNhom
                                       where !(from vp in data.VienPhis select vp.MaBNhan).Contains(dt.MaBNhan)
                                       group new { bn, dt, dtct, dv } by new { bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.GTinh, bn.Tuoi, bn.DTuong, dv.TenDV, kp.MaKP, kp.TenKP, dtct.MaCB, dtct.NgayNhap } into kq
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

                        }


                        if (_BN.Count > 0)
                        {
                            if (radMau.SelectedIndex == 0)
                            {
                                BaoCao.Rep_SoThuThuat_A4 rep = new BaoCao.Rep_SoThuThuat_A4();
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
                                BaoCao.Rep_SoThuThuat rep = new BaoCao.Rep_SoThuThuat();
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
                    #endregion
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