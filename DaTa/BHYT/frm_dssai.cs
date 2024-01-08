using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.BHYT
{
    public partial class frm_dssai : DevExpress.XtraEditors.XtraForm
    {
        public frm_dssai()
        {
            InitializeComponent();
        }

        private void frm_dssai_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = DungChung.BenhNhan_AllInfo._listBenhNhanSai;
        }
    }
}