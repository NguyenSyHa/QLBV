using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class frm_CheckDuyet : DevExpress.XtraEditors.XtraForm
    {
        public frm_CheckDuyet()
        {
            InitializeComponent();
        }
        bool tudongduyet = false;
        public frm_CheckDuyet(bool td)
        {
            InitializeComponent();
            tudongduyet = td;
        }
        string _lydo = "";
        public frm_CheckDuyet(bool td,string lydo)
        {
            InitializeComponent();
            tudongduyet = td;
            _lydo = lydo;
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            getdata("", false, false);
            this.Close();
        }
        public delegate void getString(string a, bool b, bool c);
        public getString getdata;
        private void btnInBC_Click(object sender, EventArgs e)
        {
            getdata(memoNoiDung.Text, true, ckcKoTamUng.Checked);
            this.Dispose();
        }

        private void frm_CheckDuyet_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_lydo))
                memoNoiDung.Text = _lydo;
            if (DungChung.Bien.MaBV == "30010")
                ckcKoTamUng.Visible = true;
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (tudongduyet)
            {
                
                btnInBC_Click(sender, e);
            }
        }
    }
}