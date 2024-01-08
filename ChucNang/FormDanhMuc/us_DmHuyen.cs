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
    public partial class us_DmHuyen : DevExpress.XtraEditors.XtraUserControl
    {
        public us_DmHuyen()
        {
            InitializeComponent();
        }
        int TTLuu = 0;
        //int TTXoa = 0;
        string _mahuyen = "";
        private void enableControl(bool T)
        {
            txtMaHuyen.Properties.ReadOnly = !T;
            txtTenHuyen.Properties.ReadOnly = !T;
            lupMaTinh.Properties.ReadOnly = !T;
            btnLuu.Enabled = T;
            btnMoi.Enabled = !T;
            btnSua.Enabled = !T;
            btnXoa.Enabled = !T;
            grcDmHuyen.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaHuyen.Text = "";
            txtTenHuyen.Text = "";
            lupMaTinh.Text = "";
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaHuyen.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã huyện");
                txtMaHuyen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenHuyen.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên huyện");
                txtTenHuyen.Focus();
                return false;
            }

            return true;
        }

        #endregion
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DmHuyen> _lHuyen = new List<DmHuyen>();
        private void us_DmHuyen_Load(object sender, EventArgs e)
        {
            var qq = from dmt in dataContext.DmTinhs select new { dmt.TenTinh, dmt.MaTinh};
            lupMaTinh.Properties.DataSource = qq;
            _lHuyen = dataContext.DmHuyens.OrderBy(p => p.TenHuyen).ToList();
            grcDmHuyen.DataSource = _lHuyen;
            enableControl(false);
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            txtMaHuyen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaHuyen.Enabled = false;
            TTLuu = 2;
            txtTenHuyen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            _mahuyen = txtMaHuyen.Text;
            var ma = dataContext.TTboXungs.Where(p => p.MaHuyen== (_mahuyen)).ToList();
            if (ma.Count > 0)
            {
                MessageBox.Show("Mã huyện đã được sử dụng, Bạn không thể xóa.");
            }
            else
            {
                DataRow dr = grvDmHuyen.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa huyện đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    //grcDanToc.ControlRemoved();
                    var xoa = dataContext.DmHuyens.Single(p => p.MaHuyen== (_mahuyen));
                    dataContext.DmHuyens.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoa.Enabled = true;

                }
                _lHuyen = dataContext.DmHuyens.OrderBy(p => p.TenHuyen).ToList();
                grcDmHuyen.DataSource = _lHuyen.ToList();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        _mahuyen = txtMaHuyen.Text.Trim();
                        var ma = dataContext.DmHuyens.Where(p => p.MaHuyen== (_mahuyen)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã huyện đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            DmHuyen huyen = new DmHuyen();
                            huyen.MaHuyen = txtMaHuyen.Text;
                            huyen.TenHuyen = txtTenHuyen.Text;
                            huyen.MaTinh = lupMaTinh.Text;
                            dataContext.DmHuyens.Add(huyen);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMaHuyen.Text))
                        {
                            string mahuyen = txtMaHuyen.Text;
                            DmHuyen huyensua = dataContext.DmHuyens.Single(p => p.MaHuyen== (mahuyen));
                            huyensua.TenHuyen = txtTenHuyen.Text;
                            huyensua.MaTinh = lupMaTinh.Text;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }
                _lHuyen = dataContext.DmHuyens.OrderBy(p => p.TenHuyen).ToList();
                grcDmHuyen.DataSource = _lHuyen.ToList();
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text;
            }
            grcDmHuyen.DataSource = _lHuyen.Where(p => p.TenHuyen.Contains(tk));
        }

        private void grvDmHuyen_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDmHuyen.GetFocusedRowCellValue(colMaHuyen) != null && grvDmHuyen.GetFocusedRowCellValue(colMaHuyen).ToString() != "")
            {
                txtMaHuyen.Text = grvDmHuyen.GetFocusedRowCellValue(colMaHuyen).ToString();
                if (grvDmHuyen.GetFocusedRowCellValue(colTenHuyen) != null && grvDmHuyen.GetFocusedRowCellValue(colTenHuyen).ToString() != "")
                {
                    txtTenHuyen.Text = grvDmHuyen.GetFocusedRowCellValue(colTenHuyen).ToString();
                }
                else
                {
                    txtTenHuyen.Text = "";
                }

                if (grvDmHuyen.GetFocusedRowCellValue(colTinh) != null && grvDmHuyen.GetFocusedRowCellValue(colTinh).ToString() != "")
                {
                    lupMaTinh.Text=grvDmHuyen.GetFocusedRowCellValue(colTinh).ToString();
                }
                else
                {
                    lupMaTinh.Text = "";
                }
            }
            else
            {
                txtMaHuyen.Text = "";
            }
        
        }
    }
}
