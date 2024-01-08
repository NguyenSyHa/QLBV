using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.DungChung;
using System.Threading;
using System.Configuration;
using QLBV.FormThamSo;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using System.IO;
using QLBV.MoRong.GoiBenhNhan;
using System.Transactions;
using QLBV.ChucNang;
using QLBV.Models.Business.TT27;
using QLBV.Providers.Business.Datacommunication;
using QLBV.FormNhap;
using QLBV.Providers.StoredProcedure;
using QLBV.Models.Dictionaries.KPhongs;
using QLBV.Models.Dictionaries.Thuoc;
using QLBV.Providers.Business.Medicines;
using DevExpress.XtraEditors.Repository;
using QLBV.Controls.Views.Messages;
using static QLBV.DungChung.Ham;
using QLBV.Utilities.Commons;
using static QLBV.FormThamSo.frm_benhnhanxuatduoc;
using static QLBV.ChucNang.frm_DsMaBenhChon;

namespace QLBV.Forms.Medicines
{
    public partial class usNhapKhamBenh_New : DevExpress.XtraEditors.XtraUserControl
    {
        QLBVEntities _dataContext;
        private readonly DataCommunicationProvider _datacommnunicationProvider;
        private readonly ExcuteStoredProcedureProvider _excuteStoredProcedureProvider;
        private readonly MedicinesProvider _medicinesProvider;

        public usNhapKhamBenh_New()
        {
            InitializeComponent();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _datacommnunicationProvider = new DataCommunicationProvider();
            _excuteStoredProcedureProvider = new ExcuteStoredProcedureProvider();
            _medicinesProvider = new MedicinesProvider();
            List<string> strings = new List<string>();
            strings.Add("Đông");
            strings.Add("Tây");
            strings.Add("Bắc");
            strings.Add("Nam");
            DungChung.Bien.formInDon = "Khám bệnh";
        }

        #region biến dùng để update tồn ở bảng MedicineList
        int maKhoaKe = 0;
        int TH = -1;
        int maKhoXuat = 0;
        int idThuoc = 0;
        int maDV = 0;
        double donGia = 0;
        string soLo = "";
        DateTime hanDung = new DateTime();
        double slKe = 0;
        #endregion

        List<int> deleteDThuoccts = new List<int>();

        TenDuocKD tenduoc = new TenDuocKD();
        int MaKPs = DungChung.Bien.MaKP;
        string MaCBs = DungChung.Bien.MaCB;
        int _makp = DungChung.Bien.MaKP;
        public static List<DThuocct> _ldtct1 = new List<DThuocct>();
        public static int iddthuocmau1 = 0, laydon1 = 0;
        //  int _ppxd = DungChung.Bien.PPXuat;
        int MaKPxd = 0;
        int _soLuongTon = 0;
        int TTTab = 0;
        bool TTLuuKB = false;
        int TTLuu = 0;// 1 la tao moi, 2 la sua;
        //List<BNKB> query = new List<BNKB>();
        public int _luutudong = 0;
        //List<DThuocct> _lDthuocct = new List<DThuocct>();
        IList<DThuocctModel> _lDthuocct = new List<DThuocctModel>();
        List<CanBo> _lCanBo = new List<CanBo>();
        private ConnectData connect;
        DataTable tbBenhnhan = new DataTable();
        DataTable dtTbleThuoc = new DataTable();// danh sách thuốc khi kê đơn

        List<LinhThuoc> _lLinhThuoc = new List<LinhThuoc>();
        List<ChuyenKhoa> _lChuyenKhoa = new List<ChuyenKhoa>();
        List<KPhong> _lKPhong_data = new List<KPhong>();
        string[] _sHDSDThuoc = new string[1000];
        List<c_ICD> lICD = new List<c_ICD>();
        List<ICD10> _licd10 = new List<ICD10>();
        static string _maCQCQ = "";
        bool load = false;
        //  int _iddthuoc = 0;
        List<Ham.giaSoLoHSD> _listGiaSua = new List<Ham.giaSoLoHSD>();// DS thuốc xóa
        // DataTable tbKB = new DataTable();
        List<BNKB> _listBNKB = new List<BNKB>();
        public string macbkb = "";
        // double sothang = 1;
        string _lydodongy = "";
        List<DichVu> _lDichvu = new List<DichVu>();
        List<TonDuoc> _lTonDuocall = new List<TonDuoc>();
        static double tonthuoc = 0;
        static double soluongt = 0;// số lượng một loại thuốc được kê trên cùng 1 đơn thuốc
        string _solo = "";// số lo của thuốc được focus
        DateTime? _handung = null;// hạn dùng của thuốc được focus
        bool ktCellChange = true;
        bool process = false;
        List<TieuNhomDV> _ltieunhom = new List<TieuNhomDV>();
        int ppxuat = 0;
        /// <summary>
        /// Những thuốc có tồn >0
        /// </summary>
        List<DVu> _lConTon = new List<DVu>();
        /// <summary>
        /// Tất cả những thuốc xuất nhập trong kho đó
        /// </summary>
        List<DVu> _lDvTheoKho = new List<DVu>();
        List<dsthuoc> _lDvTheoKho1 = new List<dsthuoc>();
        bool _thanhToan = false;
        int _idkb = 0;
        int _kieudon = -3;
        bool _click_ChkKDN = false;
        bool _click_khoKe = false;
        // List<CanBo> _lcb = new List<CanBo>();
        double sothangOld = 1;
        double sothangSua = 1;
        int _maKhoaKB = 0;
        int _iddthuoc = 0;
        int _phuongan = 0;
        List<DichVu> _list27183 = new List<DichVu>(); // không cho thêm dịch vụ phẫu thuật thủ thuật trực tiếp (ko có nhóm 8)
        List<DichVu> _lDVhienthi = new List<DichVu>();// tất cả dịch vụ được hiển thị trên gridview chỉ định

        bool _changeBenhNhan = false;// khi di chuyển sang bệnh nhân khác
        bool isChonNhieuBenhKhac = false;

        IList<MedicineInventoryModel> MedicineByRooms = new List<MedicineInventoryModel>();

        #region setTTTab
        private void SetTTTab()
        {
            if (xtraNgoaiTru.SelectedTabPage == xtraKhamBenh)
                btnSyncMed.Visible = true;
            else
                btnSyncMed.Visible = false;

            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            //// lay dtbn
            var qdtbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Select(p => p.IDDTBN).FirstOrDefault();
            string iddtbn = qdtbn == null ? "-100" : (";" + qdtbn.ToString() + ";");
            #region ADO
            //string strSQL = "select IDDTBN from BenhNhan where MaBNhan = '" + _int_maBN + "'";

            //DataTable dtTble = connect.FillDatatable(strSQL, CommandType.Text);
            //string iddtbn = "-100";
            //if (dtTble.Rows.Count == 1)
            //{
            //    if (!string.IsNullOrEmpty(dtTble.Rows[0]["IDDTBN"].ToString()))
            //        iddtbn = dtTble.Rows[0]["IDDTBN"].ToString();
            //}
            #endregion
            //var duoc = (from tenduoc in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.Status == 1)
            //            join nhapduoc in _dataContext.NhapDcts on tenduoc.MaDV equals nhapduoc.MaDV
            //            join nduoc in _dataContext.NhapDs.Where(p => p.PLoai == 1) on nhapduoc.IDNhap equals nduoc.IDNhap
            //            group new { tenduoc, nhapduoc } by new { tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, tenduoc.TyLeSD } into kq
            //            select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, kq.Key.TyLeSD }
            //            ).OrderBy(p => p.TenDV).ToList();
            mnXoaDon.Enabled = false;
            mnSaoDon.Enabled = false;
            if (xtraNgoaiTru.SelectedTabPage == xtraKhamBenh)
            {
                TTTab = 1;
                mnInDon.Caption = "In đơn";
                //EnableButton(true);
                //EnableControlKB(false);
                xtraKhamBenh.Text = "K.Bệnh-K.Đơn";

                //TTTab = 2;
                //EnableButton(true);
                //EnableControlKD(false);
                mnXoaDon.Enabled = true;
                mnSaoDon.Enabled = true;
                xtraLS_KCB.Text = "Lịch sử KCB";

                if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                {
                    #region linq
                    //lấy danh sách dược
                    var _lDonThuoc = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1 && p.KieuDon == -1).ToList();

                    //lupKhoXuat.EditValue = 0;
                    if (_lDonThuoc.Count > 0)
                    {
                        txtIdDonThuoc.Text = _lDonThuoc.First().IDDon.ToString();
                        iddthuocmau1 = _lDonThuoc.First().IDDon_Mau ?? 0;
                        if (_lDonThuoc.First().MaKXuat != null)
                            lupKhoXuat.EditValue = _lDonThuoc.First().MaKXuat;
                        //else lupKhoXuat.EditValue = "";

                    }
                    #endregion
                    #region ADO
                    //string strSQLDT = "select * from DThuoc where MaBNhan = '" + _int_maBN + "' and PLDV = 1 and KieuDon = -1";

                    //DataTable tbdt = connect.FillDatatable(strSQLDT, CommandType.Text);
                    //lupKhoXuat.EditValue = 0;
                    //if (tbdt.Rows.Count > 0)
                    //{
                    //    txtIdDonThuoc.Text = tbdt.Rows[0]["IDDon"].ToString();
                    //    if (!string.IsNullOrEmpty(tbdt.Rows[0]["MaKXuat"].ToString()))
                    //        lupKhoXuat.EditValue = Convert.ToInt32(tbdt.Rows[0]["MaKXuat"].ToString());
                    //}
                    #endregion
                    else
                    {
                        _changeBenhNhan = true;
                        lupKhoXuat.EditValue = 0;
                        _changeBenhNhan = false;
                        txtIdDonThuoc.Text = "";
                        iddthuocmau1 = 0;
                    }
                    // lấy chi tiết đơn
                    if (!string.IsNullOrEmpty(txtIdDonThuoc.Text))
                    {
                        Int32 id = int.Parse(txtIdDonThuoc.Text.Trim());
                        #region linq
                        var dThuoccts = _medicinesProvider.ViewInfoMedicineDThuoc(id);
                        binSDonThuocct.DataSource = dThuoccts;
                        grcDonThuocct.DataSource = binSDonThuocct;// _dataContext.ToList(); //binSDonThuocct;
                        #endregion
                        #region ADO
                        //string strSQLDTct = "select * from DThuocct where IDDon = '" + id + "' order by IDDonct";

                        //DataTable tbdtct = connect.FillDatatable(strSQLDTct, CommandType.Text);
                        //// binSDonThuocct.DataSource = tbdtct;
                        //grcDonThuocct.DataSource = tbdtct;

                        #endregion
                        if (grvDonThuocct.RowCount > 0)
                            grvDonThuocct.OptionsBehavior.ReadOnly = false;
                        else
                            grvDonThuocct.OptionsBehavior.ReadOnly = true;
                        if (grvDonThuocct.OptionsBehavior.ReadOnly == true)
                        {
                            colIDThuoc.OptionsColumn.AllowEdit = false;
                        }
                        else
                        {
                            colIDThuoc.OptionsColumn.AllowEdit = true;
                        }

                        if (_lDonThuoc != null && _lDonThuoc.Count > 0 && !string.IsNullOrEmpty(txtIdDonThuoc.Text))
                        {
                            var idDon = int.Parse(txtIdDonThuoc.Text);
                            var donThuoc = _lDonThuoc.FirstOrDefault(f => f.IDDon == idDon);

                            if (donThuoc.DongBo != null)
                                btnSyncMed.Enabled = !donThuoc.DongBo.Value;
                            else
                                btnSyncMed.Enabled = true;
                        }
                        else
                        {
                            btnSyncMed.Enabled = false;
                        }

                        if (dThuoccts == null)
                            btnSyncMed.Enabled = false;
                        else if (dThuoccts.Count == 0)
                            btnSyncMed.Enabled = false;
                    }

                }

            }
            else
            {
                if (xtraNgoaiTru.SelectedTabPage == xtraLS_KCB)
                {
                    TTTab = 2;
                    //EnableButton(true);
                    //EnableControlKD(false);
                    mnInDon.Caption = "In L.sử";
                    xtraLS_KCB.Text = "Lịch sử KCB";

                    if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                    {


                    }
                }
                else
                {
                    if (xtraNgoaiTru.SelectedTabPage == xtraChiDinh)
                    {
                        TTTab = 3;
                        mnInDon.Caption = "In D.Vụ";
                        #region ADO
                        //string strSQLBNKB = "select * from BNKB where MaBNhan = '" + _int_maBN + "'";

                        //DataTable tbBNKB = connect.FillDatatable(strSQLBNKB, CommandType.Text);
                        var tbBNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                        #endregion
                        //EnableButton(true);
                        if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                        {
                            // var kt = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                            if (tbBNKB.Count > 0)
                            {
                                grvchiDinh.OptionsBehavior.ReadOnly = true;
                                colMaDVcd.OptionsColumn.AllowEdit = false;
                            }
                            else
                            {
                                grvchiDinh.OptionsBehavior.ReadOnly = false;
                                colMaDVcd.OptionsColumn.AllowEdit = true;
                            }
                        }
                        string _maKPsd = ";";
                        if (DungChung.Bien.PLoaiKP != "Admin")
                            _maKPsd = ";" + DungChung.Bien.MaKP + ";";
                        string maQDVCMau = "VM." + DungChung.Bien.MaBV; //theo công văn 5328
                        _lDVhienthi = _lDichvu.Where(p => p.PLoai == 2 || (p.PLoai == 1 && p.MaQD == maQDVCMau)).Where(p => DungChung.Bien.MaBV == "01071" ? p.Loai != 4 : true).Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_maKPsd)).Where(p => (p.DSDTBN != null && p.DSDTBN.Contains(iddtbn)) || (p.PLoai == 1 && p.MaQD == maQDVCMau)).OrderBy(p => p.TenDV).ToList();
                        _list27183 = _lDVhienthi.Where(p => p.IDNhom != 8).ToList();
                        lupMaDVcd.DataSource = _lDVhienthi;
                        int idcd = 0;
                        xtraChiDinh.Text = "Dịch vụ";
                        if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                        {
                            var q = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                            if (q.Count > 0)
                            {
                                txtChiDinh.Text = q.First().IDDon.ToString();
                                idcd = q.First().IDDon;
                            }
                            #region ADO
                            //string strSQLDT = "select * from DThuoc where MaBNhan = '" + _int_maBN + "' and PLDV = 2";

                            //DataTable tbdt = connect.FillDatatable(strSQLDT, CommandType.Text);

                            //if (tbdt.Rows.Count > 0)
                            //{
                            //    txtChiDinh.Text = tbdt.Rows[0]["IDDon"].ToString();
                            //    if (!string.IsNullOrEmpty(tbdt.Rows[0]["IDDon"].ToString()))
                            //        idcd = Convert.ToInt32(tbdt.Rows[0]["IDDon"].ToString());
                            //}
                            #endregion
                            #region linq
                            var data = (from dt in _dataContext.DThuoccts.Where(p => p.IDDon == idcd).Where(p => p.LoaiDV < 3)
                                        join dv in _dataContext.DichVus on dt.MaDV equals dv.MaDV
                                        select new DonThuocct
                                        {
                                            IDDon = dt.IDDon,
                                            IDDonct = dt.IDDonct,
                                            MaDV = dt.MaDV,
                                            TenDV = dv.TenDV,
                                            DonVi = dt.DonVi,
                                            DonGia = dt.DonGia,
                                            SoLuong = dt.SoLuong,
                                            ThanhTien = dt.ThanhTien,
                                            TienBN = dt.TienBN,
                                            TienBH = dt.TienBH,
                                            TrongBH = dt.TrongBH,
                                            NgayNhap = dt.NgayNhap,
                                            DuongD = dt.DuongD,
                                            SoPL = dt.SoPL,
                                            Status = dt.Status,
                                            IDCD = dt.IDCD,
                                            MaCB = dt.MaCB,
                                            MaKP = dt.MaKP,
                                            IDKB = dt.IDKB,
                                            Loai = dt.Loai,
                                            ThanhToan = dt.ThanhToan,
                                            MaKPtk = dt.MaKPtk,
                                            MaKXuat = dt.MaKXuat,
                                            TyLeTT = dt.TyLeTT,
                                            XHH = dt.XHH == 1 ? true : false,
                                            MaQD = dv.MaQD
                                        }).ToList();

                            var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                            if (hthong != null && hthong.IsTV == true && data.Exists(o => o.TenDV.ToLower().Contains("khám bác sỹ chuyên gia")))
                                chkKhamChuyenGia.Checked = true;
                            else
                                chkKhamChuyenGia.Checked = false;
                            binSChiDinhct.DataSource = data.ToList();
                            grcChiDinh.DataSource = binSChiDinhct;
                            #endregion
                            //string sql = "SELECT IDDonct,IDDon,MaDV,DonVi,DonGia,SoLo,SoLuong,ThanhTien,TienBN,TienBH,TienChenh,TrongBH,NgayNhap,DuongD,MoiLan,DviUong,SoLan,Luong,MaCC,SoPL,Status,IDCD,MaCB,DSCBTH,MaKP,IDKB,Loai,ThanhToan,Mien,GhiChu,MaKPtk,MaKXuat,TyLeTT,SoLuongct,AttachIDDonct,HanDung,LoaiDV ,XHH = CASE WHEN dbo.DThuocct.XHH = 1 THEN 1 ELSE 0 end FROM dbo.DThuocct WHERE IDDon = '" + idcd + "'";

                            //DataTable tb = connect.FillDatatable(sql, CommandType.Text);
                            //// binSChiDinhct.DataSource = tb;
                            //grcChiDinh.DataSource = tb;
                        }
                        //var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                        if (tbBNKB.Count > 0)
                        {
                            grvchiDinh.OptionsBehavior.ReadOnly = false;
                            colMaDVcd.OptionsColumn.AllowEdit = true;
                        }
                        else
                        {
                            grvchiDinh.OptionsBehavior.ReadOnly = true;
                            colMaDVcd.OptionsColumn.AllowEdit = false;
                        }
                    }
                    else if (xtraNgoaiTru.SelectedTabPage == xtabDichVuCS2)
                    {
                        TTTab = 4;
                        mnInDon.Caption = "In D.Vụ";
                        #region ADO
                        //string strSQLBNKB = "select * from BNKB where MaBNhan = '" + _int_maBN + "'";
                        xtabDichVuCS2.Text = "Dịch vụ CS2";
                        //DataTable tbBNKB = connect.FillDatatable(strSQLBNKB, CommandType.Text);
                        var tbBNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                        #endregion
                        //EnableButton(true);
                        if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                        {
                            // var kt = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                            if (tbBNKB.Count > 0)
                            {
                                grvDichVuCS2.OptionsBehavior.ReadOnly = true;
                                colDichVuCS2.OptionsColumn.AllowEdit = false;
                            }
                            else
                            {
                                grvDichVuCS2.OptionsBehavior.ReadOnly = false;
                                colDichVuCS2.OptionsColumn.AllowEdit = true;
                            }
                        }
                        string _maKPsd = ";";
                        if (DungChung.Bien.PLoaiKP != "Admin")
                            _maKPsd = ";" + DungChung.Bien.MaKP + ";";
                        string maQDVCMau = "VM." + DungChung.Bien.MaBV; //theo công văn 5328
                        _lDVhienthi = _lDichvu.Where(p => p.PLoai == 2 || (p.PLoai == 1 && p.MaQD == maQDVCMau)).Where(p => p.Status == 1).Where(p => p.Loai == 4).Where(p => p.MaKPsd.Contains(_maKPsd)).Where(p => (p.DSDTBN != null && p.DSDTBN.Contains(iddtbn)) || (p.PLoai == 1 && p.MaQD == maQDVCMau)).OrderBy(p => p.TenDV).ToList();
                        _list27183 = _lDVhienthi.Where(p => p.IDNhom != 8).ToList();
                        lupMaDVCS2.DataSource = _lDVhienthi;
                        int idcd = 0;
                        if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                        {
                            var q = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                            if (q.Count > 0)
                            {
                                txtChiDinh.Text = q.First().IDDon.ToString();
                                idcd = q.First().IDDon;
                            }
                            #region ADO
                            //string strSQLDT = "select * from DThuoc where MaBNhan = '" + _int_maBN + "' and PLDV = 2";

                            //DataTable tbdt = connect.FillDatatable(strSQLDT, CommandType.Text);

                            //if (tbdt.Rows.Count > 0)
                            //{
                            //    txtChiDinh.Text = tbdt.Rows[0]["IDDon"].ToString();
                            //    if (!string.IsNullOrEmpty(tbdt.Rows[0]["IDDon"].ToString()))
                            //        idcd = Convert.ToInt32(tbdt.Rows[0]["IDDon"].ToString());
                            //}
                            #endregion
                            #region linq
                            var data = (from dt in _dataContext.DThuoccts.Where(p => p.IDDon == idcd).Where(p => p.LoaiDV == 3)
                                        join dv in _dataContext.DichVus on dt.MaDV equals dv.MaDV
                                        select new DonThuocct
                                        {
                                            IDDon = dt.IDDon,
                                            IDDonct = dt.IDDonct,
                                            MaDV = dt.MaDV,
                                            TenDV = dv.TenDV,
                                            DonVi = dt.DonVi,
                                            DonGia = dt.DonGia,
                                            SoLuong = dt.SoLuong,
                                            ThanhTien = dt.ThanhTien,
                                            TienBN = dt.TienBN,
                                            TienBH = dt.TienBH,
                                            TrongBH = dt.TrongBH,
                                            NgayNhap = dt.NgayNhap,
                                            DuongD = dt.DuongD,
                                            SoPL = dt.SoPL,
                                            Status = dt.Status,
                                            IDCD = dt.IDCD,
                                            MaCB = dt.MaCB,
                                            MaKP = dt.MaKP,
                                            IDKB = dt.IDKB,
                                            Loai = dt.Loai,
                                            ThanhToan = dt.ThanhToan,
                                            MaKPtk = dt.MaKPtk,
                                            MaKXuat = dt.MaKXuat,
                                            TyLeTT = dt.TyLeTT,
                                            XHH = dt.XHH == 1 ? true : false,
                                            MaQD = dv.MaQD
                                        }).ToList();
                            var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                            if (hthong != null && hthong.IsTV == true && data.Exists(o => o.TenDV.ToLower().Contains("khám bác sỹ chuyên gia")))
                                chkKhamChuyenGia.Checked = true;
                            else
                                chkKhamChuyenGia.Checked = false;
                            binDichVuCS2.DataSource = data;
                            grcDichVuCS2.DataSource = binDichVuCS2;
                            #endregion
                            //string sql = "SELECT IDDonct,IDDon,MaDV,DonVi,DonGia,SoLo,SoLuong,ThanhTien,TienBN,TienBH,TienChenh,TrongBH,NgayNhap,DuongD,MoiLan,DviUong,SoLan,Luong,MaCC,SoPL,Status,IDCD,MaCB,DSCBTH,MaKP,IDKB,Loai,ThanhToan,Mien,GhiChu,MaKPtk,MaKXuat,TyLeTT,SoLuongct,AttachIDDonct,HanDung,LoaiDV ,XHH = CASE WHEN dbo.DThuocct.XHH = 1 THEN 1 ELSE 0 end FROM dbo.DThuocct WHERE IDDon = '" + idcd + "'";

                            //DataTable tb = connect.FillDatatable(sql, CommandType.Text);
                            //// binSChiDinhct.DataSource = tb;
                            //grcChiDinh.DataSource = tb;
                        }
                        //var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                        if (tbBNKB.Count > 0)
                        {
                            grvDichVuCS2.OptionsBehavior.ReadOnly = false;
                            colDichVuCS2.OptionsColumn.AllowEdit = true;
                        }
                        else
                        {
                            grvDichVuCS2.OptionsBehavior.ReadOnly = true;
                            colDichVuCS2.OptionsColumn.AllowEdit = false;
                        }
                    }
                }
            }

        }

        public class dsthuoc
        {
            public string TenDV { set; get; }
            public string MaTam { get; set; }
            public string TenThau2017 { set; get; }
            public string TenHC { set; get; }
            public string DonVi { set; get; }
            public string NguonGoc { set; get; }
            public double DonGia { set; get; }
            public int TyLeBQ { set; get; }
            public string TenRG { set; get; }
            public string TenThuocRG { set; get; }
            public int MaDV { set; get; }
            public string HamLuong { set; get; }
            public int MaKP { set; get; }
            public bool SLTon { set; get; }
            public string GhiChu { set; get; }
            public int TrongBH { get; set; }
            public string SoLo { set; get; }
            public DateTime? HanDung { get; set; }

        }
        #endregion
        private void EnableControlKB(bool T)
        {
            dtNgayKhamkb.Properties.ReadOnly = !T;
            lupKhoaKhamkb.Properties.ReadOnly = !T;
            lupChanDoanKb.Properties.ReadOnly = !T;
            lupNguoiKhamkb.Properties.ReadOnly = !T;
            lup_ChuyenKhoa.Properties.ReadOnly = !T;
            //txtLoiDan.Enabled = T;
            lupMaICDkb.Properties.ReadOnly = !T;
            LupICDKhac.Properties.ReadOnly = !T;
            LupICD2.Properties.ReadOnly = !T;
            LupICD3.Properties.ReadOnly = !T;
            LupICD4.Properties.ReadOnly = !T;
            mmChanDoanBD.Properties.ReadOnly = !T;
            chkKhamChuyenGia.Properties.ReadOnly = !T;
            txtBenhKhac2.Properties.ReadOnly = !T;
            txtBenhKhac3.Properties.ReadOnly = !T;
            txtBenhKhac4.Properties.ReadOnly = !T;
            txtBenhPhu2.Properties.ReadOnly = !T;
            txtBenhPhu3.Properties.ReadOnly = !T;
            radGiaiQuyet.Properties.ReadOnly = !T;
            lupKhoaDT.Properties.ReadOnly = !T;

            //  txtGhiChu.Properties.ReadOnly = !T;
        }
        private void ResetControlKB()
        {
            dtNgayKhamkb.DateTime = System.DateTime.Now;
            if (DungChung.Bien.MaBV == "24012")
            {
                dtNgayKhamkb.Text = DateTime.Now.ToString();
            }
            dt_NgayChuyen.Text = "";
            lupChanDoanKb.EditValue = "";
            lupKhoaKhamkb.EditValue = 0;
            LupICDKhac.EditValue = "";
            lupMaICDkb.EditValue = "";
            LupICD2.EditValue = "";
            LupICD3.EditValue = "";
            LupICD4.EditValue = "";
            lupKhac.EditValue = "";
            //cboMaICDBĐ.EditValue = "";
            txtBenhKhac2.EditValue = "";
            txtBenhKhac3.EditValue = "";
            txtBenhKhac4.EditValue = "";
            txtBenhKhac1.EditValue = "";
            txtBenhPhu2.Text = "";
            txtBenhPhu3.Text = "";
            txtBenhPhu.Text = "";
            txtBenhChinh.Text = "";
            mmChanDoanBD.Text = "";
            chkKhamChuyenGia.Checked = false;
            radGiaiQuyet.SelectedIndex = 4;
            lupKhoaDT.EditValue = 0;
            txtGhiChu.Text = "";
            txtIdkb.ResetText();
        }
        private void EnableControlKD(bool T)
        {
            //dtNgayKe.Enabled = T;
            //lupBPKe.Enabled = T;
            //lupNguoiKe.Enabled = T;
            lupKhoXuat.Enabled = T;
            //grcLichSuKCB.Enabled = T;
            grvDonThuocct.OptionsBehavior.Editable = T;

        }
        private void EnableButton(bool T)
        {

            mnXoa.Enabled = T;

            mnLuu.Enabled = !T;
            btnref.Enabled = T;
            grcBNhankb.Enabled = T;

        }
        DateTime _dttu = DungChung.Ham.NgayTu(System.DateTime.Now);
        DateTime _dtden = DungChung.Ham.NgayDen(System.DateTime.Now);

        private void tmr_Tick(object sender, EventArgs e)
        {

            //Label lbl7Mau = (Label)this.Controls["labThongBaoBNCP"];
            //Color TruocDo = lbl7Mau.ForeColor;
            //Color[] clr = new Color[]
            //                        {
            //                            Color.Red
            //                            ,Color.Yellow
            //                            ,Color.Orange
            //                            ,Color.Green
            //                            ,Color.Blue
            //                            ,Color.Indigo
            //                            ,Color.Purple
            //                        };
            //for (Int32 i = 0; i < clr.Length; i++)
            //    if (lbl7Mau.ForeColor == clr[i])
            //    {
            //        lbl7Mau.ForeColor = (i == clr.Length - 1 ? clr[0] : clr[i + 1]);
            //        break;
            //    }
            //if (lbl7Mau.ForeColor == TruocDo)
            //    lbl7Mau.ForeColor = clr[0];
        }
        private void doimau(string control, string chuoi)
        {
            //Label lbl = new Label();

            //lbl.Name = control;
            //lbl.Text = chuoi;
            //lbl.Dock = DockStyle.Fill;
            //this.Controls.Add(lbl);

            //System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            //tmr.Interval = 500;//nửa giây
            //tmr.Tick += tmr_Tick;
            //tmr.Start();

        }
        public void doimaulb1()
        {
            Color cl1 = Color.Red; Color cl2 = Color.Blue;
            // int i = 0;

            while (true)
            {
                labThongBaoBNCP.BackColor = cl1;
                Thread.Sleep(500);
                labThongBaoBNCP.BackColor = cl2;
                Thread.Sleep(500);
            }
        }
        public void doimaulb2()
        {

            Color cl1 = Color.Red; Color cl2 = Color.Blue;
            //  int i = 0;
            //while (true)
            //{

            labThongBaoBNCP.ForeColor = cl1;
            Thread.Sleep(500);
            labThongBaoBNCP.ForeColor = cl2;
            Thread.Sleep(500);

            //}
        }
        List<DungChung.Ham.TrongDMBHYT> _lTrongDMBH = new List<Ham.TrongDMBHYT>();
        public class DonThuocct
        {
            public int IDDonct { set; get; }
            public int? IDDon { set; get; }
            public int? MaDV { set; get; }
            public string TenDV { get; set; }
            public string DonVi { set; get; }
            public double DonGia { set; get; }
            public double SoLuong { set; get; }
            public double ThanhTien { set; get; }
            public double TienBN { set; get; }
            public double TienBH { set; get; }
            public int TrongBH { set; get; }
            public DateTime? NgayNhap { set; get; }
            public string DuongD { set; get; }
            public int SoPL { set; get; }
            public int? Status { set; get; }
            public int? IDCD { set; get; }
            public string MaCB { set; get; }
            public int? MaKP { set; get; }
            public int IDKB { set; get; }
            public int Loai { set; get; }
            public int ThanhToan { set; get; }
            public int MaKPtk { set; get; }
            public int? MaKXuat { set; get; }
            public double TyLeTT { set; get; }
            public bool XHH { set; get; }
            public string MaQD { get; set; }
        }
        private class LinhThuoc
        {
            private string ten;
            private int _value;
            public string Ten_Status
            {
                set { ten = value; }
                get { return ten; }
            }
            public int Status
            {
                set { _value = value; }
                get { return _value; }
            }
        }

        private void _setHDSDThuoc()
        {

            string HD = "6 giờ 10UI;6 giờ 7UI, 18 giờ 17UI;Chia 2 lần: 11 giờ, 18 giờ;Chia 2 lần: 6 giờ, 18 giờ;Chiều;Nhai trước ăn;Nhai trước ăn sáng;Sau ăn;Sau ăn 1 giờ" +
   ";Sau ăn no;Sau ăn sáng;Sau ăn sáng, trưa, Tối;Sau ăn tối;Sáng;Sáng 1/2 V, chiều 1/2 V;Sáng 1V, Chiều 1V;Sáng 1V, Chiều 1V, sau ăn;Sáng 1V, Chiều 1V, trước ăn;Sáng 1V, Chiều 2V" +
   ";Sáng 2V, Chiều 1V;Sáng 2V, Chiều 2V;Sáng 2V, Chiều 2V, sau ăn;Sáng 1V, Chiều 1V, trước ăn;Sáng 3V, Chiều 3V;Sáng 3V, Chiều 3V, sau ăn;Sáng 3V, Chiều 3V, sau ăn;Thủy châm;Trưa 1V, Chiều 2V, Sau ăn;Trưa 1V, Chiều 2V, Trước ăn;" +
   "Trưa 1V, Tối 1V, trước ăn;-	Trưa 1V, Tối 1V, sau ăn;Trưa 2V, Chiều 1V, trước ăn;Trước ăn sáng;Trước ăn sáng 1 giờ;Tối;Uống sau 1 giờ;Uống sau 2 giờ;Uống sau ăn;Uống trong bữa ăn;-	Uống trước bữa ăn;Uống trước bữa ăn 1 giờ;Nhai sau ăn";
            if (DungChung.Bien.MaBV == "30303")
            {
                HD = "Sáng-chiều-Tối (sau ăn);Sáng-Chiều-Tối (trước ăn);Sáng-Chiều (trước ăn);Sáng-Chiều (sau ăn);Sáng-Tối (trước ăn);Sáng-Tối (sau ăn);Chiều-Tối (sau ăn);Chiều-Tối (trước ăn);Sáng (trước ăn);Sáng (sau ăn);Trưa (trước ăn);Trưa (Sau ăn);Chiều (trước ăn);Chiều (sau ăn);Tối (trước ăn);Tối (sau ăn)";
            }
            _sHDSDThuoc = HD.Split(';');
        }
        private class c_ICD
        {
            private string maICD;

            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }
            private string tenICD;

            public string TenICD
            {
                get { return tenICD; }
                set { tenICD = value; }
            }

            public bool Check { get; set; }

        }

        //   bool _ktraBV_TyLeTT = false; // kiểm tra tính tỷ lệ thanh toán của dịch vụ (đối với bệnh viện tuyến D hàng 4 bv Tuyến xã Việt Yên); => thành tiền = số lượng * đơn giá (ko nhân với tỷ lệ BHTT), tính tiền của bệnh nhân mới thêm tỷ lệ thanh toán
        private void usKhamBenh_Load(object sender, EventArgs e)
        {
            try
            {
                load = true;
                xtraNgoaiTru.Enabled = false;
                barBtnDonThuocH_TT04.Enabled = false;
                barBtnDonThuocN_TT04.Enabled = false;
                barBtnThuocThuongTT04.Enabled = false;
                string chuoi = LupICD4.Text;
                if (!string.IsNullOrEmpty(chuoi))
                {
                    chuoi = Convert.ToString(chuoi[(chuoi.Length - 1)]);
                }
                if (!string.IsNullOrEmpty(chuoi) && chuoi != ";")
                {
                    LupICD4.Enabled = false;
                }
                if (DungChung.Bien.MaBV == "24012")
                {
                    btnTraThuoc.Visible = true;
                    colHanDung.Visible = true;
                }
                if (DungChung.Bien.MaBV == "27022")
                {
                    checkGhiChu.Visible = true;
                }
                else
                {
                    checkGhiChu.Visible = false;
                }
                if (DungChung.Bien.MaBV != "27001")
                {
                    barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                if (DungChung.Bien.MaBV == "34019")
                {
                    this.Size = new System.Drawing.Size(1355, 600);
                    ptPhoto.Visible = true;
                }
                if (DungChung.Bien.MaBV != "24009")
                {
                    txtBenhPhu.Visible = true;
                    txtBenhPhu2.Visible = false;
                    txtBenhPhu3.Visible = false;
                    txtBenhChinh.Visible = false;
                    bbiDeleteBN.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                if (DungChung.Bien.MaBV == "24297")
                {
                    txtBenhPhu2.Visible = true;
                    txtBenhPhu3.Visible = true;
                    txtBenhChinh.Visible = true;
                }
                if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
                {
                    panel1.Visible = false;
                    //panelControl10.Height -= panel1.Height;
                }
                else
                {
                    if (DungChung.Bien.MaBV == "12345")
                    {
                        colIDThuoc.Visible = false;
                        ColTenThuoc2.Visible = true;
                    }
                    var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                    if (hthong != null && hthong.IsTV == true)
                        chkKhamChuyenGia.Visible = true;
                    mmChanDoanBD.Width = mmChanDoanBD.Width - chkKhamChuyenGia.Width - 2;
                }

                bbiDeleteBN.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                connect = Program._connect;
                if (DungChung.Bien.MaBV == "30003")
                    cboTimRaVien.Properties.Items.Add("Ra viện chưa TT");
                if (DungChung.Bien.MaBV == "01071")
                    xtabDichVuCS2.PageVisible = true;
                if (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "24297")
                    btnYHCT.Visible = true;
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                {
                    radGiaiQuyet.Properties.Items.Add("Khám phối hợp");
                }
                if (DungChung.Bien.MaBV == "30009")
                    // lupMaDuocdt.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenThau2017", "Thầu 2017", 150, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default));
                    lupIDThuoc.Columns[1].Visible = true;
                else if (DungChung.Bien.MaBV == "12001")
                {
                    lupIDThuoc.Columns[3].Visible = false;
                    lupIDThuoc.Columns[5].Visible = false;
                    lupIDThuoc.Columns[1].Visible = false;
                    lupIDThuoc.Columns[7].Visible = false;
                    lupIDThuoc.Columns[8].Visible = false;
                    lupIDThuoc.Columns[9].Visible = false;
                    lupIDThuoc.Columns[10].Visible = false;
                    lupIDThuoc.Columns[11].Visible = false;
                    lupIDThuoc.Columns[12].Visible = true;
                }
                else
                    lupIDThuoc.Columns[1].Visible = false;

                try
                {
                    connect.Connect();

                    string sqlBV = "Select top 1  BenhVien.MaChuQuan from BenhVien where MaBV = '" + DungChung.Bien.MaBV + "'";
                    SqlDataReader _dr = connect.ExecuteReader(sqlBV, CommandType.Text);
                    if (_dr.Read())
                    {

                        _maCQCQ = _dr.GetString(0);
                        _dr.Close();
                        _dr.Dispose();
                    }
                    else
                    {
                        _dr.Close();
                        _dr.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009" || DungChung.Bien.MaBV == "30005")
                {
                    this.colDuongD.OptionsColumn.TabStop = true;
                    this.colDViUong.OptionsColumn.TabStop = true;
                }
                else
                {
                    colDViUong.OptionsColumn.TabStop = false;
                    this.colDuongD.OptionsColumn.TabStop = false;
                }
                if (ppxuat == 3 || DungChung.Bien.MaBV == "24012")
                {
                    colSoLo.Visible = true;
                    //colHanDung.Visible = true;
                }

                int widthListBN = 377;
                try
                {
                    widthListBN = Convert.ToInt32(ConfigurationManager.AppSettings["withListBN"]);
                }
                catch
                {

                }
                if (DungChung.Bien.MaBV == "34019" || DungChung.Bien.MaBV == "14017")
                {
                    btnDonBS.Visible = true;
                    mm_ghiChu.Width = 606;
                    mm_ghiChu.Location = new Point(403, 1);
                    // btnDonDv.Visible = DungChung.Bien.MaBV == "34019"? true : false ;

                }

                if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                {
                    loidan.Visible = true;
                    mm_ghiChu.Visible = false;
                    cbo_loiDan.Visible = true;
                    // btnDonDv.Visible = DungChung.Bien.MaBV == "34019"? true : false ;

                }
                if (DungChung.Bien.MaBV.Substring(0, 2) != "24")
                    colXHH.Visible = false;
                radGiaiQuyet.Properties.Items.Clear();
                radGiaiQuyet.Properties.Items.AddRange(DungChung.Bien._phuongAn);
                this.splitContainerControl1.Size = new System.Drawing.Size(widthListBN, 502);
                if (DungChung.Bien.MaBV == "27001")
                {
                    colStatusct.Visible = false;
                    mnMoiBN.Enabled = false;
                }

                if (DungChung.Bien.MaBV == "30012")
                    mnMoiBN.Enabled = false;
                if (DungChung.Bien.MaBV == "01830")
                    mnThongTinBN.Enabled = false;
                _lTrongDMBH = DungChung.Ham._SetValue_TrongDMBH();
                luptrongDM_KD.DataSource = _lTrongDMBH;
                lup_TrongDM_dv.DataSource = _lTrongDMBH;
                lup_TrongDM_dvCS2.DataSource = _lTrongDMBH;
                dtTimTuNgay.DateTime = System.DateTime.Now;
                dtTimDenNgay.DateTime = System.DateTime.Now;
                _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                //if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin && DungChung.Bien.MaBV == "30007")
                //{
                //    lupNguoiKhamkb.Enabled = false;
                //    lupKhoaKhamkb.Enabled = false;
                //}
                //lupTimMaKP.EditValue = _makp;
                //1. Khám bệnh
                //EnableButton(true);
                //EnableControlKD(false);
                //EnableControlKB(false);
                _lLinhThuoc = new List<LinhThuoc>();
                _lLinhThuoc.Add(new LinhThuoc { Status = 0, Ten_Status = "Lĩnh" });
                _lLinhThuoc.Add(new LinhThuoc { Status = -1, Ten_Status = "K.Lĩnh" });
                _lLinhThuoc.Add(new LinhThuoc { Status = 1, Ten_Status = "Đã Lĩnh" });
                _lLinhThuoc.Add(new LinhThuoc { Status = 2, Ten_Status = "Hủy" });
                lupLinh.DataSource = _lLinhThuoc;

                #region get ds dịch vụ hiển thị đã thay đổi
                try
                {
                    string strSQL = "sp_KB_AllDichVuByMaBV";
                    string[] strpara = new string[] { "@MaBV" };
                    object[] oValue = new object[] { DungChung.Bien.MaBV };
                    SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.VarChar };

                    connect.Connect();

                    DataTable dtTble = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);
                    if (dtTble.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTble.Rows.Count; i++)
                        {
                            DichVu objDV = new DichVu();
                            objDV.MaDV = Convert.ToInt32(dtTble.Rows[i]["MaDV"].ToString());
                            objDV.TenDV = dtTble.Rows[i]["TenDV"].ToString();
                            objDV.TenRG = dtTble.Rows[i]["TenRG"].ToString();
                            objDV.DangBC = dtTble.Rows[i]["DangBC"].ToString();
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["BHTT"].ToString()))
                                objDV.BHTT = Convert.ToInt32(dtTble.Rows[i]["BHTT"].ToString());
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["BHYT"].ToString()))
                                objDV.BHYT = Convert.ToInt32(dtTble.Rows[i]["BHYT"].ToString());
                            objDV.BPDung = dtTble.Rows[i]["BPDung"].ToString();
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["DinhMuc"].ToString()))
                                objDV.DinhMuc = Convert.ToInt32(dtTble.Rows[i]["DinhMuc"].ToString());
                            objDV.DonGia = Convert.ToDouble(dtTble.Rows[i]["DonGia"].ToString());
                            objDV.DonGia2 = Convert.ToDouble(dtTble.Rows[i]["DonGia2"].ToString());
                            objDV.DonGiaBHYT = Convert.ToDouble(dtTble.Rows[i]["DonGiaBHYT"].ToString());
                            objDV.DonGiaTT39 = Convert.ToDouble(dtTble.Rows[i]["DonGiaTT39"].ToString());
                            objDV.DonGiaDV2 = Convert.ToDouble(dtTble.Rows[i]["DonGiaDV2"].ToString());
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["DongY"].ToString()))
                                objDV.DongY = Convert.ToInt32(dtTble.Rows[i]["DongY"].ToString());
                            objDV.DonVi = dtTble.Rows[i]["DonVi"].ToString();
                            objDV.DonViN = dtTble.Rows[i]["DonViN"].ToString();
                            objDV.DSDonGia = dtTble.Rows[i]["DSDonGia"].ToString();
                            objDV.DSDTBN = dtTble.Rows[i]["DSDTBN"].ToString();
                            objDV.DuongD = dtTble.Rows[i]["DuongD"].ToString();
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["DVKTC"].ToString()))
                                objDV.DVKTC = Convert.ToInt32(dtTble.Rows[i]["DVKTC"].ToString());
                            objDV.GhiChu = dtTble.Rows[i]["GhiChu"].ToString();
                            objDV.GiaBHGioiHanTT = Convert.ToDouble(dtTble.Rows[i]["GiaBHGioiHanTT"].ToString());
                            objDV.GiaPhuThu = Convert.ToDouble(dtTble.Rows[i]["GiaPhuThu"].ToString());
                            objDV.HamLuong = dtTble.Rows[i]["HamLuong"].ToString();
                            objDV.IDGoi = dtTble.Rows[i]["IDGoi"].ToString();
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["IDNhom"].ToString()))
                                objDV.IDNhom = Convert.ToInt32(dtTble.Rows[i]["IDNhom"].ToString());
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["IdTieuNhom"].ToString()))
                                objDV.IdTieuNhom = Convert.ToInt32(dtTble.Rows[i]["IdTieuNhom"].ToString());
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["Loai"].ToString()))
                                objDV.Loai = Convert.ToInt32(dtTble.Rows[i]["Loai"].ToString());
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["LThau"].ToString()))
                                objDV.LThau = Convert.ToInt32(dtTble.Rows[i]["LThau"].ToString());
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["LThuoc"].ToString()))
                                objDV.LThuoc = Convert.ToInt32(dtTble.Rows[i]["LThuoc"].ToString());
                            objDV.MaATC = dtTble.Rows[i]["MaATC"].ToString();
                            objDV.MaCC = dtTble.Rows[i]["MaCC"].ToString();
                            objDV.MaDuongDung = dtTble.Rows[i]["MaDuongDung"].ToString();
                            objDV.MaKPsd = dtTble.Rows[i]["MaKPsd"].ToString();
                            objDV.MaNhom = dtTble.Rows[i]["MaNhom"].ToString();
                            objDV.MaQD = dtTble.Rows[i]["MaQD"].ToString();
                            objDV.MaTam = dtTble.Rows[i]["MaTam"].ToString();
                            objDV.NguonGoc = dtTble.Rows[i]["NguonGoc"].ToString();
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["PLoai"].ToString()))
                                objDV.PLoai = Convert.ToInt32(dtTble.Rows[i]["PLoai"].ToString());
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["Status"].ToString()))
                                objDV.Status = Convert.ToInt32(dtTble.Rows[i]["Status"].ToString());
                            objDV.TenHC = dtTble.Rows[i]["TenHC"].ToString();
                            objDV.TenRG = dtTble.Rows[i]["TenRG"].ToString();
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["TrongDM"].ToString()))
                                objDV.TrongDM = Convert.ToInt32(dtTble.Rows[i]["TrongDM"].ToString());
                            if (!string.IsNullOrEmpty(dtTble.Rows[i]["Mien"].ToString()))
                                objDV.Mien = Convert.ToInt32(dtTble.Rows[i]["Mien"].ToString());
                            _lDichvu.Add(objDV);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion

                #region  get DS dịc vụ linq
                //_lDichvu = _dataContext.DichVus.ToList();
                //var qex = _dataContext.DichVuExes.ToList();
                //if (DungChung.Bien.MaBV == "20001")
                //{
                //    foreach (var item in _lDichvu)
                //    {
                //        if (!string.IsNullOrEmpty(item.TenRG) && item.PLoai == 2)
                //        {
                //            string ten = item.TenDV;
                //            item.TenDV = item.TenRG;
                //            item.TenRG = ten;
                //        }
                //    }
                //}
                //else if (DungChung.Bien.MaBV == "260007")//??????????26007?
                //{
                //    foreach (var item in _lDichvu)
                //    {
                //        item.TenDV = item.TenDV + " " + item.HamLuong;
                //    }
                //}
                //// Lấy tên thầu 2017 tạm vào trường DangBC
                //foreach (var item in _lDichvu)
                //{
                //    var qtt = qex.Where(p => p.MaDV == item.MaDV).FirstOrDefault();
                //    if (qtt != null)
                //        item.DangBC = qtt.TenThau2017 ?? "";
                //    else
                //        item.DangBC = "";
                //}
                #endregion

                if (DungChung.Bien.MaBV == "14017")
                {
                    labelControl7.Text = "Bệnh khác:";
                    labelControl17.Text = "Mã ICD:";
                    labelControl23.Visible = labelControl22.Visible = labelControl23.Visible = txtBenhPhu.Visible = txtBenhKhac4.Visible = labelControl21.Visible = labelControl24.Visible = false;
                    btnPackageICD2.Visible = btnPackageICD3.Visible = false;
                    txtBenhKhac2.Visible = txtBenhPhu2.Visible = txtBenhPhu3.Visible = txtBenhKhac3.Visible = txtBenhKhac4.Visible = txtBenhKhac.Visible = false;
                    LupICD2.Visible = LupICD3.Visible = LupICD4.Visible = LupICDKhac.Visible = false;
                    _licd10 = _dataContext.ICD10.ToList();
                    lICD = (from ICD in _licd10.Where(p => p.MaYHCT != null) select new c_ICD { MaICD = ICD.MaYHCT ?? "", TenICD = ICD.TenYHCT + "[" + ICD.TenICD + "]" ?? "", Check = false }).OrderBy(p => p.MaICD).ToList();
                    txtBenhKhac1.Properties.DataSource = lICD.Select(p => p.TenICD).ToList();
                    lupChanDoanKb.Properties.DataSource = lICD.ToList();
                    lupKhac.Properties.DataSource = lICD.Select(p => p.MaICD).ToList();
                    lupMaICDkb.Properties.DataSource = lICD.ToList();



                }
                else
                {
                    lupKhac.Visible = false;
                    txtBenhKhac1.Visible = false;
                    txtBenhKhac.Visible = false;
                    //LupICDKhac.Visible = false;
                    _licd10 = _dataContext.ICD10.ToList();
                    lICD = (from ICD in _licd10 select new c_ICD { MaICD = ICD.MaICD ?? "", TenICD = ICD.TenICD ?? "" }).OrderBy(p => p.TenICD).ToList();

                    lupChanDoanKb.Properties.DataSource = lICD.ToList();
                    txtBenhKhac2.Properties.DataSource = lICD.ToList();
                    txtBenhKhac3.Properties.DataSource = lICD.ToList();

                    string[] arrICD = lICD.Where(p => p.TenICD != null).Select(p => p.TenICD).ToArray();
                    //txtBenhBĐ.Properties.Items.AddRange(arrICD);
                    txtBenhKhac4.Properties.Items.AddRange(arrICD);
                    string[] arrMaICD = lICD.Where(p => p.TenICD != null).Select(p => p.MaICD).ToArray();
                    lupMaICDkb.Properties.DataSource = lICD.ToList();
                    LupICDKhac.Properties.DataSource = lICD.ToList();
                    LupICD2.Properties.DataSource = lICD.ToList();
                    LupICD3.Properties.DataSource = lICD.ToList();
                    LupICD4.Properties.Items.AddRange(arrMaICD);
                }

                if (DungChung.Bien.MaBV == "24012")
                {
                    txtBenhChinh.Visible = txtBenhPhu2.Visible = txtBenhPhu3.Visible = true;
                }

                List<listKP> _lkp = new List<listKP>();
                _lKPhong_data = _dataContext.KPhongs.Where(p => p.Status == 1).ToList();
                var q = (from kp in _lKPhong_data
                         where ((kp.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang) || kp.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                         select kp).OrderBy(p => p.TenKP).ToList();
                if (q.Count > 0)
                {
                    foreach (var i in q)
                    {
                        _lkp.Add(new listKP { MaKP = i.MaKP, TenKP = i.TenKP, PLoai = i.PLoai });
                    }
                    List<KPhong> kpnew = new List<KPhong>();
                    kpnew = _lKPhong_data.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh).ToList();
                    kpnew.AddRange(q.Where(p => DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin ? true : p.MaKP == DungChung.Bien.MaKP).ToList());
                    _lkp.Add(new listKP { MaKP = 0, TenKP = "", PLoai = DungChung.Bien.st_PhanLoaiKP.PhongKham });
                    lupPhongKham.DataSource = q;
                    lupKPhongdv.DataSource = (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin) ? (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "56789" ? kpnew : q) : (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" ? kpnew : q.Where(p => (p.MaKP == DungChung.Bien.MaKP)).ToList());
                    lupKPhongdvCS2.DataSource = (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin) ? (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "56789" ? kpnew : q) : (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" ? kpnew : q.Where(p => (p.MaKP == DungChung.Bien.MaKP)).ToList());
                    lupKhoaDT.Properties.DataSource = _lkp.OrderBy(p => p.TenKP).ToList();
                    if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                    {
                        lupTimMaKP.Properties.DataSource = q.Where(p => DungChung.Bien.MaBV != "30007" || (DungChung.Bien.MaBV == "30007" && p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham));
                        lupKhoaKhamkb.Properties.DataSource = q;
                    }
                    else
                    {

                        q = (from a in q.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                             join b in DungChung.Bien.listKPHoatDong on a.MaKP equals b
                             select a).ToList();
                        lupTimMaKP.Properties.DataSource = q;
                        lupKhoaKhamkb.Properties.DataSource = q;
                    }
                }
                _ltieunhom = _dataContext.TieuNhomDVs.ToList();
                if (lupTimMaKP.EditValue != null)
                {
                    _makp = Convert.ToInt32(lupTimMaKP.EditValue);
                }
                else
                    lupTimMaKP.EditValue = DungChung.Bien.MaKP;
                if (grvBNhankb.GetFocusedRowCellValue(colMaKP) != null && grvBNhankb.GetFocusedRowCellValue(colMaBNhan) != null)
                {
                    txtMaBNhankb.Text = grvBNhankb.GetFocusedRowCellValue(colMaBNhan).ToString();
                    txtTenBenhNhan.Text = grvBNhankb.GetFocusedRowCellValue(colTenBNhan).ToString();
                    txtIdbn.Text = grvBNhankb.GetFocusedRowCellValue(colMaBNhan).ToString();
                    txtMaBNhan.Text = grvBNhankb.GetFocusedRowCellValue(colMaBNhan).ToString();
                }
                #region linq
                _lCanBo = _dataContext.CanBoes.OrderBy(p => p.TenCB).ToList();
                lupNguoiKhamkb.Properties.DataSource = _lCanBo.ToList();
                lupBSTH.DataSource = _lCanBo.ToList();
                lupBSTHCS2.DataSource = _lCanBo.ToList();
                #endregion
                lup_ChuyenKhoa.Properties.DataSource = DungChung.Bien._lChuyenKhoa.Where(p => p.LoaiCK == 1).ToList();
                _lTonDuocall = _dataContext.TonDuocs.ToList();
                SetTTTab();
                // lấy danh sách khoa dược
                //if (kd.Count > 0)
                //{
                lupKhoXuat.Properties.DataSource = _medicinesProvider.GetListKhoaPhong(DungChung.Bien.MaCB, 0);
                //}
                if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.LamSang || DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.PhongKham)
                    lupTimMaKP.EditValue = _makp;
                colGhiChu.Width = 250;
                _setHDSDThuoc();
                if (_sHDSDThuoc != null && DungChung.Bien.MaBV != "30372")
                    cboGhiChu.Items.AddRange(_sHDSDThuoc);
                if (DungChung.Bien.MaBV == "20001")
                {
                    ptPhoto.Visible = false;
                }
                if (DungChung.Bien.MaBV == "30372")
                {
                    List<string> duongDung = new List<string>();
                    duongDung.Add("uống ngày");
                    duongDung.Add("xịt ngày");
                    duongDung.Add("rửa mũi ngày");
                    cbog_DuongD.Items.AddRange(duongDung);

                    List<string> donViUong = new List<string>();
                    donViUong.Add("gói");
                    donViUong.Add("viên");
                    donViUong.Add("ml");
                    Res_Cbb_DonVi.Items.AddRange(donViUong);

                    List<string> ghiChu = new List<string>();
                    ghiChu.Add("Trước ăn");
                    ghiChu.Add("sau ăn no");
                    ghiChu.Add("trước ăn sáng");
                    ghiChu.Add("trước ăn sáng 30 phút");   // hiss 206 2/6/2021
                    ghiChu.Add("trước ăn sáng-tối 30 phút");
                    ghiChu.Add("sau ăn sáng-chiều");
                    ghiChu.Add("sau ăn sáng-tối");
                    ghiChu.Add("sau ăn sáng-tối 1 giờ");
                    cboGhiChu.Items.AddRange(ghiChu);

                }
                trangthaiTT = "Chưa thực hiện";
                btnref_Click(sender, e);
            }
            finally
            {
                load = false;
            }
        }
        /// <summary>
        /// Lưu log ra file @"c:\temp\test.txt" để kiểm tra xem phần mềm chạy chậm ở đâu
        /// </summary>
        string _txtMessLog = "";
        int mabn = 0;

        string trangthaiTT = string.Empty;
        private void grvBNhankb_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            if (xtraNgoaiTru.SelectedTabPage == xtraKhamBenh)
                btnSyncMed.Visible = true;
            else
                btnSyncMed.Visible = false;

            if (xtraNgoaiTru.SelectedTabPage == xtraKeDonNgoai)
            {
                int result = 0;
                
                if (grvBNhankb.GetFocusedRowCellValue(colMaBNhan) != null)
                {
                    txtMaBNhan.Text = grvBNhankb.GetFocusedRowCellValue(colMaBNhan).ToString();
                }
                if (Int32.TryParse(txtMaBNhan.Text, out result))
                    mabn = Convert.ToInt32(txtMaBNhan.Text);
                if (mabn > 0)
                {
                    xtraKeDonNgoai.Controls.Clear();
                    Frm_KeDonNgoai frm = new Frm_KeDonNgoai(mabn, -1, DungChung.Bien.MaKP, true);
                    frm.btn_huy.Visible = false;
                    frm.TopLevel = false;
                    frm.AutoScroll = true;
                    xtraKeDonNgoai.Controls.Add(frm);
                    frm.Dock = DockStyle.Fill;
                    foreach (Control control in xtraKeDonNgoai.Controls)
                    {

                        if (control != frm) { control.Hide(); }
                        else { control.Show(); }
                        frm.Show();
                    }
                    var bnhan = _dataContext.BenhNhans.Where(x => x.MaBNhan == mabn).FirstOrDefault();
                    if (bnhan.Status == 0)
                    {
                        barBtnDonThuocH_TT04.Enabled = false;
                        barBtnDonThuocN_TT04.Enabled = false;
                        barBtnThuocThuongTT04.Enabled = false;
                    }
                    else
                    {
                        barBtnDonThuocH_TT04.Enabled = true;
                        barBtnDonThuocN_TT04.Enabled = true;
                        barBtnThuocThuongTT04.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Chưa chọn bệnh nhân");
                }
            }
            chkKhamChuyenGia.Checked = false;
            lupKhoaDT.Enabled = true;
            string curFile = @"c:\temp\test.txt";
            _changeBenhNhan = true;
            _listBNKB = new List<BNKB>();
            _listGiaSua = new List<Ham.giaSoLoHSD>();
            _iddthuoc = 0;
            sothangSua = 1;
            sothangOld = 1;
            _txtMessLog = "\r\n-----------------------------------------------";
            _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + "- 1-grvBNhankb_FocusedRowChanged    ";
            if (process)
                return;
            int rs;
            int _int_maBN = 0;
            int _idp = -1;

            string _sothe = "-"; _lydodongy = "";
            if (xtraKhamBenh.Text.Contains("*") || xtraChiDinh.Text.Contains("*") || xtabDichVuCS2.Text.Contains("*"))
            {
                DialogResult _result = MessageBox.Show("Bạn chưa lưu dữ liệu, Bạn có muốn lưu không?", "thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    mnLuu_ItemClick(null, null);
                }
            }
            if (grvBNhankb.GetFocusedRowCellValue(colDTuong) != null && grvBNhankb.GetFocusedRowCellValue(colDTuong).ToString() == "KSK")
            {
                btnPostGPLX.Visible = true;
            }
            else if (grvBNhankb.GetFocusedRowCellValue(colDTuong) != null && grvBNhankb.GetFocusedRowCellValue(colDTuong).ToString() != "KSK")
            {
                btnPostGPLX.Visible = false;
            }
            if (grvBNhankb.GetFocusedRowCellValue(colMaBNhan) != null && grvBNhankb.GetFocusedRowCellValue(colMaBNhan).ToString() != "")
            {
                try
                {
                    if (grvBNhankb.GetFocusedRowCellValue(colIDP) != null && grvBNhankb.GetFocusedRowCellValue(colIDP).ToString() != "")
                    {

                        _idp = Convert.ToInt32(grvBNhankb.GetFocusedRowCellValue(colIDP));

                    }
                }
                catch (Exception)
                {
                    _idp = -1;
                }
                List<DThuoc> _ldthuoc = new List<DThuoc>();
                txtMaBNhan.Text = grvBNhankb.GetFocusedRowCellValue(colMaBNhan).ToString();
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                txtTenBenhNhan.Text = grvBNhankb.GetFocusedRowCellValue(colTenBNhan).ToString();
                txtIdbn.Text = grvBNhankb.GetFocusedRowCellValue(colMaBNhan).ToString();
                if (grvBNhankb.GetFocusedRowCellValue(colTuoi) != null && grvBNhankb.GetFocusedRowCellValue(colTuoi).ToString() != "")
                    txtTuoi.Text = grvBNhankb.GetFocusedRowCellValue(colTuoi).ToString();
                if (grvBNhankb.GetFocusedRowCellValue(colSoThe) != null && grvBNhankb.GetFocusedRowCellValue(colSoThe).ToString() != "")
                {
                    _sothe = grvBNhankb.GetFocusedRowCellValue(colSoThe).ToString();
                    txtSoThe.Text = _sothe;
                }
                else
                    txtSoThe.Text = "";
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + "- 2-grvBNhankb_FocusedRowChanged   ";
                if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                {
                    _listBNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();

                    _txtMessLog += string.Format("  \r\n {0:MM/dd/yyyy HH:mm:ss.fff}  - {1} - 3-grvBNhankb_FocusedRowChanged - BNKB _1031      ", DateTime.Now, txtMaBNhan.Text);
                    //1. kb
                    if (xtraNgoaiTru.SelectedTabPage == xtraKhamBenh)
                    {

                        _changeBenhNhan = true;
                        lupKhoXuat.EditValue = 0;
                        _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 4-grvBNhankb_FocusedRowChanged - lupKhoXuat _1048    ";
                        _changeBenhNhan = false;
                        grcChuyenKhoa.DataSource = _listBNKB;//tbKB;
                        _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 5-grvBNhankb_FocusedRowChanged - grcChuyenKhoa _1051     ";


                        if (_listBNKB.Count <= 0)
                            lupNguoiKhamkb.EditValue = DungChung.Bien.MaCB;
                        _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 6-grvBNhankb_FocusedRowChanged - lupNguoiKhamkb _1061    ";

                        if (_listBNKB.Count > 1)
                            panelDSKB.Visible = true;
                        else
                            panelDSKB.Visible = false;

                        //kt
                        // 1. khám bệnh

                        // kê đơn
                        Int32 id = 0;
                        int Sopl = 0;
                        #region ADO
                        //string sqlDonThuoc = "SELECT dbo.DThuoc.*, DThuocct.SoPL FROM dbo.DThuoc JOIN dbo.DThuocct ON dbo.DThuoc.MaBNhan = '" + _int_maBN + "' and DThuocct.IDDon = DThuoc.IDDon AND PLDV = 1 AND (KieuDon = -1 OR KieuDon = 8)";

                        //DataTable tbDThuoc = connect.FillDatatable(sqlDonThuoc, CommandType.Text);
                        //if (tbDThuoc.Rows.Count > 0)
                        //{
                        //    if (!string.IsNullOrEmpty(tbDThuoc.Rows[0]["SoPL"].ToString()))
                        //    Sopl = Convert.ToInt32(tbDThuoc.Rows[0]["SoPL"].ToString());
                        //    txtIdDonThuoc.Text = tbDThuoc.Rows[0]["IDDon"].ToString();
                        //    mm_ghiChu.Text = tbDThuoc.Rows[0]["GhiChu"].ToString();
                        //    if (!string.IsNullOrEmpty(tbDThuoc.Rows[0]["KieuDon"].ToString()))
                        //        _kieudon = Convert.ToInt32(tbDThuoc.Rows[0]["KieuDon"].ToString());
                        //    else
                        //        _kieudon = -3;
                        //    // kê đơn ngoài

                        //    if (_kieudon == -2)
                        //    {
                        //        chkKDNgoai.Checked = true;

                        //    }
                        //    else
                        //    {
                        //        chkKDNgoai.Checked = false;
                        //        if (tbDThuoc.Rows[0]["MaKXuat"] != null && !string.IsNullOrEmpty(tbDThuoc.Rows[0]["MaKXuat"].ToString()) && tbDThuoc.Rows[0]["MaKXuat"].ToString() != "")
                        //            _changeBenhNhan = true;
                        //            lupKhoXuat.EditValue = Convert.ToInt32(tbDThuoc.Rows[0]["MaKXuat"].ToString());
                        //            _changeBenhNhan = false;
                        //    }
                        //    //hiển thị chi tiết đơn
                        //    id = Convert.ToInt32(tbDThuoc.Rows[0]["IDDon"].ToString());
                        //    _iddthuoc = id;

                        //}
                        #endregion
                        #region linq
                        if (DungChung.Bien.MaBV == "34019")
                        {
                            var ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
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
                        var _donThuoc = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1 && (p.KieuDon == -1 || p.KieuDon == 8))
                                         join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                         select new
                                         {
                                             dt,
                                             dtct.SoPL
                                         }).ToList();

                        _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + "  - " + txtMaBNhan.Text + " 7-grvBNhankb_FocusedRowChanged - _donThuoc 1114      ";

                        var _loidan = (from ld in _dataContext.DThuocs.Where(p => p.MaCB == MaCBs).Where(p => p.NgayKe >= DateTime.Today)
                                       select new { ld.GhiChu }).ToList();
                        if (_loidan.Count > 0 && (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389"))
                        {
                            if (_loidan.Count >= 10)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    cbo_loiDan.EditValue += _loidan[i].GhiChu + "\n";
                                }
                            }
                            else
                            {
                                for (int i = 0; i < _loidan.Count; i++)
                                {
                                    cbo_loiDan.EditValue += _loidan[i].GhiChu + "\n";
                                }
                            }

                        }
                        int sovv = 0;
                        if (_donThuoc.Count > 0)
                        {
                            Sopl = _donThuoc.First().SoPL;
                            sovv = _donThuoc.First().dt.SoVV ?? 0;
                            txtIdDonThuoc.Text = _donThuoc.First().dt.IDDon.ToString();
                            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                            {
                                cbo_loiDan.Text = _donThuoc.First().dt.GhiChu;
                            }
                            else
                            {
                                mm_ghiChu.Text = _donThuoc.First().dt.GhiChu;
                            }
                            if (_donThuoc.First().dt.KieuDon != null)
                                _kieudon = _donThuoc.First().dt.KieuDon.Value;
                            else
                                _kieudon = -3;
                            // kê đơn ngoài
                            if (_donThuoc.First().dt.KieuDon != null && _donThuoc.First().dt.KieuDon.Value == -2)
                            {
                                chkKDNgoai.Checked = true;

                            }
                            else
                            {
                                chkKDNgoai.Checked = false;
                                if (_donThuoc.First().dt.MaKXuat != null)
                                {
                                    lupKhoXuat.EditValue = _donThuoc.First().dt.MaKXuat;
                                    int mkp = (int)lupKhoXuat.EditValue;
                                    var kp = _dataContext.KPhongs.Where(p => p.MaKP == mkp).ToList();
                                    if (kp.Count > 0)
                                    {
                                        ppxuat = (int)kp.First().PPXuat;
                                    }
                                }
                                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 8-grvBNhankb_FocusedRowChanged - lupKhoXuat 1136     ";

                            }

                            //
                            //hiển thị chi tiết đơn
                            id = _donThuoc.First().dt.IDDon;
                            _iddthuoc = id;

                            if (!string.IsNullOrEmpty(txtIdDonThuoc.Text))
                            {
                                var idDon = int.Parse(txtIdDonThuoc.Text);
                                var donThuoc = _donThuoc.Select(s => s.dt).FirstOrDefault(f => f.IDDon == idDon);

                                if (donThuoc.DongBo != null)
                                    btnSyncMed.Enabled = !donThuoc.DongBo.Value;
                                else
                                    btnSyncMed.Enabled = true;

                                int mkp = (int)lupKhoXuat.EditValue;
                                lupIDThuoc.DataSource = _medicinesProvider.GetLupMaDuoc(mkp, idDon, 0);
                            }
                            else
                            {
                                btnSyncMed.Enabled = false;
                            }
                        }
                        #endregion
                        else
                        {
                            btnSyncMed.Enabled = false;

                            mm_ghiChu.ResetText();
                            cbo_loiDan.ResetText();
                            txtIdDonThuoc.Text = "";
                            iddthuocmau1 = 0;
                            txtSoDon.Text = "";
                            _iddthuoc = 0;
                            _changeBenhNhan = true;
                            lupKhoXuat.EditValue = 0;
                            lupKhoXuat.EditValue = DungChung.Bien.MaKho;// chậm
                            _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 9-grvBNhankb_FocusedRowChanged - lupKhoXuat 1162     ";
                            _changeBenhNhan = false;
                        }
                        // chậm
                        _lDthuocct.Clear();


                        _lDthuocct = _medicinesProvider.ViewInfoMedicineDThuoc(id);
                        var dThuocFi = _dataContext.DThuocs.Where(p => p.IDDon == id).FirstOrDefault();
                        _listGiaSua.Clear();
                        foreach (var a in _lDthuocct)
                        {
                            _listGiaSua.Add(new Ham.giaSoLoHSD { MaDV = a.MaDV ?? 0, Gia = a.DonGia, SoLuong = a.SoLuong, HanDung = a.HanDung, SoLo = a.SoLo });
                        }
                        binSDonThuocct.DataSource = _lDthuocct;
                        grcDonThuocct.DataSource = binSDonThuocct;
                        _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 10-grvBNhankb_FocusedRowChanged - grcDonThuocct 1177     ";
                        if (grvDonThuocct.RowCount > 0)
                        {
                            if (Sopl > 0 || ((DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && sovv == -1))// bv 12345 đã duyệt sẽ ko cho sửa
                                grvDonThuocct.OptionsBehavior.ReadOnly = true;
                            else
                                grvDonThuocct.OptionsBehavior.ReadOnly = false;
                        }
                        var qslSua = _dataContext.DThuoccts.Where(p => p.IDDon == _iddthuoc).ToList();
                        if (qslSua.Count > 0)
                            sothangOld = qslSua.First().SoLuong / qslSua.First().SoLuongct;
                        sothangSua = sothangOld;

                        if (_lDthuocct == null)
                        {
                            btnSyncMed.Enabled = false;
                            barBtnDonThuocH_TT04.Enabled = false;
                            barBtnDonThuocN_TT04.Enabled = false;
                            barBtnThuocThuongTT04.Enabled = false;
                        }
                        else if (_lDthuocct.Count == 0)
                        {
                            btnSyncMed.Enabled = false;
                            barBtnDonThuocH_TT04.Enabled = false;
                            barBtnDonThuocN_TT04.Enabled = false;
                            barBtnThuocThuongTT04.Enabled = false;
                        }
                        else
                        {
                            btnSyncMed.Enabled = true;
                            barBtnDonThuocH_TT04.Enabled = true;
                            barBtnDonThuocN_TT04.Enabled = true;
                            barBtnThuocThuongTT04.Enabled = true;
                        }

                        if (dThuocFi != null && dThuocFi.MaDTQG != null)
                        {
                            btnSyncMed.Enabled = false;
                            if (dThuocFi.MaDTQG.EndsWith("H") || dThuocFi.MaDTQG.EndsWith("h"))
                            {
                                barBtnDonThuocH_TT04.Enabled = true;
                                barBtnDonThuocN_TT04.Enabled = false;
                                barBtnThuocThuongTT04.Enabled = false;
                            }
                            else if (dThuocFi.MaDTQG.EndsWith("N") || dThuocFi.MaDTQG.EndsWith("n"))
                            {
                                barBtnDonThuocH_TT04.Enabled = false;
                                barBtnDonThuocN_TT04.Enabled = true;
                                barBtnThuocThuongTT04.Enabled = false;
                            }
                            else
                            {
                                barBtnDonThuocH_TT04.Enabled = false;
                                barBtnDonThuocN_TT04.Enabled = false;
                                barBtnThuocThuongTT04.Enabled = true;
                            }
                        }
                    }
                    //kết thúc kê đơn
                    // chỉ định
                    var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);

                    if (xtraNgoaiTru.SelectedTabPage == xtraChiDinh || (xtraNgoaiTru.SelectedTabPage == xtraKhamBenh && hthong != null && hthong.IsTV == true && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")))
                    {
                        int idcd = 0;
                        if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                        {
                            #region linq
                            var q = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                            if (q.Count > 0)
                            {
                                txtChiDinh.Text = q.First().IDDon.ToString();
                                idcd = q.First().IDDon;
                            }
                            #endregion
                            #region ADO
                            //string strSQLDT = "select * from DThuoc where MaBNhan = '" + _int_maBN + "' and PLDV = 2";

                            //DataTable tbdt = connect.FillDatatable(strSQLDT, CommandType.Text);

                            //if (tbdt.Rows.Count > 0)
                            //{
                            //    txtChiDinh.Text = tbdt.Rows[0]["IDDon"].ToString();
                            //    if (!string.IsNullOrEmpty(tbdt.Rows[0]["IDDon"].ToString()))
                            //        idcd = Convert.ToInt32(tbdt.Rows[0]["IDDon"].ToString());
                            //}
                            #endregion
                            #region linq
                            var data = (from dt in _dataContext.DThuoccts.Where(p => p.IDDon == idcd).Where(p => p.LoaiDV < 3)
                                        join dv in _dataContext.DichVus on dt.MaDV equals dv.MaDV
                                        select new DonThuocct
                                        {
                                            IDDon = dt.IDDon,
                                            IDDonct = dt.IDDonct,
                                            MaDV = dt.MaDV,
                                            TenDV = dv.TenDV,
                                            DonVi = dt.DonVi,
                                            DonGia = dt.DonGia,
                                            SoLuong = dt.SoLuong,
                                            ThanhTien = dt.ThanhTien,
                                            TienBN = dt.TienBN,
                                            TienBH = dt.TienBH,
                                            TrongBH = dt.TrongBH,
                                            NgayNhap = dt.NgayNhap,
                                            DuongD = dt.DuongD,
                                            SoPL = dt.SoPL,
                                            Status = dt.Status,
                                            IDCD = dt.IDCD,
                                            MaCB = dt.MaCB,
                                            MaKP = dt.MaKP,
                                            IDKB = dt.IDKB,
                                            Loai = dt.Loai,
                                            ThanhToan = dt.ThanhToan,
                                            MaKPtk = dt.MaKPtk,
                                            MaKXuat = dt.MaKXuat,
                                            TyLeTT = dt.TyLeTT,
                                            XHH = dt.XHH == 1 ? true : false,
                                            MaQD = dv.MaQD
                                        }).ToList();
                            if (data.Exists(o => o.TenDV.ToLower().Contains("khám bác sỹ chuyên gia")))
                                chkKhamChuyenGia.Checked = true;
                            else
                                chkKhamChuyenGia.Checked = false;
                            binSChiDinhct.DataSource = data.ToList();
                            grcChiDinh.DataSource = binSChiDinhct;
                            _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 11-grvBNhankb_FocusedRowChanged - grcChiDinh 1253    ";

                            #endregion
                            #region ADO
                            //string sql = "SELECT IDDonct,IDDon,MaDV,DonVi,DonGia,SoLo,SoLuong,ThanhTien,TienBN,TienBH,TienChenh,TrongBH,NgayNhap,DuongD,MoiLan,DviUong,SoLan,Luong,MaCC,SoPL,Status,IDCD,MaCB,DSCBTH,MaKP,IDKB,Loai,ThanhToan,Mien,GhiChu,MaKPtk,MaKXuat,TyLeTT,SoLuongct,AttachIDDonct,HanDung,LoaiDV ,XHH = CASE WHEN dbo.DThuocct.XHH = 1 THEN 1 ELSE 0 end FROM dbo.DThuocct WHERE IDDon = '" + idcd + "'";

                            //DataTable tb = connect.FillDatatable(sql, CommandType.Text);
                            //grcChiDinh.DataSource = tb;
                            #endregion
                        }
                    }
                    if (xtraNgoaiTru.SelectedTabPage == xtabDichVuCS2)
                    {
                        int idcd = 0;
                        if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                        {
                            #region linq
                            var q = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                            if (q.Count > 0)
                            {
                                txtChiDinh.Text = q.First().IDDon.ToString();
                                idcd = q.First().IDDon;
                            }
                            #endregion
                            #region ADO
                            //string strSQLDT = "select * from DThuoc where MaBNhan = '" + _int_maBN + "' and PLDV = 2";

                            //DataTable tbdt = connect.FillDatatable(strSQLDT, CommandType.Text);

                            //if (tbdt.Rows.Count > 0)
                            //{
                            //    txtChiDinh.Text = tbdt.Rows[0]["IDDon"].ToString();
                            //    if (!string.IsNullOrEmpty(tbdt.Rows[0]["IDDon"].ToString()))
                            //        idcd = Convert.ToInt32(tbdt.Rows[0]["IDDon"].ToString());
                            //}
                            #endregion
                            #region linq
                            var data = (from dt in _dataContext.DThuoccts.Where(p => p.IDDon == idcd).Where(p => p.LoaiDV == 3)
                                        join dv in _dataContext.DichVus on dt.MaDV equals dv.MaDV
                                        select new DonThuocct
                                        {
                                            IDDon = dt.IDDon,
                                            IDDonct = dt.IDDonct,
                                            MaDV = dt.MaDV,
                                            TenDV = dv.TenDV,
                                            DonVi = dt.DonVi,
                                            DonGia = dt.DonGia,
                                            SoLuong = dt.SoLuong,
                                            ThanhTien = dt.ThanhTien,
                                            TienBN = dt.TienBN,
                                            TienBH = dt.TienBH,
                                            TrongBH = dt.TrongBH,
                                            NgayNhap = dt.NgayNhap,
                                            DuongD = dt.DuongD,
                                            SoPL = dt.SoPL,
                                            Status = dt.Status,
                                            IDCD = dt.IDCD,
                                            MaCB = dt.MaCB,
                                            MaKP = dt.MaKP,
                                            IDKB = dt.IDKB,
                                            Loai = dt.Loai,
                                            ThanhToan = dt.ThanhToan,
                                            MaKPtk = dt.MaKPtk,
                                            MaKXuat = dt.MaKXuat,
                                            TyLeTT = dt.TyLeTT,
                                            XHH = dt.XHH == 1 ? true : false,
                                            MaQD = dv.MaQD
                                        }).ToList();
                            if (hthong != null && hthong.IsTV == true && data.Exists(o => o.TenDV.ToLower().Contains("khám bác sỹ chuyên gia")))
                                chkKhamChuyenGia.Checked = true;
                            else
                                chkKhamChuyenGia.Checked = false;
                            binDichVuCS2.DataSource = data.ToList();
                            grcDichVuCS2.DataSource = binDichVuCS2;
                            _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 11-grvBNhankb_FocusedRowChanged - grcChiDinh 1253    ";

                            #endregion
                            #region ADO
                            //string sql = "SELECT IDDonct,IDDon,MaDV,DonVi,DonGia,SoLo,SoLuong,ThanhTien,TienBN,TienBH,TienChenh,TrongBH,NgayNhap,DuongD,MoiLan,DviUong,SoLan,Luong,MaCC,SoPL,Status,IDCD,MaCB,DSCBTH,MaKP,IDKB,Loai,ThanhToan,Mien,GhiChu,MaKPtk,MaKXuat,TyLeTT,SoLuongct,AttachIDDonct,HanDung,LoaiDV ,XHH = CASE WHEN dbo.DThuocct.XHH = 1 THEN 1 ELSE 0 end FROM dbo.DThuocct WHERE IDDon = '" + idcd + "'";

                            //DataTable tb = connect.FillDatatable(sql, CommandType.Text);
                            //grcChiDinh.DataSource = tb;
                            #endregion
                        }
                    }
                    if (_listBNKB.Count > 0)
                    {

                        mmChanDoanBD.Text = _listBNKB.First().ChanDoanBD;
                        txtLyDoKham.Text = _listBNKB.First().LyDoKham;
                        txtDauHieuLamSang.Text = _listBNKB.First().DHLS;

                        //if (!string.IsNullOrEmpty(_listBNKB.First().MaICD2))
                        //{
                        //string[] a = _listBNKB.First().MaICD2.Split(';');
                        //lupKhac.SetEditValue(a);
                        //}
                        lupKhac.Text = _listBNKB.First().MaICD2;//.Replace(";", ", ");

                        txtBenhKhac1.Text = _listBNKB.First().BenhKhac;//.Replace(";", ", ");
                    }

                }

                LoadData();
                #region ADO
                //string strSQLRaVien = "select * from RaVien where MaBNhan = '" + _int_maBN + "'";

                //DataTable tbrv = connect.FillDatatable(strSQLRaVien, CommandType.Text);

                #endregion
                var rvien = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                //if (tbrv.Rows.Count > 0)
                if (rvien.Count > 0)
                {
                    mnKThucKham.Caption = "Hủy K.Thúc khám";
                    int a = lupTimMaKP.EditValue != null ? (int)lupTimMaKP.EditValue : 0;
                    if (DungChung.Bien.MaBV == "24012" && a == 18)
                    {
                        TTRV.Visible = true;
                        cbo_loiDan.Size = new System.Drawing.Size(317, 20);
                    }
                    else
                    {
                        TTRV.Visible = false;
                        cbo_loiDan.Size = new System.Drawing.Size(415, 20);
                    }
                }
                else
                {
                    mnKThucKham.Caption = "Kết thúc khám";
                    TTRV.Visible = false;
                    cbo_loiDan.Size = new System.Drawing.Size(415, 20);
                }
                //lich su kcb
                //if (xtraNgoaiTru.SelectedTabPage == xtraLS_KCB)
                //{
                #region linq
                //var lsu = (from bn in _dataContext.BenhNhans.Where(p => p.IDPerson == _idp).Where(p => p.SThe != null && p.SThe.Length > 1)
                //           join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                //           select new { rv.NgayRa, rv.ChanDoan, bn.MaBNhan, bn.NNhap }).ToList();
                //grcLichSuKCB.DataSource = lsu.ToList();
                //_txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 12-grvBNhankb_FocusedRowChanged - grcLichSuKCB 1284      "; 
                #endregion
                #region ADO
                try
                {

                    connect.Connect();

                    string strSQL = "select dbo.BenhNhan.MaBNhan, dbo.BenhNhan.NNhap,dbo.RaVien.ChanDoan, dbo.RaVien.NgayRa from BenhNhan join RaVien on BenhNhan.SThe = '" + _sothe + "' AND RaVien.MaBNhan = BenhNhan.MaBNhan  AND dbo.BenhNhan.SThe is not NULL AND LEN(dbo.BenhNhan.SThe) > 1";

                    DataTable dtTble = connect.FillDatatable(strSQL, CommandType.Text);
                    // if (dtTble.Rows.Count <= 0) return;
                    grcLichSuKCB.DataSource = dtTble;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion
                // }
                #region linq
                //var soluotkham = (from bn in _dataContext.BenhNhans.Where(p => p.IDPerson == _idp).Where(p => p.SThe != null && p.SThe.Length > 1)
                //                  select new { bn.NNhap, bn.TenBNhan, bn.MaBNhan }).OrderByDescending(p => p.NNhap).ToList();
                //_txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 13-grvBNhankb_FocusedRowChanged - soluotkham 1306    "; 
                //hpl_SoLanKham.Text = "Số lần khám: " + soluotkham.Count.ToString();
                //if (soluotkham.Count > 0)
                //    hpl_NgayKhamGan.Text = "Ngày khám gần nhất: " + (soluotkham.Count == 1 ? soluotkham.First().NNhap.Value.ToString("dd/MM/yyyy") : soluotkham.Skip(1).First().NNhap.Value.ToString("dd/MM/yyyy"));
                //else
                //    hpl_NgayKhamGan.Text = "Ngày khám gần nhất: ";
                #endregion

                #region ADO
                try
                {

                    connect.Connect();

                    string strSQL = "select dbo.BenhNhan.MaBNhan, dbo.BenhNhan.NNhap, BenhNhan.TenBNhan from BenhNhan where BenhNhan.IDPerson = '" + _idp.ToString() + "' AND dbo.BenhNhan.SThe IS not NULL AND LEN(dbo.BenhNhan.SThe) > 1 order by NNhap desc";

                    DataTable dtTble = connect.FillDatatable(strSQL, CommandType.Text);
                    hpl_SoLanKham.Text = "Số lần khám: " + dtTble.Rows.Count;
                    if (dtTble.Rows.Count > 0)
                        hpl_NgayKhamGan.Text = "Ngày khám gần nhất: " + (dtTble.Rows.Count == 1 ? Convert.ToDateTime(dtTble.Rows[0]["NNhap"]).ToString("dd/MM/yyyy") : Convert.ToDateTime(dtTble.Rows[1]["NNhap"]).ToString("dd/MM/yyyy"));
                    else
                        hpl_NgayKhamGan.Text = "Ngày khám gần nhất: ";


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion

                var vv = _dataContext.VaoViens.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                if (vv != null && vv.MaKP != null)
                {
                    lupKhoaDT.Enabled = false;
                }
            }
            else
            {
                txtIdDonThuoc.Text = "";
                iddthuocmau1 = 0;
                txtMaBNhan.Text = "";
                txtTenBenhNhan.Text = "";
                txtTuoi.Text = "";
                grcChuyenKhoa.DataSource = null;
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 14-grvBNhankb_FocusedRowChanged - grcChuyenKhoa 1347     ";
                grcLichSuKCB.DataSource = null;
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 15-grvBNhankb_FocusedRowChanged - grcLichSuKCB 1349      ";
                _iddthuoc = 0;

            }
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn";

            if (_listBNKB.Count > 0)
            {
                grvchiDinh.OptionsBehavior.ReadOnly = false;
                colMaDVcd.OptionsColumn.AllowEdit = true;
                grvDichVuCS2.OptionsBehavior.ReadOnly = false;
                colDichVuCS2.OptionsColumn.AllowEdit = true;
            }
            else
            {
                grvchiDinh.OptionsBehavior.ReadOnly = true;
                colMaDVcd.OptionsColumn.AllowEdit = false;
                grvDichVuCS2.OptionsBehavior.ReadOnly = true;
                colDichVuCS2.OptionsColumn.AllowEdit = false;
            }
            if (xtraKhamBenh.Text.Contains("K.Bệnh"))
            {
            }
            else
            {
                if (xtraKhamBenh.Text.Contains("vụ"))
                    grvchiDinh.Focus();
            }
            mnLuu.Enabled = false;
            _changeBenhNhan = false;
            // không cho phép sửa khi BN đã TT
            //if (DungChung.Ham.KTraTT(_dataContext, txtMaBNhan.Text))
            //{
            //    grvDonThuocct.OptionsBehavior.ReadOnly = false;
            //}
            //else
            //{
            //    grvDonThuocct.OptionsBehavior.ReadOnly = true;
            //}
            ////
            //EnableControlKB(true);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("lỗi!  grvBNhankb_FocusedRowChanged" + ex.Message);
            //}

            if (File.Exists(curFile))
            {
                File.AppendAllText(curFile, _txtMessLog);
            }
            GC.Collect();
        }
        //Kiem tra các trường trên form nhập trước khi lưu
        #region KtraluuKhambenh
        private bool KTraKB()
        {
            if (string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân");
                grcBNhankb.Focus();
                return false;
            }

            if (dtNgayKhamkb.EditValue == null)
            {
                MessageBox.Show("Bạn chưa nhập ngày khám");
                dtNgayKhamkb.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupKhoaKhamkb.Text))
            {
                MessageBox.Show("Bạn chưa nhập khoa khám");
                lupKhoaKhamkb.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupNguoiKhamkb.Text))
            {
                MessageBox.Show("bạn chưa nhập người khám bệnh");
                lupNguoiKhamkb.Focus();
                return false;
            }
            if (radGiaiQuyet.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(lupKhoaDT.Text))
                {
                    MessageBox.Show("Bạn chưa chọn khoa điều trị");
                    lupKhoaDT.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(dt_NgayChuyen.Text))
                {
                    MessageBox.Show("Bạn chưa nhập ngày vào!");
                    dt_NgayChuyen.Focus();
                    return false;
                }
            }
            if (radGiaiQuyet.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(dt_NgayChuyen.Text))
                {
                    MessageBox.Show("Bạn chưa nhập ngày chuyển!");
                    dt_NgayChuyen.Focus();
                    return false;
                }
            }
            if (radGiaiQuyet.SelectedIndex == 0)
            {
                if (string.IsNullOrEmpty(dt_NgayChuyen.Text))
                {
                    MessageBox.Show("Bạn chưa nhập ngày ra!");
                    dt_NgayChuyen.Focus();
                    return false;
                }
            }
            if (radGiaiQuyet.SelectedIndex == 3)
            {
                if (string.IsNullOrEmpty(lupKhoaDT.Text))
                {
                    MessageBox.Show("Bạn chưa chọn phòng khám chuyển đến");
                    lupKhoaDT.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(dt_NgayChuyen.Text))
                {
                    MessageBox.Show("Bạn chưa nhập ngày chuyển phòng!");
                    dt_NgayChuyen.Focus();
                    return false;
                }
                if (lupKhoaDT.Text == lupKhoaKhamkb.Text)
                {
                    MessageBox.Show("Khoa phòng khám phối hợp trùng với khoa phòng hiện tại", "Thông báo", MessageBoxButtons.OK);
                    lupKhoaDT.Focus();
                    return false;
                }

            }
            if (radGiaiQuyet.SelectedIndex == 1 || radGiaiQuyet.SelectedIndex == 0 || radGiaiQuyet.SelectedIndex == 2 || radGiaiQuyet.SelectedIndex == 3)
            {

                if (string.IsNullOrEmpty(lupChanDoanKb.Text) && string.IsNullOrEmpty(lupMaICDkb.Text))
                {
                    MessageBox.Show("Cần nhập bệnh chính để vào viện hoặc ra viện");
                    lupChanDoanKb.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(lup_ChuyenKhoa.Text))
            {
                MessageBox.Show("Bạn chưa chọn chuyên khoa");
                lup_ChuyenKhoa.Focus();
                return false;
            }
            if (lupKhoaKhamkb.Text != lupTimMaKP.Text)
            {
                MessageBox.Show("Bệnh nhân được chỉ định đến: " + lupTimMaKP.Text + ", bạn không thể khám bệnh tại: " + lupKhoaKhamkb.Text);
                lupKhoaKhamkb.Focus();
                return false;
            }
            if (dt_NgayChuyen.DateTime != null && (radGiaiQuyet.SelectedIndex == 0 || radGiaiQuyet.SelectedIndex == 1 || radGiaiQuyet.SelectedIndex == 2 || radGiaiQuyet.SelectedIndex == 3))
            {
                if (dt_NgayChuyen.DateTime > DateTime.Now && DungChung.Bien.MaBV != "24012")
                {
                    MessageBox.Show("Ngày Ra viện|Chuyển viện|Chuyển PK không được lớn hơn ngày hiện tại!");
                    dt_NgayChuyen.Focus();
                    return false;
                }
                if (dt_NgayChuyen.DateTime < dtNgayKhamkb.DateTime)
                {
                    MessageBox.Show("Ngày Ra viện|Chuyển viện|Chuyển PK không được nhỏ hơn ngày khám!");
                    dt_NgayChuyen.Focus();
                    return false;
                }
            }

            return true;

        }
        #endregion
        #region KtraLuuKedon
        private bool KTraKD()
        {
            string _tenthuockogia = "";
            for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
            {
                double gia = 0;
                if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null && grvDonThuocct.GetRowCellValue(i, colDonGia).ToString() != "")
                {
                    gia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());

                }
                if (gia <= 0) _tenthuockogia += grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + "; ";
            }
            if (!string.IsNullOrEmpty(_tenthuockogia))
            {
                MessageBox.Show(string.Format("Các thuốc: {0} chưa có giá", _tenthuockogia));
                return false;
            }
            return true;
        }
        #endregion

        void GetValueSoThang(ThuocThang data)
        {
            sothangSua = data.SoThang;
            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
            {
                cbo_loiDan.Text = data.SoThang + ";" + data.TuNgay.ToString("dd/MM/yyyy") + ";" + data.DenNgay.ToString("dd/MM/yyyy") + ";" + data.GhiChu + ";" + data.CachSac + ";" + data.CachUong;
            }
            else
            {
                mm_ghiChu.Text = data.SoThang + ";" + data.TuNgay.ToString("dd/MM/yyyy") + ";" + data.DenNgay.ToString("dd/MM/yyyy") + ";" + data.GhiChu + ";" + data.CachSac + ";" + data.CachUong;
            }


        }

        private void btnDonThuoc_Click(object sender, EventArgs e)
        {
            frmDonNgoaiTru frm = new frmDonNgoaiTru();
            frm.ShowDialog();
        }

        private void btnChonBN_Click(object sender, EventArgs e)
        {
            frmChonBN frm = new frmChonBN();
            frm.ShowDialog();
        }
        public void KeDonNgoai_Load()
        {
            int _int_maBN = 0;
            Int32.TryParse(txtMaBNhan.Text, out _int_maBN);
            if (_int_maBN > 0)
            {
                if (DungChung.Bien.MaBV == "24012")
                {
                    Frm_KeDonNgoai frm = new Frm_KeDonNgoai(_int_maBN, -1, DungChung.Bien.MaKP, true);
                    frm.btn_huy.Visible = false;
                    frm.TopLevel = false;
                    frm.AutoScroll = true;
                    frm.Dock = DockStyle.Fill;
                    xtraKeDonNgoai.Controls.Add(frm);
                    foreach (Control control in xtraKeDonNgoai.Controls)
                    {

                        if (control != frm) { control.Hide(); }
                        else { control.Show(); }
                        frm.Show();
                    }
                }
                else if (DungChung.Bien.MaBV == "24297")
                {
                    frm_kedon frm = new frm_kedon(_int_maBN, -1, DungChung.Bien.MaKP, true);
                    frm.btn_huy.Visible = false;
                    frm.TopLevel = false;
                    frm.AutoScroll = true;
                    frm.Dock = DockStyle.Fill;
                    xtraKeDonNgoai.Controls.Add(frm);
                    foreach (Control control in xtraKeDonNgoai.Controls)
                    {

                        if (control != frm) { control.Hide(); }
                        else { control.Show(); }
                        frm.Show();
                    }
                }
                else
                {
                    frm_kedon frm = new frm_kedon(_int_maBN, -1, DungChung.Bien.MaKP, false);
                    frm.btn_huy.Visible = false;
                    frm.TopLevel = false;
                    frm.AutoScroll = true;
                    frm.Dock = DockStyle.Fill;
                    xtraKeDonNgoai.Controls.Add(frm);
                    foreach (Control control in xtraKeDonNgoai.Controls)
                    {

                        if (control != frm) { control.Hide(); }
                        else { control.Show(); }
                        frm.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
            }
        }
        private void xtraNgoaiTru_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SetTTTab();
            if (xtraNgoaiTru.SelectedTabPage == xtraKeDonNgoai)
            {
                mnXoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                mnLuu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                mnXoaDon.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                mnInDon.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                mnInPhieuKCB.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnPhieuPhatThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bbtnTaoBenhAnNT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                KeDonNgoai_Load();
            }
            else
            {
                mnXoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                mnLuu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                mnXoaDon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                mnInDon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                mnInPhieuKCB.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnPhieuPhatThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bbtnTaoBenhAnNT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }

        private void xtraChiDinh_Click(object sender, EventArgs e)
        {
        }

        private void btnMoiKb_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            try
            {
                if (_int_maBN > 0)
                {
                    switch (TTTab)
                    {
                        case 1:
                            if (!DungChung.Ham.KTraKB(_dataContext, _int_maBN))
                            {
                                if (string.IsNullOrEmpty(txtIdkb.Text))
                                {
                                    TTLuu = 1;
                                    ResetControlKB();
                                    EnableButton(false);
                                    EnableControlKB(true);
                                    lupKhoaKhamkb.Enabled = false;
                                    xtraKhamBenh.Text = "Khám bệnh*";
                                    DungChung.Bien.CoTheChuyen = false;
                                    lupKhoaKhamkb.EditValue = DungChung.Bien.MaKP;
                                    lupNguoiKhamkb.EditValue = DungChung.Bien.MaCB;
                                    lupChanDoanKb.Focus();

                                }
                                else
                                {
                                    MessageBox.Show("Bệnh nhân này đã được khám bệnh!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Bệnh nhân này đã được khám bệnh!");
                            }
                            break;
                        case 2:
                            if (!DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                            {
                                var _ktKb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                                if (_ktKb.Count > 0)
                                {
                                    var _ktcv = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).ToList();
                                    if (_ktcv.Count > 0)
                                    {
                                        if (_ktcv.First().NoiTru == 2 || _ktcv.First().NoiTru == 1)
                                        {
                                            MessageBox.Show("BN đã chuyển viện hoặc nhập viện, bạn không thể tạo đơn!");
                                        }
                                        else
                                        {
                                            var ktdt = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1 && p.KieuDon == -1).ToList();
                                            if (ktdt.Count > 0)
                                            {
                                                MessageBox.Show("BN đã có đơn, bạn không thể tạo thêm");
                                            }
                                            else
                                            {
                                                TTLuu = 1;
                                                EnableButton(false);
                                                EnableControlKD(true);
                                                xtraLS_KCB.Text = "Kê đơn*";
                                                lupKhoXuat.Focus();
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Bệnh nhân chưa được khám bệnh \n Bạn không thể kê đơn");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Bệnh nhân đã thanh toán, không thể tạo đơn mới");
                            }
                            break;
                        case 3:
                            if (!DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                            {
                                if (DungChung.Ham.KTraKB(_dataContext, _int_maBN))
                                {
                                    var ktdv = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                    if (ktdv.Count > 0)
                                    {
                                        TTLuu = 2;// là trạng thái sửa
                                    }
                                    else
                                    {
                                        TTLuu = 1;
                                    }
                                    EnableButton(false);
                                    grvchiDinh.OptionsBehavior.Editable = true;
                                    xtraChiDinh.Text = "Dịch vụ*";
                                    grvchiDinh.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("Bệnh nhân chưa được khám bệnh, không thể tạo mới");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Bệnh nhân đã thanh toán, không thể tạo mới");
                            }
                            break;
                        case 4:
                            if (!DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                            {
                                if (DungChung.Ham.KTraKB(_dataContext, _int_maBN))
                                {
                                    var ktdv = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                    if (ktdv.Count > 0)
                                    {
                                        TTLuu = 2;// là trạng thái sửa
                                    }
                                    else
                                    {
                                        TTLuu = 1;
                                    }
                                    EnableButton(false);
                                    grvDichVuCS2.OptionsBehavior.Editable = true;
                                    xtabDichVuCS2.Text = "Dịch vụ*";
                                    grvDichVuCS2.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("Bệnh nhân chưa được khám bệnh, không thể tạo mới");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Bệnh nhân đã thanh toán, không thể tạo mới");
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("không có bệnh nhân!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mới" + ex.Message);
            }
        }


        private void xtraChiDinh_Leave(object sender, EventArgs e)
        {

        }

        private void xtraNgoaiTru_Leave(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");
        }

        private void xtraNgoaiTru_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSuaKb_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            try
            {
                if (!DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                {
                    var ktdt = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).ToList();
                    switch (TTTab)
                    {
                        case 1:
                            TTLuu = 2;
                            var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                            if (ktkb.Count <= 0)
                            {
                                MessageBox.Show("Không có khám bệnh");
                            }
                            else
                            {
                                if (!DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                                {
                                    EnableButton(false);
                                    EnableControlKB(true);
                                    xtraKhamBenh.Text = "Khám bệnh*";
                                    DungChung.Bien.CoTheChuyen = false;
                                }
                                else
                                {
                                    MessageBox.Show("Bệnh nhân đã ra viện, bạn không được sửa");
                                }
                            }
                            break;
                        case 2:
                            TTLuu = 2;

                            if (ktdt.Where(p => p.PLDV == 1 && p.KieuDon == -1).ToList().Count <= 0)
                            {
                                MessageBox.Show("Không có đơn thuốc");
                            }
                            else
                            {
                                EnableButton(false);
                                EnableControlKD(true);
                                colXoactdt.ColumnEdit.ReadOnly = false;
                                xtraLS_KCB.Text = "Đơn thuốc*";
                            }
                            break;
                        case 3:
                            TTLuu = 2;
                            if (ktdt.Where(p => p.PLDV == 2).ToList().Count <= 0)
                            {
                                MessageBox.Show("Không có dịch vụ để sửa");
                            }
                            else
                            {
                                EnableButton(false);
                                xtraChiDinh.Text = "Dịch vụ*";
                                grvchiDinh.OptionsBehavior.Editable = true;
                            }
                            break;
                        case 4:
                            TTLuu = 2;
                            if (ktdt.Where(p => p.PLDV == 2).ToList().Count <= 0)
                            {
                                MessageBox.Show("Không có dịch vụ để sửa");
                            }
                            else
                            {
                                EnableButton(false);
                                xtabDichVuCS2.Text = "Dịch vụ CS2*";
                                grvDichVuCS2.OptionsBehavior.Editable = true;
                            }
                            break;
                    }
                }
                else { MessageBox.Show("Bệnh nhân đã ra viện, bạn không được sửa"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi! không sử được" + ex.Message);
            }
        }
        private void btnXoaKb_Click(object sender, EventArgs e)
        {


        }


        private void lupChanDoanKb_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void lupMaICDkb_EditValueChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(lupMaICDkb.Text))
            {
                if (lupMaICDkb.EditValue.ToString() == "0")
                {
                    lupChanDoanKb.EditValue = "";
                    lupMaICDkb.EditValue = "";
                }
                else
                {
                    lupChanDoanKb.EditValue = lICD.Where(p => p.MaICD == lupMaICDkb.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                    txtBenhChinh.EditValue = lICD.Where(p => p.MaICD == lupMaICDkb.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                }


            }
            else
            {
                lupChanDoanKb.EditValue = "";
                txtBenhChinh.EditValue = "";
            }
            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && lupMaICDkb.EditValue != null && lupMaICDkb.EditValue != "")
            {
                btnPackageICD1.Enabled = true;
            }
            else
                btnPackageICD1.Enabled = false;
        }

        private void xtraNgoaiTru_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            if (xtraKhamBenh.Text.Contains("*"))
            {
                DialogResult result;
                result = MessageBox.Show("Bạn chưa lưu khám bệnh - Kê đơn, Bạn có muốn lưu không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    SetTTTab();
                }
                else
                {
                    mnLuu_ItemClick(null, null);
                }
            }
            if (xtraLS_KCB.Text.Contains("*"))
            {
                DialogResult result;
                result = MessageBox.Show("Bạn chưa lưu đơn thuốc, Bạn có muốn thoát không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    SetTTTab();

                }
            }
            if (xtraChiDinh.Text.Contains("*"))
            {
                DialogResult result;
                result = MessageBox.Show("Bạn chưa lưu dịch vụ, Bạn có muốn lưu không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    SetTTTab();
                }
                else
                {
                    mnLuu_ItemClick(null, null);
                }
            }
            if (xtabDichVuCS2.Text.Contains("*"))
            {
                DialogResult result;
                result = MessageBox.Show("Bạn chưa lưu dịch vụ, Bạn có muốn lưu không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    SetTTTab();
                }
                else
                {
                    mnLuu_ItemClick(null, null);
                }
            }
        }


        string _errMessage = "";
        private void grvDonThuocct_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            _errMessage = "";

            object valTrongBH = grvDonThuocct.GetRowCellValue(e.RowHandle, coltrongBH);
            if (valTrongBH != null && !string.IsNullOrEmpty(valTrongBH.ToString()))
            {
                if (Convert.ToInt32(valTrongBH) < 0)
                {
                    e.Valid = false;
                    _errMessage = "Thuốc thiết lập danh mục trong ngoài bảo hiểm chưa đúng ?";
                    return;
                }
            }

        }

        private void grvDonThuocct_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString() != "")
            {
                if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString() == "0")
                {
                    grvDonThuocct.SelectCell(e.RowHandle, colSoLuong);
                }
            }
            else if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null)
            {
                if (grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() == "0" || (grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() == ""))
                {
                    grvDonThuocct.SelectCell(e.RowHandle, colDonGia);
                }
            }
        }

        private void grvBNhankb_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

            if (grvBNhankb.GetRowCellValue(e.RowHandle, UuTien) != null && grvBNhankb.GetRowCellValue(e.RowHandle, UuTien).ToString() == "True")
            {
                e.Appearance.ForeColor = Color.Green;
            }
            else if (grvBNhankb.GetRowCellValue(e.RowHandle, colStatus) != null)
            {
                string status = grvBNhankb.GetRowCellValue(e.RowHandle, colStatus).ToString();
                if (status == "0")//chưa khám
                    e.Appearance.ForeColor = Color.Black;

                //dung280516
                else if (status == "4") // mới chỉ định CLS nhưng chưa có kq đầy đủ
                    e.Appearance.ForeColor = Color.DimGray;
                else if (status == "5")// đã có KQ CLS
                    e.Appearance.ForeColor = Color.Maroon;
                else if (status == "1")
                    e.Appearance.ForeColor = Color.Blue;

            }

        }

        private string _getDDung(int madv)
        {
            try
            {
                string dd = "";
                var ddung = _lDichvu.Where(p => p.MaDV == madv).Select(p => p.DuongD).ToList();
                if (ddung.Count > 0)
                {
                    if (ddung.First() != null)
                        dd = ddung.First().ToString() + " ngày ";
                    else
                        dd = "";
                }
                return dd;
            }
            catch (Exception)
            {
                MessageBox.Show("Thuốc chưa có đường dùng");
                return "";
            }
        }

        bool checkShowThuocThang = false;
        bool isUpdate = true;
        List<MedicineInventoryModel> lupMedicine = new List<MedicineInventoryModel>();
        MedicineInventoryModel selectedMedicine = new MedicineInventoryModel();

        private void grvDonThuocct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e != null && e.Column != null && e.Column.FieldName != null && sender is GridView gridView && gridView.ActiveEditor != null)
            {

                var oldValue = gridView.ActiveEditor.OldEditValue;
                var newValue = gridView.ActiveEditor.EditValue;

                int oldIDThuoc = 0;
                string oldSoLo = "";
                DateTime oldHanDung = new DateTime();
                double oldSL = 0;
                double oldDonGia = 0;

                int newIDThuoc = 0;
                string newSoLo = "";
                DateTime newHanDung = new DateTime();
                double newSL = 0;
                double newDonGia = 0;
                int maBN = 0;

                //var currentRow = (DThuocctModel)gridView.GetFocusedRow();
                xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
                mnLuu.Enabled = true;

                switch (e.Column.FieldName)
                {
                    case nameof(DThuocctModel.IDThuoc):
                        {
                            isUpdate = true;

                            newIDThuoc = Convert.ToInt32(newValue);

                            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                                maBN = Convert.ToInt32(txtMaBNhan.Text);
                            int trongDM = _medicinesProvider.isTrongDMBHYT(maBN);
                            DateTime ngayKe = grvDonThuocct.GetFocusedRowCellValue(colNgayKe) != null ? Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colNgayKe)) : new DateTime();
                            int idDon = _medicinesProvider.GetIDDonByMaBNInFrmNgoaiTru(maBN);

                            //lấy chi tiết đơn thuốc
                            lupMedicine = ((List<MedicineInventoryModel>)lupIDThuoc.DataSource);
                            selectedMedicine = lupMedicine.FirstOrDefault(p => p.IDThuoc == newIDThuoc);

                            if (oldValue != null) // trường hợp sửa tên thuốc
                            {
                                // SL trc khi thay đổi MaDV
                                if (grvDonThuocct.GetFocusedRowCellValue(colSoLuongct) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuongct).ToString() != "")
                                    oldSL = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colSoLuongct));
                                oldDonGia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia));

                                oldIDThuoc = Convert.ToInt32(oldValue);
                                if (grvDonThuocct.GetFocusedRowCellValue(colSoLo) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLo).ToString() != "")
                                    oldSoLo = Convert.ToString(grvDonThuocct.GetFocusedRowCellValue(colSoLo));
                                if (grvDonThuocct.GetFocusedRowCellValue(colHanDung) != null && grvDonThuocct.GetFocusedRowCellValue(colHanDung).ToString() != "")
                                    oldHanDung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));

                                _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, oldSL, 0, oldIDThuoc, 0, oldDonGia, 0, ppxuat);
                            }

                            if (selectedMedicine != null)
                            {
                                isUpdate = false;

                                grvDonThuocct.SetFocusedRowCellValue(colSoLo, selectedMedicine.SoLo);
                                grvDonThuocct.SetFocusedRowCellValue(colHanDung, selectedMedicine.HanDung);
                                grvDonThuocct.SetFocusedRowCellValue(colDonGia, selectedMedicine.DonGia);
                                grvDonThuocct.SetFocusedRowCellValue(colSoLuongct, 0);
                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, 0);
                                grvDonThuocct.SetFocusedRowCellValue(colDonVi, selectedMedicine.DonVi);
                                grvDonThuocct.SetFocusedRowCellValue(colDuongD, _getDDung(_medicinesProvider.GetMaDVbyIDThuoc(newIDThuoc)));
                                grvDonThuocct.SetFocusedRowCellValue(colMoilan, " lần, mỗi lần ");
                                grvDonThuocct.SetFocusedRowCellValue(colDViUong, selectedMedicine.DonVi.Trim());
                                grvDonThuocct.SetFocusedRowCellValue(colTyLeBHTT_dt, selectedMedicine.BHTT);
                                grvDonThuocct.SetFocusedRowCellValue(coltrongBH, trongDM == 1 ? selectedMedicine.TrongDM : 0);
                                grvDonThuocct.SetFocusedRowCellValue(colGhiChu, "");
                                grvDonThuocct.SetFocusedRowCellValue(colSoLan, 1);
                                grvDonThuocct.SetFocusedRowCellValue(colSoLuongdung, 1);

                                isUpdate = true;
                            }
                        }
                        break;
                    case nameof(DThuocctModel.SoLuongct):
                        {

                            #region theo so lo han dung
                            // Sửa số lượng thì trừ số lượng tồn trong danh mục tồn thuốc
                            var idThuocObj = gridView.GetRowCellValue(e.RowHandle, colIDThuoc);
                            var soLoObj = gridView.GetRowCellValue(e.RowHandle, colSoLo);
                            var hanDungObj = gridView.GetRowCellValue(e.RowHandle, colHanDung);
                            var donGiaObj = gridView.GetRowCellValue(e.RowHandle, colDonGia);

                            if (gridView.ActiveEditor.OldEditValue != null && gridView.ActiveEditor.EditValue != null && idThuocObj != null && soLoObj != null)
                            {
                                newIDThuoc = Convert.ToInt32(idThuocObj);
                                oldSL = Convert.ToDouble(gridView.ActiveEditor.OldEditValue);
                                newSL = Convert.ToDouble(gridView.ActiveEditor.EditValue);
                                newDonGia = Convert.ToDouble(donGiaObj);

                                lupMedicine = ((List<MedicineInventoryModel>)lupIDThuoc.DataSource);
                                selectedMedicine = lupMedicine.FirstOrDefault(p => p.IDThuoc == newIDThuoc);

                                if (isUpdate && selectedMedicine != null)
                                {
                                    if (newSL <= 0)
                                    {
                                        isUpdate = false;
                                        grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuongct, oldSL);
                                        grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                                        MessageBox.Show("Số lượng phải lớn hơn 0", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        isUpdate = true;
                                    }
                                    else if (newSL > (selectedMedicine.TonHienTai + oldSL))
                                    {
                                        isUpdate = false;
                                        grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuongct, oldSL);
                                        grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                                        MessageBox.Show("Số lượng thuốc không đủ", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        isUpdate = true;
                                    }
                                    else
                                    {
                                        _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, oldSL, newSL, newIDThuoc, newIDThuoc, newDonGia, newDonGia, ppxuat);
                                        this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + selectedMedicine.TonHienTai;

                                        var soLuong = Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colSoLuongct));
                                        var donGia = Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colDonGia));
                                        int tylett = 1;
                                        if (grvDonThuocct.GetRowCellValue(e.RowHandle, colTyLeBHTT_dt) != null)
                                            tylett = Convert.ToInt32(grvDonThuocct.GetRowCellValue(e.RowHandle, colTyLeBHTT_dt));
                                        double thanhTien = Math.Round((double)(soLuong * donGia * tylett / 100), 2);

                                        grvDonThuocct.SetRowCellValue(e.RowHandle, colThanhTien, thanhTien);
                                        grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, newSL);
                                    }
                                }
                            }
                            #endregion theo so lo han dung
                        }
                        break;
                    case nameof(DThuocctModel.NgayNhap):
                        DateTime ngayNhap = new DateTime(2100, 12, 31, 5, 10, 20, DateTimeKind.Utc);
                        DateTime ngayKham = Convert.ToDateTime(dtNgayKhamkb.EditValue);
                        if (grvDonThuocct.GetRowCellValue(e.RowHandle, colNgayKe) != null && grvDonThuocct.GetRowCellValue(e.RowHandle, colNgayKe).ToString() != "")
                            ngayNhap = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(e.RowHandle, colNgayKe));

                        if (ngayNhap < ngayKham)
                        {
                            MessageBox.Show("Ngày kê không được nhỏ hơn ngày khám", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                }
            }
        }
        private MedicineInventoryModel GetCurrentMedicine(int idThuoc)
        {

            lupMedicine = ((List<MedicineInventoryModel>)lupIDThuoc.DataSource);
            return lupMedicine.FirstOrDefault(p => p.IDThuoc == idThuoc);
        }

        /// <summary>
        /// Gộp lại những giá giống nhau
        /// </summary>
        /// <param name="_Rlist"></param>
        /// <returns></returns>
        public List<Ham.giaSoLoHSD> RefreshList(List<Ham.giaSoLoHSD> _Rlist)
        {
            List<Ham.giaSoLoHSD> _FreshList = new List<Ham.giaSoLoHSD>();
            Ham.giaSoLoHSD tem = new Ham.giaSoLoHSD();

            foreach (var a in _Rlist)
            {
                if (_FreshList.Count == 0)
                    _FreshList.Add(a);
                else
                {
                    tem = _FreshList.Last();
                    if (a.Gia == tem.Gia && a.SoLo == tem.SoLo && a.HanDung == tem.HanDung)
                        tem.SoLuong = tem.SoLuong + a.SoLuong;
                    else
                        _FreshList.Add(a);
                }
            }

            return _FreshList;
        }

        private void grvDonThuocct_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);

            if (grvDonThuocct.RowCount <= 1)
                if (grvBNhankb.GetFocusedRowCellValue(colDTuong) != null)
                {
                    string tencd = "";

                    tencd = DungChung.Ham.KTChiDinh(_dataContext, _int_maBN);

                    if (!string.IsNullOrEmpty(tencd))
                    {
                        DialogResult _result = MessageBox.Show("Bệnh nhân có các chỉ định CLS chưa được thực hiện:\n " + tencd, "Kê đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.No)
                        {
                            grvDonThuocct.DeleteSelectedRows();
                        }

                    }
                    if (grvBNhankb.GetFocusedRowCellValue(colDTuong).ToString() == "BHYT")
                    {

                    }
                    else
                    {
                        if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "24297")
                            MessageBox.Show("Lưu ý: Bệnh nhân không có thẻ BHYT!");
                    }
                }
            bool kt = ktCellChange;
            ktCellChange = false;
            grvDonThuocct.SetRowCellValue(e.RowHandle, colThanhTien, 0);
            grvDonThuocct.SetRowCellValue(e.RowHandle, colTTLuu, 1);
            grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLan, "");
            grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuongdung, "");
            grvDonThuocct.SetRowCellValue(e.RowHandle, colStatusct, 0);
            grvDonThuocct.SetRowCellValue(e.RowHandle, colNgayKe, DateTime.Now);
            ktCellChange = kt;
            string MaICD = "";
            //int j = 0;
            var cd = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
            if (cd.Count > 0)
            {
                MaICD = cd.First().MaICD;
            }
            if (string.IsNullOrEmpty(MaICD) && DungChung.Bien.MaBV == "30009")
            {
                if (string.IsNullOrEmpty(lupMaICDkb.Text))
                {
                    MessageBox.Show("Bạn chưa nhập Mã ICD chính cho bệnh nhân, nên không thể kê đơn");
                    ktCellChange = false;
                    grvDonThuocct.SetRowCellValue(e.RowHandle, colIDThuoc, "");
                    ktCellChange = kt;
                }
            }
            if (radGiaiQuyet.SelectedIndex == 3)
            {
                MessageBox.Show("Bệnh nhân đã được chuyển PK và đang chờ khám tại PK khác\n Bạn không thể kê đơn!");
                ktCellChange = false;
                grvDonThuocct.SetRowCellValue(e.RowHandle, colIDThuoc, "");
                ktCellChange = kt;
            }
        }

        //private void grvDonThuocct_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        //{
        //    //Tạo số thự tự
        //    //if (e.Column == colSoTTkd)
        //    //{
        //    //    e.DisplayText = Convert.ToString(e.RowHandle + 1);
        //    //}
        //}

        private void grvchiDinh_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int _int_maBN = 0;
            int rs = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            var bn = _dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == _int_maBN);
            grvchiDinh.SetFocusedRowCellValue(colNgayNhap, System.DateTime.Now);// kiểm tra lại
            grvchiDinh.SetFocusedRowCellValue(ColThanhTiencd, 0);
            grvchiDinh.SetFocusedRowCellValue(colTTLuucd, 1);
            grvchiDinh.SetFocusedRowCellValue(colKPhongdv, DungChung.Bien.MaKP);
            string MaCB = "";
            if (lupNguoiKhamkb.EditValue != null)
                MaCB = Convert.ToString(lupNguoiKhamkb.EditValue);
            if (MaCB != "")
                grvchiDinh.SetFocusedRowCellValue(colBSTH, MaCB);
            if (DungChung.Bien.MaBV == "20001" && bn != null && bn.DTuong == "KMP")
            {
                grvchiDinh.SetFocusedRowCellValue(colTrongBHdv, 2);
            }

        }

        private void grvchiDinh_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            xtraChiDinh.Text = "Dịch vụ*";
            mnLuu.Enabled = true;
            int _int_maBN = 0;
            int rs = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            var bn = _dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == _int_maBN);
            switch (e.Column.Name)
            {
                case "colNgayNhap":
                    if (grvchiDinh.GetFocusedRowCellValue(colNgayNhap) == null)
                    {
                        MessageBox.Show("Bạn chưa nhập ngày tháng!");
                    }
                    break;
                case "colTrongBHdv":
                    if (grvchiDinh.GetFocusedRowCellValue(colTrongBHdv) != null)
                    {
                        int ma = 0;
                        if (grvchiDinh.GetFocusedRowCellValue(colMaDVcd) != null)
                        {
                            ma = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colMaDVcd));
                        }
                        int trongBH = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colTrongBHdv));
                        if (trongBH == 3)
                        {
                            MessageBox.Show("Chi phí phụ thu không thể tự nhập");
                            if (grvchiDinh.GetFocusedRowCellValue(colIDDonct) != null)
                            {
                                int _iddonct = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colIDDonct));
                                var dtcht = _dataContext.DThuoccts.Where(p => p.IDDonct == _iddonct).FirstOrDefault();
                                if (dtcht != null)
                                {
                                    grvchiDinh.SetFocusedRowCellValue(colTrongBHdv, dtcht.TrongBH);
                                }
                            }
                            else
                            {
                                grvchiDinh.SetFocusedRowCellValue(colTrongBHdv, 0);
                            }
                        }
                        if (grvchiDinh.GetFocusedRowCellValue(colIDDonct) != null)
                        {
                            int _iddonct = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colIDDonct));
                            var dtcht = _dataContext.DThuoccts.Where(p => p.IDDonct == _iddonct).FirstOrDefault();
                            if (dtcht != null)
                            {
                                if (dtcht.TrongBH == 3)
                                {
                                    MessageBox.Show("chi phí phụ thu, không thể sửa");
                                }
                            }
                        }
                        double dongia = Ham._getGiaDM(_dataContext, ma, trongBH, string.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), dtNgayKhamkb.DateTime);
                        grvchiDinh.SetFocusedRowCellValue(colDonGiacd, dongia);
                        //set thành tiền
                        if (grvchiDinh.GetFocusedRowCellValue(colSoLuongcd) != null && grvchiDinh.GetFocusedRowCellValue(colSoLuongcd).ToString() != "")
                        {
                            double sl = 0;
                            sl = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colSoLuongcd).ToString());
                            if (sl < 0)
                            {
                                MessageBox.Show("Số lượng phải >0");
                                grvchiDinh.FocusedColumn = grvchiDinh.VisibleColumns[3];
                            }
                            else
                            {
                                double dg = 0;
                                if (grvchiDinh.GetFocusedRowCellValue(colDonGiacd) != null && grvchiDinh.GetFocusedRowCellValue(colDonGiacd).ToString() != "")
                                    dg = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colDonGiacd).ToString());

                                if (trongBH == 1)
                                {
                                    if (grvchiDinh.GetFocusedRowCellValue(colTyLeBHTT) != null)
                                    {
                                        double tltt = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colTyLeBHTT));
                                        grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg * tltt / 100);
                                    }
                                    else
                                        grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg);
                                }
                                else
                                    grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg);
                            }
                        }
                    }
                    break;
                case "colMaDVcd":
                    int _trongBH = 1;

                    if (grvchiDinh.GetFocusedRowCellValue(colMaDVcd) != null)
                    {
                        int ma = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colMaDVcd));
                        if (DungChung.Bien.MaBV == "27183")
                        {
                            var nhom = _lDVhienthi.Where(p => p.MaDV == ma).FirstOrDefault();
                            if (nhom != null && nhom.IDNhom == 8)
                            {
                                MessageBox.Show("Bạn không thể thực hiện trực tiếp dịch vụ nhóm phẫu thuật thủ thuật");
                                grvchiDinh.SetFocusedRowCellValue(colMaDVcd, -1);
                                return;
                            }
                        }
                        int dvtrung = 0;
                        for (int i = 0; i < grvchiDinh.DataRowCount; i++)
                        {
                            int madv = -10;
                            if (grvchiDinh.GetRowCellValue(i, colMaDVcd) != null && grvchiDinh.GetRowCellValue(i, colMaDVcd).ToString() != "")
                                madv = Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colMaDVcd));
                            if (ma == madv)
                            {
                                if (DungChung.Bien.MaBV != "30003" && i != grvchiDinh.FocusedRowHandle)
                                {
                                    dvtrung++;

                                }
                            }
                        }
                        if (dvtrung > 0)
                        {
                            DialogResult _result = MessageBox.Show(grvchiDinh.GetFocusedRowCellDisplayText(colMaDVcd) + " đã được chỉ định " + dvtrung + " lần, bạn vẫn muốn thêm?", "thêm dịch vụ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                            {
                                grvchiDinh.DeleteSelectedRows();
                                return;
                            }
                        }
                        double tyleBHTT = 100;
                        var trongBH = _lDichvu.Where(p => p.MaDV == ma).ToList();
                        string maQD = "";
                        if (trongBH.Count > 0)
                        {
                            tyleBHTT = trongBH.First().BHTT ?? 100;
                            _trongBH = trongBH.First().TrongDM.Value;
                            maQD = trongBH.First().MaQD;
                        }
                        int iddtbn = -10;
                        if (grvBNhankb.GetFocusedRowCellValue(colIDDTBN) != null)
                            iddtbn = Convert.ToInt32(grvBNhankb.GetFocusedRowCellValue(colIDDTBN));
                        if (DungChung.Bien._idDTBHYT != iddtbn && _trongBH == 1)
                            _trongBH = 0;
                        if (DungChung.Bien.MaBV == "20001" && bn != null && bn.DTuong == "KMP")
                        {
                            _trongBH = 2;
                        }
                        grvchiDinh.SetFocusedRowCellValue(colTrongBHdv, _trongBH);
                        if (DungChung.Bien.MaBV == "27001")
                            grvchiDinh.SetFocusedRowCellValue(colSoLuongcd, 1);
                        else
                            grvchiDinh.SetFocusedRowCellValue(colSoLuongcd, 0);
                        grvchiDinh.SetFocusedRowCellValue(colDonGiacd, Ham._getGiaDM(_dataContext, ma, _trongBH, string.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), dtNgayKhamkb.DateTime));
                        grvchiDinh.SetFocusedRowCellValue(colDonVicd, Ham._getDonVi(_dataContext, ma));
                        grvchiDinh.SetFocusedRowCellValue(colTyLeBHTT, tyleBHTT);
                        grvchiDinh.SetFocusedRowCellValue(colMaQD, maQD);
                        if (grvchiDinh.GetFocusedRowCellValue(colSoLuongcd) != null && grvchiDinh.GetFocusedRowCellValue(colSoLuongcd).ToString() != "")
                        {
                            double sl = 0;
                            sl = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colSoLuongcd).ToString());
                            if (sl < 0)
                            {
                                MessageBox.Show("Số lượng phải >0");
                                grvchiDinh.FocusedColumn = grvchiDinh.VisibleColumns[3];
                            }
                            else
                            {
                                double dg = 0;
                                if (grvchiDinh.GetFocusedRowCellValue(colDonGiacd) != null && grvchiDinh.GetFocusedRowCellValue(colDonGiacd).ToString() != "")
                                    dg = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colDonGiacd).ToString());
                                grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg);
                            }
                        }
                    }
                    break;
                case "colSoLuongcd":
                    if (grvchiDinh.GetFocusedRowCellValue(colSoLuongcd) != null && grvchiDinh.GetFocusedRowCellValue(colSoLuongcd).ToString() != "")
                    {

                        double sl = 0, tyleBHTT = 100;
                        if (grvchiDinh.GetFocusedRowCellValue(colTyLeBHTT) != null && grvchiDinh.GetFocusedRowCellValue(colTyLeBHTT).ToString() != "")
                            tyleBHTT = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colTyLeBHTT));
                        sl = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colSoLuongcd).ToString());
                        if (sl < 0)
                        {
                            MessageBox.Show("Số lượng phải >0");
                            grvchiDinh.FocusedColumn = grvchiDinh.VisibleColumns[3];
                        }
                        else
                        {
                            double dg = 0;
                            if (grvchiDinh.GetFocusedRowCellValue(colDonGiacd) != null && grvchiDinh.GetFocusedRowCellValue(colDonGiacd).ToString() != "")
                                dg = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colDonGiacd).ToString());

                            //if (_ktraBV_TyLeTT)
                            //{
                            //    if (grvchiDinh.GetFocusedRowCellValue(colMaDVcd) != null)
                            //    {
                            //        int ma = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colMaDVcd));
                            //        var qdv = _lDichvu.Where(p => p.MaDV == ma).FirstOrDefault();
                            //        if(qdv != null && qdv.IDNhom != 13 && qdv.IDNhom != 15)
                            //            grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg );
                            //        else
                            //        {
                            //            grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg * tyleBHTT / 100);
                            //        }

                            //    }
                            //}
                            // else
                            grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg * tyleBHTT / 100);
                        }
                    }
                    break;
                case "colTyLeBHTT":
                    if (grvchiDinh.GetFocusedRowCellValue(colSoLuongcd) != null && grvchiDinh.GetFocusedRowCellValue(colSoLuongcd).ToString() != "")
                    {

                        double sl = 0, tyleBHTT = 100;
                        if (grvchiDinh.GetFocusedRowCellValue(colTyLeBHTT) != null && grvchiDinh.GetFocusedRowCellValue(colTyLeBHTT).ToString() != "")
                            tyleBHTT = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colTyLeBHTT));
                        sl = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colSoLuongcd).ToString());
                        if (sl < 0)
                        {
                            MessageBox.Show("Số lượng phải >0");
                            grvchiDinh.FocusedColumn = grvchiDinh.VisibleColumns[3];
                        }
                        else
                        {
                            double dg = 0;
                            if (grvchiDinh.GetFocusedRowCellValue(colDonGiacd) != null && grvchiDinh.GetFocusedRowCellValue(colDonGiacd).ToString() != "")
                                dg = Convert.ToDouble(grvchiDinh.GetFocusedRowCellValue(colDonGiacd).ToString());
                            //grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg * tyleBHTT / 100);
                            //if (_ktraBV_TyLeTT)
                            //{
                            //    if (grvchiDinh.GetFocusedRowCellValue(colMaDVcd) != null)
                            //    {
                            //        int ma = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colMaDVcd));
                            //        var qdv = _lDichvu.Where(p => p.MaDV == ma).FirstOrDefault();
                            //        if (qdv != null && qdv.IDNhom != 13 && qdv.IDNhom != 15)
                            //            grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg );
                            //        else
                            //        {
                            //            grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg * tyleBHTT / 100);
                            //        }

                            //    }
                            //}
                            //else
                            grvchiDinh.SetFocusedRowCellValue(colThanhTien, sl * dg * tyleBHTT / 100);
                        }
                    }
                    break;
                case "colXHH":
                    if (grvchiDinh.GetFocusedRowCellValue(colXHH) != null && grvchiDinh.GetFocusedRowCellValue(colXHH).ToString() == "True")
                    {
                        grvchiDinh.SetFocusedRowCellValue(colTrongBHdv, 0);
                    }
                    break;
                    //case "colBSTH":
                    //    string MaCB = "";
                    //    if (lupNguoiKhamkb.EditValue != null)
                    //        MaCB = Convert.ToString(lupNguoiKhamkb.EditValue);
                    //    if (MaCB != "")
                    //        lupBSTH.ValueMember = MaCB;
                    //    break;
            }
        }

        private void mnInDon_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            int _idDon = 0;
            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _idDon = Convert.ToInt32(txtIdDonThuoc.Text);
            int id = 0;
            if (_int_maBN > 0)
            {
                if (mnInDon.Caption == "In đơn")
                {
                    int makp = 0;
                    if (DungChung.Bien.MaBV == "27001" && lupKhoaKhamkb.EditValue != null)
                        makp = Convert.ToInt32(lupKhoaKhamkb.EditValue);

                    DungChung.Ham.InDon(_idDon, _int_maBN, makp, id);
                }//
                else
                {
                    frmIn frm = new frmIn();
                    BaoCao.Rep_PhieuTHCD rep = new BaoCao.Rep_PhieuTHCD();

                    rep.Chuandoan.Value = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                    var bn2 = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                               select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe }).ToList();
                    if (bn2.Count > 0)
                    {
                        rep.Diachi.Value = bn2.First().DChi;
                        rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_dataContext, _int_maBN, DungChung.Bien.formatAge);
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

                    #region ADO
                    DataTable dtTble = new DataTable();
                    try
                    {

                        string strSQL = "sp_KB_XQuang";
                        string[] strpara = new string[] { "@maBNhan", "@maBV" };
                        object[] oValue = new object[] { _int_maBN, DungChung.Bien.MaBV };
                        SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar };

                        connect.Connect();

                        dtTble = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    #endregion

                    if (dtTble.Rows.Count > 0)
                    {
                        DateTime _dt = System.DateTime.Now;
                        if (!string.IsNullOrEmpty(dtTble.Rows[0]["NgayThang"].ToString()))
                            _dt = Convert.ToDateTime(dtTble.Rows[0]["NgayThang"]);// XQ.First().NgayThang.Value;
                        rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, 2);
                        rep.BSCD.Value = dtTble.Rows[0]["TenCB"];
                        rep.TenKP.Value = dtTble.Rows[0]["TenKP"];
                        rep.DataSource = dtTble;
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Không có chỉ định nào!");
                    }
                }
            }//
            else
            {
                MessageBox.Show("Không có BN để in đơn");
            }
        }
        // List<BenhNhan> _lTKbn = new List<BenhNhan>();

        //void TimKiem2()
        //{
        //    process = true;
        //    xtraNgoaiTru.Enabled = false;
        //    string _tk = "";
        //    int _int_maBN = 0;
        //    if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Nhập tên|Mã số|Số thẻ BN")
        //    {
        //        _tk = txtTimKiem.Text.ToLower();
        //        int rs;

        //        if (Int32.TryParse(txtTimKiem.Text, out rs))
        //            _int_maBN = Convert.ToInt32(txtTimKiem.Text);
        //    }
        //    grcBNhankb.DataSource = null;
        //    grcBNhankb.DataSource = _lTKbn.Where(p => p.TenBNhan.ToLower().Contains(_tk) && (_int_maBN == 0 ? true : p.MaBNhan == _int_maBN)).ToList();
        //    process = false;

        //}
        void TimKiemcheck()
        {
            if (!load)
                return;
            xtraNgoaiTru.Enabled = false;
            process = true;
            grcBNhankb.DataSource = null;
            //grcBenhKhac.DataSource = null;
            bool all = false;

            bool uutien = chkUuTien.Checked;
            bool chuakham = chkChuaKham.Checked;
            bool chidinhcls = chkChiDinhCLS.Checked;
            bool ketqua = chkKetQuaCLS.Checked;
            if (uutien && chuakham && chidinhcls && ketqua)
                all = true;
            if (!uutien && !chuakham && !chidinhcls && !ketqua)
                all = true;

            string _tk = "";
            int _int_maBN = 0;
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Nhập tên|Mã số|Số thẻ BN")
            {
                _tk = txtTimKiem.Text.ToLower();
                int rs;

                if (Int32.TryParse(txtTimKiem.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtTimKiem.Text);
            }
            //grcBNhankb.DataSource = (from a in _lTKbn
            //                         where (all ? true : ((uutien ? a.UuTien == true : false)
            //                            || (chuakham ? a.Status == 0 : false)
            //                                || (chidinhcls ? a.Status == 4 : false)
            //                                 || (ketqua ? a.Status == 5 : false))) && (a.TenBNhan.ToLower().Contains(_tk) || a.MaBNhan == _int_maBN)
            //                         select a).ToList();
            if (!all)
            {
                lICD = (from ICD in _licd10 select new c_ICD { MaICD = ICD.MaICD ?? "", TenICD = ICD.TenICD ?? "" }).OrderBy(p => p.TenICD).ToList();
                DataTable tbtk = new DataTable();
                tbtk = tbBenhnhan.Copy();
                List<DataRow> RowsToDelete = new List<DataRow>();
                for (int i = 0; i < tbtk.Rows.Count; i++)
                {
                    bool bolUuTien = Convert.ToBoolean(tbtk.Rows[i]["UuTien"].ToString());
                    int status = Convert.ToInt32(tbtk.Rows[i]["Status"].ToString());
                    if ((bolUuTien && uutien) || (chuakham && status == 0) || (chidinhcls && status == 4) || (ketqua && status == 5))
                    {

                    }
                    else
                        // tbtk.Rows.Remove(tbBenhnhan.Rows[i]);
                        RowsToDelete.Add(tbtk.Rows[i]);
                }

                foreach (var r in RowsToDelete)
                {
                    tbtk.Rows.Remove(r);

                }
                if (DungChung.Bien.MaBV == "14018")
                {
                    tbtk.DefaultView.Sort = "NNhap desc";
                    tbtk = tbBenhnhan.DefaultView.ToTable();
                }
                grcBNhankb.DataSource = tbtk;
                int soBNBH = (from row in tbtk.AsEnumerable() where row.Field<string>("DTuong") == "BHYT" select row).Count();
                int soBNDV = (from row in tbtk.AsEnumerable() where row.Field<string>("DTuong").ToLower() == "dịch vụ" select row).Count();
                int soBNKSK = (from row in tbtk.AsEnumerable() where row.Field<string>("DTuong").ToLower() == "ksk" select row).Count();
                int soBNKMP = (from row in tbtk.AsEnumerable() where row.Field<string>("DTuong").ToLower() == "kmp" select row).Count();
                lblTSBN.Text = "TS: " + tbtk.Rows.Count + " (" + soBNBH + " BN BHYT, " + soBNDV + " BN Dịch vụ, " + soBNKSK + " BN KSK" + (DungChung.Bien.MaBV == "20001" ? (", " + soBNKMP + " BN KMP") : "") + ")";
            }
            else
            {
                grcBNhankb.DataSource = tbBenhnhan;
                int soBNBH = (from row in tbBenhnhan.AsEnumerable() where row.Field<string>("DTuong") == "BHYT" select row).Count();
                int soBNDV = (from row in tbBenhnhan.AsEnumerable() where row.Field<string>("DTuong").ToLower() == "dịch vụ" select row).Count();
                int soBNKSK = (from row in tbBenhnhan.AsEnumerable() where row.Field<string>("DTuong").ToLower() == "ksk" select row).Count();
                int soBNKMP = (from row in tbBenhnhan.AsEnumerable() where row.Field<string>("DTuong").ToLower() == "kmp" select row).Count();
                lblTSBN.Text = "TS: " + tbBenhnhan.Rows.Count + " (" + soBNBH + " BN BHYT, " + soBNDV + " BN Dịch vụ, " + soBNKSK + " BN KSK" + (DungChung.Bien.MaBV == "20001" ? (", " + soBNKMP + " BN KMP") : "") + ")";
            }
            process = false;


        }
        private void TimKiem()
        {
            QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            if (!load)
            {
                xtraNgoaiTru.Enabled = false;
                process = true;
                _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                DateTime ngaykbtu = _dttu.AddMonths(-2);
                DateTime ngaykbden = _dtden.AddMonths(2);
                if (!string.IsNullOrEmpty(lupTimMaKP.Text))
                {
                    _makp = Convert.ToInt32(lupTimMaKP.EditValue);
                }
                string _tk = "";
                string _str_maBN = "";
                if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Nhập tên|Mã số|Số thẻ BN")
                {
                    _tk = txtTimKiem.Text.ToLower();
                    int rs;

                    if (Int32.TryParse(txtTimKiem.Text, out rs))
                        _str_maBN = txtTimKiem.Text;
                }
                string mKP = _makp.ToString();
                var kd = _lKPhong_data.Where(p => ((p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc) || (p.PLoai == ("Tủ trực") && (DungChung.Bien.MaBV == "24012" ? (p.MaKPsd != null ? p.MaKPsd.Contains(mKP) : true) : p.NhomKP == _makp))) && p.MaBVsd == DungChung.Bien.MaBV).Where(p => p.Status == 1).OrderBy(p => p.TenKP).ToList();
                if (kd.Count > 0)
                {
                    lupKhoXuat.Properties.DataSource = _medicinesProvider.GetListKhoaPhong(DungChung.Bien.MaCB, 0);
                }
                try
                {
                    string strSQL = "sp_KB_TimKiemBN";
                    string[] strpara = new string[] { "@chokham", "@ckChuyen", "@status", "@MaKP", "@MaKCB", "@maBNhan", "@tenBNhan", "@tungay", "@denngay" };
                    object[] oValue = new object[] { false, chkChuyenBN.Checked, cboTimRaVien.SelectedIndex, _makp, DungChung.Bien.MaBV, _str_maBN, _tk, _dttu, _dtden };
                    SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Bit, SqlDbType.Bit, SqlDbType.Int, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.NVarChar, SqlDbType.DateTime, SqlDbType.DateTime };

                    connect.Connect();

                    tbBenhnhan = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);
                    // if (dtTble.Rows.Count <= 0) return;

                    if (DungChung.Bien.MaBV == "14018")
                    {
                        tbBenhnhan.DefaultView.Sort = "NNhap desc";
                        tbBenhnhan = tbBenhnhan.DefaultView.ToTable();
                    }
                    grcBNhankb.DataSource = null;
                    grcBNhankb.DataSource = tbBenhnhan;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                #region tìm kiếm linq
                //if (chkChuyenBN.Checked == false)
                //{
                //switch (cboTimRaVien.SelectedIndex)
                //{
                //    case 0:// chưa thanh toán

                //        var bnhan = _dataContext.BenhNhans.Where(p => p.NNhap >= _dttu && p.NNhap <= _dtden).ToList();

                //        var _lbnkb = (from bn in bnhan.Where(p => p.Status <= 1 || p.Status == 4 || p.Status == 5).Where(p => p.NoiTru == 0)

                //                      select bn).ToList();

                //        _lTKbn = (from bn in _lbnkb
                //                  where (bn.MaKP == _makp)
                //                  where (bn.MaKCB == DungChung.Bien.MaBV)
                //                  where (bn.TenBNhan.ToLower().Contains(_tk) || bn.MaBNhan == _int_maBN)
                //                  // where (bn.bn.NNhap >= _dttu && bn.bn.NNhap <= _dtden && (bn.bn.Status <= 1 || bn.bn.Status == 4 || bn.bn.Status == 5))
                //                  group bn by new
                //                  {
                //                      MaBNhan = bn.MaBNhan,
                //                      Tuoi = bn.Tuoi,
                //                      GTinh = bn.GTinh,
                //                      DChi = bn.DChi,
                //                      DTuong = bn.DTuong,
                //                      NNhap = bn.NNhap,
                //                      SThe = bn.SThe,
                //                      MaCS = bn.MaCS,
                //                      NoiTru = bn.NoiTru,
                //                      TChung = bn.TChung,
                //                      MaKP = bn.MaKP,
                //                      Tuyen = bn.Tuyen,
                //                      CDNoiGT = bn.CDNoiGT,
                //                      CapCuu = bn.CapCuu,
                //                      NoiTinh = bn.NoiTinh,
                //                      SoTT = bn.SoTT,
                //                      Status = bn.Status,
                //                      HanBHTu = bn.HanBHTu,
                //                      HanBHDen = bn.HanBHDen,
                //                      NgaySinh = bn.NgaySinh,
                //                      ThangSinh = bn.ThangSinh,
                //                      NamSinh = bn.NamSinh,
                //                      MaBV = bn.MaBV,
                //                      ChuyenKhoa = bn.ChuyenKhoa,
                //                      TenBNhan = bn.TenBNhan,
                //                      NgoaiGio = bn.NgoaiGio,
                //                      TuyenDuoi = bn.TuyenDuoi,
                //                      SoKB = bn.SoKB,
                //                      MucHuong = bn.MucHuong,
                //                      KhuVuc = bn.KhuVuc,
                //                      NgayHM = bn.NgayHM,
                //                      LuongCS = bn.LuongCS,
                //                      UuTien = bn.UuTien,
                //                      DTNT = bn.DTNT,
                //                      MaDTuong = bn.MaDTuong,
                //                      IDPerson = bn.IDPerson,
                //                      NoThe = bn.NoThe,
                //                      IDDTBN = bn.IDDTBN,
                //                      Ma_lk = bn.Ma_lk,
                //                      MaKCB = bn.MaKCB,
                //                      Export = bn.Export,
                //                      Normal = bn.Normal,
                //                      MaCB = bn.MaCB,
                //                      SoDK = bn.SoDK
                //                  } into kq
                //                  select new BenhNhan
                //                  {
                //                      MaBNhan = kq.Key.MaBNhan,
                //                      Tuoi = kq.Key.Tuoi,
                //                      GTinh = kq.Key.GTinh,
                //                      DChi = kq.Key.DChi,
                //                      DTuong = kq.Key.DTuong,
                //                      NNhap = kq.Key.NNhap,
                //                      SThe = kq.Key.SThe,
                //                      MaCS = kq.Key.MaCS,
                //                      NoiTru = kq.Key.NoiTru,
                //                      TChung = kq.Key.TChung,
                //                      MaKP = kq.Key.MaKP,
                //                      Tuyen = kq.Key.Tuyen,
                //                      CDNoiGT = kq.Key.CDNoiGT,
                //                      CapCuu = kq.Key.CapCuu,
                //                      NoiTinh = kq.Key.NoiTinh,
                //                      SoTT = kq.Key.SoTT,
                //                      Status = kq.Key.Status,
                //                      HanBHTu = kq.Key.HanBHTu,
                //                      HanBHDen = kq.Key.HanBHDen,
                //                      NgaySinh = kq.Key.NgaySinh,
                //                      ThangSinh = kq.Key.ThangSinh,
                //                      NamSinh = kq.Key.NamSinh,
                //                      MaBV = kq.Key.MaBV,
                //                      ChuyenKhoa = kq.Key.ChuyenKhoa,
                //                      TenBNhan = kq.Key.TenBNhan,
                //                      NgoaiGio = kq.Key.NgoaiGio,
                //                      TuyenDuoi = kq.Key.TuyenDuoi,
                //                      SoKB = kq.Key.SoKB,
                //                      MucHuong = kq.Key.MucHuong,
                //                      KhuVuc = kq.Key.KhuVuc,
                //                      NgayHM = kq.Key.NgayHM,
                //                      LuongCS = kq.Key.LuongCS,
                //                      UuTien = kq.Key.UuTien,
                //                      DTNT = kq.Key.DTNT,
                //                      MaDTuong = kq.Key.MaDTuong,
                //                      IDPerson = kq.Key.IDPerson,
                //                      NoThe = kq.Key.NoThe,
                //                      IDDTBN = kq.Key.IDDTBN,
                //                      Ma_lk = kq.Key.Ma_lk,
                //                      MaKCB = kq.Key.MaKCB,
                //                      Export = kq.Key.Export,
                //                      Normal = kq.Key.Normal,
                //                      MaCB = kq.Key.MaCB,
                //                      SoDK = kq.Key.SoDK
                //                  }).OrderByDescending(p => p.UuTien).ThenBy(p => p.SoTT).ThenBy(p => p.MaBNhan).ToList();
                //        //}
                //        break;
                //    case 1:
                //        _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 0)
                //                  join kb in _dataContext.RaViens on bn.MaBNhan equals kb.MaBNhan
                //                  where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                //                  select bn).ToList();
                //        _lTKbn = (from bn in _lTKbn
                //                  where (bn.TenBNhan.ToLower().Contains(_tk) || bn.MaBNhan == _int_maBN)
                //                  where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                //                  where (bn.MaKP == _makp)
                //                  where (bn.MaKCB == DungChung.Bien.MaBV)
                //                  select bn).OrderByDescending(p => p.UuTien).ThenBy(p => p.SoTT).ThenBy(p => p.MaBNhan).ToList();
                //        //}
                //        break;
                //    case 2: //tất cả bệnh nhân ngoại trú
                //        {
                //            _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 0)
                //                      where (bn.NNhap >= _dttu && bn.NNhap <= _dtden)
                //                      select bn).ToList();
                //            _lTKbn = (from bn in _lTKbn
                //                      where (bn.TenBNhan.ToLower().Contains(_tk) || bn.MaBNhan == _int_maBN)
                //                      where (bn.MaKCB == DungChung.Bien.MaBV)
                //                      where (bn.MaKP == _makp)
                //                      select bn).Distinct().OrderByDescending(p => p.UuTien).ThenBy(p => p.SoTT).ThenBy(p => p.MaBNhan).ToList();
                //        }

                //        break;
                //    case 3://chuyển viện
                //        try
                //        {
                //            _lTKbn = (from bn in _dataContext.BenhNhans.Where(p => p.NoiTru == 0)
                //                      join kp in _dataContext.RaViens.Where(p => p.Status == 1) on bn.MaBNhan equals kp.MaBNhan
                //                      where (kp.MaKP == _makp)
                //                      where (bn.MaKCB == DungChung.Bien.MaBV)
                //                      where (bn.TenBNhan.ToLower().Contains(_tk) || bn.MaBNhan == _int_maBN)
                //                      where (kp.NgayRa >= _dttu && kp.NgayRa <= _dtden)
                //                      select bn).ToList();
                //            _lTKbn = (from bn in _lTKbn
                //                      where (bn.MaKP == _makp)
                //                      where (bn.MaKCB == DungChung.Bien.MaBV)
                //                      where (bn.TenBNhan.ToLower().Contains(_tk) || bn.MaBNhan == _int_maBN)
                //                      select bn).Distinct().OrderByDescending(p => p.UuTien).ThenBy(p => p.SoTT).ThenBy(p => p.MaBNhan).ToList();
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show("Lỗi tìm BN chuyển viện " + ex.Message);
                //        }
                //        break;

                //    case 4://vào viện
                //        try
                //        {
                //            _lTKbn = (from bn in _dataContext.BenhNhans
                //                      join kp in _dataContext.BNKBs on bn.MaBNhan equals kp.MaBNhan
                //                      where (kp.NgayKham >= _dttu && kp.NgayKham <= _dtden)
                //                      where (kp.MaKP == _makp && kp.PhuongAn == 1)
                //                      select bn).ToList();
                //            _lTKbn = (from bn in _lTKbn
                //                      where (bn.MaKCB == DungChung.Bien.MaBV)
                //                      where (bn.TenBNhan.ToLower().Contains(_tk) || bn.MaBNhan == _int_maBN)
                //                      select bn).Distinct().OrderByDescending(p => p.UuTien).OrderBy(p => p.SoTT).ThenBy(p => p.MaBNhan).ToList();
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show("Lỗi tìm BN vào viện " + ex.Message);
                //        }
                //        break;
                //    case 5://bệnh nhân chuyển phòng khám
                //        _lTKbn = (from bn in _dataContext.BenhNhans
                //                  join kp in _dataContext.BNKBs.Where(p => p.PhuongAn == 3) on bn.MaBNhan equals kp.MaBNhan
                //                  where (kp.NgayKham >= _dttu && kp.NgayKham <= _dtden)
                //                  where (kp.MaKP == _makp)
                //                  select bn).ToList();
                //        _lTKbn = (from bn in _lTKbn
                //                  where (bn.MaKCB == DungChung.Bien.MaBV)
                //                  where (bn.TenBNhan.ToLower().Contains(_tk) || bn.MaBNhan == _int_maBN)
                //                  select bn).Distinct().OrderByDescending(p => p.UuTien).ThenBy(p => p.SoTT).ThenBy(p => p.MaBNhan).ToList();
                //        break;
                //    case 6://tất cả bệnh nhân đã từng khám
                //        try
                //        {
                //            _lTKbn = (from bn in _dataContext.BenhNhans
                //                      join kp in _dataContext.BNKBs on bn.MaBNhan equals kp.MaBNhan
                //                      where (kp.MaKP == _makp)
                //                      where (kp.NgayKham >= _dttu && kp.NgayKham <= _dtden)
                //                      select bn).ToList();
                //            _lTKbn = (from bn in _lTKbn
                //                      where (bn.MaKCB == DungChung.Bien.MaBV)
                //                      where (bn.TenBNhan.ToLower().Contains(_tk) || bn.MaBNhan == _int_maBN)
                //                      select bn).Distinct().OrderByDescending(p => p.UuTien).ThenBy(p => p.SoTT).ThenBy(p => p.MaBNhan).ToList();
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show("Lỗi tìm BN vào viện " + ex.Message);
                //        }
                //        break;
                //}
                ////binS_DSBN.DataSource = _lTKbn;
                //grcBNhankb.DataSource = null;
                //grcBNhankb.DataSource = _lTKbn;
                //}
                //else
                //{
                //    //switch (cboTimRaVien.SelectedIndex)
                //    //{
                //    //case 0:
                //    var bnkb = (from kb in _dataContext.BNKBs
                //                where (kb.NgayKham <= _dtden && kb.NgayKham >= _dttu)
                //                group kb by new { kb.MaBNhan } into kq
                //                select new
                //                {
                //                    kq.Key.MaBNhan,
                //                    IDKB = kq.Max(p => p.IDKB),
                //                }).ToList();
                //    _lTKbn = (from kb in bnkb
                //              join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                //              join bnkbenh in _dataContext.BNKBs on kb.IDKB equals bnkbenh.IDKB
                //              where (bnkbenh.MaKPdt == _makp && bnkbenh.PhuongAn == 3 && (bn.Status <= 1 || bn.Status == 4 || bn.Status == 5))
                //              where (bn.TenBNhan.ToLower().Contains(_tk) || bn.MaBNhan == _int_maBN)
                //              where (bn.MaKCB == DungChung.Bien.MaBV)
                //              select bn).Distinct().ToList();
                //    grcBNhankb.DataSource = _lTKbn;
                //}
                #endregion
                int soBNBH = (from row in tbBenhnhan.AsEnumerable() where row.Field<string>("DTuong") == "BHYT" select row).Count();
                int soBNDV = (from row in tbBenhnhan.AsEnumerable() where row.Field<string>("DTuong").ToLower() == "dịch vụ" select row).Count();
                int soBNKSK = (from row in tbBenhnhan.AsEnumerable() where row.Field<string>("DTuong").ToLower() == "ksk" select row).Count();
                int soBNKMP = (from row in tbBenhnhan.AsEnumerable() where row.Field<string>("DTuong").ToLower() == "kmp" select row).Count();
                lblTSBN.Text = "TS: " + tbBenhnhan.Rows.Count + " (" + soBNBH + " BN BHYT, " + soBNDV + " BN Dịch vụ, " + soBNKSK + " BN KSK" + (DungChung.Bien.MaBV == "20001" ? (", " + soBNKMP + " BN KMP") : "") + ")";
                // lblTSBN.Text = "TS: " + tbBenhnhan.Rows.Count + " (" + soBNBH + " BN BHYT, " + soBNDV + " BN Dịch vụ)";
                labThongBaoBNCP.Text = ThongBaoBNChuyenPK();
                TimKiemcheck();
                process = false;
            }
        }
        private string ThongBaoBNChuyenPK()
        {
            bool _thuchien = false;
            if (_thuchien)
            {
                try
                {

                    string strSQL = "sp_KB_TimKiemBN";
                    string[] strpara = new string[] { "@chokham", "@ckChuyen", "@status", "@MaKP", "@MaKCB", "@maBNhan", "@tenBNhan", "@tungay", "@denngay" };
                    object[] oValue = new object[] { true, chkChuyenBN.Checked, cboTimRaVien.SelectedIndex, _makp, DungChung.Bien.MaBV, "", "", _dttu, _dtden };
                    SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Bit, SqlDbType.Bit, SqlDbType.Int, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.NVarChar, SqlDbType.DateTime, SqlDbType.DateTime };

                    connect.Connect();

                    DataTable tb = connect.FillDatatable(strSQL, CommandType.StoredProcedure, strpara, oValue, sqlDBType);
                    int id = 0;
                    id = tb.Rows.Count;
                    if (id > 0)
                    {
                        string a = "Có: " + id + " bệnh nhân được chuyển phòng khám đang chờ khám";
                        return a;
                    }
                    else
                    {
                        return "";
                    }

                }
                catch (Exception ex)
                {
                    return "";
                }

                #region linq
                //DateTime _dttu1;
                //DateTime _dtden1;
                //_dttu1 = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
                //_dtden1 = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
                //var bnkb = (from kb in _dataContext.BNKBs
                //            where (kb.NgayKham <= _dtden1 && kb.NgayKham >= _dttu1)
                //            group kb by new { kb.MaBNhan } into kq
                //            select new
                //            {
                //                kq.Key.MaBNhan,
                //                IDKB = kq.Max(p => p.IDKB),
                //            }).ToList();
                //var bncp = (from kb in bnkb
                //            join bn in _dataContext.BenhNhans on kb.MaBNhan equals bn.MaBNhan
                //            join bnkbenh in _dataContext.BNKBs on kb.IDKB equals bnkbenh.IDKB
                //            where (bnkbenh.MaKPdt == _makp && bnkbenh.PhuongAn == 3 && bn.Status <= 1)
                //            where (bn.MaKCB == DungChung.Bien.MaBV)
                //            select bn).Distinct().ToList();


                //int id = 0;
                //id = bncp.Count;
                //if (id > 0)
                //{
                //    string a = "Có: " + id + " bệnh nhân được chuyển phòng khám đang chờ khám";
                //    return a;
                //}
                //else
                //{
                //    return "";
                //}
                #endregion
            }
            else
            {
                return "";
            }
        }
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimTuNgay_Leave(object sender, EventArgs e)
        {

        }

        private void dtTimDenNgay_Leave(object sender, EventArgs e)
        {

        }

        private void cboTimRaVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            //int makp = 0;
            //if (lupTimMaKP.EditValue!=null)
            //{
            //    makp = Convert.ToInt32(lupTimMaKP.EditValue);
            //    var Chuyenkhoa = _dataContext.KPhongs.Where(p => p.MaKP == makp).Select(p => p.ChuyenKhoa).FirstOrDefault();
            //    if (Chuyenkhoa != null && Chuyenkhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.CKNoiTiet)
            //        barXNBNMT.Enabled = true;
            //}

            if (DungChung.Bien.MaBV == "27001")
            {
                mnMoiBN.Enabled = false;
            }
            else
                mnMoiBN.Enabled = true;
            TimKiem();

        }
        private bool KTGiaiQuyet(int mabn, int a)
        {
            if (a == 0)
            {

                var tt = _dataContext.VienPhis.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                if (tt != null)
                {
                    MessageBox.Show("Bệnh nhân đã thanh toán, bạn không thể sửa");
                    return false;
                }
                //try
                //{
                //    if (connect.isConnect)
                //    {
                //        string strSQL = "SELECT * FROM dbo.RaVien WHERE MaBNhan = '" + mabn + "'";

                //        DataTable dtTble = connect.FillDatatable(strSQL, CommandType.Text);
                //        if (dtTble.Rows.Count > 0)
                //        {
                //            DialogResult _result = MessageBox.Show("Bệnh nhân đã làm thủ tục ra viện, bạn vẫn muốn sửa?", "thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //            if (_result == DialogResult.Yes)
                //            {
                //                string deleFile = "delete RaVien where MaBnhan ='" + mabn + "'";
                //                connect.ExecuteNonQuery(deleFile, CommandType.Text);
                //                return true;
                //            }
                //            else
                //                return false;
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                #region linq
                var rv = _dataContext.RaViens.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                if (rv != null)
                {
                    DialogResult _result = MessageBox.Show("Bệnh nhân đã làm thủ tục ra viện, bạn vẫn muốn sửa?", "thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        _dataContext.RaViens.Remove(rv);
                        _dataContext.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                #endregion




            }

            if (a == 1)
            {
                #region linq
                var vv = _dataContext.VaoViens.Where(p => p.MaBNhan == mabn).ToList();
                if (vv.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã làm thủ tục vào viện, bạn không thể sửa");
                    return false;
                }
                #endregion
                //try
                //{
                //    if (connect.isConnect)
                //    {
                //        string strSQL = "SELECT * FROM dbo.VaoVien WHERE MaBNhan  ='" + mabn + "'";

                //        DataTable dtTble = connect.FillDatatable(strSQL, CommandType.Text);
                //        if (dtTble.Rows.Count > 0)
                //        {
                //            MessageBox.Show("Bệnh nhân đã làm thủ tục vào viện, bạn không thể sửa");
                //            return false;
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}

            }
            if (a == 2)
            {
                //try
                //{
                //    if (connect.isConnect)
                //    {
                //        string strSQL = "SELECT * FROM dbo.RaVien WHERE MaBNhan  ='" + mabn + "' AND Status = 1";

                //        DataTable dtTble = connect.FillDatatable(strSQL, CommandType.Text);
                //        if (dtTble.Rows.Count > 0)
                //        {
                //            MessageBox.Show("Bệnh nhân đã làm thủ tục chuyển viện, bạn không thể sửa");
                //            return false;
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                #region linq
                var rv = _dataContext.RaViens.Where(p => p.MaBNhan == mabn && p.Status == 1).ToList();
                if (rv.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã làm thủ tục chuyển viện, bạn không thể sửa");
                    return false;
                }
                #endregion

            }
            return true;
        }
        private void radGiaiQuyet_SelectedIndexChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
            switch (radGiaiQuyet.SelectedIndex)
            {
                case 0:
                    //txtGhiChu.Enabled = false;
                    lbVaoKhoa.Enabled = false;
                    lupKhoaDT.Enabled = false;
                    mnKBVaoVien.Enabled = false;
                    mnChuyenVien.Enabled = true;
                    // labLydo.Enabled = false;
                    // txtGhiChu.Enabled = false;
                    lupKhoaDT.EditValue = 0;
                    mnHenKham.Enabled = true;
                    mnXacNhanNGhiOm.Enabled = true;
                    mnChuyenVien.Caption = "Ra viện";
                    if (dt_NgayChuyen.Text == "")
                        dt_NgayChuyen.DateTime = DateTime.Now;

                    break;
                case 1:
                    // txtGhiChu.Enabled = false;
                    lbVaoKhoa.Enabled = true;
                    lbVaoKhoa.Text = "Vào khoa:";
                    lupKhoaDT.Enabled = true;
                    mnKBVaoVien.Enabled = true;
                    mnChuyenVien.Enabled = false;
                    // labLydo.Enabled = false;
                    // txtGhiChu.Enabled = false;
                    mnHenKham.Enabled = false;
                    mnXacNhanNGhiOm.Enabled = false;
                    if (dt_NgayChuyen.Text == "")
                        dt_NgayChuyen.DateTime = DateTime.Now;
                    break;
                case 2:
                    // txtGhiChu.Enabled = false;
                    lbVaoKhoa.Enabled = false;
                    lupKhoaDT.Enabled = false;
                    mnKBVaoVien.Enabled = false;
                    mnChuyenVien.Enabled = true;
                    // labLydo.Enabled = false;
                    // txtGhiChu.Enabled = false;
                    lupKhoaDT.EditValue = 0;
                    mnHenKham.Enabled = false;
                    mnXacNhanNGhiOm.Enabled = false;
                    mnChuyenVien.Caption = "Chuyển viện";
                    if (dt_NgayChuyen.Text == "")
                        dt_NgayChuyen.DateTime = DateTime.Now;
                    break;
                case 3:
                    // txtGhiChu.Enabled = true;
                    lbVaoKhoa.Enabled = true;
                    lbVaoKhoa.Text = "Chuyển PK:";
                    lupKhoaDT.Enabled = true;
                    mnKBVaoVien.Enabled = false;
                    mnChuyenVien.Enabled = false;
                    // labLydo.Enabled = true;
                    // txtGhiChu.Enabled = true;
                    mnHenKham.Enabled = false;
                    mnXacNhanNGhiOm.Enabled = false;
                    if (dt_NgayChuyen.Text == "")
                        dt_NgayChuyen.DateTime = DateTime.Now;
                    break;
                case 4:
                    lbVaoKhoa.Enabled = true;
                    lbVaoKhoa.Text = "Đ.khám|ĐT:";
                    lupKhoaDT.Enabled = true;
                    mnKBVaoVien.Enabled = false;
                    mnChuyenVien.Enabled = false;
                    // labLydo.Enabled = true;
                    // txtGhiChu.Enabled = true;
                    mnHenKham.Enabled = true;
                    mnXacNhanNGhiOm.Enabled = true;
                    dt_NgayChuyen.Text = "";
                    lupKhoaDT.Text = "";
                    break;

            }
        }
        private class DichVuTheoKhoXuat
        {
            public int? MaDV { get; set; }
            public string TenDV { get; set; }
            public string HamLuong { get; set; }
            public string SoLo { get; set; }
            public DateTime? HanDung { get; set; }
            public bool SLTon { get; set; }
            public double Gia { get; set; }


        }
        public class DVu
        {
            public string TenDV { set; get; }


            public string TenHC { get; set; }

            public int MaDV { get; set; }

            public string DonVi { get; set; }

            public int TyLeSD { get; set; }

            public string MaQD { get; set; }

            public string NguonGoc { get; set; }

            public string HamLuong { get; set; }
            public string TenThau2017 { get; set; }
            public string GhiChu { get; set; }
            public double DonGia { get; set; }
            public int TyLeBQ { get; set; }
            //public string SoLo { get; set; }

            //public DateTime? HanDung { get; set; }
        }

        void search24012()
        {
            string _makhoxuatsd = ";" + MaKPxd.ToString() + ";";
            List<KPhong> _lkho = (from aa in _dataContext.KPhongs.Where(p => p.Status == 1)
                                  select aa).OrderBy(p => p.TenKP).ToList();
            var khoKe = _lkho.FirstOrDefault(p => p.MaKP == MaKPxd);
            int makhoake = 0;
            if (lupKhoaKhamkb.EditValue != null)
                makhoake = Convert.ToInt32(lupKhoaKhamkb.EditValue);
            string _makpsd = ";" + makhoake + ";";
            var dvu = (from tenduoc in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makpsd) && p.MaKPsd.Contains(_makhoxuatsd))
                       join tieun in _dataContext.TieuNhomDVs on tenduoc.IdTieuNhom equals tieun.IdTieuNhom
                       join dvEx in _dataContext.DichVuExes on tenduoc.MaDV equals dvEx.MaDV into kq
                       from kq1 in kq.DefaultIfEmpty()
                       select new { tenduoc.TyLeBQ, tenduoc.DonGia, tenduoc.GhiChu, tenduoc.MaTam, tenduoc.MaDV, tenduoc.TenDV, tenduoc.DongY, tenduoc.TenHC, tenduoc.HamLuong, tenduoc.NguonGoc, tenduoc.DonVi, TenThuocRG = tenduoc.TenRG, tieun.TenRG, TenThau2017 = kq1 == null ? "" : kq1.TenThau2017 }).ToList();

            var nhapdc = (from nduoc in _dataContext.NhapDs.Where(p => p.MaKP == MaKPxd).Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)
                          join nhapduoc in _dataContext.NhapDcts on nduoc.IDNhap equals nhapduoc.IDNhap
                          select new { MaDV = nhapduoc.MaDV ?? 0, nhapduoc.SoLuongN, nhapduoc.SoLuongX, nhapduoc.SoLo, nhapduoc.HanDung, nduoc.PLoai, nduoc.KieuDon, nhapduoc.SoLuongDY }
                   ).ToList();
            var thuoc = (from tenduoc in dvu
                         join nhapduoc in nhapdc on tenduoc.MaDV equals nhapduoc.MaDV
                         group new { tenduoc, nhapduoc } by new { tenduoc.GhiChu, tenduoc.TenThuocRG, tenduoc.TenRG, tenduoc.MaTam, tenduoc.TenThau2017, tenduoc.TenHC, tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, tenduoc.NguonGoc, tenduoc.HamLuong, tenduoc.DonGia, tenduoc.TyLeBQ, nhapduoc.SoLo, nhapduoc.HanDung } into kq
                         select new { TyLeBQ = Convert.ToInt32(kq.Key.TyLeBQ), DonGia = kq.Key.DonGia, TenRG = kq.Key.TenRG, TenThuocRG = kq.Key.TenThuocRG, MaTam = kq.Key.MaTam, TenThau2017 = kq.Key.TenThau2017, TenHC = kq.Key.TenHC, TenDV = kq.Key.TenDV, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, NguonGoc = kq.Key.NguonGoc, HamLuong = kq.Key.HamLuong, GhiChu = kq.Key.GhiChu, SoLo = kq.Key.SoLo, HanDung = kq.Key.HanDung, }
                        ).OrderBy(p => p.TenDV).ToList();

            if (khoKe != null && khoKe.IsMuaNgoai == true)
                thuoc = (from tenduoc in dvu
                         join nhapduoc in nhapdc on tenduoc.MaDV equals nhapduoc.MaDV
                         group new { tenduoc } by new { tenduoc.GhiChu, tenduoc.TenThuocRG, tenduoc.TenRG, tenduoc.MaTam, tenduoc.TenThau2017, tenduoc.TenHC, tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, tenduoc.NguonGoc, tenduoc.HamLuong, tenduoc.DonGia, tenduoc.TyLeBQ, nhapduoc.SoLo, nhapduoc.HanDung } into kq
                         select new { TyLeBQ = Convert.ToInt32(kq.Key.TyLeBQ), DonGia = kq.Key.DonGia, TenRG = kq.Key.TenRG, TenThuocRG = kq.Key.TenThuocRG, MaTam = kq.Key.MaTam, TenThau2017 = kq.Key.TenThau2017, TenHC = kq.Key.TenHC, TenDV = kq.Key.TenDV, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, NguonGoc = kq.Key.NguonGoc, HamLuong = kq.Key.HamLuong, GhiChu = kq.Key.GhiChu, SoLo = kq.Key.SoLo, HanDung = kq.Key.HanDung, }
                        ).OrderBy(p => p.TenDV).ToList();
            foreach (var item in thuoc)
            {
                dsthuoc moi = new dsthuoc();
                if (DungChung.Bien.MaBV == "24012")
                {
                    moi.SoLo = item.SoLo;
                    moi.HanDung = item.HanDung;
                }
                moi.MaTam = item.MaTam;
                moi.TenDV = item.TenDV;
                moi.MaDV = item.MaDV;
                moi.HamLuong = item.HamLuong;
                moi.NguonGoc = item.NguonGoc;
                moi.TenHC = item.TenHC;
                moi.DonVi = item.DonVi;
                moi.NguonGoc = item.NguonGoc;
                moi.TenRG = item.TenRG;
                moi.TenThuocRG = item.TenThuocRG;
                moi.TenThau2017 = item.TenThau2017;
                moi.GhiChu = item.GhiChu;
                moi.DonGia = item.TyLeBQ != null ? (item.DonGia + item.DonGia * item.TyLeBQ / 100) : item.DonGia;
                if (item.TenRG.ToLower().Contains("thuốc"))// HIS - 1114 26042022
                {
                    var dsgia = Ham._getDSGia(_dataContext, item.MaDV, MaKPxd);
                    if (dsgia.Count > 0)
                        moi.SLTon = true;
                    else
                        moi.SLTon = false;
                }
                _lDvTheoKho1.Add(moi);
            }
        }

        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập tên|Mã số|Số thẻ BN")
                txtTimKiem.Text = "";
        }

        private void btnref_Click(object sender, EventArgs e)
        {
            TimKiem();
            if (frmGoi != null)
                frmGoi.RefreshData();
        }



        private void grvDonThuocct_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (grvDonThuocct.OptionsBehavior.Editable == true)
            {
                switch (e.Column.Name)
                {
                    case "colXoactdt":
                        int xoaiddonct = 0;
                        if (!DungChung.Ham.KTraTT(_dataContext, String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text)))
                        {
                            if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null && grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString() != "")
                            {
                                xoaiddonct = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct).ToString());
                                var dThuocct = _dataContext.DThuoccts.FirstOrDefault(o => o.IDDonct == xoaiddonct);

                                if (dThuocct != null && dThuocct.ThanhToan == 1)
                                {
                                    MessageBox.Show("Thuốc/vật tư đã thanh toán, bạn không thể xóa");
                                    return;
                                }
                                if (grvDonThuocct.GetFocusedRowCellValue(colIDThuoc) != null && grvDonThuocct.GetFocusedRowCellValue(colIDThuoc).ToString() != "")
                                {
                                    int sopl = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colSoPL));
                                    if (sopl > 0)
                                    {
                                        MessageBox.Show("Đơn thuốc đã lên phiếu lĩnh, bạn không thể xóa");
                                    }
                                    else
                                    {
                                        double donGia = 0;
                                        double sLuong = 0;

                                        if (grvDonThuocct.GetFocusedRowCellValue(colIDThuoc) != null && grvDonThuocct.GetFocusedRowCellValue(colIDThuoc).ToString() != "")
                                            idThuoc = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDThuoc));
                                        if (grvDonThuocct.GetFocusedRowCellValue(colSoLuongct) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuongct).ToString() != "")
                                            sLuong = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colSoLuongct));
                                        if (lupTimMaKP.EditValue != null && lupTimMaKP.EditValue.ToString() != "")
                                            maKhoaKe = Convert.ToInt32(lupTimMaKP.EditValue);
                                        if (lupKhoXuat.EditValue != null && lupKhoXuat.EditValue.ToString() != "")
                                            maKhoXuat = Convert.ToInt32(lupKhoXuat.EditValue);



                                        if (_medicinesProvider.isTuTruc(maKhoXuat))
                                            TH = 2;
                                        else
                                            TH = 0;


                                        if (DungChung.Bien.MaBV == "24012" && grvDonThuocct.GetFocusedRowCellValue(colStatusct) != null && grvDonThuocct.GetFocusedRowCellValue(colStatusct).ToString() == "1")
                                        {
                                            MessageBox.Show("Không thể xóa đơn đã lĩnh");
                                            break;
                                        }
                                        DialogResult _result = MessageBox.Show("Xóa thuốc: " + grvDonThuocct.GetFocusedRowCellDisplayText(colIDThuoc), "xóa chi tiết!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (_result == DialogResult.Yes)
                                        {
                                            if (xoaiddonct > 0)
                                            {
                                                deleteDThuoccts.Add(xoaiddonct);
                                            }
                                            _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, sLuong, 0, idThuoc, 0, donGia, 0, ppxuat);
                                            grvDonThuocct.DeleteSelectedRows();

                                            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
                                            mnLuu.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bệnh nhân đã thanh toán, Bạn không thể xóa!");
                        }
                        break;
                    case "htpxoa":
                        MessageBox.Show("htp");
                        break;
                }
            }
        }

        private void grvchiDinh_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (grvchiDinh.OptionsBehavior.ReadOnly == true)
            {
                MessageBox.Show("Bạn chưa nhập khám bệnh cho BN");
            }
            switch (e.Column.Name)
            {
                case "colXoactdv":
                    bool _cothexoa = true;
                    if (grvchiDinh.GetFocusedRowCellValue(colIDThuoc) != null && grvchiDinh.GetFocusedRowCellValue(colIDThuoc).ToString() != "")
                    {
                        int i = 0, _IDCD = 0;
                        if (grvchiDinh.GetRowCellValue(e.RowHandle, colIDDonct) != null && grvchiDinh.GetRowCellValue(e.RowHandle, colIDDonct).ToString() != "")
                        {
                            i = Convert.ToInt32(grvchiDinh.GetRowCellValue(e.RowHandle, colIDDonct));
                            if (grvchiDinh.GetRowCellValue(e.RowHandle, colIDCD) != null && !string.IsNullOrEmpty(grvchiDinh.GetRowCellValue(e.RowHandle, colIDCD).ToString()))
                                _IDCD = Convert.ToInt32(grvchiDinh.GetRowCellValue(e.RowHandle, colIDCD));
                            if (i > 0)
                            {
                                var ktcd = _dataContext.ChiDinhs.Where(p => p.IDCD == _IDCD).ToList();
                                if (_IDCD > 0 & ktcd.Count > 0)
                                {
                                    MessageBox.Show("Dịch vụ CLS đã được thực hiện, bạn không thể xóa");
                                    _cothexoa = false;
                                }
                                if (_cothexoa)
                                {
                                    if (DungChung.Ham.KTraTT(_dataContext, String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text)))
                                        _cothexoa = false;
                                }
                                if (_cothexoa)
                                {
                                    DialogResult _result = MessageBox.Show("Bạn muốn xóa dịch vụ: " + grvchiDinh.GetRowCellDisplayText(e.RowHandle, colIDThuoc).ToString(), "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_result == DialogResult.Yes)
                                    {
                                        grvchiDinh.DeleteRow(e.RowHandle);
                                        var xoa = _dataContext.DThuoccts.Single(p => p.IDDonct == i);
                                        _dataContext.DThuoccts.Remove(xoa);
                                        _dataContext.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                grvchiDinh.DeleteRow(e.RowHandle);
                            }
                        }
                        else
                        {
                            grvchiDinh.DeleteRow(e.RowHandle);
                        }
                    }
                    break;
            }
        }

        private void grvBNhankb_DataSourceChanged(object sender, EventArgs e)
        {
            grvBNhankb_FocusedRowChanged(null, null);
        }

        private void grvDonThuocct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetTextStock();
        }



        private void grcDonThuocct_Click(object sender, EventArgs e)
        {
            if (grvDonThuocct.OptionsBehavior.ReadOnly == true && chkKDNgoai.Checked == false)
            {
                if (string.IsNullOrEmpty(lupKhoXuat.Text))
                {
                    MessageBox.Show("Bạn chưa chọn kho xuất");
                    lupKhoXuat.Focus();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>true: ngày khám là ngày y lệnh cuối cùng và là ngày hạn thẻ; false: Ngày y lệnh cuối cùng không phải ngày hạn thẻ</returns>
        private bool ktraHanThe()
        {
            bool rs = false;
            #region  cảnh báo khi ngày y lệnh cuối cùng trùng với ngày hạn thẻ -26007
            if (DungChung.Bien.MaBV == "26007" && dtNgayKhamkb.Properties.ReadOnly == false)
            {

                int ot; int _maBN = 0;
                DateTime ngayKham = dtNgayKhamkb.DateTime.Date;
                if (Int32.TryParse(txtMaBNhan.Text, out ot))
                    _maBN = Convert.ToInt32(txtMaBNhan.Text);
                if (_maBN > 0)
                {
                    // kiểm tra hạn thẻ
                    #region linq
                    var qbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _maBN).Select(p => p.HanBHDen).FirstOrDefault();
                    if (qbn != null)
                    {

                        //Kiểm tra ngày khám bệnh có phải ngày khám cuối cùng không
                        var qkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _maBN).OrderByDescending(p => p.NgayKham).FirstOrDefault();
                        if (qkb != null)
                        {
                            if (ngayKham >= qkb.NgayKham.Value.Date && ngayKham == qbn.Value.Date)
                            {
                                rs = true;
                                MessageBox.Show("Ngày y lệnh cuối cùng trùng với hạn thẻ");
                            }
                        }
                        else if (ngayKham == qbn.Value.Date)
                        {
                            rs = true;
                            MessageBox.Show("Ngày y lệnh cuối cùng trùng với hạn thẻ");
                        }
                    }
                    #endregion
                    #region ADO

                    //DataTable qbn = connect.FillDatatable("SELECT HanBHDen FROM dbo.BenhNhan WHERE MaBNhan = '" + _maBN + "'", CommandType.Text); //_dataContext.BenhNhans.Where(p => p.MaBNhan == _maBN).Select(p => p.HanBHDen).FirstOrDefault();
                    //if (qbn.Rows.Count > 0)
                    //{
                    //    //Kiểm tra ngày khám bệnh có phải ngày khám cuối cùng không
                    //    DateTime dtHanBHDen = Convert.ToDateTime(qbn.Rows[0]["HanBHDen"].ToString()).Date;
                    //    DataTable qkb = connect.FillDatatable("SELECT NgayKham FROM dbo.BenhNhan WHERE MaBNhan = '" + _maBN + "' order by NgayKham desc", CommandType.Text); //_dataContext.BNKBs.Where(p => p.MaBNhan == _maBN).OrderByDescending(p => p.NgayKham).FirstOrDefault();
                    //    if (qkb.Rows.Count > 0)
                    //    {
                    //        if (ngayKham >= Convert.ToDateTime(qkb.Rows[0]["NgayKham"].ToString()).Date && ngayKham == dtHanBHDen)
                    //        {
                    //            rs = true;
                    //            MessageBox.Show("Ngày y lệnh cuối cùng trùng với hạn thẻ");
                    //        }

                    //    }
                    //    else if (ngayKham == dtHanBHDen)
                    //    {
                    //        rs = true;
                    //        MessageBox.Show("Ngày y lệnh cuối cùng trùng với hạn thẻ");
                    //    }
                    //}
                    #endregion
                }
            }
            #endregion
            return rs;
        }

        private void dtNgayKhamkb_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
            // ktraHanThe();

        }

        private void lupKhoaKhamkb_EditValueChanged(object sender, EventArgs e)
        {
            _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 33-lupKhoaKhamkb_EditValueChanged-  4577     ";
            if (lupKhoaKhamkb.EditValue != null && lupKhoaKhamkb.Text != "")
            {
                int makp = 0;
                string _makp = "";
                if (lupKhoaKhamkb.EditValue != null)
                {
                    makp = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                    _makp = ";" + makp.ToString() + ";";
                }
                if (_maCQCQ == "24009")
                {
                    lupNguoiKhamkb.Properties.DataSource = _lCanBo.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.MaCCHN != null && p.MaCCHN != "").ToList();
                }
                else
                {
                    lupNguoiKhamkb.Properties.DataSource = _lCanBo.Where(p => p.Status == 1).Where(p => p.MaKPsd != null && p.MaKPsd.Contains(_makp)).Where(p => p.CapBac != null).Where(p => p.CapBac.ToLower().Contains("bs") || p.CapBac.ToLower().Contains("bác sĩ") || p.CapBac.ToLower().Contains("bác sỹ") || p.CapBac.ToLower().Contains("ys") || p.CapBac.ToLower().Contains("y sĩ") || p.CapBac.ToLower().Contains("y sỹ") || p.CapBac.ToLower().Contains("giáo sư") || p.CapBac.ToLower().Contains("gs") || p.CapBac.ToLower().Contains("tiến sĩ") || p.CapBac.ToLower().Contains("ts")).ToList();
                }

                if (DungChung.Bien.MaBV == "30007")
                {
                    if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                    {
                        if (_lCanBo.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.MaCB == DungChung.Bien.MaCB).Count() > 0)
                        {
                            lupNguoiKhamkb.EditValue = DungChung.Bien.MaCB;
                        }
                    }
                }
                int chuyenkhoa = 0;
                if (_lKPhong_data.Where(p => p.MaKP == makp).Select(p => p.MaCK).ToList().Count > 0 && _lKPhong_data.Where(p => p.MaKP == makp).Select(p => p.MaCK).First() != null)
                    chuyenkhoa = _lKPhong_data.Where(p => p.MaKP == makp).Select(p => p.MaCK).First();

                //string sqlNguoiKham = "SELECT * FROM dbo.CanBo WHERE Status = 1 AND MaKPsd LIKE '%" + _makp + "%' AND CapBac IS NOT NULL AND (LOWER(CapBac) like N'%bs%' OR LOWER(CapBac) like N'%bác sĩ%' OR LOWER(CapBac) like N'%bác sỹ%' OR LOWER(CapBac) like N'%ys%'  OR LOWER(CapBac) like N'%y sĩ%' OR LOWER(CapBac) like N'%y sỹ%')";
                //connect.LoadDataInToLookup(sqlNguoiKham, CommandType.Text, lupNguoiKhamkb, "TenCB", "MaCB");
                //if (DungChung.Bien.MaBV == "30007")
                //{
                //    if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                //    {
                //        //if (_lCanBo.Where(p => p.Status == 1).Where(p => p.MaKPsd.Contains(_makp)).Where(p => p.MaCB == DungChung.Bien.MaCB).Count() > 0)
                //        //    lupNguoiKhamkb.EditValue = DungChung.Bien.MaCB;
                //        connect.LoadDataInToLookup("SELECT * FROM dbo.CanBo WHERE Status = 1 AND MaKPsd LIKE '%" + _makp + "%' AND MaCB = '" + DungChung.Bien.MaCB + "'", CommandType.Text, lupNguoiKhamkb, "TenCB", "MaCB");
                //    }
                //}
                //int chuyenkhoa = 0;
                //string sqlchuyenkhoa = "select MaCK from KPhong where status = 1 and MaKP = '" + makp + "'";
                //DataTable tbchuyenkhoa = connect.FillDatatable(sqlchuyenkhoa, CommandType.Text);
                //if (tbchuyenkhoa.Rows.Count > 0)
                //{
                //    if (!string.IsNullOrEmpty(tbchuyenkhoa.Rows[0]["MaCK"].ToString()))
                //        chuyenkhoa = Convert.ToInt32(tbchuyenkhoa.Rows[0]["MaCK"].ToString());

                //}

                lup_ChuyenKhoa.EditValue = chuyenkhoa;
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 34-lupKhoaKhamkb_EditValueChanged- lup_ChuyenKhoa-4623     ";
                xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
                lupKhoXuat_EditValueChanged(sender, e);
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 35-lupKhoaKhamkb_EditValueChanged- lupKhoXuat-4626     ";
                _maKhoaKB = makp;
                mnLuu.Enabled = true;
                if (DungChung.Bien.MaBV == "30303" || DungChung.Bien.MaBV == "34019")
                {
                    var qdskpsd = _dataContext.KPhongs.Where(p => p.MaKP == makp).FirstOrDefault();
                    if (qdskpsd != null && qdskpsd.MaKPsd != null)
                    {
                        List<int> lmakp = new List<int>();
                        List<string> lstr = new List<string>();
                        lstr = qdskpsd.MaKPsd.Split(';').ToList();
                        foreach (var a in lstr)
                        {
                            int ot;
                            if (int.TryParse(a, out ot))
                                lmakp.Add(Convert.ToInt32(a));
                        }
                        var kd = _lKPhong_data.Where(p => ((p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc) || (p.PLoai == ("Tủ trực") && p.NhomKP == DungChung.Bien.MaKP)) && p.MaBVsd == DungChung.Bien.MaBV && lmakp.Contains(p.MaKP)).Where(p => p.Status == 1).OrderBy(p => p.TenKP).ToList();
                        if (kd.Count > 0)
                        {
                            lupKhoXuat.Properties.DataSource = kd.ToList();
                        }
                    }
                    else
                    {
                        lupKhoXuat.Properties.DataSource = null;
                    }


                }
            }
        }

        private void lupNguoiKhamkb_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        private void txtBenhKhac_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        private void lupKhoaDT_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        private void grvchiDinh_Click(object sender, EventArgs e)
        {

        }

        private void btnChiDinh_Click(object sender, EventArgs e)
        {

        }

        private void xtraNgoaiTru_TextChanged(object sender, EventArgs e)
        {
            if (xtraKhamBenh.Text.Contains("*"))
                MessageBox.Show("thay đổi tiêu để");
        }

        private void grvBNhankb_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {

                mnThongTinBN_ItemClick(null, null);
                int mabn = Convert.ToInt32(txtMaBNhan.Text);
                var qbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn).FirstOrDefault();
                if (qbn != null)
                {
                    grvBNhankb.SetRowCellValue(grvBNhankb.FocusedRowHandle, colIDDTBN, qbn.IDDTBN);
                }

            }
        }

        void _dlg(bool tt)
        {
            _thanhToan = tt;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            _thanhToan = false;
            FormThamSo.frm_XemChiPhi frm = new FormThamSo.frm_XemChiPhi(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), txtTenBenhNhan.Text);
            frm._getdata = new
                 FormThamSo.frm_XemChiPhi.getvalue(_dlg);
            frm.ShowDialog();
            if (_thanhToan)
            {
                if (DungChung.Bien.MaBV == "24009")
                {
                    TimKiem();
                }
            }
        }

        private void grvDonThuocct_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

            mnLuu.Enabled = true;

            if (DungChung.Bien.MaBV == "27001")
                coltrongBH.OptionsColumn.ReadOnly = true;

            if (e.FocusedColumn.FieldName.Equals(nameof(DThuocctModel.SoLuongct)))
            {
                SetTextStock();
            }
        }

        private void chkChuyenBN_CheckedChanged(object sender, EventArgs e)
        {
            TimKiem();
            if (chkChuyenBN.Checked)
            {
                mnKBMoi.Enabled = true;
                panelDSKB.Visible = true;
            }
            else
            {
                mnKBMoi.Enabled = false;
                panelDSKB.Visible = false;
            }

        }

        private void grvChuyenKhoa_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 16-grvChuyenKhoa_FocusedRowChanged -  4748   ";
            int id = 0;

            if (grvChuyenKhoa.GetFocusedRowCellValue(colID) != null && grvChuyenKhoa.GetFocusedRowCellValue(colID).ToString() != "")
            {
                txtIdkb.Text = grvChuyenKhoa.GetFocusedRowCellValue(colID).ToString();
                id = Convert.ToInt32(txtIdkb.Text);
            }
            else
            {
                txtIdkb.Text = "";
            }
            #region thay thế phần tạm bỏ


            var qkq = _dataContext.BNKBs.Where(p => p.IDKB == id).FirstOrDefault();
            if (qkq != null)
            {
                if (!string.IsNullOrEmpty(qkq.NgayKham.ToString()))
                    dtNgayKhamkb.DateTime = Convert.ToDateTime(qkq.NgayKham.ToString());
                if (!string.IsNullOrEmpty(qkq.PhuongAn.ToString()))
                    radGiaiQuyet.SelectedIndex = Convert.ToInt32(qkq.PhuongAn.ToString());
                else
                    radGiaiQuyet.SelectedIndex = 4;
                int makp = 0;
                if (!string.IsNullOrEmpty(qkq.MaKP.ToString()))
                {
                    makp = Convert.ToInt32(qkq.MaKP.ToString());
                    lupKhoaKhamkb.EditValue = makp;
                    _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 17-grvChuyenKhoa_FocusedRowChanged -lupKhoaKhamkb-  4945     ";
                }

                lupNguoiKhamkb.EditValue = qkq.MaCB.ToString();
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 18-grvChuyenKhoa_FocusedRowChanged -lupNguoiKhamkb-  4949    ";
                lupChanDoanKb.EditValue = qkq.ChanDoan;
                lupMaICDkb.EditValue = qkq.MaICD.ToString();
                txtBenhChinh.Text = qkq.ChanDoan;//qkq.MaICD;
                lup_ChuyenKhoa.EditValue = Convert.ToInt32(qkq.MaCK.ToString());

                string[] icd = new string[4] { "", "", "", "" };
                if (!string.IsNullOrEmpty(qkq.MaICD2.ToString()))
                {
                    string[] icd2 = qkq.MaICD2.ToString().Split(';');
                    for (int i = 0; i < icd2.Length; i++)
                    {

                        if (icd2[i] != null)
                        {
                            if (i == 2)
                                icd[i] += icd2[i];
                            else
                                if (i > 2)
                                icd[2] += ";" + icd2[i];
                            else
                                icd[i] = icd2[i];

                        }

                    }
                }

                LupICD2.EditValue = icd[0];
                LupICD3.EditValue = icd[1];
                LupICD4.EditValue = icd[2];
                lupKhac.EditValue = qkq.MaICD2.ToString();
                txtBenhKhac1.Text = qkq.BenhKhac.ToString();
                string[] benhkhac = new string[4] { "", "", "", "" };
                if (!string.IsNullOrEmpty(qkq.BenhKhac.ToString()))
                {
                    string[] icd2 = qkq.BenhKhac.ToString().Split(';');
                    for (int i = 0; i < icd2.Length; i++)
                    {

                        if (icd2[i] != null)
                        {
                            if (i == 2)
                                benhkhac[i] += icd2[i];
                            else if (i > 2)
                            {
                                if (icd2[i].Length > 1)
                                    benhkhac[2] += ";" + icd2[i];
                            }

                            else
                                benhkhac[i] = icd2[i];
                        }

                    }
                }
                if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24297")
                {
                    txtBenhPhu2.EditValue = benhkhac[0];
                    txtBenhPhu3.EditValue = benhkhac[1];
                    if (DungChung.Bien.MaBV == "14017")
                        txtBenhKhac4.Text = benhkhac[2];
                    else
                        txtBenhPhu.Text = benhkhac[2];

                }
                else
                {
                    txtBenhKhac2.Text = benhkhac[0];
                    txtBenhKhac3.Text = benhkhac[1];
                    if (DungChung.Bien.MaBV == "30002")
                        txtBenhPhu.EditValue = benhkhac[2];
                    else
                        txtBenhKhac4.Text = benhkhac[2];
                }


                txtGhiChu.Text = qkq.GhiChu;

                if (!string.IsNullOrEmpty(qkq.PhuongAn.ToString()))
                {
                    int pa = Convert.ToInt32(qkq.PhuongAn.ToString());
                    radGiaiQuyet.SelectedIndex = pa;
                    if (pa == 1)
                    {
                        mnKBVaoVien.Enabled = true;
                    }
                    else
                    {
                        mnKBVaoVien.Enabled = false;
                    }
                    if (pa == 3 || pa == 4)
                    {
                        mnKBMoi.Enabled = true;
                    }
                    else
                    {
                        mnKBMoi.Enabled = false;
                    }
                }
                else
                    radGiaiQuyet.SelectedIndex = 0;

                if (!string.IsNullOrEmpty(qkq.PhuongAn.ToString()))
                {
                    _phuongan = Convert.ToInt32(qkq.PhuongAn.ToString());
                }
                if (!string.IsNullOrEmpty(qkq.MaKPdt.ToString()) && _phuongan != 4)
                    lupKhoaDT.EditValue = Convert.ToInt32(qkq.MaKPdt.ToString());
                else
                    lupKhoaDT.EditValue = 0;
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 19-grvChuyenKhoa_FocusedRowChanged -lupKhoaDT-  5035     ";
                if (!string.IsNullOrEmpty(qkq.NgayNghi.ToString()))
                    dt_NgayChuyen.DateTime = Convert.ToDateTime(qkq.NgayNghi.ToString());
                else
                    dt_NgayChuyen.Text = "";

                int makptim = 0;
                lupKhoaKhamkb.EditValue = makp;
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 20-grvChuyenKhoa_FocusedRowChanged -lupKhoaKhamkb-  5043     ";
                if (lupTimMaKP.EditValue != null)
                    makptim = Convert.ToInt32(lupTimMaKP.EditValue);
                if (makptim == makp)
                {
                    // tạm bỏ kiểm tra cuongtm 27/10
                    //if (!DungChung.Ham.KTraTT(_dataContext, String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text)))
                    if (true)
                    {
                        // kiểm tra khoa phòng khác đã KB thi không cho sửa
                        int rs;
                        int _int_maBN = 0;
                        if (Int32.TryParse(txtMaBNhan.Text, out rs))
                            _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                        // DataTable tbIdKB = connect.FillDatatable("SELECT MAX(IDKB) AS ID FROM dbo.BNKB WHERE MaBNhan = '" + _int_maBN + "'", CommandType.Text);



                        int idmax = 0;
                        if (_listBNKB.Count > 0)
                            idmax = Convert.ToInt32(_listBNKB.First().IDKB.ToString());
                        if (id < idmax)
                        {
                            EnableControlKB(false);
                            _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 21-grvChuyenKhoa_FocusedRowChanged -EnableControlKB-  5067   ";
                            grvDonThuocct.OptionsBehavior.ReadOnly = true;
                            lupKhoXuat.Properties.ReadOnly = true;
                        }
                        else
                        {
                            //
                            EnableControlKB(true);
                            _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 22-grvChuyenKhoa_FocusedRowChanged -EnableControlKB-  5075   ";
                            grvDonThuocct.OptionsBehavior.ReadOnly = false;
                            lupKhoXuat.Properties.ReadOnly = false;
                        }
                    }
                    else
                    {
                        EnableControlKB(false);
                        _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 23-grvChuyenKhoa_FocusedRowChanged -EnableControlKB-  5083   ";
                        grvDonThuocct.OptionsBehavior.ReadOnly = true;
                        lupKhoXuat.Properties.ReadOnly = true;
                        _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 24-grvChuyenKhoa_FocusedRowChanged -lupKhoXuat-  5086    ";
                    }
                }
                else
                {
                    EnableControlKB(false);
                    _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 25-grvChuyenKhoa_FocusedRowChanged -EnableControlKB-  5092   ";
                    grvDonThuocct.OptionsBehavior.ReadOnly = true;
                    lupKhoXuat.Properties.ReadOnly = true;
                    _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 26-grvChuyenKhoa_FocusedRowChanged -lupKhoXuat-  5095    ";
                }
                _idkb = id;
            }
            #endregion
            else
            {//Khám bệnh
                EnableControlKB(true);
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 27-grvChuyenKhoa_FocusedRowChanged -EnableControlKB-  5257   ";
                ResetControlKB();
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 28-grvChuyenKhoa_FocusedRowChanged -ResetControlKB-  5259    ";
                grvDonThuocct.OptionsBehavior.ReadOnly = false;
                lupKhoXuat.Properties.ReadOnly = false;
                lupChanDoanKb.EditValue = "";
                lupKhoaKhamkb_EditValueChanged(null, null);
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 29-grvChuyenKhoa_FocusedRowChanged -lupKhoaKhamkb_EditValueChanged-  5264    ";
                if (String.IsNullOrEmpty(txtMaBNhan.Text))
                    lup_ChuyenKhoa.EditValue = -1;
                lupKhoaKhamkb.EditValue = DungChung.Bien.MaKP;
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 30-grvChuyenKhoa_FocusedRowChanged -lupKhoaKhamkb_EditValueChanged-  5268    ";
                lupNguoiKhamkb.EditValue = DungChung.Bien.MaCB;
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 31-grvChuyenKhoa_FocusedRowChanged -lupNguoiKhamkb-  5270    ";
                if (!string.IsNullOrEmpty(macbkb))
                    lupNguoiKhamkb.EditValue = macbkb;
                _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 32-grvChuyenKhoa_FocusedRowChanged -lupNguoiKhamkb-  5273    ";

                _idkb = 0;
            }
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
        }

        private void grvChuyenKhoa_DataSourceChanged(object sender, EventArgs e)
        {
            grvChuyenKhoa_FocusedRowChanged(null, null);
        }

        private void btnkbMoi_Click(object sender, EventArgs e)
        {

        }

        private void grvChuyenKhoa_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (e.Column.Name == "colXoaKB")
            {
                if (!DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                {
                    // if (tbKB.Rows.Count > 1)
                    if (_listBNKB.Count > 1)
                    {
                        int makp = 0;
                        int makptim = 0;
                        if (grvChuyenKhoa.GetFocusedRowCellValue(colMaKPkham) != null)
                            makp = Convert.ToInt32(grvChuyenKhoa.GetFocusedRowCellValue(colMaKPkham));
                        if (lupTimMaKP.EditValue != null)
                            makptim = Convert.ToInt32(lupTimMaKP.EditValue);
                        if (makp == makptim)
                        {
                            int id2 = 0;
                            if (grvChuyenKhoa.GetFocusedRowCellValue(colID) != null)
                                id2 = Convert.ToInt32(grvChuyenKhoa.GetFocusedRowCellValue(colID));
                            if (id2 > 0)
                            {
                                #region ADO
                                //try
                                //{

                                //    string deleFile = "DELETE dbo.BNKB WHERE IDKB = '" + id2 + "'";
                                //    connect.ExecuteNonQuery(deleFile, CommandType.Text);
                                //    Ham._setStatus(_int_maBN, 0);
                                //    usKhamBenh_Load(sender, e);
                                //    grcBNhankb.Refresh();
                                //    //tbKB = connect.FillDatatable("SELECT * FROM dbo.BNKB WHERE MaBNhan = " + _int_maBN + " ORDER BY IDKB desc", CommandType.Text);
                                //    //grcChuyenKhoa.DataSource = tbKB;
                                _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                //    _listBNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                //    grcChuyenKhoa.DataSource = _listBNKB;

                                //}
                                //catch (Exception ex)
                                //{
                                //    MessageBox.Show(ex.Message);
                                //}
                                #endregion

                                #region linq
                                DialogResult result2 = DialogResult.Yes;
                                //result = MessageBox.Show("Bạn muốn xóa khám bệnh của BN: " + txtTenBenhNhan.Text, "xóa khám bệnh!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result2 == DialogResult.Yes)
                                {
                                    var kb = _dataContext.BNKBs.Single(p => p.IDKB == id2);
                                    _dataContext.BNKBs.Remove(kb);
                                    if (_dataContext.SaveChanges() >= 0)
                                    {
                                        Ham._setStatus(_int_maBN, 0);
                                        usKhamBenh_Load(sender, e);
                                        grcBNhankb.Refresh();
                                        _listBNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                        grcChuyenKhoa.DataSource = _listBNKB;
                                    }
                                }

                                #endregion
                            }
                        }

                        else
                        {
                            MessageBox.Show("Bạn không thể xóa khám bệnh của phòng khám khác");
                        }
                    }
                    else
                    {
                        MessageBox.Show("BN đã có dịch vụ hoặc thuốc, bạn không thể xóa kb");
                    }
                }
                else
                {
                    MessageBox.Show("BN đã thanh toán, bạn không được xóa");
                }
            }
        }
        #region InPhieuKCB
        public void InPhieuKCB(int _int_maBN)
        {

            try
            {

                string maICD = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                string maICD2 = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                bool ktIn = true;
                if (String.IsNullOrEmpty(maICD) && String.IsNullOrEmpty(maICD2)) //(String.IsNullOrEmpty(lupMaICDkb.Text) && String.IsNullOrEmpty(cboMaICD2.Text))
                {

                    DialogResult _result = MessageBox.Show("Bệnh nhân chưa có mã bệnh, bạn có muốn in phiếu không?", "thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.No)
                    {
                        ktIn = false;
                    }
                }

                if (ktIn)
                {
                    frmIn frm = new frmIn();
                    List<ChiDinh> _lcd = new List<ChiDinh>();
                    List<int> qcls = _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN).Select(p => p.IdCLS).ToList();
                    if (qcls != null)
                        _lcd = _dataContext.ChiDinhs.Where(p => qcls.Contains((int)p.IdCLS)).ToList();
                    //_lcd = (from cls in _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN)
                    //        join cdi in _dataContext.ChiDinhs on cls.IdCLS equals cdi.IdCLS
                    //        select cdi).ToList();
                    BenhNhan benhnhan = _dataContext.BenhNhans.Single(p => p.MaBNhan == _int_maBN);

                    var khambenh = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                    var canbo = _dataContext.CanBoes.ToList();
                    var kphong = _dataContext.KPhongs.ToList();

                    var tt = (from kb in khambenh
                              join cb in canbo on kb.MaCB equals cb.MaCB
                              join kp in kphong on kb.MaKP equals kp.MaKP
                              select new
                              {
                                  kb.NgayKham,
                                  kp.TenKP,
                                  cb.CapBac,
                                  cb.TenCB,
                                  kb.ChanDoan,
                                  kb.BenhKhac,
                                  kb.IDKB,
                                  kb.MaICD
                              }).OrderByDescending(p => p.IDKB).ToList();

                    var qdthuoc1 = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).ToList();
                    List<int> lIddont = qdthuoc1.Select(p => p.IDDon).ToList();
                    var qdthuoc2 = _dataContext.DThuoccts.Where(p => lIddont.Contains(p.IDDon ?? 0)).ToList();
                    var qdv = (from dv in _dataContext.DichVus
                               join nhomdv in _dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                               select new { nhomdv.TenNhom, nhomdv.STT, dv.TenDV, dv.MaDV, dv.TenHC, dv.DonVi }).ToList();
                    var q2 = (from dt in qdthuoc1
                              join dtct in qdthuoc2 on dt.IDDon equals dtct.IDDon
                              join dv in qdv on dtct.MaDV equals dv.MaDV
                              select new { dv.TenNhom, dv.STT, dv.TenDV, dv.MaDV, dv.TenHC, dv.DonVi, dtct.DonGia, dtct.IDCD, dtct.IDDonct, dtct.TrongBH, dtct.SoLuong, dtct.ThanhTien, dt.NgayKe, dtct.MaKXuat, dt.PLDV }).ToList();

                    //var q2 = (from  dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN) 
                    //         join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                    //         join  dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                    //         join nhomdv in _dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom                            
                    //         select new { nhomdv.TenNhom, nhomdv.STT, dv.TenDV, dv.MaDV, dv.TenHC, dv.DonVi, dtct.DonGia, dtct.IDCD,dtct.IDDonct, dtct.TrongBH , dtct.SoLuong, dtct.ThanhTien, dt.NgayKe})                             
                    //        .OrderBy(p => p.STT).ThenBy(p => p.IDDonct).ToList();


                    var q = (from dt in q2
                             group dt by new { dt.TenNhom, dt.STT, dt.TenDV, dt.MaDV, dt.TenHC, dt.DonVi, dt.DonGia, dt.IDCD, dt.TrongBH, dt.MaKXuat, dt.PLDV } into kq
                             select new
                             {
                                 kq.Key.TrongBH,
                                 idmin = kq.Min(p => p.IDDonct),
                                 kq.Key.IDCD,
                                 kq.Key.TenNhom,
                                 kq.Key.STT,
                                 TenDV = (kq.Key.TenHC != null && kq.Key.TenHC != "" && DungChung.Bien.MaBV == "30012") ? kq.Key.TenHC + " (" + kq.Key.TenDV + ")" : kq.Key.TenDV,
                                 kq.Key.MaDV,
                                 kq.Key.DonVi,
                                 kq.Key.DonGia,
                                 SoLuong = kq.Sum(p => p.SoLuong),
                                 ThanhTien = kq.Sum(p => p.ThanhTien),
                                 kq.Key.MaKXuat,
                                 kq.Key.PLDV
                             }).OrderBy(p => p.STT).ThenBy(p => p.idmin).ToList();
                    if (DungChung.Bien.MaBV == "30281" || DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "30340" || DungChung.Bien.MaBV == "30350")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            var ds = q.Where(p => p.TrongBH == i).ToList();
                            if (ds.Count <= 0)
                                continue;
                            BaoCao.rep_PhieuKCBNgoaiT_A4 rep = new BaoCao.rep_PhieuKCBNgoaiT_A4(_lcd);
                            string ngphat = "";
                            if (q2.Count > 0)
                            {
                                if (q2.First().NgayKe.Value.Day > 0)
                                {
                                    rep.ngayke.Value = DungChung.Ham.NgaySangChu(q2.First().NgayKe.Value);
                                }
                                if (q2.Where(p => p.PLDV == 1).ToList().Count() > 0)
                                {
                                    int makpx = Convert.ToInt32(lupKhoXuat.EditValue);
                                    var kpx = kphong.Where(p => p.MaKP == makpx).FirstOrDefault();
                                    if (kpx != null)
                                        ngphat = kpx.NguoiPhat;
                                }
                            }
                            rep._TenBNhan.Value = txtTenBenhNhan.Text.Trim();
                            rep.NguoiPhat.Value = ngphat;
                            //rep._idDon.Value = ktkd.First().IDDon;
                            if (DungChung.Bien.MaBV == "04011")
                            {
                                rep.NguoiPhat.Value = "";
                                rep.NguoiPhat.Value = "Kế toán viện phí";
                            }
                            if (DungChung.Bien.MaBV == "30009")
                            {
                                rep.NguoiPhat.Value = DungChung.Bien.ThuKho;
                            }

                            rep.ICD.Value = maICD;
                            rep.ChanDoan.Value = maICD2;
                            if (tt.Count > 0)
                            {
                                rep.TenCB.Value = tt.First().CapBac + ": " + tt.First().TenCB;
                                rep.Ngaykham.Value = tt.First().NgayKham.ToString().Substring(0, 10);
                                rep.TenKP.Value = tt.First().TenKP;
                                string _ngay = "", _thang = "";
                                if (!string.IsNullOrEmpty(benhnhan.NgaySinh))
                                    _ngay = benhnhan.NgaySinh + "/";
                                if (!string.IsNullOrEmpty(benhnhan.ThangSinh))
                                    _thang = benhnhan.ThangSinh + "/";
                                rep.Tuoi.Value = _ngay + _thang + benhnhan.NamSinh;

                                switch (benhnhan.GTinh)
                                {
                                    case 1:
                                        rep.GTinh.Value = "Nam";
                                        break;
                                    case 0:
                                        rep.GTinh.Value = "Nữ";
                                        break;
                                }
                                if (benhnhan.HanBHDen != null && benhnhan.HanBHDen.Value.Day > 0)
                                    rep.HanDen.Value = benhnhan.HanBHDen.ToString().Substring(0, 10);
                                if (benhnhan.HanBHTu != null && benhnhan.HanBHTu.Value.Day > 0)
                                    rep.HanTu.Value = benhnhan.HanBHTu.ToString().Substring(0, 10);
                                rep.MaCS.Value = benhnhan.MaCS;
                                rep.SThe.Value = benhnhan.SThe;

                                rep.DiaChi.Value = benhnhan.DChi;
                                DateTime _ngaynhap = benhnhan.NNhap.Value;
                                rep.SoPhieu.Value = "Phiếu số: " + benhnhan.SoTT;
                                rep._MaBNhan.Value = txtMaBNhan.Text;

                                // lấy mã KCB ban đầu
                                var madkkcb = (from bv in _dataContext.BenhViens.Where(p => p.MaBV == benhnhan.MaCS)
                                               select new { bv.TenBV }).ToList();
                                if (madkkcb.Count > 0)
                                    rep.dkkcbbd.Value = madkkcb.First().TenBV;
                            }

                            if (i == 1)
                            {
                                rep.lblsudung.Text = "(Chi phí trong danh mục BHYT)";
                            }
                            else if (i == 0)
                                rep.lblsudung.Text = "(Chi phí ngoài danh mục BHYT)";
                            else
                                rep.lblsudung.Text = "(Chi phí không thanh toán)";
                            rep.DataSource = ds;
                            rep.BindData();
                            rep.CreateDocument();
                            frm.prcIN.PrintingSystem = rep.PrintingSystem;
                            frm.ShowDialog();
                        }
                        return;
                    }
                    for (int i = 0; i < 3; i++)
                    {

                        if (DungChung.Bien.MaBV == "30003")
                        {
                            if (i > 0)
                                break;
                        }
                        else
                        {
                            var ds = q.Where(p => p.TrongBH == i).ToList();
                            if (ds.Count <= 0)
                                continue;
                        }
                        BaoCao.rep_PhieuKCBNgoaiT rep2 = new BaoCao.rep_PhieuKCBNgoaiT(_lcd);
                        if (q2.Count > 0)
                        {
                            if (q2.First().NgayKe.Value.Day > 0)
                            {
                                rep2.ngayke.Value = DungChung.Ham.NgaySangChu(q2.First().NgayKe.Value);
                            }
                        }
                        rep2._TenBNhan.Value = txtTenBenhNhan.Text.Trim();

                        //rep._idDon.Value = ktkd.First().IDDon;

                        rep2.ICD.Value = maICD;
                        rep2.ChanDoan.Value = maICD2;
                        if (tt.Count > 0)
                        {
                            rep2.TenCB.Value = tt.First().CapBac + ": " + tt.First().TenCB;
                            rep2.Ngaykham.Value = tt.First().NgayKham.ToString().Substring(0, 10);
                            rep2.TenKP.Value = tt.First().TenKP;
                            string _ngay = "", _thang = "";
                            if (!string.IsNullOrEmpty(benhnhan.NgaySinh))
                                _ngay = benhnhan.NgaySinh + "/";
                            if (!string.IsNullOrEmpty(benhnhan.ThangSinh))
                                _thang = benhnhan.ThangSinh + "/";
                            rep2.Tuoi.Value = _ngay + _thang + benhnhan.NamSinh;

                            switch (benhnhan.GTinh)
                            {
                                case 1:
                                    rep2.GTinh.Value = "Nam";
                                    break;
                                case 0:
                                    rep2.GTinh.Value = "Nữ";
                                    break;
                            }
                            int makpx = Convert.ToInt32(lupKhoXuat.EditValue); ;
                            var kpx = kphong.Where(p => p.MaKP == makpx).FirstOrDefault();
                            if (kpx != null)
                                rep2.colNguoiPhat_kt.Text = kpx.NguoiPhat;
                            if (benhnhan.HanBHDen != null && benhnhan.HanBHDen.Value.Day > 0)
                                rep2.HanDen.Value = benhnhan.HanBHDen.ToString().Substring(0, 10);
                            if (benhnhan.HanBHTu != null && benhnhan.HanBHTu.Value.Day > 0)
                                rep2.HanTu.Value = benhnhan.HanBHTu.ToString().Substring(0, 10);
                            rep2.MaCS.Value = benhnhan.MaCS;
                            rep2.SThe.Value = benhnhan.SThe;
                            rep2.DiaChi.Value = benhnhan.DChi;
                            DateTime _ngaynhap = benhnhan.NNhap.Value;
                            rep2.SoPhieu.Value = "Phiếu số: " + benhnhan.SoTT;
                            rep2._MaBNhan.Value = txtMaBNhan.Text;

                            // lấy mã KCB ban đầu
                            var madkkcb = (from bv in _dataContext.BenhViens.Where(p => p.MaBV == benhnhan.MaCS)
                                           select new { bv.TenBV }).ToList();
                            if (madkkcb.Count > 0)
                                rep2.dkkcbbd.Value = madkkcb.First().TenBV;
                        }
                        if (DungChung.Bien.MaBV != "30003")
                        {
                            if (i == 1)
                            {
                                rep2.lblsudung.Text = "(Chi phí trong danh mục BHYT)";
                            }
                            else if (i == 0)
                                rep2.lblsudung.Text = "(Chi phí ngoài danh mục BHYT)";
                            else
                                rep2.lblsudung.Text = "(Chi phí không thanh toán)";
                        }
                        rep2.DataSource = q.Where(p => DungChung.Bien.MaBV == "30003" ? true : p.TrongBH == i).ToList();
                        //}
                        rep2.BindData();
                        rep2.CreateDocument();
                        frm.prcIN.PrintingSystem = rep2.PrintingSystem;

                        frm.ShowDialog();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không in được phiếu:" + ex.Message);
            }

        }


        #endregion
        private void btnInPKCB_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            try
            {
                InPhieuKCB(_int_maBN);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không in được phiếu:" + ex.Message);
            }
        }

        private void IndonTuyenQuang()
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            try
            {
                frmIn frm = new frmIn();
                BaoCao.rep_DonThuoc_TQuang rep = new BaoCao.rep_DonThuoc_TQuang();
                var ktkd = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN)
                            join cb in _dataContext.CanBoes on dt.MaCB equals cb.MaCB
                            select new { dt.PLDV, dt.GhiChu, dt.IDDon, dt.KieuDon, dt.LoaiDuoc, dt.MaBNhan, dt.NgayKe, cb.TenCB }).ToList().OrderBy(p => p.PLDV).ToList();
                if (ktkd.Count > 0)
                {


                    if (ktkd.First().NgayKe.Value.Day > 0)
                    {

                        rep.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                    }
                }
                rep._TenBNhan.Value = txtTenBenhNhan.Text.Trim();

                //rep._idDon.Value = ktkd.First().IDDon;


                var tt = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                          join kb in _dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                          join cb in _dataContext.CanBoes on kb.MaCB equals cb.MaCB
                          join kp in _dataContext.KPhongs on kb.MaKP equals kp.MaKP
                          select new { kb.NgayKham, cb.CapBac, kp.TenKP, bn.SoTT, bn.GTinh, bn.MaCS, bn.NamSinh, cb.TenCB, bn.HanBHDen, bn.HanBHTu, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi }).OrderByDescending(p => p.IDKB).ToList();
                if (tt.Count > 0)
                {
                    rep.TenCB.Value = tt.First().CapBac + tt.First().TenCB;
                    rep.Ngaykham.Value = DungChung.Ham.NgaySangChu(tt.First().NgayKham.Value);
                    rep.TenKP.Value = tt.First().TenKP;
                    rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_dataContext, _int_maBN, DungChung.Bien.formatAge);

                    switch (tt.First().GTinh)
                    {
                        case 1:
                            rep.GTinh.Value = "Nam";
                            break;
                        case 0:
                            rep.GTinh.Value = "Nữ";
                            break;
                    }
                    if (tt.First().HanBHDen != null && tt.First().HanBHDen.Value.Day > 0)
                        rep.HanDen.Value = tt.First().HanBHDen.ToString().Substring(0, 10);
                    if (tt.First().HanBHTu != null && tt.First().HanBHTu.Value.Day > 0)
                        rep.HanTu.Value = tt.First().HanBHTu.ToString().Substring(0, 10);
                    rep.MaCS.Value = tt.First().MaCS;
                    rep.ICD.Value = tt.First().MaICD;
                    rep.SThe.Value = tt.First().SThe;
                    rep.ChanDoan.Value = tt.First().ChanDoan + tt.First().BenhKhac;

                    rep.DiaChi.Value = tt.First().DChi;
                    DateTime _ngaynhap = tt.First().NNhap.Value;
                    //var lSoPhieu = _dataContext.BenhNhans.Where(p => p.NNhap == _ngaynhap).ToList();
                    //int maxid = lSoPhieu.Max(p => p.MaBNhan);
                    //int minid
                    rep.SoPhieu.Value = "Phiếu số: " + tt.First().SoTT;
                    rep._MaBNhan.Value = txtMaBNhan.Text;

                    // lấy mã KCB ban đầu
                    var madkkcb = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                                   join bv in _dataContext.BenhViens on bn.MaCS equals bv.MaBV
                                   select new { bv.TenBV }).ToList();
                    if (madkkcb.Count > 0)
                        rep.dkkcbbd.Value = madkkcb.First().TenBV;
                }
                //int id = ktkd.First().IDDon;
                var q = (from dv in _dataContext.DichVus
                         join dtct in _dataContext.DThuoccts on dv.MaDV equals dtct.MaDV
                         join dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN) on dtct.IDDon equals dt.IDDon
                         join nhomdv in _dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                         //where (dtct.IDDon == id)
                         group new { dv, dtct, nhomdv } by new { nhomdv.TenNhom, nhomdv.STT, dv.TenDV, dv.MaDV, dv.DonVi, dtct.DonGia, dt.PLDV, dtct.DuongD, dtct.SoLan, dtct.MoiLan, dtct.Luong, dtct.DviUong } into kq
                         select new { kq.Key.TenNhom, kq.Key.STT, kq.Key.TenDV, kq.Key.MaDV, kq.Key.PLDV, kq.Key.DonVi, kq.Key.DonGia, SoLuong = kq.Sum(p => p.dtct.SoLuong), ThanhTien = kq.Sum(p => p.dtct.ThanhTien), HuongDan = kq.Key.DuongD + kq.Key.SoLan + kq.Key.MoiLan + kq.Key.Luong + kq.Key.DviUong }).OrderBy(p => p.STT).OrderBy(p => p.TenDV).ToList();
                //HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.DviUong 
                //var q = (from dv in _dataContext.DichVus
                //         join dtct in _dataContext.DThuoccts on dv.MaDV equals dtct.MaDV
                //         where (dtct.IDDon == id)
                //         group new { dv, dtct } by new { dv.TenDV, dv.MaDV, dtct.DonVi } into kq
                //         select new { kq.Key.TenDV, kq.Key.MaDV, kq.Key.DonVi, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).ToList();
                if (q.Count > 0)
                    rep.DataSource = q.ToList();
                //rep.ShowPreviewDialog();
                //rep.DataMember = "Table";
                rep.BindData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không in được đơn:" + ex.Message);
            }
        }




        private void grvchiDinh_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == colGia2)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
        public static void InDonGN_HTT(int _idDon, string _tentn)
        {
            QLBVEntities _dataContext = new QLBVEntities(Bien.StrCon);
            frmIn frm = new frmIn();
            var ktkd = (from dt in _dataContext.DThuocs.Where(p => p.IDDon == _idDon)
                        join cb in _dataContext.CanBoes on dt.MaCB equals cb.MaCB
                        join kp in _dataContext.KPhongs on dt.MaKP equals kp.MaKP
                        select new { dt.GhiChu, dt.IDDon, dt.KieuDon, dt.LoaiDuoc, dt.MaBNhan, dt.NgayKe, dt.PLDV, cb.TenCB, cb.CapBac, kp.TenKP }).ToList();

            if (ktkd.Count > 0)// kiểm tra có đơn thuốc hay chưa
            {
                //if (DungChung.Bien.MaBV == "08602") // Na hang ( tùng y/c ngày 05/05/16)- thông tư 05
                //{

                #region in mẫu mới
                int _int_maBN = ktkd.First().MaBNhan ?? 0;
                var ttd = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                           join kb in _dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                           join ttbs in _dataContext.TTboXungs on bn.MaBNhan equals ttbs.MaBNhan
                           select new { ttbs.CMT, bn.GTinh, bn.TenBNhan, bn.NamSinh, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi, kb.GhiChu, kb.NguoiNhanThuoc, kb.SoCMNDNguoiNhanThuoc, ttbs.SoKSinh }).OrderByDescending(p => p.IDKB).ToList();

                var qd1 = (from dv in _dataContext.DichVus
                           join dtct in _dataContext.DThuoccts.Where(p => p.TrongBH != 2) on dv.MaDV equals dtct.MaDV
                           join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           where (dtct.IDDon == _idDon && tn.TenRG == _tentn)
                           select new { tn.TenRG, TenDV = (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009" || DungChung.Bien.MaBV == "01830") ? dv.TenRG : dv.TenDV, dv.MaDV, dv.DonVi, dtct.SoLuong, dtct.IDDonct, HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.Luong + dtct.DviUong }).OrderBy(p => p.IDDonct).ToList();
                for (int i = 1; i < 4; i++)
                {
                    BaoCao.repDonThuoc_TT05_N repd = new BaoCao.repDonThuoc_TT05_N();
                    if (_tentn == "Thuốc gây nghiện")
                        repd.labTieuDe.Text = "ĐƠN THUỐC \"N\"";
                    else
                    {
                        repd.xrLabel5.Visible = false;
                        repd.xrLabel7.Visible = false;
                        repd.labTieuDe.Text = "ĐƠN THUỐC \"H\"";
                    }

                    repd._idDon.Value = ktkd.First().IDDon;
                    if (ttd.Count > 0)
                    {
                        string _ghichu = ttd.First().GhiChu ?? "";
                        string[] ar = _ghichu.Split(';');
                        if (ar.Length > 2)
                            repd.paraDotDieuTri.Value = ar[2];
                        repd._TenBNhan.Value = ttd.First().TenBNhan;
                        repd.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(_dataContext, _int_maBN) : DungChung.Ham.TuoitheoThang(_dataContext, _int_maBN, DungChung.Bien.formatAge);
                        // KT
                        switch (ttd.First().GTinh)
                        {
                            case 1:
                                repd.GTinh.Value = "Nam";
                                repd.Nu.Value = "/";
                                break;
                            case 0:
                                repd.GTinh.Value = "Nữ";
                                repd.Nam.Value = "/";
                                break;
                        }
                        repd.ICD.Value = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                        repd.SThe.Value = ttd.First().SThe;
                        repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                        repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                        repd.TenKP.Value = ktkd.First().TenKP;
                        repd.DiaChi.Value = ttd.First().DChi;
                        repd._MaBNhan.Value = _int_maBN.ToString();
                        if (ktkd.First().NgayKe.Value.Day > 0)
                            repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                    }
                    repd.DataSource = qd1.ToList();
                    repd.ThuKho.Value = DungChung.Bien.ThuKho;
                    if (i == 1)
                    {
                        repd.lblLien.Text = "(Bản lưu tại cơ sở khám bệnh, chữa bệnh)";
                        repd.lblNguoiNhanThuoc.Text = "";
                        repd.txtbnky.Text = "";
                        repd.txttenBN.Text = "";
                        repd.tt.Text = "";
                    }
                    else if (i == 2)
                    {
                        repd.lblLien.Text = "(Bản lưu tại cơ sở cấp, bán thuốc)";
                        repd.lblNguoiNhanThuoc.Text = "Người nhận thuốc";
                        repd.txtbnky.Text = "Ký, ghi rõ họ tên và số chứng minh nhân dân";

                        if (DungChung.Bien.MaBV == "34019" && !string.IsNullOrEmpty(ttd.First().NguoiNhanThuoc))
                            repd.txttenBN.Text = ttd.First().NguoiNhanThuoc;
                        else
                            repd.txttenBN.Text = ttd.First().TenBNhan;

                        if (DungChung.Bien.MaBV == "34019")
                        {
                            repd.tt.Text = "Số CMND: " + (!string.IsNullOrEmpty(ttd.First().SoCMNDNguoiNhanThuoc) ? ttd.First().SoCMNDNguoiNhanThuoc : ttd.First().SoKSinh);
                            repd.tt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            repd.tt.Font = repd.txtbnky.Font;
                        }
                        else if (DungChung.Bien.MaBV != "30007")
                            repd.tt.Text = "Hạn sử dụng của thuốc";
                        if (DungChung.Bien.MaBV == "27001")
                        {
                            repd.xrTableCell8.Visible = true;
                            repd.tt.Visible = true;
                            repd.cmnd.Value = ttd.First().CMT;
                        }
                    }
                    else if (i == 3)
                    {
                        repd.lblLien.Text = "(Bản giao cho người bệnh)";
                        repd.lblNguoiNhanThuoc.Text = "";
                        repd.txtbnky.Text = "";
                        repd.txttenBN.Text = "";
                        repd.tt.Text = "";
                    }
                    repd.BindData();
                    repd.CreateDocument();
                    frm.prcIN.PrintingSystem = repd.PrintingSystem;
                    frm.ShowDialog();

                }
                #endregion


            }
        }
        private void btn_IndonGN_HTT_Click(object sender, EventArgs e)
        {
            int rs;
            int _int_IDDon = 0;
            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _int_IDDon = Convert.ToInt32(txtIdDonThuoc.Text);
            string _kieuDon = "Thuốc gây nghiện";
            //InDonGN_HTT(_int_IDDon);
            //if (ktkd.Count > 0)// kiểm tra có đơn thuốc hay chưa
            //{
            //if (DungChung.Bien.MaBV == "08602") // Na hang ( tùng y/c ngày 05/05/16)- thông tư 05
            //{

            //}

            //else
            //{
            //    #region in mẫu cũ
            //    BaoCao.repDonThuocGN_HTT rep_ha = new BaoCao.repDonThuocGN_HTT();
            //    rep_ha._TenBNhan.Value = txtTenBenhNhan.Text.Trim();
            //    rep_ha._idDon.Value = ktkd.First().IDDon;
            //    var tt_ha = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
            //                 join kb in _dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
            //                 select new { bn.GTinh, bn.MaBNhan, bn.NamSinh, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi, bn.MaCS }).OrderBy(p => p.IDKB).ToList();
            //    if (tt_ha.Count > 0)
            //    {//kiểm tra tuồi <6 thì in tháng tuổi
            //        if (!string.IsNullOrEmpty(txtTuoi.Text))
            //        {
            //            int _tuoi = Convert.ToInt32(txtTuoi.Text);
            //            if (_tuoi <= 6)
            //            {
            //                int _ngays = 1;
            //                int _thangs = 1;
            //                int _nams = 1900;
            //                if (tt_ha.First().NgaySinh != null)
            //                    _ngays = Convert.ToInt32(tt_ha.First().NgaySinh);
            //                if (tt_ha.First().ThangSinh != null)
            //                    _thangs = Convert.ToInt32(tt_ha.First().ThangSinh);
            //                if (tt_ha.First().NamSinh != null)
            //                    _nams = Convert.ToInt32(tt_ha.First().NamSinh);
            //                //string _ngaysinh = _ngays + "/" + _thangs + "/" + _nams;
            //                //DateTime ngaysinh=System.DateTime.Now;
            //                //if (DateTime.TryParse(_ngaysinh, out ngaysinh)) { 
            //                //int thang=(tt.First().NNhap-ngaysinh).ye
            //                //}
            //                int nam = tt_ha.First().NNhap.Value.Year - _nams;
            //                int thangtuoi = 0;
            //                if (nam <= 0)
            //                    thangtuoi = (tt_ha.First().NNhap.Value.Month - _thangs);
            //                else
            //                    thangtuoi = (tt_ha.First().NNhap.Value.Month - _thangs) + 12 * nam;
            //                rep_ha.Tuoi.Value = thangtuoi + " tháng";
            //            }
            //            else
            //            {
            //                rep_ha.Tuoi.Value = txtTuoi.Text;
            //            }
            //            // KT
            //        }
            //        switch (tt_ha.First().GTinh)
            //        {
            //            case 1:
            //                rep_ha.GTinh.Value = "Nam";
            //                break;
            //            case 0:
            //                rep_ha.GTinh.Value = "Nữ";
            //                break;
            //        }
            //        //if (tt_ha.First().NoiTru == 2) {
            //        //    rep_ha.ChuyenVien.Value = "2";
            //        //}

            //        rep_ha.SThe.Value = tt_ha.First().SThe;
            //        rep_ha.ChanDoan.Value = tt_ha.First().ChanDoan + tt_ha.First().BenhKhac;
            //        rep_ha.TenCB.Value = ktkd.First().TenCB;
            //        rep_ha.TenKP.Value = ktkd.First().TenKP;
            //        rep_ha.DiaChi.Value = tt_ha.First().DChi;
            //        rep_ha._MaBNhan.Value = tt_ha.First().MaBNhan;
            //        if (ktkd.First().NgayKe.Value.Day > 0)
            //            rep_ha.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
            //    }
            //    int id_ha = ktkd.First().IDDon;
            //    var q_ha = (from dv in _dataContext.DichVus
            //                join dtct in _dataContext.DThuoccts on dv.MaDV equals dtct.MaDV
            //                where (dtct.IDDon == id_ha)
            //                select new { dv.TenDV, dv.MaDV, dv.DonVi, dtct.SoLuong, dtct.IDDonct, HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.Luong + dtct.DviUong }).OrderBy(p => p.IDDonct).ToList();

            //    if (q_ha.Count > 0)
            //        rep_ha.DataSource = q_ha.ToList();
            //    //rep.ShowPreviewDialog();
            //    //rep.DataMember = "Table";
            //    rep_ha.BindData();
            //    rep_ha.CreateDocument();
            //    frm.prcIN.PrintingSystem = rep_ha.PrintingSystem;
            //    frm.ShowDialog();
            //    #endregion
            //}

            //}
        }

        private void cboChuyenKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        private void chkKDNgoai_CheckedChanged(object sender, EventArgs e)
        {
            _changeBenhNhan = true;
            if (chkKDNgoai.Checked)
            {
                lupKhoXuat.Properties.ReadOnly = true;
                lupKhoXuat.EditValue = 1;
                lupKhoXuat.EditValue = 0;

            }
            else
            {
                lupKhoXuat.Properties.ReadOnly = false;
                lupKhoXuat.EditValue = 1;
                lupKhoXuat.EditValue = 0;
            }
            _changeBenhNhan = false;
        }

        private void chkKDNgoai_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (grvDonThuocct.RowCount > 0)
            {
                if (_click_ChkKDN == true)
                {
                    MessageBox.Show("\"Kê đơn ngoài\" để kê đơn cho B.Nhân không dùng thuốc của kho dược trong bệnh viện", "Thông báo kê đơn ngoài");
                    if (grvDonThuocct.RowCount == 1)
                    {

                        if (grvDonThuocct.GetRowCellValue(1, colIDThuoc) != null)
                        {
                            MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa");
                        e.Cancel = true;
                    }
                }
            }
            _click_ChkKDN = false;
        }

        private void chkKDNgoai_Click(object sender, EventArgs e)
        {
            _click_ChkKDN = true;
        }

        private void lupKhoXuat_Click(object sender, EventArgs e)
        {
            _click_khoKe = true;
        }

        private class LSKCB
        {
            private DateTime ngaynhap;
            private int mabn;
            private string ngayke;
            private string tendv;
            private string tencb;
            private string chandoan;
            private string donvi;
            private string soluong;
            private string huongdan;
            private string ketqua;
            private int ploai;
            public int PhanLoai
            {
                set { ploai = value; }
                get { return ploai; }
            }
            public DateTime NNhap
            { set { ngaynhap = value; } get { return ngaynhap; } }
            public int MaBNhan
            { set { mabn = value; } get { return mabn; } }
            public string NgayKe
            { set { ngayke = value; } get { return ngayke; } }
            public string TenDV
            { set { tendv = value; } get { return tendv; } }
            public string TenCB
            { set { tencb = value; } get { return tencb; } }
            public string ChanDoan
            { set { chandoan = value; } get { return chandoan; } }
            public string DonVi
            { set { donvi = value; } get { return donvi; } }
            public string SoLuong
            { set { soluong = value; } get { return soluong; } }
            public string HuongDan
            { set { huongdan = value; } get { return huongdan; } }
            public string KetQua
            { set { ketqua = value; } get { return ketqua; } }
        }

        private string _getTenCB(string _macb)
        {
            string _tencb = "";
            var qcb = _dataContext.CanBoes.Where(p => p.MaCB == _macb).FirstOrDefault();
            if (qcb != null)
                _tencb = qcb.TenCB;
            return _tencb;


            //string _tencb = "";
            //string sql = "select TenCB from CanBo where MaCB = '" + _macb + "'";
            //DataTable tb = connect.FillDatatable(sql, CommandType.Text);

            //if (tb.Rows.Count > 0)
            //    _tencb = tb.Rows[0]["TenCB"].ToString();
            //return _tencb;
        }
        #region lịch sử
        private void _xemLS(int _idP, int _mabn, bool ngaygannhat)
        {
            List<LSKCB> _BC = new List<LSKCB>();
            string _sthe = "";
            //if (!string.IsNullOrEmpty(txtIDPS.Text))
            //    _sthe = txtIDPS.Text;
            if (!String.IsNullOrEmpty(txtMaBNhan.Text))
                _mabn = Convert.ToInt32(txtMaBNhan.Text);
            else
            {
                return;
            }

            //DataTable ktid = connect.FillDatatable("select * from BenhNhan where MaBNhan = '" + _mabn + "'", CommandType.Text);

            //if (ktid.Rows.Count > 0 && !string.IsNullOrEmpty(ktid.Rows[0]["IDPerson"].ToString()))
            //    _idP = Convert.ToInt32(ktid.Rows[0]["IDPerson"].ToString());

            var qidp = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if (qidp != null && qidp.IDPerson != null)
            {
                _idP = qidp.IDPerson.Value;
                _sthe = qidp.SThe == null ? "" : qidp.SThe;
            }
            // _BC.Clear();
            DateTime _ngaygan = DateTime.Now;
            DataTable tbBC = new DataTable();

            if (DungChung.Bien.MaBV == "30007")
            {
                string[] strpara = new string[] { "@sothe", "@ngaygannhat" };
                object[] oValue = new object[] { _sthe, ngaygannhat };
                SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.VarChar, SqlDbType.Bit };
                connect.Connect();
                tbBC = connect.FillDatatable("sp_KB_xemLS_soThe", CommandType.StoredProcedure, strpara, oValue, sqlDBType);
            }
            else
            {
                string[] strpara = new string[] { "@idPerson", "@ngaygannhat" };
                object[] oValue = new object[] { _idP, ngaygannhat };
                SqlDbType[] sqlDBType = new SqlDbType[] { SqlDbType.Int, SqlDbType.Bit };
                connect.Connect();
                tbBC = connect.FillDatatable("sp_KB_xemLS", CommandType.StoredProcedure, strpara, oValue, sqlDBType);
            }
            Phieu.rep_lskcb rep = new Phieu.rep_lskcb();
            frmIn frm = new frmIn();
            rep.TenBN.Value = txtTenBenhNhan.Text.ToUpper();
            rep.DataSource = tbBC;
            rep.BindData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

            #region linq

            //  var ktid = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
            //  if (ktid.Count > 0 && ktid.First().IDPerson != null)
            //      _idP = ktid.First().IDPerson.Value;
            //  _BC.Clear();
            //  DateTime _ngaygan = DateTime.Now;
            //// var _lcb = _dataContext.CanBoes.ToList();
            //  var LS = (from bn in _dataContext.BenhNhans.Where(p => p.IDPerson == _idP)
            //            join dt in _dataContext.DThuocs.Where(p => p.PLDV == 1) on bn.MaBNhan equals dt.MaBNhan
            //            join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
            //            join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
            //            // join cb in _dataContext.CanBoes on dt.MaCB equals cb.MaCB
            //            join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
            //            select new { dt.MaCB, bn.NNhap, bn.MaBNhan, dt.NgayKe, dtct.MaDV, dv.TenDV, rv.ChanDoan, dtct.DonVi, dtct.SoLuong, HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.Luong + dtct.DviUong }).ToList().OrderBy(p => p.NNhap).ToList();
            //  if (LS.Count > 0)
            //      _ngaygan = LS.OrderByDescending(p => p.NNhap).ToList().First().NNhap.Value;
            //  //select new { bn.NNhap, bn.MaBNhan, dt.NgayKe, dtct.MaDV, dv.TenDV,  rv.ChanDoan, dtct.DonVi, dtct.SoLuong, HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.Luong + dtct.DviUong }).ToList().OrderBy(p => p.NNhap).ToList();
            //  var cls = (from bn in _dataContext.BenhNhans.Where(p => p.IDPerson == _idP)
            //             join cl in _dataContext.CLS on bn.MaBNhan equals cl.MaBNhan
            //             join cd in _dataContext.ChiDinhs.Where(p => p.Status == 1) on cl.IdCLS equals cd.IdCLS
            //             join clsct in _dataContext.CLScts.Where(p => p.Status == 1) on cd.IDCD equals clsct.IDCD
            //             join dvct in _dataContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
            //             join dv in _dataContext.DichVus on dvct.MaDV equals dv.MaDV
            //             join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
            //             select new { cl.MaCB, bn.NNhap, bn.MaBNhan, cl.NgayThang, dv.TenDV, rv.ChanDoan, clsct.KetQua, dvct.TenDVct }).ToList().OrderBy(p => p.NNhap).ToList();

            //  foreach (var a in LS)
            //  {
            //      LSKCB themmoi = new LSKCB();
            //      themmoi.PhanLoai = 1;
            //      themmoi.ChanDoan = a.ChanDoan;
            //      themmoi.DonVi = a.DonVi;
            //      themmoi.HuongDan = a.HuongDan;
            //      themmoi.MaBNhan = a.MaBNhan;
            //      themmoi.NNhap = a.NNhap.Value;
            //      themmoi.NgayKe = a.NgayKe.Value.ToShortDateString();
            //      themmoi.SoLuong = a.SoLuong.ToString();
            //      themmoi.TenCB = _getTenCB( a.MaCB);
            //      themmoi.TenDV = a.TenDV;
            //      _BC.Add(themmoi);
            //  }
            //  foreach (var b in cls)
            //  {
            //      LSKCB themmoi = new LSKCB();
            //      themmoi.PhanLoai = 2;
            //      themmoi.TenDV = b.TenDVct;
            //      themmoi.TenCB = _getTenCB(b.MaCB);
            //      themmoi.SoLuong = "0";
            //      themmoi.NgayKe = b.NgayThang.Value.ToShortDateString();
            //      themmoi.NNhap = b.NNhap.Value;
            //      themmoi.MaBNhan = b.MaBNhan;
            //      themmoi.KetQua = b.KetQua;
            //      themmoi.ChanDoan = b.ChanDoan;
            //      _BC.Add(themmoi);
            //  }
            //  _BC = _BC.OrderByDescending(p => p.NNhap).ThenBy(p => p.PhanLoai).ToList();
            //  Phieu.rep_lskcb rep = new Phieu.rep_lskcb();
            //  frmIn frm = new frmIn();
            //  rep.TenBN.Value = txtTenBenhNhan.Text.ToUpper();
            //  if (ngaygannhat)
            //      _BC = _BC.Where(p => p.NNhap == _ngaygan).ToList();
            //  rep.DataSource = _BC;
            //  rep.BindData();
            //  rep.CreateDocument();
            //  frm.prcIN.PrintingSystem = rep.PrintingSystem;
            //  frm.ShowDialog();
            #endregion
        }
        #endregion qu
        private void btnXemLS_Click(object sender, EventArgs e)
        {
            _xemLS(0, 0, false);
        }

        private void lupMaICD2_EditValueChanged(object sender, EventArgs e)
        {
            //if (lupMaICD2.EditValue != null && lupMaICD2.EditValue.ToString() != "") {
            //    txtBenhKhac.Text = lupMaICD2.Text;
            //}
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        public void getICD(string _maicd)
        {

            lupMaICDkb.EditValue = _maicd;
            txtBenhChinh.Text = _maicd;

        }
        public void getICD2(string _maicd)
        {

            LupICD2.EditValue = _maicd;
            //if (DungChung.Bien.MaBV != "30007" && DungChung.Bien.MaBV != "30340")
            //{
            string tenICD = lICD.Where(p => p.MaICD == _maicd).Select(p => p.TenICD).FirstOrDefault();
            if (string.IsNullOrEmpty(txtBenhKhac2.Text) && tenICD != null)
            {
                txtBenhKhac2.Text = tenICD;
                txtBenhPhu2.Text = tenICD;
            }


            //}

        }
        public void getICD3(string _maicd)
        {

            LupICD3.EditValue = _maicd;
            //if (DungChung.Bien.MaBV != "30007" && DungChung.Bien.MaBV != "30340")
            //{
            string tenICD = lICD.Where(p => p.MaICD == _maicd).Select(p => p.TenICD).FirstOrDefault();
            if (string.IsNullOrEmpty(txtBenhKhac3.Text) && tenICD != null)
            {
                txtBenhKhac3.Text = tenICD;
                txtBenhPhu3.Text = tenICD;
            }


            //}
        }
        private class LSICD
        {
            public string ICD { get; set; }
            public string TenICD { get; set; }
        }

        private List<LSICD> _lst = new List<LSICD>();

        public void getICDtest()
        {
            _lst.Clear();
            List<string> iCD = LupICD4.Text.Split(';').ToList();
            List<string> tenICD = new List<string>();
            foreach (var a in iCD)
            {
                LSICD item = new LSICD();
                item.ICD = a.ToString();
                _lst.Add(item);
            }
            foreach (var item in _lst)
            {
                if (!string.IsNullOrEmpty(item.ICD))
                {
                    string i = lICD.Where(p => p.MaICD == item.ICD).Select(p => p.TenICD).FirstOrDefault();
                    tenICD.Add(i);
                }
            }
            LupICD4.EditValue = string.Join(";", iCD);
            txtBenhKhac4.EditValue = string.Join(";", tenICD);
            txtBenhPhu.EditValue = string.Join(";", tenICD);
        }
        public void getICD4(string _maicd)
        {
            string a = "";
            isChonNhieuBenhKhac = true;
            if (!string.IsNullOrEmpty(LupICD4.Text.Trim()))
                a = ";";
            LupICD4.Text = "";
            LupICD4.Text += /*a +*/ _maicd;
            //if (DungChung.Bien.MaBV != "30007" && DungChung.Bien.MaBV != "30340")
            //{
            List<string> iCD = LupICD4.Text.Split(';').ToList();
            List<string> tenICD = new List<string>();
            foreach (var item in iCD)
            {
                string i = lICD.Where(p => p.MaICD == item).Select(p => p.TenICD).FirstOrDefault();
                tenICD.Add(i);
            }

            LupICD4.EditValue = LupICD4.ToolTip = string.Join(";", iCD);
            txtBenhKhac4.EditValue = txtBenhKhac4.ToolTip = string.Join(";", tenICD);
            txtBenhPhu.EditValue = txtBenhPhu.ToolTip = string.Join(";", tenICD);
            //}

        }
        public void getICDKhac(string _maicd)
        {

            lupKhac.EditValue = _maicd;

            //if (DungChung.Bien.MaBV != "30007" && DungChung.Bien.MaBV != "30340")
            //{
            string tenICD = lICD.Where(p => p.MaICD == _maicd).Select(p => p.TenICD).FirstOrDefault();
            if (string.IsNullOrEmpty(txtBenhKhac1.Text) && tenICD != null)
            {
                txtBenhKhac1.Text = tenICD;

            }


            //}
        }
        //public void getICDBD(string _maicd)
        //{
        //    string a = "";
        //    if (!string.IsNullOrEmpty(cboMaICDBĐ.Text.Trim()))
        //        a = ";";
        //    cboMaICDBĐ.Text += a + _maicd;
        //    //if (DungChung.Bien.MaBV != "30007" && DungChung.Bien.MaBV != "30340")
        //    //{
        //    string[] iCD = cboMaICDBĐ.Text.Split(';');
        //    string[] tenICD = lICD.Where(p => iCD.Contains(p.MaICD)).Select(p => p.TenICD).ToArray();

        //    txtBenhBĐ.Text = string.Join(";", tenICD);
        //    //}

        //}
        public void getICDphu(string _maicd)
        {
            //if (!string.IsNullOrEmpty(cboMaICD2.Text))
            //    cboMaICD2.Text += ";" + _maicd;
            //else
            //    cboMaICD2.Text = _maicd;
            ////string tenICD = lICD.Where(p => p.MaICD == _maicd).Select(p => p.TenICD).FirstOrDefault();
            ////if (!string.IsNullOrEmpty(txtBenhKhac.Text))
            ////    txtBenhKhac.Text += "; " + tenICD;
            ////else
            ////    txtBenhKhac.Text = tenICD;
        }
        private void btnTK_ICD_Click(object sender, EventArgs e)
        {
            FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
            frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD);
            frm.ShowDialog();
        }

        private void radGiaiQuyet_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_idkb.ToString() == txtIdkb.Text && KTGiaiQuyet(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), radGiaiQuyet.SelectedIndex) == false)
                e.Cancel = true;
        }

        private void grvchiDinh_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name != "colTrongBHdv")
            {
                int ma = 0;
                if (grvchiDinh.GetFocusedRowCellValue(colIDCD) != null && grvchiDinh.GetFocusedRowCellValue(colIDCD).ToString() != "")
                    ma = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colIDCD).ToString());
                if (ma > 0)
                {

                    MessageBox.Show("Dịch vụ đã được lưu kết quả CLS, bạn không thể sửa");
                    grvBNhankb_FocusedRowChanged(null, null);
                    //grvchiDinh.SetFocusedRowCellValue(e.Column.Name, grvchiDinh.GetFocusedRowCellValue(e.Column.Name));
                }
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {

            #region
            //int ktrv = 0;
            //if (ktrv == 0)
            //{
            //    var bnkb = _dataContext.BNKBs.Where(p => p.MaBNhan==_int_maBN).OrderByDescending(p => p.IDKB).ToList();
            //    string MaICD = "";
            //    string _chandoan = "", _chandoanfull = "";
            //    int j = 0;
            //    if (bnkb.Count > 0)
            //    {
            //        _chandoan = bnkb.First().ChanDoan + bnkb.First().BenhKhac;
            //        MaICD = bnkb.First().MaICD;
            //    }
            //    foreach (var i in bnkb)
            //    {
            //        if (j > 0)
            //            _chandoanfull += "/ ";
            //        _chandoanfull += i.ChanDoan;
            //        if (i.BenhKhac.Length > 0)
            //            _chandoanfull += "/ " + i.BenhKhac;
            //        j++;
            //    }
            //    RaVien _ravien = new RaVien();
            //    if (bnkb.Count > 0)
            //    {

            //        _ravien.MaKP = bnkb.First().MaKP;
            //        _ravien.MaICD = bnkb.First().MaICD;
            //        if (DungChung.Bien.MaBV == "24009")
            //        {
            //            _ravien.ChanDoan = _chandoanfull;
            //        }
            //        else
            //        {
            //            _ravien.ChanDoan = _chandoan;
            //        }
            //        if (bnkb.First().PhuongAn != null && bnkb.First().PhuongAn == 2)
            //            _ravien.Status = 1;
            //        else
            //            _ravien.Status = 2;
            //    }
            //    // kiểm tra lại số ngày điều trị
            //    _ravien.SoNgaydt = 1;
            //    _ravien.NgayRa = dtNgayKhamkb.DateTime;
            //    _ravien.MaBNhan = txtMaBNhan.Text;
            //    _dataContext.RaViens.Add(_ravien);
            //    _dataContext.SaveChanges();
            //}
            //else
            //{
            //    int _ntru = 0;
            //    var noitru = _dataContext.BenhNhans.Where(p => p.MaBNhan== (txtMaBNhan.Text)).Select(p => p.NoiTru).ToList();
            //    if (noitru.Count > 0)
            //    {
            //        _ntru = noitru.First().Value;
            //    }
            //    if (_ntru == 0)
            //    {
            //        var ravien = _dataContext.RaViens.Where(p => p.MaBNhan== (txtMaBNhan.Text)).ToList();
            //        if (ravien.Count > 0)
            //        {
            //            var _xoaravien = _dataContext.RaViens.Single(p => p.MaBNhan== (txtMaBNhan.Text));
            //            _dataContext.Remove(_xoaravien);
            //            _dataContext.SaveChanges();
            //        }
            //    }
            //}
            #endregion
        }

        private void btnTHthuthuat_Click(object sender, EventArgs e)
        {

        }

        private void grvLichSuKCB_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colxemls")
            {
                int _mabn = 0;
                if (grvLichSuKCB.GetFocusedRowCellValue(colMaBNhan_ls) != null)
                    _mabn = Convert.ToInt32(grvLichSuKCB.GetFocusedRowCellValue(colMaBNhan_ls));
                List<LSKCB> _BC = new List<LSKCB>();
                string _sthe = "";
                _BC.Clear();

                // _lcb = _dataContext.CanBoes.ToList();
                var LS = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn)
                          join dt in _dataContext.DThuocs.Where(p => p.PLDV == 1) on bn.MaBNhan equals dt.MaBNhan
                          join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                          join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                          join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                          select new { dt.MaCB, bn.NNhap, bn.MaBNhan, dt.NgayKe, dtct.MaDV, dv.TenDV, rv.ChanDoan, dtct.DonVi, dtct.SoLuong, HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.Luong + dtct.DviUong }).ToList().OrderBy(p => p.NNhap).ToList();
                //select new { bn.NNhap, bn.MaBNhan, dt.NgayKe, dtct.MaDV, dv.TenDV,  rv.ChanDoan, dtct.DonVi, dtct.SoLuong, HuongDan = dtct.DuongD + dtct.SoLan + dtct.MoiLan + dtct.Luong + dtct.DviUong }).ToList().OrderBy(p => p.NNhap).ToList();
                var cls = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn)
                           join cl in _dataContext.CLS on bn.MaBNhan equals cl.MaBNhan
                           join cd in _dataContext.ChiDinhs.Where(p => p.Status == 1) on cl.IdCLS equals cd.IdCLS
                           join clsct in _dataContext.CLScts.Where(p => p.Status == 1) on cd.IDCD equals clsct.IDCD
                           join dvct in _dataContext.DichVucts on clsct.MaDVct equals dvct.MaDVct
                           join dv in _dataContext.DichVus on dvct.MaDV equals dv.MaDV
                           join rv in _dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                           select new { cl.MaCB, bn.NNhap, bn.MaBNhan, cl.NgayThang, dv.TenDV, rv.ChanDoan, clsct.KetQua, dvct.TenDVct }).ToList().OrderBy(p => p.NNhap).ToList();

                foreach (var a in LS)
                {
                    LSKCB themmoi = new LSKCB();
                    themmoi.PhanLoai = 1;
                    themmoi.ChanDoan = a.ChanDoan;
                    themmoi.DonVi = a.DonVi;
                    themmoi.HuongDan = a.HuongDan;
                    themmoi.MaBNhan = a.MaBNhan;
                    themmoi.NNhap = a.NNhap.Value;
                    themmoi.NgayKe = a.NgayKe.Value.ToShortDateString();
                    themmoi.SoLuong = a.SoLuong.ToString();
                    themmoi.TenCB = _getTenCB(a.MaCB);
                    themmoi.TenDV = a.TenDV;
                    _BC.Add(themmoi);
                }
                foreach (var b in cls)
                {
                    LSKCB themmoi = new LSKCB();
                    themmoi.PhanLoai = 2;
                    themmoi.TenDV = b.TenDVct;
                    themmoi.TenCB = _getTenCB(b.MaCB);
                    themmoi.SoLuong = "0";
                    themmoi.NgayKe = b.NgayThang.Value.ToShortDateString();
                    themmoi.NNhap = b.NNhap.Value;
                    themmoi.MaBNhan = b.MaBNhan;
                    themmoi.KetQua = b.KetQua;
                    themmoi.ChanDoan = b.ChanDoan;
                    _BC.Add(themmoi);
                }
                _BC = _BC.OrderByDescending(p => p.NNhap).ThenBy(p => p.PhanLoai).ToList();
                Phieu.rep_lskcb rep = new Phieu.rep_lskcb();
                frmIn frm = new frmIn();
                rep.TenBN.Value = txtTenBenhNhan.Text.ToUpper();
                rep.DataSource = _BC;
                rep.BindData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
        private void PassData(int maDV)
        {

            if (grvDonThuocct.IsNewItemRow(grvDonThuocct.FocusedRowHandle) && grvDonThuocct.GetRow(grvDonThuocct.FocusedRowHandle) == null)
            {
                grvDonThuocct.AddNewRow();
            }
            grvDonThuocct.SetFocusedRowCellValue(colIDThuoc, maDV);
        }
        private void PassDataDV(int maDV)
        {

            if (grvchiDinh.IsNewItemRow(grvchiDinh.FocusedRowHandle) && grvchiDinh.GetRow(grvchiDinh.FocusedRowHandle) == null)
            {
                grvchiDinh.AddNewRow();
            }
            grvchiDinh.SetFocusedRowCellValue("MaDV", maDV);
        }
        private void usKhamBenh_KeyDown(object sender, KeyEventArgs e)
        {
            //FormThamSo.frm_DsMaDV frm = new FormThamSo.frm_DsMaDV();
            //frm.passMaDV = new FormThamSo.frm_DsMaDV.PassMaDV(PassData);
            //frm.ShowDialog();
            if (e.KeyCode == Keys.F9)
            {
                string maicd = LupICD4.Text;
                TraCuu.Frm_TimKiem_new frm = new TraCuu.Frm_TimKiem_new(maicd);
                frm.GetData = new TraCuu.Frm_TimKiem_new._getstring(getICD4);
                frm.ShowDialog();
            }
            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhPhu.Text != null || LupICD4.Text != null)
                {
                    txtBenhPhu.Text = null;
                    LupICD4.Text = null;
                }
            }
        }

        private void btnMoiBN_Click(object sender, EventArgs e)
        {
            if (xtraKhamBenh.Text.Contains("*") || xtraChiDinh.Text.Contains("*") || xtabDichVuCS2.Text.Contains("*"))
            {
                DialogResult _result = MessageBox.Show("Bạn chưa lưu dữ liệu, Bạn có muốn lưu không?", "thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    // btlLuuKb_Click(sender, e);
                    mnLuu_ItemClick(null, null);
                }
            }
            frmHSBNNhapMoi frm = new frmHSBNNhapMoi(0);
            frm.ShowDialog();
            TimKiem();
        }

        private void hpl_SoLanKham_Click(object sender, EventArgs e)
        {
            _xemLS(0, 0, false);
        }

        private void hpl_NgayKhamGan_Click(object sender, EventArgs e)
        {
            _xemLS(0, 0, true);
        }

        private void txtGhiChu_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }
        private void InPhieuLinhBNNgoaiTru(int idDon)
        {
            BaoCao.repPhieuPhatThuoc repd = new BaoCao.repPhieuPhatThuoc();
            var ktkd = (from dt in _dataContext.DThuocs.Where(p => p.IDDon == idDon).Where(p => p.PLDV == 1)
                        join dtct in _dataContext.DThuoccts.Where(p => DungChung.Bien.MaBV == "27023" ? true : p.TrongBH != 2) on dt.IDDon equals dtct.IDDon
                        join cb in _dataContext.CanBoes on dt.MaCB equals cb.MaCB
                        join kp in _dataContext.KPhongs on dt.MaKP equals kp.MaKP
                        select new { dt.GhiChu, dt.IDDon, cb.CapBac, dt.KieuDon, dt.LoaiDuoc, dt.MaBNhan, dt.NgayKe, dt.PLDV, cb.TenCB, kp.TenKP }).ToList();
            repd._idDon.Value = idDon;
            int _int_maBN = 0;
            if (ktkd.Count > 0 && ktkd.First().MaBNhan != null)
                _int_maBN = ktkd.First().MaBNhan ?? 0;
            var ttd = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                       join kb in _dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                       select new { bn.HanBHTu, bn.HanBHDen, bn.GTinh, bn.TenBNhan, bn.NamSinh, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi, kb.GhiChu }).OrderByDescending(p => p.IDKB).ToList();
            if (ttd.Count > 0)
            {
                repd._TenBNhan.Value = ttd.First().TenBNhan;
                repd.Tuoi.Value = DungChung.Ham.TuoitheoThang(_dataContext, _int_maBN, DungChung.Bien.formatAge);
                if (ttd.First().HanBHTu != null && ttd.First().HanBHTu.Value.Year > 2000)
                    repd.HanTu.Value = ttd.First().HanBHTu;
                if (ttd.First().HanBHDen != null && ttd.First().HanBHDen.Value.Year > 2000)
                    repd.HanDen.Value = ttd.First().HanBHDen;
                //Lời dặn, họ tên người thân
                string _ghichu = ttd.First().GhiChu ?? "";
                string[] ar = _ghichu.Split(';');
                if (ar.Length > 0)
                    repd.paraHoTenNguoiThan.Value = ar[0];
                if (ar.Length > 1)
                    repd.paraLoDanBS.Value = ar[1];
                // KT
                switch (ttd.First().GTinh)
                {
                    case 1:
                        repd.GTinh.Value = "Nam";
                        repd.Nu.Value = "/";
                        break;
                    case 0:
                        repd.GTinh.Value = "Nữ";
                        repd.Nam.Value = "/";
                        break;
                }
                repd.ICD.Value = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                repd.SThe.Value = ttd.First().SThe;
                repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];

                repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                repd.TenKP.Value = ktkd.First().TenKP;
                repd.DiaChi.Value = ttd.First().DChi;
                repd._MaBNhan.Value = _int_maBN;
                repd.xrBarCode1.Text = _int_maBN.ToString();
                if (ktkd.First().NgayKe.Value.Day > 0)
                    repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
            }

            var qd1 = (from tn in _dataContext.TieuNhomDVs
                       join dv in _dataContext.DichVus on tn.IdTieuNhom equals dv.IdTieuNhom
                       join dtct in _dataContext.DThuoccts.Where(p => DungChung.Bien.MaBV == "27023" ? true : p.TrongBH != 2) on dv.MaDV equals dtct.MaDV
                       where (dtct.IDDon == idDon)
                        && tn.TenRG != "Thuốc gây nghiện" && tn.TenRG != "Thuốc hướng tâm thần"
                       select new { dv.MaTam, TenDV = dv.TenDV, dv.HamLuong, dv.MaDV, dtct.DonGia, dtct.ThanhTien, dv.DonVi, dtct.SoLuong, dtct.IDDonct }).OrderBy(p => p.IDDonct).ToList();
            repd.DataSource = qd1.ToList();
            repd.ThuKho.Value = DungChung.Bien.ThuKho;
            repd.BindData();
            repd.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = repd.PrintingSystem;
            frm.ShowDialog();

        }



        private void mm_ghiChu_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;

        }


        private void grcDonThuocct_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                int makp = 0, makho = 0;
                if (lupKhoaKhamkb.EditValue != null)
                    makp = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                if (lupKhoXuat.EditValue != null)
                    makho = Convert.ToInt32(lupKhoXuat.EditValue);
                FormThamSo.frm_DsMaDV frm = new FormThamSo.frm_DsMaDV(makp, makho, 1);
                frm.passMaDV = new FormThamSo.frm_DsMaDV.PassMaDV(PassData);
                frm.ShowDialog();
            }
        }

        private void lupMaICDkb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD);
                frm.ShowDialog();

            }
        }

        private void lupChanDoanKb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void lupMaICD2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICDphu);
                frm.ShowDialog();

            }
        }

        private void txtGhiChu_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Cú pháp nhập: 'người thân';'Hướng dẫn đơn';'đợt kê đơn cho đơn HTT - GN'");
        }

        private void btnBNLao_Click(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void btnSaoDon_Click(object sender, EventArgs e)
        {

        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {

        }

        private void cboMaICD2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grcChiDinh_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                int makp = 0, makho = 0;
                if (lupKhoaKhamkb.EditValue != null)
                    makp = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                FormThamSo.frm_DsMaDV frm = new FormThamSo.frm_DsMaDV(makp, 0, 2);
                frm.passMaDV = new FormThamSo.frm_DsMaDV.PassMaDV(PassDataDV);
                frm.ShowDialog();
            }
        }

        private void grvDonThuocct_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == colSoTTkd)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void cboMaICD2_TextChanged(object sender, EventArgs e)
        {
        }

        private void mnMoiBN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (xtraKhamBenh.Text.Contains("*") || xtraChiDinh.Text.Contains("*") || xtabDichVuCS2.Text.Contains("*"))
            {
                DialogResult _result = MessageBox.Show("Bạn chưa lưu dữ liệu, Bạn có muốn lưu không?", "thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    // btlLuuKb_Click(sender, e);
                    mnLuu_ItemClick(null, null);
                }
            }
            bool checkQuyen = true;
            if (DungChung.Bien.MaBV == "01830")
            {
                checkQuyen = DungChung.Ham.checkQuyenFalse("frmHSBNNhapMoi")[0];
            }
            if (checkQuyen)
            {
                frmHSBNNhapMoi frm = new frmHSBNNhapMoi(0);
                frm.ShowDialog();
                TimKiem();
            }
            else
            {
                MessageBox.Show("Bạn chưa được cấp quyền");
            }
        }

        private void mnHuyKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //int rs;
            //int _int_maBN = 0;
            //if (Int32.TryParse(txtMaBNhan.Text, out rs))
            //{
            //    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            //    if (_int_maBN > 0)
            //    {
            //        if (MessageBox.Show("Bạn muốn hủy khám cho bệnh nhân " + txtTenBenhNhan.Text + " ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            DungChung.Ham._setStatus(_int_maBN, -1);
            //        }
            //    }
            //}
        }

        private void mnDonGayNghien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_IDDon = 0;

            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _int_IDDon = Convert.ToInt32(txtIdDonThuoc.Text);

            string _kieuDon = "Thuốc gây nghiện";
            InDonGN_HTT(_int_IDDon, _kieuDon);
        }

        private void mnDonHuongTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_IDDon = 0;

            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _int_IDDon = Convert.ToInt32(txtIdDonThuoc.Text);
            string _kieuDon = "Thuốc hướng tâm thần";
            InDonGN_HTT(_int_IDDon, _kieuDon);
        }

        private void mnDonThuocThang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_IDDon = 0;
            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _int_IDDon = Convert.ToInt32(txtIdDonThuoc.Text);
            if (DungChung.Bien.MaBV == "12121" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "14017")
                frm_Check_moi._InPhieuThuocDY_TT01_A5(_int_IDDon);
            else
                frm_Check_moi._InPhieuThuocDY_TT01(_int_IDDon);

        }

        private void mnPhieuLinhNgoaiTru_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_IDDon = 0;
            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _int_IDDon = Convert.ToInt32(txtIdDonThuoc.Text);
            InPhieuLinhBNNgoaiTru(_int_IDDon);
        }

        private void mnXemChiPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _thanhToan = false;
            FormThamSo.frm_XemChiPhi frm = new FormThamSo.frm_XemChiPhi(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), txtTenBenhNhan.Text);
            frm._getdata = new
                 FormThamSo.frm_XemChiPhi.getvalue(_dlg);
            frm.ShowDialog();
            if (_thanhToan)
            {
                if (DungChung.Bien.MaBV == "24009")
                {
                    TimKiem();
                }
            }
        }

        private void mnThongTinBN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                frmHSBNNhapMoi frm = new frmHSBNNhapMoi(2, String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), DungChung.Bien.MaBV);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có BN ");
            }
        }
        //public string[] GetMaYHCT(string MaICD, List<ICD10> _licd)
        //{
        //    string[] _arr = new string[] { "", "" };
        //    if (!string.IsNullOrEmpty(MaICD))
        //    {
        //        if(MaICD.Contains(";"))
        //        {
        //            List<string> q1 = new List<string>();
        //            var q2 = MaICD.Split(';').Where(p => p != "").ToList();
        //            var q3 = (from a in q2
        //                      join icd in _licd on a equals icd.MaICD
        //                      select new { icd.TenYHCT, icd.MaYHCT }).Distinct().ToList();
        //            if(q3.Count>0)
        //            {
        //                _arr[0] = string.Join(";", q3.Select(p => p.MaYHCT).ToArray());
        //                _arr[1] = string.Join(";", q3.Select(p => p.TenYHCT).ToArray());
        //            }
        //        }
        //        else
        //        {
        //            var q1 = _licd.Where(p => p.MaICD == MaICD).FirstOrDefault();
        //            if (q1 != null)
        //            {
        //                _arr[0] = q1.MaYHCT;
        //                _arr[1] = q1.TenYHCT;
        //            }
        //        }
        //    }
        //    return _arr;
        //}
        private void mnDonThuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            int _idDon = 0;
            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _idDon = Convert.ToInt32(txtIdDonThuoc.Text);
            int id = 0;

            if (DungChung.Bien.MaBV == "26007" && grvChuyenKhoa.GetFocusedRowCellValue(colID) != null && grvChuyenKhoa.GetFocusedRowCellValue(colID).ToString() != "")
            {
                id = Convert.ToInt32(txtIdkb.Text);
            }
            if ((DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && grvChuyenKhoa.GetFocusedRowCellValue(colID) != null && grvChuyenKhoa.GetFocusedRowCellValue(colID).ToString() != "")
            {
                id = Convert.ToInt32(grvChuyenKhoa.GetFocusedRowCellValue(colID));
            }
            if (_int_maBN > 0)
            {
                if (mnInDon.Caption == "In đơn")
                {
                    int makp = 0;
                    if (DungChung.Bien.MaBV == "27001" && lupKhoaKhamkb.EditValue != null)
                        makp = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                    //var qMaChuQuan = _data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                    //if (qMaChuQuan != null && qMaChuQuan.MaChuQuan == "12001")
                    //    DungChung.Ham.InDonThuocTDuong(_int_maBN, _idDon);
                    //else
                    //{
                    string tuoi = DungChung.Ham.TuoitheoThang(_dataContext, _int_maBN, "72-00");
                    if (tuoi.Length > 3)
                    {
                        var ktratt = _dataContext.TTboXungs.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                        if (ktratt != null)
                        {
                            if (string.IsNullOrEmpty(ktratt.NThan) || !ktratt.NThan.Contains(";") || ktratt.NThan.Split(';').Length < 2)
                            {
                                MessageBox.Show("với bệnh nhân dưới 72 tháng tuổi cần có thông tin người thân");
                                frm_TTNgThanTreEm frm = new frm_TTNgThanTreEm(_int_maBN);
                                frm.ShowDialog();
                                DungChung.Ham.InDon(_idDon, _int_maBN, makp, id);
                            }
                            else
                                DungChung.Ham.InDon(_idDon, _int_maBN, makp, id);
                        }
                    }
                    else if (DungChung.Bien.MaBV == "27022" && checkGhiChu.Checked)
                    {
                        DungChung.Ham.InDon_27022(_idDon, _int_maBN, makp, id, true);
                    }
                    else
                        DungChung.Ham.InDon(_idDon, _int_maBN, makp, id);
                    //}
                }//
                else
                {
                    frmIn frm = new frmIn();
                    BaoCao.Rep_PhieuTHCD rep = new BaoCao.Rep_PhieuTHCD();

                    rep.Chuandoan.Value = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                    var bn2 = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                               select new { bn.TenBNhan, bn.MaBNhan, bn.DChi, bn.GTinh, bn.CapCuu, bn.Tuoi, bn.SThe }).ToList();
                    if (bn2.Count > 0)
                    {
                        rep.Diachi.Value = bn2.First().DChi;
                        rep.Tuoi.Value = DungChung.Ham.TuoitheoThang(_dataContext, _int_maBN, DungChung.Bien.formatAge);
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
                    string xn = "12321", cdha = "12321";
                    if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "12001")
                    {
                        cdha = "Chẩn đoán hình ảnh";
                        xn = "Xét nghiệm";
                    }
                    var XQ = (
                          from canls in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2)
                          join kp in _dataContext.KPhongs on canls.MaKP equals kp.MaKP
                          join cb in _dataContext.CanBoes on canls.MaCB equals cb.MaCB
                          join chidinh in _dataContext.DThuoccts on canls.IDDon equals chidinh.IDDon
                          join dichvu in _dataContext.DichVus on chidinh.MaDV equals dichvu.MaDV
                          join tn in _dataContext.TieuNhomDVs on dichvu.IdTieuNhom equals tn.IdTieuNhom
                          join Nhom in _dataContext.NhomDVs on tn.IDNhom equals Nhom.IDNhom
                          where (Nhom.TenNhomCT.Contains("Thủ thuật, phẫu thuật") || Nhom.TenNhomCT.Contains("DVKT thanh toán theo tỷ lệ") || Nhom.TenNhomCT.Contains(cdha)
                          || Nhom.TenNhomCT.Contains(xn))
                          group new { canls, cb, kp, dichvu, tn, chidinh } by new { NgayThang = canls.NgayKe, cb.TenCB, kp.TenKP, TenDV = dichvu.TenDV, tn.TenTN } into kq
                          select new { kq.Key.NgayThang, kq.Key.TenCB, kq.Key.TenKP, kq.Key.TenDV, kq.Key.TenTN, SoLuong = kq.Sum(p => p.chidinh.SoLuong) }
                          ).ToList();
                    if (XQ.Count > 0)
                    {
                        DateTime _dt = System.DateTime.Now;
                        if (XQ.First().NgayThang != null)
                            _dt = XQ.First().NgayThang.Value;
                        rep.NgayGio.Value = DungChung.Ham.NgaySangChu(_dt, 2);
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
                        MessageBox.Show("Không có chỉ định nào!");
                    }
                }
            }//
            else
            {
                MessageBox.Show("Không có BN để in đơn");
            }
        }

        private void mnPhieuKCB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            try
            {
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                {
                    InDon01071(_int_maBN);
                }
                else
                    InPhieuKCB(_int_maBN);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không in được phiếu:" + ex.Message);
            }
        }

        private void InDon01071(int _int_maBN)
        {
            #region bv Nam Thăng Long
            BaoCao.repDonThuoc_01071 rep2 = new BaoCao.repDonThuoc_01071();

            var ttd = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                       join kb in _dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                       join cb in _dataContext.CanBoes on kb.MaCB equals cb.MaCB
                       join kp in _dataContext.KPhongs on kb.MaKP equals kp.MaKP
                       select new { bn.GTinh, bn.TenBNhan, bn.NamSinh, bn.NgaySinh, bn.DTuong, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi, kb.GhiChu, cb.TenCB, kp.TenKP, kb.NgayKham, cb.CapBac }).OrderByDescending(p => p.IDKB).ToList();
            //  rep2._idDon.Value = ktkd.First().IDDon;
            //string _dt = "";
            if (ttd.Count > 0)
            {
                //_dt = ttd.First().DTuong;
                rep2._TenBNhan.Value = ttd.First().TenBNhan;
                if (DungChung.Bien.MaBV == "27001")
                {
                    rep2.Tuoi.Value = ttd.First().NamSinh;
                    rep2.lblTuoi.Text = "N.Sinh:";
                }
                else
                {
                    rep2.Tuoi.Value = DungChung.Ham.TuoitheoThang(_dataContext, _int_maBN, DungChung.Bien.formatAge);
                    rep2.lblTuoi.Text = "Tuổi:";
                }

                //Lời dặn, họ tên người thân
                string _ghichu = ttd.First().GhiChu ?? "";
                string[] ar = _ghichu.Split(';');
                if (ar.Length > 0)
                    rep2.paraHoTenNguoiThan.Value = ar[0];
                if (ar.Length > 1)
                    rep2.paraLoDanBS.Value = ar[1];

                // KT
                switch (ttd.First().GTinh)
                {
                    case 1:
                        rep2.GTinh.Value = "Nam";
                        rep2.Nu.Value = "/";
                        break;
                    case 0:
                        rep2.GTinh.Value = "Nữ";
                        rep2.Nam.Value = "/";
                        break;
                }
                rep2.ICD.Value = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                rep2.SThe.Value = ttd.First().SThe;
                rep2.ChanDoan.Value = DungChung.Ham.getMaICDarr(_dataContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                rep2.TenCB.Value = ttd.First().CapBac + ": " + ttd.First().TenCB;
                rep2.TenKP.Value = ttd.First().TenKP;
                rep2.DiaChi.Value = ttd.First().DChi;
                rep2._MaBNhan.Value = _int_maBN.ToString();
                if (ttd.First().NgayKham.Value.Day > 0)
                    rep2.ngayke.Value = DungChung.Ham.NgaySangChu(ttd.First().NgayKham.Value);

            }
            #region in chi tiết thuốc giống phiếu KCB
            List<ChiDinh> _lcd = new List<ChiDinh>();
            List<int> qcls = _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN).Select(p => p.IdCLS).ToList();
            if (qcls != null)
                _lcd = _dataContext.ChiDinhs.Where(p => qcls.Contains((int)p.IdCLS)).ToList();
            BenhNhan benhnhan = _dataContext.BenhNhans.Single(p => p.MaBNhan == _int_maBN);

            //var khambenh = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
            //var canbo = _dataContext.CanBoes.ToList();
            //var kphong = _dataContext.KPhongs.ToList();


            var qdthuoc = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN) join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon select dtct).ToList();
            var qdv = (from dv in _dataContext.DichVus
                       join nhomdv in _dataContext.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                       select new { nhomdv.TenNhom, nhomdv.STT, dv.TenDV, dv.MaDV, dv.TenHC, dv.DonVi }).ToList();
            var q2 = (from dtct in qdthuoc.Where(p => p.TrongBH == 1 && p.ThanhToan == 0)
                      join dv in qdv on dtct.MaDV equals dv.MaDV
                      select new { dv.TenNhom, dv.STT, dv.TenDV, dv.MaDV, dv.TenHC, dv.DonVi, dtct.DonGia, dtct.IDCD, dtct.IDDonct, dtct.TrongBH, dtct.SoLuong, dtct.ThanhTien, dtct.NgayNhap }).ToList();
            var qt = (from dt in q2
                      group dt by new { dt.TenNhom, dt.STT, dt.TenDV, dt.MaDV, dt.TenHC, dt.DonVi, dt.DonGia, dt.IDCD, dt.TrongBH } into kq
                      select new
                      {
                          kq.Key.TrongBH,
                          idmin = kq.Min(p => p.IDDonct),
                          kq.Key.IDCD,
                          TenNhom = kq.Key.TenNhom,
                          kq.Key.STT,
                          TenDV = (kq.Key.TenHC != null && kq.Key.TenHC != "" && DungChung.Bien.MaBV == "30012") ? kq.Key.TenHC + " (" + kq.Key.TenDV + ")" : kq.Key.TenDV,
                          kq.Key.MaDV,
                          kq.Key.DonVi,
                          kq.Key.DonGia,
                          SoLuong = kq.Sum(p => p.SoLuong),
                          ThanhTien = kq.Sum(p => p.ThanhTien)
                      }).OrderBy(p => p.STT).ThenBy(p => p.idmin).ToList();
            #endregion
            rep2.DataSource = qt.ToList();
            rep2.ThuKho.Value = DungChung.Bien.ThuKho;
            rep2.BindData();
            rep2.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep2.PrintingSystem;
            frm.ShowDialog();
            #endregion
        }

        private void mnHenKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (!string.IsNullOrEmpty(txtIdkb.Text))
            {
                int id = Convert.ToInt32(txtIdkb.Text);
                var idkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                if (idkb.Count > 0)
                {
                    if (id >= idkb.First().IDKB)
                    {
                        FormThamSo.frm_HenKham frm = new FormThamSo.frm_HenKham(id);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Chỉ Khoa khám bệnh cuối cùng của BN mới có quyền hẹn tái khám");
                    }
                }
            }
        }

        private void mnXacNhanNGhiOm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (!string.IsNullOrEmpty(txtIdkb.Text))
            {

                int id = Convert.ToInt32(txtIdkb.Text);
                var idkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                if (idkb.Count > 0)
                {
                    if (id >= idkb.First().IDKB)
                    {
                        ChucNang.frm_NhapNghiOm frm = new ChucNang.frm_NhapNghiOm(id);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Chỉ Khoa khám bệnh cuối cùng của BN mới có quyền xác nhận");
                    }
                }
            }
        }

        private void mnChuyenVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _mabn = 0;
            if (!String.IsNullOrEmpty(txtMaBNhan.Text))
            {
                _mabn = Convert.ToInt32(txtMaBNhan.Text);
            }
            if (radGiaiQuyet.SelectedIndex == 0)
            {
                var bn1 = _dataContext.DienBiens.Where(p => p.MaBNhan == _mabn).ToList();
                if (bn1.Count > 0)
                {
                    foreach (var item in bn1)
                    {
                        if (item.Ploai == 0 && (item.DienBien1 == null || item.DienBien1 == ""))
                        {
                            XtraMessageBox.Show("Nhập đầy đủ diễn biến bệnh trước khi chuyển viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).OrderBy(p => p.IDKB).ToList();
                if (ktkb.Count > 0)
                {
                    if (string.IsNullOrEmpty(ktkb.First().MaICD))
                    {
                        MessageBox.Show("Bạn chưa nhập bệnh chính cho bệnh nhân");
                    }
                    else
                    {
                        frm_Ravien frm = new frm_Ravien(_mabn);
                        var bn = (from bnkb in _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn)
                                  select new { bnkb.Status }).ToList();
                        if ((DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "30004"
                            ) && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(15) > DateTime.Now))
                        {
                            string time = "Thời gian khám bệnh của bệnh nhân < 15 phút (" + (DateTime.Now - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p) \nBạn có muốn kết thúc?";
                            DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.No)
                                return;
                        }
                        else if (((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && (radGiaiQuyet.SelectedIndex == 0 || radGiaiQuyet.SelectedIndex == 2)) && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(5) > dt_NgayChuyen.DateTime))
                        {
                            string time = "Thời gian khám bệnh của bệnh nhân < 5 phút (" + (dt_NgayChuyen.DateTime - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p)";
                            DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            if (dr == DialogResult.OK)
                                return;
                        }
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa khám bệnh cho bệnh nhân");
                }
            }
            else
            {

                var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).ToList();
                if (ktkb.Count > 0)
                {
                    if (string.IsNullOrEmpty(ktkb.First().MaICD))
                    {
                        MessageBox.Show("Bạn chưa nhập bệnh chính cho bệnh nhân");
                    }
                    else
                    {
                        frm_CVienNoiTru frm = new frm_CVienNoiTru(_mabn);
                        frm.ShowDialog();
                        grvBNhankb_FocusedRowChanged(null, null);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa khám bệnh cho bệnh nhân");
                }
            }
        }

        private void mnKBVaoVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DungChung.Ham.KTraKB(_dataContext, String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text)))
            {
                //if (DungChung.Bien.MaBV == "26007")
                //{
                //    if (string.IsNullOrEmpty(lupMaICDkb.Text))
                //    {
                //        MessageBox.Show("không thể cho ra viện, bạn chưa nhập bệnh chính");
                //    }
                //    else
                //    {
                //        frmKBVaoVien frm = new frmKBVaoVien(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text));
                //        frm.ShowDialog();
                //    }
                //}
                //else
                //{
                frmKBVaoVien frm = new frmKBVaoVien(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text));
                frm.ShowDialog();
                grvBNhankb_FocusedRowChanged(null, null);

                //}
            }
            else
            {
                DialogResult _result = MessageBox.Show("Bạn chưa lưu khám bệnh, bạn có muốn lưu không?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    // btlLuuKb_Click(sender, e);
                    mnLuu_ItemClick(null, null);
                }
            }
        }

        private void mnChiDinhCLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTenBenhNhan.Text))
            {
                var _lkp = _dataContext.KPhongs.Where(p => p.MaKP == _makp).FirstOrDefault();
                //if (DungChung.Ham.KTraKB(_dataContext, String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text)) || (_lkp != null && _lkp.QuanLy == 1))
                //{
                int _mabn = 0;
                if (!String.IsNullOrEmpty(txtMaBNhan.Text))
                {
                    _mabn = Convert.ToInt32(txtMaBNhan.Text);
                }
                var ktrabn = _dataContext.BNKBs.Where(p => p.MaBNhan == _mabn).FirstOrDefault(); string a = "";
                if (grvBNhankb.GetFocusedRowCellValue(colDTuong) != null)
                    a = grvBNhankb.GetFocusedRowCellValue(colDTuong).ToString();
                if (ktrabn != null)
                {
                    int capcuu = 0;
                    if (DungChung.Bien.MaBV == "24012")
                    {
                        capcuu = Convert.ToInt32(_dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault().CapCuu);
                    }
                    if (a == "Dịch vụ" && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297" && DungChung.Bien.MaBV != "56789" && DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049" && capcuu != 1)
                        MessageBox.Show("Bệnh nhân không có thẻ BHYT, yêu cầu tạm thu dịch vụ");
                }
                else
                {
                    if (a == "BHYT")
                    {
                        MessageBox.Show("Chỉ định CLS không qua khám bệnh chỉ dành cho BN dịch vụ", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }

                }
                if (DungChung.Bien.MaBV == "34019")
                {
                    FormThamSo.FRM_chidinh_Moi frm = new FormThamSo.FRM_chidinh_Moi(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), txtTenBenhNhan.Text, -1);
                    frm.ShowDialog();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(maIcdCbo))
                    {
                        FormThamSo.FRM_chidinh_Moi frm = new FormThamSo.FRM_chidinh_Moi(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), txtTenBenhNhan.Text, maIcdCbo);
                        frm.ShowDialog();
                    }
                    else
                    {
                        FormThamSo.FRM_chidinh_Moi frm = new FormThamSo.FRM_chidinh_Moi(String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), txtTenBenhNhan.Text);
                        frm.ShowDialog();
                    }
                }
                // }
                //else
                //{
                //    MessageBox.Show("Bạn chưa nhập khám bệnh cho bệnh nhân.");
                //}
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân.");
            }
        }

        private void mnThucHienThuThuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChucNang.frm_ThucHienPT frm = new ChucNang.frm_ThucHienPT((String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text)));
            frm.ShowDialog();
        }

        private void mnKThucKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int mabn = Convert.ToInt32(txtMaBNhan.Text);
            var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == mabn).OrderBy(p => p.IDKB).ToList();
            if (ktkb.Count == 0)
            {
                MessageBox.Show("Bệnh nhân chưa nhập đầy đủ thông tin khám bệnh không thể kết thúc khám!!", "Thông báo!");
                return;
            }
            var bn = (from bnkb in _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn)
                      select new { bnkb.Status }).ToList();
            if ((DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "30372") && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(15) > DateTime.Now) && (mnKThucKham.Caption == "Kết thúc khám"))
            {
                string time = "Thời gian khám bệnh của bệnh nhân < 15 phút (" + (DateTime.Now - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p) ";
                DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                    return;
            }
            else if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(5) > DateTime.Now))
            {
                string time = "Thời gian khám bệnh của bệnh nhân < 5 phút (" + (DateTime.Now - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p) ";
                DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                    return;
            }
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                var kttt = _dataContext.VienPhis.Where(p => p.MaBNhan == mabn).ToList();
                if (kttt.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã thanh toán!");
                    return;
                }
                //ktra id khám bệnh
                int idkb = Convert.ToInt32(txtIdkb.Text);

                var qbnkb = (from bnkb in _dataContext.BNKBs.Where(p => p.MaBNhan == mabn)
                             join kp in _dataContext.KPhongs on bnkb.MaKP equals kp.MaKP
                             select new { bnkb, kp.TenKP }).OrderByDescending(p => p.bnkb.IDKB).ToList();

                if (qbnkb.Count == 0)
                {
                    MessageBox.Show("Bệnh nhân chưa khám bệnh");
                    return;
                }
                else if (qbnkb.Max(p => p.bnkb.IDKB) != idkb)
                {
                    MessageBox.Show("Bạn chưa chọn khám bệnh cuối cùng");
                    return;
                }
                else if (qbnkb.First().bnkb.PhuongAn == 3 && qbnkb.First().bnkb.NgayNghi != null)
                {
                    MessageBox.Show("Bệnh nhân đã chuyển phòng khám, bạn không thể kết thúc khám");
                    return;
                }
                else
                {
                    var checkBNKB = qbnkb.FirstOrDefault(o => o.bnkb.IDKB == idkb);
                    if (checkBNKB != null)
                    {
                        var bnkbOld = qbnkb.Where(o => o.bnkb.IDKB < idkb).OrderByDescending(p => p.bnkb.IDKB);
                        if (bnkbOld != null && bnkbOld.Count() > 0)
                        {
                            if (bnkbOld.FirstOrDefault().bnkb.NgayKham > dtNgayKhamkb.DateTime)
                            {
                                MessageBox.Show("Ngày khám không được nhỏ hơn ngày khám tại: " + bnkbOld.FirstOrDefault().TenKP);
                                return;
                            }
                            if (bnkbOld.FirstOrDefault().bnkb.NgayNghi >= dtNgayKhamkb.DateTime)
                            {
                                MessageBox.Show("Ngày khám không được nhỏ hơn ngày chuyển tại: " + bnkbOld.FirstOrDefault().TenKP);
                                return;
                            }
                        }
                    }

                    int mkptimkiem = 0, makhoakham = 0;
                    if (lupTimMaKP.EditValue != null)
                    {
                        mkptimkiem = Convert.ToInt32(lupTimMaKP.EditValue);
                    }
                    if (lupKhoaKhamkb.EditValue != null)
                    {
                        makhoakham = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                    }
                    if (mkptimkiem == makhoakham && makhoakham != 0)
                    {
                        var ktrv = _dataContext.RaViens.Where(p => p.MaBNhan == mabn).ToList();
                        if (ktrv.Count > 0)
                        {
                            if (ktrv.First().Status == 1)
                            {
                                MessageBox.Show("Bệnh nhân đã được chuyển viện");
                            }
                            else
                                if (DialogResult.Yes == MessageBox.Show("Bạn muốn sửa khám bệnh cho bệnh nhân?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                DungChung.Ham._LuuXoaRaVien(_dataContext, mabn, DateTime.Now, "Xoa", 2);
                            }
                        }
                        else
                        {
                            var checkDThuoc = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == mabn).Where(p => p.PLDV == 1)
                                               join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                               where dtct.SoLuong > 0
                                               select dtct).ToList();
                            if (radGiaiQuyet.SelectedIndex == 4 && dt_NgayChuyen.EditValue != null && checkDThuoc.Exists(o => o.NgayNhap > DateTime.Now))
                            {

                                MessageBox.Show("Ngày ra viện không được nhỏ hơn ngày kê");
                                return;

                            }
                            if (((DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "30372") && (Convert.ToDateTime(qbnkb.First().bnkb.NgayKham).AddMinutes(15) > DateTime.Now)) || DialogResult.Yes == MessageBox.Show("Bạn muốn kết thúc khám bệnh cho bệnh nhân?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                DungChung.Ham._LuuXoaRaVien(_dataContext, mabn, DateTime.Now, "Luu", 2);
                            }
                            else if ((((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && (radGiaiQuyet.SelectedIndex == 0 || radGiaiQuyet.SelectedIndex == 2)) && (Convert.ToDateTime(qbnkb.First().bnkb.NgayKham).AddMinutes(15) > DateTime.Now)) || DialogResult.Yes == MessageBox.Show("Bạn muốn kết thúc khám bệnh cho bệnh nhân?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                DungChung.Ham._LuuXoaRaVien(_dataContext, mabn, DateTime.Now, "Luu", 2);
                            }
                        }
                        grvBNhankb_FocusedRowChanged(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Mã khoa phòng không đúng");
                        return;
                    }
                }
            }
        }


        private void mnThongTinBNLao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int mabn = string.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text);
            frm_NhapBenhNhanLao frm = new frm_NhapBenhNhanLao(mabn);
            frm.ShowDialog();
        }

        private void mnKBMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int mabn = string.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text);
            var makpht = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn).Select(p => p.MaKP).FirstOrDefault();
            var qbnkb = _dataContext.BNKBs.Where(p => p.MaBNhan == mabn).OrderByDescending(p => p.IDKB).FirstOrDefault();
            bool ktra = true; int kpKham = 0;
            if (lupTimMaKP.EditValue != null)
                kpKham = Convert.ToInt32(lupTimMaKP.EditValue);
            if (kpKham > 0 && qbnkb != null)
            {
                if (qbnkb.MaKP == kpKham)
                {
                    ktra = false;
                    MessageBox.Show("Bệnh nhân đang điều trị tại " + lupTimMaKP.Text + ". Bạn không thể thêm mới khám bệnh tại phòng khám này");
                }
                else
                {
                    if ((qbnkb.PhuongAn == 1 || qbnkb.PhuongAn == 3) && qbnkb.MaKPdt == kpKham)
                        ktra = true;
                    else
                    {
                        ktra = false;
                        MessageBox.Show("Bệnh nhân đang điều trị tại khoa phòng khác, bạn không thể thêm mới khám bệnh cho bệnh nhân tại " + lupTimMaKP.Text);
                    }
                }
            }

            if (ktra)
            {
                ResetControlKB();
                EnableControlKB(true);
                lupKhoaKhamkb.EditValue = DungChung.Bien.MaKP;


                if (makpht != null && makpht != _makp)
                {
                    radGiaiQuyet.SelectedIndex = 4;
                    radGiaiQuyet.Enabled = false;
                    lupKhoaDT.Enabled = false;
                }
                TTLuuKB = true;
            }
            else
            {

            }
        }

        private void mnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            var kt = (from bn in _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN) select bn).ToList();
            int _mkpkb = 0;
            if (lupKhoaKhamkb.EditValue != null)
                _mkpkb = Convert.ToInt32(lupKhoaKhamkb.EditValue);
            bool checkcongkham = DungChung.Ham.KtraCongKham(_dataContext, _int_maBN, _mkpkb);
            if (kt.Count > 0)
            {
                MessageBox.Show("Bệnh nhân đã ra viện|chuyển viện, bạn không thể xóa thông tin");
                return;
            }
            switch (TTTab)
            {
                case 1:
                    try
                    {
                        if (!string.IsNullOrEmpty(txtIdDonThuoc.Text))
                        {
                            int id = int.Parse(txtIdDonThuoc.Text);


                            List<DThuocct> xoa = new List<DThuocct>();
                            xoa = (_dataContext.DThuoccts.Where(p => p.IDDon == (id))).ToList();

                            if (xoa.Count > 0 && xoa.Where(p => p.Status == 1).ToList().Count > 0)
                            {
                                MessageBox.Show("Đơn này đã được xuất dược, Bạn không được xóa!");
                            }
                            else if (xoa.Count > 0 && xoa.Where(p => p.SoPL == 1).ToList().Count > 0)
                            {
                                MessageBox.Show("Đơn này đã lên phiếu lĩnh, Bạn không thể xóa!");
                            }
                            else if (checkcongkham == false)
                            {
                                MessageBox.Show("Bệnh nhân đã phát sinh dịch vụ ngoài công khám \nmuốn xóa khám bệnh cần xóa các dịch vụ khác và đơn thuốc trước", "thông báo", MessageBoxButtons.OK);
                            }
                            else
                            {
                                var ktdt = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN && p.PLDV == 2).ToList();
                                bool _dongyxoa = true;
                                int _makp = 0;
                                if (lupKhoaKhamkb.EditValue != null)
                                    _makp = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                int id2 = int.Parse(txtIdkb.Text.Trim());
                                int idkbmax = -1;
                                var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                if (ktkb.Count > 0)
                                    idkbmax = ktkb.Max(p => p.IDKB);
                                if (id2 < idkbmax)
                                {
                                    _dongyxoa = false;
                                    MessageBox.Show("Bạn không thể xóa vì khám bệnh này không phải là khám bệnh cuối cùng");
                                }
                                else
                                {

                                    if (ktkb.First().PhuongAn != null && ktkb.First().PhuongAn == 1)
                                    {
                                        var ktvv = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                        if (ktvv.Count > 0)
                                        {
                                            MessageBox.Show("Bạn không thể xóa vì Bệnh Nhân đã có khám bệnh vào viện");
                                            _dongyxoa = false;
                                        }
                                    }
                                }
                                var ktcls = _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN && p.MaKP == _makp).ToList();
                                if (ktcls.Count > 0)
                                {
                                    _dongyxoa = false;
                                    MessageBox.Show("BN đã có chỉ định cls, bạn không thể xóa");

                                }
                                if (_dongyxoa)
                                {
                                    if (ktkb.Count > 0)
                                    {
                                        int mak = lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue);
                                        if (mak == (ktkb.First().MaKP ?? 0))
                                        {
                                            if (_makp != DungChung.Bien.MaKP && DungChung.Bien.PLoaiKP != "Admin")
                                            {
                                                _dongyxoa = false;
                                                MessageBox.Show("Bạn không có quyền xóa");
                                            }

                                        }
                                        else
                                        {
                                            _dongyxoa = false;
                                            MessageBox.Show("Mã KP không khớp, bạn không thể xóa");
                                        }
                                    }
                                }
                                if (ktdt.Count <= 0 && _dongyxoa)
                                {
                                    DialogResult result;
                                    result = MessageBox.Show("Bạn muốn xóa đơn và khám bệnh BN: " + txtTenBenhNhan.Text, "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        List<DThuocct> sl = new List<DThuocct>();
                                        if (id > 0)
                                        {
                                            //sl = (from iddct in _dataContext.DThuoccts.Where(p => p.IDDon == id) select iddct).ToList();
                                            //foreach (var s in sl)
                                            //{
                                            //    var dtct = _dataContext.DThuoccts.Single(p => p.IDDonct == (s.IDDonct));
                                            //    _dataContext.Remove(dtct);
                                            //    _dataContext.SaveChanges();
                                            //}
                                            //var xoad = _dataContext.DThuocs.Single(p => p.IDDon == (id));
                                            //_dataContext.Remove(xoad);
                                            //_dataContext.SaveChanges();
                                            var dtbn = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            foreach (var item in dtbn)
                                            {
                                                var dtctbn = _dataContext.DThuoccts.Where(p => p.IDDon == item.IDDon).ToList();
                                                foreach (var item2 in dtctbn)
                                                {
                                                    _dataContext.DThuoccts.Remove(item2);
                                                }
                                                _dataContext.DThuocs.Remove(item);
                                                _dataContext.SaveChanges();
                                            }
                                        }
                                        //xoa kb
                                        try
                                        {

                                            var ktslkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            if (ktslkb.Count > 0)
                                            {
                                                //if (!DungChung.Ham.KTraTT(_dataContext, txtMaBNhan.Text))
                                                //{
                                                foreach (var a in ktslkb)
                                                {
                                                    //DialogResult result2 = DialogResult.Yes;
                                                    ////result = MessageBox.Show("Bạn muốn xóa khám bệnh của BN: " + txtTenBenhNhan.Text, "xóa khám bệnh!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                    //if (result2 == DialogResult.Yes)
                                                    //{
                                                    // xóa công khám
                                                    DungChung.Ham.Update_Delete_CongKham(_int_maBN, a.IDKB, false, dtNgayKhamkb.DateTime);
                                                    //
                                                    var kb = _dataContext.BNKBs.Single(p => p.IDKB == a.IDKB);
                                                    _dataContext.BNKBs.Remove(kb);
                                                    if (_dataContext.SaveChanges() >= 0)
                                                    {

                                                        var ktslkb2 = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                                        if (ktslkb2.Count > 0)
                                                        {
                                                            int _chuyenkhoa = ktslkb2.First().MaKP == null ? 0 : Convert.ToInt32(ktslkb2.First().MaKP);
                                                            if (ktslkb2.First().MaKPdt != null)
                                                                _chuyenkhoa = Convert.ToInt32(ktslkb2.First().MaKPdt);

                                                            Ham._setStatus(_int_maBN, 1, _chuyenkhoa);

                                                        }
                                                        else
                                                        {
                                                            Ham._setStatus(_int_maBN, 0);
                                                        }
                                                        //usKhamBenh_Load(sender, e);
                                                        grcBNhankb.Refresh();
                                                        //usKhamBenh_Load(sender, e);

                                                    }
                                                    //}
                                                }
                                                if (DungChung.Bien.MaBV == "30303")
                                                {
                                                    var ktslkb2 = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).FirstOrDefault();
                                                    if (ktslkb2 != null)
                                                    {
                                                        DungChung.Ham.Update_Delete_CongKham(_int_maBN, ktslkb2.IDKB, true, ktslkb2.NgayKham.Value);
                                                    }
                                                }
                                                //}
                                                //else
                                                //{
                                                //    MessageBox.Show("Bệnh nhân đã thanh toán, bạn không được xóa");
                                                //}
                                            }
                                            else
                                            {
                                                MessageBox.Show("Bệnh nhân không có khám bệnh");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Lỗi xóa khám bệnh" + ex.Message);
                                        }
                                        //KT
                                        usKhamBenh_Load(sender, e);
                                        int _idxoa = 0;
                                        if (!string.IsNullOrEmpty(txtIdDonThuoc.Text))
                                            _idxoa = int.Parse(txtIdDonThuoc.Text);
                                        binSDonThuocct.DataSource = _lDthuocct.Where(p => p.IDDon == (_idxoa)).OrderBy(p => p.IDDonct).ToList();
                                        grcDonThuocct.DataSource = binSDonThuocct;// _lDthuocct.Where(p => p.IDDon == (_idxoa)).OrderBy(p => p.IDDon).ToList(); //binSDonThuocct;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Bệnh nhân đã có dịch vụ, bạn chỉ được sửa");
                                }
                            }//

                        }
                        else
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(txtIdkb.Text))
                                {
                                    //if (!DungChung.Ham.KTraTT(_dataContext, txtMaBNhan.Text))
                                    //{
                                    bool _dongyxoa = true;
                                    int id2 = int.Parse(txtIdkb.Text.Trim());
                                    int idkbmax = -1;
                                    int _makpold = 0;
                                    var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                    if (ktkb.Count > 0)
                                    {
                                        idkbmax = ktkb.Max(p => p.IDKB);
                                        _makpold = ktkb.First().MaKP ?? 0;
                                    }

                                    if (id2 < idkbmax)
                                    {
                                        _dongyxoa = false;
                                        MessageBox.Show("Bạn không thể xóa vì khám bệnh này không phải là khám bệnh cuối cùng");
                                    }
                                    else
                                    {

                                        if (ktkb.First().PhuongAn != null && ktkb.First().PhuongAn == 1)
                                        {
                                            var ktvv = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            if (ktvv.Count > 0)
                                            {
                                                MessageBox.Show("Bạn không thể xóa vì Bệnh Nhân đã có khám bệnh vào viện");
                                                _dongyxoa = false;
                                            }
                                        }
                                    }
                                    var ktclsold = _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN && p.MaKP == _makpold).ToList();
                                    var ktcls = _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN && p.MaKP == _makp).ToList();
                                    // if (ktcls.Count > 0 || ktclsold.Count > 0)
                                    if (ktclsold.Count > 0)
                                    {
                                        _dongyxoa = false;
                                        MessageBox.Show("BN đã có chỉ định cls, bạn không thể xóa");
                                        ;
                                    }
                                    if (ktkb.Where(p => p.IDKB == id2).ToList().Count > 0 && ktkb.Where(p => p.IDKB == id2).First().MaKP != DungChung.Bien.MaKP)
                                    {
                                        //if (DungChung.Bien.CapDo < 9)
                                        if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                                        {
                                            _dongyxoa = false;
                                            MessageBox.Show("Mã khoa phòng không khớp, bạn không thể xóa");
                                        }

                                    }
                                    if (checkcongkham == false)
                                    {
                                        _dongyxoa = false;
                                        MessageBox.Show("Bệnh nhân đã phát sinh dịch vụ ngoài công khám \nmuốn xóa khám bệnh cần xóa các dịch vụ khác và đơn thuốc trước", "thông báo", MessageBoxButtons.OK);
                                    }
                                    DialogResult result2 = DialogResult.Yes;
                                    if (_dongyxoa)
                                        result2 = MessageBox.Show("Bạn muốn xóa khám bệnh của BN: " + txtTenBenhNhan.Text, "xóa khám bệnh!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_dongyxoa && result2 == DialogResult.Yes)
                                    {
                                        DungChung.Ham.Update_Delete_CongKham(_int_maBN, id2, false, dtNgayKhamkb.DateTime);
                                        var kb = _dataContext.BNKBs.Single(p => p.IDKB == (id2));
                                        int _makpbd = kb.MaKP ?? 0;
                                        _dataContext.BNKBs.Remove(kb);
                                        if (_dataContext.SaveChanges() >= 0)
                                        {
                                            var ktslkb2 = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            if (ktslkb2.Count > 0)
                                            {
                                                int _chuyenkhoa = 0;
                                                if (ktslkb2.First().MaKP != null)
                                                    _chuyenkhoa = Convert.ToInt32(ktslkb2.First().MaKP);
                                                if (ktslkb2.First().MaKPdt != null)
                                                    _chuyenkhoa = Convert.ToInt32(ktslkb2.First().MaKPdt);
                                                Ham._setStatus(_int_maBN, 1, _chuyenkhoa);
                                            }
                                            else
                                            {
                                                Ham._setStatus(_int_maBN, 0, _makpbd);
                                            }
                                            var Ktra = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            if (Ktra.Count <= 0)
                                            {
                                                TTboXung ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                                                ttbx.Mach_NDo_HAp = null;
                                                ttbx.CanNang_ChieuCao = null;
                                                _dataContext.SaveChanges();
                                            }
                                            if (DungChung.Bien.MaBV == "30303")
                                            {
                                                var ktslkb21 = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).FirstOrDefault();
                                                if (ktslkb21 != null)
                                                {
                                                    DungChung.Ham.Update_Delete_CongKham(_int_maBN, ktslkb21.IDKB, true, ktslkb21.NgayKham.Value);
                                                }
                                            }
                                            grvBNhankb_FocusedRowChanged(null, null);
                                            //grcBNhankb.Refresh();
                                            //usKhamBenh_Load(sender, e);
                                        }
                                    }

                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("Bệnh nhân đã thanh toán, bạn không được xóa");
                                    //}
                                    if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                                    {
                                        // 1. khám bệnh
                                        List<BNKB> query = new List<BNKB>();
                                        query = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                        if (query.Count > 0)
                                        {
                                            txtIdkb.Text = query.First().IDKB.ToString();
                                            if (query.First().NgayKham != null)
                                                dtNgayKhamkb.DateTime = Convert.ToDateTime(query.First().NgayKham);
                                            lupKhoaKhamkb.EditValue = query.First().MaKP;
                                            lupNguoiKhamkb.EditValue = query.First().MaCB;
                                            lupChanDoanKb.EditValue = query.First().ChanDoan;
                                            mmChanDoanBD.Text = query.First().ChanDoanBD;
                                            string[] icd = new string[6] { "", "", "", "", "", "" };
                                            if (!string.IsNullOrEmpty(query.First().MaICD2))
                                            {
                                                string[] icd2 = query.First().MaICD2.Split(';');
                                                for (int i = 0; i < icd2.Length; i++)
                                                {
                                                    if (icd2[i] != null)
                                                        icd[i] = icd2[i];
                                                }
                                            }
                                            LupICD2.EditValue = icd[0];
                                            LupICD3.EditValue = icd[1];
                                            LupICD4.EditValue = icd[2];

                                            if (DungChung.Bien.MaBV != "30007" && DungChung.Bien.MaBV != "30340")
                                            {
                                                string[] benhkhac = new string[6] { "", "", "", "", "", "" };
                                                if (!string.IsNullOrEmpty(query.First().BenhKhac))
                                                {
                                                    string[] icd2 = query.First().BenhKhac.Split(';');
                                                    for (int i = 0; i < icd2.Length; i++)
                                                    {
                                                        if (icd2[i] != null)
                                                        {
                                                            if (i == 2)
                                                                benhkhac[i] += icd2[i];
                                                            else if (i > 2)
                                                            {
                                                                if (icd2[i].Length > 1)
                                                                    benhkhac[2] += ";" + icd2[i];
                                                            }

                                                            else
                                                                benhkhac[i] = icd2[i];
                                                        }
                                                    }
                                                }
                                                txtBenhKhac2.EditValue = benhkhac[0];
                                                txtBenhKhac3.EditValue = benhkhac[1];
                                                txtBenhKhac4.EditValue = benhkhac[2];
                                            }
                                            if (query.First().PhuongAn != null)
                                            {
                                                radGiaiQuyet.SelectedIndex = query.First().PhuongAn.Value;
                                                if (query.First().PhuongAn.Value == 1)
                                                {
                                                    //lbVaoKhoa.Visible = true;
                                                    //lupKhoaDT.Visible = true;
                                                    mnKBVaoVien.Enabled = true;
                                                }
                                                else
                                                {
                                                    //lbVaoKhoa.Visible = false;
                                                    //lupKhoaDT.Visible = false;
                                                    mnKBVaoVien.Enabled = false;
                                                }
                                            }
                                            lupKhoaDT.EditValue = query.First().MaKPdt;

                                        }
                                        else
                                        {//Khám bệnh
                                            ResetControlKB();
                                            lupNguoiKhamkb.EditValue = DungChung.Bien.MaCB;
                                            if (!string.IsNullOrEmpty(macbkb))
                                                lupNguoiKhamkb.EditValue = macbkb;
                                            lupKhoaKhamkb.EditValue = DungChung.Bien.MaKP;

                                            dtNgayKhamkb.DateTime = System.DateTime.Now;
                                            //EnableControl(true);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Bệnh nhân không có khám bệnh");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi xóa khám bệnh" + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa kê đơn" + ex.Message);
                    }

                    //break;
                    //    case 2:
                    xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                    break;
                case 3:
                    try
                    {
                        if (!string.IsNullOrEmpty(txtChiDinh.Text))
                        {
                            int idcd = int.Parse(txtChiDinh.Text);
                            var ktDV = (from bn in _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN) select bn).ToList();
                            if (ktDV.Count > 0)
                            {
                                MessageBox.Show("Bệnh nhân đã thanh toán, Bạn không được xóa");
                            }
                            else
                            {
                                List<DThuocct> xoa = new List<DThuocct>();
                                xoa = (_dataContext.DThuoccts.Where(p => p.IDDon == idcd)).ToList();
                                if (xoa.Count > 0 && xoa.Where(p => p.Status == 1).ToList().Count > 0)
                                {
                                    MessageBox.Show("Đơn này đã được xuất dược, Bạn không được xóa!");
                                }
                                else
                                {

                                    DialogResult result;
                                    result = MessageBox.Show("Bạn muốn xóa chi phí? ", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        List<DThuocct> sl = new List<DThuocct>();
                                        sl = (from iddct in _dataContext.DThuoccts.Where(p => p.IDDon == idcd)
                                              join dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN) on iddct.IDDon equals dt.IDDon// dung thêm 25092017 (Trường hợp chưa có công khám, khi kê đơn=> sang thu thẳng thu công khám => quay về tab dịch vụ để xóa công khám không xóa được)
                                              select iddct).ToList();
                                        bool _xoa = true;
                                        foreach (var a in sl)
                                        {
                                            var ktcd = (from cd in _dataContext.ChiDinhs.Where(p => p.IDCD == a.IDCD)
                                                        join cls in _dataContext.CLS.Where(p => p.MaBNhan == _int_maBN)
                                                            on cd.IdCLS equals cls.IdCLS
                                                        select cd).ToList();
                                            if (ktcd.Count > 0)
                                            {
                                                _xoa = false;
                                                break;
                                            }

                                        }
                                        if (_xoa == true)
                                        {
                                            foreach (var s in sl)
                                            {
                                                var dtct = _dataContext.DThuoccts.Single(p => p.IDDonct == (s.IDDonct));
                                                _dataContext.DThuoccts.Remove(dtct);
                                                _dataContext.SaveChanges();
                                            }
                                            var xoad = _dataContext.DThuocs.Single(p => p.IDDon == (idcd));
                                            _dataContext.DThuocs.Remove(xoad);
                                            _dataContext.SaveChanges();
                                            usKhamBenh_Load(sender, e);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Có các dịch vụ cls đã được thực hiện, bạn không thể xóa");
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có dịnh vụ để xóa");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa chi phí" + ex.Message);
                    }
                    // chỉ định
                    int idcd2 = 0;
                    if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                    {
                        var q = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                        if (q.Count > 0)
                        {
                            txtChiDinh.Text = q.First().IDDon.ToString();
                            idcd2 = q.First().IDDon;
                        }
                        var data = (from dt in _dataContext.DThuoccts.Where(p => p.IDDon == idcd2).Where(p => p.LoaiDV < 3)
                                    join dv in _dataContext.DichVus on dt.MaDV equals dv.MaDV
                                    select new DonThuocct
                                    {
                                        IDDon = dt.IDDon,
                                        IDDonct = dt.IDDonct,
                                        MaDV = dt.MaDV,
                                        TenDV = dv.TenDV,
                                        DonVi = dt.DonVi,
                                        DonGia = dt.DonGia,
                                        SoLuong = dt.SoLuong,
                                        ThanhTien = dt.ThanhTien,
                                        TienBN = dt.TienBN,
                                        TienBH = dt.TienBH,
                                        TrongBH = dt.TrongBH,
                                        NgayNhap = dt.NgayNhap,
                                        DuongD = dt.DuongD,
                                        SoPL = dt.SoPL,
                                        Status = dt.Status,
                                        IDCD = dt.IDCD,
                                        MaCB = dt.MaCB,
                                        MaKP = dt.MaKP,
                                        IDKB = dt.IDKB,
                                        Loai = dt.Loai,
                                        ThanhToan = dt.ThanhToan,
                                        MaKPtk = dt.MaKPtk,
                                        MaKXuat = dt.MaKXuat,
                                        TyLeTT = dt.TyLeTT,
                                        XHH = dt.XHH == 1 ? true : false,
                                        MaQD = dv.MaQD
                                    }).ToList();
                        var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                        if (hthong != null && hthong.IsTV == true && data.Exists(o => o.TenDV.ToLower().Contains("khám bác sỹ chuyên gia")))
                            chkKhamChuyenGia.Checked = true;
                        else
                            chkKhamChuyenGia.Checked = false;
                        binSChiDinhct.DataSource = data.ToList();
                        grcChiDinh.DataSource = binSChiDinhct;
                    }//kt lai
                    break;
            }
        }
        public bool KtraChiPhi(QLBVEntities _data, int _mabn, double TongTienThuoc)
        {
            var _lcpcd = (from cls in _dataContext.CLS.Where(p => p.MaBNhan == _mabn)
                          join cd in _dataContext.ChiDinhs.Where(p => p.SoPhieu <= 0 || p.SoPhieu == null).Where(p => p.Status == 0) on cls.IdCLS equals cd.IdCLS
                          select new { cd.IDCD, cd.MaDV, cd.DonGia }).ToList();
            var _dthuoc = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn)
                           join dtct in _dataContext.DThuoccts.Where(p => p.ThanhToan == 0) on dt.IDDon equals dtct.IDDon
                           select new { dtct.IDDonct, dtct.ThanhTien, dt.PLDV }).ToList();
            double Tiencd = _lcpcd.Sum(p => p.DonGia);
            double Tiendv = _dthuoc.Where(p => p.PLDV == 2).Sum(p => p.ThanhTien);
            double Tienthuoc = _dthuoc.Where(p => p.PLDV == 1).Sum(p => p.ThanhTien);
            double a = Tiencd + TongTienThuoc + Tiendv;
            var _tung = _dataContext.TamUngs.Where(p => p.MaBNhan == _mabn).Where(p => p.PhanLoai == 0).ToList();
            double TienTU = _tung.Count > 0 ? _tung.Sum(p => p.SoTien ?? 0) : 0;
            if (a > TienTU)
                return false;
            else
                return true;
        }
        void nhapcannangTEduoi1tuoi(QLBVEntities data, int mabn)
        {
            string tuoi = DungChung.Bien.MaBV == "30003" ? DungChung.Ham.TuoitheoThang(data, mabn, "84-0") : DungChung.Ham.TuoitheoThang(data, mabn, "72-0");
            int Tuoi = int.Parse(txtTuoi.Text);
            var bn = _dataContext.TTboXungs.Where(p => p.MaBNhan == mabn).Select(p => p).FirstOrDefault();
            if (tuoi.ToLower().Contains("tháng") || DungChung.Bien.MaBV == "30010" && Tuoi <= 6 || DungChung.Bien.MaBV == "30003" && Tuoi <= 7)
            {
                if (bn == null || string.IsNullOrEmpty(bn.CanNang_ChieuCao) || (bn.CanNang_ChieuCao.Contains(";") && string.IsNullOrEmpty(bn.CanNang_ChieuCao.Split(';')[0])))
                {
                    frm_TTKhamBenh frm = new frm_TTKhamBenh(mabn);
                    frm.ShowDialog();
                }

            }

        }

        bool isSave = false;
        private void mnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            try
            {
                isSave = true;
                //  if ((TTLuu == 0 || TTLuu == 1) && TTTab == 1)
                //  ktraHanThe();
                grvDonThuocct.PostEditor();
                grvchiDinh.FocusedRowHandle = 0;
                grvDonThuocct.FocusedRowHandle = 0;
                int rs;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                int idbnkb = 0;
                string[] icd = new string[4] { "", "", "", "" };
                string[] benhkhac = new string[4] { "", "", "", "" };
                var bn1 = _dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                if (bn1 != null)
                {
                    var cls = _dataContext.CLS.Where(o => o.MaBNhan == _int_maBN).ToList();
                    if (cls.Count > 0)
                    {
                        for (int i = 0; i < cls.Count; i++)
                        {
                            if (dt_NgayChuyen.DateTime < cls[i].NgayTH && (radGiaiQuyet.SelectedIndex == 0 || radGiaiQuyet.SelectedIndex == 2))
                            {
                                MessageBox.Show("Bệnh nhân có ngày ra viện bé hơn ngày chỉ định CLS");
                                return;
                            }
                            if (dt_NgayChuyen.DateTime < cls[i].NgayThang && (radGiaiQuyet.SelectedIndex == 0 || radGiaiQuyet.SelectedIndex == 2))
                            {
                                MessageBox.Show("Bệnh nhân có ngày ra viện bé hơn ngày chỉ định CLS");
                                return;
                            }
                        }
                    }

                }
                switch (TTTab)
                {
                    case 1:
                        #region  // lưu khám bệnh, kê đơn
                        int _IDDTBN = 0;
                        try
                        {
                            if (grvBNhankb.GetFocusedRowCellValue("IDDTBN") != null)
                                _IDDTBN = Convert.ToInt16(grvBNhankb.GetFocusedRowCellValue("IDDTBN"));
                        }
                        catch (Exception)
                        {
                            _IDDTBN = 0;
                        }
                        if (!KTraKB())
                            return;
                        DateTime ngaynhap = Convert.ToDateTime(grvBNhankb.GetFocusedRowCellValue(colNNhapkb));
                        if (DungChung.Ham.CheckNgay(ngaynhap, dtNgayKhamkb.DateTime))
                        {
                            #region  // lưu khám bệnh
                            if (!DungChung.Ham.KT_RaVien_ngt(_dataContext, _int_maBN))
                            {
                                //try
                                //{
                                var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderBy(p => p.IDKB).ToList();
                                if (ktkb.Count > 0)
                                    TTLuu = 2;
                                else TTLuu = 1;
                                if (TTLuu == 1)//tao moi
                                {
                                    #region thông báo bệnh nhân nội ngoại tỉnh _26007
                                    if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "01830")
                                    {

                                        //DataTable qbnhan = connect.FillDatatable("SELECT * FROM dbo.BenhNhan WHERE MaBNhan = '" + _int_maBN + "' AND DTuong = 'BHYT'", CommandType.Text);
                                        //if (qbnhan.Rows.Count > 0)
                                        //{
                                        //    int noitinh = Convert.ToInt32(qbnhan.Rows[0]["NoiTinh"].ToString());
                                        //    if (noitinh == 2)
                                        //        MessageBox.Show("Bệnh nhân nội tỉnh đến");
                                        //    else if (noitinh == 3)
                                        //        MessageBox.Show("Bệnh nhân ngoại tỉnh đến");
                                        //}

                                        var qbnhan = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).Where(p => p.DTuong == "BHYT").FirstOrDefault();
                                        if (qbnhan != null && qbnhan.NoiTinh != null)
                                        {

                                            if (qbnhan.NoiTinh.Value == 2)
                                                MessageBox.Show("Bệnh nhân nội tỉnh đến");
                                            else if (qbnhan.NoiTinh.Value == 3)
                                                MessageBox.Show("Bệnh nhân ngoại tỉnh đến");
                                        }

                                    }
                                    #endregion
                                    ktraHanThe();
                                    int ktkb1 = DungChung.Ham.KTQuaSoLanKB(_dataContext, lupNguoiKhamkb.EditValue.ToString(), 65, dtNgayKhamkb.DateTime);
                                    if (ktkb1 == 2)
                                    {
                                        MessageBox.Show("Bác sĩ: " + lupNguoiKhamkb.Text + ", đã khám quá số lần giới hạn: 65 lần/ngày");
                                        if (DungChung.Bien.MaBV == "27001")
                                            return;
                                    }
                                    else if (ktkb1 == 0)
                                    {
                                        MessageBox.Show("Bác sĩ: " + lupNguoiKhamkb.Text + ", chưa có chứng chỉ hành nghề, không thể lưu!");
                                        return;
                                    }

                                    var checkDThuoc = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1)
                                                       join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                                       where dtct.SoLuong > 0
                                                       select dtct).ToList();
                                    if (radGiaiQuyet.SelectedIndex == 0 && dt_NgayChuyen.EditValue != null && checkDThuoc.Exists(o => o.NgayNhap > dt_NgayChuyen.DateTime))
                                    {


                                        MessageBox.Show("Ngày ra viện không được nhỏ hơn ngày kê");
                                        return;



                                    }

                                    var qbnkb = (from bnkb in _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN)
                                                 join kp in _dataContext.KPhongs on bnkb.MaKP equals kp.MaKP
                                                 select new { bnkb, kp.TenKP }).OrderByDescending(p => p.bnkb.IDKB).ToList();
                                    if (qbnkb != null && qbnkb.Count > 0)
                                    {
                                        var bnkbOld = qbnkb.FirstOrDefault();
                                        if (bnkbOld.bnkb.NgayKham > dtNgayKhamkb.DateTime)
                                        {
                                            MessageBox.Show("Ngày khám không được nhỏ hơn ngày khám tại: " + bnkbOld.TenKP);
                                            return;
                                        }
                                        if (bnkbOld.bnkb.NgayNghi >= dtNgayKhamkb.DateTime)
                                        {
                                            MessageBox.Show("Ngày khám không được nhỏ hơn ngày chuyển tại: " + bnkbOld.TenKP);
                                            return;
                                        }
                                    }

                                    nhapcannangTEduoi1tuoi(_dataContext, _int_maBN);
                                    if (radGiaiQuyet.SelectedIndex < 0)
                                        radGiaiQuyet.SelectedIndex = 4;
                                    BNKB KhamBenh = new BNKB();
                                    KhamBenh.MaBNhan = _int_maBN;
                                    KhamBenh.NgayKham = dtNgayKhamkb.DateTime;
                                    KhamBenh.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                    KhamBenh.LyDoKham = txtLyDoKham.Text;
                                    KhamBenh.DHLS = txtDauHieuLamSang.Text;
                                    KhamBenh.MaCK = Convert.ToInt32(lup_ChuyenKhoa.EditValue);
                                    if (lupChanDoanKb.EditValue != null && lupChanDoanKb.EditValue != null)
                                    {
                                        KhamBenh.MaICD = lupMaICDkb.EditValue.ToString();
                                        KhamBenh.ChanDoan = (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24297") ? txtBenhChinh.Text : lupChanDoanKb.Text;
                                        if (DungChung.Bien.MaBV == "20001")
                                        {
                                            KhamBenh.MaYHCT = DungChung.Ham.GetMaYHCT(lupMaICDkb.Text, _licd10)[0];
                                            KhamBenh.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(lupMaICDkb.Text, _licd10)[1];
                                        }
                                    }
                                    else
                                    {
                                        KhamBenh.MaICD = "";
                                        KhamBenh.ChanDoan = "";
                                        if (DungChung.Bien.MaBV == "20001")
                                        {
                                            KhamBenh.MaYHCT = "";
                                            KhamBenh.ChanDoanYHCT = "";
                                        }

                                    }


                                    if (DungChung.Bien.MaBV == "14017")
                                    {
                                        KhamBenh.MaICD2 = lupKhac.EditValue.ToString().Trim();//.Replace(", ", ";");
                                    }

                                    else
                                    {
                                        if (LupICD2.EditValue != null)
                                            icd[0] = LupICD2.EditValue.ToString();
                                        if (LupICD3.EditValue != null)
                                            icd[1] = LupICD3.EditValue.ToString();
                                        if (LupICD4.EditValue != null)
                                            icd[2] = LupICD4.Text.ToString();
                                        string maicd = string.Join(";", icd);
                                        KhamBenh.MaICD2 = maicd;
                                    }


                                    if (DungChung.Bien.MaBV == "20001")
                                    {
                                        KhamBenh.MaYHCT2 = DungChung.Ham.GetMaYHCT(KhamBenh.MaICD2, _licd10)[0];
                                        KhamBenh.BenhKhacYHCT = DungChung.Ham.GetMaYHCT(KhamBenh.MaICD2, _licd10)[1];
                                    }
                                    string _benhphu = "";
                                    //dungtt 090317
                                    if (DungChung.Bien.MaBV == "30002")
                                    {
                                        _benhphu = "(Bệnh phụ)";
                                        if (txtBenhKhac2.Text.Trim().Length > 0)
                                            benhkhac[0] = txtBenhKhac2.Text.Trim() + _benhphu;
                                        if (txtBenhKhac3.Text.Trim().Length > 0)
                                            benhkhac[1] = txtBenhKhac3.Text.Trim() + _benhphu;
                                        if (txtBenhPhu.Text.Trim().Length > 0)
                                            benhkhac[2] = txtBenhPhu.Text.Trim() + _benhphu;
                                        string maicd1 = string.Join(";", benhkhac);
                                        KhamBenh.BenhKhac = maicd1;
                                    }
                                    else if (DungChung.Bien.MaBV == "14017")
                                    {
                                        KhamBenh.BenhKhac = txtBenhKhac1.Text.Trim();//.Replace(", ", ";");
                                    }
                                    else if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "24012")
                                    {
                                        benhkhac[0] = txtBenhPhu2.Text.Trim() + _benhphu;
                                        benhkhac[1] = txtBenhPhu3.Text.Trim() + _benhphu;
                                        benhkhac[2] = txtBenhPhu.Text.Trim() + _benhphu;
                                        string maicd1 = string.Join(";", benhkhac);
                                        KhamBenh.BenhKhac = maicd1;
                                    }
                                    else
                                    {
                                        benhkhac[0] = txtBenhKhac2.Text.Trim() + _benhphu;
                                        benhkhac[1] = txtBenhKhac3.Text.Trim() + _benhphu;
                                        benhkhac[2] = txtBenhPhu.Text.Trim() + _benhphu;
                                        string maicd1 = string.Join(";", benhkhac);
                                        KhamBenh.BenhKhac = maicd1;
                                    }

                                    // if (!string.IsNullOrEmpty(txtBenhBĐ.Text))
                                    //KhamBenh.ChanDoanBD = txtBenhBĐ.Text + "|" + ((cboMaICDBĐ.Text.Trim() == "" && cboMaICDBĐ.Text == null) ? "" : cboMaICDBĐ.Text);
                                    if (!string.IsNullOrEmpty(mmChanDoanBD.Text))
                                        KhamBenh.ChanDoanBD = mmChanDoanBD.Text;

                                    KhamBenh.GhiChu = txtGhiChu.Text;
                                    KhamBenh.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                    macbkb = lupNguoiKhamkb.EditValue.ToString();
                                    KhamBenh.Buong = "";
                                    KhamBenh.Giuong = "";
                                    KhamBenh.PhuongAn = radGiaiQuyet.SelectedIndex;
                                    if (radGiaiQuyet.SelectedIndex >= 0 && radGiaiQuyet.SelectedIndex <= 3)
                                    {
                                        if (!string.IsNullOrEmpty(dt_NgayChuyen.Text))
                                            KhamBenh.NgayNghi = dt_NgayChuyen.DateTime;
                                        if (radGiaiQuyet.SelectedIndex == 1 || radGiaiQuyet.SelectedIndex == 3)
                                        {
                                            if (!string.IsNullOrEmpty(lupKhoaDT.Text))
                                            {
                                                KhamBenh.MaKPdt = Convert.ToInt32(lupKhoaDT.EditValue);
                                            }
                                        }
                                    }

                                    if (radGiaiQuyet.SelectedIndex == 4)
                                    {
                                        KhamBenh.MaKPdt = 0;
                                        KhamBenh.NgayNghi = null;
                                    }
                                    //if (!string.IsNullOrEmpty(txtLoiDan.Text))
                                    //    KhamBenh.LoiGian = txtLoiDan.Text;
                                    int _status = 1;

                                    _dataContext.BNKBs.Add(KhamBenh);

                                    if (_dataContext.SaveChanges() >= 0)
                                    {
                                        txtIdkb.Text = KhamBenh.IDKB.ToString();
                                        idbnkb = KhamBenh.IDKB;
                                        int maxidKB = idbnkb;// _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).ToList().Max(p => p.IDKB);
                                        int _chuyenkhoa = 0;
                                        if (!string.IsNullOrEmpty(lupKhoaKhamkb.Text))
                                            _chuyenkhoa = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                        if (radGiaiQuyet.SelectedIndex == 3 || radGiaiQuyet.SelectedIndex == 1) //|| radGiaiQuyet.SelectedIndex == 4)khám phối hợp ko thay đổi MaKP trong bảng BenhNhan
                                        {
                                            if (!string.IsNullOrEmpty(lupKhoaDT.Text))
                                                _chuyenkhoa = Convert.ToInt32(lupKhoaDT.EditValue);
                                            //if (radGiaiQuyet.SelectedIndex == 3)// || radGiaiQuyet.SelectedIndex==4)
                                            //{
                                            //    _status = 0;
                                            //}
                                        }
                                        Ham._setStatus(_int_maBN, _status, _chuyenkhoa);
                                        // tính công khám
                                        // theo TT37 áp dụng ngàu 01/03/2016
                                        if (dtNgayKhamkb.DateTime > DungChung.Ham.NgayDen(Convert.ToDateTime("29/02/2016")))
                                        {
                                            DataRow[] foundRows;
                                            foundRows = tbBenhnhan.Select("MaBNhan = '" + _int_maBN + "' and NoiTru = 0");
                                            // if (_lTKbn.Where(p => p.MaBNhan == _int_maBN).Where(p => p.NoiTru == 0).ToList().Count >= 1)
                                            if (foundRows.Length > 0)
                                            {
                                                //var checkpa = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PhuongAn == 4 || p.PhuongAn == 1).ToList();
                                                //if (checkpa.Count == 0)
                                                //if (DungChung.Bien.MaBV == "30303")
                                                //{
                                                //    DungChung.Ham.Update_Delete_CongKham(_int_maBN, _idkbold, false, dtNgayKhamkb.DateTime);
                                                //}
                                                DungChung.Ham.Update_Delete_CongKham(_int_maBN, maxidKB, true, dtNgayKhamkb.DateTime, chkKhamChuyenGia.Checked);
                                            }

                                        }
                                        xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                                        _listBNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                        #region ADO
                                        //if (connect.isConnect)
                                        //{
                                        //    string strsql = "SELECT * FROM dbo.BNKB WHERE MaBNhan = " + _int_maBN + " ORDER BY IDKB desc";

                                        //    tbKB = connect.FillDatatable(strsql, CommandType.Text);
                                        //}
                                        #endregion
                                        grcChuyenKhoa.DataSource = null;
                                        grcChuyenKhoa.DataSource = _listBNKB; //tbKB;
                                        DungChung.Bien.CoTheChuyen = true;
                                        //if (DungChung.Bien.MaBV == "27001")
                                        //    MessageBox.Show("Thêm mới thành công");
                                        TTLuu = 0;
                                        if (radGiaiQuyet.SelectedIndex == 1)
                                        {
                                            var ktvv = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                            if (ktvv.Count <= 0)
                                            {
                                                mnKBVaoVien_ItemClick(sender, e);
                                            }
                                        }
                                        else
                                        {
                                            if (radGiaiQuyet.SelectedIndex == 2)
                                            {
                                                var ktrv = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                                if (ktrv.Count <= 0)
                                                {
                                                    mnChuyenVien_ItemClick(sender, e);
                                                }
                                                //}
                                            }
                                            else if (radGiaiQuyet.SelectedIndex == 0)
                                            {
                                                var bn = (from bnkb in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                                                          select new { bnkb.Status }).ToList();
                                                if ((DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "30004" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(15) > DateTime.Now))
                                                {
                                                    string time = "Thời gian khám bệnh của bệnh nhân < 15 phút (" + (DateTime.Now - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p) \nBạn có muốn tiếp tục?";
                                                    DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                    if (dr == DialogResult.No)
                                                        return;
                                                }
                                                else if (((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && (radGiaiQuyet.SelectedIndex == 0 || radGiaiQuyet.SelectedIndex == 2)) && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(5) > dt_NgayChuyen.DateTime))
                                                {
                                                    string time = "Thời gian khám bệnh của bệnh nhân < 5 phút (" + (dt_NgayChuyen.DateTime - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p)";
                                                    DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                                    if (dr == DialogResult.OK)
                                                        return;
                                                }
                                                if (!DungChung.Ham._LuuXoaRaVien(_dataContext, _int_maBN, dt_NgayChuyen.DateTime, "Luu", 2))
                                                {
                                                    KhamBenh.PhuongAn = 4;
                                                    _dataContext.SaveChanges();
                                                }
                                            }
                                            else
                                            {

                                                DungChung.Ham._LuuXoaRaVien(_dataContext, _int_maBN, DateTime.Now, "Xoa", 2);
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (TTLuu == 2) // Sửa
                                    {
                                        if (TTLuuKB == false)
                                        {
                                            //var ktsua = _dataContext.BNKBs.Where(p => p.MaBNhan== (txtMaBNhan.Text)).ToList();
                                            if (!string.IsNullOrEmpty(txtIdkb.Text))
                                            {
                                                int id = Int32.Parse(txtIdkb.Text);
                                                int _makp = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);

                                                int _mack = -1;
                                                if (lupKhoaKhamkb.EditValue != null)
                                                    _mack = Convert.ToInt32(lup_ChuyenKhoa.EditValue);
                                                #region his 1988- 061218- dungtt
                                                var qkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                                if (qkb.Count > 0)
                                                {
                                                    var qkbht = qkb.Where(p => p.IDKB == id).FirstOrDefault();
                                                    if (qkbht != null)
                                                    {
                                                        if (id != qkb.First().IDKB && qkbht.NgayNghi != null && dt_NgayChuyen.Text != "" && qkbht.NgayNghi.Value != dt_NgayChuyen.DateTime)
                                                        {
                                                            MessageBox.Show("Bạn không được sửa ngày chuyển");
                                                            TTLuuKB = false;
                                                            xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                                                            return;
                                                        }
                                                    }
                                                }
                                                #endregion
                                                var qMaxKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN && p.IDKB != id).OrderByDescending(p => p.IDKB).FirstOrDefault();
                                                if (qMaxKB != null)
                                                {
                                                    if (qMaxKB.MaKP == _makp)
                                                    {
                                                        MessageBox.Show("Bệnh nhân đã được khám tại " + lupKhoaKhamkb.Text + " . Bạn không thể chuyển bệnh nhan về khoa " + lupKhoaKhamkb.Text);

                                                        TTLuuKB = false;
                                                        xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                                                        return;
                                                    }
                                                }

                                                if (DungChung.Bien.MaBV == "24012" && radGiaiQuyet.SelectedIndex == 1)
                                                {
                                                    var ktvv = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                                    bool vvngoaitru = false;
                                                    if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && ktvv.Count > 0)
                                                    {
                                                        int? ma = ktvv.First().MaKP;
                                                        var checkkp = _dataContext.KPhongs.Where(p => p.MaKP == ma).FirstOrDefault();
                                                        if (checkkp.PLoai == "Phòng khám")
                                                        {
                                                            vvngoaitru = true;
                                                        }
                                                    }

                                                    if (ktvv.Count > 0 || vvngoaitru)
                                                    {
                                                        MessageBox.Show("Bệnh nhân đã vào viện ngoại trú");
                                                        return;
                                                    }

                                                }

                                                int ktkb1 = DungChung.Ham.KTQuaSoLanKB(_dataContext, lupNguoiKhamkb.EditValue.ToString(), 65, dtNgayKhamkb.DateTime);
                                                if (ktkb1 == 2)
                                                {
                                                    MessageBox.Show("Bác sĩ: " + lupNguoiKhamkb.Text + ", đã khám quá số lần giới hạn: 65 lần/ngày");
                                                    if (DungChung.Bien.MaBV == "27001")
                                                        return;
                                                }
                                                else if (ktkb1 == 0)
                                                {
                                                    MessageBox.Show("Bác sĩ: " + lupNguoiKhamkb.Text + ", chưa có chứng chỉ hành nghề, không thể lưu!");
                                                    return;
                                                }

                                                var checkDThuoc = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1)
                                                                   join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                                                   where dtct.SoLuong > 0
                                                                   select dtct).ToList();
                                                if (radGiaiQuyet.SelectedIndex == 0 && dt_NgayChuyen.EditValue != null && checkDThuoc.Exists(o => o.NgayNhap > dt_NgayChuyen.DateTime))
                                                {

                                                    MessageBox.Show("Ngày ra viện không được nhỏ hơn ngày kê");
                                                    return;

                                                }

                                                var qbnkb = (from bnkb in _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN)
                                                             join kp in _dataContext.KPhongs on bnkb.MaKP equals kp.MaKP
                                                             select new { bnkb, kp.TenKP }).OrderByDescending(p => p.bnkb.IDKB).ToList();
                                                var checkBNKB = qbnkb.FirstOrDefault(o => o.bnkb.IDKB == id);
                                                if (checkBNKB != null)
                                                {
                                                    var bnkbOld = qbnkb.Where(o => o.bnkb.IDKB < id).OrderByDescending(p => p.bnkb.IDKB);
                                                    if (bnkbOld != null && bnkbOld.Count() > 0)
                                                    {
                                                        if (bnkbOld.FirstOrDefault().bnkb.NgayKham > dtNgayKhamkb.DateTime)
                                                        {
                                                            MessageBox.Show("Ngày khám không được nhỏ hơn ngày khám tại: " + bnkbOld.FirstOrDefault().TenKP);
                                                            return;
                                                        }
                                                        if (bnkbOld.FirstOrDefault().bnkb.NgayNghi >= dtNgayKhamkb.DateTime)
                                                        {
                                                            MessageBox.Show("Ngày khám không được nhỏ hơn ngày chuyển tại: " + bnkbOld.FirstOrDefault().TenKP);
                                                            return;
                                                        }
                                                    }
                                                }
                                                var bn = (from bnkb in _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                                                          select new { bnkb.Status }).ToList();

                                                if (radGiaiQuyet.SelectedIndex == 0 && (DungChung.Bien.MaBV == "30010" || DungChung.Bien.MaBV == "30004") && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(15) > dt_NgayChuyen.DateTime))
                                                {
                                                    string time = "Thời gian khám bệnh của bệnh nhân < 15 phút (" + (DateTime.Now - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p) \nBạn có muốn tiếp tục?";
                                                    DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                    if (dr == DialogResult.No)
                                                        return;
                                                }
                                                else if (((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && (radGiaiQuyet.SelectedIndex == 0 || radGiaiQuyet.SelectedIndex == 2)) && (Convert.ToDateTime(ktkb.First().NgayKham).AddMinutes(5) > dt_NgayChuyen.DateTime))
                                                {
                                                    string time = "Thời gian khám bệnh của bệnh nhân < 5 phút (" + (dt_NgayChuyen.DateTime - Convert.ToDateTime(ktkb.First().NgayKham)).Minutes + "p)";
                                                    DialogResult dr = MessageBox.Show(time, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                                    if (dr == DialogResult.OK)
                                                        return;
                                                }
                                                //if (!DungChung.Ham.KTQuaSoLanKB(data, lupNguoiKhamkb.EditValue.ToString(), 65, dtNgayKhamkb.DateTime))
                                                //    MessageBox.Show("Bác sĩ: " + lupNguoiKhamkb.Text + ", đã khám quá số lần giới hạn: 65 lần/ngày");
                                                BNKB KhamBenh = _dataContext.BNKBs.Single(p => p.IDKB == id);
                                                KhamBenh.MaBNhan = _int_maBN;
                                                KhamBenh.NgayKham = dtNgayKhamkb.DateTime;
                                                KhamBenh.MaCK = _mack;
                                                KhamBenh.MaKP = _makp;
                                                KhamBenh.GhiChu = txtGhiChu.Text;
                                                KhamBenh.LyDoKham = txtLyDoKham.Text;
                                                KhamBenh.DHLS = txtDauHieuLamSang.Text;
                                                if (lupChanDoanKb.EditValue != null && lupChanDoanKb.EditValue != null)
                                                {
                                                    KhamBenh.MaICD = lupMaICDkb.EditValue.ToString();
                                                    KhamBenh.ChanDoan = (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24297") ? txtBenhChinh.Text : lupChanDoanKb.Text;
                                                    if (DungChung.Bien.MaBV == "20001")
                                                    {
                                                        KhamBenh.MaYHCT = DungChung.Ham.GetMaYHCT(lupMaICDkb.Text, _licd10)[0];
                                                        KhamBenh.ChanDoanYHCT = DungChung.Ham.GetMaYHCT(lupMaICDkb.Text, _licd10)[1];
                                                    }
                                                }
                                                else
                                                {
                                                    KhamBenh.MaICD = "";
                                                    KhamBenh.ChanDoan = "";
                                                    if (DungChung.Bien.MaBV == "20001")
                                                    {
                                                        KhamBenh.MaYHCT = "";
                                                        KhamBenh.ChanDoanYHCT = "";
                                                    }
                                                }


                                                if (DungChung.Bien.MaBV == "14017")
                                                {
                                                    KhamBenh.MaICD2 = lupKhac.EditValue.ToString().Trim();//.Replace(", ", ";");
                                                }
                                                else
                                                {
                                                    if (LupICD2.EditValue != null)
                                                        icd[0] = LupICD2.EditValue.ToString();
                                                    if (LupICD3.EditValue != null)
                                                        icd[1] = LupICD3.EditValue.ToString();
                                                    if (LupICD4.EditValue != null)
                                                        icd[2] = LupICD4.Text.ToString();
                                                    string maicd = string.Join(";", icd);
                                                    KhamBenh.MaICD2 = maicd;
                                                }

                                                string _benhphu = "";
                                                if (DungChung.Bien.MaBV == "30002")
                                                {
                                                    _benhphu = "(Bệnh phụ)";
                                                    if (txtBenhKhac2.Text.Trim().Length > 0 && !txtBenhKhac2.Text.Contains(_benhphu))
                                                        benhkhac[0] = txtBenhKhac2.Text.Trim() + _benhphu;
                                                    else
                                                        benhkhac[0] = txtBenhKhac2.Text.Trim();
                                                    if (txtBenhKhac3.Text.Trim().Length > 0 && !txtBenhKhac3.Text.Contains(_benhphu))
                                                        benhkhac[1] = txtBenhKhac3.Text.Trim() + _benhphu;
                                                    else
                                                        benhkhac[1] = txtBenhKhac3.Text.Trim();
                                                    if (txtBenhPhu.Text.Trim().Length > 0 && !txtBenhPhu.Text.Contains(_benhphu))
                                                        benhkhac[2] = txtBenhPhu.Text.Trim() + _benhphu;
                                                    else
                                                        benhkhac[2] = txtBenhPhu.Text.Trim();
                                                    string maicd1 = string.Join(";", benhkhac);
                                                    KhamBenh.BenhKhac = maicd1;
                                                }
                                                else if (DungChung.Bien.MaBV == "24012")
                                                {

                                                    benhkhac[0] = txtBenhPhu2.Text.Trim() + _benhphu;
                                                    benhkhac[1] = txtBenhPhu3.Text.Trim() + _benhphu;
                                                    benhkhac[2] = txtBenhPhu.Text.Trim() + _benhphu;
                                                    string maicd1 = string.Join(";", benhkhac);
                                                    KhamBenh.BenhKhac = maicd1;
                                                }
                                                else if (DungChung.Bien.MaBV == "14017")
                                                {
                                                    KhamBenh.BenhKhac = txtBenhKhac1.Text.Trim();//.Replace(", ", ";");
                                                }
                                                else if (DungChung.Bien.MaBV == "24297")
                                                {
                                                    benhkhac[0] = txtBenhPhu2.Text.Trim() + _benhphu;
                                                    benhkhac[1] = txtBenhPhu3.Text.Trim() + _benhphu;
                                                    benhkhac[2] = txtBenhPhu.Text.Trim() + _benhphu;
                                                    string maicd1 = string.Join(";", benhkhac);
                                                    KhamBenh.BenhKhac = maicd1;
                                                }
                                                else
                                                {
                                                    benhkhac[0] = txtBenhKhac2.Text.Trim();
                                                    benhkhac[1] = txtBenhKhac3.Text.Trim();

                                                    benhkhac[2] = txtBenhPhu.Text.Trim() + _benhphu;
                                                    string maicd1 = string.Join(";", benhkhac);
                                                    KhamBenh.BenhKhac = maicd1;
                                                }

                                                // if (!string.IsNullOrEmpty(txtBenhBĐ.Text))
                                                //KhamBenh.ChanDoanBD = txtBenhBĐ.Text + "|" + ((cboMaICDBĐ.Text.Trim() == "" && cboMaICDBĐ.Text == null) ? "" : cboMaICDBĐ.Text);
                                                if (!string.IsNullOrEmpty(mmChanDoanBD.Text))
                                                    KhamBenh.ChanDoanBD = mmChanDoanBD.Text;

                                                if (DungChung.Bien.MaBV == "20001")
                                                {
                                                    KhamBenh.MaYHCT2 = DungChung.Ham.GetMaYHCT(KhamBenh.MaICD2, _licd10)[0];
                                                    KhamBenh.BenhKhacYHCT = DungChung.Ham.GetMaYHCT(KhamBenh.MaICD2, _licd10)[1];
                                                }
                                                KhamBenh.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                                KhamBenh.PhuongAn = radGiaiQuyet.SelectedIndex;
                                                if (radGiaiQuyet.SelectedIndex >= 0 && radGiaiQuyet.SelectedIndex <= 3)
                                                {
                                                    if (!string.IsNullOrEmpty(dt_NgayChuyen.Text))
                                                        KhamBenh.NgayNghi = dt_NgayChuyen.DateTime;
                                                    if (radGiaiQuyet.SelectedIndex == 1 || radGiaiQuyet.SelectedIndex == 3)
                                                    {
                                                        if (!string.IsNullOrEmpty(lupKhoaDT.Text))
                                                        {
                                                            KhamBenh.MaKPdt = Convert.ToInt32(lupKhoaDT.EditValue);
                                                        }

                                                    }
                                                }

                                                if (radGiaiQuyet.SelectedIndex == 4)
                                                {
                                                    var vv = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                                                    if (vv == null)
                                                    {
                                                        KhamBenh.MaKPdt = 0;
                                                        KhamBenh.NgayNghi = null;
                                                    }

                                                }
                                                if (_dataContext.SaveChanges() >= 0)
                                                {
                                                    nhapcannangTEduoi1tuoi(_dataContext, _int_maBN);


                                                    xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                                                    txtIdkb.Text = KhamBenh.IDKB.ToString();
                                                    idbnkb = KhamBenh.IDKB;
                                                    int _status = 1;
                                                    int _chuyenkhoa = 0;
                                                    if (lupKhoaKhamkb.EditValue != null)
                                                        _chuyenkhoa = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                    if (radGiaiQuyet.SelectedIndex == 3 || radGiaiQuyet.SelectedIndex == 1) //|| radGiaiQuyet.SelectedIndex==4) khám phối hợp ko thay đổi MaKP trong bảng BenhNhan
                                                    {
                                                        if (!string.IsNullOrEmpty(lupKhoaDT.Text))
                                                            _chuyenkhoa = Convert.ToInt32(lupKhoaDT.EditValue);

                                                        if (radGiaiQuyet.SelectedIndex == 3)// || radGiaiQuyet.SelectedIndex == 4)
                                                        {
                                                            _status = 0;
                                                        }
                                                        //if (radGiaiQuyet.SelectedIndex == 2)// || radGiaiQuyet.SelectedIndex == 4)
                                                        //{
                                                        //    _status = 2;
                                                        //}

                                                    }
                                                    if (radGiaiQuyet.SelectedIndex == 2)
                                                    {
                                                        var ktrv = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                                        if (ktrv.Count <= 0)
                                                        {
                                                            mnChuyenVien_ItemClick(sender, e);

                                                        }
                                                        //}
                                                    }

                                                    else if (radGiaiQuyet.SelectedIndex == 0)
                                                    {
                                                        if (DungChung.Ham._LuuXoaRaVien(_dataContext, _int_maBN, dt_NgayChuyen.DateTime, "Luu", 2))
                                                            _status = 2;
                                                    }
                                                    else if (radGiaiQuyet.SelectedIndex == 1)
                                                    {

                                                        // kiểm tra chuyển viện nếu có thì xóa
                                                        var ktcv = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN).Where(p => p.Status == 1).FirstOrDefault();
                                                        if (ktcv != null)
                                                        {

                                                            _dataContext.RaViens.Remove(ktcv);
                                                            _dataContext.SaveChanges();
                                                        }
                                                        var ktvv = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();

                                                        if (ktvv.Count <= 0)
                                                        {
                                                            mnKBVaoVien_ItemClick(sender, e);
                                                            _chuyenkhoa = Convert.ToInt32(lupKhoaDT.EditValue);
                                                        }

                                                    }
                                                    else
                                                    {

                                                        DungChung.Ham._LuuXoaRaVien(_dataContext, _int_maBN, DateTime.Now, "Xoa", 2);
                                                    }
                                                    DungChung.Bien.CoTheChuyen = true;
                                                    TTLuu = 0;
                                                    DungChung.Ham.Update_Delete_CongKham(_int_maBN, KhamBenh.IDKB, true, dtNgayKhamkb.DateTime, chkKhamChuyenGia.Checked);
                                                    Ham._setStatus(_int_maBN, _status, _chuyenkhoa);
                                                }


                                            }
                                        }
                                        else // lưu khám bệnh mới BN chuyển PK
                                        {
                                            int makp = 0;
                                            if (lupKhoaKhamkb.EditValue != null)
                                                makp = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                            bool _ktkb = true;
                                            var qMaxKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).FirstOrDefault();
                                            int _idkbold = 0;//khám bệnh của khoa phòng cũ gần nhất

                                            if (qMaxKB == null)
                                            {
                                                _ktkb = true;
                                            }
                                            else
                                            {
                                                _idkbold = qMaxKB.IDKB;
                                                //var ktkpkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.MaKP == makp).ToList();

                                                if (qMaxKB.MaKP == makp)
                                                {
                                                    MessageBox.Show("Bệnh nhân đã được khám tại " + lupKhoaKhamkb.Text + " . Bạn không thể thêm mới khám bệnh cho bệnh nhân tại khoa đang điều trị");
                                                    //DialogResult _result = MessageBox.Show("Bệnh nhân đã được khám tại: '" + lupKhoaKhamkb.Text + "' bạn vẫn muốn tạo", "Hỏi khám bệnh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                    //if (_result == DialogResult.Yes)
                                                    //{
                                                    //    _ktkb = true;
                                                    //}
                                                    //else
                                                    //{
                                                    TTLuuKB = true;
                                                    _ktkb = false;
                                                    xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                                                    return;
                                                    // }

                                                }
                                            }
                                            if (_ktkb == true)
                                            {

                                                var checkDThuoc = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1)
                                                                   join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                                                   where dtct.SoLuong > 0
                                                                   select dtct).ToList();
                                                if (radGiaiQuyet.SelectedIndex == 0 && dt_NgayChuyen.EditValue != null && checkDThuoc.Exists(o => o.NgayNhap > dt_NgayChuyen.DateTime))
                                                {

                                                    MessageBox.Show("Ngày ra viện không được nhỏ hơn ngày kê");
                                                    return;

                                                }

                                                var qbnkb = (from bnkb in _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN)
                                                             join kp in _dataContext.KPhongs on bnkb.MaKP equals kp.MaKP
                                                             select new { bnkb, kp.TenKP }).OrderByDescending(p => p.bnkb.IDKB).ToList();
                                                if (qbnkb != null && qbnkb.Count > 0)
                                                {
                                                    var bnkbOld = qbnkb.FirstOrDefault();
                                                    if (bnkbOld.bnkb.NgayKham >= dtNgayKhamkb.DateTime)
                                                    {
                                                        MessageBox.Show("Ngày khám không được nhỏ hơn ngày khám tại: " + bnkbOld.TenKP);
                                                        return;
                                                    }

                                                    if (bnkbOld.bnkb.NgayNghi >= dtNgayKhamkb.DateTime)
                                                    {
                                                        MessageBox.Show("Ngày khám không được nhỏ hơn ngày chuyển tại: " + bnkbOld.TenKP);
                                                        return;
                                                    }
                                                }

                                                BNKB KhamBenh = new BNKB();
                                                KhamBenh.MaBNhan = _int_maBN;
                                                KhamBenh.NgayKham = dtNgayKhamkb.DateTime;
                                                KhamBenh.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);

                                                KhamBenh.MaCK = Convert.ToInt32(lup_ChuyenKhoa.EditValue);
                                                if (lupChanDoanKb.EditValue != null && lupChanDoanKb.EditValue.ToString() != "")
                                                {
                                                    KhamBenh.MaICD = lupMaICDkb.EditValue.ToString();
                                                    KhamBenh.ChanDoan = (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24297") ? txtBenhChinh.Text : lupChanDoanKb.Text.Trim();
                                                }
                                                else
                                                {
                                                    KhamBenh.MaICD = "";
                                                    KhamBenh.ChanDoan = "";
                                                }
                                                if (DungChung.Bien.MaBV == "14017")
                                                {
                                                    KhamBenh.MaICD2 = lupKhac.EditValue.ToString().Trim();//.Replace(", ", ";");
                                                }
                                                else
                                                {
                                                    if (LupICD2.EditValue != null)
                                                        icd[0] = LupICD2.EditValue.ToString();
                                                    if (LupICD3.EditValue != null)
                                                        icd[1] = LupICD3.EditValue.ToString();
                                                    if (LupICD4.EditValue != null)
                                                        icd[2] = LupICD4.EditValue.ToString();
                                                    string maicd = string.Join(";", icd);
                                                    KhamBenh.MaICD2 = maicd;
                                                }

                                                string _benhphu = "";
                                                if (DungChung.Bien.MaBV == "30002")
                                                {
                                                    _benhphu = "(Bệnh phụ)";
                                                    if (txtBenhKhac2.Text.Trim().Length > 0)
                                                        benhkhac[0] = txtBenhKhac2.Text.Trim() + _benhphu;
                                                    if (txtBenhKhac3.Text.Trim().Length > 0)
                                                        benhkhac[1] = txtBenhKhac3.Text.Trim() + _benhphu;
                                                    if (txtBenhKhac4.Text.Trim().Length > 0)
                                                        benhkhac[2] = txtBenhKhac4.Text.Trim() + _benhphu;
                                                    string maicd = string.Join(";", benhkhac);
                                                    KhamBenh.BenhKhac = maicd;
                                                }
                                                else if (DungChung.Bien.MaBV == "14017")
                                                {
                                                    KhamBenh.BenhKhac = txtBenhKhac1.Text.Trim();//.Replace(", ", ";");
                                                }
                                                else if (DungChung.Bien.MaBV == "24297")
                                                {
                                                    benhkhac[0] = txtBenhPhu2.Text.Trim() + _benhphu;
                                                    benhkhac[1] = txtBenhPhu3.Text.Trim() + _benhphu;
                                                    benhkhac[2] = txtBenhPhu.Text.Trim() + _benhphu;
                                                    string maicd1 = string.Join(";", benhkhac);
                                                    KhamBenh.BenhKhac = maicd1;
                                                }
                                                else
                                                {
                                                    benhkhac[0] = txtBenhKhac2.Text.Trim() + _benhphu;
                                                    benhkhac[1] = txtBenhKhac3.Text.Trim() + _benhphu;
                                                    benhkhac[2] = txtBenhPhu.Text.Trim() + _benhphu;
                                                    string maicd = string.Join(";", benhkhac);
                                                    KhamBenh.BenhKhac = maicd;
                                                }

                                                //if (!string.IsNullOrEmpty(txtBenhBĐ.Text) && !string.IsNullOrEmpty(cboMaICDBĐ.Text))
                                                //    KhamBenh.ChanDoanBD = txtBenhBĐ.Text + "|" + cboMaICDBĐ.Text;

                                                KhamBenh.GhiChu = txtGhiChu.Text;
                                                KhamBenh.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                                KhamBenh.PhuongAn = radGiaiQuyet.SelectedIndex;
                                                if (!string.IsNullOrEmpty(mmChanDoanBD.Text))
                                                    KhamBenh.ChanDoanBD = mmChanDoanBD.Text;
                                                if (radGiaiQuyet.SelectedIndex == 1 || radGiaiQuyet.SelectedIndex == 3 || radGiaiQuyet.SelectedIndex == 4)
                                                {
                                                    if (lupKhoaDT.EditValue != null)
                                                    {
                                                        KhamBenh.MaKPdt = Convert.ToInt32(lupKhoaDT.EditValue);
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Chưa chọn khoa phòng", "Thông báo", MessageBoxButtons.OK);
                                                        lupKhoaDT.Focus();
                                                        break;
                                                    }
                                                }
                                                //if (!string.IsNullOrEmpty(txtLoiDan.Text))
                                                //    KhamBenh.LoiGian = txtLoiDan.Text;
                                                _dataContext.BNKBs.Add(KhamBenh);
                                                if (_dataContext.SaveChanges() >= 0)
                                                {
                                                    xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                                                    txtIdkb.Text = KhamBenh.IDKB.ToString();
                                                    idbnkb = KhamBenh.IDKB;
                                                    if (dtNgayKhamkb.DateTime > DungChung.Ham.NgayDen(Convert.ToDateTime("29/02/2016")))
                                                    {
                                                        DataRow[] foundRows;
                                                        foundRows = tbBenhnhan.Select("MaBNhan = '" + _int_maBN + "' and NoiTru = 0");
                                                        // if (_lTKbn.Where(p => p.MaBNhan == _int_maBN).Where(p => p.NoiTru == 0).ToList().Count >= 1)
                                                        if (foundRows.Length > 0)
                                                        {
                                                            int maxidKB = idbnkb;
                                                            //var checkPa = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PhuongAn == 4).ToList();
                                                            //if (checkPa.Count == 0)
                                                            if (DungChung.Bien.MaBV == "30303")
                                                            {
                                                                DungChung.Ham.Update_Delete_CongKham(_int_maBN, _idkbold, false, dtNgayKhamkb.DateTime);
                                                            }
                                                            DungChung.Ham.Update_Delete_CongKham(_int_maBN, maxidKB, true, dtNgayKhamkb.DateTime, chkKhamChuyenGia.Checked);
                                                        }

                                                    }
                                                    int _status = 1;
                                                    int _chuyenkhoa = 0;
                                                    if (lupKhoaKhamkb.EditValue != null)
                                                        _chuyenkhoa = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                    if (radGiaiQuyet.SelectedIndex == 1 || radGiaiQuyet.SelectedIndex == 3)
                                                    {
                                                        if (!string.IsNullOrEmpty(lupKhoaDT.Text))
                                                            _chuyenkhoa = Convert.ToInt32(lupKhoaDT.EditValue);
                                                        if (radGiaiQuyet.SelectedIndex == 3)
                                                        {
                                                            _status = 0;
                                                        }
                                                    }
                                                    if (radGiaiQuyet.SelectedIndex == 2)
                                                    {
                                                        _status = 2;
                                                    }
                                                    Ham._setStatus(_int_maBN, _status, _chuyenkhoa);
                                                    // string strsql = "SELECT * FROM dbo.BNKB WHERE MaBNhan = " + _int_maBN + " ORDER BY IDKB desc";

                                                    //tbKB = connect.FillDatatable(strsql, CommandType.Text);
                                                    _listBNKB = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                                    grcChuyenKhoa.DataSource = _listBNKB; //tbKB;

                                                    xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                                                    DungChung.Bien.CoTheChuyen = true;
                                                    //EnableControlKB(false);
                                                    //EnableButton(true);
                                                    TTLuu = 0;
                                                }
                                                var query2 = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                                                grcChuyenKhoa.DataSource = null;
                                                grcChuyenKhoa.DataSource = query2;
                                                grvChuyenKhoa_FocusedRowChanged(null, null);
                                                if (radGiaiQuyet.SelectedIndex == 1)
                                                {
                                                    var ktvv = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                                    if (ktvv.Count <= 0)
                                                    {

                                                        mnKBVaoVien_ItemClick(sender, e);
                                                    }
                                                }
                                                else
                                                {
                                                    if (radGiaiQuyet.SelectedIndex == 2)
                                                    {
                                                        var ktrv = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                                                        if (ktrv.Count <= 0)
                                                        {
                                                            mnChuyenVien_ItemClick(sender, e);
                                                        }

                                                    }

                                                }
                                            }
                                            TTLuuKB = false;

                                        }
                                    } // kết thúc
                                }
                                // thông báo bn chuyển phòng khám
                                labThongBaoBNCP.Text = ThongBaoBNChuyenPK();
                                // kiểm tra nếu có thông tin vào viện thì xóa
                                if (radGiaiQuyet.SelectedIndex != 1)
                                {
                                    var vv = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();

                                    if (vv != null)
                                    {
                                        var kp = _dataContext.KPhongs.Where(p => p.MaKP == vv.MaKP).Select(p => p.PLoai).FirstOrDefault();
                                        if (kp != "Phòng khám")
                                        {
                                            _dataContext.VaoViens.Remove(vv);
                                            _dataContext.SaveChanges();
                                        }

                                    }
                                }
                                //var idkb = _dataContext.BNKBs.Where(p => p.MaBNhan==_int_maBN).ToList();
                                //if (idkb.Count > 0)
                                //    txtIdkb.Text = idkb.First().IDKB.ToString();
                                //}
                                //catch (Exception ex)
                                //{
                                //    MessageBox.Show("Không lưu được khám bệnh" + ex.Message);
                                //}
                                //break;
                                //lưu kê đơn
                                //case 2:
                                #endregion
                                #region lưu đơn

                                try
                                {
                                    bool _dy = false;

                                    GridColumn column = grvDonThuocct.FocusedColumn;
                                    if (column == grvDonThuocct.Columns["TrongBH"])//["coltrongBH"])
                                    {
                                        grvDonThuocct.FocusedColumn = grvDonThuocct.Columns[0];
                                        grvDonThuocct.FocusedColumn = column;
                                    }
                                    if (DungChung.Ham.CheckNgay(Convert.ToDateTime(grvBNhankb.GetFocusedRowCellValue(colNNhapkb)), dtNgayKhamkb.DateTime))
                                    {
                                        bool luudon = false;
                                        if (grvDonThuocct.RowCount > 0) // kiểm tra có thuốc mới lưu kê đơn
                                        {
                                            if (grvDonThuocct.RowCount == 1)// kiểm tra row đầu tiền có dữ liệu không
                                            {
                                                if (grvDonThuocct.GetRowCellValue(1, colIDDonct) != null && grvDonThuocct.GetRowCellValue(1, colIDDonct).ToString() != "" && Convert.ToInt32(grvDonThuocct.GetRowCellValue(1, colIDDonct)) > 0)
                                                {
                                                    luudon = true;
                                                }
                                                else
                                                {
                                                    luudon = false;
                                                }
                                            }
                                            else
                                            {
                                                luudon = true;
                                            }
                                        }
                                        string DSThuocThieuTT = "";
                                        int _Dem = 0;
                                        bool checkton = true;
                                        for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                                        {
                                            var ngayKe = grvDonThuocct.GetRowCellValue(i, colNgayKe);
                                            var row = (DThuocctModel)grvDonThuocct.GetRow(i);
                                            if (ngayKe == null)
                                            {
                                                MessageBox.Show("Ngày kê không được để trống!");
                                                return;
                                            }
                                            if (ngayKe != null && Convert.ToDateTime(ngayKe) <= dtNgayKhamkb.DateTime && row != null && (row.IDKB == idbnkb || row.IDKB == 0))
                                            {
                                                MessageBox.Show("Ngày kê không được nhỏ hơn ngày khám!");
                                                return;
                                            }
                                            if (ngayKe != null && row != null && row.IDKB > 0 && row.IDKB != idbnkb)
                                            {
                                                var bnkbCheck = _dataContext.BNKBs.FirstOrDefault(o => o.IDKB == row.IDKB);
                                                if (bnkbCheck != null && Convert.ToDateTime(ngayKe) <= bnkbCheck.NgayKham)
                                                {
                                                    MessageBox.Show("Ngày kê không được nhỏ hơn ngày khám!");
                                                    return;
                                                }
                                            }
                                            if (grvDonThuocct.GetRowCellValue(i, colIDThuoc) != null && grvDonThuocct.GetRowCellValue(i, colIDThuoc).ToString() != "")
                                            {

                                                int mdv = _medicinesProvider.GetMaDVbyIDThuoc(grvDonThuocct.GetRowCellValue(i, colIDThuoc) == null ? 0 : Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDThuoc)));

                                                if ((grvDonThuocct.GetRowCellValue(i, colSoLan) == null || grvDonThuocct.GetRowCellValue(i, colSoLan).ToString() == "" || grvDonThuocct.GetRowCellValue(i, colSoLuongdung) == null || grvDonThuocct.GetRowCellValue(i, colSoLuongdung).ToString() == "") && DungChung.Bien.MaBV != "14017")
                                                {
                                                    DSThuocThieuTT += grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc).ToString() + ";\n";
                                                    _Dem++;
                                                }
                                            }
                                        }
                                        if (_Dem > 0)
                                        {
                                            MessageBox.Show("Các thuốc:\n " + DSThuocThieuTT.ToString() + "Chưa được nhập đầy đủ Liều lượng");
                                            break;
                                        }
                                        if (_dy && checkShowThuocThang)
                                        {
                                            checkShowThuocThang = false;
                                            ThuocThang thuocThang = new ThuocThang();
                                            if (DungChung.Bien.MaBV != "24012" && DungChung.Bien.MaBV != "24389")
                                            {
                                                if (!string.IsNullOrEmpty(mm_ghiChu.Text) && mm_ghiChu.Text.Contains(";"))
                                                {
                                                    string[] arghichu = mm_ghiChu.Text.Split(';');
                                                    thuocThang.SoThang = sothangSua;
                                                    if (arghichu.Count() > 1)
                                                        thuocThang.TuNgay = Convert.ToDateTime(arghichu[1]);
                                                    if (arghichu.Count() > 2)
                                                        thuocThang.DenNgay = Convert.ToDateTime(arghichu[2]);
                                                    if (arghichu.Count() > 3)
                                                        thuocThang.GhiChu = arghichu[3];
                                                    if (arghichu.Count() > 4)
                                                        thuocThang.CachSac = arghichu[4];
                                                    if (arghichu.Count() > 5)
                                                        thuocThang.CachUong = arghichu[5];
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(cbo_loiDan.Text) && cbo_loiDan.Text.Contains(";"))
                                                {
                                                    string[] arghichu = cbo_loiDan.Text.Split(';');
                                                    thuocThang.SoThang = sothangSua;
                                                    if (arghichu.Count() > 1)
                                                        thuocThang.TuNgay = Convert.ToDateTime(arghichu[1]);
                                                    if (arghichu.Count() > 2)
                                                        thuocThang.DenNgay = Convert.ToDateTime(arghichu[2]);
                                                    if (arghichu.Count() > 3)
                                                        thuocThang.GhiChu = arghichu[3];
                                                    if (arghichu.Count() > 4)
                                                        thuocThang.CachSac = arghichu[4];
                                                    if (arghichu.Count() > 5)
                                                        thuocThang.CachUong = arghichu[5];
                                                }
                                            }
                                            FormThamSo.frm_checkThuocThang frm = new FormThamSo.frm_checkThuocThang(thuocThang);
                                            frm.getdata = new FormThamSo.frm_checkThuocThang.getString(GetValueSoThang);
                                            frm.ShowDialog();
                                            if (sothangOld != sothangSua)// thay đổi số thang => sửa số lượng
                                            {
                                                int r = grvDonThuocct.FocusedRowHandle;
                                                for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                                                {
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuongct) != null && grvDonThuocct.GetRowCellValue(i, colSoLuongct).ToString() != "" && grvDonThuocct.GetRowCellValue(i, colSoLuongct).ToString() != string.Empty)
                                                    {
                                                        double soluong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuongct));
                                                        ktCellChange = true;
                                                        grvDonThuocct.SetRowCellValue(i, colSoLuongct, 0);
                                                        grvDonThuocct.SetRowCellValue(i, colSoLuongct, soluong);
                                                    }
                                                }
                                            }
                                        }

                                        #region lưu đơn
                                        if (luudon || deleteDThuoccts.Count() > 0)
                                        {
                                            string msg = "Các thuốc có số lượng tồn không đủ: ";
                                            foreach (var item in lupIDThuoc.DataSource as List<MedicineInventoryModel>)
                                            {
                                                if (item.TonKhaDung != item.TonHienTai)
                                                {
                                                    if (!_medicinesProvider.IsDuThuoc(maKhoXuat, (int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, item.TonKhaDung - item.TonHienTai))
                                                    {
                                                        msg += item.TenDV + " ; ";
                                                        checkton = false;
                                                    }
                                                }
                                            }
                                            if (!checkton)
                                            {
                                                msg += "\nVui lòng chọn thuốc khác.";
                                                MessageBox.Show(msg, "Thuốc không đủ tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                return;
                                            }

                                            int _idkb = 0;
                                            if (!string.IsNullOrEmpty(txtIdkb.Text))
                                                _idkb = Convert.ToInt32(txtIdkb.Text);
                                            int makho = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                            var ktratutruc = _lKPhong_data.Where(p => p.MaKP == makho && p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();
                                            #region Dthuoc

                                            var source = (List<DThuocctModel>)binSDonThuocct.DataSource;
                                            DThuoc dthuocNew = new DThuoc();
                                            dthuocNew.MaBNhan = _int_maBN;
                                            dthuocNew.NgayKe = (source == null || source.Count <= 0) ? DateTime.Now : source.Min(o => o.NgayNhap);
                                            dthuocNew.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                            dthuocNew.MaCB = lupNguoiKhamkb.EditValue.ToString();

                                            dthuocNew.MaKXuat = makho;

                                            if (ktratutruc.Count > 0)
                                            {
                                                dthuocNew.KieuDon = 8;
                                            }
                                            else
                                            {
                                                dthuocNew.KieuDon = -1;
                                            }
                                            dthuocNew.Status = 0;
                                            dthuocNew.PLDV = 1;

                                            //if (!string.IsNullOrEmpty(mm_ghiChu.Text))
                                            if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                                            {
                                                dthuocNew.GhiChu = cbo_loiDan.Text;
                                            }
                                            else
                                            {
                                                dthuocNew.GhiChu = mm_ghiChu.Text;
                                            }
                                            #endregion
                                            var ktdthuoc = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1 && (p.KieuDon == -1 || p.KieuDon == 8)).ToList();
                                            if (ktdthuoc.Count > 0)
                                                TTLuu = 2;
                                            else
                                                TTLuu = 1;

                                            if (TTLuu == 1)//tạo đơn mới
                                            {
                                                #region tạo mới
                                                //tạo trên DonThuoc
                                                dthuocNew.IDDon_Mau = iddthuocmau1 > 0 ? (int?)iddthuocmau1 : null;
                                                _dataContext.DThuocs.Add(dthuocNew);

                                                //Tạo trên đơn thuốc chi tiết (donthuocct)
                                                if (_dataContext.SaveChanges() >= 0)
                                                {
                                                    int maxid = dthuocNew.IDDon;
                                                    string message = "";

                                                    for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                                                    {
                                                        if ((grvDonThuocct.GetRowCellValue(i, colIDThuoc) != null && grvDonThuocct.GetRowCellValue(i, colIDThuoc).ToString() != "") &&
                                                            (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "") &&
                                                            (grvDonThuocct.GetRowCellValue(i, colDonGia) != null && grvDonThuocct.GetRowCellValue(i, colDonGia).ToString() != "" && Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia)) >= 0) &&
                                                            ppxuat == 1 ? true : (grvDonThuocct.GetRowCellValue(i, colSoLo) != null && grvDonThuocct.GetRowCellValue(i, colSoLo).ToString() != "")
                                                            )
                                                        {
                                                            // kiểm tra tồn trước khi lưu
                                                            double _soluong = 0;
                                                            double _soluongct = 0;
                                                            int _statusct = 0;

                                                            _soluong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString());
                                                            _soluongct = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuongct).ToString());

                                                            //=============================================================================================================================================
                                                            if (grvDonThuocct.GetRowCellValue(i, colIDThuoc) != null && grvDonThuocct.GetRowCellValue(i, colIDThuoc).ToString() != "")
                                                            {
                                                                idThuoc = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDThuoc));
                                                                maDV = _medicinesProvider.GetMaDVbyIDThuoc(idThuoc);
                                                            }

                                                            if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                                soLo = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colSoLo));
                                                            if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null && grvDonThuocct.GetRowCellValue(i, colHanDung).ToString() != "")
                                                                hanDung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));
                                                            if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null && grvDonThuocct.GetRowCellValue(i, colDonGia).ToString() != "")
                                                                donGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia));
                                                            if (lupKhoaKhamkb.EditValue != null && lupKhoaKhamkb.EditValue.ToString() != "")
                                                                maKhoaKe = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                            if (lupKhoXuat.EditValue != null && lupKhoXuat.EditValue.ToString() != "")
                                                                maKhoXuat = Convert.ToInt32(lupKhoXuat.EditValue);


                                                            if (_medicinesProvider.isTuTruc(maKhoXuat))
                                                                TH = 2;
                                                            else
                                                                TH = 0;
                                                            //============================================================================================================================================

                                                            if (_soluongct > 0)
                                                            {
                                                                if (_soluong > 0)
                                                                {

                                                                    if (grvDonThuocct.GetRowCellValue(i, colStatusct) != null && grvDonThuocct.GetRowCellValue(i, colStatusct).ToString() != "")
                                                                        _statusct = Convert.ToInt16(grvDonThuocct.GetRowCellValue(i, colStatusct));

                                                                    if ((grvDonThuocct.GetRowCellValue(i, colSoLan) == null || grvDonThuocct.GetRowCellValue(i, colSoLan).ToString() == ""))
                                                                    {
                                                                        MessageBox.Show("thuốc: " + grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + " chưa nhập số lần theo cv4210");
                                                                        return;
                                                                    }
                                                                    else if ((grvDonThuocct.GetRowCellValue(i, colSoLuongdung) == null || grvDonThuocct.GetRowCellValue(i, colSoLuongdung).ToString() == ""))
                                                                    {
                                                                        MessageBox.Show("thuốc: " + grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + " chưa nhập số lượng dùng theo cv4210");
                                                                        return;
                                                                    }

                                                                    //kết thúc
                                                                    DThuocct dthuocct = new DThuocct();
                                                                    dthuocct.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                                                    dthuocct.IDKB = _idkb;
                                                                    dthuocct.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                                    dthuocct.IDDon = maxid;
                                                                    dthuocct.MaKXuat = dthuocNew.MaKXuat;
                                                                    dthuocct.NgayNhap = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colNgayKe));
                                                                    dthuocct.MaDV = maDV;
                                                                    dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                                    dthuocct.DonGia = donGia;
                                                                    dthuocct.SoLuong = _soluong;
                                                                    dthuocct.SoLuongct = _soluongct;
                                                                    dthuocct.Loai = (byte)sothangSua;
                                                                    dthuocct.ThanhTien = Math.Round(Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien)), DungChung.Bien.LamTronSo);


                                                                    if (grvDonThuocct.GetRowCellValue(i, colDuongD) != null)
                                                                        dthuocct.DuongD = grvDonThuocct.GetRowCellValue(i, colDuongD).ToString();
                                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLan) != null)
                                                                        dthuocct.SoLan = grvDonThuocct.GetRowCellValue(i, colSoLan).ToString();
                                                                    else dthuocct.SoLan = "";
                                                                    if (grvDonThuocct.GetRowCellValue(i, colMoilan) != null)
                                                                        dthuocct.MoiLan = grvDonThuocct.GetRowCellValue(i, colMoilan).ToString();
                                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuongdung) != null)
                                                                        dthuocct.Luong = grvDonThuocct.GetRowCellValue(i, colSoLuongdung).ToString();
                                                                    else dthuocct.Luong = "";
                                                                    if (grvDonThuocct.GetRowCellValue(i, colDViUong) != null)
                                                                        dthuocct.DviUong = grvDonThuocct.GetRowCellValue(i, colDViUong).ToString();
                                                                    if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                                                        dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                                                    else
                                                                        dthuocct.MaCC = "";
                                                                    if (grvDonThuocct.GetRowCellValue(i, colGhiChu) != null)
                                                                        dthuocct.GhiChu = grvDonThuocct.GetRowCellValue(i, colGhiChu).ToString();
                                                                    else
                                                                        dthuocct.GhiChu = "";
                                                                    if (grvDonThuocct.GetRowCellValue(i, coltrongBH) != null && grvDonThuocct.GetRowCellValue(i, coltrongBH).ToString() != "")
                                                                    {
                                                                        dthuocct.TrongBH = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, coltrongBH));
                                                                    }
                                                                    else
                                                                    {
                                                                        dthuocct.TrongBH = 0;
                                                                    }
                                                                    dthuocct.Status = dthuocNew.Status;
                                                                    if (grvDonThuocct.GetRowCellValue(i, colTyLeBHTT_dt) != null && grvDonThuocct.GetRowCellValue(i, colTyLeBHTT_dt).ToString() != "")
                                                                        dthuocct.TyLeTT = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colTyLeBHTT_dt));
                                                                    else
                                                                        dthuocct.TyLeTT = 0;
                                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null && grvDonThuocct.GetRowCellValue(i, colSoLo).ToString() != "")
                                                                        dthuocct.SoLo = soLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();

                                                                    if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null && grvDonThuocct.GetRowCellValue(i, colHanDung).ToString() != "")
                                                                        dthuocct.HanDung = hanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                                                                    if (grvDonThuocct.GetRowCellValue(i, colMienCT) != null)
                                                                        dthuocct.MienCT = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMienCT));

                                                                    _dataContext.DThuoccts.Add(dthuocct);
                                                                    _dataContext.SaveChanges();



                                                                }
                                                            }
                                                        }
                                                    }

                                                    //Update table MedicineList

                                                    if (_medicinesProvider.isTuTruc(maKhoXuat))
                                                    {
                                                        foreach (var item in lupIDThuoc.DataSource as List<MedicineInventoryModel>)
                                                        {
                                                            if (item.TonKhaDung != item.TonHienTai)
                                                            {
                                                                _medicinesProvider.UpdateMedicineListPPX3((int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, 0, maKhoXuat, item.TonKhaDung - item.TonHienTai, 2);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _medicinesProvider.UpdateMedicineListPPX3(0, 0, "", new DateTime(), 0, maKhoXuat, 0, 0);
                                                    }

                                                    int idDon = _medicinesProvider.GetIDDonByMaBNInFrmNgoaiTru(_int_maBN);
                                                    lupIDThuoc.DataSource = MedicineByRooms = _medicinesProvider.GetLupMaDuoc(MaKPxd, idDon, 0);

                                                    if (!string.IsNullOrWhiteSpace(message))
                                                        MessageBox.Show(message);
                                                    xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                #region sửa đơn
                                                if (TTLuu == 2) //Sửa đơn
                                                {
                                                    bool isChangeKXuat = false;
                                                    int maKXuatOld = 0;

                                                    var ktdt = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 1 && (p.KieuDon == -1 || p.KieuDon == 8)).ToList();

                                                    int id = ktdt.First().IDDon;
                                                    DThuoc dthuoc = _dataContext.DThuocs.Single(p => p.IDDon == id);
                                                    dthuoc.MaBNhan = _int_maBN;
                                                    dthuoc.NgayKe = (source == null || source.Count <= 0) ? dthuoc.NgayKe : source.Min(o => o.NgayNhap);

                                                    if (lupKhoXuat.EditValue != null)
                                                        maKhoXuat = Convert.ToInt32(lupKhoXuat.EditValue);
                                                    var ktratutruc2 = _dataContext.KPhongs.Where(p => p.MaKP == dthuoc.MaKXuat && p.PLoai.Contains("Tủ trực")).ToList();
                                                    if (dthuoc.MaKXuat != maKhoXuat)
                                                    {
                                                        isChangeKXuat = true;
                                                        maKXuatOld = dthuoc.MaKXuat ?? 0;
                                                    }

                                                    if (chkKDNgoai.Checked)
                                                        dthuoc.KieuDon = -2;
                                                    else if (ktratutruc2.Count > 0)
                                                        dthuoc.KieuDon = 8;
                                                    else
                                                        dthuoc.KieuDon = -1;
                                                    dthuoc.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                    dthuoc.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                                    dthuoc.MaKXuat = maKhoXuat;
                                                    dthuoc.IDDon_Mau = (iddthuocmau1 > 0 && iddthuocmau1 != dthuoc.IDDon_Mau) ? iddthuocmau1 : dthuoc.IDDon_Mau;
                                                    dthuoc.GhiChu = cbo_loiDan.Text;
                                                    if (_dataContext.SaveChanges() >= 0)
                                                    {
                                                        // lưu chi tiết đơn
                                                        int _ttluukd = 0;
                                                        string message = "";

                                                        for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                                                        {
                                                            //=============================================================================================================================================
                                                            if (grvDonThuocct.GetRowCellValue(i, colIDThuoc) != null && grvDonThuocct.GetRowCellValue(i, colIDThuoc).ToString() != "")
                                                            {
                                                                idThuoc = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDThuoc));
                                                                maDV = _medicinesProvider.GetMaDVbyIDThuoc(idThuoc);
                                                            }

                                                            if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                                soLo = Convert.ToString(grvDonThuocct.GetRowCellValue(i, colSoLo));
                                                            if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null && grvDonThuocct.GetRowCellValue(i, colHanDung).ToString() != "")
                                                                hanDung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));
                                                            if (grvDonThuocct.GetRowCellValue(i, colDonGia) != null && grvDonThuocct.GetRowCellValue(i, colDonGia).ToString() != "")
                                                                donGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia));
                                                            if (lupKhoaKhamkb.EditValue != null && lupKhoaKhamkb.EditValue.ToString() != "")
                                                                maKhoaKe = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                            if (lupKhoXuat.EditValue != null && lupKhoXuat.EditValue.ToString() != "")
                                                                maKhoXuat = Convert.ToInt32(lupKhoXuat.EditValue);

                                                            //============================================================================================================================================


                                                            if (DungChung.Bien.MaBV == "24012" && grvDonThuocct.GetRowCellValue(i, colStatusct) != null && grvDonThuocct.GetRowCellValue(i, colStatusct).ToString() == "1")
                                                            {
                                                                //MessageBox.Show("Không thể sửa đơn đã lĩnh");
                                                                //break;
                                                            }
                                                            else if (maDV != 0 &&
                                                                    (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "") &&
                                                                    (grvDonThuocct.GetRowCellValue(i, colDonGia) != null && grvDonThuocct.GetRowCellValue(i, colDonGia).ToString() != "" && Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia)) >= 0) &&
                                                                    (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString() != ""))
                                                            {
                                                                int idct = int.Parse(grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString());
                                                                int ttluusaodon = 0;
                                                                if (grvDonThuocct.GetRowCellValue(i, colTTLuudt) != null && grvDonThuocct.GetRowCellValue(i, colTTLuudt).ToString() != "")
                                                                    ttluusaodon = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colTTLuudt));
                                                                if (idct > 0 && ttluusaodon > 0)// sửa row
                                                                {
                                                                    int _makho = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);

                                                                    double _soluong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString());
                                                                    double _soluongct = 0;
                                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuongct) != null && grvDonThuocct.GetRowCellValue(i, colSoLuongct).ToString() != "")
                                                                        _soluongct = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuongct).ToString());

                                                                    if ((grvDonThuocct.GetRowCellValue(i, colSoLan) == null || grvDonThuocct.GetRowCellValue(i, colSoLan).ToString() == "") && lupKhoXuat.Text != "Kho Thuốc đông y" && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297"))
                                                                    {
                                                                        MessageBox.Show("thuốc: " + grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + " chưa nhập số lần theo cv4210");
                                                                        return;
                                                                    }

                                                                    if ((grvDonThuocct.GetRowCellValue(i, colSoLan) == null || grvDonThuocct.GetRowCellValue(i, colSoLan).ToString() == "") && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
                                                                    {
                                                                        MessageBox.Show("thuốc: " + grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + " chưa nhập số lần theo cv4210");
                                                                        return;
                                                                    }

                                                                    else if ((grvDonThuocct.GetRowCellValue(i, colSoLuongdung) == null || grvDonThuocct.GetRowCellValue(i, colSoLuongdung).ToString() == "") && lupKhoXuat.Text != "Kho Thuốc đông y" && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297"))
                                                                    {
                                                                        MessageBox.Show("thuốc: " + grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + " chưa nhập số lượng dùng theo cv4210");
                                                                        return;
                                                                    }

                                                                    else if ((grvDonThuocct.GetRowCellValue(i, colSoLuongdung) == null || grvDonThuocct.GetRowCellValue(i, colSoLuongdung).ToString() == "") && DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
                                                                    {
                                                                        MessageBox.Show("thuốc: " + grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + " chưa nhập số lượng dùng theo cv4210");
                                                                        return;
                                                                    }

                                                                    if (_soluong > 0 && _soluongct > 0)
                                                                    {

                                                                        DThuocct dthuocct = _dataContext.DThuoccts.Single(p => p.IDDonct == idct);
                                                                        dthuocct.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                                                        dthuocct.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                                        dthuocct.MaKXuat = dthuoc.MaKXuat;
                                                                        dthuocct.IDDon = id;
                                                                        dthuocct.NgayNhap = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colNgayKe));
                                                                        dthuocct.MaDV = maDV;
                                                                        dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                                        dthuocct.DonGia = donGia;
                                                                        dthuocct.SoLuong = _soluong;
                                                                        dthuocct.SoLuongct = _soluongct;
                                                                        dthuocct.Loai = (byte)sothangSua;
                                                                        dthuocct.ThanhTien = Math.Round(Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien)), DungChung.Bien.LamTronSo);

                                                                        if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                                                            dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colDuongD) != null)
                                                                            dthuocct.DuongD = grvDonThuocct.GetRowCellValue(i, colDuongD).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLan) != null)
                                                                            dthuocct.SoLan = grvDonThuocct.GetRowCellValue(i, colSoLan).ToString();
                                                                        else dthuocct.SoLan = "";
                                                                        if (grvDonThuocct.GetRowCellValue(i, colMoilan) != null)
                                                                            dthuocct.MoiLan = grvDonThuocct.GetRowCellValue(i, colMoilan).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuongdung) != null)
                                                                            dthuocct.Luong = grvDonThuocct.GetRowCellValue(i, colSoLuongdung).ToString();
                                                                        else dthuocct.Luong = "";
                                                                        if (grvDonThuocct.GetRowCellValue(i, colDViUong) != null)
                                                                            dthuocct.DviUong = grvDonThuocct.GetRowCellValue(i, colDViUong).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colGhiChu) != null)
                                                                            dthuocct.GhiChu = grvDonThuocct.GetRowCellValue(i, colGhiChu).ToString();
                                                                        else
                                                                            dthuocct.GhiChu = "";
                                                                        if (grvDonThuocct.GetRowCellValue(i, coltrongBH) != null && grvDonThuocct.GetRowCellValue(i, coltrongBH).ToString() != "")
                                                                        {
                                                                            dthuocct.TrongBH = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, coltrongBH));
                                                                        }
                                                                        else
                                                                        {
                                                                            dthuocct.TrongBH = 0;
                                                                        }
                                                                        if (grvDonThuocct.GetRowCellValue(i, colTyLeBHTT_dt) != null && grvDonThuocct.GetRowCellValue(i, colTyLeBHTT_dt).ToString() != "")
                                                                            dthuocct.TyLeTT = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colTyLeBHTT_dt));
                                                                        else
                                                                            dthuocct.TyLeTT = 0;
                                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                                            dthuocct.SoLo = soLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();

                                                                        if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null && grvDonThuocct.GetRowCellValue(i, colHanDung).ToString() != "")
                                                                            dthuocct.HanDung = hanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                                                                        if (grvDonThuocct.GetRowCellValue(i, colMienCT) != null)
                                                                            dthuocct.MienCT = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMienCT));
                                                                        _dataContext.SaveChanges();


                                                                        _ttluukd = 1;
                                                                    }
                                                                }
                                                                else
                                                                {// lưu row mới 
                                                                    int _makho = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                                                    double _dongia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                                                    double _soluong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString());
                                                                    double _soluongct = 0;

                                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuongct) != null && grvDonThuocct.GetRowCellValue(i, colSoLuongct).ToString() != "")
                                                                        _soluongct = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuongct).ToString());
                                                                    if ((grvDonThuocct.GetRowCellValue(i, colSoLan) == null || grvDonThuocct.GetRowCellValue(i, colSoLan).ToString() == "") && lupKhoXuat.Text != "Kho Thuốc đông y" && (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297"))
                                                                    {
                                                                        MessageBox.Show("thuốc: " + grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + " chưa nhập số lần theo cv4210");
                                                                        return;
                                                                    }

                                                                    if ((grvDonThuocct.GetRowCellValue(i, colSoLan) == null || grvDonThuocct.GetRowCellValue(i, colSoLan).ToString() == ""))
                                                                    {
                                                                        MessageBox.Show("thuốc: " + grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + " chưa nhập số lần theo cv4210");
                                                                        return;
                                                                    }
                                                                    else if ((grvDonThuocct.GetRowCellValue(i, colSoLuongdung) == null || grvDonThuocct.GetRowCellValue(i, colSoLuongdung).ToString() == ""))
                                                                    {
                                                                        MessageBox.Show("thuốc: " + grvDonThuocct.GetRowCellDisplayText(i, colIDThuoc) + " chưa nhập số lượng dùng theo cv4210");
                                                                        return;
                                                                    }

                                                                    if (_soluong > 0 && _soluongct > 0)
                                                                    {
                                                                        int _statusct = 0;

                                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null && grvDonThuocct.GetRowCellValue(i, colSoLo).ToString() != "")
                                                                            soLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colStatusct) != null && grvDonThuocct.GetRowCellValue(i, colStatusct).ToString() != "")
                                                                            _statusct = Convert.ToInt16(grvDonThuocct.GetRowCellValue(i, colStatusct));
                                                                        DThuocct dthuocct = new DThuocct();
                                                                        dthuocct.IDKB = _idkb;
                                                                        dthuocct.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                                                        dthuocct.MaKP = maKhoaKe;
                                                                        dthuocct.MaKXuat = maKhoXuat;
                                                                        dthuocct.IDDon = id;
                                                                        dthuocct.NgayNhap = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colNgayKe));
                                                                        dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                                        dthuocct.MaDV = maDV;
                                                                        dthuocct.DonGia = donGia;
                                                                        dthuocct.SoLuong = _soluong;
                                                                        dthuocct.SoLuongct = _soluongct;
                                                                        dthuocct.Loai = (byte)sothangSua;
                                                                        dthuocct.ThanhTien = Math.Round(Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien)), DungChung.Bien.LamTronSo);
                                                                        if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                                                            dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colDuongD) != null)
                                                                            dthuocct.DuongD = grvDonThuocct.GetRowCellValue(i, colDuongD).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLan) != null)
                                                                            dthuocct.SoLan = grvDonThuocct.GetRowCellValue(i, colSoLan).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colMoilan) != null)
                                                                            dthuocct.MoiLan = grvDonThuocct.GetRowCellValue(i, colMoilan).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuongdung) != null)
                                                                            dthuocct.Luong = grvDonThuocct.GetRowCellValue(i, colSoLuongdung).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, colDViUong) != null)
                                                                            dthuocct.DviUong = grvDonThuocct.GetRowCellValue(i, colDViUong).ToString();
                                                                        if (grvDonThuocct.GetRowCellValue(i, coltrongBH) != null && grvDonThuocct.GetRowCellValue(i, coltrongBH).ToString() != "")
                                                                        {
                                                                            dthuocct.TrongBH = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, coltrongBH));
                                                                        }
                                                                        else
                                                                        {
                                                                            dthuocct.TrongBH = 0;
                                                                        }
                                                                        if (grvDonThuocct.GetRowCellValue(i, colStatusct) != null && grvDonThuocct.GetRowCellValue(i, colStatusct).ToString() != "")
                                                                        {
                                                                            dthuocct.Status = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colStatusct));
                                                                        }
                                                                        else
                                                                        {
                                                                            dthuocct.Status = 0;
                                                                        }
                                                                        if (grvDonThuocct.GetRowCellValue(i, colGhiChu) != null)
                                                                            dthuocct.GhiChu = grvDonThuocct.GetRowCellValue(i, colGhiChu).ToString();
                                                                        else
                                                                            dthuocct.GhiChu = "";
                                                                        if (grvDonThuocct.GetRowCellValue(i, colTyLeBHTT_dt) != null && grvDonThuocct.GetRowCellValue(i, colTyLeBHTT_dt).ToString() != "")
                                                                            dthuocct.TyLeTT = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colTyLeBHTT_dt));
                                                                        else
                                                                            dthuocct.TyLeTT = 0;
                                                                        if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                                            dthuocct.SoLo = soLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();

                                                                        if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null && grvDonThuocct.GetRowCellValue(i, colHanDung).ToString() != "")
                                                                            dthuocct.HanDung = hanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                                                                        if (grvDonThuocct.GetRowCellValue(i, colMienCT) != null)
                                                                            dthuocct.MienCT = Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colMienCT));
                                                                        _dataContext.DThuoccts.Add(dthuocct);
                                                                        _dataContext.SaveChanges();

                                                                        TTLuu = 0;
                                                                        _ttluukd = 1;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        // cập nhật tồn cho những thuốc bị mất khỏi lupidthuoc do đổi kho xuất

                                                        //Xóa đơn
                                                        if (deleteDThuoccts.Count() > 0)
                                                        {
                                                            foreach (var item in deleteDThuoccts)
                                                            {
                                                                var dtctxoa = _dataContext.DThuoccts.FirstOrDefault(p => p.IDDonct == item);

                                                                if (dtctxoa != null && isChangeKXuat && dtctxoa.MaKXuat != maKhoXuat && _medicinesProvider.isTuTruc(maKhoXuat))
                                                                {
                                                                    _medicinesProvider.UpdateMedicineListPPX3((int)dtctxoa.MaDV, dtctxoa.DonGia, dtctxoa.SoLo, (DateTime)dtctxoa.HanDung, 0, maKXuatOld, -dtctxoa.SoLuong, 2);
                                                                }

                                                                _medicinesProvider.DeleteDThuocAndDThuocctbyIDDonct(item);
                                                            }
                                                            deleteDThuoccts.Clear();
                                                        }


                                                        //Cập nhật tồn theo lupidthuoc

                                                        if (_medicinesProvider.isTuTruc(maKhoXuat))
                                                        {
                                                            foreach (var item in lupIDThuoc.DataSource as List<MedicineInventoryModel>)
                                                            {
                                                                if (item.TonKhaDung != item.TonHienTai)
                                                                {
                                                                    _medicinesProvider.UpdateMedicineListPPX3((int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, 0, maKhoXuat, item.TonKhaDung - item.TonHienTai, 2);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            _medicinesProvider.UpdateMedicineListPPX3(0, 0, "", new DateTime(), 0, maKhoXuat, 0, 0);
                                                        }



                                                        int idDon = _medicinesProvider.GetIDDonByMaBNInFrmNgoaiTru(_int_maBN);
                                                        lupIDThuoc.DataSource = MedicineByRooms = _medicinesProvider.GetLupMaDuoc(MaKPxd, idDon, 0);

                                                        if (!string.IsNullOrWhiteSpace(message))
                                                            MessageBox.Show(message);
                                                        xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                                                        TTLuu = 0;
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }

                                    mnLuu.Enabled = false;
                                    var selectedRow = grvBNhankb.GetSelectedRows();
                                    if(selectedRow.Count() == 1)
                                    {
                                        grvBNhankb_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, selectedRow[0]));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi! Không lưu được đơn thuốc:" + ex.Message);
                                }
                                #endregion


                            }
                            else
                            {
                                MessageBox.Show("Bệnh nhân đã ra viện. Bạn không thể sửa!");
                                xtraKhamBenh.Text = "K.Bệnh-K.Đơn";

                                grvBNhankb_FocusedRowChanged(null, null);
                            }
                        }
                        else
                        {
                            xtraKhamBenh.Text = "K.Bệnh-K.Đơn";
                        }
                        break;
                    #endregion
                    case 3: // lưu chỉ định
                        bool checkValidate = true;
                        if (!KTraKB())
                            checkValidate = false;
                        else if (DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                        {
                            checkValidate = false;
                            MessageBox.Show("Bệnh nhân đã TT");
                        }
                        else if (DungChung.Ham.KT_RaVien_ngt(_dataContext, _int_maBN))
                        {
                            checkValidate = false;
                            MessageBox.Show("Bệnh nhân đã ra viện");
                        }
                        if (checkValidate)
                        {
                            bool ktraluu = true;
                            string _loingaynhap = "", loikp = "", loisoluong = "", loikokp = "";
                            int dem1 = 0, dem2 = 0, dem3 = 0, dem4 = 0;
                            for (int i = 0; i < grvchiDinh.DataRowCount; i++)
                            {
                                int _iddonct = -1;
                                if (grvchiDinh.GetRowCellValue(i, colIDctcd) != null && grvchiDinh.GetRowCellValue(i, colIDctcd).ToString() != "")
                                {
                                    _iddonct = Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colIDctcd));
                                }
                                if (grvchiDinh.GetRowCellValue(i, colNgayNhap) != null && grvchiDinh.GetRowCellValue(i, colNgayNhap).ToString() != "")
                                {
                                    //if (_iddonct <= 0)
                                    //{
                                    if (grvchiDinh.GetRowCellValue(i, colIDThuoc) != null && grvchiDinh.GetRowCellValue(i, colIDThuoc).ToString() != "")
                                    {
                                        DateTime _ngaynhap = Convert.ToDateTime(grvchiDinh.GetRowCellValue(i, colNgayNhap));
                                        if (grvchiDinh.GetRowCellValue(i, colKPhongdv) != null && grvchiDinh.GetRowCellValue(i, colKPhongdv).ToString() != "")
                                        {
                                            int _makp = Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colKPhongdv));
                                            bool ktra = true;
                                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                                            {
                                                var kttt = _lKPhong_data.Where(p => p.MaKP == _makp && p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh).ToList();
                                                if (kttt.Count > 0)
                                                    ktra = false;
                                            }
                                            if (ktra)
                                            {
                                                var ngaykham = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN && p.MaKP == _makp).Select(p => p.NgayKham).FirstOrDefault();
                                                if (ngaykham != null)
                                                {
                                                    if (_iddonct <= 0 && (ngaykham.Value > _ngaynhap))
                                                    {
                                                        dem1++;
                                                        _loingaynhap += grvchiDinh.GetRowCellDisplayText(i, colIDThuoc) + ";\n";
                                                    }
                                                }
                                                else
                                                {
                                                    dem2++;
                                                    loikp += grvchiDinh.GetRowCellDisplayText(i, colIDThuoc) + ";\n";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            dem4++;
                                            loikokp += grvchiDinh.GetRowCellDisplayText(i, colIDThuoc) + ";\n";
                                        }
                                        if (grvchiDinh.GetRowCellValue(i, colSoLuongcd) != null && grvchiDinh.GetRowCellValue(i, colSoLuongcd).ToString() != "")
                                        {
                                            Double _soluong = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colSoLuongcd));
                                            if (_soluong <= 0)
                                            {
                                                dem3++;
                                                loisoluong += grvchiDinh.GetRowCellDisplayText(i, colIDThuoc) + ";\n";
                                            }
                                        }
                                    }
                                    //}
                                }
                                else
                                {
                                    dem1++;
                                    _loingaynhap += grvchiDinh.GetRowCellDisplayText(i, colIDThuoc) + ";\n";
                                }
                            }
                            if (dem1 > 0)
                            {
                                MessageBox.Show("các dịch vụ: " + _loingaynhap.ToString() + "\n có ngày nhập không hợp lệ nhỏ hơn ngày khám hoặc lớn hơn ngày hiện tại");
                                ktraluu = false;
                            }
                            if (dem2 > 0)
                            {
                                MessageBox.Show("các dịch vụ: " + loikp.ToString() + "\n có khoa|phòng không hợp lệ");
                                ktraluu = false;
                            }
                            if (dem3 > 0)
                            {
                                MessageBox.Show("các dịch vụ: " + loikp.ToString() + "\n có số lượng không hợp lệ");
                                ktraluu = false;
                            }
                            if (dem4 > 0)
                            {
                                MessageBox.Show("các dịch vụ: " + loikokp.ToString() + "\n chưa có Khoa|Phòng");
                                ktraluu = false;
                            }
                            if (ktraluu)
                            {
                                try
                                {
                                    var ktdthuoc = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                    if (ktdthuoc.Count > 0)
                                        TTLuu = 2;
                                    else
                                        TTLuu = 1;
                                    if (TTLuu == 1)
                                    {
                                        DThuoc dthuoccd = new DThuoc();
                                        dthuoccd.NgayKe = dtNgayKhamkb.DateTime;
                                        //dthuoccd.MaKP = lupKhoaKhamkb.EditValue.ToString();
                                        dthuoccd.MaBNhan = _int_maBN;
                                        dthuoccd.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                        if (lupKhoaKhamkb.EditValue != null)
                                            dthuoccd.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                        else
                                            dthuoccd.MaKP = DungChung.Bien.MaKP;
                                        dthuoccd.PLDV = 2;
                                        dthuoccd.KieuDon = -1;
                                        _dataContext.DThuocs.Add(dthuoccd);
                                        if (_dataContext.SaveChanges() >= 0)
                                        {
                                            var maxid = dthuoccd.IDDon;
                                            for (int i = 0; i < grvchiDinh.DataRowCount; i++)
                                            {
                                                if (grvchiDinh.GetRowCellValue(i, colMaDVcd) != null && grvchiDinh.GetRowCellValue(i, colMaDVcd).ToString() != "")
                                                {
                                                    if (grvchiDinh.GetRowCellValue(i, colSoLuongcd) != null && grvchiDinh.GetRowCellValue(i, colSoLuongcd).ToString() != "")
                                                    {
                                                        double soluong = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colSoLuongcd).ToString().Trim());
                                                        //int trongbh=Convert.ToInt32(grvchiDinh.GetRowCellValue(i, coltr);
                                                        //if (grvchiDinh.GetRowCellValue(i, colBSTH) == null)
                                                        //{
                                                        //    MessageBox.Show("bạn chưa nhập bác sĩ thực hiện theo CV4210");
                                                        //    return;
                                                        //}
                                                        //else
                                                        if (soluong > 0)
                                                        {
                                                            // lưu row mới 
                                                            DThuocct dthuocct = new DThuocct();
                                                            if (lupKhoaKhamkb.EditValue != null)
                                                                dthuocct.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                            else
                                                                dthuocct.MaKP = DungChung.Bien.MaKP;
                                                            if (grvchiDinh.GetRowCellValue(i, colXHH) != null && grvchiDinh.GetRowCellValue(i, colXHH).ToString() != "")
                                                            {
                                                                bool xhh = Convert.ToBoolean(grvchiDinh.GetRowCellValue(i, colXHH));
                                                                dthuocct.XHH = xhh == true ? 1 : 0;
                                                            }
                                                            dthuocct.IDDon = maxid;
                                                            dthuocct.MaDV = grvchiDinh.GetRowCellValue(i, colMaDVcd) == null ? 0 : Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colMaDVcd));
                                                            dthuocct.DonVi = grvchiDinh.GetRowCellValue(i, colDonVicd).ToString().Trim();
                                                            dthuocct.SoLuong = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colSoLuongcd).ToString().Trim());
                                                            dthuocct.DonGia = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colDonGiacd).ToString());
                                                            dthuocct.ThanhTien = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, ColThanhTiencd).ToString());
                                                            if (grvchiDinh.GetRowCellValue(i, colTrongBHdv) != null && grvchiDinh.GetRowCellValue(i, colTrongBHdv).ToString() != "")
                                                                dthuocct.TrongBH = Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colTrongBHdv));
                                                            else
                                                                dthuocct.TrongBH = 0;
                                                            if (grvchiDinh.GetRowCellValue(i, colNgayNhap) != null && grvchiDinh.GetRowCellValue(i, colNgayNhap).ToString() != "")
                                                                dthuocct.NgayNhap = Convert.ToDateTime(grvchiDinh.GetRowCellValue(i, colNgayNhap).ToString());
                                                            if (grvchiDinh.GetRowCellValue(i, colBSTH) != null)
                                                                dthuocct.MaCB = grvchiDinh.GetRowCellValue(i, colBSTH).ToString();
                                                            if (grvchiDinh.GetRowCellValue(i, colKPhongdv) != null && grvchiDinh.GetRowCellValue(i, colKPhongdv).ToString() != "")
                                                                dthuocct.MaKP = grvchiDinh.GetRowCellValue(i, colKPhongdv) == null ? 0 : Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colKPhongdv));
                                                            else
                                                                dthuocct.MaKP = 0;
                                                            if (grvchiDinh.GetRowCellValue(i, colTyLeBHTT) != null && grvchiDinh.GetRowCellValue(i, colTyLeBHTT).ToString() != "")
                                                                dthuocct.TyLeTT = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colTyLeBHTT));
                                                            else
                                                                dthuocct.TyLeTT = 0;
                                                            _dataContext.DThuoccts.Add(dthuocct);
                                                            _dataContext.SaveChanges();
                                                        }
                                                    }
                                                }
                                            }

                                            xtraChiDinh.Text = "Dịch vụ";
                                            //MessageBox.Show("Tạo mới thành công");

                                            grvchiDinh.OptionsBehavior.Editable = true;
                                            //EnableButton(true); 
                                            xtraChiDinh.Refresh();
                                            int idcd = 0;
                                            var q = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                            if (q.Count > 0)
                                            {
                                                txtChiDinh.Text = q.First().IDDon.ToString();
                                                idcd = q.First().IDDon;
                                            }
                                            var data = (from dt in _dataContext.DThuoccts.Where(p => p.IDDon == idcd).Where(p => p.LoaiDV < 3)
                                                        join dv in _dataContext.DichVus on dt.MaDV equals dv.MaDV
                                                        select new DonThuocct
                                                        {
                                                            IDDon = dt.IDDon,
                                                            IDDonct = dt.IDDonct,
                                                            MaDV = dt.MaDV,
                                                            TenDV = dv.TenDV,
                                                            DonVi = dt.DonVi,
                                                            DonGia = dt.DonGia,
                                                            SoLuong = dt.SoLuong,
                                                            ThanhTien = dt.ThanhTien,
                                                            TienBN = dt.TienBN,
                                                            TienBH = dt.TienBH,
                                                            TrongBH = dt.TrongBH,
                                                            NgayNhap = dt.NgayNhap,
                                                            DuongD = dt.DuongD,
                                                            SoPL = dt.SoPL,
                                                            Status = dt.Status,
                                                            IDCD = dt.IDCD,
                                                            MaCB = dt.MaCB,
                                                            MaKP = dt.MaKP,
                                                            IDKB = dt.IDKB,
                                                            Loai = dt.Loai,
                                                            ThanhToan = dt.ThanhToan,
                                                            MaKPtk = dt.MaKPtk,
                                                            MaKXuat = dt.MaKXuat,
                                                            TyLeTT = dt.TyLeTT,
                                                            XHH = dt.XHH == 1 ? true : false,
                                                            MaQD = dv.MaQD
                                                        }).ToList();
                                            var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                                            if (hthong != null && hthong.IsTV == true && data.Exists(o => o.TenDV.ToLower().Contains("khám bác sỹ chuyên gia")))
                                                chkKhamChuyenGia.Checked = true;
                                            else
                                                chkKhamChuyenGia.Checked = false;
                                            binSChiDinhct.DataSource = data.ToList();
                                            grcChiDinh.DataSource = binSChiDinhct;
                                            mnLuu.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        if (TTLuu == 2)
                                        {

                                            var ktdt = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                            if (ktdt.Count > 0 && ktdt.Count < 2)
                                            {
                                                // lưu dịch vụ
                                                int id = ktdt.First().IDDon;
                                                var suadv = _dataContext.DThuocs.Single(p => p.IDDon == id);
                                                suadv.NgayKe = dtNgayKhamkb.DateTime;
                                                _dataContext.SaveChanges();
                                                // lưu chi tiết dịch vụ
                                                for (int i = 0; i < grvchiDinh.DataRowCount; i++)
                                                {
                                                    if (grvchiDinh.GetRowCellValue(i, colMaDVcd) != null && grvchiDinh.GetRowCellValue(i, colMaDVcd).ToString() != "")
                                                    {
                                                        if (grvchiDinh.GetRowCellValue(i, colSoLuongcd) != null && grvchiDinh.GetRowCellValue(i, colSoLuongcd).ToString() != "")
                                                        {
                                                            if (grvchiDinh.GetRowCellValue(i, colIDctcd) != null)
                                                            {
                                                                int idct = 0;
                                                                if (grvchiDinh.GetRowCellValue(i, colIDctcd).ToString() == "" || grvchiDinh.GetRowCellValue(i, colIDctcd).ToString() == string.Empty)
                                                                    idct = 0;
                                                                else
                                                                    idct = int.Parse(grvchiDinh.GetRowCellValue(i, colIDctcd).ToString());
                                                                if (idct > 0)// sửa row
                                                                {
                                                                    double soluong = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colSoLuongcd).ToString().Trim());
                                                                    //if (grvchiDinh.GetRowCellValue(i, colBSTH) == null)
                                                                    //{
                                                                    //    MessageBox.Show("bạn chưa nhập bác sĩ thực hiện theo CV4210");
                                                                    //    return;
                                                                    //}
                                                                    //else 
                                                                    if (soluong > 0)
                                                                    {
                                                                        //if (grvchiDinh.GetRowCellValue(i, colTTLuucd) != null && grvchiDinh.GetRowCellValue(i, colTTLuucd).ToString() == "2")
                                                                        //{ // kiểm tra lại, sau này chỉ những row được sửa mới lưu
                                                                        DThuocct dthuocct = _dataContext.DThuoccts.Single(p => p.IDDonct == idct);
                                                                        //dthuocct.IDDon =int.Parse(txtChiDinh.Text);
                                                                        if (lupKhoaKhamkb.EditValue != null)
                                                                            dthuocct.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                                        else
                                                                            dthuocct.MaKP = DungChung.Bien.MaKP;


                                                                        dthuocct.MaDV = (grvchiDinh.GetRowCellValue(i, colMaDVcd) == null || grvchiDinh.GetRowCellValue(i, colMaDVcd).ToString() == "") ? 0 : Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colMaDVcd));
                                                                        dthuocct.DonVi = grvchiDinh.GetRowCellValue(i, colDonVicd).ToString().Trim();
                                                                        dthuocct.DonGia = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colDonGiacd).ToString());
                                                                        dthuocct.SoLuong = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colSoLuongcd).ToString().Trim());
                                                                        dthuocct.ThanhTien = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, ColThanhTiencd).ToString());
                                                                        if (grvchiDinh.GetRowCellValue(i, colTrongBHdv) != null && grvchiDinh.GetRowCellValue(i, colTrongBHdv).ToString() != "")
                                                                            dthuocct.TrongBH = Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colTrongBHdv));
                                                                        else
                                                                            dthuocct.TrongBH = 0;
                                                                        if (grvchiDinh.GetRowCellValue(i, colNgayNhap) != null && grvchiDinh.GetRowCellValue(i, colNgayNhap).ToString() != "")
                                                                            dthuocct.NgayNhap = Convert.ToDateTime(grvchiDinh.GetRowCellValue(i, colNgayNhap).ToString());
                                                                        if (grvchiDinh.GetRowCellValue(i, colBSTH) != null)
                                                                            dthuocct.MaCB = grvchiDinh.GetRowCellValue(i, colBSTH).ToString();
                                                                        if (grvchiDinh.GetRowCellValue(i, colKPhongdv) != null && grvchiDinh.GetRowCellValue(i, colKPhongdv).ToString() != "")
                                                                            dthuocct.MaKP = Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colKPhongdv));
                                                                        else
                                                                            dthuocct.MaKP = 0;
                                                                        if (grvchiDinh.GetRowCellValue(i, colTyLeBHTT) != null && grvchiDinh.GetRowCellValue(i, colTyLeBHTT).ToString() != "")
                                                                            dthuocct.TyLeTT = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colTyLeBHTT));
                                                                        else
                                                                            dthuocct.TyLeTT = 0;
                                                                        if (grvchiDinh.GetRowCellValue(i, colXHH) != null && grvchiDinh.GetRowCellValue(i, colXHH) != "" && grvchiDinh.GetRowCellValue(i, colXHH) != string.Empty)
                                                                        {
                                                                            bool xhh = Convert.ToBoolean(grvchiDinh.GetRowCellValue(i, colXHH));
                                                                            dthuocct.XHH = xhh == true ? 1 : 0;
                                                                        }
                                                                        _dataContext.SaveChanges();
                                                                        //}
                                                                    }
                                                                }
                                                                else
                                                                {// lưu row mới 
                                                                    double soluong = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colSoLuongcd).ToString().Trim());
                                                                    //if (grvchiDinh.GetRowCellValue(i, colBSTH) == null)
                                                                    //{
                                                                    //    MessageBox.Show("bạn chưa nhập bác sĩ thực hiện theo CV4210");
                                                                    //    return;
                                                                    //}
                                                                    //else
                                                                    if (soluong > 0)
                                                                    {
                                                                        DThuocct dthuocct = new DThuocct();
                                                                        if (lupKhoaKhamkb.EditValue != null)
                                                                            dthuocct.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                                        else
                                                                            dthuocct.MaKP = DungChung.Bien.MaKP;
                                                                        dthuocct.IDDon = int.Parse(txtChiDinh.Text.Trim());
                                                                        dthuocct.MaDV = (grvchiDinh.GetRowCellValue(i, colMaDVcd) == null || grvchiDinh.GetRowCellValue(i, colMaDVcd).ToString() == "") ? 0 : Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colMaDVcd));
                                                                        dthuocct.DonVi = grvchiDinh.GetRowCellValue(i, colDonVicd).ToString().Trim();
                                                                        dthuocct.DonGia = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colDonGiacd).ToString());
                                                                        dthuocct.SoLuong = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colSoLuongcd).ToString().Trim());
                                                                        dthuocct.ThanhTien = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, ColThanhTiencd).ToString());
                                                                        if (grvchiDinh.GetRowCellValue(i, colTrongBHdv) != null && grvchiDinh.GetRowCellValue(i, colTrongBHdv).ToString() != "")
                                                                            dthuocct.TrongBH = Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colTrongBHdv));
                                                                        else
                                                                            dthuocct.TrongBH = 0;
                                                                        if (grvchiDinh.GetRowCellValue(i, colNgayNhap) != null && grvchiDinh.GetRowCellValue(i, colNgayNhap).ToString() != "")
                                                                            dthuocct.NgayNhap = Convert.ToDateTime(grvchiDinh.GetRowCellValue(i, colNgayNhap).ToString());
                                                                        if (grvchiDinh.GetRowCellValue(i, colBSTH) != null)
                                                                            dthuocct.MaCB = grvchiDinh.GetRowCellValue(i, colBSTH).ToString();
                                                                        if (grvchiDinh.GetRowCellValue(i, colKPhongdv) != null && grvchiDinh.GetRowCellValue(i, colKPhongdv).ToString() != "")
                                                                            dthuocct.MaKP = Convert.ToInt32(grvchiDinh.GetRowCellValue(i, colKPhongdv));
                                                                        else
                                                                            dthuocct.MaKP = 0;
                                                                        if (grvchiDinh.GetRowCellValue(i, colTyLeBHTT) != null && grvchiDinh.GetRowCellValue(i, colTyLeBHTT).ToString() != "")
                                                                            dthuocct.TyLeTT = Convert.ToDouble(grvchiDinh.GetRowCellValue(i, colTyLeBHTT));
                                                                        else
                                                                            dthuocct.TyLeTT = 0;
                                                                        if (grvchiDinh.GetRowCellValue(i, colXHH) != null && grvchiDinh.GetRowCellValue(i, colXHH).ToString() != "")
                                                                        {
                                                                            bool xhh = Convert.ToBoolean(grvchiDinh.GetRowCellValue(i, colXHH));
                                                                            dthuocct.XHH = xhh == true ? 1 : 0;
                                                                        }
                                                                        _dataContext.DThuoccts.Add(dthuocct);
                                                                        _dataContext.SaveChanges();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                                xtraChiDinh.Text = "Dịch vụ";
                                                MessageBox.Show("Lưu thành công");
                                                //EnableButton(true);
                                                grvchiDinh.OptionsBehavior.Editable = true;
                                                xtraChiDinh.Refresh();
                                                int idcd = 0;
                                                var q = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                                if (q.Count > 0)
                                                {
                                                    txtChiDinh.Text = q.First().IDDon.ToString();
                                                    idcd = q.First().IDDon;
                                                }
                                                var data = (from dt in _dataContext.DThuoccts.Where(p => p.IDDon == idcd).Where(p => p.LoaiDV < 3)
                                                            join dv in _dataContext.DichVus on dt.MaDV equals dv.MaDV
                                                            select new DonThuocct
                                                            {
                                                                IDDon = dt.IDDon,
                                                                IDDonct = dt.IDDonct,
                                                                MaDV = dt.MaDV,
                                                                TenDV = dv.TenDV,
                                                                DonVi = dt.DonVi,
                                                                DonGia = dt.DonGia,
                                                                SoLuong = dt.SoLuong,
                                                                ThanhTien = dt.ThanhTien,
                                                                TienBN = dt.TienBN,
                                                                TienBH = dt.TienBH,
                                                                TrongBH = dt.TrongBH,
                                                                NgayNhap = dt.NgayNhap,
                                                                DuongD = dt.DuongD,
                                                                SoPL = dt.SoPL,
                                                                Status = dt.Status,
                                                                IDCD = dt.IDCD,
                                                                MaCB = dt.MaCB,
                                                                MaKP = dt.MaKP,
                                                                IDKB = dt.IDKB,
                                                                Loai = dt.Loai,
                                                                ThanhToan = dt.ThanhToan,
                                                                MaKPtk = dt.MaKPtk,
                                                                MaKXuat = dt.MaKXuat,
                                                                TyLeTT = dt.TyLeTT,
                                                                XHH = dt.XHH == 1 ? true : false,
                                                                MaQD = dv.MaQD
                                                            }).ToList();
                                                var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                                                if (hthong != null && hthong.IsTV == true && data.Exists(o => o.TenDV.ToLower().Contains("khám bác sỹ chuyên gia")))
                                                    chkKhamChuyenGia.Checked = true;
                                                else
                                                    chkKhamChuyenGia.Checked = false;
                                                binSChiDinhct.DataSource = data.ToList();
                                                grcChiDinh.DataSource = binSChiDinhct;
                                                mnLuu.Enabled = false;

                                            }
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi! không lưu được chỉ định: " + ex.Message);
                                }

                                grvBNhankb_FocusedRowChanged(null, null);

                            }

                        }
                        else
                        {
                            // MessageBox.Show("Bệnh nhân đã TT");

                            grvBNhankb_FocusedRowChanged(null, null);

                        }
                        //grvBNhankb_FocusedRowChanged(null, null);
                        break;
                    case 4:

                        bool checkValidatenew = true;
                        if (!KTraKB())
                            checkValidatenew = false;
                        else if (DungChung.Ham.KTraTT(_dataContext, _int_maBN))
                        {
                            checkValidatenew = false;
                            MessageBox.Show("Bệnh nhân đã TT");
                        }
                        else if (DungChung.Ham.KT_RaVien_ngt(_dataContext, _int_maBN))
                        {
                            checkValidatenew = false;
                            MessageBox.Show("Bệnh nhân đã ra viện");
                        }
                        //    grvBNhankb_FocusedRowChanged(null, null);

                        // if (KTraKB() && !DungChung.Ham.KTraTT(data, _int_maBN) && !DungChung.Ham.KT_RaVien_ngt(data, _int_maBN))
                        if (checkValidatenew)
                        {
                            bool ktraluu = true;
                            string _loingaynhap = "", loikp = "", loisoluong = "", loikokp = "";
                            int dem1 = 0, dem2 = 0, dem3 = 0, dem4 = 0;
                            for (int i = 0; i < grvDichVuCS2.DataRowCount; i++)
                            {
                                int _iddonct = -1;
                                if (grvDichVuCS2.GetRowCellValue(i, colIDdoncs2) != null && grvDichVuCS2.GetRowCellValue(i, colIDdoncs2).ToString() != "")
                                {
                                    _iddonct = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colIDdoncs2));
                                }
                                if (grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2) != null && grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2).ToString() != "")
                                {
                                    //if (_iddonct <= 0)
                                    //{
                                    if (grvDichVuCS2.GetRowCellValue(i, colDichVuCS2) != null && grvDichVuCS2.GetRowCellValue(i, colDichVuCS2).ToString() != "")
                                    {
                                        DateTime _ngaynhap = Convert.ToDateTime(grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2));
                                        if (grvDichVuCS2.GetRowCellValue(i, colKPhongCS2) != null && grvDichVuCS2.GetRowCellValue(i, colKPhongCS2).ToString() != "")
                                        {
                                            int _makp = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colKPhongCS2));
                                            bool ktra = true;
                                            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                                            {
                                                var kttt = _lKPhong_data.Where(p => p.MaKP == _makp && p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh).ToList();
                                                if (kttt.Count > 0)
                                                    ktra = false;
                                            }
                                            if (ktra)
                                            {
                                                var ngaykham = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN && p.MaKP == _makp).Select(p => p.NgayKham).FirstOrDefault();
                                                if (ngaykham != null)
                                                {
                                                    if (_iddonct <= 0 && (ngaykham.Value > _ngaynhap))
                                                    {
                                                        dem1++;
                                                        _loingaynhap += grvDichVuCS2.GetRowCellDisplayText(i, colDichVuCS2) + ";\n";
                                                    }
                                                }
                                                else
                                                {
                                                    dem2++;
                                                    loikp += grvDichVuCS2.GetRowCellDisplayText(i, colDichVuCS2) + ";\n";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            dem4++;
                                            loikokp += grvDichVuCS2.GetRowCellDisplayText(i, colDichVuCS2) + ";\n";
                                        }
                                        if (grvDichVuCS2.GetRowCellValue(i, colSLCS2) != null && grvDichVuCS2.GetRowCellValue(i, colSLCS2).ToString() != "")
                                        {
                                            Double _soluong = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colSLCS2));
                                            if (_soluong <= 0)
                                            {
                                                dem3++;
                                                loisoluong += grvDichVuCS2.GetRowCellDisplayText(i, colDichVuCS2) + ";\n";
                                            }
                                        }
                                    }
                                    //}
                                }
                                else
                                {
                                    dem1++;
                                    _loingaynhap += grvDichVuCS2.GetRowCellDisplayText(i, colDichVuCS2) + ";\n";
                                }
                            }
                            if (dem1 > 0)
                            {
                                MessageBox.Show("các dịch vụ: " + _loingaynhap.ToString() + "\n có ngày nhập không hợp lệ nhỏ hơn ngày khám hoặc lớn hơn ngày hiện tại");
                                ktraluu = false;
                            }
                            if (dem2 > 0)
                            {
                                MessageBox.Show("các dịch vụ: " + loikp.ToString() + "\n có khoa|phòng không hợp lệ");
                                ktraluu = false;
                            }
                            if (dem3 > 0)
                            {
                                MessageBox.Show("các dịch vụ: " + loikp.ToString() + "\n có số lượng không hợp lệ");
                                ktraluu = false;
                            }
                            if (dem4 > 0)
                            {
                                MessageBox.Show("các dịch vụ: " + loikokp.ToString() + "\n chưa có Khoa|Phòng");
                                ktraluu = false;
                            }
                            if (ktraluu)
                            {
                                try
                                {
                                    var ktdthuoc = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                    if (ktdthuoc.Count > 0)
                                        TTLuu = 2;
                                    else
                                        TTLuu = 1;
                                    if (TTLuu == 1)
                                    {
                                        DThuoc dthuoccd = new DThuoc();
                                        dthuoccd.NgayKe = dtNgayKhamkb.DateTime;
                                        //dthuoccd.MaKP = lupKhoaKhamkb.EditValue.ToString();
                                        dthuoccd.MaBNhan = _int_maBN;
                                        dthuoccd.MaCB = lupNguoiKhamkb.EditValue.ToString();
                                        if (lupKhoaKhamkb.EditValue != null)
                                            dthuoccd.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                        else
                                            dthuoccd.MaKP = DungChung.Bien.MaKP;
                                        dthuoccd.PLDV = 2;
                                        dthuoccd.KieuDon = -1;
                                        _dataContext.DThuocs.Add(dthuoccd);
                                        if (_dataContext.SaveChanges() >= 0)
                                        {
                                            var maxid = dthuoccd.IDDon;
                                            for (int i = 0; i < grvDichVuCS2.DataRowCount; i++)
                                            {
                                                if (grvDichVuCS2.GetRowCellValue(i, colDichVuCS2) != null && grvDichVuCS2.GetRowCellValue(i, colDichVuCS2).ToString() != "")
                                                {
                                                    if (grvDichVuCS2.GetRowCellValue(i, colSLCS2) != null && grvDichVuCS2.GetRowCellValue(i, colSLCS2).ToString() != "")
                                                    {
                                                        double soluong = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colSLCS2).ToString().Trim());
                                                        //int trongbh=Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, coltr);
                                                        //if (grvDichVuCS2.GetRowCellValue(i, colBSTH) == null)
                                                        //{
                                                        //    MessageBox.Show("bạn chưa nhập bác sĩ thực hiện theo CV4210");
                                                        //    return;
                                                        //}
                                                        //else
                                                        if (soluong > 0)
                                                        {
                                                            // lưu row mới 
                                                            DThuocct dthuocct = new DThuocct();
                                                            if (lupKhoaKhamkb.EditValue != null)
                                                                dthuocct.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                            else
                                                                dthuocct.MaKP = DungChung.Bien.MaKP;
                                                            if (grvDichVuCS2.GetRowCellValue(i, colXHHCS2) != null && grvDichVuCS2.GetRowCellValue(i, colXHHCS2).ToString() != "")
                                                            {
                                                                bool xhh = Convert.ToBoolean(grvDichVuCS2.GetRowCellValue(i, colXHHCS2));
                                                                dthuocct.XHH = xhh == true ? 1 : 0;
                                                            }
                                                            dthuocct.IDDon = maxid;
                                                            dthuocct.MaDV = grvDichVuCS2.GetRowCellValue(i, colDichVuCS2) == null ? 0 : Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colDichVuCS2));
                                                            dthuocct.DonVi = grvDichVuCS2.GetRowCellValue(i, colDonViCS2).ToString().Trim();
                                                            dthuocct.SoLuong = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colSLCS2).ToString().Trim());
                                                            dthuocct.DonGia = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colGiaBHYTCS2).ToString());
                                                            dthuocct.ThanhTien = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colThanhTienCS2).ToString());
                                                            if (grvDichVuCS2.GetRowCellValue(i, colBHTTCS2) != null && grvDichVuCS2.GetRowCellValue(i, colBHTTCS2).ToString() != "")
                                                                dthuocct.TrongBH = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colBHTTCS2));
                                                            else
                                                                dthuocct.TrongBH = 0;
                                                            if (grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2) != null && grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2).ToString() != "")
                                                                dthuocct.NgayNhap = Convert.ToDateTime(grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2).ToString());
                                                            if (grvDichVuCS2.GetRowCellValue(i, colBSTHCS2) != null)
                                                                dthuocct.MaCB = grvDichVuCS2.GetRowCellValue(i, colBSTHCS2).ToString();
                                                            if (grvDichVuCS2.GetRowCellValue(i, colKPhongCS2) != null && grvDichVuCS2.GetRowCellValue(i, colKPhongCS2).ToString() != "")
                                                                dthuocct.MaKP = grvDichVuCS2.GetRowCellValue(i, colKPhongCS2) == null ? 0 : Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colKPhongCS2));
                                                            else
                                                                dthuocct.MaKP = 0;
                                                            if (grvDichVuCS2.GetRowCellValue(i, colTLTTdvCS2) != null && grvDichVuCS2.GetRowCellValue(i, colTLTTdvCS2).ToString() != "")
                                                                dthuocct.TyLeTT = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colTLTTdvCS2));
                                                            else
                                                                dthuocct.TyLeTT = 0;
                                                            dthuocct.LoaiDV = 3;
                                                            _dataContext.DThuoccts.Add(dthuocct);
                                                            _dataContext.SaveChanges();
                                                        }
                                                    }
                                                }
                                            }

                                            xtabDichVuCS2.Text = "Dịch vụ CS2";
                                            //MessageBox.Show("Tạo mới thành công");

                                            grvDichVuCS2.OptionsBehavior.Editable = true;
                                            //EnableButton(true); 
                                            xtabDichVuCS2.Refresh();
                                            int idcd = 0;
                                            var q = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                            if (q.Count > 0)
                                            {
                                                txtChiDinh.Text = q.First().IDDon.ToString();
                                                idcd = q.First().IDDon;
                                            }
                                            var data = (from dt in _dataContext.DThuoccts.Where(p => p.IDDon == idcd).Where(p => p.LoaiDV == 3)
                                                        join dv in _dataContext.DichVus on dt.MaDV equals dv.MaDV
                                                        select new DonThuocct
                                                        {
                                                            IDDon = dt.IDDon,
                                                            IDDonct = dt.IDDonct,
                                                            MaDV = dt.MaDV,
                                                            TenDV = dv.TenDV,
                                                            DonVi = dt.DonVi,
                                                            DonGia = dt.DonGia,
                                                            SoLuong = dt.SoLuong,
                                                            ThanhTien = dt.ThanhTien,
                                                            TienBN = dt.TienBN,
                                                            TienBH = dt.TienBH,
                                                            TrongBH = dt.TrongBH,
                                                            NgayNhap = dt.NgayNhap,
                                                            DuongD = dt.DuongD,
                                                            SoPL = dt.SoPL,
                                                            Status = dt.Status,
                                                            IDCD = dt.IDCD,
                                                            MaCB = dt.MaCB,
                                                            MaKP = dt.MaKP,
                                                            IDKB = dt.IDKB,
                                                            Loai = dt.Loai,
                                                            ThanhToan = dt.ThanhToan,
                                                            MaKPtk = dt.MaKPtk,
                                                            MaKXuat = dt.MaKXuat,
                                                            TyLeTT = dt.TyLeTT,
                                                            XHH = dt.XHH == 1 ? true : false,
                                                            MaQD = dv.MaQD
                                                        }).ToList();
                                            var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                                            if (hthong != null && hthong.IsTV == true && data.Exists(o => o.TenDV.ToLower().Contains("khám bác sỹ chuyên gia")))
                                                chkKhamChuyenGia.Checked = true;
                                            else
                                                chkKhamChuyenGia.Checked = false;
                                            binDichVuCS2.DataSource = data;
                                            grcDichVuCS2.DataSource = binDichVuCS2;
                                            mnLuu.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        if (TTLuu == 2)
                                        {

                                            var ktdt = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                            if (ktdt.Count > 0 && ktdt.Count < 2)
                                            {
                                                // lưu dịch vụ
                                                int id = ktdt.First().IDDon;
                                                var suadv = _dataContext.DThuocs.Single(p => p.IDDon == id);
                                                suadv.NgayKe = dtNgayKhamkb.DateTime;
                                                _dataContext.SaveChanges();
                                                // lưu chi tiết dịch vụ
                                                for (int i = 0; i < grvDichVuCS2.DataRowCount; i++)
                                                {
                                                    if (grvDichVuCS2.GetRowCellValue(i, colDichVuCS2) != null && grvDichVuCS2.GetRowCellValue(i, colDichVuCS2).ToString() != "")
                                                    {
                                                        if (grvDichVuCS2.GetRowCellValue(i, colSLCS2) != null && grvDichVuCS2.GetRowCellValue(i, colSLCS2).ToString() != "")
                                                        {
                                                            if (grvDichVuCS2.GetRowCellValue(i, colIDdoncs2) != null)
                                                            {
                                                                int idct = 0;
                                                                if (grvDichVuCS2.GetRowCellValue(i, colIDdoncs2).ToString() == "" || grvDichVuCS2.GetRowCellValue(i, colIDdoncs2).ToString() == string.Empty)
                                                                    idct = 0;
                                                                else
                                                                    idct = int.Parse(grvDichVuCS2.GetRowCellValue(i, colIDdoncs2).ToString());
                                                                if (idct > 0)// sửa row
                                                                {
                                                                    double soluong = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colSLCS2).ToString().Trim());
                                                                    //if (grvDichVuCS2.GetRowCellValue(i, colBSTH) == null)
                                                                    //{
                                                                    //    MessageBox.Show("bạn chưa nhập bác sĩ thực hiện theo CV4210");
                                                                    //    return;
                                                                    //}
                                                                    //else 
                                                                    if (soluong > 0)
                                                                    {
                                                                        //if (grvDichVuCS2.GetRowCellValue(i, colTTLuucd) != null && grvDichVuCS2.GetRowCellValue(i, colTTLuucd).ToString() == "2")
                                                                        //{ // kiểm tra lại, sau này chỉ những row được sửa mới lưu
                                                                        DThuocct dthuocct = _dataContext.DThuoccts.Single(p => p.IDDonct == idct);
                                                                        //dthuocct.IDDon =int.Parse(txtChiDinh.Text);
                                                                        if (lupKhoaKhamkb.EditValue != null)
                                                                            dthuocct.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                                        else
                                                                            dthuocct.MaKP = DungChung.Bien.MaKP;


                                                                        dthuocct.MaDV = (grvDichVuCS2.GetRowCellValue(i, colDichVuCS2) == null || grvDichVuCS2.GetRowCellValue(i, colDichVuCS2).ToString() == "") ? 0 : Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colMaDVcd));
                                                                        dthuocct.DonVi = grvDichVuCS2.GetRowCellValue(i, colDonViCS2).ToString().Trim();
                                                                        dthuocct.DonGia = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colGiaBHYTCS2).ToString());
                                                                        dthuocct.SoLuong = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colSLCS2).ToString().Trim());
                                                                        dthuocct.ThanhTien = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colThanhTienCS2).ToString());
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colBHTTCS2) != null && grvDichVuCS2.GetRowCellValue(i, colBHTTCS2).ToString() != "")
                                                                            dthuocct.TrongBH = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colBHTTCS2));
                                                                        else
                                                                            dthuocct.TrongBH = 0;
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2) != null && grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2).ToString() != "")
                                                                            dthuocct.NgayNhap = Convert.ToDateTime(grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2).ToString());
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colBSTHCS2) != null)
                                                                            dthuocct.MaCB = grvDichVuCS2.GetRowCellValue(i, colBSTHCS2).ToString();
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colKPhongCS2) != null && grvDichVuCS2.GetRowCellValue(i, colKPhongCS2).ToString() != "")
                                                                            dthuocct.MaKP = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colKPhongCS2));
                                                                        else
                                                                            dthuocct.MaKP = 0;
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colTLTTdvCS2) != null && grvDichVuCS2.GetRowCellValue(i, colTLTTdvCS2).ToString() != "")
                                                                            dthuocct.TyLeTT = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colTLTTdvCS2));
                                                                        else
                                                                            dthuocct.TyLeTT = 0;
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colXHH) != null && grvDichVuCS2.GetRowCellValue(i, colXHHCS2) != "" && grvDichVuCS2.GetRowCellValue(i, colXHHCS2) != string.Empty)
                                                                        {
                                                                            bool xhh = Convert.ToBoolean(grvDichVuCS2.GetRowCellValue(i, colXHHCS2));
                                                                            dthuocct.XHH = xhh == true ? 1 : 0;
                                                                        }
                                                                        dthuocct.LoaiDV = 3;
                                                                        _dataContext.SaveChanges();
                                                                        //}
                                                                    }
                                                                }
                                                                else
                                                                {// lưu row mới 
                                                                    double soluong = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colSLCS2).ToString().Trim());
                                                                    //if (grvDichVuCS2.GetRowCellValue(i, colBSTH) == null)
                                                                    //{
                                                                    //    MessageBox.Show("bạn chưa nhập bác sĩ thực hiện theo CV4210");
                                                                    //    return;
                                                                    //}
                                                                    //else
                                                                    if (soluong > 0)
                                                                    {
                                                                        DThuocct dthuocct = new DThuocct();
                                                                        if (lupKhoaKhamkb.EditValue != null)
                                                                            dthuocct.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                                                                        else
                                                                            dthuocct.MaKP = DungChung.Bien.MaKP;
                                                                        dthuocct.IDDon = int.Parse(txtChiDinh.Text.Trim());
                                                                        dthuocct.MaDV = (grvDichVuCS2.GetRowCellValue(i, colDichVuCS2) == null || grvDichVuCS2.GetRowCellValue(i, colDichVuCS2).ToString() == "") ? 0 : Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colMaDVcd));
                                                                        dthuocct.DonVi = grvDichVuCS2.GetRowCellValue(i, colDonViCS2).ToString().Trim();
                                                                        dthuocct.DonGia = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colGiaBHYTCS2).ToString());
                                                                        dthuocct.SoLuong = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colSLCS2).ToString().Trim());
                                                                        dthuocct.ThanhTien = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colThanhTienCS2).ToString());
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colBHTTCS2) != null && grvDichVuCS2.GetRowCellValue(i, colBHTTCS2).ToString() != "")
                                                                            dthuocct.TrongBH = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colBHTTCS2));
                                                                        else
                                                                            dthuocct.TrongBH = 0;
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2) != null && grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2).ToString() != "")
                                                                            dthuocct.NgayNhap = Convert.ToDateTime(grvDichVuCS2.GetRowCellValue(i, colNgayNhapdvCS2).ToString());
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colBSTHCS2) != null)
                                                                            dthuocct.MaCB = grvDichVuCS2.GetRowCellValue(i, colBSTHCS2).ToString();
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colKPhongCS2) != null && grvDichVuCS2.GetRowCellValue(i, colKPhongCS2).ToString() != "")
                                                                            dthuocct.MaKP = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colKPhongCS2));
                                                                        else
                                                                            dthuocct.MaKP = 0;
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colTLTTdvCS2) != null && grvDichVuCS2.GetRowCellValue(i, colTLTTdvCS2).ToString() != "")
                                                                            dthuocct.TyLeTT = Convert.ToDouble(grvDichVuCS2.GetRowCellValue(i, colTLTTdvCS2));
                                                                        else
                                                                            dthuocct.TyLeTT = 0;
                                                                        if (grvDichVuCS2.GetRowCellValue(i, colXHHCS2) != null && grvDichVuCS2.GetRowCellValue(i, colXHHCS2).ToString() != "")
                                                                        {
                                                                            bool xhh = Convert.ToBoolean(grvDichVuCS2.GetRowCellValue(i, colXHHCS2));
                                                                            dthuocct.XHH = xhh == true ? 1 : 0;
                                                                        }
                                                                        dthuocct.LoaiDV = 3;
                                                                        _dataContext.DThuoccts.Add(dthuocct);
                                                                        _dataContext.SaveChanges();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }



                                                xtabDichVuCS2.Text = "Dịch vụ CS2";
                                                MessageBox.Show("Lưu thành công");
                                                //EnableButton(true);
                                                grvDichVuCS2.OptionsBehavior.Editable = true;
                                                xtabDichVuCS2.Refresh();
                                                int idcd = 0;
                                                var q = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.PLDV == 2).ToList();
                                                if (q.Count > 0)
                                                {
                                                    txtChiDinh.Text = q.First().IDDon.ToString();
                                                    idcd = q.First().IDDon;
                                                }
                                                var data = (from dt in _dataContext.DThuoccts.Where(p => p.IDDon == idcd).Where(p => p.LoaiDV == 3)
                                                            join dv in _dataContext.DichVus on dt.MaDV equals dv.MaDV
                                                            select new DonThuocct
                                                            {
                                                                IDDon = dt.IDDon,
                                                                IDDonct = dt.IDDonct,
                                                                MaDV = dt.MaDV,
                                                                TenDV = dv.TenDV,
                                                                DonVi = dt.DonVi,
                                                                DonGia = dt.DonGia,
                                                                SoLuong = dt.SoLuong,
                                                                ThanhTien = dt.ThanhTien,
                                                                TienBN = dt.TienBN,
                                                                TienBH = dt.TienBH,
                                                                TrongBH = dt.TrongBH,
                                                                NgayNhap = dt.NgayNhap,
                                                                DuongD = dt.DuongD,
                                                                SoPL = dt.SoPL,
                                                                Status = dt.Status,
                                                                IDCD = dt.IDCD,
                                                                MaCB = dt.MaCB,
                                                                MaKP = dt.MaKP,
                                                                IDKB = dt.IDKB,
                                                                Loai = dt.Loai,
                                                                ThanhToan = dt.ThanhToan,
                                                                MaKPtk = dt.MaKPtk,
                                                                MaKXuat = dt.MaKXuat,
                                                                TyLeTT = dt.TyLeTT,
                                                                XHH = dt.XHH == 1 ? true : false,
                                                                MaQD = dv.MaQD
                                                            }).ToList();
                                                var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                                                if (hthong != null && hthong.IsTV == true && data.Exists(o => o.TenDV.ToLower().Contains("khám bác sỹ chuyên gia")))
                                                    chkKhamChuyenGia.Checked = true;
                                                else
                                                    chkKhamChuyenGia.Checked = false;
                                                binDichVuCS2.DataSource = data;
                                                grcDichVuCS2.DataSource = binDichVuCS2;
                                                mnLuu.Enabled = false;

                                            }
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi! không lưu được chỉ định: " + ex.Message);
                                }

                                grvBNhankb_FocusedRowChanged(null, null);

                            }

                        }
                        else
                        {
                            // MessageBox.Show("Bệnh nhân đã TT");

                            grvBNhankb_FocusedRowChanged(null, null);

                        }
                        break;
                        txtMaBNhan_TextChanged(null, null);


                }
                GC.Collect();

                if (!string.IsNullOrEmpty(txtIdDonThuoc.Text))
                {
                    var idDonThuoc = int.Parse(txtIdDonThuoc.Text);
                    var donThuoc = _dataContext.DThuocs.FirstOrDefault(f => f.IDDon == idDonThuoc);
                    if (donThuoc != null)
                    {
                        if (donThuoc.DongBo != null)
                            btnSyncMed.Enabled = !donThuoc.DongBo.Value;
                        else
                            btnSyncMed.Enabled = true;
                    }
                }
                else
                {
                    btnSyncMed.Enabled = false;
                }
            }
            finally
            {
                isSave = false;
            }
        }

        private void mnInDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mnDonThuong_ItemClick(sender, e);
        }

        private void mnInPhieuKCB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mnPhieuKCB_ItemClick(sender, e);
        }

        private void mnXoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            try
            {
                if (!string.IsNullOrEmpty(txtIdDonThuoc.Text))
                {
                    int iddon = int.Parse(txtIdDonThuoc.Text);
                    var kt = (from bn in _dataContext.VienPhis.Where(p => p.MaBNhan == _int_maBN) select bn).ToList();
                    if (kt.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã thanh toán, Bạn không được xóa");
                    }
                    else
                    {
                        //List<DThuoc> xoa = new List<DThuoc>();
                        var xoa = (from dt in _dataContext.DThuocs.Where(p => p.IDDon == iddon)
                                   join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                   select new { dt, dtct }).ToList();
                        if (xoa.Count > 0 && xoa.Where(p => p.dtct.Status == 1).ToList().Count > 0)
                        {
                            MessageBox.Show("Đơn này đã được xuất dược, Bạn không được xóa!");
                        }
                        else if (xoa.Count > 0 && xoa.Where(p => p.dtct.SoPL > 0).ToList().Count > 0)
                        {
                            MessageBox.Show("Đơn này đã lên phiếu lĩnh, Bạn không thể xóa");
                        }
                        else if (xoa.Count > 0 && xoa.Where(p => p.dt.SoVV == -1).ToList().Count > 0)
                        {
                            MessageBox.Show("Đơn này đã được duyệt, Bạn không thể xóa");
                        }
                        else if (xoa.Where(p => p.dtct.ThanhToan == 1).Count() > 0)
                        {
                            MessageBox.Show("Bệnh nhân đã có dịch vụ thu trực tiếp, \n Không thể xóa đơn thuốc !");
                        }
                        else
                        {
                            DialogResult result;
                            result = MessageBox.Show("Bạn muốn xóa đơn", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                sothangOld = 1;
                                sothangSua = 1;
                                var dThuoccts = _medicinesProvider.GetDThuocctbyIDDon(iddon);
                                foreach (var s in dThuoccts)
                                {
                                    _medicinesProvider.DeleteDThuocAndDThuocctbyIDDonct(s.IDDonct);

                                    if (_medicinesProvider.isTuTruc((int)s.MaKXuat))
                                    {
                                        _medicinesProvider.UpdateMedicineListPPX3((int)s.MaDV, s.DonGia, s.SoLo, (DateTime)s.HanDung, 0, (int)s.MaKXuat, -s.SoLuong, 2);
                                    }

                                }

                                if (!_medicinesProvider.isTuTruc((int)dThuoccts.First().MaKXuat))
                                {
                                    _medicinesProvider.UpdateMedicineListPPX3(0, 0, "", new DateTime(), 0, maKhoXuat, 0, 0);
                                }

                                int idDon = _medicinesProvider.GetIDDonByMaBNInFrmNgoaiTru(_int_maBN);
                                lupIDThuoc.DataSource = MedicineByRooms = _medicinesProvider.GetLupMaDuoc(MaKPxd, idDon, 0);

                                usKhamBenh_Load(sender, e);
                                lupKhoXuat.EditValue = -1;

                                int _idxoa = 0;
                                if (!string.IsNullOrEmpty(txtIdDonThuoc.Text))
                                    _idxoa = int.Parse(txtIdDonThuoc.Text);
                                binSDonThuocct.DataSource = _lDthuocct.Where(p => p.IDDon == (_idxoa)).OrderBy(p => p.IDDonct).ToList();
                                grcDonThuocct.DataSource = binSDonThuocct;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa kê đơn" + ex.Message);
            }
        }

        private void mnSaoDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            {
                int mabn = Convert.ToInt32(txtMaBNhan.Text);
                var ktdon = _dataContext.DThuocs.Where(p => p.MaBNhan == mabn && p.PLDV == 1 && p.KieuDon == -1).ToList();
                if (ktdon.Count > 0)
                {
                    MessageBox.Show("Bệnh nhân đã có đơn, bạn không thể sao đơn");
                }
                else
                {

                    int idkb = string.IsNullOrEmpty(txtIdkb.Text) ? 0 : Convert.ToInt32(txtIdkb.Text);
                    frm_CopyDon frm = new frm_CopyDon(mabn, idkb, false, 0);
                    frm.ShowDialog();
                    grvBNhankb_FocusedRowChanged(null, null);
                }
            }
        }

        private void txtBenhKhac2_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;

        }

        private void LupICD2_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
            if (!string.IsNullOrEmpty(LupICD2.Text))
            {
                if (LupICD2.EditValue.ToString() == "0")
                {
                    txtBenhPhu2.EditValue = "";
                    txtBenhKhac2.EditValue = "";
                    LupICD2.EditValue = "";
                }
                else
                {
                    txtBenhKhac2.EditValue = lICD.Where(p => p.MaICD == LupICD2.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                    txtBenhPhu2.EditValue = lICD.Where(p => p.MaICD == LupICD2.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                    //txtBenhKhac2.EditValue = LupICD2.EditValue.ToString();

                }

            }
            else
            {
                txtBenhKhac2.EditValue = "";
                txtBenhPhu2.EditValue = "";
            }
            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && LupICD2.EditValue != null && LupICD2.EditValue != "")
            {
                btnPackageICD2.Enabled = true;
            }
            else
                btnPackageICD2.Enabled = false;
        }

        private void txtBenhKhac3_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;

        }

        private void LupICD3_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;

            if (!string.IsNullOrEmpty(LupICD3.Text))
            {
                if (LupICD3.EditValue.ToString() == "0")
                {
                    txtBenhKhac3.EditValue = "";
                    txtBenhPhu3.EditValue = "";
                    LupICD3.EditValue = "";
                }
                else
                {
                    txtBenhKhac3.EditValue = lICD.Where(p => p.MaICD == LupICD3.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                    txtBenhPhu3.EditValue = lICD.Where(p => p.MaICD == LupICD3.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                    //txtBenhKhac3.EditValue =LupICD3.EditValue.ToString();
                }

            }
            else
            {
                txtBenhKhac3.EditValue = "";
                txtBenhPhu3.EditValue = "";
            }
            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && LupICD3.EditValue != null && LupICD3.EditValue != "")
            {
                btnPackageICD3.Enabled = true;
            }
            else
                btnPackageICD3.Enabled = false;
        }

        private void txtBenhKhac4_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
            txtBenhPhu.Text = txtBenhKhac4.Text;
            //if (string.IsNullOrEmpty(LupICD4.Text.Trim()))
            //    LupICD4.EditValue = lICD.Where(p => p.TenICD == txtBenhKhac4.Text).Select(p => p.MaICD).FirstOrDefault();

        }

        private void LupICD4_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;

            if (!string.IsNullOrEmpty(LupICD4.Text))
            {
                if (LupICD4.EditValue.ToString() == "0")
                {
                    txtBenhKhac4.EditValue = "";
                    LupICD4.EditValue = "";
                    //LupICD4.Text = "";
                }
                else
                {
                    if (DungChung.Bien.MaBV == "14017")
                    {
                        for (var i = 0; i < lICD.Count; i++)
                        {

                        }
                    }
                    //getICDtest();
                    //LupICD4.Text = LupICD4.EditValue.ToString();
                    txtBenhKhac4.EditValue = lICD.Where(p => p.MaICD == LupICD4.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                    txtBenhPhu.EditValue = lICD.Where(p => p.MaICD == LupICD4.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                }


            }
            else
            {
                txtBenhKhac4.EditValue = "";
                txtBenhPhu.EditValue = "";
            }
            //if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && LupICD4.EditValue != null && LupICD4.EditValue != "")
            //{
            //    LupICD4.Properties.Buttons[1].Visible = true;
            //}
            //else
            //    LupICD4.Properties.Buttons[1].Visible = false;
            // }
            //if (string.IsNullOrEmpty(txtBenhKhac4.Text.Trim()))
            //    txtBenhKhac4.EditValue = lICD.Where(p => p.MaICD == LupICD4.Text).Select(p => p.TenICD).FirstOrDefault();
        }

        private void LupICD2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD2);
                frm.ShowDialog();
            }

            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhPhu2.Text.Trim() != null || LupICD2.Text.Trim() != null)
                {
                    txtBenhPhu2.Text = null;
                    LupICD2.Text = null;
                }
            }
        }

        private void LupICD3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD3);
                frm.ShowDialog();

            }
        }

        private void LupICD4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void barLichSuKCB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _xemLS(0, 0, false);
        }

        private void InPhieuKhamCK(int mabn)
        {
            var qbn = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn)
                       join bnkb in _dataContext.BNKBs.Where(p => p.PhuongAn == 3) on bn.MaBNhan equals bnkb.MaBNhan
                       join kp in _dataContext.KPhongs on bnkb.MaKP equals kp.MaKP
                       join kp2 in _dataContext.KPhongs on bnkb.MaKPdt equals kp2.MaKP
                       join cb in _dataContext.CanBoes on bnkb.MaCB equals cb.MaCB
                       select new
                       {
                           bnkb.PhuongAn,
                           bn.SoTT,
                           bn.TenBNhan,
                           bn.NgaySinh,
                           bn.ThangSinh,
                           bn.NamSinh,
                           bn.GTinh,
                           bn.DChi,
                           bn.SThe,
                           bn.HanBHDen,
                           bn.HanBHTu,
                           PKMoi = kp2.TenKP,
                           PKCu = kp.TenKP,
                           DChiPK = kp2.DChi,
                           bnkb.ChanDoan,
                           ChanDoanPK = bnkb.ChanDoan,
                           //ChanDoanCK = bnkb.PhuongAn != 3 ? bnkb.ChanDoan : "",
                           BSKB = cb.TenCB,
                           // BSCK = bnkb.PhuongAn != 3 ? cb.TenCB : ""
                       }).ToList();
            if (qbn.Count > 0)
            {
                if (qbn.Any(p => p.PhuongAn == 3))
                {
                    BaoCao.rep_PhieuKhamCK rep = new BaoCao.rep_PhieuKhamCK();
                    var SoDK = _dataContext.SoDKKBs.Where(p => p.MaBNhan == mabn).Select(p => p.SoDK).FirstOrDefault();
                    if (SoDK != null)
                        rep.SoTT.Value = SoDK;
                    rep.PKMoi.Value = qbn.First().PKMoi;
                    rep.TenBN.Value = qbn.First().TenBNhan;
                    rep.NgaySinh.Value = qbn.First().NgaySinh + "/" + qbn.First().ThangSinh + "/" + qbn.First().NamSinh;
                    rep.Gioitinh.Value = qbn.First().GTinh == 1 ? "Nam" : "Nữ";
                    rep.DiaChi.Value = qbn.First().DChi;
                    rep.BH1.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(0, 2);
                    rep.BH2.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(2, 1);
                    rep.BH3.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(3, 2);
                    rep.BH4.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(5, 2);
                    rep.BH5.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(7, 3);
                    rep.BH6.Value = (qbn.First().SThe.Trim() == "" || qbn.First().SThe == null) ? "" : qbn.First().SThe.Substring(10, 5);
                    rep.HanBHTu.Value = String.Format("{0:dd/MM/yyyy}", qbn.First().HanBHTu);
                    rep.HanBHDen.Value = String.Format("{0:dd/MM/yyyy}", qbn.First().HanBHDen);
                    rep.PKBanDau.Value = qbn.First().PKCu + " " + qbn.First().DChiPK;
                    rep.KhoaCDen.Value = qbn.Where(p => p.PKMoi != "").First().PKMoi;
                    rep.ChanDoan.Value = qbn.First().ChanDoan;
                    rep.ChanDoanPK.Value = "- " + qbn.First().ChanDoanPK;
                    //   rep.ChanDoanCK.Value = "- " + qbn.Where(p => p.ChanDoanCK != "").First().ChanDoanCK;
                    rep.BSKB.Value = qbn.Where(p => p.BSKB != "").First().BSKB;
                    // rep.BSCK.Value = qbn.Where(p => p.BSCK != "").First().BSCK;

                    frmIn frm = new frmIn();
                    rep.lblNgayKhamPK.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    rep.lblNgayKhamCK.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
                    //rep.DataSource = qcls.ToList();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Bệnh nhân: " + qbn.First().TenBNhan + " không chuyển phòng khám.", "THÔNG BÁO", MessageBoxButtons.OK);
            }

        }

        private void barGiayChuyenPK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            InPhieuKhamCK(_int_maBN);
        }

        private void grvBNhankb_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colSoDK")
            {
                int rs;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                int sodk = 0;
                if (grvBNhankb.GetFocusedRowCellValue(colSoDK) != null && grvBNhankb.GetFocusedRowCellValue(colSoDK).ToString() != "" && grvBNhankb.GetFocusedRowCellValue(colSoDK).ToString() != "")
                    sodk = Convert.ToInt32(grvBNhankb.GetFocusedRowCellValue(colSoDK));
                getSoKB.ComPort(sodk);
                getSoKB.UpdateGoiSoDK(sodk, _int_maBN);
                LoadData();
            }
        }

        private void txtBenhKhac2_Leave(object sender, EventArgs e)
        {
            string ICD = lICD.Where(p => p.TenICD == txtBenhKhac2.Text).Select(p => p.MaICD).FirstOrDefault();
            if (!string.IsNullOrEmpty(ICD))
                LupICD2.Text = ICD;
        }

        private void txtBenhKhac3_Leave(object sender, EventArgs e)
        {
            string ICD = lICD.Where(p => p.TenICD == txtBenhKhac3.Text).Select(p => p.MaICD).FirstOrDefault();
            if (!string.IsNullOrEmpty(ICD))
                LupICD3.Text = ICD;

        }

        private void txtBenhKhac4_Leave(object sender, EventArgs e)
        {
            //string ICD = lICD.Where(p => p.TenICD == txtBenhKhac4.Text).Select(p => p.MaICD).FirstOrDefault();
            //if (!string.IsNullOrEmpty(ICD) && string.IsNullOrEmpty(LupICD4.Text))
            //    LupICD4.Text = ICD;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtIdkb.Text))
            {
                int id = Convert.ToInt32(txtIdkb.Text);
                BNKB idkb = _dataContext.BNKBs.Where(p => p.IDKB == id).FirstOrDefault();
                if (idkb != null)
                {
                    //frm_BC_BNKB frm = new frm_BC_BNKB(id);
                    //frm.ShowDialog();
                }
            }
        }
        void getSaoDon(List<DThuocct> _ldthuoc)
        {
            var bnkb = _dataContext.BNKBs.FirstOrDefault(o => o.IDKB == idKBSaoDon);
            if (bnkb == null)
                return;
            if (grvDonThuocct.RowCount > 1)
            {
                MessageBox.Show("Đơn đã có thuốc, bạn không thể sao đơn!");
                return;
            }
            if (grvDonThuocct.RowCount == 1)
            {
                if (grvDonThuocct.GetRowCellValue(0, colIDThuoc) != null && grvDonThuocct.GetRowCellValue(0, colIDThuoc).ToString() != "")
                {
                    MessageBox.Show("Đơn đã có thuốc, bạn không thể sao đơn!");
                    return;
                }
            }

            if (_ldthuoc.Count > 0)
            {
                var now = DateTime.Now;
                int _int_maBN = 0;
                Int32.TryParse(txtMaBNhan.Text, out _int_maBN);
                var dt = _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN && p.PLDV == 1 && p.KieuDon == -1).ToList();
                foreach (var item in dt)
                {
                    _dataContext.DThuocs.Remove(item);
                    _dataContext.SaveChanges();
                }

                if (now <= bnkb.NgayKham)
                {
                    MessageBox.Show("Ngày kê không được nhỏ hơn ngày khám!");
                    return;
                }

                DThuoc dthuoc = new DThuoc();
                dthuoc.MaBNhan = _int_maBN;
                dthuoc.NgayKe = bnkb.NgayKham;
                dthuoc.MaKP = bnkb.MaKP;
                dthuoc.MaCB = bnkb.MaCB;
                dthuoc.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                dthuoc.PLDV = 1;

                dthuoc.KieuDon = -1;

                dthuoc.LoaiDuoc = -1;
                _dataContext.DThuocs.Add(dthuoc);
                //Tạo trên đơn thuốc chi tiết (donthuocct)
                if (_dataContext.SaveChanges() >= 0)
                {
                    int maxid = dthuoc.IDDon;
                    int _madv = 0;
                    int _makho = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                    double _dongia = 0;
                    double _soluong = 0;
                    string _ylenh = "";
                    double sothang = 1;
                    foreach (var a in _ldthuoc)
                    {
                        _madv = a.MaDV == null ? 0 : a.MaDV.Value;
                        _dongia = a.DonGia;
                        _soluong = a.SoLuongct * sothang;

                        double soluongton = 0;
                        string solo = "";
                        if (a.SoLo != null)
                            solo = a.SoLo;
                        soluongton = DungChung.Ham._checkTon_KD(_dataContext, _madv, _makho, _dongia, 0, solo);
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            DateTime? handung = new DateTime();
                            if (a.HanDung != null)
                                handung = a.HanDung;
                            soluongton = DungChung.Ham._checkTon_KD1(_dataContext, _madv, _makho, _dongia, 0, solo, handung);
                        }
                        if (soluongton >= _soluong)
                        {
                            DThuocct _newdtct = new DThuocct();
                            _newdtct.MaKP = lupKhoaKhamkb.EditValue == null ? 0 : Convert.ToInt32(lupKhoaKhamkb.EditValue);
                            _newdtct.NgayNhap = now;
                            _newdtct.IDDon = maxid;
                            _newdtct.TrongBH = a.TrongBH;
                            _newdtct.MaDV = a.MaDV;
                            _newdtct.SoLuong = a.SoLuong;
                            _newdtct.Loai = (byte)sothang;
                            _newdtct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                            _newdtct.MaCB = lupNguoiKhamkb.EditValue == null ? "" : lupNguoiKhamkb.EditValue.ToString();

                            _newdtct.DuongD = a.DuongD;
                            _newdtct.DviUong = a.DviUong;
                            _newdtct.Luong = a.Luong;
                            _newdtct.MoiLan = a.MoiLan;
                            _newdtct.SoLan = a.SoLan;
                            _newdtct.SoPL = -1;
                            _newdtct.GhiChu = a.GhiChu;
                            _newdtct.SoLuongct = a.SoLuongct;
                            _newdtct.DonVi = a.DonVi;
                            _newdtct.DonGia = a.DonGia;
                            _newdtct.ThanhTien = Math.Round(a.DonGia * a.SoLuong, 4);
                            _newdtct.TyLeTT = a.TyLeTT;
                            _newdtct.SoLo = a.SoLo;
                            _newdtct.MaCC = a.MaCC;
                            _newdtct.IDKB = idKBSaoDon;
                            if (a.Status == 1 || a.Status == 2)
                                _newdtct.Status = 0;
                            else
                                _newdtct.Status = a.Status;
                            _dataContext.DThuoccts.Add(_newdtct);

                            try
                            {
                                _dataContext.SaveChanges();
                            }
                            catch (System.Data.UpdateException ex)
                            {
                                MessageBox.Show(ex.StateEntries.FirstOrDefault().ToString());
                                MessageBox.Show(ex.StateEntries.FirstOrDefault().State.ToString());
                            }
                        }
                        else
                        {
                            //try
                            //{
                            //    if (connect.isConnect)
                            //    {
                            //        string strSQL = "SELECT TenDV FROM dbo.DichVu WHERE MaDV = '" + a.MaDV + "'";

                            //        DataTable dtTble = connect.FillDatatable(strSQL, CommandType.Text);
                            //        if (dtTble.Rows.Count > 0)
                            //        {
                            //            MessageBox.Show("Thuốc: " + dtTble.Rows[0]["TenDV"].ToString() + " trong kho không đủ");
                            //        }
                            //    }
                            //    else
                            //    {
                            //        MessageBox.Show("Lỗi kết nối CSDL");
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    MessageBox.Show(ex.Message);
                            //}
                            var tendv = _lDichvu.Where(p => p.MaDV == (a.MaDV)).ToList();
                            if (tendv.Count > 0)
                            {
                                MessageBox.Show("Thuốc: " + tendv.First().TenDV + " trong kho không đủ");
                            }
                        }

                    }
                    // lưu diễn biến

                    //

                }
            }//

        }

        int idKBSaoDon;
        private void barSaoDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int rs;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                if (!string.IsNullOrEmpty(txtIdkb.Text))
                {
                    int id = Convert.ToInt32(txtIdkb.Text);
                    var idkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN).OrderByDescending(p => p.IDKB).ToList();
                    if (idkb.Count > 0)
                    {
                        if (id >= idkb.First().IDKB)
                        {
                            var tt = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN).ToList();
                            if (tt.Count > 0)
                            {
                                MessageBox.Show("Bệnh nhân đã ra viện!");
                                return;
                            }
                            if (!KTraKB())
                                return;
                            int makho = 0;
                            if (lupKhoXuat.EditValue != null)
                                makho = Convert.ToInt32(lupKhoXuat.EditValue);
                            idKBSaoDon = id;
                            FormNhap.frm_SaoDonNgTru frm = new FormNhap.frm_SaoDonNgTru(txtSoThe.Text, _int_maBN, makho);
                            frm.getdata = new FormNhap.frm_SaoDonNgTru.getlist(getSaoDon);
                            frm.ShowDialog();
                            grvBNhankb_FocusedRowChanged(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Chỉ được phép sao đơn ở khoa khám bệnh cuối cùng!");
                        }
                    }
                }
            }
            finally
            {
                idKBSaoDon = 0;
            }
        }

        private void chkUuTien_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemcheck();
        }

        private void chkChuaKham_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemcheck();
        }

        private void chkChiDinhCLS_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemcheck();
        }

        private void chkKetQuaCLS_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemcheck();
        }

        private void grvBNhankb_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            grvBNhankb_FocusedRowChanged(null, null);
            xtraNgoaiTru.Enabled = true;
        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
        /// <summary>
        /// phiếu phát thuốc _27021
        /// </summary>
        /// <param name="mabn"></param>
        public void InPhieu(int mabn)
        {
            var dstenbn = (from ten in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.KieuDon == -1)
                           join bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn) on ten.MaBNhan equals bn.MaBNhan
                           join kb in _dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                           select new
                           {
                               bn.TenBNhan,
                               bn.Tuoi,
                               bn.GTinh,
                               bn.DChi,
                               bn.DTuong,
                               SThe = bn.SThe == null ? "" : bn.SThe,
                               bn.MaBNhan,
                               IDDon = ten.IDDon == null ? 0 : ten.IDDon,
                               ChanDoan = kb.ChanDoan == null ? "" : kb.ChanDoan,
                               BenhKhac = kb.BenhKhac == null ? "" : kb.BenhKhac
                           }).FirstOrDefault();
            var lkp = (from ten in _dataContext.DThuocs
                       join bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == mabn) on ten.MaBNhan equals bn.MaBNhan
                       join kp in _dataContext.KPhongs on ten.MaKXuat equals kp.MaKP
                       select new
                       {
                           kp.MaKP,
                           TenKP = kp.TenKP == null ? "" : kp.TenKP
                       }).FirstOrDefault();
            var dsthuoc = (from
                           dt in _dataContext.DThuocs.Where(p => p.PLDV == 1 && p.MaBNhan == mabn && p.KieuDon == -1)
                           join
                            dtct in _dataContext.DThuoccts.Where(p => p.Status == 0) on dt.IDDon equals dtct.IDDon
                           join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                           select new
                           {
                               TenDV = dv.TenDV + "/" + dv.HamLuong,
                               dtct.MaDV,
                               dtct.DonVi,
                               dtct.SoLuong,
                               dtct.DonGia,
                               dtct.ThanhTien,
                               dv.NuocSX,
                               dtct.GhiChu,
                               dt.NgayKe
                           }).ToList();
            frmIn frm = new frmIn();
            BaoCao.Rep_PhieuPT rep = new BaoCao.Rep_PhieuPT();
            rep.lab_tencq.Text = DungChung.Bien.TenCQ.ToUpper();
            rep.lab_CQchuquan.Text = DungChung.Bien.TenCQCQ.ToUpper();
            string benhkhac = "", benhkhacmoi = "";
            benhkhac = dstenbn.BenhKhac.ToString();
            List<string> lisbk = benhkhac.Split(';').ToList();
            foreach (string a in lisbk)
                if (a.Trim() != "")
                {
                    benhkhacmoi = benhkhacmoi + a + ";";
                }
            if (dstenbn != null)
            {
                rep.lab_ten.Text = "Họ và tên BN: " + dstenbn.TenBNhan.ToString() + "        Tuổi: " + dstenbn.Tuoi.ToString() + "          Giới tính: " + (dstenbn.GTinh == 0 ? "Nữ" : "Nam");
                rep.lab_diachi.Text = "Địa chỉ: " + dstenbn.DChi.ToString();
                rep.lab_doituong.Text = "Đối tượng: " + dstenbn.DTuong.ToString() + "        Số thẻ BHYT: " + dstenbn.SThe.ToString();
                rep.lab_chandoan.Text = "Chẩn đoán: " + dstenbn.ChanDoan;
                rep.lab_benhkhac.Text = "Bệnh kèm theo: " + benhkhacmoi.ToString();
                rep.lab_sohoso.Text = "Số hồ sơ: " + dstenbn.MaBNhan;
                rep.lab_sophieu.Text = "Số phiếu: " + dstenbn.IDDon;
            }
            if (lkp != null)
                rep.lab_kho.Text = "Kho: " + lkp.TenKP.ToString();
            else
                rep.lab_kho.Text = "Kho: ";
            if (DungChung.Bien.MaBV == "30372")
            {
                rep.xrTableCell6.Text = dstenbn.TenBNhan.ToUpper().ToString();
            }
            if (dsthuoc.Count > 0)
            {
                DateTime date = dsthuoc.First().NgayKe.Value;
                rep.ngayke.Value = date.Hour + " " + "giờ" + " " + date.Minute + "," + " " + "ngày" + " " + date.Day + " " + "tháng" + " " + date.Month + " " + "năm" + " " + date.Year;
            }
            else
            {
                rep.ngayke.Value = DateTime.Now.Hour + " " + "giờ" + " " + DateTime.Now.Minute + "," + " " + "ngày" + " " + DateTime.Now.Day + " " + "tháng" + " " + DateTime.Now.Month + " " + "năm" + " " + DateTime.Now.Year; //dt.Count > 0 ? dt.First().NgayTT : DateTime.Now;
            }
            rep.DataSource = dsthuoc;
            rep.databinding();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _int_maBN = 0;
            Int32.TryParse(txtMaBNhan.Text, out _int_maBN);
            if (_int_maBN > 0)
            {
                if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24297")
                {
                    frm_kedon frm = new frm_kedon(_int_maBN, -1, DungChung.Bien.MaKP, true);
                    frm.ShowDialog();

                }
                else
                {
                    frm_kedon frm = new frm_kedon(_int_maBN, -1, DungChung.Bien.MaKP, false);
                    frm.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
            }

        }

        private void btnPhieuPhatThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _int_maBN = 0;
            Int32.TryParse(txtMaBNhan.Text, out _int_maBN);

            FormNhap.usKhamBenh frNhap = new FormNhap.usKhamBenh();
            frNhap.InPhieu(_int_maBN);
        }

        private void btnBNTuVongNgoaiVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _int_maBN = 0;
            Int32.TryParse(txtMaBNhan.Text, out _int_maBN);
            frm_BBKTTuVong frNhap = new frm_BBKTTuVong(_int_maBN);
            frNhap.ShowDialog();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _int_maBN = 0;
            Int32.TryParse(txtMaBNhan.Text, out _int_maBN);
            DonThuoc_NhaThuoc_27022(_int_maBN);
        }
        public void DonThuoc_NhaThuoc_27022(int MaBNhan)
        {
            BaoCao.Rep_DonThuocTPCN_NhaThuoc_27022 rep = new BaoCao.Rep_DonThuocTPCN_NhaThuoc_27022();
            frmIn frm = new frmIn();
            var dv = (from a in _dataContext.DichVus
                      join b in _dataContext.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThucPhamChucNang) on a.IdTieuNhom equals b.IdTieuNhom
                      select new
                      {
                          a.MaDV,
                          a.TenDV,
                          b.TenRG,
                          a.DonVi
                      }).ToList();
            var dthuoc1 = (from a in _dataContext.DThuocs.Where(p => p.MaBNhan == MaBNhan)
                           join b in _dataContext.DThuoccts on a.IDDon equals b.IDDon
                           join d in _dataContext.CanBoes on a.MaCB equals d.MaCB
                           select new
                           {
                               a.MaBNhan,
                               MaThuoc = b.MaDV,
                               b.DonGia,
                               b.SoLuong,
                               CachDung = (b.DuongD ?? "") + " " + (b.SoLan ?? "") + " " + (b.MoiLan ?? "") + " " + (b.Luong ?? "") + " " + (b.DviUong ?? ""),
                               a.NgayKe,
                               d.TenCB,
                               b.MaDV,
                               a.GhiChu

                           }).OrderBy(p => p.NgayKe).ToList();
            var dthuoc = (from a in dthuoc1
                          join b in dv on a.MaDV equals b.MaDV
                          select new
                          {
                              a.MaBNhan,
                              MaThuoc = b.MaDV,
                              b.TenDV,
                              b.DonVi,
                              a.DonGia,
                              a.SoLuong,
                              a.CachDung,
                              a.NgayKe,
                              a.TenCB,
                              a.GhiChu
                          }).OrderBy(p => p.NgayKe).ToList();
            if (dthuoc.Count > 0)
            {
                int _mabn = dthuoc.Max(p => p.MaBNhan ?? 0);
                var ghichu = _dataContext.DThuocs.Where(p => p.MaBNhan == _mabn).ToList();
                int maxx = ghichu.Max(q => q.IDDon);
                ghichu = ghichu.Where(p => p.IDDon == maxx).ToList();
                string _ghichu = ghichu.First().GhiChu ?? "";

                if (ghichu.Count() >= 1)
                    rep.LoiDan.Value = "Lời dặn: " + _ghichu;
                else rep.LoiDan.Value = "Lời dặn: ";
                var bn = (from a in _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn)
                          join b in _dataContext.BNKBs on a.MaBNhan equals b.MaBNhan
                          join c in _dataContext.TTboXungs on a.MaBNhan equals c.MaBNhan
                          select new { a.GTinh, a.TenBNhan, a.NamSinh, a.NgaySinh, a.ThangSinh, a.NNhap, b.MaICD, b.ChanDoan, b.BenhKhac, b.IDKB, a.SThe, a.DChi, b.GhiChu, c.NThan }).OrderByDescending(p => p.IDKB).ToList();

                if (bn.Count > 0)
                {
                    int idkb = bn.Max(p => p.IDKB);
                    bn = bn.Where(p => p.IDKB == idkb).ToList();
                    rep.TenBN.Value = bn.First().TenBNhan;
                    rep.NamSinh.Value = bn.First().NamSinh;
                    if (bn.First().GTinh == 0)
                        rep.Gtinh.Value = "Nữ";
                    else rep.Gtinh.Value = "Nam";
                    rep.DChi.Value = bn.First().DChi;
                    if (bn.First().ChanDoan != null || bn.First().BenhKhac != null)
                        rep.ChanDoan.Value = "Chẩn đoán: " + bn.First().ChanDoan + "; " + bn.First().BenhKhac;
                    rep.SoThe.Value = bn.First().SThe;
                    rep.TenNguoiThan.Value = bn.First().NThan;
                }
                else rep.LoiDan.Value = "Lời dặn: ";
                DateTime ngay = dthuoc.Min(p => p.NgayKe.Value);
                rep.NgayKe.Value = "Ngày " + ngay.Day + " tháng " + ngay.Month + " năm " + ngay.Year;
                rep.BSKham.Value = dthuoc.First().TenCB;
            }
            else
            {
                rep.LoiDan.Value = "Lời dặn: ";
                rep.NgayKe.Value = "Ngày ........ tháng ........ năm ........";
            }
            rep.DataSource = dthuoc;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnInDon_01071_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            InDon01071(_int_maBN);

        }

        private void txtMaBNhan_TextChanged(object sender, EventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (_int_maBN > 0)
            {
                var qdt = (from dt in _dataContext.DThuocs.Where(p => p.MaBNhan == _int_maBN)
                           join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                           select dtct.ThanhTien).ToList();
                if (qdt.Count > 0)
                    txtTongTien.Text = qdt.Sum(p => p).ToString("#,#");
                else
                    txtTongTien.Text = "0";
            }
        }

        private void grcChiDinh_Click(object sender, EventArgs e)
        {

        }


        private void grvDonThuocct_ShownEditor(object sender, EventArgs e)
        {

        }

        private void barDienBienBenh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            frmDienBienBenh frm = new frmDienBienBenh(_int_maBN);
            frm.ShowDialog();
        }

        private void lupKhoaKhamkb_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (lupKhoaKhamkb.EditValue != null)
                _maKhoaKB = Convert.ToInt32(lupKhoaKhamkb.EditValue);
        }

        private void cboMaICDBĐ_EditValueChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(cboMaICDBĐ.Text))
            //{
            //    if (cboMaICDBĐ.EditValue.ToString() == null)
            //    {
            //        txtBenhBĐ.EditValue = "";
            //        cboMaICDBĐ.EditValue = "";
            //    }
            //    else
            //        //lupChanDoanKb.EditValue =lupMaICDkb.EditValue.ToString();
            //        txtBenhBĐ.EditValue = lICD.Where(p => p.MaICD == cboMaICDBĐ.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            //}
            //xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            //mnLuu.Enabled = true;

        }

        private void cboMaICDBĐ_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyCode == Keys.F9)
            //{
            //    FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
            //    frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICDBD);
            //    frm.ShowDialog();

            //}
        }

        private void txtBenhBĐ_Layout(object sender, LayoutEventArgs e)
        {

        }


        private void txtBenhBĐ_EditValueChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtBenhBĐ.Text))
            //{
            //    if (txtBenhBĐ.EditValue.ToString() == "0")
            //    {
            //        txtBenhBĐ.EditValue = "";
            //        cboMaICDBĐ.EditValue = "";
            //    }
            //    else
            //        cboMaICDBĐ.EditValue = lICD.Where(p => p.TenICD == txtBenhBĐ.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();
            //    //LupICD2.EditValue = txtBenhKhac2.EditValue.ToString();

            //}
            //xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            //mnLuu.Enabled = true;
        }

        private void txtBenhBĐ_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(cboMaICDBĐ.Text))
            //    cboMaICDBĐ.Text = lICD.Where(p => p.TenICD == txtBenhBĐ.Text).Select(p => p.MaICD).FirstOrDefault();

        }

        private void txtBenhKhac2_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void txtBenhKhac3_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void txtBenhKhac4_EditValueChanged_1(object sender, EventArgs e)
        {
            if (txtBenhKhac4.Text.Trim().Split(';').Count() > 1)
                isChonNhieuBenhKhac = true;
            else
            {
                isChonNhieuBenhKhac = false;
            }
            if (!string.IsNullOrEmpty(txtBenhKhac4.Text) && !isChonNhieuBenhKhac)
            {
                if (lICD.FirstOrDefault(p => p.TenICD == txtBenhKhac4.EditValue.ToString()) != null)
                    LupICD4.EditValue = lICD.FirstOrDefault(p => p.TenICD == txtBenhKhac4.EditValue.ToString()).MaICD;
            }

            txtBenhPhu.Text = txtBenhKhac4.Text;
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;

            //if (!string.IsNullOrEmpty(txtBenhKhac4.Text))
            //{
            //    if (txtBenhKhac4.EditValue.ToString() == "0")
            //    {
            //        txtBenhKhac4.EditValue = "";
            //        LupICD4.EditValue = "";
            //        //txtBenhKhac4.Text = "";
            //    }
            //    else
            //    {
            //        LupICD4.EditValue = lICD.Where(p => p.TenICD == txtBenhKhac4.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();
            //        txtBenhPhu.Text = lICD.Where(p => p.TenICD == txtBenhKhac4.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
            //    }
            //}
            //else
            //{
            //    txtBenhKhac4.Text = "";
            //    txtBenhPhu.Text = "";
            //}
            //else
            //{
            //    LupICD4.EditValue = "";
            //}
        }

        private void btnPhieuLinhMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            int idkb = 0;
            if (!string.IsNullOrEmpty(txtIdkb.Text))
                idkb = Convert.ToInt32(txtIdkb.Text);
            FormNhap.frm_LinhMau frm = new FormNhap.frm_LinhMau(_int_maBN, idkb);
            frm.ShowDialog();
        }

        private void cboMaICDBĐ_Leave(object sender, EventArgs e)
        {

        }

        private void grvchiDinh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //check đã thu thẳng rồi thì không cho sửa trong ngoài DM
            int iddonct = 0;
            if (grvchiDinh.GetFocusedRowCellValue(colIDctcd) != null && grvchiDinh.GetFocusedRowCellValue(colIDctcd).ToString() != "" && Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colIDctcd)) > 0)
            {
                iddonct = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colIDctcd));
            }

            var qdt = _dataContext.DThuoccts.Where(p => p.IDDonct == iddonct).Where(p => p.ThanhToan == 1).FirstOrDefault();

            if (qdt != null)
                colTrongBHdv.OptionsColumn.ReadOnly = true;
            else
            {
                colTrongBHdv.OptionsColumn.ReadOnly = false;
            }

        }

        private void grvchiDinh_DataSourceChanged(object sender, EventArgs e)
        {
            grvchiDinh_FocusedRowChanged(null, null);
        }
        private void txtBenhKhac4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD4);
                frm.ShowDialog();

            }
        }
        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void lupChanDoanKb_EditValueChanged_2(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lupChanDoanKb.Text))
            {
                if (lupChanDoanKb.EditValue == null)
                {
                    lupChanDoanKb.EditValue = "";
                    lupMaICDkb.EditValue = "";
                    txtBenhChinh.Text = "";
                }
                else
                {
                    //lupMaICDkb.EditValue = lupChanDoanKb.EditValue.ToString();
                    txtBenhChinh.Text = lICD.Where(p => p.TenICD == lupChanDoanKb.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault() ?? "";
                    lupMaICDkb.EditValue = lICD.Where(p => p.TenICD == lupChanDoanKb.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault() ?? "";
                }
            }
            else
            {
                lupMaICDkb.EditValue = "";
                txtBenhChinh.Text = "";
            }

            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        private void txtBenhKhac2_EditValueChanged_2(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBenhKhac2.Text))
            {
                if (txtBenhKhac2.EditValue.ToString() == "0")
                {
                    txtBenhKhac2.EditValue = "";
                    LupICD2.EditValue = "";
                }
                else
                {
                    LupICD2.EditValue = lICD.Where(p => p.TenICD == txtBenhKhac2.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();
                    txtBenhPhu2.Text = lICD.Where(p => p.TenICD == txtBenhKhac2.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();
                    //LupICD2.EditValue = txtBenhKhac2.EditValue.ToString();
                }
            }
            else
            {
                LupICD2.EditValue = "";
            }
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        private void lupChanDoanKb_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD);
                frm.ShowDialog();

            }
        }

        private void txtBenhKhac2_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD2);
                frm.ShowDialog();

            }
        }

        private void txtBenhKhac3_EditValueChanged_2(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBenhKhac3.Text))
            {
                if (txtBenhKhac3.EditValue.ToString() == "0")
                {
                    txtBenhKhac3.EditValue = "";
                    LupICD3.EditValue = "";
                }
                else
                {
                    //LupICD3.EditValue = txtBenhKhac3.EditValue.ToString();
                    LupICD3.EditValue = lICD.Where(p => p.TenICD == txtBenhKhac3.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();
                    txtBenhPhu3.Text = lICD.Where(p => p.TenICD == txtBenhKhac3.EditValue.ToString()).Select(p => p.TenICD).FirstOrDefault();

                }

            }
            else
            {
                LupICD2.EditValue = "";
            }
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        private void txtBenhKhac3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICD3);
                frm.ShowDialog();

            }
        }

        private void barTTcoTheBN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            frm_TTKhamBenh frm = new frm_TTKhamBenh(_int_maBN);
            frm.ShowDialog();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_TraCuuHDSDThuoc frm = new frm_TraCuuHDSDThuoc();
            frm.ShowDialog();
        }

        private void barTraCuuTonDuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChucNang.frm_dsTonDuoc frm = new ChucNang.frm_dsTonDuoc();
            frm.ShowDialog();
        }

        private void LupICD4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bbTTNTHAN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            string tuoitt = "";
            tuoitt = DungChung.Ham.TuoitheoThang(_dataContext, _int_maBN, "72-00");
            if (tuoitt.Length > 3)
            {
                frm_TTNgThanTreEm frm = new frm_TTNgThanTreEm(_int_maBN);
                frm.Show();
            }
            else
            {
                MessageBox.Show("Chỉ áp dụng cho bệnh nhân dưới 72 tháng tuổi");
            }
        }

        private void barXNBNMT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);

            frm_XnBNManTinh frm = new frm_XnBNManTinh(_int_maBN);
            frm.ShowDialog();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool XN = true;
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            var TTBN = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            if (TTBN != null)
            {
                if (TTBN.DTuong == "BHYT")
                {
                    string SThe = TTBN.SThe;
                    var ktra1 = _dataContext.BNManTinhs.Where(p => p.STheSoCMT == SThe).ToList();
                    if (ktra1.Count > 0)
                        XN = false;
                }
                else
                {
                    var SoCMt = _dataContext.TTboXungs.Where(p => p.MaBNhan == _int_maBN).Select(p => p.CMT).FirstOrDefault();
                    if (SoCMt != null)
                    {
                        string SoCMT = SoCMt;
                        var ktra2 = _dataContext.BNManTinhs.Where(p => p.STheSoCMT == SoCMT).ToList();
                        if (ktra2.Count > 0)
                            XN = false;
                    }
                }
            }
            if (XN)
            {
                frm_XnBNManTinh frm = new frm_XnBNManTinh(_int_maBN);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bệnh nhân này đã có bệnh án mãn tính");
                frm_XnBNManTinh frm = new frm_XnBNManTinh(_int_maBN);
                frm.ShowDialog();
            }
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool XN = true;
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
            {
                if (txtIdkb.Text != null && txtIdkb.Text != "")
                {
                    int id = Convert.ToInt32(txtIdkb.Text);
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                    QLBV.FormThamSo.frm_KeDonTV frm = new QLBV.FormThamSo.frm_KeDonTV(_int_maBN, id);
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Bệnh nhân chưa có khám bệnh!");
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn bệnh nhân!");
            }
        }

        private void mmChanDoanBD_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        private void grvDonThuocct_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if (_errMessage != "")
            {
                e.ExceptionMode = ExceptionMode.NoAction;
                DialogResult _result = MessageBox.Show(_errMessage, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    grvDonThuocct.FocusedColumn = grvDonThuocct.VisibleColumns[14];
                }
                else
                {
                    grvDonThuocct.DeleteRow(grvDonThuocct.FocusedRowHandle);
                }
            }
        }

        private void grvchiDinh_ShownEditor(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "27183")
            {
                int iddtct = 0;
                if (grvchiDinh.GetFocusedRowCellValue(colIDDonct) != null && grvchiDinh.GetFocusedRowCellValue(colIDDonct).ToString() != "")
                {
                    iddtct = Convert.ToInt32(grvchiDinh.GetFocusedRowCellValue(colIDDonct));

                }
                if (iddtct > 0)
                    lupMaDVcd.DataSource = _lDVhienthi;
                else
                    lupMaDVcd.DataSource = _list27183;
            }
        }

        List<int> slKhongDu_MaDV = new List<int>();
        bool isDTM = false;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    isDTM = true;
            //    if (!string.IsNullOrEmpty(txtMaBNhan.Text))
            //    {
            //        int mabn = 0;
            //        mabn = Convert.ToInt32(txtMaBNhan.Text);
            //        laydon1 = 0;
            //        int makho = 0;
            //        if (lupKhoXuat.Text != "")
            //        {
            //            makho = Convert.ToInt32(lupKhoXuat.EditValue);
            //            frm_KeDonThuocMau frm = new frm_KeDonThuocMau(2, makho);
            //            frm.ShowDialog();
            //            if (laydon1 == 1)
            //            {
            //                var listmau = (from a in _dataContext.DThuocMaus.Where(p => p.IDDonMau == iddthuocmau1)
            //                               join b in _dataContext.DThuocMaucts on a.IDDonMau equals b.IDDonMau
            //                               select new { MaDV = b.MaDV, b.DonVi, b.DviUong, b.SoLan, SoLuongct = b.SoLuong ?? 0, b.DuongD, b.GhiChu, b.MoiLan, b.Luong }).ToList();
            //                _ldtct1.Clear();
            //                foreach (var item in listmau)
            //                {
            //                    DThuocct moi = new DThuocct();
            //                    moi.MaDV = item.MaDV;
            //                    moi.DviUong = item.DviUong;
            //                    moi.SoLan = item.SoLan;
            //                    moi.SoLuongct = item.SoLuongct;
            //                    moi.DuongD = item.DuongD;
            //                    moi.GhiChu = item.GhiChu;
            //                    moi.MoiLan = item.MoiLan;
            //                    moi.Luong = item.Luong;
            //                    _ldtct1.Add(moi);
            //                }
            //                string x2 = "";
            //                int dem = 0;
            //                string message = "";
            //                foreach (var item in _ldtct1)
            //                {
            //                    var dsgia = Ham._getDSGia(_dataContext, item.MaDV ?? 0, makho);
            //                    var tonthuoc = DungChung.Bien.SoLuongTon;
            //                    if (ppxuat == 3)
            //                    {
            //                        var soluongt = DungChung.Ham._checkTon_KD(_dataContext, item.MaDV ?? 0, makho, item.DonGia, 0, _solo);
            //                        if (DungChung.Bien.MaBV == "24012")
            //                        {
            //                            soluongt = DungChung.Ham._checkTon_KD1(_dataContext, item.MaDV ?? 0, makho, item.DonGia, 0, _solo, _handung);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        var soluongt = DungChung.Ham._getSL_DongY(_dataContext, item.MaDV ?? 0, 0, makho);
            //                    }
            //                    DataRow[] x1;
            //                    x1 = dtTbleThuoc.Select("MaDV = '" + item.MaDV + "'");
            //                    if (x1.Length > 0)
            //                    {
            //                        if (tonthuoc - soluongt >= item.SoLuongct * sothangSua)
            //                        {
            //                            grvDonThuocct.AddNewRow();
            //                            int rowHandle = grvDonThuocct.GetRowHandle(grvDonThuocct.DataRowCount);
            //                            if (grvDonThuocct.IsNewItemRow(rowHandle))
            //                            {
            //                                ktCellChange = true;
            //                                grvDonThuocct.SetRowCellValue(rowHandle, colMaDV, item.MaDV);
            //                                grvDonThuocct.SetRowCellValue(rowHandle, grvDonThuocct.Columns["SoLuongct"], item.SoLuongct);
            //                                grvDonThuocct.SetRowCellValue(rowHandle, colGhiChu, item.GhiChu);
            //                                grvDonThuocct.SetRowCellValue(rowHandle, colMoilan, item.MoiLan);
            //                                grvDonThuocct.SetRowCellValue(rowHandle, colSoLan, item.SoLan);
            //                                grvDonThuocct.SetRowCellValue(rowHandle, colSoLuongdung, item.Luong);

            //                            }
            //                        }
            //                        else
            //                        {
            //                            slKhongDu_MaDV.Add(item.MaDV ?? 0);
            //                        }
            //                    }
            //                    else if (x1.Length <= 0)
            //                    {
            //                        var dv = _dataContext.DichVus.FirstOrDefault(p => p.MaDV == item.MaDV);
            //                        if (dv != null)
            //                        {
            //                            string tendv1 = dv.TenDV;
            //                            x2 = x2 + " " + tendv1 + ";";
            //                            dem++;
            //                        }
            //                        else
            //                        {
            //                            message += item.MaDV + "; ";
            //                        }
            //                    }
            //                }
            //                if (dem > 0)
            //                {
            //                    MessageBox.Show("Các thuốc: " + x2 + " không tồn tại trong kho " + lupKhoXuat.Text);
            //                }
            //                if (!string.IsNullOrWhiteSpace(message))
            //                {
            //                    MessageBox.Show("Các thuốc có mã: " + message + " không tồn tại");
            //                }
            //                if (slKhongDu_MaDV.Count > 0)
            //                {
            //                    var tenDV = _dataContext.DichVus.Where(o => slKhongDu_MaDV.Contains(o.MaDV)).ToList();
            //                    if (tenDV.Count > 0)
            //                        MessageBox.Show(string.Format("Các thuốc: {0} có số lượng không đủ", string.Join("; ", tenDV.Select(o => o.TenDV))));
            //                    else
            //                    {
            //                        MessageBox.Show(string.Format("Các thuốc có mã: {0} có số lượng không đủ", string.Join("; ", slKhongDu_MaDV)));
            //                    }
            //                }

            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Bạn chưa chọn kho xuất!");
            //            lupKhoXuat.Focus();
            //        }
            //    }
            //    else
            //        MessageBox.Show("Chưa chọn bệnh nhân!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //finally
            //{
            //    isDTM = false;
            //    slKhongDu_MaDV = new List<int>();
            //}
        }

        private void grvDichVuCS2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            xtabDichVuCS2.Text = "Dịch vụ CS2*";
            mnLuu.Enabled = true;
            switch (e.Column.Name)
            {
                case "colNgayNhapdvCS2":
                    if (grvDichVuCS2.GetFocusedRowCellValue(colNgayNhapdvCS2) == null)
                    {
                        MessageBox.Show("Bạn chưa nhập ngày tháng!");
                    }
                    break;
                case "colBHTTCS2":
                    if (grvDichVuCS2.GetFocusedRowCellValue(colBHTTCS2) != null)
                    {
                        int ma = 0;
                        if (grvDichVuCS2.GetFocusedRowCellValue(colDichVuCS2) != null)
                        {
                            ma = Convert.ToInt32(grvDichVuCS2.GetFocusedRowCellValue(colDichVuCS2));
                        }
                        int trongBH = Convert.ToInt32(grvDichVuCS2.GetFocusedRowCellValue(colBHTTCS2));
                        grvDichVuCS2.SetFocusedRowCellValue(colGiaBHYTCS2, Ham._getGiaDM(_dataContext, ma, trongBH, string.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), dtNgayKhamkb.DateTime));
                        //set thành tiền
                        if (grvDichVuCS2.GetFocusedRowCellValue(colSLCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colSLCS2).ToString() != "")
                        {
                            double sl = 0;
                            sl = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colSLCS2).ToString());
                            if (sl < 0)
                            {
                                MessageBox.Show("Số lượng phải >0");
                                grvDichVuCS2.FocusedColumn = grvDichVuCS2.VisibleColumns[3];
                            }
                            else
                            {
                                double dg = 0;
                                if (grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2).ToString() != "")
                                    dg = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2).ToString());

                                if (trongBH == 1)
                                {
                                    if (grvDichVuCS2.GetFocusedRowCellValue(colBHTTCS2) != null)
                                    {
                                        double tltt = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colBHTTCS2));
                                        grvDichVuCS2.SetFocusedRowCellValue(colThanhTienCS2, sl * dg * tltt / 100);
                                    }
                                    else
                                        grvDichVuCS2.SetFocusedRowCellValue(colThanhTienCS2, sl * dg);
                                }
                                else
                                    grvDichVuCS2.SetFocusedRowCellValue(colThanhTienCS2, sl * dg);
                            }
                        }
                    }
                    break;
                case "colDichVuCS2":
                    int _trongBH = 1;

                    if (grvDichVuCS2.GetFocusedRowCellValue(colDichVuCS2) != null)
                    {
                        int ma = Convert.ToInt32(grvDichVuCS2.GetFocusedRowCellValue(colDichVuCS2));
                        if (DungChung.Bien.MaBV == "27183")
                        {
                            var nhom = _lDVhienthi.Where(p => p.MaDV == ma).FirstOrDefault();
                            if (nhom != null && nhom.IDNhom == 8)
                            {
                                MessageBox.Show("Bạn không thể thực hiện trực tiếp dịch vụ nhóm phẫu thuật thủ thuật");
                                grvDichVuCS2.SetFocusedRowCellValue(colDichVuCS2, -1);
                                return;
                            }
                        }
                        int dvtrung = 0;
                        for (int i = 0; i < grvDichVuCS2.DataRowCount; i++)
                        {
                            int madv = -10;
                            if (grvDichVuCS2.GetRowCellValue(i, colDichVuCS2) != null && grvDichVuCS2.GetRowCellValue(i, colDichVuCS2).ToString() != "")
                                madv = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(i, colDichVuCS2));
                            if (ma == madv)
                            {
                                if (DungChung.Bien.MaBV != "30003" && i != grvDichVuCS2.FocusedRowHandle)
                                {
                                    dvtrung++;

                                }
                            }
                        }
                        if (dvtrung > 0)
                        {
                            DialogResult _result = MessageBox.Show(grvDichVuCS2.GetFocusedRowCellDisplayText(colDichVuCS2) + " đã được nhập " + dvtrung + " lần, bạn vẫn muốn thêm?", "thêm dịch vụ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                            {
                                grvDichVuCS2.DeleteSelectedRows();
                                return;
                            }
                        }
                        double tyleBHTT = 100;
                        var trongBH = _lDichvu.Where(p => p.MaDV == ma).ToList();
                        string maQD = "";
                        if (trongBH.Count > 0)
                        {
                            tyleBHTT = trongBH.First().BHTT ?? 100;
                            _trongBH = trongBH.First().TrongDM.Value;
                            maQD = trongBH.First().MaQD;
                        }
                        int iddtbn = -10;
                        if (grvBNhankb.GetFocusedRowCellValue(colIDDTBN) != null)
                            iddtbn = Convert.ToInt32(grvBNhankb.GetFocusedRowCellValue(colIDDTBN));
                        if (DungChung.Bien._idDTBHYT != iddtbn && _trongBH == 1)
                            _trongBH = 0;
                        grvDichVuCS2.SetFocusedRowCellValue(colBHTTCS2, _trongBH);
                        if (DungChung.Bien.MaBV == "27001")
                            grvDichVuCS2.SetFocusedRowCellValue(colSLCS2, 1);
                        else
                            grvDichVuCS2.SetFocusedRowCellValue(colSLCS2, 0);
                        grvDichVuCS2.SetFocusedRowCellValue(colGiaBHYTCS2, Ham._getGiaDM(_dataContext, ma, _trongBH, string.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text), dtNgayKhamkb.DateTime));
                        grvDichVuCS2.SetFocusedRowCellValue(colDonViCS2, Ham._getDonVi(_dataContext, ma));
                        grvDichVuCS2.SetFocusedRowCellValue(colTLTTdvCS2, tyleBHTT);
                        grvDichVuCS2.SetFocusedRowCellValue(colMaQD1, maQD);
                        if (grvDichVuCS2.GetFocusedRowCellValue(colSLCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colSLCS2).ToString() != "")
                        {
                            double sl = 0;
                            sl = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colSLCS2).ToString());
                            if (sl < 0)
                            {
                                MessageBox.Show("Số lượng phải >0");
                                grvDichVuCS2.FocusedColumn = grvDichVuCS2.VisibleColumns[3];
                            }
                            else
                            {
                                double dg = 0;
                                if (grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2).ToString() != "")
                                    dg = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2).ToString());
                                grvDichVuCS2.SetFocusedRowCellValue(colThanhTienCS2, sl * dg);
                            }
                        }
                    }
                    break;
                case "colSLCS2":
                    if (grvDichVuCS2.GetFocusedRowCellValue(colSLCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colSLCS2).ToString() != "")
                    {

                        double sl = 0, tyleBHTT = 100;
                        if (grvDichVuCS2.GetFocusedRowCellValue(colTLTTdvCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colTLTTdvCS2).ToString() != "")
                            tyleBHTT = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colTLTTdvCS2));
                        sl = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colSLCS2).ToString());
                        if (sl < 0)
                        {
                            MessageBox.Show("Số lượng phải >0");
                            grvDichVuCS2.FocusedColumn = grvDichVuCS2.VisibleColumns[3];
                        }
                        else
                        {
                            double dg = 0;
                            if (grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2).ToString() != "")
                                dg = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2).ToString());

                            //if (_ktraBV_TyLeTT)
                            //{
                            //    if (grvDichVuCS2.GetFocusedRowCellValue(colMaDVcd) != null)
                            //    {
                            //        int ma = Convert.ToInt32(grvDichVuCS2.GetFocusedRowCellValue(colMaDVcd));
                            //        var qdv = _lDichvu.Where(p => p.MaDV == ma).FirstOrDefault();
                            //        if(qdv != null && qdv.IDNhom != 13 && qdv.IDNhom != 15)
                            //            grvDichVuCS2.SetFocusedRowCellValue(colThanhTien, sl * dg );
                            //        else
                            //        {
                            //            grvDichVuCS2.SetFocusedRowCellValue(colThanhTien, sl * dg * tyleBHTT / 100);
                            //        }

                            //    }
                            //}
                            // else
                            grvDichVuCS2.SetFocusedRowCellValue(colThanhTienCS2, sl * dg * tyleBHTT / 100);
                        }
                    }
                    break;
                case "colTLTTdvCS2":
                    if (grvDichVuCS2.GetFocusedRowCellValue(colSLCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colSLCS2).ToString() != "")
                    {

                        double sl = 0, tyleBHTT = 100;
                        if (grvDichVuCS2.GetFocusedRowCellValue(colTLTTdvCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colTLTTdvCS2).ToString() != "")
                            tyleBHTT = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colTLTTdvCS2));
                        sl = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colSLCS2).ToString());
                        if (sl < 0)
                        {
                            MessageBox.Show("Số lượng phải >0");
                            grvDichVuCS2.FocusedColumn = grvDichVuCS2.VisibleColumns[3];
                        }
                        else
                        {
                            double dg = 0;
                            if (grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2).ToString() != "")
                                dg = Convert.ToDouble(grvDichVuCS2.GetFocusedRowCellValue(colGiaBHYTCS2).ToString());
                            //grvDichVuCS2.SetFocusedRowCellValue(colThanhTien, sl * dg * tyleBHTT / 100);
                            //if (_ktraBV_TyLeTT)
                            //{
                            //    if (grvDichVuCS2.GetFocusedRowCellValue(colMaDVcd) != null)
                            //    {
                            //        int ma = Convert.ToInt32(grvDichVuCS2.GetFocusedRowCellValue(colMaDVcd));
                            //        var qdv = _lDichvu.Where(p => p.MaDV == ma).FirstOrDefault();
                            //        if (qdv != null && qdv.IDNhom != 13 && qdv.IDNhom != 15)
                            //            grvDichVuCS2.SetFocusedRowCellValue(colThanhTien, sl * dg );
                            //        else
                            //        {
                            //            grvDichVuCS2.SetFocusedRowCellValue(colThanhTien, sl * dg * tyleBHTT / 100);
                            //        }

                            //    }
                            //}
                            //else
                            grvDichVuCS2.SetFocusedRowCellValue(colThanhTienCS2, sl * dg * tyleBHTT / 100);
                        }
                    }
                    break;
                case "colXHHCS2":
                    if (grvDichVuCS2.GetFocusedRowCellValue(colXHHCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colXHHCS2).ToString() == "True")
                    {
                        grvDichVuCS2.SetFocusedRowCellValue(colBHTTCS2, 0);
                    }
                    break;
                    //case "colBSTH":
                    //    string MaCB = "";
                    //    if (lupNguoiKhamkb.EditValue != null)
                    //        MaCB = Convert.ToString(lupNguoiKhamkb.EditValue);
                    //    if (MaCB != "")
                    //        lupBSTH.ValueMember = MaCB;
                    //    break;
            }
        }

        private void grvDichVuCS2_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (grvDichVuCS2.OptionsBehavior.ReadOnly == true)
            {
                MessageBox.Show("Bạn chưa nhập khám bệnh cho BN");
            }
            switch (e.Column.Name)
            {
                case "colXoaCS2":
                    bool _cothexoa = true;
                    if (grvDichVuCS2.GetFocusedRowCellValue(colDichVuCS2) != null && grvDichVuCS2.GetFocusedRowCellValue(colDichVuCS2).ToString() != "")
                    {
                        int i = 0, _IDCD = 0;
                        if (grvDichVuCS2.GetRowCellValue(e.RowHandle, colIDdoncs2) != null && grvDichVuCS2.GetRowCellValue(e.RowHandle, colIDdoncs2).ToString() != "")
                        {
                            i = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(e.RowHandle, colIDdoncs2));
                            //if (grvDichVuCS2.GetRowCellValue(e.RowHandle, colIDCD) != null && !string.IsNullOrEmpty(grvDichVuCS2.GetRowCellValue(e.RowHandle, colIDCD).ToString()))
                            //    _IDCD = Convert.ToInt32(grvDichVuCS2.GetRowCellValue(e.RowHandle, colIDCD));
                            if (i > 0)
                            {
                                //var ktcd = _dataContext.ChiDinhs.Where(p => p.IDCD == _IDCD).ToList();
                                //if (_IDCD > 0 & ktcd.Count > 0)
                                //{
                                //    MessageBox.Show("Dịch vụ CLS đã được thực hiện, bạn không thể xóa");
                                //    _cothexoa = false;
                                //}
                                if (_cothexoa)
                                {
                                    if (DungChung.Ham.KTraTT(_dataContext, String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text)))
                                        _cothexoa = false;
                                }
                                if (_cothexoa)
                                {
                                    DialogResult _result = MessageBox.Show("Bạn muốn xóa dịch vụ: " + grvDichVuCS2.GetRowCellDisplayText(e.RowHandle, colDichVuCS2).ToString(), "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (_result == DialogResult.Yes)
                                    {
                                        grvDichVuCS2.DeleteRow(e.RowHandle);
                                        var xoa = _dataContext.DThuoccts.Single(p => p.IDDonct == i);
                                        _dataContext.DThuoccts.Remove(xoa);
                                        _dataContext.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                grvDichVuCS2.DeleteRow(e.RowHandle);
                            }
                        }
                        else
                        {
                            grvDichVuCS2.DeleteRow(e.RowHandle);
                        }
                    }
                    break;
            }
        }

        private void grvDichVuCS2_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            grvDichVuCS2.SetFocusedRowCellValue(colNgayNhapdvCS2, System.DateTime.Now);// kiểm tra lại
            grvDichVuCS2.SetFocusedRowCellValue(colThanhTienCS2, 0);
            grvDichVuCS2.SetFocusedRowCellValue(colTTLuucdCS2, 1);
            grvDichVuCS2.SetFocusedRowCellValue(colKPhongCS2, DungChung.Bien.MaKP);
            string MaCB = "";
            if (lupNguoiKhamkb.EditValue != null)
                MaCB = Convert.ToString(lupNguoiKhamkb.EditValue);
            if (MaCB != "")
                grvDichVuCS2.SetFocusedRowCellValue(colBSTHCS2, MaCB);
        }

        private void grvDichVuCS2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int iddonct = 0;
            if (grvDichVuCS2.GetFocusedRowCellValue(colIDdoncs2) != null && grvDichVuCS2.GetFocusedRowCellValue(colIDdoncs2).ToString() != "" && Convert.ToInt32(grvDichVuCS2.GetFocusedRowCellValue(colIDdoncs2)) > 0)
            {
                iddonct = Convert.ToInt32(grvDichVuCS2.GetFocusedRowCellValue(colIDdoncs2));
            }

            var qdt = _dataContext.DThuoccts.Where(p => p.IDDonct == iddonct).Where(p => p.ThanhToan == 1).FirstOrDefault();

            if (qdt != null)
                colTrongBHdv.OptionsColumn.ReadOnly = true;
            else
            {
                colTrongBHdv.OptionsColumn.ReadOnly = false;
            }

        }

        private void txtBenhKhac4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBenhKhac4.Text))
            {
                if (txtBenhPhu.EditValue.ToString() == "0")
                {
                    txtBenhPhu.EditValue = "";
                    LupICD4.EditValue = "";
                    txtBenhKhac4.Text = "";
                }
                else
                {
                    LupICD4.EditValue = lICD.Where(p => p.TenICD == txtBenhKhac4.EditValue.ToString()).Select(p => p.MaICD).FirstOrDefault();
                }
            }
            else
            {
                txtBenhKhac4.Text = "";
                txtBenhPhu.Text = "";
            }
        }

        private void barToDieuTriNgTru_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0, kpKham = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (lupTimMaKP.EditValue != null)
                kpKham = Convert.ToInt32(lupTimMaKP.EditValue);
            var ktkb = _dataContext.BNKBs.Where(p => p.MaBNhan == _int_maBN && p.MaKP == kpKham).OrderByDescending(p => p.IDKB).ToList();
            if (ktkb.Count() > 0)
            {
                BenhNhan ttbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                VaoVien ttvv = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                RaVien ttrv = _dataContext.RaViens.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
                frmIn frm = new frmIn();
                BaoCao.rep_TomTatDieuTriNgTru rep = new BaoCao.rep_TomTatDieuTriNgTru(_int_maBN, kpKham);
                rep.MABN.Value = "MABN: " + _int_maBN.ToString();
                rep.TENBN.Value = ttbn.TenBNhan + ", " + ttbn.Tuoi.ToString() + " tuổi, " + (ttbn.GTinh == 1 ? "Nam" : "Nữ");
                rep.STHE.Value = ttbn.SThe;
                rep.DCHI.Value = ttbn.DChi;
                rep.KHOA.Value = lupTimMaKP.Text;
                rep.CHANDOAN.Value = "Chẩn đoán: " + DungChung.Ham.FreshString(ktkb.First().ChanDoan + ";" + ktkb.First().BenhKhac);
                rep.MAICD.Value = "Mã ICD: " + DungChung.Ham.FreshString(ktkb.First().MaICD + ";" + ktkb.First().MaICD2);
                if (ttvv != null)
                {
                    rep.SOVV.Value = "Số vào viện: " + ttvv.SoVV;
                    rep.HUYETAP.Value = ttvv.HuyetAp;
                    rep.NHIETDO.Value = ttvv.NhietDo;
                    rep.NHIPTIM.Value = ttvv.Mach;
                    rep.NHIPTHO.Value = ttvv.NhipTho;
                }
                rep.SoNgDT.Value = ttrv != null ? (ttrv.SoNgaydt ?? 0).ToString() : "";
                rep.NGAYTHANG.Value = DungChung.Ham.NgaySangChu(DateTime.Now, DungChung.Bien.FormatDate);
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bệnh nhân chưa có khám bệnh tại khoa phòng");
            }
        }

        private void btnChiSoCoThe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _int_maBN = 0; int rs;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
            {
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                frm_TTKhamBenh frm = new frm_TTKhamBenh(_int_maBN);
                frm.ShowDialog();
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            var ttbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            var ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            if (ttbn != null)
            {
                BaoCao.rep_TheDiUngThuoc rep = new BaoCao.rep_TheDiUngThuoc();
                rep.TenBNhan.Value = ttbn.TenBNhan.ToUpper();
                rep.Tuoi.Value = ttbn.Tuoi;
                rep.Nam.Value = ttbn.GTinh == 1 ? "x" : "";
                rep.Nu.Value = ttbn.GTinh == 1 ? "" : "x";
                if (ttbn.MaKP != null)
                {
                    int makp = ttbn.MaKP ?? 0;
                    var kp = _dataContext.KPhongs.Where(p => p.MaKP == makp).FirstOrDefault();
                    if (kp != null)
                        rep.KhoaDT.Value = kp.TenKP;
                }
                rep.CMND.Value = ttbx.SoKSinh;
                frmIn frm = new frmIn();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            var ttbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            var ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            if (ttbn != null)
            {
                BaoCao.rep_PhieuKhaiThacTSDiUng rep = new BaoCao.rep_PhieuKhaiThacTSDiUng();
                rep.TenBNhan.Value = ttbn.TenBNhan.ToUpper();
                rep.Tuoi.Value = ttbn.Tuoi;
                rep.Nam.Value = ttbn.GTinh == 1 ? "x" : "";
                rep.Nu.Value = ttbn.GTinh == 1 ? "" : "x";
                if (ttbn.MaKP != null)
                {
                    int makp = ttbn.MaKP ?? 0;
                    var kp = _dataContext.KPhongs.Where(p => p.MaKP == makp).FirstOrDefault();
                    if (kp != null)
                        rep.KhoaDT.Value = kp.TenKP;
                }
                rep.CMND.Value = ttbx.CMT;
                frmIn frm = new frmIn();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnYHCT_Click(object sender, EventArgs e)
        {
            string _ICD = "", _ICD2 = "", _ICD3 = "", _ICDKhac = "";
            if (!string.IsNullOrEmpty(lupMaICDkb.Text))
            {
                _ICD = lupMaICDkb.Text;
            }
            if (!string.IsNullOrEmpty(LupICD2.Text))
            {
                _ICD2 = LupICD2.Text;
            }
            if (!string.IsNullOrEmpty(LupICD3.Text))
            {
                _ICD3 = LupICD3.Text;
            }
            if (!string.IsNullOrEmpty(LupICD4.Text))
            {
                _ICDKhac = DungChung.Ham.FreshString(LupICD4.Text);
            }
            int _idkb = 0;
            if (!string.IsNullOrEmpty(txtIdkb.Text))
                _idkb = Convert.ToInt32(txtIdkb.Text);
            QLBV.FormThamSo.frm_HTBenhYHCT frm = new QLBV.FormThamSo.frm_HTBenhYHCT(_ICD, _ICD2, _ICD3, _ICDKhac, _licd10, _idkb);
            frm.ShowDialog();
        }

        private void btnDonBS_Click(object sender, EventArgs e)
        {
            int _int_maBN = 0;
            Int32.TryParse(txtMaBNhan.Text, out _int_maBN);
            if (_int_maBN > 0)
            {
                frm_kedon frm = new frm_kedon(_int_maBN, -1, DungChung.Bien.MaKP, true);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
            }
        }

        private void btnDonDv_Click(object sender, EventArgs e)
        {
            int _int_maBN = 0;
            Int32.TryParse(txtMaBNhan.Text, out _int_maBN);
            if (_int_maBN > 0)
            {
                frm_kedon frm = new frm_kedon(_int_maBN, -1, DungChung.Bien.MaKP, false);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
            }
        }

        private void grcChuyenKhoa_Click(object sender, EventArgs e)
        {

        }

        private void btnNguoiNhanThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? _idkb = null;
            if (!string.IsNullOrEmpty(txtIdkb.Text))
                _idkb = Convert.ToInt32(txtIdkb.Text);
            if (_idkb != null && DungChung.Bien.MaBV == "34019")
            {
                frmNguoiNhanThuoc frm = new frmNguoiNhanThuoc(_idkb);
                frm.ShowDialog();
            }
        }

        private void bbtnTaoBenhAnNT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (_int_maBN <= 0)
            {
                MessageBox.Show("Chưa chọn bệnh nhân", "Thông báo");
                return;
            }
            if (lupTimMaKP.EditValue != null && lupTimMaKP.EditValue != "")
            {
                var makp = Convert.ToInt32(lupTimMaKP.EditValue);
                var bnkb = _dataContext.BNKBs.FirstOrDefault(o => o.MaKP == makp && o.MaBNhan == _int_maBN);
                frmTaoBenhAnNgoaiTru frm = new frmTaoBenhAnNgoaiTru(_int_maBN, bnkb);
                frm.ShowDialog();
            }
        }

        private void grvBNhankb_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;
                contextMenuStrip1.Show(view.GridControl, e.Point);
            }
        }

        private System.Windows.Media.MediaPlayer pl = new System.Windows.Media.MediaPlayer();

        List<string> urlChuoiDoc = new List<string>();
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (frmGoi != null)
                frmGoi.Dispose();
            btnGoiBenhNhan_ItemClick(null, null);
            urlChuoiDoc = new List<string>();
            var stt = grvBNhankb.GetRowCellValue(grvBNhankb.FocusedRowHandle, "SoTT").ToString();
            var tenBN = grvBNhankb.GetRowCellValue(grvBNhankb.FocusedRowHandle, "TenBNhan").ToString();
            var splitTenBN = tenBN.Split(' ').ToList();
            var tenKP = lupTimMaKP.Text;
            var maBN = grvBNhankb.GetRowCellValue(grvBNhankb.FocusedRowHandle, "MaBNhan").ToString();
            var chuoiDoc = new List<string>();
            chuoiDoc.Add("Xin mời bệnh nhân");
            chuoiDoc.AddRange(splitTenBN);
            chuoiDoc.Add("có số thứ tự");
            chuoiDoc.Add(stt);
            chuoiDoc.Add("vào");
            chuoiDoc.Add(tenKP);

            string message = "";
            for (int i = 0; i < chuoiDoc.Count; i++)
            {
                string result = "";
                var url = DungChung.Ham.CheckfileandDownload(chuoiDoc[i], ref result);

                if (!string.IsNullOrWhiteSpace(result))
                {
                    message += result + ";" + Environment.NewLine;
                }
                if (File.Exists(url))
                {
                    urlChuoiDoc.Add(url);
                }
            }

            if (frmGoi != null)
                frmGoi.Call(tenBN, maBN, stt);
            Play();
            //if (!string.IsNullOrWhiteSpace(message))
            //{
            //    MessageBox.Show(message, "Thông báo");
            //}
        }

        int index = 0;
        private void Play()
        {
            if (index < urlChuoiDoc.Count)
            {
                pl.Stop();
                pl.Open(new Uri(urlChuoiDoc[index].ToString()));
                pl.MediaEnded += pl_MediaEnded;
                pl.Play();
            }
            else
            {
                pl.Stop();
            }
        }

        private void pl_MediaEnded(object sender, EventArgs e)
        {
            if (index == urlChuoiDoc.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;

                Play();
            }
        }

        frmGoiBenhNhan frmGoi;

        private void btnGoiBenhNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmGoi != null)
            {
                frmGoi.Dispose();
                frmGoi = null;
            }
            frmGoi = new frmGoiBenhNhan(DungChung.Bien.MaKP);
            frmGoi.Show();
            ShowFormInExtendMonitor(frmGoi);
            //ShowFormInExtendMonitor(null);
        }

        private static void ShowFormInExtendMonitor(Form control)
        {
            try
            {
                if (control == null)
                {
                    return;
                }
                Screen[] sc;
                sc = Screen.AllScreens;
                if (sc.Length <= 1)
                {
                    control.WindowState = FormWindowState.Maximized;
                    //control.Close();
                    control.FormBorderStyle = FormBorderStyle.None;
                    control.StartPosition = FormStartPosition.Manual;
                    control.Location = sc[1].WorkingArea.Location;
                    //control.WindowState = FormWindowState.Maximized;
                    control.Show();
                }
                else
                {
                    control.FormBorderStyle = FormBorderStyle.None;
                    control.StartPosition = FormStartPosition.Manual;
                    control.Location = sc[2].WorkingArea.Location;
                    control.WindowState = FormWindowState.Maximized;
                    control.Show();
                }
            }
            catch (Exception ex)
            {
            }
        }

        //public class TTBN
        //{
        //    public TTBN(string TenBNhan, string NgaySinh, string ThangSinh, string NamSinh, string SThe, string DChi, string ChanDoan, string MaICD, string TenKP)
        //    {
        //        try
        //        {
        //            this.TenBNhan = TenBNhan;
        //            this.NgayThangNamSinh = DungChung.Ham.GhepNgaySinh("/", NamSinh, ThangSinh, NgaySinh);
        //            this.SThe = SThe;
        //            this.DChi = DChi;
        //            this.ChanDoan = ChanDoan;
        //            this.MaICD = MaICD;
        //            this.TenKP = TenKP;
        //        }
        //        catch (Exception ex)
        //        {
        //            DungChung.WriteLog.Warn(ex);
        //        }
        //    }
        //    public string TenBNhan { get; set; }
        //    public string NgayThangNamSinh { get; set; }
        //    public string SThe { get; set; }
        //    public string DChi { get; set; }
        //    public string ChanDoan { get; set; }
        //    public string MaICD { get; set; }
        //    public string TenKP { get; set; }
        //}

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _int_maBN = 0, kpKham = 0;
            int.TryParse(txtMaBNhan.Text, out _int_maBN);
            if (_int_maBN == 0)
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
                return;
            }
            if (lupTimMaKP.EditValue != null)
                kpKham = Convert.ToInt32(lupTimMaKP.EditValue);
            var bnkb = _dataContext.BNKBs.FirstOrDefault(o => o.MaBNhan == _int_maBN && o.MaKP == kpKham);
            if (bnkb != null)
            {
                frm_NhapTTPheDuyet frm = new frm_NhapTTPheDuyet(bnkb.IDKB);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chưa có thông tin khám bệnh tại phòng");
            }
        }

        string maIcdCbo;
        private void lupMaICDkb_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                OpenChiDinhICD_Properties(e, lupMaICDkb);
            }
            finally
            {
                maIcdCbo = "";
            }
        }

        private void OpenChiDinhICD_Properties(ButtonPressedEventArgs e, LookUpEdit cbo)
        {
            if (e.Button.Kind == ButtonPredefines.Glyph)
            {
                if (cbo.EditValue != null && cbo.EditValue != "")
                {
                    maIcdCbo = cbo.EditValue.ToString();
                    mnChiDinhCLS_ItemClick(null, null);
                }
            }
        }

        private void OpenChiDinhICD_Properties(ButtonPressedEventArgs e, ComboBoxEdit cbo)
        {
            if (e.Button.Kind == ButtonPredefines.Glyph)
            {
                if (cbo.EditValue != null && cbo.EditValue != "")
                {
                    maIcdCbo = cbo.EditValue.ToString();
                    mnChiDinhCLS_ItemClick(null, null);
                }
            }
        }

        private void OpenChiDinhICD(ComboBoxEdit cbo)
        {
            if (cbo.EditValue != null && cbo.EditValue != "")
            {
                maIcdCbo = cbo.EditValue.ToString();
                mnChiDinhCLS_ItemClick(null, null);
            }
        }

        private void OpenChiDinhICD(LookUpEdit cbo)
        {
            if (cbo.EditValue != null && cbo.EditValue != "")
            {
                maIcdCbo = cbo.EditValue.ToString();
                mnChiDinhCLS_ItemClick(null, null);
            }
        }

        private void LupICD2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                OpenChiDinhICD_Properties(e, LupICD2);
            }
            finally
            {
                maIcdCbo = "";
            }
        }

        private void LupICD3_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                OpenChiDinhICD_Properties(e, LupICD3);
            }
            finally
            {
                maIcdCbo = "";
            }
        }

        private void LupICD4_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                OpenChiDinhICD_Properties(e, LupICD4);
            }
            finally
            {
                maIcdCbo = "";
            }
        }

        private void btnPackageICD1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenChiDinhICD(lupMaICDkb);
            }
            finally
            {
                maIcdCbo = "";
            }
        }

        private void btnPackageICD2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenChiDinhICD(LupICD2);
            }
            finally
            {
                maIcdCbo = "";
            }
        }

        private void btnPackageICD3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenChiDinhICD(LupICD3);
            }
            finally
            {
                maIcdCbo = "";
            }
        }

        private void btnPackageICD4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenChiDinhICD(LupICD4);
            }
            finally
            {
                maIcdCbo = "";
            }
        }

        private void bbiDeleteBN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int _int_maBN = 0;
            int.TryParse(txtMaBNhan.Text, out _int_maBN);

            var bn = _dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == _int_maBN);
            if (bn != null)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa bệnh nhân: " + bn.TenBNhan + " không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var ravien = _dataContext.RaViens.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                    var dthuoc = (from dt in _dataContext.DThuocs.Where(o => o.MaBNhan == _int_maBN)
                                  join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon
                                  select dtct).ToList();
                    var cls = _dataContext.CLS.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                    var tamung = _dataContext.TamUngs.FirstOrDefault(p => p.MaBNhan == _int_maBN);
                    if (!string.IsNullOrWhiteSpace(bn.MaCB) && !string.IsNullOrWhiteSpace(DungChung.Bien.MaCB) && DungChung.Bien.MaCB != bn.MaCB)
                    {
                        MessageBox.Show("Bạn không được phép xóa bệnh nhân!");
                        return;
                    }
                    if (tamung != null)
                    {
                        MessageBox.Show("Bệnh nhân đã có tạm thu không thể xóa!");
                        return;
                    }
                    if (ravien != null)
                    {
                        MessageBox.Show("Bệnh nhân đã ra viện không thể xóa!");
                        return;
                    }
                    if (dthuoc != null && dthuoc.Count > 0)
                    {
                        MessageBox.Show("Bệnh nhân đã phát sinh chi phí dịch vụ hoặc đã được kê đơn thuốc. Bạn không thể xóa!");
                        return;
                    }
                    if (cls != null)
                    {
                        MessageBox.Show("Bệnh nhân đã có chỉ định CLS không thể xóa!");
                        return;
                    }

                    using (var scope = new TransactionScope())
                    {
                        var vaovien = _dataContext.VaoViens.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                        if (vaovien != null)
                        {
                            _dataContext.VaoViens.Remove(vaovien);
                        }
                        var bnkb = _dataContext.BNKBs.Where(o => o.MaBNhan == _int_maBN).ToList();
                        foreach (var kb in bnkb)
                        {
                            var dBnkb = _dataContext.BNKBs.FirstOrDefault(o => o.IDKB == kb.IDKB);
                            if (dBnkb != null)
                            {
                                _dataContext.BNKBs.Remove(dBnkb);
                            }
                        }
                        var ttbs = _dataContext.TTboXungs.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                        if (ttbs != null)
                        {
                            _dataContext.TTboXungs.Remove(ttbs);
                        }
                        var updateSoDK = _dataContext.SoDKKBs.Where(p => p.MaBNhan == _int_maBN).ToList();
                        foreach (var item in updateSoDK)
                        {
                            item.MaBNhan = 0;
                            _dataContext.SaveChanges();
                        }

                        int so = bn.SoKB;
                        if (so > 0)
                        {
                            DungChung.Ham.UpdateHSHuy(_int_maBN, bn.MaKP ?? 0, so.ToString(), 3, -1);
                        }
                        var donThuoc = _dataContext.DThuocs.Where(o => o.MaBNhan == _int_maBN).ToList();
                        if (donThuoc != null && donThuoc.Count > 0)
                        {
                            foreach (var dt in donThuoc)
                            {
                                var dtDelete = _dataContext.DThuocs.FirstOrDefault(o => o.IDDon == dt.IDDon);
                                if (dtDelete != null)
                                    _dataContext.DThuocs.Remove(dtDelete);
                            }
                        }
                        _dataContext.BenhNhans.Remove(bn);
                        _dataContext.SaveChanges();
                        scope.Complete();
                        MessageBox.Show("Xóa BN thành công!");
                    }

                    usKhamBenh_Load(null, null);

                }
            }
            else
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
            }

        }
        bool isNoShow = false;
        //Bệnh chính
        private void txtBenhChinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listIcd = lICD.Where(o => o.TenICD.ToUpper().Contains(txtBenhChinh.Text.ToUpper())).ToList();
                if (listIcd != null && listIcd.Count > 0)
                    lupChanDoanKb.Properties.DataSource = listIcd;
                else
                    lupChanDoanKb.Properties.DataSource = lICD;
                lupChanDoanKb.ShowPopup();
                isNoShow = true;
            }

            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhChinh.Text != null || lupMaICDkb.Text.Trim() != null)
                {
                    txtBenhChinh.Text = null;
                    lupMaICDkb.Text = null;
                }
            }
        }

        private void txtBenhChinh_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Combo)
            {
                if (lupChanDoanKb.IsPopupOpen)
                    lupChanDoanKb.ClosePopup();
                else
                    lupChanDoanKb.ShowPopup();
            }
        }



        private void lupChanDoanKb_Closed(object sender, ClosedEventArgs e)
        {
            if (isNoShow)
            {
                isNoShow = false;
                lupChanDoanKb.Properties.DataSource = lICD;
            }
        }
        // Bệnh phụ 
        private void txtBenhPhu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                string maicd = LupICD4.Text;
                TraCuu.Frm_TimKiem_new frm = new TraCuu.Frm_TimKiem_new(maicd);
                frm.GetData = new TraCuu.Frm_TimKiem_new._getstring(getICD4);
                frm.ShowDialog();

            }

            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhPhu.Text != null || LupICD4.Text != null)
                {
                    txtBenhPhu.Text = null;
                    LupICD4.Text = null;
                }
            }
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Combo)
            {
                if (txtBenhKhac4.IsPopupOpen)
                    txtBenhKhac4.ClosePopup();
                else
                    txtBenhKhac4.ShowPopup();
            }
        }
        private void txtBenhKhac4_Closed(object sender, ClosedEventArgs e)
        {
            if (isNoShow)
            {
                isNoShow = false;
                txtBenhKhac4.Properties.Items.AddRange(lICD);
            }
        }

        //Bệnh phụ 2


        private void txtBenhPhu2_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Combo)
            {
                if (txtBenhKhac2.IsPopupOpen)
                    txtBenhKhac2.ClosePopup();
                else
                    txtBenhKhac2.ShowPopup();
            }
        }


        private void txtBenhPhu2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listIcd = lICD.Where(o => o.TenICD.ToUpper().Contains(txtBenhKhac2.Text.ToUpper())).ToList();
                if (listIcd != null && listIcd.Count > 0)
                    txtBenhKhac2.Properties.DataSource = listIcd;
                else
                    txtBenhKhac2.Properties.DataSource = lICD;
                lupChanDoanKb.ShowPopup();
                isNoShow = true;
            }

            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhPhu2.Text != null || LupICD2.Text != null)
                {
                    txtBenhPhu2.Text = null;
                    LupICD2.Text = null;
                }
            }
        }
        private void txtBenhKhac2_Closed(object sender, ClosedEventArgs e)
        {
            if (isNoShow)
            {
                isNoShow = false;
                txtBenhKhac2.Properties.DataSource = lICD;
            }
        }


        //Bệnh phụ 3
        private void txtBenhPhu3_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Combo)
            {
                if (txtBenhKhac3.IsPopupOpen)
                    txtBenhKhac3.ClosePopup();
                else
                    txtBenhKhac3.ShowPopup();
            }
        }

        private void txtBenhPhu3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listIcd = lICD.Where(o => o.TenICD.ToUpper().Contains(txtBenhKhac3.Text.ToUpper())).ToList();
                if (listIcd != null && listIcd.Count > 0)
                    txtBenhKhac3.Properties.DataSource = listIcd;
                else
                    txtBenhKhac3.Properties.DataSource = lICD;
                txtBenhKhac3.ShowPopup();
                isNoShow = true;
            }


            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhPhu3.Text != null || LupICD3.Text != null)
                {
                    txtBenhPhu3.Text = null;
                    LupICD3.Text = null;
                }
            }
        }

        private void txtBenhKhac3_Closed(object sender, ClosedEventArgs e)
        {
            if (isNoShow)
            {
                isNoShow = false;
                txtBenhKhac3.Properties.DataSource = lICD;
            }
        }

        private void txtBenhChinh_EditValueChanged(object sender, EventArgs e)
        {
            mnLuu.Enabled = true;
            if (!string.IsNullOrEmpty(txtBenhChinh.Text))
            {

            }
        }

        private void txtBenhPhu2_EditValueChanged(object sender, EventArgs e)
        {
            mnLuu.Enabled = true;
        }

        private void txtBenhPhu3_EditValueChanged(object sender, EventArgs e)
        {
            mnLuu.Enabled = true;
        }

        private void txtBenhPhu_EditValueChanged(object sender, EventArgs e)
        {
            mnLuu.Enabled = true;
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int MaBn = int.Parse(txtMaBNhan.Text);
            Frm_NhapSoHoSoBenAn frm = new Frm_NhapSoHoSoBenAn(MaBn);
            frm.ShowDialog();
        }

        private void txtLyDoKham_EditValueChanged(object sender, EventArgs e)
        {
            mnLuu.Enabled = true;
        }

        private void txtDauHieuLamSang_EditValueChanged(object sender, EventArgs e)
        {
            mnLuu.Enabled = true;
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            frm_TomTatBenhAn frm = new frm_TomTatBenhAn(_int_maBN);
            frm.ShowDialog();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            frm_NghiDuongThai frm = new frm_NghiDuongThai(_int_maBN);
            frm.ShowDialog();
        }

        private void chkKhamChuyenGia_EditValueChanged(object sender, EventArgs e)
        {
            mnLuu.Enabled = true;
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (_int_maBN > 0 && lupTimMaKP.EditValue != null && lupTimMaKP.EditValue.ToString() != "")
            {
                var rv = _dataContext.RaViens.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                if (rv == null)
                {
                    MessageBox.Show("Bệnh nhân chưa ra viện");
                    return;
                }
                var makp = Convert.ToInt32(lupTimMaKP.EditValue);
                frm_TT_RaVien frm = new frm_TT_RaVien(_int_maBN, makp);
                frm.ShowDialog();
            }
        }

        private void grvBNhankb_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = grvBNhankb.GetDataRow(grvBNhankb.GetRowHandle(e.ListSourceRowIndex));
            if (e.IsGetData && e.Column.UnboundType != DevExpress.Data.UnboundColumnType.Bound)
            {
                if (e.Column.FieldName == "Tuoi_Str")
                {
                    e.Value = (DungChung.Bien.MaBV == "14018" ? (DungChung.Ham.CalculateAge(row["NgaySinh"] != null ? row["NgaySinh"].ToString() : "", row["ThangSinh"] != null ? row["ThangSinh"].ToString() : "", row["NamSinh"] != null ? row["NamSinh"].ToString() : "", "tháng")) : (DungChung.Ham.CalculateAgeByDate(row["NgaySinh"] != null ? row["NgaySinh"].ToString() : "", row["ThangSinh"] != null ? row["ThangSinh"].ToString() : "", row["NamSinh"] != null ? row["NamSinh"].ToString() : "").ToString()));
                }
            }
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (_int_maBN > 0 && lupTimMaKP.EditValue != null && lupTimMaKP.EditValue.ToString() != "")
            {
                var rv = _dataContext.RaViens.FirstOrDefault(o => o.MaBNhan == _int_maBN);
                if (rv == null)
                {
                    MessageBox.Show("Bệnh nhân chưa ra viện");
                    return;
                }
                var makp = Convert.ToInt32(lupTimMaKP.EditValue);
                //frm_NghiViecHuongBHXH frm = new frm_NghiViecHuongBHXH(_int_maBN);
                //frm.ShowDialog();
            }
        }

        private void LupICD4_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void barHCKB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int idkb = 0;

            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            if (txtIdkb.Text != "" && !String.IsNullOrEmpty(txtIdkb.Text))
            {
                idkb = Convert.ToInt32(txtIdkb.Text);
            }
            else idkb = 0;
            FormNhap.Frm_BbHoiChan frm = new FormNhap.Frm_BbHoiChan(_int_maBN, idkb);
            frm.ShowDialog();
        }

        private void grvBenhKhac_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void grvBenhKhac_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            mnLuu.Enabled = true;
        }

        private void txtBenhKhac1_EditValueChanged(object sender, EventArgs e)
        {
            xtraKhamBenh.Text = "K.Bệnh-K.Đơn*";
            mnLuu.Enabled = true;
        }

        private void lupKhac_EditValueChanged(object sender, EventArgs e)
        {
            mnLuu.Enabled = true;
            if (!string.IsNullOrWhiteSpace(lupKhac.Text))
            {

                List<string> a = lupKhac.Text.Split(';').ToList();
                List<string> b = new List<string>();
                List<string> maICDs = new List<string>();
                foreach (var item in a)
                {
                    //maICDs.Add(item);
                    string c = lICD.Where(p => p.MaICD == item.Trim()).Select(p => p.TenICD).FirstOrDefault();
                    b.Add(c);
                }
                txtBenhKhac1.SetEditValue(string.Join(";", b));

            }
            else
            {
                txtBenhKhac1.SetEditValue(null);
            }
        }

        private void lupKhac_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICDKhac);
                frm.ShowDialog();

            }
        }

        private void txtBenhKhac1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                FormThamSo.frm_TimKiem frm = new FormThamSo.frm_TimKiem();
                frm.GetData = new FormThamSo.frm_TimKiem._getstring(getICDKhac);
                frm.ShowDialog();

            }
        }

        private void lupKhac_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

        }

        private void txtBenhChinh_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {

        }

        private void loidan_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            mnLuu.Enabled = true;
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            var vaovien = _dataContext.VaoViens.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault();
            _inGiayNhapVien_24012(_dataContext, _int_maBN);
        }
        void _inGiayNhapVien_24012(QLBVEntities _data, int _mabn)//minhvd
        {
            var _ttbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
            if (_ttbn != null)
            {
                VaoVien vaovien = _dataContext.VaoViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                RaVien ravien = _dataContext.RaViens.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                TTboXung ttbx = _dataContext.TTboXungs.Where(p => p.MaBNhan == _mabn).FirstOrDefault();

                Phieu.repGiayNhapVien rep = new Phieu.repGiayNhapVien();
                frmIn frm = new frmIn();
                rep.Ten.Value = _ttbn.TenBNhan.ToUpper();
                rep.Tuoi.Value = DungChung.Bien.MaBV == "24012" ? DungChung.Ham.TuoitheoThang(_data, _mabn, "72-45") : rep.Tuoi.Value = _ttbn.Tuoi;
                rep.DiaChi.Value = _ttbn.DChi;

                var nghenghiep = _dataContext.DmNNs.Where(p => p.MaNN == ttbx.MaNN).Select(p => p.TenNN).FirstOrDefault();
                if (nghenghiep != null)
                {
                    rep.NgheNghiep.Value = nghenghiep;
                }

                if (_ttbn.NNhap != null)
                {
                    rep.NgayVao.Value = DungChung.Ham.NgaySangChu(Convert.ToDateTime(_ttbn.NNhap), 8);
                }
                rep.ChanDoan.Value = "Chẩn đoán của phòng khám: ";
                if (vaovien != null)
                {
                    if (!string.IsNullOrEmpty(vaovien.ChanDoan))
                    {
                        rep.ChanDoan.Value = "Chẩn đoán của phòng khám: " + vaovien.ChanDoan;
                    }
                }
                int makp = _ttbn.MaKP ?? 0;
                var tenkp = _dataContext.KPhongs.Where(p => p.MaKP == makp).ToList();
                if (tenkp != null)
                {
                    rep.VaoKhoa.Value = tenkp.First().TenKP;
                }
                rep.NgayThang.Value = DungChung.Ham.GetTenTinh(DungChung.Bien.MaBV) + ", " + DungChung.Ham.NgaySangChu(DateTime.Now, 1);
                //rep.NgayThang.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void barButtonItem199_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmKBVaoVien frm = new frmKBVaoVien(int.Parse(txtMaBNhan.Text), _makp, true);
            frm.ShowDialog();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmKBVaoVien frm = new frmKBVaoVien(int.Parse(txtMaBNhan.Text), _makp, true);
            frm.ShowDialog();
        }

        private void lupKhac_ButtonClick(object sender, ButtonPressedEventArgs e)
        {

        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            frm_TienSuVacXin frNhap = new frm_TienSuVacXin(_int_maBN);
            frNhap.ShowDialog();
        }

        private void barButtonItem21_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            CLS.InPhieu.PhieuDieuTriVLTL(_int_maBN);
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            else
                return;
            Frm_ChonDVKT frm = new Frm_ChonDVKT(_int_maBN, lupTimMaKP.Text, lupTimMaKP.EditValue);
        }

        private void dt_NgayChuyen_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void TTRV_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                int rs;
                int _int_maBN = 0;
                if (Int32.TryParse(txtMaBNhan.Text, out rs))
                    _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
                int MaKP = Convert.ToInt32(lupTimMaKP.EditValue);
                FormNhap.Frm_NhapNgayDieuTri frm = new FormNhap.Frm_NhapNgayDieuTri(_int_maBN, MaKP);
                frm.ShowDialog();
            }
        }

        private void btnTraThuoc_Click(object sender, EventArgs e)
        {
            int rs;
            string KhoKe = lupKhoXuat.Text;
            string KP = lupTimMaKP.Text;
            int MaKP = Convert.ToInt32(lupTimMaKP.EditValue);
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            int stt = Convert.ToInt32(_dataContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN).FirstOrDefault().Status.ToString());
            if (stt == 2 || stt == 3)
            {
                MessageBox.Show(" Bệnh nhân đã ra viện/ thanh toán.\n Không thể thêm đơn trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frm_TraThuocNgoaiTru frm = new frm_TraThuocNgoaiTru(KP, KhoKe, _int_maBN, MaKP);
            frm.ShowDialog();
        }
        private void btnPostGPLX_Click(object sender, EventArgs e)
        {
            int mabn = Convert.ToInt32(txtMaBNhan.Text);
            frm_NhapThongTinGPLX frm = new frm_NhapThongTinGPLX(mabn, _idkb);
            frm.ShowDialog();
        }

        private void lupChanDoanKb_GetNotInListValue(object sender, GetNotInListValueEventArgs e)
        {
            return;
        }

        private void usKhamBenh_KeyPress(object sender, KeyPressEventArgs e)
        {
            string benhkhac = txtBenhPhu.Text;
            string benhphu3 = txtBenhPhu3.Text;
            string benhphu2 = txtBenhPhu2.Text;
            string benhchinh = txtBenhChinh.Text;
            /////////
            string mabenhchinh = LupICDKhac.Text;
            string mabenhphu2 = LupICDKhac.Text;
            string mabenhphu3 = LupICDKhac.Text;
            string mabenhkhac = LupICDKhac.Text;
            if (e.KeyChar == '.')
            {
                if (benhkhac != null || mabenhkhac != null)
                {
                    benhkhac = null;
                    mabenhkhac = null;
                }
                else
                {
                    if (benhphu3 != null || mabenhphu3 != null)
                    {
                        benhphu3 = null;
                        mabenhphu3 = null;
                    }
                    else
                    {
                        if (benhphu2 != null || mabenhphu2 != null)
                        {
                            benhphu2 = null;
                            mabenhphu2 = null;
                        }
                        else
                        {
                            if (benhchinh != null || mabenhchinh != null)
                            {
                                benhchinh = null;
                                mabenhchinh = null;
                            }
                        }
                    }
                }
            }
        }

        private void gcNhapBenhKham_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                string benhkhac = txtBenhPhu.Text;
                string benhphu3 = txtBenhPhu3.Text;
                string benhphu2 = txtBenhPhu2.Text;
                string benhchinh = txtBenhChinh.Text;
                /////////
                string mabenhchinh = LupICDKhac.Text;
                string mabenhphu2 = LupICDKhac.Text;
                string mabenhphu3 = LupICDKhac.Text;
                string mabenhkhac = LupICDKhac.Text;
                if (benhkhac != null || mabenhkhac != null)
                {
                    benhkhac = null;
                    mabenhkhac = null;
                }
                else
                {
                    if (benhphu3 != null || mabenhphu3 != null)
                    {
                        benhphu3 = null;
                        mabenhphu3 = null;
                    }
                    else
                    {
                        if (benhphu2 != null || mabenhphu2 != null)
                        {
                            benhphu2 = null;
                            mabenhphu2 = null;
                        }
                        else
                        {
                            if (benhchinh != null || mabenhchinh != null)
                            {
                                benhchinh = null;
                                mabenhchinh = null;
                            }
                        }
                    }
                }
            }
        }

        private void dt_NgayChuyen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                string benhkhac = txtBenhPhu.Text;
                string benhphu3 = txtBenhPhu3.Text;
                string benhphu2 = txtBenhPhu2.Text;
                string benhchinh = txtBenhChinh.Text;
                /////////
                string mabenhchinh = LupICDKhac.Text;
                string mabenhphu2 = LupICDKhac.Text;
                string mabenhphu3 = LupICDKhac.Text;
                string mabenhkhac = LupICDKhac.Text;
                if (benhkhac != null || mabenhkhac != null)
                {
                    benhkhac = null;
                    mabenhkhac = null;
                }
                else
                {
                    if (benhphu3 != null || mabenhphu3 != null)
                    {
                        benhphu3 = null;
                        mabenhphu3 = null;
                    }
                    else
                    {
                        if (benhphu2 != null || mabenhphu2 != null)
                        {
                            benhphu2 = null;
                            mabenhphu2 = null;
                        }
                        else
                        {
                            if (benhchinh != null || mabenhchinh != null)
                            {
                                benhchinh = null;
                                mabenhchinh = null;
                            }
                        }
                    }
                }
            }
        }

        private void LupICDKhac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhChinh.Text.Trim() != null || LupICDKhac.Text.Trim() != null)
                {
                    txtBenhChinh.Text = null;
                    LupICDKhac.Text = null;
                }
            }
        }

        private void LupICD2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhPhu2.Text.Trim() != null || LupICD2.Text.Trim() != null)
                {
                    txtBenhPhu2.Text = null;
                    LupICD2.Text = null;
                }
            }
        }

        private void LupICD3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhPhu3.Text != null || LupICD3.Text != null)
                {
                    txtBenhPhu3.Text = null;
                    LupICD3.Text = null;
                }
            }
        }

        private void LupICDKhac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.F8)
            {
                if (txtBenhChinh.Text != null || lupMaICDkb.Text.Trim() != null)
                {
                    txtBenhChinh.Text = null;
                    lupMaICDkb.Text = null;
                }
            }
        }

        private void lupMaICDkb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhChinh.Text != null || lupMaICDkb.Text.Trim() != null)
                {
                    txtBenhChinh.Text = null;
                    lupMaICDkb.Text = null;
                }
            }
        }

        private void LupICD3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhPhu3.Text != null || LupICD3.Text.Trim() != null)
                {
                    txtBenhPhu3.Text = null;
                    LupICD3.Text = null;
                }
            }
        }

        private void txtBenhKhac2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhKhac2.Text != null || LupICD2.Text.Trim() != null)
                {
                    txtBenhKhac2.Text = null;
                    LupICD2.Text = null;
                }
            }
        }

        private void txtBenhKhac3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (txtBenhKhac3.Text != null || LupICD3.Text.Trim() != null)
                {
                    txtBenhKhac3.Text = null;
                    LupICD3.Text = null;
                }
            }
        }

        private void lupChanDoanKb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (lupChanDoanKb.Text != null || lupMaICDkb.Text.Trim() != null)
                {
                    lupChanDoanKb.Text = null;
                    lupMaICDkb.Text = null;
                }
            }
        }

        private void lupKhoXuat_EditValueChanged(object sender, EventArgs e)
        {
            _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 36-lupKhoXuat_EditValueChanged-4039-    ";
            if (process)
                return;
            ppxuat = 3;

            if (lupKhoXuat.EditValue != null && Convert.ToInt32(lupKhoXuat.EditValue) > 0)
            {
                int maBN = 0;
                if (lupKhoXuat.GetColumnValue("PPXuat") != null)
                    ppxuat = Convert.ToInt32(lupKhoXuat.GetColumnValue("PPXuat"));
                if (!string.IsNullOrEmpty(txtMaBNhan.Text))
                    maBN = Convert.ToInt32(txtMaBNhan.Text);

                //kho kê đơn
                MaKPxd = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);


                if (ppxuat == 1)
                {
                    //colSoLo.Visible = false;
                    //colHanDung.Visible = false;
                    lupIDThuoc.Columns[8].Visible = false;
                    lupIDThuoc.Columns[9].Visible = false;
                }
                else
                {
                    //colSoLo.Visible = true;
                    //colHanDung.Visible = true;
                    lupIDThuoc.Columns[8].Visible = true;
                    lupIDThuoc.Columns[9].Visible = true;
                }

                int idDon = 0;
                if (txtIdDonThuoc.Text != "")
                    idDon = Convert.ToInt32(txtIdDonThuoc.Text);
                MedicineByRooms = _medicinesProvider.GetLupMaDuoc(MaKPxd, idDon, 0);

                if ((lupKhoaKhamkb.EditValue != null && Convert.ToInt32(lupKhoaKhamkb.EditValue) != 0 && MaKPxd != 0)/* && (MaKPxd != maKhoXuat || Convert.ToInt32(lupKhoaKhamkb.EditValue) != _maKhoaKB)*/)
                {
                    if (DungChung.Ham.KTraTT(_dataContext, String.IsNullOrEmpty(txtMaBNhan.Text) ? 0 : Convert.ToInt32(txtMaBNhan.Text)))
                        grvDonThuocct.OptionsBehavior.ReadOnly = true;
                    else
                        grvDonThuocct.OptionsBehavior.ReadOnly = false;


                    var qtt = _lKPhong_data.Where(p => p.MaKP == MaKPxd).Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();

                    string _makpsd = ";";
                    if (lupKhoaKhamkb.EditValue != null)
                        _makpsd = ";" + lupKhoaKhamkb.EditValue.ToString() + ";";


                    lupMaDuoc2.DataSource = null;
                    lupIDThuoc.DataSource = null;
                    _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 37-lupKhoXuat_EditValueChanged-lupMaDuocdt - 4202-    ";


                    lupMaDuoc2.DataSource = MedicineByRooms;
                    lupIDThuoc.DataSource = MedicineByRooms;
                    _txtMessLog += "  \r\n " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  - " + txtMaBNhan.Text + " 39-lupKhoXuat_EditValueChanged-lupMaDuocdt - 4220-    ";

                }
                if (grvDonThuocct.OptionsBehavior.ReadOnly == true)
                {
                    colIDThuoc.OptionsColumn.AllowEdit = false;
                }
                else
                {
                    colIDThuoc.OptionsColumn.AllowEdit = true;
                }
                if (ppxuat == 3 || DungChung.Bien.MaBV == "24012")
                {
                    colSoLo.Visible = true;
                }
                else
                {
                    colSoLo.Visible = false;
                }
                maKhoXuat = MaKPxd;
            }
            else
            {
                lupIDThuoc.DataSource = null;
            }
        }

        private void lupKhoXuat_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (grvDonThuocct.RowCount > 0)
            {
                if (grvDonThuocct.RowCount > 1)
                {

                    int idd = 0;
                    if (!string.IsNullOrEmpty(txtIdDonThuoc.Text))
                        idd = Convert.ToInt32(txtIdDonThuoc.Text);
                    if ((lupKhoXuat.EditValue != null))
                    {
                        maKhoXuat = Convert.ToInt32(lupKhoXuat.EditValue);
                        if (!_changeBenhNhan && _iddthuoc == idd)
                        {
                            MessageBox.Show("Đơn đã có thuốc, bạn không thể sửa kho kê");
                            e.Cancel = true;
                        }
                    }

                }

            }
            _click_khoKe = false;
        }

        /// <summary>
        /// Đồng bộ đơn thuốc quốc gia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSyncMed_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBNhan.Text))
                return;
            if (lupTimMaKP.EditValue == null)
                return;
            if (string.IsNullOrEmpty(txtIdDonThuoc.Text))
                return;

            int mabn = int.Parse(txtMaBNhan.Text);
            int maKp = int.Parse(lupTimMaKP.EditValue.ToString());
            int idDon = int.Parse(txtIdDonThuoc.Text);

            var benhNhan = _dataContext.BenhNhans.FirstOrDefault(f => f.MaBNhan == mabn);
            var bNKB = _dataContext.BNKBs.FirstOrDefault(w => w.MaBNhan == benhNhan.MaBNhan && w.MaKP == maKp);

            var dThuocctModel = binSDonThuocct.DataSource as List<DThuocctModel>;
            var dThuoccts = AppConfig.MyMapper.Map<List<DThuocct>>(dThuocctModel);
            if (dThuoccts == null)
                return;

            var chanDoans = new List<ChanDoan>();
            if (lupMaICDkb.EditValue != null && !string.IsNullOrEmpty(lupMaICDkb.EditValue.ToString()))
            {
                var chan_doan = new ChanDoan()
                {
                    ma_chan_doan = lupMaICDkb.EditValue.ToString(),
                    ten_chan_doan = string.IsNullOrEmpty(txtBenhChinh.Text) ? "." : txtBenhChinh.Text,
                    ket_luan = string.IsNullOrEmpty(bNKB.GhiChu) ? "." : bNKB.GhiChu,
                };

                chanDoans.Add(chan_doan);
            }

            if (LupICD2.EditValue != null && !string.IsNullOrEmpty(LupICD2.EditValue.ToString()))
            {
                var chan_doan = new ChanDoan()
                {
                    ma_chan_doan = LupICD2.EditValue.ToString(),
                    ten_chan_doan = string.IsNullOrEmpty(txtBenhPhu2.Text) ? "." : txtBenhPhu2.Text,
                    ket_luan = string.IsNullOrEmpty(bNKB.GhiChu) ? "." : bNKB.GhiChu,
                };

                chanDoans.Add(chan_doan);
            }

            if (LupICD3.EditValue != null && !string.IsNullOrEmpty(LupICD3.EditValue.ToString()))
            {
                var chan_doan = new ChanDoan()
                {
                    ma_chan_doan = LupICD3.EditValue.ToString(),
                    ten_chan_doan = string.IsNullOrEmpty(txtBenhPhu3.Text) ? "." : txtBenhPhu3.Text,
                    ket_luan = string.IsNullOrEmpty(bNKB.GhiChu) ? "." : bNKB.GhiChu,
                };

                chanDoans.Add(chan_doan);
            }

            if (LupICD4.EditValue != null && !string.IsNullOrEmpty(LupICD4.EditValue.ToString()))
            {
                var chan_doan = new ChanDoan()
                {
                    ma_chan_doan = LupICD4.EditValue.ToString(),
                    ten_chan_doan = string.IsNullOrEmpty(txtBenhPhu.Text) ? "." : txtBenhPhu.Text,
                    ket_luan = string.IsNullOrEmpty(bNKB.GhiChu) ? "." : bNKB.GhiChu,
                };

                chanDoans.Add(chan_doan);
            }

            var message = Circulars27.Circulars27.SendPrescriptions(_dataContext, benhNhan, maKp, idDon, dThuoccts, chanDoans);
            if (string.IsNullOrEmpty(message))
            {
                btnSyncMed.Enabled = false;
                //barBtnDonThuocH_TT04.Enabled = false;
                //barBtnDonThuocN_TT04.Enabled = false;
                //barBtnThuocThuongTT04.Enabled = false;
            }
            barBtnDonThuocH_TT04.Enabled = true;
            barBtnDonThuocN_TT04.Enabled = true;
            barBtnThuocThuongTT04.Enabled = true;
        }

        private void barBtnThuocThuongTT04_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_maBN = 0;
            if (Int32.TryParse(txtMaBNhan.Text, out rs))
                _int_maBN = Convert.ToInt32(txtMaBNhan.Text);
            int _idDon = 0;
            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _idDon = Convert.ToInt32(txtIdDonThuoc.Text);
            int id = 0;
            if (DungChung.Bien.MaBV == "26007" && grvChuyenKhoa.GetFocusedRowCellValue(colID) != null && grvChuyenKhoa.GetFocusedRowCellValue(colID).ToString() != "")
            {
                id = Convert.ToInt32(txtIdkb.Text);
            }
            if ((DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297") && grvChuyenKhoa.GetFocusedRowCellValue(colID) != null && grvChuyenKhoa.GetFocusedRowCellValue(colID).ToString() != "")
            {
                id = Convert.ToInt32(grvChuyenKhoa.GetFocusedRowCellValue(colID));
            }
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            if (_int_maBN > 0)
            {
                int makp = 0;
                if (DungChung.Bien.MaBV == "27001" && lupKhoaKhamkb.EditValue != null)
                    makp = Convert.ToInt32(lupKhoaKhamkb.EditValue);
                //var qMaChuQuan = _data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                //if (qMaChuQuan != null && qMaChuQuan.MaChuQuan == "12001")
                //    DungChung.Ham.InDonThuocTDuong(_int_maBN, _idDon);
                //else
                //{
                //string tuoi = DungChung.Ham.TuoitheoThang(_data, _int_maBN, "72-00");
                DungChung.Ham.InDon_TT04(_idDon, _int_maBN, makp, id);
            }//
            else
            {
                MessageBox.Show("Không có BN để in đơn");
            }
        }
        private void barBtnDonThuocH_TT04_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_IDDon = 0;

            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _int_IDDon = Convert.ToInt32(txtIdDonThuoc.Text);

            string _kieuDon = "Thuốc hướng thần";

            InDonGN_HTT_TT04(_int_IDDon, _kieuDon);
        }

        private void barBtnDonThuocN_TT04_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rs;
            int _int_IDDon = 0;

            if (Int32.TryParse(txtIdDonThuoc.Text, out rs))
                _int_IDDon = Convert.ToInt32(txtIdDonThuoc.Text);

            string _kieuDon = "Thuốc gây nghiện";

            InDonGN_HTT_TT04(_int_IDDon, _kieuDon);
        }

        #region in đơn TT04 bổ xung

        private static void InDonGN_HTT_TT04(int _idDon, string _tentn)
        {
            QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            frmIn frm = new frmIn();
            var ktkd = (from dt in DaTaContext.DThuocs.Where(p => p.IDDon == _idDon)
                        join cb in DaTaContext.CanBoes on dt.MaCB equals cb.MaCB
                        join kp in DaTaContext.KPhongs on dt.MaKP equals kp.MaKP
                        select new { dt.MaDTQG, dt.GhiChu, dt.IDDon, dt.KieuDon, dt.LoaiDuoc, dt.MaBNhan, dt.NgayKe, dt.PLDV, cb.TenCB, cb.CapBac, kp.TenKP }).ToList();

            if (ktkd.Count > 0)// kiểm tra có đơn thuốc hay chưa
            {
                //if (DungChung.Bien.MaBV == "08602") // Na hang ( tùng y/c ngày 05/05/16)- thông tư 05
                //{
                #region in mẫu mới
                int _int_maBN = ktkd.First().MaBNhan ?? 0;
                var ttd = (from bn in DaTaContext.BenhNhans.Where(p => p.MaBNhan == _int_maBN)
                           join kb in DaTaContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                           join ttbs in DaTaContext.TTboXungs on bn.MaBNhan equals ttbs.MaBNhan
                           select new { bn.GTinh, bn.TenBNhan, bn.NamSinh, bn.NgaySinh, bn.ThangSinh, bn.NNhap, kb.MaICD, kb.ChanDoan, kb.BenhKhac, kb.IDKB, bn.SThe, bn.DChi, kb.GhiChu, kb.NguoiNhanThuoc, kb.SoCMNDNguoiNhanThuoc, ttbs.SoKSinh, ttbs.NThan, ttbs.DThoaiNT, ttbs.CanNang_ChieuCao, ttbs.DThoai }).OrderByDescending(p => p.IDKB).ToList();

                var qd1 = (from dt in DaTaContext.DThuocs.Where(p => p.MaBNhan == _int_maBN).Where(p => p.IDDon == _idDon).Where(p => p.PLDV == 1)
                           join dtct in DaTaContext.DThuoccts on dt.IDDon equals dtct.IDDon
                           join dv in DaTaContext.DichVus on dtct.MaDV equals dv.MaDV
                           join tn in DaTaContext.TieuNhomDVs.Where(p => Bien.MaBV == "12122" ? p.IDNhom == 4 : true) on dv.IdTieuNhom equals tn.IdTieuNhom
                           //where tn.TenRG != "Thuốc gây nghiện" && tn.TenRG != "Thuốc hướng tâm thần"
                           select new { dt.MaDTQG, MaTam = dv.MaTam, TenTNRG = tn.TenRG, dv.TenRG, TenDV = _maCQCQ == "00000" ? dv.TenHC + " " + dv.HamLuong : ((Bien.MaBV == "12345" || Bien.MaBV == "24297") ? ((dv.TenDV ?? "") + " (" + (dv.TenHC ?? "") + ") " + (dv.HamLuong ?? "")) : ((dv.TenHC.Contains(",") || dv.TenHC.Contains("+") || dv.TenHC.Contains(";")) ? dv.TenDV : (dv.TenDV + " (" + dv.TenHC + ") " + dv.HamLuong))), dv.TenHC, dv.HamLuong, dv.MaDV, dv.DonVi, dtct.SoLuong, dtct.IDDonct, dtct.GhiChu, HuongDan = "", DuongD = dtct.DuongD ?? "", SoLan = dtct.SoLan ?? "", MoiLan = dtct.MoiLan ?? "", Luong = dtct.Luong ?? "", DviUong = dtct.DviUong ?? "", LoiDan = dt.GhiChu, TenDVMain = dv.TenDV }).OrderBy(p => p.IDDonct).ToList();
                //var donThuoc = new DonThuoc();
                // var cDonThuoc = donThuoc.Clone();

                if (_tentn == "Thuốc gây nghiện")
                {
                    Phieu.repDonThuoc_TT04_N repd = new Phieu.repDonThuoc_TT04_N();
                    repd.MaDonThuoc.Value = ktkd.First().MaDTQG;
                    if (ttd.Count > 0)
                    {
                        string _ghichu = ttd.First().GhiChu ?? "";
                        string[] ar = _ghichu.Split(';');
                        if (ar.Length > 2)
                            repd.paraDotDieuTri.Value = ar[2];
                        repd._TenBNhan.Value = ttd.First().TenBNhan;
                        string tuoi = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(DaTaContext, _int_maBN) : DungChung.Ham.TuoitheoThang(DaTaContext, _int_maBN, DungChung.Bien.formatAge);
                        repd.NSinh.Value = ttd.First().NgaySinh + "/" + ttd.First().ThangSinh + "/" + ttd.First().NamSinh;
                        // KT
                        switch (ttd.First().GTinh)
                        {
                            case 1:
                                repd.Nam.Value = "X";
                                break;

                            case 0:
                                repd.Nu.Value = "X";
                                break;
                        }
                        if (ttd.First().CanNang_ChieuCao != null && ttd.First().CanNang_ChieuCao.Contains(";"))
                        {
                            repd.CanNang.Value = ttd.First().CanNang_ChieuCao.Split(';')[0];
                        }
                        else
                        {
                            repd.CanNang.Value = ttd.First().CanNang_ChieuCao;
                        }
                        if (!string.IsNullOrEmpty(ttd.First().NThan) && ttd.First().NThan.Contains(";"))
                        {
                            string[] arrnt = ttd.First().NThan.Split(';');
                            repd.HoTenNguoiThan.Value = arrnt[0];
                        }
                        else
                        {
                            repd.HoTenNguoiThan.Value = ttd.First().NThan;
                        }
                        repd.ICD.Value = DungChung.Ham.getMaICDarr(DaTaContext, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                        repd.SoTheBH.Value = ttd.First().SThe;
                        repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(DaTaContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                        repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                        repd.TenKP.Value = ktkd.First().TenKP;
                        repd.DiaChi.Value = ttd.First().DChi;
                        repd.CMT.Value = ttd.First().SoKSinh;
                        repd._MaBNhan.Value = _int_maBN.ToString();
                        if (ktkd.First().NgayKe.Value.Day > 0)
                            repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                        if (int.Parse(tuoi) < 15)
                        {
                            //repd.SDTLienHeBNhan.Value = Bien.MaBV == "01830" ? ((ktkd.First().SoDT != null && ktkd.First().SoDT != "") ? (ktkd.First().SoDT + ";" + ttd.First().DThoai) : "") : "";
                            repd.SDTLienHeBNhan.Value = ttd.First().DThoaiNT != null ? ";" + ttd.First().DThoaiNT : "";
                            repd.SDTLienHeBNhan.Value += ";" + ttd.First().DThoai;
                        }
                        else
                        {
                            repd.SDTLienHeBNhan.Value = ttd.First().DThoai + ";" + ttd.First().DThoaiNT;
                        }
                    }

                    string ngay = ktkd.FirstOrDefault().NgayKe.Value.Day > 9 ? ktkd.FirstOrDefault().NgayKe.Value.Day.ToString() : "0" + ktkd.FirstOrDefault().NgayKe.Value.Day.ToString();
                    string thang = ktkd.First().NgayKe.Value.Month > 9 ? ktkd.FirstOrDefault().NgayKe.Value.Month.ToString() : "0" + ktkd.FirstOrDefault().NgayKe.Value.Month.ToString();
                    string nam = ktkd.FirstOrDefault().NgayKe.Value.Year.ToString();

                    repd.Today.Value = "Ngày " + ngay + " tháng " + thang + " năm " + nam;

                    var qd2 = (from dv in qd1
                               group dv by new { dv.TenDV, dv.MaDV, dv.DonVi, dv.HamLuong, dv.TenRG, dv.MaTam, dv.IDDonct } into kq
                               select new InDonClass { TenDV = (Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV, HamLuong = kq.Key.HamLuong, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.SoLuong), HuongDan = kq.First().DuongD + " " + kq.First().SoLan + " " + kq.First().MoiLan + " " + kq.First().Luong + " " + kq.First().DviUong + " " + "; " + kq.First().GhiChu, TenHC = kq.First().TenHC, TenDVMain = kq.First().TenDVMain, IDDonct = kq.Key.IDDonct, MaTam = kq.Key.MaTam }).OrderBy(p => p.IDDonct).ToList();

                    if (Bien.MaBV == "24012")
                    {
                        var arr = qd2.ToList();
                        var d1 = (from a in arr
                                  group a by a.MaDV into kq
                                  let count = kq.Count()
                                  where count > 1
                                  select kq.Key);
                        List<InDonClass> arrNew = new List<InDonClass>();
                        if (d1.Count() > 0)
                        {
                            foreach (var item in d1)
                            {
                                var qd21 = qd2.Where(p => p.MaDV == item).ToList();
                                InDonClass newDon = new InDonClass();
                                newDon.TenDV = qd21.First().TenDV;
                                newDon.HamLuong = qd21.First().HamLuong;
                                newDon.MaDV = qd21.First().MaDV;
                                newDon.DonVi = qd21.First().DonVi;
                                newDon.HuongDan = qd21.First().HuongDan;
                                newDon.TenHC = qd21.First().TenHC;
                                newDon.TenDVMain = qd21.First().TenDVMain;
                                newDon.IDDonct = qd21.First().IDDonct;
                                newDon.MaTam = qd21.First().MaTam;
                                newDon.SoLuong = qd21.Sum(p => p.SoLuong);
                                arrNew.Add(newDon);
                            }
                        }
                        var qd22 = qd2.Where(p => !d1.Contains(p.MaDV)).ToList();
                        foreach (var a in qd22)
                        {
                            InDonClass newDon = new InDonClass();
                            newDon.TenDV = a.TenDV;
                            newDon.HamLuong = a.HamLuong;
                            newDon.MaDV = a.MaDV;
                            newDon.DonVi = a.DonVi;
                            newDon.HuongDan = a.HuongDan;
                            newDon.TenHC = a.TenHC;
                            newDon.TenDVMain = a.TenDVMain;
                            newDon.IDDonct = a.IDDonct;
                            newDon.MaTam = a.MaTam;
                            newDon.SoLuong = a.SoLuong;
                            arrNew.Add(newDon);
                        }
                        repd.DataSource = arrNew.OrderBy(p => p.IDDonct);
                    }
                    else
                        repd.DataSource = qd2.ToList();
                    repd.ThuKho.Value = DungChung.Bien.ThuKho;
                    repd.SDTLienHeBNhan.Value = ttd.First().DThoaiNT;

                    repd.SDT_CB.Value = "Điện thoại: " + DungChung.Ham.GetSDTBV();
                    repd.LoiDanBS.Value = ktkd.First().GhiChu;
                    repd.LoiDanBS.Value += Bien.MaBV == "24012" ? "\n\n\n" : "";
                    repd.BindData();
                    repd.CreateDocument();
                    frm.prcIN.PrintingSystem = repd.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    Phieu.repDonThuoc_TT04_H repd = new Phieu.repDonThuoc_TT04_H();
                    repd.MaDonThuoc.Value = ktkd.First().MaDTQG;
                    if (ttd.Count > 0)
                    {
                        string _ghichu = ttd.First().GhiChu ?? "";
                        string[] ar = _ghichu.Split(';');
                        if (ar.Length > 2)
                            repd.paraDotDieuTri.Value = ar[2];
                        repd._TenBNhan.Value = ttd.First().TenBNhan;
                        //repd.Tuoi.Value = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(DaTaContext, _int_maBN) : DungChung.Ham.TuoitheoThang(DaTaContext, _int_maBN, DungChung.Bien.formatAge);
                        repd.NSinh.Value = ttd.First().NgaySinh + "/" + ttd.First().ThangSinh + "/" + ttd.First().NamSinh;
                        string tuoi = DungChung.Bien.MaBV == "14018" ? DungChung.Ham.TinhTuoiBenhNhan(DaTaContext, _int_maBN) : DungChung.Ham.TuoitheoThang(DaTaContext, _int_maBN, DungChung.Bien.formatAge);
                        // KT
                        switch (ttd.First().GTinh)
                        {
                            case 1:
                                repd.Nam.Value = "X";
                                break;

                            case 0:
                                repd.Nu.Value = "X";
                                break;
                        }
                        if (ttd.First().CanNang_ChieuCao != null && ttd.First().CanNang_ChieuCao.Contains(";"))
                        {
                            repd.CanNang.Value = ttd.First().CanNang_ChieuCao.Split(';')[0];
                        }
                        else
                        {
                            repd.CanNang.Value = ttd.First().CanNang_ChieuCao;
                        }
                        if (!string.IsNullOrEmpty(ttd.First().NThan) && ttd.First().NThan.Contains(";"))
                        {
                            string[] arrnt = ttd.First().NThan.Split(';');
                            repd.HoTenNguoiThan.Value = arrnt[0];
                        }
                        else
                        {
                            repd.HoTenNguoiThan.Value = ttd.First().NThan;
                        }
                        repd.ICD.Value = DungChung.Ham.getMaICDarr(DaTaContext, _int_maBN, DungChung.Bien.GetICD, 0)[0];
                        repd.SoTheBH.Value = ttd.First().SThe;
                        repd.ChanDoan.Value = DungChung.Ham.getMaICDarr(DaTaContext, _int_maBN, DungChung.Bien.GetICD, 0)[1];
                        repd.TenCB.Value = ktkd.First().CapBac + ": " + ktkd.First().TenCB;
                        repd.CMT.Value = ttd.First().SoKSinh;
                        repd.TenKP.Value = ktkd.First().TenKP;
                        repd.DiaChi.Value = ttd.First().DChi;
                        repd._MaBNhan.Value = _int_maBN.ToString();
                        if (ktkd.First().NgayKe.Value.Day > 0)
                            repd.ngayke.Value = DungChung.Ham.NgaySangChu(ktkd.First().NgayKe.Value);
                        if (int.Parse(tuoi) < 15)
                        {
                            //repd.SDTLienHeBNhan.Value = Bien.MaBV == "01830" ? ((ktkd.First().SoDT != null && ktkd.First().SoDT != "") ? (ktkd.First().SoDT + ";" + ttd.First().DThoai) : "") : "";
                            repd.SDTLienHeBNhan.Value = ttd.First().DThoaiNT != null ? ";" + ttd.First().DThoaiNT : "";
                            repd.SDTLienHeBNhan.Value += ";" + ttd.First().DThoai;
                        }
                        else
                        {
                            repd.SDTLienHeBNhan.Value = ttd.First().DThoai + ";" + ttd.First().DThoaiNT;
                        }
                    }

                    string ngay = ktkd.FirstOrDefault().NgayKe.Value.Day > 9 ? ktkd.FirstOrDefault().NgayKe.Value.Day.ToString() : "0" + ktkd.FirstOrDefault().NgayKe.Value.Day.ToString();
                    string thang = ktkd.First().NgayKe.Value.Month > 9 ? ktkd.FirstOrDefault().NgayKe.Value.Month.ToString() : "0" + ktkd.FirstOrDefault().NgayKe.Value.Month.ToString();
                    string nam = ktkd.FirstOrDefault().NgayKe.Value.Year.ToString();

                    repd.Today.Value = "Ngày " + ngay + " tháng " + thang + " năm " + nam;

                    var qd2 = (from dv in qd1
                               group dv by new { dv.TenDV, dv.MaDV, dv.DonVi, dv.HamLuong, dv.TenRG, dv.MaTam, dv.IDDonct, dv.HuongDan } into kq
                               select new InDonClass { TenDV = (Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV, HamLuong = kq.Key.HamLuong, MaDV = kq.Key.MaDV, DonVi = kq.Key.DonVi, SoLuong = kq.Sum(p => p.SoLuong), HuongDan = kq.Key.HuongDan + " " + kq.First().SoLan + " " + kq.First().MoiLan + " " + kq.First().Luong + " " + kq.First().DviUong + " " + "; " + kq.First().GhiChu, TenHC = kq.First().TenHC, TenDVMain = kq.First().TenDVMain, IDDonct = kq.Key.IDDonct, MaTam = kq.Key.MaTam }).OrderBy(p => p.IDDonct).ToList();

                    if (Bien.MaBV == "24012")
                    {
                        var arr = qd2.ToList();
                        var d1 = (from a in arr
                                  group a by a.MaDV into kq
                                  let count = kq.Count()
                                  where count > 1
                                  select kq.Key);
                        List<InDonClass> arrNew = new List<InDonClass>();
                        if (d1.Count() > 0)
                        {
                            foreach (var item in d1)
                            {
                                var qd21 = qd2.Where(p => p.MaDV == item).ToList();
                                InDonClass newDon = new InDonClass();
                                newDon.TenDV = qd21.First().TenDV;
                                newDon.HamLuong = qd21.First().HamLuong;
                                newDon.MaDV = qd21.First().MaDV;
                                newDon.DonVi = qd21.First().DonVi;
                                newDon.HuongDan = qd21.First().HuongDan;
                                newDon.TenHC = qd21.First().TenHC;
                                newDon.TenDVMain = qd21.First().TenDVMain;
                                newDon.IDDonct = qd21.First().IDDonct;
                                newDon.MaTam = qd21.First().MaTam;
                                newDon.SoLuong = qd21.Sum(p => p.SoLuong);
                                arrNew.Add(newDon);
                            }
                        }
                        var qd22 = qd2.Where(p => !d1.Contains(p.MaDV)).ToList();
                        foreach (var a in qd22)
                        {
                            InDonClass newDon = new InDonClass();
                            newDon.TenDV = a.TenDV;
                            newDon.HamLuong = a.HamLuong;
                            newDon.MaDV = a.MaDV;
                            newDon.DonVi = a.DonVi;
                            newDon.HuongDan = a.HuongDan;
                            newDon.TenHC = a.TenHC;
                            newDon.TenDVMain = a.TenDVMain;
                            newDon.IDDonct = a.IDDonct;
                            newDon.MaTam = a.MaTam;
                            newDon.SoLuong = a.SoLuong;
                            arrNew.Add(newDon);
                        }
                        repd.DataSource = arrNew.OrderBy(p => p.IDDonct);
                    }
                    else
                        repd.DataSource = qd2.ToList();
                    repd.ThuKho.Value = DungChung.Bien.ThuKho;


                    repd.SDT_CB.Value = "Điện thoại: " + DungChung.Ham.GetSDTBV();
                    repd.LoiDanBS.Value = ktkd.First().GhiChu;
                    repd.LoiDanBS.Value += Bien.MaBV == "24012" ? "\n\n\n" : "";
                    repd.BindData();
                    repd.CreateDocument();
                    frm.prcIN.PrintingSystem = repd.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
            }
        }

        #endregion

        private void btnPackageICDKhac_Click(object sender, EventArgs e)
        {
            string maicd = LupICD4.Text;
            TraCuu.Frm_TimKiem_new frm = new TraCuu.Frm_TimKiem_new(maicd);
            frm.GetData = new TraCuu.Frm_TimKiem_new._getstring(getICD4);
            frm.ShowDialog();
        }

        private void LupICD4_MouseClick(object sender, MouseEventArgs e)
        {
            //
        }

        private void txtBenhPhu_MouseClick(object sender, MouseEventArgs e)
        {
            //if (txtBenhPhu.Text != null)
            //{
            //    string maicd = LupICD4.Text;
            //    TraCuu.Frm_TimKiem_new frm = new TraCuu.Frm_TimKiem_new(maicd);
            //    frm.GetData = new TraCuu.Frm_TimKiem_new._getstring(getICD4);
            //    frm.ShowDialog();
            //}
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            ////create variables
            //DateTime now = DateTime.Now;
            //string macb = Bien.MaCB;
            //string tencb = "";
            //string action = "";
            //string strFilePath = "";
            //string strLogMessage = "";
            //string strFileName = string.Format("Log_{0}_{1}", now.Month.ToString("d2"), now.Year + ".xml");

            ////input value
            //var cb = _dataContext.CanBoes.FirstOrDefault(p => p.MaCB == Bien.MaCB);
            //if(cb != null)
            //{
            //    tencb = cb.TenCB;
            //}
            //LogModel log = new LogModel(now, action, macb, tencb);

            ////choose location
            //using (var folderBrowserDialog = new FolderBrowserDialog())
            //{
            //    DialogResult result = folderBrowserDialog.ShowDialog();
            //    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            //    {
            //        strFilePath = folderBrowserDialog.SelectedPath;
            //    }
            //}


            //Ham.WtireLogToTextFile(strFilePath, strFileName, log);
        }

        private List<ThuThuatKbADO> thuThuatADOs = new List<ThuThuatKbADO>();
        private int _mabn = 0;
        private void LoadData()
        {
            var cbo_20001 = (from kp in _dataContext.KPhongs.Where(p => (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") ? true : (p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám"))
                             join cb in _dataContext.CanBoes.Where(p => p.Status == 1).Where(p => p.MaCCHN != "" && p.MaCCHN != null)
                             on kp.MaKP equals cb.MaKP
                             select cb).ToList();
            var mabnn = grvBNhankb.GetFocusedRowCellValue(colMaBNhan) == null ? 0 : Convert.ToInt32(grvBNhankb.GetFocusedRowCellValue(colMaBNhan));
            _mabn = mabnn;
            if (DungChung.Bien.MaBV == "30372")
            {
                cbo_20001 = (from kp in _dataContext.KPhongs.Where(p => (DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") ? true : (p.PLoai == ("Lâm sàng") || p.PLoai == "Phòng khám"))
                             join cb in _dataContext.CanBoes.Where(p => p.Status == 1).Where(p => p.MaCCHN != "" && p.MaCCHN != null)
                             on kp.MaKP equals cb.MaKP
                             where cb.MaKP == 65 || cb.MaKPsd.Contains("65")
                             select cb).ToList();
            }
            //int _IDCL = 0;
            //var cll = _dataContext.CLS.Where(x => x.MaBNhan == mabnn).FirstOrDefault();
            //if (cll.IdCLS > 0)
            //{
            //    _IDCL = cll.IdCLS;
            //}
            //var bnhan = _dataContext.BenhNhans.Where(x => x.MaBNhan == mabnn).FirstOrDefault();
            //bool checkTamThu = bnhan.DTuong == "Dịch vụ" || bnhan.IDDTBN == 2 ? DungChung.Ham._checkTamThu(_dataContext, mabnn, _IDCL) : true;
            if (trangthaiTT.ToUpper().Equals("Chưa thực hiện".ToUpper()) || trangthaiTT.ToUpper().Equals("Đã thực hiện".ToUpper()))
            {
                var lidcd = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == mabnn)
                             join cl in _dataContext.CLS on bn.MaBNhan equals cl.MaBNhan
                             join cd in _dataContext.ChiDinhs on cl.IdCLS equals cd.IdCLS
                             join dv in _dataContext.DichVus on cd.MaDV equals dv.MaDV
                             join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             where tn.TenTN.ToLower().Equals(("Thủ thuật").ToLower())
                             select cd).Select(x => x.IDCD).ToList();
                var cls = (from cl in _dataContext.CLS.Where(p => p.MaBNhan == mabnn)
                           join bn in _dataContext.BenhNhans on cl.MaBNhan equals bn.MaBNhan
                           join cd in _dataContext.ChiDinhs.Where(p => lidcd.Contains(p.IDCD)) on cl.IdCLS equals cd.IdCLS
                           join clsct in _dataContext.CLScts on cd.IDCD equals clsct.IDCD
                           join dv in _dataContext.DichVus.Where(o => ((DungChung.Bien.MaBV == "14018") ? o.IS_EXECUTE_CLS == true : true)) on cd.MaDV equals dv.MaDV
                           join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           join nhom in _dataContext.NhomDVs//.Where(p => p.TenNhomCT == ("Thủ thuật, phẫu thuật"))
                           on tn.IDNhom equals nhom.IDNhom
                           where (tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat || ((DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") ? (tn.TenRG == "Điều trị vật lý" || tn.TenRG == "Điều trị vận động" || tn.TenRG == "Điều trị y học cổ truyền" || tn.TenRG == "Điều trị ngôn ngữ trị liệu") : false))
                           select new { cl, dv, cd, clsct, bn })
                             .ToList()
                             .Select(x => new ThuThuatKbADO
                             {
                                 Check = false,
                                 MaCBth = x.cd.MaCBth == null ? x.dv.IsAutoExecute == true ? x.cl.MaCB : x.cd.MaCBth : x.cd.MaCBth,
                                 TenDV = x.dv.TenDV,
                                 ThoiGianCD = x.cl.NgayThang == null ? null : x.cl.NgayThang.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                 ThoiGianBD = x.cd.NgayBDTH == null ? x.cl.NgayThang.Value.AddSeconds(1).ToString("dd/MM/yyyy HH:mm:ss") : x.cd.NgayBDTH.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                 SoPhutThucHien = x.cd.NgayTH == null ? x.dv.SoPhutThucHien == null || x.dv.SoPhutThucHien == 0 ? "10" : x.dv.SoPhutThucHien.Value.ToString() : x.cd.NgayBDTH == null ? Math.Round(x.cd.NgayTH.Value.Subtract(x.cl.NgayThang.Value).TotalMinutes, 0).ToString() : Math.Round(x.cd.NgayTH.Value.Subtract(x.cd.NgayBDTH.Value).TotalMinutes, 0).ToString(),
                                 ThoiGianKT = x.cd.NgayTH == null ? (x.dv.SoPhutThucHien == null || x.dv.SoPhutThucHien == 0 ? x.cl.NgayThang.Value.AddSeconds(1).AddMinutes(10).ToString("dd/MM/yyyy HH:mm:ss") : x.cl.NgayThang.Value.AddSeconds(1).AddMinutes(x.dv.SoPhutThucHien.Value).ToString("dd/MM/yyyy HH:mm:ss")) : x.cd.NgayTH.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                 KetQua = x.clsct.KetQua,
                                 TrangThai = x.cd.Status == 0 || x.cd.Status.ToString() == null || x.cd.Status.ToString() == "" ? "Chưa thực hiện" : "Đã thực hiện",
                                 ChucNang = x.cd.Status == 0 || x.cd.Status.ToString() == null || x.cd.Status.ToString() == "" ? "Thực hiện" : "Hủy thực hiện",
                                 idcd = x.cd.IDCD.ToString() == null || x.cd.IDCD.ToString() == "" ? null : x.cd.IDCD.ToString(),
                             }).Where(c => c.TrangThai == trangthaiTT).ToList();
                lupCBth.DataSource = cbo_20001;

                if (cls != null)
                {
                    grcThuThuatkb.DataSource = cls;
                    thuThuatADOs = cls;
                }
                else
                {
                    grcThuThuatkb.DataSource = null;
                    thuThuatADOs = null;
                }
                new DevExpress.XtraGrid.Selection.CheckMarksSelection(grvThuThuatkb);
            }
            else
            {
                var lidcd = (from bn in _dataContext.BenhNhans.Where(p => p.MaBNhan == mabnn)
                             join cl in _dataContext.CLS on bn.MaBNhan equals cl.MaBNhan
                             join cd in _dataContext.ChiDinhs on cl.IdCLS equals cd.IdCLS
                             join dv in _dataContext.DichVus on cd.MaDV equals dv.MaDV
                             join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             where tn.TenTN.ToLower().Equals(("Thủ thuật").ToLower())
                             select cd).Select(x => x.IDCD).ToList();
                var cls = (from cl in _dataContext.CLS.Where(p => p.MaBNhan == mabnn)
                           join bn in _dataContext.BenhNhans on cl.MaBNhan equals bn.MaBNhan
                           join cd in _dataContext.ChiDinhs.Where(p => lidcd.Contains(p.IDCD)) on cl.IdCLS equals cd.IdCLS
                           join clsct in _dataContext.CLScts on cd.IDCD equals clsct.IDCD
                           join dv in _dataContext.DichVus.Where(o => ((DungChung.Bien.MaBV == "14018") ? o.IS_EXECUTE_CLS == true : true)) on cd.MaDV equals dv.MaDV
                           join tn in _dataContext.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           join nhom in _dataContext.NhomDVs//.Where(p => p.TenNhomCT == ("Thủ thuật, phẫu thuật"))
                           on tn.IDNhom equals nhom.IDNhom
                           where (tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat || tn.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat || ((DungChung.Bien.MaBV == "14018" || DungChung.Bien.MaBV == "14017") ? (tn.TenRG == "Điều trị vật lý" || tn.TenRG == "Điều trị vận động" || tn.TenRG == "Điều trị y học cổ truyền" || tn.TenRG == "Điều trị ngôn ngữ trị liệu") : false))
                           select new { cl, dv, cd, clsct, bn })
                             .ToList()
                             .Select(x => new ThuThuatKbADO
                             {
                                 Check = false,
                                 MaCBth = x.cd.MaCBth == null ? x.dv.IsAutoExecute == true ? x.cl.MaCB : x.cd.MaCBth : x.cd.MaCBth,
                                 TenDV = x.dv.TenDV,
                                 ThoiGianCD = x.cl.NgayThang == null ? null : x.cl.NgayThang.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                 ThoiGianBD = x.cd.NgayBDTH == null ? x.cl.NgayThang.Value.AddSeconds(1).ToString("dd/MM/yyyy HH:mm:ss") : x.cd.NgayBDTH.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                 SoPhutThucHien = x.cd.NgayTH == null ? x.dv.SoPhutThucHien == null || x.dv.SoPhutThucHien == 0 ? "10" : x.dv.SoPhutThucHien.Value.ToString() : x.cd.NgayBDTH == null ? Math.Round(x.cd.NgayTH.Value.Subtract(x.cl.NgayThang.Value).TotalMinutes, 0).ToString() : Math.Round(x.cd.NgayTH.Value.Subtract(x.cd.NgayBDTH.Value).TotalMinutes, 0).ToString(),
                                 ThoiGianKT = x.cd.NgayTH == null ? (x.dv.SoPhutThucHien == null || x.dv.SoPhutThucHien == 0 ? x.cl.NgayThang.Value.AddSeconds(1).AddMinutes(10).ToString("dd/MM/yyyy HH:mm:ss") : x.cl.NgayThang.Value.AddSeconds(1).AddMinutes(x.dv.SoPhutThucHien.Value).ToString("dd/MM/yyyy HH:mm:ss")) : x.cd.NgayTH.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                                 KetQua = x.clsct.KetQua,
                                 TrangThai = x.cd.Status == 0 || x.cd.Status.ToString() == null || x.cd.Status.ToString() == "" ? "Chưa thực hiện" : "Đã thực hiện",
                                 ChucNang = x.cd.Status == 0 || x.cd.Status.ToString() == null || x.cd.Status.ToString() == "" ? "Thực hiện" : "Hủy thực hiện",
                                 idcd = x.cd.IDCD.ToString() == null || x.cd.IDCD.ToString() == "" ? null : x.cd.IDCD.ToString(),
                             }).ToList();
                lupCBth.DataSource = cbo_20001;
                if (cls != null)
                {
                    grcThuThuatkb.DataSource = cls;
                    thuThuatADOs = cls;
                }
                else 
                {
                    grcThuThuatkb.DataSource = null;
                    thuThuatADOs = null;
                }
                new DevExpress.XtraGrid.Selection.CheckMarksSelection(grvThuThuatkb);
            }
        }
        private void grvThuThuatkb_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colChucNang")
            {
                int idcd = Convert.ToInt32(grvThuThuatkb.GetFocusedRowCellValue("idcd"));
                var bnhan = _dataContext.BenhNhans.Where(x => x.MaBNhan == _mabn).FirstOrDefault();
                bool tamthu = DungChung.Ham.Check_DuyetTamThu(idcd);
                if (bnhan.DTuong == "Dịch vụ" || bnhan.IDDTBN == 2)
                {
                    if (tamthu)
                    {
                        if (idcd > 0)
                        {
                            var cd = _dataContext.ChiDinhs.Where(x => x.IDCD == idcd).FirstOrDefault();
                            var cls = _dataContext.CLS.Where(x => x.IdCLS == cd.IdCLS).FirstOrDefault();
                            var clsct = _dataContext.CLScts.Where(x => x.IdCLS == cls.IdCLS).FirstOrDefault();
                            if (cd.Status == 0)
                            {
                                var ngaybdth = grvThuThuatkb.GetFocusedRowCellValue(colThoiGianBD);
                                var ngayth = grvThuThuatkb.GetFocusedRowCellValue(colThoiGianKT);
                                var macb = grvThuThuatkb.GetFocusedRowCellValue(colCanBoth);
                                var kq = grvThuThuatkb.GetFocusedRowCellValue(colKetQua);
                                if (Convert.ToDateTime(ngaybdth).ToString().Length > 0 && Convert.ToDateTime(ngayth).ToString().Length > 0)
                                {
                                    if (cls.NgayThang.Value > Convert.ToDateTime(ngaybdth))
                                    {
                                        cd.MaCBth = "";
                                        cls.MaCBth = "";
                                        //clsct.KetQua = "";
                                        cd.NgayBDTH = null;
                                        cd.NgayTH = null;
                                        cd.Status = 0;
                                        cls.Status = 0;
                                        MessageBox.Show("Do thời gian bắt đầu thực hiện không thể trước thời gian chỉ định, cần tiến hành nhập lại", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    else if (macb.ToString().Length == 0 || macb == null)
                                    {
                                        cd.MaCBth = "";
                                        cls.MaCBth = "";
                                        cd.Status = 0;
                                        cls.Status = 0;
                                        MessageBox.Show("Do bác sĩ thực hiện đang để trống, cần tiến hành nhập lại");
                                        return;
                                    }
                                    else
                                    {
                                        cd.NgayBDTH = Convert.ToDateTime(ngaybdth);
                                        cd.NgayTH = Convert.ToDateTime(ngayth);
                                        cls.NgayTH = Convert.ToDateTime(ngayth);
                                        cd.Status = 1;
                                        cd.MaCBth = macb.ToString();
                                        cls.MaCBth = macb.ToString();
                                        cls.Status = 1;
                                    }
                                }
                                else
                                {
                                    cd.MaCBth = "";
                                    cls.MaCBth = "";
                                    cd.NgayBDTH = null;
                                    cd.NgayTH = null;
                                    cls.NgayTH = null;
                                    cd.Status = 0;
                                    cls.Status = 0;
                                    MessageBox.Show("Do thời gian bắt đầu thực hiện và thời gian kết thúc để trống, nên chưa thể thực hiện, cần tiến hành nhập lại", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                _dataContext.SaveChanges();
                            }
                            else if (cd.Status == 1)
                            {
                                cd.Status = 0;
                                cd.NgayTH = null;
                                cd.NgayBDTH = null;
                                cd.MaCBth = "";
                                cls.MaCBth = "";
                                cls.Status = 0;
                                _dataContext.SaveChanges();
                            }

                            var mabn = _dataContext.CLS.Where(x => x.IdCLS == cd.IdCLS).Select(x => x.MaBNhan).FirstOrDefault();
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("id không có ràng ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bệnh nhân dịch vụ chưa tạm thu dịch vụ này nên không thể thực hiện", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    if (idcd > 0)
                    {
                        var cd = _dataContext.ChiDinhs.Where(x => x.IDCD == idcd).FirstOrDefault();
                        var cls = _dataContext.CLS.Where(x => x.IdCLS == cd.IdCLS).FirstOrDefault();
                        var clsct = _dataContext.CLScts.Where(x => x.IdCLS == cls.IdCLS).FirstOrDefault();
                        if (cd.Status == 0)
                        {
                            var ngaybdth = grvThuThuatkb.GetFocusedRowCellValue(colThoiGianBD);
                            var ngayth = grvThuThuatkb.GetFocusedRowCellValue(colThoiGianKT);
                            var macb = grvThuThuatkb.GetFocusedRowCellValue(colCanBoth);
                            var kq = grvThuThuatkb.GetFocusedRowCellValue(colKetQua);
                            if (Convert.ToDateTime(ngaybdth).ToString().Length > 0 && Convert.ToDateTime(ngayth).ToString().Length > 0)
                            {
                                if (cls.NgayThang.Value > Convert.ToDateTime(ngaybdth))
                                {
                                    cd.MaCBth = "";
                                    cls.MaCBth = "";
                                    //clsct.KetQua = "";
                                    cd.NgayBDTH = null;
                                    cd.NgayTH = null;
                                    cd.Status = 0;
                                    cls.Status = 0;
                                    MessageBox.Show("Do thời gian bắt đầu thực hiện không thể trước thời gian chỉ định, cần tiến hành nhập lại", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else if (macb.ToString().Length == 0 || macb == null)
                                {
                                    cd.MaCBth = "";
                                    cls.MaCBth = "";
                                    cd.Status = 0;
                                    cls.Status = 0;
                                    MessageBox.Show("Do bác sĩ thực hiện đang để trống, cần tiến hành nhập lại");
                                    return;
                                }
                                else
                                {
                                    cd.NgayBDTH = Convert.ToDateTime(ngaybdth);
                                    cd.NgayTH = Convert.ToDateTime(ngayth);
                                    cls.NgayTH = Convert.ToDateTime(ngayth);
                                    cd.Status = 1;
                                    cd.MaCBth = macb.ToString();
                                    cls.MaCBth = macb.ToString();
                                    cls.Status = 1;
                                }
                            }
                            else
                            {
                                cd.MaCBth = "";
                                cls.MaCBth = "";
                                cd.NgayBDTH = null;
                                cd.NgayTH = null;
                                cls.NgayTH = null;
                                cd.Status = 0;
                                cls.Status = 0;
                                MessageBox.Show("Do thời gian bắt đầu thực hiện và thời gian kết thúc để trống, nên chưa thể thực hiện, cần tiến hành nhập lại", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            _dataContext.SaveChanges();
                        }
                        else if (cd.Status == 1)
                        {
                            cd.Status = 0;
                            cd.NgayTH = null;
                            cd.NgayBDTH = null;
                            cd.MaCBth = "";
                            cls.MaCBth = "";
                            cls.Status = 0;
                            _dataContext.SaveChanges();
                        }
                        var mabn = _dataContext.CLS.Where(x => x.IdCLS == cd.IdCLS).Select(x => x.MaBNhan).FirstOrDefault();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("id không có ràng ");
                    }
                }
                
            }
        }

        private void grvThuThuatkb_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string idcd = grvThuThuatkb.GetFocusedRowCellValue(colIDCDTT) == null ? "" : grvThuThuatkb.GetFocusedRowCellValue(colIDCDTT).ToString();

            if (thuThuatADOs.Where(p => p.idcd == idcd).Count() > 0)
            {
                ThuThuatKbADO sua = thuThuatADOs.Where(p => p.idcd == idcd).First();

                if (e.Column == colChon) // Nếu click vào cột Chọn
                {
                    //mmYlenh.Text = "";
                    if (sua.Check == false)
                    {
                        sua.Check = false;
                    }
                    else if (sua.Check == true)
                    {
                        sua.Check = true;
                    }
                }
            }

            var ngaybdth = grvThuThuatkb.GetFocusedRowCellValue(colThoiGianBD);
            var ngayth = grvThuThuatkb.GetFocusedRowCellValue(colThoiGianKT);
            var sophut = grvThuThuatkb.GetFocusedRowCellValue(colSoPhutThucHien);
            if (e.Column.Name == "colSoPhutThucHien")
            {
                if (grvThuThuatkb.GetRowCellValue(e.RowHandle, colSoPhutThucHien) != null && grvThuThuatkb.GetRowCellValue(e.RowHandle, colSoPhutThucHien).ToString() != "")
                {
                    double sphut = Convert.ToDouble(sophut);

                    DateTime ngaythh = Convert.ToDateTime(ngaybdth).AddMinutes(sphut);
                    if (grvThuThuatkb.GetRowCellValue(e.RowHandle, colSoPhutThucHien).ToString() != "0")
                    {
                        grvThuThuatkb.SetRowCellValue(e.RowHandle, colThoiGianKT, ngaythh.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                    else
                    {
                        grvThuThuatkb.SetRowCellValue(e.RowHandle, colThoiGianKT, ngaythh.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                }
            }

            if (e.Column.Name == "colThoiGianBD")
            {
                if (grvThuThuatkb.GetRowCellValue(e.RowHandle, colThoiGianBD) != null && grvThuThuatkb.GetRowCellValue(e.RowHandle, colThoiGianBD).ToString() != "")
                {
                    double sphut = Convert.ToDouble(sophut);

                    DateTime ngaythh = Convert.ToDateTime(ngaybdth).AddMinutes(sphut);
                    if (grvThuThuatkb.GetRowCellValue(e.RowHandle, colThoiGianBD).ToString() != "")
                    {
                        grvThuThuatkb.SetRowCellValue(e.RowHandle, colThoiGianKT, ngaythh.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                    else
                    {
                        grvThuThuatkb.SetRowCellValue(e.RowHandle, colThoiGianKT, ngaythh.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                }
            }
        }

        private void btnThucHienAllThuThuat_Click(object sender, EventArgs e)
        {
            var lmacd = thuThuatADOs.Select(x => x.idcd).ToList();
            //foreach (var item in lmacd)
            //{
            //    var cd = DaTaContext.ChiDinhs.Where(x => x.IDCD == Convert.ToInt32(item)).FirstOrDefault();
            //}
            if (DialogResult.Yes == MessageBox.Show("bạn muốn thực hiện tất cả", "!!!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                if (lmacd.Count() <= 0)
                {
                    MessageBox.Show("Chưa tích chọn chỉ định, nên chưa thực hiện được", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                for (int i = 0; i < lmacd.Count(); i++)
                {
                    var idcd = grvThuThuatkb.GetRowCellValue(i, colIDCDTT);

                    ChiDinh cd = new ChiDinh();
                    if (Convert.ToInt32(idcd) > 0)
                    {
                        int idc_d = Convert.ToInt32(idcd);
                        cd = _dataContext.ChiDinhs.Where(x => x.IDCD == idc_d).FirstOrDefault();
                    }
                    var bnhan = _dataContext.BenhNhans.Where(x => x.MaBNhan == _mabn).FirstOrDefault();
                    bool tamthu = DungChung.Ham.Check_DuyetTamThu(cd.IDCD);
                    if (bnhan.DTuong == "Dịch vụ" || bnhan.IDDTBN == 2)
                    {
                        var check = thuThuatADOs.Where(x => x.idcd == idcd.ToString()).FirstOrDefault().Check;
                        if (check == true)
                        {
                            if (tamthu)
                            {
                                var cls = _dataContext.CLS.Where(x => x.IdCLS == cd.IdCLS).FirstOrDefault();
                                var clsct = _dataContext.CLScts.Where(x => x.IdCLS == cls.IdCLS).FirstOrDefault();
                                var ngaybdth = grvThuThuatkb.GetRowCellValue(i, colThoiGianBD);
                                var ngayth = grvThuThuatkb.GetRowCellValue(i, colThoiGianKT);
                                var macb = grvThuThuatkb.GetRowCellValue(i, colCanBoth);
                                var kq = grvThuThuatkb.GetRowCellValue(i, colKetQua);
                                string status = grvThuThuatkb.GetRowCellValue(i, colTrangThaiTT).ToString();
                                if (Convert.ToDateTime(ngaybdth).ToString().Length > 0 && Convert.ToDateTime(ngayth).ToString().Length > 0)
                                {
                                    if (cls.NgayThang.Value > Convert.ToDateTime(ngaybdth))
                                    {
                                        cd.MaCBth = "";
                                        cls.MaCBth = "";
                                        //clsct.KetQua = "";
                                        cd.NgayBDTH = null;
                                        cd.NgayTH = null;
                                        cd.Status = 0;
                                        cls.Status = 0;
                                        MessageBox.Show("Do thời gian bắt đầu thực hiện không thể trước thời gian chỉ định, cần tiến hành nhập lại", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }
                                    if (macb.ToString().Length == 0 || macb == null)
                                    {
                                        cd.MaCBth = "";
                                        cls.MaCBth = "";
                                        //clsct.KetQua = "";
                                        cd.NgayBDTH = null;
                                        cd.NgayTH = null;
                                        cls.NgayTH = null;
                                        cd.Status = 0;
                                        cls.Status = 0;
                                        MessageBox.Show("Do bác sĩ thực hiện đang để trống, cần tiến hành nhập lại");
                                        continue;
                                    }
                                    else
                                    {
                                        cd.NgayBDTH = Convert.ToDateTime(ngaybdth);
                                        cd.NgayTH = Convert.ToDateTime(ngayth);
                                        cls.NgayTH = Convert.ToDateTime(ngayth);
                                        cd.Status = 1;
                                        cd.MaCBth = macb.ToString();
                                        cls.MaCBth = macb.ToString();
                                        cls.Status = 1;
                                    }
                                }
                                else
                                {
                                    cd.MaCBth = "";
                                    cls.MaCBth = "";
                                    cd.NgayBDTH = null;
                                    cls.NgayTH = null;
                                    cd.NgayTH = null;
                                    cd.Status = 0;
                                    cls.Status = 0;
                                    MessageBox.Show("Do thời gian bắt đầu thực hiện và thời gian kết thúc để trống, nên chưa thể thực hiện, cần tiến hành nhập lại", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }

                                _dataContext.SaveChanges();
                            }
                            else
                            {
                                MessageBox.Show("Bệnh nhân dịch vụ chưa tạm thu dịch vụ này nên không thể thực hiện", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                continue;
                            }
                        }
                    }
                    else
                    {
                        var check = thuThuatADOs.Where(x => x.idcd == idcd.ToString()).FirstOrDefault().Check;
                        if (check == true)
                        {
                            var cls = _dataContext.CLS.Where(x => x.IdCLS == cd.IdCLS).FirstOrDefault();
                            var clsct = _dataContext.CLScts.Where(x => x.IdCLS == cls.IdCLS).FirstOrDefault();
                            var ngaybdth = grvThuThuatkb.GetRowCellValue(i, colThoiGianBD);
                            var ngayth = grvThuThuatkb.GetRowCellValue(i, colThoiGianKT);
                            var macb = grvThuThuatkb.GetRowCellValue(i, colCanBoth);
                            var kq = grvThuThuatkb.GetRowCellValue(i, colKetQua);
                            string status = grvThuThuatkb.GetRowCellValue(i, colTrangThaiTT).ToString();
                            if (Convert.ToDateTime(ngaybdth).ToString().Length > 0 && Convert.ToDateTime(ngayth).ToString().Length > 0)
                            {
                                if (cls.NgayThang.Value > Convert.ToDateTime(ngaybdth))
                                {
                                    cd.MaCBth = "";
                                    cls.MaCBth = "";
                                    //clsct.KetQua = "";
                                    cd.NgayBDTH = null;
                                    cd.NgayTH = null;
                                    cd.Status = 0;
                                    cls.Status = 0;
                                    MessageBox.Show("Do thời gian bắt đầu thực hiện không thể trước thời gian chỉ định, cần tiến hành nhập lại", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    continue;
                                }
                                if (macb.ToString().Length == 0 || macb == null)
                                {
                                    cd.MaCBth = "";
                                    cls.MaCBth = "";
                                    //clsct.KetQua = "";
                                    cd.NgayBDTH = null;
                                    cd.NgayTH = null;
                                    cls.NgayTH = null;
                                    cd.Status = 0;
                                    cls.Status = 0;
                                    MessageBox.Show("Do bác sĩ thực hiện đang để trống, cần tiến hành nhập lại");
                                    continue;
                                }
                                else
                                {
                                    cd.NgayBDTH = Convert.ToDateTime(ngaybdth);
                                    cd.NgayTH = Convert.ToDateTime(ngayth);
                                    cls.NgayTH = Convert.ToDateTime(ngayth);
                                    cd.Status = 1;
                                    cd.MaCBth = macb.ToString();
                                    cls.MaCBth = macb.ToString();
                                    cls.Status = 1;
                                }
                            }
                            else
                            {
                                cd.MaCBth = "";
                                cls.MaCBth = "";
                                cd.NgayBDTH = null;
                                cls.NgayTH = null;
                                cd.NgayTH = null;
                                cd.Status = 0;
                                cls.Status = 0;
                                MessageBox.Show("Do thời gian bắt đầu thực hiện và thời gian kết thúc để trống, nên chưa thể thực hiện, cần tiến hành nhập lại", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }

                            _dataContext.SaveChanges();
                        }
                    }
                    
                }
                LoadData();
            }

        }

        private void btnHuyAllThuThuat_Click(object sender, EventArgs e)
        {
            var lmacd = thuThuatADOs.Select(x => x.idcd).ToList();
            //
            if (DialogResult.Yes == MessageBox.Show("bạn muốn hủy tất cả", "!!!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                if (lmacd.Count() <= 0)
                {
                    MessageBox.Show("Chưa tích chọn chỉ định, nên chưa hủy thực hiện được", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                for (int i = 0; i < lmacd.Count(); i++)
                {
                    var idcd = grvThuThuatkb.GetRowCellValue(i, colIDCDTT);
                    string status = grvThuThuatkb.GetRowCellValue(i, colTrangThaiTT).ToString();
                    ChiDinh cd = new ChiDinh();
                    if (Convert.ToInt32(idcd) > 0)
                    {
                        int idc_d = Convert.ToInt32(idcd);
                        cd = _dataContext.ChiDinhs.Where(x => x.IDCD == idc_d).FirstOrDefault();
                    }
                    var check = thuThuatADOs.Where(x => x.idcd == idcd.ToString()).FirstOrDefault();
                    if (check.Check == true)
                    {
                        var cls = _dataContext.CLS.Where(x => x.IdCLS == cd.IdCLS).FirstOrDefault();
                        var clsct = _dataContext.CLScts.Where(x => x.IdCLS == cls.IdCLS).FirstOrDefault();
                        cd.MaCBth = "";
                        cls.MaCBth = "";
                        cd.NgayBDTH = null;
                        cd.NgayTH = null;
                        cls.NgayTH = null;
                        cd.Status = 0;
                        cls.Status = 0;
                        check.TrangThai = "Chưa thực hiện";
                        _dataContext.SaveChanges();
                    }
                }
                LoadData();
            }

        }

        private void radTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radTrangThai.SelectedIndex == 0)
            {
                trangthaiTT = "Chưa thực hiện";
            }
            else if (radTrangThai.SelectedIndex == 1)
            {
                trangthaiTT = "Đã thực hiện";
            }
            else
            {
                trangthaiTT = "Tất cả";
            }
            LoadData();
        }

        private void SetTextStock()
        {
            int idThuoc = 0;

            if (grvDonThuocct.GetFocusedRowCellValue(colIDThuoc) == null)
            {
                this.grvDonThuocct.ViewCaption = "Danh sách đơn thuốc";
            }


            if (grvDonThuocct.GetFocusedRowCellValue(colIDThuoc) != null)
            {
                idThuoc = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDThuoc));

                var medicine = GetCurrentMedicine(idThuoc);

                if (medicine != null)
                    this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + medicine.TonHienTai;
            }
        }
    }

    public class TenDuocKD
    {
        private string TenDV;
        private int MaDV;
        private string DonVi;
        public string tenDV
        {
            get { return TenDV; }
            set { TenDV = value; }
        }
        public int madv
        {
            get { return MaDV; }
            set { MaDV = value; }
        }
        public string donVi
        {
            get { return DonVi; }
            set { DonVi = value; }
        }

    }

    public class ThuThuatKbADO
    {
        public string idcd { get; set; }
        public bool? Check { get; set; }
        public string TenDV { get; set; }
        public string ThoiGianCD { get; set; }
        public string ThoiGianBD { get; set; }
        public string SoPhutThucHien { get; set; }
        public string ThoiGianKT { get; set; }
        public string MaCBth { get; set; }
        public string KetQua { get; set; }
        public string TrangThai { get; set; }
        public string ChucNang { get; set; }
    }
}
