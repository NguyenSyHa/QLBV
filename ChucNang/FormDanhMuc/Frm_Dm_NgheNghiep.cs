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
    public partial class Frm_Dm_NgheNghiep : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Dm_NgheNghiep()
        {
            InitializeComponent();
        }
        int TTLuu = 0;
        string Mann = "";
        private void enableControl(bool T)
        {
            txtMaNN.Properties.ReadOnly = !T;
            txtTenNN.Properties.ReadOnly = !T;
            txtTenCT.Properties.ReadOnly = !T;
            btnLuu.Enabled = T;
            btnMoi.Enabled = !T;
            btnSua.Enabled = !T;
            grcDmNN.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaNN.Text = "";
            txtTenNN.Text = "";
            txtTenCT.Text = "";
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaNN.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nghề nghiệp");
                txtMaNN.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenNN.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nghề nghiệp");
                txtTenNN.Focus();
                return false;
            }

            return true;
        }

        #endregion
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<DmNN> _lnn = new List<DmNN>();

        private void grcDmNN_Click(object sender, EventArgs e)
        {

        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            txtMaNN.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaNN.Enabled = false;
            TTLuu = 2;
            txtTenNN.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //_id = int.Parse(txtID.Text);
            string Mann = txtMaNN.Text;
                DataRow dr = grvDmNN.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa nghề nghiệp đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    //grcDanToc.ControlRemoved();
                    var xoa = dataContext.DmNNs.OrderBy(p => p.TenNN).Single(p => p.MaNN == Mann);
                    dataContext.DmNNs.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoa.Enabled = true;

                }
                _lnn = dataContext.DmNNs.OrderBy(p => p.TenNN).ToList();
                grcDmNN.DataSource = _lnn.ToList();
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        Mann = txtMaNN.Text.Trim();
                        var ma = dataContext.DmNNs.Where(p => p.MaNN== (Mann)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã nghề nghiệp đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            DmNN nn = new DmNN();
                            nn.MaNN = txtMaNN.Text;
                            nn.TenNN = txtTenNN.Text;
                            nn.TenCT = txtTenCT.Text;

                            dataContext.DmNNs.Add(nn);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMaNN.Text))
                        {
                            string mann = txtMaNN.Text;
                            DmNN nnsua = dataContext.DmNNs.Single(p => p.MaNN== (mann));
                            nnsua.MaNN = txtMaNN.Text;
                            nnsua.TenNN = txtTenNN.Text;
                            nnsua.TenCT = txtTenCT.Text;
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }
                _lnn = dataContext.DmNNs.OrderBy(p => p.TenNN).ToList();
                grcDmNN.DataSource = _lnn.ToList();
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
             string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text.ToLower();
            }
            grcDmNN.DataSource = _lnn.Where(p => p.TenNN.ToLower().Contains(tk));
        
        }

        private void Frm_Dm_NgheNghiep_Load(object sender, EventArgs e)
        {

            _lnn = dataContext.DmNNs.OrderBy(p => p.TenNN).ToList();
            grcDmNN.DataSource = _lnn;
            enableControl(false);
        }

        private void grvDmNN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDmNN.GetFocusedRowCellValue(colMaNN) != null && grvDmNN.GetFocusedRowCellValue(colMaNN).ToString() != "")
            {
                txtMaNN.Text = grvDmNN.GetFocusedRowCellValue(colMaNN).ToString();
                if (grvDmNN.GetFocusedRowCellValue(colTenNN) != null && grvDmNN.GetFocusedRowCellValue(colTenNN).ToString() != "")
                {
                    txtTenNN.Text = grvDmNN.GetFocusedRowCellValue(colTenNN).ToString();
                }
                else
                {
                    txtTenNN.Text = "";
                }
                if (grvDmNN.GetFocusedRowCellValue(colTenCT) != null && grvDmNN.GetFocusedRowCellValue(colTenCT).ToString() != "")
                {
                    txtTenCT.Text = grvDmNN.GetFocusedRowCellValue(colTenCT).ToString();
                }
                else
                {
                    txtTenCT.Text = "";
                }

            }
            else
            {
                txtMaNN.Text = "";
            }
        }
    }
}