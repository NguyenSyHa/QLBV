using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_TimKiem : DevExpress.XtraEditors.XtraForm
    {
        public frm_TimKiem()
        {
            InitializeComponent();
        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    dem = 0;
        //    txtTenBenhKQ.Text = dem.ToString();
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        public delegate void _getstring(string chuoi1);
        public _getstring GetData;
        private void grvNoiDung_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                String TenBenh = grvNoiDung.GetFocusedRowCellValue("TenBenh").ToString();
                String MaICD = grvNoiDung.GetFocusedRowCellValue("MaBenh").ToString();
                txtMaICDKQ.Text = MaICD;
                txtTenBenhKQ.Text = TenBenh;
            }
            catch (Exception)
            {
            }
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
         
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GetData(txtMaICDKQ.Text);
            this.Dispose();
        }
        class c_ICD {
            string maICD;

            public string MaBenh
            {
                get { return maICD; }
                set { maICD = value; }
            }
            string tenICD;

            public string TenBenh
            {
                get { return tenICD; }
                set { tenICD = value; }
            }
        }
        List<c_ICD> _lICD = new List<c_ICD>();
        private void txtTimKiem_KeyUp(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            String strSearch = txtTimKiem.Text.ToLower();
            grcNoiDung.DataSource = null;
            grcNoiDung.DataSource = _lICD.Where(p => p.MaBenh.ToLower().Contains(strSearch) || p.TenBenh.ToLower().Contains(strSearch)).ToList();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frm_TimKiem_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _datacontext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lICD = (from bn in _datacontext.ICD10
                   
                     select new c_ICD
                     {
                         MaBenh = bn.MaICD,
                         TenBenh = bn.TenICD
                     }).ToList();
            grcNoiDung.DataSource = _lICD;
        }

        private void grvNoiDung_DoubleClick(object sender, EventArgs e)
        {
            if (grvNoiDung.RowCount > 0 && grvNoiDung.GetFocusedRowCellValue("MaBenh") != null)
            {
                btnOK_Click(sender, e);
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            //String strSearch = txtTimKiem.Text.ToLower();
            //grcNoiDung.DataSource = null;
            //grcNoiDung.DataSource = _lICD.Where(p => p.MaBenh.ToLower().Contains(strSearch) || p.TenBenh.ToLower().Contains(strSearch)).ToList();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            GetData(null);
            this.Dispose();
        }

    }
}