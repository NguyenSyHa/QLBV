using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_DsBNtt : DevExpress.XtraEditors.XtraForm
    {
        public frm_DsBNtt()
        {
            InitializeComponent();
        }

        private void frm_DsBNtt_Load(object sender, EventArgs e)
        {
            dtNgaytu.DateTime = System.DateTime.Now;
            dtngayden.DateTime = System.DateTime.Now;
        }
    }
}