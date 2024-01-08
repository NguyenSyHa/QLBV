using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang.FormDanhMuc
{
    public partial class Frm_DMCauHinhIn : DevExpress.XtraEditors.XtraForm
    {
        public Frm_DMCauHinhIn()
        {
            InitializeComponent();
        }
        int TTLuu = 0;
        
        string _main = "";
        private void enableControl(bool t)
        {
            txtMain.Properties.ReadOnly = !t;
            txtTenmau.Properties.ReadOnly = !t;
            txtFile.Properties.ReadOnly = !t;
            cboStatus.Properties.ReadOnly = !t;
            btnLuu.Enabled = t;
            btnMoi.Enabled = !t;
            btnSua.Enabled = !t;
            btnXoa.Enabled = !t;
            btnHuy.Enabled = t;
            grcCauHinhIn.Enabled = !t;
        }

        private void resetControl()
        {
            txtMain.Text = "";
            txtTenmau.Text = "";
            txtTimkiem.Text = "";
            txtFile.Text = "";
            cboStatus.SelectedIndex = 0;

        }

        private bool KTLuu()
        {
            if (string.IsNullOrEmpty(txtMain.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã in");
                txtMain.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenmau.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên mẫu in");
                txtTenmau.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtFile.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên file");
                txtFile.Focus();
                return false;
            }

            return true;
        }

        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<CauHinhIn> _cauhinhin = new List<CauHinhIn>();

        private void Frm_DMCauHinhIn_Load(object sender, EventArgs e)
        {
            _cauhinhin = dataContext.CauHinhIns.OrderBy(p => p.ID).ToList();
            grcCauHinhIn.DataSource = _cauhinhin;
            enableControl(false);
        }

       

        private void btnMoi_Click(object sender, EventArgs e)
        {
            enableControl(true);
            resetControl();
            TTLuu = 1;
            txtMain.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enableControl(true);
            txtMain.Enabled = false;
            TTLuu = 2;
            txtTenmau.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            _main = txtMain.Text;
            DataRow dr = grvCauHinhIn.GetFocusedDataRow();
            DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn xóa mẫu đã chọn?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dia == DialogResult.Yes)
            {
                    
                    var xoa = dataContext.CauHinhIns.Single(p => p.MaIn== (_main));
                    dataContext.CauHinhIns.Remove(xoa);
                    dataContext.SaveChanges();
                    btnXoa.Enabled = true;

            }
            _cauhinhin = dataContext.CauHinhIns.OrderBy(p => p.ID).ToList();
            grcCauHinhIn.DataSource = _cauhinhin.ToList();
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KTLuu())
            {
                switch (TTLuu)
                {
                    case 1:
                        _main = txtMain.Text.Trim();
                        var ma = dataContext.CauHinhIns.Where(p => p.MaIn== (_main)).ToList();
                        if (ma.Count > 0)
                        {
                            MessageBox.Show("Mã cấu hình đã có, vui lòng nhập mã khác");
                        }
                        else
                        {
                            CauHinhIn cauhinhin = new CauHinhIn();
                            cauhinhin.MaIn = txtMain.Text;
                            cauhinhin.TenMauIn = txtTenmau.Text;
                            cauhinhin.FileName = txtFile.Text;
                            if (cboStatus.SelectedIndex == 0)
                                cauhinhin.Status = true;
                            else
                            {
                                if (cboStatus.SelectedIndex == 1)
                                {
                                    cauhinhin.Status = false;
                                }
                            }
                            dataContext.CauHinhIns.Add(cauhinhin);
                            dataContext.SaveChanges();
                            enableControl(false);
                            MessageBox.Show("Lưu thành công!");
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(txtMain.Text))
                        {
                            string main = txtMain.Text;
                            CauHinhIn mausua = dataContext.CauHinhIns.Single(p => p.MaIn== (main));
                            mausua.TenMauIn = txtTenmau.Text;
                            mausua.FileName = txtFile.Text;
                            if (cboStatus.SelectedIndex == 0)
                                mausua.Status = true;
                            else
                            {
                                if (cboStatus.SelectedIndex == 1)
                                {
                                    mausua.Status = false;
                                }
                            }
                            dataContext.SaveChanges();
                            MessageBox.Show("Lưu thành công!");
                            enableControl(false);
                        }
                        break;
                }
                _cauhinhin = dataContext.CauHinhIns.OrderBy(p => p.ID).ToList();
                grcCauHinhIn.DataSource = _cauhinhin.ToList();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            enableControl(false);
        }

        private void txtTimkiem_EditValueChanged(object sender, EventArgs e)
        {
            string tk = "";
            if (!string.IsNullOrEmpty(txtTimkiem.Text))
            {
                tk = txtTimkiem.Text;
            }
            grcCauHinhIn.DataSource = _cauhinhin.Where(p => p.FileName.Contains(tk));
        }

        private void grvCauHinhIn_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvCauHinhIn.GetFocusedRowCellValue(colID) != null && grvCauHinhIn.GetFocusedRowCellValue(colID).ToString() != "")
            {
                txtMain.Text = grvCauHinhIn.GetFocusedRowCellValue(colMa).ToString();
                if (grvCauHinhIn.GetFocusedRowCellValue(colTen) != null && grvCauHinhIn.GetFocusedRowCellValue(colTen).ToString() != "")
                {
                    txtTenmau.Text = grvCauHinhIn.GetFocusedRowCellValue(colTen).ToString();
                }
                else
                {
                    txtTenmau.Text = "";
                }

                if (grvCauHinhIn.GetFocusedRowCellValue(colFile) != null && grvCauHinhIn.GetFocusedRowCellValue(colFile).ToString() != "")
                {
                    txtFile.Text = grvCauHinhIn.GetFocusedRowCellValue(colFile).ToString();
                }
                else
                {
                    txtFile.Text = "";
                }

                if (grvCauHinhIn.GetFocusedRowCellValue(colStatus) != null)
                {
                    cboStatus.SelectedIndex = 0;
                }
                else
                {
                    cboStatus.SelectedIndex = 1;
                }
            }
            else
            {
                txtMain.Text = "";
            }
        }

        

        

        
    }
}