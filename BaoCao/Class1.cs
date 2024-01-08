using System;using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLBV.BaoCao
{
    class Class1
    {
        //using System;using QLBV_Database;
        //using System.Collections.Generic;
        //using System.ComponentModel;
        //using System.Data;
        //using System.Drawing;
        //using System.Text;
        //using System.Windows.Forms;
        //using DevExpress.XtraEditors;
        //using DevExpress.XtraEditors.Repository;
        //using System.Diagnostics;

        //namespace Media
        //{
        //    public partial class frmAddCategory : DevExpress.XtraEditors.XtraForm
        //    {
        //        public frmAddCategory()
        //        {
        //            InitializeComponent();            
        //        }       

        //        System.Configuration.AppSettingsReader ar = new System.Configuration.AppSettingsReader();
        //        sqlCon sc=new sqlCon();
        //        bool inprogress = false;
        //        string command;
        //        private void simpleButton_saveDraft_Click(object sender, EventArgs e)
        //        {
        //            DataRow drr = gridView1.GetFocusedDataRow();
        //            drr[1] = textEdit_catName.Text;
        //            drr[2] = memoEdit_catDescription.Text;  
        //            inprogress = false;
        //            simpleButton_saveDraft.Enabled = false;
        //            textEdit_catName.Properties.ReadOnly = true;
        //            memoEdit_catDescription.Properties.ReadOnly = true;
        //            simpleButton_edit.Enabled = true;
        //            simpleButton_saveData.Enabled = true;
        //            simpleButton_delete.Enabled = true;
        //            simpleButton_cancelEdit.Enabled = false;
        //            simpleButton_add.Enabled = true;
        //            simpleButton_delete.Enabled = true;
        //            gridView1.FocusedRowHandle= gridView1.GetVisibleRowHandle(0);
        //        }
        //        frmAppSettings setting = new frmAppSettings();
        //        private void frmAddCategory_Load(object sender, EventArgs e)
        //        {
        //            if (ar.GetValue("first run", typeof(string)).ToString() == "1")
        //            {
        //                XtraMessageBox.Show("Vui lòng thiết lập đường dẫn CSDL.", "Thiết lập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                setting.ShowDialog();
        //                Application.Restart();
        //            }
        //            sc.DataPath = ar.GetValue("current datapath", typeof(string)).ToString();
        //            sc.SQLVer = ar.GetValue("current sql ver", typeof(string)).ToString();
        //            gridView1.OptionsView.ShowAutoFilterRow = true;
        //            barStaticItem_dataModifiedTime.Caption = "Lần lưu dữ liệu cuối cùng: chưa bao giờ";
        //            simpleButton_cancelEdit.Enabled = false;
        //            simpleButton_saveData.Enabled = false;
        //            gridView1.OptionsBehavior.Editable = false;
        //            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
        //            simpleButton_saveDraft.Enabled = false;
        //            textEdit_catName.Properties.ReadOnly = true;
        //            memoEdit_catDescription.Properties.ReadOnly = true;
        //            command = "select * from category";
        //            try
        //            {
        //                if (!sc.sqlOpenCon())
        //                {
        //                    barStaticItem_conStatus.Caption = "Kết nối dữ liệu: thất bại!";
        //                    simpleButton_add.Enabled = false;
        //                    simpleButton_delete.Enabled = false;a
        //                    simpleButton_edit.Enabled = false;
        //                    XtraMessageBox.Show("Lỗi kết nối dữ liệu, vui lòng xem lại các thiết lập tùy chỉnh.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    setting.ShowDialog();
        //                    Application.Restart();
        //                }
        //                gridControl_dataDisplay.DataSource = sc.getData(command);
        //                gridView1.Columns[1].Caption = "Tên thể loại";
        //                gridView1.Columns[2].Caption = "Mô ta chi tiết";
        //                gridView1.Columns[0].Visible = false;
        //                barStaticItem_conStatus.Caption = "Kết nối dữ liệu: đã mở";
        //            }
        //            catch { }
        //        }

        //        private void simpleButton_delete_Click(object sender, EventArgs e)
        //        {
        //            DataRow dr = gridView1.GetFocusedDataRow();
        //            DialogResult diaRe = XtraMessageBox.Show("Xác nhận xóa thể loại: " + dr[1].ToString(), "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        //            if (diaRe== System.Windows.Forms.DialogResult.Yes)
        //            {
        //                try
        //                {
        //                    gridView1.DeleteSelectedRows();
        //                    simpleButton_saveData.Enabled = true;
        //                }
        //                catch (Exception ex)
        //                {

        //                    XtraMessageBox.Show(ex.Message);
        //                }
        //            }
        //        }

        //        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //        {
        //            if (inprogress)
        //            {
        //                return;
        //            }
        //            try
        //            {
        //                DataRow dr;
        //                dr = gridView1.GetFocusedDataRow();
        //                textEdit_catName.Text = dr[1].ToString();
        //                memoEdit_catDescription.Text = dr[2].ToString();
        //            }
        //            catch { }
        //        }

        //        private void simpleButton_add_Click(object sender, EventArgs e)
        //        {
        //            addNew = true;
        //            gridView1.AddNewRow();            
        //            simpleButton_delete.Enabled = false;
        //            simpleButton_add.Enabled = false;
        //            simpleButton_cancelEdit.Enabled = true;
        //            simpleButton_edit.Enabled = false;
        //            textEdit_catName.Text = "";
        //            memoEdit_catDescription.Text = "";
        //            textEdit_catName.Focus();
        //            inprogress = true;
        //            simpleButton_saveDraft.Enabled = true;
        //            textEdit_catName.Properties.ReadOnly = false;
        //            memoEdit_catDescription.Properties.ReadOnly = false;
        //            gridView1.GetVisibleRowHandle(gridView1.RowCount);
        //            editRowIndex = gridView1.RowCount;
        //        }

        //        DataRow editingDataRow;
        //        int editRowIndex;
        //        void editDataRow()
        //        {
        //            simpleButton_delete.Enabled = false;
        //            simpleButton_cancelEdit.Enabled = true;
        //            textEdit_catName.Properties.ReadOnly = false;
        //            memoEdit_catDescription.Properties.ReadOnly = false;
        //            simpleButton_edit.Enabled = false;
        //            simpleButton_saveDraft.Enabled = true;
        //            editingDataRow = gridView1.GetFocusedDataRow();
        //            textEdit_catName.Text = editingDataRow[1].ToString();
        //            memoEdit_catDescription.Text = editingDataRow[2].ToString();
        //            editRowIndex = gridView1.GetFocusedDataSourceRowIndex();
        //            simpleButton_add.Enabled = false;
        //            textEdit_catName.Focus();
        //        }
        //        private void simpleButton_edit_Click(object sender, EventArgs e)
        //        {
        //            editDataRow();
        //        }

        //        bool formClosing()
        //        {
        //            if (!firstask)
        //                return true;
        //            if (simpleButton_saveData.Enabled)
        //            {
        //                DialogResult diaRe = XtraMessageBox.Show("Bạn chưa lưu bảng dữ liệu, bạn có thật sự muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        //                if (diaRe == System.Windows.Forms.DialogResult.No)
        //                {
        //                    return false ;
        //                }
        //            }
        //            if (simpleButton_saveDraft.Enabled)
        //            {
        //                DialogResult diaRe = XtraMessageBox.Show("Bạn đang sửa dữ liệu, bạn có thật sự muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        //                if (diaRe == System.Windows.Forms.DialogResult.No)
        //                {
        //                    return false;
        //                }
        //            }
        //            return true;
        //        }
        //        bool firstask=false;
        //        private void simpleButton_cancel_Click(object sender, EventArgs e)
        //        {
        //            if (formClosing())
        //            {
        //                firstask = false;
        //                Close();
        //            }
        //        }

        //        private void simpleButton_saveData_Click(object sender, EventArgs e)
        //        {
        //            try
        //            {
        //                simpleButton_saveData.Enabled = false;
        //                DataTable dt0 = gridControl_dataDisplay.DataSource as DataTable;
        //                sc.updateData(dt0, command);
        //                barStaticItem_conStatus.Caption = "Cập nhật dữ liệu: thành công";
        //                barStaticItem_dataModifiedTime.Caption = "Lần lưu dữ liệu cuối cùng: " + DateTime.Now.ToLongTimeString();
        //            }
        //            catch (Exception ex)
        //            {
        //                XtraMessageBox.Show(ex.Message, "Lỗi cập nhật dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                barStaticItem_conStatus.Caption = "Cập nhật dữ liệu: thất bại";
        //            }
        //        }

        //        bool addNew = false;
        //        private void simpleButton_cancelEdit_Click(object sender, EventArgs e)
        //        {
        //            if (addNew)
        //            {
        //                gridView1.DeleteRow(gridView1.FocusedRowHandle);
        //                addNew = false;
        //            }
        //            inprogress = false;
        //            simpleButton_delete.Enabled = true;
        //            simpleButton_edit.Enabled = true;
        //            textEdit_catName.Properties.ReadOnly = true;
        //            memoEdit_catDescription.Properties.ReadOnly = true;
        //            gridView1.FocusedRowHandle = gridView1.GetVisibleRowHandle(0);
        //            simpleButton_cancelEdit.Enabled = false;
        //            simpleButton_saveDraft.Enabled = false;
        //            simpleButton_add.Enabled = true;
        //            simpleButton_delete.Enabled = true;
        //        }

        //        private void gridView1_DoubleClick(object sender, EventArgs e)
        //        {
        //            editDataRow();
        //        }

        //        private void frmAddCategory_FormClosing(object sender, FormClosingEventArgs e)
        //        {
        //            if (!formClosing())
        //                e.Cancel = true;
        //        }
        //    }
        //}
    }
}
