using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.FormNhap
{
    public partial class frm_nhappersion : DevExpress.XtraEditors.XtraForm
    {
        public frm_nhappersion()
        {
            InitializeComponent();
        }

        private void frm_nhappersion_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _person = _data.People.Where(p => p.Status == -1).ToList();
            bdpersion.DataSource = _person.ToList();
            grdpersion.DataSource = bdpersion;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
                        
        }
        public void luu(int i)
            
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string sthe = Convert.ToString(grpersion.GetRowCellValue(i, col_SThe));
            var _ktthe = _data.People.Where(p => p.SThe == sthe).FirstOrDefault();
            if (_ktthe == null)
            {
                
                string madt = Convert.ToString(grpersion.GetRowCellValue(i, col_MaDTuong));
                string tenbn = Convert.ToString(grpersion.GetRowCellValue(i, col_TenBNhan));
                int gt = Convert.ToInt32(grpersion.GetRowCellValue(i, col_GTinh));
                string dchi = Convert.ToString(grpersion.GetRowCellValue(i, col_DChi));
                DateTime hbhtu = Convert.ToDateTime(grpersion.GetRowCellValue(i, col_HanBHTu));
                DateTime hbhden = Convert.ToDateTime(grpersion.GetRowCellValue(i, col_HanBHDen));
                DateTime ngayc = Convert.ToDateTime(grpersion.GetRowCellValue(i, col_NgayCap));
                string macs = Convert.ToString(grpersion.GetRowCellValue(i, col_MaCS));
                string matinh = Convert.ToString(grpersion.GetRowCellValue(i, col_MaTinh));
                string mahuyen = Convert.ToString(grpersion.GetRowCellValue(i, col_MaHuyen));
                string maxa = Convert.ToString(grpersion.GetRowCellValue(i, col_MaXa));
                int namsinh = Convert.ToInt32(grpersion.GetRowCellValue(i, col_NSinh));
                string ngaysinh = Convert.ToString(grpersion.GetRowCellValue(i, col_NgaySinh));
                string tsinh = Convert.ToString(grpersion.GetRowCellValue(i, col_ThangSinh));
                DateTime ngayhm = Convert.ToDateTime(grpersion.GetRowCellValue(i, col_NgayHM));
                string kvuc = Convert.ToString(grpersion.GetRowCellValue(i, col_KhuVuc));
                Person personnew = new Person();
                personnew.SThe = sthe;
                personnew.MaDTuong = madt;
                personnew.TenBNhan = tenbn;
                personnew.GTinh = gt;
                personnew.DChi = dchi;
                personnew.HanBHTu = hbhtu;
                personnew.HanBHDen = hbhden;
                personnew.NgayCap = ngayc;
                personnew.MaCS = macs;
                personnew.MaTinh = matinh;
                personnew.MaHuyen = mahuyen;
                personnew.MaXa = maxa;
                personnew.NSinh = namsinh;
                personnew.NgaySinh = ngaysinh;
                personnew.ThangSinh = tsinh;
                personnew.NgayHM = ngayhm;
                personnew.KhuVuc = kvuc;
                personnew.Status = -1;
                _data.People.Add(personnew);
                _data.SaveChanges();
                grdpersion.Refresh();
            }
            else
            {
                string madt = Convert.ToString(grpersion.GetRowCellValue(i, col_MaDTuong));
                string tenbn = Convert.ToString(grpersion.GetRowCellValue(i, col_TenBNhan));
                int gt = Convert.ToInt32(grpersion.GetRowCellValue(i, col_GTinh));
                string dchi = Convert.ToString(grpersion.GetRowCellValue(i, col_DChi));
                DateTime hbhtu = Convert.ToDateTime(grpersion.GetRowCellValue(i, col_HanBHTu));
                DateTime hbhden = Convert.ToDateTime(grpersion.GetRowCellValue(i, col_HanBHDen));
                DateTime ngayc = Convert.ToDateTime(grpersion.GetRowCellValue(i, col_NgayCap));
                string macs = Convert.ToString(grpersion.GetRowCellValue(i, col_MaCS));
                string matinh = Convert.ToString(grpersion.GetRowCellValue(i, col_MaTinh));
                string mahuyen = Convert.ToString(grpersion.GetRowCellValue(i, col_MaHuyen));
                string maxa = Convert.ToString(grpersion.GetRowCellValue(i, col_MaXa));
                int namsinh = Convert.ToInt32(grpersion.GetRowCellValue(i, col_NSinh));
                string ngaysinh = Convert.ToString(grpersion.GetRowCellValue(i, col_NgaySinh));
                string tsinh = Convert.ToString(grpersion.GetRowCellValue(i, col_ThangSinh));
                DateTime ngayhm = Convert.ToDateTime(grpersion.GetRowCellValue(i, col_NgayHM));
                string kvuc = Convert.ToString(grpersion.GetRowCellValue(i, col_KhuVuc));
                Person personnew = new Person();
                personnew.SThe = sthe;
                personnew.MaDTuong = madt;
                personnew.TenBNhan = tenbn;
                personnew.GTinh = gt;
                personnew.DChi = dchi;
                personnew.HanBHTu = hbhtu;
                personnew.HanBHDen = hbhden;
                personnew.NgayCap = ngayc;
                personnew.MaCS = macs;
                personnew.MaTinh = matinh;
                personnew.MaHuyen = mahuyen;
                personnew.MaXa = maxa;
                personnew.NSinh = namsinh;
                personnew.NgaySinh = ngaysinh;
                personnew.ThangSinh = tsinh;
                personnew.NgayHM = ngayhm;
                personnew.KhuVuc = kvuc;
                personnew.Status = -1;
                _data.SaveChanges();
                grdpersion.Refresh();
            }
        }

        private void grpersion_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.Name=="col_SThe")
            {
                QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                string sthe = Convert.ToString(grpersion.GetRowCellValue(e.RowHandle, col_SThe));
                var _lttthe = _data.People.Where(p => p.SThe == sthe).FirstOrDefault();
                if(_lttthe!=null)
                {
                    grpersion.SetFocusedRowCellValue(col_MaDTuong, _lttthe.MaDTuong);
                    grpersion.SetFocusedRowCellValue(col_TenBNhan, _lttthe.TenBNhan);
                    grpersion.SetFocusedRowCellValue(col_GTinh, _lttthe.GTinh);
                    grpersion.SetFocusedRowCellValue(col_DChi, _lttthe.DChi);
                    grpersion.SetFocusedRowCellValue(col_HanBHTu, _lttthe.HanBHTu);
                    grpersion.SetFocusedRowCellValue(col_HanBHDen, _lttthe.HanBHDen);
                    grpersion.SetFocusedRowCellValue(col_NgayCap, _lttthe.NgayCap);
                    grpersion.SetFocusedRowCellValue(col_Status, _lttthe.Status);
                    grpersion.SetFocusedRowCellValue(col_MaCS, _lttthe.MaCS);
                    grpersion.SetFocusedRowCellValue(col_MaTinh, _lttthe.MaTinh);
                    grpersion.SetFocusedRowCellValue(col_MaHuyen, _lttthe.MaHuyen);
                    grpersion.SetFocusedRowCellValue(col_MaXa, _lttthe.MaXa);
                    grpersion.SetFocusedRowCellValue(col_NSinh, _lttthe.NSinh);
                    grpersion.SetFocusedRowCellValue(col_NgaySinh, _lttthe.NgaySinh);
                    grpersion.SetFocusedRowCellValue(col_ThangSinh, _lttthe.ThangSinh);
                    grpersion.SetFocusedRowCellValue(col_NgayHM, _lttthe.NgayHM);
                    grpersion.SetFocusedRowCellValue(col_KhuVuc, _lttthe.KhuVuc);
                }
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grpersion_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
       
            
            luu(e.RowHandle);
            MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK);
        }
    }
}