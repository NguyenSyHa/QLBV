using System;
using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.BaoCao;
using QLBV.TraCuu;
using QLBV.CLS;
using DevExpress.XtraGrid.Views.Grid;
using QLBV.FormNhap;
using System.Transactions;
using DevExpress.XtraGrid.Views.Grid;
using System.Text.RegularExpressions;
using System.Data.Entity;
using QLBV.ChucNang;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using DevExpress.XtraGrid.Columns;
using QLBV.Utilities.Commons;
using System.Threading.Tasks;
using QLBV.Models.Business.ConectionPACS;
using static QLBV.FormThamSo.frm_benhnhanxuatduoc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using QLBV.Utilities.Helppers;

namespace QLBV.FormThamSo
{
    public partial class FRM_chidinh_Moi : DevExpress.XtraEditors.XtraForm
    {

        public FRM_chidinh_Moi()
        {
            InitializeComponent();
        }
        int _Mabn = 0;//
        string _tenbn = "";
        bool sua = false;

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        int trangthai = 0; // Mới = 1; Sửa = 2;
        DateTime Ngaythang = System.DateTime.Now;
        List<C_DichVu> _listDichVu = new List<C_DichVu>();
        List<TTCD> _TTCD = new List<TTCD>(); // Để set trạng thái chỉ định trên gridviewNhomDV
        List<C_DichVu> _lDichVu = new List<C_DichVu>();
        List<C_YLenh> _lYLenh = new List<C_YLenh>();
        string[] arrThongTinBNKB = new string[5] { "", "", "", "", "" };
        int lan1 = 0;
        int DTBN = -1;
        int _idDB = 0;
        int _makp = 0;
        string _maIcd;
        // dùng cho BV 34019- lưu y lệnh vào diễn biến
        List<int> _lIDCLS_DB = new List<int>();
        public delegate void PassData(string ylenh);
        public PassData passData;
        public Action reLoad;
        public int MaKP_14018;
        public int idKB;
        //--------------------------------------------//
        public FRM_chidinh_Moi(int mbn, string tenbn)
        {
            InitializeComponent();
            _Mabn = mbn;
            _tenbn = tenbn;
            LoadDM();
        }
        public class BNDT
        {
            public int IDKB { get; set; }
            public int? MaKP { get; set; }
        }
        public FRM_chidinh_Moi(int mbn, string tenbn, string maIcd)
        {
            InitializeComponent();
            _Mabn = mbn;
            _tenbn = tenbn;
            _maIcd = maIcd;
            LoadDM();
        }
        private int KieuFrom;
        public class ChiDinhToPacs
        {
            public int MaChiDinh { get; set; }
            public DateTime? ThoiGianChiDinh { get; set; }
            public int? MaBenhNhan { get; set; }
            public string TenBenhNhan { get; set; }
            public string NgaySinh { get; set; }
            public string ThangSinh { get; set; }
            public string NamSinh { get; set; }
            public string GioiTinh { get; set; }
            public string DiaChi { get; set; }
            public string SDT { get; set; }
            public string NoiChiDinh { get; set; }
            public string MaBacSiChiDinh { get; set; }
            public string TenBacSiChiDinh { get; set; }
            public int? MaDichVu { get; set; }
            public string TenDichVu { get; set; }
            public string NhomDichVu { get; set; }
            public string ChanDoan { get; set; }
            public string TrangThai { get; set; }
        }
        private class PhieuGiacMac
        {
            public string DiaChiBV { get; set; }
            public string DienThoai { get; set; }
            public string HoVaTen { get; set; }
            public int? Tuoi { get; set; }
            public string GioiTinh { get; set; }
            public string KPhong { get; set; }
            public string Buong { get; set; }
            public string Giuong { get; set; }
            public string SVV { get; set; }
            public string TenCdJaval { get; set; }
            public string TenCdDiop { get; set; }
            public string DiaChi { get; set; }
            public string TenCQ { get; set; }
            public string CQCQ { get; set; }
            public string SDT { get; set; }
            public string KetLuan { get; set; }
            public string KetQua_javal { get; set; }
            public string KetQua_diop { get; set; }
            public string BSDT { get; set; }
            public string BSTH_Diop { get; set; }
            public string BSTH_Javal { get; set; }
            public string ThoiGian { get; set; }
            public string ChanDoan { get; set; }
        }
        public FRM_chidinh_Moi(int mbn, string tenbn, int iddienbien)
        {
            InitializeComponent();
            _Mabn = mbn;
            _tenbn = tenbn;
            _idDB = iddienbien;
            _makp = iddienbien;
            LoadDM();
        }

        public repPhieuXNHoaSinhMau getBaoCao(repPhieuXNHoaSinhMau rep)
        {
            return rep;
        }
        bool chaylandau = true;
        private int _getSoTT(QLBV_Database.QLBVEntities _data, int _makp, DateTime _ngay)
        {
            //lấy số thứ tự  
            Int32 STT = 1;
            try
            {
                if (_ngay != null)
                {

                    var q = (from MaxSTT in _data.CLS.Where(p => p.NgayThang.Value.Day == _ngay.Day && p.NgayThang.Value.Month == _ngay.Month && p.NgayThang.Value.Year == _ngay.Year).Where(p => p.MaKPth == _makp) select new { MaxSTT.STT }).OrderByDescending(p => p.STT).ToList();

                    if (q.Count > 0 && q.First().STT != null)
                    {
                        STT = q.First().STT.Value + 1;

                    }


                }
                return STT;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        class xemphieu
        {
            int value;

            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
            string text;

            public string Text
            {
                get { return text; }
                set { text = value; }
            }
        }
        class CanLamSang
        {
            public int? MaDV { get; set; }
            public int? IDNhom { get; set; }
            public string MaCBth { get; set; }

            public int IDCD { get; set; }

            public int? STT { get; set; }

            public string TenRG { get; set; }
            public string TenRGDV { get; set; }

            public string TenDV { get; set; }

            public int IdCLS { get; set; }

            public int IDBB { get; set; }

            public string MaCB { get; set; }

            public DateTime? NgayThang { get; set; }

            public bool Check { get; set; }

            public int? STTDV { get; set; }
            public int? STT_F { get; set; }
            public int MaBNhan { get; set; }
            public int IdTieuNhom { get; set; }
            public int IdCLS_Copy { get; set; }
            public bool IS_COPY { get; set; }
            public bool IsAutoExecute { get; set; }
            public bool TrongBH { get; set; }
        }

        public class ChiDinhDS
        {
            public ChiDinh chiDinh { get; set; }
            public CL canLamSang { get; set; }
            public TieuNhomDV tieuNhomDV { get; set; }
            public DichVu dichvu { get; set; }
        }

        List<ChiDinhDS> _dsChiDinhs = new List<ChiDinhDS>();
        List<CanLamSang> _lchidinh = new List<CanLamSang>();
        List<xemphieu> _lxemphieu = new List<xemphieu>();
        List<KPhong> _kphong = new List<KPhong>();
        List<ICD10> _licd10 = new List<ICD10>();
        string DtuongBN = "";
        string StheBHYT = "";
        int _noitru = 0;
        List<TieuNhomDV> listTieuNhomDV = new List<TieuNhomDV>();
        List<NhomDV> listNhomDV = new List<NhomDV>();
        List<ChiDinhToPacs> _ldata = new List<ChiDinhToPacs>();
        public void LoadDM()
        {
            listTieuNhomDV = _data.TieuNhomDVs.ToList();
            listNhomDV = _data.NhomDVs.ToList();
        }

        private void FRM_chidinh_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            this.panelControl8.Height = this.Height / 2;
            if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
            {
                colYLenh2.Visible = true;
                btnSaveDate.Visible = true;
                colBSTH.OptionsColumn.AllowEdit = true;
            }
            else
            {
                btnSaveDate.Visible = false;
                colYLenh2.Visible = false;
                grvNoiDungKhiVaoVien.Visible = grvTenDungCuTap.Visible = false;
                grvCLS.OptionsView.ColumnAutoWidth = true;
            }

            if (DungChung.Bien.MaBV == "24297")
            {
                colBSTH.OptionsColumn.AllowEdit = true;
            }

            if (DungChung.Bien.MaBV != "30372")
            {
                TrongDMBH1.Visible = false;
            }

            _lxemphieu.Clear();
            _lchidinh.Clear();
            if (GrvNhomDV.GetFocusedRow() == null)
            {
                lupKhoaphong.EditValue = DungChung.Bien.MaKP;
                lupNguoiKhamkb.EditValue = DungChung.Bien.MaCB;
            }
            _lxemphieu.Add(new xemphieu { Value = 0, Text = "Chọn ... " });
            _lxemphieu.Add(new xemphieu { Value = 1, Text = "In chỉ định" });
            if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30303")
            {
                _lxemphieu.Add(new xemphieu { Value = 2, Text = "In chỉ định TH" });

            }
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24297")
            {
                colInChiDinh.Caption = "In chỉ định TH";
                this.linkInchidinh.NullText = "In chỉ định TH";


            }
            if (DungChung.Bien.MaBV == "12345")
            {
                colMadv.Visible = true;
            }
            else
            {
                colDG.Visible = false;
                grvCLS.OptionsView.ShowFooter = false;
            }

            //else
            //{
            //    colInChiDinh.Visible = false;
            //    colXemPhieu.Caption = "In chỉ định";
            //}
            if (DungChung.Bien.MaBV.Substring(0, 2) != "24")
                colXHH.Visible = false;
            _lxemphieu.Add(new xemphieu { Value = 3, Text = "Xem phiếu" });
            _lxemphieu.Add(new xemphieu { Value = 4, Text = "Xem phiếu màu" });
            _lxemphieu.Add(new xemphieu { Value = 5, Text = "Tạo bản sao" });
            _lxemphieu.Add(new xemphieu { Value = 6, Text = "Thực hiện TT-PT" });
            _lxemphieu.Add(new xemphieu { Value = 7, Text = "In phiếu(full)" });
            _lxemphieu.Add(new xemphieu { Value = 8, Text = "TTXN Tế bào học" });
            _lxemphieu.Add(new xemphieu { Value = 9, Text = "Thực hiện LHN" });
            lupXemPhieu.DataSource = _lxemphieu;
            if (DungChung.Bien.MaBV == "30009")
            {
                btnInCD.Enabled = true;
            }
            grvCLS.OptionsView.RowAutoHeight = true;
            if (chaylandau)
            {
                dtNgayCD.DateTime = System.DateTime.Now;
                setBarCode();
            }
            chaylandau = false;
            _licd10 = _Data.ICD10.ToList();
            benhnhan = _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
            if (benhnhan != null)
            {
                if (benhnhan.Status == 2 || benhnhan.Status == 3)
                {
                    btnSao.Enabled = false;
                }
                else
                {
                    btnSao.Enabled = true;
                }
            }
            vaovien = _Data.VaoViens.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
            StheBHYT = benhnhan.SThe;
            _noitru = benhnhan.NoiTru ?? 0;
            var dtbn = (from dt in _Data.DTBNs.Where(p => p.HTTT == 1).Where(p => p.IDDTBN == benhnhan.IDDTBN) select dt.IDDTBN).ToList();
            DTBN = benhnhan.IDDTBN;
            if (dtbn.Count > 0)
            {
                DTBN = 1;
                ckccpdinhkem.Visible = true;
            }
            if (DTBN == 2)
            {
                colTrongBH.OptionsColumn.ReadOnly = true;
                colCPDinhKem2.Visible = false;
            }

            _kphong = _Data.KPhongs.ToList();
            var kp = _Data.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám" || ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303") ? p.PLoai == "Hành chính" : true)).Distinct().OrderBy(p => p.TenKP).ToList();
            //if (DungChung.Bien.CapDo == 9 || DungChung.Bien.CapDo == 8)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupKhoaphong.Properties.DataSource = kp;
            }
            else
            {
                //if (DungChung.Bien.MaBV == "01071")
                //{
                var kpkhamdt = (from b in kp
                                join c in DungChung.Bien.listKPHoatDong on b.MaKP equals c
                                select b).ToList();
                lupKhoaphong.Properties.DataSource = kpkhamdt;
                //if ( DungChung.Bien.MaBV == "30007")
                //{
                //    lupNguoiKhamkb.Enabled = false;
                //    lupKhoaphong.Enabled = false;
                //}
                //}
                //else
                //{
                //    var kpkhamdt = (from b in _kphong.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám")
                //                    join c in DungChung.Bien.listKPHoatDong on b.MaKP equals c
                //                    select b).ToList();
                //    lupKhoaphong.Properties.DataSource = kpkhamdt;
                //}
            }
            var CB = _Data.CanBoes.Where(p => p.Status == 1)
                .Where(p => (DungChung.Bien.MaBV == "01071" ||
                DungChung.Bien.MaBV == "01049" ||
                DungChung.Bien.MaBV == "12345" ||
                DungChung.Bien.MaBV == "24297" ||
                DungChung.Bien.MaBV == "56789" ||
                DungChung.Bien.MaBV == "30303") ?
                (p.CapBac.ToLower().Contains("bs") ||
                p.CapBac.ToLower().Contains("bác sĩ") ||
                p.CapBac.ToLower().Contains("bác sỹ") ||
                p.CapBac.ToLower().Contains("giáo sư") ||
                p.CapBac.ToLower().Contains("cn") ||
                p.CapBac.ToLower().Contains("ktv") ||
                p.CapBac.ToLower().Contains("bscki") ||
                p.CapBac.ToLower().Contains("bsckii") ||
                p.CapBac.ToLower().Contains("gs") ||
                p.CapBac.ToLower().Contains("tiến sĩ") ||
                p.CapBac.ToLower().Contains("ts")) : p.CapBac != "")
                .Distinct().OrderBy(p => p.TenCB).ToList();
            //if (DungChung.Bien.CapDo == 9 || DungChung.Bien.CapDo == 8)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                lupNguoiKhamkb.Properties.DataSource = CB;
            else
            {
                //his-79 28/04/2021
                string _makp = "";
                var makp = Convert.ToInt32(lupKhoaphong.EditValue);
                _makp = ";" + makp.ToString() + ";";
                lupNguoiKhamkb.Properties.DataSource = CB.Where(p => p.MaKPsd.Contains(_makp)).ToList();
                /*if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && lupKhoaphong.EditValue != null && !string.IsNullOrWhiteSpace(lupKhoaphong.EditValue.ToString()))
                {
                    lupNguoiKhamkb.Properties.DataSource = CB.Where(p => p.MaKPsd.Contains(_makp)).ToList();
                }
                else
                {
                    lupNguoiKhamkb.Properties.DataSource = CB.Where(p => p.MaKP == DungChung.Bien.MaKP).ToList();
                }*/
            }
            lupBSCD.DataSource = CB;
            lupTenCBth.DataSource = CB;
            TTCD themmoi = new TTCD();
            themmoi.tt = "Đã làm";
            themmoi.idcd = 1;
            _TTCD.Add(themmoi);
            TTCD themmoi2 = new TTCD();
            themmoi2.tt = "Chưa làm";
            themmoi2.idcd = 0;
            _TTCD.Add(themmoi2);
            lupTTCD.DataSource = _TTCD.ToList();
            lupTrangThai.DataSource = _TTCD.ToList();


            var cdcls = (from cl in _Data.CLS.Where(p => p.MaBNhan == (_Mabn)).Where(p => (DungChung.Bien.MaBV == "34019") ? ((_idDB == -1 && (p.IDDienBien == null || p.IDDienBien == 0)) || (_idDB > 0 && p.IDDienBien == _idDB)) : true)
                         join chidinh in _Data.ChiDinhs on cl.IdCLS equals chidinh.IdCLS
                         select new { cl, chidinh }).ToList();
            var dvcd = (from dv in _Data.DichVus
                        join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                        select new
                        {
                            tn,
                            dv,
                            TenDVRG = dv.TenRG,
                        }).ToList();
            var a = (from cls in cdcls
                     join dvu in dvcd on cls.chidinh.MaDV equals dvu.dv.MaDV
                     group new { dvu.tn, cls.cl, dvu } by new { cls.chidinh.MaCBth, clscbth = cls.cl.MaCBth, cls.chidinh.IDCD, dvu.dv.IDNhom, dvu.dv.TenDV, dvu.TenDVRG, cls.cl.STT, dvu.tn.TenRG, cls.cl.NgayThang, cls.cl.IdCLS, cls.cl.MaCB, dvu.dv.SoTT, dvu.dv.MaDV, cls.cl.MaBNhan, cls.cl.IS_COPY, dvu.dv.IsAutoExecute, cls.chidinh.TrongBH } into kq
                     select new
                     {
                         MaCBth = kq.Key.MaCBth != null ? kq.Key.MaCBth : kq.Key.clscbth,
                         kq.Key.IDCD,
                         STT = kq.Key.STT,
                         ChanDoan = kq.Key.TenRG,
                         kq.Key.TenDV,
                         kq.Key.TenDVRG,
                         IdCLS = kq.Key.IdCLS,
                         IDBB = 0,
                         MaCB = kq.Key.MaCB,
                         NgayThang = kq.Key.NgayThang,
                         STTDV = kq.Key.SoTT,
                         kq.Key.IDNhom,
                         kq.Key.MaDV,
                         kq.Key.MaBNhan,
                         kq.Key.IS_COPY,
                         kq.Key.IsAutoExecute,
                         TrongBH = kq.Key.TrongBH
                     }).ToList();

            _dsChiDinhs = new List<ChiDinhDS>();
            _dsChiDinhs = (from cls in cdcls
                           join dvu in dvcd on cls.chidinh.MaDV equals dvu.dv.MaDV
                           group new { cls.cl, cls.chidinh, dvu.tn } by new { cls.cl, cls.chidinh, dvu.tn, dvu.dv } into kq
                           select new ChiDinhDS
                           {
                               canLamSang = kq.Key.cl,
                               chiDinh = kq.Key.chidinh,
                               tieuNhomDV = kq.Key.tn,
                               dichvu = kq.Key.dv
                           }).ToList();


            _lchidinh = (from cl in a
                         select new CanLamSang
                         {
                             MaDV = cl.MaDV,
                             Check = false,
                             MaCBth = cl.MaCBth,//DungChung.Ham.Check_DuyetTamThu(cl.IDCD) ? cl.MaCBth : "",
                             IDCD = cl.IDCD,
                             MaBNhan = cl.MaBNhan ?? 0,
                             STT = cl.STT,
                             TenRG = cl.ChanDoan,
                             TenDV = DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24272" ? cl.TenDVRG : cl.TenDV,
                             IdCLS = cl.IdCLS,
                             IDBB = 0,
                             MaCB = cl.MaCB,
                             NgayThang = cl.NgayThang,
                             STTDV = cl.STTDV,
                             IDNhom = cl.IDNhom,
                             IS_COPY = cl.IS_COPY ?? false,
                             IsAutoExecute = cl.IsAutoExecute ?? false,
                             TrongBH = cl.TrongBH == 1 ? true : false
                         }).ToList();
            if (_lchidinh.Count > 0)
            {
                colYLenh.OptionsColumn.AllowEdit = false;
                colYLenh.OptionsColumn.ReadOnly = true;
                mmYlenh.Properties.ReadOnly = true;
            }
            if (DungChung.Bien.MaBV == "20001")
            {
                GrcNhomDV.DataSource = null;
                GrcNhomDV.DataSource = _lchidinh.OrderByDescending(p => p.IdCLS).ToList();
                new DevExpress.XtraGrid.Selection.CheckMarksSelection(GrvNhomDV);
                //this.GrvNhomDV.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
                //new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNgaythang, DevExpress.Data.ColumnSortOrder.Descending)});
            }
            else if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "01071")
            {
                GrcNhomDV.DataSource = null;
                GrcNhomDV.DataSource = _lchidinh.OrderBy(p => p.NgayThang).ThenBy(p => p.STTDV).ToList();
                new DevExpress.XtraGrid.Selection.CheckMarksSelection(GrvNhomDV);
                grcDichvu.DataSource = null;
                grcCLS.DataSource = null;
            }
            else if (DungChung.Bien.MaBV == "30372")
            {
                GrcNhomDV.DataSource = null;
                GrcNhomDV.DataSource = _lchidinh.OrderBy(p => p.STT_F).ToList();
                new DevExpress.XtraGrid.Selection.CheckMarksSelection(GrvNhomDV);
            }
            else
            {
                GrcNhomDV.DataSource = null;
                GrcNhomDV.DataSource = _lchidinh.ToList();
                new DevExpress.XtraGrid.Selection.CheckMarksSelection(GrvNhomDV);
            }
            labTenBN.Text = "Tên Bệnh Nhân: " + _tenbn;
            var bnkb = _Data.BNKBs.Where(p => p.MaBNhan == _Mabn).OrderByDescending(p => p.IDKB).FirstOrDefault();
            if (bnkb != null)
            {
                if ((DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "24012")
                    && DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                {
                    lupNguoiKhamkb.EditValue = DungChung.Bien.MaCB;
                    lupKhoaphong.EditValue = DungChung.Bien.MaKP;
                }
                else
                {
                    lupNguoiKhamkb.EditValue = bnkb.MaCB;
                    lupKhoaphong.EditValue = bnkb.MaKP;
                }
            }
            else if (DungChung.Bien.MaBV == "01071" ||
                DungChung.Bien.MaBV == "01049" ||
                DungChung.Bien.MaBV == "12345" ||
                DungChung.Bien.MaBV == "24297" ||
                DungChung.Bien.MaBV == "56789" ||
                DungChung.Bien.MaBV == "30303")
            {
                lupKhoaphong.EditValue = DungChung.Bien.MaKP;
            }
            //            Nhóm xét nghiệm
            //Chẩn đoán hình ảnh
            //Nhóm phẫu thuật, thủ thuật
            var q = (from tn in _Data.TieuNhomDVs
                     join nhom in _Data.NhomDVs.Where(p => p.TenNhomCT == ("Xét nghiệm") || p.TenNhomCT == ("Chẩn đoán hình ảnh") || p.TenNhomCT == ("Thăm dò chức năng") || p.TenNhomCT == ("Thủ thuật, phẫu thuật")) on tn.IDNhom equals nhom.IDNhom
                     select new { tn.IDNhom, tn.TenRG, tn.Status }).Distinct().OrderBy(p => p.IDNhom).ThenBy(p => p.TenRG).ToList();
            lupTN.Properties.DataSource = q.ToList();
            string[] ten = new string[10];
            if (q.Count > 0)
                ten = q.Where(p => p.Status != 0).Select(p => p.TenRG).Distinct().ToArray();
            this.radTenRG.Properties.Items.Clear();
            foreach (var item in ten)
            {
                this.radTenRG.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(item, item));
            }
            if (_Data.CLS.Where(p => p.MaBNhan == _Mabn).Count() > 0)
            {
                GrcNhomDV.Enabled = true;
                pnLeft.Enabled = false;
                enableControl(true);
            }
            else
            {
                pnLeft.Enabled = true;
                GrcNhomDV.Enabled = false;
                trangthai = 1;
                enableControl(false);
            }
            //var kpAD = _kphong.Where(p => p.MaKP == DungChung.Bien.MaKP && p.PLoai == "Admin").ToList();
            //if (kpAD.Count > 0)
            //{
            //    lupKhoaphong.Properties.ReadOnly = false;
            //}
            //else
            //{
            //    lupKhoaphong.Properties.ReadOnly = true;
            //}
            string iddtbn = ";" + DTBN.ToString() + ";";
            var _lgoidv = _Data.DmGoiDVs.Where(p => p.Status == 1 && p.DSDTBN != null && p.DSDTBN.Contains(iddtbn)).Select(p => new { p.IDGoi, p.TenGoi }).ToList();
            lupGoiDV.Properties.DataSource = _lgoidv;

            var CheckCC = _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn).Where(p => p.CapCuu == 1).Where(p => p.NoiTru == 0 && p.DTNT == false).ToList();
            if (CheckCC.Count > 0)
                ckCapCuu.Checked = true;

            KhoiTaoDVAll();

            if (!string.IsNullOrWhiteSpace(_maIcd) && !action)
            {
                var gdv = _lgoidv.FirstOrDefault(o => o.TenGoi == _maIcd);
                if (gdv != null)
                {
                    btnMoi_Click(null, null);
                    lupGoiDV.EditValue = gdv.IDGoi;
                }
                else
                {
                    MessageBox.Show("Không có gói dịch vụ tương ứng với mã ICD : " + _maIcd);
                }
            }
            if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                GrvNhomDV.CollapseAllGroups();
            if ((DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") && MaKP_14018 != null && MaKP_14018 > 0)
            {
                lupKhoaphong.EditValue = MaKP_14018;
                lupKhoaphong.Enabled = false;
            }
            action = false;
        }
        bool action = false;
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (trangthai == 1 || trangthai == 2)
            {
                DialogResult dr = MessageBox.Show("Bạn chưa lưu kết quả. Bạn có muốn thoát? ", "Hỏi thoát?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.Dispose();
                }
            }
            else
            {
                this.Dispose();
            }
        }
        List<KPhong> _lkpnhom = new List<KPhong>();
        private void lupTN_EditValueChanged(object sender, EventArgs e)
        {
            grcDichvu.DataSource = "";
            if (lupTN.EditValue != null)
            {
                string _Idtn = lupTN.Text;
                mm_ThongKePK.Text = getSoBNChuaTH_CLS(dtNgayCD.DateTime, _Idtn)[0];
                khoitaodichvu(_Idtn, 0);
                var ktTN = (from tn in _listDichVu.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == ("Thăm dò chức năng") || p.TenNhomCT == "Thủ thuật, phẫu thuật" || p.TenNhomCT == "Xét nghiệm")
                            select tn).ToList();
                var chkTndv = (from tndv in _data.TieuNhomDVs
                               join nhomdv in _data.NhomDVs on tndv.IDNhom equals nhomdv.IDNhom
                               where nhomdv.TenNhomCT == ("Chẩn đoán hình ảnh") || nhomdv.TenNhomCT == ("Thăm dò chức năng") || nhomdv.TenNhomCT == ("Thủ thuật, phẫu thuật") || tndv.TenRG == ("XN Tế bào học") || nhomdv.TenNhomCT == ("Xét nghiệm")
                               select tndv.TenRG).ToList();
                ////Yêu cầu thêm Y lệnh cho bệnh viện 30003
                string s = "";
                if (radTenRG.EditValue != null)
                {
                    s = radTenRG.EditValue.ToString();
                }
                //if (DungChung.Bien.MaBV != "30003")
                if (true)
                {
                    if (ktTN.Count() > 0)
                    {
                        //pnYLenh.Visible = true;
                        colYLenh.Visible = true;
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            if (_Idtn == "XN hóa sinh máu" ||
                                _Idtn == "XN huyết học" ||
                                _Idtn == "XN nước tiểu" || _Idtn == "XN khác")
                            {
                                colYLenh.Caption = "Loại mẫu";
                            }
                            else
                            {
                                colYLenh.Caption = "Y lệnh";
                            }
                        }
                        else if (DungChung.Bien.MaBV == "30007")
                        {
                            //pnYLenh.Visible = true;
                            colYLenh.Visible = true;
                        }
                        else
                        {
                            //colYLenh.Visible = false;
                            pnYLenh.Visible = false;
                            if (DungChung.Bien.MaBV == "24012")
                            {
                                if (_Idtn == "XN hóa sinh máu" || _Idtn == "XN huyết học" || _Idtn == "XN nước tiểu" || _Idtn == "XN khác")
                                {
                                    colYLenh.Caption = "Loại mẫu";
                                    colYLenh.Visible = true;
                                }
                                else
                                {
                                    colYLenh.Caption = "Y lệnh";
                                    colYLenh.Visible = true;
                                }
                            }
                        }
                    }

                    else
                    {
                        foreach (var item in chkTndv)
                        {
                            if (s == item.ToString())
                            {
                                //pnYLenh.Visible = true;
                                colYLenh.Visible = true;
                                break;
                            }
                            else
                            {
                                pnYLenh.Visible = false;
                                colYLenh.Visible = false;
                            }

                        }
                    }


                    string makp = (";" + DungChung.Bien.MaKP + ";");



                    var _lDV2 = (from dv in _listDichVu
                                     //where (dv.TenRG == _Idtn && dv.Status == 1)
                                 select new
                                 {
                                     TenDV = dv.tendv,
                                     MaDV = dv.madv,
                                     TrongDM = dv.TrongBH,
                                     SoTT = dv.SoTT,
                                     MaKPsd = dv.MaKPsd == null ? "" : dv.MaKPsd

                                 }).OrderBy(p => p.TenDV).ThenBy(p => p.SoTT).ToList();
                    var _lDV = (from dvct in _lDV2
                                    //where (DungChung.Bien.PLoaiKP == "Admin" ? true : dvct.MaKPsd.Contains(makp))
                                group dvct by new { dvct.MaDV, dvct.TenDV, dvct.TrongDM, dvct.SoTT } into kq
                                select new
                                {
                                    kq.Key.MaDV,
                                    kq.Key.TenDV,
                                    TrongDM = kq.Key.TrongDM,//DTBN == 1 ? 1 : 0,
                                    kq.Key.SoTT
                                }).OrderBy(p => p.TenDV).ThenBy(p => p.SoTT).ToList();
                    grcDichvu.DataSource = _listDichVu.Where(p => p.TenRG == _Idtn).OrderBy(p => p.SoTT).ThenBy(p => p.tendv).ToList();
                    lupTenDV.Properties.DataSource = _lDV.ToList();
                    //
                    if (lupTN.EditValue != null)
                    {
                        dtNgayCD.DateTime = System.DateTime.Now;
                        if (CDNhieuTieuNhom)//chỉ định nhiều tiểu nhóm thì kp thực hiện set tự động theo chuyên khoa
                        {
                            LupKpThuchien.Visible = false;
                            labelControl2.Visible = false;
                        }
                        else
                        {
                            LupKpThuchien.Visible = true;
                            labelControl2.Visible = true;
                            string ChuyenKhoa = lupTN.Text.ToString();

                            if (ChuyenKhoa.Contains("XN") && (DungChung.Bien.MaBV != "30299" || ChuyenKhoa != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh) && (DungChung.Bien.MaBV != "27023" || ChuyenKhoa != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc))
                                ChuyenKhoa = "Xét nghiệm";
                            List<KPhong> _lkphong = new List<KPhong>();

                            if (ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat)
                            {
                                int _makp = 0;
                                if (lupKhoaphong.EditValue != null)
                                {
                                    _makp = Convert.ToInt32(lupKhoaphong.EditValue);
                                }
                                _lkphong = (from KP in _kphong.Where(p => p.MaKP == _makp || p.MaKP == DungChung.Bien.MaKP || p.ChuyenKhoa == "Nội soi" || (p.ChuyenKhoa != null && p.ChuyenKhoa.ToLower() == ChuyenKhoa.ToLower())) select KP).OrderBy(p => p.TenKP).ToList();
                                if (ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && _lkphong.Where(p => p.ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).ToList().Count > 0)
                                    LupKpThuchien.EditValue = _lkphong.Where(p => p.ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).First().MaKP;
                                else
                                    if (_lkphong.Where(p => p.MaKP == (_makp)).ToList().Count > 0)
                                {
                                    LupKpThuchien.EditValue = _lkphong.Where(p => p.MaKP == _makp).First().MaKP;
                                }
                            }
                            else if (ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS)
                            {
                                _lkphong = (from KP in _kphong.Where(p => p.ChuyenKhoa != null && (p.ChuyenKhoa.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS.ToLower() || p.ChuyenKhoa.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang.ToLower())) select KP).OrderBy(p => p.TenKP).ToList();
                                if (_lkphong.Count > 0)
                                {
                                    var qkts = _lkphong.Where(p => p.ChuyenKhoa.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS.ToLower()).ToList();
                                    if (qkts.Count > 0)
                                        LupKpThuchien.EditValue = qkts.First().MaKP;
                                    else
                                    {
                                        LupKpThuchien.EditValue = _lkphong.First().MaKP;
                                    }
                                }

                            }
                            else if (ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc)
                            {
                                _lkphong = (from KP in _kphong.Where(p => p.ChuyenKhoa != null && (p.ChuyenKhoa.ToLower() == (ChuyenKhoa.ToLower()) || (p.ChuyenKhoa.ToLower() == "xét nghiệm"))) select KP).OrderBy(p => p.TenKP).ToList();
                                if (_lkphong.Count > 0)
                                {
                                    if (_lkphong.Count > 0)
                                        LupKpThuchien.EditValue = _lkphong.First().MaKP;
                                }
                            }
                            else
                            {
                                _lkphong = (from KP in _kphong.Where(p => p.ChuyenKhoa != null && p.ChuyenKhoa.ToLower() == (ChuyenKhoa.ToLower())) select KP).OrderBy(p => p.TenKP).ToList();
                                if (_lkphong.Count > 0)
                                {
                                    if (_lkphong.Count > 0)
                                        LupKpThuchien.EditValue = _lkphong.First().MaKP;
                                }
                            }
                            LupKpThuchien.Properties.DataSource = _lkphong;
                            _lkpnhom.Clear();
                            _lkpnhom = _lkphong;
                        }
                        var q = (from tn in _data.TieuNhomDVs
                                 join nhom in _data.NhomDVs.Where(p => p.TenNhomCT == ("Xét nghiệm") || p.TenNhomCT == ("Chẩn đoán hình ảnh") || p.TenNhomCT == ("Thăm dò chức năng") || p.TenNhomCT == ("Thủ thuật, phẫu thuật")) on tn.IDNhom equals nhom.IDNhom
                                 select new { tn.IDNhom, tn.TenRG, tn.Status }).Distinct().OrderBy(p => p.IDNhom).ThenBy(p => p.TenRG).ToList();
                        string[] ten = new string[10];
                        if (q.Count > 0)
                            ten = q.Where(p => p.Status != 0).Select(p => p.TenRG).Distinct().ToArray();
                        string tenrg = lupTN.Text;
                        int a = 0;
                        for (int i = 0; i <= ten.Count(); i++)
                        {
                            if (ten[i] == tenrg)
                            {
                                a = i;
                                break;
                            }
                        }
                        if (a > 0)
                            radTenRG.SelectedIndex = a;
                    }
                    //
                }
            }
        }
        public class TTCD
        {
            private string TT;
            private int IDCD;
            public string tt
            {
                set { TT = value; }
                get { return TT; }
            }
            public int idcd
            {
                set { IDCD = value; }
                get { return IDCD; }
            }
        }
        public class C_DichVu
        {
            private string TenDV;
            private int MaDV;
            private double dgia;
            private int idTieuNhom;
            private string Nhom;
            private string idcls;
            private string yLenh;
            private int IDCD;
            private int sott;
            private int trangthai;
            private int IDCLSCT;
            private bool trongbh;
            private bool theoyc;
            private bool chon;
            string maQD;
            string maKPsd;
            private bool cpdinhkem;
            public bool XHH { set; get; }
            private int makpth { get; set; }
            private int idnhom { get; set; }
            private string tenrg { get; set; }
            private string canhbaohm { get; set; }
            private int? gHCDTrongNgay { get; set; }
            public string YLenh2 { get; set; }
            public string TenPP { get; set; }
            public string TenDungCu { get; set; }
            public string NDVaoVien { get; set; }
            public bool? IsAutoExecute { get; set; }
            public int? SoPhutThucHien { get; set; }
            public string TenRG
            {
                get { return tenrg; }
                set { tenrg = value; }
            }

            public int? GHCDTrongNgay
            {
                get { return gHCDTrongNgay; }
                set { gHCDTrongNgay = value; }
            }
            //private string tenrg { get; set; }
            public string CanhBaoHM
            {
                get { return canhbaohm; }
                set { canhbaohm = value; }
            }

            public string MaKPsd
            {
                get { return maKPsd; }
                set { maKPsd = value; }
            }
            double donGia;

            public double DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            public string MaQD
            {
                get { return maQD; }
                set { maQD = value; }
            }
            double donGia2;

            public double DonGia2
            {
                get { return donGia2; }
                set { donGia2 = value; }
            }
            public int IdTieuNhom
            {
                get { return idTieuNhom; }
                set { idTieuNhom = value; }
            }
            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
            public bool TheoYC
            {
                get { return theoyc; }
                set { theoyc = value; }
            }

            public int idclsct
            {
                set { IDCLSCT = value; }
                get { return IDCLSCT; }
            }
            public int Trangthai
            {
                set { trangthai = value; }
                get { return trangthai; }
            }
            public int SoTT
            {
                set { sott = value; }
                get { return sott; }
            }
            public int idcd
            {
                get { return IDCD; }
                set { IDCD = value; }
            }
            public string YLenh
            {
                get { return yLenh; }
                set { yLenh = value; }
            }
            public string tendv
            {
                get { return TenDV; }
                set { TenDV = value; }
            }
            public int madv
            {
                get { return MaDV; }
                set { MaDV = value; }
            }
            public double dongia
            {
                get { return dgia; }
                set { dgia = value; }
            }
            public string nhom
            {
                get { return Nhom; }
                set { Nhom = value; }
            }
            public string Idcls
            {
                get { return idcls; }
                set { idcls = value; }
            }
            public bool TrongBH
            {
                get { return trongbh; }
                set { trongbh = value; }
            }
            string tenNhomCT;

            public string TenNhomCT
            {
                get { return tenNhomCT; }
                set { tenNhomCT = value; }
            }
            public bool CPDinhKem
            {
                get { return cpdinhkem; }
                set { cpdinhkem = value; }
            }
            public int MaKPTH
            {
                get { return makpth; }
                set { makpth = value; }
            }
            public int IdNhom
            {
                get { return idnhom; }
                set { idnhom = value; }
            }

        }
        public class Dichvumoi
        {
            private int Madv;
            private string Tendv;
            private string Chon;
            private string Gia;
            private string Tnhom;
            private int trongbh;
            private int sott;
            private int? gHCDTrongNgay;

            public int? GHCDTrongNgay
            {
                get { return gHCDTrongNgay; }
                set { gHCDTrongNgay = value; }
            }
            public string tnhom
            {
                get { return Tnhom; }
                set { Tnhom = value; }
            }
            public int TrongBH
            {
                get { return trongbh; }
                set { trongbh = value; }
            }
            public int SoTT
            {
                get { return sott; }
                set { sott = value; }
            }
            public int madv
            {
                get { return Madv; }
                set { Madv = value; }
            }
            public string tendv
            {
                get { return Tendv; }
                set { Tendv = value; }
            }
            public string chon
            {
                get { return Chon; }
                set { Chon = value; }
            }
            public string gia
            {
                get { return Gia; }
                set { Gia = value; }
            }
        }
        public class DichVuAll
        {
            public string TenRGdv { get; set; }
            public string TenNhomCT { get; set; }
            public string MaQD { get; set; }
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public int TrongDM { get; set; }
            public string MaKPsd { get; set; }
            public int SoTT { get; set; }
            public double DonGiaBHYT { get; set; }
            public string DSDTBN { get; set; }
            public double DonGia { get; set; }
            public string TenRG { get; set; }
            public double DonGia2 { get; set; }
            public double DonGiaDV2 { set; get; }
            public string IDGoi { get; set; }
            public int IdNhom { get; set; }
            public int HMChiDinh { get; set; }
            public double DonGiaTT15 { get; set; }//Đơn giá mới từ ngày 15-07-2018
            public double DonGiaTT39 { get; set; }//Đơn giá mới từ ngày 15-12-2018
            public string CanhBaoHM { get; set; }
            public int? GHCDTrongNgay { get; set; }
            public bool? IsAutoExecute { get; set; }
            public int? SoPhutThucHien { get; set; }
        }
        List<DichVuAll> _lDichVuTH = new List<DichVuAll>();
        void KhoiTaoDVAll()
        {
            _lDichVuTH.Clear();
            //if(DungChung.Bien.PLoaiKP)
            string makp = ";";
            if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                makp = ";" + DungChung.Bien.MaKP + ";";
            _lDichVuTH = (from dvct in _data.DichVucts
                          join dv in _data.DichVus.Where(p => p.Status == 1).Where(p => p.PLoai == 2).Where(p => DungChung.Bien.MaBV == "01071" ? p.Loai != 4 : true) on dvct.MaDV equals dv.MaDV
                          join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                          join nhom in _data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                          select new DichVuAll
                          {
                              DonGiaDV2 = dv.DonGiaDV2,
                              TenRGdv = dv.TenRG,
                              IdNhom = nhom.IDNhom,
                              TenNhomCT = nhom.TenNhomCT,
                              MaQD = dv.MaQD,
                              MaDV = dv.MaDV,
                              TenDV = dv.TenDV,
                              TrongDM = dv.TrongDM ?? 0,
                              MaKPsd = dv.MaKPsd,
                              SoTT = dv.SoTT ?? 0,
                              DonGiaBHYT = dv.DonGiaBHYT,
                              DSDTBN = dv.DSDTBN,
                              DonGia = dv.DonGia,
                              TenRG = tn.TenRG,
                              DonGia2 = dv.DonGia2,
                              IDGoi = dv.IDGoi,
                              DonGiaTT15 = dv.DonGiaTT15,
                              DonGiaTT39 = dv.DonGiaTT39,
                              HMChiDinh = dv.HMChiDinh,
                              CanhBaoHM = dv.CanhBaoHM,
                              IsAutoExecute = dv.IsAutoExecute,
                              SoPhutThucHien = dv.SoPhutThucHien
                          }).ToList();
            _lDichVuTH = _lDichVuTH.Where(p => p.MaKPsd.Contains(makp)).ToList();
        }
        BenhNhan benhnhan = new BenhNhan();
        VaoVien vaovien = new VaoVien();
        //public bool GiaCu(int mabn, int TrongDM, DateTime ngaychidinh)
        //{
        //    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    bool giacu = false;


        //    if (benhnhan != null)
        //    {
        //        DateTime ngaythang = DungChung.Ham.NgayTu(DungChung.Bien.ngayGiaMoiDV);
        //        if (benhnhan.DTuong == "BHYT" && TrongDM == 1)
        //            ngaythang = DungChung.Ham.NgayTu(DungChung.Bien.ngayGiaMoi);
        //        if (ngaychidinh.Date < ngaythang)
        //        {
        //            giacu = true;
        //        }
        //        else
        //        {
        //            giacu = false;
        //        }

        //        if (vaovien != null && vaovien.NgayVao != null && vaovien.NgayVao.Value.Date < ngaythang)
        //        {
        //            giacu = true;
        //        }
        //        if (vaovien == null && benhnhan.NoiTru == 0 && benhnhan.NNhap.Value.Date < ngaythang)//DÙng chung DungChung.Bien.MaBV == "30007" && 
        //            giacu = true;
        //    }
        //    return giacu;
        //}
        //public double _getGiaDM(List<DichVuAll> _lDichVuTH, int maDV, int trongDM, int mabn, DateTime ngaychidinh)
        //{
        //    bool giacu = GiaCu(mabn, trongDM, ngaychidinh);
        //    double gia = 0;
        //    var q = _lDichVuTH.Where(p => p.MaDV == maDV).FirstOrDefault();
        //    if (q != null)
        //    {
        //        if (trongDM == 1)
        //        {
        //            if (giacu)
        //                gia = q.DonGiaTT15;
        //            else
        //                gia = q.DonGiaTT39;
        //            //if (giacu)
        //            //    gia = q.DonGia;
        //            //else
        //            //    gia = q.DonGiaBHYT;
        //        }
        //        else
        //        {
        //            //if (DungChung.Bien.MaBV == "30009")
        //            //{
        //            if (giacu)
        //                gia = q.DonGia2; // giá DV cũ
        //            else
        //                gia = q.DonGiaDV2; // giá  DV mới
        //            //}
        //            //else
        //            //{
        //            //    gia = q.First().DonGia2;

        //            //}

        //        }
        //    }
        //    return gia;
        //}
        bool CDNhieuTieuNhom = false;
        private void khoitaodichvu(string Idtieunhom, int status)
        {
            if (trangthai != 2)
            {
                if (status == 1)
                    _listDichVu.Clear();
                var kttenrg = _listDichVu.Where(p => p.TenRG == Idtieunhom).ToList();//tiểu nhóm nào đã khở tạo dịch vụ rồi thì ko add thêm nữa

                if (kttenrg.Count() == 0)
                {
                    var qdtbn = _data.DTBNs.Where(p => p.DTBN1 == "BHYT").FirstOrDefault();
                    int idDTBH = 1;
                    if (qdtbn != null)
                        idDTBH = qdtbn.IDDTBN;
                    string makp = ";" + DungChung.Bien.MaKP + ";";
                    var IDDTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).Select(p => p.IDDTBN).FirstOrDefault();
                    string iddtbn = ";" + (IDDTBN == null ? "-100" : IDDTBN.ToString()) + ";";
                    var q = (from dv in _lDichVuTH.Where(p => p.TenRG == Idtieunhom)
                             where (DungChung.Bien.PLoaiKP == "Admin" ? true : dv.MaKPsd.Contains(makp))
                             where (dv.DSDTBN != null && dv.DSDTBN.Contains(iddtbn))
                             group new { dv } by new { dv.TenRGdv, dv.CanhBaoHM, dv.TenNhomCT, dv.MaQD, dv.MaDV, dv.TenDV, dv.IdNhom, dv.TrongDM, dv.SoTT, dv.DonGiaBHYT, dv.DonGia, dv.TenRG, dv.DonGia2, dv.GHCDTrongNgay, dv.IsAutoExecute, dv.SoPhutThucHien } into kq
                             select new
                             {
                                 kq.Key.TenNhomCT,
                                 kq.Key.MaQD,
                                 kq.Key.MaDV,
                                 kq.Key.TenDV,
                                 kq.Key.TenRGdv,
                                 TrongDM = DTBN == idDTBH ? kq.Key.TrongDM : 0,
                                 kq.Key.SoTT,
                                 // kq.Key.IdTieuNhom,
                                 kq.Key.TenRG,
                                 kq.Key.DonGia,
                                 kq.Key.DonGia2,
                                 kq.Key.DonGiaBHYT,
                                 kq.Key.IdNhom,
                                 kq.Key.CanhBaoHM,
                                 kq.Key.GHCDTrongNgay,
                                 kq.Key.IsAutoExecute,
                                 kq.Key.SoPhutThucHien
                             }).ToList();
                    if (q.Count > 0)
                    {
                        foreach (var item in q)
                        {
                            C_DichVu themmoi = new C_DichVu();
                            themmoi.TenNhomCT = item.TenNhomCT;
                            themmoi.MaQD = item.MaQD;
                            themmoi.madv = item.MaDV;
                            if ((DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24272") && !string.IsNullOrEmpty(item.TenRGdv))
                                themmoi.tendv = item.TenRGdv;
                            else
                                themmoi.tendv = item.TenDV;
                            themmoi.dongia = item.DonGia;
                            themmoi.DonGia2 = item.DonGia2;
                            themmoi.DonGia = DungChung.Ham._getGiaDM(_data, item.MaDV, item.TrongDM, _Mabn, dtNgayCD.DateTime);
                            themmoi.nhom = item.TenRG;
                            themmoi.Chon = false;
                            if (item.SoTT != null)
                                themmoi.SoTT = item.SoTT;
                            else
                                themmoi.SoTT = 1000;
                            themmoi.TrongBH = DTBN == idDTBH ? (item.TrongDM == 1 || item.TrongDM == 2) ? true : false : false;//Cp đính kèm với bndv => ngoài dm
                            themmoi.CPDinhKem = DTBN == idDTBH ? (item.TrongDM == 2 ? true : false) : false;
                            themmoi.IdNhom = item.IdNhom;
                            themmoi.TenRG = item.TenRG;
                            themmoi.CanhBaoHM = item.CanhBaoHM;
                            themmoi.GHCDTrongNgay = item.GHCDTrongNgay;
                            themmoi.SoPhutThucHien = item.SoPhutThucHien;
                            themmoi.IsAutoExecute = item.IsAutoExecute;
                            _listDichVu.Add(themmoi);
                        }
                    }
                }
                var htt = _listDichVu.Select(p => p.TenRG).Distinct().ToList();
                if (htt.Count > 1 && _listDichVu.Where(p => p.Chon == true).ToList().Count() > 0)
                {
                    CDNhieuTieuNhom = true;
                }
                else
                {
                    CDNhieuTieuNhom = false;
                }

            }
        }

        void guiChiDinh1(List<ChiDinhToPacs> data)
        {

            //_ldata.Add(data);

            if (data.Count() > 0)
            {
                //DialogResult dlr = MessageBox.Show("Bạn muốn gửi dữ liệu lên cổng của đối tác RIS/PACS? - Thao tác này sẽ cần một chút thời gian", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dlr == DialogResult.No)
                //    return;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                int flag = 0;
                foreach (var item in data)
                {
                    string ngaysinh = "";
                    string thangsinh = "";
                    string ngaythang = "";
                    if (string.IsNullOrEmpty(item.NgaySinh.Trim()))
                        ngaythang = "01";
                    else if (Int32.Parse(item.NgaySinh) < 10)
                        ngaysinh = "0" + item.NgaySinh;
                    else
                        ngaysinh = item.NgaySinh;

                    if (string.IsNullOrEmpty(item.ThangSinh.Trim()))
                        ngaythang = "01";
                    else if (Int32.Parse(item.ThangSinh) < 10)
                        thangsinh = "0" + item.ThangSinh;
                    else
                        thangsinh = item.ThangSinh;

                    var values = new JObject();
                    values.Add("MaChiDinh", item.MaChiDinh.ToString());
                    values.Add("ThoiGianChiDinh", item.ThoiGianChiDinh.Value.ToString("yyyyMMddHHmmss"));
                    values.Add("MaBenhNhan", item.MaBenhNhan.ToString());
                    values.Add("TenBenhNhan", item.TenBenhNhan);
                    values.Add("NgaySinh", string.IsNullOrEmpty(ngaythang) ? "" : item.NamSinh + thangsinh + ngaysinh);
                    values.Add("GioiTinh", item.GioiTinh);
                    values.Add("DiaChi", item.DiaChi);
                    values.Add("SDT", item.SDT);
                    values.Add("NoiChiDinh", item.NoiChiDinh);
                    values.Add("MaBacSiChiDinh", item.MaBacSiChiDinh);
                    values.Add("TenBacSiChiDinh", item.TenBacSiChiDinh);
                    values.Add("MaDichVu", item.MaDichVu.ToString());
                    values.Add("TenDichVu", item.TenDichVu);
                    values.Add("NhomDichVu", item.NhomDichVu.ToString());
                    values.Add("ChanDoan", item.ChanDoan);
                    values.Add("TrangThai", item.TrangThai);

                    HttpContent content = new StringContent(values.ToString(), Encoding.UTF8, "application/json");
                    var response = client.PostAsJsonAsync("http://101.99.14.170:7777", values).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        flag++;
                        ChiDinh sua = _data.ChiDinhs.Where(p => p.IDCD == item.MaChiDinh).FirstOrDefault();
                        if (sua != null)
                        {
                            sua.Pacs_Status = true;
                            _data.SaveChanges();
                        }

                    }
                }

                MessageBox.Show("Đã gửi thành công " + flag + " chỉ định");
            }
        }

        private void khoitaodichvu2(string idgoi)
        {
            if (trangthai != 2)
            {
                var qdtbn = _data.DTBNs.Where(p => p.DTBN1 == "BHYT").FirstOrDefault();
                int idDTBH = 1;
                if (qdtbn != null)
                    idDTBH = qdtbn.IDDTBN;
                _listDichVu.Clear();
                string makp = ";" + DungChung.Bien.MaKP + ";";
                var IDDTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).Select(p => p.IDDTBN).FirstOrDefault();
                string iddtbn = ";" + (IDDTBN == null ? "-100" : IDDTBN.ToString()) + ";";
                var q = (from dv in _lDichVuTH.Where(p => p.IDGoi != null && p.IDGoi.Contains(idgoi))
                         where (DungChung.Bien.PLoaiKP == "Admin" ? true : dv.MaKPsd.Contains(makp))
                         where (dv.DSDTBN != null && dv.DSDTBN.Contains(iddtbn))
                         group new { dv } by new { dv.TenRGdv, dv.TenNhomCT, dv.CanhBaoHM, dv.MaQD, dv.IdNhom, dv.MaDV, dv.TenDV, dv.TrongDM, dv.SoTT, dv.DonGiaBHYT, dv.DonGia, dv.TenRG, dv.DonGia2, dv.GHCDTrongNgay } into kq
                         select new
                         {
                             kq.Key.TenNhomCT,
                             kq.Key.MaQD,
                             kq.Key.MaDV,
                             kq.Key.TenDV,
                             kq.Key.TenRGdv,
                             TrongDM = DTBN == idDTBH ? kq.Key.TrongDM : 0,
                             kq.Key.SoTT,
                             // kq.Key.IdTieuNhom,
                             kq.Key.TenRG,
                             kq.Key.DonGia,
                             kq.Key.DonGia2,
                             kq.Key.DonGiaBHYT,
                             kq.Key.IdNhom,
                             kq.Key.CanhBaoHM,
                             kq.Key.GHCDTrongNgay
                         }).ToList();
                if (q.Count > 0)
                {
                    foreach (var item in q)
                    {
                        C_DichVu themmoi = new C_DichVu();
                        themmoi.TenNhomCT = item.TenNhomCT;
                        themmoi.MaQD = item.MaQD;
                        themmoi.madv = item.MaDV;
                        if ((DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24272") && !string.IsNullOrEmpty(item.TenRGdv))
                            themmoi.tendv = item.TenRGdv;
                        else
                            themmoi.tendv = item.TenDV;
                        themmoi.dongia = item.DonGia;
                        themmoi.DonGia2 = item.DonGia2;
                        themmoi.DonGia = DungChung.Ham._getGiaDM(_data, item.MaDV, item.TrongDM, _Mabn, dtNgayCD.DateTime);// _getGiaDM(_lDichVuTH, item.MaDV, item.TrongDM, _Mabn, dtNgayCD.DateTime);
                        themmoi.nhom = item.TenRG;
                        themmoi.Chon = true;
                        if (item.SoTT != null)
                            themmoi.SoTT = item.SoTT;
                        else
                            themmoi.SoTT = 1000;
                        themmoi.TrongBH = DTBN == idDTBH ? ((item.TrongDM == 1 || item.TrongDM == 2) ? true : false) : false;//Cp đính kèm với bndv => ngoài dm
                        themmoi.CPDinhKem = DTBN == idDTBH ? (item.TrongDM == 2 ? true : false) : false;
                        themmoi.IdNhom = item.IdNhom;
                        themmoi.CanhBaoHM = item.CanhBaoHM;
                        themmoi.GHCDTrongNgay = item.GHCDTrongNgay;
                        _listDichVu.Add(themmoi);
                    }
                }
                #region
                //string makp = ";" + DungChung.Bien.MaKP + ";";
                //var IDDTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).Select(p => p.IDDTBN).FirstOrDefault();
                //string iddtbn = ";" + (IDDTBN == null ? "-100" : IDDTBN.ToString()) + ";";


                //var q2 = (from dvct in _data.DichVucts
                //          join dv in _data.DichVus.Where(p => p.Status == 1) on dvct.MaDV equals dv.MaDV
                //          join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                //          join nhom in _data.NhomDVs on tn.IDNhom equals nhom.IDNhom
                //          select new { TenRGdv = dv.TenRG, nhom.TenNhomCT, dv.MaQD, dv.MaDV, dv.TenDV, dv.TrongDM, dv.MaKPsd, dv.SoTT, dv.DonGiaBHYT, dv.DSDTBN, dv.DonGia, tn.TenRG, dv.DonGia2 }).ToList();


                //var q = (from dv in q2.Where(p => p.TenRG == Idtieunhom)
                //         where (DungChung.Bien.PLoaiKP == "Admin" ? true : dv.MaKPsd.Contains(makp))
                //         where (dv.DSDTBN != null && dv.DSDTBN.Contains(iddtbn))
                //         group new { dv } by new { dv.TenRGdv, dv.TenNhomCT, dv.MaQD, dv.MaDV, dv.TenDV, dv.TrongDM, dv.SoTT, dv.DonGiaBHYT, dv.DonGia, dv.TenRG, dv.DonGia2 } into kq
                //         select new
                //         {
                //             kq.Key.TenNhomCT,
                //             kq.Key.MaQD,
                //             kq.Key.MaDV,
                //             kq.Key.TenDV,
                //             kq.Key.TenRGdv,
                //             TrongDM = DTBN == 1 ? kq.Key.TrongDM : 0,
                //             kq.Key.SoTT,
                //             // kq.Key.IdTieuNhom,
                //             kq.Key.TenRG,
                //             kq.Key.DonGia,
                //             kq.Key.DonGia2,
                //             kq.Key.DonGiaBHYT
                //         }).ToList();
                //if (q.Count > 0)
                //{
                //    foreach (var item in q)
                //    {
                //        C_DichVu themmoi = new C_DichVu();
                //        themmoi.TenNhomCT = item.TenNhomCT;
                //        themmoi.MaQD = item.MaQD;
                //        themmoi.madv = item.MaDV;
                //        if (DungChung.Bien.MaBV == "20001" && !string.IsNullOrEmpty(item.TenRGdv))
                //            themmoi.tendv = item.TenRGdv;
                //        else
                //            themmoi.tendv = item.TenDV;
                //        themmoi.dongia = item.DonGia;
                //        themmoi.DonGia2 = item.DonGia2;
                //        themmoi.DonGia = DungChung.Ham._getGiaDM(_data, item.MaDV, item.TrongDM ?? 0, _Mabn, dtNgayCD.DateTime);

                //        //if (item.TrongDM == 1)
                //        //{
                //        //    if (DungChung.Ham.GiaCu(_Mabn, dtNgayCD.DateTime))
                //        //        themmoi.DonGia = item.DonGia;
                //        //    else
                //        //        themmoi.DonGia = item.DonGiaBHYT;
                //        //}
                //        //else
                //        //    themmoi.DonGia = item.DonGia2;
                //        //// themmoi.IdTieuNhom = item.IdTieuNhom;
                //        themmoi.nhom = item.TenRG;
                //        themmoi.Chon = false;
                //        if (item.SoTT != null)
                //            themmoi.SoTT = item.SoTT.Value;
                //        else
                //            themmoi.SoTT = 1000;
                //        themmoi.TrongBH = item.TrongDM == 0 ? false : true;
                //        _listDichVu.Add(themmoi);
                //    }
                //}
                #endregion
            }
        }
        private void grvDichvu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            mmYlenh.Text = "";
        }
        private void grvDichvu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int _MaDV = grvDichvu.GetFocusedRowCellValue(MaDV) == null ? 0 : Convert.ToInt32(grvDichvu.GetFocusedRowCellValue(MaDV));
            //if (DungChung.Bien.MaBV == "24009")
            //{
            //    var _GioiHan = ((C_DichVu)grvDichvu.GetRow(e.RowHandle)).GHCDTrongNgay;
            //    var now = DateTime.Now.Date;
            //    if (_GioiHan != null)
            //    {
            //        var _chiDinhDV = (from cls in _data.CLS
            //                          join cd in _data.ChiDinhs.Where(o => o.MaDV == _MaDV) on cls.IdCLS equals cd.IdCLS
            //                          select cls).ToList();
            //        var _chiDinhTrongNgay = _chiDinhDV.Where(o => o.NgayThang.Value.Date == DateTime.Now.Date).ToList();
            //        if (_chiDinhTrongNgay.Count >= _GioiHan)
            //        {
            //            MessageBox.Show("Dịch vụ đã quá mức giới hạn chỉ định trong ngày");
            //        }
            //    }
            //}

            string _T = grvDichvu.GetFocusedRowCellValue(TenDV).ToString();

            if (_listDichVu.Where(p => p.madv == _MaDV).Count() > 0)
            {
                C_DichVu sua = _listDichVu.Where(p => p.madv == _MaDV).First();
                if (e.Column == colTrongBH)// Nếu click vào cột TrongBH
                {
                    if (sua.TrongBH)
                    {
                        sua.TrongBH = false;
                        double gia = DungChung.Ham._getGiaDM(_data, _MaDV, 0, _Mabn, dtNgayCD.DateTime);
                        grvDichvu.SetFocusedRowCellValue(colDonGia, gia.ToString("##,####"));
                        sua.DonGia = gia; //sua.DonGia2;
                    }
                    else
                    {
                        sua.TrongBH = true;
                        double gia = DungChung.Ham._getGiaDM(_data, _MaDV, 1, _Mabn, dtNgayCD.DateTime);
                        grvDichvu.SetFocusedRowCellValue(colDonGia, gia.ToString("##,####"));// sua.dongia.ToString("##,####"));
                        sua.DonGia = gia;// sua.dongia;
                    }
                    if (sua.Chon)
                    {
                        sua.YLenh = mmYlenh.Text;
                        sua.SoTT = 1;
                        sua.Trangthai = 0;
                    }
                    if (sua.TheoYC)
                    {
                        sua.TheoYC = false;
                        grvDichvu.FocusedColumn = colTenDV;
                    }
                }
                else if (e.Column == colChon) // Nếu click vào cột Chọn  
                {
                    mmYlenh.Text = "";
                    if (sua.Chon == false)
                    {
                        sua.Chon = true;
                        colXHH.OptionsColumn.ReadOnly = false;
                    }
                    else if (sua.Chon == true)
                    {
                        sua.Chon = false;
                    }
                    sua.YLenh = mmYlenh.Text;
                    sua.SoTT = 1;
                    sua.Trangthai = 0;
                }
                if (e.Column == colTheoYC && sua.TheoYC == false)
                {

                    sua.TrongBH = false;
                    sua.TheoYC = true;
                    grvDichvu.FocusedColumn = colTenDV;
                    if (sua.TheoYC)
                    {
                        sua.TheoYC = true;
                        double gia = DungChung.Ham._getGiaDM(_data, _MaDV, 0, _Mabn, dtNgayCD.DateTime);
                        grvDichvu.SetFocusedRowCellValue(colDonGia, gia.ToString("##,####"));
                        sua.DonGia = gia; //sua.DonGia2;
                    }
                    else
                    {
                        sua.TheoYC = false;
                        double gia = DungChung.Ham._getGiaDM(_data, _MaDV, 1, _Mabn, dtNgayCD.DateTime);
                        grvDichvu.SetFocusedRowCellValue(colDonGia, gia.ToString("##,####"));// sua.dongia.ToString("##,####"));
                        sua.DonGia = gia;// sua.dongia;
                    }
                }
            }
            grcCLS.DataSource = null;
            grcCLS.DataSource = _listDichVu.Where(p => p.Chon == true).ToList();

        }
        private bool KTLuu()
        {
            int idcdluu = 0;
            if (string.IsNullOrEmpty(lupKhoaphong.Text))
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng chỉ định");
                lupKhoaphong.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupNguoiKhamkb.Text))
            {
                MessageBox.Show("Bạn chưa chọn B.Sĩ chỉ định");
                lupNguoiKhamkb.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(LupKpThuchien.Text) && CDNhieuTieuNhom == false)
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng thực hiện");
                LupKpThuchien.Focus();
                return false;
            }
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ktrarv = data.RaViens.Where(p => p.MaBNhan == _Mabn).ToList();
            if (ktrarv.Count > 0)
            {
                MessageBox.Show("Bạn đã ra viện, không thể lưu");
                return false;
            }
            if (DungChung.Bien.MaBV == "24009" && DTBN == 1 && !string.IsNullOrEmpty(StheBHYT))
            {
                if (LupKpThuchien.EditValue != null)
                {
                    int maKP = int.Parse(LupKpThuchien.EditValue.ToString());
                    var kp = data.KPhongs.FirstOrDefault(o => o.MaKP == maKP);
                    if (kp != null)
                    {
                        var now = DateTime.Now.Date;
                        if (kp.GHCD != null)
                        {
                            var _chiDinhDV = (from cls in _data.CLS.Where(o => o.MaKPth == kp.MaKP)
                                              join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                              select cls).ToList();
                            var _chiDinhTrongNgay = _chiDinhDV.Where(o => o.NgayThang.Value.Date == DateTime.Now.Date).ToList();
                            if (_chiDinhTrongNgay.Count >= kp.GHCD)
                            {
                                if (MessageBox.Show(string.Format("Phòng '{0}' đã quá mức giới hạn chỉ định trong ngày. Bạn có muốn tiếp tục lưu không?", kp.TenKP), "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    RefreshGridDichVu();
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            if ((DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") && _idDB > 0)
            {
                var db = _data.DienBiens.FirstOrDefault(o => o.ID == _idDB);
                if (db != null)
                {
                    if (dtNgayCD.EditValue != null && db.NgayNhap != null && dtNgayCD.DateTime.Date == db.NgayNhap.Value.Date && dtNgayCD.DateTime < db.NgayNhap)
                    {
                        MessageBox.Show("Ngày chỉ định phải lớn hơn ngày nhập diễn biến");
                        return false;
                    }
                }
            }

            int makpcd = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
            var bn = _data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _Mabn);
            if (bn != null)
            {
                if (dtNgayCD.DateTime <= bn.NNhap)
                {
                    MessageBox.Show("Ngày chỉ định phải lớn hơn ngày nhập bệnh nhân!");
                    return false;
                }
            }



            return true;
        }

        bool _tamthu = true;
        private void grvCLS_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "Xoa")
            {
                bool kt = true;
                if (trangthai == 0)
                {
                    kt = false;
                    MessageBox.Show("Bạn cần nhấn sửa trước khi xoá chi tiết!");
                }
                if (trangthai == 1)
                {
                    int _M = grvCLS.GetFocusedRowCellValue(MaDV) == null ? 0 : Convert.ToInt32(grvCLS.GetFocusedRowCellValue(MaDV));
                    foreach (var item in _listDichVu.Where(p => p.Chon == true && p.madv == _M))
                    {
                        item.Chon = false;
                    }
                    grcDichvu.DataSource = _listDichVu.OrderBy(p => p.SoTT).ThenBy(p => p.tendv);
                    grcCLS.DataSource = _listDichVu.Where(p => p.Chon == true).ToList();
                }
                if (trangthai == 2)
                {
                    int _idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                    int _idCD = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colIDCD));
                    var ktKP = from cls in _data.CLS.Where(p => p.IdCLS == _idCLS) select cls;
                    var ktAdmin = _kphong.Where(p => p.MaKP == DungChung.Bien.MaKP && p.PLoai == "Admin").ToList();
                    // int ktrakpChiDinh = (from cls in ktKP.Where(p => p.MaKP != DungChung.Bien.MaKP) select cls).Count();
                    if (ktAdmin.Count == 0 && (ktKP.Count() > 0 && ktKP.First().MaKP != DungChung.Bien.MaKP))
                    {
                        kt = false;
                        MessageBox.Show("Không thể xóa! Đây không phải là khoa phòng chỉ định!");
                    }
                    //else if()
                    else
                    {

                        var kq = (from chidinh in _data.ChiDinhs.Where(p => p.IDCD == _idCD) select new { chidinh.Status }).ToList();
                        if (kq.Count > 0)
                        {
                            if (kq.First().Status.Value == 1)
                            {
                                MessageBox.Show("Chỉ định đã có kết quả bạn không được sửa");
                            }
                            else
                            {
                                if ((DungChung.Bien.TamThuCLS == 1 || DungChung.Bien.TamThuCLS == 2))
                                {
                                    int _M = grvCLS.GetFocusedRowCellValue(MaDV) == null ? 0 : Convert.ToInt32(grvCLS.GetFocusedRowCellValue(MaDV));
                                    var ktTT = from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                                               join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                               where cd.MaDV == _M
                                               select cd;
                                    if (ktTT.Count() > 0 && ktTT.First().SoPhieu > 0)
                                    {
                                        kt = false;
                                        MessageBox.Show("Không thể xóa. Chỉ định đã được tạm thu!");
                                    }
                                }
                                else
                                {
                                    #region kiểm tra dịch vụ đã được khoa CLS kê HCVTYT (nếu có dịch vụ cuối cùng mà khoa CLS đó thực hiện thì không được xóa)
                                    string tenkp = "";
                                    if (ktKP.Count() > 0)
                                    {
                                        int makpth = ktKP.First().MaKPth == null ? 0 : ktKP.First().MaKPth.Value;
                                        var qkpCLS = _kphong.Where(p => p.MaKP == makpth).FirstOrDefault();

                                        if (qkpCLS != null)
                                        {
                                            var qhcvt = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _Mabn).Where(p => p.MaKP == makpth).Where(p => p.KieuDon == 7)
                                                         join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                                                         select dtct
                                                            ).Where(p => p.AttachIDDonct == null || p.AttachIDDonct == 0).ToList();
                                            if (qhcvt.Count > 0)
                                            {
                                                var qclsCtConLai = (from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn && p.MaKPth == makpth)
                                                                    join cd in _data.ChiDinhs.Where(p => p.IDCD != _idCD)
                                                                    on cls.IdCLS equals cd.IdCLS
                                                                    select cd).ToList();
                                                if (qclsCtConLai.Count == 0)
                                                {
                                                    tenkp = qkpCLS.TenKP;
                                                }
                                            }
                                        }

                                    }
                                    if (tenkp != "")
                                    {
                                        kt = false;
                                        MessageBox.Show("Phòng " + tenkp + " đã kê hóa chất, vật tư y tế cho bệnh nhân, bạn không thể xóa chỉ định");
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                    if (kt)
                    {
                        int _M = grvCLS.GetFocusedRowCellValue(MaDV) == null ? 0 : Convert.ToInt32(grvCLS.GetFocusedRowCellValue(MaDV));
                        foreach (var item in _listDichVu.Where(p => p.Chon == true && p.madv == _M))
                        {
                            item.Chon = false;
                            break;
                        }
                        grcDichvu.DataSource = _listDichVu.OrderBy(p => p.SoTT).ThenBy(p => p.tendv);
                        grcCLS.DataSource = _listDichVu.Where(p => p.Chon == true).ToList();
                    }
                }
            }
            _setStatus(_Mabn);
            if (e.Column.Name == "colSua")
            {
                int id = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdTieuNhom"));
                int _idCD = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colIDCD));
                if (!DungChung.Ham.KTraTT(_data, _Mabn))
                {
                    var kq = (from chidinh in _data.ChiDinhs.Where(p => p.IDCD == _idCD) select new { chidinh.Status, chidinh.SoPhieu }).ToList();
                    if (kq.Count > 0)
                    {
                        if (kq.First().Status.Value == 1)
                        {
                            MessageBox.Show("Chỉ định đã có kết quả bạn không được sửa");
                        }
                        else if (kq.First().SoPhieu != null && kq.First().SoPhieu > 0)
                        {
                            MessageBox.Show("Chỉ định đã được tạm thu, bạn không thể sửa");
                        }
                        else
                        {
                            this.Hide();
                            int _idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                            FormThamSo.Frm_HuyCLS frm = new Frm_HuyCLS(_idCLS, true);
                            frm.ShowDialog();
                            this.Show();
                        }
                    }
                }
                else
                    MessageBox.Show("Bệnh nhân đã được thanh toán, bạn không thể sửa");
            }
        }

        public void thongbao(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
        private void GrvNhomDV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (GrvNhomDV.FocusedColumn == colCkChon)
            //return;
            var row = (CanLamSang)GrvNhomDV.GetRow(e.FocusedRowHandle);
            if (row == null) return;
            int _idCD = 0;
            _listDichVu.Clear();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            grcDichvu.DataSource = "";
            if (GrvNhomDV.GetFocusedRowCellValue(colIDChiDinh) != null)
                _idCD = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colIDChiDinh));
            //set Code
            var clsCode = (from clsang in _dsChiDinhs.Where(o => o.chiDinh.IDCD == _idCD) select new { clsang.canLamSang.Code, clsang.canLamSang.BarCode, clsang.canLamSang.CapCuu, clsang.canLamSang.MaCB, clsang.canLamSang.MaKP, clsang.canLamSang.MaKPth, clsang.canLamSang.NgayThang }).ToList();
            if (clsCode.Count > 0)
            {
                txtCode_CLS.Text = clsCode.First().Code;
                ckCapCuu.Checked = clsCode.First().CapCuu;
            }
            else
            {
                txtCode_CLS.Text = "";
            }
            if (clsCode.Count > 0 && clsCode.First().MaCB != null)
                lupNguoiKhamkb.EditValue = clsCode.First().MaCB;
            else
                lupNguoiKhamkb.EditValue = "";
            if (clsCode.Count > 0 && clsCode.First().MaKP != null)
            {
                lupKhoaphong.EditValue = clsCode.First().MaKP;

            }
            else
                lupKhoaphong.EditValue = 0;
            if (clsCode.Count > 0 && clsCode.First().MaKPth != null)
            {
                string ChuyenKhoa = "";
                int M = 0;
                M = clsCode.First().MaKPth == null ? 0 : clsCode.First().MaKPth.Value;
                var abc = _kphong.Where(p => p.MaKP == (M)).Select(p => p.ChuyenKhoa).ToList();
                if (abc.Count > 0)
                    ChuyenKhoa = abc.First();
                List<KPhong> _lKp = new List<KPhong>();
                if (ChuyenKhoa == "Thủ thuật")
                    _lKp = (from KP in _kphong.Where(p => p.ChuyenKhoa != null && p.ChuyenKhoa.ToLower().Contains(ChuyenKhoa.ToLower()) || p.MaKP == DungChung.Bien.MaKP) select KP).OrderBy(p => p.TenKP).ToList();
                else
                    _lKp = (from KP in _kphong.Where(p => p.ChuyenKhoa != null && p.ChuyenKhoa.ToLower().Contains(ChuyenKhoa.ToLower())) select KP).OrderBy(p => p.TenKP).ToList();

                LupKpThuchien.Properties.DataSource = _lKp;
                _lkpnhom.Clear();
                _lkpnhom = _lKp;
                LupKpThuchien.EditValue = clsCode.First().MaKPth;
            }
            else
                LupKpThuchien.EditValue = 0;
            if (clsCode.Count > 0 && clsCode.First().NgayThang != null)
                dtNgayCD.DateTime = clsCode.First().NgayThang.Value;
            else
                dtNgayCD.DateTime = System.DateTime.Now;
            var a = (from CD in _dsChiDinhs.Where(o => o.chiDinh.IDCD == _idCD)
                     group new { CD.dichvu } by new { CD.tieuNhomDV.IDNhom, CD.tieuNhomDV.TenRG, CD.dichvu.MaDV, CD.dichvu.TenDV, CD.chiDinh.ChiDinh1, CD.chiDinh.IDCD, CD.chiDinh.Status, CD.chiDinh.TrongBH, CD.chiDinh.XHH, CD.chiDinh.YLenh2, CD.chiDinh.TenDungCu, CD.chiDinh.NDVaoVien } into kq
                     select new
                     {
                         // IdTieuNhom = kq.Key.IdTieuNhom,
                         nhom = kq.Key.TenRG,
                         IDNhom = kq.Key.IDNhom,
                         idcd = kq.Key.IDCD,
                         madv = kq.Key.MaDV,
                         tendv = kq.Key.TenDV,
                         ylenh = kq.Key.ChiDinh1,
                         Trangthai = kq.Key.Status.Value,
                         TrongBH = kq.Key.TrongBH == 1 ? true : false,
                         XHH = kq.Key.XHH == 1 ? true : false,
                         YLenh2 = kq.Key.YLenh2,
                         TenDC = kq.Key.TenDungCu,
                         NDVV = kq.Key.NDVaoVien,
                     }).ToList();


            if (a.Count > 0)
            {
                int id = Convert.ToInt32(a.First().IDNhom);
                string idTN = a.First().nhom;
                khoitaodichvu(idTN, 1);
                var ktTN = from tn in listTieuNhomDV.Where(p => p.TenRG == idTN && p.IDNhom == id)
                           join ndv in listNhomDV.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == ("Thăm dò chức năng") || p.TenNhomCT == "Thủ thuật, phẫu thuật" || p.TenNhomCT == "Xét nghiệm") on tn.IDNhom equals ndv.IDNhom
                           select tn;
                var chkTndv = (from tndv in listTieuNhomDV
                               join nhomdv in listNhomDV on tndv.IDNhom equals nhomdv.IDNhom
                               where nhomdv.TenNhomCT == ("Chẩn đoán hình ảnh") || nhomdv.TenNhomCT == ("Thăm dò chức năng") || nhomdv.TenNhomCT == ("Thủ thuật, phẫu thuật") || tndv.TenRG == ("XN Tế bào học")
                               select tndv.TenRG).ToList();
                // string s = GrvNhomDV.GetRowCellValue();
                if (DungChung.Bien.MaBV == "30003")
                {
                    foreach (var item in a)
                    {
                        string s = item.nhom.ToString();
                        for (int i = 0; i < chkTndv.Count(); i++)
                        {
                            if (item.nhom == chkTndv[i].ToString())
                            {
                                ////pnYLenh.Visible = true;
                                colYLenh.Visible = true;
                                break;
                            }
                            else
                            {
                                pnYLenh.Visible = false;
                                colYLenh.Visible = false;
                            }
                        }
                    }
                }
                else if (DungChung.Bien.MaBV == "30007")
                {
                    //pnYLenh.Visible = true;
                    colYLenh.Visible = true;

                }
                else
                {
                    if (ktTN.Count() > 0)
                    {
                        //pnYLenh.Visible = true;
                        colYLenh.Visible = true;
                        if (DungChung.Bien.MaBV == "24012" && id == 1)
                        {
                            colYLenh.Caption = "Loại mẫu";
                        }
                        else if (DungChung.Bien.MaBV == "24012" && id != 1)
                        {
                            colYLenh.Caption = "Y lệnh";

                        }
                    }
                    else
                    {
                        pnYLenh.Visible = false;
                        colYLenh.Visible = false;
                        if (DungChung.Bien.MaBV == "24012" && id == 1)
                        {
                            colYLenh.Caption = "Loại mẫu";
                            colYLenh.Visible = true;
                        }
                        else if (DungChung.Bien.MaBV == "24012" && id != 1)
                        {
                            colYLenh.Caption = "Y lệnh";
                            colYLenh.Visible = true;
                        }
                    }
                }
            }
            foreach (var item in a)
            {
                foreach (var item2 in _listDichVu.Where(p => p.madv == item.madv))
                {
                    item2.Chon = true;
                    item2.YLenh = item.ylenh;
                    item2.idcd = item.idcd;
                    item2.Trangthai = item.Trangthai;
                    item2.TrongBH = item.TrongBH;
                    item2.XHH = item.XHH;
                    item2.YLenh2 = item.YLenh2;
                    item2.TenDungCu = item.TenDC;
                    item2.NDVaoVien = item.NDVV;
                }
            }

            if (_listDichVu.Where(p => p.Chon == true).Count() > 0)
            {
                mmYlenh.Text = _listDichVu.Where(p => p.Chon == true).First().YLenh;
            }
            else
                mmYlenh.Text = "";
            if (trangthai == 0)
            {
                TrongDMBH.OptionsColumn.ReadOnly = true;
                colXHH.OptionsColumn.ReadOnly = true;
            }

            grcCLS.DataSource = null;
            grcCLS.DataSource = _listDichVu.Where(p => p.Chon == true).OrderBy(p => p.SoTT).ToList();
        }

        private void GrvNhomDV_dataSourceChanged(object sender, EventArgs e)
        {
            grcDichvu.DataSource = "";
            _listDichVu.Clear();
            float SP = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
            var a = (from CD in _data.ChiDinhs.Where(p => p.IdCLS == SP)
                     join dichvu in _data.DichVus on CD.MaDV equals dichvu.MaDV
                     group new { dichvu } by new { dichvu.TenRG, dichvu.TrongDM, dichvu.CanhBaoHM, dichvu.DonGia, dichvu.DonGia2, dichvu.MaQD, dichvu.MaDV, dichvu.TenDV, CD.ChiDinh1, CD.IDCD, CD.Status, CD.XHH, CD.YLenh2, CD.TenDungCu, CD.NDVaoVien } into kq
                     select new
                     {
                         kq.Key.DonGia,
                         kq.Key.MaQD,
                         tendv = kq.Key.TenDV,
                         madv = kq.Key.MaDV,
                         chidinh = kq.Key.ChiDinh1,
                         TT = kq.Key.Status,
                         idcd = kq.Key.IDCD,
                         kq.Key.DonGia2,
                         kq.Key.TrongDM,
                         XHH = kq.Key.XHH == 1 ? true : false,
                         kq.Key.CanhBaoHM,
                         kq.Key.YLenh2,

                         kq.Key.TenDungCu,
                         kq.Key.NDVaoVien,
                     }).ToList();
            if (DungChung.Bien.MaBV == "24272")
            {
                a = (from CD in _data.ChiDinhs.Where(p => p.IdCLS == SP)
                     join dichvu in _data.DichVus on CD.MaDV equals dichvu.MaDV
                     group new { dichvu } by new { dichvu.TenRG, dichvu.TrongDM, dichvu.CanhBaoHM, dichvu.DonGia, dichvu.DonGia2, dichvu.MaQD, dichvu.MaDV, dichvu.TenDV, CD.ChiDinh1, CD.IDCD, CD.Status, CD.XHH, CD.YLenh2, CD.TenDungCu, CD.NDVaoVien } into kq
                     select new
                     {
                         kq.Key.DonGia,
                         kq.Key.MaQD,
                         tendv = kq.Key.TenRG,
                         madv = kq.Key.MaDV,
                         chidinh = kq.Key.ChiDinh1,
                         TT = kq.Key.Status,
                         idcd = kq.Key.IDCD,
                         kq.Key.DonGia2,
                         kq.Key.TrongDM,
                         XHH = kq.Key.XHH == 1 ? true : false,
                         kq.Key.CanhBaoHM,
                         kq.Key.YLenh2,

                         kq.Key.TenDungCu,
                         kq.Key.NDVaoVien,
                     }).ToList();
            }
            _listDichVu.Clear();
            if (a.Count > 0)
            {
                foreach (var b in a)
                {
                    C_DichVu themmoi = new C_DichVu();
                    themmoi.madv = b.madv;
                    themmoi.idcd = b.idcd;
                    themmoi.tendv = b.tendv;
                    themmoi.YLenh = b.chidinh;
                    themmoi.Trangthai = b.TT.Value;
                    themmoi.MaQD = b.MaQD;
                    themmoi.dongia = b.DonGia;
                    themmoi.DonGia2 = b.DonGia2;
                    themmoi.XHH = b.XHH;
                    themmoi.YLenh2 = b.YLenh2;
                    //if (b.TrongDM == 1)
                    //    themmoi.DonGia = b.DonGia;
                    //else
                    //    themmoi.DonGia = b.DonGia2;
                    themmoi.CanhBaoHM = b.CanhBaoHM;
                    themmoi.DonGia = DungChung.Ham._getGiaDM(_data, b.madv, b.TrongDM ?? 0, _Mabn, dtNgayCD.DateTime);

                    themmoi.TenDungCu = b.TenDungCu;
                    themmoi.NDVaoVien = b.NDVaoVien;
                    _listDichVu.Add(themmoi);
                }
                grcCLS.DataSource = _listDichVu.OrderBy(p => p.SoTT).ToList();
                mmYlenh.Text = _listDichVu.First().YLenh;
            }
        }

        private void GrvNhomDV_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string _TenDV = GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString();
            if (DungChung.Bien.MaBV == "24297" && e.Column.Name == "colInChiDinh")
                DungChung.Bien.CheckInFull = 3;//minhvd
            if (DungChung.Bien.MaBV == "24297" && e.Column.Name == "colXemPhieu")
                DungChung.Bien.CheckInFull = 4;//minhvd
            var row = (CanLamSang)GrvNhomDV.GetFocusedRow();
            bool visibleCDHA = DungChung.Bien._Visible_CDHA[2];
            //if (e.Column.Name == "colTenDVcd")
            //{
            //    inchidinh(1);
            //}
            if (e.Column.Name == "colBSTH")
            {
                if (DungChung.Bien.MaBV != "14017" && DungChung.Bien.MaBV != "24297")
                    inphieumau();
            }
            if (e.Column.Name == "colMaBScd" && DungChung.Bien.MaBV != "14018")
            {
                if (DungChung.Bien.MaBV == "14017")
                {
                    int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                    string tenRG = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();
                    string TenTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();
                    string TenDV = GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString();
                    CLS.frm_NgayCD frm = new frm_NgayCD(idCLS, TenTN, TenDV, tenRG, _Mabn);
                    frm.ShowDialog();
                }
                else
                {
                    inchidinh(2);
                }

            }
            if (e.Column.Name == "colXemPhieu")
            {// Xem phiếu
                C_DichVu sua = _listDichVu.Where(p => p.tendv == _TenDV).First();
                if (DungChung.Bien.MaBV == "30372")
                {
                    DungChung.Ham.CallProcessWaitingForm(inphieu_30372);
                }
                else
                {
                    if (DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "01071")
                    {

                        DungChung.Bien._Visible_CDHA[2] = false;
                    }
                    DungChung.Ham.CallProcessWaitingForm(inphieu);
                }

            }
            if (e.Column.Name == "colInChiDinh")
            {
                if ((DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") && row.IDNhom == 1)
                {
                    PrintChiDinhTongHop(1, row.NgayThang, _idDB);
                }
                else
                {
                    if (row != null && DungChung.Bien.MaBV == "14018" && (row.TenRG == "Điều trị vận động" || row.TenRG == "Điều trị y học cổ truyền" || row.TenRG == "Điều trị vật lý" || row.TenRG == "Điều trị ngôn ngữ trị liệu"))
                    {

                        InPhieu.InPhieuDieuTri_14018(row.MaDV ?? 0, row.MaBNhan, InPhieu.TypePhieuDieuTri14018.ChiDinh, row.IdCLS, row.IDCD);
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                            DungChung.Bien._Visible_CDHA[2] = false;

                        //inchidinh(DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" ? 2 : 1);          


                        //inchidinh(DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" ? 2 : 1);//minhvd    


                        if (DungChung.Bien.MaBV == "24297" && (row.TenRG != "Điện tim" && row.TenRG != "X-Quang" && row.TenRG != "Siêu âm" && row.TenRG != "Lưu huyết não"))
                        {
                            inchidinh(2);
                        }
                        else if (DungChung.Bien.MaBV == "30372")
                        {
                            inchidinh_30372(1);
                        }
                        else
                        {
                            inchidinh(DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" ? 2 : 1);
                        }//minhvd
                    }

                }
            }
            DungChung.Bien._Visible_CDHA[2] = visibleCDHA;
        }




        //private void create_PhieuChiDinh_CDHA(string p, string _Mabn, int idCLS)
        //{
        //    throw new NotImplementedException();
        //}

        private void GrvNhomDV_DataSourceChanged_1(object sender, EventArgs e)
        {
            GrvNhomDV_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0));
        }

        public class THCLS
        {
            public int IdCLS { get; set; }
            public string TenRG { get; set; }
        }

        private void btnInCD_Click(object sender, EventArgs e)
        {
            float _idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
            List<int> _lIDCD = new List<int>();
            List<int> idTTPT = new List<int>();
            List<int> _lIdCLS = new List<int>();
            List<int?> _lMaDV = new List<int?>();
            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            //float _idCLS1 = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(ID_CLS));

            // _idCD = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colIDChiDinh));

            if (dataSource != null)
            {
                _lIDCD = dataSource.Where(o => o.Check && o.IDCD > 0).Select(o => o.IDCD).ToList();
                _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0).Select(o => o.IdCLS).ToList();
                _lMaDV = dataSource.Where(o => o.Check && o.MaDV > 0).Select(o => o.MaDV).Distinct().ToList();
                idTTPT = dataSource.Where(o => o.Check && o.IDCD > 0 && o.IDNhom != 8).Select(o => o.IDCD).ToList();
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();

            var row2 = (CanLamSang)GrvNhomDV.GetFocusedRow();
            if (DungChung.Bien.MaBV == "34019")
            {
                InPhieu.PrintNow = true;
                InPhieu.InTongHop = true;

                if (dataSource != null && dataSource.Count > 0)
                {
                    var idCLS = dataSource.Select(o => o.IdCLS).ToList();
                    var cls = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                               join cls4 in _data.CLS.Where(p => idCLS.Contains(p.IdCLS)) on bn.MaBNhan equals cls4.MaBNhan
                               join kp in _data.KPhongs on cls4.MaKP equals kp.MaKP
                               join kpTH in _data.KPhongs on cls4.MaKPth equals kpTH.MaKP
                               join chidinh in _data.ChiDinhs.Where(o => o.Status == 0) on cls4.IdCLS equals chidinh.IdCLS
                               join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                               join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                               select new THCLS { TenRG = tn.TenRG, IdCLS = cls4.IdCLS }).Distinct().ToList();
                    if (cls != null && cls.Count > 0)
                    {
                        List<string> rg = new List<string>();
                        foreach (var item in cls)
                        {
                            if (!rg.Exists(o => o == item.TenRG))
                            {
                                inchidinh(item.TenRG, item.IdCLS);
                                rg.Add(item.TenRG);
                            }

                        }

                    }
                }
                InPhieu.InTongHop = false;
                InPhieu.PrintNow = false;
            }

            // 13/12 HIS 1895 dùng thêm cho 30372
            else if (DungChung.Bien.MaBV == "24012" && _lIdCLS.Count > 0)
            {
                if (dataSource != null && dataSource.Count > 0)
                {
                    var idCLS = dataSource.Select(o => o.IdCLS).ToList();
                    var cls = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                               join cls4 in _data.CLS.Where(p => _lIdCLS.Contains(p.IdCLS)) on bn.MaBNhan equals cls4.MaBNhan
                               join kp in _data.KPhongs on cls4.MaKP equals kp.MaKP
                               join chidinh in _data.ChiDinhs.Where(o => o.Status == 0) on cls4.IdCLS equals chidinh.IdCLS
                               join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                               join tn in _data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                               select new THCLS { TenRG = tn.TenRG }).Distinct().ToList();

                    var grcls = (from p in cls
                                 group p by p.TenRG into g
                                 select new { TenRG = g.Key }).ToList();

                    foreach (var item in grcls)
                    {
                        PrintChiDinhTongHop_24012(null, null, null, item.TenRG, _lIdCLS, _lMaDV);
                    }

                }
            }
            else
            {
                if (DungChung.Bien.MaBV == "27183")
                {
                    List<int> qcls = _data.CLS.Where(p => p.MaBNhan == _Mabn).Select(p => p.IdCLS).ToList();
                    frm_BC_PhieuCDTH frm = new frm_BC_PhieuCDTH(qcls);
                    frm.ShowDialog();
                }
                else if (DungChung.Bien.MaBV == "30372")
                {
                    string _inMauCD = "30372";
                    int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                    frmIn frm1 = new frmIn();
                    CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frm1, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, _makp, "");
                }
                else if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && row2.IDNhom == 2 && row2.TenDV.Contains("Siêu âm ổ bung"))
                {
                    inchidinh(1);
                }
                else
                {
                    PrintChiDinhTongHop(null, null, null);
                }
            }
        }

        private void PrintChiDinhTongHop_24012(int? idNhom, DateTime? ngayCd, int? idDb, string idtn, List<int> _lIdCLS, List<int?> _lMaDV)
        {
            var XQ1 = ((from canls in _data.CLS.Where(p => p.MaBNhan == _Mabn && (ngayCd != null ? p.NgayThang == ngayCd : true) && ((idDb != null && idDb > 0) ? p.IDDienBien == idDb : true) && _lIdCLS.Contains(p.IdCLS))
                        join kp in _data.KPhongs on canls.MaKP equals kp.MaKP
                        join cb in _data.CanBoes on canls.MaCB equals cb.MaCB
                        join chidinh in _data.ChiDinhs.Where(p => p.Status == 0 && (DungChung.Bien.MaBV == "30372" ? _lMaDV.Contains(p.MaDV) : true)) on canls.IdCLS equals chidinh.IdCLS
                        join dichvu in _data.DichVus.Where(o => idNhom != null ? (o.IDNhom == idNhom) : true) on chidinh.MaDV equals dichvu.MaDV
                        join tn in _data.TieuNhomDVs.Where(p => p.TenRG == idtn) on dichvu.IdTieuNhom equals tn.IdTieuNhom
                        select new { canls.NgayThang, canls.MaKPth, kp.MaKP, cb.TenCB, kp.TenKP, TenDV = DungChung.Bien.MaBV == "24012" ? dichvu.TenRG : dichvu.TenDV, chidinh.ChiDinh1, tn.TenTN, SoLuong = 1, chidinh.DonGia, dichvu.MaDV }
                )).ToList();
            var XQ2 = (from a in XQ1
                       select new CDTH { MaKPth = (int)a.MaKPth, NgayThang = a.NgayThang, MaKP = a.MaKP, TenCB = a.TenCB, TenKP = a.TenKP, TenDV = a.TenDV + "(" + a.ChiDinh1 + ")", ChiDinh1 = a.ChiDinh1, TenTN = a.TenTN, SoLuong = a.SoLuong, DonGia = a.DonGia, STT = 2, MaDV = a.MaDV, STT_F = Byte.MaxValue }).ToList();



            int[] MaKP = XQ2.Select(p => p.MaKP).Distinct().ToList().ToArray();


            for (int i = 0; i < MaKP.Length; i++)
            {
                var XQ = XQ2.Where(p => p.MaKP == MaKP[i]).OrderBy(o => o.STT).ToList();

                frmIn frm = new frmIn();
                BaoCao.Rep_PhieuTHCD rep = new BaoCao.Rep_PhieuTHCD();
                int makp = MaKP[i];

                var k = (from bnkb in _data.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp) select new { bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB, bnkb.ChanDoanBD, bnkb.MaICD, bnkb.MaICD2 }).OrderByDescending(p => p.IDKB).ToList();
                if (k.Count > 0)
                {
                    if (DungChung.Bien.MaBV == "14018")
                    {
                        string ChanDoan = k.First().ChanDoan + "; " + k.First().BenhKhac;
                        string MaICD = k.First().MaICD + "; " + k.First().MaICD2;
                        rep.Chuandoan.Value = DungChung.Ham.FreshString_WithCode(ChanDoan, MaICD);
                    }
                    else
                        rep.Chuandoan.Value = DungChung.Ham.GetICDstr(k.First().ChanDoan + ";" + k.First().BenhKhac + ((DungChung.Bien.MaBV == "30372" && !string.IsNullOrWhiteSpace(k.First().ChanDoanBD)) ? (";" + k.First().ChanDoanBD) : ""));
                }
                var bn2 = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)

                           select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe, bn.NgaySinh, bn.ThangSinh, bn.NamSinh }).ToList();
                if (bn2.Count > 0)
                {
                    if (DungChung.Bien.MaBV == "30372")
                    {
                        rep.MaBN.Value = bn2.First().MaBNhan;
                    }
                    rep.Diachi.Value = bn2.First().DChi;
                    rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.CalculateAge(bn2.First().NgaySinh, bn2.First().ThangSinh, bn2.First().NamSinh, "tháng.") : bn2.First().Tuoi.ToString();
                    rep.TenBN.Value = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") ? bn2.First().TenBNhan.ToUpper() : bn2.First().TenBNhan;
                    rep.NamSinh.Value = bn2.First().NamSinh;
                    rep.TenTN.Text = idtn;
                    int gioiTinh = int.Parse(bn2.First().GTinh.ToString());
                    if (gioiTinh == 1)
                    {
                        rep.Nu.Value = "/";
                        rep.Nam.Value = "";
                    }
                    else
                    {
                        rep.Nu.Value = "";
                        rep.Nam.Value = "/";
                    }
                    if (bn2.First().SThe.Length == 15)
                    {
                        rep.Sthe1.Value = bn2.First().SThe.Substring(0, 3);
                        rep.Sthe2.Value = bn2.First().SThe.Substring(3, 2);
                        rep.Sthe3.Value = bn2.First().SThe.Substring(5, 2);
                        rep.Sthe4.Value = bn2.First().SThe.Substring(7, 3);
                        rep.Sthe5.Value = bn2.First().SThe.Substring(10, 5);
                    }
                }

                if (XQ.Count > 0)
                {
                    DateTime _dt = System.DateTime.Now;
                    if (XQ.First().NgayThang != null)
                        _dt = XQ.First().NgayThang.Value;
                    if (idNhom != null)
                    {
                        rep.SubBand5.Visible = true;
                    }
                    else rep.SubBand5.Visible = false;
                    rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, 2);
                    rep.BSCD.Value = XQ.Last().TenCB;
                    rep.TenKP.Value = XQ.First().TenKP;
                    rep.DataSource = XQ;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có chỉ định nào hoặc các chỉ định đã được thực hiện.");
                }
            }
        }

        private void PrintChiDinhTongHop(int? idNhom, DateTime? ngayCd, int? idDb)
        {
            var XQ1 = ((from canls in _data.CLS.Where(p => p.MaBNhan == _Mabn && (ngayCd != null ? p.NgayThang == ngayCd : true) && ((idDb != null && idDb > 0) ? p.IDDienBien == idDb : true))
                        join kp in _data.KPhongs on canls.MaKP equals kp.MaKP
                        join cb in _data.CanBoes on canls.MaCB equals cb.MaCB
                        join chidinh in _data.ChiDinhs.Where(p => p.Status == 0) on canls.IdCLS equals chidinh.IdCLS
                        join dichvu in _data.DichVus.Where(o => idNhom != null ? (o.IDNhom == idNhom) : true) on chidinh.MaDV equals dichvu.MaDV
                        join tn in _data.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                        // group new {}
                        select new { canls.NgayThang, canls.MaKPth, kp.MaKP, cb.TenCB, kp.TenKP, TenDV = DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24272" ? dichvu.TenRG : dichvu.TenDV, chidinh.ChiDinh1, tn.TenTN, SoLuong = 1, chidinh.DonGia, dichvu.MaDV }
                )).ToList();
            var XQ2 = (from a in XQ1
                       select new CDTH { MaKPth = (int)a.MaKPth, NgayThang = a.NgayThang, MaKP = a.MaKP, TenCB = a.TenCB, TenKP = a.TenKP, TenDV = a.ChiDinh1 == null ? a.TenDV : a.TenDV + "-" + a.ChiDinh1, ylenh = a.TenDV + "(" + a.ChiDinh1 + ")", ChiDinh1 = a.ChiDinh1, TenTN = a.TenTN, SoLuong = a.SoLuong, DonGia = a.DonGia, STT = 2, MaDV = a.MaDV, STT_F = Byte.MaxValue }).ToList();

            if (DungChung.Bien.MaBV == "30372")
            {
                var maDVs = XQ2.Select(o => o.MaDV).ToList();
                var dichVucts = _data.DichVucts.Where(o => maDVs.Contains(o.MaDV ?? 0)).GroupBy(o => o.MaDV).Select(o => new { MaDV = (o.Key ?? 0), STT_F = o.Min(p => p.STT_F) }).ToList();

                XQ2 = XQ2.Join(dichVucts, o => o.MaDV, p => p.MaDV, (o, p) => new CDTH { NgayThang = o.NgayThang, MaKP = o.MaKP, TenCB = o.TenCB, TenKP = o.TenKP, TenDV = o.TenDV, ChiDinh1 = o.ChiDinh1, TenTN = o.TenTN, SoLuong = o.SoLuong, DonGia = o.DonGia, STT = 2, MaDV = o.MaDV, STT_F = (p.STT_F ?? Byte.MaxValue) }).OrderBy(o => o.STT_F).ToList();
            }

            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "27777")
            {
                var congkham = (from dt in _data.DThuocs.Where(o => o.MaBNhan == _Mabn)
                                join dtct in _data.DThuoccts.Where(o => o.ThanhTien > 0) on dt.IDDon equals dtct.IDDon
                                join kp in _data.KPhongs on dt.MaKP equals kp.MaKP
                                join cb in _data.CanBoes on dt.MaCB equals cb.MaCB
                                join dichvu in _data.DichVus on dtct.MaDV equals dichvu.MaDV
                                join tn in _data.TieuNhomDVs.Where(o => o.IDNhom == 13) on dichvu.IdTieuNhom equals tn.IdTieuNhom
                                select new CDTH { MaKPth = 0, NgayThang = dt.NgayKe, MaKP = (dt.MaKP ?? 0), TenCB = cb.TenCB, TenKP = kp.TenKP, TenDV = dichvu.TenDV, ChiDinh1 = "", TenTN = tn.TenTN, SoLuong = dtct.SoLuong, DonGia = dtct.DonGia, STT = 1 }
                                  ).ToList();
                if (congkham != null && congkham.Count > 0)
                {
                    XQ2.AddRange(congkham);
                }
            }

            int[] MaKP = XQ2.Select(p => p.MaKP).Distinct().ToList().ToArray();

            if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "27777")
            {

                for (int i = 0; i < MaKP.Length; i++)
                {
                    //var XQ = XQ2.Where(p => p.MaKP == MaKP[i]).OrderBy(o => o.STT).ToList();
                    var XQ = (from a in XQ2.Where(p => p.MaKP == MaKP[i])
                              join kp1 in _kphong on a.MaKPth equals kp1.MaKP
                              select new CDTH { MaKPth = 0, NgayThang = a.NgayThang, MaKP = a.MaKP, TenCB = a.TenCB, TenKP = a.TenKP, TenDV = a.TenDV, ChiDinh1 = a.ChiDinh1, TenTN = a.TenTN + "(" + kp1.DChi + ")", SoLuong = a.SoLuong, DonGia = a.DonGia, STT = 2, MaDV = a.MaDV, STT_F = Byte.MaxValue }).ToList();
                    var XQ_add = XQ2.Where(p => p.MaKPth == 0 && p.MaKP == MaKP[i]).ToList();
                    if (XQ_add.Count() > 0)
                    {
                        XQ.AddRange(XQ_add);
                    }
                    if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "27777")
                        XQ = XQ.OrderBy(o => o.STT).ToList();
                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "27777")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_PhieuTHCD_CoDonGia rep = new BaoCao.Rep_PhieuTHCD_CoDonGia();
                        rep.lab2.Text = "MS: " + _Mabn.ToString();
                        rep.xrBarCode1.Text = _Mabn.ToString();
                        int makp = MaKP[i];
                        if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "27777")
                        {
                            var check = (from kp in _data.KPhongs.Where(p => p.MaKP == makp)
                                         select new { kp.MaKP, kp.IsDongY }).ToList();
                            if (check.First().IsDongY == true)
                            {
                                rep.Chuandoan.Value = DungChung.Ham.GetChanDoanKB_ByKP(_data, _Mabn, makp);
                            }

                            else
                            {
                                arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(_data, _Mabn, makp == null ? 0 : makp, true);
                                rep.Chuandoan.Value = arrThongTinBNKB[1];
                            }

                        }
                        //else
                        //{
                        //    var k = (from bnkb in _data.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp)
                        //             join bn0 in _data.BenhNhans on bnkb.MaBNhan equals bn0.MaBNhan
                        //             select new { bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB, bn0.NamSinh, bnkb.ChanDoanBD }).OrderByDescending(p => p.IDKB).ToList();
                        //    if (k.Count > 0)
                        //    {
                        //        rep.Chuandoan.Value = DungChung.Ham.GetICDstr(k.First().ChanDoanBD == null ? k.First().ChanDoan + ";" + k.First().BenhKhac : k.First().ChanDoanBD); //+ ";" + k.First().BenhKhac);
                        //    }
                        //}

                        var bn2 = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)

                                   select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe, bn.NamSinh }).ToList();
                        if (bn2.Count > 0)
                        {
                            rep.NamSinh.Value = bn2.First().NamSinh;
                            rep.Diachi.Value = bn2.First().DChi;
                            rep.Tuoi.Value = bn2.First().Tuoi;
                            rep.TenBN.Value = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303") ? bn2.First().TenBNhan.ToUpper() : bn2.First().TenBNhan;
                            int gioiTinh = int.Parse(bn2.First().GTinh.ToString());
                            if (gioiTinh == 1)
                            {
                                rep.Nu.Value = "/";
                                rep.Nam.Value = "";
                            }
                            else
                            {
                                rep.Nu.Value = "";
                                rep.Nam.Value = "/";
                            }
                            if (bn2.First().SThe.Length == 15)
                            {
                                rep.Sthe1.Value = bn2.First().SThe.Substring(0, 3);
                                rep.Sthe2.Value = bn2.First().SThe.Substring(3, 2);
                                rep.Sthe3.Value = bn2.First().SThe.Substring(5, 2);
                                rep.Sthe4.Value = bn2.First().SThe.Substring(7, 3);
                                rep.Sthe5.Value = bn2.First().SThe.Substring(10, 5);
                            }
                        }

                        if (XQ.Count > 0)
                        {
                            DateTime _dt = System.DateTime.Now;
                            if (XQ.First().NgayThang != null)
                                _dt = XQ.First().NgayThang.Value;
                            rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, DungChung.Bien.FormatDate);
                            rep.BSCD.Value = XQ.Last().TenCB;
                            rep.TenKP.Value = XQ.First().TenKP;
                            rep.DataSource = XQ;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Không có chỉ định nào hoặc các chỉ định đã được thực hiện.");
                        }
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_PhieuTHCD rep = new BaoCao.Rep_PhieuTHCD();
                        int makp = MaKP[i];
                        var k = (from bnkb in _data.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp) select new { bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB, bnkb.ChanDoanBD, bnkb.MaICD, bnkb.MaICD2 }).OrderByDescending(p => p.IDKB).ToList();
                        if (k.Count > 0)
                        {
                            if (DungChung.Bien.MaBV == "14018")
                            {
                                string ChanDoan = k.First().ChanDoan + "; " + k.First().BenhKhac;
                                string MaICD = k.First().MaICD + "; " + k.First().MaICD2;
                                rep.Chuandoan.Value = DungChung.Ham.FreshString_WithCode(ChanDoan, MaICD);
                            }
                            else if (DungChung.Bien.MaBV == "24272")
                            {
                                //rep.Chuandoan.Value = DungChung.Ham.GetICDstr(k.First().ChanDoan + ";" + k.First().BenhKhac + ((DungChung.Bien.MaBV == "30372" && !string.IsNullOrWhiteSpace(k.First().ChanDoanBD)) ? (";" + k.First().ChanDoanBD) : ""));
                                rep.Chuandoan.Value = k.First().ChanDoanBD;
                            }
                            else
                                rep.Chuandoan.Value = DungChung.Ham.GetICDstr(k.First().ChanDoan + ";" + k.First().BenhKhac + ((DungChung.Bien.MaBV == "30372" && !string.IsNullOrWhiteSpace(k.First().ChanDoanBD)) ? (";" + k.First().ChanDoanBD) : ""));
                        }
                        var bn2 = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)

                                   select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe, bn.NgaySinh, bn.ThangSinh, bn.NamSinh }).ToList();
                        if (bn2.Count > 0)
                        {
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                rep.MaBN.Value = bn2.First().MaBNhan;
                            }
                            rep.Diachi.Value = bn2.First().DChi;
                            rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.CalculateAge(bn2.First().NgaySinh, bn2.First().ThangSinh, bn2.First().NamSinh, "tháng.") : bn2.First().Tuoi.ToString();
                            rep.TenBN.Value = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") ? bn2.First().TenBNhan.ToUpper() : bn2.First().TenBNhan;
                            rep.NamSinh.Value = bn2.First().NamSinh;
                            int gioiTinh = int.Parse(bn2.First().GTinh.ToString());
                            if (gioiTinh == 1)
                            {
                                rep.Nu.Value = "/";
                                rep.Nam.Value = "";
                            }
                            else
                            {
                                rep.Nu.Value = "";
                                rep.Nam.Value = "/";
                            }
                            if (bn2.First().SThe.Length == 15)
                            {
                                rep.Sthe1.Value = bn2.First().SThe.Substring(0, 3);
                                rep.Sthe2.Value = bn2.First().SThe.Substring(3, 2);
                                rep.Sthe3.Value = bn2.First().SThe.Substring(5, 2);
                                rep.Sthe4.Value = bn2.First().SThe.Substring(7, 3);
                                rep.Sthe5.Value = bn2.First().SThe.Substring(10, 5);
                            }
                        }

                        if (XQ.Count > 0)
                        {
                            DateTime _dt = System.DateTime.Now;
                            if (XQ.First().NgayThang != null)
                                _dt = XQ.First().NgayThang.Value;
                            if (idNhom != null)
                            {
                                rep.SubBand5.Visible = true;
                            }
                            else rep.SubBand5.Visible = false;
                            rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, DungChung.Bien.FormatDate);
                            rep.BSCD.Value = XQ.Last().TenCB;
                            rep.TenKP.Value = XQ.First().TenKP;
                            rep.DataSource = XQ;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Không có chỉ định nào hoặc các chỉ định đã được thực hiện.");
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < MaKP.Length; i++)
                {
                    var XQ = XQ2.Where(p => p.MaKP == MaKP[i]).OrderBy(o => o.STT).ToList();
                    if (DungChung.Bien.MaBV == "30372")
                        XQ = XQ.OrderBy(o => o.STT_F).ToList();
                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_PhieuTHCD_CoDonGia rep = new BaoCao.Rep_PhieuTHCD_CoDonGia();
                        rep.lab2.Text = "MS: " + _Mabn.ToString();
                        rep.xrBarCode1.Text = _Mabn.ToString();
                        int makp = MaKP[i];
                        if (DungChung.Bien.MaBV == "24297")
                        {
                            var check = (from kp in _data.KPhongs.Where(p => p.MaKP == makp)
                                         select new { kp.MaKP, kp.IsDongY }).ToList();
                            if (check.First().IsDongY == true)
                            {
                                rep.Chuandoan.Value = DungChung.Ham.GetChanDoanKB_ByKP(_data, _Mabn, makp);
                            }

                            else
                            {
                                arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(_data, _Mabn, makp == null ? 0 : makp, true);
                                rep.Chuandoan.Value = arrThongTinBNKB[1];
                            }

                        }
                        //else
                        //{
                        //    var k = (from bnkb in _data.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp)
                        //             join bn0 in _data.BenhNhans on bnkb.MaBNhan equals bn0.MaBNhan
                        //             select new { bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB, bn0.NamSinh, bnkb.ChanDoanBD }).OrderByDescending(p => p.IDKB).ToList();
                        //    if (k.Count > 0)
                        //    {
                        //        rep.Chuandoan.Value = DungChung.Ham.GetICDstr(k.First().ChanDoanBD == null ? k.First().ChanDoan + ";" + k.First().BenhKhac : k.First().ChanDoanBD); //+ ";" + k.First().BenhKhac);
                        //    }
                        //}

                        var bn2 = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)

                                   select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe, bn.NamSinh }).ToList();
                        if (bn2.Count > 0)
                        {
                            rep.NamSinh.Value = bn2.First().NamSinh;
                            rep.Diachi.Value = bn2.First().DChi;
                            rep.Tuoi.Value = bn2.First().Tuoi;
                            rep.TenBN.Value = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "30303") ? bn2.First().TenBNhan.ToUpper() : bn2.First().TenBNhan;
                            int gioiTinh = int.Parse(bn2.First().GTinh.ToString());
                            if (gioiTinh == 1)
                            {
                                rep.Nu.Value = "/";
                                rep.Nam.Value = "";
                            }
                            else
                            {
                                rep.Nu.Value = "";
                                rep.Nam.Value = "/";
                            }
                            if (bn2.First().SThe.Length == 15)
                            {
                                rep.Sthe1.Value = bn2.First().SThe.Substring(0, 3);
                                rep.Sthe2.Value = bn2.First().SThe.Substring(3, 2);
                                rep.Sthe3.Value = bn2.First().SThe.Substring(5, 2);
                                rep.Sthe4.Value = bn2.First().SThe.Substring(7, 3);
                                rep.Sthe5.Value = bn2.First().SThe.Substring(10, 5);
                            }
                        }

                        if (XQ.Count > 0)
                        {
                            DateTime _dt = System.DateTime.Now;
                            if (XQ.First().NgayThang != null)
                                _dt = XQ.First().NgayThang.Value;
                            rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, DungChung.Bien.FormatDate);
                            rep.BSCD.Value = XQ.Last().TenCB;
                            rep.TenKP.Value = XQ.First().TenKP;
                            rep.DataSource = XQ;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Không có chỉ định nào hoặc các chỉ định đã được thực hiện.");
                        }
                    }
                    else
                    {
                        frmIn frm = new frmIn();
                        BaoCao.Rep_PhieuTHCD rep = new BaoCao.Rep_PhieuTHCD();
                        int makp = MaKP[i];
                        var k = (from bnkb in _data.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp) select new { bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB, bnkb.ChanDoanBD, bnkb.MaICD, bnkb.MaICD2 }).OrderByDescending(p => p.IDKB).ToList();
                        if (k.Count > 0)
                        {
                            if (DungChung.Bien.MaBV == "14018")
                            {
                                string ChanDoan = k.First().ChanDoan + "; " + k.First().BenhKhac;
                                string MaICD = k.First().MaICD + "; " + k.First().MaICD2;
                                rep.Chuandoan.Value = DungChung.Ham.FreshString_WithCode(ChanDoan, MaICD);
                            }
                            else
                                rep.Chuandoan.Value = DungChung.Ham.GetICDstr(k.First().ChanDoan + ";" + k.First().BenhKhac + ((DungChung.Bien.MaBV == "30372" && !string.IsNullOrWhiteSpace(k.First().ChanDoanBD)) ? (";" + k.First().ChanDoanBD) : ""));
                        }
                        var bn2 = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)

                                   select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe, bn.NgaySinh, bn.ThangSinh, bn.NamSinh }).ToList();
                        if (bn2.Count > 0)
                        {
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                rep.MaBN.Value = bn2.First().MaBNhan;
                            }
                            rep.Diachi.Value = bn2.First().DChi;
                            rep.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.CalculateAge(bn2.First().NgaySinh, bn2.First().ThangSinh, bn2.First().NamSinh, "tháng.") : bn2.First().Tuoi.ToString();
                            rep.TenBN.Value = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789") ? bn2.First().TenBNhan.ToUpper() : bn2.First().TenBNhan;
                            rep.NamSinh.Value = bn2.First().NamSinh;
                            int gioiTinh = int.Parse(bn2.First().GTinh.ToString());
                            if (gioiTinh == 1)
                            {
                                rep.Nu.Value = "/";
                                rep.Nam.Value = "";
                            }
                            else
                            {
                                rep.Nu.Value = "";
                                rep.Nam.Value = "/";
                            }
                            if (bn2.First().SThe.Length == 15)
                            {
                                rep.Sthe1.Value = bn2.First().SThe.Substring(0, 3);
                                rep.Sthe2.Value = bn2.First().SThe.Substring(3, 2);
                                rep.Sthe3.Value = bn2.First().SThe.Substring(5, 2);
                                rep.Sthe4.Value = bn2.First().SThe.Substring(7, 3);
                                rep.Sthe5.Value = bn2.First().SThe.Substring(10, 5);
                            }
                        }

                        if (XQ.Count > 0)
                        {
                            DateTime _dt = System.DateTime.Now;
                            if (XQ.First().NgayThang != null)
                                _dt = XQ.First().NgayThang.Value;
                            if (idNhom != null)
                            {
                                rep.SubBand5.Visible = true;
                            }
                            else rep.SubBand5.Visible = false;
                            rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, 2);
                            rep.BSCD.Value = XQ.Last().TenCB;
                            rep.TenKP.Value = XQ.First().TenKP;
                            rep.DataSource = XQ;
                            rep.BindingData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Không có chỉ định nào hoặc các chỉ định đã được thực hiện.");
                        }
                    }
                }
            }


        }


        public class CDTH
        {
            public DateTime? NgayThang { get; set; }
            public int MaKP { get; set; }
            public int MaKPth { get; set; }
            public string TenCB { get; set; }
            public string TenKP { get; set; }
            public string TenDV { get; set; }
            public string ChiDinh1 { get; set; }
            public string TenTN { get; set; }
            public double SoLuong { get; set; }
            public double DonGia { get; set; }
            public int STT { get; set; }
            public int MaDV { get; set; }
            public string ylenh { get; set; }
            public byte STT_F { get; set; }
        }

        private void enableControl(bool T)
        {
            btnLuu.Enabled = !T;
            btnMoi.Enabled = T;
            btnSua.Enabled = T;
            btnXoa.Enabled = T;
        }
        private void lupTN_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //if (lupTN.EditValue != null && lupTN.EditValue.ToString() != "" && btnLuu.Enabled == true)
            //{
            //    if (grvCLS.RowCount > 0)
            //    {
            //        DialogResult _result = MessageBox.Show("Bạn chưa lưu chỉ định trước, Bạn có muốn lưu không?", "Hỏi!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (_result == DialogResult.Yes)
            //        {
            //            btnLuu_Click(sender, e);

            //        }
            //        else
            //        {
            //            grcCLS.DataSource = "";
            //        }
            //    }
            //}
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
            BaoCao.Rep_PhieuTHCD rep = new BaoCao.Rep_PhieuTHCD();
            var k = (from bnkb in _data.BNKBs.Where(p => p.MaBNhan == _Mabn) select new { bnkb.ChanDoan, bnkb.BenhKhac, bnkb.IDKB }).OrderByDescending(p => p.IDKB).ToList();
            if (k.Count > 0)
            {
                rep.Chuandoan.Value = k.First().ChanDoan + " /" + k.First().BenhKhac;
            }
            var bn2 = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)

                       select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe }).ToList();
            if (bn2.Count > 0)
            {

                rep.Diachi.Value = bn2.First().DChi;
                rep.Tuoi.Value = bn2.First().Tuoi;
                rep.TenBN.Value = bn2.First().TenBNhan;
                int gioiTinh = int.Parse(bn2.First().GTinh.ToString());
                if (gioiTinh == 1)
                {
                    rep.Nu.Value = "/";
                    rep.Nam.Value = "";
                }
                else
                {
                    rep.Nu.Value = "";
                    rep.Nam.Value = "/";
                }
                if (bn2.First().SThe.Length == 15)
                {
                    rep.Sthe1.Value = bn2.First().SThe.Substring(0, 3);
                    rep.Sthe2.Value = bn2.First().SThe.Substring(3, 2);
                    rep.Sthe3.Value = bn2.First().SThe.Substring(5, 2);
                    rep.Sthe4.Value = bn2.First().SThe.Substring(7, 3);
                    rep.Sthe5.Value = bn2.First().SThe.Substring(10, 5);
                }
            }
            var XQ = (from canls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                      join kp in _data.KPhongs on canls.MaKP equals kp.MaKP
                      join cb in _data.CanBoes on canls.MaCB equals cb.MaCB
                      join chidinh in _data.ChiDinhs.Where(p => p.Status == 0) on canls.IdCLS equals chidinh.IdCLS
                      join dichvu in _data.DichVus on chidinh.MaDV equals dichvu.MaDV
                      join tn in _data.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                      join Nhom in _data.NhomDVs on tn.IDNhom equals Nhom.IDNhom
                      select new { canls.NgayThang, cb.TenCB, kp.TenKP, TenDV = dichvu.TenDV + " - " + chidinh.ChiDinh1, tn.TenTN, SoLuong = 1 }).ToList().Union(
                      from canls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                      join kp in _data.KPhongs on canls.MaKP equals kp.MaKP
                      join cb in _data.CanBoes on canls.MaCB equals cb.MaCB
                      join chidinh in _data.ChiDinhs.Where(p => p.Status == 0) on canls.IdCLS equals chidinh.IdCLS
                      join dichvu in _data.DichVus on chidinh.MaDV equals dichvu.MaDV
                      join tn in _data.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                      join Nhom in _data.NhomDVs on tn.IDNhom equals Nhom.IDNhom
                      select new { canls.NgayThang, cb.TenCB, kp.TenKP, TenDV = dichvu.TenDV + " - " + chidinh.ChiDinh1, tn.TenTN, SoLuong = 1 }
                      ).ToList();
            if (XQ.Count > 0)
            {
                DateTime _dt = System.DateTime.Now;
                if (XQ.First().NgayThang != null)
                    _dt = XQ.First().NgayThang.Value;
                rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, DungChung.Bien.FormatDate);
                rep.BSCD.Value = XQ.First().TenCB;
                rep.TenKP.Value = XQ.First().TenKP;
                rep.DataSource = XQ;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có chỉ định nào hoặc các chỉ định đã được thực hiện.");
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            int _madv = 0;
            if (lupTenDV.EditValue != null)
                _madv = Convert.ToInt32(lupTenDV.EditValue);
            foreach (C_DichVu item in _listDichVu.Where(p => p.madv == _madv))
            {
                if (item.Chon == false)
                {
                    item.Chon = true;
                    item.CPDinhKem = ckccpdinhkem.Checked;
                    item.TrongBH = chkDichVu.Checked;
                }
                else
                {
                    item.Chon = false;
                }
            }
            dtNgayCD.DateTime = System.DateTime.Now;
            grcDichvu.DataSource = _listDichVu;
            grcCLS.DataSource = _listDichVu.Where(p => p.Chon == true).ToList();
        }


        private void grvCLS_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "TrongDMBH")
            {

                int _M = grvCLS.GetFocusedRowCellValue(MaDV) == null ? 0 : Convert.ToInt32(grvCLS.GetFocusedRowCellValue(MaDV));
                if (grvCLS.GetFocusedRowCellValue(TrongDMBH) != null)
                {

                    if (trangthai == 1)
                    {

                        foreach (var item in _listDichVu.Where(p => p.Chon == true && p.madv == _M))
                        {
                            item.TrongBH = !item.TrongBH;
                            double gia = DungChung.Ham._getGiaDM(_data, _M, Convert.ToInt32(item.TrongBH), _Mabn, dtNgayCD.DateTime);
                            grvDichvu.SetFocusedRowCellValue(colDonGia, gia.ToString("##,####"));
                            item.DonGia = gia;
                        }
                    }
                    if (trangthai == 2)
                    {
                        float SP = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));

                        var kq = (from chidinh in _data.ChiDinhs.Where(p => p.IdCLS == SP) select new { chidinh.Status }).ToList();

                        var qcd = (from cd in _data.ChiDinhs.Where(p => p.IdCLS == SP).Where(p => p.MaDV == _M)
                                   join tu in _data.TamUngs.Where(p => p.MaBNhan == _M).Where(p => p.PhanLoai == 3) on cd.SoPhieu equals tu.IDTamUng
                                   select cd).ToList();

                        if (kq.Count > 0)
                        {
                            if (kq.First().Status.Value == 1)
                            {
                                MessageBox.Show("Chỉ định đã có kết quả bạn không được sửa");
                            }
                            else if (qcd.Count > 0)
                            {
                                MessageBox.Show("Dịch vụ đã được thu thẳng, bạn không được sửa");
                            }
                            else
                            {

                                //for (int i = 0; i < grvDichvu.RowCount; i++)
                                //{
                                //    if (grvDichvu.GetRowCellValue(i, colChon) != null && grvDichvu.GetRowCellValue(i, colChon).ToString() == "True")
                                //    {
                                //        if (grvDichvu.GetRowCellValue(i, colMadv) != null && Convert.ToInt32(grvDichvu.GetRowCellValue(i, colMadv).ToString()) == _M)
                                //        {
                                //            grvDichvu.SetRowCellValue(i, colTrongBH, _trongbh);

                                //        }
                                //    }

                                //}

                                foreach (var item in _listDichVu.Where(p => p.Chon == true && p.madv == _M))
                                {
                                    item.TrongBH = !item.TrongBH;
                                    double gia = DungChung.Ham._getGiaDM(_data, _M, Convert.ToInt32(item.TrongBH), _Mabn, dtNgayCD.DateTime);
                                    //  grvDichvu.SetFocusedRowCellValue(colDonGia, gia.ToString("##,####"));
                                    item.DonGia = gia;
                                }


                                grcDichvu.DataSource = null;
                                grcDichvu.DataSource = _listDichVu;


                            }
                        }
                    }
                }


            }
            if (e.Column.Name == "colXHH")
            {
                int _M = grvCLS.GetFocusedRowCellValue(MaDV) == null ? 0 : Convert.ToInt32(grvCLS.GetFocusedRowCellValue(MaDV));
                if (grvCLS.GetFocusedRowCellValue(colXHH) != null && grvCLS.GetFocusedRowCellValue(colXHH).ToString() == "False")
                {
                    grvCLS.SetFocusedRowCellValue(TrongDMBH, 0);
                    foreach (var item in _listDichVu.Where(p => p.Chon == true && p.madv == _M))
                    {
                        item.TrongBH = false;
                        double gia = DungChung.Ham._getGiaDM(_data, _M, Convert.ToInt32(item.TrongBH), _Mabn, dtNgayCD.DateTime);
                        // grvDichvu.SetFocusedRowCellValue(colDonGia, gia.ToString("##,####"));
                        item.DonGia = gia;
                    }
                    grvCLS.SetFocusedRowCellValue(colXHH, true);
                    grcDichvu.DataSource = null;
                    grcDichvu.DataSource = _listDichVu;
                }
            }
            else if (e.Column == colYLenh)
            {
                if (grvCLS.GetFocusedRowCellValue(colYLenh) != null)
                {
                    string ylenh = grvCLS.GetFocusedRowCellDisplayText(colYLenh).ToString();
                    mmYlenh.Text = ylenh;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdTieuNhom"));
            int _idCD = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colIDCD));
            int _idCLS = 0;
            if (!DungChung.Ham.KTraTT(_data, _Mabn))
            {
                var kq = (from chidinh in _data.ChiDinhs.Where(p => p.IDCD == _idCD) select new { chidinh.Status, chidinh.SoPhieu, chidinh.IdCLS }).ToList();
                if (kq.Count > 0)
                {
                    _idCLS = kq.First().IdCLS ?? 0;
                    var ktcls = _data.CLS.Where(p => p.IdCLS == _idCLS).Where(p => p.BarCode != null || p.BarCode != "").ToList();

                    if (kq.First().Status.Value == 1)
                    {
                        MessageBox.Show("Chỉ định đã có kết quả bạn không được sửa");
                    }
                    else if (kq.First().SoPhieu != null && kq.First().SoPhieu > 0)
                    {
                        MessageBox.Show("Chỉ định đã được tạm thu, bạn không thể sửa");
                    }
                    else if (DungChung.Bien.MaBV == "01071" && ktcls.Count > 0 && DTBN == 1)
                    {
                        MessageBox.Show("Chỉ định đã được khớp mã barcode, bạn không thể sửa");
                    }
                    else
                    {
                        var _tenRG = _data.TieuNhomDVs.Where(p => p.IdTieuNhom == id).ToList();
                        if (_tenRG.Count > 0)
                            lupTN.EditValue = _tenRG.First().TenRG;
                        lupTenDV_EditValueChanged(null, null);
                        trangthai = 2;
                        foreach (var a in _listDichVu)
                        {
                            if (a.TrongBH == true && a.Chon == true)
                            {
                                a.DonGia = DungChung.Ham._getGiaDM(_data, a.madv, 1, _Mabn, dtNgayCD.DateTime);
                            }
                        }
                        grcDichvu.DataSource = _listDichVu;
                        enableControl(false);
                        colYLenh.OptionsColumn.AllowEdit = true;
                        colYLenh.OptionsColumn.ReadOnly = false;
                        TrongDMBH.OptionsColumn.ReadOnly = false;
                        colXHH.OptionsColumn.ReadOnly = false;

                        pnLeft.Enabled = true;
                        GrcNhomDV.Enabled = false;
                        mmYlenh.Properties.ReadOnly = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Bệnh nhân đã Thanh Toán, bạn không thể nhập mới");
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            setBarCode();
            var q = _data.RaViens.Where(p => p.MaBNhan == _Mabn).ToList();
            var khambenh = (from bnkb in _data.BNKBs.Where(p => p.MaBNhan == _Mabn)
                            join bn in _data.BenhNhans on bnkb.MaBNhan equals bn.MaBNhan
                            where bnkb.MaKP == bn.MaKP
                            select new {}).ToList();
            if(khambenh.Count == 0)
            {
                MessageBox.Show("Bệnh nhân chưa có khám bệnh, không thể chỉ định!!");
                return;
            }    
            if (q.Count == 0)
            {
                if (!DungChung.Ham.KTraTT(_data, _Mabn))
                {
                    // lupTN.EditValue = null;
                    trangthai = 1;
                    pnLeft.Enabled = true;
                    GrcNhomDV.Enabled = false;
                    colYLenh.OptionsColumn.AllowEdit = true;
                    colYLenh.OptionsColumn.ReadOnly = false;
                    grcCLS.DataSource = null;
                    _listDichVu.Clear();
                    enableControl(false);
                    trangthai = 1;
                    mmYlenh.Properties.ReadOnly = false;
                    mmYlenh.Text = "";
                    var bnkb = _data.BNKBs.Where(p => p.MaBNhan == _Mabn).OrderByDescending(p => p.IDKB).FirstOrDefault();
                    if (bnkb != null)
                    {
                        if (DungChung.Bien.MaBV == "30007" && DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                        {
                            lupNguoiKhamkb.EditValue = DungChung.Bien.MaCB;
                            lupKhoaphong.EditValue = DungChung.Bien.MaKP;
                        }
                        else if ((DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") && MaKP_14018 != null && MaKP_14018 > 0)
                        {
                            lupKhoaphong.EditValue = MaKP_14018;
                        }
                        else
                        {
                            lupNguoiKhamkb.EditValue = bnkb.MaCB;
                            lupKhoaphong.EditValue = bnkb.MaKP;

                        }
                    }

                    if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin && DungChung.Bien.MaBV == "30007")
                    {
                        lupNguoiKhamkb.Properties.ReadOnly = true;

                    }
                    lupTN.Focus();
                    lupTN_EditValueChanged(null, null);
                }
                else
                {
                    MessageBox.Show("Bệnh nhân đã Thanh Toán, bạn không thể nhập mới");
                }
            }
            else
            {
                MessageBox.Show("Bệnh nhân đã Ra Viện, bạn không thể nhập mới");
            }
            TrongDMBH.OptionsColumn.ReadOnly = false;
            colXHH.OptionsColumn.ReadOnly = false;
        }
        public class BenchmarkResult
        {
            public string Action { get; set; }
            public int Entities { get; set; }
            public string Performance { get; set; }
        }
        public static List<BenchmarkResult> BenchmarkResults = new List<BenchmarkResult>();
        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool kt = true;
            float _idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
            List<int> _lIDCD = new List<int>();
            List<int> idTTPT = new List<int>();
            List<int> _lIdCLS = new List<int>();

            var clsSlecteds = new List<CanLamSang>();

            //float _idCLS1 = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(ID_CLS));

            // _idCD = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colIDChiDinh));
            #region mới - xóa những chỉ định được chọn

            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            if (dataSource != null)
            {
                _lIDCD = dataSource.Where(o => o.Check && o.IDCD > 0).Select(o => o.IDCD).ToList();
                _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0).Select(o => o.IdCLS).ToList();
                idTTPT = dataSource.Where(o => o.Check && o.IDCD > 0 && o.IDNhom != 8).Select(o => o.IDCD).ToList();

                clsSlecteds = dataSource.Where(o => o.Check && o.IdCLS > 0).ToList();
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();
            #region kiểm tra xóa
            var ktKP = from cls in _data.CLS.Where(p => _lIdCLS.Contains(p.IdCLS)) select cls;
            int _makpcd = 0;
            if (ktKP.Count() > 0)
                _makpcd = ktKP.First().MaKP ?? 0;
            var ktAdmin = _kphong.Where(p => p.MaKP == DungChung.Bien.MaKP && p.PLoai == "Admin").ToList();
            int makp = -1;
            var kpsd = _data.CanBoes.FirstOrDefault(o => o.MaCB == DungChung.Bien.MaCB);

            int ktraKPChiDinh = (from cls in ktKP.Where(p => p.MaKP != DungChung.Bien.MaKP) select cls).Count();
            if (ktAdmin.Count == 0 && (DungChung.Bien.listKPHoatDong.Where(p => p == _makpcd).Count() == 0 || ktraKPChiDinh > 0) && ((kpsd != null && kpsd.MaKPsd != null && !kpsd.MaKPsd.Contains(_makpcd.ToString())) || kpsd == null || (kpsd != null && kpsd.MaKPsd == null)))
            {
                kt = false;
                MessageBox.Show("Không thể xóa! Có chỉ định không đúng mã khoa phòng !");
            }
            else
            {
                //if ((DungChung.Bien.TamThuCLS == 1 || DungChung.Bien.TamThuCLS == 2))
                //{
                var ktTT = (from cd in _data.ChiDinhs.Where(p => _lIDCD.Contains(p.IDCD)).Where(p => p.SoPhieu != null && p.SoPhieu > 0)
                            join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                            select new { dv.TenDV }).Distinct().ToList(); ;
                if (ktTT.Count > 0)
                {
                    kt = false;
                    MessageBox.Show("Không thể xóa. Các dịch vụ " + string.Join(",", ktTT) + " đã được tạm thu!");
                }

                var kp = _data.KPhongs.FirstOrDefault(o => o.MaKP == DungChung.Bien.MaKP);

                if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && DTBN == 1 && kp != null && kp.PLoai != "Admin")
                {
                    var ktbarcode = ktKP.Where(p => p.BarCode != null || p.BarCode != "").ToList();
                    if (ktbarcode.Count > 0)
                    {
                        _lIdCLS = ktbarcode.Select(p => p.IdCLS).ToList();

                        var ktTT1 = (from cd in _data.ChiDinhs.Where(p => _lIdCLS.Contains(p.IdCLS ?? 0))
                                     join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                                     select new { dv.TenDV }).Distinct().ToList();
                        kt = false;
                        MessageBox.Show("Không thể xóa. Các dịch vụ " + string.Join(",", ktTT1) + " đã khớp mã barcode!");
                    }
                }
                //}
                var ktThanhToan = from cls in _data.CLS.Where(p => _lIdCLS.Contains(p.IdCLS))
                                  join vp in _data.VienPhis on cls.MaBNhan equals vp.MaBNhan
                                  select cls;
                if (ktThanhToan.Count() > 0)
                {
                    kt = false;
                    MessageBox.Show("Bệnh nhân đã thanh toán không thể xóa!");
                }
                if (kt)
                {
                    var ktRaVien = from cls in _data.CLS.Where(p => _lIdCLS.Contains(p.IdCLS))
                                   join rv in _data.RaViens on cls.MaBNhan equals rv.MaBNhan
                                   select cls;
                    if (ktRaVien.Count() > 0)
                    {
                        kt = false;
                        MessageBox.Show("Bệnh nhân đã ra viện không thể xóa!");
                    }
                }
                if ((kt && DungChung.Bien.MaBV != "14017") || (DungChung.Bien.MaBV == "14017" && idTTPT.Count > 0))
                {
                    var kq = (from chidinh in _data.ChiDinhs.Where(p => _lIDCD.Contains(p.IDCD)) select new { chidinh.Status }).Where(p => p.Status == 1).ToList();
                    if (kq.Count > 0)
                    {
                        kt = false;
                        MessageBox.Show("Chỉ định đã có kết quả bạn không được xóa");
                    }
                    else
                    {
                        #region kiểm tra dịch vụ đã được khoa CLS kê HCVTYT (nếu có dịch vụ cuối cùng mà khoa CLS đó thực hiện thì không được xóa)
                        string tenkp = "";
                        foreach (var a in ktKP)
                        {
                            int makpth = a.MaKPth == null ? 0 : a.MaKPth.Value;
                            var qkpCLS = _kphong.Where(p => p.MaKP == makpth).FirstOrDefault();

                            //if (qkpCLS != null)
                            //{
                            var qhcvt = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _Mabn).Where(p => p.MaKP == makpth).Where(p => p.KieuDon == 7)
                                         join dtct in _data.DThuoccts on dt.IDDon equals dtct.IDDon
                                         select dtct
                                            ).Where(p => p.AttachIDDonct == null || p.AttachIDDonct == 0).ToList();
                            if (qhcvt.Count > 0)
                            {
                                var qclsCtConLai = (from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn && p.MaKPth == makpth)
                                                    join cd in _data.ChiDinhs.Where(p => !_lIDCD.Contains(p.IDCD))
                                                    on cls.IdCLS equals cd.IdCLS
                                                    select cd).ToList();
                                if (qclsCtConLai.Count == 0)
                                {
                                    if (tenkp == "")
                                        tenkp = qkpCLS.TenKP;
                                    else
                                        tenkp += ";" + qkpCLS.TenKP;
                                }
                                // }
                            }

                        }
                        if (tenkp != "")
                        {
                            kt = false;
                            MessageBox.Show("Phòng " + tenkp + " đã kê hóa chất, vật tư y tế cho bệnh nhân, bạn không thể xóa chỉ định");
                        }
                        #endregion
                    }
                }
            }

            #endregion
            #region xoa
            if (_lIDCD.Count == 0)
                MessageBox.Show("Bạn chưa chọn dịch vụ để xóa");
            else if (kt && _lIDCD.Count > 0)
            {
                DialogResult rs = MessageBox.Show("Bạn muốn xóa các dịch vụ Cận lâm sàng này?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    var _lCLSct = (from chidinh in _data.ChiDinhs.Where(p => _lIDCD.Contains(p.IDCD))
                                   join clsct in _data.CLScts on chidinh.IDCD equals clsct.IDCD
                                   select clsct).ToList();
                    if (_lCLSct.Count > 0)
                    {
                        foreach (var i in _lCLSct)
                        {
                            _data.CLScts.Remove(i);
                        }

                    }

                    var _lChiDinh = (from chidinh in _data.ChiDinhs.Where(p => _lIDCD.Contains(p.IDCD)) select chidinh).ToList();
                    if (_lChiDinh.Count > 0)
                    {
                        foreach (var c in _lChiDinh)
                        {
                            if (DungChung.Bien.MaBV == "14017")
                            {
                                var dv = _data.DichVus.FirstOrDefault(o => o.MaDV == c.MaDV);
                                if (dv != null && dv.IDNhom == 8)
                                {
                                    var dtct = (from dt in _data.DThuoccts.Where(p => p.IDCD == c.IDCD) select new { dt.IDDonct }).ToList();
                                    if (dtct.Count > 0)
                                    {
                                        foreach (var v in dtct)
                                        {
                                            var kiemtrachiphidinhkem = _data.DThuoccts.Where(p => p.AttachIDDonct == v.IDDonct).ToList();
                                            if (kiemtrachiphidinhkem.Count > 0)
                                            {
                                                MessageBox.Show("Dịch vụ đã có VTYT-HC đi kèm, bạn không thể xóa");
                                                return;
                                            }
                                            var xoa = _data.DThuoccts.FirstOrDefault(p => p.IDDonct == v.IDDonct);
                                            _data.DThuoccts.Remove(xoa);
                                        }
                                    }
                                }
                            }
                            _data.ChiDinhs.Remove(c);
                            _data.SaveChanges();
                        }
                    }

                    if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                    {
                        foreach (var item in ktKP)
                        {
                            usDieuTri.UpdateDienBien_DonThuoc_CLS(_data, item.MaBNhan ?? 0, DungChung.Bien.MaCB, 0, item.NgayThang ?? DateTime.Now, "", item.MaKP, false, true, false);
                        }
                    }

                    var clsChecks = (from cls in _data.CLS
                                     where _lIdCLS.Contains(cls.IdCLS)
                                       && (cls.Code.Contains("CDHA") || cls.Code.Contains("CĐHA"))
                                     select new
                                     {
                                         cls.IdCLS,
                                         cls.Code,
                                     }).ToList();

                    foreach (int idcls in _lIdCLS)
                    {
                        var qcd = _data.ChiDinhs.Where(p => p.IdCLS == idcls).ToList();
                        var isCDHA = clsChecks.Any(a => a.IdCLS == idcls);

                        if (qcd.Count == 0)
                        {
                            if (isCDHA && DungChung.Bien.MaBV.Equals("24012"))
                            {
                                var response = Task.Run(async () => await DeletePacs(idcls)).Result;
                                if (response == null || (response != null && response.ContentSuccess != null && !response.ContentSuccess.Data))
                                {
                                    MessageBox.Show("Gửi dữ liệu sang hệ thống PACS không thành công." + response == null ? string.Empty : "\n" + response.ContentSuccess.Message);
                                }
                            }

                            var xoacls = _data.CLS.Single(p => p.IdCLS == idcls);
                            _data.CLS.Remove(xoacls);
                            _data.SaveChanges();
                            if (_lIDCLS_DB.Contains(idcls))
                                _lIDCLS_DB.Remove(idcls);
                        }

                        else if (DungChung.Bien.MaBV.Equals("24012") && isCDHA)
                        {
                            var type = qcd.Count > 0 ? "NW" : "CA"; // Trường hợp xóa tất cả bản ghi thì là xóa 
                            var cls = clsSlecteds.FirstOrDefault(f => f.IdCLS == idcls);

                            //var response = Task.Run(async () => await SendPacs(DungChung.Bien.MaBV, _Mabn, idcls, cls.NgayThang.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss"), type)).Result;
                            //if (response == null || (response != null && response.ContentSuccess != null && !response.ContentSuccess.Data))
                            //{
                            //    MessageBox.Show("Gửi dữ liệu sang hệ thống PACS không thành công." + response == null ? string.Empty : "\n" + response.ContentSuccess.Message);
                            //}

                            Task.Run(async () => await SendPacs(DungChung.Bien.MaBV, _Mabn, idcls, cls.NgayThang.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss"), type));
                        }
                    }

                    _setStatus(_Mabn);
                    MessageBox.Show("Đã xoá thành công!");
                    GrcNhomDV.Enabled = true;
                    enableControl(false);
                    _listDichVu = _listDichVu.Where(p => !_lIDCD.Contains(p.idcd)).ToList();
                    action = true;
                    //FRM_chidinh_Load(sender, e);
                    trangthai = 0;
                    FRM_chidinh_Load(sender, e);
                    if (reLoad != null)
                        reLoad();
                }
            }
            #endregion
            #endregion
            #region Cũ
            //if (_idCLS > 0)
            //    {
            //        var ktKP = from cls in _data.CLS.Where(p => p.IdCLS == _idCLS) select cls;
            //        var ktAdmin = _kphong.Where(p => p.MaKP == DungChung.Bien.MaKP && p.PLoai == "Admin").ToList();
            //        if (ktAdmin.Count == 0 && (ktKP.Count() > 0 && ktKP.First().MaKP != DungChung.Bien.MaKP))
            //        {
            //            kt = false;
            //            MessageBox.Show("Không thể xóa! Đây không phải là khoa phòng chỉ định!");
            //        }
            //        else
            //        {
            //            if ((DungChung.Bien.TamThuCLS == 1 || DungChung.Bien.TamThuCLS == 2))
            //            {
            //                var ktTT = from cls in _data.CLS.Where(p => p.IdCLS == _idCLS)
            //                           join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
            //                           select cd;
            //                foreach (var item in ktTT)
            //                {
            //                    if (item.SoPhieu > 0)
            //                    {
            //                        kt = false;
            //                        MessageBox.Show("Không thể xóa. Chỉ định đã được tạm thu!");
            //                        break;
            //                    }
            //                }
            //            }
            //            var ktThanhToan = from cls in _data.CLS.Where(p => p.IdCLS == _idCLS)
            //                              join vp in _data.VienPhis on cls.MaBNhan equals vp.MaBNhan
            //                              select cls;
            //            if (ktThanhToan.Count() > 0)
            //            {
            //                kt = false;
            //                MessageBox.Show("Bệnh nhân đã thanh toán không thể xóa!");
            //            }
            //            if (kt)
            //            {
            //                var ktRaVien = from cls in _data.CLS.Where(p => p.IdCLS == _idCLS)
            //                               join rv in _data.RaViens on cls.MaBNhan equals rv.MaBNhan
            //                               select cls;
            //                if (ktRaVien.Count() > 0)
            //                {
            //                    kt = false;
            //                    MessageBox.Show("Bệnh nhân đã ra viện không thể xóa!");
            //                }
            //            }
            //            if (kt)
            //            {
            //                var kq = (from chidinh in _data.ChiDinhs.Where(p => p.IdCLS == _idCLS) select new { chidinh.Status }).ToList();
            //                if (kq.Count > 0)
            //                {
            //                    if (kq.First().Status != null && kq.First().Status.Value == 1)
            //                    {
            //                        kt = false;
            //                        MessageBox.Show("Chỉ định đã có kết quả bạn không được xóa");
            //                    }
            //                }
            //            }
            //        }
            //        if (kt)
            //        {
            //            DialogResult rs = MessageBox.Show("Bạn muốn xóa dịch vụ Cận lâm sàng này?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //            if (rs == DialogResult.Yes)
            //            {
            //                var _lCLSct = (from chidinh in _data.ChiDinhs.Where(p => p.IdCLS == _idCLS)
            //                               join clsct in _data.CLScts on chidinh.IDCD equals clsct.IDCD
            //                               select clsct).ToList();
            //                if (_lCLSct.Count > 0)
            //                {
            //                    foreach (var i in _lCLSct)
            //                    {
            //                        _data.CLScts.Remove(i);
            //                        _data.SaveChanges();
            //                    }

            //                }

            //                var _lChiDinh = (from chidinh in _data.ChiDinhs.Where(p => p.IdCLS == _idCLS) select chidinh).ToList();
            //                if (_lChiDinh.Count > 0)
            //                {
            //                    foreach (var c in _lChiDinh)
            //                    {
            //                        _data.ChiDinhs.Remove(c);
            //                        _data.SaveChanges();
            //                    }
            //                }
            //                var xoacls = _data.CLS.Single(p => p.IdCLS == _idCLS);
            //                _data.CLS.Remove(xoacls);
            //                _data.SaveChanges();
            //                _setStatus(_Mabn);
            //                MessageBox.Show("Đã xoá thành công!");
            //                GrcNhomDV.Enabled = true;
            //                enableControl(false);
            //                trangthai = 0;
            //                FRM_chidinh_Load(sender, e);
            //            }
            //        }
            //    }
            #endregion
        }
        #region sửa thành trạng thái đã khám nếu xóa hết các chỉ định
        //dung280516
        public static bool _setStatus(int _Mabn)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qcd = (_data.CLS.Where(p => p.MaBNhan == _Mabn && p.Status == 0)).ToList();
            var Ktra = _data.BNKBs.Where(p => p.MaBNhan == _Mabn).ToList();
            if (qcd.Count == 0)
            {

                BenhNhan bnsua = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn && p.Status != 2 && p.Status != 3).FirstOrDefault();
                if (bnsua != null)
                {
                    if (Ktra.Count > 0)
                        bnsua.Status = 1;
                    else
                        bnsua.Status = 0;
                }
                _data.SaveChanges();
            }
            return true;
        }
        #endregion

        void LoadKPTH()
        {
            foreach (var item in _listDichVu.Where(p => p.Chon == true))
            {
                if (item.nhom != null)
                {
                    string ChuyenKhoa = item.nhom;
                    //dtNgayCD.DateTime = System.DateTime.Now;
                    if (ChuyenKhoa.Contains("XN") && (DungChung.Bien.MaBV != "30299" || ChuyenKhoa != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh))
                        ChuyenKhoa = "Xét nghiệm";
                    List<KPhong> _lkphong = new List<KPhong>();

                    if (ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat)
                    {
                        int _makp = 0;
                        if (lupKhoaphong.EditValue != null)
                        {
                            _makp = Convert.ToInt32(lupKhoaphong.EditValue);
                        }
                        _lkphong = (from KP in _kphong.Where(p => p.MaKP == _makp || p.MaKP == DungChung.Bien.MaKP || p.ChuyenKhoa == "Nội soi" || (p.ChuyenKhoa != null && p.ChuyenKhoa.ToLower() == ChuyenKhoa.ToLower())) select KP).OrderBy(p => p.TenKP).ToList();
                        if (ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && _lkphong.Where(p => p.ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).ToList().Count > 0)
                            item.MaKPTH = _lkphong.Where(p => p.ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat).First().MaKP;
                        else
                            if (_lkphong.Where(p => p.MaKP == (_makp)).ToList().Count > 0)
                        {
                            item.MaKPTH = _lkphong.Where(p => p.MaKP == _makp).First().MaKP;
                        }
                    }
                    else if (ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS)
                    {
                        _lkphong = (from KP in _kphong.Where(p => p.ChuyenKhoa != null && (p.ChuyenKhoa.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang.ToLower() || p.ChuyenKhoa.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS.ToLower())) select KP).OrderBy(p => p.TenKP).ToList();


                        if (_lkphong.Count > 0)
                        {
                            var qxqkts = _kphong.Where(p => p.ChuyenKhoa != null && p.ChuyenKhoa.ToLower() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS.ToLower()).OrderBy(p => p.TenKP).ToList();
                            if (qxqkts.Count() > 0)
                                item.MaKPTH = qxqkts.First().MaKP;
                            else
                                item.MaKPTH = _lkphong.First().MaKP;
                        }
                    }
                    else
                    {
                        _lkphong = (from KP in _kphong.Where(p => p.ChuyenKhoa != null && p.ChuyenKhoa.ToLower() == (ChuyenKhoa.ToLower())) select KP).OrderBy(p => p.TenKP).ToList();
                        if (_lkphong.Count > 0)
                        {
                            item.MaKPTH = _lkphong.First().MaKP;
                        }
                    }
                }
            }
        }
        public bool KtraChiPhi(QLBV_Database.QLBVEntities _data, int _mabn, double TongTienThuoc)
        {
            var ttbs = _data.TTboXungs.FirstOrDefault(o => o.MaBNhan == _mabn);

            var _lcpcd = (from cls in _data.CLS.Where(p => p.MaBNhan == _mabn)
                          join cd in _data.ChiDinhs.Where(p => p.SoPhieu <= 0 || p.SoPhieu == null).Where(p => p.Status == 0) on cls.IdCLS equals cd.IdCLS
                          select new { cd.IDCD, cd.MaDV, cd.DonGia }).ToList();
            var _dthuoc = (from dt in _data.DThuocs.Where(p => p.MaBNhan == _mabn)
                           join dtct in _data.DThuoccts.Where(p => p.ThanhToan == 0) on dt.IDDon equals dtct.IDDon
                           select new { dtct.IDDonct, dtct.ThanhTien }).ToList();
            double TongTien = 0;
            if (_lcpcd.Count > 0)
                TongTien = _lcpcd.Sum(p => p.DonGia);
            if (_lcpcd.Count > 0)
                TongTien = _dthuoc.Sum(p => p.ThanhTien) + TongTien;
            var _tung = _data.TamUngs.Where(p => p.MaBNhan == _mabn).Where(p => p.PhanLoai == 0).ToList();
            double TienTU = _tung.Count > 0 ? _tung.Sum(p => p.SoTien ?? 0) : 0;

            if (ttbs != null && ttbs.HTThanhToan == 1 && DungChung.Bien.MaBV == "01071")
                return true;
            else if (TongTien + TongTienThuoc > TienTU)
                return false;
            else
                return true;
        }

        private void RefreshGridDichVu()
        {
            var data = (IEnumerable<C_DichVu>)grcDichvu.DataSource;
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (item.Chon)
                        item.Chon = false;
                }
                grcDichvu.BeginUpdate();
                grcDichvu.DataSource = data;
                grcDichvu.EndUpdate();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                if (grvDichvu.GetFocusedRowCellValue(MaDV) != null)
                {
                    var idCLSs = new List<int>();

                    int _idCLS = 0;
                    int _idCCD = 0;
                    var dateCD = dtNgayCD.DateTime;
                    var dateCDEdit = dtNgayCD.DateTime;
                    int makpcd = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                    #region Thêm Mới
                    if (trangthai == 1) // Nếu trạng thái đang là thêm mới.
                    {
                        bool tieptuc = true;

                        if (_listDichVu.Where(p => p.Chon == true).ToList().Count <= 0)
                        {
                            MessageBox.Show("Bạn chưa chọn dịch vụ!");
                            tieptuc = false;
                        }
                        else
                        {
                            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && DTBN == 2 && _noitru == 1)
                            {
                                double TongTien = 0;
                                TongTien = _listDichVu.Where(p => p.Chon == true).Sum(p => p.DonGia);
                                int capCuu = (int)_data.BenhNhans.Where(p => p.MaBNhan == _Mabn).Select(p => p.CapCuu).FirstOrDefault();
                                if (DungChung.Bien.MaBV != "24012" || capCuu != 1)
                                {
                                    if (!KtraChiPhi(_data, _Mabn, TongTien))
                                    {
                                        //luudon = false;
                                        MessageBox.Show("Tổng chi phí đã vượt quá tổng tạm thu");
                                        tieptuc = false;
                                    }
                                }
                            }
                            if (DTBN == 1 && !string.IsNullOrEmpty(StheBHYT))
                            {
                                List<int> _dvc = _listDichVu.Where(p => p.Chon == true).Select(p => p.madv).Distinct().ToList();
                                var ktHM = (from a1 in _dvc
                                            join a2 in _lDichVuTH.Where(p => p.HMChiDinh > 0) on a1 equals a2.MaDV
                                            select new
                                            {
                                                a2
                                            }).Distinct().ToList();
                                string tendv = "";
                                foreach (var item in ktHM)
                                {
                                    int o = item.a2.HMChiDinh * -1;
                                    DateTime NgaycdHM = dtNgayCD.DateTime.Date;
                                    var kktt = (from bn1 in _data.BenhNhans.Where(p => p.SThe == StheBHYT)
                                                join cls in _data.CLS on bn1.MaBNhan equals cls.MaBNhan
                                                join cd in _data.ChiDinhs.Where(p => p.MaDV == item.a2.MaDV) on cls.IdCLS equals cd.IdCLS
                                                select new { cls }).ToList();
                                    if (kktt.Count > 0)
                                    {
                                        var check = kktt.Where(p => p.cls.NgayThang != null && p.cls.NgayThang.Value.Date.AddDays(item.a2.HMChiDinh) >= NgaycdHM).ToList();
                                        if (check != null && check.Count > 0)
                                        {
                                            tendv += (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") ? item.a2.CanhBaoHM : item.a2.TenDV + ",\n";
                                        }
                                    }
                                }
                                if (!string.IsNullOrEmpty(tendv))
                                {
                                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                    {
                                        MessageBox.Show(tendv, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else if (DungChung.Bien.MaBV == "26007")
                                    {
                                        if (MessageBox.Show("Cảnh báo bệnh nhân có dịch vụ:\n" + tendv + " chỉ định chưa đủ hạn mức! Bạn có muốn lưu tiếp?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                        {
                                            RefreshGridDichVu();
                                            return;
                                        }

                                    }
                                    else
                                        MessageBox.Show("Cảnh báo bệnh nhân có dịch vụ:\n" + tendv + " chỉ định chưa đủ hạn mức!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }

                        var bn = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                        var bnkb = _data.BNKBs.Where(p => p.MaBNhan == _Mabn).ToList();
                        var chandoan = bnkb.Where(p => p.MaBNhan == _Mabn && (p.MaKP == makpcd || p.MaKPDTKH == makpcd)).OrderByDescending(p => p.IDKB).FirstOrDefault();
                        var _kp = _data.KPhongs.Where(p => p.MaKP == makpcd).FirstOrDefault();//.Where(p => p.PLoai == "Hành chính").ToList();
                        string arrrCDBD = "";
                        if (chandoan != null && chandoan.ChanDoanBD != null)
                            arrrCDBD = chandoan.ChanDoanBD;

                        #region kiểm tra chỉ định dành cho bệnh nhân dịch vụ không có chẩn đoán
                        if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30303" && DungChung.Bien.MaBV != "30005" && DungChung.Bien.MaBV != "30007" && DungChung.Bien.MaBV != "30372")
                        {
                            if (chandoan == null)
                            {
                                MessageBox.Show("Chưa có chẩn đoán tại: " + lupKhoaphong.Text);
                                return;
                            }
                        }
                        else
                        {
                            if (_kp.PLoai == "Hành chính")
                            {
                                if (bnkb.Count > 0)
                                {
                                    MessageBox.Show("Bệnh nhân đã được khám| điều trị, Bạn không được cấp quyền");
                                }
                                if (bn.DTuong == "BHYT")
                                {
                                    MessageBox.Show("Chỉ định CLS cho bệnh nhân không có chẩn đoán chỉ dành cho BN dịch vụ");
                                    return;
                                }
                                if (bn.NoiTru == 1 || (bn.NoiTru == 0 && bn.DTNT))
                                {
                                    MessageBox.Show("Bệnh nhân đang điều trị, Bạn không được cấp quyền");
                                    return;
                                }
                                //if (bn.MaKP != makpcd)
                                //{
                                //    MessageBox.Show("Khoa phòng chỉ định không hợp lệ, \nhãy chọn khoa phòng hiện tại bệnh nhân đang được điều trị");
                                //    lupKhoaphong.Focus();
                                //    return;
                                //}
                            }
                            else
                            {
                                if (_kp.PLoai == "Phòng khám" || _kp.PLoai == "Lâm sàng")
                                {
                                    if (_kp.QuanLy != 1)
                                    {
                                        if (chandoan == null)
                                        {
                                            MessageBox.Show("Chưa có chẩn đoán tại: " + lupKhoaphong.Text);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (chandoan == null)
                                        {
                                            if (bn.DTuong == "BHYT")
                                            {
                                                MessageBox.Show("Chỉ định CLS cho bệnh nhân không có chẩn đoán chỉ dành cho BN dịch vụ");
                                                return;
                                            }
                                            //if (bn.NoiTru == 1 || (bn.NoiTru == 0 && bn.DTNT))
                                            //{
                                            //    MessageBox.Show("Bệnh nhân đang điều trị, Bạn không được cấp quyền");
                                            //    return;
                                            //}
                                            if (bn.MaKP != makpcd)
                                            {
                                                MessageBox.Show("Khoa phòng chỉ định không hợp lệ, \nhãy chọn khoa phòng hiện tại bệnh nhân đang được điều trị");
                                                lupKhoaphong.Focus();
                                                return;
                                            }
                                            if (DialogResult.No == MessageBox.Show("Chưa có chẩn đoán bạn vẫn muốn chỉ định?", "Chỉ định cận lâm sàng", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                                                return;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Không được cấp quyền");
                                    return;
                                }
                            }
                            #endregion

                        }
                        if (tieptuc)
                        {
                            int idgoidv = 0;
                            bool cdgoidv = false;
                            if (lupGoiDV.EditValue != null)
                            {
                                LoadKPTH();
                                idgoidv = Convert.ToInt32(lupGoiDV.EditValue);
                            }

                            if (DungChung.Bien.MaBV == "24009" && idgoidv > 0)
                            {
                                CDNhieuTieuNhom = true;
                                var a = _listDichVu.Where(p => p.Chon == true).ToList().Count;
                                var b = grvDichvu.DataRowCount;
                                if (a == b)
                                {
                                    cdgoidv = true;
                                }
                            }
                            #region chỉ định theo gói dịch vụ
                            //if (idgoidv > 0)
                            if (false)
                            {
                                int IdThuTT = 0;
                                //bool DaChiDinh = false;
                                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                                {
                                    var KtraTT = _data.TamUngs.Where(p => p.MaBNhan == _Mabn && p.IDGoiDV == idgoidv).ToList();
                                    if (KtraTT.Count() > 0)
                                    {
                                        var KtraCD = (from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                                                      join cd in _data.ChiDinhs.Where(p => p.IDGoi == idgoidv) on cls.IdCLS equals cd.IdCLS
                                                      select cd).ToList();
                                        if (KtraCD.Count() > 0)
                                        {
                                            //DaChiDinh = true;
                                        }
                                        else
                                        {
                                            IdThuTT = KtraTT.First().IDTamUng;
                                        }
                                    }
                                }
                                #region xét nghiệm IDCLS theo tiểu nhóm
                                var _ltn = (from dv in _listDichVu.Where(p => p.Chon == true).Where(p => p.IdNhom == 1)
                                            group dv by new { dv.nhom } into kq
                                            select new { kq.Key.nhom }).ToList();
                                int a1 = 1;
                                foreach (var tenrg in _ltn)
                                {
                                    int _idcls = 0, _idcd = 0, _idclsct = 0;
                                    var kpth = _listDichVu.Where(p => p.Chon == true).Where(p => p.nhom == tenrg.nhom).Select(p => p.MaKPTH).FirstOrDefault();
                                    int idCLSnew = 0;
                                    CL themmoi = new CL();
                                    themmoi.MaBNhan = _Mabn;
                                    themmoi.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                    themmoi.MaKP = makpcd;
                                    themmoi.MaKPth = kpth != null ? kpth : 0; //LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                    //if (DungChung.Bien.MaBV == "24272" && _ltn.First().IsAutoExecute == true)
                                    //{
                                    //    themmoi.MaCBth = themmoi.MaCB;
                                    //    themmoi.NgayTH = dtNgayCD.DateTime;
                                    //    themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                    //    if (_ltn.FirstOrDefault().SoPhutThucHien != null)
                                    //    {
                                    //        if (_ltn.FirstOrDefault().SoPhutThucHien != 0)
                                    //        {
                                    //            themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(Convert.ToDouble(_ltn.FirstOrDefault().SoPhutThucHien.Value));
                                    //        }
                                    //        else
                                    //        {
                                    //            themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                    //        }
                                    //    }
                                    //}
                                    themmoi.NgayThang = (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "20001") ? dtNgayCD.DateTime.AddMinutes(a1) : dtNgayCD.DateTime;
                                    if (_idDB > 0)
                                        themmoi.IDDienBien = _idDB;
                                    a1++;
                                    themmoi.CapCuu = ckCapCuu.Checked;
                                    if (_idDB > 0)
                                        themmoi.IDDienBien = _idDB;
                                    if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30303")
                                    {
                                        if (chandoan != null)
                                        {

                                            themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                            themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                            if (DungChung.Bien.MaBV == "20001")
                                            {
                                                themmoi.MaYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[0];
                                                themmoi.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[1];
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (_kp.PLoai != "Hành chính")
                                        {
                                            if (_kp.PLoai == "Phòng khám" || _kp.PLoai == "Lâm sàng")
                                            {
                                                if (_kp.QuanLy == 1)
                                                {
                                                    if (chandoan != null)
                                                    {
                                                        themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                        themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                    }
                                                    else
                                                    {
                                                        var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                        if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                        {
                                                            themmoi.ChanDoan = _lTTBN.First().TChung;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (chandoan != null)
                                                    {
                                                        themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                        themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                            if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                            {
                                                themmoi.ChanDoan = _lTTBN.First().TChung;
                                            }
                                        }
                                    }
                                    bool XetNgiem = true;
                                    themmoi.Code = txtCode.Text;
                                    int _sott = 0;
                                    if (DungChung.Bien.MaBV != "30007")
                                    {
                                        int makp = LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                        if (XetNgiem)
                                        {
                                            int ktsoTT = _data.SoTTs.Where(p => p.NgayDK == dtNgayCD.DateTime.Date && p.MaKP == makp).Select(p => p.SoTT1).FirstOrDefault();
                                            if (ktsoTT > 0)
                                            {
                                                var ktstatu = _data.CLS.Where(p => p.MaBNhan == _Mabn && p.Status == 0 && p.MaKPth == makp).ToList();
                                                if (ktstatu.Count > 0)
                                                {
                                                    _sott = ktsoTT;
                                                }
                                                else
                                                    _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                            }
                                            else
                                                _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                        }
                                        else
                                            _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                        themmoi.STT = _sott;
                                    }

                                    _data.CLS.Add(themmoi);
                                    _data.SaveChanges();
                                    _idcls = themmoi.IdCLS;
                                    _lIDCLS_DB.Add(themmoi.IdCLS);

                                    // Thêm vào bảng ChiDinh
                                    _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    //set sott
                                    if (DungChung.Bien.MaBV != "30007")
                                    {
                                        FormNhap.frmHSBNNhapMoi._setSoTT(_data, dtNgayCD.DateTime, LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue), _sott, _Mabn);
                                    }

                                    foreach (var item in _listDichVu.Where(p => p.Chon == true && p.nhom == tenrg.nhom))
                                    {
                                        // kiểm tra chỉ định trong ngày
                                        bool _ThemMoi = true;
                                        var kt = from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                                                 join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                                 where cd.MaDV == item.madv
                                                 where cls.NgayThang != null && cls.NgayThang.Value.Day == dtNgayCD.DateTime.Day
                                                 select cd.MaDV;
                                        if (kt.Count() > 0)
                                        {
                                            string thongbao = "Dịch vụ" + _data.DichVus.Where(p => p.MaDV == kt.FirstOrDefault()).First().TenDV + " đã được chỉ định trong ngày, bạn vẫn muốn chỉ định thêm?";
                                            DialogResult rs = MessageBox.Show(thongbao, "Thông báo", MessageBoxButtons.OKCancel);
                                            if (rs == DialogResult.Cancel)
                                                _ThemMoi = false;
                                        }
                                        // kiểm tra đơn giá
                                        if (item.DonGia <= 0)
                                        {
                                            string thongbao1 = "Dịch vụ" + _lDichVu.Where(p => p.madv == item.madv).Select(p => p.tendv).FirstOrDefault() + " chưa có đơn giá, vui lòng kiểm tra lại!";
                                            MessageBox.Show(thongbao1);
                                            _ThemMoi = false;
                                        }
                                        if (_ThemMoi)
                                        {
                                            ChiDinh themmoiCD = new ChiDinh();
                                            themmoiCD.IdCLS = idCLSnew;
                                            themmoiCD.MaDV = item.madv;
                                            themmoiCD.Status = 0;
                                            themmoiCD.DonGia = DungChung.Ham._getGiaDM(_data, item.madv, item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1), _Mabn, dtNgayCD.DateTime);//item.DonGia;
                                            themmoiCD.IDGoi = idgoidv;
                                            if (IdThuTT > 0)
                                                themmoiCD.SoPhieu = IdThuTT;
                                            if (DTBN == 1)
                                            {
                                                themmoiCD.TrongBH = item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1);
                                            }
                                            else
                                            {
                                                themmoiCD.TrongBH = 0;
                                            }
                                            themmoiCD.XHH = 0;
                                            themmoiCD.LoaiDV = Convert.ToInt32(item.TheoYC);
                                            for (int i = 0; i < grvCLS.RowCount; i++)
                                            {
                                                if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh) != null)
                                                {
                                                    int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                    string ylenh = grvCLS.GetRowCellValue(i, colYLenh).ToString();
                                                    if (item.madv == madv)
                                                    {
                                                        themmoiCD.ChiDinh1 = ylenh;
                                                        //break;
                                                    }

                                                }
                                                if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh2) != null)
                                                {
                                                    int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                    string ylenh2 = grvCLS.GetRowCellValue(i, colYLenh2).ToString();
                                                    if (item.madv == madv)
                                                    {
                                                        themmoiCD.YLenh2 = ylenh2;
                                                        //break;
                                                    }

                                                }
                                                if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colXHH) != null)
                                                {
                                                    int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                    bool xhh = Convert.ToBoolean(grvCLS.GetRowCellValue(i, colXHH).ToString());
                                                    if (item.madv == madv)
                                                    {
                                                        themmoiCD.XHH = (xhh == true) ? 1 : 0;
                                                        // break;
                                                    }
                                                }

                                            }
                                            if (DungChung.Bien.MaBV == "24009" && cdgoidv)
                                            {
                                                themmoiCD.IDGoi = idgoidv;
                                            }
                                            _data.ChiDinhs.Add(themmoiCD);
                                            int cd = _data.SaveChanges();
                                            if (cd > 0)
                                            {
                                                if (DungChung.Bien.MaBV == "0107123")
                                                {
                                                    var DV = _data.DichVus.Where(p => p.MaDV == themmoiCD.MaDV).FirstOrDefault();
                                                    var CB = _data.CanBoes.Where(p => p.MaCB == themmoi.MaCB).FirstOrDefault();
                                                    ChiDinhToPacs guiDL = new ChiDinhToPacs();
                                                    guiDL.MaChiDinh = themmoiCD.IDCD;
                                                    guiDL.ChanDoan = (chandoan != null) ? chandoan.ChanDoan : "";
                                                    guiDL.DiaChi = bn.DChi;
                                                    guiDL.GioiTinh = bn.GTinh == 1 ? "M" : "WM";
                                                    guiDL.MaBacSiChiDinh = themmoi.MaCB;
                                                    guiDL.MaBenhNhan = themmoi.MaBNhan;
                                                    guiDL.MaDichVu = themmoiCD.MaDV;
                                                    guiDL.NgaySinh = bn.NgaySinh;
                                                    guiDL.ThangSinh = bn.ThangSinh;
                                                    guiDL.NamSinh = bn.NamSinh;
                                                    guiDL.NhomDichVu = item.nhom;
                                                    guiDL.NoiChiDinh = _kp.TenKP;
                                                    guiDL.SDT = "";
                                                    guiDL.TenBacSiChiDinh = CB.TenCB;
                                                    guiDL.TenBenhNhan = bn.TenBNhan;
                                                    guiDL.TenDichVu = DV.TenDV;
                                                    guiDL.ThoiGianChiDinh = themmoi.NgayThang;
                                                    guiDL.TrangThai = "NW";

                                                    _ldata.Add(guiDL);

                                                }
                                            }
                                        }

                                    }

                                    // Thêm vào bảng CLSct
                                    var d = (from cls in _data.CLS.Where(p => p.IdCLS == idCLSnew)
                                             join chidinh in _data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                             join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                                             join dvct in _data.DichVucts on chidinh.MaDV equals dvct.MaDV
                                             select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, chidinh.Status, chidinh.IDCD, dvct.STT, dv.IDNhom, dv.IsAutoExecute, dv.SoPhutThucHien }).ToList();
                                    if (d.Count > 0)
                                    {
                                        int dem = 0; // xác định có thêm vào bảng CLSct không
                                        foreach (var item in d)
                                        {
                                            CLSct themmoiCL = new CLSct();
                                            themmoiCL.IDCD = item.IDCD;
                                            themmoiCL.MaDVct = item.MaDVct;
                                            themmoiCL.Status = 0;
                                            themmoiCL.STTHT = item.STT;
                                            _data.CLScts.Add(themmoiCL);
                                            _data.SaveChanges();
                                            dem++;
                                            if (DungChung.Bien.MaBV == "24297")
                                            {
                                                if (item.TenDVct.ToUpper().Contains("LƯU HUYẾT NÃO") && item.IsAutoExecute == true)
                                                {
                                                    thuchienLHN(idCLSnew, item.IDCD);
                                                }
                                            }
                                            else if (item.TenDVct.ToUpper().Contains("LƯU HUYẾT NÃO"))
                                            {
                                                thuchienLHN(idCLSnew, item.IDCD);
                                            }
                                        }

                                        //dung280516
                                        if (dem > 0)
                                        {
                                            BenhNhan sua = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                                            if (sua != null)
                                            {
                                                sua.Status = 4;
                                            }
                                            _data.SaveChanges();
                                        }

                                        var dsChiDinh = d.Select(o => new { IDNhom = o.IDNhom, IDCD = o.IDCD, o.SoPhutThucHien, o.IsAutoExecute }).Distinct().ToList();
                                        foreach (var item in dsChiDinh)
                                        {
                                            if ((DungChung.Bien.MaBV == "14017" || (DungChung.Bien.MaBV == "24297" && item.IsAutoExecute == true)) && item.IDNhom == 8)
                                                ThucHienTTPT(_data, _Mabn, item.IDCD, item.SoPhutThucHien != null ? dtNgayCD.DateTime.AddMinutes(item.SoPhutThucHien ?? 0) : dtNgayCD.DateTime.AddMinutes(5), Convert.ToInt32(lupKhoaphong.EditValue), lupNguoiKhamkb.EditValue.ToString());
                                        }
                                        //
                                    }

                                }

                                #endregion

                                var _lttheodv = _listDichVu.Where(p => p.Chon == true).Where(p => DungChung.Bien.MaBV != "24009" ? p.IdNhom != 1 : true).ToList();

                                foreach (var a in _lttheodv)
                                {
                                    //var kpth = _listDichVu.Where(p => p.Chon == true).Where(p => p.nhom == a.nhom).Select(p => p.MaKPTH).FirstOrDefault();
                                    int idCLSnew = 0;
                                    CL themmoi = new CL();
                                    themmoi.MaBNhan = _Mabn;
                                    themmoi.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                    themmoi.MaKP = makpcd;
                                    themmoi.MaKPth = a.MaKPTH != null ? a.MaKPTH : 0; //LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                    themmoi.NgayThang = (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "20001") ? dtNgayCD.DateTime.AddMinutes(a1) : dtNgayCD.DateTime;
                                    if (DungChung.Bien.MaBV == "24272" && _lttheodv.First().IsAutoExecute == true)
                                    {
                                        themmoi.MaCBth = themmoi.MaCB;
                                        themmoi.NgayTH = dtNgayCD.DateTime;
                                        themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                        if (_lttheodv.FirstOrDefault().SoPhutThucHien != null)
                                        {
                                            if (_lttheodv.FirstOrDefault().SoPhutThucHien != 0)
                                            {
                                                themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(Convert.ToDouble(_lttheodv.FirstOrDefault().SoPhutThucHien.Value));
                                            }
                                            else
                                            {
                                                themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                            }
                                        }
                                    }
                                    if (_idDB > 0)
                                        themmoi.IDDienBien = _idDB;
                                    a1++;
                                    themmoi.CapCuu = ckCapCuu.Checked;
                                    if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30303")
                                    {
                                        if (chandoan != null)
                                        {

                                            themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                            themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                            if (DungChung.Bien.MaBV == "20001")
                                            {
                                                themmoi.MaYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[0];
                                                themmoi.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[1];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (_kp.PLoai != "Hành chính")
                                        {
                                            if (_kp.PLoai == "Phòng khám" || _kp.PLoai == "Lâm sàng")
                                            {
                                                if (_kp.QuanLy == 1)
                                                {
                                                    if (chandoan != null)
                                                    {
                                                        themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                        themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                    }
                                                    else
                                                    {
                                                        var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                        if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                        {
                                                            themmoi.ChanDoan = _lTTBN.First().TChung;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (chandoan != null)
                                                    {
                                                        themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                        themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                            if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                            {
                                                themmoi.ChanDoan = _lTTBN.First().TChung;
                                            }
                                        }
                                    }
                                    //bool XetNgiem = false;
                                    //if (lupTN.Text.Contains("XN"))
                                    //{
                                    //    themmoi.Code = txtCode.Text;
                                    //    XetNgiem = true;
                                    //}
                                    //else
                                    themmoi.Code = "CDHA";
                                    bool XetNgiem = false;
                                    int _sott = 0;
                                    if (DungChung.Bien.MaBV != "30007")
                                    {
                                        int makp = LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                        if (XetNgiem)
                                        {
                                            int ktsoTT = _data.SoTTs.Where(p => p.NgayDK == dtNgayCD.DateTime.Date && p.MaKP == makp).Select(p => p.SoTT1).FirstOrDefault();
                                            if (ktsoTT > 0)
                                            {
                                                var ktstatu = _data.CLS.Where(p => p.MaBNhan == _Mabn && p.Status == 0 && p.MaKPth == makp).ToList();
                                                if (ktstatu.Count > 0)
                                                {
                                                    _sott = ktsoTT;
                                                }
                                                else
                                                    _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                            }
                                            else
                                                _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                        }
                                        else
                                            _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                        themmoi.STT = _sott;
                                    }

                                    _data.CLS.Add(themmoi);
                                    _data.SaveChanges();
                                    idCLSnew = themmoi.IdCLS;
                                    _lIDCLS_DB.Add(themmoi.IdCLS);
                                    // Thêm vào bảng ChiDinh
                                    _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    //set sott
                                    if (DungChung.Bien.MaBV != "30007")
                                    {
                                        FormNhap.frmHSBNNhapMoi._setSoTT(_data, dtNgayCD.DateTime, LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue), _sott, _Mabn);
                                    }

                                    foreach (var item in _listDichVu.Where(p => p.Chon == true && p.madv == a.madv))
                                    {
                                        // kiểm tra chỉ định trong ngày
                                        bool _ThemMoi = true;
                                        var kt = from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                                                 join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                                 where cd.MaDV == item.madv
                                                 where cls.NgayThang != null && cls.NgayThang.Value.Day == dtNgayCD.DateTime.Day
                                                 select cd.MaDV;
                                        if (kt.Count() > 0)
                                        {
                                            string thongbao = "Dịch vụ" + _data.DichVus.Where(p => p.MaDV == kt.FirstOrDefault()).First().TenDV + " đã được chỉ định trong ngày, bạn vẫn muốn chỉ định thêm?";
                                            DialogResult rs = MessageBox.Show(thongbao, "Thông báo", MessageBoxButtons.OKCancel);
                                            if (rs == DialogResult.Cancel)
                                                _ThemMoi = false;
                                        }
                                        // kiểm tra đơn giá
                                        if (item.DonGia <= 0)
                                        {
                                            string thongbao1 = "Dịch vụ" + _lDichVu.Where(p => p.madv == item.madv).Select(p => p.tendv).FirstOrDefault() + " chưa có đơn giá, vui lòng kiểm tra lại!";
                                            MessageBox.Show(thongbao1);
                                            _ThemMoi = false;
                                        }
                                        if (_ThemMoi)
                                        {
                                            ChiDinh themmoiCD = new ChiDinh();
                                            themmoiCD.IdCLS = idCLSnew;
                                            themmoiCD.MaDV = item.madv;
                                            themmoiCD.Status = 0;
                                            //themmoiCD.DonGia = _getGiaDM(_lDichVuTH, item.madv, item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1), _Mabn, dtNgayCD.DateTime);//item.DonGia;
                                            themmoiCD.DonGia = DungChung.Ham._getGiaDM(_data, item.madv, item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1), _Mabn, dtNgayCD.DateTime);//item.DonGia;
                                            themmoiCD.IDGoi = idgoidv;
                                            if (IdThuTT > 0)
                                                themmoiCD.SoPhieu = IdThuTT;
                                            if (DTBN == 1)
                                            {
                                                themmoiCD.TrongBH = item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1);
                                            }
                                            else
                                            {
                                                themmoiCD.TrongBH = 0;
                                            }
                                            themmoiCD.XHH = 0;
                                            themmoiCD.LoaiDV = Convert.ToInt32(item.TheoYC);
                                            themmoiCD.TenDungCu = item.TenDungCu;
                                            themmoiCD.NDVaoVien = item.NDVaoVien;
                                            for (int i = 0; i < grvCLS.RowCount; i++)
                                            {
                                                if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh) != null)
                                                {
                                                    int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                    string ylenh = grvCLS.GetRowCellValue(i, colYLenh).ToString();
                                                    if (item.madv == madv)
                                                    {
                                                        themmoiCD.ChiDinh1 = ylenh;
                                                        //break;
                                                    }

                                                }
                                                if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh2) != null)
                                                {
                                                    int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                    string ylenh2 = grvCLS.GetRowCellValue(i, colYLenh2).ToString();
                                                    if (item.madv == madv)
                                                    {
                                                        themmoiCD.YLenh2 = ylenh2;
                                                        //break;
                                                    }

                                                }
                                                if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colXHH) != null)
                                                {
                                                    int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                    bool xhh = Convert.ToBoolean(grvCLS.GetRowCellValue(i, colXHH).ToString());
                                                    if (item.madv == madv)
                                                    {
                                                        themmoiCD.XHH = (xhh == true) ? 1 : 0;
                                                        // break;
                                                    }
                                                }

                                            }
                                            if (DungChung.Bien.MaBV == "24009" && cdgoidv)
                                            {
                                                themmoiCD.IDGoi = idgoidv;
                                            }
                                            _data.ChiDinhs.Add(themmoiCD);
                                            int cd = _data.SaveChanges();
                                            if (cd > 0)
                                            {
                                                if (DungChung.Bien.MaBV == "01071" && (a.IdNhom == 2 && a.IdNhom == 3 || lupTN.Text.ToUpper().Contains("NỘI SOI")))
                                                {
                                                    var DV = _data.DichVus.Where(p => p.MaDV == themmoiCD.MaDV).FirstOrDefault();
                                                    var CB = _data.CanBoes.Where(p => p.MaCB == themmoi.MaCB).FirstOrDefault();
                                                    ChiDinhToPacs guiDL = new ChiDinhToPacs();
                                                    guiDL.MaChiDinh = themmoiCD.IDCD;
                                                    guiDL.ChanDoan = (chandoan != null) ? chandoan.ChanDoan : "";
                                                    guiDL.DiaChi = bn.DChi;
                                                    guiDL.GioiTinh = bn.GTinh == 1 ? "M" : "WM";
                                                    guiDL.MaBacSiChiDinh = themmoi.MaCB;
                                                    guiDL.MaBenhNhan = themmoi.MaBNhan;
                                                    guiDL.MaDichVu = themmoiCD.MaDV;
                                                    guiDL.NgaySinh = bn.NgaySinh;
                                                    guiDL.ThangSinh = bn.ThangSinh;
                                                    guiDL.NamSinh = bn.NamSinh;
                                                    guiDL.NhomDichVu = item.nhom;
                                                    guiDL.NoiChiDinh = _kp.TenKP;
                                                    guiDL.SDT = "";
                                                    guiDL.TenBacSiChiDinh = CB.TenCB;
                                                    guiDL.TenBenhNhan = bn.TenBNhan;
                                                    guiDL.TenDichVu = DV.TenDV;
                                                    guiDL.ThoiGianChiDinh = themmoi.NgayThang;
                                                    guiDL.TrangThai = "NW";
                                                    _ldata.Add(guiDL);

                                                }
                                            }
                                        }

                                    }

                                    // Thêm vào bảng CLSct
                                    var d = (from cls in _data.CLS.Where(p => p.IdCLS == idCLSnew)
                                             join chidinh in _data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                             join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                                             join dvct in _data.DichVucts on chidinh.MaDV equals dvct.MaDV
                                             select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, chidinh.Status, chidinh.IDCD, dvct.STT, dv.IDNhom, dv.IsAutoExecute, dv.SoPhutThucHien }).ToList();
                                    if (d.Count > 0)
                                    {
                                        int dem = 0; // xác định có thêm vào bảng CLSct không
                                        foreach (var item in d)
                                        {
                                            CLSct themmoiCL = new CLSct();
                                            themmoiCL.IDCD = item.IDCD;
                                            themmoiCL.MaDVct = item.MaDVct;
                                            themmoiCL.Status = 0;
                                            themmoiCL.STTHT = item.STT;
                                            _data.CLScts.Add(themmoiCL);
                                            _data.SaveChanges();
                                            dem++;
                                        }

                                        //dung280516
                                        if (dem > 0)
                                        {
                                            BenhNhan sua = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                                            if (sua != null)
                                            {
                                                sua.Status = 4;
                                            }
                                            _data.SaveChanges();
                                        }
                                        var dsChiDinh = d.Select(o => new { IDNhom = o.IDNhom, IDCD = o.IDCD, o.SoPhutThucHien, o.IsAutoExecute }).Distinct().ToList();
                                        foreach (var item in dsChiDinh)
                                        {
                                            if ((DungChung.Bien.MaBV == "14017" || (DungChung.Bien.MaBV == "24297" && item.IsAutoExecute == true)) && item.IDNhom == 8)
                                                ThucHienTTPT(_data, _Mabn, item.IDCD, item.SoPhutThucHien != null ? dtNgayCD.DateTime.AddMinutes(item.SoPhutThucHien ?? 0) : dtNgayCD.DateTime.AddMinutes(5), Convert.ToInt32(lupKhoaphong.EditValue), lupNguoiKhamkb.EditValue.ToString());
                                        }
                                        //
                                    }
                                }
                                if (trangthai == 1 || trangthai == 2)
                                {
                                    FRM_chidinh_Load(sender, e);
                                    trangthai = 0;
                                    colYLenh.OptionsColumn.AllowEdit = false;
                                    colYLenh.OptionsColumn.ReadOnly = true;
                                    GrcNhomDV.Enabled = true;
                                    enableControl(true);
                                    btnSua.Enabled = false;
                                }
                            }
                            #endregion
                            else
                            {
                                #region CD nhiều tiểu nhóm
                                if (CDNhieuTieuNhom)
                                {
                                    LoadKPTH();
                                    #region xét nghiệm IDCLS theo tiểu nhóm
                                    var _ltn = (from dv in _listDichVu.Where(p => p.Chon == true).Where(p => p.IdNhom == 1)
                                                group dv by new { dv.nhom } into kq
                                                select new { kq.Key.nhom }).ToList();
                                    foreach (var tenrg in _ltn)
                                    {
                                        var kpth = _listDichVu.Where(p => p.Chon == true).Where(p => p.nhom == tenrg.nhom).Select(p => p.MaKPTH).FirstOrDefault();
                                        int idCLSnew = 0;
                                        CL themmoi = new CL();
                                        themmoi.MaBNhan = _Mabn;
                                        themmoi.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                        themmoi.MaKP = makpcd;
                                        themmoi.MaKPth = kpth != null ? kpth : 0; //LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                        //if (DungChung.Bien.MaBV == "24272" && _ltn.First().IsAutoExecute == true)
                                        //{
                                        //    themmoi.MaCBth = themmoi.MaCB;
                                        //    themmoi.NgayTH = dtNgayCD.DateTime;
                                        //    themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                        //    if (_ltn.FirstOrDefault().SoPhutThucHien != null)
                                        //    {
                                        //        if (_ltn.FirstOrDefault().SoPhutThucHien != 0)
                                        //        {
                                        //            themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(Convert.ToDouble(_ltn.FirstOrDefault().SoPhutThucHien.Value));
                                        //        }
                                        //        else
                                        //        {
                                        //            themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                        //        }
                                        //    }
                                        //}
                                        themmoi.NgayThang = dtNgayCD.DateTime;
                                        themmoi.CapCuu = ckCapCuu.Checked;
                                        if (_idDB > 0)
                                            themmoi.IDDienBien = _idDB;
                                        if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30303")
                                        {
                                            if (chandoan != null)
                                            {

                                                themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                if (DungChung.Bien.MaBV == "20001")
                                                {
                                                    themmoi.MaYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[0];
                                                    themmoi.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[1];
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_kp.PLoai != "Hành chính")
                                            {
                                                if (_kp.PLoai == "Phòng khám" || _kp.PLoai == "Lâm sàng")
                                                {
                                                    if (_kp.QuanLy == 1)
                                                    {
                                                        if (chandoan != null)
                                                        {
                                                            themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                            themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                        }
                                                        else
                                                        {
                                                            var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                            if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                            {
                                                                themmoi.ChanDoan = _lTTBN.First().TChung;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (chandoan != null)
                                                        {
                                                            themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                            themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                {
                                                    themmoi.ChanDoan = _lTTBN.First().TChung;
                                                }
                                            }
                                        }
                                        bool XetNgiem = true;
                                        themmoi.Code = txtCode.Text;
                                        int _sott = 0;
                                        if (DungChung.Bien.MaBV != "30007")
                                        {
                                            int makp = LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                            if (XetNgiem)
                                            {
                                                int ktsoTT = _data.SoTTs.Where(p => p.NgayDK == dtNgayCD.DateTime.Date && p.MaKP == makp).Select(p => p.SoTT1).FirstOrDefault();
                                                if (ktsoTT > 0)
                                                {
                                                    var ktstatu = _data.CLS.Where(p => p.MaBNhan == _Mabn && p.Status == 0 && p.MaKPth == makp).ToList();
                                                    if (ktstatu.Count > 0)
                                                    {
                                                        _sott = ktsoTT;
                                                    }
                                                    else
                                                        _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                                }
                                                else
                                                    _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                            }
                                            else
                                                _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                            themmoi.STT = _sott;
                                        }
                                        _data.CLS.Add(themmoi);
                                        _data.SaveChanges();
                                        idCLSnew = themmoi.IdCLS;

                                        idCLSs.Add(idCLSnew); // Lưu idcls gửi pacs

                                        _lIDCLS_DB.Add(themmoi.IdCLS);
                                        // Thêm vào bảng ChiDinh
                                        _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                        //set sott
                                        if (DungChung.Bien.MaBV != "30007")
                                        {
                                            FormNhap.frmHSBNNhapMoi._setSoTT(_data, dtNgayCD.DateTime, LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue), _sott, _Mabn);
                                        }

                                        foreach (var item in _listDichVu.Where(p => p.Chon == true && p.nhom == tenrg.nhom))
                                        {
                                            // kiểm tra chỉ định trong ngày
                                            bool _ThemMoi = true;
                                            var kt = from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                                                     join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                                     where cd.MaDV == item.madv
                                                     where cls.NgayThang != null && cls.NgayThang.Value.Day == dtNgayCD.DateTime.Day
                                                     select cd.MaDV;
                                            if (kt.Count() > 0)
                                            {
                                                string thongbao = "Dịch vụ" + _data.DichVus.Where(p => p.MaDV == kt.FirstOrDefault()).First().TenDV + " đã được chỉ định trong ngày, bạn vẫn muốn chỉ định thêm?";
                                                DialogResult rs = MessageBox.Show(thongbao, "Thông báo", MessageBoxButtons.OKCancel);
                                                if (rs == DialogResult.Cancel)
                                                    _ThemMoi = false;
                                            }
                                            // kiểm tra đơn giá
                                            if (item.DonGia <= 0)
                                            {
                                                string thongbao1 = "Dịch vụ" + _lDichVu.Where(p => p.madv == item.madv).Select(p => p.tendv).FirstOrDefault() + " chưa có đơn giá, vui lòng kiểm tra lại!";
                                                MessageBox.Show(thongbao1);
                                                _ThemMoi = false;
                                            }
                                            if (_ThemMoi)
                                            {
                                                ChiDinh themmoiCD = new ChiDinh();
                                                themmoiCD.IdCLS = idCLSnew;
                                                themmoiCD.MaDV = item.madv;
                                                themmoiCD.Status = 0;
                                                themmoiCD.TenDungCu = item.TenDungCu;
                                                themmoiCD.NDVaoVien = item.NDVaoVien;
                                                themmoiCD.DonGia = DungChung.Ham._getGiaDM(_data, item.madv, item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1), _Mabn, dtNgayCD.DateTime);//item.DonGia;
                                                if (DTBN == 1)
                                                {
                                                    themmoiCD.TrongBH = item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1);
                                                }
                                                else if (DungChung.Bien.MaBV == "20001" && benhnhan.DTuong == "Khám miễn phí")
                                                {
                                                    themmoiCD.TrongBH = 2;
                                                }
                                                else
                                                    themmoiCD.TrongBH = 0;
                                                themmoiCD.XHH = 0;
                                                themmoiCD.LoaiDV = Convert.ToInt32(item.TheoYC);
                                                for (int i = 0; i < grvCLS.RowCount; i++)
                                                {
                                                    if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh) != null)
                                                    {
                                                        int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                        string ylenh = grvCLS.GetRowCellValue(i, colYLenh).ToString();
                                                        if (item.madv == madv)
                                                        {
                                                            themmoiCD.ChiDinh1 = ylenh;
                                                            //break;
                                                        }

                                                    }
                                                    if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh2) != null)
                                                    {
                                                        int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                        string ylenh2 = grvCLS.GetRowCellValue(i, colYLenh2).ToString();
                                                        if (item.madv == madv)
                                                        {
                                                            themmoiCD.YLenh2 = ylenh2;
                                                            //break;
                                                        }

                                                    }
                                                    if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colXHH) != null)
                                                    {
                                                        int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                        bool xhh = Convert.ToBoolean(grvCLS.GetRowCellValue(i, colXHH).ToString());
                                                        if (item.madv == madv)
                                                        {
                                                            themmoiCD.XHH = (xhh == true) ? 1 : 0;
                                                            // break;
                                                        }
                                                    }
                                                }

                                                if (DungChung.Bien.MaBV == "24009" && cdgoidv)
                                                {
                                                    themmoiCD.IDGoi = idgoidv;
                                                }

                                                _data.ChiDinhs.Add(themmoiCD);
                                                int cd = _data.SaveChanges();
                                                if (cd > 0)
                                                {
                                                    if (DungChung.Bien.MaBV == "0107111")
                                                    {
                                                        var DV = _data.DichVus.Where(p => p.MaDV == themmoiCD.MaDV).FirstOrDefault();
                                                        var CB = _data.CanBoes.Where(p => p.MaCB == themmoi.MaCB).FirstOrDefault();
                                                        ChiDinhToPacs guiDL = new ChiDinhToPacs();
                                                        guiDL.MaChiDinh = themmoiCD.IDCD;
                                                        guiDL.ChanDoan = (chandoan != null) ? chandoan.ChanDoan : "";
                                                        guiDL.DiaChi = bn.DChi;
                                                        guiDL.GioiTinh = bn.GTinh == 1 ? "M" : "WM";
                                                        guiDL.MaBacSiChiDinh = themmoi.MaCB;
                                                        guiDL.MaBenhNhan = themmoi.MaBNhan;
                                                        guiDL.MaDichVu = themmoiCD.MaDV;
                                                        guiDL.NgaySinh = bn.NgaySinh;
                                                        guiDL.ThangSinh = bn.ThangSinh;
                                                        guiDL.NamSinh = bn.NamSinh;
                                                        guiDL.NhomDichVu = item.nhom;
                                                        guiDL.NoiChiDinh = _kp.TenKP;
                                                        guiDL.SDT = "";
                                                        guiDL.TenBacSiChiDinh = CB.TenCB;
                                                        guiDL.TenBenhNhan = bn.TenBNhan;
                                                        guiDL.TenDichVu = DV.TenDV;
                                                        guiDL.ThoiGianChiDinh = themmoi.NgayThang;
                                                        guiDL.TrangThai = "NW";
                                                        _ldata.Add(guiDL);

                                                    }
                                                }
                                            }

                                        }

                                        // Thêm vào bảng CLSct
                                        var d = (from cls in _data.CLS.Where(p => p.IdCLS == idCLSnew)
                                                 join chidinh in _data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                                 join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                                                 join dvct in _data.DichVucts on chidinh.MaDV equals dvct.MaDV
                                                 select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, chidinh.Status, chidinh.IDCD, dv.IDNhom, dv.IsAutoExecute, dv.SoPhutThucHien }).ToList();
                                        if (d.Count > 0)
                                        {
                                            int dem = 0; // xác định có thêm vào bảng CLSct không
                                            foreach (var item in d)
                                            {
                                                CLSct themmoiCL = new CLSct();
                                                themmoiCL.IDCD = item.IDCD;
                                                themmoiCL.MaDVct = item.MaDVct;
                                                themmoiCL.Status = 0;
                                                //themmoiCL.STTHT=
                                                _data.CLScts.Add(themmoiCL);
                                                _data.SaveChanges();
                                                dem++;
                                            }

                                            //dung280516
                                            if (dem > 0)
                                            {
                                                BenhNhan sua = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                                                if (sua != null)
                                                {
                                                    sua.Status = 4;
                                                }
                                                _data.SaveChanges();
                                            }
                                            var dsChiDinh = d.Select(o => new { IDNhom = o.IDNhom, IDCD = o.IDCD, o.SoPhutThucHien, o.IsAutoExecute }).Distinct().ToList();
                                            foreach (var item in dsChiDinh)
                                            {
                                                if ((DungChung.Bien.MaBV == "14017" || (DungChung.Bien.MaBV == "24297" && item.IsAutoExecute == true)) && item.IDNhom == 8)
                                                    ThucHienTTPT(_data, _Mabn, item.IDCD, item.SoPhutThucHien != null ? dtNgayCD.DateTime.AddMinutes(item.SoPhutThucHien ?? 0) : dtNgayCD.DateTime.AddMinutes(5), Convert.ToInt32(lupKhoaphong.EditValue), lupNguoiKhamkb.EditValue.ToString());
                                            }
                                            //
                                        }
                                    }
                                    //if (trangthai == 1 || trangthai == 2)
                                    //{
                                    //    trangthai = 0;
                                    //    FRM_chidinh_Load(sender, e);
                                    //    colYLenh.OptionsColumn.AllowEdit = false;
                                    //    colYLenh.OptionsColumn.ReadOnly = true;
                                    //    GrcNhomDV.Enabled = true;
                                    //    enableControl(true);
                                    //}
                                    #endregion

                                    var _lttheodv = _listDichVu.Where(p => p.Chon == true).Where(p => p.IdNhom != 1).ToList();

                                    foreach (var a in _lttheodv)
                                    {
                                        //var kpth = _listDichVu.Where(p => p.Chon == true).Where(p => p.nhom == a.nhom).Select(p => p.MaKPTH).FirstOrDefault();
                                        int idCLSnew = 0;
                                        CL themmoi = new CL();
                                        themmoi.MaBNhan = _Mabn;
                                        themmoi.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                        themmoi.MaKP = makpcd;
                                        themmoi.MaKPth = a.MaKPTH != null ? a.MaKPTH : 0; //LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                        if (DungChung.Bien.MaBV == "24272" && _lttheodv.First().IsAutoExecute == true)
                                        {
                                            themmoi.MaCBth = lupNguoiKhamkb.EditValue.ToString();
                                            themmoi.NgayTH = dtNgayCD.DateTime;
                                            themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                            themmoi.Status = 1;
                                            if (_lttheodv.FirstOrDefault().SoPhutThucHien != null)
                                            {
                                                if (_lttheodv.FirstOrDefault().SoPhutThucHien != 0)
                                                {
                                                    themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(Convert.ToDouble(_lttheodv.FirstOrDefault().SoPhutThucHien.Value));
                                                }
                                                else
                                                {
                                                    themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                                }
                                            }
                                        }
                                        themmoi.NgayThang = dtNgayCD.DateTime;
                                        themmoi.CapCuu = ckCapCuu.Checked;
                                        if (_idDB > 0)
                                            themmoi.IDDienBien = _idDB;
                                        if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30303")
                                        {
                                            if (chandoan != null)
                                            {

                                                themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                if (DungChung.Bien.MaBV == "20001")
                                                {
                                                    themmoi.MaYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[0];
                                                    themmoi.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[1];
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (_kp.PLoai != "Hành chính")
                                            {
                                                if (_kp.PLoai == "Phòng khám" || _kp.PLoai == "Lâm sàng")
                                                {
                                                    if (_kp.QuanLy == 1)
                                                    {
                                                        if (chandoan != null)
                                                        {
                                                            themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                            themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                        }
                                                        else
                                                        {
                                                            var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                            if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                            {
                                                                themmoi.ChanDoan = _lTTBN.First().TChung;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (chandoan != null)
                                                        {
                                                            themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                            themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                {
                                                    themmoi.ChanDoan = _lTTBN.First().TChung;
                                                }
                                            }
                                        }
                                        //bool XetNgiem = false;
                                        //if (lupTN.Text.Contains("XN"))
                                        //{
                                        //    themmoi.Code = txtCode.Text;
                                        //    XetNgiem = true;
                                        //}
                                        //else
                                        themmoi.Code = "CDHA";
                                        bool XetNgiem = false;
                                        int _sott = 0;
                                        if (DungChung.Bien.MaBV != "30007")
                                        {
                                            int makp = LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                            if (XetNgiem)
                                            {
                                                int ktsoTT = _data.SoTTs.Where(p => p.NgayDK == dtNgayCD.DateTime.Date && p.MaKP == makp).Select(p => p.SoTT1).FirstOrDefault();
                                                if (ktsoTT > 0)
                                                {
                                                    var ktstatu = _data.CLS.Where(p => p.MaBNhan == _Mabn && p.Status == 0 && p.MaKPth == makp).ToList();
                                                    if (ktstatu.Count > 0)
                                                    {
                                                        _sott = ktsoTT;
                                                    }
                                                    else
                                                        _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                                }
                                                else
                                                    _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                            }
                                            else
                                                _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                            themmoi.STT = _sott;
                                        }
                                        _data.CLS.Add(themmoi);
                                        _data.SaveChanges();
                                        idCLSnew = themmoi.IdCLS;

                                        idCLSs.Add(idCLSnew); // Lưu idcls gửi pacs

                                        _lIDCLS_DB.Add(themmoi.IdCLS);
                                        // Thêm vào bảng ChiDinh
                                        _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                        //set sott
                                        if (DungChung.Bien.MaBV != "30007")
                                        {
                                            FormNhap.frmHSBNNhapMoi._setSoTT(_data, dtNgayCD.DateTime, LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue), _sott, _Mabn);
                                        }

                                        foreach (var item in _listDichVu.Where(p => p.Chon == true && p.madv == a.madv))
                                        {
                                            // kiểm tra chỉ định trong ngày
                                            bool _ThemMoi = true;
                                            var kt = from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                                                     join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                                     where cd.MaDV == item.madv
                                                     where cls.NgayThang != null && cls.NgayThang.Value.Day == dtNgayCD.DateTime.Day
                                                     select cd.MaDV;
                                            if (kt.Count() > 0)
                                            {
                                                string thongbao = "Dịch vụ" + _data.DichVus.Where(p => p.MaDV == kt.FirstOrDefault()).First().TenDV + " đã được chỉ định trong ngày, bạn vẫn muốn chỉ định thêm?";
                                                DialogResult rs = MessageBox.Show(thongbao, "Thông báo", MessageBoxButtons.OKCancel);
                                                if (rs == DialogResult.Cancel)
                                                    _ThemMoi = false;
                                            }
                                            // kiểm tra đơn giá
                                            if (item.DonGia <= 0)
                                            {
                                                string thongbao1 = "Dịch vụ" + _lDichVu.Where(p => p.madv == item.madv).Select(p => p.tendv).FirstOrDefault() + " chưa có đơn giá, vui lòng kiểm tra lại!";
                                                MessageBox.Show(thongbao1);
                                                _ThemMoi = false;
                                            }
                                            if (_ThemMoi)
                                            {
                                                ChiDinh themmoiCD = new ChiDinh();
                                                themmoiCD.IdCLS = idCLSnew;
                                                themmoiCD.MaDV = item.madv;
                                                themmoiCD.Status = 0;
                                                var dvui = _data.DichVus.Where(x => x.MaDV == item.madv).FirstOrDefault();
                                                var bnnn = _data.BenhNhans.Where(x => x.MaBNhan == _Mabn).FirstOrDefault();
                                                if (DungChung.Bien.MaBV == "24272")
                                                {
                                                    if (bnnn.DTuong == "Dịch vụ" || bnnn.IDDTBN == 2)
                                                    {
                                                        if (DungChung.Ham._checkTamThu(_data, _Mabn, idCLSnew))
                                                        {
                                                            if (dvui.IsAutoExecute == true)
                                                            {
                                                                themmoiCD.Status = 1;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (dvui.IsAutoExecute == true)
                                                        {
                                                            themmoiCD.Status = 1;
                                                        }
                                                    }

                                                }
                                                themmoiCD.TenDungCu = item.TenDungCu;
                                                themmoiCD.NDVaoVien = item.NDVaoVien;
                                                themmoiCD.DonGia = DungChung.Ham._getGiaDM(_data, item.madv, item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1), _Mabn, dtNgayCD.DateTime);//item.DonGia;
                                                if (DTBN == 1)
                                                {
                                                    themmoiCD.TrongBH = item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1);
                                                }
                                                else if (DungChung.Bien.MaBV == "20001" && benhnhan.DTuong == "Khám miễn phí")
                                                {
                                                    themmoiCD.TrongBH = 2;
                                                }
                                                else
                                                {
                                                    themmoiCD.TrongBH = 0;
                                                }
                                                themmoiCD.XHH = 0;
                                                themmoiCD.LoaiDV = Convert.ToInt32(item.TheoYC);
                                                for (int i = 0; i < grvCLS.RowCount; i++)
                                                {
                                                    if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh) != null)
                                                    {
                                                        int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                        string ylenh = grvCLS.GetRowCellValue(i, colYLenh).ToString();
                                                        if (item.madv == madv)
                                                        {
                                                            themmoiCD.ChiDinh1 = ylenh;
                                                            //break;
                                                        }

                                                    }
                                                    if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh2) != null)
                                                    {
                                                        int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                        string ylenh2 = grvCLS.GetRowCellValue(i, colYLenh2).ToString();
                                                        if (item.madv == madv)
                                                        {
                                                            themmoiCD.YLenh2 = ylenh2;
                                                            //break;
                                                        }

                                                    }
                                                    if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colXHH) != null)
                                                    {
                                                        int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                        bool xhh = Convert.ToBoolean(grvCLS.GetRowCellValue(i, colXHH).ToString());
                                                        if (item.madv == madv)
                                                        {
                                                            themmoiCD.XHH = (xhh == true) ? 1 : 0;
                                                            // break;
                                                        }
                                                    }

                                                }
                                                if (DungChung.Bien.MaBV == "24009" && cdgoidv)
                                                {
                                                    themmoiCD.IDGoi = idgoidv;
                                                }
                                                _data.ChiDinhs.Add(themmoiCD);
                                                int cd = _data.SaveChanges();
                                                if (cd > 0)
                                                {
                                                    if (DungChung.Bien.MaBV == "01071" && (a.IdNhom == 2 || a.IdNhom == 3 || lupTN.Text.ToUpper().Contains("NỘI SOI")))
                                                    {
                                                        var DV = _data.DichVus.Where(p => p.MaDV == themmoiCD.MaDV).FirstOrDefault();
                                                        var CB = _data.CanBoes.Where(p => p.MaCB == themmoi.MaCB).FirstOrDefault();
                                                        ChiDinhToPacs guiDL = new ChiDinhToPacs();
                                                        guiDL.MaChiDinh = themmoiCD.IDCD;
                                                        guiDL.ChanDoan = (chandoan != null) ? chandoan.ChanDoan : "";
                                                        guiDL.DiaChi = bn.DChi;
                                                        guiDL.GioiTinh = bn.GTinh == 1 ? "M" : "WM";
                                                        guiDL.MaBacSiChiDinh = themmoi.MaCB;
                                                        guiDL.MaBenhNhan = themmoi.MaBNhan;
                                                        guiDL.MaDichVu = themmoiCD.MaDV;
                                                        guiDL.NgaySinh = bn.NgaySinh;
                                                        guiDL.ThangSinh = bn.ThangSinh;
                                                        guiDL.NamSinh = bn.NamSinh;
                                                        guiDL.NhomDichVu = item.nhom;
                                                        guiDL.NoiChiDinh = _kp.TenKP;
                                                        guiDL.SDT = "";
                                                        guiDL.TenBacSiChiDinh = CB.TenCB;
                                                        guiDL.TenBenhNhan = bn.TenBNhan;
                                                        guiDL.TenDichVu = DV.TenDV;
                                                        guiDL.ThoiGianChiDinh = themmoi.NgayThang;
                                                        guiDL.TrangThai = "NW";
                                                        _ldata.Add(guiDL);

                                                    }
                                                }
                                            }

                                        }

                                        // Thêm vào bảng CLSct
                                        var d = (from cls in _data.CLS.Where(p => p.IdCLS == idCLSnew)
                                                 join chidinh in _data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                                 join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                                                 join dvct in _data.DichVucts on chidinh.MaDV equals dvct.MaDV
                                                 select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, chidinh.Status, chidinh.IDCD, dv.IDNhom, dv.IsAutoExecute, dv.SoPhutThucHien }).ToList();
                                        if (d.Count > 0)
                                        {
                                            int dem = 0; // xác định có thêm vào bảng CLSct không
                                            foreach (var item in d)
                                            {
                                                CLSct themmoiCL = new CLSct();
                                                themmoiCL.IDCD = item.IDCD;
                                                themmoiCL.MaDVct = item.MaDVct;
                                                themmoiCL.Status = 0;
                                                //themmoiCL.STTHT=
                                                _data.CLScts.Add(themmoiCL);
                                                _data.SaveChanges();
                                                dem++;
                                                if (DungChung.Bien.MaBV == "24297")
                                                {
                                                    if (item.TenDVct.ToUpper().Contains("LƯU HUYẾT NÃO") && item.IsAutoExecute == true)
                                                    {
                                                        thuchienLHN(idCLSnew, item.IDCD);
                                                    }
                                                }
                                                else if (item.TenDVct.ToUpper().Contains("LƯU HUYẾT NÃO"))
                                                {
                                                    thuchienLHN(idCLSnew, item.IDCD);
                                                }
                                            }

                                            //dung280516
                                            if (dem > 0)
                                            {
                                                BenhNhan sua = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                                                if (sua != null)
                                                {
                                                    sua.Status = 4;
                                                }
                                                _data.SaveChanges();
                                            }
                                            var dsChiDinh = d.Select(o => new { IDNhom = o.IDNhom, IDCD = o.IDCD, o.SoPhutThucHien, o.IsAutoExecute }).Distinct().ToList();
                                            foreach (var item in dsChiDinh)
                                            {
                                                if ((DungChung.Bien.MaBV == "14017" || (DungChung.Bien.MaBV == "24297" && item.IsAutoExecute == true)) && item.IDNhom == 8)
                                                    ThucHienTTPT(_data, _Mabn, item.IDCD, item.SoPhutThucHien != null ? dtNgayCD.DateTime.AddMinutes(item.SoPhutThucHien ?? 0) : dtNgayCD.DateTime.AddMinutes(5), Convert.ToInt32(lupKhoaphong.EditValue), lupNguoiKhamkb.EditValue.ToString());
                                            }
                                            //
                                        }
                                    }
                                    if (trangthai == 1 || trangthai == 2)
                                    {
                                        FRM_chidinh_Load(sender, e);
                                        trangthai = 0;
                                        LupKpThuchien.Visible = true;
                                        labelControl2.Visible = true;
                                        colYLenh.OptionsColumn.AllowEdit = false;
                                        colYLenh.OptionsColumn.ReadOnly = true;
                                        GrcNhomDV.Enabled = true;
                                        enableControl(true);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    var _ltn = (from a in _listDichVu.Where(p => p.Chon == true)
                                                group a by new { a.IdNhom } into kq
                                                select new { kq.Key.IdNhom }).ToList();
                                    var _ltn123 = _listDichVu.Where(p => p.Chon == true);
                                    if (_ltn.Count == 1)
                                    {
                                        //Thêm vào bảng CLS
                                        if (_ltn.First().IdNhom == 1)
                                        {
                                            int idCLSnew = 0;
                                            CL themmoi = new CL();
                                            themmoi.MaBNhan = _Mabn;
                                            themmoi.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                            themmoi.MaKP = makpcd;
                                            themmoi.MaKPth = LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                            var bnhann = _data.BenhNhans.Where(x => x.MaBNhan == _Mabn).FirstOrDefault();
                                            int idcddd = _ltn123.First().idcd;
                                            if (DungChung.Bien.MaBV == "24272")
                                            {
                                                if (bnhann.DTuong.ToLower().Equals("Dịch vụ".ToLower()))
                                                {
                                                    if (DungChung.Ham.Check_DuyetTamThu(idcddd))
                                                    {
                                                        if (_ltn123.First().IsAutoExecute == true)
                                                        {
                                                            themmoi.MaCBth = lupNguoiKhamkb.EditValue.ToString();
                                                            themmoi.NgayTH = dtNgayCD.DateTime;
                                                            themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                                            themmoi.Status = 1;
                                                            if (_ltn123.First().SoPhutThucHien != null)
                                                            {
                                                                if (_ltn123.First().SoPhutThucHien != 0)
                                                                {
                                                                    themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(Convert.ToDouble(_ltn123.First().SoPhutThucHien.Value));
                                                                }
                                                                else
                                                                {
                                                                    themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (_ltn123.First().IsAutoExecute == true)
                                                    {
                                                        themmoi.MaCBth = lupNguoiKhamkb.EditValue.ToString();
                                                        themmoi.NgayTH = dtNgayCD.DateTime;
                                                        themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                                        themmoi.Status = 1;
                                                        if (_ltn123.First().SoPhutThucHien != null)
                                                        {
                                                            if (_ltn123.First().SoPhutThucHien != 0)
                                                            {
                                                                themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(Convert.ToDouble(_ltn123.First().SoPhutThucHien.Value));
                                                            }
                                                            else
                                                            {
                                                                themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                                            }
                                                        }
                                                    }
                                                }
                                                //
                                            }
                                            themmoi.NgayThang = dtNgayCD.DateTime;
                                            themmoi.CapCuu = ckCapCuu.Checked;
                                            if (_idDB > 0)
                                            {
                                                themmoi.IDDienBien = _idDB;
                                            }
                                            if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30303")
                                            {
                                                if (chandoan != null)
                                                {

                                                    themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                    themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                    if (DungChung.Bien.MaBV == "20001")
                                                    {
                                                        themmoi.MaYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[0];
                                                        themmoi.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[1];
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (_kp.PLoai != "Hành chính")
                                                {
                                                    if (_kp.PLoai == "Phòng khám" || _kp.PLoai == "Lâm sàng")
                                                    {
                                                        if (_kp.QuanLy == 1)
                                                        {
                                                            if (chandoan != null)
                                                            {
                                                                themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                                themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                            }
                                                            else
                                                            {
                                                                var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                                if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                                {
                                                                    themmoi.ChanDoan = _lTTBN.First().TChung;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (chandoan != null)
                                                            {
                                                                themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                                themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                    if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                    {
                                                        themmoi.ChanDoan = _lTTBN.First().TChung;
                                                    }
                                                }
                                            }
                                            bool XetNgiem = false;
                                            if (lupTN.Text.Contains("XN") || lupTN.Text.Contains("Trắc nghiệm tâm lý"))
                                            {
                                                themmoi.Code = txtCode.Text;
                                                XetNgiem = true;
                                            }
                                            else
                                                themmoi.Code = "CDHA";
                                            int _sott = 0;
                                            if (DungChung.Bien.MaBV != "30007")
                                            {
                                                int makp = LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                                if (XetNgiem)
                                                {
                                                    int ktsoTT = _data.SoTTs.Where(p => p.NgayDK == dtNgayCD.DateTime.Date && p.MaKP == makp).Select(p => p.SoTT1).FirstOrDefault();
                                                    if (ktsoTT > 0)
                                                    {
                                                        var ktstatu = _data.CLS.Where(p => p.MaBNhan == _Mabn && p.Status == 0 && p.MaKPth == makp).ToList();
                                                        if (ktstatu.Count > 0)
                                                        {
                                                            _sott = ktsoTT;
                                                        }
                                                        else
                                                            _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                                    }
                                                    else
                                                        _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                                }
                                                else
                                                    _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                                themmoi.STT = _sott;
                                            }
                                            _data.CLS.Add(themmoi);
                                            _data.SaveChanges();
                                            idCLSnew = themmoi.IdCLS;

                                            idCLSs.Add(idCLSnew); // Lưu IdCL gửi pacs

                                            _idCLS = themmoi.IdCLS;
                                            _lIDCLS_DB.Add(themmoi.IdCLS);
                                            // Thêm vào bảng ChiDinh
                                            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                            //set sott
                                            if (DungChung.Bien.MaBV != "30007")
                                            {
                                                FormNhap.frmHSBNNhapMoi._setSoTT(_data, dtNgayCD.DateTime, LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue), _sott, _Mabn);
                                            }

                                            foreach (var item in _listDichVu.Where(p => p.Chon == true))
                                            {

                                                // kiểm tra chỉ định trong ngày
                                                bool _ThemMoi = true;
                                                var kt = from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                                                         join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                                         where cd.MaDV == item.madv
                                                         where cls.NgayThang != null && cls.NgayThang.Value.Day == dtNgayCD.DateTime.Day
                                                         select cd.MaDV;
                                                if (kt.Count() > 0)
                                                {
                                                    string thongbao = "Dịch vụ " + _data.DichVus.Where(p => p.MaDV == kt.FirstOrDefault()).First().TenDV + " đã được chỉ định trong ngày, bạn vẫn muốn chỉ định thêm?";
                                                    DialogResult rs = MessageBox.Show(thongbao, "Thông báo", MessageBoxButtons.OKCancel);
                                                    if (rs == DialogResult.Cancel)
                                                        _ThemMoi = false;
                                                }
                                                // kiểm tra đơn giá
                                                if (item.DonGia <= 0)
                                                {
                                                    string thongbao1 = "Dịch vụ" + _lDichVu.Where(p => p.madv == item.madv).Select(p => p.tendv).FirstOrDefault() + " chưa có đơn giá, vui lòng kiểm tra lại!";
                                                    MessageBox.Show(thongbao1);
                                                    _ThemMoi = false;
                                                }
                                                if (_ThemMoi)
                                                {
                                                    ChiDinh themmoiCD = new ChiDinh();
                                                    themmoiCD.IdCLS = idCLSnew;
                                                    themmoiCD.MaDV = item.madv;
                                                    themmoiCD.Status = 0;
                                                    themmoiCD.TenDungCu = item.TenDungCu;
                                                    themmoiCD.NDVaoVien = item.NDVaoVien;
                                                    themmoiCD.DonGia = DungChung.Ham._getGiaDM(_data, item.madv, item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1), _Mabn, dtNgayCD.DateTime);//item.DonGia;
                                                    if (DTBN == 1)
                                                    {
                                                        themmoiCD.TrongBH = item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1);
                                                    }
                                                    else if (DungChung.Bien.MaBV == "20001" && benhnhan.DTuong == "Khám miễn phí")
                                                    {
                                                        themmoiCD.TrongBH = 2;
                                                    }
                                                    else
                                                    {
                                                        themmoiCD.TrongBH = 0;
                                                    }
                                                    themmoiCD.XHH = 0;
                                                    themmoiCD.LoaiDV = Convert.ToInt32(item.TheoYC);
                                                    for (int i = 0; i < grvCLS.RowCount; i++)
                                                    {
                                                        if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh) != null)
                                                        {
                                                            int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                            string ylenh = grvCLS.GetRowCellValue(i, colYLenh).ToString();
                                                            if (item.madv == madv)
                                                            {
                                                                themmoiCD.ChiDinh1 = ylenh;
                                                                //break;
                                                            }

                                                        }
                                                        if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh2) != null)
                                                        {
                                                            int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                            string ylenh2 = grvCLS.GetRowCellValue(i, colYLenh2).ToString();
                                                            if (item.madv == madv)
                                                            {
                                                                themmoiCD.YLenh2 = ylenh2;
                                                                //break;
                                                            }

                                                        }
                                                        if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colXHH) != null)
                                                        {
                                                            int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                            bool xhh = Convert.ToBoolean(grvCLS.GetRowCellValue(i, colXHH).ToString());
                                                            if (item.madv == madv)
                                                            {
                                                                themmoiCD.XHH = (xhh == true) ? 1 : 0;
                                                                // break;
                                                            }
                                                        }
                                                    }
                                                    if (DungChung.Bien.MaBV == "24009" && cdgoidv)
                                                    {
                                                        themmoiCD.IDGoi = idgoidv;
                                                    }
                                                    _data.ChiDinhs.Add(themmoiCD);
                                                    int cd = _data.SaveChanges();
                                                    if (cd > 0)
                                                    {
                                                        if (DungChung.Bien.MaBV == "01071123" && (_ltn.First().IdNhom == 2 || _ltn.First().IdNhom == 3 || lupTN.Text.ToUpper().Contains("NỘI SOI")))
                                                        {
                                                            var DV = _data.DichVus.Where(p => p.MaDV == themmoiCD.MaDV).FirstOrDefault();
                                                            var CB = _data.CanBoes.Where(p => p.MaCB == themmoi.MaCB).FirstOrDefault();
                                                            ChiDinhToPacs guiDL = new ChiDinhToPacs();
                                                            guiDL.MaChiDinh = themmoiCD.IDCD;
                                                            guiDL.ChanDoan = (chandoan != null) ? chandoan.ChanDoan : "";
                                                            guiDL.DiaChi = bn.DChi;
                                                            guiDL.GioiTinh = bn.GTinh == 1 ? "M" : "WM";
                                                            guiDL.MaBacSiChiDinh = themmoi.MaCB;
                                                            guiDL.MaBenhNhan = themmoi.MaBNhan;
                                                            guiDL.MaDichVu = themmoiCD.MaDV;
                                                            guiDL.NgaySinh = bn.NgaySinh;
                                                            guiDL.ThangSinh = bn.ThangSinh;
                                                            guiDL.NamSinh = bn.NamSinh;
                                                            guiDL.NhomDichVu = item.nhom;
                                                            guiDL.NoiChiDinh = _kp.TenKP;
                                                            guiDL.SDT = "";
                                                            guiDL.TenBacSiChiDinh = CB.TenCB;
                                                            guiDL.TenBenhNhan = bn.TenBNhan;
                                                            guiDL.TenDichVu = DV.TenDV;
                                                            guiDL.ThoiGianChiDinh = themmoi.NgayThang;
                                                            guiDL.TrangThai = "NW";
                                                            _ldata.Add(guiDL);

                                                        }
                                                    }
                                                    _idCCD = themmoiCD.IDCD;
                                                }

                                            }

                                            // Thêm vào bảng CLSct
                                            var d = (from cls in _data.CLS.Where(p => p.IdCLS == idCLSnew)
                                                     join chidinh in _data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                                     join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                                                     join dvct in _data.DichVucts on dv.MaDV equals dvct.MaDV
                                                     select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, chidinh.Status, chidinh.IDCD, dvct.STT, dv.MaDV, dv.IDNhom, dv.IsAutoExecute, dv.SoPhutThucHien }).ToList();
                                            if (d.Count > 0)
                                            {
                                                int dem = 0; // xác định có thêm vào bảng CLSct không
                                                foreach (var item in d)
                                                {
                                                    CLSct themmoiCL = new CLSct();
                                                    themmoiCL.IDCD = item.IDCD;
                                                    themmoiCL.MaDVct = item.MaDVct;
                                                    themmoiCL.Status = 0;
                                                    themmoiCL.Is_Execute = true;
                                                    themmoiCL.STTHT = item.STT;
                                                    _data.CLScts.Add(themmoiCL);
                                                    _data.SaveChanges();
                                                    dem++;
                                                }

                                                //dung280516
                                                if (dem > 0)
                                                {
                                                    BenhNhan sua = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                                                    if (sua != null)
                                                    {
                                                        sua.Status = 4;
                                                    }
                                                    _data.SaveChanges();
                                                }

                                                var dsChiDinh = d.Select(o => new { IDNhom = o.IDNhom, IDCD = o.IDCD, o.SoPhutThucHien, o.IsAutoExecute }).Distinct().ToList();
                                                foreach (var item in dsChiDinh)
                                                {
                                                    if ((DungChung.Bien.MaBV == "14017" || (DungChung.Bien.MaBV == "24297" && item.IsAutoExecute == true)) && item.IDNhom == 8)
                                                        ThucHienTTPT(_data, _Mabn, item.IDCD, item.SoPhutThucHien != null ? dtNgayCD.DateTime.AddMinutes(item.SoPhutThucHien ?? 0) : dtNgayCD.DateTime.AddMinutes(5), Convert.ToInt32(lupKhoaphong.EditValue), lupNguoiKhamkb.EditValue.ToString());
                                                }
                                            }

                                            if (trangthai == 1 || trangthai == 2)
                                            {
                                                FRM_chidinh_Load(sender, e);
                                                trangthai = 0;
                                                colYLenh.OptionsColumn.AllowEdit = false;
                                                colYLenh.OptionsColumn.ReadOnly = true;
                                                GrcNhomDV.Enabled = true;
                                                enableControl(true);
                                                btnSua.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            foreach (var a in _listDichVu.Where(p => p.Chon == true))
                                            {
                                                int idCLSnew = 0;
                                                CL themmoi = new CL();
                                                themmoi.MaBNhan = _Mabn;
                                                themmoi.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                                themmoi.MaKP = makpcd;
                                                themmoi.MaKPth = LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                                var bnhann = _data.BenhNhans.Where(x => x.MaBNhan == _Mabn).FirstOrDefault();
                                                if (DungChung.Bien.MaBV == "24272")
                                                {
                                                    if (bnhann.DTuong.ToLower().Equals("Dịch vụ".ToLower()))
                                                    {
                                                        if (DungChung.Ham.Check_DuyetTamThu(a.idcd))
                                                        {
                                                            if (a.IsAutoExecute == true)
                                                            {
                                                                themmoi.MaCBth = lupNguoiKhamkb.EditValue.ToString();
                                                                themmoi.NgayTH = dtNgayCD.DateTime;
                                                                themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                                                themmoi.Status = 1;
                                                                if (a.SoPhutThucHien != null)
                                                                {
                                                                    if (a.SoPhutThucHien != 0)
                                                                    {
                                                                        themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(Convert.ToDouble(a.SoPhutThucHien.Value));
                                                                    }
                                                                    else
                                                                    {
                                                                        themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (a.IsAutoExecute == true)
                                                        {
                                                            themmoi.MaCBth = lupNguoiKhamkb.EditValue.ToString();
                                                            themmoi.NgayTH = dtNgayCD.DateTime;
                                                            themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                                            themmoi.Status = 1;
                                                            if (a.SoPhutThucHien != null)
                                                            {
                                                                if (a.SoPhutThucHien != 0)
                                                                {
                                                                    themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(Convert.ToDouble(a.SoPhutThucHien.Value));
                                                                }
                                                                else
                                                                {
                                                                    themmoi.NgayKQ = dtNgayCD.DateTime.AddMinutes(5);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //
                                                }
                                                themmoi.NgayThang = dtNgayCD.DateTime;
                                                themmoi.CapCuu = ckCapCuu.Checked;
                                                if (_idDB > 0)
                                                {
                                                    themmoi.IDDienBien = _idDB;
                                                }
                                                if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "30303")
                                                {
                                                    if (chandoan != null)
                                                    {

                                                        themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                        themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                        if (DungChung.Bien.MaBV == "20001")
                                                        {
                                                            themmoi.MaYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[0];
                                                            themmoi.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(DungChung.Ham.FreshString(themmoi.MaICD), _licd10)[1];
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (_kp.PLoai != "Hành chính")
                                                    {
                                                        if (_kp.PLoai == "Phòng khám" || _kp.PLoai == "Lâm sàng")
                                                        {
                                                            if (_kp.QuanLy == 1)
                                                            {
                                                                if (chandoan != null)
                                                                {
                                                                    themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                                    themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                                }
                                                                else
                                                                {
                                                                    var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                                    if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                                    {
                                                                        themmoi.ChanDoan = _lTTBN.First().TChung;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (chandoan != null)
                                                                {
                                                                    themmoi.ChanDoan = (arrrCDBD != null && arrrCDBD != "") ? (arrrCDBD + ";" + DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac)) : DungChung.Ham.GetICDstr(chandoan.ChanDoan + ";" + chandoan.BenhKhac);
                                                                    themmoi.MaICD = DungChung.Ham.GetICDstr(chandoan.MaICD + ";" + chandoan.MaICD2);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var _lTTBN = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).ToList();
                                                        if (_lTTBN.Count > 0 && _lTTBN.First().TChung != null)
                                                        {
                                                            themmoi.ChanDoan = _lTTBN.First().TChung;
                                                        }
                                                    }
                                                }
                                                bool XetNgiem = false;
                                                if (lupTN.Text.Contains("XN"))
                                                {
                                                    themmoi.Code = txtCode.Text;
                                                    XetNgiem = true;
                                                }
                                                else
                                                    themmoi.Code = "CDHA";
                                                int _sott = 0;
                                                if (DungChung.Bien.MaBV != "30007")
                                                {
                                                    int makp = LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                                                    if (XetNgiem)
                                                    {
                                                        int ktsoTT = _data.SoTTs.Where(p => p.NgayDK == dtNgayCD.DateTime.Date && p.MaKP == makp).Select(p => p.SoTT1).FirstOrDefault();
                                                        if (ktsoTT > 0)
                                                        {
                                                            var ktstatu = _data.CLS.Where(p => p.MaBNhan == _Mabn && p.Status == 0 && p.MaKPth == makp).ToList();
                                                            if (ktstatu.Count > 0)
                                                            {
                                                                _sott = ktsoTT;
                                                            }
                                                            else
                                                                _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                                        }
                                                        else
                                                            _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                                    }
                                                    else
                                                        _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, makp);
                                                    themmoi.STT = _sott;
                                                }
                                                _data.CLS.Add(themmoi);
                                                _data.SaveChanges();
                                                idCLSnew = themmoi.IdCLS;
                                                idCLSs.Add(idCLSnew); // Lưu IdCL gửi pacs
                                                _idCLS = themmoi.IdCLS;
                                                _lIDCLS_DB.Add(themmoi.IdCLS);
                                                // Thêm vào bảng ChiDinh
                                                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                                //set sott
                                                if (DungChung.Bien.MaBV != "30007")
                                                {
                                                    FormNhap.frmHSBNNhapMoi._setSoTT(_data, dtNgayCD.DateTime, LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue), _sott, _Mabn);
                                                }

                                                foreach (var item in _listDichVu.Where(p => p.Chon == true && p.madv == a.madv))
                                                {

                                                    // kiểm tra chỉ định trong ngày
                                                    bool _ThemMoi = true;
                                                    var kt = from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn)
                                                             join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                                             where cd.MaDV == item.madv
                                                             where cls.NgayThang != null && cls.NgayThang.Value.Day == dtNgayCD.DateTime.Day
                                                             select cd.MaDV;
                                                    if (kt.Count() > 0)
                                                    {
                                                        string thongbao = "Dịch vụ" + _data.DichVus.Where(p => p.MaDV == kt.FirstOrDefault()).First().TenDV + " đã được chỉ định trong ngày, bạn vẫn muốn chỉ định thêm?";
                                                        DialogResult rs = MessageBox.Show(thongbao, "Thông báo", MessageBoxButtons.OKCancel);
                                                        if (rs == DialogResult.Cancel)
                                                            _ThemMoi = false;
                                                    }
                                                    // kiểm tra đơn giá
                                                    if (item.DonGia <= 0)
                                                    {
                                                        string thongbao1 = "Dịch vụ" + _lDichVu.Where(p => p.madv == item.madv).Select(p => p.tendv).FirstOrDefault() + " chưa có đơn giá, vui lòng kiểm tra lại!";
                                                        MessageBox.Show(thongbao1);
                                                        _ThemMoi = false;
                                                    }
                                                    if (_ThemMoi)
                                                    {
                                                        ChiDinh themmoiCD = new ChiDinh();
                                                        themmoiCD.IdCLS = idCLSnew;
                                                        themmoiCD.MaDV = item.madv;
                                                        themmoiCD.Status = 0;
                                                        var dvui = _data.DichVus.Where(x => x.MaDV == item.madv).FirstOrDefault();
                                                        var bnnn = _data.BenhNhans.Where(x => x.MaBNhan == _Mabn).FirstOrDefault();
                                                        if (DungChung.Bien.MaBV == "24272")
                                                        {
                                                            if (bnnn.DTuong == "Dịch vụ" || bnnn.IDDTBN == 2)
                                                            {
                                                                if (DungChung.Ham._checkTamThu(_data, _Mabn, idCLSnew))
                                                                {
                                                                    if (dvui.IsAutoExecute == true)
                                                                    {
                                                                        themmoiCD.Status = 1;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (dvui.IsAutoExecute == true)
                                                                {
                                                                    themmoiCD.Status = 1;
                                                                }
                                                            }

                                                        }
                                                        themmoiCD.TenDungCu = item.TenDungCu;
                                                        themmoiCD.NDVaoVien = item.NDVaoVien;
                                                        themmoiCD.DonGia = DungChung.Ham._getGiaDM(_data, item.madv, item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1), _Mabn, dtNgayCD.DateTime);//item.DonGia;
                                                        if (DTBN == 1)
                                                        {
                                                            themmoiCD.TrongBH = item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1);
                                                        }
                                                        else
                                                        {
                                                            themmoiCD.TrongBH = 0;
                                                        }
                                                        themmoiCD.XHH = 0;
                                                        themmoiCD.LoaiDV = Convert.ToInt32(item.TheoYC);
                                                        for (int i = 0; i < grvCLS.RowCount; i++)
                                                        {
                                                            if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh) != null)
                                                            {
                                                                int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                                string ylenh = grvCLS.GetRowCellValue(i, colYLenh).ToString();
                                                                if (item.madv == madv)
                                                                {
                                                                    themmoiCD.ChiDinh1 = ylenh;
                                                                    //break;
                                                                }

                                                            }
                                                            if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh2) != null)
                                                            {
                                                                int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                                string ylenh2 = grvCLS.GetRowCellValue(i, colYLenh2).ToString();
                                                                if (item.madv == madv)
                                                                {
                                                                    themmoiCD.YLenh2 = ylenh2;
                                                                    //break;
                                                                }

                                                            }
                                                            if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colXHH) != null)
                                                            {
                                                                int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                                                bool xhh = Convert.ToBoolean(grvCLS.GetRowCellValue(i, colXHH).ToString());
                                                                if (item.madv == madv)
                                                                {
                                                                    themmoiCD.XHH = (xhh == true) ? 1 : 0;
                                                                    // break;
                                                                }
                                                            }

                                                        }
                                                        if (DungChung.Bien.MaBV == "24009" && cdgoidv)
                                                        {
                                                            themmoiCD.IDGoi = idgoidv;
                                                        }
                                                        _data.ChiDinhs.Add(themmoiCD);
                                                        int cd = _data.SaveChanges();
                                                        if (cd > 0)
                                                        {
                                                            if (DungChung.Bien.MaBV == "01071" && (a.IdNhom == 2 || a.IdNhom == 3 || lupTN.Text.ToUpper().Contains("NỘI SOI")))
                                                            {
                                                                var DV = _data.DichVus.Where(p => p.MaDV == themmoiCD.MaDV).FirstOrDefault();
                                                                var CB = _data.CanBoes.Where(p => p.MaCB == themmoi.MaCB).FirstOrDefault();
                                                                ChiDinhToPacs guiDL = new ChiDinhToPacs();
                                                                guiDL.MaChiDinh = themmoiCD.IDCD;
                                                                //guiDL.ChanDoan = (chandoan != null) ? chandoan.ChanDoan : "";
                                                                guiDL.ChanDoan = (chandoan != null) ? chandoan.ChanDoan : "";
                                                                guiDL.DiaChi = bn.DChi;
                                                                guiDL.GioiTinh = bn.GTinh == 1 ? "M" : "WM";
                                                                guiDL.MaBacSiChiDinh = themmoi.MaCB;
                                                                guiDL.MaBenhNhan = themmoi.MaBNhan;
                                                                guiDL.MaDichVu = themmoiCD.MaDV;
                                                                guiDL.NgaySinh = bn.NgaySinh;
                                                                guiDL.ThangSinh = bn.ThangSinh;
                                                                guiDL.NamSinh = bn.NamSinh;
                                                                guiDL.NhomDichVu = item.nhom;
                                                                guiDL.NoiChiDinh = _kp.TenKP;
                                                                guiDL.SDT = "";
                                                                guiDL.TenBacSiChiDinh = CB.TenCB;
                                                                guiDL.TenBenhNhan = bn.TenBNhan;
                                                                guiDL.TenDichVu = DV.TenDV;
                                                                guiDL.ThoiGianChiDinh = themmoi.NgayThang;
                                                                guiDL.TrangThai = "NW";

                                                                _ldata.Add(guiDL);
                                                            }
                                                        }
                                                        _idCCD = themmoiCD.IDCD;
                                                    }

                                                }

                                                // Thêm vào bảng CLSct
                                                var d = (from cls in _data.CLS.Where(p => p.IdCLS == idCLSnew)
                                                         join chidinh in _data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                                                         join dv in _data.DichVus on chidinh.MaDV equals dv.MaDV
                                                         join dvct in _data.DichVucts on chidinh.MaDV equals dvct.MaDV
                                                         select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, chidinh.Status, chidinh.IDCD, dvct.STT, dv.IDNhom, dv.IsAutoExecute, dv.SoPhutThucHien }).ToList();
                                                if (d.Count > 0)
                                                {
                                                    int dem = 0; // xác định có thêm vào bảng CLSct không
                                                    foreach (var item in d)
                                                    {
                                                        CLSct themmoiCL = new CLSct();
                                                        themmoiCL.IDCD = item.IDCD;
                                                        themmoiCL.MaDVct = item.MaDVct;
                                                        themmoiCL.Status = 0;
                                                        themmoiCL.STTHT = item.STT;
                                                        themmoiCL.Is_Execute = true;
                                                        _data.CLScts.Add(themmoiCL);
                                                        _data.SaveChanges();
                                                        dem++;
                                                        if (DungChung.Bien.MaBV == "24297")
                                                        {
                                                            if (item.TenDVct.ToUpper().Contains("LƯU HUYẾT NÃO") && item.IsAutoExecute == true)
                                                            {
                                                                thuchienLHN(idCLSnew, item.IDCD);
                                                            }
                                                        }
                                                        //else if (item.TenDVct.ToUpper().Contains("LƯU HUYẾT NÃO"))
                                                        //{
                                                        //    thuchienLHN(idCLSnew, item.IDCD);
                                                        //}
                                                    }

                                                    //dung280516
                                                    if (dem > 0)
                                                    {
                                                        BenhNhan sua = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                                                        if (sua != null)
                                                        {
                                                            sua.Status = 4;
                                                        }
                                                        _data.SaveChanges();
                                                    }

                                                    var dsChiDinh = d.Select(o => new { IDNhom = o.IDNhom, IDCD = o.IDCD, o.SoPhutThucHien, o.IsAutoExecute }).Distinct().ToList();
                                                    foreach (var item in dsChiDinh)
                                                    {
                                                        if ((DungChung.Bien.MaBV == "14017" || (DungChung.Bien.MaBV == "24297" && item.IsAutoExecute == true)) && item.IDNhom == 8)
                                                            ThucHienTTPT(_data, _Mabn, item.IDCD, item.SoPhutThucHien != null ? dtNgayCD.DateTime.AddMinutes(item.SoPhutThucHien ?? 0) : dtNgayCD.DateTime.AddMinutes(5), Convert.ToInt32(lupKhoaphong.EditValue), lupNguoiKhamkb.EditValue.ToString());
                                                    }
                                                    //
                                                }
                                            }
                                            if (trangthai == 1 || trangthai == 2)
                                            {
                                                FRM_chidinh_Load(sender, e);
                                                trangthai = 0;
                                                colYLenh.OptionsColumn.AllowEdit = false;
                                                colYLenh.OptionsColumn.ReadOnly = true;
                                                GrcNhomDV.Enabled = true;
                                                enableControl(true);
                                                btnSua.Enabled = false;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Danh mục dịch vụ thiết lập sai");
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region Nếu trạng thái đang là sửa.
                    if (trangthai == 2) // Nếu trạng thái đang là sửa.
                    {
                        bool tieptuc = true;
                        if (lupTN.EditValue != null)
                        {
                            string idTN = lupTN.Text;
                            var ktTN = from tn in _data.TieuNhomDVs.Where(p => p.TenRG == idTN)
                                       join ndv in _data.NhomDVs.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh") on tn.IDNhom equals ndv.IDNhom
                                       select tn;
                            if (mmYlenh.Text == "" && ktTN.Count() > 0)
                            {
                                DialogResult rs = MessageBox.Show("Bạn chưa nhập y lệnh. Bạn có muốn tiếp tục lưu?", "Hỏi lưu!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (rs == DialogResult.No)
                                {
                                    tieptuc = false;
                                }
                            }
                        }
                        if (tieptuc)
                        {

                            if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null)
                            {
                                _idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                                idCLSs.Add(_idCLS);
                            }
                            if (GrvNhomDV.GetFocusedRowCellValue(colIDCD) != null)
                                _idCCD = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colIDCD));
                            var suacls = _data.CLS.Single(p => p.IdCLS == _idCLS);
                            dateCDEdit = suacls.NgayThang.Value;
                            suacls.MaCB = lupNguoiKhamkb.EditValue.ToString();
                            suacls.MaKP = lupKhoaphong.EditValue == null ? 0 : Convert.ToInt32(lupKhoaphong.EditValue);
                            int makpth = LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue);
                            suacls.CapCuu = ckCapCuu.Checked;
                            if (suacls.MaKPth != makpth)
                            {
                                int _sott = FormNhap.frmHSBNNhapMoi._getSoTT(_data, dtNgayCD.DateTime, LupKpThuchien.EditValue == null ? 0 : Convert.ToInt32(LupKpThuchien.EditValue));
                                suacls.STT = _sott;
                            }
                            suacls.MaKPth = makpth;
                            // suacls.Code = txtCode.Text;
                            suacls.NgayThang = dtNgayCD.DateTime;
                            _data.SaveChanges();
                            // Xóa CLSct
                            var _lCLSct = (from chidinh in _data.ChiDinhs.Where(p => p.IDCD == _idCCD)
                                           join clsct in _data.CLScts on chidinh.IDCD equals clsct.IDCD
                                           select clsct).ToList();
                            foreach (var item in _lCLSct)
                            {
                                _data.CLScts.Remove(item);
                                _data.SaveChanges();
                            }
                            // Xóa Chỉ định trong bảng Chỉ Định
                            //List idcopy trước khi xóa
                            int idCopy = 0;
                            var _lChiDinh = (from chidinh in _data.ChiDinhs.Where(p => p.IDCD == _idCCD) select chidinh).ToList();
                            foreach (var c in _lChiDinh)
                            {
                                idCopy = c.IDCD_Copy ?? 0;
                                _data.ChiDinhs.Remove(c);
                                _data.SaveChanges();
                            }
                            // Thêm lại vào bảng ChiDinh
                            int _demSL_DV = 0;//kiểm tra khi xóa còn dịch vụ nào không
                            int dem = 0;
                            var test = _listDichVu.Where(p => p.Chon == true).ToList();
                            foreach (var item in _listDichVu.Where(p => p.Chon == true))
                            {
                                _demSL_DV++;
                                ChiDinh themmoiCD = new ChiDinh();
                                themmoiCD.IdCLS = _idCLS;
                                themmoiCD.MaDV = item.madv;
                                themmoiCD.Status = 0;
                                var dvui = _data.DichVus.Where(x => x.MaDV == item.madv).FirstOrDefault();
                                var bnnn = _data.BenhNhans.Where(x => x.MaBNhan == _Mabn).FirstOrDefault();
                                if (DungChung.Bien.MaBV == "24272")
                                {
                                    if (bnnn.DTuong == "Dịch vụ" || bnnn.IDDTBN == 2)
                                    {
                                        if (DungChung.Ham._checkTamThu(_data, _Mabn, _idCLS))
                                        {
                                            if (dvui.IsAutoExecute == true)
                                            {
                                                themmoiCD.Status = 1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (dvui.IsAutoExecute == true)
                                        {
                                            themmoiCD.Status = 1;
                                        }
                                    }

                                }
                                themmoiCD.TenDungCu = item.TenDungCu;
                                themmoiCD.NDVaoVien = item.NDVaoVien;
                                //themmoiCD.TrongBH = item.TrongBH == true ? 1 : 0;
                                if (DTBN == 1)
                                {
                                    themmoiCD.TrongBH = item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1);
                                }
                                else
                                {
                                    themmoiCD.TrongBH = 0;
                                }
                                themmoiCD.DonGia = DungChung.Ham._getGiaDM(_data, item.madv, item.TrongBH == false ? 0 : (item.CPDinhKem == true ? 2 : 1), _Mabn, dtNgayCD.DateTime);//item.DonGia;
                                themmoiCD.LoaiDV = Convert.ToInt32(item.TheoYC);
                                for (int i = 0; i < grvCLS.RowCount; i++)
                                {
                                    if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh) != null)
                                    {
                                        int maDV = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                        string ylenh = grvCLS.GetRowCellValue(i, colYLenh).ToString();
                                        if (item.madv == maDV)
                                        {
                                            themmoiCD.ChiDinh1 = ylenh;
                                            //break;
                                        }
                                    }
                                    if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colYLenh2) != null)
                                    {
                                        int maDV = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                        string ylenh2 = grvCLS.GetRowCellValue(i, colYLenh2).ToString();
                                        if (item.madv == maDV)
                                        {
                                            themmoiCD.YLenh2 = ylenh2;
                                            //break;
                                        }
                                    }
                                }
                                themmoiCD.XHH = 0;
                                for (int i = 0; i < grvCLS.RowCount; i++)
                                {
                                    if (grvCLS.GetRowCellValue(i, MaDV) != null && grvCLS.GetRowCellValue(i, colXHH) != null)
                                    {
                                        int madv = Convert.ToInt32(grvCLS.GetRowCellValue(i, MaDV));
                                        string xhh = grvCLS.GetRowCellValue(i, colXHH).ToString();

                                        if (item.madv == madv)
                                        {
                                            themmoiCD.XHH = xhh == "True" ? 1 : 0;
                                            //break;
                                        }
                                    }

                                }
                                themmoiCD.IDCD_Copy = idCopy;
                                _data.ChiDinhs.Add(themmoiCD);
                                _data.SaveChanges();

                                //Update lại thông tin cho thằng sao
                                if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                                {
                                    var dataSource = (List<C_DichVu>)grcCLS.DataSource;
                                    var row = dataSource.FirstOrDefault(o => o.idcd == item.idcd);
                                    if (row != null && suacls.IS_COPY == true)
                                    {
                                        var cdCopy = _data.ChiDinhs.Where(o => o.IDCD_Copy == item.idcd).ToList();
                                        foreach (var cd in cdCopy)
                                        {
                                            var cdUpdate = _data.ChiDinhs.FirstOrDefault(o => o.IDCD == cd.IDCD);
                                            if (cdUpdate != null)
                                            {
                                                cdUpdate.YLenh2 = row.YLenh2;
                                                cdUpdate.ChiDinh1 = row.YLenh;
                                                cdUpdate.IDCD_Copy = themmoiCD.IDCD;
                                            }
                                        }
                                        _data.SaveChanges();
                                    }
                                }
                                // Thêm  vào bảng CLSct
                                var _lDichVuct =
                                    (from chidinh in _data.ChiDinhs.Where(p => p.IDCD == themmoiCD.IDCD)
                                     join dvct in _data.DichVucts on chidinh.MaDV equals dvct.MaDV
                                     select new { dvct.MaDVct, dvct.TenDVct, dvct.TSBT, chidinh.Status, chidinh.IDCD, dvct.STT }).ToList();

                                foreach (var item2 in _lDichVuct)
                                {
                                    CLSct themmoiCL = new CLSct();
                                    themmoiCL.IDCD = item2.IDCD;
                                    themmoiCL.MaDVct = item2.MaDVct;
                                    themmoiCL.Status = 0;
                                    themmoiCL.STTHT = item.SoTT;
                                    _data.CLScts.Add(themmoiCL);
                                    _data.SaveChanges();
                                    dem++;
                                }
                                if ((DungChung.Bien.MaBV == "14017" || (DungChung.Bien.MaBV == "24297" && item.IsAutoExecute == true)) && item.IdNhom == 8)
                                    ThucHienTTPT(_data, _Mabn, themmoiCD.IDCD, item.SoPhutThucHien != null ? dtNgayCD.DateTime.AddMinutes(item.SoPhutThucHien ?? 0) : dtNgayCD.DateTime.AddMinutes(5), Convert.ToInt32(lupKhoaphong.EditValue), lupNguoiKhamkb.EditValue.ToString());
                            }

                            //dung28052016
                            if (dem > 0)
                            {
                                BenhNhan sua = _data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                                if (sua != null)
                                {
                                    sua.Status = 4;
                                }
                                _data.SaveChanges();
                            }
                            //
                            var ktraxoa = _data.ChiDinhs.Where(p => p.IdCLS == _idCLS).ToList();
                            if (ktraxoa.Count == 0)// khi xóa trong GrcCLS dịch vụ đã được chỉ định
                            {
                                var Xoa = _data.CLS.Single(p => p.IdCLS == _idCLS);
                                _data.CLS.Remove(Xoa);
                                _data.SaveChanges();

                                if (_lIDCLS_DB.Contains(_idCLS))
                                    _lIDCLS_DB.Remove(_idCLS);
                            }
                            if (trangthai == 1 || trangthai == 2)
                            {

                                FRM_chidinh_Load(sender, e);
                                trangthai = 0;
                                colYLenh.OptionsColumn.AllowEdit = false;
                                colYLenh.OptionsColumn.ReadOnly = true;
                                GrcNhomDV.Enabled = true;
                                enableControl(true);
                                btnSua.Enabled = false;
                            }
                        }
                    }

                    #endregion
                    if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                    {
                        if (dateCD.Date != dateCDEdit.Date)
                        {
                            UpdateDienBienSua(dateCD, makpcd);
                            UpdateDienBienSua(dateCDEdit, makpcd);
                        }
                        else
                        {
                            UpdateDienBienSua(dateCD, makpcd);
                        }
                    }

                    var rowHandleNhomDV = GrvNhomDV.LocateByValue("IdCLS", _idCLS);
                    if (rowHandleNhomDV != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                    {
                        GrvNhomDV.FocusedRowHandle = rowHandleNhomDV;
                    }

                    var rowHandleDV = grvCLS.LocateByValue("idcd", _idCCD);
                    if (rowHandleDV != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                    {
                        grvCLS.FocusedRowHandle = rowHandleDV;
                    }

                    if (DungChung.Bien.MaBV.Equals("24012"))
                    {
                        var clsChecks = (from cls in _data.CLS
                                         where idCLSs.Contains(cls.IdCLS)
                                           && (cls.Code.Contains("CDHA") || cls.Code.Contains("CĐHA"))
                                         select new
                                         {
                                             cls.IdCLS,
                                             cls.Code,
                                         }).ToList();

                        foreach (var idCLS in clsChecks)
                        {
                            //var response = Task.Run(async () => await SendPacs(DungChung.Bien.MaBV, _Mabn, idCLS.IdCLS, dtNgayCD.DateTime.ToString("yyyy-MM-dd HH:mm:ss"))).Result;
                            //if (response == null || (response != null && response.ContentSuccess != null && !response.ContentSuccess.Data))
                            //{
                            //    MessageBox.Show("Gửi dữ liệu sang hệ thống PACS không thành công." + response == null ? string.Empty : "\n" + response.ContentSuccess.Message);
                            //}

                            Task.Run(async () => await SendPacs(DungChung.Bien.MaBV, _Mabn, idCLS.IdCLS, dtNgayCD.DateTime.ToString("yyyy-MM-dd HH:mm:ss")));
                        }
                    }
                }

                _ldata.Clear();
                btnMoi.Focus();
                ckCapCuu.Checked = false;
            }
        }
        public _getstring GetData;
        public delegate void _getstring(string chuoi1);
        public async Task<BaseApiResponse<PacsResultModel, PacsResultModel>> SendPacs(string hospitalCode, int patentId, int orderId, string orderDate, string type = "NW")
        {
            var result = new BaseApiResponse<PacsResultModel, PacsResultModel>();

            try
            {
                string baseUrl = System.Configuration.ConfigurationManager.AppSettings["URL_Send_PACS"];
                if (!string.IsNullOrEmpty(baseUrl))
                {
                    var url = $"api/PACS/SendOrder?hospitalCode={hospitalCode}&patentId={patentId}&orderId={orderId}&orderDate={orderDate}&type={type}";
                    result = await AppApi.GetAsync<PacsResultModel, PacsResultModel>(baseUrl, url);
                }
            }
            catch (Exception) { }

            return await Task.FromResult(result);
        }

        public async Task<BaseApiResponse<PacsResultModel, PacsResultModel>> DeletePacs(int orderId)
        {
            var result = new BaseApiResponse<PacsResultModel, PacsResultModel>();

            try
            {
                string baseUrl = System.Configuration.ConfigurationManager.AppSettings["URL_Send_PACS"];
                if (!string.IsNullOrEmpty(baseUrl))
                {
                    var url = $"api/PACS/DeleteOrder?orderId={orderId}";
                    result = await AppApi.DeleteAsync<PacsResultModel, PacsResultModel>(baseUrl, url);
                }
            }
            catch (Exception) { }

            return await Task.FromResult(result);
        }

        public static bool ThucHienTTPT(QLBVEntities _Data, int maBnhan, int _idcd, DateTime ngayth, int maKPth, string maCBth)
        {
            string dscb = ""; int makp = 0;
            int tyleTT = 100;
            var chidinh = _Data.ChiDinhs.Single(p => p.IDCD == _idcd);
            int idcls = 0;
            if (chidinh != null)
            {
                idcls = chidinh.IdCLS ?? 0;

            }
            var suacls = _Data.CLS.Single(p => p.IdCLS == idcls);
            if (chidinh != null)
            {
                chidinh.Status = 1;
                chidinh.NgayTH = ngayth;
                chidinh.DSCBTH = dscb;
                chidinh.MaCBth = null;
                chidinh.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(ngayth));
                _Data.SaveChanges();
                var suaclsct = _Data.CLScts.Where(p => p.IDCD == chidinh.IDCD).ToList();
                foreach (var b in suaclsct)
                {
                    b.Status = 1;
                    _Data.SaveChanges();
                }
                var qdtct = _Data.DThuoccts.Where(p => p.IDCD == _idcd).ToList();
                foreach (var a in qdtct)
                {
                    a.NgayNhap = ngayth;
                    a.MaCB = maCBth;
                    a.DSCBTH = dscb;
                    _Data.SaveChanges();
                }
            }
            var chidinhall = _Data.ChiDinhs.Where(p => p.IdCLS == idcls && p.Status == 0).ToList();
            suacls.MaCBth = null;
            makp = suacls.MaKP == null ? 0 : Convert.ToInt32(suacls.MaKP); // Lấy makp để gán vào makp trong bàng DThuocct
            // kiểm tra Khoa phòng thực hiện Lâm sàng thì gán vào DTHuocct
            var kp = _Data.KPhongs.Where(p => (p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám") && p.MaKP == suacls.MaKPth).ToList();
            if (kp.Count > 0)
                makp = suacls.MaKPth == null ? 0 : Convert.ToInt32(suacls.MaKPth);
            suacls.NgayTH = ngayth;
            if (chidinhall.Count <= 0)
                suacls.Status = 1;
            suacls.DSCBTH = dscb;

            _Data.SaveChanges();
            frm_ThucHienPT.updateDT(maBnhan, chidinh.IDCD, tyleTT, null, dscb);
            return true;
        }

        private void UpdateDienBienSua(DateTime dateCD, int makpcd)
        {
            DungChung.Ham.Update_CLS_DienBienct(_Mabn, dateCD.Date, makpcd);
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dienBien = _data.DienBiens.FirstOrDefault(o => o.MaBNhan == _Mabn && o.NgayNhap.Value.Day == dateCD.Day && o.NgayNhap.Value.Month == dateCD.Month && o.NgayNhap.Value.Year == dateCD.Year && o.Loai == 1 && o.MaKP == makpcd);
            if (dienBien != null)
            {
                dienBien.NgayNhap = dateCD;
                dienBien.YLenh = "";
                dienBien.MaKP = makpcd;
                _data.SaveChanges();
                DungChung.Ham.Update_DienBien_All(dienBien.ID, makpcd);
                if (reLoad != null)
                    reLoad();
            }
            else
            {
                DienBien dienBienNew = new DienBien();
                dienBienNew.NgayNhap = dateCD;
                dienBienNew.YLenh = "";
                dienBienNew.DienBien1 = "";
                dienBienNew.MaKP = makpcd;
                dienBienNew.MaCB = DungChung.Bien.MaCB;
                dienBienNew.Loai = 1;
                dienBienNew.Ploai = 0;
                dienBienNew.MaBNhan = _Mabn;
                _data.DienBiens.Add(dienBienNew);
                _data.SaveChanges();

                DungChung.Ham.Update_DienBien_All(dienBienNew.ID, makpcd);
                if (reLoad != null)
                    reLoad();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            FRM_chidinh_Load(sender, e);
            colYLenh.OptionsColumn.AllowEdit = false;
            colYLenh.OptionsColumn.ReadOnly = true;
            TrongDMBH.OptionsColumn.ReadOnly = true;
            colXHH.OptionsColumn.ReadOnly = true;

        }

        private void btnNewBarCode_Click(object sender, EventArgs e)
        {
            setNewBarCode();
        }

        public class objBarCode
        {
            public string BarCode { set; get; }
            public string Code { set; get; }
        }
        private void setNewBarCode()
        {
            DateTime ngaygio = dtNgayCD.DateTime;
            List<objBarCode> q = new List<objBarCode>();
            q = (from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn && p.NgayThang.Value.Day == ngaygio.Day && p.NgayThang.Value.Month == ngaygio.Month)
                 group new { cls } by new { cls.MaBNhan, cls.BarCode } into kq
                 select new objBarCode
                 {
                     BarCode = kq.Key.BarCode,
                     Code = kq.Max(p => p.cls.Code)
                 }).ToList();

            if (q.Count() > 0 && q.First().BarCode != null && q.First().BarCode != "")
            {
                if (!string.IsNullOrEmpty(txtCode.Text))
                {
                    int sttbc = Convert.ToInt32(txtCode.Text.Substring(0, 1));
                    if (sttbc < 10)
                    {
                        string code = (sttbc + 1) + ngaygio.ToString("ddMM") + _Mabn;
                        q.Add(new objBarCode { BarCode = q.First().BarCode, Code = code });
                        int stt = Convert.ToInt32(q.First().Code.Substring(0, 1));
                        txtCode.Text = (stt + 1) + ngaygio.ToString("ddMM") + _Mabn;
                    }
                    else
                        MessageBox.Show("Mã code phải < 10");
                }


            }
            else
            {
                if (!string.IsNullOrEmpty(txtCode.Text))
                {
                    int sttbc = Convert.ToInt32(txtCode.Text.Substring(0, 1));
                    if (sttbc < 10)
                    {
                        string code = (sttbc + 1) + ngaygio.ToString("ddMM") + _Mabn;
                        q.Add(new objBarCode { BarCode = q.First().BarCode, Code = code });
                        int stt = Convert.ToInt32(q.First().Code.Substring(0, 1));
                        txtCode.Text = (stt + 1) + ngaygio.ToString("ddMM") + _Mabn;
                    }
                    else
                        MessageBox.Show("Mã code phải < 10");
                }
                else
                    txtCode.Text = 1 + ngaygio.ToString("ddMM") + _Mabn;
            }
        }

        public void setBarCode()
        {
            DateTime ngaygio = dtNgayCD.DateTime;
            var q =
                from cls in _data.CLS.Where(p => p.MaBNhan == _Mabn && p.NgayThang.Value.Day == ngaygio.Day && p.NgayThang.Value.Month == ngaygio.Month)
                group new { cls } by new { cls.MaBNhan, cls.BarCode } into kq
                select new
                {
                    BarCode = kq.Key.BarCode,
                    Code = kq.Max(p => p.cls.Code)
                };
            if (q.Count() > 0 && q.First().BarCode != null && q.First().BarCode != "")
            {
                int stt = 0;
                int.TryParse(q.First().Code.Substring(0, 1), out stt);
                txtCode.Text = (stt + 1) + ngaygio.ToString("ddMM") + _Mabn;
            }
            else
            {
                txtCode.Text = 1 + ngaygio.ToString("ddMM") + _Mabn;
            }
        }
        private class C_KetQua
        {
            //MaDVct,tn.TenTN, dvct.TenDVct, dvct.TSBT, dvct.DonVi, dvct.TSBTnu, clsct.KetQua, dvct.STT
            private string maDVct;
            private String tenTN;
            private String tenDVct;
            private String donVi;
            private String tSBT;
            private String tSBTnu;
            private String ketQua;
            private int stt;

            public string MaDVct
            {
                get { return maDVct; }
                set { maDVct = value; }
            }
            public String TenTN
            {
                get { return tenTN; }
                set { tenTN = value; }
            }
            public String TenDVct
            {
                get { return tenDVct; }
                set { tenDVct = value; }
            }
            public String TSBT
            {
                get { return tSBT; }
                set { tSBT = value; }
            }
            public String DonVi
            {
                get { return donVi; }
                set { donVi = value; }
            }
            public String TSBTnu
            {
                get { return tSBTnu; }
                set { tSBTnu = value; }
            }
            public String KetQua
            {
                get { return ketQua; }
                set { ketQua = value; }
            }
            public int Stt
            {
                get { return stt; }
                set { stt = value; }
            }
        }
        private class C_YLenh
        {
            private int maDV;
            private string yLenh;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            public string YLenh
            {
                get { return yLenh; }
                set { yLenh = value; }
            }
        }
        private void grvCLS_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvCLS.GetFocusedRowCellValue(MaDV) != null)
            {
                _lYLenh = _data.ChiDinhs.Where(p => p.ChiDinh1 != null && p.ChiDinh1 != "").Select(p => new C_YLenh() { MaDV = p.MaDV == null ? 0 : p.MaDV.Value, YLenh = p.ChiDinh1 }).Distinct().Take(200).ToList();
                int _MaDV = Convert.ToInt32(grvCLS.GetFocusedRowCellValue(MaDV));
                colCBOYlenh.Items.Clear();
                colCBOYlenh.Items.AddRange(_lYLenh.Where(p => p.MaDV == _MaDV).Select(p => p.YLenh).Distinct().Take(30).ToList());
                mmYlenh.Properties.Items.Clear();
                mmYlenh.Properties.Items.AddRange(_lYLenh.Where(p => p.MaDV == _MaDV).Select(p => p.YLenh).Distinct().Take(30).ToList());
                if (grvCLS.GetFocusedRowCellValue(colYLenh) != null)
                {
                    string ylenh = grvCLS.GetFocusedRowCellValue(colYLenh).ToString();
                    mmYlenh.Text = ylenh;
                }
                else
                {
                    mmYlenh.Text = "";
                }
            }

        }

        private void mmYlenh_Leave(object sender, EventArgs e)
        {
            grvCLS.SetFocusedRowCellValue(colYLenh, mmYlenh.Text);
        }

        private void grvCLS_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (grvCLS.GetRowCellValue(e.RowHandle, colYLenh) != null)
            {
                mmYlenh.Text = grvCLS.GetRowCellValue(e.RowHandle, colYLenh).ToString();
            }

        }

        private void FRM_chidinh_Moi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (trangthai == 1 || trangthai == 2)
            {
                DialogResult _result;
                _result = MessageBox.Show("Bạn chưa lưu chỉ định, Bạn vẫn muốn thoát?", "Hỏi thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.No)
                    e.Cancel = true;
                else
                {
                    this.Dispose();
                    this.Close();
                }
            }
            else
            {
                this.Dispose();
                this.Close();
            }
            MemoryManagement.FlushMemory();

        }

        private void cbo_ChonIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_ChonIn.SelectedIndex >= 0)
            {
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int _IDCLS = 0, _IDCD = 0;
                if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null)
                    _IDCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();


                var chidinh = _data.ChiDinhs.Where(p => p.IdCLS == _IDCLS).Select(p => p.IDCD).ToList();
                if (chidinh.Count > 0)
                    _IDCD = chidinh.First();
                int mauin = -1;
                mauin = cbo_ChonIn.SelectedIndex;
                if (mauin == 3)
                {
                    PhieuGiacMac r = new PhieuGiacMac();
                    var hthong = (from ht in _data.HTHONGs select new { ht }).ToList();
                    if (hthong.Count > 0)
                    {
                        r.CQCQ = hthong.First().ht.TenCQCQ;
                    }
                    var _macqcq = _data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                    r.TenCQ = _macqcq.TenBV;

                    int idjaval = 666;
                    int idDiop = 675;

                    var sovv = "";
                    if (_data.BenhNhans.Where(p => p.MaBNhan == _Mabn).Select(p => p.NoiTru).FirstOrDefault() == 1)
                    {
                        sovv = _data.VaoViens.Where(p => p.MaBNhan == _Mabn).Select(p => p.SoVV).FirstOrDefault();
                    }

                    var benhnhan = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                                    join bnkb in _data.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                                    join kp in _data.KPhongs on bnkb.MaKP equals kp.MaKP
                                    join cb in _data.CanBoes on bnkb.MaCB equals cb.MaCB
                                    join cls in _data.CLS on bn.MaBNhan equals cls.MaBNhan
                                    join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                    join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                                    join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                                    select new { bn, cls, cd, dv, clsct, cb, kp, bnkb }).ToList();

                    if (benhnhan.Count() > 0)
                    {
                        foreach (var item in benhnhan)
                        {
                            if (item.dv.MaDV == idjaval)
                            {
                                r.TenCdJaval = "+ " + item.dv.TenDV;
                            }
                            if (item.cd.IdCLS == idDiop)
                            {
                                r.TenCdDiop = "+ " + item.dv.TenDV;
                            }
                        }
                        r.SVV = sovv;
                        //r.KetQua = benhnhan.First().clsct.KetQua;
                        r.Buong = benhnhan.First().bnkb.Buong;
                        r.Giuong = benhnhan.First().bnkb.Giuong;
                        r.DiaChiBV = DungChung.Bien.DiaChi;
                        //r.BSTH = 
                        r.DienThoai = _data.HTHONGs.First().SDT == null ? "" : "Điện thoại: " + _data.HTHONGs.First().SDT;
                        r.HoVaTen = benhnhan.First().bn.TenBNhan.ToUpper();
                        r.Tuoi = benhnhan.First().bn.Tuoi;
                        r.GioiTinh = benhnhan.First().bn.GTinh == 1 ? "Nam" : "Nữ";
                        r.DiaChi = benhnhan.First().bn.DChi;
                        r.BSDT = benhnhan.First().cb.TenCB;
                        r.KPhong = benhnhan.First().kp.TenKP;
                        r.SDT = _data.TTboXungs.Where(p => p.MaBNhan == _Mabn).First().DThoai ?? "";
                        r.ChanDoan = benhnhan.First().cls.ChanDoan;
                        r.KetLuan = benhnhan.First().cd.KetLuan;
                        r.ThoiGian = "Bắc Ninh, Ngày " + DungChung.Ham.NgaySangChu(Convert.ToDateTime(benhnhan.First().cd.NgayTH), 12);

                        var kqJaval = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                                       join cls in _data.CLS on bn.MaBNhan equals cls.MaBNhan
                                       join cb in _data.CanBoes on cls.MaCBth equals cb.MaCB
                                       join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                       join dv in _data.DichVus.Where(p => p.MaDV == idjaval) on cd.MaDV equals dv.MaDV
                                       join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                                       select new { clsct, cb, cd }).ToList();


                        if (kqJaval.Count() > 0)
                        {
                            r.KetQua_javal = kqJaval.First().clsct.KetQua;
                            r.BSTH_Javal = kqJaval.First().cb.TenCB;
                        }
                        else
                        {
                            r.KetQua_javal = "";
                        }

                        var kqDiop = (from bn in _data.BenhNhans.Where(p => p.MaBNhan == _Mabn)
                                      join cls in _data.CLS on bn.MaBNhan equals cls.MaBNhan
                                      join cb in _data.CanBoes on cls.MaCBth equals cb.MaCB
                                      join cd in _data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                      join dv in _data.DichVus.Where(p => p.MaDV == idDiop) on cd.MaDV equals dv.MaDV
                                      join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                                      select new { clsct, cb }).ToList();

                        if (kqDiop.Count() > 0)
                        {
                            r.KetQua_diop = kqDiop.First().clsct.KetQua;
                            r.BSTH_Diop = kqDiop.First().cb.TenCB;
                        }
                        else
                        {
                            r.KetQua_diop = "";
                        }
                        List<PhieuGiacMac> rep = new List<PhieuGiacMac>();
                        rep.Add(r);

                        DungChung.Ham.Print(DungChung.PrintConfig.rep_GiacMac, rep, new Dictionary<string, object>(), false);
                    }
                }
                else
                {
                    CLS.InPhieu.InLuuHuyetNao(_data, _IDCD, TTN, mauin);
                }
            }
            cbo_ChonIn.SelectedIndex = -1;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<int> _lIdCLS = new List<int>();
            List<int?> _lMaDV = new List<int?>();
            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            if (dataSource != null)
            {
                _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0).Select(o => o.IdCLS).ToList();
                _lMaDV = dataSource.Where(o => o.Check && o.MaDV > 0).Select(o => o.MaDV).Distinct().ToList();
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();

            bool _InPhieu0 = DungChung.Bien._Visible_CDHA[0];
            bool _InPhieu1 = DungChung.Bien._Visible_CDHA[1];
            DungChung.Bien._Visible_CDHA[0] = true;
            if (DungChung.Bien.MaBV != "01830")
                DungChung.Bien._Visible_CDHA[1] = true;
            DungChung.Bien._Visible_CDHA[2] = true;
            int _IDCLS = 0;
            if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null)
                _IDCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
            string TTN = "";
            if (GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
                TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();// l?y l?i theo tên rút g?n ti?u nhóm. 
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (CLS.InPhieu._isCDHA(_data, _IDCLS))
            {
                CLS.InPhieu._inPhieu_CDHA_mau(_data, TTN, _Mabn, _IDCLS, 0);
            }
            else
            {
                CLS.InPhieu._InPhieu_XetNghiem(_data, TTN, _Mabn, _IDCLS, 0, _lIdCLS, _lMaDV);

            }
            DungChung.Bien._Visible_CDHA[0] = _InPhieu0;
            DungChung.Bien._Visible_CDHA[1] = _InPhieu1;
        }
        void infull()
        {
            List<int> _lIdCLS = new List<int>();
            List<int?> _lMaDV = new List<int?>();
            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            if (dataSource != null)
            {
                _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0).Select(o => o.IdCLS).ToList();
                _lMaDV = dataSource.Where(o => o.Check && o.MaDV > 0).Select(o => o.MaDV).Distinct().ToList();
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();

            string _inMauCD = "A5";
            if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "34019")
                _inMauCD = "A4";
            for (int i = 0; i < GrvNhomDV.RowCount; i++)
                if (GrvNhomDV.GetRowCellValue(i, "IdCLS") != null && GrvNhomDV.GetRowCellValue(i, colTenRG) != null)
                {

                    int idCLS = Convert.ToInt32(GrvNhomDV.GetRowCellValue(i, "IdCLS"));
                    string TTN = GrvNhomDV.GetRowCellValue(i, colTenRG).ToString();
                    var cls = _data.CLS.Where((p => p.Status == 0 || p.Status == null)).Where(p => p.IdCLS == idCLS).ToList();
                    if (cls.Count > 0)
                    {
                        frmIn frm = new frmIn();
                        switch (TTN)
                        {

                            case "XN dịch chọc dò":

                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                                break;

                            case "XN hóa sinh máu":
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                                break;
                            case "XN huyết học":
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                                //}
                                break;
                            case "XN nước tiểu":
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                                break;
                            case "XN khác":
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm, _inMauCD, _Mabn, idCLS, TTN);
                                break;
                            //}
                            case "XN hóa sinh nội tiết tố":
                                frmIn frmnt = new frmIn();
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frmnt, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                                break;
                            case "Trắc nghiệm tâm lý":
                                frmIn frmTNTL = new frmIn();
                                CLS.InPhieu._setParam_ChiDinh_TNTL(_data, frmTNTL, _inMauCD, _Mabn, idCLS);
                                break;
                            default:
                                frmIn frm4 = new frmIn();
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4, _inMauCD, _Mabn, idCLS, TTN);
                                break;
                        }
                    }
                }
        }
        private void btnInFull_Click(object sender, EventArgs e)
        {

        }
        #region dem
        private string[] getSoBNChuaTH_CLS(DateTime dt, string tenrutgon)
        {
            string[] _kq = new string[] { "", "" };
            try
            {
                if (dt != null)
                {

                    string ms = "", ms2;
                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                    DateTime tungay = DungChung.Ham.NgayTu(dt);
                    DateTime denngay = DungChung.Ham.NgayDen(dt);
                    var dichvu = (from dv in data.DichVus
                                  join tn in data.TieuNhomDVs.Where(p => p.TenRG == tenrutgon) on dv.IdTieuNhom equals tn.IdTieuNhom
                                  select new { dv.MaDV }).ToList();
                    var qcls = (from cls in data.CLS.Where(p => p.NgayThang >= tungay && p.NgayThang <= denngay)
                                join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                select new { cls.Status, cls.MaBNhan, cls.MaKPth, cd.MaDV }).ToList().Distinct();
                    var kphong = _kphong.ToList();
                    var q = (from bn in qcls
                             join dvu in dichvu on bn.MaDV equals dvu.MaDV
                             join kp in kphong on bn.MaKPth equals kp.MaKP
                             group new { bn, kp } by new { bn.MaKPth, kp.TenKP } into kq
                             select new
                             {
                                 kq.Key.MaKPth,
                                 kq.Key.TenKP,
                                 SoBNc = kq.Where(p => p.bn.Status == 0).Count(),
                                 SoBN = kq.Count()
                             }
                             ).ToList();
                    if (q.Count > 0)
                    {
                        //ms = "Số bệnh nhân chờ: \n";
                        int i = 1;
                        foreach (var a in q)
                        {
                            ms += i.ToString() + ". " + a.TenKP + ": " + a.SoBNc.ToString() + "/" + a.SoBN + "  ";
                            i++;
                        }
                        ms2 = "Số BN thực chỉ định tại KP?";
                    }
                    _kq[0] = ms;
                }

                return _kq;
            }
            catch (Exception)
            {

                return _kq;
            }
        }

        #endregion
        private void lupTenDV_EditValueChanged(object sender, EventArgs e)
        {
            int _madv = 0;
            if (lupTenDV.EditValue != null)
                _madv = Convert.ToInt32(lupTenDV.EditValue);
            txtmaQD.Text = "";
            foreach (C_DichVu item in _listDichVu.Where(p => p.madv == _madv))
            {
                chkDichVu.Checked = item.TrongBH;
                txtmaQD.Text = item.MaQD;
            }
        }

        private void chkDichVu_CheckedChanged(object sender, EventArgs e)
        {
            int _madv = 0;
            if (lupTenDV.EditValue != null)
                _madv = Convert.ToInt32(lupTenDV.EditValue);
            //double DonGia = 0;
            foreach (C_DichVu item in _listDichVu.Where(p => p.madv == _madv))
            {
                if (chkDichVu.Checked)
                {
                    txtDonGia.Text = item.dongia.ToString("##,###");
                    ckccpdinhkem.Checked = false;
                }
                else
                {
                    txtDonGia.Text = item.DonGia2.ToString("##,###");
                }
            }
        }

        private void btnSao_Click(object sender, EventArgs e)
        {
            //this.Hide();
            QLBV.FormThamSo.frm_SaoChiDinh frm = new QLBV.FormThamSo.frm_SaoChiDinh(_Mabn);
            frm.reloaddata = new QLBV.FormThamSo.frm_SaoChiDinh.ReLoad(LoadLaiForm);
            frm.ShowDialog();
            //this.Show();
        }
        void LoadLaiForm()
        {
            FRM_chidinh_Load(null, null);
        }
        private void radTenRG_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenrg = "";
            lupGoiDV.EditValue = null;
            LupKpThuchien.Enabled = true;
            tenrg = radTenRG.Text;
            lupTN.Text = tenrg;
            txtTimkiemMaDV.Text = "";
        }
        public static string _tendv = "";
        //public class IDCD
        //{
        //    public int idcd { get; set; }
        //}
        //List<IDCD> _lIdCLS_30372 = new List<IDCD>();
        void inchidinh(int mauphieu)
        {
            List<int> _lIdCLS = new List<int>();
            List<int?> _lMaDV = new List<int?>();
            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            if (dataSource != null)
            {
                if (DungChung.Bien.MaBV == "30372")
                {
                    _lIdCLS = dataSource.Where(o => o.Check && o.IDCD > 0).Select(o => o.IDCD).ToList();
                }
                else
                {
                    _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0).Select(o => o.IdCLS).ToList();
                }
                _lMaDV = dataSource.Where(o => o.Check && o.MaDV > 0).Select(o => o.MaDV).Distinct().ToList();
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();

            #region chi dịnh
            string _inMauCD = "A5";
            if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "14017")
                _inMauCD = "A4";

            if (mauphieu == 1)  //---------------------------------------------------- IN chỉ định--------------------------------------------------
            {
                if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
                {

                    int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                    string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();
                    _tendv = GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString();
                    int status = 0;
                    var cls = _data.CLS.Where(p => p.IdCLS == idCLS).ToList();
                    if (cls.Count > 0)
                        status = cls.First().Status;
                    switch (TTN)
                    {

                        case "XN dịch chọc dò":
                            using (frmIn frmcd = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "24012")
                                    CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frmcd, _inMauCD, _Mabn, idCLS, _lIdCLS, true, "");
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frmcd, _inMauCD, _Mabn, idCLS, false, "");
                            }
                            break;

                        case "XN hóa sinh máu":
                            using (frmIn frm = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "26007")
                                    _inMauCD = "A4";
                                if (DungChung.Bien.MaBV == "30303")
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm, "A4", _Mabn, idCLS, false, "");
                                else if (DungChung.Bien.MaBV == "24012")
                                    CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frm, _inMauCD, _Mabn, idCLS, _lIdCLS, false, "");
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm, _inMauCD, _Mabn, idCLS, false, "");
                            }
                            break;
                        case "XN huyết học":
                            using (frmIn frm1 = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "26007")
                                    _inMauCD = "A4";
                                if (DungChung.Bien.MaBV == "30303")
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm1, "A4", _Mabn, idCLS, false, "");
                                else if (DungChung.Bien.MaBV == "24012")
                                    CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frm1, _inMauCD, _Mabn, idCLS, _lIdCLS, false, "");
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm1, _inMauCD, _Mabn, idCLS, false, "");
                            }
                            break;
                        case "XN nước tiểu":
                            using (frmIn frm2 = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "26007")
                                    _inMauCD = "A4";
                                if (DungChung.Bien.MaBV == "24012")
                                    CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frm2, _inMauCD, _Mabn, idCLS, _lIdCLS, false, "");
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm2, _inMauCD, _Mabn, idCLS, false, "");
                            }

                            break;
                        //}
                        case "XN vi sinh":
                            using (frmIn frm22 = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    if ((GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SAR-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Realtime PCR SARS-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SARS-CoV-2") && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"))
                                        CLS.InPhieu._InPhieu_XetNghiem(_data, GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString(), _Mabn, Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS")), 1, _lIdCLS, _lMaDV);
                                    else
                                        CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frm22, _inMauCD, _Mabn, idCLS, _lIdCLS, false, "");
                                }
                                else
                                {
                                    if ((GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SAR-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Realtime PCR SARS-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SARS-CoV-2") && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"))
                                        CLS.InPhieu._InPhieu_XetNghiem(_data, GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString(), _Mabn, Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS")), 1, _lIdCLS, _lMaDV);
                                    else
                                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm22, _inMauCD, _Mabn, idCLS, false, "");
                                }
                            }

                            break;
                        case "XN hóa sinh miễn dịch":
                            using (frmIn frm212 = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "24012")
                                    CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frm212, _inMauCD, _Mabn, idCLS, _lIdCLS, false, "");
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm212, _inMauCD, _Mabn, idCLS, false, "");
                            }
                            break;
                        case "XN Đông cầm máu":
                            using (frmIn frm21 = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "24012")
                                    CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frm21, _inMauCD, _Mabn, idCLS, _lIdCLS, false, "");
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm21, _inMauCD, _Mabn, idCLS, false, "");
                            }
                            break;
                        case "X-Quang CT":
                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                goto default;
                            else
                            {
                                using (frmIn frm31 = new frmIn())
                                {
                                    if (DungChung.Bien.MaBV == "30303")
                                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm31, "A4", _Mabn, idCLS, false, "");
                                    else
                                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm31, _inMauCD, _Mabn, idCLS, false, "");
                                }
                            }
                            break;
                        case "XN khác":

                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                            {
                                using (frmIn frm211 = new frmIn())
                                {
                                    CLS.InPhieu._InPhieu_XetNghiem(_data, TTN, _Mabn, idCLS, 0, _lIdCLS, _lMaDV);
                                }
                                break;
                            }
                            else
                            {
                                using (frmIn frm3 = new frmIn())
                                {
                                    if (DungChung.Bien.MaBV == "24012")
                                        CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frm3, _inMauCD, _Mabn, idCLS, _lIdCLS, true, "");
                                    else
                                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm3, _inMauCD, _Mabn, idCLS, false, "");
                                }
                                break;
                            }
                        //}
                        case "XN Ung thư":
                            using (frmIn frmut = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "24012")
                                    CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frmut, _inMauCD, _Mabn, idCLS, _lIdCLS, true, "");
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frmut, _inMauCD, _Mabn, idCLS, false, "");
                            }
                            break;
                        case "XN Nồng độ cồn":
                            _InPhieuCDNongDoCon(_data, _Mabn, idCLS);
                            break;
                        case "XN hóa sinh nội tiết tố":
                            using (frmIn frmnt = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "24012")
                                    CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frmnt, _inMauCD, _Mabn, idCLS, _lIdCLS, true, "");
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frmnt, _inMauCD, _Mabn, idCLS, false, "");
                            }
                            break;
                        case "Trắc nghiệm tâm lý":
                            if (DungChung.Bien.MaBV == "34019")
                            {
                                using (frmIn frm40 = new frmIn())
                                {
                                    CLS.InPhieu._setParam_ChiDinh_TNTL(_data, frm40, _inMauCD, _Mabn, idCLS);
                                }

                            }
                            else
                                CLS.InPhieu.InPhieu_TDCH(idCLS, TTN);
                            break;
                        case "Nội soi Tai-Mũi-Họng":
                            using (frmIn frm41 = new frmIn())
                            {
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm41, _inMauCD, _Mabn, idCLS, TTN);
                            }
                            break;
                        case "Phẫu Thuật":
                            using (frmIn frm411 = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "30303")
                                    CLS.InPhieu._setParam_ChiDinh_TTPT_30303(_data, _Mabn, idCLS);
                                else
                                    CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm411, _inMauCD, _Mabn, idCLS, TTN);
                            }
                            break;
                        case "Thủ thuật":
                            using (frmIn frm412 = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "30303")
                                    CLS.InPhieu._setParam_ChiDinh_TTPT_30303(_data, _Mabn, idCLS);
                                else
                                    CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm412, _inMauCD, _Mabn, idCLS, TTN);
                            }
                            break;
                        case "Điện não đồ":
                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                goto default;
                            else
                            {
                                using (frmIn frm4121 = new frmIn())
                                {
                                    CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4121, _inMauCD, _Mabn, idCLS, TTN);
                                }
                            }
                            break;
                        case "Nội soi Cổ Tử Cung":
                            using (frmIn frm43 = new frmIn())
                            {
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm43, _inMauCD, _Mabn, idCLS, TTN);
                            }
                            break;
                        case "Nội soi Dạ Dày":
                            using (frmIn frm44 = new frmIn())
                            {
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm44, _inMauCD, _Mabn, idCLS, TTN);
                            }
                            break;
                        case "Siêu âm":
                            using (frmIn frm45 = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "30372")
                                {
                                    CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm45, "A4", _Mabn, idCLS, TTN);
                                }
                                else
                                {
                                    goto default;
                                }
                            }
                            break;
                        case "X-Quang":
                            using (frmIn frm5 = new frmIn())
                            {
                                if (DungChung.Bien.MaBV == "30303")
                                    CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm5, "A4", _Mabn, idCLS, TTN);
                                else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                {
                                    goto default;
                                }
                                else
                                {
                                    using (frmIn frm4 = new frmIn())
                                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4, _inMauCD, _Mabn, idCLS, TTN);
                                }
                            }
                            break;
                        default:
                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                            {
                                CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                            }
                            else
                            {
                                using (frmIn frm4 = new frmIn())
                                {
                                    CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4, _inMauCD, _Mabn, idCLS, TTN);
                                }
                            }
                            break;
                    }
                    QLBV.Utilities.Helppers.MemoryManagement.FlushMemory();
                }
                return;
            }
            if (mauphieu == 2)
            {
                if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
                {
                    int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                    string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();

                    if (TTN == "X-Quang" || TTN == "Siêu âm" || TTN == "Điện tim" || TTN == "Nội soi" || TTN == "Nội soi Tai-Mũi-Họng" || TTN == "Thủ thuật" || TTN == "Phẫu thuật" || TTN == "Siêu âm ( Doppler )" || TTN == "X-Quang CT" || TTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiDaDay || TTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiCoTuCung)
                    {

                        if (DungChung.Bien.MaBV != "30009")
                        {
                            DialogResult _result = DialogResult.No;
                            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "24297")
                                _result = MessageBox.Show("In chỉ định tổng hợp theo nhóm CĐHA?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (_result == DialogResult.Yes)
                            {
                                using (frmIn frm = new frmIn())
                                {
                                    BaoCao.Rep_PhieuTHX_Quang_12345 rep41 = new Rep_PhieuTHX_Quang_12345();
                                    BaoCao.Rep_PhieuTHX_Quang rep = new BaoCao.Rep_PhieuTHX_Quang();
                                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                                        CLS.InPhieu._setParamInChiDinhTH_CoDonGia(_data, frm, rep41, _Mabn, "");
                                    else if (DungChung.Bien.MaBV == "14017")
                                    {
                                        int ID_CDHA = 2;
                                        string TenPhieu = "PHIẾU CHỈ ĐỊNH CĐHA";
                                        CLS.InPhieu._setParamInChiDinh_XN_SL(_data, frm, _inMauCD, _Mabn, idCLS, true, "", ID_CDHA, TenPhieu);
                                    }
                                    else
                                        CLS.InPhieu._setParamInChiDinhTH(_data, frm, rep, _Mabn, "");
                                }
                            }
                            else
                            {
                                using (frmIn frm4 = new frmIn())
                                {
                                    BaoCao.Rep_PhieuTHX_Quang_12345 rep41 = new Rep_PhieuTHX_Quang_12345();
                                    BaoCao.Rep_PhieuTHX_Quang rep4 = new BaoCao.Rep_PhieuTHX_Quang();
                                    if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                                        CLS.InPhieu._setParamInChiDinhTH_CoDonGia(_data, frm4, rep41, _Mabn, TTN);

                                    else
                                        CLS.InPhieu._setParamInChiDinhTH(_data, frm4, rep4, _Mabn, TTN);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chức năng này không ứng dụng tại đơn vị");
                        }
                    }
                    else if (TTN == "Trắc nghiệm tâm lý" && DungChung.Bien.MaBV == "34019")
                    {
                        using (frmIn frm12 = new frmIn())
                        {
                            CLS.InPhieu._setParam_ChiDinh_TNTL(_data, frm12, _inMauCD, _Mabn, idCLS);
                        }
                    }
                    else if (TTN == "Thăm dò chức năng")
                    {
                        using (frmIn frmxn = new frmIn())
                        {
                            int ID_ThamDo = 3;
                            string TenPhieu = "PHIẾU CHỈ ĐỊNH THĂM GIÒ CHỨC NĂNG";
                            CLS.InPhieu._setParamInChiDinh_XN_SL(_data, frmxn, _inMauCD, _Mabn, idCLS, true, "", ID_ThamDo, TenPhieu);
                        }
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "14017")
                        {
                            using (frmIn frmxn = new frmIn())
                            {
                                int ID_XN = 1;
                                string TenPhieu = "PHIẾU CHỈ ĐỊNH XÉT NGHIỆM";
                                CLS.InPhieu._setParamInChiDinh_XN_SL(_data, frmxn, _inMauCD, _Mabn, idCLS, true, "", ID_XN, TenPhieu);
                            }
                        }
                        else if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "30372")
                        {
                            using (frmIn frm12 = new frmIn())
                            {
                                BaoCao.repPhieuChiDinh_XetNghiem rep12 = new BaoCao.repPhieuChiDinh_XetNghiem(true);
                                CLS.InPhieu._setParamInChiDinh_XN_24012(_data, frm12, _inMauCD, _Mabn, idCLS, _lIdCLS, true, "");
                            }
                        }
                        else
                        {
                            using (frmIn frm12 = new frmIn())
                            {
                                BaoCao.repPhieuChiDinh_XetNghiem rep12 = new BaoCao.repPhieuChiDinh_XetNghiem(true);
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm12, _inMauCD, _Mabn, idCLS, true, "");
                            }
                        }

                    }
                }
                return;
            }
            #endregion
        }
        void inchidinh_30372(int mauphieu)
        {
            List<int> _lIdCLS = new List<int>();
            List<int?> _lMaDV = new List<int?>();
            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            if (dataSource != null)
            {
                _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0).Select(o => o.IdCLS).ToList();
                _lMaDV = dataSource.Where(o => o.Check && o.MaDV > 0).Select(o => o.MaDV).Distinct().ToList();
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();
            #region chi dịnh
            string _inMauCD = "A5";
            if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "14017")
                _inMauCD = "A4";

            if (mauphieu == 1)  //---------------------------------------------------- IN chỉ định--------------------------------------------------
            {
                if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
                {

                    int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                    string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();
                    _tendv = GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString();
                    int status = 0;
                    var cls = _data.CLS.Where(p => p.IdCLS == idCLS).ToList();
                    if (cls.Count > 0)
                        status = cls.First().Status;
                    switch (TTN)
                    {

                        case "XN dịch chọc dò":
                            frmIn frmcd = new frmIn();
                            if (DungChung.Bien.MaBV == "30372")
                                CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frmcd, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                            else
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frmcd, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            break;

                        case "XN hóa sinh máu":
                            if (DungChung.Bien.MaBV == "26007")
                                _inMauCD = "A4";
                            frmIn frm = new frmIn();
                            if (DungChung.Bien.MaBV == "30303")
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm, "A4", _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            else if (DungChung.Bien.MaBV == "30372")
                                CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frm, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                            else
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);

                            //}
                            break;
                        case "XN huyết học":
                            #region Phieu chi linh

                            #endregion
                            //else
                            //{
                            if (DungChung.Bien.MaBV == "26007")
                                _inMauCD = "A4";
                            frmIn frm1 = new frmIn();
                            if (DungChung.Bien.MaBV == "30303")
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm1, "A4", _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            else if (DungChung.Bien.MaBV == "30372")
                                CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frm1, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                            else
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm1, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            //}
                            break;
                        case "XN nước tiểu":
                            if (DungChung.Bien.MaBV == "26007")
                                _inMauCD = "A4";
                            frmIn frm2 = new frmIn();
                            if (DungChung.Bien.MaBV == "30372")
                                CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frm2, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                            else
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm2, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            break;
                        //}
                        case "XN vi sinh":
                            frmIn frm22 = new frmIn();
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                if ((GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SAR-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Realtime PCR SARS-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SARS-CoV-2") && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"))
                                    CLS.InPhieu._InPhieu_XetNghiem(_data, GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString(), _Mabn, Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS")), 1, _lIdCLS, _lMaDV);
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frm22, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                            }
                            else
                            {
                                if ((GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SAR-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Realtime PCR SARS-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SARS-CoV-2") && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"))
                                    CLS.InPhieu._InPhieu_XetNghiem(_data, GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString(), _Mabn, Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS")), 1, _lIdCLS, _lMaDV);
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm22, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            }
                            break;
                        case "XN hóa sinh miễn dịch":
                            frmIn frm212 = new frmIn();
                            if (DungChung.Bien.MaBV == "30372")
                                CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frm212, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                            else
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm212, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            break;
                        case "XN Đông cầm máu":
                            frmIn frm21 = new frmIn();
                            if (DungChung.Bien.MaBV == "30372")
                                CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frm21, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                            else
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frm21, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            break;
                        case "X-Quang CT":
                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                goto default;
                            else
                            {
                                frmIn frm31 = new frmIn();
                                if (DungChung.Bien.MaBV == "30303")
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm31, "A4", _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm31, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            }
                            break;
                        case "XN khác":

                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                            {
                                frmIn frm211 = new frmIn();
                                CLS.InPhieu._InPhieu_XetNghiem(_data, TTN, _Mabn, idCLS, 0, _lIdCLS, _lMaDV);
                                break;
                            }
                            else
                            {
                                frmIn frm3 = new frmIn();
                                if (DungChung.Bien.MaBV == "30372")
                                    CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frm3, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                                else
                                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm3, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                                break;
                            }
                        //}
                        case "XN Ung thư":
                            frmIn frmut = new frmIn();
                            if (DungChung.Bien.MaBV == "30372")
                                CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frmut, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                            else
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frmut, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            break;
                        case "XN Nồng độ cồn":
                            _InPhieuCDNongDoCon(_data, _Mabn, idCLS);
                            break;
                        case "XN hóa sinh nội tiết tố":
                            frmIn frmnt = new frmIn();
                            if (DungChung.Bien.MaBV == "30372")
                                CLS.InPhieu._setParamInChiDinh_XN_30372(_data, frmnt, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV, 0, TTN);
                            else
                                CLS.InPhieu._setParamInChiDinh_XN(_data, frmnt, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            break;
                        case "Trắc nghiệm tâm lý":
                            if (DungChung.Bien.MaBV == "34019")
                            {
                                frmIn frm40 = new frmIn();
                                CLS.InPhieu._setParam_ChiDinh_TNTL(_data, frm40, _inMauCD, _Mabn, idCLS);
                            }
                            else
                                CLS.InPhieu.InPhieu_TDCH(idCLS, TTN);
                            break;
                        case "Nội soi Tai-Mũi-Họng":
                            frmIn frm41 = new frmIn();
                            CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm41, _inMauCD, _Mabn, idCLS, TTN);
                            break;
                        case "Phẫu Thuật":

                            frmIn frm411 = new frmIn();
                            if (DungChung.Bien.MaBV == "30303")
                                CLS.InPhieu._setParam_ChiDinh_TTPT_30303(_data, _Mabn, idCLS);
                            else
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm411, _inMauCD, _Mabn, idCLS, TTN);
                            break;
                        case "Thủ thuật":
                            frmIn frm412 = new frmIn();
                            if (DungChung.Bien.MaBV == "30303")
                                CLS.InPhieu._setParam_ChiDinh_TTPT_30303(_data, _Mabn, idCLS);
                            else
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm412, _inMauCD, _Mabn, idCLS, TTN);
                            break;
                        case "Điện não đồ":
                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                                goto default;
                            else
                            {
                                frmIn frm4121 = new frmIn();
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4121, _inMauCD, _Mabn, idCLS, TTN);
                            }
                            break;
                        case "Nội soi Cổ Tử Cung":
                            frmIn frm43 = new frmIn();
                            CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm43, _inMauCD, _Mabn, idCLS, TTN);
                            break;
                        case "Nội soi Dạ Dày":
                            frmIn frm44 = new frmIn();
                            CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm44, _inMauCD, _Mabn, idCLS, TTN);
                            break;
                        case "Siêu âm":
                            frmIn frm45 = new frmIn();
                            if (DungChung.Bien.MaBV == "30372")
                            {
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm45, "A4", _Mabn, idCLS, TTN);
                            }
                            else
                            {
                                goto default;
                            }

                            break;
                        case "X-Quang":
                            frmIn frm5 = new frmIn();
                            if (DungChung.Bien.MaBV == "30303")
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm5, "A4", _Mabn, idCLS, TTN);
                            else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                            {
                                goto default;
                            }
                            else
                            {
                                frmIn frm4 = new frmIn();
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4, _inMauCD, _Mabn, idCLS, TTN);
                            }
                            break;
                        default:
                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                            {
                                CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                            }
                            else
                            {
                                frmIn frm4 = new frmIn();
                                CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4, _inMauCD, _Mabn, idCLS, TTN);
                            }
                            break;
                    }
                }
                return;
            }
            if (mauphieu == 2)
            {
                if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
                {
                    int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                    string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();

                    if (TTN == "X-Quang" || TTN == "Siêu âm" || TTN == "Điện tim" || TTN == "Nội soi" || TTN == "Nội soi Tai-Mũi-Họng" || TTN == "Thủ thuật" || TTN == "Phẫu thuật" || TTN == "Siêu âm ( Doppler )" || TTN == "X-Quang CT" || TTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiDaDay || TTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiCoTuCung)
                    {

                        if (DungChung.Bien.MaBV != "30009")
                        {
                            DialogResult _result = DialogResult.No;
                            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "24297")
                                _result = MessageBox.Show("In chỉ định tổng hợp theo nhóm CĐHA?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (_result == DialogResult.Yes)
                            {
                                frmIn frm = new frmIn();
                                BaoCao.Rep_PhieuTHX_Quang_12345 rep41 = new Rep_PhieuTHX_Quang_12345();
                                BaoCao.Rep_PhieuTHX_Quang rep = new BaoCao.Rep_PhieuTHX_Quang();
                                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                                    CLS.InPhieu._setParamInChiDinhTH_CoDonGia(_data, frm, rep41, _Mabn, "");
                                else if (DungChung.Bien.MaBV == "14017")
                                {
                                    int ID_CDHA = 2;
                                    string TenPhieu = "PHIẾU CHỈ ĐỊNH CĐHA";
                                    CLS.InPhieu._setParamInChiDinh_XN_SL(_data, frm, _inMauCD, _Mabn, idCLS, true, "", ID_CDHA, TenPhieu);
                                }
                                else
                                    CLS.InPhieu._setParamInChiDinhTH(_data, frm, rep, _Mabn, "");

                            }//
                            else
                            {
                                frmIn frm4 = new frmIn();
                                BaoCao.Rep_PhieuTHX_Quang_12345 rep41 = new Rep_PhieuTHX_Quang_12345();
                                BaoCao.Rep_PhieuTHX_Quang rep4 = new BaoCao.Rep_PhieuTHX_Quang();
                                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                                    CLS.InPhieu._setParamInChiDinhTH_CoDonGia(_data, frm4, rep41, _Mabn, TTN);

                                else
                                    CLS.InPhieu._setParamInChiDinhTH(_data, frm4, rep4, _Mabn, TTN);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Chức năng này không ứng dụng tại đơn vị");
                        }
                    }
                    else if (TTN == "Trắc nghiệm tâm lý" && DungChung.Bien.MaBV == "34019")
                    {
                        frmIn frm12 = new frmIn();
                        CLS.InPhieu._setParam_ChiDinh_TNTL(_data, frm12, _inMauCD, _Mabn, idCLS);
                    }
                    else if (TTN == "Thăm dò chức năng")
                    {
                        frmIn frmxn = new frmIn();
                        int ID_ThamDo = 3;
                        string TenPhieu = "PHIẾU CHỈ ĐỊNH THĂM GIÒ CHỨC NĂNG";
                        CLS.InPhieu._setParamInChiDinh_XN_SL(_data, frmxn, _inMauCD, _Mabn, idCLS, true, "", ID_ThamDo, TenPhieu);
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "14017")
                        {
                            frmIn frmxn = new frmIn();
                            int ID_XN = 1;
                            string TenPhieu = "PHIẾU CHỈ ĐỊNH XÉT NGHIỆM";
                            CLS.InPhieu._setParamInChiDinh_XN_SL(_data, frmxn, _inMauCD, _Mabn, idCLS, true, "", ID_XN, TenPhieu);
                        }
                        else
                        {
                            frmIn frm12 = new frmIn();
                            BaoCao.repPhieuChiDinh_XetNghiem rep12 = new BaoCao.repPhieuChiDinh_XetNghiem(true);
                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm12, _inMauCD, _Mabn, idCLS, true, "", _lIdCLS, _lMaDV);
                        }

                    }
                }
                return;
            }
            #endregion
        }

        //void inchidinh(int mauphieu, List<int> lstMadv)
        //{
        //    #region chi dịnh
        //    string _inMauCD = "A5";
        //    if (DungChung.Bien.MaBV == "30009" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "14017")
        //        _inMauCD = "A4";

        //    if (mauphieu == 1)  //---------------------------------------------------- IN chỉ định--------------------------------------------------
        //    {
        //        if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
        //        {

        //            int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
        //            string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();
        //            _tendv = GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString();
        //            int status = 0;
        //            var cls = _data.CLS.Where(p => p.IdCLS == idCLS).ToList();
        //            if (cls.Count > 0)
        //                status = cls.First().Status;
        //            switch (TTN)
        //            {

        //                case "XN dịch chọc dò":
        //                    frmIn frmcd = new frmIn();
        //                    CLS.InPhieu._setParamInChiDinh_XN(_data, frmcd, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                    break;

        //                case "XN hóa sinh máu":
        //                    if (DungChung.Bien.MaBV == "26007")
        //                        _inMauCD = "A4";
        //                    frmIn frm = new frmIn();
        //                    if (DungChung.Bien.MaBV == "30303")
        //                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm, "A4", _Mabn, idCLS, false, "", madv);
        //                    else
        //                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm, _inMauCD, _Mabn, idCLS, false, "", madv);

        //                    //}
        //                    break;
        //                case "XN huyết học":
        //                    #region Phieu chi linh

        //                    #endregion
        //                    //else
        //                    //{
        //                    if (DungChung.Bien.MaBV == "26007")
        //                        _inMauCD = "A4";
        //                    frmIn frm1 = new frmIn();
        //                    if (DungChung.Bien.MaBV == "30303")
        //                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm1, "A4", _Mabn, idCLS, false, "", madv);
        //                    else
        //                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm1, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                    //}
        //                    break;
        //                case "XN nước tiểu":
        //                    if (DungChung.Bien.MaBV == "26007")
        //                        _inMauCD = "A4";
        //                    frmIn frm2 = new frmIn();
        //                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm2, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                    break;
        //                //}
        //                case "XN vi sinh":
        //                    frmIn frm22 = new frmIn();
        //                    if ((GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SAR-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Realtime PCR SARS-CoV-2" || GrvNhomDV.GetFocusedRowCellValue(colTenDVcd).ToString() == "Test nhanh kháng nguyên vi rút SARS-CoV-2") && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"))
        //                        CLS.InPhieu._InPhieu_XetNghiem(_data, GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString(), _Mabn, Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS")), 1, madv);
        //                    else
        //                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm22, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                    break;
        //                case "XN hóa sinh miễn dịch":
        //                    frmIn frm212 = new frmIn();
        //                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm212, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                    break;
        //                case "XN Đông cầm máu":
        //                    frmIn frm21 = new frmIn();
        //                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm21, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                    break;
        //                case "X-Quang CT":
        //                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
        //                        goto default;
        //                    else
        //                    {
        //                        frmIn frm31 = new frmIn();
        //                        if (DungChung.Bien.MaBV == "30303")
        //                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm31, "A4", _Mabn, idCLS, false, "", madv);
        //                        else
        //                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm31, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                    }
        //                    break;
        //                case "XN khác":

        //                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
        //                    {
        //                        frmIn frm211 = new frmIn();
        //                        CLS.InPhieu._InPhieu_XetNghiem(_data, TTN, _Mabn, idCLS, 0, madv);
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        frmIn frm3 = new frmIn();
        //                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm3, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                        break;
        //                    }
        //                //}
        //                case "XN Ung thư":
        //                    frmIn frmut = new frmIn();
        //                    CLS.InPhieu._setParamInChiDinh_XN(_data, frmut, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                    break;
        //                case "XN Nồng độ cồn":
        //                    _InPhieuCDNongDoCon(_data, _Mabn, idCLS);
        //                    break;
        //                case "XN hóa sinh nội tiết tố":
        //                    frmIn frmnt = new frmIn();
        //                    CLS.InPhieu._setParamInChiDinh_XN(_data, frmnt, _inMauCD, _Mabn, idCLS, false, "", madv);
        //                    break;
        //                case "Trắc nghiệm tâm lý":
        //                    if (DungChung.Bien.MaBV == "34019")
        //                    {
        //                        frmIn frm40 = new frmIn();
        //                        CLS.InPhieu._setParam_ChiDinh_TNTL(_data, frm40, _inMauCD, _Mabn, idCLS);
        //                    }
        //                    else
        //                        CLS.InPhieu.InPhieu_TDCH(idCLS, TTN);
        //                    break;
        //                case "Nội soi Tai-Mũi-Họng":
        //                    frmIn frm41 = new frmIn();
        //                    CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm41, _inMauCD, _Mabn, idCLS, TTN);
        //                    break;
        //                case "Phẫu Thuật":

        //                    frmIn frm411 = new frmIn();
        //                    if (DungChung.Bien.MaBV == "30303")
        //                        CLS.InPhieu._setParam_ChiDinh_TTPT_30303(_data, _Mabn, idCLS);
        //                    else
        //                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm411, _inMauCD, _Mabn, idCLS, TTN);
        //                    break;
        //                case "Thủ thuật":
        //                    frmIn frm412 = new frmIn();
        //                    if (DungChung.Bien.MaBV == "30303")
        //                        CLS.InPhieu._setParam_ChiDinh_TTPT_30303(_data, _Mabn, idCLS);
        //                    else
        //                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm412, _inMauCD, _Mabn, idCLS, TTN);
        //                    break;
        //                case "Điện não đồ":
        //                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
        //                        goto default;
        //                    else
        //                    {
        //                        frmIn frm4121 = new frmIn();
        //                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4121, _inMauCD, _Mabn, idCLS, TTN);
        //                    }
        //                    break;
        //                case "Nội soi Cổ Tử Cung":
        //                    frmIn frm43 = new frmIn();
        //                    CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm43, _inMauCD, _Mabn, idCLS, TTN);
        //                    break;
        //                case "Nội soi Dạ Dày":
        //                    frmIn frm44 = new frmIn();
        //                    CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm44, _inMauCD, _Mabn, idCLS, TTN);
        //                    break;
        //                case "Siêu âm":
        //                    frmIn frm45 = new frmIn();
        //                    if (DungChung.Bien.MaBV == "30372")
        //                    {
        //                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm45, "A4", _Mabn, idCLS, TTN);
        //                    }
        //                    else
        //                    {
        //                        goto default;
        //                    }

        //                    break;
        //                case "X-Quang":
        //                    frmIn frm5 = new frmIn();
        //                    if (DungChung.Bien.MaBV == "30303")
        //                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm5, "A4", _Mabn, idCLS, TTN);
        //                    else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
        //                    {
        //                        goto default;
        //                    }
        //                    else
        //                    {
        //                        frmIn frm4 = new frmIn();
        //                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4, _inMauCD, _Mabn, idCLS, TTN);
        //                    }
        //                    break;
        //                default:
        //                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
        //                    {
        //                        CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
        //                    }
        //                    else
        //                    {
        //                        frmIn frm4 = new frmIn();
        //                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4, _inMauCD, _Mabn, idCLS, TTN);
        //                    }
        //                    break;
        //            }
        //        }
        //        return;
        //    }
        //    if (mauphieu == 2)
        //    {
        //        if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
        //        {
        //            int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
        //            string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();

        //            if (TTN == "X-Quang" || TTN == "Siêu âm" || TTN == "Điện tim" || TTN == "Nội soi" || TTN == "Nội soi Tai-Mũi-Họng" || TTN == "Thủ thuật" || TTN == "Phẫu thuật" || TTN == "Siêu âm ( Doppler )" || TTN == "X-Quang CT" || TTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiDaDay || TTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoiCoTuCung)
        //            {

        //                if (DungChung.Bien.MaBV != "30009")
        //                {
        //                    DialogResult _result = DialogResult.No;
        //                    if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "24297")
        //                        _result = MessageBox.Show("In chỉ định tổng hợp theo nhóm CĐHA?", "Hỏi in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //                    if (_result == DialogResult.Yes)
        //                    {
        //                        frmIn frm = new frmIn();
        //                        BaoCao.Rep_PhieuTHX_Quang_12345 rep41 = new Rep_PhieuTHX_Quang_12345();
        //                        BaoCao.Rep_PhieuTHX_Quang rep = new BaoCao.Rep_PhieuTHX_Quang();
        //                        if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
        //                            CLS.InPhieu._setParamInChiDinhTH_CoDonGia(_data, frm, rep41, _Mabn, "");
        //                        else if (DungChung.Bien.MaBV == "14017")
        //                        {
        //                            int ID_CDHA = 2;
        //                            string TenPhieu = "PHIẾU CHỈ ĐỊNH CĐHA";
        //                            CLS.InPhieu._setParamInChiDinh_XN_SL(_data, frm, _inMauCD, _Mabn, idCLS, true, "", ID_CDHA, TenPhieu);
        //                        }
        //                        else
        //                            CLS.InPhieu._setParamInChiDinhTH(_data, frm, rep, _Mabn, "");

        //                    }//
        //                    else
        //                    {
        //                        frmIn frm4 = new frmIn();
        //                        BaoCao.Rep_PhieuTHX_Quang_12345 rep41 = new Rep_PhieuTHX_Quang_12345();
        //                        BaoCao.Rep_PhieuTHX_Quang rep4 = new BaoCao.Rep_PhieuTHX_Quang();
        //                        if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
        //                            CLS.InPhieu._setParamInChiDinhTH_CoDonGia(_data, frm4, rep41, _Mabn, TTN);

        //                        else
        //                            CLS.InPhieu._setParamInChiDinhTH(_data, frm4, rep4, _Mabn, TTN);

        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Chức năng này không ứng dụng tại đơn vị");
        //                }
        //            }
        //            else if (TTN == "Trắc nghiệm tâm lý" && DungChung.Bien.MaBV == "34019")
        //            {
        //                frmIn frm12 = new frmIn();
        //                CLS.InPhieu._setParam_ChiDinh_TNTL(_data, frm12, _inMauCD, _Mabn, idCLS);
        //            }
        //            else if (TTN == "Thăm dò chức năng")
        //            {
        //                frmIn frmxn = new frmIn();
        //                int ID_ThamDo = 3;
        //                string TenPhieu = "PHIẾU CHỈ ĐỊNH THĂM GIÒ CHỨC NĂNG";
        //                CLS.InPhieu._setParamInChiDinh_XN_SL(_data, frmxn, _inMauCD, _Mabn, idCLS, true, "", ID_ThamDo, TenPhieu);
        //            }
        //            else
        //            {
        //                if (DungChung.Bien.MaBV == "14017")
        //                {
        //                    frmIn frmxn = new frmIn();
        //                    int ID_XN = 1;
        //                    string TenPhieu = "PHIẾU CHỈ ĐỊNH XÉT NGHIỆM";
        //                    CLS.InPhieu._setParamInChiDinh_XN_SL(_data, frmxn, _inMauCD, _Mabn, idCLS, true, "", ID_XN, TenPhieu);
        //                }
        //                else
        //                {
        //                    frmIn frm12 = new frmIn();
        //                    BaoCao.repPhieuChiDinh_XetNghiem rep12 = new BaoCao.repPhieuChiDinh_XetNghiem(true);
        //                    CLS.InPhieu._setParamInChiDinh_XN(_data, frm12, _inMauCD, _Mabn, idCLS, true, "", madv);
        //                }

        //            }
        //        }
        //        return;
        //    }
        //    #endregion
        //}

        void inchidinh(string tenRG, int idCLS)
        {
            List<int> _lIdCLS = new List<int>();
            List<int?> _lMaDV = new List<int?>();
            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            if (dataSource != null)
            {
                _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0).Select(o => o.IdCLS).ToList();
                _lMaDV = dataSource.Where(o => o.Check && o.MaDV > 0).Select(o => o.MaDV).Distinct().ToList();
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();
            #region chi dịnh
            var _inMauCD = "A4";
            if (!string.IsNullOrWhiteSpace(tenRG))
            {
                int madv = 0;
                if (GrvNhomDV.GetFocusedRowCellValue(colMaDV_NhomDV) != null)
                {
                    madv = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colMaDV_NhomDV));
                }
                string TTN = tenRG;
                switch (TTN)
                {

                    case "XN dịch chọc dò":
                        frmIn frmcd = new frmIn();
                        CLS.InPhieu._setParamInChiDinh_XN(_data, frmcd, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        break;

                    case "XN hóa sinh máu":
                        if (DungChung.Bien.MaBV == "26007")
                            _inMauCD = "A4";
                        frmIn frm = new frmIn();
                        if (DungChung.Bien.MaBV == "30303")
                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm, "A4", _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        else
                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);

                        //}
                        break;
                    case "XN huyết học":
                        #region Phieu chi linh

                        #endregion
                        //else
                        //{
                        if (DungChung.Bien.MaBV == "26007")
                            _inMauCD = "A4";
                        frmIn frm1 = new frmIn();
                        if (DungChung.Bien.MaBV == "30303")
                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm1, "A4", _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        else
                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm1, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        //}
                        break;
                    case "XN nước tiểu":
                        //if (DungChung.Bien.MaBV == "12122")
                        //{
                        //    frmIn frm211 = new frmIn();
                        //    CLS.InPhieu._InPhieu_XetNghiem(_data, TTN, _Mabn, idCLS);
                        //    break;
                        //}
                        //else
                        //{
                        if (DungChung.Bien.MaBV == "26007")
                            _inMauCD = "A4";
                        frmIn frm2 = new frmIn();
                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm2, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        break;
                    //}
                    case "XN vi sinh":
                        frmIn frm22 = new frmIn();
                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm22, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        break;
                    case "XN hóa sinh miễn dịch":
                        frmIn frm212 = new frmIn();
                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm212, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        break;
                    case "XN Đông cầm máu":
                        frmIn frm21 = new frmIn();
                        CLS.InPhieu._setParamInChiDinh_XN(_data, frm21, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        break;
                    case "X-Quang CT":
                        frmIn frm31 = new frmIn();
                        if (DungChung.Bien.MaBV == "30303")
                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm31, "A4", _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        else
                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm31, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        break;
                    case "XN khác":

                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                        {
                            frmIn frm211 = new frmIn();
                            CLS.InPhieu._InPhieu_XetNghiem(_data, TTN, _Mabn, idCLS, 0, _lIdCLS, _lMaDV);
                            break;
                        }
                        else
                        {
                            frmIn frm3 = new frmIn();
                            CLS.InPhieu._setParamInChiDinh_XN(_data, frm3, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                            break;
                        }
                    //}
                    case "XN Ung thư":
                        frmIn frmut = new frmIn();
                        CLS.InPhieu._setParamInChiDinh_XN(_data, frmut, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        break;
                    case "XN Nồng độ cồn":
                        _InPhieuCDNongDoCon(_data, _Mabn, idCLS);
                        break;
                    case "XN hóa sinh nội tiết tố":
                        frmIn frmnt = new frmIn();
                        CLS.InPhieu._setParamInChiDinh_XN(_data, frmnt, _inMauCD, _Mabn, idCLS, false, "", _lIdCLS, _lMaDV);
                        break;
                    case "Trắc nghiệm tâm lý":
                        if (DungChung.Bien.MaBV == "34019")
                        {
                            frmIn frm40 = new frmIn();
                            CLS.InPhieu._setParam_ChiDinh_TNTL(_data, frm40, _inMauCD, _Mabn, idCLS);
                        }
                        else
                            CLS.InPhieu.InPhieu_TDCH(idCLS, TTN);
                        break;
                    case "Nội soi Tai-Mũi-Họng":
                        frmIn frm41 = new frmIn();
                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm41, _inMauCD, _Mabn, idCLS, TTN);
                        break;
                    case "Phẫu Thuật":

                        frmIn frm411 = new frmIn();
                        if (DungChung.Bien.MaBV == "30303")
                            CLS.InPhieu._setParam_ChiDinh_TTPT_30303(_data, _Mabn, idCLS);
                        else
                            CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm411, _inMauCD, _Mabn, idCLS, TTN);
                        break;
                    case "Thủ thuật":
                        frmIn frm412 = new frmIn();
                        if (DungChung.Bien.MaBV == "30303")
                            CLS.InPhieu._setParam_ChiDinh_TTPT_30303(_data, _Mabn, idCLS);
                        else
                            CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm412, _inMauCD, _Mabn, idCLS, TTN);
                        break;
                    case "Điện não đồ":
                        frmIn frm4121 = new frmIn();
                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4121, _inMauCD, _Mabn, idCLS, TTN);
                        break;
                    case "Nội soi Cổ Tử Cung":
                        frmIn frm43 = new frmIn();
                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm43, _inMauCD, _Mabn, idCLS, TTN);
                        break;
                    case "Nội soi Dạ Dày":
                        frmIn frm44 = new frmIn();
                        CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm44, _inMauCD, _Mabn, idCLS, TTN);
                        break;
                    case "X-Quang":
                        frmIn frm5 = new frmIn();
                        if (DungChung.Bien.MaBV == "30303")
                            CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm5, "A4", _Mabn, idCLS, TTN);
                        else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                        {
                            CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                        }
                        else if (DungChung.Bien.MaBV == "01049")
                        {
                            CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                        }
                        else
                        {
                            frmIn frm4 = new frmIn();
                            CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4, _inMauCD, _Mabn, idCLS, TTN);
                        }
                        break;
                    default:
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                        {
                            CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                        }
                        else
                        {
                            frmIn frm4 = new frmIn();
                            CLS.InPhieu._setParam_ChiDinh_CDHA(_data, frm4, _inMauCD, _Mabn, idCLS, TTN);
                        }
                        break;
                }
            }
            return;
            #endregion
        }
        void _InPhieuCDNongDoCon(QLBV_Database.QLBVEntities _Data, int Mabn, int idcls)
        {
            frmIn frm32 = new frmIn();
            BaoCao.rep_PhieuKQXNNongDoConTrongMau rep32 = new BaoCao.rep_PhieuKQXNNongDoConTrongMau();
            var par32 = (from cls3 in _Data.CLS.Where(p => p.IdCLS == idcls)
                         join cdcls in _Data.ChiDinhs on cls3.IdCLS equals cdcls.IdCLS
                         join dv in _Data.DichVus on cdcls.MaDV equals dv.MaDV
                         select new { cls3.MaCB, cls3.TrangThaiBP, cls3.TrangThaiBN, cls3.MaCBLayMau, cls3.ThoiGianLayMau, cls3.MaCBth, cdcls.MaMay, cls3.NgayTH, cls3.NgayThang }).ToList();
            var _lcb = _Data.CanBoes.ToList();
            var ttbn = _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
            if (par32.Count > 0)
            {
                if (ttbn != null)
                {
                    rep32.HoTen.Value = ttbn.TenBNhan;
                    rep32.Tuoi.Value = ttbn.Tuoi.ToString();
                    rep32.GTinh.Value = ttbn.GTinh == 0 ? "Nữ" : "Nam";
                }
                rep32.TinhTrangMau.Value = par32.First().TrangThaiBP;
                rep32.TinhTrangBN.Value = par32.First().TrangThaiBN;
                if (par32.First().MaCB != null)
                {
                    string macb = par32.First().MaCB;
                    var tencb1 = _lcb.Where(p => p.MaCB == macb).FirstOrDefault();
                    if (tencb1 != null)
                        rep32.BSCD.Value = tencb1.TenCB.ToString();
                }
                if (par32.First().ThoiGianLayMau != null)
                    rep32.TimeLayMau.Value = par32.First().ThoiGianLayMau.Value.Hour + "Giờ, ngày " + par32.First().ThoiGianLayMau.Value.Day + " tháng " + par32.First().ThoiGianLayMau.Value.Month + " năm " + par32.First().ThoiGianLayMau.Value.Year;
                rep32.NgayGio.Value = par32.First().NgayThang.Value.Hour + "Giờ, ngày " + par32.First().NgayThang.Value.Day + " tháng " + par32.First().NgayThang.Value.Month + " năm " + par32.First().NgayThang.Value.Year;
                rep32.CreateDocument();
                frm32.prcIN.PrintingSystem = rep32.PrintingSystem;
                frm32.ShowDialog();
            }
        }

        #region xem phieu son la
        private bool phieuXNDongMau(QLBV_Database.QLBVEntities data, int idcls, string tenTNRG)
        {
            try
            {
                var bn = (from benhnhan in data.BenhNhans
                          join cls in data.CLS.Where(p => p.IdCLS == idcls) on benhnhan.MaBNhan equals cls.MaBNhan
                          select new { benhnhan.NNhap, benhnhan.MaBNhan, benhnhan.SThe, benhnhan.TenBNhan, benhnhan.Tuoi, benhnhan.GTinh, benhnhan.DChi, cls.NgayThang, cls.MaCB, cls.MaCBth, cls.NgayTH, cls.MaKP, cls.CapCuu, cls.BarCode, cls.BenhPham, cls.ThoiGianLayMau }).FirstOrDefault();
                if (bn != null)
                {
                    //int noichuyen = 3;// 1- nơi khác chuyển đến (MaBV != null), 2- Y tế tư (CDNoiGT != null) , 3-Tự đến
                    string ngaythang = "";
                    int stt = 1;
                    BaoCao.rep_XNDongMau rep = new BaoCao.rep_XNDongMau();
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    {
                        rep.txtBarcode.Text = bn.BarCode;
                        rep.lblBarcode.Visible = true;
                        rep.txtBarcode.Visible = true;
                    }

                    //khoa phòng chỉ định
                    var khoa = data.KPhongs.Where(p => p.MaKP == bn.MaKP).FirstOrDefault();
                    if (khoa != null && khoa.TenKP != null)
                        rep.celKhoa.Text = "Khoa: " + khoa.TenKP;

                    //thông tin chung
                    rep.celHoTen.Text = bn.TenBNhan;
                    rep.celCQCQ.Text = rep.cqcqSL.Text = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.celBenhpham.Text = "Bệnh phẩm: " + bn.BenhPham;
                    rep.celNgayLay.Text = "Ngày lấy mẫu: " + bn.ThoiGianLayMau.ToString();
                    if (bn.ThoiGianLayMau != null)
                    {
                        rep.celNgayLay.Text = "Ngày lấy mẫu: " + bn.NgayThang.Value.Day + " / " + bn.NgayThang.Value.Month + " / " + bn.NgayThang.Value.Year;

                    }
                    if (DungChung.Bien.MaBV == "14017")
                    {
                        List<BNDT> bndt = new List<BNDT>();
                        List<BNDT> abc = (from bnkm in _data.BNKBs.Where(p => p.MaBNhan == _Mabn)
                                          .OrderByDescending(p => p.IDKB)
                                          select new BNDT
                                          {
                                              IDKB = bnkm.IDKB,
                                              MaKP = bnkm.MaKP
                                          }).ToList();
                        bndt.AddRange(abc);

                        int idkb = bndt.First().IDKB;


                        string[] _MaICDarr = DungChung.Ham.getMaICDarrFull_SL(_data, _Mabn, DungChung.Bien.GetICD, idkb);
                        string[] icd = _MaICDarr[0].Split(';');
                        string[] tenicd = _MaICDarr[1].Split(';');
                        string lydo = "";
                        if (icd.Length <= tenicd.Length)
                        {
                            for (int i = 0; i < icd.Length; i++)
                            {
                                lydo += " " + icd[i] + "-" + tenicd[i] + ";";
                            }
                            if (icd.Length < tenicd.Length)
                            {
                                int cut1 = tenicd.Length - icd.Length;
                                int cut = tenicd.Length - cut1;
                                string mab1k = DungChung.Ham.FreshString(string.Join(";", tenicd.Skip(cut)));
                                lydo += " " + mab1k + ";";
                            }
                        }
                        else
                        {
                            for (int i = 0; i < tenicd.Length; i++)
                            {
                                lydo += " " + icd[i] + "-" + tenicd[i] + ";";
                            }
                        }

                        rep.celChanDoan.Text = "Chẩn đoán: " + DungChung.Ham.FreshString(lydo);
                    }
                    rep.celCQ.Text = rep.CQ.Text = DungChung.Bien.TenCQ.ToUpper();
                    rep.celDChi.Text = "Địa chỉ: " + bn.DChi;
                    if (DungChung.Bien.MaBV == "27023")
                    {
                        var qvv = data.VaoViens.Where(p => p.MaBNhan == bn.MaBNhan).FirstOrDefault();
                        if (qvv != null)
                            rep.celSo.Text = "Số: " + qvv.SoBA;
                        else
                            rep.celSo.Text = "Số: ";
                        if (bn.CapCuu == true)
                        {
                            rep.celCapcuu.Text = "x";
                            rep.celThuong.Text = "";
                        }
                        else
                        {

                            rep.celCapcuu.Text = "";
                            rep.celThuong.Text = "x";
                        }
                    }
                    else if (DungChung.Bien.MaBV == "14017")
                    {
                        rep.celMaBN.Text = "Mã bệnh nhân: " + bn.MaBNhan.ToString();
                    }
                    else
                        rep.celSo.Text = "Số: " + bn.MaBNhan.ToString();


                    //rep.celSoThe.Text = "Số thẻ BHYT: " + bn.SThe;
                    if (bn.SThe != null && bn.SThe.Length == 15)
                    {
                        rep.SThe1.Value = bn.SThe.Substring(0, 3);
                        rep.SThe2.Value = bn.SThe.Substring(3, 2);
                        rep.SThe3.Value = bn.SThe.Substring(5, 2);
                        rep.SThe4.Value = bn.SThe.Substring(7, 3);
                        rep.SThe5.Value = bn.SThe.Substring(10, 5);
                    }
                    rep.celTuoi.Text = bn.Tuoi == null ? "Tuổi:" : ("Tuổi: " + bn.Tuoi.ToString());
                    if (bn.GTinh == 1)
                        rep.celGTinh.Text = "Giới tính : Nam";
                    else
                        rep.celGTinh.Text = "Giới tính : Nữ";

                    // lấy thoongt in phòng, buồng giường
                    var qbnkb = data.BNKBs.Where(p => p.MaBNhan == bn.MaBNhan && p.MaKP == bn.MaKP).FirstOrDefault();
                    if (qbnkb != null)
                    {
                        if (DungChung.Bien.MaBV == "27023")
                        {
                            rep.celChanDoan.Text = "Chẩn đoán: " + qbnkb.ChanDoan;
                            rep.celBuong.Text = "số phòng: " + qbnkb.Buong;
                            rep.celGiuong.Text = "số giường: " + qbnkb.Giuong;
                        }
                        else if (DungChung.Bien.MaBV == "14017")
                        {
                            //rep.celChanDoan.Text = "Chẩn đoán: " + "(" + qbnkb.MaICD + ") " + qbnkb.ChanDoan;
                            rep.celBuong.Text = "Buồng: " + qbnkb.Buong;
                            rep.celGiuong.Text = "Giường: " + qbnkb.Giuong;
                        }
                        else
                        {
                            rep.celChanDoan.Text = "Chẩn đoán lâm sàng: " + qbnkb.ChanDoan;
                            rep.celBuong.Text = "Buồng: " + qbnkb.Buong;
                            rep.celGiuong.Text = "Giường: " + qbnkb.Giuong;
                        }
                    }

                    // ngày tháng, bác sỹ chỉ định và ngày tháng, bác sỹ thực hiện
                    if (bn.NgayThang != null)
                    {
                        rep.ngayyeucau.Text = "Ngày " + bn.NgayThang.Value.Day + " tháng " + bn.NgayThang.Value.Month + " năm " + bn.NgayThang.Value.Year;
                        ngaythang = bn.NgayThang.Value.ToString("dd/MM/yyyy");
                    }
                    if (bn.NgayTH != null)
                    {
                        rep.ngayth.Text = bn.NgayTH.Value.Hour + "h " + bn.NgayTH.Value.Minute + ", Ngày " + bn.NgayTH.Value.Day + " tháng " + bn.NgayTH.Value.Month + " năm " + bn.NgayTH.Value.Year;

                    }

                    var canbo = data.CanBoes.Where(p => p.MaCB == bn.MaCB).FirstOrDefault();
                    if (canbo != null)
                    {
                        rep.txt_BSCD.Text = canbo.TenCB;
                        rep.BSCD.Text = "BS Chỉ Định: " + canbo.TenCB;
                    }

                    var canboth = data.CanBoes.Where(p => p.MaCB == bn.MaCBth).FirstOrDefault();
                    if (canboth != null)
                        rep.txtCanBoTH.Text = canboth.TenCB;

                    // kết quả xét nghiệm
                    var kqcls = (from cd in data.ChiDinhs.Where(p => p.IdCLS == idcls)
                                 join clsct in data.CLScts on cd.IDCD equals clsct.IDCD
                                 join dvct in data.DichVucts on clsct.MaDVct equals dvct.MaDVct
                                 join dv in data.DichVus on cd.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs.Where(p => p.TenRG == tenTNRG) on dv.IdTieuNhom equals tn.IdTieuNhom
                                 select new { cd.MaDV, dvct.TenDVct, dvct.STT, clsct.KetQua, dv.TenDV, dvct.MaDVct, dvct.TSBT, dvct.DonVi }).ToList();


                    rep.DataSource = kqcls;
                    rep.BindingData();
                    rep.CreateDocument();
                    frmIn inphieu = new frmIn();
                    inphieu.prcIN.PrintingSystem = rep.PrintingSystem;
                    inphieu.ShowDialog();
                    return true;
                }
                else return false;

            }
            catch (Exception)
            {

                return false;



            }
        }
        #endregion xem phieu son la

        void inphieu()
        {
            List<int> _lIdCLS = new List<int>();
            List<int?> _lMaDV = new List<int?>();
            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            if (dataSource != null)
            {
                _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0).Select(o => o.IdCLS).ToList();
                _lMaDV = dataSource.Where(o => o.Check && o.MaDV > 0).Select(o => o.MaDV).Distinct().ToList();
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();

            if (DungChung.Bien.MaBV == "30004")
            {
                var ht = _data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).ToList();
                if (ht.Count() > 0)
                {
                    string[] kieudoc;
                    if (ht.First().KieuDoc != null)
                    {
                        kieudoc = ht.First().KieuDoc.Split(';');
                        DungChung.Bien._Visible_CDHA[0] = Convert.ToBoolean(kieudoc[0]);
                        DungChung.Bien._Visible_CDHA[1] = Convert.ToBoolean(kieudoc[1]);
                    }

                }
            }
            int makp = 0; string _makp = "";
            if (LupKpThuchien.EditValue != null)
            {
                makp = Convert.ToInt32(LupKpThuchien.EditValue);
                _makp = ";" + makp.ToString() + ";";
            }
            #region xemphieu

            if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
            {
                int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();// l?y l?i theo tên rút g?n ti?u nhóm.
                int maDV = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("MaDV"));
                var IDCD = (int)GrvNhomDV.GetFocusedRowCellValue(colIDCD);
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var cddi = _data.ChiDinhs.Where(x => x.IDCD == IDCD).FirstOrDefault();
                if (cddi.Status == 1)
                {
                    Frm_CDHA_Moi.ngaythuchien = cddi.NgayTH.Value;
                }
                if (CLS.InPhieu._isCDHA(_data, idCLS) || TTN == "Chức năng hô hấp")
                {
                    if (DungChung.Bien.MaBV == "34019")
                    {
                        CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                    }
                    else if (DungChung.Bien.MaBV == "14018" && (TTN == "Điều trị vận động" || TTN == "Điều trị y học cổ truyền" || TTN == "Điều trị vật lý" || TTN == "Điều trị ngôn ngữ trị liệu"))
                    {

                        InPhieu.InPhieuDieuTri_14018(maDV, _Mabn, InPhieu.TypePhieuDieuTri14018.DieuTri, null, IDCD);
                    }
                    else if (DungChung.Bien.MaBV == "01071" && (TTN == "Nội soi Tai-Mũi-Họng" || TTN == "Nội soi Dạ Dày"))
                    {
                        CLS.InPhieu._inPhieu_CDHA_mau(_data, TTN, _Mabn, idCLS, IDCD);
                    }
                    else if (DungChung.Bien.MaBV != "24012" && DungChung.Bien.MaBV != "30372" && TTN == "Nội soi Tai-Mũi-Họng")
                    {
                        CLS.InPhieu._inPhieu_CDHA_mau(_data, TTN, _Mabn, idCLS, IDCD);
                    }
                    else
                    {
                        bool _InPhieu0 = DungChung.Bien._Visible_CDHA[0];
                        bool _InPhieu1 = DungChung.Bien._Visible_CDHA[1];
                        //DungChung.Bien._Visible_CDHA[0] = true;

                        if (DungChung.Bien.MaBV == "01830" && TTN == "Thủ thuật")
                            DungChung.Bien._Visible_CDHA[1] = false;
                        if (DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "01071")
                        {
                            CLS.InPhieu._inPhieu_CDHA1(_data, TTN, _Mabn, idCLS, IDCD, false);
                        }
                        else
                            CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, IDCD, false, makp);

                    }

                }
                else if (CLS.InPhieu._isTDCN(_data, idCLS))
                {
                    int _idcd = 0;
                    var idcd = _data.ChiDinhs.Where(p => p.IdCLS == idCLS).Select(p => p.IDCD).FirstOrDefault();
                    if (idcd != null)
                        _idcd = idcd;
                    if (DungChung.Bien.MaBV == "34019" || (DungChung.Bien.MaBV == "30009" && TTN == "Đo khúc xạ máy"))
                    {
                        CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                    }
                    else if (DungChung.Bien.MaBV == "30010" && TTN == "Điện não đồ")
                    {
                        CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                    }
                    else if (DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "01071")
                    {
                        if (TTN == "Đo mật độ xương")
                        {
                            CLS.InPhieu.InLuuHuyetNao1(_data, _idcd, TTN, 1);
                        }
                        else if (TTN == "Điện não đồ")
                            CLS.InPhieu.InLuuHuyetNao1(_data, _idcd, TTN, 3);
                        else
                            CLS.InPhieu.InLuuHuyetNao1(_data, _idcd, TTN, 0);
                    }
                    else if (DungChung.Bien.MaBV == "27001" && TTN.ToUpper().Equals(("Nội soi Tai-Mũi-Họng").ToUpper()))
                    {
                        CLS.InPhieu._inPhieu_CDHA_mau(_data, TTN, _Mabn, idCLS, IDCD);
                    }
                    else
                    {
                        if (TTN == "Đo mật độ xương")
                        {
                            CLS.InPhieu.InLuuHuyetNao(_data, _idcd, TTN, 1);
                        }
                        else
                            CLS.InPhieu.InLuuHuyetNao(_data, _idcd, TTN, 0);
                    }
                }
                else if (TTN == "XN miễn dịch" && DungChung.Bien.MaBV != "30004")
                {

                    CLS.InPhieu.InPhieuXetNghiemMienDich(_data, _Mabn, idCLS, TTN);
                }
                else
                {
                    if (DungChung.Bien.MaBV == "14017")
                    {
                        QLBV.FormThamSo.frm_kqcls.phieuXNDongMau(_data, idCLS, TTN, _Mabn);
                    }
                    else { CLS.InPhieu._InPhieu_XetNghiem(_data, TTN, _Mabn, idCLS, 0, _lIdCLS, _lMaDV); }
                }
                return;
            }

            #endregion
        }
        void inphieu_30372()
        {
            #region xemphieu

            List<int> _lIdCLS = new List<int>();
            List<int?> _lMaDV = new List<int?>();
            List<int> _lIdCD = new List<int>();
            if (GrcNhomDV.DataSource != null)
            {
                var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
                if (dataSource != null)
                {
                    string selectedTTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();
                    _lIdCD = dataSource.Where(o => o.Check && o.IDCD > 0).Select(o => o.IDCD).ToList();
                    _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0 && o.TenRG == selectedTTN).Select(o => o.IdCLS).ToList();
                    _lMaDV = dataSource.Where(o => o.Check && o.MaDV > 0 && o.TenRG == selectedTTN).Select(o => o.MaDV).Distinct().ToList();
                }
                _lIdCLS = _lIdCLS.Distinct().ToList();
            }
            int makp = 0; string _makp = "";
            if (LupKpThuchien.EditValue != null)
            {
                makp = Convert.ToInt32(LupKpThuchien.EditValue);
                _makp = ";" + makp.ToString() + ";";
            }
            if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
            {

                int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();// l?y l?i theo tên rút g?n ti?u nhóm.
                int madv = 0;
                if (GrvNhomDV.GetFocusedRowCellValue(colMaDV_NhomDV) != null)
                {
                    madv = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colMaDV_NhomDV));
                }
                var IDCD = (int)GrvNhomDV.GetFocusedRowCellValue(colIDCD);
                //_data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (CLS.InPhieu._isCDHA(_data, idCLS) || TTN == "Chức năng hô hấp")
                {
                    if (DungChung.Bien.MaBV == "34019")
                    {
                        CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                    }
                    else if (DungChung.Bien.MaBV == "14018" && (TTN == "Điều trị vận động" || TTN == "Điều trị y học cổ truyền" || TTN == "Điều trị vật lý" || TTN == "Điều trị ngôn ngữ trị liệu"))
                    {

                        InPhieu.InPhieuDieuTri_14018(madv, _Mabn, InPhieu.TypePhieuDieuTri14018.DieuTri, null, IDCD);
                    }
                    else if (DungChung.Bien.MaBV != "24012" && DungChung.Bien.MaBV != "30372" && TTN == "Nội soi Tai-Mũi-Họng")
                    {
                        CLS.InPhieu._inPhieu_CDHA_mau(_data, TTN, _Mabn, idCLS, IDCD);
                    }
                    else
                    {
                        bool _InPhieu0 = DungChung.Bien._Visible_CDHA[0];
                        bool _InPhieu1 = DungChung.Bien._Visible_CDHA[1];
                        DungChung.Bien._Visible_CDHA[0] = true;
                        if (DungChung.Bien.MaBV == "01830" && TTN == "Thủ thuật")
                            DungChung.Bien._Visible_CDHA[1] = false;
                        if (DungChung.Bien.MaBV == "30372")
                        {
                            CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, IDCD, true, makp);
                        }
                        else
                        {
                            CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false, makp);
                        }
                    }

                }
                else if (CLS.InPhieu._isTDCN(_data, idCLS))
                {
                    var idcd = _data.ChiDinhs.Where(p => p.IdCLS == idCLS).Select(p => p.IDCD).FirstOrDefault();
                    if (DungChung.Bien.MaBV == "34019" || (DungChung.Bien.MaBV == "30009" && TTN == "Đo khúc xạ máy"))
                    {
                        CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                    }
                    else if (DungChung.Bien.MaBV == "30010" && TTN == "Điện não đồ")
                    {
                        CLS.InPhieu._inPhieu_CDHA(_data, TTN, _Mabn, idCLS, 0, false);
                    }
                    else
                    {
                        if (TTN == "Đo mật độ xương")
                        {
                            CLS.InPhieu.InLuuHuyetNao(_data, idcd, TTN, 1);
                        }
                        else
                        {
                            CLS.InPhieu.InLuuHuyetNao(_data, idcd, TTN, 0);
                        }
                    }
                }
                else if (TTN == "XN miễn dịch" && DungChung.Bien.MaBV != "30004")
                {

                    CLS.InPhieu.InPhieuXetNghiemMienDich(_data, _Mabn, idCLS, TTN, _lIdCLS, _lMaDV);
                }
                else
                {
                    if (DungChung.Bien.MaBV == "14017")
                    {
                        QLBV.FormThamSo.frm_kqcls.phieuXNDongMau(_data, idCLS, TTN, _Mabn);
                    }
                    else if (DungChung.Bien.MaBV == "30372")
                    {
                        CLS.InPhieu._InPhieu_XetNghiem_30372(_data, TTN, _Mabn, idCLS, 0, _lIdCLS, _lMaDV, _lIdCD);
                    }
                    else
                    {
                        CLS.InPhieu._InPhieu_XetNghiem(_data, TTN, _Mabn, idCLS, 0, _lIdCLS, _lMaDV);
                    }
                }
                return;
            }

            #endregion
        }

        string loadBSThLHN()
        {
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string macb = "";
            string tenck = "lưu huyết não";
            var c = (from cb in _Data.CanBoes.Where(p => p.Status == 1).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("cn") || p.CapBac.ToLower().Contains("ktv") || p.CapBac.ToLower().Contains("kỹ thuật viên") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                     join kp in _Data.KPhongs.Where(p => p.ChuyenKhoa.ToLower().Contains(tenck)) on cb.MaKP equals kp.MaKP
                     select new
                     {
                         cb.MaCB,
                     }).FirstOrDefault();
            if (c != null)
            {
                macb = c.MaCB.ToString();
            }
            return macb;
        }
        void thuchienLHN(int idCLS, int IDCD)
        {
            if (idCLS != 0 && IDCD != 0)
            {
                List<CLSct> _CLSct = new List<CLSct>();
                List<CL> _Cls = new List<CL>();
                List<ChiDinh> _Chidinh = new List<ChiDinh>();
                _Chidinh = _data.ChiDinhs.Where(p => p.IDCD == IDCD).ToList();
                _Cls = _data.CLS.Where(p => p.IdCLS == idCLS).ToList();
                _CLSct = _data.CLScts.Where(p => p.IDCD == IDCD).ToList();
                string kl = "";
                string macb = loadBSThLHN();
                bool KtraBNKSK = false;
                QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                try
                {
                    foreach (var b in _Chidinh)
                    {
                        int ID = b.IDCD;
                        var suacd = _Data.ChiDinhs.Single(p => p.IDCD == ID);
                        suacd.KetLuan = "";
                        kl = "";
                        suacd.LoiDan = "";
                        suacd.GhiChu = b.GhiChu;
                        suacd.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(DateTime.Now));
                        suacd.Status = 1;
                        suacd.TamThu = 0;
                        suacd.NgayTH = DateTime.Now;
                        suacd.MaMay = "";
                        suacd.SoPhim = 0;
                        _Data.SaveChanges();
                    }
                    foreach (var c in _CLSct)
                    {
                        var suaclsct = _Data.CLScts.Single(p => p.Id == c.Id);
                        suaclsct.DuongDan = c.DuongDan;
                        suaclsct.DuongDan2 = c.DuongDan2;
                        suaclsct.KetQua = ";;;;;;;;;;;;;;;";
                        suaclsct.KetQua_Rtf = ";;;;;;;;;;;;;;;";
                        suaclsct.KQDuKien = c.KQDuKien;
                        suaclsct.KQTyLe = c.KQTyLe;
                        suaclsct.SoPhieu = c.SoPhieu;
                        //if ((!String.IsNullOrEmpty(c.KetQua) && c.KetQua.Length > 0) || !string.IsNullOrEmpty(kl) )
                        //{
                        //    suaclsct.Status = 1;
                        //}
                        //else
                        //{
                        //    suaclsct.Status = c.Status;
                        //}
                        suaclsct.Status = 1;
                        suaclsct.STTHT = c.STTHT;
                        _Data.SaveChanges();

                        #region update dienbien 300619
                        if (c.KetQua != "" && c.KetQua != null && DungChung.Bien.MaBV != "34019")
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

                    }
                    int makp = 0;
                    foreach (var a in _Cls)
                    {
                        var suacls = _Data.CLS.Single(p => p.IdCLS == a.IdCLS);
                        makp = a.MaKP == null ? 0 : a.MaKP.Value;
                        suacls.MaCBth = macb;
                        suacls.NgayTH = DateTime.Now;
                        var ktstatuscd = _Data.ChiDinhs.Where(p => p.IdCLS == a.IdCLS).Where(p => p.Status == 0 || p.Status == null).ToList();
                        if (ktstatuscd.Count > 0)
                            suacls.Status = 0;
                        else
                        {
                            suacls.Status = 1;
                            BenhNhan sua = _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
                            if (sua != null)
                            {
                                var b = _Data.BNKBs.Where(p => p.MaBNhan == _Mabn).ToList();
                                var vienphi = _Data.VienPhis.Where(p => p.MaBNhan == _Mabn).ToList();
                                if (b.Count > 0 && vienphi.Count <= 0 && sua.Status != 2 && sua.Status != 3)
                                {
                                    sua.Status = 5;
                                }
                                if (sua.IDDTBN == 3 && DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                                    KtraBNKSK = true;
                            }
                        }
                        _Data.SaveChanges();

                    }
                    int IdCLS = idCLS;
                    var cdinh = (from cd1 in _Data.ChiDinhs.Where(p => p.IdCLS == IdCLS && p.Status == 1)
                                 join dv in _Data.DichVus on cd1.MaDV equals dv.MaDV
                                 select new { cd1.MaDV, cd1.SoPhieu, cd1.DonGia, cd1.IDCD, dv.DonVi, cd1.TrongBH, cd1.XHH, cd1.LoaiDV, cd1.IDGoi }).ToList();
                    int iddthuoc = 0;
                    int _idkb = 0;
                    var bnkb = _Data.BNKBs.Where(p => p.MaBNhan == _Mabn && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                    if (bnkb.Count > 0)
                        _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                    var ktdthuoc = _Data.DThuocs.Where(p => p.MaBNhan == _Mabn).Where(p => p.PLDV == 2).ToList();
                    if (ktdthuoc.Count > 0)
                        iddthuoc = ktdthuoc.First().IDDon;
                    List<int> dsIDGOiDV = new List<int>();//lấy danh sách những gói đã được thu thẳng trước đó
                    if (KtraBNKSK == true)
                    {
                        var _lThuTT = _Data.TamUngs.Where(p => p.IDGoiDV != null && p.PhanLoai == 3 && p.MaBNhan == _Mabn).Select(p => p.IDGoiDV ?? 0).ToList();
                        dsIDGOiDV.AddRange(_lThuTT);
                    }
                    if (iddthuoc > 0)
                    {
                        foreach (var cd2 in cdinh)
                        {
                            var kt = (from dt in _Data.DThuoccts.Where(p => p.IDCD == cd2.IDCD) select dt).ToList();
                            if (kt.Count <= 0)
                            {
                                double _dongia = DungChung.Ham._getGiaDM(_Data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, _Mabn, DateTime.Now);
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
                                if (macb != null)
                                    moi.MaCB = macb;
                                else
                                    moi.MaCB = "";
                                moi.MaKP = makp;

                                moi.ThanhTien = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                                moi.NgayNhap = DateTime.Now;
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
                                var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn).Where(p => p.DTuong == "BHYT").ToList();
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
                                    if (macb != null)
                                        dt.MaCB = macb;
                                    else
                                        dt.MaCB = "";
                                    dt.NgayNhap = DateTime.Now;
                                    dt.IDCLS = IdCLS;
                                }
                                _Data.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        DThuoc dthuoccd = new DThuoc();
                        dthuoccd.NgayKe = DateTime.Now; // cần phải lấy theo ngày tháng nhập kết quả CLS
                        dthuoccd.MaBNhan = _Mabn;
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
                                double _dongia = DungChung.Ham._getGiaDM(_Data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, _Mabn, DateTime.Now);
                                DThuocct moi = new DThuocct();
                                moi.MaDV = cd3.MaDV;
                                moi.IDDon = maxid;
                                moi.IDKB = _idkb;
                                moi.TrongBH = _Chidinh.First().TrongBH == null ? 0 : _Chidinh.First().TrongBH.Value;
                                if (macb != null)
                                    moi.MaCB = macb;
                                else
                                    moi.MaCB = "";
                                moi.NgayNhap = DateTime.Now;
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
                                var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn).Where(p => p.DTuong == "BHYT").ToList();
                                if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                                {
                                    double s = CheckGiaPhuThu.GiaPhuThu;
                                    DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("" + e, "Error");
                }
                return;
            }
        }
        void inphieumau()
        {
            List<int> _lIdCLS = new List<int>();
            List<int?> _lMaDV = new List<int?>();
            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            if (dataSource != null)
            {
                _lIdCLS = dataSource.Where(o => o.Check && o.IdCLS > 0).Select(o => o.IdCLS).ToList();
                _lMaDV = dataSource.Where(o => o.Check && o.MaDV > 0).Select(o => o.MaDV).Distinct().ToList();
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();
            if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
            {
                int madv = 0;
                if (GrvNhomDV.GetFocusedRowCellValue(colMaDV_NhomDV) != null)
                {
                    madv = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colMaDV_NhomDV));
                }
                int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();// l?y l?i theo tên rút g?n ti?u nhóm.
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (CLS.InPhieu._isCDHA(_data, idCLS))
                {
                    CLS.InPhieu._inPhieu_CDHA_mau(_data, TTN, _Mabn, idCLS, 0);
                }
                else if (CLS.InPhieu._isTDCN(_data, idCLS))
                {
                    int _idcd = 0;
                    var idcd = _data.ChiDinhs.Where(p => p.IdCLS == idCLS).Select(p => p.IDCD).FirstOrDefault();
                    if (idcd != 0)
                        _idcd = idcd;
                    if (DungChung.Bien.MaBV == "24297" && TTN == "Lưu huyết não")
                    {
                        CLS.InPhieu._inPhieu_CDHA_mau(_data, TTN, _Mabn, idCLS, 0);
                    }
                    else
                        CLS.InPhieu.InLuuHuyetNao(_data, _idcd, TTN, 0);
                }
                else
                {
                    CLS.InPhieu._InPhieu_XetNghiem(_data, TTN, _Mabn, idCLS, 0, _lIdCLS, _lMaDV);

                }
            }
            return;
        }
        private void lupXemPhieu_EditValueChanged(object sender, EventArgs e)
        {
            GrvNhomDV.FocusedColumn = colMaBScd;
            GrvNhomDV.FocusedColumn = XemPhieu;
            var row1 = (CanLamSang)GrvNhomDV.GetFocusedRow();
            int mauphieu = 0;
            if (GrvNhomDV.GetFocusedRowCellValue(XemPhieu) != null)
                mauphieu = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(XemPhieu));
            if (mauphieu > 0)
            {
                GrvNhomDV.SetFocusedRowCellValue(XemPhieu, 0);
                //                
                //
                //Xem phiếu
                //
                #region xemphieu
                if (mauphieu == 3)
                {
                    inphieu();
                }
                #endregion
                #region xem phiếu màu
                if (mauphieu == 4)
                {
                    inphieumau();
                }
                #endregion
                #region chi dịnh

                inchidinh(mauphieu);


                #endregion
                #region sao chỉ định
                if (mauphieu == 5)
                {
                    bool a = true;
                    int[] arrIDCLS = new int[100];
                    //int j = 0;
                    //for (int i = 0; i < GrvNhomDV.RowCount; i++)
                    //{
                    //    if (GrvNhomDV.GetRowCellValue(i, colChon) != null && GrvNhomDV.GetRowCellValue(i, colChon).ToString().ToLower() == "true")
                    //    {
                    //        arrIDCLS[j] = Convert.ToInt32(GrvNhomDV.GetRowCellValue(i, idcls));
                    //        j++;
                    //    }
                    //}
                    if (GrvNhomDV.GetFocusedRowCellValue(ID_CLS) != null)
                    {
                        arrIDCLS[0] = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(ID_CLS));
                    }
                    FormNhap.frm_SaoTTPT frm = new FormNhap.frm_SaoTTPT(arrIDCLS[0]);
                    frm.ShowDialog();
                    this.FRM_chidinh_Load(sender, e);
                }
                #endregion
                if (mauphieu == 6)
                {
                    QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var row = (CanLamSang)GrvNhomDV.GetFocusedRow();
                    if (row != null)
                    {
                        if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                        {
                            var dv = dataContext.DichVus.FirstOrDefault(o => o.MaDV == row.MaDV);
                            if (dv == null || dv.IS_EXECUTE_CLS != true)
                            {
                                MessageBox.Show("Chức năng không cho phép");
                                return;
                            }
                            if (idKB <= 0)
                            {
                                MessageBox.Show("Không thể thực hiện tại khoa phòng này");
                                return;
                            }
                        }
                        int idcd = 0;
                        if (GrvNhomDV.GetFocusedRowCellValue(colIDChiDinh) != null)
                        {
                            idcd = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colIDChiDinh));
                        }
                        ChucNang.frm_ThucHienPT frm = new ChucNang.frm_ThucHienPT(_Mabn, idcd, ReloadForm);

                        frm.ShowDialog();

                        return;
                    }
                }
                if (mauphieu == 7)// in full
                {
                    infull();
                }
                if (mauphieu == 8)
                {
                    int idcd = 0;
                    if (GrvNhomDV.GetFocusedRowCellValue(colIDChiDinh) != null)
                    {
                        idcd = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                    }
                    int makp = 0; string _makp = "";
                    if (LupKpThuchien.EditValue != null)
                    {
                        makp = Convert.ToInt32(LupKpThuchien.EditValue);
                        _makp = ";" + makp.ToString() + ";";
                    }
                    FormNhap.frm_ChiDinhBNLao frm = new FormNhap.frm_ChiDinhBNLao(idcd, _makp, _Mabn, 2);
                    frm.ShowDialog();
                }
                if (mauphieu == 9) // Thực hiện nhanh LHN HIS-1113 03/05/2022
                {
                    //thuchienLHN();
                }
            }
        }

        private void tendvinchidinh_Click(object sender, EventArgs e)
        {
            inchidinh(1);
        }

        private void lupBSCD_Click(object sender, EventArgs e)
        {
            inchidinh(2);
        }

        private void txtTimTenDV_TextChanged(object sender, EventArgs e)
        {
            string tenDV = "";
            if (!string.IsNullOrEmpty(txtTimTenDV.Text))
                tenDV = txtTimTenDV.Text.ToLower();
            grcDichvu.DataSource = _listDichVu.Where(p => tenDV == "" || p.tendv.ToLower().Contains(tenDV)).OrderBy(p => p.SoTT).ThenBy(p => p.tendv);
        }

        private void lupKhoaphong_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void lupKhoaphong_EditValueChanged_2(object sender, EventArgs e)
        {
            int makp = 0; string _makp = "";
            if (lupKhoaphong.EditValue != null)
            {
                makp = Convert.ToInt32(lupKhoaphong.EditValue);
                _makp = ";" + makp.ToString() + ";";
            }
            var _lCanBo = _data.CanBoes.Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.Status == 1).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();
            if (_lCanBo.Count > 0)
                lupNguoiKhamkb.Properties.DataSource = _lCanBo;
            var _lkptt = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang).Where(p => p.NhomKP == makp).ToList();
            var kpth = (from kp1 in _lkptt
                        join kp2 in _lkpnhom on kp1.MaKP equals kp2.MaKP
                        select new { kp1.MaKP }).ToList();
            if (kpth.Count > 0)
            {
                LupKpThuchien.EditValue = kpth.First().MaKP;
            }
        }

        private void grvDichvu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int madv = 0;
            if (grvDichvu.GetFocusedRowCellValue(colMadv) != null)
            {
                madv = Convert.ToInt32(grvDichvu.GetFocusedRowCellValue(colMadv));
                double dongia = Convert.ToDouble(grvDichvu.GetFocusedRowCellValue(colDonGia));
                txtDonGia.Text = dongia.ToString("##,###");
                bool trongdm = Convert.ToBoolean(grvDichvu.GetFocusedRowCellValue(colTrongBH));
                chkDichVu.Checked = trongdm;
                bool cpdinhkem = Convert.ToBoolean(grvDichvu.GetFocusedRowCellValue(colCPDinhKem));
                ckccpdinhkem.Checked = cpdinhkem;
                string malk = Convert.ToString(grvDichvu.GetFocusedRowCellValue(colMaQD));
                lupTenDV.EditValue = madv;
            }
            else
            {
                txtDonGia.Text = "0";
                chkDichVu.Checked = false;
                txtmaQD.Text = "";
                ckccpdinhkem.Checked = false;
            }
        }

        private void lupGoiDV_EditValueChanged(object sender, EventArgs e)
        {
            grcDichvu.DataSource = "";
            if (lupGoiDV.EditValue != null)
            {
                int idgoidv = Convert.ToInt32(lupGoiDV.EditValue);
                string IdGoi = ";" + idgoidv + ";";
                int idnhom = 0;
                khoitaodichvu2(IdGoi);
                var ktTN = (from tn in _listDichVu.Where(p => p.TenNhomCT == "Chẩn đoán hình ảnh" || p.TenNhomCT == ("Thăm dò chức năng") || p.TenNhomCT == "Thủ thuật, phẫu thuật" || p.TenNhomCT == "Xét nghiệm")
                            select tn).ToList();
                if (ktTN.Count() > 0)
                {
                    //pnYLenh.Visible = true;
                    colYLenh.Visible = true;
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        colYLenh.Caption = "Loại mẫu";
                    }
                }
                else if (DungChung.Bien.MaBV == "30007")
                {
                    //pnYLenh.Visible = true;
                    colYLenh.Visible = true;
                }
                else
                {
                    colYLenh.Visible = false;
                    pnYLenh.Visible = false;
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        colYLenh.Caption = "Loại mẫu";
                        colYLenh.Visible = true;
                    }
                }
                string makp = (";" + DungChung.Bien.MaKP + ";");



                var _lDV2 = (from dv in _listDichVu
                                 //where (dv.TenRG == _Idtn && dv.Status == 1)
                             select new
                             {
                                 TenDV = dv.tendv,
                                 MaDV = dv.madv,
                                 TrongDM = dv.TrongBH,
                                 SoTT = dv.SoTT,
                                 MaKPsd = dv.MaKPsd == null ? "" : dv.MaKPsd
                             }).OrderBy(p => p.TenDV).ThenBy(p => p.SoTT).ToList();
                var _lDV = (from dvct in _lDV2
                                //where (DungChung.Bien.PLoaiKP == "Admin" ? true : dvct.MaKPsd.Contains(makp))
                            group dvct by new { dvct.MaDV, dvct.TenDV, dvct.TrongDM, dvct.SoTT } into kq
                            select new
                            {
                                kq.Key.MaDV,
                                kq.Key.TenDV,
                                TrongDM = kq.Key.TrongDM,//DTBN == 1 ? 1 : 0,
                                kq.Key.SoTT
                            }).OrderBy(p => p.TenDV).ThenBy(p => p.SoTT).ToList();
                grcDichvu.DataSource = _listDichVu.OrderBy(p => p.SoTT).ThenBy(p => p.tendv);
                lupTenDV.Properties.DataSource = _lDV.ToList();
                grcCLS.DataSource = null;
                grcCLS.DataSource = _listDichVu.Where(p => p.Chon == true).ToList();
                //grvDichvu_CellValueChanging(null, null);
                LupKpThuchien.Enabled = false;
                radTenRG.SelectedIndex = -1;

            }
        }

        private void mm_ThongKePK_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvCLS_RowCellDefaultAlignment(object sender, DevExpress.XtraGrid.Views.Base.RowCellAlignmentEventArgs e)
        {

        }

        private void FRM_chidinh_Moi_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ((DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") && _lIDCLS_DB.Count > 0)
            {
                string ylenh = "";
                if (passData != null)
                {
                    passData("");
                }
            }

            this.Dispose();
            this.Close();
            MemoryManagement.FlushMemory();

        }
        public class MyObject
        {
            public int IDNhom { set; get; }
            public string TenTN { set; get; }
            public string TenRG { set; get; }
            public string MaQD { get; set; }
            public string TenDV { get; set; }
            public string YLenh { get; set; }
            public string ViTri { get; set; }

            public string TenNhom { get; set; }
            public string TenKhac { get; set; }
            public int? MaKP { get; set; }
            public int? MaDV { get; set; }
            public DateTime? NgayCD { get; set; }
        }

        CanLamSang canLamSangRightClick;
        private void GrvNhomDV_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                canLamSangRightClick = (CanLamSang)GrvNhomDV.GetRow(e.HitInfo.RowHandle);
                if (canLamSangRightClick == null)
                    return;
                if (DungChung.Bien.MaBV == "23456")
                {
                    contextMenuStrip1.Items[0].Visible = true;
                    contextMenuStrip1.Items[1].Visible = false;
                    contextMenuStrip1.Items[2].Visible = false;
                    if (canLamSangRightClick.IDNhom == 8)
                    {
                        GridView view = sender as GridView;
                        view.FocusedRowHandle = e.HitInfo.RowHandle;
                        contextMenuStrip1.Show(view.GridControl, e.Point);
                    }
                }
                else if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
                {
                    QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var dv = dataContext.DichVus.FirstOrDefault(o => o.MaDV == canLamSangRightClick.MaDV);
                    if (dv == null)
                        return;
                    contextMenuStrip1.Items[0].Visible = false;
                    contextMenuStrip1.Items[1].Visible = true;
                    contextMenuStrip1.Items[2].Visible = (dv.IS_EXECUTE_CLS == true);
                    //contextMenuStrip1.Items[3].Visible = !string.IsNullOrWhiteSpace(canLamSangRightClick.MaCBth);
                    GridView view = sender as GridView;
                    view.FocusedRowHandle = e.HitInfo.RowHandle;
                    contextMenuStrip1.Show(view.GridControl, e.Point);
                }
            }
        }

        private void saoChỉĐịnhPTTTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_SoLanSaoChiDinh frm = new frm_SoLanSaoChiDinh(canLamSangRightClick.IdCLS, ReloadForm);
            frm.ShowDialog();
        }

        private void ReloadForm()
        {
            FRM_chidinh_Load(null, null);
        }
        private void InChiDinh_30372(QLBV_Database.QLBVEntities _Data, int _mabn, int _idcd, int idcd)
        {

            var clsa = _Data.CLS.Where(p => _idcd > 0 ? true : p.IdCLS == _idcd).FirstOrDefault();
            DateTime Ngaycd = DateTime.Now;
            if (clsa != null)
            {
                Ngaycd = clsa.NgayThang.Value;
            }
            var par = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _mabn)
                       join cls in _data.CLS on bn.MaBNhan equals cls.MaBNhan
                       join cd in _data.ChiDinhs.Where(p => p.IDCD == idcd) on cls.IdCLS equals cd.IdCLS
                       join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                       join dv in _data.DichVus on cd.MaDV equals dv.MaDV
                       join kp in _data.KPhongs on cls.MaKP equals kp.MaKP
                       select new
                       {
                           bn.MaBNhan,
                           bn.TenBNhan,
                           bn.Tuoi,
                           bn.GTinh,
                           bn.DChi,
                           cls.ChanDoan,
                           bn.NgaySinh,
                           bn.ThangSinh,
                           bn.NamSinh,
                           bn.SThe,
                           cls.MaCB,
                           kp.TenKP,
                           cls.NgayThang,
                           dv.TenDV,
                           clsct.KetQua,
                           cd.KetLuan,
                           cls.NgayTH,
                           cls.MaCBth,
                           bn.DTuong
                       }).ToList();

            Dictionary<string, object> dicpar = new Dictionary<string, object>();
            // tên cơ quan chủ quản
            var MauIn = (_Data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().KieuDoc) == null ? "" : _Data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().KieuDoc;
            switch (MauIn)
            {
                case "True;True":
                    dicpar["KieuIn"] = 1;
                    break;
                case "True;False":
                    dicpar["KieuIn"] = 0;
                    break;
                case "False;True":
                    dicpar["KieuIn"] = 3;
                    break;
                default:
                    dicpar["KieuIn"] = 1;
                    break;
            }
            int TimeFormat = Convert.ToInt32(_Data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().FormatDate);
            dicpar["TenBV"] = DungChung.Bien.TenCQ;
            dicpar["TenCCCQ"] = DungChung.Bien.TenCQCQ;
            Image img = DungChung.Ham.GetLogo();
            dicpar["MaBN"] = par.First().MaBNhan;
            dicpar["AnhLogo"] = img;
            dicpar["DiaChiBenhVien"] = DungChung.Bien.DiaChi;
            dicpar["TieuDe"] = "PHIẾU CHỤP ";
            dicpar["SDTBV"] = "SĐT: " + _Data.HTHONGs.First().SDT + " - Fax: " + _Data.HTHONGs.First().Fax + "";
            dicpar["YeuCau"] = par.First().TenDV;
            dicpar["NgaySinh"] = par.First().NgaySinh + " / " + par.First().ThangSinh + " / " + par.First().NamSinh;
            dicpar["NgayChiDinh"] = DungChung.Ham.NgaySangChu(Convert.ToDateTime(par.First().NgayThang), TimeFormat);
            dicpar["Tuoi"] = par.First().Tuoi;
            if (_Data.BNKBs.Where(p => p.MaBNhan == _mabn).First().Buong != null)
            {
                dicpar["Buong"] = _Data.BNKBs.Where(p => p.MaBNhan == _mabn).First().Buong;
            }
            if (_Data.BNKBs.Where(p => p.MaBNhan == _mabn).First().Giuong != null)
            {
                dicpar["Giuong"] = _Data.BNKBs.Where(p => p.MaBNhan == _mabn).First().Giuong;
            }

            // Thông tin bệnh nhân
            dicpar["HoTenNguoiBenh"] = par.First().TenBNhan.ToUpper();
            dicpar["DiaChi"] = par.First().DChi;
            dicpar["Khoa"] = par.First().TenKP;
            dicpar["ChanDoan"] = par.First().ChanDoan;

            if (par.First().GTinh.ToString() == "Nam")
            {
                dicpar["Nam"] = "/";
            }
            else
            {
                dicpar["Nu"] = "/";
            }
            dicpar["Email"] = _Data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().Email;
            dicpar["DienThoai"] = _Data.TTboXungs.Where(p => p.MaBNhan == _mabn).First().DThoai;

            if (par.First().SThe != "")
            {
                dicpar["Ma1"] = par.First().SThe.Substring(0, 3);
                dicpar["Ma2"] = par.First().SThe.Substring(3, 2);
                dicpar["Ma3"] = par.First().SThe.Substring(5, 2);
                dicpar["Ma4"] = par.First().SThe.Substring(7, 3);
                dicpar["Ma5"] = par.First().SThe.Substring(10, 5);
            }
            dicpar["BSCD"] = DungChung.Ham._getTenCB(_Data, par.First().MaCB);
            string kq = par.First().KetQua;
            string KL = par.First().KetLuan;
            dicpar["KetQua"] = kq + "\n KẾT LUẬN: \n" + KL;
            dicpar["NgayThang"] = DungChung.Ham.NgaySangChu(Convert.ToDateTime(par.First().NgayThang), TimeFormat);
            dicpar["BSTH"] = DungChung.Ham._getTenCB(_Data, par.First().MaCBth).ToUpper();
            if (par.First().NgayTH != null)
            {
                dicpar["NgayTH"] = DungChung.Ham.NgaySangChu(Convert.ToDateTime(par.First().NgayTH), TimeFormat);
            }
            dicpar["DTBN"] = par.First().DTuong;
            DungChung.Ham.Print(DungChung.PrintConfig.Rep_PhieuCD_30372, null, dicpar, false);

        }

        private void btnSaveDate_Click(object sender, EventArgs e)
        {
            float _idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
            List<int> _lIDCD = new List<int>();
            List<int> _lIdCLS = new List<int>();
            //float _idCLS1 = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(ID_CLS));

            // _idCD = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(colIDChiDinh));
            #region chỉ định được chọn
            for (int i = 0; i < GrvNhomDV.RowCount; i++)
            {
                if (GrvNhomDV.GetRowCellValue(i, colCkChon) != null)
                {
                    bool check = Convert.ToBoolean(GrvNhomDV.GetRowCellValue(i, colCkChon).ToString());
                    if (check)
                    {
                        int idCD = Convert.ToInt32(GrvNhomDV.GetRowCellValue(i, colIDChiDinh));
                        int idCLS = Convert.ToInt32(GrvNhomDV.GetRowCellValue(i, "IdCLS"));
                        if (idCLS > 0 && idCD > 0)
                        {
                            _lIDCD.Add(idCD);
                            _lIdCLS.Add(idCLS);
                        }
                    }
                }
            }
            _lIdCLS = _lIdCLS.Distinct().ToList();
            #endregion
        }

        private void saoChỉĐịnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dataSource = (List<CanLamSang>)GrcNhomDV.DataSource;
            var checkCLS = dataSource.Where(o => o.Check).ToList();
            if (checkCLS.Count > 0 && canLamSangRightClick != null)
            {
                frm_SaoChiDinh_14018 frm = new frm_SaoChiDinh_14018(checkCLS.Select(o => o.IdCLS).ToList(), ReloadForm, canLamSangRightClick.IdCLS);
                frm.ShowDialog();
            }
        }

        private void FRM_chidinh_Moi_SizeChanged(object sender, EventArgs e)
        {
            this.panelControl8.Height = this.Height / 2;
        }

        private void GrvNhomDV_GroupRowExpanding(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017")
            {
                int rowCount = GrvNhomDV.GetChildRowCount(e.RowHandle);
                for (int i = 0; i < rowCount; i++)
                    GrvNhomDV.ExpandGroupRow(GrvNhomDV.GetChildRowHandle(e.RowHandle, i));
            }
        }

        private void thựcHiệnChỉĐịnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (canLamSangRightClick != null)
            {
                if (idKB <= 0)
                {
                    MessageBox.Show("Không thể thực hiện tại khoa phòng này");
                    return;
                }
                QLBV.ChucNang.frm_ThucHienPT frm = new ChucNang.frm_ThucHienPT(canLamSangRightClick.MaBNhan, canLamSangRightClick.IDCD, ReloadForm);
                frm.ShowDialog();
                //SaveExecuteCLS(canLamSangRightClick);
            }
        }

        private void SaveExecuteCLS(CanLamSang canLamSang)
        {
            QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool KtraBNKSK = false;

            #region Update CLS
            var dv = _Data.DichVus.FirstOrDefault(o => o.MaDV == canLamSang.MaDV);
            var suacls = _Data.CLS.Single(p => p.IdCLS == canLamSang.IdCLS);
            suacls.MaCBth = suacls.MaCB;
            //A quý bảo sau 10 phút
            suacls.NgayTH = suacls.NgayThang.Value.AddMinutes(10);
            suacls.DSCBTH = suacls.MaCBth;
            suacls.MaKPth = dv != null && dv.IS_EXECUTE_CLS == true ? suacls.MaKP : null;

            var ktstatuscd = _Data.ChiDinhs.Where(p => p.IdCLS == canLamSang.IdCLS && p.IDCD != canLamSang.IDCD).Where(p => p.Status == 0 || p.Status == null).ToList();
            if (ktstatuscd.Count > 0)
                suacls.Status = 0;
            else
            {
                suacls.Status = 1;
                BenhNhan sua = _Data.BenhNhans.Where(p => p.MaBNhan == canLamSang.MaBNhan).FirstOrDefault();
                if (sua != null)
                {
                    var b = _Data.BNKBs.Where(p => p.MaBNhan == canLamSang.MaBNhan).ToList();
                    var vienphi = _Data.VienPhis.Where(p => p.MaBNhan == canLamSang.MaBNhan).ToList();
                    if (b.Count > 0 && vienphi.Count <= 0 && sua.Status != 2 && sua.Status != 3)
                    {
                        sua.Status = 5;
                    }
                    if (sua.IDDTBN == 3 && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297"))
                        KtraBNKSK = true;
                }
            }
            #endregion

            #region Update ChiDinh
            int ID = canLamSang.IDCD;
            var suacd = _Data.ChiDinhs.Single(p => p.IDCD == ID);
            suacd.Status = 1;
            suacd.NgayTH = suacls.NgayTH;
            #endregion

            #region Update CLSct
            var _CLSct = _Data.CLScts.Where(o => o.IDCD == ID).ToList();
            foreach (var c in _CLSct)
            {
                var suaclsct = _Data.CLScts.FirstOrDefault(p => p.Id == c.Id);
                suaclsct.Status = 1;
            }
            #endregion

            int? makp = suacls.MaKPth;
            int IdCLS = canLamSang.IdCLS;
            int iddthuoc = 0;
            var ktdthuoc = _Data.DThuocs.Where(p => p.MaBNhan == canLamSang.MaBNhan).Where(p => p.PLDV == 2).ToList();
            if (ktdthuoc.Count > 0)
                iddthuoc = ktdthuoc.First().IDDon;

            var dichVu = _Data.DichVus.FirstOrDefault(o => o.MaDV == suacd.MaDV);
            List<int> dsIDGOiDV = new List<int>();//lấy danh sách những gói đã được thu thẳng trước đó
            if (KtraBNKSK == true)
            {
                var _lThuTT = _Data.TamUngs.Where(p => p.IDGoiDV != null && p.PhanLoai == 3 && p.MaBNhan == canLamSang.MaBNhan).Select(p => p.IDGoiDV ?? 0).ToList();
                dsIDGOiDV.AddRange(_lThuTT);
            }
            if (iddthuoc > 0)
            {
                var kt = (from dt in _Data.DThuoccts.Where(p => p.IDCD == suacd.IDCD) select dt).ToList();
                if (kt.Count <= 0)
                {
                    double _dongia = DungChung.Ham._getGiaDM(_Data, suacd.MaDV == null ? 0 : suacd.MaDV.Value, suacd.TrongBH == null ? 1 : suacd.TrongBH.Value, canLamSang.MaBNhan, suacls.NgayTH ?? DateTime.Now);
                    DThuocct moi = new DThuocct();
                    moi.MaDV = suacd.MaDV;
                    moi.IDKB = idKB;
                    moi.IDDon = iddthuoc;
                    moi.DonVi = dichVu != null ? dichVu.DonVi : "";
                    moi.TrongBH = suacd.TrongBH == null ? 0 : suacd.TrongBH.Value;
                    moi.IDCD = suacd.IDCD;
                    moi.DonGia = suacd.DonGia == 0 ? _dongia : suacd.DonGia;
                    moi.XHH = suacd.XHH;
                    moi.LoaiDV = suacd.LoaiDV;
                    moi.MaCB = suacls.MaCBth;
                    moi.MaKP = makp;
                    moi.ThanhTien = suacd.DonGia == 0 ? _dongia : suacd.DonGia;
                    moi.NgayNhap = suacls.NgayTH;
                    moi.SoLuong = 1;
                    moi.Status = 0;
                    if (KtraBNKSK == true && suacd.IDGoi != null && dsIDGOiDV.Where(p => p == suacd.IDGoi).Count() > 0)
                    {
                        moi.ThanhToan = 1;
                    }
                    else if (suacd.SoPhieu != null && suacd.SoPhieu > 0)
                        moi.ThanhToan = 1;
                    moi.TyLeTT = 100;
                    moi.IDCLS = IdCLS;
                    _Data.DThuoccts.Add(moi);
                    _Data.SaveChanges();
                    var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == suacd.MaDV).FirstOrDefault();
                    var sss = _Data.BenhNhans.Where(p => p.MaBNhan == suacls.MaBNhan).Where(p => p.DTuong == "BHYT").ToList();
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
                        dt.NgayNhap = suacls.NgayTH;
                        dt.IDCLS = IdCLS;
                    }
                    _Data.SaveChanges();
                }
            }
            else
            {
                DThuoc dthuoccd = new DThuoc();
                dthuoccd.NgayKe = suacls.NgayTH; // cần phải lấy theo ngày tháng nhập kết quả CLS
                dthuoccd.MaBNhan = suacls.MaBNhan;
                dthuoccd.MaKP = suacls.MaKP;
                dthuoccd.MaCB = suacls.MaCB;
                dthuoccd.PLDV = 2;
                dthuoccd.KieuDon = -1;
                _Data.DThuocs.Add(dthuoccd);
                if (_Data.SaveChanges() >= 0)
                {
                    int maxid = dthuoccd.IDDon;
                    double _dongia = DungChung.Ham._getGiaDM(_Data, suacd.MaDV == null ? 0 : suacd.MaDV.Value, suacd.TrongBH == null ? 1 : suacd.TrongBH.Value, canLamSang.MaBNhan, suacls.NgayTH ?? DateTime.Now);
                    DThuocct moi = new DThuocct();
                    moi.MaDV = suacd.MaDV;
                    moi.IDDon = maxid;
                    moi.IDKB = idKB;
                    moi.TrongBH = suacd.TrongBH == null ? 0 : suacd.TrongBH.Value;
                    moi.MaCB = suacls.MaCB;
                    moi.NgayNhap = suacls.NgayTH;
                    moi.MaKP = makp;
                    moi.IDCD = suacd.IDCD;
                    moi.DonVi = dichVu != null ? dichVu.DonVi : "";
                    moi.XHH = suacd.XHH;
                    moi.DonGia = suacd.DonGia == 0 ? _dongia : suacd.DonGia;
                    moi.ThanhTien = suacd.DonGia == 0 ? _dongia : suacd.DonGia;
                    moi.SoLuong = 1;
                    moi.Status = 0;
                    moi.LoaiDV = suacd.LoaiDV;
                    if (KtraBNKSK == true && suacd.IDGoi != null && dsIDGOiDV.Where(p => p == suacd.IDGoi).Count() > 0)
                    {
                        moi.ThanhToan = 1;
                    }
                    else if (suacd.SoPhieu != null && suacd.SoPhieu > 0)
                        moi.ThanhToan = 1;
                    moi.TyLeTT = 100;
                    moi.IDCLS = IdCLS;
                    _Data.DThuoccts.Add(moi);
                    _Data.SaveChanges();
                    var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == suacd.MaDV).FirstOrDefault();
                    var sss = _Data.BenhNhans.Where(p => p.MaBNhan == suacls.MaBNhan).Where(p => p.DTuong == "BHYT").ToList();
                    if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                    {
                        double s = CheckGiaPhuThu.GiaPhuThu;
                        DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                    }
                }
            }
            MessageBox.Show("Thực hiện thành công");
            var rowHandle = GrvNhomDV.LocateByValue("IDCD", suacd.IDCD);
            GrvNhomDV.SetRowCellValue(rowHandle, colBSTH, suacls.MaCBth);
        }

        private void hủyThựcHiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (canLamSangRightClick != null)
            {
                if (idKB <= 0)
                {
                    MessageBox.Show("Không thể thực hiện tại khoa phòng này");
                    return;
                }
                DeleteExecuteCLS(canLamSangRightClick);
            }
        }

        private void DeleteExecuteCLS(CanLamSang canLamSang)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var vp = _data.VienPhis.FirstOrDefault(p => p.MaBNhan == canLamSang.MaBNhan);
            if (vp != null)
            {
                MessageBox.Show("Bệnh nhân đã thanh toán không thể xoá!");
            }
            else
            {
                DialogResult dia = MessageBox.Show("Bạn muốn xóa kết quả?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    int ID = canLamSang.IDCD;
                    var iddt = _data.DThuoccts.Where(p => p.IDCD == ID).ToList();
                    if (iddt.Count > 0)
                    {
                        foreach (var item in iddt)
                        {
                            int iddtct = item.IDDonct;
                            var ktchiphi = _data.DThuoccts.Where(p => p.AttachIDDonct == iddtct).ToList();
                            if (ktchiphi.Count > 0)
                            {
                                MessageBox.Show("Dịch vụ đã có chi phí đính kèm, bạn không thế xóa");
                                return;
                            }
                            var xoa = _data.DThuoccts.Single(p => p.IDDonct == iddtct);
                            _data.DThuoccts.Remove(xoa);
                        }
                    }

                    var suacd = _data.ChiDinhs.FirstOrDefault(p => p.IDCD == ID);
                    suacd.NgayTH = null;
                    suacd.KetLuan = "";
                    suacd.LoiDan = "";
                    suacd.MoTa = "";
                    suacd.MaMay = "";
                    //suacd.SoPhieu = 0;
                    suacd.Status = 0;
                    //suacd.TamThu = 1;

                    #region Update CLSct
                    var _CLSct = _data.CLScts.Where(o => o.IDCD == ID).ToList();
                    foreach (var c in _CLSct)
                    {
                        var suaclsct = _data.CLScts.Single(p => p.Id == c.Id);
                        suaclsct.DuongDan = "";
                        suaclsct.DuongDan2 = "";
                        suaclsct.KetQua = "";
                        suaclsct.KQDuKien = "";
                        suaclsct.KQTyLe = "";
                        suaclsct.SoPhieu = 0;
                        suaclsct.Status = 0;
                    }
                    #endregion

                    var suacls = _data.CLS.FirstOrDefault(p => p.IdCLS == canLamSang.IdCLS);
                    if (suacls != null)
                    {
                        suacls.MaCBth = "";
                        suacls.Status = 0;
                        suacls.NgayTH = null;
                    }
                    _data.SaveChanges();
                    _setStatus(canLamSang.MaBNhan);
                    MessageBox.Show("Xoá thành công!");
                    var rowHandle = GrvNhomDV.LocateByValue("IDCD", suacd.IDCD);
                    GrvNhomDV.SetRowCellValue(rowHandle, colBSTH, null);
                }
            }
        }

        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        private void txtTimkiemMaDV_TextChanged(object sender, EventArgs e)
        {
            List<C_DichVu> _listmadv = new List<C_DichVu>();

            string _madv = "";
            if (!string.IsNullOrEmpty(txtTimkiemMaDV.Text))
                _madv = txtTimkiemMaDV.Text;

            string[] _getmadv = _madv.Split(',');
            if (txtTimkiemMaDV.Text != "")
            {
                for (int i = 0; i < _getmadv.Count(); i++)
                {
                    if (_getmadv[i] != "")
                    {
                        if (IsNumber(_getmadv[i]))
                        {
                            int s = Convert.ToInt32(_getmadv[i].ToString());
                            var q = _listDichVu.Where(p => _madv == "" || p.madv == s).OrderBy(p => p.SoTT).ThenBy(p => p.tendv);
                            foreach (var item in q)
                            {
                                C_DichVu _TK = new C_DichVu();
                                _TK.Chon = item.Chon;
                                _TK.TheoYC = item.TheoYC;
                                _TK.madv = item.madv;
                                _TK.tendv = item.tendv;
                                _TK.CPDinhKem = item.CPDinhKem;
                                _TK.MaQD = item.MaQD;
                                _TK.SoTT = item.SoTT;
                                _TK.TrongBH = item.TrongBH;
                                _TK.DonGia = item.DonGia;
                                _listmadv.Add(_TK);
                            }
                        }
                    }
                }
                grcDichvu.DataSource = _listmadv;
            }
            else grcDichvu.DataSource = _listDichVu;
        }

        private void btnCDTH_Click(object sender, EventArgs e)
        {
            QLBV.ChucNang.frm_ChiDinh_TongHop frm = new ChucNang.frm_ChiDinh_TongHop(_Mabn);
            frm.ShowDialog();
        }

        private void GrvNhomDV_ShowingEditor(object sender, CancelEventArgs e)
        {
            var row = (CanLamSang)GrvNhomDV.GetFocusedRow();
            if (row != null && (DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24297"))
            {
                if (DungChung.Bien.MaBV == "14017" ? (GrvNhomDV.FocusedColumn == colBSTH && row.IDNhom != 8) : (DungChung.Bien.MaBV == "24297" ? (!row.IsAutoExecute && GrvNhomDV.FocusedColumn == colBSTH) : true))
                {
                    e.Cancel = true;
                }
            }
        }

        private void GrvNhomDV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var row = (CanLamSang)GrvNhomDV.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "MaCBth")
                {
                    QLBVEntities dataContext = new QLBVEntities(DungChung.Bien.StrCon);
                    var cls = dataContext.CLS.FirstOrDefault(o => o.IdCLS == row.IdCLS);
                    if (cls != null)
                    {
                        cls.MaCBth = e.Value != null ? e.Value.ToString() : null;
                    }
                    var cd = dataContext.ChiDinhs.FirstOrDefault(o => o.IDCD == row.IDCD);
                    if (cd != null)
                    {
                        cd.MaCBth = e.Value != null ? e.Value.ToString() : null;
                    }
                    var dtcts = dataContext.DThuoccts.Where(o => o.IDCD == row.IDCD).ToList();
                    foreach (var item in dtcts)
                    {
                        var dtct = dataContext.DThuoccts.FirstOrDefault(o => o.IDDonct == item.IDDonct);
                        if (dtct != null)
                        {
                            dtct.MaCB = e.Value != null ? e.Value.ToString() : null;
                        }
                    }

                    if (dataContext.SaveChanges() > 0)
                    {
                        MessageBox.Show("Lưu thành công");
                        xoa = true;
                    }
                }
            }
        }

        private void lupTenCBth_EditValueChanged(object sender, EventArgs e)
        {
        }

        bool xoa = false;
        private void lupTenCBth_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                {
                    var row = (CanLamSang)GrvNhomDV.GetFocusedRow();
                    if (row != null && row.MaCBth != null && !string.IsNullOrWhiteSpace(row.MaCBth) && row.IDNhom == 8)
                    {
                        GrvNhomDV_CellValueChanging(null, new DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs(GrvNhomDV.FocusedRowHandle, colBSTH, null));
                        if (xoa)
                            GrvNhomDV.SetFocusedRowCellValue(colBSTH, null);
                    }
                }
            }
            finally
            {
                xoa = false;
            }
        }
        private void simpleButton1_Click_2(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void groupControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void txtMaBN_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {

            if (GrvNhomDV.GetFocusedRowCellValue("IdCLS") != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null)
            {
                int idCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                string TTN = GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString();
                int maDV = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("MaDV"));
                var IDCD = (int)GrvNhomDV.GetFocusedRowCellValue(colIDCD);
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (CLS.InPhieu._isCDHA(_data, idCLS) || TTN == "Chức năng hô hấp")
                {

                }
                else if (CLS.InPhieu._isTDCN(_data, idCLS))
                {
                    int _idcd = 0;
                    var idcd = _data.ChiDinhs.Where(p => p.IdCLS == idCLS).Select(p => p.IDCD).FirstOrDefault();
                    if (idcd != null)
                        _idcd = idcd;

                }
                else if (TTN == "XN miễn dịch")
                {

                }
                else
                {
                    QLBV.FormThamSo.frm_kqcls.phieuXNDongMau(_data, idCLS, TTN, _Mabn);
                }
                return;
            }
        }

        private void btnInCDDaChon_Click(object sender, EventArgs e)
        {
            List<int?> lstMadv = new List<int?>();
            foreach (var item in _lchidinh)
            {
                lstMadv.Add(item.MaDV);
            }
        }

        private void GrcNhomDV_Click(object sender, EventArgs e)
        {

        }
    }
}
