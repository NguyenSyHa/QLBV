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
    public partial class Frm_Dm_ChuyenKhoa : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Dm_ChuyenKhoa()
        {
            InitializeComponent();
        }
        int TTLuu = 0;
        //int TTXoa = 0;
        int Mack = 0;
        private void enableControl(bool T)
        {
            txtMaCK.Properties.ReadOnly = !T;
            txtTenCK.Properties.ReadOnly = !T;
            txtTenCT.Properties.ReadOnly = !T;
            cbo_Status.Properties.ReadOnly = !T;
            btnLuu.Enabled = T;
            btnMoi.Enabled = !T;
            btnSua.Enabled = !T;
            grcChuyenKhoa.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaCK.Text = "";
            txtTenCK.Text = "";
            txtTenCT.Text = "";
            cbo_Status.SelectedIndex=-1;
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaCK.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã chuyên khoa");
                txtMaCK.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenCK.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên chuyên khoa");
                txtTenCK.Focus();
                return false;
            }

            return true;
        }

        #endregion
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<ChuyenKhoa> _lck = new List<ChuyenKhoa>();

        private void Frm_Dm_ChuyenKhoa_Load(object sender, EventArgs e)
        {
            _lck = dataContext.ChuyenKhoas.OrderBy(p => p.TenCK).ToList();
            grcChuyenKhoa.DataSource = _lck;
            enableControl(false);
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            txtMaCK.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaCK.Enabled = false;
            TTLuu = 2;
            txtTenCK.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Mack = int.Parse(txtMaCK.Text);
            DataRow dr = grvChuyenKhoa.GetFocusedDataRow();
            DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa chuyên khoa đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dia == DialogResult.Yes)
            {
                var xoa = dataContext.ChuyenKhoas.OrderBy(p => p.TenCK).Single(p => p.MaCK == Mack);
                dataContext.ChuyenKhoas.Remove(xoa);
                dataContext.SaveChanges();
                btnXoa.Enabled = true;

            }
            _lck = dataContext.ChuyenKhoas.OrderBy(p => p.TenCK).ToList();
            grcChuyenKhoa.DataSource = _lck.ToList();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        Mack = Convert.ToInt32(txtMaCK.Text);
                        var ma = dataContext.ChuyenKhoas.Where(p => p.MaCK== (Mack)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã chuyên khoa đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            ChuyenKhoa ck = new ChuyenKhoa();
                            ck.MaCK = Convert.ToInt32(txtMaCK.Text);
                            ck.TenCK = txtTenCK.Text;
                            ck.TenChiTiet = txtTenCT.Text;
                            ck.Status = cbo_Status.SelectedIndex;
                            dataContext.ChuyenKhoas.Add(ck);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMaCK.Text))
                        {
                            Mack = Convert.ToInt32(txtMaCK.Text);
                            ChuyenKhoa cksua = dataContext.ChuyenKhoas.Single(p => p.MaCK== (Mack));
                            cksua.MaCK = Convert.ToInt32(txtMaCK.Text);
                            cksua.TenCK = txtTenCK.Text;
                            cksua.TenChiTiet = txtTenCT.Text;
                            cksua.Status = cbo_Status.SelectedIndex;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }
                _lck = dataContext.ChuyenKhoas.OrderBy(p => p.TenCK).ToList();
                grcChuyenKhoa.DataSource = _lck.ToList();
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text;
            }
            grcChuyenKhoa.DataSource = _lck.Where(p => p.TenCK.Contains(tk));
        }

        private void grvChuyenKhoa_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int _maCK = 0;
            if (grvChuyenKhoa.GetFocusedRowCellValue(colMaCK) != null && grvChuyenKhoa.GetFocusedRowCellValue(colMaCK).ToString() != "")
            {
               _maCK=Convert.ToInt32(grvChuyenKhoa.GetFocusedRowCellValue(colMaCK));
                        
            }
            txtTenCK.Text = _maCK.ToString();
            if(_lck.Where(p=>p.MaCK==_maCK).Count()>0)   {
            txtTenCK.Text =     _lck.Where(p=>p.MaCK==_maCK).First().TenCK;
             txtTenCT.Text = _lck.Where(p=>p.MaCK==_maCK).First().TenChiTiet;
             cbo_Status.SelectedIndex = _lck.Where(p => p.MaCK == _maCK).First().Status;
            }
              
            else
            {
                txtMaCK.Text = "";
                txtTenCT.Text = "";
                cbo_Status.SelectedIndex = 0;
            }
        }
    }
}