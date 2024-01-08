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
    public partial class frm_NghiViecHuongBHXH : Form
    {
        int maBNhan;
        bool isEdit = false;
        public frm_NghiViecHuongBHXH(int _maBNhan)
        {
            InitializeComponent();
            this.maBNhan = _maBNhan;
            LoadDataCombo();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_NghiViecHuongBHXH_Load(object sender, EventArgs e)
        {
            ResetControl();
            LoadBenhNhanInfo(this.maBNhan);
            EnableControl(isEdit);
        }

        private void LoadDataCombo()
        {
            var dscb = dataContext.CanBoes.Where(p => p.Status == 1).ToList().Where(o => !string.IsNullOrWhiteSpace(o.MaCCHN)).ToList();
            cboBacSy.Properties.DataSource = dscb;
        }

        private void ResetControl()
        {
            txtHoTen.ResetText();
            txtDonVi.ResetText();
            txtNgaySinh.ResetText();
            cboBacSy.EditValue = null;
            txtPPDieuTri.ResetText();
            txtChanDoan.ResetText();
            txtSoBHXH.ResetText();
            txtSoTheBHYT.ResetText();
            txtHoTenCha.ResetText();
            txtHoTenMe.ResetText();
            txtChanDoan.ResetText();
            dtDenNgay.EditValue = null;
            dtNgayChungTu.EditValue = null;
            dtTuNgay.EditValue = null;
            cbbThuTruongDonVi.ResetText();
            EnableControl(false);
        }

        private void EnableControl(bool edit)
        {
            dtNgayTao.Enabled = !edit;
            btnEdit.Enabled = edit;
            btnXoa.Enabled = edit;
            txtDonVi.Enabled = !edit;
            cboBacSy.Enabled = !edit;
            txtChanDoan.Enabled = !edit;
            txtPPDieuTri.Enabled = !edit;
            cbbThuTruongDonVi.Enabled = !edit;
            txtSoBHXH.Enabled = !edit;
            txtHoTenCha.Enabled = !edit;
            txtHoTenMe.Enabled = !edit;
            dtTuNgay.Enabled = !edit;
            dtNgayChungTu.Enabled = !edit;
            dtDenNgay.Enabled = !edit;
        }

        private void LoadBenhNhanInfo(int _maBNhan)
        {
            isEdit = false;
            var benhNhan = dataContext.BenhNhans.FirstOrDefault(o => o.MaBNhan == _maBNhan);
            if (benhNhan != null)
            {
                txtHoTen.Text = benhNhan.TenBNhan;
                txtSoTheBHYT.Text = benhNhan.SThe;
                txtNgaySinh.Text = DungChung.Ham.GhepNgaySinh("/", benhNhan.NamSinh, benhNhan.ThangSinh, benhNhan.NgaySinh);
                if (!string.IsNullOrWhiteSpace(benhNhan.SThe))
                    txtSoBHXH.Text = benhNhan.SThe.Substring(5);
            }

            cbbThuTruongDonVi.Text = DungChung.Bien.GiamDoc;
            var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 4);
            dtNgayTao.EditValue = DateTime.Now;
            if (hsctBHXH != null)
            {
                isEdit = true;
                dtNgayTao.EditValue = hsctBHXH.NgayTao;
                txtPPDieuTri.Text = hsctBHXH.PP_DIEUTRI;
                txtDonVi.Text = hsctBHXH.DON_VI;
                txtChanDoan.Text = hsctBHXH.CHAN_DOAN;
                txtSoBHXH.Text = hsctBHXH.MA_BHXH;
                cbbThuTruongDonVi.Text = hsctBHXH.THU_TRUONG_DVI;
                dtTuNgay.EditValue = hsctBHXH.NGOAITRU_TUNGAY;
                dtDenNgay.EditValue = hsctBHXH.NGOAITRU_DENNGAY;
                dtNgayChungTu.EditValue = hsctBHXH.NGAY_CT;
                cboBacSy.EditValue = hsctBHXH.MA_CCHN_TRUONGKHOA;
                txtHoTenCha.Text = hsctBHXH.HO_TEN_CHA;
                txtHoTenMe.Text = hsctBHXH.HO_TEN_ME;
            }
            else
            {
                var bnkb = dataContext.BNKBs.FirstOrDefault(p => p.MaBNhan == _maBNhan);
                if (bnkb != null)
                {
                    var cb = dataContext.CanBoes.FirstOrDefault(p => p.MaCB == bnkb.MaCB);
                    if (cb != null)
                    {
                        cboBacSy.EditValue = cb.MaCCHN;
                    }
                }
                var raVien = dataContext.RaViens.FirstOrDefault(o => o.MaBNhan == _maBNhan);
                if (raVien != null)
                {
                    dtNgayChungTu.EditValue = raVien.NgayRa;
                    txtPPDieuTri.Text = raVien.PPDTr;
                    txtChanDoan.Text = raVien.ChanDoan;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 4);
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
                    frm_NghiViecHuongBHXH_Load(null, null);
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
            if (string.IsNullOrWhiteSpace(txtPPDieuTri.Text))
            {
                MessageBox.Show("Chưa nhập phương pháp điều trị");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtChanDoan.Text))
            {
                MessageBox.Show("Chưa nhập chẩn đoán");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDonVi.Text))
            {
                MessageBox.Show("Chưa nhập đơn vị");
                return false;
            }
            if (dtTuNgay.EditValue == null)
            {
                MessageBox.Show("Chưa nhập ngày nghỉ từ");
                return false;
            }
            if (dtDenNgay.EditValue == null)
            {
                MessageBox.Show("Chưa nhập ngày nghỉ đến");
                return false;
            }
            if (cboBacSy.EditValue == null)
            {
                MessageBox.Show("Chưa nhập bác sỹ");
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
            if (!CheckSave())
            {
                return;
            }
            if (isEdit)
            {
                var hsctBHXH = dataContext.HSCT_BHXH.FirstOrDefault(o => o.MaBNhan == maBNhan && o.LOAI == 4);
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
                    hsctBHXH.HO_TEN_CHA = txtHoTenCha.Text;
                    hsctBHXH.HO_TEN_ME = txtHoTenMe.Text;
                    hsctBHXH.NgayTao = dtNgayTao.DateTime;
                    if (dtTuNgay.EditValue != null)
                    {
                        hsctBHXH.NGOAITRU_TUNGAY = dtTuNgay.DateTime;
                    }
                    else
                        hsctBHXH.NGOAITRU_TUNGAY = null;
                    if (dtDenNgay.EditValue != null)
                    {
                        hsctBHXH.NGOAITRU_DENNGAY = dtDenNgay.DateTime;
                    }
                    else
                        hsctBHXH.NGOAITRU_DENNGAY = null;
                    if (dtNgayChungTu.EditValue != null)
                    {
                        hsctBHXH.NGAY_CT = dtNgayChungTu.DateTime;
                    }
                    else
                        hsctBHXH.NGAY_CT = null;
                    hsctBHXH.MA_CCHN_TRUONGKHOA = cboBacSy.EditValue != null ? cboBacSy.EditValue.ToString() : null;
                    hsctBHXH.TEN_TRUONGKHOA = cboBacSy.EditValue != null ? cboBacSy.Text : null;
                    hsctBHXH.PP_DIEUTRI = txtPPDieuTri.Text;
                    hsctBHXH.THU_TRUONG_DVI = cbbThuTruongDonVi.Text;
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
                hsctBHXHNew.HO_TEN_CHA = txtHoTenCha.Text;
                hsctBHXHNew.HO_TEN_ME = txtHoTenMe.Text;
                hsctBHXHNew.NgayTao = dtNgayTao.DateTime;
                if (dtTuNgay.EditValue != null)
                {
                    hsctBHXHNew.NGOAITRU_TUNGAY = dtTuNgay.DateTime;
                }
                else
                    hsctBHXHNew.NGOAITRU_TUNGAY = null;
                if (dtDenNgay.EditValue != null)
                {
                    hsctBHXHNew.NGOAITRU_DENNGAY = dtDenNgay.DateTime;
                }
                else
                    hsctBHXHNew.NGOAITRU_DENNGAY = null;
                if (dtNgayChungTu.EditValue != null)
                {
                    hsctBHXHNew.NGAY_CT = dtNgayChungTu.DateTime;
                }
                else
                    hsctBHXHNew.NGAY_CT = null;
                hsctBHXHNew.MA_CCHN_TRUONGKHOA = cboBacSy.EditValue != null ? cboBacSy.EditValue.ToString() : null;
                hsctBHXHNew.TEN_TRUONGKHOA = cboBacSy.EditValue != null ? cboBacSy.Text : null;
                hsctBHXHNew.PP_DIEUTRI = txtPPDieuTri.Text;
                hsctBHXHNew.THU_TRUONG_DVI = cbbThuTruongDonVi.Text;
                hsctBHXHNew.LOAI = 4;
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
                frm_NghiViecHuongBHXH_Load(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnableControl(false);
        }

        private void lookUpEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
                cboBacSy.EditValue = null;
        }

        private void cboBacSy_EditValueChanged(object sender, EventArgs e)
        {
            cboBacSy.Properties.Buttons[1].Visible = (cboBacSy.EditValue != null);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            frmIn frm = new frmIn();
            var bhxh = (from bn in dataContext.BenhNhans.Where(p => p.MaBNhan == maBNhan)
                        join bh in dataContext.HSCT_BHXH on bn.MaBNhan equals bh.MaBNhan
                        select new
                        {
                            bn.MaBNhan,
                            bn.TenBNhan,
                            bn.NgaySinh,
                            bn.ThangSinh,
                            bn.NamSinh,
                            bn.GTinh,
                            bn.SThe,
                            bh.DON_VI,
                            bh.CHAN_DOAN,
                            bh.PP_DIEUTRI,
                            bh.NGOAITRU_TUNGAY,
                            bh.NGOAITRU_DENNGAY,
                            bh.HO_TEN_CHA,
                            bh.HO_TEN_ME,
                            bh.SO_LUU_TRU,
                            bh.NgayTao,
                            bh.TEN_TRUONGKHOA,
                        }).ToList();
            TimeSpan? ngaynghi = null;
            if (bhxh.First().NGOAITRU_DENNGAY != null && bhxh.First().NGOAITRU_TUNGAY != null)
            {
                DateTime? tungay = bhxh.First().NGOAITRU_TUNGAY;
                DateTime? denngay = bhxh.First().NGOAITRU_DENNGAY;
                ngaynghi = denngay.Value.Date - tungay.Value.Date ;
                
            }
            else
            {
                MessageBox.Show("Thiếu thông tin từ ngày, đến ngày!");
                return;
            }
            BaoCao.Rep_NghiViecHuongBHXH rep = new BaoCao.Rep_NghiViecHuongBHXH();
            if (DungChung.Bien.MaBV == "27183")
            {
                rep.xrLabel54.Visible = false;
                rep.colTenCB.Visible = false;
                rep.Parameters["TenBN"].Value = bhxh.First().TenBNhan.ToUpper();
            }
            else
                rep.Parameters["TenBN"].Value = bhxh.First().TenBNhan;
            rep.Parameters["TenCQCQ"].Value = DungChung.Bien.TenCQCQ;
            rep.Parameters["TenCQ"].Value = DungChung.Bien.TenCQ;
            rep.Parameters["SoKCB"].Value = bhxh.First().MaBNhan;
            rep.Parameters["So"].Value = bhxh.First().SO_LUU_TRU;
            rep.Parameters["NgaySinh"].Value = bhxh.First().NgaySinh + "/ " + bhxh.First().ThangSinh + "/ " + bhxh.First().NamSinh;
            rep.Parameters["BHYT"].Value = bhxh.First().SThe;
            rep.Parameters["GTinh"].Value = bhxh.First().GTinh == 1 ? "Nam" : "Nữ";
            rep.Parameters["DVLamViec"].Value = bhxh.First().DON_VI;
            rep.Parameters["ChanDoan"].Value = bhxh.First().CHAN_DOAN + "\r\n" + bhxh.First().PP_DIEUTRI;
            rep.Parameters["SoNN"].Value = ngaynghi.Value.TotalDays + 1;
            rep.Parameters["DenNgay"].Value ="(Từ ngày  " + bhxh.First().NGOAITRU_TUNGAY.Value.ToString("dd/MM/yyyy") + " đến ngày  " + bhxh.First().NGOAITRU_DENNGAY.Value.ToString("dd/MM/yyyy") + ")";
            rep.Parameters["TenCha"].Value = bhxh.First().HO_TEN_CHA;
            rep.Parameters["TenMe"].Value = bhxh.First().HO_TEN_ME;
            rep.Parameters["TenCB"].Value = DungChung.Bien.GiamDoc;
            DateTime ngaytao = Convert.ToDateTime(bhxh.First().NgayTao);
            rep.Parameters["Ngay"].Value = "Ngày " + ngaytao.ToString("dd") + " Tháng " + ngaytao.ToString("MM") + " Năm " + ngaytao.ToString("yyyy");
            rep.Parameters["TenKB"].Value = bhxh.First().TEN_TRUONGKHOA;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}
