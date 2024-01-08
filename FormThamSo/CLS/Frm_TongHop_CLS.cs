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

namespace QLBV.FormThamSo
{
    public partial class Frm_TongHop_CLS : DevExpress.XtraEditors.XtraForm
    {
        public Frm_TongHop_CLS()
        {
            InitializeComponent();
            if(DungChung.Bien.MaBV=="24012")
            {
            this.RadDK.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Bc theo kết quả_24012")});
            }
            if(DungChung.Bien.MaBV == "24009")
            {
                ckHTYeuCau.Checked = false;
            }
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
        public class MaMay
        {
            public string MaQD { get; set; }
            public string TenDV { get; set; }
        }
        private void Frm_ThHoaSinhMauSL_Load(object sender, EventArgs e)
        {
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            radTrongDM.SelectedIndex = 2;
            _lkp = _Data.KPhongs.ToList();
            _lcb = _Data.CanBoes.ToList();
            LupNgaytu.DateTime = System.DateTime.Now;
            LupNgayden.DateTime = System.DateTime.Now;
            _Kphong.Clear();
            _ltnhom = _Data.TieuNhomDVs.ToList();
            List<DTBN> _lDTBN = _Data.DTBNs.ToList();
            _lDTBN.Add(new DTBN { IDDTBN = 99, DTBN1 = "Tất cả" });
            cbo_DoiTuong.Properties.DataSource = _lDTBN.OrderBy(p => p.IDDTBN);
            lup_NhomXN.Properties.DataSource = (from tn in _Data.TieuNhomDVs
                                                join nhom in _Data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                                                where nhom.TenNhomCT == "Xét nghiệm" || nhom.TenNhomCT == ("Chẩn đoán hình ảnh") || nhom.TenNhomCT == "Thủ thuật, phẫu thuật" || nhom.IDNhom == 3
                                                select new { tn.TenRG }).Distinct().OrderBy(p => p).ToList();
            var kphong = (from kp in _Data.KPhongs
                          where (kp.PLoai == "Lâm sàng" || kp.PLoai == "Phòng khám" || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh)
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
            var mamay = (from ts in _Data.TaiSans
                         join dv in _Data.DichVus on ts.MaDV equals dv.MaDV
                         join kp in _Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang) on ts.MaKP equals kp.MaKP
                         select new MaMay { MaQD = dv.MaQD, TenDV = dv.TenDV }).ToList();
            mamay.Add(new MaMay { MaQD = "tatca", TenDV = "Tất cả" });
            lupMaMay.Properties.DataSource = mamay;
            lupMaMay.EditValue = "tatca";
            ckl_DichVu.DataSource = _lDichVu;
            ckl_DichVu.CheckAll();
            load++;
        }
        public class c_DSDV
        {
            private string _tenDVct, _madvct;
            private int madv;
            private int stt_R;

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
            public int Stt_R
            {
                set { stt_R = value; }
                get { return stt_R; }
            }
            public int? _thutu { get; set; }

            public int? STT4069 { get; set; }
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
            public string TuoiNam { set; get; }
            public string TuoiNu { set; get; }
            private string Diachi;
            private string Chandoan;
            private string Noigui;
            private string Yeucau;
            private int makp;
            public int STT_R;
            private string BHYT;
            private DateTime Ngaythang;
            private DateTime Ngaythang1;
            private string madvct;
            string dichVu;
            private int ngoaigioHC;
            private double cKham;

            public double CKham
            {
                get { return cKham; }
                set { cKham = value; }
            }

            public int NgoaigioHC
            {
                get { return ngoaigioHC; }
                set { ngoaigioHC = value; }
            }
            public double? soluong { set; get; }

            public string DichVu
            {
                get { return dichVu; }
                set { dichVu = value; }
            }
            double soPhieu;

            public double SoPhieu
            {
                get { return soPhieu; }
                set { soPhieu = value; }
            }
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
            public DateTime ngaythang1
            { set { Ngaythang1 = value; } get { return Ngaythang1; } }
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
            string colkq31;
            public string colKQ31 { set { colkq31 = value; } get { return colkq31; } }
            string colkq32;
            public string colKQ32 { set { colkq32 = value; } get { return colkq32; } }
            string colkq33;
            public string colKQ33 { set { colkq33 = value; } get { return colkq33; } }
            string colkq34;
            public string colKQ34 { set { colkq34 = value; } get { return colkq34; } }
            string colkq35;
            public string colKQ35 { set { colkq35 = value; } get { return colkq35; } }
            string colkq36;
            public string colKQ36 { set { colkq36 = value; } get { return colkq36; } }
            string colkq37;
            public string colKQ37 { set { colkq37 = value; } get { return colkq37; } }
            string colkq38;
            public string colKQ38 { set { colkq38 = value; } get { return colkq38; } }
            string colkq39;
            public string colKQ39 { set { colkq39 = value; } get { return colkq39; } }
            string colkq40;
            public string colKQ40 { set { colkq40 = value; } get { return colkq40; } }
            string colkq41;
            public string colKQ41 { set { colkq41 = value; } get { return colkq41; } }
            string colkq42;
            public string colKQ42 { set { colkq42 = value; } get { return colkq42; } }
            //string colkq43;
            //public string colKQ43 { set { colkq43 = value; } get { return colkq43; } }
            //string colkq44;
            //public string colKQ44 { set { colkq44 = value; } get { return colkq44; } }
            //string colkq45;
            //public string colKQ45 { set { colkq45 = value; } get { return colkq45; } }
            //string colkq46;
            //public string colKQ46 { set { colkq46 = value; } get { return colkq46; } }
            //string colkq47;
            //public string colKQ47 { set { colkq47 = value; } get { return colkq47; } }
            //string colkq48;
            //public string colKQ48 { set { colkq48 = value; } get { return colkq48; } }
            //string colkq49;
            //public string colKQ49 { set { colkq49 = value; } get { return colkq49; } }
            //string colkq50;
            //public string colKQ50 { set { colkq50 = value; } get { return colkq50; } }
            //string colkq51;
            //public string colKQ51 { set { colkq51 = value; } get { return colkq51; } }
            //string colkq52;
            //public string colKQ52 { set { colkq52 = value; } get { return colkq52; } }
          
            public string colKQ43 { get; set; }
            public string colKQ44 { get; set; }
            public string colKQ45 { get; set; }
            public string colKQ46 { get; set; }
            public string colKQ47 { get; set; }
            public string colKQ48 { get; set; }
            public string colKQ49 { get; set; }
            public string colKQ50 { get; set; }
            public string colKQ51 { get; set; }
            public string colKQ52 { get; set; }
            public string colKQ53 { get; set; }
            public string colKQ54 { get; set; }
            public string colKQ55 { get; set; }
            public string colKQ56 { get; set; }
            public string colKQ57 { get; set; }
            public string colKQ58 { get; set; }



            public string cel1 { get; set; }
            public string cel2 { get; set; }
            public string cel3 { get; set; }
            public string cel4 { get; set; }
            public string cel5 { get; set; }
            public string cel6 { get; set; }
            public string cel7 { get; set; }
            public string cel8 { get; set; }
            public string cel9 { get; set; }
            public string cel10 { get; set; }
            public string cel11 { get; set; }
            public string cel12 { get; set; }
            public string cel13 { get; set; }
            public string cel14 { get; set; }
            public string cel15 { get; set; }
            public string cel16 { get; set; }
            public string cel17 { get; set; }
            public string cel18 { get; set; }
            public string cel19 { get; set; }
            public string cel20 { get; set; }
            public string cel21 { get; set; }
            public string cel22 { get; set; }
            public string cel23 { get; set; }
            public string cel24 { get; set; }
            public string cel25 { get; set; }
            public string cel26 { get; set; }
            public string cel27 { get; set; }
            public string cel28 { get; set; }
            public string cel29 { get; set; }
            public string cel30 { get; set; }
            public string cel31 { get; set; }
            public string cel32 { get; set; }
            public string cel33 { get; set; }
            public string cel34 { get; set; }
            public string cel35 { get; set; }
            public string cel36 { get; set; }
            public string cel37 { get; set; }
            public string cel38 { get; set; }
            public string cel39 { get; set; }
            public string cel40 { get; set; }
            public string cel41 { get; set; }
            public string cel42 { get; set; }
            public string cel43 { get; set; }
            public string cel44 { get; set; }
            public string cel45 { get; set; }
            public string cel46 { get; set; }
            public string cel47 { get; set; }
            public string cel48 { get; set; }
            public string cel49 { get; set; }
            public string cel50 { get; set; }
            public string cel51 { get; set; }
            public string cel52 { get; set; }
            public string cel53 { get; set; }
            public string cel54 { get; set; }
            public string cel55 { get; set; }
            public string cel56 { get; set; }
            public string cel57 { get; set; }
            public string cel58 { get; set; }


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
            public string ChanDoanRV { get; set; }

            public int? STT4069 { get; set; }
            public int? _thutu { get; set; }
        }
        #endregion
        private string _BSGMe(string s)
        {
            try
            {
                string[] _a = s.Split(';');
                if (_a.Length > 0 && _a[1] != null)
                    return _a[1];
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        private void butTaoBC_Click(object sender, EventArgs e)
        {
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<BenhNhan> _BenhNhan = new List<BenhNhan>();
            List<c_DSDV> _lDichVu = new List<c_DSDV>();
            List<c_BN_KQ> _lBN_KQ = new List<c_BN_KQ>();
            List<BenhNhan> _Tong = new List<BenhNhan>();
            _BenhNhan.Clear();
            DateTime _ngayTu = DungChung.Ham.NgayTu(LupNgaytu.DateTime);
            DateTime _ngayDen = DungChung.Ham.NgayDen(LupNgayden.DateTime);
            if (cbo_DoiTuong.EditValue != null && cbo_DoiTuong.EditValue.ToString() != "")
                _idDTBN = Convert.ToInt32(cbo_DoiTuong.EditValue);
            DungChung.Bien.BC408_24009_ischecked = ckHTYeuCau.Checked;
            string _mamay = "tatca";
            if (lupMaMay.EditValue != null)
                _mamay = lupMaMay.EditValue.ToString();
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
                        if (item.makp == makp && item.makp != 0)
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
                _tenbc = lup_NhomXN.Text.ToUpper();
            }
            bool _CDHA = false;
            bool _TTPT = false;
            if (_tenbc.ToLower().Contains("thủ thuật") || _tenbc.ToLower().Contains("phẫu thuật"))
                _TTPT = true;

            if (_tenbc == "Siêu âm" || _tenbc == "X-Quang" || _tenbc == "Điện tim" || _tenbc == "Nội soi" || _tenbc == "Chức năng hô hấp" || (_tenbc.ToLower().Contains("nội soi") && DungChung.Bien.MaBV == "30012"))
            {
                _CDHA = true;
            }
           
            if (cboTT.SelectedIndex == 0)
            {
                #region theo ngày thực hiện
                _Tong = (from
                             cls in _Data.CLS.Where(p => p.NgayTH >= _ngayTu).Where(p => p.NgayTH <= _ngayDen)
                         join chidinh in _Data.ChiDinhs.Where(p => _mamay == "tatca" ? true : p.MaMay == _mamay).Where(p => p.Status == 1) on cls.IdCLS equals chidinh.IdCLS
                         join clsct in _Data.CLScts.Where(p => chkKQ.Checked ? p.Status == 1 : true) on chidinh.IDCD equals clsct.IDCD
                         where radTrongDM.SelectedIndex == 2 ? true : chidinh.TrongBH == radTrongDM.SelectedIndex
                         select new BenhNhan
                         {
                             SoPhieu = clsct.SoPhieu ?? 0,
                             YeuCau = chidinh.ChiDinh1,
                             STTHT = clsct.STTHT == null ? 0 : clsct.STTHT.Value,
                             IDCLS = cls.IdCLS,
                             MaBNhan = cls.MaBNhan == null ? 0 : cls.MaBNhan.Value,
                             MaKP = cls.MaKP == null ? 0 : cls.MaKP.Value,
                             ngaythang = cls.NgayTH == null ? _ngayTu : cls.NgayTH.Value,
                             DSCBTH = cls.DSCBTH,
                             LoiDan = chidinh.LoiDan,
                             MaDV = chidinh.MaDV == null ? 0 : chidinh.MaDV.Value,
                             chandoan = cls.ChanDoan,
                             //dvct.TenDVct,
                             KetLuan = chidinh.KetLuan,
                             MaDVct = clsct.MaDVct,
                             KetQua = clsct.KetQua,
                             TenCBcd = cls.MaCB == null ? "" : cls.MaCB,
                             TenCBth = cls.MaCBth == null ? "" : cls.MaCBth,
                             soluong = 1,
                             NgoaigioHC = chidinh.NgoaiGioHC
                             // chidinh.KetLuan
                         }).ToList();


                #endregion
            }
            else if (cboTT.SelectedIndex == 1)
            { //theo ngày thanh toán
                #region theo ngày thanh toán
                _Tong = (from vienphi in _Data.VienPhis.Where(p => p.NgayTT >= _ngayTu).Where(p => p.NgayTT <= _ngayDen)
                         join cls in _Data.CLS on vienphi.MaBNhan equals cls.MaBNhan
                         join chidinh in _Data.ChiDinhs.Where(p => _mamay == "tatca" ? true : p.MaMay == _mamay).Where(p => p.Status == 1) on cls.IdCLS equals chidinh.IdCLS
                         join clsct in _Data.CLScts.Where(p => chkKQ.Checked ? p.Status == 1 : true) on chidinh.IDCD equals clsct.IDCD
                         where radTrongDM.SelectedIndex == 2 ? true : chidinh.TrongBH == radTrongDM.SelectedIndex
                         select new BenhNhan
                         {
                             SoPhieu = clsct.SoPhieu ?? 0,
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
                             chandoan = cls.ChanDoan,
                             soluong = 1,
                             NgoaigioHC = chidinh.NgoaiGioHC
                             // chidinh.KetLuan
                         }).ToList();
                if (DungChung.Bien.MaBV == "30010")
                {
                    _Tong = (from vienphi in _Data.VienPhis.Where(p => p.NgayTT >= _ngayTu).Where(p => p.NgayTT <= _ngayDen)
                             join vienphict in _Data.VienPhicts on vienphi.idVPhi equals vienphict.idVPhi
                             join cls in _Data.CLS on vienphi.MaBNhan equals cls.MaBNhan
                             join chidinh in _Data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals chidinh.IdCLS
                             join clsct in _Data.CLScts.Where(p => chkKQ.Checked ? p.Status == 1 : true) on chidinh.IDCD equals clsct.IDCD
                             join kp in _Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham) on cls.MaKP equals kp.MaKP into k1
                             from k in k1.DefaultIfEmpty()
                             where radTrongDM.SelectedIndex == 2 ? true : chidinh.TrongBH == radTrongDM.SelectedIndex
                             select new BenhNhan
                             {
                                 SoPhieu = clsct.SoPhieu ?? 0,
                                 YeuCau = chidinh.ChiDinh1,
                                 STTHT = clsct.STTHT == null ? 0 : clsct.STTHT.Value,
                                 IDCLS = cls.IdCLS,
                                 MaBNhan = cls.MaBNhan == null ? 0 : cls.MaBNhan.Value,
                                 MaKP = cls.MaKP == null ? 0 : (k != null ? vienphict.MaKP.Value : cls.MaKP.Value),
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
                                 chandoan = cls.ChanDoan,
                                 soluong = 1,
                                 NgoaigioHC = chidinh.NgoaiGioHC
                                 // chidinh.KetLuan
                             }).ToList();
                }
                #endregion
            }
            else if (cboTT.SelectedIndex == 2)
            {
                //var q1 = (from tu in _Data.TamUngs.Where(p => p.PhanLoai == 3 && p.NgayThu >= _ngayTu && p.NgayThu <= _ngayDen && (p.IDGoiDV == null || p.IDGoiDV <=0))
                //          join tuct in _Data.TamUngcts.Where(p=>p.Status==0) on tu.IDTamUng equals tuct.IDTamUng
                //          join cd in _Data.ChiDinhs.Where(p => p.Status == 1) on tuct.IDCD equals cd.IDCD
                //          join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                //          join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD
                //          select new BenhNhan
                //          {
                //              SoPhieu = clsct.SoPhieu ?? 0,
                //              YeuCau = cd.ChiDinh1,
                //              STTHT = clsct.STTHT == null ? 0 : clsct.STTHT.Value,
                //              IDCLS = cls.IdCLS,
                //              MaBNhan = cls.MaBNhan == null ? 0 : cls.MaBNhan.Value,
                //              MaKP = cls.MaKP == null ? 0 : cls.MaKP.Value,
                //              ngaythang = cls.NgayTH == null ? _ngayTu : cls.NgayTH.Value,
                //              DSCBTH = cls.DSCBTH,
                //              LoiDan = cd.LoiDan,
                //              MaDV = cd.MaDV == null ? 0 : cd.MaDV.Value,
                //              chandoan = cls.ChanDoan,
                //              //dvct.TenDVct,
                //              KetLuan = cd.KetLuan,
                //              MaDVct = clsct.MaDVct,
                //              KetQua = clsct.KetQua,
                //              TenCBcd = cls.MaCB == null ? "" : cls.MaCB,
                //              TenCBth = cls.MaCBth == null ? "" : cls.MaCBth,
                //              soluong = 1,
                //              NgoaigioHC = cd.NgoaiGioHC
                //          }).ToList();
                var q2 = (from tu in _Data.TamUngs.Where(p => p.PhanLoai == 3 && p.NgayThu >= _ngayTu && p.NgayThu <= _ngayDen)// && p.IDGoiDV != null && p.IDGoiDV > 0
                          join cd in _Data.ChiDinhs.Where(p => _mamay == "tatca" ? true : p.MaMay == _mamay).Where(p => p.Status == 1) on tu.IDTamUng equals cd.SoPhieu
                          join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                          join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD
                          select new BenhNhan
                          {
                              SoPhieu = clsct.SoPhieu ?? 0,
                              YeuCau = cd.ChiDinh1,
                              STTHT = clsct.STTHT == null ? 0 : clsct.STTHT.Value,
                              IDCLS = cls.IdCLS,
                              MaBNhan = cls.MaBNhan == null ? 0 : cls.MaBNhan.Value,
                              MaKP = cls.MaKP == null ? 0 : cls.MaKP.Value,
                              ngaythang = cls.NgayTH == null ? _ngayTu : cls.NgayTH.Value,
                              DSCBTH = cls.DSCBTH,
                              LoiDan = cd.LoiDan,
                              MaDV = cd.MaDV == null ? 0 : cd.MaDV.Value,
                              chandoan = cls.ChanDoan,
                              //dvct.TenDVct,
                              KetLuan = cd.KetLuan,
                              MaDVct = clsct.MaDVct,
                              KetQua = clsct.KetQua,
                              TenCBcd = cls.MaCB == null ? "" : cls.MaCB,
                              TenCBth = cls.MaCBth == null ? "" : cls.MaCBth,
                              soluong = 1,
                              NgoaigioHC = cd.NgoaiGioHC
                          }).ToList();
                //_Tong.AddRange(q1);
                _Tong.AddRange(q2);
            }
            var mabnhan = _Tong.Select(p => p.MaBNhan).Distinct();
            var Bnhan = _Data.BenhNhans.Where(o => mabnhan.Contains(o.MaBNhan)).ToList();

            _Tong = (from a in _Tong
                     join bn in Bnhan on a.MaBNhan equals bn.MaBNhan
                     join dv in
                         _lDichVu_chon on a.MaDV equals dv.MaDV
                     where (_idDTBN == 99 ? true : bn.IDDTBN == _idDTBN)
                     && (cbo_NoiTru.SelectedIndex == 2 ? true : bn.NoiTru == cbo_NoiTru.SelectedIndex)
                     select new BenhNhan
                     {

                         DSCBTH = a.DSCBTH,
                         YeuCau = DungChung.Bien.MaBV != "20001" ? (ckHTYeuCau.Checked ? (dv.TenDV + "; " + a.YeuCau) : a.YeuCau) : (((_tenbc == "Siêu âm" || _tenbc == "X-Quang") && (RadDK.SelectedIndex == 1 || RadDK.SelectedIndex == 2)) ? dv.TenDV : dv.TenRG),
                         
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
                         LoiDan = a.LoiDan,
                         //dvct.TenDVct,
                         KetLuan = a.KetLuan,
                         MaDVct = a.MaDVct,
                         KetQua = (_tenbc == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler && DungChung.Bien.MaBV == "27023" || _tenbc == "NỘI SOI TAI-MŨI-HỌNG" && DungChung.Bien.MaBV == "27183") ? a.KetLuan : a.KetQua ,
                         TenCBcd = a.TenCBcd,
                         TenCBth = a.TenCBth,
                         SoPhieu = a.SoPhieu,
                         chandoan = a.chandoan,
                         NgoaigioHC = a.NgoaigioHC,
                         soluong = a.soluong,
                         // chidinh.KetLuan
                     }).ToList();


            var tenDVct2 = (from dvct in _Data.DichVucts
                            join dv in _Data.DichVus on dvct.MaDV equals dv.MaDV
                            join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                            select new
                            {
                                dv.MaDV,
                                dv.TenDV,
                                dvct.TenDVct,
                                dvct.MaDVct,
                                dvct.STT_R,
                                dv.Loai,
                                dvct.STT,
                                tn.TenRG
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
                               dvct.STT_R,
                               dvct.STT,
                               dvct.Loai,
                               dvct.TenRG
                           }
                           ).Distinct().ToList();

            _Tong = (from a in _Tong
                     join b in _Kphong.Where(p => p.chon == true) on a.MaKP equals b.makp
                     //join dvct in tenDVct2 on a.MaDVct equals dvct.MaDVct
                     join dvct in tenDVct on a.MaDVct equals dvct.MaDVct
                     select new BenhNhan
                     {
                         SoPhieu = a.SoPhieu,
                         LoaiTTPT = dvct.Loai ?? 0,
                         STTHT = a.STTHT,
                         TenBNhan = a.TenBNhan,
                         bhyt = chk_HT_TheBH.Checked ? a.bhyt : (a.bhyt.Length >= 15 ? "X" : ""),
                         DichVu = (a.bhyt.Length >= 15 ? "" : "X"),
                         gioitinh = a.gioitinh,
                         tuoi = a.tuoi,
                         diachi = a.diachi,
                         IDCLS = a.IDCLS,
                         MaBNhan = a.MaBNhan,
                         MaKP = a.MaKP,
                         ngaythang = _TTPT ? a.ngaythang : a.ngaythang,
                         LoiDan = _TTPT ? a.TenDV + " - " + a.LoiDan : a.LoiDan,
                         MaDV = a.MaDV,
                         KetLuan = _tenbc == "X-Quang" ? (DungChung.Bien.MaBV == "27023" ? a.KetLuan : (a.KetLuan + a.KetQua)) : a.KetLuan,
                         MaDVct = a.MaDVct,
                         KetQua = a.KetQua == null ? "" : a.KetQua,
                         TenDVct = dvct.TenDVct,
                         TenDV = dvct.TenDV,
                         TenCBcd = a.TenCBcd,
                         TenCBth = a.TenCBth,
                         YeuCau = ((RadDK.SelectedIndex == 1 || RadDK.SelectedIndex == 2) && checkTenDV.Checked == true && (DungChung.Bien.MaBV != "20001" || (_tenbc != "Siêu âm" && _tenbc != "X-Quang"))) ? dvct.TenRG : a.YeuCau,
                         DSCBTH = a.DSCBTH,
                         STT_R = dvct.STT_R,
                         STT4069 = dvct.STT,
                         chandoan = a.chandoan,
                         soluong = a.soluong,
                         NgoaigioHC = a.NgoaigioHC,

                         // chidinh.KetLuan
                     }).OrderBy(p => p.STT4069).ToList();

            if (ngoaigiohc.Checked == true)
            {

                var ck = (from a in _Data.DThuocs
                          join b in _Data.DThuoccts on a.IDDon equals b.IDDon
                          join c in _Data.DichVus.Where(p => p.IDNhom == 13) on b.MaDV equals c.MaDV
                          select new { a.MaBNhan, b.DonGia }).ToList();
                var ck1 = (from a in ck
                           group a by a.MaBNhan into kq
                           select new { kq.Key, CKham = kq.Sum(p => p.DonGia) }).ToList();
                _Tong = (from a in _Tong
                         join b in _Kphong.Where(p => p.chon == true) on a.MaKP equals b.makp
                         join c in ck1 on a.MaBNhan equals c.Key
                         join dvct in tenDVct on a.MaDVct equals dvct.MaDVct
                         select new BenhNhan
                         {
                             SoPhieu = a.SoPhieu,
                             LoaiTTPT = dvct.Loai ?? 0,
                             STTHT = a.STTHT,
                             TenBNhan = a.TenBNhan,
                             bhyt = chk_HT_TheBH.Checked ? a.bhyt : (a.bhyt.Length >= 15 ? "X" : ""),
                             DichVu = (a.bhyt.Length >= 15 ? "" : "X"),
                             gioitinh = a.gioitinh,
                             tuoi = a.tuoi,
                             diachi = a.diachi,
                             IDCLS = a.IDCLS,
                             MaBNhan = a.MaBNhan,
                             MaKP = a.MaKP,
                             ngaythang = _TTPT ? a.ngaythang : a.ngaythang,
                             LoiDan = _TTPT ? a.TenDV + " - " + a.LoiDan : a.LoiDan,
                             MaDV = a.MaDV,
                             KetLuan = _tenbc == "X-Quang" ? (DungChung.Bien.MaBV == "27023" ? a.KetLuan : (a.KetLuan + a.KetQua)) : a.KetLuan,
                             MaDVct = a.MaDVct,
                             KetQua = a.KetQua == null ? "" : a.KetQua,
                             TenDVct = dvct.TenDVct,
                             TenDV = dvct.TenDV,
                             TenCBcd = a.TenCBcd,
                             TenCBth = a.TenCBth,
                             YeuCau = a.YeuCau,
                             DSCBTH = a.DSCBTH,
                             STT_R = dvct.STT_R,
                             STT4069 = dvct.STT,
                             chandoan = a.chandoan,
                             soluong = a.soluong,
                             NgoaigioHC = a.NgoaigioHC,
                             CKham = c.CKham
                             // chidinh.KetLuan
                         }).ToList();
                _Tong = _Tong.Where(p => p.NgoaigioHC == 1 || p.NgoaigioHC == 2).ToList();
            }

            #region bệnh nhân nhập dịch vụ trực tiếp
            // dịch vụ nhập trực tiếp
            if (chk_DV_KD.Checked)
            {
                if (cboTT.SelectedIndex == 0)
                {
                    var donthuoc = (from dt in _Data.DThuocs.Where(p => p.PLDV == 2)
                                    join dtct in _Data.DThuoccts.Where(p => p.NgayNhap >= _ngayTu).Where(p => p.NgayNhap <= _ngayDen) on dt.IDDon equals dtct.IDDon
                                    select new { dtct.DSCBTH, dtct.MaKP, dtct.NgayNhap, dt.MaBNhan, dtct.IDCD, dtct.MaDV, dtct.MaCB, dtct.SoLuong }).ToList().Where(p => p.IDCD == 0 || p.IDCD == null).ToList();
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
                                         dt1.SoLuong,
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
                                   YeuCau = (DungChung.Bien.MaBV == "20001" && (_tenbc == "Siêu âm" || _tenbc == "X-Quang") && (RadDK.SelectedIndex == 1 || RadDK.SelectedIndex == 2)) ? a.TenDV : "",
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
                                   a.SoLuong,
                                   // chidinh.KetLuan
                               }).ToList();
                    List<BenhNhan> _lTong_dt = (from a in abc
                                                where (_idDTBN == 99 ? true : a.IDDTBN == _idDTBN)
                                                && (cbo_NoiTru.SelectedIndex == 2 ? true : a.NoiTru == cbo_NoiTru.SelectedIndex)
                                                select new BenhNhan
                                                {
                                                    // DSCBTH = a.DSCBTH,
                                                    YeuCau = a.YeuCau,
                                                    STTHT = 0,// a.STTHT,
                                                    TenDV = a.TenDV,
                                                    TenBNhan = a.TenBNhan,
                                                    bhyt = chk_HT_TheBH.Checked ? a.bhyt : (a.bhyt.Length >= 15 ? "X" : ""),
                                                    DichVu = (a.bhyt.Length >= 15 ? "" : "X"),
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
                                                    TenDVct = a.TenDV,
                                                    KetQua = "",
                                                    TenCBcd = a.TenCBcd,  //"a.TenCBcd",
                                                    DSCBTH = a.DSCBTH,
                                                    soluong = a.SoLuong
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
                                    select new { dtct.DSCBTH, dtct.MaKP, dtct.NgayNhap, dt.MaBNhan, dtct.IDCD, dtct.MaDV, dtct.MaCB, dtct.SoLuong }).ToList().Where(p => p.IDCD == 0 || p.IDCD == null).ToList();
                    var donthuoc2 = (from dt1 in donthuoc
                                     join dv in _lDichVu_chon.Where(p => p.Check) on dt1.MaDV equals dv.MaDV
                                     join kp in _Kphong.Where(p => p.chon) on dt1.MaKP equals kp.makp
                                     select new
                                     {
                                         dt1.MaDV,
                                         dt1.SoLuong,
                                         dt1.MaBNhan,
                                         dt1.MaKP,
                                         dt1.DSCBTH,
                                         dt1.NgayNhap,
                                         dt1.MaCB,
                                         dv.TenDV,
                                         dv.IdTieuNhom,

                                     }).ToList();
                    var abc = (from a in donthuoc2
                               join bn in _Data.BenhNhans on a.MaBNhan equals bn.MaBNhan

                               select new
                               {
                                   bn.IDDTBN,
                                   a.TenDV,
                                   bn.DTuong,
                                   bn.NoiTru,
                                   a.SoLuong,
                                   YeuCau = (DungChung.Bien.MaBV == "20001" && (_tenbc == "Siêu âm" || _tenbc == "X-Quang") && (RadDK.SelectedIndex == 1 || RadDK.SelectedIndex == 2)) ? a.TenDV : "",
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
                                                    YeuCau = a.YeuCau,
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
                                                    TenDVct = a.TenDV,
                                                    KetQua = "",
                                                    TenCBcd = a.TenCBcd,  //"a.TenCBcd",
                                                    DSCBTH = a.DSCBTH,
                                                    soluong = a.SoLuong,
                                                    // chidinh.KetLuan
                                                }).ToList();
                    if (_TTPT)
                        foreach (var item in _lTong_dt)
                        {
                            item.DSCBTH = _BSGMe(item.DSCBTH);

                        }
                    _Tong.AddRange(_lTong_dt);
                }
            }
            //
            #endregion

            //bool ht_Rp = false;// hiển thị theo STT report
            //if (RadDK.SelectedIndex == 1 ||  RadDK.SelectedIndex==2)
            //    ht_Rp = true; 
            if (DungChung.Bien.MaBV == "12122" && (RadDK.SelectedIndex == 1 || RadDK.SelectedIndex == 2))
            {
                string tenRG = lup_NhomXN.Text;
                _lDichVu = (from a in _Data.DichVucts.Where(p => p.Status == 1)
                            join dv in _Data.DichVus on a.MaDV equals dv.MaDV
                            join tn in _Data.TieuNhomDVs.Where(p => p.TenRG == tenRG) on dv.IdTieuNhom equals tn.IdTieuNhom

                            select new c_DSDV { MaDVct = a.MaDVct, TenDVct = a.TenDVct, MaDV = a.MaDV ?? 0, Stt_R = a.STT_R, STT4069 = a.STT_R, _thutu = a.STT }).OrderBy(p => p.STT4069).ToList();
            }


            else
            {
                _lDichVu = (from a in _Tong
                            group a by new { a.TenDVct, a.MaDVct,/** a.MaDV,*/ a.STT_R, a.STT4069 } into kq
                            select new c_DSDV { MaDVct = kq.Key.MaDVct, TenDVct = kq.Key.TenDVct, /**MaDV = kq.Key.MaDV,*/ Stt_R = kq.Key.STT_R, STT4069 = kq.Key.STT4069 }).OrderBy(p => p.STT4069).ToList();//.OrderBy(p => p.MaDV).ThenBy(p => p.TenDVct).ToList();
            }
            //if (RadDK.SelectedIndex == 1 ||  RadDK.SelectedIndex==2)
            //    _lDichVu = _lDichVu.OrderBy(p => p.Stt_R).ThenBy(p => p.MaDV).ToList();

            string[] _lTendvct = new string[200];
            for (int i = 0; i < 100; i++)
            {
                _lTendvct[i] = "";
            }
            for (int i = 0; i < _lDichVu.Count; i++)
            {
                _lTendvct[i] = _lDichVu.Skip(i).Take(1).First().MaDVct;
            }
            List<BenhNhan> _lbenhNhan_gr = new List<BenhNhan>();

            if (RadDK.SelectedIndex == 1 || RadDK.SelectedIndex == 2) // hiển thị kết quả
            {
                #region phẫu thuật thủ thuật
                if (_TTPT)
                {
                    _BenhNhan = (from a in _Tong
                                 group a by new { a.TenDV, a.DichVu, a.SoPhieu, a.LoaiTTPT, a.LoiDan, a.DSCBTH, a.KetQua, a.ngaythang, a.MaBNhan, a.TenBNhan, a.diachi, a.gioitinh, a.tuoi, a.bhyt, a.IDCLS, a.MaKP, a.KetLuan, a.TenCBcd, a.TenCBth, a.chandoan, a.CKham } into kq
                                 select new BenhNhan
                                 {
                                     TenDV = kq.Key.TenDV,
                                     SoPhieu = kq.Key.SoPhieu,
                                     LoaiTTPT = kq.Key.LoaiTTPT,
                                     LoiDan = kq.Key.LoiDan,
                                     YeuCau = String.Join(",", kq.Select(p => p.YeuCau).Distinct()),
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     diachi = kq.Key.diachi,
                                     gioitinh = kq.Key.gioitinh == "Nam" ? kq.Key.tuoi.ToString() : "",
                                     gioitinh_nu = kq.Key.gioitinh == "Nữ" ? kq.Key.tuoi.ToString() : "",
                                     tuoi = kq.Key.tuoi,
                                     bhyt = kq.Key.bhyt,
                                     DichVu = kq.Key.DichVu,
                                     IDCLS = kq.Key.IDCLS,
                                     MaKP = kq.Key.MaKP,
                                     ngaythang = kq.Key.ngaythang,
                                     KetLuan = kq.Key.KetLuan,
                                     KetQua = kq.Key.KetQua,
                                     TenCBcd = kq.Key.TenCBcd,
                                     TenCBth = kq.Key.TenCBth,
                                     DSCBTH = _BSGMe(kq.Key.DSCBTH),
                                     chandoan = kq.Key.chandoan,
                                     CKham = kq.Key.CKham
                                 }).OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan).ToList();
                    if (!chk_CDCLS.Checked)
                        _BenhNhan.Clear();
                }
                #endregion
                #region khác
                else
                    if (_CDHA)
                {
                    #region chẩn đoán hình ảnh
                    _BenhNhan = (from a in _Tong
                                 group a by new { a.DichVu, a.LoaiTTPT, a.SoPhieu, a.ngaythang, a.MaBNhan, a.TenBNhan, a.diachi, a.gioitinh, a.tuoi, a.bhyt, a.IDCLS, a.MaKP, a.KetLuan, a.TenCBcd, a.TenCBth, a.chandoan, a.CKham, a.KetQua } into kq
                                 select new BenhNhan
                                 {
                                     SoPhieu = kq.Key.SoPhieu,
                                     YeuCau = String.Join(",", kq.Select(p => p.YeuCau).Distinct()),
                                     LoaiTTPT = kq.Key.LoaiTTPT,
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     diachi = kq.Key.diachi,
                                     gioitinh = kq.Key.gioitinh == "Nam" ? kq.Key.tuoi.ToString() : "",
                                     gioitinh_nu = kq.Key.gioitinh == "Nữ" ? kq.Key.tuoi.ToString() : "",
                                     tuoi = kq.Key.tuoi,
                                     bhyt = kq.Key.bhyt,
                                     DichVu = kq.Key.DichVu,
                                     IDCLS = kq.Key.IDCLS,
                                     MaKP = kq.Key.MaKP,
                                     ngaythang = kq.Key.ngaythang,
                                     KetLuan = kq.Key.KetLuan,
                                     KetQua = kq.Key.KetQua,
                                     TenCBcd = kq.Key.TenCBcd,
                                     TenCBth = kq.Key.TenCBth,
                                     chandoan = kq.Key.chandoan,
                                     LoiDan = String.Join(",", kq.Where(p => p.LoiDan != "").Select(p => p.LoiDan).Distinct()),
                                     CKham = kq.Key.CKham
                                 }).OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan).ToList();
                    _BenhNhan = (from a in _BenhNhan
                                 select new BenhNhan
                                 {
                                     SoPhieu = a.SoPhieu,
                                     YeuCau = a.YeuCau,
                                     LoaiTTPT = a.LoaiTTPT,
                                     MaBNhan = a.MaBNhan,
                                     TenBNhan = a.TenBNhan,
                                     diachi = a.diachi,
                                     gioitinh = a.gioitinh,
                                     gioitinh_nu = a.gioitinh_nu,
                                     tuoi = a.tuoi,
                                     DichVu = a.DichVu,
                                     IDCLS = a.IDCLS,
                                     MaKP = a.MaKP,
                                     ngaythang = a.ngaythang,
                                     KetLuan = a.KetLuan,
                                     KetQua = a.KetQua,
                                     TenCBcd = a.TenCBcd,
                                     TenCBth = a.TenCBth,
                                     chandoan = a.chandoan,
                                     LoiDan = a.LoiDan,
                                     CKham = a.CKham,
                                     colKQ1 = _tenbc == "Siêu âm" ? (a.LoaiTTPT == 0 ? a.YeuCau : "") : (Convert.ToInt32(a.SoPhieu) == 1 ? "X" : ""),
                                     colKQ2 = _tenbc == "Siêu âm" ? (a.LoaiTTPT == 1 ? a.YeuCau : "") : (Convert.ToInt32(a.SoPhieu) == 2 ? "X" : ""),
                                     colKQ3 = _tenbc == "Siêu âm" ? (a.LoaiTTPT == 2 ? a.YeuCau : "") : (Convert.ToInt32(a.SoPhieu) == 3 ? "X" : ""),
                                     colKQ4 = _tenbc == "Siêu âm" ? (a.LoaiTTPT == 3 ? a.YeuCau : "") : (Convert.ToInt32(a.SoPhieu) == 4 ? "X" : ""),
                                 }).OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan).ToList();
                    #endregion
                }
                else
                {
                    #region không phải CĐHA
                    _BenhNhan = (from a in _Tong
                                 group a by new { a.DichVu, a.ngaythang, a.MaBNhan, a.TenBNhan, a.diachi, a.gioitinh, a.tuoi, a.bhyt, a.IDCLS, a.TenCBcd, a.MaKP, a.TenCBth, a.chandoan, a.CKham } into kq
                                 select new BenhNhan
                                 {
                                     MaBNhan = kq.Key.MaBNhan,
                                     TenBNhan = kq.Key.TenBNhan,
                                     diachi = kq.Key.diachi,
                                     gioitinh = kq.Key.gioitinh,
                                     YeuCau = String.Join(",", kq.Select(p => p.YeuCau).Distinct()),
                                     TenCBth = kq.Key.TenCBth,
                                     TuoiNam = kq.Key.gioitinh == "Nam" ? kq.Key.tuoi.ToString() : "",
                                     TuoiNu = kq.Key.gioitinh == "Nữ" ? kq.Key.tuoi.ToString() : "",
                                     tuoi = kq.Key.tuoi,
                                     bhyt = kq.Key.bhyt,
                                     DichVu = kq.Key.DichVu,
                                     IDCLS = kq.Key.IDCLS,
                                     MaKP = kq.Key.MaKP,
                                     TenCBcd = kq.Key.TenCBcd,
                                     ngaythang = kq.Key.ngaythang,
                                     chandoan = kq.Key.chandoan,
                                     CKham = kq.Key.CKham,
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

                                     colKQ31 = kq.Where(p => p.MaDVct == _lTendvct[30]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[30]).Max(p => p.KetQua),
                                     colKQ32 = kq.Where(p => p.MaDVct == _lTendvct[31]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[31]).Max(p => p.KetQua),
                                     colKQ33 = kq.Where(p => p.MaDVct == _lTendvct[32]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[32]).Max(p => p.KetQua),
                                     colKQ34 = kq.Where(p => p.MaDVct == _lTendvct[33]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[33]).Max(p => p.KetQua),
                                     colKQ35 = kq.Where(p => p.MaDVct == _lTendvct[34]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[34]).Max(p => p.KetQua),
                                     colKQ36 = kq.Where(p => p.MaDVct == _lTendvct[35]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[35]).Max(p => p.KetQua),
                                     colKQ37 = kq.Where(p => p.MaDVct == _lTendvct[36]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[36]).Max(p => p.KetQua),
                                     colKQ38 = kq.Where(p => p.MaDVct == _lTendvct[37]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[37]).Max(p => p.KetQua),
                                     colKQ39 = kq.Where(p => p.MaDVct == _lTendvct[38]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[38]).Max(p => p.KetQua),
                                     colKQ40 = kq.Where(p => p.MaDVct == _lTendvct[39]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[39]).Max(p => p.KetQua),
                                     colKQ41 = kq.Where(p => p.MaDVct == _lTendvct[40]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[40]).Max(p => p.KetQua),
                                     colKQ42 = kq.Where(p => p.MaDVct == _lTendvct[41]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[41]).Max(p => p.KetQua),
                                     colKQ43 = kq.Where(p => p.MaDVct == _lTendvct[42]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[42]).Max(p => p.KetQua),
                                     colKQ44 = kq.Where(p => p.MaDVct == _lTendvct[43]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[43]).Max(p => p.KetQua),
                                     colKQ45 = kq.Where(p => p.MaDVct == _lTendvct[44]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[44]).Max(p => p.KetQua),
                                     colKQ46 = kq.Where(p => p.MaDVct == _lTendvct[45]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[45]).Max(p => p.KetQua),
                                     colKQ47 = kq.Where(p => p.MaDVct == _lTendvct[46]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[46]).Max(p => p.KetQua),
                                     colKQ48 = kq.Where(p => p.MaDVct == _lTendvct[47]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[47]).Max(p => p.KetQua),
                                     colKQ49 = kq.Where(p => p.MaDVct == _lTendvct[48]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[48]).Max(p => p.KetQua),
                                     colKQ50 = kq.Where(p => p.MaDVct == _lTendvct[49]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[49]).Max(p => p.KetQua),
                                     colKQ51 = kq.Where(p => p.MaDVct == _lTendvct[50]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[50]).Max(p => p.KetQua),
                                     colKQ52 = kq.Where(p => p.MaDVct == _lTendvct[51]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[51]).Max(p => p.KetQua),
                                     colKQ53 = kq.Where(p => p.MaDVct == _lTendvct[52]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[52]).Max(p => p.KetQua),
                                     colKQ54 = kq.Where(p => p.MaDVct == _lTendvct[53]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[53]).Max(p => p.KetQua),
                                     colKQ55 = kq.Where(p => p.MaDVct == _lTendvct[54]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[54]).Max(p => p.KetQua),
                                     colKQ56 = kq.Where(p => p.MaDVct == _lTendvct[55]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[55]).Max(p => p.KetQua),
                                     colKQ57 = kq.Where(p => p.MaDVct == _lTendvct[56]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[56]).Max(p => p.KetQua),
                                     colKQ58 = kq.Where(p => p.MaDVct == _lTendvct[57]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[57]).Max(p => p.KetQua),

                                 }).OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan).ToList();
                    _lbenhNhan_gr = (from a in _Tong
                                     group a by a.MaDVct into kq
                                     select new BenhNhan
                                     {
                                         MaDVct = kq.Key,
                                         KetQua = kq.Where(p => p.KetQua.Length > 0).Count().ToString(),

                                     }).ToList();
                    #endregion
                }
                #endregion
            }
            else
            {
                #region hiển thị số lượng

                _BenhNhan = (from a in _Tong
                             group a by new { a.DichVu, a.ngaythang, a.MaBNhan, a.TenBNhan, a.chandoan, a.diachi, a.gioitinh, a.tuoi, a.TuoiNam, a.TuoiNu, a.bhyt, a.IDCLS, a.TenCBcd, a.MaKP, a.TenCBth } into kq
                             select new BenhNhan
                             {
                                 MaBNhan = kq.Key.MaBNhan,
                                 TenBNhan = kq.Key.TenBNhan,
                                 diachi = kq.Key.diachi,
                                 gioitinh = kq.Key.gioitinh,
                                 YeuCau = String.Join(",", kq.Select(p => p.YeuCau).Distinct()),
                                 LoiDan = String.Join(",", kq.Where(p => p.LoiDan != "").Select(p => p.LoiDan).Distinct()),
                                 TenCBth = kq.Key.TenCBth,
                                 tuoi = kq.Key.tuoi,
                                 TuoiNam = kq.Key.TuoiNam,
                                 TuoiNu = kq.Key.TuoiNu,
                                 bhyt = kq.Key.bhyt,
                                 DichVu = kq.Key.DichVu,
                                 IDCLS = kq.Key.IDCLS,
                                 MaKP = kq.Key.MaKP,
                                 TenCBcd = kq.Key.TenCBcd,
                                 ngaythang = kq.Key.ngaythang,
                                 chandoan = kq.Key.chandoan,
                                 colKQ1 = kq.Where(p => p.MaDVct == _lTendvct[0]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[0]).Sum(p => p.soluong).ToString(),
                                 colKQ2 = kq.Where(p => p.MaDVct == _lTendvct[1]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[1]).Sum(p => p.soluong).ToString(),
                                 colKQ3 = kq.Where(p => p.MaDVct == _lTendvct[2]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[2]).Sum(p => p.soluong).ToString(),
                                 colKQ4 = kq.Where(p => p.MaDVct == _lTendvct[3]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[3]).Sum(p => p.soluong).ToString(),
                                 colKQ5 = kq.Where(p => p.MaDVct == _lTendvct[4]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[4]).Sum(p => p.soluong).ToString(),
                                 colKQ6 = kq.Where(p => p.MaDVct == _lTendvct[5]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[5]).Sum(p => p.soluong).ToString(),
                                 colKQ7 = kq.Where(p => p.MaDVct == _lTendvct[6]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[6]).Sum(p => p.soluong).ToString(),
                                 colKQ8 = kq.Where(p => p.MaDVct == _lTendvct[7]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[7]).Sum(p => p.soluong).ToString(),
                                 colKQ9 = kq.Where(p => p.MaDVct == _lTendvct[8]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[8]).Sum(p => p.soluong).ToString(),
                                 colKQ10 = kq.Where(p => p.MaDVct == _lTendvct[9]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[9]).Sum(p => p.soluong).ToString(),
                                 colKQ11 = kq.Where(p => p.MaDVct == _lTendvct[10]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[10]).Sum(p => p.soluong).ToString(),
                                 colKQ12 = kq.Where(p => p.MaDVct == _lTendvct[11]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[11]).Sum(p => p.soluong).ToString(),
                                 colKQ13 = kq.Where(p => p.MaDVct == _lTendvct[12]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[12]).Sum(p => p.soluong).ToString(),
                                 colKQ14 = kq.Where(p => p.MaDVct == _lTendvct[13]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[13]).Sum(p => p.soluong).ToString(),
                                 colKQ15 = kq.Where(p => p.MaDVct == _lTendvct[14]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[14]).Sum(p => p.soluong).ToString(),
                                 colKQ16 = kq.Where(p => p.MaDVct == _lTendvct[15]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[15]).Sum(p => p.soluong).ToString(),
                                 colKQ17 = kq.Where(p => p.MaDVct == _lTendvct[16]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[16]).Sum(p => p.soluong).ToString(),
                                 colKQ18 = kq.Where(p => p.MaDVct == _lTendvct[17]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[17]).Sum(p => p.soluong).ToString(),
                                 colKQ19 = kq.Where(p => p.MaDVct == _lTendvct[18]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[18]).Sum(p => p.soluong).ToString(),
                                 colKQ20 = kq.Where(p => p.MaDVct == _lTendvct[19]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[19]).Sum(p => p.soluong).ToString(),
                                 colKQ21 = kq.Where(p => p.MaDVct == _lTendvct[20]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[20]).Sum(p => p.soluong).ToString(),
                                 colKQ22 = kq.Where(p => p.MaDVct == _lTendvct[21]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[21]).Sum(p => p.soluong).ToString(),
                                 colKQ23 = kq.Where(p => p.MaDVct == _lTendvct[22]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[22]).Sum(p => p.soluong).ToString(),
                                 colKQ24 = kq.Where(p => p.MaDVct == _lTendvct[23]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[23]).Sum(p => p.soluong).ToString(),
                                 colKQ25 = kq.Where(p => p.MaDVct == _lTendvct[24]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[24]).Sum(p => p.soluong).ToString(),
                                 colKQ26 = kq.Where(p => p.MaDVct == _lTendvct[25]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[25]).Sum(p => p.soluong).ToString(),
                                 colKQ27 = kq.Where(p => p.MaDVct == _lTendvct[26]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[26]).Sum(p => p.soluong).ToString(),
                                 colKQ28 = kq.Where(p => p.MaDVct == _lTendvct[27]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[27]).Sum(p => p.soluong).ToString(),
                                 colKQ29 = kq.Where(p => p.MaDVct == _lTendvct[28]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[28]).Sum(p => p.soluong).ToString(),
                                 colKQ30 = kq.Where(p => p.MaDVct == _lTendvct[29]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[29]).Sum(p => p.soluong).ToString(),
                                 colKQ31 = kq.Where(p => p.MaDVct == _lTendvct[30]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[30]).Max(p => p.KetQua),
                                 colKQ32 = kq.Where(p => p.MaDVct == _lTendvct[31]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[31]).Max(p => p.KetQua),
                                 colKQ33 = kq.Where(p => p.MaDVct == _lTendvct[32]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[32]).Max(p => p.KetQua),
                                 colKQ34 = kq.Where(p => p.MaDVct == _lTendvct[33]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[33]).Max(p => p.KetQua),
                                 colKQ35 = kq.Where(p => p.MaDVct == _lTendvct[34]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[34]).Max(p => p.KetQua),
                                 colKQ36 = kq.Where(p => p.MaDVct == _lTendvct[35]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[35]).Max(p => p.KetQua),
                                 colKQ37 = kq.Where(p => p.MaDVct == _lTendvct[36]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[36]).Max(p => p.KetQua),
                                 colKQ38 = kq.Where(p => p.MaDVct == _lTendvct[37]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[37]).Max(p => p.KetQua),
                                 colKQ39 = kq.Where(p => p.MaDVct == _lTendvct[38]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[38]).Max(p => p.KetQua),
                                 colKQ40 = kq.Where(p => p.MaDVct == _lTendvct[39]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[39]).Max(p => p.KetQua),
                                 colKQ41 = kq.Where(p => p.MaDVct == _lTendvct[40]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[40]).Max(p => p.KetQua),
                                 colKQ42 = kq.Where(p => p.MaDVct == _lTendvct[41]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[41]).Max(p => p.KetQua),
                                 colKQ43 = kq.Where(p => p.MaDVct == _lTendvct[42]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[42]).Max(p => p.KetQua),
                                 colKQ44 = kq.Where(p => p.MaDVct == _lTendvct[43]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[43]).Max(p => p.KetQua),
                                 colKQ45 = kq.Where(p => p.MaDVct == _lTendvct[44]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[44]).Max(p => p.KetQua),
                                 colKQ46 = kq.Where(p => p.MaDVct == _lTendvct[45]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[45]).Max(p => p.KetQua),
                                 colKQ47 = kq.Where(p => p.MaDVct == _lTendvct[46]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[46]).Max(p => p.KetQua),
                                 colKQ48 = kq.Where(p => p.MaDVct == _lTendvct[47]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[47]).Max(p => p.KetQua),
                                 colKQ49 = kq.Where(p => p.MaDVct == _lTendvct[48]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[48]).Max(p => p.KetQua),
                                 colKQ50 = kq.Where(p => p.MaDVct == _lTendvct[49]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[49]).Max(p => p.KetQua),
                                 colKQ51 = kq.Where(p => p.MaDVct == _lTendvct[50]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[50]).Max(p => p.KetQua),
                                 colKQ52 = kq.Where(p => p.MaDVct == _lTendvct[51]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[51]).Max(p => p.KetQua),
                                 colKQ53 = kq.Where(p => p.MaDVct == _lTendvct[52]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[52]).Max(p => p.KetQua),
                                 colKQ54 = kq.Where(p => p.MaDVct == _lTendvct[53]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[53]).Max(p => p.KetQua),
                                 colKQ55 = kq.Where(p => p.MaDVct == _lTendvct[54]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[54]).Max(p => p.KetQua),
                                 colKQ56 = kq.Where(p => p.MaDVct == _lTendvct[55]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[55]).Max(p => p.KetQua),
                                 colKQ57 = kq.Where(p => p.MaDVct == _lTendvct[56]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[56]).Max(p => p.KetQua),
                                 colKQ58 = kq.Where(p => p.MaDVct == _lTendvct[57]).Select(p => p.KetQua) == null ? "" : kq.Where(p => p.MaDVct == _lTendvct[57]).Max(p => p.KetQua),
                             }).OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan).ToList();

                
                _lbenhNhan_gr = (from a in _Tong
                                 group a by a.MaDVct into kq
                                 select new BenhNhan
                                 {
                                     MaDVct = kq.Key,
                                     KetQua = kq.Count().ToString(),

                                 }).ToList();
                #endregion
            }

            foreach (var a in _BenhNhan)
            {
                a.cel1 = a.colKQ1 = a.colKQ1 == "0" ? "" : a.colKQ1;
                a.cel2 = a.colKQ2 = a.colKQ2 == "0" ? "" : a.colKQ2;
                a.cel3 = a.colKQ3 = a.colKQ3 == "0" ? "" : a.colKQ3;
                a.cel4 = a.colKQ4 = a.colKQ4 == "0" ? "" : a.colKQ4;
                a.cel5 = a.colKQ5 = a.colKQ5 == "0" ? "" : a.colKQ5;
                a.cel6 = a.colKQ6 = a.colKQ6 == "0" ? "" : a.colKQ6;
                a.cel7 = a.colKQ7 = a.colKQ7 == "0" ? "" : a.colKQ7;
                a.cel8 = a.colKQ8 = a.colKQ8 == "0" ? "" : a.colKQ8;
                a.cel9 = a.colKQ9 = a.colKQ9 == "0" ? "" : a.colKQ9;
                a.cel10 = a.colKQ10 = a.colKQ10 == "0" ? "" : a.colKQ10;
                a.cel11 = a.colKQ11 = a.colKQ11 == "0" ? "" : a.colKQ11;
                a.cel12 = a.colKQ12 = a.colKQ12 == "0" ? "" : a.colKQ12;
                a.cel13 = a.colKQ13 = a.colKQ13 == "0" ? "" : a.colKQ13;
                a.cel14 = a.colKQ14 = a.colKQ14 == "0" ? "" : a.colKQ14;
                a.cel15 = a.colKQ15 = a.colKQ15 == "0" ? "" : a.colKQ15;
                a.cel16 = a.colKQ16 = a.colKQ16 == "0" ? "" : a.colKQ16;
                a.cel17 = a.colKQ17 = a.colKQ17 == "0" ? "" : a.colKQ17;
                a.cel18 = a.colKQ18 = a.colKQ18 == "0" ? "" : a.colKQ18;
                a.cel19 = a.colKQ19 = a.colKQ19 == "0" ? "" : a.colKQ19;
                a.cel20 = a.colKQ20 = a.colKQ20 == "0" ? "" : a.colKQ20;
                a.cel21 = a.colKQ21 = a.colKQ21 == "0" ? "" : a.colKQ21;
                a.cel22 = a.colKQ22 = a.colKQ22 == "0" ? "" : a.colKQ22;
                a.cel23 = a.colKQ23 = a.colKQ23 == "0" ? "" : a.colKQ23;
                a.cel24 = a.colKQ24 = a.colKQ24 == "0" ? "" : a.colKQ24;
                a.cel25 = a.colKQ25 = a.colKQ25 == "0" ? "" : a.colKQ25;
                a.cel26 = a.colKQ26 = a.colKQ26 == "0" ? "" : a.colKQ26;
                a.cel27 = a.colKQ27 = a.colKQ27 == "0" ? "" : a.colKQ27;
                a.cel28 = a.colKQ28 = a.colKQ28 == "0" ? "" : a.colKQ28;
                a.cel29 = a.colKQ29 = a.colKQ29 == "0" ? "" : a.colKQ29;
                a.cel30 = a.colKQ30 = a.colKQ30 == "0" ? "" : a.colKQ30;
                a.cel31 = a.colKQ31 = a.colKQ31 == "0" ? "" : a.colKQ31;
                a.cel32 = a.colKQ32 = a.colKQ32 == "0" ? "" : a.colKQ32;
                a.cel33 = a.colKQ33 = a.colKQ33 == "0" ? "" : a.colKQ33;
                a.cel34 = a.colKQ34 = a.colKQ34 == "0" ? "" : a.colKQ34;
                a.cel35 = a.colKQ35 = a.colKQ35 == "0" ? "" : a.colKQ35;
                a.cel36 = a.colKQ36 = a.colKQ36 == "0" ? "" : a.colKQ36;
                a.cel37 = a.colKQ37 = a.colKQ37 == "0" ? "" : a.colKQ37;
                a.cel38 = a.colKQ38 = a.colKQ38 == "0" ? "" : a.colKQ38;
                a.cel39 = a.colKQ39 = a.colKQ39 == "0" ? "" : a.colKQ39;
                a.cel40 = a.colKQ40 = a.colKQ40 == "0" ? "" : a.colKQ40;
                a.cel41 = a.colKQ41 = a.colKQ41 == "0" ? "" : a.colKQ41;
                a.cel42 = a.colKQ42 = a.colKQ42 == "0" ? "" : a.colKQ42;
                a.cel43 = a.colKQ43 = a.colKQ43 == "0" ? "" : a.colKQ43;
                a.cel44 = a.colKQ44 = a.colKQ44 == "0" ? "" : a.colKQ44;
                a.cel45 = a.colKQ45 = a.colKQ45 == "0" ? "" : a.colKQ45;
                a.cel46 = a.colKQ46 = a.colKQ46 == "0" ? "" : a.colKQ46;
                a.cel47 = a.colKQ47 = a.colKQ47 == "0" ? "" : a.colKQ47;
                a.cel48 = a.colKQ48 = a.colKQ48 == "0" ? "" : a.colKQ48;
                a.cel49 = a.colKQ49 = a.colKQ49 == "0" ? "" : a.colKQ49;
                a.cel50 = a.colKQ50 = a.colKQ50 == "0" ? "" : a.colKQ50;
                a.cel51 = a.colKQ51 = a.colKQ51 == "0" ? "" : a.colKQ51;
                a.cel52 = a.colKQ52 = a.colKQ52 == "0" ? "" : a.colKQ52;
                a.cel53 = a.colKQ53 = a.colKQ53 == "0" ? "" : a.colKQ53;
                a.cel54 = a.colKQ54 = a.colKQ54 == "0" ? "" : a.colKQ54;
                a.cel55 = a.colKQ55 = a.colKQ55 == "0" ? "" : a.colKQ55;
                a.cel56 = a.colKQ56 = a.colKQ56 == "0" ? "" : a.colKQ56;
                a.cel57 = a.colKQ57 = a.colKQ57 == "0" ? "" : a.colKQ57;
                a.cel58 = a.colKQ58 = a.colKQ58 == "0" ? "" : a.colKQ58;
               

            }

            var maBnhans = _BenhNhan.Select(o => o.MaBNhan);
            var bnkbenh = _Data.BNKBs.Where(o => maBnhans.Contains(o.MaBNhan ?? 0)).ToList();

            var chandoan = (from a in _BenhNhan
                            join cd in bnkbenh on a.MaBNhan equals cd.MaBNhan
                            where cd.MaKP == a.MaKP
                            select new { a.IDCLS, cd.MaBNhan, cd.MaKP, ChanDoan = string.IsNullOrEmpty(a.chandoan) ? (cd.ChanDoan + cd.BenhKhac) : a.chandoan }).ToList();
            var canbo = _Data.CanBoes.Where(o => true).ToList();
            foreach (var a in _BenhNhan)
            {
                foreach (var b in chandoan)
                {
                    if (a.MaBNhan == b.MaBNhan && a.IDCLS == b.IDCLS)
                    {
                        a.chandoan = b.ChanDoan;
                    }
                }
                a.TenCBcd = canbo.Where(p => p.MaCB == a.TenCBcd).Select(p => p.TenCB).FirstOrDefault();
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30005")
            {
                var ravien = _Data.RaViens.Where(o => maBnhans.Contains(o.MaBNhan)).ToList();
                var chandoanrv = (from bn in _BenhNhan
                                  join cd in ravien on bn.MaBNhan equals cd.MaBNhan
                                  select new { bn.IDCLS, cd.MaBNhan, cd.MaKP, ChanDoan = cd.ChanDoan }).ToList();
                foreach (var a in _BenhNhan)
                {
                    foreach (var b in chandoanrv)
                    {
                        if (a.MaBNhan == b.MaBNhan && a.IDCLS == b.IDCLS)
                        {
                            a.ChanDoanRV = b.ChanDoan;
                        }
                    }
                    //a.TenCBcd = canbo.Where(p => p.MaCB == a.TenCBcd).Select(p => p.TenCB).FirstOrDefault();
                }
            }
            foreach (var a in _BenhNhan)
            {
                //if (_tenbc == "Siêu âm" || _tenbc == "X-Quang")
                //{
                //    if (_lcb.Where(p => p.MaCB == a.TenCBcd).ToList().Count > 0)
                //    {
                //        a.noigui = _lcb.Where(p => p.MaCB == a.TenCBcd).First().TenCB;

                //    }
                //}
                //else
                //{
                if (_lkp.Where(p => p.MaKP == a.MaKP).ToList().Count > 0)
                {
                    a.noigui = _lkp.Where(p => p.MaKP == a.MaKP).First().TenKP;

                }
                //}
                if (_lcb.Where(p => p.MaCB == a.TenCBth).ToList().Count > 0)
                {
                    a.TenCBth = _lcb.Where(p => p.MaCB == a.TenCBth).First().TenCB;

                }
            }
            if (DungChung.Bien.MaBV == "30340")
            {
                foreach (BenhNhan a in _Tong)
                {
                    var kb = _Data.BNKBs.OrderByDescending(o => o.IDKB).FirstOrDefault(p => p.MaBNhan == a.MaBNhan);
                    if (kb != null)
                        a.chandoan = kb.ChanDoan + ((kb.BenhKhac == null || kb.BenhKhac == "") ? "" : ("; " + kb.BenhKhac));
                }
            }
            foreach (var a in _BenhNhan)
            {
                a.ngaythang1 = a.ngaythang.Date;
                //a.ngaythang = a.ngaythang.Date;
            }

            #region _TTPT && RadDK.SelectedIndex == 1 ||_TTPT && RadDK.SelectedIndex == 2
            if (_TTPT && RadDK.SelectedIndex == 1 ||_TTPT && RadDK.SelectedIndex == 2)
            {
                #region  Xuat excel
                string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                string[] _tieude = { "Stt", "Họ tên người bệnh", "Tuổi nam", "Tuổi nữ", "Địa chỉ", "Có BHYT", "Chẩn đoán trước thủ thuật", "Chẩn đoán sau thủ thuật", "Phương pháp thủ thuật", "Phương pháp vô cảm", "Ngày/Giờ thủ thuật", "Loại phẫu thuật", "Bác sỹ phẫu thuật", "Bác sỹ gây mê, tê", "Công khám", "Ghi chú" };
                string _filePath = "C:\\" + "Sổ " + lup_NhomXN.Text + ".xls";
                int[] _arrWidth = new int[] { };
                var qexcel = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 18];
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                }
                int num = 1;
                foreach (var r in qexcel)
                {
                    DungChung.Bien.MangHaiChieu[num, 0] = num;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.gioitinh;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.gioitinh_nu;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.bhyt;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.chandoan;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.KetQua;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.KetLuan;
                    DungChung.Bien.MangHaiChieu[num, 9] = r.LoiDan;
                    DungChung.Bien.MangHaiChieu[num, 10] = r.ngaythang.ToString("dd/MM/yyyy");
                    DungChung.Bien.MangHaiChieu[num, 11] = r.LoaiTTPT;
                    DungChung.Bien.MangHaiChieu[num, 12] = r.TenCBth;
                    DungChung.Bien.MangHaiChieu[num, 13] = r.DSCBTH;
                    DungChung.Bien.MangHaiChieu[num, 14] = r.CKham;
                    DungChung.Bien.MangHaiChieu[num, 15] = r.noigui;
                    num++;
                }
                //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);
                #endregion
                if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30005")
                {
                    BaoCao.Rep_SoPhauThuat_ThuThuat_408 rep1 = new BaoCao.Rep_SoPhauThuat_ThuThuat_408();
                    //rep1.TenBC.Value = ("Sổ tổng hợp " + lup_NhomXN.Text).ToUpper();
                    if (lup_NhomXN.Text == "Phẫu thuật")
                    {
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
                    rep1.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                    rep1.BindingData();
                    rep1.CreateDocument();
                    frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm1.ShowDialog();
                }
                else
                {
                    BaoCao.Rep_SoPhauThuat_ThuThuat_408_01071 rep1 = new BaoCao.Rep_SoPhauThuat_ThuThuat_408_01071();
                    //rep1.TenBC.Value = ("Sổ tổng hợp " + lup_NhomXN.Text).ToUpper();
                    if (lup_NhomXN.Text == "Phẫu thuật")
                    {
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
                    rep1.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                    rep1.BindingData();
                    rep1.CreateDocument();
                    frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                    frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                    frm1.ShowDialog();
                }
            }
            #endregion
            else
            {
                #region _CDHA && RadDK.SelectedIndex == 1 ||_CDHA &&  RadDK.SelectedIndex==2
                if ((_CDHA && RadDK.SelectedIndex == 1 || _CDHA && RadDK.SelectedIndex == 2))
                {
                    if (_tenbc == "X-Quang")
                    {
                        #region  Xuat excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        string[] _tieude = { "Stt", "Mã bệnh nhân", "Họ và tên","Tuổi nam", "Tuổi nữ", "Địa chỉ", "Đối tượng có BHYT", "Đối tượng viện phí",
                                               "Chẩn đoán", "Nơi gửi", "Y, B.Sỹ chỉ định", "Yêu cầu", "Kết quả", "Người đọc", "Cỡ phim 13/18", "cỡ phim 18/24", "Cỡ phim 24/30", "Cỡ phim 30/40", "Công khám" };
                        string _filePath = "C:\\" + "Sổ tổng hợp" + lup_NhomXN.Text + ".xls";
                        int[] _arrWidth = new int[] { };
                        var qexcel = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 20];
                        for (int i = 0; i < 19; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }
                        int num = 1;
                        foreach (var r in qexcel)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.gioitinh;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.gioitinh_nu;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.bhyt;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.DichVu;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.chandoan;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.noigui;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.TenCBcd;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.YeuCau;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.KetLuan;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.TenCBth;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.colKQ1;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.colKQ2;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.colKQ3;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.colKQ4;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.CKham;
                            num++;
                        }
                        #endregion
                        BaoCao.Rep_Th_CDHA_XQuang rep1 = new BaoCao.Rep_Th_CDHA_XQuang();
                        if (DungChung.Bien.MaBV == "20001" && (RadDK.SelectedIndex == 1 || RadDK.SelectedIndex==2))
                        {
                            rep1.xrTableCell5.Text = "Ghi chú";
                        }
                        rep1.TenBC.Value = ("Sổ tổng hợp " + lup_NhomXN.Text).ToUpper();
                        rep1.TuNgay.Value = _ngayTu.ToShortDateString();
                        rep1.DenNgay.Value = _ngayDen.ToShortDateString();
                        rep1.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        rep1.BindingData();
                        rep1.CreateDocument();
                        frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                        frm1.ShowDialog();


                        return;
                    }
                    if (_tenbc == "Điện tim" || _tenbc == "Nội soi" || (DungChung.Bien.MaBV == "30012" && _tenbc.ToLower().Contains("nội soi")))
                    {
                        #region  Xuat excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        string[] _tieude = { "Stt", "Mã bệnh nhân", "Họ và tên","Tuổi nam", "Tuổi nữ", "Địa chỉ", "Đối tượng có BHYT", "Đối tượng viện phí",
                                               "Chẩn đoán", "Nơi gửi", "Y, B.Sỹ chỉ định", "Yêu cầu", "Kết quả", "Người đọc", "Ngày giờ thực hiện", "Công khám" };
                        string _filePath = "C:\\" + "Sổ tổng hợp" + lup_NhomXN.Text + ".xls";
                        int[] _arrWidth = new int[] { };
                        var qexcel = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 16];
                        for (int i = 0; i < 16; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }
                        int num = 1;
                        foreach (var r in qexcel)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.gioitinh;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.gioitinh_nu;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.bhyt;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.DichVu;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.chandoan;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.noigui;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.TenCBcd;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.YeuCau;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.KetLuan;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.TenCBth;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.ngaythang.ToString("dd/MM/yyyy");
                            DungChung.Bien.MangHaiChieu[num, 15] = r.CKham;

                            num++;
                        }
                        #endregion
                        BaoCao.Rep_Th_CDHA_DienTim rep1 = new BaoCao.Rep_Th_CDHA_DienTim();
                        
                        rep1.TenBC.Value = ("Sổ " + lup_NhomXN.Text).ToUpper();
                        rep1.TuNgay.Value = _ngayTu.ToShortDateString();
                        rep1.DenNgay.Value = _ngayDen.ToShortDateString();
                        rep1.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        rep1.BindingData();
                        rep1.CreateDocument();
                        frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                        frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                        frm1.ShowDialog();
                        return;

                    }
                    if (_tenbc == "Siêu âm")
                    {
                        #region  Xuat excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        string[] _tieude = { "Stt", "Mã bệnh nhân", "Họ và tên","Tuổi nam", "Tuổi nữ", "Địa chỉ", "Đối tượng có BHYT", "Đối tượng viện phí",
                                               "Chẩn đoán","Yêu cầu", "Nơi gửi", "Y, B.Sỹ chỉ định",  "Yêu cầu siêu âm 2D", "Yêu cầu siêu âm màu","Yêu cầu siêu âm 3D - 4D", "Kết quả", "Người đọc", "Ngày giờ thực hiện", "Công khám" };
                        string _filePath = "C:\\" + "Sổ" + lup_NhomXN.Text + ".xls";
                        int[] _arrWidth = new int[] { };
                        var qexcel = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 19];
                        for (int i = 0; i < 19; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }
                        int num = 1;
                        foreach (var r in qexcel)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.gioitinh;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.gioitinh_nu;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.bhyt;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.DichVu;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.chandoan;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.YeuCau;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.noigui;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.TenCBcd;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.colKQ1;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.colKQ2;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.colKQ3;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.KetLuan;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.TenCBth;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.ngaythang.ToString("dd/MM/yyyy");
                            DungChung.Bien.MangHaiChieu[num, 18] = r.CKham;
                            num++;
                        }
                        #endregion

                        if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "27023")
                        {
                            BaoCao.Rep_Th_CDHA_SieuAm_20001 rep1 = new BaoCao.Rep_Th_CDHA_SieuAm_20001();
                            if (RadDK.SelectedIndex == 1 || RadDK.SelectedIndex == 2)
                            {
                                rep1.xrTableCell5.Text = "Ghi chú";
                            }
                            rep1.TenBC.Value = ("Sổ " + lup_NhomXN.Text).ToUpper();
                            rep1.TuNgay.Value = _ngayTu.ToShortDateString();
                            rep1.DenNgay.Value = _ngayDen.ToShortDateString();
                            rep1.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                            rep1.BindingData();
                            rep1.CreateDocument();
                            frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                            frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                            frm1.ShowDialog();
                        }
                        else
                        {
                            BaoCao.Rep_Th_CDHA_SieuAm rep1 = new BaoCao.Rep_Th_CDHA_SieuAm();
                            rep1.TenBC.Value = ("Sổ " + lup_NhomXN.Text).ToUpper();
                            rep1.TuNgay.Value = _ngayTu.ToShortDateString();
                            rep1.DenNgay.Value = _ngayDen.ToShortDateString();
                            rep1.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                            rep1.BindingData();
                            rep1.CreateDocument();
                            frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                            frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                            frm1.ShowDialog();
                        }
                        return;

                    }
                    else
                    {
                        #region  Xuat excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        string[] _tieude = { "Stt", "Họ và tên", "Tuổi nam", "Tuổi nữ", "Địa chỉ", "BHYT", "Chẩn đoán","Nơi gửi",
                                               "Yêu cầu", "Kết quả", "Người đọc","Ngày thực hiện", "Ghi chú", "Công khám" };
                        string _filePath = "C:\\" + "Sổ tổng hợp" + lup_NhomXN.Text + ".xls";
                        int[] _arrWidth = new int[] { };
                        var qexcel = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 14];
                        for (int i = 0; i < 14; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }
                        int num = 1;
                        foreach (var r in qexcel)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.gioitinh;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.gioitinh_nu;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.bhyt;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.chandoan;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.noigui;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.YeuCau;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.KetLuan;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.TenCBth;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.ngaythang.ToString("dd/MM/yyyy");
                            DungChung.Bien.MangHaiChieu[num, 12] = "";
                            DungChung.Bien.MangHaiChieu[num, 13] = r.CKham;
                            num++;
                        }
                        #endregion
                        if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30005")
                        {
                            BaoCao.Rep_Th_CDHA rep1 = new BaoCao.Rep_Th_CDHA();
                            rep1.TenBC.Value = ("Sổ tổng hợp " + lup_NhomXN.Text).ToUpper();
                            rep1.TuNgay.Value = _ngayTu.ToShortDateString();
                            rep1.DenNgay.Value = _ngayDen.ToShortDateString();
                            rep1.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                            rep1.BindingData();
                            rep1.CreateDocument();
                            frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                            frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                            frm1.ShowDialog();
                        }
                        else
                        {
                            BaoCao.Rep_Th_CDHA_01071 rep1 = new BaoCao.Rep_Th_CDHA_01071();
                            rep1.TenBC.Value = ("Sổ tổng hợp " + lup_NhomXN.Text).ToUpper();
                            rep1.TuNgay.Value = _ngayTu.ToShortDateString();
                            rep1.DenNgay.Value = _ngayDen.ToShortDateString();
                            rep1.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                            rep1.BindingData();
                            rep1.CreateDocument();
                            frmIn frm1 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                            frm1.prcIN.PrintingSystem = rep1.PrintingSystem;
                            frm1.ShowDialog();
                        }
                    }
                }
                else
                {

                    #region xét nghiệm huyết học và sinh hóa máu
                    if ((_tenbc == "XN huyết học" || _tenbc == "XN hóa sinh máu" || _tenbc == "XN dịch chọc dò") && (RadDK.SelectedIndex == 1||RadDK.SelectedIndex==2))
                    {
                        
                        #region  Xuat excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        #region tiêu đề
                        string[] _tieude = new string[73];
                        _tieude[0] = "STT";
                        _tieude[1] = "Mã bệnh nhân";
                        _tieude[2] = "Họ tên người bệnh";
                        _tieude[3] = "Tuổi nam";
                        _tieude[4] = "Tuổi nữ";
                        _tieude[5] = "Địa chỉ";
                        _tieude[6] = "Có BHYT";
                        _tieude[7] = "Viện phí trực tiếp";
                        _tieude[8] = "Nơi gửi";
                        _tieude[9] = "Y, Bs chỉ định";
                        _tieude[10] = "Yêu cầu";
                        _tieude[11] = "Người đọc";
                        for (int i = 0; i < _lDichVu.Count; i++)
                        {
                            if (i < 59) // số cột dịch vụ
                            {
                                string _tendv = _lDichVu.Skip(i).First().TenDVct;
                                _tieude[i + 12] = _tendv;

                            }
                        }
                        _tieude[71] = "Ngày tháng";

                        _tieude[72] = "Công khám";


                        #endregion tieu đề
                        string _filePath = "C:\\" + "Sổ " + lup_NhomXN.Text + ".xls";
                        int[] _arrWidth = new int[] { };
                        var qexcel = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 73];
                        for (int i = 0; i < 73; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }
                        int num = 1;
                        foreach (var r in qexcel)
                        {
                            
                                DungChung.Bien.MangHaiChieu[num, 0] = num;
                                DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                                DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                                DungChung.Bien.MangHaiChieu[num, 3] = r.TuoiNam;
                                DungChung.Bien.MangHaiChieu[num, 4] = r.TuoiNu;
                                DungChung.Bien.MangHaiChieu[num, 5] = r.diachi;
                                DungChung.Bien.MangHaiChieu[num, 6] = r.bhyt;
                                DungChung.Bien.MangHaiChieu[num, 7] = r.DichVu;
                                DungChung.Bien.MangHaiChieu[num, 8] = r.noigui;
                                DungChung.Bien.MangHaiChieu[num, 9] = r.TenCBcd;
                                DungChung.Bien.MangHaiChieu[num, 10] = r.YeuCau;
                                DungChung.Bien.MangHaiChieu[num, 11] = r.TenCBth;
                                DungChung.Bien.MangHaiChieu[num, 12] = r.colKQ1;
                                DungChung.Bien.MangHaiChieu[num, 13] = r.colKQ2;
                                DungChung.Bien.MangHaiChieu[num, 14] = r.colKQ3;
                                DungChung.Bien.MangHaiChieu[num, 15] = r.colKQ4;
                                DungChung.Bien.MangHaiChieu[num, 16] = r.colKQ5;
                                DungChung.Bien.MangHaiChieu[num, 17] = r.colKQ6;
                                DungChung.Bien.MangHaiChieu[num, 18] = r.colKQ7;
                                DungChung.Bien.MangHaiChieu[num, 19] = r.colKQ8;
                                DungChung.Bien.MangHaiChieu[num, 20] = r.colKQ9;
                                DungChung.Bien.MangHaiChieu[num, 21] = r.colKQ10;
                                DungChung.Bien.MangHaiChieu[num, 22] = r.colKQ11;
                                DungChung.Bien.MangHaiChieu[num, 23] = r.colKQ12;
                                DungChung.Bien.MangHaiChieu[num, 24] = r.colKQ13;
                                DungChung.Bien.MangHaiChieu[num, 25] = r.colKQ14;
                                DungChung.Bien.MangHaiChieu[num, 26] = r.colKQ15;
                                DungChung.Bien.MangHaiChieu[num, 27] = r.colKQ16;
                                DungChung.Bien.MangHaiChieu[num, 28] = r.colKQ17;
                                DungChung.Bien.MangHaiChieu[num, 29] = r.colKQ18;
                                DungChung.Bien.MangHaiChieu[num, 30] = r.colKQ19;
                                DungChung.Bien.MangHaiChieu[num, 31] = r.colKQ20;
                                DungChung.Bien.MangHaiChieu[num, 32] = r.colKQ21;
                                DungChung.Bien.MangHaiChieu[num, 33] = r.colKQ22;
                                DungChung.Bien.MangHaiChieu[num, 34] = r.colKQ23;
                                DungChung.Bien.MangHaiChieu[num, 35] = r.colKQ24;
                                DungChung.Bien.MangHaiChieu[num, 36] = r.colKQ25;
                                DungChung.Bien.MangHaiChieu[num, 37] = r.colKQ26;
                                DungChung.Bien.MangHaiChieu[num, 38] = r.colKQ27;
                                DungChung.Bien.MangHaiChieu[num, 39] = r.colKQ28;
                                DungChung.Bien.MangHaiChieu[num, 40] = r.colKQ29;
                                DungChung.Bien.MangHaiChieu[num, 41] = r.colKQ30;
                                DungChung.Bien.MangHaiChieu[num, 42] = r.colKQ31;
                                DungChung.Bien.MangHaiChieu[num, 43] = r.colKQ32;
                                DungChung.Bien.MangHaiChieu[num, 44] = r.colKQ33;
                                DungChung.Bien.MangHaiChieu[num, 45] = r.colKQ34;
                                DungChung.Bien.MangHaiChieu[num, 46] = r.colKQ35;
                                DungChung.Bien.MangHaiChieu[num, 47] = r.colKQ36;
                                DungChung.Bien.MangHaiChieu[num, 48] = r.colKQ37;
                                DungChung.Bien.MangHaiChieu[num, 49] = r.colKQ38;
                                DungChung.Bien.MangHaiChieu[num, 50] = r.colKQ39;
                                DungChung.Bien.MangHaiChieu[num, 51] = r.colKQ40;
                                DungChung.Bien.MangHaiChieu[num, 52] = r.colKQ41;
                                DungChung.Bien.MangHaiChieu[num, 53] = r.colKQ42;
                                DungChung.Bien.MangHaiChieu[num, 54] = r.colKQ43;
                                DungChung.Bien.MangHaiChieu[num, 55] = r.colKQ44;
                                DungChung.Bien.MangHaiChieu[num, 56] = r.colKQ45;
                                DungChung.Bien.MangHaiChieu[num, 57] = r.colKQ46;
                                DungChung.Bien.MangHaiChieu[num, 58] = r.colKQ47;
                                DungChung.Bien.MangHaiChieu[num, 59] = r.colKQ48;
                                DungChung.Bien.MangHaiChieu[num, 60] = r.colKQ49;
                                DungChung.Bien.MangHaiChieu[num, 61] = r.colKQ50;
                                DungChung.Bien.MangHaiChieu[num, 62] = r.colKQ51;
                                DungChung.Bien.MangHaiChieu[num, 63] = r.colKQ52;
                                DungChung.Bien.MangHaiChieu[num, 64] = r.colKQ53;
                                DungChung.Bien.MangHaiChieu[num, 65] = r.colKQ54;
                                DungChung.Bien.MangHaiChieu[num, 66] = r.colKQ55;
                                DungChung.Bien.MangHaiChieu[num, 67] = r.colKQ56;
                                DungChung.Bien.MangHaiChieu[num, 68] = r.colKQ57;
                                DungChung.Bien.MangHaiChieu[num, 69] = r.colKQ58;
                                DungChung.Bien.MangHaiChieu[num, 70] = r.TenCBth;
                                DungChung.Bien.MangHaiChieu[num, 71] = r.ngaythang.ToString("dd/MM/yyyy");
                                DungChung.Bien.MangHaiChieu[num, 72] = r.CKham;

                                num++;
                           
                         
                        }
                        #endregion
                        BaoCao.Rep_TongHop_XN_Mau_4069 rep = new BaoCao.Rep_TongHop_XN_Mau_4069(_lDichVu, _lbenhNhan_gr);
                        if (DungChung.Bien.MaBV == "24012" && RadDK.SelectedIndex == 2)
                        {
                            rep.Sub_24012.Visible = true;
                            rep.Sub_24012_1.Visible = true;
                            rep.Sub_24012_2.Visible = true;
                           rep. SubBand3.Visible = false;
                            rep.SubBand5.Visible = true;
                            rep.SubBanddefault.Visible = false;
                            rep.SubBand6.Visible = false;
                            rep.SubBand4.Visible = false;

                        }
                        else if (DungChung.Bien.MaBV == "24012" && RadDK.SelectedIndex == 1)
                        {
                            rep.Sub_24012.Visible = false;
                            rep.Sub_24012_1.Visible = false;
                            rep.Sub_24012_2.Visible = false;
                            rep.SubBand3.Visible = true;
                            rep.SubBand4.Visible = true;
                            rep.SubBanddefault.Visible = true;
                            rep.SubBand6.Visible = true;
                            rep.SubBand5.Visible = false;
 

                        }
                        rep.TenBC.Value = DungChung.Bien.MaBV == "30003" ? "SỔ " + lup_NhomXN.Text.ToUpper() : ("Sổ tổng hợp " + lup_NhomXN.Text).ToUpper();
                        rep.TuNgayDenNgay.Value = "Từ ngày: " + _ngayTu.ToShortDateString() + " đến ngày: " + _ngayDen.ToShortDateString();
                        // BaoCao.Rep_ThHoaSinhMauSL_Moi rep = new BaoCao.Rep_ThHoaSinhMauSL_Moi();
                        rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        rep.BindingData();

                        rep.CreateDocument();
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                        frm.Refresh();
                        if (DungChung.Bien.MaBV == "30372")
                        {
                            if (_lDichVu.Count > 29)
                            {

                                BaoCao.Rep_TongHop_XN_Mau_4069_2 rep2 = new BaoCao.Rep_TongHop_XN_Mau_4069_2(_lDichVu, _lbenhNhan_gr);

                                rep2.TenBC.Value = DungChung.Bien.MaBV == "30003" ? "SỔ " + lup_NhomXN.Text.ToUpper() : ("Sổ tổng hợp " + lup_NhomXN.Text).ToUpper();
                                rep2.TuNgayDenNgay.Value = "Từ ngày: " + _ngayTu.ToShortDateString() + " đến ngày: " + _ngayDen.ToShortDateString();
                                // BaoCao.Rep_ThHoaSinhMauSL_Moi rep = new BaoCao.Rep_ThHoaSinhMauSL_Moi();

                                rep2.DataSource = _BenhNhan/**.Where(p => p.colKQ30 != null || p.colKQ31 != null || p.colKQ32 != null || p.colKQ33 != null || p.colKQ34 != null || p.colKQ35 != null || p.colKQ36 != null || p.colKQ37 != null || p.colKQ38 != null || p.colKQ39 != null || p.colKQ40 != null || p.colKQ41 != null || p.colKQ42 != null || p.colKQ43 != null || p.colKQ44 != null || p.colKQ45 != null || p.colKQ46 != null || p.colKQ47 != null || p.colKQ48 != null || p.colKQ49 != null || p.colKQ50 != null || p.colKQ51 != null || p.colKQ52 != null || p.colKQ53 != null || p.colKQ54 != null || p.colKQ55 != null || p.colKQ56 != null || p.colKQ57 != null || p.colKQ58 != null)*/.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                                rep2.BindingData();
                                rep2.CreateDocument();
                                frmIn frm2 = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                                frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
                                frm2.ShowDialog();
                            }
                        }

                    }
                  
                    #endregion
                    #region xét nghiệm nước tiểu
                    else if ((_tenbc == "XN nước tiểu") && (RadDK.SelectedIndex == 1 || RadDK.SelectedIndex == 2))
                    {
                        #region  Xuat excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        #region tiêu đề
                        string[] _tieude = new string[43];
                        _tieude[0] = "STT";
                        _tieude[1] = "Mã bệnh nhân";
                        _tieude[2] = "Họ tên người bệnh";
                        _tieude[3] = "Tuổi nam";
                        _tieude[4] = "Tuổi nữ";
                        _tieude[5] = "Địa chỉ";
                        _tieude[6] = "Có BHYT";
                        _tieude[7] = "Viện phí trực tiếp";
                        _tieude[8] = "Chẩn đoán";
                        _tieude[9] = "Yêu cầu";
                        _tieude[10] = "Nơi gửi";
                        _tieude[11] = "Y, Bs chỉ định";

                        for (int i = 0; i < _lDichVu.Count; i++)
                        {
                            if (i < 28) // số cột dịch vụ
                            {
                                string _tendv = _lDichVu.Skip(i).First().TenDVct;
                                _tieude[i + 12] = _tendv;

                            }
                        }
                        _tieude[40] = "Người đọc";
                        _tieude[41] = "Ngày tháng";
                        _tieude[42] = "Công khám";

                        #endregion tieu đề
                        string _filePath = "C:\\" + "Sổ " + lup_NhomXN.Text + ".xls";
                        int[] _arrWidth = new int[] { };
                        var qexcel = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 42];
                        for (int i = 0; i < 42; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }
                        int num = 1;
                        foreach (var r in qexcel)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.TenBNhan;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.TuoiNam;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.TuoiNu;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.bhyt;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.DichVu;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.chandoan;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.noigui;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.TenCBcd;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.colKQ1;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.colKQ2;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.colKQ3;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.colKQ4;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.colKQ5;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.colKQ6;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.colKQ7;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.colKQ8;
                            DungChung.Bien.MangHaiChieu[num, 19] = r.colKQ9;
                            DungChung.Bien.MangHaiChieu[num, 20] = r.colKQ10;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.colKQ11;
                            DungChung.Bien.MangHaiChieu[num, 22] = r.colKQ12;
                            DungChung.Bien.MangHaiChieu[num, 23] = r.colKQ13;
                            DungChung.Bien.MangHaiChieu[num, 24] = r.colKQ14;
                            DungChung.Bien.MangHaiChieu[num, 25] = r.colKQ15;
                            DungChung.Bien.MangHaiChieu[num, 26] = r.colKQ16;
                            DungChung.Bien.MangHaiChieu[num, 27] = r.colKQ17;
                            DungChung.Bien.MangHaiChieu[num, 28] = r.colKQ18;
                            DungChung.Bien.MangHaiChieu[num, 39] = r.colKQ19;
                            DungChung.Bien.MangHaiChieu[num, 30] = r.colKQ20;
                            DungChung.Bien.MangHaiChieu[num, 31] = r.colKQ21;
                            DungChung.Bien.MangHaiChieu[num, 32] = r.colKQ22;
                            DungChung.Bien.MangHaiChieu[num, 33] = r.colKQ23;
                            DungChung.Bien.MangHaiChieu[num, 34] = r.colKQ24;
                            DungChung.Bien.MangHaiChieu[num, 35] = r.colKQ25;
                            DungChung.Bien.MangHaiChieu[num, 36] = r.colKQ26;
                            DungChung.Bien.MangHaiChieu[num, 37] = r.colKQ27;
                            DungChung.Bien.MangHaiChieu[num, 38] = r.colKQ28;
                            DungChung.Bien.MangHaiChieu[num, 39] = r.TenCBth;
                            DungChung.Bien.MangHaiChieu[num, 40] = r.ngaythang.ToString("dd/MM/yyyy");
                            DungChung.Bien.MangHaiChieu[num, 41] = r.CKham;
                            num++;
                        }
                        #endregion
                        BaoCao.Rep_TongHop_XN_NuocTieu_4069 rep = new BaoCao.Rep_TongHop_XN_NuocTieu_4069(_lDichVu, _lbenhNhan_gr);
                        if(DungChung.Bien.MaBV=="24012"&&RadDK.SelectedIndex==2)
                        {
                            rep.xrTable2.Visible = false;
                            rep.xrTable7.Visible = true;
                            rep.GroupHeader1.Visible = false;
                            rep.GroupHeader2.Visible = true;
                            rep.xrTable1.Visible = false;
                            rep.xrTable13.Visible = true;
                            rep.xrTable3.Visible = false;
                            rep.xrTable10.Visible = true;
                            rep.xrTable5.Visible = false;
                            rep.xrTable11.Visible = true;
                            rep.xrLine1.Visible = false;
                            rep.xrTableCell30.Visible = false;
                            rep.xrLine2.Visible = true;
                        }
                        else if (DungChung.Bien.MaBV == "24012" && RadDK.SelectedIndex == 1)
                            {
                                rep.xrTable2.Visible = true;
                                rep.xrTable7.Visible = false;
                            rep.GroupHeader1.Visible = true;
                            rep.GroupHeader2.Visible = false;
                            rep.xrTable1.Visible = true;
                                rep.xrTable13.Visible = false;
                                rep.xrTable3.Visible = true;
                                rep.xrTable10.Visible = false;
                                rep.xrTable5.Visible = true;
                                rep.xrTable11.Visible = false;
                            rep.xrLine1.Visible = true;
                            rep.xrTableCell30.Visible = true;
                            rep.xrLine2.Visible = false;
                        }
                        rep.TenBC.Value = DungChung.Bien.MaBV == "30003" ? "SỔ " + lup_NhomXN.Text.ToUpper() : ("Sổ tổng hợp " + lup_NhomXN.Text).ToUpper();
                        rep.TuNgayDenNgay.Value = "Từ ngày: " + _ngayTu.ToShortDateString() + " đến ngày: " + _ngayDen.ToShortDateString();
                        // BaoCao.Rep_ThHoaSinhMauSL_Moi rep = new BaoCao.Rep_ThHoaSinhMauSL_Moi();
                        rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    #endregion
                    #region xét nghiệm khác
                    else
                    {
                        #region  Xuat excel
                        string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
                        #region tiêu đề
                        string[] _tieude = new string[40];
                        _tieude[0] = "STT";
                        _tieude[1] = "Họ và tên";
                        _tieude[2] = "Giới tính";
                        _tieude[3] = "Tuổi";
                        _tieude[4] = "Địa chỉ";
                        _tieude[5] = "BHYT";
                        _tieude[6] = "Chẩn đoán";
                        _tieude[7] = "Yêu cầu";
                        _tieude[8] = "Nơi gửi";
                        for (int i = 0; i < _lDichVu.Count; i++)
                        {
                            if (i < 30) // số cột dịch vụ
                            {
                                string _tendv = _lDichVu.Skip(i).First().TenDVct;
                                _tieude[i + 9] = _tendv;

                            }
                        }
                        _tieude[37] = "Người đọc";
                        _tieude[38] = "Ngày thực hiện";
                        _tieude[39] = "Công khám";
                        #endregion tieu đề
                        string _filePath = "C:\\" + "Sổ " + lup_NhomXN.Text + ".xls";
                        int[] _arrWidth = new int[] { };
                        var qexcel = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                        DungChung.Bien.MangHaiChieu = new Object[qexcel.Count() + 1, 40];
                        for (int i = 0; i < 40; i++)
                        {
                            DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
                        }
                        int num = 1;
                        foreach (var r in qexcel)
                        {
                            DungChung.Bien.MangHaiChieu[num, 0] = num;
                            DungChung.Bien.MangHaiChieu[num, 1] = r.TenBNhan;
                            DungChung.Bien.MangHaiChieu[num, 2] = r.gioitinh;
                            DungChung.Bien.MangHaiChieu[num, 3] = r.tuoi;
                            DungChung.Bien.MangHaiChieu[num, 4] = r.diachi;
                            DungChung.Bien.MangHaiChieu[num, 5] = r.bhyt;
                            DungChung.Bien.MangHaiChieu[num, 6] = r.chandoan;
                            DungChung.Bien.MangHaiChieu[num, 7] = r.YeuCau;
                            DungChung.Bien.MangHaiChieu[num, 8] = r.noigui;
                            DungChung.Bien.MangHaiChieu[num, 9] = r.colKQ1;
                            DungChung.Bien.MangHaiChieu[num, 10] = r.colKQ2;
                            DungChung.Bien.MangHaiChieu[num, 11] = r.colKQ3;
                            DungChung.Bien.MangHaiChieu[num, 12] = r.colKQ4;
                            DungChung.Bien.MangHaiChieu[num, 13] = r.colKQ5;
                            DungChung.Bien.MangHaiChieu[num, 14] = r.colKQ6;
                            DungChung.Bien.MangHaiChieu[num, 15] = r.colKQ7;
                            DungChung.Bien.MangHaiChieu[num, 16] = r.colKQ8;
                            DungChung.Bien.MangHaiChieu[num, 17] = r.colKQ9;
                            DungChung.Bien.MangHaiChieu[num, 18] = r.colKQ10;
                            DungChung.Bien.MangHaiChieu[num, 19] = r.colKQ11;
                            DungChung.Bien.MangHaiChieu[num, 20] = r.colKQ12;
                            DungChung.Bien.MangHaiChieu[num, 21] = r.colKQ13;
                            DungChung.Bien.MangHaiChieu[num, 22] = r.colKQ14;
                            DungChung.Bien.MangHaiChieu[num, 23] = r.colKQ15;
                            DungChung.Bien.MangHaiChieu[num, 24] = r.colKQ16;
                            DungChung.Bien.MangHaiChieu[num, 25] = r.colKQ17;
                            DungChung.Bien.MangHaiChieu[num, 26] = r.colKQ18;
                            DungChung.Bien.MangHaiChieu[num, 27] = r.colKQ19;
                            DungChung.Bien.MangHaiChieu[num, 28] = r.colKQ20;
                            DungChung.Bien.MangHaiChieu[num, 29] = r.colKQ21;
                            DungChung.Bien.MangHaiChieu[num, 30] = r.colKQ22;
                            DungChung.Bien.MangHaiChieu[num, 31] = r.colKQ23;
                            DungChung.Bien.MangHaiChieu[num, 32] = r.colKQ24;
                            DungChung.Bien.MangHaiChieu[num, 33] = r.colKQ25;
                            DungChung.Bien.MangHaiChieu[num, 34] = r.colKQ26;
                            DungChung.Bien.MangHaiChieu[num, 35] = r.colKQ27;
                            DungChung.Bien.MangHaiChieu[num, 36] = r.colKQ28;
                            DungChung.Bien.MangHaiChieu[num, 37] = r.TenCBth;
                            DungChung.Bien.MangHaiChieu[num, 38] = r.ngaythang.ToString("dd/MM/yyyy");
                            DungChung.Bien.MangHaiChieu[num, 39] = r.CKham;
                            num++;
                        }
                        #endregion
                        if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30005")
                        {
                            if (DungChung.Bien.MaBV == "27001" && _tenbc == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong)
                            {
                                BaoCao.Rep_TongHop_DoLX_27001 rep = new BaoCao.Rep_TongHop_DoLX_27001(_lDichVu, _lbenhNhan_gr);
                                if ((DungChung.Bien.MaBV == "30005" && lup_NhomXN.Text.Contains("XN ")) || (DungChung.Bien.MaBV == "20001" && lup_NhomXN.Text.Contains("XN hóa sinh máu")))
                                {
                                    rep.GroupHeader1.Visible = true;
                                }
                                else
                                    rep.GroupHeader1.Visible = false;
                                rep.TenBC.Value = DungChung.Bien.MaBV == "30003" ? "SỔ " + lup_NhomXN.Text.ToUpper() : "Sổ tổng hợp " + lup_NhomXN.Text;
                                rep.TuNgayDenNgay.Value = "Từ ngày: " + _ngayTu.ToShortDateString() + " đến ngày: " + _ngayDen.ToShortDateString();
                                // BaoCao.Rep_ThHoaSinhMauSL_Moi rep = new BaoCao.Rep_ThHoaSinhMauSL_Moi();
                                rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                                rep.BindingData();
                                rep.CreateDocument();
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            {
                                BaoCao.Rep_TongHop_XN rep = new BaoCao.Rep_TongHop_XN(_lDichVu, _lbenhNhan_gr);
                                if ((DungChung.Bien.MaBV == "30005" && lup_NhomXN.Text.Contains("XN ")) || (DungChung.Bien.MaBV == "20001" && lup_NhomXN.Text.Contains("XN hóa sinh máu")))
                                {
                                    rep.xrTable6.Visible = true;
                                }
                                else
                                    rep.xrTable13.Visible = false;
                                if (DungChung.Bien.MaBV == "30372")
                                {
                                   rep.SubBand4.Visible = false;
                                    rep.SubBand6.Visible = false;
                                    rep.SubBand8.Visible = false;
                                }
                                else
                                {
                                    rep.SubBand3.Visible = false;
                                    rep.SubBand5.Visible = false;
                                    rep.SubBand7.Visible = false;
                                }
                                if(DungChung.Bien.MaBV=="24012" && RadDK.SelectedIndex==2)
                                {
                                    rep.sub_24012.Visible = true;
                                    rep.SubBand3.Visible = false;
                                    rep.SubBand4.Visible = false;
                                    rep.SubBand6.Visible = false;
                                    rep.SubBand8.Visible = false;
                                    rep.SubBand5.Visible = false;
                                    rep.SubBand7.Visible = false;
                                    rep.GroupHeader1.Visible = false;
                                    rep.GroupHeader2.Visible = true;
                                    rep.sub_24012_2.Visible = true;
                                    rep.sub_24012_3.Visible = true;
                                }
                                else if(DungChung.Bien.MaBV == "24012" && RadDK.SelectedIndex == 1)
                                {
                                    rep.sub_24012.Visible = false;
                                    rep.SubBand3.Visible = false;
                                    rep.SubBand4.Visible = true;
                                    rep.SubBand6.Visible = true;
                                    rep.SubBand8.Visible = true;
                                    rep.SubBand5.Visible = false;
                                    rep.SubBand7.Visible = false;
                                    rep.GroupHeader1.Visible = true;
                                    rep.GroupHeader2.Visible = false;
                                    rep.sub_24012_2.Visible = false;
                                    rep.sub_24012_3.Visible = false;
                                }
                               
                                rep.TenBC.Value = DungChung.Bien.MaBV == "30003"? "SỔ " + lup_NhomXN.Text.ToUpper() : "SỔ TỔNG HỢP " + lup_NhomXN.Text.ToUpper();
                                if(DungChung.Bien.MaBV == "24012"){ rep.TenBC.Value =  "SỔ " + lup_NhomXN.Text.ToUpper();}
                                rep.TuNgayDenNgay.Value = "Từ ngày: " + _ngayTu.ToShortDateString() + " đến ngày: " + _ngayDen.ToShortDateString();
                                // BaoCao.Rep_ThHoaSinhMauSL_Moi rep = new BaoCao.Rep_ThHoaSinhMauSL_Moi();
                                rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                                rep.BindingData();
                                rep.CreateDocument();
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            if ((_tenbc == "XN khác" && RadDK.SelectedIndex == 1 || _tenbc == "XN khác" && RadDK.SelectedIndex == 2) && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"))
                            {
                                BaoCao.Rep_TongHop_XN_01071_XNKhac rep = new BaoCao.Rep_TongHop_XN_01071_XNKhac(_lDichVu, _lbenhNhan_gr);

                                rep.GroupHeader1.Visible = false;
                                rep.TenBC.Value = "Sổ tổng hợp " + lup_NhomXN.Text;
                                rep.TuNgayDenNgay.Value = "Từ ngày: " + _ngayTu.ToShortDateString() + " đến ngày: " + _ngayDen.ToShortDateString();
                                // BaoCao.Rep_ThHoaSinhMauSL_Moi rep = new BaoCao.Rep_ThHoaSinhMauSL_Moi();
                                rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                                rep.BindingData();
                                rep.CreateDocument();
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                            else
                            {
                                BaoCao.Rep_TongHop_XN_01071 rep = new BaoCao.Rep_TongHop_XN_01071(_lDichVu, _lbenhNhan_gr);
                                if ((DungChung.Bien.MaBV == "30005" && lup_NhomXN.Text.Contains("XN ")) || (DungChung.Bien.MaBV == "20001" && lup_NhomXN.Text.Contains("XN hóa sinh máu")))
                                {
                                    rep.GroupHeader1.Visible = true;
                                }
                                else
                                    rep.GroupHeader1.Visible = false;
                                rep.TenBC.Value = "Sổ tổng hợp " + lup_NhomXN.Text;
                                rep.TuNgayDenNgay.Value = "Từ ngày: " + _ngayTu.ToShortDateString() + " đến ngày: " + _ngayDen.ToShortDateString();
                                // BaoCao.Rep_ThHoaSinhMauSL_Moi rep = new BaoCao.Rep_ThHoaSinhMauSL_Moi();
                                rep.DataSource = _BenhNhan.OrderBy(p => p.ngaythang).ThenBy(p => p.TenBNhan);
                                rep.BindingData();
                                rep.CreateDocument();
                                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "sheet1", _filePath, true, this.Name);
                                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                                frm.ShowDialog();
                            }
                        }
                        #endregion
                    }
                }
                #endregion
                
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
            string tenRG;

            public string TenRG
            {
                get { return tenRG; }
                set { tenRG = value; }
            }
        }
        List<c_DichVu> _lDichVu = new List<c_DichVu>();
        List<c_DichVu> _lDichVu_chon = new List<c_DichVu>();
        int load = 0;
        private void lup_NhomXN_EditValueChanged(object sender, EventArgs e)
        {
            var bv = _Data.BenhViens.Where(p => p.MaChuQuan == "12001" && p.MaBV == DungChung.Bien.MaBV).ToList();
            if (lup_NhomXN.Text.Contains("thuật") || (DungChung.Bien.MaBV == "26007" && lup_NhomXN.Text.Contains("XN")) || (bv.Count > 0 && lup_NhomXN.Text.Contains("XN")))
            {
                chk_CDCLS.Enabled = true;
                chk_DV_KD.Enabled = true;
                lupMaMay.Visible = false;
                labelControl10.Visible = false;
                lupMaMay.EditValue = null;
            }
            else
            {
                chk_CDCLS.Enabled = false;
                chk_DV_KD.Enabled = false;
                chk_DV_KD.Checked = false;
                lupMaMay.Visible = true;
                labelControl10.Visible = true;
                lupMaMay.EditValue = "tatca";
            }
            if (load == 1)
            {
                var dvsd3 = (from cls in _Data.CLS
                             join cd in _Data.ChiDinhs.Where(p => p.Status == 1) on cls.IdCLS equals cd.IdCLS
                             select new { cls.MaKP, cd.MaDV }).Distinct().ToList();
                var dvsd = (from cls in dvsd3
                            select new { cls.MaKP, cls.MaDV }).Distinct().ToList();
                var dvu = (from a in _Data.DichVus.Where(p => p.PLoai == 2) select new { a.MaDV, a.TenDV, a.IdTieuNhom, a.TenRG }).ToList();//&& (DungChung.Bien.MaBV == "26007" ? true : p.Status == 1)
                _lDichVu = (from dv in dvu
                            join b in dvsd on dv.MaDV equals b.MaDV

                            select new c_DichVu
                            {
                                MaKP = b.MaKP ?? 0,
                                Check = false,
                                MaDV = dv.MaDV,
                                TenDV = dv.TenDV,
                                IdTieuNhom = dv.IdTieuNhom ?? 0,
                                TenRG = dv.TenRG
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
                                          TenRG = dv.TenRG
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
                                 group dv by new { dv.MaDV, dv.TenDV, dv.Check } into kq
                                 select new c_DichVu { MaDV = kq.Key.MaDV, TenDV = kq.Key.TenDV, Check = false, }).ToList();
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
           
        }

        private void chk_DV_KD_CheckedChanged(object sender, EventArgs e)
        {

        }
        //public static bool CheckNGioHC(DateTime dt)
        //{
        //    DateTime dttu1 = new DateTime(dt.Year, dt.Month, dt.Day, DungChung.Bien.GioTu[0], DungChung.Bien.PhutTu[0], 0);
        //    DateTime dtden1 = new DateTime(dt.Year, dt.Month, dt.Day, DungChung.Bien.GioDen[0], DungChung.Bien.PhutDen[0], 0);
        //    DateTime dttu2 = new DateTime(dt.Year, dt.Month, dt.Day, DungChung.Bien.GioTu[1], DungChung.Bien.PhutTu[1], 0);
        //    DateTime dtden2 = new DateTime(dt.Year, dt.Month, dt.Day, DungChung.Bien.GioDen[1], DungChung.Bien.PhutDen[1], 0);
        //    if (dt >= dttu1 && dt <= dtden1)
        //        return false;
        //    if (dt >= dttu2 && dt <= dtden2)
        //        return false;
        //    return true;
        //}
        //public static bool CheckCuoiTuan(DateTime dt)
        //{
        //    var thu1 = dt.DayOfWeek;
        //    int thu = Convert.ToInt32(thu1);
        //    if(thu == 0)
        //        return true;
        //    if (thu == 6)
        //        return true;
        //    return false;
        //}

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //FormThamSo.frm_UpdateLetet frm = new frm_UpdateLetet();
            //frm.ShowDialog();
        }

        private void RadDK_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cklKP_ItemCheck_1(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
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
                else
                {
                    foreach (var item in _Kphong)
                    {
                        if (item.makp == makp || item.makp == 0)
                        {
                            item.chon = false;
                            // break;
                        }
                    }
                }
            }
            if (load > 0)
                lup_NhomXN_EditValueChanged(sender, e);
        }
    }
}
