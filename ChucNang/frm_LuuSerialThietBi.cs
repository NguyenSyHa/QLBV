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
    public partial class frm_LuuSerialThietBi : DevExpress.XtraEditors.XtraForm
    {
        public frm_LuuSerialThietBi()
        {
            InitializeComponent();
        }

        private void btCapNhat_Click(object sender, EventArgs e)
        {
            string S_TBNV = txtSerial.Text;
            QLBV_Library.QLBV_Ham.Write_update("Cuong.S_TBNV", S_TBNV);
            this.Close();
        }

        private void frm_LuuSerialThietBi_Load(object sender, EventArgs e)
        {
            string chuoi = "";
            chuoi = QLBV_Library.QLBV_Ham.Read_Update("Cuong.S_TBNV", 1);
            txtSerial.Text = chuoi;
        }
    }
}