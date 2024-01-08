using DevExpress.Data;
using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.CLS
{
    public partial class frm_CLS_DSBN48h : Form
    {
        int _makp;
        public frm_CLS_DSBN48h(int makp)
        {
            InitializeComponent();
            this._makp = makp;
        }

        private void gridViewDSBN48h_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.FieldName == "STT" && e.Column.UnboundType != UnboundColumnType.Bound)
            {
                e.Value = e.ListSourceRowIndex + 1;
            }
        }

        private void frm_CLS_DSBN48h_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities DaTaContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            var chidinh = (from cls in DaTaContext.CLS.Where(o => o.Status != 1)
                           join bn in DaTaContext.BenhNhans.Where(o => o.MaKP == _makp) on cls.MaBNhan equals bn.MaBNhan
                           join cd in DaTaContext.ChiDinhs on cls.IdCLS equals cd.IdCLS
                           join dv in DaTaContext.DichVus on cd.MaDV equals dv.MaDV
                           select new
                           {
                               bn.MaBNhan,
                               bn.TenBNhan,
                               cls.NgayThang,
                               dv.MaDV,
                               dv.TenDV
                           }).ToList();
            var source = (from cdgr in chidinh
                          group cdgr by new { cdgr.MaBNhan, cdgr.TenBNhan } into kq
                          select new { kq.Key.MaBNhan, kq.Key.TenBNhan, DichVu = string.Join("; ", kq.Where(o => o.NgayThang.Value.AddDays(2) < DateTime.Now).Select(o => o.TenDV)) }).Where(o => !string.IsNullOrWhiteSpace(o.DichVu)).ToList();

            gridControlDSBN48h.DataSource = source;


        }
    }
}
