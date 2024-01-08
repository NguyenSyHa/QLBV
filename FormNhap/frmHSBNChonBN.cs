using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.FormNhap
{
    public partial class frmHSBNChonBN : Form
    {
        List<BenhNhan> bn;
        public delegate void DelegateChonBN(BenhNhan bn);
        public DelegateChonBN dlg;
        public frmHSBNChonBN()
        {
            InitializeComponent();
        }

        public frmHSBNChonBN(List<BenhNhan> bn, DelegateChonBN dlg)
        {
            InitializeComponent();
            this.bn = bn;
            this.dlg = dlg;
        }

        private void frmHSBNChonBN_Load(object sender, EventArgs e)
        {
            gridControlChonBN.BeginUpdate();
            gridControlChonBN.DataSource = this.bn;
            gridControlChonBN.EndUpdate();
        }

        private void gridViewChonBN_DoubleClick(object sender, EventArgs e)
        {
            var row = (BenhNhan)gridViewChonBN.GetFocusedRow();
            if (row != null)
            {
                if (dlg != null)
                {
                    dlg(row);
                    this.Close();
                }
            }
        }

        private void gridViewChonBN_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            var row = (BenhNhan)gridViewChonBN.GetRow(gridViewChonBN.GetRowHandle(e.ListSourceRowIndex));
            if (row != null && e.IsGetData && e.Column.UnboundType != DevExpress.Data.UnboundColumnType.Bound)
            {
                if (e.Column.FieldName == "NgayThangNamSinh")
                {
                    e.Value = string.IsNullOrEmpty(row.NgaySinh.Trim()) ? row.NamSinh : (row.NgaySinh + "/" + row.ThangSinh + "/" + row.NamSinh);
                }
            }
        }

    }
}
