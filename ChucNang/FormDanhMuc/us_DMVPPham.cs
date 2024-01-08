using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.FormNhap;

namespace QLBV.FormDanhMuc
{
    public partial class us_DMVPPham : DevExpress.XtraEditors.XtraUserControl
    {
        public us_DMVPPham()
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
            btn5084.Enabled = !t;
            //txtMaDV.Properties.ReadOnly = t;
            txtTenDV.Properties.ReadOnly = t;
            lupNhom.Properties.ReadOnly = t;
            lupMaCC.Properties.ReadOnly = t;
            lupPhanLoai.Properties.ReadOnly = true;
            cboDonVi.Properties.ReadOnly = t;
            txtNuocSX.Properties.ReadOnly = t;
            chkStatus.Properties.ReadOnly = t;
            cbo_NhaSX.Properties.ReadOnly = t;
            grcDichVu.Enabled = t;
            txtTyLe.Properties.ReadOnly = t;
            txtMaTam.Properties.ReadOnly = t;
            cklKP.Enabled = !t;


        }
        private void resetcontrol()
        {
            txtMaDV.Text = "";
            txtTenDV.Text = "";

            lupNhom.EditValue = "";
            lupPhanLoai.EditValue = "";
            lupMaCC.EditValue = "";
            cboDonVi.Text = "";
            txtNuocSX.Text = "";
            cbo_NhaSX.Text = "";
            chkStatus.Checked = true;
            txtMaTam.ResetText();
        }
        private bool Ktra()
        {
            int ot;
            int _int_MaDuoc = 0;
            if (Int32.TryParse(txtMaDV.Text, out ot))
                _int_MaDuoc = Convert.ToInt32(txtMaDV.Text);
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
            //}
            if (!string.IsNullOrEmpty(txtMaTam.Text))
            {
                if (TTLuu == 1)
                {
                    var ktra_matam = _data.DichVus.Where(p => p.MaTam == txtMaTam.Text.Trim()).ToList();
                    if (ktra_matam.Count > 0)
                    {
                        MessageBox.Show("Mã số đã tồn tại, hãy nhập mã khác");
                        txtMaTam.Focus();
                        txtMaTam.SelectAll();
                        return false;
                    }
                }
                if (TTLuu == 2)
                {
                    var ktra_matam = _data.DichVus.Where(p => p.MaTam == txtMaTam.Text.Trim() && p.MaDV != _int_MaDuoc).ToList();
                    if (ktra_matam.Count > 0)
                    {
                        MessageBox.Show("Mã số đã tồn tại, hãy nhập mã khác");
                        txtMaTam.Focus();
                        txtMaTam.SelectAll();
                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập mã nội bộ");
                txtMaTam.Focus();
                return false;
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
            return true;
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<NhomDV> _lNhom = new List<NhomDV>();
        List<usDichVu.KhoaPhong> _lKPsd = new List<usDichVu.KhoaPhong>();
        List<DuongDung> _lddung = new List<DuongDung>();
        private void us_dmDuoc_Load(object sender, EventArgs e)
        {
            //    try
            //    {
            lupMaCC.Properties.DataSource = _data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            //if (DungChung.Ham.checkQuyenFalse("us_dmDuoc")[0])
            EnableButton(true);
            //else
            //    EnableButton(false);
            List<string> _nhasx = _data.DonVis.Select(p => p.TenDonVi).ToList();
            cbo_NhaSX.Properties.Items.AddRange(_nhasx);
            _lKPsd = (from kp in _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc)
                      select new usDichVu.KhoaPhong()
                      {
                          Check = false,
                          MaKP = kp.MaKP,
                          TenKP = kp.TenKP
                      }).Distinct().OrderBy(p => p.TenKP).ToList();
            cklKP.DataSource = _lKPsd;
            _lNhom = _data.NhomDVs.Where(p => p.Status == 1).ToList();
            _lddung = _data.DuongDungs.OrderBy(p => p.DuongDung1).ToList();
            var _tnhom = (from nhom in _data.NhomDVs.Where(p => p.Status == 1)
                          join tn in _data.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                          select new { nhom.TenNhom, tn.TenTN, tn.IdTieuNhom, nhom.IDNhom }).ToList();
            //List<string> _lsDM5084 = new List<string>();
            //_lsDM5084 = _data.C5084.Where(p => p.Bang != 5).OrderBy(p => p.TenDV).Select(p=>p.TenDV).ToList();
            lupNhom.Properties.DataSource = _tnhom.ToList();
            lupPhanLoai.Properties.DataSource = _lNhom;
            lupIDNhom.DataSource = _lNhom.ToList();
            luptieunhom.DataSource = _tnhom.ToList();
            _ldichvu = _data.DichVus.Where(p => p.PLoai == 5).ToList();
            grcDichVu.DataSource = _ldichvu;
        }


        private void btnMoi_Click(object sender, EventArgs e)
        {
            //if (DungChung.Bien.MaBV == "12001")
            //{
            //    if (!DungChung.Ham.checkQuyenFalse("us_dmDuoc")[0])
            //    {
            //        MessageBox.Show("Bạn chưa được cấp quyền thêm mới thuốc, VTYT ! \nLiên hệ với admin để được cấp quyền");
            //        return;
            //    }
            //}
            TTLuu = 1;
            txtTyLe_EditValueChanged(null, null);
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
                        //try
                        {
                            DichVu _newDV = new DichVu();
                            _newDV.MaTam = txtMaTam.Text;
                            _newDV.TenDV = txtTenDV.Text.Trim();
                            _newDV.PLoai = 5;
                            _newDV.IdTieuNhom = Convert.ToInt32(lupNhom.EditValue);
                            _newDV.IDNhom = Convert.ToInt32(lupPhanLoai.EditValue);
                            _newDV.DonVi = cboDonVi.Text.Trim();
                            _newDV.DSDonGia = "0";
                            _newDV.NuocSX = txtNuocSX.Text.Trim();
                            _newDV.TenRG = txtTenDV.Text.Trim();
                            _newDV.NhaSX = cbo_NhaSX.Text.Trim();
                            _newDV.GhiChu = ";;;;";
                            if (chkStatus.Checked == true)
                                _newDV.Status = 1;
                            else
                                _newDV.Status = 0;
                            int _soLuongMin = 0;
                            if (lupMaCC.EditValue != null)
                                _newDV.MaCC = lupMaCC.EditValue.ToString();
                            _newDV.SLMin = _soLuongMin;
                            _newDV.DonViN = cboDonVi.Text.Trim();
                            string[] tyle = new string[2] { "1", "1" };
                            tyle = QLBV_Library.QLBV_Ham.LayChuoi('/', txtTyLe.Text);
                            _newDV.TyLeSD = Convert.ToInt32(tyle[1]);
                            string _makpsd = ";";
                            for (int i = 0; i < cklKP.ItemCount; i++)
                            {
                                if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                                    _makpsd += cklKP.GetItemValue(i) + ";";
                            }

                            _newDV.MaKPsd = _makpsd;
                            _data.DichVus.Add(_newDV);
                            bool ktluu = false;
                            try
                            {
                                _data.SaveChanges();
                                ktluu = true;
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                ktluu = false;
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
                            if (ktluu)
                            {
                                MessageBox.Show("Thêm mới thành công");
                                timkiem();
                                TTLuu = 0;
                                //if (DungChung.Ham.checkQuyenFalse("us_dmDuoc")[0])
                                EnableButton(true);
                                //else
                                //    EnableButton(false);
                            }
                        }
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show("Lỗi không tạo mới được: " + ex.Message);
                        //}
                        break;
                    case 2:
                        try
                        {
                            var _suaDV = _data.DichVus.Single(p => p.MaDV == _madv);
                            _suaDV.MaTam = txtMaTam.Text;
                            _suaDV.TenDV = txtTenDV.Text.Trim();
                            _suaDV.TenRG = txtTenDV.Text.Trim();
                            _suaDV.PLoai = 5;
                            _suaDV.IdTieuNhom = Convert.ToInt32(lupNhom.EditValue);
                            _suaDV.IDNhom = Convert.ToInt32(lupPhanLoai.EditValue);
                            _suaDV.DonVi = cboDonVi.Text;
                            _suaDV.NuocSX = txtNuocSX.Text.Trim();
                            _suaDV.NhaSX = cbo_NhaSX.Text.Trim();
                            if (lupMaCC.EditValue != null)
                                _suaDV.MaCC = lupMaCC.EditValue.ToString();
                            if (chkStatus.Checked == true)
                                _suaDV.Status = 1;
                            else
                                _suaDV.Status = 0;
                            _suaDV.DonViN = cboDonVi.Text.Trim();
                            string[] tyle = new string[2] { "1", "1" };
                            tyle = QLBV_Library.QLBV_Ham.LayChuoi('/', txtTyLe.Text);
                            _suaDV.TyLeSD = Convert.ToInt32(tyle[1]);
                            string _makpsd = ";";
                            for (int i = 0; i < cklKP.ItemCount; i++)
                            {
                                if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                                    _makpsd += cklKP.GetItemValue(i) + ";";
                            }

                            _suaDV.MaKPsd = _makpsd;
                            if (_data.SaveChanges() >= 0)
                            //_data.SaveChanges();
                            {
                                TTLuu = 0;
                                MessageBox.Show("Sửa thành công");
                                timkiem();
                                //if (DungChung.Ham.checkQuyenFalse("us_dmDuoc")[0])
                                EnableButton(true);
                                //else
                                //    EnableButton(true);
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
            int ot;
            int _int_MmaDV = 0;
            if (Int32.TryParse(txtMaDV.Text, out ot))
                _int_MmaDV = Convert.ToInt32(txtMaDV.Text);
            try
            {
                if (TTLuu == 1)
                {
                    if (_int_MmaDV > 0)
                    {
                        var kt = _data.DichVus.Where(p => p.MaDV == _int_MmaDV).ToList();
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

            int ot;
            int _int_MmaDV = 0;
            if (Int32.TryParse(txtMaDV.Text, out ot))
                _int_MmaDV = Convert.ToInt32(txtMaDV.Text);
            try
            {
                if (_int_MmaDV > 0)
                {
                    var kt = _data.DichVus.Where(p => p.MaDV == _int_MmaDV).ToList();
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
            //if (DungChung.Bien.MaBV == "12001")
            //{
            //    if (!DungChung.Ham.checkQuyenFalse("us_dmDuoc")[1])
            //    {
            //        MessageBox.Show("Bạn chưa được cấp quyền sửa thuốc, VTYT ! \nLiên hệ với admin để được cấp quyền");
            //        return;
            //    }
            //}
            int ot;
            int _int_MmaDV = 0;
            if (Int32.TryParse(txtMaDV.Text, out ot))
                _int_MmaDV = Convert.ToInt32(txtMaDV.Text);
            txtMaDV.Properties.ReadOnly = true;
            txtMaDV.Properties.AllowFocused = false;
            if (_int_MmaDV > 0)
            {
                TTLuu = 2;
                txtTyLe_EditValueChanged(null, null);
                EnableButton(false);
            }
            else
            {
                MessageBox.Show("không có dược để sửa");
            }
        }

        private void lupNhom_EditValueChanged(object sender, EventArgs e)
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
                if (_ldichvu.Where(p => p.MaDV == _madv).Count() > 0)
                {
                    txtMaTam.Text = _ldichvu.Where(p => p.MaDV == _madv).First().MaTam;
                    txtMaDV.Text = _madv.ToString();
                    txtTenDV.Text = _ldichvu.Where(p => p.MaDV == _madv).First().TenDV;
                    if (_ldichvu.Where(p => p.MaDV == _madv).First().Status != null)
                    {
                        if (_ldichvu.Where(p => p.MaDV == _madv).First().Status == 1)
                            chkStatus.Checked = true;
                        else
                            chkStatus.Checked = false;
                    }
                    if (_ldichvu.Where(p => p.MaDV == _madv).First().IdTieuNhom != null && _ldichvu.Where(p => p.MaDV == _madv).First().IdTieuNhom.ToString() != "")
                        lupNhom.EditValue = _ldichvu.Where(p => p.MaDV == _madv).First().IdTieuNhom.Value;
                    else
                        lupNhom.EditValue = -1;
                    if (_ldichvu.Where(p => p.MaDV == _madv).First().IDNhom != null && _ldichvu.Where(p => p.MaDV == _madv).First().IDNhom.ToString() != "")
                        lupPhanLoai.EditValue = _ldichvu.Where(p => p.MaDV == _madv).First().IDNhom.Value;
                    else
                        lupPhanLoai.EditValue = -1;
                    lupMaCC.EditValue = _ldichvu.Where(p => p.MaDV == _madv).First().MaCC;
                    cbo_NhaSX.Text = _ldichvu.Where(p => p.MaDV == _madv).First().NhaSX;
                    txtNuocSX.Text = _ldichvu.Where(p => p.MaDV == _madv).First().NuocSX;
                    cboDonVi.Text = _ldichvu.Where(p => p.MaDV == _madv).First().DonVi;
                    lupMaCC.EditValue = _ldichvu.Where(p => p.MaDV == _madv).First().MaCC;
                    cboDonViN.Text = _ldichvu.Where(p => p.MaDV == _madv).First().DonViN;
                    txtTyLe.Text = "1/" + _ldichvu.Where(p => p.MaDV == _madv).First().TyLeSD;
                    usDichVu._loadKPsd(_ldichvu.Where(p => p.MaDV == _madv).First().MaKPsd, _lKPsd, cklKP);
                }
            }
            else
            {
                resetcontrol();
            }

        }
        class dsgia
        {
            string gia;

            public string Gia
            {
                get { return gia; }
                set { gia = value; }
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            //if (DungChung.Bien.MaBV == "12001")
            //{
            //    if (!DungChung.Ham.checkQuyenFalse("us_dmDuoc")[2])
            //    {
            //        MessageBox.Show("Bạn chưa được cấp quyền xóa thuốc, VTYT ! \nLiên hệ với admin để được cấp quyền");
            //        return;
            //    }
            //}
            int ot;
            int madv = 0;
            if (Int32.TryParse(txtMaDV.Text, out ot))
                madv = Convert.ToInt32(txtMaDV.Text);
            var kt = _data.NhapDcts.Where(p => p.MaDV == madv).ToList();
            if (kt.Count > 0)
            {
                MessageBox.Show("thuốc đã được sử dụng, bạn không thể xóa");
            }
            else
            {
                DialogResult _result = MessageBox.Show("Bạn muốn xóa thuốc: " + txtTenDV.Text, "Hỏi xóa!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    var xoa = _data.DichVus.Single(p => p.MaDV == madv);
                    _data.DichVus.Remove(xoa);
                    _data.SaveChanges();
                    us_dmDuoc_Load(sender, e);
                }

            }

        }

        private void grvDichVu_DataSourceChanged(object sender, EventArgs e)
        {

            grvDichVu_FocusedRowChanged(null, null);
        }

        private void chkDY_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void lupMaCC_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void btnKluu_Click(object sender, EventArgs e)
        {
            TTLuu = 0;
            us_dmDuoc_Load(sender, e);

        }

        private void chkDY_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
          
        }

        private void txtTenDV_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateCV5084_Click(object sender, EventArgs e)
        {

        }

        private void btnKhoaPhong_Click(object sender, EventArgs e)
        {

        }
        public void _setValue(string tendv, string maqd, int sott, string tenhc, string sodk, string nuocsx, string duongdung, int trongdm, string hamluong, string baoche, string qcpc, string tchuan, string tuoitho, string nhasx, string nguongoc)
        {
            txtTenDV.Text = tendv;
            txtNuocSX.Text = nuocsx;
            cbo_NhaSX.Text = nhasx;
        }

        private void btn5084_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void lup_MaDuongD_EditValueChanged(object sender, EventArgs e)
        {

        }
        void timkiem()
        {
            try
            {

                string _timkiem = "";
                if (!string.IsNullOrEmpty(txtTimKiem.Text) && txtTimKiem.Text != "Tìm kiếm")
                    _timkiem = txtTimKiem.Text;
                _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                _ldichvu.Clear();
                _ldichvu = _data.DichVus.Where(p => p.PLoai == 5).ToList();
                grcDichVu.DataSource = _ldichvu.Where(p => p.TenDV.ToLower().Contains(_timkiem.ToLower()) || p.SoDK == _timkiem || p.MaTam == _timkiem).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            timkiem();
        }

        private void btnUpdateDichVuEx_Click(object sender, EventArgs e)
        {

        }

        private void btnDongiaDV_Click(object sender, EventArgs e)
        {

        }

        private void txtTyLe_EditValueChanged(object sender, EventArgs e)
        {
            if (TTLuu == 1 || TTLuu == 2)
            {
                if (txtTyLe.Text.Trim() == "1/1")
                {
                    //cboDonViN.SelectedIndex = cboDonVi.SelectedIndex;
                    cboDonViN.Properties.ReadOnly = true;
                }
                else
                {
                    cboDonViN.Properties.ReadOnly = false;
                }
            }
        }

        private void cboDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TTLuu == 1 || TTLuu == 2)
            {
                if (txtTyLe.Text.Trim() == "1/1")
                {
                    cboDonViN.Text = cboDonVi.Text;
                }
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (TTLuu == 1 || TTLuu == 2)
            {
                if (txtTyLe.Text.Trim() == "1/1")
                {
                    cboDonViN.Text = cboDonVi.Text;
                }
            }
        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

        }



    }
}
