using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormDanhMuc
{
    public partial class Frm_DmDuoc : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DmDuoc()
        {
            InitializeComponent();
        }
        int TTLuu = 0;
        int _madv = 0;
        List<DichVu> _ldichvu = new List<DichVu>();
        private void EnableButton(bool t)
        {
            btnLuu.Enabled = !t;
            btnMoi.Enabled = t;
            btnSua.Enabled = t;
            btnXoa.Enabled = t;
            txtMaDV.Properties.ReadOnly = t;
            txtTenDV.Properties.ReadOnly = t;
            txtTenHC.Properties.ReadOnly = t;
            txtHamLuong.Properties.ReadOnly = t;
            txtDonGia.Properties.ReadOnly = t;
            txtDuongDung.Properties.ReadOnly = t;
            txtHamLuong.Properties.ReadOnly = t;
            lupNhom.Properties.ReadOnly = t;
            lupMaCC.Properties.ReadOnly = t;
            lupPhanLoai.Properties.ReadOnly = true;
            cboDonVi.Properties.ReadOnly = t;
            txtNuocSX.Properties.ReadOnly = t;
            cboTrongDM.Properties.ReadOnly = t;
            chkStatus.Properties.ReadOnly = t;
            txtSoDK.Properties.ReadOnly = t;
            txtNhaSX.Properties.ReadOnly = t;
            txtQCPC.Properties.ReadOnly = t;
            txtDangBC.Properties.ReadOnly = t;
            txtMaQD.Properties.ReadOnly = t;
            cboTTNhap.Properties.ReadOnly = t;
            cboNguonGoc.Properties.ReadOnly = t;
            cboBPDung.Properties.ReadOnly = t;
            cboYCSD.Properties.ReadOnly = t;
            txtTLHH.Properties.ReadOnly = t;
            txtTLBQ.Properties.ReadOnly = t;
            chkDY.Properties.ReadOnly = t;
            grcDichVu.Enabled = t;

        }
        private void resetcontrol()
        {
            txtMaQD.Text = "";
            txtMaDV.Text = "";
            txtTenDV.Text = "";
            txtTenHC.Text = "";
            txtHamLuong.Text = "";
            txtDonGia.Text = "";
            txtDuongDung.Text = "";
            txtHamLuong.Text = "";
            lupNhom.EditValue = "";
            lupPhanLoai.EditValue = "";
            cboDonVi.Text = "";
            txtNuocSX.Text = "";
            txtSoDK.Text = "";
            txtNhaSX.Text = "";
            txtQCPC.Text = "";
            txtDangBC.Text = "";
            cboTrongDM.SelectedIndex = -1;
            chkStatus.Checked = true;
            cboTTNhap.Text = "";
            cboNguonGoc.Text = "";
            cboYCSD.Text = "";
            cboBPDung.Text = "";
            txtTLHH.Text = "";
            txtTLBQ.Text = "";
        }
        private bool Ktra()
        {

            int ot;
            int _int_madv = 0;
            if (Int32.TryParse(txtMaDV.Text, out ot))
                _int_madv = Convert.ToInt32(txtMaDV.Text);

            if (string.IsNullOrEmpty(txtMaDV.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã dược");
                txtMaDV.Focus();
                return false;
            }
            else if (_int_madv == 0)
            {
                MessageBox.Show("Mã dược không hợp lệ!");
                txtMaDV.Focus();
                return false;
            }
            else
            {
                if (TTLuu == 1)
                {
                    bool t = false;
                    string _timkiem = "";
                    if (!string.IsNullOrEmpty(txtTenDV.Text))
                        _timkiem = txtTenDV.Text.ToLower();
                    ;
                    if (_ldichvu.Where(p => p.TenDV.ToLower().Contains(_timkiem.ToLower())).ToList().Count > 0)
                        t = true;
                    if (t)
                    {
                        DialogResult _result = MessageBox.Show("Thuốc: " + _timkiem + " đã có, bạn vẫn muốn thêm mới?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result == DialogResult.No)
                        {
                            txtTenDV.Focus();
                            txtTenDV.SelectAll();
                            return false;
                        }
                    }

                }
            }
            if (string.IsNullOrEmpty(txtTenDV.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên dược");
                txtTenDV.Focus();
                return false;
            }
            if (lupNhom.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn nhóm dịch vụ");
                lupNhom.Focus();
                return false;
            }
            if (lupPhanLoai.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn nhóm dịch vụ");
                lupPhanLoai.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboDonVi.Text))
            {
                MessageBox.Show("Bạn chưa chọn đơn vị");
                cboDonVi.Focus();
                return false;
            }
            if (lupMaCC.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn nhà cung cấp");
                lupMaCC.Focus();
                return false;
            }
            if (chkDY.Checked == true)
            {
                if (string.IsNullOrEmpty(cboTTNhap.Text))
                {
                    MessageBox.Show("Bạn chưa nhập tình trạng nhập của dịch vụ!");
                    cboTTNhap.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(cboNguonGoc.Text))
                {
                    MessageBox.Show("Bạn chưa nhập tình nguồn gốc của dịch vụ!");
                    cboNguonGoc.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(cboBPDung.Text))
                {
                    MessageBox.Show("Bạn chưa nhập bộ phận dùng của dịch vụ!");
                    cboBPDung.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(cboYCSD.Text))
                {
                    MessageBox.Show("Bạn chưa nhập yêu cầu sử dụng của dịch vụ!");
                    cboYCSD.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtTLHH.Text))
                {
                    MessageBox.Show("Bạn chưa nhập tỷ lệ hư hao khi sơ chế hay phức chế!");
                    txtTLHH.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtTLBQ.Text))
                {
                    MessageBox.Show("Bạn chưa nhập tỷ lệ bảo quản!");
                    txtTLBQ.Focus();
                    return false;
                }

            }


            return true;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<NhomDV> _lNhom = new List<NhomDV>();

        private void Frm_DmDuoc_Load(object sender, EventArgs e)
        {
            lupMaCC.Properties.DataSource = _data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            EnableButton(true);
            _lNhom = _data.NhomDVs.Where(p => p.Status == 1).ToList();
            var _tnhom = (from nhom in _data.NhomDVs.Where(p => p.Status == 1)
                          join tn in _data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                          select new { nhom.TenNhom, tn.TenTN, tn.IdTieuNhom, nhom.IDNhom }).ToList();
            lupNhom.Properties.DataSource = _tnhom.ToList();
            lupPhanLoai.Properties.DataSource = _lNhom;
            lupIDNhom.DataSource = _lNhom.ToList();
            luptieunhom.DataSource = _tnhom.ToList();
            _ldichvu = _data.DichVus.Where(p => p.PLoai == 1).ToList();
            grcDichVu.DataSource = _ldichvu;

            chkDY.Checked = false;
            panelControl2.Visible = false;

        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string _timkiem = "";
                if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Tìm Mã|Tên dược")
                    _timkiem = txtTimKiem.Text.ToLower();
                grcDichVu.DataSource = _ldichvu.Where(p => p.TenDV.ToLower().Contains(txtTimKiem.Text.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Tìm Mã|Tên dược")
                txtTimKiem.Text = "";
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            TTLuu = 1;
            EnableButton(false);
            resetcontrol();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Ktra())
            {
                switch (TTLuu)
                {
                    case 1:
                        try
                        {
                            DichVu _newDV = new DichVu();
                            _newDV.MaDV = Convert.ToInt32(txtMaDV.Text);
                            _newDV.TenDV = txtTenDV.Text.Trim();
                            _newDV.PLoai = 1;
                            _newDV.IdTieuNhom = Convert.ToInt32(lupNhom.EditValue);
                            _newDV.IDNhom = Convert.ToInt32(lupPhanLoai.EditValue);
                            if (!string.IsNullOrEmpty(txtDonGia.Text))
                                _newDV.DonGia = double.Parse(txtDonGia.Text);
                            _newDV.DonVi = cboDonVi.Text;
                            _newDV.HamLuong = txtHamLuong.Text;
                            _newDV.DuongD = txtDuongDung.Text;
                            _newDV.NuocSX = txtNuocSX.Text;
                            _newDV.TenHC = txtTenHC.Text;
                            _newDV.MaQD = txtMaQD.Text;
                            _newDV.SoDK = txtSoDK.Text;
                            _newDV.DangBC = txtDangBC.Text;
                            _newDV.NhaSX = txtNhaSX.Text;
                            _newDV.QCPC = txtQCPC.Text;
                            _newDV.MaCC = lupMaCC.EditValue.ToString();
                            _newDV.TrongDM = cboTrongDM.SelectedIndex;
                            if (chkStatus.Checked == true)
                                _newDV.Status = 1;
                            else
                                _newDV.Status = 0;
                            if (chkDY.Checked == true)
                                _newDV.DongY = 1;
                            else _newDV.DongY = 0;
                            _newDV.TinhTNhap = cboTTNhap.Text;
                            _newDV.NguonGoc = cboNguonGoc.Text;
                            if (!string.IsNullOrEmpty(txtTLHH.Text))
                                _newDV.TyLeSP = double.Parse(txtTLHH.Text);
                            if (!string.IsNullOrEmpty(txtTLBQ.Text))
                            {
                                _newDV.TyLeBQ = double.Parse(txtTLBQ.Text);
                            }
                            else _newDV.TyLeBQ = 0;
                            _newDV.BPDung = cboBPDung.Text;
                            _newDV.YCSD = cboYCSD.Text;
                            _data.DichVus.Add(_newDV);
                            if (_data.SaveChanges() >= 0)
                            {
                                MessageBox.Show("tạo mới thành công");
                                Frm_DmDuoc_Load(sender, e);
                                TTLuu = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi không tạo mới được: " + ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            var _suaDV = _data.DichVus.Single(p => p.MaDV == _madv);
                            _suaDV.MaDV = Convert.ToInt32(txtMaDV.Text.Trim());
                            _suaDV.TenDV = txtTenDV.Text.Trim();
                            _suaDV.PLoai = 1;
                            _suaDV.IdTieuNhom = Convert.ToInt32(lupNhom.EditValue);
                            _suaDV.IDNhom = Convert.ToInt32(lupPhanLoai.EditValue);
                            if (!string.IsNullOrEmpty(txtDonGia.Text))
                                _suaDV.DonGia = double.Parse(txtDonGia.Text);
                            _suaDV.DonVi = cboDonVi.Text;
                            _suaDV.HamLuong = txtHamLuong.Text;
                            _suaDV.DuongD = txtDuongDung.Text;
                            _suaDV.NuocSX = txtNuocSX.Text;
                            _suaDV.TenHC = txtTenHC.Text;
                            _suaDV.MaQD = txtMaQD.Text;
                            _suaDV.SoDK = txtSoDK.Text;
                            _suaDV.DangBC = txtDangBC.Text;
                            _suaDV.NhaSX = txtNhaSX.Text;
                            _suaDV.MaCC = lupMaCC.EditValue.ToString();
                            _suaDV.QCPC = txtQCPC.Text;
                            _suaDV.TrongDM = cboTrongDM.SelectedIndex;
                            if (chkStatus.Checked == true)
                                _suaDV.Status = 1;
                            else
                                _suaDV.Status = 0;
                            if (chkDY.Checked == true)
                                _suaDV.DongY = 1;
                            else _suaDV.DongY = 0;
                            _suaDV.TinhTNhap = cboTTNhap.Text;
                            _suaDV.NguonGoc = cboNguonGoc.Text;
                            if (!string.IsNullOrEmpty(txtTLHH.Text))
                                _suaDV.TyLeSP = double.Parse(txtTLHH.Text);
                            if (!string.IsNullOrEmpty(txtTLBQ.Text))
                            {
                                _suaDV.TyLeBQ = double.Parse(txtTLBQ.Text);
                            }
                            else { _suaDV.TyLeBQ = 0; }
                            _suaDV.BPDung = cboBPDung.Text;
                            _suaDV.YCSD = cboYCSD.Text;
                            if (_data.SaveChanges() >= 0)
                            //_data.SaveChanges();
                            {
                                TTLuu = 0;
                                MessageBox.Show("Sửa thành công");
                                Frm_DmDuoc_Load(sender, e);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không sửa được! " + ex.Message);
                        }
                        break;
                }
            }
        }

        private void txtMaDV_Leave(object sender, EventArgs e)
        {
            try
            {
                if (TTLuu == 1)
                {
                    int ot;
                    int _int_madv = 0;
                    if (Int32.TryParse(txtMaDV.Text, out ot))
                    {
                        _int_madv = Convert.ToInt32(txtMaDV.Text);

                        var kt = _data.DichVus.Where(p => p.MaDV == _int_madv).ToList();
                        if (kt.Count > 0)
                        {
                            MessageBox.Show("Mã dược đã tồn tại, hãy nhập mã khác!");
                            txtMaDV.Focus();
                            txtMaDV.SelectAll();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra mã dược: " + ex.Message);
            }
        }

        private void txtTenDV_Leave(object sender, EventArgs e)
        {
            try
            {
                int ot;
                int _int_madv = 0;
                if (Int32.TryParse(txtMaDV.Text, out ot))
                {
                    _int_madv = Convert.ToInt32(txtMaDV.Text);
                    var kt = _data.DichVus.Where(p => p.MaDV == _int_madv).ToList();
                    if (kt.Count > 0)
                    {
                        MessageBox.Show("Tên dược đã tồn tại, hãy nhập tên khác!");
                        txtTenDV.Focus();
                        txtTenDV.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra tên dược: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaDV.Properties.ReadOnly = true;
            txtMaDV.Properties.AllowFocused = false;
            if (!string.IsNullOrEmpty(txtMaDV.Text))
            {
                TTLuu = 2;
                EnableButton(false);
            }
            else
            {
                MessageBox.Show("không có dược để sửa");
            }
        }

        private void lupNhom_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (lupNhom.EditValue != null && lupNhom.EditValue.ToString() != "")
                lupPhanLoai.EditValue = lupNhom.GetColumnValue("IDNhom");
            else
                lupPhanLoai.EditValue = "";
        }

        private void grvDichVu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (grvDichVu.GetFocusedRowCellValue(colMaDV) != null)
            {
                _madv = Convert.ToInt32(grvDichVu.GetFocusedRowCellValue(colMaDV));
                txtMaDV.Text = _madv.ToString();
                txtTenDV.Text = _ldichvu.Where(p => p.MaDV== _madv).First().TenDV;
                txtTenHC.Text = _ldichvu.Where(p => p.MaDV== _madv).First().TenHC;
                if (_ldichvu.Where(p => p.MaDV== _madv).First().TrongDM != null)
                {
                    if (_ldichvu.Where(p => p.MaDV== _madv).First().TrongDM == 1)
                        cboTrongDM.SelectedIndex = _ldichvu.Where(p => p.MaDV== _madv).First().TrongDM.Value;
                }
                if (_ldichvu.Where(p => p.MaDV== _madv).First().Status != null)
                {
                    if (_ldichvu.Where(p => p.MaDV== _madv).First().Status == 1)
                        chkStatus.Checked = true;
                    else
                        chkStatus.Checked = false;
                }
                txtHamLuong.Text = _ldichvu.Where(p => p.MaDV== _madv).First().HamLuong;
                if (_ldichvu.Where(p => p.MaDV== _madv).First().IdTieuNhom != null && _ldichvu.Where(p => p.MaDV== _madv).First().IdTieuNhom.ToString() != "")
                    lupNhom.EditValue = _ldichvu.Where(p => p.MaDV== _madv).First().IdTieuNhom.Value;
                else
                    lupNhom.EditValue = -1;
                if (_ldichvu.Where(p => p.MaDV== _madv).First().IDNhom != null && _ldichvu.Where(p => p.MaDV== _madv).First().IDNhom.ToString() != "")
                    lupPhanLoai.EditValue = _ldichvu.Where(p => p.MaDV== _madv).First().IDNhom.Value;
                else
                    lupPhanLoai.EditValue = -1;
                txtMaQD.Text = _ldichvu.Where(p => p.MaDV== _madv).First().MaQD;
                txtQCPC.Text = _ldichvu.Where(p => p.MaDV== _madv).First().QCPC;
                txtNhaSX.Text = _ldichvu.Where(p => p.MaDV== _madv).First().NhaSX;
                txtDangBC.Text = _ldichvu.Where(p => p.MaDV== _madv).First().DangBC;
                txtNuocSX.Text = _ldichvu.Where(p => p.MaDV== _madv).First().NuocSX;
                txtDuongDung.Text = _ldichvu.Where(p => p.MaDV== _madv).First().DuongD;
                cboDonVi.Text = _ldichvu.Where(p => p.MaDV== _madv).First().DonVi;
                lupMaCC.EditValue = _ldichvu.Where(p => p.MaDV== _madv).First().MaCC;
                txtSoDK.Text = _ldichvu.Where(p => p.MaDV== _madv).First().SoDK;
                if (_ldichvu.Where(p => p.MaDV== _madv).First().DonGia != null)
                    txtDonGia.Text = _ldichvu.Where(p => p.MaDV== _madv).First().DonGia.ToString();
                else
                    txtDonGia.Text = "";
                if (_ldichvu.Where(p => p.MaDV== _madv).First().DongY != null && _ldichvu.Where(p => p.MaDV== _madv).First().DongY == 1)
                {
                    chkDY.Checked = true;
                }
                else { panelControl2.Visible = false; chkDY.Checked = false; }
                cboTTNhap.Text = _ldichvu.Where(p => p.MaDV== _madv).First().TinhTNhap;
                cboNguonGoc.Text = _ldichvu.Where(p => p.MaDV== _madv).First().NguonGoc;
                cboBPDung.Text = _ldichvu.Where(p => p.MaDV== _madv).First().BPDung;
                cboYCSD.Text = _ldichvu.Where(p => p.MaDV== _madv).First().YCSD;
                if (_ldichvu.Where(p => p.MaDV== _madv).First().TyLeSP != null)
                    txtTLHH.Text = _ldichvu.Where(p => p.MaDV== _madv).First().TyLeSP.ToString();
                else
                    txtTLHH.Text = "";
                if (_ldichvu.Where(p => p.MaDV== _madv).First().TyLeBQ != null)
                    txtTLBQ.Text = _ldichvu.Where(p => p.MaDV== _madv).First().TyLeBQ.ToString();
                else
                    txtTLBQ.Text = "";

            }
        }

        private void grvDichVu_DataSourceChanged(object sender, EventArgs e)
        {
            grvDichVu_FocusedRowChanged(null, null);

        }

        private void chkDY_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDY.Checked == true)
            {
                panelControl2.Visible = true;
            }
            else
            {
                panelControl2.Visible = false;
            }
        }

        private void lupMaCC_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnKluu_Click(object sender, EventArgs e)
        {
            TTLuu = 0;
            Frm_DmDuoc_Load(sender, e);
        }

        private void chkDY_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

            int ot;
            int _int_madv = 0;
            if (Int32.TryParse(txtMaDV.Text, out ot) && TTLuu == 2)
            {
                _int_madv = Convert.ToInt32(txtMaDV.Text);

                if (chkDY.Checked)
                {
                    var kt = _data.NhapDcts.Where(p => p.MaDV == _int_madv).ToList();
                    if (kt.Count > 0)
                    {
                        MessageBox.Show("thuốc đã được sử dụng, bạn không thể sửa");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void lupMaCC_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int ot;
            int _int_madv = 0;
            if (Int32.TryParse(txtMaDV.Text, out ot) && TTLuu == 2)
            {
                _int_madv = Convert.ToInt32(txtMaDV.Text);
                if (lupMaCC.EditValue != null && lupMaCC.EditValue.ToString() != "")
                {
                    var kt = _data.NhapDcts.Where(p => p.MaDV == _int_madv).ToList();
                    if (kt.Count > 0)
                    {
                        MessageBox.Show("thuốc đã được sử dụng, bạn không thể sửa nhà CC");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ot;
            int _int_madv = 0;
            if (Int32.TryParse(txtMaDV.Text, out ot))
            {
                _int_madv = Convert.ToInt32(txtMaDV.Text);
                var kt = _data.NhapDcts.Where(p => p.MaDV == _int_madv).ToList();
                if (kt.Count > 0)
                {
                    MessageBox.Show("thuốc đã được sử dụng, bạn không thể xóa");
                }
                else
                {
                    DialogResult _result = MessageBox.Show("Bạn muốn xóa thuốc: " + txtTenDV.Text, "Hỏi xóa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        var xoa = _data.DichVus.Single(p => p.MaDV == _int_madv);
                        _data.DichVus.Remove(xoa);
                        _data.SaveChanges();
                        Frm_DmDuoc_Load(sender, e);
                    }

                }
            }
        }

        private void Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}