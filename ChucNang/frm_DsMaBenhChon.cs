using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLBV.ChucNang
{
    public partial class frm_DsMaBenhChon : DevExpress.XtraEditors.XtraForm
    {
        public frm_DsMaBenhChon()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mau">ID báo cáo</param>
        public frm_DsMaBenhChon(int mau)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.mau = mau;
        }

        #region class ICD
        public class ICD
        {
            private bool chon;

            public bool Chon
            {
                get { return chon; }
                set { chon = value; }
            }
            private string maICD;

            public string MaICD
            {
                get { return maICD; }
                set { maICD = value; }
            }
            private string tenICD;

            public string TenICD
            {
                get { return tenICD; }
                set { tenICD = value; }
            }
        }
        #endregion

        List<ICD> _lICD = new List<ICD>();
        public delegate void PassMaICD(List<string> lmaICD);
        public PassMaICD passMaICD;
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_DsMaBenhChon_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ICD = (from d in data.ICD10 select new { d.MaICD, d.TenICD }).ToList();
            if (ICD.Count > 0)
            {
                ICD ICDMoi1 = new ICD();
                ICDMoi1.MaICD = "";
                ICDMoi1.TenICD = "Chọn tất cả";
                ICDMoi1.Chon = true;
                _lICD.Add(ICDMoi1);
                foreach (var item in ICD)
                {
                    ICD ICDMoi = new ICD();
                    ICDMoi.MaICD = item.MaICD;
                    ICDMoi.TenICD = item.TenICD;
                    ICDMoi.Chon = true;
                    _lICD.Add(ICDMoi);
                }
                grcICD.DataSource = _lICD.OrderBy(p => p.MaICD).ToList();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        List<string> _dsMaICD = new List<string>();//List MaDV đã chọn
        private int mau;
        private void btnXem_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<ICD> ICDChon = new List<ICD>();
            ICDChon = _lICD.Where(p => p.MaICD != null && p.MaICD != "" && p.Chon == true).ToList();
            _dsMaICD.Clear();
            string maICD = string.Empty;
            if (ICDChon.Count > 0)
            {
                foreach (var item in ICDChon)
                {
                    _dsMaICD.Add(item.MaICD);//Lưu danh sách MaDV đã chọn
                                           // madv += item.MaDV.ToString() + ";";
                }
                // madv = madv.Remove(madv.Length - 1, 1);

            }
            if (passMaICD != null)
            {
                this.Close();
                passMaICD(_dsMaICD);
            }
            // MessageBox.Show("Danh sách MaDV được chọn: " + madv);//Xem kết quả
        }

        private void grvDV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvICD.GetFocusedRowCellValue("TenICD") != null)
                {
                    string Ten = grvICD.GetFocusedRowCellValue("TenICD").ToString();

                    if (Ten.Equals("Chọn tất cả"))
                    {
                        if (_lICD.First().Chon == true)
                        {
                            foreach (var a in _lICD)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lICD)
                            {
                                a.Chon = true;
                            }
                        }
                        grcICD.DataSource = "";
                        grcICD.DataSource = _lICD.ToList();
                    }
                }
            }
        }

        private void txtTenICD_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            string tendv = txtTenICD.Text.Trim();
            var dsDvTimKiem = (from dv in _lICD.Where(p => p.TenICD.ToLower().Contains(tendv)) select dv).ToList();
            grcICD.DataSource = dsDvTimKiem.OrderBy(p => p.MaICD).ToList();
        }
    }
}
