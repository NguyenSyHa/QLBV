using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.FormThamSo
{
    public partial class frmCatKyTuTrang : DevExpress.XtraEditors.XtraForm
    {
        public frmCatKyTuTrang()
        {
            InitializeComponent();
        }

        private void frmCatKyTuTrang_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var cat = _data.People.Single();
            cat.TenBNhan = cat.TenBNhan.Trim();
        }
    }
}