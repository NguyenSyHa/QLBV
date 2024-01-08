using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_TT_RaVien : Form
    {
        int maBNhan;
        bool isEdit = false;
        int maKP;
        public frm_TT_RaVien(int _maBNhan, int maKP)
        {
            InitializeComponent();
            this.maBNhan = _maBNhan;
            this.maKP = maKP;
            LoadDataCombo();
        }

        private void frm_TT_RaVien_Load(object sender, EventArgs e)
        {
            ResetControl();
            LoadBenhNhanInfo(this.maBNhan);
            EnableControl(isEdit);
        }

        private void LoadDataCombo()
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dscb = dataContext.CanBoes.Where(p => p.Status == 1).ToList().Where(o => !string.IsNullOrWhiteSpace(o.MaCCHN)).ToList();
            cboTruongKhoa.Properties.DataSource = dscb;
            var dskp = dataContext.KPhongs.Where(p => p.Status == 1).ToList();
            cboKhoa.Properties.DataSource = dskp;
        }

        private void ResetControl()
        {
            txtHoTen.ResetText();
            txtNgaySinh.ResetText();
            txtGhiChu.ResetText();
            txtPPDieuTri.ResetText();
            txtSoBHXH.ResetText();
            txtSoTheBHYT.ResetText();
            txtHoTenCha.ResetText();
            txtHoTenMe.ResetText();
            dtNgayChungTu.EditValue = null;
            dtDTNgoaiTruTu.EditValue = null;
            dtDTNgoaiTruDen.EditValue = null;
            cbbThuTruongDonVi.ResetText();
            txtChanDoan.ResetText();
            chkDinhChiThaiNghen.Checked = false;
            txtTuoiThai.ResetText();
            cboKhoa.EditValue = null;
            cboTruongKhoa.EditValue = null;
            EnableControl(false);
        }

        private void EnableControl(bool edit)
        {
            btnEdit.Enabled = edit;
            btnXoa.Enabled = edit;
            txtGhiChu.Enabled = !edit;
            txtPPDieuTri.Enabled = !edit;
            txtSoBHXH.Enabled = !edit;
            txtHoTenCha.Enabled = !edit;
            txtHoTenMe.Enabled = !edit;
            dtNgayChungTu.Enabled = !edit;
            dtDTNgoaiTruDen.Enabled = !edit;
            dtDTNgoaiTruTu.Enabled = !edit;
            chkDinhChiThaiNghen.Enabled = !edit;
            txtTuoiThai.Enabled = !edit;
            txtChanDoan.Enabled = !edit;
            cbbThuTruongDonVi.Enabled = !edit;
            cboKhoa.Enabled = !edit;
            cboTruongKhoa.Enabled = !edit;
        }

        private void LoadBenhNhanInfo(int _maBNhan)
        {
            isEdit = false;
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var benhNhan = dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == _maBNhan);
            if (benhNhan != null)
            {
                txtHoTen.Text = benhNhan.TenBNhan;
                txtSoTheBHYT.Text = benhNhan.SThe;
                txtNgaySinh.Text = DungChung.Ham.GhepNgaySinh("/", benhNhan.NamSinh, benhNhan.ThangSinh, benhNhan.NgaySinh);
                if (!string.IsNullOrWhiteSpace(benhNhan.SThe))
                    txtSoBHXH.Text = benhNhan.SThe.Substring(5);
            }

            cboKhoa.EditValue = maKP;
            cbbThuTruongDonVi.Text = DungChung.Bien.GiamDoc;

            var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 3);
            if (hsctBHXH != null)
            {
                isEdit = true;
                txtGhiChu.Text = hsctBHXH.GHI_CHU;
                txtPPDieuTri.Text = hsctBHXH.PP_DIEUTRI;
                chkDinhChiThaiNghen.Checked = hsctBHXH.DINH_CHI_THAI_NGHEN == null ? false : true;
                txtTuoiThai.Text = hsctBHXH.TUOI_THAI;
                cbbThuTruongDonVi.Text = hsctBHXH.THU_TRUONG_DVI;
                txtSoBHXH.Text = hsctBHXH.MA_BHXH;
                dtDTNgoaiTruTu.EditValue = hsctBHXH.NGOAITRU_TUNGAY;
                dtDTNgoaiTruDen.EditValue = hsctBHXH.NGOAITRU_DENNGAY;
                txtChanDoan.Text = hsctBHXH.CHAN_DOAN;
                dtNgayChungTu.EditValue = hsctBHXH.NGAY_CT;
                txtHoTenCha.Text = hsctBHXH.HO_TEN_CHA;
                txtHoTenMe.Text = hsctBHXH.HO_TEN_ME;
                cboKhoa.EditValue = hsctBHXH.MA_KHOA;
                cboTruongKhoa.EditValue = hsctBHXH.MA_CCHN_TRUONGKHOA;
                txtSoLuuTru.Text = hsctBHXH.SO_LUU_TRU;
            }
            else
            {
                var raVien = dataContext.RaViens.FirstOrDefault(o => o.MaBNhan == _maBNhan);
                if (raVien != null)
                {
                    dtNgayChungTu.EditValue = raVien.NgayRa;
                    txtPPDieuTri.Text = raVien.PPDTr;
                    txtChanDoan.Text = raVien.ChanDoan;
                    txtSoLuuTru.Text = raVien.SoLT;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 3);
            if (hsctBHXH != null)
            {
                if (hsctBHXH.IS_SEND == true)
                {
                    MessageBox.Show("Hồ sơ đã được gửi không thể xóa");
                    return;
                }
                dataContext.HSCT_BHXH.Remove(hsctBHXH);
                if (dataContext.SaveChanges() >= 0)
                {
                    MessageBox.Show("Xóa thành công");
                    frm_TT_RaVien_Load(null, null);
                }
            }
        }

        private bool CheckSave()
        {
            bool rs = true;
            if (dtNgayChungTu.EditValue == null)
            {
                MessageBox.Show("Chưa nhập ngày chứng từ");
                return false;
            }
            if (cboKhoa.EditValue == null)
            {
                MessageBox.Show("Chưa chọn khoa");
                return false;
            }
            if (cboTruongKhoa.EditValue == null)
            {
                MessageBox.Show("Chưa chọn trưởng khoa");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSoBHXH.Text))
            {
                MessageBox.Show("Chưa nhập số BHXH");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPPDieuTri.Text))
            {
                MessageBox.Show("Chưa nhập phương pháp điều trị");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cbbThuTruongDonVi.Text))
            {
                MessageBox.Show("Chưa nhập thủ trưởng đơn vị");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtChanDoan.Text))
            {
                MessageBox.Show("Chưa nhập chẩn đoán");
                return false;
            }
            if (chkDinhChiThaiNghen.Checked && string.IsNullOrWhiteSpace(txtTuoiThai.Text))
            {
                MessageBox.Show("Chưa nhập tuổi thai");
                return false;
            }
            if (txtSoBHXH.Text.Length != 10)
            {
                MessageBox.Show("Số BHXH phải đủ 10 ký tự");
                return false;
            }
            return rs;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            bool rs = false;
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (!CheckSave())
            {
                return;
            }
            if (isEdit)
            {
                var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 3);
                if (hsctBHXH != null)
                {
                    if (hsctBHXH.IS_SEND == true)
                    {
                        MessageBox.Show("Hồ sơ đã được gửi không thể sửa");
                        return;
                    }
                    hsctBHXH.GHI_CHU = txtGhiChu.Text;
                    hsctBHXH.MA_BHXH = txtSoBHXH.Text;
                    hsctBHXH.HO_TEN_CHA = txtHoTenCha.Text;
                    hsctBHXH.HO_TEN_ME = txtHoTenMe.Text;
                    hsctBHXH.SO_LUU_TRU = txtSoLuuTru.Text;
                    if (dtDTNgoaiTruTu.EditValue != null)
                    {
                        hsctBHXH.NGOAITRU_TUNGAY = dtDTNgoaiTruTu.DateTime;
                    }
                    else
                        hsctBHXH.NGOAITRU_TUNGAY = null;
                    if (dtDTNgoaiTruDen.EditValue != null)
                    {
                        hsctBHXH.NGOAITRU_DENNGAY = dtDTNgoaiTruDen.DateTime;
                    }
                    else
                        hsctBHXH.NGOAITRU_DENNGAY = null;
                    if (dtNgayChungTu.EditValue != null)
                    {
                        hsctBHXH.NGAY_CT = dtNgayChungTu.DateTime;
                    }
                    else
                        hsctBHXH.NGAY_CT = null;
                    hsctBHXH.THU_TRUONG_DVI = cbbThuTruongDonVi.Text;
                    hsctBHXH.PP_DIEUTRI = txtPPDieuTri.Text;
                    hsctBHXH.CHAN_DOAN = txtChanDoan.Text;
                    hsctBHXH.DINH_CHI_THAI_NGHEN = chkDinhChiThaiNghen.Checked;
                    hsctBHXH.TUOI_THAI = chkDinhChiThaiNghen.Checked ? txtTuoiThai.Text : "";
                    hsctBHXH.MA_KHOA = cboKhoa.EditValue != null ? cboKhoa.EditValue.ToString() : null;
                    hsctBHXH.MA_CCHN_TRUONGKHOA = cboTruongKhoa.EditValue != null ? cboTruongKhoa.EditValue.ToString() : null;
                    hsctBHXH.TEN_TRUONGKHOA = cboTruongKhoa.EditValue != null ? cboTruongKhoa.Text : null;
                    dataContext.SaveChanges();
                    if (dataContext.SaveChanges() >= 0)
                    {
                        rs = true;
                    }
                }
            }
            else
            {
                HSCT_BHXH hsctBHXHNew = new HSCT_BHXH();
                hsctBHXHNew.GHI_CHU = txtGhiChu.Text;
                hsctBHXHNew.MA_BHXH = txtSoBHXH.Text;
                hsctBHXHNew.HO_TEN_CHA = txtHoTenCha.Text;
                hsctBHXHNew.HO_TEN_ME = txtHoTenMe.Text;
                hsctBHXHNew.SO_LUU_TRU = txtSoLuuTru.Text;
                if (dtDTNgoaiTruTu.EditValue != null)
                {
                    hsctBHXHNew.NGOAITRU_TUNGAY = dtDTNgoaiTruTu.DateTime;
                }
                else
                    hsctBHXHNew.NGOAITRU_TUNGAY = null;
                if (dtDTNgoaiTruDen.EditValue != null)
                {
                    hsctBHXHNew.NGOAITRU_DENNGAY = dtDTNgoaiTruDen.DateTime;
                }
                else
                    hsctBHXHNew.NGOAITRU_DENNGAY = null;
                if (dtNgayChungTu.EditValue != null)
                {
                    hsctBHXHNew.NGAY_CT = dtNgayChungTu.DateTime;
                }
                else
                    hsctBHXHNew.NGAY_CT = null;
                hsctBHXHNew.THU_TRUONG_DVI = cbbThuTruongDonVi.Text;
                hsctBHXHNew.PP_DIEUTRI = txtPPDieuTri.Text;
                hsctBHXHNew.CHAN_DOAN = txtChanDoan.Text;
                hsctBHXHNew.DINH_CHI_THAI_NGHEN = chkDinhChiThaiNghen.Checked;
                hsctBHXHNew.TUOI_THAI = chkDinhChiThaiNghen.Checked ? txtTuoiThai.Text : "";
                hsctBHXHNew.MA_KHOA = cboKhoa.EditValue != null ? cboKhoa.EditValue.ToString() : null;
                hsctBHXHNew.MA_CCHN_TRUONGKHOA = cboTruongKhoa.EditValue != null ? cboTruongKhoa.EditValue.ToString() : null;
                hsctBHXHNew.TEN_TRUONGKHOA = cboTruongKhoa.EditValue != null ? cboTruongKhoa.Text : null;
                hsctBHXHNew.LOAI = 3;
                hsctBHXHNew.MaBNhan = this.maBNhan;
                dataContext.HSCT_BHXH.Add(hsctBHXHNew);
                dataContext.SaveChanges();
                if (dataContext.SaveChanges() >= 0)
                {
                    rs = true;
                }
            }

            if (rs)
            {
                MessageBox.Show("Lưu thành công");
                frm_TT_RaVien_Load(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnableControl(false);
        }

        private void chkDinhChiThaiNghen_CheckedChanged(object sender, EventArgs e)
        {
            txtTuoiThai.Enabled = chkDinhChiThaiNghen.Checked;
        }
    }
}
