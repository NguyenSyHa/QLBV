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
    public partial class frm_TimKiemICD9 : DevExpress.XtraEditors.XtraForm
    {
        public frm_TimKiemICD9()
        {
            InitializeComponent();
        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    dem = 0;
        //    txtTenBenhKQ.Text = dem.ToString();
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        public delegate void _getstring(int ID);
        public _getstring GetData;
        private void grvNoiDung_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                String TenPTTT = grvNoiDung.GetFocusedRowCellValue("TenPTTT").ToString();
                String MaICD = grvNoiDung.GetFocusedRowCellValue("MaICD").ToString();
                int ID = Convert.ToInt32(grvNoiDung.GetFocusedRowCellValue("ID").ToString());
                txtMaICDKQ.Text = MaICD;
                txtTenPTTT.Text = TenPTTT;
                txtID.Text = ID.ToString();
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
            if (!String.IsNullOrEmpty(txtID.Text))
            {
                GetData(Convert.ToInt32(txtID.Text));
                this.Dispose();
            }
        }
        class c_ICD {
            string maICD;

            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }
            string tenICD;

            public string TenPTTT
            {
                get { return tenICD; }
                set { tenICD = value; }
            }
            public int ID { set; get; }
        }
        List<c_ICD> _lICD = new List<c_ICD>();
        private void txtTimKiem_KeyUp(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            String strSearch = txtTimKiem.Text.ToLower();
            grcNoiDung.DataSource = null;
            grcNoiDung.DataSource = _lICD.Where(p => p.MaICD.ToLower().Contains(strSearch) || p.TenPTTT.ToLower().Contains(strSearch)).ToList();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frm_TimKiemICD9_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _datacontext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _lICD = (from bn in _datacontext.ICD9
                   
                     select new c_ICD
                     {
                         ID= bn.ID,
                         MaICD = bn.MaICD,
                         TenPTTT = bn.TenPTTT
                     }).ToList();
            grcNoiDung.DataSource = _lICD;
        }

        private void grvNoiDung_DoubleClick(object sender, EventArgs e)
        {
            if (grvNoiDung.RowCount > 0 && grvNoiDung.GetFocusedRowCellValue("MaICD") != null)
            {
                btnOK_Click(sender, e);
            }
        }

    }
}