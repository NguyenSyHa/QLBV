using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLBV_Database.Common;
using QLBV.FormNhap;
using DevExpress.ClipboardSource.SpreadsheetML;

namespace QLBV.FormThamSo
{
    public partial class frm_MessgeBox : DevExpress.XtraEditors.XtraForm
    {
        private QLBV_Database.QLBVEntities _dataContext = EntityDbContext.DbContext;
        frmHSBNNhapMoi hSBNNhapMoi = new frmHSBNNhapMoi();
        public frm_MessgeBox(string Messshow,int status)
        {
            InitializeComponent();
            _status = status;
            _Messshow = Messshow;
        }

        string _Messshow = "";
        int _status = 0;

        private void frm_MessgeBox_Load(object sender, EventArgs e)
        {
            if (_status == 0)
            {
                ptInfomation.Visible = true;
                ptWarning.Visible = false;
                this.mmMesshow.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            }
            else
            {
                ptInfomation.Visible = false;
                ptWarning.Visible = true;
                this.mmMesshow.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            mmMesshow.Text = _Messshow;
        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}