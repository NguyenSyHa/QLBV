using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;
using QLBV.CLS;
namespace QLBV.FormThamSo
{
    public partial class Frm_PHCN_Moi : DevExpress.XtraEditors.XtraUserControl
    {
        public Frm_PHCN_Moi()
        {
            suaanhNoiSoi1 = false;
            suaanhNoiSoi2 = false;
            suaanhSA = false;
            InitializeComponent();
        }
        Boolean suaanhNoiSoi1, suaanhNoiSoi2, suaanhSA;
        private void EnabledControl(bool T)
        {
            btnLuu.Enabled = !T;
            //btnKQ.Enabled = !T;
            btnSua.Enabled = T;
            btnXoa.Enabled = T;
            lupNgayTH.Properties.ReadOnly = T;
            LupCanBo.Properties.ReadOnly = T;
            lupMaMay.Properties.ReadOnly = T;
            //mmKetLuan.Properties.ReadOnly = T;
            //mmLoidan.Properties.ReadOnly = T;
            //mmKetQua.Properties.ReadOnly = T;
        }
        private void removeAllImage()
        {

        }
        private void enableControlTMH(bool T)
        {

        }
        List<frm_kqcls._dsBenhNhan> _ldsBenhNhan = new List<frm_kqcls._dsBenhNhan>();
        void TimKiem2()
        {
            string _tenbn = txttimten.Text.ToLower();
            int _timma = 0, outTim = 0;
            if (int.TryParse(_tenbn, out outTim))
                _timma = Convert.ToInt32(_tenbn);
            grcBenhnhan.DataSource = _ldsBenhNhan.Where(p => p.TenBNhan.ToLower().Contains(_tenbn) || p.MaBNhan == (_timma)).ToList();

        }
        bool process = false;
        private void DSBN()
        {
            process = true;
            panelControl1.Enabled = false;
            grcBenhnhan.DataSource = null;
            int _MaKP = 0;
            int _Noitru = -1;
            DateTime _Ngaytu = System.DateTime.Now;
            DateTime _Ngayden = System.DateTime.Now;
            int _Trangthai = 0;
            // int _Tamthu = 0;
            if (LupKhoaphong.EditValue != null)
            {
                _MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
            }
            _Noitru = RAD.SelectedIndex;
            _Ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            _Ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            _Trangthai = cboTrangthai.SelectedIndex;

            //_Tamthu = 1;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303")
            {
                var q1 = (from cls in _Data.CLS.Where(p => p.MaKPth == _MaKP && p.NgayThang >= _Ngaytu && p.NgayThang <= _Ngayden && p.Status == _Trangthai)
                          join bn in _Data.BenhNhans.Where(p => p.NoiTru == _Noitru)
                             on cls.MaBNhan equals bn.MaBNhan
                          join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
                          join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                          where (kp.PLoai != DungChung.Bien.st_PhanLoaiKP.HanhChinh || bn.DTuong != "Dịch vụ" || (cd.SoPhieu != null && cd.SoPhieu != 0))
                          select new { bn.TenBNhan, bn.MaBNhan, bn.Tuoi, bn.DChi, cls.MaKP, bn.DTuong, bn.NNhap, bn.IDDTBN, bn.GTinh, bn.IDPerson, cls.STT, cls.NgayThang, cls.IdCLS }).ToList();
                _ldsBenhNhan = (from a in q1
                                group a by new
                                {
                                    a.MaKP,
                                    a.TenBNhan,
                                    MaBNhan = a.MaBNhan,
                                    Tuoi = a.Tuoi ?? 0,
                                    DChi = a.DChi,
                                    DTuong = a.DTuong,
                                    NNhap = a.NNhap ?? DateTime.Now,
                                    IDDTBN = a.IDDTBN,
                                    GTinh = a.GTinh ?? 0,
                                    IDPerson = a.IDPerson ?? 0,
                                    //NgayThang = a.NgayThang ?? DateTime.Now,
                                    //IdCLS = a.IdCLS,
                                    STT = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30303") ? 0 : (a.STT ?? 0)
                                } into kq
                                select new frm_kqcls._dsBenhNhan
                                {
                                    MaKP = kq.Key.MaKP ?? 0,
                                    TenBNhan = kq.Key.TenBNhan,
                                    MaBNhan = kq.Key.MaBNhan,
                                    Tuoi = kq.Key.Tuoi,
                                    DChi = kq.Key.DChi,
                                    DTuong = kq.Key.DTuong,
                                    NNhap = kq.Key.NNhap,
                                    IDDTBN = kq.Key.IDDTBN,
                                    GTinh = kq.Key.GTinh,
                                    IDPerson = kq.Key.IDPerson,
                                    //NgayThang = kq.Key.NgayThang,
                                    //IdCLS = kq.Key.IdCLS,
                                    STT = kq.Key.STT
                                }).Distinct().OrderBy(p => p.STT).ThenBy(p => p.NgayThang).ToList();
                if (!string.IsNullOrEmpty(txttimten.Text))
                {
                    string _tenbn = txttimten.Text;
                    int _timma = 0, outTim = 0;
                    if (int.TryParse(_tenbn, out outTim))
                        _timma = Convert.ToInt32(_tenbn);
                    grcBenhnhan.DataSource = _ldsBenhNhan.Where(p => p.TenBNhan.ToLower().Contains(_tenbn.ToLower()) || p.MaBNhan == _timma).ToList();//OrderBy(p => p.NgayThang).ThenBy(p => p.STT)
                }
                else
                {

                    grcBenhnhan.DataSource = _ldsBenhNhan.ToList();
                }
            }
            else
            {
                var q1 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == _Noitru)
                          join cls in _Data.CLS.Where(p => p.MaKPth == _MaKP) on bn.MaBNhan equals cls.MaBNhan
                          join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                          join dv in _Data.DichVus.Where(o => o.IS_EXECUTE_CLS == null || o.IS_EXECUTE_CLS == false) on cd.MaDV equals dv.MaDV
                          where (cls.NgayThang >= _Ngaytu && cls.NgayThang <= _Ngayden)
                          where (cls.Status == _Trangthai)
                          select new { bn.TenBNhan, bn.MaBNhan, bn.Tuoi, bn.DChi, bn.MaKP, bn.DTuong, bn.NNhap, bn.IDDTBN, bn.GTinh, bn.IDPerson, cls.STT, cls.NgayThang, cls.IdCLS }).ToList();
                _ldsBenhNhan = (from a in q1
                                group a by new
                                {
                                    a.MaKP,
                                    a.TenBNhan,
                                    MaBNhan = a.MaBNhan,
                                    Tuoi = a.Tuoi ?? 0,
                                    DChi = a.DChi,
                                    DTuong = a.DTuong,
                                    NNhap = a.NNhap ?? DateTime.Now,
                                    IDDTBN = a.IDDTBN,
                                    GTinh = a.GTinh ?? 0,
                                    IDPerson = a.IDPerson ?? 0,
                                    //NgayThang = a.NgayThang ?? DateTime.Now,
                                    //IdCLS = a.IdCLS,
                                    STT = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14018") ? 0 : (a.STT ?? 0)
                                } into kq
                                select new frm_kqcls._dsBenhNhan
                                {
                                    MaKP = kq.Key.MaKP ?? 0,
                                    TenBNhan = kq.Key.TenBNhan,
                                    MaBNhan = kq.Key.MaBNhan,
                                    Tuoi = kq.Key.Tuoi,
                                    DChi = kq.Key.DChi,
                                    DTuong = kq.Key.DTuong,
                                    NNhap = kq.Key.NNhap,
                                    IDDTBN = kq.Key.IDDTBN,
                                    GTinh = kq.Key.GTinh,
                                    IDPerson = kq.Key.IDPerson,
                                    //NgayThang = kq.Key.NgayThang,
                                    //IdCLS = kq.Key.IdCLS,
                                    STT = kq.Key.STT
                                }).Distinct().OrderBy(p => p.STT).ThenBy(p => p.NgayThang).ToList();
                if (DungChung.Bien.MaBV == "24012" && _Trangthai == 2)
                {
                    q1 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == _Noitru)
                          join cls in _Data.CLS.Where(p => p.MaKPth == _MaKP) on bn.MaBNhan equals cls.MaBNhan
                          join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                          join dv in _Data.DichVus.Where(o => o.IS_EXECUTE_CLS == null || o.IS_EXECUTE_CLS == false) on cd.MaDV equals dv.MaDV
                          where (cls.NgayThang >= _Ngaytu && cls.NgayThang <= _Ngayden)
                          select new { bn.TenBNhan, bn.MaBNhan, bn.Tuoi, bn.DChi, bn.MaKP, bn.DTuong, bn.NNhap, bn.IDDTBN, bn.GTinh, bn.IDPerson, cls.STT, cls.NgayThang, cls.IdCLS }).ToList();
                    _ldsBenhNhan = (from a in q1
                                    group a by new
                                    {
                                        a.MaKP,
                                        a.TenBNhan,
                                        MaBNhan = a.MaBNhan,
                                        Tuoi = a.Tuoi ?? 0,
                                        DChi = a.DChi,
                                        DTuong = a.DTuong,
                                        NNhap = a.NNhap ?? DateTime.Now,
                                        IDDTBN = a.IDDTBN,
                                        GTinh = a.GTinh ?? 0,
                                        IDPerson = a.IDPerson ?? 0,
                                        //NgayThang = a.NgayThang ?? DateTime.Now,
                                        //IdCLS = a.IdCLS,
                                        STT = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14018") ? 0 : (a.STT ?? 0)
                                    } into kq
                                    select new frm_kqcls._dsBenhNhan
                                    {
                                        MaKP = kq.Key.MaKP ?? 0,
                                        TenBNhan = kq.Key.TenBNhan,
                                        MaBNhan = kq.Key.MaBNhan,
                                        Tuoi = kq.Key.Tuoi,
                                        DChi = kq.Key.DChi,
                                        DTuong = kq.Key.DTuong,
                                        NNhap = kq.Key.NNhap,
                                        IDDTBN = kq.Key.IDDTBN,
                                        GTinh = kq.Key.GTinh,
                                        IDPerson = kq.Key.IDPerson,
                                        //NgayThang = kq.Key.NgayThang,
                                        //IdCLS = kq.Key.IdCLS,
                                        STT = kq.Key.STT
                                    }).Distinct().OrderBy(p => p.STT).ThenBy(p => p.NgayThang).ToList();
                }
                if (!string.IsNullOrEmpty(txttimten.Text))
                {
                    string _tenbn = txttimten.Text;
                    int _timma = 0, outTim = 0;
                    if (int.TryParse(_tenbn, out outTim))
                        _timma = Convert.ToInt32(_tenbn);
                    grcBenhnhan.DataSource = _ldsBenhNhan.Where(p => p.TenBNhan.ToLower().Contains(_tenbn.ToLower()) || p.MaBNhan == _timma).ToList();//OrderBy(p => p.NgayThang).ThenBy(p => p.STT)
                }
                else
                {

                    grcBenhnhan.DataSource = _ldsBenhNhan.ToList();
                }
            }
            process = false;
        }
        int _mabn = 0;
        int _maKP = 0;
        int trangthaiLuu = 0; //0 là thêm mới . 1 là sửa.
        string _fileanh = "";
        string _fileanh2 = "";
        string Duongdandasua = "";
        string[] arrDuongDan = new string[7];
        string strDD = "";
        public class Status_CD
        {
            string _ten;
            int _status;
            public string Ten
            {
                set { _ten = value; }
                get { return _ten; }
            }
            public int Status
            {
                set { _status = value; }
                get { return _status; }
            }
        }
        List<CLSct> _CLSct = new List<CLSct>();
        List<CL> _Cls = new List<CL>();
        List<ChiDinh> _Chidinh = new List<ChiDinh>();
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<CanBo> _lcbo = new List<CanBo>();
        List<TaiSan> _lTaiSan = new List<TaiSan>();
        List<Status_CD> _lstatus_cd = new List<Status_CD>();
        // lấy mã máy
        void getMaMay(int makp)
        {
            var madv = (from ts in _lTaiSan.Where(p => p.MaKP == makp) select new { ts.MaDV }).ToList();
            var mamay = (from m in madv
                         join dv in _ldvu on m.MaDV equals dv.MaDV
                         select new { dv.MaQD, dv.TenDV }).ToList();
            lupMaMay.Properties.DataSource = null;
            if (mamay.Count > 0)
            {
                lupMaMay.Properties.DataSource = mamay;
                lupMaMay.EditValue = mamay.First().MaQD;
            }
            //if(mamay)

        }
        //
        private void Frm_CDHA_Moi_Load(object sender, EventArgs e)
        {
            EnabledControl(false);
            txtTenBNhan.Properties.ReadOnly = true;
            txtMaBN.Properties.ReadOnly = true;
            // lấy mã máy

            //
            _lstatus_cd.Add(new Status_CD { Ten = "Chưa làm", Status = 0 });
            _lstatus_cd.Add(new Status_CD { Ten = "Đã làm", Status = 1 });
            _lstatus_cd.Add(new Status_CD { Ten = "Hủy", Status = -1 });
            lupStatus.DataSource = _lstatus_cd;
            var _kphong = _Data.KPhongs.ToList();
            lupKPcd.DataSource = _kphong;
            _ldvu = _Data.DichVus.ToList();
            _lTaiSan = _Data.TaiSans.ToList();
            var b = (from kp in _Data.KPhongs.Where(p => p.PLoai.Contains("Cận lâm sàng")).Where(p => p.ChuyenKhoa == "Điều trị y học cổ truyền" || p.ChuyenKhoa == "Điều trị vận động" || p.ChuyenKhoa == "Điều trị vật lý" || p.ChuyenKhoa == "Điều trị ngôn ngữ trị liệu")
                     select kp).ToList();

            if (b.Count > 0)
            {
                if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                {
                    LupKhoaphong.Properties.DataSource = b;
                }
                //LupKhoaphong.Properties.ReadOnly = false;
                else
                {

                    b = (from a in b
                         join c in DungChung.Bien.listKPHoatDong on a.MaKP equals c
                         select a).ToList();
                    LupKhoaphong.Properties.DataSource = b;
                    LupKhoaphong.EditValue = DungChung.Bien.MaKP;
                }
            }


            //if (DungChung.Bien.CapDo == 8 || DungChung.Bien.CapDo == 9)

            lupNgayden.DateTime = System.DateTime.Now;
            lupNgaytu.DateTime = System.DateTime.Now;
            //RAD.SelectedIndex = 2;
            cboTrangthai.SelectedIndex = 0;

            //var CAB = (from cb in _Data.CanBoes.Where(p => p.Status == 1).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ"))
            //           join kp in _Data.KPhongs.Where(p => p.PLoai.Contains("Cận lâm sàng")) on cb.MaKP equals kp.MaKP
            //           select new { cb.TenCB, cb.MaCB, kp.TenKP, kp.ChuyenKhoa, kp.MaKP }).ToList();

            //if (CAB.Count > 0)
            //{
            //    BS themmoi1 = new BS();
            //    themmoi1.TenCB = " ";
            //    themmoi1.MaCB = "";
            //    themmoi1.ChuyenKhoa = "";
            //    themmoi1.MaKP = 0;
            //    _BS.Add(themmoi1);
            //    foreach (var c in CAB)
            //    {
            //        BS themmoi = new BS();
            //        themmoi.MaCB = c.MaCB;
            //        themmoi.TenCB = c.TenCB;
            //        themmoi.ChuyenKhoa = c.ChuyenKhoa;
            //        themmoi.MaKP = c.MaKP;
            //        _BS.Add(themmoi);
            //    }
            //    _BS = _BS.OrderBy(p => p.TenCB).ToList();
            //if (DungChung.Bien.CapDo == 8 || DungChung.Bien.CapDo == 9)
            //if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            //    LupCanBo.Properties.DataSource = _BS.OrderBy(p => p.TenCB).ToList();
            //else
            //    LupCanBo.Properties.DataSource = _BS.Where(p => p.MaKP == DungChung.Bien.MaKP || p.MaKP == 0).OrderBy(p => p.TenCB).ToList();
            //LupCanBo.Properties.DataSource = _BS.ToList();
            //LupCanBo.EditValue = DungChung.Bien.MaCB;
            //}
            lupNgayTH.DateTime = System.DateTime.Now;
        }
        private class BS
        {
            private string tencb;
            private string macb;
            private string chuyenkhoa;
            private int makp;
            public string TenCB
            { set { tencb = value; } get { return tencb; } }
            public string MaCB
            { set { macb = value; } get { return macb; } }
            public string ChuyenKhoa
            { set { chuyenkhoa = value; } get { return chuyenkhoa; } }
            public int MaKP
            { set { makp = value; } get { return makp; } }
        }
        List<BS> _BS = new List<BS>();

        private void LupKhoaphong_EditValueChanged(object sender, EventArgs e)
        {
            if (LupKhoaphong.EditValue != null)
            {
                _maKP = Convert.ToInt32(LupKhoaphong.EditValue);
                ptPhoto.Image = null;
            }
            loadBSTH(_maKP);
            DSBN();
            getMaMay(_maKP);
            var Tap = (from KP in _Data.KPhongs.Where(p => p.MaKP == _maKP) select new { KP.NhomKP, KP.PLoai, KP.TenKP, KP.ChuyenKhoa }).ToList();
            string TenKP = "";
            if (Tap.Count > 0)
                TenKP = Tap.First().ChuyenKhoa;
        }

        public void loadBSTH(int _maKP)
        {
            string _makp = ";" + _maKP.ToString() + ";";
            var c = (from cb in _Data.CanBoes.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp))
                     select new
                     {
                         cb.MaCB,
                         cb.TenCB,
                         cb.MaKPsd
                     }).ToList();
            LupCanBo.Properties.DataSource = c.ToList();
        }
        bool _tamthu = true;
        public class CLSang
        {
            ChiDinh cdinh;

            public ChiDinh Cdinh
            {
                get { return cdinh; }
                set { cdinh = value; }
            }
            CLSct clschitiet;

            public CLSct Clschitiet
            {
                get { return clschitiet; }
                set { clschitiet = value; }
            }
            CL clsang;

            public CL Clsang
            {
                get { return clsang; }
                set { clsang = value; }
            }
            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
        }
        List<CLSang> _lCLSang = new List<CLSang>();
        List<DichVu> _ldvu = new List<DichVu>();
        private void grvBenhnhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (process)
                return;
            panelControl1.Enabled = true;
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            trangthaiLuu = 0;
            if (grvBenhnhan.GetFocusedRowCellValue("MaBNhan") != null)
            {
                string _tenbn = grvBenhnhan.GetFocusedRowCellDisplayText("MaBNhan").ToString();
                _mabn = Convert.ToInt32(grvBenhnhan.GetFocusedRowCellValue("MaBNhan"));
                string dtuong = "";
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303")
                {
                    dtuong = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).First().DTuong;
                }
                if (DungChung.Bien.MaBV == "34019")
                {
                    var ttbx = _Data.TTboXungs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                    if (ttbx != null && !string.IsNullOrEmpty(ttbx.FileAnh))
                    {
                        try
                        {
                            ptPhoto.Image = Image.FromFile(ttbx.FileAnh);

                        }
                        catch
                        {

                        }
                    }
                    else
                        ptPhoto.Image = null;
                }
                var cl = (from cls in _Data.CLS.Where(p => p.MaBNhan == _mabn)
                          join kp in _Data.KPhongs on cls.MaKP equals kp.MaKP
                          join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                          join clsct in _Data.CLScts on cd.IDCD equals clsct.IDCD
                          where (dtuong != "Dịch vụ" || kp.PLoai != DungChung.Bien.st_PhanLoaiKP.HanhChinh || (cd.SoPhieu != null && cd.SoPhieu != 0))
                          select new { cls, cd, clsct }).ToList();
                _lCLSang = (from cls in cl
                            join dv in _ldvu.Where(o => o.IS_EXECUTE_CLS == null || o.IS_EXECUTE_CLS == false) on cls.cd.MaDV equals dv.MaDV
                            select new CLSang { Clsang = cls.cls, Cdinh = cls.cd, TenDV = dv.TenDV, Clschitiet = cls.clsct }).ToList();
                var c = (from cls in _lCLSang.Where(p => p.Clsang.MaKPth == _maKP)
                         group new { cls } by new { cls.TenDV, cls.Cdinh.MaDV, cls.Cdinh.XHH, cls.Clsang.IdCLS, cls.Cdinh.ChiDinh1, cls.Cdinh.KetLuan, cls.Cdinh.IDCD, cls.Clsang.MaCBth, cls.Cdinh.Status, cls.Clsang.NgayThang, cls.Clsang.NgayTH } into kp
                         select new PHCN { Is_ThucHien = kp.Key.Status == 1, YLenh = kp.Key.ChiDinh1, TIDCD = kp.Key.IDCD, tendv = kp.Key.TenDV, madv = kp.Key.MaDV, id = kp.Key.IdCLS, Status = kp.Key.Status, XHH = kp.Key.XHH, NgayThang = kp.Key.NgayThang, NgayTH = kp.Key.NgayTH }).OrderBy(o => o.tendv).ThenBy(o => o.NgayThang).ToList();

                txtTenBNhan.Text = _tenbn;
                txtMaBN.Text = _mabn.ToString();
                groupChiDinh.Text = _mabn + " - " + _tenbn;
                grcKetqua.DataSource = c;
            }
            else
            {
                grcKetqua.DataSource = "";
                groupChiDinh.Text = "";
                txtMaBN.Text = "";
                _mabn = 0;
            }
        }

        public class PHCN
        {
            public bool Is_ThucHien { get; set; }
            public string YLenh { get; set; }
            public int TIDCD { get; set; }
            public string tendv { get; set; }
            public int? madv { get; set; }
            public int id { get; set; }
            public int? Status { get; set; }
            public int? XHH { get; set; }
            public DateTime? NgayThang { get; set; }
            public DateTime? NgayTH { get; set; }
        }

        private void cboTrangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void RAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void lupNgaytu_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void lupNgayden_EditValueChanged(object sender, EventArgs e)
        {
            DSBN();
        }

        private void grvketqua_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var Tap = (from KP in _Data.KPhongs.Where(p => p.MaKP == _maKP) select new { KP.NhomKP, KP.PLoai, KP.TenKP, KP.ChuyenKhoa }).ToList();
            string TenKP = "";
            if (Tap.Count > 0)
                TenKP = Tap.First().ChuyenKhoa;
            if (grvketqua.GetFocusedRowCellValue("madv") != null)
            {

                if (Tap.Count > 0)
                {
                    _tamthu = true;
                    _madv = 0;
                    if (grvketqua.GetFocusedRowCellValue("madv") != null)
                    {
                        _madv = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("madv"));
                    }
                    int _mabn = 0;
                    if (grvBenhnhan.GetFocusedRowCellValue("MaBNhan") != null)
                    {
                        _mabn = Convert.ToInt32(grvBenhnhan.GetFocusedRowCellValue("MaBNhan"));
                    }
                    int IDCD2 = 0;
                    if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                    {
                        IDCD2 = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("TIDCD").ToString());
                    }

                    if (TenKP == "X-Quang CT" || TenKP == "X-Quang KTS")
                        TenKP = "X-Quang";
                    var kq = (from cls in _lCLSang.Where(p => p.Cdinh.IDCD == IDCD2)
                              select new
                              {
                                  cls.Clsang.MaCBth,
                                  cls.Clsang.NgayTH,
                                  cls.Cdinh.MaDV,
                                  cls.Cdinh.KetLuan,
                                  cls.Cdinh.LoiDan,
                                  cls.Cdinh.Status,
                                  cls.Cdinh.SoPhieu,
                                  cls.Clschitiet.KetQua,
                                  cls.Cdinh.XHH,
                                  cls.Cdinh.GhiChu,
                                  cls.Cdinh.MaMay
                              }).ToList();
                    string KetQua = "", LoiDan = "", KetLuan = "", GhiChu = ""; ;

                    if (kq.Count > 0)
                    {

                        int _IDCL = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());

                        int mabn = 0;
                        if (!String.IsNullOrEmpty(txtMaBN.Text))
                            mabn = Convert.ToInt32(txtMaBN.Text);
                        bool Checktamthu = false;
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303")
                        {
                            var dtuong = _Data.BenhNhans.Where(p => p.MaBNhan == mabn && p.DTuong == "Dịch vụ").FirstOrDefault();
                            var qcls = _Data.CLS.Where(p => p.IdCLS == _IDCL).FirstOrDefault();
                            if (dtuong != null && qcls != null)
                            {
                                int makpcd = qcls.MaKP ?? 0;
                                var qkp = _Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh && p.MaKP == makpcd).ToList();
                                if (qkp.Count > 0)
                                    Checktamthu = true;
                            }
                        }
                        if (!Checktamthu)
                            Checktamthu = DungChung.Ham._checkTamThu(_Data, mabn, _IDCL);



                        if (!Checktamthu)
                        {
                            _tamthu = false;
                        }

                        if (kq.First().MaCBth != null && kq.First().MaCBth.ToString() != "")
                        {
                            LupCanBo.EditValue = kq.First().MaCBth;
                        }
                        else
                        {
                            if (kq.First().Status == 1)
                                LupCanBo.EditValue = "";
                            else LupCanBo.EditValue = DungChung.Bien.MaCB;
                        }
                        if (kq.First().NgayTH != null && kq.First().NgayTH.Value.Day > 0)
                            lupNgayTH.DateTime = kq.First().NgayTH.Value;
                        else
                            lupNgayTH.DateTime = System.DateTime.Now;
                        if (kq.First().Status == 1)
                        {
                            KetQua = kq.First().KetQua;
                            LoiDan = kq.First().LoiDan;
                            KetLuan = kq.First().KetLuan;
                            GhiChu = kq.First().GhiChu;
                        }
                        if (kq.First().MaMay != null && kq.First().MaMay != "")
                            lupMaMay.EditValue = kq.First().MaMay;
                    }
                    else
                    {
                        LupCanBo.EditValue = DungChung.Bien.MaCB;
                        lupNgayTH.DateTime = System.DateTime.Now;
                    }
                    string[] arr_kq = new string[10];
                    int IDCD = 0, IdCLS = 0;
                    if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                    {
                        IDCD = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("TIDCD").ToString());
                        IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                    }
                    var clct = (from clsct1 in _lCLSang.Where(p => p.Cdinh.IDCD == IDCD)// tạo list CLSct
                                select new
                                {
                                    clsct1.Clschitiet.KetQua,
                                    clsct1.Clschitiet.MaDVct,
                                    clsct1.Clschitiet.Id,
                                    clsct1.Clschitiet.IDCD,
                                    //clsct1.MaCB,
                                    //clsct1.Ngaythang,
                                    clsct1.Clschitiet.SoPhieu,
                                    clsct1.Clschitiet.STTHT,
                                    clsct1.Clschitiet.ChiDinh,
                                    clsct1.Clschitiet.DuongDan,
                                    clsct1.Clschitiet.DuongDan2,
                                    clsct1.Clschitiet.Status,
                                    clsct1.Clschitiet.KQDuKien,
                                    clsct1.Clschitiet.KQTyLe
                                }).ToList();
                    _CLSct.Clear();
                    if (clct.Count > 0)
                    {

                        foreach (var a in clct)
                        {
                            CLSct themmoi = new CLSct();
                            themmoi.ChiDinh = a.ChiDinh;
                            themmoi.DuongDan = a.DuongDan;
                            themmoi.Id = a.Id;
                            themmoi.IDCD = a.IDCD;
                            themmoi.KetQua = a.KetQua;
                            //themmoi.MaCB = a.MaCB;
                            themmoi.MaDVct = a.MaDVct;
                            //themmoi.ngaythang = a.Ngaythang;
                            themmoi.SoPhieu = a.SoPhieu;
                            themmoi.Status = a.Status;
                            themmoi.DuongDan2 = a.DuongDan2;
                            themmoi.STTHT = a.STTHT;
                            themmoi.KQDuKien = a.KQDuKien;
                            themmoi.KQTyLe = a.KQTyLe;
                            _CLSct.Add(themmoi);
                        }
                    }// end tạo list CLSct
                    var Ylenh = (from chidinh in _lCLSang.Where(p => p.Cdinh.IDCD == IDCD)// tạo list Chidinh
                                 select new { chidinh.Cdinh.ChiDinh1, chidinh.Cdinh.MaMay, chidinh.Cdinh.KetLuan, chidinh.Cdinh.SoPhim, chidinh.Cdinh.XHH, chidinh.Cdinh.Status, chidinh.Cdinh.LoiDan, chidinh.Cdinh.IdCLS, chidinh.Cdinh.MaDV, chidinh.Cdinh.SoPhieu, chidinh.Cdinh.TamThu, chidinh.Cdinh.IDCD, chidinh.Cdinh.TrongBH }).Distinct().ToList();
                    if (Ylenh.Count > 0)
                    {
                        _Chidinh.Clear();
                        foreach (var b in Ylenh)
                        {
                            ChiDinh themmoi = new ChiDinh();
                            themmoi.ChiDinh1 = b.ChiDinh1;
                            themmoi.IdCLS = b.IdCLS;
                            themmoi.IDCD = b.IDCD;
                            themmoi.KetLuan = b.KetLuan;
                            themmoi.LoiDan = b.LoiDan;
                            themmoi.MaDV = b.MaDV;
                            themmoi.SoPhieu = b.SoPhieu;
                            themmoi.Status = b.Status;
                            themmoi.TamThu = b.TamThu;
                            themmoi.TrongBH = b.TrongBH;
                            themmoi.XHH = b.XHH;
                            themmoi.MaMay = b.MaMay;
                            themmoi.SoPhim = b.SoPhim;
                            _Chidinh.Add(themmoi);
                        }
                    }// end tao list chidinh
                    var CanLS = (from C in _lCLSang.Where(p => p.Clsang.IdCLS == IdCLS) select new { C.Clsang.IdCLS, C.Clsang.MaBNhan, C.Clsang.MaCB, C.Clsang.MaCBth, C.Clsang.MaKP, C.Clsang.MaKPth, C.Clsang.NgayThang, }).Distinct().ToList();// tạo list cls
                    if (CanLS.Count > 0)
                    {
                        _Cls.Clear();
                        foreach (var c in CanLS)
                        {
                            CL themmoi = new CL();
                            themmoi.IdCLS = c.IdCLS;
                            themmoi.MaBNhan = c.MaBNhan;
                            themmoi.MaCB = c.MaCB;
                            themmoi.MaCBth = c.MaCBth;
                            themmoi.MaKP = c.MaKP;
                            themmoi.MaKPth = c.MaKPth;
                            themmoi.NgayThang = c.NgayThang;
                            _Cls.Add(themmoi);
                        }
                    }// end tạo list cls
                    if (TenKP == "Điều trị vận động" || TenKP == "Điều trị y học cổ truyền" || TenKP == "Điều trị vật lý" || TenKP == "Điều trị ngôn ngữ trị liệu")
                    {
                        if ((_Chidinh.Count > 0 && _Chidinh.First().Status != null && _Chidinh.First().Status == 1) || _tamthu == false)
                        {
                            EnabledControl(true);
                            if (_tamthu == false)
                                btnSua.Enabled = false;
                            else if (_Chidinh.First().Status == 1)
                                btnSua.Enabled = true;
                        }
                        else
                        {
                            EnabledControl(false);
                        }
                    }
                }
            }
        }
        private bool KT()
        {
            int ot;
            int _int_maBN = 0;

            if (Int32.TryParse(txtMaBN.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);
            //if (lupMaMay.EditValue == null) tạm bỏ
            //{
            //    MessageBox.Show("Bạn chưa chọn mã máy", "Thông báo");
            //    lupMaMay.Focus();
            //    return false;
            //}
            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
            {
                int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                if (lupNgayTH.DateTime != null)
                {
                    var _NgayCD = _Data.CLS.Where(p => p.IdCLS == IdCLS).Select(p => p.NgayThang).FirstOrDefault();
                    DateTime _NgayTH = lupNgayTH.DateTime;
                    if (_NgayCD != null)
                    {
                        if (_NgayTH < _NgayCD)
                        {

                            MessageBox.Show("Ngày Thực hiện không được < ngày chỉ định", "Thông báo", MessageBoxButtons.OK);
                            lupNgayTH.Focus();
                            return false;
                        }
                        else
                        {
                            if (_NgayTH > DateTime.Now)
                            {

                                MessageBox.Show("Ngày Thực hiện không được > ngày hiện tại", "Thông báo", MessageBoxButtons.OK);
                                lupNgayTH.Focus();
                                return false;
                            }
                        }
                    }
                }
            }
            var Tap = (from KP in _Data.KPhongs.Where(p => p.MaKP == _maKP) select new { KP.NhomKP, KP.PLoai, KP.TenKP, KP.ChuyenKhoa }).ToList();
            string TenKP = "";
            if (Tap.Count > 0)
                TenKP = Tap.First().ChuyenKhoa;
            DateTime ngay1 = Convert.ToDateTime("2018-01-01 00:00:00");
            DateTime ngay2 = Convert.ToDateTime("2018-11-03 23:59:59");
            bool ktra = true;
            //if (DungChung.Bien.MaBV == "26007" && DateTime.Now < ngay && TenKP==DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi)
            //{
            //    ktra = true;
            //}
            var vp = (from vpct in _Data.VienPhis.Where(p => p.MaBNhan == _mabn) select new { vpct.idVPhi, vpct.NgayTT }).ToList();

            if (vp.Count > 0)
            {
                if (DungChung.Bien.MaBV == "27023" && vp.First().NgayTT >= ngay1 && vp.First().NgayTT <= ngay2 && TenKP == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)
                {
                    ktra = true;
                }
                else
                {
                    MessageBox.Show("Bệnh nhân đã thanh toán không thể sửa!");
                    return false;
                }
            }
            var ktRaVien = _Data.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
            if (ktRaVien.Count > 0)
            {
                MessageBox.Show("Bệnh nhân đã ra viện, bạn không thể lưu kết quả");
                return false;
            }
            //if (ktra)
            //{
            //    string TT = TabKetQua.SelectedTabPage.Name;
            //    if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
            //    {
            //        int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
            //        int _madv = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("madv").ToString());
            //        switch (TT)
            //        {
            //            case "tabPhucHoiChucNang":

            //        }
            //    }
            //    return false;
            //}
            return true;
        }
        public string layTenFileAnh(string fileAnhTMH, string tenfileanh)
        {
            if (!string.IsNullOrEmpty(fileAnhTMH))
            {
                if (!File.Exists(tenfileanh))
                {
                    File.Copy(fileAnhTMH, tenfileanh);

                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string a = "";
                        string ex = Path.GetExtension(tenfileanh);
                        a = tenfileanh.Replace(ex, i + ex);
                        //if(DungChung.Bien.MaBV== "30012")
                        //    a = tenfileanh.Replace(".bmp", i + ".bmp");
                        //else
                        //    a = tenfileanh.Replace(".jpg", i + ".jpg");
                        if (!File.Exists(a))
                        {
                            File.Copy(fileAnhTMH, a);
                            tenfileanh = a;
                            break;
                        }
                    }
                }
            }
            return tenfileanh;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (_tamthu == false)
            {
                MessageBox.Show("Bệnh nhân chưa nộp tiền dịch vụ, bạn không thể lưu");
            }

            if (_tamthu && KT())
            {
                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                string kl = "";
                bool KtraBNKSK = false;
                foreach (var b in _Chidinh)
                {
                    int ID = b.IDCD;
                    var suacd = _Data.ChiDinhs.Single(p => p.IDCD == ID);
                    suacd.KetLuan = b.KetLuan;
                    kl = b.KetLuan;
                    suacd.LoiDan = b.LoiDan;
                    suacd.GhiChu = b.GhiChu;
                    // suacd.SoPhieu = b.SoPhieu;
                    suacd.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                    suacd.Status = 1;
                    suacd.TamThu = b.TamThu;
                    suacd.NgayTH = lupNgayTH.DateTime;
                    if (lupMaMay.EditValue != null)
                        suacd.MaMay = lupMaMay.EditValue.ToString();
                    _Data.SaveChanges();
                }
                foreach (var c in _CLSct)
                {
                    var suaclsct = _Data.CLScts.Single(p => p.Id == c.Id);                 
                    suaclsct.DuongDan = c.DuongDan;
                    suaclsct.DuongDan2 = c.DuongDan2;
                    suaclsct.KetQua = c.KetQua;
                    suaclsct.KQDuKien = c.KQDuKien;
                    suaclsct.KQTyLe = c.KQTyLe;
                    //suaclsct.MaCB = c.MaCB;
                    //suaclsct.Ngaythang = c.Ngaythang;
                    suaclsct.SoPhieu = c.SoPhieu;
                    if ((!String.IsNullOrEmpty(c.KetQua) && c.KetQua.Length > 0) || !string.IsNullOrEmpty(kl))
                    {
                        suaclsct.Status = 1;
                    }
                    else
                    {
                        suaclsct.Status = c.Status;
                    }
                    suaclsct.STTHT = c.STTHT;
                    _Data.SaveChanges();

                    #region update dienbien 300619
                    if (c.KetQua != "" && c.KetQua != null)
                    {
                        var qcls_db = (from cd in _Data.ChiDinhs.Where(p => p.IDCD == c.IDCD)
                                       join cls in _Data.CLS on cd.IdCLS equals cls.IdCLS
                                       select cls).FirstOrDefault();
                        if (qcls_db != null && qcls_db.IDDienBien != null && qcls_db.IDDienBien.Value > 0)
                        {
                            var qdb = _Data.DienBiens.Where(p => p.ID == qcls_db.IDDienBien).FirstOrDefault();

                            if (qdb != null)
                            {
                                var tendvct = _Data.DichVucts.Single(p => p.MaDVct == c.MaDVct);
                                qdb.DienBien1 += Environment.NewLine + "+ " + tendvct.TenDVct + ": " + c.KetQua;
                                _Data.SaveChanges();
                            }
                        }
                    }
                    #endregion
                    ////dung280516
                    //if (ktraHoanThanhKQCLS(_mabn))// hoàn thành tất cả các kqCLS => set status = 5
                    //{
                    //    BenhNhan sua = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                    //    if (sua != null)
                    //    {
                    //        sua.Status = 5;
                    //    }
                    //    _Data.SaveChanges();
                    //}

                }
                int makp = 0;
                foreach (var a in _Cls)
                {
                    var suacls = _Data.CLS.Single(p => p.IdCLS == a.IdCLS);
                    makp = a.MaKP == null ? 0 : a.MaKP.Value;
                    suacls.MaCBth = LupCanBo.EditValue.ToString();
                    suacls.NgayTH = lupNgayTH.DateTime;
                    suacls.DSCBTH = suacls.MaCBth;

                    var ktstatuscd = _Data.ChiDinhs.Where(p => p.IdCLS == a.IdCLS).Where(p => p.Status == 0 || p.Status == null).ToList();
                    if (ktstatuscd.Count > 0)
                        suacls.Status = 0;
                    else
                    {
                        suacls.Status = 1;
                        BenhNhan sua = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                        if (sua != null)
                        {
                            var b = _Data.BNKBs.Where(p => p.MaBNhan == _mabn).ToList();
                            var vienphi = _Data.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                            if (b.Count > 0 && vienphi.Count <= 0 && sua.Status != 2 && sua.Status != 3)
                            {
                                sua.Status = 5;
                            }
                            if (sua.IDDTBN == 3 && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297"))
                                KtraBNKSK = true;
                        }
                    }
                    _Data.SaveChanges();

                }
                int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                var cdinh = (from cd1 in _Data.ChiDinhs.Where(p => p.IdCLS == IdCLS && p.Status == 1)
                             join dv in _Data.DichVus on cd1.MaDV equals dv.MaDV
                             select new { cd1.MaDV, cd1.SoPhieu, cd1.DonGia, cd1.IDCD, dv.DonVi, cd1.TrongBH, cd1.XHH, cd1.LoaiDV, cd1.IDGoi }).ToList();
                int iddthuoc = 0;
                //string _mabn = grvBenhnhan.GetFocusedRowCellValue("MaBNhan").ToString();
                int _idkb = 0;
                var bnkb = _Data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                if (bnkb.Count > 0)
                    _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                var ktdthuoc = _Data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.PLDV == 2).ToList();
                if (ktdthuoc.Count > 0)
                    iddthuoc = ktdthuoc.First().IDDon;
                List<int> dsIDGOiDV = new List<int>();//lấy danh sách những gói đã được thu thẳng trước đó
                if (KtraBNKSK == true)
                {
                    var _lThuTT = _Data.TamUngs.Where(p => p.IDGoiDV != null && p.PhanLoai == 3 && p.MaBNhan == _mabn).Select(p => p.IDGoiDV ?? 0).ToList();
                    dsIDGOiDV.AddRange(_lThuTT);
                }
                if (iddthuoc > 0)
                {
                    foreach (var cd2 in cdinh)
                    {
                        var kt = (from dt in _Data.DThuoccts.Where(p => p.IDCD == cd2.IDCD) select dt).ToList();
                        if (kt.Count <= 0)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(_Data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, string.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd2.MaDV;
                            moi.IDKB = _idkb;
                            moi.IDDon = iddthuoc;
                            moi.DonVi = cd2.DonVi;
                            moi.TrongBH = _Chidinh.First().TrongBH == null ? 0 : _Chidinh.First().TrongBH.Value;
                            moi.IDCD = cd2.IDCD;
                            moi.DonGia = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                            moi.XHH = cd2.XHH;
                            moi.LoaiDV = cd2.LoaiDV;
                            if (LupCanBo.EditValue != null)
                                moi.MaCB = LupCanBo.EditValue.ToString();
                            else
                                moi.MaCB = "";
                            moi.MaKP = makp;
                            moi.ThanhTien = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                            moi.NgayNhap = lupNgayTH.DateTime;
                            moi.SoLuong = 1;
                            moi.Status = 0;
                            if (KtraBNKSK == true && cd2.IDGoi != null && dsIDGOiDV.Where(p => p == cd2.IDGoi).Count() > 0)
                            {
                                moi.ThanhToan = 1;
                            }
                            else if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = IdCLS;
                            _Data.DThuoccts.Add(moi);
                            _Data.SaveChanges();
                            var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cd2.MaDV).FirstOrDefault();
                            var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                            }
                        }
                        else
                        {
                            foreach (var dt in kt)
                            {
                                dt.NgayNhap = lupNgayTH.DateTime;
                                dt.IDCLS = IdCLS;
                            }
                            _Data.SaveChanges();
                        }
                    }
                }
                else
                {
                    DThuoc dthuoccd = new DThuoc();
                    dthuoccd.NgayKe = lupNgayTH.DateTime; // cần phải lấy theo ngày tháng nhập kết quả CLS
                    dthuoccd.MaBNhan = _mabn;
                    dthuoccd.MaKP = _Cls.First().MaKP;
                    dthuoccd.MaCB = _Cls.First().MaCB;
                    dthuoccd.PLDV = 2;
                    dthuoccd.KieuDon = -1;
                    _Data.DThuocs.Add(dthuoccd);
                    if (_Data.SaveChanges() >= 0)
                    {
                        int maxid = dthuoccd.IDDon;
                        foreach (var cd3 in cdinh)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(_Data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, string.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd3.MaDV;
                            moi.IDDon = maxid;
                            moi.IDKB = _idkb;
                            moi.TrongBH = _Chidinh.First().TrongBH == null ? 0 : _Chidinh.First().TrongBH.Value;
                            if (LupCanBo.EditValue != null)
                                moi.MaCB = LupCanBo.EditValue.ToString();
                            else
                                moi.MaCB = "";
                            moi.NgayNhap = lupNgayTH.DateTime;
                            moi.MaKP = makp;
                            moi.IDCD = cd3.IDCD;
                            moi.DonVi = cd3.DonVi;
                            moi.XHH = cd3.XHH;
                            moi.DonGia = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.ThanhTien = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.SoLuong = 1;
                            moi.Status = 0;
                            moi.LoaiDV = cd3.LoaiDV;
                            if (KtraBNKSK == true && cd3.IDGoi != null && dsIDGOiDV.Where(p => p == cd3.IDGoi).Count() > 0)
                            {
                                moi.ThanhToan = 1;
                            }
                            else if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = IdCLS;
                            _Data.DThuoccts.Add(moi);
                            _Data.SaveChanges();
                            var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cd3.MaDV).FirstOrDefault();
                            var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                            }
                        }
                    }
                }
                if (DungChung.Bien.MaBV == "01071")
                {
                    var tongcp = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                  join dtct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                  select new { dtct }).ToList();
                    double tongcptrbh = 0;
                    tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                    if (tongcptrbh >= 10000000)
                    {
                        MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vướt quá 10.000.000");
                    }
                }
                grvBenhnhan_FocusedRowChanged(null, null);
                EnabledControl(true);
                enableControlTMH(false);
                trangthaiLuu = 0;
                suaanhNoiSoi1 = false;
                suaanhNoiSoi2 = false;
                suaanhSA = false;
                _fileanh = "";
                _fileanh2 = "";
            }
        }
        //dung280516
        private bool ktraHoanThanhKQCLS(int _mabn)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qCLS = data.CLS.Where(p => p.MaBNhan == _mabn).ToList();
            foreach (var a in qCLS)
            {
                var qclsct = (from clsct in data.CLScts join cd in data.ChiDinhs.Where(p => p.IdCLS == a.IdCLS) on clsct.IDCD equals cd.IDCD select clsct).Where(p => p.KetQua != null && p.KetQua != "").ToList();
                if (qclsct.Count == 0)
                    return false;
            }
            return true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangthaiLuu = 1;
            if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null)
            {
                strDD = _CLSct.First().DuongDan2.ToString(); // Lấy đường dẫn ảnh để sửa ảnh.
                arrDuongDan = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
            }
            var vp = (from vpct in _Data.VienPhis.Where(p => p.MaBNhan == _mabn) select new { vpct.idVPhi, vpct.NgayTT }).ToList();

            DateTime ngay1 = Convert.ToDateTime("2018-01-01 00:00:00");
            DateTime ngay2 = Convert.ToDateTime("2018-11-03 23:59:59");
            bool ktra = true;
            var Tap = (from KP in _Data.KPhongs.Where(p => p.MaKP == _maKP) select new { KP.NhomKP, KP.PLoai, KP.TenKP, KP.ChuyenKhoa }).ToList();
            string TenKP = "";
            if (Tap.Count > 0)
                TenKP = Tap.First().ChuyenKhoa;

            if (vp.Count > 0)
            {
                if (DungChung.Bien.MaBV == "27023" && vp.First().NgayTT >= ngay1 && vp.First().NgayTT <= ngay2 && TenKP == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)
                {
                    ktra = true;
                }
                else
                {
                    MessageBox.Show("Bệnh nhân đã thanh toán không thể sửa!");
                    ktra = false;
                }
            }
            if (ktra)
            {
                EnabledControl(false);
            }
        }

        string[] arrThongTinBNKB;

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvBenhnhan.GetFocusedRowCellValue("MaBNhan") != null)
            {
                var vp = (from vpct in _Data.VienPhis.Where(p => p.MaBNhan == _mabn) select new { vpct.idVPhi }).ToList();
                if (vp.Count > 0)
                { MessageBox.Show("Bệnh nhân đã thanh toán không thể xoá!"); }
                else
                {
                    DialogResult dia = MessageBox.Show("Bạn muốn xóa kết quả?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dia == DialogResult.Yes)
                    {
                        int _maCK = 0;
                        var ck = (from nhom in _Data.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                                  join dvu in _Data.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                                  select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).ToList();
                        if (ck.Count > 0)
                            _maCK = ck.First().MaDV;
                        foreach (var b in _Chidinh)
                        {
                            int ID = b.IDCD;
                            var iddt = _Data.DThuoccts.Where(p => p.IDCD == ID && p.MaDV != _maCK).ToList();
                            if (iddt.Count > 0)
                            {
                                foreach (var item in iddt)
                                {
                                    int iddtct = item.IDDonct;
                                    var ktchiphi = _Data.DThuoccts.Where(p => p.AttachIDDonct == iddtct).ToList();
                                    if (ktchiphi.Count > 0)
                                    {
                                        MessageBox.Show("dịch vụ đã có chi phí đính kèm, bạn không thế xóa");
                                        return;
                                    }
                                    var xoa = _Data.DThuoccts.Single(p => p.IDDonct == iddtct);
                                    _Data.DThuoccts.Remove(xoa);
                                    _Data.SaveChanges();
                                }
                            }

                            var suacd = _Data.ChiDinhs.Single(p => p.IDCD == ID);
                            suacd.NgayTH = null;
                            suacd.KetLuan = "";
                            suacd.LoiDan = "";
                            suacd.MoTa = "";
                            suacd.MaMay = "";
                            //suacd.SoPhieu = 0;
                            suacd.Status = 0;
                            //suacd.TamThu = 1;
                            _Data.SaveChanges();
                        }
                        foreach (var c in _CLSct)
                        {
                            var suaclsct = _Data.CLScts.Single(p => p.Id == c.Id);
                            suaclsct.DuongDan = "";
                            suaclsct.DuongDan2 = "";
                            suaclsct.KetQua = "";
                            suaclsct.KQDuKien = "";
                            suaclsct.KQTyLe = "";
                            //suaclsct.MaCB = "";
                            //suaclsct.Ngaythang = null;
                            suaclsct.SoPhieu = 0;
                            suaclsct.Status = 0;
                            //suaclsct.STTHT = 0;
                            _Data.SaveChanges();
                        }
                        foreach (var a in _Cls)
                        {
                            var suacls = _Data.CLS.Single(p => p.IdCLS == a.IdCLS);
                            suacls.MaCBth = "";
                            suacls.Status = 0;
                            suacls.NgayTH = null;
                            _Data.SaveChanges();
                        }
                        FRM_chidinh_Moi._setStatus(_mabn);
                        MessageBox.Show("Xoá thành công!");
                        grvketqua_FocusedRowChanged(null, null);
                        EnabledControl(true);
                        removeAllImage();
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể xóa! Bạn chưa chọn bệnh nhân.");
            }
        }
        int _madv = 0;

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            DSBN();
        }

        private void grvketqua_DataSourceChanged(object sender, EventArgs e)
        {
            // grvketqua_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0));
            grvketqua_FocusedRowChanged(null, null);
        }

        private void grvBenhnhan_DataSourceChanged(object sender, EventArgs e)
        {
            grvBenhnhan_FocusedRowChanged(null, null);
        }

        private void txttimten_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem2();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            int _IDCL = 0;
            if (grvketqua.GetFocusedRowCellValue(id) != null)
            {
                _IDCL = Convert.ToInt32(grvketqua.GetFocusedRowCellValue(id));
            }
            FormThamSo.Frm_HuyCLS frm = new Frm_HuyCLS(_IDCL, true);
            frm.ShowDialog();
        }

        private void txttimten_Leave(object sender, EventArgs e)
        {
            DSBN();
        }

        int _IDCD_changed = 0;
        private void btn_ThayDoiDV_Click(object sender, EventArgs e)
        {

            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null)
                _IDCD_changed = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("TIDCD"));
            if (_IDCD_changed > 0)
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                ChiDinh cd = data.ChiDinhs.Where(p => p.IDCD == _IDCD_changed).FirstOrDefault();
                if (cd.SoPhieu != null && cd.SoPhieu > 0)
                    MessageBox.Show("Dịch vụ đã thu tiền, bạn không thể thay đổi");
                else if (cd != null && cd.Status == 1)
                    MessageBox.Show("Dịch vụ đã được thực hiện, bạn không thể thay đổi");
                else
                {
                    FormThamSo.frm_DsMaDV frm = new FormThamSo.frm_DsMaDV(_IDCD_changed);
                    frm.passMaDV = new FormThamSo.frm_DsMaDV.PassMaDV(GetMaDVchanged);
                    frm.ShowDialog();
                }
            }
            else
                MessageBox.Show("Bạn chưa chọn dịch vụ");

        }

        //update dịch vụ 
        private void GetMaDVchanged(int madv)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            ChiDinh cd = data.ChiDinhs.Where(p => p.IDCD == _IDCD_changed).FirstOrDefault();
            if (cd != null && madv > 0)
            {
                // đổi mã dịch vụ vào chỉ định
                cd.MaDV = madv;

                // xóa clsct
                List<CLSct> lclsct = data.CLScts.Where(p => p.IDCD == _IDCD_changed).ToList();
                foreach (CLSct cl in lclsct)
                {
                    data.CLScts.Remove(cl);
                }
                data.SaveChanges();

                //insert dịch vụ chi tiết vào clsct

                List<DichVuct> ldvu = data.DichVucts.Where(p => p.MaDV == madv).Where(p => p.Status == 1).ToList();
                foreach (DichVuct dvct in ldvu)
                {
                    CLSct ct = new CLSct();
                    ct.MaDVct = dvct.MaDVct;
                    ct.IDCD = _IDCD_changed;
                    ct.Status = 0;
                    ct.STTHT = dvct.STT_R;
                    data.CLScts.Add(ct);
                }
                data.SaveChanges();
                grvBenhnhan_FocusedRowChanged(null, null);
            }
        }

        private void grvBenhnhan_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            grvBenhnhan_FocusedRowChanged(null, null);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (grvBenhnhan.GetFocusedRowCellValue("MaBNhan") != null)
            {
                if (_Chidinh != null && _Chidinh.Count > 0)
                {
                    var IDCD = (int)grvketqua.GetFocusedRowCellValue(TIDCD);
                    InPhieu.InPhieuDieuTri_14018(_Chidinh.First().MaDV ?? 0, _mabn, InPhieu.TypePhieuDieuTri14018.DieuTri, null, IDCD);
                }
            }
            else
                MessageBox.Show("Chưa chọn bệnh nhân");
        }

        private void Res_CheckThucHien_Enable_CheckedChanged(object sender, EventArgs e)
        {
            var row = (PHCN)grvketqua.GetRow(grvketqua.FocusedRowHandle);
            if (row != null)
            {
                if (!row.Is_ThucHien)
                    btnLuu_Click(null, null);
            }
        }

        private void grvketqua_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            var row = (PHCN)grvketqua.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "Is_ThucHien")
                {
                    if (row.Is_ThucHien)
                        e.RepositoryItem = Res_CheckThucHien_Disable;
                    else
                        e.RepositoryItem = Res_CheckThucHien_Enable;
                }
            }
        }

        private void btnExecuteAll_Click(object sender, EventArgs e)
        {
            QLBV.CLS.frm_Execute_Multi_PHCN.PHCNADO ado = new frm_Execute_Multi_PHCN.PHCNADO();
            ado.MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
            ado.FromTime = lupNgaytu.DateTime;
            ado.ToTime = lupNgayden.DateTime;
            ado.NoiTru = RAD.SelectedIndex;
            ado.Is_Execute_Cls = false;
            frm_Execute_Multi_PHCN frm = new frm_Execute_Multi_PHCN(ado);
            frm.ShowDialog();
            DSBN();
        }

    }
}
