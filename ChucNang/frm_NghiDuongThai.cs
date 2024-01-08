using DevExpress.XtraEditors.Controls;
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
    public partial class frm_NghiDuongThai : Form
    {
        int maBNhan;
        bool isEdit = false;
        public frm_NghiDuongThai(int _maBNhan)
        {
            InitializeComponent();
            this.maBNhan = _maBNhan;
            LoadDataCombo();
        }

        private void frm_NghiDuongThai_Load(object sender, EventArgs e)
        {
            ResetControl();
            LoadBenhNhanInfo(this.maBNhan);
            EnableControl(isEdit);
        }

        private void LoadDataCombo()
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dscb = dataContext.CanBoes.Where(p => p.Status == 1).ToList();
            cboBacSy.Properties.DataSource = dscb;
            var dscbDaiDien = dscb.Where(p => p.ChucVu.ToLower().Contains("gđ") || p.ChucVu.ToLower().Contains("giám đốc") || p.ChucVu.ToLower().Contains("pgđ") || p.ChucVu.ToLower().Contains("phó giám đôc")).ToList();
            cboNguoiDaiDien.Properties.DataSource = dscbDaiDien;
        }

        private void ResetControl()
        {
            txtHoTen.ResetText();
            txtDonVi.ResetText();
            txtNgaySinh.ResetText();
            cboNguoiDaiDien.EditValue = null;
            txtChanDoan.ResetText();
            txtChanDoan.ResetText();
            cboBacSy.EditValue = null;
            txtSoBHXH.ResetText();
            txtSoTheBHYT.ResetText();
            dtNgayChungTu.EditValue = null;
            dtTuNgay.EditValue = null;
            dtDenNgay.EditValue = null;
            EnableControl(false);
        }

        private void EnableControl(bool edit)
        {
            btnEdit.Enabled = edit;
            btnXoa.Enabled = edit;
            txtDonVi.Enabled = !edit;
            cboNguoiDaiDien.Enabled = !edit;
            txtChanDoan.Enabled = !edit;
            txtChanDoan.Enabled = !edit;
            cboBacSy.Enabled = !edit;
            txtSoBHXH.Enabled = !edit;
            dtNgayChungTu.Enabled = !edit;
            dtTuNgay.Enabled = !edit;
            dtDenNgay.Enabled = !edit;
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

            var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 2);
            if (hsctBHXH != null)
            {
                isEdit = true;
                txtChanDoan.Text = hsctBHXH.PP_DIEUTRI;
                txtDonVi.Text = hsctBHXH.DON_VI;
                txtSoBHXH.Text = hsctBHXH.MA_BHXH;
                txtChanDoan.Text = hsctBHXH.CHAN_DOAN;
                dtNgayChungTu.EditValue = hsctBHXH.NGAY_CT;
                cboNguoiDaiDien.EditValue = hsctBHXH.NGUOI_DAI_DIEN;
                cboBacSy.EditValue = hsctBHXH.MaCB;
                dtTuNgay.EditValue = hsctBHXH.NGAY_VAO;
                dtDenNgay.EditValue = hsctBHXH.NGAY_RA;
            }
            else
            {
                var raVien = dataContext.RaViens.FirstOrDefault(o => o.MaBNhan == _maBNhan);
                if (raVien != null)
                {
                    txtChanDoan.Text = raVien.ChanDoan;
                    dtNgayChungTu.EditValue = raVien.NgayRa;
                }

                var bnkb = dataContext.BNKBs.FirstOrDefault(o => o.MaBNhan == _maBNhan && o.MaKP == benhNhan.MaKP);
                if (bnkb != null)
                {
                    cboBacSy.EditValue = bnkb.MaCB;
                }
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 2);
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
                    frm_NghiDuongThai_Load(null, null);
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
            if (string.IsNullOrWhiteSpace(txtSoBHXH.Text))
            {
                MessageBox.Show("Chưa nhập số BHXH");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDonVi.Text))
            {
                MessageBox.Show("Chưa nhập đơn vị");
                return false;
            }
            if (cboBacSy.EditValue == null)
            {
                MessageBox.Show("Chưa nhập bác sỹ");
                return false;
            }
            if (dtTuNgay.EditValue == null)
            {
                MessageBox.Show("Chưa nhập nghỉ từ ngày");
                return false;
            }
            if (dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Chưa nhập nghỉ đến ngày");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtChanDoan.Text))
            {
                MessageBox.Show("Chưa nhập chẩn đoán");
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
                var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 2);
                if (hsctBHXH != null)
                {
                    if (hsctBHXH.IS_SEND == true)
                    {
                        MessageBox.Show("Hồ sơ đã được gửi không thể sửa");
                        return;
                    }
                    hsctBHXH.DON_VI = txtDonVi.Text;
                    hsctBHXH.CHAN_DOAN = txtChanDoan.Text;
                    hsctBHXH.MA_BHXH = txtSoBHXH.Text;
                    if (cboBacSy.EditValue != null)
                    {
                        hsctBHXH.MaCB = cboBacSy.EditValue.ToString();
                    }
                    else
                        hsctBHXH.MaCB = null;
                    if (dtNgayChungTu.EditValue != null)
                    {
                        hsctBHXH.NGAY_CT = dtNgayChungTu.DateTime;
                    }
                    else
                        hsctBHXH.NGAY_CT = null;
                    if (cboNguoiDaiDien.EditValue != null)
                    {
                        hsctBHXH.NGUOI_DAI_DIEN = cboNguoiDaiDien.Text;
                    }
                    else
                        hsctBHXH.NGUOI_DAI_DIEN = null;
                    hsctBHXH.PP_DIEUTRI = txtChanDoan.Text;
                    if (dtTuNgay.EditValue != null)
                    {
                        hsctBHXH.NGAY_VAO = dtTuNgay.DateTime;
                    }
                    else
                        hsctBHXH.NGAY_VAO = null;
                    if (dtDenNgay.EditValue != null)
                    {
                        hsctBHXH.NGAY_RA = dtDenNgay.DateTime;
                    }
                    else
                        hsctBHXH.NGAY_RA = null;
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
                hsctBHXHNew.DON_VI = txtDonVi.Text;
                hsctBHXHNew.CHAN_DOAN = txtChanDoan.Text;
                hsctBHXHNew.MA_BHXH = txtSoBHXH.Text;
                if (cboBacSy.EditValue != null)
                {
                    hsctBHXHNew.MaCB = cboBacSy.EditValue.ToString();
                }
                else
                    hsctBHXHNew.MaCB = null;

                if (dtNgayChungTu.EditValue != null)
                {
                    hsctBHXHNew.NGAY_CT = dtNgayChungTu.DateTime;
                }
                else
                    hsctBHXHNew.NGAY_CT = null;
                if (cboNguoiDaiDien.EditValue != null)
                {
                    hsctBHXHNew.NGUOI_DAI_DIEN = cboNguoiDaiDien.EditValue.ToString();
                }
                else
                    hsctBHXHNew.NGUOI_DAI_DIEN = null;
                hsctBHXHNew.PP_DIEUTRI = txtChanDoan.Text;
                if (dtTuNgay.EditValue != null)
                {
                    hsctBHXHNew.NGAY_VAO = dtTuNgay.DateTime;
                }
                else
                    hsctBHXHNew.NGAY_VAO = null;
                if (dtDenNgay.EditValue != null)
                {
                    hsctBHXHNew.NGAY_RA = dtDenNgay.DateTime;
                }
                else
                    hsctBHXHNew.NGAY_RA = null;
                hsctBHXHNew.LOAI = 2;
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
                frm_NghiDuongThai_Load(null, null);
            }
        }

        private void lookUpEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                cboBacSy.EditValue = null;
            }
        }

        private void cboBacSy_EditValueChanged(object sender, EventArgs e)
        {
            cboBacSy.Properties.Buttons[1].Visible = (cboBacSy.EditValue != null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnableControl(false);
        }
    }
}
