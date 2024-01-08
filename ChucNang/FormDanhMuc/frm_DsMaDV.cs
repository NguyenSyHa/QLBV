using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace QLBV.FormThamSo
{
    public partial class frm_DsMaDV : DevExpress.XtraEditors.XtraForm
    {
        public delegate void PassMaDV(int madv);
        public PassMaDV passMaDV;
        string makp = "";
        int makho = 0;
        int Ploai;
        int _IDCD = 0;
       
        public frm_DsMaDV(int makp, int makho, int Ploai)
        {
            InitializeComponent();
            this.makp = ";" + makp.ToString() + ";";
            this.makho = makho;
            this.Ploai = Ploai;
        }

        /// <summary>
        /// load dịch vụ XQuangCT BV 27023 _ có thuốc cản quan và không có thuốc cản quang
        /// </summary>
        public frm_DsMaDV(int IDCD)
        {
            this._IDCD = IDCD;
           
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities data;
        List<DichVu> _lDichVu = new List<DichVu>();
        private void frm_DsMaDV_Load(object sender, EventArgs e)
        {
            grc_DSDichVu.DataSource = null;
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            #region oad dịch vụ XQuangCT BV 27023 _ có thuốc cản quan và không có thuốc cản quang
            if (_IDCD > 0)
            {
                _lDichVu = (from tn in data.TieuNhomDVs.Where(p=>p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang) 
                            join dv in data.DichVus  on tn.IdTieuNhom equals dv.IdTieuNhom
                            join dvct in data.DichVucts.Where(p => p.Status == 1) on dv.MaDV equals dvct.MaDV
                            select dv
                             ).Distinct().ToList();              
                grc_DSDichVu.DataSource = _lDichVu;
            }
            #endregion
            #region default
            else
            {
                if (Ploai == 1)
                {
                    _lDichVu = (from d in data.DichVus
                                join dtct in data.NhapDcts on d.MaDV equals dtct.MaDV
                                join dt in data.NhapDs on dtct.IDNhap equals dt.IDNhap
                                where d.PLoai == 1 && dt.MaKP == makho && d.MaKPsd.Contains(makp) && dt.PLoai == 1
                                select d).Distinct().ToList();
                }
                else
                {
                    _lDichVu = (from d in data.DichVus
                                where d.PLoai == 2 && d.MaKPsd.Contains(makp)
                                select d).ToList();

                }
                grc_DSDichVu.DataSource = _lDichVu;
            }
            #endregion
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            grc_DSDichVu.DataSource = null;
            string timkiem = txtTimkiem.Text.Trim().ToLower();
            var dv = (from n in _lDichVu where (n.MaDV.ToString().Contains(timkiem) || n.TenDV.ToLower().Contains(timkiem) || (n.TenHC != null && n.TenHC.ToLower().Contains(timkiem))) select n).ToList();
            grc_DSDichVu.DataSource = dv;

        }

        private void grv_DSDichVu_DoubleClick(object sender, EventArgs e)
        {
            //pasDSIDFormDC(_listSelectedMenu);

            Point pt = grv_DSDichVu.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = grv_DSDichVu.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                int maDV = 0;
                int row = info.RowHandle;
                if (grv_DSDichVu.GetRowCellValue(row, colMaDV) != null)
                {
                    maDV = Convert.ToInt32(grv_DSDichVu.GetRowCellValue(row, colMaDV));
                    if (passMaDV != null)
                    {
                        this.Close();
                        passMaDV(maDV);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            grv_DSDichVu_DoubleClick(sender, e);
        }
    }
}