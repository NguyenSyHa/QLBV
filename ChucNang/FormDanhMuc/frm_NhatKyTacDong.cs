using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang.FormDanhMuc
{
    public partial class frm_NhatKyTacDong : DevExpress.XtraEditors.XtraForm
    {
        public frm_NhatKyTacDong()
        {
            InitializeComponent();
        }
        

        private void enableControl(bool t)
        {
            txtIDLog.Properties.ReadOnly = !t;
            txtLyDo.Properties.ReadOnly = !t;
            txtTenMay.Properties.ReadOnly = !t;
            txtDate.Properties.ReadOnly = !t;
            txtMaBN.Properties.ReadOnly = !t;
            txtUser.Properties.ReadOnly = !t;
            
            txtMaCB.Properties.ReadOnly = !t;
            
            
           
            grcLog.Enabled = !t;
        }

        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<LOG> _log = new List<LOG>();

        private void frm_NhatKyTacDong_Load(object sender, EventArgs e)
        {
            detungay.DateTime = DateTime.Now;
            dedenngay.DateTime = DateTime.Now;
            _log = dataContext.LOGs.OrderBy(p => p.IdLog).ToList();
            grcLog.DataSource = _log;
            enableControl(false);
        }  
      
        

        private void grvLog_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvLog.GetFocusedRowCellValue(colID) != null && grvLog.GetFocusedRowCellValue(colID).ToString() != "")
            {
                int malog = Convert.ToInt32(grvLog.GetFocusedRowCellValue(colID));
                txtIDLog.Text = malog.ToString();
                var a = _log.Where(p => p.IdLog == malog).ToList();
                if (_log.Where(p => p.IdLog == malog).ToList().Count > 0)
                {
                    txtLyDo.Text = a.First().LyDo;
                    txtDate.Text = a.First().DateLog.ToString();
                    txtUser.Text = a.First().UserName;
                    txtTenMay.Text = a.First().ComputerName;
                    txtMaCB.Text = a.First().MaCB;
                    txtMaBN.Text = a.First().MaBNhan.ToString();

                }
            }
        }

        

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
                       
        }

        private void detungay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiemTheoNgay();
            
        }

        private void dedenngay_EditValueChanged(object sender, EventArgs e)
        {
            TimKiemTheoNgay();
        }
        private void TimKiemTheoNgay()
        {
            
            grcLog.DataSource = _log.Where(p => p.DateLog >= detungay.DateTime && p.DateLog <= dedenngay.DateTime);
        }

    }
}