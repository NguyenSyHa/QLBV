using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using QLBV.FormNhap;
using System.Collections;
using QLBV.Providers.Business.Medicines;
using QLBV.Models.Dictionaries.Thuoc;
using DevExpress.XtraGrid.Views.Grid;
using QLBV.Providers.StoredProcedure;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

namespace QLBV.Forms.Medicines
{
    public partial class Frm_LinhHC_VTYT : DevExpress.XtraEditors.XtraUserControl
    {
        private QLBVEntities _dataContext;
        private readonly MedicinesProvider _medicinesProvider;
        IList<MedicineInventoryModel> medicinesByRoom = new List<MedicineInventoryModel>();


        public Frm_LinhHC_VTYT()
        {
            InitializeComponent();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _medicinesProvider = new MedicinesProvider();
        }
        private void Enablebutton(bool Status)
        {
            btnLuu.Enabled = !Status;
            btnMoi.Enabled = Status;
            btnSua.Enabled = Status;
            btnXoa.Enabled = Status;
            btnKLuu.Enabled = !Status;
        }
        private void EnableControl(bool status)
        {
            dtNgayKe.Properties.ReadOnly = !status;
            lupBPKe.Properties.ReadOnly = !status;
            lupKhoXuat.Properties.ReadOnly = !status;
            lupNguoiKe.Properties.ReadOnly = !status;
            // cboNhomDuoc.Properties.ReadOnly = !status;
            grvDonThuocct.OptionsBehavior.Editable = status;
            grcDonThuocdt.Enabled = !status;
            cboKieuPL.Properties.ReadOnly = !status;
        }
        private void ResetControl()
        {
            dtNgayKe.EditValue = System.DateTime.Now;
            // cboNhomDuoc.Text = "";
            lupBPKe.EditValue = 0;
            lupKhoXuat.EditValue = 0;
            lupNguoiKe.EditValue = "";
        }
        public class DV
        {
            public int? MaDV { get; set; }
            public string TenDV { get; set; }
            public string HamLuong { get; set; }
            public string SoLo { get; set; }
            public DateTime? HanDung { get; set; }
        }
        private bool KtraLuu()
        {
            if (dtNgayKe.EditValue == null || dtNgayKe.EditValue.ToString() == "")
            {
                MessageBox.Show("Ngày kê không hợp lệ!");
                dtNgayKe.Focus();
                return false;
            }
            if (lupBPKe.EditValue == null || string.IsNullOrEmpty(lupBPKe.Text))
            {
                MessageBox.Show("Bộ phận kê không hợp lệ");
                lupBPKe.Focus();
                return false;
            }
            if (lupKhoXuat.EditValue == null)
            {
                MessageBox.Show("Kho xuất không hợp lệ");
                lupKhoXuat.Focus();
                return false;
            }
            if (grvDonThuocct.GetRowCellValue(0, colIDThuoc) == null)
            {
                MessageBox.Show("Bạn chưa chọn thuốc");
                lupKhoXuat.Focus();
                return false;
            }
            bool isTrue = true;
            for (int i = 0; i < grvDonThuocct.RowCount; i++)
            {
                if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null)
                {
                    double soLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                    if (soLuong == 0)
                        isTrue = false;
                }
            }
            if (!isTrue)
            {
                MessageBox.Show("Có thuốc có số lượng bằng 0, vui lòng sửa lại!");
                return false;
            }

            bool checkton = true;
            string msg = "Các thuốc có số lượng tồn không đủ: ";
            foreach (var item in lupMaDuocdt.DataSource as List<MedicineInventoryModel>)
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
        int TrangThai = 0;
        List<DThuoc> _ldthuoc = new List<DThuoc>();
        List<DThuocctModel> _ldthuocct = new List<DThuocctModel>();
        List<int> deleteDThuoccts = new List<int>();
        DateTime _dttu = new DateTime();
        DateTime _dtden = new DateTime();
        int _makp = 0;
        int StatusDT = 1;
        int SoPL = -1;
        int _selIndex = -1;
        int ppxuat = -1;
        static double tonthuoc = 0;
        static double soluongt = 0;// số lượng một loại thuốc được kê trên cùng 1 đơn thuốc
        double _TT = 0;
        int iddon = 0;
        #region biến dùng để update tồn ở bảng MedicineList
        int maKhoaKe = 0;
        int TH = -1;
        int maKhoXuat = 0;
        int maDV = 0;
        double donGia = 0;
        string soLo = "";
        DateTime hanDung = new DateTime();
        double slKe = 0;
        #endregion


        bool isLoad = false;
        private void TimKiem()
        {
            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            if (!isLoad)
            {
                return;
            }

            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            _ldthuoc.Clear();

            if (lupTimBP.EditValue != null)
            {
                _makp = Convert.ToInt32(lupTimBP.EditValue);
            }

            grcDonThuocdt.DataSource = _medicinesProvider.GetListDThuoc(_dttu, _dtden, _makp);
        }
        bool BothuocKoSD = false;
        private void Frm_LinhHC_VTYT_Load(object sender, EventArgs e)
        {

            isLoad = false;

            if (DungChung.Bien.MaBV == "24012")
            {
                colHanDung.Visible = true;
            }
            else
            {
                colHanDung.Visible = false;
            }
            var a2 = (from dt in _dataContext.DThuocs.Where(p => p.KieuDon == 2).Where(p => p.MaBNhan == null)
                      join dtct in _dataContext.DThuoccts.Where(p => p.SoLuong < 0) on dt.IDDon equals dtct.IDDon
                      select dt).ToList();
            if (a2.Count > 0)
            {
                foreach (var b in a2)
                {
                    var sua = _dataContext.DThuocs.Single(p => p.IDDon == b.IDDon);
                    sua.KieuDon = 4;
                    _dataContext.SaveChanges();
                }
            }
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtTimDenNgay.DateTime = System.DateTime.Now;
            Enablebutton(true);
            EnableControl(false);
            var kp = (from kphong in _dataContext.KPhongs
                      where (kphong.Status == 1 && (kphong.PLoai.ToLower() == "lâm sàng" || kphong.PLoai.ToLower() == "phòng khám" || kphong.PLoai.ToLower() == "khoa dược" || kphong.PLoai.ToLower() == "cận lâm sàng" || kphong.PLoai.ToLower() == "tủ trực"))
                      select new { kphong.MaKP, kphong.TenKP, kphong.PLoai }).ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupTimBP.Properties.DataSource = kp;
            }
            else
            {
                var kptheotk = (from k in kp
                                join p in DungChung.Bien.listKPHoatDong on k.MaKP equals p
                                select k).ToList();
                lupTimBP.Properties.DataSource = kptheotk;
            }
            lupBPKe.Properties.DataSource = _medicinesProvider.GetListKhoaPhong("0", 5);
            lupKhoXuat.Properties.DataSource = _medicinesProvider.GetListKhoaPhong(DungChung.Bien.MaCB, 3);
            TrangThai = 0;
            lupTimBP.EditValue = DungChung.Bien.MaKP;
            binSDonThuocct.DataSource = _ldthuocct;
            grcDonThuocct.DataSource = binSDonThuocct;

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
            {
                panelGhiChu.Visible = true;
            }

            isLoad = true;

            TimKiem();
        }

        private void dtTimTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimBP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private bool isGrvDonThuocDtFocusedRowChanged = false;
        private void grvDonThuocdt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            isGrvDonThuocDtFocusedRowChanged = false;
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;

            if (grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != null && grvDonThuocdt.GetFocusedRowCellValue(colIDDon).ToString() != "")
            {
                lupMaDuocdt.DataSource = (from a in _dataContext.DichVus
                                          from b in _dataContext.MedicineLists
                                          where a.MaDV == b.MaDV
                                          select new MedicineInventoryModel()
                                          {
                                              IDThuoc = b.IDThuoc,
                                              MaDV = a.MaDV,
                                              TenDV = a.TenDV
                                          }).ToList(); ;

                iddon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));
                _ldthuoc = _dataContext.DThuocs.Where(p => p.IDDon == iddon).ToList();
                _ldthuocct = _medicinesProvider.ViewInfoMedicineDThuoc(iddon).OrderBy(p => p.SoPL).ThenBy(p => p.IDDonct).ToList();

                if (_ldthuoc.Count > 0)
                {
                    lupBPKe.EditValue = _ldthuoc.First().MaKP;
                    lupKhoXuat.EditValue = _ldthuoc.First().MaKXuat;
                    lupNguoiKe.EditValue = _ldthuoc.First().MaCB;

                    if (_ldthuoc.First().NgayKe != null)
                        dtNgayKe.DateTime = _ldthuoc.First().NgayKe.Value;

                    if (_ldthuoc.First().KieuDon == 3)
                    {
                        if (_medicinesProvider.isTuTruc((int)_ldthuoc.First().MaKP))
                        {
                            cboKieuPL.SelectedIndex = 2;
                        }
                        else
                        {
                            cboKieuPL.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        cboKieuPL.SelectedIndex = 1;
                    }

                    var sp = (from d in _ldthuocct select new { d.SoPL }).ToList();
                    if (sp.Count > 0 && sp.First().SoPL != null && sp.First().SoPL.ToString() != "")
                    {
                        SoPL = sp.First().SoPL;
                    }
                    else
                        SoPL = -1;

                }

                binSDonThuocct.DataSource = _ldthuocct;
                grcDonThuocct.DataSource = binSDonThuocct;
            }
            else
            {
                iddon = 0;
                StatusDT = 1;
                SoPL = -1;
                StatusDT = -1;
                grcDonThuocct.DataSource = "";
            }

            isGrvDonThuocDtFocusedRowChanged = true;
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            lupMaDuocdt.DataSource = medicinesByRoom;

            Enablebutton(false);
            EnableControl(true);
            BothuocKoSD = true;
            groupControl5.Enabled = false;
            ResetControl();
            lupBPKe.EditValue = DungChung.Bien.MaKP;
            lupNguoiKe.EditValue = DungChung.Bien.MaCB;
            binSDonThuocct.DataSource = _ldthuocct.Where(p => p.IDDon == 0).ToList();
            grcDonThuocct.DataSource = binSDonThuocct;
            TrangThai = 1;

            lupKhoXuat.EditValue = -1;
        }

        private void lupBPKe_EditValueChanged(object sender, EventArgs e)
        {
            if (TrangThai == 1 || TrangThai == 2)
            {
                lupNguoiKe.EditValue = 0;
            }
            lupKhoXuat_EditValueChanged(sender, e);

            int makp = 0;
            string makptk = "";
            if (lupBPKe.EditValue != null)
            {
                makp = Convert.ToInt32(lupBPKe.EditValue);
                makptk = ";" + makp + ";";
            }
            var _cb = _dataContext.CanBoes.Where(p => p.MaKPsd.Contains(makptk)).Where(p => p.Status == 1).ToList();
            if (_cb.Count > 0)
                lupNguoiKe.Properties.DataSource = _cb;
        }

        private void cboKieuPL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isGrvDonThuocDtFocusedRowChanged)
            {
                return;
            }

            if (TrangThai == 1 || TrangThai == 2)
            {
                int i = 0;
                lupBPKe.EditValue = -1;
                if (cboKieuPL.SelectedIndex == _selIndex)
                {
                    lupKhoXuat_EditValueChanged(sender, e);

                    return;
                }

                if (grvDonThuocct.RowCount >= 1 && grvDonThuocct.GetRowCellValue(i, colIDThuoc) != null)
                {
                    cboKieuPL.SelectedIndex = _selIndex;
                    lupKhoXuat_EditValueChanged(sender, e);

                    return;
                }
            }

            _selIndex = cboKieuPL.SelectedIndex;

            lupKhoXuat_EditValueChanged(sender, e);
        }

        private void lupKhoXuat_EditValueChanged(object sender, EventArgs e)
        {
            if (grvDonThuocdt.GetFocusedRowCellValue(colIDDon) != null)
                iddon = Convert.ToInt32(grvDonThuocdt.GetFocusedRowCellValue(colIDDon));

            if (lupBPKe.EditValue != null)
            {
                maKhoaKe = Convert.ToInt32(lupBPKe.EditValue);
            }

            if (lupKhoXuat.EditValue != null)
            {
                maKhoXuat = Convert.ToInt32(lupKhoXuat.EditValue);
                ppxuat = _medicinesProvider.GetPPXuat(maKhoXuat);
            }
            if (TrangThai == 1 || TrangThai == 2)
            {
                if (cboKieuPL.SelectedIndex == 0) //lĩnh dược về khoa
                {
                    lupBPKe.Properties.DataSource = _medicinesProvider.GetListKhoaPhong("0", 3);
                    lupMaDuocdt.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoXuat, iddon, 0);
                }
                else if (cboKieuPL.SelectedIndex == 2) //lĩnh về tủ trực
                {
                    lupBPKe.Properties.DataSource = _medicinesProvider.GetListKhoaPhong("0", 2);
                    lupMaDuocdt.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoXuat, iddon, 0);
                }
                else // trả dược
                {
                    lupBPKe.Properties.DataSource = _medicinesProvider.GetListKhoaPhong("0", 4);
                    lupMaDuocdt.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuocTraDuoc(maKhoaKe, maKhoXuat, iddon);
                }
            }

            if (ppxuat == 3)
            {
                colSoLo.Visible = colHanDung.Visible = true;
                colSoLo.VisibleIndex = 7;
                colHanDung.VisibleIndex = colSoLo.VisibleIndex + 1;
            }
            else
            {
                colSoLo.Visible = colHanDung.Visible = false;
            }

            if (lupBPKe.EditValue != null && lupKhoXuat.EditValue != null && cboKieuPL.EditValue != null && Convert.ToInt32(lupBPKe.EditValue) != -1 && Convert.ToInt32(lupKhoXuat.EditValue) != -1 && Convert.ToInt32(cboKieuPL.SelectedIndex) != -1)
            {
                grvDonThuocct.OptionsBehavior.Editable = true;
            }
            else
            {
                grvDonThuocct.OptionsBehavior.Editable = false;
            }
        }

        private class _dongia
        {
            public double DonGia { set; get; }
        }

        bool isUpdate = true;
        List<MedicineInventoryModel> lookupEditMedicine = new List<MedicineInventoryModel>();
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


                switch (e.Column.FieldName)
                {
                    case nameof(MedicineInventoryModel.IDThuoc):
                        {
                            isUpdate = true;

                            newIDThuoc = Convert.ToInt32(newValue);

                            int trongDM = _medicinesProvider.isTrongDMBHYT(maBN);

                            //lấy chi tiết đơn thuốc
                            lookupEditMedicine = lupMaDuocdt.DataSource as List<MedicineInventoryModel>;
                            if (lookupEditMedicine != null)
                                selectedMedicine = lookupEditMedicine.FirstOrDefault(p => p.IDThuoc == newIDThuoc);

                            if (oldValue != null) // trường hợp sửa tên thuốc
                            {
                                // SL trc khi thay đổi MaDV
                                if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                                    oldSL = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colSoLuong));
                                oldDonGia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia));

                                oldIDThuoc = Convert.ToInt32(oldValue);
                                if (grvDonThuocct.GetFocusedRowCellValue(colSoLo) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLo).ToString() != "")
                                    oldSoLo = Convert.ToString(grvDonThuocct.GetFocusedRowCellValue(colSoLo));
                                if (grvDonThuocct.GetFocusedRowCellValue(colHanDung) != null && grvDonThuocct.GetFocusedRowCellValue(colHanDung).ToString() != "")
                                    oldHanDung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));

                                _medicinesProvider.EditStockByIDThuoc(lupMaDuocdt, oldSL, 0, oldIDThuoc, 0, oldDonGia, 0, ppxuat);
                            }

                            if (selectedMedicine != null)
                            {
                                isUpdate = false;

                                grvDonThuocct.SetFocusedRowCellValue(colSoLo, selectedMedicine.SoLo);
                                grvDonThuocct.SetFocusedRowCellValue(colHanDung, selectedMedicine.HanDung);
                                grvDonThuocct.SetFocusedRowCellValue(colDonGia, selectedMedicine.DonGia);
                                grvDonThuocct.SetFocusedRowCellValue(colSoLuong, 0);
                                grvDonThuocct.SetFocusedRowCellValue(colDonVi, selectedMedicine.DonVi);

                                isUpdate = true;
                            }
                        }
                        break;
                    case nameof(DThuocctModel.SoLuong):
                        {
                            //if (ppxuat == 1)
                            //{
                            //    #region k theo so lo han dung
                            //    // Sửa số lượng thì trừ số lượng tồn trong danh mục tồn thuốc
                            //    var idThuocObj = gridView.GetRowCellValue(e.RowHandle, colIDThuoc);
                            //    var donGiaObj = gridView.GetRowCellValue(e.RowHandle, colDonGia);

                            //    if (gridView.ActiveEditor.OldEditValue != null && gridView.ActiveEditor.EditValue != null && idThuocObj != null && donGiaObj != null)
                            //    {
                            //        newIDThuoc = Convert.ToInt32(idThuocObj);
                            //        oldSL = Convert.ToDouble(gridView.ActiveEditor.OldEditValue);
                            //        newSL = Convert.ToDouble(gridView.ActiveEditor.EditValue);
                            //        newDonGia = Convert.ToDouble(donGiaObj);

                            //        lookupEditMedicine = lupMaDuocdt.DataSource as List<MedicineInventoryModel>;
                            //        if (lookupEditMedicine != null)
                            //            selectedMedicine = lookupEditMedicine.FirstOrDefault(p => p.IDThuoc == newIDThuoc);

                            //        if (isUpdate && selectedMedicine != null)
                            //        {
                            //            if(cboKieuPL.SelectedIndex == 0)
                            //            {
                            //                if(newSL <= 0)
                            //                {
                            //                    isUpdate = false;

                            //                    grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                            //                    MessageBox.Show("Số lượng phải lớn hơn 0", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            //                    isUpdate = true;
                            //                }
                            //                else if (newSL > (selectedMedicine.TonHienTai + oldSL))
                            //                {
                            //                    isUpdate = false;

                            //                    grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                            //                    MessageBox.Show("Số lượng thuốc không đủ", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            //                    isUpdate = true;
                            //                }
                            //                else 
                            //                {
                            //                    _medicinesProvider.EditStock(lupMaDuocdt, oldSL, newSL, newIDThuoc, newIDThuoc, newDonGia, newDonGia, ppxuat, "", new DateTime());
                            //                    this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + selectedMedicine.TonHienTai;

                            //                    grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, newSL);
                            //                }
                            //            }
                            //            else if(cboKieuPL.SelectedIndex == 1)
                            //            {
                            //                bool isTuTruc = _medicinesProvider.isTuTruc(maKhoaKe);

                            //                if(newSL >= 0)
                            //                {
                            //                    isUpdate = false;

                            //                    grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                            //                    MessageBox.Show("Đơn trả dược số lượng phải nhỏ hơn 0", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            //                    isUpdate = true;
                            //                }
                            //                else if (newSL < -(selectedMedicine.TonHienTai) || newSL < -(selectedMedicine.SLDaLinh))
                            //                {
                            //                    isUpdate = false;

                            //                    grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                            //                    MessageBox.Show("Số lượng thuốc không đủ", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            //                    isUpdate = true;
                            //                }
                            //                else
                            //                {
                            //                    //_medicinesProvider.EditStock(lupMaDuocdt, oldSL, newSL, newMaDV, newMaDV, newDonGia, newDonGia, ppxuat, "", new DateTime());
                            //                    this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + selectedMedicine.TonHienTai;
                            //                }
                            //            }
                            //        }
                            //    }
                            //    #endregion k theo so lo han dung
                            //}
                            //else
                            //{
                            #region theo so lo han dung
                            // Sửa số lượng thì trừ số lượng tồn trong danh mục tồn thuốc
                            var idThuocObj = gridView.GetRowCellValue(e.RowHandle, colIDThuoc);
                            var soLoObj = gridView.GetRowCellValue(e.RowHandle, colSoLo);
                            var hanDungObj = gridView.GetRowCellValue(e.RowHandle, colHanDung);
                            var donGiaObj = gridView.GetRowCellValue(e.RowHandle, colDonGia);

                            if (gridView.ActiveEditor.OldEditValue != null && gridView.ActiveEditor.EditValue != null && idThuocObj != null && soLoObj != null && hanDungObj != null && donGiaObj != null)
                            {
                                newIDThuoc = Convert.ToInt32(idThuocObj);
                                oldSL = Convert.ToDouble(gridView.ActiveEditor.OldEditValue);
                                newSL = Convert.ToDouble(gridView.ActiveEditor.EditValue);
                                newDonGia = Convert.ToDouble(donGiaObj);

                                newSoLo = soLoObj.ToString();
                                newHanDung = Convert.ToDateTime(hanDungObj);

                                lookupEditMedicine = lupMaDuocdt.DataSource as List<MedicineInventoryModel>;
                                if (lookupEditMedicine != null)
                                    selectedMedicine = lookupEditMedicine.FirstOrDefault(p => p.IDThuoc == newIDThuoc);

                                if (isUpdate && selectedMedicine != null)
                                {
                                    if (cboKieuPL.SelectedIndex == 1)
                                    {
                                        if (newSL >= 0)
                                        {
                                            isUpdate = false;

                                            grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                                            MessageBox.Show("Đơn trả dược số lượng phải nhỏ hơn 0", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                            isUpdate = true;
                                        }
                                        else if (newSL < -selectedMedicine.SLDaLinh)
                                        {
                                            isUpdate = false;

                                            grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                                            MessageBox.Show("Số lượng thuốc không đủ", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                            isUpdate = true;
                                        }
                                        else
                                        {
                                            //_medicinesProvider.EditStock(lupMaDuocdt, Math.Abs(oldSL), Math.Abs(newSL), newMaDV, newMaDV, newDonGia, newDonGia, ppxuat, newSoLo, newHanDung);

                                            var donGia = Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colDonGia));
                                            double thanhTien = Math.Round((double)(newSL * donGia), 2);

                                            grvDonThuocct.SetRowCellValue(e.RowHandle, colThanhTien, thanhTien);

                                            this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + selectedMedicine.TonHienTai;
                                        }
                                    }
                                    else
                                    {
                                        if (newSL <= 0)
                                        {
                                            isUpdate = false;

                                            grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                                            MessageBox.Show("Số lượng phải lớn hơn 0", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                            isUpdate = true;
                                        }
                                        else if (newSL > (selectedMedicine.TonHienTai + oldSL))
                                        {
                                            isUpdate = false;

                                            grvDonThuocct.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                                            MessageBox.Show("Số lượng thuốc không đủ", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                            isUpdate = true;
                                        }
                                        else
                                        {
                                            _medicinesProvider.EditStockByIDThuoc(lupMaDuocdt, oldSL, newSL, newIDThuoc, newIDThuoc, newDonGia, newDonGia, ppxuat);
                                            this.grvDonThuocct.ViewCaption = "Số lượng tồn: " + selectedMedicine.TonHienTai;

                                            var donGia = Convert.ToDouble(gridView.GetRowCellValue(e.RowHandle, colDonGia));
                                            double thanhTien = Math.Round((double)(newSL * donGia), 2);

                                            grvDonThuocct.SetRowCellValue(e.RowHandle, colThanhTien, thanhTien);
                                        }
                                    }
                                }
                            }
                            #endregion theo so lo han dung
                            //}
                        }
                        break;
                }
            }

            SetTextStock();
        }

        private void lupKhoXuat_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TrangThai == 1 || TrangThai == 2)
            {
                if (grvDonThuocct.RowCount < 1)
                {
                    //MessageBox.Show("OK"); 
                }
                else
                {
                    int i = 0;
                    if (grvDonThuocct.GetRowCellValue(i, colIDThuoc) != null)
                    {

                        MessageBox.Show("Đơn đã có thuốc bạn không được thay đổi kho kê");
                        e.Cancel = true;
                    }
                    else
                    { e.Cancel = false; }

                }
            }
        }

        private void lupBPKe_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TrangThai == 1 || TrangThai == 2)
            {
                if (grvDonThuocct.RowCount < 1)
                {
                    //MessageBox.Show("OK");
                }
                else
                {
                    int i = 0;
                    if (grvDonThuocct.GetRowCellValue(i, colIDThuoc) != null)
                    {
                        MessageBox.Show("Đơn đã có thuốc bạn không được thay đổi khoa kê");
                        e.Cancel = true;
                    }
                    else
                    { e.Cancel = false; }
                }
            }
        }

        private void grvDonThuocct_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn.FieldName.Equals(nameof(DThuocctModel.SoLuong)))
            {
                SetTextStock();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (iddon > 0)
            {
                if (cboKieuPL.SelectedIndex == 0) //lĩnh dược về khoa
                {
                    lupMaDuocdt.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoXuat, iddon, 0);
                }
                else // trả dược
                {
                    lupMaDuocdt.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuocTraDuoc(maKhoaKe, maKhoXuat, iddon);
                }

                BothuocKoSD = true;
                int _mabp = 0;
                if (lupBPKe.EditValue != null)
                    _mabp = Convert.ToInt32(lupBPKe.EditValue);
                if (DungChung.Bien.listKPHoatDong.Where(p => p == _mabp).Count() > 0 || DungChung.Bien.PLoaiKP == "Admin")
                {
                    var qdt = _dataContext.DThuoccts.Where(p => p.IDDon == iddon).ToList();
                    if (qdt.Count > 0)
                    {
                        if (qdt.Where(p => p.SoPL <= 0).Count() > 0)
                        {
                            Enablebutton(false);
                            EnableControl(true);
                            groupControl5.Enabled = false;
                            TrangThai = 2;
                        }
                        else if (qdt.Where(p => p.Status == null || p.Status <= 0).Count() > 0)
                            MessageBox.Show("Phiếu lĩnh đã in, bạn không thể sửa");
                        else
                            MessageBox.Show("Phiếu lĩnh đã được xuất dược, bạn không được sửa");
                    }
                    else
                        MessageBox.Show("Không có phiếu lĩnh để sửa");
                }
            }
            else
            {
                MessageBox.Show("Không có phiếu lĩnh để sửa");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KtraLuu())
            {
                #region thêm mới
                if (TrangThai == 1)
                {
                    DThuoc dthuoc = new DThuoc();
                    dthuoc.NgayKe = dtNgayKe.DateTime;
                    dthuoc.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                    dthuoc.MaCB = lupNguoiKe.EditValue.ToString();
                    dthuoc.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                    dthuoc.PLDV = 1;
                    if (cboKieuPL.SelectedIndex == 0 || cboKieuPL.SelectedIndex == 2)
                    {
                        dthuoc.KieuDon = 3;
                    }
                    else
                    { dthuoc.KieuDon = 4; }
                    _dataContext.DThuocs.Add(dthuoc);
                    if (_dataContext.SaveChanges() >= 0)
                    {
                        int maxid = 0;
                        var que = (from max in _dataContext.DThuocs.OrderByDescending(p => p.IDDon) select max.IDDon).ToList();
                        if (que.Count > 0)
                        {
                            maxid = int.Parse(que.First().ToString());
                        }
                        for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                        {
                            if (grvDonThuocct.GetRowCellDisplayText(i, colSoPLct) == null || (grvDonThuocct.GetRowCellDisplayText(i, colSoPLct) != null && Convert.ToInt32(grvDonThuocct.GetRowCellDisplayText(i, colSoPLct)) <= 0))
                            {
                                if (grvDonThuocct.GetRowCellValue(i, colIDThuoc) != null)
                                {
                                    if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "0" && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "")
                                    {
                                        DThuocct dthuocct = new DThuocct();
                                        dthuocct.SoPL = 0;
                                        dthuocct.IDDon = maxid;
                                        dthuocct.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                                        dthuocct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                        dthuocct.MaDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDThuoc)));
                                        dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong));
                                        dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                        dthuocct.DonGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                        dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien).ToString());
                                        dthuocct.NgayNhap = dtNgayKe.DateTime;
                                        if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                            dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                        if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                            dthuocct.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung).ToString());
                                        if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                            dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                        dthuocct.Status = 0;
                                        _dataContext.DThuoccts.Add(dthuocct);
                                        _dataContext.SaveChanges();
                                    }
                                }
                            }
                        }


                        //update table medicinelist
                        foreach (var item in lupMaDuocdt.DataSource as List<MedicineInventoryModel>)
                        {
                            if (item.TonKhaDung != item.TonHienTai)
                            {
                                if (cboKieuPL.SelectedIndex != 1) // k phai don tra thuoc
                                {
                                    _medicinesProvider.UpdateMedicineListPPX3((int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, maKhoaKe, maKhoXuat, item.TonKhaDung - item.TonHienTai, 0);
                                }
                            }
                        }

                        if (cboKieuPL.SelectedIndex == 0) //lĩnh dược về khoa
                        {
                            lupMaDuocdt.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoXuat, iddon, 0);
                        }
                        else // trả dược
                        {
                            lupMaDuocdt.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuocTraDuoc(maKhoaKe, maKhoXuat, iddon);
                        }

                        TrangThai = 0;
                        MessageBox.Show("Tạo đơn thành công!");
                        Enablebutton(true);
                        EnableControl(true);
                        //ResetControl();

                        groupControl5.Enabled = true;
                        Frm_LinhHC_VTYT_Load(sender, e);
                    }
                }
                #endregion
                #region sửa
                else
                {
                    if (TrangThai == 2)
                    {
                        if (iddon > 0)
                        {
                            int id = iddon;
                            var dthuoc = _dataContext.DThuocs.Single(p => p.IDDon == id);
                            dthuoc.NgayKe = dtNgayKe.DateTime;
                            dthuoc.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                            dthuoc.MaCB = lupNguoiKe.EditValue.ToString();
                            dthuoc.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                            dthuoc.PLDV = 1;
                            if (cboKieuPL.SelectedIndex == 0 || cboKieuPL.SelectedIndex == 2)
                            {
                                dthuoc.KieuDon = 3;
                            }
                            else
                            { dthuoc.KieuDon = 4; }
                            if (_dataContext.SaveChanges() >= 0)
                            {
                                // lưu chi tiết đơn
                                for (int i = 0; i < grvDonThuocct.DataRowCount; i++)
                                {
                                    if (grvDonThuocct.GetRowCellValue(i, colIDThuoc) != null)
                                    {
                                        if (grvDonThuocct.GetRowCellValue(i, colSoLuong) != null && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "0" && grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString() != "")
                                        {
                                            if (grvDonThuocct.GetRowCellValue(i, colIDDonct) != null && grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString() != "")
                                            {
                                                int idct = int.Parse(grvDonThuocct.GetRowCellValue(i, colIDDonct).ToString());
                                                if (idct > 0)// sửa row
                                                {
                                                    DThuocct dthuocct = _dataContext.DThuoccts.Single(p => p.IDDonct == idct);
                                                    dthuocct.IDDon = id;
                                                    dthuocct.MaKP = lupBPKe.EditValue == null ? 0 : Convert.ToInt32(lupBPKe.EditValue);
                                                    dthuocct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                                    dthuocct.MaDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDThuoc)));
                                                    dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    dthuocct.DonGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                                    dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString());
                                                    dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien).ToString());
                                                    dthuocct.NgayNhap = dtNgayKe.DateTime;
                                                    if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                                        dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                        dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                                        dthuocct.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                                                    dthuocct.Status = 0;
                                                    _dataContext.SaveChanges();
                                                }
                                                else
                                                {// lưu row mới 
                                                    DThuocct dthuocct = new DThuocct();
                                                    dthuocct.IDDon = id;
                                                    dthuocct.SoPL = 0;
                                                    dthuocct.MaKXuat = lupKhoXuat.EditValue == null ? 0 : Convert.ToInt32(lupKhoXuat.EditValue);
                                                    dthuocct.MaDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvDonThuocct.GetRowCellValue(i, colIDThuoc)));
                                                    dthuocct.DonVi = grvDonThuocct.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    dthuocct.DonGia = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colDonGia).ToString());
                                                    dthuocct.SoLuong = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colSoLuong).ToString());
                                                    dthuocct.ThanhTien = Convert.ToDouble(grvDonThuocct.GetRowCellValue(i, colThanhTien).ToString());
                                                    dthuocct.NgayNhap = dtNgayKe.DateTime;
                                                    if (grvDonThuocct.GetRowCellValue(i, colMaCC) != null)
                                                        dthuocct.MaCC = grvDonThuocct.GetRowCellValue(i, colMaCC).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colSoLo) != null)
                                                        dthuocct.SoLo = grvDonThuocct.GetRowCellValue(i, colSoLo).ToString();
                                                    if (grvDonThuocct.GetRowCellValue(i, colHanDung) != null)
                                                        dthuocct.HanDung = Convert.ToDateTime(grvDonThuocct.GetRowCellValue(i, colHanDung));
                                                    dthuocct.Status = 0;
                                                    _dataContext.DThuoccts.Add(dthuocct);
                                                    _dataContext.SaveChanges();
                                                }
                                            }
                                        }
                                    }
                                }

                                //if (_medicinesProvider.isTuTruc(maKhoXuat))
                                //    TH = 2;
                                //else
                                //    TH = 0;

                                //Xóa đơn
                                if (deleteDThuoccts.Count() > 0)
                                {
                                    foreach (var item in deleteDThuoccts)
                                    {
                                        _medicinesProvider.DeleteDThuocAndDThuocctbyIDDonct(item);
                                    }
                                }

                                //Cập nhật tồn
                                foreach (var item in lupMaDuocdt.DataSource as List<MedicineInventoryModel>)
                                {
                                    if (item.TonKhaDung != item.TonHienTai)
                                    {
                                        if (cboKieuPL.SelectedIndex != 1) // k phai don tra thuoc
                                        {
                                            _medicinesProvider.UpdateMedicineListPPX3((int)item.MaDV, item.DonGia, item.SoLo, (DateTime)item.HanDung, maKhoaKe, maKhoXuat, item.TonKhaDung - item.TonHienTai, 0);
                                        }
                                    }
                                }

                                if (cboKieuPL.SelectedIndex == 0) //lĩnh dược về khoa
                                {
                                    lupMaDuocdt.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoXuat, iddon, 0);
                                }
                                else // trả dược
                                {
                                    lupMaDuocdt.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuocTraDuoc(maKhoaKe, maKhoXuat, iddon);
                                }

                                TrangThai = 0;
                                MessageBox.Show("Sửa thành công!");
                                groupControl5.Enabled = true;
                                Enablebutton(true);
                                //ResetControl();
                                Frm_LinhHC_VTYT_Load(sender, e);

                            }
                        }
                    }
                }
                #endregion
            }
        }

        private void btnKLuu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Có dữ liệu chưa lưu! Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                groupControl5.Enabled = true;
                this.Frm_LinhHC_VTYT_Load(sender, e);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (iddon > 0)
            {
                int id = iddon;
                var sopl = (from dt in _dataContext.DThuocs.Where(p => p.IDDon == id)
                            join dtct in _dataContext.DThuoccts
                                on dt.IDDon equals dtct.IDDon
                            join dv in _dataContext.DichVus on dtct.MaDV equals dv.MaDV
                            select new { dt.MaKP, dtct.SoPL, dv.PLoai, dt.MaBNhanChiTiet }).Distinct().ToList();

                if (sopl.Count > 0)
                {
                    if (sopl.Where(p => p.SoPL == 0).Count() > 0)
                    {
                        FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi(id, 3);
                        frm.ShowDialog();
                        groupControl5.Enabled = true;
                        this.Frm_LinhHC_VTYT_Load(sender, e);
                    }
                    else
                    {
                        foreach (var a in sopl)
                        {
                            string _spl = a.SoPL.ToString();
                            int _makp1 = a.MaKP == null ? 0 : a.MaKP.Value;
                            FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi();
                            string[] pl1 = new string[2] { "", "" };
                            pl1[0] = _spl;
                            pl1[1] = _makp1.ToString();
                            frm.InPhieu(pl1, 3);

                        }
                    }
                }
                else
                {
                    FormNhap.frm_Check_moi frm = new FormNhap.frm_Check_moi(id, 3);
                    frm.ShowDialog();
                    groupControl5.Enabled = true;
                    this.Frm_LinhHC_VTYT_Load(sender, e);
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (iddon != 0)
            {
                int id = iddon;
                var kt = (from dt in _dataContext.DThuocs.Where(p => p.IDDon == id) join dtct in _dataContext.DThuoccts on dt.IDDon equals dtct.IDDon select new { dtct.Status, dtct.SoPL }).ToList();
                if (kt.Count > 0)
                {
                    if (kt.Where(p => p.Status != 0).Count() > 0)
                    {
                        MessageBox.Show("Có phiếu lĩnh đã xuất hoặc đã hủy, Bạn không được xóa");

                    }
                    else
                    {
                        if (kt.Where(p => p.SoPL > 0).Count() > 0)
                        {
                            var ploai = grvDonThuocdt.GetFocusedRowCellValue(colPLoai) != null ? grvDonThuocdt.GetFocusedRowCellValue(colPLoai).ToString() : "";
                            var mabnhanchitiet = grvDonThuocdt.GetFocusedRowCellValue(colMaBNhanChiTiet) != null ? grvDonThuocdt.GetFocusedRowCellValue(colMaBNhanChiTiet).ToString() : "";
                            if ((DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && ploai.ToString() == "Tủ trực" && !string.IsNullOrEmpty(mabnhanchitiet.ToString()))
                            {
                                DialogResult _result = MessageBox.Show("Bạn muốn xóa phiếu lĩnh?", "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    var dThuoccts = _medicinesProvider.GetDThuocctbyIDDon(id);
                                    foreach (var i in dThuoccts)
                                    {
                                        string sopl = "";
                                        sopl = i.SoPL.ToString();

                                        _medicinesProvider.DeleteDThuocAndDThuocctbyIDDonct(i.IDDonct);

                                        if (cboKieuPL.SelectedIndex != 1) // k phai don tra thuoc
                                        {
                                            _medicinesProvider.UpdateMedicineListPPX3((int)i.MaDV, i.DonGia, i.SoLo, (DateTime)i.HanDung, 0, (int)i.MaKXuat, -i.SoLuong, TH);
                                        }

                                        if (_dataContext.SaveChanges() > 0)
                                        {
                                            // dthuocct.sopl của đơn bù quan hệ 1-n vs dthuocct.dscbth của đơn kê bn -> khi xóa đơn bù thì sửa đơn kê bn dthuocct.dscbth = null & dthuocct.status = 0
                                            var editdt = _dataContext.DThuoccts.Where(p => p.DSCBTH == sopl).Select(p => p.IDDonct).ToList();
                                            foreach (var item in editdt)
                                            {
                                                var dtct = _dataContext.DThuoccts.Single(p => p.IDDonct == item);
                                                dtct.DSCBTH = null;
                                                dtct.Status = 0;
                                                _dataContext.SaveChanges();
                                            }
                                        }
                                    }
                                    _dataContext.SaveChanges();
                                    TimKiem();
                                }
                            }
                            else
                                MessageBox.Show("Phiếu lĩnh đã in, bạn không được xóa");
                        }
                        else
                        {
                            DialogResult _result = MessageBox.Show("Bạn muốn xóa phiếu lĩnh?", "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.Yes)
                            {
                                var dThuoccts = _medicinesProvider.GetDThuocctbyIDDon(id);
                                foreach (var i in dThuoccts)
                                {
                                    _medicinesProvider.DeleteDThuocAndDThuocctbyIDDonct(i.IDDonct);

                                    if (cboKieuPL.SelectedIndex != 1) // k phai don tra thuoc
                                    {
                                        _medicinesProvider.UpdateMedicineListPPX3((int)i.MaDV, i.DonGia, i.SoLo, (DateTime)i.HanDung, maKhoaKe, (int)i.MaKXuat, -i.SoLuong, 0);
                                    }
                                }
                                _dataContext.SaveChanges();
                                TimKiem();
                            }
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Không có phiếu lĩnh để xóa");
            }
        }

        private void grvDonThuocdt_DataSourceChanged(object sender, EventArgs e)
        {
            if (grvDonThuocdt.DataSource != null
                && ((IList)grvDonThuocdt.DataSource).Count == 0)
                isGrvDonThuocDtFocusedRowChanged = true;
            else
                isGrvDonThuocDtFocusedRowChanged = false;

            //grvDonThuocdt_FocusedRowChanged(null, null);
        }

        private void grvDonThuocct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetTextStock();
            if (TrangThai == 1 || TrangThai == 2)
            {
                if (grvDonThuocct.GetFocusedRowCellValue(colIDDonct) != null)
                {
                    if (grvDonThuocct.GetFocusedRowCellValue(colSoPLct) != null)
                    {
                        if (Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colSoPLct)) > 0)
                        {
                            colIDThuoc.OptionsColumn.ReadOnly = true;
                            colSoLuong.OptionsColumn.ReadOnly = true;
                        }
                        else
                        {
                            colIDThuoc.OptionsColumn.ReadOnly = false;
                            colSoLuong.OptionsColumn.ReadOnly = false;
                        }
                    }
                    else
                    {
                        colIDThuoc.OptionsColumn.ReadOnly = false;
                        colSoLuong.OptionsColumn.ReadOnly = false;
                    }
                }
                else
                {
                    colIDThuoc.OptionsColumn.ReadOnly = false;
                    colSoLuong.OptionsColumn.ReadOnly = false;
                }
            }
            else
            {
                colIDThuoc.OptionsColumn.ReadOnly = false;
                colSoLuong.OptionsColumn.ReadOnly = false;
            }
        }

        private void grvDonThuocct_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoactdt")
            {
                double sLuong = 0;
                if (grvDonThuocct.GetFocusedRowCellValue(colIDThuoc) != null && grvDonThuocct.GetFocusedRowCellValue(colIDThuoc).ToString() != "")
                    maDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDThuoc)));
                if (grvDonThuocct.GetFocusedRowCellValue(colSoLo) != null)
                    soLo = Convert.ToString(grvDonThuocct.GetFocusedRowCellValue(colSoLo));
                if (grvDonThuocct.GetFocusedRowCellValue(colHanDung) != null && grvDonThuocct.GetFocusedRowCellValue(colHanDung).ToString() != "")
                    hanDung = Convert.ToDateTime(grvDonThuocct.GetFocusedRowCellValue(colHanDung));
                if (grvDonThuocct.GetFocusedRowCellValue(colDonGia) != null && grvDonThuocct.GetFocusedRowCellValue(colDonGia).ToString() != "")
                    donGia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia));
                if (grvDonThuocct.GetFocusedRowCellValue(colSoLuong) != null && grvDonThuocct.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                    sLuong = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colSoLuong));
                if (lupBPKe.EditValue != null && lupBPKe.EditValue.ToString() != "")
                    maKhoaKe = Convert.ToInt32(lupBPKe.EditValue);
                if (lupKhoXuat.EditValue != null && lupKhoXuat.EditValue.ToString() != "")
                    maKhoXuat = Convert.ToInt32(lupKhoXuat.EditValue);


                if (TrangThai == 1)
                {
                    grvDonThuocct.DeleteSelectedRows();
                    _medicinesProvider.EditStock(lupMaDuocdt, sLuong, 0, maDV, 0, donGia, 0, ppxuat, soLo, hanDung);
                }
                else if (TrangThai == 2)
                {
                    int idct = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDDonct));
                    if (idct > 0)
                    {
                        var xoa = _dataContext.DThuoccts.Single(p => p.IDDonct == idct);


                        if (xoa.Status == 0)
                        {
                            if (xoa.SoPL > 0)
                            {
                                MessageBox.Show("Thuốc đã lên phiếu lĩnh, bạn không thể xóa");
                            }
                            else
                            {
                                DialogResult _result = MessageBox.Show("Bạn muốn xóa thuốc: " + grvDonThuocct.GetFocusedRowCellDisplayText(colIDThuoc).ToString(), "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.Yes)
                                {
                                    deleteDThuoccts.Add(idct);
                                    _medicinesProvider.EditStock(lupMaDuocdt, sLuong, 0, maDV, 0, donGia, 0, ppxuat, soLo, hanDung);

                                    grvDonThuocct.DeleteSelectedRows();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Thuốc đã hủy hoặc đã xuất, bạn không thể xóa");
                        }
                    }
                    else
                    {
                        _medicinesProvider.EditStock(lupMaDuocdt, sLuong, 0, maDV, 0, donGia, 0, ppxuat, soLo, hanDung);

                        grvDonThuocct.DeleteSelectedRows();
                    }

                }
            }
        }

        private void btnLinhThuoc_Click(object sender, EventArgs e)
        {
            frmPhieulinh frm = new frmPhieulinh();
            frm.ShowDialog();
        }

        private void btnDuTruThuoc_Click(object sender, EventArgs e)
        {
            FormThamSo.frm_DuTruThuoc frm = new FormThamSo.frm_DuTruThuoc();
            frm.ShowDialog();
        }

        private void grvDonThuocdt_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
            {
                if (grvDonThuocdt.GetRowCellValue(e.RowHandle, colIDDon) != null)
                {
                    var ploai = grvDonThuocdt.GetRowCellValue(e.RowHandle, colPLoai) != null ? grvDonThuocdt.GetRowCellValue(e.RowHandle, colPLoai).ToString() : "";
                    var mabnhanchitiet = grvDonThuocdt.GetRowCellValue(e.RowHandle, colMaBNhanChiTiet) != null ? grvDonThuocdt.GetRowCellValue(e.RowHandle, colMaBNhanChiTiet).ToString() : "";
                    if (ploai.ToString() != "Tủ trực")// lĩnh về khoa
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(mabnhanchitiet.ToString())) // bổ sung tủ trực
                        {
                            e.Appearance.ForeColor = Color.OrangeRed;
                        }
                        else // lĩnh bù tủ trực
                        {
                            e.Appearance.ForeColor = Color.Green;
                        }
                    }
                }
            }
        }

        private void SetTextStock()
        {
            int idThuoc = 0;

            if (grvDonThuocct.GetFocusedRowCellValue(colIDThuoc) == null)
            {
                this.grpDThuocct.Text = "Danh sách đơn thuốc";
            }
            if (grvDonThuocct.GetFocusedRowCellValue(colIDThuoc) != null && grvDonThuocct.GetFocusedRowCellValue(colIDThuoc).ToString() != "")
            {
                idThuoc = Convert.ToInt32(grvDonThuocct.GetFocusedRowCellValue(colIDThuoc));
                donGia = Convert.ToDouble(grvDonThuocct.GetFocusedRowCellValue(colDonGia));

                var medicine = medicinesByRoom.FirstOrDefault(p => p.IDThuoc == idThuoc);

                if (medicine != null)
                    this.grpDThuocct.Text = "Số lượng tồn: " + medicine.TonHienTai;
            }
        }

        private void grvDonThuocct_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void grvDonThuocdt_RowClick(object sender, RowClickEventArgs e)
        {
            grvDonThuocdt_FocusedRowChanged(sender, new FocusedRowChangedEventArgs(0, e.RowHandle));
        }
    }
    public class thuoc
    {
        public DateTime? HanDung { get; set; }
        public string TenDV { get; set; }
        public string SoLo { get; set; }
        public double? SL { get; set; }
        public string DonVi { get; set; }
        public double DonGia { get; set; }
        public int? MaDV { get; set; }
    }

    public class DonThuoc
    {
        public int IDDon { get; set; }
        public DateTime? NgayKe { get; set; }
        public int? KieuDon { get; set; }
        public int? MaBNhan { get; set; }
        public IEnumerable<int> ChuaLinhs { get; set; }
        public IEnumerable<int> DaLinhs { get; set; }
        public string ChuaLinh { get; set; }
        public string DaLinh { get; set; }
        public string PLoai { get; set; }
        public int? MaBNhanChiTiet { get; set; }
    }
}
