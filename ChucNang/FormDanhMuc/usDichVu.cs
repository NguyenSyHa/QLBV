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


namespace QLBV.FormDanhMuc
{
    public partial class usDichVu : DevExpress.XtraEditors.XtraUserControl
    {
        public usDichVu()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DichVu> _ldv = new List<DichVu>();
        int TTLuu = 0;
        public struct st_PhanLoaiSieuAm
        {
            public static string Sa2D = "2D";
            public static string SaMau = "Màu";
            public static string Sa3D_4D = "3D-4D";
        }
        public struct st_PhanLoaiPhim
        {
            public static string CP13_18 = "13/18";
            public static string CP18_24 = "18/24";
            public static string CP24_30 = "24/30";
            public static string CP30_40 = "30/40";
        }
        private void EnableButton(bool t)
        {
            btnLuu.Enabled = !t;
            btnMoi.Enabled = t;
            btnSua.Enabled = t;
            btnXoa.Enabled = t;
            cboPLoai.Properties.ReadOnly = t;
            txtMaDV.Properties.ReadOnly = t;
            txtPTram.Properties.ReadOnly = t;
            txtTenDV.Properties.ReadOnly = t;
            txtDonGia.Properties.ReadOnly = t;
            chkKTC.Properties.ReadOnly = t;
            chkStatus.Properties.ReadOnly = t;
            lupNhom.Properties.ReadOnly = t;
            cboTrongDM.Properties.ReadOnly = t;
            lupPhanLoai.Properties.ReadOnly = true;
            cboDonVi.Properties.ReadOnly = t;
            mmCanhBao.Properties.ReadOnly = t;
            chkStatus.Properties.ReadOnly = t;
            txtTenRG.Properties.ReadOnly = t;
            txtMaQD.Properties.ReadOnly = t;
            txtSoTT.Properties.ReadOnly = t;
            txtDonGia2.Properties.ReadOnly = t;
            txtGiaDV2.Properties.ReadOnly = t;
            cboLoai.Properties.ReadOnly = t;
            txtSoTT_BYT.Properties.ReadOnly = t;
            txtMaTam.Properties.ReadOnly = t;
            txtGiaBHTT.Properties.ReadOnly = t;
            txtdongiatt15.Properties.ReadOnly = t;
            txtGiaTT39.Properties.ReadOnly = t;
            txtGiaPhuThu.Properties.ReadOnly = t;
            txtGiaBHGioiHanTT.Properties.ReadOnly = t;
            txtMaPTTTQT.Properties.ReadOnly = t;
            txtHanMuc.Properties.ReadOnly = t;
            grcDichVu.Enabled = t;
            cklKP.Enabled = !t;
            chkCheckAll.Enabled = !t;
            txtSoQD.Properties.ReadOnly = t;
            chkCLS.Properties.ReadOnly = t;
            txtGHCD.Properties.ReadOnly = t;
            txtQCPC.Properties.ReadOnly = t;
            txtTenKhac.Properties.ReadOnly = t;
            txtPhuongPhap.Properties.ReadOnly = t;
            txtSoPhutTH.Properties.ReadOnly = t;
            chkAutoExcecute.Properties.ReadOnly = t;
            txtMaNhom5937.Properties.ReadOnly = t;
            cboPLTT.Properties.ReadOnly = t;
        }
        private void resetcontrol()
        {
            txtMaDV.Text = "";
            txtMaQD.Text = "";
            txtTenDV.Text = "";
            txtDonGia.Text = "";
            txtGiaBHTT.ResetText();
            txtdongiatt15.ResetText();
            txtGiaTT39.ResetText();
            txtDonGia2.ResetText();
            txtGiaDV2.ResetText();
            txtGiaPhuThu.ResetText();
            lupPhanLoai.EditValue = "-1";
            cboDonVi.Text = "";
            mmCanhBao.Text = "";
            cboTrongDM.SelectedIndex = -1;
            chkStatus.Checked = true;
            chkKTC.Checked = false;
            chkStatus.Checked = true;
            chkAutoExcecute.Checked = false;
            txtSoPhutTH.Text = "";
            cboLoai.Text = "";
            txtGiaBHGioiHanTT.Text = "";
            txtSoQD.ResetText();
            txtMaPTTTQT.ResetText();
            txtHanMuc.ResetText();
            chkCLS.Checked = false;
            txtGHCD.ResetText();
            txtQCPC.ResetText();
            txtPhuongPhap.ResetText();
            txtTenKhac.ResetText();
            txtMaNhom5937.ResetText();
            cboPLTT.SelectedIndex = -1;
        }

        private bool Ktra()
        {

            //int ot;
            //       int _int_madv = 0;
            //       if (Int32.TryParse(txtMaDV.Text, out ot))
            //       {
            //           _int_madv = Convert.ToInt32(txtMaDV.Text);
            //       }

            //   if (string.IsNullOrEmpty(txtMaDV.Text))
            //   {
            //       MessageBox.Show("Bạn chưa nhập mã ");
            //       txtMaDV.Focus();
            //       return false;
            //   }
            //   if (TTLuu == 1)
            //   {
            //       if(_int_madv > 0)
            //       {

            //           var ktra = _dataContext.DichVus.Where(p =>txtMaDV.Text != "" && p.MaDV == _int_madv).ToList();
            //           if (ktra.Count > 0)
            //           {
            //               MessageBox.Show("Mã số đã tồn tại, hãy nhập mã khác");
            //               txtMaDV.Focus();
            //               txtMaDV.SelectAll();
            //               return false;
            //           }
            //       }
            //       else
            //       {
            //           MessageBox.Show("Ma DV chua dung dinh dang");// saitext
            //           txtMaDV.Focus();
            //           txtMaDV.SelectAll();
            //           return false;
            //       }
            //   }
            //   if (!string.IsNullOrEmpty(txtMaTam.Text))
            //   {
            //       if (TTLuu == 1)
            //       {
            //           var ktra_matam = _dataContext.DichVus.Where(p => p.MaTam == txtMaTam.Text.Trim()).ToList();
            //           if (ktra_matam.Count > 0)
            //           {
            //               MessageBox.Show("Mã số đã tồn tại, hãy nhập mã khác");
            //               txtMaTam.Focus();
            //               txtMaTam.SelectAll();
            //               return false;
            //           }
            //       }
            //       if (TTLuu == 2)
            //       {

            //           var ktra_matam = _dataContext.DichVus.Where(p => p.MaTam == txtMaTam.Text.Trim() && p.MaDV != _int_madv ).ToList();
            //           if (ktra_matam.Count > 0)
            //           {
            //               MessageBox.Show("Mã số đã tồn tại, hãy nhập mã khác");
            //               txtMaTam.Focus();
            //               txtMaTam.SelectAll();
            //               return false;
            //           }
            //       } 
            //   }
            if (string.IsNullOrEmpty(txtTenDV.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên");
                txtTenDV.Focus();
                return false;
            }
            if (lupPhanLoai.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn nhóm dịch vụ");
                lupPhanLoai.Focus();
                return false;
            }
            if (lupNhom.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn tiểu nhóm");
                lupNhom.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboDonVi.Text))
            {
                MessageBox.Show("Bạn chưa chọn đơn vị");
                cboDonVi.Focus();
                return false;
            }
            if (lupPhanLoai.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn nhóm dịch vụ");
                lupPhanLoai.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDonGia.Text))
            {
                MessageBox.Show("Bạn chưa nhập đơn giá");
                cboDonVi.Focus();
                return false;
            }
            //int loai=0;
            //if (!int.TryParse(cboLoai.Text,out loai))
            //{
            //    MessageBox.Show("Loại|số lượng phim phải là số nguyên");
            //    cboLoai.Focus();
            //    return false;
            //}
            //if (loai < 0) {
            //    MessageBox.Show("Loại|số lượng phim phải >=0");
            //    cboLoai.Focus();
            //    return false;
            //}
            return true;
        }
        List<NhomDV> _lNhom = new List<NhomDV>();
        //bool _Them = true, _Sua = true, _Xoa = true;
        private void usDichVu_Load(object sender, EventArgs e)
        {

            if (DungChung.Ham.checkQuyenFalse("usDichVu")[0])
                EnableButton(true);
            else
                EnableButton(false);
            if (DungChung.Bien.MaBV == "01071" && DungChung.Bien.MaBV == "01049")
            {
                txtGiaPhuThu.Visible = true;
                labelControl19.Visible = true;
                this.groupControl3.Size = new System.Drawing.Size(260, 84);
            }

            _lNhom = _dataContext.NhomDVs.Where(p => p.Status == 2).ToList();
            var _tnhom = (from nhom in _dataContext.NhomDVs.Where(p => p.Status == 2)
                          join tn in _dataContext.TieuNhomDVs on nhom.IDNhom equals tn.IDNhom
                          select new { nhom.TenNhom, tn.TenTN, tn.IdTieuNhom, nhom.IDNhom, tn.TenRG }).OrderBy(p => p.TenTN).ToList();
            lupNhom.Properties.DataSource = _tnhom.ToList();
            lupIDNhom.DataSource = _lNhom.ToList();
            lupTimTN.Properties.DataSource = _tnhom;
            lupTNhomgv.DataSource = _dataContext.TieuNhomDVs.ToList();
            lupTimNhomDV.Properties.DataSource = _lNhom.ToList();
            lupPhanLoai.Properties.DataSource = _lNhom.ToList();
            _lKPsd = (from kp in _dataContext.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.HanhChinh)
                      select new KhoaPhong()
                      {
                          Check = false,
                          MaKP = kp.MaKP,
                          TenKP = kp.TenKP
                      }).Distinct().OrderBy(p => p.TenKP).ToList();
            cklKP.DataSource = _lKPsd;
            _lDTBN = (from dt in _dataContext.DTBNs select new Dtuong { Check = false, IDDTBN = dt.IDDTBN, DTBN = dt.DTBN1 }).OrderBy(p => p.DTBN).ToList();
            cklDTBN.DataSource = _lDTBN;
            _ldv = _dataContext.DichVus.Where(p => p.PLoai == 2 || p.PLoai == 3).OrderBy(p => p.TenDV).ToList();
            grcDichVu.DataSource = _ldv;

        }



        private void btnMoi_Click(object sender, EventArgs e)
        {
            
            bool kt = true;
            if (DungChung.Bien.MaBV == "12001")
            {
                kt = DungChung.Ham.checkQuyenFalse("usDichVu")[0];
                if (!kt)
                    MessageBox.Show("Bạn chưa được cấp quyền thêm mới dịch vụ ! \nLiên hệ với admin để được cấp quyền");
            }

            if (kt)
            {
                TTLuu = 1;
                EnableButton(false);
                resetcontrol();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            int rs;
            int _int_MaDV = 0;
            if (Int32.TryParse(txtMaDV.Text, out rs))
                _int_MaDV = Convert.ToInt32(txtMaDV.Text);
            if (Ktra())
            {
                switch (TTLuu)
                {
                    case 1:
                        DichVu taomoi = new DichVu();
                        if (!string.IsNullOrEmpty(txtSoTT.Text))
                            taomoi.SoTT = Convert.ToInt32(txtSoTT.Text);
                        taomoi.TenDV = txtTenDV.Text;
                        taomoi.MaDV = _int_MaDV;
                        taomoi.MaTam = txtMaTam.Text.Trim();
                        taomoi.DonVi = cboDonVi.Text;
                        taomoi.CanhBaoHM = mmCanhBao.Text;
                        taomoi.DonGia = Convert.ToDouble(txtDonGia.Text);
                        taomoi.IDNhom = Convert.ToInt32(lupPhanLoai.EditValue);
                        taomoi.IdTieuNhom = Convert.ToInt32(lupNhom.EditValue);
                        taomoi.TrongDM = cboTrongDM.SelectedIndex;
                        taomoi.MaQD = txtMaQD.Text.Trim();

                        if (cboPLoai.SelectedIndex != 0)
                        {
                            if (cboPLoai.SelectedIndex == 1)
                                taomoi.PLoai = 3;
                        }
                        else
                        {
                            taomoi.PLoai = 2;
                        }
                        if (chkKTC.Checked)
                            taomoi.DVKTC = 1;
                        else
                            taomoi.DVKTC = 0;
                        if (chkStatus.Checked)
                            taomoi.Status = 1;
                        else
                            taomoi.Status = 0;
                        if (chkCLS.Checked)
                            taomoi.IS_EXECUTE_CLS = true;
                        else
                            taomoi.IS_EXECUTE_CLS = false;
                      
                        if (chkAutoExcecute.Checked)
                            taomoi.IsAutoExecute = true;
                        else
                            taomoi.IsAutoExecute = false;

                        if (!string.IsNullOrEmpty(txtPTram.Text))
                            taomoi.BHTT = Convert.ToInt32(txtPTram.Text);
                        taomoi.MaCC = "";

                        if (!string.IsNullOrEmpty(txtGHCD.Text))
                        {
                            taomoi.GHCDTrongNgay = Convert.ToInt32(txtGHCD.Text);
                        }
                        else
                        {
                            taomoi.GHCDTrongNgay = 0;
                        }
                        if (!string.IsNullOrEmpty(txtSoPhutTH.Text))
                        {
                            taomoi.SoPhutThucHien = Convert.ToInt32(txtSoPhutTH.Text);
                        }
                        else
                        {
                            taomoi.SoPhutThucHien = 0;
                        }
                        taomoi.TenRG = txtTenRG.Text.Trim();
                        taomoi.TenKhac = txtTenKhac.Text.Trim();
                        taomoi.TenPhuongPhap = txtPhuongPhap.Text.Trim();
                        taomoi.QCPC = txtQCPC.Text.Trim();
                        if (!string.IsNullOrEmpty(txtDonGia2.Text))
                        {
                            taomoi.DonGia2 = Convert.ToDouble(txtDonGia2.Text);
                        }
                        if (!string.IsNullOrEmpty(txtGiaPhuThu.Text))
                        {
                            taomoi.GiaPhuThu = Convert.ToDouble(txtGiaPhuThu.Text);
                        }
                        else
                        {
                            taomoi.DonGia2 = 0;
                        }

                        if (!string.IsNullOrEmpty(txtGiaDV2.Text))
                        {
                            taomoi.DonGiaDV2 = Convert.ToDouble(txtGiaDV2.Text);
                        }
                        else
                        {
                            taomoi.DonGiaDV2 = 0;
                        }
                        taomoi.Loai = cboLoai.SelectedIndex;
                        if (cboLoai.SelectedIndex == 0 && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049") && grvDichVu.GetFocusedRowCellValue(colTieuNhom).ToString() == "22") // Ploai = 0 + tieunhom = thu thuat
                        {
                            taomoi.Loai = 0;
                        }
                        taomoi.SoTTqd = txtSoTT_BYT.Text.Trim();
                        string _makpsd = ";";
                        for (int i = 0; i < cklKP.ItemCount; i++)
                        {
                            if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                                _makpsd += cklKP.GetItemValue(i) + ";";
                        }
                        taomoi.MaKPsd = _makpsd;
                        string _maDTBN = ";";
                        for (int i = 0; i < cklDTBN.ItemCount; i++)
                        {
                            if (cklDTBN.GetItemCheckState(i) == CheckState.Checked)
                                _maDTBN += cklDTBN.GetItemValue(i) + ";";
                        }
                        taomoi.DSDTBN = _maDTBN;

                        taomoi.SoQD = txtSoQD.Text.Trim();
                        if (!string.IsNullOrEmpty(txtGiaBHTT.Text))
                            taomoi.DonGiaBHYT = Convert.ToDouble(txtGiaBHTT.Text);
                        if (!string.IsNullOrEmpty(txtdongiatt15.Text))
                            taomoi.DonGiaTT15 = Convert.ToDouble(txtdongiatt15.Text);
                        if (!string.IsNullOrEmpty(txtGiaTT39.Text))
                            taomoi.DonGiaTT39 = Convert.ToDouble(txtGiaTT39.Text);
                        taomoi.DSDonGia = "";
                        if (!string.IsNullOrEmpty(txtGiaBHGioiHanTT.Text))
                            taomoi.GiaBHGioiHanTT = Convert.ToDouble(txtGiaBHGioiHanTT.Text);
                        taomoi.MaNhom = txtMaPTTTQT.Text;
                        if (!string.IsNullOrEmpty(txtHanMuc.Text))
                            taomoi.HMChiDinh = Convert.ToInt32(txtHanMuc.Text);
                        if (txtMaNhom5937.Text == "")
                            taomoi.MaNhom5937 = null;
                        else
                            taomoi.MaNhom5937 = Convert.ToInt32(txtMaNhom5937.Text);

                        if (cboPLTT.Text != "")
                            taomoi.PhanLoaiTT = cboPLTT.Text;
                        _dataContext.DichVus.Add(taomoi);
                        _dataContext.SaveChanges();
                        usDichVu_Load(sender, e);
                        EnableButton(true);
                        break;
                    case 2:
                        int idnhom = 0;
                        var sua = _dataContext.DichVus.Single(p => p.MaDV == _int_MaDV);
                        if (!string.IsNullOrEmpty(txtSoTT.Text))
                            sua.SoTT = Convert.ToInt32(txtSoTT.Text);
                        sua.TenDV = txtTenDV.Text;
                        sua.DonVi = cboDonVi.Text;
                        sua.CanhBaoHM = mmCanhBao.Text;
                        sua.DonGia = Convert.ToDouble(txtDonGia.Text);
                        sua.IDNhom = Convert.ToInt32(lupPhanLoai.EditValue);
                        sua.MaTam = txtMaTam.Text.Trim();
                        if (sua.IDNhom != null)
                            idnhom = sua.IDNhom.Value;

                        sua.IdTieuNhom = Convert.ToInt32(lupNhom.EditValue);
                        sua.TrongDM = cboTrongDM.SelectedIndex;
                        sua.MaQD = txtMaQD.Text.Trim();

                        if (cboPLoai.SelectedIndex != 0)
                        {
                            if (cboPLoai.SelectedIndex == 1)
                                sua.PLoai = 3;
                        }
                        else
                        {
                            sua.PLoai = 2;
                        }
                        if (chkKTC.Checked)
                            sua.DVKTC = 1;
                        else
                            sua.DVKTC = 0;
                        if (chkStatus.Checked)
                            sua.Status = 1;
                        else
                            sua.Status = 0;
                        if (chkCLS.Checked == true)
                        {
                            sua.IS_EXECUTE_CLS = true;
                        }
                        else
                        {
                            sua.IS_EXECUTE_CLS = false;
                        }
                        if (chkAutoExcecute.Checked)
                            sua.IsAutoExecute = true;
                        else
                            sua.IsAutoExecute = false;
                        if (!string.IsNullOrEmpty(txtPTram.Text))
                            sua.BHTT = Convert.ToInt32(txtPTram.Text);
                        if (!string.IsNullOrEmpty(txtDonGia2.Text))
                        {
                            sua.DonGia2 = Convert.ToDouble(txtDonGia2.Text);
                        }
                        else
                        {
                            sua.DonGia2 = 0;
                        }
                        if (!string.IsNullOrEmpty(txtGiaPhuThu.Text))
                        {
                            sua.GiaPhuThu = Convert.ToDouble(txtGiaPhuThu.Text);
                        }
                        if (!string.IsNullOrEmpty(txtGiaDV2.Text))
                        {
                            sua.DonGiaDV2 = Convert.ToDouble(txtGiaDV2.Text);
                        }
                        else
                        {
                            sua.DonGiaDV2 = 0;
                        }
                        if (!string.IsNullOrEmpty(txtGHCD.Text))
                        {
                            sua.GHCDTrongNgay = Convert.ToInt32(txtGHCD.Text);
                        }
                        else
                        {
                            sua.GHCDTrongNgay = 0;
                        }
                        if (!string.IsNullOrEmpty(txtSoPhutTH.Text))
                        {
                            sua.SoPhutThucHien = Convert.ToInt32(txtSoPhutTH.Text);
                        }
                        else
                        {
                            sua.SoPhutThucHien = 0;
                        }
                        sua.TenPhuongPhap = txtPhuongPhap.Text.Trim();
                        sua.TenKhac = txtTenKhac.Text.Trim();
                        sua.QCPC = txtQCPC.Text.Trim();
                        sua.TenRG = txtTenRG.Text.Trim();
                        sua.Loai = cboLoai.SelectedIndex;
                        sua.SoTTqd = txtSoTT_BYT.Text.Trim();
                        string _makpsd2 = ";";
                        for (int i = 0; i < cklKP.ItemCount; i++)
                        {
                            if (cklKP.GetItemCheckState(i) == CheckState.Checked)
                                _makpsd2 += cklKP.GetItemValue(i) + ";";
                        }
                        sua.MaKPsd = _makpsd2;
                        string _maDTBN2 = ";";
                        for (int i = 0; i < cklDTBN.ItemCount; i++)
                        {
                            if (cklDTBN.GetItemCheckState(i) == CheckState.Checked)
                                _maDTBN2 += cklDTBN.GetItemValue(i) + ";";
                        }
                        sua.DSDTBN = _maDTBN2;
                        sua.SoQD = txtSoQD.Text.Trim();
                        if (!string.IsNullOrEmpty(txtGiaBHTT.Text))
                            sua.DonGiaBHYT = Convert.ToDouble(txtGiaBHTT.Text);
                        if (!string.IsNullOrEmpty(txtdongiatt15.Text))
                            sua.DonGiaTT15 = Convert.ToDouble(txtdongiatt15.Text);
                        if (!string.IsNullOrEmpty(txtGiaTT39.Text))
                            sua.DonGiaTT39 = Convert.ToDouble(txtGiaTT39.Text);
                        if (!string.IsNullOrEmpty(txtGiaBHGioiHanTT.Text))
                            sua.GiaBHGioiHanTT = Convert.ToDouble(txtGiaBHGioiHanTT.Text);
                        sua.MaNhom = txtMaPTTTQT.Text;
                        sua.HMChiDinh = Convert.ToInt32(txtHanMuc.Text);
                        if (txtMaNhom5937.Text == "")
                            sua.MaNhom5937 = null;
                        else
                            sua.MaNhom5937 = Convert.ToInt32(txtMaNhom5937.Text);
                        if (cboPLTT.Text != "")
                            sua.PhanLoaiTT = cboPLTT.Text;

                        _dataContext.SaveChanges();
                        int idnhommoi = Convert.ToInt32(lupPhanLoai.EditValue);
                        if (idnhom != idnhommoi)
                        {
                            var nhom = _dataContext.NhomDVs.Where(p => p.IDNhom == idnhom).ToList();
                            string _tennhomcu = nhom.First().TenNhomCT;
                            var nhommoi = _dataContext.NhomDVs.Where(p => p.IDNhom == idnhommoi).ToList();
                            string _tennhommoi = nhommoi.First().TenNhomCT;
                            //var cpdv = _dataContext.ChiPhiDVs.Where(p => p.MaDV == _int_MaDV).ToList();
                            //foreach (var a in cpdv)
                            //{
                            //    switch (_tennhomcu)
                            //    {
                            //        case "Thuốc trong danh mục BHYT":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.Thuoc;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.Thuoc;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.Thuoc;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.Thuoc;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.Thuoc;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.Thuoc;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.Thuoc;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.Thuoc;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.Thuoc;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.Thuoc;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.Thuoc;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.Thuoc;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.Thuoc = null;
                            //            break;
                            //        case "Máu và chế phẩm của máu":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.Mau;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.Mau;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.Mau;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.Mau;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.Mau;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.Mau;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.Mau;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.Mau;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.Mau;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.Mau;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.Mau;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.Mau;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.Mau = null;
                            //            break;
                            //        case "Xét nghiệm":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.XetNghiem;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.XetNghiem;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.XetNghiem;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.XetNghiem;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.XetNghiem;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.XetNghiem;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.XetNghiem;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.XetNghiem;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.XetNghiem;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.XetNghiem;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.XetNghiem;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.XetNghiem;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.XetNghiem = null;
                            //            break;
                            //        case "Chẩn đoán hình ảnh":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.CDHA;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.CDHA;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.CDHA;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.CDHA;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.CDHA;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.CDHA;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.CDHA;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.CDHA;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.CDHA;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.CDHA;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.CDHA;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.CDHA;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.CDHA = null;
                            //            break;
                            //        case "Thủ thuật, phẫu thuật":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.TTPT;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.TTPT;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.TTPT;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.TTPT;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.TTPT;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.TTPT;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.TTPT;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.TTPT;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.TTPT;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.TTPT;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.TTPT;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.TTPT;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.TTPT = null;
                            //            break;
                            //        case "Khám bệnh":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.CongKham;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.CongKham;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.CongKham;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.CongKham;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.CongKham;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.CongKham;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.CongKham;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.CongKham;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.CongKham;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.CongKham;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.CongKham;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.CongKham;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.CongKham = null;
                            //            break;
                            //        case "DVKT thanh toán theo tỷ lệ":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.DVKT_tl;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.DVKT_tl;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.DVKT_tl;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.DVKT_tl;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.DVKT_tl;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.DVKT_tl;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.DVKT_tl;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.DVKT_tl;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.DVKT_tl;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.DVKT_tl;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.DVKT_tl;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.DVKT_tl;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.DVKT_tl = null;
                            //            break;
                            //        case "Vật tư y tế trong danh mục BHYT":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.VTYTTH;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.VTYTTH;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.VTYTTH;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.VTYTTH;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.VTYTTH;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.VTYTTH;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.VTYTTH;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.VTYTTH;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.VTYTTH;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.VTYTTH;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.VTYTTH;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.VTYTTH;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.VTYTTH = null;
                            //            break;
                            //        case "Giường điều trị nội trú":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.TienNgayGiuong;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.TienNgayGiuong;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.TienNgayGiuong;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.TienNgayGiuong;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.TienNgayGiuong;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.TienNgayGiuong;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.TienNgayGiuong;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.TienNgayGiuong;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.TienNgayGiuong;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.TienNgayGiuong;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.TienNgayGiuong;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.TienNgayGiuong;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.TienNgayGiuong = null;
                            //            break;
                            //        case "Vận chuyển":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.CPVanChuyen;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.CPVanChuyen;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.CPVanChuyen;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.CPVanChuyen;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.CPVanChuyen;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.CPVanChuyen;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.CPVanChuyen;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.CPVanChuyen;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.CPVanChuyen;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.CPVanChuyen;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.CPVanChuyen;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.CPVanChuyen;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.CPVanChuyen = null;
                            //            break;
                            //        case "VTYT thanh toán theo tỷ lệ":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.VTYT_tl;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.VTYT_tl;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.VTYT_tl;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.VTYT_tl;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.VTYT_tl;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.VTYT_tl;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.VTYT_tl;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.VTYT_tl;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.VTYT_tl;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.VTYT_tl;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.VTYT_tl;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.VTYT_tl;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.VTYT_tl = null;
                            //            break;
                            //        case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //            switch (_tennhommoi)
                            //            {
                            //                case "Thuốc trong danh mục BHYT":
                            //                    a.Thuoc += a.Thuoc_tl;
                            //                    break;
                            //                case "Máu và chế phẩm của máu":
                            //                    a.Mau += a.Thuoc_tl;
                            //                    break;
                            //                case "Xét nghiệm":
                            //                    a.XetNghiem += a.Thuoc_tl;
                            //                    break;
                            //                case "Chẩn đoán hình ảnh":
                            //                    a.CDHA += a.Thuoc_tl;
                            //                    break;
                            //                case "Thủ thuật, phẫu thuật":
                            //                    a.TTPT += a.Thuoc_tl;
                            //                    break;
                            //                case "Khám bệnh":
                            //                    a.CongKham += a.Thuoc_tl;
                            //                    break;
                            //                case "DVKT thanh toán theo tỷ lệ":
                            //                    a.DVKT_tl += a.Thuoc_tl;
                            //                    break;
                            //                case "Vật tư y tế trong danh mục BHYT":
                            //                    a.VTYTTH += a.Thuoc_tl;
                            //                    break;
                            //                case "Giường điều trị nội trú":
                            //                    a.TienNgayGiuong += a.Thuoc_tl;
                            //                    break;
                            //                case "Vận chuyển":
                            //                    a.CPVanChuyen += a.Thuoc_tl;
                            //                    break;
                            //                case "VTYT thanh toán theo tỷ lệ":
                            //                    a.VTYT_tl += a.Thuoc_tl;
                            //                    break;
                            //                case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //                    a.Thuoc_tl += a.Thuoc_tl;
                            //                    break;
                            //                case "Nhóm hóa chất":
                            //                    //a. += a.Thuoc;
                            //                    break;
                            //                case "Chi phí KSK":
                            //                    //  a.Mau += a.Thuoc;
                            //                    break;

                            //            }
                            //            a.Thuoc_tl = null;
                            //            break;
                            //        case "Nhóm hóa chất":
                            //            //switch (_tennhommoi)
                            //            //{
                            //            //    case  "Thuốc trong danh mục BHYT" :
                            //            //        a.Thuoc += a.XetNghiem;
                            //            //        break;
                            //            //    case "Máu và chế phẩm của máu":
                            //            //        a.Mau += a.XetNghiem;
                            //            //        break;
                            //            //    case "Xét nghiệm":
                            //            //        a.XetNghiem += a.XetNghiem;
                            //            //        break;
                            //            //    case "Chẩn đoán hình ảnh":
                            //            //        a.CDHA += a.XetNghiem;
                            //            //        break;
                            //            //    case "Thủ thuật, phẫu thuật":
                            //            //        a.TTPT += a.XetNghiem;
                            //            //        break;
                            //            //    case "Khám bệnh":
                            //            //        a.CongKham += a.XetNghiem;
                            //            //        break;
                            //            //    case "DVKT thanh toán theo tỷ lệ":
                            //            //        a.DVKT_tl += a.XetNghiem;
                            //            //        break;
                            //            //    case"Vật tư y tế trong danh mục BHYT":
                            //            //        a.VTYTTH += a.XetNghiem;
                            //            //        break;
                            //            //    case "Giường điều trị nội trú":
                            //            //        a.TienNgayGiuong += a.XetNghiem;
                            //            //        break;
                            //            //    case "Vận chuyển":
                            //            //        a.CPVanChuyen += a.XetNghiem;
                            //            //        break;
                            //            //    case"VTYT thanh toán theo tỷ lệ":
                            //            //        a.VTYT_tl += a.XetNghiem;
                            //            //        break;
                            //            //    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //            //        a.Thuoc_tl += a.XetNghiem;
                            //            //        break;
                            //            //    case "Nhóm hóa chất":
                            //            //        //a. += a.Thuoc;
                            //            //        break;
                            //            //    case "Chi phí KSK":
                            //            //        //  a.Mau += a.Thuoc;
                            //            //        break;

                            //            //}
                            //            break;
                            //        case "Chi phí KSK":
                            //            //switch (_tennhommoi)
                            //            //{
                            //            //    case  "Thuốc trong danh mục BHYT" :
                            //            //        a.Thuoc += a.XetNghiem;
                            //            //        break;
                            //            //    case "Máu và chế phẩm của máu":
                            //            //        a.Mau += a.XetNghiem;
                            //            //        break;
                            //            //    case "Xét nghiệm":
                            //            //        a.XetNghiem += a.XetNghiem;
                            //            //        break;
                            //            //    case "Chẩn đoán hình ảnh":
                            //            //        a.CDHA += a.XetNghiem;
                            //            //        break;
                            //            //    case "Thủ thuật, phẫu thuật":
                            //            //        a.TTPT += a.XetNghiem;
                            //            //        break;
                            //            //    case "Khám bệnh":
                            //            //        a.CongKham += a.XetNghiem;
                            //            //        break;
                            //            //    case "DVKT thanh toán theo tỷ lệ":
                            //            //        a.DVKT_tl += a.XetNghiem;
                            //            //        break;
                            //            //    case"Vật tư y tế trong danh mục BHYT":
                            //            //        a.VTYTTH += a.XetNghiem;
                            //            //        break;
                            //            //    case "Giường điều trị nội trú":
                            //            //        a.TienNgayGiuong += a.XetNghiem;
                            //            //        break;
                            //            //    case "Vận chuyển":
                            //            //        a.CPVanChuyen += a.XetNghiem;
                            //            //        break;
                            //            //    case"VTYT thanh toán theo tỷ lệ":
                            //            //        a.VTYT_tl += a.XetNghiem;
                            //            //        break;
                            //            //    case "Thuốc điều trị ung thư, chống thải ghép ngoài danh mục":
                            //            //        a.Thuoc_tl += a.XetNghiem;
                            //            //        break;
                            //            //    case "Nhóm hóa chất":
                            //            //        //a. += a.Thuoc;
                            //            //        break;
                            //            //    case "Chi phí KSK":
                            //            //        //  a.Mau += a.Thuoc;
                            //            //        break;

                            //            //}
                            //            break;

                            //    }
                            //}
                            //if (cpdv.Count > 0)
                            //    _dataContext.SaveChanges();
                        }
                        // usDichVu_Load(sender, e);
                        EnableButton(true);
                        break;
                }
                TTLuu = 0;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                cboLoai.Properties.Items[0] = "Đặc biệt";
            }
            else
                cboLoai.Properties.Items[0] = 0;
            bool kt = true;
            if (DungChung.Bien.MaBV == "12001")
            {
                kt = DungChung.Ham.checkQuyenFalse("usDichVu")[1];
                if (!kt)
                    MessageBox.Show("Bạn chưa được cấp quyền để sửa dịch vụ ! \nLiên hệ với admin để được cấp quyền");
            }
            if (txtPLDV.Text == "1")
            {
                kt = false;
                MessageBox.Show("Chi phí này thuộc khoa dược quản lý. Bạn không được sửa");
            }
            if (kt)
            {
                TTLuu = 2;
                EnableButton(false);
                txtMaDV.Properties.ReadOnly = true;
            }
        }
        public class KhoaPhong
        {
            public bool _check;
            public int _maKP;
            public string _kp;

            public int MaKP { get { return _maKP; } set { _maKP = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string TenKP { get { return _kp; } set { _kp = value; } }
        }
        public class Dtuong
        {
            public bool _check;
            public int iDDTBN;
            public string dTBN;


            public int IDDTBN { get { return iDDTBN; } set { iDDTBN = value; } }
            public bool Check { get { return _check; } set { _check = value; } }
            public string DTBN { get { return dTBN; } set { dTBN = value; } }
        }
        List<usDichVu.KhoaPhong> _lKPsd = new List<usDichVu.KhoaPhong>();
        List<usDichVu.Dtuong> _lDTBN = new List<usDichVu.Dtuong>();
        public static void _loadKPsd(string dsKPsd, List<KhoaPhong> _lKPsd, CheckedListBoxControl cklKP)
        {

            try
            {
                if (!string.IsNullOrEmpty(dsKPsd))
                {
                    string[] kp = dsKPsd.Split(';');
                    for (int i = 0; i < cklKP.ItemCount; i++)
                    {

                        cklKP.SetItemChecked(i, false);

                    }
                    foreach (var item in kp)
                    {
                        foreach (var item2 in _lKPsd)
                        {
                            if (!string.IsNullOrEmpty(item))
                                if (Convert.ToInt32(item) == item2.MaKP)
                                {
                                    item2.Check = true;
                                    for (int i = 0; i < cklKP.ItemCount; i++)
                                    {
                                        if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == item2.MaKP)
                                        {
                                            cklKP.SetItemChecked(i, true);
                                            break;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load Danh sách khoa phòng sử dụng: " + ex.Message);
            }

        }

        public static void _loadDSDTBN(string dsDTBN, List<Dtuong> _lDTBN, CheckedListBoxControl ckldtBN)
        {

            try
            {
                if (!string.IsNullOrEmpty(dsDTBN))
                {
                    string[] kp = dsDTBN.Split(';');
                    for (int i = 0; i < ckldtBN.ItemCount; i++)
                    {

                        ckldtBN.SetItemChecked(i, false);

                    }
                    foreach (var item in kp)
                    {
                        foreach (var item2 in _lDTBN)
                        {
                            if (!string.IsNullOrEmpty(item))
                                if (Convert.ToInt32(item) == item2.IDDTBN)
                                {
                                    item2.Check = true;
                                    for (int i = 0; i < ckldtBN.ItemCount; i++)
                                    {
                                        if (ckldtBN.GetItemValue(i) != null && Convert.ToInt32(ckldtBN.GetItemValue(i)) == item2.IDDTBN)
                                        {
                                            ckldtBN.SetItemChecked(i, true);
                                            break;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ckldtBN.ItemCount; i++)
                    {

                        ckldtBN.SetItemChecked(i, false);

                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load Danh sách DTBN: " + ex.Message);
            }

        }
        private void grvDichVu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDichVu.GetFocusedRowCellValue(colMaDV) != null)
            {
                int madv = Convert.ToInt32(grvDichVu.GetFocusedRowCellValue(colMaDV));
                txtMaDV.Text = madv.ToString();
                var dv = _ldv.FirstOrDefault(p => p.MaDV == madv);
                if (dv != null)
                {
                    txtMaTam.Text = dv.MaTam;
                    txtSoTT_BYT.Text = dv.SoTTqd;
                    txtTenDV.Text = dv.TenDV;
                    cboDonVi.Text = dv.DonVi;
                    mmCanhBao.Text = dv.CanhBaoHM;
                    txtGiaPhuThu.Text = dv.GiaPhuThu.ToString();
                    txtDonGia.Text = dv.DonGia.ToString();
                    if (dv.BHTT != null)
                        txtPTram.Text = dv.BHTT.ToString();
                    if (dv.IDNhom != null)
                        lupPhanLoai.EditValue = dv.IDNhom.Value;
                    if (dv.IdTieuNhom != null)
                        lupNhom.EditValue = dv.IdTieuNhom.Value;
                    if (dv.TrongDM != null)
                    {
                        cboTrongDM.SelectedIndex = dv.TrongDM.Value;
                    }
                    if (dv.Status != null)
                    {
                        if (dv.Status == 1)
                            chkStatus.Checked = true;
                        else
                            chkStatus.Checked = false;
                    }
                    else
                        chkStatus.Checked = false;

                    if (dv.IsAutoExecute != true)
                        chkAutoExcecute.Checked = false;
                    else
                        chkAutoExcecute.Checked = true;

                    if (dv.DVKTC != null)
                    {
                        if (dv.DVKTC == 1)
                            chkKTC.Checked = true;
                        else
                            chkKTC.Checked = false;
                    }

                    if (dv.IS_EXECUTE_CLS != null)
                    {
                        if (dv.IS_EXECUTE_CLS == true)
                        {
                            chkCLS.Checked = true;
                        }
                        else
                        {
                            chkCLS.Checked = false;
                        }
                    }

                    //chkCLS.Checked == dv.IS_EXECUTE_CLS ?? false;

                    if (dv.SoTT != null)
                        txtSoTT.Text = dv.SoTT.Value.ToString();
                    else
                        txtSoTT.Text = "";
                    txtSoPhutTH.Text = dv.SoPhutThucHien.ToString();
                    txtGHCD.Text = dv.GHCDTrongNgay.ToString();
                    txtQCPC.Text = dv.QCPC;
                    txtTenKhac.Text = dv.TenKhac;
                    txtPhuongPhap.Text = dv.TenPhuongPhap;
                    txtMaQD.Text = dv.MaQD;
                    txtTenRG.Text = dv.TenRG;
                    if (dv.PLoai != null)
                        cboPLoai.Text = dv.PLoai.ToString();
                    else
                        cboPLoai.Text = "2";
                    txtDonGia2.Text = dv.DonGia2.ToString();
                    txtGiaDV2.Text = dv.DonGiaDV2.ToString();
                    if (dv.Loai != null)
                    {
                        cboLoai.SelectedIndex = dv.Loai.Value;
                        if (cboLoai.SelectedIndex == 0 && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"))
                        {
                            cboLoai.Text = "Đặc biệt";
                        }
                    }
                    else
                    {
                        cboLoai.SelectedIndex = -1;
                    }
                    txtSoQD.Text = dv.SoQD;
                    txtGiaBHTT.Text = dv.DonGiaBHYT.ToString();
                    txtdongiatt15.Text = dv.DonGiaTT15.ToString();
                    txtGiaTT39.Text = dv.DonGiaTT39.ToString();
                    txtGiaBHGioiHanTT.Text = dv.GiaBHGioiHanTT.ToString();
                    txtMaPTTTQT.Text = dv.MaNhom;
                    txtHanMuc.Text = dv.HMChiDinh.ToString();
                    txtMaNhom5937.Text = dv.MaNhom5937.ToString();
                    if (DungChung.Bien.MaBV == "24012" && dv.IDNhom == 8)
                    {
                        lblPLThuThuat.Visible = cboPLTT.Visible = true;
                        cboPLTT.Text = dv.PhanLoaiTT;
                    }
                    else
                    {
                        lblPLThuThuat.Visible = cboPLTT.Visible = false;
                    }

                    _loadKPsd(_ldv.Where(p => p.MaDV == madv).First().MaKPsd, _lKPsd, cklKP);
                    _loadDSDTBN(_ldv.Where(p => p.MaDV == madv).First().DSDTBN, _lDTBN, cklDTBN);
                }
            }
            else
            {
                resetcontrol();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (DungChung.Bien.MaBV == "12001")
            {
                if (!DungChung.Ham.checkQuyenFalse("usDichVu")[2])
                {
                    MessageBox.Show("Bạn chưa được cấp quyền để sửa dịch vụ ! \nLiên hệ với admin để được cấp quyền");
                    return;
                }
            }

            int rs;
            int _int_MaDV = 0;
            if (Int32.TryParse(txtMaDV.Text, out rs))
                _int_MaDV = Convert.ToInt32(txtMaDV.Text);

            if (_int_MaDV > 0)
            {
                var kt = _dataContext.DThuoccts.Where(p => p.MaDV == _int_MaDV).ToList();
                if (kt.Count > 0)
                {
                    MessageBox.Show("chi phí đã được sử dụng. Bạn không được xóa!");
                }
                else
                {
                    DialogResult _result = MessageBox.Show("Bạn muốn xóa chi phí: " + txtTenDV.Text, "xóa chi phí", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_result == DialogResult.Yes)
                    {
                        var xoa = _dataContext.DichVus.Single(p => p.MaDV == _int_MaDV);
                        _dataContext.DichVus.Remove(xoa);
                        _dataContext.SaveChanges();
                        grvDichVu.DeleteSelectedRows();
                        //_ldv = _dataContext.DichVus.OrderBy(p => p.TenDV).ToList();
                        //grcDichVu.DataSource = _ldv.ToList();
                    }
                }
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            grcDichVu.DataSource = _ldv.Where(p => p.TenDV.ToLower().Contains(txtTimKiem.Text.ToLower())).ToList();
            if (lupTimNhomDV.EditValue != null)
            {
                int id = Convert.ToInt32(lupTimNhomDV.EditValue);
                grcDichVu.DataSource = _ldv.Where(p => (p.TenDV.ToLower().Contains(txtTimKiem.Text.ToLower()) || (p.TenHC != null && p.TenHC.ToLower().Contains(txtTimKiem.Text.ToLower()))) && p.IDNhom == id).ToList();
            }
        }

        private void txtMaDV_Leave(object sender, EventArgs e)
        {
            int rs;
            int _int_MaDV = 0;
            if (Int32.TryParse(txtMaDV.Text, out rs))
                _int_MaDV = Convert.ToInt32(txtMaDV.Text);
            {

                if (TTLuu == 1)
                {
                    var kt = _dataContext.DichVus.Where(p => p.MaDV == _int_MaDV).ToList();
                    if (kt.Count > 0)
                    {
                        MessageBox.Show("Mã DV đã tồn tại, bạn hãy nhập mã khác");
                    }
                }
            }
        }

        private void lupTimNhomDV_EditValueChanged(object sender, EventArgs e)
        {
            int idnhom = 0;
            if (lupTimNhomDV.EditValue != null && lupTimNhomDV.EditValue.ToString() != "")
                idnhom = Convert.ToInt16(lupTimNhomDV.GetColumnValue("IDNhom"));
            var timtn = _dataContext.TieuNhomDVs.Where(p => p.IDNhom == idnhom).ToList().OrderBy(p => p.TenTN).ToList();
            lupTimTN.Properties.DataSource = timtn;
            lupTimTN.EditValue = "";
            if (lupTimNhomDV.EditValue != null)
            {
                int id = Convert.ToInt32(lupTimNhomDV.EditValue);
                if (txtTimKiem.Text == "Tìm kiếm Tên| Mã dịch vụ")
                {
                    grcDichVu.DataSource = _ldv.Where(p => p.IDNhom == id).ToList();
                }
                else
                    grcDichVu.DataSource = _ldv.Where(p => p.TenDV.ToLower().Contains(txtTimKiem.Text.ToLower())).Where(p => p.IDNhom == id).ToList();
            }
        }


        private void lupNhom_EditValueChanged(object sender, EventArgs e)
        {
            cboLoai.Properties.Items.Clear();
            if (lupNhom.EditValue != null && lupNhom.EditValue.ToString() != "")
            {
                lupPhanLoai.EditValue = lupNhom.GetColumnValue("IDNhom");
                string tenrg = "";
                if (lupNhom.GetColumnValue("TenRG") != null)
                    tenrg = lupNhom.GetColumnValue("TenRG").ToString();
                switch (tenrg)
                {
                    case "X-Quang":
                        labSoLuongPhim.Text = "S.Lượng phim";
                        cboLoai.Properties.Items.Add(0);
                        cboLoai.Properties.Items.Add(1);
                        cboLoai.Properties.Items.Add(2);
                        cboLoai.Properties.Items.Add(3);
                        cboLoai.Properties.Items.Add(4);
                        break;
                    case "Siêu âm":
                        labSoLuongPhim.Text = "Loại siêu âm";
                        cboLoai.Properties.Items.Add(st_PhanLoaiSieuAm.Sa2D);
                        cboLoai.Properties.Items.Add(st_PhanLoaiSieuAm.SaMau);
                        cboLoai.Properties.Items.Add(st_PhanLoaiSieuAm.Sa3D_4D);
                        break;
                    default:
                        labSoLuongPhim.Text = "Loại";
                        for (int i = 0; i < 50; i++)
                            cboLoai.Properties.Items.Add(i);
                        break;
                }
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                {
                    cboLoai.Properties.Items[0] = "Đặc biệt";
                }

            }
            else
            {
                lupPhanLoai.EditValue = "";
            }

            //
        }

        private void lupTimTN_EditValueChanged(object sender, EventArgs e)
        {
            int id = 0;
            if (lupTimTN.EditValue != null && lupTimTN.EditValue.ToString() != "")
                id = Convert.ToInt32(lupTimTN.EditValue);
            string _tendv = "";
            if (txtTimKiem.Text != "Tìm kiếm Tên| Mã dịch vụ" && !string.IsNullOrEmpty(txtTimKiem.Text))
                _tendv = txtTimKiem.Text.Trim().ToLower();
            grcDichVu.DataSource = _ldv.Where(p => p.TenDV.ToLower().Contains(_tendv)).Where(p => p.IdTieuNhom == id).ToList();
        }

        private void cboLoai_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (cboPLoai.SelectedIndex == 0 && TTLuu != 0)
            {
                DialogResult _result = MessageBox.Show("Phân loại là 3 chỉ dùng cho dịch vụ phục vụ trong đơn vị sử dụng chức năng nhập chi phí BHYT của các TYT XP, Bạn vẫn muốn thay đổi?", "Thay đổi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void grvDichVu_DataSourceChanged(object sender, EventArgs e)
        {
            grvDichVu_FocusedRowChanged(null, null);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormDanhMuc.frm_UpdateGiaDV frm = new frm_UpdateGiaDV();
            frm.ShowDialog();
        }

        private void btnUpdateCV5084_Click(object sender, EventArgs e)
        {
            FormNhap.frm_CNDichVu_5084 frm = new FormNhap.frm_CNDichVu_5084(2);
            frm.ShowDialog();
        }

        public static void _updateMaKPsd(int pl)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kphong = (from kp in _dataContext.KPhongs.Where(p => p.PLoai == "Lâm sàng" || p.PLoai == "Phòng khám" || (pl == 1 ? p.PLoai == "Khoa dược" : false))
                          select new
                          {
                              MaKP = kp.MaKP,
                              TenKP = kp.TenKP
                          }).Distinct().OrderBy(p => p.TenKP).ToList();
            string _maKPsd = ";";
            foreach (var item in kphong)
            {
                _maKPsd += item.MaKP + ";";
            }

            List<DichVu> updateKp = _dataContext.DichVus.Where(p => p.PLoai == pl).ToList();
            int i = 0;
            foreach (var item in updateKp)
            {
                if (string.IsNullOrEmpty(item.MaKPsd))
                {
                    item.MaKPsd = _maKPsd;
                    i++;
                }
                else
                {
                    if (item.MaKPsd.Substring(0, 1) != ";")
                    {
                        item.MaKPsd = ";" + item.MaKPsd;
                        i++;
                    }
                }

            }
            _dataContext.SaveChanges();
            MessageBox.Show("Thiết lập thành công!" + i.ToString() + " dịch vụ");
        }
        private void btnKhoaPhong_Click(object sender, EventArgs e)
        {

            _updateMaKPsd(2);

        }

        private void btnUpdateDichVuEx_Click(object sender, EventArgs e)
        {
            FormNhap.frm_Update_DichVuEx dvEx = new FormNhap.frm_Update_DichVuEx();
            dvEx.ShowDialog();
        }

        private void s_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl18_Click(object sender, EventArgs e)
        {

        }

        private void btnDMGoi_Click(object sender, EventArgs e)
        {
            QLBV.FormDanhMuc.frm_DmGoiDV frm = new frm_DmGoiDV();
            frm.ShowDialog();
        }

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckAll.Checked)
            {
                cklKP.CheckAll();
            }
            else
            {
                cklKP.UnCheckAll();
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            XuatFileExcel();
        }
        void XuatFileExcel()
        {
            var file = (from a in _dataContext.DichVus.Where(p => p.PLoai == 2 && p.TrongDM == 1 && p.Status == 1)
                        select new
                        {
                            a.MaQD,
                            a.TenDV,
                            a.DonGiaTT39,
                            a.SoQD,
                        }).ToList();
            string[] _arr = new string[] { "0", "@", "@", "@", "@", "@", "@", "@" };
            int[] _arrWidth = new int[] { };
            int num = 1;
            DungChung.Bien.MangHaiChieu = new Object[file.Count + 1, 8];
            string[] _tieude = { "STT", "MA_DVKT", "TEN_DVKT", "MA_GIA", "DON_GIA", "QUYET_DINH", "CONG_BO", "MA_COSOKCB" };
            for (int i = 0; i < _tieude.Length; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i].ToString();
            }
            foreach (var item in file)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = num;
                DungChung.Bien.MangHaiChieu[num, 1] = item.MaQD;
                DungChung.Bien.MangHaiChieu[num, 2] = item.TenDV;
                DungChung.Bien.MangHaiChieu[num, 3] = " ";
                DungChung.Bien.MangHaiChieu[num, 4] = item.DonGiaTT39;
                DungChung.Bien.MangHaiChieu[num, 5] = item.SoQD;
                DungChung.Bien.MangHaiChieu[num, 6] = " ";
                DungChung.Bien.MangHaiChieu[num, 7] = DungChung.Bien.MaBV;
                num++;
            }
            QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "Báo cáo danh mục kĩ thuật", "‪D:\\xuat\\BC_DMKT.xls", true);
        }

        private void labelControl29_Click(object sender, EventArgs e)
        {

        }

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grvDichVu_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if(grvDichVu.RowCount == 1)
            {
                if (grvDichVu.GetFocusedRowCellValue(colMaDV) != null)
                {
                    int madv = Convert.ToInt32(grvDichVu.GetFocusedRowCellValue(colMaDV));
                    txtMaDV.Text = madv.ToString();
                    if (_ldv.Where(p => p.MaDV == madv).ToList().Count > 0)
                    {
                        txtMaTam.Text = _ldv.Where(p => p.MaDV == madv).First().MaTam;
                        txtSoTT_BYT.Text = _ldv.Where(p => p.MaDV == madv).First().SoTTqd;
                        txtTenDV.Text = _ldv.Where(p => p.MaDV == madv).First().TenDV;
                        cboDonVi.Text = _ldv.Where(p => p.MaDV == madv).First().DonVi;
                        mmCanhBao.Text = _ldv.Where(p => p.MaDV == madv).First().CanhBaoHM;
                        txtGiaPhuThu.Text = _ldv.Where(p => p.MaDV == madv).First().GiaPhuThu.ToString();
                        txtDonGia.Text = _ldv.Where(p => p.MaDV == madv).First().DonGia.ToString();
                        if (_ldv.Where(p => p.MaDV == madv).First().BHTT != null)
                            txtPTram.Text = _ldv.Where(p => p.MaDV == madv).First().BHTT.ToString();
                        if (_ldv.Where(p => p.MaDV == madv).First().IDNhom != null)
                            lupPhanLoai.EditValue = _ldv.Where(p => p.MaDV == madv).First().IDNhom.Value;
                        if (_ldv.Where(p => p.MaDV == madv).First().IdTieuNhom != null)
                            lupNhom.EditValue = _ldv.Where(p => p.MaDV == madv).First().IdTieuNhom.Value;
                        if (_ldv.Where(p => p.MaDV == madv).First().TrongDM != null)
                        {
                            cboTrongDM.SelectedIndex = _ldv.Where(p => p.MaDV == madv).First().TrongDM.Value;
                        }
                        if (_ldv.Where(p => p.MaDV == madv).First().Status != null)
                        {
                            if (_ldv.Where(p => p.MaDV == madv).First().Status == 1)
                                chkStatus.Checked = true;
                            else
                                chkStatus.Checked = false;
                        }
                        else
                            chkStatus.Checked = false;

                        if (_ldv.Where(p => p.MaDV == madv).First().IsAutoExecute != true)
                            chkAutoExcecute.Checked = false;
                        else
                            chkAutoExcecute.Checked = true;

                        if (_ldv.Where(p => p.MaDV == madv).First().DVKTC != null)
                        {
                            if (_ldv.Where(p => p.MaDV == madv).First().DVKTC == 1)
                                chkKTC.Checked = true;
                            else
                                chkKTC.Checked = false;
                        }

                        if (_ldv.Where(p => p.MaDV == madv).First().IS_EXECUTE_CLS != null)
                        {
                            if (_ldv.Where(p => p.MaDV == madv).First().IS_EXECUTE_CLS == true)
                            {
                                chkCLS.Checked = true;
                            }
                            else
                            {
                                chkCLS.Checked = false;
                            }
                        }

                        //chkCLS.Checked == _ldv.Where(p => p.MaDV == madv).First().IS_EXECUTE_CLS ?? false;

                        if (_ldv.Where(p => p.MaDV == madv).First().SoTT != null)
                            txtSoTT.Text = _ldv.Where(p => p.MaDV == madv).First().SoTT.Value.ToString();
                        else
                            txtSoTT.Text = "";
                        txtSoPhutTH.Text = _ldv.Where(p => p.MaDV == madv).First().SoPhutThucHien.ToString();
                        txtGHCD.Text = _ldv.Where(p => p.MaDV == madv).First().GHCDTrongNgay.ToString();
                        txtQCPC.Text = _ldv.Where(p => p.MaDV == madv).First().QCPC;
                        txtTenKhac.Text = _ldv.Where(p => p.MaDV == madv).First().TenKhac;
                        txtPhuongPhap.Text = _ldv.Where(p => p.MaDV == madv).First().TenPhuongPhap;
                        txtMaQD.Text = _ldv.Where(p => p.MaDV == madv).First().MaQD;
                        txtTenRG.Text = _ldv.Where(p => p.MaDV == madv).First().TenRG;
                        if (_ldv.Where(p => p.MaDV == madv).First().PLoai != null)
                            cboPLoai.Text = _ldv.Where(p => p.MaDV == madv).First().PLoai.ToString();
                        else
                            cboPLoai.Text = "2";
                        txtDonGia2.Text = _ldv.Where(p => p.MaDV == madv).First().DonGia2.ToString();
                        txtGiaDV2.Text = _ldv.Where(p => p.MaDV == madv).First().DonGiaDV2.ToString();
                        if (_ldv.Where(p => p.MaDV == madv).First().Loai != null)
                        {
                            cboLoai.SelectedIndex = _ldv.Where(p => p.MaDV == madv).First().Loai.Value;
                            if (cboLoai.SelectedIndex == 0 && (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049"))
                            {
                                cboLoai.Text = "Đặc biệt";
                            }
                        }
                        else
                        {
                            cboLoai.SelectedIndex = -1;
                        }
                        txtSoQD.Text = _ldv.Where(p => p.MaDV == madv).First().SoQD;
                        txtGiaBHTT.Text = _ldv.Where(p => p.MaDV == madv).First().DonGiaBHYT.ToString();
                        txtdongiatt15.Text = _ldv.Where(p => p.MaDV == madv).First().DonGiaTT15.ToString();
                        txtGiaTT39.Text = _ldv.Where(p => p.MaDV == madv).First().DonGiaTT39.ToString();
                        txtGiaBHGioiHanTT.Text = _ldv.Where(p => p.MaDV == madv).First().GiaBHGioiHanTT.ToString();
                        txtMaPTTTQT.Text = _ldv.Where(p => p.MaDV == madv).First().MaNhom;
                        txtHanMuc.Text = _ldv.Where(p => p.MaDV == madv).First().HMChiDinh.ToString();
                        _loadKPsd(_ldv.Where(p => p.MaDV == madv).First().MaKPsd, _lKPsd, cklKP);
                        _loadDSDTBN(_ldv.Where(p => p.MaDV == madv).First().DSDTBN, _lDTBN, cklDTBN);
                    }
                }
                else
                {
                    resetcontrol();
                }
            }
        }
    }
}
