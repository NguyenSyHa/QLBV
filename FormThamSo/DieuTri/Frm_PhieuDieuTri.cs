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
    public partial class Frm_PhieuDieuTri : DevExpress.XtraEditors.XtraForm
    {
        int _mbn = 0;
        string _TenBN = "";

        public Frm_PhieuDieuTri()
        {
            InitializeComponent();
        }
        public  Frm_PhieuDieuTri(int _m)
        {
            InitializeComponent();
            _mbn = _m;
        }
        public class l_CTThuoc
        {
            public string tendv, mabn, donvi, sl1, sl2, sl3, sl4, sl5, sl6, sl7, sl8, sl9, sl10, sl11, sl12, sl13, sl14, sl15, sl16, sl17, sl18, sl19, sl20, sl21, sl22, sl23, sl24, sl25, sl26, sl27, sl28, sl29, sl30, sl31, sl32, tennhomdv;
            string sL;

            public string SL
            {
                get { return sL; }
                set { sL = value; }
            }
            string tT;

            public string TT
            {
                get { return tT; }
                set { tT = value; }
            }
            int pLoai;

            public int PLoai
            {
                get { return pLoai; }
                set { pLoai = value; }
            }
            int trongbh, idnhom, madv;
            double soluong, dongia, thanhtien;
            public string TenNhomDV
            { set { tennhomdv = value; } get { return tennhomdv; } }
            public int IDNHOM
            { set { idnhom = value; } get { return idnhom; } }
            public string TenDV
            {
                set { tendv = value; }
                get { return tendv; }
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
            public string DonVi
            {
                set { donvi = value; }
                get { return donvi; }
            }
            public double SoLuong
            {
                set { soluong = value; }
                get { return soluong; }
            }
            public double DonGia
            {
                set { dongia = value; }
                get { return dongia; }
            }
            public double ThanhTien
            { set { thanhtien = value; } get { return thanhtien; } }
            public string SL1
            { set { sl1 = value; } get { return sl1; } }
            public string SL2
            { set { sl2 = value; } get { return sl2; } }
            public string SL3
            { set { sl3 = value; } get { return sl3; } }
            public string SL4
            { set { sl4 = value; } get { return sl4; } }
            public string SL5
            { set { sl5 = value; } get { return sl5; } }
            public string SL6
            { set { sl6 = value; } get { return sl6; } }
            public string SL7
            { set { sl7 = value; } get { return sl7; } }
            public string SL8
            { set { sl8 = value; } get { return sl8; } }
            public string SL9
            { set { sl9 = value; } get { return sl9; } }
            public string SL10
            { set { sl10 = value; } get { return sl10; } }
            public string SL11
            { set { sl11 = value; } get { return sl11; } }
            public string SL12
            { set { sl12 = value; } get { return sl12; } }
            public string SL13
            { set { sl13 = value; } get { return sl13; } }
            public string SL14
            { set { sl14 = value; } get { return sl14; } }
            public string SL15
            { set { sl15 = value; } get { return sl15; } }
            public string SL16
            { set { sl16 = value; } get { return sl16; } }
            public string SL17
            { set { sl17 = value; } get { return sl17; } }
            public string SL18
            { set { sl18 = value; } get { return sl18; } }
            public string SL19
            { set { sl19 = value; } get { return sl19; } }
            public string SL20
            { set { sl20 = value; } get { return sl20; } }
            public string SL21
            { set { sl21 = value; } get { return sl21; } }
            public string SL22
            { set { sl22 = value; } get { return sl22; } }
            public string SL23
            { set { sl23 = value; } get { return sl23; } }
            public string SL24
            { set { sl24 = value; } get { return sl24; } }
            public string SL25
            { set { sl25 = value; } get { return sl25; } }
            public string SL26
            { set { sl26 = value; } get { return sl26; } }
            public string SL27
            { set { sl27 = value; } get { return sl27; } }
            public string SL28
            { set { sl28 = value; } get { return sl28; } }
            public string SL29
            { set { sl29 = value; } get { return sl29; } }
            public string SL30
            { set { sl30 = value; } get { return sl30; } }
            public string SL31
            { set { sl31 = value; } get { return sl31; } }
            public string SL32
            { set { sl32 = value; } get { return sl32; } }
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
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        int trangthai = 0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Mẫu biểu đang nâng cấp!");
           
            var Ten = (from t in data.BenhNhans.Where(p => p.MaBNhan == _mbn) select new { t.TenBNhan }).ToList();
            if (Ten.Count > 0)
            { _TenBN = Ten.First().TenBNhan; }
            List<l_CTThuoc> _ldthuocct = new List<l_CTThuoc>();
            DateTime ngayvao = System.DateTime.Now.Date;
            DateTime ngayra = System.DateTime.Now.Date;
            DateTime[] _ngay = new DateTime[100];

            //_mbn = txtMaBNhan.Text;
            if (_mbn <= 0)
            {
                MessageBox.Show("Bạn chưa chọn BN hoặc không có BN");
                return;
            }
            int trongDM = -1, NgoaiDM = -1, CPKem = -1;
            if (chk_TrongDMBH.Checked)
                trongDM = 1;
            if (chk_NgoaiDM.Checked)
                NgoaiDM = 0;
            if (chk_CPKem.Checked)
                CPKem = 2;
            var dt_rep2 = (from dthc in data.DThuocs.Where(p => p.MaBNhan == (_mbn))
                           join dtct in data.DThuoccts.Where(p => (p.TrongBH==trongDM || p.TrongBH==NgoaiDM || p.TrongBH==CPKem)).Where(p => p.MaKP != null && p.NgayNhap != null) on dthc.IDDon equals dtct.IDDon
                           join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                           join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                           where (ChkChon.Checked ? true : dv.PLoai == 1)
                           select new { dv.PLoai, dtct.NgayNhap, dtct.MaKP, dtct.DonGia, dv.DonVi, dtct.MaDV, dv.IDNhom, dtct.SoLuong, TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ?(dv.TenRG ?? ""):  dv.TenDV, dtct.ThanhTien, nhomdv.TenNhom, nhomdv.STT }).ToList();
            var dt_rep1 = (from dtct in dt_rep2
                           group new { dtct } by new { NgayNhap = dtct.NgayNhap.Value.Date, dtct.PLoai, dtct.DonGia, dtct.DonVi, dtct.MaDV, dtct.TenDV, dtct.MaKP, dtct.IDNhom, dtct.TenNhom, dtct.STT } into kq
                           select new { kq.Key.NgayNhap, kq.Key.PLoai, kq.Key.MaKP, kq.Key.DonGia, kq.Key.DonVi, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong), kq.Key.TenDV, Thanhtien = kq.Sum(p => p.dtct.ThanhTien), kq.Key.TenNhom, kq.Key.STT }).ToList();
            var ngay1 = (from dtct in dt_rep2
                         group new { dtct } by new { dtct.NgayNhap.Value.Date, dtct.NgayNhap, dtct.MaKP } into kq
                         select new { NgayNhap = kq.Key.Date, kq.Key.MaKP }).OrderBy(p => p.NgayNhap).ToList();
            var ngay = (from n in ngay1
                        join kp in _Kphong.Where(p => p.chon == true) on n.MaKP equals kp.makp
                        group new { n } by new { n.NgayNhap } into kq
                        select new { kq.Key.NgayNhap }).OrderBy(p => p.NgayNhap).ToList();
            var ngaydt = (from dthc in ngay1
                          group new { dthc } by new { dthc.NgayNhap } into kq
                          select new { kq.Key }).OrderBy(p => p.Key.NgayNhap).ToList();
            var vvien = data.VaoViens.Where(p => p.MaBNhan == _mbn).ToList();
            var rv = data.RaViens.Where(p => p.MaBNhan == _mbn).ToList();
            int gioihan =29;
            DateTime _ngayMin = new DateTime();
            DateTime _ngayMax = new DateTime();
           List< DateTime> _lngaySD=new List<DateTime>();
           if (ngaydt.Count > 0)
           {
               _ngayMin = ngaydt.Min(p => p.Key.NgayNhap);
               _ngayMax = ngaydt.Max(p => p.Key.NgayNhap);
           }
           if (vvien.Count > 0 && chk_Ngay.Checked)
               _ngayMin = vvien.First().NgayVao.Value.Date;
           if (rv.Count > 0 && chk_Ngay.Checked)
               _ngayMax = rv.First().NgayRa.Value.Date;
            if (chk_Ngay.Checked)
            {
                //int khoangcach=  (_ngayMax.Date - _ngayMin.Date).Days
                for (int i = 0; i <= (_ngayMax.Date - _ngayMin.Date).Days; i++) {
                    _lngaySD.Add(_ngayMin.AddDays(i));
                }
            }
            else {
                foreach(var item in ngaydt)
                _lngaySD.Add(item.Key.NgayNhap);
            }
            double thuoc = 0;
             
             for (int k = -1; k < _lngaySD.Count ; k += gioihan)
            {
                if (k == -1)
                    k = 0;
                 for (int j = 0; j < 100; j++)
                {
                    _ngay[j] = Convert.ToDateTime("01/01/0001");
                }
                
                int i = 0;
                        int l =0;
                if (k >= gioihan)
                    l = k ;
               //l = k+1;
                if (k == 0)
                    k++;
                else {
                    if (k == gioihan + 1)
                        k--;
                }
                for (int item = l; item < _lngaySD.Count; item++)
                    { 
                 
                        _ngay[i] = _lngaySD.Skip(item).Take(1).First();
                        i++;
                        if (item == gioihan + l)
                            break;
                    }
                try
                {
                    _ngayMin = _ngay.Where(p => p != Convert.ToDateTime("01/01/0001")).Min(p => p);
                    _ngayMax = _ngay.Where(p => p != Convert.ToDateTime("01/01/0001")).Max(p => p);
                } catch{
                }
                _ldthuocct = (from dt in dt_rep1
                              join kp in _Kphong.Where(p => p.chon == true) on dt.MaKP equals kp.makp
                              group new { dt } by new { dt.PLoai, dt.DonGia, dt.DonVi, dt.MaDV, dt.TenDV, dt.TenNhom, dt.STT } into kq
                              select new l_CTThuoc
                              {
                                  TenNhomDV = kq.Key.TenNhom,
                                  DonGia = kq.Key.DonGia,
                                  DonVi = kq.Key.DonVi,
                                  MaDV = kq.Key.MaDV ?? 0,
                                  SoLuong = kq.Sum(p => p.dt.SoLuong),
                                  TenDV = kq.Key.TenDV,
                                  ThanhTien = kq.Sum(p => p.dt.Thanhtien),
                                  IDNHOM = kq.Key.STT ?? 0,
                                  SL = kq.Where(p => p.dt.NgayNhap >= _ngayMin && p.dt.NgayNhap <= _ngayMax).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  TT = kq.Where(p => p.dt.NgayNhap >= _ngayMin&& p.dt.NgayNhap <= _ngayMax).Sum(p => p.dt.Thanhtien).ToString("##,###"),
                                  SL1 = kq.Where(p => p.dt.NgayNhap == _ngay[0]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL2 = kq.Where(p => p.dt.NgayNhap == _ngay[1]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL3 = kq.Where(p => p.dt.NgayNhap == _ngay[2]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL4 = kq.Where(p => p.dt.NgayNhap == _ngay[3]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL5 = kq.Where(p => p.dt.NgayNhap == _ngay[4]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL6 = kq.Where(p => p.dt.NgayNhap == _ngay[5]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL7 = kq.Where(p => p.dt.NgayNhap == _ngay[6]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL8 = kq.Where(p => p.dt.NgayNhap == _ngay[7]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL9 = kq.Where(p => p.dt.NgayNhap == _ngay[8]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL10 = kq.Where(p => p.dt.NgayNhap == _ngay[9]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL11 = kq.Where(p => p.dt.NgayNhap == _ngay[10]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL12 = kq.Where(p => p.dt.NgayNhap == _ngay[11]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL13 = kq.Where(p => p.dt.NgayNhap == _ngay[12]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL14 = kq.Where(p => p.dt.NgayNhap == _ngay[13]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL15 = kq.Where(p => p.dt.NgayNhap == _ngay[14]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL16 = kq.Where(p => p.dt.NgayNhap == _ngay[15]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL17 = kq.Where(p => p.dt.NgayNhap == _ngay[16]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL18 = kq.Where(p => p.dt.NgayNhap == _ngay[17]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL19 = kq.Where(p => p.dt.NgayNhap == _ngay[18]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL20 = kq.Where(p => p.dt.NgayNhap == _ngay[19]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL21 = kq.Where(p => p.dt.NgayNhap == _ngay[20]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL22 = kq.Where(p => p.dt.NgayNhap == _ngay[21]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL23 = kq.Where(p => p.dt.NgayNhap == _ngay[22]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL24 = kq.Where(p => p.dt.NgayNhap == _ngay[23]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL25 = kq.Where(p => p.dt.NgayNhap == _ngay[24]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL26 = kq.Where(p => p.dt.NgayNhap == _ngay[25]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL27 = kq.Where(p => p.dt.NgayNhap == _ngay[26]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL28 = kq.Where(p => p.dt.NgayNhap == _ngay[27]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL29 = kq.Where(p => p.dt.NgayNhap == _ngay[28]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                                  SL30 = kq.Where(p => p.dt.NgayNhap == _ngay[29]).Sum(p => p.dt.SoLuong).ToString("##,###"),
                              }).Where(p=>p.SL.Length>0).OrderBy(p => p.TenNhomDV).ToList();
                string fomatDate = "dd/MM/yyyy";
                if (DungChung.Bien.MaBV == "04016" || DungChung.Bien.MaBV == "04011")
                    fomatDate = "dd/MM/yyyy HH:mm";
                var par = (from bn in data.BenhNhans.Where(p => p.MaBNhan == (_mbn))
                           join kb in data.BNKBs.OrderByDescending(p => p.IDKB) on bn.MaBNhan equals kb.MaBNhan
                           join vv in data.VaoViens on bn.MaBNhan equals vv.MaBNhan
                           select new
                           {
                               kb.MaKP,
                               bn.DTuong,
                               bn.MaBNhan,
                               bn.DChi,
                               bn.Tuoi,
                               bn.SThe,
                               bn.MaCS,
                               bn.NoiTru,
                               bn.HanBHTu,
                               bn.HanBHDen,
                               MaICD = DungChung.Bien.MaBV == "30003" ?  (kb.MaICD + ";" + kb.MaICD2) : kb.MaICD,
                               kb.ChanDoan,
                               vv.NgayVao,
                               kb.IDKB,
                               kb.BenhKhac,
                               kb.Buong,
                               kb.Giuong,
                               bn.NgaySinh,
                               bn.ThangSinh,
                               bn.NamSinh,
                               bn.NNhap,
                               vv.SoBA,
                           }).OrderByDescending(p => p.IDKB).ToList();
                int _makp = 0;
                if (par.Count > 0)
                {
                    ngayvao = DungChung.Ham.NgayTu(par.First().NgayVao.Value);
                    frmIn frm = new frmIn();
                    BaoCao.repDTNoiTruHangNgay_HL01a_Moi rep = new BaoCao.repDTNoiTruHangNgay_HL01a_Moi(_ngay, 2);
                    string _macs = "";
                    if (par.First().MaCS != null)
                        _macs = par.First().MaCS;
                    var bv = data.BenhViens.Where(p => p.MaBV == _macs).ToList();
                    if (bv.Count > 0)
                        rep.NoiDKKCB.Value = bv.First().TenBV;
                    _makp = par.First().MaKP ?? 0;
                    rep.Buong.Value = par.First().Buong;
                    rep.Giuong.Value = par.First().Giuong;
                    rep.SoBA.Value = par.First().SoBA;
                              
                    if (rv.Count > 0)
                    {

                        rep.NgayRV.Value = rv.First().NgayRa.Value.ToString(fomatDate);
                        if (rv.First().Status == 1)
                        {
                            rep.ch_chuyenVien.Checked = true;
                        }
                        else
                        {
                            if (rv.First().KetQua.ToLower().Contains("khỏi"))
                                rep.chk_Khoi.Checked = true;
                            else if (rv.First().KetQua.ToLower().Contains("vong"))
                                rep.chk_TuVong.Checked = true;
                            else if (rv.First().KetQua.ToLower().Contains("nặng hơn"))
                                rep.ckc_NangHon.Checked = true;
                            else
                                rep.chk_Do.Checked = true;
                        }
                    }
                    var kp = _Kphong.Where(p => p.makp == _makp).Select(p => p.tenkp).ToList();
                    rep.Khoa.Value = kp.Count > 0 ? kp.First().ToUpper() : "";
                    rep.TenBN.Value = _TenBN.ToUpper();
                    rep.TuoiBN.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.CalculateAge(par.First().NgaySinh, par.First().ThangSinh, par.First().NamSinh," tháng.") : DungChung.Ham.TuoitheoThang(data, _mbn, DungChung.Bien.formatAge);


                    if (par.First().NoiTru == 0)
                    {
                        if (DungChung.Bien.MaBV == "08204")
                            rep.TieuDe.Value = "PHIẾU THỐNG KÊ THUỐC 15 NGÀY ĐIỀU TRỊ";
                        else if (DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "30012")
                        {
                            rep.TieuDe.Value = "PHIẾU TÍNH THUỐC VÀ THANH TOÁN VIỆN PHÍ DÙNG CHO BỆNH NHÂN BHYT";
                            double t = _ldthuocct.Sum(p => p.ThanhTien);
                            rep.Tongtien.Value = "Tổng tiền: " + t.ToString("##,###");
                            rep.MaBN.Value = "Mã bệnh nhân: " + _mbn;
                        }
                        else
                            rep.TieuDe.Value = "PHIẾU ĐIỀU TRỊ NGOẠI TRÚ CHO BỆNH NHÂN HẰNG NGÀY";
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "08204")
                            rep.TieuDe.Value = "PHIẾU THỐNG KÊ THUỐC 15 NGÀY ĐIỀU TRỊ";
                        else if (DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "30012")
                        {
                            rep.TieuDe.Value = "PHIẾU TÍNH THUỐC VÀ THANH TOÁN VIỆN PHÍ DÙNG CHO BỆNH NHÂN BHYT";
                            double t = _ldthuocct.Sum(p => p.ThanhTien);
                            rep.Tongtien.Value = "Tổng tiền: " + t.ToString("##,###");
                            rep.MaBN.Value = "Mã bệnh nhân: " + _mbn;
                        }
                        else
                            rep.TieuDe.Value = "PHIẾU ĐIỀU TRỊ NỘI TRÚ CHO BỆNH NHÂN HẰNG NGÀY";
                    }
                    rep.DiaChi.Value = par.First().DChi;
                    rep.SoThe.Value = par.First().SThe + " - " + par.First().MaCS;
                    if (par.First().DTuong == "BHYT")
                    {
                        rep.GTTu.Value = par.First().HanBHTu.Value.ToShortDateString();
                        rep.GTDen.Value = par.First().HanBHDen.Value.ToShortDateString();
                    }
                    else
                    {
                        rep.GTTu.Value = null;
                        rep.GTDen.Value = null;
                    }
                    rep.NgayVV.Value = par.First().NgayVao.Value.ToString(fomatDate);
                    rep.ChanDoan.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.GetChanDoanKB(data, _mbn) : DungChung.Ham.GetICDstr(par.First().ChanDoan + par.First().BenhKhac);
                    rep.MaChanDoan.Value = par.First().MaICD;
                 
                    thuoc = _ldthuocct.Sum(p => p.ThanhTien);
                    rep.DataSource = _ldthuocct;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                  
                }
                else
                {
                    MessageBox.Show("Bệnh nhân chưa có khám bệnh vào viện");
                }
            }
             if (ChkChon.Checked == false)
                 simpleButton21_Click(sender, e, thuoc);
        }
        private void simpleButton21_Click(object sender, EventArgs e, double thuoc)
        {
            if (trangthai == 0)
            {

                int ngay = 1;
                DialogResult _result = MessageBox.Show("Hiển thị ngày thực hiện dịch vụ", "Hiển thị ngày", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                    ngay = 0;
                int trongDM = -1, NgoaiDM = -1, CPKem = -1;
                if (chk_TrongDMBH.Checked)
                    trongDM = 1;
                if (chk_NgoaiDM.Checked)
                    NgoaiDM = 0;
                if (chk_CPKem.Checked)
                    CPKem = 2;
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                frmIn frm = new frmIn();
                double tienthuoc = thuoc;
                List<BaoCao.repDTNoiTruHangNgay_HL01b.THDV> _LTHDV2 = new List<BaoCao.repDTNoiTruHangNgay_HL01b.THDV>();
                List<BaoCao.repDTNoiTruHangNgay_HL01b.THDV> _LTHDV3 = new List<BaoCao.repDTNoiTruHangNgay_HL01b.THDV>();
                // List<THDV> _LTHDV = new List<THDV>();

                var q21 = (from bn in data.BenhNhans
                           join dt in data.DThuocs on bn.MaBNhan equals dt.MaBNhan
                           join dtct in data.DThuoccts.Where(p => (p.TrongBH==trongDM || p.TrongBH==NgoaiDM || p.TrongBH==CPKem)) on dt.IDDon equals dtct.IDDon
                           join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                           join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                           //join tn in data.TieuNhomDVs on nhomdv.IDNhom equals tn.IDNhom
                           where (dv.PLoai == 2)
                           //where (dt.MaKP== (DungChung.Bien.MaKP))
                           where (bn.MaBNhan == (_mbn))
                           group new { nhomdv, dt, dtct } by new { dtct.NgayNhap, nhomdv.TenNhom, dv.TenDV, dtct.DonGia, dtct.MaKP, STT1 = nhomdv.STT } into kq
                           select new
                           {
                               //kq.Key.STT,
                               kq.Key.STT1,
                               kq.Key.NgayNhap,
                               TenNhomDV = kq.Key.TenNhom.ToUpper(),
                               TenDV = kq.Key.TenDV,
                               kq.Key.DonGia,
                               kq.Key.MaKP,
                               SoLuongT = kq.Sum(p => p.dtct.SoLuong),
                               ThanhTienT = kq.Sum(p => p.dtct.ThanhTien)
                           }).ToList();
                var q2 = (from t in q21
                          join kp in _Kphong.Where(p => p.chon == true) on t.MaKP equals kp.makp
                          group new { t } by new { t.NgayNhap, t.TenNhomDV, t.TenDV, t.DonGia, t.STT1 } into kq
                          select new
                          {
                              //kq.Key.STT,
                              kq.Key.STT1,
                              kq.Key.NgayNhap,
                              TenNhomDV = kq.Key.TenNhomDV.ToUpper(),
                              TenDV = kq.Key.TenDV,
                              kq.Key.DonGia,
                              SoLuongT = kq.Sum(p => p.t.SoLuongT),
                              ThanhTienT = kq.Sum(p => p.t.ThanhTienT)
                          }).ToList();
                int i = 0;
                int j = 1;
                bool _l1 = true;
                string tennhom = "";
                foreach (var a in q2)
                {
                    i++;
                    if (i == 1)
                        tennhom = a.TenNhomDV;
                    if (i <= 10 && _l1)
                    {
                        if (tennhom != a.TenNhomDV)
                        {
                            j++;
                            tennhom = a.TenNhomDV;

                        }
                        if (j <= 3)
                        {
                            BaoCao.repDTNoiTruHangNgay_HL01b.THDV moi = new BaoCao.repDTNoiTruHangNgay_HL01b.THDV();

                            moi.STT = i;
                            moi.TenDV = a.TenDV;
                            moi.SoLuong = a.SoLuongT;
                            moi.DonGia = a.DonGia;
                            moi.ThanhTien = a.ThanhTienT;
                            moi.TenNhomDV = a.TenNhomDV;
                            if (a.NgayNhap != null && a.NgayNhap.Value.Day > 0)
                                moi.ngaynhap = a.NgayNhap.ToString().Substring(0, 10);
                            _LTHDV2.Add(moi);
                        }
                        else
                        {
                            _l1 = false;
                            // i--;
                            BaoCao.repDTNoiTruHangNgay_HL01b.THDV moi = new BaoCao.repDTNoiTruHangNgay_HL01b.THDV();
                            moi.STT = i;
                            moi.TenDV = a.TenDV;
                            moi.SoLuong = a.SoLuongT;
                            moi.DonGia = a.DonGia;
                            moi.ThanhTien = a.ThanhTienT;
                            moi.TenNhomDV = a.TenNhomDV;
                            if (a.NgayNhap != null && a.NgayNhap.Value.Day > 0)
                                moi.ngaynhap = a.NgayNhap.ToString().Substring(0, 10);
                            _LTHDV3.Add(moi);
                        }


                    }
                    else
                    {
                        BaoCao.repDTNoiTruHangNgay_HL01b.THDV moi = new BaoCao.repDTNoiTruHangNgay_HL01b.THDV();
                        moi.STT = i;
                        moi.TenDV = a.TenDV;
                        moi.SoLuong = a.SoLuongT;
                        moi.DonGia = a.DonGia;
                        moi.ThanhTien = a.ThanhTienT;
                        moi.TenNhomDV = a.TenNhomDV;
                        if (a.NgayNhap != null && a.NgayNhap.Value.Day > 0)
                            moi.ngaynhap = a.NgayNhap.ToString().Substring(0, 10);
                        _LTHDV3.Add(moi);
                    }

                }
                // if(q2.Count>0)
                BaoCao.repDTNoiTruHangNgay_HL01b rep = new BaoCao.repDTNoiTruHangNgay_HL01b(_LTHDV2, _LTHDV3);
                rep.Ngay.Value = ngay;
                tienthuoc = q2.Sum(p => p.ThanhTienT) + thuoc;
                rep.TongTien.Value = tienthuoc;
                rep.TenBN.Value = _TenBN;
                //rep.DataSource = _LTHDV.ToList();
                //rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();

            }
        }

        private void sbtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string _maCQCQ = "";
        private void Frm_PhieuDieuTri_Load(object sender, EventArgs e)
        {
            //_mbn = "73860";
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qCQCQ = _data.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (qCQCQ != null)
                _maCQCQ = qCQCQ.MaChuQuan;
            if (DungChung.Bien.MaBV == "30003")
                ChkChon.Checked = true;
            var kphong = (from dt in data.DThuocs.Where(p => p.MaBNhan == _mbn)
                          join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                          join kp in data.KPhongs on dtct.MaKP equals kp.MaKP
                          //where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          group new { kp } by new { kp.MaKP, kp.TenKP } into kq
                          select new { TenKP = kq.Key.TenKP, MaKP = kq.Key.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = false;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = false;
                    _Kphong.Add(themmoi);
                }
                grcKP.DataSource = _Kphong.ToList();
            }
        }

        private void grvKP_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKP.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKP.GetFocusedRowCellValue("tenkp").ToString();
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
                        grcKP.DataSource = "";
                        grcKP.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
    }
}