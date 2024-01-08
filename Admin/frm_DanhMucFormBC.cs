using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using QLBV.DungChung;
using DevExpress.XtraGrid.Views.Grid;


namespace QLBV.FormThamSo
{
    public partial class frm_DanhMucFormBC : DevExpress.XtraEditors.XtraForm
    {
        QLBV_Database.QLBVEntities data;
        private List<int> _lIDForm = new List<int>(); // ds ID form
        List<Library_CLS.Lis_His.limenu> listMenu = new List<Library_CLS.Lis_His.limenu>();
        List<Library_CLS.Lis_His.limenu> _listMenuAll = new List<Library_CLS.Lis_His.limenu>();
        List<Library_CLS.Lis_His.limenu> _listSelectedMenu = new List<Library_CLS.Lis_His.limenu>();
        public delegate void PassDSIDFormBC(List<Library_CLS.Lis_His.limenu> listFBC);
        public PassDSIDFormBC pasDSIDFormDC;       
        public frm_DanhMucFormBC()
        {
            InitializeComponent();
        }

        public frm_DanhMucFormBC(List<int> lIDForm)
        {
            this._lIDForm = lIDForm;
            InitializeComponent();
        }
       
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frm_DanhMucFormBC_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //List<FormThamSo.us_menubc.limenu> lbc = (from i in DungChung.Ham.loadDSBaoCao() select new FormThamSo.us_menubc.limenu { Loai = i.Loai }).Distinct().ToList();
            //lbc.Add(new FormThamSo.us_menubc.limenu { Loai = " Tất cả" });
            //    List<Library_CLS.Lis_His.limenu> lbc = DungChung.Ham._listBC;
            List<Library_CLS.Lis_His.limenu> lbc = us_menubc.SetDM(); 
            lbc.Add(new Library_CLS.Lis_His.limenu { Loai = " Tất cả" });
           var _lbc = (from n in lbc select new { n.Loai }).OrderBy(p=>p.Loai).Distinct();
            lupLoai.Properties.DataSource = _lbc.ToList();
           // loadLupLoai(); 
           
            _listMenuAll = us_menubc.SetDM().Where(p => !_lIDForm.Contains(p.ID)).OrderBy(p => p.TenBC).ToList();
            cklDSFormBC.DataSource = _listMenuAll; 
        }
        private void loadDSFormBC()
        {
            string loaiBC = "";
            if (lupLoai.EditValue != null && lupLoai.Text != " Tất cả")  
                    loaiBC = lupLoai.Text;
            listMenu = _listMenuAll.Where(p=>p.Loai == loaiBC || loaiBC == "").ToList();
            cklDSFormBC.DataSource = listMenu;
            List<int> lIDForm = new List<int>();
            for (int k = 0; k < grv_SelectedFrm.RowCount; k++)
            {
                int id = Convert.ToInt32(grv_SelectedFrm.GetRowCellValue(k, colIDBC));
                lIDForm.Add(id);
            }

            for (int i = 0; i < cklDSFormBC.ItemCount; i++)
            {
                if (lIDForm.Contains(Convert.ToInt32(cklDSFormBC.GetItemValue(i))))
                {
                    cklDSFormBC.SetItemChecked(i, true);
                }
                else
                    cklDSFormBC.SetItemChecked(i, false);
            }
        }
        private bool checkClose = false;
        private void btnChon_Click(object sender, EventArgs e)
        {
            //if (pasDSIDFormDC != null)
            //{
            //    //foreach (var item in cklDSFormBC.CheckedItems)
            //    //{
            //    //        var frm =  (FormThamSo.us_menubc.limenu)item;
            //    //        int iD = Convert.ToInt32(frm.ID);
            //    //        string tenBC = frm.TenBC;
            //    //        listMenu.Add(frm);                   
            //    //}
            //    List<FormThamSo.us_menubc.limenu> listBC = new List<FormThamSo.us_menubc.limenu>();
            //    for( int i =0 ; i< grv_SelectedFrm.RowCount; i++)
            //    {                  
            //        int iD = Convert.ToInt32(grv_SelectedFrm.GetRowCellValue(i,colIDBC));
            //        string tenBC = grv_SelectedFrm.GetRowCellValue(i, colFormName).ToString();
            //        listBC.Add(new FormThamSo.us_menubc.limenu { ID = iD, TenBC = tenBC });
            //    }
            //    pasDSIDFormDC(listBC);
            //} 
            checkClose = true;
            this.Close();
        }
      

        private void grv_SelectedFrm_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colUnSelect")
            {
                int row = e.RowHandle;
                GridView view = sender as GridView;
                view.DeleteRow(row);
                List<int> lIDForm = new List<int>();
                for(int k = 0; k< grv_SelectedFrm.RowCount; k++)
                {
                    int id =Convert.ToInt32(grv_SelectedFrm.GetRowCellValue(k, colIDBC));
                    lIDForm.Add(id);
                }
              
                for (int i = 0; i < cklDSFormBC.ItemCount; i++)
                {                 
                    if (lIDForm.Contains(Convert.ToInt32(cklDSFormBC.GetItemValue(i))))
                    {                       
                        cklDSFormBC.SetItemChecked(i, true);                       
                    }
                    else
                        cklDSFormBC.SetItemChecked(i, false);   
                }
                               
            }   
        }
        private bool reload = false;
        private void cklDSFormBC_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
           // listMenu = new List<FormThamSo.us_menubc.limenu>();
            if (cklDSFormBC.SelectedIndex != -1)
            {
                //for (int i = 0; i < cklDSFormBC.ItemCount; i++)
                //{
                //    if (cklDSFormBC.GetItemChecked(i))
                //    {
                //        int iD = Convert.ToInt32(cklDSFormBC.GetItemValue(i));
                //        string tenBC = cklDSFormBC.GetItemText(i).ToString();
                //        listMenu.Add(new FormThamSo.us_menubc.limenu { ID = iD, TenBC = tenBC });
                //    }
                //}
                //grc_SelectedFrm.DataSource = listMenu;
                if (!reload)
                {
                    if (e.State == CheckState.Checked)
                    {
                        int iD = Convert.ToInt32(Convert.ToInt32(cklDSFormBC.GetItemValue(e.Index)));
                        string tenBC = cklDSFormBC.GetItemText(e.Index).ToString();
                        _listSelectedMenu.Add(new Library_CLS.Lis_His.limenu { ID = iD, TenBC = tenBC });
                    }
                    else
                    {
                        int iD = Convert.ToInt32(Convert.ToInt32(cklDSFormBC.GetItemValue(e.Index)));
                        _listSelectedMenu = _listSelectedMenu.Where(p => p.ID != iD).ToList();
                    }
                    grc_SelectedFrm.DataSource = _listSelectedMenu;
                    grc_SelectedFrm.RefreshDataSource();
                }
            }
           
        }

        private void frm_DanhMucFormBC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (grv_SelectedFrm.RowCount > 0)
            {
                if (checkClose)
                {
                    if (pasDSIDFormDC != null)
                    {
                        _listSelectedMenu = new List<Library_CLS.Lis_His.limenu>();
                        for (int i = 0; i < grv_SelectedFrm.RowCount; i++)
                        {
                            int iD = Convert.ToInt32(grv_SelectedFrm.GetRowCellValue(i, colIDBC));
                            string tenBC = grv_SelectedFrm.GetRowCellValue(i, colFormName).ToString();
                            _listSelectedMenu.Add(new Library_CLS.Lis_His.limenu { ID = iD, TenBC = tenBC });
                        }
                        pasDSIDFormDC(_listSelectedMenu);
                    }
                }
                else
                {
                    if (MessageBox.Show("Bạn muốn thêm các form - báo cáo đã chọn không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (pasDSIDFormDC != null)
                        {
                            _listSelectedMenu = new List<Library_CLS.Lis_His.limenu>();
                            for (int i = 0; i < grv_SelectedFrm.RowCount; i++)
                            {
                                int iD = Convert.ToInt32(grv_SelectedFrm.GetRowCellValue(i, colIDBC));
                                string tenBC = grv_SelectedFrm.GetRowCellValue(i, colFormName).ToString();
                                _listSelectedMenu.Add(new Library_CLS.Lis_His.limenu { ID = iD, TenBC = tenBC });
                            }
                            pasDSIDFormDC(_listSelectedMenu);
                        }
                    }
                }
            }
        }

        private void lupLoai_EditValueChanged(object sender, EventArgs e)
        {
            reload = true;
            loadDSFormBC();
            reload = false;
            
        }       
    }
}