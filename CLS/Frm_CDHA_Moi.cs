using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using QLBV.CLS;
using QLBV.MoRong.ManHinhCLS;
using QLBV.FormNhap;
using QLBV.ChucNang;
using iTextSharp.text.pdf.parser;
using System.Threading.Tasks;
using QLBV.Utilities.Commons;
using QLBV.Models.Business.ConectionPACS;
using System.Net.Http;

namespace QLBV.FormThamSo
{
    public partial class Frm_CDHA_Moi : DevExpress.XtraEditors.XtraUserControl
    {
        public Frm_CDHA_Moi()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "27999")
            {
                btnsieuam4D.Text = "Siêu âm thai 5D";
            }

            if (DungChung.Bien.MaBV.Equals("24012"))
            {
                btnImagePacs.Visible = true;
            }
        }

        public Frm_CDHA_Moi(int idcls)
        {
            InitializeComponent();

            //QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var _status = (from _cls in _data.CLS join cd in _data.ChiDinhs.Where(p => p.IdCLS == idcls) on _cls.IdCLS equals cd.IdCLS select new { cd.Status }).ToList();
            grvketqua.SetRowCellValue(grvketqua.FocusedRowHandle, "Status", _status.First().Status.ToString());
            var kqcls = (from cls in _data.CLS.Where(p => p.IdCLS == idcls)
                         join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                         join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                         select new { cls.NgayTH, cls.MaCBth, cd.MaDV, cd.Status, cd.IDCD, clsct.KetQua, cls.MaBNhan, cd.KetLuan, cd.MaMay, clsct.DuongDan, cd.LoiDan, clsct.DuongDan2, clsct.KetQua_Rtf }).ToList();

            if (kqcls.Count > 0)
            {
                if (kqcls.First().DuongDan != null && File.Exists(kqcls.First().DuongDan))
                {
                    _fileanh = kqcls.First().DuongDan;
                    ptSieuam.Image = Image.FromFile(_fileanh);
                }
                else
                    ptSieuam.Image = null;
                if (kqcls.First().DuongDan2 != null && File.Exists(kqcls.First().DuongDan2))
                {
                    _fileanh2 = kqcls.First().DuongDan2;
                    ptSieuam2.Image = Image.FromFile(_fileanh2);
                }
                else
                    ptSieuam2.Image = null;
                if (!string.IsNullOrWhiteSpace(kqcls.First().KetLuan))
                {
                    mmKLSieuam.Text = kqcls.First().KetLuan;
                }
                if (kqcls.First().LoiDan != null)
                {
                    mmLoidanSieuAm.Text = kqcls.First().LoiDan;
                }
                if (kqcls.First().NgayTH != null)
                {
                    lupNgayTH.DateTime = Convert.ToDateTime(kqcls.First().NgayTH);
                }
                if (kqcls.First().MaCBth != null && kqcls.First().MaCBth.ToString() != "")
                {
                    LupCanBo.EditValue = kqcls.First().MaCBth;
                }
                if (kqcls.First().KetQua != null)
                {
                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                    {
                        reKQSieuAm.RtfText = _CLSct.First().KetQua_Rtf;
                    }
                    else
                        reKQSieuAm.Text = _CLSct.First().KetQua;
                }
            }
        }

        private void EnabledControl(bool T)
        {
            btnLuu.Enabled = !T;
            btnKQMau.Enabled = !T;
            //btnKQ.Enabled = !T;
            btnSua.Enabled = T;
            btnXoa.Enabled = T;
            btnChonAnhNS1.Enabled = !T;
            btnChonAnhNS2.Enabled = !T;
            btnChonAnhNS3.Enabled = !T;
            btnChonAnhNS4.Enabled = !T;
            btnSelect1.Enabled = !T;
            btnSelect2.Enabled = !T;
            btnSelect3.Enabled = !T;
            btnSelect4.Enabled = !T;
            btnSelect5.Enabled = !T;
            btnSelect6.Enabled = !T;
            btnSelect7.Enabled = !T;
            btnSelect8.Enabled = !T;
            btnXoa1.Enabled = !T;
            btnXoa2.Enabled = !T;
            btnXoa3.Enabled = !T;
            btnXoa4.Enabled = !T;
            btnXoa5.Enabled = !T;
            btnXoa6.Enabled = !T;
            btnXoa7.Enabled = !T;
            btnXoa8.Enabled = !T;
            chk_BNKHT.Enabled = !T;
            btnXoaAnhNS1.Enabled = !T;
            btnXoaAnhNS2.Enabled = !T;
            btnXoaAnhNS3.Enabled = !T;
            btnXoaAnhNS4.Enabled = !T;
            btnChonAnhs1.Enabled = !T;
            btnChonAnhs2.Enabled = !T;
            btnChoAnhs3.Enabled = !T;
            btnChonAnhs4.Enabled = !T;
            btnChonAnhs5.Enabled = !T;
            btnChonAnhs6.Enabled = !T;
            btnChonAnhs7.Enabled = !T;
            btnChonAnhs8.Enabled = !T;
            btnXoaAnhs1.Enabled = !T;
            btnXoaAnhs2.Enabled = !T;
            btnXoaAnhs3.Enabled = !T;
            btnXoaAnhs4.Enabled = !T;
            btnXoaAnhs5.Enabled = !T;
            btnXoaAnhs6.Enabled = !T;
            btnXoaAnhs7.Enabled = !T;
            btnXoaAnhs8.Enabled = !T;


            sbtChonanhSA1.Enabled = !T;
            sbtChonanhSA2.Enabled = !T;
            sbtXoaanhSA1.Enabled = !T;
            sbtXoaanhSA2.Enabled = !T;
            btnchonanhxq1.Enabled = !T;
            btnchonanhxq2.Enabled = !T;
            bnchonanhxq3.Enabled = !T;
            btnchonanhxq4.Enabled = !T;
            simpleButton8.Enabled = !T;
            simpleButton6.Enabled = !T;
            simpleButton3.Enabled = !T;
            simpleButton1.Enabled = !T;
            lupNgayTH.Properties.ReadOnly = T;
            LupCanBo.Properties.ReadOnly = T;
            lupMaMay.Properties.ReadOnly = T;
            //mmKetLuan.Properties.ReadOnly = T;
            //mmLoidan.Properties.ReadOnly = T;
            //mmKetQua.Properties.ReadOnly = T;
        }
        private void removeAllImage()
        {
            picSieuAm1.Image = null;
            picSieuAm2.Image = null;
            picSieuAm3.Image = null;
            picSieuAm4.Image = null;
            picSieuAm5.Image = null;
            picSieuAm6.Image = null;
            picSieuAm7.Image = null;
            picSieuAm8.Image = null;
            ptSieuam.Image = null;
            ptSieuam2.Image = null;
            ptNoisoi1.Image = null;
            ptNoisoi2.Image = null;
            ptNoisoi3.Image = null;
            ptNoisoi4.Image = null;
            ptTMH1.Image = null;
            ptTMH2.Image = null;
            ptTMH3.Image = null;
            ptTMH4.Image = null;
            ptTMH5.Image = null;
            ptTMH6.Image = null;
            ptTMH7.Image = null;
            ptTMH11.Image = null;
            ptTMH33.Image = null;
            ptTMH55.Image = null;
            ptTMH66.Image = null;
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

        public static DateTime _Ngaytu = System.DateTime.Now;
        public static DateTime _Ngayden = System.DateTime.Now;
        public static DateTime ngaythuchien = System.DateTime.Now;

        private void DSBN()
        {
            process = true;
            panelControl1.Enabled = false;
            grcBenhnhan.DataSource = null;
            int _MaKP = 0;
            int _Noitru = -1;
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
                var q1 = (from cls in _data.CLS.Where(p => p.MaKPth == _MaKP && p.NgayThang >= _Ngaytu && p.NgayThang <= _Ngayden && p.Status == _Trangthai)
                          join bn in _data.BenhNhans.Where(p => p.NoiTru == _Noitru) on cls.MaBNhan equals bn.MaBNhan
                          join kp in _data.KPhongs on cls.MaKP equals kp.MaKP
                          join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                          where (kp.PLoai != DungChung.Bien.st_PhanLoaiKP.HanhChinh || bn.DTuong != "Dịch vụ" || (cd.SoPhieu != null && cd.SoPhieu != 0))
                          select new
                          {
                              bn.TenBNhan,
                              bn.MaBNhan,
                              bn.Tuoi,
                              bn.DChi,
                              cls.MaKP,
                              bn.DTuong,
                              bn.NNhap,
                              bn.IDDTBN,
                              bn.GTinh,
                              bn.IDPerson,
                              cls.STT,
                              cls.NgayThang,
                              cls.IdCLS
                          });//;.ToList();

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
                                    STT = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "30372") ? 0 : (a.STT ?? 0)
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
                                }).OrderBy(p => p.STT).ToList();//.Distinct().OrderBy(p => p.STT).ThenBy(p => p.NgayThang).ToList();

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
                if (_Trangthai == 2)
                {
                    var q12 = (from bn in _data.BenhNhans.Where(p => p.NoiTru == _Noitru)
                               join cls in _data.CLS.Where(p => p.MaKPth == _MaKP) on bn.MaBNhan equals cls.MaBNhan
                               where (cboTrangthai.SelectedIndex == 1 && DungChung.Bien.MaBV == "27023" ? cls.NgayTH >= _Ngaytu && cls.NgayTH <= _Ngayden : cls.NgayThang >= _Ngaytu && cls.NgayThang <= _Ngayden)
                               select new
                               {
                                   bn.TenBNhan,
                                   bn.MaBNhan,
                                   bn.Tuoi,
                                   bn.DChi,
                                   bn.MaKP,
                                   bn.DTuong,
                                   bn.NNhap,
                                   bn.IDDTBN,
                                   bn.GTinh,
                                   bn.IDPerson,
                                   cls.STT,
                                   cls.NgayThang,
                                   cls.IdCLS
                               });
                    _ldsBenhNhan = (from a in q12
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
                                        STT = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30372") ? 0 : (a.STT ?? 0)
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
                                        STT = kq.Key.STT
                                    }).OrderBy(p => p.STT).ToList();
                }
                var q1 = (from bn in _data.BenhNhans.Where(p => p.NoiTru == _Noitru)
                          join cls in _data.CLS.Where(p => p.MaKPth == _MaKP) on bn.MaBNhan equals cls.MaBNhan
                          where (cboTrangthai.SelectedIndex == 1 && DungChung.Bien.MaBV == "27023" ? cls.NgayTH >= _Ngaytu && cls.NgayTH <= _Ngayden : cls.NgayThang >= _Ngaytu && cls.NgayThang <= _Ngayden)
                          where (cls.Status == _Trangthai)
                          select new
                          {
                              bn.TenBNhan,
                              bn.MaBNhan,
                              bn.Tuoi,
                              bn.DChi,
                              bn.MaKP,
                              bn.DTuong,
                              bn.NNhap,
                              bn.IDDTBN,
                              bn.GTinh,
                              bn.IDPerson,
                              cls.STT,
                              cls.NgayThang,
                              cls.IdCLS
                          });//.ToList();
                if (_Trangthai == 1 || _Trangthai == 0)
                {
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
                                        STT = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30372") ? 0 : (a.STT ?? 0)
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
                                    }).OrderBy(p => p.STT).ToList();//.Distinct().OrderBy(p => p.STT).ThenBy(p => p.NgayThang).ToList();
                }
                if (DungChung.Bien.MaBV == "24012" && _Trangthai == 2)
                {
                    q1 = (from bn in _data.BenhNhans.Where(p => p.NoiTru == _Noitru)
                          join cls in _data.CLS.Where(p => p.MaKPth == _MaKP) on bn.MaBNhan equals cls.MaBNhan
                          where (cboTrangthai.SelectedIndex == 1 && DungChung.Bien.MaBV == "27023" ? cls.NgayTH >= _Ngaytu && cls.NgayTH <= _Ngayden : cls.NgayThang >= _Ngaytu && cls.NgayThang <= _Ngayden)
                          select new
                          {
                              bn.TenBNhan,
                              bn.MaBNhan,
                              bn.Tuoi,
                              bn.DChi,
                              bn.MaKP,
                              bn.DTuong,
                              bn.NNhap,
                              bn.IDDTBN,
                              bn.GTinh,
                              bn.IDPerson,
                              cls.STT,
                              cls.NgayThang,
                              cls.IdCLS
                          });//.ToList();

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
                                        STT = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30372") ? 0 : (a.STT ?? 0)
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
                                    }).OrderBy(p => p.STT).ToList();//.Distinct().OrderBy(p => p.STT).ThenBy(p => p.NgayThang).ToList();
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
        string _fileanh3 = "";
        string _fileanh4 = "";
        string _fileanh5 = "";
        string _fileanh6 = "";
        string _fileanh7 = "";
        string _fileanh8 = "";
        string Duongdandasua = "";
        public string[] arrDuongDan = new string[8];
        string strDD = "";
        string[] DuongDan2 = new string[8];
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
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //List<CanBo> _lcbo = new List<CanBo>();
        //List<TaiSan> _lTaiSan = new List<TaiSan>();
        List<Status_CD> _lstatus_cd = new List<Status_CD>();
        // lấy mã máy
        void getMaMay(int makp)
        {
            //var madv = (from ts in _lTaiSan.Where(p => p.MaKP == makp) select new { ts.MaDV }).ToList();
            //var mamay = (from m in madv
            //             join dv in _ldvu on m.MaDV equals dv.MaDV
            //             select new { dv.MaQD, dv.TenDV }).ToList();

            //var madv = (from ts in _data.TaiSans.Where(p => p.MaKP == makp) select new { ts.MaDV }).ToList();
            var mamay = (from m in _data.TaiSans
                         join dv in _data.DichVus on m.MaDV equals dv.MaDV
                         where m.MaKP == makp
                         select new { dv.MaQD, dv.TenDV }).ToList();

            lupMaMay.Properties.DataSource = null;

            if (mamay.Count > 0)
            {
                lupMaMay.Properties.DataSource = mamay;
                lupMaMay.EditValue = mamay.First().MaQD;
            }
        }

        private void Frm_CDHA_Moi_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194")
            {
                simpleButton23.Enabled = false;
                simpleButton24.Enabled = false;
                groupControl54.Text = "Tai trái";
                groupControl56.Text = "Tai phải";
                groupControl57.Text = "Họng";
            }

            if (DungChung.Bien.MaBV == "27194"
                || DungChung.Bien.MaBV == "24012")
            {
                TMH27194.PageVisible = true;
                tabNSTMH.PageVisible = false;
            }
            if (DungChung.Bien.MaBV == "30010")
            {
                panelControl49.Dock = DockStyle.Fill;
                panelControl52.Dock = DockStyle.None;
                panelControl52.Visible = false;
            }
            if (DungChung.Bien.MaBV == "12345"
                || DungChung.Bien.MaBV == "24297")
            {
                btn35tuan.Visible = true;
            }

            if (DungChung.Bien.MaBV == "12345"
                || DungChung.Bien.MaBV == "30372"
                || DungChung.Bien.MaBV == "24297")
            {
                fontBar1.Visible = true;
                paragraphBar1.Visible = true;
                //fontBar2.Visible = true;
                //paragraphBar2.Visible = true;
                //fontBar3.Visible = true;
                //paragraphBar3.Visible = true;
                //fontBar4.Visible = true;
                //paragraphBar4.Visible = true;

                if (mmKetQuaXQ.Document != null)
                    mmKetQuaXQ.Document.DefaultCharacterProperties.FontName = "Time New Roman";
                if (reKQSieuAm.Document != null)
                    reKQSieuAm.Document.DefaultCharacterProperties.FontName = "Time New Roman";
                if (mmKQNoisoi.Document != null)
                    mmKQNoisoi.Document.DefaultCharacterProperties.FontName = "Time New Roman";
                if (mKetqua.Document != null)
                    mKetqua.Document.DefaultCharacterProperties.FontName = "Time New Roman";

                layoutControlItem1.Width += 50;
                panelControl49.Width = panelControl49.Width + (panelControl52.Width - 222);
                groupControl13.Width -= 90;
                groupControl3.Width -= 30;
                groupControl36.Width += 60;
                groupControl12.Height += 25;
                panelControl55.Height -= 60;

                ptxquang2.Location = ptxquang3.Location;
                btnchonanhxq2.Location = bnchonanhxq3.Location;
                simpleButton6.Location = simpleButton3.Location;

                ptxquang3.Visible = false;
                bnchonanhxq3.Visible = false;
                simpleButton3.Visible = false;

                ptxquang4.Visible = false;
                btnchonanhxq4.Visible = false;
                simpleButton1.Visible = false;

                if (DungChung.Bien.MaBV != "30372")
                {
                    colTrongBH.Visible = false;
                }
            }

            if (DungChung.Bien.MaBV == "34019")
            {
                this.Size = new System.Drawing.Size(1140, 600);
                ptPhoto.Visible = true;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303")
            {
                colSTT.Visible = false;
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    this.mmKetQuaXQ.Appearance.Text.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }

            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "14018")
            {
                panelControl53.Visible = false;
                panelControl52.Visible = false;
                this.panelControl49.Dock = System.Windows.Forms.DockStyle.Fill;
            }
            if (DungChung.Bien.MaBV == "14018")
            {
                panelControl57.Visible = false;
                panelControl8.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27022")
            {
                cbo_ChonIn.Properties.Items.Add("Phiếu đo khúc xạ");
            }
            if (DungChung.Bien.MaBV == "30372")
            {
                PageNoisoi.PageVisible = false;
                tabNSTMH.PageVisible = false;
                xTabCDHA30372.PageVisible = true;
                btnChonAnhs1.Enabled = false;
                btnChonAnhs2.Enabled = false;
                btnChoAnhs3.Enabled = false;
                btnChonAnhs4.Enabled = false;
                btnChonAnhs5.Enabled = false;
                btnChonAnhs6.Enabled = false;
                btnChonAnhs7.Enabled = false;
                btnChonAnhs8.Enabled = false;
                btnXoaAnhs1.Enabled = false;
                btnXoaAnhs2.Enabled = false;
                btnXoaAnhs3.Enabled = false;
                btnXoaAnhs4.Enabled = false;
                btnXoaAnhs5.Enabled = false;
                btnXoaAnhs6.Enabled = false;
                btnXoaAnhs7.Enabled = false;
                btnXoaAnhs8.Enabled = false;
                string[] _phieuin = { "Phiếu nội soi đại trực tràng" };
                foreach (string _phieu in _phieuin)
                {
                    cbo_ChonIn.Properties.Items.Add(_phieu.ToString());
                }
            }

            EnabledControl(false);
            txtTenBNhan.Properties.ReadOnly = true;
            txtMaBN.Properties.ReadOnly = true;
            // lấy mã máy

            //
            _lstatus_cd.Add(new Status_CD { Ten = "Chưa làm", Status = 0 });
            _lstatus_cd.Add(new Status_CD { Ten = "Đã làm", Status = 1 });
            _lstatus_cd.Add(new Status_CD { Ten = "Hủy", Status = -1 });
            lupStatus.DataSource = _lstatus_cd;
            if (DungChung.Bien.MaBV == "27023")
            {
                btn_ThayDoiDV.Enabled = true;
            }
            var _kphong = _data.KPhongs.ToList();
            lupKPcd.DataSource = _kphong;
            //_ldvu = _data.DichVus.ToList();
            //_lTaiSan = _data.TaiSans.ToList();
            var b = (from kp in _data.KPhongs.Where(p => p.PLoai.Contains("Cận lâm sàng")).Where(p => p.ChuyenKhoa != "Xét nghiệm" && p.ChuyenKhoa != "Trắc nghiệm tâm lý" && (DungChung.Bien.MaBV == "30299" ? true : (p.ChuyenKhoa != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh)))
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
            if (DungChung.Bien.MaBV == "12001")
            {
                groupControl22.Text = "Hạ họng";
            }

            if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049")
            {
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm ổ bụng 4D (Nam Thăng Long)");
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm thai theo tuổi thai 4D (Nam Thăng Long)");
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm tuyến nước bọt (Nam Thăng Long)");
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm tuyến giáp (Nam Thăng Long)");
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm tuyến vú (Nam Thăng Long)");
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm khớp (Nam Thăng Long)");
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm tinh hoàn 2d (Nam Thăng Long)");
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm doppler mạch gan (Nam Thăng Long)");
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm doppler mạch chủ bụng (Nam Thăng Long)");
                cbo_ChonIn.Properties.Items.Remove("Phiếu siêu âm doppler mạch chi trên(Nam Thăng Long)");

            }
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

            var Tap = (from KP in _data.KPhongs.Where(p => p.MaKP == _maKP) select new { KP.NhomKP, KP.PLoai, KP.TenKP, KP.ChuyenKhoa }).ToList();
            string TenKP = "";
            if (Tap.Count > 0)
                TenKP = Tap.First().ChuyenKhoa;
            if (DungChung.Bien.MaBV == "27023")
            {
                if (TenKP == "Chức năng hô hấp")
                {
                    chk_BNKHT.Visible = true;
                    this.lupMaMay.Size = new System.Drawing.Size(67, 20);
                }
                else
                {
                    btn_ThayDoiDV.Enabled = false;
                    this.lupMaMay.Size = new System.Drawing.Size(160, 20);
                }
            }
            switch (TenKP)
            {
                case "Nội soi Tai-Mũi-Họng":
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        TabKetQua.SelectedTabPage = xTabCDHA30372;
                        xTabCDHA30372.Text = "Nội soi T-M-H";
                    }
                    else if (DungChung.Bien.MaBV == "27194" || DungChung.Bien.MaBV == "24012")
                    {
                        TabKetQua.SelectedTabPage = TMH27194;
                        TMH27194.Text = "Nội soi T-M-H";
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            groupControl54.Text = "Ảnh 1";
                            groupControl55.Text = "Ảnh 2";
                            groupControl56.Text = "Ảnh 3";
                            groupControl57.Text = "Ảnh 4";
                            groupControl51.Text = "Mô Tả";
                            mmeTMH11.Visible = false;
                            mmeTMH33.Visible = false;
                            mmeTMH55.Visible = false;
                            mmeTMH66.Visible = false;
                            this.panelControl35.Size = new System.Drawing.Size(357, 108);
                            this.panelControl72.Size = new System.Drawing.Size(357, 108);
                            this.panelControl75.Size = new System.Drawing.Size(357, 108);
                            this.panelControl78.Size = new System.Drawing.Size(357, 108);
                            this.panelControl35.Location = new System.Drawing.Point(5, 22);
                            this.panelControl72.Location = new System.Drawing.Point(5, 22);
                            this.panelControl75.Location = new System.Drawing.Point(5, 22);
                            this.panelControl78.Location = new System.Drawing.Point(5, 22);
                        }
                    }
                    else
                    {
                        TabKetQua.SelectedTabPage = tabNSTMH;
                    }

                    break;
                case "Răng Hàm Mặt":
                    TabKetQua.SelectedTabPage = tabNSTMH;
                    break;
                case "Siêu âm":
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        TabKetQua.SelectedTabPage = xTabCDHA30372;
                        xTabCDHA30372.Text = "Siêu âm";
                    }
                    else
                    {
                        TabKetQua.SelectedTabPage = PageSieuam;
                    }
                    break;
                case "Siêu âm màu":
                    TabKetQua.SelectedTabPage = PageSieuam;
                    break;
                case "X-Quang":
                    TabKetQua.SelectedTabPage = PageXquang;
                    break;
                case "X-Quang DR":
                    TabKetQua.SelectedTabPage = PageXquang;
                    break;
                case "X-Quang CR":
                    TabKetQua.SelectedTabPage = PageXquang;
                    break;
                case "Điện não đồ":
                    TabKetQua.SelectedTabPage = tabDienNaoDo;
                    break;
                case "Trắc nghiệm tâm lý":
                    TabKetQua.SelectedTabPage = tabDoKhucXaMay;
                    break;
                case "Lưu huyết não":
                    TabKetQua.SelectedTabPage = DungChung.Bien.MaBV == "24297" ? xtraLuuHNao : PageDientim;
                    break;
                case "Điện tim":
                    TabKetQua.SelectedTabPage = PageDientim;
                    break;
                //DungChung.Bien.st_TieuNhomRG_ChuyenKhoa
                case "Nội soi Dạ Dày":
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        TabKetQua.SelectedTabPage = xTabCDHA30372;
                        xTabCDHA30372.Text = "Siêu âm";
                    }
                    else
                    {
                        TabKetQua.SelectedTabPage = PageNoisoi;
                    }

                    break;
                case "Nội soi":
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        TabKetQua.SelectedTabPage = xTabCDHA30372;
                        xTabCDHA30372.Text = "Siêu âm";
                    }
                    else
                    {
                        TabKetQua.SelectedTabPage = PageNoisoi;
                    }

                    break;
                case "Nội soi Cổ Tử Cung":
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        TabKetQua.SelectedTabPage = xTabCDHA30372;
                        xTabCDHA30372.Text = "Siêu âm";
                    }
                    else
                    {
                        TabKetQua.SelectedTabPage = PageNoisoi;
                    }
                    break;
                case "Thủ thuật":
                    TabKetQua.SelectedTabPage = PageNoisoi;
                    break;
                case "Phẫu thuật":
                    TabKetQua.SelectedTabPage = PageNoisoi;
                    break;
                case "Siêu âm ( Doppler )":
                    TabKetQua.SelectedTabPage = PageSieuamDoppler;
                    TabKetQua.SelectedTabPage = PageSieuam;
                    break;
                case "Chức năng hô hấp":
                    TabKetQua.SelectedTabPage = tabDoCNHH;
                    break;
                case "Đo mật độ xương":
                    TabKetQua.SelectedTabPage = tabDoCNHH;
                    break;

            }

            ((Control)this.PageDientim).Enabled = false;
            ((Control)this.PageDientim).Enabled = false;
            ((Control)this.PageXquang).Enabled = false;
            ((Control)this.tabNSTMH).Enabled = false;
            ((Control)this.PageNoisoi).Enabled = false;
            if (TenKP == "Siêu âm ( Doppler )")
            {
                ((Control)this.PageSieuamDoppler).Enabled = true;
                ((Control)this.PageSieuam).Enabled = true;
                kếtQuảChẩnĐoánToolStripMenuItem.Visible = true;
            }
            else
            {
                ((Control)this.PageSieuamDoppler).Enabled = false;
            }
            ((Control)this.tabDienNaoDo).Enabled = false;
            ((Control)this.tabDoKhucXaMay).Enabled = false;
        }

        private void loadBSTH(int _maKP)
        {
            string _makp = ";" + _maKP.ToString() + ";";
            var c = (from cb in _data.CanBoes.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("cn") || p.CapBac.ToLower().Contains("ktv") || p.CapBac.ToLower().Contains("kỹ thuật viên") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                     select new
                     {
                         cb.MaCB,
                         cb.TenCB,
                         cb.MaKPsd
                     }).ToList();
            //if(DungChung.Bien.PLoaiKP=DungChung.Bien.st_PhanLoaiKP.Admin)
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
            private string tenRG;

            public string TenRG
            {
                get { return tenRG; }
                set { tenRG = value; }
            }
            private bool trongBH;
            public bool TrongBH
            {
                get { return trongBH; }
                set { trongBH = value; }
            }
        }

        //List<CLSang> _lCLSang = new List<CLSang>();
        //List<DichVu> _ldvu = new List<DichVu>();

        private void grvBenhnhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadKetQuaCLS();
        }

        private void LoadKetQuaCLS()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here

            arrDuongDan = new string[20];
            if (process)
                return;
            panelControl1.Enabled = true;
            //_Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            trangthaiLuu = 0;
            loadAnh();
            if (grvBenhnhan.GetFocusedRowCellValue("MaBNhan") != null)
            {
                string _tenbn = grvBenhnhan.GetFocusedRowCellDisplayText("MaBNhan").ToString();
                _mabn = Convert.ToInt32(grvBenhnhan.GetFocusedRowCellValue("MaBNhan"));

                string dtuong = "";
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303")
                {
                    dtuong = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).First().DTuong;
                }
                if (DungChung.Bien.MaBV == "34019")
                {
                    var ttbx = _data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().FileAnh ?? null;
                    if (ttbx != null)
                        ptPhoto.Image = Image.FromFile(ttbx);
                    else
                        ptPhoto.Image = null;
                }
                var cl = (from cls in _data.CLS.Where(p => p.MaBNhan == _mabn)
                          join kp in _data.KPhongs on cls.MaKP equals kp.MaKP
                          join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                          join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                          where dtuong != "Dịch vụ" || kp.PLoai != DungChung.Bien.st_PhanLoaiKP.HanhChinh || (cd.SoPhieu != null && cd.SoPhieu != 0)
                          select new
                          {
                              cls,
                              cd,
                              clsct
                          });//.ToList();

                //_lCLSang.Clear();
                //_lCLSang = (from cls in cl
                //            join dv in _data.DichVus on cls.cd.MaDV equals dv.MaDV
                //            select new CLSang
                //            {
                //                Clsang = cls.cls,
                //                Cdinh = cls.cd,
                //                TenDV = dv.TenDV,
                //                Clschitiet = cls.clsct,
                //                TenRG = dv.TenRG,
                //                TrongBH = cls.cd.TrongBH == 1 ? true : false
                //            }).ToList();

                var lCLSangs = (from cls in cl
                                join dv in _data.DichVus on cls.cd.MaDV equals dv.MaDV
                                select new CLSang
                                {
                                    Clsang = cls.cls,
                                    Cdinh = cls.cd,
                                    TenDV = dv.TenDV,
                                    Clschitiet = cls.clsct,
                                    TenRG = dv.TenRG,
                                    TrongBH = cls.cd.TrongBH == 1 ? true : false,
                                });//.ToList();

                var ketquaCls = (from cls in lCLSangs.Where(p => p.Clsang.MaKPth == _maKP)
                                 group new
                                 {
                                     cls
                                 } by new
                                 {
                                     cls.TenDV,
                                     cls.TenRG,
                                     cls.Cdinh.MaDV,
                                     cls.Cdinh.XHH,
                                     cls.Clsang.IdCLS,
                                     cls.Cdinh.ChiDinh1,
                                     cls.Cdinh.KetLuan,
                                     cls.Cdinh.IDCD,
                                     cls.Clsang.MaCBth,
                                     cls.Cdinh.Status,
                                     cls.Clschitiet.DuongDan2,
                                     cls.TrongBH
                                 } into kp
                                 select new
                                 {
                                     YLenh = kp.Key.ChiDinh1,
                                     TIDCD = kp.Key.IDCD,
                                     tendv = DungChung.Bien.MaBV == "24012" ? kp.Key.TenRG : kp.Key.TenDV,
                                     madv = kp.Key.MaDV,
                                     id = kp.Key.IdCLS,
                                     Status = kp.Key.Status,
                                     kp.Key.XHH,
                                     TrongBH = kp.Key.TrongBH
                                 }).OrderBy(p => p.TIDCD).ToList();

                txtTenBNhan.Text = _tenbn;
                txtMaBN.Text = _mabn.ToString();
                groupChiDinh.Text = _mabn + " - " + _tenbn;
                grcKetqua.DataSource = ketquaCls;
            }
            else
            {
                grcKetqua.DataSource = "";
                groupChiDinh.Text = "";
                txtMaBN.Text = "";
                _mabn = 0;
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
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

        private void loadAnh()
        {
            ptTMH1.Image = null;
            ptTMH2.Image = null;
            ptTMH3.Image = null;
            ptTMH4.Image = null;
            ptTMH5.Image = null;
            ptTMH6.Image = null;
            ptTMH7.Image = null;
            ptTMH11.Image = null;
            ptTMH33.Image = null;
            ptTMH55.Image = null;
            ptTMH66.Image = null;
        }

        private void loadAnhNoiSoi()
        {
            ptNoisoi1.Image = null;
            ptNoisoi2.Image = null;
            ptNoisoi3.Image = null;
            ptNoisoi4.Image = null;
        }

        private void loadanhxquang()
        {
            ptxquang1.Image = null;
            ptxquang2.Image = null;
            ptxquang3.Image = null;
            ptxquang4.Image = null;
        }

        private void loadKetQuaTMH(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        mmeTMH1.Text = arr[0];
                        mmeTMH11.Text = arr[0];
                        break;
                    case 1:
                        mmeTMH2.Text = arr[1];
                        if (DungChung.Bien.MaBV == "27194")
                        {
                            mmeTMH55.Text = arr[1];
                        }
                        break;
                    case 2:
                        mmeTMH3.Text = arr[2];
                        mmeTMH33.Text = arr[2];
                        break;
                    case 3:
                        mmeTMH4.Text = arr[3];
                        break;
                    case 4:
                        mmeTMH5.Text = arr[4];
                        //mmeTMH55.Text = arr[4];
                        if (DungChung.Bien.MaBV != "27194")
                        {
                            mmeTMH55.Text = arr[4];
                        }
                        break;
                    case 5:
                        mmeTMH6.Text = arr[5];
                        mmeTMH66.Text = arr[5];
                        break;
                    case 6:
                        mmeTMH7.Text = arr[6];
                        break;
                    case 7:
                        ptTMH1.Image = arrDuongDan[0] == null || arrDuongDan[0] == "" ? null : Image.FromFile(arrDuongDan[0]);
                        break;
                    case 8:
                        ptTMH2.Image = arrDuongDan[1] == null || arrDuongDan[1] == "" ? null : Image.FromFile(arrDuongDan[1]);
                        break;
                    case 9:
                        ptTMH3.Image = arrDuongDan[2] == null || arrDuongDan[2] == "" ? null : Image.FromFile(arrDuongDan[2]);
                        break;
                    case 10:
                        ptTMH4.Image = arrDuongDan[3] == null || arrDuongDan[3] == "" ? null : Image.FromFile(arrDuongDan[3]);
                        break;
                    case 11:
                        ptTMH5.Image = arrDuongDan[4] == null || arrDuongDan[4] == "" ? null : Image.FromFile(arrDuongDan[4]);
                        break;
                    case 12:
                        ptTMH6.Image = arrDuongDan[5] == null || arrDuongDan[5] == "" ? null : Image.FromFile(arrDuongDan[5]);
                        break;
                    case 13:
                        ptTMH7.Image = arrDuongDan[6] == null || arrDuongDan[6] == "" ? null : Image.FromFile(arrDuongDan[6]);
                        break;
                    default:
                        break;
                }
            }
        }

        List<c_LuuHuyetNao> _lLHN = new List<c_LuuHuyetNao>();

        List<c_LuuHuyetNao> _lDoppler = new List<c_LuuHuyetNao>();

        private void grvketqua_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            arrDuongDan = new string[20];
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _fileanh = null;
            if (DungChung.Bien.MaBV == "30372")
            {
                gpAnh(ptAnh1);
                gpAnh(ptAnh2);
                gpAnh(ptAnh3);
                gpAnh(ptAnh4);
                gpAnh(ptAnh5);
                gpAnh(ptAnh6);
                gpAnh(ptAnh7);
                gpAnh(ptAnh8);
                mKetLuan.Text = "";
                mLoiDanBacSy.Text = "";
                mKetqua.Text = "";
            }

            //var Tap = (from KP in _data.KPhongs.Where(p => p.MaKP == _maKP) select new { KP.NhomKP, KP.PLoai, KP.TenKP, KP.ChuyenKhoa }).ToList();
            var kPhong = _data.KPhongs.FirstOrDefault(p => p.MaKP == _maKP);
            string TenKP = "";
            if (kPhong != null)
                TenKP = kPhong.ChuyenKhoa;

            if (grvketqua.GetFocusedRowCellValue("madv") != null)
            {
                if (kPhong != null)
                {
                    _tamthu = true;
                    _madv = 0;
                    int _mabn = 0;
                    int IDCD2 = 0;
                    string dtuong = "";

                    if (grvketqua.GetFocusedRowCellValue("madv") != null)
                        _madv = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("madv"));

                    if (grvBenhnhan.GetFocusedRowCellValue("MaBNhan") != null)
                        _mabn = Convert.ToInt32(grvBenhnhan.GetFocusedRowCellValue("MaBNhan"));

                    if (grvketqua.GetFocusedRowCellValue("TIDCD") != null
                        && grvketqua.GetFocusedRowCellValue("id") != null)
                        IDCD2 = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("TIDCD").ToString());

                    if (TenKP == "X-Quang CT" || TenKP == "X-Quang KTS")
                        TenKP = "X-Quang";

                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303")
                        dtuong = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).First().DTuong;

                    var cl = (from cls in _data.CLS.Where(p => p.MaBNhan == _mabn)
                              join kp in _data.KPhongs on cls.MaKP equals kp.MaKP
                              join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                              join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                              where dtuong != "Dịch vụ" || kp.PLoai != DungChung.Bien.st_PhanLoaiKP.HanhChinh || (cd.SoPhieu != null && cd.SoPhieu != 0)
                              select new
                              {
                                  cls,
                                  cd,
                                  clsct
                              });//.ToList();

                    //_lCLSang.Clear();
                    //_lCLSang = (from cls in cl
                    //            join dv in _data.DichVus on cls.cd.MaDV equals dv.MaDV
                    //            select new CLSang
                    //            {
                    //                Clsang = cls.cls,
                    //                Cdinh = cls.cd,
                    //                TenDV = dv.TenDV,
                    //                Clschitiet = cls.clsct
                    //            }).ToList();

                    //var kq = (from cls in _lCLSang.Where(p => p.Cdinh.IDCD == IDCD2)
                    //          select new
                    //          {
                    //              cls.Clsang.MaCBth,
                    //              cls.Clsang.NgayTH,
                    //              cls.Cdinh.MaDV,
                    //              cls.Cdinh.KetLuan,
                    //              cls.Cdinh.LoiDan,
                    //              cls.Cdinh.Status,
                    //              cls.Cdinh.SoPhieu,
                    //              cls.Clschitiet.KetQua,
                    //              cls.Clschitiet.KetQua_Rtf,
                    //              cls.Cdinh.XHH,
                    //              cls.Cdinh.GhiChu,
                    //              cls.Cdinh.MaMay
                    //          }).ToList();

                    var lCLSangs = (from cls in cl
                                    join dv in _data.DichVus on cls.cd.MaDV equals dv.MaDV
                                    select new CLSang
                                    {
                                        Clsang = cls.cls,
                                        Cdinh = cls.cd,
                                        TenDV = dv.TenDV,
                                        Clschitiet = cls.clsct,

                                    });//.ToList();
                    var kq = (from cls in lCLSangs.Where(p => p.Cdinh.IDCD == IDCD2)
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
                                  cls.Clschitiet.KetQua_Rtf,
                                  cls.Cdinh.XHH,
                                  cls.Cdinh.GhiChu,
                                  cls.Cdinh.MaMay,
                              }).ToList();

                    string KetQua = "", LoiDan = "", KetLuan = "", GhiChu = "", KetQua_Rtf = "";

                    if (kq.Count > 0)
                    {
                        int _IDCL = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());

                        int mabn = 0;
                        if (!String.IsNullOrEmpty(txtMaBN.Text))
                            mabn = Convert.ToInt32(txtMaBN.Text);
                        if (DungChung.Bien.MaBV == "27023")
                        {
                            var bnkht = (from dt in _data.DThuoccts.Where(p => p.IDCD == IDCD2) select new { dt.TrongBH }).ToList();
                            if (bnkht.Count > 0)
                            {
                                if (bnkht.First().TrongBH == 2)
                                {
                                    chk_BNKHT.Checked = true;
                                }
                                else
                                    chk_BNKHT.Checked = false;
                            }
                        }
                        bool Checktamthu = false;
                        var qcls = _data.CLS.FirstOrDefault(p => p.IdCLS == _IDCL);
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303")
                        {
                            var dtuong23 = _data.BenhNhans.FirstOrDefault(p => p.MaBNhan == mabn && p.DTuong == "Dịch vụ");
                            if (dtuong23 != null && qcls != null)
                            {
                                int makpcd = qcls.MaKP ?? 0;
                                var qkp = _data.KPhongs.FirstOrDefault(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh && p.MaKP == makpcd);
                                if (qkp != null)
                                    Checktamthu = true;
                            }
                        }
                        var bnn = _data.BenhNhans.Where(x => x.MaBNhan == mabn).FirstOrDefault();
                        if (DungChung.Bien.MaBV == "30372")
                        {
                            if (bnn.CapCuu == 0)
                            {

                                string erro = "";
                                //var chidinh = _data.ChiDinhs.Where(p => p.IdCLS == _IDCL).FirstOrDefault();
                                var chidinh = _data.ChiDinhs.Where(p => p.IdCLS == _IDCL).ToList();
                                foreach (var th in chidinh)
                                {
                                    if (th.TrongBH == 0 && th.TamThu == null)
                                    {
                                        _tamthu = false;
                                        var dv = _data.DichVus.Where(p => p.MaDV == th.MaDV).Select(p => p.TenDV).FirstOrDefault();
                                        erro += dv + ",";
                                    }
                                }
                                //if (chidinh.TamThu == null)
                                //    _tamthu = false;
                                //if (qcls.CapCuu == true)
                                //    _tamthu = true;
                                if (_tamthu == false)
                                {
                                    string loi = erro.Remove(erro.Length - 1, 1);
                                    MessageBox.Show("Bệnh nhân chưa có tạm thu\n (dịch vụ: " + loi + " )", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            else
                            {
                                _tamthu = true;
                            }
                        }
                        else
                        {
                            if (bnn.DTuong == "Dịch vụ" || bnn.IDDTBN == 2)
                            {
                                if (!Checktamthu)
                                    Checktamthu = DungChung.Ham._checkTamThu(_data, mabn, _IDCL);
                                if (!Checktamthu)
                                {
                                    _tamthu = false;
                                }
                                if (qcls.CapCuu == true)
                                {
                                    _tamthu = true;
                                }
                            }
                            else
                            {
                                _tamthu = true;
                            }
                        }

                        if (kq.First().MaCBth != null && kq.First().MaCBth.ToString() != "")
                        {
                            LupCanBo.EditValue = kq.First().MaCBth;
                        }
                        else
                        {
                            if (kq.First().Status == 1)
                                LupCanBo.EditValue = "";
                            else
                                LupCanBo.EditValue = DungChung.Bien.MaCB;
                        }
                        if (kq.First().NgayTH != null && kq.First().NgayTH.Value.Day > 0)
                            lupNgayTH.DateTime = kq.First().NgayTH.Value;
                        else
                            lupNgayTH.DateTime = System.DateTime.Now;
                        if (kq.First().Status == 1)
                        {
                            KetQua = kq.First().KetQua;
                            KetQua_Rtf = kq.First().KetQua_Rtf;
                            LoiDan = kq.First().LoiDan;
                            KetLuan = kq.First().KetLuan;
                            GhiChu = kq.First().GhiChu;
                        }
                        if (kq.First().MaMay != null && kq.First().MaMay != "")
                            lupMaMay.EditValue = kq.First().MaMay;

                        List<KetquaSieuAmOBung> kqSAOB = KQMauSieuAmOBung();
                    }
                    else
                    {
                        LupCanBo.EditValue = DungChung.Bien.MaCB;
                        lupNgayTH.DateTime = System.DateTime.Now;
                    }
                    string[] arr_kq = new string[20];
                    int IDCD = 0, IdCLS = 0;
                    if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                    {
                        IDCD = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("TIDCD").ToString());
                        IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                    }
                    var clct = (from clsct1 in lCLSangs.Where(p => p.Cdinh.IDCD == IDCD)// tạo list CLSct
                                select new
                                {
                                    clsct1.Clschitiet.KetQua,
                                    clsct1.Clschitiet.KetQua_Rtf,
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
                            themmoi.KetQua_Rtf = a.KetQua_Rtf;
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
                     //var Ylenh = (from chidinh in _lCLSang.Where(p => p.Cdinh.IDCD == IDCD)// tạo list Chidinh
                     //             select new { chidinh.Cdinh.ChiDinh1, chidinh.Cdinh.MaMay, chidinh.Cdinh.KetLuan, chidinh.Cdinh.SoPhim, chidinh.Cdinh.XHH, chidinh.Cdinh.Status, chidinh.Cdinh.LoiDan, chidinh.Cdinh.IdCLS, chidinh.Cdinh.MaDV, chidinh.Cdinh.SoPhieu, chidinh.Cdinh.TamThu, chidinh.Cdinh.IDCD, chidinh.Cdinh.TrongBH }).Distinct().ToList();

                    var Ylenh = (from chidinh in lCLSangs.Where(p => p.Cdinh.IDCD == IDCD)// tạo list Chidinh
                                 select new
                                 {
                                     chidinh.Cdinh.ChiDinh1,
                                     chidinh.Cdinh.MaMay,
                                     chidinh.Cdinh.KetLuan,
                                     chidinh.Cdinh.SoPhim,
                                     chidinh.Cdinh.XHH,
                                     chidinh.Cdinh.Status,
                                     chidinh.Cdinh.LoiDan,
                                     chidinh.Cdinh.IdCLS,
                                     chidinh.Cdinh.MaDV,
                                     chidinh.Cdinh.SoPhieu,
                                     chidinh.Cdinh.TamThu,
                                     chidinh.Cdinh.MaCBth,
                                     chidinh.Cdinh.IDCD,
                                     chidinh.Cdinh.TrongBH
                                 }).Distinct().ToList();

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
                            themmoi.MaCBth = b.MaCBth;
                            themmoi.TamThu = b.TamThu;
                            themmoi.TrongBH = b.TrongBH;
                            themmoi.XHH = b.XHH;
                            themmoi.MaMay = b.MaMay;
                            themmoi.SoPhim = b.SoPhim;
                            _Chidinh.Add(themmoi);
                        }
                    }// end tao list chidinh
                    //var CanLS = (from C in _lCLSang.Where(p => p.Clsang.IdCLS == IdCLS) select new { C.Clsang.IdCLS, C.Clsang.MaBNhan, C.Clsang.MaCB, C.Clsang.MaCBth, C.Clsang.MaKP, C.Clsang.MaKPth, C.Clsang.NgayThang, }).Distinct().ToList();// tạo list cls
                    var CanLS = (from C in lCLSangs.Where(p => p.Clsang.IdCLS == IdCLS)
                                 select new
                                 {
                                     C.Clsang.IdCLS,
                                     C.Clsang.MaBNhan,
                                     C.Clsang.MaCB,
                                     C.Clsang.MaCBth,
                                     C.Clsang.MaKP,
                                     C.Clsang.MaKPth,
                                     C.Clsang.NgayThang,
                                 }).Distinct().ToList();// tạo list cls
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
                    var clsct2 = _data.CLScts.Where(p => p.IDCD == IDCD2).ToList();
                    switch (TenKP)
                    {
                        #region "Nội soi Tai-Mũi-Họng":
                        case "Nội soi Tai-Mũi-Họng":
                            loadAnh();
                            loadKetQuaTMH(QLBV_Library.QLBV_Ham.LayChuoi('|', KetQua));
                            mmKLTMH.Text = KetLuan;
                            memoLoidan_TMH.Text = LoiDan;
                            mmKLTMH1.Text = KetLuan;
                            memoLoidan_TMH1.Text = LoiDan;
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                TabKetQua.SelectedTabPage = xTabCDHA30372;
                                xTabCDHA30372.Text = "Nội soi T-M-H";
                            }
                            else
                            {
                                TabKetQua.SelectedTabPage = tabNSTMH;
                            }

                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.tabNSTMH).Enabled = true;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;

                            try
                            {
                                if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null && _CLSct.First().DuongDan2.Length > 0)
                                {
                                    strDD = _CLSct.First().DuongDan2.ToString(); // Lấy đường dẫn ảnh để sửa ảnh.
                                    arrDuongDan = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                }
                                else
                                {
                                    strDD = "||||||";
                                    arrDuongDan = new string[7];
                                }
                            }
                            catch (Exception)
                            {
                                arrDuongDan = new string[7];
                            }

                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Nội soi Tai-Mũi-Họng")).ToList();
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {
                                if (DungChung.Bien.MaBV == "30372")
                                {
                                    var ketqua = _data.CLScts.Where(p => p.IDCD == IDCD2);
                                    if (!string.IsNullOrEmpty(ketqua.First().KetQua_Rtf))
                                    {
                                        mKetqua.RtfText = ketqua.First().KetQua_Rtf;
                                    }
                                    else
                                        mKetqua.Text = ketqua.First().KetQua;
                                    mLoiDanBacSy.Text = LoiDan;
                                }
                                if (_CLSct.Count > 0 && _data.CLScts.Where(p => p.IDCD == IDCD2).First().KetQua != null && DungChung.Bien.MaBV != "30372")
                                {
                                    string ketqua = _data.CLScts.Where(p => p.IDCD == IDCD2).First().KetQua;
                                    string[] arr = new string[14];
                                    arr = QLBV_Library.QLBV_Ham.LayChuoi('|', ketqua);
                                    //j++;

                                    if (!String.IsNullOrEmpty(arr[0]))
                                    {
                                        mmeTMH1.Text = arr[0];
                                        mmeTMH11.Text = arr[0];
                                    }
                                    if (arr.Count() > 1 && !String.IsNullOrEmpty(arr[1]))
                                    {
                                        mmeTMH2.Text = arr[1];
                                        if (DungChung.Bien.MaBV == "27194")
                                        {
                                            mmeTMH55.Text = arr[1];
                                        }
                                    }
                                    if (arr.Length > 2 && !String.IsNullOrEmpty(arr[2]))
                                    {
                                        mmeTMH3.Text = arr[2];
                                        mmeTMH33.Text = arr[2];
                                    }
                                    if (arr.Length > 3 && !String.IsNullOrEmpty(arr[3]))
                                    {
                                        mmeTMH4.Text = arr[3];
                                    }
                                    if (arr.Length > 4 && !String.IsNullOrEmpty(arr[4]))
                                    {
                                        mmeTMH5.Text = arr[4];
                                        //mmeTMH55.Text = arr[4];
                                        if (DungChung.Bien.MaBV != "27194")
                                        {
                                            mmeTMH55.Text = arr[4];
                                        }
                                    }
                                    if (arr.Length > 5 && !String.IsNullOrEmpty(arr[5]))
                                    {
                                        mmeTMH6.Text = arr[5];
                                        mmeTMH66.Text = arr[5];
                                    }
                                    if (arr.Length > 6 && !String.IsNullOrEmpty(arr[6]))
                                    {
                                        mmeTMH7.Text = arr[6];
                                    }
                                    if (arr.Length > 7 && !string.IsNullOrEmpty(arr[7]))
                                    {
                                        ptTMH1.Image = arrDuongDan[0] == null || arrDuongDan[0] == "" ? null : Image.FromFile(arrDuongDan[0]);
                                    }
                                    if (arr.Length > 8 && !string.IsNullOrEmpty(arr[8]))
                                    {
                                        ptTMH2.Image = arrDuongDan[1] == null || arrDuongDan[1] == "" ? null : Image.FromFile(arrDuongDan[1]);
                                    }
                                    if (arr.Length > 9 && !string.IsNullOrEmpty(arr[9]))
                                    {
                                        ptTMH3.Image = arrDuongDan[2] == null || arrDuongDan[2] == "" ? null : Image.FromFile(arrDuongDan[2]);
                                    }
                                    if (arr.Length > 10 && !string.IsNullOrEmpty(arr[10]))
                                    {
                                        ptTMH4.Image = arrDuongDan[3] == null || arrDuongDan[3] == "" ? null : Image.FromFile(arrDuongDan[3]);
                                    }
                                    if (arr.Length > 11 && !string.IsNullOrEmpty(arr[11]))
                                    {
                                        ptTMH5.Image = arrDuongDan[4] == null || arrDuongDan[4] == "" ? null : Image.FromFile(arrDuongDan[4]);
                                    }
                                    if (arr.Length > 12 && !string.IsNullOrEmpty(arr[12]))
                                    {
                                        ptTMH6.Image = arrDuongDan[5] == null || arrDuongDan[5] == "" ? null : Image.FromFile(arrDuongDan[5]);
                                    }
                                    if (arr.Length > 13 && !string.IsNullOrEmpty(arr[13]))
                                    {
                                        ptTMH7.Image = arrDuongDan[6] == null || arrDuongDan[6] == "" ? null : Image.FromFile(arrDuongDan[6]);
                                    }
                                }
                                else
                                {
                                    mmeTMH1.Text = "";
                                    mmeTMH2.Text = "";
                                    mmeTMH3.Text = "";
                                    mmeTMH11.Text = "";
                                    mmeTMH33.Text = "";
                                    mmeTMH55.Text = "";
                                    mmeTMH66.Text = "";
                                    mmeTMH4.Text = "";
                                    mmeTMH5.Text = "";
                                    mmeTMH6.Text = "";
                                    mmeTMH7.Text = "";
                                    ptTMH1.Image = null;
                                    ptTMH2.Image = null;
                                    ptTMH3.Image = null;
                                    ptTMH4.Image = null;
                                    ptTMH5.Image = null;
                                    ptTMH6.Image = null;
                                    ptTMH7.Image = null;
                                }
                                if (_Chidinh.Count > 0)
                                {
                                    if (_Chidinh.First().KetLuan != null)
                                        mKetLuan.Text = mmKLTMH.Text = _Chidinh.First().KetLuan;
                                    if (_Chidinh.First().LoiDan != null)
                                        mLoiDanBacSy.Text = memoLoidan_TMH.Text = _Chidinh.First().LoiDan;

                                }
                                else
                                {
                                    mmKLTMH.Text = "";
                                    memoLoidan_TMH.Text = "";
                                    mmKLTMH1.Text = "";
                                    memoLoidan_TMH1.Text = "";
                                }

                                if (clsct2.Count > 0 && clsct2.First().DuongDan2 != null)
                                {
                                    String strDD = clsct2.First().DuongDan2;
                                    Duongdandasua = strDD;
                                    string[] arrDD = DuongDan2 = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                    for (int i = 0; i < arrDD.Length; i++)
                                    {
                                        if (DungChung.Bien.MaBV == "30372")
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                    {
                                                        ptAnh1.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh1.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh1.Image = null;
                                                    }
                                                    break;
                                                case 1:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh2.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh2.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh2.Image = null;
                                                    }
                                                    break;
                                                case 2:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh3.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh3.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh3.Image = null;
                                                    }
                                                    break;
                                                case 3:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh4.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh4.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh4.Image = null;
                                                    }
                                                    break;
                                                case 4:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh5.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh5.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh5.Image = null;
                                                    }
                                                    break;
                                                case 5:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh6.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh6.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh6.Image = null;
                                                    }
                                                    break;
                                                case 6:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh7.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh7.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh7.Image = null;
                                                    }
                                                    break;
                                                case 7:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh8.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh8.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh8.Image = null;
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                    {
                                                        ptTMH1.Image = null;
                                                        ptTMH11.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                        {
                                                            ptTMH1.Image = Image.FromFile(arrDD[i]);
                                                            ptTMH11.Image = Image.FromFile(arrDD[i]);
                                                        }
                                                        else
                                                        {
                                                            ptTMH1.Image = null;
                                                            ptTMH11.Image = null;
                                                        }
                                                    }
                                                    break;
                                                case 1:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptTMH2.Image = null;

                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptTMH2.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptTMH2.Image = null;
                                                    }
                                                    break;
                                                case 2:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptTMH3.Image = null;
                                                        ptTMH33.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                        {
                                                            ptTMH3.Image = Image.FromFile(arrDD[i]);
                                                            ptTMH33.Image = Image.FromFile(arrDD[i]);
                                                        }
                                                        else
                                                        {
                                                            ptTMH3.Image = null;
                                                            ptTMH33.Image = null;
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptTMH4.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptTMH4.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptTMH4.Image = null;
                                                    }
                                                    break;
                                                case 4:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptTMH5.Image = null;
                                                        ptTMH55.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                        {
                                                            ptTMH5.Image = Image.FromFile(arrDD[i]);
                                                            ptTMH55.Image = Image.FromFile(arrDD[i]);
                                                        }
                                                        else
                                                        {
                                                            ptTMH5.Image = null;
                                                            ptTMH55.Image = null;
                                                        }
                                                    }
                                                    break;
                                                case 5:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptTMH6.Image = null;
                                                        ptTMH66.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                        {
                                                            ptTMH6.Image = Image.FromFile(arrDD[i]);
                                                            ptTMH66.Image = Image.FromFile(arrDD[i]);
                                                        }
                                                        else
                                                        {
                                                            ptTMH6.Image = null;
                                                            ptTMH66.Image = null;
                                                        }
                                                    }
                                                    break;
                                                case 6:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptTMH7.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptTMH7.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptTMH7.Image = null;
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }

                                    }
                                }

                                if (Ylenh.First().Status == 1 || _tamthu == false)
                                {
                                    mKetqua.ReadOnly = true;
                                    mKetLuan.Properties.ReadOnly = mmKLTMH.Properties.ReadOnly = true;
                                    mLoiDanBacSy.Properties.ReadOnly = memoLoidan_TMH.Properties.ReadOnly = true;
                                    if (DungChung.Bien.MaBV == "27194")
                                    {
                                        mKetLuan.Properties.ReadOnly = mmKLTMH1.Properties.ReadOnly = true;
                                        mLoiDanBacSy.Properties.ReadOnly = memoLoidan_TMH1.Properties.ReadOnly = true;
                                    }
                                    //lupNgayTH.Enabled = false;
                                    //LupCanBo.Enabled = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);

                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (Ylenh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mKetqua.ReadOnly = false;
                                    mKetLuan.Properties.ReadOnly = mmKLTMH.Properties.ReadOnly = false;
                                    mLoiDanBacSy.Properties.ReadOnly = memoLoidan_TMH.Properties.ReadOnly = false;
                                    if (DungChung.Bien.MaBV == "27194")
                                    {
                                        mKetLuan.Properties.ReadOnly = mmKLTMH1.Properties.ReadOnly = false;
                                        mLoiDanBacSy.Properties.ReadOnly = memoLoidan_TMH1.Properties.ReadOnly = false;
                                    }
                                    mmLoidanSieuAm.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);

                                }
                            }
                            break;
                        #endregion
                        #region RĂng hàm mặt
                        case "Răng Hàm Mặt":
                            TabKetQua.SelectedTabPage = tabNSTMH;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.tabNSTMH).Enabled = true;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Răng Hàm Mặt")).ToList();
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {
                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        reKQSieuAm.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        reKQSieuAm.Text = _CLSct.First().KetQua;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    mmKLSieuam.Text = _Chidinh.First().KetLuan;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().LoiDan != null)
                                {
                                    mmLoidanSieuAm.Text = _Chidinh.First().LoiDan;
                                }
                                if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                {
                                    LupCanBo.EditValue = CanLS.First().MaCBth;
                                }
                                if (_CLSct.Count > 0 && _CLSct.First().DuongDan != null && File.Exists(_CLSct.First().DuongDan))
                                    ptSieuam.Image = Image.FromFile(_CLSct.First().DuongDan);
                                else
                                    ptSieuam.Image = null;
                                if (_Chidinh.First().Status == 1 || _tamthu == false)
                                {
                                    reKQSieuAm.ReadOnly = true;
                                    mmKLSieuam.Properties.ReadOnly = true;
                                    mmLoidanSieuAm.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (_Chidinh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    reKQSieuAm.ReadOnly = false;
                                    mmKLSieuam.Properties.ReadOnly = false;
                                    mmLoidanSieuAm.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }
                            }
                            break;
                        #endregion
                        #region siêu âm
                        case "Siêu âm":
                            gpAnh(ptSieuam);
                            gpAnh(ptSieuam2);

                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                reKQSieuAm.RtfText = _CLSct.First().KetQua_Rtf;
                            }
                            else
                                reKQSieuAm.Text = _CLSct.First().KetQua;
                            mmKLSieuam.Text = KetLuan;
                            mmLoidanSieuAm.Text = LoiDan;
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                TabKetQua.SelectedTabPage = xTabCDHA30372;
                                xTabCDHA30372.Text = "Siêu âm";
                            }
                            else
                            {
                                TabKetQua.SelectedTabPage = PageSieuam;
                            }
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = true;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Siêu âm")).ToList();
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {
                                //var ktradv = _ldvu.Where(p => p.MaDV == _madv).Where(p => p.TenDV.ToLower().Contains("siêu âm thai") && p.TenDV.ToLower().Contains("4d")).ToList();
                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    //if (DungChung.Bien.MaBV == "27183" && ktradv.Count > 0)
                                    //{
                                    //    reKQSieuAm.Text = _CLSct.First().KetQua.Replace(";", "\r");
                                    //}
                                    //else
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        mKetqua.RtfText = reKQSieuAm.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        mKetqua.Text = reKQSieuAm.Text = _CLSct.First().KetQua;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    mKetLuan.Text = mmKLSieuam.Text = _Chidinh.First().KetLuan;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().LoiDan != null)
                                {
                                    mLoiDanBacSy.Text = mmLoidanSieuAm.Text = _Chidinh.First().LoiDan;
                                }


                                if (DungChung.Bien.MaBV == "30372")
                                {
                                    if (clsct2.Count > 0 && clsct2.First().DuongDan2 != null || clsct2.First().DuongDan2 != "")
                                    {
                                        string ketqua = clsct2.First().DuongDan2;
                                        string[] arr = new string[10];
                                        arr = DuongDan2 = QLBV_Library.QLBV_Ham.LayChuoi('|', ketqua);
                                        for (int i = 0; i < arr.Length; i++)
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (string.IsNullOrEmpty(arr[i]) && arr[i].Length <= 3)
                                                    {
                                                        ptAnh1.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arr[i]))
                                                            ptAnh1.Image = Image.FromFile(arr[i]);
                                                        else
                                                            ptAnh1.Image = null;
                                                    }
                                                    break;
                                                case 1:
                                                    if (string.IsNullOrEmpty(arr[i]))
                                                    {
                                                        ptAnh2.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arr[i]))
                                                            ptAnh2.Image = Image.FromFile(arr[i]);
                                                        else
                                                            ptAnh2.Image = null;
                                                    }
                                                    break;
                                                case 2:
                                                    if (string.IsNullOrEmpty(arr[i]))
                                                    {
                                                        ptAnh3.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arr[i]))
                                                            ptAnh3.Image = Image.FromFile(arr[i]);
                                                        else
                                                            ptAnh3.Image = null;
                                                    }
                                                    break;
                                                case 3:
                                                    if (string.IsNullOrEmpty(arr[i]))
                                                    {
                                                        ptAnh4.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arr[i]))
                                                            ptAnh4.Image = Image.FromFile(arr[i]);
                                                        else
                                                            ptAnh4.Image = null;
                                                    }
                                                    break;
                                                case 4:
                                                    if (string.IsNullOrEmpty(arr[i]))
                                                    {
                                                        ptAnh5.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arr[i]))
                                                            ptAnh5.Image = Image.FromFile(arr[i]);
                                                        else
                                                            ptAnh5.Image = null;
                                                    }
                                                    break;
                                                case 5:
                                                    if (string.IsNullOrEmpty(arr[i]))
                                                    {
                                                        ptAnh6.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arr[i]))
                                                            ptAnh6.Image = Image.FromFile(arr[i]);
                                                        else
                                                            ptAnh6.Image = null;
                                                    }
                                                    break;
                                                case 6:
                                                    if (string.IsNullOrEmpty(arr[i]))
                                                    {
                                                        ptAnh7.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arr[i]))
                                                            ptAnh7.Image = Image.FromFile(arr[i]);
                                                        else
                                                            ptAnh7.Image = null;
                                                    }
                                                    break;
                                                case 7:
                                                    if (string.IsNullOrEmpty(arr[i]))
                                                    {
                                                        ptAnh8.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arr[i]))
                                                            ptAnh8.Image = Image.FromFile(arr[i]);
                                                        else
                                                            ptAnh8.Image = null;
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    if (_CLSct.Count > 0 && _CLSct.First().DuongDan != null && File.Exists(_CLSct.First().DuongDan))
                                    {
                                        _fileanh = _CLSct.First().DuongDan;
                                        ptSieuam.Image = Image.FromFile(_fileanh);

                                    }
                                    else
                                    {
                                        ptSieuam.Image = null;

                                    }
                                    if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null && File.Exists(_CLSct.First().DuongDan2))
                                    {
                                        _fileanh2 = _CLSct.First().DuongDan2;
                                        ptSieuam2.Image = Image.FromFile(_fileanh2);
                                    }
                                    else
                                    {
                                        ptSieuam2.Image = null;
                                    }
                                }

                                if (Ylenh.Count > 0 && (Ylenh.First().Status == 1 || _tamthu == false))
                                {
                                    mKetqua.ReadOnly = reKQSieuAm.ReadOnly = true;
                                    mKetLuan.ReadOnly = mmKLSieuam.Properties.ReadOnly = true;
                                    mLoiDanBacSy.Properties.ReadOnly = mmLoidanSieuAm.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (DungChung.Bien.MaBV == "27183" && _tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (Ylenh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    //string kqsieuammau = "Số lượng thai:  thai.\rCử động thai: \rNgôi thai:  \rĐK lưỡng đỉnh(BPD):  mm \rChu vi vòng đầu(HC):  mm \rĐK ngang bụng(TTD):  mm \rĐK trước sau bụng(APTD):  mm \rChu vi vòng bụng(AC):  mm \rChiều dài xương đùi(FL):  mm \rChiều dài bàn chân:  mm \rChiều dài xương cánh tay:  mm \rXương sống mũi:  mm \rKhoảng cách hai hố mắt:  mm \rTim thai đều, tần số:  Ck/p \rKích thước não thất bên:  mm \rKích thước tiểu não:  mm \rKích thước hố sau:  mm \rCân nặng ước tính: Gram(Phù hợp với tuổi thai) \rTuổi thai ước tính:  tuần  ngày(+/-10 ngày) \rNgày sinh dự kiến:  /   /201  (+/-07 ngày) \rHàm mặt - Tứ chi: Hiện không thấy bất thường. \rXương hộp sọ: phát triển tốt. \rCột sống - Hệ xương bình thường \rCác não thất không giãn \rTim thai: Cấu trúc 4 buồng, không giãn. \rThành bụng: đóng kín \r- Cơ hoành bình thường. \r- Dạ dày trong ổ bụng \r- Hai thận bình thường \r- Bàng quang trong hổ chậu \r- Lồng ngực: Cấu trúc của phổi đồng nhất. \rVị trí rau bám mặt trước đáy tử cung, mật độ đều \rDịch ối trong, số lượng ối bình thường. \rCửa sổ Doppler hai động mạch, một tĩnh mạch.";
                                    //if (DungChung.Bien.MaBV == "27183" && ktradv.Count > 0)
                                    //{

                                    //    reKQSieuAm.Text = kqsieuammau;
                                    //    mmKLSieuam.Text = "Hình ảnh   thai tương đương  tuần  ngày trong buồng tử cung. Hiện tại phát triển bình thường";
                                    //}
                                    //_ldvu
                                    mKetqua.ReadOnly = reKQSieuAm.ReadOnly = false;
                                    mKetLuan.ReadOnly = mmKLSieuam.Properties.ReadOnly = false;
                                    mLoiDanBacSy.ReadOnly = mmLoidanSieuAm.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }
                            }
                            break;
                        #endregion
                        #region siêu âm màu
                        case "Siêu âm màu":

                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                reKQSieuAm.RtfText = KetQua_Rtf;
                            }
                            else
                                reKQSieuAm.Text = KetQua;
                            mmKLSieuam.Text = KetLuan;
                            mmLoidanSieuAm.Text = LoiDan;
                            TabKetQua.SelectedTabPage = PageSieuam;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = true;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Siêu âm")).ToList();
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {
                                //var ktradv = _ldvu.Where(p => p.MaDV == _madv).Where(p => p.TenDV.ToLower().Contains("siêu âm thai") && p.TenDV.ToLower().Contains("4d")).ToList();
                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    //if (DungChung.Bien.MaBV == "27183" && ktradv.Count > 0)
                                    //{
                                    //    reKQSieuAm.Text = _CLSct.First().KetQua.Replace(";", "\r");
                                    //}
                                    //else
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        reKQSieuAm.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        reKQSieuAm.Text = _CLSct.First().KetQua;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    mmKLSieuam.Text = _Chidinh.First().KetLuan;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().LoiDan != null)
                                {
                                    mmLoidanSieuAm.Text = _Chidinh.First().LoiDan;
                                }
                                //if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                //{
                                //    LupCanBo.EditValue = CanLS.First().MaCBth;
                                //}
                                if (_CLSct.Count > 0 && _CLSct.First().DuongDan != null && File.Exists(_CLSct.First().DuongDan))
                                {
                                    _fileanh = _CLSct.First().DuongDan;
                                    ptSieuam.Image = Image.FromFile(_fileanh);
                                }
                                else
                                    ptSieuam.Image = null;
                                if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null && File.Exists(_CLSct.First().DuongDan2))
                                {
                                    _fileanh2 = _CLSct.First().DuongDan2;
                                    ptSieuam2.Image = Image.FromFile(_fileanh2);
                                }
                                else
                                    ptSieuam2.Image = null;

                                if (_Chidinh.First().Status == 1 || _tamthu == false)
                                {
                                    reKQSieuAm.ReadOnly = true;
                                    mmKLSieuam.Properties.ReadOnly = true;
                                    mmLoidanSieuAm.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (DungChung.Bien.MaBV == "27183" && _tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (_Chidinh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    //string kqsieuammau = "Số lượng thai:  thai.\rCử động thai: \rNgôi thai:  \rĐK lưỡng đỉnh(BPD):  mm \rChu vi vòng đầu(HC):  mm \rĐK ngang bụng(TTD):  mm \rĐK trước sau bụng(APTD):  mm \rChu vi vòng bụng(AC):  mm \rChiều dài xương đùi(FL):  mm \rChiều dài bàn chân:  mm \rChiều dài xương cánh tay:  mm \rXương sống mũi:  mm \rKhoảng cách hai hố mắt:  mm \rTim thai đều, tần số:  Ck/p \rKích thước não thất bên:  mm \rKích thước tiểu não:  mm \rKích thước hố sau:  mm \rCân nặng ước tính: Gram(Phù hợp với tuổi thai) \rTuổi thai ước tính:  tuần  ngày(+/-10 ngày) \rNgày sinh dự kiến:  /   /201  (+/-07 ngày) \rHàm mặt - Tứ chi: Hiện không thấy bất thường. \rXương hộp sọ: phát triển tốt. \rCột sống - Hệ xương bình thường \rCác não thất không giãn \rTim thai: Cấu trúc 4 buồng, không giãn. \rThành bụng: đóng kín \r- Cơ hoành bình thường. \r- Dạ dày trong ổ bụng \r- Hai thận bình thường \r- Bàng quang trong hổ chậu \r- Lồng ngực: Cấu trúc của phổi đồng nhất. \rVị trí rau bám mặt trước đáy tử cung, mật độ đều \rDịch ối trong, số lượng ối bình thường. \rCửa sổ Doppler hai động mạch, một tĩnh mạch.";
                                    //if (DungChung.Bien.MaBV == "27183" && ktradv.Count > 0)
                                    //{

                                    //    reKQSieuAm.Text = kqsieuammau;
                                    //    mmKLSieuam.Text = "Hình ảnh   thai tương đương  tuần  ngày trong buồng tử cung. Hiện tại phát triển bình thường";
                                    //}
                                    //_ldvu
                                    reKQSieuAm.ReadOnly = false;
                                    mmKLSieuam.Properties.ReadOnly = false;
                                    mmLoidanSieuAm.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region                          
                        case "X-Quang":
                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                mmKetQuaXQ.RtfText = KetQua_Rtf;
                            }
                            else
                                mmKetQuaXQ.Text = KetQua;
                            memoKetLuanXQ.Text = KetLuan;
                            mmLoidanXQ.Text = LoiDan;
                            TabKetQua.SelectedTabPage = PageXquang;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = true;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("X-Quang")).ToList();
                            // set cỡ phim
                            DungChung.Bien._lCoPhim = DungChung.Bien.CoPhimXQuang.setCoPhimXQuang();
                            radCoPhim.Properties.Items.Clear();
                            foreach (var item in DungChung.Bien._lCoPhim)
                                radCoPhim.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, item.CoPhim));
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {
                                if (clct.Count > 0)
                                {
                                    if (clct.First().SoPhieu != null)
                                        radCoPhim.SelectedIndex = Convert.ToInt16(clct.First().SoPhieu.Value);
                                    else
                                    {
                                        if (DungChung.Bien.MaBV == "30012")
                                            radCoPhim.SelectedIndex = 2;
                                        else
                                            radCoPhim.SelectedIndex = 0;
                                    }

                                }// end list clsct

                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        mmKetQuaXQ.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        mmKetQuaXQ.Text = _CLSct.First().KetQua;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    memoKetLuanXQ.Text = _Chidinh.First().KetLuan;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().SoPhim != null)
                                {
                                    rgsophim.SelectedIndex = (_Chidinh.First().SoPhim - 1) ?? 0;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().LoiDan != null)
                                {
                                    mmLoidanXQ.Text = _Chidinh.First().LoiDan;
                                }
                                //if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                //{
                                //    LupCanBo.EditValue = CanLS.First().MaCBth;
                                //}
                                if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null)
                                {
                                    String strDD = _CLSct.First().DuongDan2;
                                    Duongdandasua = strDD;
                                    string[] arrDD = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                    for (int i = 0; i < arrDD.Length; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                {
                                                    ptxquang1.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang1.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang1.Image = null;
                                                }
                                                break;
                                            case 1:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptxquang2.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang2.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang2.Image = null;
                                                }
                                                break;
                                            case 2:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptxquang3.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang3.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang3.Image = null;
                                                }
                                                break;
                                            case 3:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptxquang4.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang4.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang4.Image = null;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    loadanhxquang();
                                }

                                if ((_Chidinh.Count > 0 && _Chidinh.First().Status != null && _Chidinh.First().Status == 1) || _tamthu == false)
                                {
                                    mmKetQuaXQ.ReadOnly = true;
                                    mmLoidanXQ.Properties.ReadOnly = true;
                                    memoKetLuanXQ.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (_Chidinh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mmKetQuaXQ.ReadOnly = false;
                                    mmLoidanXQ.Properties.ReadOnly = false;
                                    memoKetLuanXQ.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region xquang dr
                        case "X-Quang DR":
                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                mmKetQuaXQ.RtfText = KetQua_Rtf;
                            }
                            else
                                mmKetQuaXQ.Text = KetQua;
                            memoKetLuanXQ.Text = KetLuan;
                            mmLoidanXQ.Text = LoiDan;
                            TabKetQua.SelectedTabPage = PageXquang;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = true;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("X-Quang")).ToList();
                            // set cỡ phim
                            DungChung.Bien._lCoPhim = DungChung.Bien.CoPhimXQuang.setCoPhimXQuang();
                            radCoPhim.Properties.Items.Clear();
                            foreach (var item in DungChung.Bien._lCoPhim)
                                radCoPhim.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, item.CoPhim));
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {


                                if (clct.Count > 0)
                                {
                                    if (clct.First().SoPhieu != null)
                                        radCoPhim.SelectedIndex = Convert.ToInt16(clct.First().SoPhieu.Value);
                                    else
                                    {
                                        if (DungChung.Bien.MaBV == "30012")
                                            radCoPhim.SelectedIndex = 2;
                                        else
                                            radCoPhim.SelectedIndex = 0;
                                    }


                                }// end list clsct

                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        mmKetQuaXQ.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        mmKetQuaXQ.Text = _CLSct.First().KetQua;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    memoKetLuanXQ.Text = _Chidinh.First().KetLuan;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().SoPhim != null)
                                {
                                    rgsophim.SelectedIndex = (_Chidinh.First().SoPhim - 1) ?? 0;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().LoiDan != null)
                                {
                                    mmLoidanXQ.Text = _Chidinh.First().LoiDan;
                                }
                                //if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                //{
                                //    LupCanBo.EditValue = CanLS.First().MaCBth;
                                //}
                                if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null)
                                {
                                    String strDD = _CLSct.First().DuongDan2;
                                    Duongdandasua = strDD;
                                    string[] arrDD = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                    for (int i = 0; i < arrDD.Length; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                {
                                                    ptxquang1.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang1.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang1.Image = null;
                                                }
                                                break;
                                            case 1:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptxquang2.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang2.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang2.Image = null;
                                                }
                                                break;
                                            case 2:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptxquang3.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang3.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang3.Image = null;
                                                }
                                                break;
                                            case 3:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptxquang4.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang4.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang4.Image = null;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    loadanhxquang();
                                }

                                if ((_Chidinh.Count > 0 && _Chidinh.First().Status != null && _Chidinh.First().Status == 1) || _tamthu == false)
                                {
                                    mmKetQuaXQ.ReadOnly = true;
                                    mmLoidanXQ.Properties.ReadOnly = true;
                                    memoKetLuanXQ.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (_Chidinh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mmKetQuaXQ.ReadOnly = false;
                                    mmLoidanXQ.Properties.ReadOnly = false;
                                    memoKetLuanXQ.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region xquang cr
                        case "X-Quang CR":
                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                mmKetQuaXQ.RtfText = KetQua_Rtf;
                            }
                            else
                                mmKetQuaXQ.Text = KetQua;
                            memoKetLuanXQ.Text = KetLuan;
                            mmLoidanXQ.Text = LoiDan;
                            TabKetQua.SelectedTabPage = PageXquang;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = true;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("X-Quang")).ToList();
                            // set cỡ phim
                            DungChung.Bien._lCoPhim = DungChung.Bien.CoPhimXQuang.setCoPhimXQuang();
                            radCoPhim.Properties.Items.Clear();
                            foreach (var item in DungChung.Bien._lCoPhim)
                                radCoPhim.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(null, item.CoPhim));
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {


                                if (clct.Count > 0)
                                {
                                    if (clct.First().SoPhieu != null)
                                        radCoPhim.SelectedIndex = Convert.ToInt16(clct.First().SoPhieu.Value);
                                    else
                                    {
                                        if (DungChung.Bien.MaBV == "30012")
                                            radCoPhim.SelectedIndex = 2;
                                        else
                                            radCoPhim.SelectedIndex = 0;
                                    }


                                }// end list clsct

                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        mmKetQuaXQ.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        mmKetQuaXQ.Text = _CLSct.First().KetQua;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    memoKetLuanXQ.Text = _Chidinh.First().KetLuan;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().SoPhim != null)
                                {
                                    rgsophim.SelectedIndex = (_Chidinh.First().SoPhim - 1) ?? 0;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().LoiDan != null)
                                {
                                    mmLoidanXQ.Text = _Chidinh.First().LoiDan;
                                }
                                //if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                //{
                                //    LupCanBo.EditValue = CanLS.First().MaCBth;
                                //}
                                if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null)
                                {
                                    String strDD = _CLSct.First().DuongDan2;
                                    Duongdandasua = strDD;
                                    string[] arrDD = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                    for (int i = 0; i < arrDD.Length; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                {
                                                    ptxquang1.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang1.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang1.Image = null;
                                                }
                                                break;
                                            case 1:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptxquang2.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang2.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang2.Image = null;
                                                }
                                                break;
                                            case 2:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptxquang3.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang3.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang3.Image = null;
                                                }
                                                break;
                                            case 3:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptxquang4.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptxquang4.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptxquang4.Image = null;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    loadanhxquang();
                                }

                                if ((_Chidinh.Count > 0 && _Chidinh.First().Status != null && _Chidinh.First().Status == 1) || _tamthu == false)
                                {
                                    mmKetQuaXQ.ReadOnly = true;
                                    mmLoidanXQ.Properties.ReadOnly = true;
                                    memoKetLuanXQ.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (_Chidinh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mmKetQuaXQ.ReadOnly = false;
                                    mmLoidanXQ.Properties.ReadOnly = false;
                                    memoKetLuanXQ.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region điện não đồ
                        case "Điện não đồ":
                            memoKQ_DienNaoDo.Text = KetQua;
                            memoKL_DienNaoDo.Text = KetLuan;
                            memoLoiDan_DienNaoDo.Text = LoiDan;
                            TabKetQua.SelectedTabPage = tabDienNaoDo;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = true;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;

                            if ((_Chidinh.Count > 0 && _Chidinh.First().Status != null && _Chidinh.First().Status == 1) || _tamthu == false)
                            {
                                memoKQ_DienNaoDo.ReadOnly = true;
                                memoKL_DienNaoDo.Properties.ReadOnly = true;
                                memoLoiDan_DienNaoDo.Properties.ReadOnly = true;
                                GrvKQDienTim.OptionsBehavior.Editable = false;
                                GrvDVct.OptionsBehavior.Editable = false;
                                EnabledControl(true);
                                if (_tamthu == false)
                                    btnSua.Enabled = false;
                                else if (_Chidinh.First().Status == 1)
                                    btnSua.Enabled = true;
                            }
                            else
                            {
                                memoKQ_DienNaoDo.ReadOnly = false;
                                memoKL_DienNaoDo.Properties.ReadOnly = false;
                                memoLoiDan_DienNaoDo.Properties.ReadOnly = false;
                                GrvKQDienTim.OptionsBehavior.Editable = true;
                                GrvDVct.OptionsBehavior.Editable = true;
                                EnabledControl(false);
                            }


                            break;
                        #endregion
                        #region Đo khúc xạ máy
                        case "Đo khúc xạ máy":
                            mmKetQua_DoKhucXaMay.Text = KetQua;
                            TabKetQua.SelectedTabPage = tabDoKhucXaMay;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = true;

                            if ((_Chidinh.Count > 0 && _Chidinh.First().Status != null && _Chidinh.First().Status == 1) || _tamthu == false)
                            {
                                mmKetQua_DoKhucXaMay.ReadOnly = true;
                                EnabledControl(true);
                                if (_tamthu == false)
                                    btnSua.Enabled = false;
                                else if (_Chidinh.First().Status == 1)
                                    btnSua.Enabled = true;
                            }
                            else
                            {
                                mmKetQua_DoKhucXaMay.ReadOnly = false;
                                EnabledControl(false);
                            }


                            break;
                        #endregion
                        #region lưu huyết não
                        case "Lưu huyết não":
                            if (DungChung.Bien.MaBV == "24297")
                            {
                                txtKetLuanLHN.Text = KetLuan;
                                txtLoiDanLHN.Text = LoiDan;
                                TabKetQua.SelectedTabPage = xtraLuuHNao;
                                ((Control)this.xtraLuuHNao).Enabled = true;
                                ((Control)this.PageDientim).Enabled = false;
                                ((Control)this.tabNSTMH).Enabled = false;
                                ((Control)this.PageXquang).Enabled = false;
                                ((Control)this.PageSieuam).Enabled = false;
                                ((Control)this.PageNoisoi).Enabled = false;
                                ((Control)this.PageSieuamDoppler).Enabled = false;
                                ((Control)this.tabDoCNHH).Enabled = false;
                                ((Control)this.tabDienNaoDo).Enabled = false;
                                ((Control)this.tabDoKhucXaMay).Enabled = false;
                                var TenDV = (from Dv in _data.DichVucts select new { Dv.MaDVct, Dv.TenDVct }).ToList();
                                LupTenDVDT.DataSource = TenDV;

                                if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                                {
                                    // end tạo list cls
                                    if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                    {
                                        txtKetLuanLHN.Text = _Chidinh.First().KetLuan;
                                    }
                                    if (_Chidinh.Count > 0 && _Chidinh.First().LoiDan != null)
                                    {
                                        txtLoiDanLHN.Text = _Chidinh.First().LoiDan;
                                    }
                                    if ((Ylenh.Count > 0 && Ylenh.First().Status != null && Ylenh.First().Status == 1) || _tamthu == false)
                                    {
                                        txtKetLuanLHN.Properties.ReadOnly = true;
                                        txtLoiDanLHN.Properties.ReadOnly = true;
                                        EnabledControl(true);
                                        if (_tamthu == false)
                                            btnSua.Enabled = false;
                                        else if (Ylenh.First().Status == 1)
                                            btnSua.Enabled = true;
                                    }
                                    else
                                    {
                                        txtKetLuanLHN.Properties.ReadOnly = false;
                                        txtLoiDanLHN.Properties.ReadOnly = false;
                                        EnabledControl(false);
                                    }
                                }
                                string[] a = new string[20];
                                if (_CLSct.First().KetQua != null)
                                {
                                    var lhn = _CLSct.First().KetQua.Split(';');
                                    for (int i = 0; i < lhn.Count(); i++)
                                    {
                                        a[i] = lhn[i];
                                    }
                                }
                                txt01.Text = a[0];
                                txt02.Text = a[1];
                                txt03.Text = a[2];
                                txt04.Text = a[3];
                                txt05.Text = a[4];
                                txt06.Text = a[5];
                                txt07.Text = a[6];
                                txt08.Text = a[7];
                                txt09.Text = a[8];
                                txt10.Text = a[9];
                                txt11.Text = a[10];
                                txt12.Text = a[11];
                                txt13.Text = a[12];
                                txt14.Text = a[13];
                                txt15.Text = a[14];
                                txt16.Text = a[15];
                            }
                            else
                            {
                                mmKetLuanDientim.Text = KetLuan;
                                mmLoidanDientim.Text = LoiDan;
                                TabKetQua.SelectedTabPage = PageDientim;
                                ((Control)this.xtraLuuHNao).Enabled = false;
                                ((Control)this.PageDientim).Enabled = true;
                                ((Control)this.tabNSTMH).Enabled = false;
                                ((Control)this.PageXquang).Enabled = false;
                                ((Control)this.PageSieuam).Enabled = false;
                                ((Control)this.PageNoisoi).Enabled = false;
                                ((Control)this.PageSieuamDoppler).Enabled = false;
                                ((Control)this.tabDoCNHH).Enabled = false;
                                ((Control)this.tabDienNaoDo).Enabled = false;
                                ((Control)this.tabDoKhucXaMay).Enabled = false;
                                rad_DT_LHN.SelectedIndex = 1;
                                DienTim_LHN(1);
                                //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Điện tim")).ToList();
                                var TenDV = (from Dv in _data.DichVucts select new { Dv.MaDVct, Dv.TenDVct }).ToList();
                                LupTenDVDT.DataSource = TenDV;

                                if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                                {
                                    // end tạo list cls
                                    if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                    {
                                        mmKetLuanDientim.Text = _Chidinh.First().KetLuan;
                                    }
                                    if (_Chidinh.Count > 0 && _Chidinh.First().LoiDan != null)
                                    {
                                        mmLoidanDientim.Text = _Chidinh.First().LoiDan;
                                    }
                                    //if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                    //{
                                    //    LupCanBo.EditValue = CanLS.First().MaCBth;
                                    //}
                                    if ((Ylenh.Count > 0 && Ylenh.First().Status != null && Ylenh.First().Status == 1) || _tamthu == false)
                                    {
                                        mmKetLuanDientim.Properties.ReadOnly = true;
                                        mmLoidanDientim.Properties.ReadOnly = true;
                                        GrvKQDienTim.OptionsBehavior.Editable = false;
                                        GrvDVct.OptionsBehavior.Editable = false;
                                        EnabledControl(true);
                                        if (_tamthu == false)
                                            btnSua.Enabled = false;
                                        else if (Ylenh.First().Status == 1)
                                            btnSua.Enabled = true;
                                    }
                                    else
                                    {
                                        mmKetLuanDientim.Properties.ReadOnly = false;
                                        mmLoidanDientim.Properties.ReadOnly = false;
                                        GrvKQDienTim.OptionsBehavior.Editable = true;
                                        GrvDVct.OptionsBehavior.Editable = true;
                                        EnabledControl(false);
                                    }
                                }
                                _lLHN.Clear();

                                for (int i = 0; i < 10; i++)
                                {
                                    arr_kq[i] = "";
                                }
                                foreach (var item in _CLSct)
                                {
                                    string ketqua = "";
                                    if (!string.IsNullOrEmpty(item.KetQua))
                                        ketqua = item.KetQua;
                                    arr_kq = QLBV_Library.QLBV_Ham.LayChuoi(';', ketqua);
                                    _lLHN.Add(new c_LuuHuyetNao
                                    {
                                        ChiDinh = item.ChiDinh,
                                        DuongDan = item.DuongDan,
                                        DuongDan2 = item.DuongDan2,
                                        Id = item.Id,
                                        MaDVct = item.MaDVct,
                                        IDCD = item.IDCD == null ? 0 : item.IDCD.Value,
                                        SoPhieu = 0,
                                        Status = item.Status == null ? 0 : item.Status.Value,
                                        STTHT = item.STTHT == null ? 0 : item.STTHT.Value,
                                        KetQua = arr_kq[0] == null ? "" : arr_kq[0],
                                        KetQua2 = arr_kq[1] == null ? "" : arr_kq[1],
                                        KetQua3 = arr_kq[2] == null ? "" : arr_kq[2],
                                        KetQua4 = arr_kq[3] == null ? "" : arr_kq[3],
                                        KetQua5 = arr_kq[4] == null ? "" : arr_kq[4],
                                        KetQua6 = arr_kq[5] == null ? "" : arr_kq[5],
                                        KetQua7 = arr_kq[6] == null ? "" : arr_kq[6],
                                        KetQua8 = arr_kq[7] == null ? "" : arr_kq[7],
                                        KetQua9 = arr_kq[8] == null ? "" : arr_kq[8],
                                        KetQua10 = arr_kq[9] == null ? "" : arr_kq[9],
                                        KetQua11 = arr_kq[10] == null ? "" : arr_kq[10],
                                        KetQua12 = arr_kq[11] == null ? "" : arr_kq[11],
                                    });
                                }
                                //  GrcKQDienTim.DataSource = _CLSct;
                                GrcKQDienTim.DataSource = null;

                                if (DungChung.Bien.MaBV == "30009")
                                {
                                    List<string> _string = new List<string>();

                                    _string.Add("kết quả");
                                    GrcKQDienTim.DataSource = _string;

                                }
                                else
                                    GrcKQDienTim.DataSource = _lLHN;
                            }
                            break;
                        #endregion
                        #region điện tim
                        case "Điện tim":
                            mmKetLuanDientim.Text = KetLuan;
                            mmLoidanDientim.Text = LoiDan;
                            TabKetQua.SelectedTabPage = PageDientim;
                            ((Control)this.PageDientim).Enabled = true;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            rad_DT_LHN.SelectedIndex = 0;
                            DienTim_LHN(0);
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Điện tim")).ToList();
                            var TenDV2 = (from Dv in _data.DichVucts select new { Dv.MaDVct, Dv.TenDVct, Dv.KetQua }).ToList();
                            LupTenDVDT.DataSource = TenDV2;

                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {

                                // end tạo list cls
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    mmKetLuanDientim.Text = _Chidinh.First().KetLuan;
                                }
                                if (_Chidinh.Count > 0 && _Chidinh.First().LoiDan != null)
                                {
                                    mmLoidanDientim.Text = _Chidinh.First().LoiDan;
                                }
                                //if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                //{
                                //    LupCanBo.EditValue = CanLS.First().MaCBth;
                                //}
                                if ((Ylenh.Count > 0 && Ylenh.First().Status != null && Ylenh.First().Status == 1) || _tamthu == false)
                                {
                                    mmKetLuanDientim.Properties.ReadOnly = true;
                                    mmLoidanDientim.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (Ylenh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mmKetLuanDientim.Properties.ReadOnly = false;
                                    mmLoidanDientim.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }
                            }
                            _lLHN.Clear();
                            for (int i = 0; i < 10; i++)
                            {
                                arr_kq[i] = "";
                            }

                            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                            {
                                if (Convert.ToInt32(grvketqua.GetFocusedRowCellValue("Status")) == 0)
                                {
                                    int _madv11 = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("madv"));
                                    var _listketquadvct = (from dvct in _data.DichVucts.Where(p => p.MaDV == _madv11) select new { dvct.MaDVct, dvct.TenDVct, dvct.KetQua }).ToList();

                                    if (_listketquadvct.Count > 1)
                                    {
                                        foreach (var item in _CLSct)
                                        {
                                            string ketqua = "";


                                            if (!string.IsNullOrEmpty(item.KetQua))
                                                ketqua = item.KetQua;

                                            arr_kq = QLBV_Library.QLBV_Ham.LayChuoi(';', ketqua);
                                            _lLHN.Add(new c_LuuHuyetNao
                                            {
                                                ChiDinh = item.ChiDinh,
                                                DuongDan = item.DuongDan,
                                                DuongDan2 = item.DuongDan2,
                                                Id = item.Id,
                                                MaDVct = item.MaDVct,
                                                IDCD = item.IDCD == null ? 0 : item.IDCD.Value,
                                                SoPhieu = 0,
                                                Status = item.Status == null ? 0 : item.Status.Value,
                                                STTHT = item.STTHT == null ? 0 : item.STTHT.Value,

                                                KetQua = _listketquadvct.Where(o => o.MaDVct == item.MaDVct).Select(p => p.KetQua).First().ToString().Replace(';', ' '),
                                            });
                                        }
                                    }
                                    else
                                    {
                                        foreach (var item in _CLSct)
                                        {
                                            string ketqua = "";

                                            if (!string.IsNullOrEmpty(item.KetQua))
                                                ketqua = item.KetQua;

                                            _lLHN.Add(new c_LuuHuyetNao
                                            {
                                                ChiDinh = item.ChiDinh,
                                                DuongDan = item.DuongDan,
                                                DuongDan2 = item.DuongDan2,
                                                Id = item.Id,
                                                MaDVct = item.MaDVct,
                                                IDCD = item.IDCD == null ? 0 : item.IDCD.Value,
                                                SoPhieu = 0,
                                                Status = item.Status == null ? 0 : item.Status.Value,
                                                STTHT = item.STTHT == null ? 0 : item.STTHT.Value,

                                                KetQua = item.KetQua,
                                            });


                                        }
                                    }

                                }
                                else
                                {
                                    foreach (var item in _CLSct)
                                    {
                                        string ketqua = "";
                                        if (!string.IsNullOrEmpty(item.KetQua))
                                            ketqua = item.KetQua;
                                        arr_kq = QLBV_Library.QLBV_Ham.LayChuoi(';', ketqua);
                                        _lLHN.Add(new c_LuuHuyetNao
                                        {
                                            ChiDinh = item.ChiDinh,
                                            DuongDan = item.DuongDan,
                                            DuongDan2 = item.DuongDan2,
                                            Id = item.Id,
                                            MaDVct = item.MaDVct,
                                            IDCD = item.IDCD == null ? 0 : item.IDCD.Value,
                                            SoPhieu = 0,
                                            Status = item.Status == null ? 0 : item.Status.Value,
                                            STTHT = item.STTHT == null ? 0 : item.STTHT.Value,
                                            KetQua = arr_kq[0] == null ? "" : arr_kq[0],
                                            KetQua2 = arr_kq[1] == null ? "" : arr_kq[1],
                                            KetQua3 = arr_kq[2] == null ? "" : arr_kq[2],
                                            KetQua4 = arr_kq[3] == null ? "" : arr_kq[3],
                                            KetQua5 = arr_kq[4] == null ? "" : arr_kq[4],
                                            KetQua6 = arr_kq[5] == null ? "" : arr_kq[5],
                                            KetQua7 = arr_kq[6] == null ? "" : arr_kq[6],
                                            KetQua8 = arr_kq[7] == null ? "" : arr_kq[7],
                                            KetQua9 = arr_kq[8] == null ? "" : arr_kq[8],
                                            KetQua10 = arr_kq[9] == null ? "" : arr_kq[9],
                                            KetQua11 = arr_kq[10] == null ? "" : arr_kq[10],
                                            KetQua12 = arr_kq[11] == null ? "" : arr_kq[11],
                                        });
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in _CLSct)
                                {
                                    string ketqua = "";
                                    if (!string.IsNullOrEmpty(item.KetQua))
                                        ketqua = item.KetQua;
                                    arr_kq = QLBV_Library.QLBV_Ham.LayChuoi(';', ketqua);
                                    _lLHN.Add(new c_LuuHuyetNao
                                    {
                                        ChiDinh = item.ChiDinh,
                                        DuongDan = item.DuongDan,
                                        DuongDan2 = item.DuongDan2,
                                        Id = item.Id,
                                        MaDVct = item.MaDVct,
                                        IDCD = item.IDCD == null ? 0 : item.IDCD.Value,
                                        SoPhieu = 0,
                                        Status = item.Status == null ? 0 : item.Status.Value,
                                        STTHT = item.STTHT == null ? 0 : item.STTHT.Value,
                                        KetQua = arr_kq[0] == null ? "" : arr_kq[0],
                                        KetQua2 = arr_kq[1] == null ? "" : arr_kq[1],
                                        KetQua3 = arr_kq[2] == null ? "" : arr_kq[2],
                                        KetQua4 = arr_kq[3] == null ? "" : arr_kq[3],
                                        KetQua5 = arr_kq[4] == null ? "" : arr_kq[4],
                                        KetQua6 = arr_kq[5] == null ? "" : arr_kq[5],
                                        KetQua7 = arr_kq[6] == null ? "" : arr_kq[6],
                                        KetQua8 = arr_kq[7] == null ? "" : arr_kq[7],
                                        KetQua9 = arr_kq[8] == null ? "" : arr_kq[8],
                                        KetQua10 = arr_kq[9] == null ? "" : arr_kq[9],
                                        KetQua11 = arr_kq[10] == null ? "" : arr_kq[10],
                                        KetQua12 = arr_kq[11] == null ? "" : arr_kq[11],
                                    });
                                }
                            }

                            //  GrcKQDienTim.DataSource = _CLSct;
                            GrcKQDienTim.DataSource = null;
                            if (DungChung.Bien.MaBV == "24012")
                            {
                                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                _lLHN = (from a in _lLHN
                                         join b in data.DichVucts on a.MaDVct equals b.MaDVct
                                         orderby b.STT
                                         select a).ToList();
                            }
                            GrcKQDienTim.DataSource = _lLHN;
                            break;
                        #endregion
                        #region nội soi
                        case "Nội soi":
                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                mmKQNoisoi.RtfText = KetQua_Rtf;
                            }
                            else
                                mmKQNoisoi.Text = KetQua;
                            mmKLNoisoi.Text = KetLuan;
                            mmLoidanNoisoi.Text = LoiDan;
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                TabKetQua.SelectedTabPage = xTabCDHA30372;
                                xTabCDHA30372.Text = "Nội soi T-M-H";
                            }
                            else
                            {
                                TabKetQua.SelectedTabPage = PageNoisoi;
                            }
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = true;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Nội soi")).ToList();
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {

                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        mKetqua.RtfText = mmKQNoisoi.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        mKetqua.Text = mmKQNoisoi.Text = _CLSct.First().KetQua;
                                }
                                else
                                    mKetqua.Text = mmKQNoisoi.Text = "";
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    mKetLuan.Text = mmKLNoisoi.Text = _Chidinh.First().KetLuan;
                                }
                                else
                                    mKetLuan.Text = mmKLNoisoi.Text = "";
                                if (Ylenh.Count > 0 && Ylenh.First().KetLuan != null)
                                {
                                    mLoiDanBacSy.Text = mmLoidanNoisoi.Text = Ylenh.First().LoiDan;
                                }
                                else

                                    mLoiDanBacSy.Text = mmLoidanNoisoi.Text = "";
                                if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                {
                                    LupCanBo.EditValue = CanLS.First().MaCBth;
                                }
                                if (clsct2.Count > 0 && clsct2.First().DuongDan2 != null || clsct2.First().DuongDan2 != "")
                                {
                                    String strDD = clsct2.First().DuongDan2;
                                    Duongdandasua = strDD;
                                    string[] arrDD = DuongDan2 = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                    for (int i = 0; i < arrDD.Length; i++)
                                    {
                                        if (DungChung.Bien.MaBV == "30372")
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                    {
                                                        ptAnh1.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh1.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh1.Image = null;
                                                    }
                                                    break;
                                                case 1:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh2.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh2.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh2.Image = null;
                                                    }
                                                    break;
                                                case 2:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh3.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh3.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh3.Image = null;
                                                    }
                                                    break;
                                                case 3:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh4.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh4.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh4.Image = null;
                                                    }
                                                    break;
                                                case 4:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh5.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh5.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh5.Image = null;
                                                    }
                                                    break;
                                                case 5:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh6.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh6.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh6.Image = null;
                                                    }
                                                    break;
                                                case 6:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh7.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh7.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh7.Image = null;
                                                    }
                                                    break;
                                                case 7:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh8.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh8.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh8.Image = null;
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                    {
                                                        ptNoisoi1.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi1.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi1.Image = null;
                                                    }
                                                    break;
                                                case 1:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptNoisoi2.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi2.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi2.Image = null;
                                                    }
                                                    break;
                                                case 2:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptNoisoi3.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi3.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi3.Image = null;
                                                    }
                                                    break;
                                                case 3:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptNoisoi4.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi4.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi4.Image = null;
                                                    }
                                                    break;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    loadAnhNoiSoi();
                                }

                                if (Ylenh.First().Status == 1 || _tamthu == false)
                                {
                                    mmKQNoisoi.ReadOnly = true;
                                    mmKLNoisoi.Properties.ReadOnly = true;
                                    mmLoidanNoisoi.Properties.ReadOnly = true;
                                    mLoiDanBacSy.Properties.ReadOnly = true;
                                    mKetqua.ReadOnly = true;
                                    mKetLuan.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true); if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (Ylenh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mmKQNoisoi.ReadOnly = false;
                                    mmKLNoisoi.Properties.ReadOnly = false;
                                    mLoiDanBacSy.Properties.ReadOnly = false;
                                    mKetqua.ReadOnly = false;
                                    mKetLuan.Properties.ReadOnly = false;
                                    mmLoidanNoisoi.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region nội soi dạ dày
                        //DungChung.Bien.st_TieuNhomRG_ChuyenKhoa
                        case "Nội soi Dạ Dày":
                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                mmKQNoisoi.RtfText = KetQua_Rtf;
                            }
                            else
                                mmKQNoisoi.Text = KetQua;
                            mmKLNoisoi.Text = KetLuan;
                            mmLoidanNoisoi.Text = LoiDan;
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                TabKetQua.SelectedTabPage = xTabCDHA30372;
                                xTabCDHA30372.Text = "Nội soi";
                            }
                            else
                            {
                                TabKetQua.SelectedTabPage = PageNoisoi;
                            }
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = true;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Nội soi")).ToList();
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {
                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        mKetqua.RtfText = mmKQNoisoi.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        mKetqua.Text = mmKQNoisoi.Text = _CLSct.First().KetQua;
                                }
                                else
                                    mKetqua.Text = mmKQNoisoi.Text = "";
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    mKetLuan.Text = mmKLNoisoi.Text = _Chidinh.First().KetLuan;
                                }
                                else
                                    mKetLuan.Text = mmKLNoisoi.Text = "";
                                if (Ylenh.Count > 0 && Ylenh.First().KetLuan != null)
                                {
                                    mLoiDanBacSy.Text = mmLoidanNoisoi.Text = Ylenh.First().LoiDan;
                                }
                                else
                                    mLoiDanBacSy.Text = mmLoidanNoisoi.Text = "";
                                if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                {
                                    LupCanBo.EditValue = CanLS.First().MaCBth;
                                }
                                if (clsct2.Count > 0 && clsct2.First().DuongDan2 != null || clsct2.First().DuongDan2 != "")
                                {
                                    String strDD = clsct2.First().DuongDan2;
                                    Duongdandasua = strDD;
                                    string[] arrDD = DuongDan2 = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                    for (int i = 0; i < arrDD.Length; i++)
                                    {
                                        if (DungChung.Bien.MaBV == "30372")
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                    {
                                                        ptAnh1.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh1.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh1.Image = null;
                                                    }
                                                    break;
                                                case 1:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh2.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh2.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh2.Image = null;
                                                    }
                                                    break;
                                                case 2:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh3.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh3.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh3.Image = null;
                                                    }
                                                    break;
                                                case 3:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh4.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh4.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh4.Image = null;
                                                    }
                                                    break;
                                                case 4:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh5.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh5.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh5.Image = null;
                                                    }
                                                    break;
                                                case 5:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh6.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh6.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh6.Image = null;
                                                    }
                                                    break;
                                                case 6:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh7.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh7.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh7.Image = null;
                                                    }
                                                    break;
                                                case 7:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh8.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh8.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh8.Image = null;
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                    {
                                                        ptNoisoi1.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi1.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi1.Image = null;
                                                    }
                                                    break;
                                                case 1:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptNoisoi2.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi2.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi2.Image = null;
                                                    }
                                                    break;
                                                case 2:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptNoisoi3.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi3.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi3.Image = null;
                                                    }
                                                    break;
                                                case 3:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptNoisoi4.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi4.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi4.Image = null;
                                                    }
                                                    break;

                                                default:
                                                    break;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    loadAnhNoiSoi();
                                }

                                if (Ylenh.First().Status == 1 || _tamthu == false)
                                {
                                    mmKQNoisoi.ReadOnly = true;
                                    mmKLNoisoi.Properties.ReadOnly = true;
                                    mmLoidanNoisoi.Properties.ReadOnly = true;
                                    mLoiDanBacSy.Properties.ReadOnly = true;
                                    mKetqua.ReadOnly = true;
                                    mKetLuan.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true); if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (Ylenh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mLoiDanBacSy.Properties.ReadOnly = false;
                                    mKetqua.ReadOnly = false;
                                    mKetLuan.Properties.ReadOnly = false;
                                    mmKQNoisoi.ReadOnly = false;
                                    mmKLNoisoi.Properties.ReadOnly = false;
                                    mmLoidanNoisoi.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region nội soi cổ tử cung
                        case "Nội soi Cổ Tử Cung":
                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                mmKQNoisoi.RtfText = KetQua_Rtf;
                            }
                            else
                                mmKQNoisoi.Text = KetQua;
                            mmKLNoisoi.Text = KetLuan;
                            mmLoidanNoisoi.Text = LoiDan;
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                TabKetQua.SelectedTabPage = xTabCDHA30372;
                                xTabCDHA30372.Text = "Nội soi";
                            }
                            else
                            {
                                TabKetQua.SelectedTabPage = PageNoisoi;
                            }
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = true;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Nội soi")).ToList();
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {

                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        mKetqua.RtfText = mmKQNoisoi.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        mKetqua.Text = mmKQNoisoi.Text = _CLSct.First().KetQua;
                                }
                                else
                                    mKetqua.Text = mmKQNoisoi.Text = "";
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    mKetLuan.Text = mmKLNoisoi.Text = _Chidinh.First().KetLuan;
                                }
                                else
                                    mKetLuan.Text = mmKLNoisoi.Text = "";
                                if (Ylenh.Count > 0 && Ylenh.First().KetLuan != null)
                                {
                                    mLoiDanBacSy.Text = mmLoidanNoisoi.Text = Ylenh.First().LoiDan;
                                }
                                else
                                    mLoiDanBacSy.Text = mmLoidanNoisoi.Text = "";
                                if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                {
                                    LupCanBo.EditValue = CanLS.First().MaCBth;
                                }
                                if (clsct2.Count > 0 && clsct2.First().DuongDan2 != null || clsct2.First().DuongDan2 != "")
                                {
                                    String strDD = clsct2.First().DuongDan2;
                                    Duongdandasua = strDD;
                                    string[] arrDD = DuongDan2 = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                    for (int i = 0; i < arrDD.Length; i++)
                                    {
                                        if (DungChung.Bien.MaBV == "30372")
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                    {
                                                        ptAnh1.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh1.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh1.Image = null;
                                                    }
                                                    break;
                                                case 1:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh2.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh2.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh2.Image = null;
                                                    }
                                                    break;
                                                case 2:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh3.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh3.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh3.Image = null;
                                                    }
                                                    break;
                                                case 3:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh4.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh4.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh4.Image = null;
                                                    }
                                                    break;
                                                case 4:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh5.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh5.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh5.Image = null;
                                                    }
                                                    break;
                                                case 5:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh6.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh6.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh6.Image = null;
                                                    }
                                                    break;
                                                case 6:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh7.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh7.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh7.Image = null;
                                                    }
                                                    break;
                                                case 7:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptAnh8.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptAnh8.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptAnh8.Image = null;
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (i)
                                            {
                                                case 0:
                                                    if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                    {
                                                        ptNoisoi1.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi1.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi1.Image = null;
                                                    }
                                                    break;
                                                case 1:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptNoisoi2.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi2.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi2.Image = null;
                                                    }
                                                    break;
                                                case 2:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptNoisoi3.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi3.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi3.Image = null;
                                                    }
                                                    break;
                                                case 3:
                                                    if (string.IsNullOrEmpty(arrDD[i]))
                                                    {
                                                        ptNoisoi4.Image = null;
                                                    }
                                                    else
                                                    {
                                                        if (File.Exists(arrDD[i]))
                                                            ptNoisoi4.Image = Image.FromFile(arrDD[i]);
                                                        else
                                                            ptNoisoi4.Image = null;
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    loadAnhNoiSoi();
                                }

                                if (Ylenh.First().Status == 1 || _tamthu == false)
                                {
                                    mKetLuan.Properties.ReadOnly = true;
                                    mKetqua.ReadOnly = true;
                                    mLoiDanBacSy.Properties.ReadOnly = true;
                                    mmKQNoisoi.ReadOnly = true;
                                    mmKLNoisoi.Properties.ReadOnly = true;
                                    mmLoidanNoisoi.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true); if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (Ylenh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {

                                    mKetLuan.Properties.ReadOnly = false;
                                    mKetqua.ReadOnly = false;
                                    mLoiDanBacSy.Properties.ReadOnly = false;
                                    mmKQNoisoi.ReadOnly = false;
                                    mmKLNoisoi.Properties.ReadOnly = false;
                                    mmLoidanNoisoi.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region thủ thuật
                        case "Thủ thuật":
                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                mmKQNoisoi.RtfText = KetQua_Rtf;
                            }
                            else
                                mmKQNoisoi.Text = KetQua;
                            mmKLNoisoi.Text = KetLuan;
                            mmLoidanNoisoi.Text = LoiDan;
                            TabKetQua.SelectedTabPage = PageNoisoi;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = true;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Nội soi")).ToList();
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {
                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        mmKQNoisoi.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        mmKQNoisoi.Text = _CLSct.First().KetQua;
                                }
                                else
                                    mmKQNoisoi.Text = "";
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    mmKLNoisoi.Text = _Chidinh.First().KetLuan;
                                }
                                else
                                    mmKLNoisoi.Text = "";
                                if (Ylenh.Count > 0 && Ylenh.First().KetLuan != null)
                                {
                                    mmLoidanNoisoi.Text = Ylenh.First().LoiDan;
                                }
                                else
                                    mmLoidanNoisoi.Text = "";
                                if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                {
                                    LupCanBo.EditValue = CanLS.First().MaCBth;
                                }
                                if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null)
                                {
                                    String strDD = _CLSct.First().DuongDan2;
                                    Duongdandasua = strDD;
                                    string[] arrDD = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                    for (int i = 0; i < arrDD.Length; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                {
                                                    ptNoisoi1.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptNoisoi1.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptNoisoi1.Image = null;
                                                }
                                                break;
                                            case 1:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptNoisoi2.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptNoisoi2.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptNoisoi2.Image = null;
                                                }
                                                break;
                                            case 2:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptNoisoi3.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptNoisoi3.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptNoisoi3.Image = null;
                                                }
                                                break;
                                            case 3:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptNoisoi4.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptNoisoi4.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptNoisoi4.Image = null;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    loadAnhNoiSoi();
                                }

                                if (Ylenh.First().Status == 1 || _tamthu == false)
                                {
                                    mmKQNoisoi.ReadOnly = true;
                                    mmKLNoisoi.Properties.ReadOnly = true;
                                    mmLoidanNoisoi.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (Ylenh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mmKQNoisoi.ReadOnly = false;
                                    mmKLNoisoi.Properties.ReadOnly = false;
                                    mmLoidanNoisoi.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region phẫu thuật
                        case "Phẫu thuật":
                            if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                            {
                                mmKQNoisoi.RtfText = KetQua_Rtf;
                            }
                            else
                                mmKQNoisoi.Text = KetQua;
                            mmKLNoisoi.Text = KetLuan;
                            mmLoidanNoisoi.Text = LoiDan;
                            TabKetQua.SelectedTabPage = PageNoisoi;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = true;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Nội soi")).ToList();
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {

                                if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                {
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(_CLSct.First().KetQua_Rtf))
                                    {
                                        mmKQNoisoi.RtfText = _CLSct.First().KetQua_Rtf;
                                    }
                                    else
                                        mmKQNoisoi.Text = _CLSct.First().KetQua;
                                }
                                else
                                    mmKQNoisoi.Text = "";
                                if (_Chidinh.Count > 0 && _Chidinh.First().KetLuan != null)
                                {
                                    mmKLNoisoi.Text = _Chidinh.First().KetLuan;
                                }
                                else
                                    mmKLNoisoi.Text = "";
                                if (Ylenh.Count > 0 && Ylenh.First().KetLuan != null)
                                {
                                    mmLoidanNoisoi.Text = Ylenh.First().LoiDan;
                                }
                                else
                                    mmLoidanNoisoi.Text = "";
                                if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                {
                                    LupCanBo.EditValue = CanLS.First().MaCBth;
                                }
                                if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null)
                                {
                                    String strDD = _CLSct.First().DuongDan2;
                                    Duongdandasua = strDD;
                                    string[] arrDD = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
                                    for (int i = 0; i < arrDD.Length; i++)
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                if (string.IsNullOrEmpty(arrDD[i]) && arrDD[i].Length <= 3)
                                                {
                                                    ptNoisoi1.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptNoisoi1.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptNoisoi1.Image = null;
                                                }
                                                break;
                                            case 1:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptNoisoi2.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptNoisoi2.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptNoisoi2.Image = null;
                                                }
                                                break;
                                            case 2:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptNoisoi3.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptNoisoi3.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptNoisoi3.Image = null;
                                                }
                                                break;
                                            case 3:
                                                if (string.IsNullOrEmpty(arrDD[i]))
                                                {
                                                    ptNoisoi4.Image = null;
                                                }
                                                else
                                                {
                                                    if (File.Exists(arrDD[i]))
                                                        ptNoisoi4.Image = Image.FromFile(arrDD[i]);
                                                    else
                                                        ptNoisoi4.Image = null;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    loadAnhNoiSoi();
                                }

                                if (Ylenh.First().Status == 1 || _tamthu == false)
                                {
                                    mmKQNoisoi.ReadOnly = true;
                                    mmKLNoisoi.Properties.ReadOnly = true;
                                    mmLoidanNoisoi.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false; ;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (Ylenh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mmKQNoisoi.ReadOnly = false;
                                    mmKLNoisoi.Properties.ReadOnly = false;
                                    mmLoidanNoisoi.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region "Siêu âm ( Doppler )":
                        case "Siêu âm ( Doppler )":

                            gpAnh(picSieuAm1);
                            gpAnh(picSieuAm2);
                            gpAnh(picSieuAm3);
                            gpAnh(picSieuAm4);
                            gpAnh(picSieuAm5);
                            gpAnh(picSieuAm6);
                            gpAnh(picSieuAm7);
                            gpAnh(picSieuAm8);
                            if (Phieu.Contains(grvketqua.GetFocusedRowCellValue(tendv).ToString()))
                            {
                                if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !string.IsNullOrEmpty(KetQua_Rtf))
                                {
                                    reKQSieuAm.RtfText = txtKQ_SADoppler.Text = KetQua_Rtf;
                                }
                                else
                                    reKQSieuAm.Text = txtKQ_SADoppler.Text = KetQua;
                                mmKLSieuam.Text = KetLuan;
                                mmLoidanSieuAm.Text = LoiDan;
                                string patd = clsct2.First().DuongDan == null ? "" : clsct2.First().DuongDan;
                                string patd2 = clsct2.First().DuongDan2 == null ? "" : clsct2.First().DuongDan2;
                                string[] duongdanA1 = patd.Split(';');
                                string[] duongdanA2 = patd2.Split(';');
                                if (patd != "")
                                {
                                    if (duongdanA1.Length == 1)
                                    {
                                        if (duongdanA1[0] != "")
                                        {
                                            _fileanh = duongdanA1[0];
                                            picSieuAm1.Image = _fileanh == "" ? null : Image.FromFile(_fileanh);
                                        }
                                    }
                                    if (duongdanA1.Length == 2)
                                    {
                                        if (duongdanA1[0] != "")
                                        {
                                            _fileanh = duongdanA1[0];
                                            picSieuAm1.Image = _fileanh == "" ? null : Image.FromFile(_fileanh);
                                        }

                                        if (duongdanA1[1] != "")
                                        {
                                            _fileanh3 = duongdanA1[1];
                                            picSieuAm3.Image = _fileanh3 == "" ? null : Image.FromFile(_fileanh3);
                                        }
                                    }
                                    if (duongdanA1.Length == 3)
                                    {
                                        if (duongdanA1[0] != "")
                                        {
                                            _fileanh = duongdanA1[0];
                                            picSieuAm1.Image = _fileanh == "" ? null : Image.FromFile(_fileanh);
                                        }

                                        if (duongdanA1[1] != "")
                                        {
                                            _fileanh3 = duongdanA1[1];
                                            picSieuAm3.Image = _fileanh3 == "" ? null : Image.FromFile(_fileanh3);
                                        }

                                        if (duongdanA1[2] != "")
                                        {
                                            _fileanh5 = duongdanA1[2];
                                            picSieuAm5.Image = _fileanh5 == "" ? null : Image.FromFile(_fileanh5);
                                        }
                                    }
                                    if (duongdanA1.Length == 4)
                                    {
                                        if (duongdanA1[0] != "")
                                        {
                                            _fileanh = duongdanA1[0];
                                            picSieuAm1.Image = _fileanh == "" ? null : Image.FromFile(_fileanh);
                                        }

                                        if (duongdanA1[1] != "")
                                        {
                                            _fileanh3 = duongdanA1[1];
                                            picSieuAm3.Image = _fileanh3 == "" ? null : Image.FromFile(_fileanh3);
                                        }

                                        if (duongdanA1[2] != "")
                                        {
                                            _fileanh5 = duongdanA1[2];
                                            picSieuAm5.Image = _fileanh5 == "" ? null : Image.FromFile(_fileanh5);
                                        }

                                        if (duongdanA1[3] != "")
                                        {
                                            _fileanh7 = duongdanA1[3];
                                            picSieuAm7.Image = _fileanh7 == "" ? null : Image.FromFile(_fileanh7);
                                        }
                                    }

                                }

                                if (patd2 != null)
                                {
                                    if (duongdanA2.Length == 1)
                                    {
                                        if (duongdanA2[0] != "")
                                        {
                                            _fileanh2 = duongdanA2[0];
                                            picSieuAm2.Image = _fileanh2 == "" ? null : Image.FromFile(_fileanh2);
                                        }

                                    }
                                    if (duongdanA2.Length == 2)
                                    {
                                        if (duongdanA2[0] != "")
                                        {
                                            _fileanh2 = duongdanA2[0];
                                            picSieuAm2.Image = _fileanh2 == "" ? null : Image.FromFile(_fileanh2);
                                        }

                                        if (duongdanA2[1] != "")
                                        {
                                            _fileanh4 = duongdanA2[1];
                                            picSieuAm4.Image = _fileanh4 == "" ? null : Image.FromFile(_fileanh4);
                                        }
                                    }
                                    if (duongdanA2.Length == 3)
                                    {
                                        if (duongdanA2[0] != "")
                                        {
                                            _fileanh2 = duongdanA2[0];
                                            picSieuAm2.Image = _fileanh2 == "" ? null : Image.FromFile(_fileanh2);
                                        }

                                        if (duongdanA2[1] != "")
                                        {
                                            _fileanh4 = duongdanA2[1];
                                            picSieuAm4.Image = _fileanh4 == "" ? null : Image.FromFile(_fileanh4);
                                        }

                                        if (duongdanA2[2] != "")
                                        {
                                            _fileanh6 = duongdanA2[2];
                                            picSieuAm6.Image = _fileanh6 == "" ? null : Image.FromFile(_fileanh6);
                                        }
                                    }
                                    if (duongdanA2.Length == 4)
                                    {
                                        if (duongdanA2[0] != "")
                                        {
                                            _fileanh2 = duongdanA2[0];
                                            picSieuAm2.Image = _fileanh2 == "" ? null : Image.FromFile(_fileanh2);
                                        }

                                        if (duongdanA2[1] != "")
                                        {
                                            _fileanh4 = duongdanA2[1];
                                            picSieuAm4.Image = _fileanh4 == "" ? null : Image.FromFile(_fileanh4);
                                        }

                                        if (duongdanA2[2] != "")
                                        {
                                            _fileanh6 = duongdanA2[2];
                                            picSieuAm6.Image = _fileanh6 == "" ? null : Image.FromFile(_fileanh6);
                                        }

                                        if (duongdanA2[3] != "")
                                        {
                                            _fileanh8 = duongdanA2[3];
                                            picSieuAm8.Image = _fileanh8 == null ? null : Image.FromFile(_fileanh8);
                                        }
                                    }

                                }



                                //if (patd != "")
                                //    ptSieuam.Image = Image.FromFile(patd);
                                //else
                                //    ptSieuam.Image = null;
                                //if (patd2 != "")
                                //    ptSieuam2.Image = Image.FromFile(patd2);
                                //else
                                //    ptSieuam2.Image = null;
                            }
                            if (_CLSct.Count > 0 && _CLSct.First().DuongDan != null && File.Exists(_CLSct.First().DuongDan))
                            {
                                string duongdan = _CLSct.First().DuongDan;
                                string[] duongdanA1 = duongdan.Split(';');
                                if (duongdanA1[0] != "")
                                {
                                    _fileanh = duongdanA1[0];
                                    picSieuAm1.Image = _fileanh == "" ? null : Image.FromFile(_fileanh);
                                }

                                if (duongdanA1[1] != "")
                                {
                                    _fileanh3 = duongdanA1[1];
                                    picSieuAm3.Image = _fileanh3 == "" ? null : Image.FromFile(_fileanh3);
                                }

                                if (duongdanA1[2] != "")
                                {
                                    _fileanh5 = duongdanA1[2];
                                    picSieuAm5.Image = _fileanh5 == "" ? null : Image.FromFile(_fileanh5);
                                }

                                if (duongdanA1[3] != "")
                                {
                                    _fileanh7 = duongdanA1[3];
                                    picSieuAm7.Image = _fileanh7 == "" ? null : Image.FromFile(_fileanh7);
                                }
                            }
                            //else
                            //{
                            //    string patd = clsct2.First().DuongDan == null ? "" : clsct2.First().DuongDan;
                            //    picSieuAm1.Image = null;
                            //    picSieuAm3.Image = null;
                            //    picSieuAm5.Image = null;
                            //    picSieuAm7.Image = null;

                            //}
                            if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null && File.Exists(_CLSct.First().DuongDan2))
                            {
                                string duongdan2 = _CLSct.First().DuongDan2;
                                string[] duongdanA2 = duongdan2.Split(';');
                                _fileanh2 = _CLSct.First().DuongDan2;
                                picSieuAm2.Image = Image.FromFile(_fileanh2);
                                if (duongdanA2[0] != null)
                                {
                                    _fileanh2 = duongdanA2[0];
                                    picSieuAm2.Image = _fileanh2 == "" ? null : Image.FromFile(_fileanh2);
                                }

                                if (duongdanA2[1] != null)
                                {
                                    _fileanh4 = duongdanA2[1];
                                    picSieuAm4.Image = _fileanh4 == "" ? null : Image.FromFile(_fileanh4);
                                }

                                if (duongdanA2[2] != null)
                                {
                                    _fileanh6 = duongdanA2[2];
                                    picSieuAm6.Image = _fileanh6 == "" ? null : Image.FromFile(_fileanh6);
                                }

                                if (duongdanA2[3] != null)
                                {
                                    _fileanh8 = duongdanA2[3];
                                    picSieuAm8.Image = _fileanh8 == "" ? null : Image.FromFile(_fileanh8);
                                }
                            }
                            //else
                            //{
                            //    string patd2 = clsct2.First().DuongDan2 == null ? "" : clsct2.First().DuongDan2;
                            //    picSieuAm2.Image = null;
                            //    picSieuAm4.Image = null;
                            //    picSieuAm6.Image = null;
                            //    picSieuAm8.Image = null;
                            //}
                            mmKetLuan.Text = KetLuan;
                            mmLoidan.Text = LoiDan;
                            mmSA_Doppler.Text = GhiChu;
                            if (DungChung.Bien.MaBV == "01048" || DungChung.Bien.MaBV == "01071")
                            {
                                string tdv = grvketqua.GetFocusedRowCellValue(tendv).ToString();
                                if (tdv == "Siêu âm Doppler u tuyến, hạch vùng cổ" || tdv == "Siêu âm Doppler tuyến vú" || tdv == "Siêu âm Doppler tử cung phần phụ")
                                    TabKetQua.SelectedTabPage = PageSieuam;
                                else
                                {
                                    TabKetQua.SelectedTabPage = PageSieuamDoppler;
                                    tabCtrl_SADoppler.SelectedTabPage = tab_KetQuaChung;
                                }

                            }
                            else
                            {
                                TabKetQua.SelectedTabPage = PageSieuamDoppler;
                            }
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = true;//phòng siêu âm DL có thể làm cả siêu âm thường liễu YC 29/08/17
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = true;
                            ((Control)this.tabDoCNHH).Enabled = false;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            //LupCanBo.Properties.DataSource = _BS.Where(p => p.ChuyenKhoa.Contains("Siêu âm (Doppler)")).ToList();

                            KetQua2.VisibleIndex = -1;
                            KetQua3.VisibleIndex = -1;
                            KetQua4.VisibleIndex = -1;
                            KetQua5.VisibleIndex = -1;
                            KetQua6.VisibleIndex = -1;
                            KetQua7.VisibleIndex = -1;
                            KetQua8.VisibleIndex = -1;
                            KetQua9.VisibleIndex = -1;
                            KetQua10.VisibleIndex = -1;
                            //if (chk_LHN.Checked)
                            //{
                            //col_KQ2.VisibleIndex = 2;
                            //col_KQ3.VisibleIndex = 3;
                            //col_KQ4.VisibleIndex = 4;
                            //col_KQ5.VisibleIndex = 5;
                            //col_KQ6.VisibleIndex = 6;
                            //col_KQ7.VisibleIndex = 7;
                            string[] td_lhn = CLS.InPhieu.td_lhn;


                            for (int i = 0; i < td_lhn.Length; i++)
                            {
                                switch (i)
                                {
                                    case 0:
                                        colKetqua.Caption = td_lhn[i];
                                        break;
                                    case 1:
                                        KetQua2.Caption = td_lhn[i];
                                        KetQua2.VisibleIndex = 2;
                                        break;
                                    case 2:
                                        KetQua3.Caption = td_lhn[i];
                                        KetQua3.VisibleIndex = 3;
                                        break;
                                    case 3:
                                        KetQua4.Caption = td_lhn[i];
                                        KetQua4.VisibleIndex = 4;
                                        break;
                                    case 4:
                                        KetQua5.Caption = td_lhn[i];
                                        KetQua5.VisibleIndex = 5;
                                        break;
                                    case 5:
                                        KetQua6.Caption = td_lhn[i];
                                        KetQua6.VisibleIndex = 6;
                                        break;
                                    case 6:
                                        KetQua7.Caption = td_lhn[i];
                                        KetQua7.VisibleIndex = 7;
                                        break;
                                    case 7:
                                        KetQua8.Caption = td_lhn[i];
                                        KetQua8.VisibleIndex = 8;
                                        break;
                                    case 8:
                                        KetQua9.Caption = td_lhn[i];
                                        KetQua9.VisibleIndex = 9;
                                        break;
                                    case 9:
                                        KetQua10.Caption = td_lhn[i];
                                        KetQua10.VisibleIndex = 10;
                                        break;
                                    default:

                                        break;

                                }

                            }
                            //}
                            //else
                            //{
                            //    colKetqua.Caption = "Kết quả";
                            //}
                            //else

                            var TenDV1 = (from Dv in _data.DichVucts select new { Dv.MaDVct, Dv.TenDVct }).ToList();
                            LupTenDVDT_sa.DataSource = TenDV1;
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {

                                _lDoppler.Clear();
                                string[] arr_kq_doppler = new string[10];
                                for (int i = 0; i < 10; i++)
                                {
                                    arr_kq_doppler[i] = "";
                                }
                                foreach (var item in _CLSct)
                                {
                                    string ketqua = "";
                                    if (!string.IsNullOrEmpty(item.KetQua))
                                        ketqua = item.KetQua;
                                    arr_kq_doppler = QLBV_Library.QLBV_Ham.LayChuoi(';', ketqua);
                                    _lDoppler.Add(new c_LuuHuyetNao
                                    {
                                        ChiDinh = item.ChiDinh,
                                        DuongDan = item.DuongDan,
                                        DuongDan2 = item.DuongDan2,
                                        Id = item.Id,
                                        MaDVct = item.MaDVct,
                                        IDCD = item.IDCD == null ? 0 : item.IDCD.Value,
                                        SoPhieu = 0,
                                        Status = item.Status == null ? 0 : item.Status.Value,
                                        STTHT = item.STTHT == null ? 0 : item.STTHT.Value,
                                        KetQua = arr_kq_doppler[0] == null ? "" : arr_kq_doppler[0],
                                        KetQua2 = arr_kq_doppler[1] == null ? "" : arr_kq_doppler[1],
                                        KetQua3 = arr_kq_doppler[2] == null ? "" : arr_kq_doppler[2],
                                        KetQua4 = arr_kq_doppler[3] == null ? "" : arr_kq_doppler[3],
                                        KetQua5 = arr_kq_doppler[4] == null ? "" : arr_kq_doppler[4],
                                        KetQua6 = arr_kq_doppler[5] == null ? "" : arr_kq_doppler[5],
                                        KetQua7 = arr_kq_doppler[6] == null ? "" : arr_kq_doppler[6],
                                        KetQua8 = arr_kq_doppler[7] == null ? "" : arr_kq_doppler[7],
                                        KetQua9 = arr_kq_doppler[8] == null ? "" : arr_kq_doppler[8],
                                        KetQua10 = arr_kq_doppler[9] == null ? "" : arr_kq_doppler[9],
                                    });
                                }
                                if (Ylenh.First().Status == 1 || _tamthu == false)
                                {
                                    mmKetLuan.Properties.ReadOnly = true;
                                    mmLoidan.Properties.ReadOnly = true;
                                    mmSA_Doppler.Properties.ReadOnly = true;
                                    txtSADL_TinhMach1.Properties.ReadOnly = true;
                                    txtSADL_TinhMach2.Properties.ReadOnly = true;
                                    txtKQ_SADoppler.Properties.ReadOnly = true;
                                    GrvKQDienTim.OptionsBehavior.Editable = false;
                                    GrvDVct.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (Ylenh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    mmKetLuan.Properties.ReadOnly = false;
                                    mmLoidan.Properties.ReadOnly = false;
                                    mmSA_Doppler.Properties.ReadOnly = false;
                                    txtSADL_TinhMach1.Properties.ReadOnly = false;
                                    txtSADL_TinhMach2.Properties.ReadOnly = false;
                                    txtKQ_SADoppler.Properties.ReadOnly = false;
                                    GrvKQDienTim.OptionsBehavior.Editable = true;
                                    GrvDVct.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }
                                grcDVct.DataSource = null;
                                grcDVct.DataSource = _lDoppler.OrderBy(p => p.STTHT).ToList();

                            }

                            if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                            {

                                List<string> lKQ = _CLSct.First().KetQua.Split('|').ToList();
                                if (lKQ.Count > 0)
                                {
                                    txtSADL_TinhMach1.Text = lKQ.First();
                                    if (lKQ.Count > 1)
                                        txtSADL_TinhMach2.Text = lKQ.Skip(1).First();
                                    else
                                        txtSADL_TinhMach2.Text = "";
                                }
                                else
                                {
                                    txtSADL_TinhMach1.Text = "";
                                    txtSADL_TinhMach2.Text = "";
                                }
                                txtKQ_SADoppler.Text = _CLSct.First().KetQua;
                            }
                            else
                            {
                                txtKQ_SADoppler.Text = "";
                                txtSADL_TinhMach1.Text = "";
                                txtSADL_TinhMach2.Text = "";
                            }

                            break;
                        #endregion
                        #region "Chức năng hô hấp":
                        case "Chức năng hô hấp":
                            rad_KieuDo.SelectedIndex = 1;
                            var TenDVhh = (from Dv in _data.DichVucts select new { Dv.MaDVct, Dv.TenDVct }).ToList();
                            lup_MaDVct_docn.DataSource = TenDVhh;
                            TabKetQua.SelectedTabPage = tabDoCNHH;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = true;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {


                                grc_DoCN.DataSource = null;
                                grc_DoCN.DataSource = _CLSct.OrderBy(p => p.STTHT).ToList();

                                memo_KL_DoCN.Text = KetLuan;

                                memo_LD_DoCN.Text = LoiDan;
                                if ((_Chidinh.Count > 0 && _Chidinh.First().Status != null && _Chidinh.First().Status == 1) || _tamthu == false)
                                {
                                    memo_KL_DoCN.Properties.ReadOnly = true;
                                    memo_LD_DoCN.Properties.ReadOnly = true;
                                    //memoKetLuanXQ.Properties.ReadOnly = true;

                                    grv_DoCN.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (_Chidinh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    memo_KL_DoCN.Properties.ReadOnly = false;
                                    memo_LD_DoCN.Properties.ReadOnly = false;
                                    //memoKetLuanXQ.Properties.ReadOnly = false;
                                    //GrcKQDienTim.Enabled = true;
                                    //grcDVct.Enabled = true;
                                    grv_DoCN.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                        #endregion
                        #region "Đo mật độ xương":
                        case "Đo mật độ xương":
                            rad_KieuDo.SelectedIndex = 0;
                            var TenDVlx = (from Dv in _data.DichVucts select new { Dv.MaDVct, Dv.TenDVct }).ToList();
                            lup_MaDVct_docn.DataSource = TenDVlx;
                            TabKetQua.SelectedTabPage = tabDoCNHH;
                            ((Control)this.tabNSTMH).Enabled = false;
                            ((Control)this.PageDientim).Enabled = false;
                            ((Control)this.PageXquang).Enabled = false;
                            ((Control)this.PageSieuam).Enabled = false;
                            ((Control)this.PageNoisoi).Enabled = false;
                            ((Control)this.PageSieuamDoppler).Enabled = false;
                            ((Control)this.tabDoCNHH).Enabled = true;
                            ((Control)this.tabDienNaoDo).Enabled = false;
                            ((Control)this.tabDoKhucXaMay).Enabled = false;
                            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                            {

                                grc_DoCN.DataSource = null;
                                grc_DoCN.DataSource = _CLSct.OrderBy(p => p.STTHT).ToList();
                                // end tạo list cls
                                //if (_CLSct.Count > 0 && _CLSct.First().KetQua != null)
                                //{
                                //    mmKetQuaXQ.Text = _CLSct.First().KetQua;
                                //}

                                memo_KL_DoCN.Text = KetLuan;

                                memo_LD_DoCN.Text = LoiDan;

                                //if (CanLS.Count > 0 && CanLS.First().MaCBth != null)
                                //{
                                //    LupCanBo.EditValue = CanLS.First().MaCBth;
                                //}

                                if ((_Chidinh.Count > 0 && _Chidinh.First().Status != null && _Chidinh.First().Status == 1) || _tamthu == false)
                                {
                                    memo_KL_DoCN.Properties.ReadOnly = true;
                                    memo_LD_DoCN.Properties.ReadOnly = true;
                                    //memoKetLuanXQ.Properties.ReadOnly = true;
                                    //GrcKQDienTim.Enabled = false;
                                    //grcDVct.Enabled = false;
                                    grv_DoCN.OptionsBehavior.Editable = false;
                                    EnabledControl(true);
                                    if (_tamthu == false)
                                        btnSua.Enabled = false;
                                    else if (_Chidinh.First().Status == 1)
                                        btnSua.Enabled = true;
                                }
                                else
                                {
                                    memo_KL_DoCN.Properties.ReadOnly = false;
                                    memo_LD_DoCN.Properties.ReadOnly = false;
                                    //memoKetLuanXQ.Properties.ReadOnly = false;
                                    //GrcKQDienTim.Enabled = true;
                                    //grcDVct.Enabled = true;
                                    grv_DoCN.OptionsBehavior.Editable = true;
                                    EnabledControl(false);
                                }

                            }
                            break;
                            #endregion
                    }
                }
            }
            else
            {
                switch (TenKP)
                {

                    case "Nội soi Tai-Mũi-Họng":

                        TabKetQua.SelectedTabPage = tabNSTMH;


                        break;
                    case "Răng Hàm Mặt":
                        TabKetQua.SelectedTabPage = tabNSTMH;


                        break;
                    case "Siêu âm":

                        TabKetQua.SelectedTabPage = PageSieuam;

                        break;
                    case "X-Quang":

                        TabKetQua.SelectedTabPage = PageXquang;

                        break;
                    case "Điện não đồ":

                        TabKetQua.SelectedTabPage = tabDienNaoDo;


                        break;
                    case "Trắc nghiệm tâm lý":

                        TabKetQua.SelectedTabPage = tabDoKhucXaMay;


                        break;
                    case "Lưu huyết não":

                        TabKetQua.SelectedTabPage = DungChung.Bien.MaBV == "24297" ? xtraLuuHNao : PageDientim;

                        break;


                    case "Điện tim":

                        TabKetQua.SelectedTabPage = PageDientim;

                        break;
                    case "Nội soi":

                        TabKetQua.SelectedTabPage = PageNoisoi;

                        break;
                    case "Thủ thuật":

                        TabKetQua.SelectedTabPage = PageNoisoi;

                        break;
                    case "Phẫu thuật":

                        TabKetQua.SelectedTabPage = PageNoisoi;

                        break;
                    case "Siêu âm ( Doppler )":

                        TabKetQua.SelectedTabPage = PageSieuamDoppler;

                        break;
                    case "Chức năng hô hấp":

                        TabKetQua.SelectedTabPage = tabDoCNHH;

                        break;
                    case "Đo mật độ xương":

                        TabKetQua.SelectedTabPage = tabDoCNHH;

                        break;

                }
                ((Control)this.PageDientim).Enabled = false;
                ((Control)this.PageDientim).Enabled = false;
                ((Control)this.PageXquang).Enabled = false;
                ((Control)this.tabNSTMH).Enabled = false;
                ((Control)this.PageNoisoi).Enabled = false;
                ((Control)this.PageSieuamDoppler).Enabled = false;
                ((Control)this.tabDienNaoDo).Enabled = false;
                ((Control)this.tabDoKhucXaMay).Enabled = false;
            }
        }

        private bool KT()
        {
            int ot;
            int _int_maBN = 0;
            bool ktra = true;
            if (Int32.TryParse(txtMaBN.Text, out ot))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);

            string tendn = DungChung.Bien.TenDN;

            if (DungChung.Bien.MaBV == "30372")
            {
                var q = (from ad in _data.ADMINs.Where(p => p.TenDN == tendn)
                         join cb in _data.CanBoes on ad.MaCB equals cb.MaCB
                         join kp in _data.KPhongs.Where(p => p.PLoai == "Admin") on cb.MaKP equals kp.MaKP
                         select new { ad.TenDN, ad.MaCB, kp.PLoai }).ToList();//.Distinct().ToList();
                if (q.Count > 0)
                {
                    ktra = true;
                    #region Kiểm tra Ngày thực hiện
                    if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                    {
                        int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                        if (lupNgayTH.DateTime != null)
                        {
                            string _NgayCD = _data.CLS.Where(p => p.IdCLS == IdCLS).Select(p => p.NgayThang).FirstOrDefault().ToString();
                            DateTime _NgayTH = lupNgayTH.DateTime;
                            if (_NgayCD != null)
                            {
                                if (_NgayTH < Convert.ToDateTime(_NgayCD))
                                {
                                    MessageBox.Show("Ngày Thực hiện không được < ngày chỉ định \n( Ngày chỉ định : " + _NgayCD + " )", "Thông báo", MessageBoxButtons.OK);
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
                    #endregion
                }
            }
            else
            {
                #region Kiểm tra Ngày thực hiện
                if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                {
                    int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                    if (lupNgayTH.DateTime != null)
                    {
                        var _NgayCD = _data.CLS.Where(p => p.IdCLS == IdCLS).Select(p => p.NgayThang).FirstOrDefault();
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
                #endregion
                var Tap = (from KP in _data.KPhongs.Where(p => p.MaKP == _maKP) select new { KP.NhomKP, KP.PLoai, KP.TenKP, KP.ChuyenKhoa }).ToList();
                string TenKP = "";
                if (Tap.Count > 0)
                    TenKP = Tap.First().ChuyenKhoa;
                DateTime ngay1 = Convert.ToDateTime("2018-01-01 00:00:00");
                DateTime ngay2 = Convert.ToDateTime("2018-11-03 23:59:59");

                //if (DungChung.Bien.MaBV == "26007" && DateTime.Now < ngay && TenKP==DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi)
                //{
                //    ktra = true;
                //}
                var vp = (from vpct in _data.VienPhis.Where(p => p.MaBNhan == _mabn) select new { vpct.idVPhi, vpct.NgayTT }).ToList();
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    var rv = (from rvs in _data.RaViens.Where(p => p.MaBNhan == _mabn) select new { rvs.IdRaVien }).ToList();
                    if (rv.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã ra viện không thể sửa!");
                        ktra = false;
                        return false;
                    }
                }
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
                        return false;
                    }
                }
                var ktRaVien = _data.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                if (ktRaVien.Count > 0)
                {
                    ktra = false;
                    MessageBox.Show("Bệnh nhân đã ra viện, bạn không thể lưu kết quả");
                    return false;
                }
            }

            #region kiểm tra dữ liệu
            if (ktra)
            {
                string TT = TabKetQua.SelectedTabPage.Name;
                if (grvketqua.GetFocusedRowCellValue("TIDCD") != null && grvketqua.GetFocusedRowCellValue("id") != null)
                {
                    int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                    int _madv = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("madv").ToString());
                    switch (TT)
                    {
                        case "xTabCDHA30372":

                            bool luuCD = true;
                            if (DungChung.Bien.MaBV != "30372" && (string.IsNullOrEmpty(mKetqua.Text) || string.IsNullOrEmpty(mKetLuan.Text) || LupCanBo.EditValue == null))
                            {
                                DialogResult _dresult4 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult4 == DialogResult.Yes)
                                {
                                    luuCD = true;
                                }
                                else
                                {
                                    luuCD = false;
                                }
                            }
                            if (luuCD)
                            {
                                foreach (var item in _Chidinh)
                                {
                                    item.KetLuan = mKetLuan.Text;
                                    item.LoiDan = mLoiDanBacSy.Text;
                                    item.TamThu = 1;
                                    item.Status = 1;
                                    item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                                }
                                if (LupCanBo.EditValue != null)
                                    _Cls.First().MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    _Cls.First().MaCBth = "";
                                foreach (var a in _CLSct)
                                {
                                    a.KetQua = mKetqua.Text;
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297"))
                                        a.KetQua_Rtf = mKetqua.RtfText;
                                    a.DuongDan2 = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrDuongDan);
                                }
                            }
                            return luuCD;
                        case "PageDientim":
                            bool _luu = true;
                            if (DungChung.Bien.MaBV != "24009" && (string.IsNullOrEmpty(mmKetLuanDientim.Text) || string.IsNullOrEmpty(LupCanBo.Text)))
                            {
                                DialogResult _dresult = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult == DialogResult.No)
                                {
                                    _luu = false;
                                    return false;
                                }
                            }
                            if (_luu)
                            {
                                foreach (var item in _Chidinh)
                                {
                                    item.KetLuan = mmKetLuanDientim.Text;
                                    item.LoiDan = mmLoidanDientim.Text;
                                    item.TamThu = 1;
                                    item.Status = 1;
                                    item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                                }

                                if (LupCanBo.EditValue != null)
                                    _Cls.First().MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    _Cls.First().MaCBth = "";

                                foreach (var item in _CLSct)
                                {
                                    foreach (var c in _lLHN)
                                    {
                                        string _ketqua = "";
                                        bool kq = false;
                                        if (!string.IsNullOrEmpty(c.KetQua2) || !string.IsNullOrEmpty(c.KetQua3) || !string.IsNullOrEmpty(c.KetQua4) ||
                                            !string.IsNullOrEmpty(c.KetQua5) || !string.IsNullOrEmpty(c.KetQua5) || !string.IsNullOrEmpty(c.KetQua6) || !string.IsNullOrEmpty(c.KetQua7))
                                            kq = true;
                                        if (!string.IsNullOrEmpty(c.KetQua))
                                        {
                                            _ketqua += c.KetQua;

                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua2))
                                        {
                                            _ketqua += ";" + c.KetQua2;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua3))
                                        {

                                            _ketqua += ";" + c.KetQua3;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua4))
                                        {

                                            _ketqua += ";" + c.KetQua4;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua5))
                                        {

                                            _ketqua += ";" + c.KetQua5;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua6))
                                        {

                                            _ketqua += ";" + c.KetQua6;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua7))
                                        {

                                            _ketqua += ";" + c.KetQua7;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua8))
                                        {

                                            _ketqua += ";" + c.KetQua8;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua9))
                                        {

                                            _ketqua += ";" + c.KetQua9;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua10))
                                        {
                                            _ketqua += ";" + c.KetQua10;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua11))
                                        {
                                            _ketqua += ";" + c.KetQua11;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (!string.IsNullOrEmpty(c.KetQua12))
                                        {
                                            _ketqua += ";" + c.KetQua12;
                                        }
                                        else
                                        {
                                            if (kq)
                                                _ketqua += ";";
                                        }
                                        if (item.Id == c.Id)
                                        {
                                            item.KetQua = _ketqua;
                                            item.SoPhieu = c.SoPhieu;
                                            item.Status = c.Status;
                                            item.STTHT = c.STTHT;
                                        }
                                    }
                                }
                                return true;
                            }
                            break;
                        case "PageXquang":
                            bool tieptuc_xq = true;

                            if (DungChung.Bien.MaBV != "24009" && (string.IsNullOrEmpty(memoKetLuanXQ.Text) || string.IsNullOrEmpty(LupCanBo.Text)))
                            {
                                DialogResult _dresult2 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult2 == DialogResult.No)
                                {
                                    tieptuc_xq = false;
                                    return false;
                                }

                            }
                            if (tieptuc_xq)
                            {
                                foreach (var item in _Chidinh)
                                {
                                    item.KetLuan = memoKetLuanXQ.Text;
                                    item.LoiDan = mmLoidanXQ.Text;
                                    item.TamThu = 1;
                                    item.Status = 1;
                                    item.SoPhim = rgsophim.SelectedIndex;
                                    item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                                }
                                foreach (var item in _Cls)
                                {
                                    if (LupCanBo.EditValue != null)
                                        item.MaCBth = LupCanBo.EditValue.ToString();
                                    else
                                        item.MaCBth = "";
                                }
                                foreach (var a in _CLSct)
                                {
                                    //a.Ngaythang = lupNgayTH.DateTime;
                                    //a.MaCB = LupCanBo.EditValue.ToString();
                                    a.KetQua = mmKetQuaXQ.Text;
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297"))
                                        a.KetQua_Rtf = mmKetQuaXQ.RtfText;
                                    a.SoPhieu = radCoPhim.SelectedIndex;
                                    a.DuongDan2 = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrDuongDan);
                                }
                                return true;
                            }
                            break;

                        case "tabDienNaoDo":

                            if ((DungChung.Bien.MaBV != "24009" || DungChung.Bien.MaBV != "30007") && (string.IsNullOrEmpty(memoKQ_DienNaoDo.Text) || string.IsNullOrEmpty(LupCanBo.Text)))
                            {
                                DialogResult _dresult2 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả hoặc cán bộ thực hiện, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult2 == DialogResult.No)
                                {
                                    return false;
                                }
                            }
                            foreach (var item in _Chidinh)
                            {
                                item.KetLuan = memoKL_DienNaoDo.Text;
                                item.LoiDan = memoLoiDan_DienNaoDo.Text;
                                item.TamThu = 1;
                                item.Status = 1;
                                item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                            }
                            foreach (var item in _Cls)
                            {
                                if (LupCanBo.EditValue != null)
                                    item.MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    item.MaCBth = "";
                            }
                            foreach (var a in _CLSct)
                            {
                                a.KetQua = memoKQ_DienNaoDo.Text;
                            }

                            return true;

                        case "xtraLuuHNao":

                            if (string.IsNullOrEmpty(LupCanBo.Text))
                            {
                                DialogResult _dresult2 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả hoặc cán bộ thực hiện, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult2 == DialogResult.No)
                                {
                                    return false;
                                }
                            }
                            foreach (var item in _Chidinh)
                            {
                                item.KetLuan = txtKetLuanLHN.Text;
                                item.LoiDan = txtLoiDanLHN.Text;
                                item.TamThu = 1;
                                item.Status = 1;
                                item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                            }
                            foreach (var item in _Cls)
                            {
                                if (LupCanBo.EditValue != null)
                                    item.MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    item.MaCBth = "";
                            }
                            foreach (var a in _CLSct)
                            {
                                a.KetQua = txt01.Text + ";" + txt02.Text + ";" + txt03.Text + ";" + txt04.Text + ";" + txt05.Text + ";" + txt06.Text + ";" + txt07.Text + ";" + txt08.Text + ";" + txt09.Text + ";" + txt10.Text + ";" + txt11.Text + ";" + txt12.Text + ";" + txt13.Text + ";" + txt14.Text + ";" + txt15.Text + ";" + txt16.Text;
                            }
                            return true;

                        case "tabDoKhucXaMay":

                            if (DungChung.Bien.MaBV != "24009" && (string.IsNullOrEmpty(mmKetQua_DoKhucXaMay.Text) || string.IsNullOrEmpty(LupCanBo.Text)))
                            {
                                DialogResult _dresult2 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả hoặc cán bộ thực hiện, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult2 == DialogResult.No)
                                {
                                    return false;
                                }
                            }
                            foreach (var item in _Chidinh)
                            {
                                item.TamThu = 1;
                                item.Status = 1;
                                item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                            }
                            foreach (var item in _Cls)
                            {
                                if (LupCanBo.EditValue != null)
                                    item.MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    item.MaCBth = "";
                            }
                            foreach (var a in _CLSct)
                            {
                                a.KetQua = mmKetQua_DoKhucXaMay.Text;
                            }
                            return true;

                        case "PageSieuam":
                            bool tieptuc = true;
                            if (DungChung.Bien.MaBV != "24009" && (string.IsNullOrEmpty(reKQSieuAm.Text) || string.IsNullOrEmpty(mmKLSieuam.Text) || LupCanBo.EditValue == null || LupCanBo.EditValue == null))
                            {
                                DialogResult _dresult3 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult3 != DialogResult.Yes)
                                {
                                    tieptuc = false;
                                }
                            }
                            if (tieptuc)
                            {
                                foreach (var item in _Chidinh)
                                {
                                    item.KetLuan = mmKLSieuam.Text;
                                    item.LoiDan = mmLoidanSieuAm.Text;
                                    item.TamThu = 1;
                                    item.Status = 1;
                                    item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                                }
                                if (LupCanBo.EditValue != null)
                                    _Cls.First().MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    _Cls.First().MaCBth = "";
                                string _tenfileanh = "", _tenfileanh2 = "";
                                //fileanh1
                                if (!string.IsNullOrEmpty(_fileanh))
                                {
                                    _tenfileanh = DungChung.Bien.DuongDan + "\\";
                                    _tenfileanh += _mabn + "_" + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh))
                                        {
                                            if (!File.Exists(_tenfileanh))
                                            {
                                                File.Copy(_fileanh, _tenfileanh);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh = layTenFileAnh(_fileanh, _tenfileanh);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                //fileanh2
                                if (!string.IsNullOrEmpty(_fileanh2))
                                {
                                    _tenfileanh2 = DungChung.Bien.DuongDan + "\\";
                                    _tenfileanh2 += _mabn + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh2))
                                        {
                                            if (!File.Exists(_tenfileanh2))
                                            {
                                                File.Copy(_fileanh2, _tenfileanh2);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh2 = layTenFileAnh(_fileanh2, _tenfileanh2);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                foreach (var a in _CLSct)
                                {
                                    //var ktradv = _ldvu.Where(p => p.MaDV == _madv).Where(p => p.TenDV.ToLower().Contains("siêu âm thai") && p.TenDV.ToLower().Contains("4d")).ToList();
                                    //if (DungChung.Bien.MaBV == "27183" && ktradv.Count > 0)
                                    //{
                                    //    a.KetQua = reKQSieuAm.Text.Replace("\r\n", ";");
                                    //}
                                    //else
                                    a.KetQua = reKQSieuAm.Text;
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297"))
                                        a.KetQua_Rtf = reKQSieuAm.RtfText;
                                    a.DuongDan = _tenfileanh;
                                    a.DuongDan2 = _tenfileanh2;
                                }
                            }
                            return tieptuc;

                        case "PageNoisoi":
                            bool luu = true;
                            if (DungChung.Bien.MaBV != "24009" && (string.IsNullOrEmpty(mmKQNoisoi.Text) || string.IsNullOrEmpty(mmKLNoisoi.Text) || LupCanBo.EditValue == null))
                            {
                                DialogResult _dresult4 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult4 == DialogResult.Yes)
                                {
                                    luu = true;
                                }
                                else
                                {
                                    luu = false;
                                }
                            }
                            if (luu)
                            {
                                foreach (var item in _Chidinh)
                                {
                                    item.KetLuan = mmKLNoisoi.Text;
                                    item.LoiDan = mmLoidanNoisoi.Text;
                                    item.TamThu = 1;
                                    item.Status = 1;
                                    item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                                }
                                if (LupCanBo.EditValue != null)
                                    _Cls.First().MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    _Cls.First().MaCBth = "";
                                foreach (var a in _CLSct)
                                {
                                    a.KetQua = mmKQNoisoi.Text;
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297"))
                                        a.KetQua_Rtf = mmKQNoisoi.RtfText;
                                    a.DuongDan2 = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrDuongDan);
                                }
                            }
                            return luu;
                        case "PageSieuamDoppler":
                            bool luuSA = true;
                            if (DungChung.Bien.MaBV != "24009" && (string.IsNullOrEmpty(mmKetLuan.Text) || LupCanBo.EditValue == null))
                            {
                                DialogResult _dresult5 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (_dresult5 == DialogResult.No)
                                    luuSA = false;
                            }
                            if (luuSA)
                            {
                                foreach (var item in _Chidinh)
                                {
                                    item.KetLuan = mmKetLuan.Text;
                                    item.LoiDan = mmLoidan.Text;
                                    item.GhiChu = mmSA_Doppler.Text;
                                    item.TamThu = 1;
                                    item.Status = 1;

                                    item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                                }
                                string _tenfileanh = "", _tenfileanh2 = "";
                                string _tenfileanh3 = "", _tenfileanh4 = "";
                                string _tenfileanh5 = "", _tenfileanh6 = "";
                                string _tenfileanh7 = "", _tenfileanh8 = "";
                                //fileanh1
                                if (!string.IsNullOrEmpty(_fileanh))
                                {
                                    _tenfileanh = DungChung.Bien.DuongDan + "\\";
                                    // MessageBox.Show(_fileanh);
                                    _tenfileanh += _mabn + "_" + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh))
                                        {
                                            if (!File.Exists(_tenfileanh))
                                            {
                                                File.Copy(_fileanh, _tenfileanh);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh = layTenFileAnh(_fileanh, _tenfileanh);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                //fileanh2
                                if (!string.IsNullOrEmpty(_fileanh2))
                                {
                                    _tenfileanh2 = DungChung.Bien.DuongDan + "\\";
                                    _tenfileanh2 += _mabn + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh2))
                                        {
                                            if (!File.Exists(_tenfileanh2))
                                            {
                                                File.Copy(_fileanh2, _tenfileanh2);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh2 = layTenFileAnh(_fileanh2, _tenfileanh2);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                //fileanh3
                                if (!string.IsNullOrEmpty(_fileanh3))
                                {
                                    _tenfileanh3 = DungChung.Bien.DuongDan + "\\";
                                    _tenfileanh3 += _mabn + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh3))
                                        {
                                            if (!File.Exists(_tenfileanh3))
                                            {
                                                File.Copy(_fileanh3, _tenfileanh3);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh3 = layTenFileAnh(_fileanh3, _tenfileanh3);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                //fileanh4
                                if (!string.IsNullOrEmpty(_fileanh4))
                                {
                                    _tenfileanh4 = DungChung.Bien.DuongDan + "\\";
                                    _tenfileanh4 += _mabn + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh4))
                                        {
                                            if (!File.Exists(_tenfileanh4))
                                            {
                                                File.Copy(_fileanh4, _tenfileanh4);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh4 = layTenFileAnh(_fileanh4, _tenfileanh4);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                //fileanh5
                                if (!string.IsNullOrEmpty(_fileanh5))
                                {
                                    _tenfileanh5 = DungChung.Bien.DuongDan + "\\";
                                    _tenfileanh5 += _mabn + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh5))
                                        {
                                            if (!File.Exists(_tenfileanh5))
                                            {
                                                File.Copy(_fileanh5, _tenfileanh5);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh5 = layTenFileAnh(_fileanh5, _tenfileanh5);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                //fileanh6
                                if (!string.IsNullOrEmpty(_fileanh6))
                                {
                                    _tenfileanh6 = DungChung.Bien.DuongDan + "\\";
                                    _tenfileanh6 += _mabn + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh6))
                                        {
                                            if (!File.Exists(_tenfileanh6))
                                            {
                                                File.Copy(_fileanh6, _tenfileanh6);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh6 = layTenFileAnh(_fileanh6, _tenfileanh6);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                //fileanh7
                                if (!string.IsNullOrEmpty(_fileanh7))
                                {
                                    _tenfileanh7 = DungChung.Bien.DuongDan + "\\";
                                    _tenfileanh7 += _mabn + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh7))
                                        {
                                            if (!File.Exists(_tenfileanh7))
                                            {
                                                File.Copy(_fileanh7, _tenfileanh7);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh7 = layTenFileAnh(_fileanh7, _tenfileanh7);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                //fileanh8
                                if (!string.IsNullOrEmpty(_fileanh8))
                                {
                                    _tenfileanh8 = DungChung.Bien.DuongDan + "\\";
                                    _tenfileanh8 += _mabn + IdCLS + ".jpg";
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(_fileanh8))
                                        {
                                            if (!File.Exists(_tenfileanh8))
                                            {
                                                File.Copy(_fileanh8, _tenfileanh8);
                                            }
                                            else
                                            {
                                                DialogResult _dresult1 = MessageBox.Show("tệp đã có, bạn có muốn lưu đè", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                if (_dresult1 == DialogResult.Yes)
                                                {
                                                    _tenfileanh8 = layTenFileAnh(_fileanh8, _tenfileanh8);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("không lưu được ảnh");
                                    }
                                }
                                //
                                foreach (var a in _CLSct)
                                {
                                    //var ktradv = _ldvu.Where(p => p.MaDV == _madv).Where(p => p.TenDV.ToLower().Contains("siêu âm thai") && p.TenDV.ToLower().Contains("4d")).ToList();
                                    //if (DungChung.Bien.MaBV == "27183" && ktradv.Count > 0)
                                    //{
                                    //    a.KetQua = reKQSieuAm.Text.Replace("\r\n", ";");
                                    //}
                                    //else
                                    a.KetQua = reKQSieuAm.Text;
                                    if ((DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297"))
                                        a.KetQua_Rtf = reKQSieuAm.RtfText;
                                    a.DuongDan = _tenfileanh + ";" + _tenfileanh3 + ";" + _tenfileanh5 + ";" + _tenfileanh7;
                                    a.DuongDan2 = _tenfileanh2 + ";" + _tenfileanh4 + ";" + _tenfileanh6 + ";" + _tenfileanh8;

                                }

                                if (LupCanBo.EditValue != null)
                                    _Cls.First().MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    _Cls.First().MaCBth = "";
                                foreach (var item in _CLSct)
                                {

                                    if (tabCtrl_SADoppler.SelectedTabPageIndex == 1)
                                    {
                                        item.KetQua = (txtSADL_TinhMach1.Text ?? "") + "|" + (txtSADL_TinhMach2.Text ?? "");
                                    }
                                    else if (tabCtrl_SADoppler.SelectedTabPageIndex == 2)
                                    {
                                        item.KetQua = txtKQ_SADoppler.Text;
                                    }
                                    else
                                    {
                                        foreach (var c in _lDoppler)
                                        {
                                            if (item.Id == c.Id)
                                            {
                                                string _ketqua = "";
                                                bool kq = false;
                                                if (!string.IsNullOrEmpty(c.KetQua2) || !string.IsNullOrEmpty(c.KetQua3) || !string.IsNullOrEmpty(c.KetQua4) ||
                                                    !string.IsNullOrEmpty(c.KetQua5) || !string.IsNullOrEmpty(c.KetQua5) || !string.IsNullOrEmpty(c.KetQua6) || !string.IsNullOrEmpty(c.KetQua7))
                                                    kq = true;
                                                if (!string.IsNullOrEmpty(c.KetQua))
                                                {
                                                    _ketqua += c.KetQua;

                                                }
                                                if (!string.IsNullOrEmpty(c.KetQua2))
                                                {
                                                    _ketqua += ";" + c.KetQua2;
                                                }
                                                else
                                                {
                                                    if (kq)
                                                        _ketqua += ";";
                                                }
                                                if (!string.IsNullOrEmpty(c.KetQua3))
                                                {

                                                    _ketqua += ";" + c.KetQua3;
                                                }
                                                else
                                                {
                                                    if (kq)
                                                        _ketqua += ";";
                                                }
                                                if (!string.IsNullOrEmpty(c.KetQua4))
                                                {

                                                    _ketqua += ";" + c.KetQua4;
                                                }
                                                else
                                                {
                                                    if (kq)
                                                        _ketqua += ";";
                                                }
                                                if (!string.IsNullOrEmpty(c.KetQua5))
                                                {

                                                    _ketqua += ";" + c.KetQua5;
                                                }
                                                else
                                                {
                                                    if (kq)
                                                        _ketqua += ";";
                                                }
                                                if (!string.IsNullOrEmpty(c.KetQua6))
                                                {

                                                    _ketqua += ";" + c.KetQua6;
                                                }
                                                else
                                                {
                                                    if (kq)
                                                        _ketqua += ";";
                                                }
                                                if (!string.IsNullOrEmpty(c.KetQua7))
                                                {

                                                    _ketqua += ";" + c.KetQua7;
                                                }
                                                else
                                                {
                                                    if (kq)
                                                        _ketqua += ";";
                                                }
                                                if (!string.IsNullOrEmpty(c.KetQua8))
                                                {

                                                    _ketqua += ";" + c.KetQua8;
                                                }
                                                else
                                                {
                                                    if (kq)
                                                        _ketqua += ";";
                                                }
                                                if (!string.IsNullOrEmpty(c.KetQua9))
                                                {

                                                    _ketqua += ";" + c.KetQua9;
                                                }
                                                else
                                                {
                                                    if (kq)
                                                        _ketqua += ";";
                                                }
                                                if (!string.IsNullOrEmpty(c.KetQua10))
                                                {

                                                    _ketqua += ";" + c.KetQua10;
                                                }
                                                else
                                                {
                                                    if (kq)
                                                        _ketqua += ";";
                                                }

                                                item.KetQua = _ketqua;
                                                item.SoPhieu = c.SoPhieu;
                                                item.Status = c.Status;
                                                item.STTHT = c.STTHT;
                                                break;
                                            }
                                        }
                                    }

                                }
                            }


                            return luuSA;

                        case "tabNSTMH":
                            Boolean luuTMH = true;
                            if (DungChung.Bien.MaBV != "24009" && (string.IsNullOrEmpty(mmeTMH1.Text) || string.IsNullOrEmpty(mmeTMH2.Text) || string.IsNullOrEmpty(mmeTMH3.Text) || string.IsNullOrEmpty(mmeTMH4.Text)
                                || string.IsNullOrEmpty(mmeTMH5.Text) || string.IsNullOrEmpty(mmeTMH6.Text) || string.IsNullOrEmpty(mmeTMH7.Text) || string.IsNullOrEmpty(mmKLTMH.Text) || string.IsNullOrEmpty(memoLoidan_TMH.Text)
                                || LupCanBo.EditValue == null))
                            {
                                DialogResult _dresult4 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult4 == DialogResult.No)
                                {
                                    luuTMH = false;
                                }
                            }
                            if (luuTMH)
                            {
                                foreach (var item in _Chidinh)
                                {
                                    item.KetLuan = mmKLTMH.Text;
                                    item.LoiDan = memoLoidan_TMH.Text;
                                    item.TamThu = 1;
                                    item.Status = 1;
                                    item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                                }
                                if (LupCanBo.EditValue != null)
                                    _Cls.First().MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    _Cls.First().MaCBth = "";

                                foreach (var a in _CLSct)
                                {
                                    string[] arrKQ = new string[14];
                                    arrKQ[0] = mmeTMH1.Text;
                                    arrKQ[1] = mmeTMH2.Text;
                                    arrKQ[2] = mmeTMH3.Text;
                                    arrKQ[3] = mmeTMH4.Text;
                                    arrKQ[4] = mmeTMH5.Text;
                                    arrKQ[5] = mmeTMH6.Text;
                                    arrKQ[6] = mmeTMH7.Text;
                                    //arrKQ[7] = ptTMH1.Text;
                                    a.KetQua = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrKQ);
                                    a.DuongDan2 = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrDuongDan);
                                }
                            }
                            return luuTMH;
                        case "TMH27194":
                            Boolean luuTMH1 = true;
                            if (string.IsNullOrEmpty(mmeTMH11.Text) || string.IsNullOrEmpty(mmeTMH33.Text)
                                || string.IsNullOrEmpty(mmeTMH55.Text) || string.IsNullOrEmpty(mmeTMH66.Text) || string.IsNullOrEmpty(mmKLTMH1.Text) || string.IsNullOrEmpty(memoLoidan_TMH1.Text)
                                || LupCanBo.EditValue == null)
                            {
                                DialogResult _dresult4 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_dresult4 == DialogResult.No)
                                {
                                    luuTMH1 = false;
                                }
                            }
                            if (luuTMH1)
                            {
                                foreach (var item in _Chidinh)
                                {
                                    item.KetLuan = mmKLTMH1.Text;
                                    item.LoiDan = memoLoidan_TMH1.Text;
                                    item.TamThu = 1;
                                    item.Status = 1;
                                    item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                                }
                                if (LupCanBo.EditValue != null)
                                    _Cls.First().MaCBth = LupCanBo.EditValue.ToString();
                                else
                                    _Cls.First().MaCBth = "";

                                foreach (var a in _CLSct)
                                {
                                    string[] arrKQ = new string[7];
                                    arrKQ[0] = mmeTMH11.Text;
                                    arrKQ[1] = mmeTMH55.Text;
                                    arrKQ[2] = mmeTMH33.Text;
                                    arrKQ[3] = mmeTMH4.Text;
                                    arrKQ[4] = " ";
                                    arrKQ[5] = mmeTMH66.Text;
                                    arrKQ[6] = mmeTMH7.Text;
                                    a.KetQua = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrKQ);
                                    a.DuongDan2 = QLBV_Library.QLBV_Ham.LuuChuoi('|', arrDuongDan);
                                }
                            }
                            return luuTMH1;
                        case "tabDoCNHH":
                            if (string.IsNullOrEmpty(mmKetLuan.Text) || LupCanBo.EditValue == null)
                            {
                                bool kt = true;
                                if (DungChung.Bien.MaBV != "24009")
                                {

                                    DialogResult _dresult7 = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_dresult7 == DialogResult.No)
                                        kt = false;
                                }

                                if (kt)
                                {
                                    foreach (var item in _Chidinh)
                                    {
                                        item.KetLuan = memo_KL_DoCN.Text.Trim();
                                        item.LoiDan = memo_LD_DoCN.Text.Trim();
                                        item.TamThu = 1;
                                        item.Status = 1;
                                        item.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                                    }

                                    if (LupCanBo.EditValue != null)
                                        _Cls.First().MaCBth = LupCanBo.EditValue.ToString();
                                    else
                                        _Cls.First().MaCBth = "";
                                    return true;
                                }
                                else
                                    return false;
                            }
                            else

                                return false;
                        default:
                            return false;
                    }
                    return false;
                }
                return false;
            }
            #endregion

            return false;
        }

        public string layTenFileAnh(string fileAnhTMH, string tenfileanh)
        {
            if (!string.IsNullOrEmpty(fileAnhTMH))
            {
                if (!File.Exists(tenfileanh))
                {
                    File.Copy(fileAnhTMH, tenfileanh, true);

                }
                else
                {
                    for (int i = 0; i < 100; i++)
                    {
                        string a = "";
                        string ex = System.IO.Path.GetExtension(tenfileanh);
                        a = tenfileanh.Replace(ex, i + ex);
                        //if(DungChung.Bien.MaBV== "30012")
                        //    a = tenfileanh.Replace(".bmp", i + ".bmp");
                        //else
                        //    a = tenfileanh.Replace(".jpg", i + ".jpg");
                        if (!File.Exists(a))
                        {
                            File.Copy(fileAnhTMH, a, true);
                            tenfileanh = a;
                            break;
                        }
                    }
                }
            }
            return tenfileanh;
        }

        private bool CheckMaxlength(Control control, string name)
        {
            bool rs = true;
            if (!string.IsNullOrEmpty(control.Text) && control.Text.Length > 1000)
            {
                MessageBox.Show(string.Format("{0} có độ dài vượt quá 1000 ({1})", name, control.Text.Length.ToString()));
                return false;
            }
            return rs;
        }

        private bool CheckSave()
        {
            bool rs = true;
            if (!CheckMaxlength(reKQSieuAm, "Kết quả siêu âm"))
                return false;
            if (!CheckMaxlength(mmKQNoisoi, "Kết quả nội soi"))
                return false;
            if (!CheckMaxlength(memoKQ_DienNaoDo, "Kết quả điện não đồ"))
                return false;
            if (!CheckMaxlength(mmKetQua_DoKhucXaMay, "Kết quả đo khúc xạ máy"))
                return false;
            if (!CheckMaxlength(mKetqua, "Kết quả siêu âm"))
                return false;
            return rs;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string tendn = DungChung.Bien.TenDN;

            DateTime _ngayth = Convert.ToDateTime(lupNgayTH.Text);

            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "20001")
            {
                if (!CheckSave())
                    return;
            }

            if (DungChung.Bien.MaBV == "30372")
            {
                var q = (from ad in _data.ADMINs.Where(p => p.TenDN == tendn)
                         join cb in _data.CanBoes on ad.MaCB equals cb.MaCB
                         join kp in _data.KPhongs/*.Where(p => p.PLoai == "Admin") */on cb.MaKP equals kp.MaKP
                         select new { ad.TenDN, ad.MaCB, kp.PLoai }).ToList();//.Distinct().ToList();

                if (q.Count > 0)
                {
                    var ravien = (from rv in _data.RaViens.Where(p => p.MaBNhan == _mabn).Where(p => p.NgayRa > _ngayth)
                                  select new
                                  {
                                      rv.IdRaVien,
                                      rv.NgayRa
                                  }).ToList();
                    var ravien1 = (from rv in _data.RaViens.Where(p => p.MaBNhan == _mabn)
                                   select new
                                   {
                                       rv.IdRaVien,
                                       rv.NgayRa
                                   }).ToList();
                    string s2 = ravien1.Count > 0 ? ravien1.Select(p => p.NgayRa).First().ToString() : "";

                    if (RAD.SelectedIndex == 0)
                    {
                        if (ravien.Count == 0 && ravien1.Count > 0)
                        {
                            MessageBox.Show("Ngày thực hiện không được lớn hơn ra viện\n (Ngày ra viện : " + s2 + " )", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            _HamLuuCDHA();
                        }
                    }
                    else
                    {
                        if (RAD.SelectedIndex == 1)
                        {
                            if (ravien.Count == 0 && ravien1.Count > 0)
                            {
                                MessageBox.Show("Ngày thực hiện không được lớn hơn ngày ra viện \n (Ngày ra viện : " + s2 + " )", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                _HamLuuCDHA();
                            }
                        }
                    }
                }
            }

            else if (DungChung.Bien.MaBV == "27001")//minhvd
            {
                DateTime ngayth = Convert.ToDateTime(lupNgayTH.Text);
                DateTime ngaynow = new DateTime(ngayth.Year, ngayth.Month, ngayth.Day, 0, 0, 0);
                DateTime ngaynow1 = ngaynow.AddDays(1);

                if (LupKhoaphong.EditValue != null)
                {
                    if (LupCanBo.Text != null)
                    {
                        string macb = LupCanBo.EditValue.ToString();
                        var check = (from cls in _data.CLS.Where(p => p.NgayTH > ngaynow && p.NgayTH < ngaynow1 && p.MaCBth == macb)
                                     join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                     join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                                     join tn in _data.TieuNhomDVs.Where(p => p.TenRG == "Siêu âm" || p.TenRG == "Siêu âm ( Doppler )") on dv.IdTieuNhom equals tn.IdTieuNhom
                                     select new { cls, cd, dv, tn }).ToList();
                        if (check.Count > 47)
                        {
                            DialogResult dr = MessageBox.Show("Bác sỹ đã thực hiện > 47ca siêu âm/ngày bạn có muốn tiếp tục không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                _HamLuuCDHA();
                            }
                        }
                        else
                        {
                            _HamLuuCDHA();
                        }
                    }
                    else
                    {
                        _HamLuuCDHA();
                    }
                }
            }

            else
            {
                _HamLuuCDHA();
                //if (DungChung.Bien.MaBV == "01071")
                //{
                //    DialogResult dr = MessageBox.Show("Bạn muốn gửi dữ liệu lên cổng của đối tác RIS/PACS? Thao tác này sẽ cần một chút thời gian", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (dr == DialogResult.Yes)
                //    {
                //        int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                //        DungChung.Ham.SendDataRIS(IdCLS);
                //    }
                //}
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

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            bool tontai = true;
            if (ptSieuam.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;

                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        _fileanh = fileName;
                        if (!string.IsNullOrEmpty(_fileanh))
                            ptSieuam.Image = Image.FromFile(_fileanh);
                        else
                            ptSieuam.Image = null;

                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh = fileName;
                    if (!string.IsNullOrEmpty(_fileanh))
                        ptSieuam.Image = Image.FromFile(_fileanh);
                    else
                        ptSieuam.Image = null;

                }
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi1, 1);
        }

        private void _HamLuuCDHA()
        {
            if (_tamthu == false)
            {
                MessageBox.Show("Bệnh nhân chưa nộp tiền dịch vụ, bạn không thể lưu");
            }

            #region Kiểm tra tạm thu và ra viện của bệnh nhân
            string macbth = LupCanBo.EditValue.ToString();
            if (_tamthu && KT())
            {
                //_Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                string kl = "";
                bool KtraBNKSK = false;
                BenhNhan benhNhan = _data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _mabn);

                foreach (var b in _Chidinh)
                {
                    if (benhNhan != null && string.IsNullOrWhiteSpace(benhNhan.SThe) && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !DungChung.Ham.Check_DuyetTamThu(b.IDCD))
                    {
                        MessageBox.Show("Dịch vụ chưa được duyệt tạm thu không thể thực hiện");
                        return;
                    }
                    int ID = b.IDCD;
                    var suacd = _data.ChiDinhs.Single(p => p.IDCD == ID);
                    suacd.KetLuan = b.KetLuan;
                    kl = b.KetLuan;
                    suacd.LoiDan = b.LoiDan;
                    suacd.GhiChu = b.GhiChu;
                    // suacd.SoPhieu = b.SoPhieu;
                    suacd.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                    suacd.Status = 1;
                    suacd.TamThu = b.TamThu;
                    suacd.MaCBth = b.MaCBth;
                    suacd.NgayTH = lupNgayTH.DateTime;
                    if (lupMaMay.EditValue != null)
                        suacd.MaMay = lupMaMay.EditValue.ToString();
                    suacd.SoPhim = rgsophim.SelectedIndex + 1;
                    //sua DThuocct
                    //var suadtct = _Data.DThuoccts.Single(p => p.IDCD == ID);
                    //suadtct.MaCB = _Cls.First().MaCBth;
                    _data.SaveChanges();
                }
                foreach (var c in _CLSct)
                {
                    var suaclsct = _data.CLScts.Single(p => p.Id == c.Id);
                    var suacd = _data.ChiDinhs.Single(p => p.IDCD == c.IDCD);
                    suaclsct.DuongDan = c.DuongDan;
                    suaclsct.DuongDan2 = c.DuongDan2;
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        suaclsct.KetQua = c.KetQua;
                    }
                    else
                    {
                        suaclsct.KetQua = c.KetQua;
                    }
                    if (DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "24297")
                    {
                        if (c.KetQua_Rtf != null)
                        {
                            suaclsct.KetQua_Rtf = c.KetQua_Rtf;
                        }
                        else
                        {
                            suaclsct.KetQua_Rtf = c.KetQua;
                        }
                        if (DungChung.Bien.MaBV == "24297")
                        {
                            suacd.MoTa += _data.DichVucts.Where(x => x.MaDVct == c.MaDVct).Select(p => p.TenDVct).FirstOrDefault() + ": " + c.KetQua + "\n";
                        }
                    }
                    else if (DungChung.Bien.MaBV == "24272")
                    {
                        suaclsct.KetQua_Rtf = c.KetQua;
                    }
                    else
                    {
                        suaclsct.KetQua_Rtf = c.KetQua;
                    }

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
                    _data.SaveChanges();

                    #region update dienbien 300619
                    if (c.KetQua != "" && c.KetQua != null && DungChung.Bien.MaBV != "34019")
                    {
                        var qcls_db = (from cd in _data.ChiDinhs.Where(p => p.IDCD == c.IDCD)
                                       join cls in _data.CLS on cd.IdCLS equals cls.IdCLS
                                       select cls).FirstOrDefault();
                        if (qcls_db != null && qcls_db.IDDienBien != null && qcls_db.IDDienBien.Value > 0)
                        {
                            var qdb = _data.DienBiens.Where(p => p.ID == qcls_db.IDDienBien).FirstOrDefault();

                            if (qdb != null)
                            {
                                var tendvct = _data.DichVucts.Single(p => p.MaDVct == c.MaDVct);
                                qdb.DienBien1 += Environment.NewLine + "+ " + tendvct.TenDVct + ": " + c.KetQua;
                                _data.SaveChanges();
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
                    var suacls = _data.CLS.Single(p => p.IdCLS == a.IdCLS);
                    makp = a.MaKP == null ? 0 : a.MaKP.Value;
                    suacls.MaCBth = a.MaCBth;
                    suacls.NgayTH = lupNgayTH.DateTime;
                    var ktstatuscd = _data.ChiDinhs.Where(p => p.IdCLS == a.IdCLS).Where(p => p.Status == 0 || p.Status == null).ToList();
                    if (ktstatuscd.Count > 0)
                        suacls.Status = 0;
                    else
                    {
                        suacls.Status = 1;
                        //BenhNhan sua = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                        if (benhNhan != null)
                        {
                            var b = _data.BNKBs.Where(p => p.MaBNhan == _mabn).ToList();
                            var vienphi = _data.VienPhis.Where(p => p.MaBNhan == _mabn).ToList();
                            if (b.Count > 0 && vienphi.Count <= 0 && benhNhan.Status != 2 && benhNhan.Status != 3)
                            {
                                benhNhan.Status = 5;
                            }
                            if (benhNhan.IDDTBN == 3 && DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                KtraBNKSK = true;
                        }
                    }
                    _data.SaveChanges();

                }

                int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                var cdinh = (from cd1 in _data.ChiDinhs.Where(p => p.IdCLS == IdCLS && p.Status == 1)
                             join dv in _data.DichVus on cd1.MaDV equals dv.MaDV
                             select new
                             {
                                 cd1.MaDV,
                                 cd1.SoPhieu,
                                 cd1.DonGia,
                                 cd1.IDCD,
                                 dv.DonVi,
                                 cd1.TrongBH,
                                 cd1.XHH,
                                 cd1.LoaiDV,
                                 cd1.IDGoi
                             }).ToList();

                int iddthuoc = 0;
                //string _mabn = grvBenhnhan.GetFocusedRowCellValue("MaBNhan").ToString();
                int _idkb = 0;
                var bnkb = _data.BNKBs.Where(p => p.MaBNhan == _mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                if (bnkb.Count > 0)
                    _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                var ktdthuoc = _data.DThuocs.Where(p => p.MaBNhan == _mabn).Where(p => p.PLDV == 2).ToList();
                if (ktdthuoc.Count > 0)
                    iddthuoc = ktdthuoc.First().IDDon;
                List<int> dsIDGOiDV = new List<int>();//lấy danh sách những gói đã được thu thẳng trước đó
                if (KtraBNKSK == true)
                {
                    var _lThuTT = _data.TamUngs.Where(p => p.IDGoiDV != null && p.PhanLoai == 3 && p.MaBNhan == _mabn).Select(p => p.IDGoiDV ?? 0).ToList();
                    dsIDGOiDV.AddRange(_lThuTT);
                }
                if (iddthuoc > 0)
                {
                    foreach (var cd2 in cdinh)
                    {
                        var kt = (from dt in _data.DThuoccts.Where(p => p.IDCD == cd2.IDCD) select dt).ToList();
                        if (kt.Count <= 0)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(_data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, string.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd2.MaDV;
                            moi.IDKB = _idkb;
                            moi.IDDon = iddthuoc;
                            moi.DonVi = cd2.DonVi;
                            if (chk_BNKHT.Checked && DungChung.Bien.MaBV == "27023")
                            {
                                moi.TrongBH = 2;
                            }
                            else
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
                            _data.DThuoccts.Add(moi);
                            _data.SaveChanges();
                            var CheckGiaPhuThu = _data.DichVus.FirstOrDefault(p => p.MaDV == cd2.MaDV);
                            var sss = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(_data, moi.IDDonct, s);
                            }
                        }
                        else
                        {
                            foreach (var dt in kt)
                            {
                                if (chk_BNKHT.Checked && DungChung.Bien.MaBV == "27023")
                                {
                                    dt.TrongBH = 2;
                                }
                                else
                                    dt.TrongBH = _Chidinh.First().TrongBH == null ? 0 : _Chidinh.First().TrongBH.Value;
                                if (LupCanBo.EditValue != null)
                                    dt.MaCB = LupCanBo.EditValue.ToString();
                                else
                                    dt.MaCB = "";
                                dt.NgayNhap = lupNgayTH.DateTime;
                                dt.IDCLS = IdCLS;
                            }
                            _data.SaveChanges();
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
                    _data.DThuocs.Add(dthuoccd);
                    if (_data.SaveChanges() >= 0)
                    {
                        int maxid = dthuoccd.IDDon;
                        foreach (var cd3 in cdinh)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(_data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, string.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd3.MaDV;
                            moi.IDDon = maxid;
                            moi.IDKB = _idkb;
                            if (chk_BNKHT.Checked && DungChung.Bien.MaBV == "27023")
                            {
                                moi.TrongBH = 2;
                            }
                            else
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
                            _data.DThuoccts.Add(moi);
                            _data.SaveChanges();
                            var CheckGiaPhuThu = _data.DichVus.Where(p => p.MaDV == cd3.MaDV).FirstOrDefault();
                            var sss = _data.BenhNhans.Where(p => p.MaBNhan == _mabn).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(_data, moi.IDDonct, s);
                            }
                        }
                    }
                }

                if (DungChung.Bien.MaBV == "01071")
                {
                    var tongcp = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn)
                                  join dtct in _data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                  select new { dtct }).ToList();
                    double tongcptrbh = 0;
                    tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                    if (tongcptrbh >= 10000000)
                    {
                        MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vướt quá 10.000.000");
                    }
                }
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    var _status = (from _cls in _data.CLS join cd in _data.ChiDinhs.Where(p => p.IdCLS == IdCLS) on _cls.IdCLS equals cd.IdCLS select new { cd.Status }).ToList();
                    grvketqua.SetRowCellValue(grvketqua.FocusedRowHandle, "Status", _status.First().Status.ToString());
                }

                LoadKetQuaCLS();
                var rowHandleketqua = grvketqua.LocateByValue("id", IdCLS);
                if (rowHandleketqua != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    grvketqua.FocusedRowHandle = rowHandleketqua;
                }
                mmKLTMH1.Properties.ReadOnly = true;
                memoLoidan_TMH1.Properties.ReadOnly = true;
                mmKLTMH.Properties.ReadOnly = true;
                memoLoidan_TMH.Properties.ReadOnly = true;
                reKQSieuAm.ReadOnly = true;
                mmKLSieuam.Properties.ReadOnly = true;
                mmLoidanSieuAm.Properties.ReadOnly = true;
                mmKetQuaXQ.ReadOnly = true;
                memoKetLuanXQ.Properties.ReadOnly = true;
                mmLoidanXQ.Properties.ReadOnly = true;
                mmKetLuanDientim.Properties.ReadOnly = true;
                mmLoidanDientim.Properties.ReadOnly = true;
                mmKQNoisoi.ReadOnly = true;
                mmKLNoisoi.Properties.ReadOnly = true;
                mmLoidanNoisoi.Properties.ReadOnly = true;
                memo_KL_DoCN.Properties.ReadOnly = true;
                memo_LD_DoCN.Properties.ReadOnly = true;
                mmKetLuan.Properties.ReadOnly = true;
                mmLoidan.Properties.ReadOnly = true;
                txtKetLuanLHN.Properties.ReadOnly = true;
                txtLoiDanLHN.Properties.ReadOnly = true;
                mmSA_Doppler.Properties.ReadOnly = true;
                txtSADL_TinhMach1.Properties.ReadOnly = true;
                txtSADL_TinhMach2.Properties.ReadOnly = true;
                txtKQ_SADoppler.Properties.ReadOnly = true;
                GrvKQDienTim.OptionsBehavior.Editable = false;
                GrvDVct.OptionsBehavior.Editable = false;
                grv_DoCN.OptionsBehavior.Editable = false;
                memoKL_DienNaoDo.Properties.ReadOnly = true;
                memoKQ_DienNaoDo.Enabled = false;
                memoLoiDan_DienNaoDo.Properties.ReadOnly = true;
                mKetLuan.ReadOnly = true;
                mKetqua.ReadOnly = true;
                mLoiDanBacSy.ReadOnly = true;
                EnabledControl(true);
                trangthaiLuu = 0;

                if (DungChung.Bien.MaBV.Equals("24012"))
                {
                    //var response = Task.Run(async () => await SendPacs(DungChung.Bien.MaBV, _mabn, IdCLS, _Cls.FirstOrDefault().NgayThang.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss"))).Result;
                    //if (response == null || (response != null && response.ContentSuccess != null && !response.ContentSuccess.Data))
                    //{
                    //    MessageBox.Show("Gửi dữ liệu sang hệ thống PACS không thành công." + response == null ? string.Empty : "\n" + response.ContentSuccess.Message);
                    //}

                    Task.Run(async () => await SendPacs(DungChung.Bien.MaBV, _mabn, IdCLS, _Cls.FirstOrDefault().NgayThang.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss")));
                }
            }

            #endregion
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            trangthaiLuu = 1;
            DateTime ngay1 = Convert.ToDateTime("2018-01-01 00:00:00");
            DateTime ngay2 = Convert.ToDateTime("2018-11-03 23:59:59");
            bool ktra = true;

            if (_CLSct.Count > 0 && _CLSct.First().DuongDan2 != null)
            {
                strDD = _CLSct.First().DuongDan2.ToString(); // Lấy đường dẫn ảnh để sửa ảnh.
                arrDuongDan = QLBV_Library.QLBV_Ham.LayChuoi('|', strDD);
            }

            var vp = (from vpct in _data.VienPhis.Where(p => p.MaBNhan == _mabn)
                      select new
                      {
                          vpct.idVPhi,
                          vpct.NgayTT
                      }).ToList();
            var Tap = (from KP in _data.KPhongs.Where(p => p.MaKP == _maKP)
                       select new
                       {
                           KP.NhomKP,
                           KP.PLoai,
                           KP.TenKP,
                           KP.ChuyenKhoa
                       }).ToList();

            string TenKP = "";
            if (Tap.Count > 0)
                TenKP = Tap.First().ChuyenKhoa;

            string tendn = DungChung.Bien.TenDN;

            #region Nút sửa bệnh nhân BHYT và Dịch vụ (30372 & tài khoản admin)
            if (DungChung.Bien.MaBV == "30372")
            {
                var q = (from ad in _data.ADMINs.Where(p => p.TenDN == tendn)
                         join cb in _data.CanBoes on ad.MaCB equals cb.MaCB
                         join kp in _data.KPhongs.Where(p => p.PLoai == "Admin") on cb.MaKP equals kp.MaKP
                         select new
                         {
                             ad.TenDN,
                             ad.MaCB,
                             kp.PLoai
                         }).Distinct().ToList();

                if (q.Count > 0)
                {
                    int _bnBHYT = _data.BenhNhans.Where(p => p.MaBNhan == _mabn && p.DTuong == "BHYT").Count();
                    var _bnDichvu = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _mabn && p.DTuong == "Dịch vụ")
                                     join cls in _data.CLS on bn.MaBNhan equals cls.MaBNhan
                                     join cd in _data.ChiDinhs.Where(p => p.SoPhieu != null) on cls.IdCLS equals cd.IdCLS
                                     select new
                                     {
                                         bn.MaBNhan,
                                         bn.TenBNhan,
                                         cd.SoPhieu
                                     }).ToList();

                    if (_bnBHYT > 0 || _bnDichvu.Count > 0)
                    {
                        if (_tamthu != false)
                        {
                            ktra = true;
                        }
                        else
                        {
                            ktra = false;
                            MessageBox.Show("Tài khoản của bạn không có quyền hoặc dịch vụ chưa được thu trực tiếp");
                        }
                    }
                    else
                    {
                        ktra = false;
                        MessageBox.Show("Tài khoản của bạn không có quyền hoặc dịch vụ chưa được thu trực tiếp");
                    }
                }
                else
                {
                    if (vp.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã thanh toán không thể sửa!");
                        ktra = false;
                    }
                }

            }
            else
            {
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    var rv = (from rvs in _data.RaViens.Where(p => p.MaBNhan == _mabn) select new { rvs.IdRaVien }).ToList();
                    if (rv.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã ra viện không thể sửa!");
                        ktra = false;
                    }
                }
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
            }
            #endregion

            #region TH khác

            //if (DungChung.Bien.MaBV == "30372" && q.Count > 0)
            //{
            //    if (_tamthu != false)
            //    {
            //        ktra = true;
            //    }
            //    else ktra = false;
            //}
            //else
            //{
            //    if (vp.Count > 0)
            //    {
            //        if (DungChung.Bien.MaBV == "27023" && vp.First().NgayTT >= ngay1 && vp.First().NgayTT <= ngay2 && TenKP == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler)
            //        {
            //            ktra = true;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Bệnh nhân đã thanh toán không thể sửa!");
            //            ktra = false;
            //        }
            //    }
            //}
            #endregion

            if (ktra)
            {
                EnabledControl(false);
                reKQSieuAm.ReadOnly = false;
                mmKLSieuam.Properties.ReadOnly = false;
                mmLoidanSieuAm.Properties.ReadOnly = false;
                mmKetQuaXQ.ReadOnly = false;
                memoKetLuanXQ.Properties.ReadOnly = false;
                mmLoidanXQ.Properties.ReadOnly = false;
                mmKetLuanDientim.Properties.ReadOnly = false;
                mmLoidanDientim.Properties.ReadOnly = false;
                mmKQNoisoi.ReadOnly = false;
                mmKLNoisoi.Properties.ReadOnly = false;
                mmLoidanNoisoi.Properties.ReadOnly = false;
                mmKLTMH.Properties.ReadOnly = false;
                memoLoidan_TMH.Properties.ReadOnly = false;
                mmKLTMH1.Properties.ReadOnly = false;
                memoLoidan_TMH1.Properties.ReadOnly = false;
                mmLoidan.Properties.ReadOnly = false;
                mmKetLuan.Properties.ReadOnly = false;
                mmSA_Doppler.Properties.ReadOnly = false;
                txtSADL_TinhMach1.Properties.ReadOnly = false;
                txtSADL_TinhMach2.Properties.ReadOnly = false;
                txtKQ_SADoppler.Properties.ReadOnly = false;
                txtLoiDanLHN.Properties.ReadOnly = false;
                txtKetLuanLHN.Properties.ReadOnly = false;
                memo_KL_DoCN.Properties.ReadOnly = false;
                memo_LD_DoCN.Properties.ReadOnly = false;
                GrvKQDienTim.OptionsBehavior.Editable = true;
                GrvDVct.OptionsBehavior.Editable = true;
                grv_DoCN.OptionsBehavior.Editable = true;
                memoKL_DienNaoDo.Properties.ReadOnly = false;
                memoKQ_DienNaoDo.Enabled = true;
                memoLoiDan_DienNaoDo.Properties.ReadOnly = false;
                mKetLuan.ReadOnly = false;
                mKetqua.ReadOnly = false;
                mLoiDanBacSy.ReadOnly = false;
                mmKetQua_DoKhucXaMay.ReadOnly = false;
                changeFontNameItem1.Enabled = true;
            }
        }

        public async Task<BaseApiResponse<PacsResultModel, PacsResultModel>> SendPacs(string hospitalCode, int patentId, int orderId, string orderDate, string type = "NW")
        {
            var result = new BaseApiResponse<PacsResultModel, PacsResultModel>();

            try
            {
                string baseUrl = System.Configuration.ConfigurationManager.AppSettings["URL_Send_PACS"];
                if (!string.IsNullOrEmpty(baseUrl))
                {
                    var url = $"api/PACS/UpdateOrderResult?hospitalCode={hospitalCode}&patentId={patentId}&orderId={orderId}&orderDate={orderDate}&type={type}";
                    result = await AppApi.GetAsync<PacsResultModel, PacsResultModel>(baseUrl, url);
                }
            }
            catch (Exception)
            {

            }

            return await Task.FromResult(result);
        }

        public static int _indexPhieu;
        public static string _tendv = "";
        private void simpleButton2_Click(object sender, EventArgs e) // nút In
        {
            DungChung.Bien.CheckInFull = 1;//minhvd 
            DungChung.Bien.MauIn = 1;
            if (string.IsNullOrEmpty(txtMaBN.Text))
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân!");
            }
            else
            {
                if (grvketqua.GetFocusedRowCellValue(TIDCD) != null)
                {
                    bool chedo = false;
                    int _IDCD = Convert.ToInt32(grvketqua.GetFocusedRowCellValue(TIDCD));
                    int IDCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
                    string TenTN = LupKhoaphong.Text;
                    _tendv = Convert.ToString(grvketqua.GetFocusedRowCellValue("tendv"));
                    if (TabKetQua.SelectedTabPage == PageSieuamDoppler)
                    {
                        if (tabCtrl_SADoppler.SelectedTabPageIndex == 1)//in phiếu siêu âm doppler tĩnh mạch
                        {
                            DialogResult _result = MessageBox.Show("In phiếu doppler tĩnh mạch bệnh lý ?", "Chọn mẫu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            int mau = 1;//1: Doppler tĩnh mạch bệnh lý; 2: Doppler tĩnh mạch _BT
                            if (_result == DialogResult.Yes)
                            {
                                mau = 1;
                            }
                            else
                                mau = 2;
                            frmIn frmcd = new frmIn();
                            BaoCao.rep_SADopplerDongTinhMachChiDuoi repcd = new BaoCao.rep_SADopplerDongTinhMachChiDuoi(IDCLS, mau);
                            repcd.BindingData();
                            repcd.CreateDocument();
                            frmcd.prcIN.PrintingSystem = repcd.PrintingSystem;
                            frmcd.ShowDialog();
                            return;
                        }
                        else if (tabCtrl_SADoppler.SelectedTabPageIndex == 2)// in phiếu doppler chung bv trung tâm y tế thành phố bắc ninh
                        {
                            frmIn frmcd = new frmIn();
                            BaoCao.rep_SADopplerChung repcd = new BaoCao.rep_SADopplerChung(IDCLS);
                            repcd.BindingData();
                            repcd.CreateDocument();
                            frmcd.prcIN.PrintingSystem = repcd.PrintingSystem;
                            frmcd.ShowDialog();
                            return;
                        }
                    }
                    _indexPhieu = cbo_ChonIn.SelectedIndex;
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        chedo = true;
                        CLS.InPhieu._inPhieu_CDHA(_data, TenTN, _mabn, IDCLS, _IDCD, chedo, _maKP);
                    }
                    else if (DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "01071")
                        CLS.InPhieu._inPhieu_CDHA_01071_01049(_data, TenTN, _mabn, IDCLS, _IDCD, chedo, _maKP);
                    else
                    {
                        ngaythuchien = lupNgayTH.DateTime;
                        CLS.InPhieu._inPhieu_CDHA(_data, TenTN, _mabn, IDCLS, _IDCD, chedo, _maKP);
                    }

                }
            }
        }

        string[] arrThongTinBNKB;
        string[] Phieu = new string[] { "Siêu âm Doppler động mạch, tĩnh mạch chi dưới", "Siêu âm Doppler tuyến vú", "Siêu âm Doppler tử cung phần phụ", "Siêu âm Doppler u tuyến, hạch vùng cổ", "Siêu âm Dopple tinh hoàn, mào tinh hoàn 2 bên" };
        private class RepPhieuSieuAmDoopler
        {
            public string DiaChiBV { get; set; }
            public string DienThoai { get; set; }
            public string HoVaTen { get; set; }
            public int? Tuoi { get; set; }
            public string GioiTinh { get; set; }
            public string DiaChi { get; set; }
            public string SDT { get; set; }
            public string ChanDoan { get; set; }
            public string ketqua { get; set; }
            public string SDTCanBo { get; set; }
            public string KetLuan { get; set; }

            public Image AnhTuyenVu { get; set; }
            public Image AnhTuCung1 { get; set; }
            public Image AnhTuCung2 { get; set; }
            public Image AnhTuyenGiap1 { get; set; }
            public Image AnhTuyenGiap2 { get; set; }
            public string TimeLocation { get; set; }
            public string BSTH { get; set; }

        }
        private void SbtInMau_Click(object sender, EventArgs e)
        {
            DungChung.Bien.CheckInFull = 3;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string TenDichVu = grvketqua.GetFocusedRowCellValue(tendv).ToString();
            if (string.IsNullOrEmpty(txtMaBN.Text))
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân.!");
            }
            else
            {
                //_Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                string TenKP = LupKhoaphong.Text;
                int _IDCD = 0;
                int IDCLS = 0;
                if (grvketqua.GetFocusedRowCellValue(TIDCD) != null)
                    _IDCD = Convert.ToInt32(grvketqua.GetFocusedRowCellValue(TIDCD));
                if (grvketqua.GetFocusedRowCellValue("id") != null)
                    IDCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
                if (DungChung.Bien.MaBV == "30372" && TenKP.Contains("X-Quang"))
                {
                    DungChung.Bien.MauIn = 2;
                    CLS.InPhieu._inPhieu_CDHA(_data, TenKP, _mabn, IDCLS, _IDCD, false);
                }
                else
                {
                    CLS.InPhieu._inPhieu_CDHA_mau(_data, TenKP, _mabn, IDCLS, _IDCD);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvBenhnhan.GetFocusedRowCellValue("MaBNhan") != null)
            {
                var vp = (from vpct in _data.VienPhis.Where(p => p.MaBNhan == _mabn) select new { vpct.idVPhi }).ToList();
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    var rv = (from rvs in _data.RaViens.Where(p => p.MaBNhan == _mabn) select new { rvs.IdRaVien }).ToList();
                    if (rv.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã ra viện không thể xoá!");
                        return;
                    }
                }
                if (vp.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã thanh toán không thể xoá!");
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                        return;
                }
                else
                {
                    DialogResult dia = MessageBox.Show("Bạn muốn xóa kết quả?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dia == DialogResult.Yes)
                    {
                        int _maCK = 0;
                        var ck = (from nhom in _data.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                                  join dvu in _data.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                                  select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).ToList();
                        if (ck.Count > 0)
                            _maCK = ck.First().MaDV;

                        foreach (var b in _Chidinh)
                        {
                            int ID = b.IDCD;
                            var iddt = _data.DThuoccts.Where(p => p.IDCD == ID && p.MaDV != _maCK).ToList();
                            if (iddt.Count > 0)
                            {
                                foreach (var item in iddt)
                                {
                                    int iddtct = item.IDDonct;
                                    var ktchiphi = _data.DThuoccts.Where(p => p.AttachIDDonct == iddtct).ToList();
                                    if (ktchiphi.Count > 0)
                                    {
                                        MessageBox.Show("dịch vụ đã có chi phí đính kèm, bạn không thế xóa");
                                        return;
                                    }
                                    var xoa = _data.DThuoccts.Single(p => p.IDDonct == iddtct);
                                    _data.DThuoccts.Remove(xoa);
                                    _data.SaveChanges();
                                }
                            }

                            var suacd = _data.ChiDinhs.Single(p => p.IDCD == ID);
                            suacd.NgayTH = null;
                            suacd.KetLuan = "";
                            suacd.LoiDan = "";
                            suacd.MoTa = "";
                            suacd.MaMay = "";
                            suacd.GhiChu = "";
                            //suacd.SoPhieu = 0;
                            suacd.Status = 0;
                            //suacd.TamThu = 1;
                            _data.SaveChanges();
                        }
                        foreach (var c in _CLSct)
                        {
                            var suaclsct = _data.CLScts.Single(p => p.Id == c.Id);
                            suaclsct.DuongDan = "";
                            suaclsct.DuongDan2 = "";
                            suaclsct.KetQua = "";
                            suaclsct.KetQua_Rtf = "";
                            suaclsct.KQDuKien = "";
                            suaclsct.KQTyLe = "";
                            //suaclsct.MaCB = "";
                            //suaclsct.Ngaythang = null;
                            suaclsct.SoPhieu = 0;
                            suaclsct.Status = 0;
                            //suaclsct.STTHT = 0;
                            _data.SaveChanges();
                        }
                        foreach (var a in _Cls)
                        {
                            var suacls = _data.CLS.Single(p => p.IdCLS == a.IdCLS);
                            suacls.MaCBth = "";
                            suacls.Status = 0;
                            suacls.NgayTH = null;
                            suacls.GhiChu = "";
                            _data.SaveChanges();
                        }
                        FRM_chidinh_Moi._setStatus(_mabn);
                        MessageBox.Show("Xoá thành công!");
                        LoadKetQuaCLS();
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

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            //FormThamSo.Frm_NhapHCVTYT frm = new Frm_NhapHCVTYT(txtMaBN.Text, _maKP, lupNgayTH.DateTime);
            //frm.ShowDialog();
            int _int_maBN = 0;
            Int32.TryParse(txtMaBN.Text, out _int_maBN);
            int idcd = 0;
            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null)
                idcd = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("TIDCD"));
            int makp = 0;
            if (LupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(LupKhoaphong.EditValue);
            QLBV.FormNhap.frm_kedon frm = new QLBV.FormNhap.frm_kedon(_int_maBN, idcd, makp, false);
            frm.ShowDialog();
        }
        int _madv = 0;
        string _gt1 = string.Empty;
        public void _getValuesa(string gt1, string gt2, string gt3)
        {
            string TT = TabKetQua.SelectedTabPage.Name;
            switch (TT)
            {
                case "PageDientim":
                    mmKetLuanDientim.Text = gt2;
                    mmLoidanDientim.Text = gt3;

                    string[] kqdt = new string[12];
                    kqdt = QLBV_Library.QLBV_Ham.LayChuoi(';', gt1);

                    for (int i = 0; i < GrvKQDienTim.RowCount; i++)
                    {
                        GrvKQDienTim.SetRowCellValue(i, KetQua, kqdt[i]);
                    }

                    break;
                case "PageXquang":
                    mmKetQuaXQ.Text = gt1;
                    memoKetLuanXQ.Text = gt2;
                    mmLoidanXQ.Text = gt3;
                    break;
                case "tabDienNaoDo":
                    memoKQ_DienNaoDo.Text = gt1;
                    memoKL_DienNaoDo.Text = gt2;
                    memoLoiDan_DienNaoDo.Text = gt3;
                    break;
                case "PageSieuam":
                    reKQSieuAm.Text = gt1;
                    mmKLSieuam.Text = gt2;
                    mmLoidanSieuAm.Text = gt3;
                    break;
                case "tabTracNghiemTamLy":
                    mmKetQua_DoKhucXaMay.Text = gt1;
                    break;
                // ((Control)this.tabTracNghiemTamLy).Enabled = false;
                case "PageNoisoi":
                    mmKQNoisoi.Text = gt1;
                    mmKLNoisoi.Text = gt2;
                    mmLoidanNoisoi.Text = gt3;
                    break;
                case "PageSieuamDoppler":
                    string[] arrKQSADL = QLBV_Library.QLBV_Ham.LayChuoi('|', gt1);
                    mmKetLuan.Text = gt2;
                    mmLoidan.Text = gt3;
                    mmSA_Doppler.Text = "";
                    txtSADL_TinhMach1.Text = arrKQSADL[0];
                    txtSADL_TinhMach2.Text = arrKQSADL[1];
                    txtKQ_SADoppler.Text = gt1;
                    break;
                case "tabNSTMH":
                    string kq = gt1;
                    string[] arrKQ = QLBV_Library.QLBV_Ham.LayChuoi('|', kq);
                    mmeTMH1.Text = arrKQ[0];
                    mmeTMH2.Text = arrKQ[1];
                    mmeTMH3.Text = arrKQ[2];
                    mmeTMH4.Text = arrKQ[3];
                    mmeTMH5.Text = arrKQ[4];
                    mmeTMH6.Text = arrKQ[5];
                    mmeTMH7.Text = arrKQ[6];
                    ptTMH1.Image = arrKQ[7] == null || arrKQ[7] == "" ? null : Image.FromFile(arrKQ[7]);
                    arrDuongDan[0] = arrKQ[7];
                    ptTMH2.Image = arrKQ[8] == null || arrKQ[8] == "" ? null : Image.FromFile(arrKQ[8]);
                    arrDuongDan[1] = arrKQ[8];
                    ptTMH3.Image = arrKQ[9] == null || arrKQ[9] == "" ? null : Image.FromFile(arrKQ[9]);
                    arrDuongDan[2] = arrKQ[9];
                    ptTMH4.Image = arrKQ[10] == null || arrKQ[10] == "" ? null : Image.FromFile(arrKQ[10]);
                    arrDuongDan[3] = arrKQ[10];
                    ptTMH5.Image = arrKQ[11] == null || arrKQ[11] == "" ? null : Image.FromFile(arrKQ[11]);
                    arrDuongDan[4] = arrKQ[11];
                    ptTMH6.Image = arrKQ[12] == null || arrKQ[12] == "" ? null : Image.FromFile(arrKQ[12]);
                    arrDuongDan[5] = arrKQ[12];
                    ptTMH7.Image = arrKQ[13] == null || arrKQ[13] == "" ? null : Image.FromFile(arrKQ[13]);
                    arrDuongDan[6] = arrKQ[13];
                    mmKLTMH.Text = gt2;
                    memoLoidan_TMH.Text = gt3;
                    break;
                case "TMH27194":
                    string kq1 = gt1;
                    string[] arrKQ1 = QLBV_Library.QLBV_Ham.LayChuoi('|', kq1);
                    mmeTMH11.Text = arrKQ1[0];
                    mmeTMH2.Text = arrKQ1[1];
                    mmeTMH33.Text = arrKQ1[2];
                    mmeTMH4.Text = arrKQ1[3];
                    mmeTMH55.Text = arrKQ1[4];
                    mmeTMH66.Text = arrKQ1[5];
                    mmeTMH7.Text = arrKQ1[6];
                    mmKLTMH1.Text = gt2;
                    memoLoidan_TMH1.Text = gt3;
                    break;
                case "xTabCDHA30372":
                    mKetqua.Text = gt1;
                    mKetLuan.Text = gt2;
                    mLoiDanBacSy.Text = gt3;
                    break;
            }
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DungChung.Bien.CheckInFull = 4;
            string TT = TabKetQua.SelectedTabPage.Name;
            if (TT == ("tabNSTMH"))
            {
                ChucNang.frm_KQmau frm = new ChucNang.frm_KQmau(_madv, "tabNSTMH");
                frm.GetData = new ChucNang.frm_KQmau._getstring(_getValuesa);
                frm.ShowDialog();
            }
            //if (TT == ("PageDientim"))
            //{
            //ChucNang.frm_KQmau frm = new ChucNang.frm_KQmau(_madv);
            //frm.GetData = new ChucNang.frm_KQmau._getstring(_getValuesa);
            //frm.ShowDialog();
            //}
            else
            {
                ChucNang.frm_KQmau frm = new ChucNang.frm_KQmau(_madv);
                frm.GetData = new ChucNang.frm_KQmau._getstring(_getValuesa);
                frm.ShowDialog();
            }


        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            //bool tontai = true;
            //if (ptNoisoi2.Image == null)
            //{
            //    tontai = false;
            //}
            //if (tontai)
            //{
            //    DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (_dresult == DialogResult.Yes)
            //    {
            //        string fileName = string.Empty;
            //        OpenFileDialog op = new OpenFileDialog();
            //        op.Multiselect = false;
            //        op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
            //        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            fileName = op.FileName;
            //            _fileanh2 = fileName;
            //            if (!string.IsNullOrEmpty(_fileanh2))
            //                ptNoisoi2.Image = Image.FromFile(_fileanh2);
            //            else
            //                ptNoisoi2.Image = null;
            //            suaanhNoiSoi2 = true;
            //        }
            //    }

            //}
            //else
            //{
            //    string fileName = string.Empty;
            //    OpenFileDialog op = new OpenFileDialog();
            //    op.Multiselect = false;
            //    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
            //    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        fileName = op.FileName;
            //        _fileanh2 = fileName;
            //        if (!string.IsNullOrEmpty(_fileanh2))
            //            ptNoisoi2.Image = Image.FromFile(_fileanh2);
            //        else
            //            ptNoisoi2.Image = null;
            //        suaanhNoiSoi2 = true;
            //    }
            //}
            chonAnhNoiSoi(ptNoisoi2, 2);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi1, _CLSct, 1);
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi2, _CLSct, 2);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            _fileanh = "";
            ptSieuam.Image = null;

        }

        private void txttimten_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void sbtChon1_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH1, 1);
        }


        //private string chonAnh(PictureEdit pt)
        //{
        //    string fileName = string.Empty;
        //    OpenFileDialog op = new OpenFileDialog();
        //    op.Multiselect = false;
        //    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
        //    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        fileName = op.FileName;
        //        _fileanh2 = fileName;
        //        if (!string.IsNullOrEmpty(_fileanh2))
        //            pt.Image = Image.FromFile(_fileanh2);
        //        else
        //            pt.Image = null;
        //    }
        //    return fileName;
        //}

        private void chonAnh(PictureEdit pt, int i)
        {
            bool tontai = true;
            if (DungChung.Bien.MaBV == "30372")
            {
                tontai = false;
            }
            else
            {
                switch (i)
                {
                    case 1:
                        if (ptTMH1.Image == null)
                            tontai = false;
                        if (DungChung.Bien.MaBV == "27194")
                        {
                            if (ptTMH11.Image == null)
                                tontai = false;
                        }
                        break;
                    case 2:
                        if (ptTMH2.Image == null)
                            tontai = false;
                        break;
                    case 3:
                        if (ptTMH3.Image == null)
                            tontai = false;
                        if (DungChung.Bien.MaBV == "27194")
                        {
                            if (ptTMH33.Image == null)
                                tontai = false;
                        }
                        break;
                    case 4:
                        if (ptTMH4.Image == null)
                            tontai = false;
                        break;
                    case 5:
                        if (ptTMH5.Image == null)
                            tontai = false;
                        if (DungChung.Bien.MaBV == "27194")
                        {
                            if (ptTMH55.Image == null)
                                tontai = false;
                        }
                        break;
                    case 6:
                        if (ptTMH6.Image == null)
                            tontai = false;
                        if (DungChung.Bien.MaBV == "27194")
                        {
                            if (ptTMH66.Image == null)
                                tontai = false;
                        }
                        break;
                    case 7:
                        if (ptTMH7.Image == null)
                            tontai = false;

                        break;
                    default:
                        break;
                }
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;
                    if (DungChung.Bien.MaBV == "30012")
                        op.Filter = "BMP(*.bmp)| *.bmp|JPEG (*.jpg)|*.jpg";
                    else
                        op.Filter = "JPEG (*.jpg)| *.jpg|BMP(*.bmp)| *.bmp";
                    int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                    if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = System.IO.Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;
                            string _tenfileanh = DungChung.Bien.DuongDan;
                            //dung310516
                            //if(DungChung.Bien.MaBV == "30012")
                            //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".bmp";
                            //else
                            //     _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";

                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;

                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                    if (trangthaiLuu == 1) // Nếu là sửa ảnh
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = System.IO.Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;
                            //dung310516
                            string _tenfileanh = DungChung.Bien.DuongDan;
                            //if (DungChung.Bien.MaBV == "30012")
                            //     _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".bmp";
                            //else
                            //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                if (DungChung.Bien.MaBV == "30012")
                    op.Filter = "BMP(*.bmp)| *.bmp|JPEG (*.jpg)|*.jpg";// "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                else
                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = System.IO.Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;
                        string _tenfileanh = DungChung.Bien.DuongDan;
                        //dung310516
                        //if(DungChung.Bien.MaBV == "30012")
                        //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".bmp";
                        //else
                        //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }
                }
                if (trangthaiLuu == 1) // Nếu là sửa ảnh
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = System.IO.Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;

                        string _tenfileanh = DungChung.Bien.DuongDan;
                        //dung310516
                        //if (DungChung.Bien.MaBV == "30012")
                        //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".bmp";
                        //else
                        //    _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }
                }
            }
        }

        private void chonAnhNoiSoi(PictureEdit pt, int i)
        {
            bool tontai = true;
            switch (i)
            {
                case 1:
                    if (ptNoisoi1.Image == null)
                        tontai = false;
                    break;
                case 2:
                    if (ptNoisoi2.Image == null)
                        tontai = false;
                    break;
                case 3:
                    if (ptNoisoi3.Image == null)
                        tontai = false;
                    break;
                case 4:
                    if (ptNoisoi4.Image == null)
                        tontai = false;
                    break;
                default:
                    break;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;
                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                    int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                    if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = System.IO.Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;
                            string _tenfileanh = DungChung.Bien.DuongDan;
                            // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                    if (trangthaiLuu == 1) // Nếu là sửa ảnh
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = System.IO.Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;

                            string _tenfileanh = DungChung.Bien.DuongDan;
                            // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = System.IO.Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;
                        string _tenfileanh = DungChung.Bien.DuongDan;
                        //  _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }
                }
                if (trangthaiLuu == 1) // Nếu là sửa ảnh
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = System.IO.Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;

                        string _tenfileanh = DungChung.Bien.DuongDan;
                        // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }
                }
            }
        }

        private void ChonanhXquang(PictureEdit pt, int i)
        {
            bool tontai = true;
            switch (i)
            {
                case 1:
                    if (ptxquang1.Image == null)
                        tontai = false;
                    break;
                case 2:
                    if (ptxquang2.Image == null)
                        tontai = false;
                    break;
                case 3:
                    if (ptxquang3.Image == null)
                        tontai = false;
                    break;
                case 4:
                    if (ptxquang4.Image == null)
                        tontai = false;
                    break;
                default:
                    break;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;
                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                    int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                    if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = System.IO.Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;
                            string _tenfileanh = DungChung.Bien.DuongDan;
                            // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                    if (trangthaiLuu == 1) // Nếu là sửa ảnh
                    {
                        if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            fileName = op.FileName;
                            string ex = System.IO.Path.GetExtension(op.FileName);
                            if (!string.IsNullOrEmpty(fileName))
                                pt.Image = Image.FromFile(fileName);
                            else
                                pt.Image = null;

                            string _tenfileanh = DungChung.Bien.DuongDan;
                            // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                            _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                            arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                        }
                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp";
                int IdCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id").ToString());
                if (trangthaiLuu == 0)//Nếu là thêm mới ảnh.
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = System.IO.Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;
                        string _tenfileanh = DungChung.Bien.DuongDan;
                        //  _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }
                }
                if (trangthaiLuu == 1) // Nếu là sửa ảnh
                {
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        string ex = System.IO.Path.GetExtension(op.FileName);
                        if (!string.IsNullOrEmpty(fileName))
                            pt.Image = Image.FromFile(fileName);
                        else
                            pt.Image = null;

                        string _tenfileanh = DungChung.Bien.DuongDan;
                        // _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ".jpg";
                        _tenfileanh += _mabn + "_" + IdCLS + "_" + i + ex;
                        arrDuongDan[i - 1] = layTenFileAnh(fileName, _tenfileanh);
                    }
                }
            }
        }

        private void xoaAnh(PictureEdit pt, List<CLSct> _lCLSCT, int i)
        {
            i = i - 1;
            if (trangthaiLuu == 0)
            {
                pt.Image = null;
            }
            if (trangthaiLuu == 1)
            {
                arrDuongDan[i] = "";
                pt.Image = null;
            }

        }

        private void sbtChon2_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH2, 2);
        }

        private void sbtChon3_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH3, 3);
        }

        private void sbtChon4_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH4, 4);
        }

        private void sbtChon6_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH6, 6);
        }

        private void sbtChon7_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH7, 7);
        }

        private void sbtXoa1_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH1, _CLSct, 1);
        }

        private void sbtXoa2_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH2, _CLSct, 2);
        }

        private void sbtXoa3_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH3, _CLSct, 3);
        }

        private void sbtXoa4_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH4, _CLSct, 4);
        }

        private void sbtXoa5_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH5, _CLSct, 5);
        }

        private void sbtXoa6_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH6, _CLSct, 6);
        }

        private void sbtXoa7_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH7, _CLSct, 7);
        }

        private void mmKQTMH_EditValueChanged(object sender, EventArgs e)
        {
            lupNgayTH.DateTime = DateTime.Now;
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            DSBN();
        }

        private void sbtChon5_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH5, 5);
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

        private void reKQSieuAm_MouseLeave(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
            {
                string str = QLBV_Library.QLBV_Ham.convertHTML(reKQSieuAm.Text, "red", "blue", '|', ':', "no");
                reKQSieuAm.HtmlText = str;
            }
        }

        private void btnChonAnhNS4_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi4, 4);
        }

        private void btnChonAnhNS3_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi3, 3);
        }

        private void btnXoaAnhNS3_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi3, _CLSct, 3);
        }

        private void btnXoaAnhNS4_Click(object sender, EventArgs e)
        {
            xoaAnh(ptNoisoi4, _CLSct, 4);
        }

        private void sbtChonanhSA2_Click(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (ptSieuam2.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh2 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh2))
                        ptSieuam2.Image = Image.FromFile(_fileanh2);
                    else
                        ptSieuam2.Image = null;

                }
            }
        }

        private void sbtXoaanhSA2_Click(object sender, EventArgs e)
        {


        }

        private void mmeTMH5_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void mmKetQuaXQ_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void mmeTMH3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnInPhieuFull_Click(object sender, EventArgs e)
        {
            DungChung.Bien.CheckInFull = 2;
            DungChung.Bien.MauIn = 3; // 30372
            bool _InPhieu0 = DungChung.Bien._Visible_CDHA[0];
            bool _InPhieu1 = DungChung.Bien._Visible_CDHA[1];
            DungChung.Bien._Visible_CDHA[0] = true;
            DungChung.Bien._Visible_CDHA[1] = true;
            DungChung.Bien._Visible_CDHA[2] = true;
            if (string.IsNullOrEmpty(txtMaBN.Text))
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân!");
            }
            else
            {
                if (grvketqua.GetFocusedRowCellValue(TIDCD) != null)
                {
                    bool chedo = false;
                    int _IDCD = Convert.ToInt32(grvketqua.GetFocusedRowCellValue(TIDCD));
                    int IDCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
                    string TenTN = LupKhoaphong.Text;
                    _tendv = Convert.ToString(grvketqua.GetFocusedRowCellValue("tendv"));
                    if (TabKetQua.SelectedTabPage == PageSieuamDoppler)
                    {
                        if (tabCtrl_SADoppler.SelectedTabPageIndex == 1)//in phiếu siêu âm doppler tĩnh mạch
                        {
                            DialogResult _result = MessageBox.Show("In phiếu doppler tĩnh mạch bệnh lý ?", "Chọn mẫu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            int mau = 1;//1: Doppler tĩnh mạch bệnh lý; 2: Doppler tĩnh mạch _BT
                            if (_result == DialogResult.Yes)
                            {
                                mau = 1;
                            }
                            else
                                mau = 2;
                            frmIn frmcd = new frmIn();
                            BaoCao.rep_SADopplerDongTinhMachChiDuoi repcd = new BaoCao.rep_SADopplerDongTinhMachChiDuoi(IDCLS, mau);
                            repcd.BindingData();
                            repcd.CreateDocument();
                            frmcd.prcIN.PrintingSystem = repcd.PrintingSystem;
                            frmcd.ShowDialog();
                            return;
                        }
                        else if (tabCtrl_SADoppler.SelectedTabPageIndex == 2)// in phiếu doppler chung bv trung tâm y tế thành phố bắc ninh
                        {
                            frmIn frmcd = new frmIn();
                            BaoCao.rep_SADopplerChung repcd = new BaoCao.rep_SADopplerChung(IDCLS);
                            repcd.BindingData();
                            repcd.CreateDocument();
                            frmcd.prcIN.PrintingSystem = repcd.PrintingSystem;
                            frmcd.ShowDialog();
                            return;
                        }
                    }
                    _indexPhieu = cbo_ChonIn.SelectedIndex;
                    CLS.InPhieu._inPhieu_CDHA_InFull(_data, TenTN, _mabn, IDCLS, _IDCD, chedo, _maKP);

                }
            }
            DungChung.Bien._Visible_CDHA[0] = _InPhieu0;
            DungChung.Bien._Visible_CDHA[1] = _InPhieu1;
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

        private void cbo_ChonIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _IDCD = 0;
            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null)
                _IDCD = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("TIDCD"));
            if (cbo_ChonIn.SelectedIndex >= 0 && cbo_ChonIn.SelectedIndex < 3)
            {
                //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int mauin = -1;
                string TenTN = LupKhoaphong.Text;
                mauin = cbo_ChonIn.SelectedIndex;
                CLS.InPhieu.InLuuHuyetNao(_data, _IDCD, TenTN, mauin);
                cbo_ChonIn.SelectedIndex = -1;
                return;
            }
            // đo nhịp tim
            if (cbo_ChonIn.SelectedIndex == 3)
            {
                CLS.InPhieu.InPhieuThamDoChucNang(_IDCD, 2, DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DoMatDoXuong);
                cbo_ChonIn.SelectedIndex = -1;
                return;
            }
            if (cbo_ChonIn.SelectedIndex == 4)
            {

                CLS.InPhieu.PhieuSieuAmMau4D(_IDCD, 0);
                cbo_ChonIn.SelectedIndex = -1;
                return;
            }
            if (cbo_ChonIn.SelectedIndex == 5)
            {

                CLS.InPhieu.PhieuChupCatLopViTinh(_IDCD);
                cbo_ChonIn.SelectedIndex = -1;
                return;
            }
            if (cbo_ChonIn.SelectedIndex == 6 || cbo_ChonIn.SelectedIndex == 15) // 30372 phiếu ns dạ dày giống phiếu SA tuyến zú (selecetedIndex = 15)
            {
                if (string.IsNullOrEmpty(txtMaBN.Text))
                {
                    MessageBox.Show("Bạn chưa chọn bệnh nhân!");
                }
                else
                {
                    if (grvketqua.GetFocusedRowCellValue(TIDCD) != null)
                    {
                        bool chedo = false;
                        _IDCD = Convert.ToInt32(grvketqua.GetFocusedRowCellValue(TIDCD));
                        int IDCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
                        string TenTN = LupKhoaphong.Text;
                        _tendv = Convert.ToString(grvketqua.GetFocusedRowCellValue("tendv"));
                        _indexPhieu = cbo_ChonIn.SelectedIndex;
                        CLS.InPhieu._inPhieu_CDHA(_data, TenTN, _mabn, IDCLS, _IDCD, chedo);
                        cbo_ChonIn.SelectedIndex = -1;
                        return;
                    }
                }
            }
            if (cbo_ChonIn.SelectedItem != null && (cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm ổ bụng 4D (Nam Thăng Long)"
                  || cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm thai theo tuổi thai 4D (Nam Thăng Long)"
                  || cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm tuyến nước bọt (Nam Thăng Long)"
                  || cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm tuyến giáp (Nam Thăng Long)"
                  || cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm tuyến vú (Nam Thăng Long)"
                  || cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm khớp (Nam Thăng Long)"
                  || cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm tinh hoàn 2d (Nam Thăng Long)"
                  || cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm doppler mạch gan (Nam Thăng Long)"
                  || cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm doppler mạch chủ bụng (Nam Thăng Long)"
                  || cbo_ChonIn.SelectedItem.ToString() == "Phiếu siêu âm doppler mạch chi trên(Nam Thăng Long)"))
            {
                int madv = 0;
                _IDCD = Convert.ToInt32(grvketqua.GetFocusedRowCellValue(TIDCD));
                int IDCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
                string TenTN = LupKhoaphong.Text;
                _tendv = Convert.ToString(grvketqua.GetFocusedRowCellValue("tendv"));

                var par0 = (from bn in _data.BenhNhans.Where(o => o.MaBNhan == _mabn)
                            join cls in _data.CLS.Where(p => (_IDCD > 0 ? true : p.IdCLS == IDCLS)) on bn.MaBNhan equals cls.MaBNhan
                            join chidinh in _data.ChiDinhs.Where(p => _IDCD > 0 ? p.IDCD == _IDCD : true) on cls.IdCLS equals chidinh.IdCLS
                            join clsct in _data.CLScts on chidinh.IDCD equals clsct.IDCD
                            join dvct in _data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                            join dv in _data.DichVus on dvct.MaDV equals dv.MaDV
                            select new
                            {
                                dv.MaDV,
                                cls.IdCLS,
                                cls.ChanDoan,
                                cls.ChanDoanYHCT,
                                cls.MaICD,
                                chidinh.IDCD,
                                chidinh.Status,
                                cls.MaCBth,// TenCBth = kq1 == null ? "" : kq1.TenCB,
                                dv.MaQD,
                                dv.TenRG,
                                dvct.STT_F,
                                dvct.STT,
                                dvct.TSBT,
                                cls.NgayTH,
                                clsct.KetQua,
                                clsct.DuongDan,
                                cls.NgayThang,
                                bn.DTuong,
                                bn.MaBNhan,
                                bn.TenBNhan,
                                bn.DChi,
                                bn.GTinh,
                                bn.NoiTru,
                                bn.Tuoi,
                                bn.SThe,
                                cls.MaCB,
                                chidinh.ChiDinh1,
                                chidinh.KetLuan,
                                TenDV = dv.TenDV,
                                chidinh.LoiDan,
                                clsct.DuongDan2,
                                cls.CapCuu,
                                clsct.KQDuKien,
                                clsct.KQTyLe,
                                MaKP = cls.MaKP,
                                MaKPth = cls.MaKPth,
                                bn.NgaySinh,
                                bn.ThangSinh,
                                bn.NamSinh,
                                bn.HanBHDen,
                                bn.HanBHTu,
                                dv.IdTieuNhom
                            }).OrderBy(p => p.STT_F);//.ToList();
                var par = (from a in par0
                               //join cb in _Data.CanBoes on a.MaCB equals cb.MaCB
                           join cbth in _data.CanBoes on a.MaCBth equals cbth.MaCB into kq
                           join kp in _data.KPhongs on a.MaKP equals kp.MaKP
                           join kpTH in _data.KPhongs on a.MaKPth equals kpTH.MaKP
                           //join tn in _Data.TieuNhomDVs on a.IdTieuNhom equals tn.IdTieuNhom
                           from kq1 in kq.DefaultIfEmpty()
                           select new
                           {
                               a.MaDV,
                               kq1.CapBac,
                               a.IdCLS,
                               a.NoiTru,
                               a.DuongDan2,
                               a.ChanDoan,
                               a.MaICD,
                               a.IDCD,
                               a.Status,
                               a.MaCBth,
                               //TenCBth = kq1.TenCB,
                               a.MaQD,
                               a.STT,
                               a.TSBT,
                               a.NgayTH,
                               a.KetQua,
                               a.DuongDan,
                               a.NgayThang,
                               a.MaBNhan,
                               a.TenBNhan,
                               a.DChi,
                               a.GTinh,
                               a.Tuoi,
                               a.SThe,
                               a.MaCB,
                               a.DTuong,
                               kq1.TenCB,
                               kq1.ChucVu,
                               a.ChiDinh1,
                               a.KetLuan,
                               a.TenDV,
                               a.LoiDan,
                               a.CapCuu,
                               a.TenRG,
                               a.KQDuKien,
                               a.KQTyLe,
                               a.ChanDoanYHCT,
                               a.MaKP,
                               kp.TenKP,
                               a.MaKPth,

                               a.NgaySinh,
                               a.ThangSinh,
                               a.NamSinh,
                               a.HanBHDen,
                               a.HanBHTu,
                               //tn.TenTN
                           }).ToList();


                var getKPTH = (from a in _data.CLS.Where(p => p.MaBNhan == _mabn)
                               join b in _data.KPhongs on a.MaKPth equals b.MaKP
                               select new { b.TenKP });
                var ttbx = _data.TTboXungs.Where(p => p.MaBNhan == _mabn).ToList();

                int _makp = 0;

                if (par.Count > 0)
                {
                    _makp = par.First().MaKP == null ? 0 : par.First().MaKP.Value;
                    if (DungChung.Bien._Visible_CDHA[1] == false)
                    {
                        if (par.First().Status == 1)
                            DungChung.Bien._Visible_CDHA[2] = true;
                        else
                            DungChung.Bien._Visible_CDHA[2] = false;
                    }
                }
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    Dictionary<string, object> _dic = new Dictionary<string, object>();

                    _dic.Add("Bacsy", par.First().CapBac + " " + par.First().TenCB);
                    if (par.First().NgayTH != null)
                    {
                        _dic.Add("Ngaythang", "Hà Nội" + ", " + " ngày " + par.First().NgayTH.Value.Day + " tháng " + par.First().NgayTH.Value.Month + " năm " + par.First().NgayTH.Value.Year);
                    }
                    else
                    {
                        _dic.Add("Ngaythang", "");
                    }

                    if (File.Exists(par.First().DuongDan) && File.Exists(par.First().DuongDan2))
                    {
                        _dic.Add("Anh1", Image.FromFile(par.First().DuongDan));
                        _dic.Add("Anh2", Image.FromFile(par.First().DuongDan2));
                    }
                    else
                    {
                        _dic.Add("Anh1", null);
                        _dic.Add("Anh2", null);
                    }
                    _dic.Add("sdt", ttbx.First().DThoai != null ? ttbx.First().DThoai : "");
                    string gioitinh = "";
                    if (par.First().GTinh == 1)
                    {
                        gioitinh = "Nam";
                        _dic.Add("Sinhnam", par.First().Tuoi);
                    }
                    if (par.First().GTinh == 0)
                    {
                        gioitinh = "Nữ";
                        _dic.Add("Sinhnam", par.First().NamSinh);
                    }
                    _dic.Add("Gioitinh", gioitinh);
                    if (par.First().NoiTru == 1)
                    {
                        var sovv = _data.VaoViens.Where(p => p.MaBNhan == _mabn).Select(p => p.SoVV).FirstOrDefault();
                        if (sovv != null)
                            _dic.Add("sovv", "Số VV: " + sovv.ToString());
                    }
                    _dic.Add("thuong2", (par.First().CapCuu == false) ? "X" : "");
                    _dic.Add("capcuu2", (par.First().CapCuu == true) ? "X" : "");

                    _dic.Add("MaCBDT", par.First().MaCB);
                    _dic.Add("DiaChi", par.First().DChi);
                    _dic.Add("TuoiBN", DungChung.Ham.TuoitheoThang(_data, _mabn, DungChung.Bien.formatAge));

                    int gioiTinh = int.Parse(par.First().GTinh.ToString());
                    if (gioiTinh == 1)
                    {
                        _dic.Add("Nu", "/");
                    }
                    else if (gioiTinh == 0)
                    {
                        _dic.Add("Nam", "/");
                    }

                    if (par.First().SThe.Length >= 15)
                    {
                        //_dic.Add("SThe1", par.First().SThe.Substring(0, 3));
                        //_dic.Add("SThe2", par.First().SThe.Substring(3, 2));
                        //_dic.Add("SThe3", par.First().SThe.Substring(5, 2));
                        //_dic.Add("SThe4", par.First().SThe.Substring(7, 3));
                        //_dic.Add("SThe5", par.First().SThe.Substring(10, 5));
                        _dic.Add("BHYT", par.First().SThe);
                    }
                    else
                        _dic.Add("BHYT", "Không có");
                    _dic.Add("sophieu", IDCLS);
                    arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(_data, _mabn, _makp, true);
                    _dic.Add("ChanDoan2", DungChung.Bien.MaBV == "01049" ? arrThongTinBNKB[1] + "  Mã ICD: " + arrThongTinBNKB[0] : arrThongTinBNKB[1]);
                    _dic.Add("Buong2", arrThongTinBNKB[2]);
                    _dic.Add("Giuong2", arrThongTinBNKB[3]);
                    _dic.Add("Khoa2", arrThongTinBNKB[4]);
                    _dic.Add("KetLuan", par.First().KetLuan);

                    #region Sieu am o bung
                    if (par.First().MaDV == 124 || par.First().MaDV == 125) //sieu am o bung
                    {
                        _dic.Add("tieude", "PHIẾU " + (par.First().TenDV.ToString()).ToUpper());

                        DungChung.Ham.Print(DungChung.PrintConfig.rep_PhieuSAOB4D_01071, par, _dic, false);


                    }
                    #endregion

                    #region Sieu am thai
                    if (par.First().MaDV == 127 || par.First().MaDV == 128 || par.First().MaDV == 129) //sieu am thai
                    {
                        _dic.Add("tieude", "PHIẾU " + (par.First().TenDV.ToString()).ToUpper());

                        DungChung.Ham.Print(DungChung.PrintConfig.rep_PhieuSieuAmThai4D_01071, par, _dic, false);


                    }
                    #endregion

                    #region Sieu am tuyen nuoc bot, tuyen giap, tuyen vu, khop goi, tinh hoan 2d
                    if (par.First().MaDV == 6318 || par.First().MaDV == 133 || par.First().MaDV == 6323 || par.First().MaDV == 123 || par.First().MaDV == 6326 || par.First().MaDV == 138) //sieu am tuyen nuoc bot
                    {
                        _dic.Add("tieude", "PHIẾU " + (par.First().TenDV.ToString()).ToUpper());

                        DungChung.Ham.Print(DungChung.PrintConfig.rep_PhieuSieuAmTuyenNuocBot_Moi_01071, par, _dic, false);


                    }
                    #endregion


                    if (par.First().MaDV == 136)
                    {
                        _dic.Add("tieude", "PHIẾU SIÊU ÂM DOPPLER MẠCH CHỦ BỤNG");
                        DungChung.Ham.Print(DungChung.PrintConfig.rep_PhieuSieuAmDoppler_01071, par, _dic, false);
                    }

                }
                cbo_ChonIn.SelectedIndex = -1;
                return;
            }

            else
            {
                InPhieuCDHA01071(cbo_ChonIn.SelectedIndex);
                cbo_ChonIn.SelectedIndex = -1;
                return;
            }
        }

        private void InPhieuCDHA01071(int value)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30372")
            {
                int IDCLS = 0;
                if (grvketqua.GetFocusedRowCellValue("id") != null)
                    IDCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
                var benhnhan = (from bn in data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                                join cls in data.CLS.Where(p => p.IdCLS == IDCLS) on bn.MaBNhan equals cls.MaBNhan
                                join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                select new { bn, cls, cd, dv, clsct }).ToList();
                List<RepPhieuSieuAmDoopler> repbc = new List<RepPhieuSieuAmDoopler>();

                foreach (var item in benhnhan)
                {
                    RepPhieuSieuAmDoopler r = new RepPhieuSieuAmDoopler();
                    r.DiaChiBV = DungChung.Bien.DiaChi;
                    r.DienThoai = data.HTHONGs.First().SDT == null ? "" : "Điện thoại: " + data.HTHONGs.First().SDT;
                    r.HoVaTen = item.bn.TenBNhan.ToUpper();
                    r.Tuoi = item.bn.Tuoi;
                    r.GioiTinh = item.bn.GTinh == 1 ? "Nam" : "Nữ";
                    r.DiaChi = item.bn.DChi;
                    r.SDT = data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai == null ? "" : data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai;
                    r.ChanDoan = item.cls.ChanDoan;
                    r.ketqua = item.clsct.KetQua;
                    r.KetLuan = item.cd.KetLuan;
                    r.TimeLocation = "Hà Nội, Ngày " + DungChung.Ham.NgaySangChu(Convert.ToDateTime(item.cls.NgayTH), 12);
                    #region Siêu âm Doppler động mạch, tĩnh mạch chi dưới
                    if (value == 10)
                    {

                        if (item.cls.MaCBth != null)
                        {
                            r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);

                        }
                        repbc.Add(r);
                        DungChung.Ham.Print(DungChung.PrintConfig.rep_CDHA_SieuAmDongMachTinhMachChiDuoi_01071, repbc, new Dictionary<string, object>(), false);
                    }



                    #endregion
                    #region Siêu âm Doppler tuyến vú
                    if (value == 9)
                    {

                        if (benhnhan.First().clsct.DuongDan != null)
                        {
                            if (File.Exists(benhnhan.First().clsct.DuongDan))
                            {
                                r.AnhTuyenVu = Image.FromFile(benhnhan.First().clsct.DuongDan);
                            }

                        }
                        else
                        {
                            r.AnhTuyenVu = null;
                        }
                        if (item.cls.MaCBth != null)
                        {
                            r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);

                        }
                        if (benhnhan.First().cls.MaCBth != null)
                        {
                            string macb = benhnhan.First().cls.MaCBth;
                            r.SDTCanBo = data.CanBoes.Where(p => p.MaCB == macb).First().SoDT != null ? "ĐT: " + data.CanBoes.Where(p => p.MaCB == macb).First().SoDT : "ĐT:";
                        }

                        repbc.Add(r);

                        DungChung.Ham.Print(DungChung.PrintConfig.Rep_SieuamdopplerTuyenVu_01071, repbc, new Dictionary<string, object>(), false);
                    }
                    #endregion
                    #region Siêu âm Doppler tử cung phần phụ
                    if (value == 8)
                    {
                        if (benhnhan.First().clsct.DuongDan != "")
                        {
                            string patd1 = benhnhan.First().clsct.DuongDan ?? "";
                            if (patd1 != "")
                            {
                                r.AnhTuCung1 = Image.FromFile(patd1);

                            }
                        }
                        else
                        {
                            r.AnhTuCung1 = null;

                        }

                        if (benhnhan.First().clsct.DuongDan2 != "")
                        {

                            string patd2 = benhnhan.First().clsct.DuongDan2 ?? "";

                            if (patd2 != "")
                            {
                                r.AnhTuCung2 = Image.FromFile(patd2);
                            }
                        }
                        else
                        {

                            r.AnhTuCung2 = null;
                        }

                        if (item.cls.MaCBth != null)
                        {
                            r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);
                        }

                        repbc.Add(r);



                        DungChung.Ham.Print(DungChung.PrintConfig.Rep_SieuamdopplerPhanPhu_01071, repbc, new Dictionary<string, object>(), false);
                    }
                    #endregion
                    #region Siêu âm Doppler u tuyến, hạch vùng cổ
                    if (value == 7)
                    {

                        string patd1 = benhnhan.First().clsct.DuongDan == null ? "" : benhnhan.First().clsct.DuongDan;
                        string patd2 = benhnhan.First().clsct.DuongDan2 == null ? "" : benhnhan.First().clsct.DuongDan2;
                        if (patd1 != "")
                            r.AnhTuyenGiap1 = Image.FromFile(benhnhan.First().clsct.DuongDan);
                        else
                            r.AnhTuyenGiap1 = null;
                        if (patd2 != "")
                            r.AnhTuyenGiap2 = Image.FromFile(benhnhan.First().clsct.DuongDan2);
                        else
                            r.AnhTuyenGiap2 = null;
                        if (item.cls.MaCBth != null)
                        {
                            r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);

                        }

                        repbc.Add(r);



                        DungChung.Ham.Print(DungChung.PrintConfig.Rep_SieuamdopplerUTuyenHachVungCo_01071, repbc, new Dictionary<string, object>(), false);
                    }
                    #endregion
                    #region Siêu âm Dopple tinh hoàn, mào tinh hoàn 2 bên
                    if (value == 11)
                    {
                        if (item.cls.MaCBth != null)
                        {
                            r.BSTH = DungChung.Ham._getTenCB(data, item.cls.MaCBth);

                        }
                        repbc.Add(r);
                        DungChung.Ham.Print(DungChung.PrintConfig.Rep_Sieuamdopplertinhhoan_01071, repbc, new Dictionary<string, object>(), false);
                    }
                    #endregion
                    if (value == 12)
                    {
                        int idcls = -1;
                        if (grvketqua.GetFocusedRowCellValue("id") != null)
                            idcls = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
                        int IDCD = (int)grvketqua.GetFocusedRowCellValue(TIDCD);
                        if (idcls != null)
                        {
                            frm_Dopplertim_01071 frm = new FormNhap.frm_Dopplertim_01071(_mabn, idcls, _maKP, IDCD);
                            frm.ShowDialog();
                            var rowHanlde = grvketqua.FocusedRowHandle;
                            grvBenhnhan_FocusedRowChanged(null, null);
                            grvketqua.FocusedRowHandle = rowHanlde;
                        }
                    }
                    if (value == 13)
                    {
                        int idcls = 0;
                        if (grvketqua.GetFocusedRowCellValue("id") != null)
                            idcls = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
                        int IDCD = (int)grvketqua.GetFocusedRowCellValue(TIDCD);
                        if (idcls != null)
                        {
                            frm_NhapKQDopplerDongMachCanh01071 frm = new FormNhap.frm_NhapKQDopplerDongMachCanh01071(_mabn, idcls, _maKP, IDCD);
                            frm.ShowDialog();
                            var rowHanlde = grvketqua.FocusedRowHandle;
                            grvBenhnhan_FocusedRowChanged(null, null);
                            grvketqua.FocusedRowHandle = rowHanlde;
                        }
                    }
                    if (value == 14)
                    {
                        int idcls = (int)grvketqua.GetFocusedRowCellValue(id);
                        int IDCD = (int)grvketqua.GetFocusedRowCellValue(TIDCD);

                        if (idcls != null)
                        {
                            QLBV.FormNhap.frm_NhapKQDongMachThan frm = new FormNhap.frm_NhapKQDongMachThan(_mabn, idcls, _maKP, IDCD);
                            frm.ShowDialog();
                            var rowHanlde = grvketqua.FocusedRowHandle;
                            grvBenhnhan_FocusedRowChanged(null, null);
                            grvketqua.FocusedRowHandle = rowHanlde;
                        }
                    }

                }
            }
        }

        private class c_LuuHuyetNao
        {

            private string chidinh, duongdan, duongdan2, ketqua, ketqua2, ketqua3, ketqua4, ketqua5, ketqua6, ketqua7, ketqua8, ketqua9, ketqua10, ketqua11, ketqua12, madvct;
            private int id, idcd, idcls, sophieu, status, sttht;
            public string MaDVct
            {
                set { madvct = value; }
                get { return madvct; }
            }
            public string ChiDinh
            {
                set { chidinh = value; }
                get { return chidinh; }
            }
            public string DuongDan
            {
                set { duongdan = value; }
                get { return duongdan; }
            }
            public string DuongDan2
            {
                set { duongdan2 = value; }
                get { return duongdan2; }
            }
            public string KetQua
            {
                set { ketqua = value; }
                get { return ketqua; }
            }
            public string KetQua2
            {
                set { ketqua2 = value; }
                get { return ketqua2; }
            }
            public string KetQua3
            {
                set { ketqua3 = value; }
                get { return ketqua3; }
            }
            public string KetQua4
            {
                set { ketqua4 = value; }
                get { return ketqua4; }
            }
            public string KetQua5
            {
                set { ketqua5 = value; }
                get { return ketqua5; }
            }
            public string KetQua6
            {
                set { ketqua6 = value; }
                get { return ketqua6; }
            }
            public string KetQua7
            {
                set { ketqua7 = value; }
                get { return ketqua7; }
            }
            public string KetQua8
            {
                set { ketqua8 = value; }
                get { return ketqua8; }
            }
            public string KetQua9
            {
                set { ketqua9 = value; }
                get { return ketqua9; }
            }
            public string KetQua10
            {
                set { ketqua10 = value; }
                get { return ketqua10; }
            }
            public string KetQua11
            {
                set { ketqua11 = value; }
                get { return ketqua11; }
            }
            public string KetQua12
            {
                set { ketqua12 = value; }
                get { return ketqua12; }
            }
            public int Id
            {
                set { id = value; }
                get { return id; }
            }
            public int IDCD
            {
                set { idcd = value; }
                get { return idcd; }
            }
            public int IdCLS
            {
                set { idcls = value; }
                get { return idcls; }
            }
            public int SoPhieu
            {
                set { sophieu = value; }
                get { return sophieu; }
            }
            public int Status
            {
                set { status = value; }
                get { return status; }
            }
            public int STTHT
            {
                set { sttht = value; }
                get { return sttht; }
            }

        }
        #region 01071 || 01049 Siêu âm ổ bụng
        public class KetquaSieuAmOBung
        {
            public int stt { get; set; }
            public string tendvct { get; set; }
            public string ketqua { get; set; }
            public string note { get; set; }
        }

        public List<KetquaSieuAmOBung> KQMauSieuAmOBung()
        {
            List<KetquaSieuAmOBung> st = new List<KetquaSieuAmOBung>();
            st.Add(new KetquaSieuAmOBung { stt = 1, note = "Nam;Nữ;Nhi", tendvct = "1. Gan: ", ketqua = "Kích thước không to, nhu mô đều, không có hình khối âm bất thường. Tĩnh mạch cửa không giãn, không có huyết khối, đường mật trong và ngoài gan không giãn không có sỏi." });
            st.Add(new KetquaSieuAmOBung { stt = 2, note = "Nam;Nữ;Nhi", tendvct = "2. Túi mật: ", ketqua = "Thành mỏng, dịch mật đồng nhất, không có sỏi." });
            st.Add(new KetquaSieuAmOBung { stt = 3, note = "Nam;Nữ;Nhi", tendvct = "3. Tụy:", ketqua = " Kích thước không to, nhu mô đồng nhất." });
            st.Add(new KetquaSieuAmOBung { stt = 4, note = "Nam;Nữ;Nhi", tendvct = "4. Lách:", ketqua = " Kích thước không to, nhu mô đồng nhất, tĩnh mạch lách không giãn." });
            st.Add(new KetquaSieuAmOBung { stt = 5, note = "Nam;Nữ;Nhi", tendvct = "5. Thận:", ketqua = "\n" + "\t a.Phải: Kích thước không to, nhu mô thận dày , ranh giới tuỷ vỏ rõ, đài bể thận không giãn, không có sỏi. Niệu quản phải không giãn, không có sỏi." + "\n\t" + "b.Trái: Kích thước không to, nhu mô thận dày,ranh giới tuỷ vỏ rõ, đài bể thận không giãn, không có sỏi. Niệu quản trái không giãn, không có sỏi." });
            st.Add(new KetquaSieuAmOBung { stt = 6, note = "Nam;Nữ;Nhi", tendvct = "6. Bàng Quang:", ketqua = " Thành mỏng nước tiểu đồng nhất" });
            st.Add(new KetquaSieuAmOBung { stt = 7, note = "Nữ", tendvct = "7. Tử cung:", ketqua = " Không to, không thấy bất thường trong buồng tử cung" });
            st.Add(new KetquaSieuAmOBung { stt = 8, note = "Nam", tendvct = "7. Tiền liệt tuyến:", ketqua = " Không to, nhu mô đồng nhất" });
            st.Add(new KetquaSieuAmOBung { stt = 9, note = "Nữ", tendvct = "8. Buồng trứng hai bên", ketqua = " Không phát hiện bất thường" });
            st.Add(new KetquaSieuAmOBung { stt = 10, note = "Nhi", tendvct = "8. ", ketqua = "Không có dịch trong ổ bụng" });
            st.Add(new KetquaSieuAmOBung { stt = 11, note = "Nữ", tendvct = "9. ", ketqua = "Không có dịch trong ổ bụng" });
            st.Add(new KetquaSieuAmOBung { stt = 12, note = "Nhi", tendvct = "7. ", ketqua = "Không có dịch trong ổ bụng" });
            return st;
        }

        #endregion
        private void DienTim_LHN(int _mau)
        {
            col_KQ2.VisibleIndex = -1;
            col_KQ3.VisibleIndex = -1;
            col_KQ4.VisibleIndex = -1;
            col_KQ5.VisibleIndex = -1;
            col_KQ6.VisibleIndex = -1;
            col_KQ7.VisibleIndex = -1;
            col_KQ8.VisibleIndex = -1;
            col_KQ9.VisibleIndex = -1;
            col_KQ10.VisibleIndex = -1;
            col_KQ11.VisibleIndex = -1;
            col_KQ12.VisibleIndex = -1;
            if (_mau == 1)
            {
                string[] td_lhn = new string[] { "α", "α/T(%)", "Am", "A/C", "VBC(ml/p)", "Ghi chú" }; // tiêu đề

                if (DungChung.Bien.MaBV == "19048")
                {
                    td_lhn = new string[] { "Q-a (Sec)", "α(Sec)", "β(Sec)", "T(Sec)", "α/T(Sec)", "A(mm)", "Chỉ số" };
                }
                if (DungChung.Bien.MaBV == "24009")
                {
                    td_lhn = new string[] { "a(giây) 0,16-0,20", "b(giây) 0,05-0,13", "c(giây) 0,5-0,80", "c(giây) 0,55-0,93", "b/T % 10-20", "Amin", "Bmin", "Chỉ số LHN >= 1", "Vml/ph 192-364" };
                }
                for (int i = 0; i < td_lhn.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            KetQua.Caption = td_lhn[i];
                            break;
                        case 1:
                            col_KQ2.Caption = td_lhn[i];
                            col_KQ2.VisibleIndex = 2;
                            break;
                        case 2:
                            col_KQ3.Caption = td_lhn[i];
                            col_KQ3.VisibleIndex = 3;
                            break;
                        case 3:
                            col_KQ4.Caption = td_lhn[i];
                            col_KQ4.VisibleIndex = 4;
                            break;
                        case 4:
                            col_KQ5.Caption = td_lhn[i];
                            col_KQ5.VisibleIndex = 5;
                            break;
                        case 5:
                            col_KQ6.Caption = td_lhn[i];
                            col_KQ6.VisibleIndex = 6;
                            break;
                        case 6:
                            col_KQ7.Caption = td_lhn[i];
                            col_KQ7.VisibleIndex = 7;
                            break;
                        case 7:
                            col_KQ8.Caption = td_lhn[i];
                            col_KQ8.VisibleIndex = 8;
                            break;
                        case 8:
                            col_KQ9.Caption = td_lhn[i];
                            col_KQ9.VisibleIndex = 9;
                            break;
                        case 9:
                            col_KQ10.Caption = td_lhn[i];
                            col_KQ10.VisibleIndex = 10;
                            break;
                        case 10:
                            col_KQ11.Caption = td_lhn[i];
                            col_KQ11.VisibleIndex = 11;
                            break;
                        case 11:
                            col_KQ12.Caption = td_lhn[i];
                            col_KQ12.VisibleIndex = 12;
                            break;
                        default:

                            break;

                    }

                }
            }
            else
            {
                KetQua.Caption = "Kết quả";
            }


        }

        private void rad_KieuDo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_SATim_Click(object sender, EventArgs e)
        {
            int id = -1;
            if (grvketqua.GetFocusedRowCellValue("id") != null)
                id = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
            //MessageBox.Show("Chức năng đang nâng cấp...");
            FormThamSo.frm_nhapketquasieuamdoppleer frm = new frm_nhapketquasieuamdoppleer(id, Reload);
            frm.ShowDialog();

        }

        private void rad_DT_LHN_SelectedIndexChanged(object sender, EventArgs e)
        {
            DienTim_LHN(rad_DT_LHN.SelectedIndex);
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

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBN.Text))
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân!");
            }
            else
            {
                if (grvketqua.GetFocusedRowCellValue(TIDCD) != null)
                {
                    bool chedo = false;
                    int _IDCD = Convert.ToInt32(grvketqua.GetFocusedRowCellValue(TIDCD));
                    int IDCLS = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
                    string TenTN = LupKhoaphong.Text;
                    CLS.InPhieu._inPhieu_CDHA(_data, TenTN, _mabn, IDCLS, _IDCD, chedo);

                }
            }
        }

        private void grvBenhnhan_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            grvBenhnhan_FocusedRowChanged(null, null);
        }

        private void btnchonanhxq1_Click(object sender, EventArgs e)
        {
            ChonanhXquang(ptxquang1, 1);
        }

        private void btnchonanhxq2_Click(object sender, EventArgs e)
        {
            ChonanhXquang(ptxquang2, 2);
        }

        private void bnchonanhxq3_Click(object sender, EventArgs e)
        {
            ChonanhXquang(ptxquang3, 3);
        }

        private void btnchonanhxq4_Click(object sender, EventArgs e)
        {
            ChonanhXquang(ptxquang4, 4);
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            xoaAnh(ptxquang1, _CLSct, 1);
        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            xoaAnh(ptxquang2, _CLSct, 2);
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            xoaAnh(ptxquang3, _CLSct, 3);
        }

        private void simpleButton1_Click_2(object sender, EventArgs e)
        {
            xoaAnh(ptxquang4, _CLSct, 4);
        }


        private void btnSADongMachChu_Click(object sender, EventArgs e)
        {
            int id = -1;
            if (grvketqua.GetFocusedRowCellValue("id") != null)
                id = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
            FormThamSo.frm_NhapKQDopplerDongMachCanh frm = new frm_NhapKQDopplerDongMachCanh(id);
            frm.ShowDialog();


        }

        private void btnsieuam4D_Click(object sender, EventArgs e)
        {
            int id = -1;
            if (grvketqua.GetFocusedRowCellValue("id") != null)
                id = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
            int makp = 0;
            if (LupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(LupKhoaphong.EditValue);
            FormThamSo.frm_KetQuaSieuAmThai4D frm = new frm_KetQuaSieuAmThai4D(id, makp);
            frm.ShowDialog();
        }

        private void btnNhapHCVTBN_Click(object sender, EventArgs e)
        {
            int _int_maBN = 0;
            Int32.TryParse(txtMaBN.Text, out _int_maBN);
            int idcd = 0;
            if (grvketqua.GetFocusedRowCellValue("TIDCD") != null)
                idcd = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("TIDCD"));
            int makp = 0;
            if (LupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(LupKhoaphong.EditValue);
            QLBV.FormNhap.frm_kedon frm = new QLBV.FormNhap.frm_kedon(_int_maBN, idcd, makp, true, "BẢNG THỐNG KÊ THUỐC VÀ VẬT TƯ TIÊU HAO BỆNH NHÂN KHOA CĐHA");
            frm.ShowDialog();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            int id = -1;
            if (grvketqua.GetFocusedRowCellValue("id") != null)
                id = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
            int makp = 0;
            if (LupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(LupKhoaphong.EditValue);
            FormThamSo.frm_KQNSDaDay frm = new frm_KQNSDaDay(id, makp);
            frm.ShowDialog();
        }

        private void grvketqua_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.HitInfo.InRow)
                {
                    if (grvketqua.GetRowCellValue(e.HitInfo.RowHandle, "Status") != null)
                    {
                        var status = Convert.ToInt32(grvketqua.GetRowCellValue(e.HitInfo.RowHandle, "Status"));
                        if (status != 1)
                        {
                            inTiếpToolStripMenuItem.Visible = false;
                        }
                        else
                        {
                            inTiếpToolStripMenuItem.Visible = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
        }

        private void inTiếpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                QLBV.CLS.InPhieu.InTiep = true;
                simpleButton2_Click(null, null);
            }
            catch (Exception ex)
            {
                DungChung.WriteLog.Warn(ex);
            }
            finally
            {
                QLBV.CLS.InPhieu.InTiep = false;
            }
        }

        private void btnChonAnhNS1_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi1, 1);
        }

        private void btnChonAnhNS2_Click(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi2, 2);
        }

        private void btnChonAnhNS4_Click_1(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi4, 4);

        }

        private void btnChonAnhNS3_Click_1(object sender, EventArgs e)
        {
            chonAnhNoiSoi(ptNoisoi3, 3);
        }

        private void gpAnh(PictureEdit pt)
        {
            if (pt != null)
            {
                pt.Image = null;
            }
        }

        #region chọn ảnh cho CDHA 30372
        private void btnChonAnhs1_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnh1, 1);
        }

        private void btnChonAnhs2_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnh2, 2);
        }

        private void btnChoAnhs3_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnh3, 3);
        }

        private void btnChonAnhs4_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnh4, 4);
        }

        private void btnChonAnhs5_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnh5, 5);
        }

        private void btnChonAnhs6_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnh6, 6);
        }

        private void btnChonAnhs7_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnh7, 7);
        }

        private void btnChonAnhs8_Click(object sender, EventArgs e)
        {
            chonAnh(ptAnh8, 8);
        }
        #endregion

        private void btnXoaAnhs1_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh1, _CLSct, 1);
        }

        private void btnXoaAnhs2_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh2, _CLSct, 2);
        }

        private void btnXoaAnhs3_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh3, _CLSct, 3);
        }

        private void btnXoaAnhs4_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh4, _CLSct, 4);
        }

        private void btnXoaAnhs5_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh5, _CLSct, 5);
        }

        private void btnXoaAnhs6_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh6, _CLSct, 6);
        }

        private void btnXoaAnhs7_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh7, _CLSct, 7);

        }

        private void btnXoaAnhs8_Click(object sender, EventArgs e)
        {
            xoaAnh(ptAnh8, _CLSct, 8);
        }

        private void ptAnh1_DoubleClick(object sender, EventArgs e)
        {
            if (ptAnh1.Image != null)
            {
                XemAnh(DuongDan2[0]);
            }

        }
        private void XemAnh(string FilePatd)
        {
            if (FilePatd != "")
            {
                Image img = Image.FromFile(FilePatd);
                frm_CDHA_XemAnh frm = new frm_CDHA_XemAnh(img);
                frm.ShowDialog();
            }

        }

        private void ptAnh2_DoubleClick(object sender, EventArgs e)
        {
            if (ptAnh2.Image != null)
            {
                XemAnh(DuongDan2[1]);
            }
        }

        private void ptAnh3_DoubleClick(object sender, EventArgs e)
        {
            if (ptAnh3.Image != null)
            {
                XemAnh(DuongDan2[2]);
            }
        }

        private void ptAnh4_DoubleClick(object sender, EventArgs e)
        {
            if (ptAnh4.Image != null)
            {
                XemAnh(DuongDan2[3]);
            }
        }

        private void ptAnh5_DoubleClick(object sender, EventArgs e)
        {
            if (ptAnh5.Image != null)
            {
                XemAnh(DuongDan2[4]);
            }

        }

        private void ptAnh6_DoubleClick(object sender, EventArgs e)
        {
            if (ptAnh6.Image != null)
            {
                XemAnh(DuongDan2[5]);
            }

        }

        private void ptAnh7_DoubleClick(object sender, EventArgs e)
        {
            if (ptAnh1.Image != null)
            {
                XemAnh(DuongDan2[6]);
            }
        }

        private void ptAnh8_DoubleClick(object sender, EventArgs e)
        {
            if (ptAnh1.Image != null)
            {
                XemAnh(DuongDan2[7]);
            }
        }

        private void mnGoiSTT_Click(object sender, EventArgs e)
        {
            ManHinhGoiSo_LayMauBenhPham frm = new ManHinhGoiSo_LayMauBenhPham(_maKP);

            frm.ShowDialog();
        }

        private void mnGoiBenhNhan_Click(object sender, EventArgs e)
        {
            ManHinhGoiSo_LayMauBenhPham frm = new ManHinhGoiSo_LayMauBenhPham();


        }

        private void sbtChonanhSA1_Click(object sender, EventArgs e)
        {
            bool tontai = true;
            if (ptSieuam.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;

                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        _fileanh = fileName;
                        if (!string.IsNullOrEmpty(_fileanh))
                            ptSieuam.Image = Image.FromFile(_fileanh);
                        else
                            ptSieuam.Image = null;

                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";

                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh = fileName;
                    if (!string.IsNullOrEmpty(_fileanh))
                        ptSieuam.Image = Image.FromFile(_fileanh);
                    else
                        ptSieuam.Image = null;
                }
            }
        }

        private void sbtChonanhSA2_Click_1(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (ptSieuam2.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh2 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh2))
                        ptSieuam2.Image = Image.FromFile(_fileanh2);
                    else
                        ptSieuam2.Image = null;

                }
            }
        }

        private void sbtXoaanhSA1_Click(object sender, EventArgs e)
        {
            _fileanh = "";
            ptSieuam.Image = null;
        }

        private void sbtXoaanhSA2_Click_1(object sender, EventArgs e)
        {
            _fileanh2 = "";
            ptSieuam2.Image = null;
        }

        private void btnSaDopplerTim_Click_1(object sender, EventArgs e)
        {
            int id = -1;
            if (grvketqua.GetFocusedRowCellValue("id") != null)
                id = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
            //MessageBox.Show("Chức năng đang nâng cấp...");
            FormThamSo.frm_nhapketquasieuamdoppleer frm = new frm_nhapketquasieuamdoppleer(id, Reload);
            frm.ShowDialog();
        }

        private void Reload()
        {
            grvBenhnhan_FocusedRowChanged(grvBenhnhan, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(grvBenhnhan.FocusedRowHandle, grvBenhnhan.FocusedRowHandle));
        }

        private void btnsieuam4D_Click_1(object sender, EventArgs e)
        {
            int id = -1;
            if (grvketqua.GetFocusedRowCellValue("id") != null)
                id = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
            int makp = 0;
            if (LupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(LupKhoaphong.EditValue);
            FormThamSo.frm_KetQuaSieuAmThai4D frm = new frm_KetQuaSieuAmThai4D(id, makp);
            frm.ShowDialog();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24297")
            {
                //QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var kqcls = (from cls in _data.CLS.Where(p => p.IdCLS == id)
                             join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                             join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                             select new { cls.NgayTH, cls.MaCBth, cd.MaDV, cd.Status, cd.IDCD, clsct.KetQua, cls.MaBNhan, cd.KetLuan, cd.MaMay, clsct.DuongDan, cd.LoiDan, clsct.DuongDan2, clsct.KetQua_Rtf }).ToList();

                if (kqcls.Count > 0)
                {
                    if (kqcls.First().DuongDan != null && File.Exists(kqcls.First().DuongDan))
                    {
                        _fileanh = kqcls.First().DuongDan;
                        ptSieuam.Image = Image.FromFile(_fileanh);
                    }
                    else
                        ptSieuam.Image = null;
                    if (kqcls.First().DuongDan2 != null && File.Exists(kqcls.First().DuongDan2))
                    {
                        _fileanh2 = kqcls.First().DuongDan2;
                        ptSieuam2.Image = Image.FromFile(_fileanh2);
                    }
                    else
                        ptSieuam2.Image = null;
                    if (!string.IsNullOrWhiteSpace(kqcls.First().KetLuan))
                    {
                        mmKLSieuam.Text = kqcls.First().KetLuan;
                    }
                    if (kqcls.First().LoiDan != null)
                    {
                        mmLoidanSieuAm.Text = kqcls.First().LoiDan;
                    }
                    if (kqcls.First().NgayTH != null)
                    {
                        lupNgayTH.DateTime = Convert.ToDateTime(kqcls.First().NgayTH);
                    }
                    if (kqcls.First().MaCBth != null && kqcls.First().MaCBth.ToString() != "")
                    {
                        LupCanBo.EditValue = kqcls.First().MaCBth;
                    }
                    if (kqcls.First().KetQua_Rtf == null)
                    {
                        reKQSieuAm.Text = kqcls.First().KetQua;
                    }
                    else
                    {
                        reKQSieuAm.Text = kqcls.First().KetQua_Rtf;
                    }
                }

                grvBenhnhan_FocusedRowChanged(null, null);
            }
        }

        private void kếtQuảChẩnĐoánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idcls = (int)grvketqua.GetFocusedRowCellValue(id);
            string TenDichVu = grvketqua.GetFocusedRowCellValue(tendv).ToString();
            int IDCD = (int)grvketqua.GetFocusedRowCellValue(TIDCD);
            if (idcls != null)
            {
                frm_NhapKetQuaCDHA_01071 frm = new frm_NhapKetQuaCDHA_01071(_mabn, idcls, _maKP, TenDichVu, IDCD);
                frm.ShowDialog();
                var rowHanlde = grvketqua.FocusedRowHandle;
                grvBenhnhan_FocusedRowChanged(null, null);
                grvketqua.FocusedRowHandle = rowHanlde;
            }

        }

        //private void btnSieuAmDongMachThan_Click(object sender, EventArgs e)
        //{
        //    if (grvketqua.GetFocusedRowCellValue(tendv).ToString() == "Siêu âm Doppler động mạch thận")
        //    {
        //        int idcls = (int)grvketqua.GetFocusedRowCellValue(id);
        //        int IDCD = (int)grvketqua.GetFocusedRowCellValue(TIDCD);

        //        if (idcls != null)
        //        {
        //            QLBV.FormNhap.frm_NhapKQDongMachThan frm = new FormNhap.frm_NhapKQDongMachThan(_mabn, idcls, _maKP, IDCD);
        //            frm.ShowDialog();
        //            var rowHanlde = grvketqua.FocusedRowHandle;
        //            grvBenhnhan_FocusedRowChanged(null, null);
        //            grvketqua.FocusedRowHandle = rowHanlde;
        //        }
        //    }
        //    else
        //    {
        //        XtraMessageBox.Show("không thể thao tác! Bệnh nhân chưa có chỉ định làm dịch vụ Doppler động mạch thận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }
        //}

        private void btn35tuan_Click(object sender, EventArgs e)
        {
            int makp35 = 0;
            int idcls35 = 0;
            _tendv = Convert.ToString(grvketqua.GetFocusedRowCellValue("tendv"));
            if (grvketqua.GetFocusedRowCellValue("id") != null)
                idcls35 = Convert.ToInt32(grvketqua.GetFocusedRowCellValue("id"));
            if (LupKhoaphong.EditValue != null)
                makp35 = Convert.ToInt32(LupKhoaphong.EditValue);

            frm_KetQuaSieuAmThai35w_12345 frm = new frm_KetQuaSieuAmThai35w_12345(idcls35, makp35);
            frm.ShowDialog();
        }

        private void ptTMH1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grcBenhnhan_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH11, 1);
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH33, 3);
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH55, 5);
        }

        private void simpleButton25_Click(object sender, EventArgs e)
        {
            chonAnh(ptTMH66, 6);
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH11, _CLSct, 1);
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH33, _CLSct, 3);
        }

        private void simpleButton24_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH55, _CLSct, 5);
        }

        private void simpleButton26_Click(object sender, EventArgs e)
        {
            xoaAnh(ptTMH66, _CLSct, 6);
        }

        private void mmeTMH33_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl55_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl57_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ptTMH66_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl56_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bntSelect1_Click(object sender, EventArgs e)
        {
            bool tontai = true;
            if (picSieuAm1.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                    string fileName = string.Empty;
                    OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = false;

                    op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                    if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = op.FileName;
                        _fileanh = fileName;
                        if (!string.IsNullOrEmpty(_fileanh))
                            picSieuAm1.Image = Image.FromFile(_fileanh);
                        else
                            picSieuAm1.Image = null;

                    }
                }
            }
            else
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";

                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh = fileName;
                    if (!string.IsNullOrEmpty(_fileanh))
                        picSieuAm1.Image = Image.FromFile(_fileanh);
                    else
                        picSieuAm1.Image = null;
                }
            }
        }

        private void btnSelect2_Click(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (picSieuAm2.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh2 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh2))
                        picSieuAm2.Image = Image.FromFile(_fileanh2);
                    else
                        picSieuAm2.Image = null;

                }
            }
        }

        private void btnXoa1_Click(object sender, EventArgs e)
        {
            _fileanh = "";
            picSieuAm1.Image = null;
        }

        private void bntXoa2_Click(object sender, EventArgs e)
        {
            _fileanh2 = "";
            picSieuAm2.Image = null;
        }

        private void btnSelect3_Click(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (picSieuAm3.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh3 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh3))
                        picSieuAm3.Image = Image.FromFile(_fileanh3);
                    else
                        picSieuAm3.Image = null;

                }
            }
        }

        private void btnXoa3_Click(object sender, EventArgs e)
        {
            _fileanh3 = "";
            picSieuAm3.Image = null;
        }

        private void btnSelect4_Click(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (picSieuAm4.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh4 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh4))
                        picSieuAm4.Image = Image.FromFile(_fileanh4);
                    else
                        picSieuAm4.Image = null;

                }
            }
        }

        private void btnXoa4_Click(object sender, EventArgs e)
        {
            _fileanh4 = "";
            picSieuAm4.Image = null;
        }

        private void btnSelect5_Click(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (picSieuAm5.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh5 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh5))
                        picSieuAm5.Image = Image.FromFile(_fileanh5);
                    else
                        picSieuAm5.Image = null;

                }
            }
        }

        private void btnXoa5_Click(object sender, EventArgs e)
        {
            _fileanh5 = "";
            picSieuAm5.Image = null;
        }

        private void btnSelect6_Click(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (picSieuAm6.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh6 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh6))
                        picSieuAm6.Image = Image.FromFile(_fileanh6);
                    else
                        picSieuAm6.Image = null;

                }
            }
        }

        private void btnXoa6_Click(object sender, EventArgs e)
        {
            _fileanh6 = "";
            picSieuAm6.Image = null;
        }

        private void btnSelect7_Click(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (picSieuAm7.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh7 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh7))
                        picSieuAm7.Image = Image.FromFile(_fileanh7);
                    else
                        picSieuAm7.Image = null;

                }
            }
        }

        private void btnXoa7_Click(object sender, EventArgs e)
        {
            _fileanh7 = "";
            picSieuAm7.Image = null;
        }

        private void btnSelect8_Click(object sender, EventArgs e)
        {
            bool tontai = true, tieptuc = true;
            if (picSieuAm8.Image == null)
            {
                tontai = false;
            }
            if (tontai)
            {
                DialogResult _dresult = MessageBox.Show("Bạn có muốn chọn lại ảnh!", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dresult == DialogResult.Yes)
                {
                }
                else
                {
                    tieptuc = false;
                }
            }
            if (tieptuc)
            {
                string fileName = string.Empty;
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "JPEG (*.jpg)|*.jpg|BMP(*.bmp)| *.bmp|PNG (*.png)|*.png";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = op.FileName;
                    _fileanh8 = fileName;
                    if (!string.IsNullOrEmpty(_fileanh8))
                        picSieuAm8.Image = Image.FromFile(_fileanh8);
                    else
                        picSieuAm8.Image = null;

                }
            }
        }

        private void btnXoa8_Click(object sender, EventArgs e)
        {
            _fileanh8 = "";
            picSieuAm8.Image = null;
        }

        private void mKetqua_DocumentLoaded(object sender, EventArgs e)
        {
            mKetqua.Document.DefaultCharacterProperties.FontName = "Times New Roman";
        }

        private void btnImagePacs_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = 0;
                var isNumber = int.TryParse(grvketqua.GetFocusedRowCellValue("id").ToString(), out orderId);
                if (isNumber && orderId > 0)
                {
                    var imageUrls = Task.Run(async () => await GetUrlImage(orderId)).Result;

                    //imageUrls = new List<string>();
                    //imageUrls.Add(@"https://server-vrpacs.neomedic.vn/vrpacs-scu/study-get1?sID=2817&file=0000/1.3.10001.1.jpg");

                    if (imageUrls != null && imageUrls.Count > 0)
                    {
                        if (DownloadImage(imageUrls))
                        {
                            MessageBox.Show("Ảnh đã được tải xuống thành công.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy ảnh bệnh lý.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private async Task<List<string>> GetUrlImage(int orderId)
        {
            var result = new List<string>();

            try
            {
                string baseUrl = System.Configuration.ConfigurationManager.AppSettings["URL_Send_PACS"];
                if (!string.IsNullOrEmpty(baseUrl))
                {
                    var url = $"api/PACS/GetUrlImage?orderId={orderId}";
                    var reponse = await AppApi.GetAsync<List<string>, object>(baseUrl, url);

                    if (reponse != null)
                    {
                        return reponse.ContentSuccess;
                    }
                }
            }
            catch (Exception)
            {

            }

            return await Task.FromResult(result);
        }

        private bool DownloadImage(List<string> imageUrls)
        {
            var isDowload = false;

            using (HttpClient client = new HttpClient())
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Chọn thư mục lưu trữ";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    isDowload = true;

                    foreach (var imageUrl in imageUrls)
                    {
                        byte[] imageBytes = client.GetByteArrayAsync(imageUrl).Result;

                        string selectedPath = folderBrowserDialog.SelectedPath;
                        string fileName = System.IO.Path.GetFileName(imageUrl);
                        string finalPath = System.IO.Path.Combine(selectedPath, fileName);

                        using (FileStream fileStream = new FileStream(finalPath, FileMode.Create))
                        {
                            fileStream.Write(imageBytes, 0, imageBytes.Length);
                        }
                    }
                }
            }

            return isDowload;
        }
    }
}
