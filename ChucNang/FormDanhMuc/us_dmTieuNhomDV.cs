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
    public partial class us_dmTieuNhomDV : DevExpress.XtraEditors.XtraUserControl
    {
        public us_dmTieuNhomDV()
        {
            InitializeComponent();
        }
        int _TTLuu = 0;
        string _tentn = "";
        int _id = 0;
        private void enableControl(bool T)
        {
            txtTenTN.Enabled = T;
            txtID.Enabled = T;
            cboTenRG.Enabled = T;
            lupTenNhomDV.Enabled = T;
            cboStatus.Enabled = T;
            btnLuuKb.Enabled = T;
            btnMoiKb.Enabled = !T;
            btnSuaKb.Enabled = !T;
            btnXoaKb.Enabled = !T;
            txtSoTT.Enabled = T;
            grcTieuNhomDV.Enabled = !T;
        }
        private void resetControl()
        {
            txtTenTN.Text = "";
            lupTenNhomDV.EditValue = "";
            cboStatus.SelectedIndex = 0;
            txtID.Text = "";
            txtSoTT.ResetText();
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã tiểu nhóm dịch vụ");
                txtID.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenTN.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên tiểu nhóm dịch vụ");
                txtTenTN.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupTenNhomDV.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhóm dịch vụ");
                lupTenNhomDV.Focus();
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
        List<TieuNhomDV> _lTieuNhomDV = new List<TieuNhomDV>();
        private void us_dmTieuNhomDV_Load(object sender, EventArgs e)
        {
            var qndv = from ndv in dataContext.NhomDVs select new { ndv.IDNhom, ndv.TenNhom };
            lupTenNhomDV.Properties.DataSource = qndv.ToList();
            lupTNhom.DataSource = qndv.ToList();
            _lTieuNhomDV = dataContext.TieuNhomDVs.OrderBy(p => p.TenTN).ToList();
            grcTieuNhomDV.DataSource = _lTieuNhomDV;
            cboTenRG.Properties.DataSource = (from a in DungChung.Bien._lChuyenKhoa where a.LoaiCK == 2 || a.LoaiCK == 3 select new { a.MaCK, a.ChuyenKhoa });
            enableControl(false);
        }



        private void btnLuuKb_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (_TTLuu)
                {
                    case 1:
                        {
                            _TTLuu = 0;
                            TieuNhomDV tndv = new TieuNhomDV();
                            tndv.TenTN = txtTenTN.Text;
                            if (lupTenNhomDV.EditValue != null)
                                tndv.IDNhom = int.Parse(lupTenNhomDV.EditValue.ToString());
                            tndv.IdTieuNhom = Convert.ToInt32(txtID.Text);
                            tndv.TenRG = cboTenRG.Text;
                            tndv.Status = cboStatus.SelectedIndex;
                            if (!string.IsNullOrEmpty(txtSoTT.Text))
                                tndv.STT = Convert.ToByte(txtSoTT.Text);
                            else
                                tndv.STT = 0;
                            dataContext.TieuNhomDVs.Add(tndv);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");

                        }
                        break;

                    case 2:
                        if (!string.IsNullOrEmpty(lupTenNhomDV.Text))
                        {
                            int _idnhom = 0;
                            if (lupTenNhomDV.EditValue != null && lupTenNhomDV.EditValue.ToString() != "")
                                _idnhom = Convert.ToInt32(lupTenNhomDV.EditValue.ToString());
                            _TTLuu = 0;
                            if (!string.IsNullOrEmpty(txtID.Text))
                                _id = int.Parse(txtID.Text);
                            var tndvsua = dataContext.TieuNhomDVs.Single(p => p.IdTieuNhom == _id);
                            tndvsua.TenTN = txtTenTN.Text;
                            tndvsua.IDNhom = _idnhom;
                            tndvsua.TenRG = cboTenRG.Text;
                            tndvsua.Status = cboStatus.SelectedIndex;
                            if (!string.IsNullOrEmpty(txtSoTT.Text))
                                tndvsua.STT = Convert.ToByte(txtSoTT.Text);
                            else
                                tndvsua.STT = 0;
                            dataContext.SaveChanges();
                            var dichvu = dataContext.DichVus.Where(p => p.IdTieuNhom == _id).ToList();
                            foreach (var a in dichvu)
                            {
                                a.IDNhom = _idnhom;
                                dataContext.SaveChanges();
                            }
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);

                        }
                        break;
                }
                _lTieuNhomDV = dataContext.TieuNhomDVs.OrderBy(p => p.TenTN).ToList();
                grcTieuNhomDV.DataSource = _lTieuNhomDV.ToList();
            }
        }

        private void btnSuaKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            //txtSTT.Enabled = false;
            txtID.Properties.ReadOnly = true;
            _TTLuu = 2;
            txtTenTN.Focus();
        }

        private void btnXoaKb_Click(object sender, EventArgs e)
        {
            _id = int.Parse(txtID.Text);
            var dv = dataContext.DichVus.Where(p => p.IdTieuNhom == _id).ToList();
            if (dv.Count > 0)
            {
                MessageBox.Show("Tiểu nhóm dịch vụ đã được sử dụng, Bạn không thể xóa.");
            }
            else
            {
                DataRow dr = grvTieuNhomDV.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa tiểu nhóm dịch vụ đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    //grcDanToc.ControlRemoved();
                    var xoa = dataContext.TieuNhomDVs.Single(p => p.IdTieuNhom == _id);
                    dataContext.TieuNhomDVs.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoaKb.Enabled = true;

                }
                _lTieuNhomDV = dataContext.TieuNhomDVs.OrderBy(p => p.TenTN).ToList();
                grcTieuNhomDV.DataSource = _lTieuNhomDV.ToList();
            }
        }
        private void btnMoiKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            _TTLuu = 1;
            cboStatus.SelectedIndex = 0;
            txtTenTN.Focus();
        }

        private void grvTieuNhomDV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvTieuNhomDV.GetFocusedRowCellValue(colTenTN) != null && grvTieuNhomDV.GetFocusedRowCellValue(colTenTN).ToString() != "")
            {
                txtTenTN.Text = grvTieuNhomDV.GetFocusedRowCellValue(colTenTN).ToString();
                if (grvTieuNhomDV.GetFocusedRowCellValue(colIDNhom) != null && grvTieuNhomDV.GetFocusedRowCellValue(colIDNhom).ToString() != "")
                {

                    lupTenNhomDV.EditValue = Convert.ToInt32(grvTieuNhomDV.GetFocusedRowCellValue(colIDNhom));

                }
                else
                {
                    lupTenNhomDV.EditValue = null;
                }

                if (grvTieuNhomDV.GetFocusedRowCellValue(colIdTieuNhom) != null && grvTieuNhomDV.GetFocusedRowCellValue(colIdTieuNhom).ToString() != "")
                {
                    txtID.Text = grvTieuNhomDV.GetFocusedRowCellValue(colIdTieuNhom).ToString();
                }
                else
                {
                    txtID.Text = "";
                }
                if (grvTieuNhomDV.GetFocusedRowCellValue(colTenRG) != null && grvTieuNhomDV.GetFocusedRowCellValue(colTenRG).ToString() != "")
                {
                    cboTenRG.Text = grvTieuNhomDV.GetFocusedRowCellValue(colTenRG).ToString();
                }
                else
                {
                    cboTenRG.Text = "";
                }
                if (grvTieuNhomDV.GetFocusedRowCellValue(colStatus) != null && grvTieuNhomDV.GetFocusedRowCellValue(colStatus).ToString() != "")
                {
                    int a = int.Parse(grvTieuNhomDV.GetFocusedRowCellValue(colStatus).ToString());

                    cboStatus.SelectedIndex = a;
                }
                else
                {
                    cboStatus.Text = "";
                }
                if (grvTieuNhomDV.GetFocusedRowCellValue(colSTT) != null && grvTieuNhomDV.GetFocusedRowCellValue(colSTT).ToString() != "")
                {
                    txtSoTT.Text = grvTieuNhomDV.GetFocusedRowCellValue(colSTT).ToString();
                }
                else
                {
                    txtSoTT.Text = "";
                }
            }
            else
            {
                cboStatus.Text = "";
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text.ToLower();
            }
            grcTieuNhomDV.DataSource = _lTieuNhomDV.Where(p => p.TenTN.ToLower().Contains(tk));

        }

        private void grvTieuNhomDV_DataSourceChanged(object sender, EventArgs e)
        {
            grvTieuNhomDV_FocusedRowChanged(null, null);
        }

        private void txtSoTT_Leave(object sender, EventArgs e)
        {
            if (_TTLuu == 1 || _TTLuu == 2)
            {
                int stt = 0, idtn = 0;
                if (!string.IsNullOrEmpty(txtID.Text) && !string.IsNullOrEmpty(txtSoTT.Text))
                {
                    idtn = Convert.ToInt32(txtID.Text);
                    stt = Convert.ToByte(txtSoTT.Text);
                    var kt = dataContext.TieuNhomDVs.Where(p => p.STT == stt).ToList();
                    if (kt.Count > 0)
                    {
                        if (kt.Where(p => p.IdTieuNhom == idtn).ToList().Count <= 0)
                        {
                            MessageBox.Show("Số thứ tự đã tồn tại, bạn hãy nhập số khác!");
                            txtSoTT.Focus();
                        }
                    }
                }
            }
        }



    }
}
