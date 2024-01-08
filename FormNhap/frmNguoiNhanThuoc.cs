using DevExpress.XtraEditors;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frmNguoiNhanThuoc : XtraForm
    {
        int? idBnkb;
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        BNKB bnkb;
        public frmNguoiNhanThuoc(int? idBnkb)
        {
            InitializeComponent();
            this.idBnkb = idBnkb;
        }

        private void frmNguoiNhanThuoc_Load(object sender, EventArgs e)
        {
            if (this.idBnkb != null)
            {
                bnkb = dataContext.BNKBs.FirstOrDefault(o => o.IDKB == idBnkb);
                if (bnkb != null)
                {
                    this.txtNguoiNhanThuoc.Text = bnkb.NguoiNhanThuoc;
                    this.txtSoCMND.Text = bnkb.SoCMNDNguoiNhanThuoc;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (bnkb != null)
            {
                bnkb.NguoiNhanThuoc = this.txtNguoiNhanThuoc.Text;
                bnkb.SoCMNDNguoiNhanThuoc = this.txtSoCMND.Text;
                dataContext.SaveChanges();
            }
            this.Close();
        }

    }
}
