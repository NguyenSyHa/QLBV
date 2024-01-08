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
using QLBV.Class;
using DevExpress.XtraGrid;
using DevExpress.Utils.Paint;
using QLBV.FormNhap;
using QLBV.Providers.Business.Medicines;
using QLBV.Models.Dictionaries.KPhongs;
using DevExpress.XtraGrid.Views.Grid;
using QLBV.Models.Dictionaries.Thuoc;
using QLBV.Providers.StoredProcedure;
using QLBV.Models.Dictionaries.DichVu;

namespace QLBV.Forms.Medicines
{

    public partial class Frm_NhapDuoc : DevExpress.XtraEditors.XtraUserControl
    {
        private QLBVEntities _dataContext;
        private readonly MedicinesProvider _medicinesProvider;

        public Frm_NhapDuoc()
        {
            InitializeComponent();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _medicinesProvider = new MedicinesProvider();
        }
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        IList<NhapDModel> _lNhapDuoc = new List<NhapDModel>();
        IList<NhapDctModel> _lNhapDct = new List<NhapDctModel>();
        List<KPhong> _lkphong = new List<KPhong>();
        List<NhapDctss> _listND = new List<NhapDctss>();
        List<DungChung.Ham.giaSoLoHSD> _listGiaSua = new List<DungChung.Ham.giaSoLoHSD>();
        IList<KPhongModel> listKho = new List<KPhongModel>();
        IList<MedicineInventoryModel> medicinesByRoom = new List<MedicineInventoryModel>();
        IList<MedicineInventoryModel> allMedicines = new List<MedicineInventoryModel>();
        IList<KPhongModel> allKphongs = new List<KPhongModel>();

        int _makho = 0;
        string _macc = "-1";
        string _soPhieu = "";
        int TTLuu = 0;

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
        double soLuong = 0;
        #endregion


        class NhapDctss
        {
            public int IDNhapct { get; set; }
            public Nullable<int> IDNhap { get; set; }
            public Nullable<int> MaDV { get; set; }
            public string SoLo { get; set; }
            public string SoDangKy { get; set; }
            public Nullable<System.DateTime> HanDung { get; set; }
            public string DonVi { get; set; }
            public double DonGiaCT { get; set; }
            public double DonGia { get; set; }
            public int VAT { get; set; }
            public double ThanhTienTruocVAT { get; set; }
            public double SoLuongN { get; set; }
            public double ThanhTienN { get; set; }
            public double SoLuongX { get; set; }
            public double ThanhTienX { get; set; }
            public double SoLuongKK { get; set; }
            public double ThanhTienKK { get; set; }
            public double SoLuongSD { get; set; }
            public double ThanhTienSD { get; set; }
            public string MaCC { get; set; }
            public double DonGiaDY { get; set; }
            public double SoLuongDY { get; set; }
            public double ThanhTienDY { get; set; }
            public Nullable<int> MaBNhan { get; set; }
            public byte IDDTBN { get; set; }
            public double DonGiaX { get; set; }
            public string GhiChu { get; set; }
            public int TrongBH { get; set; }
            public int TyLeCK { get; set; }
            public Nullable<int> MienCT { get; set; }

            public virtual DichVu DichVu { get; set; }
            public virtual NhapD NhapD { get; set; }
        }
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
            dtNgayTT.Properties.ReadOnly = !status;
            txtSoCT.Properties.ReadOnly = !status;
            lupMaKP.Properties.ReadOnly = !status;
            lupBPTra.Properties.ReadOnly = !status;
            lupMaCC.Properties.ReadOnly = !status;

            txtGhiChu.Properties.ReadOnly = !status;
            cboNguoiGiao.Properties.ReadOnly = !status;
            cboThueVAT.Properties.ReadOnly = !status;
            grcNhap.Enabled = !status;
            cboNhap.Properties.ReadOnly = !status;
            txtDiaChi.Properties.ReadOnly = !status;
            lupHinhthucx.Properties.ReadOnly = !status;
        }
        private void EnableColumn(bool status)
        {
            colSoLo.OptionsColumn.AllowEdit = !status;
            colHanDung.OptionsColumn.AllowEdit = !status;
            colDonGiaCT.OptionsColumn.AllowEdit = !status;
            colVAT.OptionsColumn.AllowEdit = !status;
            colDonGiaCT.OptionsColumn.AllowFocus = !status;
            colDonGia.OptionsColumn.AllowFocus = !status;
            colVAT.OptionsColumn.AllowFocus = !status;

            lupMaDuoc.Columns["TonHienTai"].Visible = status;

            colMaCCct.Visible = !status;
            cboThueVAT.ReadOnly = status;
        }
        private void ResetControl()
        {
            dtNgayNhap.EditValue = System.DateTime.Now;
            txtIDNhap.Text = "";
            txtSoCT.Text = "";
            txtGhiChu.Text = "";
            cboNguoiGiao.Text = "";
            lupMaCC.EditValue = "";
            lupBPTra.ResetText();

            lupMaKP.EditValue = 0;
            lupHinhthucx.EditValue = -1;
            txtDiaChi.ResetText();
        }
        public class DVu
        {
            public int MaDV { set; get; }
            public string TenDV { set; get; }
            public string DonVi { set; get; }
            public string SoDK { get; set; }
            public string MaTam { set; get; }
            public string MaCC { set; get; }
            public int? Status { set; get; }
            public double DonGia { get; set; }
            public string HamLuong { get; set; }
            public string SoLo { get; set; }
            public DateTime? HanDung { get; set; }
        }
        List<DVu> _ldichvu = new List<DVu>();
        string _maCQCQ = "";

        private void Frm_NhapDuoc_Load(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV != "30372")
            {
                grvThanhTienVat.Visible = false;
            }
            string format = "n" + DungChung.Bien.LamTronSo;
            this.repositoryItemTextEdit1.Mask.EditMask = format;

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                this.lupMaDuoc.Columns.Clear();
                this.lupMaDuoc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDV", 200, "Tên dược"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaTam", 80, "Mã NB"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SoDK", 40, "Số ĐK"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DonVi", 50, "Đơn vị"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DonGia", 60, "Đơn Giá"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HamLuong", "Hàm lượng", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaCC", 60, "Mã NCC"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TinhTNhap", 70, "T.Trạng N"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NguonGoc", 50, "N.Gốc"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("YCSD", 70, "Y.Cầu SD"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PhuongPhap", 100, "P.Pháp S-P"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TyLeSP", 40, "% PP"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TyLeBQ", 40, "%BQ"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaDV", "Mã dược", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            }
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "27183")
            {
                colDonGia.OptionsColumn.ReadOnly = false;
                colDonGia.OptionsColumn.AllowFocus = true;
                colDonGia.OptionsColumn.AllowEdit = true;
                colVAT.OptionsColumn.ReadOnly = true;
                colVAT.OptionsColumn.AllowFocus = false;
            }
            else
            {
                if (DungChung.Bien.MaBV == "30009")
                {
                    colDonGia.OptionsColumn.ReadOnly = true;
                    colDonGia.OptionsColumn.AllowFocus = false;
                    colDonGiaCT.OptionsColumn.ReadOnly = false;
                    colDonGiaCT.OptionsColumn.AllowFocus = true;
                    colVAT.OptionsColumn.ReadOnly = false;
                    colVAT.OptionsColumn.AllowFocus = true;


                }
                else
                {
                    colDonGia.OptionsColumn.ReadOnly = true;
                    colDonGia.OptionsColumn.AllowFocus = false;
                    colVAT.OptionsColumn.ReadOnly = false;
                    colVAT.OptionsColumn.AllowFocus = true;
                }
            }
            if (DungChung.Bien.MaBV == "27022")
            {
                colGhiChu.Visible = true;
            }
            else
            {
                colGhiChu.Visible = false;
            }

            colDonGia.OptionsColumn.ReadOnly = false;
            colDonGia.OptionsColumn.AllowFocus = true;
            colDonGia.OptionsColumn.AllowEdit = true;

            if (DungChung.Bien.MaBV == "56789")
            {
                colThanhTien.OptionsColumn.ReadOnly = false;
                colThanhTien.OptionsColumn.AllowFocus = true;
            }

            var qCQCQ = _dataContext.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            if (qCQCQ != null)
                _maCQCQ = qCQCQ.MaChuQuan;
            lupHinhthucx.Properties.DataSource = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat();
            List<NhaCC> lNhacc_TK = new List<NhaCC>();
            List<NhaCC> lNhacc = new List<NhaCC>();
            Enablebutton(true);
            EnableControl(false);
            dtTimDenNgay.DateTime = System.DateTime.Now;
            dtTimTuNgay.DateTime = System.DateTime.Now;
            dtNgayNhap.EditValue = System.DateTime.Now;
            _lkphong = (from KhoaKham in _dataContext.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP) select KhoaKham).ToList();


            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupTimMaKP.Properties.DataSource = _lkphong.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || (p.PLoai.ToUpper() == DungChung.Bien.st_PhanLoaiKP.XaPhuong.ToUpper() && DungChung.Bien.MaBV == "30007")).ToList();
            }
            else
            {
                lupTimMaKP.Properties.DataSource = _medicinesProvider.GetListKhoaPhong("0", 1);
            }

            lupMaKP.Properties.DataSource = _dataContext.KPhongs.ToList();
            lupMaKPds.DataSource = _lkphong.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.QuayThuoc).ToList();
            lupBPTra.Properties.DataSource = _medicinesProvider.GetListKhoaPhong("0", 1);

            var qu = (from CungCap in _dataContext.NhaCCs.Where(p => p.Status == 1) select CungCap).OrderBy(p => p.TenCC).ToList();
            if (qu.Count > 0)
            {
                foreach (var a in qu)
                {
                    NhaCC moi2 = new NhaCC();
                    moi2.MaCC = a.MaCC;
                    moi2.TenCC = a.TenCC;
                    lNhacc.Add(moi2);
                }
                lupMaCC.Properties.DataSource = lNhacc.ToList().OrderBy(p => p.TenCC).ToList();

                lupNhaCCct.DataSource = qu.ToList().OrderBy(p => p.TenCC);
                lNhacc_TK.AddRange(lNhacc);
                lNhacc_TK.Add(new NhaCC { TenCC = " Tất cả", MaCC = "" });
                lupTimNCC.Properties.DataSource = lNhacc_TK.ToList().OrderBy(p => p.TenCC);
            }
            string _makpsd = "";
            if (lupMaKP.EditValue != null)
                _makpsd = ";" + lupMaKP.EditValue.ToString() + ";";

            if (cboNhap.SelectedIndex == 0)
                medicinesByRoom = _medicinesProvider.GetLupMaDuoc(_makho, -1, -1);
            else
                medicinesByRoom = _medicinesProvider.GetLupMaDuoc(_makho, -1, 0);
            lupMaDuoc.DataSource = lupIDThuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;

            allMedicines = (from a in _dataContext.DichVus
                            from b in _dataContext.MedicineLists
                            where a.MaDV == b.MaDV
                            select new MedicineInventoryModel()
                            {
                                IDThuoc = b.IDThuoc,
                                MaDV = a.MaDV,
                                TenDV = a.TenDV
                            }).ToList();
            TimKiem();
            int idct = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
            {
                idct = Convert.ToInt32(txtIDNhap.Text);
            }

            _lNhapDct = _medicinesProvider.ViewInfoMedicineNhapD(idct);
            binNhapDuocct.DataSource = _lNhapDct.ToList();
            grcNhapCT.DataSource = binNhapDuocct;
            chkNhap_CheckedChanged(sender, e);
            TTLuu = 0;


            if (DungChung.Bien.MaBV == "24012")
            {
                comboBoxEdit1.Properties.Items.Add("Báo cáo phân loại mua thuốc, vật tư, hóa chất");
                comboBoxEdit1.Properties.Items.Add("Phiếu lĩnh vật tư tiêu hao");
                comboBoxEdit1.Properties.Items.Add("Phiếu lĩnh hóa chất");
                comboBoxEdit1.Properties.Items.Add("Phiếu lĩnh thuốc");
            }

        }
        #region KT
        //Kiem tra trước khi lưu
        private bool KT()
        {
            if (dtNgayNhap.EditValue == null || dtNgayNhap.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chọn 'Ngày nhập'!");
                dtNgayNhap.Focus();
                return false;
            }
            if (lupMaKP.EditValue == null || lupMaKP.EditValue.ToString() == "")
            {
                MessageBox.Show("Bạn chưa chon kho nhập!");
                lupMaKP.Focus();
                return false;
            }
            else
            {
                if (Convert.ToInt32(lupMaKP.EditValue) == 2)
                {
                    if (lupHinhthucx.EditValue == null || lupHinhthucx.EditValue.ToString() == "")
                    {
                        MessageBox.Show("Bạn chưa chọn kiểu trả dược!");
                        lupHinhthucx.Focus();
                        return false;
                    }
                }
            }
            if (cboNhap.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa chọn hình thức nhập!");
                cboNhap.Focus();
                return false;
            }
            if (ppxuat == 3)
            {
                string dsthuoc = "";
                for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                {
                    if (grvNhapCT.GetRowCellValue(i, colSoLo) == null || grvNhapCT.GetRowCellValue(i, colSoLo).ToString().Trim() == "" ||
                        grvNhapCT.GetRowCellValue(i, colHanDung) == null || grvNhapCT.GetRowCellValue(i, colHanDung).ToString().Trim() == "")
                    {
                        if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                            dsthuoc += grvNhapCT.GetRowCellDisplayText(i, colMaDV);
                    }
                }

                if (!string.IsNullOrEmpty(dsthuoc))
                {
                    dsthuoc += " chưa nhập đủ số lô & hạn dùng";
                    MessageBox.Show(dsthuoc);
                    return false;
                }


            }
            if (cboNhap.SelectedIndex == 1)
            {
                #region Kiểm tra tổng số lượng nhập với số lượng thầu
                if (DungChung.Bien.MaBV == "30005")
                {
                    List<NhapDct> _lNhapDct = new List<NhapDct>();
                    for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                    {
                        if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                        {
                            if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "" && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "0")
                            {
                                int madv = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                double dongia = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colDonGiaCT));
                                double soluong = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuong));
                                _lNhapDct.Add(new NhapDct { MaDV = madv, SoLuongN = soluong });
                            }

                        }
                    }
                    _lNhapDct = (from nd in _lNhapDct group nd by nd.MaDV into kq1 select new NhapDct { MaDV = kq1.Key, SoLuongN = kq1.Sum(p => p.SoLuongN) }).ToList();

                    int idnhap = 0;
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                    {
                        idnhap = Convert.ToInt32(txtIDNhap.Text);
                    }
                    foreach (NhapDct ndct in _lNhapDct)
                    {
                        double slThau = 0;
                        string tenthuoc = "";
                        var qdv = _dataContext.DichVus.Where(p => p.MaDV == ndct.MaDV.Value).FirstOrDefault();
                        if (qdv != null && qdv.SLuong != null)
                        {
                            slThau = qdv.SLuong.Value;
                            tenthuoc = qdv.TenDV;
                        }
                        var qtongnhap = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 1 && p.KieuDon == 1).Where(p => p.IDNhap != idnhap)
                                         join ct in _dataContext.NhapDcts on nd.IDNhap equals ct.IDNhap
                                         where ct.MaDV == ndct.MaDV
                                         select new { ct.SoLuongN }).ToList();
                        double tongslDaNhap = 0;
                        if (qtongnhap.Count > 0)
                        {
                            tongslDaNhap = qtongnhap.Sum(p => p.SoLuongN);
                        }
                        if (tongslDaNhap + ndct.SoLuongN > slThau)
                        {
                            MessageBox.Show("Tổng thuốc " + tenthuoc + " (Mã thuốc: " + ndct.MaDV + ") " + "có số lượng nhập > số lượng thầu");
                            return false;
                        }
                    }

                }
                #endregion

                DialogResult _result = MessageBox.Show("Bạn chắc chắn chứng từ này được nhập theo hóa đơn?", "Hỏi chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                    return true;
                else
                    return false;
            }
            else
            {

                if (DungChung.Bien.MaBV == "06007")
                {
                    DialogResult _result = MessageBox.Show("chứng từ này không phải nhập theo hóa đơn?", "Hỏi chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                        return true;
                    else
                        return false;
                }
            }
            if (cboNhap.SelectedIndex == 0)// Nhap chuyen kho
            {
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

            return true;
        }
        #endregion
        private void suaXuatNoiBo(int idNhapDc)
        {
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // luu bang NhapD
            bool luutt = true;
            if (KT() && DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayNhap.DateTime, "KhoaDC") == false)
            {
                switch (TTLuu)
                {
                    case 1:
                        #region thêm mới
                        string thuockluu = "các thuốc không được lưu:\n";
                        int _ttthuockluu = 0;
                        if (cboNhap.SelectedIndex == 2)
                        {
                            MessageBox.Show("Chức năng không được cho phép");
                        }
                        else
                        {
                            NhapD nhap = new NhapD();
                            nhap.PLoai = 1;
                            nhap.NgayNhap = dtNgayNhap.DateTime;

                            if (ckNgayTT.Checked == true)
                                nhap.NgayTT = dtNgayTT.DateTime;
                            else
                                nhap.NgayTT = null;
                            nhap.MaKPnx = lupBPTra.EditValue == null ? 0 : Convert.ToInt32(lupBPTra.EditValue);
                            nhap.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
                            nhap.MaCC = lupMaCC.EditValue.ToString();


                            nhap.TenNguoiCC = cboNguoiGiao.Text;
                            if (!string.IsNullOrEmpty(txtGhiChu.Text))
                                nhap.GhiChu = txtGhiChu.Text;
                            if (!string.IsNullOrEmpty(txtSoCT.Text))
                                nhap.SoCT = txtSoCT.Text;
                            else
                                nhap.SoCT = "";
                            nhap.KieuDon = cboNhap.SelectedIndex;
                            if (lupHinhthucx.EditValue != null)
                                nhap.TraDuoc_KieuDon = Convert.ToInt32(lupHinhthucx.EditValue);
                            nhap.XuatTD = 0;
                            nhap.MaCB = DungChung.Bien.MaCB;
                            nhap.DiaChi = txtDiaChi.Text.Trim();
                            nhap.SoPhieu = DungChung.Ham._idphieutheokp(1, Convert.ToInt32(lupMaKP.EditValue));

                            if (cboNhap.SelectedIndex == 0)
                                nhap.Status = -3;
                            _dataContext.NhapDs.Add(nhap);
                            List<DichVu> listDichVus = new List<DichVu>();
                            if (_dataContext.SaveChanges() >= 0)
                            {
                                int idnhap = nhap.IDNhap;

                                for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                                {
                                    if (ppxuat == 3 ? grvNhapCT.GetRowCellValue(i, colIDThuoc) != null : grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                                    {
                                        if (grvNhapCT.GetRowCellValue(i, colDonGiaCT) != null && grvNhapCT.GetRowCellValue(i, colDonGiaCT).ToString() != "")
                                        {
                                            if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "" && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "0")
                                            {
                                                NhapDct nhapdct = new NhapDct();
                                                nhapdct.IDNhap = idnhap;

                                                if(ppxuat == 1 || (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập theo hóa đơn(nhà cung cấp)") && grvNhapCT.GetRowCellValue(i, colMaDV)!= null)
                                                    nhapdct.MaDV = maDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                else if(ppxuat == 3 && grvNhapCT.GetRowCellValue(i, colIDThuoc) != null)
                                                    nhapdct.MaDV = maDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDThuoc)));

                                                nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                nhapdct.DonGiaCT = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGiaCT).ToString());
                                                nhapdct.DonGia = donGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                nhapdct.SoLuongN = soLuong = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());

                                                //double b = grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null ? (double)grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) : 0;
                                                // c = grvNhapCT.GetFocusedRowCellValue(colSoLo) != null ? (string)grvNhapCT.GetFocusedRowCellValue(colSoLo) : "";
                                                int makhotra = lupBPTra.EditValue == null ? 0 : Convert.ToInt32(lupBPTra.EditValue);

                                                nhapdct.ThanhTienN = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                if (grvNhapCT.GetRowCellValue(i, colMaCCct) != null)
                                                    nhapdct.MaCC = grvNhapCT.GetRowCellValue(i, colMaCCct).ToString();
                                                else
                                                    nhapdct.MaCC = "";
                                                nhapdct.SoLuongX = 0;
                                                nhapdct.ThanhTienX = 0;
                                                nhapdct.SoLuongSD = 0;
                                                nhapdct.ThanhTienSD = 0;
                                                nhapdct.SoLuongKK = 0;
                                                nhapdct.ThanhTienKK = 0;
                                                nhapdct.VAT = int.Parse(grvNhapCT.GetRowCellValue(i, colVAT).ToString());
                                                nhapdct.TyLeCK = int.Parse(grvNhapCT.GetRowCellValue(i, colTyLeCK).ToString());
                                                if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                    nhapdct.SoLo = soLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                else
                                                    nhapdct.SoLo = soLo = "";
                                                if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                    nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                    nhapdct.HanDung = hanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                if (grvNhapCT.GetRowCellValue(i, colGiaNhapDY) != null && grvNhapCT.GetRowCellValue(i, colGiaNhapDY).ToString() != "")
                                                {
                                                    nhapdct.DonGiaDY = double.Parse(grvNhapCT.GetRowCellValue(i, colGiaNhapDY).ToString());
                                                }
                                                else
                                                {
                                                    nhapdct.DonGiaDY = 0;
                                                }
                                                if (grvNhapCT.GetRowCellValue(i, colSoLuongDY) != null && grvNhapCT.GetRowCellValue(i, colSoLuongDY).ToString() != "")
                                                    nhapdct.SoLuongDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuongDY));
                                                else
                                                    nhapdct.SoLuongDY = 0;
                                                if (grvNhapCT.GetRowCellValue(i, colThanhTienDY) != null && grvNhapCT.GetRowCellValue(i, colThanhTienDY).ToString() != "")
                                                    nhapdct.ThanhTienDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colThanhTienDY));
                                                else
                                                    nhapdct.ThanhTienDY = 0;

                                                if (grvNhapCT.GetRowCellValue(i, colGhiChu) != null && grvNhapCT.GetRowCellValue(i, colGhiChu).ToString() != "")
                                                    nhapdct.GhiChu = grvNhapCT.GetRowCellValue(i, colGhiChu).ToString();
                                                _dataContext.NhapDcts.Add(nhapdct);
                                                listDichVus.Add(new DichVu { MaDV = (nhapdct.MaDV ?? 0) });
                                                try
                                                {
                                                    _dataContext.SaveChanges();
                                                }
                                                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                                                {
                                                    Exception raise = dbEx;

                                                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                                                    {

                                                        foreach (var validationError in validationErrors.ValidationErrors)
                                                        {

                                                            string message = string.Format("{0}:{1}",

                                                              validationErrors.Entry.Entity.ToString(),

                                                                validationError.ErrorMessage);

                                                            // raise a new exception nesting

                                                            // the current instance as InnerException

                                                            raise = new InvalidOperationException(message, raise);

                                                        }

                                                    }

                                                    throw raise;
                                                }

                                                if (cboNhap.SelectedIndex != 0)
                                                {
                                                    TH = 4;
                                                    int maKhoNhap = lupMaKP.EditValue != null ? Convert.ToInt32(lupMaKP.EditValue) : 0;
                                                    int khoXuat = lupBPTra.EditValue != null ? Convert.ToInt32(lupBPTra.EditValue) : 0;

                                                    _medicinesProvider.UpdateMedicineListPPX3(maDV, donGia, soLo, (DateTime)hanDung, maKhoNhap, khoXuat, soLuong, TH);

                                                    lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoNhap, idnhap, 0);
                                                }
                                            }
                                            else
                                            {
                                                thuockluu += grvNhapCT.GetRowCellValue(i, colMaDV).ToString() + ", ";
                                                _ttthuockluu = 1;
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
                            if (luutt)
                            {
                                if (listDichVus.Count > 0 && (DungChung.Bien.MaBV == "200012")) //tét
                                {
                                    DungChung.Ham.UpdateTonDichVu(listDichVus, nhap.MaKP ?? 0);
                                }
                                if (_ttthuockluu == 1)
                                    MessageBox.Show(thuockluu);
                                TTLuu = 0;
                                Enablebutton(true);
                                EnableControl(false);
                                TimKiem();
                            }
                        }
                        break;
                    #endregion
                    case 2:
                        #region sửa
                        if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        {
                            int id = Convert.ToInt32(txtIDNhap.Text);
                            string thuockluus = "các thuốc không được lưu:\n";
                            int _ttthuockluus = 0;
                            NhapD nhaps = _dataContext.NhapDs.Single(p => p.IDNhap == id);
                            nhaps.NgayNhap = dtNgayNhap.DateTime;
                            if (ckNgayTT.Checked == true)
                                nhaps.NgayTT = dtNgayTT.DateTime;
                            else
                                nhaps.NgayTT = null;
                            nhaps.MaKP = lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue);
                            nhaps.MaCC = lupMaCC.EditValue.ToString();
                            nhaps.TenNguoiCC = cboNguoiGiao.Text;
                            nhaps.MaCB = DungChung.Bien.MaCB;
                            nhaps.MaKPnx = lupBPTra.EditValue == null ? 0 : Convert.ToInt32(lupBPTra.EditValue);
                            nhaps.DiaChi = txtDiaChi.Text.Trim();
                            if (!string.IsNullOrEmpty(txtGhiChu.Text))
                                nhaps.GhiChu = txtGhiChu.Text;
                            if (!string.IsNullOrEmpty(txtSoCT.Text))
                                nhaps.SoCT = txtSoCT.Text;
                            nhaps.KieuDon = cboNhap.SelectedIndex;
                            nhaps.SoPhieu = txtsophieunew.Text;
                            if (lupHinhthucx.EditValue != null)
                                nhaps.TraDuoc_KieuDon = Convert.ToInt32(lupHinhthucx.EditValue);

                            if (cboNhap.SelectedIndex == 0)
                                nhaps.Status = -3;
                            _dataContext.SaveChanges();
                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD
                            List<DichVu> listDichVu1s = new List<DichVu>();

                            for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                            {
                                if (ppxuat == 3 ? grvNhapCT.GetRowCellValue(i, colIDThuoc) != null : grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                                {
                                    if (grvNhapCT.GetRowCellValue(i, colDonGiaCT) != null && grvNhapCT.GetRowCellValue(i, colDonGiaCT).ToString() != "")
                                    {
                                        if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "" && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "0")
                                        {
                                            int idct = 0;
                                            if (grvNhapCT.GetRowCellValue(i, colIDNhapct) != null && grvNhapCT.GetRowCellValue(i, colIDNhapct).ToString() != "")
                                            {
                                                idct = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDNhapct).ToString());
                                                if (idct <= 0) // them row moi
                                                {
                                                    NhapDct nhapdct = new NhapDct();
                                                    nhapdct.IDNhap = id;

                                                    if (ppxuat == 1 || (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập theo hóa đơn(nhà cung cấp)") && grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                                                        nhapdct.MaDV = maDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    else if (ppxuat == 3 && grvNhapCT.GetRowCellValue(i, colIDThuoc) != null)
                                                        nhapdct.MaDV = maDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDThuoc)));

                                                    nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    if (grvNhapCT.GetRowCellValue(i, colMaCCct) != null)
                                                        nhapdct.MaCC = grvNhapCT.GetRowCellValue(i, colMaCCct).ToString();
                                                    else
                                                        nhapdct.MaCC = "";
                                                    nhapdct.DonGiaCT = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGiaCT).ToString());
                                                    nhapdct.DonGia = donGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdct.SoLuongN = soLuong = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                    //if (nhapdct.SoLuongN > DungChung.Bien.SoLuongTon && DungChung.Bien.MaBV == "24012" && cboNhap.Text == "Nhập chuyển kho")
                                                    //{
                                                    //    MessageBox.Show("Số lượng nhập lớn hơn số lượng tồn! Lưu không thành công!");
                                                    //    return;
                                                    //}
                                                    nhapdct.ThanhTienN = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                    nhapdct.SoLuongX = 0;
                                                    nhapdct.ThanhTienX = 0;
                                                    nhapdct.SoLuongSD = 0;
                                                    nhapdct.ThanhTienSD = 0;
                                                    nhapdct.SoLuongKK = 0;
                                                    nhapdct.ThanhTienKK = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colVAT) != null && grvNhapCT.GetRowCellValue(i, colVAT).ToString() != "")
                                                        nhapdct.VAT = int.Parse(grvNhapCT.GetRowCellValue(i, colVAT).ToString());
                                                    else
                                                        nhapdct.VAT = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colTyLeCK) != null && grvNhapCT.GetRowCellValue(i, colTyLeCK).ToString() != "")
                                                        nhapdct.TyLeCK = int.Parse(grvNhapCT.GetRowCellValue(i, colTyLeCK).ToString());
                                                    else
                                                        nhapdct.TyLeCK = 0;

                                                    if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                        nhapdct.SoLo = soLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    else
                                                        nhapdct.SoLo = "";
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdct.HanDung = hanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                    if (grvNhapCT.GetRowCellValue(i, colGiaNhapDY) != null && grvNhapCT.GetRowCellValue(i, colGiaNhapDY).ToString() != "")
                                                    {
                                                        nhapdct.DonGiaDY = double.Parse(grvNhapCT.GetRowCellValue(i, colGiaNhapDY).ToString());
                                                    }
                                                    else
                                                    {
                                                        nhapdct.DonGiaDY = 0;
                                                    }
                                                    if (grvNhapCT.GetRowCellValue(i, colSoLuongDY) != null && grvNhapCT.GetRowCellValue(i, colSoLuongDY).ToString() != "")
                                                        nhapdct.SoLuongDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuongDY));
                                                    else
                                                        nhapdct.SoLuongDY = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colThanhTienDY) != null && grvNhapCT.GetRowCellValue(i, colThanhTienDY).ToString() != "")
                                                        nhapdct.ThanhTienDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colThanhTienDY));
                                                    else
                                                        nhapdct.ThanhTienDY = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colGhiChu) != null)
                                                        nhapdct.GhiChu = grvNhapCT.GetRowCellValue(i, colGhiChu).ToString();
                                                    _dataContext.NhapDcts.Add(nhapdct);
                                                    _dataContext.SaveChanges();
                                                    listDichVu1s.Add(new DichVu { MaDV = (nhapdct.MaDV ?? 0) });

                                                    if (cboNhap.SelectedIndex != 0)
                                                    {
                                                        TH = 4;
                                                        int maKhoNhap = Convert.ToInt32(lupMaKP.EditValue);

                                                        _medicinesProvider.UpdateMedicineListPPX3(maDV, donGia, soLo, (DateTime)hanDung, maKhoXuat, 0, soLuong, TH);

                                                        lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoNhap, id, 0);
                                                    }
                                                }
                                                else
                                                {
                                                    DungChung.Ham.CheckBangTonDuoc(_dataContext, Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV)), lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue), double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString()));
                                                    NhapDct nhapdcts = _dataContext.NhapDcts.Single(p => p.IDNhapct == idct);

                                                    if (ppxuat == 1 || (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập theo hóa đơn(nhà cung cấp)") && grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                                                        nhapdcts.MaDV = maDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    else if (ppxuat == 3 && grvNhapCT.GetRowCellValue(i, colIDThuoc) != null)
                                                        nhapdcts.MaDV = maDV = _medicinesProvider.GetMaDVbyIDThuoc(Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colIDThuoc)));

                                                    if (grvNhapCT.GetRowCellValue(i, colMaCCct) != null)
                                                        nhapdcts.MaCC = grvNhapCT.GetRowCellValue(i, colMaCCct).ToString();
                                                    else
                                                        nhapdcts.MaCC = "";
                                                    nhapdcts.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdcts.DonGiaCT = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGiaCT).ToString());
                                                    nhapdcts.DonGia = donGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdcts.SoLuongN = soLuong = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
                                                    nhapdcts.ThanhTienN = double.Parse(grvNhapCT.GetRowCellValue(i, colThanhTien).ToString());
                                                    if (grvNhapCT.GetRowCellValue(i, colVAT) != null && grvNhapCT.GetRowCellValue(i, colVAT).ToString() != "")
                                                        nhapdcts.VAT = int.Parse(grvNhapCT.GetRowCellValue(i, colVAT).ToString());
                                                    else
                                                        nhapdcts.VAT = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colTyLeCK) != null && grvNhapCT.GetRowCellValue(i, colTyLeCK).ToString() != "")
                                                        nhapdcts.TyLeCK = int.Parse(grvNhapCT.GetRowCellValue(i, colTyLeCK).ToString());
                                                    else
                                                        nhapdcts.TyLeCK = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colSoLo) != null && grvNhapCT.GetRowCellValue(i, colSoLo).ToString() != "")
                                                        nhapdcts.SoLo = soLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    else
                                                        nhapdcts.SoLo = "";
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdcts.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdcts.HanDung = hanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                    if (grvNhapCT.GetRowCellValue(i, colGiaNhapDY) != null && grvNhapCT.GetRowCellValue(i, colGiaNhapDY).ToString() != "")
                                                    {
                                                        nhapdcts.DonGiaDY = double.Parse(grvNhapCT.GetRowCellValue(i, colGiaNhapDY).ToString());
                                                    }
                                                    else
                                                    {
                                                        nhapdcts.DonGiaDY = 0;
                                                    }
                                                    if (grvNhapCT.GetRowCellValue(i, colSoLuongDY) != null && grvNhapCT.GetRowCellValue(i, colSoLuongDY).ToString() != "")
                                                        nhapdcts.SoLuongDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuongDY));
                                                    else
                                                        nhapdcts.SoLuongDY = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colThanhTienDY) != null && grvNhapCT.GetRowCellValue(i, colThanhTienDY).ToString() != "")
                                                        nhapdcts.ThanhTienDY = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colThanhTienDY));
                                                    else
                                                        nhapdcts.ThanhTienDY = 0;
                                                    if (grvNhapCT.GetRowCellValue(i, colGhiChu) != null)
                                                        nhapdcts.GhiChu = grvNhapCT.GetRowCellValue(i, colGhiChu).ToString();
                                                    _dataContext.SaveChanges();
                                                    listDichVu1s.Add(new DichVu { MaDV = (nhapdcts.MaDV ?? 0) });


                                                    if (cboNhap.SelectedIndex != 0)
                                                    {
                                                        TH = 4;
                                                        int maKhoNhap = lupMaKP.EditValue != null ? Convert.ToInt32(lupMaKP.EditValue) : 0;
                                                        int khoXuat = lupBPTra.EditValue != null ? Convert.ToInt32(lupBPTra.EditValue) : 0;

                                                        if(idct == 0)
                                                            _medicinesProvider.UpdateMedicineListPPX3(maDV, donGia, soLo, (DateTime)hanDung, maKhoNhap, khoXuat, soLuong, TH);

                                                        lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoNhap, id, 0);
                                                    }
                                                }
                                            }
                                        }
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

                            //Xóa đơn
                            if (deleteNhapdcts.Count() > 0)
                            {
                                foreach (var item in deleteNhapdcts)
                                {
                                    if (cboNhap.SelectedIndex != 0)
                                    {
                                        var a = _dataContext.NhapDcts.FirstOrDefault(p => p.IDNhapct == item);
                                        if(a != null)
                                        {
                                            var nhapD = _dataContext.NhapDs.FirstOrDefault(p => p.IDNhap == (int)a.IDNhap);

                                            _medicinesProvider.UpdateMedicineListPPX3((int)a.MaDV, a.DonGia, a.SoLo, (DateTime)a.HanDung, (int)nhapD.MaKP, (int)nhapD.MaKPnx, -a.SoLuongN, 4);
                                        }
                                    }
                                    _medicinesProvider.DeleteNhapDAndNhapDctbyIDNhapct(item);
                                }

                                int maKhoNhap = Convert.ToInt32(lupMaKP.EditValue);
                                lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoNhap, id, 0);

                                deleteNhapdcts.Clear();
                            }

                            if (luutt)
                            {
                                if (listDichVu1s.Count > 0 && (DungChung.Bien.MaBV == "200012")) // tét
                                {
                                    DungChung.Ham.UpdateTonDichVu(listDichVu1s, nhaps.MaKP ?? 0);
                                }
                                if (_ttthuockluus == 1)
                                    MessageBox.Show(thuockluus);
                                TTLuu = 0;
                                Enablebutton(true);
                                EnableControl(false);
                                grvNhap_FocusedRowChanged(null, null);
                            }
                        }
                        break;
                        #endregion
                }
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            colSoLo.OptionsColumn.AllowEdit = true;
            colHanDung.OptionsColumn.AllowEdit = true;
            colDonGiaCT.OptionsColumn.AllowEdit = true;
            colDonGiaCT.OptionsColumn.AllowFocus = true;
            colDonGia.OptionsColumn.AllowFocus = false;
            colSoLuong.OptionsColumn.AllowEdit = true;

            if (DungChung.Ham.checkQuyen(this.Name)[0])
            {
                lupMaKP.Properties.DataSource = _medicinesProvider.GetListKhoaPhong("0", 1);

                Enablebutton(false);
                EnableControl(true);
                ResetControl();
                cboNhap.SelectedIndex = -1;
                txtSoCT.Focus();
                lupMaKP.EditValue = DungChung.Bien.MaKP;
                _lNhapDct = _medicinesProvider.ViewInfoMedicineNhapD(0);
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
                TTLuu = 1;
                lupMaCC_EditValueChanged(null, null);
                dtNgayTT.DateTime = DateTime.Now;
                cboThueVAT.SelectedIndex = 1;

                if (cboNhap.SelectedIndex == 2)
                {
                    grvNhapCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

                }
                else
                    grvNhapCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            }
            else
            {
                MessageBox.Show("Chức năng bị giới hạn");
            }
        }

        private void lupMaCC_EditValueChanged(object sender, EventArgs e)
        {
            setlydonhap();
            int kdon = (cboNhap.SelectedIndex);
            string _makpsd = "";
            if (lupMaKP.EditValue != null)
                _makpsd = ";" + lupMaKP.EditValue.ToString() + ";";

            List<DichVu> ldv = (from dv in _dataContext.DichVus.Where(p => (p.PLoai == 1 || p.PLoai == 5) && p.MaKPsd.Contains(_makpsd)) select dv).OrderBy(p => p.TenDV).ToList();

            _ldichvu = new List<DVu>();

            if (lupMaCC.EditValue != null && lupMaCC.EditValue.ToString() != "" && (TTLuu == 1 || TTLuu == 2))
            {
                _macc = lupMaCC.EditValue.ToString();
                var que = (from NguoiGiao in _dataContext.NhaCCs where NguoiGiao.MaCC == (_macc) select new { NguoiGiao.NguoiCC, NguoiGiao.DiaChi }).ToList();
                if (que.Count > 0)
                {
                    cboNguoiGiao.Text = que.First().NguoiCC == null ? "" : que.First().NguoiCC.ToString();
                    txtDiaChi.Text = que.First().DiaChi == null ? "" : que.First().DiaChi.ToString();
                }
            }
            else
                _macc = "-1";

            try
            {
                if (TTLuu == 1 || TTLuu == 2)
                {
                    //lupBPTra
                    int _makho = 0;
                    int _makhotra = 0;
                    int _ql = -1;
                    if (lupMaKP.EditValue != null)
                        _makho = Convert.ToInt32(lupMaKP.EditValue);
                    if (lupBPTra.EditValue != null)
                        _makhotra = Convert.ToInt32(lupBPTra.EditValue);
                    //var kho = _dataContext.KPhongs.Where(p => p.MaKP == _makho).Select(p => p.QuanLy).ToList();
                    //if (kho.Count > 0 && kho.First() != null)
                    //    _ql = kho.First().Value;
                    //if (_ql == 1)
                    //    lupMaDuoc.DataSource = _medicinesProvider.GetLupMaDuoc(_makho, 0);
                    //else
                    //{
                    //    if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && cboNhap.Text == "Nhập chuyển kho")
                    //    {
                    //        if (ppxuat == 3)
                    //        {
                    //            var duoc2 = (from nhapduoc in _dataContext.NhapDcts.Where(p => p.SoLuongN > p.SoLuongX)
                    //                         join nduoc in _dataContext.NhapDs.Where(p => p.MaKP == _makhotra) on nhapduoc.IDNhap equals nduoc.IDNhap
                    //                         select new { nhapduoc.MaDV, nduoc.MaKP }
                    //       ).ToList();
                    //            var duoc = (from tenduoc in _ldichvu.Where(o => o.Status == 1)
                    //                        join nhapduoc in duoc2 on tenduoc.MaDV equals nhapduoc.MaDV
                    //                        select new { tenduoc.SoLo, tenduoc.HanDung, tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, nhapduoc.MaKP, tenduoc.MaTam }
                    //                        ).OrderBy(p => p.TenDV).ToList().Distinct().ToList();
                    //            var duoc24012 = (from tenduoc in _ldichvu.Where(o => o.Status == 1)
                    //                             join nhapduoc in duoc2 on tenduoc.MaDV equals nhapduoc.MaDV
                    //                             select new { tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, nhapduoc.MaKP, tenduoc.MaTam, tenduoc.DonGia }
                    //                        ).OrderBy(p => p.TenDV).ToList().Distinct().ToList();
                    //            lupMaDuoc.DataSource = duoc;
                    //            if (DungChung.Bien.MaBV == "24012")
                    //            {
                    //                lupMaDuoc.DataSource = duoc24012;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            var duoc2 = (from nhapduoc in _dataContext.NhapDcts.Where(p => p.SoLuongN > p.SoLuongX)
                    //                         join nduoc in _dataContext.NhapDs.Where(p => p.MaKP == _makhotra) on nhapduoc.IDNhap equals nduoc.IDNhap
                    //                         select new { nhapduoc.MaDV, nduoc.MaKP }
                    //       ).ToList();
                    //            var duoc = (from tenduoc in _ldichvu.Where(o => o.Status == 1)
                    //                        join nhapduoc in duoc2 on tenduoc.MaDV equals nhapduoc.MaDV
                    //                        select new { tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, nhapduoc.MaKP, tenduoc.MaTam }
                    //                        ).OrderBy(p => p.TenDV).ToList().Distinct().ToList();
                    //            lupMaDuoc.DataSource = duoc;
                    //        }
                    //    }
                    //    else
                    //        lupMaDuoc.DataSource = _ldichvu.Where(p => p.Status == 1).OrderBy(p => p.TenDV).ToList();
                    //}
                    if (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập chuyển kho")
                    {

                        maKhoXuat = (int)lupBPTra.EditValue;
                        int idnhap = 0;
                        if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null)
                            idnhap = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));

                        medicinesByRoom = _medicinesProvider.GetLupMaDuoc(_makhotra, idnhap, 0);

                        lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;

                        ppxuat = _medicinesProvider.GetPPXuat(_makhotra);
                    }
                    else if (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập theo hóa đơn(nhà cung cấp)")
                    {
                        maKhoXuat = (int)lupMaKP.EditValue;
                        medicinesByRoom = _medicinesProvider.GetLupMaDuoc(_makho, -1, -1);
                        lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;

                        ppxuat = _medicinesProvider.GetPPXuat(_makho);
                    }
                    else
                    {
                        maKhoXuat = (int)lupMaKP.EditValue;
                        medicinesByRoom = _medicinesProvider.GetLupMaDuoc(_makho, -1, 0);
                        lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;

                        ppxuat = _medicinesProvider.GetPPXuat(_makho);
                    }
                }
                else
                {
                    lupMaDuoc.DataSource = lupIDThuoc.DataSource = allMedicines;
                }

                if (ppxuat == 1 || (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập theo hóa đơn(nhà cung cấp)"))
                {
                    colIDThuoc.Visible = false;
                    colMaDV.Visible = true;
                    colMaDV.VisibleIndex = 3;
                }
                else if (ppxuat == 3)
                {
                    colIDThuoc.Visible = true;
                    colIDThuoc.VisibleIndex = 3;
                    colMaDV.Visible = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("trong danh mục có thuốc có một số thuốc chưa có mã nhà cung cấp");
            }
        }

        private void grvNhap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _lDichvuTC.Clear();

            lupMaDuoc.DataSource = lupIDThuoc.DataSource = allMedicines;

            int id = 0;
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString() != "")
            {
                if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString() != "")
                {

                    id = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));
                }
                else
                {
                    txtIDNhap.Text = "";
                }
                var nhapD = _dataContext.NhapDs.FirstOrDefault(p => p.IDNhap == id);

                if (nhapD != null)
                    cboNhap.SelectedIndex = (int)nhapD.KieuDon;
                else
                    cboNhap.SelectedIndex = -1;

                if (nhapD != null)
                    lupBPTra.EditValue = nhapD.MaKPnx;
                else
                    lupBPTra.EditValue = 0;

                if (grvNhap.GetFocusedRowCellValue(colMaKP) != null)
                    lupMaKP.EditValue = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colMaKP));
                else
                    lupMaKP.EditValue = 0;

                if (nhapD != null)
                    lupMaCC.EditValue = nhapD.MaCC;
                else
                    lupMaCC.EditValue = "";

                if (grvNhap.GetFocusedRowCellValue(colNgayNhap) != null && grvNhap.GetFocusedRowCellValue(colNgayNhap).ToString() != "")
                    dtNgayNhap.DateTime = Convert.ToDateTime(grvNhap.GetFocusedRowCellValue(colNgayNhap));
                else
                    dtNgayNhap.DateTime = System.DateTime.Now;

                if (nhapD != null && nhapD.NgayTT != null)
                {
                    dtNgayTT.DateTime = Convert.ToDateTime(nhapD.NgayTT);
                    ckNgayTT.Checked = true;
                }
                else
                {
                    dtNgayTT.Text = "";
                    ckNgayTT.Checked = false;
                }
                if (grvNhap.GetFocusedRowCellValue(colsphieunew) != null && grvNhap.GetFocusedRowCellValue(colsphieunew) != "")
                    txtsophieunew.Text = grvNhap.GetFocusedRowCellValue(colsphieunew).ToString();
                else
                    txtsophieunew.Text = "";

                if (nhapD != null)
                    txtSoCT.Text = nhapD.SoCT;
                else
                    txtSoCT.Text = "";

                if (nhapD != null)
                    txtGhiChu.Text = nhapD.GhiChu;
                else
                    txtGhiChu.Text = "";
                if (nhapD != null)
                    cboNguoiGiao.Text = nhapD.TenNguoiCC;
                else cboNguoiGiao.Text = "";

                _listND = (from nd in _dataContext.NhapDcts.Where(p => p.IDNhap == id)
                           select new NhapDctss { IDNhapct = nd.IDNhapct, IDNhap = nd.IDNhap, MaDV = nd.MaDV, SoLo = nd.SoLo, SoDangKy = nd.SoDangKy, HanDung = nd.HanDung, DonVi = nd.DonVi, DonGiaCT = nd.DonGiaCT, DonGia = nd.DonGia, VAT = nd.VAT, ThanhTienTruocVAT = nd.DonGiaCT * nd.SoLuongN, ThanhTienN = nd.ThanhTienN, DonGiaDY = nd.DonGiaDY, SoLuongN = nd.SoLuongN, MaCC = nd.MaCC, SoLuongDY = nd.SoLuongDY, ThanhTienDY = nd.ThanhTienDY, TyLeCK = nd.TyLeCK }).ToList();

                _lNhapDct = _medicinesProvider.ViewInfoMedicineNhapD(id);

                if (grvNhap.GetFocusedRowCellValue(colTrangThai) != null && grvNhap.GetFocusedRowCellValue(colTrangThai).ToString() != "")
                    txtstatus.Text = grvNhap.GetFocusedRowCellValue(colTrangThai).ToString();
                else
                    txtstatus.Text = "";
                if (_lNhapDct.Count() > 0)
                    cboThueVAT.EditValue = _lNhapDct.First().VAT;
                else
                    cboThueVAT.EditValue = 0;
                if (_lNhapDct.Count() > 0)
                    cboTLchietKhau.EditValue = _lNhapDct.First().TyLeCK;
                else
                    cboTLchietKhau.EditValue = 0;
                if (nhapD != null)
                    txtDiaChi.Text = nhapD.DiaChi;
                else
                    txtDiaChi.Text = "";
                if (nhapD != null)
                    lupHinhthucx.EditValue = nhapD.TraDuoc_KieuDon;
                else
                    lupHinhthucx.EditValue = -1;


                if (id > 0)
                {
                    txtIDNhap.Text = id.ToString();
                }

                //binNhapDuocct.DataSource = _listND.ToList();
                binNhapDuocct.DataSource = _lNhapDct.ToList();
                grcNhapCT.DataSource = binNhapDuocct;

                _lDichvuTC = (from nd in _lNhapDct
                              join dv in _ldichvu on nd.MaDV equals dv.MaDV
                              select dv).ToList();
                _lDichvuTC.Add(new DVu { TenDV = " Tất cả", MaDV = 0 });
                lupTimKiemTenDV.Properties.DataSource = _lDichvuTC.OrderBy(p => p.TenDV).ToList();
            }
            else
            {
                _lDichvuTC.Clear();
                lupBPTra.EditValue = -1;
                lupTimKiemTenDV.Properties.DataSource = _lDichvuTC.OrderBy(p => p.TenDV).ToList();
                txtIDNhap.Text = "";
                cboNguoiGiao.Text = "";
                lupMaCC.EditValue = "";
                lupMaKP.EditValue = 0;
                txtGhiChu.Text = "";
                txtSoCT.Text = "";
                _lNhapDct = _medicinesProvider.ViewInfoMedicineNhapD(id);
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                groupControl3.Text = "Chi tiết chứng từ";
            }
        }

        private void grvNhapCT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //Tạo số thự tự
            if (e.Column == STT)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
        List<DVu> _lDichvuTC = new List<DVu>();
        private void cbtn_AnHienDS_CheckedChanged(object sender, EventArgs e)
        {
            if (cbtn_AnHienDS.Checked == true)
            {
                panelDSCT.Visible = true;
                cbtn_AnHienDS.Text = "Ẩn danh sách CT";
            }
            else
            {
                panelDSCT.Visible = false;
                cbtn_AnHienDS.Text = "Hiển danh sách CT";
            }
        }
        #region hàm tìm kiếm
        private void TimKiem()
        {
            //QLBV_Database.Common.EntityDbContext.RefreshDbContext();
            _dataContext = QLBV_Database.Common.EntityDbContext.DbContext;
            _dttu = DungChung.Ham.NgayTu(dtTimTuNgay.DateTime);
            _dtden = DungChung.Ham.NgayDen(dtTimDenNgay.DateTime);
            if (lupTimNCC.EditValue != null && lupTimNCC.EditValue.ToString() != "")
                _macc = lupTimNCC.EditValue.ToString();
            else
                _macc = "";
            if (lupTimMaKP.EditValue != null)
            {
                _makho = Convert.ToInt32(lupTimMaKP.EditValue);
                txtMaKP.Text = _makho.ToString();
            }
            else
                _makho = 0;
            if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Số phiếu|Số CT")
                _soPhieu = txtTimKiem.Text;
            int ID = 0, ot = 0;
            if (int.TryParse(txtTimKiem.Text, out ot))
                ID = Convert.ToInt32(txtTimKiem.Text);
            //_lNhapDuoc = (from nd in _dataContext.NhapDs.Where(p => p.PLoai == 1)
            //              where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
            //              where (nd.SoCT.Contains(_soPhieu) || (ID == 0 ? true : nd.IDNhap == ID))
            //              where (nd.MaKP == (_makho))
            //              where (_macc == "" ? true : nd.MaCC.Contains(_macc))
            //              select nd).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
            _lNhapDuoc = _medicinesProvider.SearchMedicine(_dttu, _dtden, _makho, 0, _macc, string.IsNullOrEmpty(_soPhieu) ? _soPhieu : "0", 0, "Nhập dược");
            grcNhap.DataSource = _lNhapDuoc.ToList();
        }
        #endregion
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void lupTimMaKP_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();

        }

        private void lookTimNCC_EditValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void dtTimTuNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }


        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Số phiếu|Số CT")
                txtTimKiem.Text = "";
        }
        public void _getValue(bool a)
        {
            _ktmatkhau = a;
        }
        public bool _ktmatkhau = false;
        bool _suaxuatnoibo = false;
        private void btnSua_Click(object sender, EventArgs e)
        {
            if(cboNhap.SelectedIndex != 0)
            {
                //k cho sửa đơn cũ
                colMaDV.OptionsColumn.ReadOnly = true;
                colSoLo.OptionsColumn.AllowEdit = false;
                colHanDung.OptionsColumn.AllowEdit = false;
                colDonGiaCT.OptionsColumn.AllowEdit = false;
                colDonGiaCT.OptionsColumn.AllowFocus = false;
                colDonGia.OptionsColumn.AllowFocus = false;
                colVAT.OptionsColumn.AllowFocus = false;
                colSoLuong.OptionsColumn.AllowEdit = false;

                //int idNhapD = Int32.Parse(txtIDNhap.Text);
                //var xoact = _medicinesProvider.GetNhapdctByIDNhap(idNhapD);
                //var xoac = _dataContext.NhapDs.Single(p => p.IDNhap == (idNhapD));

                //foreach (var xoa in xoact)
                //{
                //    var med = _dataContext.MedicineLists.FirstOrDefault(p => p.MaDV == xoa.MaDV && p.DonGia == xoa.DonGia && p.SoLo == xoa.SoLo && p.HanDung == xoa.HanDung && p.MaKP == xoac.MaKP);
                //    if (med == null || (med != null && xoa.SoLuongN != med.TonKhaDung))
                //    {
                //        MessageBox.Show("Chứng từ có thuốc không hợp lệ, bạn không thể sửa");
                //        return;
                //    }
                //}
            }


            if (DungChung.Bien.MaBV == "24012")
            {
                this.cboDonGiaCT.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            }
            if (cboNhap.SelectedIndex == 2)
            {
                grvNhapCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            }
            else
                grvNhapCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            bool _sua = true;
            _sua = DungChung.Ham.checkQuyen(this.Name)[1];
            if (_sua)
            {
                lupMaKP.Properties.DataSource = _medicinesProvider.GetListKhoaPhong("0", 1);

                if (DungChung.Bien.MaBV == "30009")
                {
                    colDonGiaCT.OptionsColumn.ReadOnly = false;
                    colVAT.OptionsColumn.ReadOnly = false;
                }
                _suaxuatnoibo = false;
                _ktmatkhau = false;
                bool _cothexoa = true;
                int id = 0;
                int idNhap = -1;

                if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null)
                    idNhap = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));

                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                {
                    kieuDon = cboNhap.SelectedIndex;
                    int idnhap = Convert.ToInt32(txtIDNhap.Text);
                    var kt = _dataContext.NhapDs.Where(p => p.IDNhap == idnhap).ToList();

                    if (!string.IsNullOrEmpty(txtstatus.Text))
                        id = Convert.ToInt32(txtstatus.Text);
                    //if (kt.Count > 0 && ((kt.First().XuatTD != null && kt.First().XuatTD > 0) || ((kt.First().SoCT != "0" || kt.First().SoCT != null || kt.First().SoCT != "") && kieuDon == 0)))
                    //{
                    //    if (kt.First().XuatTD != null && kt.First().XuatTD > 0)
                    //        MessageBox.Show("Chứng từ nhập theo chức năng, bạn không thể sửa");
                    //    else
                    //        MessageBox.Show("Chứng từ đã bị khóa, bạn không thể sửa");
                    //    _cothexoa = false;
                    //}                    
                    if (kt.Count > 0 && ((kt.First().SoCT != "0" && kt.First().SoCT != null && kt.First().SoCT != "") && kieuDon == 0))
                    {
                        MessageBox.Show("Chứng từ đã bị khóa, bạn không thể sửa");
                        _cothexoa = false;
                    }

                    if (_cothexoa && DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayNhap.DateTime, "KhoaDC"))
                    {
                        _cothexoa = false;
                    }

                    if (DungChung.Bien.MaBV == "30372")
                    {
                        var kt1 = (from a in kt
                                   join b in _dataContext.NhapDcts on a.IDNhap equals b.IDNhap
                                   select new
                                   {
                                       a.IDNhap,
                                       b.SoLo,
                                       b.HanDung,
                                       b.DonGia,
                                       b.DonGiaCT
                                   }).ToList();
                        if (kt1.Count > 0)
                        {
                            colDonGia.OptionsColumn.ReadOnly = true;
                            //colDonGiaCT.OptionsColumn.ReadOnly = true;
                            colSoLuong.OptionsColumn.ReadOnly = true;
                            colThanhTien.OptionsColumn.ReadOnly = true;
                        }
                        _cothexoa = true;
                    }



                    if (_cothexoa && !DungChung.Ham._KiemTraCBSuaXoa(_dataContext, kt.First().MaCB, DungChung.Bien.MaCB))
                    {
                        MessageBox.Show("Tên cán bộ không khớp!");
                        _cothexoa = false;
                    }
                    var ktdung = _dataContext.NhapDs.Where(p => p.XuatTD == idnhap).ToList();

                    if (_cothexoa && ktdung.Count > 0)
                    {
                        MessageBox.Show("Chứng từ đã được sử dụng, bạn không thể sửa!");

                        _cothexoa = false;

                    }
                    if (_cothexoa)
                    {
                        int XuatTD = 0;
                        if (!string.IsNullOrEmpty(txtXuatTD.Text))
                            XuatTD = Convert.ToInt32(txtXuatTD.Text);
                        var ktct = _dataContext.NhapDs.Where(p => p.IDNhap == XuatTD).ToList();
                        if (ktct.Count > 0)
                        {
                            DialogResult _result = MessageBox.Show("Chứng từ được kho khác xuất đến, bạn vẫn muốn sửa?", "Hỏi sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.Yes)
                            {
                                string _macbxuat = "";
                                if (ktct.First().MaCB != null)
                                    _macbxuat = ktct.First().MaCB;
                                ChucNang.frm_CheckPass frm = new ChucNang.frm_CheckPass(_macbxuat);
                                frm.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                                frm.ShowDialog();
                                if (_ktmatkhau)
                                {
                                    ChucNang.frm_CheckPass frm2 = new ChucNang.frm_CheckPass();
                                    frm2.ok = new ChucNang.frm_CheckPass._getdata(_getValue);
                                    frm2.ShowDialog();
                                }
                                if (_ktmatkhau == false)
                                    _cothexoa = false;
                                if (_ktmatkhau)
                                    _suaxuatnoibo = true;
                                if (_suaxuatnoibo)
                                {
                                    //colDonGiaCT.OptionsColumn.ReadOnly = true;
                                }
                            }
                            else
                            {
                                _cothexoa = false;
                            }
                        }
                    }
                    if (_cothexoa)
                    {
                        Enablebutton(false);
                        EnableControl(true);
                        txtSoCT.Focus();
                        if (DungChung.Bien.PLoaiKP != DungChung.Bien.st_PhanLoaiKP.Admin)
                            cboNhap.Properties.ReadOnly = true;
                        TTLuu = 2;
                        //lupMaDuoc.DataSource = lupIDThuoc.DataSource = _medicinesProvider.GetLupMaDuoc((int)kt.First().MaKPnx, idNhap, -1);
                        if (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập chuyển kho")
                        {
                            lupHinhthucx.Enabled = false;
                            txtSoCT.Enabled = false;

                            maKhoXuat = (int)lupBPTra.EditValue;
                            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null)
                                idnhap = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));

                            medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoXuat, idnhap, 0);

                            lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;

                            ppxuat = _medicinesProvider.GetPPXuat(maKhoXuat);
                        }
                        else if (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập theo hóa đơn(nhà cung cấp)")
                        {
                            txtSoCT.Enabled = true;

                            maKhoXuat = (int)lupMaKP.EditValue;
                            medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoXuat, -1, -1);
                            lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;

                            ppxuat = _medicinesProvider.GetPPXuat(maKhoXuat);
                        }
                        else
                        {
                            txtSoCT.Enabled = true;

                            maKhoXuat = (int)lupMaKP.EditValue;
                            medicinesByRoom = _medicinesProvider.GetLupMaDuoc(maKhoXuat, -1, 0);
                            lupMaDuoc.DataSource = lupIDThuoc.DataSource = medicinesByRoom;

                            ppxuat = _medicinesProvider.GetPPXuat(maKhoXuat);
                        }
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa có quyền sửa nhập dược!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        double _dgvat = 0;
        int dem = 0;
        List<DonGiaDV> ldongia = new List<DonGiaDV>();
        int ppxuat = -1;

        private List<DungChung.Ham.giaSoLoHSD> LayDSGiaHT(List<DungChung.Ham.giaSoLoHSD> tongSL, List<DungChung.Ham.giaSoLoHSD> dsgia, double soluongke, double gia)
        {
            List<DungChung.Ham.giaSoLoHSD> _Rlist = new List<DungChung.Ham.giaSoLoHSD>();
            if (gia == 0)// kê số lượng mới
            {
                foreach (var a in tongSL)
                {
                    foreach (var b in dsgia)
                    {
                        if (a.Gia == b.Gia && a.SoLo == b.SoLo && a.HanDung == b.HanDung)
                        {
                            if (a.SoLuong <= b.SoLuong)
                            {
                                b.SoLuong = b.SoLuong - a.SoLuong;
                                a.SoLuong = 0;

                                break;
                            }
                            else
                            {
                                a.SoLuong = a.SoLuong - b.SoLuong;
                                b.SoLuong = 0;
                            }
                        }
                    }
                }

                dsgia = dsgia.Where(p => p.SoLuong != 0).ToList();
                foreach (var a in dsgia)
                {
                    QLBV.DungChung.Ham.giaSoLoHSD moi = new QLBV.DungChung.Ham.giaSoLoHSD();
                    moi.Gia = a.Gia;
                    moi.HanDung = a.HanDung;
                    moi.SoLo = a.SoLo;
                    if (soluongke <= a.SoLuong)
                    {
                        if (soluongke == 0) // TH mới chọn dịch vụ
                            moi.SoLuong = a.SoLuong;
                        else
                            moi.SoLuong = soluongke;
                        _Rlist.Add(moi);
                        break;
                    }
                    else
                    {
                        soluongke = soluongke - a.SoLuong;
                        moi.SoLuong = a.SoLuong;
                        _Rlist.Add(moi);
                    }

                }
            }
            else// sửa số lượng
            {
                foreach (var a in tongSL)
                {
                    foreach (var b in dsgia)
                    {
                        if (a.Gia == b.Gia && a.SoLo == b.SoLo && a.HanDung == b.HanDung)
                        {
                            if (a.SoLuong <= b.SoLuong)
                            {
                                b.SoLuong = b.SoLuong - a.SoLuong;
                                a.SoLuong = 0;

                                break;
                            }
                            else
                            {
                                a.SoLuong = a.SoLuong - b.SoLuong;
                                b.SoLuong = 0;
                            }
                        }
                    }
                }

                dsgia = dsgia.Where(p => p.SoLuong != 0).ToList();
                foreach (var a in dsgia)
                {
                    if (a.Gia == gia)
                    {
                        QLBV.DungChung.Ham.giaSoLoHSD moi = new QLBV.DungChung.Ham.giaSoLoHSD();
                        moi.Gia = a.Gia;
                        moi.HanDung = a.HanDung;
                        moi.SoLo = a.SoLo;
                        if (soluongke <= a.SoLuong)
                        {
                            if (soluongke == 0) // TH mới chọn dịch vụ
                                moi.SoLuong = a.SoLuong;
                            else
                                moi.SoLuong = soluongke;
                            _Rlist.Add(moi);
                            a.SoLuong = a.SoLuong - soluongke;
                            soluongke = 0;//đã đủ thuốc kê đơn
                            break;
                        }
                        else
                        {
                            soluongke = soluongke - a.SoLuong;
                            moi.SoLuong = a.SoLuong;
                            a.SoLuong = 0;
                            _Rlist.Add(moi);
                        }
                    }
                }
                if (soluongke > 0)
                {
                    dsgia = dsgia.Where(p => p.SoLuong != 0).ToList();
                    foreach (var a in dsgia)
                    {

                        QLBV.DungChung.Ham.giaSoLoHSD moi = new QLBV.DungChung.Ham.giaSoLoHSD();
                        moi.Gia = a.Gia;
                        moi.HanDung = a.HanDung;
                        moi.SoLo = a.SoLo;
                        if (soluongke <= a.SoLuong)
                        {
                            if (soluongke == 0) // TH mới chọn dịch vụ
                                moi.SoLuong = a.SoLuong;
                            else
                                moi.SoLuong = soluongke;
                            _Rlist.Add(moi);
                            break;
                        }
                        else
                        {
                            soluongke = soluongke - a.SoLuong;
                            moi.SoLuong = a.SoLuong;
                            _Rlist.Add(moi);
                        }

                    }
                }

            }
            _Rlist = _Rlist.Where(p => p.SoLuong > 0).ToList();
            return RefreshList(_Rlist);

        }
        public List<DungChung.Ham.giaSoLoHSD> RefreshList(List<DungChung.Ham.giaSoLoHSD> _Rlist)
        {
            List<DungChung.Ham.giaSoLoHSD> _FreshList = new List<DungChung.Ham.giaSoLoHSD>();
            DungChung.Ham.giaSoLoHSD tem = new DungChung.Ham.giaSoLoHSD();

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
        bool isUpdate = true;
        List<MedicineInventoryModel> medicineRepositoryLookupEdit = new List<MedicineInventoryModel>();
        MedicineInventoryModel selectedMedicine = new MedicineInventoryModel();

        private void grvNhapCT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e != null && e.Column != null && e.Column.FieldName != null && sender is GridView gridView && gridView.ActiveEditor != null)
            {
                int madv = 0;
                bool _thuocyhdt = false;
                double _soluong = 0;
                double _tylePP = 0, _tyleBQ = 0; int pphhdy = 2;
                int makho = 0;
                if (lupMaKP.EditValue != null)
                    makho = Convert.ToInt32(lupMaKP.EditValue);
                int _makhotra = 0;
                if (lupBPTra.EditValue != null)
                    _makhotra = Convert.ToInt32(lupBPTra.EditValue);

                var abc = _lkphong.Where(p => p.MaKP == makho).FirstOrDefault();
                if (abc != null)
                {
                    pphhdy = abc.PPHHDY;
                    if (abc.PPXuat != null)
                        ppxuat = abc.PPXuat.Value;
                }
                if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                    madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));

                int makp = -1;
                if (DungChung.Bien.MaBV == "24012")
                {
                    if (cboNhap.SelectedIndex == 0 || cboNhap.SelectedIndex == 2)
                    {
                        makp = Convert.ToInt32(lupBPTra.EditValue);
                    }
                    else if (cboNhap.SelectedIndex == 3)
                    {
                        makp = Convert.ToInt32(lupMaKP.EditValue);
                    }
                }
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
                    case "colMaDV":
                        #region colMaDV
                        {
                            if (cboNhap.SelectedIndex == 0)
                            {
                                isUpdate = true;

                                newMaDV = Convert.ToInt32(newValue);

                                //lấy chi tiết đơn thuốc
                                if(lupMaDuoc.DataSource != null)
                                    medicineRepositoryLookupEdit = lupMaDuoc.DataSource as List<MedicineInventoryModel>;

                                selectedMedicine = medicineRepositoryLookupEdit.FirstOrDefault(p => p.MaDV == newMaDV);

                                //IList<NhapDctModel> nhapDcts = new List<NhapDctModel>();
                                //Dictionary<string, string> para = new Dictionary<string, string>()
                                //{
                                //    {"@iddon", "0" },
                                //    {"@maKP", maKhoXuat.ToString() },
                                //    {"@maDV", selectedMedicine.MaDV.ToString() }
                                //};

                                //nhapDcts = _excuteStoredProcedureProvider.ExcuteStoredProcedure<NhapDctModel>("sp_medicineInventory", para);

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

                                    //var nhapDct = nhapDcts.First();

                                    isDongY = _medicinesProvider.IsDongY((int)selectedMedicine.MaDV);
                                    if (cboThueVAT.EditValue != null && cboThueVAT.EditValue.ToString() != "")
                                        tyleVAT = Convert.ToDouble(cboThueVAT.EditValue);

                                    grvNhapCT.SetFocusedRowCellValue(colSoLo, selectedMedicine.SoLo);
                                    grvNhapCT.SetFocusedRowCellValue(colHanDung, selectedMedicine.HanDung);
                                    grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, selectedMedicine.DonGia);
                                    grvNhapCT.SetFocusedRowCellValue(colVAT, tyleVAT);
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                    grvNhapCT.SetFocusedRowCellValue(colDonVi, selectedMedicine.DonVi);

                                    isUpdate = true;
                                }
                            } 
                            else if(cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập theo hóa đơn(nhà cung cấp)")
                            {
                                isUpdate = true;

                                newMaDV = Convert.ToInt32(newValue);
                                //lấy chi tiết đơn thuốc
                                //IList<DThuocctModel> dthuoccts = new List<DThuocctModel>();
                                //Dictionary<string, string> para = new Dictionary<string, string>()
                                //{
                                //    {"@iddon", "0" },
                                //    {"@maKP", maKhoXuat.ToString() },
                                //    {"@maDV", newValue.ToString() }
                                //};

                                //dthuoccts = _excuteStoredProcedureProvider.ExcuteStoredProcedure<DThuocctModel>("sp_medicineInventory", para);
                                var dv = _dataContext.DichVus.FirstOrDefault(p => p.MaDV == newMaDV);


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

                                if (dv != null)
                                {
                                    isUpdate = false;

                                    isDongY = _medicinesProvider.IsDongY(newMaDV);
                                    if (cboThueVAT.EditValue != null && cboThueVAT.EditValue.ToString() != "")
                                        tyleVAT = Convert.ToDouble(cboThueVAT.EditValue);

                                    grvNhapCT.SetFocusedRowCellValue(colSoLo, "");
                                    grvNhapCT.SetFocusedRowCellValue(colHanDung, "");
                                    grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, dv.DonGia);
                                    grvNhapCT.SetFocusedRowCellValue(colVAT, tyleVAT);
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                    grvNhapCT.SetFocusedRowCellValue(colDonVi, dv.DonVi);

                                    isUpdate = true;
                                }
                            }
                            else
                            {
                                isUpdate = true;

                                newMaDV = Convert.ToInt32(newValue);

                                //lấy chi tiết đơn thuốc
                                if (lupMaDuoc.DataSource != null)
                                    medicineRepositoryLookupEdit = lupMaDuoc.DataSource as List<MedicineInventoryModel>;

                                selectedMedicine = medicineRepositoryLookupEdit.FirstOrDefault(p => p.MaDV == newMaDV);

                                //IList<DThuocctModel> dthuoccts = new List<DThuocctModel>();
                                //Dictionary<string, string> para = new Dictionary<string, string>()
                                //{
                                //    {"@iddon", "0" },
                                //    {"@maKP", maKhoXuat.ToString() },
                                //    {"@maDV", newValue.ToString() }
                                //};

                                //dthuoccts = _excuteStoredProcedureProvider.ExcuteStoredProcedure<DThuocctModel>("sp_medicineInventory", para);

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

                                    //var dthuocct = dthuoccts.First();

                                    isDongY = _medicinesProvider.IsDongY((int)selectedMedicine.MaDV);
                                    if (cboThueVAT.EditValue != null && cboThueVAT.EditValue.ToString() != "")
                                        tyleVAT = Convert.ToDouble(cboThueVAT.EditValue);

                                    grvNhapCT.SetFocusedRowCellValue(colSoLo, "");
                                    grvNhapCT.SetFocusedRowCellValue(colHanDung, "");
                                    grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, selectedMedicine.DonGia);
                                    grvNhapCT.SetFocusedRowCellValue(colVAT, tyleVAT);
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                    grvNhapCT.SetFocusedRowCellValue(colDonVi, selectedMedicine.DonVi);

                                    isUpdate = true;
                                }
                            }
                        }
                        break;
                    #endregion
                    case "colIDThuoc":
                        {
                            if (cboNhap.SelectedIndex == 0)
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

                                    //var dthuocct = dthuoccts.First();

                                    isDongY = _medicinesProvider.IsDongY((int)selectedMedicine.MaDV);

                                    if (cboThueVAT.EditValue != null && cboThueVAT.EditValue.ToString() != "")
                                        tyleVAT = Convert.ToDouble(cboThueVAT.EditValue);

                                    grvNhapCT.SetFocusedRowCellValue(colSoLo, selectedMedicine.SoLo);
                                    grvNhapCT.SetFocusedRowCellValue(colHanDung, selectedMedicine.HanDung);
                                    grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, selectedMedicine.DonGia);
                                    if (isDongY)
                                        grvNhapCT.SetFocusedRowCellValue(colGiaNhapDY, selectedMedicine.DonGia);
                                    grvNhapCT.SetFocusedRowCellValue(colVAT, tyleVAT);
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                    grvNhapCT.SetFocusedRowCellValue(colDonVi, selectedMedicine.DonVi);

                                    isUpdate = true;
                                }
                            }
                            else
                            {
                                isUpdate = true;

                                newIdThuoc = Convert.ToInt32(newValue);

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
                                    if (cboThueVAT.EditValue != null && cboThueVAT.EditValue.ToString() != "")
                                        tyleVAT = Convert.ToDouble(cboThueVAT.EditValue);

                                    if (cboNhap.SelectedItem.ToString() == "Nhập tồn" || cboNhap.SelectedItem.ToString() == "Nhập sản xuất")
                                    {
                                        grvNhapCT.SetFocusedRowCellValue(colSoLo, selectedMedicine.SoLo);
                                        grvNhapCT.SetFocusedRowCellValue(colHanDung, selectedMedicine.HanDung);
                                    }
                                    else
                                    {
                                        grvNhapCT.SetFocusedRowCellValue(colSoLo, "");
                                        grvNhapCT.SetFocusedRowCellValue(colHanDung, "");
                                    }
                                    grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, selectedMedicine.DonGia);
                                    if(isDongY)
                                        grvNhapCT.SetFocusedRowCellValue(colGiaNhapDY, selectedMedicine.DonGia);
                                    grvNhapCT.SetFocusedRowCellValue(colVAT, tyleVAT);
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
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
                                    oldSL = Convert.ToDouble(gridView.ActiveEditor.OldEditValue);
                                    newSL = Convert.ToDouble(gridView.ActiveEditor.EditValue);
                                    newDonGia = Convert.ToDouble(donGiaObj);

                                    var medicineInventory = medicinesByRoom.FirstOrDefault(p => p.MaDV == newMaDV && p.DonGia == newDonGia);
                                    if (isUpdate && medicineInventory != null)
                                    {
                                        if (cboNhap.SelectedIndex == 0)
                                        {
                                            if (newSL < 0)
                                            {
                                                isUpdate = false;

                                                grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);

                                                if (isDongY)
                                                {
                                                    grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuongDY, oldSL);
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
                                                    grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuongDY, oldSL);
                                                }

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
                                                    grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuongDY, newSL);
                                                    grvNhapCT.SetRowCellValue(e.RowHandle, colThanhTienDY, thanhTien);
                                                }

                                                grvNhapCT.SetRowCellValue(e.RowHandle, colThanhTien, thanhTien);
                                            }
                                        }
                                        else
                                        {
                                            double thanhTien = Math.Round((double)(newSL * newDonGia), DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);

                                            if (isDongY)
                                            {
                                                grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuongDY, newSL);
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
                                        if (cboNhap.SelectedIndex == 0)
                                        {
                                            if (newSL <= 0)
                                            {
                                                isUpdate = false;

                                                grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuong, oldSL);
                                                if (isDongY)
                                                {
                                                    grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuongDY, oldSL);
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
                                                    grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuongDY, oldSL);
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
                                                    grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuongDY, newSL);
                                                    grvNhapCT.SetRowCellValue(e.RowHandle, colThanhTienDY, thanhTien);
                                                }

                                                _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, oldSL, newSL, newIdThuoc, newIdThuoc, newDonGia, newDonGia, ppxuat);
                                                this.grvNhapCT.ViewCaption = "Số lượng tồn: " + medicineInventory.TonHienTai;
                                            }
                                        }
                                        else
                                        {
                                            double thanhTien = Math.Round((double)(newSL * newDonGia), DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);

                                            if (isDongY)
                                            {
                                                grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuongDY, newSL);
                                                grvNhapCT.SetRowCellValue(e.RowHandle, colThanhTienDY, thanhTien);
                                            }

                                            grvNhapCT.SetRowCellValue(e.RowHandle, colThanhTien, thanhTien);
                                        }
                                    }
                                }
                                #endregion theo so lo han dung
                            }
                        }
                        break;
                    case "colDonGiaCT":
                        {
                            donGia = Convert.ToDouble(e.Value);

                            if (cboThueVAT.EditValue != null && cboThueVAT.EditValue.ToString() != "")
                                tyleVAT = Convert.ToDouble(cboThueVAT.EditValue);

                            donGiaVAT = donGia + (donGia * tyleVAT / 100);


                            grvNhapCT.SetFocusedRowCellValue(colDonGia, donGiaVAT);
                            if (isDongY)
                            {
                                grvNhapCT.SetFocusedRowCellValue(colGiaNhapDY, donGiaVAT);
                            }
                        }
                        break;
                }
                SetTextStock();
            }
        }

        private void grvNhapCT_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (!string.IsNullOrEmpty(cboThueVAT.Text))
            {
                int vat = int.Parse(cboThueVAT.Text);
                grvNhapCT.SetFocusedRowCellValue(colVAT, vat);
            }
            if (lupMaCC.EditValue != null)
                grvNhapCT.SetFocusedRowCellValue(colMaCCct, lupMaCC.EditValue);
            if (!string.IsNullOrEmpty(cboTLchietKhau.Text))
            {
                int tlck = int.Parse(cboTLchietKhau.Text);
                grvNhapCT.SetFocusedRowCellValue(colTyLeCK, tlck);
            }

        }

        private void dtTimDenNgay_Leave(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void grvNhap_DataSourceChanged(object sender, EventArgs e)
        {
            grvNhap_FocusedRowChanged(null, null);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool _xoa_b = DungChung.Ham.checkQuyen(this.Name)[2];
            if (_xoa_b)
            {
                int idNhapD = Int32.Parse(txtIDNhap.Text);
                var xoact = _dataContext.NhapDcts.Where(p => p.IDNhap == idNhapD).ToList();
                var xoac = _dataContext.NhapDs.FirstOrDefault(p => p.IDNhap == idNhapD);

                if (cboNhap.SelectedIndex != 0)
                {
                    foreach (var xoa in xoact)
                    {
                        var med = _dataContext.MedicineLists.FirstOrDefault(p => p.MaDV == xoa.MaDV && p.DonGia == xoa.DonGia && p.SoLo == xoa.SoLo && p.HanDung == xoa.HanDung && p.MaKP == xoac.MaKP);
                        if (med == null || (med != null && xoa.SoLuongN > med.TonKhaDung))
                        {
                            MessageBox.Show("Chứng từ có thuốc không hợp lệ, bạn không thể sửa");
                            return;
                        }
                    }
                }

                int status = 0;
                if (!string.IsNullOrEmpty(txtstatus.Text))
                    status = Convert.ToInt32(txtstatus.Text);
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                {
                    bool _cothexoa = true;
                    //if (status == 1)
                    //{
                    //    MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa");
                    //    _cothexoa = false;
                    //    return;
                    //}
                    kieuDon = cboNhap.SelectedIndex;
                    int id = Int32.Parse(txtIDNhap.Text);
                    var kt = _dataContext.NhapDs.Where(p => p.IDNhap == id).ToList();
                    if (kt.Count > 0 && (kt.First().SoCT == "0" || kt.First().SoCT == "" || kt.First().SoCT == null && kieuDon == 0) && DungChung.Ham._checkNgayKhoa(_dataContext, dtNgayNhap.DateTime, "KhoaDC") == false || kieuDon != 0)
                    {
                        if (_cothexoa)
                        {
                            if (!DungChung.Ham._KiemTraCBSuaXoa(_dataContext, kt.First().MaCB, DungChung.Bien.MaCB))
                            {
                                MessageBox.Show("Tên cán bộ không khớp!");
                                _cothexoa = false;
                            }
                        }
                        //var xoact = _dataContext.NhapDcts.Where(p => p.IDNhap == id).ToList();
                        if (_cothexoa)
                        {
                            int _mkho = 0;
                            if (lupBPTra.EditValue != null)
                                _mkho = Convert.ToInt32(lupBPTra.EditValue);
                            if (DungChung.Bien.MaBV == "24012")
                            {
                                if (cboNhap.SelectedIndex == 1 || cboNhap.SelectedIndex == 4 || cboNhap.SelectedIndex == 3)
                                    _mkho = Convert.ToInt32(lupMaKP.EditValue);
                            }

                            if (_cothexoa == false)
                            {
                                MessageBox.Show("Một số thuốc đã được sử dụng, bạn không thể xóa!");
                            }
                        }
                        int idnhap = Convert.ToInt32(txtIDNhap.Text);
                        if (_cothexoa)
                        {
                            var ktdung = _dataContext.NhapDs.Where(p => p.XuatTD == idnhap).ToList();
                            if (ktdung.Count > 0)
                            {
                                MessageBox.Show("Chứng từ đã được sử dụng, bạn không thể xóa");
                                _cothexoa = false;
                            }
                        }
                        if (_cothexoa)
                        {
                            DialogResult _result;
                            _result = MessageBox.Show("Bạn muốn xóa chứng từ số: " + txtSoCT.Text, "xóa chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            int sopl = -1;
                            int kieudon = -1;
                            int makp = -11;
                            if (_result == DialogResult.Yes)
                            {
                                sopl = xoac.SoPL ?? -1;
                                kieudon = xoac.KieuDon ?? -1;
                                makp = (int)xoac.MaKP;

                                foreach (var xoa in xoact)
                                {
                                    _medicinesProvider.DeleteNhapDAndNhapDctbyIDNhapct(xoa.IDNhapct);

                                    if (cboNhap.SelectedIndex != 0)
                                    {
                                        _medicinesProvider.UpdateMedicineListPPX3((int)xoa.MaDV, xoa.DonGia, xoa.SoLo, (DateTime)xoa.HanDung, (int)xoac.MaKP, (int)xoac.MaKPnx, -xoa.SoLuongN, 4);
                                    }
                                }
                                _dataContext.SaveChanges();
                                if (kieudon == 2)
                                {
                                    var ldt = (from dt in _dataContext.DThuocs
                                               join dtct in _dataContext.DThuoccts.Where(p => p.SoPL == sopl)
                                                on dt.IDDon equals dtct.IDDon
                                               select dt).ToList();
                                    foreach (var item2 in ldt)
                                    {
                                        var dtct = _dataContext.DThuoccts.Where(p => p.IDDon == item2.IDDon).ToList();
                                        foreach (var a in dtct)
                                        {
                                            if (a.Status != -1)
                                                a.Status = 0;
                                        }
                                        _dataContext.SaveChanges();
                                    }
                                }
                                TimKiem();
                            }
                        }

                    }

                    else
                    {
                        if (kt.First().XuatTD != null && kt.First().XuatTD > 0)
                            MessageBox.Show("Chứng từ nhận theo chức năng, bạn không thể xóa!");
                        else
                            MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa!");
                    }

                }
                else
                {
                    MessageBox.Show("Không có chứng từ để xóa!");
                }
            }
        }

        private void grvNhapCT_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXoaCT" && (TTLuu == 1 || TTLuu == 2))
            {
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                {
                    if (TTLuu == 2 && cboNhap.SelectedIndex != 0)
                    {
                        if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null && Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct)) > 0)
                        {
                            int idNhapct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));
                            int maKX = lupMaKP.EditValue != null ? Convert.ToInt32(lupMaKP.EditValue) : 0;

                            var xoac = _dataContext.NhapDcts.FirstOrDefault(p => p.IDNhapct == (idNhapct));
                            var med = _dataContext.MedicineLists.FirstOrDefault(p => p.MaDV == xoac.MaDV && p.DonGia == xoac.DonGia && p.SoLo == xoac.SoLo && p.HanDung == xoac.HanDung && p.MaKP == maKX);
                            if (med == null || (med != null && xoac.SoLuongN > med.TonKhaDung))
                            {
                                MessageBox.Show("Thuốc đã xuất, bạn không thể xóa");
                                return;
                            }
                        }
                    }

                    int id = Int32.Parse(txtIDNhap.Text);
                    var kt = _dataContext.NhapDs.Where(p => p.IDNhap == id).ToList();
                    if (kt.Count > 0 && kt.First().Status != 1)
                    {
                        if (kt.First().XuatTD != null && kt.First().XuatTD > 0 && kt.First().XuatTD != 2)
                        {
                            MessageBox.Show("Chứng từ được nhập bằng chức năng, bạn không thể xóa CT");
                        }
                        else
                        {
                            if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null && grvNhapCT.GetFocusedRowCellValue(colIDNhapct).ToString() != "")
                            {
                                if ((grvNhapCT.GetFocusedRowCellDisplayText(colMaDV) != null && ppxuat == 1) || (grvNhapCT.GetFocusedRowCellDisplayText(colIDThuoc) != null && ppxuat == 3))
                                {
                                    string tenthuoc = grvNhapCT.GetFocusedRowCellDisplayText(colMaDV).ToString();
                                    int _madv = grvNhapCT.GetFocusedRowCellValue(colMaDV) != null ? Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV)) : 0;
                                    int idthuoc = grvNhapCT.GetFocusedRowCellValue(colIDThuoc) != null ? Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDThuoc)) : 0;
                                    double dongia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                    double soluong = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                                    string _macc = "";
                                    int _mkho = 0;
                                    bool ktra = true;
                                    if (lupBPTra.EditValue != null)
                                        _mkho = Convert.ToInt32(lupBPTra.EditValue);
                                    if (grvNhapCT.GetFocusedRowCellValue(colMaCCct) != null)
                                        _macc = grvNhapCT.GetFocusedRowCellValue(colMaCCct).ToString();
                                    if (MessageBox.Show("Bạn muốn xóa thuốc: " + tenthuoc, "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        int idct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));
                                        if (idct == 0)
                                        {
                                            if (ppxuat == 1 || (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập theo hóa đơn(nhà cung cấp)"))
                                            {
                                                _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, soluong, 0, _madv, 0, dongia, 0, ppxuat);
                                            }
                                            else if (ppxuat == 3)
                                            {
                                                _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, soluong, 0, idthuoc, 0, dongia, 0, ppxuat);
                                            }

                                            grvNhapCT.DeleteSelectedRows();

                                            return;
                                        }

                                        if(ppxuat == 1 || (cboNhap.SelectedItem != null && cboNhap.SelectedItem.ToString() == "Nhập theo hóa đơn(nhà cung cấp)"))
                                        {
                                            _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, soluong, 0, _madv, 0, donGia, 0, ppxuat);
                                        }
                                        else if (ppxuat == 3)
                                        {
                                            _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, soluong, 0, idthuoc, 0, donGia, 0, ppxuat);
                                        }

                                        deleteNhapdcts.Add(idct);
                                        grvNhapCT.DeleteSelectedRows();
                                       // _lNhapDct = _medicinesProvider.ViewInfoMedicine(id, 1);

                                        //binNhapDuocct.DataSource = _lNhapDct;
                                        //grcNhapCT.DataSource = binNhapDuocct;
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
                    if (grvNhapCT.RowCount > 0)
                    {
                        int _madv = grvNhapCT.GetFocusedRowCellValue(colMaDV) != null ? Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV)) : 0;
                        int idthuoc = grvNhapCT.GetFocusedRowCellValue(colIDThuoc) != null ? Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDThuoc)) : 0;
                        double dongia = grvNhapCT.GetFocusedRowCellValue(colIDThuoc) != null ? Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia)) : 0;
                        double soluong = grvNhapCT.GetFocusedRowCellValue(colIDThuoc) != null ? Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong)) : 0;

                        if (ppxuat == 1)
                        {
                            _medicinesProvider.EditStockByIDThuoc(lupMaDuoc, soluong, 0, _madv, 0, dongia, 0, ppxuat);
                        }
                        else if (ppxuat == 3)
                        {
                            _medicinesProvider.EditStockByIDThuoc(lupIDThuoc, soluong, 0, idthuoc, 0, dongia, 0, ppxuat);
                        }

                        grvNhapCT.DeleteSelectedRows();
                    }
                    else
                        MessageBox.Show("Không có chứng từ để xóa!");
                }
            }
        }
        class PNK14018
        {
            public string TenDuoc { get; set; }
            public string MaSo { get; set; }
            public string DonViTinh { get; set; }
            public string HamLuong { get; set; }
            public string ThucNhap { get; set; }
            public string DonGia { get; set; }
            public string ThanhTien { get; set; }
        }

        private bool _inPhieuNhap(QLBV_Database.QLBVEntities data, int id, int _loaigia)//0 phiếu nhập theo giá sau VAT,1 phiếu nhập theo giá trước VAT, 2- nhập theo giá VAT (thực tế) không tính phần chiết khấu
        {
            try
            {
                var par = (from nd in data.NhapDs
                           where (nd.IDNhap == id)
                           join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                           join ncc in data.NhaCCs on nd.MaCC equals ncc.MaCC into k
                           from k1 in k.DefaultIfEmpty()
                           select new { nd.KieuDon, nd.DiaChi, DiaChi3 = kp.DChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, MaCC = k1 != null ? k1.MaCC : "", TenCC = k1 != null ? k1.TenCC : "", nd.PLoai, nd.SoPL, nd.MaKP }).ToList();
                string _mauso = "";

                var q2 = (from nd in data.NhapDs
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          join dv in data.DichVus on ndct.MaDV equals dv.MaDV
                          where (nd.IDNhap == id)
                          select new
                          {
                              nd,
                              ndct,
                              dv
                          }).ToList();
                var q = (from nd in q2
                         group nd by new { nd.dv.HamLuong, nd.dv.MaTam, nd.dv.TyLeSD, nd.ndct.VAT, nd.ndct.IDNhapct, nd.dv.TenDV, nd.dv.MaQD, nd.ndct.SoLo, nd.ndct.HanDung, nd.dv.DonViN, nd.ndct.DonGia, nd.ndct.DonGiaCT, nd.ndct.SoLuongN, nd.ndct.ThanhTienN, nd.dv.MaDV } into kq
                         select new
                         {
                             madv = kq.Key.MaDV,
                             kq.Key.MaTam,
                             kq.Key.MaQD,
                             kq.Key.IDNhapct,
                             kq.Key.HamLuong,

                             TenDuoc = (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "12122") ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : kq.Key.TenDV,
                             SoLo = kq.Key.SoLo,
                             HanDung = kq.Key.HanDung,
                             DonViTinh = kq.Key.DonViN,
                             SoLuongN = kq.Sum(p => p.ndct.SoLuongN) / kq.Key.TyLeSD,
                             DonGia = _loaigia == 1 ? (kq.Key.DonGiaCT * kq.Key.TyLeSD) : (_loaigia == 0 ? (kq.Key.DonGia * kq.Key.TyLeSD) : (kq.Key.DonGiaCT * (kq.Key.VAT + 100) / 100)),
                             DonGiaCT = kq.Key.DonGiaCT * kq.Key.TyLeSD,
                             ThanhTienN = _loaigia == 1 ? kq.Sum(p => p.ndct.SoLuongN) * kq.Key.DonGiaCT : (_loaigia == 0 ? kq.Sum(p => p.ndct.ThanhTienN) : (kq.Sum(p => p.ndct.SoLuongN) * kq.Key.DonGiaCT * (kq.Key.VAT + 100) / 100))
                         }).OrderBy(p => p.IDNhapct).ToList();
                double tongtien = Math.Round(Convert.ToDouble(q.Sum(p => p.ThanhTienN)), 3);

                #region xuat Excel

                string[] _arr = new string[] { "0", "@", "@", "@", "@", "0", "0", "0", "0" };
                string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Số lô", "Hạn sử dụng", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá", "Thành tiền" };

                int[] _arrWidth = new int[] { };

                DungChung.Bien.MangHaiChieu = new Object[q.Count + 19, 9];
                DungChung.Bien.MangHaiChieu[0, 0] = "Đơn vị: " + DungChung.Bien.TenCQ.ToUpper(); ;
                DungChung.Bien.MangHaiChieu[1, 0] = "Địa chỉ: " + DungChung.Bien.DiaChi;
                DungChung.Bien.MangHaiChieu[2, 0] = "Mã đơn vị có quan hệ với ngân sách: " + DungChung.Bien.MaNSach;
                DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C20-HD";
                DungChung.Bien.MangHaiChieu[1, 5] = _mauso;
                DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu nhập kho").ToUpper();
                DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                DungChung.Bien.MangHaiChieu[5, 4] = "Số: " + par.First().SoCT;
                DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
                DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
                DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người giao: " + par.First().TenNguoiCC;
                DungChung.Bien.MangHaiChieu[7, 0] = "Lý do nhập: " + par.First().GhiChu; ;
                DungChung.Bien.MangHaiChieu[8, 0] = "Nhập tại kho: " + par.First().TenKP;
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
                }
                int num = 10;
                DungChung.Bien.MangHaiChieu[num + q.Count, 7] = "Cộng:";
                DungChung.Bien.MangHaiChieu[num + q.Count(), 8] = q.Sum(p => p.ThanhTienN);
                DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien, " đồng!"); ;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 5] = "Nhập, ngày ..... tháng ..... năm ...... ";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập biểu";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người giao hàng";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                if (DungChung.Bien.MaBV == "02005")
                {
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Thủ trưởng đơn vị";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.GiamDoc;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Kế toán trưởng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = DungChung.Bien.KeToanTruong;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Người lập biểu";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Người giao hàng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = "";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.ThuKho;
                }
                if (DungChung.Bien.MaBV == "30003")
                {
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập biểu";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người giao hàng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = "";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = DungChung.Bien.KeToanTruong;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ trưởng đơn vị";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.GiamDoc;
                }
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                {
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Nhà cung cấp";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = par.First().TenCC;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                }
                foreach (var r in q)
                {

                    DungChung.Bien.MangHaiChieu[num, 0] = num - 9;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenDuoc;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.SoLo;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.HanDung;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.DonViTinh;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongN;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.SoLuongN;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.ThanhTienN;

                    num++;

                }


                #endregion
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu nhập kho", "C:\\PhieuNhapKho.xls", true, this.Name);
                #region
                if (DungChung.Bien.MaBV == "26007")
                {
                    BaoCao.repPhieuNhapKho_26007 rep = new BaoCao.repPhieuNhapKho_26007();
                    if (par.Count > 0)
                    {
                        if (par.First().KieuDon == 2)
                        {
                            rep.labTieuDe.Text = "PHIẾU NHẬP HÀNG TRẢ LẠI";
                            rep.lab_NguoiGiaoTra.Text = "Họ tên người trả hàng";
                        }
                        _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của Bộ trưởng BTC)";
                        if (DungChung.Bien.MaBV == "08204")
                        {
                            _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC) ngày 30/3/2006 và Thông tư số 185/2010/TT-BTC ngày 15/11/2010 của Bộ tài chính";
                        }
                        if (DungChung.Bien.MaBV == "26007")
                        {
                            _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC) ngày 30/3/2006 và sửa đổi, bổ sung Thông tư số 185/2010/TT-BTC ngày 15/11/2010 của Bộ tài chính";
                        }
                        rep.MauSo.Value = _mauso;
                        rep.SoCT.Value = "Số: " + par.First().SoCT;
                        rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        if (par.First().TenNguoiCC != null)
                            rep.TenNG.Value = par.First().TenNguoiCC;

                        rep.LyDoNhap.Value = par.First().GhiChu;
                        rep.KhoNhap.Value = par.First().TenKP;
                        string macc = "";
                        if (par.First().MaCC != null)
                            macc = par.First().MaCC;
                        if (par.First().DiaChi != null)
                            rep.DiaChiNG.Value = par.First().DiaChi;
                    }

                    rep.TongTien.Value = tongtien;
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                #region
                else if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    BaoCao.repPhieuNhapKho_01071 rep = new BaoCao.repPhieuNhapKho_01071();
                    if (par.Count > 0)
                    {
                        if (par.First().KieuDon == 2)
                        {
                            rep.labTieuDe.Text = "PHIẾU NHẬP HÀNG TRẢ LẠI";
                            rep.lab_NguoiGiaoTra.Text = "Họ tên người trả hàng";
                        }

                        _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của Bộ trưởng BTC)";
                        if (DungChung.Bien.MaBV == "08204")
                        {
                            _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC) ngày 30/3/2006 và Thông tư số 185/2010/TT-BTC ngày 15/11/2010 của Bộ tài chính";
                        }
                        if (DungChung.Bien.MaBV == "26007")
                        {
                            _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC) ngày 30/3/2006 và sửa đổi, bổ sung Thông tư số 185/2010/TT-BTC ngày 15/11/2010 của Bộ tài chính";
                        }
                        rep.MauSo.Value = _mauso;
                        rep.SoCT.Value = "Số: " + par.First().SoCT;

                        rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        if (par.First().TenNguoiCC != null)
                        {
                            rep.TenNG.Value = par.First().TenNguoiCC;
                            rep.xrTenNguoiGiao.Text = par.First().TenNguoiCC;
                        }
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                        {
                            if (par.First().TenCC != null)
                                rep.xrTenNguoiGiao.Text = par.First().TenCC;
                            rep.lab_NguoiGiaoTra.Text = "Nhà cung cấp";

                        }
                        rep.LyDoNhap.Value = par.First().GhiChu;
                        rep.KhoNhap.Value = par.First().TenKP;
                        string macc = "";
                        if (par.First().MaCC != null)
                            macc = par.First().MaCC;
                        if (DungChung.Bien.MaBV == "20001")
                        {
                            if (par.First().DiaChi3 != null)
                            {
                                rep.DiaChiNG.Value = par.First().DiaChi3;
                            }
                        }
                        else
                        {
                            if (par.First().DiaChi != null)
                                rep.DiaChiNG.Value = par.First().DiaChi;
                        }

                    }
                    rep.TongTien.Value = tongtien.ToString();
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                #region
                else if (DungChung.Bien.MaBV == "04007" || DungChung.Bien.MaBV == "24012")
                {
                    QLBV.Phieu.Rep_PhieuNhapDuoc_04007 rep = new Phieu.Rep_PhieuNhapDuoc_04007();
                    rep.Formatdate = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().FormatDate.Value;
                    rep.NguoiLap.Value = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().NguoiLapBieu.ToString();
                    rep.ThuKho.Value = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().ThuKho.ToString();
                    rep.MauSo.Value = "Mẫu số: C30-HD";
                    rep.TenPhieu.Value = "PHIẾU NHẬP KHO";
                    rep.colNgay1.Text = par.First().NgayNhap.Value.Day + " / " + par.First().NgayNhap.Value.Month + " / " + par.First().NgayNhap.Value.Year;
                    rep.colcccq.Text = _dataContext.HTHONGs.Where(p => p.MaBV.Contains(DungChung.Bien.MaBV)).First().TenCQCQ.ToUpper();
                    rep.colcccq1.Text = "Đơn vị: " + DungChung.Bien.TenCQ.ToUpper();
                    rep.colTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
                    if (par.First().TenNguoiCC != null)
                    {
                        rep.HoTen.Value = par.First().TenNguoiCC;
                        rep.NguoiNhanNguoiGiao.Value = par.First().TenNguoiCC;
                    }
                    rep.KeToanTruong.Value = _dataContext.HTHONGs.First().KeToanTruong;
                    string MaSo = "Số: " + par.First().SoCT;
                    rep.ColKhoan.Text = q.Count.ToString();
                    rep.LyDoNhap.Value = par.First().GhiChu;
                    rep.TheoHoaDonSo.Value = par.First().SoCT; ;
                    rep.So.Value = MaSo;
                    rep.coltongtien.Text = tongtien.ToString("#,##");
                    rep.coltongtien1.Text = tongtien.ToString("#,##");
                    rep.ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    if (dtNgayTT.Text != null && dtNgayTT.Text != "")
                    {
                        DateTime dt = dtNgayTT.DateTime;
                        rep.ngaycua.Value = "Ngày " + dt.Day + " tháng " + dt.Month + " năm " + dt.Year + " của " + lupMaCC.Text;
                    }
                    else
                    {
                        rep.ngaycua.Value = " của " + lupMaCC.Text;
                    }

                    rep.NhapTaiKho.Value = par.First().TenKP;
                    rep.ĐiaChi.Value = par.First().DiaChi3;
                    rep.ColTienBangChu.Text = DungChung.Ham.DocTienBangChu(tongtien, " đồng.");
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                #region
                else if (DungChung.Bien.MaBV == "14018")
                {
                    if (par.First().PLoai == 1 && par.First().KieuDon == 2)
                    {
                        string[] _ds = new string[2] { "", "" };
                        _ds[0] = (par.First().SoPL ?? 0).ToString();
                        _ds[1] = (par.First().MaKP ?? 0).ToString();

                        FormNhap.frm_Check_moi frmCheckMoi = new FormNhap.frm_Check_moi();
                        frmCheckMoi.InPhieu(_ds, 2);
                    }
                    else
                    {
                        Dictionary<string, object> dicpar = new Dictionary<string, object>();
                        List<PNK14018> listPNK = new List<PNK14018>();

                        foreach (var item in q.ToList())
                        {
                            PNK14018 themmoi = new PNK14018();
                            themmoi.TenDuoc = item.TenDuoc;
                            themmoi.MaSo = item.MaTam;
                            themmoi.DonViTinh = item.DonViTinh;
                            themmoi.HamLuong = item.HamLuong;
                            themmoi.ThucNhap = item.SoLuongN.ToString("#,##");
                            themmoi.DonGia = item.DonGia.ToString("#,##");
                            themmoi.ThanhTien = item.ThanhTienN.ToString("#,##");
                            listPNK.Add(themmoi);

                        }
                        dicpar["NguoiLap"] = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().NguoiLapBieu.ToString();
                        dicpar["ThuKho"] = _dataContext.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().ThuKho.ToString();
                        dicpar["TenBV"] = "Đơn vị: " + DungChung.Bien.TenCQ;
                        dicpar["NgayNhap"] = DungChung.Ham.NgaySangChu(par.First().NgayNhap.Value);
                        dicpar["DiaChi"] = "Địa chỉ: " + _dataContext.HTHONGs.First().DiaChi;
                        if (par.First().TenNguoiCC != null)
                        {
                            dicpar["NguoiGiao"] = par.First().TenNguoiCC;
                        }
                        dicpar["KeToanTruong"] = _dataContext.HTHONGs.First().KeToanTruong;
                        dicpar["SoChungTu"] = "Số: " + par.First().SoCT;
                        dicpar["LyDoNhap"] = par.First().GhiChu;
                        dicpar["Khoan"] = q.Count.ToString();
                        dicpar["NhapTaiKho"] = par.First().TenKP;

                        dicpar["TongTien"] = tongtien.ToString("#,##");

                        dicpar["TongTienChu"] = DungChung.Ham.DocTienBangChu(tongtien, " đồng.");
                        DungChung.Ham.Print(DungChung.PrintConfig.Rep_PhieuNhapKho_14018, listPNK.ToList(), dicpar, false);
                    }
                }
                #endregion
                #region
                else
                {
                    BaoCao.repPhieuNhapKho rep = new BaoCao.repPhieuNhapKho();
                    if (par.Count > 0)
                    {
                        if (par.First().KieuDon == 2)
                        {
                            rep.labTieuDe.Text = "PHIẾU NHẬP HÀNG TRẢ LẠI";
                            rep.lab_NguoiGiaoTra.Text = "Họ tên người trả hàng";
                        }

                        _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của Bộ trưởng BTC)";
                        if (DungChung.Bien.MaBV == "08204")
                        {
                            _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC) ngày 30/3/2006 và Thông tư số 185/2010/TT-BTC ngày 15/11/2010 của Bộ tài chính";
                        }
                        if (DungChung.Bien.MaBV == "26007")
                        {
                            _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC) ngày 30/3/2006 và sửa đổi, bổ sung Thông tư số 185/2010/TT-BTC ngày 15/11/2010 của Bộ tài chính";
                        }
                        if (DungChung.Bien.MaBV == "20001")
                        {
                            _mauso = "(Ban hành kèm theo Thông tư 107/2017/TT-BTC 24/11/2017)";
                        }
                        rep.MauSo.Value = _mauso;
                        rep.SoCT.Value = "Số: " + par.First().SoCT;
                        rep.xrLabel10.Text = "Số chứng từ kèm theo: " + par.First().SoCT;
                        rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        if (par.First().TenNguoiCC != null)
                        {
                            rep.TenNG.Value = par.First().TenNguoiCC;
                            rep.xrTenNguoiGiao.Text = par.First().TenNguoiCC;
                        }
                        if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
                        {
                            if (par.First().TenCC != null)
                                rep.xrTenNguoiGiao.Text = par.First().TenCC;
                            rep.lab_NguoiGiaoTra.Text = "Nhà cung cấp";

                        }
                        rep.LyDoNhap.Value = par.First().GhiChu;
                        rep.KhoNhap.Value = par.First().TenKP;
                        string macc = "";
                        if (par.First().MaCC != null)
                            macc = par.First().MaCC;
                        if (DungChung.Bien.MaBV == "20001")
                        {
                            if (par.First().DiaChi3 != null)
                                rep.DiaChiNG.Value = par.First().DiaChi3;
                        }
                        else
                        {
                            if (par.First().DiaChi != null)
                                rep.DiaChiNG.Value = par.First().DiaChi;
                        }

                    }
                    rep.TongTien.Value = tongtien.ToString();
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// In phiếu, tính VAT sau khi cộng tổng thành tiền -- dùng cho BV 27022 và 27023
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        /// <param name="makho">dùng cho BV YHCT Bac Ninh - 27021: nếu kho tổng thì in footer: Khoa dược, Kế toán, Người nhận. Ngược lại thì để trống</param>
        /// <returns></returns>
        /// 
        string _khoaduoc = "";
        string _ketoan = "";
        string _nguoinhan = "";
        string _thutruong = "";
        private void dsKT(string khoaduoc, string ketoan, string nguoinhan, string thutruong)
        {
            _khoaduoc = khoaduoc;
            _ketoan = ketoan;
            _nguoinhan = nguoinhan;
            _thutruong = thutruong;
        }

        private bool _inPhieuNhapVAT(QLBV_Database.QLBVEntities data, int id)
        {
            try
            {
                var macq = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList().FirstOrDefault();
                int VAT = 0;
                string tencc = "", dchi = "";
                var httn = _dataContext.NhapDs.Where(p => p.IDNhap == id).Select(p => p.KieuDon).FirstOrDefault();
                if (DungChung.Bien.MaBV == "20001" && (httn == 1 || httn == 4))
                {
                    QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001 frmts = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001();
                    frmts.passCB = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001.PassCB(PassData);
                    frmts.ShowDialog();

                    BaoCao.repPhieuNhapKho_20001 rep = new BaoCao.repPhieuNhapKho_20001();

                    var par = (from nd in _dataContext.NhapDs
                               where (nd.IDNhap == id)
                               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                               select new { nd.KieuDon, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, nd.DiaChi, nd.MaCC }).ToList();
                    if (par.Count > 0)
                    {
                        rep.So.Value = par.First().SoCT;
                        rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        rep.TenNG.Value = par.First().TenNguoiCC;
                        rep.TenKho.Value = par.First().TenKP;
                        rep.col_nguoilap.Text = _tencbIn;
                        if (par.First().MaCC != null)
                        {
                            string macc = par.First().MaCC;
                            var nhacc = _dataContext.NhaCCs.Where(p => p.MaCC == macc).ToList();
                            if (nhacc.Count() > 0)
                            {
                                tencc = nhacc.First().TenCC;
                                dchi = nhacc.First().DiaChi;
                                rep.LyDoNhap.Value = tencc;
                                rep.DiaChiNG.Value = dchi;
                            }
                        }
                        rep.KhoNhap.Value = par.First().TenKP;
                        rep.col_cbcungung.Text = par.First().TenNguoiCC;
                    }
                    var q1 = (from nd in _dataContext.NhapDs
                              join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                              join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                              where (nd.IDNhap == id)
                              select new
                              {
                                  nd,
                                  ndct,
                                  dv
                              }).ToList();
                    var q2 = (from nd in q1
                              group nd by new { nd.dv.HamLuong, nd.dv.MaTam, nd.dv.TyLeSD, nd.dv.DongY, nd.ndct.IDNhapct, nd.dv.TenDV, nd.ndct.SoLo, nd.ndct.HanDung, nd.dv.DonViN, nd.ndct.DonGia, nd.ndct.DonGiaCT, nd.ndct.SoLuongN, nd.ndct.ThanhTienN, nd.ndct.DonGiaDY, nd.ndct.ThanhTienDY, nd.dv.MaQD, nd.dv.NhaSX, nd.dv.NuocSX, nd.ndct.VAT } into kq
                              select new
                              {
                                  kq.Key.MaTam,
                                  kq.Key.IDNhapct,
                                  TenDuoc = kq.Key.TenDV,
                                  SoLo = kq.Key.SoLo,
                                  HanDung = kq.Key.HanDung,
                                  MaQD = kq.Key.MaQD,
                                  HangSX = kq.Key.NhaSX == null ? kq.Key.NuocSX : (kq.Key.NhaSX + "-" + kq.Key.NuocSX),
                                  DonViTinh = kq.Key.DonViN,
                                  SoLuongN = kq.Sum(p => p.ndct.SoLuongN),
                                  DonGia = kq.Key.DongY == 1 ? kq.Key.DonGiaDY : kq.Key.DonGia,
                                  ThanhTienN = kq.Key.DongY == 1 ? kq.Sum(p => p.ndct.ThanhTienDY) : kq.Sum(p => p.ndct.ThanhTienN)
                              }).OrderBy(p => p.IDNhapct).ToList();
                    var q = (from a in q2
                             select new
                             {
                                 a.MaTam,
                                 a.IDNhapct,
                                 a.MaQD,
                                 a.HangSX,
                                 TenDuoc = a.TenDuoc,
                                 SoLo = a.SoLo,
                                 HanDung = a.HanDung,
                                 DonViTinh = a.DonViTinh,
                                 SoLuongN = a.SoLuongN,
                                 DonGia = a.DonGia,
                                 ThanhTienN = a.ThanhTienN
                             }).ToList();
                    VAT = 0;
                    var qVAT = q1.FirstOrDefault();
                    if (qVAT != null)
                    {
                        VAT = qVAT.ndct.VAT;
                    }
                    double tongtien = q.Sum(p => p.ThanhTienN);
                    rep.lblTienBangChu.Text = DungChung.Ham.NumberToTextVN((decimal)tongtien);
                    #region xuat Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "0", "0", "0", "0" };
                    string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Mã thuốc", "Hãng, nước sản xuất", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá (VAT)", "Thành tiền (VAT)" };

                    int[] _arrWidth = new int[] { };

                    DungChung.Bien.MangHaiChieu = new Object[q.Count + 19, 9];
                    DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQCQ.ToUpper(); ;
                    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                    DungChung.Bien.MangHaiChieu[0, 5] = "Mã phiếu: BILL048791";
                    DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu nhập kho").ToUpper();
                    DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người giao: " + par.First().TenNguoiCC == null ? "" : par.First().TenNguoiCC;
                    DungChung.Bien.MangHaiChieu[7, 0] = "Theo hóa đơn số" + par.First().SoCT == null ? "" : par.First().SoCT;
                    DungChung.Bien.MangHaiChieu[7, 3] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    DungChung.Bien.MangHaiChieu[8, 0] = "Của: " + tencc;
                    DungChung.Bien.MangHaiChieu[9, 0] = "Địa chỉ: " + dchi;
                    DungChung.Bien.MangHaiChieu[10, 0] = "Nhập tại kho: " + par.First().TenKP;
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[11, i] = _tieude[i];
                    }
                    int num = 10;
                    DungChung.Bien.MangHaiChieu[num + q.Count, 0] = "Tổng thành tiền thanh toán:";
                    DungChung.Bien.MangHaiChieu[num + q.Count(), 8] = q.Sum(p => p.ThanhTienN);
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Thành tiền" + q.Sum(p => p.ThanhTienN); ;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 5] = "Bằng chữ:" + DungChung.Ham.NumberToTextVN((decimal)tongtien);
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "PGĐ. Phụ trách";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Kế toán trường";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = DungChung.Bien.KeToanTruong;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "CB cung ứng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = par.First().TenNguoiCC == null ? "" : par.First().TenNguoiCC;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thống kê dược";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";

                    foreach (var r in q)
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num - 9;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDuoc;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.MaQD;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.HangSX;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.DonViTinh;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongN;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.SoLuongN;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.ThanhTienN;

                        num++;

                    }


                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu nhập kho", "C:\\PhieuNhapKho.xls", true, this.Name);
                    rep.TongTien.Value = tongtien;
                    rep.col_ketoan.Text = DungChung.Bien.KeToanTruong;
                    rep.col_thukho.Text = DungChung.Bien.ThuKho;
                    rep.Thang.Value = DungChung.Ham.NgaySangChu(DateTime.Now, DungChung.Bien.FormatDate) + "  ";
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();

                }
                #region 12001 // namnt
                else if (DungChung.Bien.MaBV == "12001" || (macq.MaChuQuan != null && macq.MaChuQuan.Trim() == "12001"))
                {
                    var q1 = (from nd in _dataContext.NhapDs
                              join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                              join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                              where (nd.IDNhap == id)
                              select new
                              {
                                  nd,
                                  ndct,
                                  dv
                              }).ToList();
                    var q2 = (from nd in q1
                              group nd by new { nd.dv.HamLuong, nd.dv.MaTam, nd.dv.TyLeSD, nd.dv.DongY, nd.ndct.IDNhapct, nd.dv.TenDV, nd.ndct.SoLo, nd.ndct.HanDung, nd.dv.DonViN, nd.ndct.DonGia, nd.ndct.DonGiaCT, nd.ndct.SoLuongN, nd.ndct.ThanhTienN, nd.ndct.DonGiaDY, nd.ndct.ThanhTienDY, nd.dv.MaQD, nd.dv.NhaSX, nd.dv.NuocSX, nd.ndct.VAT } into kq
                              select new
                              {
                                  kq.Key.MaTam,
                                  kq.Key.IDNhapct,
                                  TenDuoc = kq.Key.TenDV,
                                  SoLo = kq.Key.SoLo,
                                  HanDung = kq.Key.HanDung,
                                  MaQD = kq.Key.MaQD,
                                  HangSX = kq.Key.NhaSX == null ? kq.Key.NuocSX : (kq.Key.NhaSX + "-" + kq.Key.NuocSX),
                                  DonViTinh = kq.Key.DonViN,
                                  SoLuongN = kq.Sum(p => p.ndct.SoLuongN),
                                  DonGia = kq.Key.DonGia,
                                  ThanhTienN = kq.Sum(p => p.ndct.ThanhTienN)
                              }).OrderBy(p => p.IDNhapct).ToList();
                    List<usNhapDuoc.PhieuNhap_12001> q = new List<usNhapDuoc.PhieuNhap_12001>();
                    q = (from a in q2
                         select new usNhapDuoc.PhieuNhap_12001
                         {
                             MaTam = a.MaTam,
                             IDNhapct = a.IDNhapct,
                             MaQD = a.MaQD,
                             HangSX = a.HangSX,
                             TenDuoc = a.TenDuoc,
                             SoLo = a.SoLo,
                             HanDung = a.HanDung,
                             DonViTinh = a.DonViTinh,
                             SoLuongN = a.SoLuongN,
                             DonGia = a.DonGia,
                             ThanhTienN = a.ThanhTienN
                         }).ToList();
                    BaoCao.repPhieuNhapKho_12001 rep = new BaoCao.repPhieuNhapKho_12001(q);

                    var par = (from nd in _dataContext.NhapDs
                               where (nd.IDNhap == id)
                               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                               select new { nd.KieuDon, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.DiaChi, nd.TenNguoiCC, nd.MaCC }).ToList();
                    if (par.Count > 0)
                    {
                        rep.SoCT.Value = "Số: " + par.First().SoCT;
                        rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        rep.TenNG.Value = par.First().TenNguoiCC;
                        rep.TenKho.Value = par.First().TenKP;
                        rep.DiaChiCQ.Value = par.First().DiaChi;
                        rep.Thang.Value = par.First().NgayNhap.Value.Hour + ":" + par.First().NgayNhap.Value.Minute + ",ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        if (par.First().MaCC != null)
                        {
                            string macc = par.First().MaCC;
                            var nhacc = _dataContext.NhaCCs.Where(p => p.MaCC == macc).ToList();
                            if (nhacc.Count() > 0)
                            {
                                tencc = nhacc.First().TenCC;
                                rep.LyDoNhap.Value = tencc;
                            }
                        }
                        rep.KhoNhap.Value = par.First().TenKP;
                    }
                    VAT = 0;
                    var qVAT = q1.FirstOrDefault();
                    if (qVAT != null)
                    {
                        VAT = qVAT.ndct.VAT;
                    }
                    double tongtien = q.Sum(p => p.ThanhTienN);
                    rep.lblTienBangChu.Text = DungChung.Ham.DocTienBangChu(tongtien, " đồng!");
                    #region xuat Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "0", "0", "0", "0" };
                    string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Mã thuốc", "Hãng, nước sản xuất", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá (VAT)", "Thành tiền (VAT)" };

                    int[] _arrWidth = new int[] { };

                    DungChung.Bien.MangHaiChieu = new Object[q.Count + 19, 9];
                    DungChung.Bien.MangHaiChieu[0, 0] = DungChung.Bien.TenCQCQ.ToUpper(); ;
                    DungChung.Bien.MangHaiChieu[1, 0] = DungChung.Bien.TenCQ.ToUpper();
                    DungChung.Bien.MangHaiChieu[0, 5] = "Mã phiếu: BILL048791";
                    DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu nhập kho").ToUpper();
                    DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người giao: " + par.First().TenNguoiCC == null ? "" : par.First().TenNguoiCC;
                    DungChung.Bien.MangHaiChieu[7, 0] = "Theo hóa đơn số" + par.First().SoCT == null ? "" : par.First().SoCT;
                    DungChung.Bien.MangHaiChieu[7, 3] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    DungChung.Bien.MangHaiChieu[8, 0] = "Của: " + tencc;
                    DungChung.Bien.MangHaiChieu[9, 0] = "Địa chỉ: " + dchi;
                    DungChung.Bien.MangHaiChieu[10, 0] = "Nhập tại kho: " + par.First().TenKP;
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[11, i] = _tieude[i];
                    }
                    int num = 10;
                    DungChung.Bien.MangHaiChieu[num + q.Count, 0] = "Tổng thành tiền thanh toán:";
                    DungChung.Bien.MangHaiChieu[num + q.Count(), 8] = q.Sum(p => p.ThanhTienN);
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Thành tiền" + q.Sum(p => p.ThanhTienN); ;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 5] = "Bằng chữ:" + DungChung.Ham.DocTienBangChu(tongtien, " đồng!");
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "PGĐ. Phụ trách";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Kế toán trường";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = DungChung.Bien.KeToanTruong;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "CB cung ứng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = par.First().TenNguoiCC == null ? "" : par.First().TenNguoiCC;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thống kê dược";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";

                    foreach (var r in q)
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num - 9;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDuoc;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.MaQD;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.HangSX;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.DonViTinh;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongN;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.SoLuongN;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.ThanhTienN;

                        num++;

                    }


                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu nhập kho", "C:\\PhieuNhapKho.xls", true, this.Name);
                    rep.TongTien.Value = tongtien;
                    rep.col_ketoan.Text = DungChung.Bien.KeToanTruong;
                    rep.col_PGd.Text = DungChung.Bien.TruongKhoaDuoc;
                    rep.col_thukho.Text = DungChung.Bien.ThuKho;
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();


                }
                #endregion
                else
                #region bv khác
                {

                    BaoCao.repPhieuNhapKho_27023 rep = new BaoCao.repPhieuNhapKho_27023();
                    var par = (from nd in _dataContext.NhapDs
                               where (nd.IDNhap == id)
                               join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                               select new { nd.KieuDon, nd.DiaChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, nd.MaCC, nd.SoPhieu }).ToList();
                    string _mauso = "";
                    if (par.Count > 0)
                    {
                        if (par.First().KieuDon == 2)
                        {
                            rep.labTieuDe.Text = "PHIẾU NHẬP HÀNG TRẢ LẠI";
                            rep.lab_NguoiGiaoTra.Text = "Họ tên người trả hàng";
                        }
                        if (DungChung.Bien.MaBV == "27022")
                        {
                            _mauso = "(Ban hành kèm theo thông tư 107/2017/TT-BTC ngày 24/11/2017)";
                        }
                        else
                        {
                            _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC Ngày 30/03/2006 của Bộ trưởng BTC)";
                        }
                        if (DungChung.Bien.MaBV == "08204")
                        {
                            _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC) ngày 30/3/2006 và Thông tư số 185/2010/TT-BTC ngày 15/11/2010 của Bộ tài chính";
                        }
                        if (DungChung.Bien.MaBV == "26007")
                        {
                            _mauso = "(Ban hành theo QĐ số 19/2006/QĐ-BTC) ngày 30/3/2006 và sửa đổi, bổ sung Thông tư số 185/2010/TT-BTC ngày 15/11/2010 của Bộ tài chính";
                        }

                        rep.KhoaDuoc.Value = _khoaduoc;
                        rep.KeToan.Value = _ketoan;
                        rep.NguoiNhan.Value = _nguoinhan;
                        rep.ThuTruong.Value = _thutruong;
                        rep.MauSo.Value = _mauso;
                        rep.SoCT.Value = "Số: " + par.First().SoCT;
                        rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                        rep.TenNG.Value = par.First().TenNguoiCC;

                        rep.LyDoNhap.Value = par.First().GhiChu;
                        rep.KhoNhap.Value = par.First().TenKP;
                        string macc = "";
                        if (par.First().MaCC != null)
                            macc = par.First().MaCC;
                        if (par.First().DiaChi != null)
                            rep.DiaChiNG.Value = par.First().DiaChi;
                    }
                    var q1 = (from nd in _dataContext.NhapDs
                              join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                              join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                              where (nd.IDNhap == id)
                              select new
                              {
                                  nd,
                                  ndct,
                                  dv
                              }).ToList();
                    var q2 = (from nd in q1
                              group nd by new { nd.dv.HamLuong, nd.dv.MaTam, nd.dv.TyLeSD, nd.ndct.IDNhapct, nd.dv.TenDV, nd.ndct.SoLo, nd.ndct.HanDung, nd.dv.DonViN, nd.ndct.DonGia, nd.ndct.DonGiaCT, nd.ndct.SoLuongN, nd.ndct.ThanhTienN } into kq
                              select new
                              {
                                  kq.Key.MaTam,
                                  kq.Key.IDNhapct,
                                  TenDuoc = DungChung.Bien.MaBV == "26007" ? (kq.Key.TenDV + " " + kq.Key.HamLuong) : kq.Key.TenDV,
                                  SoLo = kq.Key.SoLo,
                                  HanDung = kq.Key.HanDung,
                                  DonViTinh = kq.Key.DonViN,
                                  SoLuongN = kq.Sum(p => p.ndct.SoLuongN) / kq.Key.TyLeSD,
                                  DonGia = kq.Key.DonGia * kq.Key.TyLeSD,
                                  DonGiaCT = kq.Key.DonGiaCT * kq.Key.TyLeSD
                              }).OrderBy(p => p.IDNhapct).ToList();
                    var q = (from a in q2
                             select new
                             {
                                 a.MaTam,
                                 a.IDNhapct,
                                 TenDuoc = a.TenDuoc,
                                 SoLo = a.SoLo,
                                 HanDung = a.HanDung,
                                 DonViTinh = a.DonViTinh,
                                 SoLuongN = a.SoLuongN,
                                 DonGia = a.DonGiaCT == 0 ? a.DonGia : a.DonGiaCT,
                                 ThanhTienN = a.SoLuongN * (a.DonGiaCT == 0 ? a.DonGia : a.DonGiaCT)
                             }).ToList();
                    VAT = 0;
                    var qVAT = q1.FirstOrDefault();
                    if (qVAT != null)
                    {
                        VAT = qVAT.ndct.VAT;
                    }
                    double tongThanhTien = q.Sum(p => p.ThanhTienN);
                    double tongThanhTienSauVAT = tongThanhTien + tongThanhTien * ((double)VAT / 100);
                    double tongtien = Math.Round(tongThanhTienSauVAT, 0);
                    rep.colThanhTienCong.Text = tongtien.ToString("##,###");
                    rep.lblTienBangChu.Text = DungChung.Ham.DocTienBangChu(tongtien, " đồng!");
                    rep.celVAT.Text = VAT.ToString();
                    #region xuat Excel

                    string[] _arr = new string[] { "0", "@", "@", "@", "@", "0", "0", "0", "0" };
                    string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Số lô", "Hạn sử dụng", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá", "Thành tiền" };

                    int[] _arrWidth = new int[] { };

                    DungChung.Bien.MangHaiChieu = new Object[q.Count + 19, 9];
                    DungChung.Bien.MangHaiChieu[0, 0] = "Đơn vị: " + DungChung.Bien.TenCQ.ToUpper(); ;
                    DungChung.Bien.MangHaiChieu[1, 0] = "Địa chỉ: " + DungChung.Bien.DiaChi;
                    DungChung.Bien.MangHaiChieu[2, 0] = "Mã đơn vị có quan hệ với ngân sách: " + DungChung.Bien.MaNSach;
                    DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C20-HD";
                    DungChung.Bien.MangHaiChieu[1, 5] = _mauso;
                    DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu nhập kho").ToUpper();
                    DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    DungChung.Bien.MangHaiChieu[5, 4] = "Số: " + par.First().SoCT;
                    DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
                    DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
                    DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người giao: " + par.First().TenNguoiCC;
                    DungChung.Bien.MangHaiChieu[7, 0] = "Lý do nhập: " + par.First().GhiChu; ;
                    DungChung.Bien.MangHaiChieu[8, 0] = "Nhập tại kho: " + par.First().TenKP;
                    for (int i = 0; i < _tieude.Length; i++)
                    {
                        DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
                    }
                    int num = 10;
                    DungChung.Bien.MangHaiChieu[num + q.Count, 7] = "Cộng:";
                    DungChung.Bien.MangHaiChieu[num + q.Count(), 8] = q.Sum(p => p.ThanhTienN);
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien, " đồng!"); ;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 5] = "Nhập, ngày ..... tháng ..... năm ...... ";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập biểu";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người giao hàng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                    if (DungChung.Bien.MaBV == "02005")
                    {
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Thủ trưởng đơn vị";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.GiamDoc;
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Kế toán trưởng";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = DungChung.Bien.KeToanTruong;
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Người lập biểu";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.NguoiLapBieu;
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Người giao hàng";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = "";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ kho";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.ThuKho;
                    }
                    if (DungChung.Bien.MaBV == "30003")
                    {
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập biểu";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người giao hàng";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = "";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = DungChung.Bien.KeToanTruong;
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ trưởng đơn vị";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                        DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.GiamDoc;
                    }
                    foreach (var r in q)
                    {

                        DungChung.Bien.MangHaiChieu[num, 0] = num - 9;
                        DungChung.Bien.MangHaiChieu[num, 1] = r.TenDuoc;
                        DungChung.Bien.MangHaiChieu[num, 2] = r.SoLo;
                        DungChung.Bien.MangHaiChieu[num, 3] = r.HanDung;
                        DungChung.Bien.MangHaiChieu[num, 4] = r.DonViTinh;
                        DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongN;
                        DungChung.Bien.MangHaiChieu[num, 6] = r.SoLuongN;
                        DungChung.Bien.MangHaiChieu[num, 7] = r.DonGia;
                        DungChung.Bien.MangHaiChieu[num, 8] = r.ThanhTienN;

                        num++;

                    }


                    #endregion
                    frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu nhập kho", "C:\\PhieuNhapKho.xls", true, this.Name);
                    rep.TongTien.Value = tongtien;
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                #endregion
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region in phiếu nhập 20001
        string _macbIn = "";
        string _tencbIn = "";
        private void PassData(string maCB, string tenCB)
        {
            _macbIn = maCB;
            _tencbIn = tenCB;

        }
        #endregion
        public class PhieuNhap_12001
        {
            public string MaTam { get; set; }
            public int IDNhapct { get; set; }
            public string MaQD { get; set; }
            public string HangSX { get; set; }
            public string TenDuoc { get; set; }
            public string SoLo { get; set; }
            public DateTime? HanDung { get; set; }
            public string DonViTinh { get; set; }
            public double SoLuongN { get; set; }
            public double DonGia { get; set; }
            public double ThanhTienN { get; set; }
        }
        private bool _inPhieuNhap_24009(QLBV_Database.QLBVEntities data, int id)
        {
            try
            {
                BaoCao.repPhieuNhapKho24009 rep = new BaoCao.repPhieuNhapKho24009();

                var par = (from nd in _dataContext.NhapDs
                           where (nd.IDNhap == id)
                           join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                           select new { nd.DiaChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, nd.MaCC }).ToList();
                string _mauso = "";
                if (par.Count > 0)
                {
                    rep.MauSo.Value = _mauso;
                    rep.SoCT.Value = "Số: " + par.First().SoCT;
                    rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    rep.TenNG.Value = par.First().TenNguoiCC;

                    rep.LyDoNhap.Value = par.First().GhiChu;
                    rep.KhoNhap.Value = par.First().TenKP;
                    string macc = "";
                    if (par.First().MaCC != null)
                        macc = par.First().MaCC;
                    rep.DiaChiNG.Value = par.First().DiaChi;
                }

                var q = (from nd in _dataContext.NhapDs
                         join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                         where (nd.IDNhap == id)
                         group new { dv, nd, ndct } by new { dv.MaTam, dv.TyLeSD, ndct.IDNhapct, dv.TenDV, dv.TenRG, ndct.SoLo, ndct.HanDung, dv.DonViN, ndct.DonGia, ndct.SoLuongN, ndct.ThanhTienN } into kq
                         select new
                         {
                             kq.Key.MaTam,
                             kq.Key.IDNhapct,
                             TenDuoc = kq.Key.TenRG,
                             SoLo = kq.Key.SoLo,
                             HanDung = kq.Key.HanDung,
                             DonViTinh = kq.Key.DonViN,
                             SoLuongN = kq.Sum(p => p.ndct.SoLuongN) / kq.Key.TyLeSD,
                             DonGia = kq.Key.DonGia * kq.Key.TyLeSD,
                             ThanhTienN = kq.Sum(p => p.ndct.ThanhTienN)
                         }).OrderBy(p => p.IDNhapct).ToList();
                double tongtien = Math.Round(Convert.ToDouble(q.Sum(p => p.ThanhTienN)), 3);
                rep.colThanhTienCong.Text = tongtien.ToString("##,###.###");

                #region xuat Excel

                string[] _arr = new string[] { "0", "@", "@", "@", "@", "0", "0", "0", "0" };
                string[] _tieude = { "STT", "Tên, nhãn hiệu, quy cách, phẩm chất vật tư (SP, hàng hóa)", "Số lô", "Hạn sử dụng", "Đơn vị tính", "SL theo chứng từ", "SL thực nhập", "Đơn giá", "Thành tiền" };

                int[] _arrWidth = new int[] { };

                DungChung.Bien.MangHaiChieu = new Object[q.Count + 19, 9];
                DungChung.Bien.MangHaiChieu[0, 0] = "Đơn vị: " + DungChung.Bien.TenCQ.ToUpper(); ;
                DungChung.Bien.MangHaiChieu[1, 0] = "Địa chỉ: " + DungChung.Bien.DiaChi;
                DungChung.Bien.MangHaiChieu[2, 0] = "Mã đơn vị có quan hệ với ngân sách: " + DungChung.Bien.MaNSach;
                DungChung.Bien.MangHaiChieu[0, 5] = "Mẫu số C20-HD";
                DungChung.Bien.MangHaiChieu[1, 5] = _mauso;
                DungChung.Bien.MangHaiChieu[3, 4] = ("Phiếu nhập kho").ToUpper();
                DungChung.Bien.MangHaiChieu[4, 4] = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                DungChung.Bien.MangHaiChieu[5, 4] = rep.SoCT.Value = "Số: " + par.First().SoCT;
                DungChung.Bien.MangHaiChieu[4, 6] = "Nợ .....";
                DungChung.Bien.MangHaiChieu[5, 6] = "Có .....";
                DungChung.Bien.MangHaiChieu[6, 0] = "Họ và tên người giao: " + par.First().TenNguoiCC;
                DungChung.Bien.MangHaiChieu[7, 0] = "Lý do nhập: " + par.First().GhiChu; ;
                DungChung.Bien.MangHaiChieu[8, 0] = "Nhập tại kho: " + par.First().TenKP;
                for (int i = 0; i < _tieude.Length; i++)
                {
                    DungChung.Bien.MangHaiChieu[9, i] = _tieude[i];
                }
                int num = 10;
                DungChung.Bien.MangHaiChieu[num + q.Count, 7] = "Cộng:";
                DungChung.Bien.MangHaiChieu[num + q.Count(), 8] = q.Sum(p => p.ThanhTienN);
                DungChung.Bien.MangHaiChieu[num + q.Count() + 1, 0] = "Tổng số tiền (Viết bằng chữ): " + DungChung.Ham.DocTienBangChu(tongtien, " đồng!"); ;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 2, 5] = "Nhập, ngày ..... tháng ..... năm ...... ";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập biểu";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người giao hàng";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = DungChung.Bien.KeToanTruong;
                if (DungChung.Bien.MaBV == "02005")
                {
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Thủ trưởng đơn vị";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.GiamDoc;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Kế toán trưởng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = DungChung.Bien.KeToanTruong;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Người lập biểu";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Người giao hàng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 6] = "";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.ThuKho;
                }
                if (DungChung.Bien.MaBV == "30003")
                {
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 0] = "Người lập biểu";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 0] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 0] = DungChung.Bien.NguoiLapBieu;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 2] = "Người giao hàng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 2] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = "";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 4] = "Thủ kho";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 4] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 4] = DungChung.Bien.ThuKho;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 6] = "Kế toán trưởng";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 6] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 2] = DungChung.Bien.KeToanTruong;
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 3, 8] = "Thủ trưởng đơn vị";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 4, 8] = "(Ký, họ tên)";
                    DungChung.Bien.MangHaiChieu[num + q.Count() + 7, 8] = DungChung.Bien.GiamDoc;
                }
                foreach (var r in q)
                {

                    DungChung.Bien.MangHaiChieu[num, 0] = num - 9;
                    DungChung.Bien.MangHaiChieu[num, 1] = r.TenDuoc;
                    DungChung.Bien.MangHaiChieu[num, 2] = r.SoLo;
                    DungChung.Bien.MangHaiChieu[num, 3] = r.HanDung;
                    DungChung.Bien.MangHaiChieu[num, 4] = r.DonViTinh;
                    DungChung.Bien.MangHaiChieu[num, 5] = r.SoLuongN;
                    DungChung.Bien.MangHaiChieu[num, 6] = r.SoLuongN;
                    DungChung.Bien.MangHaiChieu[num, 7] = r.DonGia;
                    DungChung.Bien.MangHaiChieu[num, 8] = r.ThanhTienN;

                    num++;

                }


                #endregion
                frmIn frm = new frmIn(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Phiếu nhập kho", "C:\\PhieuNhapKho.xls", true, this.Name);
                rep.TongTien.Value = tongtien;
                rep.DataSource = q;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            var macq = _dataContext.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList().FirstOrDefault();
            frmIn frm = new frmIn();
            if (DungChung.Bien.LoaiPM == "QLBV")
            {
                int id = 0;
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                    id = int.Parse(txtIDNhap.Text);
                if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
                {
                    if (!_inPhieuNhap_24009(_dataContext, id))
                    {
                        MessageBox.Show("Lỗi in phiếu");
                    }
                }
                else if (DungChung.Bien.MaBV == "27022" || DungChung.Bien.MaBV == "27023" || DungChung.Bien.MaBV == "27021" || DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14017")
                {

                    if (DungChung.Bien.MaBV == "27021")
                    {
                        FormThamSo.frm_ThongTinCT frmct = new FormThamSo.frm_ThongTinCT();
                        frmct.dsKT = new FormThamSo.frm_ThongTinCT.dsKyTen(dsKT);
                        frmct.ShowDialog();
                    }
                    if (!_inPhieuNhapVAT(_dataContext, id))
                    {
                        MessageBox.Show("Lỗi in phiếu");
                    }
                }
                else if (DungChung.Bien.MaBV == "12001" || (macq.MaChuQuan != null && macq.MaChuQuan.Trim() == "12001"))
                {
                    if (!_inPhieuNhapVAT(_dataContext, id))
                    {
                        MessageBox.Show("Lỗi in phiếu");
                    }
                }
                else

                    if (!_inPhieuNhap(_dataContext, id, 0))
                {
                    MessageBox.Show("Lỗi in phiếu");
                }
            }

            else
            {

                BaoCao.repPhieuNhapKho_NBinh rep = new BaoCao.repPhieuNhapKho_NBinh();
                int id = 0;
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                    id = int.Parse(txtIDNhap.Text);
                var par = (from nd in _dataContext.NhapDs
                           where (nd.IDNhap == id)
                           join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                           select new { nd.DiaChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, nd.MaCC }).ToList();
                if (par.Count > 0)
                {
                    rep.SoCT.Value = "Số: " + par.First().SoCT;
                    rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    if (par.First().TenNguoiCC != null)
                        rep.TenNG.Value = par.First().TenNguoiCC;
                    rep.LyDoNhap.Value = par.First().GhiChu;
                    rep.KhoNhap.Value = par.First().TenKP;
                    string macc = "";
                    if (par.First().MaCC != null)
                        macc = par.First().MaCC;
                    if (par.First().DiaChi != null)
                        rep.DiaChiNG.Value = par.First().DiaChi;
                }

                var q = (from nd in _dataContext.NhapDs
                         join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                         where (nd.IDNhap == id)
                         group new { dv, nd, ndct } by new { dv.TenDV, ndct.SoLo, ndct.HanDung, ndct.DonVi, ndct.DonGia, ndct.SoLuongN, ndct.ThanhTienN } into kq
                         select new
                         {
                             TenDuoc = kq.Key.TenDV,
                             SoLo = kq.Key.SoLo,
                             HanDung = kq.Key.HanDung,
                             DonViTinh = kq.Key.DonVi,
                             SoLuongN = kq.Sum(p => p.ndct.SoLuongN),
                             DonGia = kq.Key.DonGia,
                             ThanhTienN = kq.Sum(p => p.ndct.ThanhTienN)
                         }).ToList();
                if (q.Count > 0)
                {
                    rep.TongTien.Value = q.Sum(p => p.ThanhTienN);
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu");
                }
            }
        }

        private void btnKLuu_Click(object sender, EventArgs e)
        {
            if (TTLuu == 2)
            {
                Enablebutton(true);
                EnableControl(false);
                grvNhap_FocusedRowChanged(null, null);
            }
            if (TTLuu == 1)
            {
                TTLuu = 0;
                Frm_NhapDuoc_Load(sender, e);
            }
            TTLuu = 0;
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            Frm_NhanXuat frm = new Frm_NhanXuat(_dataContext, _medicinesProvider);
            frm.FormClosed += new FormClosedEventHandler(this.Frm_NhapDuoc_Load);
            frm.ShowDialog();
        }

        private void btnSoKN_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
                id = Convert.ToInt32(txtIDNhap.Text);
            FormThamSo.frm_BbKiemNhap frm = new FormThamSo.frm_BbKiemNhap(id);
            frm.ShowDialog();
        }

        private void btnPhieuYHCT_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "20001")
            {
                frmIn frm = new frmIn();
                BaoCao.repPhieuNhapKho_20001_DY rep = new BaoCao.repPhieuNhapKho_20001_DY();
                int id = 0;
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                    id = int.Parse(txtIDNhap.Text);
                var par = (from nd in _dataContext.NhapDs
                           where (nd.IDNhap == id)
                           join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                           join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                           select new { nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, nd.MaCC, nd.MaCB }).ToList();
                if (par.Count > 0)
                {
                    rep.So.Value = "Số: " + par.First().SoCT;
                    rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    if (!string.IsNullOrEmpty(par.First().MaCC))
                    {
                        string _macc = par.First().MaCC;
                        var _tencc = _dataContext.NhaCCs.Where(p => p.MaCC == _macc).Select(p => p.TenCC).FirstOrDefault();
                        if (_tencc != null)
                        {
                            rep.LyDoNhap.Value = "Theo số HĐ: " + par.First().SoCT + ", của công ty: " + _tencc;
                        }
                        else
                            rep.LyDoNhap.Value = par.First().GhiChu;
                    }
                    else
                        rep.LyDoNhap.Value = par.First().GhiChu;
                    rep.TenKho.Value = par.First().TenKP;
                    rep.TenNG.Value = par.First().TenNguoiCC;
                    rep.nguoigiao.Text = par.First().TenNguoiCC;
                }
                var q = (from nd in _dataContext.NhapDs
                         join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                         where (nd.IDNhap == id)
                         group new { dv, nd, ndct } by new { dv.YCSD, dv.PhuongPhap, dv.TinhTNhap, dv.NguonGoc, dv.TenDV, ndct.SoLo, ndct.HanDung, ndct.DonVi, ndct.DonGiaDY, ndct.SoLuongN, ndct.ThanhTienN } into kq
                         select new
                         {
                             kq.Key.YCSD,
                             kq.Key.TinhTNhap,
                             kq.Key.NguonGoc,
                             TenDuoc = kq.Key.TenDV,
                             DonViTinh = kq.Key.DonVi,
                             SoLuongN = kq.Sum(p => p.ndct.SoLuongN),
                             kq.Key.DonGiaDY,
                             ThanhTienN = kq.Sum(p => p.ndct.SoLuongN) * kq.Key.DonGiaDY,
                         }).ToList();
                if (q.Count > 0)
                {
                    rep.colThanhTienCong.Text = DungChung.Ham.DocTienBangChu(q.Sum(p => p.ThanhTienN), " đồng");
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {

                    MessageBox.Show("Ko co du lieu");
                }
            }
            else
            {
                frmIn frm = new frmIn();
                BaoCao.repPhieuNhapKho_YHDT rep = new BaoCao.repPhieuNhapKho_YHDT();
                int id = 0;
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                    id = int.Parse(txtIDNhap.Text);
                var par = (from nd in _dataContext.NhapDs
                           where (nd.IDNhap == id)
                           join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                           join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                           select new { nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC }).ToList();
                if (par.Count > 0)
                {
                    rep.SoCT.Value = "Số: " + par.First().SoCT;
                    rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    rep.TenNG.Value = par.First().TenNguoiCC;
                    rep.LyDoNhap.Value = par.First().GhiChu;
                    rep.KhoNhap.Value = par.First().TenKP;


                }

                var q = (from nd in _dataContext.NhapDs
                         join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                         where (nd.IDNhap == id)
                         group new { dv, nd, ndct } by new { dv.YCSD, dv.PhuongPhap, dv.TinhTNhap, dv.NguonGoc, dv.TenDV, ndct.SoLo, ndct.HanDung, ndct.DonVi, ndct.DonGiaDY, ndct.SoLuongN, ndct.ThanhTienN } into kq
                         select new
                         {
                             kq.Key.YCSD,
                             kq.Key.PhuongPhap,
                             kq.Key.TinhTNhap,
                             kq.Key.NguonGoc,
                             TenDuoc = kq.Key.TenDV,
                             SoLo = kq.Key.SoLo,
                             HanDung = kq.Key.HanDung,
                             DonViTinh = kq.Key.DonVi,
                             SoLuongN = kq.Sum(p => p.ndct.SoLuongN),
                             kq.Key.DonGiaDY,
                             ThanhTienN = kq.Sum(p => p.ndct.SoLuongN) * kq.Key.DonGiaDY,
                         }).ToList();
                if (q.Count > 0)
                {
                    rep.TongTien.Value = q.Sum(p => p.ThanhTienN);
                    rep.DataSource = q;
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {

                    MessageBox.Show("Ko co du lieu");
                }
            }
        }

        private void btnDMDuoc_Click(object sender, EventArgs e)
        {
            FormDanhMuc.Frm_DmDuoc frm = new FormDanhMuc.Frm_DmDuoc();
            frm.ShowDialog();
            lupMaCC_EditValueChanged(null, null);
        }

        private void grcNhapCT_Click(object sender, EventArgs e)
        {

        }
        private void setlydonhap()
        {

            if (cboNhap.SelectedIndex == 1)
            {
                if (DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389")
                    txtGhiChu.Text = "Theo số HĐ: " + txtSoCT.Text + ", " + DungChung.Ham.NgaySangChu(dtNgayTT.DateTime) + ", của công ty: " + lupMaCC.Text;
                else
                    txtGhiChu.Text = "Theo số HĐ: " + txtSoCT.Text + ", của công ty: " + lupMaCC.Text;

            }
        }
        private void chkNhap_CheckedChanged(object sender, EventArgs e)
        {
            if (TTLuu == 1 || TTLuu == 2)
            {
                if (cboNhap.SelectedIndex == 1)
                {
                    lupMaCC.Properties.ReadOnly = false;
                    ckNgayTT.Enabled = true;
                    if (TTLuu == 1)
                        cboThueVAT.Text = "5";
                }
                else
                {
                    ckNgayTT.Enabled = false;
                    lupMaCC.Properties.ReadOnly = true;
                    lupMaCC.EditValue = "";
                    if (TTLuu == 1)
                        cboThueVAT.Text = "0";
                }

                if (cboNhap.SelectedIndex == 2 || cboNhap.SelectedIndex == 0)
                {
                    if (cboNhap.SelectedIndex == 2)
                    {
                        grvNhapCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

                    }
                    else
                        grvNhapCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                    labelControl16.Text = "Bộ phận trả:";
                    lupBPTra.Properties.ReadOnly = false;
                    lupMaCC.Properties.ReadOnly = true;
                    lupBPTra.EditValue = -1;
                    lupMaCC.EditValue = -1;
                    lupHinhthucx.Properties.ReadOnly = false;
                    if (cboNhap.SelectedIndex == 0)
                    {
                        labelControl16.Text = "Nhập từ kho:";
                        lupHinhthucx.EditValue = 2;
                        lupHinhthucx.ReadOnly = true;
                    }
                }
                else
                {
                    lupMaCC.Properties.ReadOnly = false;
                    lupBPTra.Properties.ReadOnly = true;
                    lupBPTra.EditValue = -1;
                    lupMaCC.EditValue = -1;
                    lupHinhthucx.Properties.ReadOnly = true;
                    grvNhapCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                }

                lupMaCC_EditValueChanged(null, null);
                if (DungChung.Bien.MaBV == "24012")
                {
                    groupControl3.Text = "Chi tiết chứng từ";
                    if (cboNhap.SelectedIndex == 0 && cboNhap.SelectedIndex == 2)
                    {
                        this.cboSoLo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                    }
                    if (cboNhap.SelectedIndex == 3)
                    {
                        this.cboSoLo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    }
                }
                EnableColumn(cboNhap.SelectedIndex == 0);
            }
        }

        private void txtTimKiemTenDV_EditValueChanged(object sender, EventArgs e)
        {
            int _madv = 0;
            if (!string.IsNullOrEmpty(lupTimKiemTenDV.Text))
            {
                _madv = Convert.ToInt32(lupTimKiemTenDV.EditValue);
            }
            grcNhapCT.DataSource = _lNhapDct.Where(p => _madv == 0 ? true : p.MaDV == _madv).ToList();
        }

        private void txtSoCT_EditValueChanged(object sender, EventArgs e)
        {
            setlydonhap();
        }

        private void lupMaKP_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TTLuu == 2 || TTLuu == 1)
            {
                if (grvNhapCT.RowCount > 0)
                {
                    if (grvNhapCT.RowCount == 1)
                    {
                        if (grvNhapCT.GetRowCellValue(1, colMaDV) != null)
                        {
                            MessageBox.Show("Chứng từ đã có thuốc, bạn không thể sửa kho nhập");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chứng từ  có thuốc, bạn không thể sửa kho nhập");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void lupMaCC_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (TTLuu == 2 || TTLuu == 1)
            {
                if (grvNhapCT.RowCount > 0)
                {
                    if (grvNhapCT.RowCount == 1)
                    {
                        if (grvNhapCT.GetRowCellValue(1, colMaDV) != null)
                        {
                            if (DungChung.Bien.MaBV == "06007")
                            {
                                DialogResult _result = MessageBox.Show("Chứng từ đã có thuốc, bạn vẫn muốn thay đổi nhà cung cấp?", "Hỏi thay đổi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (_result == DialogResult.No)
                                    e.Cancel = true;
                            }
                            else
                            {
                                MessageBox.Show("Chứng từ đã có thuốc, bạn không thể sửa nhà cung cấp");
                                e.Cancel = true;
                            }
                        }
                    }
                    else
                    {
                        if (DungChung.Bien.MaBV == "06007")
                        {
                            DialogResult _result = MessageBox.Show("Chứng từ đã có thuốc, bạn vẫn muốn thay đổi nhà cung cấp?", "Hỏi thay đổi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == DialogResult.No)
                                e.Cancel = true;
                        }
                        else
                        {
                            MessageBox.Show("Chứng từ có thuốc, bạn không thể sửa nhà cung cấp");
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        bool nhathuoc = false;// kiểm tra xem phải nhà thuốc bệnh viện hay không
        bool _huhaoGia = false;// hư hao tính vào giá
        private void lupMaKP_EditValueChanged(object sender, EventArgs e)
        {
            int makho = 0;
            if (lupMaKP.EditValue != null)
                makho = Convert.ToInt32(lupMaKP.EditValue);
            var abc = _lkphong.Where(p => p.MaKP == makho).FirstOrDefault();
            if (abc != null)
            {
                if (abc.PPXuat == null)
                    MessageBox.Show(lupMaKP.Text + " chưa có phương pháp xuất dược");
                else
                    ppxuat = abc.PPXuat.Value;

                // pp hư hao đông y
                int pphh = 2;
                if (abc.PPHHDY != null)
                    pphh = Convert.ToInt32(abc.PPHHDY);

                if (pphh == 1 && (DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "14017"))
                {
                    _huhaoGia = true;
                }
                else
                    _huhaoGia = false;
            }

            nhathuoc = false;
            lupMaCC_EditValueChanged(null, null);

            #region set cho phép nhập % lãi kỳ vọng cho nhà thuốc(VAT) (trường chuyên khoa của kho là "Nhà thuốc"

            int makp = Convert.ToInt32(lupMaKP.EditValue);
            var kp = _lkphong.Where(p => p.MaKP == makp).Where(p => p.ChuyenKhoa.ToLower().Contains("nhà thuốc")).ToList();
            if (kp.Count > 0 || DungChung.Bien.MaBV == "56789")
            {
                labTLchietKhau.Visible = true;
                cboTLchietKhau.Visible = true;
                colDonGia.OptionsColumn.ReadOnly = false;
                colDonGia.OptionsColumn.AllowFocus = true;
                colDonGia.OptionsColumn.AllowEdit = true;
                colVAT.OptionsColumn.ReadOnly = true;
                colVAT.OptionsColumn.AllowFocus = false;
                nhathuoc = true;
                colTyLeCK.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "24297")
            {
                colDonGia.OptionsColumn.ReadOnly = false;
                colDonGia.OptionsColumn.AllowFocus = true;
                colDonGia.OptionsColumn.AllowEdit = true;
                labTLchietKhau.Visible = false;
                cboTLchietKhau.Visible = false;
                colTyLeCK.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "27183")
            {
                colDonGia.OptionsColumn.ReadOnly = false;
                colDonGia.OptionsColumn.AllowFocus = true;
                colDonGia.OptionsColumn.AllowEdit = true;
            }
            else
            {
                colDonGia.OptionsColumn.ReadOnly = true;
                colDonGia.OptionsColumn.AllowFocus = false;
                colDonGia.OptionsColumn.AllowEdit = false;
                labTLchietKhau.Visible = false;
                cboTLchietKhau.Visible = false;
                colTyLeCK.Visible = false;
            }
            #endregion
        }
        double _soluongluu = 0;
        private void grvNhapCT_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetTextStock();
            if (TTLuu == 2)
            {
                if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null && Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct)) > 0)
                {
                    colMaDV.OptionsColumn.ReadOnly = true;
                    if (cboNhap.SelectedIndex != 0)
                    {
                        colSoLo.OptionsColumn.AllowEdit = false;
                        colHanDung.OptionsColumn.AllowEdit = false;
                        colDonGiaCT.OptionsColumn.AllowEdit = false;
                        colDonGiaCT.OptionsColumn.AllowFocus = false;
                        colDonGia.OptionsColumn.AllowFocus = false;
                        colVAT.OptionsColumn.AllowFocus = false;
                        colSoLuong.OptionsColumn.AllowEdit = false;
                    }
                }
                else
                {
                    colMaDV.OptionsColumn.ReadOnly = false;
                    if(cboNhap.SelectedIndex != 0)
                    {
                        colSoLo.OptionsColumn.AllowEdit = true;
                        colHanDung.OptionsColumn.AllowEdit = true;
                        colDonGiaCT.OptionsColumn.AllowEdit = true;
                        colDonGiaCT.OptionsColumn.AllowFocus = true;
                        colDonGia.OptionsColumn.AllowFocus = false;
                        colSoLuong.OptionsColumn.AllowEdit = true;
                    }
                }
            }
            else
            {
                colMaDV.OptionsColumn.ReadOnly = false;
            }
        }

        private void grvNhapCT_DataSourceChanged(object sender, EventArgs e)
        {
            grvNhapCT_FocusedRowChanged(null, null);
        }

        private void grvNhapCT_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            grvNhapCT_FocusedRowChanged(null, null);
        }


        private void cboNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetControl();
            if (cboNhap.SelectedIndex == 2 && (TTLuu == 1 || TTLuu == 2))
            {
                MessageBox.Show("Chức năng hiện không được sử dụng", "Thông báo");
                cboNhap.SelectedIndex = -1;
            }
            else
            {
                if(TTLuu == 1)
                {
                    lupMaKP.EditValue = DungChung.Bien.MaKP;
                }
                if (cboNhap.SelectedIndex == 0)
                {
                    txtSoCT.Enabled = false;
                }
                else
                {
                    txtSoCT.Enabled = true;
                }
                chkNhap_CheckedChanged(sender, e);
            }
        }

        private void btnDMGia_Click(object sender, EventArgs e)
        {
            FormNhap.frm_DonGiaDV frm = new frm_DonGiaDV();
            frm.ShowDialog();

        }

        private void cboThueVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboThueVAT.Text))
            {
                int vat = int.Parse(cboThueVAT.Text);
                grvNhapCT.SetFocusedRowCellValue(colVAT, vat);
            }
        }

        private void cboTLchietKhau_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboTLchietKhau.Text))
            {
                int TyLeCK = int.Parse(cboTLchietKhau.Text);
                grvNhapCT.SetFocusedRowCellValue(colTyLeCK, TyLeCK);
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxEdit1.SelectedIndex)
            {
                case 0:
                    int _makp = 0;
                    if (lupTimMaKP.EditValue != null)
                        _makp = Convert.ToInt32(lupTimMaKP.EditValue);
                    FormThamSo.frm_BCSLTon_ChenhLech frm = new FormThamSo.frm_BCSLTon_ChenhLech(_makp);
                    frm.ShowDialog();
                    break;
                case 1:
                    FormNhap.frm_DonGiaDV frms = new frm_DonGiaDV();
                    frms.ShowDialog();
                    break;
                case 2:
                    int id = 0;
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        id = int.Parse(txtIDNhap.Text);
                    if (!_inPhieuNhap(_dataContext, id, 1))
                    {
                        MessageBox.Show("Lỗi in phiếu");
                    }
                    break;
                case 3:
                    int idnhap = 0;
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        idnhap = int.Parse(txtIDNhap.Text);
                    if (!_inPhieuNhap(_dataContext, idnhap, 2))
                    {
                        MessageBox.Show("Lỗi in phiếu");
                    }
                    break;
                case 4:
                    FormThamSo.Frm_TheoDoiPhanLoaiMuaThuoc24012 frm577 = new FormThamSo.Frm_TheoDoiPhanLoaiMuaThuoc24012();
                    frm577.ShowDialog();
                    break;
                case 5:
                    if (cboNhap.Text == "Nhập chuyển kho" && lupMaKP.Text.Contains("Kho") && lupBPTra.Text.Contains("Kho"))
                    {
                        if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        {
                            int id1 = Convert.ToInt32(txtIDNhap.Text);
                            var par = (from nd in _dataContext.NhapDs
                                       where (nd.IDNhap == id1)
                                       join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                                       join ncc in _dataContext.NhaCCs on nd.MaCC equals ncc.MaCC into k
                                       from k1 in k.DefaultIfEmpty()
                                       select new { nd.KieuDon, nd.DiaChi, DiaChi3 = kp.DChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, MaCC = k1 != null ? k1.MaCC : "", TenCC = k1 != null ? k1.TenCC : "", nd.PLoai, nd.SoPL, nd.MaKP, }).ToList();
                            string _mauso = "";

                            var q2 = (from nd in _dataContext.NhapDs
                                      join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                      join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                                      where (nd.IDNhap == id1)
                                      select new
                                      {
                                          nd,
                                          ndct,
                                          dv
                                      }).ToList();
                            var q = (from nd in q2
                                     group nd by new { nd.dv.HamLuong, nd.dv.MaTam, nd.dv.TyLeSD, nd.ndct.VAT, nd.ndct.IDNhapct, nd.dv.TenDV, nd.dv.MaQD, nd.ndct.SoLo, nd.ndct.HanDung, nd.dv.DonViN, nd.ndct.DonGia, nd.ndct.DonGiaCT, nd.ndct.SoLuongN, nd.ndct.ThanhTienN, nd.dv.MaDV } into kq
                                     select new
                                     {
                                         madv = kq.Key.MaDV,
                                         kq.Key.MaTam,
                                         kq.Key.MaQD,
                                         kq.Key.IDNhapct,
                                         kq.Key.HamLuong,
                                         TenDuoc = kq.Key.TenDV,
                                         SoLo = kq.Key.SoLo,
                                         HanDung = kq.Key.HanDung,
                                         DonViTinh = kq.Key.DonViN,
                                         SoLuongN = kq.Sum(p => p.ndct.SoLuongN) / kq.Key.TyLeSD,
                                         DonGia = kq.Key.DonGiaCT * (kq.Key.VAT + 100) / 100,
                                         DonGiaCT = kq.Key.DonGiaCT * kq.Key.TyLeSD,
                                         ThanhTienN = kq.Sum(p => p.ndct.SoLuongN) * kq.Key.DonGiaCT * (kq.Key.VAT + 100) / 100
                                     }).OrderBy(p => p.IDNhapct).ToList();

                            double tongtien = Math.Round(Convert.ToDouble(q.Sum(p => p.ThanhTienN)), 3);

                            frmIn frm3 = new frmIn();
                            BaoCao.Phieulinhchokhoa_24012 rep3 = new BaoCao.Phieulinhchokhoa_24012();

                            rep3.SubBand1.Visible = false;
                            rep3.SubBand2.Visible = true;
                            rep3.SubBand3.Visible = false;
                            rep3.SubBand4.Visible = true;
                            rep3.xrTable2.Visible = false;
                            rep3.xrTable8.Visible = true;
                            rep3.xrTable3.Visible = false;
                            rep3.xrTable9.Visible = true;
                            rep3.celltenthuoc.Text = "Tên vật tư y tế tiêu hao";

                            rep3.Boyte1.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep3.Benhvien1.Value = DungChung.Bien.TenCQ.ToUpper();
                            if (!string.IsNullOrEmpty(lupBPTra.Text))
                            {
                                var ten = _dataContext.KPhongs.Where(p => p.TenKP == (lupMaKP.Text)).ToList();
                                rep3.Khoa1.Value = ten.First().TenKP.ToUpper();
                            }

                            rep3.LoaiPL1.Value = "Phân loại: YC Nhập";
                            if (!string.IsNullOrEmpty(dtNgayNhap.Text))
                            {
                                rep3.theongay1.Value = DungChung.Ham.NgaySangChu(Convert.ToDateTime(dtNgayNhap.Text), 1);
                            }

                            rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH VẬT TƯ Y TẾ TIÊU HAO");
                            rep3.MauSo1.Value = "MS:...D / BV - 01";
                            rep3.SoPL1.Value = txtIDNhap.Text;

                            rep3.ngaytaophieu1.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);

                            rep3.DataSource = q;
                            rep3.BindingData();
                            rep3.CreateDocument();
                            frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                            frm3.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hình thức nhập và kho nhập không phù hợp!", "Thông báo!");
                    }
                    break;
                case 6:
                    if (cboNhap.Text == "Nhập chuyển kho" && lupMaKP.Text.Contains("Kho") && lupBPTra.Text.Contains("Kho"))
                    {
                        if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        {
                            int id1 = Convert.ToInt32(txtIDNhap.Text);
                            var par = (from nd in _dataContext.NhapDs
                                       where (nd.IDNhap == id1)
                                       join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                                       join ncc in _dataContext.NhaCCs on nd.MaCC equals ncc.MaCC into k
                                       from k1 in k.DefaultIfEmpty()
                                       select new { nd.KieuDon, nd.DiaChi, DiaChi3 = kp.DChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, MaCC = k1 != null ? k1.MaCC : "", TenCC = k1 != null ? k1.TenCC : "", nd.PLoai, nd.SoPL, nd.MaKP }).ToList();
                            string _mauso = "";

                            var q2 = (from nd in _dataContext.NhapDs
                                      join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                      join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                                      where (nd.IDNhap == id1)
                                      select new
                                      {
                                          nd,
                                          ndct,
                                          dv
                                      }).ToList();
                            var q = (from nd in q2
                                     group nd by new { nd.dv.HamLuong, nd.dv.MaTam, nd.dv.TyLeSD, nd.ndct.VAT, nd.ndct.IDNhapct, nd.dv.TenDV, nd.dv.MaQD, nd.ndct.SoLo, nd.ndct.HanDung, nd.dv.DonViN, nd.ndct.DonGia, nd.ndct.DonGiaCT, nd.ndct.SoLuongN, nd.ndct.ThanhTienN, nd.dv.MaDV } into kq
                                     select new
                                     {
                                         madv = kq.Key.MaDV,
                                         kq.Key.MaTam,
                                         kq.Key.MaQD,
                                         kq.Key.IDNhapct,
                                         kq.Key.HamLuong,
                                         TenDuoc = kq.Key.TenDV,
                                         SoLo = kq.Key.SoLo,
                                         HanDung = kq.Key.HanDung,
                                         DonViTinh = kq.Key.DonViN,
                                         SoLuongN = kq.Sum(p => p.ndct.SoLuongN) / kq.Key.TyLeSD,
                                         DonGia = kq.Key.DonGiaCT * (kq.Key.VAT + 100) / 100,
                                         DonGiaCT = kq.Key.DonGiaCT * kq.Key.TyLeSD,
                                         ThanhTienN = kq.Sum(p => p.ndct.SoLuongN) * kq.Key.DonGiaCT * (kq.Key.VAT + 100) / 100
                                     }).OrderBy(p => p.IDNhapct).ToList();

                            double tongtien = Math.Round(Convert.ToDouble(q.Sum(p => p.ThanhTienN)), 3);
                            frmIn frm3 = new frmIn();
                            BaoCao.Phieulinhchokhoa_24012 rep3 = new BaoCao.Phieulinhchokhoa_24012();

                            rep3.SubBand1.Visible = false;
                            rep3.SubBand2.Visible = true;
                            rep3.SubBand3.Visible = false;
                            rep3.SubBand4.Visible = true;
                            rep3.xrTable2.Visible = false;
                            rep3.xrTable8.Visible = true;
                            rep3.xrTable3.Visible = false;
                            rep3.xrTable9.Visible = true;
                            rep3.celltenthuoc.Text = "Tên hóa chất";

                            rep3.Boyte1.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep3.Benhvien1.Value = DungChung.Bien.TenCQ.ToUpper();
                            if (!string.IsNullOrEmpty(lupBPTra.Text))
                            {
                                var ten = _dataContext.KPhongs.Where(p => p.TenKP == (lupMaKP.Text)).ToList();
                                rep3.Khoa1.Value = ten.First().TenKP.ToUpper();
                            }
                            rep3.LoaiPL1.Value = "Phân loại: YC Nhập";
                            if (!string.IsNullOrEmpty(dtNgayNhap.Text))
                            {
                                rep3.theongay1.Value = DungChung.Ham.NgaySangChu(Convert.ToDateTime(dtNgayNhap.Text), 1);
                            }

                            rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH HÓA CHẤT");
                            rep3.MauSo1.Value = "MS:...D / BV - 01";
                            rep3.SoPL1.Value = txtIDNhap.Text;

                            rep3.ngaytaophieu1.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);

                            rep3.DataSource = q;
                            rep3.BindingData();
                            rep3.CreateDocument();
                            frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                            frm3.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hình thức nhập và kho nhập không phù hợp!", "Thông báo!");
                    }
                    break;
                case 7:
                    if (cboNhap.Text == "Nhập chuyển kho" && lupMaKP.Text.Contains("Kho") && lupBPTra.Text.Contains("Kho"))
                    {
                        if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        {
                            int id1 = Convert.ToInt32(txtIDNhap.Text);
                            var par = (from nd in _dataContext.NhapDs
                                       where (nd.IDNhap == id1)
                                       join kp in _dataContext.KPhongs on nd.MaKP equals kp.MaKP
                                       join ncc in _dataContext.NhaCCs on nd.MaCC equals ncc.MaCC into k
                                       from k1 in k.DefaultIfEmpty()
                                       select new { nd.KieuDon, nd.DiaChi, DiaChi3 = kp.DChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, MaCC = k1 != null ? k1.MaCC : "", TenCC = k1 != null ? k1.TenCC : "", nd.PLoai, nd.SoPL, nd.MaKP }).ToList();
                            string _mauso = "";

                            var q2 = (from nd in _dataContext.NhapDs
                                      join ndct in _dataContext.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                      join dv in _dataContext.DichVus on ndct.MaDV equals dv.MaDV
                                      where (nd.IDNhap == id1)
                                      select new
                                      {
                                          nd,
                                          ndct,
                                          dv
                                      }).ToList();
                            var q = (from nd in q2
                                     group nd by new { nd.dv.HamLuong, nd.dv.MaTam, nd.dv.TyLeSD, nd.ndct.VAT, nd.ndct.IDNhapct, nd.dv.TenDV, nd.dv.MaQD, nd.ndct.SoLo, nd.ndct.HanDung, nd.dv.DonViN, nd.ndct.DonGia, nd.ndct.DonGiaCT, nd.ndct.SoLuongN, nd.ndct.ThanhTienN, nd.dv.MaDV } into kq
                                     select new
                                     {
                                         madv = kq.Key.MaDV,
                                         kq.Key.MaTam,
                                         kq.Key.MaQD,
                                         kq.Key.IDNhapct,
                                         kq.Key.HamLuong,
                                         TenDuoc = kq.Key.TenDV + " " + kq.Key.HamLuong,
                                         SoLo = kq.Key.SoLo,
                                         HanDung = kq.Key.HanDung,
                                         DonViTinh = kq.Key.DonViN,
                                         SoLuongN = kq.Sum(p => p.ndct.SoLuongN) / kq.Key.TyLeSD,
                                         DonGia = kq.Key.DonGiaCT * (kq.Key.VAT + 100) / 100,
                                         DonGiaCT = kq.Key.DonGiaCT * kq.Key.TyLeSD,
                                         ThanhTienN = kq.Sum(p => p.ndct.SoLuongN) * kq.Key.DonGiaCT * (kq.Key.VAT + 100) / 100
                                     }).OrderBy(p => p.IDNhapct).ToList();

                            double tongtien = Math.Round(Convert.ToDouble(q.Sum(p => p.ThanhTienN)), 3);
                            frmIn frm3 = new frmIn();
                            BaoCao.Phieulinhchokhoa_24012 rep3 = new BaoCao.Phieulinhchokhoa_24012();

                            rep3.SubBand1.Visible = false;
                            rep3.SubBand2.Visible = true;
                            rep3.SubBand3.Visible = false;
                            rep3.SubBand4.Visible = true;
                            rep3.xrTable2.Visible = false;
                            rep3.xrTable8.Visible = true;
                            rep3.xrTable3.Visible = false;
                            rep3.xrTable9.Visible = true;
                            rep3.celltenthuoc.Text = "Tên thuốc, hàm lượng";

                            rep3.Boyte1.Value = DungChung.Bien.TenCQCQ.ToUpper();
                            rep3.Benhvien1.Value = DungChung.Bien.TenCQ.ToUpper();
                            if (!string.IsNullOrEmpty(lupBPTra.Text))
                            {
                                var ten = _dataContext.KPhongs.Where(p => p.TenKP == (lupMaKP.Text)).ToList();
                                rep3.Khoa1.Value = ten.First().TenKP.ToUpper();
                            }
                            rep3.LoaiPL1.Value = "Phân loại: YC Nhập";
                            if (!string.IsNullOrEmpty(dtNgayNhap.Text))
                            {
                                rep3.theongay1.Value = DungChung.Ham.NgaySangChu(Convert.ToDateTime(dtNgayNhap.Text), 1);
                            }

                            rep3.Loaiphieulinh1.Value = ("PHIẾU LĨNH THUỐC");
                            rep3.MauSo1.Value = "MS:...D / BV - 01";
                            rep3.SoPL1.Value = txtIDNhap.Text;

                            rep3.ngaytaophieu1.Value = DungChung.Ham.NgaySangChu(DateTime.Now, 1);

                            rep3.DataSource = q;
                            rep3.BindingData();
                            rep3.CreateDocument();
                            frm3.prcIN.PrintingSystem = rep3.PrintingSystem;
                            frm3.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hình thức nhập và kho nhập không phù hợp!", "Thông báo!");
                    }
                    break;
            }
            comboBoxEdit1.SelectedIndex = -1;
        }


        private void gridLookUpEdit1View_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            CustomGridView view = (CustomGridView)sender;
            if (!view.OptionsView.ShowAutoFilterRow || !view.IsDataRow(e.RowHandle))
                return;

            string filterCellText = view.GetRowCellDisplayText(GridControl.AutoFilterRowHandle, e.Column);



            if (String.IsNullOrEmpty(filterCellText))
                return;

            string temp = CustomGridView.RemoveDiacritics(e.DisplayText, true);
            int filterTextIndex = temp.IndexOf(filterCellText, StringComparison.CurrentCultureIgnoreCase);
            if (filterTextIndex == -1)
                return;

            XPaint.Graphics.DrawMultiColorString(e.Cache, e.Bounds, e.DisplayText, filterCellText, e.Appearance, Color.Black, Color.Gold, false, filterTextIndex);
            e.Handled = true;
        }

        private void dtNgayTT_EditValueChanged(object sender, EventArgs e)
        {
            setlydonhap();
        }
        private void lupBPTra_EditValueChanged_1(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012" && cboNhap.Text == "Nhập chuyển kho")
            {
                lupMaCC_EditValueChanged(null, null);
            }
        }

        private void SetTextStock()
        {
            int maDV = 0;
            int idThuoc = 0;
            string soLo = "";
            DateTime hanDung = new DateTime();
            double donGia = 0;

            if ((grvNhapCT.GetFocusedRowCellValue(colMaDV) == null && ppxuat == 1) || (grvNhapCT.GetFocusedRowCellValue(colIDThuoc) == null && ppxuat == 3) || cboNhap.SelectedIndex != 0)
            {
                this.groupControl3.Text = "Danh sách đơn thuốc";
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
                            this.groupControl3.Text = "Số lượng tồn: " + medicine.TonHienTai;
                    }
                }
                else if(ppxuat == 3)
                {
                    if ((grvNhapCT.GetFocusedRowCellValue(colIDThuoc) != null && grvNhapCT.GetFocusedRowCellValue(colIDThuoc).ToString() != ""))
                    {
                        idThuoc = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDThuoc));

                        var medicine = medicinesByRoom.FirstOrDefault(p => p.IDThuoc == idThuoc);

                        if (medicine != null)
                            this.groupControl3.Text = "Số lượng tồn: " + medicine.TonHienTai;
                    }
                }
            }
        }
    }
}
