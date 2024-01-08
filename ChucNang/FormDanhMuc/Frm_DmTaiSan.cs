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
    public partial class Frm_DmTaiSan : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DmTaiSan()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        int TTLuu = 0;
        int _madv = 0;
        List<DichVu> _ldichvu = new List<DichVu>();
        private void EnableButton(bool t)
        {
            btnLuu.Enabled = !t;
            btnMoi.Enabled = t;
            btnSua.Enabled = t;
            btnXoa.Enabled = t;
            txtMaQD.Properties.ReadOnly = t;
            txtMaDV.Properties.ReadOnly = t;
            txtTenDV.Properties.ReadOnly = t;
            txtDonGia.Properties.ReadOnly = t;
            lupNhom.Properties.ReadOnly = t;
            lupMaCC.Properties.ReadOnly = t;
            lupPhanLoai.Properties.ReadOnly = true;
            cboDonVi.Properties.ReadOnly = t;
            txtNuocSX.Properties.ReadOnly = t;
            chkStatus.Properties.ReadOnly = t;
            txtSoDK.Properties.ReadOnly = t;
            txtNhaSX.Properties.ReadOnly = t;
            txtQCPC.Properties.ReadOnly = t;
           grcDichVu.Enabled = t;

        }
        private void resetcontrol()
        {
            txtMaDV.Text = "";
            txtTenDV.Text = "";
            txtDonGia.Text = "";
            lupNhom.EditValue = "";
            lupPhanLoai.EditValue = "";
            cboDonVi.Text = "";
            txtNuocSX.Text = "";
            txtSoDK.Text = "";
            txtNhaSX.Text = "";
            txtQCPC.Text = "";
            txtMaQD.ResetText();
            chkStatus.Checked = true;
         }
        private bool Ktra()
        {
            int ot;
            int _int_madv = 0;

            //if (string.IsNullOrEmpty(txtMaDV.Text))
            //{
            //    MessageBox.Show("Bạn chưa nhập mã tài sản");
            //    txtMaDV.Focus();
            //    return false;
            //}

            //else if (!Int32.TryParse(txtMaDV.Text, out ot))
            //{
            //    MessageBox.Show("Mã tài sản không hợp lệ");
            //    txtMaDV.Focus();
            //    return false;
            ////}

            //else
            //{
            //    _int_madv = Convert.ToInt32(txtMaDV.Text);
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
                    DialogResult _result = MessageBox.Show("Tài sản: " + _timkiem + " đã có, bạn vẫn muốn thêm mới?", "Hỏi lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.No)
                    {
                        txtTenDV.Focus();
                        txtTenDV.SelectAll();
                        return false;
                    }
                }

            }
            //}
            if (string.IsNullOrEmpty(txtTenDV.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên tài sản");
                txtTenDV.Focus();
                return false;
            }
            //if (lupNhom.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn tiểu nhóm tài sản");
            //    lupNhom.Focus();
            //    return false;
            //}
            //if (lupPhanLoai.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn nhóm tài sản");
            //    lupPhanLoai.Focus();
            //    return false;
            //}
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
           
         return true;
        }
         List<NhomDV> _lNhom = new List<NhomDV>();
        private void Frm_DmTaiSan_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            lupMaCC.Properties.DataSource = _data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            EnableButton(true);
            _lNhom = _data.NhomDVs.ToList();
            var _tnhom = (from nhom in _data.NhomDVs.Where(p=>p.TenNhom.Contains("sản"))
                          join tn in _data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                          select new { nhom.TenNhom, tn.TenTN, tn.IdTieuNhom, nhom.IDNhom }).ToList();
            lupNhom.Properties.DataSource = _tnhom.ToList();
            lupPhanLoai.Properties.DataSource = _lNhom;
            lupIDNhom.DataSource = _lNhom.ToList();
            luptieunhom.DataSource = _tnhom.ToList();
            _ldichvu = _data.DichVus.Where(p => p.PLoai == 4).ToList();
            grcDichVu.DataSource = _ldichvu;

        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            TTLuu = 1;
            EnableButton(false);
            resetcontrol();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (Ktra())
            {
                
                switch (TTLuu)
                {
                    case 1:
                        try
                        {
                              
                            DichVu _newDV = new DichVu();
                            //_newDV.MaDV = _int_madv;
                            _newDV.TenDV = txtTenDV.Text.Trim();
                            _newDV.PLoai = 4;
                            _newDV.IdTieuNhom = Convert.ToInt32(lupNhom.EditValue);
                            _newDV.IDNhom = Convert.ToInt32(lupPhanLoai.EditValue);
                            _newDV.DSDonGia = "0";
                            if (!string.IsNullOrEmpty(txtDonGia.Text))
                                _newDV.DonGia = double.Parse(txtDonGia.Text);
                            _newDV.DonVi = cboDonVi.Text;
                            _newDV.NuocSX = txtNuocSX.Text;
                            _newDV.SoDK = txtSoDK.Text;
                            _newDV.NhaSX = txtNhaSX.Text;
                            _newDV.QCPC = txtQCPC.Text;
                            _newDV.MaCC = lupMaCC.EditValue.ToString();
                            _newDV.MaQD = txtMaQD.Text.Trim();
                            if (chkStatus.Checked == true)
                                _newDV.Status = 1;
                            else
                                _newDV.Status = 0;
                            _data.DichVus.Add(_newDV);
                            if (_data.SaveChanges() >= 0)
                            {
                                MessageBox.Show("tạo mới thành công");
                                Frm_DmTaiSan_Load(sender, e);
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
                            int _int_madv = Convert.ToInt32(txtMaDV.Text);
                            var _suaDV = _data.DichVus.Single(p => p.MaDV== _madv);
                            //_suaDV.MaDV =_int_madv;
                            _suaDV.TenDV = txtTenDV.Text.Trim();
                            _suaDV.PLoai = 4;
                            _suaDV.IdTieuNhom = Convert.ToInt32(lupNhom.EditValue);
                            _suaDV.IDNhom = Convert.ToInt32(lupPhanLoai.EditValue);
                            if (!string.IsNullOrEmpty(txtDonGia.Text))
                                _suaDV.DonGia = double.Parse(txtDonGia.Text);
                            _suaDV.DonVi = cboDonVi.Text;
                             _suaDV.NuocSX = txtNuocSX.Text;
                             _suaDV.SoDK = txtSoDK.Text;
                             _suaDV.NhaSX = txtNhaSX.Text;
                            _suaDV.MaCC = lupMaCC.EditValue.ToString();
                            _suaDV.MaQD = txtMaQD.Text.Trim();
                            if (chkStatus.Checked == true)
                                _suaDV.Status = 1;
                            else
                                _suaDV.Status = 0;
                            _suaDV.QCPC = txtQCPC.Text;
                         if (_data.SaveChanges() >= 0)
                            //_data.SaveChanges();
                            {
                                TTLuu = 0;
                                MessageBox.Show("Sửa thành công");
                                Frm_DmTaiSan_Load(sender, e);
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            //txtMaDV.Properties.ReadOnly = true;
            //txtMaDV.Properties.AllowFocused = false;
            if (!string.IsNullOrEmpty(txtMaDV.Text))
            {
                TTLuu = 2;
                EnableButton(false);
            }
            else
            {
                MessageBox.Show("không có tài sản để sửa");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (!string.IsNullOrEmpty(txtMaDV.Text))
            {

                int ot;
                int madv = 0;
                if (Int32.TryParse(txtMaDV.Text, out ot))
                    madv = Convert.ToInt32(txtMaDV.Text);
                var kt = _data.NhapDcts.Where(p => p.MaDV== madv).ToList();
                if (kt.Count > 0)
                {
                    MessageBox.Show("Tài sản đã được sử dụng, bạn không thể xóa");
                }
                else
                {
                    DialogResult _result = MessageBox.Show("Bạn muốn xóa tài sản: " + txtTenDV.Text, "Hỏi xóa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        var xoa = _data.DichVus.Single(p => p.MaDV== madv);
                        _data.DichVus.Remove(xoa);
                        _data.SaveChanges();
                        Frm_DmTaiSan_Load(sender, e);
                    }

                }
            }
        }

        private void btnKluu_Click(object sender, EventArgs e)
        {
            TTLuu = 0;
            Frm_DmTaiSan_Load(sender, e);
        }

        private void Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvDichVu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDichVu.GetFocusedRowCellValue(colMaDV) != null )
            {
                _madv  =Convert.ToInt32( grvDichVu.GetFocusedRowCellValue(colMaDV));
                txtMaDV.Text = _madv.ToString();
                txtTenDV.Text = _ldichvu.Where(p => p.MaDV== _madv).First().TenDV;
                if (_ldichvu.Where(p => p.MaDV== _madv).First().Status != null)
                {
                    if (_ldichvu.Where(p => p.MaDV== _madv).First().Status == 1)
                        chkStatus.Checked = true;
                    else
                        chkStatus.Checked = false;
                }
                 if (_ldichvu.Where(p => p.MaDV== _madv).First().IdTieuNhom != null && _ldichvu.Where(p => p.MaDV== _madv).First().IdTieuNhom.ToString() != "")
                    lupNhom.EditValue = _ldichvu.Where(p => p.MaDV==_madv).First().IdTieuNhom.Value;
                else
                    lupNhom.EditValue = -1;
                if (_ldichvu.Where(p => p.MaDV== _madv).First().IDNhom != null && _ldichvu.Where(p => p.MaDV== _madv).First().IDNhom.ToString() != "")
                    lupPhanLoai.EditValue = _ldichvu.Where(p => p.MaDV==_madv).First().IDNhom.Value;
                else
                    lupPhanLoai.EditValue = -1;
                txtMaQD.Text= _ldichvu.Where(p => p.MaDV == _madv).First().MaQD;
                txtQCPC.Text = _ldichvu.Where(p => p.MaDV== _madv).First().QCPC;
                txtNhaSX.Text = _ldichvu.Where(p => p.MaDV== _madv).First().NhaSX;
                txtNuocSX.Text = _ldichvu.Where(p => p.MaDV== _madv).First().NuocSX;
                 cboDonVi.Text = _ldichvu.Where(p => p.MaDV== _madv).First().DonVi;
                lupMaCC.EditValue = _ldichvu.Where(p => p.MaDV== _madv).First().MaCC;
                txtSoDK.Text = _ldichvu.Where(p => p.MaDV== _madv).First().SoDK;
                if (_ldichvu.Where(p => p.MaDV== _madv).First().DonGia != null)
                    txtDonGia.Text = _ldichvu.Where(p => p.MaDV== _madv).First().DonGia.ToString();
                else
                    txtDonGia.Text = "";
           
            }
        }

        private void grvDichVu_DataSourceChanged(object sender, EventArgs e)
        {
            grvDichVu_FocusedRowChanged(null, null);
    
        }

   
        private void lupMaCC_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (!string.IsNullOrEmpty(txtMaDV.Text) && TTLuu == 2)
            {

                int ot;
                int madv = 0;
                if (Int32.TryParse(txtMaDV.Text, out ot))
                    madv = Convert.ToInt32(txtMaDV.Text);
                if (lupMaCC.EditValue != null && lupMaCC.EditValue.ToString() != "")
                {
                    var kt = _data.NhapDcts.Where(p => p.MaDV== madv).ToList();
                    if (kt.Count > 0)
                    {
                        MessageBox.Show("Tài sản đã được sử dụng, bạn không thể sửa nhà CC");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void lupNhom_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            //if (lupNhom.EditValue != null && lupNhom.EditValue.ToString() != "")
            //    lupPhanLoai.EditValue = lupNhom.GetColumnValue("IDNhom");
            //else
            //    lupPhanLoai.EditValue = "";
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string _timkiem = "";
                if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Tìm Mã|Tên tài sản")
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
            if (txtTimKiem.Text == "Tìm Mã|Tên tài sản")
                txtTimKiem.Text = "";
        }

        private void txtMaDV_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    if (TTLuu == 1)
            //    {
            //        int ot;
            //        int _int_madv = 0;
            //        if (Int32.TryParse(txtMaDV.Text, out ot))
            //        {
            //            _int_madv = Convert.ToInt32(txtMaDV.Text);
                    
            //            var kt = _data.DichVus.Where(p => p.MaDV== _int_madv).ToList();
            //            if (kt.Count > 0)
            //            {
            //                MessageBox.Show("Mã TS đã tồn tại, hãy nhập mã khác!");
            //                txtMaDV.Focus();
            //                txtMaDV.SelectAll();
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi kiểm tra mã TS: " + ex.Message);
            //}
        }

        private void txtTenDV_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(txtTenDV.Text))
            //    {
            //        var kt = _data.DichVus.Where(p => p.TenDV== (txtTenDV.Text.Trim())).ToList();
            //        if (kt.Count > 0)
            //        {
            //            MessageBox.Show("Tên TS đã tồn tại, hãy nhập tên khác!");
            //            txtTenDV.Focus();
            //            txtTenDV.SelectAll();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi kiểm tra tên TS: " + ex.Message);
            //}
        }

        private void lupNhom_EditValueChanged(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if(lupNhom.EditValue!=null)
            {
                int idtieunhom = Convert.ToInt32(lupNhom.EditValue);
                var idnhom = _data.TieuNhomDVs.Where(p => p.IdTieuNhom == idtieunhom).Select(p => p.IDNhom).FirstOrDefault();
                if (idnhom != null)
                    lupPhanLoai.EditValue = idnhom;
            }
        }

        private void lupPhanLoai_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}