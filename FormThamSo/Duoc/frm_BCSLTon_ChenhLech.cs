using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_BCSLTon_ChenhLech : DevExpress.XtraEditors.XtraForm
    {
        int _makp = 0;
        public frm_BCSLTon_ChenhLech()
        {
            InitializeComponent();
        }
        public frm_BCSLTon_ChenhLech(int ma)
        {
            InitializeComponent();
            this._makp = ma;
        }
        QLBV_Database.QLBVEntities _data;
        public class DichV{
        string tendv;
            int madv;
            public string TenDV{
            set{tendv =value;}
                get{return tendv;}
            }
            public int MaDV
            {
                set { madv = value; }
                get { return madv; }
            }
        }
        List<DichV> _ldv = new List<DichV>();
        private void frm_BCSLTon_ChenhLech_Load(object sender, EventArgs e)
        {
            
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ds = from  d in _data.NhapDs.Where(p=>p.MaKP==_makp)
                     join dct in _data.NhapDcts on d.IDNhap equals dct.IDNhap
                     join dv in _data.DichVus on dct.MaDV equals dv.MaDV
                     join n in _data.NhomDVs on dv.IDNhom equals n.IDNhom
                     select new
                     {
                         dv.MaDV,
                         dv.TenDV,                    
                         dct.SoLuongN,
                         dct.SoLuongX,
                         dv.SLMin,
                         n.IDNhom,
                         n.TenNhom
                     };
           
            var dst = (from d in ds
                      group d by new { d.TenNhom, d.MaDV, d.TenDV, d.SLMin, d.IDNhom } into kq
                      select new
                      {
                          MaDV = kq.Key.MaDV,
                          TenDV = kq.Key.TenDV,
                          IDNhom = kq.Key.IDNhom,
                          TenNhom = kq.Key.TenNhom,
                          SLMin = kq.Key.SLMin, // yêu cầu SLMin != null 
                          SLT = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                          CL = kq.Key.SLMin - (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),  
                          //CL = kq.Where(p => (kq.Sum(q => q.SoLuongN) - kq.Sum(q => q.SoLuongX)) <= kq.Key.SLMin).Sum(p => p.SoLuongN) - kq.Where(p => (kq.Sum(q => q.SoLuongN) - kq.Sum(q => q.SoLuongX)) <= kq.Key.SLMin).Sum(p => p.SoLuongX) - kq.Key.SLMin,
                          //SLT = kq.Where(p => (kq.Sum(q => q.SoLuongN) - kq.Sum(q => q.SoLuongX)) <= kq.Key.SLMin).Sum(p => p.SoLuongN) - kq.Where(p => (kq.Sum(q => q.SoLuongN) - kq.Sum(q => q.SoLuongX)) <= kq.Key.SLMin).Sum(p => p.SoLuongX),
                      }).Distinct().Where(p => p.SLT < p.SLMin).OrderBy(p => p.MaDV).ThenBy(p => p.MaDV).ToList();
             grc_DichVu.DataSource = dst;
             lblCount.Text = dst.Count().ToString();
            
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var ds = from d in _data.NhapDs.Where(p => p.MaKP == _makp)
                     join dct in _data.NhapDcts on d.IDNhap equals dct.IDNhap
                     join dv in _data.DichVus on dct.MaDV equals dv.MaDV
                     join n in _data.NhomDVs on dv.IDNhom equals n.IDNhom
                     select new
                     {
                         dv.MaDV,
                         dv.TenDV,
                         dct.SoLuongN,
                         dct.SoLuongX,
                         dv.SLMin,
                         n.IDNhom,
                         n.TenNhom
                     };
            var dst = (from d in ds
                       group d by new { d.TenNhom, d.MaDV, d.TenDV, d.SLMin, d.IDNhom } into kq
                       select new
                       {
                           MaDV = kq.Key.MaDV,
                           TenDV = kq.Key.TenDV,
                           IDNhom = kq.Key.IDNhom,
                           TenNhom = kq.Key.TenNhom,
                           SLMin = kq.Key.SLMin, // yêu cầu SLMin != null                           
                           SLT = kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX),
                           CL = kq.Key.SLMin - (kq.Sum(p => p.SoLuongN) - kq.Sum(p => p.SoLuongX)),                          
                       }).Distinct().Where(p => p.SLT < p.SLMin).OrderBy(p => p.IDNhom).ThenBy( p => p.MaDV).ToList();
            grc_DichVu.DataSource = dst;

            frmIn frm = new frmIn();
            BaoCao.rep_DMDichVu rep = new BaoCao.rep_DMDichVu();
            rep.Title.Value = ("Danh mục dịch vụ").ToUpper();
            rep.DataSource = dst;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void hplXoa_Click(object sender, EventArgs e)
        {
            int maDV = 0;
            int row = grv_DichVu.FocusedRowHandle;
            if (grv_DichVu.GetRowCellValue(row, "MaDV") == null)
                maDV = 0;
            else
                maDV = Convert.ToInt32( grv_DichVu.GetRowCellValue(row, "MaDV")); 
        }
    }
}