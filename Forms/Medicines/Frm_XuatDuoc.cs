using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using QLBV.FormNhap;
using QLBV.Models.Dictionaries.DichVu;
using QLBV.Models.Dictionaries.KPhongs;
using QLBV.Models.Dictionaries.Thuoc;
using QLBV.Providers.Business.Medicines;
using QLBV_Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLBV.Forms.Medicines
{
    public partial class Frm_XuatDuoc : DevExpress.XtraEditors.XtraUserControl
    {
        private QLBVEntities _dataContext;
        private readonly MedicinesProvider _medicinesProvider;
        public Frm_XuatDuoc()
        {
            InitializeComponent();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _medicinesProvider = new MedicinesProvider();
        }
        int ppxuat = -1;
        List<NhapD> _lNhapDuoc = new List<NhapD>();
        IList<NhapDctModel> _lNhapDct = new List<NhapDctModel>();
        IList<MedicineInventoryModel> allMedicines = new List<MedicineInventoryModel>();

        #region biến dùng cho kê thuốc
        int maKhoaKe = 0;
        int TH = -1;
        int maKhoXuat = 0;
        int maDV = 0;
        double donGia = 0;
        string soLo = "";
        DateTime hanDung = new DateTime();
        double slKe = 0;
        int kieuDon = -1000;
        List<int> deleteNhapdcts = new List<int>();
        IList<MedicineInventoryModel> medicinesByRoom = new List<MedicineInventoryModel>();
        #endregion

        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = !Status;
            btnMoi.Enabled = Status;
            btnSua.Enabled = Status;
            btnXoa.Enabled = Status;
            btnKLuu.Enabled = !Status;
            grvNhapCT.OptionsBehavior.Editable = !Status;
        }
        private void EnableControl(bool status)
        {
            dtNgayNhap.Properties.ReadOnly = !status;
            lupMaKP.Properties.ReadOnly = !status;
            txtSoCT.Properties.ReadOnly = !status;
            lupMaKPnx.Properties.ReadOnly = !status;
            lupDTuong.Properties.ReadOnly = !status;
            txtGhiChu.Properties.ReadOnly = !status;
            lupMaBNhan.Properties.ReadOnly = !status;
            lupMaXP.Properties.ReadOnly = !status;
            lupHinhthucx.Properties.ReadOnly = !status;
            txtNguoiNhan.Properties.ReadOnly = !status;
            grcNhap.Enabled = !status;
            grpTimKiem.Enabled = !status;
            txtDiaChi.Properties.ReadOnly = !status;
            cboMien.Properties.ReadOnly = !status;
        }
        private void ResetControl()
        {
            dtNgayNhap.EditValue = System.DateTime.Now;
            txtIDNhap.Text = "";
            txtSoCT.Text = "";
            txtGhiChu.Text = "";
            lupMaKP.EditValue = 0;
            lupMaKPnx.EditValue = 0;
            lupMaBNhan.EditValue = 0;
            txtsophieunew.Text = "";
            lupHinhthucx.EditValue = -1;
            txtDiaChi.ResetText();
            cboMien.ResetText();
        }
        private void hinhthucxuat(int i)
        {
            if (DungChung.Bien.MaBV == "30372")
            {
                labNguoiBenh.Visible = true;

                lupMaBNhan.Visible = true;
            }
            else
            {
                labNguoiBenh.Visible = false;

                lupMaBNhan.Visible = false;
            }


            lupMaXP.Visible = false;
            lupMaKPnx.Visible = false;
            labNoiNhan.Visible = false;

            if (i == 2)
            {
                txtSoCT.Enabled = false;
            }
            else
            {
                txtSoCT.Enabled = true;
            }

            switch (i)
            {
                case 1:

                    lupMaKPnx.Visible = true;
                    labNoiNhan.Visible = true;
                    var qi = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                              where (KhoaKham.PLoai == "Lâm sàng")
                              select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                    lupMaKPnx.Properties.DataSource = qi.ToList();
                    break;
                case 0:
                    lupMaKPnx.Visible = true;
                    labNoiNhan.Visible = true;

                    labNguoiBenh.Visible = true;
                    labNguoiBenh.Visible = true;
                    txtSoCT.Enabled = true;

                    lupMaBNhan.Visible = true;
                    var bn = (from ds in _dataContext.BenhNhans select ds).ToList();
                    lupMaBNhan.Properties.DataSource = bn;
                    var q0 = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                              where (KhoaKham.PLoai == "Phòng khám")
                              select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                    lupMaKPnx.Properties.DataSource = q0.ToList();
                    lupMaXP.EditValue = "";
                    break;
                case 2:

                    lupMaKPnx.Visible = true;
                    labNoiNhan.Visible = true;
                    lupMaBNhan.EditValue = "";

                    var q2 = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                              where (KhoaKham.PLoai == "Khoa dược")
                              select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                    lupMaKPnx.Properties.DataSource = q2.ToList();
                    break;
                case 3:
                    lupMaBNhan.EditValue = "";
                    var q3 = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                              where (KhoaKham.TrongBV == 0)
                              select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                    lupMaKPnx.Properties.DataSource = q3.ToList();
                    break;
                case 4:

                    lupMaKPnx.Visible = true;
                    labNoiNhan.Visible = true;
                    lupMaBNhan.EditValue = "";
                    var q4 = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                              where (KhoaKham.PLoai == "Phòng khám")
                              select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                    lupMaKPnx.Properties.DataSource = q4.ToList();
                    break;
                case 6:
                    lupMaKPnx.Visible = true;
                    labNoiNhan.Visible = true;
                    lupMaBNhan.EditValue = "";
                    var q6 = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                              where (KhoaKham.PLoai == "Tủ trực")
                              select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                    lupMaKPnx.Properties.DataSource = q6.ToList();
                    break;
                case 5:

                    lupMaKPnx.Visible = true;
                    labNoiNhan.Visible = true;
                    lupMaBNhan.EditValue = "";
                    lupMaXP.EditValue = "";
                    var q5 = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                              where (KhoaKham.PLoai == "Cận lâm sàng")
                              select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                    lupMaKPnx.Properties.DataSource = q5.ToList();
                    break;
                case 7:

                    lupMaKPnx.Visible = true;
                    labNoiNhan.Visible = true;
                    lupMaBNhan.EditValue = "";
                    var q7 = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                              where (KhoaKham.PLoai == "Phòng khám")
                              select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                    lupMaKPnx.Properties.DataSource = q7.ToList();
                    break;
                case 9:
                    var q9 = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                              where (KhoaKham.PLoai == "Khoa dược")
                              select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                    lupMaKPnx.Properties.DataSource = q9.ToList();
                    break;

            }
        }
        #region KT
        //Kiem tra trước khi lưu
        private bool KT()
        {
            int htx = -999;
            if (dtNgayNhap.EditValue == null || dtNgayNhap.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn Ngày xuất!");
                dtNgayNhap.Focus();
                return false;
            }
            if (lupMaKP.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho xuất!");
                lupMaKP.Focus();
                return false;
            }
            if (lupHinhthucx.EditValue == null || string.IsNullOrEmpty(lupHinhthucx.Text))
            {
                MessageBox.Show("Bạn chưa chọn hình thức xuất!");
                lupHinhthucx.Focus();
                return false;
            }
            else
            {
                htx = Convert.ToInt32(lupHinhthucx.EditValue);
                if (htx == 2 && (lupMaKPnx.EditValue == null || string.IsNullOrEmpty(lupMaKPnx.Text)))
                {
                    MessageBox.Show("Bạn chưa chọn nơi nhận!");
                    lupMaKPnx.Focus();
                    return false;
                }
            }

            bool checkton = true;
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
                msg += "\nVui lòng kê thuốc khác.";
                MessageBox.Show(msg, "Thuốc không đủ tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        #endregion
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        int _makho = 0;
        int _mkpnx = 0;
        int TTLuu = 0;
        #region hàm tìm kiếm
        private void TimKiem()
        {
            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            if (isLoadForm)
                return;
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (lupTimMaKPnx.EditValue != null)
                _mkpnx = Convert.ToInt32(lupTimMaKPnx.EditValue);
            else
                _mkpnx = 0;
            if (lupTimMaKP.EditValue != null)
                _makho = Convert.ToInt32(lupTimMaKP.EditValue);
            else
                _makho = 0;
            int idNhap = 0;
            string keyWord = txtTimKiem.Text;
            int a = 0;
            if (int.TryParse(keyWord, out a))
                idNhap = Convert.ToInt32(keyWord);

            if (DungChung.Bien.MaBV == "30372")
            {
                var data = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 2)
                            join bn1 in _dataContext.BenhNhans on nd.MaBNhan equals bn1.MaBNhan into bnhan
                            from bn in bnhan.DefaultIfEmpty()
                            where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                            where (nd.MaKP == _makho)
                            where (_mkpnx == 0 ? true : nd.MaKPnx == _mkpnx)
                            where cbo_PlXuat_TK.SelectedIndex == -1 ? true : nd.KieuDon == cbo_PlXuat_TK.SelectedIndex
                            select new
                            {
                                nd.DiaChi,
                                nd.GhiChu,
                                nd.IDNhap,
                                nd.IDSXThuoc,
                                nd.KieuDon,
                                nd.LienThongBanLe_B1,
                                nd.LienThongBanLe_B2,
                                nd.LoaiTang,
                                nd.MaBNhan,
                                nd.MaCB,
                                nd.MaCC,
                                nd.MaKP,
                                nd.MaKPnx,
                                nd.MaXP,
                                nd.Mien,
                                nd.NgayNhap,
                                nd.NgayNhap_NVL,
                                nd.NgayTT,
                                nd.PLoai,
                                nd.SoCT,
                                nd.SoPhieu,
                                nd.SoPL,
                                nd.Status,
                                nd.TangGiaSX,
                                nd.TenNguoiCC,
                                nd.TraDuoc_KieuDon,
                                nd.XuatTD,
                                TenBNhan = bn != null ? bn.TenBNhan : "",
                            }).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList().Where(o => !string.IsNullOrWhiteSpace(keyWord) ? (o.TenBNhan.ToLower().Contains(keyWord.ToLower()) || o.SoCT.ToLower().Contains(keyWord.ToLower()) || o.IDNhap == idNhap) : true).ToList();
                grcNhap.DataSource = data;

            }
            else
            {
                var data = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 2)
                            where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                            where (nd.SoCT.Contains(keyWord) || (idNhap == 0 ? true : (nd.IDNhap == idNhap)))
                            where (nd.MaKP == _makho)
                            where (_mkpnx == 0 ? true : nd.MaKPnx == _mkpnx)
                            where cbo_PlXuat_TK.SelectedIndex == -1 ? true : nd.KieuDon == cbo_PlXuat_TK.SelectedIndex
                            select nd).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
                grcNhap.DataSource = data;
            }
        }
        #endregion
        List<DVu> _dv = new List<DVu>();
        IList<KPhongModel> _lkp = new List<KPhongModel>();
        string _maCQCQ = "";
        bool isLoadForm = false;
        private void Frm_XuatDuoc_Load(object sender, EventArgs e)
        {
            try
            {
                isLoadForm = true;
                _dv = new List<DVu>();
                var qCQCQ = _dataContext.BenhViens.Where(panelControl1 => panelControl1.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
                if (qCQCQ != null)
                    _maCQCQ = qCQCQ.MaChuQuan;

                allMedicines = (from a in _dataContext.DichVus
                                from b in _dataContext.MedicineLists
                                where a.MaDV == b.MaDV
                                select new MedicineInventoryModel()
                                {
                                    IDThuoc = b.IDThuoc,
                                    MaDV = a.MaDV,
                                    TenDV = a.TenDV
                                }).ToList();

                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    this.lupMaDuoc.Columns.Clear();
                    this.lupMaDuoc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDV", 200, "Tên dược"),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaTam", 80, "Mã NB"),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DonVi", "Đơn vị", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Center),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HamLuong", "Hàm lượng", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SoDK", "Số ĐK"),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("soluongton", "SL tồn", 50, DevExpress.Utils.FormatType.Custom, "##,###", true, DevExpress.Utils.HorzAlignment.Far),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaDV", "Mã dược", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
                }
                _lkp = _medicinesProvider.GetListKhoaPhong(DungChung.Bien.MaCB, 0);
                lupHinhthucx.Properties.DataSource = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat();
                Enablebutton(true);
                EnableControl(false);
                dtTimDenNgay.DateTime = System.DateTime.Now;
                dtTimTuNgay.DateTime = System.DateTime.Now;
                dtNgayNhap.EditValue = System.DateTime.Now;
                var xp = (from bvien in _dataContext.BenhViens.Where(p => p.status == 2) select new { bvien.TenBV, bvien.MaBV }).ToList();
                lupMaXP.Properties.DataSource = xp;
                var q = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP) select new { KhoaKham.PLoai, KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                if (q.Count > 0)
                {
                    //lupMaKP.Properties.DataSource = q.Where(p => p.PLoai == ("Khoa dược")).ToList();
                    lupMaKPds.DataSource = q.Where(p => p.PLoai == ("Khoa dược")).ToList();
                    if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                    {
                        lupTimMaKP.Properties.DataSource = q.Where(p => p.PLoai == ("Khoa dược")).ToList();
                    }
                    else
                    {

                        q = (from a in q.Where(p => p.PLoai == ("Khoa dược"))
                             join b in DungChung.Bien.listKPHoatDong on a.MaKP equals b
                             select a).ToList();
                        lupTimMaKP.Properties.DataSource = q;
                    }

                }
                lupMaKP.Properties.DataSource = _medicinesProvider.GetListKhoaPhong("0", 1);

                for (int i = 0; i <= 100; i++)
                    cboMien.Properties.Items.Add(i);
                var qi = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP)
                          where (KhoaKham.PLoai.Contains("lâm sàng") || KhoaKham.PLoai.Contains("phòng khám") || KhoaKham.PLoai.Contains("Khoa dược") || KhoaKham.PLoai.Contains("Tủ trực") || KhoaKham.PLoai.Contains("Xã phường") || KhoaKham.PLoai.Contains("PK khu vực"))
                          select new { KhoaKham.TenKP, KhoaKham.MaKP }).ToList();
                lupTimMaKPnx.Properties.DataSource = qi.ToList();
                LoadDuoc();
                lupTimMaKP.EditValue = DungChung.Bien.MaKP;
                int idct = 0;
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                {
                    idct = Convert.ToInt32(txtIDNhap.Text);
                }

                lupMaCB.DataSource = _dataContext.CanBoes.ToList();
                if (lupHinhthucx.EditValue != null)
                    hinhthucxuat(Convert.ToInt32(lupHinhthucx.EditValue));
                _lNhapDct = _medicinesProvider.ViewInfoMedicineNhapD(idct);
                binNhapDuocct.DataSource = _lNhapDct.ToList();
                grcNhapCT.DataSource = binNhapDuocct;
                lupDTuong.DataSource = _dataContext.DTBNs.ToList();
                if (DungChung.Bien.MaBV == "23456")
                {
                    colMien.Visible = true;
                    cboMien.Enabled = false;
                }
                else
                {
                    colMien.Visible = false;
                    cboMien.Enabled = true;
                }
                if (DungChung.Bien.MaBV != "30372")
                {
                    col_Mabenhnhan.Visible = false;
                    col_Tenbenhnhan.Visible = false;
                }
                else
                    txtTimKiem.Properties.NullValuePrompt += "|Tên bệnh nhân";

                isLoadForm = false;
                TimKiem();
            }
            finally
            {
                isLoadForm = false;
            }
        }

        private void LoadDuoc()
        {
            if (TTLuu == 1 || TTLuu == 2)
            {
                lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;
            }
            else
            {
                lupMaDuoc.DataSource = lupIDThuoc.DataSource = allMedicines;
            }
        }


        private void btnMoi_Click(object sender, EventArgs e)
        {
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[0];
            if (_sua)
            {

                Enablebutton(false);
                EnableControl(true);
                ResetControl();
                txtSoCT.Focus();
                lupMaKP.EditValue = DungChung.Bien.MaKP;
                lupMaKP_EditValueChanged(sender, e);

                //lupDTuong.EditValue = 99;
                _lNhapDct = _medicinesProvider.ViewInfoMedicineNhapD(0);
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
                TTLuu = 1;
            }
        }

        bool isUpdate = true;
        List<MedicineInventoryModel> medicineRepositoryLookupEdit = new List<MedicineInventoryModel>();
        MedicineInventoryModel selectedMedicine = new MedicineInventoryModel();
        private void grvNhapCT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e != null && e.Column != null && e.Column.FieldName != null && sender is GridView gridView && gridView.ActiveEditor != null)
            {
                //Biến dược mới
                //==========================================================================================
                var oldValue = gridView.ActiveEditor.OldEditValue;
                var newValue = gridView.ActiveEditor.EditValue;

                int oldIdThuoc = 0;
                int newIdThuoc = 0;
                int oldMaDV = 0;
                string oldSoLo = "";
                DateTime oldHanDung = new DateTime();
                double oldSL = 0;
                double oldDonGia = 0;

                int newMaDV = 0;
                string newSoLo = "";
                DateTime newHanDung = new DateTime();
                double newSL = 0;
                double newDonGia = 0;
                int maBN = 0;

                double tyleVAT = 0;
                double donGiaVAT = 0;
                double donGia = 0;
                bool isDongY = false;
                //============================================================================================

                switch (e.Column.Name)
                {
                    case nameof(NhapDctModel.MaDV):
                        {
                            if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                            {
                                isUpdate = true;

                                newMaDV = Convert.ToInt32(newValue);

                                //lấy chi tiết đơn thuốc
                                if (lupMaDuoc.DataSource != null)
                                    medicineRepositoryLookupEdit = lupMaDuoc.DataSource as List<MedicineInventoryModel>;

                                selectedMedicine = medicineRepositoryLookupEdit.FirstOrDefault(p => p.MaDV == newMaDV);

                                if (oldValue != null) // trường hợp sửa tên thuốc
                                {
                                    // SL trc khi thay đổi MaDV
                                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                                        oldSL = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                                    oldDonGia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));

                                    oldMaDV = Convert.ToInt32(oldValue);
                                    if (grvNhapCT.GetFocusedRowCellValue(colSoLo) != null && grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString() != "")
                                        oldSoLo = Convert.ToString(grvNhapCT.GetFocusedRowCellValue(colSoLo));
                                    if (grvNhapCT.GetFocusedRowCellValue(colHanDung) != null && grvNhapCT.GetFocusedRowCellValue(colHanDung).ToString() != "")
                                        oldHanDung = Convert.ToDateTime(grvNhapCT.GetFocusedRowCellValue(colHanDung));

                                    _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, oldSL, 0, oldMaDV, 0, oldDonGia, 0, ppxuat);
                                }

                                if (selectedMedicine != null)
                                {
                                    isUpdate = false;

                                    isDongY = _medicinesProvider.IsDongY((int)selectedMedicine.MaDV);

                                    grvNhapCT.SetFocusedRowCellValue(colSoLo, selectedMedicine.SoLo);
                                    grvNhapCT.SetFocusedRowCellValue(colHanDung, selectedMedicine.HanDung);
                                    grvNhapCT.SetFocusedRowCellValue(colDonGia, selectedMedicine.DonGia);
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                    grvNhapCT.SetFocusedRowCellValue(colSLuongDY, 0);
                                    grvNhapCT.SetFocusedRowCellValue(colDonVi, selectedMedicine.DonVi);

                                    isUpdate = true;
                                }
                            }
                        }
                        break;
                    case "colIDThuoc":
                        {
                            if (grvNhapCT.GetFocusedRowCellValue(colIDThuoc) != null)
                            {
                                isUpdate = true;

                                newIdThuoc = Convert.ToInt32(newValue);

                                //lấy chi tiết đơn thuốc

                                if (lupIDThuoc.DataSource != null)
                                    medicineRepositoryLookupEdit = lupIDThuoc.DataSource as List<MedicineInventoryModel>;
                                selectedMedicine = medicineRepositoryLookupEdit.FirstOrDefault(p => p.IDThuoc == newIdThuoc);

                                if (oldValue != null) // trường hợp sửa tên thuốc
                                {
                                    // SL trc khi thay đổi MaDV
                                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                                        oldSL = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                                    oldDonGia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));

                                    oldIdThuoc = Convert.ToInt32(oldValue);
                                    if (grvNhapCT.GetFocusedRowCellValue(colSoLo) != null && grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString() != "")
                                        oldSoLo = Convert.ToString(grvNhapCT.GetFocusedRowCellValue(colSoLo));
                                    if (grvNhapCT.GetFocusedRowCellValue(colHanDung) != null && grvNhapCT.GetFocusedRowCellValue(colHanDung).ToString() != "")
                                        oldHanDung = Convert.ToDateTime(grvNhapCT.GetFocusedRowCellValue(colHanDung));

                                    _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, oldSL, 0, oldIdThuoc, 0, oldDonGia, 0, ppxuat);
                                }

                                if (selectedMedicine != null)
                                {
                                    isUpdate = false;

                                    isDongY = _medicinesProvider.IsDongY((int)selectedMedicine.MaDV);

                                    grvNhapCT.SetFocusedRowCellValue(colSoLo, selectedMedicine.SoLo);
                                    grvNhapCT.SetFocusedRowCellValue(colHanDung, selectedMedicine.HanDung);
                                    grvNhapCT.SetFocusedRowCellValue(colDonGia, selectedMedicine.DonGia);
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                    grvNhapCT.SetFocusedRowCellValue(colSLuongDY, 0);
                                    grvNhapCT.SetFocusedRowCellValue(colDonVi, selectedMedicine.DonVi);

                                    isUpdate = true;
                                }
                            }
                        }
                        break;
                    case "colSoLuong":
                        {
                            if (ppxuat == 1)
                            {
                                #region k theo so lo han dung
                                // Sửa số lượng thì trừ số lượng tồn trong danh mục tồn thuốc
                                var maDVObj = gridView.GetRowCellValue(e.RowHandle, colMaDV);
                                var donGiaObj = gridView.GetRowCellValue(e.RowHandle, colDonGia);

                                if (gridView.ActiveEditor.OldEditValue != null && gridView.ActiveEditor.EditValue != null && maDVObj != null && donGiaObj != null)
                                {
                                    newMaDV = Convert.ToInt32(maDVObj);
                                    oldSL = Convert.ToDouble(gridView.ActiveEditor.OldEditValue, System.Globalization.CultureInfo.InvariantCulture);
                                    newSL = Convert.ToDouble(gridView.ActiveEditor.EditValue, System.Globalization.CultureInfo.InvariantCulture);
                                    newDonGia = Convert.ToDouble(donGiaObj);

                                    var medicineInventory = medicinesByRoom.FirstOrDefault(p => p.MaDV == newMaDV && p.DonGia == newDonGia);
                                    if (isUpdate && medicineInventory != null)
                                    {
                                        if (newSL < 0)
                                        {
                                            isUpdate = false;

                                            grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);

                                            MessageBox.Show("Số lượng phải lớn hơn 0", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            isUpdate = true;
                                        }
                                        else if (newSL > (medicineInventory.TonHienTai + oldSL))
                                        {
                                            isUpdate = false;

                                            grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);

                                            MessageBox.Show("Số lượng thuốc không đủ", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            isUpdate = true;
                                        }
                                        else
                                        {
                                            _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, oldSL, newSL, newMaDV, newMaDV, newDonGia, newDonGia, ppxuat);
                                            this.grvNhapCT.ViewCaption = "Số lượng tồn: " + medicineInventory.TonHienTai;

                                            double thanhTien = Math.Round((double)(newSL * newDonGia), DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);

                                            if (isDongY)
                                            {
                                                grvNhapCT.SetRowCellValue(e.RowHandle, colSLuongDY, newSL);
                                                grvNhapCT.SetRowCellValue(e.RowHandle, colThanhTienDY, thanhTien);
                                            }

                                            grvNhapCT.SetRowCellValue(e.RowHandle, colThanhTien, thanhTien);
                                        }
                                    }
                                }
                                #endregion k theo so lo han dung
                            }
                            else if (ppxuat == 3)
                            {
                                #region theo so lo han dung
                                // Sửa số lượng thì trừ số lượng tồn trong danh mục tồn thuốc
                                var idThuocObj = gridView.GetRowCellValue(e.RowHandle, colIDThuoc);
                                var soLoObj = gridView.GetRowCellValue(e.RowHandle, colSoLo);
                                var hanDungObj = gridView.GetRowCellValue(e.RowHandle, colHanDung);
                                var donGiaObj = gridView.GetRowCellValue(e.RowHandle, colDonGia);

                                if (gridView.ActiveEditor.OldEditValue != null && gridView.ActiveEditor.EditValue != null && idThuocObj != null)
                                {
                                    newIdThuoc = Convert.ToInt32(idThuocObj);
                                    oldSL = Convert.ToDouble(gridView.ActiveEditor.OldEditValue);
                                    newSL = Convert.ToDouble(gridView.ActiveEditor.EditValue);
                                    newDonGia = Convert.ToDouble(donGiaObj);
                                    if (soLoObj != null)
                                        newSoLo = soLoObj.ToString();
                                    if (hanDungObj != null)
                                        newHanDung = Convert.ToDateTime(hanDungObj);

                                    var medicineInventory = medicinesByRoom.FirstOrDefault(p => p.IDThuoc == newIdThuoc);

                                    if (isUpdate && medicineInventory != null)
                                    {
                                        if (newSL <= 0)
                                        {
                                            isUpdate = false;

                                            grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                                            if (isDongY)
                                            {
                                                grvNhapCT.SetRowCellValue(e.RowHandle, colSLuongDY, oldSL);
                                            }

                                            MessageBox.Show("Số lượng phải lớn hơn 0", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                            isUpdate = true;
                                        }
                                        else if (newSL > (medicineInventory.TonHienTai + oldSL))
                                        {
                                            isUpdate = false;

                                            grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                                            if (isDongY)
                                            {
                                                grvNhapCT.SetRowCellValue(e.RowHandle, colSLuongDY, oldSL);
                                            }

                                            MessageBox.Show("Số lượng thuốc không đủ", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                            isUpdate = true;
                                        }
                                        else
                                        {
                                            double thanhTien = Math.Round((double)(newSL * newDonGia), DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);

                                            grvNhapCT.SetRowCellValue(e.RowHandle, colThanhTien, thanhTien);

                                            if (isDongY)
                                            {
                                                grvNhapCT.SetRowCellValue(e.RowHandle, colSLuongDY, newSL);
                                                grvNhapCT.SetRowCellValue(e.RowHandle, colThanhTienDY, thanhTien);
                                            }

                                            _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, oldSL, newSL, newIdThuoc, newIdThuoc, newDonGia, newDonGia, ppxuat);
                                            this.grvNhapCT.ViewCaption = "Số lượng tồn: " + medicineInventory.TonHienTai;
                                        }
                                    }
                                }
                                #endregion theo so lo han dung
                            }
                        }
                        break;
                    case "colMien":
                        if (grvNhapCT.GetFocusedRowCellValue(colThanhTien) != null && grvNhapCT.GetFocusedRowCellValue(colThanhTien).ToString() != "" && grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "" && grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "" && DungChung.Bien.MaBV == "23456")
                        {
                            var dongia = Convert.ToDouble(grvNhapCT.GetRowCellValue(e.RowHandle, colDonGia));
                            var soluong = Convert.ToDouble(grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong));
                            var ttMien = dongia * soluong * (100 - Convert.ToInt32((grvNhapCT.GetRowCellValue(e.RowHandle, colMien) != null ? grvNhapCT.GetRowCellValue(e.RowHandle, colMien) : 0))) / 100;
                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, ttMien);
                        }
                        break;

                }
                SetTextStock();
            }
        }

        private void grvNhapCT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[1];
            if (_sua)
            {
                int status = -1;
                if (!string.IsNullOrEmpty(txtStatus.Text))
                    status = Convert.ToInt32(txtStatus.Text);
                if (status == 1)
                {
                    MessageBox.Show("Chứng từ đã bị khóa, bạn không thể sửa");
                }
                else
                {
                    if (DungChung.Bien.LoaiPM == "QLBV")
                    {
                        kieuDon = Convert.ToInt32(lupHinhthucx.EditValue);
                        int idnhap = 0;
                        if (!string.IsNullOrEmpty(txtIDNhap.Text))
                            idnhap = Convert.ToInt32(txtIDNhap.Text);
                        var kt = _dataContext.NhapDs.Where(p => p.IDNhap == idnhap).ToList();
                        var kt1 = (from a in kt.Where(p => p.KieuDon == 9 && p.PLoai == 2)
                                   join b in _dataContext.TamUngs on a.IDNhap equals b.IDNhapD

                                   select new
                                   {
                                       a.NgayTT,
                                       a.IDNhap,
                                       b.NgayThu
                                   }).ToList();
                        if (kt.Count > 0 && (kt.First().SoCT == "" || kt.First().SoCT == "0" || kt.First().SoCT == null && kieuDon == 2) || kieuDon != 2)
                        {
                            int makpnx = lupMaKP.EditValue != null ? Convert.ToInt32(lupMaKP.EditValue) : 0;

                            lupMaDuoc.DataSource = _medicinesProvider.GetLupMaDuoc(makpnx, idnhap, 0);

                            if (kt.First().XuatTD != null && kt.First().XuatTD > 0)
                            {
                                if (kt.First().XuatTD == 1)
                                    MessageBox.Show("Chứng từ xuất theo phiếu lĩnh hoặc theo đơn bạn không thể sửa.");
                                else
                                    MessageBox.Show("xuất chứng từ theo chức năng, bạn không thể sửa.");

                            }

                            else if (DungChung.Bien.MaBV == "30372" && kt1.Count > 0)
                            {
                                MessageBox.Show("Bạn không thể sửa vì đơn xuất đã thu tiền");
                            }
                            else
                            {
                                if (DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayNhap.DateTime, "KhoaDC") == false)
                                {

                                    int _id = 0;
                                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                                        _id = Convert.ToInt32(txtIDNhap.Text);
                                    var ktxuattd = _dataContext.NhapDs.Where(p => p.XuatTD == _id).ToList();
                                    if (ktxuattd.Count > 0)
                                    {
                                        MessageBox.Show("Chứng từ đã được sử dụng");
                                    }
                                    else
                                    {
                                        if (DungChung.Ham._KiemTraCBSuaXoa(_dataContext, kt.First().MaCB, DungChung.Bien.MaCB))
                                        {

                                            Enablebutton(false);
                                            EnableControl(true);
                                            txtSoCT.Focus();

                                            TTLuu = 2;
                                            lupMaKP_EditValueChanged(sender, e);

                                            if (kieuDon == 2)
                                            {
                                                txtSoCT.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Tên cán bộ không khớp!");
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        Enablebutton(false);
                        EnableControl(true);
                        txtSoCT.Focus();
                        TTLuu = 2;
                    }
                }
            }
        }
        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }
        public bool _ktmatkhau = false;
        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[2];
            if (_sua)
            {
                bool _cothexoa = true;
                int status = -1;
                if (!string.IsNullOrEmpty(txtStatus.Text))
                    status = Convert.ToInt32(txtStatus.Text);
                if (status == 1)
                {
                    MessageBox.Show("Chứng từ đã bị khóa, bạn không thể sửa");
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                    {
                        kieuDon = Convert.ToInt32(lupHinhthucx.EditValue);
                        int id = Convert.ToInt32(txtIDNhap.Text);
                        var kt = _dataContext.NhapDs.Where(p => p.IDNhap == id).ToList();
                        if (kt.Count > 0 && (kieuDon != 2 || (kt.First().SoCT == "0" || kt.First().SoCT == "" || kt.First().SoCT == null && kieuDon == 2)) && DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayNhap.DateTime, "KhoaDC") == false)
                        {

                            int _id = 0;
                            if (!string.IsNullOrEmpty(txtIDNhap.Text))
                                _id = Convert.ToInt32(txtIDNhap.Text);
                            var ktxuattd = _dataContext.NhapDs.Where(p => p.XuatTD == _id).ToList();
                            DialogResult _result = DialogResult.Yes;
                            if (kt.First().KieuDon == 9)
                            {
                                var check = _dataContext.TamUngs.Where(p => p.IDNhapD == _id).ToList();
                                if (check.Count() > 0)
                                {
                                    MessageBox.Show("chứng từ này đã thanh toán, ko thể xóa");
                                    _cothexoa = false;
                                }
                            }
                            if (ktxuattd.Count > 0)
                            {
                                MessageBox.Show("Chứng từ đã được sử dụng, bạn không thể xóa");
                                _cothexoa = false;
                            }
                            else
                            {

                            }
                            if (_cothexoa)
                                if (!DungChung.Ham._KiemTraCBSuaXoa(_dataContext, kt.First().MaCB, DungChung.Bien.MaCB))
                                {
                                    MessageBox.Show("Tên cán bộ không khớp!");
                                    _cothexoa = false;
                                }
                            if (_cothexoa)
                            {
                                if (kt.First().SoPL != null && kt.First().SoPL > 0)
                                {
                                    MessageBox.Show("Chứng từ này được xuất theo phiếu lĩnh số: " + kt.First().SoPL.ToString(), ",\nQuay lại chức năng xuất điều trị để xóa xuất!");
                                    _cothexoa = false;
                                }
                            }
                            if (_cothexoa)
                                _result = MessageBox.Show("Bạn muốn xóa chứng từ số: " + txtSoCT.Text, "xóa chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            _ktmatkhau = false;
                            ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass();
                            frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                            frm.ShowDialog();
                            if (!_ktmatkhau)
                                _cothexoa = false;
                            if (_cothexoa && _result == DialogResult.Yes)
                            {
                                int _sopl = 0;

                                var xoact = _dataContext.NhapDcts.Where(p => p.IDNhap == id).ToList();
                                var xoac = _dataContext.NhapDs.FirstOrDefault(p => p.IDNhap == id);
                                var listDichVus = xoact.Select(o => new DichVu { MaDV = (o.MaDV ?? 0) }).ToList();
                                foreach (var xoa in xoact)
                                {
                                    _medicinesProvider.DeleteNhapDAndNhapDctbyIDNhapct(xoa.IDNhapct);

                                    if (lupHinhthucx.EditValue != null)
                                    {
                                        switch (lupHinhthucx.EditValue)
                                        {
                                            case 2:
                                            case 3:
                                            case 8:
                                            case 9:
                                                {

                                                    _medicinesProvider.UpdateMedicineListPPX3((int)xoa.MaDV, xoa.DonGia, xoa.SoLo, (DateTime)xoa.HanDung, (int)xoac.MaKPnx, (int)xoac.MaKP, -xoa.SoLuongX, 2);
                                                }
                                                break;
                                        }
                                    }
                                }
                                int iddon = xoac.SoPL ?? 0;
                                int _mbn = 0;
                                if (lupMaBNhan.EditValue != null && lupMaBNhan.EditValue != "")
                                    _mbn = Convert.ToInt32(lupMaBNhan.EditValue);
                                var dthuocs = _dataContext.DThuocs.Where(p => p.MaBNhan == (_mbn)).ToList();
                                // update lại trạng thái xuất dược
                                int hinhthuc = -1;
                                if (lupHinhthucx.EditValue != null)
                                {
                                    hinhthuc = Convert.ToInt32(lupHinhthucx.EditValue);
                                }
                                if (Int32.TryParse(txtSoCT.Text, out _sopl))
                                {
                                    var ktpl = (from dt in _dataContext.DThuocs
                                                join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _sopl) on dt.IDDon equals dtct.IDDon
                                                select dt).ToList();
                                    if (ktpl.Count > 0)
                                    {

                                        if (hinhthuc == 1 || hinhthuc == 5 || hinhthuc == 6)
                                        {
                                            var iddt = (from dt in _dataContext.DThuocs
                                                        join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == _sopl) on dt.IDDon equals dtct.IDDon
                                                        select dt).ToList();
                                            foreach (var i in iddt)
                                            {
                                                var dtct = _dataContext.DThuoccts.Where(p => p.IDDon == i.IDDon).ToList();
                                                foreach (var a in dtct)
                                                {
                                                    if (a.Status != -1)
                                                        a.Status = 0;
                                                }
                                                _dataContext.SaveChanges();
                                            }

                                        }
                                    }
                                }

                                if (listDichVus.Count > 0 && DungChung.Bien.MaBV == "200012") // tét
                                {
                                    DungChung.Ham.UpdateTonDichVu(listDichVus, xoac.MaKP ?? 0, true);
                                }
                                if (hinhthuc == 0)
                                {
                                    int _mabn = 0;
                                    if (!string.IsNullOrEmpty(lupMaBNhan.Text))
                                        _mabn = Convert.ToInt32(lupMaBNhan.EditValue);
                                }
                                LoadDuoc();
                                //
                                TimKiem();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có chứng từ để xóa!");
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // luu bang NhapD
            int makpnx = 0;
            if (lupMaKPnx.EditValue != null)
                makpnx = Convert.ToInt32(lupMaKPnx.EditValue);
            int idnhap = 0;
            bool luutt = true;
            if (KT() && DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayNhap.DateTime, "KhoaDC") == false)

                switch (TTLuu)
                {

                    case 1:
                        string thuockluu = "các thuốc không được lưu:\n";
                        int _ttthuockluu = 0;
                        NhapD nhap = new NhapD();
                        nhap.PLoai = 2;
                        nhap.NgayNhap = dtNgayNhap.DateTime;
                        nhap.TenNguoiCC = txtNguoiNhan.Text;
                        nhap.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);

                        nhap.KieuDon = Convert.ToInt32(lupHinhthucx.EditValue);
                        nhap.MaKPnx = makpnx;
                        if (lupMaBNhan.EditValue != null && lupMaBNhan.EditValue != "")
                            nhap.MaBNhan = Convert.ToInt32(lupMaBNhan.EditValue);
                        else
                            nhap.MaBNhan = 0;
                        if (lupMaXP.EditValue != null && lupMaXP.EditValue.ToString() != "")
                            nhap.MaXP = lupMaXP.EditValue.ToString();
                        nhap.GhiChu = txtGhiChu.Text;
                        if (!string.IsNullOrEmpty(txtSoCT.Text))
                            nhap.SoCT = txtSoCT.Text;
                        else
                            nhap.SoCT = "";
                        nhap.MaCB = DungChung.Bien.MaCB;
                        nhap.DiaChi = txtDiaChi.Text.Trim();
                        nhap.SoPhieu = DungChung.Ham._idphieutheokp(2, Convert.ToInt32(lupMaKP.EditValue));
                        if (!string.IsNullOrEmpty(cboMien.Text))
                            nhap.Mien = Convert.ToInt32(cboMien.Text);
                        _dataContext.NhapDs.Add(nhap);

                        List<DichVu> listDichVus = new List<DichVu>();
                        if (_dataContext.SaveChanges() >= 0)
                        {

                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD
                            idnhap = nhap.IDNhap;

                            for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                            {
                                if ((ppxuat == 1 && grvNhapCT.GetRowCellValue(i, colMaDV) != null) || (ppxuat == 3 && grvNhapCT.GetRowCellValue(i, colIDThuoc) != null))
                                {
                                    if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "")
                                    {

                                        //DungChung.Ham.CheckBangTonDuoc(_dataContext, Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV)), lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue), double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString()));
                                        NhapDct nhapdct = new NhapDct();
                                        nhapdct.IDNhap = idnhap;
                                        if (grvNhapCT.GetRowCellValue(i, colIDThuoc) != null)
                                        {
                                            nhapdct.MaDV = maDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDThuoc)));
                                        }
                                        nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                        nhapdct.DonGia = donGia = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colDonGia));
                                        nhapdct.SoLuongX = slKe = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuong));
                                        nhapdct.ThanhTienX = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colThanhTien));

                                        nhapdct.SoLuongDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSLuongDY));
                                        nhapdct.ThanhTienDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colThanhTienDY));
                                        nhapdct.MienCT = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMien));
                                        nhapdct.SoLuongN = 0;
                                        nhapdct.ThanhTienN = 0;
                                        nhapdct.SoLuongSD = 0;
                                        nhapdct.ThanhTienSD = 0;
                                        nhapdct.SoLuongKK = 0;
                                        nhapdct.ThanhTienKK = 0;
                                        if (grvNhapCT.GetRowCellValue(i, colMaCC) != null && grvNhapCT.GetRowCellValue(i, colMaCC).ToString() != "")
                                            nhapdct.MaCC = grvNhapCT.GetRowCellValue(i, colMaCC).ToString();
                                        else
                                            nhapdct.MaCC = "";
                                        if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                            nhapdct.SoLo = soLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                        if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                            nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                        if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                            nhapdct.HanDung = hanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                        if (grvNhapCT.GetRowCellValue(i, colIDDTBN) != null && grvNhapCT.GetRowCellValue(i, colIDDTBN).ToString() != "")
                                            nhapdct.IDDTBN = Convert.ToByte(grvNhapCT.GetRowCellValue(i, colIDDTBN));
                                        else
                                            nhapdct.IDDTBN = 0;
                                        _dataContext.NhapDcts.Add(nhapdct);
                                        _dataContext.SaveChanges();
                                        listDichVus.Add(new DichVu { MaDV = nhapdct.MaDV ?? 0, DonGia = nhapdct.DonGia });


                                        //Cập nhật tồn bảng MedicineList
                                        if (lupHinhthucx.EditValue != null)
                                        {
                                            switch (lupHinhthucx.EditValue)
                                            {
                                                case 2:
                                                case 3:
                                                case 8:
                                                case 9:
                                                    {
                                                        int maKXuat = lupMaKP.EditValue != null ? Convert.ToInt32(lupMaKP.EditValue) : 0;
                                                        int maKNhan = lupMaKPnx.EditValue != null ? Convert.ToInt32(lupMaKPnx.EditValue) : 0;

                                                        if (maDV == 0)
                                                            maDV = grvNhapCT.GetRowCellValue(i, colIDThuoc) != null ? _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDThuoc))) : 0;

                                                        _medicinesProvider.UpdateMedicineListPPX3(maDV, donGia, soLo, (DateTime)hanDung, maKNhan, maKXuat, slKe, 2);

                                                        lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKXuat, idnhap, 0);
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        thuockluu += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                        _ttthuockluu = 1;
                                    }
                                }
                            }
                        }
                        if (_ttthuockluu == 1)
                            MessageBox.Show(thuockluu);
                        if (luutt)
                        {
                            var kt = _dataContext.KPhongs.Where(p => p.MaKP == makpnx && p.TrongBV == 0 && p.TuTruc).ToList();
                            if (kt.Count > 0)
                                Frm_NhanXuat.nhanCTXuat(_dataContext, _medicinesProvider, idnhap, dtNgayNhap.DateTime, "", 0, 2);
                            if (DungChung.Bien.MaBV == "200012" && listDichVus.Count > 0)
                            {
                                DungChung.Ham.UpdateTonDichVu(listDichVus, nhap.MaKP ?? 0);
                            }
                            Enablebutton(true);
                            EnableControl(false);
                            TTLuu = 0;
                            Frm_XuatDuoc_Load(sender, e);
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        {
                            string thuockluus = "các thuốc không được lưu:\n";
                            int _ttthuockluus = 0;
                            int id = Convert.ToInt32(txtIDNhap.Text);
                            NhapD nhaps = _dataContext.NhapDs.Single(p => p.IDNhap == id);
                            nhaps.NgayNhap = dtNgayNhap.DateTime;
                            nhaps.TenNguoiCC = txtNguoiNhan.Text;
                            nhaps.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
                            nhaps.KieuDon = Convert.ToInt32(lupHinhthucx.EditValue);
                            if (lupMaKPnx.EditValue != null)
                                nhaps.MaKPnx = Convert.ToInt32(lupMaKPnx.EditValue);
                            if (lupMaBNhan.EditValue != null && lupMaBNhan.EditValue != "")
                                nhaps.MaBNhan = Convert.ToInt32(lupMaBNhan.EditValue);
                            if (lupMaXP.EditValue != null && lupMaXP.EditValue.ToString() != "")
                                nhaps.MaXP = lupMaXP.EditValue.ToString();
                            nhaps.GhiChu = txtGhiChu.Text;
                            if (!string.IsNullOrEmpty(txtSoCT.Text))
                                nhaps.SoCT = txtSoCT.Text;

                            nhaps.DiaChi = txtDiaChi.Text.Trim();
                            nhaps.SoPhieu = txtsophieunew.Text;
                            if (!string.IsNullOrEmpty(cboMien.Text))
                                nhaps.Mien = Convert.ToInt32(cboMien.Text);
                            _dataContext.SaveChanges();
                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD
                            List<DichVu> listDichVu1s = new List<DichVu>();
                            for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                            {
                                if ((ppxuat == 1 && grvNhapCT.GetRowCellValue(i, colMaDV) != null && grvNhapCT.GetRowCellValue(i, colMaDV).ToString() != "") || (ppxuat == 3 && grvNhapCT.GetRowCellValue(i, colIDThuoc) != null && grvNhapCT.GetRowCellValue(i, colIDThuoc).ToString() != ""))
                                {
                                    if (grvNhapCT.GetRowCellValue(i, colDonGia) != null && grvNhapCT.GetRowCellValue(i, colDonGia).ToString() != "")
                                    {
                                        if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "")
                                        {
                                            int idct = 0;
                                            if (grvNhapCT.GetRowCellValue(i, colIDNhapct) != null && grvNhapCT.GetRowCellValue(i, colIDNhapct).ToString() != "")
                                            {
                                                idct = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDNhapct).ToString());
                                                if (idct <= 0) // them row moi
                                                {
                                                    NhapDct nhapdct = new NhapDct();
                                                    nhapdct.IDNhap = id;
                                                    if (grvNhapCT.GetRowCellValue(i, colIDThuoc) != null)
                                                    {
                                                        nhapdct.MaDV = maDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDThuoc)));
                                                    }
                                                    nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdct.DonGia = donGia = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colDonGia));
                                                    nhapdct.SoLuongX = slKe = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuong));
                                                    nhapdct.ThanhTienX = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colThanhTien));
                                                    nhapdct.SoLuongDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSLuongDY));
                                                    nhapdct.ThanhTienDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colThanhTienDY));
                                                    nhapdct.MienCT = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMien));
                                                    nhapdct.SoLuongN = 0;
                                                    nhapdct.ThanhTienN = 0;
                                                    nhapdct.SoLuongSD = 0;
                                                    nhapdct.ThanhTienSD = 0;
                                                    nhapdct.SoLuongKK = 0;
                                                    nhapdct.ThanhTienKK = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colMaCC) != null && grvNhapCT.GetRowCellValue(i, colMaCC).ToString() != "")
                                                        nhapdct.MaCC = grvNhapCT.GetRowCellValue(i, colMaCC).ToString();
                                                    else
                                                        nhapdct.MaCC = "";
                                                    if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                        nhapdct.SoLo = soLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdct.HanDung = hanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                    if (grvNhapCT.GetRowCellValue(i, colIDDTBN) != null && grvNhapCT.GetRowCellValue(i, colIDDTBN).ToString() != "")
                                                        nhapdct.IDDTBN = Convert.ToByte(grvNhapCT.GetRowCellValue(i, colIDDTBN));
                                                    else
                                                        nhapdct.IDDTBN = 0;
                                                    _dataContext.NhapDcts.Add(nhapdct);
                                                    _dataContext.SaveChanges();

                                                    //Cập nhật tồn bảng MedicineList
                                                    //if (lupHinhthucx.EditValue != null)
                                                    //{
                                                    //    switch (lupHinhthucx.EditValue)
                                                    //    {
                                                    //        case 2:
                                                    //        case 3:
                                                    //        case 8:
                                                    //        case 9:
                                                    //            {
                                                    //                int maKXuat = lupMaKP.EditValue != null ? Convert.ToInt32(lupMaKP.EditValue) : 0;
                                                    //                int maKNhan = lupMaKPnx.EditValue != null ? Convert.ToInt32(lupMaKPnx.EditValue) : 0;

                                                    //                if (maDV == 0)
                                                    //                    maDV = grvNhapCT.GetRowCellValue(i, colIDThuoc) != null ? _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDThuoc))) : 0;

                                                    //                _medicinesProvider.UpdateMedicineListPPX3(maDV, donGia, soLo, (DateTime)hanDung, maKNhan, maKXuat, slKe, 2);

                                                    //                lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKXuat, idnhap, 0);
                                                    //            }
                                                    //            break;
                                                    //    }
                                                    //}
                                                }
                                                else
                                                {
                                                    NhapDct nhapdcts = _dataContext.NhapDcts.Single(p => p.IDNhapct == idct);

                                                    double oldSL = nhapdcts.SoLuongX;

                                                    if (grvNhapCT.GetRowCellValue(i, colIDThuoc) != null)
                                                    {
                                                        nhapdcts.MaDV = maDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDThuoc)));
                                                    }
                                                    nhapdcts.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdcts.DonGia = donGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdcts.SoLuongX = slKe = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                    nhapdcts.ThanhTienX = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                    nhapdcts.SoLuongDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSLuongDY));
                                                    nhapdcts.ThanhTienDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colThanhTienDY));
                                                    if (grvNhapCT.GetRowCellValue(i, colMaCC) != null && grvNhapCT.GetRowCellValue(i, colMaCC).ToString() != "")
                                                        nhapdcts.MaCC = grvNhapCT.GetRowCellValue(i, colMaCC).ToString();
                                                    else
                                                        nhapdcts.MaCC = "";
                                                    if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                        nhapdcts.SoLo = soLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdcts.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdcts.HanDung = hanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                    if (grvNhapCT.GetRowCellValue(i, colIDDTBN) != null && grvNhapCT.GetRowCellValue(i, colIDDTBN).ToString() != "")
                                                        nhapdcts.IDDTBN = Convert.ToByte(grvNhapCT.GetRowCellValue(i, colIDDTBN));
                                                    nhapdcts.SoLuongN = 0;
                                                    nhapdcts.ThanhTienN = 0;
                                                    nhapdcts.SoLuongSD = 0;
                                                    nhapdcts.ThanhTienSD = 0;
                                                    nhapdcts.SoLuongKK = 0;
                                                    nhapdcts.ThanhTienKK = 0;
                                                    _dataContext.SaveChanges();
                                                    listDichVu1s.Add(new DichVu { MaDV = nhapdcts.MaDV ?? 0 });

                                                }
                                            }
                                        }//
                                        else
                                        {
                                            thuockluus += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                            _ttthuockluus = 1;
                                        }
                                    }
                                    else
                                    {
                                        thuockluus += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                        _ttthuockluus = 1;
                                    }
                                }
                            }

                            //Cập nhật tồn bảng MedicineList
                            if (lupHinhthucx.EditValue != null)
                            {
                                switch (lupHinhthucx.EditValue)
                                {
                                    case 2:
                                    case 3:
                                    case 8:
                                    case 9:
                                        {
                                            int maKXuat = lupMaKP.EditValue != null ? Convert.ToInt32(lupMaKP.EditValue) : 0;
                                            int maKNhan = lupMaKPnx.EditValue != null ? Convert.ToInt32(lupMaKPnx.EditValue) : 0;

                                            foreach (var item in lupMaDuoc.DataSource as List<MedicineInventoryModel>)
                                            {
                                                if (item.TonKhaDung != item.TonHienTai)
                                                {
                                                    _medicinesProvider.UpdateMedicineListPPX3((int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, maKNhan, maKXuat, item.TonKhaDung - item.TonHienTai, 2);
                                                }
                                            }
                                        }
                                        break;
                                }
                            }

                            //Xóa đơn
                            if (deleteNhapdcts.Count() > 0)
                            {
                                foreach (var item in deleteNhapdcts)
                                {
                                    var a = _dataContext.NhapDcts.FirstOrDefault(p => p.IDNhapct == item);
                                    if (a != null)
                                    {
                                        var nhapD = _dataContext.NhapDs.FirstOrDefault(p => p.IDNhap == (int)a.IDNhap);
                                        //switch (lupHinhthucx.EditValue)
                                        //{
                                        //    case 2:
                                        //    case 3:
                                        //    case 8:
                                        //    case 9:
                                        //        {
                                        //            int maKXuat = lupMaKP.EditValue != null ? Convert.ToInt32(lupMaKP.EditValue) : 0;
                                        //            int maKNhan = lupMaKPnx.EditValue != null ? Convert.ToInt32(lupMaKPnx.EditValue) : 0;


                                        //            _medicinesProvider.UpdateMedicineListPPX3((int)a.MaDV, a.DonGia, a.SoLo, (DateTime)a.HanDung, maKNhan, maKXuat, -a.SoLuongX, 2);
                                        //        }
                                        //        break;
                                        //}
                                    }
                                    _medicinesProvider.DeleteNhapDAndNhapDctbyIDNhapct(item);
                                }
                                deleteNhapdcts.Clear();
                            }
                            if (luutt)
                            {
                                if (_ttthuockluus == 1)
                                    MessageBox.Show(thuockluus);
                                //if (DungChung.Bien.MaBV == "200012" && listDichVu1s.Count > 0) // tét
                                //{
                                //    DungChung.Ham.UpdateTonDichVu(listDichVu1s, nhaps.MaKP ?? 0);
                                //}
                                Enablebutton(true);
                                EnableControl(false);
                                TTLuu = 0;
                                LoadDuoc();
                                grvNhap_FocusedRowChanged(null, null);
                            }
                        }

                        break;

                }
        }

        private void lupHinhthucx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lupHinhthucx.EditValue != null)
                hinhthucxuat(Convert.ToInt32(lupHinhthucx.EditValue));
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimTuNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimDenNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimMaKPnx_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void grvNhap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            TTLuu = 0;
            int id = 0;
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString() != "")
            {
                txtIDNhap.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();
                id = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));
                var nhapd = _dataContext.NhapDs.Where(p => p.IDNhap == id).ToList();
                txtSoCT.Text = nhapd.First().SoCT;
                lupMaKP.EditValue = nhapd.First().MaKP;
                lupMaKPnx.EditValue = nhapd.First().MaKPnx;
                var a = lupMaKP.Properties.DataSource;
                var b = lupMaKPnx.Properties.DataSource;

                lupMaXP.EditValue = nhapd.First().MaXP;
                lupMaBNhan.EditValue = nhapd.First().MaBNhan;
                txtsophieunew.Text = nhapd.First().SoPhieu;
                txtGhiChu.Text = nhapd.First().GhiChu;
                txtNguoiNhan.Text = nhapd.First().TenNguoiCC;
                dtNgayNhap.DateTime = nhapd.First().NgayNhap.Value;
                txtDiaChi.Text = nhapd.First().DiaChi;
                int kieudon = -1;
                if (nhapd.First().KieuDon != null && nhapd.First().KieuDon.ToString() != "")
                {
                    kieudon = nhapd.First().KieuDon.Value;

                    lupHinhthucx.EditValue = kieudon;
                    if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023")
                    {
                        if (kieudon == 0)
                        {
                            int? mabnhan = nhapd.First().MaBNhan;
                            if (mabnhan != null)
                            {
                                var qbn = _dataContext.BenhNhans.Where(p => p.MaBNhan == mabnhan).FirstOrDefault();
                                if (qbn != null)
                                {
                                    txtNguoiNhan.Text = qbn.TenBNhan;
                                    txtDiaChi.Text = qbn.DChi;
                                }
                            }
                        }
                        else if (kieudon == 1)
                        {
                            int makp = nhapd.First().MaKPnx ?? 0;
                            txtDiaChi.Text = _dataContext.KPhongs.Where(p => p.MaKP == makp).FirstOrDefault() == null ? "" : _dataContext.KPhongs.Where(p => p.MaKP == makp).FirstOrDefault().TenKP;
                            int SoPL = nhapd.First().SoPL ?? -2;
                            var qdtct = (from dtct in _dataContext.DThuoccts.Where(p => p.SoPL == SoPL)
                                         join dt in _dataContext.DThuocs on dtct.IDDon equals dt.IDDon
                                         join ad in _dataContext.ADMINs on dt.MaCB equals ad.MaCB
                                         join htus in _dataContext.HThong_User on ad.TenDN equals htus.TenDN
                                         select htus).FirstOrDefault();
                            if (qdtct != null)
                            {
                                txtNguoiNhan.Text = qdtct.NguoiLapBieu;
                            }
                            else
                                txtNguoiNhan.Text = "";
                        }

                    }

                }

                if (nhapd.First().Status != null)
                {
                    txtStatus.Text = nhapd.First().Status.ToString();
                    status = nhapd.First().Status.Value;
                }
                else
                {
                    txtStatus.Text = "";
                    status = 0;
                }

                cboMien.Text = nhapd.First().Mien.ToString();

                if (TTLuu == 1 || TTLuu == 2)
                {
                    lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;
                }
                else
                {
                    lupMaDuoc.DataSource = lupIDThuoc.DataSource = allMedicines;
                }

                _lNhapDct = _medicinesProvider.ViewInfoMedicineNhapD(id);

                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
            else
            {
                ResetControl();

                _lNhapDct = _medicinesProvider.ViewInfoMedicineNhapD(id);
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
            if (status == 0)
                btnXML.Text = "Xuất XML";
            else
                btnXML.Text = "Hủy XML";
        }

        private void grvNhap_DataSourceChanged(object sender, EventArgs e)
        {
            grvNhap_FocusedRowChanged(null, null);
        }

        private void grvNhapCT_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int sta = 0;
            if (!string.IsNullOrEmpty(txtStatus.Text))
                sta = Convert.ToInt32(txtStatus.Text);
            if (sta == 1)
            {
                MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa");
            }
            else
            {
                if (e.Column.Name == "colXoaCT" && (TTLuu == 1 || TTLuu == 2))
                {
                    int madv = grvNhapCT.GetFocusedRowCellValue(colMaDV) != null ? Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV)) : 0;
                    int idthuoc = grvNhapCT.GetFocusedRowCellValue(colIDThuoc) != null ? Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDThuoc)) : 0;
                    double dongia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                    double soluong = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));

                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                    {
                        kieuDon = Convert.ToInt32(lupHinhthucx.EditValue);
                        int id = Int32.Parse(txtIDNhap.Text);
                        var kt = _dataContext.NhapDs.Where(p => p.IDNhap == id).ToList();
                        if ((kt.Count > 0 && kt.First().Status != 1) || kieuDon != 2)
                        {
                            if (kt.First().XuatTD != null && kt.First().XuatTD > 0)
                            {
                                MessageBox.Show("Chứng từ xuất bằng chức năng, Bạn không thể xóa");
                            }
                            else
                            {

                                if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null && grvNhapCT.GetFocusedRowCellValue(colIDNhapct).ToString() != "")
                                {
                                    if (ppxuat == 1 ? grvNhapCT.GetFocusedRowCellDisplayText(colMaDV) != null && grvNhapCT.GetFocusedRowCellDisplayText(colMaDV).ToString() != "" : grvNhapCT.GetFocusedRowCellDisplayText(colIDThuoc) != null && grvNhapCT.GetFocusedRowCellDisplayText(colIDThuoc).ToString() != "")
                                    {
                                        string tenthuoc = grvNhapCT.GetFocusedRowCellDisplayText(colMaDV).ToString();
                                        if (MessageBox.Show("Bạn muốn xóa thuốc: " + tenthuoc, "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            int idct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));
                                            if (idct > 0)
                                            {
                                                if (ppxuat == 1)
                                                {
                                                    _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, soluong, 0, madv, 0, donGia, 0, ppxuat);
                                                }
                                                else if (ppxuat == 3)
                                                {
                                                    _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, soluong, 0, idthuoc, 0, donGia, 0, ppxuat);
                                                }

                                                deleteNhapdcts.Add(idct);
                                                grvNhapCT.DeleteSelectedRows();
                                            }
                                            else
                                            {
                                                if (ppxuat == 1)
                                                {
                                                    _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, soluong, 0, madv, 0, donGia, 0, ppxuat);
                                                }
                                                else if (ppxuat == 3)
                                                {
                                                    _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, soluong, 0, idthuoc, 0, donGia, 0, ppxuat);
                                                }
                                                grvNhapCT.DeleteSelectedRows();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa!");
                        }
                    }
                    else
                    {
                        if (ppxuat == 1)
                        {
                            _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, soluong, 0, madv, 0, donGia, 0, ppxuat);
                        }
                        else if (ppxuat == 3)
                        {
                            _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, soluong, 0, idthuoc, 0, donGia, 0, ppxuat);
                        }
                        grvNhapCT.DeleteSelectedRows();
                    }
                }


            }
        }
        #region InPhieuXuat
        class PXK14018
        {
            public string TenDuoc { get; set; }
            public string MaSo { get; set; }
            public string DonViTinh { get; set; }
            public string HamLuong { get; set; }
            public string ThucXuat { get; set; }
            public string DonGia { get; set; }
            public string ThanhTien { get; set; }
        }
        public bool InPhieuXuat(int id)
        {
            int _makpnx = 0, _mabn = 0;
            string _nguoinhan = "";
            var tenkp = lupTimMaKP.Text;
            var macq = _dataContext.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (DungChung.Bien.MaBV == "30010")
            {
                #region phiếu xuất BV 30010
                BaoCao.repPhieuXuat_30010 rep = new BaoCao.repPhieuXuat_30010();
                var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                           join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                           select new { nd.DiaChi, kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan }).ToList();
                if (par.Count > 0)
                {
                    rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    _nguoinhan = par.First().TenNguoiCC;
                    if (par.First().MaKPnx != null)
                        _makpnx = par.First().MaKPnx == null ? 0 : par.First().MaKPnx.Value;
                    if (par.First().MaBNhan != null)
                        _mabn = par.First().MaBNhan == null ? 0 : par.First().MaBNhan.Value;
                    rep.Noidung.Value = par.First().GhiChu;
                    rep.Khoxuat.Value = par.First().TenKP;
                }
                var noinhan = _dataContext.KPhongs.Where(p => p.MaKP == _makpnx).Select(p => p.TenKP).FirstOrDefault();
                if (noinhan != null)
                    rep.NoiNhan.Value = noinhan;

                rep.Diachinguoinhan.Value = par.First().DiaChi;
                if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "26007")
                    rep.NoiNhan.Value = par.First().TenNguoiCC;
                var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                if (bn != null)
                    _nguoinhan += bn.TenBNhan;
                rep.Nguoinhanhang.Value = _nguoinhan;
                var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                         join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                         join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                         select new
                         {
                             ndx.IDNhapct,
                             nd.SoCT,
                             ndx.MaDV,
                             ndx.HanDung,
                             ndx.SoLo,
                             dv.MaTam,
                             TenDV = DungChung.Bien.MaBV == "26007" ? (dv.TenDV + " " + dv.HamLuong) : dv.TenDV,
                             ndx.DonVi,
                             dv.SoDK,
                             ndx.DonGia,
                             ndx.SoLuongX,
                             ndx.ThanhTienX
                         }).OrderBy(p => p.IDNhapct).ToList();
                double TT = 0; string _soct = "";
                if (q.Count > 0)
                {
                    TT = q.Sum(p => p.ThanhTienX);
                    _soct = q.First().SoCT;
                }
                rep.Soct.Value = _soct;
                rep.DataSource = q;
                rep.TongTien.Value = TT;
                #region xuat Excel

                string[] _arr = new string[] { "0", "@", "@", "@", "@", "0", "0", "0", "0" };
                string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Đơn vị tính", "Số lô", "Hạn dùng", "SL theo chứng từ", "SL thực nhập", "Đơn giá", "Thành tiền" };
                int[] _arrWidth = new int[] { };
                DungChung.Bien.MangHaiChieu = new Object[q.Count + 20, 10];
                DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQ.ToUpper(); ;
                DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.DiaChi;
                DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C21-HD";
                DungChung.Bien.MangHaiChieu[1, 5] = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của  BTC)";
                DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu xuất kho").ToUpper();
                DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                DungChung.Bien.MangHaiChieu[5, 4] = "Số: " + _soct;
                DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
                DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
                DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người nhận hàng: " + par.First().TenNguoiCC;
                DungChung.Bien.MangHaiChieu[7, 0] = "Địa chỉ: ";
                DungChung.Bien.MangHaiChieu[8, 0] = "Nội dung: ";
                DungChung.Bien.MangHaiChieu[9, 0] = "Xuất tại kho: " + par.First().TenKP;
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
                }
                int num = 12;
                DungChung.Bien.MangHaiChieu[num + q.Count, 6] = "Tổng cộng:";
                DungChung.Bien.MangHaiChieu[num + q.Count(), 7] = TT;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(TT, " đồng!"); ;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 6] = "Xuất, ngày ..... tháng ..... năm ...... ";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập phiếu";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 7] = "Thủ trưởng đơn vị";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 7] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 7] = DungChung.Bien.GiamDoc;
                if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
                {
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Thống kê dược";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = "";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Trưởng khoa dược";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = "";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ trưởng đơn vị";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.GiamDoc;
                }

                foreach (var r in q)
                {

                    DungChung.Bien.MangHaiChieu[num, 0] = num - 11;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.DonVi;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.SoLo;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.HanDung;
                    DungChung.Bien.MangHaiChieu[num, 5] = ""; ;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.SoLuongX;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.ThanhTienX;

                    num++;

                }


                #endregion
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu xuất kho", "C:\\PhieuXuatKho.xls", true, this.Name);
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                #endregion
            }
            else
            {
                //if (DungChung.Bien.MaBV == "12001" && tenkp != null && tenkp != null && tenkp.Trim().ToLower().Contains("tiêm chủng"))
                //{

                //    #region phiếu xuất BV Tam Duong kho tiêm chủng
                //    BaoCao.repPhieuXuat_TuyenXa12001 rep = new BaoCao.repPhieuXuat_TuyenXa12001();
                //    var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                //               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                //               select new { nd.DiaChi, kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan, nd.SoPL }).ToList();
                //    if (par.Count > 0)
                //    {
                //        //rep.Soct.Value = id;
                //        rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                //        _nguoinhan = par.First().TenNguoiCC;
                //        if (par.First().MaKPnx != null)
                //            _makpnx = par.First().MaKPnx == null ? 0 : par.First().MaKPnx.Value;
                //        if (par.First().MaBNhan != null)
                //            _mabn = par.First().MaBNhan == null ? 0 : par.First().MaBNhan.Value;
                //        rep.Noidung.Value = par.First().GhiChu;
                //        rep.Khoxuat.Value = par.First().TenKP;
                //    }




                //    //var q = from xd in _dataContext.XuatDs join xdct in _dataContext.XuatDcts on xd.IDXuat equals xdct.IDXuat where(xd.IDXuat == id) join dv in _dataContext.DichVus on 
                //    var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                //             join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                //             join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                //             select new
                //             {
                //                 ndx.IDNhapct,
                //                 nd.SoCT,
                //                 ndx.MaDV,
                //                 dv.MaTam,
                //                 TenDV = DungChung.Bien.MaBV == "26007" ? (dv.TenDV + " " + dv.HamLuong) : dv.TenDV,
                //                 ndx.SoLo,
                //                 ndx.HanDung,
                //                 dv.NhaSX,
                //                 ndx.DonVi,
                //                 dv.SoDK,
                //                 ndx.DonGia,
                //                 ndx.SoLuongX,
                //                 ndx.ThanhTienX
                //             }).OrderBy(p => p.IDNhapct).ToList();


                //    double TT = 0; string _soct = "";
                //    if (q.Count > 0)
                //    {
                //        TT = q.Sum(p => p.ThanhTienX);
                //        _soct = q.First().SoCT;
                //    }
                //    rep.Soct.Value = _soct;
                //    rep.DataSource = q;
                //    rep.TongTien.Value = TT;

                //    var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                //    if (bn != null)
                //        _nguoinhan += bn.TenBNhan;
                //    rep.Nguoinhanhang.Value = _nguoinhan;

                //    var noinhan = _dataContext.KPhongs.Where(p => p.MaKP == _makpnx).Select(p => p.TenKP).FirstOrDefault();
                //    if (noinhan != null)
                //        rep.NoiNhan.Value = noinhan;
                //    if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "26007")
                //        rep.NoiNhan.Value = par.First().TenNguoiCC;
                //    rep.Diachinguoinhan.Value = par.First().DiaChi;
                //    if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023")
                //    {
                //        if (lupHinhthucx.EditValue != null && (Convert.ToInt32(lupHinhthucx.EditValue) == 0 || Convert.ToInt32(lupHinhthucx.EditValue) == 1))
                //        {
                //            rep.NoiNhan.Value = txtNguoiNhan.Text;
                //            rep.Nguoinhanhang.Value = txtNguoiNhan.Text;
                //            rep.Diachinguoinhan.Value = txtDiaChi.Text;

                //        }
                //    }


                //    #region xuat Excel

                //    string[] _arr = new string[] { "0", "@", "@", "@", "0", "0", "0", "0" };
                //    string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Mã số", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá", "Thành tiền" };
                //    int[] _arrWidth = new int[] { };
                //    DungChung.Bien.MangHaiChieu = new Object[q.Count + 20, 10];
                //    DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQ.ToUpper(); ;
                //    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.DiaChi;
                //    DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C21-HD";
                //    DungChung.Bien.MangHaiChieu[1, 5] = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của  BTC)";
                //    DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu xuất kho").ToUpper();
                //    DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                //    DungChung.Bien.MangHaiChieu[5, 4] = "Số: " + _soct;
                //    DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
                //    DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
                //    DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người nhận hàng: " + par.First().TenNguoiCC;
                //    DungChung.Bien.MangHaiChieu[7, 0] = "Địa chỉ: ";
                //    DungChung.Bien.MangHaiChieu[8, 0] = "Nội dung: ";
                //    DungChung.Bien.MangHaiChieu[9, 0] = "Xuất tại kho: " + par.First().TenKP;
                //    for (int i = 0; i < _tieude.Length; i++)
                //    {
                //        DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
                //    }
                //    int num = 12;
                //    DungChung.Bien.MangHaiChieu[num + q.Count, 6] = "Tổng cộng:";
                //    DungChung.Bien.MangHaiChieu[num + q.Count(), 7] = TT;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(TT, " đồng!"); ;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 6] = "Xuất, ngày ..... tháng ..... năm ...... ";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập phiếu";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 7] = "Thủ trưởng đơn vị";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 7] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 7] = DungChung.Bien.GiamDoc;
                //    if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
                //    {
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Thống kê dược";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = "";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Trưởng khoa dược";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = "";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ trưởng đơn vị";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.GiamDoc;
                //    }

                //    foreach (var r in q)
                //    {

                //        DungChung.Bien.MangHaiChieu[num, 0] = num - 11;
                //        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                //        DungChung.Bien.MangHaiChieu[num, 2] = r.MaTam;
                //        DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                //        DungChung.Bien.MangHaiChieu[num, 4] = ""; ;
                //        DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongX;
                //        DungChung.Bien.MangHaiChieu[num, 6] = r.DonGia;
                //        DungChung.Bien.MangHaiChieu[num, 7] = r.ThanhTienX;

                //        num++;

                //    }


                //    #endregion
                //    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu xuất kho", "C:\\PhieuXuatKho.xls", true, this.Name);
                //    rep.BindingData();
                //    rep.CreateDocument();
                //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //    frm.ShowDialog();
                //    #endregion
                //}
                #region mẫu kho chương trình: bỏ
                //else if (DungChung.Bien.MaBV == "12001" && tenkp != null && tenkp != null && tenkp.ToLower().Contains("chương trình"))
                //{
                //    #region phiếu xuất BV Tam Duong kho chương trình khác
                //    BaoCao.repPhieuXuat_TuyenXa12001_KhoCT rep = new BaoCao.repPhieuXuat_TuyenXa12001_KhoCT();
                //    var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                //               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                //               select new { nd.DiaChi, kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan, nd.SoPL }).ToList();
                //    if (par.Count > 0)
                //    {
                //        //rep.Soct.Value = id;
                //        rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                //        _nguoinhan = par.First().TenNguoiCC;
                //        if (par.First().MaKPnx != null)
                //            _makpnx = par.First().MaKPnx == null ? 0 : par.First().MaKPnx.Value;
                //        if (par.First().MaBNhan != null)
                //            _mabn = par.First().MaBNhan == null ? 0 : par.First().MaBNhan.Value;
                //        rep.Noidung.Value = par.First().GhiChu;
                //        rep.Khoxuat.Value = par.First().TenKP;
                //    }




                //    //var q = from xd in _dataContext.XuatDs join xdct in _dataContext.XuatDcts on xd.IDXuat equals xdct.IDXuat where(xd.IDXuat == id) join dv in _dataContext.DichVus on 
                //    var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                //             join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                //             join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                //             select new
                //             {
                //                 ndx.IDNhapct,
                //                 nd.SoCT,
                //                 ndx.MaDV,
                //                 dv.MaTam,
                //                 TenDV = DungChung.Bien.MaBV == "26007" ? (dv.TenDV + " " + dv.HamLuong) : dv.TenDV,
                //                 ndx.SoLo,
                //                 ndx.HanDung,
                //                 dv.NhaSX,
                //                 ndx.DonVi,
                //                 dv.SoDK,
                //                 ndx.DonGia,
                //                 ndx.SoLuongX,
                //                 ndx.ThanhTienX
                //             }).OrderBy(p => p.IDNhapct).ToList();


                //    double TT = 0; string _soct = "";
                //    if (q.Count > 0)
                //    {
                //        TT = q.Sum(p => p.ThanhTienX);
                //        _soct = q.First().SoCT;
                //    }
                //    rep.Soct.Value = _soct;
                //    rep.DataSource = q;
                //    rep.TongTien.Value = TT;

                //    var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                //    if (bn != null)
                //        _nguoinhan += bn.TenBNhan;
                //    rep.Nguoinhanhang.Value = _nguoinhan;

                //    var noinhan = _dataContext.KPhongs.Where(p => p.MaKP == _makpnx).Select(p => p.TenKP).FirstOrDefault();
                //    if (noinhan != null)
                //        rep.NoiNhan.Value = noinhan;
                //    if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "26007")
                //        rep.NoiNhan.Value = par.First().TenNguoiCC;
                //    rep.Diachinguoinhan.Value = par.First().DiaChi;
                //    if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023")
                //    {
                //        if (lupHinhthucx.EditValue != null && (Convert.ToInt32(lupHinhthucx.EditValue) == 0 || Convert.ToInt32(lupHinhthucx.EditValue) == 1))
                //        {
                //            rep.NoiNhan.Value = txtNguoiNhan.Text;
                //            rep.Nguoinhanhang.Value = txtNguoiNhan.Text;
                //            rep.Diachinguoinhan.Value = txtDiaChi.Text;

                //        }
                //    }


                //    #region xuat Excel

                //    string[] _arr = new string[] { "0", "@", "@", "@", "0", "0", "0", "0" };
                //    string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Mã số", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá", "Thành tiền" };
                //    int[] _arrWidth = new int[] { };
                //    DungChung.Bien.MangHaiChieu = new Object[q.Count + 20, 10];
                //    DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQ.ToUpper(); ;
                //    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.DiaChi;
                //    DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C21-HD";
                //    DungChung.Bien.MangHaiChieu[1, 5] = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của  BTC)";
                //    DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu xuất kho").ToUpper();
                //    DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                //    DungChung.Bien.MangHaiChieu[5, 4] = "Số: " + _soct;
                //    DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
                //    DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
                //    DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người nhận hàng: " + par.First().TenNguoiCC;
                //    DungChung.Bien.MangHaiChieu[7, 0] = "Địa chỉ: ";
                //    DungChung.Bien.MangHaiChieu[8, 0] = "Nội dung: ";
                //    DungChung.Bien.MangHaiChieu[9, 0] = "Xuất tại kho: " + par.First().TenKP;
                //    for (int i = 0; i < _tieude.Length; i++)
                //    {
                //        DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
                //    }
                //    int num = 12;
                //    DungChung.Bien.MangHaiChieu[num + q.Count, 6] = "Tổng cộng:";
                //    DungChung.Bien.MangHaiChieu[num + q.Count(), 7] = TT;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(TT, " đồng!"); ;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 6] = "Xuất, ngày ..... tháng ..... năm ...... ";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập phiếu";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 7] = "Thủ trưởng đơn vị";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 7] = "(Ký, họ tên)";
                //    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 7] = DungChung.Bien.GiamDoc;
                //    if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
                //    {
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Thống kê dược";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = "";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Trưởng khoa dược";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = "";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ trưởng đơn vị";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                //        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.GiamDoc;
                //    }

                //    foreach (var r in q)
                //    {

                //        DungChung.Bien.MangHaiChieu[num, 0] = num - 11;
                //        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                //        DungChung.Bien.MangHaiChieu[num, 2] = r.MaTam;
                //        DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                //        DungChung.Bien.MangHaiChieu[num, 4] = ""; ;
                //        DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongX;
                //        DungChung.Bien.MangHaiChieu[num, 6] = r.DonGia;
                //        DungChung.Bien.MangHaiChieu[num, 7] = r.ThanhTienX;

                //        num++;

                //    }


                //    #endregion
                //    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu xuất kho", "C:\\PhieuXuatKho.xls", true, this.Name);
                //    rep.BindingData();
                //    rep.CreateDocument();
                //    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //    frm.ShowDialog();
                //    #endregion
                //}
                #endregion // bỏ -- bỏ
                if (DungChung.Bien.MaBV == "12001" || (macq != null && macq.MaChuQuan != null && macq.MaChuQuan.Trim() == "12001")) //namnt
                {
                    #region phiếu xuất BV 12001
                    List<FormNhap.usNhapDuoc.PhieuNhap_12001> _kq = new List<FormNhap.usNhapDuoc.PhieuNhap_12001>();
                    //var q = from xd in _dataContext.XuatDs join xdct in _dataContext.XuatDcts on xd.IDXuat equals xdct.IDXuat where(xd.IDXuat == id) join dv in _dataContext.DichVus on 
                    var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                             join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                             join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                             group new { nd, dv, ndx } by new { nd.SoCT, dv.MaDV, nd.SoPhieu, ndx.DonVi, dv.HamLuong, dv.MaTam, dv.TyLeSD, dv.DongY, ndx.IDNhapct, nd.KieuDon, dv.TenDV, dv.TenRG, ndx.SoLo, ndx.HanDung, ndx.DonGia, ndx.DonGiaCT, ndx.SoLuongX, ndx.ThanhTienX, ndx.DonGiaDY, ndx.ThanhTienDY, dv.MaQD, dv.NhaSX, dv.NuocSX, ndx.VAT } into kq
                             select new
                             {
                                 kq.Key.KieuDon,
                                 kq.Key.IDNhapct,
                                 kq.Key.SoCT,
                                 kq.Key.MaDV,
                                 kq.Key.MaTam,
                                 kq.Key.SoPhieu,
                                 HangSX = kq.Key.NhaSX == null ? kq.Key.NuocSX : (kq.Key.NhaSX + "-" + kq.Key.NuocSX),
                                 TenDV = (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "12122") ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV),
                                 kq.Key.DonVi,
                                 kq.Key.MaQD,
                                 kq.Key.SoLo,
                                 kq.Key.HanDung,
                                 DonGia = kq.Key.DonGia,
                                 kq.Key.SoLuongX,
                                 ThanhTienX = kq.Key.ThanhTienX
                             }).OrderBy(p => p.IDNhapct).ToList();
                    _kq = (from a in q
                           select new FormNhap.usNhapDuoc.PhieuNhap_12001
                           {
                               MaTam = a.MaTam,
                               IDNhapct = a.IDNhapct,
                               MaQD = a.MaQD,
                               HangSX = a.HangSX,
                               TenDuoc = a.TenDV,
                               SoLo = a.SoLo,
                               HanDung = a.HanDung,
                               DonViTinh = a.DonVi,
                               SoLuongN = a.SoLuongX,
                               DonGia = a.DonGia,
                               ThanhTienN = a.ThanhTienX
                           }).ToList();
                    BaoCao.repPhieuXuat_12001 rep = new BaoCao.repPhieuXuat_12001(_kq);
                    var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                               select new { nd.DiaChi, kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan, nd.SoPL, nd.SoPhieu }).ToList();
                    if (par.Count > 0)
                    {
                        //rep.Soct.Value = id;
                        rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        if (DungChung.Bien.MaBV != "12122")
                            _nguoinhan = par.First().TenNguoiCC;
                        if (par.First().MaKPnx != null)
                            _makpnx = par.First().MaKPnx == null ? 0 : par.First().MaKPnx.Value;
                        if (par.First().MaBNhan != null)
                            _mabn = par.First().MaBNhan == null ? 0 : par.First().MaBNhan.Value;
                        rep.Noidung.Value = par.First().GhiChu;
                        rep.Khoxuat.Value = par.First().TenKP;
                        rep.Diachinguoinhan.Value = par.First().DiaChi;
                    }

                    //rep.Diachi.Value = "Trung tâm y tế huyện Tam Đường";
                    double TT = 0; string _soct = "", diachi = "";
                    if (q.Count > 0)
                    {
                        TT = q.Sum(p => p.ThanhTienX);
                        //if (DungChung.Bien.MaBV == "27022")
                        //    _soct = q.First().SoPhieu;
                        //else
                        _soct = q.First().SoCT;
                    }
                    rep.Soct.Value = "Số: " + _soct;
                    rep.txtSotien.Text = DungChung.Ham.DocTienBangChu(TT, " đồng!");
                    //rep.DataSource = q;
                    rep.TongTien.Value = TT;

                    var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                    if (bn != null)
                    {
                        _nguoinhan += bn.TenBNhan;
                        diachi = bn.DChi;
                        rep.Diachinguoinhan.Value = diachi;
                    }
                    rep.Nguoinhanhang.Value = _nguoinhan;


                    //if (q.First().KieuDon == 0 && q.First().KieuDon == 4)
                    //{
                    var noinhan = _dataContext.KPhongs.Where(p => p.MaKP == _makpnx).Select(p => new { p.TenKP, p.DChi }).FirstOrDefault();
                    if (noinhan != null)
                    {
                        if (q.First().KieuDon != 0 && q.First().KieuDon != 4)
                            rep.NoiNhan.Value = DungChung.Bien.MaBV == "12122" ? "" : noinhan.TenKP;
                        //if (DungChung.Bien.MaBV == "12122")
                        //    rep.khoanhan.Value = noinhan;
                        if (!string.IsNullOrEmpty(noinhan.DChi) && DungChung.Bien.MaBV == "24009" && q.First().KieuDon == 3)
                            rep.Diachinguoinhan.Value = noinhan.DChi.ToString();
                    }


                    #region xuat Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "0", "0", "0", "0" };
                    string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Mã số", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá", "Thành tiền" };
                    int[] _arrWidth = new int[] { };
                    DungChung.Bien.MangHaiChieu = new Object[q.Count + 20, 10];
                    DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQ.ToUpper(); ;
                    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.DiaChi;
                    DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C21-HD";
                    DungChung.Bien.MangHaiChieu[1, 5] = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của  BTC)";
                    DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu xuất kho").ToUpper();
                    DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    DungChung.Bien.MangHaiChieu[5, 4] = "Số: " + _soct;
                    DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
                    DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
                    DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người nhận hàng: " + par.First().TenNguoiCC;
                    DungChung.Bien.MangHaiChieu[7, 0] = "Địa chỉ: ";
                    DungChung.Bien.MangHaiChieu[8, 0] = "Nội dung: ";
                    DungChung.Bien.MangHaiChieu[9, 0] = "Xuất tại kho: " + par.First().TenKP;
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
                    }
                    int num = 12;
                    DungChung.Bien.MangHaiChieu[num + q.Count, 6] = "Tổng cộng:";
                    DungChung.Bien.MangHaiChieu[num + q.Count(), 7] = TT;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(TT, " đồng!"); ;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 6] = "Xuất, ngày ..... tháng ..... năm ...... ";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập phiếu";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 7] = "Thủ trưởng đơn vị";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 7] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 7] = DungChung.Bien.GiamDoc;
                    if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
                    {
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Thống kê dược";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = "";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Trưởng khoa dược";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = "";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ trưởng đơn vị";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.GiamDoc;
                    }

                    foreach (var r in q)
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num - 11;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.MaTam;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 4] = ""; ;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongX;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.ThanhTienX;

                        num++;

                    }


                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu xuất kho", "C:\\PhieuXuatKho.xls", true, this.Name);
                    //rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion
                }
                else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    #region phiếu xuất BV khác
                    BaoCao.repPhieuXuat_01071 rep = new BaoCao.repPhieuXuat_01071();
                    var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                               select new { nd.DiaChi, kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan, nd.SoPL, nd.SoPhieu, kp.ChuyenKhoa }).ToList();
                    if (par.Count > 0)
                    {
                        //rep.Soct.Value = id;
                        rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        if (DungChung.Bien.MaBV != "12122")
                            _nguoinhan = par.First().TenNguoiCC;
                        if (par.First().MaKPnx != null)
                            _makpnx = par.First().MaKPnx == null ? 0 : par.First().MaKPnx.Value;
                        if (par.First().MaBNhan != null)
                            _mabn = par.First().MaBNhan == null ? 0 : par.First().MaBNhan.Value;
                        rep.Noidung.Value = par.First().GhiChu;
                        rep.Khoxuat.Value = par.First().TenKP;
                        if (par.First().ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NhaThuoc)
                        {
                            rep.xrLabel27.Text = "";
                            rep.xrLabel26.Text = "";
                        }
                    }




                    //var q = from xd in _dataContext.XuatDs join xdct in _dataContext.XuatDcts on xd.IDXuat equals xdct.IDXuat where(xd.IDXuat == id) join dv in _dataContext.DichVus on 
                    var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                             join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                             join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                             select new
                             {
                                 nd.KieuDon,
                                 ndx.IDNhapct,
                                 nd.SoCT,
                                 ndx.MaDV,
                                 dv.MaTam,
                                 nd.SoPhieu,
                                 TenDV = (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "12122") ? (dv.TenDV + " " + dv.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? dv.TenRG : dv.TenDV),
                                 ndx.DonVi,
                                 dv.SoDK,
                                 ndx.DonGia,
                                 ndx.SoLuongX,
                                 ndx.ThanhTienX,
                                 dv.HamLuong,
                                 nd.DiaChi
                             }).OrderBy(p => p.IDNhapct).ToList();


                    double TT = 0; string _soct = "", diachi = "";
                    if (q.Count > 0)
                    {
                        TT = q.Sum(p => p.ThanhTienX);
                        //if (DungChung.Bien.MaBV == "27022")
                        //    _soct = q.First().SoPhieu;
                        //else
                        _soct = q.First().SoCT;
                    }
                    rep.Soct.Value = _soct;
                    rep.DataSource = q;
                    rep.TongTien.Value = TT;

                    var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                    if (bn != null)
                    {
                        _nguoinhan += bn.TenBNhan;
                        diachi = bn.DChi;
                    }
                    rep.Nguoinhanhang.Value = _nguoinhan;
                    rep.NoiNhan.Value = _nguoinhan;


                    //if (q.First().KieuDon == 0 && q.First().KieuDon == 4)
                    //{
                    var noinhan = _dataContext.KPhongs.Where(p => p.MaKP == _makpnx).Select(p => new { p.TenKP, p.DChi }).FirstOrDefault();
                    if (noinhan != null)
                    {
                        if (q.First().KieuDon != 0 && q.First().KieuDon != 4)
                            rep.NoiNhan.Value = DungChung.Bien.MaBV == "12122" ? "" : noinhan.TenKP;
                        //if (DungChung.Bien.MaBV == "12122")
                        //    rep.khoanhan.Value = noinhan;
                        if (DungChung.Bien.MaBV == "24009")
                        {
                            if (!string.IsNullOrEmpty(noinhan.DChi))
                                rep.Diachinguoinhan.Value = noinhan.DChi.ToString();
                            else
                                rep.Diachinguoinhan.Value = q.Count > 0 && q.First().DiaChi != null ? q.First().DiaChi : "";
                        }
                    }
                    else if (diachi != "")
                    {
                        rep.Diachinguoinhan.Value = diachi;
                    }
                    else if (q.Count > 0 && q.First().DiaChi != null)
                    {
                        rep.Diachinguoinhan.Value = q.First().DiaChi;
                    }
                    if (DungChung.Bien.MaBV == "12122")
                    {
                        if ((q.First().KieuDon == 0))
                        {
                            rep.NoiNhan.Value = _nguoinhan;
                            rep.Diachinguoinhan.Value = diachi;
                        }
                        else
                        {
                            rep.Diachinguoinhan.Value = par.First().DiaChi;
                            rep.NoiNhan.Value = par.First().TenNguoiCC;
                        }
                    }
                    if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "26007")
                        rep.NoiNhan.Value = par.First().TenNguoiCC;

                    if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023")
                    {
                        if (lupHinhthucx.EditValue != null && (Convert.ToInt32(lupHinhthucx.EditValue) == 0 || Convert.ToInt32(lupHinhthucx.EditValue) == 1) || (DungChung.Bien.MaBV == "27022" && Convert.ToInt32(lupHinhthucx.EditValue) == 9))
                        {
                            rep.NoiNhan.Value = txtNguoiNhan.Text;
                            rep.Nguoinhanhang.Value = txtNguoiNhan.Text;
                            rep.Diachinguoinhan.Value = txtDiaChi.Text;

                        }
                    }


                    #region xuat Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "0", "0", "0", "0" };
                    string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Mã số", "Hàm lượng", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá", "Thành tiền" };
                    int[] _arrWidth = new int[] { };
                    DungChung.Bien.MangHaiChieu = new Object[q.Count + 20, 11];
                    DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQ.ToUpper(); ;
                    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.DiaChi;
                    DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C21-HD";
                    DungChung.Bien.MangHaiChieu[1, 5] = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của  BTC)";
                    DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu xuất kho").ToUpper();
                    DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    DungChung.Bien.MangHaiChieu[5, 4] = "Số: " + _soct;
                    DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
                    DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
                    DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người nhận hàng: " + par.First().TenNguoiCC;
                    DungChung.Bien.MangHaiChieu[7, 0] = "Địa chỉ: ";
                    DungChung.Bien.MangHaiChieu[8, 0] = "Nội dung: ";
                    DungChung.Bien.MangHaiChieu[9, 0] = "Xuất tại kho: " + par.First().TenKP;
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
                    }
                    int num = 12;
                    DungChung.Bien.MangHaiChieu[num + q.Count, 6] = "Tổng cộng:";
                    DungChung.Bien.MangHaiChieu[num + q.Count(), 7] = TT;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(TT, " đồng!"); ;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 6] = "Xuất, ngày ..... tháng ..... năm ...... ";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập phiếu";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 7] = "Thủ trưởng đơn vị";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 7] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 7] = DungChung.Bien.GiamDoc;
                    if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
                    {
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Thống kê dược";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = "";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Trưởng khoa dược";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = "";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ trưởng đơn vị";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.GiamDoc;
                    }

                    foreach (var r in q)
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num - 11;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.MaTam;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.HamLuong;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 5] = ""; ;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.SoLuongX;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.ThanhTienX;

                        num++;

                    }


                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu xuất kho", "C:\\PhieuXuatKho.xls", true, this.Name);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion
                }
                else if (DungChung.Bien.MaBV == "14018")
                {
                    Dictionary<string, object> dicpar = new Dictionary<string, object>();
                    dicpar["NguoiLap"] = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().NguoiLapBieu.ToString();
                    dicpar["ThuKho"] = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().ThuKho.ToString();
                    var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                               join KP in _dataContext.KPhongs on nd.MaKPnx equals KP.MaKP
                               select new { nd.SoCT, nd.DiaChi, kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan, nd.SoPL, nd.SoPhieu, kp.ChuyenKhoa, TenKPXD = KP.TenKP, nd.KieuDon, nd.PLoai }).ToList();
                    var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                             join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                             join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                             select new
                             {
                                 nd.KieuDon,
                                 ndx.IDNhapct,
                                 nd.SoCT,
                                 ndx.MaDV,
                                 dv.MaTam,
                                 ndx.SoLo,
                                 ndx.HanDung,
                                 nd.GhiChu,
                                 TenDV = dv.TenDV,
                                 ndx.DonVi,
                                 dv.SoDK,
                                 ndx.DonGia,
                                 ndx.SoLuongX,
                                 ndx.ThanhTienX,
                                 nd.DiaChi,
                                 dv.DonViN,
                                 dv.HamLuong,
                             }).OrderBy(p => p.IDNhapct).ToList();
                    List<PXK14018> PXK = new List<PXK14018>();
                    foreach (var item in q)
                    {
                        PXK14018 themmoi = new PXK14018();
                        themmoi.TenDuoc = item.TenDV;
                        themmoi.MaSo = item.MaTam;
                        themmoi.DonViTinh = item.DonVi;
                        themmoi.ThucXuat = item.SoLuongX.ToString("#,##");
                        themmoi.DonGia = item.DonGia.ToString("#,##");
                        themmoi.ThanhTien = item.ThanhTienX.ToString("#,##");
                        PXK.Add(themmoi);
                    }

                    dicpar["NgayXuat"] = DungChung.Ham.NgaySangChu(par.First().NgayNhap.Value);
                    dicpar["TenBV"] = "Đơn vị: " + DungChung.Bien.TenCQ;
                    dicpar["DiaChiBV"] = "Địa chỉ: " + _dataContext.HTHONGs.First().DiaChi;
                    if (par.First().PLoai == 2 && par.First().KieuDon == 1)
                        dicpar["NhanHang"] = par.First().TenNguoiCC;
                    else
                        dicpar["NhanHang"] = "";
                    dicpar["XuatTaiKho"] = par.First().TenKP;
                    dicpar["XuatDenKho"] = par.First().TenKPXD;
                    dicpar["SoCT"] = "Số: " + par.First().SoCT;
                    double TongTien = Math.Round(Convert.ToDouble(q.Sum(p => p.ThanhTienX)), 3);
                    dicpar["TongTien"] = TongTien.ToString("#,##");
                    dicpar["LyDoXuatKho"] = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat().Where(p => p.Id == par.First().KieuDon).First().PhanLoai;
                    dicpar["KeToanTruong"] = _dataContext.HTHONGs.First().KeToanTruong;
                    dicpar["TongTienChu"] = DungChung.Ham.DocTienBangChu(TongTien, " đồng.");
                    dicpar["ThuTruongDonVi"] = DungChung.Bien.GiamDoc;
                    DungChung.Ham.Print(DungChung.PrintConfig.Rep_PhieuXuatKho_14018, PXK.ToList(), dicpar, false);
                }
                else if (DungChung.Bien.MaBV == "04007" || DungChung.Bien.MaBV == "24012")
                {
                    QLBV.Phieu.Rep_PhieuXuatDuoc_04007 rep = new Phieu.Rep_PhieuXuatDuoc_04007();
                    rep.TenPhieu.Value = "PHIẾU XUẤT KHO";
                    int s = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().FormatDate.Value;
                    rep.NguoiLapBieu.Value = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().NguoiLapBieu.ToString();
                    rep.ThuKho.Value = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().ThuKho.ToString();
                    var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                               join KP in _dataContext.KPhongs on nd.MaKPnx equals KP.MaKP
                               select new { nd.SoCT, nd.DiaChi, DiaChi3 = kp.DChi, kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan, nd.SoPL, nd.SoPhieu, kp.ChuyenKhoa, TenKPXD = KP.TenKP }).ToList();
                    //var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                    //         join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                    //         join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                    //         group new {nd,ndx,dv} by new
                    //         {
                    //             nd.KieuDon,
                    //             ndx.IDNhapct,
                    //             nd.SoCT,
                    //             ndx.MaDV,
                    //             dv.MaTam,
                    //             //ndx.SoLo,
                    //             //ndx.HanDung,
                    //             nd.GhiChu,
                    //             TenDV = dv.TenDV,
                    //             ndx.DonVi,
                    //             dv.SoDK,
                    //             ndx.DonGia,
                    //             SoLuongX = ndx.SoLuongX,
                    //             ndx.ThanhTienX,
                    //             nd.DiaChi,
                    //             dv.DonViN,
                    //             dv.HamLuong,
                    //         } into hc
                    //         select new
                    //         {
                    //             hc.Key.KieuDon,
                    //             hc.Key.IDNhapct,
                    //             hc.Key.SoCT,
                    //             hc.Key.MaDV,
                    //             hc.Key.MaTam,
                    //             //ndx.SoLo,
                    //             //ndx.HanDung,
                    //             hc.Key.GhiChu,
                    //             TenDV = hc.Key.TenDV,
                    //             hc.Key.DonVi,
                    //             hc.Key.SoDK,
                    //             hc.Key.DonGia,
                    //             SoLuongX = hc.Sum(c => c),
                    //             hc.Key.ThanhTienX,
                    //             hc.Key.DiaChi,
                    //             hc.Key.DonViN,
                    //             hc.Key.HamLuong,

                    //         }).OrderBy(p => p.IDNhapct).ToList();

                    var q = from nd in _dataContext.NhapDs
                            join nd1 in _dataContext.NhapDcts on nd.IDNhap equals nd1.IDNhap
                            join dv in _dataContext.DichVus on nd1.MaDV equals dv.MaDV
                            where nd.IDNhap == id
                            group new { dv, nd1, nd } by new
                            {
                                nd1.MaDV,
                                dv.MaTam,
                                dv.TenDV,
                                nd1.DonVi,
                                dv.SoDK,
                                nd1.DonGia,
                                dv.DonViN,
                                dv.HamLuong
                            } into groupedData
                            select new
                            {
                                groupedData.Key.SoDK,
                                groupedData.Key.HamLuong,
                                TenDV = groupedData.Key.TenDV,
                                MaTam = groupedData.Key.MaTam,
                                DonViN = groupedData.Key.DonVi,
                                DonGia = groupedData.Key.DonGia,
                                MaDV = groupedData.Key.MaDV,
                                SoLuongX = groupedData.Sum(x => x.nd1.SoLuongX),
                                ThanhTienX = groupedData.Sum(x => x.nd1.ThanhTienX)
                            };

                    ;

                    if (par.Count > 0)
                    {
                        rep.Formatdate = s;
                        rep.MauSo.Value = "Mẫu số: C31-HD";
                        rep.ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        rep.colcccq.Text = _dataContext.HTHONGs.Where(p => p.MaBV.Contains(DungChung.Bien.MaBV)).First().TenCQCQ.ToUpper();
                        rep.colcccq1.Text = "Đơn vị: " + DungChung.Bien.TenCQ.ToUpper();
                        rep.colTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
                        rep.HoTen.Value = par.First().TenKPXD;
                        rep.DiaChi.Value = par.First().DiaChi;
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            rep.DiaChi.Value = lupMaKPnx.Text;
                            rep.HoTen.Value = txtNguoiNhan.Text;
                        }
                        rep.diachi3.Value = par.First().DiaChi3;
                        rep.XuatTaiKho.Value = par.First().TenKP;
                        rep.XuatDenKho.Value = par.First().TenKPXD;
                        rep.So.Value = "Số: " + par.First().SoCT;
                        rep.NguoiNhanNguoiGiao.Value = par.First().TenNguoiCC;
                        rep.ColTongCong.Text = "Tổng cộng: " + q.Count().ToString() + " Khoản";
                        rep.ColTongCong1.Text = "Tổng cộng: " + q.Count().ToString() + " Khoản";
                        double TongTien = Math.Round(Convert.ToDouble(q.Sum(p => p.ThanhTienX)), 3);
                        rep.colTongTien.Text = TongTien.ToString("#,##");
                        rep.colTongTien1.Text = TongTien.ToString("#,##");
                        rep.LyDoXuatKho.Value = par.First().GhiChu;
                        rep.KeToanTruong.Value = _dataContext.HTHONGs.First().KeToanTruong;
                        rep.xrLabel27.Text = DungChung.Ham.DocTienBangChu(TongTien, " đồng.");
                        rep.SoLuuTru.Value = "Số lưu trữ: .........................................";

                    }
                    rep.DataSource = q.ToList();
                    frmIn frm = new frmIn();
                    rep.BindingData1();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    #region phiếu xuất BV khác
                    BaoCao.repPhieuXuat rep = new BaoCao.repPhieuXuat();
                    var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                               select new { nd.DiaChi, DiaChi3 = kp.DChi, kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan, nd.SoPL, nd.SoPhieu, kp.ChuyenKhoa }).ToList();
                    if (par.Count > 0)
                    {
                        //rep.Soct.Value = id;
                        rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        if (DungChung.Bien.MaBV != "12122")
                            _nguoinhan = par.First().TenNguoiCC;
                        if (par.First().MaKPnx != null)
                            _makpnx = par.First().MaKPnx == null ? 0 : par.First().MaKPnx.Value;
                        if (par.First().MaBNhan != null)
                            _mabn = par.First().MaBNhan == null ? 0 : par.First().MaBNhan.Value;
                        rep.Noidung.Value = par.First().GhiChu;
                        rep.Khoxuat.Value = par.First().TenKP;
                        rep.DiaChiKho.Value = par.First().DiaChi3;
                        if (par.First().ChuyenKhoa == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NhaThuoc)
                        {
                            rep.xrLabel27.Text = "";
                            rep.xrLabel26.Text = "";
                        }
                        var hthong = _dataContext.HTHONGs.FirstOrDefault(o => o.MaBV == DungChung.Bien.MaBV);
                        if (hthong != null)
                            rep.colTTDV.Text = hthong.GiamDoc;
                    }




                    //var q = from xd in _dataContext.XuatDs join xdct in _dataContext.XuatDcts on xd.IDXuat equals xdct.IDXuat where(xd.IDXuat == id) join dv in _dataContext.DichVus on 
                    var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                             join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                             join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                             group new { dv, ndx, nd } by new { nd.KieuDon, nd.SoCT, nd.SoPhieu, nd.DiaChi, nd.TenNguoiCC, nd.PLoai, dv.MaTam, dv.SoDK, dv.TenRG, dv.HamLuong, nd, ndx.DonGia, ndx.MaDV, dv.TenDV, dv.DonVi } into kq
                             select new
                             {
                                 IDNhapct = kq.Max(o => o.ndx.IDNhapct),
                                 kq.Key.KieuDon,
                                 kq.Key.SoCT,
                                 kq.Key.MaDV,
                                 kq.Key.MaTam,
                                 kq.Key.SoPhieu,
                                 TenDV = (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "12122") ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : ((DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009") ? kq.Key.TenRG : kq.Key.TenDV),
                                 kq.Key.DonVi,
                                 kq.Key.SoDK,
                                 kq.Key.DonGia,
                                 SoLuongX = kq.Sum(o => o.ndx.SoLuongX),
                                 ThanhTienX = kq.Sum(o => o.ndx.ThanhTienX),
                                 kq.Key.DiaChi,
                                 kq.Key.TenNguoiCC,
                                 kq.Key.PLoai
                             }).OrderBy(p => p.IDNhapct).ToList();


                    double TT = 0; string _soct = "", diachi = "";
                    if (q.Count > 0)
                    {
                        TT = q.Sum(p => p.ThanhTienX);
                        //if (DungChung.Bien.MaBV == "27022")
                        //    _soct = q.First().SoPhieu;
                        //else
                        _soct = q.First().SoCT;
                    }
                    rep.Soct.Value = _soct;

                    rep.DataSource = q;
                    rep.TongTien.Value = TT;
                    rep.txtSoCT.Text = "Số chứng từ kèm theo: " + _soct;

                    var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();
                    if (bn != null)
                    {
                        _nguoinhan += bn.TenBNhan;
                        diachi = bn.DChi;
                    }
                    rep.Nguoinhanhang.Value = _nguoinhan;
                    rep.NoiNhan.Value = _nguoinhan;


                    //if (q.First().KieuDon == 0 && q.First().KieuDon == 4)
                    //{
                    var noinhan = _dataContext.KPhongs.Where(p => p.MaKP == _makpnx).Select(p => new { p.TenKP, p.DChi }).FirstOrDefault();
                    if (noinhan != null)
                    {
                        if (q.First().KieuDon != 0 && q.First().KieuDon != 4)
                            rep.NoiNhan.Value = DungChung.Bien.MaBV == "12122" ? "" : noinhan.TenKP;
                        //if (DungChung.Bien.MaBV == "12122")
                        //    rep.khoanhan.Value = noinhan;
                        if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003" || DungChung.Bien.MaBV == "26007")
                        {
                            if (!string.IsNullOrEmpty(noinhan.DChi))
                                rep.Diachinguoinhan.Value = noinhan.DChi.ToString();
                            else
                                rep.Diachinguoinhan.Value = q.Count > 0 && q.First().DiaChi != null ? q.First().DiaChi : "";
                        }
                    }
                    else if (diachi != "")
                    {
                        rep.Diachinguoinhan.Value = diachi;
                    }
                    else if (q.Count > 0 && q.First().DiaChi != null)
                    {
                        rep.Diachinguoinhan.Value = q.First().DiaChi;
                    }
                    if (DungChung.Bien.MaBV == "12122")
                    {
                        if ((q.First().KieuDon == 0))
                        {
                            rep.NoiNhan.Value = _nguoinhan;
                            rep.Diachinguoinhan.Value = diachi;
                        }
                        else
                        {
                            rep.Diachinguoinhan.Value = par.First().DiaChi;
                            rep.NoiNhan.Value = par.First().TenNguoiCC;
                        }
                    }
                    if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "26007")
                        rep.NoiNhan.Value = par.First().TenNguoiCC;

                    if (DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023")
                    {
                        if (lupHinhthucx.EditValue != null && (Convert.ToInt32(lupHinhthucx.EditValue) == 0 || Convert.ToInt32(lupHinhthucx.EditValue) == 1) || (DungChung.Bien.MaBV == "27022" && Convert.ToInt32(lupHinhthucx.EditValue) == 9))
                        {
                            rep.NoiNhan.Value = txtNguoiNhan.Text;
                            rep.Nguoinhanhang.Value = txtNguoiNhan.Text;
                            rep.Diachinguoinhan.Value = txtDiaChi.Text;

                        }
                    }
                    //Ngay 20/09/2019 thêm tự động fill Người lập biểu và kế toán trưởng cho viện 26007

                    if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "20001")
                    {
                        rep.NguoiLapPhieu.Value = DungChung.Bien.NguoiLapBieu;
                        rep.Ketoantruong.Value = DungChung.Bien.KeToanTruong;
                    }
                    if (DungChung.Bien.MaBV == "23456")
                    {
                        if (q.Count > 0 && q.First().PLoai == 2 && q.First().KieuDon == 9)
                            rep.NoiNhan.Value = q.First().TenNguoiCC;
                    }


                    #region xuat Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "0", "0", "0", "0" };
                    string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Mã số", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá", "Thành tiền" };
                    int[] _arrWidth = new int[] { };
                    DungChung.Bien.MangHaiChieu = new Object[q.Count + 20, 10];
                    DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQ.ToUpper(); ;
                    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.DiaChi;
                    DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C21-HD";
                    DungChung.Bien.MangHaiChieu[1, 5] = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của  BTC)";
                    DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu xuất kho").ToUpper();
                    DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    DungChung.Bien.MangHaiChieu[5, 4] = "Số: " + _soct;
                    DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
                    DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
                    DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người nhận hàng: " + par.First().TenNguoiCC;
                    DungChung.Bien.MangHaiChieu[7, 0] = "Địa chỉ: ";
                    DungChung.Bien.MangHaiChieu[8, 0] = "Nội dung: ";
                    DungChung.Bien.MangHaiChieu[9, 0] = "Xuất tại kho: " + par.First().TenKP;
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
                    }
                    int num = 12;
                    DungChung.Bien.MangHaiChieu[num + q.Count, 6] = "Tổng cộng:";
                    DungChung.Bien.MangHaiChieu[num + q.Count(), 7] = TT;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.NumberToTextVN((decimal)TT); ;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 6] = "Xuất, ngày ..... tháng ..... năm ...... ";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập phiếu";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 7] = "Thủ trưởng đơn vị";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 7] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 7] = DungChung.Bien.GiamDoc;
                    if (DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "30009")
                    {
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Thống kê dược";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = "";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Trưởng khoa dược";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = "";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ trưởng đơn vị";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.GiamDoc;
                    }

                    foreach (var r in q)
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num - 11;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.MaTam;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.DonVi;
                        DungChung.Bien.MangHaiChieu[num, 4] = ""; ;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongX;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.ThanhTienX;

                        num++;

                    }


                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu xuất kho", "C:\\PhieuXuatKho.xls", true, this.Name);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #endregion
                }
            }
            return true;
        }

        #endregion
        private void btnIn_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
                id = int.Parse(txtIDNhap.Text);
            InPhieuXuat(id);

        }

        private void lupHinhthucx_EditValueChanged(object sender, EventArgs e)
        {
            int htxuat = -1;
            // xuất nội trú chỉ có chuyên khoa là phòng mổ mới cho phép xuất

            var qck = (from kp in _dataContext.KPhongs.Where(p => p.MaKP == DungChung.Bien.MaKP) join ck in _dataContext.ChuyenKhoas.Where(p => p.TenChiTiet == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat) on kp.MaCK equals ck.MaCK select kp).ToList();

            if (lupHinhthucx.EditValue != null)
                htxuat = Convert.ToInt32(lupHinhthucx.EditValue);
            if (TTLuu == 2 || TTLuu == 1)
            {
                if ((DungChung.Bien.MaBV == "04007" ? false : (htxuat == 0)) || (DungChung.Bien.MaBV == "04007" ? false : (htxuat == 1 && qck.Count == 0) || htxuat == 4 || (htxuat == 5 && DungChung.Bien.MaBV != "12001")) || htxuat == 6 || htxuat == 7 || htxuat == 10 || htxuat == 11)
                {
                    MessageBox.Show("Chức năng không cho phép");
                    lupHinhthucx.EditValue = -1;
                }
                txtSoCT.Text = "";
            }
            if (htxuat >= 0)
                hinhthucxuat(htxuat);

        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnNhanN_Click(object sender, EventArgs e)
        {
            Frm_NhanNhap frm = new Frm_NhanNhap(_dataContext, _medicinesProvider);
            frm.ShowDialog();
        }

        public class DVu
        {
            public int MaDV { set; get; }
            public string TenDV { set; get; }
            public string DonVi { set; get; }
            public string MaTam { set; get; }
            public string HamLuong { set; get; }
            public int? Status { get; set; }
            public string SoLo { set; get; }
            public DateTime? HanDung { get; set; }

        }
        private void lupMaKP_EditValueChanged(object sender, EventArgs e)
        {
            int makho = 0;
            if (lupMaKP.EditValue != null)
                makho = maKhoXuat = Convert.ToInt32(lupMaKP.EditValue);

            int idNhap = 0;
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null)
                idNhap = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));

            ppxuat = _medicinesProvider.GetPPXuat(makho);
            if (TTLuu == 1 || TTLuu == 2 || TTLuu == 0)
            {
                medicinesByRoom = _medicinesProvider.GetLupMaDuoc(makho, idNhap, 0);

                lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;
            }
            else
            {
                //lupMaDuoc.DataSource = _dv;
                lupMaDuoc.DataSource = lupIDThuoc.DataSource = allMedicines;
            }

            if (ppxuat == 1)
            {
                colIDThuoc.Visible = false;
                colMaDV.Visible = true;
            }
            else if (ppxuat == 3)
            {
                colIDThuoc.Visible = true;
                colMaDV.Visible = false;
            }
        }

        private void cboIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboIn.SelectedIndex)
            {
                case 0:
                    btnIn_Click(sender, e);
                    break;
                case 1:
                    frmIn frm = new frmIn();
                    BaoCao.repPhieuXuat_full rep = new BaoCao.repPhieuXuat_full();
                    int id = 0;
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        id = int.Parse(txtIDNhap.Text);
                    var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                               select new { kp.MaKP, nd.SoCT, kp.TenKP, kp.DChi, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu }).ToList();
                    if (par.Count > 0)
                    {
                        if (DungChung.Bien.MaBV == "30009")
                        {
                            rep.Soct.Value = par.First().SoCT;
                            rep.Khoxuat.Value = par.First().TenKP;
                            rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;

                        }
                        else
                        {
                            rep.Soct.Value = id;
                            rep.Khoxuat.Value = par.First().TenKP;
                            rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.ToString().Substring(0, 2) + " tháng " + par.First().NgayNhap.ToString().Substring(3, 2) + " năm " + par.First().NgayNhap.ToString().Substring(6, 4);

                        }
                        rep.txtSoCT.Text = "Số chứng từ kèm theo: " + id;
                        rep.Nguoinhanhang.Value = par.First().TenNguoiCC;

                        rep.Noidung.Value = par.First().GhiChu;
                    }
                    var parx = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                                join kp in _dataContext.KPhongs on nd.MaKPnx equals kp.MaKP
                                select new { kp.TenKP, kp.DChi }).ToList();
                    if (parx.Count > 0)
                    {
                        rep.Diachinguoinhan.Value = parx.First().DChi;
                        if (DungChung.Bien.MaBV != "30009")
                        {
                            rep.Khoxuat.Value = parx.First().TenKP;
                        }
                    }
                    rep.DiaChiKho.Value = par.First().DChi;
                    //var q = from xd in _dataContext.XuatDs join xdct in _dataContext.XuatDcts on xd.IDXuat equals xdct.IDXuat where(xd.IDXuat == id) join dv in _dataContext.DichVus on 
                    var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                             join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                             join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                             group new { dv, nd, ndx } by new { dv.MaTam, dv.TenDV, dv.SoDK, dv.NuocSX, ndx.DonVi, ndx.SoLo, ndx.HanDung, ndx.DonGia, dv.MaDV } into kq
                             select new
                             {
                                 kq.Key.NuocSX,
                                 kq.Key.SoLo,
                                 kq.Key.HanDung,
                                 kq.Key.MaDV,
                                 TenDV = kq.Key.TenDV,
                                 DonVi = kq.Key.DonVi,
                                 MaSo = kq.Key.SoDK,
                                 MaNoiBo = kq.Key.MaTam,
                                 DonGia = kq.Key.DonGia,
                                 SoLuongX = kq.Sum(p => p.ndx.SoLuongX),
                                 ThanhTienX = kq.Sum(p => p.ndx.ThanhTienX)
                             }).ToList();
                    if (q.Count > 0)
                    {

                        double TT = 0;
                        for (int i = 0; i < q.Count(); i++)
                        {
                            if (q.ToList().Skip(i).Take(1).First().ThanhTienX != null)
                            {
                                string tt = q.Skip(i).Take(1).First().ThanhTienX.ToString();
                                TT = TT + Convert.ToDouble(tt);
                            }
                        }
                        rep.DataSource = q;
                        rep.TongTien.Value = TT;
                        rep.BindingData();
                        //rep.DataMember = "";
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                        cboIn.SelectedIndex = 8;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu");
                        break;
                    }

                case 2:
                    int _makpnx = 0, _mabn = 0;
                    string _nguoinhan = "";
                    frmIn frm2 = new frmIn();
                    BaoCao.repPhieuXuat_dy rep2 = new BaoCao.repPhieuXuat_dy();
                    int id2 = 0;
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        id2 = int.Parse(txtIDNhap.Text);
                    var par2 = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id2)
                                join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                                select new { kp.TenKP, kp.DChi, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan }).ToList();
                    if (par2.Count > 0)
                    {
                        rep2.Soct.Value = id2;
                        rep2.Ngaythang.Value = DungChung.Ham.NgaySangChu(par2.First().NgayNhap.Value, 1);
                        _nguoinhan = par2.First().TenNguoiCC;
                        if (par2.First().MaKPnx != null)
                            _makpnx = par2.First().MaKPnx == null ? 0 : par2.First().MaKPnx.Value;
                        if (par2.First().MaBNhan != null)
                            _mabn = par2.First().MaBNhan == null ? 0 : par2.First().MaBNhan.Value;
                        rep2.Noidung.Value = par2.First().GhiChu;
                        rep2.Khoxuat.Value = par2.First().TenKP;
                    }
                    var parx2 = (from nd in _dataContext.KPhongs.Where(p => p.MaKP == _makpnx)
                                 select new { nd.TenKP, nd.DChi }).ToList();
                    if (parx2.Count > 0)
                    {
                        rep2.Diachinguoinhan.Value = parx2.First().DChi;
                        rep2.NoiNhan.Value = parx2.First().TenKP;
                    }
                    var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).ToList();
                    if (bn.Count > 0)
                        _nguoinhan += bn.First().TenBNhan;
                    rep2.Nguoinhanhang.Value = _nguoinhan;
                    //var q = from xd in _dataContext.XuatDs join xdct in _dataContext.XuatDcts on xd.IDXuat equals xdct.IDXuat where(xd.IDXuat == id) join dv in _dataContext.DichVus on 
                    var q2 = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id2)
                              join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                              join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                              group new { dv, nd, ndx } by new { dv.TenDV, dv.SoDK, ndx.DonVi, ndx.DonGia, dv.MaDV } into kq
                              select new
                              {
                                  kq.Key.MaDV,
                                  TenDV = kq.Key.TenDV,
                                  DonVi = kq.Key.DonVi,
                                  MaSo = kq.Key.SoDK,
                                  DonGia = kq.Key.DonGia,
                                  SoLuongX = kq.Sum(p => p.ndx.SoLuongX),
                                  ThanhTienX = kq.Sum(p => p.ndx.ThanhTienX)
                              }).ToList();
                    if (q2.Count > 0)
                    {

                        double TT = 0;
                        for (int i = 0; i < q2.Count(); i++)
                        {
                            if (q2.ToList().Skip(i).Take(1).First().ThanhTienX != null)
                            {
                                string tt = q2.Skip(i).Take(1).First().ThanhTienX.ToString();
                                TT = TT + Convert.ToDouble(tt);
                            }
                        }
                        rep2.DataSource = q2;
                        rep2.TongTien.Value = TT;
                        rep2.BindingData();
                        //rep.DataMember = "";
                        rep2.CreateDocument();
                        frm2.prcIN.PrintingSystem = rep2.PrintingSystem;
                        frm2.ShowDialog();
                    }
                    else
                    {

                        MessageBox.Show("Không có dữ liệu");
                    }
                    break;
                case 3:// phiếu xuất thuốc gây nghiện hướng tâm thần
                    //string _makpnx = "", _mabn = "", _nguoinhan = "";
                    frmIn frm3 = new frmIn();
                    BaoCao.repPhieuXuat_ThuocGNHTT rep3 = new BaoCao.repPhieuXuat_ThuocGNHTT();
                    int id3 = 0;
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        id3 = int.Parse(txtIDNhap.Text);
                    var par3 = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id3)
                                join kp in _dataContext.KPhongs on nd.MaKPnx equals kp.MaKP
                                //    join cb in _dataContext.CanBoes on nd.MaCC equals cb.MaCB
                                select new { kp.TenKP, kp.DChi, nd.NgayNhap, nd.MaCB }).ToList();
                    if (par3.Count > 0)
                    {
                        if (par3.First().NgayNhap != null)
                            rep3.Ngaythang.Value = DungChung.Ham.NgaySangChu(par3.First().NgayNhap.Value);
                        rep3.NoiNhan.Value = par3.First().TenKP;
                        if (par3.First().MaCB != null && DungChung.Bien.MaBV != "30012")
                        {
                            string _macb = par3.First().MaCB;
                            var cb = _dataContext.CanBoes.Where(p => p.MaCB == _macb).Select(p => new { p.TenCB }).ToList();
                            rep3.Nguoinhanhang.Value = cb.First().TenCB;
                        }
                        rep3.Diachinguoinhan.Value = par3.First().DChi;

                        var q3 = (from ndx in _dataContext.NhapDcts.Where(p => p.IDNhap == id3)
                                  join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                                  select new
                                  {
                                      ndx.IDNhapct,
                                      dv.TenDV,
                                      ndx.DonVi,
                                      ndx.SoLo,
                                      ndx.HanDung,
                                      dv.NhaSX,
                                      dv.NuocSX,
                                      ndx.DonGia,
                                      ndx.SoLuongX,
                                  }).OrderBy(p => p.IDNhapct).ToList();

                        rep3.DataSource = q3;
                        rep3.BindingData();
                        rep3.CreateDocument();
                        frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                        frm3.ShowDialog();

                    }
                    break;
                case 4:
                    int id4 = 0;
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        id4 = int.Parse(txtIDNhap.Text);
                    InPhieuXuat_30003(id4);
                    break;
            }
        }

        #region InPhieuXuat
        public bool InPhieuXuat_30003(int id)
        {
            int _makpnx = 0, _mabn = 0;
            string _nguoinhan = "";
            #region Phiếu xuất kho ngoài BV (30003)

            BaoCao.rep_PhieuXuatNgoaiBV_30003 rep = new BaoCao.rep_PhieuXuatNgoaiBV_30003();
            var par = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                       join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                       select new { nd.DiaChi, kp.TenKP, nd.NgayNhap, nd.TenNguoiCC, nd.GhiChu, nd.MaKPnx, nd.MaBNhan, nd.SoPL }).ToList();
            if (par.Count > 0)
            {
                rep.Ngaythang.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                _nguoinhan = par.First().TenNguoiCC;
                if (par.First().MaKPnx != null)
                    _makpnx = par.First().MaKPnx == null ? 0 : par.First().MaKPnx.Value;
                if (par.First().MaBNhan != null)
                    _mabn = par.First().MaBNhan == null ? 0 : par.First().MaBNhan.Value;
                rep.Noidung.Value = par.First().GhiChu;
                rep.Khoxuat.Value = par.First().TenKP;
            }
            var bn = _dataContext.BenhNhans.Where(p => p.MaBNhan == _mabn).FirstOrDefault();

            if (bn != null)
                _nguoinhan += bn.TenBNhan;
            rep.Nguoinhanhang.Value = _nguoinhan;

            var noinhan = _dataContext.KPhongs.Where(p => p.MaKP == _makpnx).Select(p => p.TenKP).FirstOrDefault();
            if (noinhan != null)
                rep.NoiNhan.Value = noinhan;
            rep.Diachinguoinhan.Value = par.First().DiaChi;

            var q = (from nd in _dataContext.NhapDs.Where(p => p.IDNhap == id)
                     join ndx in _dataContext.NhapDcts on nd.IDNhap equals ndx.IDNhap
                     join dv in _dataContext.DichVus on ndx.MaDV equals dv.MaDV
                     select new
                     {
                         ndx.IDNhapct,
                         nd.SoCT,
                         ndx.MaDV,
                         dv.MaTam,
                         TenDV = (dv.TenDV + " " + dv.HamLuong),
                         ndx.DonVi,
                         dv.SoDK,
                         dv.SoQD,
                         dv.NgayQD,
                         ndx.DonGia,
                         ndx.SoLuongX,
                         ndx.ThanhTienX
                     }).OrderBy(p => p.IDNhapct).ToList();
            double TT = 0; string _soct = "";
            if (q.Count > 0)
            {
                TT = q.Sum(p => p.ThanhTienX);
                _soct = q.First().SoCT;
            }
            rep.Soct.Value = _soct;
            rep.DataSource = q;
            rep.TongTien.Value = TT;
            #region xuat Excel

            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "0", "0", "0", "0" };
            string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tu (SP, hàng hóa)", "Số QĐ", "Ngày công bố", "Mã số", "Ðơn vị tính", "SL theo chứng từ", "SL thực nhập", "Ðơn giá", "Thành tiền" };
            int[] _arrWidth = new int[] { };
            DungChung.Bien.MangHaiChieu = new Object[q.Count + 20, 10];
            DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQ.ToUpper(); ;
            DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.DiaChi;
            DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C21-HD";
            DungChung.Bien.MangHaiChieu[1, 5] = "(Ban hành theo QÐ số 19/2006/QÐ-BTC Ngày 30/03/2006 của  BTC)";
            DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu xuất kho ngoài bệnh viện").ToUpper();
            DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
            DungChung.Bien.MangHaiChieu[5, 4] = "Số: " + _soct;
            DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
            DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
            DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người nhận hàng: " + par.First().TenNguoiCC;
            DungChung.Bien.MangHaiChieu[7, 0] = "Ðịa chỉ: ";
            DungChung.Bien.MangHaiChieu[8, 0] = "Nội dung: ";
            DungChung.Bien.MangHaiChieu[9, 0] = "Xuất tại kho: " + par.First().TenKP;
            for (int i = 0; i < _tieude.Length; i++)
            {
                DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
            }
            int num = 12;
            DungChung.Bien.MangHaiChieu[num + q.Count, 6] = "Tổng cộng:";
            DungChung.Bien.MangHaiChieu[num + q.Count(), 7] = TT;
            DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(TT, " đồng!"); ;
            DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 6] = "Xuất, ngày ..... tháng ..... nam ...... ";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập phiếu";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
            DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người nhận hàng";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
            DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
            DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 7] = "Thủ trưởng đơn vị";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 7] = "(Ký, họ tên)";
            DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 7] = DungChung.Bien.GiamDoc;

            foreach (var r in q)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = num - 11;
                DungChung.Bien.MangHaiChieu[num, 1] = r.TenDV;
                DungChung.Bien.MangHaiChieu[num, 2] = r.SoQD;
                DungChung.Bien.MangHaiChieu[num, 3] = String.Format("{0:dd/MM/yyyy}", r.NgayQD);
                DungChung.Bien.MangHaiChieu[num, 4] = r.MaTam;
                DungChung.Bien.MangHaiChieu[num, 5] = r.DonVi;
                DungChung.Bien.MangHaiChieu[num, 6] = ""; ;
                DungChung.Bien.MangHaiChieu[num, 7] = r.SoLuongX;
                DungChung.Bien.MangHaiChieu[num, 8] = r.DonGia;
                DungChung.Bien.MangHaiChieu[num, 9] = r.ThanhTienX;

                num++;
            }

            #endregion
            //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu xuất kho ngoài BV", "C:\\PhieuXuatKhoNgoaiBV.xls", true);
            frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu xuất kho ngoài BV", "C:\\PhieuXuatKhoNgoaiBV.xls", true, this.Name);
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();

            #endregion
            return true;
        }

        #endregion
        private void txtTimKiem_Click(object sender, EventArgs e)
        {
        }

        private void lupMaKP_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TTLuu == 2 || TTLuu == 1)
            {
                if (grvNhapCT.RowCount > 0)
                {
                    if (grvNhapCT.RowCount == 1)
                    {
                        if (grvNhapCT.GetRowCellValue(1, colMaDV) != null && grvNhapCT.GetRowCellValue(1, colMaDV).ToString() != "")
                        {
                            MessageBox.Show("Chứng từ đã có thuốc, bạn không thể sửa kho xuất");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ  có thuốc, bạn không thể sửa kho xuất");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void lupMaKPnx_EditValueChanged(object sender, EventArgs e)
        {
            if (TTLuu == 1 || TTLuu == 2)
            {
                int _makpnx = 0;
                if (lupMaKPnx.EditValue != null)
                    _makpnx = Convert.ToInt32(lupMaKPnx.EditValue);
                var parx = (from nd in _dataContext.KPhongs.Where(p => p.MaKP == _makpnx)
                            select new { nd.TenKP, nd.DChi }).ToList();
                if (parx.Count > 0)
                    txtDiaChi.Text = parx.First().DChi;
            }
        }
        int status = 0;

        private void btnXML_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
                id = Convert.ToInt32(txtIDNhap.Text);

            if (DungChung.cls_KetNoiXP_SA.ExportXML(id, DungChung.Bien.xmlFilePath_LIS[3], status))
                MessageBox.Show("Xuất thành công");
            else

                MessageBox.Show("Lỗi");

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frm_NhapXuat_TTruc frm = new frm_NhapXuat_TTruc();
            frm.ShowDialog();
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cbo_PlXuat_TK_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
        private void cboMien_SelectedValueChanged(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "27183")
            {
                int ploaixuat = 0; int mien = 0;
                if (lupHinhthucx.EditValue != null)
                    ploaixuat = Convert.ToInt32(lupHinhthucx.EditValue);
                if (ploaixuat == 9)
                {
                    if (!string.IsNullOrEmpty(cboMien.Text))
                        mien = Convert.ToInt32(cboMien.Text);
                    int makho = 0;
                    if (lupMaKP.EditValue != null)
                        makho = Convert.ToInt32(lupMaKP.EditValue);
                    for (int i = 0; i < grvNhapCT.RowCount; i++)
                    {
                        if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                        {
                            int _madv = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                            if (grvNhapCT.GetRowCellValue(i, colDonGia) != null && grvNhapCT.GetRowCellValue(i, colSoLuong) != null)
                            {
                                double dongia = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colDonGia));
                                double soluong = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuong));
                                double soluongDY = DungChung.Ham._getSL_DongY(_dataContext, _madv, soluong, makho);
                                double thanhtien = (double)(dongia * soluong * (100 - mien)) / 100;
                                grvNhapCT.SetRowCellValue(i, colThanhTien, thanhtien);
                                grvNhapCT.SetFocusedRowCellValue(colSLuongDY, soluongDY);
                                grvNhapCT.SetFocusedRowCellValue(colThanhTienDY, soluongDY * dongia); //thành tiền đông y sẽ không tính theo khoản miễn
                            }
                        }
                    }
                }
            }
        }

        private void btnTcTonDuoc_Click(object sender, EventArgs e)
        {
            ChucNang.frm_dsTonDuoc frm = new ChucNang.frm_dsTonDuoc();
            frm.ShowDialog();
        }

        private void cboTLchietKhau_SelectedIndexChanged(object sender, EventArgs e)
        {
        }



        private void grvNhapCT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetTextStock();
        }

        private void grvNhapCT_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var source = (List<NhapDctModel>)binNhapDuocct.DataSource;
            if (source != null && source.Count > 1)
            {
                grvNhapCT.SetRowCellValue(e.RowHandle, colMien, source.First().MienCT);
            }
            else
            {
                grvNhapCT.SetRowCellValue(e.RowHandle, colMien, 0);
            }
        }

        private void grvNhapCT_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn outStockCol = view.Columns["SoLuongX"];
            GridColumn colMaDV = view.Columns["MaDV"];
            GridColumn colIDThuoc = view.Columns["IDThuoc"];
            GridColumn colDonGia = view.Columns["DonGia"];
            GridColumn colSoLo = view.Columns["SoLo"];

            //Get the value of the first column 
            Double outsto = (Double)view.GetRowCellValue(e.RowHandle, outStockCol);
            int maDV = view.GetRowCellValue(e.RowHandle, colMaDV) != null ? (int)view.GetRowCellValue(e.RowHandle, colMaDV) : 0;
            int idThuoc = view.GetRowCellValue(e.RowHandle, colIDThuoc) != null ? (int)view.GetRowCellValue(e.RowHandle, colIDThuoc) : 0;
            double DonGia = view.GetRowCellValue(e.RowHandle, colDonGia) != null ? (double)view.GetRowCellValue(e.RowHandle, colDonGia) : 0;
            string SoLo = view.GetRowCellValue(e.RowHandle, colSoLo) != null ? (string)view.GetRowCellValue(e.RowHandle, colSoLo) : "";
            //Get the value of the second column 

            //Validity criterion 
            if (outsto <= 0)
            {
                e.Valid = false;
                //Set errors with specific descriptions for the columns 
                view.SetColumnError(outStockCol, "Vui lòng nhập lại số lượng !");
            }
            var dataSource = (List<NhapDctModel>)binNhapDuocct.DataSource;
            if (dataSource.Where(o => o.MaDV == maDV).Count() > 1)
            {
                if (DungChung.Bien.MaBV == "27023" && (dataSource.Where(o => o.DonGia == DonGia).Count() >= 1) || dataSource.Where(o => o.SoLo == SoLo).Count() >= 1)
                {
                    e.Valid = true;
                }
                else
                {
                    e.Valid = false;
                    //Set errors with specific descriptions for the columns 
                    view.SetColumnError(colMaDV, "Đã tồn tại thuốc/vật tư này");
                }
            }
        }

        private void grvNhapCT_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void grvNhapCT_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        }

        private void btnKLuu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Có dữ liệu chưa lưu! Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                grpTimKiem.Enabled = true;
                this.Frm_XuatDuoc_Load(sender, e);
            }
        }
        private void SetTextStock()
        {
            int maDV = 0;
            int idThuoc = 0;
            string soLo = "";
            DateTime hanDung = new DateTime();
            double donGia = 0;

            if ((grvNhapCT.GetFocusedRowCellValue(colMaDV) == null && ppxuat == 1) || (grvNhapCT.GetFocusedRowCellValue(colIDThuoc) == null && ppxuat == 3))
            {
                this.grpCTCT.Text = "Danh sách đơn thuốc";
            }
            else
            {
                if (ppxuat == 1)
                {
                    if ((grvNhapCT.GetFocusedRowCellValue(colMaDV) != null && grvNhapCT.GetFocusedRowCellValue(colMaDV).ToString() != "") &&
                    (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != ""))
                    {
                        maDV = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                        donGia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));

                        var medicine = medicinesByRoom.FirstOrDefault(p => p.MaDV == maDV && p.DonGia == donGia);

                        if (medicine != null)
                            this.grpCTCT.Text = "Số lượng tồn: " + medicine.TonHienTai;
                    }
                }
                else if (ppxuat == 3)
                {
                    if ((grvNhapCT.GetFocusedRowCellValue(colIDThuoc) != null && grvNhapCT.GetFocusedRowCellValue(colIDThuoc).ToString() != ""))
                    {
                        idThuoc = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDThuoc));

                        var medicine = medicinesByRoom.FirstOrDefault(p => p.IDThuoc == idThuoc);

                        if (medicine != null)
                            this.grpCTCT.Text = "Số lượng tồn: " + medicine.TonHienTai;
                    }
                }
            }
        }

        private void grvNhapCT_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            SetTextStock();
        }
    }
}
