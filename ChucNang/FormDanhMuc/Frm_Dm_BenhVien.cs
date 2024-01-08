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
    public partial class Frm_Dm_BenhVien : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Dm_BenhVien()
        {
            InitializeComponent();
        }
        string _macs = "";
        public Frm_Dm_BenhVien(string ma)
        {
            InitializeComponent();
            _macs = ma;
        }
        int TTLuu = 0;
        //int TTXoa = 0;
        string Mabv = "";
        private void enableControl(bool T)
        {
            txtMaBV.Properties.ReadOnly = !T;
            txtTenBV.Properties.ReadOnly = !T;
            txtDiaChi.Properties.ReadOnly = !T;
            txtMaTinh.Properties.ReadOnly = !T;
            txtMaHuyen.Properties.ReadOnly = !T;
            txtMaChuQuan.Properties.ReadOnly = !T;
            cmbTuyenBV.Properties.ReadOnly = !T;
            txtHangBV.Properties.ReadOnly = !T;
            cboStatus.Properties.ReadOnly = !T;
            btnLuuBV.Enabled = T;
            btnMoiBV.Enabled = !T;
            btnSuaBV.Enabled = !T;
            grcBenhVien.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaBV.Text = "";
            txtTenBV.Text = "";
            txtDiaChi.Text = "";
            txtMaTinh.Text = "";
            txtMaHuyen.Text = "";
            txtMaChuQuan.Text = "";
            txtHangBV.Text = "";
            cmbTuyenBV.Text = "";
            cboStatus.Text = "";
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaBV.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã bệnh viện");
                txtMaBV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenBV.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên bệnh viện");
                txtTenBV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ bệnh viện");
                txtDiaChi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaTinh.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã tỉnh của bệnh viện");
                txtMaTinh.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaHuyen.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã huyện của bệnh viện");
                txtMaHuyen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaChuQuan.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã chủ quản bệnh viện");
                txtMaChuQuan.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cmbTuyenBV.Text))
            {
                MessageBox.Show("Bạn chưa nhập tuyến bệnh viện");
                cmbTuyenBV.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboStatus.Text))
            {
                MessageBox.Show("Bạn chưa chọn trạng thái");
                cboStatus.Focus();
                return false;
            }
            return true;
        }

        #endregion
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<BenhVien> _lBVien = new List<BenhVien>();

        private void Frm_Dm_BenhVien_Load(object sender, EventArgs e)
        {
            _lBVien = dataContext.BenhViens.OrderBy(p => p.TenBV).ToList();
            grcBenhVien.DataSource = _lBVien;
            enableControl(false);
            if (!string.IsNullOrEmpty(_macs))
            {
                btnMoiBV_Click(sender, e);
                txtMaBV.Text = _macs;
            }
        }

        private void btnMoiBV_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            cboStatus.SelectedIndex = 0;
            txtMaBV.Focus();
        }

        private void btnSuaBV_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaBV.Enabled = false;
            TTLuu = 2;
            txtTenBV.Focus();
        }

        private void btnLuuBV_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        Mabv = txtMaBV.Text.Trim();
                        var ma = dataContext.BenhViens.Where(p => p.MaBV == (Mabv)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã bệnh viện đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            BenhVien bv = new BenhVien();
                            bv.MaBV = txtMaBV.Text;
                            bv.TenBV = txtTenBV.Text;
                            bv.DiaChi = txtDiaChi.Text;
                            bv.MaTinh = txtMaTinh.Text;
                            bv.MaHuyen = txtMaHuyen.Text;
                            bv.MaChuQuan = txtMaChuQuan.Text;
                            if (!string.IsNullOrEmpty(txtHangBV.Text))
                                bv.HangBV = Convert.ToInt32(txtHangBV.Text);
                            else
                                bv.HangBV = -1;
                            bv.TuyenBV = cmbTuyenBV.Text;
                            bv.status = cboStatus.SelectedIndex;
                            bv.Connect = chkConnect.Checked;
                            dataContext.BenhViens.Add(bv);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMaBV.Text))
                        {
                            string mabv = txtMaBV.Text;
                            BenhVien bvsua = dataContext.BenhViens.Single(p => p.MaBV == (mabv));
                            bvsua.TenBV = txtTenBV.Text;
                            bvsua.DiaChi = txtDiaChi.Text;
                            bvsua.MaTinh = txtMaTinh.Text;
                            bvsua.MaHuyen = txtMaHuyen.Text;
                            bvsua.MaChuQuan = txtMaChuQuan.Text;
                            if (!string.IsNullOrEmpty(txtHangBV.Text))
                                bvsua.HangBV = Convert.ToInt32(txtHangBV.Text);
                            else
                                bvsua.HangBV = -1;
                            bvsua.TuyenBV = cmbTuyenBV.Text;
                            bvsua.status = cboStatus.SelectedIndex;
                            bvsua.Connect = chkConnect.Checked;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }
                _lBVien = dataContext.BenhViens.OrderBy(p => p.TenBV).ToList();
                grcBenhVien.DataSource = _lBVien.ToList();
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text;

                grcBenhVien.DataSource = _lBVien.Where(p => p.TenBV.Contains(tk));
            }
            else grcBenhVien.DataSource = _lBVien;
        }

        private void grvBenhVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvBenhVien.GetFocusedRowCellValue(colMaBV) != null && grvBenhVien.GetFocusedRowCellValue(colMaBV).ToString() != "")
            {
                txtMaBV.Text = grvBenhVien.GetFocusedRowCellValue(colMaBV).ToString();
                var benhvien = _lBVien.Where(p => p.MaBV == txtMaBV.Text).FirstOrDefault();
                if (benhvien != null)
                {
                    txtTenBV.Text = benhvien.TenBV;
                    txtDiaChi.Text = benhvien.DiaChi;
                    txtMaTinh.Text = benhvien.MaTinh;
                    txtMaHuyen.Text = benhvien.MaHuyen;
                    txtMaChuQuan.Text = benhvien.MaChuQuan;
                    txtHangBV.Text = benhvien.HangBV == null ? "" : benhvien.HangBV.ToString();
                    cmbTuyenBV.Text = benhvien.TuyenBV;
                    cboStatus.SelectedIndex = benhvien.status ?? -1;
                    cboStatus.SelectedIndex = -1;
                    chkConnect.Checked = benhvien.Connect;
                }
                else
                {
                    txtTenBV.Text = "";
                    txtDiaChi.Text = "";
                    txtMaTinh.Text = "";
                    txtMaChuQuan.Text = "";
                    txtMaHuyen.Text = "";
                    txtHangBV.Text = "";
                    cmbTuyenBV.Text = "";
                }
            }
            else
            {
                txtTenBV.Text = "";
                txtDiaChi.Text = "";
                txtMaTinh.Text = "";
                txtMaChuQuan.Text = "";
                txtMaHuyen.Text = "";
                txtHangBV.Text = "";
                cmbTuyenBV.Text = "";
                txtMaBV.Text = "";
            }
        }

        private void grcBenhVien_Click(object sender, EventArgs e)
        {

        }
    }
}