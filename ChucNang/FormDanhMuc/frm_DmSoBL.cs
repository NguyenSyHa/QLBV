using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormDanhMuc
{
    public partial class frm_DmSoBL : DevExpress.XtraEditors.XtraForm
    {
        public frm_DmSoBL()
        {
            InitializeComponent();
        }
        List<SoBienLai> _lSoBienLai;
        QLBV_Database.QLBVEntities _data;
        private void grvDMSoBL_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDMSoBL.GetFocusedRowCellValue(colPLoai) != null)
                cboPLoai.SelectedIndex = Convert.ToInt32(grvDMSoBL.GetFocusedRowCellValue(colPLoai));
            else
                cboPLoai.SelectedIndex = -1;
            if (grvDMSoBL.GetFocusedRowCellValue(colQuyen) != null)
                txtQuyen.Text = grvDMSoBL.GetFocusedRowCellValue(colQuyen).ToString();
            else
                txtQuyen.Text = "";
            if (grvDMSoBL.GetFocusedRowCellValue(colSoTu) != null)
                txtTuSo.Text = grvDMSoBL.GetFocusedRowCellValue(colSoTu).ToString();
            else
                txtTuSo.Text = "";
            if (grvDMSoBL.GetFocusedRowCellValue(colSoDen) != null)
                txtDenSo.Text = grvDMSoBL.GetFocusedRowCellValue(colSoDen).ToString();
            else
                txtDenSo.Text = "";
            if (grvDMSoBL.GetFocusedRowCellValue(colSoHT) != null)
                txtSoHT.Text = grvDMSoBL.GetFocusedRowCellValue(colSoHT).ToString();
            else
                txtSoHT.Text = "";
            if (grvDMSoBL.GetFocusedRowCellValue(colStatus) != null)
                txtStatus.SelectedIndex =Convert.ToInt32(grvDMSoBL.GetFocusedRowCellValue(colStatus));
            if (grvDMSoBL.GetFocusedRowCellValue(colMaCB) != null)
                lupMaCB.EditValue = grvDMSoBL.GetFocusedRowCellValue(colMaCB).ToString();
            else
                lupMaCB.EditValue = "";
        }
        void EnableControl(bool b)
        {
            txtDenSo.ReadOnly = b;
            txtQuyen.ReadOnly = b;
            txtSoHT.ReadOnly = b;
            txtStatus.ReadOnly = b;
            txtTuSo.ReadOnly = b;
            cboPLoai.ReadOnly = b;
            lupMaCB.ReadOnly = b;
            btnMoi.Enabled = b;
            btnSua.Enabled = b;
            btnXoa.Enabled = b;
            btnLuu.Enabled = !b;
        }

        //public class SoBL
        //{
        //    public 
        //}
        private void frm_DmSoBL_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lSoBienLai = _data.SoBienLais.ToList();
            //var sobl=from bl in _lSoBienLai
            //         select new {
            //             bl.PLoai,
            //             bl
            //         }
            grcDMSoBL.DataSource = _lSoBienLai;
            EnableControl(true);
        }

        private void grvDMSoBL_DataSourceChanged(object sender, EventArgs e)
        {
            grvDMSoBL_FocusedRowChanged(null, null);
        }
        int TTLuu = 0;
        private void btnMoi_Click(object sender, EventArgs e)
        {
            EnableControl(false);
            TTLuu = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EnableControl(false);
            TTLuu = 2;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (KT())
            {
                if (TTLuu == 1)// thêm mới
                {
                    SoBienLai moi = new SoBienLai();
                    moi.Quyen = txtQuyen.Text;
                    moi.SoDen = Convert.ToInt32(txtDenSo.Text);
                    moi.SoHT = Convert.ToInt32(txtSoHT.Text);
                    moi.SoTu = Convert.ToInt32(txtTuSo.Text);
                    moi.Status = txtStatus.SelectedIndex;
                    moi.PLoai = cboPLoai.SelectedIndex;
                    if (lupMaCB.EditValue != null)
                        moi.MaCB = lupMaCB.EditValue.ToString();
                    _data.SoBienLais.Add(moi);
                    _data.SaveChanges();

                }
                else
                {
                    string quyen = txtQuyen.Text;
                    int ploai = cboPLoai.SelectedIndex;
                    List<SoBienLai> sobl = _data.SoBienLais.Where(p => p.Quyen == quyen && p.PLoai == ploai).ToList();
                    foreach (var item in sobl)
                    {
                        item.SoDen = Convert.ToInt32(txtDenSo.Text);
                        item.SoHT = Convert.ToInt32(txtSoHT.Text);
                        item.SoTu = Convert.ToInt32(txtTuSo.Text);
                        item.Status = txtStatus.SelectedIndex;
                        if (lupMaCB.EditValue != null)
                            item.MaCB = lupMaCB.EditValue.ToString();
                        _data.SaveChanges();
                    }

                }
                EnableControl(true);
                TTLuu = 0;
                frm_DmSoBL_Load(sender, e);
            }
           
        }
        bool KT()
        {
            if (cboPLoai.SelectedIndex < 0)
            {
                MessageBox.Show("Chưa chọn phân loại");
                cboPLoai.Focus();
                return false;

            }
             QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int pl = cboPLoai.SelectedIndex;
            int tt = txtStatus.SelectedIndex;
            if (tt == 1 && TTLuu == 1)
            {
                var kt = data.SoBienLais.Where(p => p.PLoai == pl && p.Status == tt).ToList();
                if (kt.Count > 0)
                {
                    MessageBox.Show("Tại 1 thời điểm chỉ sử dụng 1 quyển biên lai");
                    txtStatus.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtQuyen.Text))
            {
                MessageBox.Show("Bạn chưa nhập quyển biên lai");
                txtQuyen.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtTuSo.Text))
            {
                MessageBox.Show("Bạn chưa nhập số biên lai bắt đầu ");
                txtTuSo.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtDenSo.Text))
            {
                MessageBox.Show("Bạn chưa nhập số biên lai kết thúc ");
                txtDenSo.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtSoHT.Text))
            {
                MessageBox.Show("Bạn chưa nhập số biên lai hiện tại");
                txtSoHT.Focus();
                return false;

            }
            if (txtStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa chọn trạng thái");
                txtStatus.Focus();
                return false;

            }
            int soHT = Convert.ToInt32(txtSoHT.Text);
            int soDen = Convert.ToInt32(txtDenSo.Text);
            int SoTu = Convert.ToInt32(txtTuSo.Text);
            if (soHT > soDen || soHT < SoTu)
            {
                MessageBox.Show("Số hiện tại ngoài giới hạn số biên lai");
                return false;
            }
            return true;

        }
        private void txtSoHT_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string quyenbl = txtQuyen.Text;
            int pl=cboPLoai.SelectedIndex;
            bool ktraxoa = true;
            if (txtStatus.SelectedIndex == 2)
            {
                MessageBox.Show("quyển hóa đơn này đã ngừng sử dụng, không thể xóa !");
                ktraxoa = false;
            }
            var ktratu = data.TamUngs.Where(p => pl == 1 ? (p.PhanLoai == 3 || p.PhanLoai == 1) : p.PhanLoai == pl).Where(p => p.QuyenHD == quyenbl).ToList();
            if (ktratu.Count > 0)
            {
                MessageBox.Show("Quyển hóa đơn này đã được sử dụng, bạn không thể xóa");
                ktraxoa = false;
            }
            if(ktraxoa)
            {
                try
                {
                    SoBienLai sobl = data.SoBienLais.Where(p => p.PLoai == pl && p.Quyen == quyenbl).FirstOrDefault();
                    data.SoBienLais.Remove(sobl);
                    data.SaveChanges();
                    MessageBox.Show("Xóa thành công");
                    frm_DmSoBL_Load(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.ToString());
                }
            }
        }

        private void txtQuyen_Leave(object sender, EventArgs e)
        {
            if (TTLuu == 1)
            {
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                string quyenbl = txtQuyen.Text;
                int pl = cboPLoai.SelectedIndex;
                SoBienLai sobl = data.SoBienLais.Where(p => p.PLoai == pl && p.Quyen == quyenbl).FirstOrDefault();
                if (sobl != null)
                {
                    MessageBox.Show("Quyển đã tồn tại, không thể thêm mới");
                    txtQuyen.Focus();
                }
            }
        }
    }
}