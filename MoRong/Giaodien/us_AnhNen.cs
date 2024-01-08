using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.Giaodien
{
    public partial class us_AnhNen : DevExpress.XtraEditors.XtraUserControl
    {
        public us_AnhNen()
        {
            InitializeComponent();
        }

        private void us_AnhNen_Load(object sender, EventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
