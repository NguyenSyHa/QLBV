using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_SoTHCLS : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoTHCLS()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        private class KPhongc
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
        List<KPhongc> _Kphong = new List<KPhongc>();
        List<DichVu> _ldvall = new List<DichVu>();
        List<KPhong> _lkpall = new List<KPhong>();
        List<CanBo> _lcb = new List<CanBo>();
        List<VienPhi> _lvp = new List<VienPhi>();
        List<TieuNhom> _ltnnew = new List<TieuNhom>();
        private void frm_SoTHCLS_Load(object sender, EventArgs e)
        {
            _ltnnew.Clear();
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            deTuNgay.Focus();
            deTuNgay.DateTime = System.DateTime.Now;
            deDenNgay.DateTime = System.DateTime.Now;
            rgDTuong.SelectedIndex = 3;
            radBN.SelectedIndex = 2;
            radDanhmuc.SelectedIndex = 2;
            radThanhtoan.SelectedIndex = 2;
            if (DungChung.Bien.MaBV != "24297")
            {
                radDanhmuc.Visible = false;
                radThanhtoan.Visible = false;
            }

            _lkpall = _data.KPhongs.Where(p => p.Status == 1).ToList();
            _ldvall = _data.DichVus.Where(p => p.Status == 1 && p.PLoai == 2).ToList();
            _lcb = _data.CanBoes.ToList();
            var kphong = (from kp in _lkpall
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám" || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh)
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhongc themmoi1 = new KPhongc();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhongc themmoi = new KPhongc();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
            var _ltn = _data.TieuNhomDVs.Where(p => p.Status == 1).Where(p => p.IDNhom == 1 || p.IDNhom == 2 || p.IDNhom == 3).Select(p => new { p.IdTieuNhom, p.TenTN, p.TenRG, p.IDNhom }).OrderBy(p => p.IdTieuNhom).ToList();

            foreach (var item in _ltn)
            {
                TieuNhom moi = new TieuNhom();
                moi.Ten = "Sổ " + item.TenTN.ToLower().ToString();
                moi.ID = item.IdTieuNhom;
                moi.TenRG = item.TenRG;
                moi.IDNhom = item.IDNhom ?? 0;
                _ltnnew.Add(moi);
            }
            lupChonSo.Properties.DataSource = _ltnnew;

            //rgLoaiSo.SelectedIndex = 0;
            //rgLoaiSo_SelectedIndexChanged(null, null);
        }
        public class TieuNhom
        {
            public int ID { get; set; }
            public string Ten { get; set; }
            public string TenRG { get; set; }
            public int IDNhom { get; set; }
        }
        public class BC
        {
            public DateTime NgayTH { get; set; }
            public string NgayTHCG { get; set; }
            public string TenBN { get; set; }
            public string Nam { get; set; }
            public string Nu { get; set; }
            public string DChi { get; set; }
            public string CoBH { get; set; }
            public string ChanDoan { get; set; }
            public string NoiGui { get; set; }
            public string Yeucau { get; set; }
            public string NguoiTH { get; set; }
            public string NguoiGui { get; set; }
            public string CoPhim { get; set; }
            public int SoLuongF { get; set; }
            public int PhimHong { get; set; }
            public string NgayTHT { get; set; }
            public string KetQua { get; set; }
            public int Mabn { get; set; }
            public string BSCD { get; set; }
            public string Phim13 { get; set; }
            public string Phim18 { get; set; }
            public string Phim24 { get; set; }
            public string Phim30 { get; set; }
            public string KetLuan { get; set; }
            public string MoTa { get; set; }
            public string KLTH { get; set; }
            public string Title { get; set; }
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

        private void btnInBC_Click(object sender, EventArgs e)
        {
            bool TaoBC = true;
            int _makp = 0;
            if (lupKpTH.EditValue != null)
            {
                _makp = Convert.ToInt32(lupKpTH.EditValue);
            }
            else
            {
                TaoBC = false;
                MessageBox.Show("Chưa chọn khoa phòng thực hiện");
            }
            List<KPhongc> _lKhoaP = new List<KPhongc>();
            List<BC> kqua = new List<BC>();
            _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
            if (_lKhoaP.Count() == 0)
            {
                TaoBC = false;
                MessageBox.Show("Chưa chọn khoa phòng chỉ định");
            }
            string tenrg = "", tenbc = ""; int idtieunhom = 0;
            TieuNhom tenrg1 = new TieuNhom();
            if (lupChonSo.EditValue != null)
            {
                idtieunhom = Convert.ToInt32(lupChonSo.EditValue);
                tenrg1 = _ltnnew.Where(p => p.ID == idtieunhom).FirstOrDefault();
                if (tenrg1 != null)
                {
                    tenrg = tenrg1.TenRG;
                    tenbc = tenrg1.Ten;
                }
            }
            else
            {
                TaoBC = false;
                MessageBox.Show("Chưa chọn loại sổ");
            }
            if (TaoBC)
            {
                DateTime _tungay = DungChung.Ham.NgayTu(deTuNgay.DateTime);
                DateTime _denngay = DungChung.Ham.NgayDen(deDenNgay.DateTime);
                DateTime _tungaynew = _tungay.AddMonths(-2);
                DateTime _denngaynew = _denngay.AddMonths(2);
                int _noitru = radBN.SelectedIndex;
                int _doituong = rgDTuong.SelectedIndex;
                int _trongDM = radDanhmuc.SelectedIndex;
                int _ThanhToan = radThanhtoan.SelectedIndex;
                #region 24297 tach ra vi timeout

                if (DungChung.Bien.MaBV == "24297")
                {
                    var q1 = (from cls in _data.CLS.Where(p => p.Status == 1).Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay).Where(p => p.MaKPth == _makp)
                              join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS

                              join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                              join dvct in _data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                              join bn in _data.BenhNhans.Where(p => _noitru == 2 ? true : p.NoiTru == _noitru).Where(p => _doituong == 3 ? true : (_doituong == 0 ? p.DTuong == "BHYT" : (_doituong == 1 ? p.DTuong == "Dịch vụ" : p.DTuong == "KSK"))).Where(p => _ThanhToan == 0 ? p.Status == 3 : _ThanhToan == 1 ? p.Status != 3 : (p.Status == 3 || p.Status != 3)) on cls.MaBNhan equals bn.MaBNhan
                              join dtct in _data.DThuoccts.Where(p => _trongDM == 2 ? (p.TrongBH == 1 || p.TrongBH == -1 || p.TrongBH == 0) : p.TrongBH == _trongDM) on cd.IDCD equals dtct.IDCD
                              select new { dvct.TenDVct, bn.MaBNhan, cd.ChiDinh1, cls.MaKP, cls.MaKPth, cls.NgayTH, cls.MaCBth, cls.MaCB, cls.NgayThang, cd.MaDV, cd.SoPhim, bn.TenBNhan, bn.DTuong, bn.DChi, bn.Tuoi, clsct.SoPhieu, bn.GTinh, cd.KetLuan, cd.MoTa, cls.IdCLS, cls.ChanDoan, clsct.KetQua, clsct.MaDVct, dtct.IDDon }).ToList();//,clsct.SoPhieu:cỡ phim

                    var q2 = (from cls in q1
                              join dv in _ldvall.Where(p => p.IdTieuNhom == idtieunhom) on cls.MaDV equals dv.MaDV
                              join vp in _lvp on cls.MaBNhan equals vp.MaBNhan into gj
                              from subVp in gj.DefaultIfEmpty()
                              join cb in _lcb on cls.MaCBth equals cb.MaCB
                              join kp in _lKhoaP on cls.MaKP equals kp.makp
                              join cb2 in _lcb on cls.MaCB equals cb2.MaCB
                              select new
                              {
                                  TT = subVp != null ? subVp.MaBNhan : 0,
                                  cls.IDDon,
                                  cls.MaBNhan,
                                  cls.ChiDinh1,
                                  cls.NgayTH,
                                  NgayTHNew = cls.NgayTH.Value.Date,
                                  cls.SoPhim,
                                  cls.TenBNhan,
                                  cls.DTuong,
                                  cls.DChi,
                                  cls.Tuoi,
                                  cls.GTinh,
                                  cls.SoPhieu,
                                  dv.TenDV,
                                  NguoiDoc = cb.TenCB,
                                  NoiGui = kp.tenkp,
                                  cls.MaKP,
                                  //KetLuan = DungChung.Bien.MaBV=="24009" ? dv.TenDV + "(" + cls.MaDVct + ")"+"-"+cls.KetQua : cls.KetQua ,
                                  //KetLuan = DungChung.Bien.MaBV == "24009" ? dv.TenDV + "[" + cls.MaDVct + "]" + "-" + cls.KetQua : cls.KetQua,
                                  KetLuan = DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "24297" ? cls.TenDVct + "(" + cls.KetQua + ")" : cls.KetQua,
                                  Mota = cls.MoTa,
                                  BSCD = cb2.TenCB,
                                  cls.IdCLS,
                                  cls.ChanDoan,
                                  KetLuanCD = cls.KetLuan
                              }).ToList(); //(dv.IDNhom != 1 ? cls.KetQua : cls.KetLuan)


                    //List<int> _lmabn = new List<int>();
                    //_lmabn = q2.Select(p => p.MaBNhan).Distinct().ToList();
                    //var _lbnkb = _data.BNKBs.Where(p => p.NgayKham >= _tungaynew && p.NgayKham <= _denngaynew).ToList();
                    //var _lcd = _lbnkb.Where(p => _lmabn.Contains(p.MaBNhan ?? 0)).ToList();
                    var q3 = (from cls in q2
                              group cls by new { cls.IDDon, cls.MaBNhan, cls.NgayTH, cls.NgayTHNew, cls.SoPhim, cls.TenBNhan, cls.DTuong, cls.DChi, cls.Tuoi, cls.SoPhieu, cls.NguoiDoc, cls.NoiGui, cls.GTinh, cls.MaKP, cls.BSCD, cls.IdCLS, cls.ChanDoan, cls.KetLuanCD, cls.ChiDinh1 } into kq
                              select new
                              {
                                  kq.Key.MaBNhan,
                                  kq.Key.NgayTH,
                                  kq.Key.NgayTHNew,
                                  kq.Key.SoPhim,
                                  kq.Key.TenBNhan,
                                  kq.Key.ChanDoan,
                                  kq.Key.DTuong,
                                  kq.Key.DChi,
                                  kq.Key.Tuoi,
                                  Nam = kq.Key.GTinh == 1 ? kq.Key.Tuoi.ToString() : "",
                                  Nu = kq.Key.GTinh == 0 ? kq.Key.Tuoi.ToString() : "",
                                  kq.Key.SoPhieu,
                                  TenDV = String.Join(",", kq.Select(p => p.TenDV).Distinct()),
                                  kq.Key.NguoiDoc,
                                  kq.Key.NoiGui,
                                  kq.Key.MaKP,
                                  KetLuan = String.Join(";", kq.Select(p => p.KetLuan)),
                                  //KetLuan = kq.Select(p => p.KetLuan).ToString(),
                                  kq.Key.BSCD,
                                  kq.Key.KetLuanCD,
                                  kq.Key.ChiDinh1
                              }).OrderBy(p => p.NgayTH).ToList();
                    var q4 = (from cls in q3
                              group cls by new { cls.NgayTHNew } into kq
                              select new { kq.Key.NgayTHNew, SoLan = kq.Select(p => p.TenDV).Count() }).ToList();
                    foreach (var a in q3)
                    {
                        BC moi = new BC();
                        moi.Mabn = a.MaBNhan;
                        moi.NgayTH = a.NgayTHNew;
                        moi.NgayTHCG = a.NgayTH.Value.Hour + ":" + a.NgayTH.Value.Minute + "\n " + a.NgayTH.Value.ToShortDateString();
                        moi.TenBN = a.TenBNhan;
                        moi.Nam = a.Nam;
                        moi.Nu = a.Nu;
                        moi.DChi = a.DChi;
                        moi.CoBH = a.DTuong == "BHYT" ? "x" : "";
                        //var cd = _lcd.Where(p => p.MaBNhan == a.MaBNhan && p.MaKP == a.MaKP).FirstOrDefault();
                        //if (cd != null)
                        moi.ChanDoan = DungChung.Ham.FreshString(a.ChanDoan);
                        moi.NoiGui = a.NoiGui;
                        moi.BSCD = a.BSCD;
                        moi.Yeucau = a.TenDV;
                        moi.NguoiTH = a.NguoiDoc;
                        moi.KetLuan = a.KetLuanCD;


                        var cp = DungChung.Bien._lCoPhim.Where(p => p.IdCoPhim == a.SoPhieu).FirstOrDefault();
                        //if (DungChung.Bien.MaBV == "27001")
                        //{
                        //    moi.NgayTHCG =  a.NgayTH.Value.ToShortDateString();

                       // }
                        if (cp != null)
                        {
                            moi.CoPhim = cp.CoPhim;
                            switch (cp.IdCoPhim)
                            {
                                case 1:
                                    moi.Phim13 = "x";
                                    break;
                                case 2:
                                    moi.Phim18 = "x";
                                    break;
                                case 3:
                                    moi.Phim24 = "x";
                                    break;
                                case 4:
                                    moi.Phim30 = "x";
                                    break;
                            }
                        }

                        moi.SoLuongF = a.SoPhim ?? 0;
                        var q5 = q4.Where(p => p.NgayTHNew == a.NgayTHNew).FirstOrDefault();
                        if (q5 != null)
                            moi.NgayTHT = "Tổng số yêu cầu thực hiện ngày " + a.NgayTHNew.ToShortDateString() + " : " + q5.SoLan + " ca";
                        moi.KetQua = DungChung.Bien.MaBV == "24009" ? a.ChiDinh1 + ": " + a.KetLuan : a.KetLuan;
                        moi.PhimHong = 0;

                        if (DungChung.Bien.MaBV == "24009")
                        {
                            


                            if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_CR || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_DR || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT)
                            {
                                moi.KLTH = a.KetLuan;
                            }
                            else if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || ckcSoSATim.Checked)
                            {
                                moi.KetQua = a.KetLuanCD;
                            }
                            else
                            {
                                moi.KLTH = a.KetLuanCD;
                            }
                        }
                        else
                        {
                            moi.KLTH = a.KetLuanCD; // + ". " + a.KetLuan  ;   KetluanCD-> Ketluan ;  a.KetLuan -> Mô tả
                        }


                        kqua.Add(moi);
                    }

                    if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_KTS || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAmMau || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDienNaoDo || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_CR || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_DR || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiCoTuCung)
                    {
                        
                        if (DungChung.Bien.MaBV == "30003")
                        {
                            BaoCao.Rep_SoTHCLSTD_30003 rep = new BaoCao.Rep_SoTHCLSTD_30003();
                            frmIn frm = new frmIn();
                            rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.KPTH.Value = lupKpTH.Text;
                            rep.TieuDe.Value = tenbc.ToUpper();
                            string dt = _doituong == 3 ? "Đối tượng tất cả" : (_doituong == 0 ? "Đối tượng BHYT" : (_doituong == 1 ? "Đối tượng dịch vụ" : "Đối tượng KSK"));
                            rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString() + "; " + dt;
                            //rep.TongDV.Value = "Tổng số dịch vụ được yêu cầu thực hiện là: " + kqua.Count().ToString() + " ca";
                            //rep.TongBN.Value = "Tổng số bệnh nhân yêu cầu thực hiện là: " + kqua.Select(p => p.Mabn).Distinct().Count() + " ca";
                            rep.DataSource = kqua;
                            rep.BinDingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {

                            BaoCao.Rep_SoTHCLSTD rep = new BaoCao.Rep_SoTHCLSTD();
                            frmIn frm = new frmIn();
                            rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.KPTH.Value = lupKpTH.Text;
                            rep.TieuDe.Value = tenbc.ToUpper();
                            string dt = _doituong == 3 ? "Đối tượng tất cả" : (_doituong == 0 ? "Đối tượng BHYT" : (_doituong == 1 ? "Đối tượng dịch vụ" : "Đối tượng KSK"));
                            rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString() + "; " + dt;
                            rep.TongDV.Value = "Tổng số dịch vụ được yêu cầu thực hiện là: " + kqua.Count().ToString() + " ca";
                            rep.TongBN.Value = "Tổng số bệnh nhân yêu cầu thực hiện là: " + kqua.Select(p => p.Mabn).Distinct().Count() + " ca";
                            rep.DataSource = kqua;
                            rep.BinDingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoNuocDich || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMienDich)
                    {
                        BaoCao.Rep_SoTHCLSTD_30003XN rep = new BaoCao.Rep_SoTHCLSTD_30003XN();
                        frmIn frm = new frmIn();
                        rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.KPTH.Value = lupKpTH.Text;
                        rep.TieuDe.Value = tenbc.ToUpper();
                        string dt = _doituong == 3 ? "Đối tượng tất cả" : (_doituong == 0 ? "Đối tượng BHYT" : (_doituong == 1 ? "Đối tượng dịch vụ" : "Đối tượng KSK"));
                        rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString() + "; " + dt;
                        //rep.TongDV.Value = "Tổng số dịch vụ được yêu cầu thực hiện là: " + kqua.Count().ToString() + " ca";
                        //rep.TongBN.Value = "Tổng số bệnh nhân yêu cầu thực hiện là: " + kqua.Select(p => p.Mabn).Distinct().Count() + " ca";
                        rep.DataSource = kqua;
                        rep.BinDingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                    else if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)
                    {
                        BaoCao.Rep_SoSieuAmDopplerTim rep = new BaoCao.Rep_SoSieuAmDopplerTim();
                        frmIn frm = new frmIn();
                        rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.KPTH.Value = lupKpTH.Text;
                        if (ckcSoSATim.Checked)
                        {
                            rep.TieuDe.Value = "SỔ SIÊU ÂM DOPPLER TIM";
                        }
                        else
                        {
                            rep.TieuDe.Value = "SỔ SIÊU ÂM DOPPLER";
                        }
                        string dt = _doituong == 3 ? "Đối tượng tất cả" : (_doituong == 0 ? "Đối tượng BHYT" : (_doituong == 1 ? "Đối tượng dịch vụ" : "Đối tượng KSK"));
                        rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString() + "; " + dt;
                        rep.TongDV.Value = "Tổng số dịch vụ được yêu cầu thực hiện là: " + kqua.Count().ToString() + " ca";
                        rep.TongBN.Value = "Tổng số bệnh nhân yêu cầu thực hiện là: " + kqua.Select(p => p.Mabn).Distinct().Count() + " ca";
                        rep.DataSource = kqua;
                        rep.BinDingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Đang cập nhật mẫu sổ");
                    }
                }
                #endregion

                #region bv khac vi bi cham
                else
                {
                    var q1 = (from cls in _data.CLS.Where(p => p.Status == 1).Where(p => p.NgayTH >= _tungay && p.NgayTH <= _denngay).Where(p => p.MaKPth == _makp)
                              join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS

                              join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                              join dvct in _data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                              join bn in _data.BenhNhans.Where(p => _noitru == 2 ? true : p.NoiTru == _noitru).Where(p => _doituong == 3 ? true : (_doituong == 0 ? p.DTuong == "BHYT" : (_doituong == 1 ? p.DTuong == "Dịch vụ" : p.DTuong == "KSK"))) on cls.MaBNhan equals bn.MaBNhan
                              select new { dvct.TenDVct, bn.MaBNhan, cd.ChiDinh1, cls.MaKP, cls.MaKPth, cls.NgayTH, cls.MaCBth, cls.MaCB, cls.NgayThang, cd.MaDV, cd.SoPhim, bn.TenBNhan, bn.DTuong, bn.DChi, bn.Tuoi, clsct.SoPhieu, bn.GTinh, cd.KetLuan, cd.MoTa, cls.IdCLS, cls.ChanDoan, clsct.KetQua, clsct.MaDVct }).ToList();//,clsct.SoPhieu:cỡ phim

                    var q2 = (from cls in q1
                              join dv in _ldvall.Where(p => p.IdTieuNhom == idtieunhom) on cls.MaDV equals dv.MaDV
                              join cb in _lcb on cls.MaCBth equals cb.MaCB
                              join kp in _lKhoaP on cls.MaKP equals kp.makp
                              join cb2 in _lcb on cls.MaCB equals cb2.MaCB
                              select new
                              {
                                  cls.MaBNhan,
                                  cls.ChiDinh1,
                                  cls.NgayTH,
                                  NgayTHNew = cls.NgayTH.Value.Date,
                                  cls.SoPhim,
                                  cls.TenBNhan,
                                  cls.DTuong,
                                  cls.DChi,
                                  cls.Tuoi,
                                  cls.GTinh,
                                  cls.SoPhieu,
                                  dv.TenDV,
                                  NguoiDoc = cb.TenCB,
                                  NoiGui = kp.tenkp,
                                  cls.MaKP,
                                  //KetLuan = DungChung.Bien.MaBV=="24009" ? dv.TenDV + "(" + cls.MaDVct + ")"+"-"+cls.KetQua : cls.KetQua ,
                                  //KetLuan = DungChung.Bien.MaBV == "24009" ? dv.TenDV + "[" + cls.MaDVct + "]" + "-" + cls.KetQua : cls.KetQua,
                                  KetLuan = DungChung.Bien.MaBV == "24009" ? cls.TenDVct + "(" + cls.KetQua + ")" : cls.KetQua,
                                  Mota = cls.MoTa,
                                  BSCD = cb2.TenCB,
                                  cls.IdCLS,
                                  cls.ChanDoan,
                                  KetLuanCD = cls.KetLuan
                              }).ToList(); //(dv.IDNhom != 1 ? cls.KetQua : cls.KetLuan)


                    //List<int> _lmabn = new List<int>();
                    //_lmabn = q2.Select(p => p.MaBNhan).Distinct().ToList();
                    //var _lbnkb = _data.BNKBs.Where(p => p.NgayKham >= _tungaynew && p.NgayKham <= _denngaynew).ToList();
                    //var _lcd = _lbnkb.Where(p => _lmabn.Contains(p.MaBNhan ?? 0)).ToList();
                    var q3 = (from cls in q2
                              group cls by new { cls.MaBNhan, cls.NgayTH, cls.NgayTHNew, cls.SoPhim, cls.TenBNhan, cls.DTuong, cls.DChi, cls.Tuoi, cls.SoPhieu, cls.NguoiDoc, cls.NoiGui, cls.GTinh, cls.MaKP, cls.BSCD, cls.IdCLS, cls.ChanDoan, cls.KetLuanCD, cls.ChiDinh1 } into kq
                              select new
                              {
                                  kq.Key.MaBNhan,
                                  kq.Key.NgayTH,
                                  kq.Key.NgayTHNew,
                                  kq.Key.SoPhim,
                                  kq.Key.TenBNhan,
                                  kq.Key.ChanDoan,
                                  kq.Key.DTuong,
                                  kq.Key.DChi,
                                  kq.Key.Tuoi,
                                  Nam = kq.Key.GTinh == 1 ? kq.Key.Tuoi.ToString() : "",
                                  Nu = kq.Key.GTinh == 0 ? kq.Key.Tuoi.ToString() : "",
                                  kq.Key.SoPhieu,
                                  TenDV = String.Join(",", kq.Select(p => p.TenDV).Distinct()),
                                  kq.Key.NguoiDoc,
                                  kq.Key.NoiGui,
                                  kq.Key.MaKP,
                                  KetLuan = String.Join(";", kq.Select(p => p.KetLuan)),
                                  //KetLuan = kq.Select(p => p.KetLuan).ToString(),
                                  kq.Key.BSCD,
                                  kq.Key.KetLuanCD,
                                  kq.Key.ChiDinh1
                              }).OrderBy(p => p.NgayTH).ToList();
                    var q4 = (from cls in q3
                              group cls by new { cls.NgayTHNew } into kq
                              select new { kq.Key.NgayTHNew, SoLan = kq.Select(p => p.TenDV).Count() }).ToList();
                    foreach (var a in q3)
                    {
                        BC moi = new BC();
                        moi.Mabn = a.MaBNhan;
                        moi.NgayTH = a.NgayTHNew;
                        moi.NgayTHCG = a.NgayTH.Value.Hour + ":" + a.NgayTH.Value.Minute + "\n " + a.NgayTH.Value.ToShortDateString();
                        moi.TenBN = a.TenBNhan;
                        moi.Nam = a.Nam;
                        moi.Nu = a.Nu;
                        moi.DChi = a.DChi;
                        moi.CoBH = a.DTuong == "BHYT" ? "x" : "";
                        //var cd = _lcd.Where(p => p.MaBNhan == a.MaBNhan && p.MaKP == a.MaKP).FirstOrDefault();
                        //if (cd != null)
                        moi.ChanDoan = DungChung.Ham.FreshString(a.ChanDoan);
                        moi.NoiGui = a.NoiGui;
                        moi.BSCD = a.BSCD;
                        moi.Yeucau = a.TenDV;
                        moi.NguoiTH = a.NguoiDoc;
                        moi.KetLuan = a.KetLuanCD;


                        var cp = DungChung.Bien._lCoPhim.Where(p => p.IdCoPhim == a.SoPhieu).FirstOrDefault();
                        if (DungChung.Bien.MaBV == "27001")
                        {
                            moi.NgayTHCG = a.NgayTH.Value.ToShortDateString();
                        }
                            if (cp != null)
                        {
                            moi.CoPhim = cp.CoPhim;
                            switch (cp.IdCoPhim)
                            {
                                case 1:
                                    moi.Phim13 = "x";
                                    break;
                                case 2:
                                    moi.Phim18 = "x";
                                    break;
                                case 3:
                                    moi.Phim24 = "x";
                                    break;
                                case 4:
                                    moi.Phim30 = "x";
                                    break;
                            }
                        }

                        moi.SoLuongF = a.SoPhim ?? 0;
                        var q5 = q4.Where(p => p.NgayTHNew == a.NgayTHNew).FirstOrDefault();
                        if (q5 != null)
                            moi.NgayTHT = "Tổng số yêu cầu thực hiện ngày " + a.NgayTHNew.ToShortDateString() + " : " + q5.SoLan + " ca";
                        moi.KetQua = DungChung.Bien.MaBV == "24009" ? a.ChiDinh1 + ": " + a.KetLuan : a.KetLuan;
                        moi.PhimHong = 0;

                        if (DungChung.Bien.MaBV == "24009")
                        {
                            if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_CR || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_DR || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT)
                            {
                                moi.KLTH = a.KetLuan;
                            }
                            else if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || ckcSoSATim.Checked)
                            {
                                moi.KetQua = a.KetLuanCD;
                            }
                            else
                            {
                                moi.KLTH = a.KetLuanCD;
                            }
                        }
                        else
                        {
                            moi.KLTH = a.KetLuanCD; // + ". " + a.KetLuan  ;   KetluanCD-> Ketluan ;  a.KetLuan -> Mô tả
                        }


                        kqua.Add(moi);
                    }
                    if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_KTS || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAmMau || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiTaiMuiHong || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.LuuHuyetNao || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKDienNaoDo || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ChucNangHoHap || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_CR || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang_DR)
                    {
                        if (DungChung.Bien.MaBV == "30003")
                        {
                            BaoCao.Rep_SoTHCLSTD_30003 rep = new BaoCao.Rep_SoTHCLSTD_30003();
                            frmIn frm = new frmIn();
                            rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.KPTH.Value = lupKpTH.Text;
                            rep.TieuDe.Value = tenbc.ToUpper();
                            string dt = _doituong == 3 ? "Đối tượng tất cả" : (_doituong == 0 ? "Đối tượng BHYT" : (_doituong == 1 ? "Đối tượng dịch vụ" : "Đối tượng KSK"));
                            rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString() + "; " + dt;
                            //rep.TongDV.Value = "Tổng số dịch vụ được yêu cầu thực hiện là: " + kqua.Count().ToString() + " ca";
                            //rep.TongBN.Value = "Tổng số bệnh nhân yêu cầu thực hiện là: " + kqua.Select(p => p.Mabn).Distinct().Count() + " ca";
                            rep.DataSource = kqua;
                            rep.BinDingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            BaoCao.Rep_SoTHCLSTD rep = new BaoCao.Rep_SoTHCLSTD();
                            frmIn frm = new frmIn();
                            rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                            rep.KPTH.Value = lupKpTH.Text;
                            rep.TieuDe.Value = tenbc.ToUpper();
                            string dt = _doituong == 3 ? "Đối tượng tất cả" : (_doituong == 0 ? "Đối tượng BHYT" : (_doituong == 1 ? "Đối tượng dịch vụ" : "Đối tượng KSK"));
                            rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString() + "; " + dt;
                            rep.TongDV.Value = "Tổng số dịch vụ được yêu cầu thực hiện là: " + kqua.Count().ToString() + " ca";
                            rep.TongBN.Value = "Tổng số bệnh nhân yêu cầu thực hiện là: " + kqua.Select(p => p.Mabn).Distinct().Count() + " ca";
                            rep.DataSource = kqua;
                            rep.BinDingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                    }
                    else if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDichChocDo || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNKhac || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoNuocDich || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMienDich)
                    {

                        BaoCao.Rep_SoTHCLSTD_30003XN rep = new BaoCao.Rep_SoTHCLSTD_30003XN();
                        frmIn frm = new frmIn();
                        rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.KPTH.Value = lupKpTH.Text;
                        rep.TieuDe.Value = tenbc.ToUpper();
                        string dt = _doituong == 3 ? "Đối tượng tất cả" : (_doituong == 0 ? "Đối tượng BHYT" : (_doituong == 1 ? "Đối tượng dịch vụ" : "Đối tượng KSK"));
                        rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString() + "; " + dt;
                        //rep.TongDV.Value = "Tổng số dịch vụ được yêu cầu thực hiện là: " + kqua.Count().ToString() + " ca";
                        //rep.TongBN.Value = "Tổng số bệnh nhân yêu cầu thực hiện là: " + kqua.Select(p => p.Mabn).Distinct().Count() + " ca";
                        rep.DataSource = kqua;
                        rep.BinDingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                   

                    else if (tenrg == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)
                    {
                        BaoCao.Rep_SoSieuAmDopplerTim rep = new BaoCao.Rep_SoSieuAmDopplerTim();
                        frmIn frm = new frmIn();
                        rep.TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                        rep.TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
                        rep.KPTH.Value = lupKpTH.Text;
                        if (ckcSoSATim.Checked)
                        {
                            rep.TieuDe.Value = "SỔ SIÊU ÂM DOPPLER TIM";
                        }
                        else
                        {
                            rep.TieuDe.Value = "SỔ SIÊU ÂM DOPPLER";
                        }
                        string dt = _doituong == 3 ? "Đối tượng tất cả" : (_doituong == 0 ? "Đối tượng BHYT" : (_doituong == 1 ? "Đối tượng dịch vụ" : "Đối tượng KSK"));
                        rep.NgayThang.Value = "Từ ngày " + _tungay.ToShortDateString() + " đến ngày " + _denngay.ToShortDateString() + "; " + dt;
                        rep.TongDV.Value = "Tổng số dịch vụ được yêu cầu thực hiện là: " + kqua.Count().ToString() + " ca";
                        rep.TongBN.Value = "Tổng số bệnh nhân yêu cầu thực hiện là: " + kqua.Select(p => p.Mabn).Distinct().Count() + " ca";
                        rep.DataSource = kqua;
                        rep.BinDingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Đang cập nhật mẫu sổ");
                    }
                }
                #endregion
            }
        }
        void LoadKPTH(string TenRG)
        {
            var kpc = _lkpall.Where(p => p.ChuyenKhoa != null && p.ChuyenKhoa.Contains(TenRG)).ToList();
            if (kpc.Count() > 0)
            {
                //kpc.Add()
                lupKpTH.Properties.DataSource = kpc;
                lupKpTH.EditValue = kpc.First().MaKP;
            }
            else
            {
                lupKpTH.Properties.DataSource = null;
            }
        }
        private void rgLoaiSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (rgLoaiSo.SelectedIndex)
            //{
            //case 0:
            //    LoadKPTH(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang);
            //    break;
            //case 1:
            //    ckcSoSATim.Checked = true;
            //    if(ckcSoSATim.Checked)
            //        LoadKPTH(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler);
            //    else
            //    LoadKPTH(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm);

            //    break;
            //case 2:
            //    LoadKPTH(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim);
            //    break;
            //case 3:
            //    LoadKPTH(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi);
            //    break;
            //}
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lupChonSo_EditValueChanged(object sender, EventArgs e)
        {
            if (lupChonSo.EditValue != null)
            {
                int idtn = Convert.ToInt32(lupChonSo.EditValue);
                var tenrg = _ltnnew.Where(p => p.ID == idtn).FirstOrDefault();
                if (tenrg != null)
                {
                    if (tenrg.IDNhom == 1)
                        LoadKPTH("Xét nghiệm");
                    else
                        LoadKPTH(tenrg.TenRG);
                }
            }
        }

        private void cboInBia_EditValueChanged(object sender, EventArgs e)
        {
            if (cboInBia.EditValue == "Tờ bìa kiểu 1")
            {
                bool TaoBC = true;
                int _makp = 0;
                if (lupKpTH.EditValue != null)
                {
                    _makp = Convert.ToInt32(lupKpTH.EditValue);
                }
                else
                {
                    TaoBC = false;
                    MessageBox.Show("Chưa chọn khoa phòng thực hiện");
                }
                List<KPhongc> _lKhoaP = new List<KPhongc>();
                List<BC> kqua = new List<BC>();
                _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                if (_lKhoaP.Count() == 0)
                {
                    TaoBC = false;
                    MessageBox.Show("Chưa chọn khoa phòng chỉ định");
                }
                string tenrg = "", tenbc = ""; int idtieunhom = 0;
                TieuNhom tenrg1 = new TieuNhom();
                if (lupChonSo.EditValue != null)
                {
                    idtieunhom = Convert.ToInt32(lupChonSo.EditValue);
                    tenrg1 = _ltnnew.Where(p => p.ID == idtieunhom).FirstOrDefault();
                    if (tenrg1 != null)
                    {
                        tenrg = tenrg1.TenRG;
                        tenbc = tenrg1.Ten;
                    }
                }
                else
                {
                    TaoBC = false;
                    MessageBox.Show("Chưa chọn loại sổ");
                }
                if (TaoBC)
                {
                    BaoCao.Rep_bia_SoTHCLS rep = new BaoCao.Rep_bia_SoTHCLS();
                    frmIn frm = new frmIn();
                    rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.CQ.Value = "BỆNH VIỆN: " + DungChung.Bien.TenCQ.ToUpper();
                    rep.TenSo.Value = tenbc.ToUpper();
                    rep.KPTH.Value = "KHOA PHÒNG: " + lupKpTH.Text.ToUpper();
                    rep.TuNgay.Value = deTuNgay.DateTime.ToShortDateString();
                    rep.DenNgay.Value = deDenNgay.DateTime.ToShortDateString();
                    rep.DataSource = kqua;
                    rep.BinDingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();


                }
            }
            else if (cboInBia.EditValue == "Tờ bìa kiểu 2")
            {
                bool TaoBC = true;
                int _makp = 0;
                if (lupKpTH.EditValue != null)
                {
                    _makp = Convert.ToInt32(lupKpTH.EditValue);
                }
                else
                {
                    TaoBC = false;
                    MessageBox.Show("Chưa chọn khoa phòng thực hiện");
                }
                List<KPhongc> _lKhoaP = new List<KPhongc>();
                List<BC> kqua = new List<BC>();
                _lKhoaP = _Kphong.Where(p => p.chon == true).ToList();
                if (_lKhoaP.Count() == 0)
                {
                    TaoBC = false;
                    MessageBox.Show("Chưa chọn khoa phòng chỉ định");
                }
                string tenrg = "", tenbc = ""; int idtieunhom = 0;
                TieuNhom tenrg1 = new TieuNhom();
                if (lupChonSo.EditValue != null)
                {
                    idtieunhom = Convert.ToInt32(lupChonSo.EditValue);
                    tenrg1 = _ltnnew.Where(p => p.ID == idtieunhom).FirstOrDefault();
                    if (tenrg1 != null)
                    {
                        tenrg = tenrg1.TenRG;
                        tenbc = tenrg1.Ten;
                    }
                }
                else
                {
                    TaoBC = false;
                    MessageBox.Show("Chưa chọn loại sổ");
                }
                if (TaoBC)
                {
                    BaoCao.Rep_bia_SoTHCLS_2 rep = new BaoCao.Rep_bia_SoTHCLS_2();
                    frmIn frm = new frmIn();
                    rep.CQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.CQ.Value = "BỆNH VIỆN: " + DungChung.Bien.TenCQ.ToUpper();
                    rep.TenSo.Value = tenbc.ToUpper();
                    rep.KPTH.Value = "KHOA PHÒNG: " + lupKpTH.Text.ToUpper();
                    rep.TuNgay.Value = deTuNgay.DateTime.ToShortDateString();
                    rep.DenNgay.Value = deDenNgay.DateTime.ToShortDateString();
                    rep.DataSource = kqua;
                    rep.BinDingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {


        }
    }
}