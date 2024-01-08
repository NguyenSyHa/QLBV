using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class frm_DoiMaBV : DevExpress.XtraEditors.XtraForm
    {
        public frm_DoiMaBV()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DungChung.Bien.MaBV = txtMaBV.Text;
            if (txtMaBV.Text != "12345" && DungChung.Bien.MaBV != "24297")
                DungChung.Bien.MaTinh = txtMaBV.Text.Substring(0, 2);
            this.Close();
        }
    }
}