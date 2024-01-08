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
    public partial class usChiPhi : DevExpress.XtraEditors.XtraUserControl
    {
        public usChiPhi()
        {
            InitializeComponent();
        }
        private void EnableControl(bool t) {
            btnMoi.Enabled = t;
            btnSua.Enabled = t;
            btnXoa.Enabled = t;
            btnLuu.Enabled = !t;
        }
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void usChiPhi_Load(object sender, EventArgs e)
        {
            EnableControl(true);
            var que = (from CP in Data.DichVus select CP).OrderBy(p => p.TenDV);
            if (que.Count() > 0)
            {
                binSChiPhi.DataSource = que.ToList();
                grcChiPhi.DataSource = binSChiPhi;
            }
            grvChiPhi.OptionsBehavior.Editable = false;
            var q = from Nhom in Data.NhomDVs select new { Nhom.IDNhom, Nhom.TenNhom,Nhom.Status };
            binSNhomCP.DataSource = q.Where(p => p.Status==1).ToList();
            lupIDNhom.DataSource = binSNhomCP;
            
        }

        private void grvChiPhi_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvChiPhi.SetRowCellValue(e.RowHandle, colMaCP, "0");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            EnableControl(false);
            grvChiPhi.OptionsBehavior.Editable = true;
            

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //frmNhomCP frm = new frmNhomCP();
            //frm.ShowDialog();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvChiPhi.GetFocusedRowCellValue(colMaCP) != null && grvChiPhi.GetFocusedRowCellValue(colMaCP).ToString() != "")
            {
                int maxoa=Convert.ToInt32(grvChiPhi.GetFocusedRowCellValue(colMaCP));
                DialogResult result;
                if (grvChiPhi.GetFocusedRowCellValue(colTenCP) != null)
                {
                    result = MessageBox.Show("Bạn muốn xóa chi phí:" + grvChiPhi.GetFocusedRowCellValue(colTenCP).ToString(), "Xóa chi phi!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }
                else {
                    result = MessageBox.Show("Bạn muốn xóa chi phí này", "Xóa chi phi!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }
                if (result == DialogResult.OK) { 
                    // chưa kiểm tra xem chi phí đã được dùng thì không cho xóa
                var q=Data.DichVus.Single(p=>p.MaDV== (maxoa));
                Data.DichVus.Remove(q);
                Data.SaveChanges();
                usChiPhi_Load(sender, e);
                }
            }
            else {
                MessageBox.Show("Bạn chưa chọn mục để xóa");
            }

        }
        #region
        private bool kt(int i) {
            
            if (grvChiPhi.GetRowCellValue(i, colMaCP) == null)
            {
                MessageBox.Show("Bạn chưa nhập mã ");
                return false;
            }
            if (grvChiPhi.GetRowCellValue(i, colTenCP) == null)
            {
                MessageBox.Show("Bạn chưa nhập tên ");
                return false;
            }
            return true;
        }
        #endregion
        private void grvChiPhi_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            kt(e.RowHandle);
        }
    }
}
