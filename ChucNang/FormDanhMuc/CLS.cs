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
    public partial class CLS : DevExpress.XtraEditors.XtraUserControl
    {
        public CLS()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnOK_Click(object sender, EventArgs e)
        {
            var q = _data.BenhNhans.ToList();
            grcCLS.DataSource = q.ToList();
        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
