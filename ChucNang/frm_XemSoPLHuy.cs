using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
namespace QLBV.FormNhap
{

    public partial class frm_XemSoPLHuy : DevExpress.XtraEditors.XtraForm
    {
        public frm_XemSoPLHuy()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        #region Tìm kiếm
        void TimKiem()
        {
            var dspl = (from pl in _dataContext.SoPLs.Where(p=>p.Status==-1)
                        join cb in _dataContext.CanBoes on pl.MaCBHuy equals cb.MaCB
                        join kp in _dataContext.KPhongs on cb.MaKP equals kp.MaKP
                        select new
                        {
                            pl.SoPL1,
                            pl.NgayNhap,
                            cb.TenCB,
                            kp.TenKP,
                            pl.TGHuy
                        }).ToList();
            grcPLhuy.DataSource = dspl.OrderBy(p=>p.TGHuy).ToList();
        }
        #endregion
       
        private void frm_XemSoPLHuy_Load(object sender, EventArgs e)
        {
            
            TimKiem();
        }
        
    }
}