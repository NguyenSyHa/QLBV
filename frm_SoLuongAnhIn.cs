using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV
{
    public partial class frm_SoLuongAnhIn : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoLuongAnhIn()
        {
            InitializeComponent();
        }

        private void frm_SoLuongAnhIn_Load(object sender, EventArgs e)
        {
            cboSoLuong.SelectedIndex = 0;
        }
        public int SoLuongAnhin;
        
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            SoLuongAnhin = Convert.ToInt32(cboSoLuong.Text);         
            this.Close();
        }
    }
}