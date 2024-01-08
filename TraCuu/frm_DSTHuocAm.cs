using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.TraCuu
{
    public partial class frm_DSTHuocAm : DevExpress.XtraEditors.XtraForm
    {
        public frm_DSTHuocAm()
        {
            InitializeComponent();
        }
        List<DungChung.Ham.ThuocTon> _lthuocton = new List<DungChung.Ham.ThuocTon>();
        public frm_DSTHuocAm(List<DungChung.Ham.ThuocTon>  _lthuoc)
        {
            InitializeComponent();
            _lthuocton = _lthuoc;
        }
        private void frm_DSTHuocAm_Load(object sender, EventArgs e)
        {
            grcThuocTon.DataSource = _lthuocton;
        }

        private void grvThuocTon_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colXem") {
                int madv = 0,_makho=0;
                double dongia = -1;

                if (grvThuocTon.GetFocusedRowCellValue(colMaDV) != null)
                    madv = Convert.ToInt32( grvThuocTon.GetFocusedRowCellValue(colMaDV));
                dongia = _lthuocton.Where(p => p.MaDV == madv).First().DonGia;
                _makho = _lthuocton.Where(p => p.MaDV == madv).First().MaKho == null ? 0 :  Convert.ToInt32(_lthuocton.Where(p => p.MaDV == madv).First().MaKho);
                FormTraCuu.FrmTC_NhapXuatTon frm = new FormTraCuu.FrmTC_NhapXuatTon(_makho,madv,dongia);
                frm.ShowDialog();
            }
        }

    }
}