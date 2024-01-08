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

namespace QLBV.FormNhap
{
    public partial class usNhapDuoc : DevExpress.XtraEditors.XtraUserControl
    {
        public usNhapDuoc()
        {
            InitializeComponent();
        }
        DateTime _dttu = System.DateTime.Now;
        DateTime _dtden = System.DateTime.Now;
        List<NhapD> _lNhapDuoc = new List<NhapD>();
        List<NhapDct> _lNhapDct = new List<NhapDct>();
        List<KPhong> _lkphong = new List<KPhong>();
        List<NhapDctss> _listND = new List<NhapDctss>();
        List<DungChung.Ham.giaSoLoHSD> _listGiaSua = new List<DungChung.Ham.giaSoLoHSD>();
        int _makho = 0;
        string _macc = "-1";
        string _soPhieu = "";
        int TTLuu = 0;
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
        private void ResetControl()
        {
            dtNgayNhap.EditValue = System.DateTime.Now;
            txtIDNhap.Text = "";
            txtSoCT.Text = "";
            txtGhiChu.Text = "";
            cboNguoiGiao.Text = "";
            lupMaCC.EditValue = "";

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
        private void usNhapDuoc_Load(object sender, EventArgs e)
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
            if(DungChung.Bien.MaBV == "24012")
            {
                lupMaDuoc.Columns["SoLo"].Visible = false;
                lupMaDuoc.Columns["HanDung"].Visible = false;
            }

            var qCQCQ = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
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
            _lkphong = (from KhoaKham in data.KPhongs.Where(p => p.Status == 1).OrderBy(p => p.TenKP) select KhoaKham).ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
            {
                lupTimMaKP.Properties.DataSource = _lkphong.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || (p.PLoai.ToUpper() == DungChung.Bien.st_PhanLoaiKP.XaPhuong.ToUpper() && DungChung.Bien.MaBV == "30007")).ToList();
            }
            else
            {

                var q = (from a in _lkphong.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || (p.PLoai.ToUpper() == DungChung.Bien.st_PhanLoaiKP.XaPhuong.ToUpper() && DungChung.Bien.MaBV == "30007"))
                         join b in DungChung.Bien.listKPHoatDong on a.MaKP equals b
                         select a).ToList();
                lupTimMaKP.Properties.DataSource = q;
            }
            lupMaKP.Properties.DataSource = _lkphong.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.QuayThuoc).ToList();
            lupMaKPds.DataSource = _lkphong.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.QuayThuoc).ToList();
            lupBPTra.Properties.DataSource = _lkphong.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai.ToUpper() == DungChung.Bien.st_PhanLoaiKP.XaPhuong.ToUpper() || p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang).ToList();

            var qu = (from CungCap in data.NhaCCs.Where(p => p.Status == 1) select CungCap).OrderBy(p => p.TenCC).ToList();
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
            _ldichvu = new List<DVu>();
            List<DichVu> ldv = (from dv in data.DichVus.Where(p => (p.PLoai == 1 || p.PLoai == 5) && p.Status == 1 && p.MaKPsd.Contains(_makpsd)) select dv).OrderBy(p => p.TenDV).ToList();

            if (DungChung.Bien.MaBV == "24012" && _makpsd!= "")
            {
                var _ldtct = (from dv in ldv
                              join dtct in data.DThuoccts on dv.MaDV equals dtct.MaDV
                              select new { dv.TenDV, dv.MaDV, dv.SoDK, dv.DonVi, dv.MaTam, dv.DonGia, dv.HamLuong, dtct.SoLo, dtct.HanDung }).ToList();
                foreach (var a in _ldtct)
                {
                    DVu moi = new DVu();
                    moi.TenDV = a.TenDV;
                    moi.MaDV = a.MaDV;
                    moi.SoDK = a.SoDK;
                    moi.DonVi = a.DonVi;
                    moi.MaTam = a.MaTam;
                    moi.DonGia = a.DonGia;
                    moi.HamLuong = a.HamLuong;
                    moi.SoLo = a.SoLo;
                    moi.HanDung = a.HanDung;
                    _ldichvu.Add(moi);
                }
            }
            else if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009" || DungChung.Bien.MaBV == "26062")
            {

                foreach (var a in ldv)
                {
                    DVu moi = new DVu();
                    moi.TenDV = a.TenRG ?? "";
                    moi.MaDV = a.MaDV;
                    moi.SoDK = a.SoDK;
                    moi.DonVi = a.DonVi;
                    moi.MaTam = a.MaTam;
                    moi.DonGia = a.DonGia;
                    moi.HamLuong = a.HamLuong;
                    _ldichvu.Add(moi);

                }
            }
            else
            {
                foreach (var a in ldv)
                {
                    DVu moi = new DVu();
                    moi.TenDV = a.TenDV;
                    moi.MaDV = a.MaDV;
                    moi.SoDK = a.SoDK;
                    moi.DonVi = a.DonVi;
                    moi.MaTam = a.MaTam;
                    moi.DonGia = a.DonGia;
                    moi.HamLuong = a.HamLuong;
                    _ldichvu.Add(moi);
                }
            }
            lupMaDuoc.DataSource = _ldichvu.OrderBy(p => p.TenDV).ToList();
            lupTimMaKP.EditValue = DungChung.Bien.MaKP;
            TimKiem();
            int idct = 0;
            if (!string.IsNullOrEmpty(txtIDNhap.Text))
            {
                idct = Convert.ToInt32(txtIDNhap.Text);
            }

            _lNhapDct = data.NhapDcts.Where(p => p.IDNhap == idct).ToList();
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
                    if (grvNhapCT.GetRowCellValue(i, colSoLo) == null || grvNhapCT.GetRowCellValue(i, colSoLo).ToString().Trim() == "")
                    {
                        if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                            dsthuoc += grvNhapCT.GetRowCellDisplayText(i, colMaDV);
                    }
                }

                if (!string.IsNullOrEmpty(dsthuoc))
                {
                    dsthuoc += " chưa nhập số lô";
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
                    QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    foreach (NhapDct ndct in _lNhapDct)
                    {
                        double slThau = 0;
                        string tenthuoc = "";
                        var qdv = _data.DichVus.Where(p => p.MaDV == ndct.MaDV.Value).FirstOrDefault();
                        if (qdv != null && qdv.SLuong != null)
                        {
                            slThau = qdv.SLuong.Value;
                            tenthuoc = qdv.TenDV;
                        }
                        var qtongnhap = (from nd in _data.NhapDs.Where(p => p.PLoai == 1 && p.KieuDon == 1).Where(p => p.IDNhap != idnhap)
                                         join ct in _data.NhapDcts on nd.IDNhap equals ct.IDNhap
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

            return true;
        }
        #endregion
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void suaXuatNoiBo(int idNhapDc)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            // luu bang NhapD
            bool luutt = true;
            if (KT() && DungChung.Ham._checkNgayKhoa(data, dtNgayNhap.DateTime, "KhoaDC") == false)
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
                            data.NhapDs.Add(nhap);
                            List<DichVu> listDichVus = new List<DichVu>();
                            if (data.SaveChanges() >= 0)
                            {
                                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                int idnhap = nhap.IDNhap;

                                for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                                {
                                    if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
                                    {
                                        if (grvNhapCT.GetRowCellValue(i, colDonGiaCT) != null && grvNhapCT.GetRowCellValue(i, colDonGiaCT).ToString() != "")
                                        {
                                            if (grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "" && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "0")
                                            {
                                                NhapDct nhapdct = new NhapDct();
                                                nhapdct.IDNhap = idnhap;
                                                nhapdct.MaDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                nhapdct.DonGiaCT = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGiaCT).ToString());
                                                nhapdct.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                nhapdct.SoLuongN = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());

                                                int madv = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                //double b = grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null ? (double)grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) : 0;
                                                // c = grvNhapCT.GetFocusedRowCellValue(colSoLo) != null ? (string)grvNhapCT.GetFocusedRowCellValue(colSoLo) : "";
                                                int makhotra = lupBPTra.EditValue == null ? 0 : Convert.ToInt32(lupBPTra.EditValue);

                                                var ct = DungChung.Ham._checkTon_KD(data, madv, makhotra, nhapdct.DonGiaCT, 0, nhapdct.SoLo);
                                                //if (nhapdct.SoLuongN > DungChung.Ham._checkTon_KD(data, madv, makhotra, nhapdct.DonGiaCT, 0, nhapdct.SoLo) && DungChung.Bien.MaBV == "24012" && cboNhap.Text == "Nhập chuyển kho")
                                                //{ 
                                                //    MessageBox.Show("Số lượng nhập lớn hơn số lượng tồn! Lưu không thành công!");
                                                //    return;
                                                //}

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
                                                    nhapdct.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                else
                                                    nhapdct.SoLo = "";
                                                if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                    nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                    nhapdct.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
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
                                                data.NhapDcts.Add(nhapdct);
                                                listDichVus.Add(new DichVu { MaDV = (nhapdct.MaDV ?? 0) });
                                                try
                                                {
                                                    data.SaveChanges();
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
                            NhapD nhaps = data.NhapDs.Single(p => p.IDNhap == id);
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
                            data.SaveChanges();
                            //Luu bang NhapDct
                            // lấy ID max trong bang NhapD
                            List<DichVu> listDichVu1s = new List<DichVu>();

                            for (int i = 0; i < grvNhapCT.DataRowCount; i++)
                            {
                                if (grvNhapCT.GetRowCellValue(i, colMaDV) != null)
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
                                                    nhapdct.MaDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    nhapdct.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    if (grvNhapCT.GetRowCellValue(i, colMaCCct) != null)
                                                        nhapdct.MaCC = grvNhapCT.GetRowCellValue(i, colMaCCct).ToString();
                                                    else
                                                        nhapdct.MaCC = "";
                                                    nhapdct.DonGiaCT = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGiaCT).ToString());
                                                    nhapdct.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdct.SoLuongN = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
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
                                                        nhapdct.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    else
                                                        nhapdct.SoLo = "";
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdct.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdct.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
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
                                                    data.NhapDcts.Add(nhapdct);
                                                    data.SaveChanges();
                                                    listDichVu1s.Add(new DichVu { MaDV = (nhapdct.MaDV ?? 0) });
                                                }
                                                else
                                                {
                                                    DungChung.Ham.CheckBangTonDuoc(data, Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV)), lupMaKP.EditValue == null ? 0 : Convert.ToInt32(lupMaKP.EditValue), double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString()));
                                                    NhapDct nhapdcts = data.NhapDcts.Single(p => p.IDNhapct == idct);
                                                    nhapdcts.MaDV = Convert.ToInt32(grvNhapCT.GetRowCellValue(i, colMaDV));
                                                    if (grvNhapCT.GetRowCellValue(i, colMaCCct) != null)
                                                        nhapdcts.MaCC = grvNhapCT.GetRowCellValue(i, colMaCCct).ToString();
                                                    else
                                                        nhapdcts.MaCC = "";
                                                    nhapdcts.DonVi = grvNhapCT.GetRowCellValue(i, colDonVi).ToString().Trim();
                                                    nhapdcts.DonGiaCT = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGiaCT).ToString());
                                                    nhapdcts.DonGia = double.Parse(grvNhapCT.GetRowCellValue(i, colDonGia).ToString());
                                                    nhapdcts.SoLuongN = double.Parse(grvNhapCT.GetRowCellValue(i, colSoLuong).ToString());
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
                                                        nhapdcts.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                    else
                                                        nhapdcts.SoLo = "";
                                                    if (grvNhapCT.GetRowCellValue(i, colSoDangKy) != null && grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString() != "")
                                                        nhapdcts.SoDangKy = grvNhapCT.GetRowCellValue(i, colSoDangKy).ToString();
                                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                                        nhapdcts.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
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
                                                    data.SaveChanges();
                                                    listDichVu1s.Add(new DichVu { MaDV = (nhapdcts.MaDV ?? 0) });
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
            if (DungChung.Ham.checkQuyen(this.Name)[0])
            {
                Enablebutton(false);
                EnableControl(true);
                ResetControl();
                txtSoCT.Focus();
                lupMaKP.EditValue = DungChung.Bien.MaKP;
                _lNhapDct = data.NhapDcts.Where(p => p.IDNhap == 0).ToList();
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
            data = new QLBVEntities(DungChung.Bien.StrCon);
            List<DichVu> ldv_27194 = (from dv in data.DichVus.Where(p => p.ISSuDung == true || p.ISSuDung == null && (p.PLoai == 1 || p.PLoai == 5) && p.MaKPsd.Contains(_makpsd)) select dv).OrderBy(p => p.TenDV).ToList();
            List<DichVu> ldv = (from dv in data.DichVus.Where(p => (p.PLoai == 1 || p.PLoai == 5) && p.MaKPsd.Contains(_makpsd)) select dv).OrderBy(p => p.TenDV).ToList();
            _ldichvu = new List<DVu>();

            if (DungChung.Bien.MaBV == "24012" && ppxuat == 3 && cboNhap.Text == "Nhập chuyển kho" && 1 == 0) // fix mat ten dv (test)
            {
                int makp = 0;
                if (lupBPTra.EditValue != null)
                {
                    makp = Convert.ToInt32(lupBPTra.EditValue);
                }

                var _ldtct = (from nd in data.NhapDs.Where(p => p.MaKP == makp)
                              join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                              join dv in data.DichVus.Where(p => (p.PLoai == 1 || p.PLoai == 5) && p.MaKPsd.Contains(_makpsd)) on ndct.MaDV equals dv.MaDV
                              select new { dv.Status, dv.TenDV, dv.MaTam, ndct.MaDV, ndct.DonVi, ndct.DonGia, dv.HamLuong, ndct.SoLo, ndct.HanDung }).Distinct().ToList();
                foreach (var a in _ldtct)
                {
                    DVu moi = new DVu();
                    moi.Status = a.Status;
                    moi.TenDV = a.TenDV;
                    moi.MaDV = (int)a.MaDV;
                    //moi.SoDK = a.SoDK;
                    moi.DonVi = a.DonVi;
                    moi.MaTam = a.MaTam;
                    moi.DonGia = a.DonGia;
                    moi.HamLuong = a.HamLuong;
                    moi.SoLo = a.SoLo;
                    moi.HanDung = a.HanDung;
                    _ldichvu.Add(moi);
                }
                //foreach (var a in ldv)
                //{
                //    DVu moi = new DVu();
                //    moi.TenDV = a.TenDV;
                //    moi.MaDV = a.MaDV;
                //    moi.DonVi = a.DonVi;
                //    moi.SoDK = a.SoDK;
                //    moi.MaTam = a.MaTam;
                //    moi.MaCC = a.MaCC;
                //    moi.Status = a.Status;
                //    moi.DonGia = a.DonGia;
                //    moi.HamLuong = a.HamLuong;
                //    _ldichvu.Add(moi);
                //}
            }
            else if(DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009" || DungChung.Bien.MaBV == "26062")
            {

                foreach (var a in ldv)
                {
                    DVu moi = new DVu();
                    moi.TenDV = a.TenRG ?? "";
                    moi.MaDV = a.MaDV;
                    moi.DonVi = a.DonVi;
                    moi.SoDK = a.SoDK;
                    moi.MaTam = a.MaTam;
                    moi.MaCC = a.MaCC;
                    moi.Status = a.Status;
                    moi.DonGia = a.DonGia;
                    moi.HamLuong = a.HamLuong;
                    _ldichvu.Add(moi);

                }
            }
            else if(DungChung.Bien.MaBV == "27194")
            {
                foreach (var a in ldv_27194)
                {
                    DVu moi = new DVu();
                    moi.TenDV = a.TenDV;
                    moi.MaDV = a.MaDV;
                    moi.DonVi = a.DonVi;
                    moi.SoDK = a.SoDK;
                    moi.MaTam = a.MaTam;
                    moi.MaCC = a.MaCC;
                    moi.Status = a.Status;
                    moi.DonGia = a.DonGia;
                    moi.HamLuong = a.HamLuong;
                    _ldichvu.Add(moi);
                }
            }    
            else
            {
                foreach (var a in ldv)
                {
                    DVu moi = new DVu();
                    moi.TenDV = a.TenDV;
                    moi.MaDV = a.MaDV;
                    moi.DonVi = a.DonVi;
                    moi.SoDK = a.SoDK;
                    moi.MaTam = a.MaTam;
                    moi.MaCC = a.MaCC;
                    moi.Status = a.Status;
                    moi.DonGia = a.DonGia;
                    moi.HamLuong = a.HamLuong;
                    _ldichvu.Add(moi);
                }
            }
            if (lupMaCC.EditValue != null && lupMaCC.EditValue.ToString() != "" && (TTLuu == 1 || TTLuu == 2))
            {
                _macc = lupMaCC.EditValue.ToString();
                var que = (from NguoiGiao in data.NhaCCs where NguoiGiao.MaCC == (_macc) select new { NguoiGiao.NguoiCC, NguoiGiao.DiaChi }).ToList();
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
                    var kho = data.KPhongs.Where(p => p.MaKP == _makho).Select(p => p.QuanLy).ToList();
                    if (kho.Count > 0 && kho.First() != null)
                        _ql = kho.First().Value;
                    if (_ql == 1)
                        lupMaDuoc.DataSource = _ldichvu.Where(p => p.MaCC == (_macc) && p.Status == 1).OrderBy(p => p.TenDV).ToList();
                    else
                    {
                        if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && cboNhap.Text == "Nhập chuyển kho")
                        {
                            if (ppxuat == 3)
                            {
                                var duoc2 = (from nhapduoc in data.NhapDcts.Where(p => p.SoLuongN > p.SoLuongX)
                                             join nduoc in data.NhapDs.Where(p => p.MaKP == _makhotra) on nhapduoc.IDNhap equals nduoc.IDNhap
                                             select new { nhapduoc.MaDV, nduoc.MaKP }
                           ).ToList();
                                var duoc = (from tenduoc in _ldichvu.Where(o => o.Status == 1)
                                            join nhapduoc in duoc2 on tenduoc.MaDV equals nhapduoc.MaDV
                                            select new { tenduoc.SoLo, tenduoc.HanDung, tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, nhapduoc.MaKP, tenduoc.MaTam }
                                            ).OrderBy(p => p.TenDV).ToList().Distinct().ToList();
                                var duoc24012 = (from tenduoc in _ldichvu.Where(o => o.Status == 1)
                                                 join nhapduoc in duoc2 on tenduoc.MaDV equals nhapduoc.MaDV
                                                 select new {tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, nhapduoc.MaKP, tenduoc.MaTam, tenduoc.DonGia }
                                            ).OrderBy(p => p.TenDV).ToList().Distinct().ToList();
                                lupMaDuoc.DataSource = duoc;
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    lupMaDuoc.DataSource = duoc24012;
                                }
                            }
                            else
                            {
                                var duoc2 = (from nhapduoc in data.NhapDcts.Where(p => p.SoLuongN > p.SoLuongX)
                                             join nduoc in data.NhapDs.Where(p => p.MaKP == _makhotra) on nhapduoc.IDNhap equals nduoc.IDNhap
                                             select new { nhapduoc.MaDV, nduoc.MaKP }
                           ).ToList();
                                var duoc = (from tenduoc in _ldichvu.Where(o => o.Status == 1)
                                            join nhapduoc in duoc2 on tenduoc.MaDV equals nhapduoc.MaDV
                                            select new { tenduoc.TenDV, tenduoc.MaDV, tenduoc.DonVi, nhapduoc.MaKP, tenduoc.MaTam }
                                            ).OrderBy(p => p.TenDV).ToList().Distinct().ToList();
                                lupMaDuoc.DataSource = duoc;
                            }
                        }
                        else
                            lupMaDuoc.DataSource = _ldichvu.Where(p => p.Status == 1).OrderBy(p => p.TenDV).ToList();
                    }

                }
                else
                {
                    lupMaDuoc.DataSource = _ldichvu.OrderBy(p => p.TenDV).ToList();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("trong danh mục có thuốc có một số thuốc chưa có mã nhà cung cấp");
            }
        }

        private void grvNhap_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _lDichvuTC.Clear();
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int id = 0;
            if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString() != "")
            {
                if (grvNhap.GetFocusedRowCellValue(colIDNhaps) != null && grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString() != "")
                {
                    txtIDNhap.Text = grvNhap.GetFocusedRowCellValue(colIDNhaps).ToString();

                    id = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colIDNhaps));
                }
                else
                {
                    txtIDNhap.Text = "";
                }
                if (grvNhap.GetFocusedRowCellValue(colNgayNhap) != null && grvNhap.GetFocusedRowCellValue(colNgayNhap).ToString() != "")
                    dtNgayNhap.DateTime = Convert.ToDateTime(grvNhap.GetFocusedRowCellValue(colNgayNhap));
                else
                    dtNgayNhap.DateTime = System.DateTime.Now;

                if (grvNhap.GetFocusedRowCellValue(colNgayTT) != null && grvNhap.GetFocusedRowCellValue(colNgayTT).ToString() != "")
                {
                    dtNgayTT.DateTime = Convert.ToDateTime(grvNhap.GetFocusedRowCellValue(colNgayTT));
                    ckNgayTT.Checked = true;
                }
                else
                {
                    ckNgayTT.Checked = false;
                }

                if (grvNhap.GetFocusedRowCellValue(colMaKP) != null)
                    lupMaKP.EditValue = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colMaKP));
                else
                    lupMaKP.EditValue = 0;
                if (grvNhap.GetFocusedRowCellValue(colsphieunew) != null && grvNhap.GetFocusedRowCellValue(colsphieunew) != "")
                    txtsophieunew.Text = grvNhap.GetFocusedRowCellValue(colsphieunew).ToString();
                else
                    txtsophieunew.Text = "";

                if (grvNhap.GetFocusedRowCellValue(colMaCC) != null && grvNhap.GetFocusedRowCellValue(colMaCC).ToString() != "")
                    lupMaCC.EditValue = grvNhap.GetFocusedRowCellValue(colMaCC).ToString();
                else
                    lupMaCC.EditValue = "";
                if (grvNhap.GetFocusedRowCellValue(colGhiChus) != null && grvNhap.GetFocusedRowCellValue(colGhiChus).ToString() != "")
                    txtGhiChu.Text = grvNhap.GetFocusedRowCellValue(colGhiChus).ToString();
                else
                    txtGhiChu.Text = "";
                if (grvNhap.GetFocusedRowCellValue(colSoCT) != null && grvNhap.GetFocusedRowCellValue(colSoCT).ToString() != "")
                    txtSoCT.Text = grvNhap.GetFocusedRowCellValue(colSoCT).ToString();
                else
                    txtSoCT.Text = "";
                if (grvNhap.GetFocusedRowCellValue(colTenNguoiCC) != null && grvNhap.GetFocusedRowCellValue(colTenNguoiCC).ToString() != "")
                    cboNguoiGiao.Text = grvNhap.GetFocusedRowCellValue(colTenNguoiCC).ToString();
                else cboNguoiGiao.Text = "";

                _listND = (from nd in data.NhapDcts.Where(p => p.IDNhap == id)
                           select new NhapDctss { IDNhapct = nd.IDNhapct, IDNhap = nd.IDNhap, MaDV = nd.MaDV, SoLo = nd.SoLo, SoDangKy = nd.SoDangKy, HanDung = nd.HanDung, DonVi = nd.DonVi, DonGiaCT = nd.DonGiaCT, DonGia = nd.DonGia, VAT = nd.VAT, ThanhTienTruocVAT = nd.DonGiaCT * nd.SoLuongN, ThanhTienN = nd.ThanhTienN, DonGiaDY = nd.DonGiaDY, SoLuongN = nd.SoLuongN, MaCC = nd.MaCC, SoLuongDY = nd.SoLuongDY, ThanhTienDY = nd.ThanhTienDY, TyLeCK = nd.TyLeCK }).ToList();

                _lNhapDct = data.NhapDcts.Where(p => p.IDNhap == id).ToList();

                if (grvNhap.GetFocusedRowCellValue(colTrangThai) != null && grvNhap.GetFocusedRowCellValue(colTrangThai).ToString() != "")
                    txtstatus.Text = grvNhap.GetFocusedRowCellValue(colTrangThai).ToString();
                else
                    txtstatus.Text = "";
                if (grvNhap.GetFocusedRowCellValue(colKieuDon) != null && grvNhap.GetFocusedRowCellValue(colKieuDon).ToString() != "")
                    cboNhap.SelectedIndex = Convert.ToInt32(grvNhap.GetFocusedRowCellValue(colKieuDon));
                else
                    cboNhap.SelectedIndex = -1;
                if (_lNhapDct.Count() > 0)
                    cboThueVAT.EditValue = _lNhapDct.First().VAT;
                else
                    cboThueVAT.EditValue = 0;
                if (_lNhapDct.Count() > 0)
                    cboTLchietKhau.EditValue = _lNhapDct.First().TyLeCK;
                else
                    cboTLchietKhau.EditValue = 0;
                if (_lNhapDuoc.Where(p => p.IDNhap == id).ToList().Count > 0)
                    txtDiaChi.Text = _lNhapDuoc.Where(p => p.IDNhap == id).ToList().First().DiaChi;
                else
                    txtDiaChi.Text = "";
                if (_lNhapDuoc.Where(p => p.IDNhap == id).ToList().Count > 0)
                    lupBPTra.EditValue = _lNhapDuoc.Where(p => p.IDNhap == id).ToList().First().MaKPnx;
                else
                    lupBPTra.EditValue = 0;
                if (_lNhapDuoc.Where(p => p.IDNhap == id).ToList().Count > 0)
                    lupHinhthucx.EditValue = _lNhapDuoc.Where(p => p.IDNhap == id).ToList().First().TraDuoc_KieuDon;
                else
                    lupBPTra.EditValue = -1;
                binNhapDuocct.DataSource = _listND.ToList();
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
                _lNhapDct = data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                binNhapDuocct.DataSource = _lNhapDct;
                grcNhapCT.DataSource = binNhapDuocct;
            }
            if(DungChung.Bien.MaBV == "24012")
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
            _lNhapDuoc = (from nd in data.NhapDs.Where(p => p.PLoai == 1)
                          where (nd.NgayNhap >= _dttu && nd.NgayNhap <= _dtden)
                          where (nd.SoCT.Contains(_soPhieu) || (ID == 0 ? true : nd.IDNhap == ID))
                          where (nd.MaKP == (_makho))
                          where (_macc == "" ? true : nd.MaCC.Contains(_macc))
                          select nd).OrderByDescending(p => p.NgayNhap).OrderByDescending(p => p.IDNhap).ToList();
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
            if(DungChung.Bien.MaBV == "24012")
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
                if (DungChung.Bien.MaBV == "30009")
                {
                    colDonGiaCT.OptionsColumn.ReadOnly = false;
                    colVAT.OptionsColumn.ReadOnly = false;
                }
                _suaxuatnoibo = false;
                _ktmatkhau = false;
                bool _cothexoa = true;
                int id = 0;
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                {
                    int idnhap = Convert.ToInt32(txtIDNhap.Text);
                    var kt = data.NhapDs.Where(p => p.IDNhap == idnhap).ToList();

                    if (!string.IsNullOrEmpty(txtstatus.Text))
                        id = Convert.ToInt32(txtstatus.Text);
                    if (kt.Count > 0 && (kt.First().Status == 1 || (kt.First().XuatTD != null && kt.First().XuatTD > 0)))
                    {
                        if (kt.First().XuatTD != null && kt.First().XuatTD > 0)
                            MessageBox.Show("Chứng từ nhập theo chức năng, bạn không thể sửa");
                        else
                            MessageBox.Show("Chứng từ đã bị khóa, bạn không thể sửa");
                        _cothexoa = false;
                    }

                    if (_cothexoa && DungChung.Ham._checkNgayKhoa(data, dtNgayNhap.DateTime, "KhoaDC"))
                    {
                        _cothexoa = false;
                    }

                    if (DungChung.Bien.MaBV == "30372")
                    {
                        var kt1 = (from a in kt
                                   join b in data.NhapDcts on a.IDNhap equals b.IDNhap
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
                            colDonGiaCT.OptionsColumn.ReadOnly = true;
                            colSoLuong.OptionsColumn.ReadOnly = true;
                            colThanhTien.OptionsColumn.ReadOnly = true;
                        }
                        _cothexoa = true;
                    }



                    if (_cothexoa && !DungChung.Ham._KiemTraCBSuaXoa(data, kt.First().MaCB, DungChung.Bien.MaCB))
                    {
                        MessageBox.Show("Tên cán bộ không khớp!");
                        _cothexoa = false;
                    }
                    var ktdung = data.NhapDs.Where(p => p.XuatTD == idnhap).ToList();

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
                        var ktct = data.NhapDs.Where(p => p.IDNhap == XuatTD).ToList();
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
                                    colDonGiaCT.OptionsColumn.ReadOnly = true;
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
                        if ((TTLuu == 1 || TTLuu == 2))
                        {
                            List<DVu> l = _ldichvu.Where(p => (_macc == "" || _macc == "-1") ? true : p.MaCC == (_macc)).OrderBy(p => p.TenDV).ToList();

                            lupMaDuoc.DataSource = l;

                        }
                        else
                        {
                            lupMaDuoc.DataSource = _ldichvu.OrderBy(p => p.TenDV).ToList();
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
        private void grvNhapCT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            List<DungChung.Ham.giaSoLoHSD> dsgia = new List<DungChung.Ham.giaSoLoHSD>();
            List<DungChung.Ham.giaSoLoHSD> dsgiaHT = new List<DungChung.Ham.giaSoLoHSD>(); // lấy ra danh sách giá đã trừ đi số lượng kê hiện tại
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
            switch (e.Column.Name)
            {
                case "colMaDV":
                    #region colMaDV
                    ldongia = new List<DonGiaDV>();
                    //DungChung.Ham.giaSoLoHSD dsgia = new DungChung.Ham.giaSoLoHSD();
                    string soloHT = "";// số lô hiện tại để tính tồn
                    DateTime? handungHT = new DateTime(); // hạn dùng hiện tại để tính tồn
                    if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                        madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                    if (lupMaKP.EditValue != null)
                    {
                        makho = Convert.ToInt32(lupMaKP.EditValue);
                    }
                    grvNhapCT.SetFocusedRowCellValue(colDonVi, DungChung.Ham._getDonVi(data, madv));
                    var dongiadv = _ldichvu.Where(p => p.MaDV == madv).Select(p => p.DonGia).FirstOrDefault();
                    if (dongiadv != null)
                    {
                        grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, dongiadv);
                    }

                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);

                    if (pphhdy == 1)
                    {
                        colSoLuongDY.OptionsColumn.ReadOnly = false;
                        colSoLuong.OptionsColumn.ReadOnly = true;
                    }
                    else
                    {

                        colSoLuongDY.OptionsColumn.ReadOnly = true;
                        colSoLuong.OptionsColumn.ReadOnly = false;
                    }
                    if ((DungChung.Bien.MaBV == "24012" || DungChung.Bien.MaBV == "24389") && cboNhap.Text == "Nhập chuyển kho")
                    {
                        //colSoLo.OptionsColumn.ReadOnly = true;
                        colHanDung.OptionsColumn.ReadOnly = true;
                        //colSoLo.OptionsColumn.AllowEdit = false;
                        colHanDung.OptionsColumn.AllowEdit = false;
                        string _solo = "";
                        DateTime? _handung = null;
                        int makhotra = 0;
                        if (lupBPTra.EditValue != null)
                        {
                            makhotra = Convert.ToInt32(lupBPTra.EditValue);
                        }

                        if (grvNhapCT.GetRowCellValue(e.RowHandle, colSoLo) != null)
                            _solo = grvNhapCT.GetRowCellValue(e.RowHandle, colSoLo).ToString();
                        if (grvNhapCT.GetRowCellValue(e.RowHandle, colHanDung) != null && grvNhapCT.GetRowCellValue(e.RowHandle, colHanDung).ToString() != "")
                            _handung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(e.RowHandle, colHanDung).ToString());
                        dsgia = DungChung.Ham._getDSGia(data, madv, makhotra);
                        List<DungChung.Ham.giaSoLoHSD> tongSL = new List<DungChung.Ham.giaSoLoHSD>();

                        cboDonGiaCT.Items.Clear();

                        double _gia = 0;
                        foreach (var g in dsgia.OrderBy(p => p.Gia))
                        {
                            if (g.Gia != null && g.Gia.ToString() != "")
                            {
                                if (g.SoLuong > 0)
                                {
                                    if (_gia != g.Gia)
                                    {
                                        cboDonGiaCT.Items.Add(g.Gia);
                                        _gia = g.Gia;
                                    }
                                }
                            }
                        }

                        double SLKe = 0;//sl kê hiện tại

                        for (int i = 0; i <= grvNhapCT.RowCount; i++)
                        {

                            if (i != e.RowHandle && grvNhapCT.GetRowCellValue(i, colMaDV) != null && grvNhapCT.GetRowCellValue(i, colSoLuong) != null && grvNhapCT.GetRowCellValue(i, colMaDV).ToString() != "" && grvNhapCT.GetRowCellValue(i, colSoLuong).ToString() != "")
                            {
                                if (Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colMaDV)) == madv)
                                {
                                    DungChung.Ham.giaSoLoHSD moi = new DungChung.Ham.giaSoLoHSD();
                                    moi.SoLuong = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuong));
                                    if (grvNhapCT.GetRowCellValue(i, colDonGia) != null && grvNhapCT.GetRowCellValue(i, colDonGia).ToString() != "")
                                        moi.Gia = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colDonGia));
                                    if (grvNhapCT.GetRowCellValue(i, colSoLo) != null)
                                        moi.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                    if (grvNhapCT.GetRowCellValue(i, colHanDung) != null && grvNhapCT.GetRowCellValue(i, colHanDung).ToString() != "")
                                        moi.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                    tongSL.Add(moi);
                                }
                            }
                        }
                        if (grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong) != null && grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong).ToString() != "")
                            SLKe = Convert.ToDouble(grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong));
                        foreach (var dct in _listGiaSua)
                        {
                            if (dct.MaDV > 0 && dct.MaDV == madv)
                                dsgia.Insert(0, new DungChung.Ham.giaSoLoHSD { Gia = dct.Gia, SoLuong = dct.SoLuong, SoLo = dct.SoLo, HanDung = dct.HanDung });
                        }

                        dsgiaHT = LayDSGiaHT(tongSL, dsgia, SLKe, 0);
                        dsgiaHT = dsgiaHT.Where(p => p.SoLuong > 0).ToList();
                        if (dsgiaHT.Count > 0 && dsgiaHT.First().SoLuong > 0)
                        {
                            grvNhapCT.SetRowCellValue(e.RowHandle, colSoLo, dsgiaHT.First().SoLo);
                            grvNhapCT.SetRowCellValue(e.RowHandle, colHanDung, dsgiaHT.First().HanDung);
                            grvNhapCT.SetRowCellValue(e.RowHandle, colDonGia, dsgiaHT.First().Gia);
                            grvNhapCT.SetRowCellValue(e.RowHandle, colSoLuong, 0);

                        }
                    }

                    break;
                #endregion
                case "colThanhTien":
                    #region colThanhTien
                    if (grvNhapCT.GetFocusedRowCellValue(colThanhTien) != null && grvNhapCT.GetFocusedRowCellValue(colThanhTien).ToString() != "")
                    {
                        double TTTong = 0, _dongiavat = 0;
                        TTTong = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colThanhTien));
                        double _dongia = 0, _soluongn = 0;
                        if (grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null)
                            _dongia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                        if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null)
                            _soluongn = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                        if (_dongia <= 0 && _soluongn > 0 && TTTong > 0)
                        {
                            double vatn = 0, tlck = 0;
                            if (grvNhapCT.GetFocusedRowCellValue(colVAT) != null && grvNhapCT.GetFocusedRowCellValue(colVAT).ToString() != "")
                                vatn = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colVAT));
                            if (grvNhapCT.GetFocusedRowCellValue(colTyLeCK) != null && grvNhapCT.GetFocusedRowCellValue(colTyLeCK).ToString() != "")
                                tlck = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colTyLeCK));
                            _dongiavat = TTTong / _soluongn;
                            double tl = (vatn + tlck) / 100;
                            _dongia = _dongiavat / (tl + 1);
                            grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, Math.Round(_dongia, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                        }
                    }
                    break;
                    #endregion
                case "colDonGiaCT":
                    #region colDonGiaCT
                    // kiểm tra khi sửa 
                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null)
                        _soluongluu = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                    else
                        _soluongluu = 0;

                    if (TTLuu == 2)
                    {
                        if (e.Column.Name == "colDonGiaCT")
                        {
                            double _dongia_gv = 0;
                            double dgSauThue = 0, soluongton = 0;
                            double vat = 0;
                            double TyLechietkhau = 0;
                            TyLechietkhau = Convert.ToDouble(cboTLchietKhau.Text);
                            if (grvNhapCT.GetFocusedRowCellValue(colVAT) != null)
                                vat = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colVAT));
                            int _madv = 0;
                            if (e.Value != null)
                                _dongia_gv = Convert.ToDouble(e.Value);
                            vat = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colVAT));
                            grvNhapCT.SetFocusedRowCellValue(colVAT, vat);
                            dgSauThue = ldongia.Where(p => p.DonGiaCT == _dongia_gv).Select(p => p.DonGiaN).FirstOrDefault();

                            grvNhapCT.SetFocusedRowCellValue(colDonGia, dgSauThue);
                            if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                                _madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                            soluongton = DungChung.Ham._checkTon(data, _madv, lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue), dgSauThue, 0, "");
                        }

                    }
                    double sl = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT).ToString()); 
                    var _solo2 = (from nhapduoc in data.NhapDcts.Where(p => p.MaDV == (madv)).Where(p => p.DonGia == sl)
                                 join nduoc in data.NhapDs.Where(p => p.MaKP == (DungChung.Bien.MaBV == "24012" ? makp : _makhotra)) on nhapduoc.IDNhap equals nduoc.IDNhap
                                 group new { nhapduoc } by new { nhapduoc.SoLo } into kq
                                 select new { kq.Key.SoLo, soluong = (kq.Sum(p => p.nhapduoc.SoLuongN) - kq.Sum(p => p.nhapduoc.SoLuongX)) }).ToList();

                    cboSoLo.Items.Clear();
                    if (_solo2.Count > 0)
                    {
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            if(_solo2.Where(p => p.soluong > 0).ToList().Count() > 0)
                            {
                                if (_solo2.Where(p => p.soluong > 0).First().SoLo != null)
                                {
                                    grvNhapCT.SetFocusedRowCellValue(colSoLo, _solo2.Where(p => p.soluong > 0).First().SoLo);
                                }
                                else
                                {
                                    //Message or sth
                                }
                            }
                        }
                        else if (_solo2.First().SoLo != null)
                            grvNhapCT.SetFocusedRowCellValue(colSoLo, _solo2.First().SoLo);
                    }
                    foreach (var g in _solo2)
                    {
                        if (g.SoLo != null && g.SoLo.ToString() != "")
                        {
                            if(g.soluong > 0)
                            {
                                cboSoLo.Items.Add(g.SoLo);
                            }
                        }
                    }
                    if(DungChung.Bien.MaBV == "24012" && cboNhap.SelectedIndex == 0 || cboNhap.SelectedIndex == 2)
                    {
                        this.cboSoLo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                    }
                    // kt
                    double dg = 0;
                    if (grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null && grvNhapCT.GetFocusedRowCellValue(colDonGiaCT).ToString() != "")
                    {
                        if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                            madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                        var ktdy = data.DichVus.Where(p => p.MaDV == madv).Where(p => p.DongY == 1).ToList();

                        if (ktdy.Count > 0)
                            _thuocyhdt = true;
                        if (_thuocyhdt)
                        {

                            if (ktdy.First().TyLeSP != null)
                                _tylePP = ktdy.First().TyLeSP.Value;
                            if (ktdy.First().TyLeBQ != null)
                                _tyleBQ = ktdy.First().TyLeBQ.Value;
                            if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                                _soluong = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                            int vat = 0;
                            int TyLechietkhau = 0;
                            TyLechietkhau = Convert.ToInt32(cboTLchietKhau.Text);
                            dg = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                            if (dg >= 0)
                            {
                                if (grvNhapCT.GetFocusedRowCellValue(colVAT) != null && grvNhapCT.GetFocusedRowCellValue(colVAT).ToString() != "")
                                    vat = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colVAT));
                                double dongia = 0, dongianhapdy = 0;
                                if (nhathuoc)
                                {
                                    dongia = ((dg * (100 + vat) / 100) * (100 + TyLechietkhau)) / 100;
                                }
                                else
                                    dongia = (dg * (100 + vat) / 100);
                                _dgvat = dongia;
                                dongianhapdy = dongia;
                                dongia = Math.Round(dongia, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                                if (DungChung.Bien.MaBV != "20001" || (DungChung.Bien.MaBV == "20001" && _huhaoGia == true))
                                {
                                    if ((DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30007") || DungChung.Bien.MaBV == "14017") // thêm giá hư hao vào giá đông y a.quý y/c đức 2302 
                                        dongianhapdy = (dongianhapdy) * ((100 + _tylePP + _tyleBQ) / 100);
                                    else
                                        dongianhapdy = (/*100 **/ dongianhapdy) /*/ (100 - _tylePP - _tyleBQ)*/;
                                }
                                dongianhapdy = Math.Round(dongianhapdy, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                                grvNhapCT.SetFocusedRowCellValue(colGiaNhapDY, ((((DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "12001") || DungChung.Bien.MaBV == "20001") || DungChung.Bien.MaBV == "14017")) ? dongia : dongianhapdy);
                                grvNhapCT.SetFocusedRowCellValue(colDonGia, dongianhapdy);
                                grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(dongianhapdy * _soluong, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                            }
                            else
                            {
                                MessageBox.Show("Đơn giá phải > 0!");
                                grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, 0);

                            }
                        }
                        else
                        {
                            if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                                _soluong = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                            int vat = 0;
                            int TyLechietkhau = 0;
                            if (grvNhapCT.GetFocusedRowCellValue(colTyLeCK) != null && grvNhapCT.GetFocusedRowCellValue(colTyLeCK).ToString() != "")
                                TyLechietkhau = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colTyLeCK));
                            else
                            {
                                MessageBox.Show("Bạn chưa nhập tỷ lệ chiết khấu!", "Thông báo!");
                                cboTLchietKhau.Focus();
                            }
                            dg = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                            if (dg >= 0)
                            {
                                if (grvNhapCT.GetFocusedRowCellValue(colVAT) != null && grvNhapCT.GetFocusedRowCellValue(colVAT).ToString() != "")
                                    vat = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colVAT));
                                double dongia = 0;
                                if (nhathuoc)
                                {
                                    dongia = ((dg * (100 + vat) / 100) * (100 + TyLechietkhau)) / 100;
                                }
                                else
                                    dongia = (dg * (100 + vat) / 100);
                                _dgvat = dongia;
                                dongia = Math.Round(dongia, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                                grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(_dgvat * _soluong, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                grvNhapCT.SetFocusedRowCellValue(colDonGia, dongia);
                                grvNhapCT.SetFocusedRowCellValue(colGiaNhapDY, dongia);
                            }
                            else
                            {
                                MessageBox.Show("Đơn giá phải > 0!");
                                grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, 0);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa nhập đơn giá");
                    }
                    break;
                #endregion
                case "colSoLuong":
                    #region
                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                    {
                        int _madv = 0;
                        _soluong = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                        if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                            _madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                        data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                        int _tylesd = 1;
                        var dv = data.DichVus.Where(p => p.MaDV == _madv).FirstOrDefault();
                        if (dv != null)
                        {
                            _tylesd = dv.TyLeSD;

                        }
                        if (TTLuu == 2)
                        {
                            int idnhapct = 0;
                            if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null)
                                idnhapct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));

                            int _row = 0;
                            _row = e.RowHandle;

                            double _dg = 0, _dgct = 0; ; double soluongton = 0; string _solo = "";

                            var soluongluu = data.NhapDcts.Where(p => p.IDNhapct == idnhapct).ToList();
                            if (soluongluu.Count > 0)
                                _soluongluu = soluongluu.First().SoLuongN;

                            if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null)
                                _dg = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                            if (grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null)
                                _dgct = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                            if (grvNhapCT.GetFocusedRowCellValue(colSoLo) != null)
                                _solo = Convert.ToString(grvNhapCT.GetFocusedRowCellValue(colSoLo));
                            if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                            {
                                double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                double c = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY));
                                if (pphhdy == 0)
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuongDY, Math.Round(_soluong, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(_soluong * b, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                grvNhapCT.SetFocusedRowCellValue(colThanhTienDY, Math.Round(_soluong * c, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                            }
                            soluongton = DungChung.Ham._checkTon(data, _madv, lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue), _dg, _soluongluu, _solo);

                            if (_soluong < 0)
                            {
                                MessageBox.Show("Số lượng phải > 0!");
                                grvNhapCT.FocusedColumn = grvNhapCT.VisibleColumns[3];
                                grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                            }
                            if (_soluong < _soluongluu)
                            {
                                if (soluongton - (_soluongluu - _soluong) < 0)
                                {
                                    MessageBox.Show("Số lượng sửa không hợp lệ!");
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, _soluongluu);
                                }
                            }
                            if (DungChung.Bien.MaBV == "24012")
                            {

                                double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString());

                                var ktdy = data.DichVus.Where(p => p.MaDV == madv).Where(p => p.DongY == 1).ToList();
                                if (ktdy.Count > 0)
                                    _thuocyhdt = true;
                                if (_thuocyhdt)
                                {
                                    if (ktdy.First().TyLeSP != null)
                                        _tylePP = ktdy.First().TyLeSP.Value;
                                    if (ktdy.First().TyLeBQ != null)
                                        _tyleBQ = ktdy.First().TyLeBQ.Value;
                                    double _soluongdy = 0;
                                    if (pphhdy == 1 && (DungChung.Bien.MaBV != "20001" && DungChung.Bien.MaBV != "14017"))
                                    {

                                        _soluongdy = (_soluong * 100) / (100 - _tyleBQ - _tylePP); // số lượng đông y hao hụt
                                    }
                                    else
                                    {
                                        _soluongdy = _soluong;
                                    }
                                    // a thay đổi theo tỷ lệ số lượng
                                    if (a >= 0)
                                    {
                                        if (a > DungChung.Bien.SoLuongTon && DungChung.Bien.MaBV == "24012" && cboNhap.Text == "Nhập chuyển kho")
                                        {
                                            MessageBox.Show("Số lượng nhập lớn hơn số lượng tồn!");
                                            grvNhapCT.FocusedColumn = grvNhapCT.VisibleColumns[3];
                                            grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                            return;
                                        }
                                        if (grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY) != null && grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY).ToString() != "")
                                        {
                                            double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                            grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(a * b, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                            double c = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY));
                                            grvNhapCT.SetFocusedRowCellValue(colThanhTienDY, Math.Round(_soluongdy * c, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Số lượng phải > 0!");
                                        grvNhapCT.FocusedColumn = grvNhapCT.VisibleColumns[3];
                                        grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);

                                    }
                                }
                                else
                                {
                                    if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                    {
                                        double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(a * b, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTienDY, Math.Round(a * b, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                    }
                                }
                                double SLKe = 0;
                                int makhotra = 0;
                                if (lupBPTra.EditValue != null)
                                    makhotra = Convert.ToInt32(lupBPTra.EditValue);
                                if (grvNhapCT.GetFocusedRowCellValue(colSoLo) != null)
                                    soloHT = grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString();
                                if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                                {
                                    string madv1 = grvNhapCT.GetFocusedRowCellValue(colMaDV).ToString();
                                }
                                dsgia = DungChung.Ham._getDSGia(data, madv, makhotra);
                                // lấy tổng số lượng thuốc đã kê
                                List<DungChung.Ham.giaSoLoHSD> tongSL = new List<DungChung.Ham.giaSoLoHSD>();
                                for (int i = 0; i <= grvNhapCT.RowCount; i++)
                                {
                                    if (grvNhapCT.GetRowCellValue(i, colMaDV) != null && grvNhapCT.GetRowCellValue(i, colSoLuong) != null)
                                    {
                                        if (Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colMaDV)) == madv)
                                        {
                                            DungChung.Ham.giaSoLoHSD moi = new DungChung.Ham.giaSoLoHSD();
                                            moi.SoLuong = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuong));
                                            if (grvNhapCT.GetRowCellValue(i, colDonGia) != null)
                                                moi.Gia = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colDonGia));
                                            if (grvNhapCT.GetRowCellValue(i, colSoLo) != null)
                                                moi.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                            if (grvNhapCT.GetRowCellValue(i, colHanDung) != null)
                                                moi.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                            tongSL.Add(moi);
                                        }
                                    }
                                }

                                if (grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong) != null && grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong).ToString() != "")
                                    SLKe = Convert.ToDouble(grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong));

                                if (ppxuat == 3)
                                {
                                    double b = grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null ? (double)grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) : 0;
                                    string c = grvNhapCT.GetFocusedRowCellValue(colSoLo) != null ? (string)grvNhapCT.GetFocusedRowCellValue(colSoLo) : "";

                                    double slke = tongSL.Where(p => p.SoLo == c).Sum(p => p.SoLuong);
                                    double ton = DungChung.Ham._checkTon_KD(data, madv, makhotra, b, 0, c);
                                    if (ton < 0)
                                    {
                                        ton = 0;
                                    }
                                    int d = e.RowHandle;
                                    if (d < 0)
                                    {
                                        DungChung.Bien.SoLuongTon = ton - SLKe - slke;
                                    }
                                    else
                                    {

                                        DungChung.Bien.SoLuongTon = ton - slke;
                                    }
                                }
                                if (DungChung.Bien.MaBV == "24012" && cboNhap.SelectedIndex != 0 && cboNhap.SelectedIndex != 2)
                                    groupControl3.Text = "Chi tiết chứng từ";
                                else
                                    groupControl3.Text = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();

                                if (DungChung.Bien.SoLuongTon < 0 && DungChung.Bien.MaBV == "24012" && cboNhap.Text == "Nhập chuyển kho")
                                {
                                    MessageBox.Show("Số lượng nhập lớn hơn số lượng tồn!");
                                    grvNhapCT.FocusedColumn = grvNhapCT.VisibleColumns[3];
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString());
                            if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                                madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                            var ktdy = data.DichVus.Where(p => p.MaDV == madv).Where(p => p.DongY == 1).ToList();
                            if (ktdy.Count > 0)
                                _thuocyhdt = true;
                            if (_thuocyhdt)
                            {
                                if (ktdy.First().TyLeSP != null)
                                    _tylePP = ktdy.First().TyLeSP.Value;
                                if (ktdy.First().TyLeBQ != null)
                                    _tyleBQ = ktdy.First().TyLeBQ.Value;
                                double _soluongdy = 0;
                                if (pphhdy == 1 && (DungChung.Bien.MaBV != "20001" && DungChung.Bien.MaBV != "14017"))
                                {

                                    _soluongdy = (_soluong * 100) / (100 - _tyleBQ - _tylePP); // số lượng đông y hao hụt
                                }
                                else
                                {
                                    _soluongdy = _soluong;
                                }
                                // a thay đổi theo tỷ lệ số lượng
                                if (a >= 0)
                                {
                                    if (a > DungChung.Bien.SoLuongTon && DungChung.Bien.MaBV == "24012" && cboNhap.Text == "Nhập chuyển kho")
                                    {
                                        MessageBox.Show("Số lượng nhập lớn hơn số lượng tồn!");
                                        grvNhapCT.FocusedColumn = grvNhapCT.VisibleColumns[3];
                                        grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                        return;
                                    }
                                    if (grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY) != null && grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY).ToString() != "")
                                    {
                                        if (pphhdy == 0)
                                            grvNhapCT.SetFocusedRowCellValue(colSoLuongDY, Math.Round(a, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                        double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(a * b, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                        double c = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY));
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTienDY, Math.Round(_soluongdy * c, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Số lượng phải > 0!");
                                    grvNhapCT.FocusedColumn = grvNhapCT.VisibleColumns[3];
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);

                                }
                            }
                            else
                            {
                                if (a >= 0)
                                {
                                    double SLKe = 0;
                                    int makhotra = 0;
                                    if (lupBPTra.EditValue != null)
                                        makhotra = Convert.ToInt32(lupBPTra.EditValue);
                                    if (grvNhapCT.GetFocusedRowCellValue(colSoLo) != null)
                                        soloHT = grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString();
                                    if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                                    {
                                        string madv1 = grvNhapCT.GetFocusedRowCellValue(colMaDV).ToString();
                                    }
                                    dsgia = DungChung.Ham._getDSGia(data, madv, makhotra);
                                    // lấy tổng số lượng thuốc đã kê
                                    List<DungChung.Ham.giaSoLoHSD> tongSL = new List<DungChung.Ham.giaSoLoHSD>();
                                    for (int i = 0; i <= grvNhapCT.RowCount; i++)
                                    {
                                        if (grvNhapCT.GetRowCellValue(i, colMaDV) != null && grvNhapCT.GetRowCellValue(i, colSoLuong) != null)
                                        {
                                            if (Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colMaDV)) == madv)
                                            {
                                                DungChung.Ham.giaSoLoHSD moi = new DungChung.Ham.giaSoLoHSD();
                                                moi.SoLuong = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colSoLuong));
                                                if (grvNhapCT.GetRowCellValue(i, colDonGia) != null)
                                                    moi.Gia = Convert.ToDouble(grvNhapCT.GetRowCellValue(i, colDonGia));
                                                if (grvNhapCT.GetRowCellValue(i, colSoLo) != null)
                                                    moi.SoLo = grvNhapCT.GetRowCellValue(i, colSoLo).ToString();
                                                if (grvNhapCT.GetRowCellValue(i, colHanDung) != null)
                                                    moi.HanDung = Convert.ToDateTime(grvNhapCT.GetRowCellValue(i, colHanDung));
                                                tongSL.Add(moi);
                                            }
                                        }
                                    }

                                    if (grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong) != null && grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong).ToString() != "")
                                        SLKe = Convert.ToDouble(grvNhapCT.GetRowCellValue(e.RowHandle, colSoLuong));

                                    foreach (var dct in _listGiaSua)
                                    {
                                        if (dct.MaDV > 0 && dct.MaDV == madv)
                                            dsgia.Insert(0, new DungChung.Ham.giaSoLoHSD { Gia = dct.Gia, SoLuong = dct.SoLuong, SoLo = dct.SoLo, HanDung = dct.HanDung });
                                    }

                                    //dsgiaHT = LayDSGiaHT(tongSL, dsgia, SLKe, 0);
                                    if (ppxuat == 3)
                                    {
                                        double b = grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null ? (double)grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) : 0;
                                        string c = grvNhapCT.GetFocusedRowCellValue(colSoLo) != null ? (string)grvNhapCT.GetFocusedRowCellValue(colSoLo) : "";

                                        double slke = tongSL.Where(p => p.SoLo == c).Sum(p => p.SoLuong);
                                        double ton = DungChung.Ham._checkTon_KD(data, madv, makhotra, b, 0, c);
                                        if (ton < 0)
                                        {
                                            ton = 0;
                                        }
                                        int d = e.RowHandle;
                                        if (d < 0)
                                        {
                                            DungChung.Bien.SoLuongTon = ton - SLKe - slke;
                                        }
                                        else
                                        {

                                            DungChung.Bien.SoLuongTon = ton - slke;
                                        }
                                    }
                                    if (DungChung.Bien.MaBV == "24012" && cboNhap.SelectedIndex != 0 && cboNhap.SelectedIndex != 2)
                                        groupControl3.Text = "Chi tiết chứng từ";
                                    else
                                        groupControl3.Text = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();

                                    if (DungChung.Bien.SoLuongTon < 0 && DungChung.Bien.MaBV == "24012" && cboNhap.Text == "Nhập chuyển kho")
                                    {
                                        MessageBox.Show("Số lượng nhập lớn hơn số lượng tồn!");
                                        grvNhapCT.FocusedColumn = grvNhapCT.VisibleColumns[3];
                                        grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                        return;
                                    }
                                    if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                                    {
                                        double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                        if (pphhdy == 0)
                                            grvNhapCT.SetFocusedRowCellValue(colSoLuongDY, Math.Round(a, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(a * b, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTienDY, Math.Round(a * b, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Số lượng phải > 0!");
                                    grvNhapCT.FocusedColumn = grvNhapCT.VisibleColumns[3];
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);

                                }
                            }
                        }
                    }
                    break;
                #endregion
                case "colSoLuongDY":
                    #region colSoLuongDY
                    if (pphhdy == 1)
                    {
                        double _sldy = 0, soluongsd = 0;
                        if (grvNhapCT.GetFocusedRowCellValue(colSoLuongDY) != null)
                            _sldy = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuongDY));
                        var ktdy2 = data.DichVus.Where(p => p.MaDV == madv).Where(p => p.DongY == 1).ToList();
                        if (ktdy2.Count > 0)
                            _thuocyhdt = true;
                        if (_thuocyhdt)
                        {
                            if (ktdy2.First().TyLeSP != null)
                                _tylePP = ktdy2.First().TyLeSP.Value;
                            if (ktdy2.First().TyLeBQ != null)
                                _tyleBQ = ktdy2.First().TyLeBQ.Value;
                            if (pphhdy == 1 && (DungChung.Bien.MaBV != "20001" && DungChung.Bien.MaBV != "14017"))
                                soluongsd = _sldy * (100 - _tyleBQ - _tylePP) / 100;
                            else
                                soluongsd = _sldy;
                        }
                        else
                            soluongsd = _sldy;
                        grvNhapCT.SetFocusedRowCellValue(colSoLuong, Math.Round(soluongsd, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                    }
                    break;
                #endregion
                case "colVAT":

                    #region colVAT
                    // kiểm tra khi sửa 
                    //if (DungChung.Bien.MaBV != "27183")
                    //{
                        if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null)
                            _soluongluu = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                        else
                            _soluongluu = 0;
                        if (DungChung.Bien.MaBV != "30340")
                        {
                            if (TTLuu == 2)
                            {

                                if (e.Column.Name == "colVAT")
                                {
                                    double _dongia_gv = 0, _vat_gv = 0;
                                    if (grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null)
                                        _dongia_gv = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                                    if (grvNhapCT.GetFocusedRowCellValue(colVAT) != null)
                                        _vat_gv = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colVAT));
                                    int _row = 0;
                                    _row = e.RowHandle;
                                    int _madv = 0;
                                    double _sl = 0, _dg = 0, _dgct = 0, _vat = 0, _TyLeCK = 0; double soluongton = 0, _soluong2 = 0;

                                    int idnhapct = 0;
                                    if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null)
                                        idnhapct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));

                                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                    var soluongluu = data.NhapDcts.Where(p => p.IDNhapct == idnhapct).ToList();

                                    if (soluongluu.Count > 0)
                                    {
                                        _dgct = soluongluu.First().DonGiaCT;
                                        _dg = soluongluu.First().DonGia;
                                        _sl = soluongluu.First().SoLuongN;
                                        _vat = soluongluu.First().VAT;
                                        _TyLeCK = soluongluu.First().TyLeCK;
                                    }
                                    if (_vat_gv != _vat)
                                    {
                                        if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                                            _madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                                        soluongton = DungChung.Ham._checkTon(data, _madv, lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue), _dg, _sl, "");

                                        if (soluongluu.Count > 0)
                                            _soluong2 = soluongluu.First().SoLuongN;
                                        if (soluongton < _soluong2 && _soluong2 > 0)
                                        {
                                            MessageBox.Show("thuốc đã được sử dụng, bạn không thể sửa!");
                                            grvNhapCT.SetFocusedRowCellValue(colVAT, _vat);
                                            break;
                                        }
                                    }
                                }

                            }
                            // kt
                            int vat1 = 0;
                            double dg1 = 0;
                            if (grvNhapCT.GetFocusedRowCellValue(colVAT) != null && grvNhapCT.GetFocusedRowCellValue(colVAT).ToString() != "")
                            {
                                int TyLeCK = 0;
                                if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                                    madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                                if (grvNhapCT.GetFocusedRowCellValue(colTyLeCK) != null && grvNhapCT.GetFocusedRowCellValue(colTyLeCK).ToString() != "")
                                    TyLeCK = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colTyLeCK));
                                var ktdy = data.DichVus.Where(p => p.MaDV == madv).Where(p => p.DongY == 1).ToList();
                                if (ktdy.Count > 0)
                                    _thuocyhdt = true;

                                if (_thuocyhdt)
                                {

                                    if (ktdy.First().TyLeSP != null)
                                        _tylePP = ktdy.First().TyLeSP.Value;
                                    if (ktdy.First().TyLeBQ != null)
                                        _tyleBQ = ktdy.First().TyLeBQ.Value;
                                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                                        _soluong = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));

                                    int vat = 0;

                                    int TyLechietkhau = 0;
                                    TyLechietkhau = Convert.ToInt32(cboTLchietKhau.Text);

                                    dg = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                                    if (dg >= 0)
                                    {
                                        if (grvNhapCT.GetFocusedRowCellValue(colVAT) != null && grvNhapCT.GetFocusedRowCellValue(colVAT).ToString() != "")
                                            vat = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colVAT));
                                        double dongia = 0, dongianhapdy = 0;
                                        if (nhathuoc)
                                        {
                                            dongia = ((dg * (100 + vat) / 100) * (100 + TyLechietkhau)) / 100;
                                        }
                                        else
                                            dongia = (dg * (100 + vat) / 100);
                                        _dgvat = dongia;
                                        dongianhapdy = dongia;
                                        dongia = Math.Round(dongia, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                                        if (DungChung.Bien.MaBV != "20001" || (DungChung.Bien.MaBV == "20001" && _huhaoGia == true))
                                        {
                                            if ((DungChung.Bien.MaBV == "20001" || DungChung.Bien.MaBV == "30007") || DungChung.Bien.MaBV == "14017") // thêm giá hư hao vào giá đông y a.quý y/c đức 2302 
                                                dongianhapdy = (dongianhapdy) * ((100 + _tylePP + _tyleBQ) / 100);
                                            else
                                                dongianhapdy = (/*100 * */dongianhapdy) /*/ (100 - _tylePP - _tyleBQ)*/;
                                        }
                                        dongianhapdy = Math.Round(dongianhapdy, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                                        grvNhapCT.SetFocusedRowCellValue(colDonGia, dongianhapdy);
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(dongianhapdy * _soluong, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                        grvNhapCT.SetFocusedRowCellValue(colGiaNhapDY, (((DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "12001") || DungChung.Bien.MaBV == "20001") || DungChung.Bien.MaBV == "14017") ? dongia : dongianhapdy);// DungChung.Bien.MaBV == "30007" ? dongianhapdy :// bỏ c.liễu y/c 07/03

                                    }
                                    else
                                    {
                                        MessageBox.Show("Đơn giá phải > 0!");
                                        grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, 0);
                                    }
                                }
                                else
                                {
                                    vat1 = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colVAT));
                                    if (grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null && grvNhapCT.GetFocusedRowCellValue(colDonGiaCT).ToString() != "")
                                        dg1 = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                                    double dongia = 0;
                                    dongia = ((dg1 * (100 + vat1) / 100) * (100 + TyLeCK)) / 100;
                                    dongia = Math.Round(dongia, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                                    grvNhapCT.SetFocusedRowCellValue(colDonGia, dongia);
                                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                                    {
                                        double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString());
                                        grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(a * dongia, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                    }
                                }

                            }
                        }
                    //}
                    break;
                #endregion
                case "colTyLeCK":
                    #region Tỷ lệ chiết khấu

                    if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null)
                        _soluongluu = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                    else
                        _soluongluu = 0;
                    if (DungChung.Bien.MaBV != "30340")
                    {
                        if (TTLuu == 2)
                        {
                            if (e.Column.Name == "colTyLeCK")
                            {
                                double _dongia_gv = 0, _TyLeCK_gv = 0;
                                if (grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null)
                                    _dongia_gv = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                                if (grvNhapCT.GetFocusedRowCellValue(colTyLeCK) != null)
                                    _TyLeCK_gv = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colTyLeCK));
                                int _row = 0;
                                _row = e.RowHandle;
                                int _madv = 0;
                                double _sl = 0, _dg = 0, _dgct = 0, _TyLeCK = 0; double soluongton = 0, _soluong2 = 0;

                                int idnhapct = 0;
                                if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null)
                                    idnhapct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));

                                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                var soluongluu = data.NhapDcts.Where(p => p.IDNhapct == idnhapct).ToList();

                                if (soluongluu.Count > 0)
                                {
                                    _dgct = soluongluu.First().DonGiaCT;
                                    _dg = soluongluu.First().DonGia;
                                    _sl = soluongluu.First().SoLuongN;
                                    _TyLeCK = soluongluu.First().TyLeCK;
                                }
                                if (_TyLeCK_gv != _TyLeCK)
                                {
                                    if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                                        _madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                                    soluongton = DungChung.Ham._checkTon(data, _madv, lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue), _dg, _sl, "");

                                    if (soluongluu.Count > 0)
                                        _soluong2 = soluongluu.First().SoLuongN;
                                    if (soluongton < _soluong2 && _soluong2 > 0)
                                    {
                                        MessageBox.Show("thuốc đã được sử dụng, bạn không thể sửa!");
                                        grvNhapCT.SetFocusedRowCellValue(colTyLeCK, _TyLeCK);
                                        break;
                                    }
                                }
                            }

                        }
                        // kt
                        int TyLeCK1 = 0;
                        double dg1 = 0;
                        if (grvNhapCT.GetFocusedRowCellValue(colTyLeCK) != null && grvNhapCT.GetFocusedRowCellValue(colTyLeCK).ToString() != "")
                        {
                            if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                                madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                            var ktdy = data.DichVus.Where(p => p.MaDV == madv).Where(p => p.DongY == 1).ToList();
                            if (ktdy.Count > 0)
                                _thuocyhdt = true;
                            if (_thuocyhdt)
                            {
                                if (ktdy.First().TyLeSP != null)
                                    _tylePP = ktdy.First().TyLeSP.Value;
                                if (ktdy.First().TyLeBQ != null)
                                    _tyleBQ = ktdy.First().TyLeBQ.Value;
                                if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                                    _soluong = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                                int vat = 0, TyLeCK = 0;
                                dg = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                                vat = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colVAT));
                                if (dg >= 0)
                                {
                                    if (grvNhapCT.GetFocusedRowCellValue(colTyLeCK) != null && grvNhapCT.GetFocusedRowCellValue(colTyLeCK).ToString() != "")
                                        TyLeCK = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colTyLeCK));
                                    double dongia = 0, dongianhapdy = 0;
                                    dongia = ((dg * (100 + vat) / 100) * (100 + TyLeCK)) / 100;
                                    _dgvat = dongia;
                                    dongianhapdy = dongia;
                                    dongia = Math.Round(dongia, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                                    grvNhapCT.SetFocusedRowCellValue(colGiaNhapDY, dongia);
                                    dongianhapdy = (100 * dongianhapdy) / (100 - _tylePP - _tyleBQ);
                                    dongianhapdy = Math.Round(dongianhapdy, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                                    grvNhapCT.SetFocusedRowCellValue(colDonGia, dongianhapdy);
                                    grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(_dgvat * _soluong, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                }
                                else
                                {
                                    MessageBox.Show("Đơn giá phải > 0!");
                                    grvNhapCT.SetFocusedRowCellValue(colDonGiaCT, 0);
                                }
                            }
                            else
                            {
                                int vat = 0;
                                vat = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colVAT));
                                TyLeCK1 = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colTyLeCK));
                                if (grvNhapCT.GetFocusedRowCellValue(colDonGiaCT) != null && grvNhapCT.GetFocusedRowCellValue(colDonGiaCT).ToString() != "")
                                    dg1 = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT));
                                double dongia = 0;
                                dongia = ((dg1 * (100 + vat) / 100) * (100 + TyLeCK1)) / 100;
                                dongia = Math.Round(dongia, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero);
                                grvNhapCT.SetFocusedRowCellValue(colDonGia, dongia);
                                if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                                {
                                    double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString());
                                    grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(a * dongia, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case "colDonGia":
                    #region colDonGia
                    double dongiaSauThue = 0;
                    string _solo1 = "";
                    if (ppxuat == 3)
                    {
                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null)
                        {
                            madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                        }
                        grvNhapCT.SetFocusedRowCellValue(colSoLo, null);
                        if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null)
                        {
                            dongiaSauThue = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));

                            double b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGiaCT).ToString());
                            var solo1 = (from nhapduoc in data.NhapDcts.Where(p => p.MaDV == (madv)).Where(p => p.DonGia == b)
                                         join nduoc in data.NhapDs.Where(p => p.MaKP == (DungChung.Bien.MaBV == "24012" ? makp : (_makhotra))) on nhapduoc.IDNhap equals nduoc.IDNhap
                                         group new { nhapduoc } by new { nhapduoc.SoLo } into kq
                                         select new { kq.Key.SoLo, soluong = (kq.Sum(p => p.nhapduoc.SoLuongN) - kq.Sum(p => p.nhapduoc.SoLuongX)) }).ToList();

                            cboSoLo.Items.Clear();
                            if (solo1.Count > 0)
                            {
                                if (DungChung.Bien.MaBV == "24012")
                                {
                                    if (solo1.Where(p => p.soluong > 0).ToList().Count() > 0)
                                    {
                                        if (solo1.Where(p => p.soluong > 0).First().SoLo != null)
                                        {
                                            grvNhapCT.SetFocusedRowCellValue(colSoLo, solo1.Where(p => p.soluong > 0).First().SoLo);
                                        }
                                        else
                                        {
                                            //Message or sth
                                        }
                                    }
                                }
                                else if (solo1.Where(p => p.soluong > 0).First().SoLo != null)
                                    grvNhapCT.SetFocusedRowCellValue(colSoLo, solo1.First().SoLo);
                            }
                            foreach (var g in solo1)
                            {
                                if (g.SoLo != null && g.SoLo.ToString() != "")
                                {
                                    if (g.soluong > 0)
                                    {
                                        cboSoLo.Items.Add(g.SoLo);
                                    }
                                }
                            }
                            if (TTLuu == 2)
                            {
                                int _madv = 0;
                                double soluongton = 0, _soluong2 = 0, _vat = 0, _dg = 0, _sl = 0;
                                int idnhapct = 0;
                                if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null)
                                    idnhapct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));

                                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                                var soluongluu = data.NhapDcts.Where(p => p.IDNhapct == idnhapct).ToList();
                                if (soluongluu.Count > 0)
                                {
                                    _vat = soluongluu.First().VAT;
                                    _dg = soluongluu.First().DonGia;
                                    _sl = soluongluu.First().SoLuongN;
                                }
                                if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                                    _madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                                soluongton = DungChung.Ham._checkTon(data, _madv, lupTimMaKP.EditValue == null ? 0 : Convert.ToInt32(lupTimMaKP.EditValue), _dg, _sl, "");

                                if (soluongluu.Count > 0)
                                    _soluong2 = soluongluu.First().SoLuongN;

                                if (soluongton < _soluong2 && _soluong2 > 0)
                                {
                                    MessageBox.Show("thuốc đã được sử dụng, bạn không thể sửa!");
                                    grvNhapCT.SetFocusedRowCellValue(colVAT, _vat);
                                    break;
                                }
                            }
                            if (grvNhapCT.GetFocusedRowCellValue(colSoLuong) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString() != "")
                            {
                                double a = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong).ToString());
                                grvNhapCT.SetFocusedRowCellValue(colThanhTien, Math.Round(a * dongiaSauThue, DungChung.Bien.LamTronSo));
                                var ktdy = data.DichVus.Where(p => p.MaDV == madv).Where(p => p.DongY == 1).ToList();
                                if (ktdy.Count > 0)
                                    _thuocyhdt = true;
                                //
                                if (_thuocyhdt && DungChung.Bien.MaBV != "30007" && (DungChung.Bien.MaBV != "14017" || (DungChung.Bien.MaBV == "14017" && !_huhaoGia)) && (DungChung.Bien.MaBV != "20001" || (DungChung.Bien.MaBV == "20001" && !_huhaoGia)))
                                {
                                    grvNhapCT.SetFocusedRowCellValue(colGiaNhapDY, dongiaSauThue);
                                }
                            }
                        }
                    }
                    break;
                #endregion
                case "colSoLo":
                    #region colSoLo
                    string solo = "";
                    if (e.Value != null)
                        solo = e.Value.ToString();
                    if (ppxuat == 3)
                    {
                        if (lupMaKP.EditValue != null)
                            makho = Convert.ToInt32(lupMaKP.EditValue);
                        if (DungChung.Bien.MaBV == "24012")
                        {
                            if (cboNhap.SelectedIndex == 0 || cboNhap.SelectedIndex == 2)
                                makho = Convert.ToInt32(lupBPTra.EditValue);
                        }
                        if (grvNhapCT.GetFocusedRowCellValue(colMaDV) != null)
                            madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                        string _solo = "";
                        if (grvNhapCT.GetFocusedRowCellValue(colSoLo) != null && grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString() != "")
                        {
                            _solo = grvNhapCT.GetFocusedRowCellValue(colSoLo).ToString();
                            var _handung = (from nhapduoc in data.NhapDcts.Where(p => p.MaDV == madv).Where(p => p.SoLo == _solo)
                                           join nduoc in data.NhapDs.Where(p => p.PLoai == 1).Where(p => p.MaKP == makho) on nhapduoc.IDNhap equals nduoc.IDNhap
                                           group new { nhapduoc } by new { nhapduoc.HanDung } into kq
                                           select new { kq.Key.HanDung }).ToList();
                            if (_handung.Count > 0)
                            {
                                if (_handung.First().HanDung != null)
                                    grvNhapCT.SetFocusedRowCellValue(colHanDung, _handung.First().HanDung.Value);
                                else
                                    grvNhapCT.SetFocusedRowCellValue(colHanDung, "");
                            }
                            else
                            {
                                grvNhapCT.SetFocusedRowCellValue(colHanDung, "");
                            }
                            // set tồn thuốc
                            double b = 0;
                            if (grvNhapCT.GetFocusedRowCellValue(colDonGia) != null && grvNhapCT.GetFocusedRowCellValue(colDonGia).ToString() != "")
                            {
                                b = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                            }
                            //var solo1 = (from nhapduoc in data.NhapDcts.Where(p => p.MaDV == (madv)).Where(p => p.DonGia == b).Where(p => p.SoLo == _solo)
                            //            join nduoc in data.NhapDs.Where(p => p.MaKP == (makho)) on nhapduoc.IDNhap equals nduoc.IDNhap
                            //            group new { nhapduoc } by new { nhapduoc.SoLo } into kq
                            //            select new { kq.Key.SoLo, soluong = (kq.Sum(p => p.nhapduoc.SoLuongN) - kq.Sum(p => p.nhapduoc.SoLuongX)) }).ToList();
                            //if (solo1.Count > 0 && solo1.First().soluong != null && solo1.First().soluong > 0)
                            //{
                                DungChung.Bien.SoLuongTon = DungChung.Ham._checkTon_KD(data, madv, makho, b, 0, _solo);
                            if (DungChung.Bien.SoLuongTon > 0)
                            {
                                //DungChung.Bien.SoLuongTon = solo1.First().soluong;
                                if (DungChung.Bien.MaBV == "24012" && cboNhap.SelectedIndex != 0 && cboNhap.SelectedIndex != 2)
                                    groupControl3.Text = "Chi tiết chứng từ";
                                else
                                    groupControl3.Text = "Số lượng tồn: " + DungChung.Bien.SoLuongTon.ToString();
                                double _slTonChuaLuu = 0;
                                double _slTonDaLuu = 0;
                                if (TTLuu == 1)
                                {
                                    var dataSource = (List<NhapDct>)binNhapDuocct.DataSource;
                                    for (int i = 0; i < dataSource.Count; i++)
                                    {
                                        if (dataSource[i].MaDV != null)
                                        {
                                            if (dataSource[i].SoLuongX > 0)
                                            {
                                                if (madv == dataSource[i].MaDV)
                                                {
                                                    if (_solo == dataSource[i].SoLo)
                                                    {
                                                        if (dataSource[i].IDNhapct <= 0)
                                                        {
                                                            _slTonChuaLuu += dataSource[i].SoLuongX;
                                                            if (DungChung.Bien.MaBV == "24012" && cboNhap.SelectedIndex != 0 && cboNhap.SelectedIndex != 2)
                                                                groupControl3.Text = "Chi tiết chứng từ";
                                                            else
                                                                groupControl3.Text = "Số lượng tồn: " + (DungChung.Bien.SoLuongTon - _slTonChuaLuu);
                                                        }
                                                        else
                                                        {
                                                            int id = dataSource[i].IDNhapct;
                                                            var layton = data.NhapDcts.Where(p => p.IDNhapct == id).Select(p => p.SoLuongX).ToList();
                                                            _slTonDaLuu += layton.Sum(p => p);
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                                else if (TTLuu == 2)
                                {
                                    var dataSourceSua = (List<NhapDctss>)binNhapDuocct.DataSource;
                                    for (int i = 0; i < dataSourceSua.Count; i++)
                                    {
                                        if (dataSourceSua[i].MaDV != null)
                                        {
                                            if (dataSourceSua[i].SoLuongX > 0)
                                            {
                                                if (madv == dataSourceSua[i].MaDV)
                                                {
                                                    if (_solo == dataSourceSua[i].SoLo)
                                                    {
                                                        if (dataSourceSua[i].IDNhapct <= 0)
                                                        {
                                                            _slTonChuaLuu += dataSourceSua[i].SoLuongX;
                                                            if (DungChung.Bien.MaBV == "24012" && cboNhap.SelectedIndex != 0 && cboNhap.SelectedIndex != 2)
                                                                groupControl3.Text = "Chi tiết chứng từ";
                                                            else
                                                                groupControl3.Text = "Số lượng tồn: " + (DungChung.Bien.SoLuongTon - _slTonChuaLuu);
                                                        }
                                                        else
                                                        {
                                                            int id = dataSourceSua[i].IDNhapct;
                                                            var layton = data.NhapDcts.Where(p => p.IDNhapct == id).Select(p => p.SoLuongX).ToList();
                                                            _slTonDaLuu += layton.Sum(p => p);
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //MessageBox.Show("Số lượng trong kho không đủ");
                                if (DungChung.Bien.MaBV == "24012" && cboNhap.SelectedIndex != 0 && cboNhap.SelectedIndex != 2)
                                    groupControl3.Text = "Chi tiết chứng từ";
                                else
                                    groupControl3.Text = "Số lượng tồn: 0";
                                DungChung.Bien.SoLuongTon = 0;
                                if (cboNhap.Text != "Nhập theo hóa đơn(nhà cung cấp)" && cboNhap.Text != "Nhập sản xuất")
                                {
                                    grvNhapCT.SetFocusedRowCellValue(colSoLuong, 0);
                                }
                            }
                        }

                    }
                    break;
                    #endregion
                case "colHanDung":
                    #region colHanDung
                    string handung = "";
                    if (e.Value != null)
                        handung = e.Value.ToString();
                    if (ppxuat == 3)
                    {
                        //if (string.IsNullOrEmpty(handung))
                        //    MessageBox.Show("Chưa nhập hạn dùng");
                        grvNhapCT.FocusedColumn = grvNhapCT.VisibleColumns[5];
                    }
                    break;
                    #endregion
                case "colGiaNhapDY":
                    #region colGiaNhapDY
                    if (grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY) != null && grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY).ToString() != "" && grvNhapCT.GetFocusedRowCellValue(colSoLuongDY) != null && grvNhapCT.GetFocusedRowCellValue(colSoLuongDY).ToString() != "")
                    {
                        var slDY = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuongDY));
                        var dgDY = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colGiaNhapDY));
                        grvNhapCT.SetFocusedRowCellValue(colThanhTienDY, Math.Round(slDY * dgDY, DungChung.Bien.LamTronSo, MidpointRounding.AwayFromZero));
                    }
                    break;
                    #endregion
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
                int status = 0;
                if (!string.IsNullOrEmpty(txtstatus.Text))
                    status = Convert.ToInt32(txtstatus.Text);
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                {
                    bool _cothexoa = true;
                    if (status == 1)
                    {
                        MessageBox.Show("Chứng từ đã bị khóa, bạn không thể xóa");
                        _cothexoa = false;
                        return;
                    }

                    int id = Int32.Parse(txtIDNhap.Text);
                    var kt = data.NhapDs.Where(p => p.IDNhap == id).ToList();
                    if (kt.Count > 0 && (kt.First().Status != 1) && DungChung.Ham._checkNgayKhoa(data, dtNgayNhap.DateTime, "KhoaDC") == false)
                    {
                        if (_cothexoa)
                        {
                            if (!DungChung.Ham._KiemTraCBSuaXoa(data, kt.First().MaCB, DungChung.Bien.MaCB))
                            {
                                MessageBox.Show("Tên cán bộ không khớp!");
                                _cothexoa = false;
                            }
                        }
                        var xoact = data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                        if (_cothexoa)
                        {
                            int _mkho = 0;
                            if (lupBPTra.EditValue != null)
                                _mkho = Convert.ToInt32(lupBPTra.EditValue);
                            if(DungChung.Bien.MaBV == "24012")
                            {
                                if(cboNhap.SelectedIndex == 1 || cboNhap.SelectedIndex == 4 || cboNhap.SelectedIndex == 3)
                                _mkho = Convert.ToInt32(lupMaKP.EditValue);
                            }

                            double _soluongton = 0;
                            foreach (var a in xoact)
                            {
                                _soluongton = DungChung.Ham._checkTon(data, a.MaDV == null ? 0 : a.MaDV.Value, _mkho, a.DonGia, 0, a.SoLo);
                                if (_soluongton - a.SoLuongN < 0)
                                    _cothexoa = false;
                                break;
                            }
                            if (_cothexoa == false)
                            {
                                MessageBox.Show("Một số thuốc đã được sử dụng, bạn không thể xóa!");
                            }
                        }
                        int idnhap = Convert.ToInt32(txtIDNhap.Text);
                        if (_cothexoa)
                        {
                            var ktdung = data.NhapDs.Where(p => p.XuatTD == idnhap).ToList();
                            if (ktdung.Count > 0)
                            {
                                MessageBox.Show("Chứng từ đã được sử dụng, bạn không thể xóa");
                                _cothexoa = false;
                            }
                        }
                        if (_cothexoa)
                        {

                        }
                        if (_cothexoa)
                        {
                            DialogResult _result;
                            _result = MessageBox.Show("Bạn muốn xóa chứng từ số: " + txtSoCT.Text, "xóa chứng từ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            int sopl = -1;
                            int kieudon = -1;
                            if (_result == DialogResult.Yes)
                            {
                                var listDichVus = xoact.Select(o => new DichVu { MaDV = (o.MaDV ?? 0) }).ToList();
                                foreach (var xoa in xoact)
                                {
                                    var _xoa = data.NhapDcts.Single(p => p.IDNhapct == (xoa.IDNhapct));

                                    data.NhapDcts.Remove(_xoa);
                                    data.SaveChanges();
                                }
                                var xoac = data.NhapDs.Single(p => p.IDNhap == (id));
                                sopl = xoac.SoPL ?? -1;
                                kieudon = xoac.KieuDon ?? -1;
                                data.NhapDs.Remove(xoac);
                                data.SaveChanges();
                                if (kieudon == 2)
                                {
                                    var ldt = (from dt in data.DThuocs
                                               join dtct in data.DThuoccts.Where(p => p.SoPL == sopl)
                                                on dt.IDDon equals dtct.IDDon
                                               select dt).ToList();
                                    foreach (var item2 in ldt)
                                    {
                                        var dtct = data.DThuoccts.Where(p => p.IDDon == item2.IDDon).ToList();
                                        foreach (var a in dtct)
                                        {
                                            if (a.Status != -1)
                                                a.Status = 0;
                                        }
                                        data.SaveChanges();
                                    }
                                }
                                if (listDichVus.Count > 0 && (DungChung.Bien.MaBV == "20001"))
                                {
                                    DungChung.Ham.UpdateTonDichVu(listDichVus, xoac.MaKP ?? 0, true);
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
                    int id = Int32.Parse(txtIDNhap.Text);
                    var kt = data.NhapDs.Where(p => p.IDNhap == id).ToList();
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
                                if (grvNhapCT.GetFocusedRowCellDisplayText(colMaDV) != null)
                                {
                                    string tenthuoc = grvNhapCT.GetFocusedRowCellDisplayText(colMaDV).ToString();
                                    int _madv = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colMaDV));
                                    double dongia = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colDonGia));
                                    double soluong = Convert.ToDouble(grvNhapCT.GetFocusedRowCellValue(colSoLuong));
                                    double _soluongton = 0;
                                    string _macc = "";
                                    int _mkho = 0;
                                    bool ktra = true;
                                    if (lupBPTra.EditValue != null)
                                        _mkho = Convert.ToInt32(lupBPTra.EditValue);
                                    if (grvNhapCT.GetFocusedRowCellValue(colMaCCct) != null)
                                        _macc = grvNhapCT.GetFocusedRowCellValue(colMaCCct).ToString();
                                    if (cboNhap.Text == "Nhập chuyển kho" || cboNhap.Text == "Nhập trả dược")
                                    {
                                        _soluongton = DungChung.Ham._checkTon(data, _madv, _mkho, dongia, 0, _macc);
                                        if (_soluongton - soluong < 0)
                                        {
                                            ktra = false;
                                        }
                                    }
                                    if (ktra)
                                    {


                                        if (MessageBox.Show("Bạn muốn xóa thuốc: " + tenthuoc, "Xóa chi tiết", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            int idct = Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct));
                                            if (idct == 0)
                                            {
                                                grvNhapCT.DeleteSelectedRows();
                                                return;
                                            }
                                            var _xoact = data.NhapDcts.Single(p => p.IDNhapct == (idct));
                                            data.NhapDcts.Remove(_xoact);
                                            data.SaveChanges();
                                            _lNhapDct = data.NhapDcts.Where(p => p.IDNhap == id).ToList();
                                            binNhapDuocct.DataSource = _lNhapDct;
                                            grcNhapCT.DataSource = binNhapDuocct;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Thuốc đã được sử dụng, bạn không thể xóa");
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
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
                    rep.Formatdate = data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().FormatDate.Value;
                    rep.NguoiLap.Value = data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().NguoiLapBieu.ToString();
                    rep.ThuKho.Value = data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().ThuKho.ToString();
                    rep.MauSo.Value = "Mẫu số: C30-HD";
                    rep.TenPhieu.Value = "PHIẾU NHẬP KHO";
                    rep.colNgay1.Text = par.First().NgayNhap.Value.Day + " / " + par.First().NgayNhap.Value.Month + " / " + par.First().NgayNhap.Value.Year;
                    rep.colcccq.Text = data.HTHONGs.Where(p => p.MaBV.Contains(DungChung.Bien.MaBV)).First().TenCQCQ.ToUpper();
                    rep.colcccq1.Text = "Đơn vị: " + DungChung.Bien.TenCQ.ToUpper();
                    rep.colTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
                    if (par.First().TenNguoiCC != null)
                    {
                        rep.HoTen.Value = par.First().TenNguoiCC;
                        rep.NguoiNhanNguoiGiao.Value = par.First().TenNguoiCC;
                    }
                    rep.KeToanTruong.Value = data.HTHONGs.First().KeToanTruong;
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
                        dicpar["NguoiLap"] = data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().NguoiLapBieu.ToString();
                        dicpar["ThuKho"] = data.HThong_User.Where(p => p.TenDN == DungChung.Bien.TenDN).First().ThuKho.ToString();
                        dicpar["TenBV"] = "Đơn vị: " + DungChung.Bien.TenCQ;
                        dicpar["NgayNhap"] = DungChung.Ham.NgaySangChu(par.First().NgayNhap.Value);
                        dicpar["DiaChi"] = "Địa chỉ: " + data.HTHONGs.First().DiaChi;
                        if (par.First().TenNguoiCC != null)
                        {
                            dicpar["NguoiGiao"] = par.First().TenNguoiCC;
                        }
                        dicpar["KeToanTruong"] = data.HTHONGs.First().KeToanTruong;
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
                data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var httn = data.NhapDs.Where(p => p.IDNhap == id).Select(p => p.KieuDon).FirstOrDefault();
                if (DungChung.Bien.MaBV == "20001" && (httn == 1 || httn == 4))
                {
                    QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001 frmts = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001();
                    frmts.passCB = new QLBV.FormThamSo.frm_ThamSo_InPhieuNhap_20001.PassCB(PassData);
                    frmts.ShowDialog();

                    BaoCao.repPhieuNhapKho_20001 rep = new BaoCao.repPhieuNhapKho_20001();

                    var par = (from nd in data.NhapDs
                               where (nd.IDNhap == id)
                               join kp in data.KPhongs on nd.MaKP equals kp.MaKP
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
                            var nhacc = data.NhaCCs.Where(p => p.MaCC == macc).ToList();
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
                    var q1 = (from nd in data.NhapDs
                              join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                              join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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
                    var q1 = (from nd in data.NhapDs
                              join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                              join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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
                    List<PhieuNhap_12001> q = new List<PhieuNhap_12001>();
                    q = (from a in q2
                         select new PhieuNhap_12001
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

                    var par = (from nd in data.NhapDs
                               where (nd.IDNhap == id)
                               join kp in data.KPhongs on nd.MaKP equals kp.MaKP
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
                            var nhacc = data.NhaCCs.Where(p => p.MaCC == macc).ToList();
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
                    var par = (from nd in data.NhapDs
                               where (nd.IDNhap == id)
                               join kp in data.KPhongs on nd.MaKP equals kp.MaKP
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
                    var q1 = (from nd in data.NhapDs
                              join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                              join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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

                var par = (from nd in data.NhapDs
                           where (nd.IDNhap == id)
                           join kp in data.KPhongs on nd.MaKP equals kp.MaKP
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

                var q = (from nd in data.NhapDs
                         join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var macq = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).ToList().FirstOrDefault();
            frmIn frm = new frmIn();
            if (DungChung.Bien.LoaiPM == "QLBV")
            {
                int id = 0;
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                    id = int.Parse(txtIDNhap.Text);
                if (DungChung.Bien.MaBV == "24009" || _maCQCQ == "24009")
                {
                    if (!_inPhieuNhap_24009(data, id))
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
                    if (!_inPhieuNhapVAT(data, id))
                    {
                        MessageBox.Show("Lỗi in phiếu");
                    }
                }
                else if (DungChung.Bien.MaBV == "12001" || (macq.MaChuQuan != null && macq.MaChuQuan.Trim() == "12001"))
                {
                    if (!_inPhieuNhapVAT(data, id))
                    {
                        MessageBox.Show("Lỗi in phiếu");
                    }
                }
                else

                    if (!_inPhieuNhap(data, id, 0))
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
                var par = (from nd in data.NhapDs
                           where (nd.IDNhap == id)
                           join kp in data.KPhongs on nd.MaKP equals kp.MaKP
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

                var q = (from nd in data.NhapDs
                         join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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
                usNhapDuoc_Load(sender, e);
            }
            TTLuu = 0;
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            frm_NhanXuat frm = new frm_NhanXuat();
            frm.FormClosed += new FormClosedEventHandler(this.usNhapDuoc_Load);
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

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (DungChung.Bien.MaBV == "20001")
            {
                frmIn frm = new frmIn();
                BaoCao.repPhieuNhapKho_20001_DY rep = new BaoCao.repPhieuNhapKho_20001_DY();
                int id = 0;
                if (!string.IsNullOrEmpty(txtIDNhap.Text))
                    id = int.Parse(txtIDNhap.Text);
                var par = (from nd in data.NhapDs
                           where (nd.IDNhap == id)
                           join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                           join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                           select new { nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, nd.MaCC, nd.MaCB }).ToList();
                if (par.Count > 0)
                {
                    rep.So.Value = "Số: " + par.First().SoCT;
                    rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    if (!string.IsNullOrEmpty(par.First().MaCC))
                    {
                        string _macc = par.First().MaCC;
                        var _tencc = data.NhaCCs.Where(p => p.MaCC == _macc).Select(p => p.TenCC).FirstOrDefault();
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
                var q = (from nd in data.NhapDs
                         join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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
                var par = (from nd in data.NhapDs
                           where (nd.IDNhap == id)
                           join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                           join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                           select new { nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC }).ToList();
                if (par.Count > 0)
                {
                    rep.SoCT.Value = "Số: " + par.First().SoCT;
                    rep.Ngay.Value = "Ngày " + par.First().NgayNhap.Value.Day + " tháng " + par.First().NgayNhap.Value.Month + " năm " + par.First().NgayNhap.Value.Year;
                    rep.TenNG.Value = par.First().TenNguoiCC;
                    rep.LyDoNhap.Value = par.First().GhiChu;
                    rep.KhoNhap.Value = par.First().TenKP;


                }

                var q = (from nd in data.NhapDs
                         join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                         join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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
                    if (cboNhap.SelectedIndex == 0)
                    {

                        labelControl16.Text = "Nhập từ kho:";
                    }
                    lupBPTra.Properties.ReadOnly = false;
                    lupMaCC.Properties.ReadOnly = true;
                    lupHinhthucx.Properties.ReadOnly = false;
                }
                else
                {
                    lupMaCC.Properties.ReadOnly = false;
                    lupBPTra.Properties.ReadOnly = true;
                    lupHinhthucx.Properties.ReadOnly = true;
                    grvNhapCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
                }

                lupMaCC_EditValueChanged(null, null);
                if(DungChung.Bien.MaBV == "24012")
                {
                    groupControl3.Text = "Chi tiết chứng từ";
                    if(cboNhap.SelectedIndex == 0 && cboNhap.SelectedIndex == 2)
                    {
                        this.cboSoLo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                    }
                    if (cboNhap.SelectedIndex == 3)
                    {
                        this.cboSoLo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    }
                }
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

            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
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
            else if( DungChung.Bien.MaBV == "27183" )
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
            if (TTLuu == 2)
            {
                if (grvNhapCT.GetFocusedRowCellValue(colIDNhapct) != null && Convert.ToInt32(grvNhapCT.GetFocusedRowCellValue(colIDNhapct)) > 0)
                {
                    colMaDV.OptionsColumn.ReadOnly = true;
                }
                else
                {
                    colMaDV.OptionsColumn.ReadOnly = false;
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
            chkNhap_CheckedChanged(sender, e);
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
                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    int id = 0;
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        id = int.Parse(txtIDNhap.Text);
                    if (!_inPhieuNhap(data, id, 1))
                    {
                        MessageBox.Show("Lỗi in phiếu");
                    }
                    break;
                case 3:
                    data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    int idnhap = 0;
                    if (!string.IsNullOrEmpty(txtIDNhap.Text))
                        idnhap = int.Parse(txtIDNhap.Text);
                    if (!_inPhieuNhap(data, idnhap, 2))
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
                            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            int id1 = Convert.ToInt32(txtIDNhap.Text);
                            var par = (from nd in data.NhapDs
                                       where (nd.IDNhap == id1)
                                       join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                                       join ncc in data.NhaCCs on nd.MaCC equals ncc.MaCC into k
                                       from k1 in k.DefaultIfEmpty()
                                       select new { nd.KieuDon, nd.DiaChi, DiaChi3 = kp.DChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, MaCC = k1 != null ? k1.MaCC : "", TenCC = k1 != null ? k1.TenCC : "", nd.PLoai, nd.SoPL, nd.MaKP, }).ToList();
                            string _mauso = "";

                            var q2 = (from nd in data.NhapDs
                                      join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                      join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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
                                var ten = data.KPhongs.Where(p => p.TenKP == (lupMaKP.Text)).ToList();
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
                            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            int id1 = Convert.ToInt32(txtIDNhap.Text);
                            var par = (from nd in data.NhapDs
                                       where (nd.IDNhap == id1)
                                       join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                                       join ncc in data.NhaCCs on nd.MaCC equals ncc.MaCC into k
                                       from k1 in k.DefaultIfEmpty()
                                       select new { nd.KieuDon, nd.DiaChi, DiaChi3 = kp.DChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, MaCC = k1 != null ? k1.MaCC : "", TenCC = k1 != null ? k1.TenCC : "", nd.PLoai, nd.SoPL, nd.MaKP }).ToList();
                            string _mauso = "";

                            var q2 = (from nd in data.NhapDs
                                      join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                      join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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
                                var ten = data.KPhongs.Where(p => p.TenKP == (lupMaKP.Text)).ToList();
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
                            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                            int id1 = Convert.ToInt32(txtIDNhap.Text);
                            var par = (from nd in data.NhapDs
                                       where (nd.IDNhap == id1)
                                       join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                                       join ncc in data.NhaCCs on nd.MaCC equals ncc.MaCC into k
                                       from k1 in k.DefaultIfEmpty()
                                       select new { nd.KieuDon, nd.DiaChi, DiaChi3 = kp.DChi, nd.NgayNhap, nd.SoCT, nd.GhiChu, kp.TenKP, nd.TenNguoiCC, MaCC = k1 != null ? k1.MaCC : "", TenCC = k1 != null ? k1.TenCC : "", nd.PLoai, nd.SoPL, nd.MaKP }).ToList();
                            string _mauso = "";

                            var q2 = (from nd in data.NhapDs
                                      join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                                      join dv in data.DichVus on ndct.MaDV equals dv.MaDV
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
                                var ten = data.KPhongs.Where(p => p.TenKP == (lupMaKP.Text)).ToList();
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
    }
}
