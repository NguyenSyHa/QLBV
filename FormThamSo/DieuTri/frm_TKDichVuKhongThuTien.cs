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
    public partial class frm_TKDichVuKhongThuTien : DevExpress.XtraEditors.XtraForm
    {
        public frm_TKDichVuKhongThuTien()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private class c_KPhong
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
        List<c_KPhong> _Kphong = new List<c_KPhong>();
        List<KPhong> _lkp = new List<KPhong>();
        List<CanBo> _lcb = new List<CanBo>();
        List<DTBN> _ldtbn = new List<DTBN>();
        private void Frm_ThHoaSinhMauSL_Load(object sender, EventArgs e)
        {
            _lkp = _Data.KPhongs.ToList();
            _lcb = _Data.CanBoes.ToList();
            _ldtbn = _Data.DTBNs.ToList();
            _ldtbn.Add(new DTBN { DTBN1 = "Tất cả", IDDTBN = 9 });
            lup_DTuongBN.Properties.DataSource = _ldtbn;
            LupNgaytu.DateTime = System.DateTime.Now;
            LupNgayden.DateTime = System.DateTime.Now;
            _Kphong.Clear();
            List<NhomDV> _nhom= (from nhom in _Data.NhomDVs
                                                where nhom.Status==1
                                     select nhom).Distinct().ToList();
            _nhom.Add(new NhomDV { TenNhomCT = " Tất cả"});
            lup_NhomXN.Properties.DataSource = _nhom.ToList().OrderBy(p => p.TenNhomCT);
            var kphong = (from kp in _Data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                c_KPhong themmoi1 = new c_KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    c_KPhong themmoi = new c_KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
            lup_DTuongBN.Text = "Tất cả";
            lup_NhomXN.Text = " Tất cả";
        }
        public class c_DSDV
        {
            private string _tenDVct, _madvct;
            int madv;

            public string TenDV
            {
                set { _tenDVct = value; }
                get { return _tenDVct; }
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
        }
        public class c_BN_KQ
        {
            private string _mabn, _madvct;
            private string _ketqua;
            private int _idcls;
            public int IDCLS
            { set { _idcls = value; } get { return _idcls; } }
            public string MaBNhan
            {
                set { _mabn = value; }
                get { return _mabn; }
            }
            public string MaDVct
            {
                set { _madvct = value; }
                get { return _madvct; }
            }
            public string KetQua
            {
                set { _ketqua = value; }
                get { return _ketqua; }
            }
        }
        public class BenhNhan
        {
            private string TenBN;
            private int MaBN;
            private string Gioitinh;
            private int Tuoi;
            private string Diachi;
            private string Chandoan;
            private string Noigui;
            private string Yeucau;
            private int MaKP;
            private string BHYT;
            private string Ngaythang;
            private string madvct;
            public string ngaythang
            { set { Ngaythang = value; } get { return Ngaythang; } }
            private DateTime NgayVao;
            public DateTime ngayvao
            { set { NgayVao = value; } get { return NgayVao; } }
            private string Ngayra;
            public string ngayra
            { set { Ngayra = value; } get { return Ngayra; } }
            public string bhyt
            { set { BHYT = value; } get { return BHYT; } }
            int madv;
            public int MaDV
            { set { madv = value; } get { return madv; } }
            public int MaBNhan
            { set { MaBN = value; } get { return MaBN; } }
            public string TenBNhan
            { set { TenBN = value; } get { return TenBN; } }
            public string gioitinh
            { set { Gioitinh = value; } get { return Gioitinh; } }
            string _gioitinh_nu;
            public string gioitinh_nu
            { set { _gioitinh_nu = value; } get { return _gioitinh_nu; } }
            public int tuoi
            { set { Tuoi = value; } get { return Tuoi; } }
            public string diachi
            { set { Diachi = value; } get { return Diachi; } }
            public string chandoan
            { set { Chandoan = value; } get { return Chandoan; } }
            public string noigui
            { set { Noigui = value; } get { return Noigui; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public string MaDVct
            { set { madvct = value; } get { return madvct; } }
            private int idcls;
            public int IDCLS
            { set { idcls = value; } get { return idcls; } }
            private int sttht;
            public int STTHT
            { set { sttht = value; } get { return sttht; } }
            string colkq1;
            public string colKQ1 { set { colkq1 = value; } get { return colkq1; } }
            string colkq2;
            public string colKQ2 { set { colkq2 = value; } get { return colkq2; } }
            string colkq3;
            public string colKQ3 { set { colkq3 = value; } get { return colkq3; } }
            string colkq4;
            public string colKQ4 { set { colkq4 = value; } get { return colkq4; } }
            string colkq5;
            public string colKQ5 { set { colkq5 = value; } get { return colkq5; } }
            string colkq6;
            public string colKQ6 { set { colkq6 = value; } get { return colkq6; } }
            string colkq7;
            public string colKQ7 { set { colkq7 = value; } get { return colkq7; } }
            string colkq8;
            public string colKQ8 { set { colkq8 = value; } get { return colkq8; } }
            string colkq9;
            public string colKQ9 { set { colkq9 = value; } get { return colkq9; } }
            string colkq10;
            public string colKQ10 { set { colkq10 = value; } get { return colkq10; } }
            string colkq11;
            public string colKQ11 { set { colkq11 = value; } get { return colkq11; } }
            string colkq12;
            public string colKQ12 { set { colkq12 = value; } get { return colkq12; } }
            string colkq13;
            public string colKQ13 { set { colkq13 = value; } get { return colkq13; } }
            string colkq14;
            public string colKQ14 { set { colkq14 = value; } get { return colkq14; } }
            string colkq15;
            public string colKQ15 { set { colkq15 = value; } get { return colkq15; } }
            string colkq16;
            public string colKQ16 { set { colkq16 = value; } get { return colkq16; } }
            string colkq17;
            public string colKQ17 { set { colkq17 = value; } get { return colkq17; } }
            string colkq18;
            public string colKQ18 { set { colkq18 = value; } get { return colkq18; } }
            string colkq19;
            public string colKQ19 { set { colkq19 = value; } get { return colkq19; } }
            string colkq20;
            public string colKQ20 { set { colkq20 = value; } get { return colkq20; } }
            string colkq21;
            public string colKQ21 { set { colkq21 = value; } get { return colkq21; } }
            string colkq22;
            public string colKQ22 { set { colkq22 = value; } get { return colkq22; } }
            string colkq23;
            public string colKQ23 { set { colkq23 = value; } get { return colkq23; } }
            string colkq24;
            public string colKQ24 { set { colkq24 = value; } get { return colkq24; } }
            string colkq25;
            public string colKQ25 { set { colkq25 = value; } get { return colkq25; } }
            string colkq26;
            public string colKQ26 { set { colkq26 = value; } get { return colkq26; } }
            string colkq27;
            public string colKQ27 { set { colkq27 = value; } get { return colkq27; } }
            string colkq28;
            public string colKQ28 { set { colkq28 = value; } get { return colkq28; } }
            string colkq29;
            public string colKQ29 { set { colkq29 = value; } get { return colkq29; } }
            string colkq30;
            public string colKQ30 { set { colkq30 = value; } get { return colkq30; } }
            string colkq;
            public string KetQua { set { colkq = value; } get { return colkq; } }
            string ketluan;
            public string KetLuan { set { ketluan = value; } get { return ketluan; } }
            string tendvct;
            public string TenDVct { set { tendvct = value; } get { return tendvct; } }
            string tendv;
            public string TenDV { set { tendv = value; } get { return tendv; } }
              string yeucau;
              public string YeuCau { set { yeucau = value; } get { return yeucau; } }

              string tenCBth;
              public string TenCBth { set { tenCBth = value; } get { return tenCBth; } }
              string tenCBcd;
              public string TenCBcd { set { tenCBcd = value; } get { return tenCBcd; } }
              double thanhtien;

              public double Thanhtien
              {
                  get { return thanhtien; }
                  set { thanhtien = value; }
              }
              double soluong;

              public double Soluong
              {
                  get { return soluong; }
                  set { soluong = value; }
              }
              string soba;

              public string SoBA
              {
                  get { return soba; }
                  set { soba = value; }
              }
              string ghiChu;

              public string GhiChu
              {
                  get { return ghiChu; }
                  set { ghiChu = value; }
              }
        }


        private void butTaoBC_Click(object sender, EventArgs e)
        {
            List<BenhNhan> _BenhNhan = new List<BenhNhan>();
            List<c_DSDV> _lDichVu = new List<c_DSDV>();
            List<c_BN_KQ> _lBN_KQ = new List<c_BN_KQ>();
            List<BenhNhan> _Tong = new List<BenhNhan>();
            _BenhNhan.Clear();
            DateTime _ngayTu = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime _ngayDen = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            string _tenbc = "BẢNG KÊ ";
            string _tenTN = " Tất cả";
            if (lup_NhomXN.EditValue != null)
            {
                if (lup_NhomXN.Text != " Tất cả")
                    _tenbc += lup_NhomXN.EditValue.ToString().ToUpper();
                else
                    _tenbc += "thuốc, vtyt tiêu hao ".ToUpper();
                _tenTN = lup_NhomXN.EditValue.ToString();
            }
            if (cbo_loaiCP.SelectedIndex < 2)
                _tenbc +=" "+ cbo_loaiCP.Text.ToUpper();
            if (chk_TT.Checked==false)
            {
                _Tong = (from
                             cls in _Data.DThuocs.Where(p => p.NgayKe >= _ngayTu).Where(p => p.NgayKe <= _ngayDen) 
                         join chidinh in _Data.DThuoccts on cls.IDDon equals chidinh.IDDon
                         select new BenhNhan
                              {
                              tuoi=chidinh.TrongBH==null?0: chidinh.TrongBH,
                                  MaBNhan = cls.MaBNhan == null ? 0 : cls.MaBNhan.Value,
                                  makp = cls.MaKP == null ? 0 : cls.MaKP.Value,
                                  MaDV = chidinh.MaDV == null ? 0 : chidinh.MaDV.Value,
                                  Soluong = chidinh.SoLuong == null ? 0 : chidinh.SoLuong,
                                  Thanhtien = chidinh.ThanhTien == null ? 0 : chidinh.ThanhTien,
                              }).ToList();
            }
            else
            { //đã thanh toán
                _Tong = (from  vienphi in _Data.VienPhis
                       join   cls in _Data.DThuocs.Where(p => p.NgayKe >= _ngayTu).Where(p => p.NgayKe <= _ngayDen) on vienphi.MaBNhan equals cls.MaBNhan
                         join chidinh in _Data.DThuoccts on cls.IDDon equals chidinh.IDDon
                         select new BenhNhan
                              {
                                  tuoi = chidinh.TrongBH ,
                                  MaBNhan = cls.MaBNhan == null ? 0 : cls.MaBNhan.Value,
                                  makp = cls.MaKP == null ? 0 : cls.MaKP.Value,
                                  MaDV = chidinh.MaDV == null ? 0 : chidinh.MaDV.Value,
                                  Soluong = chidinh.SoLuong == null ? 0 : chidinh.SoLuong,
                                  Thanhtien = chidinh.ThanhTien == null ? 0 : chidinh.ThanhTien,
                              }).ToList();
            }
            int _idDTBN = 9;
            if (lup_DTuongBN.EditValue != null)
                _idDTBN = Convert.ToByte(lup_DTuongBN.EditValue);
            _Tong =  (from a in _Tong join bn in _Data.BenhNhans on a.MaBNhan equals bn.MaBNhan
                      where (_idDTBN==9?true: bn.IDDTBN==_idDTBN)
                             && (cbo_NoiTru.SelectedIndex == 2 ? true : bn.NoiTru == cbo_NoiTru.SelectedIndex)
                             && (cbo_loaiCP.SelectedIndex == 0 ? a.tuoi ==2 :(cbo_loaiCP.SelectedIndex == 1? (a.tuoi == 1|| a.tuoi==0):true))
                             select new BenhNhan
                             {
                              
                                 TenBNhan = bn.TenBNhan,
                                 bhyt = bn.DTuong == "BHYT" ? "X" : "",
                                 gioitinh = bn.GTinh == 1 ? "Nam" : "Nữ",
                                 tuoi = bn.Tuoi == null ? 0 : bn.Tuoi.Value,
                                 diachi = bn.DChi,
                                 MaBNhan = a.MaBNhan,
                                 makp = a.makp,
                                 MaDV = a.MaDV,
                                 Soluong=a.Soluong,
                                 Thanhtien=a.Thanhtien,
                                 // chidinh.KetLuan
                             }).ToList();
            var tenDVct = (from dv in _Data.DichVus 
                           join tnhom in _Data.NhomDVs on dv.IDNhom equals tnhom.IDNhom
                           where (_tenTN == " Tất cả" ? tnhom.Status == 1 : tnhom.TenNhomCT == _tenTN)
                           select new
                           {
                               dv.TenDV,
                               dv.MaDV,
                           }
                               ).Distinct().ToList();

            _Tong = (from a in _Tong
                     join b in _Kphong.Where(p => p.chon == true) on a.makp equals b.makp
                     join dvct in tenDVct on a.MaDV equals dvct.MaDV
                     select new BenhNhan
                           {
                               STTHT = a.STTHT,
                               TenBNhan = a.TenBNhan,
                               bhyt = a.bhyt,
                               gioitinh = a.gioitinh,
                               tuoi = a.tuoi,
                               diachi = a.diachi,
                               IDCLS = a.IDCLS,
                               MaBNhan = a.MaBNhan,
                               makp = a.makp,
                               MaDV = a.MaDV,
                               MaDVct = a.MaDVct,
                               TenDV = dvct.TenDV,
                               Soluong=RadDK.SelectedIndex == 0?a.Soluong: a.Thanhtien,
                               // chidinh.KetLuan
                           }).ToList();
            _Tong = (from a in _Tong
                     join b in _Data.VaoViens on a.MaBNhan equals b.MaBNhan 
                     //from b in kq.DefaultIfEmpty()
                     select new BenhNhan
                     {
                         STTHT = a.STTHT,
                         TenBNhan = a.TenBNhan,
                         bhyt = a.bhyt,
                         gioitinh = a.gioitinh,
                         tuoi = a.tuoi,
                         diachi = a.diachi,
                         IDCLS = a.IDCLS,
                         MaBNhan = a.MaBNhan,
                         makp = a.makp,
                         ngaythang = b.NgayVao == null ? "" : b.NgayVao.Value.ToString("dd/MM/yy"),
                         MaDV = a.MaDV,
                         MaDVct = a.MaDVct,
                         TenDV = a.TenDV,
                         Soluong =a.Soluong,
                         SoBA=b.SoVV,
                         ngayvao=b.NgayVao==null? DateTime.Now: b.NgayVao.Value,
                         // chidinh.KetLuan
                     }).ToList();
            List<RaVien> _lrv = new List<RaVien>();
            _lrv=(from a in _Tong join rv in _Data.RaViens on a.MaBNhan equals rv.MaBNhan
                 select rv).ToList();
            _lDichVu = (from a in _Tong
                        group a by new { a.TenDV, a.MaDV } into kq
                        select new c_DSDV { TenDV = kq.Key.TenDV, MaDV = kq.Key.MaDV }).OrderBy(p => p.MaDV).ThenBy(p => p.TenDV).ToList();

            int[] _lTendvct = new int[1000];
            for (int i = 0; i < 1000; i++)
            {
                _lTendvct[i] = 0;
            }
            for (int i = 0; i < _lDichVu.Count; i++)
            {
                _lTendvct[i] = _lDichVu.Skip(i).Take(1).First().MaDV;
            }
            List<BenhNhan> _lbenhNhan_gr = new List<BenhNhan>();
             
                    _BenhNhan = (from a in _Tong
                                 group a by new {a.ngayvao,a.SoBA, a.ngaythang, a.MaBNhan, a.TenBNhan, a.diachi, a.gioitinh, a.tuoi, a.bhyt} into kq
                                 select new BenhNhan
                                 {
                                  ngayvao=kq.Key.ngayvao,
                                     SoBA=kq.Key.SoBA,
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     diachi = kq.Key.diachi,
                                     gioitinh = kq.Key.gioitinh,
                                     tuoi = kq.Key.tuoi,
                                     bhyt = kq.Key.bhyt,
                                     ngaythang = kq.Key.ngaythang,
                                     colKQ1 = kq.Where(p => p.MaDV == _lTendvct[0]).Select(p => p.Soluong) == null ? "": kq.Where(p => p.MaDV == _lTendvct[0]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ2 = kq.Where(p => p.MaDV == _lTendvct[1]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[1]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ3 = kq.Where(p => p.MaDV == _lTendvct[2]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[2]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ4 = kq.Where(p => p.MaDV == _lTendvct[3]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[3]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ5 = kq.Where(p => p.MaDV == _lTendvct[4]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[4]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ6 = kq.Where(p => p.MaDV == _lTendvct[5]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[5]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ7 = kq.Where(p => p.MaDV == _lTendvct[6]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[6]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ8 = kq.Where(p => p.MaDV == _lTendvct[7]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[7]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ9 = kq.Where(p => p.MaDV == _lTendvct[8]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[8]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ10 = kq.Where(p => p.MaDV == _lTendvct[9]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[9]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ11 = kq.Where(p => p.MaDV == _lTendvct[10]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[10]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ12 = kq.Where(p => p.MaDV == _lTendvct[11]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[11]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ13 = kq.Where(p => p.MaDV == _lTendvct[12]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[12]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ14 = kq.Where(p => p.MaDV == _lTendvct[13]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[13]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ15 = kq.Where(p => p.MaDV == _lTendvct[14]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[14]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ16 = kq.Where(p => p.MaDV == _lTendvct[15]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[15]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ17 = kq.Where(p => p.MaDV == _lTendvct[16]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[16]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ18 = kq.Where(p => p.MaDV == _lTendvct[17]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[17]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ19 = kq.Where(p => p.MaDV == _lTendvct[18]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[18]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ20 = kq.Where(p => p.MaDV == _lTendvct[19]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[19]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ21 = kq.Where(p => p.MaDV == _lTendvct[20]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[20]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ22 = kq.Where(p => p.MaDV == _lTendvct[21]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[21]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ23 = kq.Where(p => p.MaDV == _lTendvct[22]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[22]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ24 = kq.Where(p => p.MaDV == _lTendvct[23]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[23]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ25 = kq.Where(p => p.MaDV == _lTendvct[24]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[24]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ26 = kq.Where(p => p.MaDV == _lTendvct[25]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[25]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ27 = kq.Where(p => p.MaDV == _lTendvct[26]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[26]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ28 = kq.Where(p => p.MaDV == _lTendvct[27]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[27]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ29 = kq.Where(p => p.MaDV == _lTendvct[28]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[28]).Sum(p => p.Soluong).ToString("##,###.##"),
                                     colKQ30 = kq.Where(p => p.MaDV == _lTendvct[29]).Select(p => p.Soluong) == null ? "" : kq.Where(p => p.MaDV == _lTendvct[29]).Sum(p => p.Soluong).ToString("##,###.##"),
                                 }).OrderBy(p => p.ngayvao).ThenBy(p => p.TenBNhan).ToList();
                    _lbenhNhan_gr = (from a in _Tong
                                     group a by a.MaDV into kq
                                     select new BenhNhan
                                     {
                                         MaDV = kq.Key,
                                         Soluong = kq.Sum(p => p.Soluong),

                                     }).ToList();
                
         

                foreach (var a in _BenhNhan)
                {
                    a.ngayra = _lrv.Where(p => p.MaBNhan == a.MaBNhan).ToList().Count > 0 ? _lrv.Where(p => p.MaBNhan == a.MaBNhan).ToList().First().NgayRa.Value.ToString("dd/MM/yy") : "";
                    a.colKQ1 = a.colKQ1 == "0" ? "" : a.colKQ1;
                    a.colKQ2 = a.colKQ2 == "0" ? "" : a.colKQ2;
                    a.colKQ3 = a.colKQ3 == "0" ? "" : a.colKQ3;
                    a.colKQ4 = a.colKQ4 == "0" ? "" : a.colKQ4;
                    a.colKQ5 = a.colKQ5 == "0" ? "" : a.colKQ5;
                    a.colKQ6 = a.colKQ6 == "0" ? "" : a.colKQ6;
                    a.colKQ7 = a.colKQ7 == "0" ? "" : a.colKQ7;
                    a.colKQ8 = a.colKQ8 == "0" ? "" : a.colKQ8;
                    a.colKQ9 = a.colKQ9 == "0" ? "" : a.colKQ9;
                    a.colKQ10 = a.colKQ10 == "0" ? "" : a.colKQ10;
                    a.colKQ11 = a.colKQ11 == "0" ? "" : a.colKQ11;
                    a.colKQ12 = a.colKQ12 == "0" ? "" : a.colKQ12;
                    a.colKQ13 = a.colKQ13 == "0" ? "" : a.colKQ13;
                    a.colKQ14 = a.colKQ14 == "0" ? "" : a.colKQ14;
                    a.colKQ15 = a.colKQ15 == "0" ? "" : a.colKQ15;
                    a.colKQ16 = a.colKQ16 == "0" ? "" : a.colKQ16;
                    a.colKQ17 = a.colKQ17 == "0" ? "" : a.colKQ17;
                    a.colKQ18 = a.colKQ18 == "0" ? "" : a.colKQ18;
                    a.colKQ19 = a.colKQ19 == "0" ? "" : a.colKQ19;
                    a.colKQ20 = a.colKQ20 == "0" ? "" : a.colKQ20;
                    a.colKQ21 = a.colKQ21 == "0" ? "" : a.colKQ21;
                    a.colKQ22 = a.colKQ22 == "0" ? "" : a.colKQ22;
                    a.colKQ23 = a.colKQ23 == "0" ? "" : a.colKQ23;
                    a.colKQ24 = a.colKQ24 == "0" ? "" : a.colKQ24;
                    a.colKQ25 = a.colKQ25 == "0" ? "" : a.colKQ25;
                    a.colKQ26 = a.colKQ26 == "0" ? "" : a.colKQ26;
                    a.colKQ27 = a.colKQ27 == "0" ? "" : a.colKQ27;
                    a.colKQ28 = a.colKQ28 == "0" ? "" : a.colKQ28;
                    a.colKQ29 = a.colKQ29 == "0" ? "" : a.colKQ29;
                    a.colKQ30 = a.colKQ30 == "0" ? "" : a.colKQ30;
                    if(DungChung.Bien.MaBV=="27001")
                    a.GhiChu = a.MaBNhan==null?"": a.MaBNhan.ToString();
                }
          





            BaoCao.rep_frm_TKDichVuKhongThuTien rep1 = new BaoCao.rep_frm_TKDichVuKhongThuTien(_lDichVu, _lbenhNhan_gr);
                rep1.TenBC.Value = _tenbc;
                rep1.TuNgay.Value = _ngayTu.ToShortDateString();
                rep1.DenNgay.Value= _ngayDen.ToShortDateString();
                rep1.DataSource = _BenhNhan;
                rep1.BindingData();
                rep1.CreateDocument();
                frmIn frm1 = new frmIn();
                frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                frm1.ShowDialog();
       
            
            

        }
        private void ButHuy_Click(object sender, EventArgs e)
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