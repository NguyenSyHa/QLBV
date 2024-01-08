using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.FormThamSo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using QLBV.DungChung;
using System.Collections;
using QLBV.Admin;

namespace QLBV.FormNhap
{
    public partial class frm_CapQuyen : DevExpress.XtraEditors.XtraForm
    {
        public frm_CapQuyen()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data;
        //List<ADMIN> _lAdmin = new List<ADMIN>();        
        List<Permission> _lPms = new List<Permission>();

        private string _tenDN = "";

        private void frm_CapQuyen_Load(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            loadAdmin();

        }
        private void loadPemission()
        {
            unboundData = new ArrayList();
            var qTenF = us_menubc.SetDM().OrderBy(p => p.TenBC).ToList(); //DungChung.Ham._listBC.OrderBy(p => p.TenBC).ToList();
            lupTenBC.DataSource = qTenF;
            _lPms = (from pms in _data.Permissions
                     where pms.TenDN == (_tenDN)
                     select pms).ToList();
            grc_Pemission.DataSource = _lPms;
            for (int i = 0; i < grv_Pemission.RowCount; i++)
            {
                int idForm = Convert.ToInt32(grv_Pemission.GetRowCellValue(i, colID));
                var q = us_menubc.SetDM().FirstOrDefault(p => p.ID == idForm); //DungChung.Ham._listBC.FirstOrDefault(p => p.ID == idForm);
                string qType = q == null ? "" : q.Kieu.ToString();
                if (qType == "Report")
                {
                    grv_Pemission.SetRowCellValue(i, colAdd, false);
                    grv_Pemission.SetRowCellValue(i, colEdit, false);
                    grv_Pemission.SetRowCellValue(i, colDel, false);
                }
                unboundData.Add(false);
                object unboundValue = grv_Pemission.GetRowCellValue(i, "colChon");
                unboundValue = (bool)unboundValue;
                grv_Pemission.SetRowCellValue(grv_Pemission.FocusedRowHandle, "colChon", unboundValue);
            }
        }

        public class TTDN
        {
            public int ID { get; set; }
            public string MaCB { get; set; }
            public string TenDN { get; set; }
            public string TenGoi { get; set; }
            public string TenKP { get; set; }
        }

        private void btnAddForm_Click(object sender, EventArgs e)
        {
            List<int> lIDForm = new List<int>();
            for (int i = 0; i < grv_Pemission.RowCount; i++)
            {
                int idForm = Convert.ToInt32(grv_Pemission.GetRowCellValue(i, colID));
                lIDForm.Add(idForm);
            }
            frm_DanhMucFormBC frm = new frm_DanhMucFormBC(lIDForm);
            this.Hide();
            frm.pasDSIDFormDC = new frm_DanhMucFormBC.PassDSIDFormBC(PassData);
            frm.ShowDialog();
            this.Show();
        }

        private void PassData(List<Library_CLS.Lis_His.limenu> menu)
        {
            #region thêm những idform có được chọn thêm vào grv           
            foreach (Library_CLS.Lis_His.limenu f in menu)
            {
                int count = 0;
                for (int i = 0; i < grv_Pemission.RowCount; i++)
                {
                    int idForm = Convert.ToInt32(grv_Pemission.GetRowCellValue(i, colID));
                    if (idForm == f.ID)
                    {
                        count++;
                        break;
                    }
                }
                if (count == 0)
                {
                    _lPms.Add(new Permission { ID = f.ID, TenDN = _tenDN, C_New = false, C_Edit = false, C_Delete = false, C_View = false, C_Print = false });
                }
                #endregion
            }
            var qTenF = us_menubc.SetDM().OrderBy(p => p.TenBC).ToList(); //DungChung.Ham._listBC.OrderBy(p => p.TenBC).ToList();
            lupTenBC.DataSource = qTenF;
            grc_Pemission.DataSource = _lPms;

        }

        private void txtTenDN_EditValueChanged(object sender, EventArgs e)
        {
        }
        private void loadAdmin()
        {
            var q = (from a in _data.ADMINs
                     join cb in _data.CanBoes on a.MaCB equals cb.MaCB
                     join kp in _data.KPhongs on cb.MaKP equals kp.MaKP
                     select new TTDN { ID = a.ID, MaCB = a.MaCB, TenDN = a.TenDN, TenGoi = a.TenGoi, TenKP = kp.TenKP }
                    ).Where(p => _tenDN == "" || p.TenDN.Contains(_tenDN) || p.TenGoi.Contains(_tenDN)).OrderBy(p => p.TenDN).ThenBy(p => p.TenGoi).ToList();
            grc_Admin.DataSource = q;
        }

        private void grv_Admin_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int row = grv_Admin.FocusedRowHandle;
            if (grv_Admin.GetRowCellValue(row, "TenDN") != null)
            {
                _tenDN = grv_Admin.GetRowCellValue(row, "TenDN").ToString();
                loadPemission();
            }
        }

        private void grv_Pemission_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int row = e.RowHandle;
            GridView view = sender as GridView;
            if (e.Column.Name == "colXoaBanGhi")
            {
                int idForm = Convert.ToInt32(grv_Pemission.GetRowCellValue(row, colID));
                if (MessageBox.Show("Bạn muốn bỏ quyền cho Form/Báo cáo này ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (idForm > 0 && _tenDN != "")
                    {
                        var xoa = _data.Permissions.Where(p => p.ID == idForm && p.TenDN == _tenDN);
                        if (xoa.Count() > 0)
                        {
                            foreach (var x in xoa)
                                _data.Permissions.Remove(x);
                            _data.SaveChanges();
                        }
                    }
                    view.DeleteRow(row);
                }
            }
        }


        private void grv_Pemission_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            int row = grv_Pemission.FocusedRowHandle;
            if (grv_Pemission.GetRowCellValue(row, colID) != null && _tenDN != "")
            {
                bool ck = Convert.ToBoolean(grv_Pemission.GetRowCellValue(row, colChon));
                if (ck)
                {
                    int idForm = Convert.ToInt32(grv_Pemission.GetRowCellValue(row, colID));
                    bool add = Convert.ToBoolean(grv_Pemission.GetRowCellValue(row, colAdd));
                    bool edit = Convert.ToBoolean(grv_Pemission.GetRowCellValue(row, colEdit));
                    bool del = Convert.ToBoolean(grv_Pemission.GetRowCellValue(row, colDel));
                    bool view = Convert.ToBoolean(grv_Pemission.GetRowCellValue(row, colView));
                    bool In = Convert.ToBoolean(grv_Pemission.GetRowCellValue(row, col_In));
                    var q = _data.Permissions.Where(p => p.ID == idForm && p.TenDN == _tenDN).ToList();
                    if (q.Count() > 0) //sửa
                    {
                        var sua = q.First();
                        sua.ID = idForm;
                        sua.TenDN = _tenDN;
                        sua.C_New = add;
                        sua.C_Edit = edit;
                        sua.C_Delete = del;
                        sua.C_View = view;
                        sua.C_Print = In;
                        _data.SaveChanges();
                    }
                    else//thêm mới
                    {
                        Permission moi = new Permission();
                        moi.ID = idForm;
                        moi.TenDN = _tenDN;
                        moi.C_New = add;
                        moi.C_Edit = edit;
                        moi.C_Delete = del;
                        moi.C_View = view;
                        moi.C_Print = In;
                        _data.Permissions.Add(moi);
                        _data.SaveChanges();
                    }
                }
            }


        }

        private void grv_Admin_DataSourceChanged(object sender, EventArgs e)
        {
            grv_Admin_FocusedRowChanged(null, null);
        }

        private void txtTenDN_TextChanged(object sender, EventArgs e)
        {
            if (txtTenDN.EditValue != null)
            {
                txtTenDN.Properties.Appearance.Font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont, FontStyle.Regular);
                _tenDN = txtTenDN.Text;
            }
            else
            {
                //txtTenDN.Properties.Appearance.Reset();
                txtTenDN.Properties.Appearance.Font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont, FontStyle.Italic);
                _tenDN = "";
            }
            loadAdmin();
        }

        private bool ckSelect = false;
        private void grv_Pemission_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;

            if (IsAllowingEdit(view, view.FocusedRowHandle))
            {
                if (CheckBC(view, view.FocusedRowHandle))
                {
                    if (view.FocusedColumn.FieldName == "C_New" || view.FocusedColumn.FieldName == "C_Edit" || view.FocusedColumn.FieldName == "C_Delete")
                    {
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                if (view.FocusedColumn.FieldName == "C_New" || view.FocusedColumn.FieldName == "C_Edit" || view.FocusedColumn.FieldName == "C_Delete" || view.FocusedColumn.FieldName == "C_View" || view.FocusedColumn.FieldName == "C_Print")
                {
                    e.Cancel = true;
                }
            }


            #region set editable cho check bõ đối với báo cáo hoặc form
            //for (int i = 0; i < grv_Pemission.RowCount; i++)
            //{
            //    bool ckChon = Convert.ToBoolean(grv_Pemission.GetRowCellValue(grv_Pemission.FocusedRowHandle, colChon));
            //    if (ckChon)
            //    {
            //        if (CheckBC(view, view.FocusedRowHandle))
            //        {
            //            if (view.FocusedColumn.FieldName == "C_New" || view.FocusedColumn.FieldName == "C_Edit" || view.FocusedColumn.FieldName == "C_Delete")
            //            {
            //                e.Cancel = true;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (view.FocusedColumn.FieldName == "C_New" || view.FocusedColumn.FieldName == "C_Edit" || view.FocusedColumn.FieldName == "C_Delete" || view.FocusedColumn.FieldName == "C_View")
            //        {
            //            e.Cancel = true;
            //        }
            //    }
            //}
            #endregion


        }
        private bool IsAllowingEdit(DevExpress.XtraGrid.Views.Grid.GridView view, int row)
        {
            try
            {
                bool val = Convert.ToBoolean(view.GetRowCellValue(row, colChon));
                return val;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckBC(GridView view, int row)
        {
            int idForm = Convert.ToInt32(grv_Pemission.GetRowCellValue(row, colID));
            var q = us_menubc.SetDM().Single(p => p.ID == idForm);// DungChung.Ham._listBC.Single(p => p.ID == idForm);
            string t = q.Kieu.ToString();
            if (t == "Report")
                return true;
            else
                return false;
        }

        private void grv_Pemission_CellValueChanging_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int row = grv_Pemission.FocusedRowHandle;
            if (row > 0)
            {
                ckSelect = Convert.ToBoolean(grv_Pemission.GetRowCellValue(grv_Pemission.FocusedRowHandle, colChon));
            }
        }
        ArrayList unboundData = new ArrayList();

        private void grv_Pemission_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            int rowindex = e.ListSourceRowIndex;
            if (e.Column.FieldName == "colChon")
            {
                if (e.IsGetData)
                    try

                    {
                        e.Value = unboundData[rowindex];
                    }
                    catch (Exception)
                    {
                        unboundData.Add(false);
                        //e.Value = unboundData[rowindex];
                    }
                else
                    try
                    {
                        unboundData[rowindex] = e.Value;
                    }
                    catch (Exception)
                    {
                        unboundData[rowindex] = false;
                    }
            }
        }

        private void btnAddForm_MouseHover(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void btnAddForm_MouseLeave(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void frm_CapQuyen_FormClosing(object sender, FormClosingEventArgs e)
        {
            grv_Pemission.FocusedColumn = colChon;
            grv_Pemission_ValidateRow(null, null);
        }

        private void grv_Admin_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;
                contextMenuStrip1.Show(view.GridControl, e.Point);
            }
        }

        private void copyQuyềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var row = (TTDN)grv_Admin.GetFocusedRow();
            if (row != null)
            {
                frm_CopyQuyen frm = new frm_CopyQuyen(row.TenDN);
                frm.ShowDialog();
            }
        }
    }
}