using System;
using QLBV_Library;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
//using DevExpress.XtraRichEdit.API.Word;
using QLBV.DungChung;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using QLBV_Database;

namespace QLBV.FormThamSo
{
    public partial class frm_kqcls : DevExpress.XtraEditors.XtraUserControl
    {
        public frm_kqcls()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<Lnuoctieu> _Lnuoctieu = new List<Lnuoctieu>();
        List<TieuNhomDV> _lTieuNhom = new List<TieuNhomDV>();
        private void EnableControl(bool t)
        {

            sbtLuu.Enabled = t;
            sbtSua.Enabled = !t;
            sbtXoa.Enabled = !t;
            lupMaMay.Enabled = t;
            foreach (GridColumn col in grvketqua.Columns)
            {
                if (col.Name != colExecute.Name)
                    col.OptionsColumn.ReadOnly = !t;
            }
            txtMoTa.ReadOnly = !t;
        }
        private string _setSID(string Code, string BarCode)
        {
            string _sid = "";
            string[] _id = new string[5] { "", "", "", "", "" };
            if (!string.IsNullOrEmpty(Code))
                _id = Code.Split('_');
            if (_id.Length > 0)
                _sid = _id[0] + "-" + BarCode;
            return _sid;
        }
        List<Status_CD> _lstatus = new List<Status_CD>();
        List<Status_CD> _lstatus_cd = new List<Status_CD>();
        public class _dsBenhNhan
        {
            int maKP;

            public int MaKP
            {
                get { return maKP; }
                set { maKP = value; }
            }
            string tenBNhan;

            public string TenBNhan
            {
                get { return tenBNhan; }
                set { tenBNhan = value; }
            }
            int maBNhan;

            public int MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }
            int tuoi;

            public int Tuoi
            {
                get { return tuoi; }
                set { tuoi = value; }
            }
            string dChi;

            public string DChi
            {
                get { return dChi; }
                set { dChi = value; }
            }
            string dTuong;

            public string DTuong
            {
                get { return dTuong; }
                set { dTuong = value; }
            }
            DateTime nNhap;

            public DateTime NNhap
            {
                get { return nNhap; }
                set { nNhap = value; }
            }
            int iDDTBN;

            public int IDDTBN
            {
                get { return iDDTBN; }
                set { iDDTBN = value; }
            }
            int gTinh;

            public int GTinh
            {
                get { return gTinh; }
                set { gTinh = value; }
            }
            int iDPerson;

            public int IDPerson
            {
                get { return iDPerson; }
                set { iDPerson = value; }
            }
            DateTime ngayThang;

            public DateTime NgayThang
            {
                get { return ngayThang; }
                set { ngayThang = value; }
            }
            int idCLS;

            public int IdCLS
            {
                get { return idCLS; }
                set { idCLS = value; }
            }
            int sTT;

            public int STT
            {
                get { return sTT; }
                set { sTT = value; }
            }
            string code;

            public string Code
            {
                get { return code; }
                set { code = value; }
            }
            string barcode;

            public string BarCode
            {
                get { return barcode; }
                set { barcode = value; }
            }
            int barcodenew;

            public int BarCodeNew
            {
                get { return barcodenew; }
                set { barcodenew = value; }
            }
        }
        List<_dsBenhNhan> _lDsBenhNhan = new List<_dsBenhNhan>();
        int time = 0;
        bool load = false;// sau khi load chuyeenr trangj thais thanhf true
        List<DichVu> _ldvu = new List<DichVu>();
        List<TaiSan> _lTaiSan = new List<TaiSan>();
        private ConnectData connect;
        int barCode_Number;

        private void frm_kqcls_Load(object sender, EventArgs e)
        {
            try
            {
                load = true;
                var hthong = _Data.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                if (hthong != null && hthong.Automatic_Barcode == true)
                {
                    txtBarcode.Enabled = false;
                    btnKhopMa.Enabled = false;
                    btnCreateBarcode.Enabled = true;
                }
                else
                {
                    txtBarcode.Enabled = true;
                    btnKhopMa.Enabled = true;
                    btnCreateBarcode.Enabled = false;
                }
                if (hthong != null)
                {
                    barCode_Number = hthong.Barcode_Number ?? 0;
                }
                if (DungChung.Bien.MaBV == "34019")
                {
                    ptPhoto.Visible = true;
                    memoEdit1.Visible = false;
                    groupControl6.Text = "";
                }
                connect = Program._connect;
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    this.txtBarcode.Properties.Mask.EditMask = "[0-9]{1,6}";
                    this.txtBarcode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                }
                if (DungChung.Bien.MaBV == "30372")
                {
                    //this.cboPhieu.Properties.Items.RemoveAt(4);
                    // đổi chức năng 2 nút in phiếu 
                    //this.cboPhieu.Text.Replace("In Phiếu gộp kết quả XN tổng hợp", "In Phiếu");
                    this.cboPhieu.Properties.Items.Add("In Phiếu");
                    this.cboPhieu.Properties.Items.RemoveAt(4);
                    this.cboPhieu.Properties.Items.Insert(5, "In phiếu XN Sar-CoV-2");
                    simpleButton1.Text = "In gộp";
                }

                _ldvu = _Data.DichVus.ToList();
                _lTaiSan = _Data.TaiSans.ToList();
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                    colSTT.Visible = false;
                if (DungChung.Bien.MaBV == "27001")
                {
                    colbc.Visible = true;
                    colbc.VisibleIndex = 3;
                }
                if (DungChung.Bien.MaBV == "30004")
                {
                    IDCLS.Visible = true;
                    lupMaMay.Visible = true;
                    labelControl21.Visible = true;
                }
                else
                {
                    grvMaMay.Visible = false;
                    IDCLS.Visible = false;
                }
                _lstatus.Add(new Status_CD { Ten = "Chưa H.Thành", Status = 0 });
                _lstatus.Add(new Status_CD { Ten = "Hoàn thành", Status = 1 });
                _lstatus.Add(new Status_CD { Ten = "Hủy", Status = -1 });
                _lstatus_cd.Add(new Status_CD { Ten = "Chưa làm", Status = 0 });
                _lstatus_cd.Add(new Status_CD { Ten = "Đã làm", Status = 1 });
                _lstatus_cd.Add(new Status_CD { Ten = "Hủy", Status = -1 });
                lup_status.DataSource = _lstatus.ToList();
                lup_status_cd.DataSource = _lstatus_cd.ToList();

                try
                {
                    time = Convert.ToInt32(ConfigurationManager.AppSettings["timerAutoResultCLS"]);
                }
                catch
                {

                }
                try
                {
                    if (time != 0)
                        timer1.Interval = time;
                }
                catch
                {

                }
                DungChung.Bien.thongtinketnoi = QLBV_Library.QLBV_Ham.Read_Update("Cuong.ConnCLS", 1);
                EnableControl(false);
                var q = (from kp in _Data.KPhongs.Where(p => p.ChuyenKhoa.Contains("Xét nghiệm") || (DungChung.Bien.MaBV == "34019" && p.ChuyenKhoa == "Trắc nghiệm tâm lý") || (DungChung.Bien.MaBV == "30299" && p.ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh) || (DungChung.Bien.MaBV == "27023" && p.ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc))
                         select new { kp.TenKP, kp.MaKP }).ToList();
                if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                {
                    LupKhoaphong.Properties.DataSource = q;
                    //lupKhoaKhamkb.Properties.DataSource = kpkhamdt;
                }
                else
                {

                    var kpkhamdt = (from a in q
                                    join b in DungChung.Bien.listKPHoatDong on a.MaKP equals b
                                    select a).ToList();
                    LupKhoaphong.Properties.DataSource = kpkhamdt;
                    //lupTimMaKP.Properties.DataSource = kpkhamdt;
                    //lupKhoaKhamkb.Properties.DataSource = kpkhamdt;
                }

                _lTieuNhom = _Data.TieuNhomDVs.Where(p => p.TenRG.Contains("XN")).ToList();
                _lTieuNhom.Add(new TieuNhomDV { IdTieuNhom = 0, TenTN = "Tất cả" });
                lupTieuNhom.Properties.DataSource = _lTieuNhom;
                //if (DungChung.Bien.CapDo == 8 || DungChung.Bien.CapDo == 9)
                //if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                //{
                //    LupKhoaphong.Properties.ReadOnly = false;
                //}
                //else
                //{
                //    LupKhoaphong.Properties.ReadOnly = true;
                //}
                //LupKhoaphong.EditValue = DungChung.Bien.MaKP;
                lupKhoaPhongcd.DataSource = _Data.KPhongs.ToList();
                lupNgayden.DateTime = System.DateTime.Now;
                lupNgaytu.DateTime = System.DateTime.Now;

                lupNgayTH.DateTime = System.DateTime.Now;
                tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                denngay = DungChung.Ham.NgayDen(lupNgayden.DateTime);
                TimKiem();
                // Đối với viện 30005 và 30281 thì cột combox sẽ cho họ điền text
                if (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30281")
                {
                    colketqua.Visible = false;
                    grvKetQua30005.Visible = true;
                }
                if (DungChung.Bien.MaBV != "30372")
                {
                    colExecute.Visible = false;
                    btnSaveIndex.Visible = false;
                }
            }
            finally
            {
                load = false;
            }
        }

        // lấy mã máy
        void getMaMay(int makp)
        {
            //var madv = (from ts in _lTaiSan.Where(p => p.MaKP == makp) select new { ts.MaDV }).ToList();
            //var mamay = (from m in madv
            //             join dv in _ldvu on m.MaDV equals dv.MaDV
            //             select new { dv.MaQD, dv.TenDV }).ToList();
            //lupMaMay.Properties.DataSource = mamay;
            //if (mamay.Count > 0)
            //{
            //    lupMaMay.Properties.DataSource = mamay;
            //    lupMaMay.EditValue = mamay.First().MaQD;
            //}
            string strSQL = "SELECT  MaQD, TenDV FROM dbo.TaiSan JOIN dbo.DichVu ON DichVu.MaDV = TaiSan.MaDV WHERE MaKP = '" + makp + "'";

            connect.Connect();
            DataTable tb = connect.FillDatatable(strSQL, CommandType.Text);
            lupMaMay.Properties.DataSource = tb;
            LupMaMay30004.DataSource = tb;
            if (tb.Rows.Count > 0)
            {
                //lupMaMay.EditValue = tb.Rows[0]["MaQD"];
            }

        }
        void getMaMay(int makp, int idtieunhom)
        {
            string strSQL = "SELECT  MaQD, TenDV FROM dbo.TaiSan JOIN dbo.DichVu ON DichVu.MaDV = TaiSan.MaDV WHERE MaKP = '" + makp + "' AND TaiSan.IdTieuNhom = '" + idtieunhom + "'";

            connect.Connect();
            DataTable tb = connect.FillDatatable(strSQL, CommandType.Text);
            lupMaMay.Properties.DataSource = null;
            lupMaMay.Properties.DataSource = tb;
            LupMaMay30004.DataSource = tb;
            if (tb.Rows.Count > 0)
            {
                //lupMaMay.EditValue = tb.Rows[0]["MaQD"];
            }
        }
        
        //
        void Timkiem2()
        {
            TimKiem();
            //grcBenhnhan.DataSource = "";
            //string timten = txttimten.Text.ToLower();
            //int rs;
            //int _int_maBN = 0;
            //if (Int32.TryParse(timten, out rs))
            //    _int_maBN = Convert.ToInt32(timten);
            //grcBenhnhan.DataSource = _lDsBenhNhan.Where(p => p.TenBNhan.ToLower().Contains(timten) || (_int_maBN > 0 && p.MaBNhan == _int_maBN));

        }
        List<BenhNhan> dsbn = new List<BenhNhan>();
        DataTable tbBenhNhan = new DataTable();
        DateTime tungay;
        DateTime denngay;
        private void TimKiem()
        {
            #region ADO
            tbBenhNhan = new DataTable();
            int _TT = cboTrangthai.SelectedIndex;
            int _noitru = rad_NoiTru.SelectedIndex;
            int _MaKP = 0;
            if (LupKhoaphong.EditValue != null)
            {
                _MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
            }
            int idtieunhom = 0;
            string code = txtTimBarcode.Text;
            if (lupTieuNhom.EditValue != null)
            {
                idtieunhom = Convert.ToInt32(lupTieuNhom.EditValue);
            }
            int _int_maBN = 0;
            string timten = txttimten.Text.ToLower();
            if (!string.IsNullOrEmpty(txttimten.Text))
            {
                int rs;
                if (Int32.TryParse(timten, out rs))
                    _int_maBN = Convert.ToInt32(timten);
            }

            string strSQL = "sp_kqXN_TimKiem";
            string[] strpara = new string[] { "@MaBV", "@MaBN", "@txtTimKiem", "@code", "@MaKP", "@idTieuNhom", "@ngaytu", "@ngayden", "@noitru", "@status" };
            object[] oValue = new object[] { DungChung.Bien.MaBV, _int_maBN, timten, code, _MaKP, idtieunhom, tungay, denngay, _noitru, _TT };
            SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.VarChar, SqlDbType.Int, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.Int, SqlDbType.Int, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Int, SqlDbType.Int };

            connect.Connect();
            tbBenhNhan = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);
            //tbBenhNhan.Columns.Add("IsKQ");
            //var _cls = _Data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay).ToList();
            //if (tbBenhNhan != null && DungChung.Bien.MaBV != "26007" && DungChung.Bien.MaBV != "01830")
            //{
            //    foreach (DataRow row in tbBenhNhan.Rows)
            //    {
            //        var rsLIS = new List<Library_CLS.Lis_His.clsChiDinhCLS>();
            //        var maBN = Convert.ToInt32(row["MaBNhan"]);
            //        var listBarcode = _cls.Where(o => o.MaBNhan == maBN).Select(o => o.BarCode).Distinct().ToList();

            //        foreach (var item in listBarcode)
            //        {
            //            var _idcls = _cls.Where(p => p.BarCode == item && p.MaBNhan == maBN).Select(p => p.IdCLS).ToList();
            //            try
            //            {
            //                rsLIS = Library_CLS.Lis_His.getDS_CLSct(item, _idcls, DungChung.Bien.xmlFilePath_LIS[1]);
            //            }
            //            catch (Exception ex)
            //            {
            //                DungChung.WriteLog.Error(string.Format("Lỗi lấy KQ: Barcode: {0} - IDCLS: {1}", item, _idcls));
            //            }
            //        }
            //        if (rsLIS != null && rsLIS.Count > 0)
            //            row["IsKQ"] = "1";
            //    }
            //}
            grcBenhnhan.DataSource = tbBenhNhan;

            #endregion

            #region linq
            //_lDsBenhNhan.Clear();
            //grcBenhnhan.DataSource = "";
            //DateTime _ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            //DateTime _ngayden = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            ////int _ravien = RAD.SelectedIndex;
            //int _TT = cboTrangthai.SelectedIndex;
            //int _noitru = rad_NoiTru.SelectedIndex;
            //int _MaKP = 0;
            //if (LupKhoaphong.EditValue != null)
            //{
            //    _MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
            //}
            //int idtieunhom = 0;
            //string code = txtTimBarcode.Text;
            //if (lupTieuNhom.EditValue != null)
            //{
            //    idtieunhom = Convert.ToInt32(lupTieuNhom.EditValue);
            //}
            //_Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            ////var q2 = (from bn in _Data.BenhNhans.Where(p => p.NoiTru == _noitru)
            ////          join cls in _Data.CLS on bn.MaBNhan equals cls.MaBNhan
            ////          join chidinh in _Data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS into leftjoin
            ////          from kq in leftjoin.DefaultIfEmpty()
            ////          join chidinh in _Data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
            ////          where (cls.NgayThang >= _ngaytu && cls.NgayThang <= _ngayden && cls.MaKPth == _MaKP)
            ////          where (cls.Status == _TT)
            ////          select new { hienthi = kq == null ? 0 : 1, cls.Code, STT = 0, cls.NgayThang, bn.TenBNhan, bn.MaKP, bn.MaBNhan, bn.Tuoi, bn.DChi, bn.DTuong, bn.NNhap, bn.IDDTBN, bn.GTinh, bn.IDPerson }).OrderBy(p => p.MaBNhan).ToList();
            ////var q1 = q2.Distinct().ToList();

            ////var q2 = (from cls in _Data.CLS.Where(p => p.NgayThang >= _ngaytu && p.NgayThang <= _ngayden).Where(p => p.MaKPth == _MaKP).Where(p => p.Status == _TT)
            ////          join bn in _Data.BenhNhans.Where(p => p.NoiTru == _noitru) on cls.MaBNhan equals bn.MaBNhan
            ////          join chidinh in _Data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS                     
            ////          select new {  cls.Code, STT = 0, cls.NgayThang, bn.TenBNhan, bn.MaKP, bn.MaBNhan, bn.Tuoi, bn.DChi, bn.DTuong, bn.NNhap, bn.IDDTBN, bn.GTinh, bn.IDPerson }).OrderBy(p => p.MaBNhan).ToList();

            //var q1 = (from cls in _Data.CLS.Where(p => p.NgayThang >= _ngaytu && p.NgayThang <= _ngayden).Where(p => p.MaKPth == _MaKP).Where(p => p.Status == _TT)
            //          //join bn in _Data.BenhNhans.Where(p => p.NoiTru == _noitru) on cls.MaBNhan equals bn.MaBNhan
            //          join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
            //          select new { cls.Code, cls.BarCode, cd.MaDV, STT = DungChung.Bien.MaBV == "00000" ? cls.STT : 0, cls.NgayThang, cls.MaBNhan }).OrderBy(p => p.MaBNhan).Distinct().ToList();
            ////tạm bỏ stt tam đường 12001 05/01, bn.TenBNhan, bn.MaKP, bn.MaBNhan, bn.Tuoi, bn.DChi, bn.DTuong, bn.NNhap, bn.IDDTBN, bn.GTinh, bn.IDPerson 
            //var q2 = (from a in q1
            //          join b in _ldvu.Where(p => idtieunhom == 0 ? true : p.IdTieuNhom == idtieunhom) on a.MaDV equals b.MaDV
            //          select a).ToList();
            //int _int_maBN = 0;
            //string timten = txttimten.Text.ToLower();
            //if (!string.IsNullOrEmpty(txttimten.Text))
            //{
            //    int rs;
            //    if (Int32.TryParse(timten, out rs))
            //        _int_maBN = Convert.ToInt32(timten);
            //}
            //List<int> a1 = q2.Select(p => p.MaBNhan ?? 0).ToList();
            //DateTime _tungaynew = _ngaytu.AddMonths(-2);
            //dsbn = _Data.BenhNhans.Where(p => p.NoiTru == _noitru).Where(p => p.NNhap >= _tungaynew && p.NNhap <= _ngayden).ToList();
            //var _lbn = (from a in a1 join bn in dsbn on a equals bn.MaBNhan select new { bn }).ToList();//.Where(p => a1.Contains(p.MaBNhan)).ToList();
            //_lDsBenhNhan = (from a in q2.Where(p => code == "" ? true : p.Code == code)
            //                join bn in _lbn on a.MaBNhan equals bn.bn.MaBNhan
            //                select new _dsBenhNhan
            //                {
            //                    Code = a.Code,
            //                    MaKP = bn.bn.MaKP ?? 0,
            //                    TenBNhan = bn.bn.TenBNhan,
            //                    MaBNhan = bn.bn.MaBNhan,
            //                    Tuoi = bn.bn.Tuoi ?? 0,
            //                    DChi = bn.bn.DChi,
            //                    DTuong = bn.bn.DTuong,
            //                    NNhap = bn.bn.NNhap ?? DateTime.Now,
            //                    IDDTBN = bn.bn.IDDTBN,
            //                    GTinh = bn.bn.GTinh ?? -1,
            //                    IDPerson = bn.bn.IDPerson ?? 0,
            //                    NgayThang = a.NgayThang == null ? a.NgayThang.Value.Date : DateTime.Now.Date,
            //                    STT = a.STT ?? 0,
            //                    IdCLS = 0,
            //                    BarCode = a.BarCode
            //                }).OrderBy(p => p.NgayThang).ToList();
            //_lDsBenhNhan = (from a in _lDsBenhNhan.Where(p => p.TenBNhan.ToLower().Contains(timten) || (_int_maBN > 0 && p.MaBNhan == _int_maBN) || (p.BarCode != null && p.BarCode.Contains(timten)))
            //                group a by new { STT = (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789") ? 0 : a.STT, a.NNhap, a.MaKP, a.MaBNhan, a.IDPerson, a.IDDTBN, a.IdCLS, a.GTinh, a.DChi, a.DTuong, a.TenBNhan, a.Tuoi, a.BarCode } into kq
            //                select new _dsBenhNhan
            //                {
            //                    MaKP = kq.Key.MaKP,
            //                    TenBNhan = kq.Key.TenBNhan,
            //                    MaBNhan = kq.Key.MaBNhan,
            //                    Tuoi = kq.Key.Tuoi,
            //                    DChi = kq.Key.DChi,
            //                    DTuong = kq.Key.DTuong,
            //                    NNhap = kq.Key.NNhap,
            //                    IDDTBN = kq.Key.IDDTBN,
            //                    GTinh = kq.Key.GTinh,
            //                    IDPerson = kq.Key.IDPerson,
            //                    //NgayThang = kq.Key.NgayThang,
            //                    BarCode = kq.Key.BarCode,
            //                    STT = kq.Key.STT,
            //                }).ToList();

            //if (DungChung.Bien.MaBV == "27001")
            //{
            //    foreach (var item in _lDsBenhNhan)
            //    {
            //        try
            //        {
            //            item.BarCodeNew = Convert.ToInt32(item.BarCode);
            //        }
            //        catch
            //        {
            //            item.BarCodeNew = 1;
            //        }
            //    }
            //    grcBenhnhan.DataSource = _lDsBenhNhan.OrderBy(p => p.BarCodeNew).ToList();
            //}
            //else
            //    grcBenhnhan.DataSource = _lDsBenhNhan.ToList();//.OrderBy(p => p.NgayThang).ThenBy(p => p.STT)
            #endregion

        }
        private void lupNgaytu_EditValueChanged(object sender, EventArgs e)
        {
            //grcBenhnhan.DataSource = "";
            //KTdsbn(txttimten.Text);
        }

        private void lupNgayden_EditValueChanged(object sender, EventArgs e)
        {
            //grcBenhnhan.DataSource = "";
            //KTdsbn(txttimten.Text);
        }
        private void cboRavien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!load)
                TimKiem();
        }
        private void cboTrangthai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!load)
                TimKiem();
        }
        // List<Ketquachitiet> _kpct = new List<Ketquachitiet>();
        int _Mabn = 0;
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
        public class ListCLS
        {
            private string tenrg;
            public string TenRG
            {
                set { tenrg = value; }
                get { return tenrg; }
            }
            private string tendv;
            public string TenDV
            {
                set { tendv = value; }
                get { return tendv; }
            }
            private int idcls;
            public int IDCLS
            {
                set { idcls = value; }
                get { return idcls; }
            }
            private int status;
            public int Status
            {
                set { status = value; }
                get { return status; }
            }
            private DateTime dt;
            public DateTime Ngaythang
            {
                set { dt = value; }
                get { return dt; }
            }
            private bool chon;
            public bool Chon
            {
                set { chon = value; }
                get { return chon; }
            }

            private int iDCD;
            public int IDCD
            {
                set { iDCD = value; }
                get { return iDCD; }
            }

            private int maKPth;
            public int MaKPth
            {
                set { maKPth = value; }
                get { return maKPth; }
            }
            public bool trangThaiBN { set; get; }//true: đói; false: ngẫu nhiên
            public string BarCode { set; get; }
        }
        // DataTable _tbCLS = new DataTable();

        List<ListCLS> _lCLS = new List<ListCLS>();
        int focus = 0;
        DataTable _tbCLSct = new DataTable();
        
        private class Lnuoctieu
        {
            private string Gtri;
            private int STT;
            public string gtri
            {
                get { return Gtri; }
                set { Gtri = value; }
            }
            public int stt
            {
                get { return STT; }
                set { STT = value; }
            }
        }
        private class Ketquachitiet
        {
            private string Tenxn;
            private string MaDVct;
            private string TSBT;
            private string Ketqua;
            private string Id;
            private int STT;
            private int STTHT;
            int status;
            public int Status
            {
                get { return status; }
                set { status = value; }
            }
            public int sttht
            {
                get { return STTHT; }
                set { STTHT = value; }
            }
            public int stt
            {
                get { return STT; }
                set { STT = value; }
            }
            public string id
            {
                get { return Id; }
                set { Id = value; }
            }
            public string tenxn
            {
                get { return Tenxn; }
                set { Tenxn = value; }
            }
            public string madvct
            {
                get { return MaDVct; }
                set { MaDVct = value; }
            }
            public string tsbt
            {
                get { return TSBT; }
                set { TSBT = value; }
            }
            public string ketqua
            {
                get { return Ketqua; }
                set { Ketqua = value; }
            }
            public int IDCD { set; get; }
        }

        List<object> listCheckClsct = new List<object>();
        private void grvketqua_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string IDTN = GrvNhomDV.GetFocusedRowCellValue("TenRG").ToString();
            var TenRG = (from trg in _Data.TieuNhomDVs.Where(p => p.TenRG == IDTN) select new { trg.TenRG }).ToList();
            if (grvketqua.GetFocusedRowCellValue(colketqua) != null && !string.IsNullOrEmpty(grvketqua.GetFocusedRowCellValue(colketqua).ToString()))
                if (e.Column.Name == "colketqua")
                {
                    if (grvketqua.GetFocusedRowCellValue(colStatus_CD) != null && grvketqua.GetFocusedRowCellValue(colStatus_CD).ToString() == "-1")
                    {

                        MessageBox.Show("dịch vụ đã được hủy, bạn không thể nhập kết quả");
                        grvketqua.SetFocusedRowCellValue(colketqua, "");
                        //colketqua.ColumnEdit.ReadOnly = true;
                    }
                }
            if (DungChung.Bien.MaBV == "30005")
            {
                if (e.Column.Name == "grvKetQua30005" && (TenRG.First().TenRG.Contains("XN nước tiểu") || TenRG.First().TenRG.Contains("XN đờm")))
                {
                    if (grvketqua.GetFocusedRowCellDisplayText(grvKetQua30005) != null)
                    {
                        string Ten = grvketqua.GetFocusedRowCellDisplayText(grvKetQua30005).ToString();
                        string TenKQ = Ten.Trim();
                        string madvct = grvketqua.GetFocusedRowCellValue(MaDVct).ToString();
                        //int SHT = 0;
                        //switch (TenKQ)
                        //{
                        //    case "Âm tính":
                        //        SHT = 1;
                        //        break;
                        //    case "Vết":
                        //        SHT = 2;
                        //        break;
                        //    case "+":
                        //        SHT = 3;
                        //        break;
                        //    case "++":
                        //        SHT = 4;
                        //        break;
                        //    case "+++":
                        //        SHT = 5;
                        //        break;
                        //    case "++++":
                        //        SHT = 6;
                        //        break;
                        //    default:
                        //        SHT = 1;
                        //        break;
                        //}
                        int sttht = 1;
                        if (_Lnuoctieu.Where(p => p.gtri == TenKQ).Count() > 0)
                        {
                            sttht = _Lnuoctieu.Where(p => p.gtri.Contains(TenKQ)).First().stt;
                        }
                        grvketqua.SetRowCellValue(e.RowHandle, "STT", sttht);
                        for (int i = 0; i < _tbCLSct.Rows.Count; i++)
                        {
                            if (_tbCLSct.Rows[i]["madvct"].ToString() == madvct)
                            {
                                _tbCLSct.Rows[i]["ketqua"] = TenKQ;
                                _tbCLSct.Rows[i]["sttht"] = sttht;
                            }
                        }
                        //foreach (var a in _kpct)
                        //{
                        //    if (a.madvct == madvct)
                        //    {
                        //        a.ketqua = TenKQ;
                        //        a.sttht = sttht;
                        //    }
                        //}
                    }
                }
            }
            else
            {
                if (e.Column.Name == "colketqua" && (TenRG.First().TenRG.Contains("XN nước tiểu") || TenRG.First().TenRG.Contains("XN đờm")))
                {
                    if (grvketqua.GetFocusedRowCellDisplayText(colketqua) != null)
                    {
                        string Ten = grvketqua.GetFocusedRowCellDisplayText(colketqua).ToString();
                        string TenKQ = Ten.Trim();
                        string madvct = grvketqua.GetFocusedRowCellValue(MaDVct).ToString();
                        //int SHT = 0;
                        //switch (TenKQ)
                        //{
                        //    case "Âm tính":
                        //        SHT = 1;
                        //        break;
                        //    case "Vết":
                        //        SHT = 2;
                        //        break;
                        //    case "+":
                        //        SHT = 3;
                        //        break;
                        //    case "++":
                        //        SHT = 4;
                        //        break;
                        //    case "+++":
                        //        SHT = 5;
                        //        break;
                        //    case "++++":
                        //        SHT = 6;
                        //        break;
                        //    default:
                        //        SHT = 1;
                        //        break;
                        //}
                        int sttht = 1;
                        if (_Lnuoctieu.Where(p => p.gtri == TenKQ).Count() > 0)
                        {
                            sttht = _Lnuoctieu.Where(p => p.gtri.Contains(TenKQ)).First().stt;
                        }
                        grvketqua.SetRowCellValue(e.RowHandle, "STT", sttht);
                        for (int i = 0; i < _tbCLSct.Rows.Count; i++)
                        {
                            if (_tbCLSct.Rows[i]["madvct"].ToString() == madvct)
                            {
                                _tbCLSct.Rows[i]["ketqua"] = TenKQ;
                                _tbCLSct.Rows[i]["sttht"] = sttht;
                            }
                        }
                        //foreach (var a in _kpct)
                        //{
                        //    if (a.madvct == madvct)
                        //    {
                        //        a.ketqua = TenKQ;
                        //        a.sttht = sttht;
                        //    }
                        //}
                    }
                }
            }
            string _Madvxt = grvketqua.GetFocusedRowCellValue(MaDVct).ToString();
            string ketqua = grvketqua.GetFocusedRowCellDisplayText(colketqua);

            if (e.Column.Name == "STT" && TenRG.First().TenRG.Contains("Nước tiểu"))
            {
                if (grvketqua.GetFocusedRowCellDisplayText(STT) != null && grvketqua.GetFocusedRowCellDisplayText(STT).ToString() != "")
                {
                    string stt = grvketqua.GetFocusedRowCellDisplayText(STT).ToString();
                    if (stt == "0")
                    {
                        MessageBox.Show("Bạn hãy chọn số thứ tự");
                    }
                    else
                    {
                        int s = 0;
                        int.TryParse(stt, out s);
                        for (int i = 0; i < _tbCLSct.Rows.Count; i++)
                        {
                            if (_tbCLSct.Rows[i]["madvct"].ToString() == _Madvxt)
                            {
                                _tbCLSct.Rows[i]["stt"] = s;
                            }
                        }

                        //foreach (var item in _kpct)
                        //{
                        //    if (item.madvct == _Madvxt)
                        //    {
                        //        item.stt = s;
                        //    }
                        //}
                    }
                }
            }
            for (int i = 0; i < _tbCLSct.Rows.Count; i++)
            {
                if (_tbCLSct.Rows[i]["madvct"].ToString() == _Madvxt)
                {
                    _tbCLSct.Rows[i]["ketqua"] = ketqua;
                }
            }
            //foreach (var item in _kpct)
            //{
            //    if (item.madvct == _Madvxt)
            //    {
            //        item.ketqua = ketqua;
            //    }
            //}
        }
        /// <summary>
        /// kiểm tra bệnh nhân đã có đầy đủ kết quả CLS hay chưa
        /// </summary>
        /// <param name="maBN"></param>
        /// <returns></returns>
        /// //dung280516
        private bool ktraHoanThanhKQCLS(int maBN)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qCLS = data.CLS.Where(p => p.MaBNhan == maBN && p.Status == 0).ToList();
            if (qCLS.Count > 0)
                return false;
            return true;
        }
        private void cboluu_Click(object sender, EventArgs e) // Chưa làm nút lưu. không được lưu khi chưa tạm thu
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBN.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBN.Text);
            _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            bool KtraBNKSK = false;
            bool ktra = true;
            string tendn = DungChung.Bien.TenDN;

            var q = (from ad in _Data.ADMINs.Where(p => p.TenDN == tendn)
                     join cb in _Data.CanBoes on ad.MaCB equals cb.MaCB
                     join kp in _Data.KPhongs.Where(p => p.PLoai == "Admin") on cb.MaKP equals kp.MaKP
                     select new { ad.TenDN, ad.MaCB, kp.PLoai }).Distinct().ToList();
            DateTime _ngayth = Convert.ToDateTime(lupNgayTH.Text);
            //var vp = (from vpct in _Data.VienPhis.Where(p => p.MaBNhan == _int_maBN).Where(p => p.NgayTT > _ngayth) select new { vpct.idVPhi, vpct.NgayTT }).ToList();
            //var vp1 = (from vpct in _Data.VienPhis.Where(p => p.MaBNhan == _int_maBN) select new { vpct.idVPhi, vpct.NgayTT }).ToList();
            var ravien = (from rv in _Data.RaViens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.NgayRa > _ngayth) select new { rv.IdRaVien, rv.NgayRa }).ToList();
            var ravien1 = (from rv in _Data.RaViens.Where(p => p.MaBNhan == _int_maBN) select new { rv.IdRaVien, rv.NgayRa }).ToList();

            // string s1 = vp1.Count > 0 ? vp1.Select(p => p.NgayTT).First().ToString() : "";
            string s2 = ravien1.Count > 0 ? ravien1.Select(p => p.NgayRa).First().ToString() : "";

            var ktRaVien = _Data.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
            int _IDCL = 0;
            if (GrvNhomDV.GetFocusedRowCellValue(IDCLS) != null && GrvNhomDV.GetFocusedRowCellValue(IDCLS).ToString() != "")
            {
                _IDCL = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(IDCLS));
            }

            #region Bệnh viện 30372 & tài khoản admin
            if (DungChung.Bien.MaBV == "30372" && q.Count > 0)
            {
                bool Checktamthu = false;
                if (!Checktamthu)
                    Checktamthu = DungChung.Ham._checkTamThu(_Data, _int_maBN, _IDCL); // thông báo chưa tạm thu dịch vụ

                if (ktra && !Checktamthu)
                {
                    ktra = false;
                }

                if (rad_NoiTru.SelectedIndex == 0)
                {
                    if (ravien.Count == 0 && ravien1.Count > 0)
                    {
                        ktra = false;
                        MessageBox.Show("Ngày thực hiện không được lớn hơn ngày ra viện\n (Ngày ra viện : " + s2 + " )", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    else
                    {
                        ktra = true;
                    }
                }

                if (rad_NoiTru.SelectedIndex == 1)
                {
                    if (ravien.Count == 0 && ravien1.Count > 0)
                    {
                        ktra = false;
                        MessageBox.Show("Ngày thực hiện không được lớn hơn ngày ra viện\n (Ngày ra viện : " + s2 + " )", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        ktra = true;
                    }
                }
                #region Kiểm tra ngày thực hiện
                if (lupNgayTH.DateTime != null && ktra)
                {
                    var _NgayCD = _Data.CLS.Where(p => p.IdCLS == _IDCL).Select(p => p.NgayThang).FirstOrDefault();
                    DateTime _NgayTH = lupNgayTH.DateTime;
                    if (_NgayCD != null)
                    {
                        if (_NgayTH < _NgayCD)
                        {
                            ktra = false;
                            MessageBox.Show("Ngày Thực hiện không được < ngày chỉ định", "Thông báo", MessageBoxButtons.OK);
                            lupNgayTH.Focus();
                        }
                        else
                        {
                            if (_NgayTH > DateTime.Now)
                            {
                                ktra = false;
                                MessageBox.Show("Ngày Thực hiện không được > ngày hiện tại", "Thông báo", MessageBoxButtons.OK);
                                lupNgayTH.Focus();
                            }
                            else
                            {
                                ktra = true;
                            }
                        }
                    }
                }
                #endregion
                #region Kiểm tra đã nhập thông tin đủ chưa?
                if (ktra)
                {
                    int TT = 0;
                    bool _bXNNT = false;
                    if (GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu && DungChung.Bien.MaBV == "30009")
                        _bXNNT = true;
                    for (int i = 0; i < _tbCLSct.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(_tbCLSct.Rows[i]["ketqua"].ToString()) && (string.IsNullOrEmpty(_tbCLSct.Rows[i]["ketqua"].ToString()) || Convert.ToInt32(_tbCLSct.Rows[i]["ketqua"]) == 0))
                        {
                            TT = 1;
                            break;
                        }
                    }
                    //foreach (var item in _kpct)
                    //{
                    //    if (item.ketqua == "" && (item.Status == null || item.Status == 0))
                    //    {
                    //        TT = 1;
                    //        break;
                    //    }
                    //}

                    if (DungChung.Bien.MaBV == "27023")
                    {
                        if (string.IsNullOrEmpty(LupCanBo.Text))
                        {
                            ktra = false;
                            MessageBox.Show("Bạn chưa chọn cán bộ thực hiện");
                        }
                    }
                    DialogResult dia = DialogResult.Yes;
                    if (TT == 1 && _bXNNT == false && DungChung.Bien.MaBV != "01830" && ktra)
                    {
                        dia = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dia == DialogResult.No)
                        {
                            ktra = false;
                        }
                    }
                }
                #endregion
                #region Lưu kết quả
                if (ktra)
                {
                    int makp = 0;
                    var dataSource = (DataTable)grcKetqua.DataSource;
                    for (int i = 0; i < dataSource.Rows.Count; i++)
                    {
                        int id = Convert.ToInt32(dataSource.Rows[i]["id"]);
                        CLSct sua = _Data.CLScts.Single(p => p.Id == id);
                        sua.KetQua = dataSource.Rows[i]["ketqua"].ToString();
                        if (!string.IsNullOrEmpty(dataSource.Rows[i]["sttht"].ToString()))
                            sua.STTHT = Convert.ToInt32(dataSource.Rows[i]["sttht"]);
                        sua.Status = 1;
                        sua.Is_Execute = Convert.ToBoolean(dataSource.Rows[i]["Is_Execute"] != DBNull.Value ? dataSource.Rows[i]["Is_Execute"] : "false");


                        if (DungChung.Bien.MaBV == "30004")
                        {
                            sua.MaMayct = dataSource.Rows[i]["MaQD"].ToString();
                        }
                        _Data.SaveChanges();
                        #region update dienbien 300619
                        if (sua.KetQua != "" && sua.KetQua != null && DungChung.Bien.MaBV != "34019")
                        {
                            var qcls_db = (from cddb in _Data.ChiDinhs.Where(p => p.IDCD == sua.IDCD)
                                           join clsdb in _Data.CLS on cddb.IdCLS equals clsdb.IdCLS
                                           select clsdb).FirstOrDefault();
                            if (qcls_db != null && qcls_db.IDDienBien != null && qcls_db.IDDienBien.Value > 0)
                            {
                                var qdb = _Data.DienBiens.Where(p => p.ID == qcls_db.IDDienBien).FirstOrDefault();

                                if (qdb != null)
                                {
                                    var tendvct = _Data.DichVucts.Single(p => p.MaDVct == sua.MaDVct);
                                    qdb.DienBien1 += Environment.NewLine + "+ " + tendvct.TenDVct + ": " + sua.KetQua;
                                    _Data.SaveChanges();
                                }
                            }
                        }
                        #endregion
                    }
                    //foreach (var item in _kpct)
                    //{
                    //    int id = Convert.ToInt32(item.id);
                    //    CLSct sua = _Data.CLScts.Single(p => p.Id == id);
                    //    sua.KetQua = item.ketqua;
                    //    sua.STTHT = item.sttht;
                    //    sua.Status = 1;
                    //    _Data.SaveChanges();
                    //}

                    EnableControl(false);
                    ///////////////////////

                    int cls = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(IDCLS));
                    //var cd = (from chidinh in _Data.ChiDinhs.Where(p => p.IdCLS == cls) select chidinh).ToList();
                    var cd = (from chidinh in _Data.ChiDinhs.Where(p => _lIDCD.Contains(p.IDCD)) select chidinh).ToList();

                    foreach (var c in cd)
                    {
                        if (c.Status == 0 || c.Status == null)
                        {
                            c.Status = 1;
                            c.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                            c.NgayTH = lupNgayTH.DateTime;
                            if (lupMaMay.EditValue != null)
                                c.MaMay = lupMaMay.EditValue.ToString();
                        }
                        else
                        {
                            if (lupMaMay.EditValue != null)
                                c.MaMay = lupMaMay.EditValue.ToString();
                        }

                        _Data.SaveChanges();
                    }

                    // update bảng CLS
                    var suaCL = _Data.CLS.Single(p => p.IdCLS == _IDCL);
                    makp = suaCL.MaKP == null ? 0 : suaCL.MaKP.Value; // Lấy makp chỉ định để gán vào makp trong bảng DThuoccts
                    if (LupCanBo.EditValue != null)
                    {
                        suaCL.MaCBth = LupCanBo.EditValue.ToString();
                    }
                    else
                        suaCL.MaCBth = "";
                    if (lupNgayTH.DateTime.Day > 0)
                        suaCL.NgayTH = lupNgayTH.DateTime;
                    var ktstatuscd = _Data.ChiDinhs.Where(p => p.IdCLS == cls).Where(p => p.Status == 0 || p.Status == null).ToList();
                    if (ktstatuscd.Count > 0)
                        suaCL.Status = 0;
                    else
                    {
                        suaCL.Status = 1;
                        BenhNhan sua = _Data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                        if (sua != null)
                        {
                            var a = _Data.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                            if (a.Count > 0 && sua.Status != 2 && sua.Status != 3)
                            {
                                sua.Status = 5;
                            }
                            if (sua.IDDTBN == 3 && DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                                KtraBNKSK = true;
                        }
                    }

                    suaCL.GhiChu = txtMoTa.Text;
                    _Data.SaveChanges();


                    if (DungChung.Bien.MaBV == "08204")
                    {
                        foreach (var item in _lresult_LIS)
                        {
                            string fileName = item.FileName;
                            string filePath = item.FilePath;
                        }
                        _lresult_LIS = new List<Library_CLS.Lis_His.clsChiDinhCLS>();
                    }
                    //
                    var cdinh = (from cd1 in _Data.ChiDinhs.Where(p => p.IdCLS == cls && p.Status == 1)
                                 join dv in _Data.DichVus on cd1.MaDV equals dv.MaDV
                                 select new { cd1.MaDV, cd1.SoPhieu, cd1.DonGia, dv.DonVi, cd1.TrongBH, cd1.IDCD, cd1.XHH, cd1.LoaiDV, cd1.IDGoi }).ToList();
                    int iddthuoc = 0;
                    if (Int32.TryParse(txtMaBN.Text, out rs))
                        _int_maBN = Convert.ToInt32(txtMaBN.Text);

                    int _idkb = 0;
                    var bnkb = _Data.BNKBs.Where(p => p.MaBNhan == _int_maBN && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                    if (bnkb.Count > 0)
                        _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                    var ktdthuoc = (_Data.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2)).ToList();
                    if (ktdthuoc.Count > 0)
                        iddthuoc = ktdthuoc.First().IDDon;
                    List<int> dsIDGOiDV = new List<int>();//lấy danh sách những gói đã được thu thẳng trước đó
                    if (KtraBNKSK == true)
                    {
                        var _lThuTT = _Data.TamUngs.Where(p => p.IDGoiDV != null && p.PhanLoai == 3 && p.MaBNhan == _int_maBN).Select(p => p.IDGoiDV ?? 0).ToList();
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
                                moi.IDDon = iddthuoc;
                                moi.IDKB = _idkb;
                                moi.TrongBH = cd2.TrongBH == null ? 0 : cd2.TrongBH.Value;
                                moi.IDCD = cd2.IDCD;
                                moi.DonVi = cd2.DonVi;
                                moi.MaKP = makp;
                                moi.NgayNhap = lupNgayTH.DateTime;
                                moi.DonGia = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                                moi.ThanhTien = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                                moi.SoLuong = 1;
                                moi.XHH = cd2.XHH;
                                moi.LoaiDV = cd2.LoaiDV;
                                if (LupCanBo.EditValue != null)
                                {
                                    moi.MaCB = LupCanBo.EditValue.ToString();
                                }
                                else
                                    moi.MaCB = "";
                                moi.Status = 0;
                                if (KtraBNKSK == true && cd2.IDGoi != null && dsIDGOiDV.Where(p => p == cd2.IDGoi).Count() > 0)
                                {
                                    moi.ThanhToan = 1;
                                }
                                else if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                    moi.ThanhToan = 1;
                                moi.TyLeTT = 100;
                                moi.IDCLS = cls;
                                _Data.DThuoccts.Add(moi);
                                _Data.SaveChanges();
                                var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cd2.MaDV).FirstOrDefault();
                                var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Where(p => p.DTuong == "BHYT").ToList();
                                if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                                {
                                    double s = CheckGiaPhuThu.GiaPhuThu;
                                    DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                                }
                            }
                            else
                            {
                                //sửa
                                foreach (var a in kt)
                                {
                                    a.NgayNhap = lupNgayTH.DateTime;
                                    a.IDCLS = cls;
                                    a.MaCB = suaCL.MaCBth;
                                    _Data.SaveChanges();
                                }
                            }
                        }
                    }
                    else
                    {
                        var suaCLS = _Data.CLS.Where(p => p.IdCLS == cls).ToList();

                        DThuoc dthuoccd = new DThuoc();
                        dthuoccd.NgayKe = lupNgayTH.DateTime; // cần phải lấy theo ngày tháng nhập kết quả CLS
                        dthuoccd.MaBNhan = _int_maBN;
                        dthuoccd.PLDV = 2;
                        if (suaCLS.Count > 0)
                        {
                            dthuoccd.MaKP = suaCLS.First().MaKP;
                            dthuoccd.MaCB = suaCLS.First().MaCB;
                        }
                        else
                        {
                            dthuoccd.MaKP = 0;
                            dthuoccd.MaCB = "";
                        }
                        dthuoccd.KieuDon = -1;
                        var _kp = _Data.CLS.Where(p => p.IdCLS == cls).ToList();
                        if (_kp.Count > 0 && _kp.First().MaKP != null)
                        {
                            dthuoccd.MaKP = _kp.First().MaKP;
                        }
                        _Data.DThuocs.Add(dthuoccd);
                        _Data.SaveChanges();
                        var maxid = dthuoccd.IDDon;
                        foreach (var cd3 in cdinh)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(_Data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, string.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd3.MaDV;
                            moi.IDKB = _idkb;
                            moi.IDDon = maxid;
                            moi.DonVi = cd3.DonVi;
                            moi.TrongBH = cd3.TrongBH == null ? 0 : cd3.TrongBH.Value;
                            moi.IDCD = cd3.IDCD;
                            moi.MaKP = makp;
                            moi.DonGia = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.NgayNhap = lupNgayTH.DateTime;
                            moi.ThanhTien = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.SoLuong = 1;
                            moi.LoaiDV = cd3.LoaiDV;
                            if (LupCanBo.EditValue != null)
                            {
                                moi.MaCB = LupCanBo.EditValue.ToString();
                            }
                            else
                                moi.MaCB = "";
                            moi.Status = 0;
                            moi.XHH = cd3.XHH;
                            if (KtraBNKSK == true && cd3.IDGoi != null && dsIDGOiDV.Where(p => p == cd3.IDGoi).Count() > 0)
                            {
                                moi.ThanhToan = 1;
                            }
                            else if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            //if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                            //    moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = cls;
                            _Data.DThuoccts.Add(moi);
                            _Data.SaveChanges();
                            var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cd3.MaDV).FirstOrDefault();
                            var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                            }
                        }
                    }
                    if (DungChung.Bien.MaBV == "01071")
                    {
                        var tongcp = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _int_maBN)
                                      join dtct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                      select new { dtct }).ToList();
                        double tongcptrbh = 0;
                        tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                        if (tongcptrbh >= 10000000)
                        {
                            MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vướt quá 10.000.000");
                        }
                    }
                    int row = GrvNhomDV.FocusedRowHandle;
                    grvBenhnhan_FocusedRowChanged_1(null, null);
                    GrvNhomDV.FocusedRowHandle = row;

                }
                #endregion

            }
            #endregion

            #region Tài khoản không là admin của 30372 và BV khác 30372
            else
            {
                DateTime ngay = Convert.ToDateTime("2018-01-31 23:59:59");

                if (DungChung.Bien.MaBV == "27023" && DateTime.Now < ngay)
                {
                    ktra = true;
                }
                else
                {
                    if (ktRaVien.Count > 0)
                    {
                        ktra = false;
                        MessageBox.Show("Bệnh nhân đã ra viện, bạn không thể lưu kết quả");
                    }
                    var qcls = _Data.CLS.Where(p => p.IdCLS == _IDCL).FirstOrDefault();
                    bool Checktamthu = false;
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "27023")
                    {
                        var dtuong = _Data.BenhNhans.Where(p => p.MaBNhan == _int_maBN && p.DTuong == "Dịch vụ").FirstOrDefault();
                        if (dtuong != null && qcls != null)
                        {
                            if (DungChung.Bien.MaBV == "27023")
                                Checktamthu = true;
                            else
                            {
                                int makpcd = qcls.MaKP ?? 0;
                                var qkp = _Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh && p.MaKP == makpcd).ToList();
                                if (qkp.Count > 0)
                                    Checktamthu = true;
                            }
                        }
                    }
                    if (!Checktamthu)
                        Checktamthu = DungChung.Ham._checkTamThu(_Data, _int_maBN, _IDCL); // thông báo chưa tạm thu dịch vụ

                    if (ktra && !Checktamthu)
                    {
                        ktra = false;
                    }
                }

                #region Kiểm tra ngày thực hiện
                if (lupNgayTH.DateTime != null && ktra)
                {
                    var _NgayCD = _Data.CLS.Where(p => p.IdCLS == _IDCL).Select(p => p.NgayThang).FirstOrDefault();
                    DateTime _NgayTH = lupNgayTH.DateTime;
                    if (_NgayCD != null)
                    {
                        if (_NgayTH < _NgayCD)
                        {
                            ktra = false;
                            MessageBox.Show("Ngày Thực hiện không được < ngày chỉ định", "Thông báo", MessageBoxButtons.OK);
                            lupNgayTH.Focus();
                        }
                        else
                        {
                            if (_NgayTH > DateTime.Now)
                            {
                                ktra = false;
                                MessageBox.Show("Ngày Thực hiện không được > ngày hiện tại", "Thông báo", MessageBoxButtons.OK);
                                lupNgayTH.Focus();
                            }
                            else
                            {
                                ktra = true;
                            }
                        }
                    }
                }
                #endregion
                //if (lupMaMay.EditValue == null)
                //{
                //    MessageBox.Show("Bạn chưa chọn mã máy thực hiện", "Thông báo"); tạm bỏ
                //    ktra = false;
                //    lupMaMay.Focus();
                //}
                #region Kiểm tra đã nhập thông tin đủ chưa?
                if (ktra)
                {
                    int TT = 0;
                    bool _bXNNT = false;
                    if (GrvNhomDV.GetFocusedRowCellValue(colTenRG) != null && GrvNhomDV.GetFocusedRowCellValue(colTenRG).ToString() == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu && DungChung.Bien.MaBV == "30009")
                        _bXNNT = true;
                    for (int i = 0; i < _tbCLSct.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(_tbCLSct.Rows[i]["ketqua"].ToString()) && (string.IsNullOrEmpty(_tbCLSct.Rows[i]["ketqua"].ToString()) || Convert.ToInt32(_tbCLSct.Rows[i]["ketqua"]) == 0))
                        {
                            TT = 1;
                            break;
                        }
                    }
                    //foreach (var item in _kpct)
                    //{
                    //    if (item.ketqua == "" && (item.Status == null || item.Status == 0))
                    //    {
                    //        TT = 1;
                    //        break;
                    //    }
                    //}

                    if (DungChung.Bien.MaBV == "27023")
                    {
                        if (string.IsNullOrEmpty(LupCanBo.Text))
                        {
                            ktra = false;
                            MessageBox.Show("Bạn chưa chọn cán bộ thực hiện");
                        }
                    }
                    DialogResult dia = DialogResult.Yes;
                    if (TT == 1 && _bXNNT == false && DungChung.Bien.MaBV != "01830" && ktra)
                    {
                        dia = MessageBox.Show("Bạn chưa nhập đầy đủ kết quả, bạn có muốn lưu dữ liệu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dia == DialogResult.No)
                        {
                            ktra = false;
                        }
                    }
                }
                #endregion
                var cd = (from chidinh in _Data.ChiDinhs.Where(p => _lIDCD.Contains(p.IDCD)) select chidinh).ToList();
                BenhNhan benhNhan = _Data.BenhNhans.FirstOrDefault(p => p.MaBNhan == _int_maBN);
                foreach (var item in cd)
                {
                    if (benhNhan != null && string.IsNullOrWhiteSpace(benhNhan.SThe) && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && !DungChung.Ham.Check_DuyetTamThu(item.IDCD))
                    {
                        MessageBox.Show("Dịch vụ chưa được duyệt tạm thu không thể thực hiện");
                        return;
                    }
                }
                #region Lưu kết quả
                if (ktra)
                {
                    int makp = 0;
                    for (int i = 0; i < _tbCLSct.Rows.Count; i++)
                    {
                        int id = Convert.ToInt32(_tbCLSct.Rows[i]["id"]);
                        CLSct sua = _Data.CLScts.Single(p => p.Id == id);
                        sua.KetQua = _tbCLSct.Rows[i]["ketqua"].ToString();
                        if (!string.IsNullOrEmpty(_tbCLSct.Rows[i]["sttht"].ToString()))
                            sua.STTHT = Convert.ToInt32(_tbCLSct.Rows[i]["sttht"]);
                        sua.Status = 1;
                        if (DungChung.Bien.MaBV == "30004")
                        {
                            sua.MaMayct = _tbCLSct.Rows[i]["MaQD"].ToString();
                        }

                        _Data.SaveChanges();
                        #region update dienbien 300619
                        if (sua.KetQua != "" && sua.KetQua != null && DungChung.Bien.MaBV != "34019")
                        {
                            var qcls_db = (from cddb in _Data.ChiDinhs.Where(p => p.IDCD == sua.IDCD)
                                           join clsdb in _Data.CLS on cddb.IdCLS equals clsdb.IdCLS
                                           select clsdb).FirstOrDefault();
                            if (qcls_db != null && qcls_db.IDDienBien != null && qcls_db.IDDienBien.Value > 0)
                            {
                                var qdb = _Data.DienBiens.Where(p => p.ID == qcls_db.IDDienBien).FirstOrDefault();

                                if (qdb != null)
                                {
                                    var tendvct = _Data.DichVucts.Single(p => p.MaDVct == sua.MaDVct);
                                    qdb.DienBien1 += Environment.NewLine + "+ " + tendvct.TenDVct + ": " + sua.KetQua;
                                    _Data.SaveChanges();
                                }
                            }
                        }
                        #endregion
                    }
                    //foreach (var item in _kpct)
                    //{
                    //    int id = Convert.ToInt32(item.id);
                    //    CLSct sua = _Data.CLScts.Single(p => p.Id == id);
                    //    sua.KetQua = item.ketqua;
                    //    sua.STTHT = item.sttht;
                    //    sua.Status = 1;
                    //    _Data.SaveChanges();
                    //}

                    EnableControl(false);
                    ///////////////////////

                    int cls = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(IDCLS));
                    //var cd = (from chidinh in _Data.ChiDinhs.Where(p => p.IdCLS == cls) select chidinh).ToList();

                    foreach (var c in cd)
                    {
                        if (c.Status == 0 || c.Status == null)
                        {
                            c.Status = 1;
                            c.NgoaiGioHC = Convert.ToInt32(FormThamSo.frm_UpdateLetet.CheckNGioHC(lupNgayTH.DateTime));
                            c.NgayTH = lupNgayTH.DateTime;
                            
                        }
                        if (lupMaMay.EditValue != null)
                            c.MaMay = lupMaMay.EditValue.ToString();
                        _Data.SaveChanges();
                    }

                    // update bảng CLS
                    var suaCL = _Data.CLS.Single(p => p.IdCLS == _IDCL);
                    makp = suaCL.MaKP == null ? 0 : suaCL.MaKP.Value; // Lấy makp chỉ định để gán vào makp trong bảng DThuoccts
                    if (LupCanBo.EditValue != null)
                    {
                        suaCL.MaCBth = LupCanBo.EditValue.ToString();
                    }
                    else
                        suaCL.MaCBth = "";
                    if (lupNgayTH.DateTime.Day > 0)
                        suaCL.NgayTH = lupNgayTH.DateTime;
                    var ktstatuscd = _Data.ChiDinhs.Where(p => p.IdCLS == cls).Where(p => p.Status == 0 || p.Status == null).ToList();
                    if (ktstatuscd.Count > 0)
                        suaCL.Status = 0;
                    else
                    {
                        suaCL.Status = 1;
                        BenhNhan sua = _Data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                        if (sua != null)
                        {
                            var a = _Data.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                            if (a.Count > 0 && sua.Status != 2 && sua.Status != 3)
                            {
                                sua.Status = 5;
                            }
                            if (sua.IDDTBN == 3 && DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                                KtraBNKSK = true;
                        }
                    }
                    suaCL.GhiChu = txtMoTa.Text;
                    _Data.SaveChanges();


                    if (DungChung.Bien.MaBV == "08204")
                    {
                        foreach (var item in _lresult_LIS)
                        {
                            string fileName = item.FileName;
                            string filePath = item.FilePath;
                        }
                        _lresult_LIS = new List<Library_CLS.Lis_His.clsChiDinhCLS>();
                    }
                    //
                    var cdinh = (from cd1 in _Data.ChiDinhs.Where(p => p.IdCLS == cls && p.Status == 1)
                                 join dv in _Data.DichVus on cd1.MaDV equals dv.MaDV
                                 select new { cd1.MaDV, cd1.SoPhieu, cd1.DonGia, dv.DonVi, cd1.TrongBH, cd1.IDCD, cd1.XHH, cd1.LoaiDV, cd1.IDGoi }).ToList();
                    int iddthuoc = 0;
                    if (Int32.TryParse(txtMaBN.Text, out rs))
                        _int_maBN = Convert.ToInt32(txtMaBN.Text);

                    int _idkb = 0;
                    var bnkb = _Data.BNKBs.Where(p => p.MaBNhan == _int_maBN && p.MaKP == makp).OrderByDescending(p => p.IDKB).ToList();
                    if (bnkb.Count > 0)
                        _idkb = bnkb.First().IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                    var ktdthuoc = (_Data.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2)).ToList();
                    if (ktdthuoc.Count > 0)
                        iddthuoc = ktdthuoc.First().IDDon;
                    List<int> dsIDGOiDV = new List<int>();//lấy danh sách những gói đã được thu thẳng trước đó
                    if (KtraBNKSK == true)
                    {
                        var _lThuTT = _Data.TamUngs.Where(p => p.IDGoiDV != null && p.PhanLoai == 3 && p.MaBNhan == _int_maBN).Select(p => p.IDGoiDV ?? 0).ToList();
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
                                moi.IDDon = iddthuoc;
                                moi.IDKB = _idkb;
                                moi.TrongBH = cd2.TrongBH == null ? 0 : cd2.TrongBH.Value;
                                moi.IDCD = cd2.IDCD;
                                moi.DonVi = cd2.DonVi;
                                moi.MaKP = makp;
                                moi.NgayNhap = lupNgayTH.DateTime;
                                moi.DonGia = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                                moi.ThanhTien = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                                moi.SoLuong = 1;
                                moi.XHH = cd2.XHH;
                                moi.LoaiDV = cd2.LoaiDV;
                                if (LupCanBo.EditValue != null)
                                {
                                    moi.MaCB = LupCanBo.EditValue.ToString();
                                }
                                else
                                    moi.MaCB = "";
                                moi.Status = 0;
                                if (KtraBNKSK == true && cd2.IDGoi != null && dsIDGOiDV.Where(p => p == cd2.IDGoi).Count() > 0)
                                {
                                    moi.ThanhToan = 1;
                                }
                                else if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                    moi.ThanhToan = 1;
                                moi.TyLeTT = 100;
                                moi.IDCLS = cls;
                                _Data.DThuoccts.Add(moi);
                                _Data.SaveChanges();
                                var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cd2.MaDV).FirstOrDefault();
                                var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Where(p => p.DTuong == "BHYT").ToList();
                                if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                                {
                                    double s = CheckGiaPhuThu.GiaPhuThu;
                                    DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                                }
                            }
                            else
                            {
                                //sửa
                                foreach (var a in kt)
                                {
                                    a.NgayNhap = lupNgayTH.DateTime;
                                    a.IDCLS = cls;
                                    a.MaCB = suaCL.MaCBth;
                                    _Data.SaveChanges();
                                }
                            }
                        }
                    }
                    else
                    {
                        var suaCLS = _Data.CLS.Where(p => p.IdCLS == cls).ToList();

                        DThuoc dthuoccd = new DThuoc();
                        dthuoccd.NgayKe = lupNgayTH.DateTime; // cần phải lấy theo ngày tháng nhập kết quả CLS
                        dthuoccd.MaBNhan = _int_maBN;
                        dthuoccd.PLDV = 2;
                        if (suaCLS.Count > 0)
                        {
                            dthuoccd.MaKP = suaCLS.First().MaKP;
                            dthuoccd.MaCB = suaCLS.First().MaCB;
                        }
                        else
                        {
                            dthuoccd.MaKP = 0;
                            dthuoccd.MaCB = "";
                        }
                        dthuoccd.KieuDon = -1;
                        var _kp = _Data.CLS.Where(p => p.IdCLS == cls).ToList();
                        if (_kp.Count > 0 && _kp.First().MaKP != null)
                        {
                            dthuoccd.MaKP = _kp.First().MaKP;
                        }
                        _Data.DThuocs.Add(dthuoccd);
                        _Data.SaveChanges();
                        var maxid = dthuoccd.IDDon;
                        foreach (var cd3 in cdinh)
                        {
                            double _dongia = DungChung.Ham._getGiaDM(_Data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, string.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), lupNgayTH.DateTime);
                            DThuocct moi = new DThuocct();
                            moi.MaDV = cd3.MaDV;
                            moi.IDKB = _idkb;
                            moi.IDDon = maxid;
                            moi.DonVi = cd3.DonVi;
                            moi.TrongBH = cd3.TrongBH == null ? 0 : cd3.TrongBH.Value;
                            moi.IDCD = cd3.IDCD;
                            moi.MaKP = makp;
                            moi.DonGia = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.NgayNhap = lupNgayTH.DateTime;
                            moi.ThanhTien = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                            moi.SoLuong = 1;
                            moi.LoaiDV = cd3.LoaiDV;
                            if (LupCanBo.EditValue != null)
                            {
                                moi.MaCB = LupCanBo.EditValue.ToString();
                            }
                            else
                                moi.MaCB = "";
                            moi.Status = 0;
                            moi.XHH = cd3.XHH;
                            if (KtraBNKSK == true && cd3.IDGoi != null && dsIDGOiDV.Where(p => p == cd3.IDGoi).Count() > 0)
                            {
                                moi.ThanhToan = 1;
                            }
                            else if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                moi.ThanhToan = 1;
                            //if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                            //    moi.ThanhToan = 1;
                            moi.TyLeTT = 100;
                            moi.IDCLS = cls;
                            _Data.DThuoccts.Add(moi);
                            _Data.SaveChanges();
                            var CheckGiaPhuThu = _Data.DichVus.Where(p => p.MaDV == cd3.MaDV).FirstOrDefault();
                            var sss = _Data.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Where(p => p.DTuong == "BHYT").ToList();
                            if (CheckGiaPhuThu != null && CheckGiaPhuThu.GiaPhuThu > 0 && sss.Count > 0 && moi.TrongBH == 1)
                            {
                                double s = CheckGiaPhuThu.GiaPhuThu;
                                DungChung.Ham._InsertPhuThu(_Data, moi.IDDonct, s);
                            }
                        }
                    }
                    if (DungChung.Bien.MaBV == "01071")
                    {
                        var tongcp = (from dt in _Data.DThuocs.Where(p => p.MaBNhan == _int_maBN)
                                      join dtct in _Data.DThuoccts.Where(p => p.TrongBH == 1) on dt.IDDon equals dtct.IDDon
                                      select new { dtct }).ToList();
                        double tongcptrbh = 0;
                        tongcptrbh = tongcp.Sum(p => p.dtct.ThanhTien);
                        if (tongcptrbh >= 10000000)
                        {
                            MessageBox.Show("Tổng chi phí điều trị trong danh mục của bệnh nhân đã vướt quá 10.000.000");
                        }
                    }
                    int row = GrvNhomDV.FocusedRowHandle;
                    grvBenhnhan_FocusedRowChanged_1(null, null);
                    GrvNhomDV.FocusedRowHandle = row;

                }
                #endregion
            }
            #endregion
        }
        private void LupKhoaphong_EditValueChanged(object sender, EventArgs e)
        {
            int _MaKP = 0; string Makp = "";
            if (LupKhoaphong.EditValue != null)
            {
                _MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
                Makp = ";" + _MaKP.ToString() + ";";
            }
            getMaMay(_MaKP);
            List<CanBo> _lcanbo = new List<CanBo>();
            _lcanbo = (from cb in _Data.CanBoes.Where(p => p.Status == 1).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("cn") || p.CapBac.ToLower().Contains("ktv") || p.CapBac.ToLower().Contains("kỹ thuật viên") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts"))
                       select cb).ToList();
            _lcanbo.Add(new CanBo { MaCB = "", TenCB = " " });
            //if (DungChung.Bien.CapDo == 8 || DungChung.Bien.CapDo == 9)
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                LupCanBo.Properties.DataSource = _lcanbo.OrderBy(p => p.TenCB).ToList();
            else
                LupCanBo.Properties.DataSource = _lcanbo.Where(p => p.MaKPsd != null && p.MaKPsd.Contains(Makp)).OrderBy(p => p.TenCB).ToList();
            LupCanBo.EditValue = DungChung.Bien.MaCB;
            if (!load)
                TimKiem();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DungChung.Bien.check = 0;
            if (GrvNhomDV.GetFocusedRowCellValue("IDCLS") != null && GrvNhomDV.GetFocusedRowCellValue("IDCLS").ToString() != "" && GrvNhomDV.GetFocusedRowCellValue("TenRG") != null)
            {
                //   int idtn = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDTN"));
                DungChung.Bien.NgayTH = Convert.ToDateTime(lupNgayTH.Text);
                int idcls = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDCLS"));
                string TTN = GrvNhomDV.GetFocusedRowCellValue("TenRG").ToString();
                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                if (DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389" || DungChung.Bien.MaBV == "27194")
                {
                    //MessageBox.Show("" + _Mabn); //his 169 
                    frm_InXnTongHop frm = new frm_InXnTongHop(_Mabn);
                    frm.InPhieuTongHop3004(_Mabn, idcls);
                }
                else if (DungChung.Bien.MaBV == "30372" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                {
                    int _int_maBN = 0;
                    Int32.TryParse(txtMaBN.Text, out _int_maBN);
                    QLBV.FormThamSo.frm_InXnTongHop frm = new QLBV.FormThamSo.frm_InXnTongHop(_int_maBN);
                    frm.ShowDialog();
                }
                
                }
                else if (DungChung.Bien.MaBV == "14017")
                {
                    simpleButton2_Click(null, null);
                }
                else
                {
                    QLBV.CLS.InPhieu._InPhieu_XetNghiem(_Data, TTN, String.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), idcls, 0);
                }
            }



        }

        private void grvBenhnhan_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colSoDK")
            {
                int sodk = 0;
                if (grvBenhnhan.GetFocusedRowCellValue(colSoDK) != null && grvBenhnhan.GetFocusedRowCellValue(colSoDK).ToString() != "")
                    sodk = Convert.ToInt32(grvBenhnhan.GetFocusedRowCellValue(colSoDK));
                FormNhap.getSoKB.ComPort(sodk);
                int _int_maBN = 0;
                if (grvBenhnhan.GetFocusedRowCellValue(colMabn) != null && grvBenhnhan.GetFocusedRowCellValue(colMabn).ToString() != "")
                    _int_maBN = Convert.ToInt32(grvBenhnhan.GetFocusedRowCellValue(colMabn));
                FormNhap.getSoKB.UpdateGoiSoDK(sodk, _int_maBN);
            }
        }
        List<int> _lIDCD = new List<int>();
        private void GrvNhomDV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focus = GrvNhomDV.FocusedRowHandle;
            _lIDCD = new List<int>();
            _tbCLSct = new DataTable();
            txtCode.Text = "";
            txtBarcode.Text = "";
            lupMaMay.EditValue = "";
            txtMoTa.ReadOnly = true;
            if (GrvNhomDV.GetFocusedRowCellValue("IDCLS") != null && GrvNhomDV.GetFocusedRowCellValue("IDCLS").ToString() != "")
            {
                int IDCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDCLS"));

                //barcode
                var qBarCode = _Data.CLS.Where(p => p.IdCLS == IDCLS).ToList();
                if (qBarCode.Count > 0 && qBarCode.First().Code != null)
                {
                    txtCode.Text = qBarCode.First().Code;
                }
                else
                {
                    txtCode.Text = "";
                }
                if (qBarCode.Count > 0 && qBarCode.First().BarCode != null)
                {
                    txtBarcode.Text = qBarCode.First().BarCode;
                }
                else
                {
                    txtBarcode.Text = "";
                }
                // mô tả

                if (qBarCode.Count > 0)
                {
                    txtMoTa.Text = qBarCode.First().GhiChu;
                }
                else
                {
                    txtMoTa.Text = "";
                }


                int mabn = 0;
                if (!String.IsNullOrEmpty(txtMaBN.Text))
                    mabn = Convert.ToInt32(txtMaBN.Text);
                bool Checktamthu = false;
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "27023")
                {
                    var dtuong = _Data.BenhNhans.Where(p => p.MaBNhan == mabn && p.DTuong == "Dịch vụ").FirstOrDefault();
                    if (dtuong != null && qBarCode.Count > 0)
                    {
                        if (DungChung.Bien.MaBV == "27023")
                            Checktamthu = true;
                        else
                        {
                            int makpcd = qBarCode.First().MaKP ?? 0;
                            var qkp = _Data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh && p.MaKP == makpcd).ToList();
                            if (qkp.Count > 0)
                                Checktamthu = true;
                        }
                    }
                }
                if (!Checktamthu)
                    Checktamthu = DungChung.Ham._checkTamThu(_Data, mabn, IDCLS); // thông báo chưa tạm thu dịch vụ
                                                                                  //var CLSct = (from chidinh in _Data.ChiDinhs.Where(p => p.IdCLS == IDCLS)
                                                                                  //             join cls in _Data.CLScts on chidinh.IDCD equals cls.IDCD
                                                                                  //             join dvct in _Data.DichVucts on cls.MaDVct equals dvct.MaDVct
                                                                                  //             select new { cls.IDCD, chidinh.MaMay, cls.Status, tenxn = dvct.TenDVct, tsbt = dvct.TSBT, ketqua = cls.KetQua, Id = cls.Id, Madvct = dvct.MaDVct, STT = dvct.STT_F, STTHT = cls.STTHT }).ToList();

                if (DungChung.Bien.MaBV == "27023")
                {
                    string strSQL = "st_kqxn_CLSct_ThuThang_BV";

                    string[] strpara = new string[] { "@IDCLS", "@MaBV" };
                    object[] oValue = new object[] { IDCLS, DungChung.Bien.MaBV };
                    SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar };

                    connect.Connect();
                    _tbCLSct = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);
                }
                else
                {
                    string strSQL = "sp_kqxn_CLSct";
                    if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "30303")
                        strSQL = "st_kqxn_CLSct_ThuThang";
                    string[] strpara = new string[] { "@IDCLS" };
                    object[] oValue = new object[] { IDCLS };
                    SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Int };

                    connect.Connect();
                    _tbCLSct = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);
                }

                foreach (DataRow row in _tbCLSct.Rows)
                {
                    _lIDCD.Add((int)row[0]);
                }

                if (DungChung.Bien.MaBV == "30372")
                {
                    foreach (DataRow row in _tbCLSct.Rows)
                    {
                        if (row["Is_Execute"] == "true")
                        {
                            colExecute.Image = imageList1.Images[0];
                            IsCheckAll = true;
                        }
                        else
                        {
                            colExecute.Image = imageList1.Images[1];
                            IsCheckAll = false;
                            break;
                        }
                    }
                }
                //if (DungChung.Bien.MaBV == "30372")//his-186 28/05/2021
                //{

                //}
                //else
                //{
                //    grcKetqua.DataSource = null;
                //}
                grcKetqua.DataSource = null;
                if (_tbCLSct.Rows.Count > 0)
                {

                    if (_tbCLSct.Rows[0]["MaMay"] != null && _tbCLSct.Rows[0]["MaMay"].ToString() != "")
                    {
                        lupMaMay.EditValue = _tbCLSct.Rows[0]["MaMay"];
                    }
                    if ((!string.IsNullOrEmpty(_tbCLSct.Rows[0]["Status"].ToString()) && Convert.ToInt32(_tbCLSct.Rows[0]["Status"]) == 1) || Checktamthu == false)
                    {
                        EnableControl(false);
                        //grvketqua.OptionsBehavior.ReadOnly = true;
                        if (Checktamthu == false)
                            sbtSua.Enabled = false;
                        else if (!string.IsNullOrEmpty(_tbCLSct.Rows[0]["Status"].ToString()) && Convert.ToInt32(_tbCLSct.Rows[0]["Status"]) == 1)
                            sbtSua.Enabled = true;
                    }
                    else
                    {
                        EnableControl(true);
                        //grvketqua.OptionsBehavior.ReadOnly = false;
                        lupNgayTH.DateTime = System.DateTime.Now;
                    }

                    #region linq
                    //foreach (var c in CLSct)
                    //{
                    //    Ketquachitiet themmoi = new Ketquachitiet();
                    //    themmoi.IDCD = c.IDCD ?? 0;
                    //    themmoi.id = c.Id.ToString();
                    //    if (c.STTHT != null)
                    //    {
                    //        themmoi.sttht = c.STTHT.Value;
                    //    }
                    //    else
                    //    { themmoi.sttht = 0; }
                    //    themmoi.ketqua = c.ketqua;
                    //    themmoi.tenxn = c.tenxn;
                    //    themmoi.tsbt = c.tsbt;
                    //    if (c.STT != null)
                    //    {
                    //        themmoi.stt = c.STT.Value;
                    //    }
                    //    else
                    //    {
                    //        themmoi.stt = 0;
                    //    }
                    //    themmoi.madvct = c.Madvct;
                    //    themmoi.Status = c.Status == null ? 0 : c.Status.Value;
                    //    _kpct.Add(themmoi);
                    //}
                    #endregion

                    grcKetqua.DataSource = _tbCLSct;//_kpct.OrderBy(p => p.stt);
                    string sTenRG = GrvNhomDV.GetFocusedRowCellValue("TenRG").ToString();
                    //var TenRG = (from trg in _Data.TieuNhomDVs.Where(p => p.TenRG == sTenRG) select new { trg.TenRG }).ToList();
                    if (sTenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNNuocTieu || sTenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNDom)
                    //if (TenRG.First().TenRG.Contains("XN nước tiểu") || TenRG.First().TenRG.Contains("XN đờm"))
                    {
                        // if (CLSct.Count > 0 && CLSct.First().Status == 0)
                        if (_tbCLSct.Rows.Count > 0 && !string.IsNullOrEmpty(_tbCLSct.Rows[0]["Status"].ToString()) && Convert.ToInt32(_tbCLSct.Rows[0]["Status"].ToString()) == 0)
                        {
                            for (int i = 0; i < grvketqua.RowCount; i++)
                            {
                                string madvct = "";
                                if (grvketqua.GetRowCellValue(i, MaDVct) != null && grvketqua.GetRowCellValue(i, MaDVct).ToString() != "")
                                {
                                    madvct = grvketqua.GetRowCellValue(i, MaDVct).ToString();
                                    var kq = _Data.DichVucts.Where(p => p.MaDVct == madvct).ToList();
                                    if (kq.Count > 0 && !string.IsNullOrWhiteSpace(kq.First().KetQua))
                                    {
                                        string[] ketqua = kq.First().KetQua.Split(';');
                                        if (ketqua.Count() > 0)
                                        {
                                            grvketqua.SetRowCellValue(i, colketqua, ketqua[0]);
                                            grvketqua.SetRowCellValue(i, STT, 1);
                                        }
                                    }

                                }

                            }
                        }
                        STT.Visible = true;
                    }
                    else
                    {
                        cboketqua.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                        STT.Visible = false;
                    }

                }
            }
            if (GrvNhomDV.GetFocusedRowCellValue("IDCLS") != null && GrvNhomDV.GetFocusedRowCellValue("IDCLS").ToString() != "")
            {
                int idcls = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDCLS").ToString());
                var TenCB = (from cls in _Data.CLS.Where(p => p.IdCLS == idcls)
                             select new { cls.MaCBth, cls.NgayTH }).ToList();
                if (TenCB.Count > 0)
                {
                    DungChung.Bien.maidcls = idcls;
                    if (TenCB.First().MaCBth != null && TenCB.First().MaCBth != "")
                        LupCanBo.EditValue = TenCB.First().MaCBth;
                    else
                        LupCanBo.EditValue = DungChung.Bien.MaCB;
                    if (TenCB.First().NgayTH != null)
                        lupNgayTH.DateTime = TenCB.First().NgayTH.Value;
                    else
                        lupNgayTH.DateTime = System.DateTime.Now;
                }
                else
                {
                    LupCanBo.EditValue = DungChung.Bien.MaCB;
                    lupNgayTH.DateTime = System.DateTime.Now;
                }
                //if (DungChung.Bien.MaBV == "27001")
                //    txtBarcode.Focus();
                //int idTN = 0;
                //if(GrvNhomDV.GetFocusedRowCellValue("TenRG") != null)
                //{
                //    string tenRG = GrvNhomDV.GetFocusedRowCellValue("TenRG").ToString();
                //    var qcd = (from cd in _Data.ChiDinhs.Where(p => p.IdCLS == idcls)
                //               join dv in _Data.DichVus on cd.MaDV equals dv.MaDV
                //               join tn in _Data.TieuNhomDVs.Where(p => p.TenRG == tenRG) on dv.IdTieuNhom equals tn.IdTieuNhom
                //               select cd).FirstOrDefault();
                //    if(qcd != null)
                //    {
                //        txtMoTa.Text = qcd.GhiChu;
                //        _idcd = qcd.IDCD;
                //    }
                //    else
                //    {
                //        txtMoTa.Text = "";
                //        _idcd = 0;
                //    }

                //}
            }
            else
            {
                LupCanBo.EditValue = DungChung.Bien.MaCB;
                lupNgayTH.DateTime = System.DateTime.Now;
                grcKetqua.DataSource = null;
            }

            if (DungChung.Bien.MaBV == "30372")
            {
                grvketqua.SelectAll();
            }

            //if (DungChung.Bien.MaBV == "27194")//minhvd
            
            if (GrvNhomDV.GetFocusedRowCellValue("TenRG") != null)
            {

                string IDTN = GrvNhomDV.GetFocusedRowCellValue("TenRG").ToString();
                var tn = (from cls in _Data.TieuNhomDVs.Where(p => p.TenRG == IDTN)
                            join ts in _Data.TaiSans on cls.IdTieuNhom equals ts.IdTieuNhom
                            select new { cls, ts }).ToList();
                if (tn.Count > 0)
                {
                    string aa = GrvNhomDV.GetFocusedRowCellValue("Status").ToString();
                    if (aa != "1")
                    {
                        int _MaKP = 0; string Makp = "";
                        if (LupKhoaphong.EditValue != null)
                        {
                            _MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
                            Makp = ";" + _MaKP.ToString() + ";";
                        }
                        if (tn.Count > 0)
                        {
                            if (tn.First().ts.IdTieuNhom != null)
                            {
                                int c = Convert.ToInt32(tn.First().ts.IdTieuNhom);
                                getMaMay(_MaKP, c);
                            }
                        }
                        if (tn.First().ts.IdTieuNhom != null)
                        {
                            int c = Convert.ToInt32(tn.First().ts.IdTieuNhom);
                            var tn1 = (_Data.TaiSans.Where(p => p.IdTieuNhom == c)).ToList();
                            for (int i = 0; i < tn1.Count(); i++)
                            {
                                lupMaMay.ItemIndex = i;
                                if (lupMaMay.GetColumnValue("TenDV") != null)
                                {
                                    string a = tn.First().ts.GhiChu;
                                    string b = lupMaMay.GetColumnValue("TenDV").ToString();
                                    if (a == b)
                                    {
                                        lupMaMay.ItemIndex = i;
                                        break;
                                    }
                                    else
                                    {
                                        lupMaMay.ItemIndex = -1;
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        int a = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IdCLS"));
                        var cd = (from chidinh in _Data.ChiDinhs.Where(p => p.IdCLS == a)
                                  join cls in _Data.CLS.Where(p => p.MaBNhan == _Mabn) on chidinh.IdCLS equals cls.IdCLS
                                  select new { cls, chidinh }).ToList();
                        int _MaKP = 0; string Makp = "";
                        if (LupKhoaphong.EditValue != null)
                        {
                            _MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
                            Makp = ";" + _MaKP.ToString() + ";";
                        }
                        if (tn.Count > 0)
                        {
                            if (tn.First().ts.IdTieuNhom != null)
                            {
                                int c = Convert.ToInt32(tn.First().ts.IdTieuNhom);
                                getMaMay(_MaKP, c);
                            }
                        }
                    }
                }
                else
                {
                    int _MaKP = 0; string Makp = "";
                    if (LupKhoaphong.EditValue != null)
                    {
                        _MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
                        Makp = ";" + _MaKP.ToString() + ";";
                    }
                    getMaMay(_MaKP);
                }
                
            }

            
            //if(DungChung.Bien.MaBV == "01071")
            //{
            //    txtBarcode.Focus();
            //}
        }

        // int _idcd = 0; //lưu id chỉ định
        private void sbtSua_Click(object sender, EventArgs e)
        {
            DateTime ngay = Convert.ToDateTime("2018-01-31 23:59:59");
            string tendn = DungChung.Bien.TenDN;
            bool _checktamthu = false;

            int _bnBHYT = _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn && p.DTuong == "BHYT").Count();
            var _bnDichvu = (from bn in _Data.BenhNhans.Where(p => p.MaBNhan == _Mabn && p.DTuong == "Dịch vụ")
                             join cls in _Data.CLS on bn.MaBNhan equals cls.MaBNhan
                             join cd in _Data.ChiDinhs.Where(p => p.SoPhieu != null) on cls.IdCLS equals cd.IdCLS
                             select new { bn.MaBNhan, bn.TenBNhan, cd.SoPhieu }).ToList();

            var q = (from ad in _Data.ADMINs.Where(p => p.TenDN == tendn)
                     join cb in _Data.CanBoes on ad.MaCB equals cb.MaCB
                     join kp in _Data.KPhongs.Where(p => p.PLoai == "Admin") on cb.MaKP equals kp.MaKP
                     select new { ad.TenDN, ad.MaCB, kp.PLoai }).Distinct().ToList();
            if (DungChung.Bien.MaBV == "30372" && q.Count > 0)
            {
                int _IDCL = 0;
                if (GrvNhomDV.GetFocusedRowCellValue(IDCLS) != null && GrvNhomDV.GetFocusedRowCellValue(IDCLS).ToString() != "")
                {
                    _IDCL = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(IDCLS));
                }
                bool Checktamthu = false;
                if (!Checktamthu)
                    Checktamthu = DungChung.Ham._checkTamThu(_Data, _Mabn, _IDCL); // thông báo chưa tạm thu dịch vụ

                if (Checktamthu && (_bnBHYT > 0 || _bnDichvu.Count > 0))
                {
                    EnableControl(true);
                }
                else
                {
                    EnableControl(false);
                    MessageBox.Show("Tài khoản của bạn không có quyền hoặc dịch vụ chưa được thu trực tiếp");
                }

            }
            else
            {
                if (DungChung.Bien.MaBV == "27023" && DateTime.Now < ngay)
                {
                    EnableControl(true);
                }
                else
                {
                    var KT = (from rv in _Data.RaViens.Where(p => p.MaBNhan == _Mabn) select new { rv.MaBNhan }).ToList();
                    if (KT.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã ra viện bạn không được sửa kết quả!");
                    }
                    else
                    {
                        EnableControl(true);
                    }
                }
            }
        }

        private void sbtXoa_Click(object sender, EventArgs e)
        {
            var KT = (from RV in _Data.RaViens.Where(p => p.MaBNhan == _Mabn) select new { RV.MaBNhan }).ToList();
            if (KT.Count > 0)
            {
                MessageBox.Show("Bệnh nhân đã ra viện bạn không thể xóa kết quả!");
            }
            else
            {
                if (GrvNhomDV.GetFocusedRowCellValue("IDCLS") != null && GrvNhomDV.GetFocusedRowCellValue("IDCLS").ToString() != "")
                {
                    int IDCLS = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDCLS"));
                    var cd = (from chidinh in _Data.ChiDinhs.Where(p => _lIDCD.Contains(p.IDCD)) select new { chidinh.Status }).ToList();
                    if (cd.Count > 0)
                    {
                        if (cd.First().Status == 1)
                        {
                            DialogResult dia = MessageBox.Show("Bạn có chắc muốn xoá kết quả không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (dia == DialogResult.Yes)
                            {

                                int cls = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDCLS"));
                                var cd1 = (from chidinh in _Data.ChiDinhs.Where(p => _lIDCD.Contains(p.IDCD)) select new { chidinh.IDCD }).ToList();
                                if (cd.Count > 0)
                                {
                                    int _maCK = 0;
                                    string sqldv = "SELECT DonGia, MaDV, DonVi, TrongDM FROM dbo.NhomDV JOIN dbo.DichVu ON DichVu.IDNhom = NhomDV.IDNhom WHERE  TenNhomCT LIKE N'%Khám bệnh%' and dbo.DichVu.PLoai = 2 AND DichVu.Status = 1 ORDER BY DonGia DESC";

                                    connect.Connect();
                                    DataTable tbck = connect.FillDatatable(sqldv, CommandType.Text);
                                    if (tbck.Rows.Count > 0)
                                        _maCK = Convert.ToInt32(tbck.Rows[0]["MaDV"]);
                                    //int _maCK = 0;
                                    //var ck = (from nhom in _Data.NhomDVs.Where(p => p.TenNhomCT.Contains("Khám bệnh"))
                                    //          join dvu in _Data.DichVus.Where(p => p.PLoai == 2).Where(p => p.Status == 1) on nhom.IDNhom equals dvu.IDNhom
                                    //          select new { dvu.DonGia, dvu.MaDV, dvu.DonVi, dvu.TrongDM }).OrderByDescending(p => p.DonGia).ToList();
                                    //if (ck.Count > 0)
                                    //    _maCK = ck.First().MaDV;
                                    foreach (var c in cd1)
                                    {
                                        var dtct = (from dt in _Data.DThuoccts.Where(p => p.IDCD == c.IDCD && p.MaDV != _maCK) select new { dt.IDDonct }).ToList();
                                        if (dtct.Count > 0)
                                        {
                                            foreach (var v in dtct)
                                            {
                                                var kiemtrachiphidinhkem = _Data.DThuoccts.Where(p => p.AttachIDDonct == v.IDDonct).ToList();
                                                if (kiemtrachiphidinhkem.Count > 0)
                                                {
                                                    MessageBox.Show("dịch vụ đã có VTYT-HC đi kèm, bạn không thể xóa");
                                                    return;
                                                }
                                                var xoa = _Data.DThuoccts.Single(p => p.IDDonct == v.IDDonct);
                                                _Data.DThuoccts.Remove(xoa);
                                                _Data.SaveChanges();
                                            }
                                        }
                                        ChiDinh sua = _Data.ChiDinhs.Single(p => p.IDCD == (c.IDCD));
                                        sua.Status = 0;
                                        _Data.SaveChanges();

                                    }
                                }
                                //for (int i = 0; i < _kpct.Count; i++)
                                //{
                                //    string id = _kpct.Skip(i).First().id;
                                //    int ID = Convert.ToInt32(id);
                                //    CLSct sua = _Data.CLScts.Single(p => p.Id == (ID));
                                //    sua.KetQua = "";
                                //    sua.Status = 0;
                                //    _Data.SaveChanges();


                                //}
                                for (int i = 0; i < _tbCLSct.Rows.Count; i++)
                                {
                                    string id = _tbCLSct.Rows[i]["id"].ToString();
                                    int ID = Convert.ToInt32(id);
                                    CLSct sua = _Data.CLScts.Single(p => p.Id == (ID));
                                    sua.KetQua = "";
                                    sua.Status = 0;
                                    _Data.SaveChanges();
                                }

                                var clsang = _Data.CLS.Where(p => p.IdCLS == cls).FirstOrDefault();
                                if (clsang != null)
                                {
                                    clsang.Status = 0;
                                    clsang.MaCBth = null;
                                    clsang.NgayTH = null;
                                }
                                var cdc = _Data.ChiDinhs.Where(p => p.IdCLS == cls).FirstOrDefault();
                                if (cdc != null)
                                {
                                    cdc.Status = 0;

                                }
                                _Data.SaveChanges();
                                FRM_chidinh_Moi._setStatus(_Mabn);
                                if (DungChung.Bien.MaBV == "08204")
                                {
                                    QLBV.CLS.Connect_LIS.getFileBackup(IDCLS, DungChung.Bien.xmlFilePath_LIS[1], _Data, DungChung.Bien.xmlFilePath_LIS[2]);
                                }
                                MessageBox.Show("Xoá thành công");
                                GrvNhomDV_FocusedRowChanged(null, null);
                                EnableControl(true);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Chưa có kết quả để xóa");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 0:maICD, 1:chandoan+benhkhac, 2: buồng, 3: giường, 4: tenKP chỉ định
        /// </summary>
        /// <param name="mabn">mã bệnh nhân</param>
        /// <param name="makp">mã khoa phòng chỉ định</param>
        /// <returns></returns>

        private void GrvNhomDV_DataSourceChanged(object sender, EventArgs e)
        {
            GrvNhomDV_FocusedRowChanged(null, null);
        }



        private void RAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!load)
                TimKiem();
        }

        private void grvBenhnhan_DataSourceChanged(object sender, EventArgs e)
        {
            grvBenhnhan_FocusedRowChanged_1(null, null);
        }

        private void GrvNhomDV_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (GrvNhomDV.GetRowCellValue(e.RowHandle, colTenRG) != null)
            {
                if (GrvNhomDV.GetRowCellValue(e.RowHandle, colTenRG).ToString() == "XN hóa sinh máu")
                    e.Appearance.ForeColor = Color.Blue;
                else
                    if (GrvNhomDV.GetRowCellValue(e.RowHandle, colTenRG).ToString() == "XN huyết học")
                        e.Appearance.ForeColor = Color.Red;
            }
        }

        private void GrvKetQua_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) //minhvd
        {
            if (grvketqua.GetRowCellValue(e.RowHandle, Tenxn) != null && DungChung.Bien.MaBV == "24012")
            {
                if (grvketqua.GetRowCellValue(e.RowHandle, Tenxn).ToString() == "Số lượng bạch cầu" || grvketqua.GetRowCellValue(e.RowHandle, Tenxn).ToString() == "Số lượng HC" || grvketqua.GetRowCellValue(e.RowHandle, Tenxn).ToString() == "Số lượng tiểu cầu")
                {
                    //e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font =  new System.Drawing.Font(this.Font, FontStyle.Bold);
                }
            }
        }

        private void txttimten_Leave(object sender, EventArgs e)
        {

        }

        private void lupNgaytu_Leave(object sender, EventArgs e)
        {
            tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            if (!load)
            {
                TimKiem();
            }
        }

        private void lupNgayden_Leave(object sender, EventArgs e)
        {
            denngay = DungChung.Ham.NgayDen(lupNgayden.DateTime);
            if (!load)
                TimKiem();
        }

        private void grvketqua_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //Lnuoctieu themmoi = new Lnuoctieu();
            //themmoi.gtri = "+";
            //themmoi.stt = 1;
            //_Lnuoctieu.Add(themmoi);
            //Lnuoctieu themmoi1 = new Lnuoctieu();
            //themmoi1.gtri = "++";
            //themmoi1.stt = 2;
            //_Lnuoctieu.Add(themmoi1);
            //Lnuoctieu themmoi2 = new Lnuoctieu();
            //themmoi2.gtri = "+++";
            //themmoi2.stt = 3;
            //_Lnuoctieu.Add(themmoi2);
            //Lnuoctieu themmoi3 = new Lnuoctieu();
            //themmoi3.gtri = "Âm tính";
            //themmoi3.stt = 4;
            //_Lnuoctieu.Add(themmoi3);
            //Lnuoctieu themmoi4 = new Lnuoctieu();
            //themmoi4.gtri = "Vết";
            //themmoi4.stt = 5;
            //_Lnuoctieu.Add(themmoi4);
            //Lnuoctieu themmoi5 = new Lnuoctieu();
            //themmoi5.gtri = "++++";
            //themmoi5.stt = 6;
            //_Lnuoctieu.Add(themmoi5);
            _Lnuoctieu.Clear();
            cboketqua.Items.Clear();
            cboSTT.Items.Clear();
            string madvct = "";
            if (grvketqua.GetFocusedRowCellValue("madvct") != null && grvketqua.GetFocusedRowCellValue("madvct").ToString() != "")
            {
                madvct = grvketqua.GetFocusedRowCellValue("madvct").ToString();
            }
            var kq = _Data.DichVucts.Where(p => p.MaDVct == madvct).ToList();
            if (kq.Count > 0)
            {
                String ketqua = kq.First().KetQua;
                if (ketqua != null)
                {
                    int stt = 0;
                    foreach (var item in ketqua.Split(';'))
                    {
                        stt++;
                        Lnuoctieu themmoi = new Lnuoctieu();
                        themmoi.gtri = item;
                        themmoi.stt = stt;
                        _Lnuoctieu.Add(themmoi);
                    }
                }
            }
            foreach (var item in _Lnuoctieu)
            {
                cboketqua.Items.Add(item.gtri);
                cboSTT.Items.Add(item.stt);
            }
            
        }

        private void grvketqua_DataSourceChanged(object sender, EventArgs e)
        {
            grvketqua_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0));
        }
        public class KetQua_B
        {
            private int id;
            private string barCode;
            private int maDV;
            private string tenDV;
            private string tenTN;
            private string maDvct;
            private string tenDvct;
            private string ketQua;
            private string tenRG;
            private string idCls;
            private string maBNhan;
            private string tenBNhan;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }
            public string BarCode
            {
                get { return barCode; }
                set { barCode = value; }
            }
            public int madv
            {
                get { return maDV; }
                set { maDV = value; }
            }
            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
            public string TenTN
            {
                get { return tenTN; }
                set { tenTN = value; }
            }
            public string madvct
            {
                get { return maDvct; }
                set { maDvct = value; }
            }
            public string TenDvct
            {
                get { return tenDvct; }
                set { tenDvct = value; }
            }
            public string KetQua
            {
                get { return ketQua; }
                set { ketQua = value; }
            }
            public string TenRG
            {
                get { return tenRG; }
                set { tenRG = value; }
            }
            public string IdCls
            {
                get { return idCls; }
                set { idCls = value; }
            }
            public string MaBNhan
            {
                get { return maBNhan; }
                set { maBNhan = value; }
            }
            public string TenBNhan
            {
                get { return tenBNhan; }
                set { tenBNhan = value; }
            }
        }
        List<Library_CLS.Lis_His.clsChiDinhCLS> _lresult_LIS = new List<Library_CLS.Lis_His.clsChiDinhCLS>();

        class dsBNcoKQCLS
        {
            DateTime ngayTH;

            public DateTime NgayTH
            {
                get { return ngayTH; }
                set { ngayTH = value; }
            }
            string tenBN;

            public string TenBN
            {
                get { return tenBN; }
                set { tenBN = value; }
            }
        }
        static List<dsBNcoKQCLS> _lds = new List<dsBNcoKQCLS>();
        static void Auto_result_CLS(DateTime date_result)
        {
            // test xoa
            //_lds.Add(new dsBNcoKQCLS { NgayTH = DateTime.Now, TenBN = DateTime.Now.Date.Millisecond.ToString() });
            DateTime ngayTH = DateTime.Now;
            List<Ketquachitiet> _kpct = new List<Ketquachitiet>();
            DateTime ngaytu = DungChung.Ham.NgayTu(date_result);
            DateTime ngayden = DungChung.Ham.NgayDen(date_result);
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var listCLS = (from cls in _data.CLS.Where(p => p.NgayThang >= ngaytu && p.NgayThang <= ngayden)
                           join cd in _data.ChiDinhs.Where(p => p.Status == 0) on cls.IdCLS equals cd.IDCD
                           select new { cls.MaBNhan, cls.BarCode, cls.IdCLS, cd.IDCD }).ToList();
            List<int> _idcls = new List<int>();
            _idcls = listCLS.Select(p => p.IdCLS).Distinct().ToList();
            var bacode = listCLS.Select(p => p.BarCode).Distinct().ToList();
            foreach (var item in bacode)
            {
                List<Library_CLS.Lis_His.clsChiDinhCLS> _lresult_LIS = new List<Library_CLS.Lis_His.clsChiDinhCLS>();
                _lresult_LIS = Library_CLS.Lis_His.getDS_CLSct(item, _idcls, DungChung.Bien.xmlFilePath_LIS[1]); // updateing
                if (_lresult_LIS.Where(p => p.KetQua != null && p.KetQua != "").ToList().Count > 0)
                {
                    List<int> dsIDCD = (from a in listCLS.Where(p => p.BarCode == item) select a.IDCD).ToList();

                    // update CLSct
                    List<CLSct> updateCLSct = (from cd in _data.ChiDinhs
                                               join clsct in _data.CLScts on cd.IDCD equals clsct.IDCD
                                               where dsIDCD.Contains(cd.IDCD)
                                               select clsct).ToList();
                    foreach (var item2 in updateCLSct)
                    {
                        foreach (var item3 in _lresult_LIS)
                        {
                            if (item2.MaDVct == item3.MaDV && !string.IsNullOrEmpty(item3.KetQua))
                            {
                                item2.KetQua = item3.KetQua;
                                item2.Status = 1;
                                item2.STTHT = 0;
                            }
                        }
                        _data.SaveChanges();
                    }
                    // update Chi DInh
                    List<ChiDinh> updateCD = (from cd in _data.ChiDinhs
                                              where dsIDCD.Contains(cd.IDCD)
                                              select cd).ToList();
                    foreach (var item2 in updateCD)
                    {
                        if (item2.Status == 0 || item2.Status == null)
                        {
                            item2.Status = 1;
                            _data.SaveChanges();
                        }
                    }
                    // update CLS
                    List<int> dsIDCLS = (from a in listCLS.Where(p => p.BarCode == item) select a.IdCLS).ToList();
                    List<CL> updateCLS = (from cd in _data.CLS
                                          where dsIDCLS.Contains(cd.IdCLS)
                                          select cd).ToList();
                    int _int_maBN = 0;
                    foreach (var item2 in updateCLS)
                    {
                        _int_maBN = item2.MaBNhan ?? 0;
                        item2.MaCBth = DungChung.Bien.MaCB;
                        item2.NgayTH = ngayTH;
                        var ktstatuscd = _data.ChiDinhs.Where(p => p.IdCLS == item2.IdCLS).Where(p => p.Status == 0 || p.Status == null).ToList();
                        if (ktstatuscd.Count > 0)
                            item2.Status = 0;
                        else
                        {
                            item2.Status = 1;
                            BenhNhan sua = _data.BenhNhans.Where(p => p.MaBNhan == item2.MaBNhan).FirstOrDefault();
                            _lds.Add(new dsBNcoKQCLS { NgayTH = ngayTH, TenBN = sua.TenBNhan });
                            if (sua != null)
                            {
                                sua.Status = 5;
                            }
                        }
                        _data.SaveChanges();
                        int _idkb = 0;
                        var bnkb = _data.BNKBs.Where(p => p.MaBNhan == _int_maBN && p.MaKP == item2.MaKP).OrderByDescending(p => p.IDKB).FirstOrDefault();
                        if (bnkb != null)
                            _idkb = bnkb.IDKB;// so sánh ngày khám bệnh và ngày thực hiện, để lấy đúng IDKB
                        var cdinh = (from cd1 in _data.ChiDinhs.Where(p => p.IdCLS == item2.IdCLS && p.Status == 1)
                                     join dv in _data.DichVus on cd1.MaDV equals dv.MaDV
                                     select new { cd1.MaDV, cd1.SoPhieu, cd1.DonGia, dv.DonVi, cd1.TrongBH, cd1.IDCD, cd1.XHH }).ToList();
                        int iddthuoc = 0;
                        var ktdthuoc = (_data.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2)).ToList();
                        if (ktdthuoc.Count > 0)
                            iddthuoc = ktdthuoc.First().IDDon;
                        if (iddthuoc > 0)
                        {
                            foreach (var cd2 in cdinh)
                            {
                                var kt = (from dt in _data.DThuoccts.Where(p => p.IDCD == cd2.IDCD) select dt).ToList();
                                if (kt.Count <= 0)
                                {
                                    double _dongia = DungChung.Ham._getGiaDM(_data, cd2.MaDV == null ? 0 : cd2.MaDV.Value, cd2.TrongBH == null ? 1 : cd2.TrongBH.Value, item2.MaBNhan ?? 0, ngayTH);
                                    DThuocct moi = new DThuocct();
                                    moi.MaDV = cd2.MaDV;
                                    moi.IDDon = iddthuoc;
                                    moi.IDKB = _idkb;
                                    moi.TrongBH = cd2.TrongBH == null ? 0 : cd2.TrongBH.Value;
                                    moi.IDCD = cd2.IDCD;
                                    moi.DonVi = cd2.DonVi;
                                    moi.MaKP = item2.MaKP;
                                    moi.NgayNhap = ngayTH;
                                    moi.DonGia = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                                    moi.ThanhTien = cd2.DonGia == 0 ? _dongia : cd2.DonGia;
                                    moi.SoLuong = 1;
                                    moi.XHH = cd2.XHH;
                                    moi.MaCB = DungChung.Bien.MaCB;
                                    moi.Status = 0;
                                    if (cd2.SoPhieu != null && cd2.SoPhieu > 0)
                                        moi.ThanhToan = 1;
                                    moi.TyLeTT = 100;
                                    moi.IDCLS = item2.IdCLS;
                                    _data.DThuoccts.Add(moi);
                                    _data.SaveChanges();
                                }
                                else
                                {
                                    //sửa
                                    foreach (var a in kt)
                                    {
                                        a.NgayNhap = ngayTH;
                                        a.IDCLS = item2.IdCLS;
                                        _data.SaveChanges();
                                    }
                                }
                            }
                        }
                        else
                        {

                            DThuoc dthuoccd = new DThuoc();
                            dthuoccd.NgayKe = ngayTH; // cần phải lấy theo ngày tháng nhập kết quả CLS
                            dthuoccd.MaBNhan = _int_maBN;
                            dthuoccd.PLDV = 2;
                            dthuoccd.MaKP = item2.MaKP;
                            dthuoccd.MaCB = DungChung.Bien.MaCB;
                            dthuoccd.KieuDon = -1;
                            _data.DThuocs.Add(dthuoccd);
                            _data.SaveChanges();
                            var maxid = dthuoccd.IDDon;
                            foreach (var cd3 in cdinh)
                            {
                                double _dongia = DungChung.Ham._getGiaDM(_data, cd3.MaDV == null ? 0 : cd3.MaDV.Value, cd3.TrongBH == null ? 1 : cd3.TrongBH.Value, item2.MaBNhan ?? 0, ngayTH);
                                DThuocct moi = new DThuocct();
                                moi.MaDV = cd3.MaDV;
                                moi.IDKB = _idkb;
                                moi.IDDon = maxid;
                                moi.DonVi = cd3.DonVi;
                                moi.TrongBH = cd3.TrongBH == null ? 0 : cd3.TrongBH.Value;
                                moi.IDCD = cd3.IDCD;
                                moi.MaKP = item2.MaKP;
                                moi.DonGia = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                                moi.NgayNhap = ngayTH;
                                moi.ThanhTien = cd3.DonGia == 0 ? _dongia : cd3.DonGia;
                                moi.SoLuong = 1;
                                moi.MaCB = DungChung.Bien.MaCB;
                                moi.Status = 0;
                                moi.XHH = cd3.XHH;
                                if (cd3.SoPhieu != null && cd3.SoPhieu > 0)
                                    moi.ThanhToan = 1;
                                moi.TyLeTT = 100;
                                moi.IDCLS = item2.IdCLS;
                                _data.DThuoccts.Add(moi);
                                _data.SaveChanges();
                            }
                        }
                    }
                }
            }

        }

        private void btnLayKQ_Click(object sender, EventArgs e)
        {
            _lresult_LIS = new List<Library_CLS.Lis_His.clsChiDinhCLS>();
            List<int> _idcls = new List<int>();
            int _mabn = 0;
            if (!String.IsNullOrEmpty(txtMaBN.Text))
                _mabn = Convert.ToInt32(txtMaBN.Text);
            _idcls = _Data.CLS.Where(p => p.BarCode == txtBarcode.Text && p.MaBNhan == _mabn).Select(p => p.IdCLS).ToList();
            _lresult_LIS = Library_CLS.Lis_His.getDS_CLSct(txtBarcode.Text, _idcls, DungChung.Bien.xmlFilePath_LIS[1]); // updateing
            bool cokq = false;
            //foreach (var item in _kpct)
            //{
            //    foreach (var item2 in _lresult_LIS)
            //    {
            //        if (item.madvct == item2.MaDV.Trim() && !string.IsNullOrEmpty(item2.KetQua))
            //        {
            //            cokq = true;
            //            item.ketqua = item2.KetQua.Trim();
            //            break;
            //        }
            //    }
            //}
            for (int i = 0; i < _tbCLSct.Rows.Count; i++)
            {
                foreach (var item2 in _lresult_LIS)
                {
                    if (_tbCLSct.Rows[i]["madvct"].ToString() == item2.MaDV.Trim() && !string.IsNullOrEmpty(item2.KetQua))
                    {
                        cokq = true;
                        _tbCLSct.Rows[i]["ketqua"] = item2.KetQua.Trim();
                        break;
                    }
                }
            }
            grcKetqua.DataSource = null;
            grcKetqua.DataSource = _tbCLSct;
            if (cokq)
            {
                if (DialogResult.Yes == MessageBox.Show("Bạn muốn lưu dữ liệu?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    cboluu_Click(sender, e);
                }
            }
        }

        public string setBarCode(QLBV_Database.QLBVEntities _data, DateTime ngaygio)
        {

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
                int stt = Convert.ToInt32(q.First().Code.Split('_')[2]);
                return ngaygio.ToString("dd/MM/yyyy").Substring(0, 2) + ngaygio.ToString("dd/MM/yyyy").Substring(3, 2) + ngaygio.ToString("dd/MM/yyyy").Substring(8, 2) + "_" + _Mabn + "_" + (stt + 1);
            }
            else
            {
                return txtCode.Text = ngaygio.ToString("dd/MM/yyyy").Substring(0, 2) + ngaygio.ToString("dd/MM/yyyy").Substring(3, 2) + ngaygio.ToString("dd/MM/yyyy").Substring(8, 2) + "_" + _Mabn + "_" + 1;
            }
        }
        private void btnKhopMa_Click(object sender, EventArgs e)
        {
            #region tạo chỉ định file XML
            // update BarCode
            string barcode = txtBarcode.Text.Trim();
            if (DungChung.Bien.MaBV == "27023" && barcode.Length > barCode_Number)
            {
                MessageBox.Show("Barcode vượt quá ký tự cho phép");
                return;
            }
            if (!string.IsNullOrEmpty(txtBarcode.Text))
            {
                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                #region bỏ
                //DataRow[] foundRows;
                //foundRows = _tbCLS.Select("strChon = 1 and Status = 0");
                //for (int i = 0; i < foundRows.Length; i++)
                //{
                //    if (!string.IsNullOrEmpty(foundRows[i]["BarCode"].ToString()) && foundRows[i]["BarCode"].ToString() != barcode)
                //    {
                //        if (DialogResult.No == MessageBox.Show("Chỉ định đã được khớp mã barcode: '" + foundRows[i]["BarCode"].ToString() + "' bạn muốn khớp lại mã?", "Khớp mã!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                //            return;
                //    }
                //}
                //DataRow[] foundRows2;
                //foundRows2 = _tbCLS.Select("Status = 0");
                //for (int i = 0; i < foundRows.Length; i++)
                //{
                //    if (foundRows2[i]["strChon"].ToString() == "1")
                //    {
                //        int idcls = Convert.ToInt32(foundRows2[i]["IDCLS"].ToString());
                //        var sua = _Data.CLS.Where(p => p.IdCLS == idcls).ToList();
                //        foreach (var b in sua)
                //        {
                //            //b.Code = setBarCode(_Data, lupNgayTH.DateTime);
                //            b.BarCode = barcode;
                //            _Data.SaveChanges();
                //            if (!CLS.Connect_LIS.XML_ChiDinhCLS(_Data, DungChung.Bien.xmlFilePath_LIS[0], b.IdCLS))
                //            {
                //                MessageBox.Show("Lỗi khớp BarCode cho IDCLS:" + b.IdCLS);
                //                b.BarCode = foundRows2[i]["BarCode"].ToString();
                //                _Data.SaveChanges();
                //            }

                //        }
                //    }
                //}

                #endregion
                if (DungChung.Bien.MaBV != "27023")
                {
                    foreach (var a in _lCLS.Where(p => p.Chon && p.Status == 0))
                    {
                        if (!string.IsNullOrEmpty(a.BarCode) && a.BarCode != barcode)
                        {
                            if (DialogResult.No == MessageBox.Show("Chỉ định đã được khớp mã barcode: '" + a.BarCode + "' bạn muốn khớp lại mã?", "Khớp mã!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                                return;
                        }
                    }
                }
                foreach (var a in _lCLS.Where(p => p.Status == 0))
                {
                    if (DungChung.Bien.MaBV == "27023")
                    {
                        var sua = _Data.CLS.Where(p => p.IdCLS == a.IDCLS).ToList();
                        foreach (var b in sua)
                        {
                            //b.Code = setBarCode(_Data, lupNgayTH.DateTime);
                            b.BarCode = barcode;
                            b.Barcode_Time = DateTime.Now;
                            _Data.SaveChanges();
                            MessageBox.Show("Khớp mã thành công");
                        }
                    }
                    else if (a.Chon == true)
                    {
                        var sua = _Data.CLS.Where(p => p.IdCLS == a.IDCLS).ToList();
                        foreach (var b in sua)
                        {
                            //b.Code = setBarCode(_Data, lupNgayTH.DateTime);
                            b.BarCode = barcode;
                            _Data.SaveChanges();
                            if (!QLBV.CLS.Connect_LIS.XML_ChiDinhCLS(_Data, DungChung.Bien.xmlFilePath_LIS[0], b.IdCLS))
                            {
                                MessageBox.Show("Lỗi khớp BarCode cho IDCLS:" + b.IdCLS);
                                b.BarCode = a.BarCode;
                                _Data.SaveChanges();
                            }

                        }
                    }
                }
                grvBenhnhan_FocusedRowChanged_1(null, null);
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập mã BarCode");
            }


            #endregion

        }

        private void grcBenhnhan_Click(object sender, EventArgs e)
        {

        }

        private void cboNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!load)
                TimKiem();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            int _IDCL = 0;
            if (GrvNhomDV.GetFocusedRowCellValue(IDCLS) != null && GrvNhomDV.GetFocusedRowCellValue(IDCLS).ToString() != "")
            {
                _IDCL = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(IDCLS));
            }
            FormThamSo.Frm_HuyCLS frm = new Frm_HuyCLS(_IDCL, false);
            frm.ShowDialog();
            GrvNhomDV_FocusedRowChanged(null, null);
        }

        private void btn_FilePath_Click(object sender, EventArgs e)
        {
            int _IDCL = 0, _MaKP = 0;
            string Makp = "";
            if (LupKhoaphong.EditValue != null)
            {
                _MaKP = Convert.ToInt32(LupKhoaphong.EditValue);
                Makp = ";" + _MaKP.ToString() + ";";

                if (GrvNhomDV.GetFocusedRowCellValue(IDCLS) != null && GrvNhomDV.GetFocusedRowCellValue(IDCLS).ToString() != "")
                {
                    _IDCL = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue(IDCLS));

                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    ChiDinh cd = data.ChiDinhs.Where(p => p.IdCLS == _IDCL).FirstOrDefault();
                    bool ktra = false;
                    if (cd != null)
                    {
                        var qtn = (from tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc) join dv in data.DichVus.Where(p => p.MaDV == cd.MaDV.Value) on tn.IdTieuNhom equals dv.IdTieuNhom select tn).FirstOrDefault();
                        if (qtn != null)
                        {
                            ktra = true;

                        }
                    }
                    bool ktra1 = false;
                    if (cd != null)
                    {
                        var qtn = (from tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNTeBaoHoc) join dv in data.DichVus.Where(p => p.MaDV == cd.MaDV.Value) on tn.IdTieuNhom equals dv.IdTieuNhom select tn).FirstOrDefault();
                        if (qtn != null)
                        {
                            ktra1 = true;

                        }
                    }
                    if (ktra)
                    {
                        FormNhap.frm_ChiDinhBNLao frm = new FormNhap.frm_ChiDinhBNLao(_IDCL, Makp, _Mabn, 1);
                        frm.passData = new FormNhap.frm_ChiDinhBNLao.PassData(passData);
                        frm.ShowDialog();
                        //  frm.ShowDialog();
                    }
                    else if (ktra1)
                    {
                        FormNhap.frm_ChiDinhBNLao frm = new FormNhap.frm_ChiDinhBNLao(_IDCL, Makp, _Mabn, 2);
                        frm.passData = new FormNhap.frm_ChiDinhBNLao.PassData(passData);
                        frm.ShowDialog();
                    }
                    else
                    {
                        FormNhap.frm_ChiDinhBNLao frm = new FormNhap.frm_ChiDinhBNLao(_IDCL, Makp);
                        frm.ShowDialog();
                    }
                }
            }

        }

        private void passData(string maCBTH, DateTime? ngayTH)
        {
            LupCanBo.EditValue = maCBTH;
            if (ngayTH != null)
                lupNgayTH.DateTime = ngayTH.Value;
            cboluu_Click(null, null);
        }

        // in đông máu cho bệnh viện thường tín
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (GrvNhomDV.GetFocusedRowCellValue("IDCLS") != null && GrvNhomDV.GetFocusedRowCellValue("IDCLS").ToString() != "" && GrvNhomDV.GetFocusedRowCellValue("TenRG") != null && !string.IsNullOrEmpty(txtMaBN.Text))
            {
                //   int idtn = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDTN"));
                int idcls = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDCLS"));
                string TTN = GrvNhomDV.GetFocusedRowCellValue("TenRG").ToString();
                if (DungChung.Bien.MaBV == "30012")
                {
                    QLBV.CLS.InPhieu.PhieuXNDM_30012(Convert.ToInt32(txtMaBN.Text), idcls);
                }
                else if (DungChung.Bien.MaBV == "30005")
                {
                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    int IDCLS = Convert.ToInt32(idcls);
                    int cd = (data.ChiDinhs.Where(p => p.IdCLS == IDCLS).Count() > 0) ? data.ChiDinhs.Where(p => p.IdCLS == IDCLS).Min(p => p.IDCD) : 0;

                    var bn = (from a in data.CLS.Where(p => p.IdCLS == IDCLS)
                              join b in data.BenhNhans on a.MaBNhan equals b.MaBNhan
                              join c in data.CanBoes on a.MaCBth equals c.MaCB
                              join e1 in data.KPhongs on a.MaKP equals e1.MaKP
                              join e2 in data.KPhongs on b.MaKP equals e2.MaKP
                              select new
                              {
                                  b.MaBNhan,
                                  a.IdCLS,
                                  b.TenBNhan,
                                  b.Tuoi,
                                  GTinh = (b.GTinh == 1) ? "Nam" : "Nữ",
                                  b.DChi,
                                  b.SThe,
                                  c.TenCB,
                                  TenKPdt = e2.TenKP,
                                  TenKPyc = e1.TenKP,
                                  MaKP = b.MaKP ?? 0,
                                  a.ChanDoan,
                                  a.MaICD
                              }).ToList();

                    var cls = (from a in data.ChiDinhs.Where(p => p.IdCLS == IDCLS).Where(p => p.IDCD == cd)
                               join b in data.CLScts on a.IDCD equals b.IDCD
                               join c in data.DichVucts on b.MaDVct equals c.MaDVct
                               select new
                               {
                                   a.IdCLS,
                                   tenxetnghiem = c.TenDVct,
                                   donvi = c.DonVi,
                                   csbt = c.TSBT,
                                   b.KetQua
                               }).ToList();
                    int mabn = 0;
                    if (bn.Count() > 0)
                    {
                        mabn = bn.First().MaBNhan;
                    }
                    var phongk = (from kb in data.BNKBs.Where(p => p.MaBNhan == mabn && p.PhuongAn == 1)
                                  join kp in data.KPhongs.Where(p => p.PLoai == "Phòng khám") on kb.MaKP equals kp.MaKP
                                  select new { kp.TenKP }).ToList();
                    var _bn = (from a in bn
                               join b in cls on a.IdCLS equals b.IdCLS
                               select new
                               {
                                   a.MaBNhan,
                                   a.IdCLS,
                                   a.TenBNhan,
                                   a.Tuoi,
                                   a.GTinh,
                                   a.DChi,
                                   a.SThe,
                                   a.TenCB,
                                   a.TenKPdt,
                                   a.TenKPyc,
                                   a.MaKP,
                                   a.ChanDoan,
                                   a.MaICD,
                                   b.tenxetnghiem,
                                   b.donvi,
                                   b.csbt,
                                   b.KetQua

                               }).ToList();

                    BaoCao.rep_PhieuXNDongMau rep = new BaoCao.rep_PhieuXNDongMau();
                    frmIn frm = new frmIn();
                    rep.ngaylap2.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);
                    rep.ngaylap.Value = DateTime.Now;
                    if (phongk.Count() > 0 && phongk.First().TenKP != null)
                        rep.khoayc.Value = phongk.First().TenKP.ToString();
                    foreach (var item in _bn)
                    {
                        string[] arrThongTinBNKB = new string[5] { "", "", "", "", "" };
                        arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(data, item.MaBNhan, item.MaKP, true);
                        //rep.phong.Value = arrThongTinBNKB[2];
                        rep.giuong.Value = arrThongTinBNKB[2] + " - " + arrThongTinBNKB[3];
                        rep.mabn.Value = item.MaBNhan;
                        rep.tenbn.Value = item.TenBNhan;
                        rep.tuoi.Value = item.Tuoi;
                        rep.gioitinh.Value = item.GTinh;
                        rep.diachi.Value = item.DChi;
                        rep.sothe.Value = item.SThe;
                        rep.chandoan.Value = "Chẩn đoán: " + item.MaICD + " - " + item.ChanDoan;
                        rep.cbth.Value = item.TenCB;
                        rep.khoadt.Value = item.TenKPdt;

                    }
                    if (bn.Count > 0 && bn.First().TenKPyc != null)
                        rep.khoacd.Value = bn.First().TenKPyc.ToString();
                    rep.DataSource = _bn;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    phieuXNDongMau(data, idcls, TTN, _Mabn);
                }


            }

        }
        public static bool phieuXNDongMau(QLBV_Database.QLBVEntities data, int idcls, string tenTNRG, int _Mabn)
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
                    int? idkp = khoa.MaKP;
                    if (khoa != null && khoa.TenKP != null)
                        rep.celKhoa.Text = "Khoa: " + khoa.TenKP;

                    //thông tin chung
                    rep.celHoTen.Text = bn.TenBNhan;
                    rep.celCQCQ.Text = rep.cqcqSL.Text = DungChung.Bien.TenCQCQ.ToUpper();
                    rep.celBenhpham.Text = "Bệnh phẩm: " + bn.BenhPham;
                    rep.celNgayLay.Text = "Ngày lấy mẫu: " + bn.ThoiGianLayMau.ToString();
                    if (bn.ThoiGianLayMau != null)
                    {
                        rep.celNgayLay.Text = +bn.NgayThang.Value.Day + " / " + bn.NgayThang.Value.Month + " / " + bn.NgayThang.Value.Year;

                    }
                    if (DungChung.Bien.MaBV == "14017")
                    {
                        string[] _MaICDarr = DungChung.Ham.getMaICDarrFull_SL_kp(data, _Mabn, DungChung.Bien.GetICD, idkp);
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
                            rep.celBuong.Text = "Số phòng: " + qbnkb.Buong;
                            rep.celGiuong.Text = "Số giường: " + qbnkb.Giuong;
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
                                 select new { cd.MaDV, dvct.TenDVct, dvct.STT, dvct.STT_F, clsct.KetQua, dv.TenDV, dvct.MaDVct, dvct.TSBT, dvct.DonVi, tn.TenTN }).ToList();

                    if (DungChung.Bien.MaBV == "14017")
                    {
                        kqcls = kqcls.OrderBy(o => o.STT_F).ThenBy(o => o.TenDVct).ToList();
                    }

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

        private void txttimten_EditValueChanged(object sender, EventArgs e)
        {

            int _int_maBN = 0;
            string timten = txttimten.Text.ToLower();
            if (!string.IsNullOrEmpty(txttimten.Text))
            {
                int rs;
                if (Int32.TryParse(timten, out rs))
                    _int_maBN = Convert.ToInt32(timten);
            }
            // tbBenhNhan.DefaultView.RowFilter = string.Format("MaBNhan = {0} or TenBNhan LIKE '%{1}%'", _int_maBN, txttimten.Text);
            // tbBenhNhan.DefaultView.RowFilter = string.Format("TenBNhan LIKE '%{0}%'", txttimten.Text);

            tbBenhNhan.DefaultView.RowFilter = "MaBNhan = " + _int_maBN + " or TenBNhan LIKE '%" + timten + "%'";
            grvBenhnhan_FocusedRowChanged_1(null, null);
        }

        private void txtTimBarcode_Leave(object sender, EventArgs e)
        {
            if (!load)
                TimKiem();
            if (grvBenhnhan.RowCount > 0)
            {
                int sodk = 0;
                if (grvBenhnhan.GetFocusedRowCellValue(colSoDK) != null && grvBenhnhan.GetFocusedRowCellValue(colSoDK).ToString() != "")
                    sodk = Convert.ToInt32(grvBenhnhan.GetFocusedRowCellValue(colSoDK));
                FormNhap.getSoKB.ComPort(sodk);

            }
        }

        private void btnInTem_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "27023")
            {
                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int _int_maBN = 0;
                Int32.TryParse(txtMaBN.Text, out _int_maBN);

                var clsHT = _Data.CLS.Where(o => o.MaBNhan == _int_maBN && o.Status != 1 && o.BarCode != "" && o.BarCode != null).OrderByDescending(o => o.Barcode_Time);
                if (clsHT != null && clsHT.Count() > 0)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Barcode", clsHT.First().BarCode);
                    dic.Add("SoBarcode", clsHT.Count());

                    DungChung.Ham.Print(DungChung.PrintConfig.In_Barcode_27023, null, dic, false);
                }
            }
            else
            {
                int _mabn = 0;
                if (!String.IsNullOrEmpty(txtMaBN.Text))
                    _mabn = Convert.ToInt32(txtMaBN.Text);
                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    MessageBox.Show("Bệnh nhân chưa có mã barcode");
                    return;
                }
                var updateSID = _Data.CLS.Where(p => p.MaBNhan == _mabn && p.Code == txtCode.Text.Trim()).ToList();
                foreach (var item in updateSID)
                {
                    item.BarCode = item.Code;

                }
                _Data.SaveChanges();
                bool ok = true;
                foreach (var a in updateSID)
                {
                    if (!QLBV.CLS.Connect_LIS.XML_ChiDinhCLS(_Data, DungChung.Bien.xmlFilePath_LIS[0], a.IdCLS))
                    {
                        MessageBox.Show("Lỗi khớp BarCode");
                        ok = false;
                    }

                }
                if (ok)
                {
                    frmIn frm = new frmIn();
                    BaoCao.rep_tembarcode rep = new BaoCao.rep_tembarcode();
                    //var bn = _lDsBenhNhan.Where(p => p.MaBNhan == _mabn).FirstOrDefault();

                    //int makp = 0;
                    //if (bn != null)
                    //{
                    //    rep.TenBNhan.Value = bn.TenBNhan + "-" + bn.Tuoi + "t";
                    //    makp = bn.MaKP;
                    //}
                    #region ADO
                    DataRow[] foundRows;
                    int makp = 0;
                    foundRows = tbBenhNhan.Select("MaBNhan = '" + _mabn + "'");
                    if (foundRows.Length > 0)
                    {
                        rep.TenBNhan.Value = foundRows[0]["TenBNhan"] + "-" + foundRows[0]["Tuoi"] + "t";
                        makp = Convert.ToInt32(foundRows[0]["MaKP"]);
                    }
                    #endregion
                    var khoa = _Data.KPhongs.Where(p => p.MaKP == makp).FirstOrDefault();
                    string tenkp = "";
                    string[] ten = khoa.TenKP.Split(' ');
                    tenkp = ten[0] + " " + ten[ten.Length - 1];
                    rep.TenKP.Value = tenkp;
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (time > 0)
            {
                Auto_result_CLS(DateTime.Now);
                string[] ds = new string[1000];
                ds = _lds.Where(p => p.NgayTH >= DateTime.Now.AddMinutes(-30)).OrderBy(p => p.NgayTH).Select(p => p.TenBN).ToArray();
                if (ds.Length > 1)
                    memoEdit1.Text = "DS BN đã có KQ: " + string.Join("; ", ds);
            }
        }

        private void btnNhapHCVTYT_Click(object sender, EventArgs e)
        {
            //frm.ShowDialog();
            int _int_maBN = 0;
            Int32.TryParse(txtMaBN.Text, out _int_maBN);
            int idcd = 0;
            if (grvketqua.GetFocusedRowCellValue(colIDCDclsct) != null && grvketqua.GetFocusedRowCellValue(colIDCDclsct).ToString() != "")
                idcd = Convert.ToInt32(grvketqua.GetFocusedRowCellValue(colIDCDclsct));
            int makp = 0;
            if (LupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(LupKhoaphong.EditValue);
            QLBV.FormNhap.frm_kedon frm = new QLBV.FormNhap.frm_kedon(_int_maBN, idcd, makp, false);
            frm.ShowDialog();
        }

        private void txtBarcode_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //btnKhopMa_Click(null, null);
        }

        private void frm_kqcls_Enter(object sender, EventArgs e)
        {

        }

        private void grvBenhnhan_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //if (DungChung.Bien.MaBV != "26007" && DungChung.Bien.MaBV != "01830")
            //{
            //    DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            //    DateTime denngay = DungChung.Ham.NgayTu(lupNgayden.DateTime);
            //    if (grvBenhnhan.GetRowCellValue(e.RowHandle, colMabn) != null && grvBenhnhan.GetRowCellValue(e.RowHandle, colMabn).ToString() != "")
            //    {
            //        _lresult_LIS = new List<Library_CLS.Lis_His.clsChiDinhCLS>();
            //        List<int> _idcls = new List<int>();
            //        List<string> _barcode = new List<string>();
            //        _lresult_LIS.Clear();
            //        int _mabn = 0;
            //        _mabn = Convert.ToInt32(grvBenhnhan.GetRowCellValue(e.RowHandle, colMabn).ToString());

            //        _barcode = _Data.CLS.Where(p => p.NgayTH >= tungay && p.NgayTH <= denngay && p.MaBNhan == _mabn).Select(p => p.BarCode).Distinct().ToList();
            //        foreach (var item in _barcode)
            //        {
            //            _idcls = _Data.CLS.Where(p => p.BarCode == item && p.MaBNhan == _mabn).Select(p => p.IdCLS).ToList();
            //            try
            //            {
            //                _lresult_LIS = Library_CLS.Lis_His.getDS_CLSct(item, _idcls, DungChung.Bien.xmlFilePath_LIS[1]);
            //            }
            //            catch (Exception ex)
            //            {
            //                DungChung.WriteLog.Error(ex);
            //                DungChung.WriteLog.Error("Lỗi lấy KQ: Barcode: {0}- IDCLS: {1}");
            //            }

            //        }

            //    }
            //    if (_lresult_LIS.Count() > 0)
            //        e.Appearance.ForeColor = Color.Red;
            //}

            //var row = grvBenhnhan.GetDataRow(e.RowHandle);
            //if (row != null)
            //{
            //    var isKQ = row["isKQ"];
            //    if (isKQ != null && isKQ != DBNull.Value)
            //    {
            //        if (isKQ.ToString() == "1")
            //            e.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }

        private void lupTieuNhom_EditValueChanged(object sender, EventArgs e)
        {
            if (!load)
                TimKiem();
        }

        private void GrcNhomDV_Click(object sender, EventArgs e)
        {

        }

        private void btnNhapHCVTBN_Click(object sender, EventArgs e)
        {
            int _int_maBN = 0;
            Int32.TryParse(txtMaBN.Text, out _int_maBN);
            int idcd = 0;
            if (grvketqua.GetFocusedRowCellValue(colIDCDclsct) != null && grvketqua.GetFocusedRowCellValue(colIDCDclsct).ToString() != "")
                idcd = Convert.ToInt32(grvketqua.GetFocusedRowCellValue(colIDCDclsct));
            int makp = 0;
            if (LupKhoaphong.EditValue != null)
                makp = Convert.ToInt32(LupKhoaphong.EditValue);
            QLBV.FormNhap.frm_kedon frm = new QLBV.FormNhap.frm_kedon(_int_maBN, idcd, makp, true, "BẢNG THỐNG KÊ THUỐC VÀ VẬT TƯ TIÊU HAO BỆNH NHÂN KHOA XÉT NGHIỆM");
            frm.ShowDialog();
        }

        private void cboPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboPhieu.SelectedIndex == 1)
                simpleButton2_Click(null, null);
            else if (cboPhieu.SelectedIndex == 2)
            {
                if (GrvNhomDV.GetFocusedRowCellValue("IDCLS") != null && GrvNhomDV.GetFocusedRowCellValue("IDCLS").ToString() != "" && GrvNhomDV.GetFocusedRowCellValue("TenRG") != null && !string.IsNullOrEmpty(txtMaBN.Text))
                {
                    int idcls = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDCLS"));
                    string TTN = GrvNhomDV.GetFocusedRowCellValue("TenRG").ToString();
                    if (TTN == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc)
                    {
                        InPhieuXNGiaiPhauSinhThiet(idcls, _Mabn);
                    }
                    else
                        MessageBox.Show("Dịch vụ phải thuộc tiểu nhóm Xét nghiệm mô bệnh học");
                }


            }
            else if (cboPhieu.SelectedIndex == 3)
            {
                if (DungChung.Bien.MaBV == "30372")
                {
                    int idcls = Convert.ToInt32(GrvNhomDV.GetFocusedRowCellValue("IDCLS"));
                    string TTN = GrvNhomDV.GetFocusedRowCellValue("TenRG").ToString();
                    QLBV.CLS.InPhieu._InPhieu_XetNghiem(_Data, TTN, String.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), idcls, 0);
                }
                else
                {
                    int _int_maBN = 0;
                    Int32.TryParse(txtMaBN.Text, out _int_maBN);
                    QLBV.FormThamSo.frm_InXnTongHop frm = new QLBV.FormThamSo.frm_InXnTongHop(_int_maBN);
                    frm.ShowDialog();
                }
            }
            else if (cboPhieu.SelectedIndex == 4)
            {
                try
                {
                    QLBV.CLS.InPhieu.PrintNow = true;
                    int _int_maBN = 0;
                    Int32.TryParse(txtMaBN.Text, out _int_maBN);
                    if (_int_maBN <= 0)
                        return;
                    var query = (from cls in _Data.CLS.Where(o => o.MaBNhan == _int_maBN && o.Status == 1)
                                 join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join dv in _Data.DichVus.Where(o => o.IDNhom == 1) on cd.MaDV equals dv.MaDV
                                 join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 group cls by new { cls.IdCLS, tn.TenRG, cls.NgayThang, cls.MaKPth, cls.BarCode } into kq
                                 select new { kq.Key.IdCLS, kq.Key.TenRG }).ToList();
                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            QLBV.CLS.InPhieu._InPhieu_XetNghiem(_Data, item.TenRG, String.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), item.IdCLS, 0);
                        }
                    }
                    else
                        MessageBox.Show("Không có chỉ định nào!");
                }
                finally
                {
                    QLBV.CLS.InPhieu.PrintNow = false;
                }

            }
            else if (cboPhieu.SelectedIndex == 5) //30372 phiếu xn covid19
            {
                try
                {
                    int _int_maBN = 0;
                    Int32.TryParse(txtMaBN.Text, out _int_maBN);
                    if (_int_maBN <= 0)
                        return;
                    var query = (from cls in _Data.CLS.Where(o => o.MaBNhan == _int_maBN && o.Status == 1)
                                 join cd in _Data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                                 join dv in _Data.DichVus.Where(o => o.TenDV == "Xét nghiệm nhanh kháng nguyên virus SARS-CoV-2") on cd.MaDV equals dv.MaDV
                                 join tn in _Data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 group cls by new { cls.IdCLS, tn.TenRG, cls.NgayThang, cls.MaKPth, cls.BarCode } into kq
                                 select new { kq.Key.IdCLS, kq.Key.TenRG }).ToList();
                    if (query.Count > 0)
                    {
                        ChucNang.frm_PhieuXNCovid frm = new ChucNang.frm_PhieuXNCovid(String.IsNullOrEmpty(txtMaBN.Text) ? 0 : Convert.ToInt32(txtMaBN.Text), query.First().TenRG, query.First().IdCLS);
                        frm.ShowDialog();
                    }
                    else
                        MessageBox.Show("Không có chỉ định SARS-COV-2!");
                }
                finally
                {
                    QLBV.CLS.InPhieu.PrintNow = false;
                }

            }
            cboPhieu.SelectedIndex = 0;
        }

        public static void InPhieuXNGiaiPhauSinhThiet(int idcls, int _Mabn)
        {

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            QLBV.CLS.Phieu.rep_PhieuXNGiaiPhauSinhThiet rep = new QLBV.CLS.Phieu.rep_PhieuXNGiaiPhauSinhThiet();
            frmIn frm = new frmIn();
            var parcd1 = (from cls in data.CLS.Where(p => p.IdCLS == idcls)
                          join bn in data.BenhNhans
                          on cls.MaBNhan equals bn.MaBNhan
                          join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                          select new { cls.NgayTH, cls.NgayKQ, cls.CapCuu, cls.BarCode, bn.TenBNhan, bn.NamSinh, cd.Mau_Lan_MTruongXN, bn.MaBNhan, bn.DChi, bn.GTinh, cls.MaKP, cls.MaCBth, bn.DTuong, bn.Tuoi, bn.SThe, cls.MaCB, cls.BenhPham, cd.LoiDan, cd.KetLuan, cd.MoTa, cd.MaDV, cls.ThoiGianNhanMau, cls.ThoiGianLayMau, cd.GhiChu, cls.TrangThaiBP, cd.IDCD }).FirstOrDefault();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                rep.txtBarcode.Text = parcd1 != null ? parcd1.BarCode : "";
                rep.lblBarcode.Visible = true;
                rep.txtBarcode.Visible = true;
            }
            rep.celTenCQCQ.Text = DungChung.Bien.TenCQCQ == "" ? "Sở y tế" : DungChung.Bien.TenCQCQ;
            rep.colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (parcd1 != null)
            {
                var ttxn = data.ThongTinXetNghiems.Where(p => p.IDCD == parcd1.IDCD).FirstOrDefault();
                rep.celTenNguoiBenh.Text = parcd1.TenBNhan.ToUpper();
                rep.celNamSinh.Text = parcd1.NamSinh;
                rep.celTuoi.Text = DungChung.Ham.TuoitheoThang(data, _Mabn, DungChung.Bien.formatAge);
                List<CanBo> lcanbo = data.CanBoes.ToList();
                if (parcd1.GTinh == 1)
                {
                    rep.lblNam.Visible = false;
                    rep.lblNu.Visible = true;
                }
                else if (parcd1.GTinh == 0)
                {
                    rep.lblNam.Visible = true;
                    rep.lblNu.Visible = false;
                }
                rep.celDiaChi.Text = parcd1.DChi;
                rep.celSThe.Text = parcd1.SThe;

                bool capcuu = parcd1.CapCuu;
                if (capcuu)
                {
                    rep.celCapCuu.Text = "X";
                }
                else rep.celThuong.Text = "X";
                rep.celBenhPham.Text = parcd1.BenhPham;

                if (parcd1.ThoiGianLayMau != null)
                    rep.celNgayGio.Text = "lúc: " + parcd1.ThoiGianLayMau.Value.Hour.ToString("D2") + " giờ " + parcd1.ThoiGianLayMau.Value.Minute.ToString("D2") + "   : ngày " + parcd1.ThoiGianLayMau.Value.Day.ToString("D2") + "/" + parcd1.ThoiGianLayMau.Value.Month.ToString("D2") + "/" + parcd1.ThoiGianLayMau.Value.Year.ToString();
                if (ttxn != null)
                {
                    rep.celDungdich.Text = ttxn.DDCoDinh;
                    rep.celDauHieuLamSang.Text = ttxn.DauHieuLSVaXNKhac;
                    rep.celKQSinhThietLanTruoc.Text = ttxn.KQXNLanTruoc;
                    rep.celNguoiPhaBP.Text = getTenCB(ttxn.CBPhaBenhPham ?? "", lcanbo);
                    if (ttxn.ThoiGianLamTieuBan != null)
                        rep.celNgayPha.Text = ttxn.ThoiGianLamTieuBan.Value.Day.ToString("D2") + "/" + ttxn.ThoiGianLamTieuBan.Value.Month.ToString("D2") + "/" + ttxn.ThoiGianLamTieuBan.Value.Year.ToString();
                    rep.celPhuongphapnhuom.Text = ttxn.PhuongPhap;
                    rep.celNguoiLamTieuBan.Text = getTenCB(ttxn.CBLamTieuBan ?? "", lcanbo);
                    rep.celNhanxetdaithe.Text = ttxn.NXDaiThe;
                    rep.xrTableCell58.Text = ttxn.NXDaiThe;
                    rep.celNhanXetViThe.Text = ttxn.NXViThe;
                    rep.celChanDoanGiaiPhau.Text = ttxn.ChanDoanGiaiPhau;
                    rep.xrTableCell62.Text = ttxn.ChanDoanGiaiPhau;
                    rep.xrTableCell40.Text = DungChung.Bien.MaBV == "27023" ? " Họ tên: " + getTenCB(parcd1.MaCBth ?? "", lcanbo) : "Họ tên:...........................................";
                    rep.celSuPhuHop.Text = ttxn.PhuHopChanDoan;
                    if (parcd1.NgayKQ != null)
                        rep.celNgayTraKQ.Text = "Trả ngày " + parcd1.NgayKQ.Value.Day.ToString("D2") + " tháng " + parcd1.NgayKQ.Value.Month.ToString("D2") + " năm " + parcd1.NgayKQ.Value.Year.ToString();
                }

                rep.celQtrDtri.Text = parcd1.KetLuan;
                rep.celTrangThaiBP.Text = parcd1.TrangThaiBP;
                if (parcd1.ThoiGianNhanMau != null)
                    rep.celNgayTH.Text = "Gửi ngày " + parcd1.ThoiGianNhanMau.Value.Day.ToString("D2") + " tháng " + parcd1.ThoiGianNhanMau.Value.Month.ToString("D2") + " năm " + parcd1.ThoiGianNhanMau.Value.Year.ToString();
                var qdv = data.DichVus.Where(p => p.MaDV == parcd1.MaDV).FirstOrDefault();
                if (qdv != null)
                    rep.celDichVu.Text = qdv.TenDV;
                var vaovien = data.VaoViens.Where(p => p.MaBNhan == parcd1.MaBNhan).FirstOrDefault();
                if (vaovien != null)
                    rep.lblSoVV.Text = "Số vào viện: " + vaovien.SoVV ?? "";
                rep.celSoManh.Text = parcd1.Mau_Lan_MTruongXN;//
                if (parcd1.NgayTH != null)
                    rep.celNgaylamXong.Text = parcd1.NgayTH.Value.Day.ToString("D2") + "/" + parcd1.NgayTH.Value.Month.ToString("D2") + "/" + parcd1.NgayTH.Value.Year.ToString();

                rep.celBSDtri.Text = " Họ tên: " + getTenCB(parcd1.MaCB ?? "", lcanbo);
                rep.celBSDocKQ.Text = " Họ tên: " + getTenCB(parcd1.MaCBth ?? "", lcanbo);
            }
            string[] arrThongTinBNKB = DungChung.Ham.laythongtinBNKB(data, _Mabn, parcd1.MaKP == null ? 0 : parcd1.MaKP.Value, true);
            //  rep.ChanDoan.Value = arrThongTinBNKB[1];
            rep.celBuong.Text = arrThongTinBNKB[2];
            rep.celGiuong.Text = arrThongTinBNKB[3];
            rep.celKhoa.Text = arrThongTinBNKB[4];
            rep.celChanDoanLS.Text = arrThongTinBNKB[1];

            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private static string getTenCB(string maCB, List<CanBo> lcanbo)
        {
            var qcb = lcanbo.Where(p => p.MaCB == maCB).FirstOrDefault();
            if (qcb != null)
                return qcb.TenCB;
            else
                return "";
        }

        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            btnKhopMa.Focus();
            if (DungChung.Bien.MaBV == "27023")
                txtBarcode.Text = txtBarcode.Text.PadLeft(barCode_Number, '0');
        }

        private void grvBenhnhan_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "01071")
                txtBarcode.Focus();
            grvBenhnhan_FocusedRowChanged_1(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(e.RowHandle, e.RowHandle));
        }

        private void btnCreateBarcode_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "27023")
            {
                int _int_maBN = 0;
                Int32.TryParse(txtMaBN.Text, out _int_maBN);
                _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int numberBarcode = 0;
                //Lấy giới hạn số barcode
                var hospital = _Data.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                if (hospital != null)
                {
                    numberBarcode = hospital.Barcode_Number ?? 0;
                }

                int sht = 0;
                var maxBarCode = _Data.CLS.Where(o => o.Barcode_Time != null && o.Barcode_Time.Value.Year == DateTime.Now.Year && o.Barcode_Time.Value.Month == DateTime.Now.Month && o.Barcode_Time.Value.Day == DateTime.Now.Day).Max(o => o.BarCode);
                sht = maxBarCode != null ? (Convert.ToInt32(maxBarCode) + 1) : 1;
                string barCodeHT = sht.ToString().PadLeft(numberBarcode, '0');

                var dataSource = (List<ListCLS>)GrcNhomDV.DataSource;
                if (dataSource != null && dataSource.Count > 0 && dataSource.Exists(o => o.Chon && o.Status != 1))
                {
                    var chon = dataSource.Where(o => o.Chon && o.Status != 1).Select(o => o.IDCLS);
                    var clsCTH = _Data.CLS.Where(o => chon.Contains(o.IdCLS)).ToList();
                    if (clsCTH != null && clsCTH.Count() > 0)
                    {
                        foreach (var item in clsCTH)
                        {
                            var clsUpdate = _Data.CLS.FirstOrDefault(o => o.IdCLS == item.IdCLS);
                            clsUpdate.BarCode = barCodeHT;
                            clsUpdate.Barcode_Time = DateTime.Now;
                        }
                        _Data.SaveChanges();
                        MessageBox.Show("Tạo barcode thành công");
                        btnInTem_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu tạo barcode");
                    }
                    grvBenhnhan_FocusedRowChanged_1(null, null);
                }
            }
        }

        bool IsCheckAll = false;
        private void grvketqua_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                GridView view = sender as GridView;
                GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
                GridHitInfo hi = view.CalcHitInfo(e.Location);

                if (hi.HitTest == GridHitTest.Column)
                {
                    if (hi.Column.FieldName == "Is_Execute")
                    {
                        if (IsCheckAll)
                        {
                            hi.Column.Image = imageList1.Images[1];
                            var dataSource = (DataTable)grcKetqua.DataSource;
                            if (dataSource != null)
                            {
                                foreach (DataRow row in dataSource.Rows)
                                {
                                    row["Is_Execute"] = false;
                                }
                                grcKetqua.DataSource = dataSource;
                                IsCheckAll = false;
                            }
                        }
                        else
                        {
                            hi.Column.Image = imageList1.Images[0];
                            var dataSource = (DataTable)grcKetqua.DataSource;
                            if (dataSource != null)
                            {
                                foreach (DataRow row in dataSource.Rows)
                                {
                                    row["Is_Execute"] = true;
                                }
                                grcKetqua.DataSource = dataSource;
                                IsCheckAll = true;
                            }
                        }
                    }
                }
            }
        }

        private void btnSaveIndex_Click(object sender, EventArgs e)
        {
            var dataSource = (DataTable)grcKetqua.DataSource;
            if (dataSource != null && dataSource.Rows.Count > 0)
            {
                for (int i = 0; i < dataSource.Rows.Count; i++)
                {
                    int id = Convert.ToInt32(dataSource.Rows[i]["id"]);
                    CLSct sua = _Data.CLScts.FirstOrDefault(p => p.Id == id);
                    sua.Is_Execute = Convert.ToBoolean(dataSource.Rows[i]["Is_Execute"] != DBNull.Value ? dataSource.Rows[i]["Is_Execute"] : "false");

                    _Data.SaveChanges();
                }
                listCheckClsct = new List<object>();
                MessageBox.Show("Lưu chỉ số thành công");
            }
        }

        private void grcKetqua_Leave(object sender, EventArgs e)
        {

        }

        private void panelControl5_Leave(object sender, EventArgs e)
        {
            if (listCheckClsct.Count > 0)
            {
                if (MessageBox.Show("Bạn chưa lưu chỉ số muốn in bạn có muốn lưu không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    btnSaveIndex_Click(null, null);
                }
                else
                {
                    listCheckClsct = new List<object>();
                }
            }
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            var focusedRowHandle = grvketqua.FocusedRowHandle;
            var row = grvketqua.GetDataRow(focusedRowHandle);
            if (row != null)
            {
                var idCLSct = row["id"];
                var dataSource = (DataTable)grcKetqua.DataSource;
                if (dataSource != null && dataSource.Rows.Count > 0)
                {
                    foreach (DataRow dtr in dataSource.Rows)
                    {
                        if (dtr["id"].ToString() == row["id"].ToString())
                            dtr["Is_Execute"] = !Convert.ToBoolean(row["Is_Execute"] != DBNull.Value ? row["Is_Execute"] : "false");
                    }
                    grcKetqua.DataSource = dataSource;
                }
                if (listCheckClsct.Exists(o => o == idCLSct))
                    listCheckClsct.Remove(idCLSct);
                else
                    listCheckClsct.Add(idCLSct);
            }
        }

        private void GrvNhomDV_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var row = (ListCLS)GrvNhomDV.GetRow(e.RowHandle);
            if (row != null)
            {
                if (e.Column.FieldName == "Chon")
                {
                    if (DungChung.Bien.MaBV == "27023")
                    {
                        e.RepositoryItem = row.Status == 1 ? chk_Chon_Disable : chk_Chon;
                    }
                }
            }
        }

        private void grcKetqua_Click(object sender, EventArgs e)
        {

        }

        private void GrvNhomDV_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GrvNhomDV_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(e.RowHandle, e.RowHandle));
        }

        private void grvketqua_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            grvketqua_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(e.RowHandle, e.RowHandle));
        }

        private void grvBenhnhan_Click(object sender, EventArgs e)
        {

        }

        private void lupMaMay_EditValueChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "30004")
            {
                var dataSource = (DataTable)grcKetqua.DataSource;
                if (dataSource != null && dataSource.Rows.Count > 0)
                {
                    foreach (DataRow row in dataSource.Rows)
                    {
                        row["MaQD"] = lupMaMay.EditValue;
                    }
                }
                grvketqua.RefreshData();
            }
        }

        private void grvBenhnhan_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void grvBenhnhan_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GrvNhomDV.FocusedRowHandle = focus;
            _lCLS = new List<ListCLS>();
            // _tbCLS = new DataTable();
            _tbCLSct = new DataTable();
            //  _kpct.Clear();
            if (grvBenhnhan.GetFocusedRowCellValue(colMabn) != null && grvBenhnhan.GetFocusedRowCellValue(colMabn).ToString() != "")
            {
                _Mabn = Convert.ToInt32(grvBenhnhan.GetFocusedRowCellValue(colMabn));
                txtMaBN.Text = _Mabn.ToString();
                int sott = 0;
                if (grvBenhnhan.GetFocusedRowCellValue(colSTT) != null && grvBenhnhan.GetFocusedRowCellValue(colSTT).ToString() != "")
                    sott = Convert.ToInt32(grvBenhnhan.GetFocusedRowCellValue(colSTT));
                if (DungChung.Bien.MaBV == "34019")
                {
                    var ttbx = _Data.TTboXungs.Where(p => p.MaBNhan == _Mabn).FirstOrDefault();
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
                //int ktMoBenhHoc27023 = -1; //kiem tra xn mô bệnh học ; nếu bn khác  = -1; nếu bv 27023 ( = 0 : nếu là xn khác; =1 nếu là xn mô bệnh học)
                //string _strchuyenkhoa = "";
                //if (DungChung.Bien.MaBV == "27023")
                //{
                //    int makp = 0;

                //    if (LupKhoaphong.EditValue != null)
                //        makp = Convert.ToInt32(LupKhoaphong.EditValue);
                //    if (makp > 0)
                //    {
                //        _strchuyenkhoa = _Data.KPhongs.Where(p => p.MaKP == makp).First().ChuyenKhoa;
                //        if (_strchuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc)
                //            ktMoBenhHoc27023 = 1;
                //        else
                //            ktMoBenhHoc27023 = 0;
                //    }
                //}
                #region linq
                //var cls1 = (from cls in _Data.CLS.Where(p => p.MaBNhan == _Mabn)
                //            join chidinh in _Data.ChiDinhs on cls.IdCLS equals chidinh.IdCLS
                //            select new { cls, chidinh }).ToList();

                //var dichvu1 = (from dichvu in _Data.DichVus
                //               join tn in _Data.TieuNhomDVs.Where(p =>  ktMoBenhHoc27023 ==1 ? p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc : ( ktMoBenhHoc27023 == 0 ? (p.TenRG.Contains("XN") && p.TenRG != DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc) : p.TenRG.Contains("XN")))
                //on dichvu.IdTieuNhom equals tn.IdTieuNhom
                //               select new { dichvu.MaDV, tn.TenRG, dichvu.TenDV }).ToList();

                //_lCLS = (from cls in cls1
                //         join dichvu in dichvu1 on cls.chidinh.MaDV equals dichvu.MaDV
                //         where (sott == 0 || cls.cls.STT == sott)
                //         group new { cls, dichvu } by new { dichvu.TenRG, cls.cls.IdCLS, cls.cls.NgayThang, cls.cls.Status, cls.cls.BarCode } into kq
                //         select new ListCLS
                //         {
                //             BarCode = kq.Key.BarCode,
                //             TenRG = kq.Key.TenRG,
                //             //  TenTN = kq.Key.TenTN,
                //             //TenDV=kq.Key.TenDV,
                //             IDCLS = kq.Key.IdCLS,
                //             Ngaythang = kq.Key.NgayThang == null ? System.DateTime.Now : kq.Key.NgayThang.Value,
                //             // IDTN = kq.Key.IdTieuNhom,
                //             Status = kq.Key.Status,
                //             Chon = kq.Key.BarCode == null ? true : (kq.Key.BarCode == "" ? true : false),
                //         }).ToList();
                //if (_lCLS.Count > 0)
                //{

                //    var a = (from rv in _Data.RaViens.Where(p => p.MaBNhan == _Mabn)
                //             select new { rv.MaBNhan }).ToList();
                //    if (a.Count > 0)
                //    {
                //        grvketqua.OptionsBehavior.ReadOnly = true;
                //    }
                //}
                #endregion

                string[] strpara = new string[] { "@MaBV", "@MaBN", "@SoTT", "@ktMoBenhHoc27023" };
                object[] oValue = new object[] { DungChung.Bien.MaBV, _Mabn, sott, -1, };//ktMoBenhHoc27023 };
                SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.VarChar, SqlDbType.Int, SqlDbType.Int, SqlDbType.Int };

                connect.Connect();
                DataTable _tbCLS = connect.FillDatatable("sp_kqXN_loadNhomDV", CommandType.StoredProcedure, strpara, oValue, sqlDBType);
                _lCLS.Clear();
                for (int i = 0; i < _tbCLS.Rows.Count; i++)
                {
                    ListCLS moi = new ListCLS();
                    moi.BarCode = _tbCLS.Rows[i]["BarCode"].ToString();
                    if (!string.IsNullOrEmpty(_tbCLS.Rows[i]["Ngaythang"].ToString()))
                        moi.Ngaythang = Convert.ToDateTime(_tbCLS.Rows[i]["Ngaythang"].ToString());
                    if (!string.IsNullOrEmpty(_tbCLS.Rows[i]["IDCLS"].ToString()))
                        moi.IDCLS = Convert.ToInt32(_tbCLS.Rows[i]["IDCLS"].ToString());
                    moi.Status = Convert.ToInt16(_tbCLS.Rows[i]["Status"].ToString());
                    moi.TenRG = _tbCLS.Rows[i]["TenRG"].ToString();
                    moi.MaKPth = Convert.ToInt32(_tbCLS.Rows[i]["MaKPth"].ToString());

                    if (_tbCLS.Rows[i]["Chon"].ToString() == "1")
                        moi.Chon = true;
                    else
                        moi.Chon = false;
                    _lCLS.Add(moi);
                }

                if (DungChung.Bien.MaBV == "34019" && _lCLS != null && _lCLS.Count > 0)
                {
                    if (LupKhoaphong.EditValue != null)
                    {
                        _lCLS = _lCLS.Where(o => o.MaKPth == Convert.ToInt32(LupKhoaphong.EditValue.ToString())).ToList();
                    }
                }


                if (_tbCLS.Rows.Count > 0)
                {

                    var a = (from rv in _Data.RaViens.Where(p => p.MaBNhan == _Mabn)
                             select new { rv.MaBNhan }).ToList();
                    if (a.Count > 0)
                    {
                        foreach (GridColumn col in grvketqua.Columns)
                        {
                            if (col.Name != colExecute.Name)
                                col.OptionsColumn.ReadOnly = true;
                        }
                    }
                }
            }
            else
            {
                txtMaBN.Text = "";
            }
            GrcNhomDV.DataSource = _lCLS;


            //if (DungChung.Bien.MaBV == "20001")
            //    GrcNhomDV.DataSource = _lCLS.OrderByDescending(p => p.Ngaythang).ToList();
            //else
            //    GrcNhomDV.DataSource = _lCLS;
        }

        private void txtCode_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}