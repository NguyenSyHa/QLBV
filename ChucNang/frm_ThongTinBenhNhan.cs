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
    public partial class frm_ThongTinBenhNhan : DevExpress.XtraEditors.XtraForm
    {
        public frm_ThongTinBenhNhan()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        class Bennhan
        {
            public int IDPersson { get; set; }
            public string SThe { get; set; }
            public string TenBNhan { get; set; }
            public string GioiTinh { get; set; }
            public string NgaySinh { get; set; }

        }

        private void txtMaBenhNhan_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void txtTenbenhNhan_EditValueChanged(object sender, EventArgs e)
        {

        }
        private List<Bennhan> BenhNhan(int th)
        {
            List<Bennhan> benhnhan = new List<Bennhan>();

            int Mbn = 0;
            if (txtMaBenhNhan.Text != "")
            {
                Mbn = Convert.ToInt32(txtMaBenhNhan.Text);
            }
            string TenBenhNhan = txtTenbenhNhan.Text;
            var bn = (from a in data.People.Where(p => (th == 1 ? (Mbn == 0 ? true : p.IDPerson == Mbn) : (TenBenhNhan == "" ? true : p.TenBNhan.Contains(TenBenhNhan))))
                      select new
                      {
                          a.IDPerson,
                          a.SThe,
                          a.TenBNhan,
                          a.GTinh,
                          a.NgaySinh,
                          a.ThangSinh,
                          a.NSinh,
                      }
                ).ToList();

            foreach (var item in bn)
            {
                Bennhan themoi = new Bennhan();
                themoi.IDPersson = item.IDPerson;
                themoi.TenBNhan = item.TenBNhan;
                themoi.SThe = item.SThe;
                themoi.GioiTinh = item.GTinh == 1 ? "Nam" : "Nữ";
                themoi.NgaySinh = item.NgaySinh + "/" + item.ThangSinh + "/" + item.NSinh;
                benhnhan.Add(themoi);
            }
            return benhnhan;
        }

        private void frm_ThongTinBenhNhan_Load(object sender, EventArgs e)
        {
            LoadThongTinBenhNhan();
        }
        void LoadThongTinBenhNhan()
        {

            List<Bennhan> benhnhan = new List<Bennhan>();
            var bn = (from a in data.People
                      select new
                      {
                          a.IDPerson,
                          a.SThe,
                          a.TenBNhan,
                          a.GTinh,
                          a.NgaySinh,
                          a.ThangSinh,
                          a.NSinh,
                      }
              ).ToList();
            foreach (var item in bn)
            {
                Bennhan themoi = new Bennhan();
                themoi.IDPersson = item.IDPerson;
                themoi.TenBNhan = item.TenBNhan;
                themoi.SThe = item.SThe;
                themoi.GioiTinh = item.GTinh == 1 ? "Nam" : "Nữ";
                themoi.NgaySinh = item.NgaySinh + " / " + item.ThangSinh + " / " + item.NSinh;
                benhnhan.Add(themoi);
            }
            grcHienThi.DataSource = benhnhan.ToList();
        }


     

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                txtMaBenhNhan.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, grvMaBenhNhan).ToString();
                txtTenbenhNhan.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, grvTenBenhNhan).ToString();
            }
            catch 
            {
                
                
            }
           
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {          
            barButtonItem2.Enabled = false;
            barButtonItem3.Enabled = true;
            txtTenbenhNhan.Enabled = true;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)     
        {
            string TenBenhNhan = DungChung.Ham.ToFirstUpper(txtTenbenhNhan.Text.Trim());
            Person pesson = new Person();
            int mbn = Convert.ToInt32(txtMaBenhNhan.Text);
            pesson = data.People.Where(p => p.IDPerson == mbn).Single();
            pesson.TenBNhan = TenBenhNhan;

            if (data.SaveChanges() > 0)
            {
                XtraMessageBox.Show("Đã sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadThongTinBenhNhan();
                barButtonItem2.Enabled = true;
                barButtonItem3.Enabled = false;
                txtTenbenhNhan.Enabled = false;
            }        

        }
    }
}
