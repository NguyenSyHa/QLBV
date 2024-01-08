using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.CLS
{
    public partial class frm_CDHA_XemAnh : DevExpress.XtraEditors.XtraForm
    {
        public frm_CDHA_XemAnh()
        {
            InitializeComponent();
        }
        Image imgr;
        public frm_CDHA_XemAnh(Image img)
        {
            InitializeComponent();
            this.imgr = img;

        }
        private void Ảnh_Load(object sender, EventArgs e)
        {
            ptAnh.Image = imgr;
        }
    }
}