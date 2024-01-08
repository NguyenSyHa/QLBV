using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class Frm_SetMenu : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SetMenu()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<MenuBC> _Menu = new List<MenuBC>();
        private void Frm_SetMenu_Load(object sender, EventArgs e)
        {
            var q = (from M in _Data.MenuBCs select new { M.ID, M.Loai, M.Nhom, M.Status, M.TenBC }).ToList();
            if (q.Count > 0)
            {
                _Menu.Clear();
                foreach (var a in q)
                {
                    MenuBC themmoi = new MenuBC();
                    themmoi.ID = a.ID;
                    themmoi.Loai = a.Loai;
                    themmoi.Nhom = a.Nhom;
                    themmoi.Status = a.Status;
                    themmoi.TenBC = a.TenBC;
                    _Menu.Add(themmoi);
                    
                }
            }
            var q1 = (from M in _Data.MenuBCs select new { M.Nhom}).Distinct().ToList();
            if (q1.Count > 0)
            {
                foreach (var a in q1)
                {
                    if (a.Nhom != null && a.Nhom.ToString() != "")
                    {
                        cboNhom.Items.Add(a.Nhom);
                    }
                }
            }
            GrcMenu.DataSource = _Menu.OrderBy(p=>p.ID);

        }
        string _a1 = "";
        public void GetValue(string a)
        {
            _a1 = a;
        }
        private void GrvMenu_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "colSua")
            {
                if (GrvMenu.GetFocusedRowCellValue(colLoai) != null)
                {
                    
                    bool _a = false;
                    bool _b = false;
                    bool _c = false;
                    bool _d = false;
                    bool _e = false;
                    if (GrvMenu.GetFocusedRowCellValue(colLoai).ToString() != "")
                    {
                        string _ten = GrvMenu.GetFocusedRowCellValue(colLoai).ToString();
                        if (_ten.Contains("Khoa dược"))
                        { _a = true; }
                        if (_ten.Contains("Viện phí"))
                        { _b = true; }
                        if (_ten.Contains("BHYT"))
                        { _c = true; }
                        if (_ten.Contains("Tổng hợp"))
                        { _d = true; }
                        if (_ten.Contains("CLS"))
                        { _e = true; }
                        if (_ten.Contains("Khám bệnh|Điều trị"))
                        { _e = true; }
                    }
                    FormThamSo.Frm_SuaBC frm = new Frm_SuaBC(_a, _b, _c, _d, _e);
                    frm.par = new Frm_SuaBC._getdata(GetValue);
                    frm.ShowDialog();

                    GrvMenu.SetFocusedRowCellValue(colLoai, _a1);
                        //_ten = GrvMenu.GetFocusedRowCellValue(colLoai).ToString();
                    
                }
                
            }
        }

        private void GrvMenu_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //if (e.Column.Name == "colSua")
            //{ }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            foreach (var a in _Menu)
            {
                var xoa = _Data.MenuBCs.Single(p => p.ID == a.ID);
                _Data.MenuBCs.Remove(xoa);
                _Data.SaveChanges();
                MenuBC themmoi = new MenuBC();
                themmoi.ID = a.ID;
                themmoi.Loai = a.Loai;
                themmoi.Nhom = a.Nhom;
                themmoi.Status = a.Status;
                themmoi.TenBC = a.TenBC;
                _Data.MenuBCs.Add(themmoi);
                _Data.SaveChanges();
            }
            MessageBox.Show("Lưu thành công");
            Frm_SetMenu_Load(sender, e);
        }

        private void txtTimkiem_EditValueChanged(object sender, EventArgs e)
        {
            int id = 0;
            int.TryParse(txtTimkiem.Text, out id);
            GrcMenu.DataSource = _Menu.OrderBy(p => p.ID).Where(p=>p.TenBC.Contains(txtTimkiem.Text.Trim())|| p.ID==id).ToList();
        }

        private void btnBanDau_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult _result;
                _result = MessageBox.Show("Bạn muốn khôi phục thiết lập ban đầu?","Thiết lập ban đầu",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (_result == DialogResult.Yes)
                {
                    _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    var mn = _Data.MenuBCs.ToList();
                    foreach (var a in mn)
                    {
                        _Data.MenuBCs.Remove(a);
                        _Data.SaveChanges();
                    }
                    us_menubc mm = new us_menubc();
                    mm.us_menubc_Load(sender, e);
                    MessageBox.Show("Khôi phục thiết lập ban đầu thành công");
                    this.Frm_SetMenu_Load(sender, e);
                }
            }catch(Exception){
            MessageBox.Show("Lỗi không thực hiện được!");
            }
        }
    }
}