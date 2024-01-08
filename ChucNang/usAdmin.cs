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
    public partial class usAdmin : DevExpress.XtraEditors.XtraUserControl
    {
        public usAdmin()
        {
            InitializeComponent();
        }
        string tenDN = "";
        private void grcAdmin_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kp = dataContext.KPhongs.ToList();
            lupMaKP.DataSource = kp;
            var admin=(from ad in dataContext.ADMINs join cb in dataContext.CanBoes on ad.MaCB equals cb.MaCB
                      select new {ad.ID,ad.MaCB,ad.MatK,ad.TenDN,ad.CapDo, cb.TenCB,cb.MaKP}).ToList();
            grcAdmin.DataSource = admin;
        }

        private void btnMoiKb_Click(object sender, EventArgs e)
        {
            FormDanhMuc.frmAdmin frm = new frmAdmin("",1);
            frm.ShowDialog();
        }

        private void grvAdmin_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvAdmin.GetFocusedRowCellValue(colTenDN) != null && grvAdmin.GetFocusedRowCellValue(colTenDN).ToString() != "")
            {
                tenDN = grvAdmin.GetFocusedRowCellValue(colTenDN).ToString();

            }
            else
                tenDN = "";
        }

        private void btnSuaKb_Click(object sender, EventArgs e)
        {
            FormDanhMuc.frmAdmin frm = new frmAdmin(tenDN, 2);
            frm.ShowDialog();
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnXoaKb_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (grvAdmin.GetFocusedRowCellValue(colTenDN) != null)
            {
                string tendn=grvAdmin.GetFocusedRowCellValue(colTenDN).ToString();
                DialogResult _result = MessageBox.Show("Bạn muốn xóa tên đăng nhập: "+grvAdmin.GetFocusedRowCellValue(colTenDN).ToString(),"xóa tài khoản!",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (_result == DialogResult.Yes) {
                    var xoa = dataContext.ADMINs.Single(p => p.TenDN== (tendn));
                    dataContext.ADMINs.Remove(xoa);
                    dataContext.SaveChanges();
                }
            }
        }

        private void btnLuuKb_Click(object sender, EventArgs e)
        {

        }
    }
}
