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
    public partial class Frm_DmTinh : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DmTinh()
        {
            InitializeComponent();
        }
        int TTLuu = 0;
        //int TTXoa = 0;
        string _matinh = "";
        private void enableControl(bool T)
        {
            txtMaTinh.Properties.ReadOnly = !T;
            txtTenTinh.Properties.ReadOnly = !T;
            cboStatus.Properties.ReadOnly = !T;
            btnLuu.Enabled = T;
            btnMoi.Enabled = !T;
            btnSua.Enabled = !T;
            btnXoa.Enabled = !T;
            grcDmTinh.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaTinh.Text = "";
            txtTenTinh.Text = "";
            cboStatus.Text = "";
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaTinh.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã tỉnh");
                txtMaTinh.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenTinh.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên tỉnh");
                txtTenTinh.Focus();
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
        List<DmTinh> _lTinh = new List<DmTinh>();
        private void Frm_DmTinh_Load(object sender, EventArgs e)
        {
            _lTinh = dataContext.DmTinhs.OrderBy(p => p.TenTinh).ToList();
            grcDmTinh.DataSource = _lTinh;
            enableControl(false);
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            cboStatus.SelectedIndex = 0;
            txtMaTinh.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaTinh.Enabled = false;
            TTLuu = 2;
            txtTenTinh.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            _matinh = txtMaTinh.Text;
            var ma = dataContext.TTboXungs.Where(p => p.MaTinh== (_matinh)).ToList();
            if (ma.Count > 0)
            {
                MessageBox.Show("Mã tỉnh đã được sử dụng, Bạn không thể xóa.");
            }
            else
            {
                DataRow dr = grvDmTinh.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa tỉnh đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    //grcDanToc.ControlRemoved();
                    var xoa = dataContext.DmTinhs.Single(p => p.MaTinh== (_matinh));
                    dataContext.DmTinhs.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoa.Enabled = true;

                }
                _lTinh = dataContext.DmTinhs.OrderBy(p => p.TenTinh).ToList();
                grcDmTinh.DataSource = _lTinh.ToList();
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        _matinh = txtMaTinh.Text.Trim();
                        var ma = dataContext.DmTinhs.Where(p => p.MaTinh== (_matinh)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã tỉnh đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            DmTinh tinh = new DmTinh();
                            tinh.MaTinh = txtMaTinh.Text;
                            tinh.TenTinh = txtTenTinh.Text;
                            if (cboStatus.SelectedIndex == 0)
                                tinh.Status = 1;
                            else
                            {
                                if (cboStatus.SelectedIndex == 1)
                                {
                                    tinh.Status = 0;
                                }
                            }
                            dataContext.DmTinhs.Add(tinh);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMaTinh.Text))
                        {
                            string matinh = txtMaTinh.Text;
                            DmTinh tinhsua = dataContext.DmTinhs.Single(p => p.MaTinh== (matinh));
                            tinhsua.TenTinh = txtTenTinh.Text;
                            if (cboStatus.SelectedIndex == 0)
                                tinhsua.Status = 1;
                            else
                            {
                                if (cboStatus.SelectedIndex == 1)
                                {
                                    tinhsua.Status = 0;
                                }
                            }
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }
                _lTinh = dataContext.DmTinhs.OrderBy(p => p.TenTinh).ToList();
                grcDmTinh.DataSource = _lTinh.ToList();
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text.ToLower();
            }
            grcDmTinh.DataSource = _lTinh.Where(p => p.TenTinh.ToLower().Contains(tk));
        }

        private void grvDmTinh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDmTinh.GetFocusedRowCellValue(colMaTinh) != null && grvDmTinh.GetFocusedRowCellValue(colMaTinh).ToString() != "")
            {
                txtMaTinh.Text = grvDmTinh.GetFocusedRowCellValue(colMaTinh).ToString();
                if (grvDmTinh.GetFocusedRowCellValue(colTenTinh) != null && grvDmTinh.GetFocusedRowCellValue(colTenTinh).ToString() != "")
                {
                    txtTenTinh.Text = grvDmTinh.GetFocusedRowCellValue(colTenTinh).ToString();
                }
                else
                {
                    txtTenTinh.Text = "";
                }

                if (grvDmTinh.GetFocusedRowCellValue(colStatus) != null && grvDmTinh.GetFocusedRowCellValue(colStatus).ToString() != "")
                {
                    int a = int.Parse(grvDmTinh.GetFocusedRowCellValue(colStatus).ToString());
                    switch (a)
                    {
                        case 1:
                            cboStatus.SelectedIndex = 0;
                            break;
                        case 0:
                            cboStatus.SelectedIndex = 1;
                            break;
                    }
                }
                else
                {
                    cboStatus.Text = "";
                }
            }
            else
            {
                txtMaTinh.Text = "";
            }

        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            enableControl(false);
        }
    }
}