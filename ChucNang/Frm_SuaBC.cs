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
    public partial class Frm_SuaBC : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SuaBC()
        {
            InitializeComponent();
        }
        bool a = true;
        bool b = true;
        bool c = true;
        bool d = true;
        bool g = true;
        public Frm_SuaBC(bool _a, bool _b, bool _c, bool _d, bool _e)
        {
            InitializeComponent();
            a = _a;
            b = _b;
            c = _c;
            d = _d;
            g = _e;
        }


        private void Frm_SuaBC_Load(object sender, EventArgs e)
        {
            chkA.Checked = a;
            chkB.Checked = b;
            chkC.Checked = c;
            chkD.Checked = d;
            chkE.Checked = g;
        }
        public delegate void _getdata(string _a);
        public _getdata par;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string _Ten = "";
            if (chkA.Checked == true)
            { _Ten += "Khoa dược"; }
            if (chkB.Checked == true)
            {
                if (string.IsNullOrEmpty(_Ten))
                {
                    _Ten += "Viện phí";
                }
                else
                {
                    _Ten += " - "+"Viện phí";
                }
            }
            if (chkC.Checked == true)
            {
                if (string.IsNullOrEmpty(_Ten))
                {
                    _Ten += "BHYT";
                }
                else
                {
                    _Ten += " - " + "BHYT";
                }
            }
            if (chkD.Checked == true)
            {
                if (string.IsNullOrEmpty(_Ten))
                {
                    _Ten += "Tổng hợp";
                }
                else
                {
                    _Ten += " - " + "Tổng hợp";
                }
            }
            if (chkE.Checked == true)
            {
                if (string.IsNullOrEmpty(_Ten))
                {
                    _Ten += "CLS";
                }
                else
                {
                    _Ten += " - " + "CLS";
                }
            }
            //a = chkA.Checked;
            //b = chkB.Checked;
            //c = chkC.Checked;
            //d = chkD.Checked;
            //g = chkE.Checked;

            par(_Ten);
            this.Close();
        }
    }
}