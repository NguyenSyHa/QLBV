using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormThamSo
{
    public partial class frm_DsDichVuChon : DevExpress.XtraEditors.XtraForm
    {
        public frm_DsDichVuChon()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mau">ID báo cáo</param>
        public frm_DsDichVuChon(int mau)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.mau = mau;
        }

        #region class DV
        public class DV
        {
            private bool chon;

            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
            private int maDV;

            public int MaDV
            {
                get { return maDV; }
                set { maDV = value; }
            }
            private string tenDV;

            public string TenDV
            {
                get { return tenDV; }
                set { tenDV = value; }
            }
        }
        #endregion

        List<DV> _lDV = new List<DV>();
        public delegate void PassMaDV(List<int> lmadv);
        public PassMaDV passMaDV;
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_DsDichVuChon_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var dv = (from d in data.DichVus.Where(p =>mau == 92 ? true : p.PLoai == 2) select new { d.MaDV, d.TenDV }).ToList();
            if (dv.Count > 0)
            {
                DV dvMoi1 = new DV();
                dvMoi1.MaDV = 0;
                dvMoi1.TenDV = "Chọn tất cả";
                dvMoi1.Chon = true;
                _lDV.Add(dvMoi1);
                foreach (var item in dv)
                {
                    DV dvMoi = new DV();
                    dvMoi.MaDV = item.MaDV;
                    dvMoi.TenDV = item.TenDV;
                    dvMoi.Chon = true;
                    _lDV.Add(dvMoi);
                }
                grcDV.DataSource = _lDV.OrderBy(p => p.MaDV).ToList();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        List<int> _dsMaDV = new List<int>();//List MaDV đã chọn
        private int mau;
        private void btnXem_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<DV> dvChon = new List<DV>();
             dvChon = _lDV.Where(p => p.MaDV > 0 && p.Chon == true).ToList();
            _dsMaDV.Clear();
            string madv = string.Empty;
            if (dvChon.Count > 0)
            {
                foreach (var item in dvChon)
                {
                    _dsMaDV.Add(item.MaDV);//Lưu danh sách MaDV đã chọn
                   // madv += item.MaDV.ToString() + ";";
                }
               // madv = madv.Remove(madv.Length - 1, 1);
                
            }
            if (passMaDV != null)
            {
                this.Close();
                passMaDV(_dsMaDV);
            }
           // MessageBox.Show("Danh sách MaDV được chọn: " + madv);//Xem kết quả
        }

        private void grvDV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvDV.GetFocusedRowCellValue("TenDV") != null)
                {
                    string Ten = grvDV.GetFocusedRowCellValue("TenDV").ToString();

                    if (Ten.Equals("Chọn tất cả"))
                    {
                        if (_lDV.First().Chon == true)
                        {
                            foreach (var a in _lDV)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lDV)
                            {
                                a.Chon = true;
                            }
                        }
                        grcDV.DataSource = "";
                        grcDV.DataSource = _lDV.ToList();
                    }
                }
            }
        }

        private void txtTenDV_EditValueChanging(object sender, EventArgs e)
        {
            string tendv = txtTenDV.Text.Trim();
            var dsDvTimKiem = (from dv in _lDV.Where(p => p.TenDV.ToLower().Contains(tendv)) select dv).ToList();
            grcDV.DataSource = dsDvTimKiem.OrderBy(p => p.MaDV).ToList();
        }
    }
}