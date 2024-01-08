using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frmChonBN : DevExpress.XtraEditors.XtraForm
    {
        public frmChonBN()
        {
            InitializeComponent();
        }

        private void frmChonBN_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}