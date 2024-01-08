using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV.FormDanhMuc;

namespace QLBV.FormNhap
{
    public partial class frm_ChiSoChucNangSong : DevExpress.XtraEditors.XtraForm
    {
        public frm_ChiSoChucNangSong()
        {
            InitializeComponent();
        }
        public frm_ChiSoChucNangSong(int _mabn, int makp)
        {
            InitializeComponent();
            mabn = _mabn;
            this.makp = makp;
        }
        int mabn = 0; int makp = 0;
        private void frm_ChiSoChucNangSong_Load(object sender, EventArgs e)
        {
            panelControl1.Controls.Clear();
            usChiSoCoThe us = new usChiSoCoThe(mabn, makp);           
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Controls.Add(us);
           
        }
    }
}