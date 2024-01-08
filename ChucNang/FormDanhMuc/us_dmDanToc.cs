using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.IO;

namespace QLBV.FormDanhMuc
{
    public partial class us_dmDanToc : DevExpress.XtraEditors.XtraUserControl
    {
        public us_dmDanToc()
        {
            InitializeComponent();
        }
        int TTLuu = 0;
        //int TTXoa = 0;
        string Madt = "";
        private void enableControl(bool T)
        {
            txtMaDT.Enabled = T;
            txtTenDT.Enabled = T;
            cboStatus.Enabled = T;
            btnLuuKb.Enabled = T;
            btnMoiKb.Enabled = !T;
            btnSuaKb.Enabled = !T;
            btnXoaKb.Enabled = !T;
            grcDanToc.Enabled = !T;
        }
        private void resetControl()
        {
            txtMaDT.Text = "";
            txtTenDT.Text = "";
            cboStatus.Text = "";
        }
        #region
        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMaDT.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã dân tộc");
                txtMaDT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenDT.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên dân tộc");
                txtTenDT.Focus();
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
        List<DanToc> _lDtoc = new List<DanToc>();
        private void us_dmDanToc_Load(object sender, EventArgs e)
        {
            _lDtoc = dataContext.DanTocs.OrderBy(p => p.TenDT).ToList();
            grcDanToc.DataSource = _lDtoc;
            enableControl(false);

        }

        private void btnLuuKb_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        Madt = txtMaDT.Text.Trim();
                        var ma = dataContext.DanTocs.Where(p => p.MaDT== (Madt)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã dân tộc đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            DanToc bp = new DanToc();
                            bp.MaDT = txtMaDT.Text;
                            bp.TenDT = txtTenDT.Text;
                            if (cboStatus.SelectedIndex == 0)
                                bp.Status = 1;
                            else
                            {
                                if (cboStatus.SelectedIndex == 1)
                                {
                                    bp.Status = 0;
                                }
                            }
                            dataContext.DanTocs.Add(bp);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                            //_lDtoc = dataContext.DanTocs.OrderBy(p => p.TenDT).ToList();
                            //grcDanToc.DataSource = _lDtoc;
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMaDT.Text))
                        {
                            string madt = txtMaDT.Text;
                            DanToc bpsua = dataContext.DanTocs.Single(p => p.MaDT== (madt));
                            bpsua.TenDT = txtTenDT.Text;
                            if (cboStatus.SelectedIndex == 0)
                                bpsua.Status = 1;
                            else
                            {
                                if (cboStatus.SelectedIndex == 1)
                                {
                                    bpsua.Status = 0;
                                }
                            }
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                            //_lDtoc = dataContext.DanTocs.OrderBy(p => p.TenDT).ToList();
                            //grcDanToc.DataSource = _lDtoc;
                        }
                        break;
                }
                _lDtoc = dataContext.DanTocs.OrderBy(p => p.TenDT).ToList();
                grcDanToc.DataSource = _lDtoc.ToList();
            }
        }

        private void grvDanToc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDanToc.GetFocusedRowCellValue(colMaDT) != null && grvDanToc.GetFocusedRowCellValue(colMaDT).ToString() != "")
            {
                txtMaDT.Text = grvDanToc.GetFocusedRowCellValue(colMaDT).ToString();
                if (grvDanToc.GetFocusedRowCellValue(colTenDT) != null && grvDanToc.GetFocusedRowCellValue(colTenDT).ToString() != "")
                {
                    txtTenDT.Text = grvDanToc.GetFocusedRowCellValue(colTenDT).ToString();
                }
                else
                {
                    txtTenDT.Text = "";
                }

                if (grvDanToc.GetFocusedRowCellValue(colStatus) != null && grvDanToc.GetFocusedRowCellValue(colStatus).ToString() != "")
                {
                    int a = int.Parse(grvDanToc.GetFocusedRowCellValue(colStatus).ToString());
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
                txtMaDT.Text = "";
            }
        }

        private void btnMoiKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            cboStatus.SelectedIndex = 0;
            txtMaDT.Focus();
        }

        private void btnSuaKb_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMaDT.Enabled = false;
            TTLuu = 2;
            txtTenDT.Focus();
        }

        private void btnXoaKb_Click(object sender, EventArgs e)
        {

            Madt = txtMaDT.Text;
            var ma = dataContext.TTboXungs.Where(p => p.MaDT== (Madt)).ToList();
            if (ma.Count > 0)
            {
                MessageBox.Show("Mã dân tộc đã được sử dụng, Bạn không thể xóa.");
            }
            else
            {
                DataRow dr = grvDanToc.GetFocusedDataRow();
                DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa dân tộc đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dia == DialogResult.Yes)
                {
                    //grcDanToc.ControlRemoved();
                    var xoa = dataContext.DanTocs.Single(p=>p.MaDT== (Madt));
                    dataContext.DanTocs.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoaKb.Enabled = true;

                }
                _lDtoc = dataContext.DanTocs.OrderBy(p => p.TenDT).ToList();
                grcDanToc.DataSource = _lDtoc.ToList();
            }


        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                tk = txtTimKiem.Text.ToLower();
            }
            grcDanToc.DataSource = _lDtoc.Where(p => p.TenDT.ToLower().Contains(tk));
           
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

    }
}