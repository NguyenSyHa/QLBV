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
    public partial class frm_BcCTKhoaDuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_BcCTKhoaDuoc()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BcCTKhoaDuoc_Load(object sender, EventArgs e)
        {
            var q = from TK in _data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();
            for (int i = 2014; i <= 2020; i++) {
                cboNam.Properties.Items.Add(i);
            }
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private bool KT() {
            //if (string.IsNullOrEmpty(cboNam.Text)) {
            //    MessageBox.Show("Bạn chưa chọn năm xem dữ liệu");
            //    cboNam.Focus();
            //    return false;     
            //}
            if (string.IsNullOrEmpty(lupKho.Text))
            {
                MessageBox.Show("Bạn chưa chọn kho");
                lupKho.Focus();
                return false;
            }
            return true;
        }
        private void btnTaoBC_Click(object sender, EventArgs e)
        {
            if (KT())
            {

                DateTime tungay = System.DateTime.Now.Date;
                DateTime denngay = System.DateTime.Now.Date;
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                frmIn frm = new frmIn();
                int nam = 0;
                string makho = "";
                if (lupKho.EditValue != null)
                    makho = lupKho.EditValue.ToString();
                if (!string.IsNullOrEmpty(cboNam.Text))
                    nam = Convert.ToInt32(cboNam.Text);
                //BaoCao.rep_BcCTKhoaDuoc rep = new BaoCao.rep_BcCTKhoaDuoc(nam,makho);
               // rep.NgayTu.Value = tungay;
               // rep.NgayDen.Value = denngay;
               //// rep.Nam.Value = "Năm: " + cboNam.Text;
               // rep.Nam.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày:" + denngay.ToString().Substring(0,10);
               // rep.YKien.Value = memoKienNghi.Text;
               // rep.DanhGia.Value = memoTuDG.Text;
               // rep.NhanXet.Value = memoTuNX.Text;
               // rep.CreateDocument();
               // frm.prcIN.PrintingSystem = rep.PrintingSystem;
               // frm.ShowDialog();
            }
        }

        
    }
}