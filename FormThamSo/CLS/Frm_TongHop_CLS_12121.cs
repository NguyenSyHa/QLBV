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
    public partial class Frm_TongHop_CLS_12121 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TongHop_CLS_12121()
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
        List<TieuNhomDV> _ltnhom = new List<TieuNhomDV>();
        int _idDTBN = 99;
        private void Frm_ThHoaSinhMauSL_Load(object sender, EventArgs e)
        {
            radTrongDM.SelectedIndex = 2;
            _lkp = _Data.KPhongs.ToList();
            _lcb = _Data.CanBoes.ToList();
            LupNgaytu.DateTime = System.DateTime.Now;
            LupNgayden.DateTime = System.DateTime.Now;
            _Kphong.Clear();
            _ltnhom = _Data.TieuNhomDVs.ToList();
            List<DTBN> _lDTBN = _Data.DTBNs.ToList();
            _lDTBN.Add(new DTBN {IDDTBN=99,DTBN1="Tất cả" });
            cbo_DoiTuong.Properties.DataSource = _lDTBN.OrderBy(p=>p.IDDTBN);
            lup_NhomXN.Properties.DataSource = (from tn in _Data.TieuNhomDVs
                                                join nhom in _Data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                                                where nhom.TenNhomCT == "Xét nghiệm" || nhom.TenNhomCT == ("Chẩn đoán hình ảnh") || nhom.TenNhomCT == "Thủ thuật, phẫu thuật"
                                                select new { tn.TenRG }).Distinct().OrderBy(p => p).ToList();
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
                    themmoi.chon = false;
                    _Kphong.Add(themmoi);
                }
                cklKP.DataSource = _Kphong.ToList();
            }
            cklKP.CheckAll();
      
            //ckl_DichVu.DataSource = _lDichVu;
            //ckl_DichVu.CheckAll();
            load++;
        }
        public class c_DSDV
        {
            private string _tenDVct, _madvct;
            private int madv;

            public string TenDVct
            {
                set { _tenDVct = value; }
                get { return _tenDVct; }
            }
            public string MaDVct
            {
                set { _madvct = value; }
                get { return _madvct; }
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
        }
        public class c_BN_KQ
        {
            private string _madvct;
            private string _ketqua;
            private int _idcls, _mabn;
            public int IDCLS
            { set { _idcls = value; } get { return _idcls; } }
            public int MaBNhan
            {
                set { _mabn = value; }
                get { return _mabn; }
            }
            public string madvct
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
        #region class BenhNhan
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
            private int makp;
            private string BHYT;
            private DateTime Ngaythang;
            private string madvct;
            int loaiTTPT;

            public int LoaiTTPT
            {
                get { return loaiTTPT; }
                set { loaiTTPT = value; }
            }
            string loiDan;

            public string LoiDan
            {
                get { return loiDan; }
                set { loiDan = value; }
            }
            public DateTime ngaythang
            { set { Ngaythang = value; } get { return Ngaythang; } }
            DateTime ngayTH;

            public DateTime NgayTH
            {
                get { return ngayTH; }
                set { ngayTH = value; }
            }
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
            public int MaKP
            { set { makp = value; } get { return makp; } }
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
            string dSCBTH;

            public string DSCBTH
            {
                get { return dSCBTH; }
                set { dSCBTH = value; }
            }

        }
        #endregion
        private string _BSGMe(string s) {
            try
            {
                string[] _a = s.Split(';');
                if (_a.Length > 0 && _a[1] != null)
                    return _a[1];
                return "";
            }catch(Exception){
            return "";
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
            if (cbo_DoiTuong.EditValue != null)
                _idDTBN = Convert.ToInt32(cbo_DoiTuong.EditValue);
            foreach (var item in _lDichVu_chon)
            {
                
                    item.Check = false;
            }
            for (int i = 0; i < ckl_DichVu.ItemCount; i++)
            {
                if (ckl_DichVu.GetItemChecked(i))
                {
                    int madv = Convert.ToInt32(ckl_DichVu.GetItemValue(i));
                    foreach (var item in _lDichVu_chon)
                    {
                        if (item.MaDV == madv)
                        {
                            item.Check = true;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                {
                    int makp = Convert.ToInt32(cklKP.GetItemValue(i));
                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp && item.makp!=0)
                        {
                            item.chon = true;
                            break;
                        }
                    }
                }
            }
            string _tenbc = "";
            if (lup_NhomXN.EditValue != null)
            {
                _tenbc = lup_NhomXN.EditValue.ToString();
            }
            bool _CDHA = false;
            bool _TTPT = false;
            if (_tenbc == "Thủ thuật" || _tenbc == "Phẫu thuật")
                _TTPT = true;
            if (_tenbc == "Siêu âm" || _tenbc == "X-Quang" || _tenbc == "Điện tim" || _tenbc == "Nội soi")
            {
                _CDHA = true;
            }
            if (cboTT.SelectedIndex == 0)
            {
                _Tong = (from
                             cls in _Data.CLS.Where(p => p.NgayTH >= _ngayTu).Where(p => p.NgayTH <= _ngayDen)
                         join chidinh in _Data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals chidinh.IdCLS
                         join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                         where radTrongDM.SelectedIndex == 2 ? true : chidinh.TrongBH == radTrongDM.SelectedIndex
                         select new BenhNhan
                              {
                                  YeuCau = chidinh.ChiDinh1,
                                  STTHT = clsct.STTHT == null ? 0 : clsct.STTHT.Value,
                                  IDCLS = cls.IdCLS,
                                  MaBNhan = cls.MaBNhan == null ? 0 : cls.MaBNhan.Value,
                                  MaKP = cls.MaKP == null ? 0 : cls.MaKP.Value,
                                  ngaythang = cls.NgayTH == null ? _ngayTu : cls.NgayTH.Value,
                                 DSCBTH=cls.DSCBTH,
                                 LoiDan=chidinh.LoiDan,
                                  MaDV = chidinh.MaDV == null ? 0 : chidinh.MaDV.Value,
                                  //dvct.TenDVct,
                                  KetLuan = chidinh.KetLuan,
                                  MaDVct = clsct.MaDVct,
                                  KetQua = clsct.KetQua,
                                  TenCBcd = cls.MaCB == null ? "" : cls.MaCB,
                                  TenCBth = cls.MaCBth == null ? "" : cls.MaCBth,
                                  // chidinh.KetLuan
                              }).ToList();
            }
            else
            { //theo ngày thanh toán
                _Tong = (from vienphi in _Data.VienPhis.Where(p => p.NgayTT >= _ngayTu).Where(p => p.NgayTT <= _ngayDen)
                         join cls in _Data.CLS on vienphi.MaBNhan equals cls.MaBNhan
                         join chidinh in _Data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals chidinh.IdCLS
                         join clsct in _Data.CLScts.Where(p => p.Status == 1) on chidinh.IDCD equals clsct.IDCD
                         select new BenhNhan
                         {
                             YeuCau = chidinh.ChiDinh1,
                             STTHT = clsct.STTHT == null ? 0 : clsct.STTHT.Value,
                             IDCLS = cls.IdCLS,
                             MaBNhan = cls.MaBNhan == null ? 0 : cls.MaBNhan.Value,
                             MaKP = cls.MaKP == null ? 0 : cls.MaKP.Value,
                             ngaythang = cls.NgayTH == null ? _ngayTu : cls.NgayTH.Value,
                             DSCBTH = cls.DSCBTH,
                             LoiDan = chidinh.LoiDan,
                             MaDV = chidinh.MaDV == null ? 0 : chidinh.MaDV.Value,
                             //dvct.TenDVct,
                             MaDVct = clsct.MaDVct,
                             KetQua = clsct.KetQua,
                             KetLuan = chidinh.KetLuan,
                             TenCBcd = cls.MaCB == null ? "" : cls.MaCB,
                             TenCBth = cls.MaCBth == null ? "" : cls.MaCBth,
                             // chidinh.KetLuan
                         }).ToList();
            }

            _Tong = (from a in _Tong
                     join bn in _Data.BenhNhans on a.MaBNhan equals bn.MaBNhan
                     where (_idDTBN == 99 ? true : bn.IDDTBN == _idDTBN)
                     && (cbo_NoiTru.SelectedIndex == 2 ? true : bn.NoiTru == cbo_NoiTru.SelectedIndex)
                     select new BenhNhan
                     {
                         DSCBTH=a.DSCBTH,
                         YeuCau = a.YeuCau,
                         STTHT = a.STTHT,
                         TenBNhan = bn.TenBNhan,
                         bhyt = bn.SThe,
                         gioitinh = bn.GTinh == 1 ? "Nam" : "Nữ",
                         tuoi = bn.Tuoi == null ? 0 : bn.Tuoi.Value,
                         diachi = bn.DChi,
                         IDCLS = a.IDCLS,
                         MaBNhan = a.MaBNhan,
                         MaKP = a.MaKP,
                         ngaythang = a.ngaythang,
                         MaDV = a.MaDV,
                         LoiDan=a.LoiDan,
                         //dvct.TenDVct,
                         KetLuan = a.KetLuan,
                         MaDVct = a.MaDVct,
                         KetQua = a.KetQua,
                         TenCBcd = a.TenCBcd,
                         TenCBth = a.TenCBth,
                         // chidinh.KetLuan
                     }).ToList();

       
            var tenDVct2 = (from dvct in _Data.DichVucts
                            join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                            select new
                            {
                                dv.MaDV,
                                dv.TenDV,
                                dvct.TenDVct,
                                dvct.MaDVct,
                                dv.Loai,
                            }
                               ).Distinct().ToList();
            var tenDVct = (from dvct in tenDVct2
                           join dv in _lDichVu_chon.Where(p => p.Check) on dvct.MaDV equals dv.MaDV
                           select new
                           {
                               dv.MaDV,
                               dv.TenDV,
                               dvct.TenDVct,
                               dvct.MaDVct,
                               dvct.Loai,
                           }
                           ).Distinct().ToList();

            _Tong = (from a in _Tong
                     join b in _Kphong.Where(p => p.chon == true) on a.MaKP equals b.makp
                     join dvct in tenDVct on a.MaDVct equals dvct.MaDVct
                     select new BenhNhan
                           {
                               LoaiTTPT=dvct.Loai??0,
                               STTHT = a.STTHT,
                               TenBNhan = a.TenBNhan,
                               bhyt =chk_HT_TheBH.Checked? a.bhyt: (a.bhyt.Length>=15 ?"X":""),
                               gioitinh = a.gioitinh,
                               tuoi = a.tuoi,
                               diachi = a.diachi,
                               IDCLS = a.IDCLS,
                               MaBNhan = a.MaBNhan,
                               MaKP = a.MaKP,
                               ngaythang = a.ngaythang,
                               LoiDan = _TTPT?a.TenDV+" - "+ a.LoiDan :a.LoiDan,
                               MaDV = a.MaDV,
                               KetLuan = _tenbc == "X-Quang" ?( a.KetLuan + a.KetQua) : a.KetLuan,
                               MaDVct = a.MaDVct,
                               KetQua = a.KetQua == null ? "" : a.KetQua,
                               TenDVct = dvct.TenDVct,
                               TenDV = dvct.TenDV,
                               TenCBcd = a.TenCBcd,
                               TenCBth = a.TenCBth,
                               YeuCau = a.YeuCau,
                               DSCBTH=a.DSCBTH,
                               // chidinh.KetLuan
                           }).ToList();

            #region bệnh nhân nhập dịch vụ trực tiếp
            // dịch vụ nhập trực tiếp
            if (chk_DV_KD.Checked)
            {
                if (cboTT.SelectedIndex == 0)
                {
                    var donthuoc = (from dt in _Data.DThuocs.Where(p => p.PLDV == 2)
                                    join dtct in _Data.DThuoccts.Where(p => p.NgayNhap >= _ngayTu).Where(p => p.NgayNhap <= _ngayDen) on dt.IDDon equals dtct.IDDon
                                    select new { dtct.DSCBTH, dtct.MaKP, dtct.NgayNhap, dt.MaBNhan, dtct.IDCD, dtct.MaDV, dtct.MaCB }).ToList().Where(p => p.IDCD == 0 || p.IDCD == null).ToList();
                    var donthuoc2 = (from dt1 in donthuoc
                                     join dv in _lDichVu_chon.Where(p => p.Check) on dt1.MaDV equals dv.MaDV
                                     join kp in _Kphong.Where(p => p.chon) on dt1.MaKP equals kp.makp
                                     select new
                                     {
                                         dt1.MaDV,
                                         dt1.MaBNhan,
                                         dt1.MaKP,
                                         dt1.DSCBTH,
                                         dt1.NgayNhap,
                                         dt1.MaCB,
                                         dv.TenDV,
                                         dv.IdTieuNhom
                                     }).ToList();
                    var abc = (from a in donthuoc2
                               join bn in _Data.BenhNhans on a.MaBNhan equals bn.MaBNhan
                               select new
                               {
                                     bn.IDDTBN,
                                   a.TenDV,
                                   bn.DTuong,
                                   bn.NoiTru,
                                   YeuCau = "",
                                   STTHT = 0,// a.STTHT,
                                   TenBNhan = bn.TenBNhan,
                                   bhyt = bn.SThe,
                                   gioitinh = bn.GTinh == 1 ? "Nam" : "Nữ",
                                   tuoi = bn.Tuoi == null ? 0 : bn.Tuoi.Value,
                                   diachi = bn.DChi,
                                   IDCLS = 1,// a.IDCLS,
                                   MaBNhan = a.MaBNhan ?? 0,
                                   MaKP = a.MaKP ?? 0,
                                   ngaythang = a.NgayNhap,
                                   MaDV = a.MaDV ?? 0,
                                   LoiDan = a.TenDV, //a.LoiDan,
                                   //dvct.TenDVct,
                                   KetLuan = "a.KetLuan",
                                   MaDVct = "",
                                   KetQua = "a.KetQua",
                                   TenCBcd = a.MaCB,  //"a.TenCBcd",
                                   DSCBTH = a.DSCBTH,
                                   // chidinh.KetLuan
                               }).ToList();
                    List<BenhNhan> _lTong_dt = (from a in abc
                                                where (_idDTBN == 99 ? true : a.IDDTBN == _idDTBN)
                                                && (cbo_NoiTru.SelectedIndex == 2 ? true : a.NoiTru == cbo_NoiTru.SelectedIndex)
                                                select new BenhNhan
                                                {
                                                    // DSCBTH = a.DSCBTH,
                                                    YeuCau = "",
                                                    STTHT = 0,// a.STTHT,
                                                TenDV=    a.TenDV,
                                                    TenBNhan = a.TenBNhan,
                                                    bhyt = chk_HT_TheBH.Checked ? a.bhyt : (a.bhyt.Length >= 15 ? "X" : ""),
                                                    gioitinh = a.gioitinh == "Nam" ? a.tuoi.ToString() : "",
                                                    gioitinh_nu = a.gioitinh == "Nữ" ? a.tuoi.ToString() : "",
                                                    tuoi = a.tuoi,
                                                    diachi = a.diachi,
                                                    IDCLS = 1,// a.IDCLS,
                                                    MaBNhan = a.MaBNhan,
                                                    MaKP = a.MaKP,
                                                    ngaythang = a.ngaythang ?? DateTime.Now,
                                                    MaDV = a.MaDV,
                                                    LoiDan = "", //a.LoiDan,
                                                    //dvct.TenDVct,
                                                    KetLuan = a.TenDV,
                                                    MaDVct = a.MaDV.ToString(),
                                                    TenDVct=a.TenDV,
                                                    KetQua = "",
                                                    TenCBcd = a.TenCBcd,  //"a.TenCBcd",
                                                    DSCBTH = a.DSCBTH,
                                                    // chidinh.KetLuan
                                                }).ToList();
                    if (_TTPT)
                    foreach (var item in _lTong_dt)
                    {
                        item.DSCBTH = _BSGMe(item.DSCBTH);

                    }
                    _Tong.AddRange(_lTong_dt);
                }
                else
                {
                    var donthuoc = (from vp in _Data.VienPhis.Where(p => p.NgayTT >= _ngayTu).Where(p => p.NgayTT <= _ngayDen)
                                    join dt in _Data.DThuocs.Where(p => p.PLDV == 2) on vp.MaBNhan equals dt.MaBNhan
                                    join dtct in _Data.DThuoccts on dt.IDDon equals dtct.IDDon
                                    select new { dtct.DSCBTH, dtct.MaKP, dtct.NgayNhap, dt.MaBNhan, dtct.IDCD, dtct.MaDV, dtct.MaCB }).ToList().Where(p => p.IDCD == 0 || p.IDCD == null).ToList();
                    var donthuoc2 = (from dt1 in donthuoc
                                     join dv in _lDichVu_chon.Where(p => p.Check) on dt1.MaDV equals dv.MaDV
                                     join kp in _Kphong.Where(p => p.chon) on dt1.MaKP equals kp.makp
                                     select new
                                     {
                                         dt1.MaDV,
                                         dt1.MaBNhan,
                                         dt1.MaKP,
                                         dt1.DSCBTH,
                                         dt1.NgayNhap,
                                         dt1.MaCB,
                                         dv.TenDV,
                                         dv.IdTieuNhom
                                     }).ToList();
                    var abc = (from a in donthuoc2
                               join bn in _Data.BenhNhans on a.MaBNhan equals bn.MaBNhan
                              
                               select new
                               {
                                   bn.IDDTBN,
                                   a.TenDV,
                                   bn.DTuong,
                                   bn.NoiTru,
                                   YeuCau = "",
                                   STTHT = 0,// a.STTHT,
                                   TenBNhan = bn.TenBNhan,
                                   bhyt = bn.SThe,
                                   gioitinh = bn.GTinh == 1 ? "Nam" : "Nữ",
                                   tuoi = bn.Tuoi == null ? 0 : bn.Tuoi.Value,
                                   diachi = bn.DChi,
                                   IDCLS = 1,// a.IDCLS,
                                   MaBNhan = a.MaBNhan ?? 0,
                                   MaKP = a.MaKP ?? 0,
                                   ngaythang = a.NgayNhap,
                                   MaDV = a.MaDV ?? 0,
                                   
                                   LoiDan = a.TenDV, //a.LoiDan,
                                   //dvct.TenDVct,
                                   KetLuan = "a.KetLuan",
                                   MaDVct = "",
                                   KetQua = "a.KetQua",
                                   TenCBcd = a.MaCB,  //"a.TenCBcd",
                                   DSCBTH = a.DSCBTH,
                                   // chidinh.KetLuan
                               }).ToList();
                    List<BenhNhan> _lTong_dt = (from a in abc
                                                where (_idDTBN == 99 ? true : a.IDDTBN == _idDTBN)
                                                  && (cbo_NoiTru.SelectedIndex == 2 ? true : a.NoiTru == cbo_NoiTru.SelectedIndex)
                                                select new BenhNhan
                                                {
                                                    // DSCBTH = a.DSCBTH,
                                                    TenDV = a.TenDV,
                                                    YeuCau = "",
                                                    STTHT = 0,// a.STTHT,
                                                    TenBNhan = a.TenBNhan,
                                                    bhyt = chk_HT_TheBH.Checked ? a.bhyt : (a.bhyt.Length >= 15 ? "X" : ""),
                                                    gioitinh = a.gioitinh == "Nam" ? a.tuoi.ToString() : "",
                                                    gioitinh_nu = a.gioitinh == "Nữ" ? a.tuoi.ToString() : "",
                                                    tuoi = a.tuoi,
                                                    diachi = a.diachi,
                                                    IDCLS = 1,// a.IDCLS,
                                                    MaBNhan = a.MaBNhan,
                                                    MaKP = a.MaKP,
                                                    ngaythang = a.ngaythang ?? DateTime.Now,
                                                    MaDV = a.MaDV,
                                                    LoiDan = "", //a.LoiDan,
                                                    //dvct.TenDVct,
                                                    KetLuan = a.TenDV,
                                                    MaDVct = a.MaDV.ToString(),
                                                    TenDVct=a.TenDV,
                                                    KetQua = "",
                                                    TenCBcd = a.TenCBcd,  //"a.TenCBcd",
                                                    DSCBTH = a.DSCBTH,
                                                    // chidinh.KetLuan
                                                }).ToList();
                    if( _TTPT)
                    foreach (var item in _lTong_dt)
                    {
                        item.DSCBTH = _BSGMe(item.DSCBTH);

                    }
                    _Tong.AddRange(_lTong_dt);
                }
            }
            //
            #endregion

          
            _lDichVu = (from a in _Tong
                        group a by new { a.TenDVct, a.MaDVct, a.MaDV } into kq
                        select new c_DSDV { MaDVct = kq.Key.MaDVct, TenDVct = kq.Key.TenDVct, MaDV = kq.Key.MaDV }).OrderBy(p => p.MaDV).ThenBy(p => p.TenDVct).ToList();

            string[] _lTendvct = new string[100];
            for (int i = 0; i < 100; i++)
            {
                _lTendvct[i] = "";
            }
            for (int i = 0; i < _lDichVu.Count; i++)
            {
                _lTendvct[i] = _lDichVu.Skip(i).Take(1).First().MaDVct;
            }
            List<BenhNhan> _lbenhNhan_gr = new List<BenhNhan>();
         
            if (RadDK.SelectedIndex == 1) // hiển thị kết quả
            {
                if (_TTPT)
                {
                    _BenhNhan = (from a in _Tong
                                 group a by new {a.LoaiTTPT, a.LoiDan,a.DSCBTH, a.KetQua, a.ngaythang, a.MaBNhan, a.TenBNhan, a.diachi, a.gioitinh, a.tuoi, a.bhyt, a.IDCLS, a.MaKP, a.KetLuan, a.YeuCau, a.TenCBcd, a.TenCBth } into kq
                                 select new BenhNhan
                                 {
                                     LoaiTTPT= kq.Key.LoaiTTPT,
                                     LoiDan= kq.Key.LoiDan,
                                     YeuCau = kq.Key.YeuCau,
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     diachi = kq.Key.diachi,
                                     gioitinh = kq.Key.gioitinh == "Nam" ? kq.Key.tuoi.ToString() : "",
                                     gioitinh_nu = kq.Key.gioitinh == "Nữ" ? kq.Key.tuoi.ToString() : "",
                                     tuoi = kq.Key.tuoi,
                                     bhyt = kq.Key.bhyt,
                                     IDCLS = kq.Key.IDCLS,
                                     MaKP = kq.Key.MaKP,
                                     ngaythang = kq.Key.ngaythang,
                                     KetLuan = kq.Key.KetLuan,
                                     KetQua = kq.Key.KetQua,
                                     TenCBcd = kq.Key.TenCBcd,  
                                     TenCBth = kq.Key.TenCBth,
                                     DSCBTH = _BSGMe(kq.Key.DSCBTH),
                                 }).OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan).ToList();
                    if (!chk_CDCLS.Checked)
                        _BenhNhan.Clear();
                }
                else
                if (_CDHA)
                {
                    _BenhNhan = (from a in _Tong
                                 group a by new { a.ngaythang, a.MaBNhan, a.TenBNhan, a.diachi, a.gioitinh, a.tuoi, a.bhyt, a.IDCLS, a.MaKP, a.KetLuan, a.YeuCau, a.TenCBcd, a.TenCBth } into kq
                                 select new BenhNhan
                                 {
                                     YeuCau = kq.Key.YeuCau,
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     diachi = kq.Key.diachi,
                                     gioitinh = kq.Key.gioitinh == "Nam" ? kq.Key.tuoi.ToString() : "",
                                     gioitinh_nu = kq.Key.gioitinh == "Nữ" ? kq.Key.tuoi.ToString() : "",
                                     tuoi = kq.Key.tuoi,
                                     bhyt = kq.Key.bhyt,
                                     IDCLS = kq.Key.IDCLS,
                                     MaKP = kq.Key.MaKP,
                                     ngaythang = kq.Key.ngaythang,
                                     KetLuan = kq.Key.KetLuan,
                                     TenCBcd = kq.Key.TenCBcd,
                                     TenCBth = kq.Key.TenCBth,
                                 }).OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan).ToList();

                }
                else
                {
                    _BenhNhan = (from a in _Tong
                                 group a by new { a.ngaythang, a.MaBNhan, a.TenBNhan, a.diachi, a.gioitinh, a.tuoi, a.bhyt, a.IDCLS, a.TenCBcd, a.MaKP } into kq
                                 select new BenhNhan
                                 {
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     diachi = kq.Key.diachi,
                                     gioitinh = kq.Key.gioitinh,
                                     tuoi = kq.Key.tuoi,
                                     bhyt = kq.Key.bhyt,
                                     IDCLS = kq.Key.IDCLS,
                                     MaKP = kq.Key.MaKP,
                                     TenCBcd = kq.Key.TenCBcd,
                                     ngaythang = kq.Key.ngaythang,
                                     KetQua = "",
                                     colKQ1 = kq.Where(p => p.MaDVct == _lTendvct[0]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[0]).Max(p => p.KetQua),
                                     colKQ2 = kq.Where(p => p.MaDVct == _lTendvct[1]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[1]).Max(p => p.KetQua),
                                     colKQ3 = kq.Where(p => p.MaDVct == _lTendvct[2]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[2]).Max(p => p.KetQua),
                                     colKQ4 = kq.Where(p => p.MaDVct == _lTendvct[3]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[3]).Max(p => p.KetQua),
                                     colKQ5 = kq.Where(p => p.MaDVct == _lTendvct[4]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[4]).Max(p => p.KetQua),
                                     colKQ6 = kq.Where(p => p.MaDVct == _lTendvct[5]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[5]).Max(p => p.KetQua),
                                     colKQ7 = kq.Where(p => p.MaDVct == _lTendvct[6]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[6]).Max(p => p.KetQua),
                                     colKQ8 = kq.Where(p => p.MaDVct == _lTendvct[7]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[7]).Max(p => p.KetQua),
                                     colKQ9 = kq.Where(p => p.MaDVct == _lTendvct[8]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[8]).Max(p => p.KetQua),
                                     colKQ10 = kq.Where(p => p.MaDVct == _lTendvct[9]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[9]).Max(p => p.KetQua),
                                     colKQ11 = kq.Where(p => p.MaDVct == _lTendvct[10]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[10]).Max(p => p.KetQua),
                                     colKQ12 = kq.Where(p => p.MaDVct == _lTendvct[11]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[11]).Max(p => p.KetQua),
                                     colKQ13 = kq.Where(p => p.MaDVct == _lTendvct[12]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[12]).Max(p => p.KetQua),
                                     colKQ14 = kq.Where(p => p.MaDVct == _lTendvct[13]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[13]).Max(p => p.KetQua),
                                     colKQ15 = kq.Where(p => p.MaDVct == _lTendvct[14]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[14]).Max(p => p.KetQua),
                                     colKQ16 = kq.Where(p => p.MaDVct == _lTendvct[15]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[15]).Max(p => p.KetQua),
                                     colKQ17 = kq.Where(p => p.MaDVct == _lTendvct[16]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[16]).Max(p => p.KetQua),
                                     colKQ18 = kq.Where(p => p.MaDVct == _lTendvct[17]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[17]).Max(p => p.KetQua),
                                     colKQ19 = kq.Where(p => p.MaDVct == _lTendvct[18]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[18]).Max(p => p.KetQua),
                                     colKQ20 = kq.Where(p => p.MaDVct == _lTendvct[19]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[19]).Max(p => p.KetQua),
                                     colKQ21 = kq.Where(p => p.MaDVct == _lTendvct[20]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[20]).Max(p => p.KetQua),
                                     colKQ22 = kq.Where(p => p.MaDVct == _lTendvct[21]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[21]).Max(p => p.KetQua),
                                     colKQ23 = kq.Where(p => p.MaDVct == _lTendvct[22]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[22]).Max(p => p.KetQua),
                                     colKQ24 = kq.Where(p => p.MaDVct == _lTendvct[23]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[23]).Max(p => p.KetQua),
                                     colKQ25 = kq.Where(p => p.MaDVct == _lTendvct[24]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[24]).Max(p => p.KetQua),
                                     colKQ26 = kq.Where(p => p.MaDVct == _lTendvct[25]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[25]).Max(p => p.KetQua),
                                     colKQ27 = kq.Where(p => p.MaDVct == _lTendvct[26]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[26]).Max(p => p.KetQua),
                                     colKQ28 = kq.Where(p => p.MaDVct == _lTendvct[27]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[27]).Max(p => p.KetQua),
                                     colKQ29 = kq.Where(p => p.MaDVct == _lTendvct[28]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[28]).Max(p => p.KetQua),
                                     colKQ30 = kq.Where(p => p.MaDVct == _lTendvct[29]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[29]).Max(p => p.KetQua),
                                 }).OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan).ToList();
                    _lbenhNhan_gr = (from a in _Tong
                                     group a by a.MaDVct into kq
                                     select new BenhNhan
                                     {
                                         MaDVct = kq.Key,
                                         KetQua = kq.Where(p => p.KetQua.Length > 0).Count().ToString(),

                                     }).ToList();
                }
            }
            else
            { //hiển thị số lượng

                _BenhNhan = (from a in _Tong
                             group a by new { a.ngaythang, a.MaBNhan, a.TenBNhan, a.diachi, a.gioitinh, a.tuoi, a.bhyt, a.IDCLS, a.TenCBcd, a.MaKP } into kq
                             select new BenhNhan
                             {
                                 MaBNhan = kq.Key.MaBNhan,
                                 TenBNhan = kq.Key.TenBNhan,
                                 diachi = kq.Key.diachi,
                                 gioitinh = kq.Key.gioitinh,
                                 tuoi = kq.Key.tuoi,
                                 bhyt = kq.Key.bhyt,
                                 IDCLS = kq.Key.IDCLS,
                                 MaKP = kq.Key.MaKP,
                                 TenCBcd = kq.Key.TenCBcd,
                                 ngaythang = kq.Key.ngaythang,
                                 colKQ1 = kq.Where(p => p.MaDVct == _lTendvct[0]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[0]).Count().ToString(),
                                 colKQ2 = kq.Where(p => p.MaDVct == _lTendvct[1]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[1]).Count().ToString(),
                                 colKQ3 = kq.Where(p => p.MaDVct == _lTendvct[2]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[2]).Count().ToString(),
                                 colKQ4 = kq.Where(p => p.MaDVct == _lTendvct[3]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[3]).Count().ToString(),
                                 colKQ5 = kq.Where(p => p.MaDVct == _lTendvct[4]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[4]).Count().ToString(),
                                 colKQ6 = kq.Where(p => p.MaDVct == _lTendvct[5]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[5]).Count().ToString(),
                                 colKQ7 = kq.Where(p => p.MaDVct == _lTendvct[6]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[6]).Count().ToString(),
                                 colKQ8 = kq.Where(p => p.MaDVct == _lTendvct[7]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[7]).Count().ToString(),
                                 colKQ9 = kq.Where(p => p.MaDVct == _lTendvct[8]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[8]).Count().ToString(),
                                 colKQ10 = kq.Where(p => p.MaDVct == _lTendvct[9]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[9]).Count().ToString(),
                                 colKQ11 = kq.Where(p => p.MaDVct == _lTendvct[10]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[10]).Count().ToString(),
                                 colKQ12 = kq.Where(p => p.MaDVct == _lTendvct[11]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[11]).Count().ToString(),
                                 colKQ13 = kq.Where(p => p.MaDVct == _lTendvct[12]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[12]).Count().ToString(),
                                 colKQ14 = kq.Where(p => p.MaDVct == _lTendvct[13]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[13]).Count().ToString(),
                                 colKQ15 = kq.Where(p => p.MaDVct == _lTendvct[14]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[14]).Count().ToString(),
                                 colKQ16 = kq.Where(p => p.MaDVct == _lTendvct[15]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[15]).Count().ToString(),
                                 colKQ17 = kq.Where(p => p.MaDVct == _lTendvct[16]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[16]).Count().ToString(),
                                 colKQ18 = kq.Where(p => p.MaDVct == _lTendvct[17]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[17]).Count().ToString(),
                                 colKQ19 = kq.Where(p => p.MaDVct == _lTendvct[18]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[18]).Count().ToString(),
                                 colKQ20 = kq.Where(p => p.MaDVct == _lTendvct[19]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[19]).Count().ToString(),
                                 colKQ21 = kq.Where(p => p.MaDVct == _lTendvct[20]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[20]).Count().ToString(),
                                 colKQ22 = kq.Where(p => p.MaDVct == _lTendvct[21]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[21]).Count().ToString(),
                                 colKQ23 = kq.Where(p => p.MaDVct == _lTendvct[22]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[22]).Count().ToString(),
                                 colKQ24 = kq.Where(p => p.MaDVct == _lTendvct[23]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[23]).Count().ToString(),
                                 colKQ25 = kq.Where(p => p.MaDVct == _lTendvct[24]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[24]).Count().ToString(),
                                 colKQ26 = kq.Where(p => p.MaDVct == _lTendvct[25]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[25]).Count().ToString(),
                                 colKQ27 = kq.Where(p => p.MaDVct == _lTendvct[26]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[26]).Count().ToString(),
                                 colKQ28 = kq.Where(p => p.MaDVct == _lTendvct[27]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[27]).Count().ToString(),
                                 colKQ29 = kq.Where(p => p.MaDVct == _lTendvct[28]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[28]).Count().ToString(),
                                 colKQ30 = kq.Where(p => p.MaDVct == _lTendvct[29]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[29]).Count().ToString(),
                             }).OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan).ToList();

                foreach (var a in _BenhNhan)
                {
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
                }
                _lbenhNhan_gr = (from a in _Tong
                                 group a by a.MaDVct into kq
                                 select new BenhNhan
                                 {
                                     MaDVct = kq.Key,
                                     KetQua = kq.Count().ToString(),

                                 }).ToList();
            }


            var chandoan = (from a in _BenhNhan
                            join cd in _Data.BNKBs on a.MaBNhan equals cd.MaBNhan
                            where cd.MaKP == a.MaKP
                            select new { a.IDCLS, cd.MaBNhan, cd.MaKP, ChanDoan = cd.ChanDoan + cd.BenhKhac }).ToList();
            foreach (var a in _BenhNhan)
            {
                foreach (var b in chandoan)
                {
                    if (a.MaBNhan == b.MaBNhan && a.IDCLS == b.IDCLS)
                    {
                        a.chandoan = b.ChanDoan;
                    }
                }
            }

            foreach (var a in _BenhNhan)
            {
                if (_tenbc == "Siêu âm" || _tenbc == "X-Quang")
                {
                    if (_lcb.Where(p => p.MaCB == a.TenCBcd).ToList().Count > 0)
                    {
                        a.noigui = _lcb.Where(p => p.MaCB == a.TenCBcd).First().TenCB;

                    }
                }
                else
                {
                    if (_lkp.Where(p => p.MaKP == a.MaKP).ToList().Count > 0)
                    {
                        a.noigui = _lkp.Where(p => p.MaKP == a.MaKP).First().TenKP;

                    }
                }
                if (_lcb.Where(p => p.MaCB == a.TenCBth).ToList().Count > 0)
                {
                    a.TenCBth = _lcb.Where(p => p.MaCB == a.TenCBth).First().TenCB;

                }
            }

            if (_TTPT && RadDK.SelectedIndex == 1)
            {
                BaoCao.Rep_SoPhauThuat_ThuThuat_408 rep1 = new BaoCao.Rep_SoPhauThuat_ThuThuat_408();
                //rep1.TenBC.Value = ("Sổ tổng hợp " + lup_NhomXN.Text).ToUpper();
                if (lup_NhomXN.Text == "Phẫu thuật") {
                    rep1.cell_CD_sau_TTPT.Text = "Sau phẫu thuật";
                    rep1.cell_CD_Truoc_TTPT.Text = "Trước phẫu thuật";
                    rep1.cell_LoaiTTPT.Text = "Loại phẫu thuật";
                    rep1.cell_PPTTPT.Text = "Phương pháp phẫu thuật";
                    
                }
                rep1.cell_Nu_RF.Text = _BenhNhan.Where(p => p.gioitinh_nu != "").Select(p => p.gioitinh_nu).Count().ToString();
                rep1.cell_Nam_RF.Text = _BenhNhan.Where(p => p.gioitinh != "").Select(p => p.gioitinh).Count().ToString();
                rep1.cell_CoBHYT_RF.Text = _BenhNhan.Where(p => p.bhyt != "").Select(p => p.bhyt).Count().ToString();
                rep1.TenSo.Value = "SỔ " + lup_NhomXN.Text.ToUpper();
                rep1.TuNgay.Value = _ngayTu.ToShortDateString();
                rep1.DenNgay.Value = _ngayDen.ToShortDateString();
                rep1.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p=>p.TenBNhan);
                rep1.BindingData();
                rep1.CreateDocument();
                frmIn frm1 = new frmIn();
                frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                frm1.ShowDialog();
            }


        }
        private void ButHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        List<int> _lIdtieuNhom = new List<int>();
        class c_DichVu
        {
            int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            bool check;

            public bool Check
            {
                get { return check; }
                set { check = value; }
            }
            int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            int idTieuNhom;

            public int IdTieuNhom
            {
                get { return idTieuNhom; }
                set { idTieuNhom = value; }
            }
        }
        List<c_DichVu> _lDichVu = new List<c_DichVu>();
        List<c_DichVu> _lDichVu_chon = new List<c_DichVu>();
        int load = 0;
        private void lup_NhomXN_EditValueChanged(object sender, EventArgs e)
        {
            if (lup_NhomXN.Text.Contains("thuật"))
            {
                chk_CDCLS.Enabled = true;
                chk_DV_KD.Enabled = true;
            }
            else {
                chk_CDCLS.Enabled = false;
                chk_DV_KD.Enabled = false;
            }
            if (load ==1) {
                var dvsd3 = (from cls in _Data.CLS
                            join cd in _Data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                            select new { cls.MaKP, cd.MaDV }).Distinct().ToList();
                var dvsd = (from cls in dvsd3
                            select new { cls.MaKP, cls.MaDV }).Distinct().ToList();
                var dvu = (from a in _Data.DichVus.Where(p => p.PLoai == 2 && p.Status == 1) select new { a.MaDV, a.TenDV, a.IdTieuNhom }).ToList();
                _lDichVu = (from dv in dvu
                            join b in dvsd on dv.MaDV equals b.MaDV

                            select new c_DichVu
                            {
                                MaKP = b.MaKP ?? 0,
                                Check = false,
                                MaDV = dv.MaDV,
                                TenDV = dv.TenDV,
                                IdTieuNhom = dv.IdTieuNhom ?? 0,
                            }).OrderBy(p => p.IdTieuNhom).ThenBy(p => p.TenDV).Distinct().ToList();
                var dvsd4 = (from cls in _Data.DThuoccts select new { cls.MaKP, cls.MaDV }).ToList();
                var dvsd2 = (from cls in dvsd4 select cls).Distinct().ToList();
                List<c_DichVu> abc = (from dv in dvu
                                      join b in dvsd2 on dv.MaDV equals b.MaDV
                            select new c_DichVu
                            {
                                MaKP = b.MaKP ?? 0,
                                Check = false,
                                MaDV = dv.MaDV,
                                TenDV = dv.TenDV,
                                IdTieuNhom = dv.IdTieuNhom ?? 0,
                            }).OrderBy(p => p.IdTieuNhom).ThenBy(p => p.TenDV).Distinct().ToList();
                _lDichVu.AddRange(abc);
            }
            load++;

            if (load > 0)
            {
                _lIdtieuNhom.Clear();
                _lDichVu_chon.Clear();
                string _tenRG = "";
                if (lup_NhomXN.EditValue != null)
                {
                    _tenRG = lup_NhomXN.Text;
                }
                foreach (var a in _ltnhom.Where(p => p.TenRG == _tenRG).ToList())
                {
                    _lIdtieuNhom.Add(a.IdTieuNhom);
                }
                _lDichVu_chon = (from dv in _lDichVu
                                 join tn in _lIdtieuNhom on dv.IdTieuNhom equals tn
                                 join kp in _Kphong.Where(p => p.chon == true) on dv.MaKP equals kp.makp
                                 group dv by new{dv.MaDV,dv.TenDV,dv.Check}   into kq
                                 select new c_DichVu { MaDV = kq.Key.MaDV, TenDV =kq.Key.TenDV,Check=false}).ToList();
                _lDichVu_chon = _lDichVu_chon.Distinct().ToList();
                ckl_DichVu.DataSource = null;
                ckl_DichVu.DataSource = _lDichVu_chon;
                ckl_DichVu.CheckAll();
            }
        }

        private void hyp_Chon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckl_DichVu.CheckAll();
        }

        private void hyp_HuyChon_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            ckl_DichVu.UnCheckAll();
        }

  
        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
               if (Convert.ToInt32(cklKP.SelectedValue) == 0)
                {
                    if (cklKP.GetItemChecked(cklKP.SelectedIndex))
                    {
                        cklKP.CheckAll();
                     
                    }
                    else
                    {
                              cklKP.UnCheckAll();
                    }
                }

               for (int i = 0; i < cklKP.ItemCount; i++)
               {  
                   int makp = Convert.ToInt32(cklKP.GetItemValue(i));
                   if (cklKP.GetItemChecked(i))
                   {
                       
                       foreach (var item in _Kphong)
                       {
                           if (item.makp == makp && item.makp != 0)
                           {
                               item.chon = true;
                               //break;
                           }
                       }
                   }
                   else {
                       foreach (var item in _Kphong)
                       {
                           if (item.makp == makp || item.makp==0)
                           {
                               item.chon = false;
                              // break;
                           }
                       }
                   }
               }
               if (load>0)
               lup_NhomXN_EditValueChanged(sender, e);
        }

        private void chk_DV_KD_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}