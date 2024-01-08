using System;using QLBV_Database;
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
    public partial class us_dmNhaCungCap : DevExpress.XtraEditors.XtraUserControl
    {
        public us_dmNhaCungCap()
        {
            InitializeComponent();
        }

        int TTLuu = 0;
        //int TTXoa = 0;
        string _MaNCC = "";
        private void enableControl(bool T)
        {
            txtMaNCC.Properties.ReadOnly = !T;
            txtTenNCC.Properties.ReadOnly = !T;
            txtDiaChi.Properties.ReadOnly = !T;
            txtDienThoai.Properties.ReadOnly = !T;
            txtNguoiCC.Properties.ReadOnly = !T;
            cboStatus.Properties.ReadOnly = !T;
            btnLuuKb.Enabled = T;
            btnMoiKb.Enabled = !T;
            btnSuaKb.Enabled = !T;
            btnXoaKb.Enabled = !T;
            grcNhaCC.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtNguoiCC.Text = "";
            cboStatus.Text = "";
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã Nhà cung cấp");
                txtMaNCC.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenNCC.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên Nhà cung cấp");
                txtTenNCC.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDienThoai.Text))
            {
                MessageBox.Show("Bạn chưa nhập điện thoại của Nhà cung cấp");
                txtDienThoai.Focus();
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
        private bool KTXoa()
        {
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã Nhà cung cấp");
                txtMaNCC.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenNCC.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên Nhà cung cấp");
                txtTenNCC.Focus();
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
        List<NhaCC> _lNCC = new List<NhaCC>();

        private void us_dmNhaCungCap_Load(object sender, EventArgs e)
        {
            _lNCC = dataContext.NhaCCs.OrderBy(p => p.TenCC).ToList();
            grcNhaCC.DataSource = _lNCC;
            enableControl(false);
        }
        private void btnMoiKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            cboStatus.SelectedIndex = 0;
            txtMaNCC.Focus();
        }

        private void btnSuaKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaNCC.Enabled = false;
            TTLuu = 2;
            txtTenNCC.Focus();
        }

        private void btnXoaKb_Click(object sender, EventArgs e)
        {
            _MaNCC = txtMaNCC.Text;
            var ma = dataContext.NhapDs.Where(p => p.MaCC== (_MaNCC)).ToList();
            if (ma.Count > 0)
            {
                MessageBox.Show("Mã nhà cung cấp đã được sử dụng, Bạn không thể xóa.");
            }
            else
            {
                DataRow dr = grvNhaCC.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    var xoa = dataContext.NhaCCs.Single(p => p.MaCC== (_MaNCC));
                    dataContext.NhaCCs.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoaKb.Enabled = true;

                }
                _lNCC = dataContext.NhaCCs.OrderBy(p => p.TenCC).ToList();
                grcNhaCC.DataSource = _lNCC.ToList();
            }
        }

        private void btnLuuKb_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        _MaNCC = txtMaNCC.Text.Trim();
                        var ma = dataContext.NhaCCs.Where(p => p.MaCC== (_MaNCC)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã Nhà cung cấp đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            NhaCC ncc = new NhaCC();
                            ncc.MaCC = txtMaNCC.Text.Trim();
                            ncc.TenCC = txtTenNCC.Text.Trim();
                            ncc.DiaChi = txtDiaChi.Text.Trim();
                            ncc.DienThoai = txtDienThoai.Text.Trim();
                            ncc.NguoiCC = txtNguoiCC.Text.Trim();
                           ncc.Status= cboStatus.SelectedIndex;
                            dataContext.NhaCCs.Add(ncc);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                          }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMaNCC.Text))
                        {
                            string maNCC = txtMaNCC.Text.Trim();
                            NhaCC nccsua = dataContext.NhaCCs.Single(p => p.MaCC== (maNCC));
                            nccsua.TenCC = txtTenNCC.Text.Trim();
                            nccsua.DiaChi = txtDiaChi.Text.Trim();
                            nccsua.DienThoai = txtDienThoai.Text;
                            nccsua.NguoiCC = txtNguoiCC.Text.Trim();
                            nccsua.Status = cboStatus.SelectedIndex;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);

                        }
                        break;
                }
                _lNCC = dataContext.NhaCCs.OrderBy(p => p.TenCC).ToList();
                grcNhaCC.DataSource = _lNCC.ToList();
            }
        }

           
        private void grvNhaCC_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvNhaCC.GetFocusedRowCellValue(colMaNCC) != null && grvNhaCC.GetFocusedRowCellValue(colMaNCC).ToString() != "")
            {
                txtMaNCC.Text = grvNhaCC.GetFocusedRowCellValue(colMaNCC).ToString();
                if (grvNhaCC.GetFocusedRowCellValue(colTenNCC) != null && grvNhaCC.GetFocusedRowCellValue(colTenNCC).ToString() != "")
                {
                    txtTenNCC.Text = grvNhaCC.GetFocusedRowCellValue(colTenNCC).ToString();
                }
                else
                {
                    txtTenNCC.Text = "";
                }
                if (grvNhaCC.GetFocusedRowCellValue(colDiaChi) != null && grvNhaCC.GetFocusedRowCellValue(colDiaChi).ToString() != "")
                {
                    txtDiaChi.Text = grvNhaCC.GetFocusedRowCellValue(colDiaChi).ToString();
                }
                else
                {
                    txtDiaChi.Text = "";
                }
                if (grvNhaCC.GetFocusedRowCellValue(colDienThoai) != null && grvNhaCC.GetFocusedRowCellValue(colDienThoai).ToString() != "")
                {
                    txtDienThoai.Text = grvNhaCC.GetFocusedRowCellValue(colDienThoai).ToString();
                }
                else
                {
                    txtDienThoai.Text = "";
                }
                if (grvNhaCC.GetFocusedRowCellValue(colNguoiCC) != null && grvNhaCC.GetFocusedRowCellValue(colNguoiCC).ToString() != "")
                {
                    txtNguoiCC.Text = grvNhaCC.GetFocusedRowCellValue(colNguoiCC).ToString();
                }
                else
                {
                    txtNguoiCC.Text = "";
                }
                if (grvNhaCC.GetFocusedRowCellValue(colStatus) != null && grvNhaCC.GetFocusedRowCellValue(colStatus).ToString() != "")
                {
                    int a = int.Parse(grvNhaCC.GetFocusedRowCellValue(colStatus).ToString());
                   
                            cboStatus.SelectedIndex =a;
                           
                }
                else
                {
                    cboStatus.SelectedIndex = -1;
                }
            }
            else
            {
                txtMaNCC.Text = "";
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text;
            }
            grcNhaCC.DataSource = _lNCC.Where(p => p.TenCC.ToLower().Contains(tk.ToLower()));
           
        }

        private void grvNhaCC_DataSourceChanged(object sender, EventArgs e)
        {
            grvNhaCC_FocusedRowChanged(null, null);
        }
       
    }
}
